using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common.Helpers;

namespace StaffSync
{
    /// <summary>
    /// Trusted-time restriction window.
    /// The explanation and technical details are displayed in a
    /// vertically scrollable, read-only (locked) text box.
    /// </summary>
    public sealed class TrustedTimeLockForm : Form
    {
        private readonly Label _titleLabel;
        private readonly Label _statusLabel;
        private readonly TextBox _messageTextBox;
        private readonly Button _retryButton;
        private readonly Button _exitButton;

        private bool _allowProgrammaticClose;
        private Timer _successCloseTimer;

        public TrustedTimeLockForm()
        {
            Text = "StaffSync - Connection Verification";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ControlBox = false;
            ShowInTaskbar = true;
            TopMost = true;
            ClientSize = new Size(700, 500);
            MinimumSize = new Size(650, 440);
            Font = new Font("Segoe UI", 9F);
            BackColor = Color.White;

            _titleLabel = new Label
            {
                AutoSize = false,
                Text = "StaffSync – Connection Verification Required",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.FromArgb(31, 78, 121),
                Location = new Point(28, 22),
                Size = new Size(640, 38),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            _statusLabel = new Label
            {
                AutoSize = false,
                Text = "● OFFLINE / TRUSTED TIME UNAVAILABLE",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.Firebrick,
                Location = new Point(30, 72),
                Size = new Size(635, 28),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            // Read-only textbox: users can scroll and copy the message,
            // but cannot edit or remove any of the displayed information.
            _messageTextBox = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                WordWrap = true,
                ScrollBars = ScrollBars.Vertical,
                ShortcutsEnabled = true,
                TabStop = true,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(249, 251, 253),
                ForeColor = Color.FromArgb(45, 45, 45),
                Font = new Font("Segoe UI", 9.5F),
                Location = new Point(30, 112),
                Size = new Size(638, 310),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                         AnchorStyles.Left | AnchorStyles.Right
            };

            _retryButton = new Button
            {
                Text = "Retry Synchronization",
                Location = new Point(390, 448),
                Size = new Size(165, 32),
                Font = new Font("Segoe UI", 9F),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            _retryButton.Click += RetryButton_Click;

            _exitButton = new Button
            {
                Text = "Exit StaffSync",
                Location = new Point(564, 448),
                Size = new Size(104, 32),
                Font = new Font("Segoe UI", 9F),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            _exitButton.Click += ExitButton_Click;

            Controls.AddRange(new Control[]
            {
                _titleLabel,
                _statusLabel,
                _messageTextBox,
                _retryButton,
                _exitButton
            });

            UpdateStatus();
        }

        public void UpdateStatus()
        {
            if (IsDisposed)
                return;

            TrustedTimeAccessManager manager =
                TrustedTimeAccessManager.Instance;

            if (manager.IsChecking)
            {
                SetCheckingStatus();
            }
            else if (manager.IsLocked)
            {
                SetOfflineStatus();
            }
            else
            {
                ShowOnlineStatusThenClose();
            }
        }

        private void SetOfflineStatus()
        {
            _titleLabel.Text = "StaffSync – Connection Verification Required";
            _statusLabel.Text = "● OFFLINE / TRUSTED TIME UNAVAILABLE";
            _statusLabel.ForeColor = Color.Firebrick;

            string failureReason =
                TrustedTimeAccessManager.Instance.FailureReason;

            StringBuilder message = new StringBuilder();
            message.AppendLine("StaffSync is temporarily unable to connect to the trusted time service.");
            message.AppendLine();
            message.AppendLine("Please check your internet connection and ensure that your device is connected to the internet.");
            message.AppendLine();
            message.AppendLine("If your connection was interrupted, kindly restore it and select \"Retry Synchronization\".");
            message.AppendLine();
            message.AppendLine("Once the connection is restored and trusted time is verified, you can continue using StaffSync as usual.");
            message.AppendLine();
            message.AppendLine("ADDITIONAL INFORMATION");
            message.AppendLine("----------------------");
            message.AppendLine(string.IsNullOrWhiteSpace(failureReason)
                ? "Trusted date and time could not be verified."
                : failureReason);

            SetMessageText(message.ToString());

            _retryButton.Enabled = true;
            _retryButton.Text = "Retry Synchronization";
        }

        private void SetCheckingStatus()
        {
            _titleLabel.Text = "StaffSync – Verifying Connection";
            _statusLabel.Text = "● CHECKING CONNECTION";
            _statusLabel.ForeColor = Color.DarkOrange;

            SetMessageText(
                "StaffSync is checking the internet connection and contacting the trusted time providers." +
                Environment.NewLine + Environment.NewLine +
                "Please wait while the trusted date and time verification completes." +
                Environment.NewLine + Environment.NewLine +
                "StaffSync will remain restricted until the required trusted time sources have been verified.");

            _retryButton.Enabled = false;
            _retryButton.Text = "Verifying...";
        }

        /// <summary>
        /// Shows the successful online state briefly before closing the
        /// restriction window and allowing the coordinator to restore access.
        /// </summary>
        public void ShowOnlineStatusThenClose()
        {
            if (IsDisposed || _allowProgrammaticClose)
                return;

            _titleLabel.Text = "StaffSync – Connection Restored";
            _statusLabel.Text = "● ONLINE – TRUSTED TIME VERIFIED";
            _statusLabel.ForeColor = Color.ForestGreen;

            SetMessageText(
                "Your connection has been restored and trusted date and time has been successfully verified." +
                Environment.NewLine + Environment.NewLine +
                "The required trusted time sources are now in agreement." +
                Environment.NewLine + Environment.NewLine +
                "You can now continue using StaffSync.");

            _retryButton.Enabled = false;
            _retryButton.Text = "Verified";

            if (_successCloseTimer == null)
            {
                _successCloseTimer = new Timer();
                _successCloseTimer.Interval = 1200;
                _successCloseTimer.Tick += (s, e) =>
                {
                    _successCloseTimer.Stop();
                    CloseAfterVerification();
                };
                _successCloseTimer.Start();
            }
        }

        private void SetMessageText(string value)
        {
            _messageTextBox.Text = value ?? string.Empty;
            _messageTextBox.SelectionStart = 0;
            _messageTextBox.SelectionLength = 0;
            _messageTextBox.ScrollToCaret();
        }

        private async void RetryButton_Click(object sender, EventArgs e)
        {
            SetCheckingStatus();

            bool success =
                await TrustedTimeAccessManager.Instance.RetryTrustedTimeAsync();

            if (IsDisposed)
                return;

            if (!success)
                UpdateStatus();
            // On success, AccessStateChanged is raised by the manager.
            // The coordinator then displays the ONLINE state and closes.
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void CloseAfterVerification()
        {
            _allowProgrammaticClose = true;

            if (_successCloseTimer != null)
            {
                _successCloseTimer.Stop();
                _successCloseTimer.Dispose();
                _successCloseTimer = null;
            }

            Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing &&
                !_allowProgrammaticClose)
            {
                e.Cancel = true;
                return;
            }

            if (_successCloseTimer != null)
            {
                _successCloseTimer.Stop();
                _successCloseTimer.Dispose();
                _successCloseTimer = null;
            }

            base.OnFormClosing(e);
        }
    }
}
