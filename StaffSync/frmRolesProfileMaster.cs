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
    public partial class frmRolesProfileMaster : Form
    {
        DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster();
        DALStaffSync.clsUserManagement objUserManagementList = new DALStaffSync.clsUserManagement();
        clsImpageOperation objImpageOperation = new clsImpageOperation();
        DALStaffSync.clsPhotoMas objPhotoMas = new DALStaffSync.clsPhotoMas();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        DALStaffSync.clsRolesAndResponsibilities objRolesAndResponsibilities = new DALStaffSync.clsRolesAndResponsibilities();
        DALStaffSync.clsAppModule objAppModule = new DALStaffSync.clsAppModule();
        frmDashboard objDashboard = (frmDashboard)System.Windows.Forms.Application.OpenForms["frmDashboard"];

        public frmRolesProfileMaster()
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

        private void frmRolesProfileMaster_Load(object sender, EventArgs e)
        {
            RefreshDefaultRolesProfileList();

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
            RefreshDefaultRolesProfileList();
            errValidator.Clear();
        }

        public void onModifyButtonClick()
        {
            lblActionMode.Text = "modify";
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onSaveButtonClick()
        {
            lblActionMode.Text = "";
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnCancel.Enabled = true;
        }

        public void onCancelButtonClick()
        {
            lblActionMode.Text = "";
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnCancel.Enabled = true;
        }

        public void clearControls()
        {

        }

        public void enableControls()
        {
            dtgRoleProfileManagement.Enabled = true;
        }

        public void disableControls()
        {
            dtgRoleProfileManagement.Enabled = false;
        }

        private void btnModifyDetails_Click(object sender, EventArgs e)
        {
            onModifyButtonClick();
            enableControls();
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            int insertNewRecordCount = 0;
            if (validateValues())
            {

            }
            onSaveButtonClick();
            clearControls();
            disableControls();
        }

        private bool validateValues()
        {
            bool validateStatus = true;

            return validateStatus;
        }

        private void btnReportingManagerSearch_Click(object sender, EventArgs e)
        {
            frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listRoleProfileManagement");
            frmEmployeeList.ShowDialog();
        }

        private void btnCloseMe_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SelectedEmployeeID(string SearchOptionSelectedForm)
        {
            if (SearchOptionSelectedForm == "listRoleProfileManagement")
            {
                //RefreshUserSpecificModulesList(Convert.ToInt16(lblReportingManagerID.Text.ToString()));
            }
        }

        private void RefreshDefaultRolesProfileList()
        {
            dtgRoleProfileManagement.DataSource = objRolesAndResponsibilities.GetDefaultRolesProfileList();
            dtgRoleProfileManagement.Columns[0].Visible = false;
            dtgRoleProfileManagement.Columns[0].ReadOnly = true;
            dtgRoleProfileManagement.Columns[1].Width = 250;
            dtgRoleProfileManagement.Columns[1].ReadOnly = true;
            dtgRoleProfileManagement.Columns[2].Visible = false; 
            dtgRoleProfileManagement.Columns[2].Width = 100;
            dtgRoleProfileManagement.Columns[2].ReadOnly = false;
            dtgRoleProfileManagement.Columns[3].Width = 100;
            dtgRoleProfileManagement.Columns[3].ReadOnly = true;
            dtgRoleProfileManagement.Columns[4].Width = 100;
            dtgRoleProfileManagement.Columns[4].ReadOnly = true;
            dtgRoleProfileManagement.Columns[5].Width = 100;
            dtgRoleProfileManagement.Columns[5].ReadOnly = true;
            dtgRoleProfileManagement.Columns[6].Width = 100;
            dtgRoleProfileManagement.Columns[6].ReadOnly = true;
        }

        private void frmRolesProfileMaster_KeyDown(object sender, KeyEventArgs e)
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
