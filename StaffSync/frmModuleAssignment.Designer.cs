namespace StaffSync
{
    partial class frmModuleAssignment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModuleAssignment));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtgModulesList = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.btnCloseMe = new Krypton.Toolkit.KryptonButton();
            this.btnSaveDetails = new Krypton.Toolkit.KryptonButton();
            this.btnModifyDetails = new Krypton.Toolkit.KryptonButton();
            this.errValidator = new System.Windows.Forms.ErrorProvider(this.components);
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.qryUserSpecificRolesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.staffsyncDBDTSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.staffsyncDBDTSet = new StaffSync.StaffsyncDBDTSet();
            this.qryRolesDefBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qryUserRolesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qryUserRolesTableAdapter = new StaffSync.StaffsyncDBDTSetTableAdapters.qryUserRolesTableAdapter();
            this.qryRolesDefTableAdapter = new StaffSync.StaffsyncDBDTSetTableAdapters.qryRolesDefTableAdapter();
            this.qryUserSpecificRolesTableAdapter = new StaffSync.StaffsyncDBDTSetTableAdapters.qryUserSpecificRolesTableAdapter();
            this.txtAssignedRole = new Krypton.Toolkit.KryptonTextBox();
            this.label28 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRepEmpPhoto)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgModulesList)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryUserSpecificRolesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryRolesDefBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryUserRolesBindingSource)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(1237, 662);
            this.splitContainer1.SplitterDistance = 585;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.groupBox8);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1237, 585);
            this.panel1.TabIndex = 1;
            // 
            // groupBox8
            // 
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
            this.groupBox8.Location = new System.Drawing.Point(16, 4);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox8.Size = new System.Drawing.Size(1204, 184);
            this.groupBox8.TabIndex = 31;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "User Information";
            // 
            // btnReportingManagerSearch
            // 
            this.btnReportingManagerSearch.Location = new System.Drawing.Point(420, 25);
            this.btnReportingManagerSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnReportingManagerSearch.Name = "btnReportingManagerSearch";
            this.btnReportingManagerSearch.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnReportingManagerSearch.Size = new System.Drawing.Size(35, 29);
            this.btnReportingManagerSearch.TabIndex = 44;
            this.btnReportingManagerSearch.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnReportingManagerSearch.Values.Image = global::StaffSync.Properties.Resources.search;
            this.btnReportingManagerSearch.Values.Text = "";
            this.btnReportingManagerSearch.Click += new System.EventHandler(this.btnReportingManagerSearch_Click);
            // 
            // txtRepEmpDepartment
            // 
            this.txtRepEmpDepartment.Location = new System.Drawing.Point(188, 137);
            this.txtRepEmpDepartment.Margin = new System.Windows.Forms.Padding(4);
            this.txtRepEmpDepartment.Multiline = true;
            this.txtRepEmpDepartment.Name = "txtRepEmpDepartment";
            this.txtRepEmpDepartment.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtRepEmpDepartment.Size = new System.Drawing.Size(587, 28);
            this.txtRepEmpDepartment.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpDepartment.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtRepEmpDepartment.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpDepartment.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpDepartment.TabIndex = 43;
            // 
            // txtRepEmpDesig
            // 
            this.txtRepEmpDesig.Location = new System.Drawing.Point(188, 100);
            this.txtRepEmpDesig.Margin = new System.Windows.Forms.Padding(4);
            this.txtRepEmpDesig.Multiline = true;
            this.txtRepEmpDesig.Name = "txtRepEmpDesig";
            this.txtRepEmpDesig.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtRepEmpDesig.Size = new System.Drawing.Size(587, 28);
            this.txtRepEmpDesig.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpDesig.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtRepEmpDesig.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpDesig.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpDesig.TabIndex = 42;
            this.txtRepEmpDesig.WordWrap = false;
            // 
            // txtRepEmpName
            // 
            this.txtRepEmpName.Location = new System.Drawing.Point(188, 63);
            this.txtRepEmpName.Margin = new System.Windows.Forms.Padding(4);
            this.txtRepEmpName.Multiline = true;
            this.txtRepEmpName.Name = "txtRepEmpName";
            this.txtRepEmpName.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtRepEmpName.Size = new System.Drawing.Size(587, 28);
            this.txtRepEmpName.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpName.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtRepEmpName.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpName.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpName.TabIndex = 41;
            this.txtRepEmpName.WordWrap = false;
            // 
            // txtRepEmpCode
            // 
            this.txtRepEmpCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRepEmpCode.Location = new System.Drawing.Point(188, 26);
            this.txtRepEmpCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtRepEmpCode.Multiline = true;
            this.txtRepEmpCode.Name = "txtRepEmpCode";
            this.txtRepEmpCode.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtRepEmpCode.ReadOnly = true;
            this.txtRepEmpCode.Size = new System.Drawing.Size(224, 28);
            this.txtRepEmpCode.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpCode.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtRepEmpCode.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpCode.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpCode.TabIndex = 40;
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
            this.label38.Location = new System.Drawing.Point(917, 28);
            this.label38.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(44, 15);
            this.label38.TabIndex = 16;
            this.label38.Text = "Photo";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picRepEmpPhoto
            // 
            this.picRepEmpPhoto.Location = new System.Drawing.Point(977, 25);
            this.picRepEmpPhoto.Margin = new System.Windows.Forms.Padding(4);
            this.picRepEmpPhoto.Name = "picRepEmpPhoto";
            this.picRepEmpPhoto.Size = new System.Drawing.Size(176, 140);
            this.picRepEmpPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRepEmpPhoto.TabIndex = 15;
            this.picRepEmpPhoto.TabStop = false;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(97, 143);
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
            this.label36.Location = new System.Drawing.Point(95, 107);
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
            this.label35.Location = new System.Drawing.Point(134, 72);
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
            this.label34.Location = new System.Drawing.Point(72, 33);
            this.label34.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(107, 15);
            this.label34.TabIndex = 6;
            this.label34.Text = "Employee Code";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtAssignedRole);
            this.groupBox1.Controls.Add(this.label28);
            this.groupBox1.Controls.Add(this.dtgModulesList);
            this.groupBox1.Location = new System.Drawing.Point(16, 177);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1204, 337);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // dtgModulesList
            // 
            this.dtgModulesList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.dtgModulesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgModulesList.Location = new System.Drawing.Point(47, 64);
            this.dtgModulesList.Margin = new System.Windows.Forms.Padding(4);
            this.dtgModulesList.Name = "dtgModulesList";
            this.dtgModulesList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgModulesList.Size = new System.Drawing.Size(1139, 262);
            this.dtgModulesList.TabIndex = 31;
            this.dtgModulesList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgModulesList_CellContentClick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnCloseMe);
            this.panel2.Controls.Add(this.btnSaveDetails);
            this.panel2.Controls.Add(this.btnModifyDetails);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1237, 72);
            this.panel2.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(331, 12);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCancel.Size = new System.Drawing.Size(126, 38);
            this.btnCancel.TabIndex = 26;
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Image = global::StaffSync.Properties.Resources.cancel;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(1033, 12);
            this.btnCloseMe.Margin = new System.Windows.Forms.Padding(4);
            this.btnCloseMe.Name = "btnCloseMe";
            this.btnCloseMe.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCloseMe.Size = new System.Drawing.Size(126, 38);
            this.btnCloseMe.TabIndex = 29;
            this.btnCloseMe.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCloseMe.Values.Image = global::StaffSync.Properties.Resources.close;
            this.btnCloseMe.Values.Text = "Close Me";
            this.btnCloseMe.Click += new System.EventHandler(this.btnCloseMe_Click);
            // 
            // btnSaveDetails
            // 
            this.btnSaveDetails.Location = new System.Drawing.Point(197, 12);
            this.btnSaveDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveDetails.Name = "btnSaveDetails";
            this.btnSaveDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSaveDetails.Size = new System.Drawing.Size(126, 38);
            this.btnSaveDetails.TabIndex = 28;
            this.btnSaveDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSaveDetails.Values.Image = global::StaffSync.Properties.Resources.save;
            this.btnSaveDetails.Values.Text = "Save";
            this.btnSaveDetails.Click += new System.EventHandler(this.btnSaveDetails_Click);
            // 
            // btnModifyDetails
            // 
            this.btnModifyDetails.Location = new System.Drawing.Point(63, 12);
            this.btnModifyDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnModifyDetails.Name = "btnModifyDetails";
            this.btnModifyDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnModifyDetails.Size = new System.Drawing.Size(126, 38);
            this.btnModifyDetails.TabIndex = 27;
            this.btnModifyDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnModifyDetails.Values.Image = global::StaffSync.Properties.Resources.update;
            this.btnModifyDetails.Values.Text = "Modify";
            this.btnModifyDetails.Click += new System.EventHandler(this.btnModifyDetails_Click);
            // 
            // errValidator
            // 
            this.errValidator.ContainerControl = this;
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList.Images.SetKeyName(0, "lock.png");
            this.imgList.Images.SetKeyName(1, "unlock.png");
            this.imgList.Images.SetKeyName(2, "green-circle.png");
            this.imgList.Images.SetKeyName(3, "red-circle.png");
            // 
            // qryUserSpecificRolesBindingSource
            // 
            this.qryUserSpecificRolesBindingSource.DataMember = "qryUserSpecificRoles";
            this.qryUserSpecificRolesBindingSource.DataSource = this.staffsyncDBDTSetBindingSource;
            // 
            // staffsyncDBDTSetBindingSource
            // 
            this.staffsyncDBDTSetBindingSource.DataSource = this.staffsyncDBDTSet;
            this.staffsyncDBDTSetBindingSource.Position = 0;
            // 
            // staffsyncDBDTSet
            // 
            this.staffsyncDBDTSet.DataSetName = "StaffsyncDBDTSet";
            this.staffsyncDBDTSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // qryRolesDefBindingSource
            // 
            this.qryRolesDefBindingSource.DataMember = "qryRolesDef";
            this.qryRolesDefBindingSource.DataSource = this.staffsyncDBDTSet;
            // 
            // qryUserRolesBindingSource
            // 
            this.qryUserRolesBindingSource.DataMember = "qryUserRoles";
            this.qryUserRolesBindingSource.DataSource = this.staffsyncDBDTSet;
            // 
            // qryUserRolesTableAdapter
            // 
            this.qryUserRolesTableAdapter.ClearBeforeFill = true;
            // 
            // qryRolesDefTableAdapter
            // 
            this.qryRolesDefTableAdapter.ClearBeforeFill = true;
            // 
            // qryUserSpecificRolesTableAdapter
            // 
            this.qryUserSpecificRolesTableAdapter.ClearBeforeFill = true;
            // 
            // txtAssignedRole
            // 
            this.txtAssignedRole.Enabled = false;
            this.txtAssignedRole.Location = new System.Drawing.Point(188, 20);
            this.txtAssignedRole.Multiline = true;
            this.txtAssignedRole.Name = "txtAssignedRole";
            this.txtAssignedRole.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtAssignedRole.Size = new System.Drawing.Size(587, 28);
            this.txtAssignedRole.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAssignedRole.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtAssignedRole.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAssignedRole.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAssignedRole.TabIndex = 42;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label28.Location = new System.Drawing.Point(85, 27);
            this.label28.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(99, 15);
            this.label28.TabIndex = 41;
            this.label28.Text = "Assigned Role";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmModuleAssignment
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1237, 662);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmModuleAssignment";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Management";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmModuleAssignment_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmModuleAssignment_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRepEmpPhoto)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgModulesList)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryUserSpecificRolesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryRolesDefBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryUserRolesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label lblReportingManagerID;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.PictureBox picRepEmpPhoto;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        public System.Windows.Forms.Label lblActionMode;
        private System.Windows.Forms.ErrorProvider errValidator;
        private System.Windows.Forms.ImageList imgList;
        private StaffsyncDBDTSet staffsyncDBDTSet;
        private System.Windows.Forms.BindingSource qryUserRolesBindingSource;
        private StaffsyncDBDTSetTableAdapters.qryUserRolesTableAdapter qryUserRolesTableAdapter;
        private System.Windows.Forms.BindingSource qryRolesDefBindingSource;
        private StaffsyncDBDTSetTableAdapters.qryRolesDefTableAdapter qryRolesDefTableAdapter;
        private System.Windows.Forms.BindingSource staffsyncDBDTSetBindingSource;
        private System.Windows.Forms.BindingSource qryUserSpecificRolesBindingSource;
        private StaffsyncDBDTSetTableAdapters.qryUserSpecificRolesTableAdapter qryUserSpecificRolesTableAdapter;
        private System.Windows.Forms.DataGridView dtgModulesList;
        private Krypton.Toolkit.KryptonTextBox txtRepEmpDepartment;
        private Krypton.Toolkit.KryptonTextBox txtRepEmpDesig;
        private Krypton.Toolkit.KryptonTextBox txtRepEmpName;
        private Krypton.Toolkit.KryptonTextBox txtRepEmpCode;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private Krypton.Toolkit.KryptonButton btnCloseMe;
        private Krypton.Toolkit.KryptonButton btnSaveDetails;
        private Krypton.Toolkit.KryptonButton btnModifyDetails;
        private Krypton.Toolkit.KryptonButton btnReportingManagerSearch;
        private Krypton.Toolkit.KryptonTextBox txtAssignedRole;
        private System.Windows.Forms.Label label28;
    }
}