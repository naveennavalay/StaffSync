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
    public partial class frmViewLeavesOutstanding : Form
    {
        clsLeaveTRList objLeaveTRList = new clsLeaveTRList();
        clsWeeklyOffInfo objWeeklyOffInfo = new clsWeeklyOffInfo();
        clsAttendanceMas objAttendanceInfo = new clsAttendanceMas();
        clsEmpLeaveEntitlementInfo objEmpLeaveEntitlementInfo = new clsEmpLeaveEntitlementInfo();
        List<WklyOffProfileDetailsInfo> lstWeeklyOffDetailsInfo = new List<WklyOffProfileDetailsInfo>();

        public frmViewLeavesOutstanding()
        {
            InitializeComponent();
        }

        public frmViewLeavesOutstanding(int txtEmployeeID, int txtLeaveMasID)
        {
            InitializeComponent();

            txtTotalUtilised.Text = "0.00";
            txtTotalLeavesAlloted.Text = "0.00";
            txtTotalBalanceLeaves.Text = "0.00";

            dtgOutstandingLeaveInfo.DataSource = null;
            dtgOutstandingLeaveInfo.DataSource = objEmpLeaveEntitlementInfo.getEmployeeLeaveEntitilementList(Convert.ToInt16(txtEmployeeID), Convert.ToInt16(txtLeaveMasID));

            dtgOutstandingLeaveInfo.Columns["LeaveEntmtID"].Visible = false;
            dtgOutstandingLeaveInfo.Columns["EmpID"].Visible = false;
            dtgOutstandingLeaveInfo.Columns["EmpID"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["LeaveMasID"].Visible = false;
            dtgOutstandingLeaveInfo.Columns["LeaveMasID"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["LeaveTypeID"].Visible = false;
            dtgOutstandingLeaveInfo.Columns["LeaveTypeID"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["LeaveTypeTitle"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["LeaveTypeTitle"].Width = 325;

            dtgOutstandingLeaveInfo.Columns["TotalLeaves"].Width = 135;
            dtgOutstandingLeaveInfo.Columns["TotalLeaves"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["TotalLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgOutstandingLeaveInfo.Columns["TotalLeaves"].DefaultCellStyle.Format = "0.00";

            dtgOutstandingLeaveInfo.Columns["BalanceLeaves"].Width = 135;
            dtgOutstandingLeaveInfo.Columns["BalanceLeaves"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["BalanceLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgOutstandingLeaveInfo.Columns["BalanceLeaves"].DefaultCellStyle.Format = "0.00";

            dtgOutstandingLeaveInfo.Columns["UsedLeaves"].Width = 135;
            dtgOutstandingLeaveInfo.Columns["UsedLeaves"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["UsedLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgOutstandingLeaveInfo.Columns["UsedLeaves"].DefaultCellStyle.Format = "0.00";
            dtgOutstandingLeaveInfo.Columns["OrderID"].Visible = false;

            decimal totalLeavesAllotted = 0;
            decimal totalBalanceLeaves = 0;
            foreach (DataGridViewRow dc in dtgOutstandingLeaveInfo.Rows)
            {
                totalLeavesAllotted = totalLeavesAllotted + Convert.ToDecimal(dc.Cells["TotalLeaves"].Value.ToString());
                totalBalanceLeaves = totalBalanceLeaves + Convert.ToDecimal(dc.Cells["BalanceLeaves"].Value.ToString());
                
                dc.Cells["UsedLeaves"].Value = Convert.ToDecimal(dc.Cells["TotalLeaves"].Value.ToString()) - Convert.ToDecimal(dc.Cells["BalanceLeaves"].Value.ToString());
                if (Convert.ToDecimal(dc.Cells["BalanceLeaves"].Value.ToString()) < Convert.ToDecimal(dc.Cells["TotalLeaves"].Value.ToString()))
                {
                    dc.Cells["BalanceLeaves"].Style.BackColor = Color.DarkRed;
                    dc.Cells["UsedLeaves"].Style.BackColor = Color.DarkRed;
                }
            }

            txtTotalLeavesAlloted.Text = Convert.ToDecimal(totalLeavesAllotted.ToString()).ToString("00.00", CultureInfo.InvariantCulture);
            txtTotalBalanceLeaves.Text = Convert.ToDecimal(totalBalanceLeaves.ToString()).ToString("00.00", CultureInfo.InvariantCulture);
            txtTotalUtilised.Text = (Convert.ToDecimal(txtTotalLeavesAlloted.Text.ToString())  - Convert.ToDecimal(txtTotalBalanceLeaves.Text.ToString())).ToString("00.00", CultureInfo.InvariantCulture);
        }



        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmViewLeavesOutstanding_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'staffsyncDBDataSet1.qryAllEmpLeavePendingStatement' table. You can move, or remove it, as needed.
            //this.qryAllEmpLeavePendingStatementTableAdapter.Fill(this.staffsyncDBDataSet1.qryAllEmpLeavePendingStatement);
            //// TODO: This line of code loads data into the 'staffsyncDBDTSet.EmpMasInfo' table. You can move, or remove it, as needed.
            //this.empMasInfoTableAdapter.Fill(this.staffsyncDBDTSet.EmpMasInfo);
            onCancelButtonClick();
            disableControls();
            clearControls();
            //FormatTheGrid();
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
            dtgOutstandingLeaveInfo.Enabled = true;
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
            dtgOutstandingLeaveInfo.DataSource = objBulkPendingLeaveApprovalList;

            dtgOutstandingLeaveInfo.Columns["Select"].ReadOnly = false;
            dtgOutstandingLeaveInfo.Columns["Select"].Visible = true;
            dtgOutstandingLeaveInfo.Columns["Select"].Width = 50;
            dtgOutstandingLeaveInfo.Columns["EmpID"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["EmpID"].Width = 50;
            dtgOutstandingLeaveInfo.Columns["EmpID"].Visible = false;
            dtgOutstandingLeaveInfo.Columns["EmpCode"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["EmpCode"].Width = 100;
            dtgOutstandingLeaveInfo.Columns["EmpCode"].Visible = true;
            dtgOutstandingLeaveInfo.Columns["EmpName"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["EmpName"].Width = 250;
            dtgOutstandingLeaveInfo.Columns["EmpName"].Visible = true;
            dtgOutstandingLeaveInfo.Columns["DesignationTitle"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["DesignationTitle"].Width = 200;
            dtgOutstandingLeaveInfo.Columns["DesignationTitle"].Visible = true;
            dtgOutstandingLeaveInfo.Columns["DepartmentTitle"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["DepartmentTitle"].Width = 200;
            dtgOutstandingLeaveInfo.Columns["DepartmentTitle"].Visible = true;
            dtgOutstandingLeaveInfo.Columns["LeaveTypeID"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["LeaveTypeID"].Width = 100;
            dtgOutstandingLeaveInfo.Columns["LeaveTypeID"].Visible = false;
            dtgOutstandingLeaveInfo.Columns["LeaveTypeTitle"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["LeaveTypeTitle"].Width = 200;
            dtgOutstandingLeaveInfo.Columns["LeaveTypeTitle"].Visible = true;
            dtgOutstandingLeaveInfo.Columns["LeaveTRID"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["LeaveTRID"].Width = 100;
            dtgOutstandingLeaveInfo.Columns["LeaveTRID"].Visible = false;
            dtgOutstandingLeaveInfo.Columns["ActualLeaveDateFrom"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["ActualLeaveDateFrom"].Width = 100;
            dtgOutstandingLeaveInfo.Columns["ActualLeaveDateFrom"].Visible = true;
            dtgOutstandingLeaveInfo.Columns["ActualLeaveDateFrom"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgOutstandingLeaveInfo.Columns["ActualLeaveDateTo"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["ActualLeaveDateTo"].Width = 100;
            dtgOutstandingLeaveInfo.Columns["ActualLeaveDateTo"].Visible = true;
            dtgOutstandingLeaveInfo.Columns["ActualLeaveDateTo"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgOutstandingLeaveInfo.Columns["LeaveDuration"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["LeaveDuration"].Width = 100;
            dtgOutstandingLeaveInfo.Columns["LeaveDuration"].Visible = true;
            dtgOutstandingLeaveInfo.Columns["LeaveComments"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["LeaveComments"].Width = 250;
            dtgOutstandingLeaveInfo.Columns["LeaveComments"].Visible = true;
            dtgOutstandingLeaveInfo.Columns["Canceled"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["Canceled"].Width = 100;
            dtgOutstandingLeaveInfo.Columns["Canceled"].Visible = true;
            dtgOutstandingLeaveInfo.Columns["CanceledDate"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["CanceledDate"].Width = 125;
            dtgOutstandingLeaveInfo.Columns["CanceledDate"].Visible = true;
            dtgOutstandingLeaveInfo.Columns["CanceledDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgOutstandingLeaveInfo.Columns["LeaveApprovalComments"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["LeaveApprovalComments"].Width = 350;
            dtgOutstandingLeaveInfo.Columns["LeaveApprovalComments"].Visible = false;
            dtgOutstandingLeaveInfo.Columns["LeaveRejectionComments"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["LeaveRejectionComments"].Width = 350;
            dtgOutstandingLeaveInfo.Columns["LeaveRejectionComments"].Visible = false;
            dtgOutstandingLeaveInfo.Columns["OrderID"].ReadOnly = true;
            dtgOutstandingLeaveInfo.Columns["OrderID"].Width = 350;
            dtgOutstandingLeaveInfo.Columns["OrderID"].Visible = false;

            foreach (DataGridViewRow indRow in this.dtgOutstandingLeaveInfo.Rows)
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
            if (e.RowIndex >= 0 && e.ColumnIndex == dtgOutstandingLeaveInfo.Columns["Select"].Index)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dtgOutstandingLeaveInfo.Rows[e.RowIndex].Cells["Select"];
                //chk.Value = !(Convert.ToBoolean(chk.Value));
                dtgOutstandingLeaveInfo.CommitEdit(DataGridViewDataErrorContexts.Commit);
                btnSaveDetails.Enabled = false;
                foreach (DataGridViewRow indRow in this.dtgOutstandingLeaveInfo.Rows)
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
            dtgOutstandingLeaveInfo.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void btnSelectUnselect_Click(object sender, EventArgs e)
        {

        }

        private void chkSelectUnselect_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}
