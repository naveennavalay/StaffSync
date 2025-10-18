using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public partial class frmUserManagement : Form
    {
        DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster();
        DALStaffSync.clsUserManagement objUserManagementList = new DALStaffSync.clsUserManagement();
        clsImpageOperation objImpageOperation = new clsImpageOperation();
        DALStaffSync.clsPhotoMas objPhotoMas = new DALStaffSync.clsPhotoMas();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        frmDashboard objDashboard = (frmDashboard) System.Windows.Forms.Application.OpenForms["frmDashboard"];

        public frmUserManagement()
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

        private void frmUserManagement_Load(object sender, EventArgs e)
        {
            //lstUserManagementList.Items.Clear();
            //List<UserManagementList> objUserManagmentList = objUserManagementList.GetUserManagementList();
            //foreach (UserManagementList indUserManagement in objUserManagmentList)
            //{
            //    System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            //        indUserManagement.EmpID.ToString(),
            //        indUserManagement.EmpCode,
            //        indUserManagement.EmpName,
            //        indUserManagement.DepartmentTitle,
            //        indUserManagement.DesignationTitle,
            //        indUserManagement.IsActive == true ? "Active" : "In-Active",
            //        indUserManagement.IsLocked == false ? "Unlocked" : "Locked"
            //    });
            //    lstUserManagementList.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem1 });
            //}
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
            btnResetPassword.Enabled = false;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = true;
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
            txtRepEmpContactNumber.Text = "";
            cmbActiveStatus.Enabled = false;
            cmbLockStatus.Enabled = false;
            btnResetPassword.Enabled = false;
            picRepEmpPhoto.Image = null;
            picActiveInActive.Image = null;
            picLockUnlock.Image = null;
            cmbActiveStatus.Items.Clear();
            cmbLockStatus.Items.Clear();
            picActiveInActive.Visible = false;
            picLockUnlock.Visible = false;
        }

        public void enableControls()
        {
            //txtRepEmpCode.Enabled = false;
            //txtRepEmpName.Enabled = false;
            //txtRepEmpDesig.Enabled = false;
            //txtRepEmpDepartment.Enabled = false;
            //txtRepEmpContactNumber.Enabled = false;
            btnReportingManagerSearch.Enabled = true;
            cmbActiveStatus.Enabled = true;
            cmbLockStatus.Enabled = true;
            btnResetPassword.Enabled = true;
        }

        public void disableControls()
        {
            btnReportingManagerSearch.Enabled = false;
            txtRepEmpCode.Enabled = false;
            txtRepEmpName.Enabled = false;
            txtRepEmpDesig.Enabled = false;
            txtRepEmpDepartment.Enabled = false;
            txtRepEmpContactNumber.Enabled = false;
            cmbActiveStatus.Enabled = false;
            cmbLockStatus.Enabled = false;
            btnResetPassword.Enabled = false;
        }

        private void btnModifyDetails_Click(object sender, EventArgs e)
        {
            onModifyButtonClick();
            enableControls();
            cmbActiveStatusList();
            cmbLockStatusList();
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            if (validateValues())
            {
                string strActiveStatus = cmbActiveStatus.Text.ToString().ToLower();
                string strLockStatus = cmbLockStatus.Text.ToString().ToLower();

                objUserManagementList.UpdateUserActiveStatus(Convert.ToInt16(lblReportingManagerID.Text), strActiveStatus == "active" ? true : false);
                objUserManagementList.UpdateUserLockStatus(Convert.ToInt16(lblReportingManagerID.Text), strLockStatus == "lock" ? true : false);

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

        private void cmbActiveStatusList()
        {
            cmbActiveStatus.Items.Clear();
            cmbActiveStatus.Items.Add("Active");
            cmbActiveStatus.Items.Add("In-Active");
            cmbActiveStatus.SelectedIndex = 0;
        }

        private void cmbLockStatusList()
        {
            cmbLockStatus.Items.Clear();
            cmbLockStatus.Items.Add("Lock");
            cmbLockStatus.Items.Add("Unlock");
            cmbLockStatus.SelectedIndex = 0;
        }

        private void btnReportingManagerSearch_Click(object sender, EventArgs e)
        {
            frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listUserManagementList");
            frmEmployeeList.ShowDialog();
        }

        private void btnCloseMe_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SelectedEmployeeID(string SearchOptionSelectedForm, int selectedEmployeeID)
        {
            if (SearchOptionSelectedForm == "listUserManagementList")
            {
                picActiveInActive.Visible = true;
                picLockUnlock.Visible = true;
                lblReportingManagerID.Text = selectedEmployeeID.ToString();
                ReportingManagerInfo objReportingManagerInfo = objEmployeeMaster.GetReportingManagerInfo(Convert.ToInt16(selectedEmployeeID.ToString()));
                txtRepEmpCode.Text = objReportingManagerInfo.EmpCode;
                txtRepEmpName.Text = objReportingManagerInfo.EmpName;
                txtRepEmpDesig.Text = objReportingManagerInfo.DesignationTitle;
                txtRepEmpDepartment.Text = objReportingManagerInfo.DepartmentTitle;
                txtRepEmpContactNumber.Text = objReportingManagerInfo.ContactNumber1;
                picRepEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(lblReportingManagerID.Text.ToString())).EmpPhoto);

                UserInfo objLoggingInUserInfo = objLogin.getSpecificUserInfo(Convert.ToInt16(selectedEmployeeID.ToString()));
                if (objLoggingInUserInfo.UserID != 0)
                {
                    if (objLoggingInUserInfo.IsActive == true)
                        cmbActiveStatus.SelectedIndex = 0;
                    else if (objLoggingInUserInfo.IsActive == false)
                        cmbActiveStatus.SelectedIndex = 1;

                    if (objLoggingInUserInfo.IsLocked == true)
                        cmbLockStatus.SelectedIndex = 0;
                    else if (objLoggingInUserInfo.IsLocked == false)
                        cmbLockStatus.SelectedIndex = 1;
                }
            }
        }

        private void cmbLockStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbLockStatus.SelectedIndex == 0)
                picLockUnlock.Image = imgList.Images[0];
            else if (cmbLockStatus.SelectedIndex == 1)
                picLockUnlock.Image = imgList.Images[1];
        }

        private void cmbActiveStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbActiveStatus.SelectedIndex == 0)
                picActiveInActive.Image = imgList.Images[2];
            else if (cmbActiveStatus.SelectedIndex == 1)
                picActiveInActive.Image = imgList.Images[3];
        }

        private void frmUserManagement_KeyDown(object sender, KeyEventArgs e)
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
