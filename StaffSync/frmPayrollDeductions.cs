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
using DALStaffSync;

namespace StaffSync
{
    public partial class frmPayrollDeductions : Form
    {
        //myDBClass objDBClass = new myDBClass();
        //OleDbConnection conn = null;
        //DataSet dtDataset;

        //clsAllowenceInfo objAllowence = new clsAllowenceInfo();
        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        DALStaffSync.clsDeductionsInfo objDeductionInfo = new DALStaffSync.clsDeductionsInfo();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        frmDashboard objDashboard = (frmDashboard) System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();

        public frmPayrollDeductions()
        {
            InitializeComponent();
        }

        public frmPayrollDeductions(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmPayrollDeductions(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
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

        private void frmPayrollDeductions_Load(object sender, EventArgs e)
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
            frmPayrollDeductionsList frmPayrollDeductionsList = new frmPayrollDeductionsList(this);
            frmPayrollDeductionsList.ShowDialog(this);
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
            cmbProrataBased.SelectedIndex = 1;
            cmbProrataBased.SelectedIndex = 1;
            cmbIsFixed.SelectedIndex = 1;
            cmbIsActive.SelectedIndex = 1;
            lblDeductionID.Text = objGenFunc.getMaxRowCount("DeductionHeaderMas", "DedID").Data.ToString();
            txtDedCode.Text = "DDN-" + (lblDeductionID.Text.Trim()).ToString().PadLeft(4, '0');
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
            
            if (string.IsNullOrEmpty(txtDedTitle.Text))
            {
                txtDedTitle.Focus();
                errValidator.SetError(this.txtDedTitle, "Please enter Allowence Title");
            }
            else if (string.IsNullOrEmpty(txtDedDescription.Text))
            {
                txtDedDescription.Focus();
                errValidator.SetError(this.txtDedDescription, "Please enter Allowence Description");
            }
            else if (string.IsNullOrEmpty(cmbIsFixed.Text))
            {
                cmbIsFixed.Focus();
                cmbIsFixed.SelectedIndex = 1;
                errValidator.SetError(this.cmbIsFixed, "Is the Allowence Fixed.?");
            }
            else if (string.IsNullOrEmpty(cmbIsActive.Text))
            {
                cmbIsActive.Focus();
                cmbIsActive.SelectedIndex = 1;
                errValidator.SetError(this.cmbIsActive, "Please select Allowence Status");
            }
            else if (string.IsNullOrEmpty(cmbShowInPayslip.Text))
            {
                cmbShowInPayslip.Focus();
                cmbShowInPayslip.SelectedIndex = 1;
                errValidator.SetError(this.cmbShowInPayslip, "Show In Payslip.?");
            }
            else if (string.IsNullOrEmpty(cmbProrataBased.Text))
            {
                cmbProrataBased.Focus();
                cmbProrataBased.SelectedIndex = 1;
                errValidator.SetError(this.cmbProrataBased, "Consider calculation based on Pro-rata based.?");
            }
            else if (string.IsNullOrEmpty(txtMaxCap.Text))
            {
                txtMaxCap.Focus();
                errValidator.SetError(this.txtMaxCap, "Please enter Maximum Cap Amount Limit");
            }
            else
            {
                if (lblActionMode.Text == "add")
                {
                    int affectedRows = objDeductionInfo.InsertDeduction(txtDedCode.Text.Trim(), txtDedTitle.Text.Trim(), txtDedDescription.Text.Trim(), cmbIsFixed.Text.Trim() == "Yes" ? true : false, cmbIsActive.Text.Trim() == "Yes" ? true : false, false, Convert.ToDecimal(txtMaxCap.Text.ToString()), cmbShowInPayslip.Text.Trim() == "Yes" ? true : false, cmbProrataBased.Text.Trim() == "Yes" ? true : false);
                    if (affectedRows > 0)
                        MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (lblActionMode.Text == "modify")
                {
                    int affectedRows = objDeductionInfo.UpdateDeduction(Convert.ToInt16(lblDeductionID.Text.Trim()), txtDedCode.Text.Trim(), txtDedTitle.Text.Trim(), txtDedDescription.Text.Trim(), cmbIsFixed.Text.Trim() == "Yes" ? true : false, cmbIsActive.Text.Trim() == "Yes" ? true : false, false, Convert.ToDecimal(txtMaxCap.Text.ToString()), cmbShowInPayslip.Text.Trim() == "Yes" ? true : false, cmbProrataBased.Text.Trim() == "Yes" ? true : false);
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
            txtDedCode.Text = "";
            txtDedCode.ReadOnly = true;
            txtDedTitle.Text = "";
            txtDedDescription.Text = "";

            cmbShowInPayslip.Items.Clear();
            cmbShowInPayslip.Items.Add("");
            cmbShowInPayslip.Items.Add("Yes");
            cmbShowInPayslip.Items.Add("No");
            cmbShowInPayslip.SelectedIndex = 0;

            cmbProrataBased.Items.Clear();
            cmbProrataBased.Items.Add("");
            cmbProrataBased.Items.Add("Yes");
            cmbProrataBased.Items.Add("No");
            cmbProrataBased.SelectedIndex = 0;

            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.SelectedIndex = 0;

            cmbIsFixed.Items.Clear();
            cmbIsFixed.Items.Add("");
            cmbIsFixed.Items.Add("Yes");
            cmbIsFixed.Items.Add("No");
            cmbIsFixed.SelectedIndex = 0;
            txtMaxCap.Text = "0.00";
        }

        public void enableControls()
        {
            txtDedCode.Enabled = true;
            txtDedCode.ReadOnly = true;
            txtDedTitle.Enabled = true;
            txtDedDescription.Enabled = true;

            cmbShowInPayslip.Items.Clear();
            cmbShowInPayslip.Items.Add("");
            cmbShowInPayslip.Items.Add("Yes");
            cmbShowInPayslip.Items.Add("No");
            cmbShowInPayslip.SelectedIndex = 0;

            cmbProrataBased.Items.Clear();
            cmbProrataBased.Items.Add("");
            cmbProrataBased.Items.Add("Yes");
            cmbProrataBased.Items.Add("No");
            cmbProrataBased.SelectedIndex = 0;

            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.Enabled = true;
            cmbIsActive.SelectedIndex = 1;

            cmbIsFixed.Items.Clear();
            cmbIsFixed.Items.Add("");
            cmbIsFixed.Items.Add("Yes");
            cmbIsFixed.Items.Add("No");
            cmbIsFixed.Enabled = true;
            cmbIsFixed.SelectedIndex = 1;
            txtMaxCap.Enabled = true;
        }

        public void disableControls()
        {
            txtDedCode.Enabled = false;
            txtDedCode.ReadOnly = true;
            txtDedTitle.Enabled = false;
            txtDedDescription.Enabled = false;

            cmbShowInPayslip.Items.Clear();
            cmbShowInPayslip.Items.Add("");
            cmbShowInPayslip.Items.Add("Yes");
            cmbShowInPayslip.Items.Add("No");
            cmbShowInPayslip.SelectedIndex = 0;

            cmbProrataBased.Items.Clear();
            cmbProrataBased.Items.Add("");
            cmbProrataBased.Items.Add("Yes");
            cmbProrataBased.Items.Add("No");
            cmbProrataBased.SelectedIndex = 0;

            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.Enabled = false;

            cmbIsFixed.Items.Clear();
            cmbIsFixed.Items.Add("");
            cmbIsFixed.Items.Add("Yes");
            cmbIsFixed.Items.Add("No");
            cmbIsFixed.Enabled = false;
            txtMaxCap.Enabled = false;
        }

        public void onGenerateButtonClick()
        {
            lblActionMode.Text = "add";
            lblDeductionID.Text = "";
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
            lblDeductionID.Text = "";
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
            lblDeductionID.Text = "";
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
            lblDeductionID.Text = "";
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
            lblDeductionID.Text = "";
            btnSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void displaySelectedValuesOnUI(DeductionModel DeductionModel)
        {
            lblDeductionID.Text = DeductionModel.DedID.ToString();
            txtDedCode.Text = DeductionModel.DedCode;
            txtDedTitle.Text = DeductionModel.DedTitle;
            txtDedDescription.Text = DeductionModel.DedDescription;
            cmbIsActive.Text = DeductionModel.IsActive == true ? "Yes" : "No";
            cmbIsFixed.Text = DeductionModel.IsFixed == true ? "Yes" : "No";
            cmbShowInPayslip.Text = DeductionModel.VisibleInPayslip == true ? "Yes" : "No";
            cmbProrataBased.Text = DeductionModel.ProrataBasis == true ? "Yes" : "No";
            txtMaxCap.Text = DeductionModel.MaxCap.ToString();
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
            cmbIsFixed.SelectedIndex = 1;
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
                cmbIsFixed.SelectedIndex = 1;
                cmbIsActive.SelectedIndex = 1;
            }
            else if (lblActionMode.Text == "delete")
            {
                if (MessageBox.Show("The selected record will be deleted. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int affectedRows = objDeductionInfo.DeleteDeduction(Convert.ToInt16(lblDeductionID.Text.Trim()));
                    if (affectedRows > 0)
                        MessageBox.Show("Details deleted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                onCancelButtonClick();
                disableControls();
                clearControls();
                cmbIsFixed.SelectedIndex = 0;
                cmbIsActive.SelectedIndex = 0;
                lblActionMode.Text = "";
                errValidator.Clear();
            }
        }

        private void frmPayrollDeductions_KeyDown(object sender, KeyEventArgs e)
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
    }
}
