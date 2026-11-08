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
    public partial class frmEmployeeResignation : Form
    {
        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        frmDashboard objDashboard = (frmDashboard)System.Windows.Forms.Application.OpenForms["frmDashboard"];
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        DALStaffSync.clsResignationInfo objResignationInfo = new DALStaffSync.clsResignationInfo();
        DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster(); 
        DALStaffSync.clsEmployeePersonalInfo objEmployeePersonalInfo = new DALStaffSync.clsEmployeePersonalInfo();

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

        public frmEmployeeResignation()
        {
            InitializeComponent();
        }

        public frmEmployeeResignation(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmEmployeeResignation(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
            lblClientID.Text = objTempClientFinYearInfo.ClientID.ToString();
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
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

            lblEmpResignID.Text = "";
            dtResignationDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            cmbResignationReason.DataSource = null;
            lblEmpResignID.Text = "";
            lblTaskID.Text = "";

            txtResignationMoreDetails.Text = "";

            lblRequestFromMailID.Text = "";
            lblRequestToMailID.Text = "";
            lblResignationReasonID.Text = "";

            lnkViewAuditLog.Visible = false;
        }

        public void enableControls()
        {
            btnEmpSearch.Enabled = true;
            txtResignationCode.Enabled = false;
            txtEmpCode.Enabled = false;
            txtEmpName.Enabled = false;
            txtDesignation.Enabled = false;
            txtDepartment.Enabled = false;

            txtRepCode.Enabled = false;
            txtRepName.Enabled = false;
            txtRepDesignation.Enabled = false;
            txtRepDepartment.Enabled = false;
            btnRepSearch.Enabled = true;

            cmbResignationReason.Enabled = true;
            dtResignationDate.Enabled = true;
            txtResignationMoreDetails.Enabled = true;
        }

        public void disableControls()
        {
            btnEmpSearch.Enabled = false;
            txtResignationCode.Enabled = false;
            txtEmpCode.Enabled = false;
            txtEmpName.Enabled = false;
            txtDesignation.Enabled = false;
            txtDepartment.Enabled = false;

            txtRepCode.Enabled = false;
            txtRepName.Enabled = false;
            txtRepDesignation.Enabled = false;
            txtRepDepartment.Enabled = false;
            btnRepSearch.Enabled = false;

            cmbResignationReason.Enabled = false;
            dtResignationDate.Enabled = false;
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

        private void frmEmployeeResignation_Load(object sender, EventArgs e)
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

            lblActionMode.Text = "add";
            onGenerateButtonClick();
            clearControls();
            enableControls();
            errValidator.Clear();

            lblEmpResignID.Text = objGenFunc.getMaxRowCount("EmpResignationInfo", "EmpResignID").Data.ToString();
            txtResignationCode.Text = "RSGN-" + (lblEmpResignID.Text.Trim()).ToString().PadLeft(4, '0');

            dtResignationDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            cmbResignationReason.DataSource = objResignationInfo.getResignationInfotatements();
            cmbResignationReason.DisplayMember = "ResigMasTitle";
            cmbResignationReason.ValueMember = "ResigMasID";
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            if(objResignationInfo.getIsResignationAppliedInfotatements(Convert.ToInt32(lblEmpID.Text)).EmpResignStatus > 0)
            {
                MessageBox.Show("Resignation has already been applied.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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

            var updatedValues = AuditLogger.getOriginalValues(this);
            var onlyChangedValues = (dynamic)null;

            strActionStatement = "";

            lblResignationReasonID.Text = (cmbResignationReason.SelectedIndex + 1).ToString();

            if (lblActionMode.Text == "add")
            {
                objResignationInfo.InsertEmployeeResignationInfo(Convert.ToInt32(lblPersonalInfoID.Text.ToString()), Convert.ToInt32(lblResignationReasonID.Text), txtResignationMoreDetails.Text.Trim(), Convert.ToDateTime(dtResignationDate.Text.ToString()),
                Convert.ToDateTime(dtResignationDate.Text.ToString()), Convert.ToInt32(lblReportingManagerID.Text.ToString()), false,
                Convert.ToDateTime(dtResignationDate.Text.ToString()), Convert.ToInt32(lblReportingManagerID.Text.ToString()), false,
                1, Convert.ToDateTime(dtResignationDate.Text.ToString()), 0, Convert.ToDateTime(dtResignationDate.Text.ToString()), "Need to serve");
            }

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
            if (lblEmpID.Text.Trim() == "")
            {
                errValidator.SetError(txtEmpCode, "Please select the employee");
                validationStatus = false;
            }
            else
            {
                errValidator.SetError(txtEmpCode, "");
            }

            if (cmbResignationReason.Text.Trim() == "")
            {
                errValidator.SetError(cmbResignationReason, "Please select the Resignation Reason");
                validationStatus = false;
            }
            else
            {
                errValidator.SetError(cmbResignationReason, "");
            }

            // Date of Birth
            if (string.IsNullOrEmpty(dtResignationDate.Text))
            {
                validationStatus = false;
                errValidator.SetError(this.dtResignationDate, dtResignationDate.Tag?.ToString() ?? "Date of Resignation is required.");
            }
            else if (!DateTime.TryParseExact(dtResignationDate.Text, dateFormat, provider, DateTimeStyles.None, out dos))
            {
                validationStatus = false;
                errValidator.SetError(this.dtResignationDate, "Invalid Date of Resignation format (dd-MM-yyyy).");
            }
            else if (dos < DateTime.Now.Date)
            {
                validationStatus = false;
                errValidator.SetError(this.dtResignationDate, "Date of Resignation cannot be in the past.");
            }
            else if (dos > DateTime.Now.Date)
            {
                validationStatus = false;
                errValidator.SetError(this.dtResignationDate, "Date of Resignation cannot be in the future.");
            }
            else
            {
                errValidator.SetError(this.dtResignationDate, "");
            }

            if (txtResignationMoreDetails.Text.Trim() == "")
            {
                txtResignationMoreDetails.Text = "N/A";
            }

            return validationStatus;
        }

        private void frmEmployeeResignation_KeyDown(object sender, KeyEventArgs e)
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
            frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "EmployeeListToResign");
            frmEmployeeList.ShowDialog();
        }

        public void SelectedEmployeeID(string SearchOptionSelectedForm, int selectedEmployeeID)
        {
            if (SearchOptionSelectedForm == "EmployeeListToResign")
            {
                if (objResignationInfo.getIsResignationAppliedInfotatements(Convert.ToInt32(selectedEmployeeID)).EmpResignStatus > 0)
                {
                    MessageBox.Show("Resignation has already been applied.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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
            }
        }

        public void UpdateUIWithSelectedEmployeeDetails(int EmployeeID)
        {
            this.Cursor = Cursors.WaitCursor;

            lblEmpID.Text = EmployeeID.ToString();
            EmployeeInfo objSelectedEmployeeInfo = objEmployeeMaster.GetSelectedEmployeeInfo(EmployeeID);
            txtEmpCode.Text = objSelectedEmployeeInfo.EmpCode;
            txtEmpName.Text = objSelectedEmployeeInfo.EmpName;
            txtDepartment.Text = objSelectedEmployeeInfo.DepartmentTitle;
            txtDesignation.Text = objSelectedEmployeeInfo.DesignationTitle;

            EmpPersonalPersonalInfo objSelectedPersonalInfo = objEmployeePersonalInfo.GetEmpPersonalPersonalInfo(Convert.ToInt16(lblEmpID.Text));
            lblPersonalInfoID.Text = objSelectedPersonalInfo.PersonalInfoID.ToString();
            picEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(EmployeeID).EmpPhoto);

            lblReportingManagerID.Text = objSelectedEmployeeInfo.EmpRepManID.ToString();
            ReportingManagerInfo objReportingManagerInfo = objEmployeeMaster.GetReportingManagerInfo(Convert.ToInt16(lblReportingManagerID.Text));
            txtRepCode.Text = objReportingManagerInfo.EmpCode;
            txtRepName.Text = objReportingManagerInfo.EmpName;
            txtRepDesignation.Text = objReportingManagerInfo.DesignationTitle;
            txtRepDepartment.Text = objReportingManagerInfo.DepartmentTitle;
            picRepEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(lblReportingManagerID.Text)).EmpPhoto);

            this.Cursor = Cursors.Default;
        }
    }
}
