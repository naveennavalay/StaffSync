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
    public partial class frmDailyAttendanceProcess : Form
    {

        DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster();
        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        DALStaffSync.clsLeaveTRList objLeaveTRList = new DALStaffSync.clsLeaveTRList();
        DALStaffSync.clsUserManagement objUserManagementList = new DALStaffSync.clsUserManagement();
        clsImpageOperation objImpageOperation = new clsImpageOperation();
        DALStaffSync.clsPhotoMas objPhotoMas = new DALStaffSync.clsPhotoMas();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        DALStaffSync.clsAppModule objAppModule = new DALStaffSync.clsAppModule();
        DALStaffSync.clsAttendanceMas objAttendanceMas = new DALStaffSync.clsAttendanceMas();
        DALStaffSync.clsEmpMnthlyAttdInfo objEmpMnthlyAttdInfo = new DALStaffSync.clsEmpMnthlyAttdInfo();
        DALStaffSync.clsWeeklyOffInfo objWeeklyOffInfo = new DALStaffSync.clsWeeklyOffInfo();
        DALStaffSync.clsAttendanceMas objAttendanceInfo = new DALStaffSync.clsAttendanceMas();
        frmDashboard objDashboard = (frmDashboard)System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();

        public frmDailyAttendanceProcess()
        {
            InitializeComponent();
            lblBatchProcessID.Text = "";
        }

        public frmDailyAttendanceProcess(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            lblBatchProcessID.Text = "";
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmDailyAttendanceProcess(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;

            lblBatchProcessID.Text = "";
            lblNote.Text = "Note:\n* The system will automatically mark attendance as \"Present\".\n* No updates will be made if attendance is already marked as \"Present\".\n* Attendance will not be updated for weekly off days.\n* Attendance will not be updated for leave days.";

            txtDailyAttendanceDate.Value = DateTime.Today;

            RefreshEmpAttendanceInfo("Compact View");
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
            objDashboard.lblDashboardTitle.Text = "Dashboard";
            objDashboard.sptrDashboardContainer.Visible = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //if (lblActionMode.Text == "modify")
            //{
            //    if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //    {
            //        return;
            //    }
            //}

            //lblActionMode.Text = "";
            onCancelButtonClick();
            disableControls();
            clearControls();
            errValidator.Clear();

            //txtDailyAttendanceDate.Value = DateTime.Today;
            chkCompactDetailedView.Checked = false;
            chkCompactDetailedView.Text = "Detailed View";
            RefreshEmpAttendanceInfo("Compact View");
        }

        public void onModifyButtonClick()
        {
            //lblActionMode.Text = "modify";
            //lblReportingManagerID.Text = "";
            //btnReportingManagerSearch.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onSaveButtonClick()
        {
            //lblReportingManagerID.Text = "";
            //lblActionMode.Text = "";
            //lblReportingManagerID.Text = "";
            //btnReportingManagerSearch.Enabled = false;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onCancelButtonClick()
        {
            //lblActionMode.Text = "";
            //lblReportingManagerID.Text = "";
            //btnReportingManagerSearch.Enabled = false;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void clearControls()
        {

        }

        public void enableControls()
        {
            //txtRepEmpCode.Enabled = false;
            //txtRepEmpName.Enabled = false;
            //txtRepEmpDesig.Enabled = false;
            //txtRepEmpDepartment.Enabled = false;
            //txtRepEmpContactNumber.Enabled = false;
            //btnReportingManagerSearch.Enabled = true;
            //cmbMonthNameList.Enabled = true;
        }

        public void disableControls()
        {
            //btnReportingManagerSearch.Enabled = false;
            //txtRepEmpCode.Enabled = false;
            //txtRepEmpName.Enabled = false;
            //txtRepEmpDesig.Enabled = false;
            //txtRepEmpDepartment.Enabled = false;
            //txtRepEmpContactNumber.Enabled = false;
            //cmbMonthNameList.Enabled = false;
        }

        private void btnModifyDetails_Click(object sender, EventArgs e)
        {
            //LoadMonthNameList();
            onModifyButtonClick();
            enableControls();
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            int insertNewRecordCount = 0;
            int RowCounter = 0;
            int DailyAttendanceID = 0;
            bool FewNotProcessed = false;

            dtgDailyAttendanceUnprocessed.DataSource = null;

            DateTime currSelectedDate = DateTime.Parse(txtDailyAttendanceDate.Value.ToString());

            int MonthlyAttendanceSlNumber = 0;

            if (validateValues())
            {
                lblBatchProcessID.Text = objGenFunc.getMaxRowCount("EmpBatchAttndEntrNotProc", "OrderID").Data.ToString();

                foreach (DataGridViewRow indRow in dtgDailyAttendanceProcess.Rows)
                {
                    if (indRow.Cells["EmpID"].Value.ToString() != "")
                    {
                        EmployeeAttendanceInfo objEmployeeAttendanceInfo = objAttendanceMas.GetEmployeeSpecificDailyAttendanceInfo(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), currSelectedDate);
                        if(objEmployeeAttendanceInfo.AttID != 0)
                        {
                            if(!objEmployeeAttendanceInfo.AttStatus.Contains("Leave"))
                            {
                                List<EmpSpecificWklyOffInfo> objWeeklyOff = objWeeklyOffInfo.getEmployeeSpecificWeeklyOffInfo(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()));

                                EmpSpecificWklyOffInfo objIsWeeklyOff = objWeeklyOff.FirstOrDefault(p => p.WklyOffDay == ((int)currSelectedDate.DayOfWeek + 6) % 7 + 1);
                                if (objIsWeeklyOff == null)
                                {
                                    MonthlyAttendanceSlNumber = objEmpMnthlyAttdInfo.getMonthlyAttendanceInfo(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), currSelectedDate);
                                    if (MonthlyAttendanceSlNumber == 0)
                                    {
                                        RowCounter = objEmpMnthlyAttdInfo.InsertMonthlyAttendanceInfo(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), currSelectedDate);
                                    }
                                    else
                                    {
                                        RowCounter = objEmpMnthlyAttdInfo.UpdateMonthlyAttendanceInfo(MonthlyAttendanceSlNumber, Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), currSelectedDate, "Day" + currSelectedDate.Day, "Present");
                                        RowCounter = objAttendanceInfo.UpdateDailyAttendance(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), Convert.ToDateTime(currSelectedDate.Date.ToString()), "Present", 0);
                                        if (RowCounter == 0)
                                            RowCounter = objAttendanceInfo.InsertDailyAttendance(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), Convert.ToDateTime(currSelectedDate.Date.ToString()), "Present", 0);
                                    }
                                }
                                else
                                {
                                    MonthlyAttendanceSlNumber = objEmpMnthlyAttdInfo.getMonthlyAttendanceInfo(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), currSelectedDate);
                                    RowCounter = objEmpMnthlyAttdInfo.InsertUnprocessedBatchAttendanceEntries(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), Convert.ToInt16(lblBatchProcessID.Text), Convert.ToDateTime(currSelectedDate.ToString("dd-MMM-yyy") + " " + DateTime.Now.ToString("hh:mm:ss tt")), "Its not updated due to WeeklyOff.");
                                    if(RowCounter > 0)
                                        RowCounter = objEmpMnthlyAttdInfo.UpdateMonthlyAttendanceInfo(MonthlyAttendanceSlNumber, Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), currSelectedDate, "Day" + currSelectedDate.Day, objEmployeeAttendanceInfo.AttStatus.ToString());
                                    FewNotProcessed = true;
                                }
                            }
                            else
                            {
                                MonthlyAttendanceSlNumber = objEmpMnthlyAttdInfo.getMonthlyAttendanceInfo(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), currSelectedDate);
                                RowCounter = objEmpMnthlyAttdInfo.InsertUnprocessedBatchAttendanceEntries(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), Convert.ToInt16(lblBatchProcessID.Text), Convert.ToDateTime(currSelectedDate.ToString("dd-MMM-yyy") + " " + DateTime.Now.ToString("hh:mm:ss tt")), "Its not updated due to Leave Request.");
                                if (RowCounter > 0)
                                    RowCounter = objEmpMnthlyAttdInfo.UpdateMonthlyAttendanceInfo(MonthlyAttendanceSlNumber, Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), currSelectedDate, "Day" + currSelectedDate.Day, objEmployeeAttendanceInfo.AttStatus.ToString());
                                FewNotProcessed = true;
                            }
                        }
                        else
                        {
                            if (objEmployeeAttendanceInfo.AttStatus == null)
                            {
                                List<EmpSpecificWklyOffInfo> objWeeklyOff = objWeeklyOffInfo.getEmployeeSpecificWeeklyOffInfo(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()));

                                EmpSpecificWklyOffInfo objIsWeeklyOff = objWeeklyOff.FirstOrDefault(p => p.WklyOffDay == ((int)currSelectedDate.DayOfWeek + 6) % 7 + 1);
                                if (objIsWeeklyOff == null)
                                {
                                    MonthlyAttendanceSlNumber = objEmpMnthlyAttdInfo.getMonthlyAttendanceInfo(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), currSelectedDate);
                                    if (MonthlyAttendanceSlNumber == 0)
                                    {
                                        RowCounter = objEmpMnthlyAttdInfo.InsertMonthlyAttendanceInfo(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), currSelectedDate);
                                    }
                                    else
                                    {
                                        RowCounter = objEmpMnthlyAttdInfo.UpdateMonthlyAttendanceInfo(MonthlyAttendanceSlNumber, Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), currSelectedDate, "Day" + currSelectedDate.Day, "Present");
                                        RowCounter = objAttendanceInfo.UpdateDailyAttendance(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), Convert.ToDateTime(currSelectedDate.Date.ToString()), "Present", 0);
                                        if (RowCounter == 0)
                                            RowCounter = objAttendanceInfo.InsertDailyAttendance(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), Convert.ToDateTime(currSelectedDate.Date.ToString()), "Present", 0);
                                    }
                                }
                                else
                                {
                                    MonthlyAttendanceSlNumber = objEmpMnthlyAttdInfo.getMonthlyAttendanceInfo(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), currSelectedDate);
                                    RowCounter = objEmpMnthlyAttdInfo.InsertUnprocessedBatchAttendanceEntries(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), Convert.ToInt16(lblBatchProcessID.Text), Convert.ToDateTime(currSelectedDate.ToString("dd-MMM-yyy") + " " + DateTime.Now.ToString("hh:mm:ss tt")), "Its not updated due to WeeklyOff.");
                                    FewNotProcessed = true;
                                }
                            }
                            else
                            {
                                MonthlyAttendanceSlNumber = objEmpMnthlyAttdInfo.getMonthlyAttendanceInfo(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), currSelectedDate);
                                RowCounter = objEmpMnthlyAttdInfo.InsertUnprocessedBatchAttendanceEntries(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), Convert.ToInt16(lblBatchProcessID.Text), Convert.ToDateTime(currSelectedDate.ToString("dd-MMM-yyy") + " " + DateTime.Now.ToString("hh:mm:ss tt")), "Its not updated due to Leave Request.");
                                if (RowCounter > 0)
                                    RowCounter = objEmpMnthlyAttdInfo.UpdateMonthlyAttendanceInfo(MonthlyAttendanceSlNumber, Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), currSelectedDate, "Day" + currSelectedDate.Day, objEmployeeAttendanceInfo.AttStatus.ToString());
                                FewNotProcessed = true;
                            }
                        }
                    }
                }

                if (FewNotProcessed == false)
                {
                    MessageBox.Show("Details updated successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("One or more employee's \"Weekly Off\" or \"Leaves\" \nhas prevented their attendance from being processed.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    dtgDailyAttendanceUnprocessed.DataSource = null;
                    dtgDailyAttendanceUnprocessed.DataSource = objEmpMnthlyAttdInfo.GetUnprocessedBatchAttendanceEntries(Convert.ToInt16(lblBatchProcessID.Text));
                    dtgDailyAttendanceUnprocessed.Columns["EmpID"].Visible = false;
                    dtgDailyAttendanceUnprocessed.Columns["BatchAttndEntrNotProcID"].Visible = false;
                    dtgDailyAttendanceUnprocessed.Columns["OrderID"].Visible = false;
                    dtgDailyAttendanceUnprocessed.Columns["BatchNumber"].Visible = false;

                    string filePath = AppVariables.TempFolderPath + @"\Unprocessed Employee Attendance Summary.csv";
                    bool ReportGenerated = Download.DownloadExcel(filePath, dtgDailyAttendanceUnprocessed);
                    if (ReportGenerated)
                        Download.OpenCSV(filePath);
                }
                //txtDailyAttendanceDate.Value = DateTime.Today;
                chkCompactDetailedView.Checked = false;
                chkCompactDetailedView.Text = "Detailed View";
                RefreshEmpAttendanceInfo("Compact View");
            }
        }

        private bool validateValues()
        {
            bool validateStatus = true;

            if (Convert.ToDateTime(txtDailyAttendanceDate.Value) > DateTime.Today)
            {
                MessageBox.Show("Attendance Date should not be greater than today's date", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                validateStatus = false;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("You are about to execute the Daily Attendance Batch Process." + "\n\n" + lblNote.Text + "\n\nDo you want to continue.?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                {
                    validateStatus = false;
                }
            }
            return validateStatus;
        }

        public void SelectedEmployeeID(string SearchOptionSelectedForm, int selectedEmployeeID)
        {

        }

        private void btnReportingManagerSearch_Click(object sender, EventArgs e)
        {
            //frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "DailyAttendanceProcess");
            //frmEmployeeList.ShowDialog();
        }

        private void frmDailyAttendanceProcess_Load(object sender, EventArgs e)
        {
            //onCancelButtonClick();
            //disableControls();
            //clearControls();
        }

        private void cmbMonthNameList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void empAttCalender_DetailedDayClicked(object sender, DetailedDateClickedEventArgs e)
        {
            MessageBox.Show("Detailed Day Clicked: " + e.Date.ToShortDateString() + ", " + e.FillAmount + ", " + e.CustomText, "Calendar Event", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void picViewLeaves_Click(object sender, EventArgs e)
        {
            //if (lblReportingManagerID.Text.Trim() == "")
            //    return;

            //frmViewLeavesOutstanding frmViewLeavesOutstanding = new frmViewLeavesOutstanding(Convert.ToInt16(lblReportingManagerID.Text.ToString()), Convert.ToInt16(lblLeaveMasID.Text.ToString()));
            //frmViewLeavesOutstanding.ShowDialog(this);
        }

        private void frmDailyAttendanceProcess_KeyDown(object sender, KeyEventArgs e)
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

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void txtDailyAttendanceDate_ValueChanged(object sender, EventArgs e)
        {
            //RefreshEmpAttendanceInfo(chkCompactDetailedView.Text);
            chkCompactDetailedView.Checked = false;
            chkCompactDetailedView.Text = "Detailed View";
            RefreshEmpAttendanceInfo("Compact View");
            btnSaveDetails.Enabled = true;
        }

        private void RefreshEmpAttendanceInfo(string ViewMode)
        {
            int PresentCounter = 0;
            int LeaveCounter = 0;
            int FirstHalfLeaveCounter = 0;
            int SecondHalfLeaveCounter = 0;

            dtgDailyAttendanceProcess.DataSource = null;

            DateTime dtSelectedMonth = DateTime.Parse(txtDailyAttendanceDate.Value.ToString());
            if (ViewMode == "Detailed View")
            {
                dtSelectedMonth = Convert.ToDateTime("01" + Convert.ToDateTime(dtSelectedMonth).Date.ToString("-MMM-yyyy"));
                dtgDailyAttendanceProcess.DataSource = objAttendanceMas.MonthlyAttendanceReport(Convert.ToDateTime("01" + Convert.ToDateTime(dtSelectedMonth).Date.ToString("-MMM-yyyy")));
            }
            else
            {
                dtSelectedMonth = DateTime.Parse(txtDailyAttendanceDate.Value.ToString());
                dtgDailyAttendanceProcess.DataSource = objAttendanceMas.DailyBatchAttendance(Convert.ToInt16(objTempClientFinYearInfo.ClientID), Convert.ToDateTime("01" + Convert.ToDateTime(dtSelectedMonth).Date.ToString("-MMM-yyyy")), "Day" + dtSelectedMonth.Day);
            }

            dtgDailyAttendanceProcess.Columns["ClientID"].Visible = false;
            dtgDailyAttendanceProcess.Columns["ClientID"].ReadOnly = true;
            dtgDailyAttendanceProcess.Columns["EmpID"].Visible = false;
            dtgDailyAttendanceProcess.Columns["EmpID"].ReadOnly = true;
            dtgDailyAttendanceProcess.Columns["EmpCode"].Width = 100;
            dtgDailyAttendanceProcess.Columns["EmpName"].Width = 250;
            dtgDailyAttendanceProcess.Columns["DesignationTitle"].Width = 250;
            dtgDailyAttendanceProcess.Columns["DepartmentTitle"].Width = 250;
            dtgDailyAttendanceProcess.Columns["DesignationTitle"].ReadOnly = true;
            dtgDailyAttendanceProcess.Columns["DepartmentTitle"].ReadOnly = true;
            dtgDailyAttendanceProcess.Columns["SlNo"].ReadOnly = true;
            dtgDailyAttendanceProcess.Columns["SlNo"].Visible = false;

            foreach (DataGridViewColumn indColumn in dtgDailyAttendanceProcess.Columns)
            {
                if (indColumn.Name == "ClientID" || indColumn.Name == "EmpID" || indColumn.Name == "EmpCode" || indColumn.Name == "EmpName" || indColumn.Name == "DesignationTitle" || indColumn.Name == "DepartmentTitle" || indColumn.Name == "SlNo")
                {
                    continue;
                }

                if (ViewMode == "Detailed View")
                {
                    dtgDailyAttendanceProcess.Columns[indColumn.Name].Visible = true;
                    dtgDailyAttendanceProcess.Columns[indColumn.Name].ReadOnly = true;
                    dtgDailyAttendanceProcess.Columns["AttdMonth"].Visible = false;
                    dtgDailyAttendanceProcess.Columns["Day32"].Visible = false;
                }
                else if (ViewMode == "Compact View")
                {
                    if (indColumn.Name.ToString() == "Day" + dtSelectedMonth.Day)
                    {
                        dtgDailyAttendanceProcess.Columns[indColumn.Name].Visible = true;
                        dtgDailyAttendanceProcess.Columns[indColumn.Name].ReadOnly = true;
                    }
                    else
                    {
                        dtgDailyAttendanceProcess.Columns[indColumn.Name].Visible = false;
                        dtgDailyAttendanceProcess.Columns[indColumn.Name].ReadOnly = true;
                    }
                    dtgDailyAttendanceProcess.Columns["Day32"].Visible = false;
                }
            }
        }

        private void dtgDailyAttendanceProcess_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            frmIndEmpAttendanceCalender frmIndEmpAttendanceCalender = new frmIndEmpAttendanceCalender(objTempCurrentlyLoggedInUserInfo, objTempClientFinYearInfo, Convert.ToInt16(dtgDailyAttendanceProcess.Rows[e.RowIndex].Cells["EmpID"].Value.ToString()), Convert.ToDateTime(txtDailyAttendanceDate.Value));
            frmIndEmpAttendanceCalender.ShowDialog();
        }

        private void chkCompactDetailedView_Click(object sender, EventArgs e)
        {
            if (chkCompactDetailedView.Checked)
            {
                chkCompactDetailedView.Text = "Compact View";
                RefreshEmpAttendanceInfo("Detailed View");
                btnSaveDetails.Enabled = false;
            }
            else
            {
                chkCompactDetailedView.Text = "Detailed View";
                RefreshEmpAttendanceInfo("Compact View");
                btnSaveDetails.Enabled = true;
            }
        }

        private void frmDailyAttendanceProcess_Activated(object sender, EventArgs e)
        {
            dtgDailyAttendanceProcess.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
        }

        private void btnAttCalender_Click(object sender, EventArgs e)
        {

        }
    }
}
