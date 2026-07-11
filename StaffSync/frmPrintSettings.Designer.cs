namespace StaffSync
{
    partial class frmPrintSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrintSettings));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.lblPrintSettingID = new System.Windows.Forms.Label();
            this.lblClientID = new System.Windows.Forms.Label();
            this.txtClientName = new Krypton.Toolkit.KryptonTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtClientCode = new Krypton.Toolkit.KryptonTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.grpEnableDisable = new System.Windows.Forms.GroupBox();
            this.chkPrintWatermark = new System.Windows.Forms.CheckBox();
            this.chkPrintReportFooter = new System.Windows.Forms.CheckBox();
            this.chkPrintReportHeader = new System.Windows.Forms.CheckBox();
            this.chkPrintLogo = new System.Windows.Forms.CheckBox();
            this.chkPrintReportGeneratedOn = new System.Windows.Forms.CheckBox();
            this.chkPrintReportGeneratedBy = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCloseMe = new Krypton.Toolkit.KryptonButton();
            this.btnSaveDetails = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.errValidator = new System.Windows.Forms.ErrorProvider(this.components);
            this.empMasInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.staffsyncDBDTSet = new StaffSync.StaffsyncDBDTSet();
            this.empMasInfoTableAdapter = new StaffSync.StaffsyncDBDTSetTableAdapters.EmpMasInfoTableAdapter();
            this.staffsyncDBDataSet1 = new StaffSync.StaffsyncDBDataSet1();
            this.qryAllEmpLeavePendingStatementBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.qryAllEmpLeavePendingStatementTableAdapter = new StaffSync.StaffsyncDBDataSet1TableAdapters.qryAllEmpLeavePendingStatementTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.grpEnableDisable.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryAllEmpLeavePendingStatementBindingSource)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(805, 568);
            this.splitContainer1.SplitterDistance = 506;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.groupBox5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(805, 506);
            this.panel1.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupBox5.Controls.Add(this.lblPrintSettingID);
            this.groupBox5.Controls.Add(this.lblClientID);
            this.groupBox5.Controls.Add(this.txtClientName);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.txtClientCode);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.grpEnableDisable);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(13, 11);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox5.Size = new System.Drawing.Size(790, 476);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            // 
            // lblPrintSettingID
            // 
            this.lblPrintSettingID.AutoSize = true;
            this.lblPrintSettingID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblPrintSettingID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrintSettingID.Location = new System.Drawing.Point(493, 28);
            this.lblPrintSettingID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrintSettingID.Name = "lblPrintSettingID";
            this.lblPrintSettingID.Size = new System.Drawing.Size(11, 15);
            this.lblPrintSettingID.TabIndex = 31;
            this.lblPrintSettingID.Text = " ";
            // 
            // lblClientID
            // 
            this.lblClientID.AutoSize = true;
            this.lblClientID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblClientID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClientID.Location = new System.Drawing.Point(441, 28);
            this.lblClientID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClientID.Name = "lblClientID";
            this.lblClientID.Size = new System.Drawing.Size(11, 15);
            this.lblClientID.TabIndex = 30;
            this.lblClientID.Text = " ";
            // 
            // txtClientName
            // 
            this.txtClientName.Location = new System.Drawing.Point(127, 55);
            this.txtClientName.Multiline = true;
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtClientName.Size = new System.Drawing.Size(414, 21);
            this.txtClientName.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClientName.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtClientName.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClientName.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClientName.TabIndex = 24;
            this.txtClientName.Tag = "Name";
            this.txtClientName.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 15);
            this.label1.TabIndex = 23;
            this.label1.Text = "Client Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtClientCode
            // 
            this.txtClientCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtClientCode.Location = new System.Drawing.Point(127, 25);
            this.txtClientCode.Name = "txtClientCode";
            this.txtClientCode.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtClientCode.Size = new System.Drawing.Size(213, 21);
            this.txtClientCode.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClientCode.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtClientCode.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClientCode.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClientCode.TabIndex = 22;
            this.txtClientCode.Tag = "Name";
            this.txtClientCode.WordWrap = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(45, 28);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(81, 15);
            this.label19.TabIndex = 21;
            this.label19.Text = "Client Code";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpEnableDisable
            // 
            this.grpEnableDisable.Controls.Add(this.chkPrintWatermark);
            this.grpEnableDisable.Controls.Add(this.chkPrintReportFooter);
            this.grpEnableDisable.Controls.Add(this.chkPrintReportHeader);
            this.grpEnableDisable.Controls.Add(this.chkPrintLogo);
            this.grpEnableDisable.Controls.Add(this.chkPrintReportGeneratedOn);
            this.grpEnableDisable.Controls.Add(this.chkPrintReportGeneratedBy);
            this.grpEnableDisable.Location = new System.Drawing.Point(18, 82);
            this.grpEnableDisable.Name = "grpEnableDisable";
            this.grpEnableDisable.Size = new System.Drawing.Size(760, 314);
            this.grpEnableDisable.TabIndex = 28;
            this.grpEnableDisable.TabStop = false;
            // 
            // chkPrintWatermark
            // 
            this.chkPrintWatermark.AutoSize = true;
            this.chkPrintWatermark.Checked = true;
            this.chkPrintWatermark.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrintWatermark.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkPrintWatermark.Location = new System.Drawing.Point(30, 122);
            this.chkPrintWatermark.Name = "chkPrintWatermark";
            this.chkPrintWatermark.Size = new System.Drawing.Size(129, 19);
            this.chkPrintWatermark.TabIndex = 60;
            this.chkPrintWatermark.Tag = "Print Watermark";
            this.chkPrintWatermark.Text = "Print Watermark";
            this.chkPrintWatermark.UseVisualStyleBackColor = true;
            // 
            // chkPrintReportFooter
            // 
            this.chkPrintReportFooter.AutoSize = true;
            this.chkPrintReportFooter.Checked = true;
            this.chkPrintReportFooter.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrintReportFooter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkPrintReportFooter.Location = new System.Drawing.Point(30, 97);
            this.chkPrintReportFooter.Name = "chkPrintReportFooter";
            this.chkPrintReportFooter.Size = new System.Drawing.Size(148, 19);
            this.chkPrintReportFooter.TabIndex = 59;
            this.chkPrintReportFooter.Tag = "Print Report Footer";
            this.chkPrintReportFooter.Text = "Print Report Footer";
            this.chkPrintReportFooter.UseVisualStyleBackColor = true;
            // 
            // chkPrintReportHeader
            // 
            this.chkPrintReportHeader.AutoSize = true;
            this.chkPrintReportHeader.Checked = true;
            this.chkPrintReportHeader.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrintReportHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkPrintReportHeader.Location = new System.Drawing.Point(30, 72);
            this.chkPrintReportHeader.Name = "chkPrintReportHeader";
            this.chkPrintReportHeader.Size = new System.Drawing.Size(154, 19);
            this.chkPrintReportHeader.TabIndex = 58;
            this.chkPrintReportHeader.Tag = "Print Report Header";
            this.chkPrintReportHeader.Text = "Print Report Header";
            this.chkPrintReportHeader.UseVisualStyleBackColor = true;
            // 
            // chkPrintLogo
            // 
            this.chkPrintLogo.AutoSize = true;
            this.chkPrintLogo.Checked = true;
            this.chkPrintLogo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrintLogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkPrintLogo.Location = new System.Drawing.Point(452, 20);
            this.chkPrintLogo.Name = "chkPrintLogo";
            this.chkPrintLogo.Size = new System.Drawing.Size(92, 19);
            this.chkPrintLogo.TabIndex = 57;
            this.chkPrintLogo.Tag = "Print Logo";
            this.chkPrintLogo.Text = "Print Logo";
            this.chkPrintLogo.UseVisualStyleBackColor = true;
            // 
            // chkPrintReportGeneratedOn
            // 
            this.chkPrintReportGeneratedOn.AutoSize = true;
            this.chkPrintReportGeneratedOn.Checked = true;
            this.chkPrintReportGeneratedOn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrintReportGeneratedOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkPrintReportGeneratedOn.Location = new System.Drawing.Point(228, 20);
            this.chkPrintReportGeneratedOn.Name = "chkPrintReportGeneratedOn";
            this.chkPrintReportGeneratedOn.Size = new System.Drawing.Size(149, 19);
            this.chkPrintReportGeneratedOn.TabIndex = 56;
            this.chkPrintReportGeneratedOn.Tag = "Print Generated On";
            this.chkPrintReportGeneratedOn.Text = "Print Generated On";
            this.chkPrintReportGeneratedOn.UseVisualStyleBackColor = true;
            // 
            // chkPrintReportGeneratedBy
            // 
            this.chkPrintReportGeneratedBy.AutoSize = true;
            this.chkPrintReportGeneratedBy.Checked = true;
            this.chkPrintReportGeneratedBy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPrintReportGeneratedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.chkPrintReportGeneratedBy.Location = new System.Drawing.Point(30, 20);
            this.chkPrintReportGeneratedBy.Name = "chkPrintReportGeneratedBy";
            this.chkPrintReportGeneratedBy.Size = new System.Drawing.Size(146, 19);
            this.chkPrintReportGeneratedBy.TabIndex = 53;
            this.chkPrintReportGeneratedBy.Tag = "Print Generated By";
            this.chkPrintReportGeneratedBy.Text = "Print Generated By";
            this.chkPrintReportGeneratedBy.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel2.Controls.Add(this.btnCloseMe);
            this.panel2.Controls.Add(this.btnSaveDetails);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(805, 57);
            this.panel2.TabIndex = 1;
            // 
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(659, 8);
            this.btnCloseMe.Name = "btnCloseMe";
            this.btnCloseMe.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCloseMe.Size = new System.Drawing.Size(126, 38);
            this.btnCloseMe.TabIndex = 17;
            this.btnCloseMe.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCloseMe.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnCloseMe.Values.Image")));
            this.btnCloseMe.Values.Text = "Close Me";
            this.btnCloseMe.Click += new System.EventHandler(this.btnCloseMe_Click);
            // 
            // btnSaveDetails
            // 
            this.btnSaveDetails.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSaveDetails.Location = new System.Drawing.Point(13, 8);
            this.btnSaveDetails.Name = "btnSaveDetails";
            this.btnSaveDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSaveDetails.Size = new System.Drawing.Size(126, 38);
            this.btnSaveDetails.TabIndex = 16;
            this.btnSaveDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSaveDetails.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveDetails.Values.Image")));
            this.btnSaveDetails.Values.Text = "Save";
            this.btnSaveDetails.Click += new System.EventHandler(this.btnSaveDetails_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(148, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCancel.Size = new System.Drawing.Size(126, 38);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Values.Image")));
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errValidator
            // 
            this.errValidator.ContainerControl = this;
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
            // frmPrintSettings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(805, 568);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrintSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Organisation Report Settings";
            this.Activated += new System.EventHandler(this.frmPrintSettings_Activated);
            this.Load += new System.EventHandler(this.frmPrintSettings_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPrintSettings_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.grpEnableDisable.ResumeLayout(false);
            this.grpEnableDisable.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.qryAllEmpLeavePendingStatementBindingSource)).EndInit();
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
        private StaffsyncDBDataSet1 staffsyncDBDataSet1;
        private System.Windows.Forms.BindingSource qryAllEmpLeavePendingStatementBindingSource;
        private StaffsyncDBDataSet1TableAdapters.qryAllEmpLeavePendingStatementTableAdapter qryAllEmpLeavePendingStatementTableAdapter;
        private Krypton.Toolkit.KryptonTextBox txtClientCode;
        private System.Windows.Forms.Label label19;
        private Krypton.Toolkit.KryptonTextBox txtClientName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpEnableDisable;
        private System.Windows.Forms.CheckBox chkPrintReportGeneratedBy;
        private Krypton.Toolkit.KryptonButton btnCloseMe;
        private Krypton.Toolkit.KryptonButton btnSaveDetails;
        private Krypton.Toolkit.KryptonButton btnCancel;
        public System.Windows.Forms.Label lblPrintSettingID;
        public System.Windows.Forms.Label lblClientID;
        private System.Windows.Forms.CheckBox chkPrintReportGeneratedOn;
        private System.Windows.Forms.CheckBox chkPrintReportFooter;
        private System.Windows.Forms.CheckBox chkPrintReportHeader;
        private System.Windows.Forms.CheckBox chkPrintLogo;
        private System.Windows.Forms.CheckBox chkPrintWatermark;
    }
}