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
using System.Windows.Controls;
using System.Windows.Forms;
using YourNamespace;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StaffSync
{
    public partial class frmAssetsInfo : Form
    {
        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        DALStaffSync.clsCountries objCountries = new DALStaffSync.clsCountries();
        DALStaffSync.clsClientInfo objClientInfo = new DALStaffSync.clsClientInfo();
        DALStaffSync.clsClientSignatoryInfo objClientSignatoryInfo = new DALStaffSync.clsClientSignatoryInfo();
        DALStaffSync.clsClientBranchInfo clsClientBranchInfo = new DALStaffSync.clsClientBranchInfo();
        clsImpageOperation objImpageOperation = new clsImpageOperation();
        DALStaffSync.clsPhotoMas objPhotoMas = new DALStaffSync.clsPhotoMas();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        DALStaffSync.clsClientStatutory objClientStatutory = new DALStaffSync.clsClientStatutory();
        DALStaffSync.clsClientBranchInfo objClientBranchInfo = new DALStaffSync.clsClientBranchInfo();
        DALStaffSync.clsDesignation objDesignation = new DALStaffSync.clsDesignation();
        DALStaffSync.clsAssetsCategory objAssetsCategory = new DALStaffSync.clsAssetsCategory();
        DALStaffSync.clsAssetsInfo objAssetsInfo = new DALStaffSync.clsAssetsInfo();
        DALStaffSync.clsSexMas objGender = new DALStaffSync.clsSexMas();
        DALStaffSync.clsProfessionalTaxCalculation objProfessionalTaxSlab = new DALStaffSync.clsProfessionalTaxCalculation();
        frmDashboard objDashboard = (frmDashboard) System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();

        public frmAssetsInfo()
        {
            InitializeComponent();
        }

        public frmAssetsInfo(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmAssetsInfo(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
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

        private void frmAssetsInfo_Load(object sender, EventArgs e)
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
            if (lblAsssetID.Text.ToString().Trim() == "")
                lblAsssetID.Text = "0";

            frmAssetInfoList frmAssetInfoList = new frmAssetInfoList(this, objTempClientFinYearInfo.ClientID);
            frmAssetInfoList.ShowDialog(this);
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
            lblAsssetID.Text = objGenFunc.getMaxRowCount("AssetMas", "AssetID").Data.ToString();
            txtAssetCode.Text = "ASC-" + (lblAsssetID.Text.Trim()).ToString().PadLeft(4, '0');
            errValidator.Clear();

            cmbAssetCategory.DataSource = objAssetsCategory.getAssetsCategoryNamesList(Convert.ToInt32(objTempClientFinYearInfo.ClientID));
            cmbAssetCategory.DisplayMember = "AssetName";
            cmbAssetCategory.ValueMember = "AssetCatMasID";

            cmbRecoveryType.DataSource = objAssetsCategory.getRecoveryTypeNamesList();
            cmbRecoveryType.DisplayMember = "RecoveryTypeName";
            cmbRecoveryType.ValueMember = "RecoveryTypeID";

            cmbAssetCurrentStatus.DataSource = objAssetsCategory.getCurrentStatusNamesList();
            cmbAssetCurrentStatus.DisplayMember = "CurrentAssetStatusName";
            cmbAssetCurrentStatus.ValueMember = "CurrentAssetStatusID";
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
                int AssetID = 0;
                if (lblActionMode.Text == "add")
                {
                    AssetID = objAssetsInfo.InsertAssetInfo(txtAssetCode.Text, txtAssetName.Text, txtAssetDescription.Text, cmbIsActive.Text.Trim() == "Yes" ? true : false, false, cmbAssetCategory.SelectedIndex + 1, chkRecoverable.Checked, chkReturnRequired.Checked, chkRecoverable.Checked, cmbRecoveryType.SelectedIndex + 1, chkAffectsPayroll.Checked, cmbPayrollAffectType.Text, 0, cmbAssetCurrentStatus.SelectedIndex + 1);
                    if (AssetID > 0)
                    {
                        AssetID = objAssetsInfo.InsertAssetMoreInfo(AssetID, txtSerialNumber.Text.ToString(), txtModelNumber.Text.ToString(), txtManufacturerInfo.Text.ToString(), "", dtPurchaseDate.Value, 0, txtVenderName.Text.ToString(), txtInvoiceNumber.Text.ToString(), "", chkHasWarranty.Checked, dtWarrantyStartDate.Value, dtWarrantyEndDate.Value, dtLastServiceDate.Value, dtNextServiceDate.Value);

                        MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (lblActionMode.Text == "modify")
                {
                    AssetID = objAssetsInfo.UpdateAssetInfo(Convert.ToInt32(lblAsssetID.Text.ToString()), txtAssetCode.Text, txtAssetName.Text, txtAssetDescription.Text, cmbIsActive.Text.Trim() == "Yes" ? true : false, false, cmbAssetCategory.SelectedIndex + 1, chkRecoverable.Checked, chkReturnRequired.Checked, chkRecoverable.Checked, cmbRecoveryType.SelectedIndex + 1, chkAffectsPayroll.Checked, cmbPayrollAffectType.Text, 0, cmbAssetCurrentStatus.SelectedIndex + 1);
                    if (AssetID > 0)
                    {
                        AssetID = objAssetsInfo.UpdateAssetMoreInfo(1, AssetID, txtSerialNumber.Text.ToString(), txtModelNumber.Text.ToString(), txtManufacturerInfo.Text.ToString(), "", dtPurchaseDate.Value, 0, txtVenderName.Text.ToString(), txtInvoiceNumber.Text.ToString(), "", chkHasWarranty.Checked, dtWarrantyStartDate.Value, dtWarrantyEndDate.Value, dtLastServiceDate.Value, dtNextServiceDate.Value);

                        MessageBox.Show("Details updated successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

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

            if (string.IsNullOrEmpty(txtAssetCode.Text))
            {
                validationStatus = false;
                errValidator.SetError(this.txtAssetCode, txtAssetCode.Tag?.ToString() ?? "Contact Person Name should not be blank.");
            }

            return validationStatus;
        }

        public void clearControls()
        {
            lblAsssetID.Text = "";
            lblAssetMasMoreDetID.Text = "";
            txtAssetCode.Enabled = false;
            txtAssetCode.Text = "";
            txtAssetCode.ReadOnly = true;
            txtAssetName.Text = "";
            cmbAssetCategory.DataSource = null;
            cmbIsActive.DataSource = null;
            cmbAssetCurrentStatus.DataSource = null;
            chkCriticalAsset.Checked = false;
            chkReturnRequired.Checked = false;
            chkRecoverable.Checked = false;
            cmbRecoveryType.DataSource = null;
            chkAffectsPayroll.Checked = false;
            cmbPayrollAffectType.DataSource = null;

            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.SelectedIndex = 0;

            txtModelNumber.Text = "";
            txtSerialNumber.Text = "";
            txtManufacturerInfo.Text = "";
            txtVenderName.Text = "";
            txtInvoiceNumber.Text = "";
            dtPurchaseDate.Value = DateTime.Today;
            dtWarrantyStartDate.Value = DateTime.Today;
            dtWarrantyEndDate.Value = DateTime.Today;

            dtLastServiceDate.Value = DateTime.Today;
            dtNextServiceDate.Value = DateTime.Today;

            tabControl1.SelectedIndex = 0;
            tabControl1.Enabled = false;
            lnkViewAuditLog.Visible = false;
        }

        public void enableControls()
        {
            txtAssetCode.Enabled = false;
            txtAssetCode.ReadOnly = true;
            txtAssetName.Enabled = true;
            txtAssetDescription.Enabled = true;
            cmbAssetCategory.Enabled = true;
            cmbIsActive.Enabled = true;
            cmbAssetCurrentStatus.Enabled = true;
            chkCriticalAsset.Enabled = true;
            chkReturnRequired.Enabled = true;
            chkRecoverable.Enabled = true;
            cmbRecoveryType.Enabled = true;
            chkAffectsPayroll.Enabled = true;
            cmbPayrollAffectType.Enabled = true;

            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.Enabled = true;

            txtModelNumber.Enabled = true;
            txtSerialNumber.Enabled = true;
            txtManufacturerInfo.Enabled = true;
            txtVenderName.Enabled = true;
            dtPurchaseDate.Enabled = true;
            txtInvoiceNumber.Enabled = true;
            chkHasWarranty.Enabled = true;
            dtWarrantyStartDate.Enabled = true;
            dtWarrantyEndDate.Enabled = true;
            dtLastServiceDate.Enabled = true;
            dtNextServiceDate.Enabled = true;
            tabControl1.Enabled = true;
        }

        public void disableControls()
        {
            txtAssetCode.Enabled = false;
            txtAssetCode.ReadOnly = true;
            txtAssetName.Enabled = false;
            txtAssetDescription.Enabled = false;
            cmbAssetCategory.Enabled = false;
            cmbIsActive.Enabled = false;
            cmbAssetCurrentStatus.Enabled = false;
            chkCriticalAsset.Enabled = false;
            chkReturnRequired.Enabled = false;
            chkRecoverable.Enabled = false;
            cmbRecoveryType.Enabled = false;
            chkAffectsPayroll.Enabled = false;
            cmbPayrollAffectType.Enabled = false;

            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.Enabled = false;

            txtModelNumber.Enabled = false;
            txtSerialNumber.Enabled = false;
            txtManufacturerInfo.Enabled = false;
            txtVenderName.Enabled = false;
            dtPurchaseDate.Enabled = false;
            txtInvoiceNumber.Enabled = false;
            chkHasWarranty.Enabled = false;
            dtWarrantyStartDate.Enabled = false;
            dtWarrantyEndDate.Enabled = false;
            dtLastServiceDate.Enabled = false;
            dtNextServiceDate.Enabled = false;
        }

        public void onGenerateButtonClick()
        {
            lblActionMode.Text = "add";
            lblAsssetID.Text = "";
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
            lblAsssetID.Text = "";
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
            lblAsssetID.Text = "";
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
            lblAsssetID.Text = "";
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
            lblAsssetID.Text = "";
            btnSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void displaySelectedValuesOnUI(AssetInfoListing AssetInfo)
        {
            if (AssetInfo.AssetID == 0)
                return;

            AssetInfo objSelectedAssetInfo = new AssetInfo();
            lblAsssetID.Text = AssetInfo.AssetID.ToString();
            objSelectedAssetInfo = objAssetsInfo.getAssetInfo(Convert.ToInt32(lblAsssetID.Text.ToString()));
            lblAsssetID.Text = objSelectedAssetInfo.AssetID.ToString();
            txtAssetCode.Text = objSelectedAssetInfo.AssetCode.ToString();
            txtAssetName.Text = objSelectedAssetInfo.AssetName.ToString();
            txtAssetDescription.Text = objSelectedAssetInfo.AssetDescription.ToString();
            cmbIsActive.Text = objSelectedAssetInfo.IsActive ? "Yes" : "No";
            cmbAssetCategory.SelectedIndex = objSelectedAssetInfo.AssetCatMasID - 1;
            
            cmbAssetCurrentStatus.SelectedIndex = objSelectedAssetInfo.CurrentAssetStatusID - 1;
            chkCriticalAsset.Checked = Convert.ToBoolean(objSelectedAssetInfo.IsCriticalAsset.ToString());
            chkReturnRequired.Checked = Convert.ToBoolean(objSelectedAssetInfo.IsRequireReturn.ToString());
            chkRecoverable.Checked = Convert.ToBoolean(objSelectedAssetInfo.IsRecoverable.ToString());
            cmbRecoveryType.SelectedIndex = objSelectedAssetInfo.RecoveryTypeID - 1;
            chkAffectsPayroll.Checked = Convert.ToBoolean(objSelectedAssetInfo.AffectsPayroll.ToString());
            //cmbPayrollAffectType.SelectedIndex = objSelectedAssetInfo.PayrollImpactTypeID - 1;

            AssetMoreInfo objSelectedAssetMoreInfo = objAssetsInfo.getAssetMoreInfo(Convert.ToInt32(lblAsssetID.Text.ToString()));
            lblAssetMasMoreDetID.Text = objSelectedAssetMoreInfo.AssetMasMoreDetID.ToString();
            txtModelNumber.Text = objSelectedAssetMoreInfo.ModelNumber.ToString();
            txtSerialNumber.Text = objSelectedAssetMoreInfo.SerialNumber.ToString();
            txtManufacturerInfo.Text = objSelectedAssetMoreInfo.ManufacturerInfo.ToString();
            txtVenderName.Text = objSelectedAssetMoreInfo.VendorName.ToString();
            dtPurchaseDate.Value = Convert.ToDateTime(objSelectedAssetMoreInfo.PurchaseDate.ToString("dd-MMM-yyyy"));
            txtInvoiceNumber.Text = objSelectedAssetMoreInfo.InvoiceNumber.ToString();
            chkHasWarranty.Checked = Convert.ToBoolean(objSelectedAssetMoreInfo.HasWarranty.ToString());
            dtWarrantyStartDate.Value = Convert.ToDateTime(objSelectedAssetMoreInfo.WarrantyStartDate.ToString("dd-MMM-yyyy"));
            dtWarrantyEndDate.Value = Convert.ToDateTime(objSelectedAssetMoreInfo.WarrantyEndDate.ToString("dd-MMM-yyyy"));
            dtLastServiceDate.Value = Convert.ToDateTime(objSelectedAssetMoreInfo.LastServiceDate.ToString("dd-MMM-yyyy"));
            dtNextServiceDate.Value = Convert.ToDateTime(objSelectedAssetMoreInfo.NextServiceDate.ToString("dd-MMM-yyyy"));

            lnkViewAuditLog.Visible = true;
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

            cmbAssetCategory.DataSource = objAssetsCategory.getAssetsCategoryNamesList(Convert.ToInt32(objTempClientFinYearInfo.ClientID));
            cmbAssetCategory.DisplayMember = "AssetName";
            cmbAssetCategory.ValueMember = "AssetCatMasID";

            cmbRecoveryType.DataSource = objAssetsCategory.getRecoveryTypeNamesList();
            cmbRecoveryType.DisplayMember = "RecoveryTypeName";
            cmbRecoveryType.ValueMember = "RecoveryTypeID";

            cmbAssetCurrentStatus.DataSource = objAssetsCategory.getCurrentStatusNamesList();
            cmbAssetCurrentStatus.DisplayMember = "CurrentAssetStatusName";
            cmbAssetCurrentStatus.ValueMember = "CurrentAssetStatusID";
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

                cmbAssetCategory.DataSource = objAssetsCategory.getAssetsCategoryNamesList(Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                cmbAssetCategory.DisplayMember = "AssetName";
                cmbAssetCategory.ValueMember = "AssetCatMasID";

                cmbRecoveryType.DataSource = objAssetsCategory.getRecoveryTypeNamesList();
                cmbRecoveryType.DisplayMember = "RecoveryTypeName";
                cmbRecoveryType.ValueMember = "RecoveryTypeID";

                cmbAssetCurrentStatus.DataSource = objAssetsCategory.getCurrentStatusNamesList();
                cmbAssetCurrentStatus.DisplayMember = "CurrentAssetStatusName";
                cmbAssetCurrentStatus.ValueMember = "CurrentAssetStatusID";
            }
            else if (lblActionMode.Text == "delete")
            {
                if (MessageBox.Show("The selected record will be deleted. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int affectedRows = objAssetsInfo.DeleteAssetMoreInfo(Convert.ToInt16(lblAsssetID.Text.Trim()));
                    if (affectedRows > 0)
                    {
                        affectedRows = objAssetsInfo.DeleteAssetInfo(Convert.ToInt16(lblAsssetID.Text.Trim()));
                        MessageBox.Show("Details deleted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                onCancelButtonClick();
                disableControls();
                clearControls();
                cmbIsActive.SelectedIndex = 0;
                lblActionMode.Text = "";
                errValidator.Clear();
            }
        }

        private void frmAssetsInfo_KeyDown(object sender, KeyEventArgs e)
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

        }

        private void chkEnablePayrollStatutory_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void chkEnableProvidentFund_CheckedChanged(object sender, EventArgs e)
        {

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

        }

        private void optEmpPFPercentage_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void optEmpPFFixedAmount_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void optEmprPFPercentage_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void optEmprPFFixedAmount_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void optEmprEPSPercentage_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void optEmprPESFixedAmount_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void optEmpESIPercentage_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void optEmpESIFixedAmount_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void optEmprESIPercentage_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void optEmprESIFixedAmount_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkEnablePayrollStatutory_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void chkHasWarranty_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHasWarranty.Checked)
            {
                dtWarrantyStartDate.Enabled = true;
                dtWarrantyEndDate.Enabled = true;
            }
            else
            {
                dtWarrantyStartDate.Enabled  = false;
                dtWarrantyEndDate.Enabled = false;
            }
        }

        private void lnkViewAuditLog_LinkClicked(object sender, EventArgs e)
        {
            frmAuditLogStatements objAuditLogStatements = new frmAuditLogStatements(Convert.ToInt32(lblAsssetID.Text.ToString()), "AssetInfo", "Assets Information", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
            objAuditLogStatements.ShowDialog(this);
        }
    }
}
