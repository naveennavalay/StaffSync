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
            this.dtgSalaryDetails1 = new Krypton.Toolkit.KryptonDataGridView();
            this.txtNetPayable = new Krypton.Toolkit.KryptonTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblBasicSalary = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblBasicSalaryPerDay = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtReimbursement = new Krypton.Toolkit.KryptonTextBox();
            this.lblBasicSalaryPerHour = new System.Windows.Forms.Label();
            this.txtDeductions = new Krypton.Toolkit.KryptonTextBox();
            this.txtAallowences = new Krypton.Toolkit.KryptonTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblSexID = new System.Windows.Forms.Label();
            this.txtTotalPayableDays = new System.Windows.Forms.TextBox();
            this.lblStateID = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblReportingManagerID = new System.Windows.Forms.Label();
            this.txtUnpaidLeaves = new System.Windows.Forms.TextBox();
            this.lblActionMode = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkAutoCalculate = new System.Windows.Forms.CheckBox();
            this.txtLeaveDays = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPFCalcAmount = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.chkExcludeEmpWithPendingAdvances = new Krypton.Toolkit.KryptonCheckButton();
            this.label16 = new System.Windows.Forms.Label();
            this.lblSalarySummary = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtgSalaryDetails = new Krypton.Toolkit.KryptonDataGridView();
            this.chkFlipOnOff = new Krypton.Toolkit.KryptonCheckButton();
            this.label10 = new System.Windows.Forms.Label();
            this.chkSelectUnSelect = new System.Windows.Forms.CheckBox();
            this.btnBatchProcess = new Krypton.Toolkit.KryptonButton();
            this.picDownloadDataAsCSV = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.picRefreshBankList = new System.Windows.Forms.PictureBox();
            this.dtSalaryDate = new Krypton.Toolkit.KryptonDateTimePicker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label29 = new System.Windows.Forms.Label();
            this.cmbSalaryMonth = new Krypton.Toolkit.KryptonComboBox();
            this.label42 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.lblNetPayable = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblTotalReimbursement = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblTotalDeductions = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblTotalEarnings = new System.Windows.Forms.Label();
            this.txtTotalWorkedDays = new System.Windows.Forms.TextBox();
            this.txtTotalWorkingDays = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
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
            ((System.ComponentModel.ISupportInitialize)(this.dtgSalaryDetails1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSalaryDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDownloadDataAsCSV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRefreshBankList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSalaryMonth)).BeginInit();
            this.groupBox3.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(1065, 652);
            this.splitContainer1.SplitterDistance = 587;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.dtgSalaryDetails1);
            this.panel1.Controls.Add(this.txtNetPayable);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.lblBasicSalary);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lblBasicSalaryPerDay);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtReimbursement);
            this.panel1.Controls.Add(this.lblBasicSalaryPerHour);
            this.panel1.Controls.Add(this.txtDeductions);
            this.panel1.Controls.Add(this.txtAallowences);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.lblSexID);
            this.panel1.Controls.Add(this.txtTotalPayableDays);
            this.panel1.Controls.Add(this.lblStateID);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.lblReportingManagerID);
            this.panel1.Controls.Add(this.txtUnpaidLeaves);
            this.panel1.Controls.Add(this.lblActionMode);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.chkAutoCalculate);
            this.panel1.Controls.Add(this.txtLeaveDays);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.txtTotalWorkedDays);
            this.panel1.Controls.Add(this.txtTotalWorkingDays);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1065, 587);
            this.panel1.TabIndex = 1;
            // 
            // dtgSalaryDetails1
            // 
            this.dtgSalaryDetails1.AllowUserToAddRows = false;
            this.dtgSalaryDetails1.AllowUserToDeleteRows = false;
            this.dtgSalaryDetails1.AllowUserToResizeRows = false;
            this.dtgSalaryDetails1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgSalaryDetails1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dtgSalaryDetails1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgSalaryDetails1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dtgSalaryDetails1.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dtgSalaryDetails1.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.ButtonLowProfile;
            this.dtgSalaryDetails1.Location = new System.Drawing.Point(1076, 340);
            this.dtgSalaryDetails1.Margin = new System.Windows.Forms.Padding(4);
            this.dtgSalaryDetails1.MultiSelect = false;
            this.dtgSalaryDetails1.Name = "dtgSalaryDetails1";
            this.dtgSalaryDetails1.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtgSalaryDetails1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgSalaryDetails1.Size = new System.Drawing.Size(564, 227);
            this.dtgSalaryDetails1.TabIndex = 46;
            this.dtgSalaryDetails1.Visible = false;
            this.dtgSalaryDetails1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgSalaryDetails1_CellEndEdit);
            // 
            // txtNetPayable
            // 
            this.txtNetPayable.Location = new System.Drawing.Point(1391, 268);
            this.txtNetPayable.Margin = new System.Windows.Forms.Padding(4);
            this.txtNetPayable.Multiline = true;
            this.txtNetPayable.Name = "txtNetPayable";
            this.txtNetPayable.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtNetPayable.ReadOnly = true;
            this.txtNetPayable.Size = new System.Drawing.Size(134, 28);
            this.txtNetPayable.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetPayable.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtNetPayable.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetPayable.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetPayable.TabIndex = 74;
            this.txtNetPayable.Text = "0.00";
            this.txtNetPayable.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNetPayable.Visible = false;
            this.txtNetPayable.WordWrap = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1095, 275);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(515, 15);
            this.label4.TabIndex = 73;
            this.label4.Text = "Net Payable For the Month (Total Earnings - Total Deductions + Reimbursment)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Visible = false;
            // 
            // lblBasicSalary
            // 
            this.lblBasicSalary.AutoSize = true;
            this.lblBasicSalary.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblBasicSalary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBasicSalary.Location = new System.Drawing.Point(1153, 80);
            this.lblBasicSalary.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblBasicSalary.Name = "lblBasicSalary";
            this.lblBasicSalary.Size = new System.Drawing.Size(11, 15);
            this.lblBasicSalary.TabIndex = 85;
            this.lblBasicSalary.Text = " ";
            this.lblBasicSalary.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(1454, 99);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 15);
            this.label9.TabIndex = 84;
            this.label9.Text = "Salary/Day";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label9.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1073, 311);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 15);
            this.label5.TabIndex = 72;
            this.label5.Text = "Total";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Visible = false;
            // 
            // lblBasicSalaryPerDay
            // 
            this.lblBasicSalaryPerDay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBasicSalaryPerDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBasicSalaryPerDay.Location = new System.Drawing.Point(1531, 95);
            this.lblBasicSalaryPerDay.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblBasicSalaryPerDay.Name = "lblBasicSalaryPerDay";
            this.lblBasicSalaryPerDay.Size = new System.Drawing.Size(109, 22);
            this.lblBasicSalaryPerDay.TabIndex = 81;
            this.lblBasicSalaryPerDay.Text = "0.00";
            this.lblBasicSalaryPerDay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblBasicSalaryPerDay.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1447, 59);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 15);
            this.label8.TabIndex = 83;
            this.label8.Text = "Salary/Hour";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label8.Visible = false;
            // 
            // txtReimbursement
            // 
            this.txtReimbursement.Location = new System.Drawing.Point(1391, 304);
            this.txtReimbursement.Margin = new System.Windows.Forms.Padding(4);
            this.txtReimbursement.Multiline = true;
            this.txtReimbursement.Name = "txtReimbursement";
            this.txtReimbursement.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtReimbursement.ReadOnly = true;
            this.txtReimbursement.Size = new System.Drawing.Size(134, 28);
            this.txtReimbursement.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReimbursement.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtReimbursement.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReimbursement.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReimbursement.TabIndex = 71;
            this.txtReimbursement.Text = "0.00";
            this.txtReimbursement.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtReimbursement.Visible = false;
            this.txtReimbursement.WordWrap = false;
            // 
            // lblBasicSalaryPerHour
            // 
            this.lblBasicSalaryPerHour.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblBasicSalaryPerHour.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBasicSalaryPerHour.Location = new System.Drawing.Point(1531, 55);
            this.lblBasicSalaryPerHour.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblBasicSalaryPerHour.Name = "lblBasicSalaryPerHour";
            this.lblBasicSalaryPerHour.Size = new System.Drawing.Size(109, 22);
            this.lblBasicSalaryPerHour.TabIndex = 82;
            this.lblBasicSalaryPerHour.Text = "0.00";
            this.lblBasicSalaryPerHour.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblBasicSalaryPerHour.Visible = false;
            // 
            // txtDeductions
            // 
            this.txtDeductions.Location = new System.Drawing.Point(1256, 304);
            this.txtDeductions.Margin = new System.Windows.Forms.Padding(4);
            this.txtDeductions.Multiline = true;
            this.txtDeductions.Name = "txtDeductions";
            this.txtDeductions.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtDeductions.ReadOnly = true;
            this.txtDeductions.Size = new System.Drawing.Size(134, 28);
            this.txtDeductions.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeductions.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtDeductions.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeductions.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeductions.TabIndex = 70;
            this.txtDeductions.Text = "0.00";
            this.txtDeductions.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDeductions.Visible = false;
            this.txtDeductions.WordWrap = false;
            // 
            // txtAallowences
            // 
            this.txtAallowences.Location = new System.Drawing.Point(1121, 304);
            this.txtAallowences.Margin = new System.Windows.Forms.Padding(4);
            this.txtAallowences.Multiline = true;
            this.txtAallowences.Name = "txtAallowences";
            this.txtAallowences.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtAallowences.ReadOnly = true;
            this.txtAallowences.Size = new System.Drawing.Size(134, 28);
            this.txtAallowences.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAallowences.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtAallowences.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAallowences.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAallowences.TabIndex = 69;
            this.txtAallowences.Text = "0.00";
            this.txtAallowences.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAallowences.Visible = false;
            this.txtAallowences.WordWrap = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1275, 219);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 15);
            this.label7.TabIndex = 80;
            this.label7.Text = "Payable Days";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label7.Visible = false;
            // 
            // lblSexID
            // 
            this.lblSexID.AutoSize = true;
            this.lblSexID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblSexID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSexID.Location = new System.Drawing.Point(1153, 51);
            this.lblSexID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSexID.Name = "lblSexID";
            this.lblSexID.Size = new System.Drawing.Size(11, 15);
            this.lblSexID.TabIndex = 68;
            this.lblSexID.Text = " ";
            this.lblSexID.Visible = false;
            // 
            // txtTotalPayableDays
            // 
            this.txtTotalPayableDays.Location = new System.Drawing.Point(1372, 214);
            this.txtTotalPayableDays.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtTotalPayableDays.MaxLength = 255;
            this.txtTotalPayableDays.Multiline = true;
            this.txtTotalPayableDays.Name = "txtTotalPayableDays";
            this.txtTotalPayableDays.Size = new System.Drawing.Size(54, 25);
            this.txtTotalPayableDays.TabIndex = 79;
            this.txtTotalPayableDays.Tag = "Please enter Employeee Code";
            this.txtTotalPayableDays.Text = "0";
            this.txtTotalPayableDays.Visible = false;
            this.txtTotalPayableDays.WordWrap = false;
            // 
            // lblStateID
            // 
            this.lblStateID.AutoSize = true;
            this.lblStateID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblStateID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStateID.Location = new System.Drawing.Point(1153, 22);
            this.lblStateID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblStateID.Name = "lblStateID";
            this.lblStateID.Size = new System.Drawing.Size(11, 15);
            this.lblStateID.TabIndex = 67;
            this.lblStateID.Text = " ";
            this.lblStateID.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1266, 179);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 15);
            this.label6.TabIndex = 78;
            this.label6.Text = "Unpaid Leaves";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Visible = false;
            // 
            // lblReportingManagerID
            // 
            this.lblReportingManagerID.AutoSize = true;
            this.lblReportingManagerID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblReportingManagerID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportingManagerID.Location = new System.Drawing.Point(1153, 109);
            this.lblReportingManagerID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblReportingManagerID.Name = "lblReportingManagerID";
            this.lblReportingManagerID.Size = new System.Drawing.Size(11, 15);
            this.lblReportingManagerID.TabIndex = 66;
            this.lblReportingManagerID.Text = " ";
            this.lblReportingManagerID.Visible = false;
            // 
            // txtUnpaidLeaves
            // 
            this.txtUnpaidLeaves.Location = new System.Drawing.Point(1372, 174);
            this.txtUnpaidLeaves.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtUnpaidLeaves.MaxLength = 255;
            this.txtUnpaidLeaves.Multiline = true;
            this.txtUnpaidLeaves.Name = "txtUnpaidLeaves";
            this.txtUnpaidLeaves.Size = new System.Drawing.Size(54, 25);
            this.txtUnpaidLeaves.TabIndex = 77;
            this.txtUnpaidLeaves.Tag = "Please enter Employeee Code";
            this.txtUnpaidLeaves.Text = "0";
            this.txtUnpaidLeaves.Visible = false;
            this.txtUnpaidLeaves.WordWrap = false;
            // 
            // lblActionMode
            // 
            this.lblActionMode.AutoSize = true;
            this.lblActionMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActionMode.Location = new System.Drawing.Point(1095, 128);
            this.lblActionMode.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblActionMode.Name = "lblActionMode";
            this.lblActionMode.Size = new System.Drawing.Size(31, 15);
            this.lblActionMode.TabIndex = 65;
            this.lblActionMode.Text = "add";
            this.lblActionMode.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(1283, 139);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 15);
            this.label3.TabIndex = 76;
            this.label3.Text = "Paid Leaves";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Visible = false;
            // 
            // chkAutoCalculate
            // 
            this.chkAutoCalculate.AutoSize = true;
            this.chkAutoCalculate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAutoCalculate.Checked = true;
            this.chkAutoCalculate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkAutoCalculate.Location = new System.Drawing.Point(1075, 159);
            this.chkAutoCalculate.Name = "chkAutoCalculate";
            this.chkAutoCalculate.Size = new System.Drawing.Size(118, 19);
            this.chkAutoCalculate.TabIndex = 64;
            this.chkAutoCalculate.Text = "Auto Calculate";
            this.chkAutoCalculate.UseVisualStyleBackColor = true;
            this.chkAutoCalculate.Visible = false;
            // 
            // txtLeaveDays
            // 
            this.txtLeaveDays.Location = new System.Drawing.Point(1372, 134);
            this.txtLeaveDays.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtLeaveDays.MaxLength = 255;
            this.txtLeaveDays.Multiline = true;
            this.txtLeaveDays.Name = "txtLeaveDays";
            this.txtLeaveDays.Size = new System.Drawing.Size(54, 25);
            this.txtLeaveDays.TabIndex = 75;
            this.txtLeaveDays.Tag = "Please enter Employeee Code";
            this.txtLeaveDays.Text = "0";
            this.txtLeaveDays.Visible = false;
            this.txtLeaveDays.WordWrap = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1278, 99);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 15);
            this.label2.TabIndex = 74;
            this.label2.Text = "Days Worked";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupBox1.Controls.Add(this.lblPFCalcAmount);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.chkExcludeEmpWithPendingAdvances);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.lblSalarySummary);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.dtgSalaryDetails);
            this.groupBox1.Controls.Add(this.chkFlipOnOff);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.chkSelectUnSelect);
            this.groupBox1.Controls.Add(this.btnBatchProcess);
            this.groupBox1.Controls.Add(this.picDownloadDataAsCSV);
            this.groupBox1.Controls.Add(this.pictureBox4);
            this.groupBox1.Controls.Add(this.picRefreshBankList);
            this.groupBox1.Controls.Add(this.dtSalaryDate);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.cmbSalaryMonth);
            this.groupBox1.Controls.Add(this.label42);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(16, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1032, 566);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            // 
            // lblPFCalcAmount
            // 
            this.lblPFCalcAmount.AutoSize = true;
            this.lblPFCalcAmount.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblPFCalcAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPFCalcAmount.Location = new System.Drawing.Point(566, 66);
            this.lblPFCalcAmount.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblPFCalcAmount.Name = "lblPFCalcAmount";
            this.lblPFCalcAmount.Size = new System.Drawing.Size(11, 15);
            this.lblPFCalcAmount.TabIndex = 106;
            this.lblPFCalcAmount.Text = " ";
            this.lblPFCalcAmount.Visible = false;
            // 
            // label17
            // 
            this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(9, 380);
            this.label17.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(400, 22);
            this.label17.TabIndex = 102;
            this.label17.Text = "💡 Tip: Double-click an employee row to review or update salary details.";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkExcludeEmpWithPendingAdvances
            // 
            this.errValidator.SetIconAlignment(this.chkExcludeEmpWithPendingAdvances, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.chkExcludeEmpWithPendingAdvances.Location = new System.Drawing.Point(575, 380);
            this.chkExcludeEmpWithPendingAdvances.Name = "chkExcludeEmpWithPendingAdvances";
            this.chkExcludeEmpWithPendingAdvances.Size = new System.Drawing.Size(439, 22);
            this.chkExcludeEmpWithPendingAdvances.TabIndex = 100;
            this.chkExcludeEmpWithPendingAdvances.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.chkExcludeEmpWithPendingAdvances.Values.Text = "☐ Show Salary Summary excluding employees who have Pending Advances";
            this.chkExcludeEmpWithPendingAdvances.Click += new System.EventHandler(this.chkExcludeEmpWithPendingAdvances_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(678, 482);
            this.label16.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(111, 15);
            this.label16.TabIndex = 98;
            this.label16.Text = "Salary Summary";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSalarySummary
            // 
            this.lblSalarySummary.AutoSize = true;
            this.lblSalarySummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSalarySummary.Location = new System.Drawing.Point(678, 500);
            this.lblSalarySummary.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSalarySummary.Name = "lblSalarySummary";
            this.lblSalarySummary.Size = new System.Drawing.Size(145, 15);
            this.lblSalarySummary.TabIndex = 97;
            this.lblSalarySummary.Text = "Employees loaded : 0";
            this.lblSalarySummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(10, 482);
            this.label12.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(45, 15);
            this.label12.TabIndex = 94;
            this.label12.Text = "Note :";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtgSalaryDetails
            // 
            this.dtgSalaryDetails.AllowUserToAddRows = false;
            this.dtgSalaryDetails.AllowUserToDeleteRows = false;
            this.dtgSalaryDetails.AllowUserToResizeRows = false;
            this.dtgSalaryDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgSalaryDetails.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dtgSalaryDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgSalaryDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dtgSalaryDetails.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dtgSalaryDetails.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.ButtonLowProfile;
            this.dtgSalaryDetails.Location = new System.Drawing.Point(15, 96);
            this.dtgSalaryDetails.Margin = new System.Windows.Forms.Padding(4);
            this.dtgSalaryDetails.MultiSelect = false;
            this.dtgSalaryDetails.Name = "dtgSalaryDetails";
            this.dtgSalaryDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtgSalaryDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgSalaryDetails.Size = new System.Drawing.Size(998, 280);
            this.dtgSalaryDetails.TabIndex = 44;
            this.dtgSalaryDetails.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dtgSalaryDetails_CellBeginEdit);
            this.dtgSalaryDetails.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgSalaryDetails_CellDoubleClick);
            this.dtgSalaryDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgSalaryDetails_CellEndEdit);
            this.dtgSalaryDetails.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgSalaryDetails_CellValueChanged);
            this.dtgSalaryDetails.CurrentCellDirtyStateChanged += new System.EventHandler(this.dtgSalaryDetails_CurrentCellDirtyStateChanged);
            this.dtgSalaryDetails.Paint += new System.Windows.Forms.PaintEventHandler(this.dtgSalaryDetails_Paint);
            // 
            // chkFlipOnOff
            // 
            this.errValidator.SetIconAlignment(this.chkFlipOnOff, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.chkFlipOnOff.Location = new System.Drawing.Point(194, 66);
            this.chkFlipOnOff.Name = "chkFlipOnOff";
            this.chkFlipOnOff.Size = new System.Drawing.Size(142, 28);
            this.chkFlipOnOff.TabIndex = 87;
            this.chkFlipOnOff.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.chkFlipOnOff.Values.Text = "Invert Selection";
            this.chkFlipOnOff.Click += new System.EventHandler(this.chkFlipOnOff_Click);
            // 
            // label10
            // 
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(10, 500);
            this.label10.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(567, 30);
            this.label10.TabIndex = 86;
            this.label10.Text = "⚠ Some employees have pending advances, so they cannot be included in batch salar" +
    "y processing.\nPlease complete individual salary processing for those employees t" +
    "o settle the advance.";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkSelectUnSelect
            // 
            this.chkSelectUnSelect.AutoSize = true;
            this.chkSelectUnSelect.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSelectUnSelect.Checked = true;
            this.chkSelectUnSelect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSelectUnSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkSelectUnSelect.Location = new System.Drawing.Point(12, 71);
            this.chkSelectUnSelect.Name = "chkSelectUnSelect";
            this.chkSelectUnSelect.Size = new System.Drawing.Size(160, 19);
            this.chkSelectUnSelect.TabIndex = 75;
            this.chkSelectUnSelect.Text = "Select All Employees";
            this.chkSelectUnSelect.UseVisualStyleBackColor = true;
            this.chkSelectUnSelect.CheckedChanged += new System.EventHandler(this.chkSelectUnSelect_CheckedChanged);
            // 
            // btnBatchProcess
            // 
            this.btnBatchProcess.Location = new System.Drawing.Point(344, 27);
            this.btnBatchProcess.Margin = new System.Windows.Forms.Padding(4);
            this.btnBatchProcess.Name = "btnBatchProcess";
            this.btnBatchProcess.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnBatchProcess.Size = new System.Drawing.Size(135, 25);
            this.btnBatchProcess.TabIndex = 62;
            this.btnBatchProcess.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnBatchProcess.Values.Text = "Initate Batch Process";
            this.btnBatchProcess.Click += new System.EventHandler(this.btnBatchProcess_Click);
            // 
            // picDownloadDataAsCSV
            // 
            this.picDownloadDataAsCSV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picDownloadDataAsCSV.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picDownloadDataAsCSV.Image = global::StaffSync.Properties.Resources.download01;
            this.picDownloadDataAsCSV.Location = new System.Drawing.Point(942, 70);
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
            this.pictureBox4.Location = new System.Drawing.Point(965, 69);
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
            this.picRefreshBankList.Location = new System.Drawing.Point(990, 69);
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
            this.dtSalaryDate.Location = new System.Drawing.Point(833, 29);
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
            this.label29.Location = new System.Drawing.Point(744, 32);
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
            this.cmbSalaryMonth.Location = new System.Drawing.Point(112, 28);
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
            this.label42.Location = new System.Drawing.Point(10, 32);
            this.label42.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(91, 15);
            this.label42.TabIndex = 45;
            this.label42.Text = "Salary Month";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.lblNetPayable);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.lblTotalReimbursement);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.lblTotalDeductions);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.lblTotalEarnings);
            this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox3.Location = new System.Drawing.Point(15, 389);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(998, 67);
            this.groupBox3.TabIndex = 101;
            this.groupBox3.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(783, 39);
            this.label13.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 15);
            this.label13.TabIndex = 104;
            this.label13.Text = "Net Payable";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNetPayable
            // 
            this.lblNetPayable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNetPayable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetPayable.Location = new System.Drawing.Point(870, 35);
            this.lblNetPayable.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblNetPayable.Name = "lblNetPayable";
            this.lblNetPayable.Size = new System.Drawing.Size(130, 22);
            this.lblNetPayable.TabIndex = 103;
            this.lblNetPayable.Text = "0.00";
            this.lblNetPayable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(497, 39);
            this.label15.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(145, 15);
            this.label15.TabIndex = 102;
            this.label15.Text = "Reimbursement Total";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalReimbursement
            // 
            this.lblTotalReimbursement.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalReimbursement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalReimbursement.Location = new System.Drawing.Point(646, 35);
            this.lblTotalReimbursement.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTotalReimbursement.Name = "lblTotalReimbursement";
            this.lblTotalReimbursement.Size = new System.Drawing.Size(130, 22);
            this.lblTotalReimbursement.TabIndex = 101;
            this.lblTotalReimbursement.Text = "0.00";
            this.lblTotalReimbursement.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(245, 39);
            this.label14.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(115, 15);
            this.label14.TabIndex = 100;
            this.label14.Text = "Deductions Total";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalDeductions
            // 
            this.lblTotalDeductions.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalDeductions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalDeductions.Location = new System.Drawing.Point(362, 35);
            this.lblTotalDeductions.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTotalDeductions.Name = "lblTotalDeductions";
            this.lblTotalDeductions.Size = new System.Drawing.Size(130, 22);
            this.lblTotalDeductions.TabIndex = 99;
            this.lblTotalDeductions.Text = "0.00";
            this.lblTotalDeductions.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(-1, 39);
            this.label11.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 15);
            this.label11.TabIndex = 98;
            this.label11.Text = "Earnings Total";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalEarnings
            // 
            this.lblTotalEarnings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalEarnings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalEarnings.Location = new System.Drawing.Point(102, 35);
            this.lblTotalEarnings.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTotalEarnings.Name = "lblTotalEarnings";
            this.lblTotalEarnings.Size = new System.Drawing.Size(130, 22);
            this.lblTotalEarnings.TabIndex = 97;
            this.lblTotalEarnings.Text = "0.00";
            this.lblTotalEarnings.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTotalWorkedDays
            // 
            this.txtTotalWorkedDays.Location = new System.Drawing.Point(1372, 94);
            this.txtTotalWorkedDays.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtTotalWorkedDays.MaxLength = 255;
            this.txtTotalWorkedDays.Multiline = true;
            this.txtTotalWorkedDays.Name = "txtTotalWorkedDays";
            this.txtTotalWorkedDays.Size = new System.Drawing.Size(54, 25);
            this.txtTotalWorkedDays.TabIndex = 73;
            this.txtTotalWorkedDays.Tag = "Please enter Employeee Code";
            this.txtTotalWorkedDays.Text = "0";
            this.txtTotalWorkedDays.Visible = false;
            this.txtTotalWorkedDays.WordWrap = false;
            // 
            // txtTotalWorkingDays
            // 
            this.txtTotalWorkingDays.Location = new System.Drawing.Point(1372, 54);
            this.txtTotalWorkingDays.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtTotalWorkingDays.MaxLength = 255;
            this.txtTotalWorkingDays.Multiline = true;
            this.txtTotalWorkingDays.Name = "txtTotalWorkingDays";
            this.txtTotalWorkingDays.ReadOnly = true;
            this.txtTotalWorkingDays.Size = new System.Drawing.Size(54, 25);
            this.txtTotalWorkingDays.TabIndex = 71;
            this.txtTotalWorkingDays.Tag = "Please enter Employeee Code";
            this.txtTotalWorkingDays.Text = "0";
            this.txtTotalWorkingDays.Visible = false;
            this.txtTotalWorkingDays.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1274, 59);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 15);
            this.label1.TabIndex = 72;
            this.label1.Text = "Working Days";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Visible = false;
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
            this.panel2.Size = new System.Drawing.Size(1065, 60);
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
            this.btnGenerateDetails.Enabled = false;
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
            this.ClientSize = new System.Drawing.Size(1065, 652);
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
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSalaryDetails1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSalaryDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDownloadDataAsCSV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRefreshBankList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSalaryMonth)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
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
        private Krypton.Toolkit.KryptonButton btnBatchProcess;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTotalPayableDays;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUnpaidLeaves;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLeaveDays;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTotalWorkedDays;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTotalWorkingDays;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblBasicSalaryPerHour;
        private System.Windows.Forms.Label lblBasicSalaryPerDay;
        private System.Windows.Forms.Label lblBasicSalary;
        private Krypton.Toolkit.KryptonDataGridView dtgSalaryDetails1;
        private System.Windows.Forms.CheckBox chkAutoCalculate;
        public System.Windows.Forms.Label lblActionMode;
        private System.Windows.Forms.Label lblReportingManagerID;
        private System.Windows.Forms.Label lblSexID;
        private System.Windows.Forms.Label lblStateID;
        private System.Windows.Forms.Label label5;
        private Krypton.Toolkit.KryptonTextBox txtReimbursement;
        private Krypton.Toolkit.KryptonTextBox txtDeductions;
        private Krypton.Toolkit.KryptonTextBox txtAallowences;
        private Krypton.Toolkit.KryptonTextBox txtNetPayable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkSelectUnSelect;
        private System.Windows.Forms.Label label10;
        private Krypton.Toolkit.KryptonCheckButton chkFlipOnOff;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblSalarySummary;
        private Krypton.Toolkit.KryptonCheckButton chkExcludeEmpWithPendingAdvances;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblNetPayable;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblTotalReimbursement;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblTotalDeductions;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblTotalEarnings;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblPFCalcAmount;
    }
}