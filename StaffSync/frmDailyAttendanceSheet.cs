using Krypton.Toolkit;
using ModelStaffSync;
using StaffSync.Controls;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StaffSync
{
    public partial class frmDailyAttendanceSheet : Form
    {
        DALStaffSync.clsLeaveTRList objLeaveTRList = new DALStaffSync.clsLeaveTRList();
        DALStaffSync.clsWeeklyOffInfo objWeeklyOffInfo = new DALStaffSync.clsWeeklyOffInfo();
        DALStaffSync.clsAttendanceMas objAttendanceInfo = new DALStaffSync.clsAttendanceMas();
        DALStaffSync.clsEmpLeaveEntitlementInfo objEmpLeaveEntitlementInfo = new DALStaffSync.clsEmpLeaveEntitlementInfo();
        DALStaffSync.clsAttendanceMas objAttendanceMas = new DALStaffSync.clsAttendanceMas();
        List<WklyOffProfileDetailsInfo> lstWeeklyOffDetailsInfo = new List<WklyOffProfileDetailsInfo>();

        private void Control_CellValueChangedCustom(object sender, CellValueChangedEventArgs e)
        {
            // Fires immediately when dropdown changes
            //MessageBox.Show($"RowID: {e.Change.RowIndex}, Column: {e.Change.ColumnIndex}, ColumnName: {e.Change.ColumnName}, OldValue: {e.Change.OldValue}, NewValue: {e.Change.NewValue}");
        }

        public frmDailyAttendanceSheet()
        {
            InitializeComponent();

            attendanceGridControl1.CellValueChangedCustom += Control_CellValueChangedCustom;
        }

        public frmDailyAttendanceSheet(int txtEmployeeID, int txtLeaveMasID)
        {
            InitializeComponent();     
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDailyAttendanceSheet_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'staffsyncDBDataSet1.qryAllEmpLeavePendingStatement' table. You can move, or remove it, as needed.
            //this.qryAllEmpLeavePendingStatementTableAdapter.Fill(this.staffsyncDBDataSet1.qryAllEmpLeavePendingStatement);
            //// TODO: This line of code loads data into the 'staffsyncDBDTSet.EmpMasInfo' table. You can move, or remove it, as needed.
            //this.empMasInfoTableAdapter.Fill(this.staffsyncDBDTSet.EmpMasInfo);
            onCancelButtonClick();
            disableControls();
            clearControls();

            LoadSalaryMonthList();
            LoadAttendanceInfo();
        }

        public void LoadSalaryMonthList()
        {
            cmbAttendanceMonth.Items.Clear();

            List<string> last6Months = new List<string>();
            DateTime currentMonth = DateTime.Now;

            for (int i = 0; i < 12; i++)
            {
                cmbAttendanceMonth.Items.Add(CultureInfo.CurrentUICulture.DateTimeFormat.MonthNames[i].ToString().Substring(0, 3) + " - " + DateTime.Today.Year.ToString());
            }
            cmbAttendanceMonth.SelectedIndex = DateTime.Today.Month - 1; // Set the default selected month to the current month 
        }

        public void LoadAttendanceInfo()
        {
            //List<MonthlyAttendanceInfo> objMonthlyAttendanceReport = objAttendanceMas.MonthlyAttendanceReport(DateTime.Today);
            //attendanceGridControl1.SetColumnAlignment("SlNo", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnEditable("SlNo", false);
            //attendanceGridControl1.SetColumnAlignment("EmployeeCode", CellTextAlignment.Left);
            //attendanceGridControl1.SetColumnEditable("EmployeeCode", false);
            //attendanceGridControl1.SetColumnAlignment("EmployeeName", CellTextAlignment.Left);
            //attendanceGridControl1.SetColumnEditable("EmployeeName", false);

            List<AttendanceRecord> dtAttendanceRecord = new List<AttendanceRecord>();

            //// Add columns
            attendanceGridControl1.AddColumns("SlNo", "Employee Code", "Employee Name");
            attendanceGridControl1.DayNameFormat = StaffSync.Controls.DayNameFormat.None;
            attendanceGridControl1.SetDisplayMonth(cmbAttendanceMonth.Text.ToString().Substring(0, cmbAttendanceMonth.Text.ToString().IndexOf("-") - 1));    // August
            attendanceGridControl1.AddDayColumns(DateTime.DaysInMonth(DateTime.Now.Year, cmbAttendanceMonth.SelectedIndex + 1));
            attendanceGridControl1.DisplayYear = DateTime.Now.Year;
            attendanceGridControl1.AllowFutureDates = false;
            attendanceGridControl1.WeeklyOffs = new[] { DayOfWeek.Sunday };
            attendanceGridControl1.WeeklyOffsAreReadOnly = true;
            attendanceGridControl1.DateHeaderAlignment = DateHeaderAlignment.Center;
            attendanceGridControl1.OptionsList = new[] { "P", "P/L", "L/P", "L", "A", "WFH", "CompOff", "OOD" };
            attendanceGridControl1.CellTextAlignment = CellTextAlignment.Left;

            List<MonthlyAttendanceInfo> objMonthlyAttendanceReport = objAttendanceMas.MonthlyAttendanceReport(Convert.ToDateTime("01-" + (cmbAttendanceMonth.SelectedIndex + 1) + "-" + DateTime.Today.Year));

            foreach (MonthlyAttendanceInfo indInfo in objMonthlyAttendanceReport)
            {
                dtAttendanceRecord.Add(new AttendanceRecord
                {
                    SlNo = indInfo.SlNo,
                    EmployeeCode = indInfo.EmpCode,
                    EmployeeName = indInfo.EmpName,
                    Day1 = indInfo.Day1,
                    Day2 = indInfo.Day2,
                    Day3 = indInfo.Day3,
                    Day4 = indInfo.Day4,
                    Day5 = indInfo.Day5,
                    Day6 = indInfo.Day6,
                    Day7 = indInfo.Day7,
                    Day8 = indInfo.Day8,
                    Day9 = indInfo.Day9,
                    Day10 = indInfo.Day10,
                    Day11 = indInfo.Day11,
                    Day12 = indInfo.Day12,
                    Day13 = indInfo.Day13,
                    Day14 = indInfo.Day14,
                    Day15 = indInfo.Day15,
                    Day16 = indInfo.Day16,
                    Day17 = indInfo.Day17,
                    Day18 = indInfo.Day18,
                    Day19 = indInfo.Day19,
                    Day20 = indInfo.Day20,
                    Day21 = indInfo.Day21,
                    Day22 = indInfo.Day22,
                    Day23 = indInfo.Day23,
                    Day24 = indInfo.Day24,
                    Day25 = indInfo.Day25,
                    Day26 = indInfo.Day26,
                    Day27 = indInfo.Day27,
                    Day28 = indInfo.Day28,
                    Day29 = indInfo.Day29,
                    Day30 = indInfo.Day30,
                    Day31 = indInfo.Day31
                    //DayStatus = indInfo.Day1.ToDictionary(k => k.Key, v => v.Value)
                });
            }

            attendanceGridControl1.BindData(dtAttendanceRecord);

            attendanceGridControl1.DayNameFormat = StaffSync.Controls.DayNameFormat.None;
            attendanceGridControl1.SetColumnEditable("SlNo", false);
            attendanceGridControl1.SetColumnEditable("EmployeeCode", false);
            attendanceGridControl1.SetColumnEditable("EmployeeName", false);
            attendanceGridControl1.SetDisplayMonth(cmbAttendanceMonth.Text.ToString().Substring(0, cmbAttendanceMonth.Text.ToString().IndexOf("-") - 1));    // August
            attendanceGridControl1.DisplayYear = DateTime.Now.Year;
            attendanceGridControl1.AllowFutureDates = false;
            attendanceGridControl1.WeeklyOffs = new[] { DayOfWeek.Sunday };
            attendanceGridControl1.WeeklyOffsAreReadOnly = true;
            attendanceGridControl1.DateHeaderAlignment = DateHeaderAlignment.Center;
            attendanceGridControl1.OptionsList = new[] { "P", "P/L", "L/P", "L", "A", "WFH", "CompOff", "OOD" };
            attendanceGridControl1.CellTextAlignment = CellTextAlignment.Left;

            attendanceGridControl1.SetColumnAlignment("Day1", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day2", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day3", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day4", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day5", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day6", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day7", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day8", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day9", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day10", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day11", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day12", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day13", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day14", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day15", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day16", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day17", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day18", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day19", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day20", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day21", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day22", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day23", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day24", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day25", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day26", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day27", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day28", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day29", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day30", CellTextAlignment.Center);
            attendanceGridControl1.SetColumnAlignment("Day31", CellTextAlignment.Center);

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
            onGenerateButtonClick();
            clearControls();
            enableControls();
            errValidator.Clear();
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            onSaveButtonClick();
            disableControls();
            clearControls();
            //FormatTheGrid();
            errValidator.Clear();          
        }

        public void clearControls()
        {
            //FormatTheGrid();
        }

        public void enableControls()
        {
            //DailyAttendanceSheet.Enabled = true;
        }

        public void disableControls()
        {
            //txtLeaveApprovalDate.Enabled = false;
            //dtgBulkLeaveApproval.Enabled = false;
        }

        public void onGenerateButtonClick()
        {
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = false;
            btnCancel.Enabled = true;
        }

        public void onModifyButtonClick()
        {
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = false;
            btnCancel.Enabled = true;
        }

        public void onRemoveButtonClick()
        {
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onSaveButtonClick()
        {
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onCancelButtonClick()
        {
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void displaySelectedValuesOnUI(WklyOffProfileMasInfo WklyOffProfileMasInfoModel)
        {
            //FormatTheGrid();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            onCancelButtonClick();
            disableControls();
            clearControls();
            //FormatTheGrid();
            errValidator.Clear();
        }

        private void btnModifyDetails_Click(object sender, EventArgs e)
        {
            onModifyButtonClick();
            clearControls();
            enableControls();
            errValidator.Clear();
        }

        private void btnRemoveDetails_Click(object sender, EventArgs e)
        {

            attendanceGridControl1.ExportToPDF("DailyAttendanceSheet.pdf");
            attendanceGridControl1.ExportToExcel("DailyAttendanceSheet.xlsx");
            attendanceGridControl1.ExportToCSV("DailyAttendanceSheet.csv");
            attendanceGridControl1.ExportToXML("DailyAttendanceSheet.xml");

            //var changes = attendanceGridControl1.GetChangedCells().OrderBy(x => x.RowIndex);
            //foreach (var xx in changes)
            //{
            //    MessageBox.Show($"RowID: {xx.RowIndex}, RowKey: {xx.RowKey}, Column: {xx.ColumnIndex}, ColumnName: {xx.ColumnName}, OldValue: {xx.OldValue}, NewValue: {xx.NewValue}");
            //}
        }

        private void dtgSalaryProfileDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dtgSalaryProfileDetails_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
        }

        private void txtLeaveApprovalDate_TextChanged(object sender, EventArgs e)
        {
            //FormatTheGrid();
        }

        private void dtgBulkLeaveApproval_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgBulkLeaveApproval_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void btnSelectUnselect_Click(object sender, EventArgs e)
        {

        }

        private void chkSelectUnselect_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void dtgRejectionLeaveList_DoubleClick(object sender, EventArgs e)
        {
            //MessageBox.Show(dtgRejectionLeaveList.SelectedRows[0].Cells["EmpID"].Value.ToString(), "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmbAttendanceMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAttendanceInfo();
            //attendanceGridControl1.SetCellValue(1, 15, "P"); // row 1, column 3 → "P"

        }

        private void attendanceGridControl1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("Double Clicked on the grid", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
