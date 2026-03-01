using DALStaffSync;
using DocumentFormat.OpenXml.Bibliography;
using Humanizer;
using LiveCharts;
using LiveCharts.Definitions.Charts;
using LiveCharts.WinForms;
using LiveCharts.Wpf;
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
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;

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
        DALStaffSync.clsFinYearMas objFinYearInfo = new DALStaffSync.clsFinYearMas();
        DALStaffSync.clsDashboardWidgetData objDashboardWidgetData = new DALStaffSync.clsDashboardWidgetData();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        DALStaffSync.clsAppSettings objAppSettings = new DALStaffSync.clsAppSettings();
        List<ClientInfo> objActiveClientInfo = new List<ClientInfo>();
        List<FinYearMas> objActiveFinYear = new List<FinYearMas>();
        ClientFinYearInfo objSelectedClientFinYearInfo = new ClientFinYearInfo();
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

            //objActiveClientInfo = objClientInfo.getClientInfo(1);
            objActiveClientInfo = objClientInfo.getClientInfoByEmpID(EmpID);

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(EmpID);
        }

        private void employeeDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void manageEmployeeDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));

            AppModuleID = 2;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Employee Details";
                    sptrDashboardContainer.Visible = false;
                    frmEmployeeMaster frmEmployeeMasterDetails = new frmEmployeeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmEmployeeMasterDetails.MdiParent = this;
                    frmEmployeeMasterDetails.Dock = DockStyle.Fill;
                    frmEmployeeMasterDetails.Show();
                    frmEmployeeMasterDetails.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Employee Details";
                        sptrDashboardContainer.Visible = false;
                        frmEmployeeMaster frmEmployeeMasterDetails = new frmEmployeeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmEmployeeMasterDetails.MdiParent = this;
                        frmEmployeeMasterDetails.Dock = DockStyle.Fill;
                        frmEmployeeMasterDetails.Show();
                        frmEmployeeMasterDetails.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Employee Details";
                    sptrDashboardContainer.Visible = false;
                    frmEmployeeMaster frmEmployeeMasterDetails = new frmEmployeeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmEmployeeMasterDetails.MdiParent = this;
                    frmEmployeeMasterDetails.Dock = DockStyle.Fill;
                    frmEmployeeMasterDetails.Show();
                    frmEmployeeMasterDetails.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void manageEmployeeAttendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));

            AppModuleID = 4;
            
            sptrDashboardContainer.Visible = false;
            frmDailyAttendanceProcess frmDailyAttendanceProcessDetails = new frmDailyAttendanceProcess(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
            frmDailyAttendanceProcessDetails.MdiParent = this;
            frmDailyAttendanceProcessDetails.Dock = DockStyle.Fill;
            frmDailyAttendanceProcessDetails.Show();
            frmDailyAttendanceProcessDetails.WindowState = FormWindowState.Maximized;
            return;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Attendance Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmAttendanceMater frmAttendanceMaterDetails = new frmAttendanceMater(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmAttendanceMaterDetails.MdiParent = this;
                    frmAttendanceMaterDetails.Dock = DockStyle.Fill;
                    frmAttendanceMaterDetails.Show();
                    frmAttendanceMaterDetails.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Attendance Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmAttendanceMater frmAttendanceMaterDetails = new frmAttendanceMater(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmAttendanceMaterDetails.MdiParent = this;
                        frmAttendanceMaterDetails.Dock = DockStyle.Fill;
                        frmAttendanceMaterDetails.Show();
                        frmAttendanceMaterDetails.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Attendance Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmAttendanceMater frmAttendanceMaterDetails = new frmAttendanceMater(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmAttendanceMaterDetails.MdiParent = this;
                    frmAttendanceMaterDetails.Dock = DockStyle.Fill;
                    frmAttendanceMaterDetails.Show();
                    frmAttendanceMaterDetails.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void manageEmployeePayrollToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));

            AppModuleID = 3;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {

            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Employee Details";
                        sptrDashboardContainer.Visible = false;
                        frmEmployeeMaster frmEmployeeMasterDetails = new frmEmployeeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmEmployeeMasterDetails.MdiParent = this;
                        frmEmployeeMasterDetails.Dock = DockStyle.Fill;
                        frmEmployeeMasterDetails.Show();
                        frmEmployeeMasterDetails.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    lblDashboardTitle.Text = "Dashboard";
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //frmPayrollMaster frmPayrollMasterDetails = new frmPayrollMaster();
                //frmPayrollMasterDetails.Show();
            }
        }

        private void manageEmployeeLeavesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 5;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Leave Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmLeavesMaster frmLeavesMasterDetails = new frmLeavesMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmLeavesMasterDetails.MdiParent = this;
                    frmLeavesMasterDetails.Dock = DockStyle.Fill;
                    frmLeavesMasterDetails.Show();
                    frmLeavesMasterDetails.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Leave Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmLeavesMaster frmLeavesMasterDetails = new frmLeavesMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmLeavesMasterDetails.MdiParent = this;
                        frmLeavesMasterDetails.Dock = DockStyle.Fill;
                        frmLeavesMasterDetails.Show();
                        frmLeavesMasterDetails.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Leave Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmLeavesMaster frmLeavesMasterDetails = new frmLeavesMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmLeavesMasterDetails.MdiParent = this;
                    frmLeavesMasterDetails.Dock = DockStyle.Fill;
                    frmLeavesMasterDetails.Show();
                    frmLeavesMasterDetails.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void employeeWiseReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Employee Wise Reports";
                sptrDashboardContainer.Visible = false;
                frmEmployeeWiseReports frmEmployeeWiseReportsDetails = new frmEmployeeWiseReports(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmEmployeeWiseReportsDetails.MdiParent = this;
                frmEmployeeWiseReportsDetails.Dock = DockStyle.Fill;
                frmEmployeeWiseReportsDetails.Show();
                frmEmployeeWiseReportsDetails.WindowState = FormWindowState.Maximized;
            }
        }

        private void employeeAttendanceReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Employee Attendance Reports";
                sptrDashboardContainer.Visible = false;
                frmEmployeeAttendanceReports frmEmployeeAttendanceReportsDetails = new frmEmployeeAttendanceReports(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmEmployeeAttendanceReportsDetails.MdiParent = this;
                frmEmployeeAttendanceReportsDetails.Dock = DockStyle.Fill;
                frmEmployeeAttendanceReportsDetails.Show();
                frmEmployeeAttendanceReportsDetails.WindowState = FormWindowState.Maximized;

            }
        }

        private void employeePayrollReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Employee Payroll Reports";
                sptrDashboardContainer.Visible = false;
                frmEmployeePayrollReports frmEmployeePayrollReportsDetails = new frmEmployeePayrollReports(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmEmployeePayrollReportsDetails.MdiParent = this;
                frmEmployeePayrollReportsDetails.Dock = DockStyle.Fill;
                frmEmployeePayrollReportsDetails.Show();
                frmEmployeePayrollReportsDetails.WindowState = FormWindowState.Maximized;
            }
        }

        private void employeeLeavesReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Employee Leave Reports";
                sptrDashboardContainer.Visible = false;
                frmEmployeeLeavesReports frmEmployeeLeavesReportsDetails = new frmEmployeeLeavesReports(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmEmployeeLeavesReportsDetails.MdiParent = this;
                frmEmployeeLeavesReportsDetails.Dock = DockStyle.Fill;
                frmEmployeeLeavesReportsDetails.Show();
                frmEmployeeLeavesReportsDetails.WindowState = FormWindowState.Maximized;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "StaffSync - About";
                sptrDashboardContainer.Visible = false;
                frmAbout frmAboutDetails = new frmAbout();
                frmAboutDetails.MdiParent = this;
                frmAboutDetails.Dock = DockStyle.Fill;
                frmAboutDetails.Show();
                frmAboutDetails.WindowState = FormWindowState.Maximized;
            }
        }

        private void departmentListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Department Master Details";
                sptrDashboardContainer.Visible = false;
                frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmDepartmentMaster.MdiParent = this;
                frmDepartmentMaster.Dock = DockStyle.Fill;
                frmDepartmentMaster.Show();
                frmDepartmentMaster.WindowState = FormWindowState.Maximized;
            }
        }

        private void countriesListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Country Master Details";
                sptrDashboardContainer.Visible = false;
                frmCountryMaster frmCountryMaster = new frmCountryMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmCountryMaster.MdiParent = this;
                frmCountryMaster.Dock = DockStyle.Fill;
                frmCountryMaster.Show();
                frmCountryMaster.WindowState = FormWindowState.Maximized;
            }
        }

        private void designationListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Designation Master Details";
                sptrDashboardContainer.Visible = false;
                frmDesignationMaster frmDesignationMaster = new frmDesignationMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmDesignationMaster.MdiParent = this;
                frmDesignationMaster.Dock = DockStyle.Fill;
                frmDesignationMaster.Show();
                frmDesignationMaster.WindowState = FormWindowState.Maximized;
            }
        }

        private void countriesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "State Master Details";
                sptrDashboardContainer.Visible = false;
                frmStateMaster frmStateMaster = new frmStateMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmStateMaster.MdiParent = this;
                frmStateMaster.Dock = DockStyle.Fill;
                frmStateMaster.Show();
                frmStateMaster.WindowState = FormWindowState.Maximized;
            }
        }

        private void relationshipListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Relationship Master Details";
                sptrDashboardContainer.Visible = false;
                frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmRelationshipMaster.MdiParent = this;
                frmRelationshipMaster.Dock = DockStyle.Fill;
                frmRelationshipMaster.Show();
                frmRelationshipMaster.WindowState = FormWindowState.Maximized;
            }
        }

        private void companyDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Last Company Master Details";
                sptrDashboardContainer.Visible = false;
                frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmLastCompanyMaster.MdiParent = this;
                frmLastCompanyMaster.Dock = DockStyle.Fill;
                frmLastCompanyMaster.Show();
                frmLastCompanyMaster.WindowState = FormWindowState.Maximized;
            }
        }

        private void educationListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Qualification Master Details";
                sptrDashboardContainer.Visible = false;
                frmEduQualMaster frmEduQualMaster = new frmEduQualMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmEduQualMaster.MdiParent = this;
                frmEduQualMaster.Dock = DockStyle.Fill;
                frmEduQualMaster.Show();
                frmEduQualMaster.WindowState = FormWindowState.Maximized;
            }
        }

        private void departmentListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));

            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Department Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmDepartmentMaster.MdiParent = this;
                    frmDepartmentMaster.Dock = DockStyle.Fill;
                    frmDepartmentMaster.Show();
                    frmDepartmentMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Department Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmDepartmentMaster.MdiParent = this;
                        frmDepartmentMaster.Dock = DockStyle.Fill;
                        frmDepartmentMaster.Show();
                        frmDepartmentMaster.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    lblDashboardTitle.Text = "Dashboard";
                    MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Department Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
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
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Designation Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmDesignationMaster frmDesignationMaster = new frmDesignationMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmDesignationMaster.MdiParent = this;
                    frmDesignationMaster.Dock = DockStyle.Fill;
                    frmDesignationMaster.Show();
                    frmDesignationMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Designation Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmDesignationMaster frmDesignationMaster = new frmDesignationMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmDesignationMaster.MdiParent = this;
                        frmDesignationMaster.Dock = DockStyle.Fill;
                        frmDesignationMaster.Show();
                        frmDesignationMaster.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Designation Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmDesignationMaster frmDesignationMaster = new frmDesignationMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmDesignationMaster.MdiParent = this;
                    frmDesignationMaster.Dock = DockStyle.Fill;
                    frmDesignationMaster.Show();
                    frmDesignationMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void educationListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Qualification Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmEduQualMaster frmEduQualMaster = new frmEduQualMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmEduQualMaster.MdiParent = this;
                    frmEduQualMaster.Dock = DockStyle.Fill;
                    frmEduQualMaster.Show();
                    frmEduQualMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Qualification Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmEduQualMaster frmEduQualMaster = new frmEduQualMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmEduQualMaster.MdiParent = this;
                        frmEduQualMaster.Dock = DockStyle.Fill;
                        frmEduQualMaster.Show();
                        frmEduQualMaster.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Qualification Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmEduQualMaster frmEduQualMaster = new frmEduQualMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmEduQualMaster.MdiParent = this;
                    frmEduQualMaster.Dock = DockStyle.Fill;
                    frmEduQualMaster.Show();
                    frmEduQualMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void relationshipListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Relationship Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmRelationshipMaster.MdiParent = this;
                    frmRelationshipMaster.Dock = DockStyle.Fill;
                    frmRelationshipMaster.Show();
                    frmRelationshipMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Relationship Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmRelationshipMaster.MdiParent = this;
                        frmRelationshipMaster.Dock = DockStyle.Fill;
                        frmRelationshipMaster.Show();
                        frmRelationshipMaster.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Relationship Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmRelationshipMaster.MdiParent = this;
                    frmRelationshipMaster.Dock = DockStyle.Fill;
                    frmRelationshipMaster.Show();
                    frmRelationshipMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void statesListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "State Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmStateMaster frmStateMaster = new frmStateMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmStateMaster.MdiParent = this;
                    frmStateMaster.Dock = DockStyle.Fill;
                    frmStateMaster.Show();
                    frmStateMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "State Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmStateMaster frmStateMaster = new frmStateMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmStateMaster.MdiParent = this;
                        frmStateMaster.Dock = DockStyle.Fill;
                        frmStateMaster.Show();
                        frmStateMaster.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "State Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmStateMaster frmStateMaster = new frmStateMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmStateMaster.MdiParent = this;
                    frmStateMaster.Dock = DockStyle.Fill;
                    frmStateMaster.Show();
                    frmStateMaster.WindowState = FormWindowState.Maximized;

                }
            }
        }

        private void countriesListToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Country Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmCountryMaster frmCountryMaster = new frmCountryMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmCountryMaster.MdiParent = this;
                    frmCountryMaster.Dock = DockStyle.Fill;
                    frmCountryMaster.Show();
                    frmCountryMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Country Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmCountryMaster frmCountryMaster = new frmCountryMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmCountryMaster.MdiParent = this;
                        frmCountryMaster.Dock = DockStyle.Fill;
                        frmCountryMaster.Show();
                        frmCountryMaster.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Country Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmCountryMaster frmCountryMaster = new frmCountryMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmCountryMaster.MdiParent = this;
                    frmCountryMaster.Dock = DockStyle.Fill;
                    frmCountryMaster.Show();
                    frmCountryMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void companyListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Last Company Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmLastCompanyMaster.MdiParent = this;
                    frmLastCompanyMaster.Dock = DockStyle.Fill;
                    frmLastCompanyMaster.Show();
                    frmLastCompanyMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Last Company Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmLastCompanyMaster.MdiParent = this;
                        frmLastCompanyMaster.Dock = DockStyle.Fill;
                        frmLastCompanyMaster.Show();
                        frmLastCompanyMaster.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Last Company Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmLastCompanyMaster.MdiParent = this;
                    frmLastCompanyMaster.Dock = DockStyle.Fill;
                    frmLastCompanyMaster.Show();
                    frmLastCompanyMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void skillsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    sptrDashboardContainer.Visible = false;
                    frmSkillsMaster frmSkillsMaster = new frmSkillsMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmSkillsMaster.MdiParent = this;
                    frmSkillsMaster.Dock = DockStyle.Fill;
                    frmSkillsMaster.Show();
                    frmSkillsMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        sptrDashboardContainer.Visible = false;
                        frmSkillsMaster frmSkillsMaster = new frmSkillsMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmSkillsMaster.MdiParent = this;
                        frmSkillsMaster.Dock = DockStyle.Fill;
                        frmSkillsMaster.Show();
                        frmSkillsMaster.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                sptrDashboardContainer.Visible = false;
                frmSkillsMaster frmSkillsMaster = new frmSkillsMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmSkillsMaster.MdiParent = this;
                frmSkillsMaster.Dock = DockStyle.Fill;
                frmSkillsMaster.Show();
                frmSkillsMaster.WindowState = FormWindowState.Maximized;
            }
        }

        private void LoadDepartmentColumnChart(int txtClientID, int txtFinYearID)
        {
            DataTable dt = objDashboardWidgetData.GetDepartmentExposure(txtClientID, txtFinYearID);

            if (dt == null || dt.Rows.Count == 0)
                return;

            ChartValues<double> values = new ChartValues<double>();
            List<string> labels = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                double amount = row["TotalOutstanding"] != DBNull.Value ? Convert.ToDouble(row["TotalOutstanding"]) : 0;

                values.Add(amount);
                labels.Add(row["DepartmentTitle"].ToString());
            }

            chrtCompanySummaryMatrix.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Outstanding",
                    Values = values,
                    DataLabels = true,
                    LabelPoint = point => "₹ " + point.Y.ToString("N0")
                }
            };

            // X Axis → Categories
            chrtCompanySummaryMatrix.AxisX.Clear();
            chrtCompanySummaryMatrix.AxisX.Add(new Axis
            {
                Title = "Department",
                Labels = labels,
                LabelsRotation = 45
            });

            // Y Axis → Amount
            chrtCompanySummaryMatrix.AxisY.Clear();
            chrtCompanySummaryMatrix.AxisY.Add(new Axis
            {
                Title = "Outstanding Amount",
                LabelFormatter = value => "₹ " + value.ToString("N0")
            });

            chrtCompanySummaryMatrix.LegendLocation = LegendLocation.Right;
            chrtCompanySummaryMatrix.DisableAnimations = true;
            chrtCompanySummaryMatrix.AxisX[0].Separator.StrokeThickness = 0;
            chrtCompanySummaryMatrix.AxisY[0].Separator.StrokeThickness = 0;

            //foreach (DataRow row in dt.Rows)
            //{
            //    double amount = row["TotalOutstanding"] != DBNull.Value ? Convert.ToDouble(row["TotalOutstanding"]): 0;

            //    values.Add(amount);
            //    labels.Add(row["DepartmentTitle"].ToString());
            //}

            //chrtCompanySummaryMatrix.Series = new SeriesCollection
            //{
            //    new RowSeries
            //    {
            //        Title = "Outstanding",
            //        Values = values,
            //        DataLabels = true,
            //        LabelPoint = point => "₹ " + point.X.ToString("N0")
            //    }
            //};

            //chrtCompanySummaryMatrix.AxisY.Clear();
            //chrtCompanySummaryMatrix.AxisY.Add(new Axis
            //{
            //    Labels = labels
            //});

            //chrtCompanySummaryMatrix.AxisX.Clear();
            //chrtCompanySummaryMatrix.AxisX.Add(new Axis
            //{
            //    LabelFormatter = value => "₹ " + value.ToString("N0")
            //});

            //chrtCompanySummaryMatrix.DisableAnimations = true;
            //if (dt == null || dt.Rows.Count == 0)
            //{
            //    chrtCompanySummaryMatrix.Series = null;
            //    return;
            //}

            //SeriesCollection pieSeries = new SeriesCollection();

            //foreach (DataRow row in dt.Rows)
            //{
            //    double value = Convert.ToDouble(row["TotalOutstanding"]);

            //    if (value > 0) // avoid zero slices
            //    {
            //        pieSeries.Add(new PieSeries
            //        {
            //            Title = row["DepartmentTitle"].ToString(),
            //            Values = new ChartValues<double> { value },
            //            DataLabels = true,
            //            LabelPoint = chartPoint =>
            //                string.Format("{0} ({1:P})",
            //                    chartPoint.Y.ToString("N0"),
            //                    chartPoint.Participation)
            //        });
            //    }
            //}

            //chrtCompanySummaryMatrix.Series = pieSeries;
            //chrtCompanySummaryMatrix.LegendLocation = LegendLocation.Right;
            //chrtCompanySummaryMatrix.DisableAnimations = true;
        }

        private void GetAdvanceSummary(int txtClientID, int txtFinYearID)
        {
            DataTable dt = objDashboardWidgetData.GetAdvanceSummary(txtClientID, txtFinYearID);

            if (dt.Rows.Count == 0)
                return;

            DataRow row = dt.Rows[0];

            double totalAdvances = Convert.ToDouble(row["TotalAdvances"]);
            double totalSanctioned = Convert.ToDouble(row["TotalSanctioned"]);
            double totalRecovered = Convert.ToDouble(row["TotalRecovered"]);
            double totalOutstanding = Convert.ToDouble(row["TotalOutstanding"]);

            var values = new ChartValues<double>
            {
                totalSanctioned,
                totalRecovered,
                totalOutstanding
            };

            chrtCompanyAdvanceSummaryMatrix.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Sanctioned",
                    Values = new ChartValues<double> { totalSanctioned },
                    DataLabels = true,
                    Fill = System.Windows.Media.Brushes.SteelBlue
                },
                new ColumnSeries
                {
                    Title = "Recovered",
                    Values = new ChartValues<double> { totalRecovered },
                    DataLabels = true,
                    Fill = System.Windows.Media.Brushes.SeaGreen
                },
                new ColumnSeries
                {
                    Title = "Outstanding",
                    Values = new ChartValues<double> { totalOutstanding },
                    DataLabels = true,
                    Fill = System.Windows.Media.Brushes.IndianRed
                }
            };

            chrtCompanyAdvanceSummaryMatrix.AxisX.Clear();
            chrtCompanyAdvanceSummaryMatrix.AxisX.Add(new Axis
            {
                Labels = new[]
                {
                    "Sanctioned",
                    "Recovered",
                    "Outstanding"
                },
                LabelsRotation = 0
            });

            chrtCompanyAdvanceSummaryMatrix.AxisY.Clear();
            chrtCompanyAdvanceSummaryMatrix.AxisY.Add(new Axis
            {
                Title = "Amount",
                LabelFormatter = value => value.ToString("N0")
            });

            chrtCompanyAdvanceSummaryMatrix.LegendLocation = LegendLocation.None;
            chrtCompanyAdvanceSummaryMatrix.DisableAnimations = true;
            chrtCompanyAdvanceSummaryMatrix.AxisX[0].Separator.StrokeThickness = 0;
            chrtCompanyAdvanceSummaryMatrix.AxisY[0].Separator.StrokeThickness = 0;

            //foreach (DataRow row in dt.Rows)
            //{
            //    double amount = row["TotalOutstanding"] != DBNull.Value ? Convert.ToDouble(row["TotalOutstanding"]): 0;

            //    values.Add(amount);
            //    labels.Add(row["DepartmentTitle"].ToString());
            //}

            //chrtCompanySummaryMatrix.Series = new SeriesCollection
            //{
            //    new RowSeries
            //    {
            //        Title = "Outstanding",
            //        Values = values,
            //        DataLabels = true,
            //        LabelPoint = point => "₹ " + point.X.ToString("N0")
            //    }
            //};

            //chrtCompanySummaryMatrix.AxisY.Clear();
            //chrtCompanySummaryMatrix.AxisY.Add(new Axis
            //{
            //    Labels = labels
            //});

            //chrtCompanySummaryMatrix.AxisX.Clear();
            //chrtCompanySummaryMatrix.AxisX.Add(new Axis
            //{
            //    LabelFormatter = value => "₹ " + value.ToString("N0")
            //});

            //chrtCompanySummaryMatrix.DisableAnimations = true;
            //if (dt == null || dt.Rows.Count == 0)
            //{
            //    chrtCompanySummaryMatrix.Series = null;
            //    return;
            //}

            //SeriesCollection pieSeries = new SeriesCollection();

            //foreach (DataRow row in dt.Rows)
            //{
            //    double value = Convert.ToDouble(row["TotalOutstanding"]);

            //    if (value > 0) // avoid zero slices
            //    {
            //        pieSeries.Add(new PieSeries
            //        {
            //            Title = row["DepartmentTitle"].ToString(),
            //            Values = new ChartValues<double> { value },
            //            DataLabels = true,
            //            LabelPoint = chartPoint =>
            //                string.Format("{0} ({1:P})",
            //                    chartPoint.Y.ToString("N0"),
            //                    chartPoint.Participation)
            //        });
            //    }
            //}

            //chrtCompanySummaryMatrix.Series = pieSeries;
            //chrtCompanySummaryMatrix.LegendLocation = LegendLocation.Right;
            //chrtCompanySummaryMatrix.DisableAnimations = true;
        }

        private void GetAdvanceRiskBaseInfo(int txtClientID, int txtFinYearID)
        {
            dtgAdvanceRiskBase.DataSource = null;
            dtgAdvanceRiskBase.DataSource = objDashboardWidgetData.GetAdvanceRiskBaseInfo(txtClientID, txtFinYearID);
            dtgAdvanceRiskBase.Columns["EmpID"].Visible = false;
            dtgAdvanceRiskBase.Columns["EmpCode"].ReadOnly = true;
            dtgAdvanceRiskBase.Columns["EmpName"].ReadOnly = true;
            dtgAdvanceRiskBase.Columns["EmpName"].Width = 200;
            dtgAdvanceRiskBase.Columns["DesignationTitle"].Width = 200;
            dtgAdvanceRiskBase.Columns["DesignationTitle"].ReadOnly = true;
            dtgAdvanceRiskBase.Columns["DepartmentTitle"].ReadOnly = true;
            dtgAdvanceRiskBase.Columns["DepartmentTitle"].Width = 150;
            dtgAdvanceRiskBase.Columns["AdvanceTypeID"].Visible = false;
            dtgAdvanceRiskBase.Columns["AdvanceTypeTitle"].ReadOnly = true;
            dtgAdvanceRiskBase.Columns["AdvanceTypeTitle"].Width = 150;
            dtgAdvanceRiskBase.Columns["EmpAdvanceRequestID"].Visible = false;
            dtgAdvanceRiskBase.Columns["EmpAdvReqCode"].ReadOnly = true;
            dtgAdvanceRiskBase.Columns["EmpAdvReqCode"].Width = 150;
            dtgAdvanceRiskBase.Columns["AdvanceAmount"].Width = 125;
            dtgAdvanceRiskBase.Columns["AdvanceAmount"].ReadOnly = true;
            dtgAdvanceRiskBase.Columns["AdvanceAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgAdvanceRiskBase.Columns["AdvanceAmount"].DefaultCellStyle.Format = "c2";
            dtgAdvanceRiskBase.Columns["AdvanceStartDate"].Width = 100;
            dtgAdvanceRiskBase.Columns["AdvanceStartDate"].ReadOnly = true;
            dtgAdvanceRiskBase.Columns["AdvanceStartDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgAdvanceRiskBase.Columns["AdvanceStartDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgAdvanceRiskBase.Columns["AdvanceEndDate"].Width = 100;
            dtgAdvanceRiskBase.Columns["AdvanceEndDate"].ReadOnly = true;
            dtgAdvanceRiskBase.Columns["AdvanceEndDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgAdvanceRiskBase.Columns["AdvanceEndDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgAdvanceRiskBase.Columns["RemainingBalance"].Width = 125;
            dtgAdvanceRiskBase.Columns["RemainingBalance"].ReadOnly = true;
            dtgAdvanceRiskBase.Columns["RemainingBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgAdvanceRiskBase.Columns["RemainingBalance"].DefaultCellStyle.Format = "c2";
            dtgAdvanceRiskBase.Columns["TotalRecovered"].Width = 125;
            dtgAdvanceRiskBase.Columns["TotalRecovered"].ReadOnly = true;
            dtgAdvanceRiskBase.Columns["TotalRecovered"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgAdvanceRiskBase.Columns["TotalRecovered"].DefaultCellStyle.Format = "c2";
            dtgAdvanceRiskBase.Columns["AdvanceAgeDays"].ReadOnly = true;
            dtgAdvanceRiskBase.Columns["AdvanceAgeDays"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
        }

        //private void GetAgingDistributionInfo(int txtClientID, int txtFinYearID)
        //{
        //    DataTable dt = objDashboardWidgetData.GetAgingDistributionInfo(txtClientID, txtFinYearID);

        //    if (dt.Rows.Count == 0)
        //        return;

        //    chrtAgingDistribution.Series.Clear();
        //    chrtAgingDistribution.AxisX.Clear();
        //    chrtAgingDistribution.AxisY.Clear();

        //    var values = new ChartValues<double>();
        //    var labels = new List<string>();

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        labels.Add(row["AgingBucket"].ToString());
        //        values.Add(Convert.ToDouble(row["AdvanceCount"]));
        //    }

        //    chrtAgingDistribution.Series = new SeriesCollection
        //    {
        //        new RowSeries   // Horizontal Bar
        //        {
        //            Title = "Advance Count",
        //            Values = values,
        //            DataLabels = true,
        //            LabelPoint = point => point.Y.ToString("N0"),
        //            Fill = Brushes.SteelBlue
        //        }
        //    };

        //    chrtAgingDistribution.AxisY.Add(new Axis
        //    {
        //        Labels = labels
        //    });

        //    chrtAgingDistribution.AxisX.Add(new Axis
        //    {
        //        Title = "No. of Advances",
        //        MinValue = 0
        //    });

        //    chrtAgingDistribution.LegendLocation = LegendLocation.None;
        //    chrtAgingDistribution.DisableAnimations = true;
        //    chrtAgingDistribution.AxisX[0].Separator.StrokeThickness = 0;
        //    chrtAgingDistribution.AxisY[0].Separator.StrokeThickness = 0;
        //}

        //private void GetAgingDistributionInfo(int txtClientID, int txtFinYearID)
        //{
        //    DataTable dt = objDashboardWidgetData.GetAgingDistributionInfo(txtClientID, txtFinYearID);

        //    if (dt.Rows.Count == 0)
        //        return;

        //    chrtAgingDistribution.Series.Clear();
        //    chrtAgingDistribution.AxisX.Clear();
        //    chrtAgingDistribution.AxisY.Clear();

        //    var seriesCollection = new SeriesCollection();
        //    var labels = new List<string>();

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        string bucket = row["AgingBucket"].ToString();
        //        double count = Convert.ToDouble(row["AdvanceCount"]);

        //        labels.Add(bucket);

        //        seriesCollection.Add(new ColumnSeries
        //        {
        //            Title = bucket,
        //            Values = new ChartValues<double> { count },
        //            DataLabels = true,
        //            Fill = GetBucketColor(bucket)
        //        });
        //    }

        //    chrtAgingDistribution.Series = seriesCollection;

        //    chrtAgingDistribution.AxisX.Add(new Axis
        //    {
        //        Labels = new[] { "Aging" }
        //    });

        //    chrtAgingDistribution.AxisY.Add(new Axis
        //    {
        //        Title = "No. of Advances",
        //        MinValue = 0
        //    });

        //    chrtAgingDistribution.LegendLocation = LegendLocation.Right;
        //    chrtAgingDistribution.DisableAnimations = true;
        //    chrtAgingDistribution.AxisX[0].Separator.StrokeThickness = 0;
        //    chrtAgingDistribution.AxisY[0].Separator.StrokeThickness = 0;
        //}

        private System.Windows.Media.Brush GetBucketColor(string bucket)
        {
            if (bucket.Contains("0-30"))
                return System.Windows.Media.Brushes.SeaGreen;

            if (bucket.Contains("31-60"))
                return System.Windows.Media.Brushes.Gold;

            if (bucket.Contains("61-90"))
                return System.Windows.Media.Brushes.Orange;

            if (bucket.Contains("90"))
                return System.Windows.Media.Brushes.IndianRed;

            return System.Windows.Media.Brushes.SteelBlue;
        }

        private void LoadWorkforceInfo(int txtClientID, int txtFinYearID)
        {
            bdgTotalActiveEmployees.Value = objDashboardWidgetData.GetActiveEmployeesCount(txtClientID, txtFinYearID);
            bdgTotalPresentEmployees.Value = objDashboardWidgetData.GetTotalEmployeesPresent(txtClientID, txtFinYearID);
            bdgTotalLeaveEmployees.Value = objDashboardWidgetData.GetTotalEmployeesOnLeave(txtClientID, txtFinYearID);
            bdgTotalLeaveApprovals.Value = objDashboardWidgetData.GetPendingLeaveApprovalCount(txtClientID, txtFinYearID);
            bdgTotalLeaveApprovals.Value = objDashboardWidgetData.GetPendingLeaveApprovalCount(txtClientID, txtFinYearID);
            bdgEmployeesWithWeeklyOff.Value = objDashboardWidgetData.GetEmployeesWeeklyOffCount(txtClientID, txtFinYearID);
            bdgEmployeesBirthday.Value = objDashboardWidgetData.GetCountOfAllEmployeesBirthdayCountToday(txtClientID, txtFinYearID);
            bdgWorkAnniversary.Value = objDashboardWidgetData.GetCountOfAllEmployeesWorkAnniversaryCountToday(txtClientID, txtFinYearID);
        }

        private void GetUpcomingHolidays(int txtClientID, int txtFinYearID)
        {
            dtgUpcomingHolidays.DataSource = null;
            dtgUpcomingHolidays.DataSource = objDashboardWidgetData.GetUpcomingHolidays(txtClientID, txtFinYearID);
            dtgUpcomingHolidays.Columns["PubHolTypeTitle"].HeaderText = "Holiday Type";
            dtgUpcomingHolidays.Columns["PubHolTypeTitle"].ReadOnly = true;
            dtgUpcomingHolidays.Columns["PubHolTypeTitle"].Width = 150;
            dtgUpcomingHolidays.Columns["PubHolidayTitle"].HeaderText = "Holiday Title";
            dtgUpcomingHolidays.Columns["PubHolidayTitle"].ReadOnly = true;
            dtgUpcomingHolidays.Columns["PubHolTypeTitle"].Width = 150;
            dtgUpcomingHolidays.Columns["PubHolDate"].HeaderText = "Holiday Date";
            dtgUpcomingHolidays.Columns["PubHolDate"].Width = 100;
            dtgUpcomingHolidays.Columns["PubHolDate"].ReadOnly = true;
            dtgUpcomingHolidays.Columns["PubHolDate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgUpcomingHolidays.Columns["PubHolDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgUpcomingHolidays.Columns["DaysRemaining"].HeaderText = "Due Day(s)";
            dtgUpcomingHolidays.Columns["DaysRemaining"].Width = 75;
            dtgUpcomingHolidays.Columns["DaysRemaining"].ReadOnly = true;
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            if (clsCurrentUser.UserName == null || clsCurrentUser.UserName == "")
            {
                myStatusBar.Items[0].Text = "User Name : ";
                myStatusBar.Items[1].Text = "Log In : " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt");

                frmActiveCompanyInfo frmActiveCompanyInfo = new frmActiveCompanyInfo(objSelectedClientFinYearInfo);
                frmActiveCompanyInfo.ShowDialog(this);
                objSelectedClientFinYearInfo = frmActiveCompanyInfo.GetSelectedClientAndFinYearDetails();

                objActiveClientInfo = objClientInfo.getClientInfo(objSelectedClientFinYearInfo.ClientID);
                objActiveFinYear = objFinYearInfo.GetSpecificFinYearInfo(objSelectedClientFinYearInfo.FinYearID);

                if(Convert.ToBoolean(objAppSettings.GetSpecificAppSettingsInfo("Show Dashboard KPIs").AppSettingValue.ToString()) == true)
                {
                    sptrDashboardContainer.Visible = true;
                    LoadDepartmentColumnChart(objSelectedClientFinYearInfo.ClientID, objSelectedClientFinYearInfo.FinYearID);
                    GetAdvanceSummary(objSelectedClientFinYearInfo.ClientID, objSelectedClientFinYearInfo.FinYearID);
                    GetAdvanceRiskBaseInfo(objSelectedClientFinYearInfo.ClientID, objSelectedClientFinYearInfo.FinYearID);
                    //GetAgingDistributionInfo(objSelectedClientFinYearInfo.ClientID, objSelectedClientFinYearInfo.FinYearID);
                    LoadWorkforceInfo(objSelectedClientFinYearInfo.ClientID, objSelectedClientFinYearInfo.FinYearID);
                    GetUpcomingHolidays(objSelectedClientFinYearInfo.ClientID, objSelectedClientFinYearInfo.FinYearID);
                }
                else
                {
                    sptrDashboardContainer.Visible = false;
                }
            }
            else
            {
                myStatusBar.Items[0].Text = "User Name : " + clsCurrentUser.UserName == null ? "" : clsCurrentUser.UserName.ToString();
                myStatusBar.Items[1].Text = "Log In : " + clsCurrentUser.LoginDateTime.ToString("dd-MMM-yyyy hh:mm:ss tt");
            }

            cmbLeaveApproval.Text = "Leave Approval (" + objLeaveInfo.getPendingLeaveApprovalList().Count + ")";
            cmbLeaveReject.Text = "Leave Reject (" + objLeaveInfo.getPendingLeaveApprovalList().Count + ")";

            this.Text = "Staffsync Dashboard - " + objActiveClientInfo.FirstOrDefault().ClientName + " [ FY - " + objActiveFinYear.FirstOrDefault().FinYearFromTo + " ]";
            CurrentUser.ClientID = objActiveClientInfo.FirstOrDefault().ClientID;

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
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 5;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Leave Approval Details";
                    sptrDashboardContainer.Visible = false;
                    frmLeavesApproval frmLeavesApproval = new frmLeavesApproval(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmLeavesApproval.MdiParent = this;
                    frmLeavesApproval.Dock = DockStyle.Fill;
                    frmLeavesApproval.Show();
                    frmLeavesApproval.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Leave Approval Details";
                        sptrDashboardContainer.Visible = false;
                        frmLeavesApproval frmLeavesApproval = new frmLeavesApproval(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmLeavesApproval.MdiParent = this;
                        frmLeavesApproval.Dock = DockStyle.Fill;
                        frmLeavesApproval.Show();
                        frmLeavesApproval.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Leave Approval Details";
                    sptrDashboardContainer.Visible = false;
                    frmLeavesApproval frmLeavesApproval = new frmLeavesApproval(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmLeavesApproval.MdiParent = this;
                    frmLeavesApproval.Dock = DockStyle.Fill;
                    frmLeavesApproval.Show();
                    frmLeavesApproval.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 9;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "User Management Details";
                    sptrDashboardContainer.Visible = false;
                    frmUserManagement frmUserManagement = new frmUserManagement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmUserManagement.MdiParent = this;
                    frmUserManagement.Dock = DockStyle.Fill;
                    frmUserManagement.Show();
                    frmUserManagement.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "User Management Details";
                        sptrDashboardContainer.Visible = false;
                        frmUserManagement frmUserManagement = new frmUserManagement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmUserManagement.MdiParent = this;
                        frmUserManagement.Dock = DockStyle.Fill;
                        frmUserManagement.Show();
                        frmUserManagement.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "User Management Details";
                    sptrDashboardContainer.Visible = false;
                    frmUserManagement frmUserManagement = new frmUserManagement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmUserManagement.MdiParent = this;
                    frmUserManagement.Dock = DockStyle.Fill;
                    frmUserManagement.Show();
                    frmUserManagement.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void rolesAndResponsibilitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 9;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Roles and Responsibilities Details";
                    sptrDashboardContainer.Visible = false;
                    frmRolesAndResponsibilities frmRolesAndResponsibilities = new frmRolesAndResponsibilities(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmRolesAndResponsibilities.MdiParent = this;
                    frmRolesAndResponsibilities.Dock = DockStyle.Fill;
                    frmRolesAndResponsibilities.Show();
                    frmRolesAndResponsibilities.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Roles and Responsibilities Details";
                        sptrDashboardContainer.Visible = false;
                        frmRolesAndResponsibilities frmRolesAndResponsibilities = new frmRolesAndResponsibilities(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmRolesAndResponsibilities.MdiParent = this;
                        frmRolesAndResponsibilities.Dock = DockStyle.Fill;
                        frmRolesAndResponsibilities.Show();
                        frmRolesAndResponsibilities.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Roles and Responsibilities Details";
                    sptrDashboardContainer.Visible = false;
                    frmRolesAndResponsibilities frmRolesAndResponsibilities = new frmRolesAndResponsibilities(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmRolesAndResponsibilities.MdiParent = this;
                    frmRolesAndResponsibilities.Dock = DockStyle.Fill;
                    frmRolesAndResponsibilities.Show();
                    frmRolesAndResponsibilities.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 9;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Modules Assignment Details";
                    sptrDashboardContainer.Visible = false;
                    frmModuleAssignment frmModuleAssignment = new frmModuleAssignment(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmModuleAssignment.MdiParent = this;
                    frmModuleAssignment.Dock = DockStyle.Fill;
                    frmModuleAssignment.Show();
                    frmModuleAssignment.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Modules Assignment Details";
                        sptrDashboardContainer.Visible = false;
                        frmModuleAssignment frmModuleAssignment = new frmModuleAssignment(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmModuleAssignment.MdiParent = this;
                        frmModuleAssignment.Dock = DockStyle.Fill;
                        frmModuleAssignment.Show();
                        frmModuleAssignment.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Modules Assignment Details";
                    sptrDashboardContainer.Visible = false;
                    frmModuleAssignment frmModuleAssignment = new frmModuleAssignment(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmModuleAssignment.MdiParent = this;
                    frmModuleAssignment.Dock = DockStyle.Fill;
                    frmModuleAssignment.Show();
                    frmModuleAssignment.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void assetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 6;

            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
            {
                lblDashboardTitle.Text = "Dashboard";
                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void assetsAllotmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AppModuleID = 6;

            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
            {
                lblDashboardTitle.Text = "Dashboard";
                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void roleProfileManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 8;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Roles Profile Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmRolesProfileMaster frmRolesProfileMaster = new frmRolesProfileMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmRolesProfileMaster.MdiParent = this;
                    frmRolesProfileMaster.Dock = DockStyle.Fill;
                    frmRolesProfileMaster.Show();
                    frmRolesProfileMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Roles Profile Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmRolesProfileMaster frmRolesProfileMaster = new frmRolesProfileMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmRolesProfileMaster.MdiParent = this;
                        frmRolesProfileMaster.Dock = DockStyle.Fill;
                        frmRolesProfileMaster.Show();
                        frmRolesProfileMaster.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Roles Profile Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmRolesProfileMaster frmRolesProfileMaster = new frmRolesProfileMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
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
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
                            sptrDashboardContainer.Visible = false;
                            frmUserManagement frmUserManagement = new frmUserManagement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                            frmUserManagement.MdiParent = this;
                            frmUserManagement.Dock = DockStyle.Fill;
                            frmUserManagement.Show();
                            frmUserManagement.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                            {
                                lblDashboardTitle.Text = "User Management Details";
                                sptrDashboardContainer.Visible = false;
                                frmUserManagement frmUserManagement = new frmUserManagement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                                frmUserManagement.MdiParent = this;
                                frmUserManagement.Dock = DockStyle.Fill;
                                frmUserManagement.Show();
                                frmUserManagement.WindowState = FormWindowState.Maximized;
                            }
                            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                            {

                            }
                            else
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "User Management Details";
                            sptrDashboardContainer.Visible = false;
                            frmUserManagement frmUserManagement = new frmUserManagement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
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
                            sptrDashboardContainer.Visible = false;
                            frmRolesAndResponsibilities frmRolesAndResponsibilities = new frmRolesAndResponsibilities(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                            frmRolesAndResponsibilities.MdiParent = this;
                            frmRolesAndResponsibilities.Dock = DockStyle.Fill;
                            frmRolesAndResponsibilities.Show();
                            frmRolesAndResponsibilities.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                            {
                                lblDashboardTitle.Text = "Roles and Responsibilities Details";
                                sptrDashboardContainer.Visible = false;
                                frmRolesAndResponsibilities frmRolesAndResponsibilities = new frmRolesAndResponsibilities(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                                frmRolesAndResponsibilities.MdiParent = this;
                                frmRolesAndResponsibilities.Dock = DockStyle.Fill;
                                frmRolesAndResponsibilities.Show();
                                frmRolesAndResponsibilities.WindowState = FormWindowState.Maximized;
                            }
                            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                            {

                            }
                            else
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Roles and Responsibilities Details";
                            sptrDashboardContainer.Visible = false;
                            frmRolesAndResponsibilities frmRolesAndResponsibilities = new frmRolesAndResponsibilities(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
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
                            sptrDashboardContainer.Visible = false;
                            frmModuleAssignment frmModuleAssignment = new frmModuleAssignment(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                            frmModuleAssignment.MdiParent = this;
                            frmModuleAssignment.Dock = DockStyle.Fill;
                            frmModuleAssignment.Show();
                            frmModuleAssignment.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                            {
                                lblDashboardTitle.Text = "Module Assignment Details";
                                sptrDashboardContainer.Visible = false;
                                frmModuleAssignment frmModuleAssignment = new frmModuleAssignment(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                                frmModuleAssignment.MdiParent = this;
                                frmModuleAssignment.Dock = DockStyle.Fill;
                                frmModuleAssignment.Show();
                                frmModuleAssignment.WindowState = FormWindowState.Maximized;
                            }
                            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                            {

                            }
                            else
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Module Assignment Details";
                            sptrDashboardContainer.Visible = false;
                            frmModuleAssignment frmModuleAssignment = new frmModuleAssignment(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
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
                            sptrDashboardContainer.Visible = false;
                            frmRolesProfileMaster frmRolesProfileMaster = new frmRolesProfileMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                            frmRolesProfileMaster.MdiParent = this;
                            frmRolesProfileMaster.Dock = DockStyle.Fill;
                            frmRolesProfileMaster.Show();
                            frmRolesProfileMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                            {
                                lblDashboardTitle.Text = "Roles Profile Master Details";
                                sptrDashboardContainer.Visible = false;
                                frmRolesProfileMaster frmRolesProfileMaster = new frmRolesProfileMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                                frmRolesProfileMaster.MdiParent = this;
                                frmRolesProfileMaster.Dock = DockStyle.Fill;
                                frmRolesProfileMaster.Show();
                                frmRolesProfileMaster.WindowState = FormWindowState.Maximized;
                            }
                            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                            {

                            }
                            else
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Roles Profile Master Details";
                            sptrDashboardContainer.Visible = false;
                            frmRolesProfileMaster frmRolesProfileMaster = new frmRolesProfileMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
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
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (e.ClickedItem.Tag == null)
                return;

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));

            switch (e.ClickedItem.Tag.ToString())
            {
                case "cmbManageEmployeeAttendance":
                    AppModuleID = 4;

                    //frmDailyAttendanceProcess frmDailyAttendanceProcessDetails = new frmDailyAttendanceProcess(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo);
                    //frmDailyAttendanceProcessDetails.MdiParent = this;
                    //frmDailyAttendanceProcessDetails.Dock = DockStyle.Fill;
                    //frmDailyAttendanceProcessDetails.Show();
                    //frmDailyAttendanceProcessDetails.WindowState = FormWindowState.Maximized;
                    //return;

                    if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Attendance Master Details";
                            sptrDashboardContainer.Visible = false;
                            frmAttendanceMater frmAttendanceMaterDetails = new frmAttendanceMater(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                            frmAttendanceMaterDetails.MdiParent = this;
                            frmAttendanceMaterDetails.Dock = DockStyle.Fill;
                            frmAttendanceMaterDetails.Show();
                            frmAttendanceMaterDetails.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                            {
                                lblDashboardTitle.Text = "Attendance Master Details";
                                sptrDashboardContainer.Visible = false;
                                frmAttendanceMater frmAttendanceMaterDetails = new frmAttendanceMater(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                                frmAttendanceMaterDetails.MdiParent = this;
                                frmAttendanceMaterDetails.Dock = DockStyle.Fill;
                                frmAttendanceMaterDetails.Show();
                                frmAttendanceMaterDetails.WindowState = FormWindowState.Maximized;
                            }
                            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                            {

                            }
                            else
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Attendance Master Details";
                            sptrDashboardContainer.Visible = false;
                            frmAttendanceMater frmAttendanceMaterDetails = new frmAttendanceMater(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
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
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
                            sptrDashboardContainer.Visible = false;
                            frmLeavesMaster frmLeavesMasterDetails = new frmLeavesMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                            frmLeavesMasterDetails.MdiParent = this;
                            frmLeavesMasterDetails.Dock = DockStyle.Fill;
                            frmLeavesMasterDetails.Show();
                            frmLeavesMasterDetails.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                            {
                                lblDashboardTitle.Text = "Leaves Master Details";
                                sptrDashboardContainer.Visible = false;
                                frmLeavesMaster frmLeavesMasterDetails = new frmLeavesMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                                frmLeavesMasterDetails.MdiParent = this;
                                frmLeavesMasterDetails.Dock = DockStyle.Fill;
                                frmLeavesMasterDetails.Show();
                                frmLeavesMasterDetails.WindowState = FormWindowState.Maximized;
                            }
                            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                            {

                            }
                            else
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Leaves Master Details";
                            sptrDashboardContainer.Visible = false;
                            frmLeavesMaster frmLeavesMasterDetails = new frmLeavesMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
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
                            sptrDashboardContainer.Visible = false;
                            frmLeavesApproval frmLeavesApproval = new frmLeavesApproval(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                            frmLeavesApproval.MdiParent = this;
                            frmLeavesApproval.Dock = DockStyle.Fill;
                            frmLeavesApproval.Show();
                            frmLeavesApproval.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                            {
                                lblDashboardTitle.Text = "Leaves Approval Details";
                                sptrDashboardContainer.Visible = false;
                                frmLeavesApproval frmLeavesApproval = new frmLeavesApproval(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                                frmLeavesApproval.MdiParent = this;
                                frmLeavesApproval.Dock = DockStyle.Fill;
                                frmLeavesApproval.Show();
                                frmLeavesApproval.WindowState = FormWindowState.Maximized;
                            }
                            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                            {

                            }
                            else
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Leaves Approval Details";
                            sptrDashboardContainer.Visible = false;
                            frmLeavesApproval frmLeavesApproval = new frmLeavesApproval(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
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
                            sptrDashboardContainer.Visible = false;
                            frmLeavesReject frmLeavesReject = new frmLeavesReject(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
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
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                            {
                                lblDashboardTitle.Text = "Leaves Rejection Details";
                                sptrDashboardContainer.Visible = false;
                                frmLeavesReject frmLeavesReject = new frmLeavesReject(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                                frmLeavesReject.MdiParent = this;
                                frmLeavesReject.Dock = DockStyle.Fill;
                                frmLeavesReject.Show();
                                frmLeavesReject.WindowState = FormWindowState.Maximized;
                            }
                            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                            {

                            }
                            else
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Leaves Rejection Details";
                            sptrDashboardContainer.Visible = false;
                            frmLeavesReject frmLeavesReject = new frmLeavesReject(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
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
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
                            sptrDashboardContainer.Visible = false;
                            frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                            frmLastCompanyMaster.MdiParent = this;
                            frmLastCompanyMaster.Dock = DockStyle.Fill;
                            frmLastCompanyMaster.Show();
                            frmLastCompanyMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                            {
                                lblDashboardTitle.Text = "Last Company Master Details";
                                sptrDashboardContainer.Visible = false;
                                frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                                frmLastCompanyMaster.MdiParent = this;
                                frmLastCompanyMaster.Dock = DockStyle.Fill;
                                frmLastCompanyMaster.Show();
                                frmLastCompanyMaster.WindowState = FormWindowState.Maximized;
                            }
                            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                            {

                            }
                            else
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Last Company Master Details";
                            sptrDashboardContainer.Visible = false;
                            frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
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
                            sptrDashboardContainer.Visible = false;
                            frmEduQualMaster frmEduQualMaster = new frmEduQualMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                            frmEduQualMaster.MdiParent = this;
                            frmEduQualMaster.Dock = DockStyle.Fill;
                            frmEduQualMaster.Show();
                            frmEduQualMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                            {
                                lblDashboardTitle.Text = "Qualification Master Details";
                                sptrDashboardContainer.Visible = false;
                                frmEduQualMaster frmEduQualMaster = new frmEduQualMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                                frmEduQualMaster.MdiParent = this;
                                frmEduQualMaster.Dock = DockStyle.Fill;
                                frmEduQualMaster.Show();
                                frmEduQualMaster.WindowState = FormWindowState.Maximized;
                            }
                            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                            {

                            }
                            else
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Qualification Master Details";
                            sptrDashboardContainer.Visible = false;
                            frmEduQualMaster frmEduQualMaster = new frmEduQualMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
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
                            sptrDashboardContainer.Visible = false;
                            frmSkillsMaster frmSkillsMaster = new frmSkillsMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                            frmSkillsMaster.MdiParent = this;
                            frmSkillsMaster.Dock = DockStyle.Fill;
                            frmSkillsMaster.Show();
                            frmSkillsMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                            {
                                lblDashboardTitle.Text = "Skills Master Details";
                                sptrDashboardContainer.Visible = false;
                                frmSkillsMaster frmSkillsMaster = new frmSkillsMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                                frmSkillsMaster.MdiParent = this;
                                frmSkillsMaster.Dock = DockStyle.Fill;
                                frmSkillsMaster.Show();
                                frmSkillsMaster.WindowState = FormWindowState.Maximized;
                            }
                            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                            {

                            }
                            else
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        sptrDashboardContainer.Visible = false;
                        frmSkillsMaster frmSkillsMaster = new frmSkillsMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
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
                            sptrDashboardContainer.Visible = false;
                            frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                            frmDepartmentMaster.MdiParent = this;
                            frmDepartmentMaster.Dock = DockStyle.Fill;
                            frmDepartmentMaster.Show();
                            frmDepartmentMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                            {
                                lblDashboardTitle.Text = "Department Master Details";
                                sptrDashboardContainer.Visible = false;
                                frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                                frmDepartmentMaster.MdiParent = this;
                                frmDepartmentMaster.Dock = DockStyle.Fill;
                                frmDepartmentMaster.Show();
                                frmDepartmentMaster.WindowState = FormWindowState.Maximized;
                            }
                            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                            {

                            }
                            else
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Department Master Details";
                            sptrDashboardContainer.Visible = false;
                            frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
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
                            sptrDashboardContainer.Visible = false;
                            frmDesignationMaster frmDesignationMaster = new frmDesignationMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                            frmDesignationMaster.MdiParent = this;
                            frmDesignationMaster.Dock = DockStyle.Fill;
                            frmDesignationMaster.Show();
                            frmDesignationMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                            {
                                lblDashboardTitle.Text = "Designation Master Details";
                                sptrDashboardContainer.Visible = false;
                                frmDesignationMaster frmDesignationMaster = new frmDesignationMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                                frmDesignationMaster.MdiParent = this;
                                frmDesignationMaster.Dock = DockStyle.Fill;
                                frmDesignationMaster.Show();
                                frmDesignationMaster.WindowState = FormWindowState.Maximized;
                            }
                            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                            {

                            }
                            else
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Designation Master Details";
                            sptrDashboardContainer.Visible = false;
                            frmDesignationMaster frmDesignationMaster = new frmDesignationMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
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
                            sptrDashboardContainer.Visible = false;
                            frmCountryMaster frmCountryMaster = new frmCountryMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                            frmCountryMaster.MdiParent = this;
                            frmCountryMaster.Dock = DockStyle.Fill;
                            frmCountryMaster.Show();
                            frmCountryMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                            {
                                lblDashboardTitle.Text = "Country Master Details";
                                sptrDashboardContainer.Visible = false;
                                frmCountryMaster frmCountryMaster = new frmCountryMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                                frmCountryMaster.MdiParent = this;
                                frmCountryMaster.Dock = DockStyle.Fill;
                                frmCountryMaster.Show();
                                frmCountryMaster.WindowState = FormWindowState.Maximized;
                            }
                            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                            {

                            }
                            else
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Country Master Details";
                            sptrDashboardContainer.Visible = false;
                            frmCountryMaster frmCountryMaster = new frmCountryMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
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
                            sptrDashboardContainer.Visible = false;
                            frmStateMaster frmStateMaster = new frmStateMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                            frmStateMaster.MdiParent = this;
                            frmStateMaster.Dock = DockStyle.Fill;
                            frmStateMaster.Show();
                            frmStateMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                            {
                                lblDashboardTitle.Text = "State Master Details";
                                sptrDashboardContainer.Visible = false;
                                frmStateMaster frmStateMaster = new frmStateMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                                frmStateMaster.MdiParent = this;
                                frmStateMaster.Dock = DockStyle.Fill;
                                frmStateMaster.Show();
                                frmStateMaster.WindowState = FormWindowState.Maximized;
                            }
                            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                            {

                            }
                            else
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "State Master Details";
                            sptrDashboardContainer.Visible = false;
                            frmStateMaster frmStateMaster = new frmStateMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
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
                            sptrDashboardContainer.Visible = false;
                            frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                            frmRelationshipMaster.MdiParent = this;
                            frmRelationshipMaster.Dock = DockStyle.Fill;
                            frmRelationshipMaster.Show();
                            frmRelationshipMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                            {
                                lblDashboardTitle.Text = "Relationship Master Details";
                                sptrDashboardContainer.Visible = false;
                                frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                                frmRelationshipMaster.MdiParent = this;
                                frmRelationshipMaster.Dock = DockStyle.Fill;
                                frmRelationshipMaster.Show();
                                frmRelationshipMaster.WindowState = FormWindowState.Maximized;
                            }
                            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                            {

                            }
                            else
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Relationship Master Details";
                            sptrDashboardContainer.Visible = false;
                            frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
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
                            sptrDashboardContainer.Visible = false;
                            frmLeaveTypeMaster frmLeaveTypeMaster = new frmLeaveTypeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                            frmLeaveTypeMaster.MdiParent = this;
                            frmLeaveTypeMaster.Dock = DockStyle.Fill;
                            frmLeaveTypeMaster.Show();
                            frmLeaveTypeMaster.WindowState = FormWindowState.Maximized;
                        }
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
                    {
                        if (this.MdiChildren.Length == 0)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                            {
                                lblDashboardTitle.Text = "Leave Type Master Details";
                                sptrDashboardContainer.Visible = false;
                                frmLeaveTypeMaster frmLeaveTypeMaster = new frmLeaveTypeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                                frmLeaveTypeMaster.MdiParent = this;
                                frmLeaveTypeMaster.Dock = DockStyle.Fill;
                                frmLeaveTypeMaster.Show();
                                frmLeaveTypeMaster.WindowState = FormWindowState.Maximized;
                            }
                            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                        {
                            if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                            {

                            }
                            else
                            {
                                lblDashboardTitle.Text = "Dashboard";
                                MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        if (this.MdiChildren.Length == 0)
                        {
                            lblDashboardTitle.Text = "Leave Type Master Details";
                            sptrDashboardContainer.Visible = false;
                            frmLeaveTypeMaster frmLeaveTypeMaster = new frmLeaveTypeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
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
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Last Company Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmLastCompanyMaster.MdiParent = this;
                    frmLastCompanyMaster.Dock = DockStyle.Fill;
                    frmLastCompanyMaster.Show();
                    frmLastCompanyMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Last Company Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmLastCompanyMaster.MdiParent = this;
                        frmLastCompanyMaster.Dock = DockStyle.Fill;
                        frmLastCompanyMaster.Show();
                        frmLastCompanyMaster.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Last Company Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmLastCompanyMaster frmLastCompanyMaster = new frmLastCompanyMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmLastCompanyMaster.MdiParent = this;
                    frmLastCompanyMaster.Dock = DockStyle.Fill;
                    frmLastCompanyMaster.Show();
                    frmLastCompanyMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbEducationList01_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Qualification Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmEduQualMaster frmEduQualMaster = new frmEduQualMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmEduQualMaster.MdiParent = this;
                    frmEduQualMaster.Dock = DockStyle.Fill;
                    frmEduQualMaster.Show();
                    frmEduQualMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Qualification Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmEduQualMaster frmEduQualMaster = new frmEduQualMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmEduQualMaster.MdiParent = this;
                        frmEduQualMaster.Dock = DockStyle.Fill;
                        frmEduQualMaster.Show();
                        frmEduQualMaster.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Qualification Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmEduQualMaster frmEduQualMaster = new frmEduQualMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmEduQualMaster.MdiParent = this;
                    frmEduQualMaster.Dock = DockStyle.Fill;
                    frmEduQualMaster.Show();
                    frmEduQualMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbSkillsList01_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Skills Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmSkillsMaster frmSkillsMaster = new frmSkillsMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmSkillsMaster.MdiParent = this;
                    frmSkillsMaster.Dock = DockStyle.Fill;
                    frmSkillsMaster.Show();
                    frmSkillsMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Skills Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmSkillsMaster frmSkillsMaster = new frmSkillsMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmSkillsMaster.MdiParent = this;
                        frmSkillsMaster.Dock = DockStyle.Fill;
                        frmSkillsMaster.Show();
                        frmSkillsMaster.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                sptrDashboardContainer.Visible = false;
                frmSkillsMaster frmSkillsMaster = new frmSkillsMaster();
                frmSkillsMaster.MdiParent = this;
                frmSkillsMaster.Dock = DockStyle.Fill;
                frmSkillsMaster.Show();
                frmSkillsMaster.WindowState = FormWindowState.Maximized;

            }
        }

        private void cmbDepartmentList01_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Department Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmDepartmentMaster.MdiParent = this;
                    frmDepartmentMaster.Dock = DockStyle.Fill;
                    frmDepartmentMaster.Show();
                    frmDepartmentMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Department Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmDepartmentMaster.MdiParent = this;
                        frmDepartmentMaster.Dock = DockStyle.Fill;
                        frmDepartmentMaster.Show();
                        frmDepartmentMaster.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Department Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmDepartmentMaster frmDepartmentMaster = new frmDepartmentMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmDepartmentMaster.MdiParent = this;
                    frmDepartmentMaster.Dock = DockStyle.Fill;
                    frmDepartmentMaster.Show();
                    frmDepartmentMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbDesignationList01_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Designation Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmDesignationMaster frmDesignationMaster = new frmDesignationMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmDesignationMaster.MdiParent = this;
                    frmDesignationMaster.Dock = DockStyle.Fill;
                    frmDesignationMaster.Show();
                    frmDesignationMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Designation Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmDesignationMaster frmDesignationMaster = new frmDesignationMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmDesignationMaster.MdiParent = this;
                        frmDesignationMaster.Dock = DockStyle.Fill;
                        frmDesignationMaster.Show();
                        frmDesignationMaster.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Designation Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmDesignationMaster frmDesignationMaster = new frmDesignationMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmDesignationMaster.MdiParent = this;
                    frmDesignationMaster.Dock = DockStyle.Fill;
                    frmDesignationMaster.Show();
                    frmDesignationMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbStatesList01_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "State Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmStateMaster frmStateMaster = new frmStateMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmStateMaster.MdiParent = this;
                    frmStateMaster.Dock = DockStyle.Fill;
                    frmStateMaster.Show();
                    frmStateMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "State Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmStateMaster frmStateMaster = new frmStateMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmStateMaster.MdiParent = this;
                        frmStateMaster.Dock = DockStyle.Fill;
                        frmStateMaster.Show();
                        frmStateMaster.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "State Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmStateMaster frmStateMaster = new frmStateMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmStateMaster.MdiParent = this;
                    frmStateMaster.Dock = DockStyle.Fill;
                    frmStateMaster.Show();
                    frmStateMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbRelationshipList01_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Relationship Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmRelationshipMaster.MdiParent = this;
                    frmRelationshipMaster.Dock = DockStyle.Fill;
                    frmRelationshipMaster.Show();
                    frmRelationshipMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Relationship Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmRelationshipMaster.MdiParent = this;
                        frmRelationshipMaster.Dock = DockStyle.Fill;
                        frmRelationshipMaster.Show();
                        frmRelationshipMaster.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Relationship Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmRelationshipMaster frmRelationshipMaster = new frmRelationshipMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmRelationshipMaster.MdiParent = this;
                    frmRelationshipMaster.Dock = DockStyle.Fill;
                    frmRelationshipMaster.Show();
                    frmRelationshipMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbPayrollSystem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 3;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Payroll Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmPayrollMaster payrollMaster = new frmPayrollMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    payrollMaster.MdiParent = this;
                    payrollMaster.Dock = DockStyle.Fill;
                    payrollMaster.Show();
                    payrollMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Payroll Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmPayrollMaster payrollMaster = new frmPayrollMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        payrollMaster.MdiParent = this;
                        payrollMaster.Dock = DockStyle.Fill;
                        payrollMaster.Show();
                        payrollMaster.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Payroll Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmPayrollMaster payrollMaster = new frmPayrollMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    payrollMaster.MdiParent = this;
                    payrollMaster.Dock = DockStyle.Fill;
                    payrollMaster.Show();
                    payrollMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbEarningsList_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));

            AppModuleID = 3;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Allowence Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmPayrollAllowences frmPayrollAllowences = new frmPayrollAllowences(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmPayrollAllowences.MdiParent = this;
                    frmPayrollAllowences.Dock = DockStyle.Fill;
                    frmPayrollAllowences.Show();
                    frmPayrollAllowences.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Allowence Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmPayrollAllowences frmPayrollAllowences = new frmPayrollAllowences(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmPayrollAllowences.MdiParent = this;
                        frmPayrollAllowences.Dock = DockStyle.Fill;
                        frmPayrollAllowences.Show();
                        frmPayrollAllowences.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Allowence Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmPayrollAllowences frmPayrollAllowences = new frmPayrollAllowences(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmPayrollAllowences.MdiParent = this;
                    frmPayrollAllowences.Dock = DockStyle.Fill;
                    frmPayrollAllowences.Show();
                    frmPayrollAllowences.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbDeductionsList_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));

            AppModuleID = 3;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Deduction Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmPayrollDeductions frmPayrollDeductions = new frmPayrollDeductions(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmPayrollDeductions.MdiParent = this;
                    frmPayrollDeductions.Dock = DockStyle.Fill;
                    frmPayrollDeductions.Show();
                    frmPayrollDeductions.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Deduction Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmPayrollDeductions frmPayrollDeductions = new frmPayrollDeductions(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmPayrollDeductions.MdiParent = this;
                        frmPayrollDeductions.Dock = DockStyle.Fill;
                        frmPayrollDeductions.Show();
                        frmPayrollDeductions.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Deduction Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmPayrollDeductions frmPayrollDeductions = new frmPayrollDeductions(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmPayrollDeductions.MdiParent = this;
                    frmPayrollDeductions.Dock = DockStyle.Fill;
                    frmPayrollDeductions.Show();
                    frmPayrollDeductions.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbReimbursmentList_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));

            AppModuleID = 3;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Reimbursement Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmReimbursement frmReimbursement = new frmReimbursement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmReimbursement.MdiParent = this;
                    frmReimbursement.Dock = DockStyle.Fill;
                    frmReimbursement.Show();
                    frmReimbursement.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Reimbursement Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmReimbursement frmReimbursement = new frmReimbursement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmReimbursement.MdiParent = this;
                        frmReimbursement.Dock = DockStyle.Fill;
                        frmReimbursement.Show();
                        frmReimbursement.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Reimbursement Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmReimbursement frmReimbursement = new frmReimbursement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmReimbursement.MdiParent = this;
                    frmReimbursement.Dock = DockStyle.Fill;
                    frmReimbursement.Show();
                    frmReimbursement.WindowState = FormWindowState.Maximized;
                }
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
                sptrDashboardContainer.Visible = false;
                frmLeaveStatement frmLeaveStatement = new frmLeaveStatement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmLeaveStatement.MdiParent = this;
                frmLeaveStatement.Dock = DockStyle.Fill;
                frmLeaveStatement.Show();
                frmLeaveStatement.WindowState = FormWindowState.Maximized;
            }
        }

        private void tlbCompanyInfo_Click(object sender, EventArgs e)
        {
            //if (CurrentUser.ClientID == 0)
            //{
            //    MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));

            //AppModuleID = 1;

            //if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            //{
            //    if (this.MdiChildren.Length == 0)
            //    {
            //        lblDashboardTitle.Text = "Company Master Details";
            //        frmCompanyInfo frmCompanyInfo = new frmCompanyInfo(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
            //        frmCompanyInfo.MdiParent = this;
            //        frmCompanyInfo.Dock = DockStyle.Fill;
            //        frmCompanyInfo.Show();
            //        frmCompanyInfo.WindowState = FormWindowState.Maximized;
            //    }
            //}
            //else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            //{
            //    if (this.MdiChildren.Length == 0)
            //    {
            //        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
            //        {
            //            lblDashboardTitle.Text = "Company Master Details";
            //            frmCompanyInfo frmCompanyInfo = new frmCompanyInfo(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
            //            frmCompanyInfo.MdiParent = this;
            //            frmCompanyInfo.Dock = DockStyle.Fill;
            //            frmCompanyInfo.Show();
            //            frmCompanyInfo.WindowState = FormWindowState.Maximized;
            //        }
            //        else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
            //        {
            //            lblDashboardTitle.Text = "Dashboard";
            //            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            return;
            //        }
            //    }
            //}
            //else
            //{
            //    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
            //    {
            //        if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
            //        {

            //        }
            //        else
            //        {
            //            lblDashboardTitle.Text = "Dashboard";
            //            MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            return;
            //        }
            //    }
            //    if (this.MdiChildren.Length == 0)
            //    {
            //        lblDashboardTitle.Text = "Company Master Details";
            //        frmCompanyInfo frmCompanyInfo = new frmCompanyInfo(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
            //        frmCompanyInfo.MdiParent = this;
            //        frmCompanyInfo.Dock = DockStyle.Fill;
            //        frmCompanyInfo.Show();
            //        frmCompanyInfo.WindowState = FormWindowState.Maximized;
            //    }
            //}
        }

        private void cmbApplyLeave_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Current User Leave Master Details";
                sptrDashboardContainer.Visible = false;
                frmCurrentUserLeaveMaster frmCurrentUserLeaveMaster = new frmCurrentUserLeaveMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmCurrentUserLeaveMaster.MdiParent = this;
                frmCurrentUserLeaveMaster.Dock = DockStyle.Fill;
                frmCurrentUserLeaveMaster.Show();
                frmCurrentUserLeaveMaster.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbIndividualLeaveStatement_Click(object sender, EventArgs e)
        {
            //if (this.MdiChildren.Length == 0)
            //{
            //    lblDashboardTitle.Text = "Leave Statement Details";
            //    frmLeaveStatement frmLeaveStatement = new frmLeaveStatement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
            //    frmLeaveStatement.MdiParent = this;
            //    frmLeaveStatement.Dock = DockStyle.Fill;
            //    frmLeaveStatement.Show();
            //    frmLeaveStatement.WindowState = FormWindowState.Maximized;
            //}
        }

        private void cmbLeaveEntitlement_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));

            AppModuleID = 1;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Leave Entitlement Details";
                    sptrDashboardContainer.Visible = false;
                    frmEmpLeaveEntitlement frmEmpLeaveEntitlement = new frmEmpLeaveEntitlement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
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
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Leave Entitlement Details";
                        sptrDashboardContainer.Visible = false;
                        frmEmpLeaveEntitlement frmEmpLeaveEntitlement = new frmEmpLeaveEntitlement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmEmpLeaveEntitlement.MdiParent = this;
                        frmEmpLeaveEntitlement.Dock = DockStyle.Fill;
                        frmEmpLeaveEntitlement.Show();
                        frmEmpLeaveEntitlement.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Leave Entitlement Details";
                    sptrDashboardContainer.Visible = false;
                    frmEmpLeaveEntitlement frmEmpLeaveEntitlement = new frmEmpLeaveEntitlement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmEmpLeaveEntitlement.MdiParent = this;
                    frmEmpLeaveEntitlement.Dock = DockStyle.Fill;
                    frmEmpLeaveEntitlement.Show();
                    frmEmpLeaveEntitlement.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbConfigureSalaryProfile_Click(object sender, EventArgs e)
        {
            //frmUpdateSalaryProfile frmUpdateSalaryProfile = new frmUpdateSalaryProfile();
            //frmUpdateSalaryProfile.Show();
        }

        private void cmbSalaryProfile_Click_1(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Salary Profile Details";
                    sptrDashboardContainer.Visible = false;
                    frmSalaryProfile frmSalaryProfile = new frmSalaryProfile(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmSalaryProfile.MdiParent = this;
                    frmSalaryProfile.Dock = DockStyle.Fill;
                    frmSalaryProfile.Show();
                    frmSalaryProfile.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Salary Profile Details";
                        sptrDashboardContainer.Visible = false;
                        frmSalaryProfile frmSalaryProfile = new frmSalaryProfile(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmSalaryProfile.MdiParent = this;
                        frmSalaryProfile.Dock = DockStyle.Fill;
                        frmSalaryProfile.Show();
                        frmSalaryProfile.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Salary Profile Details";
                    sptrDashboardContainer.Visible = false;
                    frmSalaryProfile frmSalaryProfile = new frmSalaryProfile(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmSalaryProfile.MdiParent = this;
                    frmSalaryProfile.Dock = DockStyle.Fill;
                    frmSalaryProfile.Show();
                    frmSalaryProfile.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbConfigureSalaryProfile_Click_1(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Salary Profile Details";
                    sptrDashboardContainer.Visible = false;
                    frmUpdateSalaryProfile frmUpdateSalaryProfile = new frmUpdateSalaryProfile(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmUpdateSalaryProfile.MdiParent = this;
                    frmUpdateSalaryProfile.Dock = DockStyle.Fill;
                    frmUpdateSalaryProfile.Show();
                    frmUpdateSalaryProfile.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Salary Profile Details";
                        sptrDashboardContainer.Visible = false;
                        frmUpdateSalaryProfile frmUpdateSalaryProfile = new frmUpdateSalaryProfile(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmUpdateSalaryProfile.MdiParent = this;
                        frmUpdateSalaryProfile.Dock = DockStyle.Fill;
                        frmUpdateSalaryProfile.Show();
                        frmUpdateSalaryProfile.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Salary Profile Details";
                    sptrDashboardContainer.Visible = false;
                    frmUpdateSalaryProfile frmUpdateSalaryProfile = new frmUpdateSalaryProfile(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmUpdateSalaryProfile.MdiParent = this;
                    frmUpdateSalaryProfile.Dock = DockStyle.Fill;
                    frmUpdateSalaryProfile.Show();
                    frmUpdateSalaryProfile.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void bulkLeaveApprovalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Bulk Leave Approval Details";
                sptrDashboardContainer.Visible = false;
                frmEmpBulkLeaveApproval frmEmpBulkLeaveApproval = new frmEmpBulkLeaveApproval(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmEmpBulkLeaveApproval.MdiParent = this;
                frmEmpBulkLeaveApproval.Dock = DockStyle.Fill;
                frmEmpBulkLeaveApproval.Show();
                frmEmpBulkLeaveApproval.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbLeaveTypeMaster_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Leave Type Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmLeaveTypeMaster frmLeaveTypeMaster = new frmLeaveTypeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmLeaveTypeMaster.MdiParent = this;
                    frmLeaveTypeMaster.Dock = DockStyle.Fill;
                    frmLeaveTypeMaster.Show();
                    frmLeaveTypeMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Leave Type Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmLeaveTypeMaster frmLeaveTypeMaster = new frmLeaveTypeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmLeaveTypeMaster.MdiParent = this;
                        frmLeaveTypeMaster.Dock = DockStyle.Fill;
                        frmLeaveTypeMaster.Show();
                        frmLeaveTypeMaster.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Leave Type Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmLeaveTypeMaster frmLeaveTypeMaster = new frmLeaveTypeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
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
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));
            
            AppModuleID = 2;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Employee Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmEmployeeMaster frmEmployeeMasterDetails = new frmEmployeeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmEmployeeMasterDetails.MdiParent = this;
                    frmEmployeeMasterDetails.Dock = DockStyle.Fill;
                    frmEmployeeMasterDetails.Show();
                    frmEmployeeMasterDetails.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Employee Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmEmployeeMaster frmEmployeeMasterDetails = new frmEmployeeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmEmployeeMasterDetails.MdiParent = this;
                        frmEmployeeMasterDetails.Dock = DockStyle.Fill;
                        frmEmployeeMasterDetails.Show();
                        frmEmployeeMasterDetails.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Employee Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmEmployeeMaster frmEmployeeMasterDetails = new frmEmployeeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmEmployeeMasterDetails.MdiParent = this;
                    frmEmployeeMasterDetails.Dock = DockStyle.Fill;
                    frmEmployeeMasterDetails.Show();
                    frmEmployeeMasterDetails.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbLeaveEntitlement1_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Employee Leave Entitlement Details";
                    sptrDashboardContainer.Visible = false;
                    frmEmpLeaveEntitlement frmEmpLeaveEntitlement = new frmEmpLeaveEntitlement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmEmpLeaveEntitlement.MdiParent = this;
                    frmEmpLeaveEntitlement.Dock = DockStyle.Fill;
                    frmEmpLeaveEntitlement.Show();
                    frmEmpLeaveEntitlement.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Employee Leave Entitlement Details";
                        sptrDashboardContainer.Visible = false;
                        frmEmpLeaveEntitlement frmEmpLeaveEntitlement = new frmEmpLeaveEntitlement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmEmpLeaveEntitlement.MdiParent = this;
                        frmEmpLeaveEntitlement.Dock = DockStyle.Fill;
                        frmEmpLeaveEntitlement.Show();
                        frmEmpLeaveEntitlement.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Employee Leave Entitlement Details";
                    sptrDashboardContainer.Visible = false;
                    frmEmpLeaveEntitlement frmEmpLeaveEntitlement = new frmEmpLeaveEntitlement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmEmpLeaveEntitlement.MdiParent = this;
                    frmEmpLeaveEntitlement.Dock = DockStyle.Fill;
                    frmEmpLeaveEntitlement.Show();
                    frmEmpLeaveEntitlement.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbMyLeaveStatementReport01_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Leave Statement Details";
                sptrDashboardContainer.Visible = false;
                frmLeaveStatements frmLeaveStatements = new frmLeaveStatements(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmLeaveStatements.MdiParent = this;
                frmLeaveStatements.Dock = DockStyle.Fill;
                frmLeaveStatements.Show();
                frmLeaveStatements.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbWeeklyOffList_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));

            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Weekly Profile Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmWeeklyProfileMas frmWeeklyProfileMas = new frmWeeklyProfileMas(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmWeeklyProfileMas.MdiParent = this;
                    frmWeeklyProfileMas.Dock = DockStyle.Fill;
                    frmWeeklyProfileMas.Show();
                    frmWeeklyProfileMas.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Weekly Profile Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmWeeklyProfileMas frmWeeklyProfileMas = new frmWeeklyProfileMas(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmWeeklyProfileMas.MdiParent = this;
                        frmWeeklyProfileMas.Dock = DockStyle.Fill;
                        frmWeeklyProfileMas.Show();
                        frmWeeklyProfileMas.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Weekly Profile Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmWeeklyProfileMas frmWeeklyProfileMas = new frmWeeklyProfileMas(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmWeeklyProfileMas.MdiParent = this;
                    frmWeeklyProfileMas.Dock = DockStyle.Fill;
                    frmWeeklyProfileMas.Show();
                    frmWeeklyProfileMas.WindowState = FormWindowState.Maximized;
                }
            }
            //frmWeeklyProfileMaster frmWeeklyProfileMaster = new frmWeeklyProfileMaster();
            //frmWeeklyProfileMaster.Show();
        }

        private void cmbWeeklyOffConfiguration_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));

            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Weekly Profile Details Information";
                    sptrDashboardContainer.Visible = false;
                    frmWeeklyProfileDetailsInfo frmWeeklyProfileDetailsInfo = new frmWeeklyProfileDetailsInfo(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmWeeklyProfileDetailsInfo.MdiParent = this;
                    frmWeeklyProfileDetailsInfo.Dock = DockStyle.Fill;
                    frmWeeklyProfileDetailsInfo.Show();
                    frmWeeklyProfileDetailsInfo.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Weekly Profile Details Information";
                        sptrDashboardContainer.Visible = false;
                        frmWeeklyProfileDetailsInfo frmWeeklyProfileDetailsInfo = new frmWeeklyProfileDetailsInfo(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmWeeklyProfileDetailsInfo.MdiParent = this;
                        frmWeeklyProfileDetailsInfo.Dock = DockStyle.Fill;
                        frmWeeklyProfileDetailsInfo.Show();
                        frmWeeklyProfileDetailsInfo.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Weekly Profile Details Information";
                    sptrDashboardContainer.Visible = false;
                    frmWeeklyProfileDetailsInfo frmWeeklyProfileDetailsInfo = new frmWeeklyProfileDetailsInfo(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmWeeklyProfileDetailsInfo.MdiParent = this;
                    frmWeeklyProfileDetailsInfo.Dock = DockStyle.Fill;
                    frmWeeklyProfileDetailsInfo.Show();
                    frmWeeklyProfileDetailsInfo.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbBulkLeaveApproval_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Bulk Leave Approval Details";
                sptrDashboardContainer.Visible = false;
                frmBulkLeaveApproval frmBulkLeaveApproval = new frmBulkLeaveApproval(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmBulkLeaveApproval.MdiParent = this;
                frmBulkLeaveApproval.Dock = DockStyle.Fill;
                frmBulkLeaveApproval.Show();
                frmBulkLeaveApproval.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbEarningsListConfig_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Payroll Allowance Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmPayrollAllowences frmPayrollAllowences = new frmPayrollAllowences(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmPayrollAllowences.MdiParent = this;
                    frmPayrollAllowences.Dock = DockStyle.Fill;
                    frmPayrollAllowences.Show();
                    frmPayrollAllowences.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Payroll Allowance Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmPayrollAllowences frmPayrollAllowences = new frmPayrollAllowences(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmPayrollAllowences.MdiParent = this;
                        frmPayrollAllowences.Dock = DockStyle.Fill;
                        frmPayrollAllowences.Show();
                        frmPayrollAllowences.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Payroll Allowance Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmPayrollAllowences frmPayrollAllowences = new frmPayrollAllowences(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmPayrollAllowences.MdiParent = this;
                    frmPayrollAllowences.Dock = DockStyle.Fill;
                    frmPayrollAllowences.Show();
                    frmPayrollAllowences.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbReimbursmentListConfig_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Reimbursement Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmReimbursement frmReimbursement = new frmReimbursement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmReimbursement.MdiParent = this;
                    frmReimbursement.Dock = DockStyle.Fill;
                    frmReimbursement.Show();
                    frmReimbursement.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Reimbursement Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmReimbursement frmReimbursement = new frmReimbursement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmReimbursement.MdiParent = this;
                        frmReimbursement.Dock = DockStyle.Fill;
                        frmReimbursement.Show();
                        frmReimbursement.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Reimbursement Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmReimbursement frmReimbursement = new frmReimbursement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmReimbursement.MdiParent = this;
                    frmReimbursement.Dock = DockStyle.Fill;
                    frmReimbursement.Show();
                    frmReimbursement.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbDeductionsListConfig_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString())); 
            
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Deduction Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmPayrollDeductions frmPayrollDeductions = new frmPayrollDeductions(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmPayrollDeductions.MdiParent = this;
                    frmPayrollDeductions.Dock = DockStyle.Fill;
                    frmPayrollDeductions.Show();
                    frmPayrollDeductions.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Deduction Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmPayrollDeductions frmPayrollDeductions = new frmPayrollDeductions(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmPayrollDeductions.MdiParent = this;
                        frmPayrollDeductions.Dock = DockStyle.Fill;
                        frmPayrollDeductions.Show();
                        frmPayrollDeductions.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Deduction Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmPayrollDeductions frmPayrollDeductions = new frmPayrollDeductions(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmPayrollDeductions.MdiParent = this;
                    frmPayrollDeductions.Dock = DockStyle.Fill;
                    frmPayrollDeductions.Show();
                    frmPayrollDeductions.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbPendingLeaveApprovalList_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Pending Approval Details";
                sptrDashboardContainer.Visible = false;
                frmPendingApprovalList frmPendingApprovalList = new frmPendingApprovalList(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmPendingApprovalList.MdiParent = this;
                frmPendingApprovalList.Dock = DockStyle.Fill;
                frmPendingApprovalList.Show();
                frmPendingApprovalList.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbRejectionLeaveList_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Leave Rejection Details";
                sptrDashboardContainer.Visible = false;
                frmLeaveRejectionList frmLeaveRejectionList = new frmLeaveRejectionList();
                frmLeaveRejectionList.MdiParent = this;
                frmLeaveRejectionList.Dock = DockStyle.Fill;
                frmLeaveRejectionList.Show();
                frmLeaveRejectionList.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbConsolidatedLeaveStatement_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Consolidated Leave Statement";
                sptrDashboardContainer.Visible = false;
                frmConsolidatedLeaveStatement frmConsolidatedLeaveStatement = new frmConsolidatedLeaveStatement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmConsolidatedLeaveStatement.MdiParent = this;
                frmConsolidatedLeaveStatement.Dock = DockStyle.Fill;
                frmConsolidatedLeaveStatement.Show();
                frmConsolidatedLeaveStatement.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbOutstandingLeaveStatement_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Outstanding Leave Statement";
                sptrDashboardContainer.Visible = false;
                frmOutstandingLeaveStatement frmOutstandingLeaveStatement = new frmOutstandingLeaveStatement(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmOutstandingLeaveStatement.MdiParent = this;
                frmOutstandingLeaveStatement.Dock = DockStyle.Fill;
                frmOutstandingLeaveStatement.Show();
                frmOutstandingLeaveStatement.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbDailyAttendanceSheet_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Daily Attendance Sheet";
                sptrDashboardContainer.Visible = false;
                frmDailyAttendanceProcess frmDailyAttendanceProcess = new frmDailyAttendanceProcess(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmDailyAttendanceProcess.MdiParent = this;
                frmDailyAttendanceProcess.Dock = DockStyle.Fill;
                frmDailyAttendanceProcess.Show();
                frmDailyAttendanceProcess.WindowState = FormWindowState.Maximized;
            }

            //if (CurrentUser.ClientID == 0)
            //{
            //    MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            //if (this.MdiChildren.Length == 0)
            //{
            //    lblDashboardTitle.Text = "Daily Attendance Sheet";
            //    frmDailyAttendanceSheet frmDailyAttendanceSheet = new frmDailyAttendanceSheet(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
            //    frmDailyAttendanceSheet.MdiParent = this;
            //    frmDailyAttendanceSheet.Dock = DockStyle.Fill;
            //    frmDailyAttendanceSheet.Show();
            //    frmDailyAttendanceSheet.WindowState = FormWindowState.Maximized;
            //}
        }

        private void frmDashboard_ResizeEnd(object sender, EventArgs e)
        {
            //lblDashboardTitle.Width = this.Width;
        }

        private void frmDashboard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                frmActiveCompanyInfo frmActiveCompanyInfo = new frmActiveCompanyInfo(objSelectedClientFinYearInfo);
                frmActiveCompanyInfo.ShowDialog(this);
                objSelectedClientFinYearInfo = frmActiveCompanyInfo.GetSelectedClientAndFinYearDetails();

                objActiveClientInfo = objClientInfo.getClientInfo(objSelectedClientFinYearInfo.ClientID);
                objActiveFinYear = objFinYearInfo.GetSpecificFinYearInfo(objSelectedClientFinYearInfo.FinYearID);

                this.Text = "Staffsync Dashboard - " + objActiveClientInfo.FirstOrDefault().ClientName + " [ FY - " + objActiveFinYear.FirstOrDefault().FinYearFromTo + " ]";
                CurrentUser.ClientID = objActiveClientInfo.FirstOrDefault().ClientID;

                if (Convert.ToBoolean(objAppSettings.GetSpecificAppSettingsInfo("Show Dashboard KPIs").AppSettingValue.ToString()) == true)
                {
                    sptrDashboardContainer.Visible = true;
                    LoadDepartmentColumnChart(objSelectedClientFinYearInfo.ClientID, objSelectedClientFinYearInfo.FinYearID);
                    GetAdvanceSummary(objSelectedClientFinYearInfo.ClientID, objSelectedClientFinYearInfo.FinYearID);
                    GetAdvanceRiskBaseInfo(objSelectedClientFinYearInfo.ClientID, objSelectedClientFinYearInfo.FinYearID);
                    //GetAgingDistributionInfo(objSelectedClientFinYearInfo.ClientID, objSelectedClientFinYearInfo.FinYearID);
                    LoadWorkforceInfo(objSelectedClientFinYearInfo.ClientID, objSelectedClientFinYearInfo.FinYearID);
                    GetUpcomingHolidays(objSelectedClientFinYearInfo.ClientID, objSelectedClientFinYearInfo.FinYearID);
                }
                else
                {
                    sptrDashboardContainer.Visible = false;
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (MessageBox.Show("Are you sure you want to exit.?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                Application.Exit();
            }
        }

        private void cmbBatchDailyAttendanceProcess_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Daily Attendance Sheet";
                sptrDashboardContainer.Visible = false;
                frmDailyAttendanceProcess frmDailyAttendanceProcess = new frmDailyAttendanceProcess(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmDailyAttendanceProcess.MdiParent = this;
                frmDailyAttendanceProcess.Dock = DockStyle.Fill;
                frmDailyAttendanceProcess.Show();
                frmDailyAttendanceProcess.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbPayrollBatch_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Payroll Batch Process";
                sptrDashboardContainer.Visible = false;
                frmPayrollBatchProcess frmPayrollBatchProcess = new frmPayrollBatchProcess(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmPayrollBatchProcess.MdiParent = this;
                frmPayrollBatchProcess.Dock = DockStyle.Fill;
                frmPayrollBatchProcess.Show();
                frmPayrollBatchProcess.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbPublicHolidayConfig_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Public Holiday Configuration";
                sptrDashboardContainer.Visible = false;
                frmPublicHolidayConfig frmPublicHolidayConfig = new frmPublicHolidayConfig(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmPublicHolidayConfig.MdiParent = this;
                frmPublicHolidayConfig.Dock = DockStyle.Fill;
                frmPublicHolidayConfig.Show();
                frmPublicHolidayConfig.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbCountriesList_Click(object sender, EventArgs e)
        {
            AppModuleID = 10;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Country Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmCountryMaster frmCountryMaster = new frmCountryMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmCountryMaster.MdiParent = this;
                    frmCountryMaster.Dock = DockStyle.Fill;
                    frmCountryMaster.Show();
                    frmCountryMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Country Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmCountryMaster frmCountryMaster = new frmCountryMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmCountryMaster.MdiParent = this;
                        frmCountryMaster.Dock = DockStyle.Fill;
                        frmCountryMaster.Show();
                        frmCountryMaster.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Country Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmCountryMaster frmCountryMaster = new frmCountryMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmCountryMaster.MdiParent = this;
                    frmCountryMaster.Dock = DockStyle.Fill;
                    frmCountryMaster.Show();
                    frmCountryMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbSSManageMyDetails_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));

            AppModuleID = 2;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Employee Details";
                    sptrDashboardContainer.Visible = false;
                    frmSSEmployeeMaster frmSSEmployeeMaster = new frmSSEmployeeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmSSEmployeeMaster.MdiParent = this;
                    frmSSEmployeeMaster.Dock = DockStyle.Fill;
                    frmSSEmployeeMaster.Show();
                    frmSSEmployeeMaster.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 2 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 3 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 4 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 5)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Employee Details";
                        sptrDashboardContainer.Visible = false;
                        frmSSEmployeeMaster frmSSEmployeeMaster = new frmSSEmployeeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmSSEmployeeMaster.MdiParent = this;
                        frmSSEmployeeMaster.Dock = DockStyle.Fill;
                        frmSSEmployeeMaster.Show();
                        frmSSEmployeeMaster.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Employee Details";
                    sptrDashboardContainer.Visible = false;
                    frmSSEmployeeMaster frmSSEmployeeMaster = new frmSSEmployeeMaster(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmSSEmployeeMaster.MdiParent = this;
                    frmSSEmployeeMaster.Dock = DockStyle.Fill;
                    frmSSEmployeeMaster.Show();
                    frmSSEmployeeMaster.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbManageCompanyInfo_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));

            AppModuleID = 1;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Company Master Details";
                    sptrDashboardContainer.Visible = false;
                    //frmCompanyInfo frmCompanyInfo = new frmCompanyInfo(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmOrgMasterInfo frmCompanyInfo = new frmOrgMasterInfo(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmCompanyInfo.MdiParent = this;
                    frmCompanyInfo.Dock = DockStyle.Fill;
                    frmCompanyInfo.Show();
                    frmCompanyInfo.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Company Master Details";
                        sptrDashboardContainer.Visible = false;
                        //frmCompanyInfo frmCompanyInfo = new frmCompanyInfo(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmOrgMasterInfo frmCompanyInfo = new frmOrgMasterInfo(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmCompanyInfo.MdiParent = this;
                        frmCompanyInfo.Dock = DockStyle.Fill;
                        frmCompanyInfo.Show();
                        frmCompanyInfo.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Company Master Details";
                    sptrDashboardContainer.Visible = false;
                    //frmCompanyInfo frmCompanyInfo = new frmCompanyInfo(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmOrgMasterInfo frmCompanyInfo = new frmOrgMasterInfo(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmCompanyInfo.MdiParent = this;
                    frmCompanyInfo.Dock = DockStyle.Fill;
                    frmCompanyInfo.Show();
                    frmCompanyInfo.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbManageBranchInfo_Click(object sender, EventArgs e)
        {

            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.EmpID.ToString()));

            AppModuleID = 1;

            if (@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Branch Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmBranchInfo frmBranchInfo = new frmBranchInfo(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmBranchInfo.MdiParent = this;
                    frmBranchInfo.Dock = DockStyle.Fill;
                    frmBranchInfo.Show();
                    frmBranchInfo.WindowState = FormWindowState.Maximized;
                }
            }
            else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.RoleID == 1)
            {
                if (this.MdiChildren.Length == 0)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 1 || objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == AppModuleID)
                    {
                        lblDashboardTitle.Text = "Branch Master Details";
                        sptrDashboardContainer.Visible = false;
                        frmBranchInfo frmBranchInfo = new frmBranchInfo(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                        frmBranchInfo.MdiParent = this;
                        frmBranchInfo.Dock = DockStyle.Fill;
                        frmBranchInfo.Show();
                        frmBranchInfo.WindowState = FormWindowState.Maximized;
                    }
                    else if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID != AppModuleID)
                {
                    if (objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo.ModuleID == 12)
                    {

                    }
                    else
                    {
                        lblDashboardTitle.Text = "Dashboard";
                        MessageBox.Show("Access denied. \nYou are not authorised to access this module.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (this.MdiChildren.Length == 0)
                {
                    lblDashboardTitle.Text = "Branch Master Details";
                    sptrDashboardContainer.Visible = false;
                    frmBranchInfo frmBranchInfo = new frmBranchInfo(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                    frmBranchInfo.MdiParent = this;
                    frmBranchInfo.Dock = DockStyle.Fill;
                    frmBranchInfo.Show();
                    frmBranchInfo.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void cmbBulkLeaveRejection_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Bulk Leave Rejection Details";
                sptrDashboardContainer.Visible = false;
                frmBulkLeaveRejection frmBulkLeaveRejection = new frmBulkLeaveRejection(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmBulkLeaveRejection.MdiParent = this;
                frmBulkLeaveRejection.Dock = DockStyle.Fill;
                frmBulkLeaveRejection.Show();
                frmBulkLeaveRejection.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbApplicationSettings_Opening(object sender, CancelEventArgs e)
        {

        }

        private void cmbAdvanceConfiguration_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Advance Configuration Details";
                sptrDashboardContainer.Visible = false;
                frmAdvanceTypeMas frmAdvanceTypeMas = new frmAdvanceTypeMas(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmAdvanceTypeMas.MdiParent = this;
                frmAdvanceTypeMas.Dock = DockStyle.Fill;
                frmAdvanceTypeMas.Show();
                frmAdvanceTypeMas.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbAdvanceRequest_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Advance Request Details";
                sptrDashboardContainer.Visible = false;
                frmEmpAdvanceRequest frmEmpAdvanceRequest = new frmEmpAdvanceRequest(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmEmpAdvanceRequest.MdiParent = this;
                frmEmpAdvanceRequest.Dock = DockStyle.Fill;
                frmEmpAdvanceRequest.Show();
                frmEmpAdvanceRequest.WindowState = FormWindowState.Maximized;
            }
        }

        private void tlbMyTasks_Click(object sender, EventArgs e)
        {

        }

        private void cmbFirstApproval_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "My Tasks List";
                sptrDashboardContainer.Visible = false;
                frmAdvanceApprovalList frmAdvanceApprovalList = new frmAdvanceApprovalList(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo, "FirstApprover");
                frmAdvanceApprovalList.MdiParent = this;
                frmAdvanceApprovalList.Dock = DockStyle.Fill;
                frmAdvanceApprovalList.Show();
                frmAdvanceApprovalList.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbSecondApproval_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "My Tasks List";
                sptrDashboardContainer.Visible = false;
                frmAdvanceApprovalList frmAdvanceApprovalList = new frmAdvanceApprovalList(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo, "SecondApprover");
                frmAdvanceApprovalList.MdiParent = this;
                frmAdvanceApprovalList.Dock = DockStyle.Fill;
                frmAdvanceApprovalList.Show();
                frmAdvanceApprovalList.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbAdvanceRepayment_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Advance Repayment Details";
                sptrDashboardContainer.Visible = false;
                frmEmpAdvanceRepayment frmEmpAdvanceRepayment = new frmEmpAdvanceRepayment(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmEmpAdvanceRepayment.MdiParent = this;
                frmEmpAdvanceRepayment.Dock = DockStyle.Fill;
                frmEmpAdvanceRepayment.Show();
                frmEmpAdvanceRepayment.WindowState = FormWindowState.Maximized;
            }
        }

        private void cmbManageEmployeePayrollConfiguration_Click(object sender, EventArgs e)
        {
            if (this.MdiChildren.Length == 0)
            {
                lblDashboardTitle.Text = "Payroll Configuration Details";
                sptrDashboardContainer.Visible = false;
                frmPayrollConfiguration frmPayrollConfiguration = new frmPayrollConfiguration(objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, objSelectedClientFinYearInfo);
                frmPayrollConfiguration.MdiParent = this;
                frmPayrollConfiguration.Dock = DockStyle.Fill;
                frmPayrollConfiguration.Show();
                frmPayrollConfiguration.WindowState = FormWindowState.Maximized;
            }
        }

        private void frmDashboard_Activated(object sender, EventArgs e)
        {
            dtgAdvanceRiskBase.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
            dtgUpcomingHolidays.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
        }

        private void circularKpiControl1_Click(object sender, EventArgs e)
        {
            
        }

        private void circularKpiControl1_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void dtgAdvanceRiskBase_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dtgAdvanceRiskBase.Columns[e.ColumnIndex].Name == "EmpAdvReqCode")
            {
                int SelectedEmpID = Convert.ToInt32(dtgAdvanceRiskBase.Rows[e.RowIndex].Cells["EmpID"].Value);
                int AdvanceRequestID = Convert.ToInt32(dtgAdvanceRiskBase.Rows[e.RowIndex].Cells["EmpAdvanceRequestID"].Value);

                frmEmpAdvanceTRList frmEmpAdvanceTRList = new frmEmpAdvanceTRList(this, "dashboardempadvancestatement", Convert.ToInt32(SelectedEmpID.ToString()), Convert.ToInt32(AdvanceRequestID.ToString()));
                frmEmpAdvanceTRList.ShowDialog(this);
            }
        }

        public void SelectedEmployeeID(string SearchOptionSelectedForm, int selectedEmployeeID, int selectedMonthSalaryID)
        {
            if (SearchOptionSelectedForm == "dashboardempadvancestatement")
            {

            }
        }

        private void kryptonGroupBox5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoggedInUser_Click(object sender, EventArgs e)
        {

        }

        private void LogInTime_Click(object sender, EventArgs e)
        {

        }

        private void LastActionByLoggedInUser_Click(object sender, EventArgs e)
        {

        }

        private void myStatusBar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tblRibbon_SelectedTabChanged(object sender, EventArgs e)
        {

        }

        private void kryptonRibbonGroup5_DialogBoxLauncherClick(object sender, EventArgs e)
        {

        }

        private void tlbMyOptions_Click(object sender, EventArgs e)
        {

        }

        private void cmMyOptions_Opening(object sender, CancelEventArgs e)
        {

        }

        private void cmbSSEmployeeManagement_Click(object sender, EventArgs e)
        {

        }

        private void cmbSSReports_Click(object sender, EventArgs e)
        {

        }

        private void cmbSSEmployeeDetails_Click(object sender, EventArgs e)
        {

        }

        private void cmbSSEmployeeFamilyDetails_Click(object sender, EventArgs e)
        {

        }

        private void cmbSSEmployeePreviousExperienceDetails_Click(object sender, EventArgs e)
        {

        }

        private void cmbSSEmployeeSkillsDetails_Click(object sender, EventArgs e)
        {

        }

        private void cmbSSEmployeeQualificationDetails_Click(object sender, EventArgs e)
        {

        }

        private void cmbSSEmployeeOrganisationDetails_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {

        }

        private void cmbSSAttendanceManagement_Click(object sender, EventArgs e)
        {

        }

        private void cmbSSMyAttendanceManagement_Click(object sender, EventArgs e)
        {

        }

        private void cmbSSMyAttendanceReports_Click(object sender, EventArgs e)
        {

        }

        private void cmbSSMyAttendanceReportsMonthly_Click(object sender, EventArgs e)
        {

        }

        private void cmbSSMyAttendanceReportsCustom_Click(object sender, EventArgs e)
        {

        }

        private void cmbSSMyAttendanceLeaveStatement_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {

        }

        private void cmbSSLeaveManagement_Click(object sender, EventArgs e)
        {

        }

        private void cmbSSLeaveRequest_Click(object sender, EventArgs e)
        {

        }

        private void cmbSSLeaveReports_Click(object sender, EventArgs e)
        {

        }

        private void leaveReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {

        }

        private void kryptonRibbonGroup2_DialogBoxLauncherClick(object sender, EventArgs e)
        {

        }

        private void tlbManageRecruitmentButton_Click(object sender, EventArgs e)
        {

        }

        private void kryptonRibbonGroup1_DialogBoxLauncherClick(object sender, EventArgs e)
        {

        }

        private void cmManageEmployee_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem29_Click(object sender, EventArgs e)
        {

        }

        private void cmAttendanceManagementList_Opening(object sender, CancelEventArgs e)
        {

        }

        private void cmbManageEmployeeAttendance_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {

        }

        private void tlbManageEmployeeLeavesButton_Click(object sender, EventArgs e)
        {

        }

        private void cmLeaveManagement_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripSeparator4_Click(object sender, EventArgs e)
        {

        }

        private void cmbLeaveRequest_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {

        }

        private void cmbLeaveApproval_Click(object sender, EventArgs e)
        {

        }

        private void cmbLeaveReject_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {

        }

        private void cmbMyLeaveReports_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {

        }

        private void tlbManageEmployeeAdvancesButton_Click(object sender, EventArgs e)
        {

        }

        private void cmbManageAdvances_Opening(object sender, CancelEventArgs e)
        {

        }

        private void cmbAdvanceManagement_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem25_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem20_Click(object sender, EventArgs e)
        {

        }

        private void cmbAdvanceManagementReports_Click(object sender, EventArgs e)
        {

        }

        private void employeeAdvanceOutstandingSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem24_Click(object sender, EventArgs e)
        {

        }

        private void advanceRecoveryStatementEmployeewiseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem26_Click(object sender, EventArgs e)
        {

        }

        private void monthlyAdvanceRecoveryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem27_Click(object sender, EventArgs e)
        {

        }

        private void advanceDeductionThroughPayrollReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem28_Click(object sender, EventArgs e)
        {

        }

        private void advanceRepaymentHistoryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void kryptonRibbonGroup6_DialogBoxLauncherClick(object sender, EventArgs e)
        {

        }

        private void tlbManagePayrollManagement_Click(object sender, EventArgs e)
        {

        }

        private void cmPayrollManagement_Opening(object sender, CancelEventArgs e)
        {

        }

        private void cmbPayrollSettings_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSeparator2_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {

        }

        private void cmbPayrollReports_Click(object sender, EventArgs e)
        {

        }

        private void cmbPayslipRegister_Click(object sender, EventArgs e)
        {

        }

        private void cmbSalaryStatement_Click(object sender, EventArgs e)
        {

        }

        private void cmbPayrollSummaryReport_Click(object sender, EventArgs e)
        {

        }

        private void cmbEarningsDeductions_Click(object sender, EventArgs e)
        {

        }

        private void kryptonRibbonGroup7_DialogBoxLauncherClick(object sender, EventArgs e)
        {

        }

        private void tlbUserManagement_Click(object sender, EventArgs e)
        {

        }

        private void cmUserManagementList_Opening(object sender, CancelEventArgs e)
        {

        }

        private void tlbRoleAssignment_Click(object sender, EventArgs e)
        {

        }

        private void tlbModuleAssignment_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSeparator1_Click(object sender, EventArgs e)
        {

        }

        private void cmbRoleProfilement_Click(object sender, EventArgs e)
        {

        }

        private void kryptonRibbonGroup3_DialogBoxLauncherClick(object sender, EventArgs e)
        {

        }

        private void cmCompanyInfo_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripSeparator3_Click(object sender, EventArgs e)
        {

        }

        private void kryptonRibbonGroup4_DialogBoxLauncherClick(object sender, EventArgs e)
        {

        }

        private void tlbApplicationSettings_Click(object sender, EventArgs e)
        {

        }

        private void cmbMasterList_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem4_Click_1(object sender, EventArgs e)
        {

        }

        private void cmbSalaryConfiguration_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void cmbLeaveConfiguration_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {

        }

        private void payrollConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {

        }

        private void advanceConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {

        }

        private void tlbApplicationGroup_DialogBoxLauncherClick(object sender, EventArgs e)
        {

        }

        private void cmbApprovals_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {

        }

        private void toolStripSeparator5_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem23_Click(object sender, EventArgs e)
        {

        }

        private void tlbApplicationHelp_Click(object sender, EventArgs e)
        {

        }

        private void tlbManageEmployeePayroll_Click(object sender, EventArgs e)
        {

        }

        private void kryptonRibbonGroupButton14_Click(object sender, EventArgs e)
        {

        }

        private void kryptonRibbonGroupButton4_Click(object sender, EventArgs e)
        {

        }

        private void kryptonRibbonGroupButton7_Click(object sender, EventArgs e)
        {

        }

        private void kryptonContextMenuItem2_Click(object sender, EventArgs e)
        {

        }

        private void kryptonContextMenuItem3_Click(object sender, EventArgs e)
        {

        }

        private void kryptonContextMenuItem4_Click(object sender, EventArgs e)
        {

        }

        private void kryptonContextMenuItem6_Click(object sender, EventArgs e)
        {

        }

        private void lblDashboardTitle_Click(object sender, EventArgs e)
        {

        }

        private void sptrDashboardContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void kryptonSplitContainer4_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void kryptonGroupBox1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chrtCompanySummaryMatrix_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void kryptonGroupBox4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chrtAgingDistribution_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void kryptonSplitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void kryptonSplitContainer5_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void kryptonGroupBox2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chrtCompanyAdvanceSummaryMatrix_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void kryptonSplitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void kryptonGroupBox3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtgAdvanceRiskBase_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void kryptonSplitContainer3_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void kryptonPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bdgLeaveEmployees_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void bdgPresentEmployees_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bdgActiveEmployees_Load(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void kryptonLabel1_Click(object sender, EventArgs e)
        {

        }

        private void kryptonSplitContainer6_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void kryptonLabel2_Click(object sender, EventArgs e)
        {

        }

        private void qryRoleProfileBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void kryptonRibbonGroupButton5_Click(object sender, EventArgs e)
        {

        }

        private void bdgTotalLeaveApprovals_DoubleClick(object sender, EventArgs e)
        {
            if (bdgTotalLeaveApprovals.Value > 0)
            {
                frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "DashboardEmployeeLeaveApprovalPendingRequestList", objSelectedClientFinYearInfo.ClientID, objSelectedClientFinYearInfo.FinYearID, DateTime.Now.Date);
                frmEmployeeList.ShowDialog();
            }
            else
            {
                MessageBox.Show("There are no pending requests for leave approval.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bdgTotalLeaveEmployees_DoubleClick(object sender, EventArgs e)
        {
            if (bdgTotalLeaveEmployees.Value > 0)
            {
                frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "DashboardEmployeeLeaveApprovedList", objSelectedClientFinYearInfo.ClientID, objSelectedClientFinYearInfo.FinYearID, DateTime.Now.Date);
                frmEmployeeList.ShowDialog();
            }
            else
            {
                MessageBox.Show("Today, there are no employees on leave.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void kryptonGroupBox4_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void dtgUpcomingHolidays_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void kryptonPictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void kryptonPictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void kryptonPictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void kryptonPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label9_Click_1(object sender, EventArgs e)
        {

        }

        private void bdgTotalLeaveApprovals_Load(object sender, EventArgs e)
        {

        }

        private void bdgTotalLeaveEmployees_Load(object sender, EventArgs e)
        {

        }

        private void bdgTotalPresentEmployees_Load(object sender, EventArgs e)
        {

        }

        private void bdgTotalActiveEmployees_Load(object sender, EventArgs e)
        {

        }

        private void dtgUpcomingHolidays_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dtgUpcomingHolidays.Columns[e.ColumnIndex].Name == "DaysRemaining" && e.Value != null)
            {
                int days = Convert.ToInt32(e.Value);

                if (days == 0)
                {
                    e.Value = "Today";
                    //dtgUpcomingHolidays.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                    //dtgUpcomingHolidays.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Yellow;
                }
                else if (days <= 3)
                {
                    //dtgUpcomingHolidays.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.OrangeRed;
                }
                else if (days <= 7)
                {
                    //dtgUpcomingHolidays.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.DarkOrange;
                }
                e.Value = $"{days} Days";

                e.FormattingApplied = true;
            }
        }

        private void dtgUpcomingHolidays_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //if (e.RowIndex < 0)
            //    return;

            //if(e.ColumnIndex < 0)
            //    return;

            //if (dtgUpcomingHolidays.Columns[e.ColumnIndex].Name == "DaysRemaining")
            //{
            //    e.Handled = true;
            //    e.PaintBackground(e.CellBounds, true);

            //    int days = Convert.ToInt32(e.Value);

            //    string text;
            //    Color backColor;

            //    // 🎨 Badge Logic
            //    if (days == 0)
            //    {
            //        text = "Today";
            //        backColor = Color.Red;
            //    }
            //    else if (days == 1)
            //    {
            //        text = "Tomorrow";
            //        backColor = Color.OrangeRed;
            //    }
            //    else if (days <= 7)
            //    {
            //        text = $"{days}";
            //        backColor = Color.DarkOrange;
            //    }
            //    else if (days <= 30)
            //    {
            //        text = $"{days}";
            //        backColor = Color.Goldenrod;
            //    }
            //    else
            //    {
            //        text = $"{days}";
            //        backColor = Color.SeaGreen;
            //    }

            //    // 🔵 Draw Rounded Badge
            //    using (SolidBrush brush = new SolidBrush(backColor))
            //    using (StringFormat sf = new StringFormat()
            //    {
            //        Alignment = StringAlignment.Center,
            //        LineAlignment = StringAlignment.Center
            //    })
            //    {
            //        Rectangle badgeRect = new Rectangle(
            //            e.CellBounds.X + 10,
            //            e.CellBounds.Y + 5,
            //            e.CellBounds.Width - 20,
            //            e.CellBounds.Height - 10
            //        );

            //        Graphics g = e.Graphics;
            //        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //        System.Drawing.Drawing2D.GraphicsPath path =
            //            GetRoundedRectangle(badgeRect, 15);

            //        g.FillPath(brush, path);

            //        g.DrawString(text, new Font("Segoe UI", 9, FontStyle.Bold), System.Drawing.Brushes.White, badgeRect,sf);
            //    }
            //}
        }

        private System.Drawing.Drawing2D.GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            int diameter = radius * 2;
            var path = new System.Drawing.Drawing2D.GraphicsPath();

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);

            path.CloseFigure();
            return path;
        }

        private void bdgTotalPresentEmployees_DoubleClick(object sender, EventArgs e)
        {
            if (bdgTotalLeaveEmployees.Value > 0)
            {
                frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "DashboardEmployeeLeaveApprovedList", objSelectedClientFinYearInfo.ClientID, objSelectedClientFinYearInfo.FinYearID, DateTime.Now.Date);
                frmEmployeeList.ShowDialog();
            }
            else
            {
                MessageBox.Show("Today, there are no employees on leave.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bdgEmployeesWithWeeklyOff_DoubleClick(object sender, EventArgs e)
        {
            if (bdgEmployeesWithWeeklyOff.Value > 0)
            {
                frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "DashboardEmployeesWeeklyOffList", objSelectedClientFinYearInfo.ClientID, objSelectedClientFinYearInfo.FinYearID, DateTime.Now.Date);
                frmEmployeeList.ShowDialog();
            }
            else
            {
                MessageBox.Show("Today, there are no employees on leave.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bdgEmployeesBirthday_DoubleClick(object sender, EventArgs e)
        {
            if (bdgEmployeesBirthday.Value > 0)
            {
                frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "DashboardEmployeesBirthdayList", objSelectedClientFinYearInfo.ClientID, objSelectedClientFinYearInfo.FinYearID, DateTime.Now.Date);
                frmEmployeeList.ShowDialog();
            }
            else
            {
                MessageBox.Show("Today, there are no employees on leave.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bdgWorkAnniversary_DoubleClick(object sender, EventArgs e)
        {
            if (bdgWorkAnniversary.Value > 0)
            {
                frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "DashboardEmployeesWorkAnniversaryList", objSelectedClientFinYearInfo.ClientID, objSelectedClientFinYearInfo.FinYearID, DateTime.Now.Date);
                frmEmployeeList.ShowDialog();
            }
            else
            {
                MessageBox.Show("Today, there are no employees on leave.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void bdgEmployeesBirthday_Load(object sender, EventArgs e)
        {

        }
    }
}
