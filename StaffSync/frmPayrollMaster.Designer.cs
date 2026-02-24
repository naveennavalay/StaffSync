namespace StaffSync
{
    partial class frmPayrollMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPayrollMaster));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblBasicSalaryPerHour = new System.Windows.Forms.Label();
            this.lblBasicSalaryPerDay = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTotalPayableDays = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUnpaidLeaves = new System.Windows.Forms.TextBox();
            this.lblBasicSalary = new System.Windows.Forms.Label();
            this.chkAutoCalculate = new System.Windows.Forms.CheckBox();
            this.btnViewCalender = new Krypton.Toolkit.KryptonButton();
            this.label5 = new System.Windows.Forms.Label();
            this.txtReimbursement = new Krypton.Toolkit.KryptonTextBox();
            this.txtDeductions = new Krypton.Toolkit.KryptonTextBox();
            this.txtAallowences = new Krypton.Toolkit.KryptonTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtNetPayable = new Krypton.Toolkit.KryptonTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLeaveDays = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotalWorkedDays = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTotalWorkingDays = new System.Windows.Forms.TextBox();
            this.txtSalaryDate = new System.Windows.Forms.MaskedTextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.cmbSalaryMonth = new Krypton.Toolkit.KryptonComboBox();
            this.label42 = new System.Windows.Forms.Label();
            this.tabControl1 = new Krypton.Docking.KryptonDockableNavigator();
            this.tabSalaryHeaders = new Krypton.Navigator.KryptonPage();
            this.dtgSalaryDetails = new Krypton.Toolkit.KryptonDataGridView();
            this.tabAdvanceHeaders = new Krypton.Navigator.KryptonPage();
            this.dtgAdvanceDetails = new Krypton.Toolkit.KryptonDataGridView();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lblSexID = new System.Windows.Forms.Label();
            this.lblStateID = new System.Windows.Forms.Label();
            this.lblSelectedMonthSalaryID = new System.Windows.Forms.Label();
            this.btnReportingManagerSearch = new Krypton.Toolkit.KryptonButton();
            this.txtRepEmpDepartment = new Krypton.Toolkit.KryptonTextBox();
            this.txtRepEmpDesig = new Krypton.Toolkit.KryptonTextBox();
            this.txtRepEmpName = new Krypton.Toolkit.KryptonTextBox();
            this.txtRepEmpCode = new Krypton.Toolkit.KryptonTextBox();
            this.lblActionMode = new System.Windows.Forms.Label();
            this.lblReportingManagerID = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.picRepEmpPhoto = new System.Windows.Forms.PictureBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCloseMe = new Krypton.Toolkit.KryptonButton();
            this.btnRemoveDetails = new Krypton.Toolkit.KryptonButton();
            this.btnSaveDetails = new Krypton.Toolkit.KryptonButton();
            this.btnModifyDetails = new Krypton.Toolkit.KryptonButton();
            this.btnGenerateDetails = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.errValidator = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSalaryMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabSalaryHeaders)).BeginInit();
            this.tabSalaryHeaders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgSalaryDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabAdvanceHeaders)).BeginInit();
            this.tabAdvanceHeaders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAdvanceDetails)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRepEmpPhoto)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(1063, 699);
            this.splitContainer1.SplitterDistance = 631;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox8);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1063, 631);
            this.panel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupBox1.Controls.Add(this.lblBasicSalaryPerHour);
            this.groupBox1.Controls.Add(this.lblBasicSalaryPerDay);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtTotalPayableDays);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtUnpaidLeaves);
            this.groupBox1.Controls.Add(this.lblBasicSalary);
            this.groupBox1.Controls.Add(this.chkAutoCalculate);
            this.groupBox1.Controls.Add(this.btnViewCalender);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtReimbursement);
            this.groupBox1.Controls.Add(this.txtDeductions);
            this.groupBox1.Controls.Add(this.txtAallowences);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.txtNetPayable);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtLeaveDays);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtTotalWorkedDays);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtTotalWorkingDays);
            this.groupBox1.Controls.Add(this.txtSalaryDate);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Controls.Add(this.cmbSalaryMonth);
            this.groupBox1.Controls.Add(this.label42);
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(16, 178);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1032, 443);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            // 
            // lblBasicSalaryPerHour
            // 
            this.lblBasicSalaryPerHour.AutoSize = true;
            this.lblBasicSalaryPerHour.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblBasicSalaryPerHour.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBasicSalaryPerHour.Location = new System.Drawing.Point(841, 53);
            this.lblBasicSalaryPerHour.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblBasicSalaryPerHour.Name = "lblBasicSalaryPerHour";
            this.lblBasicSalaryPerHour.Size = new System.Drawing.Size(11, 15);
            this.lblBasicSalaryPerHour.TabIndex = 72;
            this.lblBasicSalaryPerHour.Text = " ";
            // 
            // lblBasicSalaryPerDay
            // 
            this.lblBasicSalaryPerDay.AutoSize = true;
            this.lblBasicSalaryPerDay.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblBasicSalaryPerDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBasicSalaryPerDay.Location = new System.Drawing.Point(841, 31);
            this.lblBasicSalaryPerDay.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblBasicSalaryPerDay.Name = "lblBasicSalaryPerDay";
            this.lblBasicSalaryPerDay.Size = new System.Drawing.Size(11, 15);
            this.lblBasicSalaryPerDay.TabIndex = 71;
            this.lblBasicSalaryPerDay.Text = " ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(667, 58);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 15);
            this.label7.TabIndex = 70;
            this.label7.Text = "Payable Days";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTotalPayableDays
            // 
            this.txtTotalPayableDays.Location = new System.Drawing.Point(763, 55);
            this.txtTotalPayableDays.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtTotalPayableDays.MaxLength = 255;
            this.txtTotalPayableDays.Multiline = true;
            this.txtTotalPayableDays.Name = "txtTotalPayableDays";
            this.txtTotalPayableDays.Size = new System.Drawing.Size(54, 25);
            this.txtTotalPayableDays.TabIndex = 69;
            this.txtTotalPayableDays.Tag = "Please enter Employeee Code";
            this.txtTotalPayableDays.Text = "0";
            this.txtTotalPayableDays.WordWrap = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(498, 58);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 15);
            this.label6.TabIndex = 68;
            this.label6.Text = "Unpaid Leaves";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUnpaidLeaves
            // 
            this.txtUnpaidLeaves.Location = new System.Drawing.Point(603, 53);
            this.txtUnpaidLeaves.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtUnpaidLeaves.MaxLength = 255;
            this.txtUnpaidLeaves.Multiline = true;
            this.txtUnpaidLeaves.Name = "txtUnpaidLeaves";
            this.txtUnpaidLeaves.Size = new System.Drawing.Size(54, 25);
            this.txtUnpaidLeaves.TabIndex = 67;
            this.txtUnpaidLeaves.Tag = "Please enter Employeee Code";
            this.txtUnpaidLeaves.Text = "0";
            this.txtUnpaidLeaves.WordWrap = false;
            // 
            // lblBasicSalary
            // 
            this.lblBasicSalary.AutoSize = true;
            this.lblBasicSalary.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblBasicSalary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBasicSalary.Location = new System.Drawing.Point(841, 10);
            this.lblBasicSalary.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblBasicSalary.Name = "lblBasicSalary";
            this.lblBasicSalary.Size = new System.Drawing.Size(11, 15);
            this.lblBasicSalary.TabIndex = 66;
            this.lblBasicSalary.Text = " ";
            // 
            // chkAutoCalculate
            // 
            this.chkAutoCalculate.AutoSize = true;
            this.chkAutoCalculate.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAutoCalculate.Checked = true;
            this.chkAutoCalculate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCalculate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkAutoCalculate.Location = new System.Drawing.Point(887, 96);
            this.chkAutoCalculate.Name = "chkAutoCalculate";
            this.chkAutoCalculate.Size = new System.Drawing.Size(118, 19);
            this.chkAutoCalculate.TabIndex = 63;
            this.chkAutoCalculate.Text = "Auto Calculate";
            this.chkAutoCalculate.UseVisualStyleBackColor = true;
            this.chkAutoCalculate.Visible = false;
            // 
            // btnViewCalender
            // 
            this.btnViewCalender.Location = new System.Drawing.Point(724, 18);
            this.btnViewCalender.Margin = new System.Windows.Forms.Padding(4);
            this.btnViewCalender.Name = "btnViewCalender";
            this.btnViewCalender.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnViewCalender.Size = new System.Drawing.Size(27, 28);
            this.btnViewCalender.TabIndex = 62;
            this.btnViewCalender.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnViewCalender.Values.Image = global::StaffSync.Properties.Resources.attendance;
            this.btnViewCalender.Values.Text = "";
            this.btnViewCalender.Visible = false;
            this.btnViewCalender.Click += new System.EventHandler(this.btnViewCalender_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(513, 373);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 15);
            this.label5.TabIndex = 61;
            this.label5.Text = "Total";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtReimbursement
            // 
            this.txtReimbursement.Location = new System.Drawing.Point(831, 366);
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
            this.txtReimbursement.TabIndex = 60;
            this.txtReimbursement.Text = "0.00";
            this.txtReimbursement.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtReimbursement.WordWrap = false;
            // 
            // txtDeductions
            // 
            this.txtDeductions.Location = new System.Drawing.Point(696, 366);
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
            this.txtDeductions.TabIndex = 59;
            this.txtDeductions.Text = "0.00";
            this.txtDeductions.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDeductions.WordWrap = false;
            // 
            // txtAallowences
            // 
            this.txtAallowences.Location = new System.Drawing.Point(561, 366);
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
            this.txtAallowences.TabIndex = 58;
            this.txtAallowences.Text = "0.00";
            this.txtAallowences.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAallowences.WordWrap = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(0, 79);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1312, 10);
            this.groupBox2.TabIndex = 57;
            this.groupBox2.TabStop = false;
            // 
            // txtNetPayable
            // 
            this.txtNetPayable.Location = new System.Drawing.Point(831, 402);
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
            this.txtNetPayable.TabIndex = 56;
            this.txtNetPayable.Text = "0.00";
            this.txtNetPayable.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtNetPayable.WordWrap = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(315, 409);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(515, 15);
            this.label4.TabIndex = 55;
            this.label4.Text = "Net Payable For the Month (Total Earnings - Total Deductions + Reimbursment)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(346, 58);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 15);
            this.label3.TabIndex = 54;
            this.label3.Text = "Paid Leaves";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLeaveDays
            // 
            this.txtLeaveDays.Location = new System.Drawing.Point(434, 53);
            this.txtLeaveDays.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtLeaveDays.MaxLength = 255;
            this.txtLeaveDays.Multiline = true;
            this.txtLeaveDays.Name = "txtLeaveDays";
            this.txtLeaveDays.Size = new System.Drawing.Size(54, 25);
            this.txtLeaveDays.TabIndex = 53;
            this.txtLeaveDays.Tag = "Please enter Employeee Code";
            this.txtLeaveDays.Text = "0";
            this.txtLeaveDays.WordWrap = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(189, 58);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 15);
            this.label2.TabIndex = 52;
            this.label2.Text = "Days Worked";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTotalWorkedDays
            // 
            this.txtTotalWorkedDays.Location = new System.Drawing.Point(282, 53);
            this.txtTotalWorkedDays.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtTotalWorkedDays.MaxLength = 255;
            this.txtTotalWorkedDays.Multiline = true;
            this.txtTotalWorkedDays.Name = "txtTotalWorkedDays";
            this.txtTotalWorkedDays.Size = new System.Drawing.Size(54, 25);
            this.txtTotalWorkedDays.TabIndex = 51;
            this.txtTotalWorkedDays.Tag = "Please enter Employeee Code";
            this.txtTotalWorkedDays.Text = "0";
            this.txtTotalWorkedDays.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 15);
            this.label1.TabIndex = 50;
            this.label1.Text = "Working Days";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTotalWorkingDays
            // 
            this.txtTotalWorkingDays.Location = new System.Drawing.Point(125, 53);
            this.txtTotalWorkingDays.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtTotalWorkingDays.MaxLength = 255;
            this.txtTotalWorkingDays.Multiline = true;
            this.txtTotalWorkingDays.Name = "txtTotalWorkingDays";
            this.txtTotalWorkingDays.ReadOnly = true;
            this.txtTotalWorkingDays.Size = new System.Drawing.Size(54, 25);
            this.txtTotalWorkingDays.TabIndex = 49;
            this.txtTotalWorkingDays.Tag = "Please enter Employeee Code";
            this.txtTotalWorkingDays.Text = "0";
            this.txtTotalWorkingDays.WordWrap = false;
            // 
            // txtSalaryDate
            // 
            this.txtSalaryDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(0)))), ((int)(((byte)(254)))));
            this.txtSalaryDate.Location = new System.Drawing.Point(603, 22);
            this.txtSalaryDate.Margin = new System.Windows.Forms.Padding(4);
            this.txtSalaryDate.Mask = "##-##-####";
            this.txtSalaryDate.Name = "txtSalaryDate";
            this.txtSalaryDate.Size = new System.Drawing.Size(120, 21);
            this.txtSalaryDate.TabIndex = 47;
            this.txtSalaryDate.Tag = "Please enter Employeee Date Of Birth";
            this.txtSalaryDate.ValidatingType = typeof(System.DateTime);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(519, 25);
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
            this.cmbSalaryMonth.Location = new System.Drawing.Point(125, 21);
            this.cmbSalaryMonth.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSalaryMonth.Name = "cmbSalaryMonth";
            this.cmbSalaryMonth.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbSalaryMonth.Size = new System.Drawing.Size(211, 22);
            this.cmbSalaryMonth.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbSalaryMonth.TabIndex = 46;
            this.cmbSalaryMonth.SelectedIndexChanged += new System.EventHandler(this.cmbSalaryMonth_SelectedIndexChanged);
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(32, 25);
            this.label42.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(91, 15);
            this.label42.TabIndex = 45;
            this.label42.Text = "Salary Month";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabControl1
            // 
            this.tabControl1.Bar.BarMapExtraText = Krypton.Navigator.MapKryptonPageText.None;
            this.tabControl1.Bar.BarMapImage = Krypton.Navigator.MapKryptonPageImage.Small;
            this.tabControl1.Bar.BarMapText = Krypton.Navigator.MapKryptonPageText.TextTitle;
            this.tabControl1.Bar.BarMultiline = Krypton.Navigator.BarMultiline.Singleline;
            this.tabControl1.Bar.BarOrientation = Krypton.Toolkit.VisualOrientation.Top;
            this.tabControl1.Bar.CheckButtonStyle = Krypton.Toolkit.ButtonStyle.Standalone;
            this.tabControl1.Bar.ItemAlignment = Krypton.Toolkit.RelativePositionAlign.Near;
            this.tabControl1.Bar.ItemMaximumSize = new System.Drawing.Size(200, 200);
            this.tabControl1.Bar.ItemMinimumSize = new System.Drawing.Size(20, 20);
            this.tabControl1.Bar.ItemOrientation = Krypton.Toolkit.ButtonOrientation.FixedTop;
            this.tabControl1.Bar.ItemSizing = Krypton.Navigator.BarItemSizing.SameWidthAndHeight;
            this.tabControl1.Bar.TabBorderStyle = Krypton.Toolkit.TabBorderStyle.RoundedOutsizeLarge;
            this.tabControl1.Bar.TabStyle = Krypton.Toolkit.TabStyle.HighProfile;
            this.tabControl1.Button.ButtonDisplayLogic = Krypton.Navigator.ButtonDisplayLogic.None;
            this.tabControl1.Button.CloseButtonAction = Krypton.Navigator.CloseButtonAction.None;
            this.tabControl1.Button.CloseButtonDisplay = Krypton.Navigator.ButtonDisplay.Hide;
            this.tabControl1.Button.ContextButtonAction = Krypton.Navigator.ContextButtonAction.SelectPage;
            this.tabControl1.Button.ContextButtonDisplay = Krypton.Navigator.ButtonDisplay.Logic;
            this.tabControl1.Button.ContextMenuMapImage = Krypton.Navigator.MapKryptonPageImage.Small;
            this.tabControl1.Button.ContextMenuMapText = Krypton.Navigator.MapKryptonPageText.TextTitle;
            this.tabControl1.Button.NextButtonAction = Krypton.Navigator.DirectionButtonAction.None;
            this.tabControl1.Button.NextButtonDisplay = Krypton.Navigator.ButtonDisplay.Logic;
            this.tabControl1.Button.PreviousButtonAction = Krypton.Navigator.DirectionButtonAction.ModeAppropriateAction;
            this.tabControl1.Button.PreviousButtonDisplay = Krypton.Navigator.ButtonDisplay.Logic;
            this.tabControl1.ControlKryptonFormFeatures = false;
            this.tabControl1.Group.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.PanelClient;
            this.tabControl1.Group.GroupBorderStyle = Krypton.Toolkit.PaletteBorderStyle.ButtonStandalone;
            this.tabControl1.Location = new System.Drawing.Point(7, 96);
            this.tabControl1.NavigatorMode = Krypton.Navigator.NavigatorMode.BarTabGroup;
            this.tabControl1.Outlook.BorderEdgeStyle = Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.tabControl1.Outlook.CheckButtonStyle = Krypton.Toolkit.ButtonStyle.Standalone;
            this.tabControl1.Outlook.HeaderSecondaryVisible = Krypton.Toolkit.InheritBool.False;
            this.tabControl1.Outlook.ItemOrientation = Krypton.Toolkit.ButtonOrientation.Auto;
            this.tabControl1.Outlook.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tabControl1.Outlook.OverflowButtonStyle = Krypton.Toolkit.ButtonStyle.NavigatorStack;
            this.tabControl1.Owner = null;
            this.tabControl1.PageBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonNavigatorMini;
            this.tabControl1.Pages.AddRange(new Krypton.Navigator.KryptonPage[] {
            this.tabSalaryHeaders,
            this.tabAdvanceHeaders});
            this.tabControl1.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.tabControl1.Panel.PanelBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonLowProfile;
            this.tabControl1.PopupPages.AllowPopupPages = Krypton.Navigator.PopupPageAllow.Never;
            this.tabControl1.PopupPages.Element = Krypton.Navigator.PopupPageElement.Item;
            this.tabControl1.PopupPages.Position = Krypton.Navigator.PopupPagePosition.ModeAppropriate;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1006, 265);
            this.tabControl1.TabIndex = 64;
            // 
            // tabSalaryHeaders
            // 
            this.tabSalaryHeaders.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.tabSalaryHeaders.AutoScroll = true;
            this.tabSalaryHeaders.Controls.Add(this.dtgSalaryDetails);
            this.tabSalaryHeaders.Flags = 65534;
            this.tabSalaryHeaders.LastVisibleSet = true;
            this.tabSalaryHeaders.MinimumSize = new System.Drawing.Size(50, 50);
            this.tabSalaryHeaders.Name = "tabSalaryHeaders";
            this.tabSalaryHeaders.Size = new System.Drawing.Size(1002, 236);
            this.tabSalaryHeaders.Text = "Salary Headers";
            this.tabSalaryHeaders.TextDescription = "";
            this.tabSalaryHeaders.ToolTipTitle = "Page ToolTip";
            this.tabSalaryHeaders.UniqueName = "410b78002c6e42e38be14684af8f8fb9";
            // 
            // dtgSalaryDetails
            // 
            this.dtgSalaryDetails.AllowUserToAddRows = false;
            this.dtgSalaryDetails.AllowUserToDeleteRows = false;
            this.dtgSalaryDetails.AllowUserToResizeRows = false;
            this.dtgSalaryDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgSalaryDetails.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dtgSalaryDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgSalaryDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgSalaryDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dtgSalaryDetails.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dtgSalaryDetails.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.ButtonLowProfile;
            this.dtgSalaryDetails.Location = new System.Drawing.Point(0, 0);
            this.dtgSalaryDetails.Margin = new System.Windows.Forms.Padding(4);
            this.dtgSalaryDetails.MultiSelect = false;
            this.dtgSalaryDetails.Name = "dtgSalaryDetails";
            this.dtgSalaryDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtgSalaryDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgSalaryDetails.Size = new System.Drawing.Size(1002, 236);
            this.dtgSalaryDetails.TabIndex = 45;
            this.dtgSalaryDetails.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dtgSalaryDetails_CellBeginEdit);
            this.dtgSalaryDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgSalaryDetails_CellEndEdit);
            this.dtgSalaryDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dtgSalaryDetails_KeyDown);
            // 
            // tabAdvanceHeaders
            // 
            this.tabAdvanceHeaders.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.tabAdvanceHeaders.Controls.Add(this.dtgAdvanceDetails);
            this.tabAdvanceHeaders.Enabled = false;
            this.tabAdvanceHeaders.Flags = 65534;
            this.tabAdvanceHeaders.LastVisibleSet = true;
            this.tabAdvanceHeaders.MinimumSize = new System.Drawing.Size(150, 50);
            this.tabAdvanceHeaders.Name = "tabAdvanceHeaders";
            this.tabAdvanceHeaders.Size = new System.Drawing.Size(1002, 236);
            this.tabAdvanceHeaders.Text = "Advance Headers";
            this.tabAdvanceHeaders.ToolTipTitle = "Page ToolTip";
            this.tabAdvanceHeaders.UniqueName = "7b4809ade98340beb18a313b3e589dac";
            // 
            // dtgAdvanceDetails
            // 
            this.dtgAdvanceDetails.AllowUserToAddRows = false;
            this.dtgAdvanceDetails.AllowUserToDeleteRows = false;
            this.dtgAdvanceDetails.AllowUserToResizeRows = false;
            this.dtgAdvanceDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgAdvanceDetails.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dtgAdvanceDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgAdvanceDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgAdvanceDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dtgAdvanceDetails.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dtgAdvanceDetails.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.ButtonLowProfile;
            this.dtgAdvanceDetails.Location = new System.Drawing.Point(0, 0);
            this.dtgAdvanceDetails.Margin = new System.Windows.Forms.Padding(4);
            this.dtgAdvanceDetails.MultiSelect = false;
            this.dtgAdvanceDetails.Name = "dtgAdvanceDetails";
            this.dtgAdvanceDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtgAdvanceDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgAdvanceDetails.Size = new System.Drawing.Size(1002, 236);
            this.dtgAdvanceDetails.TabIndex = 46;
            this.dtgAdvanceDetails.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgAdvanceDetails_CellDoubleClick);
            this.dtgAdvanceDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgAdvanceDetails_CellEndEdit);
            this.dtgAdvanceDetails.DoubleClick += new System.EventHandler(this.dtgAdvanceDetails_DoubleClick);
            // 
            // groupBox8
            // 
            this.groupBox8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupBox8.Controls.Add(this.lblSexID);
            this.groupBox8.Controls.Add(this.lblStateID);
            this.groupBox8.Controls.Add(this.lblSelectedMonthSalaryID);
            this.groupBox8.Controls.Add(this.btnReportingManagerSearch);
            this.groupBox8.Controls.Add(this.txtRepEmpDepartment);
            this.groupBox8.Controls.Add(this.txtRepEmpDesig);
            this.groupBox8.Controls.Add(this.txtRepEmpName);
            this.groupBox8.Controls.Add(this.txtRepEmpCode);
            this.groupBox8.Controls.Add(this.lblActionMode);
            this.groupBox8.Controls.Add(this.lblReportingManagerID);
            this.groupBox8.Controls.Add(this.label38);
            this.groupBox8.Controls.Add(this.picRepEmpPhoto);
            this.groupBox8.Controls.Add(this.label37);
            this.groupBox8.Controls.Add(this.label36);
            this.groupBox8.Controls.Add(this.label35);
            this.groupBox8.Controls.Add(this.label34);
            this.groupBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox8.Location = new System.Drawing.Point(16, 10);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox8.Size = new System.Drawing.Size(1032, 174);
            this.groupBox8.TabIndex = 33;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "User Information";
            // 
            // lblSexID
            // 
            this.lblSexID.AutoSize = true;
            this.lblSexID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblSexID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSexID.Location = new System.Drawing.Point(760, 98);
            this.lblSexID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSexID.Name = "lblSexID";
            this.lblSexID.Size = new System.Drawing.Size(11, 15);
            this.lblSexID.TabIndex = 35;
            this.lblSexID.Text = " ";
            this.lblSexID.Visible = false;
            // 
            // lblStateID
            // 
            this.lblStateID.AutoSize = true;
            this.lblStateID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblStateID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStateID.Location = new System.Drawing.Point(760, 68);
            this.lblStateID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblStateID.Name = "lblStateID";
            this.lblStateID.Size = new System.Drawing.Size(11, 15);
            this.lblStateID.TabIndex = 34;
            this.lblStateID.Text = " ";
            this.lblStateID.Visible = false;
            // 
            // lblSelectedMonthSalaryID
            // 
            this.lblSelectedMonthSalaryID.AutoSize = true;
            this.lblSelectedMonthSalaryID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblSelectedMonthSalaryID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedMonthSalaryID.Location = new System.Drawing.Point(652, 33);
            this.lblSelectedMonthSalaryID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSelectedMonthSalaryID.Name = "lblSelectedMonthSalaryID";
            this.lblSelectedMonthSalaryID.Size = new System.Drawing.Size(11, 15);
            this.lblSelectedMonthSalaryID.TabIndex = 33;
            this.lblSelectedMonthSalaryID.Text = " ";
            this.lblSelectedMonthSalaryID.Visible = false;
            // 
            // btnReportingManagerSearch
            // 
            this.btnReportingManagerSearch.Location = new System.Drawing.Point(301, 25);
            this.btnReportingManagerSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnReportingManagerSearch.Name = "btnReportingManagerSearch";
            this.btnReportingManagerSearch.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnReportingManagerSearch.Size = new System.Drawing.Size(29, 28);
            this.btnReportingManagerSearch.TabIndex = 32;
            this.btnReportingManagerSearch.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnReportingManagerSearch.Values.Image = global::StaffSync.Properties.Resources.search;
            this.btnReportingManagerSearch.Values.Text = "";
            this.btnReportingManagerSearch.Click += new System.EventHandler(this.btnReportingManagerSearch_Click);
            // 
            // txtRepEmpDepartment
            // 
            this.txtRepEmpDepartment.Location = new System.Drawing.Point(125, 135);
            this.txtRepEmpDepartment.Margin = new System.Windows.Forms.Padding(4);
            this.txtRepEmpDepartment.Multiline = true;
            this.txtRepEmpDepartment.Name = "txtRepEmpDepartment";
            this.txtRepEmpDepartment.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtRepEmpDepartment.Size = new System.Drawing.Size(587, 28);
            this.txtRepEmpDepartment.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpDepartment.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtRepEmpDepartment.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpDepartment.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpDepartment.TabIndex = 30;
            // 
            // txtRepEmpDesig
            // 
            this.txtRepEmpDesig.Location = new System.Drawing.Point(125, 98);
            this.txtRepEmpDesig.Margin = new System.Windows.Forms.Padding(4);
            this.txtRepEmpDesig.Multiline = true;
            this.txtRepEmpDesig.Name = "txtRepEmpDesig";
            this.txtRepEmpDesig.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtRepEmpDesig.Size = new System.Drawing.Size(587, 28);
            this.txtRepEmpDesig.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpDesig.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtRepEmpDesig.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpDesig.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpDesig.TabIndex = 29;
            this.txtRepEmpDesig.WordWrap = false;
            // 
            // txtRepEmpName
            // 
            this.txtRepEmpName.Location = new System.Drawing.Point(125, 61);
            this.txtRepEmpName.Margin = new System.Windows.Forms.Padding(4);
            this.txtRepEmpName.Multiline = true;
            this.txtRepEmpName.Name = "txtRepEmpName";
            this.txtRepEmpName.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtRepEmpName.Size = new System.Drawing.Size(587, 28);
            this.txtRepEmpName.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpName.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtRepEmpName.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpName.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpName.TabIndex = 28;
            this.txtRepEmpName.WordWrap = false;
            // 
            // txtRepEmpCode
            // 
            this.txtRepEmpCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRepEmpCode.Location = new System.Drawing.Point(125, 25);
            this.txtRepEmpCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtRepEmpCode.Multiline = true;
            this.txtRepEmpCode.Name = "txtRepEmpCode";
            this.txtRepEmpCode.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtRepEmpCode.ReadOnly = true;
            this.txtRepEmpCode.Size = new System.Drawing.Size(168, 28);
            this.txtRepEmpCode.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpCode.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtRepEmpCode.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpCode.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpCode.TabIndex = 27;
            this.txtRepEmpCode.WordWrap = false;
            // 
            // lblActionMode
            // 
            this.lblActionMode.AutoSize = true;
            this.lblActionMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActionMode.Location = new System.Drawing.Point(509, 33);
            this.lblActionMode.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblActionMode.Name = "lblActionMode";
            this.lblActionMode.Size = new System.Drawing.Size(98, 15);
            this.lblActionMode.TabIndex = 20;
            this.lblActionMode.Text = "lblActionMode";
            this.lblActionMode.Visible = false;
            // 
            // lblReportingManagerID
            // 
            this.lblReportingManagerID.AutoSize = true;
            this.lblReportingManagerID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblReportingManagerID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportingManagerID.Location = new System.Drawing.Point(484, 33);
            this.lblReportingManagerID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblReportingManagerID.Name = "lblReportingManagerID";
            this.lblReportingManagerID.Size = new System.Drawing.Size(11, 15);
            this.lblReportingManagerID.TabIndex = 19;
            this.lblReportingManagerID.Text = " ";
            this.lblReportingManagerID.Visible = false;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(791, 28);
            this.label38.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(44, 15);
            this.label38.TabIndex = 16;
            this.label38.Text = "Photo";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picRepEmpPhoto
            // 
            this.picRepEmpPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picRepEmpPhoto.Location = new System.Drawing.Point(844, 25);
            this.picRepEmpPhoto.Margin = new System.Windows.Forms.Padding(4);
            this.picRepEmpPhoto.Name = "picRepEmpPhoto";
            this.picRepEmpPhoto.Size = new System.Drawing.Size(161, 138);
            this.picRepEmpPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRepEmpPhoto.TabIndex = 15;
            this.picRepEmpPhoto.TabStop = false;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(41, 142);
            this.label37.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(82, 15);
            this.label37.TabIndex = 13;
            this.label37.Text = "Department";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(39, 105);
            this.label36.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(84, 15);
            this.label36.TabIndex = 11;
            this.label36.Text = "Designation";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(78, 68);
            this.label35.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(45, 15);
            this.label35.TabIndex = 9;
            this.label35.Text = "Name";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(16, 33);
            this.label34.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(107, 15);
            this.label34.TabIndex = 6;
            this.label34.Text = "Employee Code";
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
            this.panel2.Size = new System.Drawing.Size(1063, 63);
            this.panel2.TabIndex = 1;
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
            this.btnGenerateDetails.Values.Image = global::StaffSync.Properties.Resources._new;
            this.btnGenerateDetails.Values.Text = "Generate";
            this.btnGenerateDetails.Click += new System.EventHandler(this.btnGenerateDetails_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(568, 12);
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
            // errValidator
            // 
            this.errValidator.ContainerControl = this;
            // 
            // frmPayrollMaster
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1063, 699);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPayrollMaster";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Payroll Master Details";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.frmPayrollMaster_Activated);
            this.Load += new System.EventHandler(this.frmPayrollMaster_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPayrollMaster_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbSalaryMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabSalaryHeaders)).EndInit();
            this.tabSalaryHeaders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgSalaryDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabAdvanceHeaders)).EndInit();
            this.tabAdvanceHeaders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgAdvanceDetails)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRepEmpPhoto)).EndInit();
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
        private System.Windows.Forms.GroupBox groupBox8;
        private Krypton.Toolkit.KryptonButton btnReportingManagerSearch;
        private Krypton.Toolkit.KryptonTextBox txtRepEmpDepartment;
        private Krypton.Toolkit.KryptonTextBox txtRepEmpDesig;
        private Krypton.Toolkit.KryptonTextBox txtRepEmpName;
        private Krypton.Toolkit.KryptonTextBox txtRepEmpCode;
        public System.Windows.Forms.Label lblActionMode;
        private System.Windows.Forms.Label lblReportingManagerID;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.PictureBox picRepEmpPhoto;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.GroupBox groupBox1;
        private Krypton.Toolkit.KryptonComboBox cmbSalaryMonth;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.MaskedTextBox txtSalaryDate;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTotalWorkingDays;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLeaveDays;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTotalWorkedDays;
        private Krypton.Toolkit.KryptonTextBox txtNetPayable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ErrorProvider errValidator;
        private System.Windows.Forms.Label lblSelectedMonthSalaryID;
        private Krypton.Toolkit.KryptonTextBox txtReimbursement;
        private Krypton.Toolkit.KryptonTextBox txtDeductions;
        private Krypton.Toolkit.KryptonTextBox txtAallowences;
        private System.Windows.Forms.Label label5;
        private Krypton.Toolkit.KryptonButton btnViewCalender;
        private System.Windows.Forms.Label lblSexID;
        private System.Windows.Forms.Label lblStateID;
        private Krypton.Docking.KryptonDockableNavigator tabControl1;
        private Krypton.Navigator.KryptonPage tabSalaryHeaders;
        private Krypton.Navigator.KryptonPage tabAdvanceHeaders;
        private Krypton.Toolkit.KryptonDataGridView dtgSalaryDetails;
        private Krypton.Toolkit.KryptonDataGridView dtgAdvanceDetails;
        private System.Windows.Forms.Label lblBasicSalary;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUnpaidLeaves;
        private System.Windows.Forms.TextBox txtTotalPayableDays;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblBasicSalaryPerHour;
        private System.Windows.Forms.Label lblBasicSalaryPerDay;
        private System.Windows.Forms.CheckBox chkAutoCalculate;
    }
}