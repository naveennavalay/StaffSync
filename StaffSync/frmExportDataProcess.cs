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
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static StaffSync.PDFComponent;
using static StaffSync.PDFComponent.SimplePdfGenerator;
using static System.Windows.Forms.MonthCalendar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StaffSync
{
    public partial class frmExportDataProcess : Form
    {
        DALStaffSync.clsClientInfo objClientInfo = new DALStaffSync.clsClientInfo();
        DALStaffSync.clsClientStatutory objClientStatutory = new DALStaffSync.clsClientStatutory();
        DALStaffSync.clsClientBranchInfo clsClientBranchInfo = new DALStaffSync.clsClientBranchInfo();
        clsImpageOperation objImpageOperation = new clsImpageOperation();
        DALStaffSync.clsPhotoMas objPhotoMas = new DALStaffSync.clsPhotoMas();
        DALStaffSync.clsImportDataInfo objImportDataInfo = new DALStaffSync.clsImportDataInfo();
        DALStaffSync.clsDesignation objDesignation = new DALStaffSync.clsDesignation();
        DALStaffSync.clsDepartment objDepartment = new DALStaffSync.clsDepartment();
        DALStaffSync.clsCountries objCountries = new DALStaffSync.clsCountries();
        DALStaffSync.clsStates objStates = new DALStaffSync.clsStates();
        DALStaffSync.clsEduQalification objEduQalification = new DALStaffSync.clsEduQalification();
        DALStaffSync.clsSkillsMas objSkillsMas = new DALStaffSync.clsSkillsMas();
        DALStaffSync.clsRelationship objRelationship = new DALStaffSync.clsRelationship();
        DALStaffSync.clsShiftMas objShiftMas = new DALStaffSync.clsShiftMas();
        DALStaffSync.clsEmploymentTypeInfo objEmploymentTypeInfo = new DALStaffSync.clsEmploymentTypeInfo();
        DALStaffSync.clsBankMas objBankMas = new DALStaffSync.clsBankMas();
        DALStaffSync.clsAdvanceTypeMas objAdvanceTypeMas = new DALStaffSync.clsAdvanceTypeMas();
        DALStaffSync.clsAssetsCategory objAssetsCategory = new DALStaffSync.clsAssetsCategory();
        DALStaffSync.clsAssetsInfo objAssetsInfo = new DALStaffSync.clsAssetsInfo();
        DALStaffSync.clsAssetRegister objAssetRegister = new DALStaffSync.clsAssetRegister();
        DALStaffSync.clsLeaveTypeMas objLeaveTypeMas = new DALStaffSync.clsLeaveTypeMas();
        DALStaffSync.clsWeeklyOffInfo objWeeklyOffInfo = new DALStaffSync.clsWeeklyOffInfo();
        DALStaffSync.clsAllowenceInfo objAllowenceInfo = new DALStaffSync.clsAllowenceInfo();
        DALStaffSync.clsDeductionsInfo objDeductionInfo = new DALStaffSync.clsDeductionsInfo();
        DALStaffSync.clsReimbursement objReimbursementInfo = new DALStaffSync.clsReimbursement();

        frmDashboard objDashboard = (frmDashboard)System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        List<ClientInfo> objActiveClientInfo = new List<ClientInfo>();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();

        // Import/Export preview state
        private DataTable objImportDataPreviewTable = new DataTable();
        private string objSelectedImportFilePath = string.Empty;
        private bool isImportPreviewEventsAttached = false;
        private bool isExportPreviewEventsAttached = false;
        // Download template button state
        private bool isDownloadTemplateEventsAttached = false;

        private void Control_CellValueChangedCustom(object sender, CellValueChangedEventArgs e)
        {
            // Fires immediately when dropdown changes
            //MessageBox.Show($"RowID: {e.Change.RowIndex}, Column: {e.Change.ColumnIndex}, ColumnName: {e.Change.ColumnName}, OldValue: {e.Change.OldValue}, NewValue: {e.Change.NewValue}");
        }

        public frmExportDataProcess()
        {
            InitializeComponent();
            dtgImportDataSourceList.DataSource = objImportDataInfo.getImportInfoData();
            formatGrid();
            defaultSelectedImportOption();
            //attendanceGridControl1.CellValueChangedCustom += Control_CellValueChangedCustom;
        }

        public frmExportDataProcess(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objActiveClientInfo = objClientInfo.getClientInfoByEmpID(objTempCurrentlyLoggedInUserInfo.EmpID);
            dtgImportDataSourceList.DataSource = objImportDataInfo.getImportInfoData();
            formatGrid();
            defaultSelectedImportOption();
        }

        public frmExportDataProcess(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
            objActiveClientInfo = objClientInfo.getClientInfoByEmpID(objTempCurrentlyLoggedInUserInfo.EmpID);

            dtgImportDataSourceList.DataSource = objImportDataInfo.getImportInfoData();
            formatGrid();
            defaultSelectedImportOption();
        }

        public frmExportDataProcess(int txtEmployeeID, int txtLeaveMasID)
        {
            InitializeComponent();
            dtgImportDataSourceList.DataSource = objImportDataInfo.getImportInfoData();
            formatGrid();
            defaultSelectedImportOption();
        }

        private void defaultSelectedImportOption()
        {
            try
            {
                if (dtgImportDataSourceList == null ||
                    dtgImportDataSourceList.Rows.Count == 0)
                {
                    ClearExportPreview();
                    return;
                }

                string selectedTitle = GetSelectedImportSourceTitle();

                if (string.IsNullOrWhiteSpace(selectedTitle))
                {
                    selectedTitle = Convert.ToString(
                        dtgImportDataSourceList.Rows[0]
                            .Cells["ImpDataInfoTitle"].Value);
                }

                lblSelectedDataAction.Text =
                    selectedTitle == null
                        ? string.Empty
                        : selectedTitle.Trim();

                // This is an Export form. Do not read an import template here.
                // The selected master data is loaded directly from the database
                // when the user selects the item on the left grid.
                ClearExportPreview();
            }
            catch (Exception ex)
            {
                ClearExportPreview();

                MessageBox.Show(
                    "Unable to initialize Export Data Process." +
                    Environment.NewLine + Environment.NewLine +
                    ex.Message,
                    "Staffsync",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            objDashboard.lblDashboardTitle.Text = "Dashboard";
            objDashboard.sptrDashboardContainer.Visible = true;
            objDashboard.grpDashboardDateRange.Visible = true;
            objDashboard.refreshDashboardCharts();
            this.Close();
        }

        private void frmExportDataProcess_Load(object sender, EventArgs e)
        {
            FocusManager.EnableHighlighting = false;
            FocusManager.ShowNavigationError = true;
            FocusManager.Register(this);
            FocusManager.SetFocus(btnGenerateDetails);

            // TODO: This line of code loads data into the 'staffsyncDBDataSet1.qryAllEmpLeavePendingStatement' table. You can move, or remove it, as needed.
            //this.qryAllEmpLeavePendingStatementTableAdapter.Fill(this.staffsyncDBDataSet1.qryAllEmpLeavePendingStatement);
            //// TODO: This line of code loads data into the 'staffsyncDBDTSet.EmpMasInfo' table. You can move, or remove it, as needed.
            //this.empMasInfoTableAdapter.Fill(this.staffsyncDBDTSet.EmpMasInfo);
            InitializeExportFormats();
            disableControls();
            defaultSelectedImportOption();
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
            //attendanceGridControl1.AddColumns("SlNo", "Employee Code", "Employee Name");
            //attendanceGridControl1.DayNameFormat = StaffSync.Controls.DayNameFormat.None;
            //attendanceGridControl1.SetDisplayMonth(cmbAttendanceMonth.Text.ToString().Substring(0, cmbAttendanceMonth.Text.ToString().IndexOf("-") - 1));    // August
            //attendanceGridControl1.AddDayColumns(DateTime.DaysInMonth(DateTime.Now.Year, cmbAttendanceMonth.SelectedIndex + 1));
            //attendanceGridControl1.DisplayYear = DateTime.Now.Year;
            //attendanceGridControl1.AllowFutureDates = false;
            //attendanceGridControl1.WeeklyOffs = new[] { DayOfWeek.Sunday };
            //attendanceGridControl1.WeeklyOffsAreReadOnly = true;
            //attendanceGridControl1.DateHeaderAlignment = DateHeaderAlignment.Center;
            //attendanceGridControl1.OptionsList = new[] { "P", "P/L", "L/P", "L", "A", "WFH", "CompOff", "OOD" };
            //attendanceGridControl1.CellTextAlignment = CellTextAlignment.Left;

            //List<MonthlyAttendanceInfo> objMonthlyAttendanceReport = objAttendanceMas.MonthlyAttendanceReport(Convert.ToDateTime("01-" + (cmbAttendanceMonth.SelectedIndex + 1) + "-" + DateTime.Today.Year));

            //foreach (MonthlyAttendanceInfo indInfo in objMonthlyAttendanceReport)
            //{
            //    dtAttendanceRecord.Add(new AttendanceRecord
            //    {
            //        SlNo = indInfo.SlNo,
            //        EmployeeCode = indInfo.EmpCode,
            //        EmployeeName = indInfo.EmpName,
            //        Day1 = indInfo.Day1,
            //        Day2 = indInfo.Day2,
            //        Day3 = indInfo.Day3,
            //        Day4 = indInfo.Day4,
            //        Day5 = indInfo.Day5,
            //        Day6 = indInfo.Day6,
            //        Day7 = indInfo.Day7,
            //        Day8 = indInfo.Day8,
            //        Day9 = indInfo.Day9,
            //        Day10 = indInfo.Day10,
            //        Day11 = indInfo.Day11,
            //        Day12 = indInfo.Day12,
            //        Day13 = indInfo.Day13,
            //        Day14 = indInfo.Day14,
            //        Day15 = indInfo.Day15,
            //        Day16 = indInfo.Day16,
            //        Day17 = indInfo.Day17,
            //        Day18 = indInfo.Day18,
            //        Day19 = indInfo.Day19,
            //        Day20 = indInfo.Day20,
            //        Day21 = indInfo.Day21,
            //        Day22 = indInfo.Day22,
            //        Day23 = indInfo.Day23,
            //        Day24 = indInfo.Day24,
            //        Day25 = indInfo.Day25,
            //        Day26 = indInfo.Day26,
            //        Day27 = indInfo.Day27,
            //        Day28 = indInfo.Day28,
            //        Day29 = indInfo.Day29,
            //        Day30 = indInfo.Day30,
            //        Day31 = indInfo.Day31
            //        //DayStatus = indInfo.Day1.ToDictionary(k => k.Key, v => v.Value)
            //    });
            //}

            //attendanceGridControl1.BindData(dtAttendanceRecord);

            //attendanceGridControl1.DayNameFormat = StaffSync.Controls.DayNameFormat.None;
            //attendanceGridControl1.SetColumnEditable("SlNo", false);
            //attendanceGridControl1.SetColumnEditable("EmployeeCode", false);
            //attendanceGridControl1.SetColumnEditable("EmployeeName", false);
            //attendanceGridControl1.SetDisplayMonth(cmbAttendanceMonth.Text.ToString().Substring(0, cmbAttendanceMonth.Text.ToString().IndexOf("-") - 1));    // August
            //attendanceGridControl1.DisplayYear = DateTime.Now.Year;
            //attendanceGridControl1.AllowFutureDates = false;
            //attendanceGridControl1.WeeklyOffs = new[] { DayOfWeek.Sunday };
            //attendanceGridControl1.WeeklyOffsAreReadOnly = true;
            //attendanceGridControl1.DateHeaderAlignment = DateHeaderAlignment.Center;
            //attendanceGridControl1.OptionsList = new[] { "P", "P/L", "L/P", "L", "A", "WFH", "CompOff", "OOD" };
            //attendanceGridControl1.CellTextAlignment = CellTextAlignment.Left;

            //attendanceGridControl1.SetColumnAlignment("Day1", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day2", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day3", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day4", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day5", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day6", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day7", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day8", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day9", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day10", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day11", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day12", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day13", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day14", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day15", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day16", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day17", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day18", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day19", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day20", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day21", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day22", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day23", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day24", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day25", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day26", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day27", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day28", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day29", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day30", CellTextAlignment.Center);
            //attendanceGridControl1.SetColumnAlignment("Day31", CellTextAlignment.Center);

        }


        private void btnCloseMe_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbRelationship_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void formatGrid()
        {
            dtgImportDataSourceList.Columns["ImpDataInfoID"].Visible = false;
            dtgImportDataSourceList.Columns["ImpDataInfoCode"].Visible = false;
            dtgImportDataSourceList.Columns["ImpDataInfoTitle"].HeaderText = "Import Data";
            dtgImportDataSourceList.Columns["ImpDataInfoTitle"].ReadOnly = true;
            dtgImportDataSourceList.Columns["ImpDataInfoTitle"].Width = 225;
            dtgImportDataSourceList.Columns["ImpDataInfoDescription"].Visible = false;
            dtgImportDataSourceList.Columns["ImpDataInfoTemplateName"].ReadOnly = true;
            dtgImportDataSourceList.Columns["ImpDataInfoTemplateName"].Width = 225;
            dtgImportDataSourceList.Columns["ImpDataInfoTemplateName"].Visible = false;
            dtgImportDataSourceList.Columns["IsActive"].Visible = false;
            dtgImportDataSourceList.Columns["IsDeleted"].Visible = false;
            dtgImportDataSourceList.Columns["OrderID"].Visible = false;

            // Download Template button column.
            //AddDownloadTemplateColumn();

            if (lblSelectedDataAction.Text == "Company Information")
            {
                //dtgImportDataPreview.Columns["ClientID"].HeaderText = "Client ID";
                //dtgImportDataPreview.Columns["ClientID"].Visible = false;
                //dtgImportDataPreview.Columns["ClientCode"].HeaderText = "Client Code";
                //dtgImportDataPreview.Columns["ClientCode"].Visible = false;
                //dtgImportDataPreview.Columns["ClientCode"].Width = 150;
                dtgImportDataPreview.Columns["ClientName"].HeaderText = "Client Title";
                dtgImportDataPreview.Columns["ClientName"].Width = 250;
                dtgImportDataPreview.Columns["ClientName"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientAddress1"].HeaderText = "Address 01";
                dtgImportDataPreview.Columns["ClientAddress1"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientAddress1"].Width = 250;
                dtgImportDataPreview.Columns["ClientAddress2"].HeaderText = "Address 02";
                dtgImportDataPreview.Columns["ClientAddress2"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientAddress2"].Width = 250;
                dtgImportDataPreview.Columns["ClientArea"].HeaderText = "Area";
                dtgImportDataPreview.Columns["ClientArea"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientArea"].Width = 100;
                dtgImportDataPreview.Columns["ClientCity"].HeaderText = "City";
                dtgImportDataPreview.Columns["ClientCity"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientCity"].Width = 100;
                dtgImportDataPreview.Columns["ClientState"].HeaderText = "State";
                dtgImportDataPreview.Columns["ClientState"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientState"].Width = 100;
                dtgImportDataPreview.Columns["ClientPIN"].HeaderText = "PIN";
                dtgImportDataPreview.Columns["ClientPIN"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientPIN"].Width = 100;
                dtgImportDataPreview.Columns["ClientCountry"].HeaderText = "Country";
                dtgImportDataPreview.Columns["ClientCountry"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientCountry"].Width = 100;
                dtgImportDataPreview.Columns["ClientPhone"].HeaderText = "Phone";
                dtgImportDataPreview.Columns["ClientPhone"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientPhone"].Width = 100;
                dtgImportDataPreview.Columns["ClientMailID"].HeaderText = "Mail ID";
                dtgImportDataPreview.Columns["ClientMailID"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientMailID"].Width = 250;
                dtgImportDataPreview.Columns["ClientContactPerson"].HeaderText = "Contact Person";
                dtgImportDataPreview.Columns["ClientContactPerson"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientContactPerson"].Width = 250;
                dtgImportDataPreview.Columns["ClientContactNumber"].HeaderText = "Contact Number";
                dtgImportDataPreview.Columns["ClientContactNumber"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientContactNumber"].Width = 100;
                dtgImportDataPreview.Columns["ClientContactMail"].HeaderText = "Contact Mail ID";
                dtgImportDataPreview.Columns["ClientContactMail"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientContactMail"].Width = 250;
                dtgImportDataPreview.Columns["ClientWebSite"].HeaderText = "Client Web Site";
                dtgImportDataPreview.Columns["ClientWebSite"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientWebSite"].Width = 250;
                dtgImportDataPreview.Columns["IsActive"].HeaderText = "Is Active";
                dtgImportDataPreview.Columns["IsDeleted"].HeaderText = "Is Deleted";
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
                dtgImportDataPreview.Columns["FinYearID"].ReadOnly = true;
                dtgImportDataPreview.Columns["FinYearID"].Visible = false;
            }
            else if (lblSelectedDataAction.Text == "Designation Information")
            {
                //dtgImportDataPreview.Columns["DesignationID"].HeaderText = "Designation ID";
                //dtgImportDataPreview.Columns["DesignationID"].Visible = false;
                //dtgImportDataPreview.Columns["DesignationCode"].HeaderText = "Designation Code";
                //dtgImportDataPreview.Columns["DesignationCode"].Visible = false;
                //dtgImportDataPreview.Columns["DesignationCode"].Width = 150;
                dtgImportDataPreview.Columns["DesignationTitle"].HeaderText = "Designation";
                dtgImportDataPreview.Columns["DesignationTitle"].Width = 300;
                dtgImportDataPreview.Columns["DesignationTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["DesignationInitial"].HeaderText = "Initial";
                dtgImportDataPreview.Columns["DesignationInitial"].Width = 200;
                dtgImportDataPreview.Columns["DesignationInitial"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].HeaderText = "Is Active";
                dtgImportDataPreview.Columns["IsDeleted"].HeaderText = "Is Deleted";
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (lblSelectedDataAction.Text == "Department Information")
            {
                //dtgImportDataPreview.Columns["DepartmentID"].HeaderText = "Department ID";
                //dtgImportDataPreview.Columns["DepartmentID"].Visible = false;
                //dtgImportDataPreview.Columns["DepCode"].HeaderText = "Department Code";
                //dtgImportDataPreview.Columns["DepCode"].Visible = false;
                //dtgImportDataPreview.Columns["DepCode"].Width = 150;
                dtgImportDataPreview.Columns["DepartmentTitle"].HeaderText = "Department";
                dtgImportDataPreview.Columns["DepartmentTitle"].Width = 300;
                dtgImportDataPreview.Columns["DepartmentTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["DepartmentInitial"].HeaderText = "Initial";
                dtgImportDataPreview.Columns["DepartmentInitial"].Width = 200;
                dtgImportDataPreview.Columns["DepartmentInitial"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].HeaderText = "Is Active";
                dtgImportDataPreview.Columns["IsDeleted"].HeaderText = "Is Deleted";
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (lblSelectedDataAction.Text == "Countries Information")
            {
                //dtgImportDataPreview.Columns["CountryID"].HeaderText = "Country ID";
                //dtgImportDataPreview.Columns["CountryID"].Visible = false;
                //dtgImportDataPreview.Columns["CountryCode"].HeaderText = "Country Code";
                //dtgImportDataPreview.Columns["CountryCode"].Visible = false;
                //dtgImportDataPreview.Columns["CountryCode"].Width = 150;
                dtgImportDataPreview.Columns["CountryTitle"].HeaderText = "Country";
                dtgImportDataPreview.Columns["CountryTitle"].Width = 300;
                dtgImportDataPreview.Columns["CountryTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["CountryInitial"].HeaderText = "Initial";
                dtgImportDataPreview.Columns["CountryInitial"].Width = 200;
                dtgImportDataPreview.Columns["CountryInitial"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].HeaderText = "Is Active";
                dtgImportDataPreview.Columns["IsDeleted"].HeaderText = "Is Deleted";
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (lblSelectedDataAction.Text == "States Information")
            {
                //dtgImportDataPreview.Columns["StateID"].HeaderText = "State ID";
                //dtgImportDataPreview.Columns["StateID"].Visible = false;
                //dtgImportDataPreview.Columns["StateCode"].HeaderText = "State Code";
                //dtgImportDataPreview.Columns["StateCode"].Visible = false;
                //dtgImportDataPreview.Columns["StateCode"].Width = 150;
                dtgImportDataPreview.Columns["StateTitle"].HeaderText = "State Title";
                dtgImportDataPreview.Columns["StateTitle"].Width = 250;
                dtgImportDataPreview.Columns["StateTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["StateInitial"].HeaderText = "State Initial";
                dtgImportDataPreview.Columns["StateInitial"].ReadOnly = true;
                dtgImportDataPreview.Columns["StateInitial"].Width = 150;
                dtgImportDataPreview.Columns["IsActive"].HeaderText = "Is Active";
                dtgImportDataPreview.Columns["IsDeleted"].HeaderText = "Is Deleted";
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (lblSelectedDataAction.Text == "Education Information")
            {
                //dtgImportDataPreview.Columns["EduQualID"].HeaderText = "Education ID";
                //dtgImportDataPreview.Columns["EduQualID"].Visible = false;
                //dtgImportDataPreview.Columns["EduQualCode"].HeaderText = "Education Code";
                //dtgImportDataPreview.Columns["EduQualCode"].Visible = false;
                //dtgImportDataPreview.Columns["EduQualCode"].Width = 150;
                dtgImportDataPreview.Columns["EduQualTitle"].HeaderText = "Education Title";
                dtgImportDataPreview.Columns["EduQualTitle"].Width = 300;
                dtgImportDataPreview.Columns["EduQualTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["EduQualInitial"].HeaderText = "Education Initial";
                dtgImportDataPreview.Columns["EduQualInitial"].Width = 200;
                dtgImportDataPreview.Columns["EduQualInitial"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].HeaderText = "Is Active";
                dtgImportDataPreview.Columns["IsDeleted"].HeaderText = "Is Deleted";
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (lblSelectedDataAction.Text == "Skills Information")
            {
                //dtgImportDataPreview.Columns["SkillID"].HeaderText = "Skill ID";
                //dtgImportDataPreview.Columns["SkillID"].Visible = false;
                //dtgImportDataPreview.Columns["SkillCode"].HeaderText = "Skill Code";
                //dtgImportDataPreview.Columns["SkillCode"].Visible = false;
                //dtgImportDataPreview.Columns["SkillCode"].Width = 150;
                dtgImportDataPreview.Columns["SkillTitle"].HeaderText = "Skill Title";
                dtgImportDataPreview.Columns["SkillTitle"].Width = 300;
                dtgImportDataPreview.Columns["SkillTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["SkillInitial"].HeaderText = "Skill Initial";
                dtgImportDataPreview.Columns["SkillInitial"].Width = 200;
                dtgImportDataPreview.Columns["SkillInitial"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].HeaderText = "Is Active";
                dtgImportDataPreview.Columns["IsDeleted"].HeaderText = "Is Deleted";
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (lblSelectedDataAction.Text == "Relationship Information")
            {
                //dtgImportDataPreview.Columns["RelationShipID"].HeaderText = "Relationship ID";
                //dtgImportDataPreview.Columns["RelationShipID"].Visible = false;
                //dtgImportDataPreview.Columns["RelationshipCode"].HeaderText = "Relationship Code";
                //dtgImportDataPreview.Columns["RelationshipCode"].Visible = false;
                //dtgImportDataPreview.Columns["RelationshipCode"].Width = 150;
                dtgImportDataPreview.Columns["RelationshipTitle"].HeaderText = "Relationship Title";
                dtgImportDataPreview.Columns["RelationshipTitle"].Width = 300;
                dtgImportDataPreview.Columns["RelationshipTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["RelationshipInitial"].HeaderText = "Relationship Initial";
                dtgImportDataPreview.Columns["RelationshipInitial"].Width = 200;
                dtgImportDataPreview.Columns["RelationshipInitial"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].HeaderText = "Is Active";
                dtgImportDataPreview.Columns["IsDeleted"].HeaderText = "Is Deleted";
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (lblSelectedDataAction.Text == "Weekly Off Information")
            {
                //dtgImportDataPreview.Columns["WklyOffMasID"].HeaderText = "Relationship ID";
                //dtgImportDataPreview.Columns["WklyOffMasID"].Visible = false;
                //dtgImportDataPreview.Columns["WklyOffCode"].HeaderText = "Relationship Code";
                //dtgImportDataPreview.Columns["WklyOffCode"].Visible = false;
                //dtgImportDataPreview.Columns["WklyOffCode"].Width = 150;
                dtgImportDataPreview.Columns["WklyOffTitle"].HeaderText = "Weekly Off Title";
                dtgImportDataPreview.Columns["WklyOffTitle"].Width = 300;
                dtgImportDataPreview.Columns["WklyOffTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["WklyOffDay"].HeaderText = "Weekly Day";
                dtgImportDataPreview.Columns["WklyOffDay"].Width = 300;
                dtgImportDataPreview.Columns["WklyOffDay"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].HeaderText = "Is Active";
                dtgImportDataPreview.Columns["IsDeleted"].HeaderText = "Is Deleted";
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (lblSelectedDataAction.Text == "Asset Category Information")
            {
                //dtgImportDataPreview.Columns["AssetCatMasID"].HeaderText = "Category ID";
                //dtgImportDataPreview.Columns["AssetCatMasID"].Visible = false;
                //dtgImportDataPreview.Columns["AssetCode"].HeaderText = "Category Code";
                //dtgImportDataPreview.Columns["AssetCode"].Visible = false;
                //dtgImportDataPreview.Columns["AssetCode"].Width = 150;
                dtgImportDataPreview.Columns["AssetName"].HeaderText = "Category Title";
                dtgImportDataPreview.Columns["AssetName"].Width = 300;
                dtgImportDataPreview.Columns["AssetName"].ReadOnly = true;
                dtgImportDataPreview.Columns["AssetDescription"].HeaderText = "Category Description";
                dtgImportDataPreview.Columns["AssetDescription"].Width = 200;
                dtgImportDataPreview.Columns["AssetDescription"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].HeaderText = "Is Active";
                dtgImportDataPreview.Columns["IsDeleted"].HeaderText = "Is Deleted";
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
                //dtgImportDataPreview.Columns["AssetNote"].HeaderText = "Skill Initial";
                //dtgImportDataPreview.Columns["AssetNote"].Width = 200;
                //dtgImportDataPreview.Columns["AssetNote"].ReadOnly = true;
                //dtgImportDataPreview.Columns["AssetNote"].Visible = false;
                //dtgImportDataPreview.Columns["ClientID"].Visible = false;
                //dtgImportDataPreview.Columns["ParentAssetCatMasID"].Visible = false;
            }
            else if (lblSelectedDataAction.Text == "Assets Information")
            {
                //dtgImportDataPreview.Columns["AssetID"].HeaderText = "Asset ID";
                //dtgImportDataPreview.Columns["AssetID"].Visible = false;
                //dtgImportDataPreview.Columns["AssetCode"].HeaderText = "Asset Code";
                //dtgImportDataPreview.Columns["AssetCode"].Visible = false;
                //dtgImportDataPreview.Columns["AssetCode"].Width = 150;
                dtgImportDataPreview.Columns["AssetName"].HeaderText = "Asset Name";
                dtgImportDataPreview.Columns["AssetName"].Width = 300;
                dtgImportDataPreview.Columns["AssetName"].ReadOnly = true;
                dtgImportDataPreview.Columns["AssetDescription"].HeaderText = "Asset Description";
                dtgImportDataPreview.Columns["AssetDescription"].Width = 200;
                dtgImportDataPreview.Columns["AssetDescription"].ReadOnly = true;
                dtgImportDataPreview.Columns["ParentCategory"].HeaderText = "Category";
                dtgImportDataPreview.Columns["ParentCategory"].Width = 200;
                dtgImportDataPreview.Columns["ParentCategory"].ReadOnly = true;

                dtgImportDataPreview.Columns["TotalQuantity"].HeaderText = "Total Quantity";
                dtgImportDataPreview.Columns["TotalQuantity"].Width = 200;
                dtgImportDataPreview.Columns["TotalQuantity"].ReadOnly = true;
                dtgImportDataPreview.Columns["OutstandingQuantity"].HeaderText = "Outstanding Quantity";
                dtgImportDataPreview.Columns["OutstandingQuantity"].Width = 200;
                dtgImportDataPreview.Columns["OutstandingQuantity"].ReadOnly = true;

                dtgImportDataPreview.Columns["IsActive"].HeaderText = "Is Active";
                dtgImportDataPreview.Columns["IsDeleted"].HeaderText = "Is Deleted";
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
                //dtgImportDataPreview.Columns["AssetNote"].HeaderText = "Skill Initial";
                //dtgImportDataPreview.Columns["AssetNote"].Width = 200;
                //dtgImportDataPreview.Columns["AssetNote"].ReadOnly = true;
                //dtgImportDataPreview.Columns["AssetNote"].Visible = false;
                //dtgImportDataPreview.Columns["ClientID"].Visible = false;
                //dtgImportDataPreview.Columns["ParentAssetCatMasID"].Visible = false;
            }
            else if (lblSelectedDataAction.Text == "Leave Type Information")
            {
                //dtgImportDataPreview.Columns["LeaveTypeID"].HeaderText = "Leave Type ID";
                //dtgImportDataPreview.Columns["LeaveTypeID"].Visible = false;
                //dtgImportDataPreview.Columns["LeaveCode"].HeaderText = "Leave Type Code";
                //dtgImportDataPreview.Columns["LeaveCode"].Visible = false;
                //dtgImportDataPreview.Columns["LeaveCode"].Width = 150;
                dtgImportDataPreview.Columns["LeaveTypeTitle"].HeaderText = "Leave Type Name";
                dtgImportDataPreview.Columns["LeaveTypeTitle"].Width = 300;
                dtgImportDataPreview.Columns["LeaveTypeTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsPaid"].HeaderText = "Is Paid";
                dtgImportDataPreview.Columns["IsPaid"].Width = 300;
                dtgImportDataPreview.Columns["IsPaid"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].HeaderText = "Is Active";
                dtgImportDataPreview.Columns["IsDeleted"].HeaderText = "Is Deleted";
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (lblSelectedDataAction.Text == "Allowance Information")
            {
                //dtgImportDataPreview.Columns["AllID"].HeaderText = "Allowance ID";
                //dtgImportDataPreview.Columns["AllID"].Visible = false;
                //dtgImportDataPreview.Columns["AllCode"].HeaderText = "Allowance Code";
                //dtgImportDataPreview.Columns["AllCode"].Visible = false;
                //dtgImportDataPreview.Columns["AllCode"].Width = 150;
                dtgImportDataPreview.Columns["AllTitle"].HeaderText = "Allowance Name";
                dtgImportDataPreview.Columns["AllTitle"].Width = 300;
                dtgImportDataPreview.Columns["AllTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["AllDescription"].HeaderText = "Allowance Description";
                dtgImportDataPreview.Columns["AllDescription"].Width = 300;
                dtgImportDataPreview.Columns["AllDescription"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].HeaderText = "Is Active";
                dtgImportDataPreview.Columns["IsDeleted"].HeaderText = "Is Deleted";
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (lblSelectedDataAction.Text == "Deductions Information")
            {
                //dtgImportDataPreview.Columns["DedID"].HeaderText = "Deduction ID";
                //dtgImportDataPreview.Columns["DedID"].Visible = false;
                //dtgImportDataPreview.Columns["DedCode"].HeaderText = "Deduction Code";
                //dtgImportDataPreview.Columns["DedCode"].Visible = false;
                //dtgImportDataPreview.Columns["DedCode"].Width = 150;
                dtgImportDataPreview.Columns["DedTitle"].HeaderText = "Deduction Name";
                dtgImportDataPreview.Columns["DedTitle"].Width = 300;
                dtgImportDataPreview.Columns["DedTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["DedDescription"].HeaderText = "Deduction Description";
                dtgImportDataPreview.Columns["DedDescription"].Width = 300;
                dtgImportDataPreview.Columns["DedDescription"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].HeaderText = "Is Active";
                dtgImportDataPreview.Columns["IsDeleted"].HeaderText = "Is Deleted";
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (lblSelectedDataAction.Text == "Reimbursement Information")
            {
                //dtgImportDataPreview.Columns["ReimbID"].HeaderText = "Reimbursement ID";
                //dtgImportDataPreview.Columns["ReimbID"].Visible = false;
                //dtgImportDataPreview.Columns["ReimbCode"].HeaderText = "Reimbursement Code";
                //dtgImportDataPreview.Columns["ReimbCode"].Visible = false;
                //dtgImportDataPreview.Columns["ReimbCode"].Width = 150;
                dtgImportDataPreview.Columns["ReimbTitle"].HeaderText = "Reimbursement Name";
                dtgImportDataPreview.Columns["ReimbTitle"].Width = 300;
                dtgImportDataPreview.Columns["ReimbTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["ReimbDescription"].HeaderText = "Reimbursement Description";
                dtgImportDataPreview.Columns["ReimbDescription"].Width = 300;
                dtgImportDataPreview.Columns["ReimbDescription"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].HeaderText = "Is Active";
                dtgImportDataPreview.Columns["IsDeleted"].HeaderText = "Is Deleted";
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (lblSelectedDataAction.Text == "Advance Type Information")
            {
                //dtgImportDataPreview.Columns["AdvanceTypeID"].HeaderText = "Advance Type ID";
                //dtgImportDataPreview.Columns["AdvanceTypeID"].Visible = false;
                //dtgImportDataPreview.Columns["AdvanceTypeCode"].HeaderText = "Advance Type Code";
                //dtgImportDataPreview.Columns["AdvanceTypeCode"].Visible = false;
                //dtgImportDataPreview.Columns["AdvanceTypeCode"].Width = 150;
                dtgImportDataPreview.Columns["AdvanceTypeTitle"].HeaderText = "Advance Type Title";
                dtgImportDataPreview.Columns["AdvanceTypeTitle"].Width = 300;
                dtgImportDataPreview.Columns["AdvanceTypeTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].HeaderText = "Is Active";
                dtgImportDataPreview.Columns["IsDeleted"].HeaderText = "Is Deleted";
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (lblSelectedDataAction.Text == "Public Holiday Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Gender Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Employement Type Information")
            {
                //dtgImportDataPreview.Columns["EmpTypeMasID"].HeaderText = "Advance Type ID";
                //dtgImportDataPreview.Columns["EmpTypeMasID"].Visible = false;
                //dtgImportDataPreview.Columns["EmpTypeCode"].HeaderText = "Advance Type Code";
                //dtgImportDataPreview.Columns["EmpTypeCode"].Visible = false;
                //dtgImportDataPreview.Columns["EmpTypeCode"].Width = 150;
                dtgImportDataPreview.Columns["EmpTypeTitle"].HeaderText = "Advance Type Title";
                dtgImportDataPreview.Columns["EmpTypeTitle"].Width = 300;
                dtgImportDataPreview.Columns["EmpTypeTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["EmpTypeInitial"].HeaderText = "Advance Type Initial";
                dtgImportDataPreview.Columns["EmpTypeInitial"].Width = 300;
                dtgImportDataPreview.Columns["EmpTypeInitial"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].HeaderText = "Is Active";
                dtgImportDataPreview.Columns["IsDeleted"].HeaderText = "Is Deleted";
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (lblSelectedDataAction.Text == "Shift Information")
            {
                //dtgImportDataPreview.Columns["ShiftID"].HeaderText = "Shift ID";
                //dtgImportDataPreview.Columns["ShiftID"].Visible = false;
                //dtgImportDataPreview.Columns["ShiftCode"].HeaderText = "Shift Code";
                //dtgImportDataPreview.Columns["ShiftCode"].Visible = false;
                //dtgImportDataPreview.Columns["ShiftCode"].Width = 150;
                dtgImportDataPreview.Columns["ShiftTitle"].HeaderText = "Shift Title";
                dtgImportDataPreview.Columns["ShiftTitle"].Width = 300;
                dtgImportDataPreview.Columns["ShiftTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["ShiftInitital"].HeaderText = "Shift Initial";
                dtgImportDataPreview.Columns["ShiftInitital"].Width = 200;
                dtgImportDataPreview.Columns["ShiftInitital"].ReadOnly = true;
                dtgImportDataPreview.Columns["ShiftStart"].HeaderText = "Shift Start Time";
                dtgImportDataPreview.Columns["ShiftStart"].DefaultCellStyle.Format = "hh:mm:ss tt";
                dtgImportDataPreview.Columns["ShiftStart"].Width = 200;
                dtgImportDataPreview.Columns["ShiftStart"].ReadOnly = true;
                dtgImportDataPreview.Columns["ShiftEnd"].HeaderText = "Shift End Time";
                dtgImportDataPreview.Columns["ShiftEnd"].DefaultCellStyle.Format = "hh:mm:ss tt";
                dtgImportDataPreview.Columns["ShiftEnd"].Width = 200;
                dtgImportDataPreview.Columns["ShiftEnd"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].HeaderText = "Is Active";
                dtgImportDataPreview.Columns["IsDeleted"].HeaderText = "Is Deleted";
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (lblSelectedDataAction.Text == "Bank Information")
            {
                //dtgImportDataPreview.Columns["BankID"].HeaderText = "Bank ID";
                //dtgImportDataPreview.Columns["BankID"].Visible = false;
                //dtgImportDataPreview.Columns["BankCode"].HeaderText = "Bank Code";
                //dtgImportDataPreview.Columns["BankCode"].Visible = false;
                //dtgImportDataPreview.Columns["BankCode"].Width = 150;
                dtgImportDataPreview.Columns["BankName"].HeaderText = "Bank Name";
                dtgImportDataPreview.Columns["BankName"].Width = 300;
                dtgImportDataPreview.Columns["BankName"].ReadOnly = true;
                dtgImportDataPreview.Columns["BankAddress"].HeaderText = "Bank Address";
                dtgImportDataPreview.Columns["BankAddress"].Width = 200;
                dtgImportDataPreview.Columns["BankAddress"].ReadOnly = true;
                dtgImportDataPreview.Columns["IFSCCode"].HeaderText = "IFSC Code";
                dtgImportDataPreview.Columns["IFSCCode"].Width = 200;
                dtgImportDataPreview.Columns["IFSCCode"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].HeaderText = "Is Active";
                dtgImportDataPreview.Columns["IsDeleted"].HeaderText = "Is Deleted";
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
        }


        /// <summary>
        /// Adds the Download Template button column to the import source grid.
        /// Safe to call multiple times because formatGrid() is called more than once.
        /// </summary>
        private void AddDownloadTemplateColumn()
        {
            try
            {
                const string columnName = "DownloadTemplate";

                if (!dtgImportDataSourceList.Columns.Contains(columnName))
                {
                    DataGridViewButtonColumn downloadColumn = new DataGridViewButtonColumn();

                    downloadColumn.Name = columnName;
                    downloadColumn.HeaderText = "Download";
                    downloadColumn.ToolTipText = "Download CSV Template";
                    downloadColumn.Text = "⇩";
                    downloadColumn.UseColumnTextForButtonValue = true;
                    downloadColumn.Width = 52;
                    downloadColumn.MinimumWidth = 52;
                    downloadColumn.FlatStyle = FlatStyle.Standard;
                    downloadColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    downloadColumn.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI Symbol", 11F, System.Drawing.FontStyle.Bold);
                    downloadColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
                    downloadColumn.ReadOnly = true;
                    dtgImportDataSourceList.Columns.Add(downloadColumn);
                }

                if (!isDownloadTemplateEventsAttached)
                {
                    dtgImportDataSourceList.CellContentClick += dtgImportDataSourceList_CellContentClick;
                    isDownloadTemplateEventsAttached = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to add the template download button." + Environment.NewLine + Environment.NewLine + ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles a click on the Download Template button for the selected row.
        /// The physical template file name is taken directly from column
        /// "ImpDataInfoTemplateName" and copied from the application's
        /// importtemplate folder to the user's Downloads folder.
        /// </summary>
        private void dtgImportDataSourceList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;

                if (dtgImportDataSourceList.Columns[e.ColumnIndex].Name !=
                    "DownloadTemplate")
                {
                    return;
                }

                if (dtgImportDataSourceList.Rows[e.RowIndex].IsNewRow)
                    return;

                string templateFileName = Convert.ToString(
                    dtgImportDataSourceList.Rows[e.RowIndex]
                        .Cells["ImpDataInfoTemplateName"].Value).Trim();

                if (string.IsNullOrWhiteSpace(templateFileName))
                {
                    MessageBox.Show(
                        "Template file name is not available for the selected import data.",
                        "Staffsync",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                DownloadImportTemplate(templateFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to download the selected template." +
                    Environment.NewLine + Environment.NewLine +
                    ex.Message,
                    "Staffsync",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Copies the existing physical template file from:
        ///     Application.exe path\importtemplate\<filename from column 2>
        ///
        /// directly to the current user's Downloads folder.
        /// No new CSV content is generated and no SaveFileDialog is shown.
        /// </summary>
        private void DownloadImportTemplate(string templateFileName)
        {
            try
            {
                // Only the file name is allowed. This prevents a value in the
                // grid from escaping the importtemplate folder.
                templateFileName = Path.GetFileName(templateFileName);

                if (string.IsNullOrWhiteSpace(templateFileName))
                {
                    MessageBox.Show(
                        "Invalid template file name.",
                        "Staffsync",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                string sourceTemplateFolder = Path.Combine(
                    Application.StartupPath,
                    "importtemplate");

                string sourceTemplateFile = Path.Combine(
                    sourceTemplateFolder,
                    templateFileName);

                if (!File.Exists(sourceTemplateFile))
                {
                    MessageBox.Show(
                        "The template file was not found." +
                        Environment.NewLine + Environment.NewLine +
                        sourceTemplateFile,
                        "Staffsync",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                string downloadsFolder = Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.UserProfile),
                    "Downloads");

                if (!Directory.Exists(downloadsFolder))
                {
                    Directory.CreateDirectory(downloadsFolder);
                }

                string destinationTemplateFile = Path.Combine(
                    downloadsFolder,
                    templateFileName);

                // Copy the actual physical file from the application's
                // importtemplate folder to Downloads.
                File.Copy(
                    sourceTemplateFile,
                    destinationTemplateFile,
                    true);

                // Open the downloaded physical template file directly using
                // the application associated with its file type.
                System.Diagnostics.Process.Start(
                    new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = destinationTemplateFile,
                        UseShellExecute = true
                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to download the selected template." +
                    Environment.NewLine + Environment.NewLine +
                    ex.Message,
                    "Staffsync",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Returns the input columns expected by the existing import logic.
        /// Database-generated IDs and IsDeleted are intentionally omitted.
        /// </summary>
        private string[] GetImportTemplateColumns(string importSourceTitle)
        {
            switch (importSourceTitle.Trim())
            {
                case "Import Organisation Information":
                case "Import Designation Information":
                    return new[]
                    {
                        "DesignationCode",
                        "DesignationTitle",
                        "DesignationInitial",
                        "IsActive"
                    };

                case "Import Department Information":
                    return new[]
                    {
                        "DepartmentTitle",
                        "DepartmentInitial",
                        "IsActive"
                    };

                case "Import Countries Information":
                    return new[]
                    {
                        "CountryTitle",
                        "CountryInitial",
                        "IsActive"
                    };

                case "Import States Information":
                    return new[]
                    {
                        "StateCode",
                        "StateTitle",
                        "StateInitial",
                        "IsActive"
                    };

                case "Import Education Information":
                    return new[]
                    {
                        "EduQualCode",
                        "EduQualTitle",
                        "EduQualInitial",
                        "IsActive"
                    };

                case "Import Skills Information":
                    return new[]
                    {
                        "SkillCode",
                        "SkillTitle",
                        "SkillInitial",
                        "IsActive"
                    };

                case "Import Relationship Information":
                    return new[]
                    {
                        "RelationshipCode",
                        "RelationshipTitle",
                        "RelationshipInitial",
                        "IsActive"
                    };

                case "Import Asset Category Information":
                    return new[]
                    {
                        "AssetCode",
                        "AssetName",
                        "AssetDescription",
                        "ParentCategory",
                        "IsActive"
                    };

                case "Import Assets Information":
                    return new[]
                    {
                        "AssetCode",
                        "AssetName",
                        "AssetDescription",
                        "ParentCategory",
                        "TotalQuantity",
                        "OutstandingQuantity",
                        "IsActive"
                    };

                case "Import Leave Type Information":
                    return new[]
                    {
                        "LeaveCode",
                        "LeaveTypeTitle",
                        "IsPaid",
                        "IsActive"
                    };

                case "Import Allowance Information":
                    return new[]
                    {
                        "AllCode",
                        "AllTitle",
                        "AllDescription",
                        "IsActive"
                    };

                case "Import Deductions Information":
                    return new[]
                    {
                        "DedCode",
                        "DedTitle",
                        "DedDescription",
                        "IsActive"
                    };

                case "Import Reimbursement Information":
                    return new[]
                    {
                        "ReimbCode",
                        "ReimbTitle",
                        "ReimbDescription",
                        "IsActive"
                    };

                case "Import Advance Type Information":
                    return new[]
                    {
                        "AdvanceTypeCode",
                        "AdvanceTypeTitle",
                        "IsActive"
                    };

                case "Import Employement Type Information":
                    return new[]
                    {
                        "EmpTypeCode",
                        "EmpTypeTitle",
                        "EmpTypeInitial",
                        "IsActive"
                    };

                case "Import Shift Information":
                    return new[]
                    {
                        "ShiftCode",
                        "ShiftTitle",
                        "ShiftInitital",
                        "ShiftStart",
                        "ShiftEnd",
                        "IsActive"
                    };

                case "Import Bank Information":
                    return new[]
                    {
                        "BankCode",
                        "BankName",
                        "BankAddress",
                        "IFSCCode",
                        "IsActive"
                    };

                // These branches currently have no import-processing logic
                // in the supplied source, so a misleading template is not
                // generated for them.
                case "Import Company Information":
                case "Import Weekly Off Information":
                case "Import Public Holiday Information":
                case "Import Gender Information":
                    return null;

                default:
                    return null;
            }
        }

        private string EscapeCsvTemplateValue(string value)
        {
            if (value == null)
                return string.Empty;

            if (value.Contains(",") ||
                value.Contains("\"") ||
                value.Contains("\r") ||
                value.Contains("\n"))
            {
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            }

            return value;
        }

        private string MakeSafeTemplateFileName(string fileName)
        {
            foreach (char invalidCharacter in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(
                    invalidCharacter.ToString(),
                    string.Empty);
            }

            return fileName.Trim();
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            // The original Import form used this button to select an external
            // CSV/Excel file. Export Data Process does not need a source file.
            // The data source is the selected StaffSync master on the left.
            LoadSelectedExportData();
        }

        /// <summary>
        /// Gets the selected import source title from the source-list grid.
        /// </summary>
        private string GetSelectedImportSourceTitle()
        {
            if (dtgImportDataSourceList == null || dtgImportDataSourceList.CurrentRow == null)
            {
                return string.Empty;
            }

            try
            {
                if (dtgImportDataSourceList.Columns.Contains("ImpDataInfoTitle"))
                {
                    return Convert.ToString(dtgImportDataSourceList.CurrentRow.Cells["ImpDataInfoTitle"].Value);
                }

                // Fallback to the existing title column position used by
                // the current form.
                if (dtgImportDataSourceList.CurrentRow.Cells.Count > 2)
                {
                    return Convert.ToString(dtgImportDataSourceList.CurrentRow.Cells[2].Value);
                }
            }
            catch
            {
                // Return an empty title if the source grid has no usable row.
            }

            return string.Empty;
        }

        /// <summary>
        /// Reads CSV/XLS/XLSX and returns the complete data as a DataTable.
        /// The first row is treated as the column header.
        /// </summary>
        private DataTable ReadImportFile(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();

            switch (extension)
            {
                case ".csv":
                    return ReadCsvFile(filePath);

                case ".xls":
                case ".xlsx":
                    return ReadExcelFile(filePath);

                default:
                    throw new NotSupportedException("Only .csv, .xls and .xlsx files are supported.");
            }
        }

        /// <summary>
        /// Reads a CSV file. The first row is used as the column headers.
        /// Handles quoted fields and commas inside quoted values.
        /// </summary>
        private DataTable ReadCsvFile(string filePath)
        {
            DataTable dt = new DataTable();

            using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8, true))
            {
                string headerLine = reader.ReadLine();

                if (string.IsNullOrWhiteSpace(headerLine))
                    return dt;

                List<string> headers = ParseCsvLine(headerLine);

                HashSet<string> usedHeaders = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                foreach (string rawHeader in headers)
                {
                    string header = rawHeader == null ? string.Empty : rawHeader.Trim();

                    if (string.IsNullOrWhiteSpace(header))
                        header = "Column" + (dt.Columns.Count + 1);

                    string originalHeader = header;
                    int suffix = 2;

                    while (usedHeaders.Contains(header))
                    {
                        header = originalHeader + "_" + suffix;
                        suffix++;
                    }

                    usedHeaders.Add(header);
                    dt.Columns.Add(header, typeof(string));
                }

                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    List<string> values = ParseCsvLine(line);

                    DataRow row = dt.NewRow();

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        row[i] = i < values.Count ? values[i] : string.Empty;
                    }

                    dt.Rows.Add(row);
                }
            }

            return dt;
        }

        /// <summary>
        /// Reads the first worksheet from an XLS/XLSX file using OleDb.
        /// </summary>
        private DataTable ReadExcelFile(string filePath)
        {
            DataTable result = new DataTable();

            string extension = Path.GetExtension(filePath).ToLowerInvariant();

            string connectionString;

            if (extension == ".xls")
            {
                connectionString =
                    "Provider=Microsoft.ACE.OLEDB.12.0;" +
                    "Data Source=" + filePath + ";" +
                    "Extended Properties=\"Excel 8.0; HDR = YES; IMEX = 1\";";
            }
            else
            {
                connectionString =
                    "Provider=Microsoft.ACE.OLEDB.12.0;" +
                    "Data Source=" + filePath + ";" +
                    "Extended Properties=\"Excel 12.0 Xml; HDR = YES; IMEX = 1\";";
            }

            using (OleDbConnection connection =
                   new OleDbConnection(connectionString))
            {
                connection.Open();

                DataTable schema =
                    connection.GetOleDbSchemaTable(
                        OleDbSchemaGuid.Tables,
                        null);

                if (schema == null || schema.Rows.Count == 0)
                    return result;

                string worksheetName = null;

                foreach (DataRow schemaRow in schema.Rows)
                {
                    string tableName =
                        Convert.ToString(schemaRow["TABLE_NAME"]);

                    if (string.IsNullOrWhiteSpace(tableName))
                        continue;

                    if (tableName.EndsWith("$") ||
                        tableName.EndsWith("$'"))
                    {
                        worksheetName = tableName;
                        break;
                    }
                }

                if (string.IsNullOrWhiteSpace(worksheetName))
                    throw new Exception(
                        "No worksheet was found in the selected Excel file.");

                using (OleDbCommand command =
                       new OleDbCommand(
                           "SELECT * FROM [" + worksheetName + "]",
                           connection))
                {
                    command.CommandType = CommandType.Text;

                    using (OleDbDataAdapter adapter =
                           new OleDbDataAdapter(command))
                    {
                        adapter.Fill(result);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Simple CSV parser supporting quoted values and escaped quotes.
        /// </summary>
        private List<string> ParseCsvLine(string line)
        {
            List<string> values = new List<string>();
            StringBuilder currentValue = new StringBuilder();
            bool insideQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char currentCharacter = line[i];

                if (currentCharacter == '"')
                {
                    if (insideQuotes &&
                        i + 1 < line.Length &&
                        line[i + 1] == '"')
                    {
                        currentValue.Append('"');
                        i++;
                    }
                    else
                    {
                        insideQuotes = !insideQuotes;
                    }

                    continue;
                }

                if (currentCharacter == ',' && !insideQuotes)
                {
                    values.Add(currentValue.ToString());
                    currentValue.Clear();
                    continue;
                }

                currentValue.Append(currentCharacter);
            }

            values.Add(currentValue.ToString());

            return values;
        }

        /// <summary>
        /// Binds imported data to the preview grid and selects every row by default.
        /// </summary>
        private void BindImportPreviewData(DataTable importedData)
        {
            objImportDataPreviewTable = importedData.Copy();

            // The checkbox column is maintained by the form and is not part
            // of the source Excel/CSV data.
            if (objImportDataPreviewTable.Columns.Contains("IsSelected"))
            {
                objImportDataPreviewTable.Columns.Remove("IsSelected");
            }

            DataColumn selectedColumn = new DataColumn("IsSelected", typeof(bool));

            selectedColumn.DefaultValue = true;

            objImportDataPreviewTable.Columns.Add(selectedColumn);
            selectedColumn.SetOrdinal(0);

            dtgImportDataPreview.AutoGenerateColumns = true;
            dtgImportDataPreview.DataSource = null;
            dtgImportDataPreview.DataSource = objImportDataPreviewTable;

            if (dtgImportDataPreview.Columns.Contains("IsSelected"))
            {
                dtgImportDataPreview.Columns["IsSelected"].HeaderText = "Select";
                dtgImportDataPreview.Columns["IsSelected"].Width = 60;
                dtgImportDataPreview.Columns["IsSelected"].DisplayIndex = 0;
                dtgImportDataPreview.Columns["IsSelected"].ReadOnly = false;
            }

            AttachImportPreviewEvents();

            // Explicitly set every row to selected.
            foreach (DataGridViewRow row in dtgImportDataPreview.Rows)
            {
                if (!row.IsNewRow && row.Cells["IsSelected"] != null)
                {
                    row.Cells["IsSelected"].Value = true;
                }
            }

            UpdateImportSelectedRowCount();

            dtgImportDataPreview.Columns["IsActive"].Visible = false;
            dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
        }

        /// <summary>
        /// Binds database data to the Export preview grid and adds the UI-only
        /// IsSelected checkbox column as the first column.
        /// </summary>
        /// <summary>
        /// Converts a strongly typed list returned by a StaffSync DAL method
        /// into a DataTable so the existing export preview/export pipeline can
        /// work with both DataTable and List&lt;T&gt; based DAL methods.
        /// </summary>
        private DataTable ConvertListToDataTable<T>(IEnumerable<T> items)
        {
            DataTable table = new DataTable(typeof(T).Name);

            if (items == null)
                return table;

            System.Reflection.PropertyInfo[] properties =
                typeof(T).GetProperties(System.Reflection.BindingFlags.Public |
                                         System.Reflection.BindingFlags.Instance);

            foreach (System.Reflection.PropertyInfo property in properties)
            {
                Type propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

                // DataTable does not accept pointer/by-ref property types.
                if (propertyType.IsPointer || propertyType.IsByRef)
                    continue;

                table.Columns.Add(property.Name, propertyType);
            }

            foreach (T item in items)
            {
                DataRow row = table.NewRow();

                foreach (System.Reflection.PropertyInfo property in properties)
                {
                    if (!table.Columns.Contains(property.Name))
                        continue;

                    object value = property.GetValue(item, null);
                    row[property.Name] = value ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }

            return table;
        }

        private void BindExportPreviewData(DataTable sourceData)
        {
            if (sourceData == null)
            {
                ClearExportPreview();
                return;
            }

            objImportDataPreviewTable = sourceData.Copy();

            if (objImportDataPreviewTable.Columns.Contains("IsSelected"))
            {
                objImportDataPreviewTable.Columns.Remove("IsSelected");
            }

            DataColumn selectedColumn = new DataColumn("IsSelected", typeof(bool));

            selectedColumn.DefaultValue = true;
            objImportDataPreviewTable.Columns.Add(selectedColumn);
            selectedColumn.SetOrdinal(0);

            dtgImportDataPreview.AutoGenerateColumns = true;
            dtgImportDataPreview.DataSource = null;
            dtgImportDataPreview.DataSource = objImportDataPreviewTable;

            if (dtgImportDataPreview.Columns.Contains("IsSelected"))
            {
                dtgImportDataPreview.Columns["IsSelected"].HeaderText = "Select";
                dtgImportDataPreview.Columns["IsSelected"].Width = 60;
                dtgImportDataPreview.Columns["IsSelected"].DisplayIndex = 0;
                dtgImportDataPreview.Columns["IsSelected"].ReadOnly = false;
            }

            AttachExportPreviewEvents();

            foreach (DataGridViewRow row in dtgImportDataPreview.Rows)
            {
                if (row.IsNewRow)
                    continue;

                if (row.Cells["IsSelected"] != null)
                    row.Cells["IsSelected"].Value = true;
            }

            lblTotalRowsFromSource.Text = "Total Rows from Source : " + sourceData.Rows.Count.ToString();
            chkSelectOrUnselect.Enabled = sourceData.Rows.Count > 0;
            chkSelectOrUnselect.Checked = sourceData.Rows.Count > 0;
            chkSelectOrUnselect.Text = sourceData.Rows.Count > 0 ? "Unselect All" : "Select All";
            UpdateExportSelectedRowCount();
        }

        private void AttachExportPreviewEvents()
        {
            if (isExportPreviewEventsAttached)
                return;

            dtgImportDataPreview.CurrentCellDirtyStateChanged +=
                dtgExportPreview_CurrentCellDirtyStateChanged;

            dtgImportDataPreview.CellValueChanged +=
                dtgExportPreview_CellValueChanged;

            isExportPreviewEventsAttached = true;
        }

        private void dtgExportPreview_CurrentCellDirtyStateChanged(
            object sender,
            EventArgs e)
        {
            if (dtgImportDataPreview.IsCurrentCellDirty)
            {
                dtgImportDataPreview.CommitEdit(
                    DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dtgExportPreview_CellValueChanged(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 ||
                e.ColumnIndex < 0 ||
                !dtgImportDataPreview.Columns.Contains("IsSelected"))
            {
                return;
            }

            if (dtgImportDataPreview.Columns[e.ColumnIndex].Name ==
                "IsSelected")
            {
                UpdateExportSelectedRowCount();
            }
        }

        private void UpdateExportSelectedRowCount()
        {
            int selectedCount = 0;

            if (dtgImportDataPreview != null &&
                dtgImportDataPreview.Columns.Contains("IsSelected"))
            {
                foreach (DataGridViewRow row in dtgImportDataPreview.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    bool isSelected = false;

                    if (row.Cells["IsSelected"].Value != null)
                    {
                        bool.TryParse(
                            row.Cells["IsSelected"].Value.ToString(),
                            out isSelected);
                    }

                    if (isSelected)
                        selectedCount++;
                }
            }

            lblTotalRowsSelected.Text =
                "Total Rows Selected : " +
                selectedCount.ToString();

            btnSaveDetails.Enabled = selectedCount > 0;

            if (dtgImportDataPreview != null &&
                dtgImportDataPreview.Rows.Count > 0 &&
                selectedCount == dtgImportDataPreview.Rows.Count)
            {
                chkSelectOrUnselect.Checked = true;
                chkSelectOrUnselect.Text = "Unselect All";
            }
            else if (selectedCount == 0)
            {
                chkSelectOrUnselect.Checked = false;
                chkSelectOrUnselect.Text = "Select All";
            }
            else
            {
                chkSelectOrUnselect.Checked = false;
                chkSelectOrUnselect.Text = "Select All";
            }
        }

        private void ClearExportPreview()
        {
            objImportDataPreviewTable = new DataTable();

            if (dtgImportDataPreview != null)
                dtgImportDataPreview.DataSource = null;

            lblTotalRowsFromSource.Text =
                "Total Rows from Source : 0";

            lblTotalRowsSelected.Text =
                "Total Rows Selected : 0";

            lblTotalImportedRows.Text =
                "Total Rows Imported : 0";

            lblTotalDuplicateRows.Text =
                "Total Duplicate Rows : 0";

            lblTotalNotImportedRows.Text =
                "Total Rows Not Imported : 0";

            chkSelectOrUnselect.Checked = false;
            chkSelectOrUnselect.Text = "Select All";
            chkSelectOrUnselect.Enabled = false;
            btnSaveDetails.Enabled = false;
        }

        private void AttachImportPreviewEvents()
        {
            if (isImportPreviewEventsAttached)
                return;

            dtgImportDataPreview.CurrentCellDirtyStateChanged +=
                dtgImportDataPreview_CurrentCellDirtyStateChanged;

            dtgImportDataPreview.CellValueChanged +=
                dtgImportDataPreview_CellValueChanged;

            isImportPreviewEventsAttached = true;
        }

        private void dtgImportDataPreview_CurrentCellDirtyStateChanged(
            object sender,
            EventArgs e)
        {
            if (dtgImportDataPreview.IsCurrentCellDirty)
            {
                dtgImportDataPreview.CommitEdit(
                    DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dtgImportDataPreview_CellValueChanged(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 ||
                e.ColumnIndex < 0 ||
                !dtgImportDataPreview.Columns.Contains("IsSelected"))
            {
                return;
            }

            if (dtgImportDataPreview.Columns[e.ColumnIndex].Name ==
                "IsSelected")
            {
                UpdateImportSelectedRowCount();
            }
        }

        private void UpdateImportSelectedRowCount()
        {
            int selectedCount = 0;

            btnSaveDetails.Enabled = false;
            if (dtgImportDataPreview != null && dtgImportDataPreview.Columns.Contains("IsSelected"))
            {
                foreach (DataGridViewRow row in dtgImportDataPreview.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    bool isSelected = false;

                    if (row.Cells["IsSelected"].Value != null)
                    {
                        bool.TryParse(row.Cells["IsSelected"].Value.ToString(), out isSelected);
                    }

                    if (isSelected)
                    {
                        selectedCount++;
                        btnSaveDetails.Enabled = true;
                    }
                }
            }

            lblTotalRowsSelected.Text = "Total Rows Selected : " + selectedCount.ToString();

            if (btnSaveDetails.Enabled == false)
            {
                chkSelectOrUnselect.Checked = false;
                chkSelectOrUnselect.Text = "Select All";
            }
        }

        private void ClearImportPreview()
        {
            objImportDataPreviewTable =
                new DataTable();

            objSelectedImportFilePath =
                string.Empty;

            if (dtgImportDataPreview != null)
                dtgImportDataPreview.DataSource = null;

            lblTotalRowsFromSource.Text =
                "Total Rows from Source : 0";

            lblTotalRowsSelected.Text =
                "Total Rows Selected : 0";
        }

        /// <summary>
        /// Gets only the rows selected by the user from the preview grid.
        /// This is the point where the selected values can be passed to
        /// the corresponding master-table insert/update logic.
        /// </summary>
        private void ProcessSelectedImportRows()
        {
            string selectedImportSource =
                GetSelectedImportSourceTitle();

            if (string.Equals(
                    selectedImportSource,
                    "Import Designation Information",
                    StringComparison.OrdinalIgnoreCase))
            {
                foreach (DataGridViewRow row
                         in dtgImportDataPreview.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    bool isSelected = false;

                    if (row.Cells["IsSelected"].Value != null)
                    {
                        bool.TryParse(
                            row.Cells["IsSelected"].Value.ToString(),
                            out isSelected);
                    }

                    if (!isSelected)
                        continue;

                    string designationID = GetImportCellValue(row, "DesignationID");

                    string designationCode = GetImportCellValue(row, "DesignationCode");

                    string designationTitle = GetImportCellValue(row, "DesignationTitle");

                    string designationInitial = GetImportCellValue(row, "DesignationInitial");

                    // ------------------------------------------------------
                    // SELECTED DESIGNATION ROW
                    //
                    // Pass these values to DesigMas insert/update logic:
                    //
                    // designationID
                    // designationCode
                    // designationTitle
                    // designationInitial
                    //
                    // IsActive / IsDeleted can be handled according to
                    // your master-table import rules.
                    // ------------------------------------------------------
                }
            }
        }

        private string GetImportCellValue(DataGridViewRow row, string columnName)
        {
            if (row == null || !dtgImportDataPreview.Columns.Contains(columnName))
            {
                return string.Empty;
            }

            object value = row.Cells[columnName].Value;

            return value == null || value == DBNull.Value ? string.Empty : value.ToString();
        }

        /// <summary>
        /// Public helper for the next stage of the import implementation.
        /// Returns only selected rows from the preview grid.
        /// </summary>
        public List<Dictionary<string, string>> GetSelectedImportRows()
        {
            List<Dictionary<string, string>> selectedRows = new List<Dictionary<string, string>>();

            if (dtgImportDataPreview == null || dtgImportDataPreview.Rows.Count == 0)
            {
                return selectedRows;
            }

            foreach (DataGridViewRow row in dtgImportDataPreview.Rows)
            {
                if (row.IsNewRow)
                    continue;

                bool isSelected = false;

                if (row.Cells["IsSelected"].Value != null)
                {
                    bool.TryParse(row.Cells["IsSelected"].Value.ToString(), out isSelected);
                }

                if (!isSelected)
                    continue;

                Dictionary<string, string> values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

                foreach (DataGridViewColumn column in dtgImportDataPreview.Columns)
                {
                    if (column.Name == "IsSelected")
                        continue;

                    values[column.Name] = GetImportCellValue(row, column.Name);
                }

                selectedRows.Add(values);
            }

            return selectedRows;
        }


        private void btnCloseMe_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenerateDetails_Click(object sender, EventArgs e)
        {
            LoadSelectedExportData();
            errValidator.Clear();
        }

        /// <summary>
        /// Exports only the rows currently selected in the preview grid.
        /// Supported formats: PDF, CSV, XML and JSON.
        /// </summary>
        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            ExportSelectedData();
        }

        private void LoadSelectedExportData()
        {
            try
            {
                string selectedTitle = GetSelectedImportSourceTitle();

                if (string.IsNullOrWhiteSpace(selectedTitle))
                {
                    MessageBox.Show("Please select an export data source.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                lblSelectedDataAction.Text = selectedTitle.Trim();

                if (string.Equals(lblSelectedDataAction.Text, "Designation Information", StringComparison.OrdinalIgnoreCase))
                {
                    DataTable dtDesignationList = ConvertListToDataTable(objDesignation.GetDesignationList());
                    if (dtDesignationList == null || dtDesignationList.Rows.Count == 0)
                    {
                        ClearExportPreview();
                        MessageBox.Show("No Designation Information is available for export.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    BindExportPreviewData(dtDesignationList);
                    FormatPreviewGrid(lblSelectedDataAction.Text);
                    dtgImportDataPreview.Enabled = true;

                    return;
                }
                else if (string.Equals(lblSelectedDataAction.Text, "Department Information", StringComparison.OrdinalIgnoreCase))
                {
                    DataTable dtDepartmentList = ConvertListToDataTable(objDepartment.GetDepartmentList());
                    if (dtDepartmentList == null || dtDepartmentList.Rows.Count == 0)
                    {
                        ClearExportPreview();
                        MessageBox.Show("No Department Information is available for export.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    BindExportPreviewData(dtDepartmentList);
                    FormatPreviewGrid(lblSelectedDataAction.Text);
                    dtgImportDataPreview.Enabled = true;

                    return;
                }
                else if (string.Equals(lblSelectedDataAction.Text, "Countries Information", StringComparison.OrdinalIgnoreCase))
                {
                    DataTable dtCountriesList = objCountries.GetCountryList();
                    if (dtCountriesList == null || dtCountriesList.Rows.Count == 0)
                    {
                        ClearExportPreview();
                        MessageBox.Show("No Countries Information is available for export.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    BindExportPreviewData(dtCountriesList);
                    FormatPreviewGrid(lblSelectedDataAction.Text);
                    dtgImportDataPreview.Enabled = true;

                    return;
                }
                else if (string.Equals(lblSelectedDataAction.Text, "States Information", StringComparison.OrdinalIgnoreCase))
                {
                    DataTable dtStatesList = ConvertListToDataTable(objStates.GetStateList());
                    if (dtStatesList == null || dtStatesList.Rows.Count == 0)
                    {
                        ClearExportPreview();
                        MessageBox.Show("No States Information is available for export.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    BindExportPreviewData(dtStatesList);
                    FormatPreviewGrid(lblSelectedDataAction.Text);
                    dtgImportDataPreview.Enabled = true;

                    return;
                }
                else if (string.Equals(lblSelectedDataAction.Text, "Education Information", StringComparison.OrdinalIgnoreCase))
                {
                    DataTable dtEductionList = ConvertListToDataTable(objEduQalification.GetEduQualMasList());
                    if (dtEductionList == null || dtEductionList.Rows.Count == 0)
                    {
                        ClearExportPreview();
                        MessageBox.Show("No Education Information is available for export.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    BindExportPreviewData(dtEductionList);
                    FormatPreviewGrid(lblSelectedDataAction.Text);
                    dtgImportDataPreview.Enabled = true;

                    return;
                }
                else if (string.Equals(lblSelectedDataAction.Text, "Relationship Information", StringComparison.OrdinalIgnoreCase))
                {
                    DataTable dtRelationshipList = objRelationship.GetRelationshipList();
                    if (dtRelationshipList == null || dtRelationshipList.Rows.Count == 0)
                    {
                        ClearExportPreview();
                        MessageBox.Show("No Relationship Information is available for export.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    
                    BindExportPreviewData(dtRelationshipList);
                    FormatPreviewGrid(lblSelectedDataAction.Text);
                    dtgImportDataPreview.Enabled = true;
                    
                    return; 
                }
                else if (string.Equals(lblSelectedDataAction.Text, "Weekly Off Information", StringComparison.OrdinalIgnoreCase))
                {
                    DataTable dtWeeklyOffList = ConvertListToDataTable(objWeeklyOffInfo.getWklyOffProfileMasInfoList(""));
                    if (dtWeeklyOffList == null || dtWeeklyOffList.Rows.Count == 0)
                    {
                        ClearExportPreview();
                        MessageBox.Show("No Weekly Off Information is available for export.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    BindExportPreviewData(dtWeeklyOffList);
                    FormatPreviewGrid(lblSelectedDataAction.Text);
                    dtgImportDataPreview.Enabled = true;
                    return;
                }
                else if (string.Equals(lblSelectedDataAction.Text, "Asset Category Information", StringComparison.OrdinalIgnoreCase))
                {
                    DataTable dtAssetCategoryList = ConvertListToDataTable(objAssetsCategory.getAssetsCategoryList(objTempClientFinYearInfo.ClientID));
                    if (dtAssetCategoryList == null || dtAssetCategoryList.Rows.Count == 0)
                    {
                        ClearExportPreview();
                        MessageBox.Show("No Asset Category Information is available for export.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    BindExportPreviewData(dtAssetCategoryList);
                    FormatPreviewGrid(lblSelectedDataAction.Text);
                    dtgImportDataPreview.Enabled = true;

                    return;
                }
                else if (string.Equals(lblSelectedDataAction.Text, "Assets Information", StringComparison.OrdinalIgnoreCase))
                {
                    DataTable dtAssetCategoryList = ConvertListToDataTable(objAssetsInfo.getAssetsInfoList(objTempClientFinYearInfo.ClientID));
                    if (dtAssetCategoryList == null || dtAssetCategoryList.Rows.Count == 0)
                    {
                        ClearExportPreview();
                        MessageBox.Show("No Asset Category Information is available for export.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    BindExportPreviewData(dtAssetCategoryList);
                    FormatPreviewGrid(lblSelectedDataAction.Text);
                    dtgImportDataPreview.Enabled = true;
                    return;
                }
                else if (string.Equals(lblSelectedDataAction.Text, "Leave Type Information", StringComparison.OrdinalIgnoreCase))
                {
                    DataTable dtLeaveTypeList = objLeaveTypeMas.GetLeaveTypeList();
                    if (dtLeaveTypeList == null || dtLeaveTypeList.Rows.Count == 0)
                    {
                        ClearExportPreview();
                        MessageBox.Show("No Leave Type Information is available for export.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    BindExportPreviewData(dtLeaveTypeList);
                    FormatPreviewGrid(lblSelectedDataAction.Text);
                    dtgImportDataPreview.Enabled = true;
                    return;
                }
                else if (string.Equals(lblSelectedDataAction.Text, "Skills Information", StringComparison.OrdinalIgnoreCase))
                {
                    DataTable dtSkillsList = ConvertListToDataTable(objSkillsMas.GetSkillList());
                    if (dtSkillsList == null || dtSkillsList.Rows.Count == 0)
                    {
                        ClearExportPreview();
                        MessageBox.Show("No Skills Information is available for export.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    BindExportPreviewData(dtSkillsList);
                    FormatPreviewGrid(lblSelectedDataAction.Text);
                    dtgImportDataPreview.Enabled = true;

                    return;
                }
                else if (string.Equals(lblSelectedDataAction.Text, "Shift Information", StringComparison.OrdinalIgnoreCase))
                {
                    DataTable shiftData = ConvertListToDataTable(objShiftMas.GetShiftList());
                    if (shiftData == null || shiftData.Rows.Count == 0)
                    {
                        ClearExportPreview();
                        MessageBox.Show("No Shift Information is available for export.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    BindExportPreviewData(shiftData);
                    FormatPreviewGrid(lblSelectedDataAction.Text);
                    dtgImportDataPreview.Enabled = true;

                    return;
                }

                ClearExportPreview();

                MessageBox.Show("Export loading is not yet configured for: " + lblSelectedDataAction.Text, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load the selected export data." + Environment.NewLine + Environment.NewLine + ex.Message, "Staffsync - Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatPreviewGrid(string selectedItem)
        {
            if (dtgImportDataPreview == null)
                return;

            dtgImportDataPreview.Columns["IsSelected"].HeaderText = "Select";
            dtgImportDataPreview.Columns["IsSelected"].Width = 60;
            dtgImportDataPreview.Columns["IsSelected"].DisplayIndex = 0;
            dtgImportDataPreview.Columns["IsSelected"].ReadOnly = false;

            if (string.Equals(lblSelectedDataAction.Text, "Designation Information", StringComparison.OrdinalIgnoreCase))
            {
                dtgImportDataPreview.Columns["DesignationID"].Visible = false;
                dtgImportDataPreview.Columns["DesignationCode"].Visible = false;
                dtgImportDataPreview.Columns["DesignationTitle"].HeaderText = "Designation Name";
                dtgImportDataPreview.Columns["DesignationTitle"].Width = 300;
                dtgImportDataPreview.Columns["DesignationTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["DesignationInitial"].HeaderText = "Designation Initial";
                dtgImportDataPreview.Columns["DesignationInitial"].Width = 200;
                dtgImportDataPreview.Columns["DesignationInitial"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (string.Equals(lblSelectedDataAction.Text, "Department Information", StringComparison.OrdinalIgnoreCase))
            {
                dtgImportDataPreview.Columns["DepartmentID"].Visible = false;
                dtgImportDataPreview.Columns["DepCode"].Visible = false;
                dtgImportDataPreview.Columns["DepartmentTitle"].HeaderText = "Department Name";
                dtgImportDataPreview.Columns["DepartmentTitle"].Width = 300;
                dtgImportDataPreview.Columns["DepartmentTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["DepartmentInitial"].HeaderText = "Department Initial";
                dtgImportDataPreview.Columns["DepartmentInitial"].Width = 200;
                dtgImportDataPreview.Columns["DepartmentInitial"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (string.Equals(lblSelectedDataAction.Text, "Countries Information", StringComparison.OrdinalIgnoreCase))
            {
                dtgImportDataPreview.Columns["CountryID"].Visible = false;
                dtgImportDataPreview.Columns["CountryCode"].Visible = false;
                dtgImportDataPreview.Columns["CountryTitle"].HeaderText = "Country Name";
                dtgImportDataPreview.Columns["CountryTitle"].Width = 300;
                dtgImportDataPreview.Columns["CountryTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["CountryInitial"].HeaderText = "Country Code";
                dtgImportDataPreview.Columns["CountryInitial"].Width = 200;
                dtgImportDataPreview.Columns["CountryInitial"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (string.Equals(lblSelectedDataAction.Text, "States Information", StringComparison.OrdinalIgnoreCase))
            {
                dtgImportDataPreview.Columns["StateID"].Visible = false;
                dtgImportDataPreview.Columns["StateCode"].Visible = false;
                dtgImportDataPreview.Columns["StateTitle"].HeaderText = "State Name";
                dtgImportDataPreview.Columns["StateTitle"].Width = 300;
                dtgImportDataPreview.Columns["StateTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["StateInitial"].HeaderText = "State Code";
                dtgImportDataPreview.Columns["StateInitial"].Width = 200;
                dtgImportDataPreview.Columns["StateInitial"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsConfigured"].Visible = false;
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (string.Equals(lblSelectedDataAction.Text, "Education Information", StringComparison.OrdinalIgnoreCase))
            {
                dtgImportDataPreview.Columns["EduQualID"].Visible = false;
                dtgImportDataPreview.Columns["EduQualCode"].Visible = false;
                dtgImportDataPreview.Columns["EduQualTitle"].HeaderText = "Education Name";
                dtgImportDataPreview.Columns["EduQualTitle"].Width = 300;
                dtgImportDataPreview.Columns["EduQualTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["EduQualInitial"].HeaderText = "Education Code";
                dtgImportDataPreview.Columns["EduQualInitial"].Width = 200;
                dtgImportDataPreview.Columns["EduQualInitial"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (string.Equals(lblSelectedDataAction.Text, "Weekly Off Information", StringComparison.OrdinalIgnoreCase))
            {
                dtgImportDataPreview.Columns["WklyOffMasID"].Visible = false;
                dtgImportDataPreview.Columns["WklyOffCode"].Visible = false;
                dtgImportDataPreview.Columns["WklyOffTitle"].HeaderText = "Weekly Off Profile Name";
                dtgImportDataPreview.Columns["WklyOffTitle"].Width = 300;
                dtgImportDataPreview.Columns["WklyOffTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["WklyOffDays"].HeaderText = "Weekly Days Off Days";
                dtgImportDataPreview.Columns["WklyOffDays"].Width = 300;
                dtgImportDataPreview.Columns["WklyOffTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["WklyOffEffectiveDate"].Visible = false;
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDelete"].Visible = false;
            }
            else if (string.Equals(lblSelectedDataAction.Text, "Asset Category Information", StringComparison.OrdinalIgnoreCase))
            {
                dtgImportDataPreview.Columns["AssetCatMasID"].Visible = false;
                dtgImportDataPreview.Columns["ParentAssetCatMasID"].Visible = false;
                dtgImportDataPreview.Columns["AssetCode"].Visible = false;
                dtgImportDataPreview.Columns["AssetName"].HeaderText = "Asset Category Name";
                dtgImportDataPreview.Columns["AssetName"].Width = 350;
                dtgImportDataPreview.Columns["AssetName"].ReadOnly = true;
                dtgImportDataPreview.Columns["AssetDescription"].HeaderText = "Asset Category Description";
                dtgImportDataPreview.Columns["AssetDescription"].Width = 350;
                dtgImportDataPreview.Columns["AssetDescription"].ReadOnly = true;
                dtgImportDataPreview.Columns["ParentCatMasName"].HeaderText = "Parent Category Asset";
                dtgImportDataPreview.Columns["ParentCatMasName"].Width = 350;
                dtgImportDataPreview.Columns["ParentCatMasName"].ReadOnly = true;
                dtgImportDataPreview.Columns["AssetNote"].Visible = false;
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
                dtgImportDataPreview.Columns["ClientID"].Visible = false;
            }
            else if (string.Equals(lblSelectedDataAction.Text, "Assets Information", StringComparison.OrdinalIgnoreCase))
            {
                dtgImportDataPreview.Columns["AssetID"].Visible = false;
                dtgImportDataPreview.Columns["AssetCode"].Visible = false;
                dtgImportDataPreview.Columns["AssetName"].HeaderText = "Asset Name";
                dtgImportDataPreview.Columns["AssetName"].Width = 350;
                dtgImportDataPreview.Columns["AssetName"].ReadOnly = true;
                dtgImportDataPreview.Columns["AssetDescription"].HeaderText = "Asset Description";
                dtgImportDataPreview.Columns["AssetDescription"].Width = 350;
                dtgImportDataPreview.Columns["AssetDescription"].ReadOnly = true;
                dtgImportDataPreview.Columns["AssetCategoryName"].HeaderText = "Asset Category";
                dtgImportDataPreview.Columns["AssetCategoryName"].Width = 350;
                dtgImportDataPreview.Columns["AssetCategoryName"].ReadOnly = true;
                dtgImportDataPreview.Columns["OutstandingQuantity"].Visible = false;
                dtgImportDataPreview.Columns["CurrentAssetStatusName"].Visible = false;
                dtgImportDataPreview.Columns["CurrentAssetDescription"].Visible = false;
                dtgImportDataPreview.Columns["AssetCatMasID"].Visible = false;
                dtgImportDataPreview.Columns["ParentAssetCatMasID"].Visible = false;
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (string.Equals(lblSelectedDataAction.Text, "Leave Type Information", StringComparison.OrdinalIgnoreCase))
            {
                dtgImportDataPreview.Columns["LeaveTypeID"].Visible = false;
                dtgImportDataPreview.Columns["LeaveCode"].Visible = false;
                dtgImportDataPreview.Columns["LeaveTypeTitle"].HeaderText = "Leave Type Name";
                dtgImportDataPreview.Columns["LeaveTypeTitle"].Width = 300;
                dtgImportDataPreview.Columns["LeaveTypeTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsPaid"].HeaderText = "Is Paid";
                dtgImportDataPreview.Columns["IsPaid"].Width = 75;
                dtgImportDataPreview.Columns["IsPaid"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDelete"].Visible = false;
                dtgImportDataPreview.Columns["OrderID"].Visible = false;
            }
            else if (string.Equals(lblSelectedDataAction.Text, "Skills Information", StringComparison.OrdinalIgnoreCase))
            {
                dtgImportDataPreview.Columns["SkillID"].Visible = false;
                dtgImportDataPreview.Columns["SkillCode"].Visible = false;
                dtgImportDataPreview.Columns["SkillTitle"].HeaderText = "Skills Name";
                dtgImportDataPreview.Columns["SkillTitle"].Width = 300;
                dtgImportDataPreview.Columns["SkillTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["SkillInitial"].HeaderText = "Skills Code";
                dtgImportDataPreview.Columns["SkillInitial"].Width = 200;
                dtgImportDataPreview.Columns["SkillInitial"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (string.Equals(lblSelectedDataAction.Text, "Relationship Information", StringComparison.OrdinalIgnoreCase))
            {
                dtgImportDataPreview.Columns["RelationshipID"].Visible = false;
                dtgImportDataPreview.Columns["RelationshipCode"].Visible = false;
                dtgImportDataPreview.Columns["RelationshipTitle"].HeaderText = "Relationship Name";
                dtgImportDataPreview.Columns["RelationshipTitle"].Width = 300;
                dtgImportDataPreview.Columns["RelationshipTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["RelationshipInitial"].HeaderText = "Relationship Code";
                dtgImportDataPreview.Columns["RelationshipInitial"].Width = 200;
                dtgImportDataPreview.Columns["RelationshipInitial"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (string.Equals(lblSelectedDataAction.Text, "Shift Information", StringComparison.OrdinalIgnoreCase))
            {
                dtgImportDataPreview.Columns["ShiftID"].Visible = false;
                dtgImportDataPreview.Columns["ShiftCode"].Visible = false;
                dtgImportDataPreview.Columns["ShiftTitle"].HeaderText = "Shift Title";
                dtgImportDataPreview.Columns["ShiftTitle"].Width = 300;
                dtgImportDataPreview.Columns["ShiftTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["ShiftStart"].HeaderText = "Shift Start";
                dtgImportDataPreview.Columns["ShiftStart"].DefaultCellStyle.Format = "hh:mm:ss tt";
                dtgImportDataPreview.Columns["ShiftStart"].DefaultCellStyle.NullValue = string.Empty;
                dtgImportDataPreview.Columns["ShiftStart"].ReadOnly = true;
                dtgImportDataPreview.Columns["ShiftEnd"].HeaderText = "Shift End";
                dtgImportDataPreview.Columns["ShiftEnd"].DefaultCellStyle.Format = "hh:mm:ss tt";
                dtgImportDataPreview.Columns["ShiftEnd"].DefaultCellStyle.NullValue = string.Empty;
                dtgImportDataPreview.Columns["ShiftEnd"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }

            dtgImportDataPreview.Refresh();
        }

        private void SetExportColumnVisible(string columnName, bool visible)
        {
            if (dtgImportDataPreview != null &&
                dtgImportDataPreview.Columns.Contains(columnName))
            {
                dtgImportDataPreview.Columns[columnName].Visible = visible;
            }
        }

        /// <summary>
        /// Returns the value exactly as displayed by the Export preview grid.
        /// This deliberately uses DataGridViewCell.FormattedValue instead of
        /// DataGridViewCell.Value so column formatting is preserved in every
        /// export format (CSV, XML, JSON, HTML and PDF).
        /// </summary>
        /// <summary>
        /// Returns the column title exactly as displayed in the DataGridView.
        /// HeaderText is intentionally preferred over the database column Name.
        /// </summary>
        private string GetExportDisplayColumnHeader(DataGridViewColumn column)
        {
            if (column == null)
                return string.Empty;

            string headerText = Convert.ToString(column.HeaderText);

            if (!string.IsNullOrWhiteSpace(headerText))
                return headerText.Trim();

            return Convert.ToString(column.Name);
        }

        private string GetFormattedExportCellValue(
            DataGridViewRow row,
            string columnName)
        {
            if (row == null || string.IsNullOrWhiteSpace(columnName))
                return string.Empty;

            if (!dtgImportDataPreview.Columns.Contains(columnName))
                return string.Empty;

            DataGridViewCell cell = row.Cells[columnName];

            if (cell == null)
                return string.Empty;

            object cellValue = cell.Value;

            if (cellValue == null || cellValue == DBNull.Value)
                return string.Empty;

            // First choice: use exactly what the DataGridView displays.
            // This preserves DefaultCellStyle.Format and any other normal
            // DataGridView formatting.
            object formattedValue = cell.FormattedValue;

            if (formattedValue != null && formattedValue != DBNull.Value)
            {
                string displayValue = Convert.ToString(
                    formattedValue,
                    CultureInfo.CurrentCulture);

                // If the cell has an explicit .NET format and the underlying
                // value is DateTime, apply the same format explicitly as a
                // defensive fallback. This prevents a DateTime such as
                // 30-Dec-1899 09:00:00 from leaking into the export.
                string format = cell.InheritedStyle != null
                    ? cell.InheritedStyle.Format
                    : string.Empty;

                if (!string.IsNullOrWhiteSpace(format) &&
                    cellValue is DateTime)
                {
                    return ((DateTime)cellValue).ToString(
                        format,
                        CultureInfo.CurrentCulture);
                }

                return displayValue;
            }

            // Final fallback for formatted DateTime cells.
            string inheritedFormat = cell.InheritedStyle != null
                ? cell.InheritedStyle.Format
                : string.Empty;

            if (cellValue is DateTime)
            {
                DateTime dateTimeValue = (DateTime)cellValue;

                if (!string.IsNullOrWhiteSpace(inheritedFormat))
                {
                    return dateTimeValue.ToString(
                        inheritedFormat,
                        CultureInfo.CurrentCulture);
                }

                return dateTimeValue.ToString(
                    CultureInfo.CurrentCulture);
            }

            return Convert.ToString(
                cellValue,
                CultureInfo.CurrentCulture);
        }

        private DataTable GetSelectedExportData()
        {
            DataTable selectedData = new DataTable();

            if (dtgImportDataPreview == null ||
                dtgImportDataPreview.Rows.Count == 0 ||
                !dtgImportDataPreview.Columns.Contains("IsSelected"))
            {
                return selectedData;
            }

            foreach (DataGridViewColumn column in dtgImportDataPreview.Columns)
            {
                if (column.Name == "IsSelected")
                    continue;

                if (!column.Visible)
                    continue;

                // IMPORTANT:
                // Export the column title exactly as it is displayed in the
                // DataGridView UI.  Do not use the underlying database column
                // name here.
                //
                // Example:
                //     Database column : ShiftStart
                //     Grid title      : Shift Start
                //     Export title    : Shift Start
                //
                // This makes the CSV, JSON, HTML and PDF headers match the UI.
                // XML also retains the exact UI title through the DisplayHeader
                // metadata written by ExportDataTableToXml().
                string columnName = GetExportDisplayColumnHeader(column);

                if (string.IsNullOrWhiteSpace(columnName))
                    columnName = "Column" + selectedData.Columns.Count.ToString();

                // DataTable column names must be unique. Keep the UI title for
                // the first occurrence and add a minimal suffix only if two
                // visible grid columns have exactly the same title.
                string originalColumnName = columnName;
                int duplicateIndex = 2;

                while (selectedData.Columns.Contains(columnName))
                {
                    columnName = originalColumnName + " (" +
                                 duplicateIndex.ToString() + ")";
                    duplicateIndex++;
                }

                selectedData.Columns.Add(columnName, typeof(string));
            }

            foreach (DataGridViewRow row in dtgImportDataPreview.Rows)
            {
                if (row.IsNewRow)
                    continue;

                bool isSelected = false;

                if (row.Cells["IsSelected"].Value != null)
                {
                    bool.TryParse(
                        row.Cells["IsSelected"].Value.ToString(),
                        out isSelected);
                }

                if (!isSelected)
                    continue;

                DataRow dataRow = selectedData.NewRow();
                int outputColumnIndex = 0;

                foreach (DataGridViewColumn column in dtgImportDataPreview.Columns)
                {
                    if (column.Name == "IsSelected" || !column.Visible)
                        continue;

                    // IMPORTANT:
                    // Export the value exactly as it is displayed in the
                    // DataGridView, including the column's formatting.
                    // For example, a DateTime column displayed as
                    // "hh:mm:ss tt" is exported as "09:00:00 AM" and not
                    // as the underlying DateTime value (30-Dec-1899 09:00:00).
                    string displayValue =
                        GetFormattedExportCellValue(row, column.Name);

                    // Store the displayed grid value as a string.
                    // This is intentional: CSV, JSON, XML, HTML and PDF must
                    // not re-serialize the original database DateTime.
                    dataRow[outputColumnIndex] = displayValue;

                    outputColumnIndex++;
                }

                selectedData.Rows.Add(dataRow);
            }

            return selectedData;
        }

        private void ExportSelectedData()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(lblSelectedDataAction.Text))
                {
                    MessageBox.Show(
                        "Please select an export data source.",
                        "Staffsync",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                if (dtgImportDataPreview == null ||
                    dtgImportDataPreview.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "No data is available for export.",
                        "Staffsync",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                DataTable selectedData = GetSelectedExportData();

                if (selectedData.Rows.Count == 0)
                {
                    MessageBox.Show(
                        "Please select at least one row to export.",
                        "Staffsync",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                string format = Convert.ToString(cmbOutputFormat.SelectedItem);

                if (string.IsNullOrWhiteSpace(format))
                {
                    MessageBox.Show(
                        "Please select an export format.",
                        "Staffsync",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                string extension = format.Trim().TrimStart('.').ToLowerInvariant();
                string filter;

                switch (extension)
                {
                    case "pdf":
                        filter = "PDF Files (*.pdf)|*.pdf";
                        break;
                    case "csv":
                        filter = "CSV Files (*.csv)|*.csv";
                        break;
                    case "xml":
                        filter = "XML Files (*.xml)|*.xml";
                        break;
                    case "json":
                        filter = "JSON Files (*.json)|*.json";
                        break;
                    default:
                        MessageBox.Show(
                            "Unsupported export format: " + format,
                            "Staffsync",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        return;
                }

                string defaultFileName =
                    MakeSafeExportFileName(lblSelectedDataAction.Text) +
                    "_" +
                    DateTime.Now.ToString("yyyyMMdd_HHmmss") +
                    "." + extension;

                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Title = "Export StaffSync Data";
                    saveFileDialog.Filter = filter;
                    saveFileDialog.DefaultExt = extension;
                    saveFileDialog.AddExtension = true;
                    saveFileDialog.FileName = defaultFileName;
                    saveFileDialog.OverwritePrompt = true;
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog(this) != DialogResult.OK)
                        return;

                    string outputFile = saveFileDialog.FileName;

                    switch (extension)
                    {
                        case "csv":
                            ExportDataTableToCsv(selectedData, outputFile);
                            break;
                        case "xml":
                            ExportDataTableToXml(selectedData, outputFile);
                            break;
                        case "json":
                            ExportDataTableToJson(selectedData, outputFile);
                            break;
                        case "pdf":
                            ExportDataTableToPdf(selectedData, outputFile);
                            break;
                    }

                    if (!File.Exists(outputFile))
                        throw new IOException("The export file could not be created.");

                    string fileToOpen = outputFile;
                    string successMessage =
                        selectedData.Rows.Count.ToString() +
                        " row(s) exported successfully." +
                        Environment.NewLine + Environment.NewLine +
                        outputFile;

                    if (extension == "xml")
                    {
                        string htmlPreviewFile = Path.Combine(
                            Path.GetDirectoryName(outputFile),
                            Path.GetFileNameWithoutExtension(outputFile) + ".html");

                        if (File.Exists(htmlPreviewFile))
                        {
                            fileToOpen = htmlPreviewFile;
                            successMessage +=
                                Environment.NewLine + Environment.NewLine +
                                "HTML preview:" +
                                Environment.NewLine +
                                htmlPreviewFile;
                        }
                    }

                    MessageBox.Show(
                        successMessage,
                        "Staffsync",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    System.Diagnostics.Process.Start(
                        new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = fileToOpen,
                            UseShellExecute = true
                        });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to export data." +
                    Environment.NewLine + Environment.NewLine +
                    ex.Message,
                    "Staffsync - Export Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string MakeSafeExportFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return "StaffSyncExport";

            foreach (char invalidCharacter in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(invalidCharacter, '_');
            }

            return fileName.Trim();
        }

        private string EscapeExportCsvValue(string value)
        {
            if (value == null)
                return string.Empty;

            if (value.Contains(",") ||
                value.Contains("\"") ||
                value.Contains("\r") ||
                value.Contains("\n"))
            {
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            }

            return value;
        }

        private void ExportDataTableToCsv(DataTable data, string outputFile)
        {
            StringBuilder csv = new StringBuilder();

            for (int i = 0; i < data.Columns.Count; i++)
            {
                if (i > 0)
                    csv.Append(",");

                csv.Append(EscapeExportCsvValue(data.Columns[i].ColumnName));
            }

            csv.AppendLine();

            foreach (DataRow row in data.Rows)
            {
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    if (i > 0)
                        csv.Append(",");

                    csv.Append(
                        EscapeExportCsvValue(
                            row[i] == DBNull.Value
                                ? string.Empty
                                : Convert.ToString(row[i])));
                }

                csv.AppendLine();
            }

            File.WriteAllText(outputFile, csv.ToString(), new UTF8Encoding(true));
        }

        private void ExportDataTableToXml(DataTable data, string outputFile)
        {
            // Keep the XML as a clean, machine-readable export.
            // A standalone HTML companion is also generated from the same data so the
            // exported information can be opened reliably in Chrome/Edge without
            // depending on local-file XSLT security behavior.
            using (System.Xml.XmlWriter writer =
                   System.Xml.XmlWriter.Create(
                       outputFile,
                       new System.Xml.XmlWriterSettings
                       {
                           Indent = true,
                           Encoding = new UTF8Encoding(false)
                       }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("StaffSyncExport");
                writer.WriteAttributeString("DataType", lblSelectedDataAction.Text);
                writer.WriteAttributeString(
                    "GeneratedOn",
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                writer.WriteStartElement("Rows");

                foreach (DataRow row in data.Rows)
                {
                    writer.WriteStartElement("Row");

                    foreach (DataColumn column in data.Columns)
                    {
                        // XML element names cannot contain spaces, so the
                        // machine-readable element name is made XML-safe.
                        // The exact DataGridView UI title is preserved in the
                        // DisplayHeader attribute.
                        writer.WriteStartElement(
                            MakeSafeXmlName(column.ColumnName));

                        writer.WriteAttributeString(
                            "DisplayHeader",
                            column.ColumnName);

                        if (row[column] != DBNull.Value)
                        {
                            // GetSelectedExportData stores the DataGridView
                            // formatted/display value as a string, so XML
                            // receives exactly the value shown in the preview.
                            writer.WriteString(Convert.ToString(row[column]));
                        }

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }

            // Generate a self-contained HTML view next to the XML file.
            // This is intentionally generated without external CSS/XSL files so the
            // user can double-click the HTML file and view the export immediately.
            string htmlFile = Path.Combine(
                Path.GetDirectoryName(outputFile),
                Path.GetFileNameWithoutExtension(outputFile) + ".html");

            ExportDataTableToHtml(data, htmlFile);
        }

        private void ExportDataTableToHtml(DataTable data, string outputFile)
        {
            StringBuilder html = new StringBuilder();

            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine("<meta charset=\"utf-8\" />");
            html.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />");
            html.AppendLine("<title>StaffSync Export - " + HtmlEncode(lblSelectedDataAction.Text) + "</title>");
            html.AppendLine("<style>");
            html.AppendLine("body{font-family:Segoe UI,Arial,sans-serif;background:#f5f7fa;margin:0;padding:24px;color:#202124;}");
            html.AppendLine(".report{max-width:1400px;margin:0 auto;background:#fff;border:1px solid #d9dee7;border-radius:10px;box-shadow:0 2px 8px rgba(0,0,0,.08);overflow:hidden;}");
            html.AppendLine(".header{padding:20px 24px;border-bottom:1px solid #e3e7ed;background:#f8fafc;}");
            html.AppendLine(".title{font-size:22px;font-weight:600;margin:0 0 6px 0;}");
            html.AppendLine(".meta{font-size:13px;color:#667085;}");
            html.AppendLine(".table-wrap{overflow:auto;padding:18px 24px 24px 24px;}");
            html.AppendLine("table{border-collapse:collapse;width:100%;min-width:650px;font-size:13px;}");
            html.AppendLine("th{background:#eef2f7;font-weight:600;text-align:left;border:1px solid #d9dee7;padding:9px 10px;white-space:nowrap;}");
            html.AppendLine("td{border:1px solid #e1e5eb;padding:8px 10px;vertical-align:top;}");
            html.AppendLine("tr:nth-child(even) td{background:#fafbfc;}");
            html.AppendLine(".footer{padding:12px 24px;border-top:1px solid #e3e7ed;color:#667085;font-size:12px;}");
            html.AppendLine("</style>");
            html.AppendLine("</head>");
            html.AppendLine("<body>");
            html.AppendLine("<div class=\"report\">");
            html.AppendLine("<div class=\"header\">");
            html.AppendLine("<div class=\"title\">StaffSync Export - " + HtmlEncode(lblSelectedDataAction.Text) + "</div>");
            html.AppendLine("<div class=\"meta\">Generated On: " + HtmlEncode(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")) + "</div>");
            html.AppendLine("</div>");
            html.AppendLine("<div class=\"table-wrap\">");
            html.AppendLine("<table>");
            html.AppendLine("<thead><tr>");

            foreach (DataColumn column in data.Columns)
            {
                html.Append("<th>");
                html.Append(HtmlEncode(column.ColumnName));
                html.AppendLine("</th>");
            }

            html.AppendLine("</tr></thead>");
            html.AppendLine("<tbody>");

            foreach (DataRow row in data.Rows)
            {
                html.AppendLine("<tr>");

                foreach (DataColumn column in data.Columns)
                {
                    html.Append("<td>");

                    if (row[column] != DBNull.Value)
                    {
                        html.Append(HtmlEncode(Convert.ToString(row[column])));
                    }

                    html.AppendLine("</td>");
                }

                html.AppendLine("</tr>");
            }

            html.AppendLine("</tbody>");
            html.AppendLine("</table>");
            html.AppendLine("</div>");
            html.AppendLine("<div class=\"footer\">StaffSync Payroll Management - Exported rows: " + data.Rows.Count.ToString() + "</div>");
            html.AppendLine("</div>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            File.WriteAllText(outputFile, html.ToString(), new UTF8Encoding(false));
        }

        private string HtmlEncode(string value)
        {
            if (value == null)
                return string.Empty;

            return System.Net.WebUtility.HtmlEncode(value);
        }

        private string MakeSafeXmlName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return "Column";

            StringBuilder result = new StringBuilder();

            foreach (char character in name)
            {
                if (char.IsLetterOrDigit(character) || character == '_')
                    result.Append(character);
                else
                    result.Append('_');
            }

            if (result.Length == 0)
                return "Column";

            if (char.IsDigit(result[0]))
                result.Insert(0, '_');

            return result.ToString();
        }

        private void ExportDataTableToJson(DataTable data, string outputFile)
        {
            List<Dictionary<string, object>> rows =
                new List<Dictionary<string, object>>();

            foreach (DataRow row in data.Rows)
            {
                Dictionary<string, object> item =
                    new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

                foreach (DataColumn column in data.Columns)
                {
                    // Values in selectedData are already the formatted
                    // DataGridView display values. Keep them as strings so
                    // JSON does not re-serialize the original DateTime.
                    item[column.ColumnName] =
                        row[column] == DBNull.Value ? null : row[column].ToString();
                }

                rows.Add(item);
            }

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(
                rows,
                Newtonsoft.Json.Formatting.Indented);

            File.WriteAllText(outputFile, json, new UTF8Encoding(true));
        }

        private void ExportDataTableToPdf(DataTable data, string outputFile)
        {
            using (FileStream stream = new FileStream(outputFile, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                iTextSharp.text.Document document =
                    new iTextSharp.text.Document(
                        iTextSharp.text.PageSize.A4.Rotate(),
                        20f,
                        20f,
                        25f,
                        25f);

                PdfWriter.GetInstance(document, stream);
                document.Open();

                iTextSharp.text.Font titleFont =
                    FontFactory.GetFont(
                        FontFactory.HELVETICA_BOLD,
                        16f);

                iTextSharp.text.Font infoFont =
                    FontFactory.GetFont(
                        FontFactory.HELVETICA,
                        8f);

                document.Add(
                    new Paragraph(
                        lblSelectedDataAction.Text,
                        titleFont));

                document.Add(
                    new Paragraph(
                        "Generated: " +
                        DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss"),
                        infoFont));

                document.Add(new Paragraph(" "));

                int columnCount = data.Columns.Count;

                PdfPTable table =
                    new PdfPTable(columnCount);

                table.WidthPercentage = 100f;
                table.HeaderRows = 1;

                iTextSharp.text.Font headerFont =
                    FontFactory.GetFont(
                        FontFactory.HELVETICA_BOLD,
                        8f);

                iTextSharp.text.Font cellFont =
                    FontFactory.GetFont(
                        FontFactory.HELVETICA,
                        7f);

                foreach (DataColumn column in data.Columns)
                {
                    PdfPCell headerCell =
                        new PdfPCell(
                            new Phrase(
                                column.ColumnName,
                                headerFont));

                    headerCell.HorizontalAlignment =
                        Element.ALIGN_CENTER;

                    headerCell.VerticalAlignment =
                        Element.ALIGN_MIDDLE;

                    headerCell.BackgroundColor =
                        new BaseColor(230, 230, 230);

                    table.AddCell(headerCell);
                }

                foreach (DataRow row in data.Rows)
                {
                    foreach (DataColumn column in data.Columns)
                    {
                        string value =
                            row[column] == DBNull.Value
                                ? string.Empty
                                : Convert.ToString(row[column]);

                        PdfPCell cell =
                            new PdfPCell(
                                new Phrase(value, cellFont));

                        cell.VerticalAlignment =
                            Element.ALIGN_MIDDLE;

                        table.AddCell(cell);
                    }
                }

                document.Add(table);
                document.Close();
            }
        }

        private void InitializeExportFormats()
        {
            cmbOutputFormat.Items.Clear();
            cmbOutputFormat.Items.Add("CSV");
            cmbOutputFormat.Items.Add("JSON");
            cmbOutputFormat.Items.Add("PDF");
            cmbOutputFormat.Items.Add("XML");

            if (cmbOutputFormat.Items.Count > 0)
                cmbOutputFormat.SelectedIndex = 0;
        }


        public void clearControls()
        {
            lblTotalRowsFromSource.Text = "Total Rows from Source : 0";
            lblTotalRowsSelected.Text = "Total Rows Selected : 0";
            lblTotalImportedRows.Text = "Total Rows Imported : 0";
            lblTotalDuplicateRows.Text = "Total Duplicate Rows : 0";
            lblTotalNotImportedRows.Text = "Total Rows Not Imported : 0";

            InitializeExportFormats();
        }

        public void enableControls()
        {
            //DailyAttendanceSheet.Enabled = true;
        }

        public void disableControls()
        {
            btnSaveDetails.Enabled = false;
            //txtLeaveApprovalDate.Enabled = false;
            //dtgBulkLeaveApproval.Enabled = false;
        }

        public void onGenerateButtonClick()
        {
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = dtgImportDataPreview != null &&
                                      dtgImportDataPreview.Rows.Count > 0;
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
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onSaveButtonClick()
        {
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onCancelButtonClick()
        {
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = true;
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
            //LoadAttendanceInfo();
            //attendanceGridControl1.SetCellValue(1, 15, "P"); // row 1, column 3 → "P"
        }

        private void attendanceGridControl1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("Double Clicked on the grid", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmExportDataProcess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                objDashboard.lblDashboardTitle.Text = "Dashboard";
                objDashboard.sptrDashboardContainer.Visible = true;
                objDashboard.grpDashboardDateRange.Visible = true;
                objDashboard.refreshDashboardCharts();
                this.Close();
            }
        }

        private void frmExportDataProcess_Activated(object sender, EventArgs e)
        {
            dtgImportDataSourceList.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
        }

        private void dtgConsolidatedAttendanceReport_Paint(object sender, PaintEventArgs e)
        {
            KryptonDataGridView dgv = sender as KryptonDataGridView;

            if (dgv.Rows.Count == 0)
            {
                string message = "No Data Available";

                using (System.Drawing.Font font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold))
                {
                    SizeF size = e.Graphics.MeasureString(message, font);

                    e.Graphics.DrawString(message, font, System.Drawing.Brushes.Gray, (dgv.Width - size.Width) / 2, (dgv.Height - size.Height) / 2);
                }
            }
        }

        private void dtgImportDataSourceList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || dtgImportDataSourceList.Rows[e.RowIndex].IsNewRow)
                {
                    return;
                }

                if (dtgImportDataSourceList.Columns.Contains("ImpDataInfoTitle"))
                {
                    lblSelectedDataAction.Text = Convert.ToString(dtgImportDataSourceList.Rows[e.RowIndex].Cells["ImpDataInfoTitle"].Value).Trim();
                }
                else
                {
                    lblSelectedDataAction.Text = Convert.ToString(dtgImportDataSourceList.Rows[e.RowIndex].Cells[3].Value).Trim();
                }

                LoadSelectedExportData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unable to load the selected export data." +
                    Environment.NewLine + Environment.NewLine +
                    ex.Message,
                    "Staffsync - Export Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void chkSelectOrUnselect_Click(object sender, EventArgs e)
        {
            if (dtgImportDataPreview == null ||
                !dtgImportDataPreview.Columns.Contains("IsSelected"))
            {
                chkSelectOrUnselect.Text =
                    chkSelectOrUnselect.Checked
                        ? "Unselect All"
                        : "Select All";

                lblTotalRowsSelected.Text =
                    "Total Rows Selected : 0";
                return;
            }

            bool selectAll = chkSelectOrUnselect.Checked;

            chkSelectOrUnselect.Text =
                selectAll ? "Unselect All" : "Select All";

            foreach (DataGridViewRow row in dtgImportDataPreview.Rows)
            {
                if (row.IsNewRow)
                    continue;

                row.Cells["IsSelected"].Value = selectAll;
            }

            UpdateExportSelectedRowCount();
        }

    }
}
