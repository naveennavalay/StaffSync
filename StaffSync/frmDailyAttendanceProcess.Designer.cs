namespace StaffSync
{
    partial class frmDailyAttendanceProcess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDailyAttendanceProcess));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblNote = new System.Windows.Forms.Label();
            this.dtgDailyAttendanceProcess = new Krypton.Toolkit.KryptonDataGridView();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lblBatchProcessID = new System.Windows.Forms.Label();
            this.chkCompactDetailedView = new Krypton.Toolkit.KryptonCheckButton();
            this.lblLeaveMasID = new System.Windows.Forms.Label();
            this.txtDailyAttendanceDate = new Krypton.Toolkit.KryptonDateTimePicker();
            this.label29 = new System.Windows.Forms.Label();
            this.dtgDailyAttendanceUnprocessed = new Krypton.Toolkit.KryptonDataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.btnSaveDetails = new Krypton.Toolkit.KryptonButton();
            this.btnModifyDetails = new Krypton.Toolkit.KryptonButton();
            this.btnCloseMe = new Krypton.Toolkit.KryptonButton();
            this.qryMnthlyAttdInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.staffsyncDBDataSet2 = new StaffSync.StaffsyncDBDataSet2();
            this.errValidator = new System.Windows.Forms.ErrorProvider(this.components);
            this.qryMnthlyAttdInfoTableAdapter = new StaffSync.StaffsyncDBDataSet2TableAdapters.qryMnthlyAttdInfoTableAdapter();
            this.qryDepartmentListTableAdapter1 = new StaffSync.dsDepartmentListTableAdapters.qryDepartmentListTableAdapter();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDailyAttendanceProcess)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDailyAttendanceUnprocessed)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qryMnthlyAttdInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDataSet2)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(1177, 615);
            this.splitContainer1.SplitterDistance = 554;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblNote);
            this.panel1.Controls.Add(this.dtgDailyAttendanceProcess);
            this.panel1.Controls.Add(this.groupBox8);
            this.panel1.Controls.Add(this.dtgDailyAttendanceUnprocessed);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1177, 554);
            this.panel1.TabIndex = 1;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNote.Location = new System.Drawing.Point(13, 525);
            this.lblNote.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(41, 15);
            this.lblNote.TabIndex = 60;
            this.lblNote.Text = "Note:";
            this.lblNote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtgDailyAttendanceProcess
            // 
            this.dtgDailyAttendanceProcess.AllowUserToAddRows = false;
            this.dtgDailyAttendanceProcess.AllowUserToDeleteRows = false;
            this.dtgDailyAttendanceProcess.AllowUserToResizeRows = false;
            this.dtgDailyAttendanceProcess.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgDailyAttendanceProcess.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgDailyAttendanceProcess.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dtgDailyAttendanceProcess.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dtgDailyAttendanceProcess.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.ContextMenuItemImage;
            this.dtgDailyAttendanceProcess.Location = new System.Drawing.Point(16, 80);
            this.dtgDailyAttendanceProcess.Name = "dtgDailyAttendanceProcess";
            this.dtgDailyAttendanceProcess.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtgDailyAttendanceProcess.ReadOnly = true;
            this.dtgDailyAttendanceProcess.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgDailyAttendanceProcess.Size = new System.Drawing.Size(1142, 442);
            this.dtgDailyAttendanceProcess.TabIndex = 59;
            this.dtgDailyAttendanceProcess.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgDailyAttendanceProcess_CellDoubleClick);
            // 
            // groupBox8
            // 
            this.groupBox8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupBox8.Controls.Add(this.lblBatchProcessID);
            this.groupBox8.Controls.Add(this.chkCompactDetailedView);
            this.groupBox8.Controls.Add(this.lblLeaveMasID);
            this.groupBox8.Controls.Add(this.txtDailyAttendanceDate);
            this.groupBox8.Controls.Add(this.label29);
            this.groupBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox8.Location = new System.Drawing.Point(16, 4);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox8.Size = new System.Drawing.Size(1142, 69);
            this.groupBox8.TabIndex = 32;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Batch Daily Attendence Process";
            // 
            // lblBatchProcessID
            // 
            this.lblBatchProcessID.AutoSize = true;
            this.lblBatchProcessID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblBatchProcessID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatchProcessID.Location = new System.Drawing.Point(381, 37);
            this.lblBatchProcessID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBatchProcessID.Name = "lblBatchProcessID";
            this.lblBatchProcessID.Size = new System.Drawing.Size(11, 15);
            this.lblBatchProcessID.TabIndex = 67;
            this.lblBatchProcessID.Text = " ";
            this.lblBatchProcessID.Visible = false;
            // 
            // chkCompactDetailedView
            // 
            this.chkCompactDetailedView.Location = new System.Drawing.Point(969, 33);
            this.chkCompactDetailedView.Name = "chkCompactDetailedView";
            this.chkCompactDetailedView.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.chkCompactDetailedView.Size = new System.Drawing.Size(166, 22);
            this.chkCompactDetailedView.TabIndex = 66;
            this.chkCompactDetailedView.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.chkCompactDetailedView.Values.Text = "Detailed View";
            this.chkCompactDetailedView.Click += new System.EventHandler(this.chkCompactDetailedView_Click);
            // 
            // lblLeaveMasID
            // 
            this.lblLeaveMasID.AutoSize = true;
            this.lblLeaveMasID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblLeaveMasID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeaveMasID.Location = new System.Drawing.Point(566, 37);
            this.lblLeaveMasID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLeaveMasID.Name = "lblLeaveMasID";
            this.lblLeaveMasID.Size = new System.Drawing.Size(11, 15);
            this.lblLeaveMasID.TabIndex = 51;
            this.lblLeaveMasID.Text = " ";
            this.lblLeaveMasID.Visible = false;
            // 
            // txtDailyAttendanceDate
            // 
            this.txtDailyAttendanceDate.CalendarCloseOnTodayClick = true;
            this.txtDailyAttendanceDate.CalendarTodayDate = new System.DateTime(2026, 1, 24, 0, 0, 0, 0);
            this.txtDailyAttendanceDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.txtDailyAttendanceDate.Location = new System.Drawing.Point(190, 34);
            this.txtDailyAttendanceDate.MaxDate = new System.DateTime(3000, 11, 12, 0, 0, 0, 0);
            this.txtDailyAttendanceDate.MinDate = new System.DateTime(2001, 1, 1, 0, 0, 0, 0);
            this.txtDailyAttendanceDate.Name = "txtDailyAttendanceDate";
            this.txtDailyAttendanceDate.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtDailyAttendanceDate.ShowCheckBox = true;
            this.txtDailyAttendanceDate.Size = new System.Drawing.Size(148, 21);
            this.txtDailyAttendanceDate.TabIndex = 47;
            this.txtDailyAttendanceDate.ValueNullable = new System.DateTime(((long)(0)));
            this.txtDailyAttendanceDate.ValueChanged += new System.EventHandler(this.txtDailyAttendanceDate_ValueChanged);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(16, 37);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(167, 15);
            this.label29.TabIndex = 9;
            this.label29.Text = "Attendence Process Date";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtgDailyAttendanceUnprocessed
            // 
            this.dtgDailyAttendanceUnprocessed.AllowUserToAddRows = false;
            this.dtgDailyAttendanceUnprocessed.AllowUserToDeleteRows = false;
            this.dtgDailyAttendanceUnprocessed.AllowUserToResizeRows = false;
            this.dtgDailyAttendanceUnprocessed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgDailyAttendanceUnprocessed.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgDailyAttendanceUnprocessed.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dtgDailyAttendanceUnprocessed.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dtgDailyAttendanceUnprocessed.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.ContextMenuItemImage;
            this.dtgDailyAttendanceUnprocessed.Location = new System.Drawing.Point(296, 354);
            this.dtgDailyAttendanceUnprocessed.Name = "dtgDailyAttendanceUnprocessed";
            this.dtgDailyAttendanceUnprocessed.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtgDailyAttendanceUnprocessed.ReadOnly = true;
            this.dtgDailyAttendanceUnprocessed.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgDailyAttendanceUnprocessed.Size = new System.Drawing.Size(878, 144);
            this.dtgDailyAttendanceUnprocessed.TabIndex = 61;
            this.dtgDailyAttendanceUnprocessed.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnSaveDetails);
            this.panel2.Controls.Add(this.btnModifyDetails);
            this.panel2.Controls.Add(this.btnCloseMe);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1177, 56);
            this.panel2.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(898, 8);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCancel.Size = new System.Drawing.Size(126, 38);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Image = global::StaffSync.Properties.Resources.cancel;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSaveDetails
            // 
            this.btnSaveDetails.Location = new System.Drawing.Point(35, 8);
            this.btnSaveDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveDetails.Name = "btnSaveDetails";
            this.btnSaveDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSaveDetails.Size = new System.Drawing.Size(126, 38);
            this.btnSaveDetails.TabIndex = 12;
            this.btnSaveDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSaveDetails.Values.Image = global::StaffSync.Properties.Resources.execute;
            this.btnSaveDetails.Values.Text = "Execute";
            this.btnSaveDetails.Click += new System.EventHandler(this.btnSaveDetails_Click);
            // 
            // btnModifyDetails
            // 
            this.btnModifyDetails.Location = new System.Drawing.Point(361, 8);
            this.btnModifyDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnModifyDetails.Name = "btnModifyDetails";
            this.btnModifyDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnModifyDetails.Size = new System.Drawing.Size(126, 38);
            this.btnModifyDetails.TabIndex = 11;
            this.btnModifyDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnModifyDetails.Values.Image = global::StaffSync.Properties.Resources.update;
            this.btnModifyDetails.Values.Text = "Modify";
            this.btnModifyDetails.Visible = false;
            this.btnModifyDetails.Click += new System.EventHandler(this.btnModifyDetails_Click);
            // 
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(1032, 8);
            this.btnCloseMe.Margin = new System.Windows.Forms.Padding(4);
            this.btnCloseMe.Name = "btnCloseMe";
            this.btnCloseMe.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCloseMe.Size = new System.Drawing.Size(126, 38);
            this.btnCloseMe.TabIndex = 10;
            this.btnCloseMe.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCloseMe.Values.Image = global::StaffSync.Properties.Resources.close;
            this.btnCloseMe.Values.Text = "Close Me";
            this.btnCloseMe.Click += new System.EventHandler(this.btnCloseMe_Click);
            // 
            // qryMnthlyAttdInfoBindingSource
            // 
            this.qryMnthlyAttdInfoBindingSource.DataMember = "qryMnthlyAttdInfo";
            this.qryMnthlyAttdInfoBindingSource.DataSource = this.staffsyncDBDataSet2;
            // 
            // staffsyncDBDataSet2
            // 
            this.staffsyncDBDataSet2.DataSetName = "StaffsyncDBDataSet2";
            this.staffsyncDBDataSet2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // errValidator
            // 
            this.errValidator.ContainerControl = this;
            // 
            // qryMnthlyAttdInfoTableAdapter
            // 
            this.qryMnthlyAttdInfoTableAdapter.ClearBeforeFill = true;
            // 
            // qryDepartmentListTableAdapter1
            // 
            this.qryDepartmentListTableAdapter1.ClearBeforeFill = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(875, 525);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(283, 15);
            this.label1.TabIndex = 62;
            this.label1.Text = "* Double Click to view Attendance Calender";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmDailyAttendanceProcess
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1177, 615);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDailyAttendanceProcess";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Attendance Details";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmDailyAttendanceProcess_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDailyAttendanceProcess_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDailyAttendanceProcess)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDailyAttendanceUnprocessed)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.qryMnthlyAttdInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDataSet2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ErrorProvider errValidator;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private Krypton.Toolkit.KryptonButton btnSaveDetails;
        private Krypton.Toolkit.KryptonButton btnModifyDetails;
        private Krypton.Toolkit.KryptonButton btnCloseMe;
        private Krypton.Toolkit.KryptonDataGridView dtgDailyAttendanceProcess;
        private StaffsyncDBDataSet2 staffsyncDBDataSet2;
        private System.Windows.Forms.BindingSource qryMnthlyAttdInfoBindingSource;
        private StaffsyncDBDataSet2TableAdapters.qryMnthlyAttdInfoTableAdapter qryMnthlyAttdInfoTableAdapter;
        private dsDepartmentListTableAdapters.qryDepartmentListTableAdapter qryDepartmentListTableAdapter1;
        private System.Windows.Forms.Label label29;
        private Krypton.Toolkit.KryptonDateTimePicker txtDailyAttendanceDate;
        public System.Windows.Forms.Label lblLeaveMasID;
        private System.Windows.Forms.Label lblNote;
        private Krypton.Toolkit.KryptonCheckButton chkCompactDetailedView;
        public System.Windows.Forms.Label lblBatchProcessID;
        private Krypton.Toolkit.KryptonDataGridView dtgDailyAttendanceUnprocessed;
        private System.Windows.Forms.Label label1;
        //private Calender empAttCalender;
    }
}