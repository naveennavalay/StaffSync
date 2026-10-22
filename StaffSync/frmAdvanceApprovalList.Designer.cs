namespace StaffSync
{
    partial class frmAdvanceApprovalList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdvanceApprovalList));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.picRefresh2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtgReimbursmentRequestersList = new Krypton.Toolkit.KryptonDataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.picRefresh1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtgAssetsRequestersList = new Krypton.Toolkit.KryptonDataGridView();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.picRefresh = new System.Windows.Forms.PictureBox();
            this.label34 = new System.Windows.Forms.Label();
            this.dtgAdvanceRequestersList = new Krypton.Toolkit.KryptonDataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCloseMe = new Krypton.Toolkit.KryptonButton();
            this.btnSaveDetails = new Krypton.Toolkit.KryptonButton();
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
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRefresh2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgReimbursmentRequestersList)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRefresh1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAssetsRequestersList)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAdvanceRequestersList)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(1167, 871);
            this.splitContainer1.SplitterDistance = 787;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1167, 787);
            this.panel1.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupBox2.Controls.Add(this.picRefresh2);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.dtgReimbursmentRequestersList);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(13, 602);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(1122, 287);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Reimbursment Requesters List";
            // 
            // picRefresh2
            // 
            this.picRefresh2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picRefresh2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picRefresh2.Image = ((System.Drawing.Image)(resources.GetObject("picRefresh2.Image")));
            this.picRefresh2.Location = new System.Drawing.Point(1089, 1);
            this.picRefresh2.Name = "picRefresh2";
            this.picRefresh2.Size = new System.Drawing.Size(23, 22);
            this.picRefresh2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRefresh2.TabIndex = 61;
            this.picRefresh2.TabStop = false;
            this.picRefresh2.Click += new System.EventHandler(this.picRefresh2_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label2.Location = new System.Drawing.Point(5, 264);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(715, 19);
            this.label2.TabIndex = 60;
            this.label2.Text = "🟢 Double-click on the Status column to update the Reimbursment Request status";
            // 
            // dtgReimbursmentRequestersList
            // 
            this.dtgReimbursmentRequestersList.AllowUserToAddRows = false;
            this.dtgReimbursmentRequestersList.AllowUserToDeleteRows = false;
            this.dtgReimbursmentRequestersList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgReimbursmentRequestersList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dtgReimbursmentRequestersList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgReimbursmentRequestersList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dtgReimbursmentRequestersList.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dtgReimbursmentRequestersList.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.ButtonLowProfile;
            this.dtgReimbursmentRequestersList.Location = new System.Drawing.Point(8, 30);
            this.dtgReimbursmentRequestersList.Margin = new System.Windows.Forms.Padding(4);
            this.dtgReimbursmentRequestersList.MultiSelect = false;
            this.dtgReimbursmentRequestersList.Name = "dtgReimbursmentRequestersList";
            this.dtgReimbursmentRequestersList.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtgReimbursmentRequestersList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgReimbursmentRequestersList.Size = new System.Drawing.Size(1104, 230);
            this.dtgReimbursmentRequestersList.TabIndex = 59;
            this.dtgReimbursmentRequestersList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgReimbursmentRequestersList_CellDoubleClick);
            this.dtgReimbursmentRequestersList.Paint += new System.Windows.Forms.PaintEventHandler(this.dtgReimbursmentRequestersList_Paint);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupBox1.Controls.Add(this.picRefresh1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dtgAssetsRequestersList);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 306);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1122, 287);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Assets Requesters List";
            // 
            // picRefresh1
            // 
            this.picRefresh1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picRefresh1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picRefresh1.Image = ((System.Drawing.Image)(resources.GetObject("picRefresh1.Image")));
            this.picRefresh1.Location = new System.Drawing.Point(1089, 1);
            this.picRefresh1.Name = "picRefresh1";
            this.picRefresh1.Size = new System.Drawing.Size(23, 22);
            this.picRefresh1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRefresh1.TabIndex = 61;
            this.picRefresh1.TabStop = false;
            this.picRefresh1.Click += new System.EventHandler(this.picRefresh1_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label1.Location = new System.Drawing.Point(5, 264);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(471, 15);
            this.label1.TabIndex = 60;
            this.label1.Text = "🟢 Double-click on the Status column to update the Asset Request status";
            // 
            // dtgAssetsRequestersList
            // 
            this.dtgAssetsRequestersList.AllowUserToAddRows = false;
            this.dtgAssetsRequestersList.AllowUserToDeleteRows = false;
            this.dtgAssetsRequestersList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgAssetsRequestersList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dtgAssetsRequestersList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgAssetsRequestersList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dtgAssetsRequestersList.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dtgAssetsRequestersList.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.ButtonLowProfile;
            this.dtgAssetsRequestersList.Location = new System.Drawing.Point(8, 30);
            this.dtgAssetsRequestersList.Margin = new System.Windows.Forms.Padding(4);
            this.dtgAssetsRequestersList.MultiSelect = false;
            this.dtgAssetsRequestersList.Name = "dtgAssetsRequestersList";
            this.dtgAssetsRequestersList.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtgAssetsRequestersList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgAssetsRequestersList.Size = new System.Drawing.Size(1104, 230);
            this.dtgAssetsRequestersList.TabIndex = 59;
            this.dtgAssetsRequestersList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgAssetsRequestersList_CellDoubleClick);
            this.dtgAssetsRequestersList.Paint += new System.Windows.Forms.PaintEventHandler(this.dtgAssetsRequestersList_Paint);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupBox5.Controls.Add(this.picRefresh);
            this.groupBox5.Controls.Add(this.label34);
            this.groupBox5.Controls.Add(this.dtgAdvanceRequestersList);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(13, 13);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(1122, 287);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Advance Requesters List";
            // 
            // picRefresh
            // 
            this.picRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picRefresh.Image = ((System.Drawing.Image)(resources.GetObject("picRefresh.Image")));
            this.picRefresh.Location = new System.Drawing.Point(1089, 1);
            this.picRefresh.Name = "picRefresh";
            this.picRefresh.Size = new System.Drawing.Size(23, 22);
            this.picRefresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRefresh.TabIndex = 61;
            this.picRefresh.TabStop = false;
            this.picRefresh.Click += new System.EventHandler(this.picRefresh_Click);
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.Transparent;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label34.Location = new System.Drawing.Point(5, 264);
            this.label34.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(490, 15);
            this.label34.TabIndex = 60;
            this.label34.Text = "🟢 Double-click on the Status column to update the Advance Request status";
            // 
            // dtgAdvanceRequestersList
            // 
            this.dtgAdvanceRequestersList.AllowUserToAddRows = false;
            this.dtgAdvanceRequestersList.AllowUserToDeleteRows = false;
            this.dtgAdvanceRequestersList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgAdvanceRequestersList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dtgAdvanceRequestersList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgAdvanceRequestersList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dtgAdvanceRequestersList.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dtgAdvanceRequestersList.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.ButtonLowProfile;
            this.dtgAdvanceRequestersList.Location = new System.Drawing.Point(8, 30);
            this.dtgAdvanceRequestersList.Margin = new System.Windows.Forms.Padding(4);
            this.dtgAdvanceRequestersList.MultiSelect = false;
            this.dtgAdvanceRequestersList.Name = "dtgAdvanceRequestersList";
            this.dtgAdvanceRequestersList.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtgAdvanceRequestersList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgAdvanceRequestersList.Size = new System.Drawing.Size(1104, 230);
            this.dtgAdvanceRequestersList.TabIndex = 59;
            this.dtgAdvanceRequestersList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtgAdvanceRequestersList_CellDoubleClick);
            this.dtgAdvanceRequestersList.Paint += new System.Windows.Forms.PaintEventHandler(this.dtgAdvanceRequestersList_Paint);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel2.Controls.Add(this.btnCloseMe);
            this.panel2.Controls.Add(this.btnSaveDetails);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1167, 79);
            this.panel2.TabIndex = 1;
            // 
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(999, 8);
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
            // btnSaveDetails
            // 
            this.btnSaveDetails.Enabled = false;
            this.btnSaveDetails.Location = new System.Drawing.Point(21, 8);
            this.btnSaveDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveDetails.Name = "btnSaveDetails";
            this.btnSaveDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSaveDetails.Size = new System.Drawing.Size(126, 38);
            this.btnSaveDetails.TabIndex = 18;
            this.btnSaveDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSaveDetails.Values.Image = global::StaffSync.Properties.Resources.execute;
            this.btnSaveDetails.Values.Text = "Process Status";
            this.btnSaveDetails.Click += new System.EventHandler(this.btnSaveDetails_Click);
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
            // frmAdvanceApprovalList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1167, 871);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAdvanceApprovalList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pending Requesters List";
            this.Activated += new System.EventHandler(this.frmAdvanceApprovalList_Activated);
            this.Load += new System.EventHandler(this.frmAdvanceApprovalList_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAdvanceApprovalList_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picRefresh2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgReimbursmentRequestersList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picRefresh1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAssetsRequestersList)).EndInit();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAdvanceRequestersList)).EndInit();
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
        private Krypton.Toolkit.KryptonButton btnSaveDetails;
        private StaffsyncDBDataSet1 staffsyncDBDataSet1;
        private System.Windows.Forms.BindingSource qryAllEmpLeavePendingStatementBindingSource;
        private StaffsyncDBDataSet1TableAdapters.qryAllEmpLeavePendingStatementTableAdapter qryAllEmpLeavePendingStatementTableAdapter;
        private Krypton.Toolkit.KryptonDataGridView dtgAdvanceRequestersList;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.PictureBox picRefresh;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox picRefresh1;
        private System.Windows.Forms.Label label1;
        private Krypton.Toolkit.KryptonDataGridView dtgAssetsRequestersList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox picRefresh2;
        private System.Windows.Forms.Label label2;
        private Krypton.Toolkit.KryptonDataGridView dtgReimbursmentRequestersList;
    }
}