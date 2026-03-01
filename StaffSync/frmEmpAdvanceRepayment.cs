using Common;
using DocumentFormat.OpenXml.Wordprocessing;
using Krypton.Ribbon;
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
    public partial class frmEmpAdvanceRepayment : Form
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

        public frmEmpAdvanceRepayment()
        {
            InitializeComponent();
        }

        public frmEmpAdvanceRepayment(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmEmpAdvanceRepayment(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
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
            objDashboard.sptrDashboardContainer.Visible = true;
            this.Close();
        }

        private bool ValidateValuesOnUI()
        {
            bool validationStatus = true;
            errValidator.Clear();

            if (string.IsNullOrEmpty(lblEmpID.Text))
            {
                validationStatus = false;
                errValidator.SetError(this.txtEmpCode, txtEmpCode.Tag?.ToString() ?? "Select an Employee Information to continue.");
                return validationStatus;
            }
            if (string.IsNullOrEmpty(txtAdvanceTRDate.Text))
            {
                validationStatus = false;
                errValidator.SetError(this.txtAdvanceTRDate, txtAdvanceTRDate.Tag?.ToString() ?? "Repayment Date is required.");
            }
            else if (txtAdvanceTRDate.Text.ToString().Trim() == "-  -")
            {
                validationStatus = false;
                errValidator.SetError(this.txtAdvanceTRDate, txtAdvanceTRDate.Tag?.ToString() ?? "Repayment Date is required.");
            }
            else if (!DateTime.TryParseExact(txtAdvanceTRDate.Text, dateFormat, provider, DateTimeStyles.None, out dos))
            {
                validationStatus = false;
                errValidator.SetError(this.txtAdvanceTRDate, "Invalid Repayment Date format (dd-MM-yyyy).");
            }
            else if (dos < Convert.ToDateTime(lblLastTRDate.Text.ToString()).Date)
            {
                validationStatus = false;
                errValidator.SetError(this.txtAdvanceTRDate, "Repayment Date cannot be less than previous transaction date.");
            }
            else if (dos > DateTime.Now.Date)
            {
                validationStatus = false;
                errValidator.SetError(this.txtAdvanceTRDate, "Repayment Date cannot be in the future.");
            }
            else
            {
                errValidator.SetError(this.txtAdvanceTRDate, "");
            }

            if(Convert.ToDecimal(txtClosingBalance.Text.Trim()) - Convert.ToDecimal(txtAdvanceTRAmount.Text) < 0)
            {
                errValidator.SetError(txtAdvanceTRAmount, "Re-pay Advance Amount cannot be greater than Closing Balance.");
                validationStatus = false;
            }
            else if (Convert.ToDecimal(txtClosingBalance.Text.Trim()) - Convert.ToDecimal(txtAdvanceTRAmount.Text) == 0)
            {
                if (MessageBox.Show("The " + txtAdvanceType.Text + " " + Convert.ToDecimal(txtAdvanceAmount.Text.ToString()).ToString("#,##0.00") + " will get completely recovered.\nPlease make a note and notify the Employee." , "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {

                }
            }
            else if (Convert.ToDecimal(txtAdvanceTRAmount.Text.Trim()) < 0)
            {
                errValidator.SetError(txtAdvanceTRAmount, "Re-pay Advance Amount cannot be negative.");
                validationStatus = false;
            }
            else if (Convert.ToDecimal(txtAdvanceTRAmount.Text.Trim()) > Convert.ToDecimal(txtInstallmentAmount.Text.Trim()))
            {
                errValidator.SetError(txtAdvanceTRAmount, "Re-pay Advance Amount cannot be greater than Installment Amount.");
                validationStatus = false;
            }
            else if (Convert.ToDecimal(txtAdvanceTRAmount.Text.Trim()) > Convert.ToDecimal(txtClosingBalance.Text.Trim()))
            {
                errValidator.SetError(txtAdvanceTRAmount, "Re-pay Advance Amount cannot be greater than Closing Balance.");
                validationStatus = false;
            }
            else if (Convert.ToDecimal(txtAdvanceTRAmount.Text.Trim()) == 0)
            {
                errValidator.SetError(txtAdvanceTRAmount, "Re-pay Advance Amount cannot be zero.");
                validationStatus = false;
            }
            else
            {
                errValidator.SetError(txtAdvanceTRAmount, "");
            }

            return validationStatus;
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {            
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

            if(lblActionMode.Text == "add")
            {
                int newTransactionID = objAdvanceTransaction.InsertAdvanceTransaction(txtAdvanceRequestCode.Text, Convert.ToInt32(lblEmpAdvanceRequestID.Text.ToString()), Convert.ToDateTime(txtAdvanceTRDate.Text.Trim()), Convert.ToDecimal(txtClosingBalance.Text.Trim()), 0, Convert.ToDecimal(txtAdvanceTRAmount.Text), Convert.ToDecimal(txtClosingBalance.Text.Trim()) - Convert.ToDecimal(txtAdvanceTRAmount.Text), cmbPaymentType.SelectedItem.ToString(), txtComments.Text.Trim(), 0);
                if (newTransactionID > 0)
                {
                    if (Convert.ToDecimal(txtClosingBalance.Text.Trim()) - Convert.ToDecimal(txtAdvanceTRAmount.Text) == 0)
                    {
                        objAdvanceTransaction.CloseEmployeeSpecificAdvanceRequest(Convert.ToInt32(lblEmpAdvanceRequestID.Text.ToString()));
                    }

                    objAuditLog.InsertAuditLog(Convert.ToInt32(lblEmpID.Text.ToString()), newTransactionID, txtComments.Text, ModelStaffSync.CurrentUser.EmpName, "AdvanceAmountRepayment");
                    MessageBox.Show("Advance repayment details saved successfully.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


            txtAdvanceTRDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            //cmbPaymentMode.DataSource = objAdvanceTypeMas.GetAdvanceTypeList(Convert.ToInt32(objTempClientFinYearInfo.ClientID));
            //cmbPaymentMode.DisplayMember = "AdvanceTypeTitle";
            //cmbPaymentMode.ValueMember = "AdvanceTypeID";
        }

        public void onGenerateButtonClick()
        {
            lblActionMode.Text = "add";
            lblEmpID.Text = "";
            lblPersonalInfoID.Text = "";
            lblEmpMailID.Text = "";
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
            lblPersonalInfoID.Text = "";
            lblEmpMailID.Text = "";
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
            lblPersonalInfoID.Text = "";
            lblEmpMailID.Text = "";
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
            lblPersonalInfoID.Text = "";
            lblEmpMailID.Text = "";
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
            lblEmpAdvanceRequestID.Text = "";
            lblLastTRDate.Text = "";

            txtAdvanceRequestCode.Text = "";
            txtAdvanceType.Text = "";
            txtAdvanceAmount.Text = "0.00";
            txtInstallmentAmount.Text = "";
            txtAdvanceStartDate.Text = "";
            txtAdvanceEndDate.Text = "";
            txtClosingBalance.Text = "0.00";
            txtCBalance.Text = "0.00";
            txtAdvanceTRAmount.Text = "0.00";
            txtComments.Text = "";

            cmbPaymentType.Items.Clear();
            txtAdvanceTRDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            lblEmpMailID.Text = "";
        }

        public void enableControls()
        {
            btnEmpSearch.Enabled = true;
            txtEmpCode.Enabled = false;
            txtEmpName.Enabled = false;
            txtDesignation.Enabled = false;
            txtDepartment.Enabled = false;
            cmbPaymentType.Enabled = false;

            txtAdvanceRequestCode.Enabled = false;
            txtAdvanceType.Enabled = false;
            txtAdvanceAmount.Enabled = false;
            txtInstallmentAmount.Enabled = false;
            txtAdvanceStartDate.Enabled = false;
            txtAdvanceEndDate.Enabled = false;
            txtClosingBalance.Enabled = false;
            txtCBalance.Enabled = false;

            cmbPaymentType.Enabled = true;
            txtAdvanceTRAmount.Enabled = true;
            txtAdvanceTRDate.Enabled = true;
            txtComments.Enabled = true;

            cmbPaymentType.Items.Clear();
            cmbPaymentType.Items.Add("");
            cmbPaymentType.Items.Add("Cr");
            cmbPaymentType.Items.Add("Dr");
            cmbPaymentType.SelectedIndex = 2;

            cmbPaymentMode.Items.Clear();
            cmbPaymentMode.Items.Add("");
            cmbPaymentMode.Items.Add("By Cash");
            cmbPaymentMode.Items.Add("By Bank Transfer");
            cmbPaymentMode.Items.Add("By Check");
            cmbPaymentMode.SelectedIndex = 1;
        }

        public void disableControls()
        {
            btnEmpSearch.Enabled = false;
            txtEmpCode.Enabled = false;
            txtEmpName.Enabled = false;
            txtDesignation.Enabled = false;
            txtDepartment.Enabled = false;
            cmbPaymentType.Enabled = false;

            txtAdvanceRequestCode.Enabled = false;
            txtAdvanceType.Enabled = false;
            txtAdvanceAmount.Enabled = false;
            txtInstallmentAmount.Enabled = false;
            txtAdvanceStartDate.Enabled = false;
            txtAdvanceEndDate.Enabled = false;
            txtClosingBalance.Enabled = false;
            txtCBalance.Enabled = false;
            txtComments.Enabled = false;

            cmbPaymentType.Items.Clear();

            cmbPaymentType.Enabled = false;
            txtAdvanceTRAmount.Enabled = false;
            txtAdvanceTRDate.Enabled = false;
        }

        private void frmEmpAdvanceRepayment_Load(object sender, EventArgs e)
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

            //cmbPaymentMode.DataSource = objAdvanceTypeMas.GetAdvanceTypeList(Convert.ToInt32(objTempClientFinYearInfo.ClientID));
            //cmbPaymentMode.DisplayMember = "AdvanceTypeTitle";
            //cmbPaymentMode.ValueMember = "AdvanceTypeID";
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
            //frmIndEmpAttendanceCalender frmIndEmpAttendanceCalender = new frmIndEmpAttendanceCalender(objTempCurrentlyLoggedInUserInfo, objTempClientFinYearInfo, Convert.ToInt16(lblReportingManagerID.Text.ToString()), Convert.ToDateTime("01-" + cmbPaymentMode.Text.Substring(0, 3) + "-" + cmbPaymentMode.Text.Substring(cmbPaymentMode.Text.IndexOf("-") + 2)));
            //frmIndEmpAttendanceCalender.ShowDialog();
        }

        private void frmEmpAdvanceRepayment_KeyDown(object sender, KeyEventArgs e)
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
            if (lblEmpID.Text == "")
                lblEmpID.Text = "0";
            if (lblEmpAdvanceRequestID.Text == "")
                lblEmpAdvanceRequestID.Text = "0";

            frmEmpAdvanceTRList frmEmpAdvanceTRList = new frmEmpAdvanceTRList(this, "empadvancerepayment", 0, 0);
            frmEmpAdvanceTRList.ShowDialog(this);
        }

        public void displaySelectedValuesOnUI(int txtEmpID, int txtAdvanceID)
        {
            List<EmployeeSpecificAdvanceInformation> objEmployeeSpecificAdvanceInformation = objAdvanceTransaction.EmployeeSpecificAdvanceInformation(txtEmpID, txtAdvanceID);
            lblEmpID.Text = txtEmpID.ToString();
            txtEmpCode.Text = objEmployeeSpecificAdvanceInformation[0].EmpAdvReqCode.ToString();
            txtEmpName.Text = objEmployeeSpecificAdvanceInformation[0].EmpName.ToString();
            txtDesignation.Text = objEmployeeSpecificAdvanceInformation[0].DesignationTitle.ToString();
            txtDepartment.Text = objEmployeeSpecificAdvanceInformation[0].DepartmentTitle.ToString();
            picEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(lblEmpID.Text.ToString())).EmpPhoto);
            lblPersonalInfoID.Text = objEmployeeSpecificAdvanceInformation[0].PersonalInfoID.ToString();
            lblEmpMailID.Text = objEmployeeSpecificAdvanceInformation[0].ContactNumber2.ToString();
            lblEmpAdvanceRequestID.Text = objEmployeeSpecificAdvanceInformation[0].EmpAdvanceRequestID.ToString();
            txtAdvanceRequestCode.Text = objEmployeeSpecificAdvanceInformation[0].EmpAdvReqCode.ToString();
            txtAdvanceType.Text = objEmployeeSpecificAdvanceInformation[0].AdvanceTypeTitle.ToString();
            txtAdvanceAmount.Text = Convert.ToDecimal(objEmployeeSpecificAdvanceInformation[0].AdvanceAmount.ToString()).ToString("#,##0.00");
            txtInstallmentAmount.Text = Convert.ToDecimal(objEmployeeSpecificAdvanceInformation[0].AdvanceInstallment.ToString()).ToString("#,##0.00");
            txtInstallmentAmount.Text = Convert.ToDecimal(txtInstallmentAmount.Text.ToString()).ToString("###0.00");
            txtAdvanceStartDate.Text = Convert.ToDateTime(objEmployeeSpecificAdvanceInformation[0].AdvanceStartDate.ToString()).Date.ToString("dd-MMM-yyyy");
            txtAdvanceEndDate.Text = Convert.ToDateTime(objEmployeeSpecificAdvanceInformation[0].AdvanceEndDate.ToString()).Date.ToString("dd-MMM-yyyy");
            txtClosingBalance.Text = Convert.ToDecimal(objEmployeeSpecificAdvanceInformation[0].RePaymentBalance.ToString()).ToString("#,##0.00");
            txtClosingBalance.Text = Convert.ToDecimal(txtClosingBalance.Text.ToString()).ToString("###0.00");
            txtCBalance.Text = Convert.ToDecimal(objEmployeeSpecificAdvanceInformation[0].RePaymentBalance.ToString()).ToString("#,##0.00");
            txtCBalance.Text = Convert.ToDecimal(txtCBalance.Text.ToString()).ToString("###0.00");
            lblLastTRDate.Text = Convert.ToDateTime(objEmployeeSpecificAdvanceInformation[0].LastRepayDate.ToString()).Date.ToString("dd-MMM-yyyy");

            txtAdvanceTRAmount.Text = Convert.ToDecimal(objEmployeeSpecificAdvanceInformation[0].AdvanceInstallment.ToString()).ToString("#,##0.00");
            txtAdvanceTRAmount.Text = Convert.ToDecimal(txtAdvanceTRAmount.Text.ToString()).ToString("###0.00");
        }

        private void btnRepSearch_Click(object sender, EventArgs e)
        {  

        }

        private void cmbAdvanceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lblEmpID.Text.Trim() == "")
                return;
        }

        private void txtTenure_TextChanged(object sender, EventArgs e)
        {
           
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

        }

        private void txtComments_Enter(object sender, EventArgs e)
        {
            if(txtComments.Text.Trim() == "")
               txtComments.Text = "To " + txtAdvanceType.Text.Trim() + " Repayment";
        }

        private void lblViewStatement_Click(object sender, EventArgs e)
        {
            if (lblEmpAdvanceRequestID.Text.Trim() == "")
                return;

            frmEmpAdvanceTRList frmEmpAdvanceTRList = new frmEmpAdvanceTRList(this, "empadvancestatement", Convert.ToInt32(lblEmpID.Text.ToString()), Convert.ToInt32(lblEmpAdvanceRequestID.Text.ToString()));
            frmEmpAdvanceTRList.ShowDialog(this);
        }

        private void txtAdvanceTRAmount_TextChanged(object sender, EventArgs e)
        {
            if(txtAdvanceTRAmount.Text.Trim() == "")
            {
                txtAdvanceTRAmount.Text = "0.00";
            }
            if(txtAdvanceTRAmount.Text.Trim() != "" && Convert.ToDecimal(txtAdvanceTRAmount.Text) > 0)
            {
                txtCBalance.Text = (Convert.ToDecimal(txtClosingBalance.Text.Trim()) - Convert.ToDecimal(txtAdvanceTRAmount.Text.Trim())).ToString("#,##0.00");
                if (Convert.ToDecimal(txtCBalance.Text) < 0)
                {
                    txtAdvanceTRAmount.Text = Convert.ToDecimal(txtClosingBalance.Text.ToString()).ToString("###0.00");
                    txtCBalance.Text = "0.00";
                }
            }
            else
            {
                txtCBalance.Text = Convert.ToDecimal(txtClosingBalance.Text.ToString()).ToString("#,##0.00");
            }
        }

        private void txtAdvanceTRAmount_Leave(object sender, EventArgs e)
        {
            if (txtAdvanceTRAmount.Text.ToString().Trim() == "")
                return;

            txtAdvanceTRAmount.Text = Convert.ToDecimal(txtAdvanceTRAmount.Text.ToString()).ToString("###0.00");
        }
    }
}
