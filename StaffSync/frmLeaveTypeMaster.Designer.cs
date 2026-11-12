namespace StaffSync
{
    partial class frmLeaveTypeMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLeaveTypeMaster));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cmbIsActive = new Krypton.Toolkit.KryptonComboBox();
            this.cmbLeavePaid = new Krypton.Toolkit.KryptonComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txtLeaveTitle = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lnkViewAuditLog = new Krypton.Toolkit.KryptonLinkLabel();
            this.btnSearch = new Krypton.Toolkit.KryptonButton();
            this.lblLeaveTypeID = new System.Windows.Forms.Label();
            this.lblActionMode = new System.Windows.Forms.Label();
            this.txtLeaveCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCloseMe = new Krypton.Toolkit.KryptonButton();
            this.btnRemoveDetails = new Krypton.Toolkit.KryptonButton();
            this.btnSaveDetails = new Krypton.Toolkit.KryptonButton();
            this.btnModifyDetails = new Krypton.Toolkit.KryptonButton();
            this.btnGenerateDetails = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.empMasInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.staffsyncDBDTSet = new StaffSync.StaffsyncDBDTSet();
            this.empMasInfoTableAdapter = new StaffSync.StaffsyncDBDTSetTableAdapters.EmpMasInfoTableAdapter();
            this.errValidator = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.grpIsCarryForwardAllowed = new System.Windows.Forms.GroupBox();
            this.txtMaxDaysCarryForwardAllowed = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCarryForwardExpiryMonths = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkIsCarryForwardExpiryApplicable = new System.Windows.Forms.CheckBox();
            this.grpIsEncashmentAllowed = new System.Windows.Forms.GroupBox();
            this.chkIsEncashmentDuringEmploymentAllowed = new System.Windows.Forms.CheckBox();
            this.txtMaxLeaveDaysAllowedToEncash = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chkIsEncashmentAllowed = new System.Windows.Forms.CheckBox();
            this.chkIsEncashmentAtResignationAllowed = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkIsLeaveAccrualApplicable = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.chkIsApplicableDuringNoticePeriod = new System.Windows.Forms.CheckBox();
            this.chkIsApplicableDuringProbation = new System.Windows.Forms.CheckBox();
            this.cmbAccrualFrequency = new Krypton.Toolkit.KryptonComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkIsCarryForwardAllowed = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbIsActive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLeavePaid)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.grpIsCarryForwardAllowed.SuspendLayout();
            this.grpIsEncashmentAllowed.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAccrualFrequency)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(944, 627);
            this.splitContainer1.SplitterDistance = 554;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(944, 554);
            this.panel1.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cmbIsActive);
            this.groupBox5.Controls.Add(this.cmbLeavePaid);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.txtLeaveTitle);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(19, 102);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(910, 144);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Leave Details";
            // 
            // cmbIsActive
            // 
            this.cmbIsActive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIsActive.DropDownWidth = 440;
            this.cmbIsActive.Location = new System.Drawing.Point(188, 104);
            this.cmbIsActive.Margin = new System.Windows.Forms.Padding(4);
            this.cmbIsActive.Name = "cmbIsActive";
            this.cmbIsActive.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbIsActive.Size = new System.Drawing.Size(569, 22);
            this.cmbIsActive.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbIsActive.TabIndex = 45;
            this.cmbIsActive.Tag = "Is Active";
            // 
            // cmbLeavePaid
            // 
            this.cmbLeavePaid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLeavePaid.DropDownWidth = 440;
            this.cmbLeavePaid.Location = new System.Drawing.Point(188, 74);
            this.cmbLeavePaid.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLeavePaid.Name = "cmbLeavePaid";
            this.cmbLeavePaid.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbLeavePaid.Size = new System.Drawing.Size(569, 22);
            this.cmbLeavePaid.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbLeavePaid.TabIndex = 44;
            this.cmbLeavePaid.Tag = "Is Paid";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(134, 78);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 15);
            this.label2.TabIndex = 35;
            this.label2.Text = "Is Paid";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(126, 108);
            this.label17.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(59, 15);
            this.label17.TabIndex = 33;
            this.label17.Text = "Is Active";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLeaveTitle
            // 
            this.txtLeaveTitle.Location = new System.Drawing.Point(188, 34);
            this.txtLeaveTitle.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtLeaveTitle.MaxLength = 255;
            this.txtLeaveTitle.Multiline = true;
            this.txtLeaveTitle.Name = "txtLeaveTitle";
            this.txtLeaveTitle.Size = new System.Drawing.Size(567, 25);
            this.txtLeaveTitle.TabIndex = 3;
            this.txtLeaveTitle.Tag = "Leave Title";
            this.txtLeaveTitle.WordWrap = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(108, 39);
            this.label16.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 15);
            this.label16.TabIndex = 7;
            this.label16.Text = "Leave Title";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lnkViewAuditLog);
            this.groupBox4.Controls.Add(this.btnSearch);
            this.groupBox4.Controls.Add(this.lblLeaveTypeID);
            this.groupBox4.Controls.Add(this.lblActionMode);
            this.groupBox4.Controls.Add(this.txtLeaveCode);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(24, 15);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(905, 80);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Leave Info";
            // 
            // lnkViewAuditLog
            // 
            this.lnkViewAuditLog.Location = new System.Drawing.Point(462, 34);
            this.lnkViewAuditLog.Name = "lnkViewAuditLog";
            this.lnkViewAuditLog.Size = new System.Drawing.Size(93, 20);
            this.lnkViewAuditLog.TabIndex = 90;
            this.lnkViewAuditLog.Values.Text = "View Audit Log";
            this.lnkViewAuditLog.Visible = false;
            this.lnkViewAuditLog.LinkClicked += new System.EventHandler(this.lnkViewAuditLog_LinkClicked);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(416, 27);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSearch.Size = new System.Drawing.Size(39, 34);
            this.btnSearch.TabIndex = 37;
            this.btnSearch.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSearch.Values.Image = global::StaffSync.Properties.Resources.search;
            this.btnSearch.Values.Text = "";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblLeaveTypeID
            // 
            this.lblLeaveTypeID.AutoSize = true;
            this.lblLeaveTypeID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblLeaveTypeID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeaveTypeID.Location = new System.Drawing.Point(776, 37);
            this.lblLeaveTypeID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblLeaveTypeID.Name = "lblLeaveTypeID";
            this.lblLeaveTypeID.Size = new System.Drawing.Size(11, 15);
            this.lblLeaveTypeID.TabIndex = 5;
            this.lblLeaveTypeID.Text = " ";
            this.lblLeaveTypeID.Visible = false;
            // 
            // lblActionMode
            // 
            this.lblActionMode.AutoSize = true;
            this.lblActionMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActionMode.Location = new System.Drawing.Point(573, 37);
            this.lblActionMode.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblActionMode.Name = "lblActionMode";
            this.lblActionMode.Size = new System.Drawing.Size(98, 15);
            this.lblActionMode.TabIndex = 4;
            this.lblActionMode.Text = "lblActionMode";
            this.lblActionMode.Visible = false;
            // 
            // txtLeaveCode
            // 
            this.txtLeaveCode.Location = new System.Drawing.Point(188, 32);
            this.txtLeaveCode.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtLeaveCode.MaxLength = 255;
            this.txtLeaveCode.Multiline = true;
            this.txtLeaveCode.Name = "txtLeaveCode";
            this.txtLeaveCode.ReadOnly = true;
            this.txtLeaveCode.Size = new System.Drawing.Size(223, 25);
            this.txtLeaveCode.TabIndex = 2;
            this.txtLeaveCode.Tag = "Leave Code";
            this.txtLeaveCode.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(99, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Leave Code";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel2.Controls.Add(this.btnCloseMe);
            this.panel2.Controls.Add(this.btnRemoveDetails);
            this.panel2.Controls.Add(this.btnSaveDetails);
            this.panel2.Controls.Add(this.btnModifyDetails);
            this.panel2.Controls.Add(this.btnGenerateDetails);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(944, 68);
            this.panel2.TabIndex = 1;
            // 
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(803, 18);
            this.btnCloseMe.Margin = new System.Windows.Forms.Padding(4);
            this.btnCloseMe.Name = "btnCloseMe";
            this.btnCloseMe.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCloseMe.Size = new System.Drawing.Size(126, 38);
            this.btnCloseMe.TabIndex = 20;
            this.toolTip1.SetToolTip(this.btnCloseMe, "Close Me (Ctrl + Z)");
            this.btnCloseMe.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCloseMe.Values.Image = global::StaffSync.Properties.Resources.close;
            this.btnCloseMe.Values.Text = "Close Me";
            this.btnCloseMe.Click += new System.EventHandler(this.btnCloseMe_Click);
            // 
            // btnRemoveDetails
            // 
            this.btnRemoveDetails.Location = new System.Drawing.Point(438, 18);
            this.btnRemoveDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemoveDetails.Name = "btnRemoveDetails";
            this.btnRemoveDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnRemoveDetails.Size = new System.Drawing.Size(126, 38);
            this.btnRemoveDetails.TabIndex = 19;
            this.toolTip1.SetToolTip(this.btnRemoveDetails, "Delete (Ctrl + D)");
            this.btnRemoveDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnRemoveDetails.Values.Image = global::StaffSync.Properties.Resources.delete;
            this.btnRemoveDetails.Values.Text = "Delete";
            this.btnRemoveDetails.Click += new System.EventHandler(this.btnRemoveDetails_Click);
            // 
            // btnSaveDetails
            // 
            this.btnSaveDetails.Location = new System.Drawing.Point(304, 18);
            this.btnSaveDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveDetails.Name = "btnSaveDetails";
            this.btnSaveDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSaveDetails.Size = new System.Drawing.Size(126, 38);
            this.btnSaveDetails.TabIndex = 18;
            this.toolTip1.SetToolTip(this.btnSaveDetails, "Save (Ctrl + S)");
            this.btnSaveDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSaveDetails.Values.Image = global::StaffSync.Properties.Resources.save;
            this.btnSaveDetails.Values.Text = "Save";
            this.btnSaveDetails.Click += new System.EventHandler(this.btnSaveDetails_Click);
            // 
            // btnModifyDetails
            // 
            this.btnModifyDetails.Location = new System.Drawing.Point(170, 18);
            this.btnModifyDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnModifyDetails.Name = "btnModifyDetails";
            this.btnModifyDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnModifyDetails.Size = new System.Drawing.Size(126, 38);
            this.btnModifyDetails.TabIndex = 17;
            this.toolTip1.SetToolTip(this.btnModifyDetails, "Modify (Ctrl + E)");
            this.btnModifyDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnModifyDetails.Values.Image = global::StaffSync.Properties.Resources.update;
            this.btnModifyDetails.Values.Text = "Modify";
            this.btnModifyDetails.Click += new System.EventHandler(this.btnModifyDetails_Click);
            // 
            // btnGenerateDetails
            // 
            this.btnGenerateDetails.Location = new System.Drawing.Point(36, 18);
            this.btnGenerateDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerateDetails.Name = "btnGenerateDetails";
            this.btnGenerateDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnGenerateDetails.Size = new System.Drawing.Size(126, 38);
            this.btnGenerateDetails.TabIndex = 16;
            this.toolTip1.SetToolTip(this.btnGenerateDetails, "Generate (Ctrl + N)");
            this.btnGenerateDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnGenerateDetails.Values.Image = global::StaffSync.Properties.Resources._new;
            this.btnGenerateDetails.Values.Text = "Generate";
            this.btnGenerateDetails.Click += new System.EventHandler(this.btnGenerateDetails_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(574, 18);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCancel.Size = new System.Drawing.Size(126, 38);
            this.btnCancel.TabIndex = 15;
            this.toolTip1.SetToolTip(this.btnCancel, "Cancel (Ctrl + X)");
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Image = global::StaffSync.Properties.Resources.cancel;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // empMasInfoBindingSource
            // 
            this.empMasInfoBindingSource.DataMember = "EmpMasInfo";
            this.empMasInfoBindingSource.DataSource = this.staffsyncDBDTSet;
            // 
            // staffsyncDBDTSet
            // 
            this.staffsyncDBDTSet.DataSetName = "StaffsyncDBDTSet";
            this.staffsyncDBDTSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // empMasInfoTableAdapter
            // 
            this.empMasInfoTableAdapter.ClearBeforeFill = true;
            // 
            // errValidator
            // 
            this.errValidator.ContainerControl = this;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkIsCarryForwardAllowed);
            this.groupBox1.Controls.Add(this.groupBox6);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.chkIsEncashmentAllowed);
            this.groupBox1.Controls.Add(this.grpIsEncashmentAllowed);
            this.groupBox1.Controls.Add(this.grpIsCarryForwardAllowed);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(17, 254);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(910, 285);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Additional Configurations";
            // 
            // grpIsCarryForwardAllowed
            // 
            this.grpIsCarryForwardAllowed.Controls.Add(this.chkIsCarryForwardExpiryApplicable);
            this.grpIsCarryForwardAllowed.Controls.Add(this.txtCarryForwardExpiryMonths);
            this.grpIsCarryForwardAllowed.Controls.Add(this.label4);
            this.grpIsCarryForwardAllowed.Controls.Add(this.txtMaxDaysCarryForwardAllowed);
            this.grpIsCarryForwardAllowed.Controls.Add(this.label3);
            this.grpIsCarryForwardAllowed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpIsCarryForwardAllowed.Location = new System.Drawing.Point(19, 137);
            this.grpIsCarryForwardAllowed.Margin = new System.Windows.Forms.Padding(4);
            this.grpIsCarryForwardAllowed.Name = "grpIsCarryForwardAllowed";
            this.grpIsCarryForwardAllowed.Padding = new System.Windows.Forms.Padding(4);
            this.grpIsCarryForwardAllowed.Size = new System.Drawing.Size(424, 134);
            this.grpIsCarryForwardAllowed.TabIndex = 55;
            this.grpIsCarryForwardAllowed.TabStop = false;
            // 
            // txtMaxDaysCarryForwardAllowed
            // 
            this.txtMaxDaysCarryForwardAllowed.Location = new System.Drawing.Point(255, 26);
            this.txtMaxDaysCarryForwardAllowed.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtMaxDaysCarryForwardAllowed.MaxLength = 255;
            this.txtMaxDaysCarryForwardAllowed.Multiline = true;
            this.txtMaxDaysCarryForwardAllowed.Name = "txtMaxDaysCarryForwardAllowed";
            this.txtMaxDaysCarryForwardAllowed.Size = new System.Drawing.Size(97, 25);
            this.txtMaxDaysCarryForwardAllowed.TabIndex = 55;
            this.txtMaxDaysCarryForwardAllowed.Tag = "Leave Code";
            this.txtMaxDaysCarryForwardAllowed.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(33, 31);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(212, 15);
            this.label3.TabIndex = 56;
            this.label3.Text = "Max. Days Carryforward Allowed";
            // 
            // txtCarryForwardExpiryMonths
            // 
            this.txtCarryForwardExpiryMonths.Location = new System.Drawing.Point(255, 88);
            this.txtCarryForwardExpiryMonths.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtCarryForwardExpiryMonths.MaxLength = 255;
            this.txtCarryForwardExpiryMonths.Multiline = true;
            this.txtCarryForwardExpiryMonths.Name = "txtCarryForwardExpiryMonths";
            this.txtCarryForwardExpiryMonths.Size = new System.Drawing.Size(97, 25);
            this.txtCarryForwardExpiryMonths.TabIndex = 57;
            this.txtCarryForwardExpiryMonths.Tag = "Leave Code";
            this.txtCarryForwardExpiryMonths.WordWrap = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 93);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(246, 15);
            this.label4.TabIndex = 58;
            this.label4.Text = "Carry Forward Expiry Period (Months)";
            // 
            // chkIsCarryForwardExpiryApplicable
            // 
            this.chkIsCarryForwardExpiryApplicable.AutoSize = true;
            this.chkIsCarryForwardExpiryApplicable.CheckAlign = System.Drawing.ContentAlignment.TopRight;
            this.chkIsCarryForwardExpiryApplicable.Checked = true;
            this.chkIsCarryForwardExpiryApplicable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsCarryForwardExpiryApplicable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkIsCarryForwardExpiryApplicable.Location = new System.Drawing.Point(26, 63);
            this.chkIsCarryForwardExpiryApplicable.Margin = new System.Windows.Forms.Padding(4);
            this.chkIsCarryForwardExpiryApplicable.Name = "chkIsCarryForwardExpiryApplicable";
            this.chkIsCarryForwardExpiryApplicable.Size = new System.Drawing.Size(244, 19);
            this.chkIsCarryForwardExpiryApplicable.TabIndex = 59;
            this.chkIsCarryForwardExpiryApplicable.Tag = "Is Carry Forward Expiry Applicable";
            this.chkIsCarryForwardExpiryApplicable.Text = "Is Carry Forward Expiry Applicable";
            this.chkIsCarryForwardExpiryApplicable.UseVisualStyleBackColor = true;
            this.chkIsCarryForwardExpiryApplicable.CheckedChanged += new System.EventHandler(this.chkIsCarryForwardExpiryApplicable_CheckedChanged);
            // 
            // grpIsEncashmentAllowed
            // 
            this.grpIsEncashmentAllowed.Controls.Add(this.chkIsEncashmentAtResignationAllowed);
            this.grpIsEncashmentAllowed.Controls.Add(this.chkIsEncashmentDuringEmploymentAllowed);
            this.grpIsEncashmentAllowed.Controls.Add(this.txtMaxLeaveDaysAllowedToEncash);
            this.grpIsEncashmentAllowed.Controls.Add(this.label6);
            this.grpIsEncashmentAllowed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpIsEncashmentAllowed.Location = new System.Drawing.Point(450, 137);
            this.grpIsEncashmentAllowed.Margin = new System.Windows.Forms.Padding(4);
            this.grpIsEncashmentAllowed.Name = "grpIsEncashmentAllowed";
            this.grpIsEncashmentAllowed.Padding = new System.Windows.Forms.Padding(4);
            this.grpIsEncashmentAllowed.Size = new System.Drawing.Size(424, 134);
            this.grpIsEncashmentAllowed.TabIndex = 56;
            this.grpIsEncashmentAllowed.TabStop = false;
            // 
            // chkIsEncashmentDuringEmploymentAllowed
            // 
            this.chkIsEncashmentDuringEmploymentAllowed.AutoSize = true;
            this.chkIsEncashmentDuringEmploymentAllowed.CheckAlign = System.Drawing.ContentAlignment.TopRight;
            this.chkIsEncashmentDuringEmploymentAllowed.Checked = true;
            this.chkIsEncashmentDuringEmploymentAllowed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsEncashmentDuringEmploymentAllowed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkIsEncashmentDuringEmploymentAllowed.Location = new System.Drawing.Point(8, 62);
            this.chkIsEncashmentDuringEmploymentAllowed.Margin = new System.Windows.Forms.Padding(4);
            this.chkIsEncashmentDuringEmploymentAllowed.Name = "chkIsEncashmentDuringEmploymentAllowed";
            this.chkIsEncashmentDuringEmploymentAllowed.Size = new System.Drawing.Size(304, 19);
            this.chkIsEncashmentDuringEmploymentAllowed.TabIndex = 59;
            this.chkIsEncashmentDuringEmploymentAllowed.Tag = "Is Encashment During Employment Allowed";
            this.chkIsEncashmentDuringEmploymentAllowed.Text = "Is Encashment During Employment Allowed";
            this.chkIsEncashmentDuringEmploymentAllowed.UseVisualStyleBackColor = true;
            // 
            // txtMaxLeaveDaysAllowedToEncash
            // 
            this.txtMaxLeaveDaysAllowedToEncash.Location = new System.Drawing.Point(296, 26);
            this.txtMaxLeaveDaysAllowedToEncash.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtMaxLeaveDaysAllowedToEncash.MaxLength = 255;
            this.txtMaxLeaveDaysAllowedToEncash.Multiline = true;
            this.txtMaxLeaveDaysAllowedToEncash.Name = "txtMaxLeaveDaysAllowedToEncash";
            this.txtMaxLeaveDaysAllowedToEncash.Size = new System.Drawing.Size(97, 25);
            this.txtMaxLeaveDaysAllowedToEncash.TabIndex = 55;
            this.txtMaxLeaveDaysAllowedToEncash.Tag = "Leave Code";
            this.txtMaxLeaveDaysAllowedToEncash.WordWrap = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(54, 31);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(240, 15);
            this.label6.TabIndex = 56;
            this.label6.Text = "Max. Leave Days Allowed To Encash";
            // 
            // chkIsEncashmentAllowed
            // 
            this.chkIsEncashmentAllowed.AutoSize = true;
            this.chkIsEncashmentAllowed.CheckAlign = System.Drawing.ContentAlignment.TopRight;
            this.chkIsEncashmentAllowed.Checked = true;
            this.chkIsEncashmentAllowed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsEncashmentAllowed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkIsEncashmentAllowed.Location = new System.Drawing.Point(470, 134);
            this.chkIsEncashmentAllowed.Margin = new System.Windows.Forms.Padding(4);
            this.chkIsEncashmentAllowed.Name = "chkIsEncashmentAllowed";
            this.chkIsEncashmentAllowed.Size = new System.Drawing.Size(174, 19);
            this.chkIsEncashmentAllowed.TabIndex = 54;
            this.chkIsEncashmentAllowed.Tag = "Is Encashment Allowed";
            this.chkIsEncashmentAllowed.Text = "Is Encashment Allowed";
            this.chkIsEncashmentAllowed.UseVisualStyleBackColor = true;
            this.chkIsEncashmentAllowed.CheckedChanged += new System.EventHandler(this.chkIsEncashmentAllowed_CheckedChanged);
            // 
            // chkIsEncashmentAtResignationAllowed
            // 
            this.chkIsEncashmentAtResignationAllowed.AutoSize = true;
            this.chkIsEncashmentAtResignationAllowed.CheckAlign = System.Drawing.ContentAlignment.TopRight;
            this.chkIsEncashmentAtResignationAllowed.Checked = true;
            this.chkIsEncashmentAtResignationAllowed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsEncashmentAtResignationAllowed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkIsEncashmentAtResignationAllowed.Location = new System.Drawing.Point(41, 89);
            this.chkIsEncashmentAtResignationAllowed.Margin = new System.Windows.Forms.Padding(4);
            this.chkIsEncashmentAtResignationAllowed.Name = "chkIsEncashmentAtResignationAllowed";
            this.chkIsEncashmentAtResignationAllowed.Size = new System.Drawing.Size(271, 19);
            this.chkIsEncashmentAtResignationAllowed.TabIndex = 60;
            this.chkIsEncashmentAtResignationAllowed.Tag = "Is Encashment At Resignation Allowed";
            this.chkIsEncashmentAtResignationAllowed.Text = "Is Encashment At Resignation Allowed";
            this.chkIsEncashmentAtResignationAllowed.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbAccrualFrequency);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.chkIsLeaveAccrualApplicable);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(19, 19);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(855, 52);
            this.groupBox3.TabIndex = 57;
            this.groupBox3.TabStop = false;
            // 
            // chkIsLeaveAccrualApplicable
            // 
            this.chkIsLeaveAccrualApplicable.AutoSize = true;
            this.chkIsLeaveAccrualApplicable.CheckAlign = System.Drawing.ContentAlignment.TopRight;
            this.chkIsLeaveAccrualApplicable.Checked = true;
            this.chkIsLeaveAccrualApplicable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsLeaveAccrualApplicable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkIsLeaveAccrualApplicable.Location = new System.Drawing.Point(18, 22);
            this.chkIsLeaveAccrualApplicable.Margin = new System.Windows.Forms.Padding(4);
            this.chkIsLeaveAccrualApplicable.Name = "chkIsLeaveAccrualApplicable";
            this.chkIsLeaveAccrualApplicable.Size = new System.Drawing.Size(201, 19);
            this.chkIsLeaveAccrualApplicable.TabIndex = 54;
            this.chkIsLeaveAccrualApplicable.Tag = "Is Leave Accrual Applicable";
            this.chkIsLeaveAccrualApplicable.Text = "Is Leave Accrual Applicable";
            this.chkIsLeaveAccrualApplicable.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.chkIsApplicableDuringNoticePeriod);
            this.groupBox6.Controls.Add(this.chkIsApplicableDuringProbation);
            this.groupBox6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(19, 74);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox6.Size = new System.Drawing.Size(855, 52);
            this.groupBox6.TabIndex = 58;
            this.groupBox6.TabStop = false;
            // 
            // chkIsApplicableDuringNoticePeriod
            // 
            this.chkIsApplicableDuringNoticePeriod.AutoSize = true;
            this.chkIsApplicableDuringNoticePeriod.CheckAlign = System.Drawing.ContentAlignment.TopRight;
            this.chkIsApplicableDuringNoticePeriod.Checked = true;
            this.chkIsApplicableDuringNoticePeriod.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsApplicableDuringNoticePeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkIsApplicableDuringNoticePeriod.Location = new System.Drawing.Point(278, 18);
            this.chkIsApplicableDuringNoticePeriod.Margin = new System.Windows.Forms.Padding(4);
            this.chkIsApplicableDuringNoticePeriod.Name = "chkIsApplicableDuringNoticePeriod";
            this.chkIsApplicableDuringNoticePeriod.Size = new System.Drawing.Size(246, 19);
            this.chkIsApplicableDuringNoticePeriod.TabIndex = 59;
            this.chkIsApplicableDuringNoticePeriod.Tag = "Is Applicable During Notice Period";
            this.chkIsApplicableDuringNoticePeriod.Text = "Is Applicable During Notice Period";
            this.chkIsApplicableDuringNoticePeriod.UseVisualStyleBackColor = true;
            // 
            // chkIsApplicableDuringProbation
            // 
            this.chkIsApplicableDuringProbation.AutoSize = true;
            this.chkIsApplicableDuringProbation.CheckAlign = System.Drawing.ContentAlignment.TopRight;
            this.chkIsApplicableDuringProbation.Checked = true;
            this.chkIsApplicableDuringProbation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsApplicableDuringProbation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkIsApplicableDuringProbation.Location = new System.Drawing.Point(18, 18);
            this.chkIsApplicableDuringProbation.Margin = new System.Windows.Forms.Padding(4);
            this.chkIsApplicableDuringProbation.Name = "chkIsApplicableDuringProbation";
            this.chkIsApplicableDuringProbation.Size = new System.Drawing.Size(221, 19);
            this.chkIsApplicableDuringProbation.TabIndex = 54;
            this.chkIsApplicableDuringProbation.Tag = "Is Applicable During Probation";
            this.chkIsApplicableDuringProbation.Text = "Is Applicable During Probation";
            this.chkIsApplicableDuringProbation.UseVisualStyleBackColor = true;
            // 
            // cmbAccrualFrequency
            // 
            this.cmbAccrualFrequency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccrualFrequency.DropDownWidth = 440;
            this.cmbAccrualFrequency.Location = new System.Drawing.Point(402, 18);
            this.cmbAccrualFrequency.Margin = new System.Windows.Forms.Padding(4);
            this.cmbAccrualFrequency.Name = "cmbAccrualFrequency";
            this.cmbAccrualFrequency.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbAccrualFrequency.Size = new System.Drawing.Size(199, 22);
            this.cmbAccrualFrequency.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbAccrualFrequency.TabIndex = 56;
            this.cmbAccrualFrequency.Tag = "Accrual Frequency";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(275, 22);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 15);
            this.label5.TabIndex = 55;
            this.label5.Text = "Accrual Frequency";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkIsCarryForwardAllowed
            // 
            this.chkIsCarryForwardAllowed.AutoSize = true;
            this.chkIsCarryForwardAllowed.CheckAlign = System.Drawing.ContentAlignment.TopRight;
            this.chkIsCarryForwardAllowed.Checked = true;
            this.chkIsCarryForwardAllowed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsCarryForwardAllowed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkIsCarryForwardAllowed.Location = new System.Drawing.Point(37, 133);
            this.chkIsCarryForwardAllowed.Margin = new System.Windows.Forms.Padding(4);
            this.chkIsCarryForwardAllowed.Name = "chkIsCarryForwardAllowed";
            this.chkIsCarryForwardAllowed.Size = new System.Drawing.Size(180, 19);
            this.chkIsCarryForwardAllowed.TabIndex = 59;
            this.chkIsCarryForwardAllowed.Tag = "Is Carry forward allowed";
            this.chkIsCarryForwardAllowed.Text = "Is Carry forward allowed";
            this.chkIsCarryForwardAllowed.UseVisualStyleBackColor = true;
            this.chkIsCarryForwardAllowed.CheckedChanged += new System.EventHandler(this.chkIsCarryForwardAllowed_CheckedChanged);
            // 
            // frmLeaveTypeMaster
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(944, 627);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLeaveTypeMaster";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Leave Type List";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmLeaveTypeMaster_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLeaveTypeMaster_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbIsActive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLeavePaid)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpIsCarryForwardAllowed.ResumeLayout(false);
            this.grpIsCarryForwardAllowed.PerformLayout();
            this.grpIsEncashmentAllowed.ResumeLayout(false);
            this.grpIsEncashmentAllowed.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbAccrualFrequency)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private StaffsyncDBDTSet staffsyncDBDTSet;
        private System.Windows.Forms.BindingSource empMasInfoBindingSource;
        private StaffsyncDBDTSetTableAdapters.EmpMasInfoTableAdapter empMasInfoTableAdapter;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtLeaveCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtLeaveTitle;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblLeaveTypeID;
        public System.Windows.Forms.Label lblActionMode;
        private System.Windows.Forms.ErrorProvider errValidator;
        private Krypton.Toolkit.KryptonButton btnCloseMe;
        private Krypton.Toolkit.KryptonButton btnRemoveDetails;
        private Krypton.Toolkit.KryptonButton btnSaveDetails;
        private Krypton.Toolkit.KryptonButton btnModifyDetails;
        private Krypton.Toolkit.KryptonButton btnGenerateDetails;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private Krypton.Toolkit.KryptonButton btnSearch;
        private System.Windows.Forms.Label label2;
        private Krypton.Toolkit.KryptonComboBox cmbIsActive;
        private Krypton.Toolkit.KryptonComboBox cmbLeavePaid;
        private Krypton.Toolkit.KryptonLinkLabel lnkViewAuditLog;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox grpIsCarryForwardAllowed;
        private System.Windows.Forms.TextBox txtMaxDaysCarryForwardAllowed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCarryForwardExpiryMonths;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkIsCarryForwardExpiryApplicable;
        private System.Windows.Forms.CheckBox chkIsEncashmentAllowed;
        private System.Windows.Forms.GroupBox grpIsEncashmentAllowed;
        private System.Windows.Forms.CheckBox chkIsEncashmentDuringEmploymentAllowed;
        private System.Windows.Forms.TextBox txtMaxLeaveDaysAllowedToEncash;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkIsEncashmentAtResignationAllowed;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkIsLeaveAccrualApplicable;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox chkIsApplicableDuringNoticePeriod;
        private System.Windows.Forms.CheckBox chkIsApplicableDuringProbation;
        private Krypton.Toolkit.KryptonComboBox cmbAccrualFrequency;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkIsCarryForwardAllowed;
    }
}