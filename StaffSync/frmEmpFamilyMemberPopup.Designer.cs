namespace StaffSync
{
    partial class frmEmpFamilyMemberPopup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmpFamilyMemberPopup));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtRelationship = new Krypton.Toolkit.KryptonTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.chkInsuranceEnrolled = new System.Windows.Forms.CheckBox();
            this.txtBloodGroup = new Krypton.Toolkit.KryptonTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtMailID = new Krypton.Toolkit.KryptonTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtContactNumber = new Krypton.Toolkit.KryptonTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtAddress01 = new Krypton.Toolkit.KryptonTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAge = new Krypton.Toolkit.KryptonTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDOB = new System.Windows.Forms.MaskedTextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.cmbCountry = new Krypton.Toolkit.KryptonComboBox();
            this.txtPIN = new Krypton.Toolkit.KryptonTextBox();
            this.txtState = new Krypton.Toolkit.KryptonTextBox();
            this.txtCity = new Krypton.Toolkit.KryptonTextBox();
            this.txtArea = new Krypton.Toolkit.KryptonTextBox();
            this.txtAddress02 = new Krypton.Toolkit.KryptonTextBox();
            this.txtMemberName = new Krypton.Toolkit.KryptonTextBox();
            this.lblEmpPerFamInfoID = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUpdate = new Krypton.Toolkit.KryptonButton();
            this.btnCloseMe = new Krypton.Toolkit.KryptonButton();
            this.empMasInfoBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.staffsyncDBDTSet = new StaffSync.StaffsyncDBDTSet();
            this.empMasInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.empMasInfoTableAdapter = new StaffSync.StaffsyncDBDTSetTableAdapters.EmpMasInfoTableAdapter();
            this.empMasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.empMasTableAdapter = new StaffSync.StaffsyncDBDTSetTableAdapters.EmpMasTableAdapter();
            this.errValidator = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCountry)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(585, 611);
            this.splitContainer1.SplitterDistance = 552;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(585, 552);
            this.panel1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtRelationship);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.chkInsuranceEnrolled);
            this.groupBox2.Controls.Add(this.txtBloodGroup);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.txtMailID);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtContactNumber);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtAddress01);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtAge);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtDOB);
            this.groupBox2.Controls.Add(this.label29);
            this.groupBox2.Controls.Add(this.cmbCountry);
            this.groupBox2.Controls.Add(this.txtPIN);
            this.groupBox2.Controls.Add(this.txtState);
            this.groupBox2.Controls.Add(this.txtCity);
            this.groupBox2.Controls.Add(this.txtArea);
            this.groupBox2.Controls.Add(this.txtAddress02);
            this.groupBox2.Controls.Add(this.txtMemberName);
            this.groupBox2.Controls.Add(this.lblEmpPerFamInfoID);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(15, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(552, 532);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            // 
            // txtRelationship
            // 
            this.txtRelationship.Location = new System.Drawing.Point(144, 95);
            this.txtRelationship.Multiline = true;
            this.txtRelationship.Name = "txtRelationship";
            this.txtRelationship.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtRelationship.Size = new System.Drawing.Size(393, 28);
            this.txtRelationship.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRelationship.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtRelationship.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRelationship.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRelationship.TabIndex = 55;
            this.txtRelationship.WordWrap = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(51, 102);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 15);
            this.label13.TabIndex = 54;
            this.label13.Text = "Relationship";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkInsuranceEnrolled
            // 
            this.chkInsuranceEnrolled.AutoSize = true;
            this.chkInsuranceEnrolled.CheckAlign = System.Drawing.ContentAlignment.TopRight;
            this.chkInsuranceEnrolled.Checked = true;
            this.chkInsuranceEnrolled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInsuranceEnrolled.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkInsuranceEnrolled.Location = new System.Drawing.Point(9, 496);
            this.chkInsuranceEnrolled.Name = "chkInsuranceEnrolled";
            this.chkInsuranceEnrolled.Size = new System.Drawing.Size(147, 19);
            this.chkInsuranceEnrolled.TabIndex = 53;
            this.chkInsuranceEnrolled.Text = "Insurance Enrolled";
            this.chkInsuranceEnrolled.UseVisualStyleBackColor = true;
            // 
            // txtBloodGroup
            // 
            this.txtBloodGroup.Location = new System.Drawing.Point(144, 454);
            this.txtBloodGroup.Multiline = true;
            this.txtBloodGroup.Name = "txtBloodGroup";
            this.txtBloodGroup.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtBloodGroup.Size = new System.Drawing.Size(393, 28);
            this.txtBloodGroup.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBloodGroup.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtBloodGroup.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBloodGroup.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBloodGroup.TabIndex = 52;
            this.txtBloodGroup.WordWrap = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(52, 461);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 15);
            this.label12.TabIndex = 51;
            this.label12.Text = "Blood Group";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMailID
            // 
            this.txtMailID.Location = new System.Drawing.Point(144, 420);
            this.txtMailID.Multiline = true;
            this.txtMailID.Name = "txtMailID";
            this.txtMailID.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtMailID.Size = new System.Drawing.Size(393, 28);
            this.txtMailID.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMailID.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtMailID.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMailID.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMailID.TabIndex = 50;
            this.txtMailID.WordWrap = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(86, 427);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 15);
            this.label11.TabIndex = 49;
            this.label11.Text = "Mail ID";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtContactNumber
            // 
            this.txtContactNumber.Location = new System.Drawing.Point(144, 386);
            this.txtContactNumber.Multiline = true;
            this.txtContactNumber.Name = "txtContactNumber";
            this.txtContactNumber.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtContactNumber.Size = new System.Drawing.Size(393, 28);
            this.txtContactNumber.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactNumber.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtContactNumber.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactNumber.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtContactNumber.TabIndex = 48;
            this.txtContactNumber.WordWrap = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(29, 393);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 15);
            this.label10.TabIndex = 47;
            this.label10.Text = "Contact Number";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAddress01
            // 
            this.txtAddress01.Location = new System.Drawing.Point(144, 137);
            this.txtAddress01.Multiline = true;
            this.txtAddress01.Name = "txtAddress01";
            this.txtAddress01.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtAddress01.Size = new System.Drawing.Size(393, 28);
            this.txtAddress01.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress01.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtAddress01.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress01.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress01.TabIndex = 46;
            this.txtAddress01.WordWrap = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(81, 144);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 15);
            this.label9.TabIndex = 45;
            this.label9.Text = "Address";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(324, 61);
            this.txtAge.Multiline = true;
            this.txtAge.Name = "txtAge";
            this.txtAge.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtAge.ReadOnly = true;
            this.txtAge.Size = new System.Drawing.Size(155, 28);
            this.txtAge.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAge.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtAge.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAge.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAge.TabIndex = 44;
            this.txtAge.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(286, 68);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 43;
            this.label1.Text = "Age";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDOB
            // 
            this.txtDOB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(0)))), ((int)(((byte)(254)))));
            this.txtDOB.Location = new System.Drawing.Point(144, 65);
            this.txtDOB.Mask = "##-##-####";
            this.txtDOB.Name = "txtDOB";
            this.txtDOB.Size = new System.Drawing.Size(108, 21);
            this.txtDOB.TabIndex = 41;
            this.txtDOB.Tag = "Please enter Employeee Date Of Birth";
            this.txtDOB.ValidatingType = typeof(System.DateTime);
            this.txtDOB.TextChanged += new System.EventHandler(this.txtDOB_TextChanged);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(50, 68);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(89, 15);
            this.label29.TabIndex = 42;
            this.label29.Text = "Date Of Birth";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbCountry
            // 
            this.cmbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCountry.DropDownWidth = 440;
            this.cmbCountry.Location = new System.Drawing.Point(144, 348);
            this.cmbCountry.Name = "cmbCountry";
            this.cmbCountry.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbCountry.Size = new System.Drawing.Size(393, 22);
            this.cmbCountry.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbCountry.TabIndex = 40;
            // 
            // txtPIN
            // 
            this.txtPIN.Location = new System.Drawing.Point(144, 311);
            this.txtPIN.Multiline = true;
            this.txtPIN.Name = "txtPIN";
            this.txtPIN.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtPIN.Size = new System.Drawing.Size(173, 28);
            this.txtPIN.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPIN.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtPIN.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPIN.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPIN.TabIndex = 24;
            this.txtPIN.WordWrap = false;
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(144, 276);
            this.txtState.Multiline = true;
            this.txtState.Name = "txtState";
            this.txtState.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtState.Size = new System.Drawing.Size(393, 28);
            this.txtState.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtState.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtState.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtState.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtState.TabIndex = 23;
            this.txtState.WordWrap = false;
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(144, 241);
            this.txtCity.Multiline = true;
            this.txtCity.Name = "txtCity";
            this.txtCity.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtCity.Size = new System.Drawing.Size(393, 28);
            this.txtCity.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCity.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtCity.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCity.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCity.TabIndex = 22;
            this.txtCity.WordWrap = false;
            // 
            // txtArea
            // 
            this.txtArea.Location = new System.Drawing.Point(144, 206);
            this.txtArea.Multiline = true;
            this.txtArea.Name = "txtArea";
            this.txtArea.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtArea.Size = new System.Drawing.Size(393, 28);
            this.txtArea.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtArea.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtArea.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtArea.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtArea.TabIndex = 21;
            // 
            // txtAddress02
            // 
            this.txtAddress02.Location = new System.Drawing.Point(144, 171);
            this.txtAddress02.Multiline = true;
            this.txtAddress02.Name = "txtAddress02";
            this.txtAddress02.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtAddress02.Size = new System.Drawing.Size(393, 28);
            this.txtAddress02.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress02.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtAddress02.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress02.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress02.TabIndex = 20;
            this.txtAddress02.WordWrap = false;
            // 
            // txtMemberName
            // 
            this.txtMemberName.Location = new System.Drawing.Point(144, 26);
            this.txtMemberName.Multiline = true;
            this.txtMemberName.Name = "txtMemberName";
            this.txtMemberName.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtMemberName.Size = new System.Drawing.Size(393, 28);
            this.txtMemberName.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMemberName.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtMemberName.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMemberName.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMemberName.TabIndex = 19;
            this.txtMemberName.WordWrap = false;
            // 
            // lblEmpPerFamInfoID
            // 
            this.lblEmpPerFamInfoID.AutoSize = true;
            this.lblEmpPerFamInfoID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblEmpPerFamInfoID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpPerFamInfoID.Location = new System.Drawing.Point(538, 9);
            this.lblEmpPerFamInfoID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmpPerFamInfoID.Name = "lblEmpPerFamInfoID";
            this.lblEmpPerFamInfoID.Size = new System.Drawing.Size(11, 15);
            this.lblEmpPerFamInfoID.TabIndex = 18;
            this.lblEmpPerFamInfoID.Text = " ";
            this.lblEmpPerFamInfoID.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(84, 349);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 15);
            this.label8.TabIndex = 17;
            this.label8.Text = "Country";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(109, 318);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 15);
            this.label7.TabIndex = 15;
            this.label7.Text = "PIN";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(99, 283);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "State";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(109, 248);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "City";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(103, 213);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Area";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(81, 178);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Address";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(37, 33);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Member Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel2.Controls.Add(this.btnUpdate);
            this.panel2.Controls.Add(this.btnCloseMe);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(585, 55);
            this.panel2.TabIndex = 1;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(426, 9);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnUpdate.Size = new System.Drawing.Size(126, 38);
            this.btnUpdate.TabIndex = 16;
            this.btnUpdate.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnUpdate.Values.Image = global::StaffSync.Properties.Resources.update;
            this.btnUpdate.Values.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(28, 9);
            this.btnCloseMe.Name = "btnCloseMe";
            this.btnCloseMe.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCloseMe.Size = new System.Drawing.Size(126, 38);
            this.btnCloseMe.TabIndex = 15;
            this.btnCloseMe.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCloseMe.Values.Image = global::StaffSync.Properties.Resources.cancel;
            this.btnCloseMe.Values.Text = "Cancel";
            this.btnCloseMe.Click += new System.EventHandler(this.btnCloseMe_Click_1);
            // 
            // empMasInfoBindingSource1
            // 
            this.empMasInfoBindingSource1.DataMember = "EmpMasInfo";
            this.empMasInfoBindingSource1.DataSource = this.staffsyncDBDTSet;
            // 
            // staffsyncDBDTSet
            // 
            this.staffsyncDBDTSet.DataSetName = "StaffsyncDBDTSet";
            this.staffsyncDBDTSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // empMasInfoBindingSource
            // 
            this.empMasInfoBindingSource.DataMember = "EmpMasInfo";
            this.empMasInfoBindingSource.DataSource = this.staffsyncDBDTSet;
            // 
            // empMasInfoTableAdapter
            // 
            this.empMasInfoTableAdapter.ClearBeforeFill = true;
            // 
            // empMasBindingSource
            // 
            this.empMasBindingSource.DataMember = "EmpMas";
            this.empMasBindingSource.DataSource = this.staffsyncDBDTSet;
            // 
            // empMasTableAdapter
            // 
            this.empMasTableAdapter.ClearBeforeFill = true;
            // 
            // errValidator
            // 
            this.errValidator.ContainerControl = this;
            // 
            // frmEmpFamilyMemberPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 611);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEmpFamilyMemberPopup";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Family Members Info";
            this.Load += new System.EventHandler(this.frmEmpFamilyMemberPopup_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmEmpFamilyMemberPopup_KeyUp);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbCountry)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private StaffsyncDBDTSet staffsyncDBDTSet;
        private System.Windows.Forms.BindingSource empMasInfoBindingSource;
        private StaffsyncDBDTSetTableAdapters.EmpMasInfoTableAdapter empMasInfoTableAdapter;
        private System.Windows.Forms.BindingSource empMasBindingSource;
        private StaffsyncDBDTSetTableAdapters.EmpMasTableAdapter empMasTableAdapter;
        private System.Windows.Forms.BindingSource empMasInfoBindingSource1;
        private Krypton.Toolkit.KryptonButton btnCloseMe;
        private System.Windows.Forms.GroupBox groupBox2;
        private Krypton.Toolkit.KryptonComboBox cmbCountry;
        private Krypton.Toolkit.KryptonTextBox txtPIN;
        private Krypton.Toolkit.KryptonTextBox txtState;
        private Krypton.Toolkit.KryptonTextBox txtCity;
        private Krypton.Toolkit.KryptonTextBox txtArea;
        private Krypton.Toolkit.KryptonTextBox txtAddress02;
        private Krypton.Toolkit.KryptonTextBox txtMemberName;
        private System.Windows.Forms.Label lblEmpPerFamInfoID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Krypton.Toolkit.KryptonTextBox txtAge;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox txtDOB;
        private System.Windows.Forms.Label label29;
        private Krypton.Toolkit.KryptonTextBox txtAddress01;
        private System.Windows.Forms.Label label9;
        private Krypton.Toolkit.KryptonTextBox txtBloodGroup;
        private System.Windows.Forms.Label label12;
        private Krypton.Toolkit.KryptonTextBox txtMailID;
        private System.Windows.Forms.Label label11;
        private Krypton.Toolkit.KryptonTextBox txtContactNumber;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkInsuranceEnrolled;
        private Krypton.Toolkit.KryptonButton btnUpdate;
        private Krypton.Toolkit.KryptonTextBox txtRelationship;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ErrorProvider errValidator;
    }
}