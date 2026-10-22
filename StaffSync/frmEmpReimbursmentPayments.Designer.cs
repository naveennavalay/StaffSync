namespace StaffSync
{
    partial class frmEmpReimbursmentPayments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmpReimbursmentPayments));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dtgReimbursmentPaymentHistory = new Krypton.Toolkit.KryptonDataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.kryptonLinkLabel1 = new Krypton.Toolkit.KryptonLinkLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnReimbursmentSearch = new Krypton.Toolkit.KryptonButton();
            this.txtRequestDate = new Krypton.Toolkit.KryptonTextBox();
            this.txtReimbStatus = new Krypton.Toolkit.KryptonTextBox();
            this.txtReimbType = new Krypton.Toolkit.KryptonTextBox();
            this.txtRequestCode = new Krypton.Toolkit.KryptonTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lblTaskID = new System.Windows.Forms.Label();
            this.lblReimbRequestID = new System.Windows.Forms.Label();
            this.lblClientID = new System.Windows.Forms.Label();
            this.lblReimbTypeID = new System.Windows.Forms.Label();
            this.lblRequestFromMailID = new System.Windows.Forms.Label();
            this.lblEmpID = new System.Windows.Forms.Label();
            this.lblPersonalInfoID = new System.Windows.Forms.Label();
            this.txtDepartment = new Krypton.Toolkit.KryptonTextBox();
            this.txtDesignation = new Krypton.Toolkit.KryptonTextBox();
            this.txtEmpName = new Krypton.Toolkit.KryptonTextBox();
            this.txtEmpCode = new Krypton.Toolkit.KryptonTextBox();
            this.lblActionMode = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.picEmpPhoto = new System.Windows.Forms.PictureBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtComments = new Krypton.Toolkit.KryptonTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtReimbursementAmount = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.dtReimbRequestDate = new System.Windows.Forms.MaskedTextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCloseMe = new Krypton.Toolkit.KryptonButton();
            this.btnRemoveDetails = new Krypton.Toolkit.KryptonButton();
            this.btnSaveDetails = new Krypton.Toolkit.KryptonButton();
            this.btnModifyDetails = new Krypton.Toolkit.KryptonButton();
            this.btnGenerateDetails = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.errValidator = new System.Windows.Forms.ErrorProvider(this.components);
            this.staffsyncDBDataSet1 = new StaffSync.StaffsyncDBDataSet1();
            this.qryAllEmpLeavePendingStatementBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qryAllEmpLeavePendingStatementTableAdapter = new StaffSync.StaffsyncDBDataSet1TableAdapters.qryAllEmpLeavePendingStatementTableAdapter();
            this.empMasInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.staffsyncDBDTSet = new StaffSync.StaffsyncDBDTSet();
            this.empMasInfoTableAdapter = new StaffSync.StaffsyncDBDTSetTableAdapters.EmpMasInfoTableAdapter();
            this.txtRequestDescription = new Krypton.Toolkit.KryptonTextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgReimbursmentPaymentHistory)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEmpPhoto)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(1057, 802);
            this.splitContainer1.SplitterDistance = 721;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox8);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1057, 721);
            this.panel1.TabIndex = 1;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupBox3.Controls.Add(this.dtgReimbursmentPaymentHistory);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(11, 509);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1035, 207);
            this.groupBox3.TabIndex = 67;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Payment Details";
            // 
            // dtgReimbursmentPaymentHistory
            // 
            this.dtgReimbursmentPaymentHistory.AllowUserToAddRows = false;
            this.dtgReimbursmentPaymentHistory.AllowUserToDeleteRows = false;
            this.dtgReimbursmentPaymentHistory.AllowUserToResizeRows = false;
            this.dtgReimbursmentPaymentHistory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgReimbursmentPaymentHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgReimbursmentPaymentHistory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dtgReimbursmentPaymentHistory.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dtgReimbursmentPaymentHistory.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.ContextMenuItemImage;
            this.dtgReimbursmentPaymentHistory.Location = new System.Drawing.Point(122, 20);
            this.dtgReimbursmentPaymentHistory.Name = "dtgReimbursmentPaymentHistory";
            this.dtgReimbursmentPaymentHistory.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtgReimbursmentPaymentHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgReimbursmentPaymentHistory.Size = new System.Drawing.Size(790, 173);
            this.dtgReimbursmentPaymentHistory.StateCommon.Background.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.dtgReimbursmentPaymentHistory.StateCommon.Background.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.dtgReimbursmentPaymentHistory.StateCommon.BackStyle = Krypton.Toolkit.PaletteBackStyle.ContextMenuItemImage;
            this.dtgReimbursmentPaymentHistory.StateDisabled.Background.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.dtgReimbursmentPaymentHistory.StateDisabled.Background.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.dtgReimbursmentPaymentHistory.StateNormal.Background.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.dtgReimbursmentPaymentHistory.StateNormal.Background.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.dtgReimbursmentPaymentHistory.TabIndex = 67;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtRequestDescription);
            this.groupBox2.Controls.Add(this.kryptonLinkLabel1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.btnReimbursmentSearch);
            this.groupBox2.Controls.Add(this.txtRequestDate);
            this.groupBox2.Controls.Add(this.txtReimbStatus);
            this.groupBox2.Controls.Add(this.txtReimbType);
            this.groupBox2.Controls.Add(this.txtRequestCode);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(13, 29);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1032, 181);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Reimbursment Request Information";
            // 
            // kryptonLinkLabel1
            // 
            this.kryptonLinkLabel1.Location = new System.Drawing.Point(360, 28);
            this.kryptonLinkLabel1.Name = "kryptonLinkLabel1";
            this.kryptonLinkLabel1.Size = new System.Drawing.Size(93, 20);
            this.kryptonLinkLabel1.TabIndex = 80;
            this.kryptonLinkLabel1.Values.Text = "View Audit Log";
            this.kryptonLinkLabel1.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(567, 41);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(171, 15);
            this.label5.TabIndex = 79;
            this.label5.Text = "View Advance Information";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(547, 20);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 15);
            this.label6.TabIndex = 44;
            this.label6.Text = "Department";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(652, 43);
            this.label8.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 15);
            this.label8.TabIndex = 33;
            this.label8.Text = " ";
            this.label8.Visible = false;
            // 
            // btnReimbursmentSearch
            // 
            this.btnReimbursmentSearch.Location = new System.Drawing.Point(324, 25);
            this.btnReimbursmentSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnReimbursmentSearch.Name = "btnReimbursmentSearch";
            this.btnReimbursmentSearch.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnReimbursmentSearch.Size = new System.Drawing.Size(29, 28);
            this.btnReimbursmentSearch.TabIndex = 0;
            this.btnReimbursmentSearch.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnReimbursmentSearch.Values.Image = global::StaffSync.Properties.Resources.search;
            this.btnReimbursmentSearch.Values.Text = "";
            this.btnReimbursmentSearch.Click += new System.EventHandler(this.btnReimbursmentSearch_Click);
            // 
            // txtRequestDate
            // 
            this.txtRequestDate.Location = new System.Drawing.Point(150, 96);
            this.txtRequestDate.Margin = new System.Windows.Forms.Padding(4);
            this.txtRequestDate.Multiline = true;
            this.txtRequestDate.Name = "txtRequestDate";
            this.txtRequestDate.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtRequestDate.Size = new System.Drawing.Size(167, 28);
            this.txtRequestDate.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRequestDate.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtRequestDate.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRequestDate.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRequestDate.TabIndex = 30;
            this.txtRequestDate.Tag = "Department";
            // 
            // txtReimbStatus
            // 
            this.txtReimbStatus.Location = new System.Drawing.Point(150, 131);
            this.txtReimbStatus.Margin = new System.Windows.Forms.Padding(4);
            this.txtReimbStatus.Multiline = true;
            this.txtReimbStatus.Name = "txtReimbStatus";
            this.txtReimbStatus.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtReimbStatus.Size = new System.Drawing.Size(167, 28);
            this.txtReimbStatus.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReimbStatus.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtReimbStatus.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReimbStatus.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReimbStatus.TabIndex = 29;
            this.txtReimbStatus.Tag = "Designation";
            this.txtReimbStatus.WordWrap = false;
            // 
            // txtReimbType
            // 
            this.txtReimbType.Location = new System.Drawing.Point(150, 61);
            this.txtReimbType.Margin = new System.Windows.Forms.Padding(4);
            this.txtReimbType.Multiline = true;
            this.txtReimbType.Name = "txtReimbType";
            this.txtReimbType.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtReimbType.Size = new System.Drawing.Size(587, 28);
            this.txtReimbType.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReimbType.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtReimbType.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReimbType.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReimbType.TabIndex = 28;
            this.txtReimbType.Tag = "Name";
            this.txtReimbType.WordWrap = false;
            // 
            // txtRequestCode
            // 
            this.txtRequestCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRequestCode.Location = new System.Drawing.Point(150, 25);
            this.txtRequestCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtRequestCode.Multiline = true;
            this.txtRequestCode.Name = "txtRequestCode";
            this.txtRequestCode.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtRequestCode.ReadOnly = true;
            this.txtRequestCode.Size = new System.Drawing.Size(168, 28);
            this.txtRequestCode.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRequestCode.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtRequestCode.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRequestCode.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRequestCode.TabIndex = 27;
            this.txtRequestCode.Tag = "Employee Code";
            this.txtRequestCode.WordWrap = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(509, 43);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 15);
            this.label9.TabIndex = 20;
            this.label9.Text = "label9";
            this.label9.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(50, 103);
            this.label11.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 15);
            this.label11.TabIndex = 13;
            this.label11.Text = "Request Date";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(94, 138);
            this.label12.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 15);
            this.label12.TabIndex = 11;
            this.label12.Text = "Status";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(9, 68);
            this.label13.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(135, 15);
            this.label13.TabIndex = 9;
            this.label13.Text = "Reimbursment Type";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(47, 31);
            this.label14.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(97, 15);
            this.label14.TabIndex = 6;
            this.label14.Text = "Request Code";
            // 
            // groupBox8
            // 
            this.groupBox8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupBox8.Controls.Add(this.lblTaskID);
            this.groupBox8.Controls.Add(this.lblReimbRequestID);
            this.groupBox8.Controls.Add(this.lblClientID);
            this.groupBox8.Controls.Add(this.lblReimbTypeID);
            this.groupBox8.Controls.Add(this.lblRequestFromMailID);
            this.groupBox8.Controls.Add(this.lblEmpID);
            this.groupBox8.Controls.Add(this.lblPersonalInfoID);
            this.groupBox8.Controls.Add(this.txtDepartment);
            this.groupBox8.Controls.Add(this.txtDesignation);
            this.groupBox8.Controls.Add(this.txtEmpName);
            this.groupBox8.Controls.Add(this.txtEmpCode);
            this.groupBox8.Controls.Add(this.lblActionMode);
            this.groupBox8.Controls.Add(this.label38);
            this.groupBox8.Controls.Add(this.picEmpPhoto);
            this.groupBox8.Controls.Add(this.label37);
            this.groupBox8.Controls.Add(this.label36);
            this.groupBox8.Controls.Add(this.label35);
            this.groupBox8.Controls.Add(this.label34);
            this.groupBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox8.Location = new System.Drawing.Point(10, 208);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox8.Size = new System.Drawing.Size(1032, 174);
            this.groupBox8.TabIndex = 34;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Requestor Information";
            // 
            // lblTaskID
            // 
            this.lblTaskID.AutoSize = true;
            this.lblTaskID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblTaskID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaskID.Location = new System.Drawing.Point(763, 108);
            this.lblTaskID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTaskID.Name = "lblTaskID";
            this.lblTaskID.Size = new System.Drawing.Size(11, 15);
            this.lblTaskID.TabIndex = 84;
            this.lblTaskID.Text = " ";
            // 
            // lblReimbRequestID
            // 
            this.lblReimbRequestID.AutoSize = true;
            this.lblReimbRequestID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblReimbRequestID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReimbRequestID.Location = new System.Drawing.Point(763, 78);
            this.lblReimbRequestID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblReimbRequestID.Name = "lblReimbRequestID";
            this.lblReimbRequestID.Size = new System.Drawing.Size(11, 15);
            this.lblReimbRequestID.TabIndex = 83;
            this.lblReimbRequestID.Text = " ";
            // 
            // lblClientID
            // 
            this.lblClientID.AutoSize = true;
            this.lblClientID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblClientID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClientID.Location = new System.Drawing.Point(763, 48);
            this.lblClientID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblClientID.Name = "lblClientID";
            this.lblClientID.Size = new System.Drawing.Size(11, 15);
            this.lblClientID.TabIndex = 82;
            this.lblClientID.Text = " ";
            // 
            // lblReimbTypeID
            // 
            this.lblReimbTypeID.AutoSize = true;
            this.lblReimbTypeID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblReimbTypeID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReimbTypeID.Location = new System.Drawing.Point(442, 25);
            this.lblReimbTypeID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblReimbTypeID.Name = "lblReimbTypeID";
            this.lblReimbTypeID.Size = new System.Drawing.Size(11, 15);
            this.lblReimbTypeID.TabIndex = 81;
            this.lblReimbTypeID.Text = " ";
            // 
            // lblRequestFromMailID
            // 
            this.lblRequestFromMailID.AutoSize = true;
            this.lblRequestFromMailID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRequestFromMailID.Location = new System.Drawing.Point(547, 20);
            this.lblRequestFromMailID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblRequestFromMailID.Name = "lblRequestFromMailID";
            this.lblRequestFromMailID.Size = new System.Drawing.Size(82, 15);
            this.lblRequestFromMailID.TabIndex = 44;
            this.lblRequestFromMailID.Text = "Department";
            this.lblRequestFromMailID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblRequestFromMailID.Visible = false;
            // 
            // lblEmpID
            // 
            this.lblEmpID.AutoSize = true;
            this.lblEmpID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblEmpID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpID.Location = new System.Drawing.Point(509, 25);
            this.lblEmpID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblEmpID.Name = "lblEmpID";
            this.lblEmpID.Size = new System.Drawing.Size(11, 15);
            this.lblEmpID.TabIndex = 36;
            this.lblEmpID.Text = " ";
            // 
            // lblPersonalInfoID
            // 
            this.lblPersonalInfoID.AutoSize = true;
            this.lblPersonalInfoID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblPersonalInfoID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPersonalInfoID.Location = new System.Drawing.Point(652, 43);
            this.lblPersonalInfoID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblPersonalInfoID.Name = "lblPersonalInfoID";
            this.lblPersonalInfoID.Size = new System.Drawing.Size(11, 15);
            this.lblPersonalInfoID.TabIndex = 33;
            this.lblPersonalInfoID.Text = " ";
            this.lblPersonalInfoID.Visible = false;
            // 
            // txtDepartment
            // 
            this.txtDepartment.Location = new System.Drawing.Point(150, 135);
            this.txtDepartment.Margin = new System.Windows.Forms.Padding(4);
            this.txtDepartment.Multiline = true;
            this.txtDepartment.Name = "txtDepartment";
            this.txtDepartment.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtDepartment.Size = new System.Drawing.Size(587, 28);
            this.txtDepartment.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDepartment.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtDepartment.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDepartment.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDepartment.TabIndex = 30;
            this.txtDepartment.Tag = "Department";
            // 
            // txtDesignation
            // 
            this.txtDesignation.Location = new System.Drawing.Point(150, 98);
            this.txtDesignation.Margin = new System.Windows.Forms.Padding(4);
            this.txtDesignation.Multiline = true;
            this.txtDesignation.Name = "txtDesignation";
            this.txtDesignation.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtDesignation.Size = new System.Drawing.Size(587, 28);
            this.txtDesignation.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesignation.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtDesignation.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesignation.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesignation.TabIndex = 29;
            this.txtDesignation.Tag = "Designation";
            this.txtDesignation.WordWrap = false;
            // 
            // txtEmpName
            // 
            this.txtEmpName.Location = new System.Drawing.Point(150, 61);
            this.txtEmpName.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmpName.Multiline = true;
            this.txtEmpName.Name = "txtEmpName";
            this.txtEmpName.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtEmpName.Size = new System.Drawing.Size(587, 28);
            this.txtEmpName.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpName.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtEmpName.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpName.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpName.TabIndex = 28;
            this.txtEmpName.Tag = "Name";
            this.txtEmpName.WordWrap = false;
            // 
            // txtEmpCode
            // 
            this.txtEmpCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEmpCode.Location = new System.Drawing.Point(150, 25);
            this.txtEmpCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmpCode.Multiline = true;
            this.txtEmpCode.Name = "txtEmpCode";
            this.txtEmpCode.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtEmpCode.ReadOnly = true;
            this.txtEmpCode.Size = new System.Drawing.Size(168, 28);
            this.txtEmpCode.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpCode.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtEmpCode.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpCode.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpCode.TabIndex = 27;
            this.txtEmpCode.Tag = "Employee Code";
            this.txtEmpCode.WordWrap = false;
            // 
            // lblActionMode
            // 
            this.lblActionMode.AutoSize = true;
            this.lblActionMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActionMode.Location = new System.Drawing.Point(509, 43);
            this.lblActionMode.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblActionMode.Name = "lblActionMode";
            this.lblActionMode.Size = new System.Drawing.Size(98, 15);
            this.lblActionMode.TabIndex = 20;
            this.lblActionMode.Text = "lblActionMode";
            this.lblActionMode.Visible = false;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(805, 31);
            this.label38.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(44, 15);
            this.label38.TabIndex = 16;
            this.label38.Text = "Photo";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picEmpPhoto
            // 
            this.picEmpPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picEmpPhoto.Location = new System.Drawing.Point(852, 25);
            this.picEmpPhoto.Margin = new System.Windows.Forms.Padding(4);
            this.picEmpPhoto.Name = "picEmpPhoto";
            this.picEmpPhoto.Size = new System.Drawing.Size(161, 138);
            this.picEmpPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picEmpPhoto.TabIndex = 15;
            this.picEmpPhoto.TabStop = false;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(62, 142);
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
            this.label36.Location = new System.Drawing.Point(60, 105);
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
            this.label35.Location = new System.Drawing.Point(99, 68);
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
            this.label34.Location = new System.Drawing.Point(38, 31);
            this.label34.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(107, 15);
            this.label34.TabIndex = 6;
            this.label34.Text = "Employee Code";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupBox1.Controls.Add(this.txtComments);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtReimbursementAmount);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.dtReimbRequestDate);
            this.groupBox1.Controls.Add(this.label29);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(10, 384);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1035, 118);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            // 
            // txtComments
            // 
            this.txtComments.Location = new System.Drawing.Point(150, 48);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtComments.Size = new System.Drawing.Size(589, 55);
            this.txtComments.StateActive.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.txtComments.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.txtComments.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.txtComments.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.txtComments.TabIndex = 94;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(87, 19);
            this.label10.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(55, 15);
            this.label10.TabIndex = 93;
            this.label10.Text = "Amount";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtReimbursementAmount
            // 
            this.txtReimbursementAmount.Location = new System.Drawing.Point(150, 16);
            this.txtReimbursementAmount.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtReimbursementAmount.MaxLength = 255;
            this.txtReimbursementAmount.Name = "txtReimbursementAmount";
            this.txtReimbursementAmount.Size = new System.Drawing.Size(103, 21);
            this.txtReimbursementAmount.TabIndex = 89;
            this.txtReimbursementAmount.Tag = "Asset Quantity";
            this.txtReimbursementAmount.Text = "0";
            this.txtReimbursementAmount.WordWrap = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(67, 48);
            this.label15.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(75, 15);
            this.label15.TabIndex = 92;
            this.label15.Text = "Comments";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtReimbRequestDate
            // 
            this.dtReimbRequestDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(0)))), ((int)(((byte)(254)))));
            this.dtReimbRequestDate.Location = new System.Drawing.Point(393, 16);
            this.dtReimbRequestDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtReimbRequestDate.Mask = "##-##-####";
            this.dtReimbRequestDate.Name = "dtReimbRequestDate";
            this.dtReimbRequestDate.Size = new System.Drawing.Size(103, 21);
            this.dtReimbRequestDate.TabIndex = 90;
            this.dtReimbRequestDate.Tag = "Asset Request Date";
            this.dtReimbRequestDate.ValidatingType = typeof(System.DateTime);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(293, 19);
            this.label29.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(94, 15);
            this.label29.TabIndex = 91;
            this.label29.Text = "Request Date";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1057, 77);
            this.panel2.TabIndex = 1;
            // 
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(918, 11);
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
            this.btnRemoveDetails.Location = new System.Drawing.Point(776, 11);
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
            this.btnSaveDetails.Location = new System.Drawing.Point(147, 11);
            this.btnSaveDetails.Name = "btnSaveDetails";
            this.btnSaveDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSaveDetails.Size = new System.Drawing.Size(126, 38);
            this.btnSaveDetails.TabIndex = 18;
            this.btnSaveDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSaveDetails.Values.Image = global::StaffSync.Properties.Resources.save;
            this.btnSaveDetails.Values.Text = "Export Data";
            this.btnSaveDetails.Click += new System.EventHandler(this.btnSaveDetails_Click);
            // 
            // btnModifyDetails
            // 
            this.btnModifyDetails.Location = new System.Drawing.Point(556, 11);
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
            this.btnGenerateDetails.Location = new System.Drawing.Point(12, 11);
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
            this.btnCancel.Location = new System.Drawing.Point(281, 11);
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
            // txtRequestDescription
            // 
            this.txtRequestDescription.Location = new System.Drawing.Point(471, 99);
            this.txtRequestDescription.Multiline = true;
            this.txtRequestDescription.Name = "txtRequestDescription";
            this.txtRequestDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRequestDescription.Size = new System.Drawing.Size(267, 62);
            this.txtRequestDescription.StateActive.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.txtRequestDescription.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.txtRequestDescription.StateDisabled.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.txtRequestDescription.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.txtRequestDescription.TabIndex = 95;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(326, 103);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 15);
            this.label1.TabIndex = 96;
            this.label1.Text = "Request Description";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmEmpReimbursmentPayments
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1057, 802);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEmpReimbursmentPayments";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Daily Attendance Sheet";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.frmEmpReimbursmentPayout_Activated);
            this.Load += new System.EventHandler(this.frmEmpReimbursmentPayout_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmEmpReimbursmentPayout_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgReimbursmentPaymentHistory)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEmpPhoto)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBox2;
        private Krypton.Toolkit.KryptonLinkLabel kryptonLinkLabel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private Krypton.Toolkit.KryptonButton btnReimbursmentSearch;
        private Krypton.Toolkit.KryptonTextBox txtRequestDate;
        private Krypton.Toolkit.KryptonTextBox txtReimbStatus;
        private Krypton.Toolkit.KryptonTextBox txtReimbType;
        private Krypton.Toolkit.KryptonTextBox txtRequestCode;
        public System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label lblTaskID;
        private System.Windows.Forms.Label lblReimbRequestID;
        private System.Windows.Forms.Label lblClientID;
        private System.Windows.Forms.Label lblReimbTypeID;
        private System.Windows.Forms.Label lblRequestFromMailID;
        private System.Windows.Forms.Label lblEmpID;
        private System.Windows.Forms.Label lblPersonalInfoID;
        private Krypton.Toolkit.KryptonTextBox txtDepartment;
        private Krypton.Toolkit.KryptonTextBox txtDesignation;
        private Krypton.Toolkit.KryptonTextBox txtEmpName;
        private Krypton.Toolkit.KryptonTextBox txtEmpCode;
        public System.Windows.Forms.Label lblActionMode;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.PictureBox picEmpPhoto;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private Krypton.Toolkit.KryptonTextBox txtComments;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtReimbursementAmount;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.MaskedTextBox dtReimbRequestDate;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.GroupBox groupBox3;
        private Krypton.Toolkit.KryptonDataGridView dtgReimbursmentPaymentHistory;
        private Krypton.Toolkit.KryptonTextBox txtRequestDescription;
        private System.Windows.Forms.Label label1;
    }
}