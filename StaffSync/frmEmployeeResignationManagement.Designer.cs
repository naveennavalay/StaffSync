namespace StaffSync
{
    partial class frmEmployeeResignationManagement
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
            this.ttAdvanceType = new System.Windows.Forms.ToolTip(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCloseMe = new Krypton.Toolkit.KryptonButton();
            this.btnRemoveDetails = new Krypton.Toolkit.KryptonButton();
            this.btnSaveDetails = new Krypton.Toolkit.KryptonButton();
            this.btnModifyDetails = new Krypton.Toolkit.KryptonButton();
            this.btnGenerateDetails = new Krypton.Toolkit.KryptonButton();
            this.btnCancel = new Krypton.Toolkit.KryptonButton();
            this.errValidator = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblResignationReasonID = new System.Windows.Forms.Label();
            this.lblEmpResignID = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtResignationMoreDetails = new Krypton.Toolkit.KryptonTextBox();
            this.dtResignationDate = new System.Windows.Forms.MaskedTextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.cmbResignationReason = new Krypton.Toolkit.KryptonComboBox();
            this.label42 = new System.Windows.Forms.Label();
            this.txtResignationCode = new Krypton.Toolkit.KryptonTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new Krypton.Navigator.KryptonNavigator();
            this.tabOrgAddressInfo = new Krypton.Navigator.KryptonPage();
            this.tabOrgEPFSettings = new Krypton.Navigator.KryptonPage();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbResignationReason)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabOrgAddressInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabOrgEPFSettings)).BeginInit();
            this.SuspendLayout();
            // 
            // ttAdvanceType
            // 
            this.ttAdvanceType.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.ttAdvanceType.ToolTipTitle = "This is sample tooltip text";
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
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1056, 71);
            this.panel2.TabIndex = 1;
            // 
            // btnCloseMe
            // 
            this.btnCloseMe.Location = new System.Drawing.Point(903, 12);
            this.btnCloseMe.Margin = new System.Windows.Forms.Padding(4);
            this.btnCloseMe.Name = "btnCloseMe";
            this.btnCloseMe.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCloseMe.Size = new System.Drawing.Size(126, 38);
            this.btnCloseMe.TabIndex = 5;
            this.toolTip1.SetToolTip(this.btnCloseMe, "Close Me (Ctrl + Z)");
            this.btnCloseMe.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCloseMe.Values.Image = global::StaffSync.Properties.Resources.close;
            this.btnCloseMe.Values.Text = "Close Me";
            this.btnCloseMe.Click += new System.EventHandler(this.btnCloseMe_Click);
            // 
            // btnRemoveDetails
            // 
            this.btnRemoveDetails.Location = new System.Drawing.Point(434, 12);
            this.btnRemoveDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemoveDetails.Name = "btnRemoveDetails";
            this.btnRemoveDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnRemoveDetails.Size = new System.Drawing.Size(126, 38);
            this.btnRemoveDetails.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnRemoveDetails, "Delete (Ctrl + D)");
            this.btnRemoveDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnRemoveDetails.Values.Image = global::StaffSync.Properties.Resources.delete;
            this.btnRemoveDetails.Values.Text = "Delete";
            this.btnRemoveDetails.Visible = false;
            // 
            // btnSaveDetails
            // 
            this.btnSaveDetails.Location = new System.Drawing.Point(166, 12);
            this.btnSaveDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveDetails.Name = "btnSaveDetails";
            this.btnSaveDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnSaveDetails.Size = new System.Drawing.Size(126, 38);
            this.btnSaveDetails.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnSaveDetails, "Save (Ctrl + S)");
            this.btnSaveDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnSaveDetails.Values.Image = global::StaffSync.Properties.Resources.save;
            this.btnSaveDetails.Values.Text = "Save";
            this.btnSaveDetails.Click += new System.EventHandler(this.btnSaveDetails_Click);
            // 
            // btnModifyDetails
            // 
            this.btnModifyDetails.Location = new System.Drawing.Point(568, 12);
            this.btnModifyDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnModifyDetails.Name = "btnModifyDetails";
            this.btnModifyDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnModifyDetails.Size = new System.Drawing.Size(126, 38);
            this.btnModifyDetails.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnModifyDetails, "Modify (Ctrl + E)");
            this.btnModifyDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnModifyDetails.Values.Image = global::StaffSync.Properties.Resources.update;
            this.btnModifyDetails.Values.Text = "Modify";
            this.btnModifyDetails.Visible = false;
            // 
            // btnGenerateDetails
            // 
            this.btnGenerateDetails.Location = new System.Drawing.Point(31, 12);
            this.btnGenerateDetails.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerateDetails.Name = "btnGenerateDetails";
            this.btnGenerateDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnGenerateDetails.Size = new System.Drawing.Size(126, 38);
            this.btnGenerateDetails.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btnGenerateDetails, "Generate (Ctrl + N)");
            this.btnGenerateDetails.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnGenerateDetails.Values.Image = global::StaffSync.Properties.Resources._new;
            this.btnGenerateDetails.Values.Text = "Generate";
            this.btnGenerateDetails.Click += new System.EventHandler(this.btnGenerateDetails_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(300, 12);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007Blue;
            this.btnCancel.Size = new System.Drawing.Size(126, 38);
            this.btnCancel.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btnCancel, "Cancel (Ctrl + X)");
            this.btnCancel.Values.DropDownArrowColor = System.Drawing.Color.Empty;
            this.btnCancel.Values.Image = global::StaffSync.Properties.Resources.cancel;
            this.btnCancel.Values.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // errValidator
            // 
            this.errValidator.ContainerControl = this;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1056, 643);
            this.panel1.TabIndex = 1;
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
            this.splitContainer1.Size = new System.Drawing.Size(1056, 719);
            this.splitContainer1.SplitterDistance = 643;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.groupBox3.Controls.Add(this.lblResignationReasonID);
            this.groupBox3.Controls.Add(this.lblEmpResignID);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txtResignationMoreDetails);
            this.groupBox3.Controls.Add(this.dtResignationDate);
            this.groupBox3.Controls.Add(this.label29);
            this.groupBox3.Controls.Add(this.cmbResignationReason);
            this.groupBox3.Controls.Add(this.label42);
            this.groupBox3.Controls.Add(this.txtResignationCode);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.groupBox3.Location = new System.Drawing.Point(12, 13);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(1032, 189);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Resignation Details";
            // 
            // lblResignationReasonID
            // 
            this.lblResignationReasonID.AutoSize = true;
            this.lblResignationReasonID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblResignationReasonID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResignationReasonID.Location = new System.Drawing.Point(748, 65);
            this.lblResignationReasonID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblResignationReasonID.Name = "lblResignationReasonID";
            this.lblResignationReasonID.Size = new System.Drawing.Size(11, 15);
            this.lblResignationReasonID.TabIndex = 83;
            this.lblResignationReasonID.Text = " ";
            this.lblResignationReasonID.Visible = false;
            // 
            // lblEmpResignID
            // 
            this.lblEmpResignID.AutoSize = true;
            this.lblEmpResignID.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblEmpResignID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpResignID.Location = new System.Drawing.Point(341, 28);
            this.lblEmpResignID.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.lblEmpResignID.Name = "lblEmpResignID";
            this.lblEmpResignID.Size = new System.Drawing.Size(11, 15);
            this.lblEmpResignID.TabIndex = 82;
            this.lblEmpResignID.Text = " ";
            this.lblEmpResignID.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(63, 124);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 15);
            this.label5.TabIndex = 75;
            this.label5.Text = "More Details";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtResignationMoreDetails
            // 
            this.txtResignationMoreDetails.Location = new System.Drawing.Point(154, 124);
            this.txtResignationMoreDetails.Margin = new System.Windows.Forms.Padding(4);
            this.txtResignationMoreDetails.Multiline = true;
            this.txtResignationMoreDetails.Name = "txtResignationMoreDetails";
            this.txtResignationMoreDetails.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtResignationMoreDetails.Size = new System.Drawing.Size(587, 56);
            this.txtResignationMoreDetails.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResignationMoreDetails.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtResignationMoreDetails.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResignationMoreDetails.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResignationMoreDetails.TabIndex = 72;
            this.txtResignationMoreDetails.Tag = "Resignation More Details";
            this.txtResignationMoreDetails.WordWrap = false;
            // 
            // dtResignationDate
            // 
            this.dtResignationDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(0)))), ((int)(((byte)(254)))));
            this.dtResignationDate.Location = new System.Drawing.Point(154, 92);
            this.dtResignationDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtResignationDate.Mask = "##-##-####";
            this.dtResignationDate.Name = "dtResignationDate";
            this.dtResignationDate.Size = new System.Drawing.Size(103, 21);
            this.dtResignationDate.TabIndex = 71;
            this.dtResignationDate.Tag = "Resignation Date";
            this.dtResignationDate.ValidatingType = typeof(System.DateTime);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label29.Location = new System.Drawing.Point(34, 95);
            this.label29.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(118, 15);
            this.label29.TabIndex = 74;
            this.label29.Text = "Resignation Date";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbResignationReason
            // 
            this.cmbResignationReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResignationReason.DropDownWidth = 440;
            this.cmbResignationReason.Location = new System.Drawing.Point(154, 60);
            this.cmbResignationReason.Margin = new System.Windows.Forms.Padding(4);
            this.cmbResignationReason.Name = "cmbResignationReason";
            this.cmbResignationReason.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.cmbResignationReason.Size = new System.Drawing.Size(588, 22);
            this.cmbResignationReason.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            this.cmbResignationReason.TabIndex = 70;
            this.cmbResignationReason.Tag = "Resignation Reason";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label42.Location = new System.Drawing.Point(15, 63);
            this.label42.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(137, 15);
            this.label42.TabIndex = 73;
            this.label42.Text = "Resignation Reason";
            this.label42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtResignationCode
            // 
            this.txtResignationCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtResignationCode.Location = new System.Drawing.Point(154, 22);
            this.txtResignationCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtResignationCode.Multiline = true;
            this.txtResignationCode.Name = "txtResignationCode";
            this.txtResignationCode.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.txtResignationCode.ReadOnly = true;
            this.txtResignationCode.Size = new System.Drawing.Size(168, 28);
            this.txtResignationCode.StateActive.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResignationCode.StateCommon.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F);
            this.txtResignationCode.StateDisabled.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResignationCode.StateNormal.Content.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResignationCode.TabIndex = 30;
            this.txtResignationCode.Tag = "Employee Code";
            this.txtResignationCode.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 15);
            this.label1.TabIndex = 29;
            this.label1.Text = "Resignation Code";
            // 
            // tabControl1
            // 
            this.tabControl1.Bar.BarMapExtraText = Krypton.Navigator.MapKryptonPageText.None;
            this.tabControl1.Bar.BarMapImage = Krypton.Navigator.MapKryptonPageImage.Small;
            this.tabControl1.Bar.BarMapText = Krypton.Navigator.MapKryptonPageText.TextTitle;
            this.tabControl1.Bar.BarMultiline = Krypton.Navigator.BarMultiline.Singleline;
            this.tabControl1.Bar.BarOrientation = Krypton.Toolkit.VisualOrientation.Top;
            this.tabControl1.Bar.CheckButtonStyle = Krypton.Toolkit.ButtonStyle.Standalone;
            this.tabControl1.Bar.ItemAlignment = Krypton.Toolkit.RelativePositionAlign.Near;
            this.tabControl1.Bar.ItemMaximumSize = new System.Drawing.Size(200, 200);
            this.tabControl1.Bar.ItemMinimumSize = new System.Drawing.Size(20, 20);
            this.tabControl1.Bar.ItemOrientation = Krypton.Toolkit.ButtonOrientation.Auto;
            this.tabControl1.Bar.ItemSizing = Krypton.Navigator.BarItemSizing.SameWidthAndHeight;
            this.tabControl1.Bar.TabBorderStyle = Krypton.Toolkit.TabBorderStyle.RoundedOutsizeMedium;
            this.tabControl1.Bar.TabStyle = Krypton.Toolkit.TabStyle.HighProfile;
            this.tabControl1.Button.ButtonDisplayLogic = Krypton.Navigator.ButtonDisplayLogic.None;
            this.tabControl1.Button.CloseButtonAction = Krypton.Navigator.CloseButtonAction.None;
            this.tabControl1.Button.CloseButtonDisplay = Krypton.Navigator.ButtonDisplay.Hide;
            this.tabControl1.Button.ContextButtonAction = Krypton.Navigator.ContextButtonAction.SelectPage;
            this.tabControl1.Button.ContextButtonDisplay = Krypton.Navigator.ButtonDisplay.Logic;
            this.tabControl1.Button.ContextMenuMapImage = Krypton.Navigator.MapKryptonPageImage.Small;
            this.tabControl1.Button.ContextMenuMapText = Krypton.Navigator.MapKryptonPageText.TextTitle;
            this.tabControl1.Button.NextButtonAction = Krypton.Navigator.DirectionButtonAction.ModeAppropriateAction;
            this.tabControl1.Button.NextButtonDisplay = Krypton.Navigator.ButtonDisplay.Logic;
            this.tabControl1.Button.PreviousButtonAction = Krypton.Navigator.DirectionButtonAction.ModeAppropriateAction;
            this.tabControl1.Button.PreviousButtonDisplay = Krypton.Navigator.ButtonDisplay.Logic;
            this.tabControl1.ControlKryptonFormFeatures = false;
            this.tabControl1.Group.GroupBackStyle = Krypton.Toolkit.PaletteBackStyle.ContextMenuHeading;
            this.tabControl1.Group.GroupBorderStyle = Krypton.Toolkit.PaletteBorderStyle.ControlClient;
            this.tabControl1.Location = new System.Drawing.Point(12, 225);
            this.tabControl1.NavigatorMode = Krypton.Navigator.NavigatorMode.BarTabGroup;
            this.tabControl1.Owner = null;
            this.tabControl1.PageBackStyle = Krypton.Toolkit.PaletteBackStyle.ButtonStandalone;
            this.tabControl1.Pages.AddRange(new Krypton.Navigator.KryptonPage[] {
            this.tabOrgAddressInfo,
            this.tabOrgEPFSettings});
            this.tabControl1.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlueLightMode;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(916, 304);
            this.tabControl1.TabIndex = 37;
            // 
            // tabOrgAddressInfo
            // 
            this.tabOrgAddressInfo.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.tabOrgAddressInfo.Flags = 65534;
            this.tabOrgAddressInfo.LastVisibleSet = true;
            this.tabOrgAddressInfo.MinimumSize = new System.Drawing.Size(150, 50);
            this.tabOrgAddressInfo.Name = "tabOrgAddressInfo";
            this.tabOrgAddressInfo.Size = new System.Drawing.Size(914, 277);
            this.tabOrgAddressInfo.StateNormal.Page.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabOrgAddressInfo.StateNormal.Page.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabOrgAddressInfo.StateNormal.Panel.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabOrgAddressInfo.StateNormal.Panel.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabOrgAddressInfo.Tag = "tabOrgAddressInfo";
            this.tabOrgAddressInfo.Text = "Asset Master Information";
            this.tabOrgAddressInfo.ToolTipTitle = "Organisation Address Details";
            this.tabOrgAddressInfo.UniqueName = "835e9c622415491ca48a7f41c4542b75";
            // 
            // tabOrgEPFSettings
            // 
            this.tabOrgEPFSettings.AutoHiddenSlideSize = new System.Drawing.Size(200, 200);
            this.tabOrgEPFSettings.Flags = 65534;
            this.tabOrgEPFSettings.LastVisibleSet = true;
            this.tabOrgEPFSettings.MinimumSize = new System.Drawing.Size(150, 50);
            this.tabOrgEPFSettings.Name = "tabOrgEPFSettings";
            this.tabOrgEPFSettings.Size = new System.Drawing.Size(914, 277);
            this.tabOrgEPFSettings.StateNormal.Page.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabOrgEPFSettings.StateNormal.Page.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabOrgEPFSettings.StateNormal.Panel.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabOrgEPFSettings.StateNormal.Panel.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(228)))), ((int)(((byte)(242)))));
            this.tabOrgEPFSettings.Tag = "tabOrgEPFSettings";
            this.tabOrgEPFSettings.Text = "Assets More Information";
            this.tabOrgEPFSettings.ToolTipTitle = "Assets More Information";
            this.tabOrgEPFSettings.UniqueName = "cc53dc217c7c4b2ebf55c32b66c45cb7";
            // 
            // frmEmployeeResignationManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 719);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmEmployeeResignationManagement";
            this.Text = "frmEmployeeResignationManagement";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmEmployeeResignationManagement_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmEmployeeResignationManagement_KeyDown);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errValidator)).EndInit();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbResignationReason)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabOrgAddressInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabOrgEPFSettings)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip ttAdvanceType;
        private System.Windows.Forms.Panel panel2;
        private Krypton.Toolkit.KryptonButton btnCloseMe;
        private System.Windows.Forms.ToolTip toolTip1;
        private Krypton.Toolkit.KryptonButton btnRemoveDetails;
        private Krypton.Toolkit.KryptonButton btnSaveDetails;
        private Krypton.Toolkit.KryptonButton btnModifyDetails;
        private Krypton.Toolkit.KryptonButton btnGenerateDetails;
        private Krypton.Toolkit.KryptonButton btnCancel;
        private System.Windows.Forms.ErrorProvider errValidator;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblResignationReasonID;
        private System.Windows.Forms.Label lblEmpResignID;
        private System.Windows.Forms.Label label5;
        private Krypton.Toolkit.KryptonTextBox txtResignationMoreDetails;
        private System.Windows.Forms.MaskedTextBox dtResignationDate;
        private System.Windows.Forms.Label label29;
        private Krypton.Toolkit.KryptonComboBox cmbResignationReason;
        private System.Windows.Forms.Label label42;
        private Krypton.Toolkit.KryptonTextBox txtResignationCode;
        private System.Windows.Forms.Label label1;
        private Krypton.Navigator.KryptonNavigator tabControl1;
        private Krypton.Navigator.KryptonPage tabOrgAddressInfo;
        private Krypton.Navigator.KryptonPage tabOrgEPFSettings;
    }
}