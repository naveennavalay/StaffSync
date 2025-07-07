namespace StaffSync
{
    partial class frmLastCompanySelection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLastCompanySelection));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblEmpID = new System.Windows.Forms.Label();
            this.txtMoreDetails = new Krypton.Toolkit.KryptonTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtEndDate = new System.Windows.Forms.MaskedTextBox();
            this.txtStartDate = new System.Windows.Forms.MaskedTextBox();
            this.dtgPreviousWorkExp = new Krypton.Toolkit.KryptonDataGridView();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnOK = new Krypton.Toolkit.KryptonButton();
            this.btnCloseMe = new Krypton.Toolkit.KryptonButton();
            this.empMasInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.staffsyncDBDTSet = new StaffSync.StaffsyncDBDTSet();
            this.empMasInfoTableAdapter = new StaffSync.StaffsyncDBDTSetTableAdapters.EmpMasInfoTableAdapter();
            this.errValidator = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPreviousWorkExp)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(953, 485);
            this.splitContainer1.SplitterDistance = 419;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(953, 419);
            this.panel1.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.lblEmpID);
            this.groupBox5.Controls.Add(this.txtMoreDetails);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.txtEndDate);
            this.groupBox5.Controls.Add(this.txtStartDate);
            this.groupBox5.Controls.Add(this.dtgPreviousWorkExp);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(14, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(926, 400);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Last Company Info";
            // 
            // lblEmpID
            // 
            this.lblEmpID.AutoSize = true;
            this.lblEmpID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblEmpID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpID.Location = new System.Drawing.Point(506, 230);
            this.lblEmpID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEmpID.Name = "lblEmpID";
            this.lblEmpID.Size = new System.Drawing.Size(11, 15);
            this.lblEmpID.TabIndex = 8;
            this.lblEmpID.Text = " ";
            this.lblEmpID.Visible = false;
            // 
            // txtMoreDetails
            // 
            this.txtMoreDetails.Location = new System.Drawing.Point(110, 258);
            this.txtMoreDetails.Multiline = true;
            this.txtMoreDetails.Name = "txtMoreDetails";
            this.txtMoreDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtMoreDetails.Size = new System.Drawing.Size(478, 129);
            this.txtMoreDetails.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMoreDetails.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtMoreDetails.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMoreDetails.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMoreDetails.TabIndex = 49;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(14, 263);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(89, 15);
            this.label19.TabIndex = 48;
            this.label19.Text = "More Details";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtEndDate
            // 
            this.txtEndDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(0)))), ((int)(((byte)(254)))));
            this.txtEndDate.Location = new System.Drawing.Point(342, 224);
            this.txtEndDate.Mask = "##-##-####";
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.Size = new System.Drawing.Size(100, 21);
            this.txtEndDate.TabIndex = 47;
            this.txtEndDate.Tag = "Please enter Employeee Date Of Joining";
            // 
            // txtStartDate
            // 
            this.txtStartDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(0)))), ((int)(((byte)(254)))));
            this.txtStartDate.Location = new System.Drawing.Point(110, 224);
            this.txtStartDate.Mask = "##-##-####";
            this.txtStartDate.Name = "txtStartDate";
            this.txtStartDate.Size = new System.Drawing.Size(100, 21);
            this.txtStartDate.TabIndex = 46;
            this.txtStartDate.Tag = "Please enter Employeee Date Of Birth";
            this.txtStartDate.ValidatingType = typeof(System.DateTime);
            this.txtStartDate.TextChanged += new System.EventHandler(this.txtStartDate_TextChanged);
            // 
            // dtgPreviousWorkExp
            // 
            this.dtgPreviousWorkExp.AllowUserToAddRows = false;
            this.dtgPreviousWorkExp.AllowUserToDeleteRows = false;
            this.dtgPreviousWorkExp.AllowUserToResizeRows = false;
            this.dtgPreviousWorkExp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dtgPreviousWorkExp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgPreviousWorkExp.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dtgPreviousWorkExp.GridStyles.Style = Krypton.Toolkit.DataGridViewStyle.Mixed;
            this.dtgPreviousWorkExp.GridStyles.StyleBackground = Krypton.Toolkit.PaletteBackStyle.ContextMenuItemImage;
            this.dtgPreviousWorkExp.Location = new System.Drawing.Point(15, 23);
            this.dtgPreviousWorkExp.Name = "dtgPreviousWorkExp";
            this.dtgPreviousWorkExp.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.dtgPreviousWorkExp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgPreviousWorkExp.Size = new System.Drawing.Size(884, 194);
            this.dtgPreviousWorkExp.TabIndex = 45;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(269, 227);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(66, 15);
            this.label18.TabIndex = 21;
            this.label18.Text = "End Date";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(32, 227);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(71, 15);
            this.label16.TabIndex = 7;
            this.label16.Text = "Start Date";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel2.Controls.Add(this.btnOK);
            this.panel2.Controls.Add(this.btnCloseMe);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(953, 62);
            this.panel2.TabIndex = 1;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(36, 14);
            this.btnOK.Name = "btnOK";
            this.btnOK.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnOK.Size = new System.Drawing.Size(126, 38);
            this.btnOK.TabIndex = 21;
            this.btnOK.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnOK.Values.Image = global::StaffSync.Properties.Resources.update;
            this.btnOK.Values.Text = "Accept";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(794, 14);
            this.btnCloseMe.Name = "btnCloseMe";
            this.btnCloseMe.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCloseMe.Size = new System.Drawing.Size(126, 38);
            this.btnCloseMe.TabIndex = 20;
            this.btnCloseMe.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCloseMe.Values.Image = global::StaffSync.Properties.Resources.close;
            this.btnCloseMe.Values.Text = "Close Me";
            this.btnCloseMe.Click += new System.EventHandler(this.btnCloseMe_Click);
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
            // errValidator
            // 
            this.errValidator.ContainerControl = this;
            // 
            // frmLastCompanySelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(953, 485);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLastCompanySelection";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Last Company Info";
            this.Load += new System.EventHandler(this.frmLastCompanySelection_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgPreviousWorkExp)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).EndInit();
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
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ErrorProvider errValidator;
        private Krypton.Toolkit.KryptonButton btnCloseMe;
        private Krypton.Toolkit.KryptonDataGridView dtgPreviousWorkExp;
        private System.Windows.Forms.MaskedTextBox txtEndDate;
        private System.Windows.Forms.MaskedTextBox txtStartDate;
        private Krypton.Toolkit.KryptonTextBox txtMoreDetails;
        private System.Windows.Forms.Label label19;
        private Krypton.Toolkit.KryptonButton btnOK;
        public System.Windows.Forms.Label lblEmpID;
    }
}