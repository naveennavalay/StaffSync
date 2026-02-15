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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StaffSync
{
    public partial class frmBranchInfo : Form
    {
        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        DALStaffSync.clsCountries objCountries = new DALStaffSync.clsCountries();
        //DALStaffSync.clsClientInfo objClientInfo = new DALStaffSync.clsClientInfo();
        DALStaffSync.clsClientBranchInfo clsClientBranchInfo = new DALStaffSync.clsClientBranchInfo();
        clsImpageOperation objImpageOperation = new clsImpageOperation();
        DALStaffSync.clsPhotoMas objPhotoMas = new DALStaffSync.clsPhotoMas();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        frmDashboard objDashboard = (frmDashboard) System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();

        public frmBranchInfo()
        {
            InitializeComponent();
        }

        public frmBranchInfo(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmBranchInfo(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
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
            this.Close();
        }

        private void frmBranchInfo_Load(object sender, EventArgs e)
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
            frmBranchList frmBranchList = new frmBranchList(this, objTempClientFinYearInfo.ClientID);
            frmBranchList.ShowDialog(this);
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
            cmbIsActive.SelectedIndex = 2;
            //lblCompID.Text = objGenFunc.getMaxRowCount("ClientBranchMas", "ClientBranchCode", objTempClientFinYearInfo.ClientID).Data.ToString();
            lblCompID.Text = objGenFunc.getMaxRowCount("ClientBranchMas", "ClientBranchCode", objTempClientFinYearInfo.ClientID).Data.ToString();
            txtCompCode.Text = "BRN-" + (lblCompID.Text.Trim()).ToString().PadLeft(4, '0');
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
            
            if (string.IsNullOrEmpty(txtCompanyName.Text))
            {
                txtCompanyName.Focus();
                errValidator.SetError(this.txtCompanyName, "Please enter Company Name");
            }
            else
            {
                if (lblActionMode.Text == "add")
                {
                    int newID = clsClientBranchInfo.InsertClientBranchInfo(txtCompCode.Text, txtCompanyName.Text, txtAddress01.Text, txtAddress02.Text, txtArea.Text, txtCity.Text, txtState.Text, txtPIN.Text, cmbCountry.Text, txtContactNumber.Text, txtMailID.Text, txtContactPerson.Text, txtContactNumber.Text, txtMailID.Text, txtWebsite.Text, cmbIsActive.Text.Trim() == "Yes" ? true : false, false, objTempClientFinYearInfo.ClientID);
                    if (txtCompLogo.Text == "overwrite")
                    {
                        byte[] image_bytes = objImpageOperation.ImageToBytes(picCompLogo.Image, ImageFormat.Jpeg, true);
                        if (image_bytes.Length > 0)
                        {
                            int photoID = objPhotoMas.UpdateCompanyBranchLogoInfo(Convert.ToInt16(lblCompID.Text), image_bytes);
                        }
                    }

                    if (newID > 0)
                        MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (lblActionMode.Text == "modify")
                {
                    int affectedRows = clsClientBranchInfo.UpdateClientBranchInfo(Convert.ToInt16(lblCompID.Text), txtCompCode.Text, txtCompanyName.Text, txtAddress01.Text, txtAddress02.Text, txtArea.Text, txtCity.Text, txtState.Text, txtPIN.Text, cmbCountry.Text, txtContactNumber.Text, txtMailID.Text, txtContactPerson.Text, txtContactNumber.Text, txtMailID.Text, txtWebsite.Text, cmbIsActive.Text.Trim() == "Yes" ? true : false, false, objTempClientFinYearInfo.ClientID);
                    if (txtCompLogo.Text == "overwrite")
                    {
                        byte[] image_bytes = objImpageOperation.ImageToBytes(picCompLogo.Image, ImageFormat.Jpeg, true);
                        if (image_bytes.Length > 0)
                        {
                            int photoID = objPhotoMas.UpdateCompanyBranchLogoInfo(Convert.ToInt16(lblCompID.Text), image_bytes);
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
            txtContactPerson.Text = "";
            txtContactNumber.Text = "";
            txtMailID.Text = "";
            txtWebsite.Text = "";

            picCompLogo.Image = null;

            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.SelectedIndex = 2;
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
            txtContactPerson.Enabled = true;
            txtContactNumber.Enabled = true;
            txtMailID.Enabled = true;
            txtWebsite.Enabled = true;

            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.SelectedIndex = 2;
            cmbIsActive.Enabled = true;
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
            txtContactPerson.Enabled = false;
            txtContactNumber.Enabled = false;
            txtMailID.Enabled = false;
            txtWebsite.Enabled = false;
            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.SelectedIndex = 2;
            cmbIsActive.Enabled = false;
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

        public void displaySelectedValuesOnUI(ClientBranchInfo ClientBranchInfoModel)
        {
            lblCompID.Text = ClientBranchInfoModel.ClientBranchID.ToString();
            txtCompCode.Text = ClientBranchInfoModel.ClientBranchCode.ToString();
            txtCompanyName.Text = ClientBranchInfoModel.ClientBranchName.ToString();
            txtAddress01.Text = ClientBranchInfoModel.ClientBranchAddress1.ToString();
            txtAddress02.Text = ClientBranchInfoModel.ClientBranchAddress2.ToString();
            txtArea.Text = ClientBranchInfoModel.ClientBranchArea.ToString();
            txtCity.Text = ClientBranchInfoModel.ClientBranchCity.ToString();
            txtState.Text = ClientBranchInfoModel.ClientBranchState.ToString();
            txtPIN.Text = ClientBranchInfoModel.ClientBranchPIN.ToString();
            cmbCountry.Text = ClientBranchInfoModel.ClientBranchCountry.ToString();
            txtContactPerson.Text = ClientBranchInfoModel.ClientBranchContactPerson.ToString();
            txtContactNumber.Text = ClientBranchInfoModel.ClientBranchPhone.ToString();
            txtMailID.Text = ClientBranchInfoModel.ClientBranchContactMail.ToString(); ;
            txtWebsite.Text = ClientBranchInfoModel.ClientBranchWebSite.ToString();

            txtCompLogo.Text = "";
            picCompLogo.Image = objImpageOperation.BytesToImage(objPhotoMas.getCompanyBranchLogo(Convert.ToInt16(lblCompID.Text.ToString())).ClientBranchLogo);

            cmbIsActive.Text = ClientBranchInfoModel.IsActive == true ? "Yes" : "No";
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
            cmbIsActive.SelectedIndex = 2;
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
                cmbIsActive.SelectedIndex = 2;

                cmbCountry.DataSource = objCountries.GetCountryList();
                cmbCountry.DisplayMember = "CountryTitle";
                cmbCountry.ValueMember = "CountryID";
            }
            else if (lblActionMode.Text == "delete")
            {
                if (MessageBox.Show("The selected record will be deleted. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int affectedRows = clsClientBranchInfo.DeleteClientBranchInfo(Convert.ToInt16(lblCompID.Text.Trim()));
                    if (affectedRows > 0)
                        MessageBox.Show("Details deleted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                onCancelButtonClick();
                disableControls();
                clearControls();
                cmbIsActive.SelectedIndex = 2;
                lblActionMode.Text = "";
                errValidator.Clear();
            }
        }

        private void frmBranchInfo_KeyDown(object sender, KeyEventArgs e)
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
    }
}
