namespace StaffSync
{
    partial class frmEmpAdvanceTRList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmpAdvanceTRList));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtgAdvanceList = new Krypton.Toolkit.KryptonDataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearchCaption = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblFilterID = new System.Windows.Forms.Label();
            this.lblSearchOptionClickedFor = new System.Windows.Forms.Label();
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
            this.lblEmpID = new System.Windows.Forms.Label();
            this.lblAdvanceID = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAdvanceList)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(828, 386);
            this.splitContainer1.SplitterDistance = 328;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.dtgAdvanceList);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Controls.Add(this.lblSearchCaption);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(828, 328);
            this.panel1.TabIndex = 1;
            // 
            // dtgAdvanceList
            // 
            this.dtgAdvanceList.AllowUserToAddRows = false;
            this.dtgAdvanceList.AllowUserToDeleteRows = false;
            this.dtgAdvanceList.AllowUserToOrderColumns = true;
            this.dtgAdvanceList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dtgAdvanceList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dtgAdvanceList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgAdvanceList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dtgAdvanceList.Location = new System.Drawing.Point(18, 71);
            this.dtgAdvanceList.Name = "dtgAdvanceList";
            this.dtgAdvanceList.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtgAdvanceList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgAdvanceList.Size = new System.Drawing.Size(794, 240);
            this.dtgAdvanceList.TabIndex = 11;
            this.dtgAdvanceList.DoubleClick += new System.EventHandler(this.dtgEmployeeList_DoubleClick);
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
            // lblSearchCaption
            // 
            this.lblSearchCaption.AutoSize = true;
            this.lblSearchCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchCaption.Location = new System.Drawing.Point(15, 31);
            this.lblSearchCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchCaption.Name = "lblSearchCaption";
            this.lblSearchCaption.Size = new System.Drawing.Size(179, 15);
            this.lblSearchCaption.TabIndex = 5;
            this.lblSearchCaption.Text = "Search by Employee Name";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel2.Controls.Add(this.lblAdvanceID);
            this.panel2.Controls.Add(this.lblEmpID);
            this.panel2.Controls.Add(this.lblFilterID);
            this.panel2.Controls.Add(this.lblSearchOptionClickedFor);
            this.panel2.Controls.Add(this.btnCloseMe);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(828, 54);
            this.panel2.TabIndex = 1;
            // 
            // lblFilterID
            // 
            this.lblFilterID.AutoSize = true;
            this.lblFilterID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblFilterID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterID.Location = new System.Drawing.Point(154, 21);
            this.lblFilterID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFilterID.Name = "lblFilterID";
            this.lblFilterID.Size = new System.Drawing.Size(11, 15);
            this.lblFilterID.TabIndex = 22;
            this.lblFilterID.Text = " ";
            this.lblFilterID.Visible = false;
            // 
            // lblSearchOptionClickedFor
            // 
            this.lblSearchOptionClickedFor.AutoSize = true;
            this.lblSearchOptionClickedFor.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblSearchOptionClickedFor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchOptionClickedFor.Location = new System.Drawing.Point(107, 21);
            this.lblSearchOptionClickedFor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchOptionClickedFor.Name = "lblSearchOptionClickedFor";
            this.lblSearchOptionClickedFor.Size = new System.Drawing.Size(11, 15);
            this.lblSearchOptionClickedFor.TabIndex = 9;
            this.lblSearchOptionClickedFor.Text = " ";
            this.lblSearchOptionClickedFor.Visible = false;
            // 
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(686, 8);
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
            // lblEmpID
            // 
            this.lblEmpID.AutoSize = true;
            this.lblEmpID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblEmpID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpID.Location = new System.Drawing.Point(409, 20);
            this.lblEmpID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmpID.Name = "lblEmpID";
            this.lblEmpID.Size = new System.Drawing.Size(11, 15);
            this.lblEmpID.TabIndex = 23;
            this.lblEmpID.Text = " ";
            this.lblEmpID.Visible = false;
            // 
            // lblAdvanceID
            // 
            this.lblAdvanceID.AutoSize = true;
            this.lblAdvanceID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblAdvanceID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdvanceID.Location = new System.Drawing.Point(464, 20);
            this.lblAdvanceID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAdvanceID.Name = "lblAdvanceID";
            this.lblAdvanceID.Size = new System.Drawing.Size(11, 15);
            this.lblAdvanceID.TabIndex = 24;
            this.lblAdvanceID.Text = " ";
            this.lblAdvanceID.Visible = false;
            // 
            // frmEmpAdvanceTRList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(828, 386);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEmpAdvanceTRList";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Advance Type List";
            this.Activated += new System.EventHandler(this.frmEmpAdvanceTRList_Activated);
            this.Load += new System.EventHandler(this.frmEmpAdvanceTRList_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmEmpAdvanceTRList_KeyUp);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgAdvanceList)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        private System.Windows.Forms.Label lblSearchCaption;
        private StaffsyncDBDTSet staffsyncDBDTSet;
        private System.Windows.Forms.BindingSource empMasInfoBindingSource;
        private StaffsyncDBDTSetTableAdapters.EmpMasInfoTableAdapter empMasInfoTableAdapter;
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
        private Krypton.Toolkit.KryptonDataGridView dtgAdvanceList;
        public System.Windows.Forms.Label lblSearchOptionClickedFor;
        public System.Windows.Forms.Label lblFilterID;
        public System.Windows.Forms.Label lblAdvanceID;
        public System.Windows.Forms.Label lblEmpID;
    }
}