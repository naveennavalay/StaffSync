using Common;
using DocumentFormat.OpenXml.Bibliography;
using Krypton.Toolkit;
using ModelStaffSync;
using Newtonsoft.Json;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public partial class frmEmployeeResignationManagement : Form
    {
        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        frmDashboard objDashboard = (frmDashboard)System.Windows.Forms.Application.OpenForms["frmDashboard"];
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        DALStaffSync.clsAuditLog objAuditLog = new DALStaffSync.clsAuditLog();
        DALStaffSync.clsResignationInfo objResignationInfo = new DALStaffSync.clsResignationInfo();
        DALStaffSync.clsEmpResignLeaveEncashmentInfo objEmpResignLeaveEncashmentInfo = new DALStaffSync.clsEmpResignLeaveEncashmentInfo();
        DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster();
        DALStaffSync.clsEmployeePersonalInfo objEmployeePersonalInfo = new DALStaffSync.clsEmployeePersonalInfo();
        DALStaffSync.clsDepartment objDepartmentInfo = new DALStaffSync.clsDepartment();
        DALStaffSync.clsEmpReimbursRequestInfo objEmpReimbursRequestInfo = new DALStaffSync.clsEmpReimbursRequestInfo();
        DALStaffSync.clsAdvanceTransaction objAdvanceTransaction = new DALStaffSync.clsAdvanceTransaction();
        DALStaffSync.clsAssetRegister objAssetRegisterInfo = new DALStaffSync.clsAssetRegister();
        DALStaffSync.clsEmpLeaveEntitlementInfo objEmpLeaveEntitlementInfo = new DALStaffSync.clsEmpLeaveEntitlementInfo();
        DALStaffSync.clsEmpResignGratuityInfo objEmpResignGratuityInfo = new DALStaffSync.clsEmpResignGratuityInfo();

        clsImpageOperation objImpageOperation = new clsImpageOperation();
        DALStaffSync.clsPhotoMas objPhotoMas = new DALStaffSync.clsPhotoMas();

        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();
        SpecificEmployeeSalaryInfo objSpecificEmployeeSalaryInfo = new SpecificEmployeeSalaryInfo();
        AdvanceTypeConfigModel objAdvanceTypeConfigModel = new AdvanceTypeConfigModel();

        string strActionStatement = "";
        private Dictionary<string, object> _originalValues;
        private bool _isUpdatingGratuityValues = false;

        // Email notification baseline: captures the assigned reviewer and status when a record is loaded.
        private string _reviewEmailOriginalStatus = string.Empty;
        private string _reviewEmailOriginalApprover = string.Empty;
        private string _reviewEmailOriginalApproverID = string.Empty;

        // Workflow UI: access is based on the status saved in the database when the
        // resignation is loaded. Changing the dropdown does not unlock tabs for the
        // current reviewer; the newly assigned reviewer receives access on next load.
        private int _loadedResignationWorkflowStatus = 0;
        private Panel _resignationWorkflowProgressPanel;

        bool boolMaxLeaveToBeConsidered = false;

        DateTime dos;
        string dateFormat = "dd-MM-yyyy";
        CultureInfo provider = CultureInfo.InvariantCulture;

        public frmEmployeeResignationManagement()
        {
            InitializeComponent();
            RegisterGratuityCalculationEvents();
            InitializeResignationWorkflowUI();
        }

        public frmEmployeeResignationManagement(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            RegisterGratuityCalculationEvents();
            InitializeResignationWorkflowUI();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmEmployeeResignationManagement(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            RegisterGratuityCalculationEvents();
            InitializeResignationWorkflowUI();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
            lblClientID.Text = objTempClientFinYearInfo.ClientID.ToString();
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            //if (lblActionMode.Text != "")
            //{
            //    if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //    {
            //        return;
            //    }
            //}
            objDashboard.lblDashboardTitle.Text = "Dashboard";
            objDashboard.sptrDashboardContainer.Visible = true;
            objDashboard.grpDashboardDateRange.Visible = true;
            objDashboard.refreshDashboardCharts();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (lblActionMode.Text == "add")
            {
                if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            else if (lblActionMode.Text == "modify")
            {
                if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            else if (lblActionMode.Text == "delete")
            {
                if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            lblActionMode.Text = "";
            onCancelButtonClick();
            disableControls();
            clearControls();
            errValidator.Clear();
        }

        public void clearControls()
        {
            _reviewEmailOriginalStatus = string.Empty;
            _reviewEmailOriginalApprover = string.Empty;
            _reviewEmailOriginalApproverID = string.Empty;
            lblMaxLeaveConsidered.Visible = false;
            lnkViewAuditLog.Visible = false;
            lblEmpID.Text = "";
            txtEmpCode.Text = "";
            txtEmpName.Text = "";
            txtDesignation.Text = "";
            txtDepartment.Text = "";
            picEmpPhoto.Image = null;
            lblPersonalInfoID.Text = "";
            txtReportingManager.Text = "";
            txtHROwner.Text = "";

            lblEmpResignID.Text = "";
            txtResignationCode.Text = "";
            dtResignationDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            cmbResignationReason.DataSource = null;
            cmbResignationStatus.Items.Clear();
            cmbResignationStatus.DataSource = null;
            txtResignationMoreDetails.Text = "";

            lblResignationReasonID.Text = "";
            lblRepManagerApprovalStatus.Text = "";

            txtDateOfJoining.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            txtNoticePeriod.Text = "30";
            dtLastWorkingDay.Text = DateTime.Now.AddDays(Convert.ToInt32(txtNoticePeriod.Text)).ToString("dd-MM-yyyy");
            dtApprovalDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            lblActionMode.Text = "";
            lblRepManID.Text = "";
            tabControl1.SelectedIndex = 0;
            dtgReimbursmentPaymentHistory.DataSource = null;
            dtgAdvanceList.DataSource = null;
            dtgAssetInformation.DataSource = null;

            boolMaxLeaveToBeConsidered = false;
            txtTotalLeaveAlloted.Text = "0";
            txtTotalLeaveUtilised.Text = "0";
            txtLeaveBalance.Text = "0";
            txtEncashableLeaveBalance.Text = "0";

            txtMonthlyBasicSalary.Text = "0.00";
            txtDailySalaryRate.Text = "0.00";
            txtEncashmentAmount.Text = "0.00";
            txtTaxDeductionOnEncashmentAmount.Text = "0.00";
            txtNetEncashmentAmount.Text = "0.00";
            txtLeaveEncashmentMoreDetails.Text = "";
            chkLeaveEncashmentVerifiedAndConfirmed.Checked = false;
            lblNextApproverMailID.Text = "";
            lblLeaveEncashmentID.Text = "";

            txtGRTDateOfJoining.Text = "";
            txtGRTResignationDate.Text = "";
            txtGRTLastWorkingDate.Text = "";
            txtGRTServiceTenure.Text = "";
            txtGRTEligibleYears.Text = "";
            txtGRTEligibleStatus.Text = "";

            txtGRTLastDrawnBasic.Text = "0.00";
            txtGRTEligibleAllowance.Text = "0.00";
            txtGRTGratuitySalaryBasis.Text = "0.00";
            txtGRTGorssGratuityAmount.Text = "0.00";
            txtGRTTaxOrDeduction.Text = "0.00";
            txtGRTNetGratuityAmount.Text = "0.00";

            cmbGRTHRApprovalBy.DataSource = null;
            dtGRTHRApprovalDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
            cmbGRTHRApprovalStatus.Items.Clear();
            cmbGRTHRApprovalStatus.DataSource = null;
            txtGRTHRRemarks.Text = "";

            cmbGRTFINApprovalBy.DataSource = null;
            dtGRTFINApprovalDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
            cmbGRTFINApprovalStatus.Items.Clear();
            cmbGRTFINApprovalStatus.DataSource = null;
            txtGRTFINRemarks.Text = "";

            cmbGRTPaymentStatus.Items.Clear();
            cmbGRTPaymentStatus.DataSource = null;
            dtGRTPaymentDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
            txtGRTPaymentRefNo.Text = "";
            txtGRTPaymentRemarks.Text = "";
            lblEmpResignGratuityID.Text = "";
        }

        public void enableControls()
        {
            btnResignedRequestSearch.Enabled = true;
            txtResignationCode.Enabled = false;
            txtEmpCode.Enabled = false;
            txtEmpName.Enabled = false;
            txtDesignation.Enabled = false;
            txtDepartment.Enabled = false;

            txtDateOfJoining.Enabled = false;
            txtServiceTenure.Enabled = false;
            cmbResignationReason.Enabled = false;
            dtResignationDate.Enabled = false;
            txtNoticePeriod.Enabled = true;
            dtLastWorkingDay.Enabled = true;
            txtResignationMoreDetails.Enabled = false;
            txtNoticePeriodStatus.Enabled = false;
            dtApprovalDate.Enabled = true;
            txtReportingManager.Enabled = false;
            txtHROwner.Enabled = false;
            txtLeaveEncashmentMoreDetails.Enabled = true;
            chkLeaveEncashmentVerifiedAndConfirmed.Checked = false;
            chkLeaveEncashmentVerifiedAndConfirmed.Enabled = true;

            txtGRTDateOfJoining.Enabled = true;
            txtGRTResignationDate.Enabled = true;
            txtGRTLastWorkingDate.Enabled = true;
            txtGRTServiceTenure.Enabled = true;
            txtGRTEligibleYears.Enabled = true;
            txtGRTEligibleStatus.Enabled = true;

            txtGRTLastDrawnBasic.Enabled = true;
            txtGRTEligibleAllowance.Enabled = true; ;
            txtGRTGratuitySalaryBasis.Enabled = true;
            txtGRTGorssGratuityAmount.Enabled = true;
            txtGRTTaxOrDeduction.Enabled = true;
            txtGRTNetGratuityAmount.Enabled = true;

            cmbGRTHRApprovalBy.Enabled = true;
            dtGRTHRApprovalDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
            dtGRTHRApprovalDate.Enabled = true;
            cmbGRTHRApprovalStatus.Enabled = true;
            txtGRTHRRemarks.Enabled = true;

            cmbGRTFINApprovalBy.Enabled = true;
            dtGRTFINApprovalDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
            dtGRTFINApprovalDate.Enabled = true;
            cmbGRTFINApprovalStatus.Enabled = true;
            txtGRTFINRemarks.Enabled = true;

            cmbGRTPaymentStatus.Enabled = true;
            dtGRTPaymentDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
            txtGRTPaymentRefNo.Enabled = true;
            txtGRTPaymentRemarks.Enabled = true;
        }

        public void disableControls()
        {
            btnResignedRequestSearch.Enabled = false;
            txtResignationCode.Enabled = false;
            txtEmpCode.Enabled = false;
            txtEmpName.Enabled = false;
            txtDesignation.Enabled = false;
            txtDepartment.Enabled = false;

            txtDateOfJoining.Enabled = false;
            txtServiceTenure.Enabled = false;
            txtNoticePeriod.Enabled = false;
            cmbResignationReason.Enabled = false;
            dtResignationDate.Enabled = false;
            txtNoticePeriodStatus.Enabled = false;
            dtLastWorkingDay.Enabled = false;
            txtResignationMoreDetails.Enabled = false;
            dtApprovalDate.Enabled = false;
            txtReportingManager.Enabled = false;
            txtHROwner.Enabled = false;
            tabControl1.Enabled = false;
            tabControl1.SelectedIndex = 0;
            txtLeaveEncashmentMoreDetails.Enabled = false;
            chkLeaveEncashmentVerifiedAndConfirmed.Enabled = false;

            txtGRTDateOfJoining.Enabled = false;
            txtGRTResignationDate.Enabled = false;
            txtGRTLastWorkingDate.Enabled = false;
            txtGRTServiceTenure.Enabled = false;
            txtGRTEligibleYears.Enabled = false;
            txtGRTEligibleStatus.Enabled = false;

            txtGRTLastDrawnBasic.Enabled = false;
            txtGRTEligibleAllowance.Enabled = false; ;
            txtGRTGratuitySalaryBasis.Enabled = false;
            txtGRTGorssGratuityAmount.Enabled = false;
            txtGRTTaxOrDeduction.Enabled = false;
            txtGRTNetGratuityAmount.Enabled = false;

            cmbGRTHRApprovalBy.Enabled = false;
            dtGRTHRApprovalDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
            dtGRTHRApprovalDate.Enabled = false;
            cmbGRTHRApprovalStatus.Enabled = false;
            txtGRTHRRemarks.Enabled = false;

            cmbGRTFINApprovalBy.Enabled = false;
            dtGRTFINApprovalDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
            dtGRTFINApprovalDate.Enabled = false;
            cmbGRTFINApprovalStatus.Enabled = false;
            txtGRTFINRemarks.Enabled = false;

            cmbGRTPaymentStatus.Enabled = false;
            dtGRTPaymentDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
            txtGRTPaymentRefNo.Enabled = false;
            txtGRTPaymentRemarks.Enabled = false;
        }

        public void onGenerateButtonClick()
        {
            lblActionMode.Text = "add";
            lblEmpID.Text = "";
            lblPersonalInfoID.Text = "";
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = false;
            btnCancel.Enabled = true;
        }

        public void onModifyButtonClick()
        {
            lblActionMode.Text = "modify";
            lblEmpID.Text = "";
            lblPersonalInfoID.Text = "";
            btnResignedRequestSearch.Enabled = true;
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = false;
            btnCancel.Enabled = true;
        }

        public void onRemoveButtonClick()
        {
            lblActionMode.Text = "remove";
            lblEmpID.Text = "";
            lblPersonalInfoID.Text = "";
            btnResignedRequestSearch.Enabled = true;
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onSaveButtonClick()
        {
            lblActionMode.Text = "";
            lblPersonalInfoID.Text = "";
            btnResignedRequestSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onCancelButtonClick()
        {
            lblActionMode.Text = "";
            lblEmpID.Text = "";
            lblPersonalInfoID.Text = "";
            btnResignedRequestSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void frmEmployeeResignationManagement_Load(object sender, EventArgs e)
        {
            FocusManager.EnableHighlighting = false;
            FocusManager.ShowNavigationError = true;
            FocusManager.Register(this);
            FocusManager.SetFocus(btnGenerateDetails);
            clearControls();
            disableControls();
            onCancelButtonClick();
        }

        private void btnGenerateDetails_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string strValidationMessage = objLogin.ValidateUserRolesAndResponsibilitiesInfo(objTempCurrentlyLoggedInUserInfo.EmpID, "add");
            if (strValidationMessage != "Success")
            {
                MessageBox.Show(strValidationMessage, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            onGenerateButtonClick();
            clearControls();
            enableControls();
            errValidator.Clear();

            lblActionMode.Text = "add";

            dtResignationDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            cmbResignationReason.DataSource = objResignationInfo.getResignationInfotatements();
            cmbResignationReason.DisplayMember = "ResigMasTitle";
            cmbResignationReason.ValueMember = "ResigMasID";
            txtNoticePeriod.Enabled = true;
            dtLastWorkingDay.Enabled = true;
            txtNoticePeriodStatus.Enabled = true;
            LoadResignationFinalStatus();
            _loadedResignationWorkflowStatus = 0;
            ApplyResignationWorkflowTabAccess();
            UpdateResignationWorkflowProgress();
        }

        /// <summary>
        /// Adds the workflow progress indicator without changing the Windows Forms designer
        /// or moving/replacing any existing controls.
        /// </summary>
        private void InitializeResignationWorkflowUI()
        {
            if (_resignationWorkflowProgressPanel != null || panel1 == null || tabControl1 == null)
                return;

            // Keep the existing layout positions and use the available gap above the tab strip.
            // The timeline must not push the tab strip or Approval & Status group downward.
            // Retain these layout statements with zero offset to avoid changing other controls.
            tabControl1.Top += 0;
            if (groupBox2 != null)
                groupBox2.Top += 0;

            _resignationWorkflowProgressPanel = new Panel();
            _resignationWorkflowProgressPanel.Name = "pnlResignationWorkflowProgress";
            _resignationWorkflowProgressPanel.BackColor = panel1.BackColor;
            _resignationWorkflowProgressPanel.Location = new Point(tabControl1.Left, tabControl1.Top - 27);
            _resignationWorkflowProgressPanel.Size = new Size(tabControl1.Width, 27);
            _resignationWorkflowProgressPanel.Anchor = tabControl1.Anchor;
            _resignationWorkflowProgressPanel.Paint += ResignationWorkflowProgressPanel_Paint;
            _resignationWorkflowProgressPanel.Visible = true;
            panel1.Controls.Add(_resignationWorkflowProgressPanel);
            _resignationWorkflowProgressPanel.BringToFront();

            // Keep the indicator aligned if the form or its content area is resized.
            tabControl1.LocationChanged += (sender, args) =>
            {
                if (_resignationWorkflowProgressPanel != null)
                    _resignationWorkflowProgressPanel.Location = new Point(tabControl1.Left, tabControl1.Top - 27);
            };
            tabControl1.SizeChanged += (sender, args) =>
            {
                if (_resignationWorkflowProgressPanel != null)
                    _resignationWorkflowProgressPanel.Width = tabControl1.Width;
            };

            ApplyResignationWorkflowTabAccess();
            UpdateResignationWorkflowProgress();
        }

        /// <summary>
        /// Always enables Resignation Information. Only the tab associated with the
        /// currently saved review stage is enabled for the assigned reviewer.
        /// </summary>
        private void ApplyResignationWorkflowTabAccess()
        {
            if (tabResignationInfo == null || tabControl1 == null)
                return;

            // First tab is the common information tab available at every stage.
            tabResignationInfo.Enabled = true;
            tabReimbursementInformation.Enabled = (_loadedResignationWorkflowStatus == 3);
            tabAdvanceInformation.Enabled = (_loadedResignationWorkflowStatus == 4);
            tabLeaveEncashment.Enabled = (_loadedResignationWorkflowStatus == 5);
            tabGratuityInformation.Enabled = (_loadedResignationWorkflowStatus == 6);
            tabAssetsInformation.Enabled = (_loadedResignationWorkflowStatus == 7);

            // HR review (status 2) is performed through Resignation Information.
            // Completed and Withdrawn/Cancelled are terminal; only the common tab remains accessible.
            if (_loadedResignationWorkflowStatus < 1 || _loadedResignationWorkflowStatus >= 8)
            {
                tabReimbursementInformation.Enabled = false;
                tabAdvanceInformation.Enabled = false;
                tabLeaveEncashment.Enabled = false;
                tabGratuityInformation.Enabled = false;
                tabAssetsInformation.Enabled = false;
            }

            if (!tabResignationInfo.Enabled || tabControl1.SelectedPage != tabResignationInfo)
                tabControl1.SelectedPage = tabResignationInfo;
        }

        private void UpdateResignationWorkflowProgress()
        {
            if (_resignationWorkflowProgressPanel != null)
                _resignationWorkflowProgressPanel.Invalidate();
        }

        private void ResignationWorkflowProgressPanel_Paint(object sender, PaintEventArgs e)
        {
            // Status order mirrors cmbResignationStatus: 1..7 are review stages,
            // 8 is Completed, and 9 is Withdrawn/Cancelled.
            string[] stageNames = { "Start", "HR", "Reimbursements", "Advances", "Leave Encashment", "Gratuity", "Assets", "Completed" };
            int stageCount = stageNames.Length;
            int currentStatus = _loadedResignationWorkflowStatus;
            int width = _resignationWorkflowProgressPanel.ClientSize.Width;
            // Compact timeline: place stage labels ABOVE the markers so they do not
            // consume the vertical gap between the timeline and the tab strip.
            int centerY = 19;
            int left = 16;
            int right = Math.Max(left + 1, width - 16);
            float spacing = stageCount > 1 ? (right - left) / (float)(stageCount - 1) : 0;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            using (Pen basePen = new Pen(Color.FromArgb(190, 205, 30), 4))
                e.Graphics.DrawLine(basePen, left, centerY, right, centerY);

            int completedThrough = currentStatus == 8 ? 8 : Math.Max(0, currentStatus - 1);
            if (currentStatus == 9)
                completedThrough = 0;

            if (completedThrough > 0)
            {
                float completedX = left + (completedThrough - 1) * spacing;
                using (Pen completedPen = new Pen(Color.FromArgb(150, 205, 30), 5))
                    e.Graphics.DrawLine(completedPen, left, centerY, Math.Min(right, completedX), centerY);
            }

            using (Font labelFont = new Font("Segoe UI", 7.5f, FontStyle.Bold))
            using (Brush labelBrush = new SolidBrush(Color.FromArgb(45, 55, 70)))
            {
                for (int i = 0; i < stageCount; i++)
                {
                    float x = left + i * spacing;
                    bool completed = currentStatus == 8 || (currentStatus > 0 && currentStatus < 8 && i < currentStatus - 1);
                    bool current = currentStatus >= 1 && currentStatus <= 7 && i == currentStatus - 1;
                    if (currentStatus == 9 && i == 0)
                        current = true;

                    Color dotColor = completed ? Color.FromArgb(220, 55, 65) :
                                     current ? Color.FromArgb(135, 75, 190) : Color.FromArgb(190, 190, 190);

                    using (Brush dotBrush = new SolidBrush(dotColor))
                    using (Pen ringPen = new Pen(dotColor, 2))
                    {
                        e.Graphics.FillEllipse(dotBrush, x - 4, centerY - 4, 8, 8);
                        e.Graphics.DrawEllipse(ringPen, x - 7, centerY - 7, 14, 14);
                    }

                    SizeF labelSize = e.Graphics.MeasureString(stageNames[i], labelFont);
                    float labelX = Math.Max(0, Math.Min(width - labelSize.Width, x - labelSize.Width / 2));
                    // Labels are centred above their corresponding dots.
                    e.Graphics.DrawString(stageNames[i], labelFont, labelBrush, labelX, 0);
                }
            }

            if (currentStatus == 9)
            {
                using (Font cancelledFont = new Font("Segoe UI", 8f, FontStyle.Bold))
                using (Brush cancelledBrush = new SolidBrush(Color.FromArgb(180, 45, 55)))
                    e.Graphics.DrawString("Withdrawn / Cancelled", cancelledFont, cancelledBrush, Math.Max(0, width - 115), 0);
            }
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            int affectedRows = 0;
            string strValidationMessage = objLogin.ValidateUserRolesAndResponsibilitiesInfo(objTempCurrentlyLoggedInUserInfo.EmpID, "");
            if (strValidationMessage != "Success")
            {
                MessageBox.Show(strValidationMessage, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            if (!ValidateValuesOnUI())
            {
                this.Cursor = Cursors.Default;
                return;
            }

            CalculateGratuityEligibilityAndAmounts();

            // Capture the current UI values and compare them with the snapshot taken
            // after the resignation details were loaded.
            var updatedValues = AuditLogger.getOriginalValues(this);
            var onlyChangedValues = AuditLogger.getUpdatedValues(
                _originalValues ?? new Dictionary<string, object>(),
                updatedValues,
                false);

            strActionStatement = "EmployeeResignationStatusUpdate";

            // Tab-specific database persistence must always be guarded by the matching
            // tab's Enabled property. Existing persistence in this handler is currently
            // implemented for Leave Encashment and Gratuity (guarded below).
            // Reimbursement, Advance and Assets save routines can use these same guards
            // when their respective persistence operations are added to this handler:
            // if (tabReimbursementInformation.Enabled) { ... }
            // if (tabAdvanceInformation.Enabled) { ... }
            // if (tabAssetsInformation.Enabled) { ... }

            if (lblActionMode.Text == "add" && lblRepManagerApprovalStatus.Text == "False")
            {
                affectedRows = objResignationInfo.ApprovalResignationByReportingManager(Convert.ToInt32(lblEmpResignID.Text.ToString()), Convert.ToDateTime(dtApprovalDate.Text.ToString()), Convert.ToInt32(lblRepManID.Text.ToString()), true, cmbResignationStatus.SelectedIndex);
                if (affectedRows > 0)
                    affectedRows = objResignationInfo.UpdateApprovers(Convert.ToInt32(lblEmpResignID.Text.ToString()), Convert.ToInt32(lblRepManID.Text.ToString()), Convert.ToInt32(lblHROwnerID.Text.ToString()));

            }
            else if (lblActionMode.Text == "add" && lblRepManagerApprovalStatus.Text == "True")
            {
                affectedRows = objResignationInfo.ApprovalResignationByHRManager(Convert.ToInt32(lblEmpResignID.Text.ToString()), Convert.ToDateTime(dtApprovalDate.Text.ToString()), Convert.ToInt32(lblRepManID.Text.ToString()), true, cmbResignationStatus.SelectedIndex);
                if (affectedRows > 0)
                    affectedRows = objResignationInfo.UpdateApprovers(Convert.ToInt32(lblEmpResignID.Text.ToString()), Convert.ToInt32(lblRepManID.Text.ToString()), Convert.ToInt32(lblHROwnerID.Text.ToString()));


                // Persist Leave Encashment details only when its workflow tab is enabled
                // for the currently assigned reviewer. The existing insert/update logic is retained.
                if (tabLeaveEncashment.Enabled)
                {
                    if (lblLeaveEncashmentID.Text.ToString() == "")
                    {
                        lblLeaveEncashmentID.Text = objEmpResignLeaveEncashmentInfo.InsertEmpResignLeaveEncashmentInfo(Convert.ToInt32(lblEmpResignID.Text.ToString()), Convert.ToDateTime(dtEncashmentDate.Text), Convert.ToDecimal(txtTotalLeaveAlloted.Text), Convert.ToDecimal(txtTotalLeaveUtilised.Text), Convert.ToDecimal(txtLeaveBalance.Text), Convert.ToDecimal(optEncashmentDays.Value),
                            Convert.ToDecimal(optEncashmentDays.Value), Convert.ToDecimal(txtMonthlyBasicSalary.Text), Convert.ToDecimal(txtDailySalaryRate.Text), "Monthly", Convert.ToDecimal(txtEncashmentAmount.Text),
                            Convert.ToDecimal(txtTaxDeductionOnEncashmentAmount.Text), Convert.ToDecimal(txtNetEncashmentAmount.Text), txtLeaveEncashmentMoreDetails.Text, chkLeaveEncashmentVerifiedAndConfirmed.Checked).ToString();
                    }
                    else
                    {
                        lblLeaveEncashmentID.Text = objEmpResignLeaveEncashmentInfo.UpdateEmpResignLeaveEncashmentInfo(Convert.ToInt32(lblLeaveEncashmentID.Text.ToString()), Convert.ToInt32(lblEmpResignID.Text.ToString()), Convert.ToDateTime(dtEncashmentDate.Text), Convert.ToDecimal(txtTotalLeaveAlloted.Text), Convert.ToDecimal(txtTotalLeaveUtilised.Text), Convert.ToDecimal(txtLeaveBalance.Text), Convert.ToDecimal(optEncashmentDays.Value),
                            Convert.ToDecimal(optEncashmentDays.Value), Convert.ToDecimal(txtMonthlyBasicSalary.Text), Convert.ToDecimal(txtDailySalaryRate.Text), "Monthly", Convert.ToDecimal(txtEncashmentAmount.Text),
                            Convert.ToDecimal(txtTaxDeductionOnEncashmentAmount.Text), Convert.ToDecimal(txtNetEncashmentAmount.Text), txtLeaveEncashmentMoreDetails.Text, chkLeaveEncashmentVerifiedAndConfirmed.Checked).ToString();
                    }
                }

                // Persist Gratuity details only when its workflow tab is enabled
                // for the currently assigned reviewer. The existing calculation and insert/update logic is retained.
                if (tabGratuityInformation.Enabled)
                {
                    // Calculate actual service duration using Date Of Joining and Last Working Date.
                    DateTime gratuityDateOfJoining = Convert.ToDateTime(txtGRTDateOfJoining.Text.ToString());
                    DateTime gratuityLastWorkingDate = Convert.ToDateTime(txtGRTLastWorkingDate.Text.ToString());

                    int actualServiceYears = gratuityLastWorkingDate.Year - gratuityDateOfJoining.Year;
                    if (gratuityDateOfJoining.AddYears(actualServiceYears) > gratuityLastWorkingDate)
                        actualServiceYears--;

                    DateTime serviceDateCursor = gratuityDateOfJoining.AddYears(actualServiceYears);
                    int actualServiceMonths = 0;

                    while (serviceDateCursor.AddMonths(1) <= gratuityLastWorkingDate)
                    {
                        serviceDateCursor = serviceDateCursor.AddMonths(1);
                        actualServiceMonths++;
                    }

                    int actualServiceDays = (gratuityLastWorkingDate - serviceDateCursor).Days;
                    decimal gratuityServiceYears = Convert.ToDecimal(txtGRTEligibleYears.Text.ToString());

                    if (lblEmpResignGratuityID.Text.ToString() == "")
                    {
                        lblEmpResignGratuityID.Text = objEmpResignGratuityInfo.InsertEmpResignResignGratuityInfo(Convert.ToInt32(lblEmpResignID.Text.ToString()),
                            gratuityDateOfJoining, Convert.ToDateTime(txtGRTResignationDate.Text.ToString()), gratuityLastWorkingDate,
                            actualServiceYears, actualServiceMonths, actualServiceDays, gratuityServiceYears, txtGRTEligibleStatus.Text == "Eligible for Gratuity" ? true : false, txtGRTEligibleStatus.Text, cmbGRTCalculationFormula.Text,
                            Convert.ToDecimal(txtGRTLastDrawnBasic.Text), Convert.ToDecimal(txtGRTEligibleAllowance.Text), 0m, Convert.ToDecimal(txtGRTGratuitySalaryBasis.Text), 15, 26,
                            Convert.ToDecimal(txtGRTGorssGratuityAmount.Text), 0m, 0m, Convert.ToDecimal(txtGRTTaxOrDeduction.Text), Convert.ToDecimal(txtGRTNetGratuityAmount.Text),
                            Convert.ToInt32(cmbGRTHRApprovalBy.SelectedValue), Convert.ToDateTime(dtGRTHRApprovalDate.Text.ToString()), cmbGRTHRApprovalStatus.SelectedIndex, txtGRTHRRemarks.Text,
                            Convert.ToInt32(cmbGRTFINApprovalBy.SelectedValue), Convert.ToDateTime(dtGRTFINApprovalDate.Text), cmbGRTFINApprovalStatus.SelectedIndex, txtGRTFINRemarks.Text,
                            cmbGRTPaymentStatus.SelectedIndex, Convert.ToDateTime(dtGRTPaymentDate.Text), txtGRTPaymentRefNo.Text, txtGRTPaymentRemarks.Text).ToString();
                    }
                    else
                    {
                        // Keep the update parameter order identical to the insert method.
                        lblEmpResignGratuityID.Text = objEmpResignGratuityInfo.UpdateEmpResignResignGratuityInfo(Convert.ToInt32(lblEmpResignGratuityID.Text.ToString()), Convert.ToInt32(lblEmpResignID.Text.ToString()),
                            gratuityDateOfJoining, Convert.ToDateTime(txtGRTResignationDate.Text.ToString()), gratuityLastWorkingDate,
                            actualServiceYears, actualServiceMonths, actualServiceDays, gratuityServiceYears, txtGRTEligibleStatus.Text == "Eligible for Gratuity" ? true : false, txtGRTEligibleStatus.Text, cmbGRTCalculationFormula.Text,
                            Convert.ToDecimal(txtGRTLastDrawnBasic.Text), Convert.ToDecimal(txtGRTEligibleAllowance.Text), 0m, Convert.ToDecimal(txtGRTGratuitySalaryBasis.Text), 15, 26,
                            Convert.ToDecimal(txtGRTGorssGratuityAmount.Text), 0m, 0m, Convert.ToDecimal(txtGRTTaxOrDeduction.Text), Convert.ToDecimal(txtGRTNetGratuityAmount.Text),
                            Convert.ToInt32(cmbGRTHRApprovalBy.SelectedValue), Convert.ToDateTime(dtGRTHRApprovalDate.Text.ToString()), cmbGRTHRApprovalStatus.SelectedIndex, txtGRTHRRemarks.Text,
                            Convert.ToInt32(cmbGRTFINApprovalBy.SelectedValue), Convert.ToDateTime(dtGRTFINApprovalDate.Text), cmbGRTFINApprovalStatus.SelectedIndex, txtGRTFINRemarks.Text,
                            cmbGRTPaymentStatus.SelectedIndex, Convert.ToDateTime(dtGRTPaymentDate.Text), txtGRTPaymentRefNo.Text, txtGRTPaymentRemarks.Text).ToString();
                    }
                }

                if (cmbResignationStatus.SelectedIndex == 8)
                {
                    if (chkLeaveEncashmentVerifiedAndConfirmed.Checked == false && Convert.ToDecimal(txtNetEncashmentAmount.Text) > 0)
                    {
                        this.Cursor = Cursors.Default;
                        errValidator.SetError(chkLeaveEncashmentVerifiedAndConfirmed, "Before marking as \"Closed\", complete the Leave Encashment.");
                        return;
                    }

                    objEmployeeMaster.UpdateResginationStatus(Convert.ToInt32(lblEmpID.Text.ToString()));
                }
            }

            // Write audit entries only for values that changed on the UI and only
            // when the resignation approval/update operation succeeded.
            if (affectedRows > 0 && onlyChangedValues != null)
            {
                foreach (var changedValues in onlyChangedValues)
                {
                    objAuditLog.InsertAuditLog(
                        Convert.ToInt32(lblEmpID.Text.ToString()),
                        Convert.ToInt32(lblEmpResignID.Text.ToString()),
                        changedValues.ToString().Trim(),
                        "Update",
                        ModelStaffSync.CurrentUser.EmpName,
                        strActionStatement,
                        Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                }

                MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Send a status-specific review assignment only after the database update succeeds.
            // This is additive; the existing save, gratuity, leave encashment and audit logic remains unchanged.
            if (affectedRows > 0)
            {
                SendResignationReviewAssignmentEmailIfChanged();
            }

            objTempCurrentlyLoggedInUserInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objTempCurrentlyLoggedInUserInfo.EmpID.ToString()));

            onSaveButtonClick();
            disableControls();
            clearControls();
            errValidator.Clear();
            this.Cursor = Cursors.Default;
        }

        private void SendResignationReviewAssignmentEmailIfChanged()
        {
            string currentStatus = cmbResignationStatus.SelectedIndex.ToString();
            string currentApproverEmail = (lblNextApproverMailID.Text ?? string.Empty).Trim();

            bool statusChanged = !string.Equals(_reviewEmailOriginalStatus, currentStatus, StringComparison.Ordinal);
            bool approverChanged = !string.Equals(_reviewEmailOriginalApprover, currentApproverEmail, StringComparison.OrdinalIgnoreCase)
                || !string.Equals(_reviewEmailOriginalApproverID, (lblHROwnerID.Text ?? string.Empty).Trim(), StringComparison.Ordinal);

            // No notification when neither the status nor the assigned approver changed.
            if (!statusChanged && !approverChanged)
                return;

            // Status indexes 2-7 represent active review/verification stages.
            string reviewTitle;
            string reviewInstruction;
            switch (cmbResignationStatus.SelectedIndex)
            {
                case 2:
                    reviewTitle = "HR Formalities and Clearance Review";
                    reviewInstruction = "Please review the resignation request and coordinate the applicable HR formalities, employee clearances and required documentation.";
                    break;
                case 3:
                    reviewTitle = "Reimbursement Review";
                    reviewInstruction = "Please review the employee's reimbursement claims, including pending, approved and unsettled claims, and verify the relevant settlement details.";
                    break;
                case 4:
                    reviewTitle = "Advance Review";
                    reviewInstruction = "Please review the employee's advance transactions, outstanding balances and applicable recovery details.";
                    break;
                case 5:
                    reviewTitle = "Leave Encashment Review";
                    reviewInstruction = "Please review the employee's leave entitlement, leave balance, eligible encashment days and leave encashment calculation.";
                    break;
                case 6:
                    reviewTitle = "Gratuity Review";
                    reviewInstruction = "Please review the employee's gratuity eligibility, service duration, calculation basis, applicable deductions and net gratuity amount.";
                    break;
                case 7:
                    reviewTitle = "Asset Return and Clearance Review";
                    reviewInstruction = "Please review the assets allocated to the employee, confirm returned assets and identify any pending asset clearance.";
                    break;
                default:
                    // In Progress, Completed, Withdrawn/Cancelled and blank are not review assignments.
                    return;
            }

            if (string.IsNullOrWhiteSpace(currentApproverEmail) || !currentApproverEmail.Contains("@"))
                return;

            string employeeName = (txtEmpName.Text ?? string.Empty).Trim();
            string employeeCode = (txtEmpCode.Text ?? string.Empty).Trim();
            string employeeReference = string.IsNullOrWhiteSpace(employeeCode)
                ? employeeName
                : employeeName + " [" + employeeCode + "]";

            string subject = "Action Required – Resignation " + reviewTitle + " – " + employeeReference;
            string html = BuildResignationReviewAssignmentEmailTemplate();

            var templateData = new Dictionary<string, string>
            {
                { "NextApproverName", txtHROwner.Text ?? string.Empty },
                { "EmployeeName", employeeName },
                { "EmployeeCode", employeeCode },
                { "Designation", txtDesignation.Text ?? string.Empty },
                { "Department", txtDepartment.Text ?? string.Empty },
                { "ResignationDate", dtResignationDate.Text ?? string.Empty },
                { "LastWorkingDay", dtLastWorkingDay.Text ?? string.Empty },
                { "ReviewTitle", reviewTitle },
                { "CurrentStatus", cmbResignationStatus.Text ?? string.Empty },
                { "ReviewInstruction", reviewInstruction }
            };

            try
            {
                myMailService mailService = new myMailService();
                mailService.SendMail(currentApproverEmail, subject, html, templateData);
            }
            catch (Exception ex)
            {
                // Email failure must not undo or interrupt the successfully saved resignation update.
                MessageBox.Show("The resignation details were saved successfully, but the review assignment email could not be sent.\n\n" + ex.Message,
                    "StaffSync – Email Notification", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string BuildResignationReviewAssignmentEmailTemplate()
        {
            StringBuilder html = new StringBuilder();
            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html><head><meta charset=\"utf-8\" />");
            html.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />");
            html.AppendLine("<title>Resignation Review Assignment</title>");
            html.AppendLine("<style>");
            html.AppendLine("body{font-family:Segoe UI,Arial,sans-serif;background:#edf1f7;margin:0;padding:24px;color:#202124;}");
            html.AppendLine(".mail-container{max-width:700px;margin:0 auto;background:#fff;border:1px solid #d9dee7;border-radius:10px;overflow:hidden;}");
            html.AppendLine(".mail-header{background:#245b85;color:#fff;padding:20px 26px;font-size:21px;font-weight:600;}");
            html.AppendLine(".mail-body{padding:24px 26px;font-size:14px;line-height:1.65;}");
            html.AppendLine(".details{width:100%;border-collapse:collapse;margin:16px 0;font-size:13px;}");
            html.AppendLine(".details th{background:#f0f4f8;color:#34495e;text-align:left;width:34%;}");
            html.AppendLine(".details th,.details td{border:1px solid #d9e1ea;padding:9px 11px;vertical-align:top;}");
            html.AppendLine(".action{margin:18px 0;padding:13px 16px;background:#fff7e6;border-left:4px solid #e0a12b;color:#7a4b00;font-weight:700;}");
            html.AppendLine(".mail-footer{background:#f8fafc;border-top:1px solid #e3e7ed;padding:14px 26px;color:#667085;font-size:11px;}");
            html.AppendLine("</style></head><body>");
            html.AppendLine("<div class=\"mail-container\"><div class=\"mail-header\">StaffSync – Resignation Management</div>");
            html.AppendLine("<div class=\"mail-body\">Dear {{NextApproverName}},<br/><br/>");
            html.AppendLine("I hope you are doing well.<br/><br/>");
            html.AppendLine("A resignation request has been assigned to you for review and verification. Please refer to the details below and take the appropriate action in StaffSync.");
            html.AppendLine("<table class=\"details\"><tr><th colspan=\"2\">Resignation Reference</th></tr>");
            html.AppendLine("<tr><th>Employee Name</th><td>{{EmployeeName}} [{{EmployeeCode}}]</td></tr>");
            html.AppendLine("<tr><th>Designation</th><td>{{Designation}}</td></tr>");
            html.AppendLine("<tr><th>Department</th><td>{{Department}}</td></tr>");
            html.AppendLine("<tr><th>Resignation Date</th><td>{{ResignationDate}}</td></tr>");
            html.AppendLine("<tr><th>Last Working Day</th><td>{{LastWorkingDay}}</td></tr></table>");
            html.AppendLine("<div class=\"action\">ACTION REQUIRED: TO REVIEW – {{ReviewTitle}}</div>");
            html.AppendLine("<p><strong>Current Status:</strong> {{CurrentStatus}}</p>");
            html.AppendLine("<p>{{ReviewInstruction}}</p>");
            html.AppendLine("<p>Please record your review/confirmation in StaffSync. Once your part is completed, update the resignation status and assign it to the next appropriate reviewer, where applicable.</p>");
            html.AppendLine("Your timely attention will help ensure a smooth and coordinated Full &amp; Final Settlement process.");
            html.AppendLine("<br/><br/>Thank you for your support and cooperation.<br/><br/>Warm regards,<br/><strong>StaffSync – Resignation Management</strong>");
            html.AppendLine("</div><div class=\"mail-footer\">This is an automatically generated notification from StaffSync.</div></div></body></html>");
            return html.ToString();
        }

        private bool TryGetDecimal(string value, out decimal result)
        {
            return decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out result)
                || decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out result);
        }

        private bool ValidateNonNegativeDecimal(Control control, string fieldName)
        {
            decimal value;
            if (!TryGetDecimal(control.Text, out value))
            {
                errValidator.SetError(control, fieldName + " must contain a valid numeric value.");
                return false;
            }

            if (value < 0m)
            {
                errValidator.SetError(control, fieldName + " cannot be negative.");
                return false;
            }

            errValidator.SetError(control, "");
            return true;
        }

        private bool TryParseStaffSyncDate(string value, out DateTime parsedDate)
        {
            string[] formats = { "dd-MMM-yyyy", "dd-MM-yyyy", "dd/MM/yyyy", "d-M-yyyy", "d/M/yyyy" };

            return DateTime.TryParseExact(value.Trim(), formats, CultureInfo.InvariantCulture,
                       DateTimeStyles.None, out parsedDate)
                   || DateTime.TryParse(value, CultureInfo.CurrentCulture,
                       DateTimeStyles.None, out parsedDate);
        }

        private bool ValidateGratuityApprovalDate(Control dateControl, DateTime resignationDate, string fieldName)
        {
            DateTime enteredDate;

            if (string.IsNullOrWhiteSpace(dateControl.Text) ||
                !TryParseStaffSyncDate(dateControl.Text, out enteredDate))
            {
                errValidator.SetError(dateControl, fieldName + " is required and must be a valid date.");
                return false;
            }

            if (enteredDate.Date > DateTime.Today)
            {
                errValidator.SetError(dateControl, fieldName + " cannot be a future date.");
                return false;
            }

            if (enteredDate.Date < resignationDate.Date)
            {
                errValidator.SetError(dateControl, fieldName + " cannot be earlier than the Resignation Date.");
                return false;
            }

            errValidator.SetError(dateControl, "");
            return true;
        }

        private bool ComboSelectionStartsWith(KryptonComboBox comboBox, string expectedValue)
        {
            if (comboBox == null || comboBox.SelectedItem == null)
                return false;

            return comboBox.SelectedItem.ToString().StartsWith(expectedValue, StringComparison.OrdinalIgnoreCase);
        }

        private bool ValidateValuesOnUI()
        {
            bool validationStatus = true;
            errValidator.Clear();

            if (lblEmpResignID.Text == "")
            {
                validationStatus = false;
                errValidator.SetError(this.btnResignedRequestSearch, "Please select resigned employee to continue");
            }


            bool gratuityEligible = string.Equals(
                txtGRTEligibleStatus.Text.Trim(),
                "Eligible for Gratuity",
                StringComparison.OrdinalIgnoreCase);

            // Validate all gratuity numeric inputs when gratuity is applicable.
            if (gratuityEligible)
            {
                validationStatus &= ValidateNonNegativeDecimal(txtGRTEligibleYears, "Eligible Years");
                validationStatus &= ValidateNonNegativeDecimal(txtGRTLastDrawnBasic, "Last Basic Salary");
                validationStatus &= ValidateNonNegativeDecimal(txtGRTEligibleAllowance, "Eligible Allowances");
                validationStatus &= ValidateNonNegativeDecimal(txtGRTGratuitySalaryBasis, "Gratuity Salary Basis");
                validationStatus &= ValidateNonNegativeDecimal(txtGRTGorssGratuityAmount, "Gross Gratuity Amount");
                validationStatus &= ValidateNonNegativeDecimal(txtGRTTaxOrDeduction, "Tax / Deductions");
                validationStatus &= ValidateNonNegativeDecimal(txtGRTNetGratuityAmount, "Net Gratuity Amount");

                // Check that the calculated fields are consistent with their source values.
                decimal basicSalary, eligibleAllowances, salaryBasis, eligibleYears;
                decimal grossAmount, taxOrDeduction, netAmount;

                if (TryGetDecimal(txtGRTLastDrawnBasic.Text, out basicSalary) &&
                    TryGetDecimal(txtGRTEligibleAllowance.Text, out eligibleAllowances) &&
                    TryGetDecimal(txtGRTGratuitySalaryBasis.Text, out salaryBasis) &&
                    TryGetDecimal(txtGRTEligibleYears.Text, out eligibleYears) &&
                    TryGetDecimal(txtGRTGorssGratuityAmount.Text, out grossAmount) &&
                    TryGetDecimal(txtGRTTaxOrDeduction.Text, out taxOrDeduction) &&
                    TryGetDecimal(txtGRTNetGratuityAmount.Text, out netAmount))
                {
                    //if (Math.Abs(salaryBasis - (basicSalary + eligibleAllowances)) > 0.01m)
                    //{
                    //    errValidator.SetError(txtGRTGratuitySalaryBasis,
                    //        "Gratuity Salary Basis must equal Last Basic Salary + Eligible Allowances.");
                    //    validationStatus = false;
                    //}

                    //decimal expectedGrossAmount = (salaryBasis * 15m / 26m) * eligibleYears;
                    //if (Math.Abs(grossAmount - expectedGrossAmount) > 0.01m)
                    //{
                    //    errValidator.SetError(txtGRTGorssGratuityAmount,
                    //        "Gross Gratuity Amount must equal (Gratuity Salary Basis × 15 ÷ 26) × Eligible Years.");
                    //    validationStatus = false;
                    //}

                    //if (Math.Abs(netAmount - (grossAmount - taxOrDeduction)) > 0.01m)
                    //{
                    //    errValidator.SetError(txtGRTNetGratuityAmount,
                    //        "Net Gratuity Amount must equal Gross Gratuity Amount − Tax / Deductions.");
                    //    validationStatus = false;
                    //}

                    if (netAmount <= 0m)
                    {
                        errValidator.SetError(txtGRTNetGratuityAmount,
                            "Net Gratuity Amount must be greater than zero.");
                        validationStatus = false;
                    }

                    if (netAmount > 2000000m)
                    {
                        errValidator.SetError(txtGRTNetGratuityAmount,
                            "Net Gratuity Amount cannot exceed ₹20,00,000.");
                        validationStatus = false;
                    }
                }

                DateTime resignationDate;
                if (!TryParseStaffSyncDate(txtGRTResignationDate.Text, out resignationDate))
                {
                    errValidator.SetError(txtGRTResignationDate, "Please enter a valid Resignation Date.");
                    validationStatus = false;
                }
                else
                {
                    validationStatus &= ValidateGratuityApprovalDate(
                        dtGRTHRApprovalDate, resignationDate, "HR Verification Date");
                    validationStatus &= ValidateGratuityApprovalDate(
                        dtGRTFINApprovalDate, resignationDate, "Finance Verification Date");
                    validationStatus &= ValidateGratuityApprovalDate(
                        dtGRTPaymentDate, resignationDate, "Payment Date");
                }
            }

            // A resignation can be marked Completed only after gratuity is fully
            // verified and paid. This is in addition to existing pending-item checks.
            if (cmbResignationStatus.Text.IndexOf("Completed", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                decimal completedNetAmount;
                if (!TryGetDecimal(txtGRTNetGratuityAmount.Text, out completedNetAmount) ||
                    completedNetAmount <= 0m)
                {
                    errValidator.SetError(txtGRTNetGratuityAmount,
                        "Net Gratuity Amount must be greater than zero before marking the resignation as Completed.");
                    errValidator.SetError(cmbResignationStatus,
                        "Cannot mark resignation as Completed until Net Gratuity Amount is greater than zero.");
                    validationStatus = false;
                }

                if (!ComboSelectionStartsWith(cmbGRTHRApprovalStatus, "3 - Verified"))
                {
                    errValidator.SetError(cmbGRTHRApprovalStatus,
                        "HR Verification Status must be '3 - Verified' before marking the resignation as Completed.");
                    validationStatus = false;
                }

                if (!ComboSelectionStartsWith(cmbGRTFINApprovalStatus, "3 - Verified"))
                {
                    errValidator.SetError(cmbGRTFINApprovalStatus,
                        "Finance Verification Status must be '3 - Verified' before marking the resignation as Completed.");
                    validationStatus = false;
                }

                if (!ComboSelectionStartsWith(cmbGRTPaymentStatus, "3 - Paid"))
                {
                    errValidator.SetError(cmbGRTPaymentStatus,
                        "Payment Status must be '3 - Paid' before marking the resignation as Completed.");
                    validationStatus = false;
                }
            }

            // Gratuity validation: the net amount must not exceed the statutory/company cap.
            if (string.Equals(txtGRTEligibleStatus.Text.Trim(), "Eligible for Gratuity", StringComparison.OrdinalIgnoreCase))
            {
                decimal netGratuityAmount;
                if (!decimal.TryParse(txtGRTNetGratuityAmount.Text, NumberStyles.Number, CultureInfo.CurrentCulture, out netGratuityAmount))
                {
                    errValidator.SetError(txtGRTNetGratuityAmount, "Please enter a valid Net Gratuity Amount.");
                    validationStatus = false;
                }
                else if (netGratuityAmount < 0)
                {
                    errValidator.SetError(txtGRTNetGratuityAmount, "Net Gratuity Amount cannot be negative.");
                    validationStatus = false;
                }
                else if (netGratuityAmount > 2000000m)
                {
                    errValidator.SetError(txtGRTNetGratuityAmount, "Net Gratuity Amount cannot exceed ₹20,00,000.");
                    validationStatus = false;
                }
                else
                {
                    errValidator.SetError(txtGRTNetGratuityAmount, "");
                }
            }

            // Helper for date validation
            if (lblEmpResignID.Text.Trim() == "")
            {
                errValidator.SetError(txtEmpCode, "Please select a specific Resignation Information to continue.");
                validationStatus = false;
            }
            else
            {
                errValidator.SetError(txtEmpCode, "");
            }

            if (string.IsNullOrEmpty(dtApprovalDate.Text))
            {
                validationStatus = false;
                errValidator.SetError(this.dtApprovalDate, dtApprovalDate.Tag?.ToString() ?? "Date of Approval is required.");
            }
            else if (!DateTime.TryParseExact(dtApprovalDate.Text, dateFormat, provider, DateTimeStyles.None, out dos))
            {
                validationStatus = false;
                errValidator.SetError(this.dtApprovalDate, "Invalid Date of Approval format (dd-MM-yyyy).");
            }
            else if (dos < DateTime.Now.Date)
            {
                validationStatus = false;
                errValidator.SetError(this.dtApprovalDate, "Date of Approval cannot be in the past.");
            }
            else if (dos > DateTime.Now.Date)
            {
                validationStatus = false;
                errValidator.SetError(this.dtApprovalDate, "Date of Approval cannot be in the future.");
            }
            else
            {
                errValidator.SetError(this.dtApprovalDate, "");
            }

            if (txtResignationMoreDetails.Text.Trim() == "")
            {
                txtResignationMoreDetails.Text = "N/A";
            }

            if (cmbResignationStatus.SelectedIndex == 6)
            {
                if (dtgReimbursmentPaymentHistory.Rows.Count > 0)
                {
                    validationStatus = false;
                    errValidator.SetError(this.cmbResignationStatus, "Employee has pending reimbursement requests. Cannot mark resignation as completed.");
                }
                else
                {
                    errValidator.SetError(this.cmbResignationStatus, "");
                }
                if (dtgAdvanceList.Rows.Count > 0)
                {
                    validationStatus = false;
                    errValidator.SetError(this.cmbResignationStatus, "Employee has pending advance transactions. Cannot mark resignation as completed.");
                }
                else
                {
                    errValidator.SetError(this.cmbResignationStatus, "");
                }
            }

            string maxAllowedLeaveChecker = "";
            foreach (DataGridViewRow row in dtgLeaveBalanceInfo.Rows)
            {
                if (row.IsNewRow || !row.Visible)
                    continue;

                if (Convert.ToBoolean(row.Cells["IsEncashmentAllowed"].Value) == true)
                {
                    if (Convert.ToDecimal(row.Cells["BalanceLeaves"].Value) > Convert.ToDecimal(row.Cells["MaxLeaveDaysAllowedToEncash"].Value))
                    {
                        if (maxAllowedLeaveChecker == "")
                            maxAllowedLeaveChecker = row.Cells["LeaveTypeTitle"].Value.ToString() + " - Available Leave Balance : " + Convert.ToDecimal(row.Cells["BalanceLeaves"].Value) + " exceeds the Maximum allowed leaves : " + Convert.ToDecimal(row.Cells["MaxLeaveDaysAllowedToEncash"].Value) + ".";
                        else
                            maxAllowedLeaveChecker = "\n" + row.Cells["LeaveTypeTitle"].Value.ToString() + " - Available Leave Balance : " + Convert.ToDecimal(row.Cells["BalanceLeaves"].Value) + " exceeds the Maximum allowed leaves : " + Convert.ToDecimal(row.Cells["MaxLeaveDaysAllowedToEncash"].Value) + ".";

                    }
                }
            }

            if (lblLeaveEncashmentID.Text.ToString().Trim() == "")
            {
                if (MessageBox.Show(maxAllowedLeaveChecker + "\nWould you like to continue with Maximum allowed number of days.?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    validationStatus = false;
                    errValidator.SetError(this.dtgLeaveBalanceInfo, maxAllowedLeaveChecker);
                }
            }

            return validationStatus;
        }

        private void frmEmployeeResignationManagement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                //if (lblActionMode.Text != "")
                //{
                //    if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                //    {
                //        return;
                //    }
                //}
                objDashboard.lblDashboardTitle.Text = "Dashboard";
                objDashboard.sptrDashboardContainer.Visible = true;
                objDashboard.grpDashboardDateRange.Visible = true;
                objDashboard.refreshDashboardCharts();
                this.Close();
            }
            else if (e.Control && e.KeyCode == Keys.N)
            {
                e.SuppressKeyPress = true;
                btnGenerateDetails_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                e.SuppressKeyPress = true;
                //btnModifyDetails_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                e.SuppressKeyPress = true;
                //btnRemoveDetails_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                e.SuppressKeyPress = true;
                btnSaveDetails_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                e.SuppressKeyPress = true;
                btnCancel_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.Z)
            {
                e.SuppressKeyPress = true;
                btnCloseMe_Click(sender, e);
            }
        }

        private void btnEmpSearch_Click(object sender, EventArgs e)
        {
            //frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "EmployeeListToResign");
            //frmEmployeeList.ShowDialog();
        }

        private void btnResignedRequestSearch_Click(object sender, EventArgs e)
        {
            frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listResignedEmployeesList", Convert.ToInt32(lblClientID.Text.ToString()));
            frmEmployeeList.ShowDialog(this);
        }

        public void SelectedEmployeeID(string SearchOptionSelectedForm, int selectedResignationID)
        {
            if (SearchOptionSelectedForm == "listResignedEmployeesList")
            {
                EmployeeSpecificResignationInfo objEmployeeSpecificResignationInfo = objResignationInfo.getEmployeeSpecificEmployeeSpecificResignationInfo(selectedResignationID);
                lblEmpResignID.Text = objEmployeeSpecificResignationInfo.EmpResignID.ToString();
                lblEmpID.Text = objEmployeeSpecificResignationInfo.EmpID.ToString();
                lblPersonalInfoID.Text = objEmployeeSpecificResignationInfo.PersonalInfoID.ToString();
                lblRepManID.Text = objEmployeeSpecificResignationInfo.EmpResignApprovedBy1.ToString();
                lblHROwnerID.Text = objEmployeeSpecificResignationInfo.EmpResignApprovedBy2.ToString();
                txtResignationCode.Text = objEmployeeSpecificResignationInfo.EmpResignCode;
                dtResignationDate.Text = objEmployeeSpecificResignationInfo.EmpResignDate.ToString("dd-MM-yyyy");
                txtResignationMoreDetails.Text = objEmployeeSpecificResignationInfo.EmpResignMoreInfo;
                cmbResignationStatus.SelectedIndex = objEmployeeSpecificResignationInfo.EmpResignStatus;
                lblRepManagerApprovalStatus.Text = objEmployeeSpecificResignationInfo.EmpResignApprovalStatus1.ToString();

                if (objEmployeeSpecificResignationInfo.EmpResignApprovalStatus1 == false)
                {
                    txtNoticePeriod.Text = "0";
                    txtNoticePeriod.Enabled = false;
                    dtLastWorkingDay.Enabled = false;
                    dtApprovalDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
                }
                if (objEmployeeSpecificResignationInfo.EmpResignApprovalStatus2 == false)
                {
                    txtNoticePeriod.Text = "30";
                    txtNoticePeriod.Enabled = true;
                    dtLastWorkingDay.Enabled = true;
                    dtApprovalDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
                }
                if (objEmployeeSpecificResignationInfo.EmpResignApprovalStatus2 == true)
                {
                    txtNoticePeriod.Text = "30";
                    txtNoticePeriod.Enabled = true;
                    dtLastWorkingDay.Enabled = true;
                    dtApprovalDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
                }

                ReportingManagerInfo objEmpMaster = objEmployeeMaster.GetReportingManagerInfo(Convert.ToInt16(lblEmpID.Text.ToString()));
                txtEmpCode.Text = objEmpMaster.EmpCode;
                txtEmpName.Text = objEmpMaster.EmpName;
                txtDesignation.Text = objEmpMaster.DesignationTitle;
                txtDepartment.Text = objEmpMaster.DepartmentTitle;
                picEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(lblEmpID.Text.ToString())).EmpPhoto);

                //EmployeeInfo objSelectedEmployeeInfo = objEmployeeMaster.GetSelectedEmployeeInfo(Convert.ToInt16(lblRepManID.Text.ToString()));
                //lblRepManID.Text = objSelectedEmployeeInfo.EmpRepManID.ToString();

                ReportingManagerInfo objReportingManagerInfo = objEmployeeMaster.GetReportingManagerInfo(Convert.ToInt16(lblRepManID.Text));
                txtReportingManager.Text = objReportingManagerInfo.EmpName;

                ReportingManagerInfo objHROwnerInfo = objEmployeeMaster.GetReportingManagerInfo(Convert.ToInt16(lblHROwnerID.Text));
                txtHROwner.Text = objHROwnerInfo.EmpName;
                lblNextApproverMailID.Text = objHROwnerInfo.ContactNumber2.ToString();

                lnkViewAuditLog.Visible = true;

                lblLeaveEncashmentID.Text = "";

                cmbGRTHRApprovalBy.DataSource = objDepartmentInfo.qryHREmployeeList(Convert.ToInt32(lblClientID.Text));
                cmbGRTHRApprovalBy.DisplayMember = "EmpName";
                cmbGRTHRApprovalBy.ValueMember = "EmpID";

                cmbGRTFINApprovalBy.DataSource = objDepartmentInfo.qryFINEmployeeList(Convert.ToInt32(lblClientID.Text));
                cmbGRTFINApprovalBy.DisplayMember = "EmpName";
                cmbGRTFINApprovalBy.ValueMember = "EmpID";

                cmbGRTCalculationFormula.DataSource = null;
                cmbGRTCalculationFormula.Items.Clear();
                cmbGRTCalculationFormula.Items.Add("(Last Drawn Basic * 15 / 26) * Completed Years");
                cmbGRTCalculationFormula.SelectedIndex = 0;

                EmpPersonalPersonalInfo objSelectedPersonalInfo = objEmployeePersonalInfo.GetEmpPersonalPersonalInfo(Convert.ToInt16(lblEmpID.Text));
                txtDateOfJoining.Text = objSelectedPersonalInfo.DOJ.ToString("dd-MMM-yyyy");
                dtLastWorkingDay.Text = DateTime.Now.AddDays(Convert.ToInt32(txtNoticePeriod.Text)).ToString("dd-MM-yyyy");
                txtServiceTenure.Text = CalculateServiceTenure(objSelectedPersonalInfo.DOJ);
                txtServiceTenure.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtServiceTenure.Text.ToString().ToLower());

                txtGRTDateOfJoining.Text = objSelectedPersonalInfo.DOJ.ToString("dd-MMM-yyyy");
                txtGRTLastWorkingDate.Text = Convert.ToDateTime(dtLastWorkingDay.Text).ToString("dd-MMM-yyy");
                txtGRTResignationDate.Text = Convert.ToDateTime(dtResignationDate.Text).ToString("dd-MMM-yyyy");
                txtGRTServiceTenure.Text = txtServiceTenure.Text;

                LoadGratuityVerificationStatus();
                LoadReimbursementInfo();
                LoadEmployeeSpecificAdvanceInfo();
                LoadEmployeeSpecificAssetInfo();
                LoadEmployeeSpecificLeaveBalanceInfo();
                LoadEmployeeLeaveEncashmentInfo();
                LoadEmployeeGratuityInfo();

                CalculateGratuityEligibilityAndAmounts();

                dtEncashmentDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
                tabControl1.Enabled = true;
                tabControl1.SelectedIndex = 0;

                // Apply tab access from the persisted workflow status for this reviewer.
                _loadedResignationWorkflowStatus = objEmployeeSpecificResignationInfo.EmpResignStatus;
                ApplyResignationWorkflowTabAccess();
                UpdateResignationWorkflowProgress();

                // Keep the baseline after every resignation-related section is loaded.
                _originalValues = AuditLogger.getOriginalValues(this);

                // Preserve the initially loaded review assignment for change detection on Save.
                _reviewEmailOriginalStatus = cmbResignationStatus.SelectedIndex.ToString();
                _reviewEmailOriginalApprover = (lblNextApproverMailID.Text ?? string.Empty).Trim();
                _reviewEmailOriginalApproverID = (lblHROwnerID.Text ?? string.Empty).Trim();
            }
            else if (SearchOptionSelectedForm == "listResignationApproverList")
            {
                if (lblRepManID.Text == selectedResignationID.ToString())
                {
                    MessageBox.Show("Approver cannot be same as Resignee.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                lblRepManID.Text = selectedResignationID.ToString();
                ReportingManagerInfo objReportingManagerInfo = objEmployeeMaster.GetReportingManagerInfo(Convert.ToInt16(lblRepManID.Text));
                txtReportingManager.Text = objReportingManagerInfo.EmpName;
            }
            else if (SearchOptionSelectedForm == "listResignationApproverHRList")
            {
                if (lblHROwnerID.Text == selectedResignationID.ToString())
                {
                    MessageBox.Show("HR Owner cannot be same as Resignee.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                lblHROwnerID.Text = selectedResignationID.ToString();
                ReportingManagerInfo objHROwnerInfo = objEmployeeMaster.GetReportingManagerInfo(Convert.ToInt16(lblHROwnerID.Text));
                txtHROwner.Text = objHROwnerInfo.EmpName;
                lblNextApproverMailID.Text = objHROwnerInfo.ContactNumber2;
            }
        }

        private void LoadResignationFinalStatus()
        {
            cmbResignationStatus.Items.Clear();
            cmbResignationStatus.Items.Add("");
            cmbResignationStatus.Items.Add("1 - In Progress");
            cmbResignationStatus.Items.Add("2 - Approved and Transfered to HR");
            cmbResignationStatus.Items.Add("3 - With Finance (Reimbursements)");
            cmbResignationStatus.Items.Add("4 - With Finance (Advances)");
            cmbResignationStatus.Items.Add("5 - With Finance (Leave Encashments)");
            cmbResignationStatus.Items.Add("6 - With Finance (Gratuity)");
            cmbResignationStatus.Items.Add("7 - With IT (Assets)");
            cmbResignationStatus.Items.Add("8 - Completed");
            cmbResignationStatus.Items.Add("9 - Withdrawn/Cancelled");
            cmbResignationStatus.SelectedIndex = 0;
        }

        private void LoadGratuityVerificationStatus()
        {
            cmbGRTHRApprovalStatus.Items.Clear();
            cmbGRTHRApprovalStatus.Items.Add("");
            cmbGRTHRApprovalStatus.Items.Add("1 - Pending");
            cmbGRTHRApprovalStatus.Items.Add("2 - Processing");
            cmbGRTHRApprovalStatus.Items.Add("3 - Verified");
            cmbGRTHRApprovalStatus.Items.Add("4 - Rejected");
            cmbGRTHRApprovalStatus.Items.Add("5 - Not Applicable");
            cmbGRTHRApprovalStatus.SelectedIndex = 5;

            cmbGRTFINApprovalStatus.Items.Clear();
            cmbGRTFINApprovalStatus.Items.Add("");
            cmbGRTFINApprovalStatus.Items.Add("1 - Pending");
            cmbGRTFINApprovalStatus.Items.Add("2 - Processing");
            cmbGRTFINApprovalStatus.Items.Add("3 - Verified");
            cmbGRTFINApprovalStatus.Items.Add("4 - Rejected");
            cmbGRTFINApprovalStatus.Items.Add("5 - Not Applicable");
            cmbGRTFINApprovalStatus.SelectedIndex = 5;

            cmbGRTPaymentStatus.Items.Clear();
            cmbGRTPaymentStatus.Items.Add("");
            cmbGRTPaymentStatus.Items.Add("1 - Pending");
            cmbGRTPaymentStatus.Items.Add("2 - Processing");
            cmbGRTPaymentStatus.Items.Add("3 - Paid");
            cmbGRTPaymentStatus.Items.Add("4 - Failed");
            cmbGRTPaymentStatus.Items.Add("5 - Not Applicable");
            cmbGRTPaymentStatus.SelectedIndex = 5;
        }

        /// <summary>
        /// Calculates gratuity eligibility and the related amounts using the
        /// Date of Joining and Resignation Date displayed in the Gratuity tab.
        /// Eligibility requires service strictly greater than 4 years and 7 months.
        /// Completed years are rounded up only when the remaining months exceed 6.
        /// </summary>
        private void RegisterGratuityCalculationEvents()
        {
            // Recalculate dependent gratuity values whenever an input changes.
            txtGRTLastDrawnBasic.TextChanged += GratuityInput_TextChanged;
            txtGRTEligibleAllowance.TextChanged += GratuityInput_TextChanged;
            txtGRTTaxOrDeduction.TextChanged += GratuityInput_TextChanged;
            txtGRTEligibleYears.TextChanged += GratuityInput_TextChanged;
            txtGRTDateOfJoining.TextChanged += GratuityInput_TextChanged;
            txtGRTResignationDate.TextChanged += GratuityInput_TextChanged;
        }

        private void GratuityInput_TextChanged(object sender, EventArgs e)
        {
            if (_isUpdatingGratuityValues)
                return;

            CalculateGratuityEligibilityAndAmounts();
        }

        private decimal GetGratuityDecimalValue(string value)
        {
            decimal result;
            if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.CurrentCulture, out result))
                return result;

            if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out result))
                return result;

            return 0m;
        }

        /// <summary>
        /// Calculates gratuity eligibility and all dependent amounts.
        /// Salary Basis = Last Basic Salary + Eligible Allowances.
        /// Gross = Salary Basis * Eligible Years.
        /// Net = Gross - Tax/Deductions.
        /// </summary>
        private void CalculateGratuityEligibilityAndAmounts()
        {
            if (_isUpdatingGratuityValues)
                return;

            _isUpdatingGratuityValues = true;

            try
            {
                DateTime joiningDate;
                DateTime resignationDate;

                bool validJoiningDate =
                    DateTime.TryParseExact(txtGRTDateOfJoining.Text.Trim(),
                        new[] { "dd-MMM-yyyy", "dd-MM-yyyy", "dd/MM/yyyy" },
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out joiningDate)
                    || DateTime.TryParse(txtGRTDateOfJoining.Text, CultureInfo.CurrentCulture,
                        DateTimeStyles.None, out joiningDate);

                bool validResignationDate =
                    DateTime.TryParseExact(txtGRTResignationDate.Text.Trim(),
                        new[] { "dd-MMM-yyyy", "dd-MM-yyyy", "dd/MM/yyyy" },
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out resignationDate)
                    || DateTime.TryParse(txtGRTResignationDate.Text, CultureInfo.CurrentCulture,
                        DateTimeStyles.None, out resignationDate);

                bool validServiceDates = validJoiningDate && validResignationDate &&
                                         resignationDate.Date >= joiningDate.Date;

                int eligibleYears = 0;
                bool isEligible = false;

                if (validServiceDates)
                {
                    // Eligibility requires service strictly greater than 4 years and 7 months.
                    DateTime eligibilityCutoff = joiningDate.Date.AddYears(4).AddMonths(7);
                    isEligible = resignationDate.Date > eligibilityCutoff;

                    int completedYears = resignationDate.Year - joiningDate.Year;
                    DateTime anniversary = joiningDate.Date.AddYears(completedYears);

                    if (anniversary > resignationDate.Date)
                    {
                        completedYears--;
                        anniversary = joiningDate.Date.AddYears(completedYears);
                    }

                    int remainingMonths = (resignationDate.Year - anniversary.Year) * 12
                                          + resignationDate.Month - anniversary.Month;

                    if (anniversary.AddMonths(remainingMonths) > resignationDate.Date)
                        remainingMonths--;

                    eligibleYears = completedYears + (remainingMonths > 6 ? 1 : 0);
                    if (eligibleYears < 0)
                        eligibleYears = 0;
                }

                txtGRTEligibleYears.Text = eligibleYears.ToString(CultureInfo.InvariantCulture);
                txtGRTEligibleStatus.Text = isEligible
                    ? "Eligible for Gratuity"
                    : "Not eligible for Gratuity";

                SetGratuitySectionsEnabled(isEligible);

                // Recalculate the complete dependency chain on every relevant input change.
                decimal lastBasicSalary = GetGratuityDecimalValue(txtGRTLastDrawnBasic.Text);
                decimal eligibleAllowances = GetGratuityDecimalValue(txtGRTEligibleAllowance.Text);
                decimal taxOrDeduction = GetGratuityDecimalValue(txtGRTTaxOrDeduction.Text);

                decimal salaryBasis = lastBasicSalary + eligibleAllowances;
                txtGRTGratuitySalaryBasis.Text = salaryBasis.ToString("0.00", CultureInfo.CurrentCulture);

                decimal grossAmount = 0m;
                decimal netAmount = 0m;

                if (isEligible)
                {
                    // Formula: (Gratuity Salary Basis * 15 / 26) * Eligible Years.
                    grossAmount = (salaryBasis * 15m / 26m) * eligibleYears;
                    // Net Gratuity Amount = Gross Gratuity Amount - Tax / Deductions.
                    netAmount = grossAmount - taxOrDeduction;
                }

                txtGRTGorssGratuityAmount.Text = grossAmount.ToString("0.00", CultureInfo.CurrentCulture);
                txtGRTGorssGratuityAmount.Text = DBCommon.RoundOffDecimal(Convert.ToDecimal(txtGRTGorssGratuityAmount.Text.ToString())).ToString("0.00");
                txtGRTNetGratuityAmount.Text = netAmount.ToString("0.00", CultureInfo.CurrentCulture);
                txtGRTNetGratuityAmount.Text = DBCommon.RoundOffDecimal(Convert.ToDecimal(txtGRTNetGratuityAmount.Text.ToString())).ToString("0.00");
            }
            finally
            {
                _isUpdatingGratuityValues = false;
            }
        }

        private void SetGratuitySectionsEnabled(bool enabled)
        {
            grpGratuityCalculation.Enabled = enabled;
            grpHRApproval.Enabled = enabled;
            grpFinanceApproval.Enabled = enabled;
            grpPaymentInformation.Enabled = enabled;
        }

        private string CalculateServiceTenure(DateTime dateOfJoining)
        {
            DateTime today = DateTime.Today;

            if (dateOfJoining.Date > today)
                return "Invalid Date of Joining";

            int years = today.Year - dateOfJoining.Year;
            int months = today.Month - dateOfJoining.Month;
            int days = today.Day - dateOfJoining.Day;

            if (days < 0)
            {
                months--;

                DateTime previousMonth = today.AddMonths(-1);
                days += DateTime.DaysInMonth(
                    previousMonth.Year,
                    previousMonth.Month);
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            return $"{years} Years {months} Months {days} Days";
        }

        private void LoadReimbursementInfo()
        {
            dtgReimbursmentPaymentHistory.DataSource = null;
            dtgReimbursmentPaymentHistory.DataSource = objEmpReimbursRequestInfo.getEmpSpecificReimbursmentPaymentInfo(Convert.ToInt16(lblEmpID.Text.ToString())); //objEmpReimbursRequestInfo.GetReimbursmentPaymentPendingList(Convert.ToInt16(lblClientID.Text.ToString()));
            dtgReimbursmentPaymentHistory.Columns["EmpID"].Visible = false;
            dtgReimbursmentPaymentHistory.Columns["EmpCode"].HeaderText = "Emp Code";
            dtgReimbursmentPaymentHistory.Columns["EmpCode"].Width = 100;
            dtgReimbursmentPaymentHistory.Columns["EmpCode"].ReadOnly = true;
            dtgReimbursmentPaymentHistory.Columns["EmpCode"].Visible = false;
            dtgReimbursmentPaymentHistory.Columns["EmpName"].HeaderText = "Employee Name";
            dtgReimbursmentPaymentHistory.Columns["EmpName"].Width = 200;
            dtgReimbursmentPaymentHistory.Columns["EmpName"].ReadOnly = true;
            dtgReimbursmentPaymentHistory.Columns["EmpName"].Visible = false;
            dtgReimbursmentPaymentHistory.Columns["DesignationTitle"].HeaderText = "Designation";
            dtgReimbursmentPaymentHistory.Columns["DesignationTitle"].Width = 150;
            dtgReimbursmentPaymentHistory.Columns["DesignationTitle"].ReadOnly = true;
            dtgReimbursmentPaymentHistory.Columns["DesignationTitle"].Visible = false;
            dtgReimbursmentPaymentHistory.Columns["DepartmentTitle"].HeaderText = "Department";
            dtgReimbursmentPaymentHistory.Columns["DepartmentTitle"].Width = 150;
            dtgReimbursmentPaymentHistory.Columns["DepartmentTitle"].ReadOnly = true;
            dtgReimbursmentPaymentHistory.Columns["DepartmentTitle"].Visible = false;
            dtgReimbursmentPaymentHistory.Columns["ReimbID"].Visible = false;
            dtgReimbursmentPaymentHistory.Columns["ReimbTitle"].HeaderText = "Reimbursment Type";
            dtgReimbursmentPaymentHistory.Columns["ReimbTitle"].Width = 200;
            dtgReimbursmentPaymentHistory.Columns["ReimbTitle"].ReadOnly = true;
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestID"].Visible = false;
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestCode"].HeaderText = "Request Code";
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestCode"].Width = 150;
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestCode"].ReadOnly = true;
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestDescription"].HeaderText = "Request Description";
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestDescription"].Width = 300;
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestDescription"].ReadOnly = true;
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestDate"].HeaderText = "Request Date";
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestDate"].Width = 150;
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestDate"].ReadOnly = true;
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestStatus"].HeaderText = "Request Status";
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestStatus"].Width = 150;
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestStatus"].ReadOnly = true;
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestDetailID"].Visible = false;
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestTRDate"].HeaderText = "Last Pay Date";
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestTRDate"].Width = 200;
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestTRDate"].ReadOnly = true;
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestTRDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgReimbursmentPaymentHistory.Columns["CBalance"].HeaderText = "Outstanding Balance";
            dtgReimbursmentPaymentHistory.Columns["CBalance"].Width = 150;
            dtgReimbursmentPaymentHistory.Columns["CBalance"].ReadOnly = true;
            dtgReimbursmentPaymentHistory.Columns["CBalance"].DefaultCellStyle.Format = "c2";
            dtgReimbursmentPaymentHistory.Columns["Comments"].HeaderText = "Comments";
            dtgReimbursmentPaymentHistory.Columns["Comments"].Width = 350;
            dtgReimbursmentPaymentHistory.Columns["Comments"].ReadOnly = true;
            dtgReimbursmentPaymentHistory.Columns["ReimbursRequestID"].Visible = false;
            dtgReimbursmentPaymentHistory.Columns["RequestStatus"].Visible = false;
            dtgReimbursmentPaymentHistory.Columns["EmpIsActive"].Visible = false;
            dtgReimbursmentPaymentHistory.Columns["EmpIsDeleted"].Visible = false;
            dtgReimbursmentPaymentHistory.Columns["RequestIsActive"].Visible = false;
            dtgReimbursmentPaymentHistory.Columns["RequestIsDeleted"].Visible = false;
            dtgReimbursmentPaymentHistory.Columns["ClientID"].Visible = false;
            dtgReimbursmentPaymentHistory.Columns["IncludeInNextPayslipProcess"].Visible = false;
        }

        private void LoadEmployeeSpecificAdvanceInfo()
        {
            dtgAdvanceList.DataSource = objAdvanceTransaction.EmployeeSpecificAdvanceInformation(Convert.ToInt32(lblEmpID.Text.ToString()), -1);
            dtgAdvanceList.Columns["Select"].Visible = false;

            dtgAdvanceList.Columns["EmpID"].Visible = false;
            dtgAdvanceList.Columns["EmpID"].ReadOnly = true;
            dtgAdvanceList.Columns["EmpID"].Width = 100;

            dtgAdvanceList.Columns["EmpCode"].Visible = false;
            dtgAdvanceList.Columns["EmpCode"].ReadOnly = true;
            dtgAdvanceList.Columns["EmpCode"].Width = 100;

            dtgAdvanceList.Columns["EmpName"].Visible = false;
            dtgAdvanceList.Columns["EmpName"].ReadOnly = true;
            dtgAdvanceList.Columns["EmpName"].Width = 175;

            dtgAdvanceList.Columns["DesignationTitle"].Visible = false;
            dtgAdvanceList.Columns["DesignationTitle"].ReadOnly = true;
            dtgAdvanceList.Columns["DesignationTitle"].Width = 175;

            dtgAdvanceList.Columns["DepartmentTitle"].Visible = false;
            dtgAdvanceList.Columns["DepartmentTitle"].ReadOnly = true;
            dtgAdvanceList.Columns["DepartmentTitle"].Width = 175;

            dtgAdvanceList.Columns["PersonalInfoID"].Visible = false;
            dtgAdvanceList.Columns["PersonalInfoID"].ReadOnly = true;
            dtgAdvanceList.Columns["PersonalInfoID"].Width = 100;

            dtgAdvanceList.Columns["ContactNumber2"].Visible = false;
            dtgAdvanceList.Columns["ContactNumber2"].ReadOnly = true;
            dtgAdvanceList.Columns["ContactNumber2"].Width = 200;

            dtgAdvanceList.Columns["EmpAdvanceRequestID"].Visible = false;
            dtgAdvanceList.Columns["EmpAdvanceRequestID"].ReadOnly = true;
            dtgAdvanceList.Columns["EmpAdvanceRequestID"].Width = 175;

            dtgAdvanceList.Columns["EmpAdvReqCode"].Visible = true;
            dtgAdvanceList.Columns["EmpAdvReqCode"].ReadOnly = true;
            dtgAdvanceList.Columns["EmpAdvReqCode"].Width = 175;

            dtgAdvanceList.Columns["AdvanceTypeID"].Visible = false;
            dtgAdvanceList.Columns["AdvanceTypeID"].ReadOnly = true;
            dtgAdvanceList.Columns["AdvanceTypeID"].Width = 175;

            dtgAdvanceList.Columns["AdvanceTypeTitle"].Visible = true;
            dtgAdvanceList.Columns["AdvanceTypeTitle"].ReadOnly = true;
            dtgAdvanceList.Columns["AdvanceTypeTitle"].Width = 175;

            dtgAdvanceList.Columns["AdvanceAmount"].Visible = true;
            dtgAdvanceList.Columns["AdvanceAmount"].ReadOnly = true;
            dtgAdvanceList.Columns["AdvanceAmount"].Width = 125;
            dtgAdvanceList.Columns["AdvanceAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgAdvanceList.Columns["AdvanceAmount"].DefaultCellStyle.Format = "c2";

            dtgAdvanceList.Columns["AdvanceInstallment"].Visible = true;
            dtgAdvanceList.Columns["AdvanceInstallment"].ReadOnly = true;
            dtgAdvanceList.Columns["AdvanceInstallment"].Width = 125;
            dtgAdvanceList.Columns["AdvanceInstallment"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgAdvanceList.Columns["AdvanceInstallment"].DefaultCellStyle.Format = "c2";


            dtgAdvanceList.Columns["AdvanceStartDate"].Visible = true;
            dtgAdvanceList.Columns["AdvanceStartDate"].ReadOnly = true;
            dtgAdvanceList.Columns["AdvanceStartDate"].Width = 125;
            dtgAdvanceList.Columns["AdvanceStartDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";

            dtgAdvanceList.Columns["AdvanceEndDate"].Visible = true;
            dtgAdvanceList.Columns["AdvanceEndDate"].ReadOnly = true;
            dtgAdvanceList.Columns["AdvanceEndDate"].Width = 125;
            dtgAdvanceList.Columns["AdvanceEndDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";

            dtgAdvanceList.Columns["AdvanceRequestStatus"].Visible = true;
            dtgAdvanceList.Columns["AdvanceRequestStatus"].ReadOnly = true;
            dtgAdvanceList.Columns["AdvanceRequestStatus"].Width = 150;

            dtgAdvanceList.Columns["LastRepayDate"].Visible = true;
            dtgAdvanceList.Columns["LastRepayDate"].ReadOnly = true;
            dtgAdvanceList.Columns["LastRepayDate"].Width = 125;
            dtgAdvanceList.Columns["LastRepayDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";

            dtgAdvanceList.Columns["CBalance"].Visible = false;
            dtgAdvanceList.Columns["CBalance"].ReadOnly = true;
            dtgAdvanceList.Columns["CBalance"].Width = 125;
            dtgAdvanceList.Columns["CBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgAdvanceList.Columns["CBalance"].DefaultCellStyle.Format = "c2";

            dtgAdvanceList.Columns["RePaymentBalance"].Visible = true;
            dtgAdvanceList.Columns["RePaymentBalance"].ReadOnly = true;
            dtgAdvanceList.Columns["RePaymentBalance"].Width = 125;
            dtgAdvanceList.Columns["RePaymentBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgAdvanceList.Columns["RePaymentBalance"].DefaultCellStyle.Format = "c2";

            dtgAdvanceList.Columns["EmpAdvanceRecoveryID"].Visible = false;
            dtgAdvanceList.Columns["EmpAdvanceRecoveryID"].ReadOnly = true;
            dtgAdvanceList.Columns["EmpAdvanceRecoveryID"].Width = 175;

            dtgAdvanceList.Columns["AdvanceRequestStatus"].Visible = false;
            dtgAdvanceList.Columns["AdvanceRequestStatus"].ReadOnly = true;
            dtgAdvanceList.Columns["AdvanceRequestStatus"].Width = 175;
        }

        private void LoadEmployeeSpecificAssetInfo()
        {
            dtgAssetInformation.DataSource = null;
            dtgAssetInformation.DataSource = objAssetRegisterInfo.getEmployeeSpecificApprovedAssetInformationRequestList(Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToInt16(lblClientID.Text.ToString()));
            dtgAssetInformation.Columns["AssetCatMasID"].Visible = false;
            dtgAssetInformation.Columns["AssetID"].Visible = false;
            dtgAssetInformation.Columns["EmpID"].Visible = false;
            dtgAssetInformation.Columns["PersonalInfoID"].Visible = false;
            dtgAssetInformation.Columns["AssetRequestID"].Visible = false;
            dtgAssetInformation.Columns["CatAssetCode"].Visible = false;
            dtgAssetInformation.Columns["CatAssetName"].HeaderText = "Category";
            dtgAssetInformation.Columns["CatAssetName"].Width = 150;
            dtgAssetInformation.Columns["CatAssetName"].ReadOnly = true;
            dtgAssetInformation.Columns["AssetCode"].Visible = false;
            dtgAssetInformation.Columns["AssetName"].HeaderText = "Asset Name";
            dtgAssetInformation.Columns["AssetName"].Width = 400;
            dtgAssetInformation.Columns["AssetName"].ReadOnly = true;
            dtgAssetInformation.Columns["AssetRequestCode"].HeaderText = "Asset Request Code";
            dtgAssetInformation.Columns["AssetRequestCode"].Width = 125;
            dtgAssetInformation.Columns["AssetRequestCode"].ReadOnly = true;
            dtgAssetInformation.Columns["EmpCode"].Visible = false;
            dtgAssetInformation.Columns["EmpCode"].HeaderText = "Emp Code";
            dtgAssetInformation.Columns["EmpCode"].Width = 100;
            dtgAssetInformation.Columns["EmpCode"].ReadOnly = true;
            dtgAssetInformation.Columns["EmpName"].Visible = false;
            dtgAssetInformation.Columns["EmpName"].HeaderText = "Employee Name";
            dtgAssetInformation.Columns["EmpName"].Width = 200;
            dtgAssetInformation.Columns["EmpName"].ReadOnly = true;
            dtgAssetInformation.Columns["DesignationTitle"].Visible = false;
            dtgAssetInformation.Columns["DesignationTitle"].HeaderText = "Designation";
            dtgAssetInformation.Columns["DesignationTitle"].Width = 150;
            dtgAssetInformation.Columns["DesignationTitle"].ReadOnly = true;
            dtgAssetInformation.Columns["DepartmentTitle"].Visible = false;
            dtgAssetInformation.Columns["DepartmentTitle"].HeaderText = "Department";
            dtgAssetInformation.Columns["DepartmentTitle"].Width = 150;
            dtgAssetInformation.Columns["DepartmentTitle"].ReadOnly = true;
            dtgAssetInformation.Columns["AssetRequestComments"].HeaderText = "Comments";
            dtgAssetInformation.Columns["AssetRequestComments"].Width = 350;
            dtgAssetInformation.Columns["AssetRequestComments"].ReadOnly = true;
            dtgAssetInformation.Columns["AssetRequestDate"].HeaderText = "Request Date";
            dtgAssetInformation.Columns["AssetRequestDate"].Width = 100;
            dtgAssetInformation.Columns["AssetRequestDate"].ReadOnly = true;
            dtgAssetInformation.Columns["AssetRequestDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgAssetInformation.Columns["RequestStatus"].HeaderText = "Status";
            dtgAssetInformation.Columns["RequestStatus"].Width = 100;
            dtgAssetInformation.Columns["RequestStatus"].ReadOnly = true;
        }

        private void LoadEmployeeSpecificLeaveBalanceInfo()
        {
            dtgLeaveBalanceInfo.DataSource = null;
            dtgLeaveBalanceInfo.DataSource = objEmpLeaveEntitlementInfo.getEmployeeSpecificLeaveOutstandingInfo(Convert.ToInt16(lblEmpID.Text));
            dtgLeaveBalanceInfo.Columns["LeaveEntmtID"].Visible = false;
            dtgLeaveBalanceInfo.Columns["EmpID"].Visible = false;
            dtgLeaveBalanceInfo.Columns["EmpID"].ReadOnly = true;
            dtgLeaveBalanceInfo.Columns["LeaveMasID"].Visible = false;
            dtgLeaveBalanceInfo.Columns["LeaveMasID"].ReadOnly = true;
            dtgLeaveBalanceInfo.Columns["LeaveTypeID"].Visible = false;
            dtgLeaveBalanceInfo.Columns["LeaveTypeID"].ReadOnly = true;
            dtgLeaveBalanceInfo.Columns["LeaveTypeTitle"].ReadOnly = true;
            dtgLeaveBalanceInfo.Columns["LeaveTypeTitle"].Width = 218;

            dtgLeaveBalanceInfo.Columns["TotalLeaves"].HeaderText = "Allotted Leaves";
            dtgLeaveBalanceInfo.Columns["TotalLeaves"].Width = 100;
            dtgLeaveBalanceInfo.Columns["TotalLeaves"].ReadOnly = true;
            dtgLeaveBalanceInfo.Columns["TotalLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgLeaveBalanceInfo.Columns["TotalLeaves"].DefaultCellStyle.Format = "0.00";

            dtgLeaveBalanceInfo.Columns["BalanceLeaves"].HeaderText = "Available Leaves";
            dtgLeaveBalanceInfo.Columns["BalanceLeaves"].Width = 100;
            dtgLeaveBalanceInfo.Columns["BalanceLeaves"].ReadOnly = true;
            dtgLeaveBalanceInfo.Columns["BalanceLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgLeaveBalanceInfo.Columns["BalanceLeaves"].DefaultCellStyle.Format = "0.00";

            dtgLeaveBalanceInfo.Columns["UsedLeaves"].HeaderText = "Utilised Leaves";
            dtgLeaveBalanceInfo.Columns["UsedLeaves"].Width = 90;
            dtgLeaveBalanceInfo.Columns["UsedLeaves"].ReadOnly = true;
            dtgLeaveBalanceInfo.Columns["UsedLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgLeaveBalanceInfo.Columns["UsedLeaves"].DefaultCellStyle.Format = "0.00";
            dtgLeaveBalanceInfo.Columns["OrderID"].Visible = false;
            dtgLeaveBalanceInfo.Columns["IsCarryForwardAllowed"].Visible = false;
            dtgLeaveBalanceInfo.Columns["MaxCarryForwardAllowed"].Visible = false;
            dtgLeaveBalanceInfo.Columns["IsCarryForwardExpiryApplicable"].Visible = false;
            dtgLeaveBalanceInfo.Columns["CarryForwardExpiryMonths"].Visible = false;
            dtgLeaveBalanceInfo.Columns["IsEncashmentAllowed"].Visible = false;

            dtgLeaveBalanceInfo.Columns["MaxLeaveDaysAllowedToEncash"].HeaderText = "Max. Allowed Leaves";
            dtgLeaveBalanceInfo.Columns["MaxLeaveDaysAllowedToEncash"].Width = 125;
            dtgLeaveBalanceInfo.Columns["MaxLeaveDaysAllowedToEncash"].ReadOnly = true;
            dtgLeaveBalanceInfo.Columns["MaxLeaveDaysAllowedToEncash"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgLeaveBalanceInfo.Columns["MaxLeaveDaysAllowedToEncash"].DefaultCellStyle.Format = "0.00";

            dtgLeaveBalanceInfo.Columns["IsEncashmentDuringEmploymentAllowed"].Visible = false;
            dtgLeaveBalanceInfo.Columns["IsEncashmentAtResignationAllowed"].Visible = false;
            dtgLeaveBalanceInfo.Columns["IsLeaveAccrualApplicable"].Visible = false;
            dtgLeaveBalanceInfo.Columns["AccrualFrequency"].Visible = false;
            dtgLeaveBalanceInfo.Columns["IsApplicableDuringProbation"].Visible = false;
            dtgLeaveBalanceInfo.Columns["IsApplicableDuringNoticePeriod"].Visible = false;

            decimal totalLeavesAllotted = 0;
            decimal totalOutstandingLeaves = 0;
            decimal totalLeavesUtilised = 0;

            foreach (DataGridViewRow row in dtgLeaveBalanceInfo.Rows)
            {
                if (row.IsNewRow || !row.Visible)
                    continue;

                if (Convert.ToBoolean(row.Cells["TotalLeaves"].Value) == true)
                {
                    totalLeavesAllotted += row.Cells["TotalLeaves"].Value == null ? 0 : Convert.ToDecimal(row.Cells["TotalLeaves"].Value);
                    totalOutstandingLeaves += row.Cells["BalanceLeaves"].Value == null ? 0 : Convert.ToDecimal(row.Cells["BalanceLeaves"].Value);
                    totalLeavesUtilised += row.Cells["UsedLeaves"].Value == null ? 0 : Convert.ToDecimal(row.Cells["UsedLeaves"].Value);
                }
            }

            // Display totals
            txtTotalLeaveAlloted.Text = totalLeavesAllotted.ToString("0.00");
            txtLeaveBalance.Text = totalOutstandingLeaves.ToString("0.00");
            txtTotalLeaveUtilised.Text = totalLeavesUtilised.ToString("0.00");

            // Overall leave balance
            decimal leaveBalance = totalOutstandingLeaves;

            txtEncashableLeaveBalance.Text = leaveBalance.ToString("0.00");

            txtMonthlyBasicSalary.Text = "0.00";
            txtDailySalaryRate.Text = "0.00";
            txtEncashmentAmount.Text = "0.00";
            txtTaxDeductionOnEncashmentAmount.Text = "0.00";
            txtNetEncashmentAmount.Text = "0.00";
            txtMonthlyBasicSalary.Text = objEmpLeaveEntitlementInfo.getBasicSalaryComponent(Convert.ToInt16(lblEmpID.Text)).ToString("0.00");
            txtMonthlyBasicSalary.Text = DBCommon.RoundOffDecimal(Convert.ToDecimal(txtMonthlyBasicSalary.Text.ToString())).ToString("0.00");

            txtDailySalaryRate.Text = (Convert.ToDecimal(txtMonthlyBasicSalary.Text.ToString()) / 30).ToString("0.00");
            txtDailySalaryRate.Text = DBCommon.RoundOffDecimal(Convert.ToDecimal(txtDailySalaryRate.Text.ToString())).ToString("0.00");

            txtEncashmentAmount.Text = (Convert.ToDecimal(txtDailySalaryRate.Text.ToString()) * Convert.ToDecimal(optEncashmentDays.Value.ToString())).ToString("0.00");
            txtEncashmentAmount.Text = DBCommon.RoundOffDecimal(Convert.ToDecimal(txtEncashmentAmount.Text.ToString())).ToString("0.00");

            txtNetEncashmentAmount.Text = (Convert.ToDecimal(txtEncashmentAmount.Text.ToString()) - Convert.ToDecimal(txtTaxDeductionOnEncashmentAmount.Text.ToString())).ToString("0.00");
            txtNetEncashmentAmount.Text = DBCommon.RoundOffDecimal(Convert.ToDecimal(txtNetEncashmentAmount.Text.ToString())).ToString("0.00");

            txtGRTLastDrawnBasic.Text = objEmpLeaveEntitlementInfo.getBasicSalaryComponent(Convert.ToInt16(lblEmpID.Text)).ToString("0.00");
            txtGRTLastDrawnBasic.Text = DBCommon.RoundOffDecimal(Convert.ToDecimal(txtGRTLastDrawnBasic.Text.ToString())).ToString("0.00");
            txtGRTEligibleAllowance.Text = objEmpLeaveEntitlementInfo.getGratuityAllowanceComponent(Convert.ToInt16(lblEmpID.Text)).ToString("0.00");
            txtGRTEligibleAllowance.Text = DBCommon.RoundOffDecimal(Convert.ToDecimal(txtGRTEligibleAllowance.Text.ToString())).ToString("0.00");
            txtGRTGratuitySalaryBasis.Text = (Convert.ToDecimal(txtGRTLastDrawnBasic.Text.ToString()) + Convert.ToDecimal(txtGRTEligibleAllowance.Text.ToString())).ToString("0.00");
            txtGRTEligibleAllowance.Text = DBCommon.RoundOffDecimal(Convert.ToDecimal(txtGRTEligibleAllowance.Text.ToString())).ToString("0.00");
            CalculateGratuityEligibilityAndAmounts();
        }

        private void LoadEmployeeLeaveEncashmentInfo()
        {
            ModelStaffSync.EmpResignLeaveEncashmentInfo tmpEmpResignLeaveEncashmentInfo = objEmpResignLeaveEncashmentInfo.getEmpResignLeaveEncashmentInfotatements(Convert.ToInt32(lblEmpResignID.Text.ToString()));
            if (tmpEmpResignLeaveEncashmentInfo.EmpResignID != 0)
            {
                lblLeaveEncashmentID.Text = tmpEmpResignLeaveEncashmentInfo.LeaveEncashmentID.ToString();
                txtTotalLeaveAlloted.Text = tmpEmpResignLeaveEncashmentInfo.TotalLeaveAllotted.ToString("0.00");
                txtTotalLeaveUtilised.Text = tmpEmpResignLeaveEncashmentInfo.TotalLeaveUtilised.ToString("0.00");
                txtLeaveBalance.Text = tmpEmpResignLeaveEncashmentInfo.TotalLeaveBalance.ToString("0.00");
                txtEncashableLeaveBalance.Text = tmpEmpResignLeaveEncashmentInfo.TotalEncashableLeave.ToString("0.00");

                optEncashmentDays.Value = tmpEmpResignLeaveEncashmentInfo.DaysToEncash;
                dtEncashmentDate.Text = tmpEmpResignLeaveEncashmentInfo.EncashmentDate.ToString("dd-MM-yyyy");
                txtMonthlyBasicSalary.Text = tmpEmpResignLeaveEncashmentInfo.EligibleSalary.ToString("0.00");
                txtDailySalaryRate.Text = tmpEmpResignLeaveEncashmentInfo.DailySalaryRate.ToString("0.00");
                txtEncashmentAmount.Text = tmpEmpResignLeaveEncashmentInfo.EncashmentAmount.ToString("0.00");
                txtTaxDeductionOnEncashmentAmount.Text = tmpEmpResignLeaveEncashmentInfo.TaxDeduction.ToString("0.00");
                txtNetEncashmentAmount.Text = tmpEmpResignLeaveEncashmentInfo.NetEncashmentAmount.ToString("0.00");
                chkLeaveEncashmentVerifiedAndConfirmed.Checked = tmpEmpResignLeaveEncashmentInfo.EncashmentProcessStatus;
                txtLeaveEncashmentMoreDetails.Text = tmpEmpResignLeaveEncashmentInfo.MoreDetails.ToString();
            }
            else
            {
                lblLeaveEncashmentID.Text = "";
            }
        }

        private void LoadEmployeeGratuityInfo()
        {
            ModelStaffSync.EmpResignGratuityInfo tmpEmpResignGratuityInfo = objEmpResignGratuityInfo.getEmpResignResignGratuityInfotatements(Convert.ToInt32(lblEmpResignID.Text.ToString()));
            if (tmpEmpResignGratuityInfo.EmpResignID != 0)
            {
                lblEmpResignGratuityID.Text = tmpEmpResignGratuityInfo.EmpResignGratuityID.ToString();
                txtGRTDateOfJoining.Text = tmpEmpResignGratuityInfo.DateOfJoining.ToString("dd-MMM-yyyy");
                txtGRTResignationDate.Text = tmpEmpResignGratuityInfo.ResignationDate.ToString("dd-MMM-yyyy");
                txtGRTLastWorkingDate.Text = tmpEmpResignGratuityInfo.LastWorkingDate.ToString("dd-MMM-yyyy");
                txtGRTServiceTenure.Text = tmpEmpResignGratuityInfo.ActualServiceYears.ToString() + " Year " + tmpEmpResignGratuityInfo.ActualServiceMonths.ToString() + " Months " + tmpEmpResignGratuityInfo.ActualServiceDays.ToString() + " Days";
                txtGRTEligibleYears.Text = tmpEmpResignGratuityInfo.GratuityServiceYears.ToString();
                txtGRTEligibleStatus.Text = tmpEmpResignGratuityInfo.IsGratuityEligible.ToString();
                txtGRTEligibleStatus.Text = tmpEmpResignGratuityInfo.EligibilityStatus.ToString();
                cmbGRTCalculationFormula.SelectedIndex = 0;
                txtGRTLastDrawnBasic.Text = tmpEmpResignGratuityInfo.LastDrawnBasicSalary.ToString("0.00");
                txtGRTEligibleAllowance.Text = tmpEmpResignGratuityInfo.EligibleAllowances.ToString("0.00");
                txtGRTGratuitySalaryBasis.Text = tmpEmpResignGratuityInfo.GratuitySalaryBasis.ToString("0.00");
                txtGRTGorssGratuityAmount.Text = tmpEmpResignGratuityInfo.GrossGratuityAmount.ToString("0.00");
                txtGRTTaxOrDeduction.Text = tmpEmpResignGratuityInfo.TaxOrDeductionAmount.ToString("0.00");
                txtGRTNetGratuityAmount.Text = tmpEmpResignGratuityInfo.NetGratuityAmount.ToString("0.00");

                cmbGRTHRApprovalBy.SelectedValue = Convert.ToInt32(tmpEmpResignGratuityInfo.HRVerifiedBy);
                dtGRTHRApprovalDate.Text = tmpEmpResignGratuityInfo.HRVerificationDate.Date.ToString("dd-MM-yyyy");
                cmbGRTHRApprovalStatus.SelectedIndex = tmpEmpResignGratuityInfo.HRVerificationStatus;
                txtGRTHRRemarks.Text = tmpEmpResignGratuityInfo.HRRemarks.ToString();

                cmbGRTFINApprovalBy.SelectedValue = Convert.ToInt32(tmpEmpResignGratuityInfo.FinanceVerifiedBy);
                dtGRTFINApprovalDate.Text = tmpEmpResignGratuityInfo.FinanceVerificationDate.Date.ToString("dd-MM-yyyy");
                cmbGRTFINApprovalStatus.SelectedIndex = tmpEmpResignGratuityInfo.FinanceVerificationStatus;
                txtGRTFINRemarks.Text = tmpEmpResignGratuityInfo.FinanceRemarks.ToString();

                cmbGRTPaymentStatus.SelectedIndex = tmpEmpResignGratuityInfo.PaymentStatus;
                dtGRTFINApprovalDate.Text = tmpEmpResignGratuityInfo.FinanceVerificationDate.Date.ToString("dd-MM-yyyy");
                txtGRTPaymentRefNo.Text = tmpEmpResignGratuityInfo.PaymentReferenceNo.ToString();
                txtGRTPaymentRemarks.Text = tmpEmpResignGratuityInfo.PaymentRemarks.ToString();
            }
            else
            {
                lblEmpResignGratuityID.Text = "";
            }
        }

        private void txtNoticePeriod_TextChanged(object sender, EventArgs e)
        {
            if (txtNoticePeriod.Text.ToString().Trim() == "")
                return;

            if (Convert.ToInt16(txtNoticePeriod.Text) < 0)
            {
                MessageBox.Show("Notice Period cannot be negative.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNoticePeriod.Text = "0";
                return;
            }
            else if (Convert.ToInt16(txtNoticePeriod.Text) > 0)
            {

            }


            if (!int.TryParse(txtNoticePeriod.Text, out _))
            {
                MessageBox.Show("Please enter a valid number for Notice Period.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNoticePeriod.Text = "0";
                return;
            }
            dtLastWorkingDay.Text = DateTime.Now.AddDays(Convert.ToInt32(txtNoticePeriod.Text)).ToString("dd-MM-yyyy");

        }

        private void lnkViewAuditLog_LinkClicked(object sender, EventArgs e)
        {
            frmAuditLogStatements objAuditLogStatements = new frmAuditLogStatements(Convert.ToInt32(lblEmpResignID.Text.ToString()), "EmployeeResignationStatusUpdate", "Employee Resignation Management", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
            objAuditLogStatements.ShowDialog(this);
        }

        private void btnSearchReportingManager_Click(object sender, EventArgs e)
        {
            frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listResignationApproverList", Convert.ToInt32(lblClientID.Text.ToString()));
            frmEmployeeList.ShowDialog();
        }

        private void btnSearchHRManager_Click(object sender, EventArgs e)
        {
            frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listResignationApproverHRList", Convert.ToInt32(lblClientID.Text.ToString()));
            frmEmployeeList.ShowDialog();
        }

        private void dtgReimbursmentPaymentHistory_Paint(object sender, PaintEventArgs e)
        {
            KryptonDataGridView dgv = sender as KryptonDataGridView;

            if (dgv.Rows.Count == 0)
            {
                string message = "No Pending Reimbursement(s) Information Available";

                using (System.Drawing.Font font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold))
                {
                    SizeF size = e.Graphics.MeasureString(message, font);

                    e.Graphics.DrawString(message, font, System.Drawing.Brushes.Gray, (dgv.Width - size.Width) / 2, (dgv.Height - size.Height) / 2);
                }
            }
        }

        private void dtgAdvanceList_Paint(object sender, PaintEventArgs e)
        {
            KryptonDataGridView dgv = sender as KryptonDataGridView;

            if (dgv.Rows.Count == 0)
            {
                string message = "No Pending Advance(s) Information Available";

                using (System.Drawing.Font font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold))
                {
                    SizeF size = e.Graphics.MeasureString(message, font);

                    e.Graphics.DrawString(message, font, System.Drawing.Brushes.Gray, (dgv.Width - size.Width) / 2, (dgv.Height - size.Height) / 2);
                }
            }
        }

        private void dtgAssetInformation_Paint(object sender, PaintEventArgs e)
        {
            KryptonDataGridView dgv = sender as KryptonDataGridView;

            if (dgv.Rows.Count == 0)
            {
                string message = "No Pending Asset(s) Information Available";

                using (System.Drawing.Font font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold))
                {
                    SizeF size = e.Graphics.MeasureString(message, font);

                    e.Graphics.DrawString(message, font, System.Drawing.Brushes.Gray, (dgv.Width - size.Width) / 2, (dgv.Height - size.Height) / 2);
                }
            }
        }

        private void tabControl1_TabClicked(object sender, Krypton.Navigator.KryptonPageEventArgs e)
        {
        }

        private void txtEncashmentAmount_TextChanged(object sender, EventArgs e)
        {
            //if (txtEncashmentAmount.Text.ToString().Trim() == "")
            //    return;

            //if (!string.IsNullOrEmpty(txtEncashmentAmount.Text))
            //{
            //    if(Convert.ToDecimal(txtEncashmentAmount.Text.ToString()) > 0)
            //    {
            //        txtEncashmentAmount.Text = (Convert.ToDecimal(txtDailySalaryRate.Text.ToString()) * Convert.ToDecimal(optEncashmentDays.Value.ToString())).ToString("0.00");
            //        txtEncashmentAmount.Text = Math.Round(Convert.ToDecimal(txtEncashmentAmount.Text.ToString()), 0).ToString("0.00");

            //        txtNetEncashmentAmount.Text = (Convert.ToDecimal(txtEncashmentAmount.Text.ToString()) - Convert.ToDecimal(txtTaxDeductionOnEncashmentAmount.Text.ToString())).ToString("0.00");
            //        txtNetEncashmentAmount.Text = Math.Round(Convert.ToDecimal(txtNetEncashmentAmount.Text.ToString()), 0).ToString("0.00");
            //    }
            //}
        }

        private void txtTaxDeductionOnEncashmentAmount_TextChanged(object sender, EventArgs e)
        {
            //if (txtTaxDeductionOnEncashmentAmount.Text.ToString().Trim() == "")
            //    return;

            //if (!string.IsNullOrEmpty(txtTaxDeductionOnEncashmentAmount.Text))
            //{
            //    if (Convert.ToDecimal(txtTaxDeductionOnEncashmentAmount.Text.ToString()) > 0)
            //    {
            //        if (!string.IsNullOrEmpty(txtEncashmentAmount.Text))
            //        {
            //            if (Convert.ToDecimal(txtEncashmentAmount.Text.ToString()) > 0)
            //            {
            //                txtEncashmentAmount.Text = (Convert.ToDecimal(txtDailySalaryRate.Text.ToString()) * Convert.ToDecimal(optEncashmentDays.Value.ToString())).ToString("0.00");
            //                txtEncashmentAmount.Text = Math.Round(Convert.ToDecimal(txtEncashmentAmount.Text.ToString()), 0).ToString("0.00");

            //                txtNetEncashmentAmount.Text = (Convert.ToDecimal(txtEncashmentAmount.Text.ToString()) - Convert.ToDecimal(txtTaxDeductionOnEncashmentAmount.Text.ToString())).ToString("0.00");
            //                txtNetEncashmentAmount.Text = Math.Round(Convert.ToDecimal(txtNetEncashmentAmount.Text.ToString()), 0).ToString("0.00");
            //            }
            //        }
            //    }
            //}
        }

        private void txtMonthlyBasicSalary_TextChanged(object sender, EventArgs e)
        {
            //if (txtMonthlyBasicSalary.Text.ToString().Trim() == "")
            //    return;

            //if (!string.IsNullOrEmpty(txtMonthlyBasicSalary.Text))
            //{
            //    if (Convert.ToDecimal(txtMonthlyBasicSalary.Text.ToString()) > 0)
            //    {
            //        if (!string.IsNullOrEmpty(txtTaxDeductionOnEncashmentAmount.Text))
            //        {
            //            if (Convert.ToDecimal(txtTaxDeductionOnEncashmentAmount.Text.ToString()) > 0)
            //            {
            //                if (!string.IsNullOrEmpty(txtEncashmentAmount.Text))
            //                {
            //                    if (Convert.ToDecimal(txtEncashmentAmount.Text.ToString()) > 0)
            //                    {
            //                        txtEncashmentAmount.Text = (Convert.ToDecimal(txtDailySalaryRate.Text.ToString()) * Convert.ToDecimal(optEncashmentDays.Value.ToString())).ToString("0.00");
            //                        txtEncashmentAmount.Text = Math.Round(Convert.ToDecimal(txtEncashmentAmount.Text.ToString()), 0).ToString("0.00");

            //                        txtNetEncashmentAmount.Text = (Convert.ToDecimal(txtEncashmentAmount.Text.ToString()) - Convert.ToDecimal(txtTaxDeductionOnEncashmentAmount.Text.ToString())).ToString("0.00");
            //                        txtNetEncashmentAmount.Text = Math.Round(Convert.ToDecimal(txtNetEncashmentAmount.Text.ToString()), 0).ToString("0.00");
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }

        private void txtDailySalaryRate_TextChanged(object sender, EventArgs e)
        {
            //if (txtDailySalaryRate.Text.ToString().Trim() == "")
            //    return;

            //if (!string.IsNullOrEmpty(txtDailySalaryRate.Text))
            //{
            //    if (!string.IsNullOrEmpty(txtMonthlyBasicSalary.Text))
            //    {
            //        if (Convert.ToDecimal(txtMonthlyBasicSalary.Text.ToString()) > 0)
            //        {
            //            if (!string.IsNullOrEmpty(txtTaxDeductionOnEncashmentAmount.Text))
            //            {
            //                if (Convert.ToDecimal(txtTaxDeductionOnEncashmentAmount.Text.ToString()) > 0)
            //                {
            //                    if (!string.IsNullOrEmpty(txtEncashmentAmount.Text))
            //                    {
            //                        if (Convert.ToDecimal(txtEncashmentAmount.Text.ToString()) > 0)
            //                        {
            //                            txtEncashmentAmount.Text = (Convert.ToDecimal(txtDailySalaryRate.Text.ToString()) * Convert.ToDecimal(optEncashmentDays.Value.ToString())).ToString("0.00");
            //                            txtEncashmentAmount.Text = Math.Round(Convert.ToDecimal(txtEncashmentAmount.Text.ToString()), 0).ToString("0.00");

            //                            txtNetEncashmentAmount.Text = (Convert.ToDecimal(txtEncashmentAmount.Text.ToString()) - Convert.ToDecimal(txtTaxDeductionOnEncashmentAmount.Text.ToString())).ToString("0.00");
            //                            txtNetEncashmentAmount.Text = Math.Round(Convert.ToDecimal(txtNetEncashmentAmount.Text.ToString()), 0).ToString("0.00");
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }

        private void tabControl1_Selecting(object sender, Krypton.Navigator.KryptonPageCancelEventArgs e)
        {

        }

        private void tabControl1_Selected(object sender, Krypton.Navigator.KryptonPageEventArgs e)
        {
            if (e.Item.Tag.ToString() == "tabLeaveEncashment")
            {
                string maxAllowedLeaveChecker = "";
                foreach (DataGridViewRow row in dtgLeaveBalanceInfo.Rows)
                {
                    if (row.IsNewRow || !row.Visible)
                        continue;

                    if (Convert.ToBoolean(row.Cells["IsEncashmentAllowed"].Value) == true)
                    {
                        if (Convert.ToDecimal(row.Cells["BalanceLeaves"].Value) > Convert.ToDecimal(row.Cells["MaxLeaveDaysAllowedToEncash"].Value))
                        {
                            if (maxAllowedLeaveChecker == "")
                                maxAllowedLeaveChecker = row.Cells["LeaveTypeTitle"].Value.ToString() + " - Available Leave Balance : " + Convert.ToDecimal(row.Cells["BalanceLeaves"].Value) + " exceeds the Maximum allowed leaves : " + Convert.ToDecimal(row.Cells["MaxLeaveDaysAllowedToEncash"].Value) + ".";
                            else
                                maxAllowedLeaveChecker = "\n" + row.Cells["LeaveTypeTitle"].Value.ToString() + " - Available Leave Balance : " + Convert.ToDecimal(row.Cells["BalanceLeaves"].Value) + " exceeds the Maximum allowed leaves : " + Convert.ToDecimal(row.Cells["MaxLeaveDaysAllowedToEncash"].Value) + ".";

                        }
                    }
                }
                if (lblLeaveEncashmentID.Text.ToString().Trim() == "")
                {
                    if (boolMaxLeaveToBeConsidered == false)
                    {
                        if (MessageBox.Show(maxAllowedLeaveChecker + "\nWould you like to continue with Maximum allowed number of days.?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            boolMaxLeaveToBeConsidered = true;
                        }
                    }
                }

                if (boolMaxLeaveToBeConsidered)
                {
                    lblMaxLeaveConsidered.Visible = true;
                    decimal totalLeavesAllotted = 0;
                    decimal totalOutstandingLeaves = 0;
                    decimal totalLeavesUtilised = 0;

                    foreach (DataGridViewRow row in dtgLeaveBalanceInfo.Rows)
                    {
                        if (row.IsNewRow || !row.Visible)
                            continue;

                        if (Convert.ToBoolean(row.Cells["TotalLeaves"].Value) == true)
                        {
                            if (Convert.ToDecimal(row.Cells["BalanceLeaves"].Value) > Convert.ToDecimal(row.Cells["MaxLeaveDaysAllowedToEncash"].Value))
                            {
                                totalLeavesAllotted += row.Cells["TotalLeaves"].Value == null ? 0 : Convert.ToDecimal(row.Cells["TotalLeaves"].Value);
                                totalOutstandingLeaves += row.Cells["MaxLeaveDaysAllowedToEncash"].Value == null ? 0 : Convert.ToDecimal(row.Cells["MaxLeaveDaysAllowedToEncash"].Value);
                                totalLeavesUtilised += row.Cells["UsedLeaves"].Value == null ? 0 : Convert.ToDecimal(row.Cells["UsedLeaves"].Value);
                            }
                        }
                    }

                    // Display totals
                    txtTotalLeaveAlloted.Text = totalLeavesAllotted.ToString("0.00");
                    txtLeaveBalance.Text = totalOutstandingLeaves.ToString("0.00");
                    txtTotalLeaveUtilised.Text = totalLeavesUtilised.ToString("0.00");

                    // Overall leave balance
                    decimal leaveBalance = totalOutstandingLeaves;

                    txtEncashableLeaveBalance.Text = leaveBalance.ToString("0.00");

                    txtMonthlyBasicSalary.Text = "0.00";
                    txtDailySalaryRate.Text = "0.00";
                    txtEncashmentAmount.Text = "0.00";
                    txtTaxDeductionOnEncashmentAmount.Text = "0.00";
                    txtNetEncashmentAmount.Text = "0.00";
                    txtMonthlyBasicSalary.Text = objEmpLeaveEntitlementInfo.getBasicSalaryComponent(Convert.ToInt16(lblEmpID.Text)).ToString("0.00");
                    txtMonthlyBasicSalary.Text = DBCommon.RoundOffDecimal(Convert.ToDecimal(txtMonthlyBasicSalary.Text.ToString())).ToString("0.00");

                    txtDailySalaryRate.Text = (Convert.ToDecimal(txtMonthlyBasicSalary.Text.ToString()) / 30).ToString("0.00");
                    txtDailySalaryRate.Text = DBCommon.RoundOffDecimal(Convert.ToDecimal(txtDailySalaryRate.Text.ToString())).ToString("0.00");

                    txtEncashmentAmount.Text = (Convert.ToDecimal(txtDailySalaryRate.Text.ToString()) * Convert.ToDecimal(optEncashmentDays.Value.ToString())).ToString("0.00");
                    txtEncashmentAmount.Text = DBCommon.RoundOffDecimal(Convert.ToDecimal(txtEncashmentAmount.Text.ToString())).ToString("0.00");

                    txtNetEncashmentAmount.Text = (Convert.ToDecimal(txtEncashmentAmount.Text.ToString()) - Convert.ToDecimal(txtTaxDeductionOnEncashmentAmount.Text.ToString())).ToString("0.00");
                    txtNetEncashmentAmount.Text = DBCommon.RoundOffDecimal(Convert.ToDecimal(txtNetEncashmentAmount.Text.ToString())).ToString("0.00");
                }
            }
        }
    }
}
