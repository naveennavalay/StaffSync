namespace StaffSync
{
    partial class frmAttendanceMater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAttendanceMater));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.picViewLeaves = new Krypton.Toolkit.KryptonButton();
            this.empAttCalender = new StaffSync.Calender();
            this.cmbMonthNameList = new Krypton.Toolkit.KryptonComboBox();
            this.label50 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lblLeaveMasID = new System.Windows.Forms.Label();
            this.btnReportingManagerSearch = new Krypton.Toolkit.KryptonButton();
            this.txtRepEmpContactNumber = new Krypton.Toolkit.KryptonTextBox();
            this.txtRepEmpDepartment = new Krypton.Toolkit.KryptonTextBox();
            this.txtRepEmpDesig = new Krypton.Toolkit.KryptonTextBox();
            this.txtRepEmpName = new Krypton.Toolkit.KryptonTextBox();
            this.txtRepEmpCode = new Krypton.Toolkit.KryptonTextBox();
            this.lblActionMode = new System.Windows.Forms.Label();
            this.lblReportingManagerID = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.picRepEmpPhoto = new System.Windows.Forms.PictureBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.btnSaveDetails = new Krypton.Toolkit.KryptonButton();
            this.btnModifyDetails = new Krypton.Toolkit.KryptonButton();
            this.btnCloseMe = new Krypton.Toolkit.KryptonButton();
            this.errValidator = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonthNameList)).BeginInit();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRepEmpPhoto)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).BeginInit();
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
            this.splitContainer1.Size = new System.Drawing.Size(1231, 615);
            this.splitContainer1.SplitterDistance = 554;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.picViewLeaves);
            this.panel1.Controls.Add(this.empAttCalender);
            this.panel1.Controls.Add(this.cmbMonthNameList);
            this.panel1.Controls.Add(this.label50);
            this.panel1.Controls.Add(this.groupBox8);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1231, 554);
            this.panel1.TabIndex = 1;
            // 
            // picViewLeaves
            // 
            this.picViewLeaves.Location = new System.Drawing.Point(317, 186);
            this.picViewLeaves.Margin = new System.Windows.Forms.Padding(4);
            this.picViewLeaves.Name = "picViewLeaves";
            this.picViewLeaves.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.picViewLeaves.Size = new System.Drawing.Size(32, 28);
            this.picViewLeaves.TabIndex = 60;
            this.picViewLeaves.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.picViewLeaves.Values.Image = global::StaffSync.Properties.Resources.search;
            this.picViewLeaves.Values.Text = "";
            this.picViewLeaves.Click += new System.EventHandler(this.picViewLeaves_Click);
            // 
            // empAttCalender
            // 
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
            this.empAttCalender.DisplayMonth = new System.DateTime(2025, 8, 9, 0, 0, 0, 0);
            this.empAttCalender.HeaderBackColor = System.Drawing.SystemColors.ControlText;
            this.empAttCalender.HeaderFontBold = true;
            this.empAttCalender.HeaderFontItalic = false;
            this.empAttCalender.HeaderFontSize = 12F;
            this.empAttCalender.HeaderForeColor = System.Drawing.Color.LimeGreen;
            this.empAttCalender.LegendPosition = StaffSync.LegendAlignment.BottomRight;
            this.empAttCalender.Location = new System.Drawing.Point(33, 227);
            this.empAttCalender.Name = "empAttCalender";
            this.empAttCalender.SaturdaySundayTogether = false;
            this.empAttCalender.ShowGridLines = true;
            this.empAttCalender.ShowHeader = true;
            this.empAttCalender.ShowLegend = true;
            this.empAttCalender.ShowToolTips = true;
            this.empAttCalender.ShowWeekends = true;
            this.empAttCalender.ShowWeeklyOff = true;
            this.empAttCalender.Size = new System.Drawing.Size(1150, 325);
            this.empAttCalender.TabIndex = 59;
            this.empAttCalender.Text = "calender1";
            this.empAttCalender.WeekendColor = System.Drawing.Color.LightGray;
            this.empAttCalender.WeeklyOffColor = System.Drawing.Color.DarkGray;
            this.empAttCalender.WeeklyOffDay = null;
            this.empAttCalender.DetailedDayClicked += new System.EventHandler<StaffSync.DetailedDateClickedEventArgs>(this.empAttCalender_DetailedDayClicked);
            // 
            // cmbMonthNameList
            // 
            this.cmbMonthNameList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonthNameList.DropDownWidth = 440;
            this.cmbMonthNameList.Location = new System.Drawing.Point(162, 189);
            this.cmbMonthNameList.Name = "cmbMonthNameList";
            this.cmbMonthNameList.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbMonthNameList.Size = new System.Drawing.Size(148, 22);
            this.cmbMonthNameList.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbMonthNameList.TabIndex = 58;
            this.cmbMonthNameList.SelectedIndexChanged += new System.EventHandler(this.cmbMonthNameList_SelectedIndexChanged);
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label50.Location = new System.Drawing.Point(112, 192);
            this.label50.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(47, 15);
            this.label50.TabIndex = 57;
            this.label50.Text = "Month";
            this.label50.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox8
            // 
            this.groupBox8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupBox8.Controls.Add(this.lblLeaveMasID);
            this.groupBox8.Controls.Add(this.btnReportingManagerSearch);
            this.groupBox8.Controls.Add(this.txtRepEmpContactNumber);
            this.groupBox8.Controls.Add(this.txtRepEmpDepartment);
            this.groupBox8.Controls.Add(this.txtRepEmpDesig);
            this.groupBox8.Controls.Add(this.txtRepEmpName);
            this.groupBox8.Controls.Add(this.txtRepEmpCode);
            this.groupBox8.Controls.Add(this.lblActionMode);
            this.groupBox8.Controls.Add(this.lblReportingManagerID);
            this.groupBox8.Controls.Add(this.label20);
            this.groupBox8.Controls.Add(this.label38);
            this.groupBox8.Controls.Add(this.picRepEmpPhoto);
            this.groupBox8.Controls.Add(this.label37);
            this.groupBox8.Controls.Add(this.label36);
            this.groupBox8.Controls.Add(this.label35);
            this.groupBox8.Controls.Add(this.label34);
            this.groupBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox8.Location = new System.Drawing.Point(16, 4);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox8.Size = new System.Drawing.Size(1199, 174);
            this.groupBox8.TabIndex = 32;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "User Information";
            // 
            // lblLeaveMasID
            // 
            this.lblLeaveMasID.AutoSize = true;
            this.lblLeaveMasID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblLeaveMasID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLeaveMasID.Location = new System.Drawing.Point(616, 33);
            this.lblLeaveMasID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLeaveMasID.Name = "lblLeaveMasID";
            this.lblLeaveMasID.Size = new System.Drawing.Size(11, 15);
            this.lblLeaveMasID.TabIndex = 50;
            this.lblLeaveMasID.Text = " ";
            // 
            // btnReportingManagerSearch
            // 
            this.btnReportingManagerSearch.Location = new System.Drawing.Point(262, 25);
            this.btnReportingManagerSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnReportingManagerSearch.Name = "btnReportingManagerSearch";
            this.btnReportingManagerSearch.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnReportingManagerSearch.Size = new System.Drawing.Size(32, 28);
            this.btnReportingManagerSearch.TabIndex = 32;
            this.btnReportingManagerSearch.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnReportingManagerSearch.Values.Image = global::StaffSync.Properties.Resources.search;
            this.btnReportingManagerSearch.Values.Text = "";
            this.btnReportingManagerSearch.Click += new System.EventHandler(this.btnReportingManagerSearch_Click);
            // 
            // txtRepEmpContactNumber
            // 
            this.txtRepEmpContactNumber.Location = new System.Drawing.Point(146, 160);
            this.txtRepEmpContactNumber.Margin = new System.Windows.Forms.Padding(4);
            this.txtRepEmpContactNumber.Multiline = true;
            this.txtRepEmpContactNumber.Name = "txtRepEmpContactNumber";
            this.txtRepEmpContactNumber.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtRepEmpContactNumber.Size = new System.Drawing.Size(587, 28);
            this.txtRepEmpContactNumber.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpContactNumber.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtRepEmpContactNumber.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpContactNumber.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpContactNumber.TabIndex = 31;
            this.txtRepEmpContactNumber.Visible = false;
            this.txtRepEmpContactNumber.WordWrap = false;
            // 
            // txtRepEmpDepartment
            // 
            this.txtRepEmpDepartment.Location = new System.Drawing.Point(146, 130);
            this.txtRepEmpDepartment.Margin = new System.Windows.Forms.Padding(4);
            this.txtRepEmpDepartment.Multiline = true;
            this.txtRepEmpDepartment.Name = "txtRepEmpDepartment";
            this.txtRepEmpDepartment.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtRepEmpDepartment.Size = new System.Drawing.Size(587, 28);
            this.txtRepEmpDepartment.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpDepartment.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtRepEmpDepartment.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpDepartment.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpDepartment.TabIndex = 30;
            // 
            // txtRepEmpDesig
            // 
            this.txtRepEmpDesig.Location = new System.Drawing.Point(146, 95);
            this.txtRepEmpDesig.Margin = new System.Windows.Forms.Padding(4);
            this.txtRepEmpDesig.Multiline = true;
            this.txtRepEmpDesig.Name = "txtRepEmpDesig";
            this.txtRepEmpDesig.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtRepEmpDesig.Size = new System.Drawing.Size(587, 28);
            this.txtRepEmpDesig.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpDesig.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtRepEmpDesig.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpDesig.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpDesig.TabIndex = 29;
            this.txtRepEmpDesig.WordWrap = false;
            // 
            // txtRepEmpName
            // 
            this.txtRepEmpName.Location = new System.Drawing.Point(146, 59);
            this.txtRepEmpName.Margin = new System.Windows.Forms.Padding(4);
            this.txtRepEmpName.Multiline = true;
            this.txtRepEmpName.Name = "txtRepEmpName";
            this.txtRepEmpName.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtRepEmpName.Size = new System.Drawing.Size(587, 28);
            this.txtRepEmpName.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpName.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtRepEmpName.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpName.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpName.TabIndex = 28;
            this.txtRepEmpName.WordWrap = false;
            // 
            // txtRepEmpCode
            // 
            this.txtRepEmpCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRepEmpCode.Location = new System.Drawing.Point(146, 25);
            this.txtRepEmpCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtRepEmpCode.Multiline = true;
            this.txtRepEmpCode.Name = "txtRepEmpCode";
            this.txtRepEmpCode.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtRepEmpCode.ReadOnly = true;
            this.txtRepEmpCode.Size = new System.Drawing.Size(111, 28);
            this.txtRepEmpCode.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpCode.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtRepEmpCode.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpCode.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRepEmpCode.TabIndex = 27;
            this.txtRepEmpCode.WordWrap = false;
            // 
            // lblActionMode
            // 
            this.lblActionMode.AutoSize = true;
            this.lblActionMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActionMode.Location = new System.Drawing.Point(509, 33);
            this.lblActionMode.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblActionMode.Name = "lblActionMode";
            this.lblActionMode.Size = new System.Drawing.Size(98, 15);
            this.lblActionMode.TabIndex = 20;
            this.lblActionMode.Text = "lblActionMode";
            this.lblActionMode.Visible = false;
            // 
            // lblReportingManagerID
            // 
            this.lblReportingManagerID.AutoSize = true;
            this.lblReportingManagerID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblReportingManagerID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportingManagerID.Location = new System.Drawing.Point(484, 33);
            this.lblReportingManagerID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblReportingManagerID.Name = "lblReportingManagerID";
            this.lblReportingManagerID.Size = new System.Drawing.Size(11, 15);
            this.lblReportingManagerID.TabIndex = 19;
            this.lblReportingManagerID.Text = " ";
            this.lblReportingManagerID.Visible = false;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(33, 165);
            this.label20.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(110, 15);
            this.label20.TabIndex = 17;
            this.label20.Text = "Contact Number";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label20.Visible = false;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(924, 33);
            this.label38.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(44, 15);
            this.label38.TabIndex = 16;
            this.label38.Text = "Photo";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picRepEmpPhoto
            // 
            this.picRepEmpPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picRepEmpPhoto.Location = new System.Drawing.Point(977, 25);
            this.picRepEmpPhoto.Margin = new System.Windows.Forms.Padding(4);
            this.picRepEmpPhoto.Name = "picRepEmpPhoto";
            this.picRepEmpPhoto.Size = new System.Drawing.Size(190, 134);
            this.picRepEmpPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRepEmpPhoto.TabIndex = 15;
            this.picRepEmpPhoto.TabStop = false;
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(61, 136);
            this.label37.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(82, 15);
            this.label37.TabIndex = 13;
            this.label37.Text = "Department";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(59, 102);
            this.label36.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(84, 15);
            this.label36.TabIndex = 11;
            this.label36.Text = "Designation";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(98, 68);
            this.label35.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(45, 15);
            this.label35.TabIndex = 9;
            this.label35.Text = "Name";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(36, 33);
            this.label34.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(107, 15);
            this.label34.TabIndex = 6;
            this.label34.Text = "Employee Code";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnSaveDetails);
            this.panel2.Controls.Add(this.btnModifyDetails);
            this.panel2.Controls.Add(this.btnCloseMe);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1231, 56);
            this.panel2.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(167, 8);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCancel.Size = new System.Drawing.Size(126, 38);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Image = global::StaffSync.Properties.Resources.cancel;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSaveDetails
            // 
            this.btnSaveDetails.Location = new System.Drawing.Point(167, 12);
            this.btnSaveDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveDetails.Name = "btnSaveDetails";
            this.btnSaveDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSaveDetails.Size = new System.Drawing.Size(126, 38);
            this.btnSaveDetails.TabIndex = 12;
            this.btnSaveDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSaveDetails.Values.Image = global::StaffSync.Properties.Resources.save;
            this.btnSaveDetails.Values.Text = "Save";
            this.btnSaveDetails.Visible = false;
            this.btnSaveDetails.Click += new System.EventHandler(this.btnSaveDetails_Click);
            // 
            // btnModifyDetails
            // 
            this.btnModifyDetails.Location = new System.Drawing.Point(33, 8);
            this.btnModifyDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnModifyDetails.Name = "btnModifyDetails";
            this.btnModifyDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnModifyDetails.Size = new System.Drawing.Size(126, 38);
            this.btnModifyDetails.TabIndex = 11;
            this.btnModifyDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnModifyDetails.Values.Image = global::StaffSync.Properties.Resources.update;
            this.btnModifyDetails.Values.Text = "Modify";
            this.btnModifyDetails.Click += new System.EventHandler(this.btnModifyDetails_Click);
            // 
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(1057, 8);
            this.btnCloseMe.Margin = new System.Windows.Forms.Padding(4);
            this.btnCloseMe.Name = "btnCloseMe";
            this.btnCloseMe.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCloseMe.Size = new System.Drawing.Size(126, 38);
            this.btnCloseMe.TabIndex = 10;
            this.btnCloseMe.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCloseMe.Values.Image = global::StaffSync.Properties.Resources.close;
            this.btnCloseMe.Values.Text = "Close Me";
            this.btnCloseMe.Click += new System.EventHandler(this.btnCloseMe_Click);
            // 
            // errValidator
            // 
            this.errValidator.ContainerControl = this;
            // 
            // frmAttendanceMater
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(1231, 615);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAttendanceMater";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Attendance Details";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAttendanceMater_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAttendanceMater_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMonthNameList)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRepEmpPhoto)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox8;
        public System.Windows.Forms.Label lblActionMode;
        private System.Windows.Forms.Label lblReportingManagerID;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.PictureBox picRepEmpPhoto;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.ErrorProvider errValidator;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private Krypton.Toolkit.KryptonButton btnSaveDetails;
        private Krypton.Toolkit.KryptonButton btnModifyDetails;
        private Krypton.Toolkit.KryptonButton btnCloseMe;
        private Krypton.Toolkit.KryptonTextBox txtRepEmpContactNumber;
        private Krypton.Toolkit.KryptonTextBox txtRepEmpDepartment;
        private Krypton.Toolkit.KryptonTextBox txtRepEmpDesig;
        private Krypton.Toolkit.KryptonTextBox txtRepEmpName;
        private Krypton.Toolkit.KryptonTextBox txtRepEmpCode;
        private Krypton.Toolkit.KryptonButton btnReportingManagerSearch;
        private Krypton.Toolkit.KryptonComboBox cmbMonthNameList;
        private System.Windows.Forms.Label label50;
        private Calender empAttCalender;
        private Krypton.Toolkit.KryptonButton picViewLeaves;
        public System.Windows.Forms.Label lblLeaveMasID;
        //private Calender empAttCalender;
    }
}