using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public partial class frmEmployeeList : Form
    {
        clsEmployeeMaster objEmployeeMaster = new clsEmployeeMaster();
        frmEmployeeMaster frmEmployeeMaster = null;
        frmLeavesMaster frmLeavesMaster = null;
        frmUserManagement frmUserManagement = null;
        frmRolesAndResponsibilities frmRolesAndResponsibilities = null;
        frmModuleAssignment frmModuleAssignment = null;
        frmRolesProfileMaster frmRolesProfileMaster = null;
        frmAttendanceMater frmAttendanceMater = null;
        frmLeavesApproval frmLeavesApproval = null;
        frmLeavesReject frmLeaveReject = null;
        frmPayrollMaster frmPayrollInfo = null;
        frmLeaveStatement frmLeaveStatement = null;
        clsLeaveTRList objLeaveInfo = new clsLeaveTRList();
        clsUserManagement objUsersInfo = new clsUserManagement();
        clsRolesAndResponsibilities objRolesAndResponsibilities = new clsRolesAndResponsibilities();
        clsAppModule objAppModule = new clsAppModule();
        clsEmpPayroll objEmpPayroll = new clsEmpPayroll();



        public frmEmployeeList()
        {
            InitializeComponent();
        }

        public frmEmployeeList(frmEmployeeMaster frmEmployeeMaster, string SearchOptionClickedFor)
        {
            InitializeComponent();
            this.frmEmployeeMaster = frmEmployeeMaster;
            lblSearchOptionClickedFor.Text = SearchOptionClickedFor;
        }
        public frmEmployeeList(frmLeavesMaster frmLeavesMaster, string SearchOptionClickedFor)
        {
            InitializeComponent();
            this.frmLeavesMaster = frmLeavesMaster;
            lblSearchOptionClickedFor.Text = SearchOptionClickedFor;
        }

        public frmEmployeeList(frmUserManagement frmUserManagement, string SearchOptionClickedFor)
        {
            InitializeComponent();
            this.frmUserManagement = frmUserManagement;
            lblSearchOptionClickedFor.Text = SearchOptionClickedFor;
        }

        public frmEmployeeList(frmRolesAndResponsibilities frmRolesAndResponsibilities, string SearchOptionClickedFor)
        {
            InitializeComponent();
            this.frmRolesAndResponsibilities = frmRolesAndResponsibilities;
            lblSearchOptionClickedFor.Text = SearchOptionClickedFor;
        }

        public frmEmployeeList(frmModuleAssignment frmModuleAssignment, string SearchOptionClickedFor)
        {
            InitializeComponent();
            this.frmModuleAssignment = frmModuleAssignment;
            lblSearchOptionClickedFor.Text = SearchOptionClickedFor;
        }

        public frmEmployeeList(frmRolesProfileMaster frmRolesProfileMaster, string SearchOptionClickedFor)
        {
            InitializeComponent();
            this.frmRolesProfileMaster = frmRolesProfileMaster;
            lblSearchOptionClickedFor.Text = SearchOptionClickedFor;
        }

        public frmEmployeeList(frmAttendanceMater frmAttendanceMater, string SearchOptionClickedFor)
        {
            InitializeComponent();
            this.frmAttendanceMater = frmAttendanceMater;
            lblSearchOptionClickedFor.Text = SearchOptionClickedFor;
        }

        public frmEmployeeList(frmLeavesApproval frmLeavesApprval, string SearchOptionClickedFor)
        {
            InitializeComponent();
            this.frmLeavesApproval = frmLeavesApprval;
            lblSearchOptionClickedFor.Text = SearchOptionClickedFor;
        }

        public frmEmployeeList(frmLeavesReject frmLeaveReject, string SearchOptionClickedFor)
        {
            InitializeComponent();
            this.frmLeaveReject = frmLeaveReject;
            lblSearchOptionClickedFor.Text = SearchOptionClickedFor;
        }

        public frmEmployeeList(frmPayrollMaster frmPayrollMaster, string SearchOptionClickedFor)
        {
            InitializeComponent();
            this.frmPayrollInfo = frmPayrollMaster;
            lblSearchOptionClickedFor.Text = SearchOptionClickedFor;
        }

        public frmEmployeeList(frmLeaveStatement frmLeaveStment, string SearchOptionClickedFor)
        {
            InitializeComponent();
            this.frmLeaveStatement = frmLeaveStment;
            lblSearchOptionClickedFor.Text = SearchOptionClickedFor;
        }
        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEmployeeList_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'staffsyncDBDTSet.EmpMas' table. You can move, or remove it, as needed.
            //this.empMasTableAdapter.Fill(this.staffsyncDBDTSet.EmpMas);
            //// TODO: This line of code loads data into the 'staffsyncDBDTSet.EmpMasInfo' table. You can move, or remove it, as needed.
            //this.empMasInfoTableAdapter.Fill(this.staffsyncDBDTSet.EmpMasInfo);

            if (lblSearchOptionClickedFor.Text.Trim() == "listEmployees" || lblSearchOptionClickedFor.Text.Trim() == "listRepManagers" || lblSearchOptionClickedFor.Text.Trim() == "listAttendanceMasterList")
            {
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objEmployeeMaster.getCompleteEmployeesList();
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 150;
                dtgEmployeeList.Columns["EmpName"].Width = 350;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 150;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 150;
                dtgEmployeeList.Columns["ContactNumber1"].Width = 150;
                dtgEmployeeList.Columns["ContactNumber2"].Width = 150;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listEmployeeLeaveList")
            {
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objEmployeeMaster.getCompleteEmployeesList();
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 150;
                dtgEmployeeList.Columns["EmpName"].Width = 350;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 150;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 150;
                dtgEmployeeList.Columns["ContactNumber1"].Width = 150;
                dtgEmployeeList.Columns["ContactNumber2"].Width = 150;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listEmployeeLeaveApprovalRequestList")
            {
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objLeaveInfo.getPendingLeaveApprovalList();
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["LeaveTRID"].Visible = false;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listEmployeeLeaveRejectRequestList")
            {
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objLeaveInfo.getPendingLeaveApprovalList();
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["LeaveTRID"].Visible = false;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listUserModuleAssignment")
            {
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objAppModule.GetDefaultAppModuleInfo();
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listUserRolesAndResponsibilities")
            {
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objLeaveInfo.getPendingLeaveApprovalList();
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listRoleProfileManagement")
            {
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objRolesAndResponsibilities.GetDefaultRolesProfileList();
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listUserManagementList")
            {
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objUsersInfo.GetUserManagementList();
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listPayrollUsersList")
            {
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objEmployeeMaster.getCompleteEmployeesList();
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 150;
                dtgEmployeeList.Columns["EmpName"].Width = 350;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 150;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 150;
                dtgEmployeeList.Columns["ContactNumber1"].Width = 150;
                dtgEmployeeList.Columns["ContactNumber2"].Width = 150;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listPayrollUsersList")
            {
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objEmployeeMaster.getCompleteEmployeesList();
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 150;
                dtgEmployeeList.Columns["EmpName"].Width = 350;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 150;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 150;
                dtgEmployeeList.Columns["ContactNumber1"].Width = 150;
                dtgEmployeeList.Columns["ContactNumber2"].Width = 150;

            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listEmployeesPayslip")
            {
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objEmpPayroll.getAllEmployeePayslipList();
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpSalID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 150;
                dtgEmployeeList.Columns["EmpName"].Width = 350;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 150;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 150;
                dtgEmployeeList.Columns["EmpSalDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dtgEmployeeList.Columns["EmpSalDate"].Width = 150;
                dtgEmployeeList.Columns["EmpSalMonthYear"].Width = 150;
                dtgEmployeeList.Columns["OrderID"].Visible = false;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listLeaveStatement")
            {
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objEmpPayroll.getAllEmployeePayslipList();
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpSalID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 150;
                dtgEmployeeList.Columns["EmpName"].Width = 350;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 150;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 150;
                dtgEmployeeList.Columns["EmpSalDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dtgEmployeeList.Columns["EmpSalDate"].Width = 150;
                dtgEmployeeList.Columns["EmpSalMonthYear"].Width = 150;
                dtgEmployeeList.Columns["OrderID"].Visible = false;
            }
        }

        private void btnCloseMe_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtgEmployeeList_DoubleClick(object sender, EventArgs e)
        {
            if (lblSearchOptionClickedFor.Text.Trim() == "listEmployees")
            {
                this.frmEmployeeMaster.SelectedEmployeeID("listEmployees", Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["EmpID"].Value.ToString()));
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listRepManagers")
            {
                this.frmEmployeeMaster.SelectedEmployeeID("listRepManagers", Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["EmpID"].Value.ToString()));
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listEmployeeLeaveList")
            {
                this.frmLeavesMaster.SelectedEmployeeID("listEmployeeLeaveList", Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["EmpID"].Value.ToString()));
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listUserManagementList")
            {
                this.frmUserManagement.SelectedEmployeeID("listUserManagementList", Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["EmpID"].Value.ToString()));
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listUserRolesAndResponsibilities")
            {
                this.frmRolesAndResponsibilities.SelectedEmployeeID("listUserRolesAndResponsibilities", Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["EmpID"].Value.ToString()));
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listUserModuleAssignment")
            {
                this.frmModuleAssignment.SelectedEmployeeID("listUserModuleAssignment", Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["ModuleID"].Value.ToString()));
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listRoleProfileManagement")
            {
                this.frmRolesProfileMaster.SelectedEmployeeID("listRoleProfileManagement");
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listAttendanceMasterList")
            {
                this.frmAttendanceMater.SelectedEmployeeID("listAttendanceMasterList", Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["EmpID"].Value.ToString()));
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listEmployeeLeaveApprovalRequestList")
            {
                this.frmLeavesApproval.SelectedEmployeeID("listEmployeeLeaveApprovalRequestList", Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["EmpID"].Value.ToString()), Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["LeaveTRID"].Value.ToString()));
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listEmployeeLeaveRejectRequestList")
            {
                this.frmLeaveReject.SelectedEmployeeID("listEmployeeLeaveRejectRequestList", Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["EmpID"].Value.ToString()), Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["LeaveTRID"].Value.ToString()));
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listPayrollUsersList")
            {
                this.frmPayrollInfo.SelectedEmployeeID("listPayrollUsersList", Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["EmpID"].Value.ToString()), 0);
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listEmployeesPayslip")
            {
                this.frmPayrollInfo.SelectedEmployeeID("listEmployeesPayslip", Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["EmpID"].Value.ToString()), Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["EmpSalID"].Value.ToString()));
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listLeaveStatement")
            {
                this.frmLeaveStatement.SelectedEmployeeID("listLeaveStatement", Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["EmpID"].Value.ToString()));
            }

            this.Close();
        }

        private void frmEmployeeList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    dtgEmployeeList.DataSource = objEmployeeMaster.GetEmployeeList();
                }
                else
                {
                    dtgEmployeeList.DataSource = objEmployeeMaster.GetEmployeeList(txtSearch.Text.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
