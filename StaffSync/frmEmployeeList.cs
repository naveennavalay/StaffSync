using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using ModelStaffSync;
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
        DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster();
        frmEmployeeMaster frmEmployeeMaster = null;
        frmSSEmployeeMaster frmSSEmployeeMaster = null;
        frmDailyAttendanceProcess frmDailyAttendanceProces = null;
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
        frmUpdateCurrentUserInfo frmUpdateCurrentUserInfo = null;
        frmCurrentUserLeaveMaster frmCurrentUserLeaveMaster = null;
        frmEmpLeaveEntitlement frmEmpLeaveEntitlement = null;
        frmLeaveStatements frmLeaveStatements = null;
        frmEmpAdvanceRequest frmEmpAdvancRequest = null;
        frmDashboard frmDashboard = null;
        frmPayrollConfiguration frmPayrollConfiguration = null;
        DALStaffSync.clsLeaveTRList objLeaveInfo = new DALStaffSync.clsLeaveTRList();
        DALStaffSync.clsDashboardWidgetData objDashboard = new DALStaffSync.clsDashboardWidgetData();
        DALStaffSync.clsUserManagement objUsersInfo = new DALStaffSync.clsUserManagement();
        DALStaffSync.clsRolesAndResponsibilities objRolesAndResponsibilities = new DALStaffSync.clsRolesAndResponsibilities();
        DALStaffSync.clsAppModule objAppModule = new DALStaffSync.clsAppModule();
        DALStaffSync.clsEmpPayroll objEmpPayroll = new DALStaffSync.clsEmpPayroll();

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
        public frmEmployeeList(frmSSEmployeeMaster frmSSEmpMaster, string SearchOptionClickedFor)
        {
            InitializeComponent();
            this.frmSSEmployeeMaster = frmSSEmpMaster;
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

        public frmEmployeeList(frmUpdateCurrentUserInfo frmUpdateCurrntUserInfo, string SearchOptionClickedFor)
        {
            InitializeComponent();
            this.frmUpdateCurrentUserInfo = frmUpdateCurrntUserInfo;
            lblSearchOptionClickedFor.Text = SearchOptionClickedFor;
        }

        public frmEmployeeList(frmCurrentUserLeaveMaster frmCurrentUserLeaveMster, string SearchOptionClickedFor)
        {
            InitializeComponent();
            this.frmCurrentUserLeaveMaster = frmCurrentUserLeaveMster;
            lblSearchOptionClickedFor.Text = SearchOptionClickedFor;
        }

        public frmEmployeeList(frmEmpLeaveEntitlement frmEmpLeaveEntitlment, string SearchOptionClickedFor)
        {
            InitializeComponent();
            this.frmEmpLeaveEntitlement = frmEmpLeaveEntitlment;
            lblSearchOptionClickedFor.Text = SearchOptionClickedFor;
        }

        public frmEmployeeList(frmLeaveStatements frmLeaveStatments, string SearchOptionClickedFor)
        {
            InitializeComponent();
            this.frmLeaveStatements = frmLeaveStatments;
            lblSearchOptionClickedFor.Text = SearchOptionClickedFor;
        }

        public frmEmployeeList(frmEmpAdvanceRequest frmEmpAdvanceRequest, string SearchOptionClickedFor)
        {
            InitializeComponent();
            this.frmEmpAdvancRequest = frmEmpAdvanceRequest;
            lblSearchOptionClickedFor.Text = SearchOptionClickedFor;
        }

        public frmEmployeeList(frmPayrollConfiguration frmPayrollConfig, string SearchOptionClickedFor)
        {
            InitializeComponent();
            this.frmPayrollConfiguration = frmPayrollConfig;
            lblSearchOptionClickedFor.Text = SearchOptionClickedFor;
        }

        public frmEmployeeList(frmDashboard frmDashbard, string SearchOptionClickedFor, int txtClientID, int txtFinYearID, DateTime filterDate)
        {
            InitializeComponent();
            this.frmDashboard = frmDashbard;
            lblSearchOptionClickedFor.Text = SearchOptionClickedFor;
            lblClientID.Text = txtClientID.ToString();
            lblFinYearID.Text = txtFinYearID.ToString();
            lblFilterDate.Text = filterDate.ToString("dd-MMM-yyyy");
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
                dtgEmployeeList.Columns["EmpName"].Width = 250;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 250;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 250;
                dtgEmployeeList.Columns["ContactNumber1"].Width = 250;
                dtgEmployeeList.Columns["ContactNumber2"].Width = 250;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listSSEmployees")
            {
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objEmployeeMaster.getMyEmployeeInformation(CurrentUser.EmpID);
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 150;
                dtgEmployeeList.Columns["EmpName"].Width = 250;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 250;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 250;
                dtgEmployeeList.Columns["ContactNumber1"].Width = 250;
                dtgEmployeeList.Columns["ContactNumber2"].Width = 250;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listEmployeeLeaveList")
            {
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objEmployeeMaster.getCompleteEmployeesList();
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 150;
                dtgEmployeeList.Columns["EmpName"].Width = 250;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 250;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 250;
                dtgEmployeeList.Columns["ContactNumber1"].Width = 250;
                dtgEmployeeList.Columns["ContactNumber2"].Width = 250;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listEmployeeLeaveApprovalRequestList")
            {
                this.Text = "Leave Request List";
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objLeaveInfo.getPendingLeaveApprovalList();
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["LeaveTRID"].Visible = false;
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 100;
                dtgEmployeeList.Columns["EmpName"].Width = 200;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 200;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 200;
                dtgEmployeeList.Columns["LeaveTypeID"].Visible = false; 
                dtgEmployeeList.Columns["LeaveTypeTitle"].Width = 150;
                dtgEmployeeList.Columns["LeaveAppliedDate"].Visible = false;
                dtgEmployeeList.Columns["LeaveAppliedDate"].Width = 100;
                dtgEmployeeList.Columns["LeaveAppliedDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dtgEmployeeList.Columns["LeaveDuration"].Width = 100;
                dtgEmployeeList.Columns["ActualLeaveDateFrom"].Width = 100;
                dtgEmployeeList.Columns["ActualLeaveDateFrom"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dtgEmployeeList.Columns["ActualLeaveDateTo"].Width = 100;
                dtgEmployeeList.Columns["ActualLeaveDateTo"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dtgEmployeeList.Columns["LeaveComments"].Width = 250;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listEmployeeLeaveRejectRequestList")
            {
                this.Text = "Leave Request List";
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objLeaveInfo.getPendingLeaveApprovalList();
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["LeaveTRID"].Visible = false;
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 100;
                dtgEmployeeList.Columns["EmpName"].Width = 200;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 200;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 200;
                dtgEmployeeList.Columns["LeaveTypeID"].Visible = false;
                dtgEmployeeList.Columns["LeaveTypeTitle"].Width = 150;
                dtgEmployeeList.Columns["LeaveAppliedDate"].Visible = false;
                dtgEmployeeList.Columns["LeaveAppliedDate"].Width = 100;
                dtgEmployeeList.Columns["LeaveAppliedDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dtgEmployeeList.Columns["ActualLeaveDateFrom"].Width = 100;
                dtgEmployeeList.Columns["ActualLeaveDateFrom"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dtgEmployeeList.Columns["ActualLeaveDateTo"].Width = 100;
                dtgEmployeeList.Columns["ActualLeaveDateTo"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dtgEmployeeList.Columns["LeaveDuration"].Width = 100;
                dtgEmployeeList.Columns["LeaveComments"].Width = 250;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listUserModuleAssignment")
            {
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objUsersInfo.GetUserManagementList();
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 150;
                dtgEmployeeList.Columns["EmpName"].Width = 350;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 300;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 300;
                dtgEmployeeList.Columns["UserID"].Visible = false;
                dtgEmployeeList.Columns["IsActive"].Visible = false;
                dtgEmployeeList.Columns["IsDeleted"].Visible = false;
                dtgEmployeeList.Columns["IsLocked"].Visible = false;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listUserRolesAndResponsibilities")
            {
                dtgEmployeeList.DataSource = null; 
                dtgEmployeeList.DataSource = objUsersInfo.GetUserManagementList();
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 150;
                dtgEmployeeList.Columns["EmpName"].Width = 350;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 300;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 300;
                dtgEmployeeList.Columns["UserID"].Visible = false;
                dtgEmployeeList.Columns["IsActive"].Visible = false;
                dtgEmployeeList.Columns["IsDeleted"].Visible = false;
                dtgEmployeeList.Columns["IsLocked"].Visible = false;
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
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 150;
                dtgEmployeeList.Columns["EmpName"].Width = 350;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 300;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 300;
                dtgEmployeeList.Columns["UserID"].Visible = false;
                dtgEmployeeList.Columns["IsActive"].Visible = false;
                dtgEmployeeList.Columns["IsDeleted"].Visible = false;
                dtgEmployeeList.Columns["IsLocked"].Visible = false;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listPayrollUsersList")
            {
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objEmployeeMaster.getCompleteEmployeesList();
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 150;
                dtgEmployeeList.Columns["EmpName"].Width = 250;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 250;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 250;
                dtgEmployeeList.Columns["ContactNumber1"].Width = 250;
                dtgEmployeeList.Columns["ContactNumber2"].Width = 250;
                dtgEmployeeList.Columns["StateID"].Visible = false;
                dtgEmployeeList.Columns["SexID"].Visible = false;
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
                dtgEmployeeList.Columns["StateID"].Visible = false;
                dtgEmployeeList.Columns["SexID"].Visible = false;

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
                dtgEmployeeList.DataSource = objEmployeeMaster.getMyEmployeeInformation(CurrentUser.EmpID);
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 150;
                dtgEmployeeList.Columns["EmpName"].Width = 350;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 150;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 150;
                dtgEmployeeList.Columns["DOJ"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dtgEmployeeList.Columns["DOB"].Visible = false;
                dtgEmployeeList.Columns["Address1"].Visible = false;
                dtgEmployeeList.Columns["Address2"].Visible = false;
                dtgEmployeeList.Columns["Area"].Visible = false;
                dtgEmployeeList.Columns["City"].Visible = false;
                dtgEmployeeList.Columns["StateTitle"].Visible = false;
                dtgEmployeeList.Columns["PIN"].Visible = false;
                dtgEmployeeList.Columns["CountryTitle"].Visible = false;
                dtgEmployeeList.Columns["SexTitle"].Visible = false;
                dtgEmployeeList.Columns["BloodGroupTitle"].Visible = false;
                dtgEmployeeList.Columns["NomineePerson"].Visible = false;
                dtgEmployeeList.Columns["RelationShipTitle"].Visible = false;
                dtgEmployeeList.Columns["ContactNumber1"].Visible = false;
                dtgEmployeeList.Columns["ContactNumber2"].Visible = false;
                dtgEmployeeList.Columns["EmpACNumber"].Visible = false;
                dtgEmployeeList.Columns["BankName"].Visible = false;
                dtgEmployeeList.Columns["BankAddress"].Visible = false;
                dtgEmployeeList.Columns["IFSCCode"].Visible = false;
                dtgEmployeeList.Columns["BalanceLeaves"].Visible = false;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listLeaveStatements")
            {
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objEmployeeMaster.getMyEmployeeInformation(CurrentUser.EmpID);
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 150;
                dtgEmployeeList.Columns["EmpName"].Width = 350;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 150;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 150;
                dtgEmployeeList.Columns["DOJ"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dtgEmployeeList.Columns["DOB"].Visible = false;
                dtgEmployeeList.Columns["Address1"].Visible = false;
                dtgEmployeeList.Columns["Address2"].Visible = false;
                dtgEmployeeList.Columns["Area"].Visible = false;
                dtgEmployeeList.Columns["City"].Visible = false;
                dtgEmployeeList.Columns["StateTitle"].Visible = false;
                dtgEmployeeList.Columns["PIN"].Visible = false;
                dtgEmployeeList.Columns["CountryTitle"].Visible = false;
                dtgEmployeeList.Columns["SexTitle"].Visible = false;
                dtgEmployeeList.Columns["BloodGroupTitle"].Visible = false;
                dtgEmployeeList.Columns["NomineePerson"].Visible = false;
                dtgEmployeeList.Columns["RelationShipTitle"].Visible = false;
                dtgEmployeeList.Columns["ContactNumber1"].Visible = false;
                dtgEmployeeList.Columns["ContactNumber2"].Visible = false;
                dtgEmployeeList.Columns["EmpACNumber"].Visible = false;
                dtgEmployeeList.Columns["BankName"].Visible = false;
                dtgEmployeeList.Columns["BankAddress"].Visible = false;
                dtgEmployeeList.Columns["IFSCCode"].Visible = false;
                dtgEmployeeList.Columns["BalanceLeaves"].Visible = false;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listUpdateEmployeeInfo")
            {
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objEmployeeMaster.getMyEmployeeInformation(CurrentUser.EmpID);
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 150;
                dtgEmployeeList.Columns["EmpName"].Width = 350;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 150;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 150;
                dtgEmployeeList.Columns["DOJ"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dtgEmployeeList.Columns["DOB"].Visible = false;
                dtgEmployeeList.Columns["Address1"].Visible = false;
                dtgEmployeeList.Columns["Address2"].Visible = false;
                dtgEmployeeList.Columns["Area"].Visible = false;
                dtgEmployeeList.Columns["City"].Visible = false;
                dtgEmployeeList.Columns["StateTitle"].Visible = false;
                dtgEmployeeList.Columns["PIN"].Visible = false;
                dtgEmployeeList.Columns["CountryTitle"].Visible = false;
                dtgEmployeeList.Columns["SexTitle"].Visible = false;
                dtgEmployeeList.Columns["BloodGroupTitle"].Visible = false;
                dtgEmployeeList.Columns["NomineePerson"].Visible = false;
                dtgEmployeeList.Columns["RelationShipTitle"].Visible = false;
                dtgEmployeeList.Columns["ContactNumber1"].Visible = false;
                dtgEmployeeList.Columns["ContactNumber2"].Visible = false;
                dtgEmployeeList.Columns["EmpACNumber"].Visible = false;
                dtgEmployeeList.Columns["BankName"].Visible = false;
                dtgEmployeeList.Columns["BankAddress"].Visible = false;
                dtgEmployeeList.Columns["IFSCCode"].Visible = false;
                dtgEmployeeList.Columns["BalanceLeaves"].Visible = false;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listApplyLeaveInfo")
            {
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objEmployeeMaster.getMyEmployeeInformation(CurrentUser.EmpID);
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 150;
                dtgEmployeeList.Columns["EmpName"].Width = 350;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 150;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 150;
                dtgEmployeeList.Columns["DOJ"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dtgEmployeeList.Columns["DOB"].Visible = false;
                dtgEmployeeList.Columns["Address1"].Visible = false;
                dtgEmployeeList.Columns["Address2"].Visible = false;
                dtgEmployeeList.Columns["Area"].Visible = false;
                dtgEmployeeList.Columns["City"].Visible = false;
                dtgEmployeeList.Columns["StateTitle"].Visible = false;
                dtgEmployeeList.Columns["PIN"].Visible = false;
                dtgEmployeeList.Columns["CountryTitle"].Visible = false;
                dtgEmployeeList.Columns["SexTitle"].Visible = false;
                dtgEmployeeList.Columns["BloodGroupTitle"].Visible = false;
                dtgEmployeeList.Columns["NomineePerson"].Visible = false;
                dtgEmployeeList.Columns["RelationShipTitle"].Visible = false;
                dtgEmployeeList.Columns["ContactNumber1"].Visible = false;
                dtgEmployeeList.Columns["ContactNumber2"].Visible = false;
                dtgEmployeeList.Columns["EmpACNumber"].Visible = false;
                dtgEmployeeList.Columns["BankName"].Visible = false;
                dtgEmployeeList.Columns["BankAddress"].Visible = false;
                dtgEmployeeList.Columns["IFSCCode"].Visible = false;
                dtgEmployeeList.Columns["BalanceLeaves"].Visible = false;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listEmpLeaveEntitlements")
            {
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objEmployeeMaster.getCompleteEmployeesList();
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 150;
                dtgEmployeeList.Columns["EmpName"].Width = 250;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 250;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 250;
                dtgEmployeeList.Columns["ContactNumber1"].Width = 250;
                dtgEmployeeList.Columns["ContactNumber2"].Width = 250;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listAdvanceRequestingUsers" || lblSearchOptionClickedFor.Text.Trim() == "listAdvanceRequestToUsers")
            {
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objEmployeeMaster.getCompleteEmployeesList();
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 150;
                dtgEmployeeList.Columns["EmpName"].Width = 250;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 250;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 250;
                dtgEmployeeList.Columns["ContactNumber1"].Width = 250;
                dtgEmployeeList.Columns["ContactNumber2"].Width = 250;
                dtgEmployeeList.Columns["StateID"].Visible = false;
                dtgEmployeeList.Columns["SexID"].Visible = false;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listPayrollConfigUsersList")
            {
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objEmployeeMaster.getCompleteEmployeesList();
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 150;
                dtgEmployeeList.Columns["EmpName"].Width = 250;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 250;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 250;
                dtgEmployeeList.Columns["ContactNumber1"].Width = 250;
                dtgEmployeeList.Columns["ContactNumber2"].Width = 250;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "DashboardEmployeeLeaveApprovalPendingRequestList")
            {
                this.Text = "Leave Request List";
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objLeaveInfo.getPendingLeaveApprovalList();
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["LeaveTRID"].Visible = false;
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 100;
                dtgEmployeeList.Columns["EmpName"].Width = 200;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 200;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 200;
                dtgEmployeeList.Columns["LeaveTypeID"].Visible = false;
                dtgEmployeeList.Columns["LeaveTypeTitle"].Width = 150;
                dtgEmployeeList.Columns["LeaveAppliedDate"].Visible = false;
                dtgEmployeeList.Columns["LeaveAppliedDate"].Width = 100;
                dtgEmployeeList.Columns["LeaveAppliedDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dtgEmployeeList.Columns["LeaveDuration"].Width = 100;
                dtgEmployeeList.Columns["ActualLeaveDateFrom"].Width = 100;
                dtgEmployeeList.Columns["ActualLeaveDateFrom"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dtgEmployeeList.Columns["ActualLeaveDateTo"].Width = 100;
                dtgEmployeeList.Columns["ActualLeaveDateTo"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dtgEmployeeList.Columns["LeaveComments"].Width = 250;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "DashboardEmployeeLeaveApprovedList")
            {
                this.Text = "Leave Request List";
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objLeaveInfo.GetEmployeeOOOList(Convert.ToInt32(lblClientID.Text.ToString()), Convert.ToInt32(lblFinYearID.Text.ToString()), Convert.ToDateTime(lblFilterDate.Text.ToString()));
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 100;
                dtgEmployeeList.Columns["EmpCode"].ReadOnly = true;
                dtgEmployeeList.Columns["EmpName"].Width = 200;
                dtgEmployeeList.Columns["EmpName"].ReadOnly = true;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 200;
                dtgEmployeeList.Columns["DesignationTitle"].ReadOnly = true;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 200;
                dtgEmployeeList.Columns["DepartmentTitle"].ReadOnly = true;
                dtgEmployeeList.Columns["LeaveTypeTitle"].Width = 150;
                dtgEmployeeList.Columns["LeaveTypeTitle"].ReadOnly = true;
                dtgEmployeeList.Columns["ActualLeaveDateFrom"].Width = 100;
                dtgEmployeeList.Columns["ActualLeaveDateFrom"].ReadOnly = true;
                dtgEmployeeList.Columns["ActualLeaveDateFrom"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dtgEmployeeList.Columns["ActualLeaveDateTo"].Width = 100;
                dtgEmployeeList.Columns["ActualLeaveDateTo"].ReadOnly = true;
                dtgEmployeeList.Columns["ActualLeaveDateTo"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dtgEmployeeList.Columns["LeaveDuration"].Width = 100;
                dtgEmployeeList.Columns["LeaveDuration"].ReadOnly = true;
                dtgEmployeeList.Columns["LeaveMode"].Width = 100;
                dtgEmployeeList.Columns["LeaveMode"].ReadOnly = true;
                dtgEmployeeList.Columns["OrderID"].Visible = false;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "DashboardEmployeePresentList")
            {
                this.Text = "Leave Request List";
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objDashboard.GetAllEmployeesPresentList(Convert.ToInt32(lblClientID.Text.ToString()), Convert.ToInt32(lblFinYearID.Text.ToString()));
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 100;
                dtgEmployeeList.Columns["EmpCode"].ReadOnly = true;
                dtgEmployeeList.Columns["EmpName"].Width = 200;
                dtgEmployeeList.Columns["EmpName"].ReadOnly = true;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 200;
                dtgEmployeeList.Columns["DesignationTitle"].ReadOnly = true;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 200;
                dtgEmployeeList.Columns["DepartmentTitle"].ReadOnly = true;
                dtgEmployeeList.Columns["ContactNumber1"].Width = 150;
                dtgEmployeeList.Columns["ContactNumber1"].ReadOnly = true;
                dtgEmployeeList.Columns["ContactNumber2"].Width = 150;
                dtgEmployeeList.Columns["ContactNumber2"].ReadOnly = true;
                dtgEmployeeList.Columns["AttStatus"].Width = 150;
                dtgEmployeeList.Columns["AttStatus"].ReadOnly = true;
                dtgEmployeeList.Columns["AttDate"].Visible = false;
                dtgEmployeeList.Columns["ClientID"].Visible = false;
                dtgEmployeeList.Columns["FinYearID"].Visible = false;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "DashboardEmployeesWeeklyOffList")
            {
                this.Text = "Employees Weekly Off List";
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objDashboard.GetAllEmployeesWeeklyOffList(Convert.ToInt32(lblClientID.Text.ToString()), Convert.ToInt32(lblFinYearID.Text.ToString()));
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].Width = 100;
                dtgEmployeeList.Columns["EmpCode"].ReadOnly = true;
                dtgEmployeeList.Columns["EmpName"].Width = 200;
                dtgEmployeeList.Columns["EmpName"].ReadOnly = true;
                dtgEmployeeList.Columns["DesignationTitle"].Width = 200;
                dtgEmployeeList.Columns["DesignationTitle"].ReadOnly = true;
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 200;
                dtgEmployeeList.Columns["DepartmentTitle"].ReadOnly = true;
                dtgEmployeeList.Columns["ContactNumber1"].Width = 150;
                dtgEmployeeList.Columns["ContactNumber1"].ReadOnly = true;
                dtgEmployeeList.Columns["ContactNumber2"].Width = 150;
                dtgEmployeeList.Columns["ContactNumber2"].ReadOnly = true;
                dtgEmployeeList.Columns["WklyOffCode"].Width = 150;
                dtgEmployeeList.Columns["WklyOffCode"].ReadOnly = true;
                dtgEmployeeList.Columns["WklyOffTitle"].Width = 150;
                dtgEmployeeList.Columns["WklyOffTitle"].ReadOnly = true;
                dtgEmployeeList.Columns["WklyOffDay"].Visible = false;
                dtgEmployeeList.Columns["ClientID"].Visible = false;
                dtgEmployeeList.Columns["FinYearID"].Visible = false;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "DashboardEmployeesBirthdayList")
            {
                this.Text = "Employees Weekly Off List";
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objDashboard.GetTodaysEmployeesBirthdayList(Convert.ToInt32(lblClientID.Text.ToString()), Convert.ToInt32(lblFinYearID.Text.ToString()));
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].HeaderText = "Emp Code";
                dtgEmployeeList.Columns["EmpCode"].Width = 100;
                dtgEmployeeList.Columns["EmpCode"].ReadOnly = true;
                dtgEmployeeList.Columns["EmpName"].HeaderText = "Employee Name";
                dtgEmployeeList.Columns["EmpName"].Width = 200;
                dtgEmployeeList.Columns["EmpName"].ReadOnly = true;
                dtgEmployeeList.Columns["DesignationTitle"].HeaderText = "Designation";
                dtgEmployeeList.Columns["DesignationTitle"].Width = 200;
                dtgEmployeeList.Columns["DesignationTitle"].ReadOnly = true;
                dtgEmployeeList.Columns["DepartmentTitle"].HeaderText = "Department";
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 200;
                dtgEmployeeList.Columns["DepartmentTitle"].ReadOnly = true;
                dtgEmployeeList.Columns["ContactNumber1"].HeaderText = "Contact Number";
                dtgEmployeeList.Columns["ContactNumber1"].Width = 150;
                dtgEmployeeList.Columns["ContactNumber1"].ReadOnly = true;
                dtgEmployeeList.Columns["ContactNumber2"].HeaderText = "Mail ID";
                dtgEmployeeList.Columns["ContactNumber2"].Width = 200;
                dtgEmployeeList.Columns["ContactNumber2"].ReadOnly = true;
                dtgEmployeeList.Columns["DOB"].HeaderText = "BirthDay Date";
                dtgEmployeeList.Columns["DOB"].Width = 100;
                dtgEmployeeList.Columns["DOB"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dtgEmployeeList.Columns["ClientID"].Visible = false;
                dtgEmployeeList.Columns["FinYearID"].Visible = false;
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "DashboardEmployeesWorkAnniversaryList")
            {
                this.Text = "Employees Weekly Off List";
                dtgEmployeeList.DataSource = null;
                dtgEmployeeList.DataSource = objDashboard.GetTodaysEmployeesWorkAnniversaryList(Convert.ToInt32(lblClientID.Text.ToString()), Convert.ToInt32(lblFinYearID.Text.ToString()));
                dtgEmployeeList.Columns["EmpID"].Visible = false;
                dtgEmployeeList.Columns["EmpCode"].HeaderText = "Emp Code";
                dtgEmployeeList.Columns["EmpCode"].Width = 100;
                dtgEmployeeList.Columns["EmpCode"].ReadOnly = true;
                dtgEmployeeList.Columns["EmpName"].HeaderText = "Employee Name";
                dtgEmployeeList.Columns["EmpName"].Width = 200;
                dtgEmployeeList.Columns["EmpName"].ReadOnly = true;
                dtgEmployeeList.Columns["DesignationTitle"].HeaderText = "Designation";
                dtgEmployeeList.Columns["DesignationTitle"].Width = 200;
                dtgEmployeeList.Columns["DesignationTitle"].ReadOnly = true;
                dtgEmployeeList.Columns["DepartmentTitle"].HeaderText = "Department";
                dtgEmployeeList.Columns["DepartmentTitle"].Width = 200;
                dtgEmployeeList.Columns["DepartmentTitle"].ReadOnly = true;
                dtgEmployeeList.Columns["ContactNumber1"].HeaderText = "Contact Number";
                dtgEmployeeList.Columns["ContactNumber1"].Width = 150;
                dtgEmployeeList.Columns["ContactNumber1"].ReadOnly = true;
                dtgEmployeeList.Columns["ContactNumber2"].HeaderText = "Mail ID";
                dtgEmployeeList.Columns["ContactNumber2"].Width = 200;
                dtgEmployeeList.Columns["ContactNumber2"].ReadOnly = true;
                dtgEmployeeList.Columns["DOJ"].HeaderText = "Joining Date";
                dtgEmployeeList.Columns["DOJ"].Width = 100;
                dtgEmployeeList.Columns["DOJ"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dtgEmployeeList.Columns["ClientID"].Visible = false;
                dtgEmployeeList.Columns["FinYearID"].Visible = false;
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
            else if (lblSearchOptionClickedFor.Text.Trim() == "listSSEmployees")
            {
                this.frmSSEmployeeMaster.SelectedEmployeeID("listSSEmployees", Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["EmpID"].Value.ToString()));
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
                this.frmModuleAssignment.SelectedEmployeeID("listUserModuleAssignment", Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["EmpID"].Value.ToString()));
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
            else if (lblSearchOptionClickedFor.Text.Trim() == "listUpdateEmployeeInfo")
            {
                this.frmUpdateCurrentUserInfo.SelectedEmployeeID("listUpdateEmployeeInfo", Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["EmpID"].Value.ToString()));
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listApplyLeaveInfo")
            {
                this.frmCurrentUserLeaveMaster.SelectedEmployeeID("listApplyLeaveInfo", Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["EmpID"].Value.ToString()));
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listEmpLeaveEntitlements")
            {
                this.frmEmpLeaveEntitlement.SelectedEmployeeID("listEmpLeaveEntitlements", Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["EmpID"].Value.ToString()));
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listLeaveStatements")
            {
                this.frmLeaveStatements.SelectedEmployeeID("listLeaveStatements", Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["EmpID"].Value.ToString()));
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listAdvanceRequestingUsers")
            {
                this.frmEmpAdvancRequest.SelectedEmployeeID("listAdvanceRequestingUsers", Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["EmpID"].Value.ToString()));
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listAdvanceRequestToUsers")
            {
                this.frmEmpAdvancRequest.SelectedEmployeeID("listAdvanceRequestToUsers", Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["EmpID"].Value.ToString()));
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listPayrollConfigUsersList")
            {
                this.frmPayrollConfiguration.SelectedEmployeeID("listPayrollConfigUsersList", Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["EmpID"].Value.ToString()));
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "DashboardEmployeeLeaveApprovalPendingRequestList")
            {
                //Just Close this form
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "DashboardEmployeeLeaveApprovedList")
            {
                //Just Close this form
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "DashboardEmployeePresentList")
            {
                //Just Close this form
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "DashboardEmployeesBirthdayList")
            {
                //Just Close this form
            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "DashboardEmployeesWorkAnniversaryList")
            {
                //Just Close this form
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
                if (lblSearchOptionClickedFor.Text.Trim() == "listUpdateEmployeeInfo" || lblSearchOptionClickedFor.Text.Trim() == "listApplyLeaveInfo")  //Don't Search
                    return;

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

        private void frmEmployeeList_Activated(object sender, EventArgs e)
        {
            dtgEmployeeList.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
        }
    }
}
