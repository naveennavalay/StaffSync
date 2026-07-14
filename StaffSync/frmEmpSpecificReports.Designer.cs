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
            this.dtgReportsList = new Krypton.Toolkit.KryptonDataGridView();
            this.grpCommon = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.empMasInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.staffsyncDBDTSet = new StaffSync.StaffsyncDBDTSet();
            this.empMasInfoTableAdapter = new StaffSync.StaffsyncDBDTSetTableAdapters.EmpMasInfoTableAdapter();
            this.cmbMonth = new Krypton.Toolkit.KryptonComboBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.kryptonDataGridView1 = new Krypton.Toolkit.KryptonDataGridView();
            this.cmbDesignation = new Krypton.Toolkit.KryptonComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBranch = new Krypton.Toolkit.KryptonComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.optDOJ = new Krypton.Toolkit.KryptonRadioButton();
            this.optProbDate = new Krypton.Toolkit.KryptonRadioButton();
            this.optConfirmDate = new Krypton.Toolkit.KryptonRadioButton();
            this.optRelivingDate = new Krypton.Toolkit.KryptonRadioButton();
            this.optResignationDate = new Krypton.Toolkit.KryptonRadioButton();
            this.optDOB = new Krypton.Toolkit.KryptonRadioButton();
            this.cmbDepartment = new Krypton.Toolkit.KryptonComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSearch = new Krypton.Toolkit.KryptonTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbGender = new Krypton.Toolkit.KryptonComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDTTo = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDTFrom = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgReportsList)).BeginInit();
            this.grpCommon.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDesignation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGender)).BeginInit();
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
            this.groupBox5.Size = new System.Drawing.Size(393, 598);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Reports List";
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
            this.dtgReportsList.Size = new System.Drawing.Size(375, 539);
            this.dtgReportsList.TabIndex = 47;
            this.dtgReportsList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgReportsList_CellDoubleClick);
            // 
            // grpCommon
            // 
            this.grpCommon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.grpCommon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCommon.Location = new System.Drawing.Point(414, 13);
            this.grpCommon.Margin = new System.Windows.Forms.Padding(4);
            this.grpCommon.Name = "grpCommon";
            this.grpCommon.Padding = new System.Windows.Forms.Padding(4);
            this.grpCommon.Size = new System.Drawing.Size(1152, 247);
            this.grpCommon.TabIndex = 11;
            this.grpCommon.TabStop = false;
            this.grpCommon.Text = "Filter Parameters";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.kryptonDataGridView1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(414, 268);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1152, 343);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
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
            // cmbMonth
            // 
            this.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonth.DropDownWidth = 440;
            this.cmbMonth.Location = new System.Drawing.Point(103, 34);
            this.cmbMonth.Margin = new System.Windows.Forms.Padding(4);
            this.cmbMonth.Name = "cmbMonth";
            this.cmbMonth.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbMonth.Size = new System.Drawing.Size(126, 22);
            this.cmbMonth.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbMonth.TabIndex = 51;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(47, 38);
            this.label42.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(47, 15);
            this.label42.TabIndex = 50;
            this.label42.Text = "Month";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(8, 564);
            this.label17.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(375, 30);
            this.label17.TabIndex = 103;
            this.label17.Text = "💡Double-click to view Report Filter Option";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 309);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(790, 30);
            this.label1.TabIndex = 104;
            this.label1.Text = "💡Double-click to view Report Filter Option";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Visible = false;
            // 
            // kryptonDataGridView1
            // 
            this.kryptonDataGridView1.AllowUserToAddRows = false;
            this.kryptonDataGridView1.AllowUserToDeleteRows = false;
            this.kryptonDataGridView1.AllowUserToResizeRows = false;
            this.kryptonDataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.kryptonDataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.kryptonDataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.kryptonDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.kryptonDataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.kryptonDataGridView1.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.kryptonDataGridView1.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.ButtonLowProfile;
            this.kryptonDataGridView1.Location = new System.Drawing.Point(0, 0);
            this.kryptonDataGridView1.MultiSelect = false;
            this.kryptonDataGridView1.Name = "kryptonDataGridView1";
            this.kryptonDataGridView1.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.kryptonDataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.kryptonDataGridView1.Size = new System.Drawing.Size(1152, 306);
            this.kryptonDataGridView1.TabIndex = 48;
            // 
            // cmbDesignation
            // 
            this.cmbDesignation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDesignation.DropDownWidth = 440;
            this.cmbDesignation.Location = new System.Drawing.Point(341, 34);
            this.cmbDesignation.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDesignation.Name = "cmbDesignation";
            this.cmbDesignation.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbDesignation.Size = new System.Drawing.Size(126, 22);
            this.cmbDesignation.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbDesignation.TabIndex = 57;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(248, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 15);
            this.label2.TabIndex = 56;
            this.label2.Text = "Designation";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbBranch
            // 
            this.cmbBranch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBranch.DropDownWidth = 440;
            this.cmbBranch.Location = new System.Drawing.Point(991, 34);
            this.cmbBranch.Margin = new System.Windows.Forms.Padding(4);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbBranch.Size = new System.Drawing.Size(378, 22);
            this.cmbBranch.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbBranch.TabIndex = 59;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(930, 38);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 58;
            this.label4.Text = "Branch";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.groupBox2.Location = new System.Drawing.Point(0, 114);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1152, 96);
            this.groupBox2.TabIndex = 60;
            this.groupBox2.TabStop = false;
            // 
            // optDOJ
            // 
            this.optDOJ.Location = new System.Drawing.Point(161, 29);
            this.optDOJ.Name = "optDOJ";
            this.optDOJ.Size = new System.Drawing.Size(107, 20);
            this.optDOJ.TabIndex = 62;
            this.optDOJ.Values.Text = "Date Of Joining";
            // 
            // optProbDate
            // 
            this.optProbDate.Location = new System.Drawing.Point(307, 29);
            this.optProbDate.Name = "optProbDate";
            this.optProbDate.Size = new System.Drawing.Size(105, 20);
            this.optProbDate.TabIndex = 63;
            this.optProbDate.Values.Text = "Probation Date";
            // 
            // optConfirmDate
            // 
            this.optConfirmDate.Location = new System.Drawing.Point(451, 29);
            this.optConfirmDate.Name = "optConfirmDate";
            this.optConfirmDate.Size = new System.Drawing.Size(123, 20);
            this.optConfirmDate.TabIndex = 64;
            this.optConfirmDate.Values.Text = "Confirmation Date";
            // 
            // optRelivingDate
            // 
            this.optRelivingDate.Location = new System.Drawing.Point(613, 29);
            this.optRelivingDate.Name = "optRelivingDate";
            this.optRelivingDate.Size = new System.Drawing.Size(95, 20);
            this.optRelivingDate.TabIndex = 65;
            this.optRelivingDate.Values.Text = "Reliving Date";
            // 
            // optResignationDate
            // 
            this.optResignationDate.Location = new System.Drawing.Point(747, 29);
            this.optResignationDate.Name = "optResignationDate";
            this.optResignationDate.Size = new System.Drawing.Size(116, 20);
            this.optResignationDate.TabIndex = 66;
            this.optResignationDate.Values.Text = "Resignation Date";
            // 
            // optDOB
            // 
            this.optDOB.Location = new System.Drawing.Point(28, 29);
            this.optDOB.Name = "optDOB";
            this.optDOB.Size = new System.Drawing.Size(94, 20);
            this.optDOB.TabIndex = 67;
            this.optDOB.Values.Text = "Date Of Birth";
            // 
            // cmbDepartment
            // 
            this.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDepartment.DropDownWidth = 440;
            this.cmbDepartment.Location = new System.Drawing.Point(577, 34);
            this.cmbDepartment.Margin = new System.Windows.Forms.Padding(4);
            this.cmbDepartment.Name = "cmbDepartment";
            this.cmbDepartment.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbDepartment.Size = new System.Drawing.Size(126, 22);
            this.cmbDepartment.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbDepartment.TabIndex = 62;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(486, 38);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 15);
            this.label6.TabIndex = 61;
            this.label6.Text = "Department";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(97, 82);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtSearch.Size = new System.Drawing.Size(370, 20);
            this.txtSearch.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtSearch.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.TabIndex = 65;
            this.txtSearch.Tag = "Name";
            this.txtSearch.WordWrap = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(42, 85);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 15);
            this.label8.TabIndex = 66;
            this.label8.Text = "Search";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbGender
            // 
            this.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGender.DropDownWidth = 440;
            this.cmbGender.Location = new System.Drawing.Point(785, 34);
            this.cmbGender.Margin = new System.Windows.Forms.Padding(4);
            this.cmbGender.Name = "cmbGender";
            this.cmbGender.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbGender.Size = new System.Drawing.Size(126, 22);
            this.cmbGender.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbGender.TabIndex = 68;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(722, 38);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 15);
            this.label7.TabIndex = 67;
            this.label7.Text = "Gender";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDTTo
            // 
            this.txtDTTo.Location = new System.Drawing.Point(328, 65);
            this.txtDTTo.Mask = "##-##-####";
            this.txtDTTo.Name = "txtDTTo";
            this.txtDTTo.Size = new System.Drawing.Size(125, 21);
            this.txtDTTo.TabIndex = 71;
            this.txtDTTo.Tag = "To";
            this.txtDTTo.ValidatingType = typeof(System.DateTime);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(302, 68);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 15);
            this.label5.TabIndex = 70;
            this.label5.Text = "To";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDTFrom
            // 
            this.txtDTFrom.Location = new System.Drawing.Point(97, 65);
            this.txtDTFrom.Mask = "##-##-####";
            this.txtDTFrom.Name = "txtDTFrom";
            this.txtDTFrom.Size = new System.Drawing.Size(125, 21);
            this.txtDTFrom.TabIndex = 69;
            this.txtDTFrom.Tag = "From";
            this.txtDTFrom.ValidatingType = typeof(System.DateTime);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(54, 68);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 15);
            this.label3.TabIndex = 68;
            this.label3.Text = "From";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmEmpSpecificReports
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1579, 624);
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
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonDataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDesignation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbBranch)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbGender)).EndInit();
            this.ResumeLayout(false);

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
        private Krypton.Toolkit.KryptonDataGridView kryptonDataGridView1;
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
    }
}