namespace StaffSync
{
    partial class frmIndEmpAttendanceCalender
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIndEmpAttendanceCalender));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblEmpID = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCloseMe = new Krypton.Toolkit.KryptonButton();
            this.empMasInfoBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.staffsyncDBDTSet = new StaffSync.StaffsyncDBDTSet();
            this.empMasInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.empMasInfoTableAdapter = new StaffSync.StaffsyncDBDTSetTableAdapters.EmpMasInfoTableAdapter();
            this.empMasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.empMasTableAdapter = new StaffSync.StaffsyncDBDTSetTableAdapters.EmpMasTableAdapter();
            this.errValidator = new System.Windows.Forms.ErrorProvider(this.components);
            this.empAttCalender = new StaffSync.Calender();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasBindingSource)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(1118, 440);
            this.splitContainer1.SplitterDistance = 376;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.lblEmpID);
            this.panel1.Controls.Add(this.empAttCalender);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1118, 376);
            this.panel1.TabIndex = 1;
            // 
            // lblEmpID
            // 
            this.lblEmpID.AutoSize = true;
            this.lblEmpID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblEmpID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpID.Location = new System.Drawing.Point(593, 65);
            this.lblEmpID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblEmpID.Name = "lblEmpID";
            this.lblEmpID.Size = new System.Drawing.Size(11, 15);
            this.lblEmpID.TabIndex = 20;
            this.lblEmpID.Text = " ";
            this.lblEmpID.Visible = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel2.Controls.Add(this.btnCloseMe);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1118, 60);
            this.panel2.TabIndex = 1;
            // 
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(980, 11);
            this.btnCloseMe.Margin = new System.Windows.Forms.Padding(4);
            this.btnCloseMe.Name = "btnCloseMe";
            this.btnCloseMe.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCloseMe.Size = new System.Drawing.Size(126, 38);
            this.btnCloseMe.TabIndex = 11;
            this.btnCloseMe.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCloseMe.Values.Image = global::StaffSync.Properties.Resources.close;
            this.btnCloseMe.Values.Text = "Close Me";
            this.btnCloseMe.Click += new System.EventHandler(this.btnCloseMe_Click);
            // 
            // empMasInfoBindingSource1
            // 
            this.empMasInfoBindingSource1.DataMember = "EmpMasInfo";
            this.empMasInfoBindingSource1.DataSource = this.staffsyncDBDTSet;
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
            // empMasInfoTableAdapter
            // 
            this.empMasInfoTableAdapter.ClearBeforeFill = true;
            // 
            // empMasBindingSource
            // 
            this.empMasBindingSource.DataMember = "EmpMas";
            this.empMasBindingSource.DataSource = this.staffsyncDBDTSet;
            // 
            // empMasTableAdapter
            // 
            this.empMasTableAdapter.ClearBeforeFill = true;
            // 
            // errValidator
            // 
            this.errValidator.ContainerControl = this;
            // 
            // empAttCalender
            // 
            this.empAttCalender.AllowFutureDates = false;
            this.empAttCalender.AllowMultiSelect = false;
            this.empAttCalender.AllowPreviousDates = false;
            this.empAttCalender.CustomTextColor = System.Drawing.Color.Black;
            this.empAttCalender.CustomTextFontBold = true;
            this.empAttCalender.CustomTextFontFamily = "Segoe UI";
            this.empAttCalender.CustomTextFontItalic = false;
            this.empAttCalender.CustomTextFontSize = 12F;
            this.empAttCalender.DateFormat = "MMMM yyyy";
            this.empAttCalender.DayHeaderBackColor = System.Drawing.SystemColors.ControlText;
            this.empAttCalender.DayHeaderFontBold = true;
            this.empAttCalender.DayHeaderFontItalic = false;
            this.empAttCalender.DayHeaderFontSize = 12F;
            this.empAttCalender.DayHeaderForeColor = System.Drawing.Color.LimeGreen;
            this.empAttCalender.DayNameFormat = StaffSync.DayNameFormat.Short;
            this.empAttCalender.DayNumberAlignment = StaffSync.DateAlignment.TopLeft;
            this.empAttCalender.DayNumberColor = System.Drawing.Color.Black;
            this.empAttCalender.DayNumberFontBold = true;
            this.empAttCalender.DayNumberFontFamily = "Segoe UI";
            this.empAttCalender.DayNumberFontItalic = false;
            this.empAttCalender.DayNumberFontSize = 12F;
            this.empAttCalender.DisplayMonth = new System.DateTime(2025, 11, 12, 0, 0, 0, 0);
            this.empAttCalender.HeaderBackColor = System.Drawing.SystemColors.ControlText;
            this.empAttCalender.HeaderFontBold = true;
            this.empAttCalender.HeaderFontItalic = false;
            this.empAttCalender.HeaderFontSize = 12F;
            this.empAttCalender.HeaderForeColor = System.Drawing.Color.LimeGreen;
            this.empAttCalender.LegendPosition = StaffSync.LegendAlignment.BottomRight;
            this.empAttCalender.Location = new System.Drawing.Point(12, 7);
            this.empAttCalender.Name = "empAttCalender";
            this.empAttCalender.SaturdaySundayTogether = false;
            this.empAttCalender.ShowGridLines = true;
            this.empAttCalender.ShowHeader = true;
            this.empAttCalender.ShowLegend = true;
            this.empAttCalender.ShowToolTips = true;
            this.empAttCalender.ShowWeekends = true;
            this.empAttCalender.ShowWeeklyOff = true;
            this.empAttCalender.Size = new System.Drawing.Size(1094, 361);
            this.empAttCalender.TabIndex = 60;
            this.empAttCalender.Text = "calender1";
            this.empAttCalender.WeekendColor = System.Drawing.Color.LightGray;
            this.empAttCalender.WeeklyOffColor = System.Drawing.Color.DarkGray;
            this.empAttCalender.WeeklyOffDay = null;
            // 
            // frmIndEmpAttendanceCalender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1118, 440);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmIndEmpAttendanceCalender";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Attendance Calendar";
            this.Load += new System.EventHandler(this.frmIndEmpAttendanceCalender_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmIndEmpAttendanceCalender_KeyUp);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.staffsyncDBDTSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasInfoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empMasBindingSource)).EndInit();
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
        private System.Windows.Forms.BindingSource empMasBindingSource;
        private StaffsyncDBDTSetTableAdapters.EmpMasTableAdapter empMasTableAdapter;
        private System.Windows.Forms.BindingSource empMasInfoBindingSource1;
        private System.Windows.Forms.ErrorProvider errValidator;
        private Calender empAttCalender;
        private Krypton.Toolkit.KryptonButton btnCloseMe;
        private System.Windows.Forms.Label lblEmpID;
    }
}