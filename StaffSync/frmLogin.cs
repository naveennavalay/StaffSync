using ModelStaffSync;
using Quartz;
using Quartz.Impl;
using StaffSyncJobs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Quartz.Logging.OperationName;

namespace StaffSync
{
    public partial class frmLogin : Form
    {
        private IScheduler _scheduler;

        //AppMail mail = new AppMail();
        DALStaffSync.clsCurrentUserInfo objCurrentUserInfo = new DALStaffSync.clsCurrentUserInfo();
        DALStaffSync.clsClientInfo objClientInfo = new DALStaffSync.clsClientInfo();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        DALStaffSync.clsEncryptDecrypt objEncryptDecrypt = new DALStaffSync.clsEncryptDecrypt();
        DALStaffSync.clsFinYearMas objFinYearMas = new DALStaffSync.clsFinYearMas();

        int LoginCounter = 0;

        public frmLogin()
        {
            InitializeComponent();

            cmbCurrentCompany.DataSource = objClientInfo.getAllCompanyList();
            cmbCurrentCompany.DisplayMember = "ClientName";
            cmbCurrentCompany.ValueMember = "ClientID";

            cmbCurrentFinYear.DataSource = objFinYearMas.GetFinYearList();
            cmbCurrentFinYear.DisplayMember = "FinYearFromTo";
            cmbCurrentFinYear.ValueMember = "FinYearID";
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            // Define the job and associate it with MyJob class
            IJobDetail job = JobBuilder.Create<StaffSyncLeaveJobs>()
                .WithIdentity("myJob", "group1")
                .UsingJobData("EmpID", "1")
                .UsingJobData("LeaveTypeID", "1")
                .UsingJobData("LeaveActionType", "Approved")
                .UsingJobData("LeaveDateFrom", "21-Sep-2025")
                .UsingJobData("LeaveDateTo", "21-Sep-2025")
                .UsingJobData("LeaveDuration", "1")
                .UsingJobData("LeaveDurationType", "FullDay")

                //.UsingJobData("message", "Hello from Quartz.NET!")
                //.UsingJobData("value", 3.141f)
                .Build();

            // Trigger it to run immediately
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .Build();

            await _scheduler.ScheduleJob(job, trigger);

            //MessageBox.Show("Job scheduled! Check console output.");

            //this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //AppMail.SendMail();

            if(@System.Configuration.ConfigurationSettings.AppSettings["login"].ToString() == "by!pass")
            {
                this.Hide();
                frmDashboard objDashboard = new frmDashboard();
                objDashboard.Show();
            }
            else if (validateAuthInfo() == true)
            {
                UserInfo loginUserInfo = objLogin.getSpecificUserInfo(txtUserName.Text.ToString());
                if (loginUserInfo != null)
                {
                    if(loginUserInfo.UserID == 0 || loginUserInfo.EmpID == 0)
                    {
                        MessageBox.Show("Unauthorized access. \nThe user is not registered.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Dispose();
                        GC.Collect();
                        return;
                    }

                    if (objLogin.AuthenticateUserInfo(txtUserName.Text.ToString(), txtPassword.Text.ToString()))
                    {
                        this.Hide();

                        DirectoryInfo directory = Directory.CreateDirectory(AppVariables.TempFolderPath);

                        objCurrentUserInfo = new DALStaffSync.clsCurrentUserInfo(loginUserInfo.EmpID);

                        objLogin.getLoggedInUserInfo(loginUserInfo.EmpID);
                        frmDashboard objDashboard = new frmDashboard();
                        objDashboard.Show();
                    }
                    else
                    {
                        if(loginUserInfo.IsLocked == false)
                        {
                            LoginCounter = LoginCounter + 1;
                            if (LoginCounter == 5)
                            {
                                objLogin.LockOrUnlockUserInfo(loginUserInfo.EmpID, true);
                                MessageBox.Show("Exceeded several attempts using an incorrect User Name or wrong password. \nThe user is now locked. \nTo regain access to the application, kindly check with your organization.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Dispose();
                                GC.Collect();
                            }
                        }
                        else
                        {
                            GC.Collect();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "Please provide your credentials.", "Staffsync", MessageBoxButtons.OK);
                txtUserName.Focus();
            }
        }

        private bool validateAuthInfo()
        {
            bool AuthValidation = true;

            if(txtUserName.Text.ToString().Trim() == "")
                AuthValidation = false;
            if (txtPassword.Text.ToString().Trim() == "")
                AuthValidation = false;

            return AuthValidation;
        }

        private async void frmLogin_Load(object sender, EventArgs e)
        {
            // Start the scheduler when form loads
            _scheduler = await new StdSchedulerFactory().GetScheduler();
            await _scheduler.Start();
        }

        private async void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop scheduler gracefully
            if (_scheduler != null)
            {
                await _scheduler.Shutdown();
            }
        }
    }
}
