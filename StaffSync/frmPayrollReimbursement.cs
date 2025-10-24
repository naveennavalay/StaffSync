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
    public partial class frmReimbursement : Form
    {
        //myDBClass objDBClass = new myDBClass();
        //OleDbConnection conn = null;
        //DataSet dtDataset;

        //clsAllowenceInfo objAllowence = new clsAllowenceInfo();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        DALStaffSync.clsReimbursement objReimbursement = new DALStaffSync.clsReimbursement();
        frmDashboard objDashboard = (frmDashboard) System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();

        public frmReimbursement()
        {
            InitializeComponent();
        }

        public frmReimbursement(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
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

        private void frmReimbursement_Load(object sender, EventArgs e)
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
            frmPayrollReimbursementList frmPayrollReimbursementList = new frmPayrollReimbursementList(this);
            frmPayrollReimbursementList.ShowDialog(this);
        }

        private void btnCloseMe_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenerateDetails_Click(object sender, EventArgs e)
        {
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
            cmbIsFixed.SelectedIndex = 1;
            cmbIsActive.SelectedIndex = 1;
            lblReimbursementID.Text = objGenFunc.getMaxRowCount("ReimbursementHeaderMas", "ReimbID").Data.ToString();
            txtReimbCode.Text = "RIM-" + (lblReimbursementID.Text.Trim()).ToString().PadLeft(4, '0');
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
            
            if (string.IsNullOrEmpty(txtReimbTitle.Text))
            {
                txtReimbTitle.Focus();
                errValidator.SetError(this.txtReimbTitle, "Please enter Reimbursement Title");
            }
            else if (string.IsNullOrEmpty(txtReimbDescription.Text))
            {
                txtReimbDescription.Focus();
                errValidator.SetError(this.txtReimbDescription, "Please enter Reimbursement Description");
            }
            else if (string.IsNullOrEmpty(cmbIsFixed.Text))
            {
                cmbIsFixed.Focus();
                cmbIsFixed.SelectedIndex = 1;
                errValidator.SetError(this.cmbIsFixed, "Is the Reimbursement Fixed");
            }
            else if (string.IsNullOrEmpty(cmbIsActive.Text))
            {
                cmbIsActive.Focus();
                cmbIsActive.SelectedIndex = 1;
                errValidator.SetError(this.cmbIsActive, "Please select Reimbursement Status");
            }
            else
            {
                if (lblActionMode.Text == "add")
                {
                    int affectedRows = objReimbursement.InsertReimbursement(txtReimbCode.Text.Trim(), txtReimbTitle.Text.Trim(), txtReimbDescription.Text.Trim(), cmbIsFixed.Text.Trim() == "Yes" ? true : false, cmbIsActive.Text.Trim() == "Yes" ? true : false, false);
                    if (affectedRows > 0)
                        MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (lblActionMode.Text == "modify")
                {
                    int affectedRows = objReimbursement.UpdateReimbursement(Convert.ToInt16(lblReimbursementID.Text.Trim()), txtReimbCode.Text.Trim(), txtReimbTitle.Text.Trim(), txtReimbDescription.Text.Trim(), cmbIsFixed.Text.Trim() == "Yes" ? true : false, cmbIsActive.Text.Trim() == "Yes" ? true : false, false);
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
            txtReimbCode.Text = "";
            txtReimbCode.ReadOnly = true;
            txtReimbTitle.Text = "";
            txtReimbDescription.Text = "";
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
        }

        public void enableControls()
        {
            txtReimbCode.Enabled = true;
            txtReimbCode.ReadOnly = true;
            txtReimbTitle.Enabled = true;
            txtReimbDescription.Enabled = true;
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
        }

        public void disableControls()
        {
            txtReimbCode.Enabled = false;
            txtReimbCode.ReadOnly = true;
            txtReimbTitle.Enabled = false;
            txtReimbDescription.Enabled = false;
            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.Enabled = false;
            cmbIsActive.SelectedIndex = 0;

            cmbIsFixed.Items.Clear();
            cmbIsFixed.Items.Add("");
            cmbIsFixed.Items.Add("Yes");
            cmbIsFixed.Items.Add("No");
            cmbIsFixed.Enabled = false;
            cmbIsFixed.SelectedIndex = 0;
        }

        public void onGenerateButtonClick()
        {
            lblActionMode.Text = "add";
            lblReimbursementID.Text = "";
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
            lblReimbursementID.Text = "";
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
            lblReimbursementID.Text = "";
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
            lblReimbursementID.Text = "";
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
            lblReimbursementID.Text = "";
            btnSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void displaySelectedValuesOnUI(ReimbursementModel ReimbursementModel)
        {
            lblReimbursementID.Text = ReimbursementModel.ReimbID.ToString();
            txtReimbCode.Text = ReimbursementModel.ReimbCode;
            txtReimbTitle.Text = ReimbursementModel.ReimbTitle;
            txtReimbDescription.Text = ReimbursementModel.ReimbDescription;
            cmbIsFixed.Text = ReimbursementModel.IsFixed == true ? "Yes" : "No";
            cmbIsActive.Text = ReimbursementModel.IsActive == true ? "Yes" : "No";
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
            cmbIsFixed.SelectedIndex = 1;
            cmbIsActive.SelectedIndex = 1;
            errValidator.Clear();
        }

        private void btnRemoveDetails_Click(object sender, EventArgs e)
        {
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
                    int affectedRows = objReimbursement.DeleteReimbursement(Convert.ToInt16(lblReimbursementID.Text.Trim()));
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

        private void frmReimbursement_KeyDown(object sender, KeyEventArgs e)
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
    }
}
