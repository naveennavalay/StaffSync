using ModelStaffSync;
using Org.BouncyCastle.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static C1.Util.Win.Win32;

namespace StaffSync
{
    public partial class frmRolesAndResponsibilities : Form
    {
        DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster();
        DALStaffSync.clsUserManagement objUserManagementList = new DALStaffSync.clsUserManagement();
        clsImpageOperation objImpageOperation = new clsImpageOperation();
        DALStaffSync.clsPhotoMas objPhotoMas = new DALStaffSync.clsPhotoMas();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        DALStaffSync.clsAppModule objAppModule = new DALStaffSync.clsAppModule();
        DALStaffSync.clsRolesAndResponsibilities objRolesAndResponsibilities = new DALStaffSync.clsRolesAndResponsibilities();
        frmDashboard objDashboard = (frmDashboard) System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();

        public frmRolesAndResponsibilities()
        {
            InitializeComponent();
        }

        public frmRolesAndResponsibilities(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmRolesAndResponsibilities(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
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

        private void frmRolesAndResponsibilities_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'staffsyncDBDTSet.qryUserSpecificRoles' table. You can move, or remove it, as needed.
            //this.qryUserSpecificRolesTableAdapter.Fill(this.staffsyncDBDTSet.qryUserSpecificRoles);
            // TODO: This line of code loads data into the 'staffsyncDBDTSet.qryRolesDef' table. You can move, or remove it, as needed.
            //this.qryRolesDefTableAdapter.Fill(this.staffsyncDBDTSet.qryRolesDef);
            // TODO: This line of code loads data into the 'staffsyncDBDTSet.qryUserRoles' table. You can move, or remove it, as needed.
            //this.qryUserRolesTableAdapter.Fill(this.staffsyncDBDTSet.qryUserRoles);
            RefreshDefaultRolesAndResponsibilities();

            onCancelButtonClick();
            clearControls();
            disableControls();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (lblActionMode.Text == "modify")
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
            RefreshDefaultRolesAndResponsibilities();
            errValidator.Clear();
        }

        public void onModifyButtonClick()
        {
            lblActionMode.Text = "modify";
            lblReportingManagerID.Text = "";
            btnReportingManagerSearch.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onSaveButtonClick()
        {
            lblReportingManagerID.Text = "";
            lblActionMode.Text = "";
            lblReportingManagerID.Text = "";
            btnReportingManagerSearch.Enabled = false;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnCancel.Enabled = true;
        }

        public void onCancelButtonClick()
        {
            lblActionMode.Text = "";
            lblReportingManagerID.Text = "";
            btnReportingManagerSearch.Enabled = false;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnCancel.Enabled = true;
        }

        public void clearControls()
        {
            btnReportingManagerSearch.Enabled = false;
            txtRepEmpCode.Text = "";
            txtRepEmpName.Text = "";
            txtRepEmpDesig.Text = "";
            txtRepEmpDepartment.Text = "";
            txtAssignedRole.Text = "";
            picRepEmpPhoto.Image = null;
        }

        public void enableControls()
        {
            //txtRepEmpCode.Enabled = false;
            //txtRepEmpName.Enabled = false;
            //txtRepEmpDesig.Enabled = false;
            //txtRepEmpDepartment.Enabled = false;
            //txtRepEmpContactNumber.Enabled = false;
            txtAssignedRole.Enabled = false;
            btnReportingManagerSearch.Enabled = true;
            dtgRolesAndResponsibilties.Enabled = true;
        }

        public void disableControls()
        {
            btnReportingManagerSearch.Enabled = false;
            txtRepEmpCode.Enabled = false;
            txtRepEmpName.Enabled = false;
            txtRepEmpDesig.Enabled = false;
            txtRepEmpDepartment.Enabled = false;
            txtAssignedRole.Enabled = false;
            dtgRolesAndResponsibilties.Enabled = false;
        }

        private void btnModifyDetails_Click(object sender, EventArgs e)
        {
            string strValidationMessage = objLogin.ValidateUserRolesAndResponsibilitiesInfo(objTempCurrentlyLoggedInUserInfo.EmpID);
            if (strValidationMessage != "Success")
            {
                MessageBox.Show(strValidationMessage, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            onModifyButtonClick();
            enableControls();
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            string strValidationMessage = objLogin.ValidateUserRolesAndResponsibilitiesInfo(objTempCurrentlyLoggedInUserInfo.EmpID);
            if (strValidationMessage != "Success")
            {
                MessageBox.Show(strValidationMessage, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            int insertNewRecordCount = 0;
            if (validateValues())
            {
                int deletedExistingRecordCount = objRolesAndResponsibilities.RemoveUsersRolesAndResponsibilitiesInfo(Convert.ToInt16(lblReportingManagerID.Text.ToString()));

                foreach (DataGridViewRow indRow in dtgRolesAndResponsibilties.Rows)
                {
                    if(indRow.Cells["Access"].Value.ToString() == "True")
                        insertNewRecordCount = objRolesAndResponsibilities.InsertUsersRolesAndResponsibilitiesInfo(Convert.ToInt16(lblReportingManagerID.Text.ToString()), Convert.ToInt16(indRow.Cells["RoleID"].Value.ToString()));
                }

                objTempCurrentlyLoggedInUserInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(lblReportingManagerID.Text.ToString()));

                onSaveButtonClick();
                clearControls();
                disableControls();

                if (insertNewRecordCount > 0)
                {
                    //objLeaveTRList.UpdateEmployeeLeaveBalance(Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToDecimal(txtAvailableLeave.Text), Convert.ToDecimal(txtBalanceLeave.Text));
                    RefreshDefaultRolesAndResponsibilities();
                    MessageBox.Show("Details updated successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Details not inserted successfully.\nPlease verify once again.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private bool validateValues()
        {
            bool validateStatus = true;

            return validateStatus;
        }

        private void btnReportingManagerSearch_Click(object sender, EventArgs e)
        {
            frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listUserRolesAndResponsibilities");
            frmEmployeeList.ShowDialog();
        }

        private void btnCloseMe_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SelectedEmployeeID(string SearchOptionSelectedForm, int selectedEmployeeID)
        {
            if (SearchOptionSelectedForm == "listUserRolesAndResponsibilities")
            {
                lblReportingManagerID.Text = selectedEmployeeID.ToString();
                ReportingManagerInfo objReportingManagerInfo = objEmployeeMaster.GetReportingManagerInfo(Convert.ToInt16(selectedEmployeeID.ToString()));
                txtRepEmpCode.Text = objReportingManagerInfo.EmpCode;
                txtRepEmpName.Text = objReportingManagerInfo.EmpName;
                txtRepEmpDesig.Text = objReportingManagerInfo.DesignationTitle;
                txtRepEmpDepartment.Text = objReportingManagerInfo.DepartmentTitle;
                txtAssignedRole.Text = objTempCurrentlyLoggedInUserInfo.RoleTitle;
                picRepEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(lblReportingManagerID.Text.ToString())).EmpPhoto);

                UserInfo objLoggingInUserInfo = objLogin.getSpecificUserInfo(Convert.ToInt16(selectedEmployeeID.ToString()));
                if (objLoggingInUserInfo.UserID != 0)
                {

                }

                RefreshRolesAndResponsibilities(Convert.ToInt16(lblReportingManagerID.Text.ToString()));

                //cmbModule.DataSource = objRolesAndResponsibilities.GetDefaultRolesAndResponsibilitiesInfo();
                //cmbModule.DisplayMember = "ModuleTitle";
                //cmbModule.ValueMember = "ModuleID";
            }
        }

        private void RefreshDefaultRolesAndResponsibilities()
        {
            dtgRolesAndResponsibilties.DataSource = objRolesAndResponsibilities.GetDefaultRolesAndResponsibilitiesInfo();
            dtgRolesAndResponsibilties.Columns[0].Visible = false;
            dtgRolesAndResponsibilties.Columns[0].ReadOnly = true;
            dtgRolesAndResponsibilties.Columns[1].Width = 250;
            dtgRolesAndResponsibilties.Columns[1].ReadOnly = true;
            dtgRolesAndResponsibilties.Columns[2].Width = 350;
            dtgRolesAndResponsibilties.Columns[2].ReadOnly = true;
            dtgRolesAndResponsibilties.Columns[3].Width = 100;
            dtgRolesAndResponsibilties.Columns[4].Visible = false;
        }

        private void RefreshRolesAndResponsibilities(int UserID)
        {
            dtgRolesAndResponsibilties.DataSource = objRolesAndResponsibilities.GetRolesAndResponsibilitiesInfo(UserID);
            dtgRolesAndResponsibilties.Columns[0].Visible = false;
            dtgRolesAndResponsibilties.Columns[0].ReadOnly = true;
            dtgRolesAndResponsibilties.Columns[1].Width = 250;
            dtgRolesAndResponsibilties.Columns[1].ReadOnly = true;
            dtgRolesAndResponsibilties.Columns[2].Width = 350;
            dtgRolesAndResponsibilties.Columns[2].ReadOnly = true;
            dtgRolesAndResponsibilties.Columns[3].Width = 100;
            dtgRolesAndResponsibilties.Columns[4].Visible = false;
        }

        private void frmRolesAndResponsibilities_KeyDown(object sender, KeyEventArgs e)
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

        private void dtgRolesAndResponsibilties_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            // Only handle checkbox clicks
            if (e.ColumnIndex == dgv.Columns["Access"].Index && e.RowIndex >= 0)
            {
                // Uncheck all other rows
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Index != e.RowIndex)
                        row.Cells["Access"].Value = false;
                }

                // Toggle the current one
                bool currentValue = Convert.ToBoolean(dgv.Rows[e.RowIndex].Cells["Access"].Value ?? false);
                dgv.Rows[e.RowIndex].Cells["Access"].Value = !currentValue;
                dgv.Rows[6].Cells["Access"].Value = true;
            }
        }

        private void dtgRolesAndResponsibilties_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex == dtgRolesAndResponsibilties.Rows.Count - 1 && dtgRolesAndResponsibilties.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn)
            {
                e.Cancel = true;
            }
        }

        private void frmRolesAndResponsibilities_Activated(object sender, EventArgs e)
        {
            //dtgRolesAndResponsibilties.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
        }
    }
}
