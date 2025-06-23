using Calendar.NET;
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

namespace StaffSync
{
    public partial class frmAttendanceMater : Form
    {

        clsEmployeeMaster objEmployeeMaster = new clsEmployeeMaster();
        clsUserManagement objUserManagementList = new clsUserManagement();
        clsImpageOperation objImpageOperation = new clsImpageOperation();
        clsPhotoMas objPhotoMas = new clsPhotoMas();
        clsLogin objLogin = new clsLogin();
        clsAppModule objAppModule = new clsAppModule();
        clsAttendanceMas objAttendanceMas = new clsAttendanceMas();

        public frmAttendanceMater()
        {
            InitializeComponent();
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
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

            //empAttCalender = new Calendar.NET.Calendar();
            empAttCalender.CalendarDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            empAttCalender.CalendarView = CalendarViews.Month;
            empAttCalender.AllowEditingEvents = false;
            empAttCalender.Refresh();
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
            lblTotalPresent.Text = "0";
            lblTotalLeave.Text = "0";

            //empAttCalender = new Calendar.NET.Calendar();
            empAttCalender.CalendarDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            empAttCalender.CalendarView = CalendarViews.Month;
            empAttCalender.AllowEditingEvents = false;
        }

        public void enableControls()
        {
            //txtRepEmpCode.Enabled = false;
            //txtRepEmpName.Enabled = false;
            //txtRepEmpDesig.Enabled = false;
            //txtRepEmpDepartment.Enabled = false;
            //txtRepEmpContactNumber.Enabled = false;
            btnReportingManagerSearch.Enabled = true;
        }

        public void disableControls()
        {
            btnReportingManagerSearch.Enabled = false;
            txtRepEmpCode.Enabled = false;
            txtRepEmpName.Enabled = false;
            txtRepEmpDesig.Enabled = false;
            txtRepEmpDepartment.Enabled = false;
            txtRepEmpContactNumber.Enabled = false;
        }

        private void RefreshEmpAttendanceInfo()
        {
            int PresentCounter = 0;
            int LeaveCounter = 0;
            List<EmployeeAttendanceInfo> objEmployeeAttendanceList = objAttendanceMas.GetDefaultEmployeeAttendanceInfo(Convert.ToInt16(lblReportingManagerID.Text.ToString()));
            foreach (EmployeeAttendanceInfo indEmployeeAttendanceInfo in objEmployeeAttendanceList)
            {

                string strDayAttendance = "";
                strDayAttendance = indEmployeeAttendanceInfo.AttStatus == "Present" ? "Present" : "Leave";

                var ce2 = new CustomEvent
                {
                    IgnoreTimeComponent = false,
                    EventText = strDayAttendance,
                    Date = new DateTime(indEmployeeAttendanceInfo.AttDate.Year, indEmployeeAttendanceInfo.AttDate.Month, indEmployeeAttendanceInfo.AttDate.Day, 0, 0, 0),
                    EventLengthInHours = 2f,
                    RecurringFrequency = RecurringFrequencies.None,
                    EventFont = new Font("Verdana", 12, FontStyle.Regular),
                    Enabled = true,
                    EventColor = strDayAttendance == "Present" ? Color.Yellow : Color.OrangeRed,
                    EventTextColor = Color.Black,
                    ThisDayForwardOnly = true
                };

                if (strDayAttendance == "Present")
                    PresentCounter = PresentCounter + 1;
                else if (strDayAttendance == "Leave")
                    LeaveCounter = LeaveCounter + 1;

                empAttCalender.AddEvent(ce2);
            }

            empAttCalender.Refresh();
            lblTotalPresent.Text = PresentCounter.ToString();
            lblTotalLeave.Text = LeaveCounter.ToString();
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
                int deletedExistingRecordCount = 1;

                if (insertNewRecordCount > 0)
                {
                    //objLeaveTRList.UpdateEmployeeLeaveBalance(Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToDecimal(txtAvailableLeave.Text), Convert.ToDecimal(txtBalanceLeave.Text));
                    MessageBox.Show("Details updated successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Details not inserted successfully.\nPlease verify once again.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
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
            //empAttCalender = new Calendar.NET.Calendar();
            empAttCalender.CalendarDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 0, 0, 0);
            empAttCalender.CalendarView = CalendarViews.Month;
            empAttCalender.AllowEditingEvents = false;

            lblActionMode.Text = "";
            onCancelButtonClick();
            disableControls();
            clearControls();
        }
    }
}
