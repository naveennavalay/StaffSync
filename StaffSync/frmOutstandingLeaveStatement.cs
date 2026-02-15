using ModelStaffSync;
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
    public partial class frmOutstandingLeaveStatement : Form
    {
        DALStaffSync.clsLeaveTRList objLeaveTRList = new DALStaffSync.clsLeaveTRList();
        DALStaffSync.clsWeeklyOffInfo objWeeklyOffInfo = new DALStaffSync.clsWeeklyOffInfo();
        DALStaffSync.clsAttendanceMas objAttendanceInfo = new DALStaffSync.clsAttendanceMas();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        DALStaffSync.clsEmpLeaveEntitlementInfo objEmpLeaveEntitlementInfo = new DALStaffSync.clsEmpLeaveEntitlementInfo();
        List<WklyOffProfileDetailsInfo> lstWeeklyOffDetailsInfo = new List<WklyOffProfileDetailsInfo>();
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();

        public frmOutstandingLeaveStatement()
        {
            InitializeComponent();
        }

        public frmOutstandingLeaveStatement(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmOutstandingLeaveStatement(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
        }

        public frmOutstandingLeaveStatement(int txtEmployeeID, int txtLeaveMasID)
        {
            InitializeComponent();
            CompactView(objTempClientFinYearInfo.ClientID);
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmOutstandingLeaveStatement_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'staffsyncDBDataSet1.qryAllEmpLeavePendingStatement' table. You can move, or remove it, as needed.
            //this.qryAllEmpLeavePendingStatementTableAdapter.Fill(this.staffsyncDBDataSet1.qryAllEmpLeavePendingStatement);
            //// TODO: This line of code loads data into the 'staffsyncDBDTSet.EmpMasInfo' table. You can move, or remove it, as needed.
            //this.empMasInfoTableAdapter.Fill(this.staffsyncDBDTSet.EmpMasInfo);
            chkCompactDetailedView.Text = "Detailed View";
            CompactView(objTempClientFinYearInfo.ClientID);
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
            string filePath = AppVariables.TempFolderPath + @"\Consolidated Leave Summary.csv";
            bool ReportGenerated = Download.DownloadExcel(filePath, dtgOutstandingLeaveStatement);
            if (ReportGenerated)
                Download.OpenCSV(filePath);

            //if (chkCompactDetailedView.Checked)
            //{
            //    chkCompactDetailedView.Text = "Compact View";
            //    DetailedView(objTempClientFinYearInfo.ClientID);
            //}
            //else
            //{
            //    chkCompactDetailedView.Text = "Detailed View";
            //    CompactView(objTempClientFinYearInfo.ClientID);
            //}
        }

        public void clearControls()
        {
            //FormatTheGrid();
        }

        public void enableControls()
        {
            dtgOutstandingLeaveStatement.Enabled = true;
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

        }

        private void FormatTheGrid()
        {
            List<OutstandingLeaveStatement> objBulkPendingLeaveApprovalList = objLeaveTRList.getOutStandingLeaveStaetment();
            dtgOutstandingLeaveStatement.DataSource = objBulkPendingLeaveApprovalList;

            dtgOutstandingLeaveStatement.Columns["Select"].ReadOnly = false;
            dtgOutstandingLeaveStatement.Columns["Select"].Visible = true;
            dtgOutstandingLeaveStatement.Columns["Select"].Width = 50;
            dtgOutstandingLeaveStatement.Columns["EmpID"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["EmpID"].Width = 50;
            dtgOutstandingLeaveStatement.Columns["EmpID"].Visible = false;
            dtgOutstandingLeaveStatement.Columns["EmpCode"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["EmpCode"].Width = 100;
            dtgOutstandingLeaveStatement.Columns["EmpCode"].Visible = true;
            dtgOutstandingLeaveStatement.Columns["EmpName"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["EmpName"].Width = 250;
            dtgOutstandingLeaveStatement.Columns["EmpName"].Visible = true;
            dtgOutstandingLeaveStatement.Columns["DesignationTitle"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["DesignationTitle"].Width = 200;
            dtgOutstandingLeaveStatement.Columns["DesignationTitle"].Visible = true;
            dtgOutstandingLeaveStatement.Columns["DepartmentTitle"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["DepartmentTitle"].Width = 200;
            dtgOutstandingLeaveStatement.Columns["DepartmentTitle"].Visible = true;
            dtgOutstandingLeaveStatement.Columns["TotalLeaves"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["TotalLeaves"].Width = 100;
            dtgOutstandingLeaveStatement.Columns["TotalLeaves"].DefaultCellStyle.Format = "0.00";
            dtgOutstandingLeaveStatement.Columns["BalanceLeaves"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["BalanceLeaves"].Width = 100;
            dtgOutstandingLeaveStatement.Columns["BalanceLeaves"].DefaultCellStyle.Format = "0.00";
            dtgOutstandingLeaveStatement.Columns["UtilisedLeaves"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["UtilisedLeaves"].Width = 100;
            dtgOutstandingLeaveStatement.Columns["UtilisedLeaves"].DefaultCellStyle.Format = "0.00";

            foreach (DataGridViewRow indRow in this.dtgOutstandingLeaveStatement.Rows)
            {
                indRow.Cells["UtilisedLeaves"].Value = Convert.ToDecimal(indRow.Cells["TotalLeaves"].Value) - Convert.ToDecimal(indRow.Cells["BalanceLeaves"].Value);
            }
            dtgOutstandingLeaveStatement.Columns["UtilisedLeaves"].DefaultCellStyle.Format = "0.00";
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
            if (e.RowIndex >= 0 && e.ColumnIndex == dtgOutstandingLeaveStatement.Columns["Select"].Index)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dtgOutstandingLeaveStatement.Rows[e.RowIndex].Cells["Select"];
                //chk.Value = !(Convert.ToBoolean(chk.Value));
                dtgOutstandingLeaveStatement.CommitEdit(DataGridViewDataErrorContexts.Commit);
                btnSaveDetails.Enabled = false;
                foreach (DataGridViewRow indRow in this.dtgOutstandingLeaveStatement.Rows)
                {
                    if (Convert.ToBoolean(indRow.Cells["Select"].Value.ToString()) == true)
                    {
                        btnSaveDetails.Enabled = true;
                    }
                }
            }
        }

        private void dtgBulkLeaveApproval_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dtgOutstandingLeaveStatement.CommitEdit(DataGridViewDataErrorContexts.Commit);
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

        private void chkCompactDetailedView_Click(object sender, EventArgs e)
        {
            if (chkCompactDetailedView.Checked)
            {
                chkCompactDetailedView.Text = "Compact View";
                DetailedView(objTempClientFinYearInfo.ClientID);
            }
            else
            {
                chkCompactDetailedView.Text = "Detailed View";
                CompactView(objTempClientFinYearInfo.ClientID);
            }
        }

        public void CompactView(int ClientID)
        {
            dtgOutstandingLeaveStatement.DataSource = null;
            dtgOutstandingLeaveStatement.DataSource = objEmpLeaveEntitlementInfo.getConsolidatedLeaveOutStandingStatement(Convert.ToInt16(ClientID));
            dtgOutstandingLeaveStatement.Columns["EmpID"].Visible = false;
            dtgOutstandingLeaveStatement.Columns["EmpID"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["EmpCode"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["EmpCode"].Width = 150;
            dtgOutstandingLeaveStatement.Columns["EmpName"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["EmpName"].Width = 200;
            dtgOutstandingLeaveStatement.Columns["DesignationTitle"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["DesignationTitle"].Width = 200;
            dtgOutstandingLeaveStatement.Columns["DepartmentTitle"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["DepartmentTitle"].Width = 200;
            dtgOutstandingLeaveStatement.Columns["LeaveBalance"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["LeaveBalance"].Width = 150;
            dtgOutstandingLeaveStatement.Columns["LeaveBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgOutstandingLeaveStatement.Columns["LeaveBalance"].DefaultCellStyle.Format = "0.00";
        }

        public void DetailedView(int ClientID)
        {
            dtgOutstandingLeaveStatement.DataSource = null;
            dtgOutstandingLeaveStatement.DataSource = objEmpLeaveEntitlementInfo.getDetailedLeaveStatement(Convert.ToInt16(ClientID));
            dtgOutstandingLeaveStatement.Columns["EmpID"].Visible = false;
            dtgOutstandingLeaveStatement.Columns["EmpID"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["EmpCode"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["EmpCode"].Width = 150;
            dtgOutstandingLeaveStatement.Columns["EmpName"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["EmpName"].Width = 200;
            dtgOutstandingLeaveStatement.Columns["DesignationTitle"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["DesignationTitle"].Width = 200;
            dtgOutstandingLeaveStatement.Columns["DepartmentTitle"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["DepartmentTitle"].Width = 200;
            dtgOutstandingLeaveStatement.Columns["_01PaidLeave"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["_01PaidLeave"].Width = 150;
            dtgOutstandingLeaveStatement.Columns["_01PaidLeave"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgOutstandingLeaveStatement.Columns["_01PaidLeave"].DefaultCellStyle.Format = "0.00";
            dtgOutstandingLeaveStatement.Columns["_02CompensatoryOff"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["_02CompensatoryOff"].Width = 150;
            dtgOutstandingLeaveStatement.Columns["_02CompensatoryOff"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgOutstandingLeaveStatement.Columns["_02CompensatoryOff"].DefaultCellStyle.Format = "0.00";
            dtgOutstandingLeaveStatement.Columns["_03UnpaidLeave"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["_03UnpaidLeave"].Width = 150;
            dtgOutstandingLeaveStatement.Columns["_03UnpaidLeave"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgOutstandingLeaveStatement.Columns["_03UnpaidLeave"].DefaultCellStyle.Format = "0.00";
            dtgOutstandingLeaveStatement.Columns["_04LossofPayLOPLeaveWithoutPayLWP"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["_04LossofPayLOPLeaveWithoutPayLWP"].Width = 150;
            dtgOutstandingLeaveStatement.Columns["_04LossofPayLOPLeaveWithoutPayLWP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgOutstandingLeaveStatement.Columns["_04LossofPayLOPLeaveWithoutPayLWP"].DefaultCellStyle.Format = "0.00";
            dtgOutstandingLeaveStatement.Columns["_05SickLeave"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["_05SickLeave"].Width = 150;
            dtgOutstandingLeaveStatement.Columns["_05SickLeave"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgOutstandingLeaveStatement.Columns["_05SickLeave"].DefaultCellStyle.Format = "0.00";
            dtgOutstandingLeaveStatement.Columns["_06PrivilegeLeaveEarnedLeave"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["_06PrivilegeLeaveEarnedLeave"].Width = 150;
            dtgOutstandingLeaveStatement.Columns["_06PrivilegeLeaveEarnedLeave"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgOutstandingLeaveStatement.Columns["_06PrivilegeLeaveEarnedLeave"].DefaultCellStyle.Format = "0.00";
            dtgOutstandingLeaveStatement.Columns["_07CasualLeave"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["_07CasualLeave"].Width = 150;
            dtgOutstandingLeaveStatement.Columns["_07CasualLeave"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgOutstandingLeaveStatement.Columns["_07CasualLeave"].DefaultCellStyle.Format = "0.00";
            dtgOutstandingLeaveStatement.Columns["_08MaternityLeave"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["_08MaternityLeave"].Width = 150;
            dtgOutstandingLeaveStatement.Columns["_08MaternityLeave"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgOutstandingLeaveStatement.Columns["_08MaternityLeave"].DefaultCellStyle.Format = "0.00";
            dtgOutstandingLeaveStatement.Columns["_09MarriageLeave"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["_09MarriageLeave"].Width = 150;
            dtgOutstandingLeaveStatement.Columns["_09MarriageLeave"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgOutstandingLeaveStatement.Columns["_09MarriageLeave"].DefaultCellStyle.Format = "0.00";
            dtgOutstandingLeaveStatement.Columns["_10PaternityLeave"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["_10PaternityLeave"].Width = 150;
            dtgOutstandingLeaveStatement.Columns["_10PaternityLeave"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgOutstandingLeaveStatement.Columns["_10PaternityLeave"].DefaultCellStyle.Format = "0.00";
            dtgOutstandingLeaveStatement.Columns["_11BereavementLeave"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["_11BereavementLeave"].Width = 150;
            dtgOutstandingLeaveStatement.Columns["_11BereavementLeave"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgOutstandingLeaveStatement.Columns["_11BereavementLeave"].DefaultCellStyle.Format = "0.00";
            dtgOutstandingLeaveStatement.Columns["_12PublicHoliday"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["_12PublicHoliday"].Width = 150;
            dtgOutstandingLeaveStatement.Columns["_12PublicHoliday"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgOutstandingLeaveStatement.Columns["_12PublicHoliday"].DefaultCellStyle.Format = "0.00";
            dtgOutstandingLeaveStatement.Columns["_13BirthdayLeave"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["_13BirthdayLeave"].Width = 150;
            dtgOutstandingLeaveStatement.Columns["_13BirthdayLeave"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgOutstandingLeaveStatement.Columns["_13BirthdayLeave"].DefaultCellStyle.Format = "0.00";
        }
    }
}
