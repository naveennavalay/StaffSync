namespace StaffSync
{
    partial class frmSchedulerJobDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSchedulerJobDetails));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblJobSettingsID = new System.Windows.Forms.Label();
            this.lblJobID = new System.Windows.Forms.Label();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.txtJobDescription = new Krypton.Toolkit.KryptonTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtJobName = new Krypton.Toolkit.KryptonTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtJobCode = new Krypton.Toolkit.KryptonTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.grpEnableDisable = new System.Windows.Forms.GroupBox();
            this.kryptonPanel1 = new Krypton.Toolkit.KryptonPanel();
            this.lblAdvanceNote = new System.Windows.Forms.Label();
            this.lblRuntime = new System.Windows.Forms.Label();
            this.lblNextRun = new System.Windows.Forms.Label();
            this.lblLastRun = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.optSunday = new Krypton.Toolkit.KryptonRadioButton();
            this.optSaturday = new Krypton.Toolkit.KryptonRadioButton();
            this.optFriday = new Krypton.Toolkit.KryptonRadioButton();
            this.optThursday = new Krypton.Toolkit.KryptonRadioButton();
            this.optWednesday = new Krypton.Toolkit.KryptonRadioButton();
            this.optTuesday = new Krypton.Toolkit.KryptonRadioButton();
            this.optMonday = new Krypton.Toolkit.KryptonRadioButton();
            this.chkRepeat = new System.Windows.Forms.CheckBox();
            this.cmbIntervalUnit = new Krypton.Toolkit.KryptonComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtInterval = new Krypton.Toolkit.KryptonTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRunTime = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEndDate = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStartDate = new System.Windows.Forms.MaskedTextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.cmbScheduleType = new Krypton.Toolkit.KryptonComboBox();
            this.label28 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCloseMe = new Krypton.Toolkit.KryptonButton();
            this.btnSaveDetails = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.errValidator = new System.Windows.Forms.ErrorProvider(this.components);
            this.empMasInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.staffsyncDBDTSet = new StaffSync.StaffsyncDBDTSet();
            this.empMasInfoTableAdapter = new StaffSync.StaffsyncDBDTSetTableAdapters.EmpMasInfoTableAdapter();
            this.staffsyncDBDataSet1 = new StaffSync.StaffsyncDBDataSet1();
            this.qryAllEmpLeavePendingStatementBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qryAllEmpLeavePendingStatementTableAdapter = new StaffSync.StaffsyncDBDataSet1TableAdapters.qryAllEmpLeavePendingStatementTableAdapter();
            this.chkSystemJob = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.grpEnableDisable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbIntervalUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbScheduleType)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(805, 568);
            this.splitContainer1.SplitterDistance = 506;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(805, 506);
            this.panel1.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupBox5.Controls.Add(this.lblJobSettingsID);
            this.groupBox5.Controls.Add(this.lblJobID);
            this.groupBox5.Controls.Add(this.chkEnabled);
            this.groupBox5.Controls.Add(this.txtJobDescription);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.txtJobName);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.txtJobCode);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.grpEnableDisable);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(13, 11);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(790, 476);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            // 
            // lblJobSettingsID
            // 
            this.lblJobSettingsID.AutoSize = true;
            this.lblJobSettingsID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblJobSettingsID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJobSettingsID.Location = new System.Drawing.Point(493, 28);
            this.lblJobSettingsID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblJobSettingsID.Name = "lblJobSettingsID";
            this.lblJobSettingsID.Size = new System.Drawing.Size(11, 15);
            this.lblJobSettingsID.TabIndex = 31;
            this.lblJobSettingsID.Text = " ";
            this.lblJobSettingsID.Visible = false;
            // 
            // lblJobID
            // 
            this.lblJobID.AutoSize = true;
            this.lblJobID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblJobID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJobID.Location = new System.Drawing.Point(441, 28);
            this.lblJobID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblJobID.Name = "lblJobID";
            this.lblJobID.Size = new System.Drawing.Size(11, 15);
            this.lblJobID.TabIndex = 30;
            this.lblJobID.Text = " ";
            this.lblJobID.Visible = false;
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Checked = true;
            this.chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnabled.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkEnabled.Location = new System.Drawing.Point(43, 172);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(71, 19);
            this.chkEnabled.TabIndex = 27;
            this.chkEnabled.Tag = "Enable";
            this.chkEnabled.Text = "Enable";
            this.chkEnabled.UseVisualStyleBackColor = true;
            this.chkEnabled.CheckedChanged += new System.EventHandler(this.chkEnabled_CheckedChanged);
            // 
            // txtJobDescription
            // 
            this.txtJobDescription.Location = new System.Drawing.Point(127, 82);
            this.txtJobDescription.Multiline = true;
            this.txtJobDescription.Name = "txtJobDescription";
            this.txtJobDescription.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtJobDescription.Size = new System.Drawing.Size(414, 67);
            this.txtJobDescription.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJobDescription.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtJobDescription.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJobDescription.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJobDescription.TabIndex = 26;
            this.txtJobDescription.Tag = "Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 86);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 15);
            this.label2.TabIndex = 25;
            this.label2.Text = "Job Description";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtJobName
            // 
            this.txtJobName.Location = new System.Drawing.Point(127, 55);
            this.txtJobName.Multiline = true;
            this.txtJobName.Name = "txtJobName";
            this.txtJobName.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtJobName.Size = new System.Drawing.Size(414, 21);
            this.txtJobName.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJobName.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtJobName.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJobName.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJobName.TabIndex = 24;
            this.txtJobName.Tag = "Name";
            this.txtJobName.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(53, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 23;
            this.label1.Text = "Job Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtJobCode
            // 
            this.txtJobCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtJobCode.Location = new System.Drawing.Point(127, 25);
            this.txtJobCode.Name = "txtJobCode";
            this.txtJobCode.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtJobCode.Size = new System.Drawing.Size(213, 21);
            this.txtJobCode.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJobCode.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtJobCode.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJobCode.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtJobCode.TabIndex = 22;
            this.txtJobCode.Tag = "Name";
            this.txtJobCode.WordWrap = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(58, 28);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(67, 15);
            this.label19.TabIndex = 21;
            this.label19.Text = "Job Code";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpEnableDisable
            // 
            this.grpEnableDisable.Controls.Add(this.chkSystemJob);
            this.grpEnableDisable.Controls.Add(this.kryptonPanel1);
            this.grpEnableDisable.Controls.Add(this.groupBox1);
            this.grpEnableDisable.Controls.Add(this.chkRepeat);
            this.grpEnableDisable.Controls.Add(this.cmbIntervalUnit);
            this.grpEnableDisable.Controls.Add(this.label6);
            this.grpEnableDisable.Controls.Add(this.txtInterval);
            this.grpEnableDisable.Controls.Add(this.label5);
            this.grpEnableDisable.Controls.Add(this.txtRunTime);
            this.grpEnableDisable.Controls.Add(this.label4);
            this.grpEnableDisable.Controls.Add(this.txtEndDate);
            this.grpEnableDisable.Controls.Add(this.label3);
            this.grpEnableDisable.Controls.Add(this.txtStartDate);
            this.grpEnableDisable.Controls.Add(this.label29);
            this.grpEnableDisable.Controls.Add(this.cmbScheduleType);
            this.grpEnableDisable.Controls.Add(this.label28);
            this.grpEnableDisable.Location = new System.Drawing.Point(18, 162);
            this.grpEnableDisable.Name = "grpEnableDisable";
            this.grpEnableDisable.Size = new System.Drawing.Size(760, 314);
            this.grpEnableDisable.TabIndex = 28;
            this.grpEnableDisable.TabStop = false;
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.Controls.Add(this.lblAdvanceNote);
            this.kryptonPanel1.Controls.Add(this.lblRuntime);
            this.kryptonPanel1.Controls.Add(this.lblNextRun);
            this.kryptonPanel1.Controls.Add(this.lblLastRun);
            this.kryptonPanel1.Controls.Add(this.label9);
            this.kryptonPanel1.Controls.Add(this.label7);
            this.kryptonPanel1.Location = new System.Drawing.Point(7, 223);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.Size = new System.Drawing.Size(747, 84);
            this.kryptonPanel1.StateNormal.Color1 = System.Drawing.SystemColors.Info;
            this.kryptonPanel1.StateNormal.Color2 = System.Drawing.SystemColors.Info;
            this.kryptonPanel1.TabIndex = 55;
            // 
            // lblAdvanceNote
            // 
            this.lblAdvanceNote.BackColor = System.Drawing.Color.Transparent;
            this.lblAdvanceNote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblAdvanceNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblAdvanceNote.Location = new System.Drawing.Point(22, 5);
            this.lblAdvanceNote.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblAdvanceNote.Name = "lblAdvanceNote";
            this.lblAdvanceNote.Size = new System.Drawing.Size(93, 25);
            this.lblAdvanceNote.TabIndex = 105;
            this.lblAdvanceNote.Text = "💡 Last Run";
            this.lblAdvanceNote.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRuntime
            // 
            this.lblRuntime.AutoSize = true;
            this.lblRuntime.BackColor = System.Drawing.Color.Transparent;
            this.lblRuntime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRuntime.Location = new System.Drawing.Point(114, 54);
            this.lblRuntime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRuntime.Name = "lblRuntime";
            this.lblRuntime.Size = new System.Drawing.Size(61, 15);
            this.lblRuntime.TabIndex = 47;
            this.lblRuntime.Text = "Runtime";
            this.lblRuntime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNextRun
            // 
            this.lblNextRun.AutoSize = true;
            this.lblNextRun.BackColor = System.Drawing.Color.Transparent;
            this.lblNextRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNextRun.Location = new System.Drawing.Point(114, 33);
            this.lblNextRun.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNextRun.Name = "lblNextRun";
            this.lblNextRun.Size = new System.Drawing.Size(66, 15);
            this.lblNextRun.TabIndex = 46;
            this.lblNextRun.Text = "Next Run";
            this.lblNextRun.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLastRun
            // 
            this.lblLastRun.AutoSize = true;
            this.lblLastRun.BackColor = System.Drawing.Color.Transparent;
            this.lblLastRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastRun.Location = new System.Drawing.Point(114, 10);
            this.lblLastRun.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLastRun.Name = "lblLastRun";
            this.lblLastRun.Size = new System.Drawing.Size(64, 15);
            this.lblLastRun.TabIndex = 45;
            this.lblLastRun.Text = "Last Run";
            this.lblLastRun.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(46, 54);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(69, 15);
            this.label9.TabIndex = 44;
            this.label9.Text = "Run Time";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(49, 33);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 15);
            this.label7.TabIndex = 42;
            this.label7.Text = "Next Run";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.optSunday);
            this.groupBox1.Controls.Add(this.optSaturday);
            this.groupBox1.Controls.Add(this.optFriday);
            this.groupBox1.Controls.Add(this.optThursday);
            this.groupBox1.Controls.Add(this.optWednesday);
            this.groupBox1.Controls.Add(this.optTuesday);
            this.groupBox1.Controls.Add(this.optMonday);
            this.groupBox1.Location = new System.Drawing.Point(7, 142);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(747, 61);
            this.groupBox1.TabIndex = 54;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Days Of Week and Month";
            // 
            // optSunday
            // 
            this.optSunday.Location = new System.Drawing.Point(14, 30);
            this.optSunday.Name = "optSunday";
            this.optSunday.Size = new System.Drawing.Size(66, 18);
            this.optSunday.StateCommon.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optSunday.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optSunday.TabIndex = 6;
            this.optSunday.Values.Text = "Sunday";
            // 
            // optSaturday
            // 
            this.optSaturday.Location = new System.Drawing.Point(624, 30);
            this.optSaturday.Name = "optSaturday";
            this.optSaturday.Size = new System.Drawing.Size(74, 18);
            this.optSaturday.StateCommon.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optSaturday.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optSaturday.TabIndex = 5;
            this.optSaturday.Values.Text = "Saturday";
            // 
            // optFriday
            // 
            this.optFriday.Location = new System.Drawing.Point(536, 30);
            this.optFriday.Name = "optFriday";
            this.optFriday.Size = new System.Drawing.Size(58, 18);
            this.optFriday.StateCommon.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optFriday.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optFriday.TabIndex = 4;
            this.optFriday.Values.Text = "Friday";
            // 
            // optThursday
            // 
            this.optThursday.Location = new System.Drawing.Point(430, 30);
            this.optThursday.Name = "optThursday";
            this.optThursday.Size = new System.Drawing.Size(76, 18);
            this.optThursday.StateCommon.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optThursday.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optThursday.TabIndex = 3;
            this.optThursday.Values.Text = "Thursday";
            // 
            // optWednesday
            // 
            this.optWednesday.Location = new System.Drawing.Point(310, 30);
            this.optWednesday.Name = "optWednesday";
            this.optWednesday.Size = new System.Drawing.Size(90, 18);
            this.optWednesday.StateCommon.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optWednesday.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optWednesday.TabIndex = 2;
            this.optWednesday.Values.Text = "Wednesday";
            // 
            // optTuesday
            // 
            this.optTuesday.Location = new System.Drawing.Point(208, 30);
            this.optTuesday.Name = "optTuesday";
            this.optTuesday.Size = new System.Drawing.Size(72, 18);
            this.optTuesday.StateCommon.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optTuesday.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optTuesday.TabIndex = 1;
            this.optTuesday.Values.Text = "Tuesday";
            // 
            // optMonday
            // 
            this.optMonday.Checked = true;
            this.optMonday.Location = new System.Drawing.Point(110, 30);
            this.optMonday.Name = "optMonday";
            this.optMonday.Size = new System.Drawing.Size(68, 18);
            this.optMonday.StateCommon.LongText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optMonday.StateCommon.ShortText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optMonday.TabIndex = 0;
            this.optMonday.Values.Text = "Monday";
            // 
            // chkRepeat
            // 
            this.chkRepeat.AutoSize = true;
            this.chkRepeat.Checked = true;
            this.chkRepeat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRepeat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkRepeat.Location = new System.Drawing.Point(536, 36);
            this.chkRepeat.Name = "chkRepeat";
            this.chkRepeat.Size = new System.Drawing.Size(124, 19);
            this.chkRepeat.TabIndex = 53;
            this.chkRepeat.Tag = "Repeat Forever";
            this.chkRepeat.Text = "Repeat Forever";
            this.chkRepeat.UseVisualStyleBackColor = true;
            // 
            // cmbIntervalUnit
            // 
            this.cmbIntervalUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIntervalUnit.DropDownWidth = 440;
            this.cmbIntervalUnit.Location = new System.Drawing.Point(109, 103);
            this.cmbIntervalUnit.Name = "cmbIntervalUnit";
            this.cmbIntervalUnit.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbIntervalUnit.Size = new System.Drawing.Size(106, 22);
            this.cmbIntervalUnit.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbIntervalUnit.TabIndex = 52;
            this.cmbIntervalUnit.Tag = "Interval Unit";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(24, 107);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 15);
            this.label6.TabIndex = 51;
            this.label6.Text = "Interval Unit";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtInterval
            // 
            this.txtInterval.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtInterval.Location = new System.Drawing.Point(317, 104);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtInterval.Size = new System.Drawing.Size(106, 21);
            this.txtInterval.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInterval.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtInterval.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInterval.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInterval.TabIndex = 50;
            this.txtInterval.Tag = "Name";
            this.txtInterval.WordWrap = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(260, 107);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 15);
            this.label5.TabIndex = 49;
            this.label5.Text = "Interval";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRunTime
            // 
            this.txtRunTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(0)))), ((int)(((byte)(254)))));
            this.txtRunTime.Location = new System.Drawing.Point(536, 68);
            this.txtRunTime.Mask = "##:##:##";
            this.txtRunTime.Name = "txtRunTime";
            this.txtRunTime.Size = new System.Drawing.Size(106, 21);
            this.txtRunTime.TabIndex = 47;
            this.txtRunTime.Tag = "Date Of Birth";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(466, 71);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 15);
            this.label4.TabIndex = 48;
            this.label4.Text = "Run Time";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtEndDate
            // 
            this.txtEndDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(0)))), ((int)(((byte)(254)))));
            this.txtEndDate.Location = new System.Drawing.Point(317, 68);
            this.txtEndDate.Mask = "##-##-####";
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.Size = new System.Drawing.Size(106, 21);
            this.txtEndDate.TabIndex = 45;
            this.txtEndDate.Tag = "Date Of Birth";
            this.txtEndDate.ValidatingType = typeof(System.DateTime);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(248, 71);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 15);
            this.label3.TabIndex = 46;
            this.label3.Text = "End Date";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtStartDate
            // 
            this.txtStartDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(0)))), ((int)(((byte)(254)))));
            this.txtStartDate.Location = new System.Drawing.Point(109, 68);
            this.txtStartDate.Mask = "##-##-####";
            this.txtStartDate.Name = "txtStartDate";
            this.txtStartDate.Size = new System.Drawing.Size(106, 21);
            this.txtStartDate.TabIndex = 43;
            this.txtStartDate.Tag = "Date Of Birth";
            this.txtStartDate.ValidatingType = typeof(System.DateTime);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(37, 71);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(71, 15);
            this.label29.TabIndex = 44;
            this.label29.Text = "Start Date";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbScheduleType
            // 
            this.cmbScheduleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScheduleType.DropDownWidth = 440;
            this.cmbScheduleType.Location = new System.Drawing.Point(109, 34);
            this.cmbScheduleType.Name = "cmbScheduleType";
            this.cmbScheduleType.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbScheduleType.Size = new System.Drawing.Size(106, 22);
            this.cmbScheduleType.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbScheduleType.TabIndex = 42;
            this.cmbScheduleType.Tag = "Gender";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(7, 38);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(101, 15);
            this.label28.TabIndex = 41;
            this.label28.Text = "Schedule Type";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel2.Controls.Add(this.btnCloseMe);
            this.panel2.Controls.Add(this.btnSaveDetails);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(805, 57);
            this.panel2.TabIndex = 1;
            // 
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(659, 8);
            this.btnCloseMe.Name = "btnCloseMe";
            this.btnCloseMe.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCloseMe.Size = new System.Drawing.Size(126, 38);
            this.btnCloseMe.TabIndex = 17;
            this.btnCloseMe.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCloseMe.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseMe.Values.Image")));
            this.btnCloseMe.Values.Text = "Close Me";
            this.btnCloseMe.Click += new System.EventHandler(this.btnCloseMe_Click);
            // 
            // btnSaveDetails
            // 
            this.btnSaveDetails.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSaveDetails.Location = new System.Drawing.Point(13, 8);
            this.btnSaveDetails.Name = "btnSaveDetails";
            this.btnSaveDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSaveDetails.Size = new System.Drawing.Size(126, 38);
            this.btnSaveDetails.TabIndex = 16;
            this.btnSaveDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSaveDetails.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveDetails.Values.Image")));
            this.btnSaveDetails.Values.Text = "Save";
            this.btnSaveDetails.Click += new System.EventHandler(this.btnSaveDetails_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(148, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCancel.Size = new System.Drawing.Size(126, 38);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Values.Image")));
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            // chkSystemJob
            // 
            this.chkSystemJob.AutoSize = true;
            this.chkSystemJob.Checked = true;
            this.chkSystemJob.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSystemJob.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkSystemJob.Location = new System.Drawing.Point(317, 36);
            this.chkSystemJob.Name = "chkSystemJob";
            this.chkSystemJob.Size = new System.Drawing.Size(141, 19);
            this.chkSystemJob.TabIndex = 56;
            this.chkSystemJob.Tag = "System Scheduler";
            this.chkSystemJob.Text = "System Scheduler";
            this.chkSystemJob.UseVisualStyleBackColor = true;
            // 
            // frmSchedulerJobDetails
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(805, 568);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSchedulerJobDetails";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scheduler Job Configuration";
            this.Activated += new System.EventHandler(this.frmSchedulerJobDetails_Activated);
            this.Load += new System.EventHandler(this.frmSchedulerJobDetails_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSchedulerJobDetails_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.grpEnableDisable.ResumeLayout(false);
            this.grpEnableDisable.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            this.kryptonPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbIntervalUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbScheduleType)).EndInit();
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
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ErrorProvider errValidator;
        private StaffsyncDBDataSet1 staffsyncDBDataSet1;
        private System.Windows.Forms.BindingSource qryAllEmpLeavePendingStatementBindingSource;
        private StaffsyncDBDataSet1TableAdapters.qryAllEmpLeavePendingStatementTableAdapter qryAllEmpLeavePendingStatementTableAdapter;
        private Krypton.Toolkit.KryptonTextBox txtJobCode;
        private System.Windows.Forms.Label label19;
        private Krypton.Toolkit.KryptonTextBox txtJobName;
        private System.Windows.Forms.Label label1;
        private Krypton.Toolkit.KryptonTextBox txtJobDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.GroupBox grpEnableDisable;
        private Krypton.Toolkit.KryptonComboBox cmbScheduleType;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.MaskedTextBox txtRunTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox txtEndDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox txtStartDate;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.CheckBox chkRepeat;
        private Krypton.Toolkit.KryptonComboBox cmbIntervalUnit;
        private System.Windows.Forms.Label label6;
        private Krypton.Toolkit.KryptonTextBox txtInterval;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private Krypton.Toolkit.KryptonRadioButton optMonday;
        private Krypton.Toolkit.KryptonRadioButton optTuesday;
        private Krypton.Toolkit.KryptonRadioButton optSunday;
        private Krypton.Toolkit.KryptonRadioButton optSaturday;
        private Krypton.Toolkit.KryptonRadioButton optFriday;
        private Krypton.Toolkit.KryptonRadioButton optThursday;
        private Krypton.Toolkit.KryptonRadioButton optWednesday;
        private Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblNextRun;
        private System.Windows.Forms.Label lblLastRun;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblRuntime;
        private System.Windows.Forms.Label lblAdvanceNote;
        private Krypton.Toolkit.KryptonButton btnCloseMe;
        private Krypton.Toolkit.KryptonButton btnSaveDetails;
        private Krypton.Toolkit.KryptonButton btnCancel;
        public System.Windows.Forms.Label lblJobSettingsID;
        public System.Windows.Forms.Label lblJobID;
        private System.Windows.Forms.CheckBox chkSystemJob;
    }
}