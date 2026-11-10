using DocumentFormat.OpenXml.Bibliography;
using Krypton.Toolkit;
using ModelStaffSync;
using Newtonsoft.Json;
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
        DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster(); 
        DALStaffSync.clsEmployeePersonalInfo objEmployeePersonalInfo = new DALStaffSync.clsEmployeePersonalInfo();
        DALStaffSync.clsEmpReimbursRequestInfo objEmpReimbursRequestInfo = new DALStaffSync.clsEmpReimbursRequestInfo();
        DALStaffSync.clsAdvanceTransaction objAdvanceTransaction = new DALStaffSync.clsAdvanceTransaction();
        DALStaffSync.clsAssetRegister objAssetRegisterInfo = new DALStaffSync.clsAssetRegister();

        clsImpageOperation objImpageOperation = new clsImpageOperation();
        DALStaffSync.clsPhotoMas objPhotoMas = new DALStaffSync.clsPhotoMas();

        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();
        SpecificEmployeeSalaryInfo objSpecificEmployeeSalaryInfo = new SpecificEmployeeSalaryInfo();
        AdvanceTypeConfigModel objAdvanceTypeConfigModel = new AdvanceTypeConfigModel();

        string strActionStatement = "";
        private Dictionary<string, object> _originalValues;

        DateTime dos;
        string dateFormat = "dd-MM-yyyy";
        CultureInfo provider = CultureInfo.InvariantCulture;

        public frmEmployeeResignationManagement()
        {
            InitializeComponent();
        }

        public frmEmployeeResignationManagement(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmEmployeeResignationManagement(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
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

            int iTaskID = 0;

            var updatedValues = AuditLogger.getOriginalValues(this);
            var onlyChangedValues = (dynamic)null;

            strActionStatement = "EmployeeResignationStatusUpdate";

            if (lblActionMode.Text == "add" && lblRepManagerApprovalStatus.Text == "False")
            {
                affectedRows = objResignationInfo.ApprovalResignationByReportingManager(Convert.ToInt32(lblEmpResignID.Text.ToString()), Convert.ToDateTime(dtApprovalDate.Text.ToString()), Convert.ToInt32(lblRepManID.Text.ToString()), true, cmbResignationStatus.SelectedIndex);
                if(affectedRows > 0)
                    affectedRows = objResignationInfo.UpdateApprovers(Convert.ToInt32(lblEmpResignID.Text.ToString()), Convert.ToInt32(lblRepManID.Text.ToString()), Convert.ToInt32(lblHROwnerID.Text.ToString()));

                objAuditLog.InsertAuditLog(Convert.ToInt32(lblEmpID.Text.ToString()), Convert.ToInt32(lblEmpResignID.Text.ToString()), "Resignation Status updated to " + cmbResignationStatus.Text, "Insert", ModelStaffSync.CurrentUser.EmpName, strActionStatement, Convert.ToInt32(objTempClientFinYearInfo.ClientID));
            }
            else if (lblActionMode.Text == "add" && lblRepManagerApprovalStatus.Text == "True")
            {
                affectedRows = objResignationInfo.ApprovalResignationByHRManager(Convert.ToInt32(lblEmpResignID.Text.ToString()), Convert.ToDateTime(dtApprovalDate.Text.ToString()), Convert.ToInt32(lblRepManID.Text.ToString()), true, cmbResignationStatus.SelectedIndex);
                if(affectedRows > 0)
                    affectedRows = objResignationInfo.UpdateApprovers(Convert.ToInt32(lblEmpResignID.Text.ToString()), Convert.ToInt32(lblRepManID.Text.ToString()), Convert.ToInt32(lblHROwnerID.Text.ToString()));

                objAuditLog.InsertAuditLog(Convert.ToInt32(lblEmpID.Text.ToString()), Convert.ToInt32(lblEmpResignID.Text.ToString()), "Resignation Status updated to " + cmbResignationStatus.Text, "Insert", ModelStaffSync.CurrentUser.EmpName, strActionStatement, Convert.ToInt32(objTempClientFinYearInfo.ClientID));

                if(cmbResignationStatus.SelectedIndex == 6)
                    objEmployeeMaster.UpdateResginationStatus(Convert.ToInt32(lblEmpID.Text.ToString()));
            }

            if (affectedRows > 0)
                MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);

            objTempCurrentlyLoggedInUserInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objTempCurrentlyLoggedInUserInfo.EmpID.ToString()));

            onSaveButtonClick();
            disableControls();
            clearControls();
            errValidator.Clear();
            this.Cursor = Cursors.Default;
        }

        private bool ValidateValuesOnUI()
        {
            bool validationStatus = true;
            errValidator.Clear();

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

            if(cmbResignationStatus.SelectedIndex == 6)
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
                
                lnkViewAuditLog.Visible = true;

                EmpPersonalPersonalInfo objSelectedPersonalInfo = objEmployeePersonalInfo.GetEmpPersonalPersonalInfo(Convert.ToInt16(lblEmpID.Text));
                txtDateOfJoining.Text = objSelectedPersonalInfo.DOJ.ToString("dd-MMM-yyyy");
                dtLastWorkingDay.Text = DateTime.Now.AddDays(Convert.ToInt32(txtNoticePeriod.Text)).ToString("dd-MM-yyyy");
                txtServiceTenure.Text = CalculateServiceTenure(objSelectedPersonalInfo.DOJ);
                txtServiceTenure.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(txtServiceTenure.Text.ToString().ToLower());
                LoadReimbursementInfo();
                LoadEmployeeSpecificAdvanceInfo();
                LoadEmployeeSpecificAssetInfo();
                tabControl1.Enabled = true;
                tabControl1.SelectedIndex = 0;
            }
            else if (SearchOptionSelectedForm == "listResignationApproverList")
            {
                if(lblRepManID.Text == selectedResignationID.ToString())
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
            cmbResignationStatus.Items.Add("5 - With IT (Assets)");
            cmbResignationStatus.Items.Add("6 - Completed");
            cmbResignationStatus.Items.Add("7 - Withdrawn/Cancelled");
            cmbResignationStatus.SelectedIndex = 0;
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
    }
}
