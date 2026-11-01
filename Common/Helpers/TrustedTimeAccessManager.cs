using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public sealed class TrustedTimeAccessManager
    {
        private static readonly Lazy<TrustedTimeAccessManager> _instance =
            new Lazy<TrustedTimeAccessManager>(() => new TrustedTimeAccessManager());

        public static TrustedTimeAccessManager Instance { get { return _instance.Value; } }

        // Reads the interval from the executable application's App.config.
        // Default is 5 minutes when the setting is missing or invalid.
        private const string RevalidationIntervalSettingKey =
            "TrustedTimeRevalidationIntervalMinutes";

        private const int DefaultRevalidationIntervalMinutes = 5;

        private static readonly TimeSpan RevalidationInterval =
            LoadRevalidationInterval();

        /// <summary>
        /// Loads the periodic trusted-time revalidation interval from App.config.
        /// The value is expressed in whole minutes and must be at least 1.
        /// </summary>
        private static TimeSpan LoadRevalidationInterval()
        {
            string configuredValue = null;

            try
            {
                configuredValue =
                    ConfigurationManager.AppSettings[RevalidationIntervalSettingKey];
            }
            catch (ConfigurationErrorsException ex)
            {
                Trace.TraceWarning(
                    "Unable to read {0} from App.config. Using the default of {1} minutes. {2}",
                    RevalidationIntervalSettingKey,
                    DefaultRevalidationIntervalMinutes,
                    ex.Message);
            }

            int minutes;

            if (!int.TryParse(
                    configuredValue,
                    NumberStyles.Integer,
                    CultureInfo.InvariantCulture,
                    out minutes) ||
                minutes < 1)
            {
                if (!string.IsNullOrWhiteSpace(configuredValue))
                {
                    Trace.TraceWarning(
                        "Invalid value '{0}' for {1}. Using the default of {2} minutes.",
                        configuredValue,
                        RevalidationIntervalSettingKey,
                        DefaultRevalidationIntervalMinutes);
                }

                minutes = DefaultRevalidationIntervalMinutes;
            }

            return TimeSpan.FromMinutes(minutes);
        }

        private readonly StaffSyncTrustedTimeService _timeService;
        private readonly object _syncRoot = new object();
        private Timer _revalidationTimer;
        private Stopwatch _trustedClock;
        private DateTimeOffset _trustedUtcAtBaseline;
        private bool _isInitialized;
        private bool _isLocked = true;
        private bool _isChecking;
        private string _failureReason = "Trusted time has not yet been verified.";

        public event EventHandler AccessStateChanged;

        private TrustedTimeAccessManager()
        {
            _timeService = StaffSyncTrustedTimeService.objTrustedTime;
        }

        public bool IsInitialized { get { lock (_syncRoot) return _isInitialized; } }
        public bool IsLocked { get { lock (_syncRoot) return _isLocked; } }
        public bool IsChecking { get { lock (_syncRoot) return _isChecking; } }
        public string FailureReason { get { lock (_syncRoot) return _failureReason; } }

        public async Task InitializeAsync()
        {
            await VerifyTrustedTimeAsync();
            lock (_syncRoot)
            {
                if (_isLocked) throw new InvalidOperationException(_failureReason);
                if (_revalidationTimer == null)
                    _revalidationTimer = new Timer(
                        RevalidationTimerCallback, null,
                        RevalidationInterval, RevalidationInterval);
            }
        }

        public async Task<bool> RetryTrustedTimeAsync()
        {
            try
            {
                await VerifyTrustedTimeAsync();
                return !IsLocked && !IsChecking;
            }
            catch { return false; }
        }

        private async Task VerifyTrustedTimeAsync()
        {
            lock (_syncRoot)
            {
                if (_isChecking)
                    throw new InvalidOperationException(
                        "Trusted time verification is already in progress.");
                _isChecking = true;
            }

            RaiseAccessStateChanged();

            try
            {
                StaffSyncTrustedTimeService.TrustedTimeResult result =
                    await _timeService.GetTime().ConfigureAwait(false);

                if (result == null || result.SuccessfulSourceCount < 2)
                    throw new InvalidOperationException(
                        "At least two agreeing trusted time sources are required.");

                lock (_syncRoot)
                {
                    _trustedUtcAtBaseline = result.UtcTime.ToUniversalTime();
                    _trustedClock = Stopwatch.StartNew();
                    _isInitialized = true;
                    _isLocked = false;
                    _failureReason = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LockApplication(
                    "StaffSync could not verify trusted date and time." +
                    Environment.NewLine + Environment.NewLine + ex.Message);
                throw;
            }
            finally
            {
                lock (_syncRoot) { _isChecking = false; }
                RaiseAccessStateChanged();
            }
        }

        private async void RevalidationTimerCallback(object state)
        {
            try { await VerifyTrustedTimeAsync().ConfigureAwait(false); }
            catch { /* Failure is recorded and announced by VerifyTrustedTimeAsync. */ }
        }

        /// <summary>
        /// Checks whether business operations are permitted.
        /// A periodic check does not invalidate the last verified monotonic clock.
        /// Initial verification and failed verification remain fail-closed.
        /// </summary>
        public void EnsureAccess()
        {
            lock (_syncRoot)
            {
                if (!_isInitialized || _isLocked || _trustedClock == null)
                {
                    throw new InvalidOperationException(
                        string.IsNullOrWhiteSpace(_failureReason)
                            ? "StaffSync is locked because trusted time is unavailable."
                            : _failureReason);
                }
            }
        }

        /// <summary>
        /// Returns trusted UTC time advanced using the monotonic stopwatch.
        /// The last verified clock remains available during periodic revalidation.
        /// </summary>
        public DateTime GetTrustedUtcNow()
        {
            lock (_syncRoot)
            {
                if (!_isInitialized || _isLocked || _trustedClock == null)
                    throw new InvalidOperationException(_failureReason);

                return _trustedUtcAtBaseline
                    .AddMilliseconds(_trustedClock.Elapsed.TotalMilliseconds)
                    .UtcDateTime;
            }
        }

        public DateTime GetTrustedLocalNow()
        {
            EnsureAccess();
            DateTime utcNow = GetTrustedUtcNow();
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(
                _timeService.GetConfiguredTimeZone());
            return TimeZoneInfo.ConvertTimeFromUtc(
                DateTime.SpecifyKind(utcNow, DateTimeKind.Utc), zone);
        }

        public void LockApplication(string reason)
        {
            lock (_syncRoot)
            {
                _isLocked = true;
                _failureReason = string.IsNullOrWhiteSpace(reason)
                    ? "Trusted time verification failed." : reason;
            }
            RaiseAccessStateChanged();
        }

        private void RaiseAccessStateChanged()
        {
            EventHandler handler = AccessStateChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void Stop()
        {
            lock (_syncRoot)
            {
                if (_revalidationTimer != null)
                {
                    _revalidationTimer.Dispose();
                    _revalidationTimer = null;
                }
            }
        }
    }
}
