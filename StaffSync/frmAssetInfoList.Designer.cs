namespace StaffSync
{
    partial class frmAssetInfoList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssetInfoList));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblClientID = new System.Windows.Forms.Label();
            this.dtgAssetInfoList = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCloseMe = new Krypton.Toolkit.KryptonButton();
            this.qryCountryListBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.staffsyncDBDataSet = new StaffSync.StaffsyncDBDataSet();
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
            this.qryCountryListTableAdapter = new StaffSync.StaffsyncDBDataSetTableAdapters.qryCountryListTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAssetInfoList)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qryCountryListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryDepartmentListBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDepartmentList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasAddressMasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource2)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(1064, 509);
            this.splitContainer1.SplitterDistance = 449;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.lblClientID);
            this.panel1.Controls.Add(this.dtgAssetInfoList);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1064, 449);
            this.panel1.TabIndex = 1;
            // 
            // lblClientID
            // 
            this.lblClientID.AutoSize = true;
            this.lblClientID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblClientID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClientID.Location = new System.Drawing.Point(820, 31);
            this.lblClientID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClientID.Name = "lblClientID";
            this.lblClientID.Size = new System.Drawing.Size(11, 15);
            this.lblClientID.TabIndex = 8;
            this.lblClientID.Text = " ";
            this.lblClientID.Visible = false;
            // 
            // dtgAssetInfoList
            // 
            this.dtgAssetInfoList.AllowUserToAddRows = false;
            this.dtgAssetInfoList.AllowUserToDeleteRows = false;
            this.dtgAssetInfoList.AllowUserToOrderColumns = true;
            this.dtgAssetInfoList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.dtgAssetInfoList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dtgAssetInfoList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgAssetInfoList.Location = new System.Drawing.Point(18, 78);
            this.dtgAssetInfoList.Name = "dtgAssetInfoList";
            this.dtgAssetInfoList.ReadOnly = true;
            this.dtgAssetInfoList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgAssetInfoList.Size = new System.Drawing.Size(1021, 355);
            this.dtgAssetInfoList.TabIndex = 7;
            this.dtgAssetInfoList.DoubleClick += new System.EventHandler(this.dtgDepartmentList_DoubleClick);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(189, 24);
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
            this.label1.Location = new System.Drawing.Point(36, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Search by Asset Name";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel2.Controls.Add(this.btnCloseMe);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1064, 56);
            this.panel2.TabIndex = 1;
            // 
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(913, 9);
            this.btnCloseMe.Name = "btnCloseMe";
            this.btnCloseMe.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCloseMe.Size = new System.Drawing.Size(126, 38);
            this.btnCloseMe.TabIndex = 21;
            this.btnCloseMe.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCloseMe.Values.Image = global::StaffSync.Properties.Resources.close;
            this.btnCloseMe.Values.Text = "Close Me";
            this.btnCloseMe.Click += new System.EventHandler(this.btnCloseMe_Click);
            // 
            // qryCountryListBindingSource
            // 
            this.qryCountryListBindingSource.DataMember = "qryCountryList";
            this.qryCountryListBindingSource.DataSource = this.staffsyncDBDataSet;
            // 
            // staffsyncDBDataSet
            // 
            this.staffsyncDBDataSet.DataSetName = "StaffsyncDBDataSet";
            this.staffsyncDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // countryMasBindingSource
            // 
            this.countryMasBindingSource.DataMember = "CountryMas";
            this.countryMasBindingSource.DataSource = this.staffsyncDBDTSet;
            // 
            // staffsyncDBDTSet
            // 
            this.staffsyncDBDTSet.DataSetName = "StaffsyncDBDTSet";
            this.staffsyncDBDTSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
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
            // qryCountryListTableAdapter
            // 
            this.qryCountryListTableAdapter.ClearBeforeFill = true;
            // 
            // frmAssetInfoList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 509);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAssetInfoList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Assets Listing";
            this.Load += new System.EventHandler(this.frmAssetInfoList_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmAssetInfoList_KeyUp);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAssetInfoList)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.qryCountryListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryDepartmentListBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dsDepartmentList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasAddressMasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.countryMasBindingSource2)).EndInit();
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
        private System.Windows.Forms.DataGridView dtgAssetInfoList;
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
        private Krypton.Toolkit.KryptonButton btnCloseMe;
        private System.Windows.Forms.Label lblClientID;
    }
}