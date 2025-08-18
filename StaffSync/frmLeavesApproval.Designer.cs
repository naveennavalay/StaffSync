namespace StaffSync
{
    partial class frmLeavesApproval
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLeavesApproval));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstLeaveTRList = new System.Windows.Forms.ListView();
            this.LeaveTRID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LeaveType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LeaveDateFrom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LeaveDateTo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LeaveDuration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LeaveComments = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LeaveMode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.LeaveStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Canceled = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CanceledDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblLeaveMasID = new System.Windows.Forms.Label();
            this.lblSpecificLeaveBalance = new System.Windows.Forms.Label();
            this.lblLeaveTRID = new System.Windows.Forms.Label();
            this.picDownloadLeaveTRList = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.picRefreshLeaveTRList = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtApprovalNote = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtActualLeaveDays = new System.Windows.Forms.TextBox();
            this.txtBalanceLeave = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAvailableLeave = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtLeaveDateTo = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbDuration = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLeaveDateFrom = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbLeaveType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSearch = new Krypton.Toolkit.KryptonButton();
            this.label38 = new System.Windows.Forms.Label();
            this.picEmpPhoto = new System.Windows.Forms.PictureBox();
            this.cmbDesignation = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txtEmployeeName = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.lblEmpID = new System.Windows.Forms.Label();
            this.lblActionMode = new System.Windows.Forms.Label();
            this.txtEmpCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.btnCloseMe = new Krypton.Toolkit.KryptonButton();
            this.btnRemoveDetails = new Krypton.Toolkit.KryptonButton();
            this.btnSaveDetails = new Krypton.Toolkit.KryptonButton();
            this.btnModifyDetails = new Krypton.Toolkit.KryptonButton();
            this.btnGenerateDetails = new Krypton.Toolkit.KryptonButton();
            this.errValidator = new System.Windows.Forms.ErrorProvider(this.components);
            this.staffsyncDBDTSet = new StaffSync.StaffsyncDBDTSet();
            this.qryRoleProfileBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qryRoleProfileTableAdapter = new StaffSync.StaffsyncDBDTSetTableAdapters.qryRoleProfileTableAdapter();
            this.kryptonCommand1 = new Krypton.Toolkit.KryptonCommand();
            this.kryptonCommand2 = new Krypton.Toolkit.KryptonCommand();
            this.picViewLeaves = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDownloadLeaveTRList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRefreshLeaveTRList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEmpPhoto)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryRoleProfileBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picViewLeaves)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
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
            this.splitContainer1.Size = new System.Drawing.Size(1133, 624);
            this.splitContainer1.SplitterDistance = 563;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1133, 563);
            this.panel1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lstLeaveTRList);
            this.groupBox2.Controls.Add(this.lblLeaveMasID);
            this.groupBox2.Controls.Add(this.lblSpecificLeaveBalance);
            this.groupBox2.Controls.Add(this.lblLeaveTRID);
            this.groupBox2.Controls.Add(this.picDownloadLeaveTRList);
            this.groupBox2.Controls.Add(this.pictureBox3);
            this.groupBox2.Controls.Add(this.picRefreshLeaveTRList);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 328);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1112, 225);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Leave History";
            // 
            // lstLeaveTRList
            // 
            this.lstLeaveTRList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLeaveTRList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.lstLeaveTRList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.LeaveTRID,
            this.LeaveType,
            this.LeaveDateFrom,
            this.LeaveDateTo,
            this.LeaveDuration,
            this.LeaveComments,
            this.LeaveMode,
            this.LeaveStatus,
            this.Canceled,
            this.CanceledDate});
            this.lstLeaveTRList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lstLeaveTRList.FullRowSelect = true;
            this.lstLeaveTRList.GridLines = true;
            this.lstLeaveTRList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstLeaveTRList.HideSelection = false;
            this.lstLeaveTRList.LabelWrap = false;
            this.lstLeaveTRList.Location = new System.Drawing.Point(17, 48);
            this.lstLeaveTRList.MultiSelect = false;
            this.lstLeaveTRList.Name = "lstLeaveTRList";
            this.lstLeaveTRList.ShowItemToolTips = true;
            this.lstLeaveTRList.Size = new System.Drawing.Size(1078, 163);
            this.lstLeaveTRList.TabIndex = 26;
            this.lstLeaveTRList.UseCompatibleStateImageBehavior = false;
            this.lstLeaveTRList.View = System.Windows.Forms.View.Details;
            // 
            // LeaveTRID
            // 
            this.LeaveTRID.Text = "LeaveTRID";
            this.LeaveTRID.Width = 0;
            // 
            // LeaveType
            // 
            this.LeaveType.Text = "Leave Type";
            this.LeaveType.Width = 150;
            // 
            // LeaveDateFrom
            // 
            this.LeaveDateFrom.Text = "Leave From";
            this.LeaveDateFrom.Width = 100;
            // 
            // LeaveDateTo
            // 
            this.LeaveDateTo.Text = "Leave To";
            this.LeaveDateTo.Width = 100;
            // 
            // LeaveDuration
            // 
            this.LeaveDuration.Text = "Leave Duration";
            this.LeaveDuration.Width = 100;
            // 
            // LeaveComments
            // 
            this.LeaveComments.Text = "Comments";
            this.LeaveComments.Width = 300;
            // 
            // LeaveMode
            // 
            this.LeaveMode.Text = "Leave Mode";
            this.LeaveMode.Width = 150;
            // 
            // LeaveStatus
            // 
            this.LeaveStatus.Text = "Leave Status";
            this.LeaveStatus.Width = 150;
            // 
            // Canceled
            // 
            this.Canceled.Text = "Cancelled";
            this.Canceled.Width = 100;
            // 
            // CanceledDate
            // 
            this.CanceledDate.Text = "Cancelled Date";
            this.CanceledDate.Width = 125;
            // 
            // lblLeaveMasID
            // 
            this.lblLeaveMasID.AutoSize = true;
            this.lblLeaveMasID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblLeaveMasID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeaveMasID.Location = new System.Drawing.Point(639, 22);
            this.lblLeaveMasID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLeaveMasID.Name = "lblLeaveMasID";
            this.lblLeaveMasID.Size = new System.Drawing.Size(11, 15);
            this.lblLeaveMasID.TabIndex = 25;
            this.lblLeaveMasID.Text = " ";
            // 
            // lblSpecificLeaveBalance
            // 
            this.lblSpecificLeaveBalance.AutoSize = true;
            this.lblSpecificLeaveBalance.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblSpecificLeaveBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSpecificLeaveBalance.Location = new System.Drawing.Point(690, 22);
            this.lblSpecificLeaveBalance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSpecificLeaveBalance.Name = "lblSpecificLeaveBalance";
            this.lblSpecificLeaveBalance.Size = new System.Drawing.Size(11, 15);
            this.lblSpecificLeaveBalance.TabIndex = 24;
            this.lblSpecificLeaveBalance.Text = " ";
            // 
            // lblLeaveTRID
            // 
            this.lblLeaveTRID.AutoSize = true;
            this.lblLeaveTRID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblLeaveTRID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeaveTRID.Location = new System.Drawing.Point(590, 22);
            this.lblLeaveTRID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLeaveTRID.Name = "lblLeaveTRID";
            this.lblLeaveTRID.Size = new System.Drawing.Size(11, 15);
            this.lblLeaveTRID.TabIndex = 23;
            this.lblLeaveTRID.Text = " ";
            // 
            // picDownloadLeaveTRList
            // 
            this.picDownloadLeaveTRList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picDownloadLeaveTRList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picDownloadLeaveTRList.Image = global::StaffSync.Properties.Resources.download01;
            this.picDownloadLeaveTRList.Location = new System.Drawing.Point(1023, 22);
            this.picDownloadLeaveTRList.Name = "picDownloadLeaveTRList";
            this.picDownloadLeaveTRList.Size = new System.Drawing.Size(21, 20);
            this.picDownloadLeaveTRList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDownloadLeaveTRList.TabIndex = 22;
            this.picDownloadLeaveTRList.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox3.Image = global::StaffSync.Properties.Resources.mail01;
            this.pictureBox3.Location = new System.Drawing.Point(1046, 21);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(23, 22);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 21;
            this.pictureBox3.TabStop = false;
            // 
            // picRefreshLeaveTRList
            // 
            this.picRefreshLeaveTRList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picRefreshLeaveTRList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picRefreshLeaveTRList.Image = global::StaffSync.Properties.Resources.refresh01;
            this.picRefreshLeaveTRList.Location = new System.Drawing.Point(1071, 21);
            this.picRefreshLeaveTRList.Name = "picRefreshLeaveTRList";
            this.picRefreshLeaveTRList.Size = new System.Drawing.Size(23, 22);
            this.picRefreshLeaveTRList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRefreshLeaveTRList.TabIndex = 20;
            this.picRefreshLeaveTRList.TabStop = false;
            this.picRefreshLeaveTRList.Click += new System.EventHandler(this.picRefreshLeaveTRList_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.picViewLeaves);
            this.groupBox1.Controls.Add(this.txtApprovalNote);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtActualLeaveDays);
            this.groupBox1.Controls.Add(this.txtBalanceLeave);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtAvailableLeave);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtLeaveDateTo);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbDuration);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtLeaveDateFrom);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbLeaveType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 167);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1112, 155);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Leave Request";
            // 
            // txtApprovalNote
            // 
            this.txtApprovalNote.Location = new System.Drawing.Point(763, 25);
            this.txtApprovalNote.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtApprovalNote.MaxLength = 255;
            this.txtApprovalNote.Multiline = true;
            this.txtApprovalNote.Name = "txtApprovalNote";
            this.txtApprovalNote.Size = new System.Drawing.Size(297, 116);
            this.txtApprovalNote.TabIndex = 47;
            this.txtApprovalNote.Tag = "Please enter Employeee Code";
            this.txtApprovalNote.WordWrap = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(661, 32);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 15);
            this.label8.TabIndex = 48;
            this.label8.Text = "Approval Note";
            // 
            // txtActualLeaveDays
            // 
            this.txtActualLeaveDays.Enabled = false;
            this.txtActualLeaveDays.Location = new System.Drawing.Point(488, 120);
            this.txtActualLeaveDays.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtActualLeaveDays.MaxLength = 255;
            this.txtActualLeaveDays.Multiline = true;
            this.txtActualLeaveDays.Name = "txtActualLeaveDays";
            this.txtActualLeaveDays.ReadOnly = true;
            this.txtActualLeaveDays.Size = new System.Drawing.Size(78, 21);
            this.txtActualLeaveDays.TabIndex = 46;
            this.txtActualLeaveDays.Tag = "Please enter Employeee Code";
            this.txtActualLeaveDays.WordWrap = false;
            // 
            // txtBalanceLeave
            // 
            this.txtBalanceLeave.Location = new System.Drawing.Point(413, 25);
            this.txtBalanceLeave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtBalanceLeave.MaxLength = 255;
            this.txtBalanceLeave.Multiline = true;
            this.txtBalanceLeave.Name = "txtBalanceLeave";
            this.txtBalanceLeave.ReadOnly = true;
            this.txtBalanceLeave.Size = new System.Drawing.Size(154, 28);
            this.txtBalanceLeave.TabIndex = 44;
            this.txtBalanceLeave.Tag = "Please enter Employeee Code";
            this.txtBalanceLeave.WordWrap = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(303, 32);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 15);
            this.label7.TabIndex = 45;
            this.label7.Text = "Balance Leaves";
            // 
            // txtAvailableLeave
            // 
            this.txtAvailableLeave.Location = new System.Drawing.Point(126, 25);
            this.txtAvailableLeave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtAvailableLeave.MaxLength = 255;
            this.txtAvailableLeave.Multiline = true;
            this.txtAvailableLeave.Name = "txtAvailableLeave";
            this.txtAvailableLeave.ReadOnly = true;
            this.txtAvailableLeave.Size = new System.Drawing.Size(154, 28);
            this.txtAvailableLeave.TabIndex = 42;
            this.txtAvailableLeave.Tag = "Please enter Employeee Code";
            this.txtAvailableLeave.WordWrap = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(14, 32);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 15);
            this.label6.TabIndex = 43;
            this.label6.Text = "Available Leave";
            // 
            // txtLeaveDateTo
            // 
            this.txtLeaveDateTo.Enabled = false;
            this.txtLeaveDateTo.Location = new System.Drawing.Point(356, 120);
            this.txtLeaveDateTo.Mask = "##-##-####";
            this.txtLeaveDateTo.Name = "txtLeaveDateTo";
            this.txtLeaveDateTo.Size = new System.Drawing.Size(125, 21);
            this.txtLeaveDateTo.TabIndex = 41;
            this.txtLeaveDateTo.Tag = "Please enter Employeee Date Of Birth";
            this.txtLeaveDateTo.ValidatingType = typeof(System.DateTime);
            this.txtLeaveDateTo.TextChanged += new System.EventHandler(this.txtLeaveDateTo_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(330, 123);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 15);
            this.label5.TabIndex = 40;
            this.label5.Text = "To";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbDuration
            // 
            this.cmbDuration.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDuration.Enabled = false;
            this.cmbDuration.FormattingEnabled = true;
            this.cmbDuration.Location = new System.Drawing.Point(126, 88);
            this.cmbDuration.Name = "cmbDuration";
            this.cmbDuration.Size = new System.Drawing.Size(440, 23);
            this.cmbDuration.TabIndex = 39;
            this.cmbDuration.Tag = "Please enter Employee Designation";
            this.cmbDuration.SelectedIndexChanged += new System.EventHandler(this.cmbDuration_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(59, 92);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 15);
            this.label4.TabIndex = 38;
            this.label4.Text = "Duration";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLeaveDateFrom
            // 
            this.txtLeaveDateFrom.Enabled = false;
            this.txtLeaveDateFrom.Location = new System.Drawing.Point(125, 120);
            this.txtLeaveDateFrom.Mask = "##-##-####";
            this.txtLeaveDateFrom.Name = "txtLeaveDateFrom";
            this.txtLeaveDateFrom.Size = new System.Drawing.Size(125, 21);
            this.txtLeaveDateFrom.TabIndex = 37;
            this.txtLeaveDateFrom.Tag = "Please enter Employeee Date Of Birth";
            this.txtLeaveDateFrom.ValidatingType = typeof(System.DateTime);
            this.txtLeaveDateFrom.TextChanged += new System.EventHandler(this.txtLeaveDateFrom_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(80, 123);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 15);
            this.label3.TabIndex = 36;
            this.label3.Text = "From";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbLeaveType
            // 
            this.cmbLeaveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLeaveType.Enabled = false;
            this.cmbLeaveType.FormattingEnabled = true;
            this.cmbLeaveType.Location = new System.Drawing.Point(126, 59);
            this.cmbLeaveType.Name = "cmbLeaveType";
            this.cmbLeaveType.Size = new System.Drawing.Size(440, 23);
            this.cmbLeaveType.TabIndex = 35;
            this.cmbLeaveType.Tag = "Please enter Employee Designation";
            this.cmbLeaveType.SelectedIndexChanged += new System.EventHandler(this.cmbLeaveType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(42, 63);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 15);
            this.label2.TabIndex = 34;
            this.label2.Text = "Leave Type";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnSearch);
            this.groupBox4.Controls.Add(this.label38);
            this.groupBox4.Controls.Add(this.picEmpPhoto);
            this.groupBox4.Controls.Add(this.cmbDesignation);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Controls.Add(this.cmbDepartment);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.txtEmployeeName);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.lblEmpID);
            this.groupBox4.Controls.Add(this.lblActionMode);
            this.groupBox4.Controls.Add(this.txtEmpCode);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1111, 156);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Professional Info";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(303, 23);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSearch.Size = new System.Drawing.Size(29, 28);
            this.btnSearch.TabIndex = 36;
            this.btnSearch.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSearch.Values.Image = global::StaffSync.Properties.Resources.search;
            this.btnSearch.Values.Text = "";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(713, 30);
            this.label38.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(44, 15);
            this.label38.TabIndex = 35;
            this.label38.Text = "Photo";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picEmpPhoto
            // 
            this.picEmpPhoto.Location = new System.Drawing.Point(764, 26);
            this.picEmpPhoto.Name = "picEmpPhoto";
            this.picEmpPhoto.Size = new System.Drawing.Size(131, 115);
            this.picEmpPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picEmpPhoto.TabIndex = 34;
            this.picEmpPhoto.TabStop = false;
            // 
            // cmbDesignation
            // 
            this.cmbDesignation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDesignation.FormattingEnabled = true;
            this.cmbDesignation.Location = new System.Drawing.Point(127, 122);
            this.cmbDesignation.Name = "cmbDesignation";
            this.cmbDesignation.Size = new System.Drawing.Size(440, 23);
            this.cmbDesignation.TabIndex = 33;
            this.cmbDesignation.Tag = "Please enter Employee Designation";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(43, 126);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(84, 15);
            this.label26.TabIndex = 31;
            this.label26.Text = "Designation";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment.FormattingEnabled = true;
            this.cmbDepartment.Location = new System.Drawing.Point(127, 93);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(440, 23);
            this.cmbDepartment.TabIndex = 32;
            this.cmbDepartment.Tag = "Please enter Employee Department";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(43, 97);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(82, 15);
            this.label25.TabIndex = 30;
            this.label25.Text = "Department";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Location = new System.Drawing.Point(128, 57);
            this.txtEmployeeName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtEmployeeName.MaxLength = 255;
            this.txtEmployeeName.Multiline = true;
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.Size = new System.Drawing.Size(440, 28);
            this.txtEmployeeName.TabIndex = 12;
            this.txtEmployeeName.Tag = "Please enter Employeee Name";
            this.txtEmployeeName.WordWrap = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(80, 64);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(45, 15);
            this.label19.TabIndex = 11;
            this.label19.Text = "Name";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblEmpID
            // 
            this.lblEmpID.AutoSize = true;
            this.lblEmpID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblEmpID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpID.Location = new System.Drawing.Point(591, 30);
            this.lblEmpID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmpID.Name = "lblEmpID";
            this.lblEmpID.Size = new System.Drawing.Size(11, 15);
            this.lblEmpID.TabIndex = 7;
            this.lblEmpID.Text = " ";
            this.lblEmpID.Visible = false;
            // 
            // lblActionMode
            // 
            this.lblActionMode.AutoSize = true;
            this.lblActionMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActionMode.Location = new System.Drawing.Point(439, 30);
            this.lblActionMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblActionMode.Name = "lblActionMode";
            this.lblActionMode.Size = new System.Drawing.Size(98, 15);
            this.lblActionMode.TabIndex = 6;
            this.lblActionMode.Text = "lblActionMode";
            this.lblActionMode.Visible = false;
            // 
            // txtEmpCode
            // 
            this.txtEmpCode.Location = new System.Drawing.Point(128, 23);
            this.txtEmpCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtEmpCode.MaxLength = 255;
            this.txtEmpCode.Multiline = true;
            this.txtEmpCode.Name = "txtEmpCode";
            this.txtEmpCode.ReadOnly = true;
            this.txtEmpCode.Size = new System.Drawing.Size(168, 28);
            this.txtEmpCode.TabIndex = 2;
            this.txtEmpCode.Tag = "Please enter Employeee Code";
            this.txtEmpCode.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Employee Code";
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
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1133, 57);
            this.panel2.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(284, 9);
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
            this.btnCloseMe.Location = new System.Drawing.Point(981, 9);
            this.btnCloseMe.Name = "btnCloseMe";
            this.btnCloseMe.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCloseMe.Size = new System.Drawing.Size(126, 38);
            this.btnCloseMe.TabIndex = 20;
            this.btnCloseMe.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCloseMe.Values.Image = global::StaffSync.Properties.Resources.close;
            this.btnCloseMe.Values.Text = "Close Me";
            this.btnCloseMe.Click += new System.EventHandler(this.btnCloseMe_Click);
            // 
            // btnRemoveDetails
            // 
            this.btnRemoveDetails.Location = new System.Drawing.Point(420, 9);
            this.btnRemoveDetails.Name = "btnRemoveDetails";
            this.btnRemoveDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnRemoveDetails.Size = new System.Drawing.Size(126, 38);
            this.btnRemoveDetails.TabIndex = 19;
            this.btnRemoveDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnRemoveDetails.Values.Text = "Delete";
            this.btnRemoveDetails.Visible = false;
            // 
            // btnSaveDetails
            // 
            this.btnSaveDetails.Location = new System.Drawing.Point(149, 9);
            this.btnSaveDetails.Name = "btnSaveDetails";
            this.btnSaveDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSaveDetails.Size = new System.Drawing.Size(126, 38);
            this.btnSaveDetails.TabIndex = 18;
            this.btnSaveDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSaveDetails.Values.Image = global::StaffSync.Properties.Resources.save;
            this.btnSaveDetails.Values.Text = "Save";
            this.btnSaveDetails.Click += new System.EventHandler(this.btnSaveDetails_Click);
            // 
            // btnModifyDetails
            // 
            this.btnModifyDetails.Location = new System.Drawing.Point(149, 9);
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
            this.btnGenerateDetails.Location = new System.Drawing.Point(13, 9);
            this.btnGenerateDetails.Name = "btnGenerateDetails";
            this.btnGenerateDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnGenerateDetails.Size = new System.Drawing.Size(126, 38);
            this.btnGenerateDetails.TabIndex = 16;
            this.btnGenerateDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnGenerateDetails.Values.Image = global::StaffSync.Properties.Resources._new;
            this.btnGenerateDetails.Values.Text = "Generate";
            this.btnGenerateDetails.Click += new System.EventHandler(this.btnGenerateDetails_Click);
            // 
            // errValidator
            // 
            this.errValidator.ContainerControl = this;
            // 
            // staffsyncDBDTSet
            // 
            this.staffsyncDBDTSet.DataSetName = "StaffsyncDBDTSet";
            this.staffsyncDBDTSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // qryRoleProfileBindingSource
            // 
            this.qryRoleProfileBindingSource.DataMember = "qryRoleProfile";
            this.qryRoleProfileBindingSource.DataSource = this.staffsyncDBDTSet;
            // 
            // qryRoleProfileTableAdapter
            // 
            this.qryRoleProfileTableAdapter.ClearBeforeFill = true;
            // 
            // kryptonCommand1
            // 
            this.kryptonCommand1.Text = "kryptonCommand1";
            // 
            // kryptonCommand2
            // 
            this.kryptonCommand2.Text = "kryptonCommand2";
            // 
            // picViewLeaves
            // 
            this.picViewLeaves.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picViewLeaves.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picViewLeaves.Image = global::StaffSync.Properties.Resources.search;
            this.picViewLeaves.Location = new System.Drawing.Point(574, 28);
            this.picViewLeaves.Name = "picViewLeaves";
            this.picViewLeaves.Size = new System.Drawing.Size(23, 22);
            this.picViewLeaves.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picViewLeaves.TabIndex = 50;
            this.picViewLeaves.TabStop = false;
            this.picViewLeaves.Click += new System.EventHandler(this.picViewLeaves_Click);
            // 
            // frmLeavesApproval
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1133, 624);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLeavesApproval";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Leave Approval";
            this.Load += new System.EventHandler(this.frmLeavesApproval_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDownloadLeaveTRList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRefreshLeaveTRList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEmpPhoto)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryRoleProfileBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picViewLeaves)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.Label lblEmpID;
        public System.Windows.Forms.Label lblActionMode;
        private System.Windows.Forms.TextBox txtEmpCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtEmployeeName;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cmbDesignation;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.ComboBox cmbDepartment;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.PictureBox picEmpPhoto;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox picDownloadLeaveTRList;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox picRefreshLeaveTRList;
        private System.Windows.Forms.ErrorProvider errValidator;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbLeaveType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox txtLeaveDateFrom;
        private System.Windows.Forms.ComboBox cmbDuration;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox txtLeaveDateTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAvailableLeave;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBalanceLeave;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label lblLeaveTRID;
        private System.Windows.Forms.TextBox txtActualLeaveDays;
        private System.Windows.Forms.TextBox txtApprovalNote;
        private System.Windows.Forms.Label label8;
        private StaffsyncDBDTSet staffsyncDBDTSet;
        private System.Windows.Forms.BindingSource qryRoleProfileBindingSource;
        private StaffsyncDBDTSetTableAdapters.qryRoleProfileTableAdapter qryRoleProfileTableAdapter;
        private Krypton.Toolkit.KryptonCommand kryptonCommand1;
        private Krypton.Toolkit.KryptonCommand kryptonCommand2;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private Krypton.Toolkit.KryptonButton btnCloseMe;
        private Krypton.Toolkit.KryptonButton btnRemoveDetails;
        private Krypton.Toolkit.KryptonButton btnSaveDetails;
        private Krypton.Toolkit.KryptonButton btnModifyDetails;
        private Krypton.Toolkit.KryptonButton btnGenerateDetails;
        private Krypton.Toolkit.KryptonButton btnSearch;
        public System.Windows.Forms.Label lblSpecificLeaveBalance;
        public System.Windows.Forms.Label lblLeaveMasID;
        private System.Windows.Forms.ListView lstLeaveTRList;
        private System.Windows.Forms.ColumnHeader LeaveTRID;
        private System.Windows.Forms.ColumnHeader LeaveType;
        private System.Windows.Forms.ColumnHeader LeaveDateFrom;
        private System.Windows.Forms.ColumnHeader LeaveDateTo;
        private System.Windows.Forms.ColumnHeader LeaveDuration;
        private System.Windows.Forms.ColumnHeader LeaveComments;
        private System.Windows.Forms.ColumnHeader LeaveStatus;
        private System.Windows.Forms.ColumnHeader LeaveMode;
        private System.Windows.Forms.ColumnHeader Canceled;
        private System.Windows.Forms.ColumnHeader CanceledDate;
        private System.Windows.Forms.PictureBox picViewLeaves;
    }
}