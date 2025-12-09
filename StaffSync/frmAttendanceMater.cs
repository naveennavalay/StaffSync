using DocumentFormat.OpenXml.Wordprocessing;
using Krypton.Toolkit;
using ModelStaffSync;
using Quartz;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static StaffSync.TextBoxHelper;

namespace StaffSync
{
    public partial class frmAttendanceMater : Form
    {

        DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster();
        DALStaffSync.clsLeaveTRList objLeaveTRList = new DALStaffSync.clsLeaveTRList();
        DALStaffSync.clsUserManagement objUserManagementList = new DALStaffSync.clsUserManagement();
        clsImpageOperation objImpageOperation = new clsImpageOperation();
        DALStaffSync.clsPhotoMas objPhotoMas = new DALStaffSync.clsPhotoMas();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        DALStaffSync.clsLeaveTypeMas objLeaveTypeMas = new DALStaffSync.clsLeaveTypeMas();
        DALStaffSync.clsAppModule objAppModule = new DALStaffSync.clsAppModule();
        DALStaffSync.clsAttendanceMas objAttendanceMas = new DALStaffSync.clsAttendanceMas();
        DALStaffSync.clsWeeklyOffInfo objWeeklyOffInfo = new DALStaffSync.clsWeeklyOffInfo();
        DALStaffSync.clsEmpMnthlyAttdInfo objEmpMnthlyAttdInfo = new DALStaffSync.clsEmpMnthlyAttdInfo();
        frmDashboard objDashboard = (frmDashboard)System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        DALStaffSync.clsAttendanceMas objAttendanceInfo = new DALStaffSync.clsAttendanceMas();
        List<WklyOffProfileDetailsInfo> lstWeeklyOffDetailsInfo = new List<WklyOffProfileDetailsInfo>();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();

        ArrayList arrControlList = new ArrayList();

        public frmAttendanceMater()
        {
            InitializeComponent();
        }

        public frmAttendanceMater(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            empAttCalender.ResetWeekendDaysToDefault();
        }

        public frmAttendanceMater(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
            empAttCalender.ResetWeekendDaysToDefault();
        }

        //public frmAttendanceMater(string SearchOptionSelectedForm, int selectedEmployeeID)
        //{
        //    InitializeComponent();
        //    LoadMonthNameList();
        //    onModifyButtonClick();
        //    enableControls();
        //    SelectedEmployeeID("listAttendanceMasterList", Convert.ToInt16(selectedEmployeeID));
        //}

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            int selectedMonth = cmbMonthNameList.SelectedIndex + 1;

            if (lblActionMode.Text == "modify")
            {
                if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            empAttCalender.ClearCalendar();
            empAttCalender.DisplayMonth = new DateTime(DateTime.Today.Year, selectedMonth, 1);
            empAttCalender.Invalidate(); // Redraw the control

            lblActionMode.Text = "";
            onCancelButtonClick();
            disableControls();
            clearControls();
            LoadMonthNameList();
            errValidator.Clear();
            cmbMonthNameList.SelectedIndex = DateTime.Now.Month - 1; // Set to current month
            selectedMonth = cmbMonthNameList.SelectedIndex + 1;
            empAttCalender.DisplayMonth = new DateTime(DateTime.Today.Year, selectedMonth, 1);
            empAttCalender.Invalidate(); // Redraw the control

        }

        public void onModifyButtonClick()
        {
            lblActionMode.Text = "modify";
            lblReportingManagerID.Text = "";
            btnReportingManagerSearch.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = true;
            btnCancel.Enabled = true;
            int selectedMonth = cmbMonthNameList.SelectedIndex + 1;
            empAttCalender.DisplayMonth = new DateTime(DateTime.Today.Year, selectedMonth, 1);
            empAttCalender.Invalidate(); // Redraw the control
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
            picRepEmpPhoto.Image = null;
            int selectedMonth = cmbMonthNameList.SelectedIndex + 1;
            empAttCalender.DisplayMonth = new DateTime(DateTime.Today.Year, selectedMonth, 1);
            empAttCalender.Invalidate(); // Redraw the control

            int TotalDaysInMonth = DateTime.DaysInMonth(DateTime.Today.Year, selectedMonth);
            DateTime dtSelectedMonth = new DateTime(DateTime.Now.Year, cmbMonthNameList.SelectedIndex + 1, 1, 1, 1, 1);
            dtSelectedMonth = Convert.ToDateTime("1-" + (cmbMonthNameList.SelectedIndex + 1) + "-" + (DateTime.Now.Year));
            empAttCalender.ClearCalendar();
            empAttCalender.ResetWeekendDaysToDefault();
            //for (int day = 1; day <= TotalDaysInMonth; day++)
            //{
            //    empAttCalender.SetDayStyle(new DateTime(dtSelectedMonth.Year, dtSelectedMonth.Month, day), "", Color.FromArgb(213, 228, 242), 0f);
            //}
        }

        public void enableControls()
        {
            //txtRepEmpCode.Enabled = false;
            //txtRepEmpName.Enabled = false;
            //txtRepEmpDesig.Enabled = false;
            //txtRepEmpDepartment.Enabled = false;
            //txtRepEmpContactNumber.Enabled = false;
            btnReportingManagerSearch.Enabled = true;
            cmbMonthNameList.Enabled = true;
        }

        public void disableControls()
        {
            btnReportingManagerSearch.Enabled = false;
            txtRepEmpCode.Enabled = false;
            txtRepEmpName.Enabled = false;
            txtRepEmpDesig.Enabled = false;
            txtRepEmpDepartment.Enabled = false;
            txtRepEmpContactNumber.Enabled = false;
            cmbMonthNameList.Enabled = false;
        }

        private void RefreshEmpAttendanceInfo()
        {
            int PresentCounter = 0;
            int LeaveCounter = 0;
            int FirstHalfLeaveCounter = 0;
            int SecondHalfLeaveCounter = 0;

            DateTime dtSelectedMonth = new DateTime(DateTime.Now.Year, cmbMonthNameList.SelectedIndex + 1, 1, 1, 1, 1);

            int selectedMonth = cmbMonthNameList.SelectedIndex + 1;

            int TotalDaysInMonth = DateTime.DaysInMonth(DateTime.Today.Year, selectedMonth);



            empAttCalender.DisplayMonth = new DateTime(DateTime.Today.Year, selectedMonth, 1);
            //empAttCalender.SetDayStyle(new DateTime(DateTime.Today.Year, cmbMonthNameList.SelectedIndex + 1, 17), "", Color.FromArgb(213, 228, 242), 0f);
            empAttCalender.Invalidate(); // Redraw the control

            dtSelectedMonth = Convert.ToDateTime("1-" + (cmbMonthNameList.SelectedIndex + 1) + "-" + (DateTime.Now.Year));
            empAttCalender.ClearCalendar();
            //for(int day = 1; day <= TotalDaysInMonth; day++)
            //{
            //    empAttCalender.SetDayStyle(new DateTime(dtSelectedMonth.Year, dtSelectedMonth.Month, day), "", Color.FromArgb(213, 228, 242), 0f);
            //}

            List<EmployeeAttendanceInfo> objEmployeeAttendanceList = objAttendanceMas.GetDefaultEmployeeAttendanceInfo(Convert.ToInt16(lblReportingManagerID.Text.ToString()), dtSelectedMonth);
            foreach (EmployeeAttendanceInfo indEmployeeAttendanceInfo in objEmployeeAttendanceList)
            {

                string strDayAttendance = "";
                //strDayAttendance = indEmployeeAttendanceInfo.AttStatus == "Present" ? "Present" : "Leave";

                strDayAttendance = indEmployeeAttendanceInfo.AttStatus;
                if (indEmployeeAttendanceInfo.AttStatus == "Present")
                {
                    strDayAttendance = "Present";
                    empAttCalender.SetDayStyle(new DateTime(indEmployeeAttendanceInfo.AttDate.Year, indEmployeeAttendanceInfo.AttDate.Month, indEmployeeAttendanceInfo.AttDate.Day), strDayAttendance, System.Drawing.Color.LimeGreen, 1f);
                    PresentCounter = PresentCounter + 1;
                }
                else if (indEmployeeAttendanceInfo.AttStatus == "Leave : Full Day" || indEmployeeAttendanceInfo.AttStatus == "Leave")
                {
                    strDayAttendance = "Leave";
                    empAttCalender.SetDayStyle(new DateTime(indEmployeeAttendanceInfo.AttDate.Year, indEmployeeAttendanceInfo.AttDate.Month, indEmployeeAttendanceInfo.AttDate.Day), strDayAttendance, System.Drawing.Color.Yellow, 1f);
                    LeaveCounter = LeaveCounter + 1;
                }
                else if (indEmployeeAttendanceInfo.AttStatus == "Leave : First Half")
                {
                    strDayAttendance = "First Half";
                    empAttCalender.SetDayStyle(new DateTime(indEmployeeAttendanceInfo.AttDate.Year, indEmployeeAttendanceInfo.AttDate.Month, indEmployeeAttendanceInfo.AttDate.Day), strDayAttendance, System.Drawing.Color.LightYellow, 0.5f);
                    FirstHalfLeaveCounter = FirstHalfLeaveCounter + 1;
                }
                else if (indEmployeeAttendanceInfo.AttStatus == "Leave : Second Half")
                {
                    strDayAttendance = "Second Half";
                    empAttCalender.SetDayStyle(new DateTime(indEmployeeAttendanceInfo.AttDate.Year, indEmployeeAttendanceInfo.AttDate.Month, indEmployeeAttendanceInfo.AttDate.Day), strDayAttendance, System.Drawing.Color.LightYellow, -0.5f);
                    SecondHalfLeaveCounter = SecondHalfLeaveCounter + 1;
                }
            }
        }

        private void btnModifyDetails_Click(object sender, EventArgs e)
        {
            LoadMonthNameList();
            onModifyButtonClick();
            enableControls();

            cmbWeeklyOff.DataSource = objWeeklyOffInfo.getWklyOffProfileMasInfoList("");
            cmbWeeklyOff.DisplayMember = "WklyOffTitle";
            cmbWeeklyOff.ValueMember = "WklyOffMasID";
        }

        private void LoadMonthNameList()
        {
            Dictionary<int, string> Months = Enumerable.Range(1, 12).Select(i => new KeyValuePair<int, string>(i, System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(i))).ToDictionary(x => x.Key, x => x.Value);
            cmbMonthNameList.DataSource = new BindingSource(Months, null);
            cmbMonthNameList.DisplayMember = "Value";
            cmbMonthNameList.ValueMember = "Key";
            cmbMonthNameList.SelectedIndex = DateTime.Now.Month - 1; // Set to current month
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(empAttCalender.SelectedDay.Value.ToString("dd-MMM-yyyy"), "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (empAttCalender.SelectedDay == null)
            {
                MessageBox.Show("To update your attendance, please choose a date before continue.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int RowCounter = 0;
            int DailyAttendanceID = 0;
            DateTime currSelectedDate = DateTime.Today;

            if(empAttCalender.IsWeekend(Convert.ToDateTime(empAttCalender.SelectedDay.Value.ToString("dd-MMM-yyyy"))))
            {
                if (MessageBox.Show("It appears that you selected \"Weekend\" as the Attendance Date. \nWould you like to proceed..?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            if (chkAllowBackDated.Checked == true)
            {
                currSelectedDate = Convert.ToDateTime(empAttCalender.SelectedDay.Value.ToString("dd-MMM-yyyy"));
            }

            if (empAttCalender.IsDayChecked(currSelectedDate))
            {
                DateTime dtAttSelectedDate = Convert.ToDateTime(empAttCalender.SelectedDay.Value.ToString("dd-MMM-yyyy"));

                int insertNewRecordCount = 0;
                int PresentCounter = 0;
                int LeaveCounter = 0;
                int FirstHalfLeaveCounter = 0;
                int SecondHalfLeaveCounter = 0;
                int MonthlyAttendanceSlNumber = 0;


                if (validateValues())
                {

                    List<EmployeeAttendanceInfo> objEmployeeAttendanceList = objAttendanceMas.GetDefaultEmployeeAttendanceInfoForJobs(Convert.ToInt16(lblReportingManagerID.Text.ToString()), dtAttSelectedDate);
                    if (objEmployeeAttendanceList.Count > 0)
                    {
                        foreach (EmployeeAttendanceInfo indEmployeeAttendanceInfo in objEmployeeAttendanceList)
                        {
                            string strDayAttendance = "";
                            //strDayAttendance = indEmployeeAttendanceInfo.AttStatus == "Present" ? "Present" : "Leave";

                            strDayAttendance = indEmployeeAttendanceInfo.AttStatus;
                            if (indEmployeeAttendanceInfo.AttStatus == "Present")
                            {
                                strDayAttendance = "Present";
                            }
                            else if (indEmployeeAttendanceInfo.AttStatus == "Leave : Full Day" || indEmployeeAttendanceInfo.AttStatus == "Leave")
                            {
                                strDayAttendance = indEmployeeAttendanceInfo.AttStatus; //"Leave : Full Day";
                            }
                            else if (indEmployeeAttendanceInfo.AttStatus == "Leave : First Half")
                            {
                                strDayAttendance = indEmployeeAttendanceInfo.AttStatus;
                            }
                            else if (indEmployeeAttendanceInfo.AttStatus == "Leave : Second Half")
                            {
                                strDayAttendance = indEmployeeAttendanceInfo.AttStatus;
                            }

                            MonthlyAttendanceSlNumber = objEmpMnthlyAttdInfo.getMonthlyAttendanceInfo(indEmployeeAttendanceInfo.EmpID, dtAttSelectedDate);
                            if (MonthlyAttendanceSlNumber == 0)
                            {
                                RowCounter = objEmpMnthlyAttdInfo.InsertMonthlyAttendanceInfo(indEmployeeAttendanceInfo.EmpID, dtAttSelectedDate);
                            }
                            else
                            {
                                RowCounter = objEmpMnthlyAttdInfo.UpdateMonthlyAttendanceInfo(MonthlyAttendanceSlNumber, indEmployeeAttendanceInfo.EmpID, dtAttSelectedDate, "Day" + indEmployeeAttendanceInfo.AttDate.Day, strDayAttendance);
                                RowCounter = objAttendanceInfo.UpdateDailyAttendance(Convert.ToInt16(lblReportingManagerID.Text.ToString()), Convert.ToDateTime(dtAttSelectedDate.Date.ToString()), strDayAttendance, Convert.ToInt16(indEmployeeAttendanceInfo.LeaveTRID));
                                if (RowCounter == 0)
                                    RowCounter = objAttendanceInfo.InsertDailyAttendance(Convert.ToInt16(lblReportingManagerID.Text.ToString()), Convert.ToDateTime(dtAttSelectedDate.Date.ToString()), strDayAttendance, Convert.ToInt16(indEmployeeAttendanceInfo.LeaveTRID));
                            }
                        }
                    }
                    else
                    {
                        MonthlyAttendanceSlNumber = objEmpMnthlyAttdInfo.getMonthlyAttendanceInfo(Convert.ToInt16(lblReportingManagerID.Text.ToString()), dtAttSelectedDate);
                        if (MonthlyAttendanceSlNumber == 0)
                        {
                            RowCounter = objEmpMnthlyAttdInfo.InsertMonthlyAttendanceInfo(Convert.ToInt16(lblReportingManagerID.Text.ToString()), dtAttSelectedDate);
                            RowCounter = objEmpMnthlyAttdInfo.UpdateMonthlyAttendanceInfo(MonthlyAttendanceSlNumber, Convert.ToInt16(lblReportingManagerID.Text.ToString()), dtAttSelectedDate, "Day" + dtAttSelectedDate.Day, "Present");
                            RowCounter = objAttendanceInfo.InsertDailyAttendance(Convert.ToInt16(lblReportingManagerID.Text.ToString()), Convert.ToDateTime(dtAttSelectedDate.Date.ToString()), "Present", 0);
                        }
                        else
                        {
                            MonthlyAttendanceSlNumber = objEmpMnthlyAttdInfo.UpdateMonthlyAttendanceInfo(MonthlyAttendanceSlNumber, Convert.ToInt16(lblReportingManagerID.Text.ToString()), dtAttSelectedDate, "Day" + dtAttSelectedDate.Day, "Present");
                            RowCounter = objAttendanceInfo.UpdateDailyAttendance(Convert.ToInt16(lblReportingManagerID.Text.ToString()), Convert.ToDateTime(dtAttSelectedDate.Date.ToString()), "Present", 0);
                            if (RowCounter == 0)
                                RowCounter = objAttendanceInfo.InsertDailyAttendance(Convert.ToInt16(lblReportingManagerID.Text.ToString()), Convert.ToDateTime(dtAttSelectedDate.Date.ToString()), "Present", 0);
                        }
                    }

                    if (RowCounter > 0)
                    {
                        MessageBox.Show("Details updated successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Details not inserted successfully.\nPlease verify once again.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                objTempCurrentlyLoggedInUserInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(lblReportingManagerID.Text.ToString()));
                onSaveButtonClick();
                clearControls();
                disableControls();
            }
            else
            {
                MessageBox.Show("Select Attendance Date to continue", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool validateValues()
        {
            bool validateStatus = true;

            return validateStatus;
        }

        public void SelectedEmployeeID(string SearchOptionSelectedForm, int selectedEmployeeID)
        {
            if (SearchOptionSelectedForm == "listAttendanceMasterList")
            {
                lblReportingManagerID.Text = selectedEmployeeID.ToString();
                ReportingManagerInfo objReportingManagerInfo = objEmployeeMaster.GetReportingManagerInfo(Convert.ToInt16(selectedEmployeeID.ToString()));
                txtRepEmpCode.Text = objReportingManagerInfo.EmpCode;
                txtRepEmpName.Text = objReportingManagerInfo.EmpName;
                txtRepEmpDesig.Text = objReportingManagerInfo.DesignationTitle;
                txtRepEmpDepartment.Text = objReportingManagerInfo.DepartmentTitle;
                picRepEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(lblReportingManagerID.Text.ToString())).EmpPhoto);
                lblLeaveMasID.Text = objLeaveTRList.getMaxLeaveMasID(Convert.ToInt16(selectedEmployeeID.ToString())).ToString();

                List<EmployeeWklyOffInfo> objWeeklyOff = objWeeklyOffInfo.getEmployeeSpecificWeeklyOffMasterInfo(Convert.ToInt16(selectedEmployeeID.ToString()));
                //lblEmployeeWeeklyOffID.Text = objWeeklyOff.OrderByDescending(x => x.EffectDateFrom).FirstOrDefault().WeeklyOffID.ToString();
                cmbWeeklyOff.SelectedIndex = objWeeklyOff.OrderByDescending(x => x.EffectDateFrom).FirstOrDefault().WklyOffMasID - 1;
                if (cmbWeeklyOff.SelectedIndex < 0)
                    cmbWeeklyOff.SelectedIndex = 0;

                lstWeeklyOffDetailsInfo = objWeeklyOffInfo.getWeeklyOffDetailsInfo(Convert.ToInt16(cmbWeeklyOff.SelectedIndex) + 1).ToList();
                int[] arr = new int[lstWeeklyOffDetailsInfo.Count];
                int iCounter = 0;
                foreach (WklyOffProfileDetailsInfo indDay in lstWeeklyOffDetailsInfo)
                {
                    if (indDay.WklyOffDay > 6)
                        indDay.WklyOffDay = 0;
                    arr[iCounter] = indDay.WklyOffDay;   
                    iCounter = iCounter + 1;
                }
                empAttCalender.SetWeekendDays(arr);

                UserInfo objLoggingInUserInfo = objLogin.getSpecificUserInfo(Convert.ToInt16(selectedEmployeeID.ToString()));
                if (objLoggingInUserInfo.UserID != 0)
                {

                }

                RefreshEmpAttendanceInfo();
            }
        }

        private void btnReportingManagerSearch_Click(object sender, EventArgs e)
        {
            frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listAttendanceMasterList");
            frmEmployeeList.ShowDialog();
        }

        private void frmAttendanceMater_Load(object sender, EventArgs e)
        {
            LoadMonthNameList();

            //cmbMonthNameList.SelectedIndex = DateTime.Now.Month - 1; // Set to current month

            lblActionMode.Text = "";
            onCancelButtonClick();
            disableControls();
            clearControls();
        }

        private void cmbMonthNameList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lblReportingManagerID.Text.ToString().Trim() == "")
                return;

            UserInfo objLoggingInUserInfo = objLogin.getSpecificUserInfo(Convert.ToInt16(lblReportingManagerID.Text.ToString()));
            if (objLoggingInUserInfo.UserID != 0)
            {
                List<MonthlyAttendanceInfo> objMonthlyAttendanceInfo = objEmpMnthlyAttdInfo.getConsolidatedMonthlyAttendanceInfo(Convert.ToDateTime("1-" + (cmbMonthNameList.SelectedIndex+1) + "-" + (DateTime.Now.Year)));
            }

            RefreshEmpAttendanceInfo();
        }

        private void empAttCalender_DetailedDayClicked(object sender, DetailedDateClickedEventArgs e)
        {
            //MessageBox.Show("Detailed Day Clicked: " + e.Date.ToShortDateString() + ", " + e.FillAmount + ", " + e.CustomText, "Calendar Event", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void picViewLeaves_Click(object sender, EventArgs e)
        {
            if (lblReportingManagerID.Text.Trim() == "")
                return;

            frmViewLeavesOutstanding frmViewLeavesOutstanding = new frmViewLeavesOutstanding(Convert.ToInt16(lblReportingManagerID.Text.ToString()), Convert.ToInt16(lblLeaveMasID.Text.ToString()));
            frmViewLeavesOutstanding.ShowDialog(this);
        }

        private void frmAttendanceMater_KeyDown(object sender, KeyEventArgs e)
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

        private void chkAllowBackDated_CheckedChanged(object sender, EventArgs e)
        {
            empAttCalender.AllowPreviousDates = chkAllowBackDated.Checked;
        }

        private void empAttCalender_DayClicked(object sender, DateClickedEventArgs e)
        {
            //if (empAttCalender.IsWeekend(e.Date))
            //{
            //    MessageBox.Show("You clicked a WEEKEND");
            //}
            //else
            //{
            //    MessageBox.Show("You clicked a WEEKDAY");
            //}
        }
    }
}
