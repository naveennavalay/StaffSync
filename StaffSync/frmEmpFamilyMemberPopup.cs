using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using ModelStaffSync;
using Quartz.Impl.AdoJobStore.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Quartz.Logging.OperationName;

namespace StaffSync
{
    public partial class frmEmpFamilyMemberPopup : Form
    {
        //DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster();
        //frmEmployeeMaster frmEmployeeMaster = null;
        //frmDailyAttendanceProcess frmDailyAttendanceProces = null;
        //frmLeavesMaster frmLeavesMaster = null;
        //frmUserManagement frmUserManagement = null;
        //frmRolesAndResponsibilities frmRolesAndResponsibilities = null;
        //frmModuleAssignment frmModuleAssignment = null;
        //frmRolesProfileMaster frmRolesProfileMaster = null;
        //frmAttendanceMater frmAttendanceMater = null;
        //frmLeavesApproval frmLeavesApproval = null;
        //frmLeavesReject frmLeaveReject = null;
        //frmPayrollMaster frmPayrollInfo = null;
        //frmLeaveStatement frmLeaveStatement = null;
        //frmUpdateCurrentUserInfo frmUpdateCurrentUserInfo = null;
        //frmCurrentUserLeaveMaster frmCurrentUserLeaveMaster = null;
        //frmEmpLeaveEntitlement frmEmpLeaveEntitlement = null;
        //frmLeaveStatements frmLeaveStatements = null;
        //DALStaffSync.clsLeaveTRList objLeaveInfo = new DALStaffSync.clsLeaveTRList();
        //DALStaffSync.clsUserManagement objUsersInfo = new DALStaffSync.clsUserManagement();
        //DALStaffSync.clsRolesAndResponsibilities objRolesAndResponsibilities = new DALStaffSync.clsRolesAndResponsibilities();
        //DALStaffSync.clsAppModule objAppModule = new DALStaffSync.clsAppModule();
        //DALStaffSync.clsEmpPayroll objEmpPayroll = new DALStaffSync.clsEmpPayroll();
        DALStaffSync.clsCountries objCountries = new DALStaffSync.clsCountries();
        EmpPersonalFamilyMemberInfo objOriginalValues = new EmpPersonalFamilyMemberInfo();
        public EmpPersonalFamilyMemberInfo objSaveTheseValues = new EmpPersonalFamilyMemberInfo();

        DateTime dob, doj;
        string dateFormat = "dd-MM-yyyy";
        CultureInfo provider = CultureInfo.InvariantCulture;


        public frmEmpFamilyMemberPopup()
        {
            InitializeComponent();
        }

        public frmEmpFamilyMemberPopup(EmpPersonalFamilyMemberInfo objEmpPersonalFamilyMemberInfo)
        {
            InitializeComponent();
            cmbCountry.DataSource = objCountries.GetCountryList();
            cmbCountry.DisplayMember = "CountryTitle";
            cmbCountry.ValueMember = "CountryID";

            objOriginalValues = objEmpPersonalFamilyMemberInfo;
            lblEmpPerFamInfoID.Text = objEmpPersonalFamilyMemberInfo.EmpPerFamInfoID.ToString();
            txtMemberName.Text = objEmpPersonalFamilyMemberInfo.FamMemName;
            txtDOB.Text = objEmpPersonalFamilyMemberInfo.FamMemDOB?.ToString("dd-MM-yyyy");
            txtAge.Text = objEmpPersonalFamilyMemberInfo.FamMemAge.ToString();
            txtRelationship.Text = objEmpPersonalFamilyMemberInfo.FamMemRelationship;
            txtAddress01.Text = objEmpPersonalFamilyMemberInfo.FamMemAddr1;
            txtAddress02.Text = objEmpPersonalFamilyMemberInfo.FamMemAddr2;
            txtArea.Text = objEmpPersonalFamilyMemberInfo.FamMemArea;
            txtCity.Text = objEmpPersonalFamilyMemberInfo.FamMemCity;
            txtState.Text = objEmpPersonalFamilyMemberInfo.FamMemState;
            txtPIN.Text = objEmpPersonalFamilyMemberInfo.FamMemPIN;
            cmbCountry.SelectedText = objEmpPersonalFamilyMemberInfo.FamMemCountry;
            cmbCountry.SelectedItem = objEmpPersonalFamilyMemberInfo.FamMemCountry;
            cmbCountry.Text = objEmpPersonalFamilyMemberInfo.FamMemCountry;
            txtContactNumber.Text = objEmpPersonalFamilyMemberInfo.FamMemContactNumber;
            txtMailID.Text = objEmpPersonalFamilyMemberInfo.FamMemMailID;
            txtBloodGroup.Text = objEmpPersonalFamilyMemberInfo.FamMemBloodGroup;
            chkInsuranceEnrolled.Checked = objEmpPersonalFamilyMemberInfo.FamMemInsuranceEnrolled;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEmpFamilyMemberPopup_Load(object sender, EventArgs e)
        {

        }

        private void btnCloseMe_Click_1(object sender, EventArgs e)
        {
            objSaveTheseValues = objOriginalValues;
            this.Close();
        }

        private void frmEmpFamilyMemberPopup_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateValuesOnUI())
            {
                this.Cursor = Cursors.Default;
                return;
            }

            if (string.IsNullOrEmpty(txtRelationship.Text))
                txtRelationship.Text = "N/A";
            if (string.IsNullOrEmpty(txtAddress01.Text))
                txtAddress01.Text = "N/A";
            if (string.IsNullOrEmpty(txtAddress02.Text))
                txtAddress02.Text = "N/A";
            if (string.IsNullOrEmpty(txtArea.Text))
                txtArea.Text = "N/A";
            if (string.IsNullOrEmpty(txtCity.Text))
                txtCity.Text = "N/A";
            if (string.IsNullOrEmpty(txtState.Text))
                txtState.Text = "N/A";
            if (string.IsNullOrEmpty(txtPIN.Text))
                txtPIN.Text = "N/A";
            if (string.IsNullOrEmpty(txtContactNumber.Text))
                txtContactNumber.Text = "N/A";
            if (string.IsNullOrEmpty(txtMailID.Text))
                txtMailID.Text = "N/A";
            if (string.IsNullOrEmpty(txtBloodGroup.Text))
                txtBloodGroup.Text = "N/A";

            objSaveTheseValues.EmpPerFamInfoID = Convert.ToInt16(lblEmpPerFamInfoID.Text.ToString());
            objSaveTheseValues.PersonalInfoID = Convert.ToInt16(objOriginalValues.PersonalInfoID);
            objSaveTheseValues.FamMemName = txtMemberName.Text;
            objSaveTheseValues.FamMemDOB = Convert.ToDateTime(txtDOB.Text.ToString());
            objSaveTheseValues.FamMemAge = Convert.ToInt16(txtAge.Text.ToString());
            objSaveTheseValues.FamMemRelationship = txtRelationship.Text.ToString();
            objSaveTheseValues.FamMemAddr1 = txtAddress01.Text.ToString();
            objSaveTheseValues.FamMemAddr2 = txtAddress02.Text.ToString();
            objSaveTheseValues.FamMemArea = txtArea.Text.ToString();
            objSaveTheseValues.FamMemCity = txtCity.Text.ToString();
            objSaveTheseValues.FamMemState = txtState.Text.ToString();
            objSaveTheseValues.FamMemPIN = txtPIN.Text.ToString();
            objSaveTheseValues.FamMemCountry = cmbCountry.Text.ToString();
            objSaveTheseValues.FamMemContactNumber = txtContactNumber.Text.ToString();
            objSaveTheseValues.FamMemMailID = txtMailID.Text.ToString();
            objSaveTheseValues.FamMemBloodGroup = txtBloodGroup.Text.ToString();
            objSaveTheseValues.FamMemInsuranceEnrolled = chkInsuranceEnrolled.Checked;
            this.Close();
        }

        public bool ValidateValuesOnUI()
        {
            bool validationStatus = true;
            errValidator.Clear();

            if (string.IsNullOrEmpty(txtMemberName.Text.ToString().Trim()))
            {
                validationStatus = false;
                txtMemberName.Focus();
                errValidator.SetError(this.txtMemberName, txtMemberName.Tag?.ToString() ?? "Member's Name is required.");
            }
            if (string.IsNullOrEmpty(txtDOB.Text.ToString().Trim()))
            {
                validationStatus = false;
                txtDOB.Focus();
                errValidator.SetError(this.txtDOB, txtDOB.Tag?.ToString() ?? "Member's Date Of Birth is required.");
            }
            else if (!DateTime.TryParseExact(txtDOB.Text, dateFormat, provider, DateTimeStyles.None, out dob))
            {
                validationStatus = false;
                txtDOB.Focus();
                errValidator.SetError(this.txtDOB, "Invalid Date of Birth format (dd-MM-yyyy).");
            }
            else if (dob > DateTime.Now.Date)
            {
                validationStatus = false;
                txtDOB.Focus();
                errValidator.SetError(this.txtDOB, "Date of Birth cannot be in the future.");
            }

            return validationStatus;
        }


        public EmpPersonalFamilyMemberInfo getLatestValues()
        {
            this.Close();
            return objSaveTheseValues;
        }

        private void txtDOB_TextChanged(object sender, EventArgs e)
        {
            // Date of Birth
            if (string.IsNullOrEmpty(txtDOB.Text))
            {

            }
            else if (!DateTime.TryParseExact(txtDOB.Text, dateFormat, provider, DateTimeStyles.None, out dob))
            {

            }
            else if (dob > DateTime.Today)
            {

            }
            else
            {
                DateTime birth = Convert.ToDateTime(txtDOB.Text);
                txtAge.Text = (DateTime.Today.Date.Year - birth.Year).ToString();
            }
        }
    }
}
