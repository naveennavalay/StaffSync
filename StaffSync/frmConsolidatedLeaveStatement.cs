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
    public partial class frmConsolidatedLeaveStatement : Form
    {
        DALStaffSync.clsLeaveTRList objLeaveTRList = new DALStaffSync.clsLeaveTRList();
        DALStaffSync.clsWeeklyOffInfo objWeeklyOffInfo = new DALStaffSync.clsWeeklyOffInfo();
        DALStaffSync.clsAttendanceMas objAttendanceInfo = new DALStaffSync.clsAttendanceMas();
        DALStaffSync.clsEmpLeaveEntitlementInfo objEmpLeaveEntitlementInfo = new DALStaffSync.clsEmpLeaveEntitlementInfo();
        List<WklyOffProfileDetailsInfo> lstWeeklyOffDetailsInfo = new List<WklyOffProfileDetailsInfo>();

        public frmConsolidatedLeaveStatement()
        {
            InitializeComponent();
        }

        public frmConsolidatedLeaveStatement(int txtEmployeeID, int txtLeaveMasID)
        {
            InitializeComponent();

            dtgConsolidatedLeaveStatement.DataSource = null;
            dtgConsolidatedLeaveStatement.DataSource = objEmpLeaveEntitlementInfo.getEmployeeLeaveEntitilementList(Convert.ToInt16(txtEmployeeID), Convert.ToInt16(txtLeaveMasID));

            dtgConsolidatedLeaveStatement.Columns["LeaveEntmtID"].Visible = false;
            dtgConsolidatedLeaveStatement.Columns["EmpID"].Visible = false;
            dtgConsolidatedLeaveStatement.Columns["EmpID"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["LeaveMasID"].Visible = false;
            dtgConsolidatedLeaveStatement.Columns["LeaveMasID"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["LeaveTypeID"].Visible = false;
            dtgConsolidatedLeaveStatement.Columns["LeaveTypeID"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["LeaveTypeTitle"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["LeaveTypeTitle"].Width = 325;

            dtgConsolidatedLeaveStatement.Columns["TotalLeaves"].Width = 135;
            dtgConsolidatedLeaveStatement.Columns["TotalLeaves"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["TotalLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgConsolidatedLeaveStatement.Columns["TotalLeaves"].DefaultCellStyle.Format = "0.00";

            dtgConsolidatedLeaveStatement.Columns["BalanceLeaves"].Width = 135;
            dtgConsolidatedLeaveStatement.Columns["BalanceLeaves"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["BalanceLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgConsolidatedLeaveStatement.Columns["BalanceLeaves"].DefaultCellStyle.Format = "0.00";

            dtgConsolidatedLeaveStatement.Columns["UsedLeaves"].Width = 135;
            dtgConsolidatedLeaveStatement.Columns["UsedLeaves"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["UsedLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgConsolidatedLeaveStatement.Columns["UsedLeaves"].DefaultCellStyle.Format = "0.00";
            dtgConsolidatedLeaveStatement.Columns["OrderID"].Visible = false;         
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmConsolidatedLeaveStatement_Load(object sender, EventArgs e)
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
            dtgConsolidatedLeaveStatement.Enabled = true;
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
            List<BulkPendingLeaveApproval> objBulkPendingLeaveApprovalList = objLeaveTRList.getRejectedLeavelList();
            dtgConsolidatedLeaveStatement.DataSource = objBulkPendingLeaveApprovalList;

            dtgConsolidatedLeaveStatement.Columns["Select"].ReadOnly = false;
            dtgConsolidatedLeaveStatement.Columns["Select"].Visible = true;
            dtgConsolidatedLeaveStatement.Columns["Select"].Width = 50;
            dtgConsolidatedLeaveStatement.Columns["EmpID"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["EmpID"].Width = 50;
            dtgConsolidatedLeaveStatement.Columns["EmpID"].Visible = false;
            dtgConsolidatedLeaveStatement.Columns["EmpCode"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["EmpCode"].Width = 100;
            dtgConsolidatedLeaveStatement.Columns["EmpCode"].Visible = true;
            dtgConsolidatedLeaveStatement.Columns["EmpName"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["EmpName"].Width = 250;
            dtgConsolidatedLeaveStatement.Columns["EmpName"].Visible = true;
            dtgConsolidatedLeaveStatement.Columns["DesignationTitle"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["DesignationTitle"].Width = 200;
            dtgConsolidatedLeaveStatement.Columns["DesignationTitle"].Visible = true;
            dtgConsolidatedLeaveStatement.Columns["DepartmentTitle"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["DepartmentTitle"].Width = 200;
            dtgConsolidatedLeaveStatement.Columns["DepartmentTitle"].Visible = true;
            dtgConsolidatedLeaveStatement.Columns["LeaveTypeID"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["LeaveTypeID"].Width = 100;
            dtgConsolidatedLeaveStatement.Columns["LeaveTypeID"].Visible = false;
            dtgConsolidatedLeaveStatement.Columns["LeaveTypeTitle"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["LeaveTypeTitle"].Width = 200;
            dtgConsolidatedLeaveStatement.Columns["LeaveTypeTitle"].Visible = true;
            dtgConsolidatedLeaveStatement.Columns["LeaveTRID"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["LeaveTRID"].Width = 100;
            dtgConsolidatedLeaveStatement.Columns["LeaveTRID"].Visible = false;
            dtgConsolidatedLeaveStatement.Columns["ActualLeaveDateFrom"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["ActualLeaveDateFrom"].Width = 100;
            dtgConsolidatedLeaveStatement.Columns["ActualLeaveDateFrom"].Visible = true;
            dtgConsolidatedLeaveStatement.Columns["ActualLeaveDateFrom"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgConsolidatedLeaveStatement.Columns["ActualLeaveDateTo"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["ActualLeaveDateTo"].Width = 100;
            dtgConsolidatedLeaveStatement.Columns["ActualLeaveDateTo"].Visible = true;
            dtgConsolidatedLeaveStatement.Columns["ActualLeaveDateTo"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgConsolidatedLeaveStatement.Columns["LeaveDuration"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["LeaveDuration"].Width = 100;
            dtgConsolidatedLeaveStatement.Columns["LeaveDuration"].Visible = true;
            dtgConsolidatedLeaveStatement.Columns["LeaveComments"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["LeaveComments"].Width = 250;
            dtgConsolidatedLeaveStatement.Columns["LeaveComments"].Visible = true;
            dtgConsolidatedLeaveStatement.Columns["Canceled"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["Canceled"].Width = 100;
            dtgConsolidatedLeaveStatement.Columns["Canceled"].Visible = true;
            dtgConsolidatedLeaveStatement.Columns["CanceledDate"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["CanceledDate"].Width = 125;
            dtgConsolidatedLeaveStatement.Columns["CanceledDate"].Visible = true;
            dtgConsolidatedLeaveStatement.Columns["CanceledDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgConsolidatedLeaveStatement.Columns["LeaveApprovalComments"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["LeaveApprovalComments"].Width = 350;
            dtgConsolidatedLeaveStatement.Columns["LeaveApprovalComments"].Visible = false;
            dtgConsolidatedLeaveStatement.Columns["LeaveRejectionComments"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["LeaveRejectionComments"].Width = 350;
            dtgConsolidatedLeaveStatement.Columns["LeaveRejectionComments"].Visible = false;
            dtgConsolidatedLeaveStatement.Columns["OrderID"].ReadOnly = true;
            dtgConsolidatedLeaveStatement.Columns["OrderID"].Width = 350;
            dtgConsolidatedLeaveStatement.Columns["OrderID"].Visible = false;

            foreach (DataGridViewRow indRow in this.dtgConsolidatedLeaveStatement.Rows)
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
            if (e.RowIndex >= 0 && e.ColumnIndex == dtgConsolidatedLeaveStatement.Columns["Select"].Index)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dtgConsolidatedLeaveStatement.Rows[e.RowIndex].Cells["Select"];
                //chk.Value = !(Convert.ToBoolean(chk.Value));
                dtgConsolidatedLeaveStatement.CommitEdit(DataGridViewDataErrorContexts.Commit);
                btnSaveDetails.Enabled = false;
                foreach (DataGridViewRow indRow in this.dtgConsolidatedLeaveStatement.Rows)
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
            dtgConsolidatedLeaveStatement.CommitEdit(DataGridViewDataErrorContexts.Commit);
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
