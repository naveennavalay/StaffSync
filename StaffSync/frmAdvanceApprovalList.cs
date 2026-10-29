using Common;
using DocumentFormat.OpenXml.Office2016.Drawing.Charts;
using Krypton.Toolkit;
using ModelStaffSync;
using Org.BouncyCastle.Asn1.Ocsp;
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
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StaffSync
{
    public partial class frmAdvanceApprovalList : Form
    {
        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        //DALStaffSync.clsLeaveTRList objLeaveTRList = new DALStaffSync.clsLeaveTRList();
        //DALStaffSync.clsWeeklyOffInfo objWeeklyOffInfo = new DALStaffSync.clsWeeklyOffInfo();
        //DALStaffSync.clsAttendanceMas objAttendanceInfo = new DALStaffSync.clsAttendanceMas();
        //DALStaffSync.clsEmpLeaveEntitlementInfo objEmpLeaveEntitlementInfo = new DALStaffSync.clsEmpLeaveEntitlementInfo();
        //List<WklyOffProfileDetailsInfo> lstWeeklyOffDetailsInfo = new List<WklyOffProfileDetailsInfo>();
        DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster();
        DALStaffSync.clsEmployeePersonalInfo objEmployeePersonalInfo = new DALStaffSync.clsEmployeePersonalInfo();
        DALStaffSync.clsAuditLog objAuditLog = new DALStaffSync.clsAuditLog();
        DALStaffSync.clsAdvanceTypeMas objAdvanceTypeMas = new DALStaffSync.clsAdvanceTypeMas();
        DALStaffSync.clsAdvanceTransaction objAdvanceTransaction = new DALStaffSync.clsAdvanceTransaction();
        DALStaffSync.clsAssetsInfo objAssetsInfo = new DALStaffSync.clsAssetsInfo();
        DALStaffSync.clsAssetRegister objAssetRegister = new DALStaffSync.clsAssetRegister();
        DALStaffSync.clsEmpAssetAllocationInfo objEmpAssetAllocationInfo = new DALStaffSync.clsEmpAssetAllocationInfo();
        DALStaffSync.clsEmpReimbursRequestInfo objEmpReimbursRequestInfo = new DALStaffSync.clsEmpReimbursRequestInfo();
        DALStaffSync.clsReimbursement objReimbursementInfo = new DALStaffSync.clsReimbursement();
        //DALStaffSync.clsEmpReimbursRequestInfo objEmpReimbursRequestInfo = new DALStaffSync.clsEmpReimbursRequestInfo()
        frmDashboard objDashboard = (frmDashboard)System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();
        ClientStatutory tmpClientStatutory = new ClientStatutory();
        string strApproverType = "";
        public frmAdvanceApprovalList()
        {
            InitializeComponent();
        }

        public frmAdvanceApprovalList(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmAdvanceApprovalList(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo, string tmpApproverType)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
            strApproverType = tmpApproverType;
            this.Text = "Pending Requesters List : " + strApproverType;
            GetAdvancePendingList(strApproverType);
            GetAssetRequesterPendingList(strApproverType);
            GetReimbursmentRequesterPendingList(strApproverType);
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            {
                if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            objDashboard.lblDashboardTitle.Text = "Dashboard";
            objDashboard.sptrDashboardContainer.Visible = true;
            objDashboard.grpDashboardDateRange.Visible = true;
            objDashboard.refreshDashboardCharts();
            this.Close();
        }

        private void frmAdvanceApprovalList_Load(object sender, EventArgs e)
        {
            FocusManager.EnableHighlighting = false;
            FocusManager.ShowNavigationError = true;
            FocusManager.Register(this);
            FocusManager.SetFocus(btnSaveDetails);

            // TODO: This line of code loads data into the 'staffsyncDBDataSet1.qryAllEmpLeavePendingStatement' table. You can move, or remove it, as needed.
            //this.qryAllEmpLeavePendingStatementTableAdapter.Fill(this.staffsyncDBDataSet1.qryAllEmpLeavePendingStatement);
            //// TODO: This line of code loads data into the 'staffsyncDBDTSet.EmpMasInfo' table. You can move, or remove it, as needed.
            //this.empMasInfoTableAdapter.Fill(this.staffsyncDBDTSet.EmpMasInfo);
            disableControls();
            clearControls();            
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
            clearControls();
            enableControls();
            errValidator.Clear();
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            bool advanceSelected = dtgAdvanceRequestersList.Rows.Cast<DataGridViewRow>().Any(row => !row.IsNewRow && Convert.ToBoolean(row.Cells["Select"].Value ?? false));
            bool assetSelected = dtgAssetsRequestersList.Rows.Cast<DataGridViewRow>().Any(row => !row.IsNewRow && Convert.ToBoolean(row.Cells["Select"].Value ?? false));
            bool reimbursementSelected = dtgReimbursmentRequestersList.Rows.Cast<DataGridViewRow>().Any(row => !row.IsNewRow && Convert.ToBoolean(row.Cells["Select"].Value ?? false));
            if (!advanceSelected && !assetSelected && !reimbursementSelected)
            {
                MessageBox.Show("Please select at least one request from either grid.", "StaffSync", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string message = "";
            if (advanceSelected && assetSelected && reimbursementSelected)
            {
                message = "You are about to process Bulk Request Approvals.\nAre you sure to continue?";
            }
            else if (advanceSelected)
            {
                message = "You are about to process Bulk Advance Request Approval.\nAre you sure to continue?";
            }
            else if (assetSelected)
            {
                message = "You are about to process Bulk Asset Request Approval.\nAre you sure to continue?";
            }
            else if (reimbursementSelected)
            {
                message = "You are about to process Bulk Reimbursment Request Approval.\nAre you sure to continue?";
            }

            DialogResult result = MessageBox.Show(message, "Confirm Bulk Approval", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            string AdvanceRequestCode = "";

            if (strApproverType == "FirstApprover")
            {
                foreach (DataGridViewRow dc in dtgAdvanceRequestersList.Rows)
                {
                    if (Convert.ToBoolean(dc.Cells["Select"].Value.ToString()) == true)
                    {
                        int FirstApproverID = Convert.ToInt32(dc.Cells["ApproverEmpID1"].Value);
                        int AdvanceRequestID = Convert.ToInt32(dc.Cells["EmpAdvanceRequestID"].Value);
                        string ApproverRequestedToComments1 = dc.Cells["ApproverRequestedToComments1"].Value?.ToString();
                        AdvanceRequestCode = dc.Cells["EmpAdvReqCode"].Value?.ToString();

                        objAdvanceTransaction.UpdateFirstApproverStatus(AdvanceRequestID, FirstApproverID, ApproverRequestedToComments1, DateTime.Now.Date);

                        if (ApproverRequestedToComments1 == "In Progress" || ApproverRequestedToComments1 == "Pending")
                        {
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AdvanceRequestID, "First Approver updated the Status for Advance Request : \"" + AdvanceRequestCode + "\" to \"" + ApproverRequestedToComments1 + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                        else if (ApproverRequestedToComments1 == "Approved")
                        {
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AdvanceRequestID, "First Approver updated the Status for Advance Request : \"" + AdvanceRequestCode + "\" to \"" + ApproverRequestedToComments1 + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                        else if (ApproverRequestedToComments1 == "Rejected")
                        {
                            objAdvanceTransaction.InsertAdvanceTransaction(dc.Cells["EmpAdvReqCode"].Value.ToString(), AdvanceRequestID, DateTime.Now.Date, 0, 0, 0, 0, "Cr", "By Advance Request : \"" + AdvanceRequestCode + "\" Rejection", 0);
                            objAdvanceTransaction.RejectOrCancleApproverStatus(AdvanceRequestID, "Rejected", DateTime.Now.Date);
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AdvanceRequestID, "First Approver updated the Status for Advance Request : \"" + AdvanceRequestCode + "\" to \"" + ApproverRequestedToComments1 + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                        else if (ApproverRequestedToComments1 == "Cancelled")
                        {
                            objAdvanceTransaction.InsertAdvanceTransaction(dc.Cells["EmpAdvReqCode"].Value.ToString(), AdvanceRequestID, DateTime.Now.Date, 0, 0, 0, 0, "Cr", "By Advance Request : \"" + AdvanceRequestCode + "\" Rejection", 0);
                            objAdvanceTransaction.RejectOrCancleApproverStatus(AdvanceRequestID, "Cancelled", DateTime.Now.Date);
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AdvanceRequestID, "First Approver updated the Status for Advance Request : \"" + AdvanceRequestCode + "\" to \"" + ApproverRequestedToComments1 + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                    }
                }

                foreach (DataGridViewRow dc in dtgAssetsRequestersList.Rows)
                {
                    if (Convert.ToBoolean(dc.Cells["Select"].Value.ToString()) == true)
                    {
                        int AssetID = Convert.ToInt32(dc.Cells["AssetID"].Value);
                        int AssetRequestID = Convert.ToInt32(dc.Cells["AssetRequestID"].Value);
                        string AssetRequestCode = dc.Cells["AssetRequestCode"].Value.ToString();
                        string AssetRequestStatus = dc.Cells["RequestStatus"].Value.ToString();
                        objAssetsInfo.UpdateAssetRequestFirstApprover(AssetRequestID, AssetRequestStatus);
                        SelectedAssetInfo objSelectedAssetInfo = objAssetsInfo.getSelectedAssetInfo(AssetID);
                        SpecificAssetRegisterInfo objSpecificAssetRegisterInfo = objAssetRegister.getSpecificAssetRegisterInfo(AssetID, AssetRequestID);

                        decimal newBalance = objSelectedAssetInfo.OutstandingQuantity - objSpecificAssetRegisterInfo.CBalance;

                        if (AssetRequestStatus.ToString() == "In Progress" || AssetRequestStatus.ToString() == "Pending")
                        {
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AssetRequestID, "First Approver updated the Status for Asset Request : \"" + AssetRequestCode + "\" to \"" + AssetRequestStatus + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "AssetRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                        else if (AssetRequestStatus.ToString() == "Approved")
                        {
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AssetRequestID, "First Approver updated the Status for Asset Request : \"" + AssetRequestCode + "\" to \"" + AssetRequestStatus + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "AssetRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                        else if (AssetRequestStatus.ToString() == "Rejected")
                        {
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AssetRequestID, "First Approver updated the Status for Asset Request : \"" + AssetRequestCode + "\" to \"" + AssetRequestStatus + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "AssetRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                        else if (AssetRequestStatus.ToString() == "Cancelled")
                        {
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AssetRequestID, "First Approver updated the Status for Asset Request : \"" + AssetRequestCode + "\" to \"" + AssetRequestStatus + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "AssetRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                    }
                }

                foreach (DataGridViewRow dc in dtgReimbursmentRequestersList.Rows)
                {
                    if (Convert.ToBoolean(dc.Cells["Select"].Value.ToString()) == true)
                    {
                        int ReimbID = Convert.ToInt32(dc.Cells["ReimbID"].Value);
                        int ReimbursRequestID = Convert.ToInt32(dc.Cells["ReimbursRequestID"].Value);
                        string ReimbursRequestCode = dc.Cells["ReimbursRequestCode"].Value.ToString();
                        string ReimbursRequestStatus = dc.Cells["RequestStatus"].Value.ToString();
                        objEmpReimbursRequestInfo.UpdateReimbursmentRequestFirstApprover(ReimbursRequestID, ReimbursRequestStatus);
                        ReimbursementModel objSelectedReimbursement = objReimbursementInfo.getSelectedReimbursementInfo(ReimbID);

                        if (ReimbursRequestStatus.ToString() == "In Progress" || ReimbursRequestStatus.ToString() == "Pending")
                        {
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), ReimbursRequestID, "First Approver updated the Status for Reimbursment Request : \"" + ReimbursRequestCode + "\" to \"" + ReimbursRequestStatus + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "ReimbursRequestStatusRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                        else if (ReimbursRequestStatus.ToString() == "Approved")
                        {
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), ReimbursRequestID, "First Approver updated the Status for Reimbursment Request : \"" + ReimbursRequestCode + "\" to \"" + ReimbursRequestStatus + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "ReimbursRequestStatusRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                        else if (ReimbursRequestStatus.ToString() == "Rejected")
                        {
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), ReimbursRequestID, "First Approver updated the Status for Reimbursment Request : \"" + ReimbursRequestCode + "\" to \"" + ReimbursRequestStatus + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "ReimbursRequestStatusRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                        else if (ReimbursRequestStatus.ToString() == "Cancelled")
                        {
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), ReimbursRequestID, "First Approver updated the Status for Reimbursment Request : \"" + ReimbursRequestCode + "\" to \"" + ReimbursRequestStatus + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "ReimbursRequestStatusRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                    }
                }
            }
            else if (strApproverType == "SecondApprover")
            {
                foreach (DataGridViewRow dc in dtgAdvanceRequestersList.Rows)
                {
                    if (Convert.ToBoolean(dc.Cells["Select"].Value.ToString()) == true)
                    {
                        int SecondApproverID = Convert.ToInt32(dc.Cells["ApproverEmpID2"].Value);
                        int AdvanceRequestID = Convert.ToInt32(dc.Cells["EmpAdvanceRequestID"].Value);
                        string RequestMovedToComments = dc.Cells["RequestMovedToComments"].Value?.ToString();
                        AdvanceRequestCode = dc.Cells["EmpAdvReqCode"].Value?.ToString();
                        objAdvanceTransaction.UpdateSecondApproverStatus(AdvanceRequestID, SecondApproverID, RequestMovedToComments, DateTime.Now.Date);

                        if (RequestMovedToComments == "In Progress" || RequestMovedToComments == "Pending")
                        {
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AdvanceRequestID,"Second Approver updated the Status for Advance Request : \"" + AdvanceRequestCode + "\" to \"" + RequestMovedToComments + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                        else if (RequestMovedToComments == "Approved")
                        {
                            objAdvanceTransaction.InsertAdvanceTransaction(dc.Cells["EmpAdvReqCode"].Value.ToString(), AdvanceRequestID, Convert.ToDateTime(dc.Cells["EmpAdvanceRequestDate"].Value.ToString()), 0, Convert.ToDecimal(dc.Cells["AdvanceAmount"].Value.ToString()), 0, Convert.ToDecimal(dc.Cells["AdvanceAmount"].Value.ToString()), "Cr", "By Advance Request : \"" + AdvanceRequestCode + "\" Approval", 0);
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AdvanceRequestID, "Second Approver updated the Status for Advance Request : \"" + AdvanceRequestCode + "\" to \"" + RequestMovedToComments + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AdvanceRequestID, "Advance Request : \"" + dc.Cells["EmpAdvReqCode"].Value.ToString() +  "\" raised by Employee Code : \"" + dc.Cells["RequesterEmpCode"].Value.ToString() + "\" and Employee Name : \"" + dc.Cells["RequesterEmpName"].Value.ToString() + "\" has been approved and credited", "Update", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                        else if (RequestMovedToComments == "Rejected")
                        {
                            objAdvanceTransaction.InsertAdvanceTransaction(dc.Cells["EmpAdvReqCode"].Value.ToString(), AdvanceRequestID, DateTime.Now.Date, 0, 0, 0, 0, "Cr", "By Advance Request : \"" + AdvanceRequestCode + "\" Rejection", 0);
                            objAdvanceTransaction.RejectOrCancleApproverStatus(AdvanceRequestID, "Rejected", DateTime.Now.Date);
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AdvanceRequestID, "Second Approver updated the Status for Advance Request : \"" + AdvanceRequestCode + "\" to \"" + RequestMovedToComments + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                        else if (RequestMovedToComments == "Cancelled")
                        {
                            objAdvanceTransaction.InsertAdvanceTransaction(dc.Cells["EmpAdvReqCode"].Value.ToString(), AdvanceRequestID, DateTime.Now.Date, 0, 0, 0, 0, "Cr", "By Advance Request : \"" + AdvanceRequestCode + "\" Rejection", 0);
                            objAdvanceTransaction.RejectOrCancleApproverStatus(AdvanceRequestID, "Cancelled", DateTime.Now.Date);
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AdvanceRequestID, "Second Approver updated the Status for Advance Request : \"" + AdvanceRequestCode + "\" to \"" + RequestMovedToComments + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                    }
                }

                foreach (DataGridViewRow dc in dtgAssetsRequestersList.Rows)
                {
                    if (Convert.ToBoolean(dc.Cells["Select"].Value.ToString()) == true)
                    {
                        int AssetID = Convert.ToInt32(dc.Cells["AssetID"].Value);
                        int AssetRequestID = Convert.ToInt32(dc.Cells["AssetRequestID"].Value);
                        string AssetRequestCode = dc.Cells["AssetRequestCode"].Value.ToString();
                        string AssetRequestStatus = dc.Cells["RequestStatus"].Value.ToString();
                        
                        objAssetsInfo.UpdateAssetRequestSecondApprover(AssetRequestID, AssetRequestStatus);

                        SelectedAssetInfo objSelectedAssetInfo = objAssetsInfo.getSelectedAssetInfo(AssetID);
                        SpecificAssetRegisterInfo objSpecificAssetRegisterInfo = objAssetRegister.getSpecificAssetRegisterInfo(AssetID, AssetRequestID);

                        decimal newBalance = objSelectedAssetInfo.OutstandingQuantity - objSpecificAssetRegisterInfo.CBalance;

                        if (AssetRequestStatus.ToString() == "In Progress" || AssetRequestStatus.ToString() == "Pending")
                        {
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AssetRequestID, "Second Approver updated the Status for Asset Request : \"" + AssetRequestCode + "\" to \"" + AssetRequestStatus + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                        else if (AssetRequestStatus.ToString() == "Approved")
                        {
                            int AssetRegisterID = objAssetRegister.InsertAssetRegisterInfo(AssetID, DateTime.Today, objSelectedAssetInfo.OutstandingQuantity, 0, objSpecificAssetRegisterInfo.CBalance, newBalance, "Dr", "To Asset Issue", AssetRequestID);
                            AssetRegisterID = objAssetsInfo.UpdateAssetRegisterBalanceInfo(AssetID, newBalance);
                            objEmpAssetAllocationInfo.InsertEmployeeAssetAllocationShortInformation(Convert.ToInt32(dc.Cells["AssetRequestByID"].Value), AssetID, DateTime.Today, "Asset Request : \"" + AssetRequestCode + "\" approved to issue \"" + dc.Cells["AssetName"].Value + "\" to issue to \nEmployee Code : \"" + dc.Cells["EmpCode"].Value.ToString() + "\" and Employee Name : \"" + dc.Cells["EmpName"].Value.ToString() + "\"", AssetRequestID);
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AssetRequestID, "Second Approver updated the Status for Asset Request : \"" + AssetRequestCode + "\" to \"" + AssetRequestStatus + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                        else if (AssetRequestStatus.ToString() == "Rejected")
                        {
                            int AssetRegisterID = objAssetRegister.InsertAssetRegisterInfo(AssetID, DateTime.Today, objSpecificAssetRegisterInfo.CBalance, 0, objSpecificAssetRegisterInfo.CBalance, 0, "Rj", "To Asset Issue - " + objSelectedAssetInfo.AssetName + " Request Rejected", AssetRequestID);
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AssetRequestID, "Second Approver updated the Status for Asset Request : \"" + AssetRequestCode + "\" to \"" + AssetRequestStatus + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                        else if (AssetRequestStatus.ToString() == "Cancelled")
                        {
                            int AssetRegisterID = objAssetRegister.InsertAssetRegisterInfo(AssetID, DateTime.Today, objSpecificAssetRegisterInfo.CBalance, 0, objSpecificAssetRegisterInfo.CBalance, 0, "Cnl", "To Asset Issue - " + objSelectedAssetInfo.AssetName + " Request Cancellation", AssetRequestID); 
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AssetRequestID, "Second Approver updated the Status for Asset Request : \"" + AssetRequestCode + "\" to \"" + AssetRequestStatus + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                    }
                }

                foreach (DataGridViewRow dc in dtgReimbursmentRequestersList.Rows)
                {
                    if (Convert.ToBoolean(dc.Cells["Select"].Value.ToString()) == true)
                    {
                        int ReimbID = Convert.ToInt32(dc.Cells["ReimbID"].Value);
                        int ReimbursRequestID = Convert.ToInt32(dc.Cells["ReimbursRequestID"].Value);
                        string ReimbursRequestCode = dc.Cells["ReimbursRequestCode"].Value.ToString();
                        string ReimbursRequestStatus = dc.Cells["RequestStatus"].Value.ToString();
                        string ReimbursRequestDescription = dc.Cells["ReimbursAproveComments1"].Value.ToString();
                        decimal ReimbursRequestAmount = Convert.ToDecimal(dc.Cells["ReimbursRequestAmount"].Value.ToString());
                        DateTime ReimbursRequestDate = Convert.ToDateTime(dc.Cells["ReimbursRequestDate"].Value.ToString());
                        objEmpReimbursRequestInfo.UpdateReimbursmentRequestSecondApprover(ReimbursRequestID, ReimbursRequestStatus);
                        ReimbursementModel objSelectedReimbursement = objReimbursementInfo.getSelectedReimbursementInfo(ReimbID);

                        if (ReimbursRequestStatus.ToString() == "In Progress" || ReimbursRequestStatus.ToString() == "Pending")
                        {
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), ReimbursRequestID, "Second Approver updated the Status for Reimbursment Request : \"" + ReimbursRequestCode + "\" to \"" + ReimbursRequestStatus + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "ReimbursRequestStatusRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                        else if (ReimbursRequestStatus.ToString() == "Approved")
                        {
                            objEmpReimbursRequestInfo.InsertEmployeeReimbursmentRequestMasInfo(ReimbursRequestDate, ReimbursRequestDescription, "In Progress", true, false, ReimbursRequestID);
                            objEmpReimbursRequestInfo.InsertEmployeeReimbursmentRequestDetailedInfo(ReimbursRequestID, ReimbursRequestDate, 0, ReimbursRequestAmount, 0, ReimbursRequestAmount, "Cr", "By Reimbursment Request");
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), ReimbursRequestID, "Second Approver updated the Status for Reimbursment Request : \"" + ReimbursRequestCode + "\" to \"" + ReimbursRequestStatus + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "ReimbursRequestStatusRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                        else if (ReimbursRequestStatus.ToString() == "Rejected")
                        {
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), ReimbursRequestID, "Second Approver updated the Status for Reimbursment Request : \"" + ReimbursRequestCode + "\" to \"" + ReimbursRequestStatus + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "ReimbursRequestStatusRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                        else if (ReimbursRequestStatus.ToString() == "Cancelled")
                        {
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), ReimbursRequestID, "Second Approver updated the Status for Reimbursment Request : \"" + ReimbursRequestCode + "\" to \"" + ReimbursRequestStatus + "\"", "Update", ModelStaffSync.CurrentUser.EmpName, "ReimbursRequestStatusRequestStatusUpdate", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                        }
                    }
                }
            }

            GetAdvancePendingList(strApproverType);
            GetAssetRequesterPendingList(strApproverType);
            GetReimbursmentRequesterPendingList(strApproverType);

            disableControls();
            clearControls();
            errValidator.Clear();          
        }

        public void clearControls()
        {

        }

        public void enableControls()
        {
            dtgAdvanceRequestersList.Enabled = true;
        }

        public void disableControls()
        {
            btnSaveDetails.Enabled = false;
            //txtLeaveApprovalDate.Enabled = false;
            //dtgBulkLeaveApproval.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            disableControls();
            clearControls();            
            errValidator.Clear();
        }

        private void btnModifyDetails_Click(object sender, EventArgs e)
        {
            clearControls();
            enableControls();
            errValidator.Clear();
        }

        private void btnRemoveDetails_Click(object sender, EventArgs e)
        {

        }

        private void GetAdvancePendingList(string tmpApproverType)
        {
            dtgAdvanceRequestersList.DataSource = null;
            if (tmpApproverType == "FirstApprover")
                dtgAdvanceRequestersList.DataSource = objAdvanceTransaction.AdvanceFirstApprovalPendingList(Convert.ToInt32(ModelStaffSync.CurrentUser.ClientID), Convert.ToInt32(objTempCurrentlyLoggedInUserInfo.EmpID));
            else if (tmpApproverType == "SecondApprover")
                dtgAdvanceRequestersList.DataSource = objAdvanceTransaction.AdvanceSecondApprovalPendingList(Convert.ToInt32(ModelStaffSync.CurrentUser.ClientID), Convert.ToInt32(objTempCurrentlyLoggedInUserInfo.EmpID));

            dtgAdvanceRequestersList.Columns["Select"].ReadOnly = true;
            dtgAdvanceRequestersList.Columns["Select"].Visible = true;
            dtgAdvanceRequestersList.Columns["Select"].Width = 50;
            dtgAdvanceRequestersList.Columns["RequesterEmpID"].ReadOnly = true;
            dtgAdvanceRequestersList.Columns["RequesterEmpID"].Width = 50;
            dtgAdvanceRequestersList.Columns["RequesterEmpID"].Visible = false;
            dtgAdvanceRequestersList.Columns["RequesterEmpCode"].ReadOnly = true;
            dtgAdvanceRequestersList.Columns["RequesterEmpCode"].Width = 100;
            dtgAdvanceRequestersList.Columns["RequesterEmpCode"].Visible = true;
            dtgAdvanceRequestersList.Columns["RequesterEmpName"].ReadOnly = true;
            dtgAdvanceRequestersList.Columns["RequesterEmpName"].Width = 200;
            dtgAdvanceRequestersList.Columns["RequesterEmpName"].Visible = true;
            dtgAdvanceRequestersList.Columns["RequesterDesignationTitle"].ReadOnly = true;
            dtgAdvanceRequestersList.Columns["RequesterDesignationTitle"].Width = 175;
            dtgAdvanceRequestersList.Columns["RequesterDesignationTitle"].Visible = true;
            dtgAdvanceRequestersList.Columns["RequesterDepartmentTitle"].ReadOnly = true;
            dtgAdvanceRequestersList.Columns["RequesterDepartmentTitle"].Width = 175;
            dtgAdvanceRequestersList.Columns["RequesterDepartmentTitle"].Visible = true;
            dtgAdvanceRequestersList.Columns["EmpAdvanceRequestID"].ReadOnly = true;
            dtgAdvanceRequestersList.Columns["EmpAdvanceRequestID"].Width = 100;
            dtgAdvanceRequestersList.Columns["EmpAdvanceRequestID"].Visible = false;
            dtgAdvanceRequestersList.Columns["AdvanceTypeTitle"].ReadOnly = true;
            dtgAdvanceRequestersList.Columns["AdvanceTypeTitle"].Width = 125;
            dtgAdvanceRequestersList.Columns["AdvanceTypeTitle"].Visible = true;
            dtgAdvanceRequestersList.Columns["EmpAdvReqCode"].ReadOnly = true;
            dtgAdvanceRequestersList.Columns["EmpAdvReqCode"].Width = 100;
            dtgAdvanceRequestersList.Columns["EmpAdvReqCode"].Visible = false;
            dtgAdvanceRequestersList.Columns["EmpAdvanceRequestDate"].ReadOnly = true;
            dtgAdvanceRequestersList.Columns["EmpAdvanceRequestDate"].Width = 100;
            dtgAdvanceRequestersList.Columns["EmpAdvanceRequestDate"].Visible = true;
            dtgAdvanceRequestersList.Columns["EmpAdvanceRequestDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgAdvanceRequestersList.Columns["AdvanceAmount"].ReadOnly = true;
            dtgAdvanceRequestersList.Columns["AdvanceAmount"].Width = 100;
            dtgAdvanceRequestersList.Columns["AdvanceAmount"].Visible = true;
            dtgAdvanceRequestersList.Columns["AdvanceAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgAdvanceRequestersList.Columns["AdvanceAmount"].DefaultCellStyle.Format = "0.00";

            if (tmpApproverType == "FirstApprover")
            {
                dtgAdvanceRequestersList.Columns["ApproverEmpID1"].ReadOnly = true;
                dtgAdvanceRequestersList.Columns["ApproverEmpID1"].Width = 50;
                dtgAdvanceRequestersList.Columns["ApproverEmpID1"].Visible = true;
                dtgAdvanceRequestersList.Columns["ApproverEmpCode1"].ReadOnly = true;
                dtgAdvanceRequestersList.Columns["ApproverEmpCode1"].Width = 100;
                dtgAdvanceRequestersList.Columns["ApproverEmpCode1"].Visible = true;
                dtgAdvanceRequestersList.Columns["ApproverEmpName1"].ReadOnly = true;
                dtgAdvanceRequestersList.Columns["ApproverEmpName1"].Width = 200;
                dtgAdvanceRequestersList.Columns["ApproverEmpName1"].Visible = true;
                dtgAdvanceRequestersList.Columns["ApproverEmpMailID1"].ReadOnly = true;
                dtgAdvanceRequestersList.Columns["ApproverEmpMailID1"].Width = 200;
                dtgAdvanceRequestersList.Columns["ApproverEmpMailID1"].Visible = true;
                dtgAdvanceRequestersList.Columns["ApproverRequestedToComments1"].ReadOnly = true;
                dtgAdvanceRequestersList.Columns["ApproverRequestedToComments1"].Width = 75;
                dtgAdvanceRequestersList.Columns["ApproverRequestedToComments1"].Visible = true;

                dtgAdvanceRequestersList.Columns["ApproverEmpID2"].Visible = false;
                dtgAdvanceRequestersList.Columns["ApproverEmpCode2"].Visible = false;
                dtgAdvanceRequestersList.Columns["ApproverEmpName2"].Visible = false;
                dtgAdvanceRequestersList.Columns["ApproverEmpMailID2"].Visible = false;
                dtgAdvanceRequestersList.Columns["RequestMovedToComments"].Visible = false;
            }
            else if (tmpApproverType == "SecondApprover")
            {
                dtgAdvanceRequestersList.Columns["ApproverEmpID1"].Visible = false;
                dtgAdvanceRequestersList.Columns["ApproverEmpCode1"].Visible = false;
                dtgAdvanceRequestersList.Columns["ApproverEmpName1"].Visible = false;
                dtgAdvanceRequestersList.Columns["ApproverEmpMailID1"].Visible = false;
                dtgAdvanceRequestersList.Columns["ApproverEmpCode1"].ReadOnly = true;
                dtgAdvanceRequestersList.Columns["ApproverEmpName1"].ReadOnly = true;
                dtgAdvanceRequestersList.Columns["ApproverEmpMailID1"].ReadOnly = true;
                dtgAdvanceRequestersList.Columns["ApproverRequestedToComments1"].ReadOnly = true;

                dtgAdvanceRequestersList.Columns["ApproverEmpID2"].ReadOnly = true;
                dtgAdvanceRequestersList.Columns["ApproverEmpID2"].Width = 50;
                dtgAdvanceRequestersList.Columns["ApproverEmpID2"].Visible = true;
                dtgAdvanceRequestersList.Columns["ApproverEmpCode2"].ReadOnly = true;
                dtgAdvanceRequestersList.Columns["ApproverEmpCode2"].Width = 100;
                dtgAdvanceRequestersList.Columns["ApproverEmpCode2"].Visible = true;
                dtgAdvanceRequestersList.Columns["ApproverEmpName2"].ReadOnly = true;
                dtgAdvanceRequestersList.Columns["ApproverEmpName2"].Width = 200;
                dtgAdvanceRequestersList.Columns["ApproverEmpName2"].Visible = true;
                dtgAdvanceRequestersList.Columns["ApproverEmpMailID2"].ReadOnly = true;
                dtgAdvanceRequestersList.Columns["ApproverEmpMailID2"].Width = 200;
                dtgAdvanceRequestersList.Columns["ApproverEmpMailID2"].Visible = true;
                dtgAdvanceRequestersList.Columns["RequestMovedToComments"].ReadOnly = true;
                dtgAdvanceRequestersList.Columns["RequestMovedToComments"].Width = 75;
                dtgAdvanceRequestersList.Columns["RequestMovedToComments"].Visible = true;
            }

            dtgAdvanceRequestersList.Columns["AdvanceRequestStatus"].ReadOnly = true;
            dtgAdvanceRequestersList.Columns["AdvanceRequestStatus"].Width = 200;
            dtgAdvanceRequestersList.Columns["AdvanceRequestStatus"].Visible = false;

            //foreach (DataGridViewRow indRow in this.dtgAdvanceRequestersList.Rows)
            //{
            //    indRow.Cells["Select"].ReadOnly = false;
            //    if (Convert.ToBoolean(indRow.Cells["Canceled"].Value.ToString()) == true)
            //    {
            //        indRow.DefaultCellStyle.BackColor = Color.LightGray;
            //    }
            //}
        }

        private void GetAssetRequesterPendingList(string tmpApproverType)
        {   
            dtgAssetsRequestersList.DataSource = null;

            if (strApproverType == "FirstApprover")
                dtgAssetsRequestersList.DataSource = objAssetRegister.FirstApprovalPendingAssetApprovalList(Convert.ToInt32(ModelStaffSync.CurrentUser.ClientID), Convert.ToInt32(objTempCurrentlyLoggedInUserInfo.EmpID));
            else if (strApproverType == "SecondApprover")
                dtgAssetsRequestersList.DataSource = objAssetRegister.SecondApprovalPendingAssetApprovalList(Convert.ToInt32(ModelStaffSync.CurrentUser.ClientID), Convert.ToInt32(objTempCurrentlyLoggedInUserInfo.EmpID));

            dtgAssetsRequestersList.Columns["Select"].ReadOnly = true;
            dtgAssetsRequestersList.Columns["Select"].Visible = true;
            dtgAssetsRequestersList.Columns["Select"].Width = 50;
            dtgAssetsRequestersList.Columns["EmpID"].Visible = false;
            dtgAssetsRequestersList.Columns["EmpCode"].ReadOnly = true;
            dtgAssetsRequestersList.Columns["EmpCode"].Visible = true;
            dtgAssetsRequestersList.Columns["EmpCode"].Width = 100;
            dtgAssetsRequestersList.Columns["EmpName"].ReadOnly = true;
            dtgAssetsRequestersList.Columns["EmpName"].Visible = true;
            dtgAssetsRequestersList.Columns["EmpName"].Width = 250;
            dtgAssetsRequestersList.Columns["DesignationTitle"].ReadOnly = true;
            dtgAssetsRequestersList.Columns["DesignationTitle"].Visible = true;
            dtgAssetsRequestersList.Columns["DesignationTitle"].Width = 150;
            dtgAssetsRequestersList.Columns["DepartmentTitle"].ReadOnly = true;
            dtgAssetsRequestersList.Columns["DepartmentTitle"].Visible = true;
            dtgAssetsRequestersList.Columns["DepartmentTitle"].Width = 150;
            dtgAssetsRequestersList.Columns["AssetCatMasID"].Visible = false;
            dtgAssetsRequestersList.Columns["AssetCategoryName"].ReadOnly = true;
            dtgAssetsRequestersList.Columns["AssetCategoryName"].Visible = true;
            dtgAssetsRequestersList.Columns["AssetCategoryName"].Width = 150;
            dtgAssetsRequestersList.Columns["AssetID"].Visible = false;
            dtgAssetsRequestersList.Columns["AssetCode"].ReadOnly = true;
            dtgAssetsRequestersList.Columns["AssetCode"].Visible = true;
            dtgAssetsRequestersList.Columns["AssetCode"].Width = 100;
            dtgAssetsRequestersList.Columns["AssetName"].ReadOnly = true;
            dtgAssetsRequestersList.Columns["AssetName"].Visible = true;
            dtgAssetsRequestersList.Columns["AssetName"].Width = 350;
            dtgAssetsRequestersList.Columns["AssetRequestID"].Visible = false;
            dtgAssetsRequestersList.Columns["AssetRequestCode"].ReadOnly = true;
            dtgAssetsRequestersList.Columns["AssetRequestCode"].Visible = true;
            dtgAssetsRequestersList.Columns["AssetRequestCode"].Width = 125;
            dtgAssetsRequestersList.Columns["AssetRequestDate"].ReadOnly = true;
            dtgAssetsRequestersList.Columns["AssetRequestDate"].Visible = true;
            dtgAssetsRequestersList.Columns["AssetRequestDate"].Width = 125;
            dtgAssetsRequestersList.Columns["AssetRequestDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgAssetsRequestersList.Columns["AssetRequestComments"].ReadOnly = true;
            dtgAssetsRequestersList.Columns["AssetRequestComments"].Visible = true;
            dtgAssetsRequestersList.Columns["AssetRequestComments"].Width = 250;
            dtgAssetsRequestersList.Columns["AssetRequestByStatus"].ReadOnly = true;
            dtgAssetsRequestersList.Columns["AssetRequestByID"].Visible = false;
            dtgAssetsRequestersList.Columns["AssetRequestByStatus"].Visible = true;
            dtgAssetsRequestersList.Columns["AssetRequestByStatus"].Width = 150;
            dtgAssetsRequestersList.Columns["RequestStatus"].ReadOnly = true;
            dtgAssetsRequestersList.Columns["RequestStatus"].Visible = true;
            dtgAssetsRequestersList.Columns["RequestStatus"].Width = 100;
            dtgAssetsRequestersList.Columns["RequestedTo"].Visible = false;
            //dtgAssetsRequestersList.Columns["ApproverEmpCode"].ReadOnly = true;
            //dtgAssetsRequestersList.Columns["ApproverEmpCode"].Visible = true;
            //dtgAssetsRequestersList.Columns["ApproverEmpCode"].Width = 150;
            //dtgAssetsRequestersList.Columns["ApproverEmpDesignationTitle"].ReadOnly = true;
            //dtgAssetsRequestersList.Columns["ApproverEmpDesignationTitle"].Visible = true;
            //dtgAssetsRequestersList.Columns["ApproverEmpDesignationTitle"].Width = 150;
            //dtgAssetsRequestersList.Columns["ApproverEmpDepartmentTitle"].ReadOnly = true;
            //dtgAssetsRequestersList.Columns["ApproverEmpDepartmentTitle"].Visible = true;
            //dtgAssetsRequestersList.Columns["ApproverEmpDepartmentTitle"].Width = 150;
            dtgAssetsRequestersList.Columns["AssetRequestStatus"].ReadOnly = true;
            dtgAssetsRequestersList.Columns["AssetRequestStatus"].Visible = true;
            dtgAssetsRequestersList.Columns["AssetRequestStatus"].Width = 150;
            dtgAssetsRequestersList.Columns["AssetRequestByStatus"].Visible = false; 
            dtgAssetsRequestersList.Columns["RequestedToComments"].Visible = false;
            dtgAssetsRequestersList.Columns["RequestedDate"].Visible = false;
            dtgAssetsRequestersList.Columns["AssetRequestStatus"].Visible = false;
            dtgAssetsRequestersList.Columns["ClientID"].Visible = false;
        }

        private void GetReimbursmentRequesterPendingList(string tmpApproverType)
        {
            dtgReimbursmentRequestersList.DataSource = null;

            if (strApproverType == "FirstApprover")
                dtgReimbursmentRequestersList.DataSource = objEmpReimbursRequestInfo.FirstApprovalPendingReimbursmentApprovalList(Convert.ToInt32(ModelStaffSync.CurrentUser.ClientID), Convert.ToInt32(objTempCurrentlyLoggedInUserInfo.EmpID));
            else if (strApproverType == "SecondApprover")
                dtgReimbursmentRequestersList.DataSource = objEmpReimbursRequestInfo.SecondApprovalPendingReimbursmentApprovalList(Convert.ToInt32(ModelStaffSync.CurrentUser.ClientID), Convert.ToInt32(objTempCurrentlyLoggedInUserInfo.EmpID));

            dtgReimbursmentRequestersList.Columns["Select"].ReadOnly = true;
            dtgReimbursmentRequestersList.Columns["Select"].Visible = true;
            dtgReimbursmentRequestersList.Columns["Select"].Width = 50;
            dtgReimbursmentRequestersList.Columns["EmpID"].Visible = false;
            dtgReimbursmentRequestersList.Columns["EmpCode"].ReadOnly = true;
            dtgReimbursmentRequestersList.Columns["EmpCode"].Visible = true;
            dtgReimbursmentRequestersList.Columns["EmpCode"].Width = 100;
            dtgReimbursmentRequestersList.Columns["EmpName"].ReadOnly = true;
            dtgReimbursmentRequestersList.Columns["EmpName"].Visible = true;
            dtgReimbursmentRequestersList.Columns["EmpName"].Width = 250;
            dtgReimbursmentRequestersList.Columns["DesignationTitle"].ReadOnly = true;
            dtgReimbursmentRequestersList.Columns["DesignationTitle"].Visible = true;
            dtgReimbursmentRequestersList.Columns["DesignationTitle"].Width = 150;
            dtgReimbursmentRequestersList.Columns["DepartmentTitle"].ReadOnly = true;
            dtgReimbursmentRequestersList.Columns["DepartmentTitle"].Visible = true;
            dtgReimbursmentRequestersList.Columns["DepartmentTitle"].Width = 150;
            dtgReimbursmentRequestersList.Columns["ReimbursRequestID"].Visible = false;
            dtgReimbursmentRequestersList.Columns["ReimbursRequestCode"].ReadOnly = true;
            dtgReimbursmentRequestersList.Columns["ReimbursRequestCode"].Visible = true;
            dtgReimbursmentRequestersList.Columns["ReimbursRequestCode"].Width = 100;
            dtgReimbursmentRequestersList.Columns["ReimbID"].Visible = false; 
            dtgReimbursmentRequestersList.Columns["ReimbTitle"].ReadOnly = true;
            dtgReimbursmentRequestersList.Columns["ReimbTitle"].Visible = true;
            dtgReimbursmentRequestersList.Columns["ReimbTitle"].Width = 300;
            dtgReimbursmentRequestersList.Columns["ReimbursRequestDate"].ReadOnly = true;
            dtgReimbursmentRequestersList.Columns["ReimbursRequestDate"].Width = 125;
            dtgReimbursmentRequestersList.Columns["ReimbursRequestDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgReimbursmentRequestersList.Columns["ReimbursRequestAmount"].ReadOnly = true;
            dtgReimbursmentRequestersList.Columns["ReimbursRequestAmount"].Visible = true;
            dtgReimbursmentRequestersList.Columns["ReimbursRequestAmount"].Width = 125;
            dtgReimbursmentRequestersList.Columns["ReimbursRequestAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgReimbursmentRequestersList.Columns["ReimbursRequestAmount"].DefaultCellStyle.Format = "c2";
            dtgReimbursmentRequestersList.Columns["ReimbursAproveComments1"].ReadOnly = true;
            dtgReimbursmentRequestersList.Columns["ReimbursAproveComments1"].Visible = true;
            dtgReimbursmentRequestersList.Columns["ReimbursAproveComments1"].Width = 300;
            dtgReimbursmentRequestersList.Columns["RequestStatus"].ReadOnly = true;
            dtgReimbursmentRequestersList.Columns["RequestStatus"].Visible = true;
            dtgReimbursmentRequestersList.Columns["RequestStatus"].Width = 100;
            dtgReimbursmentRequestersList.Columns["ReimbursAproveBy1"].Visible = false;
            dtgReimbursmentRequestersList.Columns["ReimbursAproveBy2"].Visible = false;
            dtgReimbursmentRequestersList.Columns["ReimbursRequestFinalStatus"].Visible = false;
            dtgReimbursmentRequestersList.Columns["ReimbursAproveStatus1"].Visible = false;
            dtgReimbursmentRequestersList.Columns["ReimbursAproveStatus2"].Visible = false;
            dtgReimbursmentRequestersList.Columns["IsActive"].Visible = false;
            dtgReimbursmentRequestersList.Columns["IsDeleted"].Visible = false;
            //dtgReimbursmentRequestersList.Columns["ClientID"].Visible = false;
        }

        private void frmAdvanceApprovalList_KeyDown(object sender, KeyEventArgs e)
        {
            //if (lblActionMode.Text != "")
            {
                if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            objDashboard.lblDashboardTitle.Text = "Dashboard";
            objDashboard.sptrDashboardContainer.Visible = true;
            objDashboard.grpDashboardDateRange.Visible = true;
            objDashboard.refreshDashboardCharts();
            this.Close();
        }

        private void btnChart_Click(object sender, EventArgs e)
        {
        }

        private void cmbChartType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SeriesChartType selectedType = (SeriesChartType)cmbChartType.SelectedItem;
            //chrtLeaveSummary.Series[0].ChartType = selectedType;
        }

        private void dtgAdvanceRequestersList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dtgAdvanceRequestersList.Columns[e.ColumnIndex].Name == "ApproverRequestedToComments1")
            {
                int AdvanceRequestID = Convert.ToInt32(dtgAdvanceRequestersList.Rows[e.RowIndex].Cells["EmpAdvanceRequestID"].Value);

                string CurrentStatus = dtgAdvanceRequestersList.Rows[e.RowIndex].Cells["ApproverRequestedToComments1"].Value?.ToString();

                using (var frm = new frmUpdateAdvanceStatus(AdvanceRequestID, CurrentStatus))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if(frm.ApplyToAllRows == true)
                        {
                            foreach (DataGridViewRow dc in dtgAdvanceRequestersList.Rows)
                            {
                                dc.Cells["Select"].Value = true;
                                dc.Cells["ApproverRequestedToComments1"].Value = frm.SelectedStatus;
                            }
                        }
                        btnSaveDetails.Enabled = true;
                        dtgAdvanceRequestersList.Rows[e.RowIndex].Cells["Select"].Value = true;
                        dtgAdvanceRequestersList.Rows[e.RowIndex].Cells["ApproverRequestedToComments1"].Value = frm.SelectedStatus;
                    }
                }
            }
            else if (dtgAdvanceRequestersList.Columns[e.ColumnIndex].Name == "ApproverEmpCode1") //ApproverEmpID1
            {
                frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listAdvanceFirstApproverEmpCode1", objTempClientFinYearInfo.ClientID);
                frmEmployeeList.ShowDialog(this);
            }
            else if (dtgAdvanceRequestersList.Columns[e.ColumnIndex].Name == "ApproverEmpCode2") //ApproverEmpID1
            {
                frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listAdvanceSecondApproverEmpCode1", objTempClientFinYearInfo.ClientID);
                frmEmployeeList.ShowDialog(this);
            }
            else if (dtgAdvanceRequestersList.Columns[e.ColumnIndex].Name == "RequestMovedToComments")
            {
                int AdvanceRequestID = Convert.ToInt32(dtgAdvanceRequestersList.Rows[e.RowIndex].Cells["EmpAdvanceRequestID"].Value);

                string CurrentStatus = dtgAdvanceRequestersList.Rows[e.RowIndex].Cells["RequestMovedToComments"].Value?.ToString();

                using (var frm = new frmUpdateAdvanceStatus(AdvanceRequestID, CurrentStatus))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (frm.ApplyToAllRows == true)
                        {
                            foreach (DataGridViewRow dc in dtgAdvanceRequestersList.Rows)
                            {
                                dc.Cells["Select"].Value = true;
                                dc.Cells["RequestMovedToComments"].Value = frm.SelectedStatus;
                            }
                        }
                        btnSaveDetails.Enabled = true;
                        dtgAdvanceRequestersList.Rows[e.RowIndex].Cells["Select"].Value = true;
                        dtgAdvanceRequestersList.Rows[e.RowIndex].Cells["RequestMovedToComments"].Value = frm.SelectedStatus;
                    }
                }
            }
        }

        private void picRefresh_Click(object sender, EventArgs e)
        {
            btnSaveDetails.Enabled = false;
            GetAdvancePendingList(strApproverType);
            GetAssetRequesterPendingList(strApproverType);
            GetReimbursmentRequesterPendingList(strApproverType);
        }

        private void frmAdvanceApprovalList_Activated(object sender, EventArgs e)
        {
            dtgAdvanceRequestersList.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
        }

        private void dtgAdvanceRequestersList_Paint(object sender, PaintEventArgs e)
        {
            KryptonDataGridView dgv = sender as KryptonDataGridView;

            if (dgv.Rows.Count == 0)
            {
                string message = "No Data Available";

                using (System.Drawing.Font font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold))
                {
                    SizeF size = e.Graphics.MeasureString(message, font);

                    e.Graphics.DrawString(message, font, System.Drawing.Brushes.Gray, (dgv.Width - size.Width) / 2, (dgv.Height - size.Height) / 2);
                }
            }
        }

        private void dtgAssetsRequestersList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dtgAssetsRequestersList.Columns[e.ColumnIndex].Name == "RequestStatus")
            {
                int AdvanceRequestID = Convert.ToInt32(dtgAssetsRequestersList.Rows[e.RowIndex].Cells["assetRequestID"].Value);

                string CurrentStatus = dtgAssetsRequestersList.Rows[e.RowIndex].Cells["RequestStatus"].Value?.ToString();

                using (var frm = new frmUpdateAdvanceStatus(AdvanceRequestID, CurrentStatus))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (frm.ApplyToAllRows == true)
                        {
                            foreach (DataGridViewRow dc in dtgAssetsRequestersList.Rows)
                            {
                                dc.Cells["Select"].Value = true;
                                dc.Cells["RequestStatus"].Value = frm.SelectedStatus;
                            }
                        }
                        btnSaveDetails.Enabled = true;
                        dtgAssetsRequestersList.Rows[e.RowIndex].Cells["Select"].Value = true;
                        dtgAssetsRequestersList.Rows[e.RowIndex].Cells["RequestStatus"].Value = frm.SelectedStatus;
                    }
                }
            }
        }

        private void picRefresh1_Click(object sender, EventArgs e)
        {
            btnSaveDetails.Enabled = false;
            GetAdvancePendingList(strApproverType);
            GetAssetRequesterPendingList(strApproverType);
            GetReimbursmentRequesterPendingList(strApproverType);
        }

        private void picRefresh2_Click(object sender, EventArgs e)
        {
            btnSaveDetails.Enabled = false;
            GetAdvancePendingList(strApproverType);
            GetAssetRequesterPendingList(strApproverType);
            GetReimbursmentRequesterPendingList(strApproverType);
        }

        private void dtgAssetsRequestersList_Paint(object sender, PaintEventArgs e)
        {
            KryptonDataGridView dgv = sender as KryptonDataGridView;

            if (dgv.Rows.Count == 0)
            {
                string message = "No Data Available";

                using (System.Drawing.Font font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold))
                {
                    SizeF size = e.Graphics.MeasureString(message, font);

                    e.Graphics.DrawString(message, font, System.Drawing.Brushes.Gray, (dgv.Width - size.Width) / 2, (dgv.Height - size.Height) / 2);
                }
            }
        }

        private void dtgReimbursmentRequestersList_Paint(object sender, PaintEventArgs e)
        {
            KryptonDataGridView dgv = sender as KryptonDataGridView;

            if (dgv.Rows.Count == 0)
            {
                string message = "No Data Available";

                using (System.Drawing.Font font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold))
                {
                    SizeF size = e.Graphics.MeasureString(message, font);

                    e.Graphics.DrawString(message, font, System.Drawing.Brushes.Gray, (dgv.Width - size.Width) / 2, (dgv.Height - size.Height) / 2);
                }
            }
        }

        private void dtgReimbursmentRequestersList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dtgReimbursmentRequestersList.Columns[e.ColumnIndex].Name == "RequestStatus")
            {
                int ReimbursRequestID = Convert.ToInt32(dtgReimbursmentRequestersList.Rows[e.RowIndex].Cells["ReimbursRequestID"].Value);

                string CurrentStatus = dtgReimbursmentRequestersList.Rows[e.RowIndex].Cells["RequestStatus"].Value?.ToString();

                using (var frm = new frmUpdateAdvanceStatus(ReimbursRequestID, CurrentStatus))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        if (frm.ApplyToAllRows == true)
                        {
                            foreach (DataGridViewRow dc in dtgReimbursmentRequestersList.Rows)
                            {
                                dc.Cells["Select"].Value = true;
                                dc.Cells["RequestStatus"].Value = frm.SelectedStatus;
                            }
                        }
                        btnSaveDetails.Enabled = true;
                        dtgReimbursmentRequestersList.Rows[e.RowIndex].Cells["Select"].Value = true;
                        dtgReimbursmentRequestersList.Rows[e.RowIndex].Cells["RequestStatus"].Value = frm.SelectedStatus;
                    }
                }
            }
        }

        public void SelectedEmployeeID(string SearchOptionSelectedForm, int selectedEmployeeID)
        {
            if (SearchOptionSelectedForm.Trim() == "listAdvanceFirstApproverEmpCode1")
            {
                dtgAdvanceRequestersList.CurrentRow.Cells["ApproverEmpID1"].Value = selectedEmployeeID;
                EmployeeInfo objEmployeeInfo = objEmployeeMaster.GetSelectedEmployeeInfo(Convert.ToInt32(selectedEmployeeID));
                dtgAdvanceRequestersList.CurrentRow.Cells["ApproverEmpCode1"].Value = objEmployeeInfo.EmpCode;
                dtgAdvanceRequestersList.CurrentRow.Cells["ApproverEmpName1"].Value = objEmployeeInfo.EmpName;

                EmpPersonalPersonalInfo objSelectedPersonalInfo = objEmployeePersonalInfo.GetEmpPersonalPersonalInfo(Convert.ToInt32(selectedEmployeeID));
                dtgAdvanceRequestersList.CurrentRow.Cells["ApproverEmpMailID1"].Value = objSelectedPersonalInfo.ContactNumber2;
            }
            else if (SearchOptionSelectedForm.Trim() == "listAdvanceSecondApproverEmpCode1")
            {
                dtgAdvanceRequestersList.CurrentRow.Cells["ApproverEmpID2"].Value = selectedEmployeeID;
                EmployeeInfo objEmployeeInfo = objEmployeeMaster.GetSelectedEmployeeInfo(Convert.ToInt32(selectedEmployeeID));
                dtgAdvanceRequestersList.CurrentRow.Cells["ApproverEmpCode2"].Value = objEmployeeInfo.EmpCode;
                dtgAdvanceRequestersList.CurrentRow.Cells["ApproverEmpName2"].Value = objEmployeeInfo.EmpName;

                EmpPersonalPersonalInfo objSelectedPersonalInfo = objEmployeePersonalInfo.GetEmpPersonalPersonalInfo(Convert.ToInt32(selectedEmployeeID));
                dtgAdvanceRequestersList.CurrentRow.Cells["ApproverEmpMailID2"].Value = objSelectedPersonalInfo.ContactNumber2;
            }
        }
    }
}
