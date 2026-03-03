using DocumentFormat.OpenXml.Drawing;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace YourNamespace
{
    #region ===== Custom EventArgs =====

    public class BranchTileEventArgs : EventArgs
    {
        public int BranchID { get; }
        public int StateID { get; }

        public BranchTileEventArgs(int branchID, int stateID)
        {
            BranchID = branchID;
            StateID = stateID;
        }
    }

    #endregion

    [DefaultEvent("ViewSlabClicked")]
    public partial class BranchTilesControl : UserControl
    {
        private Label lblBranchName;
        private Label lblCode;
        private Label lblLocation;
        private Label lblCountry;
        private LinkLabel lnkViewSlab;
        private Label lblEmployeesText;
        private Label lblBadge;
        private Panel bottomPanel;

        private Color _originalBackColor;

        Font normalFont = new Font("Segoe UI", 9F, FontStyle.Regular);

        private int _branchID;
        private int _stateID;

        private PictureBox picStatus;
        private ToolTip toolTip1;
        private bool _isConfigured;

        #region ===== Event =====

        public event EventHandler<BranchTileEventArgs> ViewSlabClicked;

        #endregion

        public BranchTilesControl()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Size = new Size(280, 170);
            this.Dock = DockStyle.None;
            this.Padding = new Padding(15);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;

            // ===== Status Icon =====
            toolTip1 = new ToolTip();

            picStatus = new PictureBox
            {
                Size = new Size(16, 16),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            //picStatus.Location = new System.Drawing.Point(this.Width - 22, 6);

            //this.Controls.Add(picStatus);

            //this.Resize += (s, e) =>
            //{
            //    picStatus.Location = new System.Drawing.Point(this.Width - 22, 6);
            //};

            _originalBackColor = this.BackColor;

            // ===== Branch Name =====
            lblBranchName = new Label
            {
                Text = "Branch Name",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 25
            };

            // ===== Code =====
            lblCode = new Label
            {
                Text = "Code: BRN-0001",
                Dock = DockStyle.Top,
                AutoSize = true,
                MaximumSize = new Size(this.Width - 30, 0)
            };

            // ===== Location =====
            lblLocation = new Label
            {
                Text = "City, State",
                Dock = DockStyle.Top,
                AutoSize = true,
                MaximumSize = new Size(this.Width - 30, 0)
            };

            // ===== Country =====
            lblCountry = new Label
            {
                Text = "🌍 India",
                Dock = DockStyle.Top,
                AutoSize = true,
            };

            // ===== View Slab =====
            lnkViewSlab = new LinkLabel
            {
                Text = "View Slab",
                Dock = DockStyle.Top,
                Height = 22
            };

            lnkViewSlab.Click += (s, e) =>
            {
                ViewSlabClicked?.Invoke(this, new BranchTileEventArgs(BranchID, StateID));
            };

            // ===== Bottom Panel =====
            bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 40
            };

            lblBadge = new Label
            {
                Text = "0",
                BackColor = Color.MediumBlue,
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Size = new Size(32, 32),
                Location = new System.Drawing.Point(0, 2)
            };

            MakeBadgeRound();

            lblEmployeesText = new Label
            {
                Text = "No. Of Employees",
                AutoSize = true,
                Location = new System.Drawing.Point(42, 10)
            };

            // View Slab moved here
            lnkViewSlab = new LinkLabel
            {
                Text = "View Slab",
                AutoSize = true
            };

            lnkViewSlab.Location = new System.Drawing.Point(bottomPanel.Width - 70, 10);

            lnkViewSlab.Anchor = AnchorStyles.Right;

            lnkViewSlab.Click += (s, e) =>
            {
                ViewSlabClicked?.Invoke(this, new BranchTileEventArgs(BranchID, StateID));
            };

            // Status Icon
            picStatus = new PictureBox
            {
                Size = new Size(16, 16),
                SizeMode = PictureBoxSizeMode.StretchImage
            };

            bottomPanel.Resize += (s, e) =>
            {
                lnkViewSlab.Left = bottomPanel.Width - lnkViewSlab.Width - 25;
                picStatus.Left = lnkViewSlab.Right + 5;
                picStatus.Top = lnkViewSlab.Top + 2;
            };

            lblCode.Font = normalFont;
            lblLocation.Font = normalFont;
            lblCountry.Font = normalFont;
            lblEmployeesText.Font = normalFont;
            lnkViewSlab.Font = normalFont;
            lblBranchName.Font = new Font("Segoe UI", 11F, FontStyle.Bold);

            Panel divider = new Panel
            {
                Height = 8,
                Dock = DockStyle.Top,
                BackColor = Color.Transparent
            };
            bottomPanel.Margin = new Padding(0, 12, 0, 0);
            bottomPanel.Controls.Add(divider);
            bottomPanel.Controls.Add(lblBadge);
            bottomPanel.Controls.Add(lblEmployeesText);
            bottomPanel.Controls.Add(lnkViewSlab);
            bottomPanel.Controls.Add(picStatus);

            
            // ===== Add Controls =====
            this.Controls.Add(bottomPanel);
            this.Controls.Add(lblCountry);
            this.Controls.Add(lblLocation);
            this.Controls.Add(lblCode);
            this.Controls.Add(lblBranchName);

            AddHoverEffect();
        }

        #region ===== Hover Effect =====

        private void AddHoverEffect()
        {
            this.MouseEnter += (s, e) =>
            {
                if (this.BackColor != Color.Transparent)
                    this.BackColor = Color.FromArgb(235, 235, 235);
            };

            this.MouseLeave += (s, e) =>
            {
                this.BackColor = _originalBackColor;
            };

            foreach (Control ctl in this.Controls)
            {
                ctl.MouseEnter += (s, e) =>
                {
                    if (this.BackColor != Color.Transparent)
                        this.BackColor = Color.FromArgb(235, 235, 235);
                };

                ctl.MouseLeave += (s, e) =>
                {
                    this.BackColor = _originalBackColor;
                };
            }
        }

        #endregion

        #region ===== Data Properties =====

        [Category("Branch Data")]
        public bool IsConfigured
        {
            get => _isConfigured;
            set
            {
                _isConfigured = value;
                UpdateStatusIcon();
            }
        }

        [Category("Branch Data")]
        public int BranchID
        {
            get => _branchID;
            set => _branchID = value;
        }

        [Category("Branch Data")]
        public int StateID
        {
            get => _stateID;
            set => _stateID = value;
        }

        [Category("Branch Data")]
        public string BranchName
        {
            get => lblBranchName.Text;
            set => lblBranchName.Text = value;
        }

        [Category("Branch Data")]
        public string BranchCode
        {
            get => lblCode.Text.Replace("Code: ", "");
            set => lblCode.Text = "Code: " + value;
        }

        [Category("Branch Data")]
        public string CityState
        {
            get => lblLocation.Text;
            set => lblLocation.Text = value;
        }

        [Category("Branch Data")]
        public string Country
        {
            get => lblCountry.Text.Replace("🌍 ", "");
            set => lblCountry.Text = "🌍 " + value;
        }

        [Category("Branch Data")]
        public string BadgeText
        {
            get => lblBadge.Text;
            set
            {
                lblBadge.Text = value;
                AdjustBadgeFont();
            }
        }

        #endregion

        #region ===== Appearance Properties =====

        [Category("Appearance")]
        public Color TileBackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                _originalBackColor = value;
            }
        }

        [Category("Appearance")]
        public Color TileForeColor
        {
            get => this.ForeColor;
            set
            {
                this.ForeColor = value;
                foreach (Control ctl in this.Controls)
                    ctl.ForeColor = value;
            }
        }

        [Category("Appearance")]
        public Font BranchNameFont
        {
            get => lblBranchName.Font;
            set => lblBranchName.Font = value;
        }

        [Category("Appearance")]
        public Font NormalTextFont
        {
            get => lblCode.Font;
            set
            {
                lblCode.Font = value;
                lblLocation.Font = value;
                lblCountry.Font = value;
                lblEmployeesText.Font = value;
            }
        }

        #endregion

        #region ===== Visibility Properties =====

        [Category("Visibility")]
        public bool ShowBranchName
        {
            get => lblBranchName.Visible;
            set => lblBranchName.Visible = value;
        }

        [Category("Visibility")]
        public bool ShowBranchCode
        {
            get => lblCode.Visible;
            set => lblCode.Visible = value;
        }

        [Category("Visibility")]
        public bool ShowLocation
        {
            get => lblLocation.Visible;
            set => lblLocation.Visible = value;
        }

        [Category("Visibility")]
        public bool ShowCountry
        {
            get => lblCountry.Visible;
            set => lblCountry.Visible = value;
        }

        [Category("Visibility")]
        public bool ShowViewSlab
        {
            get => lnkViewSlab.Visible;
            set => lnkViewSlab.Visible = value;
        }

        [Category("Visibility")]
        public bool ShowEmployeeBadge
        {
            get => bottomPanel.Visible;
            set => bottomPanel.Visible = value;
        }

        #endregion

        #region ===== Helper =====

        public string GetState()
        {
            if (lblLocation.Text.Contains(","))
                return lblLocation.Text.Split(',')[1].Trim();

            return lblLocation.Text;
        }

        #endregion

        private void MakeBadgeRound()
        {
            lblBadge.Resize += (s, e) =>
            {
                System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
                gp.AddEllipse(0, 0, lblBadge.Width - 1, lblBadge.Height - 1);
                lblBadge.Region = new Region(gp);
            };
        }

        private void AdjustBadgeFont()
        {
            int length = lblBadge.Text.Length;

            if (length <= 2)
                lblBadge.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            else if (length <= 4)
                lblBadge.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            else
                lblBadge.Font = new Font("Segoe UI", 7F, FontStyle.Bold);
        }

        private void UpdateStatusIcon()
        {
            if (_isConfigured)
            {
                picStatus.Image = SystemIcons.Shield.ToBitmap(); // Or your green icon
                toolTip1.SetToolTip(picStatus, "Slab Configured");
            }
            else
            {
                picStatus.Image = SystemIcons.Warning.ToBitmap(); // Or custom red icon
                toolTip1.SetToolTip(picStatus, "Slab Not Configured");
            }
        }
    }
}