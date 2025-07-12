namespace StaffSync
{
    partial class frmLeaveTypeList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLeaveTypeList));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtgLeaveTypeList = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCloseMe = new Krypton.Toolkit.KryptonButton();
            this.qryRelationshipListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.staffsyncDBDataSet = new StaffSync.StaffsyncDBDataSet();
            this.countryMasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qryDepartmentListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsDepartmentList = new StaffSync.dsDepartmentList();
            this.empMasInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qryDepartmentListTableAdapter = new StaffSync.dsDepartmentListTableAdapters.qryDepartmentListTableAdapter();
            this.countryMasAddressMasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.countryMasBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.countryMasBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.qryStateListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qryDesignationListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qryCountryListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qryCountryListTableAdapter = new StaffSync.StaffsyncDBDataSetTableAdapters.qryCountryListTableAdapter();
            this.qryDesignationListTableAdapter = new StaffSync.StaffsyncDBDataSetTableAdapters.qryDesignationListTableAdapter();
            this.qryStateListTableAdapter = new StaffSync.StaffsyncDBDataSetTableAdapters.qryStateListTableAdapter();
            this.qryRelationshipListTableAdapter = new StaffSync.StaffsyncDBDataSetTableAdapters.qryRelationshipListTableAdapter();
            this.lblSearchOptionClickedFor = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLeaveTypeList)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qryRelationshipListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryDepartmentListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDepartmentList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasAddressMasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryStateListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryDesignationListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryCountryListBindingSource)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(1227, 583);
            this.splitContainer1.SplitterDistance = 515;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.lblSearchOptionClickedFor);
            this.panel1.Controls.Add(this.dtgLeaveTypeList);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1227, 515);
            this.panel1.TabIndex = 1;
            // 
            // dtgLeaveTypeList
            // 
            this.dtgLeaveTypeList.AllowUserToAddRows = false;
            this.dtgLeaveTypeList.AllowUserToDeleteRows = false;
            this.dtgLeaveTypeList.AllowUserToOrderColumns = true;
            this.dtgLeaveTypeList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.dtgLeaveTypeList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dtgLeaveTypeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgLeaveTypeList.Location = new System.Drawing.Point(18, 78);
            this.dtgLeaveTypeList.Name = "dtgLeaveTypeList";
            this.dtgLeaveTypeList.ReadOnly = true;
            this.dtgLeaveTypeList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgLeaveTypeList.Size = new System.Drawing.Size(1192, 423);
            this.dtgLeaveTypeList.TabIndex = 7;
            this.dtgLeaveTypeList.DoubleClick += new System.EventHandler(this.dtgDepartmentList_DoubleClick);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(157, 24);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtSearch.Multiline = true;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(615, 28);
            this.txtSearch.TabIndex = 4;
            this.txtSearch.WordWrap = false;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Search by State Title";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel2.Controls.Add(this.btnCloseMe);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1227, 64);
            this.panel2.TabIndex = 1;
            // 
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(1084, 14);
            this.btnCloseMe.Name = "btnCloseMe";
            this.btnCloseMe.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCloseMe.Size = new System.Drawing.Size(126, 38);
            this.btnCloseMe.TabIndex = 30;
            this.btnCloseMe.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCloseMe.Values.Image = global::StaffSync.Properties.Resources.close;
            this.btnCloseMe.Values.Text = "Close Me";
            this.btnCloseMe.Click += new System.EventHandler(this.btnCloseMe_Click_2);
            // 
            // qryRelationshipListBindingSource
            // 
            this.qryRelationshipListBindingSource.DataMember = "qryRelationshipList";
            this.qryRelationshipListBindingSource.DataSource = this.staffsyncDBDataSet;
            // 
            // staffsyncDBDataSet
            // 
            this.staffsyncDBDataSet.DataSetName = "StaffsyncDBDataSet";
            this.staffsyncDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // qryDepartmentListBindingSource
            // 
            this.qryDepartmentListBindingSource.DataMember = "qryDepartmentList";
            this.qryDepartmentListBindingSource.DataSource = this.dsDepartmentList;
            // 
            // dsDepartmentList
            // 
            this.dsDepartmentList.DataSetName = "dsDepartmentList";
            this.dsDepartmentList.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // qryDepartmentListTableAdapter
            // 
            this.qryDepartmentListTableAdapter.ClearBeforeFill = true;
            // 
            // qryStateListBindingSource
            // 
            this.qryStateListBindingSource.DataMember = "qryStateList";
            this.qryStateListBindingSource.DataSource = this.staffsyncDBDataSet;
            // 
            // qryDesignationListBindingSource
            // 
            this.qryDesignationListBindingSource.DataMember = "qryDesignationList";
            this.qryDesignationListBindingSource.DataSource = this.staffsyncDBDataSet;
            // 
            // qryCountryListBindingSource
            // 
            this.qryCountryListBindingSource.DataMember = "qryCountryList";
            this.qryCountryListBindingSource.DataSource = this.staffsyncDBDataSet;
            // 
            // qryCountryListTableAdapter
            // 
            this.qryCountryListTableAdapter.ClearBeforeFill = true;
            // 
            // qryDesignationListTableAdapter
            // 
            this.qryDesignationListTableAdapter.ClearBeforeFill = true;
            // 
            // qryStateListTableAdapter
            // 
            this.qryStateListTableAdapter.ClearBeforeFill = true;
            // 
            // qryRelationshipListTableAdapter
            // 
            this.qryRelationshipListTableAdapter.ClearBeforeFill = true;
            // 
            // lblSearchOptionClickedFor
            // 
            this.lblSearchOptionClickedFor.AutoSize = true;
            this.lblSearchOptionClickedFor.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblSearchOptionClickedFor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchOptionClickedFor.Location = new System.Drawing.Point(780, 31);
            this.lblSearchOptionClickedFor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchOptionClickedFor.Name = "lblSearchOptionClickedFor";
            this.lblSearchOptionClickedFor.Size = new System.Drawing.Size(11, 15);
            this.lblSearchOptionClickedFor.TabIndex = 9;
            this.lblSearchOptionClickedFor.Text = " ";
            this.lblSearchOptionClickedFor.Visible = false;
            // 
            // frmLeaveTypeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1227, 583);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLeaveTypeList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Leave Type List";
            this.Load += new System.EventHandler(this.frmLeaveTypeList_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmLeaveTypeList_KeyUp);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgLeaveTypeList)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.qryRelationshipListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryDepartmentListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDepartmentList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasAddressMasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryStateListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryDesignationListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryCountryListBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label1;
        private StaffsyncDBDTSet staffsyncDBDTSet;
        private System.Windows.Forms.BindingSource empMasInfoBindingSource;
        private StaffsyncDBDTSetTableAdapters.EmpMasInfoTableAdapter empMasInfoTableAdapter;
        private System.Windows.Forms.DataGridView dtgLeaveTypeList;
        private dsDepartmentList dsDepartmentList;
        private System.Windows.Forms.BindingSource qryDepartmentListBindingSource;
        private dsDepartmentListTableAdapters.qryDepartmentListTableAdapter qryDepartmentListTableAdapter;
        private System.Windows.Forms.BindingSource countryMasBindingSource;
        private StaffsyncDBDTSetTableAdapters.CountryMasTableAdapter countryMasTableAdapter;
        private System.Windows.Forms.BindingSource countryMasAddressMasBindingSource;
        private StaffsyncDBDTSetTableAdapters.AddressMasTableAdapter addressMasTableAdapter;
        private System.Windows.Forms.BindingSource countryMasBindingSource1;
        private System.Windows.Forms.BindingSource countryMasBindingSource2;
        private StaffsyncDBDataSet staffsyncDBDataSet;
        private System.Windows.Forms.BindingSource qryCountryListBindingSource;
        private StaffsyncDBDataSetTableAdapters.qryCountryListTableAdapter qryCountryListTableAdapter;
        private System.Windows.Forms.BindingSource qryDesignationListBindingSource;
        private StaffsyncDBDataSetTableAdapters.qryDesignationListTableAdapter qryDesignationListTableAdapter;
        private System.Windows.Forms.BindingSource qryStateListBindingSource;
        private StaffsyncDBDataSetTableAdapters.qryStateListTableAdapter qryStateListTableAdapter;
        private System.Windows.Forms.BindingSource qryRelationshipListBindingSource;
        private StaffsyncDBDataSetTableAdapters.qryRelationshipListTableAdapter qryRelationshipListTableAdapter;
        private Krypton.Toolkit.KryptonButton btnCloseMe;
        public System.Windows.Forms.Label lblSearchOptionClickedFor;
    }
}