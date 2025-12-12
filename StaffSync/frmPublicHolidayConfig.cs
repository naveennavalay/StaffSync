using DocumentFormat.OpenXml.Drawing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Krypton.Toolkit;
using ModelStaffSync;
using Org.BouncyCastle.Ocsp;
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static StaffSync.PDFComponent;
using static StaffSync.PDFComponent.SimplePdfGenerator;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StaffSync
{
    public partial class frmPublicHolidayConfig : Form
    {
        DALStaffSync.clsClientInfo objClientInfo = new DALStaffSync.clsClientInfo();
        DALStaffSync.clsFinYearMas objFinYearMas = new DALStaffSync.clsFinYearMas();
        DALStaffSync.clsPublicHolidayInfo objPublicHolidayInfo = new DALStaffSync.clsPublicHolidayInfo();
        frmDashboard objDashboard = (frmDashboard)System.Windows.Forms.Application.OpenForms["frmDashboard"];
        List<WklyOffProfileDetailsInfo> lstWeeklyOffDetailsInfo = new List<WklyOffProfileDetailsInfo>();
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();
        List<ClientInfo> objActiveClientInfo = new List<ClientInfo>();

        private void Control_CellValueChangedCustom(object sender, CellValueChangedEventArgs e)
        {
            // Fires immediately when dropdown changes
            //MessageBox.Show($"RowID: {e.Change.RowIndex}, Column: {e.Change.ColumnIndex}, ColumnName: {e.Change.ColumnName}, OldValue: {e.Change.OldValue}, NewValue: {e.Change.NewValue}");
        }

        public frmPublicHolidayConfig()
        {
            InitializeComponent();

            //attendanceGridControl1.CellValueChangedCustom += Control_CellValueChangedCustom;
        }

        public frmPublicHolidayConfig(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objActiveClientInfo = objClientInfo.getClientInfoByEmpID(objTempCurrentlyLoggedInUserInfo.EmpID);

            cmbYearlyPublicHoliday.DataSource = objFinYearMas.GetCompleteFinYearList();
            cmbYearlyPublicHoliday.DisplayMember = "FinYearFromTo";
            cmbYearlyPublicHoliday.ValueMember = "FinYearID";

            if (cmbYearlyPublicHoliday.Items.Count > 0)
                cmbYearlyPublicHoliday.SelectedIndex = 0;

            objPublicHolidayInfo.GetHolidayDetailsInfo(cmbYearlyPublicHoliday.SelectedIndex + 2);
        }

        public frmPublicHolidayConfig(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
            objActiveClientInfo = objClientInfo.getClientInfoByEmpID(objTempCurrentlyLoggedInUserInfo.EmpID);

            cmbYearlyPublicHoliday.DataSource = objFinYearMas.GetCompleteFinYearList();
            cmbYearlyPublicHoliday.DisplayMember = "FinYearFromTo";
            cmbYearlyPublicHoliday.ValueMember = "FinYearID";

            if (cmbYearlyPublicHoliday.Items.Count > 0)
                cmbYearlyPublicHoliday.SelectedIndex = 0;

            objPublicHolidayInfo.GetHolidayDetailsInfo(cmbYearlyPublicHoliday.SelectedIndex + 2);
        }

        public frmPublicHolidayConfig(int txtEmployeeID, int txtLeaveMasID)
        {
            InitializeComponent();     
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            objDashboard.lblDashboardTitle.Text = "Dashboard";
            this.Close();
        }

        private void frmPublicHolidayConfig_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'staffsyncDBDataSet1.qryAllEmpLeavePendingStatement' table. You can move, or remove it, as needed.
            //this.qryAllEmpLeavePendingStatementTableAdapter.Fill(this.staffsyncDBDataSet1.qryAllEmpLeavePendingStatement);
            //// TODO: This line of code loads data into the 'staffsyncDBDTSet.EmpMasInfo' table. You can move, or remove it, as needed.
            //this.empMasInfoTableAdapter.Fill(this.staffsyncDBDTSet.EmpMasInfo);
            onCancelButtonClick();
            disableControls();
            clearControls();
        }

        private void btnCloseMe_Click_1(object sender, EventArgs e)
        {
            this.Close();
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
            btnExportData.Enabled = true;
            btnRemoveDetails.Enabled = false;
            btnCancel.Enabled = true;
        }

        public void onModifyButtonClick()
        {
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = true;
            btnExportData.Enabled = true;
            btnRemoveDetails.Enabled = false;
            btnCancel.Enabled = true;
        }

        public void onRemoveButtonClick()
        {
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = false;
            btnExportData.Enabled = true;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onSaveButtonClick()
        {
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnExportData.Enabled = true;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onCancelButtonClick()
        {
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnExportData.Enabled = true;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
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
            //attendanceGridControl1.ExportToPDF("DailyAttendanceSheet.pdf");
            //attendanceGridControl1.ExportToExcel("DailyAttendanceSheet.xlsx");
            //attendanceGridControl1.ExportToCSV("DailyAttendanceSheet.csv");
            //attendanceGridControl1.ExportToXML("DailyAttendanceSheet.xml");

            //var changes = attendanceGridControl1.GetChangedCells().OrderBy(x => x.RowIndex);
            //foreach (var xx in changes)
            //{
            //    MessageBox.Show($"RowID: {xx.RowIndex}, RowKey: {xx.RowKey}, Column: {xx.ColumnIndex}, ColumnName: {xx.ColumnName}, OldValue: {xx.OldValue}, NewValue: {xx.NewValue}");
            //}
        }

        private void cmbAttendanceMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void attendanceGridControl1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void RefreshPublicHolidayList()
        {
            dtgConsolidatedAttendanceReport.EditMode = DataGridViewEditMode.EditProgrammatically;
            dtgConsolidatedAttendanceReport.DataSource = null;
            dtgConsolidatedAttendanceReport.DataSource = objPublicHolidayInfo.GetHolidayDetailsInfo(cmbYearlyPublicHoliday.SelectedIndex + 2);
            dtgConsolidatedAttendanceReport.Columns["PubHolDetID"].Visible = false;
            dtgConsolidatedAttendanceReport.Columns["PubHolMasID"].Visible = false;
            dtgConsolidatedAttendanceReport.Columns["PubHolidayTitle"].HeaderText = "Holiday Name";
            dtgConsolidatedAttendanceReport.Columns["PubHolidayTitle"].Width = 250;
            dtgConsolidatedAttendanceReport.Columns["PubHolidayTitle"].ReadOnly = true;
            dtgConsolidatedAttendanceReport.Columns["PubHolDate"].HeaderText = "Date";
            dtgConsolidatedAttendanceReport.Columns["PubHolDate"].ReadOnly = true;
            dtgConsolidatedAttendanceReport.Columns["PubHolDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgConsolidatedAttendanceReport.Columns["PubHolDate"].Width = 100;
            dtgConsolidatedAttendanceReport.Columns["OrderID"].Visible = false;
            dtgConsolidatedAttendanceReport.Columns["DayName"].HeaderText = "Day Name";
            dtgConsolidatedAttendanceReport.Columns["DayName"].Visible = true;
            dtgConsolidatedAttendanceReport.Columns["DayName"].ReadOnly = true;
            dtgConsolidatedAttendanceReport.Columns["DayName"].Width = 150;
        }

        private void frmPublicHolidayConfig_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                objDashboard.lblDashboardTitle.Text = "Dashboard";
                this.Close();
            }

        }

        private void cmbYearlyPublicHoliday_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbYearlyPublicHoliday.SelectedIndex >= 0)
            {
                chkCompactDetailedView.Checked = false;
                chkCompactDetailedView.Text = "Detailed View";
                RefreshPublicHolidayList();
            }
        }

        private void dtgConsolidatedAttendanceReport_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (chkCompactDetailedView.Checked)
                return;

            bool boolSetDefaultDate = false;

            PublicHolidayInfo objPublicHolidayInfo = new PublicHolidayInfo();
            objPublicHolidayInfo.PubHolDetID = (int)dtgConsolidatedAttendanceReport.Rows[e.RowIndex].Cells["PubHolDetID"].Value;
            objPublicHolidayInfo.PubHolMasID = (int)dtgConsolidatedAttendanceReport.Rows[e.RowIndex].Cells["PubHolMasID"].Value;
            objPublicHolidayInfo.PubHolidayTitle = dtgConsolidatedAttendanceReport.Rows[e.RowIndex].Cells["PubHolidayTitle"].Value.ToString();
            objPublicHolidayInfo.PubHolDate = Convert.ToDateTime(dtgConsolidatedAttendanceReport.Rows[e.RowIndex].Cells["PubHolDate"].Value);
            objPublicHolidayInfo.OrderID = dtgConsolidatedAttendanceReport.Rows[e.RowIndex].Cells["OrderID"].Value == null
                ? (int?)null : int.TryParse(dtgConsolidatedAttendanceReport.Rows[e.RowIndex].Cells["OrderID"].Value.ToString(), out var OrderID) ? OrderID : (int?)null;
            if (objPublicHolidayInfo.PubHolDate.Value.ToString("dd-MM-yyyy").Equals("01-01-0001"))
            {
                objPublicHolidayInfo.PubHolDate = DateTime.Today;
                boolSetDefaultDate = true;
            }

            frmPublicHolidayConfigPopup frmPublicHolidayConfigPopup = new frmPublicHolidayConfigPopup(objPublicHolidayInfo);
            frmPublicHolidayConfigPopup.ShowDialog(this);
            objPublicHolidayInfo = frmPublicHolidayConfigPopup.objSaveTheseValues;

            dtgConsolidatedAttendanceReport.Rows[e.RowIndex].Cells["PubHolDetID"].Value = (int)objPublicHolidayInfo.PubHolDetID;
            dtgConsolidatedAttendanceReport.Rows[e.RowIndex].Cells["PubHolMasID"].Value = (int)objPublicHolidayInfo.PubHolMasID;
            dtgConsolidatedAttendanceReport.Rows[e.RowIndex].Cells["PubHolidayTitle"].Value = objPublicHolidayInfo.PubHolidayTitle;
            dtgConsolidatedAttendanceReport.Rows[e.RowIndex].Cells["PubHolDate"].Value = objPublicHolidayInfo.PubHolDate;
            if (boolSetDefaultDate && objPublicHolidayInfo.PubHolDate.Value.ToString("dd-MM-yyyy").Equals(DateTime.Today.ToString("dd-MM-yyyy")))
            {
                dtgConsolidatedAttendanceReport.Rows[e.RowIndex].Cells["PubHolDate"].Value = "";
            }

            RefreshPublicHolidayList();
        }

        private void chkCompactDetailedView_Click(object sender, EventArgs e)
        {
            if (chkCompactDetailedView.Checked)
            {
                chkCompactDetailedView.Text = "Compact View";
                LoadCalendarGrid();
            }
            else
            {
                chkCompactDetailedView.Text = "Detailed View";
                RefreshPublicHolidayList();
            }
        }

        private DataTable BuildCalendarTable()
        {
            List<PublicHolidayInfo> lstPublicHolidayList = objPublicHolidayInfo.GetHolidayDetailsInfo(cmbYearlyPublicHoliday.SelectedIndex + 2);

            DataTable dt = new DataTable();
            dt.Columns.Add("MonthName", typeof(string));

            // Add 31 day columns (even if some months have less)
            for (int d = 1; d <= 31; d++)
                dt.Columns.Add($"Day{d}", typeof(string));

            string[] months = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };

            for (int m = 1; m <= 12; m++)
            {
                DataRow row = dt.NewRow();
                row["MonthName"] = months[m - 1];

                int days = DateTime.DaysInMonth(DateTime.Now.Year, m);

                // Optional: fill values if required
                for (int d = 1; d <= days; d++)
                {
                    var date = new DateTime(DateTime.Now.Year, m, d).Date;
                    var IsHoliday = lstPublicHolidayList.FirstOrDefault(x => x.PubHolDate.HasValue && x.PubHolDate.Value.Date == date);
                    if (IsHoliday != null)
                    {
                        row[$"Day{d}"] = IsHoliday.PubHolidayTitle;
                    }
                    else
                        row[$"Day{d}"] = "";
                }

                dt.Rows.Add(row);
            }

            return dt;
        }

        private void LoadCalendarGrid()
        {
            var dt = BuildCalendarTable();
            dtgConsolidatedAttendanceReport.DataSource = dt;

            dtgConsolidatedAttendanceReport.EditMode = DataGridViewEditMode.EditProgrammatically;
            //dtgConsolidatedAttendanceReport.Columns["MonthName"].Frozen = true;

            // Style the columns
            for (int d = 1; d <= 31; d++)
            {
                var col = dtgConsolidatedAttendanceReport.Columns[$"Day{d}"];
                col.HeaderText = d.ToString();
                col.Width = 35;  // tile width
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            foreach (DataGridViewRow dc in dtgConsolidatedAttendanceReport.Rows)
            {
                foreach (DataGridViewCell cell in dc.Cells)
                {
                    if (cell.ColumnIndex == 0)
                        continue;

                    if (cell.Value == null || string.IsNullOrEmpty(cell.Value.ToString()))
                        continue;

                    if (cell.Value.ToString() != "")
                    {
                        cell.Style.BackColor = Color.Red;
                        cell.Style.ForeColor = Color.White;
                    }
                    cell.ReadOnly = true;
                }
            }

            foreach (DataGridViewColumn dc in dtgConsolidatedAttendanceReport.Columns)
            {
                dc.Width = 150;
            }


        }

        private void dtgConsolidatedAttendanceReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void btnExportData_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV Files (*.csv)|*.csv";
                if (chkCompactDetailedView.Checked)
                    sfd.FileName = "Public Holiday Year View.csv";
                else
                    sfd.FileName = "Public Holiday Compact View.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    bool ReportGenerated = Download.DownloadExcel(sfd.FileName, dtgConsolidatedAttendanceReport);
                    if (ReportGenerated)
                        Download.OpenCSV(sfd.FileName);
                }
            }
        }

    }
}
