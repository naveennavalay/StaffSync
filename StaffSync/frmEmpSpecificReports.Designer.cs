namespace StaffSync
{
    partial class frmEmpSpecificReports
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmpSpecificReports));
            this.errValidator = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.dtgReportsList = new Krypton.Toolkit.KryptonDataGridView();
            this.grpCommon = new System.Windows.Forms.GroupBox();
            this.cmbBloodGroup = new Krypton.Toolkit.KryptonComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.chkBloodGroup = new System.Windows.Forms.CheckBox();
            this.cmbFreeSearchAttributeName = new Krypton.Toolkit.KryptonComboBox();
            this.chkIncludeMonth = new System.Windows.Forms.CheckBox();
            this.cmbGender = new Krypton.Toolkit.KryptonComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSearch = new Krypton.Toolkit.KryptonTextBox();
            this.cmbDepartment = new Krypton.Toolkit.KryptonComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDTTo = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDTFrom = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.optDOB = new Krypton.Toolkit.KryptonRadioButton();
            this.optResignationDate = new Krypton.Toolkit.KryptonRadioButton();
            this.optRelivingDate = new Krypton.Toolkit.KryptonRadioButton();
            this.optConfirmDate = new Krypton.Toolkit.KryptonRadioButton();
            this.optProbDate = new Krypton.Toolkit.KryptonRadioButton();
            this.optDOJ = new Krypton.Toolkit.KryptonRadioButton();
            this.cmbBranch = new Krypton.Toolkit.KryptonComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDesignation = new Krypton.Toolkit.KryptonComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbMonth = new Krypton.Toolkit.KryptonComboBox();
            this.label42 = new System.Windows.Forms.Label();
            this.chkIncludeBranch = new System.Windows.Forms.CheckBox();
            this.chkIncludeGender = new System.Windows.Forms.CheckBox();
            this.chkIncludeDepartment = new System.Windows.Forms.CheckBox();
            this.chkIncludeDesignation = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgDataResult = new Krypton.Toolkit.KryptonDataGridView();
            this.empMasInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.staffsyncDBDTSet = new StaffSync.StaffsyncDBDTSet();
            this.empMasInfoTableAdapter = new StaffSync.StaffsyncDBDTSetTableAdapters.EmpMasInfoTableAdapter();
            this.btnExecute = new Krypton.Toolkit.KryptonButton();
            this.btnExport = new Krypton.Toolkit.KryptonButton();
            this.lblSelectedReport = new System.Windows.Forms.Label();
            this.lblSelectedReportName = new System.Windows.Forms.Label();
            this.lblFilter = new System.Windows.Forms.Label();
            this.btnReset = new Krypton.Toolkit.KryptonButton();
            this.cmbCriteriaOperator = new Krypton.Toolkit.KryptonComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgReportsList)).BeginInit();
            this.grpCommon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBloodGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFreeSearchAttributeName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGender)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDepartment)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDesignation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonth)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDataResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCriteriaOperator)).BeginInit();
            this.SuspendLayout();
            // 
            // errValidator
            // 
            this.errValidator.ContainerControl = this;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.dtgReportsList);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(11, 13);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(393, 745);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Reports List";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(8, 711);
            this.label17.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(375, 30);
            this.label17.TabIndex = 103;
            this.label17.Text = "💡Double-click to view Report Filter Option";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtgReportsList
            // 
            this.dtgReportsList.AllowUserToAddRows = false;
            this.dtgReportsList.AllowUserToDeleteRows = false;
            this.dtgReportsList.AllowUserToResizeRows = false;
            this.dtgReportsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dtgReportsList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgReportsList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dtgReportsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgReportsList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dtgReportsList.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dtgReportsList.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.ButtonLowProfile;
            this.dtgReportsList.Location = new System.Drawing.Point(8, 22);
            this.dtgReportsList.MultiSelect = false;
            this.dtgReportsList.Name = "dtgReportsList";
            this.dtgReportsList.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtgReportsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgReportsList.Size = new System.Drawing.Size(375, 686);
            this.dtgReportsList.TabIndex = 0;
            this.dtgReportsList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgReportsList_CellDoubleClick);
            // 
            // grpCommon
            // 
            this.grpCommon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCommon.Controls.Add(this.cmbCriteriaOperator);
            this.grpCommon.Controls.Add(this.cmbBloodGroup);
            this.grpCommon.Controls.Add(this.label9);
            this.grpCommon.Controls.Add(this.chkBloodGroup);
            this.grpCommon.Controls.Add(this.cmbFreeSearchAttributeName);
            this.grpCommon.Controls.Add(this.chkIncludeMonth);
            this.grpCommon.Controls.Add(this.cmbGender);
            this.grpCommon.Controls.Add(this.label7);
            this.grpCommon.Controls.Add(this.label8);
            this.grpCommon.Controls.Add(this.txtSearch);
            this.grpCommon.Controls.Add(this.cmbDepartment);
            this.grpCommon.Controls.Add(this.label6);
            this.grpCommon.Controls.Add(this.groupBox2);
            this.grpCommon.Controls.Add(this.cmbBranch);
            this.grpCommon.Controls.Add(this.label4);
            this.grpCommon.Controls.Add(this.cmbDesignation);
            this.grpCommon.Controls.Add(this.label2);
            this.grpCommon.Controls.Add(this.cmbMonth);
            this.grpCommon.Controls.Add(this.label42);
            this.grpCommon.Controls.Add(this.chkIncludeBranch);
            this.grpCommon.Controls.Add(this.chkIncludeGender);
            this.grpCommon.Controls.Add(this.chkIncludeDepartment);
            this.grpCommon.Controls.Add(this.chkIncludeDesignation);
            this.grpCommon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCommon.Location = new System.Drawing.Point(414, 13);
            this.grpCommon.Margin = new System.Windows.Forms.Padding(4);
            this.grpCommon.Name = "grpCommon";
            this.grpCommon.Padding = new System.Windows.Forms.Padding(4);
            this.grpCommon.Size = new System.Drawing.Size(1152, 345);
            this.grpCommon.TabIndex = 11;
            this.grpCommon.TabStop = false;
            this.grpCommon.Text = "Filter Parameters";
            // 
            // cmbBloodGroup
            // 
            this.cmbBloodGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBloodGroup.DropDownWidth = 440;
            this.cmbBloodGroup.Location = new System.Drawing.Point(767, 114);
            this.cmbBloodGroup.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBloodGroup.Name = "cmbBloodGroup";
            this.cmbBloodGroup.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbBloodGroup.Size = new System.Drawing.Size(126, 22);
            this.cmbBloodGroup.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbBloodGroup.TabIndex = 69;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(677, 118);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 15);
            this.label9.TabIndex = 70;
            this.label9.Text = "Blood Group";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkBloodGroup
            // 
            this.chkBloodGroup.AutoSize = true;
            this.chkBloodGroup.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkBloodGroup.Checked = true;
            this.chkBloodGroup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBloodGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkBloodGroup.Location = new System.Drawing.Point(694, 96);
            this.chkBloodGroup.Name = "chkBloodGroup";
            this.chkBloodGroup.Size = new System.Drawing.Size(84, 19);
            this.chkBloodGroup.TabIndex = 68;
            this.chkBloodGroup.Tag = "Filtery By";
            this.chkBloodGroup.Text = "Filtery By";
            this.chkBloodGroup.UseVisualStyleBackColor = true;
            this.chkBloodGroup.CheckedChanged += new System.EventHandler(this.chkBloodGroup_CheckedChanged);
            // 
            // cmbFreeSearchAttributeName
            // 
            this.cmbFreeSearchAttributeName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFreeSearchAttributeName.DropDownWidth = 440;
            this.cmbFreeSearchAttributeName.Location = new System.Drawing.Point(97, 209);
            this.cmbFreeSearchAttributeName.Margin = new System.Windows.Forms.Padding(4);
            this.cmbFreeSearchAttributeName.Name = "cmbFreeSearchAttributeName";
            this.cmbFreeSearchAttributeName.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbFreeSearchAttributeName.Size = new System.Drawing.Size(126, 22);
            this.cmbFreeSearchAttributeName.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbFreeSearchAttributeName.TabIndex = 11;
            // 
            // chkIncludeMonth
            // 
            this.chkIncludeMonth.AutoSize = true;
            this.chkIncludeMonth.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIncludeMonth.Checked = true;
            this.chkIncludeMonth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkIncludeMonth.Location = new System.Drawing.Point(28, 27);
            this.chkIncludeMonth.Name = "chkIncludeMonth";
            this.chkIncludeMonth.Size = new System.Drawing.Size(84, 19);
            this.chkIncludeMonth.TabIndex = 1;
            this.chkIncludeMonth.Tag = "Filtery By";
            this.chkIncludeMonth.Text = "Filtery By";
            this.chkIncludeMonth.UseVisualStyleBackColor = true;
            this.chkIncludeMonth.CheckedChanged += new System.EventHandler(this.chkIncludeMonth_CheckedChanged);
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.DropDownWidth = 440;
            this.cmbGender.Location = new System.Drawing.Point(541, 114);
            this.cmbGender.Margin = new System.Windows.Forms.Padding(4);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbGender.Size = new System.Drawing.Size(126, 22);
            this.cmbGender.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbGender.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(485, 118);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 15);
            this.label7.TabIndex = 67;
            this.label7.Text = "Gender";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(24, 213);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 15);
            this.label8.TabIndex = 66;
            this.label8.Text = "Search By";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(333, 209);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtSearch.Size = new System.Drawing.Size(334, 22);
            this.txtSearch.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtSearch.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.TabIndex = 12;
            this.txtSearch.Tag = "Name";
            this.txtSearch.WordWrap = false;
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment.DropDownWidth = 440;
            this.cmbDepartment.Location = new System.Drawing.Point(333, 114);
            this.cmbDepartment.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbDepartment.Size = new System.Drawing.Size(126, 22);
            this.cmbDepartment.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbDepartment.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(249, 118);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 15);
            this.label6.TabIndex = 61;
            this.label6.Text = "Department";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtDTTo);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtDTFrom);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.optDOB);
            this.groupBox2.Controls.Add(this.optResignationDate);
            this.groupBox2.Controls.Add(this.optRelivingDate);
            this.groupBox2.Controls.Add(this.optConfirmDate);
            this.groupBox2.Controls.Add(this.optProbDate);
            this.groupBox2.Controls.Add(this.optDOJ);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 256);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1152, 88);
            this.groupBox2.TabIndex = 60;
            this.groupBox2.TabStop = false;
            // 
            // txtDTTo
            // 
            this.txtDTTo.Location = new System.Drawing.Point(401, 48);
            this.txtDTTo.Mask = "##-##-####";
            this.txtDTTo.Name = "txtDTTo";
            this.txtDTTo.Size = new System.Drawing.Size(125, 21);
            this.txtDTTo.TabIndex = 20;
            this.txtDTTo.Tag = "To";
            this.txtDTTo.ValidatingType = typeof(System.DateTime);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(375, 51);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 15);
            this.label5.TabIndex = 70;
            this.label5.Text = "To";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDTFrom
            // 
            this.txtDTFrom.Location = new System.Drawing.Point(97, 48);
            this.txtDTFrom.Mask = "##-##-####";
            this.txtDTFrom.Name = "txtDTFrom";
            this.txtDTFrom.Size = new System.Drawing.Size(125, 21);
            this.txtDTFrom.TabIndex = 19;
            this.txtDTFrom.Tag = "From";
            this.txtDTFrom.ValidatingType = typeof(System.DateTime);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(54, 51);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 15);
            this.label3.TabIndex = 68;
            this.label3.Text = "From";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // optDOB
            // 
            this.optDOB.CheckPosition = Krypton.Toolkit.VisualOrientation.Right;
            this.optDOB.Location = new System.Drawing.Point(13, 14);
            this.optDOB.Name = "optDOB";
            this.optDOB.Size = new System.Drawing.Size(94, 20);
            this.optDOB.TabIndex = 13;
            this.optDOB.Values.Text = "Date Of Birth";
            // 
            // optResignationDate
            // 
            this.optResignationDate.CheckPosition = Krypton.Toolkit.VisualOrientation.Right;
            this.optResignationDate.Location = new System.Drawing.Point(747, 14);
            this.optResignationDate.Name = "optResignationDate";
            this.optResignationDate.Size = new System.Drawing.Size(116, 20);
            this.optResignationDate.TabIndex = 18;
            this.optResignationDate.Values.Text = "Resignation Date";
            this.optResignationDate.Visible = false;
            // 
            // optRelivingDate
            // 
            this.optRelivingDate.CheckPosition = Krypton.Toolkit.VisualOrientation.Right;
            this.optRelivingDate.Location = new System.Drawing.Point(613, 14);
            this.optRelivingDate.Name = "optRelivingDate";
            this.optRelivingDate.Size = new System.Drawing.Size(95, 20);
            this.optRelivingDate.TabIndex = 17;
            this.optRelivingDate.Values.Text = "Reliving Date";
            this.optRelivingDate.Visible = false;
            // 
            // optConfirmDate
            // 
            this.optConfirmDate.CheckPosition = Krypton.Toolkit.VisualOrientation.Right;
            this.optConfirmDate.Location = new System.Drawing.Point(451, 14);
            this.optConfirmDate.Name = "optConfirmDate";
            this.optConfirmDate.Size = new System.Drawing.Size(123, 20);
            this.optConfirmDate.TabIndex = 16;
            this.optConfirmDate.Values.Text = "Confirmation Date";
            // 
            // optProbDate
            // 
            this.optProbDate.CheckPosition = Krypton.Toolkit.VisualOrientation.Right;
            this.optProbDate.Location = new System.Drawing.Point(307, 14);
            this.optProbDate.Name = "optProbDate";
            this.optProbDate.Size = new System.Drawing.Size(105, 20);
            this.optProbDate.TabIndex = 15;
            this.optProbDate.Values.Text = "Probation Date";
            // 
            // optDOJ
            // 
            this.optDOJ.CheckPosition = Krypton.Toolkit.VisualOrientation.Right;
            this.optDOJ.Location = new System.Drawing.Point(161, 14);
            this.optDOJ.Name = "optDOJ";
            this.optDOJ.Size = new System.Drawing.Size(107, 20);
            this.optDOJ.TabIndex = 14;
            this.optDOJ.Values.Text = "Date Of Joining";
            // 
            // cmbBranch
            // 
            this.cmbBranch.DropDownWidth = 440;
            this.cmbBranch.Location = new System.Drawing.Point(97, 171);
            this.cmbBranch.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbBranch.Size = new System.Drawing.Size(570, 22);
            this.cmbBranch.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbBranch.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(43, 175);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 58;
            this.label4.Text = "Branch";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbDesignation
            // 
            this.cmbDesignation.DropDownWidth = 440;
            this.cmbDesignation.Location = new System.Drawing.Point(97, 114);
            this.cmbDesignation.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDesignation.Name = "cmbDesignation";
            this.cmbDesignation.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbDesignation.Size = new System.Drawing.Size(126, 22);
            this.cmbDesignation.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbDesignation.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 118);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 15);
            this.label2.TabIndex = 56;
            this.label2.Text = "Designation";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbMonth
            // 
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.DropDownWidth = 440;
            this.cmbMonth.Location = new System.Drawing.Point(97, 53);
            this.cmbMonth.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbMonth.Size = new System.Drawing.Size(126, 22);
            this.cmbMonth.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbMonth.TabIndex = 2;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(48, 57);
            this.label42.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(47, 15);
            this.label42.TabIndex = 50;
            this.label42.Text = "Month";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkIncludeBranch
            // 
            this.chkIncludeBranch.AutoSize = true;
            this.chkIncludeBranch.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIncludeBranch.Checked = true;
            this.chkIncludeBranch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeBranch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkIncludeBranch.Location = new System.Drawing.Point(26, 153);
            this.chkIncludeBranch.Name = "chkIncludeBranch";
            this.chkIncludeBranch.Size = new System.Drawing.Size(84, 19);
            this.chkIncludeBranch.TabIndex = 9;
            this.chkIncludeBranch.Tag = "Filtery By";
            this.chkIncludeBranch.Text = "Filtery By";
            this.chkIncludeBranch.UseVisualStyleBackColor = true;
            this.chkIncludeBranch.CheckedChanged += new System.EventHandler(this.chkIncludeBranch_CheckedChanged);
            // 
            // chkIncludeGender
            // 
            this.chkIncludeGender.AutoSize = true;
            this.chkIncludeGender.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIncludeGender.Checked = true;
            this.chkIncludeGender.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkIncludeGender.Location = new System.Drawing.Point(471, 96);
            this.chkIncludeGender.Name = "chkIncludeGender";
            this.chkIncludeGender.Size = new System.Drawing.Size(84, 19);
            this.chkIncludeGender.TabIndex = 7;
            this.chkIncludeGender.Tag = "Filtery By";
            this.chkIncludeGender.Text = "Filtery By";
            this.chkIncludeGender.UseVisualStyleBackColor = true;
            this.chkIncludeGender.CheckedChanged += new System.EventHandler(this.chkIncludeGender_CheckedChanged);
            // 
            // chkIncludeDepartment
            // 
            this.chkIncludeDepartment.AutoSize = true;
            this.chkIncludeDepartment.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIncludeDepartment.Checked = true;
            this.chkIncludeDepartment.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeDepartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkIncludeDepartment.Location = new System.Drawing.Point(263, 96);
            this.chkIncludeDepartment.Name = "chkIncludeDepartment";
            this.chkIncludeDepartment.Size = new System.Drawing.Size(84, 19);
            this.chkIncludeDepartment.TabIndex = 5;
            this.chkIncludeDepartment.Tag = "Filtery By";
            this.chkIncludeDepartment.Text = "Filtery By";
            this.chkIncludeDepartment.UseVisualStyleBackColor = true;
            this.chkIncludeDepartment.CheckedChanged += new System.EventHandler(this.chkIncludeDepartment_CheckedChanged);
            // 
            // chkIncludeDesignation
            // 
            this.chkIncludeDesignation.AutoSize = true;
            this.chkIncludeDesignation.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIncludeDesignation.Checked = true;
            this.chkIncludeDesignation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeDesignation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkIncludeDesignation.Location = new System.Drawing.Point(27, 96);
            this.chkIncludeDesignation.Name = "chkIncludeDesignation";
            this.chkIncludeDesignation.Size = new System.Drawing.Size(84, 19);
            this.chkIncludeDesignation.TabIndex = 3;
            this.chkIncludeDesignation.Tag = "Filtery By";
            this.chkIncludeDesignation.Text = "Filtery By";
            this.chkIncludeDesignation.UseVisualStyleBackColor = true;
            this.chkIncludeDesignation.CheckedChanged += new System.EventHandler(this.chkIncludeDesignation_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtgDataResult);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(414, 404);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1152, 354);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 320);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(790, 30);
            this.label1.TabIndex = 104;
            this.label1.Text = "💡Double-click to view Report Filter Option";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Visible = false;
            // 
            // dtgDataResult
            // 
            this.dtgDataResult.AllowUserToAddRows = false;
            this.dtgDataResult.AllowUserToDeleteRows = false;
            this.dtgDataResult.AllowUserToResizeRows = false;
            this.dtgDataResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgDataResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgDataResult.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dtgDataResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgDataResult.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dtgDataResult.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dtgDataResult.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.ButtonLowProfile;
            this.dtgDataResult.Location = new System.Drawing.Point(0, 6);
            this.dtgDataResult.MultiSelect = false;
            this.dtgDataResult.Name = "dtgDataResult";
            this.dtgDataResult.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtgDataResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgDataResult.Size = new System.Drawing.Size(1152, 311);
            this.dtgDataResult.TabIndex = 48;
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
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(414, 365);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnExecute.Size = new System.Drawing.Size(126, 38);
            this.btnExecute.TabIndex = 21;
            this.btnExecute.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnExecute.Values.Image = global::StaffSync.Properties.Resources.execute;
            this.btnExecute.Values.Text = "Execute";
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(1440, 365);
            this.btnExport.Name = "btnExport";
            this.btnExport.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnExport.Size = new System.Drawing.Size(126, 38);
            this.btnExport.TabIndex = 22;
            this.btnExport.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnExport.Values.Image = global::StaffSync.Properties.Resources.export;
            this.btnExport.Values.Text = "Export";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // lblSelectedReport
            // 
            this.lblSelectedReport.AutoSize = true;
            this.lblSelectedReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedReport.Location = new System.Drawing.Point(1251, 385);
            this.lblSelectedReport.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSelectedReport.Name = "lblSelectedReport";
            this.lblSelectedReport.Size = new System.Drawing.Size(122, 15);
            this.lblSelectedReport.TabIndex = 51;
            this.lblSelectedReport.Text = "lblSelectedReport";
            this.lblSelectedReport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSelectedReport.Visible = false;
            // 
            // lblSelectedReportName
            // 
            this.lblSelectedReportName.AutoSize = true;
            this.lblSelectedReportName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedReportName.Location = new System.Drawing.Point(1251, 370);
            this.lblSelectedReportName.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSelectedReportName.Name = "lblSelectedReportName";
            this.lblSelectedReportName.Size = new System.Drawing.Size(160, 15);
            this.lblSelectedReportName.TabIndex = 52;
            this.lblSelectedReportName.Text = "lblSelectedReportName";
            this.lblSelectedReportName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSelectedReportName.Visible = false;
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter.Location = new System.Drawing.Point(1251, 355);
            this.lblFilter.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(56, 15);
            this.lblFilter.TabIndex = 53;
            this.lblFilter.Text = "lblFilter";
            this.lblFilter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblFilter.Visible = false;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(546, 365);
            this.btnReset.Name = "btnReset";
            this.btnReset.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnReset.Size = new System.Drawing.Size(47, 38);
            this.btnReset.TabIndex = 54;
            this.btnReset.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnReset.Values.Image = global::StaffSync.Properties.Resources.refresh;
            this.btnReset.Values.Text = "";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // cmbCriteriaOperator
            // 
            this.cmbCriteriaOperator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCriteriaOperator.DropDownWidth = 440;
            this.cmbCriteriaOperator.Location = new System.Drawing.Point(228, 209);
            this.cmbCriteriaOperator.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCriteriaOperator.Name = "cmbCriteriaOperator";
            this.cmbCriteriaOperator.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbCriteriaOperator.Size = new System.Drawing.Size(100, 22);
            this.cmbCriteriaOperator.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbCriteriaOperator.TabIndex = 71;
            // 
            // frmEmpSpecificReports
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1579, 771);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.lblSelectedReportName);
            this.Controls.Add(this.lblSelectedReport);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpCommon);
            this.Controls.Add(this.groupBox5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEmpSpecificReports";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Leave Type List";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.frmEmpSpecificReports_Activated);
            this.Load += new System.EventHandler(this.frmEmpSpecificReports_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmEmpSpecificReports_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgReportsList)).EndInit();
            this.grpCommon.ResumeLayout(false);
            this.grpCommon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBloodGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFreeSearchAttributeName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGender)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDepartment)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDesignation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonth)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgDataResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCriteriaOperator)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private StaffsyncDBDTSet staffsyncDBDTSet;
        private System.Windows.Forms.BindingSource empMasInfoBindingSource;
        private StaffsyncDBDTSetTableAdapters.EmpMasInfoTableAdapter empMasInfoTableAdapter;
        private System.Windows.Forms.ErrorProvider errValidator;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox grpCommon;
        private Krypton.Toolkit.KryptonDataGridView dtgReportsList;
        private System.Windows.Forms.GroupBox groupBox1;
        private Krypton.Toolkit.KryptonComboBox cmbMonth;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label1;
        private Krypton.Toolkit.KryptonDataGridView dtgDataResult;
        private Krypton.Toolkit.KryptonComboBox cmbBranch;
        private System.Windows.Forms.Label label4;
        private Krypton.Toolkit.KryptonComboBox cmbDesignation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private Krypton.Toolkit.KryptonRadioButton optRelivingDate;
        private Krypton.Toolkit.KryptonRadioButton optConfirmDate;
        private Krypton.Toolkit.KryptonRadioButton optProbDate;
        private Krypton.Toolkit.KryptonRadioButton optDOJ;
        private Krypton.Toolkit.KryptonRadioButton optDOB;
        private Krypton.Toolkit.KryptonRadioButton optResignationDate;
        private Krypton.Toolkit.KryptonComboBox cmbDepartment;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private Krypton.Toolkit.KryptonTextBox txtSearch;
        private Krypton.Toolkit.KryptonComboBox cmbGender;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox txtDTTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.MaskedTextBox txtDTFrom;
        private System.Windows.Forms.Label label3;
        private Krypton.Toolkit.KryptonButton btnExecute;
        private Krypton.Toolkit.KryptonButton btnExport;
        private System.Windows.Forms.CheckBox chkIncludeBranch;
        private System.Windows.Forms.CheckBox chkIncludeGender;
        private System.Windows.Forms.CheckBox chkIncludeDepartment;
        private System.Windows.Forms.CheckBox chkIncludeDesignation;
        private System.Windows.Forms.CheckBox chkIncludeMonth;
        private System.Windows.Forms.Label lblSelectedReport;
        private Krypton.Toolkit.KryptonComboBox cmbFreeSearchAttributeName;
        private System.Windows.Forms.Label lblSelectedReportName;
        private System.Windows.Forms.Label lblFilter;
        private Krypton.Toolkit.KryptonComboBox cmbBloodGroup;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkBloodGroup;
        private Krypton.Toolkit.KryptonButton btnReset;
        private Krypton.Toolkit.KryptonComboBox cmbCriteriaOperator;
    }
}