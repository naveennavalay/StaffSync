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
using System.IO;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static StaffSync.PDFComponent;
using static StaffSync.PDFComponent.SimplePdfGenerator;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.AccessControl;

namespace StaffSync
{
    public partial class frmImportDataProcess : Form
    {
        DALStaffSync.clsClientInfo objClientInfo = new DALStaffSync.clsClientInfo();
        DALStaffSync.clsImportDataInfo objImportDataInfo = new DALStaffSync.clsImportDataInfo();
        DALStaffSync.clsDesignation objDesignation = new DALStaffSync.clsDesignation();
        DALStaffSync.clsDepartment objDepartment = new DALStaffSync.clsDepartment();
        DALStaffSync.clsCountries objCountries = new DALStaffSync.clsCountries();
        DALStaffSync.clsStates objStates = new DALStaffSync.clsStates();
        DALStaffSync.clsEduQalification objEduQalification = new DALStaffSync.clsEduQalification();
        DALStaffSync.clsSkillsMas objSkillsMas = new DALStaffSync.clsSkillsMas();
        DALStaffSync.clsRelationship objRelationship = new DALStaffSync.clsRelationship();
        DALStaffSync.clsShiftMas objShiftMas = new DALStaffSync.clsShiftMas();
        frmDashboard objDashboard = (frmDashboard)System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        List<ClientInfo> objActiveClientInfo = new List<ClientInfo>();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();

        // Import preview state
        private DataTable objImportDataPreviewTable = new DataTable();
        private string objSelectedImportFilePath = string.Empty;
        private bool isImportPreviewEventsAttached = false;

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
            //attendanceGridControl1.CellValueChangedCustom += Control_CellValueChangedCustom;
        }

        public frmImportDataProcess(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objActiveClientInfo = objClientInfo.getClientInfoByEmpID(objTempCurrentlyLoggedInUserInfo.EmpID);
            dtgImportDataSourceList.DataSource = objImportDataInfo.getImportInfoData();
            formatGrid();
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
        }

        public frmImportDataProcess(int txtEmployeeID, int txtLeaveMasID)
        {
            InitializeComponent();
            dtgImportDataSourceList.DataSource = objImportDataInfo.getImportInfoData();
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
            dtgImportDataSourceList.Columns["ImpDataInfoTitle"].Width = 250;
            dtgImportDataSourceList.Columns["ImpDataInfoDescription"].Visible = false;
            dtgImportDataSourceList.Columns["IsActive"].Visible = false;
            dtgImportDataSourceList.Columns["IsDeleted"].Visible = false;
            dtgImportDataSourceList.Columns["OrderID"].Visible = false;

            if (lblSelectedDataAction.Text == "Import Organisation Information")
            {
                dtgImportDataPreview.Columns["DesignationID"].HeaderText = "Designation ID";
                dtgImportDataPreview.Columns["DesignationID"].Visible = false;
                dtgImportDataPreview.Columns["DesignationCode"].HeaderText = "Designation Code";
                dtgImportDataPreview.Columns["DesignationCode"].Visible = false;
                dtgImportDataPreview.Columns["DesignationCode"].Width = 150;
                dtgImportDataPreview.Columns["DesignationTitle"].HeaderText = "Designation Title";
                dtgImportDataPreview.Columns["DesignationTitle"].Width = 250;
                dtgImportDataPreview.Columns["DesignationTitle"].ReadOnly = true;
                dtgImportDataPreview.Columns["DesignationInitial"].HeaderText = "Designation Initial";
                dtgImportDataPreview.Columns["DesignationInitial"].ReadOnly = true;
                dtgImportDataPreview.Columns["DesignationInitial"].Width = 150;
                dtgImportDataPreview.Columns["IsActive"].HeaderText = "Is Active";
                dtgImportDataPreview.Columns["IsDeleted"].HeaderText = "Is Deleted";                
                dtgImportDataPreview.Columns["IsActive"].Visible = false;
                dtgImportDataPreview.Columns["IsDeleted"].Visible = false;
            }
            else if (lblSelectedDataAction.Text == "Import Designation Information")
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
            else if (lblSelectedDataAction.Text == "Import Department Information")
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
            else if (lblSelectedDataAction.Text == "Import Company Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Countries Information")
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
            else if (lblSelectedDataAction.Text == "Import States Information")
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
            else if (lblSelectedDataAction.Text == "Import Education Information")
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
            else if (lblSelectedDataAction.Text == "Import Skills Information")
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
            else if (lblSelectedDataAction.Text == "Import Relationship Information")
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
            else if (lblSelectedDataAction.Text == "Import Weekly Off Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Asset Category Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Assets Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Leave Type Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Earnings Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Deductions Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Reimbursement Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Advances Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Public Holiday Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Gender Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Employement Type Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Shift Information")
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
            else if (lblSelectedDataAction.Text == "Import Bank Information")
            {

            }
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if(lblSelectedDataAction.Text == "")
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
                        return;
                    }

                    dtgImportDataPreview.Enabled = true;
                    chkSelectOrUnselect.Enabled = true;
                    chkSelectOrUnselect.Checked = true;

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
                MessageBox.Show(
                    "Unable to read the selected import file.\n\n" +
                    ex.Message,
                    "Staffsync",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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

            if (dtgImportDataPreview != null &&
                dtgImportDataPreview.Columns.Contains("IsSelected"))
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

                    if (isSelected)
                        selectedCount++;
                }
            }

            lblTotalRowsSelected.Text =
                "Total Rows Selected : " +
                selectedCount.ToString();
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

            if (lblSelectedDataAction.Text == "Import Organisation Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Designation Information")
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

                    int intDesignationID = objDesignation.InsertDesignation(designationCode, designationTitle, designationInitial, true, false);
                    if(intDesignationID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Import Department Information")
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

                    int intDepartmentID = objDepartment.InsertDepartment("", departmentTitle, departmentInitial, true, false);
                    if (intDepartmentID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Import Company Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Countries Information")
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

                    int intCountryID = objCountries.InsertCountry("", countryTitle, countryInitial, true, false);
                    if (intCountryID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Import States Information")
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

                    int intCountryID = objStates.InsertState("", StateTitle, StateInitial, true, false);
                    if (intCountryID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Import Education Information")
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

                    int intEduQualID = objEduQalification.InsertEduQual("", EduQualTitle, EduQualInitial, true, false);
                    if (intEduQualID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Import Skills Information")
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

                    int intSkillsID = objSkillsMas.InsertSkill("", SkillTitle, SkillInitial, true, false);
                    if (intSkillsID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Import Relationship Information")
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

                    int intRelationshipID = objRelationship.InsertRelationship("", RelationshipTitle, RelationshipInitial, true, false);
                    if (intRelationshipID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Import Weekly Off Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Asset Category Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Assets Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Leave Type Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Earnings Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Deductions Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Reimbursement Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Advances Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Public Holiday Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Gender Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Employement Type Information")
            {

            }
            else if (lblSelectedDataAction.Text == "Import Shift Information")
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

                    int intShiftInfoID = objShiftMas.InsertShiftMasInfo("", ShiftTitle, ShiftInitital, StartTime, EndTime, true, false);
                    if (intShiftInfoID > 0)
                    {
                        intTotalImportedRowsCount = intTotalImportedRowsCount + 1;
                        lblTotalImportedRows.Text = "Total Rows Imported : " + intTotalImportedRowsCount.ToString();
                    }
                }
            }
            else if (lblSelectedDataAction.Text == "Import Bank Information")
            {

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
            lblSelectedDataAction.Text = dtgImportDataSourceList[2, e.RowIndex].Value.ToString();
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
