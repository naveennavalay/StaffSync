using DocumentFormat.OpenXml.Wordprocessing;
using ModelStaffSync;
using StaffSync.StaffsyncDBDataSetTableAdapters;
using StaffSync.StaffsyncDBDTSetTableAdapters;
using StaffSyncJobs.Scheduler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StaffSync
{
    public partial class frmSchedulerDashboard : Form
    {
        //clsCountries objCountries = new clsCountries();
        //clsDesignation objDesignation = new clsDesignation();
        //clsStates objState = new clsStates();
        //clsRelationship objRelationship = new clsRelationship();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        DALStaffSync.SchedulerRepository objSchedulerRepository = new DALStaffSync.SchedulerRepository();
        DALStaffSync.clsJobAuditLog objJobAuditLog = new DALStaffSync.clsJobAuditLog();
        DALStaffSync.clsSkillsMas objSkills = new DALStaffSync.clsSkillsMas();
        frmDashboard objDashboard = (frmDashboard) System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();
        //DALStaffSync.clsAuditLog objAuditLog = new DALStaffSync.clsAuditLog();

        string strActionStatement = "";
        private Dictionary<string, object> _originalValues;

        public frmSchedulerDashboard()
        {
            InitializeComponent();
        }

        public frmSchedulerDashboard(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmSchedulerDashboard(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            //if (lblActionMode.Text != "")
            //{
            //    if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //    {
            //        return;
            //    }
            //}
            //objDashboard.lblDashboardTitle.Text = "Dashboard";
            //objDashboard.sptrDashboardContainer.Visible = true;
            //this.Close();
        }

        private void frmSchedulerDashboard_Load(object sender, EventArgs e)
        {
            FocusManager.EnableHighlighting = false;
            FocusManager.ShowNavigationError = true;
            FocusManager.Register(this);
            //FocusManager.SetFocus(btnGenerateDetails);

            //// TODO: This line of code loads data into the 'staffsyncDBDTSet.EmpMasInfo' table. You can move, or remove it, as needed.
            //this.empMasInfoTableAdapter.Fill(this.staffsyncDBDTSet.EmpMasInfo);
            onCancelButtonClick();
            disableControls();
            clearControls();
            loadSchedulerJobDetailsOnUI();
            loadScheduledJobHistory();

            tmrUIRefresher.Enabled = true;
            tmrUIRefresher.Start();
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
            //frmSkillsList frmSkillsList = new frmSkillsList(this);
            //frmSkillsList.ShowDialog(this);
        }

        private void btnCloseMe_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenerateDetails_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            
        }

        private string FormatRemainingTime(TimeSpan ts)
        {

            return string.Format("{0:00}:{1:00}:{2:00}:{3:00}", ts.Days, ts.Hours, ts.Minutes, ts.Seconds);

            //if (ts.TotalDays >= 1)
            //    return $"{ts.Days:00}d {ts.Hours:00}h {ts.Minutes:00}m";

            //if (ts.TotalHours >= 1)
            //    return $"{ts.Hours:00}h {ts.Minutes:00}m {ts.Seconds:00}s";

            //if (ts.TotalMinutes >= 1)
            //    return $"{ts.Minutes:00}m {ts.Seconds:00}s";

            //return $"{ts.Seconds:00}s";
        }

        private void UpdateRemainingTime()
        {
            foreach (DataGridViewRow row in dtgAllScheduledJobsList.Rows)
            {
                if (row.IsNewRow)
                    continue;

                if (row.Cells["NextRun"].Value == null)
                    continue;

                if (string.IsNullOrWhiteSpace(row.Cells["NextRun"].Value.ToString()))
                    continue;

                //DateTime nextRun;

                //if (!DateTime.TryParse(row.Cells["NextRun"].Value.ToString(), out nextRun))
                //    continue;

                DateTime nextRun = (DateTime)row.Cells["NextRun"].Value;

                DateTime now = DateTime.Now;

                TimeSpan remaining = nextRun.Subtract(now);

                Console.WriteLine($"Seconds  : {remaining.TotalSeconds}");

                if (remaining.TotalSeconds <= 0)
                {
                    row.Cells["TimeLeft"].Value = "Running...";
                }
                else
                {
                    row.Cells["TimeLeft"].Value = FormatRemainingTime(remaining);
                }
                //row.Cells["TimeLeft"].Value = FormatRemainingTime(remaining);
            }
        }

        private void RefreshSchedulerSummary()
        {
            int totalJobs = 0;
            int running = 0;
            int success = 0;
            int failed = 0;
            int pending = 0;
            int stopped = 0;
            int paused = 0;

            foreach (DataGridViewRow row in dtgAllScheduledJobsList.Rows)
            {
                if (row.IsNewRow)
                    continue;

                row.Cells["JobName"].ToolTipText = row.Cells["Description"].Value.ToString();

                totalJobs++;
                bool isEnabled = Convert.ToBoolean(row.Cells["IsEnabled"].Value);

                if (!isEnabled)
                {
                    stopped++;
                    continue;   // Disabled jobs cannot be running
                }

                string status = Convert.ToString(row.Cells["LastStatus"].Value)?.Trim().ToUpper();

                switch (status)
                {
                    case "RUNNING":
                        running++;
                        break;

                    case "SUCCESS":
                        success++;
                        break;

                    case "FAILED":
                        failed++;
                        break;

                    case "PENDING":
                        pending++;
                        break;

                    case "PAUSED":
                        paused++;
                        break;

                    case "STOPPED":
                        stopped++;
                        break;
                }
            }

            lblAllConfiguredJobsCount.Text = totalJobs.ToString();
            lblCurrentlyRunningJobsCount.Text = running.ToString();
            lblCurrentlyStoppedJobsCount.Text = stopped.ToString();
            lblStoppedJobsCount.Text = stopped.ToString();
            lblCurrentlySuccessfulJobsCount.Text = success.ToString();

            //lblSuccessJobs.Text = success.ToString();
            //lblFailedJobs.Text = failed.ToString();
            //lblPendingJobs.Text = pending.ToString();
            //lblPausedJobs.Text = paused.ToString();
        }

        private void loadScheduledJobHistory()
        {
            dtgScheduledJobsExecutionHistory.DataSource = objJobAuditLog.getClientSpecificSchedulerJobHistoryStatements(objTempClientFinYearInfo.ClientID);
            dtgScheduledJobsExecutionHistory.Columns["SchedulerJobHistoryID"].Visible = false;
            dtgScheduledJobsExecutionHistory.Columns["SchedulerJobHistoryID"].ReadOnly = true;
            dtgScheduledJobsExecutionHistory.Columns["JobSchedulerSettingsID"].Visible = false;
            dtgScheduledJobsExecutionHistory.Columns["JobSchedulerSettingsID"].ReadOnly = true;
            dtgScheduledJobsExecutionHistory.Columns["StartTime"].ReadOnly = true;
            dtgScheduledJobsExecutionHistory.Columns["StartTime"].Width = 150;
            dtgScheduledJobsExecutionHistory.Columns["StartTime"].DefaultCellStyle.Format = "dd-MMM-yyyy hh:mm:ss tt";
            dtgScheduledJobsExecutionHistory.Columns["EndTime"].ReadOnly = true;
            dtgScheduledJobsExecutionHistory.Columns["EndTime"].Width = 150;
            dtgScheduledJobsExecutionHistory.Columns["EndTime"].DefaultCellStyle.Format = "dd-MMM-yyyy hh:mm:ss tt";
            dtgScheduledJobsExecutionHistory.Columns["DurationSeconds"].ReadOnly = true;
            dtgScheduledJobsExecutionHistory.Columns["DurationSeconds"].Width = 125;
            dtgScheduledJobsExecutionHistory.Columns["DurationSeconds"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgScheduledJobsExecutionHistory.Columns["Status"].ReadOnly = true;
            dtgScheduledJobsExecutionHistory.Columns["Status"].Width = 250;
            dtgScheduledJobsExecutionHistory.Columns["Message"].ReadOnly = true;
            dtgScheduledJobsExecutionHistory.Columns["Message"].Width = 300;
            dtgScheduledJobsExecutionHistory.Columns["TriggeredBy"].ReadOnly = true;
            dtgScheduledJobsExecutionHistory.Columns["Message"].Width = 300;
            dtgScheduledJobsExecutionHistory.Columns["ExecutionDate"].ReadOnly = true;
            dtgScheduledJobsExecutionHistory.Columns["Message"].Width = 600;
            dtgScheduledJobsExecutionHistory.Columns["ExecutionDate"].DefaultCellStyle.Format = "dd-MMM-yyyy hh:mm:ss tt";
            dtgScheduledJobsExecutionHistory.Columns["ExecutionDate"].Width = 150;
            dtgScheduledJobsExecutionHistory.Columns["ClientID"].Visible = false;
            dtgScheduledJobsExecutionHistory.Columns["ClientID"].ReadOnly = true;
        }

        private void loadSchedulerJobDetailsOnUI()
        {
            dtgAllScheduledJobsList.DataSource = objSchedulerRepository.GetAllJobsList();

            dtgAllScheduledJobsList.Columns["JobID"].Visible = false;
            dtgAllScheduledJobsList.Columns["JobID"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["JobCode"].Width = 175;
            dtgAllScheduledJobsList.Columns["JobCode"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["JobCode"].Visible = false;
            dtgAllScheduledJobsList.Columns["JobName"].Width = 175;
            dtgAllScheduledJobsList.Columns["JobName"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["Description"].Width = 250;
            dtgAllScheduledJobsList.Columns["Description"].Visible = false;
            dtgAllScheduledJobsList.Columns["Description"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["IsSystemJob"].Width = 75;
            dtgAllScheduledJobsList.Columns["IsSystemJob"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["IsActive"].Width = 70;
            dtgAllScheduledJobsList.Columns["IsActive"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["IsEnabled"].Width = 70;
            dtgAllScheduledJobsList.Columns["IsEnabled"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["ScheduleType"].Width = 100;
            dtgAllScheduledJobsList.Columns["ScheduleType"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["StartDate"].Width = 75;
            dtgAllScheduledJobsList.Columns["StartDate"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["StartDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgAllScheduledJobsList.Columns["RunTime"].Width = 85;
            dtgAllScheduledJobsList.Columns["RunTime"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["RunTime"].DefaultCellStyle.Format = "hh:mm:ss tt";
            dtgAllScheduledJobsList.Columns["EndDate"].Width = 75;
            dtgAllScheduledJobsList.Columns["EndDate"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["EndDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgAllScheduledJobsList.Columns["IntervalValue"].Width = 70;
            dtgAllScheduledJobsList.Columns["IntervalValue"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["IntervalValue"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgAllScheduledJobsList.Columns["IntervalType"].Width = 80;
            dtgAllScheduledJobsList.Columns["IntervalType"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["RepeatForever"].Width = 70;
            dtgAllScheduledJobsList.Columns["RepeatForever"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["DayOfWeek"].Visible = false;
            dtgAllScheduledJobsList.Columns["DayOfWeek"].Width = 125;
            dtgAllScheduledJobsList.Columns["DayOfWeek"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["WeeklyDayName"].Width = 120;
            dtgAllScheduledJobsList.Columns["WeeklyDayName"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["DayOfMonth"].Visible = false;
            dtgAllScheduledJobsList.Columns["DayOfMonth"].Width = 125;
            dtgAllScheduledJobsList.Columns["DayOfMonth"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["MonthlyDayName"].Width = 120;
            dtgAllScheduledJobsList.Columns["MonthlyDayName"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["LastRun"].Width = 150;
            dtgAllScheduledJobsList.Columns["LastRun"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["LastRun"].DefaultCellStyle.Format = "dd-MMM-yyyy hh:mm:ss tt";
            dtgAllScheduledJobsList.Columns["LastStatus"].Width = 75;
            dtgAllScheduledJobsList.Columns["LastStatus"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["NextRun"].Width = 150;
            dtgAllScheduledJobsList.Columns["NextRun"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["NextRun"].DefaultCellStyle.Format = "dd-MMM-yyyy hh:mm:ss tt";

            dtgAllScheduledJobsList.Columns["TimeLeft"].Width = 80;
            dtgAllScheduledJobsList.Columns["TimeLeft"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["TimeLeft"].DefaultCellStyle.Format = "dd-MMM-yyyy hh:mm:ss tt";

            dtgAllScheduledJobsList.Columns["ClassName"].Visible = false;
            dtgAllScheduledJobsList.Columns["ClassName"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["JobSchedulerSettingsID"].Visible = false;
            dtgAllScheduledJobsList.Columns["JobSchedulerSettingsID"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["CronExpression"].Visible = false;
            dtgAllScheduledJobsList.Columns["CronExpression"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["RetryCount"].Visible = false;
            dtgAllScheduledJobsList.Columns["RetryCount"].ReadOnly = true;
            dtgAllScheduledJobsList.Columns["ClientID"].Visible = false;
            dtgAllScheduledJobsList.Columns["ClientID"].ReadOnly = true;

            RefreshSchedulerSummary();
        }

        public void clearControls()
        {
            lblAllConfiguredJobsCount.Text = "0";
            lblCurrentlyPausedJobsCount.Text = "0";
            lblCurrentlyRunningJobsCount.Text = "0";
            lblCurrentlyStoppedJobsCount.Text = "0";
            lblStoppedJobsCount.Text = "0";
            lblCurrentlySuccessfulJobsCount.Text = "0";
        }

        public void enableControls()
        {

        }

        public void disableControls()
        {

        }

        public void onGenerateButtonClick()
        {

        }

        public void onModifyButtonClick()
        {

        }

        public void onRemoveButtonClick()
        {

        }

        public void onSaveButtonClick()
        {

        }

        public void onCancelButtonClick()
        {

        }

        public void displaySelectedValuesOnUI(SkillModel skillsModel)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnModifyDetails_Click(object sender, EventArgs e)
        {

        }

        private void btnRemoveDetails_Click(object sender, EventArgs e)
        {

        }

        private void frmSchedulerDashboard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                //if (lblActionMode.Text != "")
                //{
                //    if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                //    {
                //        return;
                //    }
                //}
                objDashboard.lblDashboardTitle.Text = "Dashboard";
                objDashboard.sptrDashboardContainer.Visible = true;
                this.Close();
            }
        }

        private void lnkViewAuditLog_LinkClicked(object sender, EventArgs e)
        {
            //frmAuditLogStatements objAuditLogStatements = new frmAuditLogStatements(Convert.ToInt32(lblCountryID.Text.ToString()), "SkillsMasterInfo", "Skills Master Information", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
            //objAuditLogStatements.ShowDialog(this);
        }

        private void tmrUIRefresher_Tick(object sender, EventArgs e)
        {
            loadSchedulerJobDetailsOnUI();
            loadScheduledJobHistory();
        }

        private void dtgAllScheduledJobsList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            
            DataGridView dgv = sender as DataGridView;

            if (Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["JobID"].Value.ToString()) > 0)
            {
                SchedulerJobModel objSelectedSchedulerJobModel = objSchedulerRepository.GetScheduledJobByID(Convert.ToInt32(dgv.Rows[e.RowIndex].Cells["JobID"].Value.ToString()));
                using (frmSchedulerJobDetails frmSchedulerJobDetails = new frmSchedulerJobDetails(objTempCurrentlyLoggedInUserInfo, objTempClientFinYearInfo, objSelectedSchedulerJobModel))
                {
                    if (frmSchedulerJobDetails.ShowDialog() == DialogResult.OK)
                    {
                        loadSchedulerJobDetailsOnUI();
                        loadScheduledJobHistory();
                    }
                }
            }
        }

        private async void btnStopAll_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                btnRunAll.Enabled = false;
                btnStopAll.Enabled = false;
                btnRefresh.Enabled = false;

                foreach (DataGridViewRow row in dtgAllScheduledJobsList.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    bool enabled = Convert.ToBoolean(row.Cells["IsEnabled"].Value);

                    if (!enabled)
                        continue;

                    string jobCode = row.Cells["JobCode"].Value.ToString();

                    await SchedulerManager.StopJob(jobCode);

                    objSchedulerRepository.UpdateLastStatus(Convert.ToInt32(row.Cells["JobID"].Value), "STOPPED");
                }

                loadSchedulerJobDetailsOnUI();
                loadScheduledJobHistory();

                btnRunAll.Enabled = true;
                btnStopAll.Enabled = true;
                btnRefresh.Enabled = true;
                MessageBox.Show("Scheduler service stopped successfully.", "StaffSync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnRunAll.Enabled = true;
                btnStopAll.Enabled = true;
                btnRefresh.Enabled = true;
                Cursor = Cursors.Default;
            }            
        }

        private async void btnRunAll_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                btnRunAll.Enabled = false;
                btnStopAll.Enabled = false;
                btnRefresh.Enabled = false;

                await SchedulerManager.Start();

                loadSchedulerJobDetailsOnUI();
                loadScheduledJobHistory();

                btnRunAll.Enabled = true;
                btnStopAll.Enabled = true;
                btnRefresh.Enabled = true;
                MessageBox.Show("Scheduler service started successfully.", "StaffSync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnRunAll.Enabled = true;
                btnStopAll.Enabled = true;
                btnRefresh.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                loadSchedulerJobDetailsOnUI();
                loadScheduledJobHistory();
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void tmrTimeLeftCounter_Tick(object sender, EventArgs e)
        {
            UpdateRemainingTime();
        }
    }
}
