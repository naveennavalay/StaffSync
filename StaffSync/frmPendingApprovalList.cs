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
    public partial class frmPendingApprovalList : Form
    {
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        DALStaffSync.clsLeaveTRList objLeaveTRList = new DALStaffSync.clsLeaveTRList();
        DALStaffSync.clsWeeklyOffInfo objWeeklyOffInfo = new DALStaffSync.clsWeeklyOffInfo();
        DALStaffSync.clsAttendanceMas objAttendanceInfo = new DALStaffSync.clsAttendanceMas();
        DALStaffSync.clsEmpLeaveEntitlementInfo objEmpLeaveEntitlementInfo = new DALStaffSync.clsEmpLeaveEntitlementInfo();
        List<WklyOffProfileDetailsInfo> lstWeeklyOffDetailsInfo = new List<WklyOffProfileDetailsInfo>();
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();

        public frmPendingApprovalList()
        {
            InitializeComponent();
        }

        public frmPendingApprovalList(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmPendingApprovalList(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
        }

        public frmPendingApprovalList(int txtEmployeeID, int txtLeaveMasID)
        {
            InitializeComponent();

            dtgPendingApprovalLeaveInfo.DataSource = null;
            dtgPendingApprovalLeaveInfo.DataSource = objEmpLeaveEntitlementInfo.getEmployeeLeaveEntitilementList(Convert.ToInt16(txtEmployeeID), Convert.ToInt16(txtLeaveMasID));

            dtgPendingApprovalLeaveInfo.Columns["LeaveEntmtID"].Visible = false;
            dtgPendingApprovalLeaveInfo.Columns["EmpID"].Visible = false;
            dtgPendingApprovalLeaveInfo.Columns["EmpID"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["LeaveMasID"].Visible = false;
            dtgPendingApprovalLeaveInfo.Columns["LeaveMasID"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["LeaveTypeID"].Visible = false;
            dtgPendingApprovalLeaveInfo.Columns["LeaveTypeID"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["LeaveTypeTitle"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["LeaveTypeTitle"].Width = 325;

            dtgPendingApprovalLeaveInfo.Columns["TotalLeaves"].Width = 135;
            dtgPendingApprovalLeaveInfo.Columns["TotalLeaves"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["TotalLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgPendingApprovalLeaveInfo.Columns["TotalLeaves"].DefaultCellStyle.Format = "0.00";

            dtgPendingApprovalLeaveInfo.Columns["BalanceLeaves"].Width = 135;
            dtgPendingApprovalLeaveInfo.Columns["BalanceLeaves"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["BalanceLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgPendingApprovalLeaveInfo.Columns["BalanceLeaves"].DefaultCellStyle.Format = "0.00";

            dtgPendingApprovalLeaveInfo.Columns["UsedLeaves"].Width = 135;
            dtgPendingApprovalLeaveInfo.Columns["UsedLeaves"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["UsedLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgPendingApprovalLeaveInfo.Columns["UsedLeaves"].DefaultCellStyle.Format = "0.00";
            dtgPendingApprovalLeaveInfo.Columns["OrderID"].Visible = false;         
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPendingApprovalList_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'staffsyncDBDataSet1.qryAllEmpLeavePendingStatement' table. You can move, or remove it, as needed.
            //this.qryAllEmpLeavePendingStatementTableAdapter.Fill(this.staffsyncDBDataSet1.qryAllEmpLeavePendingStatement);
            //// TODO: This line of code loads data into the 'staffsyncDBDTSet.EmpMasInfo' table. You can move, or remove it, as needed.
            //this.empMasInfoTableAdapter.Fill(this.staffsyncDBDTSet.EmpMasInfo);
            onCancelButtonClick();
            disableControls();
            clearControls();
            FormatTheGrid();
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
            objTempCurrentlyLoggedInUserInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objTempCurrentlyLoggedInUserInfo.EmpID.ToString()));

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
            dtgPendingApprovalLeaveInfo.Enabled = true;
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
            List<BulkPendingLeaveApproval> objBulkPendingLeaveApprovalList = objLeaveTRList.getBulkPendingLeaveApprovalList();
            dtgPendingApprovalLeaveInfo.DataSource = objBulkPendingLeaveApprovalList;

            dtgPendingApprovalLeaveInfo.Columns["Select"].ReadOnly = false;
            dtgPendingApprovalLeaveInfo.Columns["Select"].Visible = true;
            dtgPendingApprovalLeaveInfo.Columns["Select"].Width = 50;
            dtgPendingApprovalLeaveInfo.Columns["EmpID"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["EmpID"].Width = 50;
            dtgPendingApprovalLeaveInfo.Columns["EmpID"].Visible = false;
            dtgPendingApprovalLeaveInfo.Columns["EmpCode"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["EmpCode"].Width = 100;
            dtgPendingApprovalLeaveInfo.Columns["EmpCode"].Visible = true;
            dtgPendingApprovalLeaveInfo.Columns["EmpName"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["EmpName"].Width = 250;
            dtgPendingApprovalLeaveInfo.Columns["EmpName"].Visible = true;
            dtgPendingApprovalLeaveInfo.Columns["DesignationTitle"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["DesignationTitle"].Width = 200;
            dtgPendingApprovalLeaveInfo.Columns["DesignationTitle"].Visible = true;
            dtgPendingApprovalLeaveInfo.Columns["DepartmentTitle"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["DepartmentTitle"].Width = 200;
            dtgPendingApprovalLeaveInfo.Columns["DepartmentTitle"].Visible = true;
            dtgPendingApprovalLeaveInfo.Columns["LeaveTypeID"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["LeaveTypeID"].Width = 100;
            dtgPendingApprovalLeaveInfo.Columns["LeaveTypeID"].Visible = false;
            dtgPendingApprovalLeaveInfo.Columns["LeaveTypeTitle"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["LeaveTypeTitle"].Width = 200;
            dtgPendingApprovalLeaveInfo.Columns["LeaveTypeTitle"].Visible = true;
            dtgPendingApprovalLeaveInfo.Columns["LeaveTRID"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["LeaveTRID"].Width = 100;
            dtgPendingApprovalLeaveInfo.Columns["LeaveTRID"].Visible = false;
            dtgPendingApprovalLeaveInfo.Columns["ActualLeaveDateFrom"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["ActualLeaveDateFrom"].Width = 100;
            dtgPendingApprovalLeaveInfo.Columns["ActualLeaveDateFrom"].Visible = true;
            dtgPendingApprovalLeaveInfo.Columns["ActualLeaveDateFrom"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgPendingApprovalLeaveInfo.Columns["ActualLeaveDateTo"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["ActualLeaveDateTo"].Width = 100;
            dtgPendingApprovalLeaveInfo.Columns["ActualLeaveDateTo"].Visible = true;
            dtgPendingApprovalLeaveInfo.Columns["ActualLeaveDateTo"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgPendingApprovalLeaveInfo.Columns["LeaveDuration"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["LeaveDuration"].Width = 100;
            dtgPendingApprovalLeaveInfo.Columns["LeaveDuration"].Visible = true;
            dtgPendingApprovalLeaveInfo.Columns["LeaveComments"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["LeaveComments"].Width = 250;
            dtgPendingApprovalLeaveInfo.Columns["LeaveComments"].Visible = true;
            dtgPendingApprovalLeaveInfo.Columns["Canceled"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["Canceled"].Width = 100;
            dtgPendingApprovalLeaveInfo.Columns["Canceled"].Visible = false;
            dtgPendingApprovalLeaveInfo.Columns["CanceledDate"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["CanceledDate"].Width = 125;
            dtgPendingApprovalLeaveInfo.Columns["CanceledDate"].Visible = false;
            dtgPendingApprovalLeaveInfo.Columns["CanceledDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgPendingApprovalLeaveInfo.Columns["LeaveApprovalComments"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["LeaveApprovalComments"].Width = 350;
            dtgPendingApprovalLeaveInfo.Columns["LeaveApprovalComments"].Visible = false;
            dtgPendingApprovalLeaveInfo.Columns["LeaveRejectionComments"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["LeaveRejectionComments"].Width = 350;
            dtgPendingApprovalLeaveInfo.Columns["LeaveRejectionComments"].Visible = false;
            dtgPendingApprovalLeaveInfo.Columns["OrderID"].ReadOnly = true;
            dtgPendingApprovalLeaveInfo.Columns["OrderID"].Width = 350;
            dtgPendingApprovalLeaveInfo.Columns["OrderID"].Visible = false;

            foreach (DataGridViewRow indRow in this.dtgPendingApprovalLeaveInfo.Rows)
            {
                indRow.Cells["Select"].ReadOnly = false;
                if (Convert.ToBoolean(indRow.Cells["Canceled"].Value.ToString()) == true)
                {
                    indRow.DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
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
            if (e.RowIndex >= 0 && e.ColumnIndex == dtgPendingApprovalLeaveInfo.Columns["Select"].Index)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dtgPendingApprovalLeaveInfo.Rows[e.RowIndex].Cells["Select"];
                //chk.Value = !(Convert.ToBoolean(chk.Value));
                dtgPendingApprovalLeaveInfo.CommitEdit(DataGridViewDataErrorContexts.Commit);
                btnSaveDetails.Enabled = false;
                foreach (DataGridViewRow indRow in this.dtgPendingApprovalLeaveInfo.Rows)
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
            dtgPendingApprovalLeaveInfo.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void btnSelectUnselect_Click(object sender, EventArgs e)
        {

        }

        private void chkSelectUnselect_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void frmPendingApprovalList_Activated(object sender, EventArgs e)
        {
            dtgPendingApprovalLeaveInfo.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
        }
    }
}
