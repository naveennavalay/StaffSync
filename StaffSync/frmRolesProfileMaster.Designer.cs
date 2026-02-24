namespace StaffSync
{
    partial class frmRolesProfileMaster
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRolesProfileMaster));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtgRoleProfileManagement = new Krypton.Toolkit.KryptonDataGridView();
            this.lblActionMode = new System.Windows.Forms.Label();
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
            this.qryRoleProfileBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRoleProfileManagement)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryUserSpecificRolesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryRolesDefBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryUserRolesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryRoleProfileBindingSource)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(1199, 428);
            this.splitContainer1.SplitterDistance = 356;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1199, 356);
            this.panel1.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtgRoleProfileManagement);
            this.groupBox1.Controls.Add(this.lblActionMode);
            this.groupBox1.Location = new System.Drawing.Point(16, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1168, 342);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // dtgRoleProfileManagement
            // 
            this.dtgRoleProfileManagement.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgRoleProfileManagement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgRoleProfileManagement.Location = new System.Drawing.Point(13, 23);
            this.dtgRoleProfileManagement.Margin = new System.Windows.Forms.Padding(4);
            this.dtgRoleProfileManagement.Name = "dtgRoleProfileManagement";
            this.dtgRoleProfileManagement.Size = new System.Drawing.Size(1139, 299);
            this.dtgRoleProfileManagement.TabIndex = 33;
            // 
            // lblActionMode
            // 
            this.lblActionMode.AutoSize = true;
            this.lblActionMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActionMode.Location = new System.Drawing.Point(621, 7);
            this.lblActionMode.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblActionMode.Name = "lblActionMode";
            this.lblActionMode.Size = new System.Drawing.Size(98, 15);
            this.lblActionMode.TabIndex = 32;
            this.lblActionMode.Text = "lblActionMode";
            this.lblActionMode.Visible = false;
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
            this.panel2.Size = new System.Drawing.Size(1199, 67);
            this.panel2.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(297, 10);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCancel.Size = new System.Drawing.Size(126, 38);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Image = global::StaffSync.Properties.Resources.cancel;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(1000, 10);
            this.btnCloseMe.Margin = new System.Windows.Forms.Padding(4);
            this.btnCloseMe.Name = "btnCloseMe";
            this.btnCloseMe.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCloseMe.Size = new System.Drawing.Size(126, 38);
            this.btnCloseMe.TabIndex = 33;
            this.btnCloseMe.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCloseMe.Values.Image = global::StaffSync.Properties.Resources.close;
            this.btnCloseMe.Values.Text = "Close Me";
            this.btnCloseMe.Click += new System.EventHandler(this.btnCloseMe_Click);
            // 
            // btnSaveDetails
            // 
            this.btnSaveDetails.Location = new System.Drawing.Point(163, 10);
            this.btnSaveDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveDetails.Name = "btnSaveDetails";
            this.btnSaveDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSaveDetails.Size = new System.Drawing.Size(126, 38);
            this.btnSaveDetails.TabIndex = 32;
            this.btnSaveDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSaveDetails.Values.Image = global::StaffSync.Properties.Resources.save;
            this.btnSaveDetails.Values.Text = "Save";
            this.btnSaveDetails.Click += new System.EventHandler(this.btnSaveDetails_Click);
            // 
            // btnModifyDetails
            // 
            this.btnModifyDetails.Location = new System.Drawing.Point(29, 10);
            this.btnModifyDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnModifyDetails.Name = "btnModifyDetails";
            this.btnModifyDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnModifyDetails.Size = new System.Drawing.Size(126, 38);
            this.btnModifyDetails.TabIndex = 31;
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
            // qryRoleProfileBindingSource
            // 
            this.qryRoleProfileBindingSource.DataMember = "qryRoleProfile";
            this.qryRoleProfileBindingSource.DataSource = this.staffsyncDBDTSet;
            // 
            // frmRolesProfileMaster
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1199, 428);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRolesProfileMaster";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Role Profile Management";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Activated += new System.EventHandler(this.frmRolesProfileMaster_Activated);
            this.Load += new System.EventHandler(this.frmRolesProfileMaster_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmRolesProfileMaster_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRoleProfileManagement)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryUserSpecificRolesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryRolesDefBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryUserRolesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryRoleProfileBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
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
        public System.Windows.Forms.Label lblActionMode;
        private System.Windows.Forms.BindingSource qryRoleProfileBindingSource;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private Krypton.Toolkit.KryptonButton btnCloseMe;
        private Krypton.Toolkit.KryptonButton btnSaveDetails;
        private Krypton.Toolkit.KryptonButton btnModifyDetails;
        private Krypton.Toolkit.KryptonDataGridView dtgRoleProfileManagement;
    }
}