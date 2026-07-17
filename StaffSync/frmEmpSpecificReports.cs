using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Krypton.Toolkit;
using ModelStaffSync;
using ReportingEngine;
using ReportingEngine.Core;
using ReportingEngine.Helpers;
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

        private void btnSearch_Click(object sender, EventArgs e)
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

        private void lnkViewAuditLog_LinkClicked(object sender, EventArgs e)
        {

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
                    new tmpDropdownItem { MemberValue = "DepMas.DepartmentTitle", MemberName = "Department" },
                };
                cmbGroupBy.DataSource = null;
                cmbGroupBy.Items.Clear();
                cmbGroupBy.DataSource = lstGroupByValues;
                cmbGroupBy.DisplayMember = "MemberName";
                cmbGroupBy.ValueMember = "MemberValue";
                cmbGroupBy.SelectedIndex = 0;
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

                chkActiveInactiveStatus.Enabled = true;
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

                lblSelectedReport.Text = dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString();
                lblSelectedReportName.Text = dtgReportsList.SelectedRows[0].Cells["ReportsName"].Value.ToString().Replace("-", "_").ToString();

                List<tmpDropdownItem> lstGroupByValues = new List<tmpDropdownItem>()
                {
                    new tmpDropdownItem { MemberValue = "Blank", MemberName = "" },
                    new tmpDropdownItem { MemberValue = "DepMas.DepartmentTitle", MemberName = "Department" },
                };
                cmbGroupBy.DataSource = null;
                cmbGroupBy.Items.Clear();
                cmbGroupBy.DataSource = lstGroupByValues;
                cmbGroupBy.DisplayMember = "MemberName";
                cmbGroupBy.ValueMember = "MemberValue";
                cmbGroupBy.SelectedIndex = 0;
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
                    new tmpDropdownItem { MemberValue = "DepMas.DepartmentTitle", MemberName = "Department" },
                };
                cmbGroupBy.DataSource = null;
                cmbGroupBy.Items.Clear();
                cmbGroupBy.DataSource = lstGroupByValues;
                cmbGroupBy.DisplayMember = "MemberName";
                cmbGroupBy.ValueMember = "MemberValue";
                cmbGroupBy.SelectedIndex = 0;
            }
            else if (dtgReportsList.SelectedRows[0].Cells["ReportsCode"].Value.ToString().Replace("-", "_").ToString() == ReportCode.REP_0005.ToString())
            {

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

                LogoPath = @Application.StartupPath + "\\" + objSelectedClientInfo.ClientCode + "-logo.png",
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


            if (lblSelectedReport.Text.ToString() == ReportCode.REP_0001.ToString())
            {
                new ReportBuilder()
                .Company(company)
                .Title(report)
                .Data(objActiveEmployeeListReport)
                .Settings(settings)
                .Generate(filePath);
            }
            else if (lblSelectedReport.Text.ToString() == ReportCode.REP_0002.ToString())
            {
                new ReportBuilder()
                .Company(company)
                .Title(report)
                .Data(objPersonalInformationListReport)
                .Settings(settings)
                .Generate(filePath);
            }
            else if (lblSelectedReport.Text.ToString() == ReportCode.REP_0003.ToString())
            {
                new ReportBuilder()
                .Company(company)
                .Title(report)
                .Data(objEmployeeActiveInactiveReportListReport)
                .Settings(settings)
                .Generate(filePath);
            }
            else if (lblSelectedReport.Text.ToString() == ReportCode.REP_0004.ToString())
            {
                if (optDailyAttendance.Checked && !optMonthlyAttendanceRegister.Checked)
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
                else if (!optDailyAttendance.Checked && optMonthlyAttendanceRegister.Checked)
                {
                    report.ReportTitle = report.ReportTitle + "\n(" + Convert.ToDateTime(txtDTFrom.Text).ToString("dd-MMM-yyyy") + " - " + Convert.ToDateTime(txtDTTo.Text).ToString("dd-MMM-yyyy") + ")";

                    int totalEmployees = objMonthlyAttendanceReport.Count;
                    int totalPresentDays = objMonthlyAttendanceReport.Sum(x => x.PresentCount);
                    int totalLeaveDays = objMonthlyAttendanceReport.Sum(x => x.LeaveCount);
                    int totalHalfLeaveDays = objMonthlyAttendanceReport.Sum(x => x.HalfLeaveCount);

                    DateTime month = Convert.ToDateTime(txtDTFrom.Text);

                    int totalDays = DateTime.DaysInMonth(month.Year, month.Month);

                    int weekEndDays = Enumerable.Range(1, totalDays).Select(day => new DateTime(month.Year, month.Month, day)) .Count(d => d.DayOfWeek == DayOfWeek.Saturday || d.DayOfWeek == DayOfWeek.Sunday);
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
    }
}
