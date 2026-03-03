using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace YourNamespace
{
    [DefaultEvent("ViewSlabClicked")]
    public partial class BranchTileControl : UserControl
    {
        private Label lblBranchName;
        private Label lblCode;
        private Label lblLocation;
        private Label lblCountry;
        private LinkLabel lnkViewSlab;
        private Label lblEmployeesText;
        private Label lblBadge;
        private Color _originalBackColor;

        public event EventHandler ViewSlabClicked;

        public BranchTileControl()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Size = new Size(280, 160);
            this.Dock = DockStyle.None; // NOT Fill
            this.BackColor = Color.Transparent;
            this.TileBackColor = Color.Transparent;
            this.Padding = new Padding(15);
            this.BorderStyle = BorderStyle.FixedSingle;

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
                Font = new Font("Segoe UI", 9F),
                Dock = DockStyle.Top,
                Height = 20
            };

            // ===== Location =====
            lblLocation = new Label
            {
                Text = "City, State",
                Font = new Font("Segoe UI", 9F),
                Dock = DockStyle.Top,
                Height = 20
            };

            // ===== Country =====
            lblCountry = new Label
            {
                Text = "🌍 India",
                Font = new Font("Segoe UI", 9F),
                Dock = DockStyle.Top,
                Height = 20
            };

            // ===== View Slab Link =====
            lnkViewSlab = new LinkLabel
            {
                Text = "View Slab",
                Dock = DockStyle.Top,
                Height = 20
            };
            lnkViewSlab.Click += (s, e) =>
            {
                ViewSlabClicked?.Invoke(this, EventArgs.Empty);
            };

            // ===== Badge =====
            lblBadge = new Label
            {
                Text = "0",
                BackColor = Color.MediumBlue,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(30, 25),
                Location = new Point(15, 120)
            };

            // ===== Employees Text =====
            lblEmployeesText = new Label
            {
                Text = "No. Of Employees",
                Font = new Font("Segoe UI", 9F),
                Location = new Point(55, 122),
                AutoSize = true
            };

            this.Controls.Add(lblEmployeesText);
            this.Controls.Add(lblBadge);
            this.Controls.Add(lnkViewSlab);
            this.Controls.Add(lblCountry);
            this.Controls.Add(lblLocation);
            this.Controls.Add(lblCode);
            this.Controls.Add(lblBranchName);

            AddHoverEffect();
        }

        private void AddHoverEffect()
        {
            _originalBackColor = this.BackColor;

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

        #region ===== DESIGN-TIME PROPERTIES =====

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
            set => lblBadge.Text = value;
        }

        [Category("Appearance")]
        public Color TileBackColor
        {
            get => base.BackColor;
            set => base.BackColor = value;
        }

        #endregion

        #region ===== HELPER =====

        public string GetState()
        {
            if (lblLocation.Text.Contains(","))
                return lblLocation.Text.Split(',')[1].Trim();

            return lblLocation.Text;
        }

        #endregion
    }
}