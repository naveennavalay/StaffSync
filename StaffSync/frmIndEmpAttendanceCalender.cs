using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using ModelStaffSync;
using Quartz.Impl.AdoJobStore.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Quartz.Logging.OperationName;

namespace StaffSync
{
    public partial class frmIndEmpAttendanceCalender : Form
    {
        DALStaffSync.clsFinYearMas objFinYearMas = new DALStaffSync.clsFinYearMas();
        DALStaffSync.clsCurrentUserInfo objCurrentUserInfo = new DALStaffSync.clsCurrentUserInfo();
        DALStaffSync.clsClientInfo objClientInfo = new DALStaffSync.clsClientInfo();
        DALStaffSync.clsAttendanceMas objAttendanceMas = new DALStaffSync.clsAttendanceMas();
        frmDashboard objDashboard = (frmDashboard)System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        DateTime dob, doj;
        string dateFormat = "dd-MM-yyyy";
        CultureInfo provider = CultureInfo.InvariantCulture;
        ClientInfo selectedCompany = new ClientInfo();
        List<FinYearMas> selectedFinYearMas = new List<FinYearMas>();
        ClientFinYearInfo objClientFinYearInfo = new ClientFinYearInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();

        public frmIndEmpAttendanceCalender()
        {
            InitializeComponent();
        }

        public frmIndEmpAttendanceCalender(ClientFinYearInfo objClientFinYearInfo)
        {
            InitializeComponent();
        }

        public frmIndEmpAttendanceCalender(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmIndEmpAttendanceCalender(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
        }

        public frmIndEmpAttendanceCalender(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo, int EmployeeID, DateTime dtSelectedDate)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
            lblEmpID.Text = EmployeeID.ToString();
            RefreshEmpAttendanceInfo(dtSelectedDate);
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
            this.Close();
        }

        private void frmIndEmpAttendanceCalender_Load(object sender, EventArgs e)
        {

        }

        private void frmIndEmpAttendanceCalender_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        public ClientFinYearInfo GetSelectedClientAndFinYearDetails()
        {
            return objClientFinYearInfo;
        }

        private void RefreshEmpAttendanceInfo(DateTime dtSelectedMonth)
        {
            int PresentCounter = 0;
            int LeaveCounter = 0;
            int FirstHalfLeaveCounter = 0;
            int SecondHalfLeaveCounter = 0;
            DateTime dtCurrentDate = new DateTime();

            empAttCalender.DisplayMonth = new DateTime(dtSelectedMonth.Year, dtSelectedMonth.Month, 1);
            //empAttCalender.SetDayStyle(new DateTime(DateTime.Today.Year, cmbMonthNameList.SelectedIndex + 1, 17), "", Color.FromArgb(213, 228, 242), 0f);
            empAttCalender.Invalidate(); // Redraw the control

            //dtSelectedMonth = Convert.ToDateTime("1-" + (cmbMonthNameList.SelectedIndex + 1) + "-" + (DateTime.Now.Year));
            empAttCalender.ClearCalendar();
            //for(int day = 1; day <= TotalDaysInMonth; day++)
            //{
            //    empAttCalender.SetDayStyle(new DateTime(dtSelectedMonth.Year, dtSelectedMonth.Month, day), "", Color.FromArgb(213, 228, 242), 0f);
            //}

            List<EmployeeAttendanceInfo> objEmployeeAttendanceList = objAttendanceMas.GetDefaultEmployeeAttendanceInfo(Convert.ToInt16(lblEmpID.Text.ToString()), dtSelectedMonth);
            foreach (EmployeeAttendanceInfo indEmployeeAttendanceInfo in objEmployeeAttendanceList)
            {

                string strDayAttendance = "";
                //strDayAttendance = indEmployeeAttendanceInfo.AttStatus == "Present" ? "Present" : "Leave";

                if(indEmployeeAttendanceInfo.AttDate != dtCurrentDate)
                {
                    dtCurrentDate = indEmployeeAttendanceInfo.AttDate;
                }

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
                    if (strDayAttendance.Contains("First") || strDayAttendance.Contains("Second"))
                    {
                        strDayAttendance = "Leave";
                        empAttCalender.SetDayStyle(new DateTime(indEmployeeAttendanceInfo.AttDate.Year, indEmployeeAttendanceInfo.AttDate.Month, indEmployeeAttendanceInfo.AttDate.Day), strDayAttendance, System.Drawing.Color.LightYellow, -0.5f);
                        SecondHalfLeaveCounter = SecondHalfLeaveCounter + 1;
                    }
                    else
                    {
                        strDayAttendance = "Second Half";
                        empAttCalender.SetDayStyle(new DateTime(indEmployeeAttendanceInfo.AttDate.Year, indEmployeeAttendanceInfo.AttDate.Month, indEmployeeAttendanceInfo.AttDate.Day), strDayAttendance, System.Drawing.Color.LightYellow, -0.5f);
                        SecondHalfLeaveCounter = SecondHalfLeaveCounter + 1;
                    }
                }
                else if (indEmployeeAttendanceInfo.AttStatus == "Leave : First Half, Leave : Second Half" || indEmployeeAttendanceInfo.AttStatus == "Leave : Second Half, Leave : First Half")
                {
                    strDayAttendance = "Leave";
                    empAttCalender.SetDayStyle(new DateTime(indEmployeeAttendanceInfo.AttDate.Year, indEmployeeAttendanceInfo.AttDate.Month, indEmployeeAttendanceInfo.AttDate.Day), strDayAttendance, System.Drawing.Color.LightYellow, 1f);
                    LeaveCounter = LeaveCounter + 1;
                }
                else if (indEmployeeAttendanceInfo.AttStatus == "Leave : Full Day - Loss Of Pay")
                {
                    strDayAttendance = "Leave (LOP)";
                    empAttCalender.SetDayStyle(new DateTime(indEmployeeAttendanceInfo.AttDate.Year, indEmployeeAttendanceInfo.AttDate.Month, indEmployeeAttendanceInfo.AttDate.Day), strDayAttendance, System.Drawing.Color.GreenYellow, 1f);
                    LeaveCounter = LeaveCounter + 1;
                }
                else if (indEmployeeAttendanceInfo.AttStatus == "Leave : First Half - Loss Of Pay")
                {
                    strDayAttendance = "Leave (LOP)";
                    empAttCalender.SetDayStyle(new DateTime(indEmployeeAttendanceInfo.AttDate.Year, indEmployeeAttendanceInfo.AttDate.Month, indEmployeeAttendanceInfo.AttDate.Day), strDayAttendance, System.Drawing.Color.GreenYellow, 0.5f);
                    LeaveCounter = LeaveCounter + 1;
                }
                else if (indEmployeeAttendanceInfo.AttStatus == "Leave : Second Half - Loss Of Pay")
                {
                    strDayAttendance = "Leave (LOP)";
                    empAttCalender.SetDayStyle(new DateTime(indEmployeeAttendanceInfo.AttDate.Year, indEmployeeAttendanceInfo.AttDate.Month, indEmployeeAttendanceInfo.AttDate.Day), strDayAttendance, System.Drawing.Color.GreenYellow, -0.5f);
                    LeaveCounter = LeaveCounter + 1;
                }
                else if (indEmployeeAttendanceInfo.AttStatus == "Leave : First Half - Loss Of Pay, Leave : Second Half - Loss Of Pay" || indEmployeeAttendanceInfo.AttStatus == "Leave : Second Half - Loss Of Pay, Leave : First Half - Loss Of Pay")
                {
                    strDayAttendance = "Leave (LOP)";
                    empAttCalender.SetDayStyle(new DateTime(indEmployeeAttendanceInfo.AttDate.Year, indEmployeeAttendanceInfo.AttDate.Month, indEmployeeAttendanceInfo.AttDate.Day), strDayAttendance, System.Drawing.Color.GreenYellow, 1f);
                    LeaveCounter = LeaveCounter + 1;
                }
            }
        }
    }
}
