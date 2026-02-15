namespace StaffSync
{
    partial class frmAdvanceConfigInfoReadOnly
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdvanceConfigInfoReadOnly));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblAdvanceTypeConfigID = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtMaxTenure = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkAutoDeductFromSalary = new System.Windows.Forms.CheckBox();
            this.chkWaiverAllowed = new System.Windows.Forms.CheckBox();
            this.chkAllowPause = new System.Windows.Forms.CheckBox();
            this.chkApprovalNeeded = new System.Windows.Forms.CheckBox();
            this.chkInterestRequired = new System.Windows.Forms.CheckBox();
            this.chkRecoveryRequired = new System.Windows.Forms.CheckBox();
            this.chkIncludeAsDeductionInSalary = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAdvanceAmountFixed = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbAdvanceAmountBased = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAdvancePercentage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbAdvanceBasedOn = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkAutoDeductFromNextSaslary = new System.Windows.Forms.CheckBox();
            this.cmbIsActive = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtAdvanceTitle = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblAdvanceTypeID = new System.Windows.Forms.Label();
            this.lblActionMode = new System.Windows.Forms.Label();
            this.txtAdvanceCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCloseMe = new Krypton.Toolkit.KryptonButton();
            this.errValidator = new System.Windows.Forms.ErrorProvider(this.components);
            this.empMasInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.staffsyncDBDTSet = new StaffSync.StaffsyncDBDTSet();
            this.empMasInfoTableAdapter = new StaffSync.StaffsyncDBDTSetTableAdapters.EmpMasInfoTableAdapter();
            this.staffsyncDBDataSet1 = new StaffSync.StaffsyncDBDataSet1();
            this.qryAllEmpLeavePendingStatementBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qryAllEmpLeavePendingStatementTableAdapter = new StaffSync.StaffsyncDBDataSet1TableAdapters.qryAllEmpLeavePendingStatementTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryAllEmpLeavePendingStatementBindingSource)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(1013, 500);
            this.splitContainer1.SplitterDistance = 439;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1013, 439);
            this.panel1.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblAdvanceTypeConfigID);
            this.groupBox5.Controls.Add(this.groupBox1);
            this.groupBox5.Controls.Add(this.cmbIsActive);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.txtAdvanceTitle);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Enabled = false;
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(10, 78);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(989, 357);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Advance Details";
            // 
            // lblAdvanceTypeConfigID
            // 
            this.lblAdvanceTypeConfigID.AutoSize = true;
            this.lblAdvanceTypeConfigID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblAdvanceTypeConfigID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdvanceTypeConfigID.Location = new System.Drawing.Point(617, 32);
            this.lblAdvanceTypeConfigID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAdvanceTypeConfigID.Name = "lblAdvanceTypeConfigID";
            this.lblAdvanceTypeConfigID.Size = new System.Drawing.Size(11, 15);
            this.lblAdvanceTypeConfigID.TabIndex = 37;
            this.lblAdvanceTypeConfigID.Text = " ";
            this.lblAdvanceTypeConfigID.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtMaxTenure);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.chkAutoDeductFromSalary);
            this.groupBox1.Controls.Add(this.chkWaiverAllowed);
            this.groupBox1.Controls.Add(this.chkAllowPause);
            this.groupBox1.Controls.Add(this.chkApprovalNeeded);
            this.groupBox1.Controls.Add(this.chkInterestRequired);
            this.groupBox1.Controls.Add(this.chkRecoveryRequired);
            this.groupBox1.Controls.Add(this.chkIncludeAsDeductionInSalary);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtAdvanceAmountFixed);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbAdvanceAmountBased);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtAdvancePercentage);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbAdvanceBasedOn);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chkAutoDeductFromNextSaslary);
            this.groupBox1.Enabled = false;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(989, 253);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Advance Configuration";
            // 
            // txtMaxTenure
            // 
            this.txtMaxTenure.Location = new System.Drawing.Point(153, 132);
            this.txtMaxTenure.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtMaxTenure.MaxLength = 255;
            this.txtMaxTenure.Multiline = true;
            this.txtMaxTenure.Name = "txtMaxTenure";
            this.txtMaxTenure.Size = new System.Drawing.Size(134, 28);
            this.txtMaxTenure.TabIndex = 54;
            this.txtMaxTenure.WordWrap = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(64, 139);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 15);
            this.label7.TabIndex = 55;
            this.label7.Text = "Max. Tenure";
            // 
            // chkAutoDeductFromSalary
            // 
            this.chkAutoDeductFromSalary.AutoSize = true;
            this.chkAutoDeductFromSalary.Checked = true;
            this.chkAutoDeductFromSalary.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoDeductFromSalary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkAutoDeductFromSalary.Location = new System.Drawing.Point(569, 186);
            this.chkAutoDeductFromSalary.Name = "chkAutoDeductFromSalary";
            this.chkAutoDeductFromSalary.Size = new System.Drawing.Size(184, 19);
            this.chkAutoDeductFromSalary.TabIndex = 53;
            this.chkAutoDeductFromSalary.Text = "Auto Deduct From Salary";
            this.chkAutoDeductFromSalary.UseVisualStyleBackColor = true;
            // 
            // chkWaiverAllowed
            // 
            this.chkWaiverAllowed.AutoSize = true;
            this.chkWaiverAllowed.Checked = true;
            this.chkWaiverAllowed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWaiverAllowed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkWaiverAllowed.Location = new System.Drawing.Point(812, 220);
            this.chkWaiverAllowed.Name = "chkWaiverAllowed";
            this.chkWaiverAllowed.Size = new System.Drawing.Size(123, 19);
            this.chkWaiverAllowed.TabIndex = 52;
            this.chkWaiverAllowed.Text = "Waiver Allowed";
            this.chkWaiverAllowed.UseVisualStyleBackColor = true;
            // 
            // chkAllowPause
            // 
            this.chkAllowPause.AutoSize = true;
            this.chkAllowPause.Checked = true;
            this.chkAllowPause.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAllowPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkAllowPause.Location = new System.Drawing.Point(812, 186);
            this.chkAllowPause.Name = "chkAllowPause";
            this.chkAllowPause.Size = new System.Drawing.Size(104, 19);
            this.chkAllowPause.TabIndex = 51;
            this.chkAllowPause.Text = "Allow Pause";
            this.chkAllowPause.UseVisualStyleBackColor = true;
            // 
            // chkApprovalNeeded
            // 
            this.chkApprovalNeeded.AutoSize = true;
            this.chkApprovalNeeded.Checked = true;
            this.chkApprovalNeeded.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkApprovalNeeded.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkApprovalNeeded.Location = new System.Drawing.Point(153, 186);
            this.chkApprovalNeeded.Name = "chkApprovalNeeded";
            this.chkApprovalNeeded.Size = new System.Drawing.Size(144, 19);
            this.chkApprovalNeeded.TabIndex = 50;
            this.chkApprovalNeeded.Text = "Approval Required";
            this.chkApprovalNeeded.UseVisualStyleBackColor = true;
            // 
            // chkInterestRequired
            // 
            this.chkInterestRequired.AutoSize = true;
            this.chkInterestRequired.Checked = true;
            this.chkInterestRequired.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInterestRequired.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkInterestRequired.Location = new System.Drawing.Point(328, 186);
            this.chkInterestRequired.Name = "chkInterestRequired";
            this.chkInterestRequired.Size = new System.Drawing.Size(137, 19);
            this.chkInterestRequired.TabIndex = 49;
            this.chkInterestRequired.Text = "Interest Required";
            this.chkInterestRequired.UseVisualStyleBackColor = true;
            // 
            // chkRecoveryRequired
            // 
            this.chkRecoveryRequired.AutoSize = true;
            this.chkRecoveryRequired.Checked = true;
            this.chkRecoveryRequired.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRecoveryRequired.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkRecoveryRequired.Location = new System.Drawing.Point(153, 220);
            this.chkRecoveryRequired.Name = "chkRecoveryRequired";
            this.chkRecoveryRequired.Size = new System.Drawing.Size(147, 19);
            this.chkRecoveryRequired.TabIndex = 47;
            this.chkRecoveryRequired.Text = "Recovery Required";
            this.chkRecoveryRequired.UseVisualStyleBackColor = true;
            // 
            // chkIncludeAsDeductionInSalary
            // 
            this.chkIncludeAsDeductionInSalary.AutoSize = true;
            this.chkIncludeAsDeductionInSalary.Checked = true;
            this.chkIncludeAsDeductionInSalary.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludeAsDeductionInSalary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkIncludeAsDeductionInSalary.Location = new System.Drawing.Point(328, 220);
            this.chkIncludeAsDeductionInSalary.Name = "chkIncludeAsDeductionInSalary";
            this.chkIncludeAsDeductionInSalary.Size = new System.Drawing.Size(221, 19);
            this.chkIncludeAsDeductionInSalary.TabIndex = 46;
            this.chkIncludeAsDeductionInSalary.Text = "Include in Salary as Deduction";
            this.chkIncludeAsDeductionInSalary.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(290, 103);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 15);
            this.label6.TabIndex = 45;
            this.label6.Text = "%";
            // 
            // txtAdvanceAmountFixed
            // 
            this.txtAdvanceAmountFixed.Location = new System.Drawing.Point(412, 96);
            this.txtAdvanceAmountFixed.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtAdvanceAmountFixed.MaxLength = 255;
            this.txtAdvanceAmountFixed.Multiline = true;
            this.txtAdvanceAmountFixed.Name = "txtAdvanceAmountFixed";
            this.txtAdvanceAmountFixed.Size = new System.Drawing.Size(134, 28);
            this.txtAdvanceAmountFixed.TabIndex = 43;
            this.txtAdvanceAmountFixed.WordWrap = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(367, 103);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 15);
            this.label5.TabIndex = 44;
            this.label5.Text = "Fixed";
            // 
            // cmbAdvanceAmountBased
            // 
            this.cmbAdvanceAmountBased.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAdvanceAmountBased.FormattingEnabled = true;
            this.cmbAdvanceAmountBased.Location = new System.Drawing.Point(153, 65);
            this.cmbAdvanceAmountBased.MaxDropDownItems = 5;
            this.cmbAdvanceAmountBased.Name = "cmbAdvanceAmountBased";
            this.cmbAdvanceAmountBased.Size = new System.Drawing.Size(393, 23);
            this.cmbAdvanceAmountBased.TabIndex = 42;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(57, 69);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 15);
            this.label4.TabIndex = 41;
            this.label4.Text = "Advance Type";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAdvancePercentage
            // 
            this.txtAdvancePercentage.Location = new System.Drawing.Point(153, 96);
            this.txtAdvancePercentage.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtAdvancePercentage.MaxLength = 255;
            this.txtAdvancePercentage.Multiline = true;
            this.txtAdvancePercentage.Name = "txtAdvancePercentage";
            this.txtAdvancePercentage.Size = new System.Drawing.Size(134, 28);
            this.txtAdvancePercentage.TabIndex = 39;
            this.txtAdvancePercentage.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(71, 103);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 15);
            this.label3.TabIndex = 40;
            this.label3.Text = "Percentage";
            // 
            // cmbAdvanceBasedOn
            // 
            this.cmbAdvanceBasedOn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAdvanceBasedOn.FormattingEnabled = true;
            this.cmbAdvanceBasedOn.Location = new System.Drawing.Point(153, 34);
            this.cmbAdvanceBasedOn.MaxDropDownItems = 5;
            this.cmbAdvanceBasedOn.Name = "cmbAdvanceBasedOn";
            this.cmbAdvanceBasedOn.Size = new System.Drawing.Size(393, 23);
            this.cmbAdvanceBasedOn.TabIndex = 38;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(82, 38);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 15);
            this.label2.TabIndex = 37;
            this.label2.Text = "Based On";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkAutoDeductFromNextSaslary
            // 
            this.chkAutoDeductFromNextSaslary.AutoSize = true;
            this.chkAutoDeductFromNextSaslary.Checked = true;
            this.chkAutoDeductFromNextSaslary.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoDeductFromNextSaslary.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkAutoDeductFromNextSaslary.Location = new System.Drawing.Point(569, 220);
            this.chkAutoDeductFromNextSaslary.Name = "chkAutoDeductFromNextSaslary";
            this.chkAutoDeductFromNextSaslary.Size = new System.Drawing.Size(217, 19);
            this.chkAutoDeductFromNextSaslary.TabIndex = 36;
            this.chkAutoDeductFromNextSaslary.Text = "Auto Deduct From Next Salary";
            this.chkAutoDeductFromNextSaslary.UseVisualStyleBackColor = true;
            // 
            // cmbIsActive
            // 
            this.cmbIsActive.FormattingEnabled = true;
            this.cmbIsActive.Location = new System.Drawing.Point(153, 61);
            this.cmbIsActive.MaxDropDownItems = 5;
            this.cmbIsActive.Name = "cmbIsActive";
            this.cmbIsActive.Size = new System.Drawing.Size(204, 23);
            this.cmbIsActive.TabIndex = 34;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(92, 65);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(59, 15);
            this.label17.TabIndex = 33;
            this.label17.Text = "Is Active";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAdvanceTitle
            // 
            this.txtAdvanceTitle.Location = new System.Drawing.Point(153, 25);
            this.txtAdvanceTitle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtAdvanceTitle.MaxLength = 255;
            this.txtAdvanceTitle.Multiline = true;
            this.txtAdvanceTitle.Name = "txtAdvanceTitle";
            this.txtAdvanceTitle.Size = new System.Drawing.Size(427, 28);
            this.txtAdvanceTitle.TabIndex = 3;
            this.txtAdvanceTitle.WordWrap = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(59, 32);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(92, 15);
            this.label16.TabIndex = 7;
            this.label16.Text = "Advance Title";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblAdvanceTypeID);
            this.groupBox4.Controls.Add(this.lblActionMode);
            this.groupBox4.Controls.Add(this.txtAdvanceCode);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Enabled = false;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(14, 7);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(985, 65);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Advance Info";
            // 
            // lblAdvanceTypeID
            // 
            this.lblAdvanceTypeID.AutoSize = true;
            this.lblAdvanceTypeID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblAdvanceTypeID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdvanceTypeID.Location = new System.Drawing.Point(592, 30);
            this.lblAdvanceTypeID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAdvanceTypeID.Name = "lblAdvanceTypeID";
            this.lblAdvanceTypeID.Size = new System.Drawing.Size(11, 15);
            this.lblAdvanceTypeID.TabIndex = 5;
            this.lblAdvanceTypeID.Text = " ";
            this.lblAdvanceTypeID.Visible = false;
            // 
            // lblActionMode
            // 
            this.lblActionMode.AutoSize = true;
            this.lblActionMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActionMode.Location = new System.Drawing.Point(440, 30);
            this.lblActionMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblActionMode.Name = "lblActionMode";
            this.lblActionMode.Size = new System.Drawing.Size(98, 15);
            this.lblActionMode.TabIndex = 4;
            this.lblActionMode.Text = "lblActionMode";
            this.lblActionMode.Visible = false;
            // 
            // txtAdvanceCode
            // 
            this.txtAdvanceCode.Location = new System.Drawing.Point(153, 23);
            this.txtAdvanceCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtAdvanceCode.MaxLength = 255;
            this.txtAdvanceCode.Multiline = true;
            this.txtAdvanceCode.Name = "txtAdvanceCode";
            this.txtAdvanceCode.ReadOnly = true;
            this.txtAdvanceCode.Size = new System.Drawing.Size(168, 28);
            this.txtAdvanceCode.TabIndex = 2;
            this.txtAdvanceCode.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(54, 30);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Advance Code";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel2.Controls.Add(this.btnCloseMe);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1013, 56);
            this.panel2.TabIndex = 1;
            // 
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(873, 9);
            this.btnCloseMe.Margin = new System.Windows.Forms.Padding(4);
            this.btnCloseMe.Name = "btnCloseMe";
            this.btnCloseMe.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCloseMe.Size = new System.Drawing.Size(126, 38);
            this.btnCloseMe.TabIndex = 20;
            this.btnCloseMe.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCloseMe.Values.Image = global::StaffSync.Properties.Resources.close;
            this.btnCloseMe.Values.Text = "Close Me";
            this.btnCloseMe.Click += new System.EventHandler(this.btnCloseMe_Click);
            // 
            // errValidator
            // 
            this.errValidator.ContainerControl = this;
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
            // frmAdvanceConfigInfoReadOnly
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1013, 500);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdvanceConfigInfoReadOnly";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Advance Configuration";
            this.Load += new System.EventHandler(this.frmAdvanceConfigInfoReadOnly_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAdvanceConfigInfoReadOnly_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryAllEmpLeavePendingStatementBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private StaffsyncDBDTSet staffsyncDBDTSet;
        private System.Windows.Forms.BindingSource empMasInfoBindingSource;
        private StaffsyncDBDTSetTableAdapters.EmpMasInfoTableAdapter empMasInfoTableAdapter;
        private System.Windows.Forms.ErrorProvider errValidator;
        private Krypton.Toolkit.KryptonButton btnCloseMe;
        private StaffsyncDBDataSet1 staffsyncDBDataSet1;
        private System.Windows.Forms.BindingSource qryAllEmpLeavePendingStatementBindingSource;
        private StaffsyncDBDataSet1TableAdapters.qryAllEmpLeavePendingStatementTableAdapter qryAllEmpLeavePendingStatementTableAdapter;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label lblAdvanceTypeConfigID;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtMaxTenure;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkAutoDeductFromSalary;
        private System.Windows.Forms.CheckBox chkWaiverAllowed;
        private System.Windows.Forms.CheckBox chkAllowPause;
        private System.Windows.Forms.CheckBox chkApprovalNeeded;
        private System.Windows.Forms.CheckBox chkInterestRequired;
        private System.Windows.Forms.CheckBox chkRecoveryRequired;
        private System.Windows.Forms.CheckBox chkIncludeAsDeductionInSalary;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAdvanceAmountFixed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbAdvanceAmountBased;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAdvancePercentage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbAdvanceBasedOn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkAutoDeductFromNextSaslary;
        private System.Windows.Forms.ComboBox cmbIsActive;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtAdvanceTitle;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblAdvanceTypeID;
        public System.Windows.Forms.Label lblActionMode;
        private System.Windows.Forms.TextBox txtAdvanceCode;
        private System.Windows.Forms.Label label1;
    }
}