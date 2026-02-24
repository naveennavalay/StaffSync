using Krypton.Toolkit;
using ModelStaffSync;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Crypto.Encodings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static StaffSync.TextBoxHelper;
using static System.Windows.Forms.MonthCalendar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

//using static C1.Util.Win.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace StaffSync
{
    public partial class frmSSEmployeeMaster : Form
    {

        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        DALStaffSync.clsEncryptDecrypt objEncryptDecrypt = new DALStaffSync.clsEncryptDecrypt();
        DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster();
        DALStaffSync.clsAddressInfo objAddressInfo = new DALStaffSync.clsAddressInfo();
        DALStaffSync.clsEmpContactPersonMas objContactPerson = new DALStaffSync.clsEmpContactPersonMas();
        DALStaffSync.clsEmployeePersonalInfo objEmployeePersonalInfo = new DALStaffSync.clsEmployeePersonalInfo();
        DALStaffSync.clsEmployeePersonalIDInfo objEmployeePersonalIDInfo = new DALStaffSync.clsEmployeePersonalIDInfo();
        DALStaffSync.clsEmpPersonalFamilyMemberInfo objEmpPersonalFamilyMemberInfo = new DALStaffSync.clsEmpPersonalFamilyMemberInfo();
        DALStaffSync.clsEmpNomineeMas objNomineeInfo = new DALStaffSync.clsEmpNomineeMas();
        DALStaffSync.clsEmpEduQualMas objEmpEduQualInfo = new DALStaffSync.clsEmpEduQualMas();
        DALStaffSync.clsEmpSkillsMas objEmpSkillMas = new DALStaffSync.clsEmpSkillsMas();
        DALStaffSync.clsBloodGroup objBloodGroup = new DALStaffSync.clsBloodGroup();
        DALStaffSync.clsCountries objCountries = new DALStaffSync.clsCountries();
        DALStaffSync.clsStates objStates = new DALStaffSync.clsStates();
        DALStaffSync.clsSkillsMas objSkills = new DALStaffSync.clsSkillsMas();
        DALStaffSync.clsEduQalification objEduQualMas = new DALStaffSync.clsEduQalification();
        DALStaffSync.clsDepartment objDepartment = new DALStaffSync.clsDepartment();
        DALStaffSync.clsDesignation objDesignation = new DALStaffSync.clsDesignation();
        DALStaffSync.clsRelationship objRelationship = new DALStaffSync.clsRelationship();
        DALStaffSync.clsSexMas objSexMaster = new DALStaffSync.clsSexMas();
        DALStaffSync.clsPhotoMas objPhotoMas = new DALStaffSync.clsPhotoMas();
        DALStaffSync.clsUploadDocuments objUploadDocument = new DALStaffSync.clsUploadDocuments();
        DALStaffSync.clsLeaveTRList objLeaveTRList = new DALStaffSync.clsLeaveTRList();
        DALStaffSync.clsEmpLeaveEntitlementInfo objEmpLeaveEntitlementInfo = new DALStaffSync.clsEmpLeaveEntitlementInfo();
        //Download objDownload = new Download();
        clsImpageOperation objImpageOperation = new clsImpageOperation();
        DALStaffSync.clsUploadDocuments objUploadedDocuments = new DALStaffSync.clsUploadDocuments();
        DALStaffSync.clsBankMas objBankInfo = new DALStaffSync.clsBankMas();
        DALStaffSync.clsSalaryProfile objSalaryProfile = new DALStaffSync.clsSalaryProfile();
        DALStaffSync.clsEmployeeSalaryProfileInfo objEmployeeSalaryProfileInfo = new DALStaffSync.clsEmployeeSalaryProfileInfo();
        DALStaffSync.clsEmpWorkExperienceInfo objEmpWorkExperienceInfo = new DALStaffSync.clsEmpWorkExperienceInfo();
        DALStaffSync.clsEmpPayroll objEmployeePayroll = new DALStaffSync.clsEmpPayroll();
        DALStaffSync.clsWeeklyOffInfo objWeeklyOffInfo = new DALStaffSync.clsWeeklyOffInfo();
        DALStaffSync.clsTaxMas objTaxMas = new DALStaffSync.clsTaxMas();
        DALStaffSync.clsShiftMas objShiftMas = new DALStaffSync.clsShiftMas();
        DALStaffSync.clsEmploymentTypeInfo objEmploymentTypeInfo = new DALStaffSync.clsEmploymentTypeInfo();
        frmDashboard objDashboard = (frmDashboard)System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();

        public frmSSEmployeeMaster()
        {
            InitializeComponent();
        }

        public frmSSEmployeeMaster(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            RefreshFamilyMembersInformation();
        }

        public frmSSEmployeeMaster(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
            RefreshFamilyMembersInformation();
        }

        public frmSSEmployeeMaster(int EmployeeID)
        {
            this.Cursor = Cursors.WaitCursor;
            InitializeComponent();

            lblEmpID.Text = EmployeeID.ToString();
            lblActionMode.Text = "modify";
            tabControl1.SelectedIndex = 0;
            clearControls();
            enableControls();
            lblEmpID.Text = "";
            txtEmpCode.Text = "";
            errValidator.Clear();

            cmbDepartment.DataSource = objDepartment.GetDepartmentList();
            cmbDepartment.DisplayMember = "DepartmentTitle";
            cmbDepartment.ValueMember = "DepartmentID";

            cmbGender.DataSource = objSexMaster.GetSexList();
            cmbGender.DisplayMember = "SexTitle";
            cmbGender.ValueMember = "SexID";

            cmbDesignation.DataSource = objDesignation.GetDesignationList();
            cmbDesignation.DisplayMember = "DesignationTitle";
            cmbDesignation.ValueMember = "DesignationID";

            cmbShift.DataSource = objShiftMas.GetShiftList();
            cmbShift.DisplayMember = "ShiftTitle";
            cmbShift.ValueMember = "ShiftID";

            cmbEmploymentType.DataSource = objEmploymentTypeInfo.GetEmploymentTypeList();
            cmbEmploymentType.DisplayMember = "EmpTypeTitle";
            cmbEmploymentType.ValueMember = "EmpTypeMasID";

            cmbContactPersonRelationship.DataSource = objRelationship.GetRelationshipList();
            cmbContactPersonRelationship.DisplayMember = "RelationShipTitle";
            cmbContactPersonRelationship.ValueMember = "RelationShipID";

            cmbNomineeRelationship.DataSource = objRelationship.GetRelationshipList();
            cmbNomineeRelationship.DisplayMember = "RelationShipTitle";
            cmbNomineeRelationship.ValueMember = "RelationShipID";

            cmbBloodGroup.DataSource = objBloodGroup.GetBloodGroupList();
            cmbBloodGroup.DisplayMember = "BloodGroupTitle";
            cmbBloodGroup.ValueMember = "BloodGroupID";

            cmbCurrentCountry.DataSource = objCountries.GetCountryList();
            cmbCurrentCountry.DisplayMember = "CountryTitle";
            cmbCurrentCountry.ValueMember = "CountryID";

            cmbPermanentCountry.DataSource = objCountries.GetCountryList();
            cmbPermanentCountry.DisplayMember = "CountryTitle";
            cmbPermanentCountry.ValueMember = "CountryID";

            chkEduQualList.ColumnWidth = 300;
            ((ListBox)chkEduQualList).DataSource = objEduQualMas.GetEduQualMasList();
            ((ListBox)chkEduQualList).DisplayMember = "EduQualTitle";
            ((ListBox)chkEduQualList).ValueMember = "EduQualID";

            chkSkillsList.ColumnWidth = 300;
            ((ListBox)chkSkillsList).DataSource = objSkills.GetSkillList();
            ((ListBox)chkSkillsList).DisplayMember = "SkillTitle";
            ((ListBox)chkSkillsList).ValueMember = "SkillID";

            RefreshFamilyMembersInformation();

            cmbSalProfile.DataSource = objSalaryProfile.GetSalProfileTitleList();
            cmbSalProfile.DisplayMember = "SalProfileTitle";
            cmbSalProfile.ValueMember = "SalProfileID";

            cmbEmpTaxScheme.DataSource = objTaxMas.GetTaxList();
            cmbEmpTaxScheme.DisplayMember = "TaxTitle";
            cmbEmpTaxScheme.ValueMember = "TaxSchemeID";

            cmbWeeklyOff.DataSource = objWeeklyOffInfo.getWklyOffProfileMasInfoList("");
            cmbWeeklyOff.DisplayMember = "WklyOffTitle";
            cmbWeeklyOff.ValueMember = "WklyOffMasID";

            UpdateUIWithSelectedEmployeeDetails(Convert.ToInt16(EmployeeID.ToString()));
            enableControls();

            this.Cursor = Cursors.Default;
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
            tabControl1.SelectedIndex = 0;
            onCancelButtonClick();
            disableControls();
            clearControls();
            errValidator.Clear();
        }

        public void resetButtonsToNew()
        {
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = false;
        }

        public void resetButtonsToModify()
        {
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = false;
        }

        public void resetButtonsToRemove()
        {
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = false;
        }

        public void resetButtonsToCancel()
        {
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
        }

        public void clearFormControls()
        {
            foreach (Control indControl in this.Controls)
            {
                if (indControl is System.Windows.Forms.TextBox)
                {
                    indControl.Text = "";
                }
            }
        }

        public void disableFormControls()
        {
            foreach (Control indControl in this.Controls)
            {
                if (indControl is System.Windows.Forms.TextBox)
                {
                    indControl.Text = "";
                }
            }
        }

        private void frmSSEmployeeMaster_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'staffsyncDBDTSet.DocUploads' table. You can move, or remove it, as needed.
            //this.docUploadsTableAdapter.Fill(this.staffsyncDBDTSet.DocUploads);
            //// TODO: This line of code loads data into the 'staffsyncDBDTSet.EmpMasInfo' table. You can move, or remove it, as needed.
            //this.empMasInfoTableAdapter.Fill(this.staffsyncDBDTSet.EmpMasInfo);
            disableControls();
            clearFormControls();
            onCancelButtonClick();
            resetButtonsToCancel();
            clearControls();
            TextBoxHelper.EnableSelectAllOnFocus(this);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listSSEmployees");
            frmEmployeeList.ShowDialog();
        }

        private void btnGenerateDetails_Click(object sender, EventArgs e)
        {
            if(CurrentUser.ClientID == 0)
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
            txtTotalLeaveAllotment.Enabled = true;
            txtBalanceLeaveAllotment.Enabled = true;
            txtTotalUtilised.Enabled = true;
            lblEmpID.Text = objGenFunc.getMaxRowCount("EMPMas", "EmpID", objTempClientFinYearInfo.ClientID).Data.ToString();
            txtEmpCode.Text = "EMP-" + (lblEmpID.Text.Trim()).ToString().PadLeft(4, '0');
            errValidator.Clear();

            cmbDepartment.DataSource = objDepartment.GetDepartmentList();
            cmbDepartment.DisplayMember = "DepartmentTitle";
            cmbDepartment.ValueMember = "DepartmentID";

            cmbGender.DataSource = objSexMaster.GetSexList();
            cmbGender.DisplayMember = "SexTitle";
            cmbGender.ValueMember = "SexID";

            cmbDesignation.DataSource = objDesignation.GetDesignationList();
            cmbDesignation.DisplayMember = "DesignationTitle";
            cmbDesignation.ValueMember = "DesignationID";

            cmbEmploymentType.DataSource = objEmploymentTypeInfo.GetEmploymentTypeList();
            cmbEmploymentType.DisplayMember = "EmpTypeTitle";
            cmbEmploymentType.ValueMember = "EmpTypeMasID";
            txtEmploymentEffectiveFrom.Text = DateTime.Now.ToString("dd-MM-yyyy");

            cmbShift.DataSource = objShiftMas.GetShiftList();
            cmbShift.DisplayMember = "ShiftTitle";
            cmbShift.ValueMember = "ShiftID";
            txtShiftEffectiveFrom.Text = DateTime.Now.ToString("dd-MM-yyyy");

            //txtCurrentAddress01.EnableAutoCompleteFromDataTable(objAddressInfo.GetAddressList("Address1"), "Address1");
            //txtCurrentAddress02.EnableAutoCompleteFromDataTable(objAddressInfo.GetAddressList("Address2"), "Address2");
            //txtCurrentArea.EnableAutoCompleteFromDataTable(objAddressInfo.GetAddressList("Area"), "Area");
            //txtCurrentCity.EnableAutoCompleteFromDataTable(objAddressInfo.GetAddressList("City"), "City");
            //txtCurrentPIN.EnableAutoCompleteFromDataTable(objAddressInfo.GetAddressList("PIN"), "PIN");
            txtCurrentState.EnableAutoCompleteFromDataTable(objStates.GetStateList(), "StateTitle");

            //txtPermanentAddress01.EnableAutoCompleteFromDataTable(objAddressInfo.GetAddressList("Address1"), "Address1");
            //txtPermanentAddress02.EnableAutoCompleteFromDataTable(objAddressInfo.GetAddressList("Address2"), "Address2");
            //txtPermanentArea.EnableAutoCompleteFromDataTable(objAddressInfo.GetAddressList("Area"), "Area");
            //txtPermanentCity.EnableAutoCompleteFromDataTable(objAddressInfo.GetAddressList("City"), "City");
            //txtPermanentPIN.EnableAutoCompleteFromDataTable(objAddressInfo.GetAddressList("PIN"), "PIN");
            //txtPermanentState.EnableAutoCompleteFromDataTable(objStates.GetStateList(), "StateTitle");

            lblContactInfoID.Text = "";
            cmbContactPersonRelationship.DataSource = objRelationship.GetRelationshipList();
            cmbContactPersonRelationship.DisplayMember = "RelationShipTitle";
            cmbContactPersonRelationship.ValueMember = "RelationShipID";

            cmbNomineeRelationship.DataSource = objRelationship.GetRelationshipList();
            cmbNomineeRelationship.DisplayMember = "RelationShipTitle";
            cmbNomineeRelationship.ValueMember = "RelationShipID";

            cmbBloodGroup.DataSource = objBloodGroup.GetBloodGroupList();
            cmbBloodGroup.DisplayMember = "BloodGroupTitle";
            cmbBloodGroup.ValueMember = "BloodGroupID";

            cmbCurrentCountry.DataSource = objCountries.GetCountryList();
            cmbCurrentCountry.DisplayMember = "CountryTitle";
            cmbCurrentCountry.ValueMember = "CountryID";

            cmbPermanentCountry.DataSource = objCountries.GetCountryList();
            cmbPermanentCountry.DisplayMember = "CountryTitle";
            cmbPermanentCountry.ValueMember = "CountryID";

            chkEduQualList.ColumnWidth = 300;
            ((ListBox)chkEduQualList).DataSource = objEduQualMas.GetEduQualMasList();
            ((ListBox)chkEduQualList).DisplayMember = "EduQualTitle";
            ((ListBox)chkEduQualList).ValueMember = "EduQualID";

            chkSkillsList.ColumnWidth = 300;
            ((ListBox)chkSkillsList).DataSource = objSkills.GetSkillList();
            ((ListBox)chkSkillsList).DisplayMember = "SkillTitle";
            ((ListBox)chkSkillsList).ValueMember = "SkillID";

            cmbSalProfile.DataSource = objSalaryProfile.GetSalProfileTitleList();
            cmbSalProfile.DisplayMember = "SalProfileTitle";
            cmbSalProfile.ValueMember = "SalProfileID";

            cmbEmpTaxScheme.DataSource = objTaxMas.GetTaxList();
            cmbEmpTaxScheme.DisplayMember = "TaxTitle";
            cmbEmpTaxScheme.ValueMember = "TaxSchemeID";
            txtTaxSchemeEffectiveFrom.Text = DateTime.Now.ToString("dd-MM-yyyy");

            cmbWeeklyOff.DataSource = objWeeklyOffInfo.getWklyOffProfileMasInfoList("");
            cmbWeeklyOff.DisplayMember = "WklyOffTitle";
            cmbWeeklyOff.ValueMember = "WklyOffMasID";

            RefreshBankList();
            RefreshLeavesHistoryList();
        }

        public void onGenerateButtonClick()
        {
            lblActionMode.Text = "add";
            tabControl1.SelectedIndex = 0;
            lblEmpID.Text = "";
            btnSearch.Enabled = false;
            btnEmpPhotoUpload.Enabled = true;
            btnUploadDocument.Enabled = true;
            btnReportingManagerSearch.Enabled = true;
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
            btnSearch.Enabled = true;
            btnEmpPhotoUpload.Enabled = true;
            btnUploadDocument.Enabled = true;
            btnReportingManagerSearch.Enabled = true;
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
            btnSearch.Enabled = true;
            btnEmpPhotoUpload.Enabled = false;
            btnUploadDocument.Enabled = false;
            btnReportingManagerSearch.Enabled = false;
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = false;
            btnReportingManagerSearch.Enabled = false;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onSaveButtonClick()
        {
            lblActionMode.Text = "";
            lblEmpID.Text = "";
            btnSearch.Enabled = false;
            btnEmpPhotoUpload.Enabled = false;
            btnUploadDocument.Enabled = false;
            btnReportingManagerSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnReportingManagerSearch.Enabled = false;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onCancelButtonClick()
        {
            lblActionMode.Text = "";
            lblEmpID.Text = "";
            lblReportingManagerID.Text = "";
            lblCurrentAddressID.Text = "";
            lblPermanentAddressID.Text = "";
            btnSearch.Enabled = false;
            btnEmpPhotoUpload.Enabled = false;
            btnUploadDocument.Enabled = false;
            btnReportingManagerSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnReportingManagerSearch.Enabled = false;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void clearControls()
        {
            lblEmpID.Text = "";
            txtEmpCode.Text = "";
            txtEmployeeName.Text = "";
            txtDateOfBirth.Text = "";
            txtDateOfJoining.Text = "";
            cmbContactPersonRelationship.DataSource = null;
            cmbBloodGroup.DataSource = null;
            picEmpPhoto.Image = null;

            lblCurrentAddressID.Text = "";
            txtCurrentAddress01.Text = "";
            txtCurrentAddress02.Text = "";
            txtCurrentArea.Text = "";
            txtCurrentCity.Text = "";
            txtCurrentState.Text = "";
            txtCurrentPIN.Text = "";
            cmbCurrentCountry.DataSource = null;
            txtEmployeeContactNumber.Text = "";
            txtEmployeeMailID.Text = "";

            lblPermanentAddressID.Text = "";
            txtPermanentAddress01.Text = "";
            txtPermanentAddress02.Text = "";
            txtPermanentArea.Text = "";
            txtPermanentCity.Text = "";
            txtPermanentState.Text = "";
            txtPermanentPIN.Text = "";
            cmbPermanentCountry.DataSource = null;

            cmbSalProfile.DataSource = null;
            cmbEmpTaxScheme.DataSource = null;

            lblReportingManagerID.Text = "";
            txtRepEmpCode.Text = "";
            txtRepEmpName.Text = "";
            txtRepEmpDesig.Text = "";
            txtRepEmpDepartment.Text = "";
            txtRepEmpContactNumber.Text = "";
            picRepEmpPhoto.Image = null;
            txtEmpPhoto.Text = "";

            cmbDesignation.DataSource = null;
            cmbDepartment.DataSource = null;

            cmbEmploymentType.DataSource = null;
            txtEmploymentEffectiveFrom.Text = "";
            cmbShift.DataSource = null;
            txtShiftEffectiveFrom.Text = "";

            lblContactInfoID.Text = "";
            txtContactPersonName.Text = "";
            cmbContactPersonRelationship.DataSource = null;
            txtContactPersonNumber.Text = "";

            lblNomineeID.Text = "";
            txtNomineeName.Text = "";
            cmbNomineeRelationship.DataSource = null;
            txtNomineeContactNumber.Text = "";

            lblEmpGovtID.Text = "";
            txtPassportNumber.Text = "";
            txtPassportIssueDate.Text = "";
            txtPassportRenewalDate.Text = "";
            txtAadhaarCardNumber.Text = "";
            txtVoterCardNumber.Text = "";
            txtPANCardNumber.Text = "";
            txtAdditonalCardNumber.Text = "";

            cmbGender.DataSource = null;

            chkEduQualList.DataSource = null;
            chkSkillsList.DataSource = null;

            chkEduQualList.ColumnWidth = 300;
            ((ListBox)chkEduQualList).DataSource = objEduQualMas.GetEduQualMasList();
            ((ListBox)chkEduQualList).DisplayMember = "EduQualTitle";
            ((ListBox)chkEduQualList).ValueMember = "EduQualID";

            chkSkillsList.ColumnWidth = 300;
            ((ListBox)chkSkillsList).DataSource = objSkills.GetSkillList();
            ((ListBox)chkSkillsList).DisplayMember = "SkillTitle";
            ((ListBox)chkSkillsList).ValueMember = "SkillID";

            lblBankID.Text = "0";
            txtBankAccountNumber.Text = "";

            lblLeaveMasID.Text = "";
            lblEmployeeWeeklyOffID.Text = "";

            lstLDocumentsList.Items.Clear();

            dtgSalaryProfileDetails.DataSource = null;

            dtgPreviousWorkExp.DataSource = objEmpWorkExperienceInfo.GetWorkExpDefaultList();
            dtgPreviousWorkExp.Columns["LastCompID"].Visible = false;
            dtgPreviousWorkExp.Columns["LastCompanyInfoID"].Visible = false;
            dtgPreviousWorkExp.Columns["EmpID"].Visible = false;
            dtgPreviousWorkExp.Columns["LastCompanyTitle"].Width = 200;
            dtgPreviousWorkExp.Columns["Address"].Width = 350;
            dtgPreviousWorkExp.Columns["StartDate"].Width = 100;
            dtgPreviousWorkExp.Columns["StartDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgPreviousWorkExp.Columns["EndDate"].Width = 100;
            dtgPreviousWorkExp.Columns["EndDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgPreviousWorkExp.Columns["Comments"].Width = 350;
            btnViewCalender.Visible = false;

            RefreshFamilyMembersInformation();
        }

        public void enableControls()
        {
            txtEmpCode.Enabled = true;
            txtEmployeeName.Enabled = true;
            txtDateOfBirth.Enabled = true;
            txtDateOfJoining.Enabled = true;
            cmbContactPersonRelationship.Enabled = true;
            cmbBloodGroup.Enabled = true;

            txtCurrentAddress01.Enabled = true;
            txtCurrentAddress02.Enabled = true;
            txtCurrentArea.Enabled = true;
            txtCurrentCity.Enabled = true;
            txtCurrentState.Enabled = true;
            txtCurrentPIN.Enabled = true;
            cmbCurrentCountry.Enabled = true;
            txtEmployeeContactNumber.Enabled = true;
            txtEmployeeMailID.Enabled = true;

            chkSamePerAddAsCurAdd.Enabled = true;
            txtPermanentAddress01.Enabled = true;
            txtPermanentAddress02.Enabled = true;
            txtPermanentArea.Enabled = true;
            txtPermanentCity.Enabled = true;
            txtPermanentState.Enabled = true;
            txtPermanentPIN.Enabled = true;
            cmbPermanentCountry.Enabled = true;
            cmbSalProfile.Enabled = true;
            cmbEmpTaxScheme.Enabled = true;
            txtTaxSchemeEffectiveFrom.Enabled = true;

            cmbDesignation.Enabled = true;
            cmbDepartment.Enabled = true;
            cmbEmploymentType.Enabled = true;
            txtEmploymentEffectiveFrom.Enabled = true;
            cmbShift.Enabled = true;
            txtShiftEffectiveFrom.Enabled = true;

            txtPassportNumber.Enabled = true;
            txtPassportIssueDate.Enabled = true;
            txtPassportRenewalDate.Enabled = true;
            txtAadhaarCardNumber.Enabled = true;
            txtVoterCardNumber.Enabled = true;
            txtPANCardNumber.Enabled = true;
            txtAdditonalCardNumber.Enabled = true;

            txtRepEmpCode.Enabled = false;
            txtRepEmpName.Enabled = false;
            txtRepEmpDesig.Enabled = false;
            txtRepEmpDepartment.Enabled = false;
            txtRepEmpContactNumber.Enabled = false;

            txtContactPersonName.Enabled = true;
            cmbContactPersonRelationship.Enabled = true;
            txtContactPersonNumber.Enabled = true;

            txtNomineeName.Enabled = true;
            cmbNomineeRelationship.Enabled = true;
            txtNomineeContactNumber.Enabled = true;

            chkEduQualList.Enabled = true;
            chkSkillsList.Enabled = true;

            txtTotalLeaveAllotment.Enabled = false;
            txtBalanceLeaveAllotment.Enabled = false;
            //txtTotalUtilised.Enabled = false;
            cmbWeeklyOff.Enabled = true;

            txtBankAccountNumber.Enabled = true;
        }

        public void disableControls()
        {
            txtEmpCode.Enabled = false;
            txtEmployeeName.Enabled = false;
            txtDateOfBirth.Enabled = false;
            txtDateOfJoining.Enabled = false;
            cmbContactPersonRelationship.Enabled = false;
            cmbBloodGroup.Enabled = false;

            txtCurrentAddress01.Enabled = false;
            txtCurrentAddress02.Enabled = false;
            txtCurrentArea.Enabled = false;
            txtCurrentCity.Enabled = false;
            txtCurrentState.Enabled = false;
            txtCurrentPIN.Enabled = false;
            cmbCurrentCountry.Enabled = false;
            txtEmployeeContactNumber.Enabled = false;
            txtEmployeeMailID.Enabled = false;

            chkSamePerAddAsCurAdd.Enabled = false;
            txtPermanentAddress01.Enabled = false;
            txtPermanentAddress02.Enabled = false;
            txtPermanentArea.Enabled = false;
            txtPermanentCity.Enabled = false;
            txtPermanentState.Enabled = false;
            txtPermanentPIN.Enabled = false;
            cmbPermanentCountry.Enabled = false;
            cmbSalProfile.Enabled = false;
            cmbEmpTaxScheme.Enabled = false;
            txtTaxSchemeEffectiveFrom.Enabled = false;

            cmbDesignation.Enabled = false;
            cmbDepartment.Enabled = false;
            cmbEmploymentType.Enabled = false;
            txtEmploymentEffectiveFrom.Enabled = false;
            cmbShift.Enabled = false;
            txtShiftEffectiveFrom.Enabled = false;

            txtPassportNumber.Enabled = false;
            txtPassportIssueDate.Enabled = false;
            txtPassportRenewalDate.Enabled = false;
            txtAadhaarCardNumber.Enabled = false;
            txtVoterCardNumber.Enabled = false;
            txtPANCardNumber.Enabled = false;
            txtAdditonalCardNumber.Enabled = false;

            txtRepEmpCode.Enabled = false;
            txtRepEmpName.Enabled = false;
            txtRepEmpDesig.Enabled = false;
            txtRepEmpDepartment.Enabled = false;
            txtRepEmpContactNumber.Enabled = false;

            txtContactPersonName.Enabled = false;
            cmbContactPersonRelationship.Enabled = false;
            txtContactPersonNumber.Enabled = false;

            txtNomineeName.Enabled = false;
            cmbNomineeRelationship.Enabled = false;
            txtNomineeContactNumber.Enabled = false;

            txtPassportNumber.Enabled = false;
            txtAadhaarCardNumber.Enabled = false;
            txtVoterCardNumber.Enabled = false;
            txtPANCardNumber.Enabled = false;
            txtAdditonalCardNumber.Enabled = false;

            chkEduQualList.Enabled = false;
            chkSkillsList.Enabled = false;

            txtTotalLeaveAllotment.Enabled = false;
            txtBalanceLeaveAllotment.Enabled = false;
            //txtTotalUtilised.Enabled = false;
            cmbWeeklyOff.Enabled = false;

            txtBankAccountNumber.Enabled = false;
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

            lblActionMode.Text = "modify";
            tabControl1.SelectedIndex = 0;
            onModifyButtonClick();
            clearControls();
            enableControls();
            lblEmpID.Text = "";
            txtEmpCode.Text = "";
            errValidator.Clear();

            cmbDepartment.DataSource = objDepartment.GetDepartmentList();
            cmbDepartment.DisplayMember = "DepartmentTitle";
            cmbDepartment.ValueMember = "DepartmentID";

            cmbGender.DataSource = objSexMaster.GetSexList();
            cmbGender.DisplayMember = "SexTitle";
            cmbGender.ValueMember = "SexID";

            cmbDesignation.DataSource = objDesignation.GetDesignationList();
            cmbDesignation.DisplayMember = "DesignationTitle";
            cmbDesignation.ValueMember = "DesignationID";

            cmbEmploymentType.DataSource = objEmploymentTypeInfo.GetEmploymentTypeList();
            cmbEmploymentType.DisplayMember = "EmpTypeTitle";
            cmbEmploymentType.ValueMember = "EmpTypeMasID";
            txtEmploymentEffectiveFrom.Text = DateTime.Now.ToString("dd-MMM-yyyy");

            cmbShift.DataSource = objShiftMas.GetShiftList();
            cmbShift.DisplayMember = "ShiftTitle";
            cmbShift.ValueMember = "ShiftID";
            txtShiftEffectiveFrom.Text = DateTime.Now.ToString("dd-MMM-yyyy");

            cmbContactPersonRelationship.DataSource = objRelationship.GetRelationshipList();
            cmbContactPersonRelationship.DisplayMember = "RelationShipTitle";
            cmbContactPersonRelationship.ValueMember = "RelationShipID";

            cmbNomineeRelationship.DataSource = objRelationship.GetRelationshipList();
            cmbNomineeRelationship.DisplayMember = "RelationShipTitle";
            cmbNomineeRelationship.ValueMember = "RelationShipID";

            cmbBloodGroup.DataSource = objBloodGroup.GetBloodGroupList();
            cmbBloodGroup.DisplayMember = "BloodGroupTitle";
            cmbBloodGroup.ValueMember = "BloodGroupID";

            //txtCurrentAddress01.EnableAutoCompleteFromDataTable(objAddressInfo.GetAddressList("Address1"), "Address1");
            //txtCurrentAddress02.EnableAutoCompleteFromDataTable(objAddressInfo.GetAddressList("Address2"), "Address2");
            //txtCurrentArea.EnableAutoCompleteFromDataTable(objAddressInfo.GetAddressList("Area"), "Area");
            //txtCurrentCity.EnableAutoCompleteFromDataTable(objAddressInfo.GetAddressList("City"), "City");
            //txtCurrentPIN.EnableAutoCompleteFromDataTable(objAddressInfo.GetAddressList("PIN"), "PIN");
            //txtCurrentState.EnableAutoCompleteFromDataTable(objStates.GetStateList("StateTitle"), "StateTitle");

            //txtPermanentAddress01.EnableAutoCompleteFromDataTable(objAddressInfo.GetAddressList("Address1"), "Address1");
            //txtPermanentAddress02.EnableAutoCompleteFromDataTable(objAddressInfo.GetAddressList("Address2"), "Address2");
            //txtPermanentArea.EnableAutoCompleteFromDataTable(objAddressInfo.GetAddressList("Area"), "Area");
            //txtPermanentCity.EnableAutoCompleteFromDataTable(objAddressInfo.GetAddressList("City"), "City");
            //txtPermanentPIN.EnableAutoCompleteFromDataTable(objAddressInfo.GetAddressList("PIN"), "PIN");
            //txtPermanentState.EnableAutoCompleteFromDataTable(objStates.GetStateList("StateTitle"), "StateTitle");

            cmbCurrentCountry.DataSource = objCountries.GetCountryList();
            cmbCurrentCountry.DisplayMember = "CountryTitle";
            cmbCurrentCountry.ValueMember = "CountryID";

            cmbPermanentCountry.DataSource = objCountries.GetCountryList();
            cmbPermanentCountry.DisplayMember = "CountryTitle";
            cmbPermanentCountry.ValueMember = "CountryID";

            chkEduQualList.ColumnWidth = 300;
            ((ListBox)chkEduQualList).DataSource = objEduQualMas.GetEduQualMasList();
            ((ListBox)chkEduQualList).DisplayMember = "EduQualTitle";
            ((ListBox)chkEduQualList).ValueMember = "EduQualID";

            chkSkillsList.ColumnWidth = 300;
            ((ListBox)chkSkillsList).DataSource = objSkills.GetSkillList();
            ((ListBox)chkSkillsList).DisplayMember = "SkillTitle";
            ((ListBox)chkSkillsList).ValueMember = "SkillID";

            cmbSalProfile.DataSource = objSalaryProfile.GetSalProfileTitleList();
            cmbSalProfile.DisplayMember = "SalProfileTitle";
            cmbSalProfile.ValueMember = "SalProfileID";

            cmbEmpTaxScheme.DataSource = objTaxMas.GetTaxList();
            cmbEmpTaxScheme.DisplayMember = "TaxTitle";
            cmbEmpTaxScheme.ValueMember = "TaxSchemeID";

            cmbWeeklyOff.DataSource = objWeeklyOffInfo.getWklyOffProfileMasInfoList("");
            cmbWeeklyOff.DisplayMember = "WklyOffTitle";
            cmbWeeklyOff.ValueMember = "WklyOffMasID";
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

            if (lblActionMode.Text == "add")
            {
                int iRowCounter = 1;

                if (lblReportingManagerID.Text.ToString().Trim() == "")
                    lblReportingManagerID.Text = "1";

                int employeeID = objEmployeeMaster.InsertEmployeeMaster(Convert.ToInt16(lblEmpID.Text.Trim()), txtEmpCode.Text.Trim(), txtEmployeeName.Text.Trim(), cmbDesignation.SelectedIndex + 1, Convert.ToInt16(lblReportingManagerID.Text.Trim()), cmbDepartment.SelectedIndex + 1, cmbBloodGroup.SelectedIndex + 1, true, false, CurrentUser.ClientID);
                if (employeeID > 0)
                {
                    int userID = objLogin.InsertUserInfo(employeeID, true, false, txtEmployeeMailID.Text, objEncryptDecrypt.encryptText(txtDateOfBirth.Text.ToString()));

                    if (tabPersonalPhoto.Visible == true)
                    {
                        if (txtEmpPhoto.Text != "overwrite")
                        {
                            byte[] image_bytes = objImpageOperation.ImageToBytes(picEmpPhoto.Image, ImageFormat.Jpeg, txtEmpPhoto.Text == "overwrite" ? true : true);
                            if (image_bytes.Length > 0)
                            {
                                int photoID = objPhotoMas.UpdatePhotoInfo(employeeID, image_bytes);
                                if (photoID == 0)
                                    photoID = objPhotoMas.InsertPhotoInfo(employeeID, image_bytes);
                            }
                        }
                    }

                    int contactInfoID01 = 0;

                    if (tabProfessionalInfo.Visible == true)
                    {
                        int nomineeID = objNomineeInfo.InsertNomineeIfo(txtNomineeName.Text.Trim(), employeeID, cmbNomineeRelationship.SelectedIndex + 1, txtNomineeContactNumber.Text.Trim());
                        contactInfoID01 = objContactPerson.InsertContactInfo(txtContactPersonName.Text.Trim(), txtContactPersonNumber.Text.ToString(), cmbContactPersonRelationship.SelectedIndex + 1, 1);

                        int shiftInfoID = objShiftMas.InsertEmployeeShiftInfo(employeeID, cmbShift.SelectedIndex + 1, Convert.ToDateTime(txtShiftEffectiveFrom.Text.Trim()));
                        int employmentTypeID = objEmploymentTypeInfo.InsertEmploymentTypeInfo(employeeID, cmbEmploymentType.SelectedIndex + 1, Convert.ToDateTime(txtEmploymentEffectiveFrom.Text.Trim()));
                    }

                    if (tabPersonalInfo.Visible == true)
                    {
                        int curAddressID = objAddressInfo.InsertAddressInfo(txtCurrentAddress01.Text.Trim(), txtCurrentAddress02.Text.Trim(), txtCurrentArea.Text.Trim(), txtCurrentCity.Text.Trim(), txtCurrentPIN.Text.Trim(), txtCurrentState.Text.Trim(), cmbCurrentCountry.Text);
                        int perAddressID = objAddressInfo.InsertAddressInfo(txtPermanentAddress01.Text.Trim(), txtPermanentAddress02.Text.Trim(), txtPermanentArea.Text.Trim(), txtPermanentCity.Text.Trim(), txtPermanentPIN.Text.Trim(), txtPermanentState.Text.Trim(), cmbPermanentCountry.Text);
                        int personalInfoID = 1; // objEmployeePersonalInfo.InsertEmployeePersonalInfo(employeeID, Convert.ToDateTime(txtDateOfBirth.Text), Convert.ToDateTime(txtDateOfJoining.Text), 1, curAddressID, perAddressID, txtEmployeeContactNumber.Text.Trim(), txtEmployeeMailID.Text.Trim(), contactInfoID01, contactInfoID01, cmbGender.SelectedIndex + 1, 1);
                        //int personalIDInfoID = objEmployeePersonalIDInfo.InsertEmployeePersonalIDInfo(personalInfoID, txtAadhaarCardNumber.Text.Trim(), txtVoterCardNumber.Text.Trim(), txtPANCardNumber.Text.Trim(), txtPassportNumber.Text.Trim(), Convert.ToDateTime(txtPassportIssueDate.Text), Convert.ToDateTime(txtPassportRenewalDate.Text), txtAdditonalCardNumber.Text.Trim(), "", "", "", "");
                    }

                    if (tabLeaves.Visible == true)
                    {
                        int employeeLeaveAllotmentID = objLeaveTRList.InsertDefaultLeaveAllotment(Convert.ToInt16(lblEmpID.Text.Trim()), Convert.ToDecimal(txtTotalLeaveAllotment.Text), Convert.ToDecimal(txtBalanceLeaveAllotment.Text), DateTime.Now);
                        lblLeaveMasID.Text = employeeLeaveAllotmentID.ToString();
                        int employeeLeaveTRID = objLeaveTRList.InsertLeaveTransaction(Convert.ToInt16(lblEmpID.Text.ToString()), 1, DateTime.Now, "By Leave Allotment", DateTime.Now, DateTime.Now, 0, "", DateTime.Now, "", DateTime.Now, "", Convert.ToInt16(lblEmpID.Text.ToString()));

                        iRowCounter = 1;
                        foreach (DataGridViewRow dc in dtgLeaveEntitlement.Rows)
                        {
                            int empLeaveEntitlementID = 0;
                            if (Convert.ToInt16(dc.Cells["LeaveEntmtID"].Value.ToString()) == 0)
                            {
                                empLeaveEntitlementID = objEmpLeaveEntitlementInfo.InsertLeaveEntitlementInfo(Convert.ToInt16(lblEmpID.Text.Trim()), Convert.ToInt16(lblLeaveMasID.Text.ToString()), Convert.ToInt16(dc.Cells["LeaveTypeID"].Value.ToString()), Convert.ToInt16(dc.Cells["TotalLeaves"].Value.ToString()), Convert.ToDecimal(dc.Cells["BalanceLeaves"].Value.ToString()), iRowCounter);
                            }
                            else
                            {
                                empLeaveEntitlementID = objEmpLeaveEntitlementInfo.UpadateLeaveEntitlementInfo(Convert.ToInt16(dc.Cells["LeaveEntmtID"].Value.ToString()), Convert.ToInt16(lblEmpID.Text.Trim()), Convert.ToInt16(dc.Cells["LeaveMasID"].Value.ToString()), Convert.ToInt16(dc.Cells["LeaveTypeID"].Value.ToString()), Convert.ToInt16(dc.Cells["TotalLeaves"].Value.ToString()), Convert.ToDecimal(dc.Cells["BalanceLeaves"].Value.ToString()), iRowCounter);
                            }
                            iRowCounter = iRowCounter + 1;
                        }

                        int employeWeeklyOffID = objWeeklyOffInfo.InsertEmployeeSpecificWeeklyInfo(Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToInt16(cmbWeeklyOff.SelectedIndex + 1), DateTime.Now);
                    }

                    if (tabBankAccountInfo1.Visible == true)
                    {
                        int employeeBankAccountID = objBankInfo.InsertEmployeeBankReference(Convert.ToInt16(lblEmpID.Text.ToString()), txtBankAccountNumber.Text.Trim(), lstBankList.SelectedItems[0].Index + 1, true);
                    }

                    foreach (DataGridViewRow dc in dtgFamilyMemberInforamtion.Rows)
                    {
                        if (!string.IsNullOrEmpty(dc.Cells["EmpPerFamInfoID"].Value.ToString()))
                        {
                            int EmpWorkExpID = 0;
                            if (Convert.ToInt16(dc.Cells["EmpPerFamInfoID"].Value.ToString()) == 0)
                                EmpWorkExpID = objEmpPersonalFamilyMemberInfo.InsertEmployeePersonalFamilyMemberInfo(Convert.ToInt16(dc.Cells["EmpPerFamInfoID"].Value.ToString()), Convert.ToInt16(dc.Cells["PersonalInfoID"].Value.ToString()), dc.Cells["FamMemName"].Value.ToString(), Convert.ToDateTime(dc.Cells["FamMemDOB"].Value.ToString()), Convert.ToInt16(dc.Cells["FamMemAge"].Value.ToString()), dc.Cells["FamMemRelationship"].Value.ToString(), dc.Cells["FamMemAddr1"].Value.ToString(), dc.Cells["FamMemAddr2"].Value.ToString(), dc.Cells["FamMemArea"].Value.ToString(), dc.Cells["FamMemCity"].Value.ToString(), dc.Cells["FamMemState"].Value.ToString(), dc.Cells["FamMemPIN"].Value.ToString(), dc.Cells["FamMemCountry"].Value.ToString(), dc.Cells["FamMemContactNumber"].Value.ToString(), dc.Cells["FamMemMailID"].Value.ToString(), dc.Cells["FamMemBloodGroup"].Value.ToString(), Convert.ToBoolean(dc.Cells["FamMemInsuranceEnrolled"].Value.ToString()));
                            else
                                EmpWorkExpID = objEmpPersonalFamilyMemberInfo.InsertEmployeePersonalFamilyMemberInfo(Convert.ToInt16(dc.Cells["EmpPerFamInfoID"].Value.ToString()), Convert.ToInt16(dc.Cells["PersonalInfoID"].Value.ToString()), dc.Cells["FamMemName"].Value.ToString(), Convert.ToDateTime(dc.Cells["FamMemDOB"].Value.ToString()), Convert.ToInt16(dc.Cells["FamMemAge"].Value.ToString()), dc.Cells["FamMemRelationship"].Value.ToString(), dc.Cells["FamMemAddr1"].Value.ToString(), dc.Cells["FamMemAddr2"].Value.ToString(), dc.Cells["FamMemArea"].Value.ToString(), dc.Cells["FamMemCity"].Value.ToString(), dc.Cells["FamMemState"].Value.ToString(), dc.Cells["FamMemPIN"].Value.ToString(), dc.Cells["FamMemCountry"].Value.ToString(), dc.Cells["FamMemContactNumber"].Value.ToString(), dc.Cells["FamMemMailID"].Value.ToString(), dc.Cells["FamMemBloodGroup"].Value.ToString(), Convert.ToBoolean(dc.Cells["FamMemInsuranceEnrolled"].Value.ToString()));
                        }
                    }

                    if (tabPreviousExperience1.Visible == true)
                    {
                        foreach (DataGridViewRow dc in dtgPreviousWorkExp.Rows)
                        {
                            if(!string.IsNullOrEmpty(dc.Cells["StartDate"].Value.ToString()) && !string.IsNullOrEmpty(dc.Cells["EndDate"].Value.ToString()))
                            {
                                int EmpWorkExpID = 0;
                                if (Convert.ToInt16(dc.Cells["LastCompanyInfoID"].Value.ToString()) > 0)
                                    EmpWorkExpID = objEmpWorkExperienceInfo.InsertEmpWorkExpInfo(Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToInt16(dc.Cells["LastCompanyInfoID"].Value.ToString()), Convert.ToDateTime(dc.Cells["StartDate"].Value.ToString()), Convert.ToDateTime(dc.Cells["EndDate"].Value.ToString()), dc.Cells["Comments"].Value.ToString());
                                else
                                    EmpWorkExpID = objEmpWorkExperienceInfo.UpdatetEmpWorkExpInfo(Convert.ToInt16(dc.Cells["LastCompID"].Value.ToString()), Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToInt16(dc.Cells["LastCompanyInfoID"].Value.ToString()), Convert.ToDateTime(dc.Cells["StartDate"].Value.ToString()), Convert.ToDateTime(dc.Cells["EndDate"].Value.ToString()), dc.Cells["Comments"].Value.ToString());
                            }
                        }
                    }

                    if (tabDocuments1.Visible == true)
                    {
                        for (int linkedDocumentIDCount = 0; linkedDocumentIDCount <= lstLDocumentsList.Items.Count - 1; linkedDocumentIDCount++)
                        {
                            int linkedDocumentID = 0;
                            EmployeeDocumentInfo employeeDocumentInfo = objUploadDocument.isDocumentReferenced(Convert.ToInt16(lblEmpID.Text), Convert.ToInt16(lstLDocumentsList.Items[linkedDocumentIDCount].SubItems[0].Text.ToString()));
                            if (employeeDocumentInfo.EmpDocumentID == 0)
                                linkedDocumentID = objUploadDocument.InsertLinkUpdatedDocuments(Convert.ToInt16(lblEmpID.Text), Convert.ToInt16(lstLDocumentsList.Items[linkedDocumentIDCount].SubItems[0].Text.ToString()));
                            else
                                linkedDocumentID = objUploadDocument.UpdateLinkUpdatedDocuments(Convert.ToInt16(employeeDocumentInfo.EmpDocumentID.ToString()), Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToInt16(employeeDocumentInfo.DocID.ToString()));
                        }
                    }

                    if (tabEducationalInfo1.Visible == true)
                    {
                        int deletedEduQualCount = objEmpEduQualInfo.DeleteEmpEduQualInfo(employeeID);
                        for (int iEduQualCounter = 0; iEduQualCounter < chkEduQualList.Items.Count - 1; iEduQualCounter++)
                        {
                            if (chkEduQualList.GetItemChecked(iEduQualCounter) == true)
                            {
                                objEmpEduQualInfo.InsertEmpEduQualInfo(employeeID, iEduQualCounter + 1, 5);
                                //Thread.Sleep(100);
                            }
                        }
                    }

                    if (tabSkils.Visible == true)
                    {
                        int deletedSkillsCount = objEmpSkillMas.DeleteSkillsInfo(employeeID);
                        for (int iSkillCounter = 0; iSkillCounter < chkSkillsList.Items.Count - 1; iSkillCounter++)
                        {
                            if (chkSkillsList.GetItemChecked(iSkillCounter) == true)
                            {
                                objEmpSkillMas.InsertEmpSkillsInfo(employeeID, iSkillCounter + 1, 5);
                                //Thread.Sleep(100);
                            }
                        }
                    }

                    decimal AllowanceAmount = 0;
                    decimal DeductionAmount = 0;
                    decimal ReimbursmentAmount = 0;
                    iRowCounter = 1;

                    if (tabSalaryProfile.Visible == true)
                    {
                        int employeeSalaryProfileID = objEmployeeSalaryProfileInfo.InsertEmployeeEmployeeSalaryProfileInfo(Convert.ToInt16(lblEmpID.Text.ToString()), cmbSalProfile.SelectedIndex + 1, DateTime.Now);

                        int empSalaryID = objEmployeePayroll.InsertEmployeeSalaryMasterInfo(Convert.ToInt16(lblEmpID.Text.ToString().ToString().Trim()), Convert.ToDateTime(DateTime.Now.ToString()), "Jan - 1900", 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, false);
                        foreach (DataGridViewRow dc in dtgSalaryProfileDetails.Rows)
                        {
                            //int EmpSalDetID = objEmployeePayroll.InsertEmployeeSalaryDetailsInfo(Convert.ToInt16(empSalaryID), Convert.ToInt16(dc.Cells["SalProDetID"].Value.ToString()), Convert.ToInt16(dc.Cells["HeaderID"].Value.ToString()), dc.Cells["HeaderTitle"].Value.ToString(), dc.Cells["HeaderType"].Value.ToString(), Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()), Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString()), Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString()), 0);
                            iRowCounter = iRowCounter + 1;
                        }

                        objTaxMas.InsertEmployeeTaxSchemeModel(Convert.ToInt16(lblEmpID.Text.ToString()), cmbEmpTaxScheme.SelectedIndex + 1, Convert.ToDateTime(txtTaxSchemeEffectiveFrom.Text.ToString()));
                    }
                    MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Details not inserted successfully.\nPlease verify once again.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (lblActionMode.Text == "modify")
            {
                if (Convert.ToInt16(lblBankID.Text.ToString()) != 0 && Convert.ToInt16(lblBankID.Text.ToString()) != lstBankList.SelectedItems[0].Index + 1)
                {
                    if (MessageBox.Show("The Bank Name is getting changed from \n\n" + lstBankList.Items[Convert.ToInt16(lblBankID.Text.ToString()) - 1].SubItems[2].Text + " : " + lstBankList.Items[Convert.ToInt16(lblBankID.Text.ToString()) - 1].SubItems[3].Text + " [" + lstBankList.Items[Convert.ToInt16(lblBankID.Text.ToString()) - 1].SubItems[4].Text + "]" + " \n\nto\n\n " + lstBankList.SelectedItems[0].SubItems[2].Text + " : " + lstBankList.SelectedItems[0].SubItems[3].Text + " [" + lstBankList.SelectedItems[0].SubItems[4].Text + "]" + ".\n\n\nAre you sure to continue.?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        lstBankList.Items[Convert.ToInt16(lblBankID.Text.ToString()) - 1].Selected = true;
                        return;
                    }
                }

                int iRowCounter = 1;

                int employeeID = objEmployeeMaster.UpdateEmployeeMaster(Convert.ToInt16(lblEmpID.Text.Trim()), txtEmpCode.Text.Trim(), txtEmployeeName.Text.Trim(), cmbDesignation.SelectedIndex + 1, Convert.ToInt16(lblReportingManagerID.Text.Trim()), cmbDepartment.SelectedIndex + 1, cmbBloodGroup.SelectedIndex + 1, true, false, CurrentUser.ClientID);
                if (employeeID > 0)
                {
                    if (txtEmpPhoto.Text != "")
                    {
                        byte[] image_bytes = objImpageOperation.ImageToBytes(picEmpPhoto.Image, ImageFormat.Jpeg, txtEmpPhoto.Text == "overwrite" ? true : true);
                        if (image_bytes.Length > 0)
                        {
                            int photoID = objPhotoMas.UpdatePhotoInfo(employeeID, image_bytes);
                            if (photoID == 0)
                                photoID = objPhotoMas.InsertPhotoInfo(employeeID, image_bytes);
                        }
                    }
                    int curAddressID = objAddressInfo.UpdateAddressInfo(Convert.ToInt16(lblCurrentAddressID.Text.Trim()), txtCurrentAddress01.Text.Trim(), txtCurrentAddress02.Text.Trim(), txtCurrentArea.Text.Trim(), txtCurrentCity.Text.Trim(), txtCurrentPIN.Text.Trim(), txtCurrentState.Text.Trim(), cmbCurrentCountry.Text);
                    int perAddressID = objAddressInfo.UpdateAddressInfo(Convert.ToInt16(lblPermanentAddressID.Text.Trim()), txtPermanentAddress01.Text.Trim(), txtPermanentAddress02.Text.Trim(), txtPermanentArea.Text.Trim(), txtPermanentCity.Text.Trim(), txtPermanentPIN.Text.Trim(), txtPermanentState.Text.Trim(), cmbPermanentCountry.Text);
                    int contactInfoID01 = objContactPerson.UdpateContactInfo(Convert.ToInt16(lblContactInfoID.Text.Trim()), txtContactPersonName.Text.Trim(), txtContactPersonNumber.Text.ToString(), cmbContactPersonRelationship.SelectedIndex + 1, 1);
                    int personalInfoID = 1; // objEmployeePersonalInfo.UpdateEmployeePersonalInfo(employeeID, Convert.ToDateTime(txtDateOfBirth.Text), Convert.ToDateTime(txtDateOfJoining.Text), 1, curAddressID, perAddressID, txtEmployeeContactNumber.Text.Trim(), txtEmployeeMailID.Text.Trim(), contactInfoID01, contactInfoID01, cmbGender.SelectedIndex + 1, 1);
                    //int personalIDInfoID = objEmployeePersonalIDInfo.UpdateEmployeePersonalIDInfo(Convert.ToInt16(lblEmpGovtID.Text.Trim()), personalInfoID, txtAadhaarCardNumber.Text.Trim(), txtVoterCardNumber.Text.Trim(), txtPANCardNumber.Text.Trim(), txtPassportNumber.Text.Trim(), Convert.ToDateTime(txtPassportIssueDate.Text), Convert.ToDateTime(txtPassportRenewalDate.Text), txtAdditonalCardNumber.Text.Trim(), "", "", "", "");

                    int nomineeID = objNomineeInfo.UdpateNomineeInfo(Convert.ToInt16(lblNomineeID.Text.Trim()), txtNomineeName.Text.Trim(), employeeID, cmbNomineeRelationship.SelectedIndex + 1, txtNomineeContactNumber.Text.Trim());
                    if (nomineeID == 0)
                        nomineeID = objNomineeInfo.InsertNomineeIfo(txtNomineeName.Text.Trim(), employeeID, cmbNomineeRelationship.SelectedIndex + 1, txtNomineeContactNumber.Text.Trim());


                    int shiftInfoID = objShiftMas.UpdateEmployeeShiftInfo(1, employeeID, cmbShift.SelectedIndex + 1, Convert.ToDateTime(txtShiftEffectiveFrom.Text.Trim()));
                    if (shiftInfoID == 0)
                        shiftInfoID = objShiftMas.InsertEmployeeShiftInfo(employeeID, cmbShift.SelectedIndex + 1, Convert.ToDateTime(txtShiftEffectiveFrom.Text.Trim()));

                    int employmentTypeID = objEmploymentTypeInfo.UpdateEmploymentTypeInfo(1, employeeID, cmbEmploymentType.SelectedIndex + 1, Convert.ToDateTime(txtEmploymentEffectiveFrom.Text.Trim()));
                    if (employmentTypeID == 0)
                        employmentTypeID = objEmploymentTypeInfo.InsertEmploymentTypeInfo(employeeID, cmbEmploymentType.SelectedIndex + 1, Convert.ToDateTime(txtEmploymentEffectiveFrom.Text.Trim()));

                    int employeeBankAccountID = objBankInfo.UpdateEmployeeBankReference(Convert.ToInt16(lblBankID.Text.ToString()), Convert.ToInt16(lblEmpID.Text.ToString()), txtBankAccountNumber.Text.Trim(), lstBankList.SelectedItems[0].Index + 1, true);
                    if (employeeBankAccountID == 0)
                        employeeBankAccountID = objBankInfo.InsertEmployeeBankReference(Convert.ToInt16(lblEmpID.Text.ToString()), txtBankAccountNumber.Text.Trim(), lstBankList.SelectedItems[0].Index + 1, true);


                    foreach (DataGridViewRow dc in dtgFamilyMemberInforamtion.Rows)
                    {
                        if (!string.IsNullOrEmpty(dc.Cells["FamMemName"].Value.ToString()))
                        {
                            int EmpWorkExpID = 0;
                            if (Convert.ToInt16(dc.Cells["EmpPerFamInfoID"].Value.ToString()) == 0)
                                EmpWorkExpID = objEmpPersonalFamilyMemberInfo.InsertEmployeePersonalFamilyMemberInfo(Convert.ToInt16(dc.Cells["EmpPerFamInfoID"].Value.ToString()), Convert.ToInt16(dc.Cells["PersonalInfoID"].Value.ToString()), dc.Cells["FamMemName"].Value.ToString(), Convert.ToDateTime(dc.Cells["FamMemDOB"].Value), Convert.ToInt16(dc.Cells["FamMemAge"].Value.ToString()), dc.Cells["FamMemRelationship"].Value.ToString(), dc.Cells["FamMemAddr1"].Value.ToString(), dc.Cells["FamMemAddr2"].Value.ToString(), dc.Cells["FamMemArea"].Value.ToString(), dc.Cells["FamMemCity"].Value.ToString(), dc.Cells["FamMemState"].Value.ToString(), dc.Cells["FamMemPIN"].Value.ToString(), dc.Cells["FamMemCountry"].Value.ToString(), dc.Cells["FamMemContactNumber"].Value.ToString(), dc.Cells["FamMemMailID"].Value.ToString(), dc.Cells["FamMemBloodGroup"].Value.ToString(), Convert.ToBoolean(dc.Cells["FamMemInsuranceEnrolled"].Value.ToString()));
                            else
                                EmpWorkExpID = objEmpPersonalFamilyMemberInfo.UpdateEmployeePersonalFamilyMemberInfo(Convert.ToInt16(dc.Cells["EmpPerFamInfoID"].Value.ToString()), Convert.ToInt16(dc.Cells["PersonalInfoID"].Value.ToString()), dc.Cells["FamMemName"].Value.ToString(), Convert.ToDateTime(dc.Cells["FamMemDOB"].Value.ToString()), Convert.ToInt16(dc.Cells["FamMemAge"].Value.ToString()), dc.Cells["FamMemRelationship"].Value.ToString(), dc.Cells["FamMemAddr1"].Value.ToString(), dc.Cells["FamMemAddr2"].Value.ToString(), dc.Cells["FamMemArea"].Value.ToString(), dc.Cells["FamMemCity"].Value.ToString(), dc.Cells["FamMemState"].Value.ToString(), dc.Cells["FamMemPIN"].Value.ToString(), dc.Cells["FamMemCountry"].Value.ToString(), dc.Cells["FamMemContactNumber"].Value.ToString(), dc.Cells["FamMemMailID"].Value.ToString(), dc.Cells["FamMemBloodGroup"].Value.ToString(), Convert.ToBoolean(dc.Cells["FamMemInsuranceEnrolled"].Value.ToString()));
                        }
                    }

                    foreach (DataGridViewRow dc in dtgPreviousWorkExp.Rows)
                    {
                        if (!string.IsNullOrEmpty(dc.Cells["StartDate"].Value.ToString()) && !string.IsNullOrEmpty(dc.Cells["EndDate"].Value.ToString()))
                        {
                            int EmpWorkExpID = 0;
                            if (Convert.ToInt16(dc.Cells["LastCompID"].Value.ToString()) > 0)
                                EmpWorkExpID = objEmpWorkExperienceInfo.UpdatetEmpWorkExpInfo(Convert.ToInt16(dc.Cells["LastCompID"].Value.ToString()), Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToInt16(dc.Cells["LastCompanyInfoID"].Value.ToString()), Convert.ToDateTime(dc.Cells["StartDate"].Value.ToString()), Convert.ToDateTime(dc.Cells["EndDate"].Value.ToString()), dc.Cells["Comments"].Value.ToString());
                            else
                                EmpWorkExpID = objEmpWorkExperienceInfo.InsertEmpWorkExpInfo(Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToInt16(dc.Cells["LastCompanyInfoID"].Value.ToString()), Convert.ToDateTime(dc.Cells["StartDate"].Value.ToString()), Convert.ToDateTime(dc.Cells["EndDate"].Value.ToString()), dc.Cells["Comments"].Value.ToString());
                        }
                    }

                    for (int linkedDocumentIDCount = 0; linkedDocumentIDCount <= lstLDocumentsList.Items.Count - 1; linkedDocumentIDCount++)
                    {
                        int linkedDocumentID = 0;
                        EmployeeDocumentInfo employeeDocumentInfo = objUploadDocument.isDocumentReferenced(Convert.ToInt16(lblEmpID.Text), Convert.ToInt16(lstLDocumentsList.Items[linkedDocumentIDCount].SubItems[0].Text.ToString()));
                        if (employeeDocumentInfo.EmpDocumentID == 0)
                            linkedDocumentID = objUploadDocument.InsertLinkUpdatedDocuments(Convert.ToInt16(lblEmpID.Text), Convert.ToInt16(lstLDocumentsList.Items[linkedDocumentIDCount].SubItems[0].Text.ToString()));
                        else
                            linkedDocumentID = objUploadDocument.UpdateLinkUpdatedDocuments(Convert.ToInt16(employeeDocumentInfo.EmpDocumentID.ToString()), Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToInt16(employeeDocumentInfo.DocID.ToString()));
                    }

                    int deletedEduQualCount = objEmpEduQualInfo.DeleteEmpEduQualInfo(employeeID);
                    for (int iEduQualCounter = 0; iEduQualCounter < chkEduQualList.Items.Count; iEduQualCounter++)
                    {
                        if (chkEduQualList.GetItemChecked(iEduQualCounter) == true)
                        {
                            objEmpEduQualInfo.InsertEmpEduQualInfo(employeeID, iEduQualCounter + 1, 5);
                            //Thread.Sleep(100);
                        }
                    }

                    int deletedSkillsCount = objEmpSkillMas.DeleteSkillsInfo(employeeID);
                    for (int iSkillCounter = 0; iSkillCounter < chkSkillsList.Items.Count; iSkillCounter++)
                    {
                        if (chkSkillsList.GetItemChecked(iSkillCounter) == true)
                        {
                            objEmpSkillMas.InsertEmpSkillsInfo(employeeID, iSkillCounter + 1, 5);
                            //Thread.Sleep(100);
                        }
                    }

                    if (tabLeaves.Visible == true)
                    {
                        int employeeLeaveAllotmentID = objLeaveTRList.UpdateEmployeeLeaveBalance(1, Convert.ToInt16(lblEmpID.Text.Trim()), Convert.ToDecimal(txtTotalLeaveAllotment.Text), Convert.ToDecimal(txtBalanceLeaveAllotment.Text), DateTime.Now);
                        int employeeLeaveTRID = objLeaveTRList.InsertLeaveTransaction(Convert.ToInt16(lblEmpID.Text.ToString()), 1, DateTime.Now, "By Leave Allotment", DateTime.Now, DateTime.Now, 0, "", DateTime.Now, "", DateTime.Now, "", Convert.ToInt16(lblEmpID.Text.ToString()));

                        iRowCounter = 1;
                        foreach (DataGridViewRow dc in dtgLeaveEntitlement.Rows)
                        {
                            int empLeaveEntitlementID = 0;
                            //decimal TotalLeaves = 0;
                            //decimal BalanceLeaves = 0;

                            //if (dc.Cells["TotalLeaves"].Value.ToString() != "0.00")
                            //    TotalLeaves = Convert.ToDecimal(dc.Cells["TotalLeaves"].Value.ToString());
                            //else
                            //    TotalLeaves = Convert.ToDecimal(dc.Cells["TotalLeaves"].Value.ToString());

                            if (Convert.ToInt16(dc.Cells["LeaveEntmtID"].Value.ToString()) == 0)
                            {
                                empLeaveEntitlementID = objEmpLeaveEntitlementInfo.InsertLeaveEntitlementInfo(Convert.ToInt16(lblEmpID.Text.Trim()), Convert.ToInt16(lblLeaveMasID.Text.ToString()), Convert.ToInt16(dc.Cells["LeaveTypeID"].Value.ToString()), Convert.ToInt16(dc.Cells["TotalLeaves"].Value.ToString()), Convert.ToDecimal(dc.Cells["BalanceLeaves"].Value.ToString()), iRowCounter);
                            }
                            else
                            {
                                empLeaveEntitlementID = objEmpLeaveEntitlementInfo.UpadateLeaveEntitlementInfo(Convert.ToInt16(dc.Cells["LeaveEntmtID"].Value.ToString()), Convert.ToInt16(lblEmpID.Text.Trim()), Convert.ToInt16(lblLeaveMasID.Text.ToString()), Convert.ToInt16(dc.Cells["LeaveTypeID"].Value.ToString()), Convert.ToDecimal(dc.Cells["TotalLeaves"].Value.ToString()), Convert.ToDecimal(dc.Cells["BalanceLeaves"].Value.ToString()), iRowCounter);
                            }
                            iRowCounter = iRowCounter + 1;
                        }

                        int employeWeeklyOffID = objWeeklyOffInfo.UpdateEmployeeSpecificWeeklyInfo(Convert.ToInt16(lblEmployeeWeeklyOffID.Text.ToString()), Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToInt16(cmbWeeklyOff.SelectedIndex + 1), DateTime.Now);
                    }

                    if (tabSalaryProfile.Visible == true)
                    {
                        int employeeSalaryProfileID = objEmployeeSalaryProfileInfo.InsertEmployeeEmployeeSalaryProfileInfo(Convert.ToInt16(lblEmpID.Text.ToString()), cmbSalProfile.SelectedIndex + 1, DateTime.Now);

                        iRowCounter = 1;
                        int empSalaryID = objEmployeePayroll.InsertEmployeeSalaryMasterInfo(Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToDateTime(DateTime.Now.ToString()), "Jan - 1900", 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, false);
                        foreach (DataGridViewRow dc in dtgSalaryProfileDetails.Rows)
                        {
                            //int EmpSalDetID = objEmployeePayroll.InsertEmployeeSalaryDetailsInfo(Convert.ToInt16(empSalaryID.ToString()), Convert.ToInt16(dc.Cells["SalProDetID"].Value.ToString()), Convert.ToInt16(dc.Cells["HeaderID"].Value.ToString()), dc.Cells["HeaderTitle"].Value.ToString(), dc.Cells["HeaderType"].Value.ToString(), Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()), Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString()), Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString()), iRowCounter);
                            iRowCounter = iRowCounter + 1;
                        }

                        objTaxMas.UpdateEmployeeTaxSchemeModel(Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToInt16(lblEmpID.Text.ToString()), cmbEmpTaxScheme.SelectedIndex + 1, Convert.ToDateTime(txtTaxSchemeEffectiveFrom.Text.ToString()));
                    }
                    MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Details not inserted successfully.\nPlease verify once again.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            objTempCurrentlyLoggedInUserInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(lblReportingManagerID.Text.ToString()));

            onSaveButtonClick();
            disableControls();
            clearControls();
            errValidator.Clear();
            tabControl1.SelectedIndex = 0;
            this.Cursor = Cursors.Default;
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
                tabControl1.SelectedIndex = 0;
                onRemoveButtonClick();
                clearControls();
                //enableControls();
                lblEmpID.Text = "";
                txtEmpCode.Text = "";
                errValidator.Clear();

                cmbDepartment.DataSource = objDepartment.GetDepartmentList();
                cmbDepartment.DisplayMember = "DepartmentTitle";
                cmbDepartment.ValueMember = "DepartmentID";

                cmbGender.DataSource = objSexMaster.GetSexList();
                cmbGender.DisplayMember = "SexTitle";
                cmbGender.ValueMember = "SexID";

                cmbDesignation.DataSource = objDesignation.GetDesignationList();
                cmbDesignation.DisplayMember = "DesignationTitle";
                cmbDesignation.ValueMember = "DesignationID";

                cmbContactPersonRelationship.DataSource = objRelationship.GetRelationshipList();
                cmbContactPersonRelationship.DisplayMember = "RelationShipTitle";
                cmbContactPersonRelationship.ValueMember = "RelationShipID";

                cmbNomineeRelationship.DataSource = objRelationship.GetRelationshipList();
                cmbNomineeRelationship.DisplayMember = "RelationShipTitle";
                cmbNomineeRelationship.ValueMember = "RelationShipID";

                cmbBloodGroup.DataSource = objBloodGroup.GetBloodGroupList();
                cmbBloodGroup.DisplayMember = "BloodGroupTitle";
                cmbBloodGroup.ValueMember = "BloodGroupID";

                cmbCurrentCountry.DataSource = objCountries.GetCountryList();
                cmbCurrentCountry.DisplayMember = "CountryTitle";
                cmbCurrentCountry.ValueMember = "CountryID";

                cmbPermanentCountry.DataSource = objCountries.GetCountryList();
                cmbPermanentCountry.DisplayMember = "CountryTitle";
                cmbPermanentCountry.ValueMember = "CountryID";

                chkSkillsList.ColumnWidth = 300;
                ((ListBox)chkSkillsList).DataSource = objSkills.GetSkillList();
                ((ListBox)chkSkillsList).DisplayMember = "SkillTitle";
                ((ListBox)chkSkillsList).ValueMember = "SkillID";

                cmbSalProfile.DataSource = objSalaryProfile.GetSalProfileTitleList();
                cmbSalProfile.DisplayMember = "SalProfileTitle";
                cmbSalProfile.ValueMember = "SalProfileID";

                cmbEmpTaxScheme.DataSource = objTaxMas.GetTaxList();
                cmbEmpTaxScheme.DisplayMember = "TaxTitle";
                cmbEmpTaxScheme.ValueMember = "TaxSchemeID";
            }
            else if (lblActionMode.Text == "delete")
            {
                if (MessageBox.Show("The selected record will be deleted. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int affectedRows = objEmployeeMaster.DeleteEmployeeMaster(Convert.ToInt16(lblEmpID.Text.Trim()));
                    if (affectedRows > 0)
                        MessageBox.Show("Details deleted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                onCancelButtonClick();
                disableControls();
                clearControls();
                lblActionMode.Text = "";
                errValidator.Clear();
                tabControl1.SelectedIndex = 0;
            }
        }

        private void txtCurrentAddress01_TextChanged(object sender, EventArgs e)
        {
            if (chkSamePerAddAsCurAdd.Checked == true)
                txtPermanentAddress01.Text = txtCurrentAddress01.Text.Trim();
        }

        private void txtCurrentAddress02_TextChanged(object sender, EventArgs e)
        {
            if (chkSamePerAddAsCurAdd.Checked == true)
                txtPermanentAddress02.Text = txtCurrentAddress02.Text.Trim();
        }

        private void txtCurrentArea_TextChanged(object sender, EventArgs e)
        {
            if (chkSamePerAddAsCurAdd.Checked == true)
                txtPermanentArea.Text = txtCurrentArea.Text.Trim();
        }

        private void txtCurrentCity_TextChanged(object sender, EventArgs e)
        {
            if (chkSamePerAddAsCurAdd.Checked == true)
                txtPermanentCity.Text = txtCurrentCity.Text.Trim();
        }

        private void txtCurrentState_TextChanged(object sender, EventArgs e)
        {
            if (chkSamePerAddAsCurAdd.Checked == true)
                txtPermanentState.Text = txtCurrentState.Text.Trim();
        }

        private void txtCurrentPIN_TextChanged(object sender, EventArgs e)
        {
            if (chkSamePerAddAsCurAdd.Checked == true)
                txtPermanentPIN.Text = txtCurrentPIN.Text.Trim();
        }

        private void cmbCurrentCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkSamePerAddAsCurAdd.Checked == true)
                cmbPermanentCountry.Text = cmbCurrentCountry.Text.Trim();
        }

        private void chkSamePerAddAsCurAdd_CheckedChanged(object sender, EventArgs e)
        {
            txtPermanentAddress01.ReadOnly = chkSamePerAddAsCurAdd.Checked;
            txtPermanentAddress02.ReadOnly = chkSamePerAddAsCurAdd.Checked;
            txtPermanentArea.ReadOnly = chkSamePerAddAsCurAdd.Checked;
            txtPermanentCity.ReadOnly = chkSamePerAddAsCurAdd.Checked;
            txtCurrentState.ReadOnly = chkSamePerAddAsCurAdd.Checked;
            txtPermanentPIN.ReadOnly = chkSamePerAddAsCurAdd.Checked;
            cmbPermanentCountry.Enabled = chkSamePerAddAsCurAdd.Checked;
        }

        public bool ValidateValuesOnUI()
        {
            bool validationStatus = true;
            errValidator.Clear();

            // Helper for date validation
            DateTime dob, doj;
            string dateFormat = "dd-MM-yyyy";
            CultureInfo provider = CultureInfo.InvariantCulture;

            // Employee Name
            if (string.IsNullOrEmpty(txtEmployeeName.Text))
            {
                validationStatus = false;
                txtEmployeeName.Focus();
                errValidator.SetError(this.txtEmployeeName, txtEmployeeName.Tag?.ToString() ?? "Employee Name is required.");
            }

            if (string.IsNullOrEmpty(txtDateOfBirth.Text))
            {
                validationStatus = false;
                txtDateOfBirth.Focus();
                errValidator.SetError(this.txtDateOfBirth, txtDateOfBirth.Tag?.ToString() ?? "Date of Birth is required.");
            }
            else if (!DateTime.TryParseExact(txtDateOfBirth.Text, dateFormat, provider, DateTimeStyles.None, out dob))
            {
                validationStatus = false;
                txtDateOfBirth.Focus();
                errValidator.SetError(this.txtDateOfBirth, "Invalid Date of Birth format (dd-MM-yyyy).");
            }
            else if (dob > DateTime.Now.Date)
            {
                validationStatus = false;
                txtDateOfBirth.Focus();
                errValidator.SetError(this.txtDateOfBirth, "Date of Birth cannot be in the future.");
            }

            // Date of Joining
            if (string.IsNullOrEmpty(txtDateOfJoining.Text))
            {
                validationStatus = false;
                txtDateOfJoining.Focus();
                errValidator.SetError(this.txtDateOfJoining, txtDateOfJoining.Tag?.ToString() ?? "Date of Joining is required.");
            }
            else if (!DateTime.TryParseExact(txtDateOfJoining.Text, dateFormat, provider, DateTimeStyles.None, out doj))
            {
                validationStatus = false;
                txtDateOfJoining.Focus();
                errValidator.SetError(this.txtDateOfJoining, "Invalid Date of Joining format (dd-MM-yyyy).");
            }
            else if (doj > DateTime.Now.Date)
            {
                validationStatus = false;
                txtDateOfJoining.Focus();
                errValidator.SetError(this.txtDateOfJoining, "Date of Joining cannot be in the future.");
            }
            else if (!string.IsNullOrEmpty(txtDateOfBirth.Text) && DateTime.TryParseExact(txtDateOfBirth.Text, dateFormat, provider, DateTimeStyles.None, out dob) && doj < dob)
            {
                validationStatus = false;
                txtDateOfJoining.Focus();
                errValidator.SetError(this.txtDateOfJoining, "Date of Joining cannot be before Date of Birth.");
            }

            // Current Address
            if (string.IsNullOrEmpty(txtCurrentAddress01.Text))
            {
                validationStatus = false;
                txtCurrentAddress01.Focus();
                errValidator.SetError(this.txtCurrentAddress01, txtCurrentAddress01.Tag?.ToString() ?? "Current Address Line 1 is required.");
            }
            if (string.IsNullOrEmpty(txtCurrentAddress02.Text))
            {
                validationStatus = false;
                txtCurrentAddress02.Focus();
                errValidator.SetError(this.txtCurrentAddress02, txtCurrentAddress02.Tag?.ToString() ?? "Current Address Line 2 is required.");
            }
            if (string.IsNullOrEmpty(txtCurrentArea.Text))
            {
                validationStatus = false;
                txtCurrentArea.Focus();
                errValidator.SetError(this.txtCurrentArea, txtCurrentArea.Tag?.ToString() ?? "Current Area is required.");
            }
            if (string.IsNullOrEmpty(txtCurrentCity.Text))
            {
                validationStatus = false;
                txtCurrentCity.Focus();
                errValidator.SetError(this.txtCurrentCity, txtCurrentCity.Tag?.ToString() ?? "Current City is required.");
            }
            if (string.IsNullOrEmpty(txtCurrentPIN.Text))
            {
                validationStatus = false;
                txtCurrentPIN.Focus();
                errValidator.SetError(this.txtCurrentPIN, "Enter a valid 6-digit PIN code.");
            }
            if (string.IsNullOrEmpty(txtCurrentState.Text))
            {
                validationStatus = false;
                txtCurrentState.Focus();
                errValidator.SetError(this.txtCurrentState, txtCurrentState.Tag?.ToString() ?? "Current State is required.");
            }
            if (string.IsNullOrEmpty(cmbCurrentCountry.Text))
            {
                validationStatus = false;
                cmbCurrentCountry.Focus();
                errValidator.SetError(this.cmbCurrentCountry, cmbCurrentCountry.Tag?.ToString() ?? "Current Country is required.");
            }

            // Employee Contact Number
            if (string.IsNullOrEmpty(txtEmployeeContactNumber.Text))
            {
                validationStatus = false;
                txtEmployeeContactNumber.Focus();
                errValidator.SetError(this.txtEmployeeContactNumber, "Enter a valid contact number (10-15 digits).");
            }

            // Employee Email
            if (string.IsNullOrEmpty(txtEmployeeMailID.Text))
            {
                validationStatus = false;
                txtEmployeeMailID.Focus();
                errValidator.SetError(this.txtEmployeeMailID, "Enter a valid email address.");
            }

            // Permanent Address
            if (string.IsNullOrEmpty(txtPassportIssueDate.Text))
            {
                validationStatus = false;
                txtPermanentAddress01.Focus();
                errValidator.SetError(this.txtPassportIssueDate, txtPermanentAddress01.Tag?.ToString() ?? "Passport Issue Date is required.");
            }
            // Date of Joining
            if (string.IsNullOrEmpty(txtPassportRenewalDate.Text))
            {
                validationStatus = false;
                txtDateOfJoining.Focus();
                errValidator.SetError(this.txtPassportRenewalDate, txtDateOfJoining.Tag?.ToString() ?? "Passport Renewal Date is required.");
            }
            else if (!DateTime.TryParseExact(txtPassportIssueDate.Text, dateFormat, provider, DateTimeStyles.None, out doj))
            {
                validationStatus = false;
                txtDateOfJoining.Focus();
                errValidator.SetError(this.txtPassportIssueDate, "Invalid Passport Issue Date (dd-MM-yyyy).");
            }
            else if (doj > DateTime.Now.Date)
            {
                validationStatus = false;
                txtDateOfJoining.Focus();
                errValidator.SetError(this.txtPassportIssueDate, "Passport Issue Date cannot be in the future.");
            }
            else if (!DateTime.TryParseExact(txtPassportRenewalDate.Text, dateFormat, provider, DateTimeStyles.None, out doj))
            {
                validationStatus = false;
                txtDateOfJoining.Focus();
                errValidator.SetError(this.txtPassportRenewalDate, "Invalid Passport Renewal Date (dd-MM-yyyy).");
            }
            else if (doj < DateTime.Now.Date)
            {
                validationStatus = false;
                txtDateOfJoining.Focus();
                errValidator.SetError(this.txtPassportRenewalDate, "Passport Renewal Date cannot be in the past.");
            }
            else if (!string.IsNullOrEmpty(txtPassportIssueDate.Text) && DateTime.TryParseExact(txtPassportIssueDate.Text, dateFormat, provider, DateTimeStyles.None, out dob) && doj < dob)
            {
                validationStatus = false;
                txtDateOfJoining.Focus();
                errValidator.SetError(this.txtDateOfJoining, "Passport Renewal Date cannot be before Passport Issue Date.");
            }

            if (string.IsNullOrEmpty(txtPermanentAddress02.Text))
            {
                validationStatus = false;
                txtPermanentAddress02.Focus();
                errValidator.SetError(this.txtPermanentAddress02, txtPermanentAddress02.Tag?.ToString() ?? "Permanent Address Line 2 is required.");
            }
            if (string.IsNullOrEmpty(txtPermanentArea.Text))
            {
                validationStatus = false;
                txtPermanentArea.Focus();
                errValidator.SetError(this.txtPermanentArea, txtPermanentArea.Tag?.ToString() ?? "Permanent Area is required.");
            }
            if (string.IsNullOrEmpty(txtPermanentCity.Text))
            {
                validationStatus = false;
                txtPermanentCity.Focus();
                errValidator.SetError(this.txtPermanentCity, txtPermanentCity.Tag?.ToString() ?? "Permanent City is required.");
            }
            if (string.IsNullOrEmpty(txtPermanentPIN.Text))
            {
                validationStatus = false;
                txtPermanentPIN.Focus();
                errValidator.SetError(this.txtPermanentPIN, "Enter a valid 6-digit PIN code.");
            }
            if (string.IsNullOrEmpty(txtPermanentState.Text))
            {
                validationStatus = false;
                txtPermanentState.Focus();
                errValidator.SetError(this.txtPermanentState, txtPermanentState.Tag?.ToString() ?? "Permanent State is required.");
            }
            if (string.IsNullOrEmpty(cmbPermanentCountry.Text))
            {
                validationStatus = false;
                cmbPermanentCountry.Focus();
                errValidator.SetError(this.cmbPermanentCountry, cmbPermanentCountry.Tag?.ToString() ?? "Permanent Country is required.");
            }

            // Department & Designation
            if (string.IsNullOrEmpty(cmbDepartment.Text))
            {
                validationStatus = false;
                cmbDepartment.Focus();
                errValidator.SetError(this.cmbDepartment, cmbDepartment.Tag?.ToString() ?? "Department is required.");
            }
            if (string.IsNullOrEmpty(cmbDesignation.Text))
            {
                validationStatus = false;
                cmbDesignation.Focus();
                errValidator.SetError(this.cmbDesignation, cmbDesignation.Tag?.ToString() ?? "Designation is required.");
            }
            if (string.IsNullOrEmpty(cmbShift.Text))
            {
                validationStatus = false;
                cmbShift.Focus();
                errValidator.SetError(this.cmbShift, cmbDesignation.Tag?.ToString() ?? "Active Shift is required.");
            }

            // Contact Person
            if (string.IsNullOrEmpty(txtContactPersonName.Text))
            {
                validationStatus = false;
                txtContactPersonName.Focus();
                errValidator.SetError(this.txtContactPersonName, txtContactPersonName.Tag?.ToString() ?? "Contact Person Name is required.");
            }
            if (string.IsNullOrEmpty(cmbContactPersonRelationship.Text))
            {
                validationStatus = false;
                cmbContactPersonRelationship.Focus();
                errValidator.SetError(this.cmbContactPersonRelationship, cmbContactPersonRelationship.Tag?.ToString() ?? "Contact Person Relationship is required.");
            }
            if (string.IsNullOrEmpty(txtContactPersonNumber.Text))
            {
                validationStatus = false;
                txtContactPersonNumber.Focus();
                errValidator.SetError(this.txtContactPersonNumber, "Enter a valid contact number (10-15 digits).");
            }

            // Nominee
            if (string.IsNullOrEmpty(txtNomineeName.Text))
            {
                validationStatus = false;
                txtNomineeName.Focus();
                errValidator.SetError(this.txtNomineeName, txtNomineeName.Tag?.ToString() ?? "Nominee Name is required.");
            }
            if (string.IsNullOrEmpty(cmbNomineeRelationship.Text))
            {
                validationStatus = false;
                cmbNomineeRelationship.Focus();
                errValidator.SetError(this.cmbNomineeRelationship, cmbNomineeRelationship.Tag?.ToString() ?? "Nominee Relationship is required.");
            }
            if (string.IsNullOrEmpty(txtNomineeContactNumber.Text))
            {
                validationStatus = false;
                txtNomineeContactNumber.Focus();
                errValidator.SetError(this.txtNomineeContactNumber, "Enter a valid contact number (10-15 digits).");
            }
            if (string.IsNullOrEmpty(txtPassportNumber.Text))
            {
                validationStatus = false;
                txtPassportNumber.Focus();
                errValidator.SetError(this.txtPassportNumber, "Enter a valid Employee Passport Number.");
            }
            if (string.IsNullOrEmpty(txtAadhaarCardNumber.Text))
            {
                validationStatus = false;
                txtPassportNumber.Focus();
                errValidator.SetError(this.txtAadhaarCardNumber, "Enter a valid Employee Aadhaar Card Number.");
            }
            if (string.IsNullOrEmpty(txtVoterCardNumber.Text))
            {
                validationStatus = false;
                txtPassportNumber.Focus();
                errValidator.SetError(this.txtVoterCardNumber, "Enter a valid Voter ID Number.");
            }
            if (string.IsNullOrEmpty(txtPANCardNumber.Text))
            {
                validationStatus = false;
                txtPassportNumber.Focus();
                errValidator.SetError(this.txtPANCardNumber, "Enter a valid Employee PAN Number.");
            }
            if (string.IsNullOrEmpty(txtAdditonalCardNumber.Text))
            {
                validationStatus = false;
                txtPassportNumber.Focus();
                errValidator.SetError(this.txtAdditonalCardNumber, "Enter a additional Card Number.");
            }

            //EmpWorkExpID = objEmpPersonalFamilyMemberInfo.InsertEmployeePersonalFamilyMemberInfo(Convert.ToInt16(dc.Cells["EmpPerFamInfoID"].Value.ToString()), Convert.ToInt16(dc.Cells["PersonalInfoID"].Value.ToString()), dc.Cells["FamMemName"].Value.ToString(), Convert.ToDateTime(dc.Cells["FamMemDOB"].Value), Convert.ToInt16(dc.Cells["FamMemAge"].Value.ToString()), dc.Cells["FamMemRelationship"].Value.ToString(), dc.Cells["FamMemAddr1"].Value.ToString(), dc.Cells["FamMemAddr2"].Value.ToString(), dc.Cells["FamMemArea"].Value.ToString(), dc.Cells["FamMemCity"].Value.ToString(), dc.Cells["FamMemState"].Value.ToString(), dc.Cells["FamMemPIN"].Value.ToString(), dc.Cells["FamMemCountry"].Value.ToString(), dc.Cells["FamMemContactNumber"].Value.ToString(), dc.Cells["FamMemMailID"].Value.ToString(), dc.Cells["FamMemBloodGroup"].Value.ToString(), Convert.ToBoolean(dc.Cells["FamMemInsuranceEnrolled"].Value.ToString()));

            //foreach (DataGridViewRow dc in dtgFamilyMemberInforamtion.Rows)
            //{
            //    if (dc.IsNewRow)
            //        continue;

            //    // Read values
            //    string MemberName = Convert.ToString(dc.Cells["FamMemName"].Value)?.Trim();
            //    string MemberDOB = Convert.ToString(dc.Cells["FamMemDOB"].Value)?.Trim();

            //    bool isNameEmpty = string.IsNullOrWhiteSpace(MemberName);
            //    bool isDobEmpty = string.IsNullOrWhiteSpace(MemberDOB);

            //    if (isNameEmpty && isDobEmpty)
            //        continue;

            //    if (!isNameEmpty && isDobEmpty)
            //    {
            //        validationStatus = false;
            //        errValidator.SetError(dtgFamilyMemberInforamtion, "Family Members Information is incomplete. Please fill in all the necessary details.");
            //        break;
            //    }
            //}

            // Validate Previous Work Experience Grid
            foreach (DataGridViewRow row in dtgPreviousWorkExp.Rows)
            {
                // Skip new row placeholder
                if (row.IsNewRow) continue;

                object startObj = row.Cells["StartDate"].Value;
                object endObj = row.Cells["EndDate"].Value;
                DateTime startDate, endDate;

                // Check for empty or invalid Start Date
                if (startObj == null || !DateTime.TryParse(startObj.ToString(), out startDate))
                {
                    validationStatus = false;
                    dtgPreviousWorkExp.CurrentCell = row.Cells["StartDate"];
                    errValidator.SetError(dtgPreviousWorkExp, "Please enter a valid Start Date for all work experience rows.");
                    break;
                }

                // Check for empty or invalid End Date
                if (endObj == null || !DateTime.TryParse(endObj.ToString(), out endDate))
                {
                    validationStatus = false;
                    dtgPreviousWorkExp.CurrentCell = row.Cells["EndDate"];
                    errValidator.SetError(dtgPreviousWorkExp, "Please enter a valid End Date for all work experience rows.");
                    break;
                }

                // Start Date should not be after End Date
                if (startDate > endDate)
                {
                    validationStatus = false;
                    dtgPreviousWorkExp.CurrentCell = row.Cells["StartDate"];
                    errValidator.SetError(dtgPreviousWorkExp, "Start Date cannot be after End Date in work experience.");
                    break;
                }

                // End Date should not be in the future
                if (endDate > DateTime.Now.Date)
                {
                    validationStatus = false;
                    dtgPreviousWorkExp.CurrentCell = row.Cells["EndDate"];
                    errValidator.SetError(dtgPreviousWorkExp, "End Date cannot be in the future in work experience.");
                    break;
                }
            }

            if (chkEduQualList.CheckedItems.Count == 0)
            {
                validationStatus = false;
                chkEduQualList.Focus();
                errValidator.SetError(chkEduQualList, "Please select at least one Education Qualification.");
            }

            if (chkSkillsList.CheckedItems.Count == 0)
            {
                validationStatus = false;
                chkSkillsList.Focus();
                errValidator.SetError(chkSkillsList, "Please select at least one Skill.");
            }

            if (lstLDocumentsList.Items.Count == 0)
            {
                validationStatus = false;
                lstLDocumentsList.Focus();
                errValidator.SetError(lstLDocumentsList, "Please reference at least one document.");
            }

            if (lblActionMode.Text.ToString() == "add")
            {
                if (string.IsNullOrEmpty(txtTotalLeaveAllotment.Text))
                {
                    validationStatus = false;
                    txtTotalLeaveAllotment.Focus();
                    errValidator.SetError(this.txtTotalLeaveAllotment, "Please enter Total Leaves Alloted.");
                }
                if (string.IsNullOrEmpty(txtTotalBalanceLeaves.Text))
                {
                    validationStatus = false;
                    txtTotalBalanceLeaves.Focus();
                    errValidator.SetError(this.txtTotalBalanceLeaves, "Please enter Balance Leaves available.");
                }
            }

            if (txtBankAccountNumber.Text.ToString().Trim() == "")
            {
                validationStatus = false;
                txtBankAccountNumber.Focus();
                errValidator.SetError(txtBankAccountNumber, "Bank Account Number is required.");
            }

            if (lstBankList.SelectedItems.Count == 0)
            {
                validationStatus = false;
                lstBankList.Focus();
                errValidator.SetError(lstBankList, "Please select Bank Address.");
            }

            if (string.IsNullOrEmpty(cmbEmpTaxScheme.Text))
            {
                validationStatus = false;
                cmbEmpTaxScheme.Focus();
                errValidator.SetError(this.cmbEmpTaxScheme, cmbEmpTaxScheme.Tag?.ToString() ?? "Active Tax Scheme is required.");
            }


            return validationStatus;

        }

        private void btnReportingManagerSearch_Click(object sender, EventArgs e)
        {
            frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listRepManagers");
            frmEmployeeList.ShowDialog();
        }

        public void SelectedEmployeeID(string SearchOptionSelectedForm, int selectedEmployeeID)
        {
            if (SearchOptionSelectedForm == "listSSEmployees")
            {
                lblEmpID.Text = selectedEmployeeID.ToString();
                btnViewCalender.Visible = true;
                UpdateUIWithSelectedEmployeeDetails(Convert.ToInt16(lblEmpID.Text));
            }
            else if (SearchOptionSelectedForm == "listRepManagers")
            {
                lblReportingManagerID.Text = selectedEmployeeID.ToString();
                ReportingManagerInfo objReportingManagerInfo = objEmployeeMaster.GetReportingManagerInfo(Convert.ToInt16(selectedEmployeeID.ToString()));
                txtRepEmpCode.Text = objReportingManagerInfo.EmpCode;
                txtRepEmpName.Text = objReportingManagerInfo.EmpName;
                txtRepEmpDesig.Text = objReportingManagerInfo.DesignationTitle;
                txtRepEmpDepartment.Text = objReportingManagerInfo.DepartmentTitle;
                txtRepEmpContactNumber.Text = objReportingManagerInfo.ContactNumber1;
                picRepEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(lblReportingManagerID.Text.ToString())).EmpPhoto);
            }
        }

        public void UpdateUIWithSelectedEmployeeDetails(int EmployeeID)
        {
            this.Cursor = Cursors.WaitCursor;

            lblEmpID.Text = EmployeeID.ToString();
            EmployeeInfo objSelectedEmployeeInfo = objEmployeeMaster.GetSelectedEmployeeInfo(EmployeeID);
            txtEmpCode.Text = objSelectedEmployeeInfo.EmpCode;
            txtEmployeeName.Text = objSelectedEmployeeInfo.EmpName;
            cmbDesignation.SelectedIndex = objSelectedEmployeeInfo.EmpDesignationID - 1;
            cmbDepartment.SelectedIndex = objSelectedEmployeeInfo.DepartmentID - 1;
            cmbBloodGroup.SelectedIndex = objSelectedEmployeeInfo.BloodGroupID - 1;

            EmpTypeInfo objSelectedEmploymentTypeInfo = objEmploymentTypeInfo.getEmployeeSpecificEmploymentTypeInfo(EmployeeID);
            cmbEmploymentType.SelectedIndex = objSelectedEmploymentTypeInfo.EmpTypeMasID - 1;
            if (cmbEmploymentType.SelectedIndex < 0)
                cmbEmploymentType.SelectedIndex = 0;

            txtEmploymentEffectiveFrom.Text = objSelectedEmploymentTypeInfo.EffectiveDate.ToString("dd-MM-yyyy");

            EmpShiftInfo objEmpShiftInfo = objShiftMas.getEmployeeSpecificShiftInfo(EmployeeID);
            cmbShift.SelectedIndex = objEmpShiftInfo.ShiftID - 1;
            if (cmbShift.SelectedIndex < 0)
                cmbShift.SelectedIndex = 0;

            txtShiftEffectiveFrom.Text = objEmpShiftInfo.EffectiveDate.ToString("dd-MM-yyyy");

            EmpPersonalPersonalInfo objSelectedPersonalInfo = objEmployeePersonalInfo.GetEmpPersonalPersonalInfo(Convert.ToInt16(lblEmpID.Text));
            txtDateOfBirth.Text = objSelectedPersonalInfo.DOB.ToString("dd-MM-yyyy");
            txtDateOfJoining.Text = objSelectedPersonalInfo.DOJ.ToString("dd-MM-yyyy");
            txtEmployeeContactNumber.Text = objSelectedPersonalInfo.ContactNumber1;
            txtEmployeeMailID.Text = objSelectedPersonalInfo.ContactNumber2;
            cmbGender.SelectedIndex = objSelectedPersonalInfo.SexID - 1;

            AddressInfo objSelectedCurrAddressInfo = objAddressInfo.GetAddressInfo(Convert.ToInt16(objSelectedPersonalInfo.CurAddressID));
            lblCurrentAddressID.Text = objSelectedCurrAddressInfo.AddressID.ToString();
            txtCurrentAddress01.Text = objSelectedCurrAddressInfo.Address1;
            txtCurrentAddress02.Text = objSelectedCurrAddressInfo.Address2;
            txtCurrentArea.Text = objSelectedCurrAddressInfo.Area;
            txtCurrentCity.Text = objSelectedCurrAddressInfo.City;
            txtCurrentState.Text = objSelectedCurrAddressInfo.State;
            txtCurrentPIN.Text = objSelectedCurrAddressInfo.PIN;
            cmbCurrentCountry.SelectedIndex = objSelectedCurrAddressInfo.CountryID - 1;

            AddressInfo objSelectedPermAddressInfo = objAddressInfo.GetAddressInfo(Convert.ToInt16(objSelectedPersonalInfo.PerAddressID));
            lblPermanentAddressID.Text = objSelectedPermAddressInfo.AddressID.ToString();
            txtPermanentAddress01.Text = objSelectedPermAddressInfo.Address1;
            txtPermanentAddress02.Text = objSelectedPermAddressInfo.Address2;
            txtPermanentArea.Text = objSelectedPermAddressInfo.Area;
            txtPermanentCity.Text = objSelectedPermAddressInfo.City;
            txtPermanentState.Text = objSelectedPermAddressInfo.State;
            txtPermanentPIN.Text = objSelectedPermAddressInfo.PIN;
            cmbPermanentCountry.SelectedIndex = objSelectedPermAddressInfo.CountryID - 1;

            txtCurrentAddress01.Focus();
            txtEmployeeName.Focus();

            picEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(EmployeeID).EmpPhoto);

            lblReportingManagerID.Text = objSelectedEmployeeInfo.EmpRepManID.ToString();
            ReportingManagerInfo objReportingManagerInfo = objEmployeeMaster.GetReportingManagerInfo(Convert.ToInt16(lblReportingManagerID.Text));
            txtRepEmpCode.Text = objReportingManagerInfo.EmpCode;
            txtRepEmpName.Text = objReportingManagerInfo.EmpName;
            txtRepEmpDesig.Text = objReportingManagerInfo.DesignationTitle;
            txtRepEmpDepartment.Text = objReportingManagerInfo.DepartmentTitle;
            txtRepEmpContactNumber.Text = objReportingManagerInfo.ContactNumber1;
            picRepEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(lblReportingManagerID.Text)).EmpPhoto);

            ContactInfo objSelectedContactInfo = objContactPerson.GetContactInfo(objSelectedPersonalInfo.ContactID1);
            lblContactInfoID.Text = objSelectedContactInfo.ContactID.ToString();
            txtContactPersonName.Text = objSelectedContactInfo.ContactPerson;
            cmbContactPersonRelationship.SelectedIndex = objSelectedContactInfo.SexID - 1;
            txtContactPersonNumber.Text = objSelectedContactInfo.ContactPersonAddressInfo;

            NomineeInfo objSelectedNomineeInfo = objNomineeInfo.GetNomineeInfo(EmployeeID);
            lblNomineeID.Text = objSelectedNomineeInfo.NomineeID.ToString();
            txtNomineeName.Text = objSelectedNomineeInfo.NomineePerson;
            cmbNomineeRelationship.SelectedIndex = objSelectedNomineeInfo.RelationshipID - 1;
            txtNomineeContactNumber.Text = objSelectedNomineeInfo.ContactNumber;

            lblEmpGovtID.Text = objSelectedPersonalInfo.PersonalInfoID.ToString();
            EmpPersonalIDInfo objEmpPersonalIDInfo = objEmployeePersonalIDInfo.GetEmpPersonalIDInfo(Convert.ToInt16(lblEmpGovtID.Text.Trim()));
            txtPassportNumber.Text = objEmpPersonalIDInfo.PassportNumber.ToString();
            txtPassportIssueDate.Text = objEmpPersonalIDInfo.IssueDate.ToString("dd-MM-yyyy");
            txtPassportRenewalDate.Text = objEmpPersonalIDInfo.RenewalDate.ToString("dd-MM-yyyy");
            txtAadhaarCardNumber.Text = objEmpPersonalIDInfo.AadhaarCardNumber.ToString();
            txtVoterCardNumber.Text = objEmpPersonalIDInfo.VoterCardNumber.ToString();
            txtPANCardNumber.Text = objEmpPersonalIDInfo.PANNumber.ToString();
            txtAdditonalCardNumber.Text = objEmpPersonalIDInfo.ID1.ToString();

            RefreshFamilyMembersInformation();

            chkEduQualList.ColumnWidth = 300;
            ((ListBox)chkEduQualList).DataSource = objEduQualMas.GetEduQualMasList();

            string DataTableToJSon = "";
            DataTableToJSon = JsonConvert.SerializeObject(objEmpEduQualInfo.GetEduQualListInfo(EmployeeID));
            List<EmpEduQualInfo> empEduQualInfoList = JsonConvert.DeserializeObject<List<EmpEduQualInfo>>(DataTableToJSon);
            foreach (var indEduQual in empEduQualInfoList)
            {
                chkEduQualList.SetItemChecked(indEduQual.EduQualID - 1, true);
            }

            chkSkillsList.ColumnWidth = 300;
            ((ListBox)chkSkillsList).DataSource = objSkills.GetSkillList();

            DataTableToJSon = JsonConvert.SerializeObject(objEmpSkillMas.GetSkillsListInfo(EmployeeID));
            List<SkillsInfo> empSkillsInfoList = JsonConvert.DeserializeObject<List<SkillsInfo>>(DataTableToJSon);
            foreach (var indSkill in empSkillsInfoList)
            {
                chkSkillsList.SetItemChecked(indSkill.SkillID - 1, true);
            }

            dtgPreviousWorkExp.DataSource = objEmpWorkExperienceInfo.GetWorkExpListInfo(EmployeeID);
            dtgPreviousWorkExp.Columns["LastCompID"].Visible = false;
            dtgPreviousWorkExp.Columns["LastCompanyInfoID"].Visible = false;
            dtgPreviousWorkExp.Columns["EmpID"].Visible = false;
            dtgPreviousWorkExp.Columns["LastCompanyTitle"].Width = 200;
            dtgPreviousWorkExp.Columns["Address"].Width = 350;
            dtgPreviousWorkExp.Columns["StartDate"].Width = 100;
            dtgPreviousWorkExp.Columns["StartDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgPreviousWorkExp.Columns["EndDate"].Width = 100;
            dtgPreviousWorkExp.Columns["EndDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgPreviousWorkExp.Columns["Comments"].Width = 350;

            cmbSalProfile.SelectedIndex = objSalaryProfile.getEmployeeSpecificSalaryProfile(EmployeeID).SalProfileID - 1;
            if (cmbSalProfile.SelectedIndex < 0)
                cmbSalProfile.SelectedIndex = 0;

            EmpTaxSchemeModel objEmpTaxSchemeInfo = objTaxMas.getEmployeeSpecificTaxSchemeModel(EmployeeID);
            cmbEmpTaxScheme.SelectedIndex = objEmpTaxSchemeInfo.TaxSchemeID - 1;
            if (cmbEmpTaxScheme.SelectedIndex < 0)
                cmbEmpTaxScheme.SelectedIndex = 0;

            txtTaxSchemeEffectiveFrom.Text = objEmpTaxSchemeInfo.EffectiveDate.ToString("dd-MM-yyyy");

            List<EmployeeWklyOffInfo> objWeeklyOff = objWeeklyOffInfo.getEmployeeSpecificWeeklyOffMasterInfo(EmployeeID);
            lblEmployeeWeeklyOffID.Text = objWeeklyOff.OrderByDescending(x => x.EffectDateFrom).FirstOrDefault().WeeklyOffID.ToString();
            cmbWeeklyOff.SelectedIndex = objWeeklyOff.OrderByDescending(x => x.EffectDateFrom).FirstOrDefault().WklyOffMasID - 1;
            if (cmbWeeklyOff.SelectedIndex < 0)
                cmbWeeklyOff.SelectedIndex = 0;

            RefreshLeavesHistoryList();
            RefreshUploadedDocumentsList();
            RefreshBankList();

            SalaryProfileInfo();

            this.Cursor = Cursors.Default;
        }

        public void UpdateLastCompanyInfo(EmpWorkExpInfo objEmpWorkExpInfo)
        {
            dtgPreviousWorkExp.Rows.Add(objEmpWorkExpInfo);
        }

        private void btnEmpPhotoUpload_Click(object sender, EventArgs e)
        {
            if (ofdImage.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //txtEmpPhoto.Text = @ofdImage.FileName;
                    txtEmpPhoto.Text = "overwrite";
                    picEmpPhoto.Image = Image.FromFile(@ofdImage.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnUploadDocument_Click(object sender, EventArgs e)
        {
            if (lblEmpID.Text.Trim() == "")
                return;

            if (ofdDocuments.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string selectedFilePath = (@ofdDocuments.FileName).ToLower();
                    string startupFilePath = (Application.StartupPath + @"\uploadedfiles\" + txtEmpCode.Text + "-" + Path.GetFileName(selectedFilePath)).ToLower();
                    string shortPath = (Application.StartupPath.Substring(Application.StartupPath.IndexOf(@"\bin")) + @"\uploadedfiles\" + txtEmpCode.Text + "-" + Path.GetFileName(selectedFilePath)).ToLower();
                    File.Copy(selectedFilePath, startupFilePath, true);

                    int uploadDocumentID = objUploadDocument.InsertDocumentUpload((txtEmpCode.Text + "-" + Path.GetFileName(selectedFilePath)).ToLower(), (Path.GetExtension(selectedFilePath)).ToLower(), DateTime.Now, shortPath, false);
                    EmployeeDocumentInfo employeeDocumentInfo = objUploadDocument.getSpecificDocumentInfo(uploadDocumentID);
                    System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
                        employeeDocumentInfo.DocID.ToString(),
                        employeeDocumentInfo.DocCode.ToString(),
                        employeeDocumentInfo.DocName.ToString(),
                        employeeDocumentInfo.DocType.ToString(),
                        employeeDocumentInfo.DocUploadDate.ToString("dd-MMM-yyyy"),
                        employeeDocumentInfo.DocPath.ToString()
                    });
                    lstLDocumentsList.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem1 });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void picRefreshLeaveTRList_Click(object sender, EventArgs e)
        {
            RefreshLeavesHistoryList();
        }

        private void picDownloadLeaveTRList_Click(object sender, EventArgs e)
        {
            //Download.DownloadExcel(lstLeaveTRList);
        }

        private void RefreshFamilyMembersInformation()
        {
            if(lblEmpID.Text.Trim() == "")
                lblEmpGovtID.Text = "0";
            dtgFamilyMemberInforamtion.DataSource = objEmpPersonalFamilyMemberInfo.GetEmpPersonalFamilyMemberInfo(Convert.ToInt16(lblEmpGovtID.Text.Trim()));
            dtgFamilyMemberInforamtion.Columns["EmpPerFamInfoID"].Visible = false;
            dtgFamilyMemberInforamtion.Columns["EmpPerFamInfoID"].ReadOnly = true;
            dtgFamilyMemberInforamtion.Columns["EmpPerFamInfoID"].Width = 80;
            dtgFamilyMemberInforamtion.Columns["PersonalInfoID"].Visible = false;
            dtgFamilyMemberInforamtion.Columns["PersonalInfoID"].Width = 80;
            dtgFamilyMemberInforamtion.Columns["PersonalInfoID"].ReadOnly = true;
            dtgFamilyMemberInforamtion.Columns["FamMemName"].Visible = true;
            dtgFamilyMemberInforamtion.Columns["FamMemName"].HeaderText = "Member Name";
            dtgFamilyMemberInforamtion.Columns["FamMemName"].Width = 250;
            dtgFamilyMemberInforamtion.Columns["FamMemName"].ReadOnly = true;
            dtgFamilyMemberInforamtion.Columns["FamMemDOB"].HeaderText = "DOB";
            dtgFamilyMemberInforamtion.Columns["FamMemDOB"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgFamilyMemberInforamtion.Columns["FamMemDOB"].Width = 100;
            dtgFamilyMemberInforamtion.Columns["FamMemDOB"].ReadOnly = true;
            dtgFamilyMemberInforamtion.Columns["FamMemAge"].HeaderText = "Age";
            dtgFamilyMemberInforamtion.Columns["FamMemAge"].Visible = true;
            dtgFamilyMemberInforamtion.Columns["FamMemAge"].Width = 75;
            dtgFamilyMemberInforamtion.Columns["FamMemAge"].ReadOnly = true;
            dtgFamilyMemberInforamtion.Columns["FamMemRelationship"].HeaderText = "Relationship";
            dtgFamilyMemberInforamtion.Columns["FamMemRelationship"].Visible = true;
            dtgFamilyMemberInforamtion.Columns["FamMemRelationship"].Width = 100;
            dtgFamilyMemberInforamtion.Columns["FamMemRelationship"].ReadOnly = true;
            dtgFamilyMemberInforamtion.Columns["FamMemAddr1"].HeaderText = "Address 1";
            dtgFamilyMemberInforamtion.Columns["FamMemAddr1"].Visible = true;
            dtgFamilyMemberInforamtion.Columns["FamMemAddr1"].Width = 250;
            dtgFamilyMemberInforamtion.Columns["FamMemAddr1"].ReadOnly = true;
            dtgFamilyMemberInforamtion.Columns["FamMemAddr2"].HeaderText = "Address 2";
            dtgFamilyMemberInforamtion.Columns["FamMemAddr2"].Visible = true;
            dtgFamilyMemberInforamtion.Columns["FamMemAddr2"].Width = 250;
            dtgFamilyMemberInforamtion.Columns["FamMemAddr2"].ReadOnly = true;
            dtgFamilyMemberInforamtion.Columns["FamMemArea"].HeaderText = "Area";
            dtgFamilyMemberInforamtion.Columns["FamMemArea"].Visible = true;
            dtgFamilyMemberInforamtion.Columns["FamMemArea"].Width = 250;
            dtgFamilyMemberInforamtion.Columns["FamMemArea"].ReadOnly = true;
            dtgFamilyMemberInforamtion.Columns["FamMemCity"].HeaderText = "City";
            dtgFamilyMemberInforamtion.Columns["FamMemCity"].Visible = true;
            dtgFamilyMemberInforamtion.Columns["FamMemCity"].Width = 250;
            dtgFamilyMemberInforamtion.Columns["FamMemCity"].ReadOnly = true;
            dtgFamilyMemberInforamtion.Columns["FamMemState"].HeaderText = "City";
            dtgFamilyMemberInforamtion.Columns["FamMemState"].Visible = true;
            dtgFamilyMemberInforamtion.Columns["FamMemState"].Width = 250;
            dtgFamilyMemberInforamtion.Columns["FamMemState"].ReadOnly = true;
            dtgFamilyMemberInforamtion.Columns["FamMemPIN"].HeaderText = "PIN";
            dtgFamilyMemberInforamtion.Columns["FamMemPIN"].Visible = true;
            dtgFamilyMemberInforamtion.Columns["FamMemPIN"].Width = 100;
            dtgFamilyMemberInforamtion.Columns["FamMemPIN"].ReadOnly = true;
            dtgFamilyMemberInforamtion.Columns["FamMemCountry"].HeaderText = "Country";
            dtgFamilyMemberInforamtion.Columns["FamMemCountry"].Visible = true;
            dtgFamilyMemberInforamtion.Columns["FamMemCountry"].Width = 250;
            dtgFamilyMemberInforamtion.Columns["FamMemCountry"].ReadOnly = true;
            dtgFamilyMemberInforamtion.Columns["FamMemContactNumber"].HeaderText = "Contact Number";
            dtgFamilyMemberInforamtion.Columns["FamMemContactNumber"].Visible = true;
            dtgFamilyMemberInforamtion.Columns["FamMemContactNumber"].Width = 250;
            dtgFamilyMemberInforamtion.Columns["FamMemContactNumber"].ReadOnly = true;
            dtgFamilyMemberInforamtion.Columns["FamMemMailID"].HeaderText = "Mail ID";
            dtgFamilyMemberInforamtion.Columns["FamMemMailID"].Visible = true;
            dtgFamilyMemberInforamtion.Columns["FamMemMailID"].Width = 250;
            dtgFamilyMemberInforamtion.Columns["FamMemMailID"].ReadOnly = true;
            dtgFamilyMemberInforamtion.Columns["FamMemBloodGroup"].HeaderText = "Blood Group";
            dtgFamilyMemberInforamtion.Columns["FamMemBloodGroup"].Visible = true;
            dtgFamilyMemberInforamtion.Columns["FamMemBloodGroup"].Width = 250;
            dtgFamilyMemberInforamtion.Columns["FamMemBloodGroup"].ReadOnly = true;
            dtgFamilyMemberInforamtion.Columns["FamMemInsuranceEnrolled"].HeaderText = "Insurance Enrolled";
            dtgFamilyMemberInforamtion.Columns["FamMemInsuranceEnrolled"].Width = 150;
            dtgFamilyMemberInforamtion.Columns["FamMemInsuranceEnrolled"].ReadOnly = true;
        }

        private void RefreshLeavesHistoryList()
        {
            if (lblEmpID.Text.Trim() == "")
                return;

            lblLeaveMasID.Text = objLeaveTRList.getMaxLeaveMasID(Convert.ToInt16(lblEmpID.Text)).ToString();
            txtTotalLeaveAllotment.Text = objLeaveTRList.getBalanceLeave(Convert.ToInt16(lblEmpID.Text)).ToString();
            txtBalanceLeaveAllotment.Text = txtTotalLeaveAllotment.Text;
            txtTotalUtilised.Text = "0.00";

            dtgLeaveEntitlement.DataSource = null;
            if (lblActionMode.Text == "add")
                dtgLeaveEntitlement.DataSource = objEmpLeaveEntitlementInfo.getDefaultLeaveEntitilementList();
            else if (lblActionMode.Text == "modify")
                dtgLeaveEntitlement.DataSource = objEmpLeaveEntitlementInfo.getEmployeeLeaveEntitilementList(Convert.ToInt16(lblEmpID.Text), Convert.ToInt16(lblLeaveMasID.Text));

            dtgLeaveEntitlement.Columns["LeaveEntmtID"].Visible = false;
            dtgLeaveEntitlement.Columns["EmpID"].Visible = false;
            dtgLeaveEntitlement.Columns["EmpID"].ReadOnly = true;
            dtgLeaveEntitlement.Columns["LeaveMasID"].Visible = false;
            dtgLeaveEntitlement.Columns["LeaveMasID"].ReadOnly = true;
            dtgLeaveEntitlement.Columns["LeaveTypeID"].Visible = false;
            dtgLeaveEntitlement.Columns["LeaveTypeID"].ReadOnly = true;
            dtgLeaveEntitlement.Columns["LeaveTypeTitle"].ReadOnly = true;
            dtgLeaveEntitlement.Columns["LeaveTypeTitle"].Width = 350;

            dtgLeaveEntitlement.Columns["TotalLeaves"].Width = 135;
            dtgLeaveEntitlement.Columns["TotalLeaves"].ReadOnly = true;
            dtgLeaveEntitlement.Columns["TotalLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgLeaveEntitlement.Columns["TotalLeaves"].DefaultCellStyle.Format = "0.00";

            dtgLeaveEntitlement.Columns["BalanceLeaves"].Width = 135;
            dtgLeaveEntitlement.Columns["BalanceLeaves"].ReadOnly = true;
            dtgLeaveEntitlement.Columns["BalanceLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgLeaveEntitlement.Columns["BalanceLeaves"].DefaultCellStyle.Format = "0.00";

            dtgLeaveEntitlement.Columns["UsedLeaves"].Width = 135;
            dtgLeaveEntitlement.Columns["UsedLeaves"].ReadOnly = true;
            dtgLeaveEntitlement.Columns["UsedLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgLeaveEntitlement.Columns["UsedLeaves"].DefaultCellStyle.Format = "0.00";
            dtgLeaveEntitlement.Columns["OrderID"].Visible = false;

            decimal totalLeavesAllotted = 0;
            decimal totalBalanceLeaves = 0;
            foreach (DataGridViewRow dc in dtgLeaveEntitlement.Rows)
            {
                totalLeavesAllotted = totalLeavesAllotted + Convert.ToDecimal(dc.Cells["TotalLeaves"].Value.ToString());
                totalBalanceLeaves = totalBalanceLeaves + Convert.ToDecimal(dc.Cells["BalanceLeaves"].Value.ToString());

                if (Convert.ToDecimal(dc.Cells["BalanceLeaves"].Value.ToString()) < Convert.ToDecimal(dc.Cells["TotalLeaves"].Value.ToString()))
                {
                    dc.Cells["BalanceLeaves"].Style.BackColor = Color.LightPink;
                }
            }

            txtTotalLeavesAlloted.Text = Convert.ToDecimal(totalLeavesAllotted.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtTotalBalanceLeaves.Text = Convert.ToDecimal(totalBalanceLeaves.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtTotalUtilised.Text = (Convert.ToDecimal(txtTotalLeavesAlloted.Text.ToString()) - Convert.ToDecimal(txtTotalBalanceLeaves.Text.ToString())).ToString("0.00", CultureInfo.InvariantCulture);

            //lstLeaveTRList.Items.Clear();
            //List<EmployeeLeaveTRList> objEmployeeLeaveTRList = objLeaveTRList.getEmployeeLeaveTRList(Convert.ToInt16(lblEmpID.Text));
            //foreach (EmployeeLeaveTRList indEmployeeLeaveTRList in objEmployeeLeaveTRList)
            //{
            //    System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            //        indEmployeeLeaveTRList.LeaveTRID.ToString(),
            //        indEmployeeLeaveTRList.LeaveTypeTitle != null ? indEmployeeLeaveTRList.LeaveTypeTitle.ToString() : "",
            //        indEmployeeLeaveTRList.ActualLeaveDateFrom != null ? Convert.ToDateTime(indEmployeeLeaveTRList.ActualLeaveDateFrom.ToString()).ToString("dd-MMM-yyyy") : "",
            //        indEmployeeLeaveTRList.ActualLeaveDateTo != null ? Convert.ToDateTime(indEmployeeLeaveTRList.ActualLeaveDateTo.ToString()).ToString("dd-MMM-yyyy") : "",
            //        indEmployeeLeaveTRList.LeaveDuration != null ? indEmployeeLeaveTRList.LeaveDuration.ToString() : "0.00",
            //        indEmployeeLeaveTRList.LeaveComments.ToString()
            //    });
            //    lstLeaveTRList.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem1 });
            //}
            //txtTotalLeaveAllotment.Text = objLeaveTRList.getBalanceLeave(Convert.ToInt16(lblEmpID.Text)).ToString();
            //txtBalanceLeaveAllotment.Text = txtTotalLeaveAllotment.Text;
        }

        private void RefreshUploadedDocumentsList()
        {
            if (lblEmpID.Text.Trim() == "")
                return;

            lstLDocumentsList.Items.Clear();
            List<EmployeeDocumentInfo> objEmployeeLeaveTRList = objUploadedDocuments.getEmployeeSpecificDocumentsList(Convert.ToInt16(lblEmpID.Text));
            foreach (EmployeeDocumentInfo indEmployeeDocumentInfo in objEmployeeLeaveTRList)
            {
                System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
                    indEmployeeDocumentInfo.DocID.ToString(),
                    indEmployeeDocumentInfo.DocCode != null ? indEmployeeDocumentInfo.DocCode.ToString() : "",
                    indEmployeeDocumentInfo.DocName != null ? indEmployeeDocumentInfo.DocName.ToString() : "",
                    indEmployeeDocumentInfo.DocType != null ? indEmployeeDocumentInfo.DocType.ToString() : "",
                    indEmployeeDocumentInfo.DocUploadDate != null ? Convert.ToDateTime(indEmployeeDocumentInfo.DocUploadDate.ToString()).ToString("dd-MMM-yyyy") : "",
                    indEmployeeDocumentInfo.DocPath != null ? indEmployeeDocumentInfo.DocPath.ToString() : ""
                });
                lstLDocumentsList.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem1 });
            }
        }

        private void RefreshBankList()
        {
            if (lblEmpID.Text.Trim() == "")
                return;

            lstBankList.Items.Clear();
            List<BankDetailsInfo> objBankDetailsInfoList = objBankInfo.GetFullBanksList();
            foreach (BankDetailsInfo indBankInfo in objBankDetailsInfoList)
            {
                System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
                    indBankInfo.BankID.ToString(),
                    indBankInfo.BankCode != null ? indBankInfo.BankCode.ToString() : "",
                    indBankInfo.BankName != null ? indBankInfo.BankName.ToString() : "",
                    indBankInfo.BankAddress != null ? indBankInfo.BankAddress.ToString() : "",
                    indBankInfo.IFSCCode != null ? indBankInfo.IFSCCode.ToString() : ""
                });
                lstBankList.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem1 });
            }

            EmployeeBankInfo objEmpSpecificBankInfo = objBankInfo.GetEmployeeSpecificBankInfo(Convert.ToInt16(lblEmpID.Text));
            if (objEmpSpecificBankInfo.EmpBankID == 0)
                return;

            lblBankID.Text = objEmpSpecificBankInfo.BankID.ToString();
            txtBankAccountNumber.Text = objEmpSpecificBankInfo.EmpACNumber.ToString();
            lstBankList.Items[Convert.ToInt16(objEmpSpecificBankInfo.BankID.ToString()) - 1].Selected = true;
        }

        private void SalaryProfileInfo()
        {
            if (lblActionMode.Text.ToLower() == "add")
                dtgSalaryProfileDetails.DataSource = objSalaryProfile.GetDefaultSalaryProfileInfo(cmbSalProfile.SelectedIndex + 1);
            else if (lblActionMode.Text.ToLower() == "modify")
                dtgSalaryProfileDetails.DataSource = objSalaryProfile.GetEmployeeSpecificSalaryProfileInfo(Convert.ToInt16(lblEmpID.Text));

            dtgSalaryProfileDetails.Columns["EmpSalDetID"].Visible = false;
            dtgSalaryProfileDetails.Columns["SalProDetID"].Visible = false;
            dtgSalaryProfileDetails.Columns["SalProfileID"].Visible = false;
            dtgSalaryProfileDetails.Columns["HeaderID"].Visible = false;
            dtgSalaryProfileDetails.Columns["HeaderTitle"].Width = 350;
            dtgSalaryProfileDetails.Columns["HeaderType"].Width = 150;
            dtgSalaryProfileDetails.Columns["CalcFormula"].Visible = false;
            dtgSalaryProfileDetails.Columns["IsFixed"].Visible = false;
            dtgSalaryProfileDetails.Columns["AllowanceAmount"].Width = 150;
            dtgSalaryProfileDetails.Columns["AllowanceAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgSalaryProfileDetails.Columns["AllowanceAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryProfileDetails.Columns["DeductionAmount"].Width = 150;
            dtgSalaryProfileDetails.Columns["DeductionAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgSalaryProfileDetails.Columns["DeductionAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryProfileDetails.Columns["ReimbursmentAmount"].Width = 150;
            dtgSalaryProfileDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgSalaryProfileDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryProfileDetails.Columns["OrderID"].Visible = false;
            dtgSalaryProfileDetails.Columns["SalProAmount"].Visible = false;

            decimal totalAallowences = 0;
            decimal totalDeductions = 0;
            decimal totalReimbursement = 0;

            foreach (DataGridViewRow dc in dtgSalaryProfileDetails.Rows)
            {
                totalAallowences = totalAallowences + Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString());
                totalDeductions = totalDeductions + Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString());
                totalReimbursement = totalReimbursement + Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString());

                if (dc.Cells["CalcFormula"].Value != null)
                    dc.Cells["HeaderTitle"].ToolTipText = dc.Cells["CalcFormula"].Value.ToString();

                dc.Cells["EmpSalDetID"].ReadOnly = true;
                dc.Cells["SalProDetID"].ReadOnly = true;
                dc.Cells["SalProfileID"].ReadOnly = true;
                dc.Cells["HeaderID"].ReadOnly = true;
                dc.Cells["HeaderTitle"].ReadOnly = true;
                dc.Cells["HeaderType"].ReadOnly = true;
                if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "allowences")
                {
                    dc.Cells["AllowanceAmount"].ReadOnly = true;
                    dc.Cells["DeductionAmount"].ReadOnly = true;
                    dc.Cells["ReimbursmentAmount"].ReadOnly = true;
                }
                else if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "deductions")
                {
                    dc.Cells["AllowanceAmount"].ReadOnly = true;
                    dc.Cells["DeductionAmount"].ReadOnly = true;
                    dc.Cells["ReimbursmentAmount"].ReadOnly = true;
                }
                else if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "reimbursement")
                {
                    dc.Cells["AllowanceAmount"].ReadOnly = true;
                    dc.Cells["DeductionAmount"].ReadOnly = true;
                    dc.Cells["ReimbursmentAmount"].ReadOnly = true;
                }
            }

            txtAallowences.Text = Convert.ToDecimal(totalAallowences.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtDeductions.Text = Convert.ToDecimal(totalDeductions.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtReimbursement.Text = Convert.ToDecimal(totalReimbursement.ToString()).ToString("0.00", CultureInfo.InvariantCulture);

            txtNetPayable.Text = Convert.ToDecimal((totalAallowences + totalReimbursement) - totalDeductions).ToString();
            txtNetPayable.Text = Convert.ToDecimal(txtNetPayable.Text.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
        }

        private void picRefreshDocumentsList_Click(object sender, EventArgs e)
        {
            RefreshUploadedDocumentsList();
        }

        private void picDownloadDocumentsList_Click(object sender, EventArgs e)
        {
            Download.DownloadExcel(lstLDocumentsList);
        }

        private void lstLDocumentsList_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show(lstLDocumentsList.SelectedItems[0].SubItems[2].Text.ToString());
        }

        private void picDownloadBankList_Click(object sender, EventArgs e)
        {
            Download.DownloadExcel(lstBankList);
        }

        private void picRefreshBankList_Click(object sender, EventArgs e)
        {
            RefreshBankList();
        }

        private void lstBankList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lblBankID.Text.ToString() == "0" || lblBankID.Text.ToString() == "")
                return;

            if (Convert.ToInt16(lblBankID.Text.ToString()) != lstBankList.SelectedItems[0].Index + 1)
            {
                if (MessageBox.Show("The Bank Name is getting changed from \n\n" + lstBankList.Items[Convert.ToInt16(lblBankID.Text.ToString()) - 1].SubItems[2].Text + " : " + lstBankList.Items[Convert.ToInt16(lblBankID.Text.ToString()) - 1].SubItems[3].Text + " [" + lstBankList.Items[Convert.ToInt16(lblBankID.Text.ToString()) - 1].SubItems[4].Text + "]" + " \n\nto\n\n " + lstBankList.SelectedItems[0].SubItems[2].Text + " : " + lstBankList.SelectedItems[0].SubItems[3].Text + " [" + lstBankList.SelectedItems[0].SubItems[4].Text + "]" + ".\n\n\nAre you sure to continue.?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    lstBankList.Items[Convert.ToInt16(lblBankID.Text.ToString()) - 1].Selected = true;
                    return;
                }
            }
        }

        private void cmbSalProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lblEmpID.Text.ToString() == "")
                return;

            if (cmbSalProfile.SelectedIndex < 0)
                return;

            dtgSalaryProfileDetails.DataSource = objSalaryProfile.GetDefaultSalaryProfileInfo(cmbSalProfile.SelectedIndex + 1);
            dtgSalaryProfileDetails.Columns["EmpSalDetID"].Visible = false;
            dtgSalaryProfileDetails.Columns["SalProDetID"].Visible = false;
            dtgSalaryProfileDetails.Columns["SalProfileID"].Visible = false;
            dtgSalaryProfileDetails.Columns["HeaderID"].Visible = false;
            dtgSalaryProfileDetails.Columns["HeaderTitle"].Width = 350;
            dtgSalaryProfileDetails.Columns["HeaderType"].Width = 150;
            dtgSalaryProfileDetails.Columns["CalcFormula"].Visible = false;
            dtgSalaryProfileDetails.Columns["IsFixed"].Visible = false;
            dtgSalaryProfileDetails.Columns["AllowanceAmount"].Width = 150;
            dtgSalaryProfileDetails.Columns["AllowanceAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgSalaryProfileDetails.Columns["AllowanceAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryProfileDetails.Columns["DeductionAmount"].Width = 150;
            dtgSalaryProfileDetails.Columns["DeductionAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgSalaryProfileDetails.Columns["DeductionAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryProfileDetails.Columns["ReimbursmentAmount"].Width = 150;
            dtgSalaryProfileDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgSalaryProfileDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryProfileDetails.Columns["OrderID"].Visible = false;
            dtgSalaryProfileDetails.Columns["SalProAmount"].Visible = false;

            decimal totalAallowences = 0;
            decimal totalDeductions = 0;
            decimal totalReimbursement = 0;

            foreach (DataGridViewRow dc in dtgSalaryProfileDetails.Rows)
            {
                totalAallowences = totalAallowences + Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString());
                totalDeductions = totalDeductions + Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString());
                totalReimbursement = totalReimbursement + Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString());
                dc.Cells["EmpSalDetID"].ReadOnly = true;
                dc.Cells["SalProDetID"].ReadOnly = true;
                dc.Cells["SalProfileID"].ReadOnly = true;
                dc.Cells["HeaderID"].ReadOnly = true;
                dc.Cells["HeaderTitle"].ReadOnly = true;
                dc.Cells["HeaderType"].ReadOnly = true;
                if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "allowences")
                {
                    dc.Cells["AllowanceAmount"].ReadOnly = true;
                    dc.Cells["DeductionAmount"].ReadOnly = true;
                    dc.Cells["ReimbursmentAmount"].ReadOnly = true;
                }
                else if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "deductions")
                {
                    dc.Cells["AllowanceAmount"].ReadOnly = true;
                    dc.Cells["DeductionAmount"].ReadOnly = true;
                    dc.Cells["ReimbursmentAmount"].ReadOnly = true;
                }
                else if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "reimbursement")
                {
                    dc.Cells["AllowanceAmount"].ReadOnly = true;
                    dc.Cells["DeductionAmount"].ReadOnly = true;
                    dc.Cells["ReimbursmentAmount"].ReadOnly = true;
                }
            }

            txtAallowences.Text = Convert.ToDecimal(totalAallowences.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtDeductions.Text = Convert.ToDecimal(totalDeductions.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtReimbursement.Text = Convert.ToDecimal(totalReimbursement.ToString()).ToString("0.00", CultureInfo.InvariantCulture);

            txtNetPayable.Text = Convert.ToDecimal((totalAallowences + totalReimbursement) - totalDeductions).ToString();
            txtNetPayable.Text = Convert.ToDecimal(txtNetPayable.Text.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
        }

        private void picPrevExperience_Click(object sender, EventArgs e)
        {
            frmLastCompanySelection frmLastCompanySelection = new frmLastCompanySelection(Convert.ToInt16(lblEmpID.Text));
            frmLastCompanySelection.ShowDialog(this);

            EmpWorkExpInfo objSelectedEmpWorkInfo = frmLastCompanySelection.selectedPublicWorkExpInfo;
            if (objSelectedEmpWorkInfo.LastCompID == null)
                return;

            List<EmpWorkExpInfo> objEmpWorkExpInfoList = new List<EmpWorkExpInfo>();
            foreach (DataGridViewRow dc in dtgPreviousWorkExp.Rows)
            {
                objEmpWorkExpInfoList.Add(new EmpWorkExpInfo
                {
                    LastCompID = Convert.ToInt16(dc.Cells["LastCompID"].Value.ToString()),
                    EmpID = Convert.ToInt16(dc.Cells["EmpID"].Value.ToString()),
                    LastCompanyInfoID = Convert.ToInt16(dc.Cells["LastCompanyInfoID"].Value.ToString()),
                    LastCompanyTitle = dc.Cells["LastCompanyTitle"].Value.ToString(),
                    Address = dc.Cells["Address"].Value.ToString(),
                    StartDate = Convert.ToDateTime(dc.Cells["StartDate"].Value.ToString()),
                    EndDate = Convert.ToDateTime(dc.Cells["EndDate"].Value.ToString()),
                    Comments = dc.Cells["Comments"].Value.ToString()
                });
            }

            objEmpWorkExpInfoList.Add(new EmpWorkExpInfo
            {
                LastCompID = objSelectedEmpWorkInfo.LastCompID,
                EmpID = objSelectedEmpWorkInfo.EmpID,
                LastCompanyInfoID = objSelectedEmpWorkInfo.LastCompanyInfoID,
                LastCompanyTitle = objSelectedEmpWorkInfo.LastCompanyTitle,
                Address = objSelectedEmpWorkInfo.Address,
                StartDate = objSelectedEmpWorkInfo.StartDate,
                EndDate = objSelectedEmpWorkInfo.EndDate,
                Comments = objSelectedEmpWorkInfo.Comments
            });

            dtgPreviousWorkExp.DataSource = objEmpWorkExpInfoList;// objEmpWorkExperienceInfo.UpdateWorkExpListInfo(Convert.ToInt16(lblEmpID.Text), objSelectedEmpWorkInfo);
            dtgPreviousWorkExp.Columns["LastCompID"].Visible = false;
            dtgPreviousWorkExp.Columns["LastCompanyInfoID"].Visible = false;
            dtgPreviousWorkExp.Columns["EmpID"].Visible = false;
            dtgPreviousWorkExp.Columns["LastCompanyTitle"].Width = 200;
            dtgPreviousWorkExp.Columns["Address"].Width = 350;
            dtgPreviousWorkExp.Columns["StartDate"].Width = 100;
            dtgPreviousWorkExp.Columns["StartDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgPreviousWorkExp.Columns["EndDate"].Width = 100;
            dtgPreviousWorkExp.Columns["EndDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgPreviousWorkExp.Columns["Comments"].Width = 350;
        }

        private void dtgPreviousWorkExp_DoubleClick(object sender, EventArgs e)
        {
            EmpWorkExpInfo objSelectedEmpWorkInfo = new EmpWorkExpInfo();
            objSelectedEmpWorkInfo.LastCompID = Convert.ToInt16(dtgPreviousWorkExp.SelectedRows[0].Cells["LastCompID"].Value.ToString());
            objSelectedEmpWorkInfo.LastCompanyInfoID = Convert.ToInt16(dtgPreviousWorkExp.SelectedRows[0].Cells["LastCompanyInfoID"].Value.ToString());
            objSelectedEmpWorkInfo.EmpID = dtgPreviousWorkExp.SelectedRows[0].Cells["EmpID"].Value == null ? 0 : Convert.ToInt16(dtgPreviousWorkExp.SelectedRows[0].Cells["EmpID"].Value.ToString());
            objSelectedEmpWorkInfo.LastCompanyTitle = dtgPreviousWorkExp.SelectedRows[0].Cells["LastCompanyTitle"].Value == null ? "" : dtgPreviousWorkExp.SelectedRows[0].Cells["LastCompanyTitle"].Value.ToString();
            objSelectedEmpWorkInfo.Address = dtgPreviousWorkExp.SelectedRows[0].Cells["Address"].Value == null ? "" : dtgPreviousWorkExp.SelectedRows[0].Cells["Address"].Value.ToString();
            objSelectedEmpWorkInfo.StartDate = dtgPreviousWorkExp.SelectedRows[0].Cells["StartDate"].Value == null ? Convert.ToDateTime(DateTime.Now.Date.ToString()) : Convert.ToDateTime(dtgPreviousWorkExp.SelectedRows[0].Cells["StartDate"].Value.ToString());
            objSelectedEmpWorkInfo.EndDate = dtgPreviousWorkExp.SelectedRows[0].Cells["EndDate"].Value == null ? Convert.ToDateTime(DateTime.Now.Date.ToString()) : Convert.ToDateTime(dtgPreviousWorkExp.SelectedRows[0].Cells["EndDate"].Value.ToString());
            objSelectedEmpWorkInfo.Comments = dtgPreviousWorkExp.SelectedRows[0].Cells["Comments"].Value == null ? "" : dtgPreviousWorkExp.SelectedRows[0].Cells["Comments"].Value.ToString();

            frmLastCompanySelection frmLastCompanySelection = new frmLastCompanySelection(objSelectedEmpWorkInfo);
            frmLastCompanySelection.ShowDialog(this);

            objSelectedEmpWorkInfo = frmLastCompanySelection.selectedPublicWorkExpInfo;

            dtgPreviousWorkExp.SelectedRows[0].Cells["LastCompID"].Value = objSelectedEmpWorkInfo.LastCompID.ToString();
            dtgPreviousWorkExp.SelectedRows[0].Cells["EmpID"].Value = objSelectedEmpWorkInfo.EmpID.ToString();
            dtgPreviousWorkExp.SelectedRows[0].Cells["LastCompanyInfoID"].Value = objSelectedEmpWorkInfo.LastCompanyInfoID.ToString();
            dtgPreviousWorkExp.SelectedRows[0].Cells["LastCompanyTitle"].Value = objSelectedEmpWorkInfo.LastCompanyTitle.ToString();
            dtgPreviousWorkExp.SelectedRows[0].Cells["Address"].Value = objSelectedEmpWorkInfo.Address.ToString();
            dtgPreviousWorkExp.SelectedRows[0].Cells["StartDate"].Value = objSelectedEmpWorkInfo.StartDate.ToString();
            dtgPreviousWorkExp.SelectedRows[0].Cells["EndDate"].Value = objSelectedEmpWorkInfo.EndDate.ToString();
            dtgPreviousWorkExp.SelectedRows[0].Cells["Comments"].Value = objSelectedEmpWorkInfo.Comments.ToString();

            dtgPreviousWorkExp.Columns["LastCompID"].Visible = false;
            dtgPreviousWorkExp.Columns["LastCompanyInfoID"].Visible = false;
            dtgPreviousWorkExp.Columns["EmpID"].Visible = false;
            dtgPreviousWorkExp.Columns["LastCompanyTitle"].Width = 200;
            dtgPreviousWorkExp.Columns["Address"].Width = 350;
            dtgPreviousWorkExp.Columns["StartDate"].Width = 100;
            dtgPreviousWorkExp.Columns["StartDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgPreviousWorkExp.Columns["EndDate"].Width = 100;
            dtgPreviousWorkExp.Columns["EndDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgPreviousWorkExp.Columns["Comments"].Width = 350;
        }

        private void dtgSalaryProfileDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            decimal totalAallowences = 0;
            decimal totalDeductions = 0;
            decimal totalReimbursement = 0;

            foreach (DataGridViewRow dc in dtgSalaryProfileDetails.Rows)
            {
                totalAallowences = totalAallowences + Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString());
                totalDeductions = totalDeductions + Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString());
                totalReimbursement = totalReimbursement + Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString());
            }

            txtAallowences.Text = Convert.ToDecimal(totalAallowences.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtDeductions.Text = Convert.ToDecimal(totalDeductions.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtReimbursement.Text = Convert.ToDecimal(totalReimbursement.ToString()).ToString("0.00", CultureInfo.InvariantCulture);

            txtNetPayable.Text = Convert.ToDecimal((totalAallowences + totalReimbursement) - totalDeductions).ToString();
            txtNetPayable.Text = Convert.ToDecimal(txtNetPayable.Text.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
        }

        private void cmbCurrentCountry_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (chkSamePerAddAsCurAdd.Checked == true)
                if (cmbPermanentCountry.SelectedIndex != -1)
                    cmbPermanentCountry.SelectedIndex = cmbCurrentCountry.SelectedIndex;
        }

        private void txtEmployeeName_TextChanged(object sender, EventArgs e)
        {
            string name = txtEmployeeName.Text.Trim();
            if (!string.IsNullOrEmpty(name))
            {
                string email = name.Replace(' ', '.').ToLower() + "@temp.com";
                txtEmployeeMailID.Text = email;
            }
            else
            {
                txtEmployeeMailID.Text = string.Empty;
            }
        }

        private void btnViewCalender_Click(object sender, EventArgs e)
        {
            //frmAttendanceMater frmAttendanceMater = new frmAttendanceMater("listAttendanceMasterList", Convert.ToInt16(lblEmpID.Text));
            frmAttendanceMater frmAttendanceMater = new frmAttendanceMater();
            frmAttendanceMater.ShowDialog(this);
        }

        private void frmSSEmployeeMaster_KeyDown(object sender, KeyEventArgs e)
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

        private void txtDateOfJoining_TextChanged(object sender, EventArgs e)
        {
            if (lblActionMode.Text.ToString().Trim() == "add")
            {
                txtEmploymentEffectiveFrom.Text = txtDateOfJoining.Text.ToString();
                txtShiftEffectiveFrom.Text = txtDateOfJoining.Text.ToString();
                txtTaxSchemeEffectiveFrom.Text = txtDateOfJoining.Text.ToString();
            }
        }

        private void txtTotalLeaveAllotment_TextChanged(object sender, EventArgs e)
        {
            if (lblActionMode.Text.ToString().Trim() == "add")
            {
                txtBalanceLeaveAllotment.Text = txtTotalLeaveAllotment.Text.ToString();
            }
        }

        private void picDownloadBankList_Click_1(object sender, EventArgs e)
        {

        }

        private void dtgFamilyMemberInforamtion_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            bool boolSetDefaultDate = false;
            EmpPersonalFamilyMemberInfo objMemberInfo = new EmpPersonalFamilyMemberInfo();
            objMemberInfo.EmpPerFamInfoID = (int)dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["EmpPerFamInfoID"].Value;
            objMemberInfo.PersonalInfoID = (int)dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["PersonalInfoID"].Value;
            objMemberInfo.FamMemName = dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemName"].Value.ToString();
            objMemberInfo.FamMemDOB = Convert.ToDateTime(dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemDOB"].Value);
            objMemberInfo.FamMemAge = dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemAge"].Value == null
                ? (int?)null : int.TryParse(dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemAge"].Value.ToString(), out var famMemAgeVal) ? famMemAgeVal : (int?)null;
            if (objMemberInfo.FamMemDOB.Value.ToString("dd-MM-yyyy").Equals("01-01-0001"))
            {
                objMemberInfo.FamMemDOB = DateTime.Today;
                objMemberInfo.FamMemAge = 0;
                boolSetDefaultDate = true;
            }
            objMemberInfo.FamMemRelationship = dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemRelationship"].Value.ToString();
            objMemberInfo.FamMemAddr1 = dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemAddr1"].Value.ToString();
            objMemberInfo.FamMemAddr2 = dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemAddr2"].Value.ToString();
            objMemberInfo.FamMemArea = dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemArea"].Value.ToString();
            objMemberInfo.FamMemCity = dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemCity"].Value.ToString();
            objMemberInfo.FamMemState = dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemState"].Value.ToString();
            objMemberInfo.FamMemPIN = dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemPIN"].Value.ToString();
            objMemberInfo.FamMemCountry = dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemCountry"].Value.ToString();
            objMemberInfo.FamMemContactNumber = dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemContactNumber"].Value.ToString();
            objMemberInfo.FamMemMailID = dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemMailID"].Value.ToString();
            objMemberInfo.FamMemBloodGroup = dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemBloodGroup"].Value.ToString();
            objMemberInfo.FamMemInsuranceEnrolled = Convert.ToBoolean(dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemInsuranceEnrolled"].Value.ToString());

            frmEmpFamilyMemberPopup frmEmpFamilyMemberPopup = new frmEmpFamilyMemberPopup(objMemberInfo);
            frmEmpFamilyMemberPopup.ShowDialog(this);
            objMemberInfo = frmEmpFamilyMemberPopup.objSaveTheseValues;

            dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["EmpPerFamInfoID"].Value = (int)objMemberInfo.EmpPerFamInfoID;
            dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["PersonalInfoID"].Value = (int)objMemberInfo.PersonalInfoID;
            dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemName"].Value = objMemberInfo.FamMemName;
            dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemAge"].Value = objMemberInfo.FamMemAge;
            dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemDOB"].Value = objMemberInfo.FamMemDOB;
            dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemCountry"].Value = objMemberInfo.FamMemCountry;
            if (boolSetDefaultDate && objMemberInfo.FamMemDOB.Value.ToString("dd-MM-yyyy").Equals(DateTime.Today.ToString("dd-MM-yyyy")))
            {
                dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemDOB"].Value = "";
                dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemAge"].Value = "";
                dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemCountry"].Value = "";
            }
            dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemRelationship"].Value = objMemberInfo.FamMemRelationship;
            dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemAddr1"].Value = objMemberInfo.FamMemAddr1;
            dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemAddr2"].Value = objMemberInfo.FamMemAddr2;
            dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemArea"].Value = objMemberInfo.FamMemArea;
            dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemCity"].Value = objMemberInfo.FamMemCity;
            dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemState"].Value = objMemberInfo.FamMemState;
            dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemPIN"].Value = objMemberInfo.FamMemPIN;
            dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemContactNumber"].Value = objMemberInfo.FamMemContactNumber;
            dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemMailID"].Value = objMemberInfo.FamMemMailID;
            dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemBloodGroup"].Value = objMemberInfo.FamMemBloodGroup;
            dtgFamilyMemberInforamtion.Rows[e.RowIndex].Cells["FamMemInsuranceEnrolled"].Value = Convert.ToBoolean(objMemberInfo.FamMemInsuranceEnrolled);
        }

        private void frmSSEmployeeMaster_Activated(object sender, EventArgs e)
        {
            dtgFamilyMemberInforamtion.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
            dtgLeaveEntitlement.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
            dtgPreviousWorkExp.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
            dtgSalaryProfileDetails.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
        }
    }
}
