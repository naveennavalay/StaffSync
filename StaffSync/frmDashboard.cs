using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static C1.Util.Win.Win32;
using static System.Windows.Forms.AxHost;

namespace StaffSync
{
    public partial class frmDashboard : Form
    {
        public int AppModuleID = 0;
        clsCurrentUserInfo objCurrentUserInfo = new clsCurrentUserInfo();
        clsBirthdayList objBirthdayList = new clsBirthdayList();
        //Download objDownload = new Download();
        clsLeaveTRList objLeaveTRList = new clsLeaveTRList();
        clsLeaveTRList objLeaveInfo = new clsLeaveTRList();
        clsClientInfo objClientInfo = new clsClientInfo();

        public frmDashboard()
        {
            InitializeComponent();
            List<ClientInfo> objActiveClientInfo = objClientInfo.getClientInfo(1);
        }

        private void employeeDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manageEmployeeDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppModuleID = 2;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            frmEmployeeMaster frmEmployeeMasterDetails = new frmEmployeeMaster();
            frmEmployeeMasterDetails.ShowDialog();
        }

        private void manageEmployeeAttendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppModuleID = 4;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            frmAttendanceMater frmAttendanceMaterDetails = new frmAttendanceMater();
            frmAttendanceMaterDetails.ShowDialog();
        }

        private void manageEmployeePayrollToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppModuleID = 3;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //frmPayrollMaster frmPayrollMasterDetails = new frmPayrollMaster();
            //frmPayrollMasterDetails.ShowDialog();
        }

        private void manageEmployeeLeavesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppModuleID = 5;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            frmLeavesMaster frmLeavesMasterDetails = new frmLeavesMaster();
            frmLeavesMasterDetails.ShowDialog();
        }

        private void employeeWiseReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmployeeWiseReports frmEmployeeWiseReportsDetails = new frmEmployeeWiseReports();
            frmEmployeeWiseReportsDetails.ShowDialog();
        }

        private void employeeAttendanceReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmployeeAttendanceReports frmEmployeeAttendanceReportsDetails = new frmEmployeeAttendanceReports();
            frmEmployeeAttendanceReportsDetails.ShowDialog();
        }

        private void employeePayrollReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmployeePayrollReports frmEmployeePayrollReportsDetails = new frmEmployeePayrollReports();
            frmEmployeePayrollReportsDetails.ShowDialog();
        }

        private void employeeLeavesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmployeeLeavesReports frmEmployeeLeavesReportsDetails = new frmEmployeeLeavesReports();
            frmEmployeeLeavesReportsDetails.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout frmAboutDetails = new frmAbout();
            frmAboutDetails.ShowDialog();
        }

        private void departmentListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster();
            frmDepartmentMaster.ShowDialog(this);
        }

        private void countriesListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmCountryMaster frmCountryMaster = new frmCountryMaster();
            frmCountryMaster.ShowDialog(this);
        }

        private void designationListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDesignationMaster frmDesignationMaster = new frmDesignationMaster();
            frmDesignationMaster.ShowDialog(this);
        }

        private void countriesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStateMaster frmStateMaster = new frmStateMaster();
            frmStateMaster.ShowDialog(this);
        }

        private void relationshipListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster();
            frmRelationshipMaster.ShowDialog(this);
        }

        private void companyDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster();
            frmLastCompanyMaster.ShowDialog(this);
        }

        private void educationListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEduQualMaster frmEduQualMaster = new frmEduQualMaster();
            frmEduQualMaster.ShowDialog(this);
        }

        private void departmentListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AppModuleID = 10;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            
            frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster();
            frmDepartmentMaster.ShowDialog(this);
        }

        private void mainListingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void designationListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AppModuleID = 10;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            
            frmDesignationMaster frmDesignationMaster = new frmDesignationMaster();
            frmDesignationMaster.ShowDialog(this);
        }

        private void educationListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AppModuleID = 10;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            
            frmEduQualMaster frmEduQualMaster = new frmEduQualMaster();
            frmEduQualMaster.ShowDialog(this);
        }

        private void relationshipListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AppModuleID = 10;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            
            frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster();
            frmRelationshipMaster.ShowDialog(this);
        }

        private void statesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppModuleID = 10;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            
            frmStateMaster frmStateMaster = new frmStateMaster();
            frmStateMaster.ShowDialog(this);
        }

        private void countriesListToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            AppModuleID = 10;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            
            frmCountryMaster frmCountryMaster = new frmCountryMaster();
            frmCountryMaster.ShowDialog(this);
        }

        private void companyListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppModuleID = 10;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            
            frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster();
            frmLastCompanyMaster.ShowDialog(this);
        }

        private void skillsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppModuleID = 10;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            
            frmSkillsMaster frmSkillsMaster = new frmSkillsMaster();
            frmSkillsMaster.Show(this);
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            myStatusBar.Items[0].Text = "User Name : " + clsCurrentUser.UserName.ToString();
            myStatusBar.Items[1].Text = "Log In : " + clsCurrentUser.LoginDateTime.ToString("dd-MMM-yyyy hh:mm:ss tt");

            cmbLeaveApproval.Text = "Leave Approval (" + objLeaveInfo.getPendingLeaveApprovalList().Count + ")";
            cmbLeaveReject.Text = "Leave Reject (" + objLeaveInfo.getPendingLeaveApprovalList().Count + ")";

            //lstBirthdayList.Items.Clear();
            //List<BirthdayList> objEmployeesBirthdayList = objBirthdayList.GetEmployeesBirthdayList();
            //foreach (BirthdayList indEmpBirthday in objEmployeesBirthdayList)
            //{
            //    System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            //        indEmpBirthday.EmpID.ToString(),
            //        indEmpBirthday.EmpCode,
            //        indEmpBirthday.EmpName,
            //        indEmpBirthday.DOB.ToString("dd-MMM-yyyy"),
            //        indEmpBirthday.DepartmentTitle,
            //        indEmpBirthday.DesignationTitle
            //    });
            //    lstBirthdayList.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem1 });
            //}

            //lstOOOList.Items.Clear();
            //List<EmployeeOOOList> objEmployeeOOOList = objLeaveTRList.GetEmployeeOOOList();
            //foreach (EmployeeOOOList indEmployeeOOO in objEmployeeOOOList)
            //{
            //    System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            //        indEmployeeOOO.EmpID.ToString(),
            //        indEmployeeOOO.EmpCode,
            //        indEmployeeOOO.EmpName,
            //        indEmployeeOOO.DepartmentTitle,
            //        indEmployeeOOO.DesignationTitle,
            //        indEmployeeOOO.LeaveTypeTitle,
            //        indEmployeeOOO.ActualLeaveDateFrom.ToString("dd-MMM-yyyy"),
            //        indEmployeeOOO.ActualLeaveDateFrom.ToString("dd-MMM-yyyy"),
            //        Convert.ToDecimal(indEmployeeOOO.LeaveDuration).ToString()
            //    });
            //    lstOOOList.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem1 });
            //}
        }

        private Bitmap BytesToImage(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return null;

            using (MemoryStream image_stream = new MemoryStream(bytes))
            {
                Bitmap bm = new Bitmap(image_stream);
                image_stream.Close();
                return bm;
            }
        }

        private void picRefreshBirthdayList_Click(object sender, EventArgs e)
        {
            //lstBirthdayList.Items.Clear();
            //List<BirthdayList> objEmployeesBirthdayList = objBirthdayList.GetEmployeesBirthdayList();
            //foreach (BirthdayList indEmpBirthday in objEmployeesBirthdayList)
            //{
            //    System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            //        indEmpBirthday.EmpID.ToString(),
            //        indEmpBirthday.EmpCode,
            //        indEmpBirthday.EmpName,
            //        indEmpBirthday.DOB.ToString("dd-MMM-yyyy") ,
            //        indEmpBirthday.DepartmentTitle,
            //        indEmpBirthday.DesignationTitle

            //    });
            //    lstBirthdayList.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem1 });
            //}
        }

        private void lstBirthdayList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //frmEmployeeMaster frmEmployeeMasterDetails = new frmEmployeeMaster(Convert.ToInt16(lstBirthdayList.SelectedItems[0].SubItems[0].Text));
            //frmEmployeeMasterDetails.ShowDialog();
        }

        private void picDownloadBirthdayList_Click(object sender, EventArgs e)
        {
            //objDownload.DownloadExcel(lstBirthdayList);
        }

        private void picRefreshEmpOOOList_Click(object sender, EventArgs e)
        {
            //lstOOOList.Items.Clear();
            //List<EmployeeOOOList> objEmployeeOOOList = objLeaveTRList.GetEmployeeOOOList();
            //foreach (EmployeeOOOList indEmployeeOOO in objEmployeeOOOList)
            //{
            //    System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            //        indEmployeeOOO.EmpID.ToString(),
            //        indEmployeeOOO.EmpCode,
            //        indEmployeeOOO.EmpName,
            //        indEmployeeOOO.DepartmentTitle,
            //        indEmployeeOOO.DesignationTitle,
            //        indEmployeeOOO.LeaveTypeTitle,
            //        indEmployeeOOO.ActualLeaveDateFrom.ToString("dd-MMM-yyyy"),
            //        indEmployeeOOO.ActualLeaveDateFrom.ToString("dd-MMM-yyyy"),
            //        Convert.ToDecimal(indEmployeeOOO.LeaveDuration).ToString()
            //    });
            //    lstOOOList.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem1 });
            //}
        }

        private void picDownloadOOOList_Click(object sender, EventArgs e)
        {
            //objDownload.DownloadExcel(lstOOOList);
        }

        private void leaveApprovalRejectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppModuleID = 5;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            frmLeavesApproval frmLeavesApproval = new frmLeavesApproval();
            frmLeavesApproval.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppModuleID = 9;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            frmUserManagement frmUserManagement = new frmUserManagement();
            frmUserManagement.ShowDialog();
        }

        private void rolesAndResponsibilitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppModuleID = 9;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            frmRolesAndResponsibilities frmRolesAndResponsibilities = new frmRolesAndResponsibilities();
            frmRolesAndResponsibilities.ShowDialog();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            AppModuleID = 9;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            frmModuleAssignment frmModuleAssignment = new frmModuleAssignment();
            frmModuleAssignment.ShowDialog();
        }

        private void assetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppModuleID = 6;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void assetsAllotmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppModuleID = 6;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void roleProfileManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppModuleID = 8;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            frmRolesProfileMaster frmRolesProfileMaster = new frmRolesProfileMaster();
            frmRolesProfileMaster.ShowDialog();
        }

        private void tlbExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void tlbManageEmployeeInfoButton_Click(object sender, EventArgs e)
        {
            AppModuleID = 2;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            frmEmployeeMaster frmEmployeeMasterDetails = new frmEmployeeMaster();
            frmEmployeeMasterDetails.ShowDialog();
        }

        private void tlbManageEmployeeAttendanceButton_Click(object sender, EventArgs e)
        {

        }

        private void cmUserManagementList_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag.ToString())
            {
                case "cmbManageUsers":
                    AppModuleID = 9;

                    objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                    if (clsCurrentUser.ModuleID != AppModuleID)
                    {
                        if (clsCurrentUser.ModuleID != 1)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    frmUserManagement frmUserManagement = new frmUserManagement();
                    frmUserManagement.ShowDialog();
                    break;

                case "cmbRoleAssignment":
                    AppModuleID = 9;

                    objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                    if (clsCurrentUser.ModuleID != AppModuleID)
                    {
                        if (clsCurrentUser.ModuleID != 1)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    frmRolesAndResponsibilities frmRolesAndResponsibilities = new frmRolesAndResponsibilities();
                    frmRolesAndResponsibilities.ShowDialog();
                    break;

                case "cmbModuleAssignment":
                    AppModuleID = 9;

                    objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                    if (clsCurrentUser.ModuleID != AppModuleID)
                    {
                        if (clsCurrentUser.ModuleID != 1)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    frmModuleAssignment frmModuleAssignment = new frmModuleAssignment();
                    frmModuleAssignment.ShowDialog();
                    break;

                case "cmbRoleProfilement":
                    AppModuleID = 10;

                    objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                    if (clsCurrentUser.ModuleID != AppModuleID)
                    {
                        if (clsCurrentUser.ModuleID != 1)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    frmRolesProfileMaster frmRolesProfileMaster = new frmRolesProfileMaster();
                    frmRolesProfileMaster.ShowDialog(this);
                    break;
            }
        }

        private void cmAttendanceManagementList_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag.ToString())
            {
                case "cmbManageEmployeeAttendance":
                    AppModuleID = 4;

                    objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                    if (clsCurrentUser.ModuleID != AppModuleID)
                    {
                        if (clsCurrentUser.ModuleID != 1)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    frmAttendanceMater frmAttendanceMaterDetails = new frmAttendanceMater();
                    frmAttendanceMaterDetails.ShowDialog();
                    break;
            }
        }

        private void cmLeaveManagement_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag.ToString())
            {
                case "cmbLeaveRequest":
                    AppModuleID = 5;

                    objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                    if (clsCurrentUser.ModuleID != AppModuleID)
                    {
                        if (clsCurrentUser.ModuleID != 1)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    frmLeavesMaster frmLeavesMasterDetails = new frmLeavesMaster();
                    frmLeavesMasterDetails.ShowDialog();
                    cmbLeaveApproval.Text = "Leave Approval (" + objLeaveInfo.getPendingLeaveApprovalList().Count + ")";
                    cmbLeaveReject.Text = "Leave Reject (" + objLeaveInfo.getPendingLeaveApprovalList().Count + ")";
                    break;
                case "cmbLeaveApproval":
                    AppModuleID = 5;

                    objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                    if (clsCurrentUser.ModuleID != AppModuleID)
                    {
                        if (clsCurrentUser.ModuleID != 1)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    frmLeavesApproval frmLeavesApproval = new frmLeavesApproval();
                    frmLeavesApproval.ShowDialog();
                    cmbLeaveApproval.Text = "Leave Approval (" + objLeaveInfo.getPendingLeaveApprovalList().Count + ")";
                    cmbLeaveReject.Text = "Leave Reject (" + objLeaveInfo.getPendingLeaveApprovalList().Count + ")";
                    break;
                case "cmbLeaveReject":
                    AppModuleID = 5;

                    objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                    if (clsCurrentUser.ModuleID != AppModuleID)
                    {
                        if (clsCurrentUser.ModuleID != 1)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    frmLeavesReject frmLeavesReject = new frmLeavesReject();
                    frmLeavesReject.ShowDialog();
                    break;
            }
        }

        private void cmbCompanyList01_Click(object sender, EventArgs e)
        {

        }

        private void cmbApplicationSettings_Click(object sender, EventArgs e)
        {

        }

        private void cmbApplicationSettings_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            switch (e.ClickedItem.Tag.ToString())
            {
                case "cmbCompanyList":
                    AppModuleID = 10;

                    objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                    if (clsCurrentUser.ModuleID != AppModuleID)
                    {
                        if (clsCurrentUser.ModuleID != 1)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster();
                    frmLastCompanyMaster.ShowDialog(this);
                    break;
                case "cmbEducationList":
                    AppModuleID = 10;

                    objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                    if (clsCurrentUser.ModuleID != AppModuleID)
                    {
                        if (clsCurrentUser.ModuleID != 1)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    frmEduQualMaster frmEduQualMaster = new frmEduQualMaster();
                    frmEduQualMaster.ShowDialog(this);
                    break;
                case "cmbSkillsList":
                    AppModuleID = 10;

                    objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                    if (clsCurrentUser.ModuleID != AppModuleID)
                    {
                        if (clsCurrentUser.ModuleID != 1)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    frmSkillsMaster frmSkillsMaster = new frmSkillsMaster();
                    frmSkillsMaster.Show(this);
                    break;
                case "cmbDepartmentList":
                    AppModuleID = 10;

                    objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                    if (clsCurrentUser.ModuleID != AppModuleID)
                    {
                        if (clsCurrentUser.ModuleID != 1)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster();
                    frmDepartmentMaster.ShowDialog(this);
                    break;
                case "cmbDesignationList":
                    AppModuleID = 10;

                    objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                    if (clsCurrentUser.ModuleID != AppModuleID)
                    {
                        if (clsCurrentUser.ModuleID != 1)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    frmDesignationMaster frmDesignationMaster = new frmDesignationMaster();
                    frmDesignationMaster.ShowDialog(this);
                    break;
                case "cmbCountriesList":
                    AppModuleID = 10;

                    objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                    if (clsCurrentUser.ModuleID != AppModuleID)
                    {
                        if (clsCurrentUser.ModuleID != 1)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    frmCountryMaster frmCountryMaster = new frmCountryMaster();
                    frmCountryMaster.ShowDialog(this);
                    break;
                case "cmbStatesList":
                    AppModuleID = 10;

                    objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                    if (clsCurrentUser.ModuleID != AppModuleID)
                    {
                        if (clsCurrentUser.ModuleID != 1)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    frmStateMaster frmStateMaster = new frmStateMaster();
                    frmStateMaster.ShowDialog(this);
                    break;
                case "cmbRelationshipList":
                    AppModuleID = 10;

                    objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                    if (clsCurrentUser.ModuleID != AppModuleID)
                    {
                        if (clsCurrentUser.ModuleID != 1)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster();
                    frmRelationshipMaster.ShowDialog(this);
                    break;

                case "cmbSalaryProfile":
                    AppModuleID = 10;

                    objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                    if (clsCurrentUser.ModuleID != AppModuleID)
                    {
                        if (clsCurrentUser.ModuleID != 1)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    frmRelationshipMaster frmRelationshipMaster1 = new frmRelationshipMaster();
                    frmRelationshipMaster1.ShowDialog(this);
                    break;
            }
        }

        private void cmbCompanyList01_Click_1(object sender, EventArgs e)
        {
            AppModuleID = 9;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster();
            frmLastCompanyMaster.ShowDialog(this);
        }

        private void cmbEducationList01_Click(object sender, EventArgs e)
        {
            AppModuleID = 10;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            frmEduQualMaster frmEduQualMaster = new frmEduQualMaster();
            frmEduQualMaster.ShowDialog(this);
        }

        private void cmbSkillsList01_Click(object sender, EventArgs e)
        {
            AppModuleID = 10;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            frmSkillsMaster frmSkillsMaster = new frmSkillsMaster();
            frmSkillsMaster.Show(this);
        }

        private void cmbDepartmentList01_Click(object sender, EventArgs e)
        {
            AppModuleID = 10;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster();
            frmDepartmentMaster.ShowDialog(this);
        }

        private void cmbDesignationList01_Click(object sender, EventArgs e)
        {
            AppModuleID = 10;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            frmDesignationMaster frmDesignationMaster = new frmDesignationMaster();
            frmDesignationMaster.ShowDialog(this);
        }

        private void cmbStatesList01_Click(object sender, EventArgs e)
        {
            AppModuleID = 10;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            frmStateMaster frmStateMaster = new frmStateMaster();
            frmStateMaster.ShowDialog(this);
        }

        private void cmbRelationshipList01_Click(object sender, EventArgs e)
        {
            AppModuleID = 10;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster();
            frmRelationshipMaster.ShowDialog(this);
        }

        private void cmbPayrollSystem_Click(object sender, EventArgs e)
        {
            AppModuleID = 3;

            objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (clsCurrentUser.ModuleID != AppModuleID)
            {
                if (clsCurrentUser.ModuleID != 1)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            frmPayrollMaster payrollMaster = new frmPayrollMaster();
            payrollMaster.ShowDialog();
        }

        private void cmbEarningsList_Click(object sender, EventArgs e)
        {
            frmPayrollAllowences frmPayrollAllowences = new frmPayrollAllowences();
            frmPayrollAllowences.ShowDialog(this);
        }

        private void cmbDeductionsList_Click(object sender, EventArgs e)
        {
            frmPayrollDeductions frmPayrollDeductions = new frmPayrollDeductions();
            frmPayrollDeductions.ShowDialog(this);
        }

        private void cmbReimbursmentList_Click(object sender, EventArgs e)
        {
            frmReimbursement frmReimbursement = new frmReimbursement();
            frmReimbursement.ShowDialog(this);
        }

        private void cmbSalaryProfile_Click(object sender, EventArgs e)
        {

        }

        private void cmbLeaveStatement_Click(object sender, EventArgs e)
        {
            frmLeaveStatement frmLeaveStatement = new frmLeaveStatement();
            frmLeaveStatement.ShowDialog(this);
        }

        private void tlbCompanyInfo_Click(object sender, EventArgs e)
        {
            frmCompanyInfo frmCompanyInfo = new frmCompanyInfo();
            frmCompanyInfo.ShowDialog(this);
        }

        private void cmbUpdateAddressInfo_Click(object sender, EventArgs e)
        {
            frmUpdateCurrentUserInfo frmUpdateCurrentUserInfo = new frmUpdateCurrentUserInfo();
            frmUpdateCurrentUserInfo.ShowDialog(this);
        }

        private void cmbApplyLeave_Click(object sender, EventArgs e)
        {
            frmCurrentUserLeaveMaster frmCurrentUserLeaveMaster = new frmCurrentUserLeaveMaster();
            frmCurrentUserLeaveMaster.ShowDialog(this);
        }

        private void cmbIndividualLeaveStatement_Click(object sender, EventArgs e)
        {
            frmLeaveStatement frmLeaveStatement = new frmLeaveStatement();
            frmLeaveStatement.ShowDialog(this);
        }
    }
}
