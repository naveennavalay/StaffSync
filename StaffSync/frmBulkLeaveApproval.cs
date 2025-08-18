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
    public partial class frmBulkLeaveApproval : Form
    {
        clsLeaveTRList objLeaveTRList = new clsLeaveTRList();
        clsWeeklyOffInfo objWeeklyOffInfo = new clsWeeklyOffInfo();
        clsAttendanceMas objAttendanceInfo = new clsAttendanceMas();
        List<WklyOffProfileDetailsInfo> lstWeeklyOffDetailsInfo = new List<WklyOffProfileDetailsInfo>();

        public frmBulkLeaveApproval()
        {
            InitializeComponent();
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            if (lblActionMode.Text != "")
            {
                if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            this.Close();
        }

        private void frmBulkLeaveApproval_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'staffsyncDBDataSet1.qryAllEmpLeavePendingStatement' table. You can move, or remove it, as needed.
            //this.qryAllEmpLeavePendingStatementTableAdapter.Fill(this.staffsyncDBDataSet1.qryAllEmpLeavePendingStatement);
            //// TODO: This line of code loads data into the 'staffsyncDBDTSet.EmpMasInfo' table. You can move, or remove it, as needed.
            //this.empMasInfoTableAdapter.Fill(this.staffsyncDBDTSet.EmpMasInfo);
            onCancelButtonClick();
            disableControls();
            clearControls();
            FormatTheGrid();

            if (this.dtgBulkLeaveApproval.Rows.Count > 0)
            {
                chkSelectUnselect.Enabled = true;
                btnSaveDetails.Enabled = true;
            }
            else
            {
                chkSelectUnselect.Enabled = false;
                btnSaveDetails.Enabled = false;
            }

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
            lblActionMode.Text = "add";
            onGenerateButtonClick();
            clearControls();
            enableControls();
            errValidator.Clear();
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            if (this.dtgBulkLeaveApproval.Rows.Count > 0)
            {
                if (MessageBox.Show("You are about to execute Bulk Leave Approval. \nAre you sure to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            int WeeklyOffDayID = 0;
            int employeeLeaveTRID = 0;
            foreach (DataGridViewRow indRow in this.dtgBulkLeaveApproval.Rows)
            {
                if (Convert.ToBoolean(indRow.Cells["Select"].Value.ToString()) == true)
                {
                    employeeLeaveTRID = objLeaveTRList.ApproveLeave(Convert.ToInt16(indRow.Cells["LeaveTRID"].Value.ToString()), Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), "Request Approved via Bulk Approval", clsCurrentUser.UserID);

                    int lblLeaveMasID = Convert.ToInt16(objLeaveTRList.getMaxLeaveMasID(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString())).ToString());

                    decimal txtAvailableLeave = 0;
                    decimal txtBalanceLeave = 0;

                    if (Convert.ToBoolean(indRow.Cells["Canceled"].Value.ToString()) == false)
                    {
                        txtAvailableLeave = Convert.ToDecimal(objLeaveTRList.getBalanceLeave(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString())));
                        txtBalanceLeave = Convert.ToDecimal(txtAvailableLeave);
                        objLeaveTRList.UpdateEmployeeLeaveBalance(Convert.ToInt16(lblLeaveMasID.ToString()), Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), Convert.ToDecimal(txtAvailableLeave), Convert.ToDecimal(txtBalanceLeave), DateTime.Now);
                        objAttendanceInfo.InsertDailyAttendance(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), Convert.ToDateTime(indRow.Cells["ActualLeaveDateFrom"].Value.ToString()), "Leave", Convert.ToInt16(indRow.Cells["LeaveTRID"].Value.ToString()));
                    }
                    else if (Convert.ToBoolean(indRow.Cells["Canceled"].Value.ToString()) == true)
                    {
                        txtAvailableLeave = Convert.ToDecimal(objLeaveTRList.getSpecificLeaveTypeBalance(Convert.ToInt16(lblLeaveMasID), Convert.ToInt16(indRow.Cells["LeaveTypeID"].Value.ToString())).ToString());
                        txtBalanceLeave = Convert.ToDecimal(txtAvailableLeave) + Convert.ToDecimal(indRow.Cells["LeaveDuration"].Value.ToString());
                        objLeaveTRList.UpdateEmployeeLeaveBalance(Convert.ToInt16(lblLeaveMasID.ToString()), Convert.ToInt16(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString())), Convert.ToDecimal(txtAvailableLeave.ToString()), Convert.ToDecimal(txtBalanceLeave.ToString()), DateTime.Now);
                        objAttendanceInfo.InsertDailyAttendance(Convert.ToInt16(indRow.Cells["EmpID"].Value.ToString()), Convert.ToDateTime(indRow.Cells["ActualLeaveDateFrom"].Value.ToString()), "Leave", Convert.ToInt16(indRow.Cells["LeaveTRID"].Value.ToString()));
                    }
                }
            }
            MessageBox.Show("Details updated successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);

            onSaveButtonClick();
            disableControls();
            clearControls();
            FormatTheGrid();
            errValidator.Clear();          
        }

        public void clearControls()
        {
            txtLeaveApprovalDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
        }

        public void enableControls()
        {
            txtLeaveApprovalDate.Enabled = true;
            dtgBulkLeaveApproval.Enabled = true;
        }

        public void disableControls()
        {
            //txtLeaveApprovalDate.Enabled = false;
            //dtgBulkLeaveApproval.Enabled = false;
        }

        public void onGenerateButtonClick()
        {
            lblActionMode.Text = "add";
            lblWeeklyOffID.Text = "";
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = false;
            btnCancel.Enabled = true;

        }

        public void onModifyButtonClick()
        {
            lblActionMode.Text = "modify";
            lblWeeklyOffID.Text = "";
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = false;
            btnCancel.Enabled = true;
        }

        public void onRemoveButtonClick()
        {
            lblActionMode.Text = "remove";
            lblWeeklyOffID.Text = "";
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onSaveButtonClick()
        {
            lblActionMode.Text = "";
            lblWeeklyOffID.Text = "";
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onCancelButtonClick()
        {
            lblActionMode.Text = "";
            lblWeeklyOffID.Text = "";
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void displaySelectedValuesOnUI(WklyOffProfileMasInfo WklyOffProfileMasInfoModel)
        {
            lblWeeklyOffID.Text = WklyOffProfileMasInfoModel.WklyOffMasID.ToString();
            FormatTheGrid();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (lblActionMode.Text == "add")
            {
                if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            else if (lblActionMode.Text == "modify")
            {
                if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            else if (lblActionMode.Text == "delete")
            {
                if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            lblActionMode.Text = "";
            onCancelButtonClick();
            disableControls();
            clearControls();
            FormatTheGrid();
            errValidator.Clear();

        }

        private void btnModifyDetails_Click(object sender, EventArgs e)
        {
            lblActionMode.Text = "modify"; 
            onModifyButtonClick();
            clearControls();
            enableControls();
            errValidator.Clear();
        }

        private void btnRemoveDetails_Click(object sender, EventArgs e)
        {
            if(lblActionMode.Text == "" || lblActionMode.Text == "remove")
            {
                lblActionMode.Text = "remove";
                onRemoveButtonClick();
                clearControls();
                enableControls();
            }
            else if (lblActionMode.Text == "delete")
            {
                if (MessageBox.Show("The selected record will be deleted. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MessageBox.Show("Details deleted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                onCancelButtonClick();
                disableControls();
                clearControls();
                lblActionMode.Text = "";
                errValidator.Clear();
            }
        }

        private void FormatTheGrid()
        {
            if (txtLeaveApprovalDate.Text.ToString().Trim() == "")
                return;

            List<BulkPendingLeaveApproval> objBulkPendingLeaveApprovalList = objLeaveTRList.getBulkPendingLeaveApprovalList();
            dtgBulkLeaveApproval.DataSource = objBulkPendingLeaveApprovalList;

            dtgBulkLeaveApproval.Columns["Select"].ReadOnly = false;
            dtgBulkLeaveApproval.Columns["Select"].Visible = true;
            dtgBulkLeaveApproval.Columns["Select"].Width = 50;
            dtgBulkLeaveApproval.Columns["EmpID"].ReadOnly = true;
            dtgBulkLeaveApproval.Columns["EmpID"].Width = 50;
            dtgBulkLeaveApproval.Columns["EmpID"].Visible = false;
            dtgBulkLeaveApproval.Columns["EmpCode"].ReadOnly = true;
            dtgBulkLeaveApproval.Columns["EmpCode"].Width = 100;
            dtgBulkLeaveApproval.Columns["EmpCode"].Visible = true;
            dtgBulkLeaveApproval.Columns["EmpName"].ReadOnly = true;
            dtgBulkLeaveApproval.Columns["EmpName"].Width = 250;
            dtgBulkLeaveApproval.Columns["EmpName"].Visible = true;
            dtgBulkLeaveApproval.Columns["DesignationTitle"].ReadOnly = true;
            dtgBulkLeaveApproval.Columns["DesignationTitle"].Width = 200;
            dtgBulkLeaveApproval.Columns["DesignationTitle"].Visible = true;
            dtgBulkLeaveApproval.Columns["DepartmentTitle"].ReadOnly = true;
            dtgBulkLeaveApproval.Columns["DepartmentTitle"].Width = 200;
            dtgBulkLeaveApproval.Columns["DepartmentTitle"].Visible = true;
            dtgBulkLeaveApproval.Columns["LeaveTypeID"].ReadOnly = true;
            dtgBulkLeaveApproval.Columns["LeaveTypeID"].Width = 100;
            dtgBulkLeaveApproval.Columns["LeaveTypeID"].Visible = false;
            dtgBulkLeaveApproval.Columns["LeaveTypeTitle"].ReadOnly = true;
            dtgBulkLeaveApproval.Columns["LeaveTypeTitle"].Width = 200;
            dtgBulkLeaveApproval.Columns["LeaveTypeTitle"].Visible = true;
            dtgBulkLeaveApproval.Columns["LeaveTRID"].ReadOnly = true;
            dtgBulkLeaveApproval.Columns["LeaveTRID"].Width = 100;
            dtgBulkLeaveApproval.Columns["LeaveTRID"].Visible = false;
            dtgBulkLeaveApproval.Columns["ActualLeaveDateFrom"].ReadOnly = true;
            dtgBulkLeaveApproval.Columns["ActualLeaveDateFrom"].Width = 100;
            dtgBulkLeaveApproval.Columns["ActualLeaveDateFrom"].Visible = true;
            dtgBulkLeaveApproval.Columns["ActualLeaveDateFrom"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgBulkLeaveApproval.Columns["ActualLeaveDateTo"].ReadOnly = true;
            dtgBulkLeaveApproval.Columns["ActualLeaveDateTo"].Width = 100;
            dtgBulkLeaveApproval.Columns["ActualLeaveDateTo"].Visible = true;
            dtgBulkLeaveApproval.Columns["ActualLeaveDateTo"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgBulkLeaveApproval.Columns["LeaveDuration"].ReadOnly = true;
            dtgBulkLeaveApproval.Columns["LeaveDuration"].Width = 100;
            dtgBulkLeaveApproval.Columns["LeaveDuration"].Visible = true;
            dtgBulkLeaveApproval.Columns["LeaveComments"].ReadOnly = true;
            dtgBulkLeaveApproval.Columns["LeaveComments"].Width = 250;
            dtgBulkLeaveApproval.Columns["LeaveComments"].Visible = true;
            dtgBulkLeaveApproval.Columns["Canceled"].ReadOnly = true;
            dtgBulkLeaveApproval.Columns["Canceled"].Width = 100;
            dtgBulkLeaveApproval.Columns["Canceled"].Visible = true;
            dtgBulkLeaveApproval.Columns["CanceledDate"].ReadOnly = true;
            dtgBulkLeaveApproval.Columns["CanceledDate"].Width = 125;
            dtgBulkLeaveApproval.Columns["CanceledDate"].Visible = true;
            dtgBulkLeaveApproval.Columns["CanceledDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgBulkLeaveApproval.Columns["LeaveApprovalComments"].ReadOnly = true;
            dtgBulkLeaveApproval.Columns["LeaveApprovalComments"].Width = 350;
            dtgBulkLeaveApproval.Columns["LeaveApprovalComments"].Visible = false;
            dtgBulkLeaveApproval.Columns["LeaveRejectionComments"].ReadOnly = true;
            dtgBulkLeaveApproval.Columns["LeaveRejectionComments"].Width = 350;
            dtgBulkLeaveApproval.Columns["LeaveRejectionComments"].Visible = false;
            dtgBulkLeaveApproval.Columns["OrderID"].ReadOnly = true;
            dtgBulkLeaveApproval.Columns["OrderID"].Width = 350;
            dtgBulkLeaveApproval.Columns["OrderID"].Visible = false;

            foreach (DataGridViewRow indRow in this.dtgBulkLeaveApproval.Rows)
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
            if (txtLeaveApprovalDate.Text.ToString().Trim() == "")
                return;
            FormatTheGrid();
        }

        private void dtgBulkLeaveApproval_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dtgBulkLeaveApproval.Columns["Select"].Index)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dtgBulkLeaveApproval.Rows[e.RowIndex].Cells["Select"];
                //chk.Value = !(Convert.ToBoolean(chk.Value));
                dtgBulkLeaveApproval.CommitEdit(DataGridViewDataErrorContexts.Commit);
                btnSaveDetails.Enabled = false;
                foreach (DataGridViewRow indRow in this.dtgBulkLeaveApproval.Rows)
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
            dtgBulkLeaveApproval.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void btnSelectUnselect_Click(object sender, EventArgs e)
        {

        }

        private void chkSelectUnselect_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectUnselect.Text == "Select All")
            {
                foreach (DataGridViewRow indRow in this.dtgBulkLeaveApproval.Rows)
                {
                    indRow.Cells["Select"].Value = true;
                }
                chkSelectUnselect.Text = "Unselect All";
                chkSelectUnselect.Refresh();
            }
            else if (chkSelectUnselect.Text == "Unselect All")
            {
                foreach (DataGridViewRow indRow in this.dtgBulkLeaveApproval.Rows)
                {
                    indRow.Cells["Select"].Value = false;
                }
                chkSelectUnselect.Text = "Select All";
                chkSelectUnselect.Refresh();
            }
        }
    }
}
