using Common;
using DocumentFormat.OpenXml.Office2016.Drawing.Charts;
using Krypton.Toolkit;
using ModelStaffSync;
using Org.BouncyCastle.Asn1.Ocsp;
using StaffSync.StaffsyncDBDataSetTableAdapters;
using StaffSync.StaffsyncDBDTSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StaffSync
{
    public partial class frmSchedulerJobDetails : Form
    {
        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        //DALStaffSync.clsLeaveTRList objLeaveTRList = new DALStaffSync.clsLeaveTRList();
        //DALStaffSync.clsWeeklyOffInfo objWeeklyOffInfo = new DALStaffSync.clsWeeklyOffInfo();
        //DALStaffSync.clsAttendanceMas objAttendanceInfo = new DALStaffSync.clsAttendanceMas();
        //DALStaffSync.clsEmpLeaveEntitlementInfo objEmpLeaveEntitlementInfo = new DALStaffSync.clsEmpLeaveEntitlementInfo();
        //List<WklyOffProfileDetailsInfo> lstWeeklyOffDetailsInfo = new List<WklyOffProfileDetailsInfo>();
        DALStaffSync.clsAuditLog objAuditLog = new DALStaffSync.clsAuditLog();
        DALStaffSync.SchedulerRepository objSchedulerRepository = new DALStaffSync.SchedulerRepository();
        DALStaffSync.clsAdvanceTransaction objAdvanceTransaction = new DALStaffSync.clsAdvanceTransaction();
        DALStaffSync.clsAssetRegister objAssetRegister = new DALStaffSync.clsAssetRegister();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        frmDashboard objDashboard = (frmDashboard)System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();
        ClientStatutory tmpClientStatutory = new ClientStatutory();

        public frmSchedulerJobDetails()
        {
            InitializeComponent();
        }

        public frmSchedulerJobDetails(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmSchedulerJobDetails(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
        }

        public frmSchedulerJobDetails(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo, SchedulerJobModel objSelectedSchedulerJobModel)
        {
            InitializeComponent();

            disableControls();
            clearControls();

            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;

            lblJobID.Text = objSelectedSchedulerJobModel.JobID.ToString();
            lblJobSettingsID.Text = objSelectedSchedulerJobModel.JobSchedulerSettingsID.ToString();
            txtJobCode.Text = objSelectedSchedulerJobModel.JobCode;
            txtJobName.Text = objSelectedSchedulerJobModel.JobName;
            txtJobDescription.Text = objSelectedSchedulerJobModel.Description;
            chkEnabled.Checked = objSelectedSchedulerJobModel.IsEnabled;
            chkRepeat.Checked = objSelectedSchedulerJobModel.RepeatForever;
            chkSystemJob.Checked = objSelectedSchedulerJobModel.IsSystemJob;
            //cmbScheduleType.SelectedText = objSelectedSchedulerJobModel.ScheduleType;
            cmbScheduleType.SelectedIndex = cmbScheduleType.FindStringExact(objSelectedSchedulerJobModel.ScheduleType);
            txtStartDate.Text = Convert.ToDateTime(objSelectedSchedulerJobModel.StartDate).ToString("dd-MM-yyyy");
            txtEndDate.Text = objSelectedSchedulerJobModel.EndDate == null ? "" : Convert.ToDateTime(objSelectedSchedulerJobModel.EndDate).ToString("dd-MM-yyyy");
            txtRunTime.Text = Convert.ToDateTime(objSelectedSchedulerJobModel.RunTime).ToString("hh:mm:ss tt");
            //cmbIntervalUnit.SelectedText = objSelectedSchedulerJobModel.IntervalType;
            cmbIntervalUnit.SelectedIndex = cmbScheduleType.FindStringExact(objSelectedSchedulerJobModel.IntervalType);
            txtInterval.Text = objSelectedSchedulerJobModel.IntervalValue.ToString();
            lblLastRun.Text = objSelectedSchedulerJobModel.LastRun.HasValue ? Convert.ToDateTime(objSelectedSchedulerJobModel.LastRun).ToString("dd-MM-yyyy hh:mm:ss tt") : "";
            lblLastRun.Text = lblLastRun.Text + " (" + objSelectedSchedulerJobModel.LastStatus + ")";
            lblNextRun.Text = objSelectedSchedulerJobModel.NextRun.HasValue ? Convert.ToDateTime(objSelectedSchedulerJobModel.NextRun).ToString("dd-MM-yyyy hh:mm:ss tt") : "";

            if(objSelectedSchedulerJobModel.DayOfWeek == 0)
            {
                optSunday.Checked = true;
            }
            else if (objSelectedSchedulerJobModel.DayOfWeek == 1)
            {
                optMonday.Checked = true;
            }
            else if (objSelectedSchedulerJobModel.DayOfWeek == 2)
            {
                optTuesday.Checked = true;
            }
            else if (objSelectedSchedulerJobModel.DayOfWeek == 3)
            {
                optWednesday.Checked = true;
            }
            else if (objSelectedSchedulerJobModel.DayOfWeek == 4)
            {
                optThursday.Checked = true;
            }
            else if (objSelectedSchedulerJobModel.DayOfWeek == 5)
            {
                optFriday.Checked = true;
            }
            else if (objSelectedSchedulerJobModel.DayOfWeek == 6)
            {
                optSaturday.Checked = true;
            }
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSchedulerJobDetails_Load(object sender, EventArgs e)
        {
            FocusManager.EnableHighlighting = false;
            FocusManager.ShowNavigationError = true;
            FocusManager.Register(this);
            //FocusManager.SetFocus(btnSaveDetails);

            // TODO: This line of code loads data into the 'staffsyncDBDataSet1.qryAllEmpLeavePendingStatement' table. You can move, or remove it, as needed.
            //this.qryAllEmpLeavePendingStatementTableAdapter.Fill(this.staffsyncDBDataSet1.qryAllEmpLeavePendingStatement);
            //// TODO: This line of code loads data into the 'staffsyncDBDTSet.EmpMasInfo' table. You can move, or remove it, as needed.
            //this.empMasInfoTableAdapter.Fill(this.staffsyncDBDTSet.EmpMasInfo);
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
            //frmWeeklyProfileMasterList frmWeeklyProfileMasList = new frmWeeklyProfileMasterList(this, "weeklyOffProfileDetails");
            //frmWeeklyProfileMasList.ShowDialog(this);
        }

        private void btnCloseMe_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenerateDetails_Click(object sender, EventArgs e)
        {
            clearControls();
            enableControls();
            errValidator.Clear();
        }

        public void clearControls()
        {
            lblJobID.Text = "0";
            lblJobSettingsID.Text = "0";
            txtJobCode.Text = "";
            txtJobName.Text = "";
            txtJobDescription.Text = "";
            chkEnabled.Checked = false;
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            txtRunTime.Text = "";
            txtInterval.Text = "";
            lblLastRun.Text = "";
            lblNextRun.Text = "";
            lblRuntime.Text = "";

            cmbScheduleType.Items.Clear();
            cmbScheduleType.Items.Add("");
            cmbScheduleType.Items.Add("INTERVAL");
            cmbScheduleType.Items.Add("SECONDS");
            cmbScheduleType.Items.Add("MINUTES");
            cmbScheduleType.Items.Add("HOURS");
            cmbScheduleType.Items.Add("DAILY");
            cmbScheduleType.Items.Add("WEEKLY");
            cmbScheduleType.Items.Add("MONTHLY");
            cmbScheduleType.Items.Add("YEARLY");
            cmbScheduleType.SelectedIndex = 3;

            cmbIntervalUnit.Items.Clear();
            cmbIntervalUnit.Items.Add("");
            cmbIntervalUnit.Items.Add("INTERVAL");
            cmbIntervalUnit.Items.Add("SECONDS");
            cmbIntervalUnit.Items.Add("MINUTES");
            cmbIntervalUnit.Items.Add("HOURS");
            cmbIntervalUnit.SelectedIndex = 3;

        }

        public void enableControls()
        {
            //dtgAdvanceRequestersList.Enabled = true;
        }

        public void disableControls()
        {
            //btnSaveDetails.Enabled = false;
            //txtLeaveApprovalDate.Enabled = false;
            //dtgBulkLeaveApproval.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            disableControls();
            clearControls();
            errValidator.Clear();
        }

        private void btnModifyDetails_Click(object sender, EventArgs e)
        {
            clearControls();
            enableControls();
            errValidator.Clear();
        }

        private void btnRemoveDetails_Click(object sender, EventArgs e)
        {

        }

        private void frmSchedulerJobDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnChart_Click(object sender, EventArgs e)
        {
        }

        private void cmbChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SeriesChartType selectedType = (SeriesChartType)cmbChartType.SelectedItem;
            //chrtLeaveSummary.Series[0].ChartType = selectedType;
        }

        private void dtgAdvanceRequestersList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void picRefresh_Click(object sender, EventArgs e)
        {

        }

        private void frmSchedulerJobDetails_Activated(object sender, EventArgs e)
        {

        }

        private void dtgAdvanceRequestersList_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtgAssetsRequestersList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEnabled.Checked)
            {
                grpEnableDisable.Enabled = true;
            }
            else
            {
                grpEnableDisable.Enabled = false;
            }
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            string strValidationMessage = objLogin.ValidateUserRolesAndResponsibilitiesInfo(objTempCurrentlyLoggedInUserInfo.EmpID, "");
            if (strValidationMessage != "Success")
            {
                MessageBox.Show(strValidationMessage, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtJobCode.Text))
            {
                txtJobCode.Focus();
                errValidator.SetError(this.txtJobCode, "Please enter Job Code");
            }
            else if (string.IsNullOrEmpty(txtJobName.Text))
            {
                txtJobName.Focus();
                errValidator.SetError(this.txtJobName, "Please enter Job Name");
            }
            else if (string.IsNullOrEmpty(txtJobDescription.Text))
            {
                txtJobDescription.Focus();
                errValidator.SetError(this.txtJobDescription, "Please enter Job Description");
            }
            else if (cmbScheduleType.SelectedIndex == -1)
            {
                cmbScheduleType.Focus();
                errValidator.SetError(this.cmbScheduleType, "Please select Schedule Type");
            }
            else if(string.IsNullOrEmpty(txtStartDate.Text))
            {
                txtStartDate.Focus();
                errValidator.SetError(this.txtStartDate, "Please enter Start Date");
            }
            else if (string.IsNullOrEmpty(txtEndDate.Text))
            {
                txtEndDate.Focus();
                errValidator.SetError(this.txtEndDate, "Please enter End Date");
            }
            else if (string.IsNullOrEmpty(txtRunTime.Text))
            {
                txtRunTime.Focus();
                errValidator.SetError(this.txtRunTime, "Please enter Run Time");
            }
            else if (string.IsNullOrEmpty(txtInterval.Text))
            {
                txtInterval.Focus();
                errValidator.SetError(this.txtInterval, "Please enter Interval Value");
            }
            else if (cmbIntervalUnit.SelectedIndex == -1)
            {
                cmbIntervalUnit.Focus();
                errValidator.SetError(this.cmbIntervalUnit, "Please select Interval Unit");
            }
            else
            {
                errValidator.Clear();
            }

            SchedulerJobModel objSchedulerJobModel = new SchedulerJobModel();
            objSchedulerJobModel.JobID = Convert.ToInt32(lblJobID.Text);
            objSchedulerJobModel.JobCode = txtJobCode.Text;
            objSchedulerJobModel.JobName = txtJobName.Text;
            objSchedulerJobModel.Description = txtJobDescription.Text;
            objSchedulerJobModel.IsSystemJob = chkSystemJob.Checked;
            //objSchedulerJobModel.IsActive = chkEnabled.Checked;
            objSchedulerJobModel.IsEnabled = chkEnabled.Checked;
            objSchedulerJobModel.RepeatForever = chkRepeat.Checked;
            objSchedulerJobModel.ScheduleType = cmbScheduleType.Text;
            if (!string.IsNullOrEmpty(txtStartDate.Text.Trim().Replace("-  -", "")))
                objSchedulerJobModel.StartDate = Convert.ToDateTime(txtStartDate.Text);
            if (!string.IsNullOrEmpty(txtEndDate.Text.Trim().Replace("-  -", "")))
                objSchedulerJobModel.EndDate = Convert.ToDateTime(txtEndDate.Text);
            objSchedulerJobModel.RunTime = Convert.ToDateTime(txtRunTime.Text);
            objSchedulerJobModel.IntervalValue = Convert.ToInt32(txtInterval.Text);
            if(chkEnabled.Checked == false)
                objSchedulerJobModel.IntervalType = cmbIntervalUnit.SelectedItem.ToString();
            if(optSunday.Checked)
            {
                objSchedulerJobModel.DayOfWeek = 0;
                objSchedulerJobModel.DayOfMonth = 0;
                objSchedulerJobModel.WeeklyDayName = "Sunday";
            }
            else if (optMonday.Checked)
            {
                objSchedulerJobModel.DayOfWeek = 1;
                objSchedulerJobModel.DayOfMonth = 1;
                objSchedulerJobModel.WeeklyDayName = "Monday";
            }
            else if (optTuesday.Checked)
            {
                objSchedulerJobModel.DayOfWeek = 2;
                objSchedulerJobModel.DayOfMonth = 2;
                objSchedulerJobModel.WeeklyDayName = "Tuesday";
            }
            else if (optWednesday.Checked)
            {
                objSchedulerJobModel.DayOfWeek = 3;
                objSchedulerJobModel.DayOfMonth = 3;
                objSchedulerJobModel.WeeklyDayName = "Wednesday";
            }
            else if (optThursday.Checked)
            {
                objSchedulerJobModel.DayOfWeek = 4;
                objSchedulerJobModel.DayOfMonth = 4;
                objSchedulerJobModel.WeeklyDayName = "Thursday";
            }
            else if (optFriday.Checked)
            {
                objSchedulerJobModel.DayOfWeek = 5;
                objSchedulerJobModel.DayOfMonth = 5;
                objSchedulerJobModel.WeeklyDayName = "Friday";
            }
            else if (optSaturday.Checked)
            {
                objSchedulerJobModel.DayOfWeek = 6;
                objSchedulerJobModel.DayOfMonth = 6;
                objSchedulerJobModel.WeeklyDayName = "Saturday";
            }

            objSchedulerRepository.UpdateScheduledJobConfigByID(objSchedulerJobModel);

            clearControls();
            disableControls();
        }
    }
}
