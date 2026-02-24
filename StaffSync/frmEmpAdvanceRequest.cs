using DocumentFormat.OpenXml.Wordprocessing;
using ModelStaffSync;
using Quartz.Impl.AdoJobStore.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Quartz.Logging.OperationName;
//using static C1.Util.Win.Win32;

namespace StaffSync
{
    public partial class frmEmpAdvanceRequest : Form
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

        DateTime dos;
        string dateFormat = "dd-MM-yyyy";
        CultureInfo provider = CultureInfo.InvariantCulture;

        public frmEmpAdvanceRequest()
        {
            InitializeComponent();
        }

        public frmEmpAdvanceRequest(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmEmpAdvanceRequest(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
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

            if (cmbAdvanceType.Text.Trim() == "")
            {
                errValidator.SetError(cmbAdvanceType, "Please select the salary month");
                validationStatus = false;
            }
            else
            {
                errValidator.SetError(cmbAdvanceType, "");
            }

            // Date of Birth
            if (string.IsNullOrEmpty(txtAdvanceStartDate.Text))
            {
                validationStatus = false;
                errValidator.SetError(this.txtAdvanceStartDate, txtAdvanceStartDate.Tag?.ToString() ?? "Date of Salary is required.");
            }
            else if (txtAdvanceEndDate.Text.ToString().Trim() == "-  -")
            {
                validationStatus = false;
                errValidator.SetError(this.txtAdvanceStartDate, txtAdvanceStartDate.Tag?.ToString() ?? "Date of Salary is required.");
            }
            else if (!DateTime.TryParseExact(txtAdvanceStartDate.Text, dateFormat, provider, DateTimeStyles.None, out dos))
            {
                validationStatus = false;
                errValidator.SetError(this.txtAdvanceStartDate, "Invalid Date of Salary format (dd-MM-yyyy).");
            }
            //else if (dos > DateTime.Now.Date)
            //{
            //    validationStatus = false;
            //    errValidator.SetError(this.txtAdvanceStartDate, "Date of Salary cannot be in the future.");
            //}
            else
            {
                errValidator.SetError(this.txtAdvanceStartDate, "");
            }

            if (txtTenure.Text.Trim() == "" || Convert.ToDecimal(txtTenure.Text.Trim()) == 0)
            {
                errValidator.SetError(txtTenure, "Please enter the total working days");
                validationStatus = false;
            }
            else if (Convert.ToDecimal(txtTenure.Text.Trim()) > Convert.ToDecimal(objAdvanceTypeConfigModel.MaxTenure))
            {
                errValidator.SetError(txtTenure, "Tenure cannot be greater than maximum allowed tenure");
                validationStatus = false;
            }
            else
            {
                errValidator.SetError(txtTenure, "");
            }

            if (txtAdvanceAmount.Text.Trim() == "" || Convert.ToDecimal(txtAdvanceAmount.Text.Trim()) == 0)
            {
                errValidator.SetError(txtAdvanceAmount, "Please enter the Advance Amount");
                validationStatus = false;
            }
            else if (Convert.ToDecimal(txtAdvanceAmount.Text.Trim()) > Convert.ToDecimal(txtMaxLoanAmountAvail.Text.ToString()))
            {
                errValidator.SetError(txtAdvanceAmount, "Advance Amount cannot be greater than maximum allowed amount");
                validationStatus = false;
            }
            else
            {
                errValidator.SetError(txtAdvanceAmount, "");
            }

            if (txtInstallmentAmount.Text.Trim() == "" || Convert.ToDecimal(txtInstallmentAmount.Text.Trim()) == 0)
            {
                errValidator.SetError(txtInstallmentAmount, "Please enter the Installment Amount");
                validationStatus = false;
            }
            else
            {
                errValidator.SetError(txtInstallmentAmount, "");
            }

            if(txtAdvanceStartDate.Text .Trim() != "" && txtAdvanceEndDate.Text.Trim() != "")
            {
                DateTime advanceStartDate = DateTime.ParseExact(txtAdvanceStartDate.Text.Trim(), dateFormat, provider);
                DateTime advanceEndDate = DateTime.ParseExact(txtAdvanceEndDate.Text.Trim(), dateFormat, provider);
                //if (advanceStartDate > DateTime.Today)
                //{
                //    errValidator.SetError(txtAdvanceEndDate, "Advance Start Date cannot be future date");
                //    validationStatus = false;
                //} else 
                if (advanceEndDate < advanceStartDate)
                {
                    errValidator.SetError(txtAdvanceEndDate, "Advance End Date cannot be earlier than Advance Start Date");
                    validationStatus = false;
                }
                else
                {
                    errValidator.SetError(txtAdvanceEndDate, "");
                }
            }

            return validationStatus;
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(objAppSettings.GetSpecificAppSettingsInfo("Allow Multiple Advance Requests").AppSettingValue.ToString()) == false)
            {
                if (objAdvanceTransaction.IsAdvanceAlreadyExist(Convert.ToInt16(lblPersonalInfoID.Text.Trim())) && lblActionMode.Text == "add")
                {
                    MessageBox.Show("An advance request already exists for the selected employee. Please modify the existing request or select a different employee.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            int EmpAdvanceRequestID = 0;
            decimal AllowanceAmount = 0;
            decimal DeductionAmount = 0;
            decimal ReimbursmentAmount = 0;
            int iRowCounter = 1;
            int iTaskID = 0;

            if (lblActionMode.Text == "add")
            {
                EmpAdvanceRequestID = objAdvanceTransaction.InsertEmpAdvanceRequestMas(Convert.ToInt16(lblPersonalInfoID.Text.Trim()), Convert.ToInt16(cmbAdvanceType.SelectedValue.ToString()), true, false, 
                    DateTime.Now, "Requested", Convert.ToInt16(lblReportingManagerID.Text.Trim()), DateTime.Now, false, "Pending", Convert.ToInt16(lblReportingManagerID.Text.Trim()), DateTime.Now, false, "Pending", 
                    DateTime.Now, false, Convert.ToDecimal(txtAdvanceAmount.Text.Trim()), Convert.ToDecimal(txtTenure.Text.Trim()), Convert.ToDecimal(txtInstallmentAmount.Text.Trim()), 
                    DateTime.ParseExact(txtAdvanceStartDate.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(txtAdvanceEndDate.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture));

                objAuditLog.InsertAuditLog(Convert.ToInt32(lblEmpID.Text.ToString()), EmpAdvanceRequestID, "Advance Request - " + "\"ADV-REQ-" + (EmpAdvanceRequestID).ToString().PadLeft(4, '0').Trim() + "\" Raised by Employee Code : \"" + txtEmpCode.Text.Trim() + "\" and Employee Name : \"" + txtEmpName.Text.Trim() + "\".", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestRaised");

                if (EmpAdvanceRequestID > 0)
                {
                     iTaskID = objAppUserTasks.InsertUserTask(DateTime.Now, EmpAdvanceRequestID, Convert.ToInt16(lblEmpID.Text.Trim()), Convert.ToInt16(lblReportingManagerID.Text.Trim()), "New advance request raised from employee code : \"" + txtEmpCode.Text.Trim() + "\" and employee name: \"" + txtEmpName.Text.Trim() + "\".", "Initiated", DateTime.Now.AddDays(7), "Advance Request");
                    MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (lblActionMode.Text == "modify")
            {
                EmpAdvanceRequestID = objAdvanceTransaction.UpdateEmpAdvanceRequestMas(Convert.ToInt32(lblPersonalInfoID.Text.ToString()), Convert.ToInt16(lblPersonalInfoID.Text.Trim()),
                    true, false, Convert.ToInt16(cmbAdvanceType.SelectedValue.ToString()), DateTime.Now, "Requested", Convert.ToInt16(lblReportingManagerID.Text.Trim()), DateTime.Now, false, 
                    "Pending", Convert.ToInt16(lblReportingManagerID.Text.Trim()), DateTime.Now, false, "Pending", DateTime.Now, false, Convert.ToDecimal(txtAdvanceAmount.Text.Trim()), Convert.ToDecimal(txtTenure.Text.Trim()), 
                    Convert.ToDecimal(txtInstallmentAmount.Text.Trim()), DateTime.ParseExact(txtAdvanceStartDate.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture), 
                    DateTime.ParseExact(txtAdvanceEndDate.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture));

                objAuditLog.InsertAuditLog(Convert.ToInt32(lblEmpID.Text.ToString()), EmpAdvanceRequestID, "Advance Request - " + "\"ADV-REQ-" + (EmpAdvanceRequestID).ToString().PadLeft(4, '0').Trim() + "\" Raised by Employee Code : \"" + txtEmpCode.Text.Trim() + "\" and Employee Name : \"" + txtEmpName.Text.Trim() + "\".", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestRaised");

                if (EmpAdvanceRequestID > 0)
                    MessageBox.Show("Details updated successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            txtAdvanceStartDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtAdvanceEndDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            cmbAdvanceType.DataSource = objAdvanceTypeMas.GetAdvanceTypeList(Convert.ToInt32(objTempClientFinYearInfo.ClientID));
            cmbAdvanceType.DisplayMember = "AdvanceTypeTitle";
            cmbAdvanceType.ValueMember = "AdvanceTypeID";
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

            txtTenure.Text = "0";
            txtAdvanceStartDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtAdvanceEndDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            cmbAdvanceType.DataSource = null;

            txtMaxLoanAmountAvail.Text = "0";
            txtAdvanceAmount.Text = "0";
            txtInstallmentAmount.Text = "0";
            txtComments.Text = "";

            lblRequestFromMailID.Text = "";
            lblRequestToMailID.Text = "";
        }

        public void enableControls()
        {
            btnEmpSearch.Enabled = true;
            txtEmpCode.Enabled = false;
            txtEmpName.Enabled = false;
            txtDesignation.Enabled = false;
            txtDepartment.Enabled = false;
            cmbAdvanceType.Enabled = false;

            txtRepCode.Enabled = false;
            txtRepName.Enabled = false;
            txtRepDesignation.Enabled = false;
            txtRepDepartment.Enabled = false;
            btnRepSearch.Enabled = true;

            cmbAdvanceType.Enabled = true;
            txtAdvanceAmount.Enabled = true;
            txtInstallmentAmount.Enabled = true;
            txtAdvanceStartDate.Enabled = true;
            txtAdvanceEndDate.Enabled = true;
            txtMaxLoanAmountAvail.Enabled = false;
            txtTenure.Enabled = true;
            txtComments.Enabled = true;
        }

        public void disableControls()
        {
            btnEmpSearch.Enabled = false;
            txtEmpCode.Enabled = false;
            txtEmpName.Enabled = false;
            txtDesignation.Enabled = false;
            txtDepartment.Enabled = false;
            cmbAdvanceType.Enabled = false;

            txtRepCode.Enabled = false;
            txtRepName.Enabled = false;
            txtRepDesignation.Enabled = false;
            txtRepDepartment.Enabled = false;
            btnRepSearch.Enabled = false;

            cmbAdvanceType.Enabled = false;
            txtAdvanceAmount.Enabled = false;
            txtInstallmentAmount.Enabled = false;
            txtMaxLoanAmountAvail.Enabled = false;
            txtTenure.Enabled = false;
            txtAdvanceStartDate.Enabled = false;
            txtAdvanceEndDate.Enabled = false;
        }

        private void frmEmpAdvanceRequest_Load(object sender, EventArgs e)
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
            if (SearchOptionSelectedForm == "listAdvanceRequestingUsers")
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

                cmbAdvanceType.SelectedIndex = 1;
                cmbAdvanceType.SelectedIndex = 0;
            }
            else if (SearchOptionSelectedForm == "listAdvanceRequestToUsers")
            {
                EmployeeInfo objSelectedEmployeeInfo = objEmployeeMaster.GetSelectedEmployeeInfo(Convert.ToInt16(selectedEmployeeID.ToString()));
                lblReportingManagerID.Text = objSelectedEmployeeInfo.EmpRepManID.ToString();
                ReportingManagerInfo objReportingManagerInfo = objEmployeeMaster.GetReportingManagerInfo(Convert.ToInt16(lblReportingManagerID.Text));
                txtRepCode.Text = objReportingManagerInfo.EmpCode;
                txtRepName.Text = objReportingManagerInfo.EmpName;
                txtRepDesignation.Text = objReportingManagerInfo.DesignationTitle;
                txtRepDepartment.Text = objReportingManagerInfo.DepartmentTitle;
                picRepEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(lblReportingManagerID.Text)).EmpPhoto);
                cmbAdvanceType.SelectedIndex = 0;
                EmpPersonalPersonalInfo objSelectedReportingManagerPersonalInfo = objEmployeePersonalInfo.GetEmpPersonalPersonalInfo(Convert.ToInt16(lblReportingManagerID.Text));
                lblRequestToMailID.Text = objSelectedReportingManagerPersonalInfo.ContactNumber2.ToString();
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
            
            onModifyButtonClick();
            enableControls();

            cmbAdvanceType.DataSource = objAdvanceTypeMas.GetAdvanceTypeList(Convert.ToInt32(objTempClientFinYearInfo.ClientID));
            cmbAdvanceType.DisplayMember = "AdvanceTypeTitle";
            cmbAdvanceType.ValueMember = "AdvanceTypeID";
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
            frmIndEmpAttendanceCalender frmIndEmpAttendanceCalender = new frmIndEmpAttendanceCalender(objTempCurrentlyLoggedInUserInfo, objTempClientFinYearInfo, Convert.ToInt16(lblReportingManagerID.Text.ToString()), Convert.ToDateTime("01-" + cmbAdvanceType.Text.Substring(0, 3) + "-" + cmbAdvanceType.Text.Substring(cmbAdvanceType.Text.IndexOf("-") + 2)));
            frmIndEmpAttendanceCalender.ShowDialog();
        }

        private void frmEmpAdvanceRequest_KeyDown(object sender, KeyEventArgs e)
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
                this.Close();
            }
        }

        private void btnEmpSearch_Click(object sender, EventArgs e)
        {
            if (lblActionMode.Text == "add")
            {
                frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listAdvanceRequestingUsers");
                frmEmployeeList.ShowDialog(this);
            }
            else if (lblActionMode.Text == "modify")
            {
                frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listAdvanceRequestingUsers");
                frmEmployeeList.ShowDialog(this);
            }
        }

        private void btnRepSearch_Click(object sender, EventArgs e)
        {   
            if (lblActionMode.Text == "add")
            {
                frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listAdvanceRequestToUsers");
                frmEmployeeList.ShowDialog(this);
            }
            else if (lblActionMode.Text == "modify")
            {
                frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listAdvanceRequestToUsers");
                frmEmployeeList.ShowDialog(this);
            }
        }

        private void cmbAdvanceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lblEmpID.Text.Trim() == "")
                return;

            objSpecificEmployeeSalaryInfo = objSalaryProfile.getSpecificEmployeeSalaryInfo(Convert.ToInt32(lblEmpID.Text.Trim()));
            if(objSpecificEmployeeSalaryInfo != null)
            {
                objAdvanceTypeConfigModel = objAdvanceTypeConfigInfo.GetAdvanceTypeConfigByID(Convert.ToInt32(cmbAdvanceType.SelectedIndex + 1));
                if (objAdvanceTypeConfigModel != null)
                {
                    if (objAdvanceTypeConfigModel.BasedOnNetOrGross.ToString() == "Gross Salary")
                    {
                        if(objAdvanceTypeConfigModel.MaxPerOfNetOrGross.ToString() == "Percentage")
                        {
                            txtMaxLoanAmountAvail.Text = (((objSpecificEmployeeSalaryInfo.TotalAllowance + objSpecificEmployeeSalaryInfo.TotalReimbursement) * objAdvanceTypeConfigModel.MaxPercentage) / 100).ToString();
                        }
                        else if (objAdvanceTypeConfigModel.MaxPerOfNetOrGross.ToString() == "Fixed")
                        {
                            txtMaxLoanAmountAvail.Text = objAdvanceTypeConfigModel.MaxFixed.ToString();
                        }
                        txtAdvanceAmount.Text = Convert.ToDecimal(txtAdvanceAmount.Text.ToString()).ToString("###0.00");
                        txtInstallmentAmount.Text = Convert.ToDecimal(txtInstallmentAmount.Text.ToString()).ToString("###0.00");
                        txtMaxLoanAmountAvail.Text = Math.Round(Convert.ToDecimal(txtMaxLoanAmountAvail.Text.ToString()), 0).ToString();
                        txtMaxLoanAmountAvail.Text = Convert.ToDecimal(txtMaxLoanAmountAvail.Text.ToString()).ToString("###0.00");
                    }
                    else if (objAdvanceTypeConfigModel.BasedOnNetOrGross.ToString() == "Net Salary")
                    {
                        if (objAdvanceTypeConfigModel.MaxPerOfNetOrGross.ToString() == "Percentage")
                        {
                            txtMaxLoanAmountAvail.Text = ((objSpecificEmployeeSalaryInfo.NetPayable * objAdvanceTypeConfigModel.MaxPercentage) / 100).ToString();
                        }
                        else if (objAdvanceTypeConfigModel.MaxPerOfNetOrGross.ToString() == "Fixed")
                        {
                            txtMaxLoanAmountAvail.Text = objAdvanceTypeConfigModel.MaxFixed.ToString();
                        }
                        txtAdvanceAmount.Text = Convert.ToDecimal(txtAdvanceAmount.Text.ToString()).ToString("###0.00");
                        txtInstallmentAmount.Text = Convert.ToDecimal(txtInstallmentAmount.Text.ToString()).ToString("###0.00");
                        txtMaxLoanAmountAvail.Text = Math.Round(Convert.ToDecimal(txtMaxLoanAmountAvail.Text.ToString()), 0).ToString();
                        txtMaxLoanAmountAvail.Text = Convert.ToDecimal(txtMaxLoanAmountAvail.Text.ToString()).ToString("###0.00");
                    }
                }
            }
        }

        private void txtTenure_TextChanged(object sender, EventArgs e)
        {
            if (txtTenure.Text.Trim() == "")
                return;
            if (txtAdvanceAmount.Text.Trim() == "")
                return;
            if (txtInstallmentAmount.Text.Trim() == "")
                return;
            if (string.IsNullOrEmpty(txtAdvanceStartDate.Text))
                return;
            if (txtAdvanceEndDate.Text.ToString().Trim() == "-  -")
                return;
            if (!DateTime.TryParseExact(txtAdvanceStartDate.Text, dateFormat, provider, DateTimeStyles.None, out dos))
                return;

            if (Convert.ToDecimal(txtTenure.Text.ToString()) >  Convert.ToDecimal(objAdvanceTypeConfigModel.MaxTenure.ToString()))
            {
                errValidator.SetError(txtTenure, "Tenure cannot be greater than maximum allowed tenure.");
                return;
            }
            else
            {
                errValidator.SetError(txtTenure, "");

            }

            if (Convert.ToDecimal(txtAdvanceAmount.Text.ToString()) > Convert.ToDecimal(txtMaxLoanAmountAvail.Text.ToString()))
            {
                errValidator.SetError(txtAdvanceAmount, "Advance Amount cannot be greater than maximum allowed advance amount");
                return;
            }
            else
            {
                errValidator.SetError(txtAdvanceAmount, "");

            }

            txtAdvanceEndDate.Text = Convert.ToDateTime(txtAdvanceStartDate.Text.ToString()).Date.AddMonths(Convert.ToInt32(txtTenure.Text.Trim())).ToString("dd-MM-yyyy");

            if (txtTenure.Text.Trim() != "" && txtAdvanceAmount.Text.Trim() != "")
            {
                decimal advanceAmount = Convert.ToDecimal(txtAdvanceAmount.Text.Trim());
                decimal tenure = Convert.ToDecimal(txtTenure.Text.Trim());
                if (tenure > 0)
                {
                    decimal calculatedInstallmentAmount = advanceAmount / tenure;
                    txtInstallmentAmount.Text = Math.Round(calculatedInstallmentAmount, 0).ToString();
                }
            }

            if (txtAdvanceAmount.Text.Trim() != "" && txtInstallmentAmount.Text.Trim() != "" && txtTenure.Text.Trim() != "")
            {
                decimal advanceAmount = Convert.ToDecimal(txtAdvanceAmount.Text.Trim());
                decimal installmentAmount = Convert.ToDecimal(txtInstallmentAmount.Text.Trim());
                decimal maxTenure = Convert.ToDecimal(objAdvanceTypeConfigModel.MaxTenure);
                if (installmentAmount > 0)
                {
                    decimal calculatedTenure = advanceAmount / installmentAmount;
                    if (calculatedTenure > maxTenure)
                    {
                        errValidator.SetError(txtInstallmentAmount, "Calculated tenure exceeds maximum allowed tenure.");
                    }
                    else
                    {
                        errValidator.SetError(txtInstallmentAmount, "");
                    }
                }
            }
        }

        private void txtAdvanceAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtTenure.Text.Trim() == "")
                return;
            if (txtAdvanceAmount.Text.Trim() == "")
                return;
            if (txtInstallmentAmount.Text.Trim() == "")
                return;

            if (Convert.ToDecimal(txtTenure.Text.ToString()) > Convert.ToDecimal(objAdvanceTypeConfigModel.MaxTenure.ToString()))
            {
                errValidator.SetError(txtTenure, "Tenure cannot be greater than maximum allowed tenure.");
                return;
            }
            else
            {
                errValidator.SetError(txtTenure, "");

            }

            if (Convert.ToDecimal(txtAdvanceAmount.Text.ToString()) > Convert.ToDecimal(txtMaxLoanAmountAvail.Text.ToString()))
            {
                errValidator.SetError(txtAdvanceAmount, "Advance Amount cannot be greater than maximum allowed Advance Amount");
                return;
            }
            else
            {
                errValidator.SetError(txtAdvanceAmount, "");

            }

            txtAdvanceStartDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtAdvanceEndDate.Text = Convert.ToDateTime(txtAdvanceStartDate.Text.ToString()).Date.AddMonths(Convert.ToInt32(txtTenure.Text.Trim())).ToString("dd-MM-yyyy");

            if (txtTenure.Text.Trim() != "" && txtAdvanceAmount.Text.Trim() != "")
            {
                decimal advanceAmount = Convert.ToDecimal(txtAdvanceAmount.Text.Trim());
                decimal tenure = Convert.ToDecimal(txtTenure.Text.Trim());
                if (tenure > 0)
                {
                    decimal calculatedInstallmentAmount = advanceAmount / tenure;
                    txtInstallmentAmount.Text = Math.Round(calculatedInstallmentAmount, 0).ToString();
                }
            }

            if (txtAdvanceAmount.Text.Trim() != "" && txtInstallmentAmount.Text.Trim() != "" && txtTenure.Text.Trim() != "")
            {
                decimal advanceAmount = Convert.ToDecimal(txtAdvanceAmount.Text.Trim());
                decimal installmentAmount = Convert.ToDecimal(txtInstallmentAmount.Text.Trim());
                decimal maxTenure = Convert.ToDecimal(objAdvanceTypeConfigModel.MaxTenure);
                if (installmentAmount > 0)
                {
                    decimal calculatedTenure = advanceAmount / installmentAmount;
                    if (calculatedTenure > maxTenure)
                    {
                        errValidator.SetError(txtInstallmentAmount, "Calculated tenure exceeds maximum allowed tenure.");
                    }
                    else
                    {
                        errValidator.SetError(txtInstallmentAmount, "");
                    }
                }
            }
        }

        private void txtAdvanceStartDate_TextChanged(object sender, EventArgs e)
        {
            if (txtTenure.Text.Trim() == "")
                return;
            if (txtAdvanceAmount.Text.Trim() == "")
                return;
            if (txtInstallmentAmount.Text.Trim() == "")
                return;
            if (string.IsNullOrEmpty(txtAdvanceStartDate.Text))
                return;
            if (txtAdvanceEndDate.Text.ToString().Trim() == "-  -")
                return;
            if (!DateTime.TryParseExact(txtAdvanceStartDate.Text, dateFormat, provider, DateTimeStyles.None, out dos))
                return;

            txtAdvanceEndDate.Text = Convert.ToDateTime(txtAdvanceStartDate.Text.ToString()).Date.AddMonths(Convert.ToInt32(txtTenure.Text.Trim())).ToString("dd-MM-yyyy");
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
            if (cmbAdvanceType.SelectedValue == null)
                return;

            frmAdvanceConfigInfoReadOnly frmViewAdvanceConfigInfoReadOnly = new frmAdvanceConfigInfoReadOnly(Convert.ToInt32(cmbAdvanceType.SelectedValue.ToString()));
            frmViewAdvanceConfigInfoReadOnly.ShowDialog(this);
        }

        private void txtComments_Enter(object sender, EventArgs e)
        {
            if (txtComments.Text == "")
                txtComments.Text = "Request to approve the \"" + cmbAdvanceType.Text + "\" of " + Convert.ToDecimal(txtAdvanceAmount.Text.ToString()).ToString("#0.00") + "/- which will be paid back in " + txtTenure.Text + " months starting from \"" + Convert.ToDateTime(txtAdvanceStartDate.Text.ToString()).Date.ToString("dd-MMM-yyyy") + " and ends on \"" + Convert.ToDateTime(txtAdvanceEndDate.Text.ToString()).Date.ToString("dd-MMM-yyyy") + "\".";
        }

        private void lblViewEmpSpecificAdvanceInfo_Click(object sender, EventArgs e)
        {
            if(lblEmpID.Text.Trim() == "")
                return;

            frmEmpAdvanceTRList frmEmpAdvanceTRList = new frmEmpAdvanceTRList(this, "beforeempadvanceoutstanding", 0, Convert.ToInt32(lblEmpID.Text.ToString()), 0);
            frmEmpAdvanceTRList.ShowDialog(this);
        }

        private void txtAdvanceAmount_Leave(object sender, EventArgs e)
        {
            if (txtAdvanceAmount.Text.ToString().Trim() == "")
                return;

            txtAdvanceAmount.Text = Convert.ToDecimal(txtAdvanceAmount.Text.ToString()).ToString("###0.00");
            txtInstallmentAmount.Text = Convert.ToDecimal(txtInstallmentAmount.Text.ToString()).ToString("###0.00");
        }

        private void txtInstallmentAmount_Leave(object sender, EventArgs e)
        {
            if (txtInstallmentAmount.Text.ToString().Trim() == "")
                return;

            txtInstallmentAmount.Text = Convert.ToDecimal(txtInstallmentAmount.Text.ToString()).ToString("###0.00");
        }
    }
}
