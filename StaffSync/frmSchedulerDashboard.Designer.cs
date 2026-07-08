namespace StaffSync
{
    partial class frmSchedulerDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSchedulerDashboard));
            this.empMasInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.staffsyncDBDTSet = new StaffSync.StaffsyncDBDTSet();
            this.empMasInfoTableAdapter = new StaffSync.StaffsyncDBDTSetTableAdapters.EmpMasInfoTableAdapter();
            this.errValidator = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnRunAll = new Krypton.Toolkit.KryptonButton();
            this.btnRefresh = new Krypton.Toolkit.KryptonButton();
            this.btnStopAll = new Krypton.Toolkit.KryptonButton();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lblStoppedJobsCount = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblCurrentlyStoppedJobsCount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblCurrentlyPausedJobsCount = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblCurrentlyRunningJobsCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblAllConfiguredJobsCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblAdvanceNote = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dtgAllScheduledJobsList = new Krypton.Toolkit.KryptonDataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabConfigJobs = new Krypton.Navigator.KryptonNavigator();
            this.tabExecutionHistory = new Krypton.Navigator.KryptonPage();
            this.dtgScheduledJobsExecutionHistory = new Krypton.Toolkit.KryptonDataGridView();
            this.tabJobLogs = new Krypton.Navigator.KryptonPage();
            this.dtgScheduledJobsLogs = new Krypton.Toolkit.KryptonDataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.tmrUIRefresher = new System.Windows.Forms.Timer(this.components);
            this.panel7 = new System.Windows.Forms.Panel();
            this.lblCurrentlySuccessfulJobsCount = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tmrTimeLeftCounter = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAllScheduledJobsList)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabConfigJobs)).BeginInit();
            this.tabConfigJobs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabExecutionHistory)).BeginInit();
            this.tabExecutionHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgScheduledJobsExecutionHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabJobLogs)).BeginInit();
            this.tabJobLogs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgScheduledJobsLogs)).BeginInit();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
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
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.btnRunAll);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnStopAll);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1724, 130);
            this.panel1.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(1307, 94);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 15);
            this.label14.TabIndex = 23;
            this.label14.Text = "Refresh";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(1307, 57);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(90, 15);
            this.label13.TabIndex = 22;
            this.label13.Text = "Stop All Jobs";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1307, 20);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 15);
            this.label10.TabIndex = 21;
            this.label10.Text = "Start All Jobs";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnRunAll
            // 
            this.btnRunAll.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnRunAll.Location = new System.Drawing.Point(1264, 13);
            this.btnRunAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnRunAll.Name = "btnRunAll";
            this.btnRunAll.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnRunAll.Size = new System.Drawing.Size(35, 29);
            this.btnRunAll.TabIndex = 20;
            this.btnRunAll.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnRunAll.Values.Image = global::StaffSync.Properties.Resources.runningjobs;
            this.btnRunAll.Values.Text = "Stop All";
            this.btnRunAll.Click += new System.EventHandler(this.btnRunAll_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnRefresh.Location = new System.Drawing.Point(1264, 87);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnRefresh.Size = new System.Drawing.Size(35, 29);
            this.btnRefresh.TabIndex = 19;
            this.btnRefresh.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnRefresh.Values.Image = global::StaffSync.Properties.Resources.refresh;
            this.btnRefresh.Values.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnStopAll
            // 
            this.btnStopAll.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnStopAll.Location = new System.Drawing.Point(1264, 50);
            this.btnStopAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnStopAll.Name = "btnStopAll";
            this.btnStopAll.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnStopAll.Size = new System.Drawing.Size(35, 29);
            this.btnStopAll.TabIndex = 18;
            this.btnStopAll.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnStopAll.Values.Image = global::StaffSync.Properties.Resources.stop;
            this.btnStopAll.Values.Text = "Stop All";
            this.btnStopAll.Click += new System.EventHandler(this.btnStopAll_Click);
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.lblStoppedJobsCount);
            this.panel6.Controls.Add(this.label11);
            this.panel6.Controls.Add(this.label12);
            this.panel6.Controls.Add(this.pictureBox5);
            this.panel6.Location = new System.Drawing.Point(842, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(204, 129);
            this.panel6.TabIndex = 4;
            // 
            // lblStoppedJobsCount
            // 
            this.lblStoppedJobsCount.AutoSize = true;
            this.lblStoppedJobsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStoppedJobsCount.Location = new System.Drawing.Point(57, 35);
            this.lblStoppedJobsCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStoppedJobsCount.Name = "lblStoppedJobsCount";
            this.lblStoppedJobsCount.Size = new System.Drawing.Size(50, 54);
            this.lblStoppedJobsCount.TabIndex = 12;
            this.lblStoppedJobsCount.Text = "0";
            this.lblStoppedJobsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(64, 98);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 15);
            this.label11.TabIndex = 11;
            this.label11.Text = "Stopped Jobs";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(64, 12);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(94, 15);
            this.label12.TabIndex = 10;
            this.label12.Text = "Stopped Jobs";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.lblCurrentlyStoppedJobsCount);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.pictureBox4);
            this.panel5.Location = new System.Drawing.Point(632, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(204, 129);
            this.panel5.TabIndex = 3;
            // 
            // lblCurrentlyStoppedJobsCount
            // 
            this.lblCurrentlyStoppedJobsCount.AutoSize = true;
            this.lblCurrentlyStoppedJobsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentlyStoppedJobsCount.Location = new System.Drawing.Point(57, 35);
            this.lblCurrentlyStoppedJobsCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentlyStoppedJobsCount.Name = "lblCurrentlyStoppedJobsCount";
            this.lblCurrentlyStoppedJobsCount.Size = new System.Drawing.Size(50, 54);
            this.lblCurrentlyStoppedJobsCount.TabIndex = 12;
            this.lblCurrentlyStoppedJobsCount.Text = "0";
            this.lblCurrentlyStoppedJobsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(64, 98);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(119, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "Currently stopped";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(64, 12);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 15);
            this.label8.TabIndex = 10;
            this.label8.Text = "Disabled Jobs";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lblCurrentlyPausedJobsCount);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.pictureBox3);
            this.panel4.Location = new System.Drawing.Point(1053, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(204, 129);
            this.panel4.TabIndex = 2;
            this.panel4.Visible = false;
            // 
            // lblCurrentlyPausedJobsCount
            // 
            this.lblCurrentlyPausedJobsCount.AutoSize = true;
            this.lblCurrentlyPausedJobsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentlyPausedJobsCount.Location = new System.Drawing.Point(57, 35);
            this.lblCurrentlyPausedJobsCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentlyPausedJobsCount.Name = "lblCurrentlyPausedJobsCount";
            this.lblCurrentlyPausedJobsCount.Size = new System.Drawing.Size(50, 54);
            this.lblCurrentlyPausedJobsCount.TabIndex = 12;
            this.lblCurrentlyPausedJobsCount.Text = "0";
            this.lblCurrentlyPausedJobsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(64, 98);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "Currently paused";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(64, 12);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "Paused Jobs";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lblCurrentlyRunningJobsCount);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.pictureBox2);
            this.panel3.Location = new System.Drawing.Point(423, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(204, 129);
            this.panel3.TabIndex = 1;
            // 
            // lblCurrentlyRunningJobsCount
            // 
            this.lblCurrentlyRunningJobsCount.AutoSize = true;
            this.lblCurrentlyRunningJobsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentlyRunningJobsCount.Location = new System.Drawing.Point(57, 35);
            this.lblCurrentlyRunningJobsCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentlyRunningJobsCount.Name = "lblCurrentlyRunningJobsCount";
            this.lblCurrentlyRunningJobsCount.Size = new System.Drawing.Size(50, 54);
            this.lblCurrentlyRunningJobsCount.TabIndex = 12;
            this.lblCurrentlyRunningJobsCount.Text = "0";
            this.lblCurrentlyRunningJobsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(64, 98);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "Currently running";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(64, 12);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Running Jobs";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblAllConfiguredJobsCount);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label19);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(3, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(204, 129);
            this.panel2.TabIndex = 0;
            // 
            // lblAllConfiguredJobsCount
            // 
            this.lblAllConfiguredJobsCount.AutoSize = true;
            this.lblAllConfiguredJobsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAllConfiguredJobsCount.Location = new System.Drawing.Point(57, 35);
            this.lblAllConfiguredJobsCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAllConfiguredJobsCount.Name = "lblAllConfiguredJobsCount";
            this.lblAllConfiguredJobsCount.Size = new System.Drawing.Size(50, 54);
            this.lblAllConfiguredJobsCount.TabIndex = 12;
            this.lblAllConfiguredJobsCount.Text = "0";
            this.lblAllConfiguredJobsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(64, 98);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "All Configured Jobs";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(64, 12);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(73, 15);
            this.label19.TabIndex = 10;
            this.label19.Text = "Total Jobs";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupBox4.Controls.Add(this.lblAdvanceNote);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.dtgAllScheduledJobsList);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(12, 149);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(1723, 247);
            this.groupBox4.TabIndex = 49;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Configured Jobs";
            // 
            // lblAdvanceNote
            // 
            this.lblAdvanceNote.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblAdvanceNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblAdvanceNote.Location = new System.Drawing.Point(-6, 217);
            this.lblAdvanceNote.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblAdvanceNote.Name = "lblAdvanceNote";
            this.lblAdvanceNote.Size = new System.Drawing.Size(506, 25);
            this.lblAdvanceNote.TabIndex = 104;
            this.lblAdvanceNote.Text = "💡 Tip: Double click to update Scheduler Job Settings";
            this.lblAdvanceNote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 289);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(217, 15);
            this.label2.TabIndex = 50;
            this.label2.Text = "Double click update Job Settings";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtgAllScheduledJobsList
            // 
            this.dtgAllScheduledJobsList.AllowUserToResizeRows = false;
            this.dtgAllScheduledJobsList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgAllScheduledJobsList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgAllScheduledJobsList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dtgAllScheduledJobsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgAllScheduledJobsList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dtgAllScheduledJobsList.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dtgAllScheduledJobsList.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.ButtonLowProfile;
            this.dtgAllScheduledJobsList.Location = new System.Drawing.Point(0, 21);
            this.dtgAllScheduledJobsList.MultiSelect = false;
            this.dtgAllScheduledJobsList.Name = "dtgAllScheduledJobsList";
            this.dtgAllScheduledJobsList.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtgAllScheduledJobsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgAllScheduledJobsList.Size = new System.Drawing.Size(1723, 195);
            this.dtgAllScheduledJobsList.TabIndex = 49;
            this.dtgAllScheduledJobsList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgAllScheduledJobsList_CellDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupBox1.Controls.Add(this.tabConfigJobs);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 401);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1723, 291);
            this.groupBox1.TabIndex = 50;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configured Jobs";
            // 
            // tabConfigJobs
            // 
            this.tabConfigJobs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabConfigJobs.Bar.BarMapExtraText = Krypton.Navigator.MapKryptonPageText.None;
            this.tabConfigJobs.Bar.BarMapImage = Krypton.Navigator.MapKryptonPageImage.Small;
            this.tabConfigJobs.Bar.BarMapText = Krypton.Navigator.MapKryptonPageText.TextTitle;
            this.tabConfigJobs.Bar.BarMultiline = Krypton.Navigator.BarMultiline.Singleline;
            this.tabConfigJobs.Bar.BarOrientation = Krypton.Toolkit.VisualOrientation.Top;
            this.tabConfigJobs.Bar.CheckButtonStyle = Krypton.Toolkit.ButtonStyle.Standalone;
            this.tabConfigJobs.Bar.ItemAlignment = Krypton.Toolkit.RelativePositionAlign.Near;
            this.tabConfigJobs.Bar.ItemMaximumSize = new System.Drawing.Size(200, 200);
            this.tabConfigJobs.Bar.ItemMinimumSize = new System.Drawing.Size(20, 20);
            this.tabConfigJobs.Bar.ItemOrientation = Krypton.Toolkit.ButtonOrientation.Auto;
            this.tabConfigJobs.Bar.ItemSizing = Krypton.Navigator.BarItemSizing.SameWidthAndHeight;
            this.tabConfigJobs.Bar.TabBorderStyle = Krypton.Toolkit.TabBorderStyle.RoundedOutsizeMedium;
            this.tabConfigJobs.Bar.TabStyle = Krypton.Toolkit.TabStyle.HighProfile;
            this.tabConfigJobs.Button.ButtonDisplayLogic = Krypton.Navigator.ButtonDisplayLogic.None;
            this.tabConfigJobs.Button.CloseButtonAction = Krypton.Navigator.CloseButtonAction.None;
            this.tabConfigJobs.Button.CloseButtonDisplay = Krypton.Navigator.ButtonDisplay.Hide;
            this.tabConfigJobs.Button.ContextButtonAction = Krypton.Navigator.ContextButtonAction.SelectPage;
            this.tabConfigJobs.Button.ContextButtonDisplay = Krypton.Navigator.ButtonDisplay.Logic;
            this.tabConfigJobs.Button.ContextMenuMapImage = Krypton.Navigator.MapKryptonPageImage.Small;
            this.tabConfigJobs.Button.ContextMenuMapText = Krypton.Navigator.MapKryptonPageText.TextTitle;
            this.tabConfigJobs.Button.NextButtonAction = Krypton.Navigator.DirectionButtonAction.ModeAppropriateAction;
            this.tabConfigJobs.Button.NextButtonDisplay = Krypton.Navigator.ButtonDisplay.Logic;
            this.tabConfigJobs.Button.PreviousButtonAction = Krypton.Navigator.DirectionButtonAction.ModeAppropriateAction;
            this.tabConfigJobs.Button.PreviousButtonDisplay = Krypton.Navigator.ButtonDisplay.Logic;
            this.tabConfigJobs.ControlKryptonFormFeatures = false;
            this.tabConfigJobs.Group.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.TabHighProfile;
            this.tabConfigJobs.Group.GroupBorderStyle = Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.tabConfigJobs.Location = new System.Drawing.Point(12, 22);
            this.tabConfigJobs.NavigatorMode = Krypton.Navigator.NavigatorMode.BarTabGroup;
            this.tabConfigJobs.Owner = null;
            this.tabConfigJobs.PageBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonStandalone;
            this.tabConfigJobs.Pages.AddRange(new Krypton.Navigator.KryptonPage[] {
            this.tabExecutionHistory,
            this.tabJobLogs});
            this.tabConfigJobs.PaletteMode = Krypton.Toolkit.PaletteMode.Office2010BlueLightMode;
            this.tabConfigJobs.SelectedIndex = 0;
            this.tabConfigJobs.Size = new System.Drawing.Size(1690, 262);
            this.tabConfigJobs.TabIndex = 51;
            this.tabConfigJobs.Text = "kryptonNavigator1";
            // 
            // tabExecutionHistory
            // 
            this.tabExecutionHistory.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.tabExecutionHistory.Controls.Add(this.dtgScheduledJobsExecutionHistory);
            this.tabExecutionHistory.Flags = 65534;
            this.tabExecutionHistory.LastVisibleSet = true;
            this.tabExecutionHistory.MinimumSize = new System.Drawing.Size(150, 50);
            this.tabExecutionHistory.Name = "tabExecutionHistory";
            this.tabExecutionHistory.Size = new System.Drawing.Size(1688, 235);
            this.tabExecutionHistory.Text = "Execution History";
            this.tabExecutionHistory.ToolTipTitle = "Execution History";
            this.tabExecutionHistory.UniqueName = "17f3fa05ebe644ae9c3621aed298314b";
            // 
            // dtgScheduledJobsExecutionHistory
            // 
            this.dtgScheduledJobsExecutionHistory.AllowUserToResizeRows = false;
            this.dtgScheduledJobsExecutionHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgScheduledJobsExecutionHistory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgScheduledJobsExecutionHistory.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dtgScheduledJobsExecutionHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgScheduledJobsExecutionHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dtgScheduledJobsExecutionHistory.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dtgScheduledJobsExecutionHistory.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.ButtonLowProfile;
            this.dtgScheduledJobsExecutionHistory.Location = new System.Drawing.Point(13, 19);
            this.dtgScheduledJobsExecutionHistory.MultiSelect = false;
            this.dtgScheduledJobsExecutionHistory.Name = "dtgScheduledJobsExecutionHistory";
            this.dtgScheduledJobsExecutionHistory.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtgScheduledJobsExecutionHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgScheduledJobsExecutionHistory.Size = new System.Drawing.Size(1659, 200);
            this.dtgScheduledJobsExecutionHistory.TabIndex = 50;
            // 
            // tabJobLogs
            // 
            this.tabJobLogs.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.tabJobLogs.Controls.Add(this.dtgScheduledJobsLogs);
            this.tabJobLogs.Enabled = false;
            this.tabJobLogs.Flags = 65534;
            this.tabJobLogs.LastVisibleSet = true;
            this.tabJobLogs.MinimumSize = new System.Drawing.Size(150, 50);
            this.tabJobLogs.Name = "tabJobLogs";
            this.tabJobLogs.Size = new System.Drawing.Size(1688, 235);
            this.tabJobLogs.Text = "Job Logs";
            this.tabJobLogs.ToolTipTitle = "Job Logs";
            this.tabJobLogs.UniqueName = "3d8fc9f208e54cc3b8cd4155a7044fcc";
            this.tabJobLogs.Visible = false;
            // 
            // dtgScheduledJobsLogs
            // 
            this.dtgScheduledJobsLogs.AllowUserToResizeRows = false;
            this.dtgScheduledJobsLogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgScheduledJobsLogs.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgScheduledJobsLogs.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dtgScheduledJobsLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgScheduledJobsLogs.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dtgScheduledJobsLogs.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dtgScheduledJobsLogs.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.ButtonLowProfile;
            this.dtgScheduledJobsLogs.Location = new System.Drawing.Point(14, 17);
            this.dtgScheduledJobsLogs.MultiSelect = false;
            this.dtgScheduledJobsLogs.Name = "dtgScheduledJobsLogs";
            this.dtgScheduledJobsLogs.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtgScheduledJobsLogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgScheduledJobsLogs.Size = new System.Drawing.Size(1659, 200);
            this.dtgScheduledJobsLogs.TabIndex = 51;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(9, 289);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(217, 15);
            this.label9.TabIndex = 50;
            this.label9.Text = "Double click update Job Settings";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tmrUIRefresher
            // 
            this.tmrUIRefresher.Enabled = true;
            this.tmrUIRefresher.Interval = 5000;
            this.tmrUIRefresher.Tick += new System.EventHandler(this.tmrUIRefresher_Tick);
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.lblCurrentlySuccessfulJobsCount);
            this.panel7.Controls.Add(this.label16);
            this.panel7.Controls.Add(this.label17);
            this.panel7.Controls.Add(this.pictureBox6);
            this.panel7.Location = new System.Drawing.Point(213, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(204, 129);
            this.panel7.TabIndex = 24;
            // 
            // lblCurrentlySuccessfulJobsCount
            // 
            this.lblCurrentlySuccessfulJobsCount.AutoSize = true;
            this.lblCurrentlySuccessfulJobsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentlySuccessfulJobsCount.Location = new System.Drawing.Point(57, 35);
            this.lblCurrentlySuccessfulJobsCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCurrentlySuccessfulJobsCount.Name = "lblCurrentlySuccessfulJobsCount";
            this.lblCurrentlySuccessfulJobsCount.Size = new System.Drawing.Size(50, 54);
            this.lblCurrentlySuccessfulJobsCount.TabIndex = 12;
            this.lblCurrentlySuccessfulJobsCount.Text = "0";
            this.lblCurrentlySuccessfulJobsCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(64, 98);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(137, 15);
            this.label16.TabIndex = 11;
            this.label16.Text = "Currently Successful";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(64, 12);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(110, 15);
            this.label17.TabIndex = 10;
            this.label17.Text = "Successful Jobs";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = global::StaffSync.Properties.Resources.success;
            this.pictureBox6.Location = new System.Drawing.Point(14, 41);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(40, 40);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 2;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(14, 41);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(40, 40);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 2;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::StaffSync.Properties.Resources.disabled;
            this.pictureBox4.Location = new System.Drawing.Point(14, 41);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(40, 40);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 2;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(14, 41);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(40, 40);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 2;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(14, 41);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(40, 40);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(14, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // tmrTimeLeftCounter
            // 
            this.tmrTimeLeftCounter.Enabled = true;
            this.tmrTimeLeftCounter.Interval = 1000;
            this.tmrTimeLeftCounter.Tick += new System.EventHandler(this.tmrTimeLeftCounter_Tick);
            // 
            // frmSchedulerDashboard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1748, 730);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSchedulerDashboard";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Skills List";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSchedulerDashboard_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSchedulerDashboard_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAllScheduledJobsList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabConfigJobs)).EndInit();
            this.tabConfigJobs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabExecutionHistory)).EndInit();
            this.tabExecutionHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgScheduledJobsExecutionHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabJobLogs)).EndInit();
            this.tabJobLogs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgScheduledJobsLogs)).EndInit();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private StaffsyncDBDTSet staffsyncDBDTSet;
        private System.Windows.Forms.BindingSource empMasInfoBindingSource;
        private StaffsyncDBDTSetTableAdapters.EmpMasInfoTableAdapter empMasInfoTableAdapter;
        private System.Windows.Forms.ErrorProvider errValidator;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblAllConfiguredJobsCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblCurrentlyRunningJobsCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblCurrentlyPausedJobsCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lblCurrentlyStoppedJobsCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.GroupBox groupBox4;
        private Krypton.Toolkit.KryptonDataGridView dtgAllScheduledJobsList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private Krypton.Navigator.KryptonNavigator tabConfigJobs;
        private Krypton.Navigator.KryptonPage tabExecutionHistory;
        private Krypton.Navigator.KryptonPage tabJobLogs;
        private System.Windows.Forms.Label lblAdvanceNote;
        private Krypton.Toolkit.KryptonDataGridView dtgScheduledJobsExecutionHistory;
        private Krypton.Toolkit.KryptonDataGridView dtgScheduledJobsLogs;
        private System.Windows.Forms.Timer tmrUIRefresher;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lblStoppedJobsCount;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox pictureBox5;
        private Krypton.Toolkit.KryptonButton btnRefresh;
        private Krypton.Toolkit.KryptonButton btnStopAll;
        private Krypton.Toolkit.KryptonButton btnRunAll;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label lblCurrentlySuccessfulJobsCount;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Timer tmrTimeLeftCounter;
    }
}