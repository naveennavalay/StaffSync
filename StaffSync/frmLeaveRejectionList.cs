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
    public partial class frmLeaveRejectionList : Form
    {
        clsLeaveTRList objLeaveTRList = new clsLeaveTRList();
        clsWeeklyOffInfo objWeeklyOffInfo = new clsWeeklyOffInfo();
        clsAttendanceMas objAttendanceInfo = new clsAttendanceMas();
        clsEmpLeaveEntitlementInfo objEmpLeaveEntitlementInfo = new clsEmpLeaveEntitlementInfo();
        List<WklyOffProfileDetailsInfo> lstWeeklyOffDetailsInfo = new List<WklyOffProfileDetailsInfo>();

        public frmLeaveRejectionList()
        {
            InitializeComponent();
        }

        public frmLeaveRejectionList(int txtEmployeeID, int txtLeaveMasID)
        {
            InitializeComponent();

            dtgRejectionLeaveList.DataSource = null;
            dtgRejectionLeaveList.DataSource = objEmpLeaveEntitlementInfo.getEmployeeLeaveEntitilementList(Convert.ToInt16(txtEmployeeID), Convert.ToInt16(txtLeaveMasID));

            dtgRejectionLeaveList.Columns["LeaveEntmtID"].Visible = false;
            dtgRejectionLeaveList.Columns["EmpID"].Visible = false;
            dtgRejectionLeaveList.Columns["EmpID"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["LeaveMasID"].Visible = false;
            dtgRejectionLeaveList.Columns["LeaveMasID"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["LeaveTypeID"].Visible = false;
            dtgRejectionLeaveList.Columns["LeaveTypeID"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["LeaveTypeTitle"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["LeaveTypeTitle"].Width = 325;

            dtgRejectionLeaveList.Columns["TotalLeaves"].Width = 135;
            dtgRejectionLeaveList.Columns["TotalLeaves"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["TotalLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgRejectionLeaveList.Columns["TotalLeaves"].DefaultCellStyle.Format = "0.00";

            dtgRejectionLeaveList.Columns["BalanceLeaves"].Width = 135;
            dtgRejectionLeaveList.Columns["BalanceLeaves"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["BalanceLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgRejectionLeaveList.Columns["BalanceLeaves"].DefaultCellStyle.Format = "0.00";

            dtgRejectionLeaveList.Columns["UsedLeaves"].Width = 135;
            dtgRejectionLeaveList.Columns["UsedLeaves"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["UsedLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgRejectionLeaveList.Columns["UsedLeaves"].DefaultCellStyle.Format = "0.00";
            dtgRejectionLeaveList.Columns["OrderID"].Visible = false;         
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLeaveRejectionList_Load(object sender, EventArgs e)
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
            dtgRejectionLeaveList.Enabled = true;
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
            dtgRejectionLeaveList.DataSource = objBulkPendingLeaveApprovalList;

            dtgRejectionLeaveList.Columns["Select"].ReadOnly = false;
            dtgRejectionLeaveList.Columns["Select"].Visible = true;
            dtgRejectionLeaveList.Columns["Select"].Width = 50;
            dtgRejectionLeaveList.Columns["EmpID"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["EmpID"].Width = 50;
            dtgRejectionLeaveList.Columns["EmpID"].Visible = false;
            dtgRejectionLeaveList.Columns["EmpCode"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["EmpCode"].Width = 100;
            dtgRejectionLeaveList.Columns["EmpCode"].Visible = true;
            dtgRejectionLeaveList.Columns["EmpName"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["EmpName"].Width = 250;
            dtgRejectionLeaveList.Columns["EmpName"].Visible = true;
            dtgRejectionLeaveList.Columns["DesignationTitle"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["DesignationTitle"].Width = 200;
            dtgRejectionLeaveList.Columns["DesignationTitle"].Visible = true;
            dtgRejectionLeaveList.Columns["DepartmentTitle"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["DepartmentTitle"].Width = 200;
            dtgRejectionLeaveList.Columns["DepartmentTitle"].Visible = true;
            dtgRejectionLeaveList.Columns["LeaveTypeID"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["LeaveTypeID"].Width = 100;
            dtgRejectionLeaveList.Columns["LeaveTypeID"].Visible = false;
            dtgRejectionLeaveList.Columns["LeaveTypeTitle"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["LeaveTypeTitle"].Width = 200;
            dtgRejectionLeaveList.Columns["LeaveTypeTitle"].Visible = true;
            dtgRejectionLeaveList.Columns["LeaveTRID"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["LeaveTRID"].Width = 100;
            dtgRejectionLeaveList.Columns["LeaveTRID"].Visible = false;
            dtgRejectionLeaveList.Columns["ActualLeaveDateFrom"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["ActualLeaveDateFrom"].Width = 100;
            dtgRejectionLeaveList.Columns["ActualLeaveDateFrom"].Visible = true;
            dtgRejectionLeaveList.Columns["ActualLeaveDateFrom"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgRejectionLeaveList.Columns["ActualLeaveDateTo"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["ActualLeaveDateTo"].Width = 100;
            dtgRejectionLeaveList.Columns["ActualLeaveDateTo"].Visible = true;
            dtgRejectionLeaveList.Columns["ActualLeaveDateTo"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgRejectionLeaveList.Columns["LeaveDuration"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["LeaveDuration"].Width = 100;
            dtgRejectionLeaveList.Columns["LeaveDuration"].Visible = true;
            dtgRejectionLeaveList.Columns["LeaveComments"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["LeaveComments"].Width = 250;
            dtgRejectionLeaveList.Columns["LeaveComments"].Visible = true;
            dtgRejectionLeaveList.Columns["Canceled"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["Canceled"].Width = 100;
            dtgRejectionLeaveList.Columns["Canceled"].Visible = true;
            dtgRejectionLeaveList.Columns["CanceledDate"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["CanceledDate"].Width = 125;
            dtgRejectionLeaveList.Columns["CanceledDate"].Visible = true;
            dtgRejectionLeaveList.Columns["CanceledDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgRejectionLeaveList.Columns["LeaveApprovalComments"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["LeaveApprovalComments"].Width = 350;
            dtgRejectionLeaveList.Columns["LeaveApprovalComments"].Visible = false;
            dtgRejectionLeaveList.Columns["LeaveRejectionComments"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["LeaveRejectionComments"].Width = 350;
            dtgRejectionLeaveList.Columns["LeaveRejectionComments"].Visible = false;
            dtgRejectionLeaveList.Columns["OrderID"].ReadOnly = true;
            dtgRejectionLeaveList.Columns["OrderID"].Width = 350;
            dtgRejectionLeaveList.Columns["OrderID"].Visible = false;

            foreach (DataGridViewRow indRow in this.dtgRejectionLeaveList.Rows)
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
            if (e.RowIndex >= 0 && e.ColumnIndex == dtgRejectionLeaveList.Columns["Select"].Index)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dtgRejectionLeaveList.Rows[e.RowIndex].Cells["Select"];
                //chk.Value = !(Convert.ToBoolean(chk.Value));
                dtgRejectionLeaveList.CommitEdit(DataGridViewDataErrorContexts.Commit);
                btnSaveDetails.Enabled = false;
                foreach (DataGridViewRow indRow in this.dtgRejectionLeaveList.Rows)
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
            dtgRejectionLeaveList.CommitEdit(DataGridViewDataErrorContexts.Commit);
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
