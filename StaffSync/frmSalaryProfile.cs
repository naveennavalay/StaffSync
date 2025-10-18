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
    public partial class frmSalaryProfile : Form
    {
        //myDBClass objDBClass = new myDBClass();
        //OleDbConnection conn = null;
        //DataSet dtDataset;

        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        DALStaffSync.clsAllowenceInfo objAllowenceInfo = new DALStaffSync.clsAllowenceInfo();
        DALStaffSync.clsDeductionsInfo objDeductionsInfo = new DALStaffSync.clsDeductionsInfo();
        DALStaffSync.clsReimbursement objReimbursement = new DALStaffSync.clsReimbursement();
        DALStaffSync.clsSalaryProfile objSalaryProfile = new DALStaffSync.clsSalaryProfile();
        frmDashboard objDashboard = (frmDashboard) System.Windows.Forms.Application.OpenForms["frmDashboard"];

        public frmSalaryProfile()
        {
            InitializeComponent();
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

        private void frmSalaryProfile_Load(object sender, EventArgs e)
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
            frmSalaryProfileList frmSalaryProfileList = new frmSalaryProfileList(this);
            frmSalaryProfileList.ShowDialog(this);
        }

        private void btnCloseMe_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenerateDetails_Click(object sender, EventArgs e)
        {
            lblActionMode.Text = "add";
            onGenerateButtonClick();
            clearControls();
            enableControls();
            cmbIsActive.SelectedIndex = 1;
            lblSalaryProfileID.Text = objGenFunc.getMaxRowCount("SalProfileMas", "SalProfileID").Data.ToString();
            txtSalProfCode.Text = "SPF-" + (lblSalaryProfileID.Text.Trim()).ToString().PadLeft(4, '0');
            errValidator.Clear();
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSalProfTitle.Text))
            {
                txtSalProfTitle.Focus();
                errValidator.SetError(this.txtSalProfTitle, "Please enter Allowence Title");
            }
            else if (string.IsNullOrEmpty(txtSalProfDescription.Text))
            {
                txtSalProfDescription.Focus();
                errValidator.SetError(this.txtSalProfDescription, "Please enter Allowence Description");
            }
            else if (string.IsNullOrEmpty(cmbIsActive.Text))
            {
                cmbIsActive.Focus();
                cmbIsActive.SelectedIndex = 1;
                errValidator.SetError(this.cmbIsActive, "Please select Allowence Status");
            }
            else
            {
                if (lblActionMode.Text == "add")
                {
                    int salaryProfileID = objSalaryProfile.InsertSalaryProfileInfo(txtSalProfCode.Text.Trim(), txtSalProfTitle.Text.Trim(), txtSalProfDescription.Text.Trim(), cmbIsActive.Text.Trim() == "Yes" ? true : false, false);

                    DataTable dtAllowanceList = objAllowenceInfo.GetAllowenceList();
                    foreach (DataRow indRow in dtAllowanceList.Rows)
                    {
                        objSalaryProfile.InsertSalaryProfileDetailInfo(salaryProfileID, Convert.ToInt16(indRow["AllID"].ToString()), 0, 0, 0);
                    }
                    DataTable dtDeductionsList = objDeductionsInfo.GetDeductionList();
                    foreach (DataRow indRow in dtDeductionsList.Rows)
                    {
                        objSalaryProfile.InsertSalaryProfileDetailInfo(salaryProfileID, 0, Convert.ToInt16(indRow["DedID"].ToString()), 0, 0);
                    }
                    DataTable dtReimbursment = objReimbursement.GetReimbursementList();
                    foreach (DataRow indRow in dtReimbursment.Rows)
                    {
                        objSalaryProfile.InsertSalaryProfileDetailInfo(salaryProfileID, 0, 0, Convert.ToInt16(indRow["ReimbID"].ToString()), 0);
                    }

                    if (salaryProfileID > 0)
                        MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (lblActionMode.Text == "modify")
                {
                    int affectedRows = objSalaryProfile.UpdateSalaryProfileInfo(Convert.ToInt16(lblSalaryProfileID.Text.Trim()), txtSalProfCode.Text.Trim(), txtSalProfTitle.Text.Trim(), txtSalProfDescription.Text.Trim(), cmbIsActive.Text.Trim() == "Yes" ? true : false, false);
                    if (affectedRows > 0)
                        MessageBox.Show("Details updated successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                onSaveButtonClick();
                disableControls();
                clearControls();
                errValidator.Clear();
            }
        }

        public void clearControls()
        {
            txtSalProfCode.Text = "";
            txtSalProfCode.ReadOnly = true;
            txtSalProfTitle.Text = "";
            txtSalProfDescription.Text = "";
            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.SelectedIndex = 0;
        }

        public void enableControls()
        {
            txtSalProfCode.Enabled = true;
            txtSalProfCode.ReadOnly = true;
            txtSalProfTitle.Enabled = true;
            txtSalProfDescription.Enabled = true;
            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.Enabled = true;
        }

        public void disableControls()
        {
            txtSalProfCode.Enabled = false;
            txtSalProfCode.ReadOnly = true;
            txtSalProfTitle.Enabled = false;
            txtSalProfDescription.Enabled = false;
            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.Enabled = false;
        }

        public void onGenerateButtonClick()
        {
            lblActionMode.Text = "add";
            lblSalaryProfileID.Text = "";
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
            lblSalaryProfileID.Text = "";
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
            lblSalaryProfileID.Text = "";
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
            lblSalaryProfileID.Text = "";
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
            lblSalaryProfileID.Text = "";
            btnSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void displaySelectedValuesOnUI(SalaryProfileTitleList SalaryProfileInfoModel)
        {
            lblSalaryProfileID.Text = SalaryProfileInfoModel.SalProfileID.ToString();
            txtSalProfCode.Text = SalaryProfileInfoModel.SalProfileCode;
            txtSalProfTitle.Text = SalaryProfileInfoModel.SalProfileTitle;
            txtSalProfDescription.Text = SalaryProfileInfoModel.SalProfileDescription;
            cmbIsActive.Text = SalaryProfileInfoModel.IsActive == true ? "Yes" : "No";
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
            lblActionMode.Text = "modify"; 
            onModifyButtonClick();
            clearControls();
            enableControls();
            cmbIsActive.SelectedIndex = 1;
            errValidator.Clear();
        }

        private void btnRemoveDetails_Click(object sender, EventArgs e)
        {
            if(lblActionMode.Text == "" || lblActionMode.Text == "remove")
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
                    int affectedRows = objSalaryProfile.DeleteSalaryProfileInfo(Convert.ToInt16(lblSalaryProfileID.Text.Trim()));
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

        private void frmSalaryProfile_KeyDown(object sender, KeyEventArgs e)
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
