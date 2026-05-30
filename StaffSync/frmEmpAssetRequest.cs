using DocumentFormat.OpenXml.Wordprocessing;
using ModelStaffSync;
using Quartz.Impl.AdoJobStore.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Quartz.Logging.OperationName;
//using static C1.Util.Win.Win32;

namespace StaffSync
{
    public partial class frmEmpAssetRequest : Form
    {
        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        clsImpageOperation objImpageOperation = new clsImpageOperation();
        DALStaffSync.clsEmployeePersonalIDInfo objEmployeePersonalIDInfo = new DALStaffSync.clsEmployeePersonalIDInfo();
        DALStaffSync.clsEmployeePersonalInfo objEmployeePersonalInfo = new DALStaffSync.clsEmployeePersonalInfo();
        DALStaffSync.clsPhotoMas objPhotoMas = new DALStaffSync.clsPhotoMas();
        DALStaffSync.clsSalaryProfile objSalaryProfile = new DALStaffSync.clsSalaryProfile();
        DALStaffSync.clsAdvanceTypeConfigInfo objAdvanceTypeConfigInfo = new DALStaffSync.clsAdvanceTypeConfigInfo();
        DALStaffSync.clsEmpPayroll objEmployeePayroll = new DALStaffSync.clsEmpPayroll();
        DALStaffSync.clsClientStatutory objClientStatutory = new DALStaffSync.clsClientStatutory();
        DALStaffSync.clsAttendanceMas objAttendanceMas = new DALStaffSync.clsAttendanceMas();
        DALStaffSync.clsAdvanceTypeMas objAdvanceTypeMas = new DALStaffSync.clsAdvanceTypeMas();
        DALStaffSync.clsAssetsInfo objAssetInfo = new DALStaffSync.clsAssetsInfo();
        DALStaffSync.clsAssetRegister objAssetRegister = new DALStaffSync.clsAssetRegister(); 
        DALStaffSync.clsAuditLog objAuditLog = new DALStaffSync.clsAuditLog();
        DALStaffSync.clsAppSettings objAppSettings = new DALStaffSync.clsAppSettings();
        DALStaffSync.clsProvidentFundCalculation objProvidentFundCalculation = new DALStaffSync.clsProvidentFundCalculation();
        DALStaffSync.clsProfessionalTaxCalculation objProfessionalTaxCalculation = new DALStaffSync.clsProfessionalTaxCalculation();
        DALStaffSync.clsAdvanceTransaction objAdvanceTransaction = new DALStaffSync.clsAdvanceTransaction();
        DALStaffSync.clsAppUserTasks objAppUserTasks = new DALStaffSync.clsAppUserTasks();
        frmDashboard objDashboard = (frmDashboard) System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();
        SpecificEmployeeSalaryInfo objSpecificEmployeeSalaryInfo = new SpecificEmployeeSalaryInfo();
        AdvanceTypeConfigModel objAdvanceTypeConfigModel = new AdvanceTypeConfigModel();

        string strActionStatement = "";
        private Dictionary<string, object> _originalValues;

        DateTime dos;
        string dateFormat = "dd-MM-yyyy";
        CultureInfo provider = CultureInfo.InvariantCulture;

        public frmEmpAssetRequest()
        {
            InitializeComponent();
        }

        public frmEmpAssetRequest(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmEmpAssetRequest(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
            lblClientID.Text = objTempClientFinYearInfo.ClientID.ToString();
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
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

        private void btnCloseMe_Click_1(object sender, EventArgs e)
        {
            if (lblActionMode.Text != "")
            {
                if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            objDashboard.lblDashboardTitle.Text = "Dashboard";
            objDashboard.sptrDashboardContainer.Visible = true;
            this.Close();
        }

        private bool ValidateValuesOnUI()
        {
            bool validationStatus = true;
            errValidator.Clear();

            // Helper for date validation
            if (lblReportingManagerID.Text.Trim() == "")
            {
                errValidator.SetError(txtEmpCode, "Please select the employee");
                validationStatus = false;
            }
            else
            {
                errValidator.SetError(txtEmpCode, "");
            }

            if (cmbAssetType.Text.Trim() == "")
            {
                errValidator.SetError(cmbAssetType, "Please select the Asset Type");
                validationStatus = false;
            }
            else
            {
                errValidator.SetError(cmbAssetType, "");
            }

            // Date of Birth
            if (string.IsNullOrEmpty(dtAssetRequestDate.Text))
            {
                validationStatus = false;
                errValidator.SetError(this.dtAssetRequestDate, dtAssetRequestDate.Tag?.ToString() ?? "Date of Request is required.");
            }
            else if (!DateTime.TryParseExact(dtAssetRequestDate.Text, dateFormat, provider, DateTimeStyles.None, out dos))
            {
                validationStatus = false;
                errValidator.SetError(this.dtAssetRequestDate, "Invalid Date of Date of Request format (dd-MM-yyyy).");
            }
            //else if (dos > DateTime.Now.Date)
            //{
            //    validationStatus = false;
            //    errValidator.SetError(this.txtAdvanceStartDate, "Date of Salary cannot be in the future.");
            //}
            else
            {
                errValidator.SetError(this.dtAssetRequestDate, "");
            }

            if (txtQuantity.Text.Trim() == "" || Convert.ToDecimal(txtQuantity.Text.Trim()) == 0)
            {
                errValidator.SetError(txtQuantity, "Please enter the Asset Quantity");
                validationStatus = false;
            }
            else
            {
                errValidator.SetError(txtQuantity, "");
            }

            return validationStatus;
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(objAppSettings.GetSpecificAppSettingsInfo("Allow Multiple Asset Requests").AppSettingValue.ToString()) == false)
            {
                if (objAdvanceTransaction.IsAdvanceAlreadyExist(Convert.ToInt16(lblPersonalInfoID.Text.Trim())) && lblActionMode.Text == "add")
                {
                    MessageBox.Show("An Asset request already exists for the selected employee. Please modify the existing request or select a different employee.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

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
            int AssetRequestID = 0;
            int AssetRequestRegisterID = 0;

            var updatedValues = AuditLogger.getOriginalValues(this);
            var onlyChangedValues = (dynamic)null;

            strActionStatement = "";

            lblAssetID.Text = (cmbAssetType.SelectedIndex + 1).ToString();

            if (lblActionMode.Text == "add")
            {
                AssetRequestID = objAssetInfo.InsertAssetRequestInfo("", Convert.ToInt32(lblAssetID.Text.ToString()), true, false, Convert.ToInt32(lblEmpID.Text.ToString()), Convert.ToDateTime(dtAssetRequestDate.Text.ToString()), txtComments.Text, false, Convert.ToDateTime(dtAssetRequestDate.Text.ToString()), Convert.ToInt32(lblReportingManagerID.Text.ToString()), false, "", false);
                AssetRequestRegisterID = objAssetRegister.InsertAssetRegisterInfo(Convert.ToInt32(lblAssetID.Text.ToString()), Convert.ToDateTime(dtAssetRequestDate.Text), 0, Convert.ToDecimal(txtQuantity.Text.ToString()), 0, Convert.ToDecimal(txtQuantity.Text.ToString()), "Rq", "By " + cmbAssetType.Text + " - Request", Convert.ToInt32(AssetRequestID));

                objAuditLog.InsertAuditLog(Convert.ToInt32(lblEmpID.Text.ToString()), AssetRequestID, "Asset Request - " + "\"ASR-REQ-" + (AssetRequestID).ToString().PadLeft(4, '0').Trim() + "\" Raised by Employee Code : \"" + txtEmpCode.Text.Trim() + "\" and Employee Name : \"" + txtEmpName.Text.Trim() + "\".", "Insert", ModelStaffSync.CurrentUser.EmpName, "AssetRequestNewUpdates", Convert.ToInt32(objTempClientFinYearInfo.ClientID));

                if (AssetRequestID > 0)
                {
                    iTaskID = objAppUserTasks.InsertUserTask(DateTime.Now, AssetRequestID, Convert.ToInt16(lblEmpID.Text.Trim()), Convert.ToInt16(lblReportingManagerID.Text.Trim()), "New Asset request raised from employee code : \"" + txtEmpCode.Text.Trim() + "\" and employee name: \"" + txtEmpName.Text.Trim() + "\".", "Initiated", DateTime.Now.AddDays(7), "AssetNewRequest");
                    MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (lblActionMode.Text == "modify")
            {
                onlyChangedValues = AuditLogger.getUpdatedValues(_originalValues, updatedValues, false);

                AssetRequestID = objAssetInfo.UpdateAssetRequestInfo(Convert.ToInt32(lblAssetRequestID.Text.ToString()), Convert.ToInt32(lblAssetID.Text.ToString()), true, false, Convert.ToInt32(lblEmpID.Text.ToString()), Convert.ToDateTime(dtAssetRequestDate.Text.ToString()), txtComments.Text, false, Convert.ToDateTime(dtAssetRequestDate.Text.ToString()), Convert.ToInt32(lblReportingManagerID.Text.ToString()), false, "", false);
                AssetRequestRegisterID = objAssetRegister.UpdateAssetRegisterInfo(Convert.ToInt32(lblAssetRegID.Text.ToString()), Convert.ToInt32(lblAssetID.Text.ToString()), Convert.ToDateTime(dtAssetRequestDate.Text), 0, Convert.ToDecimal(txtQuantity.Text.ToString()), 0, Convert.ToDecimal(txtQuantity.Text.ToString()), "Rq", "By " + cmbAssetType.Text + " - Request", Convert.ToInt32(AssetRequestID));

                objAuditLog.InsertAuditLog(Convert.ToInt32(lblEmpID.Text.ToString()), AssetRequestID, "Asset Request - " + "\"ASR-REQ-" + (AssetRequestID).ToString().PadLeft(4, '0').Trim() + "\" Raised by Employee Code : \"" + txtEmpCode.Text.Trim() + "\" and Employee Name : \"" + txtEmpName.Text.Trim() + "\".", "Update", ModelStaffSync.CurrentUser.EmpName, "AssetRequestExistingUpdates", Convert.ToInt32(objTempClientFinYearInfo.ClientID));

                if (AssetRequestID > 0)
                {
                    iTaskID = objAppUserTasks.UpdateUserTask(Convert.ToInt32(lblTaskID.Text.ToString()), AssetRequestID, DateTime.Now, Convert.ToInt16(lblEmpID.Text.Trim()), Convert.ToInt16(lblReportingManagerID.Text.Trim()), "Asset Request - " + "\"ASR-REQ-" + (AssetRequestID).ToString().PadLeft(4, '0').Trim() + "\" Raised by Employee Code : \"" + txtEmpCode.Text.Trim() + "\" and Employee Name : \"" + txtEmpName.Text.Trim() + "\".", "Initiated", DateTime.Now.AddDays(7), "AssetNewRequest");
                    MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                foreach (var changedValues in onlyChangedValues)
                {
                    if (lblActionMode.Text == "modify")
                        objAuditLog.InsertAuditLog(Convert.ToInt32(CurrentUser.EmpID.ToString()), Convert.ToInt32(lblAssetRequestID.Text.ToString()), changedValues.ToString().Trim(), "Update", ModelStaffSync.CurrentUser.EmpName, strActionStatement, Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                }
            }

            objTempCurrentlyLoggedInUserInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objTempCurrentlyLoggedInUserInfo.EmpID.ToString()));

            onSaveButtonClick();
            disableControls();
            clearControls();
            errValidator.Clear();
            this.Cursor = Cursors.Default;
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

            lblActionMode.Text = "add";
            onGenerateButtonClick();
            clearControls();
            enableControls();
            errValidator.Clear();

            dtAssetRequestDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            cmbAssetType.DataSource = objAssetInfo.getAssetsInfoList(Convert.ToInt32(objTempClientFinYearInfo.ClientID));
            cmbAssetType.DisplayMember = "AssetName";
            cmbAssetType.ValueMember = "AssetID";
        }

        public void onGenerateButtonClick()
        {
            lblActionMode.Text = "add";
            lblEmpID.Text = "";
            lblReportingManagerID.Text = "";
            lblPersonalInfoID.Text = "";
            btnEmpSearch.Enabled = true;
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
            lblReportingManagerID.Text = "";
            lblPersonalInfoID.Text = "";
            btnEmpSearch.Enabled = true;
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
            lblReportingManagerID.Text = "";
            lblPersonalInfoID.Text = "";
            btnEmpSearch.Enabled = true;
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onSaveButtonClick()
        {
            lblActionMode.Text = "";
            lblReportingManagerID.Text = "";
            lblPersonalInfoID.Text = "";
            btnEmpSearch.Enabled = false;
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
            lblReportingManagerID.Text = "";
            lblPersonalInfoID.Text = "";
            btnEmpSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void clearControls()
        {
            lblEmpID.Text = "";
            txtEmpCode.Text = "";
            txtEmpName.Text = "";
            txtDesignation.Text = "";
            txtDepartment.Text = "";
            picEmpPhoto.Image = null;
            lblPersonalInfoID.Text = "";

            lblReportingManagerID.Text = "";
            txtRepCode.Text = "";
            txtRepName.Text = "";
            txtRepDesignation.Text = "";
            txtRepDepartment.Text = "";
            picRepEmpPhoto.Image = null;

            lblAssetRequestID.Text = "";
            txtQuantity.Text = "0";
            dtAssetRequestDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            cmbAssetType.DataSource = null;
            lblAssetID.Text = "";
            lblAssetRegID.Text = "";
            lblTaskID.Text = "";

            txtMaxLoanAmountAvail.Text = "0";
            txtComments.Text = "";

            lblRequestFromMailID.Text = "";
            lblRequestToMailID.Text = "";

            lnkViewAuditLog.Visible = false;
        }

        public void enableControls()
        {
            btnEmpSearch.Enabled = true;
            txtEmpCode.Enabled = false;
            txtEmpName.Enabled = false;
            txtDesignation.Enabled = false;
            txtDepartment.Enabled = false;

            txtRepCode.Enabled = false;
            txtRepName.Enabled = false;
            txtRepDesignation.Enabled = false;
            txtRepDepartment.Enabled = false;
            btnRepSearch.Enabled = true;

            cmbAssetType.Enabled = true;
            dtAssetRequestDate.Enabled = true;
            txtMaxLoanAmountAvail.Enabled = false;
            txtQuantity.Enabled = true;
            txtComments.Enabled = true;
        }

        public void disableControls()
        {
            btnEmpSearch.Enabled = false;
            txtEmpCode.Enabled = false;
            txtEmpName.Enabled = false;
            txtDesignation.Enabled = false;
            txtDepartment.Enabled = false;
            
            txtRepCode.Enabled = false;
            txtRepName.Enabled = false;
            txtRepDesignation.Enabled = false;
            txtRepDepartment.Enabled = false;
            btnRepSearch.Enabled = false;

            cmbAssetType.Enabled = false;
            txtQuantity.Enabled = false;
            dtAssetRequestDate.Enabled = false;
        }

        private void frmEmpAssetRequest_Load(object sender, EventArgs e)
        {
            clearControls();
            disableControls();
            onCancelButtonClick();
        }

        private void btnReportingManagerSearch_Click(object sender, EventArgs e)
        {
            
        }

        public void SelectedEmployeeID(string SearchOptionSelectedForm, int selectedEmployeeID)
        {
            if (SearchOptionSelectedForm == "listAssetRequestingUsers")
            {
                lblEmpID.Text = selectedEmployeeID.ToString();
                EmpPersonalPersonalInfo objSelectedPersonalInfo = objEmployeePersonalInfo.GetEmpPersonalPersonalInfo(Convert.ToInt16(lblEmpID.Text));
                lblPersonalInfoID.Text = objSelectedPersonalInfo.PersonalInfoID.ToString();
                lblRequestFromMailID.Text = objSelectedPersonalInfo.ContactNumber2.ToString();

                ReportingManagerInfo objEmpMaster = objEmployeeMaster.GetReportingManagerInfo(Convert.ToInt16(selectedEmployeeID.ToString()));
                txtEmpCode.Text = objEmpMaster.EmpCode;
                txtEmpName.Text = objEmpMaster.EmpName;
                txtDesignation.Text = objEmpMaster.DesignationTitle;
                txtDepartment.Text = objEmpMaster.DepartmentTitle;
                picEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(selectedEmployeeID.ToString())).EmpPhoto);

                EmployeeInfo objSelectedEmployeeInfo = objEmployeeMaster.GetSelectedEmployeeInfo(Convert.ToInt16(selectedEmployeeID.ToString()));
                lblReportingManagerID.Text = objSelectedEmployeeInfo.EmpRepManID.ToString();
                ReportingManagerInfo objReportingManagerInfo = objEmployeeMaster.GetReportingManagerInfo(Convert.ToInt16(lblReportingManagerID.Text));
                txtRepCode.Text = objReportingManagerInfo.EmpCode;
                txtRepName.Text = objReportingManagerInfo.EmpName;
                txtRepDesignation.Text = objReportingManagerInfo.DesignationTitle;
                txtRepDepartment.Text = objReportingManagerInfo.DepartmentTitle;
                picRepEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(lblReportingManagerID.Text)).EmpPhoto);
                EmpPersonalPersonalInfo objSelectedReportingManagerPersonalInfo = objEmployeePersonalInfo.GetEmpPersonalPersonalInfo(Convert.ToInt16(lblReportingManagerID.Text));
                lblRequestToMailID.Text = objSelectedReportingManagerPersonalInfo.ContactNumber2.ToString();

                cmbAssetType.SelectedIndex = 1;
                cmbAssetType.SelectedIndex = 0;

                _originalValues = AuditLogger.getOriginalValues(this);

                lnkViewAuditLog.Visible = true;
            }
            else if (SearchOptionSelectedForm == "listAssetRequestEditUsers")
            {
                lblAssetRequestID.Text = selectedEmployeeID.ToString();

                AssetRequestInfo objAssetRequestInfo = objAssetInfo.getSelectedSpecificAssetRequetInfo(selectedEmployeeID);

                ReportingManagerInfo objSelectedEmployeeInfo = objEmployeeMaster.GetReportingManagerInfo(Convert.ToInt32(objAssetRequestInfo.AssetRequestByID.ToString()));
                lblEmpID.Text = objSelectedEmployeeInfo.EmpID.ToString();
                txtEmpCode.Text = objSelectedEmployeeInfo.EmpCode;
                txtEmpName.Text = objSelectedEmployeeInfo.EmpName;
                txtDesignation.Text = objSelectedEmployeeInfo.DesignationTitle;
                txtDepartment.Text = objSelectedEmployeeInfo.DepartmentTitle;

                ReportingManagerInfo objReportingManagerInfo = objEmployeeMaster.GetReportingManagerInfo(Convert.ToInt32(objAssetRequestInfo.RequestedTo.ToString()));
                lblReportingManagerID.Text = objSelectedEmployeeInfo.EmpID.ToString(); 
                txtRepCode.Text = objReportingManagerInfo.EmpCode;
                txtRepName.Text = objReportingManagerInfo.EmpName;
                txtRepDesignation.Text = objReportingManagerInfo.DesignationTitle;
                txtRepDepartment.Text = objReportingManagerInfo.DepartmentTitle;
                picRepEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(lblReportingManagerID.Text)).EmpPhoto);
                
                cmbAssetType.SelectedIndex = objAssetRequestInfo.AssetID - 1;

                dtAssetRequestDate.Text = Convert.ToDateTime(objAssetRequestInfo.AssetRequestDate.ToString()).ToString("dd-MM-yyyy");
                txtComments.Text = Convert.ToString(objAssetRequestInfo.AssetRequestComments);
                AssetRegister objTempAssetRegister = objAssetRegister.getSpecificAssetRegisterInfo(Convert.ToInt32(objAssetRequestInfo.AssetID), Convert.ToDateTime(dtAssetRequestDate.Text.ToString()), Convert.ToInt32(lblAssetRequestID.Text.ToString()));
                lblAssetRegID.Text = objTempAssetRegister.AssetRegID.ToString();
                txtQuantity.Text = Convert.ToDecimal(objTempAssetRegister.CrBalance.ToString()).ToString();

                AppUserTasks objSpecificTask = objAppUserTasks.getSpecificTaskInfo(selectedEmployeeID, "Initiated", "AssetNewRequest");
                lblTaskID.Text = objSpecificTask.TaskID.ToString();

                _originalValues = AuditLogger.getOriginalValues(this);

                lnkViewAuditLog.Visible = true;
            }
        }

        private void btnModifyDetails_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            string strValidationMessage = objLogin.ValidateUserRolesAndResponsibilitiesInfo(objTempCurrentlyLoggedInUserInfo.EmpID, "update");
            if (strValidationMessage != "Success")
            {
                MessageBox.Show(strValidationMessage, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clearControls();
            onModifyButtonClick();
            enableControls();

            cmbAssetType.DataSource = objAssetInfo.getAssetsInfoList(Convert.ToInt32(objTempClientFinYearInfo.ClientID));
            cmbAssetType.DisplayMember = "AssetName";
            cmbAssetType.ValueMember = "AssetID";
        }

        private void btnRemoveDetails_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            string strValidationMessage = objLogin.ValidateUserRolesAndResponsibilitiesInfo(objTempCurrentlyLoggedInUserInfo.EmpID, "delete");
            if (strValidationMessage != "Success")
            {
                MessageBox.Show(strValidationMessage, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (lblActionMode.Text == "" || lblActionMode.Text == "remove")
            {
                lblActionMode.Text = "remove";
                onRemoveButtonClick();
                clearControls();
                //enableControls();
                errValidator.Clear();
            }
            else if (lblActionMode.Text == "delete")
            {
                if (MessageBox.Show("The selected record will be deleted. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int affectedRows = 1; // objEmployeeMaster.DeleteEmployeeMaster(Convert.ToInt16(lblReportingManagerID.Text.Trim()));
                    if (affectedRows > 0)
                        MessageBox.Show("Details deleted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                onCancelButtonClick();
                disableControls();
                clearControls();
                lblActionMode.Text = "";
                errValidator.Clear();
            }
        }

        private void btnViewCalender_Click(object sender, EventArgs e)
        {
            //frmAttendanceMater frmAttendanceMater = new frmAttendanceMater("listAttendanceMasterList", Convert.ToInt16(lblReportingManagerID.Text));
            //frmAttendanceMater frmAttendanceMater = new frmAttendanceMater();
            //frmAttendanceMater.ShowDialog(this);
            frmIndEmpAttendanceCalender frmIndEmpAttendanceCalender = new frmIndEmpAttendanceCalender(objTempCurrentlyLoggedInUserInfo, objTempClientFinYearInfo, Convert.ToInt16(lblReportingManagerID.Text.ToString()), Convert.ToDateTime("01-" + cmbAssetType.Text.Substring(0, 3) + "-" + cmbAssetType.Text.Substring(cmbAssetType.Text.IndexOf("-") + 2)));
            frmIndEmpAttendanceCalender.ShowDialog();
        }

        private void frmEmpAssetRequest_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (lblActionMode.Text != "")
                {
                    if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }
                objDashboard.lblDashboardTitle.Text = "Dashboard";
                objDashboard.sptrDashboardContainer.Visible = true;
                this.Close();
            }
        }

        private void btnEmpSearch_Click(object sender, EventArgs e)
        {
            if (lblActionMode.Text == "add")
            {
                frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listAssetRequestingUsers", Convert.ToInt32(lblClientID.Text.ToString()));
                frmEmployeeList.ShowDialog(this);
            }
            else if (lblActionMode.Text == "modify")
            {
                frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listAssetRequestEditUsers", Convert.ToInt32(lblClientID.Text.ToString()));
                frmEmployeeList.ShowDialog(this);
            }
        }

        private void btnRepSearch_Click(object sender, EventArgs e)
        {   
            if (lblActionMode.Text == "add")
            {
                //frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listAdvanceRequestToUsers");
                //frmEmployeeList.ShowDialog(this);
            }
            else if (lblActionMode.Text == "modify")
            {
                //frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listAdvanceRequestToUsers");
                //frmEmployeeList.ShowDialog(this);
            }
        }

        private void cmbAdvanceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lblEmpID.Text.Trim() == "")
                return;

            //objSpecificEmployeeSalaryInfo = objSalaryProfile.getSpecificEmployeeSalaryInfo(Convert.ToInt32(lblEmpID.Text.Trim()));
            //if(objSpecificEmployeeSalaryInfo != null)
            //{
            //    objAdvanceTypeConfigModel = objAdvanceTypeConfigInfo.GetAdvanceTypeConfigByID(Convert.ToInt32(cmbAssetType.SelectedIndex + 1));
            //    if (objAdvanceTypeConfigModel != null)
            //    {
            //        if (objAdvanceTypeConfigModel.BasedOnNetOrGross.ToString() == "Gross Salary")
            //        {
            //            if(objAdvanceTypeConfigModel.MaxPerOfNetOrGross.ToString() == "Percentage")
            //            {
            //                txtMaxLoanAmountAvail.Text = (((objSpecificEmployeeSalaryInfo.TotalAllowance + objSpecificEmployeeSalaryInfo.TotalReimbursement) * objAdvanceTypeConfigModel.MaxPercentage) / 100).ToString();
            //            }
            //            else if (objAdvanceTypeConfigModel.MaxPerOfNetOrGross.ToString() == "Fixed")
            //            {
            //                txtMaxLoanAmountAvail.Text = objAdvanceTypeConfigModel.MaxFixed.ToString();
            //            }
            //            txtAdvanceAmount.Text = Convert.ToDecimal(txtAdvanceAmount.Text.ToString()).ToString("###0.00");
            //            txtInstallmentAmount.Text = Convert.ToDecimal(txtInstallmentAmount.Text.ToString()).ToString("###0.00");
            //            txtMaxLoanAmountAvail.Text = Math.Round(Convert.ToDecimal(txtMaxLoanAmountAvail.Text.ToString()), 0).ToString();
            //            txtMaxLoanAmountAvail.Text = Convert.ToDecimal(txtMaxLoanAmountAvail.Text.ToString()).ToString("###0.00");
            //        }
            //        else if (objAdvanceTypeConfigModel.BasedOnNetOrGross.ToString() == "Net Salary")
            //        {
            //            if (objAdvanceTypeConfigModel.MaxPerOfNetOrGross.ToString() == "Percentage")
            //            {
            //                txtMaxLoanAmountAvail.Text = ((objSpecificEmployeeSalaryInfo.NetPayable * objAdvanceTypeConfigModel.MaxPercentage) / 100).ToString();
            //            }
            //            else if (objAdvanceTypeConfigModel.MaxPerOfNetOrGross.ToString() == "Fixed")
            //            {
            //                txtMaxLoanAmountAvail.Text = objAdvanceTypeConfigModel.MaxFixed.ToString();
            //            }
            //            txtAdvanceAmount.Text = Convert.ToDecimal(txtAdvanceAmount.Text.ToString()).ToString("###0.00");
            //            txtInstallmentAmount.Text = Convert.ToDecimal(txtInstallmentAmount.Text.ToString()).ToString("###0.00");
            //            txtMaxLoanAmountAvail.Text = Math.Round(Convert.ToDecimal(txtMaxLoanAmountAvail.Text.ToString()), 0).ToString();
            //            txtMaxLoanAmountAvail.Text = Convert.ToDecimal(txtMaxLoanAmountAvail.Text.ToString()).ToString("###0.00");
            //        }
            //    }
            //}
        }

        private void txtTenure_TextChanged(object sender, EventArgs e)
        {
            if (txtQuantity.Text.Trim() == "")
                return;
        }

        private void txtAdvanceAmount_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtAdvanceStartDate_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenure_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void txtAdvanceAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void picMoreInfo_Click(object sender, EventArgs e)
        {
            if (cmbAssetType.SelectedValue == null)
                return;

            frmAdvanceConfigInfoReadOnly frmViewAdvanceConfigInfoReadOnly = new frmAdvanceConfigInfoReadOnly(Convert.ToInt32(cmbAssetType.SelectedValue.ToString()));
            frmViewAdvanceConfigInfoReadOnly.ShowDialog(this);
        }

        private void txtComments_Enter(object sender, EventArgs e)
        {
        }

        private void lblViewEmpSpecificAdvanceInfo_Click(object sender, EventArgs e)
        {
            if(lblEmpID.Text.Trim() == "")
                return;

            //frmEmpAdvanceTRList frmEmpAdvanceTRList = new frmEmpAdvanceTRList(this, "beforeempadvanceoutstanding", 0, Convert.ToInt32(lblEmpID.Text.ToString()), 0);
            //frmEmpAdvanceTRList.ShowDialog(this);
        }

        private void txtAdvanceAmount_Leave(object sender, EventArgs e)
        {

        }

        private void txtInstallmentAmount_Leave(object sender, EventArgs e)
        {

        }

        private void lnkViewAuditLog_LinkClicked(object sender, EventArgs e)
        {
            frmAuditLogStatements objAuditLogStatements = new frmAuditLogStatements(Convert.ToInt32(lblPersonalInfoID.Text.ToString()), "AdvanceRequest", "Advance Request", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
            objAuditLogStatements.ShowDialog(this);
        }
    }
}
