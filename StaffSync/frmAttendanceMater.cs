using ModelStaffSync;
using Quartz;
using System;
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
        DALStaffSync.clsAppModule objAppModule = new DALStaffSync.clsAppModule();
        DALStaffSync.clsAttendanceMas objAttendanceMas = new DALStaffSync.clsAttendanceMas();

        public frmAttendanceMater()
        {
            InitializeComponent();
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

            empAttCalender.DisplayMonth = new DateTime();
            empAttCalender.Invalidate(); // Redraw the control

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
            empAttCalender.DisplayMonth = new DateTime();
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

            empAttCalender.DisplayMonth = new DateTime();
            empAttCalender.Invalidate(); // Redraw the control                                         
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
            empAttCalender.DisplayMonth = new DateTime(DateTime.Today.Year, selectedMonth, 1);
            empAttCalender.Invalidate(); // Redraw the control

            List<EmployeeAttendanceInfo> objEmployeeAttendanceList = objAttendanceMas.GetDefaultEmployeeAttendanceInfo(Convert.ToInt16(lblReportingManagerID.Text.ToString()), dtSelectedMonth.Month);
            foreach (EmployeeAttendanceInfo indEmployeeAttendanceInfo in objEmployeeAttendanceList)
            {

                string strDayAttendance = "";
                //strDayAttendance = indEmployeeAttendanceInfo.AttStatus == "Present" ? "Present" : "Leave";

                strDayAttendance = indEmployeeAttendanceInfo.AttStatus;
                if (indEmployeeAttendanceInfo.AttStatus == "Present")
                {
                    PresentCounter = PresentCounter + 1;
                }
                else if (indEmployeeAttendanceInfo.AttStatus == "Leave : Full Day" || indEmployeeAttendanceInfo.AttStatus == "Leave")
                {
                    strDayAttendance = "Leave";
                    empAttCalender.SetDayStyle(new DateTime(indEmployeeAttendanceInfo.AttDate.Year, indEmployeeAttendanceInfo.AttDate.Month, indEmployeeAttendanceInfo.AttDate.Day), strDayAttendance, Color.Yellow, 1f);
                    LeaveCounter = LeaveCounter + 1;
                }
                else if (indEmployeeAttendanceInfo.AttStatus == "Leave : First Half")
                {
                    strDayAttendance = "First Half";
                    empAttCalender.SetDayStyle(new DateTime(indEmployeeAttendanceInfo.AttDate.Year, indEmployeeAttendanceInfo.AttDate.Month, indEmployeeAttendanceInfo.AttDate.Day), strDayAttendance, Color.LightYellow, 0.5f);
                    FirstHalfLeaveCounter = FirstHalfLeaveCounter + 1;
                }
                else if (indEmployeeAttendanceInfo.AttStatus == "Leave : Second Half")
                {
                    strDayAttendance = "Second Half";
                    empAttCalender.SetDayStyle(new DateTime(indEmployeeAttendanceInfo.AttDate.Year, indEmployeeAttendanceInfo.AttDate.Month, indEmployeeAttendanceInfo.AttDate.Day), strDayAttendance, Color.LightYellow, -0.5f);
                    SecondHalfLeaveCounter = SecondHalfLeaveCounter + 1;
                }
            }
        }

        private void btnModifyDetails_Click(object sender, EventArgs e)
        {
            LoadMonthNameList();
            onModifyButtonClick();
            enableControls();
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
                lblLeaveMasID.Text = objLeaveTRList.getMaxLeaveMasID(Convert.ToInt16(selectedEmployeeID.ToString())).ToString();

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

            }

            RefreshEmpAttendanceInfo();
        }

        private void empAttCalender_DetailedDayClicked(object sender, DetailedDateClickedEventArgs e)
        {
            MessageBox.Show("Detailed Day Clicked: " + e.Date.ToShortDateString() + ", " + e.FillAmount + ", " + e.CustomText, "Calendar Event", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (lblActionMode.Text != "")
            {
                if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            this.Close();
        }
    }
}
