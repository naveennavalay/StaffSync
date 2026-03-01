using DALStaffSync;
using iTextSharp.text.pdf;
using ModelStaffSync;
using StaffSync.StaffsyncDBDataSetTableAdapters;
using StaffSync.StaffsyncDBDTSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StaffSync
{
    public partial class frmOrgMasterInfo : Form
    {
        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        DALStaffSync.clsCountries objCountries = new DALStaffSync.clsCountries();
        DALStaffSync.clsClientInfo objClientInfo = new DALStaffSync.clsClientInfo();
        DALStaffSync.clsClientBranchInfo clsClientBranchInfo = new DALStaffSync.clsClientBranchInfo();
        clsImpageOperation objImpageOperation = new clsImpageOperation();
        DALStaffSync.clsPhotoMas objPhotoMas = new DALStaffSync.clsPhotoMas();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        DALStaffSync.clsClientStatutory objClientStatutory = new DALStaffSync.clsClientStatutory();
        frmDashboard objDashboard = (frmDashboard) System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();

        public frmOrgMasterInfo()
        {
            InitializeComponent();
        }

        public frmOrgMasterInfo(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmOrgMasterInfo(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
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
            this.Close();
        }

        private void frmOrgMasterInfo_Load(object sender, EventArgs e)
        {
            //// TODO: This line of code loads data into the 'staffsyncDBDTSet.EmpMasInfo' table. You can move, or remove it, as needed.
            //this.empMasInfoTableAdapter.Fill(this.staffsyncDBDTSet.EmpMasInfo);
            onCancelButtonClick();
            disableControls();
            clearControls();
        }

        private void btnCloseMe_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbRelationship_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmCompanyList frmCompanyList = new frmCompanyList(this);
            frmCompanyList.ShowDialog(this);
        }

        private void btnCloseMe_Click_2(object sender, EventArgs e)
        {
            this.Close();
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
            cmbIsActive.SelectedIndex = 1;
            //lblCompID.Text = objGenFunc.getMaxRowCount("ClientMas", "ClientID").Data.ToString();
            lblCompID.Text = objGenFunc.getMaxRowCount("ClientMas", "ClientCode", objTempClientFinYearInfo.ClientID).Data.ToString();
            txtCompCode.Text = "CNT-" + (lblCompID.Text.Trim()).ToString().PadLeft(4, '0');
            errValidator.Clear();

            cmbCountry.DataSource = objCountries.GetCountryList();
            cmbCountry.DisplayMember = "CountryTitle";
            cmbCountry.ValueMember = "CountryID";
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            string strValidationMessage = objLogin.ValidateUserRolesAndResponsibilitiesInfo(objTempCurrentlyLoggedInUserInfo.EmpID, "");
            if (strValidationMessage != "Success")
            {
                MessageBox.Show(strValidationMessage, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (ValidateValuesOnUI())
            {
                if (lblActionMode.Text == "add")
                {
                    int newID = objClientInfo.InsertClientInfo(txtCompCode.Text, txtCompanyName.Text, txtAddress01.Text, txtAddress02.Text, txtArea.Text, txtCity.Text, txtState.Text, txtPIN.Text, cmbCountry.Text, txtContactNumber.Text, txtMailID.Text, txtContactPerson.Text, txtContactNumber.Text, txtMailID.Text, txtWebsite.Text, cmbIsActive.Text.Trim() == "Yes" ? true : false, false, objTempClientFinYearInfo.FinYearID);
                    if (picCompLogo.Image != null)
                    {
                        byte[] image_bytes = objImpageOperation.ImageToBytes(picCompLogo.Image, ImageFormat.Jpeg, true);
                        if (image_bytes.Length > 0)
                        {
                            int photoID = objPhotoMas.UpdateCompanyLogoInfo(Convert.ToInt16(lblCompID.Text), image_bytes);
                        }
                    }

                    if (newID > 0)
                    {
                        objClientStatutory.InsertClientStatutory(Convert.ToInt16(lblCompID.Text), DateTime.Now, chkEnablePayrollStatutory.Checked, chkEnableProvidentFund.Checked, txtProvidentFundRegNumber.Text, chkEnableProfessionalTax.Checked, txtProfTaxRegNumber.Text, chkEnableEmployeeStateInsurance.Checked, txtESIRegNumber.Text, chkNationalPensionScheme.Checked, "NPS Reg. Number");
                        objClientStatutory.InsertClientProvidentFundSettings(1, optEmpPFPercentage.Checked == true ? "P" : "A", Convert.ToDecimal(txtEmpPFPercentage.Text == "" ? "0" : txtEmpPFPercentage.Text), Convert.ToDecimal(txtEmpPFFixedAmount.Text == "" ? "0" : txtEmpPFFixedAmount.Text), optEmprPFPercentage.Checked == true ? "P" : "A", Convert.ToDecimal(txtEmprPFPercentage.Text == "" ? "0" : txtEmprPFPercentage.Text), Convert.ToDecimal(txtEmprPFFixedAmount.Text == "" ? "0" : txtEmprPFFixedAmount.Text), optEmprEPSPercentage.Checked == true ? "P" : "A", Convert.ToDecimal(txtEmprEPSPercentage.Text == "" ? "0" : txtEmprEPSPercentage.Text), Convert.ToDecimal(txtEmprEPSFixedAmount.Text == "" ? "0" : txtEmprEPSFixedAmount.Text), DateTime.Now);

                        newID = clsClientBranchInfo.InsertClientBranchInfo(txtCompCode.Text, txtCompanyName.Text, txtAddress01.Text, txtAddress02.Text, txtArea.Text, txtCity.Text, txtState.Text, txtPIN.Text, cmbCountry.Text, txtContactNumber.Text, txtMailID.Text, txtContactPerson.Text, txtContactNumber.Text, txtMailID.Text, txtWebsite.Text, cmbIsActive.Text.Trim() == "Yes" ? true : false, false, objTempClientFinYearInfo.ClientID);
                        if (txtCompLogo.Text == "overwrite")
                        {
                            byte[] image_bytes = objImpageOperation.ImageToBytes(picCompLogo.Image, ImageFormat.Jpeg, true);
                            if (image_bytes.Length > 0)
                            {
                                int photoID = objPhotoMas.UpdateCompanyBranchLogoInfo(Convert.ToInt16(lblCompID.Text), image_bytes);
                            }
                        }
                        MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (lblActionMode.Text == "modify")
                {
                    int affectedRows = objClientInfo.UpdateClientInfo(Convert.ToInt16(lblCompID.Text), txtCompCode.Text, txtCompanyName.Text, txtAddress01.Text, txtAddress02.Text, txtArea.Text, txtCity.Text, txtState.Text, txtPIN.Text, cmbCountry.Text, txtContactNumber.Text, txtMailID.Text, txtContactPerson.Text, txtContactNumber.Text, txtMailID.Text, txtWebsite.Text, cmbIsActive.Text.Trim() == "Yes" ? true : false, false);
                    objClientStatutory.InsertClientStatutory(Convert.ToInt16(lblCompID.Text), DateTime.Now, chkEnablePayrollStatutory.Checked, chkEnableProvidentFund.Checked, txtProvidentFundRegNumber.Text, chkEnableProfessionalTax.Checked, txtProfTaxRegNumber.Text, chkEnableEmployeeStateInsurance.Checked, txtESIRegNumber.Text, chkNationalPensionScheme.Checked, "NSP Reg. Number" );
                    objClientStatutory.InsertClientProvidentFundSettings(1, optEmpPFPercentage.Checked == true ? "P" : "A", Convert.ToDecimal(txtEmpPFPercentage.Text == "" ? "0" : txtEmpPFPercentage.Text), Convert.ToDecimal(txtEmpPFFixedAmount.Text == "" ? "0" : txtEmpPFFixedAmount.Text), optEmprPFPercentage.Checked == true ? "P" : "A", Convert.ToDecimal(txtEmprPFPercentage.Text == "" ? "0" : txtEmprPFPercentage.Text), Convert.ToDecimal(txtEmprPFFixedAmount.Text == "" ? "0" : txtEmprPFFixedAmount.Text), optEmprEPSPercentage.Checked == true ? "P" : "A", Convert.ToDecimal(txtEmprEPSPercentage.Text == "" ? "0" : txtEmprEPSPercentage.Text), Convert.ToDecimal(txtEmprEPSFixedAmount.Text == "" ? "0" : txtEmprEPSFixedAmount.Text), DateTime.Now);

                    if (txtCompLogo.Text == "overwrite")
                    {
                        byte[] image_bytes = objImpageOperation.ImageToBytes(picCompLogo.Image, ImageFormat.Jpeg, true);
                        if (image_bytes.Length > 0)
                        {
                            int photoID = objPhotoMas.UpdateCompanyLogoInfo(Convert.ToInt16(lblCompID.Text), image_bytes);
                        }
                    }

                    if (affectedRows > 0)
                        MessageBox.Show("Details updated successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                objTempCurrentlyLoggedInUserInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objTempCurrentlyLoggedInUserInfo.EmpID.ToString()));

                onSaveButtonClick();
                disableControls();
                clearControls();
                errValidator.Clear();
            }
        }

        public bool ValidateValuesOnUI()
        {
            bool validationStatus = true;
            errValidator.Clear();

           DateTime dob, doj;
           string dateFormat = "dd-MM-yyyy";
           CultureInfo provider = CultureInfo.InvariantCulture;

            if (string.IsNullOrEmpty(txtCompanyName.Text))
            {
                validationStatus = false;
                txtCompanyName.Focus();
                errValidator.SetError(this.txtCompanyName, txtCompanyName.Tag?.ToString() ?? "Company Name is required.");
            }
            if (string.IsNullOrEmpty(txtAddress01.Text))
                txtAddress01.Text = "N/A";
            if (string.IsNullOrEmpty(txtAddress02.Text))
                txtAddress02.Text = "N/A";
            if (string.IsNullOrEmpty(txtArea.Text))
                txtArea.Text = "N/A";
            if (string.IsNullOrEmpty(txtCity.Text))
                txtCity.Text = "N/A";

            if (string.IsNullOrEmpty(txtState.Text))
            {
                validationStatus = false;
                txtState.Focus();
                errValidator.SetError(this.txtState, txtState.Tag?.ToString() ?? "State Name is required.");
            }
            if (string.IsNullOrEmpty(txtPIN.Text))
                txtPIN.Text = "N/A";
            if (string.IsNullOrEmpty(txtContactPerson.Text))
                txtContactPerson.Text = "N/A";
            if (string.IsNullOrEmpty(txtContactNumber.Text))
                txtContactNumber.Text = "N/A";
            if (string.IsNullOrEmpty(txtMailID.Text))
                txtMailID.Text = "N/A";
            if (string.IsNullOrEmpty(txtWebsite.Text))
                txtWebsite.Text = "N/A";

            if (chkEnablePayrollStatutory.Checked)
            {
                if (chkEnableProvidentFund.Checked)
                {
                    if (string.IsNullOrEmpty(txtProvidentFundRegNumber.Text))
                    {
                        validationStatus = false;
                        errValidator.SetError(this.txtProvidentFundRegNumber, txtProvidentFundRegNumber.Tag?.ToString() ?? "Provident Fund Registration Number is required.");
                    }
                    if (string.IsNullOrEmpty(cmbEPFDeductionCycleType.Text))
                    {
                        validationStatus = false;
                        errValidator.SetError(this.cmbEPFDeductionCycleType, cmbEPFDeductionCycleType.Tag?.ToString() ?? "Provident Fund Deduction Cycle need to be specified.");
                    }

                    if(txtEmpPFMaxLimit.Text.ToString().Trim() == "" || Convert.ToDecimal(txtEmpPFMaxLimit.Text) <= 0)
                    {
                        txtEmpPFMaxLimit.Text = "0.00";
                    }

                    if (optEmpPFPercentage.Checked == true && optEmpPFPercentage.Checked == false)
                    {
                        if(txtEmpPFPercentage.Text.ToString().Trim() == "" || Convert.ToDecimal(txtEmpPFPercentage.Text) <= 0)
                        {
                            validationStatus = false;
                            errValidator.SetError(this.txtEmpPFPercentage, txtEmpPFPercentage.Tag?.ToString() ?? "Employee Provident Fund Percentage need to be specified.");
                        }
                        txtEmpPFFixedAmount.Text = "0.00";
                    }
                    else if (optEmpPFPercentage.Checked == false && optEmpPFPercentage.Checked == true)
                    {
                        if (txtEmpPFFixedAmount.Text.ToString().Trim() == "" || Convert.ToDecimal(txtEmpPFFixedAmount.Text) <= 0)
                        {
                            validationStatus = false;
                            errValidator.SetError(this.txtEmpPFPercentage, txtEmpPFPercentage.Tag?.ToString() ?? "Employee Provident Fund Fixed Amount need to be specified.");
                        }
                        txtEmpPFFixedAmount.Text = "0.00";
                    }

                    if (optEmprPFPercentage.Checked == true && optEmprPFFixedAmount.Checked == false)
                    {
                        if (txtEmprPFPercentage.Text.ToString().Trim() == "" || Convert.ToDecimal(txtEmprPFPercentage.Text) <= 0)
                        {
                            validationStatus = false;
                            errValidator.SetError(this.txtEmprPFPercentage, txtEmprPFPercentage.Tag?.ToString() ?? "Employer Provident Fund Percentage need to be specified.");
                        }
                        txtEmprPFFixedAmount.Text = "0.00";
                    }
                    else if (optEmprPFPercentage.Checked == false && optEmprPFFixedAmount.Checked == true)
                    {
                        if (txtEmprPFFixedAmount.Text.ToString().Trim() == "" || Convert.ToDecimal(txtEmprPFFixedAmount.Text) <= 0)
                        {
                            validationStatus = false;
                            errValidator.SetError(this.txtEmprPFFixedAmount, txtEmprPFFixedAmount.Tag?.ToString() ?? "Employer Provident Fund Fixed Amount need to be specified.");
                        }
                        txtEmprPFPercentage.Text = "0.00";
                    }
                }
                else
                {
                    txtProvidentFundRegNumber.Text = "0.00";
                    txtEmpPFMaxLimit.Text = "0.00";
                    optEmpPFPercentage.Checked = true;
                    txtEmpPFPercentage.Text = "0.00";
                    optEmpPFFixedAmount.Checked = false;
                    txtEmpPFFixedAmount.Text = "0.00";
                    optEmprPFPercentage.Checked = true;
                    txtEmprPFPercentage.Text = "0.00";
                    optEmprPFFixedAmount.Checked = false;
                    txtEmprPFFixedAmount.Text = "0.00";
                }

                if(chkEnableEmployeeStateInsurance.Checked)
                {
                    if (string.IsNullOrEmpty(txtESIRegNumber.Text))
                    {
                        validationStatus = false;
                        errValidator.SetError(this.txtESIRegNumber, txtESIRegNumber.Tag?.ToString() ?? "Employee State Insurance Registration Number is required.");
                    }
                    if(cmbESIDurationCycle.Text == "")
                    {
                        validationStatus = false;
                        errValidator.SetError(this.cmbESIDurationCycle, cmbESIDurationCycle.Tag?.ToString() ?? "Employee State Insurance Duration Cycle need to be specified.");
                    }
                    if (optEmpESIPercentage.Checked && optEmpESIFixedAmount.Checked == false)
                    {
                        if (txtEmpESIPercentage.Text.ToString().Trim() == "" || Convert.ToDecimal(txtEmpESIPercentage.Text) <= 0)
                        {
                            validationStatus = false;
                            errValidator.SetError(this.txtEmpESIPercentage, txtEmpESIPercentage.Tag?.ToString() ?? "Employee ESI Percentage need to be specified.");
                        }
                        txtEmpESIFixedAmount.Text = "0.00";
                    }
                    else if (optEmpESIPercentage.Checked == false && optEmpESIFixedAmount.Checked == true)
                    {
                        if (txtEmpESIFixedAmount.Text.ToString().Trim() == "" || Convert.ToDecimal(txtEmpESIFixedAmount.Text) <= 0)
                        {
                            validationStatus = false;
                            errValidator.SetError(this.txtEmpESIFixedAmount, txtEmpESIFixedAmount.Tag?.ToString() ?? "Employee ESI Fixed Amount need to be specified.");
                        }
                        txtEmpESIPercentage.Text = "0.00";
                    }

                    if(optEmprESIPercentage.Checked && optEmprESIFixedAmount.Checked == false)
                    {
                        if (txtEmprESIPercentage.Text.ToString().Trim() == "" || Convert.ToDecimal(txtEmprESIPercentage.Text) <= 0)
                        {
                            validationStatus = false;
                            errValidator.SetError(this.txtEmprESIPercentage, txtEmprESIPercentage.Tag?.ToString() ?? "Employer ESI Percentage need to be specified.");
                        }
                        txtEmprESIFixedAmount.Text = "0.00";
                    }
                    else if (optEmprESIPercentage.Checked == false && optEmprESIFixedAmount.Checked == true)
                    {
                        if (txtEmprESIFixedAmount.Text.ToString().Trim() == "" || Convert.ToDecimal(txtEmprESIFixedAmount.Text) <= 0)
                        {
                            validationStatus = false;
                            errValidator.SetError(this.txtEmprESIFixedAmount, txtEmprESIFixedAmount.Tag?.ToString() ?? "Employer ESI Fixed Amount need to be specified.");
                        }
                        txtEmprESIPercentage.Text = "0.00";
                    }
                }
                else
                {
                    txtESIRegNumber.Text = "0.00";
                }
            }
            else
            {
                //chkEnableProvidentFund.Checked = false;
                grpPFGroup.Enabled = false;
                txtProvidentFundRegNumber.Text = "";
                //chkEnableEmployeeStateInsurance.Checked = false;
                grpESIGroup.Enabled = false;
                txtESIRegNumber.Text = "";
            }

            return validationStatus;
        }

        public void clearControls()
        {
            txtCompCode.Text = "";
            txtCompCode.ReadOnly = true;
            txtCompanyName.Text = "";
            txtAddress01.Text = "";
            txtAddress02.Text = "";
            txtArea.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtPIN.Text = "";
            cmbCountry.Text = "";
            txtContactNumber.Text = "";
            txtMailID.Text = "";
            txtWebsite.Text = "";

            picCompLogo.Image = null;

            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.SelectedIndex = 0;

            cmbEPFDeductionCycleType.Items.Clear();
            //cmbEPFDeductionCycleType.Items.Add("");
            cmbEPFDeductionCycleType.Items.Add("Monthly");
            cmbEPFDeductionCycleType.SelectedIndex = 0;
            cmbEPFDeductionCycleType.Enabled = false;

            cmbESIDurationCycle.Items.Clear();
            //cmbESIDurationCycle.Items.Add("");
            cmbESIDurationCycle.Items.Add("Monthly");
            cmbESIDurationCycle.SelectedIndex = 0;
            cmbESIDurationCycle.Enabled = false;

            //chkEnableProvidentFund.Checked = false;
            grpPFGroup.Enabled = false;
            txtProvidentFundRegNumber.Text = "";
            txtEmpPFMaxLimit.Text = "0.00";
            optEmpPFPercentage.Checked = true;
            txtEmpPFPercentage.Text = "0.00";
            optEmpPFFixedAmount.Checked = false;
            txtEmpPFFixedAmount.Text = "0.00";
            optEmprPFPercentage.Checked = true;
            txtEmprPFPercentage.Text = "0.00";
            optEmprPFFixedAmount.Checked = false;
            txtEmprPFFixedAmount.Text = "0.00";
            txtEmpPFMaxLimit.Text = "0.00";

            //chkEnableEmployeeStateInsurance.Checked = false;
            grpESIGroup.Enabled = false;
            txtESIRegNumber.Text = "";

            cmbESIDurationCycle.Items.Clear();
            //cmbESIDurationCycle.Items.Add("");
            cmbESIDurationCycle.Items.Add("Monthly");
            cmbESIDurationCycle.SelectedIndex = 0;
            cmbESIDurationCycle.Enabled = false;

            optEmpESIPercentage.Checked = true;
            txtEmpESIPercentage.Text = "0.00";
            optEmpESIFixedAmount.Checked = false;
            txtEmpESIFixedAmount.Text = "0.00";
            optEmprESIPercentage.Checked = true;
            txtEmprESIPercentage.Text = "0.00";
            optEmprESIFixedAmount.Checked = false;
            txtEmprESIFixedAmount.Text = "0.00";

            tabControl1.SelectedIndex = 0;
        }

        public void enableControls()
        {
            txtCompCode.Enabled = true;
            txtCompCode.ReadOnly = true;
            txtCompanyName.Enabled = true;
            txtAddress01.Enabled = true;
            txtAddress02.Enabled = true;
            txtArea.Enabled = true;
            txtCity.Enabled = true;
            txtState.Enabled = true;
            txtPIN.Enabled = true;
            cmbCountry.Enabled = true;
            txtContactNumber.Enabled = true;
            txtMailID.Enabled = true;
            txtWebsite.Enabled = true;

            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.Enabled = true;

            cmbEPFDeductionCycleType.Items.Clear();
            //cmbEPFDeductionCycleType.Items.Add("");
            cmbEPFDeductionCycleType.Items.Add("Monthly");
            cmbEPFDeductionCycleType.SelectedIndex = 0;
            cmbEPFDeductionCycleType.Enabled = false;

            //chkEnableProvidentFund.Checked = false;
            txtProvidentFundRegNumber.Enabled = true;
            txtEmpPFMaxLimit.Enabled = true;
            optEmpPFPercentage.Enabled = true;
            txtEmpPFPercentage.Enabled = true;
            optEmpPFFixedAmount.Enabled = true;
            txtEmpPFFixedAmount.Enabled = true;
            optEmprPFPercentage.Enabled = true;
            txtEmprPFPercentage.Enabled = true;
            optEmprPFFixedAmount.Enabled = true;
            txtEmprPFFixedAmount.Enabled = true;

            //chkEnableEmployeeStateInsurance.Checked = false;
            grpESIGroup.Enabled = false;
            txtESIRegNumber.Enabled = true;

            cmbESIDurationCycle.Items.Clear();
            //cmbESIDurationCycle.Items.Add("");
            cmbESIDurationCycle.Items.Add("Monthly");
            cmbESIDurationCycle.SelectedIndex = 0;
            cmbESIDurationCycle.Enabled = false;

            optEmpESIPercentage.Enabled = true;
            txtEmpESIPercentage.Enabled = true;
            optEmpESIFixedAmount.Enabled = true;
            txtEmpESIFixedAmount.Enabled = true;
            optEmprESIPercentage.Enabled = true;
            txtEmprESIPercentage.Enabled = true;
            optEmprESIFixedAmount.Enabled = true;
            txtEmprESIFixedAmount.Enabled = true;
        }

        public void disableControls()
        {
            txtCompCode.Enabled = false;
            txtCompCode.ReadOnly = true;
            txtCompanyName.Enabled = false;
            txtAddress01.Enabled = false;
            txtAddress02.Enabled = false;
            txtArea.Enabled = false;  
            txtCity.Enabled = false;  
            txtState.Enabled = false;
            txtPIN.Enabled = false;
            cmbCountry.Enabled = false;
            txtContactNumber.Enabled = false;
            txtMailID.Enabled = false;
            txtWebsite.Enabled = false;
            
            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.Enabled = false;

            cmbEPFDeductionCycleType.Items.Clear();
            //cmbEPFDeductionCycleType.Items.Add("");
            cmbEPFDeductionCycleType.Items.Add("Monthly");
            cmbEPFDeductionCycleType.SelectedIndex = 0;
            cmbEPFDeductionCycleType.Enabled = false;

            //chkEnableProvidentFund.Checked = false;
            grpPFGroup.Enabled = false;
            txtProvidentFundRegNumber.Enabled = false;
            txtEmpPFMaxLimit.Enabled = false;
            optEmpPFPercentage.Enabled = false;
            txtEmpPFPercentage.Enabled = false;
            optEmpPFFixedAmount.Enabled = false;
            txtEmpPFFixedAmount.Enabled = false;
            optEmprPFPercentage.Enabled = false;
            txtEmprPFPercentage.Enabled = false;
            optEmprPFFixedAmount.Enabled = false;
            txtEmprPFFixedAmount.Enabled = false;

            //chkEnableEmployeeStateInsurance.Checked = false;
            grpESIGroup.Enabled = false;
            txtESIRegNumber.Enabled = false;

            cmbESIDurationCycle.Items.Clear();
            //cmbESIDurationCycle.Items.Add("");
            cmbESIDurationCycle.Items.Add("Monthly");
            cmbESIDurationCycle.SelectedIndex = 0;
            cmbESIDurationCycle.Enabled = false;

            optEmpESIPercentage.Enabled = false;
            txtEmpESIPercentage.Enabled = false;
            optEmpESIFixedAmount.Enabled = false;
            txtEmpESIFixedAmount.Enabled = false;
            optEmprESIPercentage.Enabled = false;
            txtEmprESIPercentage.Enabled = false;
            optEmprESIFixedAmount.Enabled = false;
            txtEmprESIFixedAmount.Enabled = false;
        }

        public void onGenerateButtonClick()
        {
            lblActionMode.Text = "add";
            lblCompID.Text = "";
            btnSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = false;
            btnCancel.Enabled = true;
        }

        public void onModifyButtonClick()
        {
            lblActionMode.Text = "modify";
            lblCompID.Text = "";
            btnSearch.Enabled = true;
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = false;
            btnCancel.Enabled = true;
        }

        public void onRemoveButtonClick()
        {
            lblActionMode.Text = "remove";
            lblCompID.Text = "";
            btnSearch.Enabled = true;
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onSaveButtonClick()
        {
            lblActionMode.Text = "";
            lblCompID.Text = "";
            btnSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onCancelButtonClick()
        {
            lblActionMode.Text = "";
            lblCompID.Text = "";
            btnSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void displaySelectedValuesOnUI(ClientInfo ClientInfoModel)
        {
            lblCompID.Text = ClientInfoModel.ClientID.ToString();
            txtCompCode.Text = ClientInfoModel.ClientCode.ToString();
            txtCompanyName.Text = ClientInfoModel.ClientName.ToString();
            txtAddress01.Text = ClientInfoModel.ClientAddress1.ToString();
            txtAddress02.Text = ClientInfoModel.ClientAddress2.ToString();
            txtArea.Text = ClientInfoModel.ClientArea.ToString();
            txtCity.Text = ClientInfoModel.ClientCity.ToString();
            txtState.Text = ClientInfoModel.ClientState.ToString();
            txtPIN.Text = ClientInfoModel.ClientPIN.ToString();
            cmbCountry.Text = ClientInfoModel.ClientCountry.ToString();
            txtContactNumber.Text = ClientInfoModel.ClientPhone.ToString();
            txtMailID.Text = ClientInfoModel.ClientContactMail.ToString(); ;
            txtWebsite.Text = ClientInfoModel.ClientWebSite.ToString();

            txtCompLogo.Text = "";
            picCompLogo.Image = objImpageOperation.BytesToImage(objPhotoMas.getCompanyLogo(Convert.ToInt16(lblCompID.Text.ToString())).ClientLogo);

            cmbIsActive.Text = ClientInfoModel.IsActive == true ? "Yes" : "No";

            ClientStatutory selectedClientStatutory = objClientStatutory.getClientStatutory(Convert.ToInt16(lblCompID.Text.ToString()));
            chkEnablePayrollStatutory.Checked = selectedClientStatutory.EnableClientStatutory;
            chkEnableProvidentFund.Checked = selectedClientStatutory.EnablePF;
            grpPFGroup.Enabled = chkEnableProvidentFund.Checked;
            txtProvidentFundRegNumber.Text = selectedClientStatutory.PFCode;
            chkEnableProfessionalTax.Checked = selectedClientStatutory.EnablePT;
            //grpProfTaxGroup.Enabled = chkEnableProfessionalTax.Checked;
            txtProfTaxRegNumber.Text = selectedClientStatutory.PTCode;
            chkEnableEmployeeStateInsurance.Checked = chkEnableProfessionalTax.Checked;
            grpESIGroup.Enabled = chkEnableEmployeeStateInsurance.Checked;
            txtESIRegNumber.Text = selectedClientStatutory.ESICode;
            chkNationalPensionScheme.Checked = selectedClientStatutory.EnableNPS;
            //txtNPSRegNumber.Text = selectedClientStatutory.NPSCode;

            ProvidentFund objProvidentFund = objClientStatutory.GetCompanyProvidentFundSettings(Convert.ToInt16(lblCompID.Text.ToString()));
            if (chkEnableProvidentFund.Checked == true)
            {
                if (objProvidentFund.EmpPFPercentageOrAmount.ToString().ToUpper() == "P")
                {
                    optEmpPFPercentage.Checked = true;
                    optEmpPFFixedAmount.Checked = false;
                }
                else
                {
                    optEmpPFPercentage.Checked = false;
                    optEmpPFFixedAmount.Checked = true;
                }
                txtEmpPFPercentage.Text = objProvidentFund.EmpPFPercentage.ToString();
                txtEmpPFFixedAmount.Text = objProvidentFund.EmpPFAmount.ToString();
            }

            if (chkEnableProfessionalTax.Checked == true)
            {
                if (objProvidentFund.EmprPFPercentageOrAmount.ToString().ToUpper() == "P")
                {
                    optEmprPFPercentage.Checked = true;
                    optEmprPFFixedAmount.Checked = false;
                }
                else
                {
                    optEmprPFPercentage.Checked = false;
                    optEmprPFFixedAmount.Checked = true;
                }
                txtEmprPFPercentage.Text = objProvidentFund.EmprPFPercentage.ToString();
                txtEmprPFFixedAmount.Text = objProvidentFund.EmprPFAmount.ToString();

                if (objProvidentFund.EmprPSPercentageOrAmount.ToString().ToUpper() == "P")
                {
                    optEmprEPSPercentage.Checked = true;
                    optEmprESIFixedAmount.Checked = false;
                }
                else
                {
                    optEmprEPSPercentage.Checked = false;
                    optEmprESIFixedAmount.Checked = true;
                }
                txtEmprEPSPercentage.Text = objProvidentFund.EmprPSPercentage.ToString();
                txtEmprEPSFixedAmount.Text = objProvidentFund.EmprPSAmount.ToString();
            }
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
            onModifyButtonClick();
            clearControls();
            enableControls();
            cmbIsActive.SelectedIndex = 1;
            errValidator.Clear();

            cmbCountry.DataSource = objCountries.GetCountryList();
            cmbCountry.DisplayMember = "CountryTitle";
            cmbCountry.ValueMember = "CountryID";
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
                enableControls();
                cmbIsActive.SelectedIndex = 1;

                cmbCountry.DataSource = objCountries.GetCountryList();
                cmbCountry.DisplayMember = "CountryTitle";
                cmbCountry.ValueMember = "CountryID";
            }
            else if (lblActionMode.Text == "delete")
            {
                if (MessageBox.Show("The selected record will be deleted. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int affectedRows = objClientInfo.DeleteClientInfo(Convert.ToInt16(lblCompID.Text.Trim()));
                    if (affectedRows > 0)
                        MessageBox.Show("Details deleted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                onCancelButtonClick();
                disableControls();
                clearControls();
                cmbIsActive.SelectedIndex = 0;
                lblActionMode.Text = "";
                errValidator.Clear();
            }
        }

        private void frmOrgMasterInfo_KeyDown(object sender, KeyEventArgs e)
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

        private void btnCompanyLogoUpload_Click(object sender, EventArgs e)
        {
            if (ofdImage.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //txtEmpPhoto.Text = @ofdImage.FileName;
                    txtCompLogo.Text = "overwrite";
                    picCompLogo.Image = Image.FromFile(@ofdImage.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void chkEnablePayrollStatutory_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void chkEnableProvidentFund_CheckedChanged(object sender, EventArgs e)
        {
            grpPFGroup.Enabled = chkEnableProvidentFund.Checked;
        }

        private void chkEnableProfessionalTax_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkEnableEmployeeStateInsurance_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkNationalPensionScheme_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkEnableESI_CheckedChanged(object sender, EventArgs e)
        {
            grpESIGroup.Enabled = chkEnableEmployeeStateInsurance.Checked;
        }

        private void optEmpPFPercentage_CheckedChanged(object sender, EventArgs e)
        {
            if(optEmpPFPercentage.Checked)
            {
                txtEmpPFPercentage.Enabled = true;
                txtEmpPFFixedAmount.Enabled = false;
                txtEmpPFFixedAmount.Text = "0.00";
            }
        }

        private void optEmpPFFixedAmount_CheckedChanged(object sender, EventArgs e)
        {
            if (optEmpPFFixedAmount.Checked)
            {
                txtEmpPFPercentage.Enabled = false;
                txtEmpPFPercentage.Text = "0.00";
                txtEmpPFFixedAmount.Enabled = true;
            }
        }

        private void optEmprPFPercentage_CheckedChanged(object sender, EventArgs e)
        {
            if (optEmprPFPercentage.Checked)
            {
                txtEmprPFPercentage.Enabled = true;
                txtEmprPFFixedAmount.Enabled = false;
                txtEmprPFFixedAmount.Text = "0.00";
            }
        }

        private void optEmprPFFixedAmount_CheckedChanged(object sender, EventArgs e)
        {
            if (optEmprPFFixedAmount.Checked)
            {
                txtEmprPFPercentage.Enabled = false;
                txtEmprPFPercentage.Text = "0.00";
                txtEmprPFFixedAmount.Enabled = true;
            }
        }

        private void optEmprEPSPercentage_CheckedChanged(object sender, EventArgs e)
        {
            if (optEmprEPSPercentage.Checked)
            {
                txtEmprEPSPercentage.Enabled = true;
                txtEmprEPSFixedAmount.Enabled = false;
                txtEmprEPSFixedAmount.Text = "0.00";
            }
        }

        private void optEmprPESFixedAmount_CheckedChanged(object sender, EventArgs e)
        {
            if (optEmprPESFixedAmount.Checked)
            {
                txtEmprEPSPercentage.Enabled = false;
                txtEmprEPSPercentage.Text = "0.00";
                txtEmprEPSFixedAmount.Enabled = true;
            }
        }

        private void optEmpESIPercentage_CheckedChanged(object sender, EventArgs e)
        {
            if(optEmpESIPercentage.Checked)
            {
                txtEmpESIPercentage.Enabled = true;
                txtEmpESIFixedAmount.Enabled = false;
                txtEmpESIFixedAmount.Text = "0.00";
            }
        }

        private void optEmpESIFixedAmount_CheckedChanged(object sender, EventArgs e)
        {
            if (optEmpESIFixedAmount.Checked)
            {
                txtEmpESIPercentage.Enabled = false;
                txtEmpESIPercentage.Text = "0.00";
                txtEmpESIFixedAmount.Enabled = true;
            }
        }

        private void optEmprESIPercentage_CheckedChanged(object sender, EventArgs e)
        {
            if (optEmprESIPercentage.Checked)
            {
                txtEmprESIPercentage.Enabled = true;
                txtEmprESIFixedAmount.Enabled = false;
                txtEmprESIFixedAmount.Text = "0.00";
            }
        }

        private void optEmprESIFixedAmount_CheckedChanged(object sender, EventArgs e)
        {
            if (optEmprESIFixedAmount.Checked)
            {
                txtEmprESIPercentage.Enabled = false;
                txtEmprESIPercentage.Text = "0.00";
                txtEmprESIFixedAmount.Enabled = true;
            }
        }

        private void chkEnablePayrollStatutory_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkEnablePayrollStatutory.Checked)
            {
                tabOrgESISettings.Enabled = true;
                tabOrgProfessionalTax.Enabled = true;
                tabOrgLabourWelfareFund.Enabled = true;
            }
            else
            {
                tabOrgESISettings.Enabled = false;
                tabOrgProfessionalTax.Enabled = false;
                tabOrgLabourWelfareFund.Enabled = false;
            }
        }
    }
}
