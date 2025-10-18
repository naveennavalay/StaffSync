using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static C1.Util.Win.Win32;

namespace StaffSync
{
    public partial class frmEmpBulkLeaveApproval : Form
    {
        DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster();
        DALStaffSync.clsUserManagement objUserManagementList = new DALStaffSync.clsUserManagement();
        DALStaffSync.clsLeaveTRList objLeaveTRList = new DALStaffSync.clsLeaveTRList();
        DALStaffSync.clsAttendanceMas objAttendanceInfo = new DALStaffSync.clsAttendanceMas();
        frmDashboard objDashboard = (frmDashboard) System.Windows.Forms.Application.OpenForms["frmDashboard"];

        public frmEmpBulkLeaveApproval()
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
            objDashboard.lblDashboardTitle.Text = "Dashboard";
            this.Close();
        }

        private void frmEmpBulkLeaveApproval_Load(object sender, EventArgs e)
        {
            onCancelButtonClick();
            clearControls();
            disableControls();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (lblActionMode.Text == "modify")
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
            errValidator.Clear();
        }

        public void onModifyButtonClick()
        {
            lblActionMode.Text = "modify";

            dtgBulkLeaveList.Columns.Clear();
            dtgBulkLeaveList.DataSource = null;
            dtgBulkLeaveList.DataSource = objLeaveTRList.getPendingLeaveApprovalList();
            FormatTable();

            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void FormatTable()        
        {
            dtgBulkLeaveList.Columns["Select"].Width = 50;
            dtgBulkLeaveList.Columns["Select"].HeaderText = "Select";
            dtgBulkLeaveList.Columns["Select"].ReadOnly = false;
            dtgBulkLeaveList.Columns["EmpID"].Visible = false;
            dtgBulkLeaveList.Columns["LeaveTRID"].Visible = false;
            dtgBulkLeaveList.Columns["LeaveTypeID"].Visible = false;
            dtgBulkLeaveList.Columns["EmpName"].ReadOnly = true;
            dtgBulkLeaveList.Columns["EmpName"].Width = 200;
            dtgBulkLeaveList.Columns["DesignationTitle"].Width = 150;
            dtgBulkLeaveList.Columns["DesignationTitle"].ReadOnly = true;
            dtgBulkLeaveList.Columns["DepartmentTitle"].Width = 150;
            dtgBulkLeaveList.Columns["DepartmentTitle"].ReadOnly = true;
            dtgBulkLeaveList.Columns["LeaveTypeTitle"].Width = 150;
            dtgBulkLeaveList.Columns["LeaveTypeTitle"].ReadOnly = true;
            dtgBulkLeaveList.Columns["LeaveAppliedDate"].Width = 100;
            dtgBulkLeaveList.Columns["LeaveAppliedDate"].ReadOnly = true;
            dtgBulkLeaveList.Columns["LeaveAppliedDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgBulkLeaveList.Columns["LeaveComments"].Width = 250;
            dtgBulkLeaveList.Columns["LeaveComments"].ReadOnly = true;
            dtgBulkLeaveList.Columns["ActualLeaveDateFrom"].ReadOnly = true;
            dtgBulkLeaveList.Columns["ActualLeaveDateFrom"].Width = 100;
            dtgBulkLeaveList.Columns["ActualLeaveDateFrom"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgBulkLeaveList.Columns["ActualLeaveDateTo"].ReadOnly = true;
            dtgBulkLeaveList.Columns["ActualLeaveDateTo"].Width = 100;
            dtgBulkLeaveList.Columns["ActualLeaveDateTo"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgBulkLeaveList.Columns["LeaveDuration"].ReadOnly = true;
        }

        public void onSaveButtonClick()
        {
            lblActionMode.Text = "";
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnCancel.Enabled = true;
        }

        public void onCancelButtonClick()
        {
            lblActionMode.Text = "";
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnCancel.Enabled = true;
        }

        public void clearControls()
        {
            dtgBulkLeaveList.DataSource = null;
            //FormatTable();
        }

        public void enableControls()
        {
            dtgBulkLeaveList.Enabled = true;
        }

        public void disableControls()
        {
            dtgBulkLeaveList.Enabled = false;
        }

        private void btnModifyDetails_Click(object sender, EventArgs e)
        {
            onModifyButtonClick();
            enableControls();
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            int insertNewRecordCount = 0;
            if (validateValues())
            {
                int employeeLeaveTRID = 0;
                foreach (DataGridViewRow dc in dtgBulkLeaveList.Rows)
                {
                    if (Convert.ToBoolean(dc.Cells["Select"].Value.ToString()) == true)
                    {
                        employeeLeaveTRID = objLeaveTRList.ApproveLeave(Convert.ToInt16(dc.Cells["LeaveTRID"].Value.ToString()), Convert.ToInt16(dc.Cells["EmpID"].Value.ToString()), "Approved by Bulk Leave Approval", clsCurrentUser.UserID);

                        int lblLeaveMasID = Convert.ToInt16(objLeaveTRList.getMaxLeaveMasID(Convert.ToInt16(dc.Cells["EmpID"].Value.ToString())).ToString());
                        
                        decimal txtAvailableLeave = Convert.ToDecimal(objLeaveTRList.getSpecificLeaveTypeBalance(Convert.ToInt16(lblLeaveMasID), Convert.ToInt16(dc.Cells["LeaveTypeID"].Value.ToString())).ToString());

                        decimal txtBalanceLeave = Convert.ToDecimal(txtAvailableLeave) - Convert.ToDecimal(dc.Cells["LeaveDuration"].Value.ToString());

                        objLeaveTRList.UpdateEmployeeLeaveBalance(Convert.ToInt16(lblLeaveMasID.ToString()), Convert.ToInt16(dc.Cells["LeaveTypeID"].Value.ToString()), Convert.ToDecimal(txtAvailableLeave), Convert.ToDecimal(txtBalanceLeave), DateTime.Now);
                        objAttendanceInfo.InsertDailyAttendance(Convert.ToInt16(dc.Cells["EmpID"].Value.ToString()), Convert.ToDateTime(dc.Cells["ActualLeaveDateFrom"].Value.ToString()), "Leave", Convert.ToInt16(dc.Cells["LeaveTRID"].Value.ToString()));
                    }
                }
            }
            onSaveButtonClick();
            clearControls();
            disableControls();
        }

        private bool validateValues()
        {
            bool validateStatus = true;

            return validateStatus;
        }

        private void btnReportingManagerSearch_Click(object sender, EventArgs e)
        {
            //frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listBulkLeaveApproval");
            //frmEmployeeList.ShowDialog();
        }

        private void btnCloseMe_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SelectedEmployeeID(string SearchOptionSelectedForm)
        {
            if (SearchOptionSelectedForm == "listRoleProfileManagement")
            {
                //RefreshUserSpecificModulesList(Convert.ToInt16(lblReportingManagerID.Text.ToString()));
            }
        }

        private void frmEmpBulkLeaveApproval_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (lblActionMode.Text != "")
                {
                    if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }
                objDashboard.lblDashboardTitle.Text = "Dashboard";
                this.Close();
            }
        }
    }
}
