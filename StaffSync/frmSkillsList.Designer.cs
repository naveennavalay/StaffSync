namespace StaffSync
{
    partial class frmSkillsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSkillsList));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtgStateList = new System.Windows.Forms.DataGridView();
            this.SkillID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SkillCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SkillTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SkillInitial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.qrySkillsListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.staffsyncDBDataSet = new StaffSync.StaffsyncDBDataSet();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCloseMe = new Krypton.Toolkit.KryptonButton();
            this.qryRelationshipListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.countryMasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.staffsyncDBDTSet = new StaffSync.StaffsyncDBDTSet();
            this.qryDepartmentListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dsDepartmentList = new StaffSync.dsDepartmentList();
            this.empMasInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.empMasInfoTableAdapter = new StaffSync.StaffsyncDBDTSetTableAdapters.EmpMasInfoTableAdapter();
            this.qryDepartmentListTableAdapter = new StaffSync.dsDepartmentListTableAdapters.qryDepartmentListTableAdapter();
            this.countryMasTableAdapter = new StaffSync.StaffsyncDBDTSetTableAdapters.CountryMasTableAdapter();
            this.countryMasAddressMasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.addressMasTableAdapter = new StaffSync.StaffsyncDBDTSetTableAdapters.AddressMasTableAdapter();
            this.countryMasBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.countryMasBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.qryStateListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qryDesignationListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qryCountryListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qryCountryListTableAdapter = new StaffSync.StaffsyncDBDataSetTableAdapters.qryCountryListTableAdapter();
            this.qryDesignationListTableAdapter = new StaffSync.StaffsyncDBDataSetTableAdapters.qryDesignationListTableAdapter();
            this.qryStateListTableAdapter = new StaffSync.StaffsyncDBDataSetTableAdapters.qryStateListTableAdapter();
            this.qryRelationshipListTableAdapter = new StaffSync.StaffsyncDBDataSetTableAdapters.qryRelationshipListTableAdapter();
            this.staffsyncDBDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qrySkillsListTableAdapter = new StaffSync.StaffsyncDBDataSetTableAdapters.qrySkillsListTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgStateList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qrySkillsListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDataSet)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qryRelationshipListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryDepartmentListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDepartmentList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasAddressMasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryStateListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryDesignationListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryCountryListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDataSetBindingSource)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(857, 583);
            this.splitContainer1.SplitterDistance = 515;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.dtgStateList);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(857, 515);
            this.panel1.TabIndex = 1;
            // 
            // dtgStateList
            // 
            this.dtgStateList.AllowUserToAddRows = false;
            this.dtgStateList.AllowUserToDeleteRows = false;
            this.dtgStateList.AllowUserToOrderColumns = true;
            this.dtgStateList.AutoGenerateColumns = false;
            this.dtgStateList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.dtgStateList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dtgStateList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgStateList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SkillID,
            this.SkillCode,
            this.SkillTitle,
            this.SkillInitial,
            this.IsActive});
            this.dtgStateList.DataSource = this.qrySkillsListBindingSource;
            this.dtgStateList.Location = new System.Drawing.Point(18, 78);
            this.dtgStateList.Name = "dtgStateList";
            this.dtgStateList.ReadOnly = true;
            this.dtgStateList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgStateList.Size = new System.Drawing.Size(826, 423);
            this.dtgStateList.TabIndex = 7;
            this.dtgStateList.DoubleClick += new System.EventHandler(this.dtgDepartmentList_DoubleClick);
            // 
            // SkillID
            // 
            this.SkillID.DataPropertyName = "SkillID";
            this.SkillID.HeaderText = "SkillID";
            this.SkillID.Name = "SkillID";
            this.SkillID.ReadOnly = true;
            this.SkillID.Width = 150;
            // 
            // SkillCode
            // 
            this.SkillCode.DataPropertyName = "SkillCode";
            this.SkillCode.HeaderText = "Skill Code";
            this.SkillCode.Name = "SkillCode";
            this.SkillCode.ReadOnly = true;
            this.SkillCode.Width = 150;
            // 
            // SkillTitle
            // 
            this.SkillTitle.DataPropertyName = "SkillTitle";
            this.SkillTitle.HeaderText = "Skill Title";
            this.SkillTitle.Name = "SkillTitle";
            this.SkillTitle.ReadOnly = true;
            this.SkillTitle.Width = 150;
            // 
            // SkillInitial
            // 
            this.SkillInitial.DataPropertyName = "SkillInitial";
            this.SkillInitial.HeaderText = "SkillInitial";
            this.SkillInitial.Name = "SkillInitial";
            this.SkillInitial.ReadOnly = true;
            this.SkillInitial.Width = 150;
            // 
            // IsActive
            // 
            this.IsActive.DataPropertyName = "IsActive";
            this.IsActive.HeaderText = "IsActive";
            this.IsActive.Name = "IsActive";
            this.IsActive.ReadOnly = true;
            this.IsActive.Width = 150;
            // 
            // qrySkillsListBindingSource
            // 
            this.qrySkillsListBindingSource.DataMember = "qrySkillsList";
            this.qrySkillsListBindingSource.DataSource = this.staffsyncDBDataSet;
            // 
            // staffsyncDBDataSet
            // 
            this.staffsyncDBDataSet.DataSetName = "StaffsyncDBDataSet";
            this.staffsyncDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            this.label1.Size = new System.Drawing.Size(141, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Search by Skills Title";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel2.Controls.Add(this.btnCloseMe);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(857, 64);
            this.panel2.TabIndex = 1;
            // 
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(646, 14);
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
            // countryMasBindingSource
            // 
            this.countryMasBindingSource.DataMember = "CountryMas";
            this.countryMasBindingSource.DataSource = this.staffsyncDBDTSet;
            // 
            // staffsyncDBDTSet
            // 
            //this.staffsyncDBDTSet.DataSetName = "StaffsyncDBDTSet";
            //this.staffsyncDBDTSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // empMasInfoBindingSource
            // 
            this.empMasInfoBindingSource.DataMember = "EmpMasInfo";
            this.empMasInfoBindingSource.DataSource = this.staffsyncDBDTSet;
            // 
            // empMasInfoTableAdapter
            // 
            this.empMasInfoTableAdapter.ClearBeforeFill = true;
            // 
            // qryDepartmentListTableAdapter
            // 
            this.qryDepartmentListTableAdapter.ClearBeforeFill = true;
            // 
            // countryMasTableAdapter
            // 
            this.countryMasTableAdapter.ClearBeforeFill = true;
            // 
            // countryMasAddressMasBindingSource
            // 
            this.countryMasAddressMasBindingSource.DataMember = "CountryMasAddressMas";
            this.countryMasAddressMasBindingSource.DataSource = this.countryMasBindingSource;
            // 
            // addressMasTableAdapter
            // 
            this.addressMasTableAdapter.ClearBeforeFill = true;
            // 
            // countryMasBindingSource1
            // 
            this.countryMasBindingSource1.DataMember = "CountryMas";
            this.countryMasBindingSource1.DataSource = this.staffsyncDBDTSet;
            // 
            // countryMasBindingSource2
            // 
            this.countryMasBindingSource2.DataMember = "CountryMas";
            this.countryMasBindingSource2.DataSource = this.staffsyncDBDTSet;
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
            // staffsyncDBDataSetBindingSource
            // 
            this.staffsyncDBDataSetBindingSource.DataSource = this.staffsyncDBDataSet;
            this.staffsyncDBDataSetBindingSource.Position = 0;
            // 
            // qrySkillsListTableAdapter
            // 
            this.qrySkillsListTableAdapter.ClearBeforeFill = true;
            // 
            // frmSkillsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(857, 583);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSkillsList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Skills List";
            this.Load += new System.EventHandler(this.frmSkillsList_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmSkillsList_KeyUp);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgStateList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qrySkillsListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDataSet)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.qryRelationshipListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryDepartmentListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDepartmentList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasAddressMasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryStateListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryDesignationListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryCountryListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDataSetBindingSource)).EndInit();
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
        private System.Windows.Forms.DataGridView dtgStateList;
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
        private System.Windows.Forms.BindingSource staffsyncDBDataSetBindingSource;
        private System.Windows.Forms.BindingSource qrySkillsListBindingSource;
        private StaffsyncDBDataSetTableAdapters.qrySkillsListTableAdapter qrySkillsListTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn SkillID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SkillCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn SkillTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn SkillInitial;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsActive;
        private Krypton.Toolkit.KryptonButton btnCloseMe;
    }
}