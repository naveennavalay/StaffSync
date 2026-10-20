using DALStaffSync;
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
        DALStaffSync.clsEmployeeMaster objEmployeeInfo = new DALStaffSync.clsEmployeeMaster();
        DALStaffSync.clsDashboardChartWidgets objDashboardChartWidgets = new DALStaffSync.clsDashboardChartWidgets();
        DALStaffSync.clsClientStatutory objClientStatutory = new DALStaffSync.clsClientStatutory();
        DALStaffSync.clsClientBranchInfo clsClientBranchInfo = new DALStaffSync.clsClientBranchInfo();
        clsImpageOperation objImpageOperation = new clsImpageOperation();
        DALStaffSync.clsPhotoMas objPhotoMas = new DALStaffSync.clsPhotoMas();
        DALStaffSync.clsImportDataInfo objImportDataInfo = new DALStaffSync.clsImportDataInfo();
        DALStaffSync.clsDesignation objDesignation = new DALStaffSync.clsDesignation();
        DALStaffSync.clsDepartment objDepartment = new DALStaffSync.clsDepartment();
        DALStaffSync.clsEmpPersonalFamilyMemberInfo objEmpPersonalFamilyMemberInfo = new DALStaffSync.clsEmpPersonalFamilyMemberInfo();
        DALStaffSync.clsEmpNomineeMas objEmpNomineeMas = new DALStaffSync.clsEmpNomineeMas();
        DALStaffSync.clsEmpEduQualMas objEmpEduQualMas = new DALStaffSync.clsEmpEduQualMas();
        DALStaffSync.clsEmpLeaveEntitlementInfo objEmpLeaveEntitlementInfo = new DALStaffSync.clsEmpLeaveEntitlementInfo();
        DALStaffSync.clsCountries objCountries = new DALStaffSync.clsCountries();
        DALStaffSync.clsStates objStates = new DALStaffSync.clsStates();
        DALStaffSync.clsEduQalification objEduQalification = new DALStaffSync.clsEduQalification();
        DALStaffSync.clsEmpSkillsMas objEmpSkillsMas = new DALStaffSync.clsEmpSkillsMas();
        DALStaffSync.clsEmployeeAdvanceInfo objEmployeeAdvanceInfo = new DALStaffSync.clsEmployeeAdvanceInfo();
        DALStaffSync.clsEmployeeBankACInfo objEmployeeBankACInfo = new clsEmployeeBankACInfo();
        DALStaffSync.clsEmpPayroll objEmpPayroll = new DALStaffSync.clsEmpPayroll();
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
        DALStaffSync.clsPublicHolidayInfo objPublicHolidayInfo = new DALStaffSync.clsPublicHolidayInfo();

        frmDashboard objDashboard = (frmDashboard)System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        List<ClientInfo> objActiveClientInfo = new List<ClientInfo>();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();

        // Import/Export preview state

        DateTime dtFromDate = DateTime.Today;
        DateTime dtToDate = DateTime.Today;

        private DataTable objImportDataPreviewTable = new DataTable();
        // Employee Information keeps its complete detail rows separately from the
        // one-row-per-employee preview grid so selecting an employee still exports
        // all optional detail records without duplicating employees in the UI.
        private DataTable objEmployeeInformationExportTable = new DataTable();
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

        public frmExportDataProcess(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo, DateTime dtFrom, DateTime dtTo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
            objActiveClientInfo = objClientInfo.getClientInfoByEmpID(objTempCurrentlyLoggedInUserInfo.EmpID);

            dtgImportDataSourceList.DataSource = objImportDataInfo.getImportInfoData();
            formatGrid();
            defaultSelectedImportOption();
            dtFromDate = dtFrom;
            dtToDate = dtTo;
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
        /// PLACEHOLDER: Replace this method body with your actual DAL call.
        /// Return all family-member rows for the employees being exported.
        /// Required columns:
        /// EmpID, FamMemName, FamMemDOB, FamMemAge, FamMemRelationship,
        /// FamMemAddr1, FamMemAddr2, FamMemArea, FamMemCity, FamMemState,
        /// FamMemPIN, FamMemCountry, FamMemContactNumber, FamMemMailID,
        /// FamMemBloodGroup, FamMemInsuranceEnrolled.
        /// Do not hard-code a single EmpID; the export contains multiple employees.
        /// </summary>
        private DataTable GetFamilyMembersInformation()
        {
            // TODO: Call your DAL method here and return its DataTable.
            // Example placeholder return keeps the form compilable until the DAL is added.
            DataTable familyMembers = new DataTable("FamilyMembersInformation");
            familyMembers.Columns.Add("EmpID", typeof(int));
            familyMembers.Columns.Add("FamMemName", typeof(string));
            familyMembers.Columns.Add("FamMemDOB", typeof(DateTime));
            familyMembers.Columns.Add("FamMemAge", typeof(int));
            familyMembers.Columns.Add("FamMemRelationship", typeof(string));
            familyMembers.Columns.Add("FamMemAddr1", typeof(string));
            familyMembers.Columns.Add("FamMemAddr2", typeof(string));
            familyMembers.Columns.Add("FamMemArea", typeof(string));
            familyMembers.Columns.Add("FamMemCity", typeof(string));
            familyMembers.Columns.Add("FamMemState", typeof(string));
            familyMembers.Columns.Add("FamMemPIN", typeof(string));
            familyMembers.Columns.Add("FamMemCountry", typeof(string));
            familyMembers.Columns.Add("FamMemContactNumber", typeof(string));
            familyMembers.Columns.Add("FamMemMailID", typeof(string));
            familyMembers.Columns.Add("FamMemBloodGroup", typeof(string));
            familyMembers.Columns.Add("FamMemInsuranceEnrolled", typeof(bool));
            return familyMembers;
        }

        /// <summary>
        /// Combines employee data with family-member data by EmpID.
        /// An employee with multiple family members produces one row per family member.
        /// Employees without family records are retained with blank family columns.
        /// </summary>
        private DataTable MergeEmployeeAndFamilyMemberInformation(
            DataTable employees, DataTable familyMembers)
        {
            if (employees == null)
                return new DataTable();

            if (familyMembers == null)
                familyMembers = new DataTable();

            string[] familyColumns = new string[]
            {
                "FamMemName", "FamMemDOB", "FamMemAge", "FamMemRelationship",
                "FamMemAddr1", "FamMemAddr2", "FamMemArea", "FamMemCity",
                "FamMemState", "FamMemPIN", "FamMemCountry",
                "FamMemContactNumber", "FamMemMailID", "FamMemBloodGroup",
                "FamMemInsuranceEnrolled"
            };

            DataTable merged = employees.Clone();
            foreach (string columnName in familyColumns)
            {
                if (!merged.Columns.Contains(columnName))
                {
                    Type columnType = familyMembers.Columns.Contains(columnName)
                        ? familyMembers.Columns[columnName].DataType
                        : typeof(string);
                    merged.Columns.Add(columnName, columnType);
                }
            }

            bool hasEmployeeId = employees.Columns.Contains("EmpID");
            bool hasFamilyEmployeeId = familyMembers.Columns.Contains("EmpID");
            if (!hasEmployeeId || !hasFamilyEmployeeId)
            {
                // Do not silently join data without the key needed to match records.
                throw new InvalidOperationException(
                    "Family Members Information requires an EmpID column in both data sets.");
            }

            foreach (DataRow employeeRow in employees.Rows)
            {
                object employeeId = employeeRow["EmpID"];
                List<DataRow> matchingFamilyRows = new List<DataRow>();

                foreach (DataRow familyRow in familyMembers.Rows)
                {
                    if (familyRow["EmpID"] != DBNull.Value && employeeId != DBNull.Value &&
                        Convert.ToString(familyRow["EmpID"]) == Convert.ToString(employeeId))
                    {
                        matchingFamilyRows.Add(familyRow);
                    }
                }

                if (matchingFamilyRows.Count == 0)
                {
                    DataRow mergedRow = merged.NewRow();
                    CopyEmployeeColumns(employeeRow, mergedRow, employees.Columns);
                    merged.Rows.Add(mergedRow);
                    continue;
                }

                foreach (DataRow familyRow in matchingFamilyRows)
                {
                    DataRow mergedRow = merged.NewRow();
                    CopyEmployeeColumns(employeeRow, mergedRow, employees.Columns);

                    foreach (string columnName in familyColumns)
                    {
                        if (familyMembers.Columns.Contains(columnName))
                            mergedRow[columnName] = familyRow[columnName] ?? DBNull.Value;
                    }

                    merged.Rows.Add(mergedRow);
                }
            }

            return merged;
        }

        /// <summary>
        /// PLACEHOLDER: Returns nominee information for the employees included
        /// in the Employee Information export.
        ///
        /// Based on the confirmed query, the expected columns are:
        /// EmpID, NomineePerson, ContactNumber, RelationShipTitle.
        ///
        /// Replace the empty DataTable below with the actual DAL/database call.
        /// The final implementation must return nominees for all exported
        /// employees, not only EmpID = 1.
        /// </summary>
        private DataTable GetEmployeeNomineeInformation()
        {
            // TODO: Replace this placeholder with the actual nominee DAL call.
            DataTable nomineeInformation = new DataTable("EmployeeNomineeInformation");
            nomineeInformation.Columns.Add("EmpID", typeof(int));
            nomineeInformation.Columns.Add("NomineePerson", typeof(string));
            nomineeInformation.Columns.Add("ContactNumber", typeof(string));
            nomineeInformation.Columns.Add("RelationShipTitle", typeof(string));
            return nomineeInformation;
        }

        /// <summary>
        /// Combines employee data with nominee information by EmpID.
        /// An employee with multiple nominees produces one row per nominee.
        /// Employees without nominee records are retained with blank nominee columns.
        /// </summary>
        private DataTable MergeEmployeeAndNomineeInformation(
            DataTable employees, DataTable nomineeInformation)
        {
            if (employees == null)
                return new DataTable();

            if (nomineeInformation == null)
                nomineeInformation = new DataTable();

            string[] nomineeColumns = new string[]
            {
                "NomineePerson", "ContactNumber", "RelationShipTitle"
            };

            DataTable merged = employees.Clone();
            foreach (string columnName in nomineeColumns)
            {
                if (!merged.Columns.Contains(columnName))
                {
                    Type columnType = nomineeInformation.Columns.Contains(columnName)
                        ? nomineeInformation.Columns[columnName].DataType
                        : typeof(string);
                    merged.Columns.Add(columnName, columnType);
                }
            }

            if (!employees.Columns.Contains("EmpID") ||
                !nomineeInformation.Columns.Contains("EmpID"))
            {
                throw new InvalidOperationException(
                    "Employee Nominee Information requires an EmpID column in both data sets.");
            }

            foreach (DataRow employeeRow in employees.Rows)
            {
                object employeeId = employeeRow["EmpID"];
                List<DataRow> matchingNomineeRows = new List<DataRow>();

                foreach (DataRow nomineeRow in nomineeInformation.Rows)
                {
                    if (nomineeRow["EmpID"] != DBNull.Value && employeeId != DBNull.Value &&
                        Convert.ToString(nomineeRow["EmpID"]) == Convert.ToString(employeeId))
                    {
                        matchingNomineeRows.Add(nomineeRow);
                    }
                }

                if (matchingNomineeRows.Count == 0)
                {
                    DataRow mergedRow = merged.NewRow();
                    CopyEmployeeColumns(employeeRow, mergedRow, employees.Columns);
                    merged.Rows.Add(mergedRow);
                    continue;
                }

                foreach (DataRow nomineeRow in matchingNomineeRows)
                {
                    DataRow mergedRow = merged.NewRow();
                    CopyEmployeeColumns(employeeRow, mergedRow, employees.Columns);

                    foreach (string columnName in nomineeColumns)
                    {
                        if (nomineeInformation.Columns.Contains(columnName))
                            mergedRow[columnName] = nomineeRow[columnName] ?? DBNull.Value;
                    }

                    merged.Rows.Add(mergedRow);
                }
            }

            return merged;
        }


        /// <summary>
        /// PLACEHOLDER: Returns education qualification records for employees
        /// included in the Employee Information export.
        ///
        /// Expected columns are EmpID and EduQualTitle. The DAL implementation
        /// should return one row for each qualification assigned to an employee,
        /// allowing an employee to have multiple qualifications.
        ///
        /// TODO: Replace this empty DataTable with the employee-specific DAL call.
        /// Do not use the education qualification master list here: that list
        /// contains available qualification types, not qualifications earned by
        /// each employee.
        /// </summary>
        private DataTable GetEmployeeEducationQualificationInformation()
        {
            // TODO: Replace with the relevant DAL method/query.
            // Suggested result shape: EmpID (int), EduQualTitle (string).
            DataTable educationQualificationInformation =
                new DataTable("EmployeeEducationQualificationInformation");
            educationQualificationInformation.Columns.Add("EmpID", typeof(int));
            educationQualificationInformation.Columns.Add("EduQualTitle", typeof(string));
            return educationQualificationInformation;
        }

        /// <summary>
        /// PLACEHOLDER: Returns employee skill records for the Employee Information export.
        ///
        /// Expected columns: EmpID and SkillTitle. Return one row per skill assigned to
        /// an employee. The employee-specific data is expected to come from the
        /// EmpMas -> EmpSkillMas -> SkillsMas relationship.
        ///
        /// TODO: Replace this placeholder with the actual DAL/query. Do not use
        /// objSkillsMas.GetSkillList() here because it returns the skills master list,
        /// not the skills assigned to each employee.
        /// </summary>
        private DataTable GetEmployeeSkillsInformation()
        {
            // TODO: Replace with the employee-specific Skills DAL method/query.
            DataTable employeeSkillsInformation = new DataTable("EmployeeSkillsInformation");
            employeeSkillsInformation.Columns.Add("EmpID", typeof(int));
            employeeSkillsInformation.Columns.Add("SkillTitle", typeof(string));
            employeeSkillsInformation.Columns.Add("ExpertiseLevel", typeof(int));
            return employeeSkillsInformation;
        }

        /// <summary>
        /// Combines employee data with education qualification information by EmpID.
        /// An employee with multiple qualifications produces one row per qualification.
        /// Employees without qualification records are retained with a blank qualification.
        /// </summary>
        private DataTable MergeEmployeeAndEducationQualificationInformation(
            DataTable employees, DataTable educationQualificationInformation)
        {
            if (employees == null)
                return new DataTable();

            if (educationQualificationInformation == null)
                educationQualificationInformation = new DataTable();

            const string qualificationColumn = "EduQualTitle";
            DataTable merged = employees.Clone();

            if (!merged.Columns.Contains(qualificationColumn))
            {
                Type qualificationType = educationQualificationInformation.Columns.Contains(qualificationColumn)
                    ? educationQualificationInformation.Columns[qualificationColumn].DataType
                    : typeof(string);
                merged.Columns.Add(qualificationColumn, qualificationType);
            }

            if (!employees.Columns.Contains("EmpID") ||
                !educationQualificationInformation.Columns.Contains("EmpID"))
            {
                throw new InvalidOperationException(
                    "Employee Education Qualification Information requires an EmpID column in both data sets.");
            }

            foreach (DataRow employeeRow in employees.Rows)
            {
                object employeeId = employeeRow["EmpID"];
                List<DataRow> matchingQualifications = new List<DataRow>();

                foreach (DataRow qualificationRow in educationQualificationInformation.Rows)
                {
                    if (qualificationRow["EmpID"] != DBNull.Value && employeeId != DBNull.Value &&
                        Convert.ToString(qualificationRow["EmpID"]) == Convert.ToString(employeeId))
                    {
                        matchingQualifications.Add(qualificationRow);
                    }
                }

                if (matchingQualifications.Count == 0)
                {
                    DataRow mergedRow = merged.NewRow();
                    CopyEmployeeColumns(employeeRow, mergedRow, employees.Columns);
                    merged.Rows.Add(mergedRow);
                    continue;
                }

                foreach (DataRow qualificationRow in matchingQualifications)
                {
                    DataRow mergedRow = merged.NewRow();
                    CopyEmployeeColumns(employeeRow, mergedRow, employees.Columns);
                    if (educationQualificationInformation.Columns.Contains(qualificationColumn))
                        mergedRow[qualificationColumn] = qualificationRow[qualificationColumn] ?? DBNull.Value;
                    merged.Rows.Add(mergedRow);
                }
            }

            return merged;
        }

        /// <summary>
        /// Creates employee rows with independent family, nominee, education and skills detail
        /// columns. Detail lists are aligned by index (not cross-joined), so multiple
        /// records in one section do not overwrite records in another section.
        /// </summary>
        private DataTable CreateEmployeeInformationPreview(DataTable employeeDetails)
        {
            if (employeeDetails == null)
                return new DataTable();

            DataTable preview = employeeDetails.Clone();
            if (!employeeDetails.Columns.Contains("EmpID"))
                throw new InvalidOperationException("Employee Information preview requires an EmpID column.");

            HashSet<string> addedEmployeeIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (DataRow sourceRow in employeeDetails.Rows)
            {
                string employeeId = sourceRow["EmpID"] == DBNull.Value
                    ? string.Empty
                    : Convert.ToString(sourceRow["EmpID"]).Trim();

                // Do not collapse rows with a missing key together; preserve those
                // records independently rather than accidentally hiding employees.
                if (!string.IsNullOrEmpty(employeeId) && !addedEmployeeIds.Add(employeeId))
                    continue;

                preview.ImportRow(sourceRow);
            }

            return preview;
        }

        private void ConfigureEmployeeInformationPreviewColumns()
        {
            if (dtgImportDataPreview == null || dtgImportDataPreview.Columns.Count == 0)
                return;

            // The preview is an employee list, not a detail-data browser. Keep the
            // standard employee fields only; optional family, nominee, education,
            // skills and monthly attendance columns remain in the export backing table.
            string[] visibleColumns = new string[]
            {
                "EmpCode", "EmpName", "DOB", "DOJ", "DateOfJoining",
                "Date Of Joining", "DesignationTitle", "DepartmentTitle",
                "ContactNumber1", "ContactNumber2"
            };

            foreach (DataGridViewColumn column in dtgImportDataPreview.Columns)
            {
                column.Visible = column.Name == "IsSelected" ||
                                 visibleColumns.Contains(column.Name, StringComparer.OrdinalIgnoreCase);
            }

            string dobColumn = dtgImportDataPreview.Columns.Contains("DOB") ? "DOB" : null;
            if (dobColumn != null)
            {
                dtgImportDataPreview.Columns[dobColumn].HeaderText = "DOB";
                dtgImportDataPreview.Columns[dobColumn].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dtgImportDataPreview.Columns[dobColumn].DefaultCellStyle.NullValue = string.Empty;
            }

            string dojColumn = dtgImportDataPreview.Columns.Contains("DOJ") ? "DOJ" :
                (dtgImportDataPreview.Columns.Contains("DateOfJoining") ? "DateOfJoining" :
                (dtgImportDataPreview.Columns.Contains("Date Of Joining") ? "Date Of Joining" : null));
            if (dojColumn != null)
            {
                dtgImportDataPreview.Columns[dojColumn].HeaderText = "DOJ";
                dtgImportDataPreview.Columns[dojColumn].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dtgImportDataPreview.Columns[dojColumn].DefaultCellStyle.NullValue = string.Empty;
            }
        }

        private DataTable BuildEmployeeInformationWithDetails(DataTable employees)
        {
            string[] familyColumns = new string[]
            {
                "FamMemName", "FamMemDOB", "FamMemAge", "FamMemRelationship",
                "FamMemAddr1", "FamMemAddr2", "FamMemArea", "FamMemCity",
                "FamMemState", "FamMemPIN", "FamMemCountry",
                "FamMemContactNumber", "FamMemMailID", "FamMemBloodGroup",
                "FamMemInsuranceEnrolled"
            };
            string[] nomineeColumns = new string[] { "NomineePerson", "NomineeContactNumber", "NomineeRelationShipTitle" };

            DataTable result = employees.Clone();
            foreach (string column in familyColumns)
                if (!result.Columns.Contains(column)) result.Columns.Add(column, column == "FamMemDOB" ? typeof(DateTime) : (column == "FamMemAge" ? typeof(int) : (column == "FamMemInsuranceEnrolled" ? typeof(bool) : typeof(string))));
            foreach (string column in nomineeColumns)
                if (!result.Columns.Contains(column)) result.Columns.Add(column, typeof(string));
            if (!result.Columns.Contains("EduQualTitle")) result.Columns.Add("EduQualTitle", typeof(string));
            if (!result.Columns.Contains("SkillTitle")) result.Columns.Add("SkillTitle", typeof(string));
            if (!result.Columns.Contains("ExpertiseLevel")) result.Columns.Add("ExpertiseLevel", typeof(int));

            // Employee-specific bank account information for the expandable HTML detail section.
            if (!result.Columns.Contains("EmployeeBankName")) result.Columns.Add("EmployeeBankName", typeof(string));
            if (!result.Columns.Contains("EmployeeBankAccountNumber")) result.Columns.Add("EmployeeBankAccountNumber", typeof(string));
            if (!result.Columns.Contains("EmployeeBankAddress")) result.Columns.Add("EmployeeBankAddress", typeof(string));
            if (!result.Columns.Contains("EmployeeBankIFSCCode")) result.Columns.Add("EmployeeBankIFSCCode", typeof(string));

            // Employee-specific Leave Information columns populated from the
            // employee leave-entitlement DAL below.
            if (!result.Columns.Contains("LeaveTitle")) result.Columns.Add("LeaveTitle", typeof(string));
            if (!result.Columns.Contains("LeaveAlloted")) result.Columns.Add("LeaveAlloted", typeof(decimal));
            if (!result.Columns.Contains("LeaveUtilised")) result.Columns.Add("LeaveUtilised", typeof(decimal));
            if (!result.Columns.Contains("LeaveBalance")) result.Columns.Add("LeaveBalance", typeof(decimal));

            // Employee-specific Advance Statement columns populated from
            // objEmployeeAdvanceInfo.getEmployeeAdvanceInformationList(empId).
            if (!result.Columns.Contains("EmployeeAdvanceName")) result.Columns.Add("EmployeeAdvanceName", typeof(string));
            if (!result.Columns.Contains("EmployeeAdvanceDate")) result.Columns.Add("EmployeeAdvanceDate", typeof(object));
            if (!result.Columns.Contains("EmployeeAdvanceAmount")) result.Columns.Add("EmployeeAdvanceAmount", typeof(object));
            if (!result.Columns.Contains("EmployeeAdvanceIssueDate")) result.Columns.Add("EmployeeAdvanceIssueDate", typeof(object));
            if (!result.Columns.Contains("EmployeeAdvanceLastPaymentDate")) result.Columns.Add("EmployeeAdvanceLastPaymentDate", typeof(object));
            if (!result.Columns.Contains("EmployeeAdvanceOutstandingBalance")) result.Columns.Add("EmployeeAdvanceOutstandingBalance", typeof(object));

            // Employee-specific monthly payslip columns. These values come from
            // objEmpPayroll.getEmployeeSpecificConsolidatedPayslipValues(empId).
            if (!result.Columns.Contains("EmployeePayslipSalaryDate")) result.Columns.Add("EmployeePayslipSalaryDate", typeof(object));
            if (!result.Columns.Contains("EmployeePayslipBasicSalary")) result.Columns.Add("EmployeePayslipBasicSalary", typeof(object));
            if (!result.Columns.Contains("EmployeePayslipTotalAllowances")) result.Columns.Add("EmployeePayslipTotalAllowances", typeof(object));
            if (!result.Columns.Contains("EmployeePayslipTotalDeductions")) result.Columns.Add("EmployeePayslipTotalDeductions", typeof(object));
            if (!result.Columns.Contains("EmployeePayslipTotalReimbursements")) result.Columns.Add("EmployeePayslipTotalReimbursements", typeof(object));
            if (!result.Columns.Contains("EmployeePayslipNetSalary")) result.Columns.Add("EmployeePayslipNetSalary", typeof(object));

            // Consolidated Attendance Report columns.
            if (!result.Columns.Contains("AttendanceMonth")) result.Columns.Add("AttendanceMonth", typeof(string));
            // WorkingDays retains the total calendar days returned by the monthly query.
            if (!result.Columns.Contains("WorkingDays")) result.Columns.Add("WorkingDays", typeof(decimal));
            // ActualWorkingDays excludes the configured weekly-off days for the month.
            if (!result.Columns.Contains("ActualWorkingDays")) result.Columns.Add("ActualWorkingDays", typeof(decimal));
            if (!result.Columns.Contains("Present")) result.Columns.Add("Present", typeof(decimal));
            if (!result.Columns.Contains("Leave")) result.Columns.Add("Leave", typeof(decimal));
            if (!result.Columns.Contains("PayableDays")) result.Columns.Add("PayableDays", typeof(decimal));
            if (!result.Columns.Contains("AttendancePercentage")) result.Columns.Add("AttendancePercentage", typeof(decimal));

            // Reuse the same Monthly Attendance Register DAL/query used by the dashboard.
            // The query returns MONTH and DETAIL rows and keeps the latest AttID per employee/date.
            int attendanceYear = dtFromDate.Year > 1900 ? dtFromDate.Year : DateTime.Today.Year;
            List<MonthlyAttendanceRegisterRow> monthlyAttendanceRegisterData = new List<MonthlyAttendanceRegisterRow>();

            foreach (DataRow employee in employees.Rows)
            {
                int empId = Convert.ToInt32(employee["EmpID"]);
                DataTable family = chkFamilyMembersInfo.Checked ? ConvertListToDataTable(objEmpPersonalFamilyMemberInfo.GetEmpPersonalFamilyMemberInfoByEmpID(objTempClientFinYearInfo.ClientID, empId)) : new DataTable();

                DataTable nominee = new DataTable();
                if (chkNomineeInfo.Checked)
                {
                    ModelStaffSync.NomineeInfo nomineeInfo = objEmpNomineeMas.GetNomineeInfo(empId);
                    nominee = ConvertListToDataTable(nomineeInfo == null ? new List<ModelStaffSync.NomineeInfo>() : new List<ModelStaffSync.NomineeInfo> { nomineeInfo });
                }

                DataTable education = chkQualSkillsInfo.Checked ? (objEmpEduQualMas.GetEduQualListInfo(empId)) : new DataTable();
                DataTable skills = chkQualSkillsInfo.Checked ? (objEmpSkillsMas.GetSkillsListInfo(empId)) : new DataTable();
                DataTable monthlyAttendance = ConvertListToDataTable(objDashboardChartWidgets.GetMonthlyAttendanceRegisterDataByEmpID(empId));
                DataTable leaveInformation = ConvertListToDataTable(objEmpLeaveEntitlementInfo.getEmployeeSpecificLeaveInfo(empId));
                DataTable advanceInformation = ConvertListToDataTable(objEmployeeAdvanceInfo.getEmployeeAdvanceInformationList(empId));
                DataTable consolidatedPayslipValues = ConvertListToDataTable(objEmpPayroll.getEmployeeSpecificConsolidatedPayslipValues(empId));
                DataTable employeeBankAccountInfo = ConvertListToDataTable(objEmployeeBankACInfo.getEmployeeSpecificBankAccountInfo(empId));

                List<DataRow> familyRows = family.Rows.Cast<DataRow>().Where(r => family.Columns.Contains("EmpID") && r["EmpID"] != DBNull.Value && Convert.ToString(r["EmpID"]) == Convert.ToString(empId)).ToList();
                List<DataRow> nomineeRows = nominee.Rows.Cast<DataRow>().ToList();
                List<DataRow> educationRows = education.Rows.Cast<DataRow>().Where(r => education.Columns.Contains("EmpID") && r["EmpID"] != DBNull.Value && Convert.ToString(r["EmpID"]) == Convert.ToString(empId)).ToList();
                List<DataRow> skillsRows = skills.Rows.Cast<DataRow>().Where(r => skills.Columns.Contains("EmpID") && r["EmpID"] != DBNull.Value && Convert.ToString(r["EmpID"]) == Convert.ToString(empId)).ToList();
                List<DataRow> mnthlyAttendanceRows = monthlyAttendance.Rows.Cast<DataRow>().Where(r => !monthlyAttendance.Columns.Contains("EmpID") || r["EmpID"] == DBNull.Value || Convert.ToString(r["EmpID"]) == Convert.ToString(empId)).Where(r => !monthlyAttendance.Columns.Contains("RecordType") || r["RecordType"] == DBNull.Value || string.Equals(Convert.ToString(r["RecordType"]), "MONTH", StringComparison.OrdinalIgnoreCase)).ToList();
                List<DataRow> empLeaveRows = leaveInformation.Rows.Cast<DataRow>().Where(r => !leaveInformation.Columns.Contains("EmpID") || r["EmpID"] == DBNull.Value || Convert.ToString(r["EmpID"]) == Convert.ToString(empId)).ToList();
                List<DataRow> empAdvanceRows = advanceInformation.Rows.Cast<DataRow>().Where(r => !advanceInformation.Columns.Contains("EmpID") || r["EmpID"] == DBNull.Value || Convert.ToString(r["EmpID"]) == Convert.ToString(empId)).ToList();
                List<DataRow> empPayslipRows = consolidatedPayslipValues.Rows.Cast<DataRow>().Where(r => !consolidatedPayslipValues.Columns.Contains("EmpID") || r["EmpID"] == DBNull.Value || Convert.ToString(r["EmpID"]) == Convert.ToString(empId)).ToList();
                List<DataRow> empBankAccountRows = employeeBankAccountInfo.Rows.Cast<DataRow>().Where(r => !employeeBankAccountInfo.Columns.Contains("EmpID") || r["EmpID"] == DBNull.Value || Convert.ToString(r["EmpID"]) == Convert.ToString(empId)).ToList();

                //DataTable employeeAttendanceSummary = BuildConsolidatedAttendanceSummary(monthlyAttendanceRegisterData, empId, attendanceYear);
                //List<DataRow> attendanceRows = employeeAttendanceSummary.Rows.Cast<DataRow>().ToList();

                int rowCount = Math.Max(1, Math.Max(empBankAccountRows.Count, Math.Max(empPayslipRows.Count, Math.Max(empAdvanceRows.Count, Math.Max(empLeaveRows.Count, Math.Max(mnthlyAttendanceRows.Count, Math.Max(familyRows.Count, Math.Max(nomineeRows.Count, Math.Max(educationRows.Count, skillsRows.Count)))))))));

                for (int i = 0; i < rowCount; i++)
                {
                    DataRow output = result.NewRow();
                    CopyEmployeeColumns(employee, output, employees.Columns);
                    if (i < familyRows.Count)
                        foreach (string column in familyColumns)
                            if (family.Columns.Contains(column) && result.Columns.Contains(column))
                                output[column] = familyRows[i][column] == DBNull.Value ? DBNull.Value : familyRows[i][column];
                    if (i < nomineeRows.Count)
                    {
                        if (nominee.Columns.Contains("NomineePerson")) output["NomineePerson"] = nomineeRows[i]["NomineePerson"] == DBNull.Value ? DBNull.Value : nomineeRows[i]["NomineePerson"];
                        if (nominee.Columns.Contains("ContactNumber")) output["NomineeContactNumber"] = nomineeRows[i]["ContactNumber"] == DBNull.Value ? DBNull.Value : nomineeRows[i]["ContactNumber"];
                        if (nominee.Columns.Contains("RelationShipTitle")) output["NomineeRelationShipTitle"] = nomineeRows[i]["RelationShipTitle"] == DBNull.Value ? DBNull.Value : nomineeRows[i]["RelationShipTitle"];
                    }
                    if (i < educationRows.Count && education.Columns.Contains("EduQualTitle"))
                        output["EduQualTitle"] = educationRows[i]["EduQualTitle"] == DBNull.Value ? DBNull.Value : educationRows[i]["EduQualTitle"];
                    if (i < skillsRows.Count)
                    {
                        if (skills.Columns.Contains("SkillTitle"))
                            output["SkillTitle"] = skillsRows[i]["SkillTitle"] == DBNull.Value ? DBNull.Value : skillsRows[i]["SkillTitle"];
                        if (skills.Columns.Contains("ExpertiseLevel") && skillsRows[i]["ExpertiseLevel"] != DBNull.Value)
                        {
                            int expertiseLevel;
                            if (int.TryParse(Convert.ToString(skillsRows[i]["ExpertiseLevel"]), out expertiseLevel))
                                output["ExpertiseLevel"] = Math.Max(0, Math.Min(10, expertiseLevel));
                        }
                    }

                    if (i < empBankAccountRows.Count)
                    {
                        DataRow empBankRow = empBankAccountRows[i];
                        // Read source column names used by the employee-bank DAL.
                        // Always write to the dedicated detail columns added to result;
                        // do not assign EmpACNumber/AccountNumber to output directly,
                        // because those columns may not exist in the employee result table.
                        string bankNameColumn = GetFirstExistingColumn(empBankRow.Table,
                            "EmployeeBankACName", "BankName", "BankTitle", "BankTitleName");
                        string bankAccountColumn = GetFirstExistingColumn(empBankRow.Table,
                            "EmpACNumber", "EmployeeBankACNumber", "AccountNumber", "EmployeeBankAccountNumber", "BankAccountNumber");
                        string bankIfscColumn = GetFirstExistingColumn(empBankRow.Table,
                            "IFSCCode", "EmployeeBankIFSCCode", "IFSC", "EmpIFSCCode");
                        string bankAddressColumn = GetFirstExistingColumn(empBankRow.Table,
                            "EmployeeBankAddress", "Address", "BankAddress", "BranchAddress", "BankBranchAddress", "AccountAddress");

                        if (!string.IsNullOrWhiteSpace(bankNameColumn) && output.Table.Columns.Contains("EmployeeBankName"))
                            output["EmployeeBankName"] = empBankRow[bankNameColumn] == DBNull.Value ? DBNull.Value : empBankRow[bankNameColumn];

                        if (!string.IsNullOrWhiteSpace(bankAccountColumn) && output.Table.Columns.Contains("EmployeeBankAccountNumber"))
                            output["EmployeeBankAccountNumber"] = empBankRow[bankAccountColumn] == DBNull.Value ? DBNull.Value : empBankRow[bankAccountColumn];

                        if (!string.IsNullOrWhiteSpace(bankIfscColumn) && output.Table.Columns.Contains("EmployeeBankIFSCCode"))
                            output["EmployeeBankIFSCCode"] = empBankRow[bankIfscColumn] == DBNull.Value ? DBNull.Value : empBankRow[bankIfscColumn];

                        if (!string.IsNullOrWhiteSpace(bankAddressColumn) && output.Table.Columns.Contains("EmployeeBankAddress"))
                            output["EmployeeBankAddress"] = empBankRow[bankAddressColumn] == DBNull.Value ? DBNull.Value : empBankRow[bankAddressColumn];
                    }

                    if (i < empLeaveRows.Count)
                    {
                        DataRow empIndLeaveRow = empLeaveRows[i];

                        output["LeaveTitle"] = empLeaveRows[i].Table.Columns.Contains("LeaveTypeTitle") && empIndLeaveRow["LeaveTypeTitle"] != DBNull.Value
                            ? empIndLeaveRow["LeaveTypeTitle"] : DBNull.Value;

                        if (empLeaveRows[i].Table.Columns.Contains("TotalLeaves"))
                            output["LeaveAlloted"] = empIndLeaveRow["TotalLeaves"] == DBNull.Value ? DBNull.Value : empIndLeaveRow["TotalLeaves"];

                        // Use an actual utilised value if this DAL version supplies one.
                        // Otherwise the HTML renderer calculates TotalLeaves - BalanceLeaves.
                        if (empLeaveRows[i].Table.Columns.Contains("UtilisedLeaves"))
                            output["LeaveUtilised"] = empIndLeaveRow["UtilisedLeaves"] == DBNull.Value ? DBNull.Value : empIndLeaveRow["UtilisedLeaves"];
                        else if (empLeaveRows[i].Table.Columns.Contains("LeaveUtilised"))
                            output["LeaveUtilised"] = empIndLeaveRow["LeaveUtilised"] == DBNull.Value ? DBNull.Value : empIndLeaveRow["LeaveUtilised"];

                        if (empLeaveRows[i].Table.Columns.Contains("BalanceLeaves"))
                            output["LeaveBalance"] = empIndLeaveRow["BalanceLeaves"] == DBNull.Value ? DBNull.Value : empIndLeaveRow["BalanceLeaves"];
                        else if (empLeaveRows[i].Table.Columns.Contains("LeaveBalance"))
                            output["LeaveBalance"] = empIndLeaveRow["LeaveBalance"] == DBNull.Value ? DBNull.Value : empIndLeaveRow["LeaveBalance"];
                        // IsPaid remains available in the source but is not one of the requested display columns.
                    }

                    if (i < empAdvanceRows.Count)
                    {
                        DataRow empIndAdvanceRow = empAdvanceRows[i];

                        string advanceNameColumn = GetFirstExistingColumn(empIndAdvanceRow.Table,
                            "AdvanceName", "AdvanceTypeTitle", "AdvTypeTitle", "AdvanceTitle", "AdvanceTypeName", "AdvName");
                        string advanceDateColumn = GetFirstExistingColumn(empIndAdvanceRow.Table,
                            "Date", "AdvanceDate", "AdvDate", "CreatedDate", "TransactionDate", "IssueDate");
                        string advanceAmountColumn = GetFirstExistingColumn(empIndAdvanceRow.Table,
                            // The employee advance query returns IssueAmount as the original issued amount.
                            // CBalance is the running/outstanding balance and must not be used here.
                            "IssueAmount", "Issue Amount", "AdvanceAmount", "Advance Amount", "AdvAmount", "Amount", "IssuedAmount", "TotalAdvanceAmount");
                        string advanceIssueDateColumn = GetFirstExistingColumn(empIndAdvanceRow.Table,
                            "IssueDate", "AdvanceIssueDate", "AdvIssueDate", "DateOfIssue", "IssuedDate", "Date", "AdvanceDate");
                        string advanceLastPaymentColumn = GetFirstExistingColumn(empIndAdvanceRow.Table,
                            "LastPaymentDate", "Last Payment Date", "LastPaidDate", "LastPayment", "LastRecoveryDate", "LastInstallmentDate");
                        string advanceOutstandingColumn = GetFirstExistingColumn(empIndAdvanceRow.Table,
                            "OutstandingBalance", "Outstanding Balance", "OutstandingAmount", "BalanceAmount", "Balance", "RemainingAmount", "PendingAmount", "CBalance");

                        output["EmployeeAdvanceName"] = GetAdvanceText(empIndAdvanceRow, advanceNameColumn);
                        output["EmployeeAdvanceDate"] = GetAdvanceRawValue(empIndAdvanceRow, advanceDateColumn);
                        output["EmployeeAdvanceAmount"] = GetAdvanceRawValue(empIndAdvanceRow, advanceAmountColumn);
                        output["EmployeeAdvanceIssueDate"] = GetAdvanceRawValue(empIndAdvanceRow, advanceIssueDateColumn);
                        output["EmployeeAdvanceLastPaymentDate"] = GetAdvanceRawValue(empIndAdvanceRow, advanceLastPaymentColumn);
                        output["EmployeeAdvanceOutstandingBalance"] = GetAdvanceRawValue(empIndAdvanceRow, advanceOutstandingColumn);
                    }

                    // Consolidated Attendance Information - one row per month.
                    // Map the actual column names returned by the Monthly Attendance
                    // Register query to the export's existing consolidated columns.
                    // Keep aliases supported as well, so the export remains compatible
                    // if the DAL returns the older summary column names.
                    if (i < mnthlyAttendanceRows.Count)
                    {
                        DataRow attendanceRow = mnthlyAttendanceRows[i];

                        output["AttendanceMonth"] = attendanceRow["MonthYear"];
                        output["WorkingDays"] = attendanceRow["TotalDays"];
                        output["Present"] = attendanceRow["PresentCount"];
                        output["Leave"] = attendanceRow["LeaveCount"];
                        output["PayableDays"] = attendanceRow["PayableDays"];

                        // Keep the query's Total Days value in WorkingDays and calculate
                        // ActualWorkingDays separately by excluding configured weekly offs.
                        // Attendance percentage uses ActualWorkingDays as its denominator.
                        object actualWorkingDaysValue = DBNull.Value;
                        DateTime attendanceMonthDate;
                        if (TryGetAttendanceMonthDate(output["AttendanceMonth"], (int)attendanceRow["YearNo"], out attendanceMonthDate))
                        {
                            int configuredWorkingDays = CountWorkingDaysExcludingWeeklyOffs(attendanceMonthDate.Year, attendanceMonthDate.Month);
                            if (configuredWorkingDays >= 0)
                                actualWorkingDaysValue = (decimal)configuredWorkingDays;
                        }

                        if (result.Columns.Contains("ActualWorkingDays"))
                            output["ActualWorkingDays"] = actualWorkingDaysValue;

                        if (result.Columns.Contains("AttendancePercentage"))
                        {
                            decimal presentDays;
                            decimal actualWorkingDays;
                            if (TryConvertToDecimal(output["Present"], out presentDays) && TryConvertToDecimal(output["ActualWorkingDays"], out actualWorkingDays) && actualWorkingDays > 0m)
                            {
                                output["AttendancePercentage"] = Math.Round((presentDays / actualWorkingDays) * 100m, 2, MidpointRounding.AwayFromZero);
                            }
                            else
                            {
                                output["AttendancePercentage"] = DBNull.Value;
                            }
                        }
                    }

                    if (i < empPayslipRows.Count)
                    {
                        DataRow indPayslipInfo = empPayslipRows[i];
                        DataTable payslipTable = indPayslipInfo.Table;

                        string salaryDateColumn = GetFirstExistingColumn(payslipTable,
                            "SalaryMonth", "SalaryDate", "EmpSalDate", "PayDate", "PayrollDate", "MonthYear");
                        string basicSalaryColumn = GetFirstExistingColumn(payslipTable,
                            "Basic Salary", "BasicSalary", "BasicAmount", "BasicPay");
                        string allowancesColumn = GetFirstExistingColumn(payslipTable,
                            "TotalAllowanceAmount", "TotalAllowances", "TotalAllowance", "AllowanceAmount", "Total Allowances");
                        string deductionsColumn = GetFirstExistingColumn(payslipTable,
                            "TotalDeductionAmount", "TotalDeductions", "TotalDeduction", "DeductionAmount", "Total Deductions");
                        string reimbursementsColumn = GetFirstExistingColumn(payslipTable,
                            "TotalReimbursementAmount", "TotalReimbursements", "TotalReimbursement", "ReimbursementAmount", "Total Reimbursements", "TotalReimbursmentAmount");

                        object salaryDateValue = GetPayslipRawValue(indPayslipInfo, salaryDateColumn);
                        decimal basicSalary = GetPayslipDecimal(indPayslipInfo, basicSalaryColumn);
                        decimal totalAllowances = GetPayslipDecimal(indPayslipInfo, allowancesColumn);
                        decimal totalDeductions = GetPayslipDecimal(indPayslipInfo, deductionsColumn);
                        decimal totalReimbursements = GetPayslipDecimal(indPayslipInfo, reimbursementsColumn);
                        decimal netSalary = basicSalary + totalAllowances - totalDeductions + totalReimbursements;

                        output["EmployeePayslipSalaryDate"] = salaryDateValue;
                        output["EmployeePayslipBasicSalary"] = string.IsNullOrEmpty(basicSalaryColumn) ? (object)DBNull.Value : basicSalary;
                        output["EmployeePayslipTotalAllowances"] = string.IsNullOrEmpty(allowancesColumn) ? (object)DBNull.Value : totalAllowances;
                        output["EmployeePayslipTotalDeductions"] = string.IsNullOrEmpty(deductionsColumn) ? (object)DBNull.Value : totalDeductions;
                        output["EmployeePayslipTotalReimbursements"] = string.IsNullOrEmpty(reimbursementsColumn) ? (object)DBNull.Value : totalReimbursements;
                        output["EmployeePayslipNetSalary"] =
                            string.IsNullOrEmpty(basicSalaryColumn) && string.IsNullOrEmpty(allowancesColumn) &&
                            string.IsNullOrEmpty(deductionsColumn) && string.IsNullOrEmpty(reimbursementsColumn)
                            ? (object)DBNull.Value : netSalary;
                    }

                    result.Rows.Add(output);
                }
            }
            return result;
        }

        /// <summary>
        /// Builds month-wise attendance totals from the same latest-AttID detail rows
        /// used by the Monthly Attendance Register dashboard.
        /// WorkingDays counts days with a recognized attendance status.
        /// PayableDays currently assumes Present + full-day leave; adjust this rule
        /// if the payroll policy distinguishes paid and unpaid leave.
        /// </summary>
        private DataTable BuildConsolidatedAttendanceSummary(
            List<MonthlyAttendanceRegisterRow> attendanceData,
            int empId,
            int year)
        {
            DataTable summary = new DataTable("ConsolidatedAttendanceSummary");
            summary.Columns.Add("AttendanceMonth", typeof(string));
            summary.Columns.Add("WorkingDays", typeof(decimal));
            summary.Columns.Add("Present", typeof(decimal));
            summary.Columns.Add("Leave", typeof(decimal));
            summary.Columns.Add("PayableDays", typeof(decimal));

            if (attendanceData == null || attendanceData.Count == 0)
                return summary;

            // Use the DETAIL records returned by the same dashboard query.
            // The query has already selected the latest AttID for each employee/date.
            // Filter by employee and year before grouping by month.
            List<MonthlyAttendanceRegisterRow> employeeDetailRows = attendanceData
                .Where(row => row != null &&
                    string.Equals(row.RecordType, "DETAIL", StringComparison.OrdinalIgnoreCase) &&
                    row.EmpID.HasValue && row.EmpID.Value == empId &&
                    row.AttendanceDate.HasValue && row.AttendanceDate.Value.Year == year)
                .GroupBy(row => new
                {
                    MonthNo = row.MonthNo ?? row.AttendanceDate.Value.Month,
                    Date = row.AttendanceDate.Value.Date
                })
                .Select(group => group.First())
                .ToList();

            foreach (var monthGroup in employeeDetailRows
                .GroupBy(row => row.MonthNo ?? row.AttendanceDate.Value.Month)
                .OrderBy(group => group.Key))
            {
                int present = 0;
                int fullDayLeave = 0;
                int cancelled = 0;
                int rejected = 0;

                foreach (MonthlyAttendanceRegisterRow attendanceRow in monthGroup)
                {
                    string status = (attendanceRow.AttendanceStatus ?? string.Empty).Trim();

                    // Match the supplied Access query's MONTH summary rules exactly.
                    if (string.Equals(status, "Present", StringComparison.OrdinalIgnoreCase))
                    {
                        present++;
                    }
                    else if (string.Equals(status, "Leave : Full Day", StringComparison.OrdinalIgnoreCase))
                    {
                        fullDayLeave++;
                    }
                    else if (status.IndexOf("Cancelled", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        cancelled++;
                    }
                    else if (status.IndexOf("Rejected", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        rejected++;
                    }
                }

                DataRow summaryRow = summary.NewRow();
                int monthNo = monthGroup.Key;
                string monthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(monthNo);

                summaryRow["AttendanceMonth"] = monthName + " - " + year.ToString(CultureInfo.InvariantCulture);
                summaryRow["WorkingDays"] = present + fullDayLeave + cancelled + rejected;
                summaryRow["Present"] = present;
                summaryRow["Leave"] = fullDayLeave;
                // In the supplied query, LeaveCount counts only "Leave : Full Day".
                // Therefore PayableDays follows the requested query as Present + LeaveCount.
                summaryRow["PayableDays"] = present + fullDayLeave;
                summary.Rows.Add(summaryRow);
            }

            return summary;
        }

        /// <summary>
        /// Converts a month label such as "Jan - 2026" or a month number into
        /// the first date of that month.
        /// </summary>
        private bool TryGetAttendanceMonthDate(object monthValue, int fallbackYear, out DateTime monthDate)
        {
            monthDate = DateTime.MinValue;
            if (monthValue == null || monthValue == DBNull.Value)
                return false;

            string text = Convert.ToString(monthValue, CultureInfo.InvariantCulture).Trim();
            if (string.IsNullOrWhiteSpace(text))
                return false;

            DateTime parsedDate;
            if (DateTime.TryParseExact(text, new[] { "MMM - yyyy", "MMMM - yyyy", "MMM-yyyy", "MMMM-yyyy" },
                CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate) ||
                DateTime.TryParse(text, CultureInfo.CurrentCulture, DateTimeStyles.None, out parsedDate))
            {
                monthDate = new DateTime(parsedDate.Year, parsedDate.Month, 1);
                return true;
            }

            int monthNumber;
            if (int.TryParse(text, out monthNumber) && monthNumber >= 1 && monthNumber <= 12 && fallbackYear >= 1900)
            {
                monthDate = new DateTime(fallbackYear, monthNumber, 1);
                return true;
            }

            string monthPart = text;
            int separator = text.IndexOf('-');
            if (separator >= 0)
                monthPart = text.Substring(0, separator).Trim();

            for (int month = 1; month <= 12; month++)
            {
                string abbreviated = CultureInfo.InvariantCulture.DateTimeFormat.GetAbbreviatedMonthName(month);
                string full = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(month);
                if (string.Equals(monthPart, abbreviated, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(monthPart, full, StringComparison.OrdinalIgnoreCase))
                {
                    int year = fallbackYear;
                    if (separator >= 0)
                    {
                        int parsedYear;
                        if (int.TryParse(text.Substring(separator + 1).Trim(), out parsedYear) && parsedYear >= 1900)
                            year = parsedYear;
                    }
                    if (year < 1900)
                        return false;
                    monthDate = new DateTime(year, month, 1);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Counts dates in the month that are not configured weekly-off days.
        /// The active/effective weekly-off profile is read through the existing DAL.
        /// </summary>
        private int CountWorkingDaysExcludingWeeklyOffs(int year, int month)
        {
            HashSet<DayOfWeek> weeklyOffDays = GetConfiguredWeeklyOffDays(year, month);
            int daysInMonth = DateTime.DaysInMonth(year, month);
            int workingDays = 0;

            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime date = new DateTime(year, month, day);
                if (!weeklyOffDays.Contains(date.DayOfWeek))
                    workingDays++;
            }

            return workingDays;
        }

        /// <summary>
        /// Reads WklyOffDays from the most recently effective weekly-off profile
        /// for the requested month. WklyOffDays may contain day names or numeric
        /// DayOfWeek values separated by commas, semicolons, or spaces.
        /// </summary>
        private HashSet<DayOfWeek> GetConfiguredWeeklyOffDays(int year, int month)
        {
            HashSet<DayOfWeek> result = new HashSet<DayOfWeek>();
            DateTime monthEnd = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            try
            {
                var profiles = objWeeklyOffInfo.getWklyOffProfileMasInfoList("");
                object selectedProfile = null;
                DateTime selectedEffectiveDate = DateTime.MinValue;
                bool foundProfileWithDate = false;

                if (profiles != null)
                {
                    foreach (object profile in profiles)
                    {
                        if (profile == null)
                            continue;

                        object daysValue = GetObjectPropertyValue(profile, "WklyOffDays");
                        if (daysValue == null || daysValue == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(daysValue)))
                            continue;

                        object effectiveValue = GetObjectPropertyValue(profile, "WklyOffEffectiveDate");
                        DateTime effectiveDate;
                        bool hasEffectiveDate = DateTime.TryParse(Convert.ToString(effectiveValue), out effectiveDate);
                        if (hasEffectiveDate && effectiveDate.Date > monthEnd.Date)
                            continue;

                        if (selectedProfile == null ||
                            (hasEffectiveDate && (!foundProfileWithDate || effectiveDate.Date >= selectedEffectiveDate.Date)) ||
                            (!hasEffectiveDate && !foundProfileWithDate))
                        {
                            selectedProfile = profile;
                            if (hasEffectiveDate)
                            {
                                selectedEffectiveDate = effectiveDate.Date;
                                foundProfileWithDate = true;
                            }
                        }
                    }
                }

                if (selectedProfile == null)
                    return result;

                string dayText = Convert.ToString(GetObjectPropertyValue(selectedProfile, "WklyOffDays"));
                string[] tokens = dayText.Split(new char[] { ',', ';', '|', '/', '\t', '\r', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string tokenValue in tokens)
                {
                    string token = tokenValue.Trim().Trim('[', ']', '(', ')');
                    DayOfWeek parsedDay;
                    if (Enum.TryParse(token, true, out parsedDay))
                    {
                        result.Add(parsedDay);
                        continue;
                    }

                    int dayNumber;
                    if (int.TryParse(token, out dayNumber) && dayNumber >= 0 && dayNumber <= 6)
                        result.Add((DayOfWeek)dayNumber);
                }
            }
            catch
            {
                // Preserve export operation if the weekly-off profile cannot be read.
                // An empty set means no weekday is silently assumed to be an off day.
            }

            return result;
        }

        private object GetObjectPropertyValue(object source, string propertyName)
        {
            if (source == null || string.IsNullOrWhiteSpace(propertyName))
                return null;

            System.Reflection.PropertyInfo property = source.GetType().GetProperty(propertyName);
            return property == null ? null : property.GetValue(source, null);
        }

        /// <summary>
        /// Returns the first value found in a DataRow from a list of possible column
        /// names. This allows the export mapper to consume the Monthly Attendance
        /// Register query's field names without requiring changes to the DAL/query.
        /// </summary>
        private object GetFirstAvailableColumnValue(DataRow row, params string[] columnNames)
        {
            if (row == null || columnNames == null)
                return DBNull.Value;

            foreach (string columnName in columnNames)
            {
                if (!string.IsNullOrWhiteSpace(columnName) &&
                    row.Table.Columns.Contains(columnName))
                {
                    object value = row[columnName];
                    if (value == null || value == DBNull.Value)
                        continue;

                    // Keep valid zero values, but skip empty strings so the next alias
                    // can be tried when a DAL exposes multiple compatible columns.
                    if (value is string && string.IsNullOrWhiteSpace((string)value))
                        continue;

                    return value;
                }
            }

            return DBNull.Value;
        }

        private bool TryConvertToDecimal(object value, out decimal result)
        {
            result = 0m;
            if (value == null || value == DBNull.Value)
                return false;

            string text = Convert.ToString(value, CultureInfo.InvariantCulture);
            return decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out result)
                || decimal.TryParse(Convert.ToString(value), NumberStyles.Any, CultureInfo.CurrentCulture, out result);
        }

        private object SumNumericValues(params object[] values)
        {
            decimal total = 0m;
            bool foundValue = false;

            if (values != null)
            {
                foreach (object value in values)
                {
                    if (value == null || value == DBNull.Value)
                        continue;

                    decimal number;
                    if (decimal.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture),
                        NumberStyles.Any, CultureInfo.InvariantCulture, out number))
                    {
                        total += number;
                        foundValue = true;
                    }
                    else if (decimal.TryParse(Convert.ToString(value), out number))
                    {
                        total += number;
                        foundValue = true;
                    }
                }
            }

            return foundValue ? (object)total : DBNull.Value;
        }

        private object FormatAttendanceMonthYear(object monthValue, int year)
        {
            if (monthValue == null || monthValue == DBNull.Value)
                return DBNull.Value;

            string value = Convert.ToString(monthValue, CultureInfo.InvariantCulture);
            if (string.IsNullOrWhiteSpace(value))
                return DBNull.Value;

            // Preserve values already formatted by the query, including Month - Year.
            if (value.IndexOf("-", StringComparison.Ordinal) >= 0)
                return value;

            int monthNumber;
            if (int.TryParse(value, out monthNumber) && monthNumber >= 1 && monthNumber <= 12)
                return CultureInfo.InvariantCulture.DateTimeFormat.GetAbbreviatedMonthName(monthNumber) + " - " + year.ToString(CultureInfo.InvariantCulture);

            DateTime monthDate;
            if (DateTime.TryParse(value, CultureInfo.CurrentCulture, DateTimeStyles.None, out monthDate) ||
                DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out monthDate))
                return monthDate.ToString("MMM - yyyy", CultureInfo.InvariantCulture);

            // If Month is already a month name (e.g. October), append the report year.
            for (int monthNumberFromName = 1; monthNumberFromName <= 12; monthNumberFromName++)
            {
                string monthName = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(monthNumberFromName);
                if (string.Equals(value, monthName, StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(value, monthName.Substring(0, 3), StringComparison.OrdinalIgnoreCase))
                    return CultureInfo.InvariantCulture.DateTimeFormat.GetAbbreviatedMonthName(monthNumberFromName) + " - " + year.ToString(CultureInfo.InvariantCulture);
            }

            return value + " - " + year.ToString(CultureInfo.InvariantCulture);
        }

        private void CopyEmployeeColumns(DataRow sourceRow, DataRow targetRow, DataColumnCollection sourceColumns)
        {
            foreach (DataColumn column in sourceColumns)
            {
                if (targetRow.Table.Columns.Contains(column.ColumnName))
                    targetRow[column.ColumnName] = sourceRow[column.ColumnName] ?? DBNull.Value;
            }
        }

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
            objEmployeeInformationExportTable = new DataTable();

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

                grpMoreOptions.Visible = false;

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
                else if (string.Equals(lblSelectedDataAction.Text, "Allowance Information", StringComparison.OrdinalIgnoreCase))
                {
                    DataTable dtAllowanceList = objAllowenceInfo.GetAllowenceList();
                    if (dtAllowanceList == null || dtAllowanceList.Rows.Count == 0)
                    {
                        ClearExportPreview();
                        MessageBox.Show("No Allowance Information is available for export.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    BindExportPreviewData(dtAllowanceList);
                    FormatPreviewGrid(lblSelectedDataAction.Text);
                    dtgImportDataPreview.Enabled = true;
                    return;
                }
                else if (string.Equals(lblSelectedDataAction.Text, "Deductions Information", StringComparison.OrdinalIgnoreCase))
                {
                    DataTable dtDeductionsList = objDeductionInfo.GetDeductionList();
                    if (dtDeductionsList == null || dtDeductionsList.Rows.Count == 0)
                    {
                        ClearExportPreview();
                        MessageBox.Show("No Deductions Information is available for export.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    BindExportPreviewData(dtDeductionsList);
                    FormatPreviewGrid(lblSelectedDataAction.Text);
                    dtgImportDataPreview.Enabled = true;
                    return;
                }
                else if (string.Equals(lblSelectedDataAction.Text, "Reimbursement Information", StringComparison.OrdinalIgnoreCase))
                {
                    DataTable dtReimbursementList = objReimbursementInfo.GetReimbursementList();
                    if (dtReimbursementList == null || dtReimbursementList.Rows.Count == 0)
                    {
                        ClearExportPreview();
                        MessageBox.Show("No Reimbursement Information is available for export.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    BindExportPreviewData(dtReimbursementList);
                    FormatPreviewGrid(lblSelectedDataAction.Text);
                    dtgImportDataPreview.Enabled = true;
                    return;
                }
                else if (string.Equals(lblSelectedDataAction.Text, "Advance Type Information", StringComparison.OrdinalIgnoreCase))
                {
                    DataTable dtAdvanceTypeList = objAdvanceTypeMas.GetAdvanceTypeList(objTempClientFinYearInfo.ClientID);
                    if (dtAdvanceTypeList == null || dtAdvanceTypeList.Rows.Count == 0)
                    {
                        ClearExportPreview();
                        MessageBox.Show("No Advance Type Information is available for export.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    BindExportPreviewData(dtAdvanceTypeList);
                    FormatPreviewGrid(lblSelectedDataAction.Text);
                    dtgImportDataPreview.Enabled = true;
                    return;
                }
                else if (string.Equals(lblSelectedDataAction.Text, "Public Holiday Information", StringComparison.OrdinalIgnoreCase))
                {
                    DataTable dtPublicHolidayList = ConvertListToDataTable(objPublicHolidayInfo.getHolidayList(objTempClientFinYearInfo.ClientID, Convert.ToDateTime(dtFromDate), Convert.ToDateTime(dtToDate)));
                    if (dtPublicHolidayList == null || dtPublicHolidayList.Rows.Count == 0)
                    {
                        ClearExportPreview();
                        MessageBox.Show("No Public Holiday Information is available for export.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    BindExportPreviewData(dtPublicHolidayList);
                    FormatPreviewGrid(lblSelectedDataAction.Text);
                    dtgImportDataPreview.Enabled = true;
                    return;
                }
                else if (string.Equals(lblSelectedDataAction.Text, "Employement Type Information", StringComparison.OrdinalIgnoreCase))
                {
                    DataTable dtEmploymentTypeList = ConvertListToDataTable(objEmploymentTypeInfo.GetEmploymentTypeList());
                    if (dtEmploymentTypeList == null || dtEmploymentTypeList.Rows.Count == 0)
                    {
                        ClearExportPreview();
                        MessageBox.Show("No Employment Type Information is available for export.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    BindExportPreviewData(dtEmploymentTypeList);
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
                else if (string.Equals(lblSelectedDataAction.Text, "Bank Information", StringComparison.OrdinalIgnoreCase))
                {
                    DataTable dtBankList = ConvertListToDataTable(objBankMas.GetFullBanksList());
                    if (dtBankList == null || dtBankList.Rows.Count == 0)
                    {
                        ClearExportPreview();
                        MessageBox.Show("No Bank Information is available for export.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    BindExportPreviewData(dtBankList);
                    FormatPreviewGrid(lblSelectedDataAction.Text);
                    dtgImportDataPreview.Enabled = true;
                    return;
                }
                else if (string.Equals(lblSelectedDataAction.Text, "Company Information", StringComparison.OrdinalIgnoreCase))
                {
                    DataTable dtCompanyList = ConvertListToDataTable(objClientInfo.getAllCompanyList(objTempClientFinYearInfo.ClientID));
                    if (dtCompanyList == null || dtCompanyList.Rows.Count == 0)
                    {
                        ClearExportPreview();
                        MessageBox.Show("No Company Information is available for export.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    BindExportPreviewData(dtCompanyList);
                    FormatPreviewGrid(lblSelectedDataAction.Text);
                    dtgImportDataPreview.Enabled = true;
                    return;
                }
                else if (string.Equals(lblSelectedDataAction.Text, "Employee Information", StringComparison.OrdinalIgnoreCase))
                {
                    grpMoreOptions.Visible = true;
                    DataTable dtEmployeeList = ConvertListToDataTable(objEmployeeInfo.getCompleteEmployeesList());
                    if (dtEmployeeList == null || dtEmployeeList.Rows.Count == 0)
                    {
                        ClearExportPreview();
                        MessageBox.Show("No Employee Information is available for export.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    cmbOutputFormat.Items.Clear();
                    cmbOutputFormat.Items.Add("XML");
                    if (cmbOutputFormat.Items.Count > 0)
                        cmbOutputFormat.SelectedIndex = 0;

                    // Build one employee/detail data set. Family, nominee and qualification
                    // values are kept in their own columns so the HTML renderer can display
                    // each type in a separate subtable instead of the employee parent row.
                    dtEmployeeList = BuildEmployeeInformationWithDetails(dtEmployeeList);

                    // Keep the complete employee/detail rows for export, but show
                    // only one row per employee in the preview grid. Monthly
                    // attendance and other optional details can create multiple
                    // backing rows for one employee; those must not duplicate the
                    // employee in the selectable preview.
                    objEmployeeInformationExportTable = dtEmployeeList.Copy();
                    DataTable employeePreview = CreateEmployeeInformationPreview(dtEmployeeList);
                    BindExportPreviewData(employeePreview);
                    FormatPreviewGrid(lblSelectedDataAction.Text);
                    ConfigureEmployeeInformationPreviewColumns();
                    dtgImportDataPreview.Enabled = true;
                    grpMoreOptions.Visible = true;
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
            else if (string.Equals(lblSelectedDataAction.Text, "Allowance Information", StringComparison.OrdinalIgnoreCase))
            {
                dtgImportDataPreview.Columns["AllID"].Visible = false;
                dtgImportDataPreview.Columns["AllCode"].Visible = false;
                dtgImportDataPreview.Columns["AllTitle"].HeaderText = "Allowance Name";
                dtgImportDataPreview.Columns["AllTitle"].Width = 300;
                dtgImportDataPreview.Columns["AllTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["AllDescription"].HeaderText = "Description";
                dtgImportDataPreview.Columns["AllDescription"].Width = 350;
                dtgImportDataPreview.Columns["AllDescription"].ReadOnly = true;
                dtgImportDataPreview.Columns["CalcFormula"].Visible = false;
                dtgImportDataPreview.Columns["IsFixed"].Visible = false;
                dtgImportDataPreview.Columns["MaxCap"].Visible = false;
                dtgImportDataPreview.Columns["VisibleInPayslip"].Visible = false;
                dtgImportDataPreview.Columns["ProrataBasis"].Visible = false;
                dtgImportDataPreview.Columns["CompContribute"].Visible = false;
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
                dtgImportDataPreview.Columns["OrderID"].Visible = false;
            }
            else if (string.Equals(lblSelectedDataAction.Text, "Deductions Information", StringComparison.OrdinalIgnoreCase))
            {
                dtgImportDataPreview.Columns["DedID"].Visible = false;
                dtgImportDataPreview.Columns["DedCode"].Visible = false;
                dtgImportDataPreview.Columns["DedTitle"].HeaderText = "Deductions Name";
                dtgImportDataPreview.Columns["DedTitle"].Width = 300;
                dtgImportDataPreview.Columns["DedTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["DedDescription"].HeaderText = "Description";
                dtgImportDataPreview.Columns["DedDescription"].Width = 350;
                dtgImportDataPreview.Columns["DedDescription"].ReadOnly = true;
                dtgImportDataPreview.Columns["CalcFormula"].Visible = false;
                dtgImportDataPreview.Columns["IsFixed"].Visible = false;
                dtgImportDataPreview.Columns["MaxCap"].Visible = false;
                dtgImportDataPreview.Columns["VisibleInPayslip"].Visible = false;
                dtgImportDataPreview.Columns["ProrataBasis"].Visible = false;
                dtgImportDataPreview.Columns["CompContribute"].Visible = false;
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
                dtgImportDataPreview.Columns["OrderID"].Visible = false;
            }
            else if (string.Equals(lblSelectedDataAction.Text, "Reimbursement Information", StringComparison.OrdinalIgnoreCase))
            {
                dtgImportDataPreview.Columns["ReimbID"].Visible = false;
                dtgImportDataPreview.Columns["ReimbCode"].Visible = false;
                dtgImportDataPreview.Columns["ReimbTitle"].HeaderText = "Reimbursement Name";
                dtgImportDataPreview.Columns["ReimbTitle"].Width = 300;
                dtgImportDataPreview.Columns["ReimbTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["ReimbDescription"].HeaderText = "Description";
                dtgImportDataPreview.Columns["ReimbDescription"].Width = 350;
                dtgImportDataPreview.Columns["ReimbDescription"].ReadOnly = true;
                dtgImportDataPreview.Columns["CalcFormula"].Visible = false;
                dtgImportDataPreview.Columns["IsFixed"].Visible = false;
                dtgImportDataPreview.Columns["MaxCap"].Visible = false;
                dtgImportDataPreview.Columns["VisibleInPayslip"].Visible = false;
                dtgImportDataPreview.Columns["ProrataBasis"].Visible = false;
                dtgImportDataPreview.Columns["CompContribute"].Visible = false;
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
                dtgImportDataPreview.Columns["OrderID"].Visible = false;
            }
            else if (string.Equals(lblSelectedDataAction.Text, "Advance Type Information", StringComparison.OrdinalIgnoreCase))
            {
                dtgImportDataPreview.Columns["AdvanceTypeID"].Visible = false;
                dtgImportDataPreview.Columns["AdvanceTypeCode"].Visible = false;
                dtgImportDataPreview.Columns["AdvanceTypeTitle"].HeaderText = "Advance Type Name";
                dtgImportDataPreview.Columns["AdvanceTypeTitle"].Width = 300;
                dtgImportDataPreview.Columns["AdvanceTypeTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
                dtgImportDataPreview.Columns["OrderID"].Visible = false;
                dtgImportDataPreview.Columns["ClientID"].Visible = false;
            }
            else if (string.Equals(lblSelectedDataAction.Text, "Advance Type Information", StringComparison.OrdinalIgnoreCase))
            {
                dtgImportDataPreview.Columns["AdvTypeID"].Visible = false;
                dtgImportDataPreview.Columns["AdvTypeCode"].Visible = false;
                dtgImportDataPreview.Columns["AdvTypeTitle"].HeaderText = "Advance Type Name";
                dtgImportDataPreview.Columns["AdvTypeTitle"].Width = 300;
                dtgImportDataPreview.Columns["AdvTypeTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["AdvTypeDescription"].HeaderText = "Description";
                dtgImportDataPreview.Columns["AdvTypeDescription"].Width = 350;
                dtgImportDataPreview.Columns["AdvTypeDescription"].ReadOnly = true;
                dtgImportDataPreview.Columns["CalcFormula"].Visible = false;
                dtgImportDataPreview.Columns["IsFixed"].Visible = false;
                dtgImportDataPreview.Columns["MaxCap"].Visible = false;
                dtgImportDataPreview.Columns["VisibleInPayslip"].Visible = false;
                dtgImportDataPreview.Columns["ProrataBasis"].Visible = false;
                dtgImportDataPreview.Columns["CompContribute"].Visible = false;
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
                dtgImportDataPreview.Columns["OrderID"].Visible = false;
            }
            else if (string.Equals(lblSelectedDataAction.Text, "Public Holiday Information", StringComparison.OrdinalIgnoreCase))
            {
                dtgImportDataPreview.Columns["PubHolDetID"].Visible = false;
                dtgImportDataPreview.Columns["PubHolMasID"].Visible = false;
                dtgImportDataPreview.Columns["PubHolidayTitle"].HeaderText = "Public Holiday Name";
                dtgImportDataPreview.Columns["PubHolidayTitle"].Width = 200;
                dtgImportDataPreview.Columns["PubHolidayTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["PubHolDate"].HeaderText = "Date";
                dtgImportDataPreview.Columns["PubHolDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dtgImportDataPreview.Columns["PubHolDate"].DefaultCellStyle.NullValue = string.Empty;
                dtgImportDataPreview.Columns["PubHolDate"].Width = 100;
                dtgImportDataPreview.Columns["PubHolDate"].ReadOnly = true;
                dtgImportDataPreview.Columns["PubHolTypeTitle"].HeaderText = "Holiday Type";
                dtgImportDataPreview.Columns["PubHolTypeTitle"].Width = 150;
                dtgImportDataPreview.Columns["PubHolTypeTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["DayName"].HeaderText = "Day";
                dtgImportDataPreview.Columns["DayName"].Width = 100;
                dtgImportDataPreview.Columns["DayName"].ReadOnly = true;
                dtgImportDataPreview.Columns["OrderID"].Visible = false;
                dtgImportDataPreview.Columns["IsFestival"].HeaderText = "Is Festival";
                dtgImportDataPreview.Columns["IsFestival"].Width = 100;
                dtgImportDataPreview.Columns["IsFestival"].ReadOnly = true;
                dtgImportDataPreview.Columns["PubHolTypeID"].Visible = false;
            }
            else if (string.Equals(lblSelectedDataAction.Text, "Employement Type Information", StringComparison.OrdinalIgnoreCase))
            {
                dtgImportDataPreview.Columns["EmpTypeMasID"].Visible = false;
                dtgImportDataPreview.Columns["EmpTypeCode"].Visible = false;
                dtgImportDataPreview.Columns["EmpTypeTitle"].HeaderText = "Employment Type Name";
                dtgImportDataPreview.Columns["EmpTypeTitle"].Width = 300;
                dtgImportDataPreview.Columns["EmpTypeTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["EmpTypeInitial"].Visible = false;
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
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
            else if (string.Equals(lblSelectedDataAction.Text, "Bank Information", StringComparison.OrdinalIgnoreCase))
            {
                dtgImportDataPreview.Columns["BankID"].Visible = false;
                dtgImportDataPreview.Columns["BankCode"].Visible = false;
                dtgImportDataPreview.Columns["BankName"].HeaderText = "Bank Name";
                dtgImportDataPreview.Columns["BankName"].Width = 250;
                dtgImportDataPreview.Columns["BankName"].ReadOnly = true;
                dtgImportDataPreview.Columns["BankAddress"].HeaderText = "Bank Code";
                dtgImportDataPreview.Columns["BankAddress"].Width = 350;
                dtgImportDataPreview.Columns["BankAddress"].ReadOnly = true;
                dtgImportDataPreview.Columns["IFSCCode"].HeaderText = "IFSC Code";
                dtgImportDataPreview.Columns["IFSCCode"].Width = 200;
                dtgImportDataPreview.Columns["IFSCCode"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (string.Equals(lblSelectedDataAction.Text, "Company Information", StringComparison.OrdinalIgnoreCase))
            {
                dtgImportDataPreview.Columns["ClientID"].Visible = false;
                dtgImportDataPreview.Columns["ClientCode"].Visible = false;
                dtgImportDataPreview.Columns["ClientName"].HeaderText = "Company Name";
                dtgImportDataPreview.Columns["ClientName"].Width = 250;
                dtgImportDataPreview.Columns["ClientName"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientAddress1"].HeaderText = "Address";
                dtgImportDataPreview.Columns["ClientAddress1"].Width = 250;
                dtgImportDataPreview.Columns["ClientAddress1"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientAddress2"].HeaderText = "Address";
                dtgImportDataPreview.Columns["ClientAddress2"].Width = 250;
                dtgImportDataPreview.Columns["ClientAddress2"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientArea"].HeaderText = "Area";
                dtgImportDataPreview.Columns["ClientArea"].Width = 150;
                dtgImportDataPreview.Columns["ClientArea"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientCity"].HeaderText = "City";
                dtgImportDataPreview.Columns["ClientCity"].Width = 150;
                dtgImportDataPreview.Columns["ClientCity"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientState"].HeaderText = "State";
                dtgImportDataPreview.Columns["ClientState"].Width = 150;
                dtgImportDataPreview.Columns["ClientState"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientPIN"].HeaderText = "PIN";
                dtgImportDataPreview.Columns["ClientPIN"].Width = 100;
                dtgImportDataPreview.Columns["ClientPIN"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientCountry"].HeaderText = "Country";
                dtgImportDataPreview.Columns["ClientCountry"].Width = 100;
                dtgImportDataPreview.Columns["ClientCountry"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientPhone"].HeaderText = "Phone";
                dtgImportDataPreview.Columns["ClientPhone"].Width = 100;
                dtgImportDataPreview.Columns["ClientPhone"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientMailID"].HeaderText = "Mail ID";
                dtgImportDataPreview.Columns["ClientMailID"].Width = 250;
                dtgImportDataPreview.Columns["ClientMailID"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientContactPerson"].HeaderText = "Contact Person";
                dtgImportDataPreview.Columns["ClientContactPerson"].Width = 125;
                dtgImportDataPreview.Columns["ClientContactPerson"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientContactNumber"].HeaderText = "Contact Number";
                dtgImportDataPreview.Columns["ClientContactNumber"].Width = 100;
                dtgImportDataPreview.Columns["ClientContactNumber"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientContactMail"].HeaderText = "Contact Mail";
                dtgImportDataPreview.Columns["ClientContactMail"].Width = 250;
                dtgImportDataPreview.Columns["ClientContactMail"].ReadOnly = true;
                dtgImportDataPreview.Columns["ClientWebsite"].HeaderText = "Web Site";
                dtgImportDataPreview.Columns["ClientWebsite"].Width = 250;
                dtgImportDataPreview.Columns["ClientWebsite"].ReadOnly = true;
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (string.Equals(lblSelectedDataAction.Text, "Employee Information", StringComparison.OrdinalIgnoreCase))
            {
                dtgImportDataPreview.Columns["EmpID"].Visible = false;
                dtgImportDataPreview.Columns["EmpCode"].HeaderText = "Employee Code";
                dtgImportDataPreview.Columns["EmpCode"].Width = 100;
                dtgImportDataPreview.Columns["EmpCode"].ReadOnly = true;
                dtgImportDataPreview.Columns["EmpName"].HeaderText = "Employee Name";
                dtgImportDataPreview.Columns["EmpName"].Width = 250;
                dtgImportDataPreview.Columns["EmpName"].ReadOnly = true;
                dtgImportDataPreview.Columns["DOB"].HeaderText = "Birth Date";
                dtgImportDataPreview.Columns["DOB"].Width = 150;
                dtgImportDataPreview.Columns["DOB"].ReadOnly = true;
                dtgImportDataPreview.Columns["DesignationTitle"].HeaderText = "Designation";
                dtgImportDataPreview.Columns["DesignationTitle"].Width = 150;
                dtgImportDataPreview.Columns["DesignationTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["DepartmentTitle"].HeaderText = "Department";
                dtgImportDataPreview.Columns["DepartmentTitle"].Width = 150;
                dtgImportDataPreview.Columns["DepartmentTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["DOJ"].HeaderText = "Joining Date";
                dtgImportDataPreview.Columns["DOJ"].Width = 150;
                dtgImportDataPreview.Columns["DOJ"].ReadOnly = true;
                dtgImportDataPreview.Columns["ContactNumber1"].HeaderText = "Contact Number";
                dtgImportDataPreview.Columns["ContactNumber1"].Width = 150;
                dtgImportDataPreview.Columns["ContactNumber1"].ReadOnly = true;
                dtgImportDataPreview.Columns["ContactNumber2"].HeaderText = "Mail ID";
                dtgImportDataPreview.Columns["ContactNumber2"].Width = 150;
                dtgImportDataPreview.Columns["ContactNumber2"].ReadOnly = true;
                dtgImportDataPreview.Columns["StateID"].Visible = false;
                dtgImportDataPreview.Columns["SexID"].Visible = false;
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
            if (string.Equals(lblSelectedDataAction.Text, "Employee Information", StringComparison.OrdinalIgnoreCase) &&
                objEmployeeInformationExportTable != null &&
                objEmployeeInformationExportTable.Rows.Count > 0)
            {
                return GetSelectedEmployeeInformationExportData();
            }

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

        private DataTable GetSelectedEmployeeInformationExportData()
        {
            DataTable selectedData = new DataTable();
            if (dtgImportDataPreview == null ||
                !dtgImportDataPreview.Columns.Contains("EmpID") ||
                !objEmployeeInformationExportTable.Columns.Contains("EmpID"))
                return selectedData;

            List<DataGridViewColumn> visibleEmployeeColumns = dtgImportDataPreview.Columns
                .Cast<DataGridViewColumn>()
                .Where(column => column.Name != "IsSelected" && column.Name != "EmpID" && column.Visible)
                .OrderBy(column => column.DisplayIndex)
                .ToList();

            // Keep EmpID as an internal key for employee-specific detail lookups.
            // It is excluded from visibleEmployeeColumns above, so it will not appear in the report.
            if (!selectedData.Columns.Contains("EmpID"))
                selectedData.Columns.Add("EmpID", typeof(string));

            foreach (DataGridViewColumn column in visibleEmployeeColumns)
            {
                string name = GetExportDisplayColumnHeader(column);
                if (string.IsNullOrWhiteSpace(name)) name = column.Name;
                string baseName = name;
                int suffix = 2;
                while (selectedData.Columns.Contains(name)) name = baseName + " (" + (suffix++).ToString() + ")";
                selectedData.Columns.Add(name, typeof(string));
            }

            // Preserve detail fields for the HTML/XML/CSV/JSON/PDF export pipeline,
            // but never expose them as preview-grid columns.
            string[] detailColumns = new string[]
            {
                "FamMemName", "FamMemDOB", "FamMemAge", "FamMemRelationship",
                "FamMemAddr1", "FamMemAddr2", "FamMemArea", "FamMemCity",
                "FamMemState", "FamMemPIN", "FamMemCountry", "FamMemContactNumber",
                "FamMemMailID", "FamMemBloodGroup", "FamMemInsuranceEnrolled",
                "NomineePerson", "NomineeContactNumber", "NomineeRelationShipTitle",
                "EduQualTitle", "SkillTitle", "ExpertiseLevel",
                "EmployeeBankName", "EmployeeBankAccountNumber", "EmployeeBankAddress", "EmployeeBankIFSCCode",
                "LeaveTitle", "LeaveAlloted", "LeaveUtilised", "LeaveBalance",
                "AttendanceMonth", "WorkingDays", "ActualWorkingDays", "Present", "Leave", "PayableDays",
                "AttendancePercentage"
            };
            foreach (string detailColumn in detailColumns)
            {
                if (objEmployeeInformationExportTable.Columns.Contains(detailColumn) &&
                    !selectedData.Columns.Contains(detailColumn))
                    selectedData.Columns.Add(detailColumn, typeof(string));
            }

            foreach (DataGridViewRow gridRow in dtgImportDataPreview.Rows)
            {
                if (gridRow.IsNewRow) continue;
                bool isSelected = false;
                if (gridRow.Cells["IsSelected"].Value != null)
                    bool.TryParse(Convert.ToString(gridRow.Cells["IsSelected"].Value), out isSelected);
                if (!isSelected) continue;

                object employeeIdValue = gridRow.Cells["EmpID"].Value;
                if (employeeIdValue == null || employeeIdValue == DBNull.Value) continue;
                string employeeId = Convert.ToString(employeeIdValue).Trim();

                List<DataRow> detailRows = objEmployeeInformationExportTable.Rows.Cast<DataRow>()
                    .Where(row => row["EmpID"] != DBNull.Value &&
                                  string.Equals(Convert.ToString(row["EmpID"]).Trim(), employeeId, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                foreach (DataRow detailRow in detailRows)
                {
                    DataRow output = selectedData.NewRow();
                    int outputIndex = 0;
                    foreach (DataGridViewColumn column in visibleEmployeeColumns)
                    {
                        output[outputIndex++] = GetFormattedExportCellValue(gridRow, column.Name);
                    }
                    output["EmpID"] = employeeId;
                    foreach (string detailColumn in detailColumns)
                    {
                        if (selectedData.Columns.Contains(detailColumn))
                        {
                            object value = objEmployeeInformationExportTable.Columns.Contains(detailColumn)
                                ? detailRow[detailColumn]
                                : DBNull.Value;
                            output[detailColumn] = value == null || value == DBNull.Value
                                ? string.Empty
                                : Convert.ToString(value, CultureInfo.CurrentCulture);
                        }
                    }
                    selectedData.Rows.Add(output);
                }
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
            // When Family Members Information is selected, render the HTML export
            // as employee rows with expandable family-member details underneath.
            // Other exports continue to use the original flat-table layout.
            string[] familyColumns = new string[]
            {
                "FamMemName", "FamMemDOB", "FamMemAge", "FamMemRelationship",
                "FamMemAddr1", "FamMemAddr2", "FamMemArea", "FamMemCity",
                "FamMemState", "FamMemPIN", "FamMemCountry",
                "FamMemContactNumber", "FamMemMailID", "FamMemBloodGroup",
                "FamMemInsuranceEnrolled"
            };
            string[] nomineeColumns = new string[] { "NomineePerson", "NomineeContactNumber", "NomineeRelationShipTitle" };
            string[] educationColumns = new string[] { "EduQualTitle" };
            string[] skillsColumns = new string[] { "SkillTitle", "ExpertiseLevel" };
            string[] bankColumns = new string[] { "EmployeeBankName", "EmployeeBankAccountNumber", "EmployeeBankAddress", "EmployeeBankIFSCCode" };
            string[] leaveColumns = new string[] { "LeaveTitle", "LeaveAlloted", "LeaveUtilised", "LeaveBalance" };
            string[] attendanceColumns = new string[] { "AttendanceMonth", "WorkingDays", "ActualWorkingDays", "Present", "Leave", "PayableDays", "AttendancePercentage" };
            string[] detailColumns = familyColumns.Concat(nomineeColumns).Concat(educationColumns).Concat(skillsColumns).Concat(bankColumns).Concat(leaveColumns).Concat(attendanceColumns).ToArray();

            // EmpID may be hidden in the preview grid and therefore omitted by
            // GetSelectedExportData(). Detect the family export by its family
            // columns, then group using EmpID when available or EmpCode otherwise.
            bool hasFamilyColumns = data != null && detailColumns.Any(c => data.Columns.Contains(c));

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
            html.AppendLine("th{background:#800000;color:#fff;font-weight:600;text-align:left;border:1px solid #6b0000;padding:9px 10px;white-space:nowrap;}");
            html.AppendLine("td{border:1px solid #e1e5eb;padding:8px 10px;vertical-align:top;}");
            html.AppendLine(".employee-row td{background:#fafbfc;font-weight:600;}");
            html.AppendLine(".toggle{border:1px solid #98a2b3;background:#fff;border-radius:4px;cursor:pointer;padding:2px 8px;margin-right:8px;font-size:14px;}");
            html.AppendLine(".family-detail td{background:#fff;padding:14px 18px;}");
            // Adjustable family-subtable column widths. Change these percentages to suit the report layout.
            html.AppendLine(":root{--family-name-width:15%;--family-dob-width:10%;--family-age-width:5%;--family-relationship-width:8%;--family-contact-width:9%;--family-email-width:15%;--family-blood-width:7%;--family-insurance-width:8%;--family-address-width:32%;}");
            // Manually configurable widths for every employee detail subtable.
            // Change these percentages here; no rendering logic needs to be rewritten.
            html.AppendLine(":root{" +
                "--nominee-name-width:34%;" +
                "--nominee-contact-width:33%;" +
                "--nominee-relationship-width:33%;" +
                "--education-qualification-width:100%;" +
                "--skills-name-width:65%;" +
                "--skills-expertise-width:35%;" +
                "--leave-title-width:40%;" +
                "--leave-alloted-width:20%;" +
                "--leave-utilised-width:20%;" +
                "--leave-balance-width:20%;" +
                "--advance-name-width:28%;" +
                "--advance-amount-width:18%;" +
                "--advance-issue-date-width:18%;" +
                "--advance-last-payment-width:18%;" +
                "--advance-outstanding-width:18%;" +
                "--attendance-month-width:16%;" +
                "--attendance-total-days-width:16%;" +
                "--attendance-working-days-width:16%;" +
                "--attendance-present-width:10%;" +
                "--attendance-leave-width:10%;" +
                "--attendance-payable-days-width:12%;" +
                "--attendance-percentage-width:20%;" +
                "}");
            html.AppendLine(".family-table{table-layout:fixed;min-width:1100px;width:100%;margin:0;border-collapse:collapse;}");
            html.AppendLine(".family-table th{background:#800000;color:#fff;border-color:#6b0000;white-space:normal;overflow-wrap:anywhere;}");
            html.AppendLine(".family-table td{overflow-wrap:anywhere;word-break:normal;}");
            html.AppendLine(".bank-information-table{table-layout:fixed;width:100%;min-width:0;margin-bottom:14px;}");
            html.AppendLine(".bank-information-table th:nth-child(1){width:24%;}");
            html.AppendLine(".bank-information-table th:nth-child(2){width:20%;}");
            html.AppendLine(".bank-information-table th:nth-child(3){width:36%;}");
            html.AppendLine(".bank-information-table th:nth-child(4){width:20%;}");
            html.AppendLine(".family-table .col-family-name{width:var(--family-name-width);}");
            html.AppendLine(".family-table .col-family-dob{width:var(--family-dob-width);}");
            html.AppendLine(".family-table .col-family-age{width:var(--family-age-width);}");
            html.AppendLine(".family-table .col-family-relationship{width:var(--family-relationship-width);}");
            html.AppendLine(".family-table .col-family-contact{width:var(--family-contact-width);}");
            html.AppendLine(".family-table .col-family-email{width:var(--family-email-width);}");
            html.AppendLine(".family-table .col-family-blood{width:var(--family-blood-width);}");
            html.AppendLine(".family-table .col-family-insurance{width:var(--family-insurance-width);}");
            html.AppendLine(".family-table .col-family-address{width:var(--family-address-width);}");
            // Nominee Information column widths
            html.AppendLine(".nominee-table{table-layout:fixed;width:100%;min-width:0;}");
            html.AppendLine(".nominee-table .col-nominee-name{width:var(--nominee-name-width);}");
            html.AppendLine(".nominee-table .col-nominee-contact{width:var(--nominee-contact-width);}");
            html.AppendLine(".nominee-table .col-nominee-relationship{width:var(--nominee-relationship-width);}");

            // Education Qualification column width
            html.AppendLine(".education-table{table-layout:fixed;width:100%;min-width:0;}");
            html.AppendLine(".education-table .col-education-qualification{width:var(--education-qualification-width);}");

            // Skills Information column widths
            html.AppendLine(".skills-table{table-layout:fixed;width:100%;min-width:0;}");
            html.AppendLine(".skills-table .col-skills-name{width:var(--skills-name-width);}");
            html.AppendLine(".skills-table .col-skills-expertise{width:var(--skills-expertise-width);}");

            // Leave Information placeholder column widths
            html.AppendLine(".leave-table{table-layout:fixed;width:100%;min-width:0;}");
            html.AppendLine(".leave-table th,.leave-table td{overflow-wrap:anywhere;}");
            html.AppendLine(".leave-table .col-leave-title{width:var(--leave-title-width);}");
            html.AppendLine(".leave-table .col-leave-alloted{width:var(--leave-alloted-width);}");
            html.AppendLine(".leave-table .col-leave-utilised{width:var(--leave-utilised-width);}");
            html.AppendLine(".leave-table .col-leave-balance{width:var(--leave-balance-width);}");
            // Employee-specific Advance Statement column widths.
            html.AppendLine(".advance-statement-table{table-layout:fixed;width:100%;min-width:0;}");
            html.AppendLine(".advance-statement-table th,.advance-statement-table td{overflow-wrap:anywhere;}");
            html.AppendLine(".advance-statement-table .col-advance-name{width:var(--advance-name-width);}");
            html.AppendLine(".advance-statement-table .col-advance-amount{width:var(--advance-amount-width);}");
            html.AppendLine(".advance-statement-table .col-advance-issue-date{width:var(--advance-issue-date-width);}");
            html.AppendLine(".advance-statement-table .col-advance-last-payment{width:var(--advance-last-payment-width);}");
            html.AppendLine(".advance-statement-table .col-advance-outstanding{width:var(--advance-outstanding-width);}");
            // Employee-specific Payslip Statement column widths.
            html.AppendLine(".payslip-statement-table{table-layout:fixed;width:100%;min-width:0;}");
            html.AppendLine(".payslip-statement-table th,.payslip-statement-table td{overflow-wrap:anywhere;text-align:right;}");
            html.AppendLine(".payslip-statement-table th:first-child,.payslip-statement-table td:first-child{text-align:left;}");

            // Consolidated Attendance Report column widths
            html.AppendLine(".attendance-summary-table{table-layout:fixed;width:100%;}");
            html.AppendLine(".attendance-summary-table th,.attendance-summary-table td{overflow-wrap:anywhere;}");
            html.AppendLine(".attendance-summary-table .col-attendance-month{width:var(--attendance-month-width);}");
            html.AppendLine(".attendance-summary-table .col-attendance-total-days{width:var(--attendance-total-days-width);}");
            html.AppendLine(".attendance-summary-table .col-attendance-working-days{width:var(--attendance-working-days-width);}");
            html.AppendLine(".attendance-summary-table .col-attendance-present{width:var(--attendance-present-width);}");
            html.AppendLine(".attendance-summary-table .col-attendance-leave{width:var(--attendance-leave-width);}");
            html.AppendLine(".attendance-summary-table .col-attendance-payable-days{width:var(--attendance-payable-days-width);}");
            html.AppendLine(".attendance-summary-table .col-attendance-percentage{width:var(--attendance-percentage-width);}");
            html.AppendLine(".family-heading{font-weight:600;margin:0 0 8px 0;color:#800000;}");
            html.AppendLine(".empty-family{color:#667085;font-style:italic;font-weight:normal;}");
            html.AppendLine(".report-toolbar{display:flex;gap:12px;align-items:center;justify-content:space-between;flex-wrap:wrap;margin-bottom:12px;}");
            html.AppendLine(".report-search{flex:1;min-width:220px;max-width:520px;padding:9px 12px;border:1px solid #c7ced8;border-radius:5px;font:inherit;font-size:13px;box-sizing:border-box;}");
            html.AppendLine(".report-search:focus{outline:2px solid #b9d4ff;border-color:#5488d8;}");
            html.AppendLine(".report-toolbar-actions{display:flex;justify-content:flex-end;gap:8px;align-items:center;} ");
            html.AppendLine(".report-count{font-size:12px;color:#475467;white-space:nowrap;padding:6px 0;}");
            html.AppendLine(".expertise-rating{display:inline-flex;align-items:center;gap:1px;white-space:nowrap;} .expertise-star{font-size:18px;line-height:1;} .expertise-value{font-size:12px;margin-left:6px;color:#344054;font-weight:600;}");
            html.AppendLine(".footer{padding:12px 24px;border-top:1px solid #e3e7ed;color:#667085;font-size:12px;}");
            html.AppendLine("</style>");
            if (hasFamilyColumns)
            {
                html.AppendLine("<script>");
                html.AppendLine("var allFamiliesExpanded=false;");
                html.AppendLine("function setFamilyExpanded(id,expand){var row=document.getElementById(id);if(!row)return;row.setAttribute('data-expanded',expand?'true':'false');row.style.display=expand?'table-row':'none';var buttons=document.querySelectorAll('.employee-toggle');for(var j=0;j<buttons.length;j++){if(buttons[j].getAttribute('aria-controls')===id){buttons[j].textContent=expand?'−':'+';buttons[j].setAttribute('aria-expanded',expand?'true':'false');break;}}}");
                html.AppendLine("function toggleFamily(id,button){var row=document.getElementById(id);if(!row)return;var expand=row.style.display==='none';setFamilyExpanded(id,expand);updateGlobalToggleState();}");
                html.AppendLine("function toggleAllFamilies(){var rows=document.querySelectorAll('.family-detail');allFamiliesExpanded=!allFamiliesExpanded;for(var i=0;i<rows.length;i++){setFamilyExpanded(rows[i].id,allFamiliesExpanded);}var button=document.getElementById('toggle-all-families');if(button)button.textContent=allFamiliesExpanded?'Collapse All':'Expand All';}");
                html.AppendLine("function updateGlobalToggleState(){var rows=document.querySelectorAll('.family-detail');var allExpanded=rows.length>0;for(var i=0;i<rows.length;i++){if(rows[i].getAttribute('data-expanded')!=='true'){allExpanded=false;break;}}allFamiliesExpanded=allExpanded;var button=document.getElementById('toggle-all-families');if(button)button.textContent=allExpanded?'Collapse All':'Expand All';}");
                html.AppendLine("function updateReportCount(found,total){var el=document.getElementById('report-count');if(el)el.textContent='Showing '+found+' of '+total+' records';}function filterReport(){var input=document.getElementById('report-search');var query=input?input.value.toLowerCase().trim():'';var employeeRows=document.querySelectorAll('tr.employee-row');if(employeeRows.length){var found=0;for(var i=0;i<employeeRows.length;i++){var employee=employeeRows[i];var detail=employee.nextElementSibling;if(!detail||!detail.classList.contains('family-detail'))continue;var employeeText=employee.textContent.toLowerCase();var familyText=detail.textContent.toLowerCase();var match=!query||employeeText.indexOf(query)>=0||familyText.indexOf(query)>=0;employee.style.display=match?'table-row':'none';if(match)found++;if(!query){detail.style.display=detail.getAttribute('data-expanded')==='true'?'table-row':'none';}else if(!match){detail.style.display='none';}else if(familyText.indexOf(query)>=0){detail.style.display='table-row';}else{detail.style.display=detail.getAttribute('data-expanded')==='true'?'table-row':'none';}}updateReportCount(found,employeeRows.length);return;}var tables=document.querySelectorAll('.table-wrap table');var found=0,total=0;for(var t=0;t<tables.length;t++){var rows=tables[t].querySelectorAll('tbody tr');for(var r=0;r<rows.length;r++){total++;var rowText=rows[r].textContent.toLowerCase();var match=!query||rowText.indexOf(query)>=0;rows[r].style.display=match?'table-row':'none';if(match)found++;}}updateReportCount(found,total);}");
                html.AppendLine("</script>");
            }
            else
            {
                html.AppendLine("<script>function filterReport(){var input=document.getElementById('report-search');var query=input?input.value.toLowerCase().trim():'';var tables=document.querySelectorAll('.table-wrap table');var found=0,total=0;for(var t=0;t<tables.length;t++){var rows=tables[t].querySelectorAll('tbody tr');for(var r=0;r<rows.length;r++){total++;var rowText=rows[r].textContent.toLowerCase();var match=!query||rowText.indexOf(query)>=0;rows[r].style.display=match?'table-row':'none';if(match)found++;}}var el=document.getElementById('report-count');if(el)el.textContent='Showing '+found+' of '+total+' records';}</script>");
            }
            html.AppendLine("</head>");
            html.AppendLine("<body><div class=\"report\">");
            html.AppendLine("<div class=\"header\"><div class=\"title\">StaffSync Export - " + HtmlEncode(lblSelectedDataAction.Text) + "</div>");
            html.AppendLine("<div class=\"meta\">Generated On: " + HtmlEncode(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")) + "</div></div>");
            html.AppendLine("<div class=\"table-wrap\">");
            if (!hasFamilyColumns)
                html.AppendLine("<div class=\"report-toolbar\"><input type=\"search\" id=\"report-search\" class=\"report-search\" placeholder=\"Search data...\" oninput=\"filterReport()\" autocomplete=\"off\" /><span id=\"report-count\" class=\"report-count\"></span></div>");

            if (hasFamilyColumns)
            {
                List<DataColumn> employeeColumns = data.Columns.Cast<DataColumn>()
                    .Where(c => c.ColumnName != "IsSelected" &&
                                !string.Equals(c.ColumnName, "EmpID", StringComparison.OrdinalIgnoreCase) &&
                                !detailColumns.Contains(c.ColumnName, StringComparer.OrdinalIgnoreCase))
                    .ToList();
                // Render the address components as one readable Address column.
                // The source DataTable remains unchanged; this affects HTML presentation only.
                string[] addressColumns = new string[]
                {
                    "FamMemAddr1", "FamMemAddr2", "FamMemArea", "FamMemCity",
                    "FamMemState", "FamMemPIN", "FamMemCountry"
                };

                List<DataColumn> actualFamilyColumns = data.Columns.Cast<DataColumn>()
                    .Where(c => familyColumns.Contains(c.ColumnName, StringComparer.OrdinalIgnoreCase) &&
                                !addressColumns.Contains(c.ColumnName, StringComparer.OrdinalIgnoreCase))
                    .ToList();

                html.AppendLine("<div class=\"report-toolbar\"><input type=\"search\" id=\"report-search\" class=\"report-search\" placeholder=\"Search employees and family members...\" oninput=\"filterReport()\" autocomplete=\"off\" /><span id=\"report-count\" class=\"report-count\"></span><div class=\"report-toolbar-actions\"><button type=\"button\" id=\"toggle-all-families\" class=\"toggle global-toggle\" onclick=\"toggleAllFamilies()\">Expand All</button></div></div>");
                html.AppendLine("<table><thead><tr><th></th>");
                foreach (DataColumn column in employeeColumns)
                    html.Append("<th>" + HtmlEncode(column.ColumnName) + "</th>");
                html.AppendLine("</tr></thead><tbody>");

                // Each merged row represents one employee/family-member pair.
                // EmpID is often hidden and omitted from the export data, so use
                // EmpCode as the fallback grouping key. If neither is present,
                // fall back to the available employee-identifying columns.
                // Prefer the visible employee code over EmpID. EmpID can be a
                // hidden/internal value that is not the best grouping key in the
                // formatted export dataset. Normalize whitespace and casing so
                // duplicate employee rows are grouped into a single parent row.
                string employeeKeyColumn = employeeColumns
                    .FirstOrDefault(c => c.ColumnName.IndexOf("Employee Code", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                         c.ColumnName.IndexOf("EmployeeCode", StringComparison.OrdinalIgnoreCase) >= 0 ||
                                         c.ColumnName.IndexOf("EmpCode", StringComparison.OrdinalIgnoreCase) >= 0)?.ColumnName;

                if (employeeKeyColumn == null && data.Columns.Contains("EmpCode"))
                    employeeKeyColumn = "EmpCode";
                if (employeeKeyColumn == null && data.Columns.Contains("EmpID"))
                    employeeKeyColumn = "EmpID";

                var employeeGroups = employeeKeyColumn == null
                    ? data.Rows.Cast<DataRow>().Select((row, index) => new { Key = index.ToString(), Rows = new List<DataRow> { row } })
                    : data.Rows.Cast<DataRow>()
                        .GroupBy(r => Convert.ToString(r[employeeKeyColumn]).Trim(), StringComparer.OrdinalIgnoreCase)
                        .Select(group => new { Key = group.Key, Rows = group.ToList() });
                int groupIndex = 0;
                foreach (var group in employeeGroups)
                {
                    DataRow employeeRow = group.Rows[0];
                    string detailId = "family_detail_" + groupIndex.ToString();
                    html.AppendLine("<tr class=\"employee-row\"><td><button type=\"button\" class=\"toggle employee-toggle\" aria-expanded=\"false\" aria-controls=\"" + detailId + "\" onclick=\"toggleFamily('" + detailId + "',this)\">+</button></td>");
                    foreach (DataColumn column in employeeColumns)
                        html.Append("<td>" + HtmlEncode(employeeRow[column] == DBNull.Value ? string.Empty : Convert.ToString(employeeRow[column])) + "</td>");
                    html.AppendLine("</tr>");
                    html.AppendLine("<tr id=\"" + detailId + "\" class=\"family-detail\" data-expanded=\"false\" style=\"display:none\"><td colspan=\"" + (employeeColumns.Count + 1).ToString() + "\">");
                    html.AppendLine("<div class=\"family-heading\">Family Members Information</div>");

                    List<DataRow> familyRows = group.Rows.Where(r => actualFamilyColumns.Any(c => r[c] != DBNull.Value && !string.IsNullOrWhiteSpace(Convert.ToString(r[c])))).ToList();
                    if (familyRows.Count == 0)
                    {
                        html.AppendLine("<div class=\"empty-family\">No family-member information available.</div>");
                    }
                    else
                    {
                        html.AppendLine("<table class=\"family-table\"><colgroup>");
                        foreach (DataColumn column in actualFamilyColumns)
                            html.AppendLine("<col class=\"" + GetFamilyMemberColumnCssClass(column.ColumnName) + "\" />");
                        html.AppendLine("<col class=\"col-family-address\" />");
                        html.AppendLine("</colgroup><thead><tr>");
                        foreach (DataColumn column in actualFamilyColumns)
                            html.Append("<th>" + HtmlEncode(GetFamilyMemberDisplayHeader(column.ColumnName)) + "</th>");
                        html.AppendLine("<th>Address</th>");
                        html.AppendLine("</tr></thead><tbody>");
                        foreach (DataRow familyRow in familyRows)
                        {
                            html.AppendLine("<tr>");
                            foreach (DataColumn column in actualFamilyColumns)
                            {
                                string value = familyRow[column] == DBNull.Value ? string.Empty : Convert.ToString(familyRow[column]);

                                DateTime dtValue;
                                if (DateTime.TryParse(value, out dtValue))
                                    value = dtValue.ToString("dd-MMM-yyyy");

                                value = ConvertBooleanDisplayValue(value);
                                html.Append("<td>" + HtmlEncode(value) + "</td>");
                            }

                            List<string> addressParts = new List<string>();
                            foreach (string addressColumn in addressColumns)
                            {
                                if (data.Columns.Contains(addressColumn) && familyRow[addressColumn] != DBNull.Value)
                                {
                                    string addressPart = Convert.ToString(familyRow[addressColumn]).Trim();
                                    if (!string.IsNullOrWhiteSpace(addressPart))
                                        addressParts.Add(addressPart);
                                }
                            }
                            html.Append("<td>" + HtmlEncode(string.Join(", ", addressParts)) + "</td>");
                            html.AppendLine("</tr>");
                        }
                        html.AppendLine("</tbody></table>");
                    }
                    // Nominee details are rendered in their own subtable.
                    List<DataRow> nomineeRows = group.Rows.Where(r => nomineeColumns.Any(c => data.Columns.Contains(c) && r[c] != DBNull.Value && !string.IsNullOrWhiteSpace(Convert.ToString(r[c])))).ToList();
                    html.AppendLine("<div class=\"family-heading\">Nominee Information</div>");
                    AppendSimpleDetailTable(html, nomineeRows, nomineeColumns, data, "No nominee information available.");

                    // Education qualification supports multiple rows per employee.
                    List<DataRow> educationRows = group.Rows.Where(r => educationColumns.Any(c => data.Columns.Contains(c) && r[c] != DBNull.Value && !string.IsNullOrWhiteSpace(Convert.ToString(r[c])))).ToList();
                    html.AppendLine("<div class=\"family-heading\">Education Qualification Information</div>");
                    AppendSimpleDetailTable(html, educationRows, educationColumns, data, "No education qualification information available.");

                    // Skills are displayed in their own subtable and may contain multiple rows per employee.
                    List<DataRow> employeeSkillRows = group.Rows.Where(r => skillsColumns.Any(c => data.Columns.Contains(c) && r[c] != DBNull.Value && !string.IsNullOrWhiteSpace(Convert.ToString(r[c])))).ToList();
                    html.AppendLine("<div class=\"family-heading\">Skills Information</div>");
                    AppendSimpleDetailTable(html, employeeSkillRows, skillsColumns, data, "No skills information available.");

                    // Employee-specific Bank Information.
                    AppendEmployeeBankInformation(html, group.Rows, data);

                    // Leave Information: render the employee's leave entitlement records.
                    // EmpID is carried internally through the export dataset for this lookup.
                    AppendLeaveInformationPlaceholder(html, group.Rows, data);

                    // Employee-specific Advance Statements.
                    AppendEmployeeAdvanceStatements(html, group.Rows);

                    // Employee-specific monthly Payslip Statements.
                    AppendEmployeePayslipStatements(html, group.Rows);

                    // Consolidated Attendance Report placeholder.
                    // Monthly attendance values will be populated by the attendance DAL.
                    AppendConsolidatedAttendancePlaceholder(html, group.Rows, data);

                    html.AppendLine("</td></tr>");
                    groupIndex++;
                }
                html.AppendLine("</tbody></table>");
            }
            else
            {
                // Preserve the existing flat-table presentation for every other export.
                html.AppendLine("<table><thead><tr>");
                foreach (DataColumn column in data.Columns)
                    html.Append("<th>" + HtmlEncode(column.ColumnName) + "</th>");
                html.AppendLine("</tr></thead><tbody>");
                foreach (DataRow row in data.Rows)
                {
                    html.AppendLine("<tr>");
                    foreach (DataColumn column in data.Columns)
                    {
                        string value = row[column] == DBNull.Value ? string.Empty : Convert.ToString(row[column]);
                        html.Append("<td>" + HtmlEncode(value) + "</td>");
                    }
                    html.AppendLine("</tr>");
                }
                html.AppendLine("</tbody></table>");
            }

            html.AppendLine("<script>if(document.getElementById('report-search'))filterReport();else{var count=document.getElementById('report-count');if(count)count.textContent='Showing " + data.Rows.Count.ToString() + " of " + data.Rows.Count.ToString() + " records';}</script>");
            html.AppendLine("</div><div class=\"footer\">StaffSync Payroll Management - Exported rows: " + data.Rows.Count.ToString() + "</div>");
            html.AppendLine("</div></body></html>");
            File.WriteAllText(outputFile, html.ToString(), new UTF8Encoding(false));
        }

        /// <summary>
        /// Renders employee-specific bank account details in their own subtable.
        /// Data is read from getEmployeeSpecificBankAccountInfo and supports the
        /// field names currently used by the employee bank account DAL.
        /// </summary>
        private void AppendEmployeeBankInformation(StringBuilder html, List<DataRow> employeeRows, DataTable data)
        {
            html.AppendLine("<div class=\"family-heading\">Bank Information</div>");
            html.AppendLine("<table class=\"family-table bank-information-table\"><thead><tr><th>Bank Name</th><th>A/C Number</th><th>Address</th><th>IFSC Code</th></tr></thead><tbody>");

            int empId = 0;
            if (employeeRows != null)
            {
                foreach (DataRow employeeRow in employeeRows)
                {
                    if (employeeRow == null || employeeRow.RowState == DataRowState.Deleted ||
                        employeeRow.Table == null || !employeeRow.Table.Columns.Contains("EmpID") ||
                        employeeRow["EmpID"] == DBNull.Value)
                        continue;
                    int.TryParse(Convert.ToString(employeeRow["EmpID"]), out empId);
                    if (empId > 0) break;
                }
            }

            DataTable bankData = new DataTable();
            if (empId > 0)
            {
                try
                {
                    bankData = ConvertListToDataTable(objEmployeeBankACInfo.getEmployeeSpecificBankAccountInfo(empId));
                }
                catch
                {
                    bankData = new DataTable();
                }
            }

            bool hasRows = false;
            if (bankData != null)
            {
                foreach (DataRow bankRow in bankData.Rows)
                {
                    if (bankData.Columns.Contains("EmpID") && bankRow["EmpID"] != DBNull.Value &&
                        Convert.ToString(bankRow["EmpID"]) != Convert.ToString(empId))
                        continue;

                    // Use the actual source-column names returned by clsEmployeeBankACInfo.
                    // In this DAL, the account number may be named EmpACNumber rather than AccountNumber.
                    string bankNameColumn = GetFirstExistingColumn(bankData,
                        "EmployeeBankACName", "BankName", "BankTitle", "BankTitleName");
                    string accountColumn = GetFirstExistingColumn(bankData,
                        "EmpACNumber", "EmployeeBankACNumber", "AccountNumber", "AccountNo",
                        "BankAccountNumber", "EmployeeBankAccountNumber");
                    string addressColumn = GetFirstExistingColumn(bankData,
                        "EmployeeBankAddress", "Address", "BankAddress", "BranchAddress",
                        "BankBranchAddress", "AccountAddress");
                    string ifscColumn = GetFirstExistingColumn(bankData,
                        "IFSCCode", "EmployeeBankIFSCCode", "IFSC", "EmpIFSCCode");
                    string bankName = string.IsNullOrWhiteSpace(bankNameColumn) || bankRow[bankNameColumn] == DBNull.Value ? string.Empty : Convert.ToString(bankRow[bankNameColumn]);
                    string account = string.IsNullOrWhiteSpace(accountColumn) || bankRow[accountColumn] == DBNull.Value ? string.Empty : Convert.ToString(bankRow[accountColumn]);
                    string address = string.IsNullOrWhiteSpace(addressColumn) || bankRow[addressColumn] == DBNull.Value ? string.Empty : Convert.ToString(bankRow[addressColumn]);
                    string ifsc = string.IsNullOrWhiteSpace(ifscColumn) || bankRow[ifscColumn] == DBNull.Value ? string.Empty : Convert.ToString(bankRow[ifscColumn]);
                    if (string.IsNullOrWhiteSpace(bankName) && string.IsNullOrWhiteSpace(account) && string.IsNullOrWhiteSpace(address) && string.IsNullOrWhiteSpace(ifsc))
                        continue;

                    html.AppendLine("<tr><td>" + HtmlEncode(bankName) + "</td><td>" + HtmlEncode(account) + "</td><td>" + HtmlEncode(address) + "</td><td>" + HtmlEncode(ifsc) + "</td></tr>");
                    hasRows = true;
                }
            }

            if (!hasRows)
                html.AppendLine("<tr><td colspan=\"4\" class=\"empty-family\">No bank information available for this employee.</td></tr>");
            html.AppendLine("</tbody></table>");
        }

        /// <summary>
        /// Renders the employee-specific leave entitlement rows supplied by
        /// objEmpLeaveEntitlementInfo.getEmployeeSpecificLeaveInfo(empId).
        /// The DAL fields are LeaveTypeTitle, TotalLeaves and BalanceLeaves.
        /// If a utilised-leave field is returned, it is used; otherwise utilised
        /// leaves are calculated as TotalLeaves - BalanceLeaves.
        /// </summary>
        private void AppendLeaveInformationPlaceholder(StringBuilder html, List<DataRow> employeeRows, DataTable data)
        {
            html.AppendLine("<div class=\"family-heading\">Leave Information</div>");
            html.AppendLine("<table class=\"family-table leave-table\"><colgroup>");
            html.AppendLine("<col class=\"col-leave-title\" />");
            html.AppendLine("<col class=\"col-leave-alloted\" />");
            html.AppendLine("<col class=\"col-leave-utilised\" />");
            html.AppendLine("<col class=\"col-leave-balance\" />");
            html.AppendLine("</colgroup><thead><tr>");
            html.AppendLine("<th>LeaveTitle</th><th>Leave Alloted</th><th>Leave Utilised</th><th>Leave Balance</th>");
            html.AppendLine("</tr></thead><tbody>");

            bool hasLeaveRows = false;
            int empId = 0;

            // Fetch the leave rows directly while rendering. This avoids losing them
            // when leave, attendance, family and skills are merged by row index.
            if (employeeRows != null)
            {
                foreach (DataRow employeeRow in employeeRows)
                {
                    if (employeeRow == null || employeeRow.RowState == DataRowState.Deleted ||
                        employeeRow.Table == null || !employeeRow.Table.Columns.Contains("EmpID") ||
                        employeeRow["EmpID"] == DBNull.Value)
                        continue;
                    int.TryParse(Convert.ToString(employeeRow["EmpID"]), out empId);
                    if (empId > 0) break;
                }
            }

            DataTable leaveData = new DataTable();
            if (empId > 0)
            {
                try
                {
                    leaveData = ConvertListToDataTable(objEmpLeaveEntitlementInfo.getEmployeeSpecificLeaveInfo(empId));
                }
                catch
                {
                    leaveData = new DataTable();
                }
            }

            if (leaveData != null && leaveData.Rows.Count > 0)
            {
                string titleColumn = GetFirstExistingColumn(leaveData, "LeaveTypeTitle", "LeaveTitle", "LeaveType", "LeaveName");
                string allottedColumn = GetFirstExistingColumn(leaveData, "TotalLeaves", "LeaveAlloted", "LeavesAllotted", "AllottedLeaves");
                string balanceColumn = GetFirstExistingColumn(leaveData, "BalanceLeaves", "LeaveBalance", "LeavesBalance", "Balance");
                string utilisedColumn = GetFirstExistingColumn(leaveData, "UtilisedLeaves", "LeaveUtilised", "LeavesUtilised", "UtilizedLeaves", "LeaveUsed");

                foreach (DataRow leaveRow in leaveData.Rows)
                {
                    if (leaveRow == null || leaveRow.RowState == DataRowState.Deleted) continue;
                    string leaveTitle = titleColumn == null || leaveRow[titleColumn] == DBNull.Value
                        ? string.Empty : Convert.ToString(leaveRow[titleColumn]).Trim();
                    if (string.IsNullOrWhiteSpace(leaveTitle)) continue;

                    decimal allotted = allottedColumn == null ? 0m : GetLeaveDecimal(leaveRow, allottedColumn);
                    decimal balance = balanceColumn == null ? 0m : GetLeaveDecimal(leaveRow, balanceColumn);
                    decimal utilised = utilisedColumn != null ? GetLeaveDecimal(leaveRow, utilisedColumn) : allotted - balance;
                    if (utilised < 0m) utilised = 0m;

                    html.AppendLine("<tr>");
                    html.AppendLine("<td>" + HtmlEncode(leaveTitle) + "</td>");
                    html.AppendLine("<td>" + allotted.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + "</td>");
                    html.AppendLine("<td>" + utilised.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + "</td>");
                    html.AppendLine("<td>" + balance.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + "</td>");
                    html.AppendLine("</tr>");
                    hasLeaveRows = true;
                }
            }

            // Compatibility fallback: use leave values already merged into the report.
            if (!hasLeaveRows && employeeRows != null && data != null &&
                data.Columns.Contains("LeaveTitle") && data.Columns.Contains("LeaveAlloted") && data.Columns.Contains("LeaveBalance"))
            {
                foreach (DataRow row in employeeRows)
                {
                    if (row == null || row.RowState == DataRowState.Deleted || row["LeaveTitle"] == DBNull.Value) continue;
                    string leaveTitle = Convert.ToString(row["LeaveTitle"]).Trim();
                    if (string.IsNullOrWhiteSpace(leaveTitle)) continue;
                    decimal allotted = GetLeaveDecimal(row, "LeaveAlloted");
                    decimal balance = GetLeaveDecimal(row, "LeaveBalance");
                    decimal utilised = data.Columns.Contains("LeaveUtilised") && row["LeaveUtilised"] != DBNull.Value
                        ? GetLeaveDecimal(row, "LeaveUtilised") : allotted - balance;
                    if (utilised < 0m) utilised = 0m;
                    html.AppendLine("<tr>");
                    html.AppendLine("<td>" + HtmlEncode(leaveTitle) + "</td>");
                    html.AppendLine("<td>" + allotted.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + "</td>");
                    html.AppendLine("<td>" + utilised.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + "</td>");
                    html.AppendLine("<td>" + balance.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) + "</td>");
                    html.AppendLine("</tr>");
                    hasLeaveRows = true;
                }
            }

            if (!hasLeaveRows)
                html.AppendLine("<tr><td colspan=\"4\" class=\"empty-family\">No leave information available for this employee.</td></tr>");
            html.AppendLine("</tbody></table>");
        }

        /// <summary>
        /// Renders the employee's month-wise consolidated payslip values.
        /// Net Salary is calculated as Basic Salary + Total Allowances - Total Deductions
        /// + Total Reimbursements. Values are taken from the rows already populated
        /// by getEmployeeSpecificConsolidatedPayslipValues in BuildEmployeeInformationWithDetails.
        /// </summary>
        private void AppendEmployeePayslipStatements(StringBuilder html, List<DataRow> employeeRows)
        {
            html.AppendLine("<div class=\"family-heading\">Employee Specific Payslip Statements</div>");
            html.AppendLine("<table class=\"family-table payslip-statement-table\"><thead><tr>");
            html.AppendLine("<th>Salary Date</th><th>Basic Salary</th><th>Total Allowances</th><th>Total Deductions</th><th>Total Reimbursements</th><th>Net Salary</th>");
            html.AppendLine("</tr></thead><tbody>");

            // Load the payslip rows directly for the employee being rendered. This avoids
            // relying on the flattened employee/detail rows to carry every payslip field.
            int employeeId = 0;
            if (employeeRows != null)
            {
                foreach (DataRow employeeRow in employeeRows)
                {
                    if (employeeRow == null || employeeRow.RowState == DataRowState.Deleted || employeeRow.Table == null)
                        continue;
                    string idColumn = GetFirstExistingColumn(employeeRow.Table, "EmpID", "EmployeeID", "EmpId");
                    if (!string.IsNullOrEmpty(idColumn) && employeeRow[idColumn] != DBNull.Value &&
                        int.TryParse(Convert.ToString(employeeRow[idColumn]), out employeeId) && employeeId > 0)
                        break;
                }
            }

            DataTable payslipData = new DataTable("EmployeePayslipStatements");
            if (employeeId > 0)
            {
                try
                {
                    payslipData = ConvertListToDataTable(
                        objEmpPayroll.getEmployeeSpecificConsolidatedPayslipValues(employeeId));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(
                        "Unable to load employee-specific payslip statements: " + ex.Message);
                }
            }

            bool found = false;
            if (payslipData != null && payslipData.Rows.Count > 0)
            {
                string empIdColumn = GetFirstExistingColumn(payslipData, "EmpID", "EmployeeID", "EmpId");
                string salaryDateColumn = FindPayslipColumn(payslipData,
                    new string[] { "SalaryMonth", "SalaryDate", "EmpSalDate", "EmpSalMonth", "SalDate", "PayDate", "PayrollDate", "MonthYear", "SalaryPeriod", "Month", "Date" },
                    "date");
                string basicColumn = FindPayslipColumn(payslipData,
                    new string[] { "Basic Salary", "BasicSalary", "BasicSal", "EmpSalBasic", "BasicAmount", "BasicPay", "Basic", "BasicSalaryAmount" },
                    "basic");
                string allowanceColumn = FindPayslipColumn(payslipData,
                    new string[] { "TotalAllowanceAmount", "TotalAllowances", "TotalAllowance", "AllowanceAmount", "Total Allowances", "AllAmount", "AllowanceTotal", "Allowances" },
                    "allowance");
                string deductionColumn = FindPayslipColumn(payslipData,
                    new string[] { "TotalDeductionAmount", "TotalDeductions", "TotalDeduction", "DeductionAmount", "Total Deductions", "DedAmount", "DeductionTotal", "Deductions" },
                    "deduction");
                string reimbursementColumn = FindPayslipColumn(payslipData,
                    new string[] { "TotalReimbursementAmount", "TotalReimbursements", "TotalReimbursement", "ReimbursementAmount", "Total Reimbursements", "TotalReimbursmentAmount", "TotalReimbursementAmout", "ReimbAmount", "ReimbursementTotal", "Reimbursements" },
                    "reimburs");

                IEnumerable<DataRow> sourceRows = payslipData.Rows.Cast<DataRow>();
                if (!string.IsNullOrEmpty(empIdColumn))
                    sourceRows = sourceRows.Where(r => r[empIdColumn] == DBNull.Value ||
                        Convert.ToString(r[empIdColumn]) == employeeId.ToString());

                foreach (DataRow row in sourceRows)
                {
                    if (row == null || row.RowState == DataRowState.Deleted) continue;
                    object salaryDate = GetPayslipRawValue(row, salaryDateColumn);
                    object basicRaw = GetPayslipRawValue(row, basicColumn);
                    object allowanceRaw = GetPayslipRawValue(row, allowanceColumn);
                    object deductionRaw = GetPayslipRawValue(row, deductionColumn);
                    object reimbursementRaw = GetPayslipRawValue(row, reimbursementColumn);

                    bool hasAnyValue = (salaryDate != DBNull.Value) || (basicRaw != DBNull.Value) ||
                        (allowanceRaw != DBNull.Value) || (deductionRaw != DBNull.Value) ||
                        (reimbursementRaw != DBNull.Value);
                    if (!hasAnyValue) continue;

                    decimal basic = GetPayslipDecimalValue(basicRaw);
                    decimal allowances = GetPayslipDecimalValue(allowanceRaw);
                    decimal deductions = GetPayslipDecimalValue(deductionRaw);
                    decimal reimbursements = GetPayslipDecimalValue(reimbursementRaw);
                    decimal netSalary = basic + allowances - deductions + reimbursements;

                    html.AppendLine("<tr>");
                    html.AppendLine("<td>" + HtmlEncode(GetPayslipDateText(salaryDate)) + "</td>");
                    html.AppendLine("<td class=\"numeric\">" + HtmlEncode(basic.ToString("N2", CultureInfo.InvariantCulture)) + "</td>");
                    html.AppendLine("<td class=\"numeric\">" + HtmlEncode(allowances.ToString("N2", CultureInfo.InvariantCulture)) + "</td>");
                    html.AppendLine("<td class=\"numeric\">" + HtmlEncode(deductions.ToString("N2", CultureInfo.InvariantCulture)) + "</td>");
                    html.AppendLine("<td class=\"numeric\">" + HtmlEncode(reimbursements.ToString("N2", CultureInfo.InvariantCulture)) + "</td>");
                    html.AppendLine("<td class=\"numeric\"><strong>" + HtmlEncode(netSalary.ToString("N2", CultureInfo.InvariantCulture)) + "</strong></td>");
                    html.AppendLine("</tr>");
                    found = true;
                }
            }

            if (!found)
                html.AppendLine("<tr><td colspan=\"6\" class=\"empty-family\">No payslip information available for this employee.</td></tr>");
            html.AppendLine("</tbody></table>");
        }

        /// <summary>
        /// Finds a payslip column using exact aliases first, then a normalized
        /// case/spacing-insensitive name match, and finally a conservative semantic match.
        /// </summary>
        private string FindPayslipColumn(DataTable table, string[] aliases, string semantic)
        {
            if (table == null) return null;

            string exact = GetFirstExistingColumn(table, aliases);
            if (!string.IsNullOrEmpty(exact)) return exact;

            Func<string, string> normalize = value => new string((value ?? string.Empty)
                .Where(char.IsLetterOrDigit).Select(char.ToLowerInvariant).ToArray());
            foreach (string alias in aliases ?? new string[0])
            {
                string normalizedAlias = normalize(alias);
                foreach (DataColumn column in table.Columns)
                {
                    if (normalize(column.ColumnName) == normalizedAlias)
                        return column.ColumnName;
                }
            }

            foreach (DataColumn column in table.Columns)
            {
                string name = normalize(column.ColumnName);
                if (semantic == "date" &&
                    (name.Contains("salarydate") || name.Contains("salarymonth") || name.Contains("paydate") || name.Contains("payrollmonth") || name.Contains("salaryperiod")))
                    return column.ColumnName;
                if (semantic == "basic" && name.Contains("basic") && !name.Contains("allowance") && !name.Contains("deduction"))
                    return column.ColumnName;
                if (semantic == "allowance" && name.Contains("allowance") && !name.Contains("deduction"))
                    return column.ColumnName;
                if (semantic == "deduction" && name.Contains("deduction"))
                    return column.ColumnName;
                if (semantic == "reimburs" && (name.Contains("reimburs") || name.Contains("reimburse")))
                    return column.ColumnName;
            }
            return null;
        }

        private object GetPayslipRawValue(DataRow row, string columnName)
        {
            return row == null || row.Table == null || string.IsNullOrEmpty(columnName) ||
                !row.Table.Columns.Contains(columnName) || row[columnName] == DBNull.Value
                ? (object)DBNull.Value : row[columnName];
        }

        private decimal GetPayslipDecimal(DataRow row, string columnName)
        {
            return GetPayslipDecimalValue(GetPayslipRawValue(row, columnName));
        }

        private decimal GetPayslipDecimalValue(object value)
        {
            if (value == null || value == DBNull.Value) return 0m;
            decimal amount;
            if (decimal.TryParse(Convert.ToString(value), NumberStyles.Any, CultureInfo.InvariantCulture, out amount))
                return amount;
            if (decimal.TryParse(Convert.ToString(value), NumberStyles.Any, CultureInfo.CurrentCulture, out amount))
                return amount;
            return 0m;
        }

        private string GetPayslipDateText(object value)
        {
            if (value == null || value == DBNull.Value) return string.Empty;
            DateTime date;
            return DateTime.TryParse(Convert.ToString(value), out date)
                ? date.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture)
                : Convert.ToString(value);
        }

        private DateTime GetPayslipDateSortValue(object value)
        {
            if (value == null || value == DBNull.Value) return DateTime.MaxValue;
            DateTime date;
            return DateTime.TryParse(Convert.ToString(value), out date) ? date : DateTime.MaxValue;
        }

        private string GetPayslipMoneyText(object value)
        {
            return GetPayslipDecimalValue(value).ToString("N2", CultureInfo.InvariantCulture);
        }

        private string GetFirstExistingColumn(DataTable table, params string[] candidates)
        {
            if (table == null || candidates == null) return null;
            foreach (string candidate in candidates)
            {
                if (table.Columns.Contains(candidate)) return candidate;
                foreach (DataColumn column in table.Columns)
                    if (string.Equals(column.ColumnName, candidate, StringComparison.OrdinalIgnoreCase)) return column.ColumnName;
            }
            return null;
        }

        /// <summary>
        /// Safely reads a numeric leave field, returning zero for missing/null values.
        /// </summary>
        private decimal GetLeaveDecimal(DataRow row, string columnName)
        {
            if (row == null || row.Table == null ||
                !row.Table.Columns.Contains(columnName) ||
                row[columnName] == DBNull.Value)
                return 0m;

            decimal value;
            return decimal.TryParse(
                Convert.ToString(row[columnName]),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out value)
                ? value
                : 0m;
        }

        /// <summary>
        /// Loads and renders employee-specific advance statements directly from the
        /// existing advance DAL. This avoids depending on advance values being copied
        /// into the flattened employee-detail rows before HTML rendering.
        /// </summary>
        private void AppendEmployeeAdvanceStatements(StringBuilder html, List<DataRow> employeeRows)
        {
            html.AppendLine("<div class=\"family-heading\">Employee Specific Advance Statements</div>");
            html.AppendLine("<table class=\"family-table advance-statement-table\"><colgroup>");
            // The standalone Date column was redundant: the issue date is shown in Issue Date.
            html.AppendLine("<col class=\"col-advance-name\" /><col class=\"col-advance-amount\" />");
            html.AppendLine("<col class=\"col-advance-issue-date\" /><col class=\"col-advance-last-payment\" />");
            html.AppendLine("<col class=\"col-advance-outstanding\" />");
            html.AppendLine("</colgroup><thead><tr>");
            html.AppendLine("<th>Advance Name</th><th>Advance Amount</th><th>Issue Date</th><th>Last Payment Date</th><th>Outstanding Balance</th>");
            html.AppendLine("</tr></thead><tbody>");

            int employeeId = 0;
            if (employeeRows != null)
            {
                foreach (DataRow employeeRow in employeeRows)
                {
                    if (employeeRow == null || employeeRow.RowState == DataRowState.Deleted || employeeRow.Table == null)
                        continue;
                    string empIdColumn = GetFirstExistingColumn(employeeRow.Table, "EmpID", "EmployeeID", "EmpId");
                    if (string.IsNullOrEmpty(empIdColumn) || employeeRow[empIdColumn] == DBNull.Value)
                        continue;
                    if (int.TryParse(Convert.ToString(employeeRow[empIdColumn]), out employeeId) && employeeId > 0)
                        break;
                }
            }

            DataTable advances = new DataTable("EmployeeAdvanceStatements");
            if (employeeId > 0)
            {
                try
                {
                    advances = ConvertListToDataTable(objEmployeeAdvanceInfo.getEmployeeAdvanceInformationList(employeeId));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Unable to load employee advance statements: " + ex.Message);
                }
            }

            bool found = false;
            if (advances != null && advances.Rows.Count > 0)
            {
                string empIdColumn = GetFirstExistingColumn(advances, "EmpID", "EmployeeID", "EmpId");
                string nameColumn = GetFirstExistingColumn(advances, "AdvanceName", "AdvanceTypeTitle", "AdvTypeTitle", "AdvanceTitle", "AdvanceTypeName", "AdvName");
                string requestIdColumn = GetFirstExistingColumn(advances, "EmpAdvanceRequestID", "AdvanceRequestID", "EmpAdvRequestID");
                string orderColumn = GetFirstExistingColumn(advances, "OrderID", "OrderId", "RecoveryOrderID");
                string advanceDateColumn = GetFirstExistingColumn(advances, "AdvanceDate", "Date", "AdvDate", "CreatedDate", "TransactionDate", "IssueDate", "AdvanceIssueDate");
                string amountColumn = GetFirstExistingColumn(advances, "IssueAmount", "Issue Amount", "AdvanceAmount", "Advance Amount", "AdvAmount", "Amount", "IssuedAmount", "TotalAdvanceAmount", "RequestAmount", "SanctionedAmount");
                string issueDateColumn = GetFirstExistingColumn(advances, "IssueDate", "AdvanceIssueDate", "AdvIssueDate", "DateOfIssue", "IssuedDate");
                string lastPaymentColumn = GetFirstExistingColumn(advances, "LastPaymentDate", "Last Payment Date", "LastPaidDate", "LastPayment", "LastRecoveryDate", "LastInstallmentDate");
                string outstandingColumn = GetFirstExistingColumn(advances, "OutstandingBalance", "Outstanding Balance", "OutstandingAmount", "BalanceAmount", "Balance", "RemainingAmount", "PendingAmount");
                string closingBalanceColumn = GetFirstExistingColumn(advances, "CBalance", "ClosingBalance", "CurrentBalance");

                // Filter the DAL result to this employee, then group recovery rows into one
                // statement per advance request (or per advance name if the request ID is absent).
                List<DataRow> employeeAdvanceRows = advances.Rows.Cast<DataRow>()
                    .Where(r => r != null && r.RowState != DataRowState.Deleted)
                    .Where(r => string.IsNullOrEmpty(empIdColumn) || r[empIdColumn] == DBNull.Value ||
                        Convert.ToString(r[empIdColumn]) == employeeId.ToString())
                    .ToList();

                var advanceGroups = employeeAdvanceRows.GroupBy(r =>
                {
                    string requestId = !string.IsNullOrEmpty(requestIdColumn) ? GetAdvanceText(r, requestIdColumn) : string.Empty;
                    if (!string.IsNullOrWhiteSpace(requestId)) return "REQUEST:" + requestId;
                    string advanceName = GetAdvanceText(r, nameColumn);
                    return "NAME:" + (string.IsNullOrWhiteSpace(advanceName) ? "Unknown" : advanceName);
                });

                foreach (var advanceGroup in advanceGroups)
                {
                    List<DataRow> records = advanceGroup.ToList();
                    if (!string.IsNullOrEmpty(orderColumn))
                    {
                        records = records.OrderBy(r =>
                        {
                            decimal order;
                            return decimal.TryParse(GetAdvanceText(r, orderColumn), NumberStyles.Any, CultureInfo.InvariantCulture, out order) ? order : decimal.MaxValue;
                        }).ToList();
                    }
                    else if (!string.IsNullOrEmpty(advanceDateColumn))
                    {
                        records = records.OrderBy(r =>
                        {
                            DateTime date;
                            return DateTime.TryParse(GetAdvanceText(r, advanceDateColumn), out date) ? date : DateTime.MaxValue;
                        }).ToList();
                    }

                    if (records.Count == 0) continue;
                    DataRow firstRecord = records[0];
                    DataRow lastRecord = records[records.Count - 1];

                    string advanceName = GetAdvanceText(firstRecord, nameColumn);
                    if (string.IsNullOrWhiteSpace(advanceName)) advanceName = GetAdvanceText(lastRecord, nameColumn);

                    // The validated Access query uses CBalance from the earliest recovery
                    // record as the advance amount (e.g. 800.00), and CBalance from the
                    // latest recovery record as the outstanding balance (e.g. 675.00).
                    // IssueAmount is the actual amount issued (₹800 in the validated query).
                    // CBalance is the remaining balance (₹675 in the validated query),
                    // so only use it as an amount fallback if no issued-amount field exists.
                    string advanceAmount = GetFirstNonEmptyAdvanceText(firstRecord,
                        "IssueAmount", "Issue Amount", amountColumn, "AdvanceAmount", "Advance Amount", "AdvAmount", "Amount", "IssuedAmount", "TotalAdvanceAmount", "RequestAmount", "SanctionedAmount");
                    if (string.IsNullOrWhiteSpace(advanceAmount))
                        advanceAmount = GetFirstNonEmptyAdvanceText(lastRecord,
                            "IssueAmount", "Issue Amount", amountColumn, "AdvanceAmount", "Advance Amount", "AdvAmount", "Amount", "IssuedAmount", "TotalAdvanceAmount", "RequestAmount", "SanctionedAmount");

                    string issueDate = GetFirstNonEmptyAdvanceText(firstRecord,
                        issueDateColumn, "IssueDate", "AdvanceIssueDate", "AdvIssueDate", "DateOfIssue", "IssuedDate", advanceDateColumn, "AdvanceDate", "Date");
                    string lastPaymentDate = GetFirstNonEmptyAdvanceText(lastRecord,
                        lastPaymentColumn, "LastPaymentDate", "Last Payment Date", "LastPaidDate", "LastRecoveryDate", "LastInstallmentDate", advanceDateColumn, "AdvanceDate", "Date");
                    string outstanding = GetFirstNonEmptyAdvanceText(lastRecord,
                        outstandingColumn, "OutstandingBalance", "Outstanding Balance", "OutstandingAmount", "BalanceAmount", "Balance", "RemainingAmount", "PendingAmount", closingBalanceColumn, "CBalance", "ClosingBalance", "CurrentBalance");

                    if (string.IsNullOrWhiteSpace(advanceName) && string.IsNullOrWhiteSpace(advanceAmount) &&
                        string.IsNullOrWhiteSpace(issueDate) && string.IsNullOrWhiteSpace(lastPaymentDate) &&
                        string.IsNullOrWhiteSpace(outstanding))
                        continue;

                    html.AppendLine("<tr><td>" + HtmlEncode(advanceName) + "</td>" +
                        "<td class=\"numeric\">" + HtmlEncode(FormatAdvanceMoney(advanceAmount)) + "</td>" +
                        "<td>" + HtmlEncode(FormatAdvanceDate(issueDate)) + "</td>" +
                        "<td>" + HtmlEncode(FormatAdvanceDate(lastPaymentDate)) + "</td>" +
                        "<td class=\"numeric\">" + HtmlEncode(FormatAdvanceMoney(outstanding)) + "</td></tr>");
                    found = true;
                }
            }

            if (!found)
                html.AppendLine("<tr><td colspan=\"5\" class=\"empty-family\">No advance statement information available for this employee.</td></tr>");
            html.AppendLine("</tbody></table>");
        }

        private string GetFirstNonEmptyAdvanceText(DataRow row, params string[] columnNames)
        {
            if (row == null || row.Table == null || columnNames == null) return string.Empty;
            foreach (string requestedName in columnNames)
            {
                if (string.IsNullOrWhiteSpace(requestedName)) continue;
                string actualName = GetFirstExistingColumn(row.Table, requestedName);
                if (string.IsNullOrEmpty(actualName) || row[actualName] == DBNull.Value) continue;
                string value = Convert.ToString(row[actualName]).Trim();
                if (!string.IsNullOrWhiteSpace(value)) return value;
            }
            return string.Empty;
        }

        private string FormatAdvanceDate(string value)
        {
            DateTime date;
            return DateTime.TryParse(value, out date) ? date.ToString("dd-MMM-yyyy") : value;
        }

        private string FormatAdvanceMoney(string value)
        {
            decimal amount;
            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out amount) ||
                decimal.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out amount))
                return amount.ToString("N2", CultureInfo.CurrentCulture);
            return value;
        }

        private object GetAdvanceRawValue(DataRow row, string columnName)
        {
            if (row == null || string.IsNullOrEmpty(columnName) || row.Table == null ||
                !row.Table.Columns.Contains(columnName) || row[columnName] == DBNull.Value)
                return DBNull.Value;
            return row[columnName];
        }

        private string GetAdvanceText(DataRow row, string columnName)
        {
            return row == null || row.Table == null || string.IsNullOrEmpty(columnName) || !row.Table.Columns.Contains(columnName) || row[columnName] == DBNull.Value
                ? string.Empty : Convert.ToString(row[columnName]).Trim();
        }

        private string GetAdvanceDateText(DataRow row, string columnName)
        {
            string value = GetAdvanceText(row, columnName);
            DateTime date;
            return DateTime.TryParse(value, out date) ? date.ToString("dd-MMM-yyyy") : value;
        }

        private string GetAdvanceMoneyText(DataRow row, string columnName)
        {
            string value = GetAdvanceText(row, columnName);
            decimal amount;
            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out amount))
                return amount.ToString("N2", CultureInfo.InvariantCulture);
            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out amount))
                return amount.ToString("N2", CultureInfo.CurrentCulture);
            return value;
        }

        /// <summary>
        /// Renders the employee's consolidated monthly attendance report.
        /// Values are supplied from the same Monthly Attendance Register DAL used by the dashboard.
        /// Expected fields: AttendanceMonth, WorkingDays, Present, Leave, PayableDays.
        /// </summary>
        private void AppendConsolidatedAttendancePlaceholder(StringBuilder html, List<DataRow> employeeRows, DataTable data)
        {
            string[] attendanceColumns = new string[]
            {
                "AttendanceMonth", "WorkingDays", "ActualWorkingDays", "Present", "Leave", "PayableDays", "AttendancePercentage"
            };

            html.AppendLine("<div class=\"family-heading\">Consolidated Attendance Report</div>");
            html.AppendLine("<table class=\"family-table attendance-summary-table\"><colgroup>");
            html.AppendLine("<col class=\"col-attendance-month\" />");
            html.AppendLine("<col class=\"col-attendance-total-days\" />");
            html.AppendLine("<col class=\"col-attendance-working-days\" />");
            html.AppendLine("<col class=\"col-attendance-present\" />");
            html.AppendLine("<col class=\"col-attendance-leave\" />");
            html.AppendLine("<col class=\"col-attendance-payable-days\" />");
            html.AppendLine("<col class=\"col-attendance-percentage\" />");
            html.AppendLine("</colgroup><thead><tr>");
            html.AppendLine("<th>Month - Year</th>");
            html.AppendLine("<th>Total Days in Month</th>");
            html.AppendLine("<th>Actual Working Days</th>");
            html.AppendLine("<th>Present</th>");
            html.AppendLine("<th>Leave</th>");
            html.AppendLine("<th>Payable Days</th>");
            html.AppendLine("<th>Attendance % (Working Days)</th>");
            html.AppendLine("</tr></thead><tbody>");

            List<DataRow> attendanceRows = employeeRows == null
                ? new List<DataRow>()
                : employeeRows.Where(row =>
                    data != null &&
                    attendanceColumns.All(column => data.Columns.Contains(column)) &&
                    row["AttendanceMonth"] != DBNull.Value &&
                    !string.IsNullOrWhiteSpace(Convert.ToString(row["AttendanceMonth"])))
                    .GroupBy(row => Convert.ToString(row["AttendanceMonth"]), StringComparer.OrdinalIgnoreCase)
                    .Select(group => group.First())
                    .ToList();

            if (attendanceRows.Count == 0)
            {
                html.AppendLine("<tr><td colspan=\"7\" class=\"empty-family\">No monthly attendance records were returned for this employee and year.</td></tr>");
            }
            else
            {
                foreach (DataRow attendanceRow in attendanceRows)
                {
                    html.AppendLine("<tr>");
                    foreach (string column in attendanceColumns)
                    {
                        string value = string.Empty;
                        if (attendanceRow[column] != DBNull.Value)
                        {
                            if (string.Equals(column, "AttendancePercentage", StringComparison.OrdinalIgnoreCase))
                            {
                                decimal percentage;
                                if (TryConvertToDecimal(attendanceRow[column], out percentage))
                                    value = percentage.ToString("0.00", CultureInfo.InvariantCulture) + "%";
                            }
                            else
                            {
                                value = Convert.ToString(attendanceRow[column]);
                            }
                        }
                        html.Append("<td>" + HtmlEncode(value) + "</td>");
                    }
                    html.AppendLine("</tr>");
                }
            }

            html.AppendLine("</tbody></table>");
        }

        private void AppendSimpleDetailTable(StringBuilder html, List<DataRow> rows, string[] columns, DataTable data, string emptyMessage)
        {
            List<string> availableColumns = columns.Where(c => data.Columns.Contains(c)).ToList();
            if (rows == null || rows.Count == 0 || availableColumns.Count == 0)
            {
                html.AppendLine("<div class=\"empty-family\">" + HtmlEncode(emptyMessage) + "</div>");
                return;
            }

            // Determine the subtable type from its existing data columns.
            // This only controls layout; row selection and data rendering stay unchanged.
            string subtableCssClass;
            if (availableColumns.Contains("NomineePerson") ||
                availableColumns.Contains("NomineeContactNumber") ||
                availableColumns.Contains("NomineeRelationShipTitle"))
            {
                subtableCssClass = "nominee-table";
            }
            else if (availableColumns.Contains("EduQualTitle"))
            {
                subtableCssClass = "education-table";
            }
            else if (availableColumns.Contains("SkillTitle") ||
                     availableColumns.Contains("ExpertiseLevel"))
            {
                subtableCssClass = "skills-table";
            }
            else
            {
                subtableCssClass = string.Empty;
            }

            html.AppendLine("<table class=\"family-table " + subtableCssClass + "\"><colgroup>");
            foreach (string column in availableColumns)
            {
                string cssClass = string.Empty;
                switch (column)
                {
                    case "NomineePerson": cssClass = "col-nominee-name"; break;
                    case "NomineeContactNumber": cssClass = "col-nominee-contact"; break;
                    case "NomineeRelationShipTitle": cssClass = "col-nominee-relationship"; break;
                    case "EduQualTitle": cssClass = "col-education-qualification"; break;
                    case "SkillTitle": cssClass = "col-skills-name"; break;
                    case "ExpertiseLevel": cssClass = "col-skills-expertise"; break;
                }
                html.AppendLine("<col class=\"" + cssClass + "\" />");
            }
            html.AppendLine("</colgroup><thead><tr>");
            foreach (string column in availableColumns)
            {
                // Resolve each displayed header explicitly to avoid nested-ternary
                // syntax errors and keep the existing field/header mapping clear.
                string displayHeader;
                switch (column)
                {
                    case "EduQualTitle":
                        displayHeader = "Education Qualification";
                        break;
                    case "SkillTitle":
                        displayHeader = "Skills Name";
                        break;
                    case "ExpertiseLevel":
                        displayHeader = "Expertise Level";
                        break;
                    case "NomineePerson":
                        displayHeader = "Nominee Name";
                        break;
                    case "NomineeContactNumber":
                        displayHeader = "Contact Number";
                        break;
                    case "NomineeRelationShipTitle":
                    case "RelationShipTitle":
                        displayHeader = "Relationship";
                        break;
                    default:
                        displayHeader = column;
                        break;
                }

                html.Append("<th>" + HtmlEncode(displayHeader) + "</th>");
            }
            html.AppendLine("</tr></thead><tbody>");
            foreach (DataRow row in rows)
            {
                html.AppendLine("<tr>");
                foreach (string column in availableColumns)
                {
                    if (string.Equals(column, "ExpertiseLevel", StringComparison.OrdinalIgnoreCase))
                    {
                        int level = 0;
                        if (row[column] != DBNull.Value)
                            int.TryParse(Convert.ToString(row[column]), out level);
                        level = Math.Max(0, Math.Min(10, level));
                        int hue = (int)Math.Round(level * 12.0); // 0 = red, 120 = green
                        html.Append("<td><span class=\"expertise-rating\" title=\"Expertise level " + level.ToString() + " out of 10\">");
                        for (int star = 1; star <= 10; star++)
                        {
                            string starColor = star <= level ? "hsl(" + hue.ToString() + ", 75%, 38%)" : "#d0d5dd";
                            html.Append("<span class=\"expertise-star\" style=\"color:" + starColor + "\">" + (star <= level ? "★" : "☆") + "</span>");
                        }
                        html.Append("<span class=\"expertise-value\">" + level.ToString() + "/10</span></span></td>");
                    }
                    else
                    {
                        html.Append("<td>" + HtmlEncode(row[column] == DBNull.Value ? string.Empty : Convert.ToString(row[column])) + "</td>");
                    }
                }
                html.AppendLine("</tr>");
            }
            html.AppendLine("</tbody></table>");
        }

        private string ConvertBooleanDisplayValue(string value)
        {
            if (string.Equals(value, "True", StringComparison.OrdinalIgnoreCase))
                return "Yes";

            if (string.Equals(value, "False", StringComparison.OrdinalIgnoreCase))
                return "No";

            return value ?? string.Empty;
        }


        private string GetFamilyMemberColumnCssClass(string columnName)
        {
            switch (columnName)
            {
                case "FamMemName": return "col-family-name";
                case "FamMemDOB": return "col-family-dob";
                case "FamMemAge": return "col-family-age";
                case "FamMemRelationship": return "col-family-relationship";
                case "FamMemContactNumber": return "col-family-contact";
                case "FamMemMailID": return "col-family-email";
                case "FamMemBloodGroup": return "col-family-blood";
                case "FamMemInsuranceEnrolled": return "col-family-insurance";
                default: return "";
            }
        }

        private string GetFamilyMemberDisplayHeader(string columnName)
        {
            switch (columnName)
            {
                case "FamMemName": return "Member Name";
                case "FamMemDOB": return "Date of Birth";
                case "FamMemAge": return "Age";
                case "FamMemRelationship": return "Relationship";
                case "FamMemAddr1": return "Address 01";
                case "FamMemAddr2": return "Address 02";
                case "FamMemArea": return "Area";
                case "FamMemCity": return "City";
                case "FamMemState": return "State";
                case "FamMemPIN": return "PIN Code";
                case "FamMemCountry": return "Country";
                case "FamMemContactNumber": return "Contact Number";
                case "FamMemMailID": return "Email ID";
                case "FamMemBloodGroup": return "Blood Group";
                case "FamMemInsuranceEnrolled": return "Insuranced?";
                default: return columnName;
            }
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

                this.Cursor = Cursors.WaitCursor;

                if (dtgImportDataSourceList.Columns.Contains("ImpDataInfoTitle"))
                {
                    lblSelectedDataAction.Text = Convert.ToString(dtgImportDataSourceList.Rows[e.RowIndex].Cells["ImpDataInfoTitle"].Value).Trim();
                }
                else
                {
                    lblSelectedDataAction.Text = Convert.ToString(dtgImportDataSourceList.Rows[e.RowIndex].Cells[3].Value).Trim();
                }

                InitializeExportFormats();
                LoadSelectedExportData();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Unable to load the selected export data." + Environment.NewLine + Environment.NewLine + ex.Message, "Staffsync - Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkSelectOrUnselect_Click(object sender, EventArgs e)
        {
            if (dtgImportDataPreview == null || !dtgImportDataPreview.Columns.Contains("IsSelected"))
            {
                chkSelectOrUnselect.Text = chkSelectOrUnselect.Checked ? "Unselect All" : "Select All";
                lblTotalRowsSelected.Text = "Total Rows Selected : 0";
                return;
            }

            bool selectAll = chkSelectOrUnselect.Checked;

            chkSelectOrUnselect.Text = selectAll ? "Unselect All" : "Select All";

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
