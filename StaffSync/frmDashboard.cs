using ModelStaffSync;
using Org.BouncyCastle.Ocsp;
using Quartz;
using Quartz.Impl;
using StaffSyncJobs;
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
//using static C1.Util.Win.Win32;
using static System.Windows.Forms.AxHost;

namespace StaffSync
{
    public partial class frmDashboard : Form
    {
        private IScheduler scheduler;
        private IScheduler _scheduler;

        public int AppModuleID = 0;
        DALStaffSync.clsCurrentUserInfo objCurrentUserInfo = new DALStaffSync.clsCurrentUserInfo();
        DALStaffSync.clsBirthdayList objBirthdayList = new DALStaffSync.clsBirthdayList();
        //Download objDownload = new Download();
        DALStaffSync.clsLeaveTRList objLeaveTRList = new DALStaffSync.clsLeaveTRList();
        DALStaffSync.clsLeaveTRList objLeaveInfo = new DALStaffSync.clsLeaveTRList();
        DALStaffSync.clsClientInfo objClientInfo = new DALStaffSync.clsClientInfo();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = new UserRolesAndResponsibilitiesInfo();

        private async void InitializeScheduler()
        {
            //scheduler = await new StdSchedulerFactory().GetScheduler();
            //await scheduler.Start();

            //IJobDetail jobDailyAttendance = JobBuilder.Create<DailyAttendanceJob>().WithIdentity("DailyAttendanceJob", "grpDailyAttendanceJob").Build();
            //ITrigger trgDailyAttendance = TriggerBuilder.Create().WithIdentity("trgDailyAttendanceJob", "grpDailyAttendanceJob").WithSimpleSchedule(x => x .WithIntervalInMinutes(1).RepeatForever().WithMisfireHandlingInstructionNextWithExistingCount()).Build();
            //await scheduler.ScheduleJob(jobDailyAttendance, trgDailyAttendance);


            //IJobDetail jobLeaveApproval = JobBuilder.Create<DailyLeavesJob>().WithIdentity("DailyLeavesJob", "grpDailyLeavesJob").Build();
            //ITrigger trgLeaveApproval = TriggerBuilder.Create().WithIdentity("trgDailyLeavesJob", "grpDailyLeavesJob").WithSimpleSchedule(x => x.WithIntervalInMinutes(1).RepeatForever().WithMisfireHandlingInstructionNextWithExistingCount()).Build();
            //await scheduler.ScheduleJob(jobLeaveApproval, trgLeaveApproval);


            //// Define the job and associate it with MyJob class
            //IJobDetail job = JobBuilder.Create<StaffSyncLeaveJobs>()
            //    .WithIdentity("myJob", "group1")
            //    .UsingJobData("EmpID", "1")
            //    .UsingJobData("LeaveTypeID", "1")
            //    .UsingJobData("LeaveActionType", "Approved")
            //    .UsingJobData("LeaveDateFrom", "21-Sep-2025")
            //    .UsingJobData("LeaveDateTo", "21-Sep-2025")
            //    .UsingJobData("LeaveDuration", "1")
            //    .UsingJobData("LeaveDurationType", "FullDay")

            //    //.UsingJobData("message", "Hello from Quartz.NET!")
            //    //.UsingJobData("value", 3.141f)
            //    .Build();

            //// Trigger it to run immediately
            //ITrigger trigger = TriggerBuilder.Create()
            //    .WithIdentity("myTrigger", "group1")
            //    .StartNow()
            //    .Build();

            //await _scheduler.ScheduleJob(job, trigger);
        }

        public frmDashboard(int EmpID)
        {
            InitializeComponent();
            InitializeScheduler();

            objLogin.getLoggedInUserInfo(EmpID);

            List<ClientInfo> objActiveClientInfo = objClientInfo.getClientInfo(1);

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(EmpID);
        }

        private void employeeDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manageEmployeeDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));

            AppModuleID = 2;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Employee Details";
                    frmEmployeeMaster frmEmployeeMasterDetails = new frmEmployeeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmEmployeeMasterDetails.MdiParent = this;
                    frmEmployeeMasterDetails.Dock = DockStyle.Fill;
                    frmEmployeeMasterDetails.Show();
                    frmEmployeeMasterDetails.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Employee Details";
                    frmEmployeeMaster frmEmployeeMasterDetails = new frmEmployeeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmEmployeeMasterDetails.MdiParent = this;
                    frmEmployeeMasterDetails.Dock = DockStyle.Fill;
                    frmEmployeeMasterDetails.Show();
                    frmEmployeeMasterDetails.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                ////objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Employee Details";
                    frmEmployeeMaster frmEmployeeMasterDetails = new frmEmployeeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmEmployeeMasterDetails.MdiParent = this;
                    frmEmployeeMasterDetails.Dock = DockStyle.Fill;
                    frmEmployeeMasterDetails.Show();
                    frmEmployeeMasterDetails.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void manageEmployeeAttendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));

            AppModuleID = 4;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Attendance Master Details";
                    frmAttendanceMater frmAttendanceMaterDetails = new frmAttendanceMater(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmAttendanceMaterDetails.MdiParent = this;
                    frmAttendanceMaterDetails.Dock = DockStyle.Fill;
                    frmAttendanceMaterDetails.Show();
                    frmAttendanceMaterDetails.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Attendance Master Details";
                    frmAttendanceMater frmAttendanceMaterDetails = new frmAttendanceMater(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmAttendanceMaterDetails.MdiParent = this;
                    frmAttendanceMaterDetails.Dock = DockStyle.Fill;
                    frmAttendanceMaterDetails.Show();
                    frmAttendanceMaterDetails.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                ////objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Attendance Master Details";
                    frmAttendanceMater frmAttendanceMaterDetails = new frmAttendanceMater(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmAttendanceMaterDetails.MdiParent = this;
                    frmAttendanceMaterDetails.Dock = DockStyle.Fill;
                    frmAttendanceMaterDetails.Show();
                    frmAttendanceMaterDetails.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void manageEmployeePayrollToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));

            AppModuleID = 3;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {

            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Employee Details";
                    frmEmployeeMaster frmEmployeeMasterDetails = new frmEmployeeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmEmployeeMasterDetails.MdiParent = this;
                    frmEmployeeMasterDetails.Dock = DockStyle.Fill;
                    frmEmployeeMasterDetails.Show();
                    frmEmployeeMasterDetails.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                ////objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //frmPayrollMaster frmPayrollMasterDetails = new frmPayrollMaster();
                //frmPayrollMasterDetails.Show();
            }
        }

        private void manageEmployeeLeavesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 5;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Leave Master Details";
                    frmLeavesMaster frmLeavesMasterDetails = new frmLeavesMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmLeavesMasterDetails.MdiParent = this;
                    frmLeavesMasterDetails.Dock = DockStyle.Fill;
                    frmLeavesMasterDetails.Show();
                    frmLeavesMasterDetails.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Leave Master Details";
                    frmLeavesMaster frmLeavesMasterDetails = new frmLeavesMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmLeavesMasterDetails.MdiParent = this;
                    frmLeavesMasterDetails.Dock = DockStyle.Fill;
                    frmLeavesMasterDetails.Show();
                    frmLeavesMasterDetails.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                ////objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Leave Master Details";
                    frmLeavesMaster frmLeavesMasterDetails = new frmLeavesMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmLeavesMasterDetails.MdiParent = this;
                    frmLeavesMasterDetails.Dock = DockStyle.Fill;
                    frmLeavesMasterDetails.Show();
                    frmLeavesMasterDetails.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void employeeWiseReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Employee Wise Reports";
                frmEmployeeWiseReports frmEmployeeWiseReportsDetails = new frmEmployeeWiseReports(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmEmployeeWiseReportsDetails.MdiParent = this;
                frmEmployeeWiseReportsDetails.Dock = DockStyle.Fill;
                frmEmployeeWiseReportsDetails.Show();
                frmEmployeeWiseReportsDetails.WindowState = FormWindowState.Maximized;
            }
        }

        private void employeeAttendanceReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Employee Attendance Reports";
                frmEmployeeAttendanceReports frmEmployeeAttendanceReportsDetails = new frmEmployeeAttendanceReports(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmEmployeeAttendanceReportsDetails.MdiParent = this;
                frmEmployeeAttendanceReportsDetails.Dock = DockStyle.Fill;
                frmEmployeeAttendanceReportsDetails.Show();
                frmEmployeeAttendanceReportsDetails.WindowState = FormWindowState.Maximized;

            }
        }

        private void employeePayrollReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Employee Payroll Reports";
                frmEmployeePayrollReports frmEmployeePayrollReportsDetails = new frmEmployeePayrollReports(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmEmployeePayrollReportsDetails.MdiParent = this;
                frmEmployeePayrollReportsDetails.Dock = DockStyle.Fill;
                frmEmployeePayrollReportsDetails.Show();
                frmEmployeePayrollReportsDetails.WindowState = FormWindowState.Maximized;
            }
        }

        private void employeeLeavesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Employee Leave Reports";
                frmEmployeeLeavesReports frmEmployeeLeavesReportsDetails = new frmEmployeeLeavesReports(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmEmployeeLeavesReportsDetails.MdiParent = this;
                frmEmployeeLeavesReportsDetails.Dock = DockStyle.Fill;
                frmEmployeeLeavesReportsDetails.Show();
                frmEmployeeLeavesReportsDetails.WindowState = FormWindowState.Maximized;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "StaffSync - About";
                frmAbout frmAboutDetails = new frmAbout();
                frmAboutDetails.MdiParent = this;
                frmAboutDetails.Dock = DockStyle.Fill;
                frmAboutDetails.Show();
                frmAboutDetails.WindowState = FormWindowState.Maximized;
            }
        }

        private void departmentListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Department Master Details";
                frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmDepartmentMaster.MdiParent = this;
                frmDepartmentMaster.Dock = DockStyle.Fill;
                frmDepartmentMaster.Show();
                frmDepartmentMaster.WindowState = FormWindowState.Maximized;
            }
        }

        private void countriesListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Country Master Details";
                frmCountryMaster frmCountryMaster = new frmCountryMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmCountryMaster.MdiParent = this;
                frmCountryMaster.Dock = DockStyle.Fill;
                frmCountryMaster.Show();
                frmCountryMaster.WindowState = FormWindowState.Maximized;
            }
        }

        private void designationListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Designation Master Details";
                frmDesignationMaster frmDesignationMaster = new frmDesignationMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmDesignationMaster.MdiParent = this;
                frmDesignationMaster.Dock = DockStyle.Fill;
                frmDesignationMaster.Show();
                frmDesignationMaster.WindowState = FormWindowState.Maximized;
            }
        }

        private void countriesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "State Master Details";
                frmStateMaster frmStateMaster = new frmStateMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmStateMaster.MdiParent = this;
                frmStateMaster.Dock = DockStyle.Fill;
                frmStateMaster.Show();
                frmStateMaster.WindowState = FormWindowState.Maximized;
            }
        }

        private void relationshipListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Relationship Master Details";
                frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmRelationshipMaster.MdiParent = this;
                frmRelationshipMaster.Dock = DockStyle.Fill;
                frmRelationshipMaster.Show();
                frmRelationshipMaster.WindowState = FormWindowState.Maximized;
            }
        }

        private void companyDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Last Company Master Details";
                frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmLastCompanyMaster.MdiParent = this;
                frmLastCompanyMaster.Dock = DockStyle.Fill;
                frmLastCompanyMaster.Show();
                frmLastCompanyMaster.WindowState = FormWindowState.Maximized;
            }
        }

        private void educationListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Qualification Master Details";
                frmEduQualMaster frmEduQualMaster = new frmEduQualMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmEduQualMaster.MdiParent = this;
                frmEduQualMaster.Dock = DockStyle.Fill;
                frmEduQualMaster.Show();
                frmEduQualMaster.WindowState = FormWindowState.Maximized;
            }
        }

        private void departmentListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));

            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Department Master Details";
                    frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmDepartmentMaster.MdiParent = this;
                    frmDepartmentMaster.Dock = DockStyle.Fill;
                    frmDepartmentMaster.Show();
                    frmDepartmentMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Department Master Details";
                    frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmDepartmentMaster.MdiParent = this;
                    frmDepartmentMaster.Dock = DockStyle.Fill;
                    frmDepartmentMaster.Show();
                    frmDepartmentMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                ////objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Department Master Details";
                    frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmDepartmentMaster.MdiParent = this;
                    frmDepartmentMaster.Dock = DockStyle.Fill;
                    frmDepartmentMaster.Show();
                    frmDepartmentMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void mainListingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void designationListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Designation Master Details";
                    frmDesignationMaster frmDesignationMaster = new frmDesignationMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmDesignationMaster.MdiParent = this;
                    frmDesignationMaster.Dock = DockStyle.Fill;
                    frmDesignationMaster.Show();
                    frmDesignationMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Designation Master Details";
                    frmDesignationMaster frmDesignationMaster = new frmDesignationMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmDesignationMaster.MdiParent = this;
                    frmDesignationMaster.Dock = DockStyle.Fill;
                    frmDesignationMaster.Show();
                    frmDesignationMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                ////objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Designation Master Details";
                    frmDesignationMaster frmDesignationMaster = new frmDesignationMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmDesignationMaster.MdiParent = this;
                    frmDesignationMaster.Dock = DockStyle.Fill;
                    frmDesignationMaster.Show();
                    frmDesignationMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void educationListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Qualification Master Details";
                    frmEduQualMaster frmEduQualMaster = new frmEduQualMaster();
                    frmEduQualMaster.MdiParent = this;
                    frmEduQualMaster.Dock = DockStyle.Fill;
                    frmEduQualMaster.Show();
                    frmEduQualMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Qualification Master Details";
                    frmEduQualMaster frmEduQualMaster = new frmEduQualMaster();
                    frmEduQualMaster.MdiParent = this;
                    frmEduQualMaster.Dock = DockStyle.Fill;
                    frmEduQualMaster.Show();
                    frmEduQualMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                ////objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Qualification Master Details";
                    frmEduQualMaster frmEduQualMaster = new frmEduQualMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmEduQualMaster.MdiParent = this;
                    frmEduQualMaster.Dock = DockStyle.Fill;
                    frmEduQualMaster.Show();
                    frmEduQualMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void relationshipListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Relationship Master Details";
                    frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmRelationshipMaster.MdiParent = this;
                    frmRelationshipMaster.Dock = DockStyle.Fill;
                    frmRelationshipMaster.Show();
                    frmRelationshipMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Relationship Master Details";
                    frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmRelationshipMaster.MdiParent = this;
                    frmRelationshipMaster.Dock = DockStyle.Fill;
                    frmRelationshipMaster.Show();
                    frmRelationshipMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                ////objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Relationship Master Details";
                    frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmRelationshipMaster.MdiParent = this;
                    frmRelationshipMaster.Dock = DockStyle.Fill;
                    frmRelationshipMaster.Show();
                    frmRelationshipMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void statesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "State Master Details";
                    frmStateMaster frmStateMaster = new frmStateMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmStateMaster.MdiParent = this;
                    frmStateMaster.Dock = DockStyle.Fill;
                    frmStateMaster.Show();
                    frmStateMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "State Master Details";
                    frmStateMaster frmStateMaster = new frmStateMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmStateMaster.MdiParent = this;
                    frmStateMaster.Dock = DockStyle.Fill;
                    frmStateMaster.Show();
                    frmStateMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                ////objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "State Master Details";
                    frmStateMaster frmStateMaster = new frmStateMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmStateMaster.MdiParent = this;
                    frmStateMaster.Dock = DockStyle.Fill;
                    frmStateMaster.Show();
                    frmStateMaster.WindowState = FormWindowState.Maximized;

                }
            }
        }

        private void countriesListToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Country Master Details";
                    frmCountryMaster frmCountryMaster = new frmCountryMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmCountryMaster.MdiParent = this;
                    frmCountryMaster.Dock = DockStyle.Fill;
                    frmCountryMaster.Show();
                    frmCountryMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Country Master Details";
                    frmCountryMaster frmCountryMaster = new frmCountryMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmCountryMaster.MdiParent = this;
                    frmCountryMaster.Dock = DockStyle.Fill;
                    frmCountryMaster.Show();
                    frmCountryMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                ////objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Country Master Details";
                    frmCountryMaster frmCountryMaster = new frmCountryMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmCountryMaster.MdiParent = this;
                    frmCountryMaster.Dock = DockStyle.Fill;
                    frmCountryMaster.Show();
                    frmCountryMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void companyListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Last Company Master Details";
                    frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmLastCompanyMaster.MdiParent = this;
                    frmLastCompanyMaster.Dock = DockStyle.Fill;
                    frmLastCompanyMaster.Show();
                    frmLastCompanyMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Last Company Master Details";
                    frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmLastCompanyMaster.MdiParent = this;
                    frmLastCompanyMaster.Dock = DockStyle.Fill;
                    frmLastCompanyMaster.Show();
                    frmLastCompanyMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                ////objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Last Company Master Details";
                    frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmLastCompanyMaster.MdiParent = this;
                    frmLastCompanyMaster.Dock = DockStyle.Fill;
                    frmLastCompanyMaster.Show();
                    frmLastCompanyMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void skillsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    frmSkillsMaster frmSkillsMaster = new frmSkillsMaster();
                    frmSkillsMaster.MdiParent = this;
                    frmSkillsMaster.Dock = DockStyle.Fill;
                    frmSkillsMaster.Show();
                    frmSkillsMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    frmSkillsMaster frmSkillsMaster = new frmSkillsMaster();
                    frmSkillsMaster.MdiParent = this;
                    frmSkillsMaster.Dock = DockStyle.Fill;
                    frmSkillsMaster.Show();
                    frmSkillsMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                ////objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                frmSkillsMaster frmSkillsMaster = new frmSkillsMaster();
                frmSkillsMaster.MdiParent = this;
                frmSkillsMaster.Dock = DockStyle.Fill;
                frmSkillsMaster.Show();
                frmSkillsMaster.WindowState = FormWindowState.Maximized;
            }
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            if (clsCurrentUser.UserName == null || clsCurrentUser.UserName == "")
            {
                myStatusBar.Items[0].Text = "User Name : ";
                myStatusBar.Items[1].Text = "Log In : " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");
            }
            else
            {
                myStatusBar.Items[0].Text = "User Name : " + clsCurrentUser.UserName == null ? "" : clsCurrentUser.UserName.ToString();
                myStatusBar.Items[1].Text = "Log In : " + clsCurrentUser.LoginDateTime.ToString("dd-MMM-yyyy hh:mm:ss tt");
            }

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
            //frmEmployeeMasterDetails.Show();
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
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 5;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Leave Approval Details";
                    frmLeavesApproval frmLeavesApproval = new frmLeavesApproval(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmLeavesApproval.MdiParent = this;
                    frmLeavesApproval.Dock = DockStyle.Fill;
                    frmLeavesApproval.Show();
                    frmLeavesApproval.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Leave Approval Details";
                    frmLeavesApproval frmLeavesApproval = new frmLeavesApproval(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmLeavesApproval.MdiParent = this;
                    frmLeavesApproval.Dock = DockStyle.Fill;
                    frmLeavesApproval.Show();
                    frmLeavesApproval.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                ////objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Leave Approval Details";
                    frmLeavesApproval frmLeavesApproval = new frmLeavesApproval(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmLeavesApproval.MdiParent = this;
                    frmLeavesApproval.Dock = DockStyle.Fill;
                    frmLeavesApproval.Show();
                    frmLeavesApproval.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 9;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "User Management Details";
                    frmUserManagement frmUserManagement = new frmUserManagement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmUserManagement.MdiParent = this;
                    frmUserManagement.Dock = DockStyle.Fill;
                    frmUserManagement.Show();
                    frmUserManagement.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "User Management Details";
                    frmUserManagement frmUserManagement = new frmUserManagement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmUserManagement.MdiParent = this;
                    frmUserManagement.Dock = DockStyle.Fill;
                    frmUserManagement.Show();
                    frmUserManagement.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                ////objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "User Management Details";
                    frmUserManagement frmUserManagement = new frmUserManagement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmUserManagement.MdiParent = this;
                    frmUserManagement.Dock = DockStyle.Fill;
                    frmUserManagement.Show();
                    frmUserManagement.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void rolesAndResponsibilitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 9;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Roles and Responsibilities Details";
                    frmRolesAndResponsibilities frmRolesAndResponsibilities = new frmRolesAndResponsibilities(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmRolesAndResponsibilities.MdiParent = this;
                    frmRolesAndResponsibilities.Dock = DockStyle.Fill;
                    frmRolesAndResponsibilities.Show();
                    frmRolesAndResponsibilities.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Roles and Responsibilities Details";
                    frmRolesAndResponsibilities frmRolesAndResponsibilities = new frmRolesAndResponsibilities(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmRolesAndResponsibilities.MdiParent = this;
                    frmRolesAndResponsibilities.Dock = DockStyle.Fill;
                    frmRolesAndResponsibilities.Show();
                    frmRolesAndResponsibilities.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                ////objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Roles and Responsibilities Details";
                    frmRolesAndResponsibilities frmRolesAndResponsibilities = new frmRolesAndResponsibilities(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmRolesAndResponsibilities.MdiParent = this;
                    frmRolesAndResponsibilities.Dock = DockStyle.Fill;
                    frmRolesAndResponsibilities.Show();
                    frmRolesAndResponsibilities.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 9;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Modules Assignment Details";
                    frmModuleAssignment frmModuleAssignment = new frmModuleAssignment(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmModuleAssignment.MdiParent = this;
                    frmModuleAssignment.Dock = DockStyle.Fill;
                    frmModuleAssignment.Show();
                    frmModuleAssignment.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Modules Assignment Details";
                    frmModuleAssignment frmModuleAssignment = new frmModuleAssignment(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmModuleAssignment.MdiParent = this;
                    frmModuleAssignment.Dock = DockStyle.Fill;
                    frmModuleAssignment.Show();
                    frmModuleAssignment.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Modules Assignment Details";
                    frmModuleAssignment frmModuleAssignment = new frmModuleAssignment(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmModuleAssignment.MdiParent = this;
                    frmModuleAssignment.Dock = DockStyle.Fill;
                    frmModuleAssignment.Show();
                    frmModuleAssignment.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void assetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 6;

            //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
            {
                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void assetsAllotmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppModuleID = 6;

            //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
            {
                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void roleProfileManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 8;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Roles Profile Master Details";
                    frmRolesProfileMaster frmRolesProfileMaster = new frmRolesProfileMaster();
                    frmRolesProfileMaster.MdiParent = this;
                    frmRolesProfileMaster.Dock = DockStyle.Fill;
                    frmRolesProfileMaster.Show();
                    frmRolesProfileMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Roles Profile Master Details";
                    frmRolesProfileMaster frmRolesProfileMaster = new frmRolesProfileMaster();
                    frmRolesProfileMaster.MdiParent = this;
                    frmRolesProfileMaster.Dock = DockStyle.Fill;
                    frmRolesProfileMaster.Show();
                    frmRolesProfileMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Roles Profile Master Details";
                    frmRolesProfileMaster frmRolesProfileMaster = new frmRolesProfileMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmRolesProfileMaster.MdiParent = this;
                    frmRolesProfileMaster.Dock = DockStyle.Fill;
                    frmRolesProfileMaster.Show();
                    frmRolesProfileMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void tlbExitButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit.?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            Application.Exit();
        }

        private void frmDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void tlbManageEmployeeInfoButton_Click(object sender, EventArgs e)
        {
            //AppModuleID = 2;
            //if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            //{
            //    frmEmployeeMaster frmEmployeeMasterDetails = new frmEmployeeMaster();
            //    frmEmployeeMasterDetails.Show();
            //}
            //else
            //{
            //    //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
            //    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
            //    {
            //        //if (clsCurrentUser.ModuleID != 1)
            //        {
            //            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            return;
            //        }
            //    }

            //    frmEmployeeMaster frmEmployeeMasterDetails = new frmEmployeeMaster();
            //    frmEmployeeMasterDetails.Show();
            //}
        }

        private void tlbManageEmployeeAttendanceButton_Click(object sender, EventArgs e)
        {

        }

        private void cmUserManagementList_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag == null)
                return;

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));

            switch (e.ClickedItem.Tag.ToString())
            {
                case "cmbManageUsers":
                    AppModuleID = 9;

                    if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "User Management Details";
                            frmUserManagement frmUserManagement = new frmUserManagement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmUserManagement.MdiParent = this;
                            frmUserManagement.Dock = DockStyle.Fill;
                            frmUserManagement.Show();
                            frmUserManagement.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "User Management Details";
                            frmUserManagement frmUserManagement = new frmUserManagement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmUserManagement.MdiParent = this;
                            frmUserManagement.Dock = DockStyle.Fill;
                            frmUserManagement.Show();
                            frmUserManagement.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else
                    {
                        //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "User Management Details";
                            frmUserManagement frmUserManagement = new frmUserManagement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmUserManagement.MdiParent = this;
                            frmUserManagement.Dock = DockStyle.Fill;
                            frmUserManagement.Show();
                            frmUserManagement.WindowState = FormWindowState.Maximized;
                        }
                    }
                    break;

                case "cmbRoleAssignment":
                    AppModuleID = 9;

                    if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Roles and Responsibilities Details";
                            frmRolesAndResponsibilities frmRolesAndResponsibilities = new frmRolesAndResponsibilities(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmRolesAndResponsibilities.MdiParent = this;
                            frmRolesAndResponsibilities.Dock = DockStyle.Fill;
                            frmRolesAndResponsibilities.Show();
                            frmRolesAndResponsibilities.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
                    {
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Roles and Responsibilities Details";
                            frmRolesAndResponsibilities frmRolesAndResponsibilities = new frmRolesAndResponsibilities(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmRolesAndResponsibilities.MdiParent = this;
                            frmRolesAndResponsibilities.Dock = DockStyle.Fill;
                            frmRolesAndResponsibilities.Show();
                            frmRolesAndResponsibilities.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else
                    {
                        //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Roles and Responsibilities Details";
                            frmRolesAndResponsibilities frmRolesAndResponsibilities = new frmRolesAndResponsibilities(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmRolesAndResponsibilities.MdiParent = this;
                            frmRolesAndResponsibilities.Dock = DockStyle.Fill;
                            frmRolesAndResponsibilities.Show();
                            frmRolesAndResponsibilities.WindowState = FormWindowState.Maximized;
                        }
                    }
                    break;

                case "cmbModuleAssignment":
                    AppModuleID = 9;

                    if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Module Assignment Details";
                            frmModuleAssignment frmModuleAssignment = new frmModuleAssignment(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmModuleAssignment.MdiParent = this;
                            frmModuleAssignment.Dock = DockStyle.Fill;
                            frmModuleAssignment.Show();
                            frmModuleAssignment.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Module Assignment Details";
                            frmModuleAssignment frmModuleAssignment = new frmModuleAssignment(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmModuleAssignment.MdiParent = this;
                            frmModuleAssignment.Dock = DockStyle.Fill;
                            frmModuleAssignment.Show();
                            frmModuleAssignment.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else
                    {
                        //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Module Assignment Details";
                            frmModuleAssignment frmModuleAssignment = new frmModuleAssignment(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmModuleAssignment.MdiParent = this;
                            frmModuleAssignment.Dock = DockStyle.Fill;
                            frmModuleAssignment.Show();
                            frmModuleAssignment.WindowState = FormWindowState.Maximized;
                        }
                    }
                    break;

                case "cmbRoleProfilement":
                    AppModuleID = 10;

                    if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Roles Profile Master Details";
                            frmRolesProfileMaster frmRolesProfileMaster = new frmRolesProfileMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmRolesProfileMaster.MdiParent = this;
                            frmRolesProfileMaster.Dock = DockStyle.Fill;
                            frmRolesProfileMaster.Show();
                            frmRolesProfileMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Roles Profile Master Details";
                            frmRolesProfileMaster frmRolesProfileMaster = new frmRolesProfileMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmRolesProfileMaster.MdiParent = this;
                            frmRolesProfileMaster.Dock = DockStyle.Fill;
                            frmRolesProfileMaster.Show();
                            frmRolesProfileMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else
                    {
                        //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Roles Profile Master Details";
                            frmRolesProfileMaster frmRolesProfileMaster = new frmRolesProfileMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmRolesProfileMaster.MdiParent = this;
                            frmRolesProfileMaster.Dock = DockStyle.Fill;
                            frmRolesProfileMaster.Show();
                            frmRolesProfileMaster.WindowState = FormWindowState.Maximized;
                        }
                    }

                    break;
            }
        }

        private void cmAttendanceManagementList_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag == null)
                return;

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));

            switch (e.ClickedItem.Tag.ToString())
            {
                case "cmbManageEmployeeAttendance":
                    AppModuleID = 4;

                    if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Attendance Master Details";
                            frmAttendanceMater frmAttendanceMaterDetails = new frmAttendanceMater(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmAttendanceMaterDetails.MdiParent = this;
                            frmAttendanceMaterDetails.Dock = DockStyle.Fill;
                            frmAttendanceMaterDetails.Show();
                            frmAttendanceMaterDetails.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Attendance Master Details";
                            frmAttendanceMater frmAttendanceMaterDetails = new frmAttendanceMater(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmAttendanceMaterDetails.MdiParent = this;
                            frmAttendanceMaterDetails.Dock = DockStyle.Fill;
                            frmAttendanceMaterDetails.Show();
                            frmAttendanceMaterDetails.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else
                    {
                        //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Attendance Master Details";
                            frmAttendanceMater frmAttendanceMaterDetails = new frmAttendanceMater(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmAttendanceMaterDetails.MdiParent = this;
                            frmAttendanceMaterDetails.Dock = DockStyle.Fill;
                            frmAttendanceMaterDetails.Show();
                            frmAttendanceMaterDetails.WindowState = FormWindowState.Maximized;
                        }
                    }
                    break;
            }
        }

        private void cmLeaveManagement_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag == null)
                return;

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));

            switch (e.ClickedItem.Tag.ToString())
            {
                case "cmbLeaveRequest":
                    AppModuleID = 5;

                    if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Leaves Master Details";
                            frmLeavesMaster frmLeavesMasterDetails = new frmLeavesMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmLeavesMasterDetails.MdiParent = this;
                            frmLeavesMasterDetails.Dock = DockStyle.Fill;
                            frmLeavesMasterDetails.Show();
                            frmLeavesMasterDetails.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Leaves Master Details";
                            frmLeavesMaster frmLeavesMasterDetails = new frmLeavesMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmLeavesMasterDetails.MdiParent = this;
                            frmLeavesMasterDetails.Dock = DockStyle.Fill;
                            frmLeavesMasterDetails.Show();
                            frmLeavesMasterDetails.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else
                    {
                        //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Leaves Master Details";
                            frmLeavesMaster frmLeavesMasterDetails = new frmLeavesMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmLeavesMasterDetails.MdiParent = this;
                            frmLeavesMasterDetails.Dock = DockStyle.Fill;
                            frmLeavesMasterDetails.Show();
                            frmLeavesMasterDetails.WindowState = FormWindowState.Maximized;
                        }
                    }
                    cmbLeaveApproval.Text = "Leave Approval (" + objLeaveInfo.getPendingLeaveApprovalList().Count + ")";
                    cmbLeaveReject.Text = "Leave Reject (" + objLeaveInfo.getPendingLeaveApprovalList().Count + ")";
                    break;
                case "cmbLeaveApproval":
                    AppModuleID = 5;

                    if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Leaves Approval Details";
                            frmLeavesApproval frmLeavesApproval = new frmLeavesApproval(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmLeavesApproval.MdiParent = this;
                            frmLeavesApproval.Dock = DockStyle.Fill;
                            frmLeavesApproval.Show();
                            frmLeavesApproval.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Leaves Approval Details";
                            frmLeavesApproval frmLeavesApproval = new frmLeavesApproval(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmLeavesApproval.MdiParent = this;
                            frmLeavesApproval.Dock = DockStyle.Fill;
                            frmLeavesApproval.Show();
                            frmLeavesApproval.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else
                    {
                        //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Leaves Approval Details";
                            frmLeavesApproval frmLeavesApproval = new frmLeavesApproval(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmLeavesApproval.MdiParent = this;
                            frmLeavesApproval.Dock = DockStyle.Fill;
                            frmLeavesApproval.Show();
                            frmLeavesApproval.WindowState = FormWindowState.Maximized;

                        }
                    }
                    cmbLeaveApproval.Text = "Leave Approval (" + objLeaveInfo.getPendingLeaveApprovalList().Count + ")";
                    cmbLeaveReject.Text = "Leave Reject (" + objLeaveInfo.getPendingLeaveApprovalList().Count + ")";
                    break;
                case "cmbLeaveReject":
                    AppModuleID = 5;

                    if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Leaves Rejection Details";
                            frmLeavesReject frmLeavesReject = new frmLeavesReject(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmLeavesReject.MdiParent = this;
                            frmLeavesReject.Dock = DockStyle.Fill;
                            frmLeavesReject.Show();
                            frmLeavesReject.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Leaves Rejection Details";
                            frmLeavesReject frmLeavesReject = new frmLeavesReject(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmLeavesReject.MdiParent = this;
                            frmLeavesReject.Dock = DockStyle.Fill;
                            frmLeavesReject.Show();
                            frmLeavesReject.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else
                    {
                        //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Leaves Rejection Details";
                            frmLeavesReject frmLeavesReject = new frmLeavesReject(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmLeavesReject.MdiParent = this;
                            frmLeavesReject.Dock = DockStyle.Fill;
                            frmLeavesReject.Show();
                            frmLeavesReject.WindowState = FormWindowState.Maximized;
                        }
                    }
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
            if (e.ClickedItem.Tag == null)
                return;

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));

            switch (e.ClickedItem.Tag.ToString())
            {
                case "cmbCompanyList":
                    AppModuleID = 10;

                    if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Last Company Master Details";
                            frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmLastCompanyMaster.MdiParent = this;
                            frmLastCompanyMaster.Dock = DockStyle.Fill;
                            frmLastCompanyMaster.Show();
                            frmLastCompanyMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Last Company Master Details";
                            frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmLastCompanyMaster.MdiParent = this;
                            frmLastCompanyMaster.Dock = DockStyle.Fill;
                            frmLastCompanyMaster.Show();
                            frmLastCompanyMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else
                    {
                        //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Last Company Master Details";
                            frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmLastCompanyMaster.MdiParent = this;
                            frmLastCompanyMaster.Dock = DockStyle.Fill;
                            frmLastCompanyMaster.Show();
                            frmLastCompanyMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    break;
                case "cmbEducationList":
                    AppModuleID = 10;

                    if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Qualification Master Details";
                            frmEduQualMaster frmEduQualMaster = new frmEduQualMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmEduQualMaster.MdiParent = this;
                            frmEduQualMaster.Dock = DockStyle.Fill;
                            frmEduQualMaster.Show();
                            frmEduQualMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Qualification Master Details";
                            frmEduQualMaster frmEduQualMaster = new frmEduQualMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmEduQualMaster.MdiParent = this;
                            frmEduQualMaster.Dock = DockStyle.Fill;
                            frmEduQualMaster.Show();
                            frmEduQualMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else
                    {
                        //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Qualification Master Details";
                            frmEduQualMaster frmEduQualMaster = new frmEduQualMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmEduQualMaster.MdiParent = this;
                            frmEduQualMaster.Dock = DockStyle.Fill;
                            frmEduQualMaster.Show();
                            frmEduQualMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    break;
                case "cmbSkillsList":
                    AppModuleID = 10;

                    if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Skills Master Details";
                            frmSkillsMaster frmSkillsMaster = new frmSkillsMaster();
                            frmSkillsMaster.MdiParent = this;
                            frmSkillsMaster.Dock = DockStyle.Fill;
                            frmSkillsMaster.Show();
                            frmSkillsMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Skills Master Details";
                            frmSkillsMaster frmSkillsMaster = new frmSkillsMaster();
                            frmSkillsMaster.MdiParent = this;
                            frmSkillsMaster.Dock = DockStyle.Fill;
                            frmSkillsMaster.Show();
                            frmSkillsMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else
                    {
                        //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        frmSkillsMaster frmSkillsMaster = new frmSkillsMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                        frmSkillsMaster.MdiParent = this;
                        frmSkillsMaster.Dock = DockStyle.Fill;
                        frmSkillsMaster.Show();
                        frmSkillsMaster.WindowState = FormWindowState.Maximized;
                    }

                    break;
                case "cmbDepartmentList":
                    AppModuleID = 10;

                    if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Department Master Details";
                            frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmDepartmentMaster.MdiParent = this;
                            frmDepartmentMaster.Dock = DockStyle.Fill;
                            frmDepartmentMaster.Show();
                            frmDepartmentMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Department Master Details";
                            frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmDepartmentMaster.MdiParent = this;
                            frmDepartmentMaster.Dock = DockStyle.Fill;
                            frmDepartmentMaster.Show();
                            frmDepartmentMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else
                    {
                        //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Department Master Details";
                            frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmDepartmentMaster.MdiParent = this;
                            frmDepartmentMaster.Dock = DockStyle.Fill;
                            frmDepartmentMaster.Show();
                            frmDepartmentMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    break;
                case "cmbDesignationList":
                    AppModuleID = 10;

                    if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Designation Master Details";
                            frmDesignationMaster frmDesignationMaster = new frmDesignationMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmDesignationMaster.MdiParent = this;
                            frmDesignationMaster.Dock = DockStyle.Fill;
                            frmDesignationMaster.Show();
                            frmDesignationMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Designation Master Details";
                            frmDesignationMaster frmDesignationMaster = new frmDesignationMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmDesignationMaster.MdiParent = this;
                            frmDesignationMaster.Dock = DockStyle.Fill;
                            frmDesignationMaster.Show();
                            frmDesignationMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else
                    {
                        //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Designation Master Details";
                            frmDesignationMaster frmDesignationMaster = new frmDesignationMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmDesignationMaster.MdiParent = this;
                            frmDesignationMaster.Dock = DockStyle.Fill;
                            frmDesignationMaster.Show();
                            frmDesignationMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    break;
                case "cmbCountriesList":
                    AppModuleID = 10;

                    if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Country Master Details";
                            frmCountryMaster frmCountryMaster = new frmCountryMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmCountryMaster.MdiParent = this;
                            frmCountryMaster.Dock = DockStyle.Fill;
                            frmCountryMaster.Show();
                            frmCountryMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Country Master Details";
                            frmCountryMaster frmCountryMaster = new frmCountryMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmCountryMaster.MdiParent = this;
                            frmCountryMaster.Dock = DockStyle.Fill;
                            frmCountryMaster.Show();
                            frmCountryMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else
                    {
                        //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Country Master Details";
                            frmCountryMaster frmCountryMaster = new frmCountryMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmCountryMaster.MdiParent = this;
                            frmCountryMaster.Dock = DockStyle.Fill;
                            frmCountryMaster.Show();
                            frmCountryMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    break;
                case "cmbStatesList":
                    AppModuleID = 10;

                    if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "State Master Details";
                            frmStateMaster frmStateMaster = new frmStateMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmStateMaster.MdiParent = this;
                            frmStateMaster.Dock = DockStyle.Fill;
                            frmStateMaster.Show();
                            frmStateMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "State Master Details";
                            frmStateMaster frmStateMaster = new frmStateMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmStateMaster.MdiParent = this;
                            frmStateMaster.Dock = DockStyle.Fill;
                            frmStateMaster.Show();
                            frmStateMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else
                    {
                        //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "State Master Details";
                            frmStateMaster frmStateMaster = new frmStateMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmStateMaster.MdiParent = this;
                            frmStateMaster.Dock = DockStyle.Fill;
                            frmStateMaster.Show();
                            frmStateMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    break;
                case "cmbRelationshipList":
                    AppModuleID = 10;

                    if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Relationship Master Details";
                            frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmRelationshipMaster.MdiParent = this;
                            frmRelationshipMaster.Dock = DockStyle.Fill;
                            frmRelationshipMaster.Show();
                            frmRelationshipMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Relationship Master Details";
                            frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmRelationshipMaster.MdiParent = this;
                            frmRelationshipMaster.Dock = DockStyle.Fill;
                            frmRelationshipMaster.Show();
                            frmRelationshipMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else
                    {
                        //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Relationship Master Details";
                            frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmRelationshipMaster.MdiParent = this;
                            frmRelationshipMaster.Dock = DockStyle.Fill;
                            frmRelationshipMaster.Show();
                            frmRelationshipMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    break;
                case "cmbLeaveTypeMaster":
                    AppModuleID = 10;

                    if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Leave Type Master Details";
                            frmLeaveTypeMaster frmLeaveTypeMaster = new frmLeaveTypeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmLeaveTypeMaster.MdiParent = this;
                            frmLeaveTypeMaster.Dock = DockStyle.Fill;
                            frmLeaveTypeMaster.Show();
                            frmLeaveTypeMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Leave Type Master Details";
                            frmLeaveTypeMaster frmLeaveTypeMaster = new frmLeaveTypeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmLeaveTypeMaster.MdiParent = this;
                            frmLeaveTypeMaster.Dock = DockStyle.Fill;
                            frmLeaveTypeMaster.Show();
                            frmLeaveTypeMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else
                    {
                        //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Leave Type Master Details";
                            frmLeaveTypeMaster frmLeaveTypeMaster = new frmLeaveTypeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                            frmLeaveTypeMaster.MdiParent = this;
                            frmLeaveTypeMaster.Dock = DockStyle.Fill;
                            frmLeaveTypeMaster.Show();
                            frmLeaveTypeMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    break;
            }
        }

        private void cmbCompanyList01_Click_1(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 9;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Last Company Master Details";
                    frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmLastCompanyMaster.MdiParent = this;
                    frmLastCompanyMaster.Dock = DockStyle.Fill;
                    frmLastCompanyMaster.Show();
                    frmLastCompanyMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Last Company Master Details";
                    frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmLastCompanyMaster.MdiParent = this;
                    frmLastCompanyMaster.Dock = DockStyle.Fill;
                    frmLastCompanyMaster.Show();
                    frmLastCompanyMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {

                //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Last Company Master Details";
                    frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmLastCompanyMaster.MdiParent = this;
                    frmLastCompanyMaster.Dock = DockStyle.Fill;
                    frmLastCompanyMaster.Show();
                    frmLastCompanyMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbEducationList01_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Qualification Master Details";
                    frmEduQualMaster frmEduQualMaster = new frmEduQualMaster();
                    frmEduQualMaster.MdiParent = this;
                    frmEduQualMaster.Dock = DockStyle.Fill;
                    frmEduQualMaster.Show();
                    frmEduQualMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Qualification Master Details";
                    frmEduQualMaster frmEduQualMaster = new frmEduQualMaster();
                    frmEduQualMaster.MdiParent = this;
                    frmEduQualMaster.Dock = DockStyle.Fill;
                    frmEduQualMaster.Show();
                    frmEduQualMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Qualification Master Details";
                    frmEduQualMaster frmEduQualMaster = new frmEduQualMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmEduQualMaster.MdiParent = this;
                    frmEduQualMaster.Dock = DockStyle.Fill;
                    frmEduQualMaster.Show();
                    frmEduQualMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbSkillsList01_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Skills Master Details";
                    frmSkillsMaster frmSkillsMaster = new frmSkillsMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmSkillsMaster.MdiParent = this;
                    frmSkillsMaster.Dock = DockStyle.Fill;
                    frmSkillsMaster.Show();
                    frmSkillsMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Skills Master Details";
                    frmSkillsMaster frmSkillsMaster = new frmSkillsMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmSkillsMaster.MdiParent = this;
                    frmSkillsMaster.Dock = DockStyle.Fill;
                    frmSkillsMaster.Show();
                    frmSkillsMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                frmSkillsMaster frmSkillsMaster = new frmSkillsMaster();
                frmSkillsMaster.MdiParent = this;
                frmSkillsMaster.Dock = DockStyle.Fill;
                frmSkillsMaster.Show();
                frmSkillsMaster.WindowState = FormWindowState.Maximized;

            }
        }

        private void cmbDepartmentList01_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Department Master Details";
                    frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmDepartmentMaster.MdiParent = this;
                    frmDepartmentMaster.Dock = DockStyle.Fill;
                    frmDepartmentMaster.Show();
                    frmDepartmentMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Department Master Details";
                    frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmDepartmentMaster.MdiParent = this;
                    frmDepartmentMaster.Dock = DockStyle.Fill;
                    frmDepartmentMaster.Show();
                    frmDepartmentMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Department Master Details";
                    frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmDepartmentMaster.MdiParent = this;
                    frmDepartmentMaster.Dock = DockStyle.Fill;
                    frmDepartmentMaster.Show();
                    frmDepartmentMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbDesignationList01_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Designation Master Details";
                    frmDesignationMaster frmDesignationMaster = new frmDesignationMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmDesignationMaster.MdiParent = this;
                    frmDesignationMaster.Dock = DockStyle.Fill;
                    frmDesignationMaster.Show();
                    frmDesignationMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Designation Master Details";
                    frmDesignationMaster frmDesignationMaster = new frmDesignationMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmDesignationMaster.MdiParent = this;
                    frmDesignationMaster.Dock = DockStyle.Fill;
                    frmDesignationMaster.Show();
                    frmDesignationMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Designation Master Details";
                    frmDesignationMaster frmDesignationMaster = new frmDesignationMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmDesignationMaster.MdiParent = this;
                    frmDesignationMaster.Dock = DockStyle.Fill;
                    frmDesignationMaster.Show();
                    frmDesignationMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbStatesList01_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "State Master Details";
                    frmStateMaster frmStateMaster = new frmStateMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmStateMaster.MdiParent = this;
                    frmStateMaster.Dock = DockStyle.Fill;
                    frmStateMaster.Show();
                    frmStateMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "State Master Details";
                    frmStateMaster frmStateMaster = new frmStateMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmStateMaster.MdiParent = this;
                    frmStateMaster.Dock = DockStyle.Fill;
                    frmStateMaster.Show();
                    frmStateMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "State Master Details";
                    frmStateMaster frmStateMaster = new frmStateMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmStateMaster.MdiParent = this;
                    frmStateMaster.Dock = DockStyle.Fill;
                    frmStateMaster.Show();
                    frmStateMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbRelationshipList01_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Relationship Master Details";
                    frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmRelationshipMaster.MdiParent = this;
                    frmRelationshipMaster.Dock = DockStyle.Fill;
                    frmRelationshipMaster.Show();
                    frmRelationshipMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Relationship Master Details";
                    frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmRelationshipMaster.MdiParent = this;
                    frmRelationshipMaster.Dock = DockStyle.Fill;
                    frmRelationshipMaster.Show();
                    frmRelationshipMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Relationship Master Details";
                    frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmRelationshipMaster.MdiParent = this;
                    frmRelationshipMaster.Dock = DockStyle.Fill;
                    frmRelationshipMaster.Show();
                    frmRelationshipMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbPayrollSystem_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 3;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Payroll Master Details";
                    frmPayrollMaster payrollMaster = new frmPayrollMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    payrollMaster.MdiParent = this;
                    payrollMaster.Dock = DockStyle.Fill;
                    payrollMaster.Show();
                    payrollMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Payroll Master Details";
                    frmPayrollMaster payrollMaster = new frmPayrollMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    payrollMaster.MdiParent = this;
                    payrollMaster.Dock = DockStyle.Fill;
                    payrollMaster.Show();
                    payrollMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Payroll Master Details";
                    frmPayrollMaster payrollMaster = new frmPayrollMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    payrollMaster.MdiParent = this;
                    payrollMaster.Dock = DockStyle.Fill;
                    payrollMaster.Show();
                    payrollMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbEarningsList_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Allowence Master Details";
                frmPayrollAllowences frmPayrollAllowences = new frmPayrollAllowences(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmPayrollAllowences.MdiParent = this;
                frmPayrollAllowences.Dock = DockStyle.Fill;
                frmPayrollAllowences.Show();
                frmPayrollAllowences.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbDeductionsList_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Deduction Master Details";
                frmPayrollDeductions frmPayrollDeductions = new frmPayrollDeductions(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmPayrollDeductions.MdiParent = this;
                frmPayrollDeductions.Dock = DockStyle.Fill;
                frmPayrollDeductions.Show();
                frmPayrollDeductions.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbReimbursmentList_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Reimbursement Master Details";
                frmReimbursement frmReimbursement = new frmReimbursement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmReimbursement.MdiParent = this;
                frmReimbursement.Dock = DockStyle.Fill;
                frmReimbursement.Show();
                frmReimbursement.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbSalaryProfile_Click(object sender, EventArgs e)
        {
            //frmSalaryProfile frmSalaryProfile = new frmSalaryProfile();
            //frmSalaryProfile.Show();
        }

        private void cmbLeaveStatement_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Leave Statement Details";
                frmLeaveStatement frmLeaveStatement = new frmLeaveStatement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmLeaveStatement.MdiParent = this;
                frmLeaveStatement.Dock = DockStyle.Fill;
                frmLeaveStatement.Show();
                frmLeaveStatement.WindowState = FormWindowState.Maximized;
            }
        }

        private void tlbCompanyInfo_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Company Master Details";
                frmCompanyInfo frmCompanyInfo = new frmCompanyInfo(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmCompanyInfo.MdiParent = this;
                frmCompanyInfo.Dock = DockStyle.Fill;
                frmCompanyInfo.Show();
                frmCompanyInfo.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbUpdateAddressInfo_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Current User Master Details";
                frmUpdateCurrentUserInfo frmUpdateCurrentUserInfo = new frmUpdateCurrentUserInfo(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmUpdateCurrentUserInfo.MdiParent = this;
                frmUpdateCurrentUserInfo.Dock = DockStyle.Fill;
                frmUpdateCurrentUserInfo.Show();
                frmUpdateCurrentUserInfo.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbApplyLeave_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Current User Leave Master Details";
                frmCurrentUserLeaveMaster frmCurrentUserLeaveMaster = new frmCurrentUserLeaveMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmCurrentUserLeaveMaster.MdiParent = this;
                frmCurrentUserLeaveMaster.Dock = DockStyle.Fill;
                frmCurrentUserLeaveMaster.Show();
                frmCurrentUserLeaveMaster.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbIndividualLeaveStatement_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Leave Statement Details";
                frmLeaveStatement frmLeaveStatement = new frmLeaveStatement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmLeaveStatement.MdiParent = this;
                frmLeaveStatement.Dock = DockStyle.Fill;
                frmLeaveStatement.Show();
                frmLeaveStatement.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbLeaveEntitlement_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Leave Entitlement Details";
                frmEmpLeaveEntitlement frmEmpLeaveEntitlement = new frmEmpLeaveEntitlement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmEmpLeaveEntitlement.MdiParent = this;
                frmEmpLeaveEntitlement.Dock = DockStyle.Fill;
                frmEmpLeaveEntitlement.Show();
                frmEmpLeaveEntitlement.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbConfigureSalaryProfile_Click(object sender, EventArgs e)
        {
            //frmUpdateSalaryProfile frmUpdateSalaryProfile = new frmUpdateSalaryProfile();
            //frmUpdateSalaryProfile.Show();
        }

        private void cmbSalaryProfile_Click_1(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Salary Profile Details";
                    frmSalaryProfile frmSalaryProfile = new frmSalaryProfile(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmSalaryProfile.MdiParent = this;
                    frmSalaryProfile.Dock = DockStyle.Fill;
                    frmSalaryProfile.Show();
                    frmSalaryProfile.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Salary Profile Details";
                    frmSalaryProfile frmSalaryProfile = new frmSalaryProfile(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmSalaryProfile.MdiParent = this;
                    frmSalaryProfile.Dock = DockStyle.Fill;
                    frmSalaryProfile.Show();
                    frmSalaryProfile.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Salary Profile Details";
                    frmSalaryProfile frmSalaryProfile = new frmSalaryProfile(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmSalaryProfile.MdiParent = this;
                    frmSalaryProfile.Dock = DockStyle.Fill;
                    frmSalaryProfile.Show();
                    frmSalaryProfile.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbConfigureSalaryProfile_Click_1(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Salary Profile Details";
                    frmUpdateSalaryProfile frmUpdateSalaryProfile = new frmUpdateSalaryProfile(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmUpdateSalaryProfile.MdiParent = this;
                    frmUpdateSalaryProfile.Dock = DockStyle.Fill;
                    frmUpdateSalaryProfile.Show();
                    frmUpdateSalaryProfile.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Salary Profile Details";
                    frmUpdateSalaryProfile frmUpdateSalaryProfile = new frmUpdateSalaryProfile(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmUpdateSalaryProfile.MdiParent = this;
                    frmUpdateSalaryProfile.Dock = DockStyle.Fill;
                    frmUpdateSalaryProfile.Show();
                    frmUpdateSalaryProfile.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Salary Profile Details";
                    frmUpdateSalaryProfile frmUpdateSalaryProfile = new frmUpdateSalaryProfile(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmUpdateSalaryProfile.MdiParent = this;
                    frmUpdateSalaryProfile.Dock = DockStyle.Fill;
                    frmUpdateSalaryProfile.Show();
                    frmUpdateSalaryProfile.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void bulkLeaveApprovalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Bulk Leave Approval Details";
                frmEmpBulkLeaveApproval frmEmpBulkLeaveApproval = new frmEmpBulkLeaveApproval(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmEmpBulkLeaveApproval.MdiParent = this;
                frmEmpBulkLeaveApproval.Dock = DockStyle.Fill;
                frmEmpBulkLeaveApproval.Show();
                frmEmpBulkLeaveApproval.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbLeaveTypeMaster_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Leave Type Master Details";
                    frmLeaveTypeMaster frmLeaveTypeMaster = new frmLeaveTypeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmLeaveTypeMaster.MdiParent = this;
                    frmLeaveTypeMaster.Dock = DockStyle.Fill;
                    frmLeaveTypeMaster.Show();
                    frmLeaveTypeMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Leave Type Master Details";
                    frmLeaveTypeMaster frmLeaveTypeMaster = new frmLeaveTypeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmLeaveTypeMaster.MdiParent = this;
                    frmLeaveTypeMaster.Dock = DockStyle.Fill;
                    frmLeaveTypeMaster.Show();
                    frmLeaveTypeMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Leave Type Master Details";
                    frmLeaveTypeMaster frmLeaveTypeMaster = new frmLeaveTypeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmLeaveTypeMaster.MdiParent = this;
                    frmLeaveTypeMaster.Dock = DockStyle.Fill;
                    frmLeaveTypeMaster.Show();
                    frmLeaveTypeMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmManageEmployee_Click(object sender, EventArgs e)
        {

        }

        private void cmbManageEmployee_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));
            
            AppModuleID = 2;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Employee Master Details";
                    frmEmployeeMaster frmEmployeeMasterDetails = new frmEmployeeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmEmployeeMasterDetails.MdiParent = this;
                    frmEmployeeMasterDetails.Dock = DockStyle.Fill;
                    frmEmployeeMasterDetails.Show();
                    frmEmployeeMasterDetails.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Employee Master Details";
                    frmEmployeeMaster frmEmployeeMasterDetails = new frmEmployeeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmEmployeeMasterDetails.MdiParent = this;
                    frmEmployeeMasterDetails.Dock = DockStyle.Fill;
                    frmEmployeeMasterDetails.Show();
                    frmEmployeeMasterDetails.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Employee Master Details";
                    frmEmployeeMaster frmEmployeeMasterDetails = new frmEmployeeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmEmployeeMasterDetails.MdiParent = this;
                    frmEmployeeMasterDetails.Dock = DockStyle.Fill;
                    frmEmployeeMasterDetails.Show();
                    frmEmployeeMasterDetails.WindowState = FormWindowState.Maximized;
                }
            }

        }

        private void cmbLeaveEntitlement1_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Employee Leave Entitlement Details";
                    frmEmpLeaveEntitlement frmEmpLeaveEntitlement = new frmEmpLeaveEntitlement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmEmpLeaveEntitlement.MdiParent = this;
                    frmEmpLeaveEntitlement.Dock = DockStyle.Fill;
                    frmEmpLeaveEntitlement.Show();
                    frmEmpLeaveEntitlement.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Employee Leave Entitlement Details";
                    frmEmpLeaveEntitlement frmEmpLeaveEntitlement = new frmEmpLeaveEntitlement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmEmpLeaveEntitlement.MdiParent = this;
                    frmEmpLeaveEntitlement.Dock = DockStyle.Fill;
                    frmEmpLeaveEntitlement.Show();
                    frmEmpLeaveEntitlement.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Employee Leave Entitlement Details";
                    frmEmpLeaveEntitlement frmEmpLeaveEntitlement = new frmEmpLeaveEntitlement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmEmpLeaveEntitlement.MdiParent = this;
                    frmEmpLeaveEntitlement.Dock = DockStyle.Fill;
                    frmEmpLeaveEntitlement.Show();
                    frmEmpLeaveEntitlement.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbMyLeaveStatementReport01_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Leave Statement Details";
                frmLeaveStatements frmLeaveStatements = new frmLeaveStatements(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmLeaveStatements.MdiParent = this;
                frmLeaveStatements.Dock = DockStyle.Fill;
                frmLeaveStatements.Show();
                frmLeaveStatements.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbWeeklyOffList_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Weekly Profile Master Details";
                frmWeeklyProfileMas frmWeeklyProfileMas = new frmWeeklyProfileMas(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmWeeklyProfileMas.MdiParent = this;
                frmWeeklyProfileMas.Dock = DockStyle.Fill;
                frmWeeklyProfileMas.Show();
                frmWeeklyProfileMas.WindowState = FormWindowState.Maximized;
            }
            //frmWeeklyProfileMaster frmWeeklyProfileMaster = new frmWeeklyProfileMaster();
            //frmWeeklyProfileMaster.Show();
        }

        private void cmbWeeklyOffConfiguration_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Weekly Profile Details Information";
                frmWeeklyProfileDetailsInfo frmWeeklyProfileDetailsInfo = new frmWeeklyProfileDetailsInfo(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmWeeklyProfileDetailsInfo.MdiParent = this;
                frmWeeklyProfileDetailsInfo.Dock = DockStyle.Fill;
                frmWeeklyProfileDetailsInfo.Show();
                frmWeeklyProfileDetailsInfo.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbBulkLeaveApproval_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Bulk Leave Approval Details";
                frmBulkLeaveApproval frmBulkLeaveApproval = new frmBulkLeaveApproval(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmBulkLeaveApproval.MdiParent = this;
                frmBulkLeaveApproval.Dock = DockStyle.Fill;
                frmBulkLeaveApproval.Show();
                frmBulkLeaveApproval.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbEarningsListConfig_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 4;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Payroll Allowance Master Details";
                    frmPayrollAllowences frmPayrollAllowences = new frmPayrollAllowences(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmPayrollAllowences.MdiParent = this;
                    frmPayrollAllowences.Dock = DockStyle.Fill;
                    frmPayrollAllowences.Show();
                    frmPayrollAllowences.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Payroll Allowance Master Details";
                    frmPayrollAllowences frmPayrollAllowences = new frmPayrollAllowences(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmPayrollAllowences.MdiParent = this;
                    frmPayrollAllowences.Dock = DockStyle.Fill;
                    frmPayrollAllowences.Show();
                    frmPayrollAllowences.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Payroll Allowance Master Details";
                    frmPayrollAllowences frmPayrollAllowences = new frmPayrollAllowences(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmPayrollAllowences.MdiParent = this;
                    frmPayrollAllowences.Dock = DockStyle.Fill;
                    frmPayrollAllowences.Show();
                    frmPayrollAllowences.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbReimbursmentListConfig_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 4;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Reimbursement Master Details";
                    frmReimbursement frmReimbursement = new frmReimbursement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmReimbursement.MdiParent = this;
                    frmReimbursement.Dock = DockStyle.Fill;
                    frmReimbursement.Show();
                    frmReimbursement.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Reimbursement Master Details";
                    frmReimbursement frmReimbursement = new frmReimbursement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmReimbursement.MdiParent = this;
                    frmReimbursement.Dock = DockStyle.Fill;
                    frmReimbursement.Show();
                    frmReimbursement.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Reimbursement Master Details";
                    frmReimbursement frmReimbursement = new frmReimbursement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmReimbursement.MdiParent = this;
                    frmReimbursement.Dock = DockStyle.Fill;
                    frmReimbursement.Show();
                    frmReimbursement.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbDeductionsListConfig_Click(object sender, EventArgs e)
        {
            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 4;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Deduction Master Details";
                    frmPayrollDeductions frmPayrollDeductions = new frmPayrollDeductions(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmPayrollDeductions.MdiParent = this;
                    frmPayrollDeductions.Dock = DockStyle.Fill;
                    frmPayrollDeductions.Show();
                    frmPayrollDeductions.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Deduction Master Details";
                    frmPayrollDeductions frmPayrollDeductions = new frmPayrollDeductions(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmPayrollDeductions.MdiParent = this;
                    frmPayrollDeductions.Dock = DockStyle.Fill;
                    frmPayrollDeductions.Show();
                    frmPayrollDeductions.WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                //objCurrentUserInfo.UserModuleAccessInfo(clsCurrentUser.EmpID, AppModuleID);
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Deduction Master Details";
                    frmPayrollDeductions frmPayrollDeductions = new frmPayrollDeductions(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    frmPayrollDeductions.MdiParent = this;
                    frmPayrollDeductions.Dock = DockStyle.Fill;
                    frmPayrollDeductions.Show();
                    frmPayrollDeductions.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbPendingLeaveApprovalList_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Pending Approval Details";
                frmPendingApprovalList frmPendingApprovalList = new frmPendingApprovalList(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmPendingApprovalList.MdiParent = this;
                frmPendingApprovalList.Dock = DockStyle.Fill;
                frmPendingApprovalList.Show();
                frmPendingApprovalList.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbRejectionLeaveList_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Leave Rejection Details";
                frmLeaveRejectionList frmLeaveRejectionList = new frmLeaveRejectionList();
                frmLeaveRejectionList.MdiParent = this;
                frmLeaveRejectionList.Dock = DockStyle.Fill;
                frmLeaveRejectionList.Show();
                frmLeaveRejectionList.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbConsolidatedLeaveStatement_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Consolidated Leave Statement";
                frmConsolidatedLeaveStatement frmConsolidatedLeaveStatement = new frmConsolidatedLeaveStatement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmConsolidatedLeaveStatement.MdiParent = this;
                frmConsolidatedLeaveStatement.Dock = DockStyle.Fill;
                frmConsolidatedLeaveStatement.Show();
                frmConsolidatedLeaveStatement.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbOutstandingLeaveStatement_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Outstanding Leave Statement";
                frmOutstandingLeaveStatement frmOutstandingLeaveStatement = new frmOutstandingLeaveStatement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmOutstandingLeaveStatement.MdiParent = this;
                frmOutstandingLeaveStatement.Dock = DockStyle.Fill;
                frmOutstandingLeaveStatement.Show();
                frmOutstandingLeaveStatement.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbDailyAttendanceSheet_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Daily Attendance Sheet";
                frmDailyAttendanceSheet frmDailyAttendanceSheet = new frmDailyAttendanceSheet(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                frmDailyAttendanceSheet.MdiParent = this;
                frmDailyAttendanceSheet.Dock = DockStyle.Fill;
                frmDailyAttendanceSheet.Show();
                frmDailyAttendanceSheet.WindowState = FormWindowState.Maximized;
            }
        }

        private void frmDashboard_ResizeEnd(object sender, EventArgs e)
        {
            //lblDashboardTitle.Width = this.Width;
        }

        private void frmDashboard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (MessageBox.Show("Are you sure you want to exit.?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                Application.Exit();
            }
        }
    }
}
