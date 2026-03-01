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
    public partial class frmCompanyInfo : Form
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

        public frmCompanyInfo()
        {
            InitializeComponent();
        }

        public frmCompanyInfo(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmCompanyInfo(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
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

        private void frmCompanyInfo_Load(object sender, EventArgs e)
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
                        objClientStatutory.InsertClientStatutory(Convert.ToInt16(lblCompID.Text), DateTime.Now, chkEnablePayrollStatutory.Checked, chkEnableProvidentFund.Checked, txtProvidentFundRegNumber.Text, chkEnableProfessionalTax.Checked, txtProfTaxRegNumber.Text, chkEnableEmployeeStateInsurance.Checked, txtESIRegNumber.Text, chkNationalPensionScheme.Checked, txtNPSRegNumber.Text);
                        objClientStatutory.InsertClientProvidentFundSettings(1, optEmpPFPercentage.Checked == true ? "P" : "A", Convert.ToDecimal(txtEmpPFPercentage.Text == "" ? "0" : txtEmpPFPercentage.Text), Convert.ToDecimal(txtEmpPFFixedAmount.Text == "" ? "0" : txtEmpPFFixedAmount.Text), optEmprPFPercentage.Checked == true ? "P" : "A", Convert.ToDecimal(txtEmprPFPercentage.Text == "" ? "0" : txtEmprPFPercentage.Text), Convert.ToDecimal(txtEmprPFFixedAmount.Text == "" ? "0" : txtEmprPFFixedAmount.Text), optEmprNPSPercentage.Checked == true ? "P" : "A", Convert.ToDecimal(txtEmprNPSPercentage.Text == "" ? "0" : txtEmprNPSPercentage.Text), Convert.ToDecimal(txtEmprNPSFixedAmount.Text == "" ? "0" : txtEmprNPSFixedAmount.Text), DateTime.Now);

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
                    objClientStatutory.InsertClientStatutory(Convert.ToInt16(lblCompID.Text), DateTime.Now, chkEnablePayrollStatutory.Checked, chkEnableProvidentFund.Checked, txtProvidentFundRegNumber.Text, chkEnableProfessionalTax.Checked, txtProfTaxRegNumber.Text, chkEnableEmployeeStateInsurance.Checked, txtESIRegNumber.Text, chkNationalPensionScheme.Checked, txtNPSRegNumber.Text);
                    objClientStatutory.InsertClientProvidentFundSettings(1, optEmpPFPercentage.Checked == true ? "P" : "A", Convert.ToDecimal(txtEmpPFPercentage.Text == "" ? "0" : txtEmpPFPercentage.Text), Convert.ToDecimal(txtEmpPFFixedAmount.Text == "" ? "0" : txtEmpPFFixedAmount.Text), optEmprPFPercentage.Checked == true ? "P" : "A", Convert.ToDecimal(txtEmprPFPercentage.Text == "" ? "0" : txtEmprPFPercentage.Text), Convert.ToDecimal(txtEmprPFFixedAmount.Text == "" ? "0" : txtEmprPFFixedAmount.Text), optEmprNPSPercentage.Checked == true ? "P" : "A", Convert.ToDecimal(txtEmprNPSPercentage.Text == "" ? "0" : txtEmprNPSPercentage.Text), Convert.ToDecimal(txtEmprNPSFixedAmount.Text == "" ? "0" : txtEmprNPSFixedAmount.Text), DateTime.Now);

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

            // Helper for date validation
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
                        txtProvidentFundRegNumber.Focus();
                        errValidator.SetError(this.txtProvidentFundRegNumber, txtProvidentFundRegNumber.Tag?.ToString() ?? "Provident Fund Registration Number is required.");
                    }
                }
                if (chkEnableProfessionalTax.Checked)
                {
                    if (string.IsNullOrEmpty(txtProfTaxRegNumber.Text))
                    {
                        validationStatus = false;
                        txtProfTaxRegNumber.Focus();
                        errValidator.SetError(this.txtProfTaxRegNumber, txtProfTaxRegNumber.Tag?.ToString() ?? "Professional Tax Registration Number is required.");
                    }
                }
                if (chkEnableEmployeeStateInsurance.Checked)
                {
                    if (string.IsNullOrEmpty(txtESIRegNumber.Text))
                    {
                        validationStatus = false;
                        txtESIRegNumber.Focus();
                        errValidator.SetError(this.txtESIRegNumber, txtESIRegNumber.Tag?.ToString() ?? "State Insurance Registration Number is required.");
                    }
                }
                if (chkNationalPensionScheme.Checked)
                {
                    if (string.IsNullOrEmpty(txtNPSRegNumber.Text))
                    {
                        validationStatus = false;
                        txtNPSRegNumber.Focus();
                        errValidator.SetError(this.txtNPSRegNumber, txtNPSRegNumber.Tag?.ToString() ?? "National Pension Scheme Registration Number is required.");
                    }
                }
            }
            else
            {
                chkEnableProvidentFund.Checked = false;
                txtProvidentFundRegNumber.Text = "";
                chkEnableProfessionalTax.Checked = false;
                txtProfTaxRegNumber.Text = "";
                chkNationalPensionScheme.Checked = false;
                txtNPSRegNumber.Text = "";
                chkEnableEmployeeStateInsurance.Checked = false;
                txtESIRegNumber.Text = "";
            }

            if (chkEnablePayrollStatutory.Checked)
            {
                if (chkEnableProvidentFund.Checked == false && chkEnableProfessionalTax.Checked == false && chkNationalPensionScheme.Checked == false)
                {
                    validationStatus = false;
                    chkEnableProvidentFund.Focus();
                    errValidator.SetError(this.chkEnableProvidentFund, chkEnableProvidentFund.Tag?.ToString() ?? "At least one statutory option must be enabled.");
                }
            }

            if (chkEnableProvidentFund.Checked)
            {
                if(txtProvidentFundRegNumber.Text == "")
                {
                    validationStatus = false;
                    txtProvidentFundRegNumber.Focus();
                    errValidator.SetError(this.txtProvidentFundRegNumber, txtProvidentFundRegNumber.Tag?.ToString() ?? "Provident Fund Registration Number is required.");
                }
                if (optEmpPFPercentage.Checked == false && optEmpPFFixedAmount.Checked == false)
                {
                    validationStatus = false;
                    //optEmpPFPercentage.Focus();
                    errValidator.SetError(this.optEmpPFPercentage, optEmpPFPercentage.Tag?.ToString() ?? "Employee PF Percentage/Amount is required.");
                }
            }

            if (chkEnableProfessionalTax.Checked)
            {
                if(txtProfTaxRegNumber.Text == "")
                {
                    validationStatus = false;
                    txtProfTaxRegNumber.Focus();
                    errValidator.SetError(this.txtProfTaxRegNumber, txtProfTaxRegNumber.Tag?.ToString() ?? "Professional Tax Registration Number is required.");
                }
                if (optEmprPFPercentage.Checked == false && optEmprPFFixedAmount.Checked == false)
                {
                    validationStatus = false;
                    //optEmprPFPercentage.Focus();
                    errValidator.SetError(this.optEmprPFPercentage, optEmprPFPercentage.Tag?.ToString() ?? "Employeer PF Percentage/Amount is required.");
                }
            }

            if (chkNationalPensionScheme.Checked)
            {
                if(txtNPSRegNumber.Text == "")
                {
                    validationStatus = false;
                    txtNPSRegNumber.Focus();
                    errValidator.SetError(this.txtNPSRegNumber, txtNPSRegNumber.Tag?.ToString() ?? "National Pension Scheme Registration Number is required.");
                }
                if (optEmprNPSPercentage.Checked == false && optEmprNPSFixedAmount.Checked == false)
                {
                    validationStatus = false;
                    //optEmprNPSPercentage.Focus();
                    errValidator.SetError(this.optEmprNPSPercentage, optEmprNPSPercentage.Tag?.ToString() ?? "Employeer NPS Percentage/Amount is required.");
                }
            }

            if (optEmpPFPercentage.Checked)
            {
                if (string.IsNullOrEmpty(txtEmpPFPercentage.Text))
                {
                    validationStatus = false;
                    txtEmpPFPercentage.Focus();
                    errValidator.SetError(this.txtEmpPFPercentage, txtEmpPFPercentage.Tag?.ToString() ?? "Employee PF Percentage is required.");
                }
            }
            else if (optEmpPFFixedAmount.Checked)
            {
                if (string.IsNullOrEmpty(txtEmpPFFixedAmount.Text))
                {
                    validationStatus = false;
                    txtEmpPFFixedAmount.Focus();
                    errValidator.SetError(this.txtEmpPFFixedAmount, txtEmpPFFixedAmount.Tag?.ToString() ?? "Employee PF Fixed Amount is required.");
                }
            }

            if(optEmprPFPercentage.Checked)
            {
                if (string.IsNullOrEmpty(txtEmprPFPercentage.Text))
                {
                    validationStatus = false;
                    txtEmprPFPercentage.Focus();
                    errValidator.SetError(this.txtEmprPFPercentage, txtEmprPFPercentage.Tag?.ToString() ?? "Employer PF Percentage is required.");
                }
            }
            else if (optEmprPFFixedAmount.Checked)
            {
                if (string.IsNullOrEmpty(txtEmprPFFixedAmount.Text))
                {
                    validationStatus = false;
                    txtEmprPFFixedAmount.Focus();
                    errValidator.SetError(this.txtEmprPFFixedAmount, txtEmprPFFixedAmount.Tag?.ToString() ?? "Employer PF Fixed Amount is required.");
                }
            }

            if(optEmprNPSPercentage.Checked)
            {
                if (string.IsNullOrEmpty(txtEmprNPSPercentage.Text))
                {
                    validationStatus = false;
                    txtEmprNPSPercentage.Focus();
                    errValidator.SetError(this.txtEmprNPSPercentage, txtEmprNPSPercentage.Tag?.ToString() ?? "Employer NPS Percentage is required.");
                }
            }
            else if (optEmprNPSFixedAmount.Checked)
            {
                if (string.IsNullOrEmpty(txtEmprNPSFixedAmount.Text))
                {
                    validationStatus = false;
                    txtEmprNPSFixedAmount.Focus();
                    errValidator.SetError(this.txtEmprNPSFixedAmount, txtEmprNPSFixedAmount.Tag?.ToString() ?? "Employer NPS Fixed Amount is required.");
                }
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

            chkEnablePayrollStatutory.Checked = false;
            chkEnablePayrollStatutory.Checked = false;
            txtProvidentFundRegNumber.Text = "";
            chkEnableProfessionalTax.Checked = false;
            txtProfTaxRegNumber.Text = "";
            chkEnableEmployeeStateInsurance.Checked = false;
            txtESIRegNumber.Text = "";
            chkNationalPensionScheme.Checked = false;
            txtNPSRegNumber.Text = "";

            optEmpPFPercentage.Checked = false;
            optEmpPFFixedAmount.Checked = false;
            txtEmpPFPercentage.Text = "";
            txtEmpPFFixedAmount.Text = "";

            optEmprPFPercentage.Checked = false;
            optEmprPFFixedAmount.Checked = false;
            txtEmprPFPercentage.Text = "";
            txtEmprPFFixedAmount.Text = "";

            optEmprNPSPercentage.Checked = false;
            optEmprNPSFixedAmount.Checked = false;
            txtEmprNPSPercentage.Text = "";
            txtEmprNPSFixedAmount.Text = "";
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

            chkEnablePayrollStatutory.Enabled = true;
            chkEnableProvidentFund.Checked = false;
            txtProvidentFundRegNumber.Text = "";
            chkEnableProfessionalTax.Checked = false;
            txtProfTaxRegNumber.Text = "";
            chkEnableEmployeeStateInsurance.Checked = false;
            txtESIRegNumber.Text = "";
            chkNationalPensionScheme.Checked = false;
            txtNPSRegNumber.Text = "";

            optEmpPFPercentage.Enabled = true;
            optEmpPFFixedAmount.Enabled = true;
            txtEmpPFPercentage.Enabled = true;
            txtEmpPFFixedAmount.Enabled = true;

            optEmprPFPercentage.Enabled = true;
            optEmprPFFixedAmount.Enabled = true;
            txtEmprPFPercentage.Enabled = true;
            txtEmprPFFixedAmount.Enabled = true;

            optEmprNPSPercentage.Enabled = true;
            optEmprNPSFixedAmount.Enabled = true;
            txtEmprNPSPercentage.Enabled = true;
            txtEmprNPSFixedAmount.Enabled = true;
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

            chkEnablePayrollStatutory.Enabled = false;
            chkEnablePayrollStatutory.Checked = false;
            chkEnableProvidentFund.Checked = false;
            txtProvidentFundRegNumber.Text = "";
            chkEnableProfessionalTax.Checked = false;
            txtProfTaxRegNumber.Text = "";
            chkEnableEmployeeStateInsurance.Checked = false;
            txtESIRegNumber.Text = "";
            chkNationalPensionScheme.Checked = false;
            txtNPSRegNumber.Text = "";

            optEmpPFPercentage.Enabled = false;
            optEmpPFFixedAmount.Enabled = false;
            txtEmpPFPercentage.Enabled = false;
            txtEmpPFFixedAmount.Enabled = false;

            optEmprPFPercentage.Enabled = false;
            optEmprPFFixedAmount.Enabled = false;
            txtEmprPFPercentage.Enabled = false;
            txtEmprPFFixedAmount.Enabled = false;

            optEmprNPSPercentage.Enabled = false;
            optEmprNPSFixedAmount.Enabled = false;
            txtEmprNPSPercentage.Enabled = false;
            txtEmprNPSFixedAmount.Enabled = false;
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
            txtProvidentFundRegNumber.Text = selectedClientStatutory.PFCode;
            chkEnableProfessionalTax.Checked = selectedClientStatutory.EnablePT;
            txtProfTaxRegNumber.Text = selectedClientStatutory.PTCode;
            chkEnableEmployeeStateInsurance.Checked = selectedClientStatutory.EnableESI;
            txtESIRegNumber.Text = selectedClientStatutory.ESICode;
            chkNationalPensionScheme.Checked = selectedClientStatutory.EnableNPS;
            txtNPSRegNumber.Text = selectedClientStatutory.NPSCode;

            ProvidentFund objProvidentFund = objClientStatutory.GetCompanyProvidentFundSettings(Convert.ToInt16(lblCompID.Text.ToString()));
            if(chkEnableProvidentFund.Checked == true)
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
                    optEmprNPSPercentage.Checked = true;
                    optEmprNPSFixedAmount.Checked = false;
                }
                else
                {
                    optEmprNPSPercentage.Checked = false;
                    optEmprNPSFixedAmount.Checked = true;
                }
                txtEmprNPSPercentage.Text = objProvidentFund.EmprPSPercentage.ToString();
                txtEmprNPSFixedAmount.Text = objProvidentFund.EmprPSAmount.ToString();
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

        private void frmCompanyInfo_KeyDown(object sender, KeyEventArgs e)
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
            if (chkEnablePayrollStatutory.Checked)
            {
                grpPayrollStatutory.Enabled = true;
            }
            else
            {
                grpPayrollStatutory.Enabled = false;
            }
        }

        private void chkEnableProvidentFund_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnableProvidentFund.Checked)
            {
                grpProvidentFund.Enabled = true;
            }
            else
            {
                grpProvidentFund.Enabled = false;
                txtProvidentFundRegNumber.Text = "";
            }
        }

        private void chkEnableProfessionalTax_CheckedChanged(object sender, EventArgs e)
        {
            if(chkEnableProfessionalTax.Checked)
            {
                grpProfessionalTax.Enabled = true;
            }
            else
            {
                grpProfessionalTax.Enabled = false;
                txtProfTaxRegNumber.Text = "";
            }
        }

        private void chkEnableEmployeeStateInsurance_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnableEmployeeStateInsurance.Checked)
            {
                grpEmployeeStateInsurance.Enabled = true;
            }
            else
            {
                grpEmployeeStateInsurance.Enabled = false;
                txtESIRegNumber.Text = "";
            }
        }

        private void chkNationalPensionScheme_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNationalPensionScheme.Checked)
            {
                grpNationalPensionScheme.Enabled = true;
            }
            else
            {
                grpNationalPensionScheme.Enabled = false;
                txtNPSRegNumber.Text = "";
            }
        }
    }
}
