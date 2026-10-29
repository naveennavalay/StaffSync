
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class StaffSyncTrustedTimeService
    {
        #region Shared Instance

        // One shared instance throughout the application process.
        public static readonly StaffSyncTrustedTimeService objTrustedTime =
            new StaffSyncTrustedTimeService();

        private static readonly HttpClient httpClient;

        private const double AllowedDifferenceSeconds = 8;

        private const int MinimumRequiredSources = 2;

        private const int RequestTimeoutSeconds = 7;

        #endregion

        #region Time Sources

        private static readonly string[] TimeSources =
        {
            "https://www.google.com/generate_204",
            "https://www.microsoft.com/",
            "https://www.cloudflare.com/"
        };

        #endregion

        #region Application Time Zone

        // Read the configured time zone once when first accessed.
        private static readonly Lazy<TimeZoneInfo> configuredTimeZone =
            new Lazy<TimeZoneInfo>(() =>
            {
                string timeZoneID =
                    ConfigurationManager.AppSettings["StaffSyncTimeZone"];

                if (string.IsNullOrWhiteSpace(timeZoneID))
                {
                    throw new ConfigurationErrorsException(
                        "StaffSyncTimeZone is missing in App.config.");
                }

                try
                {
                    return TimeZoneInfo.FindSystemTimeZoneById(timeZoneID);
                }
                catch (TimeZoneNotFoundException)
                {
                    throw new ConfigurationErrorsException(
                        "Invalid StaffSyncTimeZone configured in App.config: "
                        + timeZoneID);
                }
                catch (InvalidTimeZoneException)
                {
                    throw new ConfigurationErrorsException(
                        "The configured time zone is invalid: " + timeZoneID);
                }
            });

        public TimeZoneInfo ApplicationTimeZone
        {
            get { return configuredTimeZone.Value; }
        }

        #endregion

        #region Constructor

        static StaffSyncTrustedTimeService()
        {
            ServicePointManager.SecurityProtocol |=
                SecurityProtocolType.Tls12;

            httpClient = new HttpClient();

            httpClient.Timeout =
                TimeSpan.FromSeconds(RequestTimeoutSeconds);
        }

        private StaffSyncTrustedTimeService()
        {
        }

        #endregion

        #region Models

        public class TrustedTimeResult
        {
            public bool IsTrusted { get; set; }

            public DateTimeOffset UtcTimestamp { get; set; }

            public DateTime UtcDateTime { get; set; }

            public int VerifiedSourceCount { get; set; }

            public string TimeSource { get; set; }

            public string Message { get; set; }
        }

        private class TimeSample
        {
            public DateTimeOffset UtcTime { get; set; }

            public string Source { get; set; }
        }

        #endregion

        #region Get Trusted Time

        /// <summary>
        /// Retrieves timestamps from independent HTTPS sources
        /// and verifies that at least two sources agree.
        /// </summary>
        public async Task<TrustedTimeResult> GetTime()
        {
            List<Task<TimeSample>> requests =
                new List<Task<TimeSample>>();

            foreach (string source in TimeSources)
            {
                requests.Add(GetTimeFromSourceAsync(source));
            }

            TimeSample[] results =
                await Task.WhenAll(requests);

            List<TimeSample> validSamples = results
                .Where(x => x != null)
                .OrderBy(x => x.UtcTime)
                .ToList();

            if (validSamples.Count < MinimumRequiredSources)
            {
                throw new Exception(
                    "Unable to obtain trusted date and time. " +
                    "At least two time sources must respond successfully.");
            }

            // Find the largest group of agreeing time sources.

            List<TimeSample> matchingSamples = null;

            for (int i = 0; i < validSamples.Count; i++)
            {
                List<TimeSample> currentGroup =
                    validSamples.Where(x =>
                        Math.Abs(
                            (x.UtcTime - validSamples[i].UtcTime)
                            .TotalSeconds)
                        <= AllowedDifferenceSeconds
                    ).ToList();

                if (matchingSamples == null ||
                    currentGroup.Count > matchingSamples.Count)
                {
                    matchingSamples = currentGroup;
                }
            }

            if (matchingSamples == null ||
                matchingSamples.Count < MinimumRequiredSources)
            {
                throw new Exception(
                    "Trusted time verification failed. " +
                    "The available time sources do not agree.");
            }

            // Calculate the median timestamp from agreeing sources.

            List<TimeSample> orderedSamples = matchingSamples
                .OrderBy(x => x.UtcTime)
                .ToList();

            DateTimeOffset trustedTimestamp;

            int middle = orderedSamples.Count / 2;

            if (orderedSamples.Count % 2 == 1)
            {
                trustedTimestamp =
                    orderedSamples[middle].UtcTime;
            }
            else
            {
                long firstTicks =
                    orderedSamples[middle - 1]
                    .UtcTime.UtcDateTime.Ticks;

                long secondTicks =
                    orderedSamples[middle]
                    .UtcTime.UtcDateTime.Ticks;

                trustedTimestamp = new DateTimeOffset(
                    firstTicks + ((secondTicks - firstTicks) / 2),
                    TimeSpan.Zero);
            }

            return new TrustedTimeResult
            {
                IsTrusted = true,

                UtcTimestamp = trustedTimestamp,

                UtcDateTime = trustedTimestamp.UtcDateTime,

                VerifiedSourceCount = matchingSamples.Count,

                TimeSource = "HTTPS Time Consensus",

                Message = "Trusted date and time verified successfully."
            };
        }

        #endregion

        #region Get Time From Individual Source

        private static async Task<TimeSample>
            GetTimeFromSourceAsync(string url)
        {
            try
            {
                using (HttpRequestMessage request =
                    new HttpRequestMessage(HttpMethod.Get, url))
                {
                    request.Headers.CacheControl =
                        new CacheControlHeaderValue
                        {
                            NoCache = true,
                            NoStore = true
                        };

                    Stopwatch stopwatch = Stopwatch.StartNew();

                    using (CancellationTokenSource cancellation =
                        new CancellationTokenSource(
                            TimeSpan.FromSeconds(RequestTimeoutSeconds)))
                    {
                        using (HttpResponseMessage response =
                            await httpClient.SendAsync(
                                request,
                                HttpCompletionOption.ResponseHeadersRead,
                                cancellation.Token))
                        {
                            stopwatch.Stop();

                            response.EnsureSuccessStatusCode();

                            if (!response.Headers.Date.HasValue)
                                return null;

                            DateTimeOffset serverTime =
                                response.Headers.Date.Value.ToUniversalTime();

                            // Estimate the time at the midpoint
                            // of the HTTP request.
                            serverTime = serverTime.AddMilliseconds(
                                stopwatch.Elapsed.TotalMilliseconds / 2);

                            return new TimeSample
                            {
                                UtcTime = serverTime,

                                Source = new Uri(url).Host
                            };
                        }
                    }
                }
            }
            catch
            {
                // Ignore unavailable sources.
                // GetTime() verifies the minimum source count.
                return null;
            }
        }

        #endregion

        #region Get Current Local Date Time

        /// <summary>
        /// Retrieves trusted UTC time and automatically
        /// converts it using the configured application time zone.
        /// </summary>
        public async Task<DateTime> GetCurrentLocalDateTime()
        {
            TrustedTimeResult trustedTime = await GetTime();

            return GetLocalDateTime(trustedTime);
        }

        #endregion

        #region Local Date Time Conversion

        /// <summary>
        /// Converts a verified UTC timestamp into the
        /// configured application time zone.
        /// </summary>
        public DateTime GetLocalDateTime(TrustedTimeResult trustedTime)
        {
            if (trustedTime == null || !trustedTime.IsTrusted)
            {
                throw new InvalidOperationException(
                    "A verified timestamp is required.");
            }

            return TimeZoneInfo.ConvertTimeFromUtc(
                trustedTime.UtcDateTime,
                ApplicationTimeZone);
        }

        #endregion

        #region MS Access Date Time Conversion

        /// <summary>
        /// Returns UTC DateTime with Unspecified Kind
        /// for storing in an MS Access Date/Time field.
        ///
        /// IMPORTANT:
        /// Always interpret this database value as UTC.
        /// </summary>
        public DateTime GetAccessUtcDateTime(
            TrustedTimeResult trustedTime)
        {
            if (trustedTime == null || !trustedTime.IsTrusted)
            {
                throw new InvalidOperationException(
                    "A verified timestamp is required.");
            }

            return DateTime.SpecifyKind(
                trustedTime.UtcDateTime,
                DateTimeKind.Unspecified);
        }

        #endregion
    }
}
