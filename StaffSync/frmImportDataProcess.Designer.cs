namespace StaffSync
{
    partial class frmImportDataProcess
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportDataProcess));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtgImportDataSourceList = new Krypton.Toolkit.KryptonDataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblSelectedDataAction = new System.Windows.Forms.Label();
            this.chkSelectOrUnselect = new Krypton.Toolkit.KryptonCheckButton();
            this.chkAvoidDuplicateRows = new System.Windows.Forms.CheckBox();
            this.btnSearch = new Krypton.Toolkit.KryptonButton();
            this.txtSourceFilePath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTotalNotImportedRows = new System.Windows.Forms.Label();
            this.lblTotalDuplicateRows = new System.Windows.Forms.Label();
            this.lblTotalImportedRows = new System.Windows.Forms.Label();
            this.lblTotalRowsSelected = new System.Windows.Forms.Label();
            this.lblTotalRowsFromSource = new System.Windows.Forms.Label();
            this.dtgImportDataPreview = new Krypton.Toolkit.KryptonDataGridView();
            this.lnkViewAuditLog = new Krypton.Toolkit.KryptonLinkLabel();
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgImportDataSourceList)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgImportDataPreview)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(1195, 570);
            this.splitContainer1.SplitterDistance = 512;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1195, 512);
            this.panel1.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupBox5.Controls.Add(this.groupBox1);
            this.groupBox5.Controls.Add(this.groupBox4);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(10, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(1175, 487);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtgImportDataSourceList);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(370, 459);
            this.groupBox1.TabIndex = 66;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Import Data Action";
            // 
            // dtgImportDataSourceList
            // 
            this.dtgImportDataSourceList.AllowUserToAddRows = false;
            this.dtgImportDataSourceList.AllowUserToDeleteRows = false;
            this.dtgImportDataSourceList.AllowUserToResizeRows = false;
            this.dtgImportDataSourceList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgImportDataSourceList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgImportDataSourceList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dtgImportDataSourceList.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dtgImportDataSourceList.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.ContextMenuItemImage;
            this.dtgImportDataSourceList.Location = new System.Drawing.Point(11, 25);
            this.dtgImportDataSourceList.Name = "dtgImportDataSourceList";
            this.dtgImportDataSourceList.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtgImportDataSourceList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgImportDataSourceList.Size = new System.Drawing.Size(349, 422);
            this.dtgImportDataSourceList.TabIndex = 64;
            this.dtgImportDataSourceList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgImportDataSourceList_CellDoubleClick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblSelectedDataAction);
            this.groupBox4.Controls.Add(this.chkSelectOrUnselect);
            this.groupBox4.Controls.Add(this.chkAvoidDuplicateRows);
            this.groupBox4.Controls.Add(this.btnSearch);
            this.groupBox4.Controls.Add(this.txtSourceFilePath);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.lblTotalNotImportedRows);
            this.groupBox4.Controls.Add(this.lblTotalDuplicateRows);
            this.groupBox4.Controls.Add(this.lblTotalImportedRows);
            this.groupBox4.Controls.Add(this.lblTotalRowsSelected);
            this.groupBox4.Controls.Add(this.lblTotalRowsFromSource);
            this.groupBox4.Controls.Add(this.dtgImportDataPreview);
            this.groupBox4.Controls.Add(this.lnkViewAuditLog);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(390, 18);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(775, 459);
            this.groupBox4.TabIndex = 65;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Asset Information";
            // 
            // lblSelectedDataAction
            // 
            this.lblSelectedDataAction.AutoSize = true;
            this.lblSelectedDataAction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lblSelectedDataAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectedDataAction.Location = new System.Drawing.Point(354, 75);
            this.lblSelectedDataAction.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblSelectedDataAction.Name = "lblSelectedDataAction";
            this.lblSelectedDataAction.Size = new System.Drawing.Size(11, 15);
            this.lblSelectedDataAction.TabIndex = 87;
            this.lblSelectedDataAction.Text = " ";
            this.lblSelectedDataAction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblSelectedDataAction.Visible = false;
            // 
            // chkSelectOrUnselect
            // 
            this.chkSelectOrUnselect.Enabled = false;
            this.chkSelectOrUnselect.Location = new System.Drawing.Point(106, 119);
            this.chkSelectOrUnselect.Name = "chkSelectOrUnselect";
            this.chkSelectOrUnselect.Size = new System.Drawing.Size(87, 15);
            this.chkSelectOrUnselect.TabIndex = 86;
            this.chkSelectOrUnselect.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.chkSelectOrUnselect.Values.Text = "Unselect";
            this.chkSelectOrUnselect.Click += new System.EventHandler(this.chkSelectOrUnselect_Click);
            // 
            // chkAvoidDuplicateRows
            // 
            this.chkAvoidDuplicateRows.AutoSize = true;
            this.chkAvoidDuplicateRows.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAvoidDuplicateRows.Checked = true;
            this.chkAvoidDuplicateRows.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAvoidDuplicateRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkAvoidDuplicateRows.Location = new System.Drawing.Point(10, 75);
            this.chkAvoidDuplicateRows.Name = "chkAvoidDuplicateRows";
            this.chkAvoidDuplicateRows.Size = new System.Drawing.Size(164, 19);
            this.chkAvoidDuplicateRows.TabIndex = 85;
            this.chkAvoidDuplicateRows.Tag = "Avoid Duplicate Rows";
            this.chkAvoidDuplicateRows.Text = "Avoid Duplicate Rows";
            this.chkAvoidDuplicateRows.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(680, 25);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSearch.Size = new System.Drawing.Size(39, 28);
            this.btnSearch.TabIndex = 74;
            this.btnSearch.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSearch.Values.Image = global::StaffSync.Properties.Resources.search;
            this.btnSearch.Values.Text = "";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSourceFilePath
            // 
            this.txtSourceFilePath.Location = new System.Drawing.Point(95, 25);
            this.txtSourceFilePath.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.txtSourceFilePath.MaxLength = 255;
            this.txtSourceFilePath.Multiline = true;
            this.txtSourceFilePath.Name = "txtSourceFilePath";
            this.txtSourceFilePath.ReadOnly = true;
            this.txtSourceFilePath.Size = new System.Drawing.Size(576, 28);
            this.txtSourceFilePath.TabIndex = 72;
            this.txtSourceFilePath.Tag = "Asset Code ";
            this.txtSourceFilePath.WordWrap = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 32);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 15);
            this.label6.TabIndex = 73;
            this.label6.Text = "Source File";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 119);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 15);
            this.label5.TabIndex = 71;
            this.label5.Text = "Data Preview";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalNotImportedRows
            // 
            this.lblTotalNotImportedRows.AutoSize = true;
            this.lblTotalNotImportedRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalNotImportedRows.Location = new System.Drawing.Point(610, 427);
            this.lblTotalNotImportedRows.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTotalNotImportedRows.Name = "lblTotalNotImportedRows";
            this.lblTotalNotImportedRows.Size = new System.Drawing.Size(149, 15);
            this.lblTotalNotImportedRows.TabIndex = 70;
            this.lblTotalNotImportedRows.Text = "Not Imported Rows : 0";
            this.lblTotalNotImportedRows.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalDuplicateRows
            // 
            this.lblTotalDuplicateRows.AutoSize = true;
            this.lblTotalDuplicateRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalDuplicateRows.Location = new System.Drawing.Point(405, 427);
            this.lblTotalDuplicateRows.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTotalDuplicateRows.Name = "lblTotalDuplicateRows";
            this.lblTotalDuplicateRows.Size = new System.Drawing.Size(163, 15);
            this.lblTotalDuplicateRows.TabIndex = 69;
            this.lblTotalDuplicateRows.Text = "Total Duplicate Rows : 0";
            this.lblTotalDuplicateRows.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalImportedRows
            // 
            this.lblTotalImportedRows.AutoSize = true;
            this.lblTotalImportedRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalImportedRows.Location = new System.Drawing.Point(204, 427);
            this.lblTotalImportedRows.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTotalImportedRows.Name = "lblTotalImportedRows";
            this.lblTotalImportedRows.Size = new System.Drawing.Size(159, 15);
            this.lblTotalImportedRows.TabIndex = 68;
            this.lblTotalImportedRows.Text = "Total Imported Rows : 0";
            this.lblTotalImportedRows.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalRowsSelected
            // 
            this.lblTotalRowsSelected.AutoSize = true;
            this.lblTotalRowsSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRowsSelected.Location = new System.Drawing.Point(4, 427);
            this.lblTotalRowsSelected.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTotalRowsSelected.Name = "lblTotalRowsSelected";
            this.lblTotalRowsSelected.Size = new System.Drawing.Size(158, 15);
            this.lblTotalRowsSelected.TabIndex = 67;
            this.lblTotalRowsSelected.Text = "Total Rows Selected : 0";
            this.lblTotalRowsSelected.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalRowsFromSource
            // 
            this.lblTotalRowsFromSource.AutoSize = true;
            this.lblTotalRowsFromSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalRowsFromSource.Location = new System.Drawing.Point(539, 119);
            this.lblTotalRowsFromSource.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblTotalRowsFromSource.Name = "lblTotalRowsFromSource";
            this.lblTotalRowsFromSource.Size = new System.Drawing.Size(180, 15);
            this.lblTotalRowsFromSource.TabIndex = 66;
            this.lblTotalRowsFromSource.Text = "Total Rows from Source : 0";
            this.lblTotalRowsFromSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtgImportDataPreview
            // 
            this.dtgImportDataPreview.AllowUserToAddRows = false;
            this.dtgImportDataPreview.AllowUserToDeleteRows = false;
            this.dtgImportDataPreview.AllowUserToResizeRows = false;
            this.dtgImportDataPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgImportDataPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgImportDataPreview.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dtgImportDataPreview.Enabled = false;
            this.dtgImportDataPreview.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dtgImportDataPreview.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.ContextMenuItemImage;
            this.dtgImportDataPreview.Location = new System.Drawing.Point(7, 137);
            this.dtgImportDataPreview.Name = "dtgImportDataPreview";
            this.dtgImportDataPreview.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtgImportDataPreview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgImportDataPreview.Size = new System.Drawing.Size(777, 279);
            this.dtgImportDataPreview.StateCommon.Background.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.dtgImportDataPreview.StateCommon.Background.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.dtgImportDataPreview.StateCommon.BackStyle = Krypton.Toolkit.PaletteBackStyle.ContextMenuItemImage;
            this.dtgImportDataPreview.StateDisabled.Background.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.dtgImportDataPreview.StateDisabled.Background.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.dtgImportDataPreview.StateNormal.Background.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.dtgImportDataPreview.StateNormal.Background.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.dtgImportDataPreview.TabIndex = 65;
            // 
            // lnkViewAuditLog
            // 
            this.lnkViewAuditLog.Location = new System.Drawing.Point(726, 31);
            this.lnkViewAuditLog.Name = "lnkViewAuditLog";
            this.lnkViewAuditLog.Size = new System.Drawing.Size(93, 20);
            this.lnkViewAuditLog.TabIndex = 37;
            this.lnkViewAuditLog.Values.Text = "View Audit Log";
            this.lnkViewAuditLog.Visible = false;
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
            this.panel2.Size = new System.Drawing.Size(1195, 54);
            this.panel2.TabIndex = 1;
            // 
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(1040, 6);
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
            this.btnRemoveDetails.Location = new System.Drawing.Point(152, 6);
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
            this.btnSaveDetails.Enabled = false;
            this.btnSaveDetails.Location = new System.Drawing.Point(20, 6);
            this.btnSaveDetails.Name = "btnSaveDetails";
            this.btnSaveDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSaveDetails.Size = new System.Drawing.Size(126, 38);
            this.btnSaveDetails.TabIndex = 18;
            this.btnSaveDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSaveDetails.Values.Image = global::StaffSync.Properties.Resources.execute;
            this.btnSaveDetails.Values.Text = "Import Data";
            this.btnSaveDetails.Click += new System.EventHandler(this.btnSaveDetails_Click);
            // 
            // btnModifyDetails
            // 
            this.btnModifyDetails.Location = new System.Drawing.Point(284, 6);
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
            this.btnGenerateDetails.Location = new System.Drawing.Point(284, 6);
            this.btnGenerateDetails.Name = "btnGenerateDetails";
            this.btnGenerateDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnGenerateDetails.Size = new System.Drawing.Size(126, 38);
            this.btnGenerateDetails.TabIndex = 16;
            this.btnGenerateDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnGenerateDetails.Values.Image = global::StaffSync.Properties.Resources._new;
            this.btnGenerateDetails.Values.Text = "Generate";
            this.btnGenerateDetails.Visible = false;
            this.btnGenerateDetails.Click += new System.EventHandler(this.btnGenerateDetails_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(284, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCancel.Size = new System.Drawing.Size(126, 38);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Image = global::StaffSync.Properties.Resources.cancel;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Visible = false;
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
            // frmImportDataProcess
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1195, 570);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmImportDataProcess";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Daily Attendance Sheet";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.frmImportDataProcess_Activated);
            this.Load += new System.EventHandler(this.frmImportDataProcess_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmImportDataProcess_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgImportDataSourceList)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgImportDataPreview)).EndInit();
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
        private System.Windows.Forms.GroupBox groupBox5;
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
        private Krypton.Toolkit.KryptonDataGridView dtgImportDataSourceList;
        private System.Windows.Forms.GroupBox groupBox4;
        private Krypton.Toolkit.KryptonLinkLabel lnkViewAuditLog;
        private Krypton.Toolkit.KryptonDataGridView dtgImportDataPreview;
        private System.Windows.Forms.Label lblTotalNotImportedRows;
        private System.Windows.Forms.Label lblTotalDuplicateRows;
        private System.Windows.Forms.Label lblTotalImportedRows;
        private System.Windows.Forms.Label lblTotalRowsSelected;
        private System.Windows.Forms.Label lblTotalRowsFromSource;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSourceFilePath;
        private System.Windows.Forms.Label label6;
        private Krypton.Toolkit.KryptonButton btnSearch;
        private System.Windows.Forms.CheckBox chkAvoidDuplicateRows;
        private Krypton.Toolkit.KryptonCheckButton chkSelectOrUnselect;
        private System.Windows.Forms.Label lblSelectedDataAction;
    }
}