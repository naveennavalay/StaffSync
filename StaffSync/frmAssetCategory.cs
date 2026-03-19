using StaffSync.StaffsyncDBDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using StaffSync.StaffsyncDBDTSetTableAdapters;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ModelStaffSync;

namespace StaffSync
{
    public partial class frmAssetCategory : Form
    {
        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        DALStaffSync.clsAssetsCategory objAssetsCategory = new DALStaffSync.clsAssetsCategory();
        DALStaffSync.clsAuditLog objAuditLog = new DALStaffSync.clsAuditLog(); 
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        frmDashboard objDashboard = (frmDashboard) System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();

        private Dictionary<string, object> _originalValues;

        public frmAssetCategory()
        {
            InitializeComponent();
        }

        public frmAssetCategory(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmAssetCategory(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
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

        private void frmAssetCategory_Load(object sender, EventArgs e)
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
            frmAssetCategoryList frmAssetCategoryList = new frmAssetCategoryList(this, objTempClientFinYearInfo.ClientID);
            frmAssetCategoryList.ShowDialog(this);
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
            lblCategoryID.Text = objGenFunc.getMaxRowCount("AssetCategoryMas", "AssetCatMasID").Data.ToString();
            txtAssetCode.Text = "ACC-" + (lblCategoryID.Text.Trim()).ToString().PadLeft(4, '0');
            errValidator.Clear();
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            string strValidationMessage = objLogin.ValidateUserRolesAndResponsibilitiesInfo(objTempCurrentlyLoggedInUserInfo.EmpID, "");
            if (strValidationMessage != "Success")
            {
                MessageBox.Show(strValidationMessage, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (string.IsNullOrEmpty(txtAssetName.Text))
            {
                txtAssetName.Focus();
                errValidator.SetError(this.txtAssetName, "Please enter Asset Name");
            }
            else if (string.IsNullOrEmpty(txtAssetDescription.Text))
            {
                txtAssetDescription.Focus();
                errValidator.SetError(this.txtAssetDescription, "Please enter Asset Description");
            }
            else if (string.IsNullOrEmpty(cmbIsActive.Text))
            {
                cmbIsActive.Focus();
                cmbIsActive.SelectedIndex = 1;
                errValidator.SetError(this.cmbIsActive, "Please select Asset Status");
            }
            else
            {
                var updatedValues = AuditLogger.getOriginalValues(this);
                var onlyChangedValues = (dynamic)null;

                string strActionStatement = "";

                if (lblActionMode.Text == "add")
                {
                    strActionStatement = "AssetCategoryNewUpdates";
                    onlyChangedValues = AuditLogger.getUpdatedValues(_originalValues, updatedValues, true);

                    int newID = objAssetsCategory.InsertAssetCategoryInfo(txtAssetCode.Text.Trim(), txtAssetName.Text.Trim(), txtAssetDescription.Text.Trim(), "", cmbIsActive.Text.Trim() == "Yes" ? true : false, false, Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                    if (newID > 0)
                        MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else if (lblActionMode.Text == "modify")
                {
                    strActionStatement = "AssetCategoryExistingUpdates";
                    onlyChangedValues = AuditLogger.getUpdatedValues(_originalValues, updatedValues, false);

                    int affectedRows = objAssetsCategory.UpdateAssetCategoryInfo(Convert.ToInt16(lblCategoryID.Text.Trim()), txtAssetCode.Text.Trim(), txtAssetName.Text.Trim(), txtAssetDescription.Text.Trim(), "", cmbIsActive.Text.Trim() == "Yes" ? true : false, false, Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                    if (affectedRows > 0)
                        MessageBox.Show("Details updated successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
               }

                //if (onlyChangedValues.Any())
                {
                    foreach (var changedValues in onlyChangedValues)
                    {
                        objAuditLog.InsertAuditLog(Convert.ToInt32(CurrentUser.EmpID.ToString()), Convert.ToInt32(lblCategoryID.Text.ToString()), changedValues.ToString().Trim(), ModelStaffSync.CurrentUser.EmpName, strActionStatement, Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                    }
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
            txtAssetCode.Text = "";
            txtAssetCode.ReadOnly = true;
            txtAssetName.Text = "";
            txtAssetDescription.Text = "";
            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.SelectedIndex = 0;
            lnkViewAuditLog.Visible = false;
        }

        public void enableControls()
        {
            txtAssetCode.Enabled = true;
            txtAssetCode.ReadOnly = true;
            txtAssetName.Enabled = true;
            txtAssetDescription.Enabled = true;
            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.Enabled = true;
        }

        public void disableControls()
        {
            txtAssetCode.Enabled = false;
            txtAssetCode.ReadOnly = true;
            txtAssetName.Enabled = false;
            txtAssetDescription.Enabled = false;
            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.Enabled = false;
        }

        public void onGenerateButtonClick()
        {
            lblActionMode.Text = "add";
            lblCategoryID.Text = "";
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
            lblCategoryID.Text = "";
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
            lblCategoryID.Text = "";
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
            lblCategoryID.Text = "";
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
            lblCategoryID.Text = "";
            btnSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void displaySelectedValuesOnUI(AssetsCategory AssetsCategoryModel)
        {
            lblCategoryID.Text = AssetsCategoryModel.AssetCatMasID.ToString();
            txtAssetCode.Text = AssetsCategoryModel.AssetCode;
            txtAssetName.Text = AssetsCategoryModel.AssetName;
            txtAssetDescription.Text = AssetsCategoryModel.AssetDescription;
            cmbIsActive.Text = AssetsCategoryModel.IsActive == true ? "Yes" : "No";

            _originalValues = AuditLogger.getOriginalValues(this);
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
            }
            else if (lblActionMode.Text == "delete")
            {
                if (MessageBox.Show("The selected record will be deleted. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int affectedRows = objAssetsCategory.DeleteAssetCategoryInfo(Convert.ToInt16(lblCategoryID.Text.Trim()));
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

        private void frmAssetCategory_KeyDown(object sender, KeyEventArgs e)
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

        private void lnkViewAuditLog_LinkClicked(object sender, EventArgs e)
        {
            frmAuditLogStatements objAuditLogStatements = new frmAuditLogStatements(Convert.ToInt32(lblCategoryID.Text.ToString()), "AssetCategory", "Asset Category", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
            objAuditLogStatements.ShowDialog(this);
        }
    }
}
