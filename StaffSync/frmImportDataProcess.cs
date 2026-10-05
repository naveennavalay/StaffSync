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
    public partial class frmImportDataProcess : Form
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

        // Import preview state
        private DataTable objImportDataPreviewTable = new DataTable();
        private string objSelectedImportFilePath = string.Empty;
        private bool isImportPreviewEventsAttached = false;
        // Download template button state
        private bool isDownloadTemplateEventsAttached = false;

        private void Control_CellValueChangedCustom(object sender, CellValueChangedEventArgs e)
        {
            // Fires immediately when dropdown changes
            //MessageBox.Show($"RowID: {e.Change.RowIndex}, Column: {e.Change.ColumnIndex}, ColumnName: {e.Change.ColumnName}, OldValue: {e.Change.OldValue}, NewValue: {e.Change.NewValue}");
        }

        public frmImportDataProcess()
        {
            InitializeComponent();
            dtgImportDataSourceList.DataSource = objImportDataInfo.getImportInfoData();
            formatGrid();
            defaultSelectedImportOption();
            //attendanceGridControl1.CellValueChangedCustom += Control_CellValueChangedCustom;
        }

        public frmImportDataProcess(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objActiveClientInfo = objClientInfo.getClientInfoByEmpID(objTempCurrentlyLoggedInUserInfo.EmpID);
            dtgImportDataSourceList.DataSource = objImportDataInfo.getImportInfoData();
            formatGrid();
            defaultSelectedImportOption();
        }

        public frmImportDataProcess(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
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

        public frmImportDataProcess(int txtEmployeeID, int txtLeaveMasID)
        {
            InitializeComponent();
            dtgImportDataSourceList.DataSource = objImportDataInfo.getImportInfoData();
            formatGrid();
            defaultSelectedImportOption();
        }

        private void defaultSelectedImportOption()
        {
            //lblSelectedDataAction.Text = Convert.ToString(dtgImportDataSourceList.Rows[1].Cells["ImpDataInfoTitle"].Value).Trim();
            lblSelectedDataAction.Text = dtgImportDataSourceList[3, 0].Value.ToString();
            string templateFileName = Convert.ToString(dtgImportDataSourceList.Rows[0].Cells["ImpDataInfoTemplateName"].Value).Trim();
            templateFileName = Path.GetFileName(templateFileName);
            string sourceTemplateFolder = Path.Combine(Application.StartupPath, "importtemplate");
            string sourceTemplateFile = Path.Combine(sourceTemplateFolder, templateFileName);

            txtSourceFilePath.Text = sourceTemplateFile;

            DataTable importedData = ReadImportFile(sourceTemplateFile);
            dtgImportDataPreview.DataSource = null;
            dtgImportDataPreview.DataSource = importedData;

            BindImportPreviewData(importedData);

            formatGrid();
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmImportDataProcess_Load(object sender, EventArgs e)
        {
            FocusManager.EnableHighlighting = false;
            FocusManager.ShowNavigationError = true;
            FocusManager.Register(this);
            FocusManager.SetFocus(btnGenerateDetails);

            // TODO: This line of code loads data into the 'staffsyncDBDataSet1.qryAllEmpLeavePendingStatement' table. You can move, or remove it, as needed.
            //this.qryAllEmpLeavePendingStatementTableAdapter.Fill(this.staffsyncDBDataSet1.qryAllEmpLeavePendingStatement);
            //// TODO: This line of code loads data into the 'staffsyncDBDTSet.EmpMasInfo' table. You can move, or remove it, as needed.
            //this.empMasInfoTableAdapter.Fill(this.staffsyncDBDTSet.EmpMasInfo);
            onCancelButtonClick();
            disableControls();
            clearControls();
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
            AddDownloadTemplateColumn();

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
                dtgImportDataPreview.Columns["ShiftStart"].DefaultCellStyle.Format = "hh:mm:ss";
                dtgImportDataPreview.Columns["ShiftStart"].Width = 200;
                dtgImportDataPreview.Columns["ShiftStart"].ReadOnly = true;
                dtgImportDataPreview.Columns["ShiftEnd"].HeaderText = "Shift End Time";
                dtgImportDataPreview.Columns["ShiftEnd"].DefaultCellStyle.Format = "hh:mm:ss";
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
                MessageBox.Show("Unable to add the template download button." +  Environment.NewLine + Environment.NewLine + ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles a click on the Download Template button for the selected row.
        /// The physical template file name is taken directly from column
        /// "ImpDataInfoTemplateName" and copied from the application's
        /// importtemplate folder to the user's Downloads folder.
        /// </summary>
        private void dtgImportDataSourceList_CellContentClick(
            object sender,
            DataGridViewCellEventArgs e)
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
            try
            {
                if (lblSelectedDataAction.Text == "")
                {
                    MessageBox.Show("Please select an import data source.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string selectedImportSource = GetSelectedImportSourceTitle();

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Select Import File";
                    openFileDialog.Filter =
                        "Supported Files (*.csv;*.xls;*.xlsx)|*.csv;*.xls;*.xlsx|" +
                        "CSV Files (*.csv)|*.csv|" +
                        "Excel 97-2003 (*.xls)|*.xls|" +
                        "Excel Files (*.xlsx)|*.xlsx|" +
                        "All Files (*.*)|*.*";
                    openFileDialog.FilterIndex = 1;
                    openFileDialog.Multiselect = false;
                    openFileDialog.CheckFileExists = true;
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog(this) != DialogResult.OK)
                        return;

                    objSelectedImportFilePath = openFileDialog.FileName;
                    txtSourceFilePath.Text = objSelectedImportFilePath;

                    DataTable importedData = ReadImportFile(objSelectedImportFilePath);

                    if (importedData == null || importedData.Rows.Count == 0)
                    {
                        MessageBox.Show("No data rows were found in the selected file.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearImportPreview();
                        btnSaveDetails.Enabled = false;
                        return;
                    }

                    dtgImportDataPreview.Enabled = true;
                    chkSelectOrUnselect.Enabled = true;
                    chkSelectOrUnselect.Checked = true;
                    btnSaveDetails.Enabled = true;

                    dtgImportDataPreview.DataSource = null;
                    dtgImportDataPreview.DataSource = importedData;

                    BindImportPreviewData(importedData);

                    formatGrid();

                    lblTotalRowsFromSource.Text = "Total Rows from Source : " + importedData.Rows.Count.ToString();

                    UpdateImportSelectedRowCount();

                    MessageBox.Show(importedData.Rows.Count.ToString() + " row(s) found and loaded successfully.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                defaultSelectedImportOption();
                dtgImportDataSourceList[0, 0].Selected = true;
                MessageBox.Show("Selected source template is different or \nunable to read the selected source file.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            onGenerateButtonClick();
            clearControls();
            enableControls();
            errValidator.Clear();
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            int intTotalDuplicateRowsCount = 0;
            int intTotalImportedRowsCount = 0;


            if (lblSelectedDataAction.Text == "")
            {
                MessageBox.Show("Please select an import data source.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (objImportDataPreviewTable == null || objImportDataPreviewTable.Rows.Count == 0)
            {
                MessageBox.Show("No data available to import. Please load the data first.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (lblSelectedDataAction.Text == "Company Information")
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
                    if (!isSelected)
                        continue;
                    //string ClientID = GetImportCellValue(row, "ClientID");
                    //string ClientCode = GetImportCellValue(row, "ClientCode");
                    string ClientName = GetImportCellValue(row, "ClientName");
                    string ClientAddress1 = GetImportCellValue(row, "ClientAddress1");
                    string ClientAddress2 = GetImportCellValue(row, "ClientAddress2");
                    string ClientArea = GetImportCellValue(row, "ClientArea");
                    string ClientCity = GetImportCellValue(row, "ClientCity");
                    string ClientState = GetImportCellValue(row, "ClientState");
                    string ClientPIN = GetImportCellValue(row, "ClientPIN");
                    string ClientCountry = GetImportCellValue(row, "ClientCountry");
                    string ClientPhone = GetImportCellValue(row, "ClientPhone");
                    string ClientMailID = GetImportCellValue(row, "ClientMailID");
                    string ClientContactPerson = GetImportCellValue(row, "ClientContactPerson");
                    string ClientContactNumber = GetImportCellValue(row, "ClientContactNumber");
                    string ClientContactMail = GetImportCellValue(row, "ClientContactMail");
                    string ClientWebSite = GetImportCellValue(row, "ClientWebSite");

                    if (objClientInfo.getClientInfoByTitle(ClientName) != 0)
                    {
                        intTotalDuplicateRowsCount = intTotalDuplicateRowsCount + 1;
                        lblTotalDuplicateRows.Text = "Total Duplicate Rows : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalDuplicateRows.Refresh();
                        lblTotalNotImportedRows.Text = "Total Rows Not Imported : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalNotImportedRows.Refresh();
                        continue;
                    }

                    int intClientID = objClientInfo.InsertClientInfo("", ClientName, ClientAddress1, ClientAddress2, ClientArea, ClientCity, ClientState, ClientPIN, ClientCountry, ClientPhone, ClientMailID, ClientContactPerson, ClientContactNumber, ClientMailID, ClientWebSite, GetImportCellValue(row, "IsActive").ToString() == "1" ? true : false, false, objTempClientFinYearInfo.FinYearID);
                    if (intClientID > 0)
                    {
                        if (picCompLogo.Image != null)
                        {
                            byte[] image_bytes = objImpageOperation.ImageToBytes(picCompLogo.Image, ImageFormat.Jpeg, true);
                            if (image_bytes.Length > 0)
                            {
                                int photoID = objPhotoMas.UpdateCompanyLogoInfo(Convert.ToInt16(intClientID), image_bytes);
                            }
                        }

                        objClientStatutory.InsertClientStatutory(intClientID, DateTime.Now, false, false, "N/A",false, "N/A", false, "N/A", false, "N/A");
                        objClientStatutory.InsertClientProvidentFundSettings(1, "A", Convert.ToDecimal("0.00"), Convert.ToDecimal("0.00"), "A", Convert.ToDecimal("0.00"), Convert.ToDecimal("0.00"), "A", Convert.ToDecimal("0.00"), Convert.ToDecimal("0.00"), DateTime.Now);

                        int BranchID = clsClientBranchInfo.InsertClientBranchInfo("", ClientName, ClientAddress1, ClientAddress2, ClientArea, ClientCity, ClientState, ClientPIN, ClientCountry, ClientPhone, ClientMailID, ClientContactPerson, ClientContactNumber, ClientMailID, ClientWebSite, GetImportCellValue(row, "IsActive").ToString() == "1" ? true : false, false, objTempClientFinYearInfo.ClientID);
                        if (txtCompLogo.Text == "overwrite")
                        {
                            byte[] image_bytes = objImpageOperation.ImageToBytes(picCompLogo.Image, ImageFormat.Jpeg, true);
                            if (image_bytes.Length > 0)
                            {
                                int photoID = objPhotoMas.UpdateCompanyBranchLogoInfo(Convert.ToInt16(intClientID), image_bytes);
                            }
                        }

                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Designation Information")
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
                    if (!isSelected)
                        continue;
                    //string designationID = GetImportCellValue(row, "DesignationID");
                    string designationCode = GetImportCellValue(row, "DesignationCode");
                    string designationTitle = GetImportCellValue(row, "DesignationTitle");
                    string designationInitial = GetImportCellValue(row, "DesignationInitial");

                    if (objDesignation.GetDesignationByTitle(designationTitle) != 0)
                    {
                        intTotalDuplicateRowsCount = intTotalDuplicateRowsCount + 1;
                        lblTotalDuplicateRows.Text = "Total Duplicate Rows : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalDuplicateRows.Refresh();
                        lblTotalNotImportedRows.Text = "Total Rows Not Imported : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalNotImportedRows.Refresh();
                        continue;
                    }

                    int intDesignationID = objDesignation.InsertDesignation(designationCode, designationTitle, designationInitial, GetImportCellValue(row, "IsActive").ToString() == "1" ? true : false, false);
                    if (intDesignationID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Department Information")
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
                    if (!isSelected)
                        continue;

                    //string designationID = GetImportCellValue(row, "DepartmentID");
                    //string designationCode = GetImportCellValue(row, "DepCode");
                    string departmentTitle = GetImportCellValue(row, "DepartmentTitle");
                    string departmentInitial = GetImportCellValue(row, "DepartmentInitial");

                    if (objDepartment.GetDepartmentTitleByTitle(departmentTitle) != 0)
                    {
                        intTotalDuplicateRowsCount = intTotalDuplicateRowsCount + 1;
                        lblTotalDuplicateRows.Text = "Total Duplicate Rows : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalDuplicateRows.Refresh();
                        lblTotalNotImportedRows.Text = "Total Rows Not Imported : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalNotImportedRows.Refresh();
                        continue;
                    }

                    int intDepartmentID = objDepartment.InsertDepartment("", departmentTitle, departmentInitial, GetImportCellValue(row, "IsActive").ToString() == "1" ? true : false, false);
                    if (intDepartmentID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Countries Information")
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
                    if (!isSelected)
                        continue;

                    //string countryID = GetImportCellValue(row, "DepartmentID");
                    //string countryCode = GetImportCellValue(row, "DepCode");
                    string countryTitle = GetImportCellValue(row, "CountryTitle");
                    string countryInitial = GetImportCellValue(row, "CountryInitial");

                    if (objCountries.GetCountryByTitle(countryTitle) != 0)
                    {
                        intTotalDuplicateRowsCount = intTotalDuplicateRowsCount + 1;
                        lblTotalDuplicateRows.Text = "Total Duplicate Rows : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalDuplicateRows.Refresh();
                        lblTotalNotImportedRows.Text = "Total Rows Not Imported : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalNotImportedRows.Refresh();
                        continue;
                    }

                    int intCountryID = objCountries.InsertCountry("", countryTitle, countryInitial, GetImportCellValue(row, "IsActive").ToString() == "1" ? true : false, false);
                    if (intCountryID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "States Information")
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
                    if (!isSelected)
                        continue;

                    //string stateID = GetImportCellValue(row, "StateID");
                    //string stateCode = GetImportCellValue(row, "StateCode");
                    string StateTitle = GetImportCellValue(row, "StateTitle");
                    string StateInitial = GetImportCellValue(row, "StateInitial");

                    if (objStates.GetStateByTitle(StateTitle) != 0)
                    {
                        intTotalDuplicateRowsCount = intTotalDuplicateRowsCount + 1;
                        lblTotalDuplicateRows.Text = "Total Duplicate Rows : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalDuplicateRows.Refresh();
                        lblTotalNotImportedRows.Text = "Total Rows Not Imported : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalNotImportedRows.Refresh();
                        continue;
                    }

                    int intCountryID = objStates.InsertState("", StateTitle, StateInitial, GetImportCellValue(row, "IsActive").ToString() == "1" ? true : false, false);
                    if (intCountryID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Education Information")
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
                    if (!isSelected)
                        continue;

                    //string EduQualID = GetImportCellValue(row, "EduQualID");
                    //string eduQualCode = GetImportCellValue(row, "EduQualCode");
                    string EduQualTitle = GetImportCellValue(row, "EduQualTitle");
                    string EduQualInitial = GetImportCellValue(row, "EduQualInitial");

                    if (objEduQalification.GetEduQualByTitle(EduQualTitle) != 0)
                    {
                        intTotalDuplicateRowsCount = intTotalDuplicateRowsCount + 1;
                        lblTotalDuplicateRows.Text = "Total Duplicate Rows : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalDuplicateRows.Refresh();
                        lblTotalNotImportedRows.Text = "Total Rows Not Imported : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalNotImportedRows.Refresh();
                        continue;
                    }

                    int intEduQualID = objEduQalification.InsertEduQual("", EduQualTitle, EduQualInitial, GetImportCellValue(row, "IsActive").ToString() == "1" ? true : false, false);
                    if (intEduQualID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Skills Information")
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
                    if (!isSelected)
                        continue;

                    //string SkillID = GetImportCellValue(row, "SkillID");
                    //string SkillCode = GetImportCellValue(row, "SkillCode");
                    string SkillTitle = GetImportCellValue(row, "SkillTitle");
                    string SkillInitial = GetImportCellValue(row, "SkillInitial");

                    if (objSkillsMas.GetSkillByTitle(SkillTitle) != 0)
                    {
                        intTotalDuplicateRowsCount = intTotalDuplicateRowsCount + 1;
                        lblTotalDuplicateRows.Text = "Total Duplicate Rows : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalDuplicateRows.Refresh();
                        lblTotalNotImportedRows.Text = "Total Rows Not Imported : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalNotImportedRows.Refresh();
                        continue;
                    }

                    int intSkillsID = objSkillsMas.InsertSkill("", SkillTitle, SkillInitial, GetImportCellValue(row, "IsActive").ToString() == "1" ? true : false, false);
                    if (intSkillsID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Relationship Information")
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
                    if (!isSelected)
                        continue;

                    //string RelationShipID = GetImportCellValue(row, "RelationShipID");
                    //string RelationshipCode = GetImportCellValue(row, "RelationshipCode");
                    string RelationshipTitle = GetImportCellValue(row, "RelationShipTitle");
                    string RelationshipInitial = GetImportCellValue(row, "RelationshipInitial");

                    if (objRelationship.GetRelationshipTitleByTitle(RelationshipTitle) != 0)
                    {
                        intTotalDuplicateRowsCount = intTotalDuplicateRowsCount + 1;
                        lblTotalDuplicateRows.Text = "Total Duplicate Rows : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalDuplicateRows.Refresh();
                        lblTotalNotImportedRows.Text = "Total Rows Not Imported : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalNotImportedRows.Refresh();
                        continue;
                    }

                    int intRelationshipID = objRelationship.InsertRelationship("", RelationshipTitle, RelationshipInitial, GetImportCellValue(row, "IsActive").ToString() == "1" ? true : false, false);
                    if (intRelationshipID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Weekly Off Information")
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
                    if (!isSelected)
                        continue;

                    //string WklyOffMasID = GetImportCellValue(row, "WklyOffMasID");
                    //string WklyOffCode = GetImportCellValue(row, "WklyOffCode");
                    string WklyOffTitle = GetImportCellValue(row, "WklyOffTitle");
                    string WklyOffDay = GetImportCellValue(row, "WklyOffDay");
                    //DateTime WklyOffEffectiveDate = GetImportCellValue(row, "WklyOffEffectiveDate");

                    if (objWeeklyOffInfo.GetWklyOffTitleByTitle(WklyOffTitle) != 0)
                    {
                        intTotalDuplicateRowsCount = intTotalDuplicateRowsCount + 1;
                        lblTotalDuplicateRows.Text = "Total Duplicate Rows : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalDuplicateRows.Refresh();
                        lblTotalNotImportedRows.Text = "Total Rows Not Imported : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalNotImportedRows.Refresh();
                        continue;
                    }

                    int intWeeklyOffInfoID = objWeeklyOffInfo.InsertWeeklyOffInfo("", WklyOffTitle, DateTime.Today, GetImportCellValue(row, "IsActive").ToString() == "1" ? true : false, false);
                    if (intWeeklyOffInfoID > 0)
                    {
                        string[] weekDays = new CultureInfo("en-us").DateTimeFormat.DayNames;

                        List<string> weeklyOffDays = WklyOffDay.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
                        foreach (string dayName in weeklyOffDays)
                        {
                            DayOfWeek day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), dayName, true);
                            int WeeklyOffDayID = objWeeklyOffInfo.InsertWeeklyOffDetailInfo(Convert.ToInt16(intWeeklyOffInfoID), Convert.ToInt32(day) == 0 ? 7 : Convert.ToInt32(day), 0);
                        }                        

                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Asset Category Information")
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
                    if (!isSelected)
                        continue;

                    //string AssetCatMasID = GetImportCellValue(row, "AssetCatMasID");
                    //string AssetCode = GetImportCellValue(row, "AssetCode");
                    string AssetName = GetImportCellValue(row, "AssetName");
                    string AssetDescription = GetImportCellValue(row, "AssetDescription");
                    string ParentCategory = GetImportCellValue(row, "ParentCategory");

                    if (objAssetsCategory.GetAssetsCategoryInfoByName(AssetName) != 0)
                    {
                        intTotalDuplicateRowsCount = intTotalDuplicateRowsCount + 1;
                        lblTotalDuplicateRows.Text = "Total Duplicate Rows : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalDuplicateRows.Refresh();
                        lblTotalNotImportedRows.Text = "Total Rows Not Imported : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalNotImportedRows.Refresh();
                        continue;
                    }

                    int intCategoryID = objAssetsCategory.InsertAssetCategoryInfo("", AssetName, AssetDescription, "", Convert.ToInt32(objAssetsCategory.GetAssetsCategoryInfoByName(ParentCategory).ToString()), GetImportCellValue(row, "IsActive").ToString() == "1" ? true : false, false, objTempClientFinYearInfo.ClientID);
                    if (intCategoryID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Assets Information")
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
                    if (!isSelected)
                        continue;

                    //string AssetID = GetImportCellValue(row, "AssetID");
                    //string AssetCode = GetImportCellValue(row, "AssetCode");
                    string AssetName = GetImportCellValue(row, "AssetName");
                    string AssetDescription = GetImportCellValue(row, "AssetDescription");
                    string ParentCategory = GetImportCellValue(row, "ParentCategory");
                    string TotalQuantity = GetImportCellValue(row, "TotalQuantity");
                    string OutstandingQuantity = GetImportCellValue(row, "OutstandingQuantity");

                    if (objAssetsInfo.GetAssetInfoByName(AssetName) != 0)
                    {
                        intTotalDuplicateRowsCount = intTotalDuplicateRowsCount + 1;
                        lblTotalDuplicateRows.Text = "Total Duplicate Rows : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalDuplicateRows.Refresh();
                        lblTotalNotImportedRows.Text = "Total Rows Not Imported : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalNotImportedRows.Refresh();
                        continue;
                    }

                    int intAssetID = objAssetsInfo.InsertAssetInfo("", AssetName, AssetDescription, GetImportCellValue(row, "IsActive").ToString() == "1" ? true : false, false, Convert.ToInt32(objAssetsCategory.GetAssetsCategoryInfoByName(ParentCategory).ToString()), false, false, false, 1, false, "", 0, 1, Convert.ToDecimal(TotalQuantity.ToString()), Convert.ToDecimal(OutstandingQuantity.ToString()));
                    if (intAssetID > 0)
                    {
                        int AssetRegisterID = objAssetRegister.InsertAssetRegisterInfo(intAssetID, DateTime.Today, 0, Convert.ToDecimal(TotalQuantity), 0, Convert.ToDecimal(OutstandingQuantity), "Cr", "By Opening", 0);
                        int AssetMoreDetailedID = objAssetsInfo.InsertAssetMoreInfo(intAssetID, "N/A", "N/A", "N/A", "N/A", Convert.ToDateTime(DateTime.Today), 0, "N/A", "N/A", "N/A", true, Convert.ToDateTime(DateTime.Today), Convert.ToDateTime(DateTime.Today), Convert.ToDateTime(DateTime.Today), Convert.ToDateTime(DateTime.Today));
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Leave Type Information")
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
                    if (!isSelected)
                        continue;

                    //string LeaveTypeID = GetImportCellValue(row, "LeaveTypeID");
                    //string LeaveCode = GetImportCellValue(row, "LeaveCode");
                    string LeaveTypeTitle = GetImportCellValue(row, "LeaveTypeTitle");
                    string IsPaid = GetImportCellValue(row, "IsPaid");

                    if (objLeaveTypeMas.GetLeaveTypeMasInfoByName(LeaveTypeTitle) != 0)
                    {
                        intTotalDuplicateRowsCount = intTotalDuplicateRowsCount + 1;
                        lblTotalDuplicateRows.Text = "Total Duplicate Rows : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalDuplicateRows.Refresh();
                        lblTotalNotImportedRows.Text = "Total Rows Not Imported : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalNotImportedRows.Refresh();
                        continue;
                    }

                    int intLeaveTypeID = objLeaveTypeMas.InsertLeaveTypeInfo("", LeaveTypeTitle, IsPaid.ToString() == "1" ? true : false, GetImportCellValue(row, "IsActive").ToString() == "1" ? true : false, false);
                    if (intLeaveTypeID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Allowance Information")
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
                    if (!isSelected)
                        continue;

                    //string ReimbID = GetImportCellValue(row, "AllID");
                    //string ReimbCode = GetImportCellValue(row, "AllCode");
                    string AllTitle = GetImportCellValue(row, "AllTitle");
                    string AllDescription = GetImportCellValue(row, "AllDescription");

                    if (objAllowenceInfo.GetAllowenceTitleByTitle(AllTitle) != 0)
                    {
                        intTotalDuplicateRowsCount = intTotalDuplicateRowsCount + 1;
                        lblTotalDuplicateRows.Text = "Total Duplicate Rows : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalDuplicateRows.Refresh();
                        lblTotalNotImportedRows.Text = "Total Rows Not Imported : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalNotImportedRows.Refresh();
                        continue;
                    }

                    int intAllowanceID = objAllowenceInfo.InsertAllowence("", AllTitle, AllDescription, false, GetImportCellValue(row, "IsActive").ToString() == "1" ? true : false, false, 0, false, false);
                    if (intAllowanceID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Deductions Information")
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
                    if (!isSelected)
                        continue;

                    //string DedID = GetImportCellValue(row, "DedID");
                    //string DedCode = GetImportCellValue(row, "DedCode");
                    string DedTitle = GetImportCellValue(row, "DedTitle");
                    string DedDescription = GetImportCellValue(row, "DedDescription");

                    if (objDeductionInfo.GetDeductionTitleByTitle(DedTitle) != 0)
                    {
                        intTotalDuplicateRowsCount = intTotalDuplicateRowsCount + 1;
                        lblTotalDuplicateRows.Text = "Total Duplicate Rows : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalDuplicateRows.Refresh();
                        lblTotalNotImportedRows.Text = "Total Rows Not Imported : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalNotImportedRows.Refresh();
                        continue;
                    }

                    int intDeductionID = objDeductionInfo.InsertDeduction("", DedTitle, DedDescription, false, GetImportCellValue(row, "IsActive").ToString() == "1" ? true : false, false, 0, false, false);
                    if (intDeductionID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Reimbursement Information")
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
                    if (!isSelected)
                        continue;

                    //string ReimbID = GetImportCellValue(row, "ReimbID");
                    //string ReimbCode = GetImportCellValue(row, "ReimbCode");
                    string ReimbTitle = GetImportCellValue(row, "ReimbTitle");
                    string ReimbDescription = GetImportCellValue(row, "ReimbDescription");

                    if (objReimbursementInfo.GetReimbursementTitleByTitle(ReimbTitle) != 0)
                    {
                        intTotalDuplicateRowsCount = intTotalDuplicateRowsCount + 1;
                        lblTotalDuplicateRows.Text = "Total Duplicate Rows : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalDuplicateRows.Refresh();
                        lblTotalNotImportedRows.Text = "Total Rows Not Imported : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalNotImportedRows.Refresh();
                        continue;
                    }

                    int intDeductionID = objReimbursementInfo.InsertReimbursement("", ReimbTitle, ReimbDescription, false, GetImportCellValue(row, "IsActive").ToString() == "1" ? true : false, false, 0, false, false);
                    if (intDeductionID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Advance Type Information")
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
                    if (!isSelected)
                        continue;

                    //string AdvanceTypeID = GetImportCellValue(row, "AdvanceTypeID");
                    //string AdvanceTypeCode = GetImportCellValue(row, "AdvanceTypeCode");
                    string AdvanceTypeTitle = GetImportCellValue(row, "AdvanceTypeTitle");

                    if (objAdvanceTypeMas.GetAdvanceTypeByTitle(AdvanceTypeTitle) != 0)
                    {
                        intTotalDuplicateRowsCount = intTotalDuplicateRowsCount + 1;
                        lblTotalDuplicateRows.Text = "Total Duplicate Rows : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalDuplicateRows.Refresh();
                        lblTotalNotImportedRows.Text = "Total Rows Not Imported : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalNotImportedRows.Refresh();
                        continue;
                    }

                    int intAdvanceTypeID = objAdvanceTypeMas.InsertAdvanceType("", AdvanceTypeTitle, GetImportCellValue(row, "IsActive").ToString() == "1" ? true : false, false, objTempClientFinYearInfo.ClientID);
                    if (intAdvanceTypeID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Public Holiday Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Gender Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Employement Type Information")
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
                    if (!isSelected)
                        continue;

                    //string EmpTypeMasID = GetImportCellValue(row, "EmpTypeMasID");
                    //string EmpTypeCode = GetImportCellValue(row, "EmpTypeCode");
                    string EmpTypeTitle = GetImportCellValue(row, "EmpTypeTitle");
                    string EmpTypeInitial = GetImportCellValue(row, "EmpTypeInitial");

                    if (objEmploymentTypeInfo.GetEmployeeTypeTitleByTitle(EmpTypeTitle) != 0)
                    {
                        intTotalDuplicateRowsCount = intTotalDuplicateRowsCount + 1;
                        lblTotalDuplicateRows.Text = "Total Duplicate Rows : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalDuplicateRows.Refresh();
                        lblTotalNotImportedRows.Text = "Total Rows Not Imported : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalNotImportedRows.Refresh();
                        continue;
                    }

                    int intEmployementTypeID = objEmploymentTypeInfo.InsertEmploymentTypeMasInfo("", EmpTypeTitle, EmpTypeInitial, GetImportCellValue(row, "IsActive").ToString() == "1" ? true : false, false);
                    if (intEmployementTypeID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Shift Information")
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
                    if (!isSelected)
                        continue;

                    //string RelationShipID = GetImportCellValue(row, "ShiftID");
                    //string RelationshipCode = GetImportCellValue(row, "ShiftCode");
                    string ShiftTitle = GetImportCellValue(row, "ShiftTitle");
                    string ShiftInitital = GetImportCellValue(row, "ShiftInitital");
                    DateTime StartTime = DateTime.Parse(GetImportCellValue(row, "ShiftStart"));
                    DateTime EndTime = DateTime.Parse(GetImportCellValue(row, "ShiftEnd"));

                    if (objShiftMas.GetShiftTitleByTitle(ShiftTitle) != 0)
                    {
                        intTotalDuplicateRowsCount = intTotalDuplicateRowsCount + 1;
                        lblTotalDuplicateRows.Text = "Total Duplicate Rows : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalDuplicateRows.Refresh();
                        lblTotalNotImportedRows.Text = "Total Rows Not Imported : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalNotImportedRows.Refresh();
                        continue;
                    }

                    int intShiftInfoID = objShiftMas.InsertShiftMasInfo("", ShiftTitle, ShiftInitital, StartTime, EndTime, GetImportCellValue(row, "IsActive").ToString() == "1" ? true : false, false);
                    if (intShiftInfoID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Bank Information")
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
                    if (!isSelected)
                        continue;

                    //string BankID = GetImportCellValue(row, "BankID");
                    //string BankCode = GetImportCellValue(row, "BankCode");
                    string BankName = GetImportCellValue(row, "BankName");
                    string BankAddress = GetImportCellValue(row, "BankAddress");
                    string IFSCCode = GetImportCellValue(row, "IFSCCode");

                    if (objBankMas.GetBankInfoTitleByTitle(BankName) != 0)
                    {
                        intTotalDuplicateRowsCount = intTotalDuplicateRowsCount + 1;
                        lblTotalDuplicateRows.Text = "Total Duplicate Rows : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalDuplicateRows.Refresh();
                        lblTotalNotImportedRows.Text = "Total Rows Not Imported : " + intTotalDuplicateRowsCount.ToString();
                        lblTotalNotImportedRows.Refresh();
                        continue;
                    }

                    int intBankInfoID = objBankMas.InsertBankMasInfo("", BankName, BankAddress, IFSCCode, GetImportCellValue(row, "IsActive").ToString() == "1" ? true : false, false);
                    if (intBankInfoID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }

            dtgImportDataPreview.Enabled = false;
            btnSaveDetails.Enabled = false;

            MessageBox.Show("Data imported Successfully !!!", "Info");

            //onSaveButtonClick();
            //disableControls();
            //clearControls();
            ////FormatTheGrid();
            //errValidator.Clear();
        }

        public void clearControls()
        {
            lblSelectedDataAction.Text = "";
            lblTotalRowsFromSource.Text = "Total Rows from Source : 0";
            lblTotalRowsSelected.Text = "Total Rows Selected : 0";
            lblTotalImportedRows.Text = "Total Rows Imported : 0";
            lblTotalDuplicateRows.Text = "Total Duplicate Rows : 0";
            lblTotalNotImportedRows.Text = "Total Rows Not Imported : 0";
            //FormatTheGrid();
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

        private void frmImportDataProcess_KeyDown(object sender, KeyEventArgs e)
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

        private void frmImportDataProcess_Activated(object sender, EventArgs e)
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
            lblSelectedDataAction.Text = dtgImportDataSourceList[3, e.RowIndex].Value.ToString();
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

            UpdateImportSelectedRowCount();
        }
    }
}
