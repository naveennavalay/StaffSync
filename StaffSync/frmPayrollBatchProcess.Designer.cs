namespace StaffSync
{
    partial class frmPayrollBatchProcess
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPayrollBatchProcess));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.picDownloadDataAsCSV = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.picRefreshBankList = new System.Windows.Forms.PictureBox();
            this.dtSalaryDate = new Krypton.Toolkit.KryptonDateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label29 = new System.Windows.Forms.Label();
            this.cmbSalaryMonth = new Krypton.Toolkit.KryptonComboBox();
            this.label42 = new System.Windows.Forms.Label();
            this.dtgSalaryDetails = new Krypton.Toolkit.KryptonDataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.btnCloseMe = new Krypton.Toolkit.KryptonButton();
            this.btnRemoveDetails = new Krypton.Toolkit.KryptonButton();
            this.btnSaveDetails = new Krypton.Toolkit.KryptonButton();
            this.btnModifyDetails = new Krypton.Toolkit.KryptonButton();
            this.btnGenerateDetails = new Krypton.Toolkit.KryptonButton();
            this.errValidator = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDownloadDataAsCSV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRefreshBankList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSalaryMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSalaryDetails)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(1063, 641);
            this.splitContainer1.SplitterDistance = 578;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1063, 578);
            this.panel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupBox1.Controls.Add(this.picDownloadDataAsCSV);
            this.groupBox1.Controls.Add(this.pictureBox4);
            this.groupBox1.Controls.Add(this.picRefreshBankList);
            this.groupBox1.Controls.Add(this.dtSalaryDate);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.cmbSalaryMonth);
            this.groupBox1.Controls.Add(this.label42);
            this.groupBox1.Controls.Add(this.dtgSalaryDetails);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(16, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1032, 557);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            // 
            // picDownloadDataAsCSV
            // 
            this.picDownloadDataAsCSV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picDownloadDataAsCSV.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picDownloadDataAsCSV.Image = global::StaffSync.Properties.Resources.download01;
            this.picDownloadDataAsCSV.Location = new System.Drawing.Point(942, 69);
            this.picDownloadDataAsCSV.Name = "picDownloadDataAsCSV";
            this.picDownloadDataAsCSV.Size = new System.Drawing.Size(21, 20);
            this.picDownloadDataAsCSV.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDownloadDataAsCSV.TabIndex = 61;
            this.picDownloadDataAsCSV.TabStop = false;
            this.picDownloadDataAsCSV.Click += new System.EventHandler(this.picDownloadDataAsCSV_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox4.Image = global::StaffSync.Properties.Resources.mail01;
            this.pictureBox4.Location = new System.Drawing.Point(965, 68);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(23, 22);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 60;
            this.pictureBox4.TabStop = false;
            // 
            // picRefreshBankList
            // 
            this.picRefreshBankList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picRefreshBankList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picRefreshBankList.Image = global::StaffSync.Properties.Resources.refresh01;
            this.picRefreshBankList.Location = new System.Drawing.Point(990, 68);
            this.picRefreshBankList.Name = "picRefreshBankList";
            this.picRefreshBankList.Size = new System.Drawing.Size(23, 22);
            this.picRefreshBankList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRefreshBankList.TabIndex = 59;
            this.picRefreshBankList.TabStop = false;
            // 
            // dtSalaryDate
            // 
            this.dtSalaryDate.CalendarTodayDate = new System.DateTime(2025, 11, 22, 0, 0, 0, 0);
            this.dtSalaryDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtSalaryDate.Location = new System.Drawing.Point(833, 22);
            this.dtSalaryDate.Name = "dtSalaryDate";
            this.dtSalaryDate.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtSalaryDate.Size = new System.Drawing.Size(180, 21);
            this.dtSalaryDate.TabIndex = 58;
            this.dtSalaryDate.ValueChanged += new System.EventHandler(this.dtSalaryDate_ValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(0, 51);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1312, 10);
            this.groupBox2.TabIndex = 57;
            this.groupBox2.TabStop = false;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(744, 25);
            this.label29.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(81, 15);
            this.label29.TabIndex = 48;
            this.label29.Text = "Salary Date";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbSalaryMonth
            // 
            this.cmbSalaryMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSalaryMonth.DropDownWidth = 440;
            this.cmbSalaryMonth.Location = new System.Drawing.Point(112, 21);
            this.cmbSalaryMonth.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSalaryMonth.Name = "cmbSalaryMonth";
            this.cmbSalaryMonth.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbSalaryMonth.Size = new System.Drawing.Size(224, 22);
            this.cmbSalaryMonth.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbSalaryMonth.TabIndex = 46;
            this.cmbSalaryMonth.SelectedIndexChanged += new System.EventHandler(this.cmbSalaryMonth_SelectedIndexChanged);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(12, 25);
            this.label42.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(91, 15);
            this.label42.TabIndex = 45;
            this.label42.Text = "Salary Month";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtgSalaryDetails
            // 
            this.dtgSalaryDetails.AllowUserToAddRows = false;
            this.dtgSalaryDetails.AllowUserToDeleteRows = false;
            this.dtgSalaryDetails.AllowUserToResizeRows = false;
            this.dtgSalaryDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgSalaryDetails.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dtgSalaryDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgSalaryDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dtgSalaryDetails.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dtgSalaryDetails.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.ButtonLowProfile;
            this.dtgSalaryDetails.Location = new System.Drawing.Point(15, 97);
            this.dtgSalaryDetails.Margin = new System.Windows.Forms.Padding(4);
            this.dtgSalaryDetails.MultiSelect = false;
            this.dtgSalaryDetails.Name = "dtgSalaryDetails";
            this.dtgSalaryDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtgSalaryDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgSalaryDetails.Size = new System.Drawing.Size(998, 442);
            this.dtgSalaryDetails.TabIndex = 44;
            this.dtgSalaryDetails.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dtgSalaryDetails_CellBeginEdit);
            this.dtgSalaryDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgSalaryDetails_CellEndEdit);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnCloseMe);
            this.panel2.Controls.Add(this.btnRemoveDetails);
            this.panel2.Controls.Add(this.btnSaveDetails);
            this.panel2.Controls.Add(this.btnModifyDetails);
            this.panel2.Controls.Add(this.btnGenerateDetails);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1063, 58);
            this.panel2.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(165, 12);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCancel.Size = new System.Drawing.Size(126, 38);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Image = global::StaffSync.Properties.Resources.cancel;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(903, 12);
            this.btnCloseMe.Margin = new System.Windows.Forms.Padding(4);
            this.btnCloseMe.Name = "btnCloseMe";
            this.btnCloseMe.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCloseMe.Size = new System.Drawing.Size(126, 38);
            this.btnCloseMe.TabIndex = 20;
            this.btnCloseMe.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCloseMe.Values.Image = global::StaffSync.Properties.Resources.close;
            this.btnCloseMe.Values.Text = "Close Me";
            this.btnCloseMe.Click += new System.EventHandler(this.btnCloseMe_Click_1);
            // 
            // btnRemoveDetails
            // 
            this.btnRemoveDetails.Location = new System.Drawing.Point(434, 12);
            this.btnRemoveDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemoveDetails.Name = "btnRemoveDetails";
            this.btnRemoveDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnRemoveDetails.Size = new System.Drawing.Size(126, 38);
            this.btnRemoveDetails.TabIndex = 19;
            this.btnRemoveDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnRemoveDetails.Values.Image = global::StaffSync.Properties.Resources.delete;
            this.btnRemoveDetails.Values.Text = "Delete";
            this.btnRemoveDetails.Visible = false;
            this.btnRemoveDetails.Click += new System.EventHandler(this.btnRemoveDetails_Click);
            // 
            // btnSaveDetails
            // 
            this.btnSaveDetails.Location = new System.Drawing.Point(299, 12);
            this.btnSaveDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveDetails.Name = "btnSaveDetails";
            this.btnSaveDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSaveDetails.Size = new System.Drawing.Size(126, 38);
            this.btnSaveDetails.TabIndex = 18;
            this.btnSaveDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSaveDetails.Values.Image = global::StaffSync.Properties.Resources.save;
            this.btnSaveDetails.Values.Text = "Save";
            this.btnSaveDetails.Visible = false;
            this.btnSaveDetails.Click += new System.EventHandler(this.btnSaveDetails_Click);
            // 
            // btnModifyDetails
            // 
            this.btnModifyDetails.Location = new System.Drawing.Point(165, 12);
            this.btnModifyDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnModifyDetails.Name = "btnModifyDetails";
            this.btnModifyDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnModifyDetails.Size = new System.Drawing.Size(126, 38);
            this.btnModifyDetails.TabIndex = 17;
            this.btnModifyDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnModifyDetails.Values.Image = global::StaffSync.Properties.Resources.update;
            this.btnModifyDetails.Values.Text = "Modify";
            this.btnModifyDetails.Visible = false;
            this.btnModifyDetails.Click += new System.EventHandler(this.btnModifyDetails_Click);
            // 
            // btnGenerateDetails
            // 
            this.btnGenerateDetails.Location = new System.Drawing.Point(31, 12);
            this.btnGenerateDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerateDetails.Name = "btnGenerateDetails";
            this.btnGenerateDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnGenerateDetails.Size = new System.Drawing.Size(126, 38);
            this.btnGenerateDetails.TabIndex = 16;
            this.btnGenerateDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnGenerateDetails.Values.Image = global::StaffSync.Properties.Resources.execute;
            this.btnGenerateDetails.Values.Text = "Process";
            this.btnGenerateDetails.Click += new System.EventHandler(this.btnGenerateDetails_Click);
            // 
            // errValidator
            // 
            this.errValidator.ContainerControl = this;
            // 
            // frmPayrollBatchProcess
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1063, 641);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPayrollBatchProcess";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payroll Batch Process";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.frmPayrollBatchProcess_Activated);
            this.Load += new System.EventHandler(this.frmPayrollBatchProcess_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPayrollBatchProcess_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDownloadDataAsCSV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRefreshBankList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSalaryMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSalaryDetails)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Krypton.Toolkit.KryptonButton btnCloseMe;
        private Krypton.Toolkit.KryptonButton btnRemoveDetails;
        private Krypton.Toolkit.KryptonButton btnSaveDetails;
        private Krypton.Toolkit.KryptonButton btnModifyDetails;
        private Krypton.Toolkit.KryptonButton btnGenerateDetails;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private Krypton.Toolkit.KryptonDataGridView dtgSalaryDetails;
        private Krypton.Toolkit.KryptonComboBox cmbSalaryMonth;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ErrorProvider errValidator;
        private Krypton.Toolkit.KryptonDateTimePicker dtSalaryDate;
        private System.Windows.Forms.PictureBox picDownloadDataAsCSV;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox picRefreshBankList;
    }
}