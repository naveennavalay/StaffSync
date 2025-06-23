namespace StaffSync
{
    partial class frmDesignationList
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtgDesignationList = new System.Windows.Forms.DataGridView();
            this.DesignationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DesignationCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DesignationTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DesignationInitial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.designationIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designationCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designationTitleDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.designationInitialDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isActiveDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.isDeletedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.qryDesignationListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.staffsyncDBDataSet = new StaffSync.StaffsyncDBDataSet();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
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
            this.qryCountryListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qryCountryListTableAdapter = new StaffSync.StaffsyncDBDataSetTableAdapters.qryCountryListTableAdapter();
            this.qryDesignationListTableAdapter = new StaffSync.StaffsyncDBDataSetTableAdapters.qryDesignationListTableAdapter();
            this.btnCloseMe = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDesignationList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryDesignationListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDataSet)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryDepartmentListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDepartmentList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasAddressMasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource2)).BeginInit();
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
            this.panel1.Controls.Add(this.dtgDesignationList);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1227, 515);
            this.panel1.TabIndex = 1;
            // 
            // dtgDesignationList
            // 
            this.dtgDesignationList.AllowUserToAddRows = false;
            this.dtgDesignationList.AllowUserToDeleteRows = false;
            this.dtgDesignationList.AllowUserToOrderColumns = true;
            this.dtgDesignationList.AutoGenerateColumns = false;
            this.dtgDesignationList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.dtgDesignationList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dtgDesignationList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgDesignationList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DesignationID,
            this.DesignationCode,
            this.DesignationTitle,
            this.DesignationInitial,
            this.IsActive,
            this.designationIDDataGridViewTextBoxColumn,
            this.designationCodeDataGridViewTextBoxColumn,
            this.designationTitleDataGridViewTextBoxColumn,
            this.designationInitialDataGridViewTextBoxColumn,
            this.isActiveDataGridViewCheckBoxColumn,
            this.isDeletedDataGridViewCheckBoxColumn});
            this.dtgDesignationList.DataSource = this.qryDesignationListBindingSource;
            this.dtgDesignationList.Location = new System.Drawing.Point(18, 78);
            this.dtgDesignationList.Name = "dtgDesignationList";
            this.dtgDesignationList.ReadOnly = true;
            this.dtgDesignationList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgDesignationList.Size = new System.Drawing.Size(1192, 423);
            this.dtgDesignationList.TabIndex = 7;
            this.dtgDesignationList.DoubleClick += new System.EventHandler(this.dtgDepartmentList_DoubleClick);
            // 
            // DesignationID
            // 
            this.DesignationID.DataPropertyName = "DesignationID";
            this.DesignationID.HeaderText = "DesignationID";
            this.DesignationID.Name = "DesignationID";
            this.DesignationID.ReadOnly = true;
            this.DesignationID.Width = 150;
            // 
            // DesignationCode
            // 
            this.DesignationCode.DataPropertyName = "DesignationCode";
            this.DesignationCode.HeaderText = "Designation Code";
            this.DesignationCode.Name = "DesignationCode";
            this.DesignationCode.ReadOnly = true;
            this.DesignationCode.Width = 150;
            // 
            // DesignationTitle
            // 
            this.DesignationTitle.DataPropertyName = "DesignationTitle";
            this.DesignationTitle.HeaderText = "DesignationTitle";
            this.DesignationTitle.Name = "DesignationTitle";
            this.DesignationTitle.ReadOnly = true;
            this.DesignationTitle.Width = 150;
            // 
            // DesignationInitial
            // 
            this.DesignationInitial.DataPropertyName = "DesignationInitial";
            this.DesignationInitial.HeaderText = "DesignationInitial";
            this.DesignationInitial.Name = "DesignationInitial";
            this.DesignationInitial.ReadOnly = true;
            this.DesignationInitial.Width = 150;
            // 
            // IsActive
            // 
            this.IsActive.DataPropertyName = "IsActive";
            this.IsActive.HeaderText = "IsActive";
            this.IsActive.Name = "IsActive";
            this.IsActive.ReadOnly = true;
            this.IsActive.Width = 150;
            // 
            // designationIDDataGridViewTextBoxColumn
            // 
            this.designationIDDataGridViewTextBoxColumn.DataPropertyName = "DesignationID";
            this.designationIDDataGridViewTextBoxColumn.HeaderText = "DesignationID";
            this.designationIDDataGridViewTextBoxColumn.Name = "designationIDDataGridViewTextBoxColumn";
            this.designationIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // designationCodeDataGridViewTextBoxColumn
            // 
            this.designationCodeDataGridViewTextBoxColumn.DataPropertyName = "DesignationCode";
            this.designationCodeDataGridViewTextBoxColumn.HeaderText = "DesignationCode";
            this.designationCodeDataGridViewTextBoxColumn.Name = "designationCodeDataGridViewTextBoxColumn";
            this.designationCodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // designationTitleDataGridViewTextBoxColumn
            // 
            this.designationTitleDataGridViewTextBoxColumn.DataPropertyName = "DesignationTitle";
            this.designationTitleDataGridViewTextBoxColumn.HeaderText = "DesignationTitle";
            this.designationTitleDataGridViewTextBoxColumn.Name = "designationTitleDataGridViewTextBoxColumn";
            this.designationTitleDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // designationInitialDataGridViewTextBoxColumn
            // 
            this.designationInitialDataGridViewTextBoxColumn.DataPropertyName = "DesignationInitial";
            this.designationInitialDataGridViewTextBoxColumn.HeaderText = "DesignationInitial";
            this.designationInitialDataGridViewTextBoxColumn.Name = "designationInitialDataGridViewTextBoxColumn";
            this.designationInitialDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // isActiveDataGridViewCheckBoxColumn
            // 
            this.isActiveDataGridViewCheckBoxColumn.DataPropertyName = "IsActive";
            this.isActiveDataGridViewCheckBoxColumn.HeaderText = "IsActive";
            this.isActiveDataGridViewCheckBoxColumn.Name = "isActiveDataGridViewCheckBoxColumn";
            this.isActiveDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // isDeletedDataGridViewCheckBoxColumn
            // 
            this.isDeletedDataGridViewCheckBoxColumn.DataPropertyName = "IsDeleted";
            this.isDeletedDataGridViewCheckBoxColumn.HeaderText = "IsDeleted";
            this.isDeletedDataGridViewCheckBoxColumn.Name = "isDeletedDataGridViewCheckBoxColumn";
            this.isDeletedDataGridViewCheckBoxColumn.ReadOnly = true;
            // 
            // qryDesignationListBindingSource
            // 
            this.qryDesignationListBindingSource.DataMember = "qryDesignationList";
            this.qryDesignationListBindingSource.DataSource = this.staffsyncDBDataSet;
            // 
            // staffsyncDBDataSet
            // 
            this.staffsyncDBDataSet.DataSetName = "StaffsyncDBDataSet";
            this.staffsyncDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(197, 24);
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
            this.label1.Size = new System.Drawing.Size(154, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Search by Country Title";
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
            //this.empMasInfoTableAdapter.ClearBeforeFill = true;
            // 
            // qryDepartmentListTableAdapter
            // 
            //this.qryDepartmentListTableAdapter.ClearBeforeFill = true;
            // 
            // countryMasTableAdapter
            // 
            //this.countryMasTableAdapter.ClearBeforeFill = true;
            // 
            // countryMasAddressMasBindingSource
            // 
            this.countryMasAddressMasBindingSource.DataMember = "CountryMasAddressMas";
            this.countryMasAddressMasBindingSource.DataSource = this.countryMasBindingSource;
            // 
            // addressMasTableAdapter
            // 
            //this.addressMasTableAdapter.ClearBeforeFill = true;
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
            // qryCountryListBindingSource
            // 
            this.qryCountryListBindingSource.DataMember = "qryCountryList";
            this.qryCountryListBindingSource.DataSource = this.staffsyncDBDataSet;
            // 
            // qryCountryListTableAdapter
            // 
            //this.qryCountryListTableAdapter.ClearBeforeFill = true;
            // 
            // qryDesignationListTableAdapter
            // 
            //this.qryDesignationListTableAdapter.ClearBeforeFill = true;
            // 
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(1084, 13);
            this.btnCloseMe.Name = "btnCloseMe";
            this.btnCloseMe.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCloseMe.Size = new System.Drawing.Size(126, 38);
            this.btnCloseMe.TabIndex = 21;
            this.btnCloseMe.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCloseMe.Values.Image = global::StaffSync.Properties.Resources.close;
            this.btnCloseMe.Values.Text = "Close Me";
            this.btnCloseMe.Click += new System.EventHandler(this.btnCloseMe_Click);
            // 
            // frmDesignationList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 583);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDesignationList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Countries List";
            this.Load += new System.EventHandler(this.frmDesignationList_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmDesignationList_KeyUp);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgDesignationList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryDesignationListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDataSet)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryDepartmentListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDepartmentList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasAddressMasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource2)).EndInit();
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
        private System.Windows.Forms.DataGridView dtgDesignationList;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn DesignationID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DesignationCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn DesignationTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn DesignationInitial;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn designationIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn designationCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn designationTitleDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn designationInitialDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isActiveDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isDeletedDataGridViewCheckBoxColumn;
        private Krypton.Toolkit.KryptonButton btnCloseMe;
    }
}