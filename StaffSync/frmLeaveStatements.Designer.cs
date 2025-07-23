namespace StaffSync
{
    partial class frmLeaveStatements
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLeaveStatements));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtgLeaveStatement = new Krypton.Toolkit.KryptonDataGridView();
            this.picDownloadLeaveTRList = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.picRefreshLeaveTRList = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbLeaveStatus = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtToDate = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFromDate = new System.Windows.Forms.MaskedTextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.cmbLeaveType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnSearch = new Krypton.Toolkit.KryptonButton();
            this.txtEmployeeName = new Krypton.Toolkit.KryptonTextBox();
            this.txtEmpCode = new Krypton.Toolkit.KryptonTextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.picEmpPhoto = new System.Windows.Forms.PictureBox();
            this.cmbDesignation = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.cmbDepartment = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblEmpID = new System.Windows.Forms.Label();
            this.lblActionMode = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.btnCloseMe = new Krypton.Toolkit.KryptonButton();
            this.btnSaveDetails = new Krypton.Toolkit.KryptonButton();
            this.btnModifyDetails = new Krypton.Toolkit.KryptonButton();
            this.btnGenerateDetails = new Krypton.Toolkit.KryptonButton();
            this.btnRemoveDetails = new Krypton.Toolkit.KryptonButton();
            this.cmLeaveCancel = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tlbCancelLeave = new System.Windows.Forms.ToolStripMenuItem();
            this.errValidator = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLeaveStatement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDownloadLeaveTRList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRefreshLeaveTRList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEmpPhoto)).BeginInit();
            this.panel2.SuspendLayout();
            this.cmLeaveCancel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(930, 624);
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
            this.panel1.Size = new System.Drawing.Size(930, 563);
            this.panel1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtgLeaveStatement);
            this.groupBox2.Controls.Add(this.picDownloadLeaveTRList);
            this.groupBox2.Controls.Add(this.pictureBox3);
            this.groupBox2.Controls.Add(this.picRefreshLeaveTRList);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(13, 300);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(906, 253);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Leave History";
            // 
            // dtgLeaveStatement
            // 
            this.dtgLeaveStatement.AllowUserToAddRows = false;
            this.dtgLeaveStatement.AllowUserToDeleteRows = false;
            this.dtgLeaveStatement.AllowUserToResizeRows = false;
            this.dtgLeaveStatement.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgLeaveStatement.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dtgLeaveStatement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgLeaveStatement.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dtgLeaveStatement.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dtgLeaveStatement.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.ButtonLowProfile;
            this.dtgLeaveStatement.Location = new System.Drawing.Point(15, 49);
            this.dtgLeaveStatement.MultiSelect = false;
            this.dtgLeaveStatement.Name = "dtgLeaveStatement";
            this.dtgLeaveStatement.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtgLeaveStatement.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgLeaveStatement.Size = new System.Drawing.Size(880, 200);
            this.dtgLeaveStatement.TabIndex = 47;
            // 
            // picDownloadLeaveTRList
            // 
            this.picDownloadLeaveTRList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picDownloadLeaveTRList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picDownloadLeaveTRList.Image = global::StaffSync.Properties.Resources.download01;
            this.picDownloadLeaveTRList.Location = new System.Drawing.Point(823, 22);
            this.picDownloadLeaveTRList.Name = "picDownloadLeaveTRList";
            this.picDownloadLeaveTRList.Size = new System.Drawing.Size(21, 20);
            this.picDownloadLeaveTRList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDownloadLeaveTRList.TabIndex = 22;
            this.picDownloadLeaveTRList.TabStop = false;
            this.picDownloadLeaveTRList.Click += new System.EventHandler(this.picDownloadLeaveTRList_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox3.Image = global::StaffSync.Properties.Resources.mail01;
            this.pictureBox3.Location = new System.Drawing.Point(846, 21);
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
            this.picRefreshLeaveTRList.Location = new System.Drawing.Point(871, 21);
            this.picRefreshLeaveTRList.Name = "picRefreshLeaveTRList";
            this.picRefreshLeaveTRList.Size = new System.Drawing.Size(23, 22);
            this.picRefreshLeaveTRList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRefreshLeaveTRList.TabIndex = 20;
            this.picRefreshLeaveTRList.TabStop = false;
            this.picRefreshLeaveTRList.Click += new System.EventHandler(this.picRefreshLeaveTRList_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbLeaveStatus);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtToDate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtFromDate);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.cmbLeaveType);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 167);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(906, 113);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Leave Filter";
            // 
            // cmbLeaveStatus
            // 
            this.cmbLeaveStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLeaveStatus.FormattingEnabled = true;
            this.cmbLeaveStatus.Location = new System.Drawing.Point(124, 79);
            this.cmbLeaveStatus.Name = "cmbLeaveStatus";
            this.cmbLeaveStatus.Size = new System.Drawing.Size(259, 23);
            this.cmbLeaveStatus.TabIndex = 41;
            this.cmbLeaveStatus.Tag = "Please enter Employee Designation";
            this.cmbLeaveStatus.SelectedIndexChanged += new System.EventHandler(this.cmbLeaveStatus_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(34, 83);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 15);
            this.label4.TabIndex = 40;
            this.label4.Text = "Leave Status";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtToDate
            // 
            this.txtToDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(0)))), ((int)(((byte)(254)))));
            this.txtToDate.Location = new System.Drawing.Point(286, 20);
            this.txtToDate.Mask = "##-##-####";
            this.txtToDate.Name = "txtToDate";
            this.txtToDate.Size = new System.Drawing.Size(97, 21);
            this.txtToDate.TabIndex = 38;
            this.txtToDate.Tag = "Please enter Employeee Date Of Birth";
            this.txtToDate.ValidatingType = typeof(System.DateTime);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(261, 23);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 15);
            this.label3.TabIndex = 39;
            this.label3.Text = "To";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtFromDate
            // 
            this.txtFromDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(0)))), ((int)(((byte)(254)))));
            this.txtFromDate.Location = new System.Drawing.Point(125, 20);
            this.txtFromDate.Mask = "##-##-####";
            this.txtFromDate.Name = "txtFromDate";
            this.txtFromDate.Size = new System.Drawing.Size(97, 21);
            this.txtFromDate.TabIndex = 36;
            this.txtFromDate.Tag = "Please enter Employeee Date Of Birth";
            this.txtFromDate.ValidatingType = typeof(System.DateTime);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(83, 23);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(40, 15);
            this.label29.TabIndex = 37;
            this.label29.Text = "From";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbLeaveType
            // 
            this.cmbLeaveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLeaveType.FormattingEnabled = true;
            this.cmbLeaveType.Location = new System.Drawing.Point(125, 50);
            this.cmbLeaveType.Name = "cmbLeaveType";
            this.cmbLeaveType.Size = new System.Drawing.Size(258, 23);
            this.cmbLeaveType.TabIndex = 35;
            this.cmbLeaveType.Tag = "Please enter Employee Designation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(44, 54);
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
            this.groupBox4.Controls.Add(this.txtEmployeeName);
            this.groupBox4.Controls.Add(this.txtEmpCode);
            this.groupBox4.Controls.Add(this.label38);
            this.groupBox4.Controls.Add(this.picEmpPhoto);
            this.groupBox4.Controls.Add(this.cmbDesignation);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Controls.Add(this.cmbDepartment);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.lblEmpID);
            this.groupBox4.Controls.Add(this.lblActionMode);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(12, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(907, 156);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Professional Info";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(300, 26);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSearch.Size = new System.Drawing.Size(29, 28);
            this.btnSearch.TabIndex = 44;
            this.btnSearch.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSearch.Values.Image = global::StaffSync.Properties.Resources.search;
            this.btnSearch.Values.Text = "";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.Location = new System.Drawing.Point(125, 60);
            this.txtEmployeeName.Multiline = true;
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtEmployeeName.Size = new System.Drawing.Size(440, 28);
            this.txtEmployeeName.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmployeeName.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtEmployeeName.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmployeeName.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmployeeName.TabIndex = 43;
            this.txtEmployeeName.WordWrap = false;
            // 
            // txtEmpCode
            // 
            this.txtEmpCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEmpCode.Location = new System.Drawing.Point(125, 26);
            this.txtEmpCode.Multiline = true;
            this.txtEmpCode.Name = "txtEmpCode";
            this.txtEmpCode.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtEmpCode.ReadOnly = true;
            this.txtEmpCode.Size = new System.Drawing.Size(168, 28);
            this.txtEmpCode.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpCode.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtEmpCode.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpCode.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpCode.TabIndex = 42;
            this.txtEmpCode.WordWrap = false;
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
            this.cmbDesignation.Location = new System.Drawing.Point(125, 122);
            this.cmbDesignation.Name = "cmbDesignation";
            this.cmbDesignation.Size = new System.Drawing.Size(440, 23);
            this.cmbDesignation.TabIndex = 33;
            this.cmbDesignation.Tag = "Please enter Employee Designation";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(39, 126);
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
            this.cmbDepartment.Location = new System.Drawing.Point(125, 93);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.Size = new System.Drawing.Size(440, 23);
            this.cmbDepartment.TabIndex = 32;
            this.cmbDepartment.Tag = "Please enter Employee Department";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(41, 97);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(82, 15);
            this.label25.TabIndex = 30;
            this.label25.Text = "Department";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(78, 64);
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
            this.panel2.Controls.Add(this.btnSaveDetails);
            this.panel2.Controls.Add(this.btnModifyDetails);
            this.panel2.Controls.Add(this.btnGenerateDetails);
            this.panel2.Controls.Add(this.btnRemoveDetails);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(930, 57);
            this.panel2.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(154, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCancel.Size = new System.Drawing.Size(126, 38);
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Image = global::StaffSync.Properties.Resources.cancel;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(782, 9);
            this.btnCloseMe.Name = "btnCloseMe";
            this.btnCloseMe.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCloseMe.Size = new System.Drawing.Size(126, 38);
            this.btnCloseMe.TabIndex = 25;
            this.btnCloseMe.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCloseMe.Values.Image = global::StaffSync.Properties.Resources.close;
            this.btnCloseMe.Values.Text = "Close Me";
            this.btnCloseMe.Click += new System.EventHandler(this.btnCloseMe_Click);
            // 
            // btnSaveDetails
            // 
            this.btnSaveDetails.Location = new System.Drawing.Point(154, 9);
            this.btnSaveDetails.Name = "btnSaveDetails";
            this.btnSaveDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSaveDetails.Size = new System.Drawing.Size(126, 38);
            this.btnSaveDetails.TabIndex = 24;
            this.btnSaveDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSaveDetails.Values.Image = global::StaffSync.Properties.Resources.save;
            this.btnSaveDetails.Values.Text = "Save";
            this.btnSaveDetails.Visible = false;
            this.btnSaveDetails.Click += new System.EventHandler(this.btnSaveDetails_Click);
            // 
            // btnModifyDetails
            // 
            this.btnModifyDetails.Location = new System.Drawing.Point(154, 9);
            this.btnModifyDetails.Name = "btnModifyDetails";
            this.btnModifyDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnModifyDetails.Size = new System.Drawing.Size(126, 38);
            this.btnModifyDetails.TabIndex = 23;
            this.btnModifyDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnModifyDetails.Values.Image = global::StaffSync.Properties.Resources.update;
            this.btnModifyDetails.Values.Text = "Modify";
            this.btnModifyDetails.Visible = false;
            this.btnModifyDetails.Click += new System.EventHandler(this.btnModifyDetails_Click);
            // 
            // btnGenerateDetails
            // 
            this.btnGenerateDetails.Location = new System.Drawing.Point(18, 9);
            this.btnGenerateDetails.Name = "btnGenerateDetails";
            this.btnGenerateDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnGenerateDetails.Size = new System.Drawing.Size(126, 38);
            this.btnGenerateDetails.TabIndex = 22;
            this.btnGenerateDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnGenerateDetails.Values.Image = global::StaffSync.Properties.Resources._new;
            this.btnGenerateDetails.Values.Text = "Generate";
            this.btnGenerateDetails.Click += new System.EventHandler(this.btnGenerateDetails_Click);
            // 
            // btnRemoveDetails
            // 
            this.btnRemoveDetails.Location = new System.Drawing.Point(426, 9);
            this.btnRemoveDetails.Name = "btnRemoveDetails";
            this.btnRemoveDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnRemoveDetails.Size = new System.Drawing.Size(126, 38);
            this.btnRemoveDetails.TabIndex = 26;
            this.btnRemoveDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnRemoveDetails.Values.Text = "Delete";
            this.btnRemoveDetails.Visible = false;
            // 
            // cmLeaveCancel
            // 
            this.cmLeaveCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cmLeaveCancel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlbCancelLeave});
            this.cmLeaveCancel.Name = "cmDatamartList01";
            this.cmLeaveCancel.Size = new System.Drawing.Size(144, 26);
            this.cmLeaveCancel.Tag = "DatamartMenu";
            this.cmLeaveCancel.Text = "DatamartMenu";
            this.cmLeaveCancel.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.cmLeaveCancel_ItemClicked);
            // 
            // tlbCancelLeave
            // 
            this.tlbCancelLeave.Image = global::StaffSync.Properties.Resources.auth01;
            this.tlbCancelLeave.Name = "tlbCancelLeave";
            this.tlbCancelLeave.Size = new System.Drawing.Size(143, 22);
            this.tlbCancelLeave.Tag = "cmbCancelLeave";
            this.tlbCancelLeave.Text = "Cancel Leave";
            // 
            // errValidator
            // 
            this.errValidator.ContainerControl = this;
            // 
            // frmLeaveStatements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(930, 624);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLeaveStatements";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Leave Statement";
            this.Load += new System.EventHandler(this.frmLeaveStatements_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgLeaveStatement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDownloadLeaveTRList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRefreshLeaveTRList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEmpPhoto)).EndInit();
            this.panel2.ResumeLayout(false);
            this.cmLeaveCancel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.Label lblEmpID;
        public System.Windows.Forms.Label lblActionMode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
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
        private System.Windows.Forms.ComboBox cmbLeaveType;
        private System.Windows.Forms.Label label2;
        private Krypton.Toolkit.KryptonTextBox txtEmployeeName;
        private Krypton.Toolkit.KryptonTextBox txtEmpCode;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private Krypton.Toolkit.KryptonButton btnCloseMe;
        private Krypton.Toolkit.KryptonButton btnSaveDetails;
        private Krypton.Toolkit.KryptonButton btnModifyDetails;
        private Krypton.Toolkit.KryptonButton btnGenerateDetails;
        private Krypton.Toolkit.KryptonButton btnRemoveDetails;
        private Krypton.Toolkit.KryptonButton btnSearch;
        private System.Windows.Forms.ContextMenuStrip cmLeaveCancel;
        private System.Windows.Forms.ToolStripMenuItem tlbCancelLeave;
        private System.Windows.Forms.ComboBox cmbLeaveStatus;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox txtToDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox txtFromDate;
        private System.Windows.Forms.Label label29;
        private Krypton.Toolkit.KryptonDataGridView dtgLeaveStatement;
    }
}