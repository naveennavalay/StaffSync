using StaffSync.StaffsyncDBDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using StaffSync.StaffsyncDBDTSetTableAdapters;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ModelStaffSync;

namespace StaffSync
{
    public partial class frmEmpSpecificReports : Form
    {
        //clsCountries objCountries = new clsCountries();
        //clsDesignation objDesignation = new clsDesignation();
        //clsStates objState = new clsStates();
        //clsRelationship objRelationship = new clsRelationship();
        
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
            LoadSalaryMonthList();
            LoadReportsList();

            cmbDepartment.DataSource = objDepartment.GetDepartmentList();
            cmbDepartment.DisplayMember = "DepartmentTitle";
            cmbDepartment.ValueMember = "DepartmentID";

            cmbBranch.DataSource = objClientBranchInfo.getBranchInfoList(objTempClientFinYearInfo.ClientID);
            cmbBranch.DisplayMember = "ClientBranchName";
            cmbBranch.ValueMember = "ClientBranchID";

            cmbGender.DataSource = objSexMaster.GetSexList();
            cmbGender.DisplayMember = "SexTitle";
            cmbGender.ValueMember = "SexID";

            cmbDesignation.DataSource = objDesignation.GetDesignationList();
            cmbDesignation.DisplayMember = "DesignationTitle";
            cmbDesignation.ValueMember = "DesignationID";
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

        private void btnCancel_Click(object sender, EventArgs e)
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
            MessageBox.Show(dtgReportsList.SelectedRows[0].Cells["ReportsID"].Value.ToString(), "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
