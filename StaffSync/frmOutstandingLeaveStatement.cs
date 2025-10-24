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
        
        public frmOutstandingLeaveStatement()
        {
            InitializeComponent();
        }

        public frmOutstandingLeaveStatement(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmOutstandingLeaveStatement(int txtEmployeeID, int txtLeaveMasID)
        {
            InitializeComponent();

            dtgOutstandingLeaveStatement.DataSource = null;
            dtgOutstandingLeaveStatement.DataSource = objEmpLeaveEntitlementInfo.getEmployeeLeaveEntitilementList(Convert.ToInt16(txtEmployeeID), Convert.ToInt16(txtLeaveMasID));

            dtgOutstandingLeaveStatement.Columns["LeaveEntmtID"].Visible = false;
            dtgOutstandingLeaveStatement.Columns["EmpID"].Visible = false;
            dtgOutstandingLeaveStatement.Columns["EmpID"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["LeaveMasID"].Visible = false;
            dtgOutstandingLeaveStatement.Columns["LeaveMasID"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["LeaveTypeID"].Visible = false;
            dtgOutstandingLeaveStatement.Columns["LeaveTypeID"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["LeaveTypeTitle"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["LeaveTypeTitle"].Width = 325;

            dtgOutstandingLeaveStatement.Columns["TotalLeaves"].Width = 135;
            dtgOutstandingLeaveStatement.Columns["TotalLeaves"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["TotalLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgOutstandingLeaveStatement.Columns["TotalLeaves"].DefaultCellStyle.Format = "0.00";

            dtgOutstandingLeaveStatement.Columns["BalanceLeaves"].Width = 135;
            dtgOutstandingLeaveStatement.Columns["BalanceLeaves"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["BalanceLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgOutstandingLeaveStatement.Columns["BalanceLeaves"].DefaultCellStyle.Format = "0.00";

            dtgOutstandingLeaveStatement.Columns["UsedLeaves"].Width = 135;
            dtgOutstandingLeaveStatement.Columns["UsedLeaves"].ReadOnly = true;
            dtgOutstandingLeaveStatement.Columns["UsedLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgOutstandingLeaveStatement.Columns["UsedLeaves"].DefaultCellStyle.Format = "0.00";
            dtgOutstandingLeaveStatement.Columns["OrderID"].Visible = false;         
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
    }
}
