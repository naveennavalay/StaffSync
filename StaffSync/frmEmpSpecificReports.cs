using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Krypton.Toolkit;
using Microsoft.Office.Interop.Excel;
using ModelStaffSync;
using ReportingEngine;
using ReportingEngine.Core;
using ReportingEngine.Factories;
using ReportingEngine.Helpers;
using ReportingEngine.Layout;
using ReportingEngine.Models;
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
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StaffSync
{
    public partial class frmEmpSpecificReports : Form
    {
        //clsCountries objCountries = new clsCountries();
        //clsDesignation objDesignation = new clsDesignation();
        //clsStates objState = new clsStates();
        //clsRelationship objRelationship = new clsRelationship();

        DALStaffSync.EmployeeRelatedReportQueries objEmployeeRelatedReportQueries = new DALStaffSync.EmployeeRelatedReportQueries();
        DALStaffSync.clsClientInfo objClientInfo = new DALStaffSync.clsClientInfo();

        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        DALStaffSync.clsLeaveTypeMas objLeaveTypeMas = new DALStaffSync.clsLeaveTypeMas();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        DALStaffSync.clsDepartment objDepartment = new DALStaffSync.clsDepartment();
        DALStaffSync.clsDesignation objDesignation = new DALStaffSync.clsDesignation();
        DALStaffSync.clsBloodGroup objBloodGroup = new DALStaffSync.clsBloodGroup();
        DALStaffSync.clsSexMas objSexMaster = new DALStaffSync.clsSexMas();
        DALStaffSync.clsClientBranchInfo objClientBranchInfo = new DALStaffSync.clsClientBranchInfo();
        DALStaffSync.clsPublicHolidayInfo objPublicHolidayInfo = new DALStaffSync.clsPublicHolidayInfo();
        DALStaffSync.clsAttendanceMas objAttendanceMas = new DALStaffSync.clsAttendanceMas();
        DALStaffSync.clsLeaveTypeMas objLeaveTypeInfo = new DALStaffSync.clsLeaveTypeMas();
        DALStaffSync.clsLeaveTRList objLeaveTRReportsList = new DALStaffSync.clsLeaveTRList();
        DALStaffSync.clsEmpLeaveEntitlementInfo objEmpLeaveEntitlementInfo = new DALStaffSync.clsEmpLeaveEntitlementInfo();
        DALStaffSync.clsLeaveTypeMas objLeaveTypeList = new DALStaffSync.clsLeaveTypeMas();

        frmDashboard objDashboard = (frmDashboard) System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();
        DALStaffSync.clsAuditLog objAuditLog = new DALStaffSync.clsAuditLog();
        DALStaffSync.clsAppReports objAppReports = new DALStaffSync.clsAppReports();

        List<ActiveEmployeeListReport> objActiveEmployeeListReport = new List<ActiveEmployeeListReport>();
        List<PersonalInformationListReport> objPersonalInformationListReport = new List<PersonalInformationListReport>();
        List<EmployeeActiveInactiveReport> objEmployeeActiveInactiveReportListReport = new List<EmployeeActiveInactiveReport>();
        List<MonthlyAttendanceReport> objMonthlyAttendanceReport = new List<MonthlyAttendanceReport>();
        List<DailyAttendanceReport> objDailyAttendanceReport = new List<DailyAttendanceReport>();
        List<MonthlyAttendanceSummaryInfo> objMonthlyAttendanceSummaryInfo = new List<MonthlyAttendanceSummaryInfo>();
        List<PublicHolidayInfo> objNonFestivalHolidayList = new List<PublicHolidayInfo>();
        List<PublicHolidayInfo> objFestivalHolidayList = new List<PublicHolidayInfo>();
        List<MonthlyAttendanceSummary> objMonthlyAttendanceSummaryReport = new List<MonthlyAttendanceSummary>();
        List<LeaveTypeInfoModel> objLeaveTypeInfoList = new List<LeaveTypeInfoModel>();
        List<LeaveRegister> objLeaveRegisterReports = new List<LeaveRegister>();
        List<PivotLeaveTrendSummary> objPivotLeaveTrendSummary = new List<PivotLeaveTrendSummary>();
        List<OutstandingLeaveStatement> objOutstandingLeaveStatement = new List<OutstandingLeaveStatement>();
        List<LeaveOutStandingSummary> objLeaveOutStandingSummary = new List<LeaveOutStandingSummary>();
        System.Data.DataTable dtLeaveTrendSummaryDatasource = new System.Data.DataTable();

        string strActionStatement = "";
        private Dictionary<string, object> _originalValues;

        public frmEmpSpecificReports()
        {
            InitializeComponent();
        }

        public frmEmpSpecificReports(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmEmpSpecificReports(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;

            ResetScreen();
            disableControls();
        }


        private void ResetScreen()
        {
            //lblSelectedReport.Text = "";
            //lblSelectedReportName.Text = "";
            //lblFilter.Text = "";

            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
            LoadSalaryMonthList();
            LoadReportsList();

            List<tmpDropdownItem> freeSearchDropdown = new List<tmpDropdownItem>()
            {
                new tmpDropdownItem { MemberValue = "Blank", MemberName = "" },
                new tmpDropdownItem { MemberValue = "EmpMas.EmpCode", MemberName = "Employee Code" },
                new tmpDropdownItem { MemberValue = "EmpMas.EmpName", MemberName = "Employee Name" },
                new tmpDropdownItem { MemberValue = "PersonalInfoMas.ContactNumber1", MemberName = "Contact Number" },
                new tmpDropdownItem { MemberValue = "PersonalInfoMas.ContactNumber2", MemberName = "Mail ID" },
                new tmpDropdownItem { MemberValue = "NomineeMas.NomineePerson", MemberName = "Nominee Name" },
                new tmpDropdownItem { MemberValue = "NomineeMas.ContactNumber", MemberName = "Nominee Contact Number" },
                new tmpDropdownItem { MemberValue = "RelationShipMas.RelationShipTitle", MemberName = "Nominee Relationship" },
                new tmpDropdownItem { MemberValue = "ClientBranchMas.ClientBranchCode", MemberName = "Branch Code" },
                new tmpDropdownItem { MemberValue = "ClientBranchMas.ClientBranchName", MemberName = "Branch Name" },
            };
            cmbFreeSearchAttributeName.DataSource = freeSearchDropdown;
            cmbFreeSearchAttributeName.DisplayMember = "MemberName";
            cmbFreeSearchAttributeName.ValueMember = "MemberValue";
            cmbFreeSearchAttributeName.SelectedIndex = 0;

            List<tmpDropdownItem> ActiveInactiveStatus = new List<tmpDropdownItem>()
            {
                new tmpDropdownItem { MemberValue = "Blank", MemberName = "" },
                new tmpDropdownItem { MemberValue = "vwEmployeeLatestStatus.ActiveInactiveStatus = True", MemberName = "Active" },
                new tmpDropdownItem { MemberValue = "(vwEmployeeLatestStatus.ActiveInactiveStatus) = False", MemberName = "In-active" },
            };
            cmbActiveInactiveStatus.DataSource = ActiveInactiveStatus;
            cmbActiveInactiveStatus.DisplayMember = "MemberName";
            cmbActiveInactiveStatus.ValueMember = "MemberValue";
            cmbActiveInactiveStatus.SelectedIndex = 1;

            cmbCriteriaOperator.Items.Clear();
            cmbCriteriaOperator.Items.Add("");
            cmbCriteriaOperator.Items.Add("equal to");
            cmbCriteriaOperator.Items.Add("not equal to");
            cmbCriteriaOperator.Items.Add("starts with");
            cmbCriteriaOperator.Items.Add("contains");
            cmbCriteriaOperator.Items.Add("ends with");
            cmbCriteriaOperator.SelectedIndex = 1;

            cmbDepartment.DataSource = objDepartment.GetDepartmentList();
            cmbDepartment.DisplayMember = "DepartmentTitle";
            cmbDepartment.ValueMember = "DepartmentID";

            cmbBranch.DataSource = objClientBranchInfo.getBranchInfoList(objTempClientFinYearInfo.ClientID);
            cmbBranch.DisplayMember = "ClientBranchName";
            cmbBranch.ValueMember = "ClientBranchID";

            cmbGender.DataSource = objSexMaster.GetSexList();
            cmbGender.DisplayMember = "SexTitle";
            cmbGender.ValueMember = "SexID";

            cmbBloodGroup.DataSource = objBloodGroup.GetBloodGroupList();
            cmbBloodGroup.DisplayMember = "BloodGroupTitle";
            cmbBloodGroup.ValueMember = "BloodGroupID";

            cmbDesignation.DataSource = objDesignation.GetDesignationList();
            cmbDesignation.DisplayMember = "DesignationTitle";
            cmbDesignation.ValueMember = "DesignationID";

            objLeaveTypeInfoList = objLeaveTypeInfo.GetLeaveTypeInfoList();
            chkLSTLeaveTypeList.Items.Clear();
            foreach (LeaveTypeInfoModel indLeaveTypeInfo in objLeaveTypeInfoList)
            {
                chkLSTLeaveTypeList.Items.Add(indLeaveTypeInfo.LeaveTypeTitle);
            }

            optDOB.Enabled = true;
            optDOB.Checked = false;
            optDOJ.Enabled = true;
            optDOJ.Checked = false;
            optProbDate.Enabled = true;
            optProbDate.Checked = false;
            optConfirmDate.Enabled = true;
            optConfirmDate.Checked = false;
            optDailyAttendance.Enabled = true;
            optDailyAttendance.Checked = false;
            optMonthlyAttendanceRegister.Enabled = true;
            optMonthlyAttendanceRegister.Checked = false;
            chkIncludeBranch.Enabled = false;
        }


        private void LoadReportsList()
        {
            dtgReportsList.DataSource = objAppReports.GetReportsList("");
            dtgReportsList.Columns["ReportsID"].Width = 50;
            dtgReportsList.Columns["ReportsID"].Visible = false;
            dtgReportsList.Columns["ReportsID"].ReadOnly = true;

            dtgReportsList.Columns["ReportsCode"].Width = 70;
            dtgReportsList.Columns["ReportsCode"].HeaderText = "Code";
            dtgReportsList.Columns["ReportsCode"].Visible = true;
            dtgReportsList.Columns["ReportsCode"].ReadOnly = true;

            dtgReportsList.Columns["ReportsName"].Width = 240;
            dtgReportsList.Columns["ReportsName"].HeaderText = "Report Name";
            dtgReportsList.Columns["ReportsName"].Visible = true;
            dtgReportsList.Columns["ReportsName"].ReadOnly = true;

            dtgReportsList.Columns["ReportsDescription"].Visible = false;
            dtgReportsList.Columns["IsActive"].Visible = false;
            dtgReportsList.Columns["IsDeleted"].Visible = false;
            dtgReportsList.Columns["ClientID"].Visible = false;
            dtgReportsList.Columns["OrderID"].Visible = false;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {

        }

        private void frmEmpSpecificReports_Load(object sender, EventArgs e)
        {
            EmployeeMasterDetails("");
        }

        private void btnCloseMe_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbRelationship_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnCloseMe_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenerateDetails_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            
        }

        public void clearControls()
        {
            
        }

        public void enableControls()
        {

        }

        public void disableControls()
        {
            chkIncludeMonth.Checked = false;
            cmbMonth.Enabled = false;
            chkIncludeDesignation.Checked = false;
            cmbDesignation.Enabled = false;
            chkIncludeDepartment.Checked = false;
            cmbDepartment.Enabled = false;
            chkIncludeGender.Checked = false;
            cmbGender.Enabled = false;
            chkIncludeBranch.Checked = false;
            cmbBranch.Enabled = false;
            chkBloodGroup.Checked = false;
            cmbBloodGroup.Enabled = false;
            chkActiveInactiveStatus.Enabled = false;
            chkActiveInactiveStatus.Checked = false;
            cmbActiveInactiveStatus.Enabled = false;

            optDailyAttendance.Enabled = false;
            optMonthlyAttendanceRegister.Enabled = false;

            optDOB.Checked = false;
            optDOJ.Checked = false;
            optProbDate.Checked = false;
            optConfirmDate.Checked = false;
            optRelivingDate.Checked = false;
            optResignationDate.Checked = false;

            txtDTFrom.Text = DateTime.Today.ToString("dd-MM-yyyy");
            txtDTTo.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }

        public void onGenerateButtonClick()
        {

        }

        public void onModifyButtonClick()
        {
           
        }

        public void onRemoveButtonClick()
        {

        }

        public void onSaveButtonClick()
        {

        }

        public void onCancelButtonClick()
        {

        }

        public void displaySelectedValuesOnUI(LeaveTypeInfoModel LeaveTypeInfoModel)
        {

        }

        private void btnModifyDetails_Click(object sender, EventArgs e)
        {

        }

        private void btnRemoveDetails_Click(object sender, EventArgs e)
        {

        }

        private void frmEmpSpecificReports_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                objDashboard.lblDashboardTitle.Text = "Dashboard";
                objDashboard.sptrDashboardContainer.Visible = true;
                this.Close();
            }
        }

        private void frmEmpSpecificReports_Activated(object sender, EventArgs e)
        {
            dtgReportsList.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
        }

        public void LoadSalaryMonthList()
        {
            cmbMonth.Items.Clear();

            List<string> last6Months = new List<string>();
            DateTime currentMonth = DateTime.Now;

            currentMonth = DateTime.Parse("01-01-" + DateTime.Now.Year.ToString());
            for (int i = 0; i < DateTime.Now.Month - 1; i++)
            {
                DateTime month = currentMonth.AddMonths(i);
                cmbMonth.Items.Add(month.ToString("MMM - yyyy"));
            }
            cmbMonth.SelectedIndex = cmbMonth.Items.Count - 1;
        }

        private void dtgReportsList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            lblSelectedReport.Text = "";
            lblSelectedReportName.Text = "";
            lblFilter.Text = "";
            if (dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString() == ReportCode.REP_0001.ToString() || dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString() == ReportCode.REP_0002.ToString())
            {
                chkIncludeMonth.Checked = false;
                cmbMonth.Enabled = false;
                chkIncludeDesignation.Checked = false;
                cmbDesignation.Enabled = false;
                chkIncludeDepartment.Checked = false;
                cmbDepartment.Enabled = false;
                chkIncludeGender.Checked = false;
                cmbGender.Enabled = false;
                chkIncludeBranch.Checked = false;
                cmbBranch.Enabled = false;
                chkActiveInactiveStatus.Enabled = false;
                chkActiveInactiveStatus.Checked = false;
                cmbActiveInactiveStatus.Enabled = false;
                optDailyAttendance.Enabled = false;
                optMonthlyAttendanceRegister.Enabled = false;
                chkLSTLeaveTypeList.Enabled = false;
                cmbFilterLeaveType.Enabled = false;
                cmbFilterLeaveType.DataSource = null;

                optDOB.Checked = false;
                optDOJ.Checked = false;
                optProbDate.Checked = false;
                optConfirmDate.Checked = false;
                optRelivingDate.Checked = false;
                optResignationDate.Checked = false;

                txtDTFrom.Text = DateTime.Today.ToString("dd-MM-yyyy");
                txtDTTo.Text = DateTime.Today.ToString("dd-MM-yyyy");

                lblSelectedReport.Text = dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString();
                lblSelectedReportName.Text = dtgReportsList.SelectedRows[0].Cells["ReportsName"].Value.ToString().Replace("-", "_").ToString();

                List<tmpDropdownItem> lstGroupByValues = new List<tmpDropdownItem>()
                {
                    new tmpDropdownItem { MemberValue = "Blank", MemberName = "" },
                    new tmpDropdownItem { MemberValue = "DesignationTitle", MemberName = "Designation" },
                    new tmpDropdownItem { MemberValue = "DepartmentTitle", MemberName = "Department" },
                    new tmpDropdownItem { MemberValue = "ClientBranchName", MemberName = "Branch Name" },
                    new tmpDropdownItem { MemberValue = "BloodGroupTitle", MemberName = "Blood Group" },
                };
                cmbGroupBy.DataSource = null;
                cmbGroupBy.Items.Clear();
                cmbGroupBy.DataSource = lstGroupByValues;
                cmbGroupBy.DisplayMember = "MemberName";
                cmbGroupBy.ValueMember = "MemberValue";
                cmbGroupBy.SelectedIndex = 2;

                grpLeaveInfo.Enabled = false;
            }
            else if (dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString() == ReportCode.REP_0003.ToString())
            {
                chkIncludeMonth.Checked = false;
                cmbMonth.Enabled = false;
                chkIncludeDesignation.Checked = false;
                cmbDesignation.Enabled = false;
                chkIncludeDepartment.Checked = false;
                cmbDepartment.Enabled = false;
                chkIncludeGender.Checked = false;
                cmbGender.Enabled = false;
                chkIncludeBranch.Checked = false;
                cmbBranch.Enabled = false;
                cmbFilterLeaveType.Enabled = false;
                cmbFilterLeaveType.DataSource = null;

                chkActiveInactiveStatus.Enabled = true;
                chkActiveInactiveStatus.Checked = false;
                cmbActiveInactiveStatus.Enabled = false;
                chkLSTLeaveTypeList.Enabled = false;

                optDailyAttendance.Enabled = false;
                optMonthlyAttendanceRegister.Enabled = false;

                optDOB.Checked = false;
                optDOJ.Checked = false;
                optProbDate.Checked = false;
                optConfirmDate.Checked = false;
                optRelivingDate.Checked = false;
                optResignationDate.Checked = false;

                txtDTFrom.Text = DateTime.Today.ToString("dd-MM-yyyy");
                txtDTTo.Text = DateTime.Today.ToString("dd-MM-yyyy");

                lblSelectedReport.Text = dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString();
                lblSelectedReportName.Text = dtgReportsList.SelectedRows[0].Cells["ReportsName"].Value.ToString().Replace("-", "_").ToString();

                List<tmpDropdownItem> lstGroupByValues = new List<tmpDropdownItem>()
                {
                    new tmpDropdownItem { MemberValue = "Blank", MemberName = "" },
                    new tmpDropdownItem { MemberValue = "DesignationTitle", MemberName = "Designation" },
                    new tmpDropdownItem { MemberValue = "DepartmentTitle", MemberName = "Department" },
                    new tmpDropdownItem { MemberValue = "ClientBranchName", MemberName = "Branch Name" },
                    new tmpDropdownItem { MemberValue = "BloodGroupTitle", MemberName = "Blood Group" },
                };
                cmbGroupBy.DataSource = null;
                cmbGroupBy.Items.Clear();
                cmbGroupBy.DataSource = lstGroupByValues;
                cmbGroupBy.DisplayMember = "MemberName";
                cmbGroupBy.ValueMember = "MemberValue";
                cmbGroupBy.SelectedIndex = 2;

                grpLeaveInfo.Enabled = false;
            }
            else if (dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString() == ReportCode.REP_0004.ToString())
            {
                chkIncludeMonth.Checked = false;
                cmbMonth.Enabled = false;
                chkIncludeDesignation.Checked = false;
                cmbDesignation.Enabled = false;
                chkIncludeDepartment.Checked = false;
                cmbDepartment.Enabled = false;
                chkIncludeGender.Checked = false;
                cmbGender.Enabled = false;
                chkIncludeBranch.Checked = false;
                cmbBranch.Enabled = false;
                chkActiveInactiveStatus.Enabled = false;
                chkActiveInactiveStatus.Checked = false;
                cmbActiveInactiveStatus.Enabled = false;
                chkLSTLeaveTypeList.Enabled = false;
                cmbFilterLeaveType.Enabled = false;
                cmbFilterLeaveType.DataSource = null;

                optDailyAttendance.Enabled = true;
                optMonthlyAttendanceRegister.Enabled = true;

                optDOB.Checked = false;
                optDOJ.Checked = false;
                optProbDate.Checked = false;
                optConfirmDate.Checked = false;
                optRelivingDate.Checked = false;
                optResignationDate.Checked = false;

                txtDTFrom.Text = DateTime.Today.ToString("dd-MM-yyyy");
                txtDTTo.Text = DateTime.Today.ToString("dd-MM-yyyy");

                lblSelectedReport.Text = dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString();
                lblSelectedReportName.Text = dtgReportsList.SelectedRows[0].Cells["ReportsName"].Value.ToString().Replace("-", "_").ToString();

                List<tmpDropdownItem> lstGroupByValues = new List<tmpDropdownItem>()
                {
                    new tmpDropdownItem { MemberValue = "Blank", MemberName = "" },
                    new tmpDropdownItem { MemberValue = "DesignationTitle", MemberName = "Designation" },
                    new tmpDropdownItem { MemberValue = "DepartmentTitle", MemberName = "Department" },
                    new tmpDropdownItem { MemberValue = "ClientBranchName", MemberName = "Branch Name" },
                    new tmpDropdownItem { MemberValue = "BloodGroupTitle", MemberName = "Blood Group" },
                };
                cmbGroupBy.DataSource = null;
                cmbGroupBy.Items.Clear();
                cmbGroupBy.DataSource = lstGroupByValues;
                cmbGroupBy.DisplayMember = "MemberName";
                cmbGroupBy.ValueMember = "MemberValue";
                cmbGroupBy.SelectedIndex = 2;

                grpLeaveInfo.Enabled = false;
            }
            else if (dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString() == ReportCode.REP_0005.ToString())
            {
                chkIncludeMonth.Checked = false;
                cmbMonth.Enabled = false;
                chkIncludeDesignation.Checked = false;
                cmbDesignation.Enabled = false;
                chkIncludeDepartment.Checked = false;
                cmbDepartment.Enabled = false;
                chkIncludeGender.Checked = false;
                cmbGender.Enabled = false;
                chkIncludeBranch.Checked = false;
                cmbBranch.Enabled = false;
                chkActiveInactiveStatus.Enabled = false;
                chkActiveInactiveStatus.Checked = false;
                chkLSTLeaveTypeList.Enabled = false;
                cmbActiveInactiveStatus.Enabled = false;
                chkIncludeGroupSummary.Checked = false;
                chkIncludeGroupSummary.Enabled = false;
                cmbFilterLeaveType.Enabled = false;
                cmbFilterLeaveType.DataSource = null;

                optDOB.Checked = false;
                optDOB.Enabled = false;
                optDOJ.Checked = false;
                optDOJ.Enabled = false;
                optProbDate.Checked = false;
                optProbDate.Enabled = false;
                optConfirmDate.Checked = false;
                optConfirmDate.Enabled = false;
                optRelivingDate.Checked = false;
                optRelivingDate.Enabled = false;
                optResignationDate.Checked = false;
                optResignationDate.Enabled = false;
                optDailyAttendance.Checked = false;
                optDailyAttendance.Enabled = false;
                optMonthlyAttendanceRegister.Enabled = true;
                optMonthlyAttendanceRegister.Checked = true;

                txtDTFrom.Text = DateTime.Today.ToString("dd-MM-yyyy");
                txtDTTo.Text = DateTime.Today.ToString("dd-MM-yyyy");

                lblSelectedReport.Text = dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString();
                lblSelectedReportName.Text = dtgReportsList.SelectedRows[0].Cells["ReportsName"].Value.ToString().Replace("-", "_").ToString();

                List<tmpDropdownItem> lstGroupByValues = new List<tmpDropdownItem>()
                {
                    new tmpDropdownItem { MemberValue = "Blank", MemberName = "" },
                };
                cmbGroupBy.DataSource = null;
                cmbGroupBy.Items.Clear();
                cmbGroupBy.DataSource = lstGroupByValues;
                cmbGroupBy.DisplayMember = "MemberName";
                cmbGroupBy.ValueMember = "MemberValue";
                cmbGroupBy.SelectedIndex = 0;

                grpLeaveInfo.Enabled = false;
            }
            else if (dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString() == ReportCode.REP_0006.ToString())
            {
                chkIncludeMonth.Checked = false;
                cmbMonth.Enabled = false;
                chkIncludeDesignation.Checked = false;
                cmbDesignation.Enabled = false;
                chkIncludeDepartment.Checked = false;
                cmbDepartment.Enabled = false;
                chkIncludeGender.Checked = false;
                cmbGender.Enabled = false;
                chkIncludeBranch.Checked = false;
                cmbBranch.Enabled = false;
                chkActiveInactiveStatus.Enabled = false;
                chkActiveInactiveStatus.Checked = false;
                cmbActiveInactiveStatus.Enabled = false;
                chkIncludeGroupSummary.Checked = false;
                chkIncludeGroupSummary.Enabled = false;
                chkLSTLeaveTypeList.Enabled = true;

                optDOB.Checked = false;
                optDOB.Enabled = false;
                optDOJ.Checked = false;
                optDOJ.Enabled = false;
                optProbDate.Checked = false;
                optProbDate.Enabled = false;
                optConfirmDate.Checked = false;
                optConfirmDate.Enabled = false;
                optRelivingDate.Checked = false;
                optRelivingDate.Enabled = false;
                optResignationDate.Checked = false;
                optResignationDate.Enabled = false;
                optDailyAttendance.Checked = false;
                optDailyAttendance.Enabled = false;
                optMonthlyAttendanceRegister.Enabled = true;
                optMonthlyAttendanceRegister.Checked = true;

                List<tmpDropdownItem> lstLeaveMode = new List<tmpDropdownItem>()
                {
                    new tmpDropdownItem { MemberValue = "Blank", MemberName = "" },
                    new tmpDropdownItem { MemberValue = "FullDay", MemberName = "Full Day" },
                    new tmpDropdownItem { MemberValue = "FirstHalf", MemberName = "First Half" },
                    new tmpDropdownItem { MemberValue = "SecondHalf", MemberName = "Second Half" },
                };
                cmbFilterLeaveMode.DataSource = null;
                cmbFilterLeaveMode.Items.Clear();
                cmbFilterLeaveMode.DataSource = lstLeaveMode;
                cmbFilterLeaveMode.DisplayMember = "MemberName";
                cmbFilterLeaveMode.ValueMember = "MemberValue";
                cmbFilterLeaveMode.SelectedIndex = 0;
                cmbFilterLeaveMode.Enabled = true;

                List<tmpDropdownItem> lstLeaveStatus = new List<tmpDropdownItem>()
                {
                    new tmpDropdownItem { MemberValue = "Blank", MemberName = "" },
                    new tmpDropdownItem { MemberValue = "LeaveStatus", MemberName = "Approved" },
                    new tmpDropdownItem { MemberValue = "LeaveStatus", MemberName = "Pending" },
                    new tmpDropdownItem { MemberValue = "LeaveStatus", MemberName = "Rejected" },
                    new tmpDropdownItem { MemberValue = "LeaveStatus", MemberName = "Cancelled" },
                };
                cmbFilterLeaveType.DataSource = null;
                cmbFilterLeaveType.Items.Clear();
                cmbFilterLeaveType.DataSource = lstLeaveStatus;
                cmbFilterLeaveType.DisplayMember = "MemberName";
                cmbFilterLeaveType.ValueMember = "MemberValue";
                cmbFilterLeaveType.SelectedIndex = 0;
                cmbFilterLeaveType.Enabled = true;

                List<tmpDropdownItem> lstFilterLeaveType = new List<tmpDropdownItem>()
                {
                    new tmpDropdownItem { MemberValue = "Blank", MemberName = "" },
                    new tmpDropdownItem { MemberValue = "LeaveTypeTitle", MemberName = "Leave Type" },
                    new tmpDropdownItem { MemberValue = "LeaveStatus", MemberName = "Leave Status" },
                    new tmpDropdownItem { MemberValue = "LeaveMode", MemberName = "Leave Mode" },
                    new tmpDropdownItem { MemberValue = "DesignationTitle", MemberName = "Designation" },
                    new tmpDropdownItem { MemberValue = "DepartmentTitle", MemberName = "Department" },
                };
                cmbGroupBy.DataSource = null;
                cmbGroupBy.Items.Clear();
                cmbGroupBy.DataSource = lstFilterLeaveType;
                cmbGroupBy.DisplayMember = "MemberName";
                cmbGroupBy.ValueMember = "MemberValue";
                cmbGroupBy.SelectedIndex = 1;
                cmbGroupBy.Enabled = true;
                chkIncludeGroupSummary.Enabled = true;

                grpLeaveInfo.Enabled = true;

                txtDTFrom.Text = Convert.ToDateTime("01-03-2026").ToString("dd-MM-yyyy");//DateTime.Today.ToString("dd-MM-yyyy"); 
                txtDTTo.Text = Convert.ToDateTime("31-03-2026").ToString("dd-MM-yyyy");  //DateTime.Today.ToString("dd-MM-yyyy");

                lblSelectedReport.Text = dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString();
                lblSelectedReportName.Text = dtgReportsList.SelectedRows[0].Cells["ReportsName"].Value.ToString().Replace("-", "_").ToString();
            }
            else if (dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString() == ReportCode.REP_0007.ToString())
            {
                chkIncludeMonth.Checked = false;
                cmbMonth.Enabled = false;
                chkIncludeDesignation.Checked = false;
                cmbDesignation.Enabled = false;
                chkIncludeDepartment.Checked = false;
                cmbDepartment.Enabled = false;
                chkIncludeGender.Checked = false;
                cmbGender.Enabled = false;
                chkIncludeBranch.Checked = false;
                cmbBranch.Enabled = false;
                chkActiveInactiveStatus.Enabled = false;
                chkActiveInactiveStatus.Checked = false;
                chkLSTLeaveTypeList.Enabled = false;
                cmbActiveInactiveStatus.Enabled = false;
                chkIncludeGroupSummary.Checked = false;
                chkIncludeGroupSummary.Enabled = false;
                cmbFilterLeaveType.Enabled = false;
                cmbFilterLeaveType.DataSource = null;

                optDOB.Checked = false;
                optDOB.Enabled = false;
                optDOJ.Checked = false;
                optDOJ.Enabled = false;
                optProbDate.Checked = false;
                optProbDate.Enabled = false;
                optConfirmDate.Checked = false;
                optConfirmDate.Enabled = false;
                optRelivingDate.Checked = false;
                optRelivingDate.Enabled = false;
                optResignationDate.Checked = false;
                optResignationDate.Enabled = false;
                optDailyAttendance.Checked = false;
                optDailyAttendance.Enabled = false;
                optMonthlyAttendanceRegister.Enabled = true;
                optMonthlyAttendanceRegister.Checked = true;

                txtDTFrom.Text = DateTime.Today.ToString("dd-MM-yyyy");
                txtDTTo.Text = DateTime.Today.ToString("dd-MM-yyyy");

                lblSelectedReport.Text = dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString();
                lblSelectedReportName.Text = dtgReportsList.SelectedRows[0].Cells["ReportsName"].Value.ToString().Replace("-", "_").ToString();

                List<tmpDropdownItem> lstGroupByValues = new List<tmpDropdownItem>()
                {
                    new tmpDropdownItem { MemberValue = "Blank", MemberName = "" },
                };
                cmbGroupBy.DataSource = null;
                cmbGroupBy.Items.Clear();
                cmbGroupBy.DataSource = lstGroupByValues;
                cmbGroupBy.DisplayMember = "MemberName";
                cmbGroupBy.ValueMember = "MemberValue";
                cmbGroupBy.SelectedIndex = 0;

                grpLeaveInfo.Enabled = false;
            }
        }

        private void EmployeeMasterDetails(string strFilter)
        {
            objActiveEmployeeListReport = objEmployeeRelatedReportQueries.getActiveEmployeeListReport(objTempClientFinYearInfo.ClientID, strFilter);
            dtgDataResult.DataSource = objActiveEmployeeListReport;

            dtgDataResult.Columns["EmpID"].Width = 50;
            dtgDataResult.Columns["EmpID"].Visible = false;
            dtgDataResult.Columns["EmpID"].ReadOnly = true;

            dtgDataResult.Columns["FinYearFromTo"].Width = 50;
            dtgDataResult.Columns["FinYearFromTo"].Visible = false;
            dtgDataResult.Columns["FinYearFromTo"].ReadOnly = true;

            dtgDataResult.Columns["Status"].Width = 50;
            dtgDataResult.Columns["Status"].Visible = false;
            dtgDataResult.Columns["Status"].ReadOnly = true;

            dtgDataResult.Columns["EmpCode"].Width = 70;
            dtgDataResult.Columns["EmpCode"].HeaderText = "Emp. Code";
            dtgDataResult.Columns["EmpCode"].ReadOnly = true;

            dtgDataResult.Columns["EmpName"].Width = 225;
            dtgDataResult.Columns["EmpName"].HeaderText = "Report Name";
            dtgDataResult.Columns["EmpName"].ReadOnly = true;

            dtgDataResult.Columns["DesignationTitle"].Width = 200;
            dtgDataResult.Columns["DesignationTitle"].HeaderText = "Designation Name";
            dtgDataResult.Columns["DesignationTitle"].ReadOnly = true;

            dtgDataResult.Columns["DepartmentTitle"].Width = 200;
            dtgDataResult.Columns["DepartmentTitle"].HeaderText = "Department Name";
            dtgDataResult.Columns["DepartmentTitle"].ReadOnly = true;

            dtgDataResult.Columns["ContactNumber1"].Width = 125;
            dtgDataResult.Columns["ContactNumber1"].HeaderText = "Contact Number";
            dtgDataResult.Columns["ContactNumber1"].ReadOnly = true;

            dtgDataResult.Columns["ContactNumber2"].Width = 240;
            dtgDataResult.Columns["ContactNumber2"].HeaderText = "Mail ID";
            dtgDataResult.Columns["ContactNumber2"].ReadOnly = true;

            dtgDataResult.Columns["DOJ"].Width = 100;
            dtgDataResult.Columns["DOJ"].HeaderText = "Joining Date";
            dtgDataResult.Columns["DOJ"].ReadOnly = true;

            dtgDataResult.Columns["LastDateOfProbation"].Width = 100;
            dtgDataResult.Columns["LastDateOfProbation"].HeaderText = "Probation Date";
            dtgDataResult.Columns["LastDateOfProbation"].ReadOnly = true;

            dtgDataResult.Columns["DateOfConfirmation"].Width = 100;
            dtgDataResult.Columns["DateOfConfirmation"].HeaderText = "Confirmat Date";
            dtgDataResult.Columns["DateOfConfirmation"].ReadOnly = true;

            dtgDataResult.Columns["SexTitle"].Width = 125;
            dtgDataResult.Columns["SexTitle"].HeaderText = "Gender";
            dtgDataResult.Columns["SexTitle"].ReadOnly = true;

            dtgDataResult.Columns["BloodGroupTitle"].Width = 125;
            dtgDataResult.Columns["BloodGroupTitle"].HeaderText = "Blood Group";
            dtgDataResult.Columns["BloodGroupTitle"].ReadOnly = true;

            dtgDataResult.Columns["NomineePerson"].Width = 225;
            dtgDataResult.Columns["NomineePerson"].HeaderText = "Nominee Name";
            dtgDataResult.Columns["NomineePerson"].ReadOnly = true;

            dtgDataResult.Columns["ContactNumber"].Width = 125;
            dtgDataResult.Columns["ContactNumber"].HeaderText = "Nominee Contact";
            dtgDataResult.Columns["ContactNumber"].ReadOnly = true;

            dtgDataResult.Columns["RelationShipTitle"].Width = 125;
            dtgDataResult.Columns["RelationShipTitle"].HeaderText = "Relationship";
            dtgDataResult.Columns["RelationShipTitle"].ReadOnly = true;

            dtgDataResult.Columns["ClientBranchCode"].Width = 100;
            dtgDataResult.Columns["ClientBranchCode"].HeaderText = "Branch Code";
            dtgDataResult.Columns["ClientBranchCode"].ReadOnly = true;

            dtgDataResult.Columns["ClientBranchName"].Width = 225;
            dtgDataResult.Columns["ClientBranchName"].HeaderText = "Branch Name";
            dtgDataResult.Columns["ClientBranchName"].ReadOnly = true;
        }

        private void EmployeePersonalInformation(string strFilter)
        {
            objPersonalInformationListReport = objEmployeeRelatedReportQueries.getPersonalInformationListReport(objTempClientFinYearInfo.ClientID, strFilter);
            dtgDataResult.DataSource = objPersonalInformationListReport;

            dtgDataResult.Columns["EmpID"].Width = 50;
            dtgDataResult.Columns["EmpID"].Visible = false;
            dtgDataResult.Columns["EmpID"].ReadOnly = true;

            dtgDataResult.Columns["FinYearFromTo"].Width = 50;
            dtgDataResult.Columns["FinYearFromTo"].Visible = false;
            dtgDataResult.Columns["FinYearFromTo"].ReadOnly = true;

            //dtgDataResult.Columns["Status"].Width = 50;
            //dtgDataResult.Columns["Status"].Visible = false;
            //dtgDataResult.Columns["Status"].ReadOnly = true;

            dtgDataResult.Columns["EmpCode"].Width = 70;
            dtgDataResult.Columns["EmpCode"].HeaderText = "Emp. Code";
            dtgDataResult.Columns["EmpCode"].ReadOnly = true;

            dtgDataResult.Columns["EmpName"].Width = 225;
            dtgDataResult.Columns["EmpName"].HeaderText = "Report Name";
            dtgDataResult.Columns["EmpName"].ReadOnly = true;

            dtgDataResult.Columns["DesignationTitle"].Width = 200;
            dtgDataResult.Columns["DesignationTitle"].HeaderText = "Designation Name";
            dtgDataResult.Columns["DesignationTitle"].ReadOnly = true;

            dtgDataResult.Columns["DepartmentTitle"].Width = 200;
            dtgDataResult.Columns["DepartmentTitle"].HeaderText = "Department Name";
            dtgDataResult.Columns["DepartmentTitle"].ReadOnly = true;

            dtgDataResult.Columns["ContactNumber1"].Width = 125;
            dtgDataResult.Columns["ContactNumber1"].HeaderText = "Contact Number";
            dtgDataResult.Columns["ContactNumber1"].ReadOnly = true;

            dtgDataResult.Columns["ContactNumber2"].Width = 240;
            dtgDataResult.Columns["ContactNumber2"].HeaderText = "Mail ID";
            dtgDataResult.Columns["ContactNumber2"].ReadOnly = true;

            dtgDataResult.Columns["SexTitle"].Width = 125;
            dtgDataResult.Columns["SexTitle"].HeaderText = "Blood Group";
            dtgDataResult.Columns["SexTitle"].ReadOnly = true;

            dtgDataResult.Columns["CurrentAddress"].Width = 350;
            dtgDataResult.Columns["CurrentAddress"].HeaderText = "Current Address";
            dtgDataResult.Columns["CurrentAddress"].ReadOnly = true;

            dtgDataResult.Columns["PermanentAddress"].Width = 350;
            dtgDataResult.Columns["PermanentAddress"].HeaderText = "Permanent Address";
            dtgDataResult.Columns["PermanentAddress"].ReadOnly = true;

            dtgDataResult.Columns["ContactPersonInfo"].Width = 350;
            dtgDataResult.Columns["ContactPersonInfo"].HeaderText = "Contact Person Information";
            dtgDataResult.Columns["ContactPersonInfo"].ReadOnly = true;

            dtgDataResult.Columns["NomineeInfo"].Width = 350;
            dtgDataResult.Columns["NomineeInfo"].HeaderText = "Nominee Information";
            dtgDataResult.Columns["NomineeInfo"].ReadOnly = true;
        }

        private void EmployeeActiveInactiveReport(string strFilter)
        {
            objEmployeeActiveInactiveReportListReport = objEmployeeRelatedReportQueries.getEmployeeActiveInactiveReport(objTempClientFinYearInfo.ClientID, strFilter);
            dtgDataResult.DataSource = objEmployeeActiveInactiveReportListReport;

            dtgDataResult.Columns["EmpID"].Width = 50;
            dtgDataResult.Columns["EmpID"].Visible = false;
            dtgDataResult.Columns["EmpID"].ReadOnly = true;

            dtgDataResult.Columns["FinYearFromTo"].Width = 50;
            dtgDataResult.Columns["FinYearFromTo"].Visible = false;
            dtgDataResult.Columns["FinYearFromTo"].ReadOnly = true;

            //dtgDataResult.Columns["Status"].Width = 50;
            //dtgDataResult.Columns["Status"].Visible = false;
            //dtgDataResult.Columns["Status"].ReadOnly = true;

            dtgDataResult.Columns["EmpCode"].Width = 70;
            dtgDataResult.Columns["EmpCode"].HeaderText = "Emp. Code";
            dtgDataResult.Columns["EmpCode"].ReadOnly = true;

            dtgDataResult.Columns["EmpName"].Width = 225;
            dtgDataResult.Columns["EmpName"].HeaderText = "Report Name";
            dtgDataResult.Columns["EmpName"].ReadOnly = true;

            dtgDataResult.Columns["DesignationTitle"].Width = 200;
            dtgDataResult.Columns["DesignationTitle"].HeaderText = "Designation Name";
            dtgDataResult.Columns["DesignationTitle"].ReadOnly = true;

            dtgDataResult.Columns["DepartmentTitle"].Width = 200;
            dtgDataResult.Columns["DepartmentTitle"].HeaderText = "Department Name";
            dtgDataResult.Columns["DepartmentTitle"].ReadOnly = true;

            dtgDataResult.Columns["DOJ"].Width = 120;
            dtgDataResult.Columns["DOJ"].HeaderText = "Joining Date";
            dtgDataResult.Columns["DOJ"].ReadOnly = true;

            dtgDataResult.Columns["LastDateOfProbation"].Width = 120;
            dtgDataResult.Columns["LastDateOfProbation"].HeaderText = "Probation Date";
            dtgDataResult.Columns["LastDateOfProbation"].ReadOnly = true;

            dtgDataResult.Columns["DateOfConfirmation"].Width = 120;
            dtgDataResult.Columns["DateOfConfirmation"].HeaderText = "Confirmation Date";
            dtgDataResult.Columns["DateOfConfirmation"].ReadOnly = true;

            dtgDataResult.Columns["EmpActiveInactiveStatusID"].Width = 120;
            dtgDataResult.Columns["EmpActiveInactiveStatusID"].Visible = false;
            dtgDataResult.Columns["EmpActiveInactiveStatusID"].ReadOnly = true;

            dtgDataResult.Columns["ActiveInactiveStatusDate"].Width = 120;
            dtgDataResult.Columns["ActiveInactiveStatusDate"].HeaderText = "Status Date";
            dtgDataResult.Columns["ActiveInactiveStatusDate"].ReadOnly = true;

            dtgDataResult.Columns["ActiveInactiveStatus"].Width = 120;
            dtgDataResult.Columns["ActiveInactiveStatus"].HeaderText = "Status";
            dtgDataResult.Columns["ActiveInactiveStatus"].ReadOnly = true;

            dtgDataResult.Columns["Comments"].Width = 350;
            dtgDataResult.Columns["Comments"].HeaderText = "Comments";
            dtgDataResult.Columns["Comments"].ReadOnly = true;
        }

        private void EmployeeMonthlyAttendanceRegister(string strFilter)
        {
            objMonthlyAttendanceReport = objEmployeeRelatedReportQueries.getMonthlyAttendanceRegister(objTempClientFinYearInfo.ClientID, Convert.ToDateTime(txtDTFrom.Text), Convert.ToDateTime(txtDTTo.Text));
            dtgDataResult.DataSource = objMonthlyAttendanceReport;

            dtgDataResult.Columns["EmpID"].Width = 50;
            dtgDataResult.Columns["EmpID"].Visible = false;
            dtgDataResult.Columns["EmpID"].ReadOnly = true;

            dtgDataResult.Columns["FinYearFromTo"].Width = 50;
            dtgDataResult.Columns["FinYearFromTo"].Visible = false;
            dtgDataResult.Columns["FinYearFromTo"].ReadOnly = true;

            //dtgDataResult.Columns["Status"].Width = 50;
            //dtgDataResult.Columns["Status"].Visible = false;
            //dtgDataResult.Columns["Status"].ReadOnly = true;

            dtgDataResult.Columns["EmpCode"].Width = 70;
            dtgDataResult.Columns["EmpCode"].HeaderText = "Emp. Code";
            dtgDataResult.Columns["EmpCode"].ReadOnly = true;

            dtgDataResult.Columns["EmpName"].Width = 225;
            dtgDataResult.Columns["EmpName"].HeaderText = "Report Name";
            dtgDataResult.Columns["EmpName"].ReadOnly = true;

            dtgDataResult.Columns["DesignationTitle"].Width = 200;
            dtgDataResult.Columns["DesignationTitle"].HeaderText = "Designation Name";
            dtgDataResult.Columns["DesignationTitle"].ReadOnly = true;

            dtgDataResult.Columns["DepartmentTitle"].Width = 200;
            dtgDataResult.Columns["DepartmentTitle"].HeaderText = "Department Name";
            dtgDataResult.Columns["DepartmentTitle"].ReadOnly = true;

            foreach (DataGridViewColumn col in dtgDataResult.Columns)
            {
                if (col.Name != "SelectRow")
                    col.ReadOnly = true;

                if (col.Index > 5)
                {
                    col.HeaderText = col.HeaderText.Replace("_", "");
                    if (optDailyAttendance.Checked && !optMonthlyAttendanceRegister.Checked)
                    {
                        if (Convert.ToDateTime(txtDTFrom.Text).Day.ToString() != col.HeaderText.ToString())
                        {
                            col.Visible = false;
                        }
                    }
                    else if (!optDailyAttendance.Checked && optMonthlyAttendanceRegister.Checked)
                        col.Visible = true;

                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; //Allowences
                }
            }
        }

        private void EmployeeDailyAttendanceRegister(string strFilter)
        {
            objDailyAttendanceReport = objEmployeeRelatedReportQueries.getDailyAttendanceRegister(objTempClientFinYearInfo.ClientID, Convert.ToDateTime(txtDTFrom.Text));
            dtgDataResult.DataSource = objDailyAttendanceReport;

            dtgDataResult.Columns["EmpID"].Width = 50;
            dtgDataResult.Columns["EmpID"].Visible = false;
            dtgDataResult.Columns["EmpID"].ReadOnly = true;

            dtgDataResult.Columns["FinYearFromTo"].Width = 50;
            dtgDataResult.Columns["FinYearFromTo"].Visible = false;
            dtgDataResult.Columns["FinYearFromTo"].ReadOnly = true;

            //dtgDataResult.Columns["Status"].Width = 50;
            //dtgDataResult.Columns["Status"].Visible = false;
            //dtgDataResult.Columns["Status"].ReadOnly = true;

            dtgDataResult.Columns["EmpCode"].Width = 70;
            dtgDataResult.Columns["EmpCode"].HeaderText = "Emp. Code";
            dtgDataResult.Columns["EmpCode"].ReadOnly = true;

            dtgDataResult.Columns["EmpName"].Width = 225;
            dtgDataResult.Columns["EmpName"].HeaderText = "Report Name";
            dtgDataResult.Columns["EmpName"].ReadOnly = true;

            dtgDataResult.Columns["DesignationTitle"].Width = 200;
            dtgDataResult.Columns["DesignationTitle"].HeaderText = "Designation Name";
            dtgDataResult.Columns["DesignationTitle"].ReadOnly = true;

            dtgDataResult.Columns["DepartmentTitle"].Width = 200;
            dtgDataResult.Columns["DepartmentTitle"].HeaderText = "Department Name";
            dtgDataResult.Columns["DepartmentTitle"].ReadOnly = true;

            dtgDataResult.Columns["AttendanceStatus"].Width = 150;
            dtgDataResult.Columns["AttendanceStatus"].HeaderText = "Attendance Status";
            dtgDataResult.Columns["AttendanceStatus"].ReadOnly = true;
        }

        private void AttendanceSummaryReport(string strFilter)
        {
            objActiveEmployeeListReport = objEmployeeRelatedReportQueries.getActiveEmployeeListReport(objTempClientFinYearInfo.ClientID, strFilter);
            objDailyAttendanceReport = objEmployeeRelatedReportQueries.getDailyAttendanceRegister(objTempClientFinYearInfo.ClientID, Convert.ToDateTime(txtDTFrom.Text));
            objNonFestivalHolidayList = objPublicHolidayInfo.getNonFestivalHolidayList(objTempClientFinYearInfo.ClientID, Convert.ToDateTime(txtDTFrom.Text), Convert.ToDateTime(txtDTTo.Text)); 
            objFestivalHolidayList = objPublicHolidayInfo.getFestivalHolidayList(objTempClientFinYearInfo.ClientID, Convert.ToDateTime(txtDTFrom.Text), Convert.ToDateTime(txtDTTo.Text));
            
            int totalEmployees = objDailyAttendanceReport.Count;

            decimal totalPresent = 0;
            decimal totalLeave = 0;
            decimal totalHalfDay = 0;

            int totalWeekend = objDailyAttendanceReport.Count(x => x.AttendanceStatus == "WE");
            double presentPercent = totalEmployees == 0 ? 0 : (double)totalPresent * 100 / totalEmployees;
            double leavePercent = totalEmployees == 0 ? 0 : (double)totalLeave * 100 / totalEmployees;
            double halfDayPercent = totalEmployees == 0 ? 0 : (double)totalHalfDay * 100 / totalEmployees;

            DateTime month = Convert.ToDateTime(txtDTFrom.Text);

            int totalDays = DateTime.DaysInMonth(month.Year, month.Month);

            int weekEndDays = Enumerable.Range(1, totalDays).Select(day => new DateTime(month.Year, month.Month, day)).Count(d => d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday);
            int workingDays = totalDays - weekEndDays;

            List<PublicHolidayInfo> objPublicHolidayList = objPublicHolidayInfo.getHolidayList(objTempClientFinYearInfo.ClientID, Convert.ToDateTime(txtDTFrom.Text), Convert.ToDateTime(txtDTTo.Text));
            objMonthlyAttendanceSummaryInfo = objAttendanceMas.getMonthlyAttendanceSummaryInfo(objTempClientFinYearInfo.ClientID, Convert.ToDateTime(txtDTFrom.Text), Convert.ToDateTime(txtDTTo.Text));
            if (objMonthlyAttendanceSummaryInfo.Count > 0)
            {
                totalPresent = objMonthlyAttendanceSummaryInfo[0].PresentEmployees;
                totalLeave = objMonthlyAttendanceSummaryInfo[0].LeaveEmployees;
                totalHalfDay = objMonthlyAttendanceSummaryInfo[0].HalfLeaveEmployees;
            }

            objMonthlyAttendanceSummaryReport = new List<MonthlyAttendanceSummary>();
            objMonthlyAttendanceSummaryReport.Add(new MonthlyAttendanceSummary { RowHeader = "Calender Summary", RowValue = "" });
            objMonthlyAttendanceSummaryReport.Add(new MonthlyAttendanceSummary{ RowHeader = "Month", RowValue = month.ToString("MMM") + " " + month.Year });
            objMonthlyAttendanceSummaryReport.Add(new MonthlyAttendanceSummary { RowHeader = "Period", RowValue = Convert.ToDateTime(txtDTFrom.Text).ToString("dd-MMM-yyyy") + " - " + Convert.ToDateTime(txtDTTo.Text).ToString("dd-MMM-yyyy")  });
            objMonthlyAttendanceSummaryReport.Add(new MonthlyAttendanceSummary { RowHeader = "Total Days", RowValue = totalDays.ToString() });
            objMonthlyAttendanceSummaryReport.Add(new MonthlyAttendanceSummary { RowHeader = "Working Days", RowValue = workingDays.ToString() });
            objMonthlyAttendanceSummaryReport.Add(new MonthlyAttendanceSummary { RowHeader = "Weekend Days", RowValue = weekEndDays.ToString() });
            objMonthlyAttendanceSummaryReport.Add(new MonthlyAttendanceSummary { RowHeader = "Holidays", RowValue = objNonFestivalHolidayList.Count.ToString() });
            objMonthlyAttendanceSummaryReport.Add(new MonthlyAttendanceSummary { RowHeader = "Festival Holidays", RowValue = objFestivalHolidayList.Count.ToString() });

            objMonthlyAttendanceSummaryReport.Add(new MonthlyAttendanceSummary { RowHeader = "", RowValue = "" });

            objMonthlyAttendanceSummaryReport.Add(new MonthlyAttendanceSummary { RowHeader = "Employee Summary", RowValue = "" });
            objMonthlyAttendanceSummaryReport.Add(new MonthlyAttendanceSummary { RowHeader = "Total Employees", RowValue = totalEmployees.ToString() });
            objMonthlyAttendanceSummaryReport.Add(new MonthlyAttendanceSummary { RowHeader = "Present Employees", RowValue = totalPresent.ToString() });
            objMonthlyAttendanceSummaryReport.Add(new MonthlyAttendanceSummary { RowHeader = "Leave Employees", RowValue = totalLeave.ToString() });
            objMonthlyAttendanceSummaryReport.Add(new MonthlyAttendanceSummary { RowHeader = "Half-Day Employees", RowValue = totalHalfDay.ToString() });
            objMonthlyAttendanceSummaryReport.Add(new MonthlyAttendanceSummary { RowHeader = "Absent Employees", RowValue = "0" });

            dtgDataResult.DataSource = null;
            dtgDataResult.DataSource = objMonthlyAttendanceSummaryReport;

            dtgDataResult.Columns["RowHeader"].Width = 250;
            dtgDataResult.Columns["RowHeader"].HeaderText = "Header";
            dtgDataResult.Columns["RowHeader"].Visible = true;
            dtgDataResult.Columns["RowHeader"].ReadOnly = true;

            dtgDataResult.Columns["RowValue"].Width = 200;
            dtgDataResult.Columns["RowValue"].HeaderText = "Value";
            dtgDataResult.Columns["RowValue"].ReadOnly = true;
        }

        private void LeaveRegisterInformation(string strFilter)
        {
            objLeaveRegisterReports = objLeaveTRReportsList.getLeaveRegisterInformation(objTempClientFinYearInfo.ClientID, strFilter);
            objOutstandingLeaveStatement = objLeaveTRReportsList.getOutStandingLeaveStaetment(objTempClientFinYearInfo.ClientID);
            objPivotLeaveTrendSummary = objLeaveTRReportsList.getPivotLeaveTrendSummary(objTempClientFinYearInfo.ClientID, Convert.ToDateTime(txtDTFrom.Text), Convert.ToDateTime(txtDTTo.Text));
            //objLeaveOutStandingSummary = objEmpLeaveEntitlementInfo.getConsolidatedLeaveOutStandingStatement(objTempClientFinYearInfo.ClientID);
            //var objData = objSalaryProfile.GetSalaryInfoForBatchProcess(Convert.ToInt32(objTempClientFinYearInfo.ClientID), Convert.ToDateTime(DateTime.Today));

            dtgDataResult.DataSource = null;
            dtgDataResult.DataSource = objLeaveRegisterReports;

            dtgDataResult.Columns["EmpID"].Width = 1;
            dtgDataResult.Columns["EmpID"].ReadOnly = true;
            dtgDataResult.Columns["EmpID"].Visible = false;

            dtgDataResult.Columns["EmpCode"].Width = 100;
            dtgDataResult.Columns["EmpCode"].ReadOnly = true;
            dtgDataResult.Columns["EmpName"].Width = 250;
            dtgDataResult.Columns["EmpName"].ReadOnly = true;
            dtgDataResult.Columns["DesignationTitle"].Width = 200;
            dtgDataResult.Columns["DesignationTitle"].ReadOnly = true;
            dtgDataResult.Columns["DepartmentTitle"].Width = 200;
            dtgDataResult.Columns["DepartmentTitle"].ReadOnly = true;
            dtgDataResult.Columns["LeaveTypeTitle"].Width = 200;
            dtgDataResult.Columns["LeaveTypeTitle"].ReadOnly = true;
            dtgDataResult.Columns["ActualLeaveDateFrom"].Width = 100;
            dtgDataResult.Columns["ActualLeaveDateFrom"].ReadOnly = true;
            dtgDataResult.Columns["ActualLeaveDateFrom"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgDataResult.Columns["ActualLeaveDateTo"].Width = 100;
            dtgDataResult.Columns["ActualLeaveDateTo"].ReadOnly = true;
            dtgDataResult.Columns["ActualLeaveDateTo"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgDataResult.Columns["LeaveDuration"].Width = 300;
            dtgDataResult.Columns["LeaveDuration"].ReadOnly = true;
            dtgDataResult.Columns["LeaveDuration"].Visible = false;
            dtgDataResult.Columns["LeaveMode"].Width = 100;
            dtgDataResult.Columns["LeaveMode"].ReadOnly = true;
            dtgDataResult.Columns["LeaveStatus"].Width = 100;
            dtgDataResult.Columns["LeaveStatus"].ReadOnly = true;
            dtgDataResult.Columns["OrderID"].Width = 100;
            dtgDataResult.Columns["OrderID"].ReadOnly = true;
            dtgDataResult.Columns["OrderID"].Visible = false;

            cmbGroupBy.Enabled = true;
            chkIncludeGroupSummary.Enabled = true;
        }


        private void chkIncludeMonth_CheckedChanged(object sender, EventArgs e)
        {
            cmbMonth.Enabled = chkIncludeMonth.Checked;
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
        }

        private void chkIncludeDesignation_CheckedChanged(object sender, EventArgs e)
        {
            cmbDesignation.Enabled = chkIncludeDesignation.Checked;
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
        }

        private void chkIncludeDepartment_CheckedChanged(object sender, EventArgs e)
        {
            cmbDepartment.Enabled = chkIncludeDepartment.Checked;
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
        }

        private void chkIncludeGender_CheckedChanged(object sender, EventArgs e)
        {
            cmbGender.Enabled = chkIncludeGender.Checked;
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
        }

        private void chkIncludeBranch_CheckedChanged(object sender, EventArgs e)
        {
            cmbBranch.Enabled = chkIncludeBranch.Checked;
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ClientInfo objSelectedClientInfo = new ClientInfo();
            objSelectedClientInfo = objClientInfo.getClientInfoByEmpID(objTempClientFinYearInfo.ClientID).FirstOrDefault();

            ReportBuilder builder = new ReportBuilder();

            CompanyInfo company = new CompanyInfo()
            {
                CompanyName = objSelectedClientInfo.ClientName,
                ProductName = "",

                AddressLine1 = objSelectedClientInfo.ClientAddress1,
                AddressLine2 = objSelectedClientInfo.ClientAddress2,

                City = objSelectedClientInfo.ClientCity,
                State = objSelectedClientInfo.ClientState,
                Country = objSelectedClientInfo.ClientCountry,
                PinCode = objSelectedClientInfo.ClientCountry,

                Phone = objSelectedClientInfo.ClientPhone,
                Mobile = objSelectedClientInfo.ClientPhone,

                Email = objSelectedClientInfo.ClientContactMail,
                Website = objSelectedClientInfo.ClientWebSite,

                GSTNumber = "",
                CINNumber = "",

                LogoPath = System.Windows.Forms.Application.StartupPath + "\\" + objSelectedClientInfo.ClientCode + "-logo.png",
                LogoHeight = 3.5,
                LogoWidth = 3.5
            };

            ReportInfo report = new ReportInfo()
            {
                ReportTitle = lblSelectedReportName.Text,
                GeneratedBy = objTempCurrentlyLoggedInUserInfo.EmpUserName,
                GeneratedOn = DateTime.Now,
                Version = "",
                FinancialYear = ""
            };

            ReportDisplayOptions displayOptions = new ReportDisplayOptions()
            {
                ShowCompanyLogo = true,
                ShowHeader = true,
                ShowFooter = true,
                ShowGeneratedDate = true,
                ShowPageNumbers = true,
                ShowSummary = false,
                ShowWatermark = true,
                WatermarkText = "TRIAL VERSION",
                WatermarkFontSize = 48,
                WatermarkColorHex = "#D0D0D0",
                WatermarkAngle = 45,
                WatermarkOpacity = 0.15
            };

            ReportSettings settings = new ReportSettings
            {
                PageWidth = 60,
                PageHeight = 30,
                LeftMargin = 1,
                RightMargin = 1,
                TopMargin = 1,
                BottomMargin = 1
            };

            string filePath = "";
            filePath = FileHelper.GetTempFolder() + objSelectedClientInfo.ClientCode + "_" + lblSelectedReportName.Text.ToString().Replace(" ", "_") + ".pdf"; // @"C:\Development\StaffSync\StaffSync\bin\Debug\ReportDesigner.pdf";


            //ReportBuilder builder = new ReportBuilder();
            //tmpDropdownItem objtmpDropdownItem1 = (tmpDropdownItem)cmbGroupBy.SelectedItem;

            //builder
            //    .Company(company)
            //    .Title(report)
            //    .Data(objLeaveRegisterReports)
            //    .Settings(settings)
            //    .AddTableRow(
            //        new ReportDynamicTable()
            //            .AddColumn("Employee", 6)
            //            .AddColumn("Salary", 3, ReportColumnAlignment.Right)
            //            .AddColumn("Joining Date", 4, ReportColumnAlignment.Center)
            //            .AddRow("Naveen", 85000, "01-Jan-2025"),
            //        new ReportDynamicTable()
            //            .AddColumn("Department", 5)
            //            .AddColumn("Count", 2)
            //            .AddRow("Testing", 45)
            //            .AddRow("HR", 15),
            //        new ReportDynamicTable()
            //            .AddColumn("Designation", 5)
            //            .AddColumn("Count", 2)
            //            .AddRow("Software Engineer", 45)
            //            .AddRow("Team Lead", 15),
            //        new ReportDynamicTable()
            //            .AddColumn("Employee", 6)
            //            .AddColumn("Salary", 3, ReportColumnAlignment.Right)
            //            .AddColumn("Joining Date", 4, ReportColumnAlignment.Center)
            //            .AddRow("Naveen222222222222", 85000, "01-Jan-2025")
            //    )
            //    .GroupBy(objtmpDropdownItem1.MemberValue, objtmpDropdownItem1.MemberName)
            //    .Generate(filePath);

            //ReportTableRow summaryRow = new ReportTableRow();
            //ReportDynamicTable table =
            //                new ReportDynamicTable()
            //                    .AddColumn("Employee", 6)
            //                    .AddColumn("Salary", 3, ReportColumnAlignment.Right)
            //                    .AddColumn("Joining Date", 4, ReportColumnAlignment.Center)
            //                    .AddRow("Naveen3333333333", 85000, "01-Jan-2025");
            //summaryRow.AddTable(table);
            //builder.AddTableRow(summaryRow);
            //builder.Generate(filePath);

            //ReportTableRow summaryRow = new ReportTableRow();
            //summaryRow.MaxTablesPerRow = 4;
            //summaryRow.AddTable(table);
            //summaryRow.AddTable(dsgSummary);
            //summaryRow.AddTable(deptSummary);
            //summaryRow.AddTable(table);
            //summaryRow.AddTable(dsgSummary);
            //summaryRow.AddTable(objActiveEmployeeListReport);
            //summaryRow.AddTable(DynamicTableFactory.Create(objActiveEmployeeListReport));
            //builder.AddTableRow(summaryRow);
            //builder.AddTableRow(table, deptSummary, dsgSummary, table, deptSummary, dsgSummary, table, deptSummary, dsgSummary);
            //builder.AddTableRow(table, deptSummary, dsgSummary, table, deptSummary, dsgSummary, table, deptSummary, dsgSummary);

            //tmpDropdownItem objtmpDropdownItem1 = (tmpDropdownItem)cmbGroupBy.SelectedItem;
            //builder.GroupBy(objtmpDropdownItem1.MemberValue, objtmpDropdownItem1.MemberName);
            //builder.Generate(filePath);

            //Download.DownloadPDF(filePath);

            //return;

            if (lblSelectedReport.Text.ToString() == ReportCode.REP_0001.ToString())
            {
                if(cmbGroupBy.SelectedIndex == 0)
                {
                    new ReportBuilder()
                    .Company(company)
                    .Title(report)
                    .Data(objActiveEmployeeListReport)
                    .Settings(settings)
                    .Generate(filePath);
                }
                else if(cmbGroupBy.SelectedIndex > 0)
                {
                    tmpDropdownItem objtmpDropdownItem = (tmpDropdownItem)cmbGroupBy.SelectedItem;
                    int totalEmployees = objDailyAttendanceReport.Count;

                    int totalPresent = objDailyAttendanceReport.Count(x => x.AttendanceStatus == "P");

                    int totalLeave = objDailyAttendanceReport.Count(x => x.AttendanceStatus == "L");

                    int totalHalfDay = objDailyAttendanceReport.Count(x => x.AttendanceStatus == "P/L" || x.AttendanceStatus == "L/P");

                    int totalWeekend = objDailyAttendanceReport.Count(x => x.AttendanceStatus == "WE");

                    double presentPercent = totalEmployees == 0 ? 0 : (double)totalPresent * 100 / totalEmployees;

                    double leavePercent = totalEmployees == 0 ? 0 : (double)totalLeave * 100 / totalEmployees;

                    double halfDayPercent = totalEmployees == 0 ? 0 : (double)totalHalfDay * 100 / totalEmployees;

                    if (chkIncludeGroupSummary.Checked)
                    {
                        new ReportBuilder()
                        .Company(company)
                        .Title(report)
                        .Data(objActiveEmployeeListReport)
                        .Settings(settings)
                        .Summary(new List<ReportSummary>()
                                {
                                new ReportSummary
                                {
                                    Caption = "Total Employees",
                                    Value = totalEmployees.ToString()
                                },
                                new ReportSummary
                                {
                                    Caption = "Present Employees",
                                    Value = $"{totalPresent} ({presentPercent:0.00}%)"
                                },
                                new ReportSummary
                                {
                                    Caption = "Employees on Leave",
                                    Value = $"{totalLeave} ({leavePercent:0.00}%)"
                                },
                                new ReportSummary
                                {
                                    Caption = "Half Day Leave",
                                    Value = $"{totalHalfDay} ({halfDayPercent:0.00}%)"
                                },
                                new ReportSummary
                                {
                                    Caption = "Weekend / Holiday",
                                    Value = totalWeekend.ToString()
                                }
                                })
                        .GroupBy(objtmpDropdownItem.MemberValue, objtmpDropdownItem.MemberName)
                        .Generate(filePath);
                    }
                    else if (chkIncludeGroupSummary.Checked == false)
                    {
                        new ReportBuilder()
                        .Company(company)
                        .Title(report)
                        .Data(objActiveEmployeeListReport)
                        .Settings(settings)
                        .GroupBy(objtmpDropdownItem.MemberValue, objtmpDropdownItem.MemberName)
                        .Generate(filePath);
                    }
                }
            }
            else if (lblSelectedReport.Text.ToString() == ReportCode.REP_0002.ToString())
            {
                if (cmbGroupBy.SelectedIndex == 0)
                {
                    new ReportBuilder()
                    .Company(company)
                    .Title(report)
                    .Data(objPersonalInformationListReport)
                    .Settings(settings)
                    .Generate(filePath);
                }
                else if (cmbGroupBy.SelectedIndex > 0)
                {
                    tmpDropdownItem objtmpDropdownItem = (tmpDropdownItem)cmbGroupBy.SelectedItem;
                    int totalEmployees = objDailyAttendanceReport.Count;

                    int totalPresent = objDailyAttendanceReport.Count(x => x.AttendanceStatus == "P");

                    int totalLeave = objDailyAttendanceReport.Count(x => x.AttendanceStatus == "L");

                    int totalHalfDay = objDailyAttendanceReport.Count(x => x.AttendanceStatus == "P/L" || x.AttendanceStatus == "L/P");

                    int totalWeekend = objDailyAttendanceReport.Count(x => x.AttendanceStatus == "WE");

                    double presentPercent = totalEmployees == 0 ? 0 : (double)totalPresent * 100 / totalEmployees;

                    double leavePercent = totalEmployees == 0 ? 0 : (double)totalLeave * 100 / totalEmployees;

                    double halfDayPercent = totalEmployees == 0 ? 0 : (double)totalHalfDay * 100 / totalEmployees;

                    if (chkIncludeGroupSummary.Checked)
                    {
                        new ReportBuilder()
                        .Company(company)
                        .Title(report)
                        .Data(objPersonalInformationListReport)
                        .Settings(settings)
                        .Summary(new List<ReportSummary>()
                                {
                                new ReportSummary
                                {
                                    Caption = "Total Employees",
                                    Value = totalEmployees.ToString()
                                },
                                new ReportSummary
                                {
                                    Caption = "Present Employees",
                                    Value = $"{totalPresent} ({presentPercent:0.00}%)"
                                },
                                new ReportSummary
                                {
                                    Caption = "Employees on Leave",
                                    Value = $"{totalLeave} ({leavePercent:0.00}%)"
                                },
                                new ReportSummary
                                {
                                    Caption = "Half Day Leave",
                                    Value = $"{totalHalfDay} ({halfDayPercent:0.00}%)"
                                },
                                new ReportSummary
                                {
                                    Caption = "Weekend / Holiday",
                                    Value = totalWeekend.ToString()
                                }
                                })
                        .GroupBy(objtmpDropdownItem.MemberValue, objtmpDropdownItem.MemberName)
                        .Generate(filePath);
                    }
                    else if (chkIncludeGroupSummary.Checked == false)
                    {
                        new ReportBuilder()
                        .Company(company)
                        .Title(report)
                        .Data(objPersonalInformationListReport)
                        .Settings(settings)
                        .GroupBy(objtmpDropdownItem.MemberValue, objtmpDropdownItem.MemberName)
                        .Generate(filePath);
                    }
                }
            }
            else if (lblSelectedReport.Text.ToString() == ReportCode.REP_0003.ToString())
            {
                if (cmbGroupBy.SelectedIndex == 0)
                {
                    new ReportBuilder()
                    .Company(company)
                    .Title(report)
                    .Data(objEmployeeActiveInactiveReportListReport)
                    .Settings(settings)
                    .Generate(filePath);
                }
                else if (cmbGroupBy.SelectedIndex > 0)
                {
                    tmpDropdownItem objtmpDropdownItem = (tmpDropdownItem)cmbGroupBy.SelectedItem;
                    int totalEmployees = objDailyAttendanceReport.Count;

                    int totalPresent = objDailyAttendanceReport.Count(x => x.AttendanceStatus == "P");

                    int totalLeave = objDailyAttendanceReport.Count(x => x.AttendanceStatus == "L");

                    int totalHalfDay = objDailyAttendanceReport.Count(x => x.AttendanceStatus == "P/L" || x.AttendanceStatus == "L/P");

                    int totalWeekend = objDailyAttendanceReport.Count(x => x.AttendanceStatus == "WE");

                    double presentPercent = totalEmployees == 0 ? 0 : (double)totalPresent * 100 / totalEmployees;

                    double leavePercent = totalEmployees == 0 ? 0 : (double)totalLeave * 100 / totalEmployees;

                    double halfDayPercent = totalEmployees == 0 ? 0 : (double)totalHalfDay * 100 / totalEmployees;

                    if (chkIncludeGroupSummary.Checked)
                    {
                        new ReportBuilder()
                            .Company(company)
                            .Title(report)
                            .Data(objEmployeeActiveInactiveReportListReport)
                            .Settings(settings)
                            .GroupBy(objtmpDropdownItem.MemberValue, objtmpDropdownItem.MemberName)
                            .Summary(new List<ReportSummary>()
                            {
                            new ReportSummary
                            {
                                Caption = "Total Employees",
                                Value = totalEmployees.ToString()
                            },
                            new ReportSummary
                            {
                                Caption = "Present Employees",
                                Value = $"{totalPresent} ({presentPercent:0.00}%)"
                            },
                            new ReportSummary
                            {
                                Caption = "Employees on Leave",
                                Value = $"{totalLeave} ({leavePercent:0.00}%)"
                            },
                            new ReportSummary
                            {
                                Caption = "Half Day Leave",
                                Value = $"{totalHalfDay} ({halfDayPercent:0.00}%)"
                            },
                            new ReportSummary
                            {
                                Caption = "Weekend / Holiday",
                                Value = totalWeekend.ToString()
                            }
                            })
                            .GroupBy(objtmpDropdownItem.MemberValue, objtmpDropdownItem.MemberName)
                            .Generate(filePath);
                    }
                    else if (chkIncludeGroupSummary.Checked == false)
                    {
                        new ReportBuilder()
                        .Company(company)
                        .Title(report)
                        .Data(objEmployeeActiveInactiveReportListReport)
                        .Settings(settings)
                        .GroupBy(objtmpDropdownItem.MemberValue, objtmpDropdownItem.MemberName)
                        .Generate(filePath);
                    }
                }
            }
            else if (lblSelectedReport.Text.ToString() == ReportCode.REP_0004.ToString())
            {
                if (optDailyAttendance.Checked && !optMonthlyAttendanceRegister.Checked)
                {
                    if(cmbGroupBy.SelectedIndex == 0)
                    {
                        new ReportBuilder()
                            .Company(company)
                            .Title(report)
                            .Data(objDailyAttendanceReport)
                            .Settings(settings)
                            .Generate(filePath);
                    }
                    else if(cmbGroupBy.SelectedIndex > 0)
                    {
                        if(chkIncludeGroupSummary.Checked)
                        {
                            int totalEmployees = objDailyAttendanceReport.Count;

                            int totalPresent = objDailyAttendanceReport.Count(x => x.AttendanceStatus == "P");

                            int totalLeave = objDailyAttendanceReport.Count(x => x.AttendanceStatus == "L");

                            int totalHalfDay = objDailyAttendanceReport.Count(x => x.AttendanceStatus == "P/L" || x.AttendanceStatus == "L/P");

                            int totalWeekend = objDailyAttendanceReport.Count(x => x.AttendanceStatus == "WE");

                            double presentPercent = totalEmployees == 0 ? 0 : (double)totalPresent * 100 / totalEmployees;

                            double leavePercent = totalEmployees == 0 ? 0 : (double)totalLeave * 100 / totalEmployees;

                            double halfDayPercent = totalEmployees == 0 ? 0 : (double)totalHalfDay * 100 / totalEmployees;

                            report.ReportTitle = "Daily " + report.ReportTitle + " : " + Convert.ToDateTime(txtDTFrom.Text).ToString("dd-MMM-yyyy");

                            new ReportBuilder()
                                .Company(company)
                                .Title(report)
                                .Data(objDailyAttendanceReport)
                                .Settings(settings)
                                .Summary(new List<ReportSummary>()
                                {
                                    new ReportSummary
                                    {
                                        Caption = "Total Employees",
                                        Value = totalEmployees.ToString()
                                    },
                                    new ReportSummary
                                    {
                                        Caption = "Present Employees",
                                        Value = $"{totalPresent} ({presentPercent:0.00}%)"
                                    },
                                    new ReportSummary
                                    {
                                        Caption = "Employees on Leave",
                                        Value = $"{totalLeave} ({leavePercent:0.00}%)"
                                    },
                                    new ReportSummary
                                    {
                                        Caption = "Half Day Leave",
                                        Value = $"{totalHalfDay} ({halfDayPercent:0.00}%)"
                                    },
                                    new ReportSummary
                                    {
                                        Caption = "Weekend / Holiday",
                                        Value = totalWeekend.ToString()
                                    }
                                })
                                .Generate(filePath);
                        }
                        else
                        {
                            new ReportBuilder()
                            .Company(company)
                            .Title(report)
                            .Data(objDailyAttendanceReport)
                            .Settings(settings)
                            .Generate(filePath);
                        }
                    }
                }
                else if (!optDailyAttendance.Checked && optMonthlyAttendanceRegister.Checked)
                {
                    report.ReportTitle = report.ReportTitle + "\n(" + Convert.ToDateTime(txtDTFrom.Text).ToString("dd-MMM-yyyy") + " - " + Convert.ToDateTime(txtDTTo.Text).ToString("dd-MMM-yyyy") + ")";

                    int totalEmployees = objMonthlyAttendanceReport.Count;
                    int totalPresentDays = objMonthlyAttendanceReport.Sum(x => x.PresentCount);
                    int totalLeaveDays = objMonthlyAttendanceReport.Sum(x => x.LeaveCount);
                    int totalHalfLeaveDays = objMonthlyAttendanceReport.Sum(x => x.HalfLeaveCount);

                    DateTime month = Convert.ToDateTime(txtDTFrom.Text);

                    int totalDays = DateTime.DaysInMonth(month.Year, month.Month);

                    int weekEndDays = Enumerable.Range(1, totalDays).Select(day => new DateTime(month.Year, month.Month, day)).Count(d => d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday);
                    int workingDays = totalDays - weekEndDays;

                    double effectivePresentDays = totalPresentDays + (totalHalfLeaveDays * 0.5);
                    double effectiveLeaveDays = totalLeaveDays + (totalHalfLeaveDays * 0.5);

                    int totalPossibleAttendance = totalEmployees * workingDays;

                    double attendancePercentage = totalPossibleAttendance == 0 ? 0 : (effectivePresentDays * 100.0) / totalPossibleAttendance;

                    double absenteePercentage = totalPossibleAttendance == 0 ? 0 : (effectiveLeaveDays * 100.0) / totalPossibleAttendance;

                    new ReportBuilder()
                        .Company(company)
                        .Title(report)
                        .Data(objMonthlyAttendanceReport)
                        .Settings(settings)
                        .GroupBy("DepartmentTitle")
                        .Summary(new List<ReportSummary>()
                        {
                            new ReportSummary("Total Days", totalDays.ToString()),
                            new ReportSummary("Week End Days", weekEndDays.ToString()),
                            new ReportSummary("Working Days", workingDays.ToString()),
                            new ReportSummary("Total Employees", totalEmployees.ToString()),
                            new ReportSummary("Present Days", totalPresentDays.ToString()),
                            new ReportSummary("Leave Days", totalLeaveDays.ToString()),
                            new ReportSummary("Half Leave Days", totalHalfLeaveDays.ToString()),
                            new ReportSummary("Attendance %", attendancePercentage.ToString("0.00") + "%"),
                            new ReportSummary("Absenteeism %", absenteePercentage.ToString("0.00") + "%")
                        })
                        .Generate(filePath);
                }

                //ReportBuilder builder = new ReportBuilder();
                //builder
                //    .Company(company)
                //    .Title(report);

                //if (optDailyAttendance.Checked && !optMonthlyAttendanceRegister.Checked)
                //{
                //    var item = objMonthlyAttendanceReport.FirstOrDefault();

                //    var summary = new List<ReportSummary>
                //    {
                //        new ReportSummary("Present Days", item.PresentCount.ToString()),
                //        new ReportSummary("Leave Days", item.LeaveCount.ToString()),
                //        new ReportSummary("Half Leave Days", item.HalfLeaveCount.ToString())
                //    };

                //    for (int i = 1; i <= 31; i++)
                //    {
                //        if (Convert.ToDateTime(txtDTFrom.Text).Day.ToString() != i.ToString())
                //            builder.SetColumnVisibility("_" + i, false);
                //    }
                //    builder.SetColumnVisibility("PresentCount", false);
                //    builder.SetColumnVisibility("LeaveCount", false);
                //    builder.SetColumnVisibility("HalfLeaveCount", false);
                //    builder.Summary(summary);

                //}
                //else if (!optDailyAttendance.Checked && optMonthlyAttendanceRegister.Checked)
                //{
                //    for (int i = 1; i <= 31; i++)
                //    {
                //        builder.SetColumnVisibility("_" + i, true);
                //    }
                //}
                //builder
                //    .Data(objMonthlyAttendanceReport)
                //    .Settings(settings)
                //    .Generate(filePath);
            }
            else if (lblSelectedReport.Text.ToString() == ReportCode.REP_0005.ToString())
            {
                if (optMonthlyAttendanceRegister.Checked)
                {
                    report.ReportTitle = "Monthly " + report.ReportTitle;

                    if (cmbGroupBy.SelectedIndex == 0)
                    {
                        new ReportBuilder()
                            .Company(company)
                            .Title(report)
                            .Data(objMonthlyAttendanceSummaryReport)
                            .Settings(settings)
                            .Generate(filePath);
                    }
                    else if (cmbGroupBy.SelectedIndex > 0)
                    {
                        if (chkIncludeGroupSummary.Checked)
                        {
                            new ReportBuilder()
                                .Company(company)
                                .Title(report)
                                .Data(objMonthlyAttendanceSummaryReport)
                                .Settings(settings)
                                .Generate(filePath);
                        }
                        else
                        {
                            new ReportBuilder()
                                .Company(company)
                                .Title(report)
                                .Data(objMonthlyAttendanceSummaryReport)
                                .Settings(settings)
                                .Generate(filePath);
                        }
                    }
                }
            }
            else if (lblSelectedReport.Text.ToString() == ReportCode.REP_0006.ToString())
            {
                //ReportDynamicTable tbl = new ReportDynamicTable();
                //tbl.Title = "Leave Matrix";
                //tbl.SpaceBefore = 0.05;
                //tbl.SpaceAfter = 0.05;
                //tbl.AddColumn("Code", 3, ReportColumnAlignment.Left, true);
                //tbl.AddColumn("Name", 4, ReportColumnAlignment.Left, true);
                //tbl.AddColumn("Designation", 4, ReportColumnAlignment.Left, true);
                //tbl.AddColumn("Department", 4, ReportColumnAlignment.Left, true);

                ////foreach (LeaveTypeInfoModel indLeaveTypeInfo in objLeaveTypeInfoList)
                ////{
                ////    tbl.AddColumn(indLeaveTypeInfo.LeaveCode, 3, ReportColumnAlignment.Left, true);
                ////}

                //foreach (DataRow row in dt.Rows)
                //{
                //    List<object> values = new List<object>();

                //    foreach (DataColumn column in dt.Columns)
                //    {
                //        values.Add(row[column]);
                //    }

                //    tbl.AddRow(values.ToArray());
                //}
                //return;

                //System.Data.DataTable dt = objLeaveTRReportsList.getLeaveMatrixInformation(objTempClientFinYearInfo.ClientID, Convert.ToDateTime(txtDTFrom.Text), Convert.ToDateTime(txtDTTo.Text));
                //ReportDynamicTable tbl1 = ReportTableFactory.FromDataTable(dt, "Leave Matrix");
                //tbl1.Columns[0].Visible = false;
                //tbl1.Columns[5].Visible = false;
                //tbl1.Title = "Leave Matrix";
                //tbl1.SpaceBefore = 1;
                //tbl1.SpaceAfter = 1;

                //builder
                //    .Company(company)
                //    .Title(report)
                //    //.Data(objLeaveRegisterReports)
                //    .Settings(settings);

                //builder.AddTableRow(tbl1);
                //builder.Generate(filePath);
                //Download.DownloadPDF(filePath);


                ReportTableRow objLeaveSummaryInfo = new ReportTableRow();
                ReportTableRow objPivotLeaveInfo = new ReportTableRow();
                ReportTableRow objOutStandingLeaveInfo = new ReportTableRow();
                ReportTableRow objLeaveMatrix = new ReportTableRow();

                if (optMonthlyAttendanceRegister.Checked)
                {
                    report.ReportTitle = report.ReportTitle + "\n(" + Convert.ToDateTime(txtDTFrom.Text).ToString("dd-MMM-yyyy") + " - " + Convert.ToDateTime(txtDTTo.Text).ToString("dd-MMM-yyyy") + ")";

                    if (cmbGroupBy.SelectedIndex == 0)
                    {
                        if (chkIndividualOrGroupedReport.Checked)
                        {
                            if (chkLeaveSummary.Checked)
                            {
                                report.ReportTitle = "Leave Summary";
                                //ReportTableRow objPivotLeaveInfo = new ReportTableRow();
                                objPivotLeaveInfo.Caption = "Leave Summary";
                                objPivotLeaveInfo.SpaceBefore = 0.05;
                                objPivotLeaveInfo.SpaceAfter = 0.05;
                                builder
                                    .Company(company)
                                    .Title(report)
                                    .Data(objPivotLeaveTrendSummary)
                                    .Settings(settings)
                                    .Generate(filePath);
                            }
                            if (chkLeaveBalance.Checked)
                            {
                                report.ReportTitle = "Leave Balance Report";
                                objOutStandingLeaveInfo.Caption = "Leave Balance Report";
                                objOutStandingLeaveInfo.SpaceBefore = 0.05;
                                objOutStandingLeaveInfo.SpaceAfter = 0.05;
                                builder
                                    .Company(company)
                                    .Title(report)
                                    .Data(objOutstandingLeaveStatement)
                                    .Settings(settings)
                                    .Generate(filePath);
                            }
                            if (chkLeaveMatrix.Checked)
                            {
                                report.ReportTitle = "Leave Matrix";
                                System.Data.DataTable dt = objLeaveTRReportsList.getLeaveMatrixInformation(objTempClientFinYearInfo.ClientID, Convert.ToDateTime(txtDTFrom.Text), Convert.ToDateTime(txtDTTo.Text));
                                ReportDynamicTable tbl1 = ReportTableFactory.FromDataTable(dt, "Leave Matrix");
                                tbl1.Columns[0].Visible = false;
                                tbl1.Columns[1].Width = 5;
                                tbl1.Columns[2].Width = 5;
                                tbl1.Columns[3].Width = 5;
                                tbl1.Columns[5].Visible = false;
                                //tbl1.Title = "Leave Matrix";
                                tbl1.SpaceBefore = 1;
                                tbl1.SpaceAfter = 1;

                                builder
                                    .Company(company)
                                    .Title(report)
                                    //.Data(objLeaveRegisterReports)
                                    .Settings(settings);
                                builder.AddTableRow(tbl1);
                                builder.Generate(filePath);
                            }
                            if (chkLeaveSummary.Checked == false && chkLeaveBalance.Checked == false && chkLeaveLedger.Checked == false && chkLeaveMatrix.Checked == false)
                            {
                                builder
                                    .Company(company)
                                    .Title(report)
                                    .Data(objLeaveRegisterReports)
                                    .Settings(settings)
                                    .Generate(filePath);
                            }
                        }
                        else if (!chkIndividualOrGroupedReport.Checked)
                        {
                            builder
                                .Company(company)
                                .Title(report)
                                .Data(objLeaveRegisterReports)
                                .Settings(settings);

                            if (chkLeaveSummary.Checked)
                            {
                                objPivotLeaveInfo.Caption = "Leave Summary";
                                objPivotLeaveInfo.SpaceBefore = 0.05;
                                objPivotLeaveInfo.SpaceAfter = 0.05;
                                objPivotLeaveInfo.AddTable(objPivotLeaveTrendSummary);
                                builder.AddTableRow(objPivotLeaveInfo);
                            }
                            if (chkLeaveBalance.Checked)
                            {
                                objOutStandingLeaveInfo.Caption = "Leave Outstanding Summary";
                                objOutStandingLeaveInfo.SpaceBefore = 0.05;
                                objOutStandingLeaveInfo.SpaceAfter = 0.05;
                                objOutStandingLeaveInfo.AddTable(objOutstandingLeaveStatement);
                                builder.AddTableRow(objOutStandingLeaveInfo);
                            }
                            if(chkLeaveMatrix.Checked)
                            {
                                System.Data.DataTable dt = objLeaveTRReportsList.getLeaveMatrixInformation(objTempClientFinYearInfo.ClientID, Convert.ToDateTime(txtDTFrom.Text), Convert.ToDateTime(txtDTTo.Text));
                                ReportDynamicTable tbl1 = ReportTableFactory.FromDataTable(dt, "Leave Matrix");
                                tbl1.Columns[0].Visible = false;
                                tbl1.Columns[1].Width = 5;
                                tbl1.Columns[2].Width = 5;
                                tbl1.Columns[3].Width = 5;
                                tbl1.Columns[5].Visible = false;
                                tbl1.Title = "Leave Matrix";
                                tbl1.SpaceBefore = 1;
                                tbl1.SpaceAfter = 1;

                                builder.AddTableRow(tbl1);
                            }
                            if (chkLeaveSummary.Checked == false && chkLeaveBalance.Checked == false && chkLeaveLedger.Checked == false && chkLeaveMatrix.Checked == false)
                            {
                                builder
                                    .Company(company)
                                    .Title(report)
                                    .Data(objLeaveRegisterReports)
                                    .Settings(settings)
                                    .Generate(filePath);
                            }
                            else
                            {
                                builder.Generate(filePath);
                            }
                        }
                    }
                    else if (cmbGroupBy.SelectedIndex > 0)
                    {
                        tmpDropdownItem objtmpDropdownItem = (tmpDropdownItem)cmbGroupBy.SelectedItem;
                        List<ReportSummary> objLeaveStatus = new List<ReportSummary>();
                        List<ReportSummary> objLeaveType = new List<ReportSummary>();
                        List<ReportSummary> objLeaveMode = new List<ReportSummary>();
                        List<ReportSummary> objDesignation = new List<ReportSummary>();
                        List<ReportSummary> objDepartment = new List<ReportSummary>();

                        if (chkIncludeGroupSummary.Checked)
                        {
                            string groupField = objtmpDropdownItem.MemberValue;
                            //if (groupField == "LeaveStatus")
                            {
                                string[] values =
                                {
                                    "Approved",
                                    "Rejected",
                                    "Pending",
                                    "Cancelled"
                                };

                                foreach (string value in values)
                                {
                                    objLeaveStatus.Add(new ReportSummary(value, objLeaveRegisterReports.Count(x => x.LeaveStatus == value).ToString()));
                                }
                            }
                            //else if (groupField == "LeaveTypeTitle")
                            {
                                var values = objLeaveRegisterReports.GroupBy(x => x.LeaveTypeTitle).OrderBy(x => x.Key);

                                foreach (var item in values)
                                {
                                    objLeaveType.Add(new ReportSummary(item.Key, item.Count().ToString()));
                                }
                            }
                            //else if (groupField == "LeaveMode")
                            {
                                var values = objLeaveRegisterReports.GroupBy(x => x.LeaveMode).OrderBy(x => x.Key);

                                foreach (var item in values)
                                {
                                    objLeaveMode.Add(new ReportSummary(item.Key, item.Count().ToString()));
                                }
                            }
                            //else if (groupField == "DepartmentTitle")
                            {
                                var values = objLeaveRegisterReports.GroupBy(x => x.DepartmentTitle).OrderBy(x => x.Key);

                                foreach (var item in values)
                                {
                                    objDepartment.Add(new ReportSummary(item.Key, item.Count().ToString()));
                                }
                            }
                            //else if (groupField == "DesignationTitle")
                            {
                                var values = objLeaveRegisterReports.GroupBy(x => x.DesignationTitle).OrderBy(x => x.Key);

                                foreach (var item in values)
                                {
                                    objDesignation.Add(new ReportSummary(item.Key, item.Count().ToString()));
                                }
                            }

                            builder
                                .Company(company)
                                .Title(report)
                                .Data(objLeaveRegisterReports)
                                .Settings(settings);

                            if (chkLeaveSummary.Checked)
                            {
                                objPivotLeaveInfo.Caption = "Leave Summary";
                                objPivotLeaveInfo.SpaceBefore = 0.05;
                                objPivotLeaveInfo.SpaceAfter = 0.05;
                                objPivotLeaveInfo.AddTable(objPivotLeaveTrendSummary);
                                builder.AddTableRow(objPivotLeaveInfo);
                            }
                            if (chkLeaveBalance.Checked)
                            {
                                objOutStandingLeaveInfo.Caption = "Leave Outstanding Summary";
                                objOutStandingLeaveInfo.SpaceBefore = 0.05;
                                objOutStandingLeaveInfo.SpaceAfter = 0.05;
                                objOutStandingLeaveInfo.AddTable(objOutstandingLeaveStatement);
                                builder.AddTableRow(objOutStandingLeaveInfo);
                            }
                            if (chkLeaveMatrix.Checked)
                            {
                                System.Data.DataTable dt = objLeaveTRReportsList.getLeaveMatrixInformation(objTempClientFinYearInfo.ClientID, Convert.ToDateTime(txtDTFrom.Text), Convert.ToDateTime(txtDTTo.Text));
                                ReportDynamicTable tbl1 = ReportTableFactory.FromDataTable(dt, "Leave Matrix");
                                tbl1.Columns[0].Visible = false;
                                tbl1.Columns[1].Width = 5;
                                tbl1.Columns[2].Width = 5;
                                tbl1.Columns[3].Width = 5;
                                tbl1.Columns[5].Visible = false;
                                tbl1.Title = "Leave Matrix";
                                tbl1.SpaceBefore = 1;
                                tbl1.SpaceAfter = 1;

                                builder.AddTableRow(tbl1);
                            }

                            builder.AddTableRow
                            (
                                ReportTableFactory.FromList(objLeaveStatus, "Leave Status"), ReportTableFactory.FromList(objLeaveMode, "Leave Mode"),
                                ReportTableFactory.FromList(objLeaveType, "Leave Type"),
                                ReportTableFactory.FromList(objDesignation, "Designation"),
                                ReportTableFactory.FromList(objDepartment, "Department")
                            )
                            .GroupBy(objtmpDropdownItem.MemberValue, objtmpDropdownItem.MemberName)
                            .Generate(filePath);

                            //new ReportBuilder()
                            //    .Company(company)
                            //    .Title(report)
                            //    .Data(objLeaveRegisterReports)
                            //    .Settings(settings)
                            //    //.Summary(summaries)
                            //    //.AddTable(summaryTable)
                            //    .AddTableRow
                            //    (
                            //        ReportTableFactory.FromList(objLeaveStatus, "Leave Status"), ReportTableFactory.FromList(objLeaveMode, "Leave Mode"), 
                            //        ReportTableFactory.FromList(objLeaveType, "Leave Type"),
                            //        ReportTableFactory.FromList(objDesignation, "Designation"),
                            //        ReportTableFactory.FromList(objDepartment, "Department")
                            //    )
                            //    .GroupBy(objtmpDropdownItem.MemberValue, objtmpDropdownItem.MemberName)
                            //    .Generate(filePath);
                        }
                        else
                        {
                            builder
                                .Company(company)
                                .Title(report)
                                .Data(objLeaveRegisterReports)
                                .Settings(settings)
                                .GroupBy(objtmpDropdownItem.MemberValue, objtmpDropdownItem.MemberName)
                                .OrderBy(objtmpDropdownItem.MemberName);

                            if (chkLeaveSummary.Checked)
                            {
                                objPivotLeaveInfo.Caption = "Leave Summary";
                                objPivotLeaveInfo.SpaceBefore = 0.05;
                                objPivotLeaveInfo.SpaceAfter = 0.05;
                                objPivotLeaveInfo.AddTable(objPivotLeaveTrendSummary);
                                builder.AddTableRow(objPivotLeaveInfo);
                            }
                            if (chkLeaveBalance.Checked)
                            {
                                objOutStandingLeaveInfo.Caption = "Leave Outstanding Summary";
                                objOutStandingLeaveInfo.SpaceBefore = 0.05;
                                objOutStandingLeaveInfo.SpaceAfter = 0.05;
                                objOutStandingLeaveInfo.AddTable(objOutstandingLeaveStatement);
                                builder.AddTableRow(objOutStandingLeaveInfo);
                            }
                            if (chkLeaveMatrix.Checked)
                            {
                                System.Data.DataTable dt = objLeaveTRReportsList.getLeaveMatrixInformation(objTempClientFinYearInfo.ClientID, Convert.ToDateTime(txtDTFrom.Text), Convert.ToDateTime(txtDTTo.Text));
                                ReportDynamicTable tbl1 = ReportTableFactory.FromDataTable(dt, "Leave Matrix");
                                tbl1.Columns[0].Visible = false;
                                tbl1.Columns[1].Width = 5;
                                tbl1.Columns[2].Width = 5;
                                tbl1.Columns[3].Width = 5;
                                tbl1.Columns[5].Visible = false;
                                tbl1.Title = "Leave Matrix";
                                tbl1.SpaceBefore = 1;
                                tbl1.SpaceAfter = 1;

                                builder.AddTableRow(tbl1);
                            }
                            if (chkLeaveSummary.Checked == false && chkLeaveBalance.Checked == false && chkLeaveLedger.Checked == false && chkLeaveMatrix.Checked == false)
                            {
                                builder
                                    .Company(company)
                                    .Title(report)
                                    .Data(objLeaveRegisterReports)
                                    .Settings(settings)
                                    .Generate(filePath);
                            }
                            else
                            {
                                builder.Generate(filePath);
                            }
                            //new ReportBuilder()
                            //    .Company(company)
                            //    .Title(report)
                            //    .Data(objLeaveRegisterReports)
                            //    .Settings(settings)
                            //    .GroupBy(objtmpDropdownItem.MemberValue, objtmpDropdownItem.MemberName)
                            //    .OrderBy(objtmpDropdownItem.MemberName)
                            //    .Generate(filePath);
                        }
                    }
                }
            }

            MessageBox.Show("Data Exported Successfully !!!", "StaffSync", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            Download.DownloadPDF(filePath);

        }
        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lblSelectedReport.Text))
            {
                MessageBox.Show("Please select a report.", "StaffSync", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (VerifyFilterValues(out string filter) == false)
                return;

            btnExport.Enabled = true;
            cmbGroupBy.Enabled = true;
            lblFilter.Text = filter;

            if (dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString() == ReportCode.REP_0001.ToString())
            {
                lblSelectedReport.Text = dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString();
                EmployeeMasterDetails(lblFilter.Text);
            }
            else if (dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString() == ReportCode.REP_0002.ToString())
            {
                lblSelectedReport.Text = dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString();
                EmployeePersonalInformation(lblFilter.Text);
            }
            else if (dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString() == ReportCode.REP_0003.ToString())
            {
                lblSelectedReport.Text = dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString();
                EmployeeActiveInactiveReport(lblFilter.Text);
            }
            else if (dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString() == ReportCode.REP_0004.ToString())
            {
                lblSelectedReport.Text = dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString();
                if(optDailyAttendance.Checked && !optMonthlyAttendanceRegister.Checked)
                {
                    EmployeeDailyAttendanceRegister(lblFilter.Text);
                }
                else if (!optDailyAttendance.Checked && optMonthlyAttendanceRegister.Checked)
                {
                    EmployeeMonthlyAttendanceRegister(lblFilter.Text);
                }
            }
            else if (dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString() == ReportCode.REP_0005.ToString())
            {
                lblSelectedReport.Text = dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString();
                if (optMonthlyAttendanceRegister.Checked)
                {
                    AttendanceSummaryReport(lblFilter.Text);
                }
            }
            else if (dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString() == ReportCode.REP_0006.ToString())
            {
                lblSelectedReport.Text = dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString();
                if (optMonthlyAttendanceRegister.Checked)
                {
                    LeaveRegisterInformation(lblFilter.Text);
                }
            }
        }

        private bool VerifyFilterValues(out string filter) 
        {
            DateTime dob, doj;
            string dateFormat = "dd-MM-yyyy";
            CultureInfo provider = CultureInfo.InvariantCulture;
            bool validationStatus = true;
            filter = "";

            bool hasCheckedFilter =
                       chkIncludeMonth.Checked
                    || chkIncludeDesignation.Checked
                    || chkIncludeDepartment.Checked
                    || chkIncludeGender.Checked
                    || chkBloodGroup.Checked
                    || chkIncludeBranch.Checked
                    || chkActiveInactiveStatus.Checked;

            bool hasSearch = cmbFreeSearchAttributeName.SelectedIndex > 0 && !string.IsNullOrWhiteSpace(txtSearch.Text);

            bool hasDateFilter =
                       optDOB.Checked
                    || optDOJ.Checked
                    || optProbDate.Checked
                    || optConfirmDate.Checked
                    || optDailyAttendance.Checked
                    || optMonthlyAttendanceRegister.Checked;

            if (!hasCheckedFilter && !hasSearch && !hasDateFilter)
            {
                MessageBox.Show("Please select at least one filter or a valid date filter.", "StaffSync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                validationStatus = false;
            }

            if (hasCheckedFilter)
            {
                if(chkIncludeMonth.Checked)
                    filter = filter + " AND ((DesigMas.DesignationTitle) = 'Sr. Software Engineer')";
                if (chkIncludeDesignation.Checked)
                    filter = filter + " AND ((DesigMas.DesignationTitle) = '" + cmbDesignation.Text + "')";
                if (chkIncludeDepartment.Checked)
                    filter = filter + " AND ((DepMas.DepartmentTitle) = '" + cmbDepartment.Text + "')";
                if (chkIncludeGender.Checked)
                    filter = filter + " AND ((SexMas.SexTitle) = '" + cmbGender.Text + "')";
                if (chkBloodGroup.Checked)
                    filter = filter + " AND ((BloodGroupMas.BloodGroupTitle) = '" + cmbBloodGroup.Text + "')";
                if (chkIncludeBranch.Checked)
                    filter = filter + " AND ((ClientBranchMas.ClientBranchCode) = '" + cmbBranch.Text.Substring(0, cmbBranch.Text.IndexOf(",")) + "')";
                if (chkActiveInactiveStatus.Checked)
                {
                    if(cmbActiveInactiveStatus.Text.ToString().ToLower() != "")
                        filter = filter + cmbActiveInactiveStatus.Text.ToString().ToLower() == "active" ? " AND ((ActiveInactiveStatus) = True)" : " AND ((ActiveInactiveStatus) = False" + ")";
                }
            }
            if (hasSearch)
            {
                tmpDropdownItem objtmpDropdownItem = (tmpDropdownItem)cmbFreeSearchAttributeName.SelectedItem;
                filter = filter + " AND ((" + objtmpDropdownItem.MemberValue + ") = '" + txtSearch.Text.ToString().Trim()  + "')";
            }
            if (hasDateFilter)
            {
                if (!DateTime.TryParseExact(txtDTFrom.Text, dateFormat, provider, DateTimeStyles.None, out DateTime dtFromDate))
                {
                    MessageBox.Show("Please select From Date.", "StaffSync", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDTFrom.Focus();
                    return false;
                }
                if (!DateTime.TryParseExact(txtDTTo.Text, dateFormat, provider, DateTimeStyles.None, out DateTime dtToDate))
                {
                    MessageBox.Show("Please select To Date.", "StaffSync", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDTTo.Focus();
                    return false;
                }

                if (dtToDate.Date < dtFromDate.Date)
                {
                    MessageBox.Show("'To Date' cannot be earlier than 'From Date'.", "StaffSync", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDTFrom.Focus();
                    return false;
                }
                if(optDOB.Checked)
                    filter = filter + " AND (((PersonalInfoMas.DOB) >= #" + Convert.ToDateTime(txtDTFrom.Text).ToString("dd-MMM-yyyy") + "#) AND ((PersonalInfoMas.DOB) <= #" + Convert.ToDateTime(txtDTTo.Text).ToString("dd-MMM-yyyy") + "#))";
                else if(optDOJ.Checked)
                    filter = filter + " AND (((PersonalInfoMas.DOJ) >= #" + Convert.ToDateTime(txtDTFrom.Text).ToString("dd-MMM-yyyy") + "#) AND ((PersonalInfoMas.DOJ) <= #" + Convert.ToDateTime(txtDTTo.Text).ToString("dd-MMM-yyyy") + "#))";
                else if (optProbDate.Checked)
                    filter = filter + " AND (((PersonalInfoMas.LastDateOfProbation) >= #" + Convert.ToDateTime(txtDTFrom.Text).ToString("dd-MMM-yyyy") + "#) AND ((PersonalInfoMas.LastDateOfProbation) <= #" + Convert.ToDateTime(txtDTTo.Text).ToString("dd-MMM-yyyy") + "#))";
                else if (optConfirmDate.Checked)
                    filter = filter + " AND (((PersonalInfoMas.DateOfConfirmation) >= #" + Convert.ToDateTime(txtDTFrom.Text).ToString("dd-MMM-yyyy") + "#) AND ((PersonalInfoMas.DateOfConfirmation) <= #" + Convert.ToDateTime(txtDTTo.Text).ToString("dd-MMM-yyyy") + "#))";
            }
            if (dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString() == ReportCode.REP_0006.ToString())
            {
                filter = filter.Replace(" AND ", "");
                if (filter != "")
                    filter = filter + " AND (((Q.ActualLeaveDateFrom) >= #" + Convert.ToDateTime(txtDTFrom.Text).ToString("dd-MMM-yyyy") + "#) AND ((Q.ActualLeaveDateTo) <= #" + Convert.ToDateTime(txtDTTo.Text).ToString("dd-MMM-yyyy") + "#))";
                else
                    filter = filter + " (((Q.ActualLeaveDateFrom) >= #" + Convert.ToDateTime(txtDTFrom.Text).ToString("dd-MMM-yyyy") + "#) AND ((Q.ActualLeaveDateTo) <= #" + Convert.ToDateTime(txtDTTo.Text).ToString("dd-MMM-yyyy") + "#))";

                if (cmbFilterLeaveMode.SelectedIndex > 0)
                {
                    filter = filter + " AND ((Q.LeaveMode) = '" + cmbFilterLeaveMode.Text.ToString() + "')";
                }

                if (cmbFilterLeaveType.SelectedIndex > 0)
                {
                    filter = filter + " AND ((Q.LeaveStatus) = '" + cmbFilterLeaveType.Text.ToString() + "')";
                }

                if (chkLSTLeaveTypeList.CheckedItems.Count == 0)
                {

                }
                else if (chkLSTLeaveTypeList.CheckedItems.Count == 1)
                {
                    foreach (var indCheckedItem in chkLSTLeaveTypeList.CheckedItems)
                    {
                        filter = filter + " AND ((Q.LeaveTypeTitle) = '" + indCheckedItem.ToString() + "')";
                    }
                }
                else if (chkLSTLeaveTypeList.CheckedItems.Count > 1)
                {
                    int selectedLeaveTypeCounter = 0;
                    filter = filter + " AND ((LeaveTypeMas.LeaveTypeTitle) IN ('";
                    foreach (var indCheckedItem in chkLSTLeaveTypeList.CheckedItems)
                    {
                        if (indCheckedItem.ToString() != "")
                        {
                            filter = filter + indCheckedItem.ToString() + "'";
                        }
                        if (selectedLeaveTypeCounter < chkLSTLeaveTypeList.CheckedItems.Count - 1)
                        {
                            filter = filter + ", '";
                        }
                        selectedLeaveTypeCounter = selectedLeaveTypeCounter + 1;
                    }
                    filter = filter + "))";
                }
            }

            return validationStatus;
        }

        private void chkBloodGroup_CheckedChanged(object sender, EventArgs e)
        {
            cmbBloodGroup.Enabled = chkBloodGroup.Checked;
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
        }

        public class tmpDropdownItem
        {
            public string MemberValue { get; set; }

            public string MemberName { get; set; }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetScreen();
            disableControls();
        }

        private void chkActiveInactiveStatus_CheckedChanged(object sender, EventArgs e)
        {
            cmbActiveInactiveStatus.Enabled = chkActiveInactiveStatus.Checked;
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
        }

        private void txtDTFrom_TextChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
            if (optDailyAttendance.Checked && !optMonthlyAttendanceRegister.Checked)
            {
                txtDTTo.Text = txtDTFrom.Text;
                txtDTTo.Enabled = false;
            }
            else if (!optDailyAttendance.Checked && optMonthlyAttendanceRegister.Checked)
            {
                DateTime dtToDate;
                string dateFormat = "dd-MM-yyyy";
                CultureInfo provider = CultureInfo.InvariantCulture;
                if (DateTime.TryParseExact(txtDTFrom.Text, dateFormat, provider, DateTimeStyles.None, out dtToDate) == true)
                {
                    txtDTTo.Text = Convert.ToDateTime(dtToDate.AddMonths(1).AddDays(-dtToDate.AddMonths(1).Day)).ToString("dd-MM-yyyy");
                    txtDTTo.Enabled = false;
                }
            }
        }

        private void optDailyAttendance_Click(object sender, EventArgs e)
        {
            if(optDailyAttendance.Checked)
                lblSelectedReportName.Text = "Daily Attendance Report";
        }

        private void optMonthlyAttendanceRegister_Click(object sender, EventArgs e)
        {
            if (optMonthlyAttendanceRegister.Checked)
                lblSelectedReportName.Text = "Monthly Attendance Report";
        }

        private void cmbDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
        }

        private void cmbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
        }

        private void cmbBloodGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
        }

        private void cmbActiveInactiveStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
        }

        private void optDOB_CheckedChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
        }

        private void optDOJ_CheckedChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
        }

        private void optProbDate_CheckedChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
        }

        private void optConfirmDate_CheckedChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
        }

        private void optDailyAttendance_CheckedChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
            if (dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString() == ReportCode.REP_0004.ToString())
            {
                if (optDailyAttendance.Checked && !optMonthlyAttendanceRegister.Checked)
                {
                    txtDTTo.Text = txtDTFrom.Text;
                    txtDTTo.Enabled = false;
                }
                else if (!optDailyAttendance.Checked && optMonthlyAttendanceRegister.Checked)
                {
                    DateTime dtToDate;
                    string dateFormat = "dd-MM-yyyy";
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    if (DateTime.TryParseExact(txtDTFrom.Text, dateFormat, provider, DateTimeStyles.None, out dtToDate) == true)
                    {
                        txtDTTo.Text = Convert.ToDateTime(dtToDate.AddMonths(1).AddDays(-dtToDate.AddMonths(1).Day)).ToString("dd-MM-yyyy");
                        txtDTTo.Enabled = false;
                    }
                }
            }
        }

        private void optMonthlyAttendanceRegister_CheckedChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
            if (dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString() == ReportCode.REP_0004.ToString())
            {
                if (optDailyAttendance.Checked && !optMonthlyAttendanceRegister.Checked)
                {
                    txtDTTo.Text = txtDTFrom.Text;
                    txtDTTo.Enabled = false;
                }
                else if (!optDailyAttendance.Checked && optMonthlyAttendanceRegister.Checked)
                {
                    DateTime dtToDate;
                    string dateFormat = "dd-MM-yyyy";
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    if (DateTime.TryParseExact(txtDTFrom.Text, dateFormat, provider, DateTimeStyles.None, out dtToDate) == true)
                    {
                        txtDTTo.Text = Convert.ToDateTime(dtToDate.AddMonths(1).AddDays(-dtToDate.AddMonths(1).Day)).ToString("dd-MM-yyyy");
                        txtDTTo.Enabled = false;
                    }
                }
            }
            else if (dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString() == ReportCode.REP_0005.ToString())
            {
                if (optMonthlyAttendanceRegister.Checked)
                {
                    DateTime dtToDate;
                    string dateFormat = "dd-MM-yyyy";
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    if (DateTime.TryParseExact(txtDTFrom.Text, dateFormat, provider, DateTimeStyles.None, out dtToDate) == true)
                    {
                        txtDTTo.Text = Convert.ToDateTime(dtToDate.AddMonths(1).AddDays(-dtToDate.AddMonths(1).Day)).ToString("dd-MM-yyyy");
                        txtDTTo.Enabled = false;
                    }
                }
            }
        }

        private void txtDTTo_TextChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
        }

        private void optRelivingDate_CheckedChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
        }

        private void optResignationDate_CheckedChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
        }

        private void cmbGroupBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGroupBy.SelectedIndex > 0)
            {
                chkIncludeGroupSummary.Enabled = true;
            }
            else
            {
                chkIncludeGroupSummary.Enabled = false;
                chkIncludeGroupSummary.Enabled = false;
            }
        }

        private void cmbFilterLeaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
            chkIncludeGroupSummary.Enabled = false;
        }

        private void chkLSTLeaveTypeList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
        }

        private void cmbFilterLeaveMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            cmbGroupBy.Enabled = false;
            chkIncludeGroupSummary.Enabled = false;
        }

        private void chkIndividualOrGroupedReport_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIndividualOrGroupedReport.Checked)
            {
                chkLeaveSummary.Enabled = true;
                chkLeaveSummary.Checked = true;
                chkLeaveBalance.Enabled = true;
                chkLeaveBalance.Checked = false;
                chkLeaveLedger.Enabled = true;
                chkLeaveLedger.Checked = false;
                chkLeaveMatrix.Enabled = true;
                chkLeaveMatrix.Checked = false;
            }
            else
            {

            }
        }

        private void chkLeaveSummary_CheckedChanged(object sender, EventArgs e)
        {
            if(chkIndividualOrGroupedReport.Checked && chkLeaveSummary.Checked)
            {
                chkLeaveBalance.Checked = false;
                chkLeaveLedger.Checked = false;
                chkLeaveMatrix.Checked = false;
            }
        }

        private void chkLeaveBalance_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIndividualOrGroupedReport.Checked && chkLeaveBalance.Checked)
            {
                chkLeaveSummary.Checked = false;
                chkLeaveLedger.Checked = false;
                chkLeaveMatrix.Checked = false;
            }
        }

        private void chkLeaveLedger_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIndividualOrGroupedReport.Checked && chkLeaveLedger.Checked)
            {
                chkLeaveBalance.Checked = false;
                chkLeaveSummary.Checked = false;
                chkLeaveMatrix.Checked = false;
            }
        }

        private void chkLeaveHistory_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIndividualOrGroupedReport.Checked && chkLeaveMatrix.Checked)
            {
                chkLeaveBalance.Checked = false;
                chkLeaveSummary.Checked = false;
                chkLeaveLedger.Checked = false;
            }
        }
    }
}
