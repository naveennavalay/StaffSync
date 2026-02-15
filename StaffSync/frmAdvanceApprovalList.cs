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
        DALStaffSync.clsAuditLog objAuditLog = new DALStaffSync.clsAuditLog();
        DALStaffSync.clsAdvanceTypeMas objAdvanceTypeMas = new DALStaffSync.clsAdvanceTypeMas();
        DALStaffSync.clsAdvanceTransaction objAdvanceTransaction = new DALStaffSync.clsAdvanceTransaction();
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
            GetAdvancePendingList(strApproverType);
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAdvanceApprovalList_Load(object sender, EventArgs e)
        {
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
            if (this.dtgAdvanceRequestersList.Rows.Count > 0)
            {
                if (MessageBox.Show("You are about to execute Bulk Advance Request Approval. \nAre you sure to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            string AdvanceRequestCode = "";


            if (strApproverType == "FirstApprover")
            {
                foreach (DataGridViewRow dc in dtgAdvanceRequestersList.Rows)
                {
                    if (Convert.ToBoolean(dc.Cells["Select"].Value.ToString()) == true)
                    {
                        int AdvanceRequestID = Convert.ToInt32(dc.Cells["EmpAdvanceRequestID"].Value);
                        string ApproverRequestedToComments1 = dc.Cells["ApproverRequestedToComments1"].Value?.ToString();
                        AdvanceRequestCode = dc.Cells["EmpAdvReqCode"].Value?.ToString();
                        objAdvanceTransaction.UpdateFirstApproverStatus(AdvanceRequestID, ApproverRequestedToComments1, DateTime.Now.Date);

                        if (ApproverRequestedToComments1 == "In Progress" || ApproverRequestedToComments1 == "Pending")
                        {
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AdvanceRequestID, "First Approver updated the Status for Advance Request : \"" + AdvanceRequestCode + "\" to \"" + ApproverRequestedToComments1 + "\"", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestStatusUpdate");
                        }
                        else if (ApproverRequestedToComments1 == "Approved")
                        {
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AdvanceRequestID, "First Approver updated the Status for Advance Request : \"" + AdvanceRequestCode + "\" to \"" + ApproverRequestedToComments1 + "\"", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestStatusUpdate");
                        }
                        else if (ApproverRequestedToComments1 == "Rejected")
                        {
                            objAdvanceTransaction.InsertAdvanceTransaction(dc.Cells["EmpAdvReqCode"].Value.ToString(), AdvanceRequestID, DateTime.Now.Date, 0, 0, 0, 0, "Cr", "By Advance Request : \"" + AdvanceRequestCode + "\" Rejection", 0);
                            objAdvanceTransaction.RejectOrCancleApproverStatus(AdvanceRequestID, "Rejected", DateTime.Now.Date);
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AdvanceRequestID, "First Approver updated the Status for Advance Request : \"" + AdvanceRequestCode + "\" to \"" + ApproverRequestedToComments1 + "\"", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestStatusUpdate");
                        }
                        else if (ApproverRequestedToComments1 == "Cancelled")
                        {
                            objAdvanceTransaction.InsertAdvanceTransaction(dc.Cells["EmpAdvReqCode"].Value.ToString(), AdvanceRequestID, DateTime.Now.Date, 0, 0, 0, 0, "Cr", "By Advance Request : \"" + AdvanceRequestCode + "\" Rejection", 0);
                            objAdvanceTransaction.RejectOrCancleApproverStatus(AdvanceRequestID, "Cancelled", DateTime.Now.Date);
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AdvanceRequestID, "First Approver updated the Status for Advance Request : \"" + AdvanceRequestCode + "\" to \"" + ApproverRequestedToComments1 + "\"", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestStatusUpdate");
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
                        int AdvanceRequestID = Convert.ToInt32(dc.Cells["EmpAdvanceRequestID"].Value);
                        string RequestMovedToComments = dc.Cells["RequestMovedToComments"].Value?.ToString();
                        AdvanceRequestCode = dc.Cells["EmpAdvReqCode"].Value?.ToString();
                        objAdvanceTransaction.UpdateSecondApproverStatus(AdvanceRequestID, RequestMovedToComments, DateTime.Now.Date);

                        if (RequestMovedToComments == "In Progress" || RequestMovedToComments == "Pending")
                        {
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AdvanceRequestID,"Second Approver updated the Status for Advance Request : \"" + AdvanceRequestCode + "\" to \"" + RequestMovedToComments + "\"", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestStatusUpdate");
                        }
                        else if (RequestMovedToComments == "Approved")
                        {
                            objAdvanceTransaction.InsertAdvanceTransaction(dc.Cells["EmpAdvReqCode"].Value.ToString(), AdvanceRequestID, DateTime.Now.Date, 0, Convert.ToDecimal(dc.Cells["AdvanceAmount"].Value.ToString()), 0, Convert.ToDecimal(dc.Cells["AdvanceAmount"].Value.ToString()), "Cr", "By Advance Request : \"" + AdvanceRequestCode + "\" Approval", 0);
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AdvanceRequestID, "Second Approver updated the Status for Advance Request : \"" + AdvanceRequestCode + "\" to \"" + RequestMovedToComments + "\"", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestStatusUpdate");
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AdvanceRequestID, "Advance Request : \"" + dc.Cells["EmpAdvReqCode"].Value.ToString() +  "\" raised by Employee Code : \"" + dc.Cells["RequesterEmpCode"].Value.ToString() + "\" and Employee Name : \"" + dc.Cells["RequesterEmpName"].Value.ToString() + "\" has been approved and credited", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestStatusUpdate");
                        }
                        else if (RequestMovedToComments == "Rejected")
                        {
                            objAdvanceTransaction.InsertAdvanceTransaction(dc.Cells["EmpAdvReqCode"].Value.ToString(), AdvanceRequestID, DateTime.Now.Date, 0, 0, 0, 0, "Cr", "By Advance Request : \"" + AdvanceRequestCode + "\" Rejection", 0);
                            objAdvanceTransaction.RejectOrCancleApproverStatus(AdvanceRequestID, "Rejected", DateTime.Now.Date);
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AdvanceRequestID, "Second Approver updated the Status for Advance Request : \"" + AdvanceRequestCode + "\" to \"" + RequestMovedToComments + "\"", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestStatusUpdate");
                        }
                        else if (RequestMovedToComments == "Cancelled")
                        {
                            objAdvanceTransaction.InsertAdvanceTransaction(dc.Cells["EmpAdvReqCode"].Value.ToString(), AdvanceRequestID, DateTime.Now.Date, 0, 0, 0, 0, "Cr", "By Advance Request : \"" + AdvanceRequestCode + "\" Rejection", 0);
                            objAdvanceTransaction.RejectOrCancleApproverStatus(AdvanceRequestID, "Cancelled", DateTime.Now.Date);
                            objAuditLog.InsertAuditLog(Convert.ToInt32(ModelStaffSync.CurrentUser.EmpID), AdvanceRequestID, "Second Approver updated the Status for Advance Request : \"" + AdvanceRequestCode + "\" to \"" + RequestMovedToComments + "\"", ModelStaffSync.CurrentUser.EmpName, "AdvanceRequestStatusUpdate");
                        }
                    }
                }
            }

            GetAdvancePendingList(strApproverType);

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
                dtgAdvanceRequestersList.DataSource = objAdvanceTransaction.AdvanceFirstApprovalPendingList(Convert.ToInt32(ModelStaffSync.CurrentUser.ClientID));
            else if (tmpApproverType == "SecondApprover")
                dtgAdvanceRequestersList.DataSource = objAdvanceTransaction.AdvanceSecondApprovalPendingList(Convert.ToInt32(ModelStaffSync.CurrentUser.ClientID));

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
                dtgAdvanceRequestersList.Columns["ApproverEmpID1"].Visible = false;
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
                dtgAdvanceRequestersList.Columns["ApproverEmpID2"].Visible = false;
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

        private void frmAdvanceApprovalList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
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
                        btnSaveDetails.Enabled = true;
                        dtgAdvanceRequestersList.Rows[e.RowIndex].Cells["Select"].Value = true;
                        dtgAdvanceRequestersList.Rows[e.RowIndex].Cells["ApproverRequestedToComments1"].Value = frm.SelectedStatus;
                    }
                }
            }
            else if (dtgAdvanceRequestersList.Columns[e.ColumnIndex].Name == "RequestMovedToComments")
            {
                int AdvanceRequestID = Convert.ToInt32(dtgAdvanceRequestersList.Rows[e.RowIndex].Cells["EmpAdvanceRequestID"].Value);

                string CurrentStatus = dtgAdvanceRequestersList.Rows[e.RowIndex].Cells["RequestMovedToComments"].Value?.ToString();

                using (var frm = new frmUpdateAdvanceStatus(AdvanceRequestID, CurrentStatus))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
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
        }
    }
}
