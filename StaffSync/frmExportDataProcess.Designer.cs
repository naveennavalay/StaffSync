namespace StaffSync
{
    partial class frmExportDataProcess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExportDataProcess));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grpMoreOptions = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.chkQualSkillsInfo = new System.Windows.Forms.CheckBox();
            this.chkInsuredMemberInfo = new System.Windows.Forms.CheckBox();
            this.chkNomineeInfo = new System.Windows.Forms.CheckBox();
            this.chkFamilyMembersInfo = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label34 = new System.Windows.Forms.Label();
            this.dtgImportDataSourceList = new Krypton.Toolkit.KryptonDataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cmbOutputFormat = new Krypton.Toolkit.KryptonComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.txtCompLogo = new Krypton.Toolkit.KryptonTextBox();
            this.picCompLogo = new System.Windows.Forms.PictureBox();
            this.lblSelectedDataAction = new System.Windows.Forms.Label();
            this.chkSelectOrUnselect = new Krypton.Toolkit.KryptonCheckButton();
            this.btnSearch = new Krypton.Toolkit.KryptonButton();
            this.txtSourceFilePath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTotalNotImportedRows = new System.Windows.Forms.Label();
            this.lblTotalDuplicateRows = new System.Windows.Forms.Label();
            this.lblTotalImportedRows = new System.Windows.Forms.Label();
            this.lblTotalRowsSelected = new System.Windows.Forms.Label();
            this.lblTotalRowsFromSource = new System.Windows.Forms.Label();
            this.dtgImportDataPreview = new Krypton.Toolkit.KryptonDataGridView();
            this.lnkViewAuditLog = new Krypton.Toolkit.KryptonLinkLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCloseMe = new Krypton.Toolkit.KryptonButton();
            this.btnRemoveDetails = new Krypton.Toolkit.KryptonButton();
            this.btnSaveDetails = new Krypton.Toolkit.KryptonButton();
            this.btnModifyDetails = new Krypton.Toolkit.KryptonButton();
            this.chkAvoidDuplicateRows = new System.Windows.Forms.CheckBox();
            this.btnGenerateDetails = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.errValidator = new System.Windows.Forms.ErrorProvider(this.components);
            this.staffsyncDBDataSet1 = new StaffSync.StaffsyncDBDataSet1();
            this.qryAllEmpLeavePendingStatementBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qryAllEmpLeavePendingStatementTableAdapter = new StaffSync.StaffsyncDBDataSet1TableAdapters.qryAllEmpLeavePendingStatementTableAdapter();
            this.empMasInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.staffsyncDBDTSet = new StaffSync.StaffsyncDBDTSet();
            this.empMasInfoTableAdapter = new StaffSync.StaffsyncDBDTSetTableAdapters.EmpMasInfoTableAdapter();
            this.chkReimbStatements = new System.Windows.Forms.CheckBox();
            this.chkAssetsInformation = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.grpMoreOptions.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgImportDataSourceList)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOutputFormat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCompLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgImportDataPreview)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryAllEmpLeavePendingStatementBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(1203, 728);
            this.splitContainer1.SplitterDistance = 653;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.grpMoreOptions);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1203, 653);
            this.panel1.TabIndex = 1;
            // 
            // grpMoreOptions
            // 
            this.grpMoreOptions.Controls.Add(this.chkAssetsInformation);
            this.grpMoreOptions.Controls.Add(this.chkReimbStatements);
            this.grpMoreOptions.Controls.Add(this.checkBox1);
            this.grpMoreOptions.Controls.Add(this.checkBox2);
            this.grpMoreOptions.Controls.Add(this.checkBox3);
            this.grpMoreOptions.Controls.Add(this.checkBox4);
            this.grpMoreOptions.Controls.Add(this.chkQualSkillsInfo);
            this.grpMoreOptions.Controls.Add(this.chkInsuredMemberInfo);
            this.grpMoreOptions.Controls.Add(this.chkNomineeInfo);
            this.grpMoreOptions.Controls.Add(this.chkFamilyMembersInfo);
            this.grpMoreOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpMoreOptions.Location = new System.Drawing.Point(10, 506);
            this.grpMoreOptions.Margin = new System.Windows.Forms.Padding(4);
            this.grpMoreOptions.Name = "grpMoreOptions";
            this.grpMoreOptions.Padding = new System.Windows.Forms.Padding(4);
            this.grpMoreOptions.Size = new System.Drawing.Size(1175, 126);
            this.grpMoreOptions.TabIndex = 67;
            this.grpMoreOptions.TabStop = false;
            this.grpMoreOptions.Text = "More Options";
            this.grpMoreOptions.Visible = false;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.checkBox1.Location = new System.Drawing.Point(290, 100);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(227, 19);
            this.checkBox1.TabIndex = 92;
            this.checkBox1.Tag = "Qualification & Skills Information";
            this.checkBox1.Text = "Qualification & Skills Information";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.checkBox2.Location = new System.Drawing.Point(366, 75);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(151, 19);
            this.checkBox2.TabIndex = 91;
            this.checkBox2.Tag = "Holiday Information";
            this.checkBox2.Text = "Holiday Information";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.checkBox3.Location = new System.Drawing.Point(422, 50);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(95, 19);
            this.checkBox3.TabIndex = 90;
            this.checkBox3.Tag = "Salary Slip";
            this.checkBox3.Text = "Salary Slip";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox4.Checked = true;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.checkBox4.Location = new System.Drawing.Point(343, 25);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(174, 19);
            this.checkBox4.TabIndex = 89;
            this.checkBox4.Tag = "Attendance Information";
            this.checkBox4.Text = "Attendance Information";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // chkQualSkillsInfo
            // 
            this.chkQualSkillsInfo.AutoSize = true;
            this.chkQualSkillsInfo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkQualSkillsInfo.Checked = true;
            this.chkQualSkillsInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkQualSkillsInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkQualSkillsInfo.Location = new System.Drawing.Point(13, 100);
            this.chkQualSkillsInfo.Name = "chkQualSkillsInfo";
            this.chkQualSkillsInfo.Size = new System.Drawing.Size(227, 19);
            this.chkQualSkillsInfo.TabIndex = 88;
            this.chkQualSkillsInfo.Tag = "Qualification & Skills Information";
            this.chkQualSkillsInfo.Text = "Qualification & Skills Information";
            this.chkQualSkillsInfo.UseVisualStyleBackColor = true;
            // 
            // chkInsuredMemberInfo
            // 
            this.chkInsuredMemberInfo.AutoSize = true;
            this.chkInsuredMemberInfo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkInsuredMemberInfo.Checked = true;
            this.chkInsuredMemberInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInsuredMemberInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkInsuredMemberInfo.Location = new System.Drawing.Point(60, 75);
            this.chkInsuredMemberInfo.Name = "chkInsuredMemberInfo";
            this.chkInsuredMemberInfo.Size = new System.Drawing.Size(180, 19);
            this.chkInsuredMemberInfo.TabIndex = 87;
            this.chkInsuredMemberInfo.Tag = "Insured Member Details";
            this.chkInsuredMemberInfo.Text = "Insured Member Details";
            this.chkInsuredMemberInfo.UseVisualStyleBackColor = true;
            // 
            // chkNomineeInfo
            // 
            this.chkNomineeInfo.AutoSize = true;
            this.chkNomineeInfo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkNomineeInfo.Checked = true;
            this.chkNomineeInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNomineeInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkNomineeInfo.Location = new System.Drawing.Point(79, 50);
            this.chkNomineeInfo.Name = "chkNomineeInfo";
            this.chkNomineeInfo.Size = new System.Drawing.Size(161, 19);
            this.chkNomineeInfo.TabIndex = 86;
            this.chkNomineeInfo.Tag = "Nominee Information";
            this.chkNomineeInfo.Text = "Nominee Information";
            this.chkNomineeInfo.UseVisualStyleBackColor = true;
            // 
            // chkFamilyMembersInfo
            // 
            this.chkFamilyMembersInfo.AutoSize = true;
            this.chkFamilyMembersInfo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkFamilyMembersInfo.Checked = true;
            this.chkFamilyMembersInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFamilyMembersInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkFamilyMembersInfo.Location = new System.Drawing.Point(31, 25);
            this.chkFamilyMembersInfo.Name = "chkFamilyMembersInfo";
            this.chkFamilyMembersInfo.Size = new System.Drawing.Size(209, 19);
            this.chkFamilyMembersInfo.TabIndex = 85;
            this.chkFamilyMembersInfo.Tag = "Family Members Information";
            this.chkFamilyMembersInfo.Text = "Family Members Information";
            this.chkFamilyMembersInfo.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupBox5.Controls.Add(this.groupBox1);
            this.groupBox5.Controls.Add(this.groupBox4);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(10, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1175, 487);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label34);
            this.groupBox1.Controls.Add(this.dtgImportDataSourceList);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(339, 459);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Export Data Action";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.BackColor = System.Drawing.Color.Transparent;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label34.Location = new System.Drawing.Point(8, 432);
            this.label34.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(321, 15);
            this.label34.TabIndex = 65;
            this.label34.Text = "🟢 Double-click to preview before export the data";
            // 
            // dtgImportDataSourceList
            // 
            this.dtgImportDataSourceList.AllowUserToAddRows = false;
            this.dtgImportDataSourceList.AllowUserToDeleteRows = false;
            this.dtgImportDataSourceList.AllowUserToResizeRows = false;
            this.dtgImportDataSourceList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgImportDataSourceList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgImportDataSourceList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dtgImportDataSourceList.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dtgImportDataSourceList.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.ContextMenuItemImage;
            this.dtgImportDataSourceList.Location = new System.Drawing.Point(11, 25);
            this.dtgImportDataSourceList.Name = "dtgImportDataSourceList";
            this.dtgImportDataSourceList.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtgImportDataSourceList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgImportDataSourceList.Size = new System.Drawing.Size(318, 401);
            this.dtgImportDataSourceList.TabIndex = 64;
            this.dtgImportDataSourceList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgImportDataSourceList_CellDoubleClick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cmbOutputFormat);
            this.groupBox4.Controls.Add(this.label28);
            this.groupBox4.Controls.Add(this.txtCompLogo);
            this.groupBox4.Controls.Add(this.picCompLogo);
            this.groupBox4.Controls.Add(this.lblSelectedDataAction);
            this.groupBox4.Controls.Add(this.chkSelectOrUnselect);
            this.groupBox4.Controls.Add(this.btnSearch);
            this.groupBox4.Controls.Add(this.txtSourceFilePath);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.lblTotalNotImportedRows);
            this.groupBox4.Controls.Add(this.lblTotalDuplicateRows);
            this.groupBox4.Controls.Add(this.lblTotalImportedRows);
            this.groupBox4.Controls.Add(this.lblTotalRowsSelected);
            this.groupBox4.Controls.Add(this.lblTotalRowsFromSource);
            this.groupBox4.Controls.Add(this.dtgImportDataPreview);
            this.groupBox4.Controls.Add(this.lnkViewAuditLog);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(359, 18);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(806, 459);
            this.groupBox4.TabIndex = 65;
            this.groupBox4.TabStop = false;
            // 
            // cmbOutputFormat
            // 
            this.cmbOutputFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOutputFormat.DropDownWidth = 440;
            this.cmbOutputFormat.Location = new System.Drawing.Point(87, 380);
            this.cmbOutputFormat.Name = "cmbOutputFormat";
            this.cmbOutputFormat.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbOutputFormat.Size = new System.Drawing.Size(190, 22);
            this.cmbOutputFormat.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbOutputFormat.TabIndex = 91;
            this.cmbOutputFormat.Tag = "Gender";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(29, 384);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(52, 15);
            this.label28.TabIndex = 90;
            this.label28.Text = "Format";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCompLogo
            // 
            this.txtCompLogo.Location = new System.Drawing.Point(517, 408);
            this.txtCompLogo.Multiline = true;
            this.txtCompLogo.Name = "txtCompLogo";
            this.txtCompLogo.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtCompLogo.Size = new System.Drawing.Size(101, 28);
            this.txtCompLogo.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompLogo.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtCompLogo.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompLogo.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompLogo.TabIndex = 89;
            this.txtCompLogo.Visible = false;
            this.txtCompLogo.WordWrap = false;
            // 
            // picCompLogo
            // 
            this.picCompLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCompLogo.Location = new System.Drawing.Point(621, 397);
            this.picCompLogo.Margin = new System.Windows.Forms.Padding(4);
            this.picCompLogo.Name = "picCompLogo";
            this.picCompLogo.Size = new System.Drawing.Size(50, 39);
            this.picCompLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCompLogo.TabIndex = 88;
            this.picCompLogo.TabStop = false;
            this.picCompLogo.Visible = false;
            // 
            // lblSelectedDataAction
            // 
            this.lblSelectedDataAction.AutoSize = true;
            this.lblSelectedDataAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblSelectedDataAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedDataAction.Location = new System.Drawing.Point(384, 426);
            this.lblSelectedDataAction.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSelectedDataAction.Name = "lblSelectedDataAction";
            this.lblSelectedDataAction.Size = new System.Drawing.Size(11, 15);
            this.lblSelectedDataAction.TabIndex = 87;
            this.lblSelectedDataAction.Text = " ";
            this.lblSelectedDataAction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSelectedDataAction.Visible = false;
            // 
            // chkSelectOrUnselect
            // 
            this.chkSelectOrUnselect.Enabled = false;
            this.chkSelectOrUnselect.Location = new System.Drawing.Point(106, 21);
            this.chkSelectOrUnselect.Name = "chkSelectOrUnselect";
            this.chkSelectOrUnselect.Size = new System.Drawing.Size(87, 15);
            this.chkSelectOrUnselect.TabIndex = 86;
            this.chkSelectOrUnselect.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.chkSelectOrUnselect.Values.Text = "Unselect";
            this.chkSelectOrUnselect.Click += new System.EventHandler(this.chkSelectOrUnselect_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(672, 414);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSearch.Size = new System.Drawing.Size(39, 28);
            this.btnSearch.TabIndex = 74;
            this.btnSearch.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSearch.Values.Image = global::StaffSync.Properties.Resources.search;
            this.btnSearch.Values.Text = "";
            this.btnSearch.Visible = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSourceFilePath
            // 
            this.txtSourceFilePath.Location = new System.Drawing.Point(87, 415);
            this.txtSourceFilePath.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSourceFilePath.MaxLength = 255;
            this.txtSourceFilePath.Multiline = true;
            this.txtSourceFilePath.Name = "txtSourceFilePath";
            this.txtSourceFilePath.ReadOnly = true;
            this.txtSourceFilePath.Size = new System.Drawing.Size(576, 28);
            this.txtSourceFilePath.TabIndex = 72;
            this.txtSourceFilePath.Tag = "Asset Code ";
            this.txtSourceFilePath.Visible = false;
            this.txtSourceFilePath.WordWrap = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(4, 421);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 15);
            this.label6.TabIndex = 73;
            this.label6.Text = "Output File";
            this.label6.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 21);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 15);
            this.label5.TabIndex = 71;
            this.label5.Text = "Data Preview";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalNotImportedRows
            // 
            this.lblTotalNotImportedRows.AutoSize = true;
            this.lblTotalNotImportedRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalNotImportedRows.Location = new System.Drawing.Point(610, 346);
            this.lblTotalNotImportedRows.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTotalNotImportedRows.Name = "lblTotalNotImportedRows";
            this.lblTotalNotImportedRows.Size = new System.Drawing.Size(149, 15);
            this.lblTotalNotImportedRows.TabIndex = 70;
            this.lblTotalNotImportedRows.Text = "Not Imported Rows : 0";
            this.lblTotalNotImportedRows.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalDuplicateRows
            // 
            this.lblTotalDuplicateRows.AutoSize = true;
            this.lblTotalDuplicateRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalDuplicateRows.Location = new System.Drawing.Point(405, 346);
            this.lblTotalDuplicateRows.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTotalDuplicateRows.Name = "lblTotalDuplicateRows";
            this.lblTotalDuplicateRows.Size = new System.Drawing.Size(163, 15);
            this.lblTotalDuplicateRows.TabIndex = 69;
            this.lblTotalDuplicateRows.Text = "Total Duplicate Rows : 0";
            this.lblTotalDuplicateRows.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalImportedRows
            // 
            this.lblTotalImportedRows.AutoSize = true;
            this.lblTotalImportedRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalImportedRows.Location = new System.Drawing.Point(204, 346);
            this.lblTotalImportedRows.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTotalImportedRows.Name = "lblTotalImportedRows";
            this.lblTotalImportedRows.Size = new System.Drawing.Size(159, 15);
            this.lblTotalImportedRows.TabIndex = 68;
            this.lblTotalImportedRows.Text = "Total Imported Rows : 0";
            this.lblTotalImportedRows.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalRowsSelected
            // 
            this.lblTotalRowsSelected.AutoSize = true;
            this.lblTotalRowsSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRowsSelected.Location = new System.Drawing.Point(4, 346);
            this.lblTotalRowsSelected.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTotalRowsSelected.Name = "lblTotalRowsSelected";
            this.lblTotalRowsSelected.Size = new System.Drawing.Size(158, 15);
            this.lblTotalRowsSelected.TabIndex = 67;
            this.lblTotalRowsSelected.Text = "Total Rows Selected : 0";
            this.lblTotalRowsSelected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalRowsFromSource
            // 
            this.lblTotalRowsFromSource.AutoSize = true;
            this.lblTotalRowsFromSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRowsFromSource.Location = new System.Drawing.Point(539, 21);
            this.lblTotalRowsFromSource.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTotalRowsFromSource.Name = "lblTotalRowsFromSource";
            this.lblTotalRowsFromSource.Size = new System.Drawing.Size(180, 15);
            this.lblTotalRowsFromSource.TabIndex = 66;
            this.lblTotalRowsFromSource.Text = "Total Rows from Source : 0";
            this.lblTotalRowsFromSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtgImportDataPreview
            // 
            this.dtgImportDataPreview.AllowUserToAddRows = false;
            this.dtgImportDataPreview.AllowUserToDeleteRows = false;
            this.dtgImportDataPreview.AllowUserToResizeRows = false;
            this.dtgImportDataPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgImportDataPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgImportDataPreview.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dtgImportDataPreview.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dtgImportDataPreview.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.ContextMenuItemImage;
            this.dtgImportDataPreview.Location = new System.Drawing.Point(7, 39);
            this.dtgImportDataPreview.Name = "dtgImportDataPreview";
            this.dtgImportDataPreview.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtgImportDataPreview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgImportDataPreview.Size = new System.Drawing.Size(790, 291);
            this.dtgImportDataPreview.StateCommon.Background.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.dtgImportDataPreview.StateCommon.Background.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.dtgImportDataPreview.StateCommon.BackStyle = Krypton.Toolkit.PaletteBackStyle.ContextMenuItemImage;
            this.dtgImportDataPreview.StateDisabled.Background.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.dtgImportDataPreview.StateDisabled.Background.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.dtgImportDataPreview.StateNormal.Background.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.dtgImportDataPreview.StateNormal.Background.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.dtgImportDataPreview.TabIndex = 65;
            // 
            // lnkViewAuditLog
            // 
            this.lnkViewAuditLog.Location = new System.Drawing.Point(726, 417);
            this.lnkViewAuditLog.Name = "lnkViewAuditLog";
            this.lnkViewAuditLog.Size = new System.Drawing.Size(93, 20);
            this.lnkViewAuditLog.TabIndex = 37;
            this.lnkViewAuditLog.Values.Text = "View Audit Log";
            this.lnkViewAuditLog.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel2.Controls.Add(this.btnCloseMe);
            this.panel2.Controls.Add(this.btnRemoveDetails);
            this.panel2.Controls.Add(this.btnSaveDetails);
            this.panel2.Controls.Add(this.btnModifyDetails);
            this.panel2.Controls.Add(this.chkAvoidDuplicateRows);
            this.panel2.Controls.Add(this.btnGenerateDetails);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1203, 71);
            this.panel2.TabIndex = 1;
            // 
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(1040, 6);
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
            this.btnRemoveDetails.Location = new System.Drawing.Point(152, 6);
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
            this.btnSaveDetails.Location = new System.Drawing.Point(20, 6);
            this.btnSaveDetails.Name = "btnSaveDetails";
            this.btnSaveDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSaveDetails.Size = new System.Drawing.Size(126, 38);
            this.btnSaveDetails.TabIndex = 18;
            this.btnSaveDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSaveDetails.Values.Image = global::StaffSync.Properties.Resources.export;
            this.btnSaveDetails.Values.Text = "Export Data";
            this.btnSaveDetails.Click += new System.EventHandler(this.btnSaveDetails_Click);
            // 
            // btnModifyDetails
            // 
            this.btnModifyDetails.Location = new System.Drawing.Point(284, 6);
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
            // chkAvoidDuplicateRows
            // 
            this.chkAvoidDuplicateRows.AutoSize = true;
            this.chkAvoidDuplicateRows.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAvoidDuplicateRows.Checked = true;
            this.chkAvoidDuplicateRows.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAvoidDuplicateRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkAvoidDuplicateRows.Location = new System.Drawing.Point(525, 0);
            this.chkAvoidDuplicateRows.Name = "chkAvoidDuplicateRows";
            this.chkAvoidDuplicateRows.Size = new System.Drawing.Size(164, 19);
            this.chkAvoidDuplicateRows.TabIndex = 85;
            this.chkAvoidDuplicateRows.Tag = "Avoid Duplicate Rows";
            this.chkAvoidDuplicateRows.Text = "Avoid Duplicate Rows";
            this.chkAvoidDuplicateRows.UseVisualStyleBackColor = true;
            this.chkAvoidDuplicateRows.Visible = false;
            // 
            // btnGenerateDetails
            // 
            this.btnGenerateDetails.Location = new System.Drawing.Point(284, 6);
            this.btnGenerateDetails.Name = "btnGenerateDetails";
            this.btnGenerateDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnGenerateDetails.Size = new System.Drawing.Size(126, 38);
            this.btnGenerateDetails.TabIndex = 16;
            this.btnGenerateDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnGenerateDetails.Values.Image = global::StaffSync.Properties.Resources._new;
            this.btnGenerateDetails.Values.Text = "Generate";
            this.btnGenerateDetails.Visible = false;
            this.btnGenerateDetails.Click += new System.EventHandler(this.btnGenerateDetails_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(284, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCancel.Size = new System.Drawing.Size(126, 38);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Image = global::StaffSync.Properties.Resources.cancel;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errValidator
            // 
            this.errValidator.ContainerControl = this;
            // 
            // staffsyncDBDataSet1
            // 
            this.staffsyncDBDataSet1.DataSetName = "StaffsyncDBDataSet1";
            this.staffsyncDBDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // qryAllEmpLeavePendingStatementBindingSource
            // 
            this.qryAllEmpLeavePendingStatementBindingSource.DataMember = "qryAllEmpLeavePendingStatement";
            this.qryAllEmpLeavePendingStatementBindingSource.DataSource = this.staffsyncDBDataSet1;
            // 
            // qryAllEmpLeavePendingStatementTableAdapter
            // 
            this.qryAllEmpLeavePendingStatementTableAdapter.ClearBeforeFill = true;
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
            // chkReimbStatements
            // 
            this.chkReimbStatements.AutoSize = true;
            this.chkReimbStatements.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkReimbStatements.Checked = true;
            this.chkReimbStatements.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkReimbStatements.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkReimbStatements.Location = new System.Drawing.Point(618, 25);
            this.chkReimbStatements.Name = "chkReimbStatements";
            this.chkReimbStatements.Size = new System.Drawing.Size(196, 19);
            this.chkReimbStatements.TabIndex = 93;
            this.chkReimbStatements.Tag = "Reimbursment Statements";
            this.chkReimbStatements.Text = "Reimbursment Statements";
            this.chkReimbStatements.UseVisualStyleBackColor = true;
            // 
            // chkAssetsInformation
            // 
            this.chkAssetsInformation.AutoSize = true;
            this.chkAssetsInformation.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAssetsInformation.Checked = true;
            this.chkAssetsInformation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAssetsInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkAssetsInformation.Location = new System.Drawing.Point(670, 50);
            this.chkAssetsInformation.Name = "chkAssetsInformation";
            this.chkAssetsInformation.Size = new System.Drawing.Size(144, 19);
            this.chkAssetsInformation.TabIndex = 94;
            this.chkAssetsInformation.Tag = "Assets Information";
            this.chkAssetsInformation.Text = "Assets Information";
            this.chkAssetsInformation.UseVisualStyleBackColor = true;
            // 
            // frmExportDataProcess
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1195, 728);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExportDataProcess";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Daily Attendance Sheet";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.frmExportDataProcess_Activated);
            this.Load += new System.EventHandler(this.frmExportDataProcess_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmExportDataProcess_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.grpMoreOptions.ResumeLayout(false);
            this.grpMoreOptions.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgImportDataSourceList)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOutputFormat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCompLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgImportDataPreview)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryAllEmpLeavePendingStatementBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private StaffsyncDBDTSet staffsyncDBDTSet;
        private System.Windows.Forms.BindingSource empMasInfoBindingSource;
        private StaffsyncDBDTSetTableAdapters.EmpMasInfoTableAdapter empMasInfoTableAdapter;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ErrorProvider errValidator;
        private Krypton.Toolkit.KryptonButton btnCloseMe;
        private Krypton.Toolkit.KryptonButton btnRemoveDetails;
        private Krypton.Toolkit.KryptonButton btnSaveDetails;
        private Krypton.Toolkit.KryptonButton btnModifyDetails;
        private Krypton.Toolkit.KryptonButton btnGenerateDetails;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private StaffsyncDBDataSet1 staffsyncDBDataSet1;
        private System.Windows.Forms.BindingSource qryAllEmpLeavePendingStatementBindingSource;
        private StaffsyncDBDataSet1TableAdapters.qryAllEmpLeavePendingStatementTableAdapter qryAllEmpLeavePendingStatementTableAdapter;
        private System.Windows.Forms.GroupBox groupBox1;
        private Krypton.Toolkit.KryptonDataGridView dtgImportDataSourceList;
        private System.Windows.Forms.GroupBox groupBox4;
        private Krypton.Toolkit.KryptonLinkLabel lnkViewAuditLog;
        private Krypton.Toolkit.KryptonDataGridView dtgImportDataPreview;
        private System.Windows.Forms.Label lblTotalNotImportedRows;
        private System.Windows.Forms.Label lblTotalDuplicateRows;
        private System.Windows.Forms.Label lblTotalImportedRows;
        private System.Windows.Forms.Label lblTotalRowsSelected;
        private System.Windows.Forms.Label lblTotalRowsFromSource;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSourceFilePath;
        private System.Windows.Forms.Label label6;
        private Krypton.Toolkit.KryptonButton btnSearch;
        private System.Windows.Forms.CheckBox chkAvoidDuplicateRows;
        private Krypton.Toolkit.KryptonCheckButton chkSelectOrUnselect;
        private System.Windows.Forms.Label lblSelectedDataAction;
        private System.Windows.Forms.PictureBox picCompLogo;
        private Krypton.Toolkit.KryptonTextBox txtCompLogo;
        private Krypton.Toolkit.KryptonComboBox cmbOutputFormat;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.GroupBox grpMoreOptions;
        private System.Windows.Forms.CheckBox chkQualSkillsInfo;
        private System.Windows.Forms.CheckBox chkInsuredMemberInfo;
        private System.Windows.Forms.CheckBox chkNomineeInfo;
        private System.Windows.Forms.CheckBox chkFamilyMembersInfo;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.CheckBox chkAssetsInformation;
        private System.Windows.Forms.CheckBox chkReimbStatements;
    }
}