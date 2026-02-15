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
    public partial class frmAdvanceTypeMas : Form
    {
        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        //DALStaffSync.clsEduQalification objEduQalification = new DALStaffSync.clsEduQalification();
        DALStaffSync.clsAdvanceTypeMas objAdvanceTypeMas = new DALStaffSync.clsAdvanceTypeMas();
        DALStaffSync.clsAdvanceTypeConfigInfo objAdvanceTypeConfigInfo = new DALStaffSync.clsAdvanceTypeConfigInfo();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        frmDashboard objDashboard = (frmDashboard) System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();

        public frmAdvanceTypeMas()
        {
            InitializeComponent();
        }

        public frmAdvanceTypeMas(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmAdvanceTypeMas(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
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

        private void frmAdvanceTypeMas_Load(object sender, EventArgs e)
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
            frmAdvanceTypeMasList frmAdvanceTypeMasList = new frmAdvanceTypeMasList(this);
            frmAdvanceTypeMasList.ShowDialog(this);
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
            cmbAdvanceAmountBased.SelectedIndex = 1;
            cmbAdvanceBasedOn.SelectedIndex = 1;
            lblAdvanceTypeID.Text = objGenFunc.getMaxRowCount("AdvanceTypeMas", "AdvanceTypeID").Data.ToString();
            txtAdvanceCode.Text = "ADV-" + (lblAdvanceTypeID.Text.Trim()).ToString().PadLeft(4, '0');
            errValidator.Clear();
        }
        public bool ValidateValuesOnUI()
        {
            bool validationStatus = true;
            errValidator.Clear();

            if (string.IsNullOrEmpty(txtAdvanceTitle.Text))
            {
                validationStatus = false;
                txtAdvanceTitle.Focus();
                errValidator.SetError(this.txtAdvanceTitle, "Please enter Advance Title");
            }
            else if (string.IsNullOrEmpty(cmbIsActive.Text))
            {
                validationStatus = false; 
                cmbIsActive.Focus();
                cmbIsActive.SelectedIndex = 1;
                errValidator.SetError(this.cmbIsActive, "Please select Advance Status");
            }
            else if (string.IsNullOrEmpty(cmbAdvanceBasedOn.Text))
            {
                validationStatus = false; 
                cmbIsActive.Focus();
                cmbIsActive.SelectedIndex = 1;
                errValidator.SetError(this.cmbAdvanceBasedOn, "Please select Advance Based On");
            }
            else if (string.IsNullOrEmpty(cmbAdvanceAmountBased.Text))
            {
                validationStatus = false; 
                cmbIsActive.Focus();
                cmbIsActive.SelectedIndex = 1;
                errValidator.SetError(this.cmbAdvanceAmountBased, "Please select Advance Based On");
            }
            else if (cmbAdvanceAmountBased.Text.ToString().ToLower() == "percentage")
            {
                if (string.IsNullOrEmpty(txtAdvancePercentage.Text) || Convert.ToDecimal(txtAdvancePercentage.Text) <= 0)
                {
                    validationStatus = false; 
                    txtAdvancePercentage.Focus();
                    txtAdvanceAmountFixed.Text = "0";
                    errValidator.SetError(this.txtAdvancePercentage, "Please enter valid Advance Percentage");
                }
            }
            else if (cmbAdvanceAmountBased.Text.ToString().ToLower() == "fixed")
            {
                if (string.IsNullOrEmpty(txtAdvanceAmountFixed.Text) || Convert.ToDecimal(txtAdvanceAmountFixed.Text) <= 0)
                {
                    validationStatus = false; 
                    txtAdvanceAmountFixed.Focus();
                    txtAdvancePercentage.Text = "0";
                    errValidator.SetError(this.txtAdvanceAmountFixed, "Please enter valid Advance Amount");
                }
            }
            else if (string.IsNullOrEmpty(txtMaxTenure.Text) || Convert.ToDecimal(txtMaxTenure.Text) < 0)
            {
                validationStatus = false; 
                txtMaxTenure.Focus();
                errValidator.SetError(this.txtMaxTenure, "Please enter valid Max. Tenure");
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

            if (!ValidateValuesOnUI())
            {
                this.Cursor = Cursors.Default;
                return;
            }

            if (lblActionMode.Text == "add")
            {
                int newID = objAdvanceTypeMas.InsertAdvanceType(txtAdvanceCode.Text.Trim(), txtAdvanceTitle.Text.Trim(), cmbIsActive.Text.Trim() == "Yes" ? true : false, false, CurrentUser.ClientID);
                if (newID > 0)
                    objAdvanceTypeConfigInfo.InsertAdvanceTypeConfig(newID, chkAutoDeductFromSalary.Checked, cmbAdvanceBasedOn.Text.ToString(), cmbAdvanceAmountBased.Text.ToString(), Convert.ToDecimal(txtAdvancePercentage.Text.ToString()), Convert.ToDecimal(txtAdvanceAmountFixed.Text.ToString()), chkIncludeAsDeductionInSalary.Checked, chkRecoveryRequired.Checked, chkAutoDeductFromNextSaslary.Checked, chkInterestRequired.Checked, chkApprovalNeeded.Checked, chkAllowPause.Checked, chkWaiverAllowed.Checked, Convert.ToDecimal(txtMaxTenure.Text.ToString()));
                if (newID > 0)
                    MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (lblActionMode.Text == "modify")
            {
                int affectedRows = objAdvanceTypeMas.UpdateAdvanceType(Convert.ToInt32(lblAdvanceTypeID.Text.ToString()), txtAdvanceCode.Text.Trim(), txtAdvanceTitle.Text.Trim(), cmbIsActive.Text.Trim() == "Yes" ? true : false, false);
                if (affectedRows > 0)
                    objAdvanceTypeConfigInfo.UpdateAdvanceTypeConfig(Convert.ToInt32(lblAdvanceTypeConfigID.Text.ToString()), Convert.ToInt32(lblAdvanceTypeID.Text.ToString()), chkAutoDeductFromSalary.Checked, cmbAdvanceBasedOn.Text.ToString(), cmbAdvanceAmountBased.Text.ToString(), Convert.ToDecimal(txtAdvancePercentage.Text.ToString()), Convert.ToDecimal(txtAdvanceAmountFixed.Text.ToString()), chkIncludeAsDeductionInSalary.Checked, chkRecoveryRequired.Checked, chkAutoDeductFromNextSaslary.Checked, chkInterestRequired.Checked, chkApprovalNeeded.Checked, chkAllowPause.Checked, chkWaiverAllowed.Checked, Convert.ToDecimal(txtMaxTenure.Text.ToString()));

                if (affectedRows > 0)
                    MessageBox.Show("Details updated successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            objTempCurrentlyLoggedInUserInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objTempCurrentlyLoggedInUserInfo.EmpID.ToString()));

            onSaveButtonClick();
            disableControls();
            clearControls();
            errValidator.Clear();

        }

        public void clearControls()
        {
            lblAdvanceTypeID.Text = "";
            lblAdvanceTypeConfigID.Text = "";
            txtAdvanceCode.Text = "";
            txtAdvanceCode.ReadOnly = true;
            txtAdvanceTitle.Text = "";
            txtAdvancePercentage.Text = "0";
            txtAdvanceAmountFixed.Text = "0";
            txtMaxTenure.Text = "0";
            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.SelectedIndex = 0;

            cmbAdvanceBasedOn.Items.Clear();
            cmbAdvanceBasedOn.Items.Add("");
            cmbAdvanceBasedOn.Items.Add("Net Salary");
            cmbAdvanceBasedOn.Items.Add("Gross Salary");
            cmbAdvanceBasedOn.SelectedIndex = 0;

            cmbAdvanceAmountBased.Items.Clear();
            cmbAdvanceAmountBased.Items.Add("");
            cmbAdvanceAmountBased.Items.Add("Percentage");
            cmbAdvanceAmountBased.Items.Add("Fixed");
            cmbAdvanceAmountBased.SelectedIndex = 0;

            chkApprovalNeeded.Checked = false;
            chkInterestRequired.Checked = false;
            chkAutoDeductFromSalary.Checked = false;
            chkAutoDeductFromNextSaslary.Checked = false;
            chkAllowPause.Checked = false;   
            chkRecoveryRequired.Checked = false;
            chkIncludeAsDeductionInSalary.Checked = false;
            chkWaiverAllowed.Checked = false;
            chkWaiverAllowed.Checked = false;
        }

        public void enableControls()
        {
            txtAdvanceCode.Enabled = false;
            txtAdvanceCode.ReadOnly = true;
            txtAdvanceTitle.Enabled = true;
            txtAdvanceAmountFixed.Enabled = false;
            txtAdvancePercentage.Enabled = false;
            txtMaxTenure.Enabled = true;
            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.SelectedIndex = 0;
            cmbIsActive.Enabled = true;

            cmbAdvanceBasedOn.Items.Clear();
            cmbAdvanceBasedOn.Items.Add("");
            cmbAdvanceBasedOn.Items.Add("Net Salary");
            cmbAdvanceBasedOn.Items.Add("Gross Salary");
            cmbAdvanceBasedOn.SelectedIndex = 0;
            cmbAdvanceBasedOn.Enabled = true;

            cmbAdvanceAmountBased.Items.Clear();
            cmbAdvanceAmountBased.Items.Add("");
            cmbAdvanceAmountBased.Items.Add("Percentage");
            cmbAdvanceAmountBased.Items.Add("Fixed");
            cmbAdvanceAmountBased.SelectedIndex = 0;
            cmbAdvanceAmountBased.Enabled = true;

            chkApprovalNeeded.Enabled = true;
            chkInterestRequired.Enabled = true;
            chkAutoDeductFromSalary.Enabled = true;
            chkAutoDeductFromNextSaslary.Enabled = true;
            chkAllowPause.Enabled = true;
            chkRecoveryRequired.Enabled = true;
            chkIncludeAsDeductionInSalary.Enabled = true;
            chkWaiverAllowed.Enabled = true;
            chkWaiverAllowed.Enabled = true;
        }

        public void disableControls()
        {
            txtAdvanceCode.Enabled = false;
            txtAdvanceCode.ReadOnly = true;
            txtAdvanceTitle.Enabled = false;
            txtAdvanceAmountFixed.Enabled = false;
            txtAdvancePercentage.Enabled = false;
            txtMaxTenure.Enabled = false;
            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.SelectedIndex = 0;
            cmbIsActive.Enabled = false;

            cmbAdvanceBasedOn.Items.Clear();
            cmbAdvanceBasedOn.Items.Add("");
            cmbAdvanceBasedOn.Items.Add("Net Salary");
            cmbAdvanceBasedOn.Items.Add("Gross Salary");
            cmbAdvanceBasedOn.SelectedIndex = 0;
            cmbAdvanceBasedOn.Enabled = false;

            cmbAdvanceAmountBased.Items.Clear();
            cmbAdvanceAmountBased.Items.Add("");
            cmbAdvanceAmountBased.Items.Add("Percentage");
            cmbAdvanceAmountBased.Items.Add("Fixed");
            cmbAdvanceAmountBased.SelectedIndex = 0;
            cmbAdvanceAmountBased.Enabled = false;

            chkApprovalNeeded.Enabled = false;
            chkInterestRequired.Enabled = false;
            chkAutoDeductFromSalary.Enabled = false;
            chkAutoDeductFromNextSaslary.Enabled = false;
            chkAllowPause.Enabled = false;
            chkRecoveryRequired.Enabled = false;
            chkIncludeAsDeductionInSalary.Enabled = false;
            chkWaiverAllowed.Enabled = false;
            chkWaiverAllowed.Enabled = false;

            chkApprovalNeeded.Checked = false;
            chkInterestRequired.Checked = false;
            chkAutoDeductFromSalary.Checked = false;
            chkAutoDeductFromNextSaslary.Checked = false;
            chkAllowPause.Checked = false;
            chkRecoveryRequired.Checked = false;
            chkIncludeAsDeductionInSalary.Checked = false;
            chkWaiverAllowed.Checked = false;
            chkWaiverAllowed.Checked = false;
        }

        public void onGenerateButtonClick()
        {
            lblActionMode.Text = "add";
            lblAdvanceTypeID.Text = "";
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
            lblAdvanceTypeID.Text = "";
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
            lblAdvanceTypeID.Text = "";
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
            lblAdvanceTypeID.Text = "";
            lblAdvanceTypeConfigID.Text = "";
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
            lblAdvanceTypeID.Text = "";
            lblAdvanceTypeConfigID.Text = "";
            btnSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void displaySelectedValuesOnUI(AdvanceTypesModel objAdvanceTypesModel)
        {

            lblAdvanceTypeID.Text = objAdvanceTypesModel.AdvanceTypeID.ToString();
            txtAdvanceCode.Text = objAdvanceTypesModel.AdvanceTypeCode.ToString();
            txtAdvanceTitle.Text = objAdvanceTypesModel.AdvanceTypeTitle.ToString();
            cmbIsActive.Text = objAdvanceTypesModel.IsActive ? "Yes" : "No";

            AdvanceTypeConfigModel objAdvanceTypeConfigModel = objAdvanceTypeConfigInfo.GetAdvanceTypeConfigByID(objAdvanceTypesModel.AdvanceTypeID);
            lblAdvanceTypeConfigID.Text = objAdvanceTypeConfigModel.AdvanceTypeConfigID.ToString();
            cmbAdvanceBasedOn.Text = objAdvanceTypeConfigModel.BasedOnNetOrGross.ToString();
            cmbAdvanceAmountBased.Text = objAdvanceTypeConfigModel.MaxPerOfNetOrGross.ToString();
            txtAdvancePercentage.Text = objAdvanceTypeConfigModel.MaxPercentage.ToString();
            txtAdvanceAmountFixed.Text = objAdvanceTypeConfigModel.MaxFixed.ToString();
            chkRecoveryRequired.Checked = objAdvanceTypeConfigModel.RecoveryRequired;
            chkAutoDeductFromSalary.Checked = objAdvanceTypeConfigModel.AutoDeductFromSalary;
            chkAutoDeductFromSalary.Enabled = chkRecoveryRequired.Checked == true || chkAutoDeductFromSalary.Checked == true ? true : false;
            chkIncludeAsDeductionInSalary.Checked = objAdvanceTypeConfigModel.IncludeInSalary;
            chkIncludeAsDeductionInSalary.Enabled = chkRecoveryRequired.Checked == true || chkIncludeAsDeductionInSalary.Checked == true ? true : false;
            chkAutoDeductFromNextSaslary.Checked = objAdvanceTypeConfigModel.AutoRecoveryFromNextSalary;
            chkAutoDeductFromNextSaslary.Enabled = chkRecoveryRequired.Checked == true || chkAutoDeductFromNextSaslary.Checked == true ? true : false;
            chkInterestRequired.Checked = objAdvanceTypeConfigModel.InterestRequired;
            chkApprovalNeeded.Checked = objAdvanceTypeConfigModel.ApprovalRequired;
            chkAllowPause.Checked = objAdvanceTypeConfigModel.AllowPause;
            chkWaiverAllowed.Checked = objAdvanceTypeConfigModel.WaiverAllowed;
            txtMaxTenure.Text = objAdvanceTypeConfigModel.MaxTenure.ToString();
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
            cmbAdvanceAmountBased.SelectedIndex = 1;
            cmbAdvanceBasedOn.SelectedIndex = 1;
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
                    int affectedRows = objAdvanceTypeConfigInfo.DeleteAdvanceTypeConfig(Convert.ToInt16(lblAdvanceTypeConfigID.Text.Trim()));
                    if (affectedRows > 0) 
                        affectedRows = objAdvanceTypeMas.DeleteAdvanceType(Convert.ToInt16(lblAdvanceTypeID.Text.Trim()));
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

        private void frmAdvanceTypeMas_KeyDown(object sender, KeyEventArgs e)
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

        private void cmbAdvanceAmountBased_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbAdvanceAmountBased.Text.ToString().ToLower() == "percentage")
            {
                txtAdvancePercentage.Enabled = true;
                txtAdvanceAmountFixed.Enabled = false;
                txtAdvanceAmountFixed.Text = "0";
            }
            else if (cmbAdvanceAmountBased.Text.ToString().ToLower() == "fixed")
            {
                txtAdvancePercentage.Enabled = false;
                txtAdvancePercentage.Text = "0";
                txtAdvanceAmountFixed.Enabled = true;
            }
            else
            {
                txtAdvancePercentage.Enabled = false;
                txtAdvancePercentage.Text = "0";
                txtAdvanceAmountFixed.Enabled = false;
                txtAdvanceAmountFixed.Text = "0";
            }
        }

        private void chkRecoveryRequired_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRecoveryRequired.Checked)
            {
                chkAutoDeductFromSalary.Checked = true;
                chkAutoDeductFromNextSaslary.Checked = true;
                chkIncludeAsDeductionInSalary.Checked = true;
                chkAutoDeductFromSalary.Enabled = true;
                chkAutoDeductFromNextSaslary.Enabled = true;
                chkIncludeAsDeductionInSalary.Enabled = true;
            }
            else
            {
                chkAutoDeductFromSalary.Checked = false;
                chkAutoDeductFromNextSaslary.Checked = false;
                chkIncludeAsDeductionInSalary.Checked = false;
                chkAutoDeductFromSalary.Enabled = false;
                chkAutoDeductFromNextSaslary.Enabled = false;
                chkIncludeAsDeductionInSalary.Enabled = false;
            }
        }
    }
}
