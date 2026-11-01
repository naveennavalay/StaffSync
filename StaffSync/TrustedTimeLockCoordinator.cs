using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Common.Helpers;

namespace StaffSync
{
    /// <summary>
    /// Coordinates trusted-time state with the WinForms UI.
    ///
    /// A short verification that succeeds quickly will disable business forms
    /// while it is running, but will NOT flash the restriction window.
    /// The restriction window appears only when verification actually fails,
    /// or when a verification remains in progress beyond the display delay.
    /// </summary>
    public static class TrustedTimeLockCoordinator
    {
        // A brief check should not display a full-screen restriction dialog.
        // If verification is still running after this interval, show the
        // checking window so the user understands why the application is paused.
        private const int LockWindowDisplayDelayMilliseconds = 900;

        private static readonly object SyncRoot = new object();

        private static Form _mainForm;
        private static TrustedTimeLockForm _lockForm;
        private static Dictionary<Form, bool> _previousStates;

        private static Timer _displayDelayTimer;
        private static bool _attached;
        private static bool _formsDisabled;
        private static bool _isClosingLockForm;

        public static void Attach(Form mainForm)
        {
            if (mainForm == null)
                throw new ArgumentNullException("mainForm");

            lock (SyncRoot)
            {
                if (_attached)
                    return;

                _attached = true;
                _mainForm = mainForm;

                TrustedTimeAccessManager.Instance.AccessStateChanged +=
                    Gate_AccessStateChanged;

                _mainForm.FormClosed += MainForm_FormClosed;
            }

            DispatchToMainForm(ApplyCurrentState);
        }

        private static void Gate_AccessStateChanged(object sender, EventArgs e)
        {
            DispatchToMainForm(ApplyCurrentState);
        }

        private static void DispatchToMainForm(Action action)
        {
            Form form;

            lock (SyncRoot)
            {
                form = _mainForm;
            }

            if (form == null || form.IsDisposed)
                return;

            if (!form.IsHandleCreated)
            {
                EventHandler handleCreatedHandler = null;

                handleCreatedHandler = (s, e) =>
                {
                    form.HandleCreated -= handleCreatedHandler;
                    DispatchToMainForm(action);
                };

                form.HandleCreated += handleCreatedHandler;
                return;
            }

            try
            {
                if (form.InvokeRequired)
                    form.BeginInvoke(action);
                else
                    action();
            }
            catch (ObjectDisposedException)
            {
                // The main form is closing.
            }
            catch (InvalidOperationException)
            {
                // The UI handle is being destroyed.
            }
        }

        private static void ApplyCurrentState()
        {
            if (_mainForm == null || _mainForm.IsDisposed)
                return;

            TrustedTimeAccessManager gate =
                TrustedTimeAccessManager.Instance;

            // If a lock form is already visible, keep it visible during retry.
            if (_lockForm != null && !_lockForm.IsDisposed)
            {
                if (gate.IsLocked || gate.IsChecking)
                {
                    _lockForm.UpdateStatus();
                    return;
                }

                // Verification succeeded. Let the lock form display its
                // ONLINE / VERIFIED confirmation and close itself.
                _lockForm.ShowOnlineStatusThenClose();
                return;
            }

            if (gate.IsLocked)
            {
                // A failed verification is a confirmed restriction.
                // Do not delay showing the restriction window.
                CancelDisplayDelay();
                DisableOpenForms();
                ShowLockForm();
                return;
            }

            if (gate.IsChecking)
            {
                // During periodic revalidation, the last trusted monotonic clock
                // remains valid. Keep an already initialized application usable.
                // Initial verification still restricts the UI until it succeeds.
                if (gate.IsInitialized)
                {
                    CancelDisplayDelay();
                    RestoreOpenForms();
                    return;
                }

                DisableOpenForms();
                StartDisplayDelay();
                return;
            }

            // Trusted time is verified and no check is running.
            CancelDisplayDelay();
            RestoreOpenForms();
        }

        private static void StartDisplayDelay()
        {
            if (_displayDelayTimer != null)
                return;

            _displayDelayTimer = new Timer();
            _displayDelayTimer.Interval =
                LockWindowDisplayDelayMilliseconds;

            _displayDelayTimer.Tick += DisplayDelayTimer_Tick;
            _displayDelayTimer.Start();
        }

        private static void DisplayDelayTimer_Tick(object sender, EventArgs e)
        {
            CancelDisplayDelay();

            if (_mainForm == null || _mainForm.IsDisposed)
                return;

            TrustedTimeAccessManager gate =
                TrustedTimeAccessManager.Instance;

            // Re-check current state. If the check succeeded while the
            // timer was waiting, do not show the restriction window.
            if (gate.IsLocked)
            {
                DisableOpenForms();
                ShowLockForm();
            }
            else if (gate.IsChecking && !gate.IsInitialized)
            {
                // Only the very first trusted-time verification should keep
                // the application UI disabled while the check is pending.
                DisableOpenForms();
                ShowLockForm();
            }
            else
            {
                // An initialized application can continue using its last
                // verified monotonic clock during periodic revalidation.
                RestoreOpenForms();
            }
        }

        private static void CancelDisplayDelay()
        {
            if (_displayDelayTimer == null)
                return;

            _displayDelayTimer.Stop();
            _displayDelayTimer.Tick -= DisplayDelayTimer_Tick;
            _displayDelayTimer.Dispose();
            _displayDelayTimer = null;
        }

        private static void DisableOpenForms()
        {
            if (_formsDisabled)
                return;

            _previousStates = new Dictionary<Form, bool>();

            foreach (Form form in Application.OpenForms.Cast<Form>().ToArray())
            {
                if (form == null || form.IsDisposed)
                    continue;

                // Do not disable the restriction window itself.
                if (form is TrustedTimeLockForm)
                    continue;

                _previousStates[form] = form.Enabled;
                form.Enabled = false;
            }

            _formsDisabled = true;
        }

        private static void ShowLockForm()
        {
            if (_lockForm != null && !_lockForm.IsDisposed)
            {
                _lockForm.UpdateStatus();
                return;
            }

            if (_mainForm == null || _mainForm.IsDisposed)
                return;

            _lockForm = new TrustedTimeLockForm();
            _lockForm.FormClosed += LockForm_FormClosed;

            // Modeless display: the coordinator continues processing state
            // changes without running a nested ShowDialog message loop.
            _lockForm.Show();
            _lockForm.BringToFront();
            _lockForm.Activate();
        }

        private static void LockForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_isClosingLockForm)
                return;

            _lockForm = null;

            // Re-evaluate the gate after the restriction form closes.
            // This restores the previously enabled business forms only when
            // trusted time is verified and no check is still in progress.
            DispatchToMainForm(ApplyCurrentState);
        }

        private static void RestoreOpenForms()
        {
            if (!_formsDisabled || _previousStates == null)
                return;

            foreach (KeyValuePair<Form, bool> item in _previousStates)
            {
                Form form = item.Key;

                if (form == null || form.IsDisposed)
                    continue;

                form.Enabled = item.Value;
            }

            _previousStates = null;
            _formsDisabled = false;
        }

        private static void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            CancelDisplayDelay();

            TrustedTimeAccessManager.Instance.AccessStateChanged -=
                Gate_AccessStateChanged;

            if (_lockForm != null && !_lockForm.IsDisposed)
            {
                _isClosingLockForm = true;
                _lockForm.CloseAfterVerification();
                _lockForm = null;
                _isClosingLockForm = false;
            }

            lock (SyncRoot)
            {
                _mainForm = null;
                _attached = false;
            }
        }
    }
}
