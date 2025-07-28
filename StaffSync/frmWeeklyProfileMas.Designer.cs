namespace StaffSync
{
    partial class frmWeeklyProfileMas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWeeklyProfileMas));
            this.errValidator = new System.Windows.Forms.ErrorProvider(this.components);
            this.staffsyncDBDTSet = new StaffSync.StaffsyncDBDTSet();
            this.empMasInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btnCloseMe = new Krypton.Toolkit.KryptonButton();
            this.btnRemoveDetails = new Krypton.Toolkit.KryptonButton();
            this.btnSaveDetails = new Krypton.Toolkit.KryptonButton();
            this.btnModifyDetails = new Krypton.Toolkit.KryptonButton();
            this.btnGenerateDetails = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSearch = new Krypton.Toolkit.KryptonButton();
            this.lblWeeklyOffID = new System.Windows.Forms.Label();
            this.lblActionMode = new System.Windows.Forms.Label();
            this.txtWeeklyCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtEffectiveDate = new System.Windows.Forms.MaskedTextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.cmbIsActive = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtWeeklyOffTitle = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.empMasInfoTableAdapter = new StaffSync.StaffsyncDBDTSetTableAdapters.EmpMasInfoTableAdapter();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // errValidator
            // 
            this.errValidator.ContainerControl = this;
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
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(822, 10);
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
            this.btnRemoveDetails.Location = new System.Drawing.Point(406, 10);
            this.btnRemoveDetails.Name = "btnRemoveDetails";
            this.btnRemoveDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnRemoveDetails.Size = new System.Drawing.Size(126, 38);
            this.btnRemoveDetails.TabIndex = 19;
            this.btnRemoveDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnRemoveDetails.Values.Image = global::StaffSync.Properties.Resources.delete;
            this.btnRemoveDetails.Values.Text = "Delete";
            this.btnRemoveDetails.Click += new System.EventHandler(this.btnRemoveDetails_Click);
            // 
            // btnSaveDetails
            // 
            this.btnSaveDetails.Location = new System.Drawing.Point(274, 10);
            this.btnSaveDetails.Name = "btnSaveDetails";
            this.btnSaveDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSaveDetails.Size = new System.Drawing.Size(126, 38);
            this.btnSaveDetails.TabIndex = 18;
            this.btnSaveDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSaveDetails.Values.Image = global::StaffSync.Properties.Resources.save;
            this.btnSaveDetails.Values.Text = "Save";
            this.btnSaveDetails.Click += new System.EventHandler(this.btnSaveDetails_Click);
            // 
            // btnModifyDetails
            // 
            this.btnModifyDetails.Location = new System.Drawing.Point(142, 10);
            this.btnModifyDetails.Name = "btnModifyDetails";
            this.btnModifyDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnModifyDetails.Size = new System.Drawing.Size(126, 38);
            this.btnModifyDetails.TabIndex = 17;
            this.btnModifyDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnModifyDetails.Values.Image = global::StaffSync.Properties.Resources.update;
            this.btnModifyDetails.Values.Text = "Modify";
            this.btnModifyDetails.Click += new System.EventHandler(this.btnModifyDetails_Click);
            // 
            // btnGenerateDetails
            // 
            this.btnGenerateDetails.Location = new System.Drawing.Point(10, 10);
            this.btnGenerateDetails.Name = "btnGenerateDetails";
            this.btnGenerateDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnGenerateDetails.Size = new System.Drawing.Size(126, 38);
            this.btnGenerateDetails.TabIndex = 16;
            this.btnGenerateDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnGenerateDetails.Values.Image = global::StaffSync.Properties.Resources._new;
            this.btnGenerateDetails.Values.Text = "Generate";
            this.btnGenerateDetails.Click += new System.EventHandler(this.btnGenerateDetails_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(538, 10);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCancel.Size = new System.Drawing.Size(126, 38);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Image = global::StaffSync.Properties.Resources.cancel;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            this.panel2.Size = new System.Drawing.Size(982, 58);
            this.panel2.TabIndex = 1;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(346, 23);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSearch.Size = new System.Drawing.Size(29, 28);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSearch.Values.Image = global::StaffSync.Properties.Resources.search;
            this.btnSearch.Values.Text = "";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblWeeklyOffID
            // 
            this.lblWeeklyOffID.AutoSize = true;
            this.lblWeeklyOffID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblWeeklyOffID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeeklyOffID.Location = new System.Drawing.Point(582, 30);
            this.lblWeeklyOffID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWeeklyOffID.Name = "lblWeeklyOffID";
            this.lblWeeklyOffID.Size = new System.Drawing.Size(11, 15);
            this.lblWeeklyOffID.TabIndex = 5;
            this.lblWeeklyOffID.Text = " ";
            this.lblWeeklyOffID.Visible = false;
            // 
            // lblActionMode
            // 
            this.lblActionMode.AutoSize = true;
            this.lblActionMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActionMode.Location = new System.Drawing.Point(430, 30);
            this.lblActionMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblActionMode.Name = "lblActionMode";
            this.lblActionMode.Size = new System.Drawing.Size(98, 15);
            this.lblActionMode.TabIndex = 4;
            this.lblActionMode.Text = "lblActionMode";
            this.lblActionMode.Visible = false;
            // 
            // txtWeeklyCode
            // 
            this.txtWeeklyCode.Location = new System.Drawing.Point(173, 23);
            this.txtWeeklyCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtWeeklyCode.MaxLength = 255;
            this.txtWeeklyCode.Multiline = true;
            this.txtWeeklyCode.Name = "txtWeeklyCode";
            this.txtWeeklyCode.ReadOnly = true;
            this.txtWeeklyCode.Size = new System.Drawing.Size(168, 28);
            this.txtWeeklyCode.TabIndex = 2;
            this.txtWeeklyCode.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(62, 29);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "Weekly Off Code";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupBox4.Controls.Add(this.btnSearch);
            this.groupBox4.Controls.Add(this.lblWeeklyOffID);
            this.groupBox4.Controls.Add(this.lblActionMode);
            this.groupBox4.Controls.Add(this.txtWeeklyCode);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(10, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(938, 65);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Weekly Off Info";
            // 
            // txtEffectiveDate
            // 
            this.txtEffectiveDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(0)))), ((int)(((byte)(254)))));
            this.txtEffectiveDate.Location = new System.Drawing.Point(176, 64);
            this.txtEffectiveDate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtEffectiveDate.Mask = "##-##-####";
            this.txtEffectiveDate.Name = "txtEffectiveDate";
            this.txtEffectiveDate.Size = new System.Drawing.Size(164, 21);
            this.txtEffectiveDate.TabIndex = 35;
            this.txtEffectiveDate.Tag = "Please enter Employeee Date Of Birth";
            this.txtEffectiveDate.ValidatingType = typeof(System.DateTime);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(75, 66);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(98, 15);
            this.label29.TabIndex = 36;
            this.label29.Text = "Effective From";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbIsActive
            // 
            this.cmbIsActive.FormattingEnabled = true;
            this.cmbIsActive.Location = new System.Drawing.Point(176, 96);
            this.cmbIsActive.MaxDropDownItems = 5;
            this.cmbIsActive.Name = "cmbIsActive";
            this.cmbIsActive.Size = new System.Drawing.Size(423, 23);
            this.cmbIsActive.TabIndex = 34;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(114, 98);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(59, 15);
            this.label17.TabIndex = 33;
            this.label17.Text = "Is Active";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtWeeklyOffTitle
            // 
            this.txtWeeklyOffTitle.Location = new System.Drawing.Point(176, 25);
            this.txtWeeklyOffTitle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtWeeklyOffTitle.MaxLength = 255;
            this.txtWeeklyOffTitle.Multiline = true;
            this.txtWeeklyOffTitle.Name = "txtWeeklyOffTitle";
            this.txtWeeklyOffTitle.Size = new System.Drawing.Size(423, 28);
            this.txtWeeklyOffTitle.TabIndex = 3;
            this.txtWeeklyOffTitle.WordWrap = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(67, 31);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(106, 15);
            this.label16.TabIndex = 7;
            this.label16.Text = "Weekly Off Title";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupBox5.Controls.Add(this.txtEffectiveDate);
            this.groupBox5.Controls.Add(this.label29);
            this.groupBox5.Controls.Add(this.cmbIsActive);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.txtWeeklyOffTitle);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(10, 83);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(938, 170);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Weekly Off Details";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(982, 267);
            this.panel1.TabIndex = 1;
            // 
            // empMasInfoTableAdapter
            // 
            this.empMasInfoTableAdapter.ClearBeforeFill = true;
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
            this.splitContainer1.Size = new System.Drawing.Size(982, 329);
            this.splitContainer1.SplitterDistance = 267;
            this.splitContainer1.TabIndex = 2;
            // 
            // frmWeeklyProfileMas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(982, 329);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWeeklyProfileMas";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Weekly Off Master";
            this.Load += new System.EventHandler(this.frmWeeklyProfileMas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).EndInit();
            this.panel2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errValidator;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.MaskedTextBox txtEffectiveDate;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ComboBox cmbIsActive;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtWeeklyOffTitle;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox4;
        private Krypton.Toolkit.KryptonButton btnSearch;
        public System.Windows.Forms.Label lblActionMode;
        private System.Windows.Forms.TextBox txtWeeklyCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private Krypton.Toolkit.KryptonButton btnCloseMe;
        private Krypton.Toolkit.KryptonButton btnRemoveDetails;
        private Krypton.Toolkit.KryptonButton btnSaveDetails;
        private Krypton.Toolkit.KryptonButton btnModifyDetails;
        private Krypton.Toolkit.KryptonButton btnGenerateDetails;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private StaffsyncDBDTSet staffsyncDBDTSet;
        private System.Windows.Forms.BindingSource empMasInfoBindingSource;
        private StaffsyncDBDTSetTableAdapters.EmpMasInfoTableAdapter empMasInfoTableAdapter;
        public System.Windows.Forms.Label lblWeeklyOffID;
    }
}