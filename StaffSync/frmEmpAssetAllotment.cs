using DALStaffSync;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Krypton.Toolkit;
using ModelStaffSync;
using Org.BouncyCastle.Ocsp;
using StaffSync.Controls;
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
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static StaffSync.PDFComponent;
using static StaffSync.PDFComponent.SimplePdfGenerator;
using static System.Windows.Forms.MonthCalendar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StaffSync
{
    public partial class frmEmpAssetAllotment : Form
    {
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin(); 
        DALStaffSync.clsClientInfo objClientInfo = new DALStaffSync.clsClientInfo();
        DALStaffSync.clsEmployeeMaster objEmployeeInfo = new DALStaffSync.clsEmployeeMaster();
        DALStaffSync.clsDashboardChartWidgets objDashboardChartWidgets = new DALStaffSync.clsDashboardChartWidgets();
        DALStaffSync.clsClientStatutory objClientStatutory = new DALStaffSync.clsClientStatutory();
        DALStaffSync.clsClientBranchInfo clsClientBranchInfo = new DALStaffSync.clsClientBranchInfo();
        clsImpageOperation objImpageOperation = new clsImpageOperation();
        DALStaffSync.clsPhotoMas objPhotoMas = new DALStaffSync.clsPhotoMas();
        DALStaffSync.clsImportDataInfo objImportDataInfo = new DALStaffSync.clsImportDataInfo();
        DALStaffSync.clsEmpReimbursRequestInfo objEmpReimbursRequestInfo = new DALStaffSync.clsEmpReimbursRequestInfo();
        DALStaffSync.clsAssetRegister objAssetRegisterInfo = new DALStaffSync.clsAssetRegister();

        frmDashboard objDashboard = (frmDashboard)System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        List<ClientInfo> objActiveClientInfo = new List<ClientInfo>();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();

        // Import/Export preview state

        DateTime dtFromDate = DateTime.Today;
        DateTime dtToDate = DateTime.Today;

        private DataTable objImportDataPreviewTable = new DataTable();
        // Employee Information keeps its complete detail rows separately from the
        // one-row-per-employee preview grid so selecting an employee still exports
        // all optional detail records without duplicating employees in the UI.
        private DataTable objEmployeeInformationExportTable = new DataTable();
        private string objSelectedImportFilePath = string.Empty;
        private bool isImportPreviewEventsAttached = false;
        private bool isExportPreviewEventsAttached = false;
        // Download template button state
        private bool isDownloadTemplateEventsAttached = false;

        private void Control_CellValueChangedCustom(object sender, CellValueChangedEventArgs e)
        {
            // Fires immediately when dropdown changes
            //MessageBox.Show($"RowID: {e.Change.RowIndex}, Column: {e.Change.ColumnIndex}, ColumnName: {e.Change.ColumnName}, OldValue: {e.Change.OldValue}, NewValue: {e.Change.NewValue}");
        }

        public frmEmpAssetAllotment()
        {
            InitializeComponent();
            clearControls();
        }

        public frmEmpAssetAllotment(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objActiveClientInfo = objClientInfo.getClientInfoByEmpID(objTempCurrentlyLoggedInUserInfo.EmpID);
            clearControls();
        }

        public frmEmpAssetAllotment(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
            objActiveClientInfo = objClientInfo.getClientInfoByEmpID(objTempCurrentlyLoggedInUserInfo.EmpID);
            clearControls();
        }

        public frmEmpAssetAllotment(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo, DateTime dtFrom, DateTime dtTo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
            objActiveClientInfo = objClientInfo.getClientInfoByEmpID(objTempCurrentlyLoggedInUserInfo.EmpID);
            clearControls();
        }

        public frmEmpAssetAllotment(int txtEmployeeID, int txtLeaveMasID)
        {
            InitializeComponent();
            clearControls();
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            objDashboard.lblDashboardTitle.Text = "Dashboard";
            objDashboard.sptrDashboardContainer.Visible = true;
            objDashboard.grpDashboardDateRange.Visible = true;
            objDashboard.refreshDashboardCharts();
            this.Close();
        }

        private void frmEmpReimbursmentPayout_Load(object sender, EventArgs e)
        {
            FocusManager.EnableHighlighting = false;
            FocusManager.ShowNavigationError = true;
            FocusManager.Register(this);
            FocusManager.SetFocus(btnGenerateDetails);

            // TODO: This line of code loads data into the 'staffsyncDBDataSet1.qryAllEmpLeavePendingStatement' table. You can move, or remove it, as needed.
            //this.qryAllEmpLeavePendingStatementTableAdapter.Fill(this.staffsyncDBDataSet1.qryAllEmpLeavePendingStatement);
            //// TODO: This line of code loads data into the 'staffsyncDBDTSet.EmpMasInfo' table. You can move, or remove it, as needed.
            //this.empMasInfoTableAdapter.Fill(this.staffsyncDBDTSet.EmpMasInfo);
            disableControls();
        }

        private void btnCloseMe_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbRelationship_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dtgImportDataSourceList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // The original Import form used this button to select an external
            // CSV/Excel file. Export Data Process does not need a source file.
            // The data source is the selected StaffSync master on the left.

        }

        private void btnCloseMe_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenerateDetails_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string strValidationMessage = objLogin.ValidateUserRolesAndResponsibilitiesInfo(objTempCurrentlyLoggedInUserInfo.EmpID, "add");
            if (strValidationMessage != "Success")
            {
                MessageBox.Show(strValidationMessage, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblActionMode.Text = "add";
            onGenerateButtonClick();
            clearControls();
            enableControls();
            errValidator.Clear();

            txtComments.Text = "To Reimbursment";

            btnSaveDetails.Enabled = true;
            dtIssueDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
        }

        /// <summary>
        /// Exports only the rows currently selected in the preview grid.
        /// Supported formats: PDF, CSV, XML and JSON.
        /// </summary>
        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            string strValidationMessage = objLogin.ValidateUserRolesAndResponsibilitiesInfo(objTempCurrentlyLoggedInUserInfo.EmpID, "");
            if (strValidationMessage != "Success")
            {
                MessageBox.Show(strValidationMessage, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(ValidateValuesOnUI())
            {
                foreach (DataGridViewRow dc in dtgAssetRequestedList.Rows)
                {

                }
                //int transactionID = objAssetRegisterInfo.UpdateAssetRegisterInfo()
                //if (transactionID > 0)
                //{
                //    if(Convert.ToDecimal(txtCurrentBalance.Text) == 0)
                //    {
                //        objEmpReimbursRequestInfo.UpdateEmployeeReimbursmentRequestStatus(Convert.ToInt32(lblReimbRequestID.Text), "Completed");
                //    }
                //    else if (Convert.ToDecimal(txtCurrentBalance.Text) > 0)
                //    {
                //        objEmpReimbursRequestInfo.UpdateEmployeeReimbursmentRequestStatus(Convert.ToInt32(lblReimbRequestID.Text), "In Progress");
                //    }

                //    dtgReimbursmentPaymentHistory.DataSource = null;
                //    dtgReimbursmentPaymentHistory.DataSource = objEmpReimbursRequestInfo.getReimbursmentTransactionList(Convert.ToInt32(lblReimbRequestID.Text.ToString()));
                //    formatTransactionGrid();

                //    MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    disableControls();
                //    clearControls();
                //    errValidator.Clear();
                //}
            }
        }

        public bool ValidateValuesOnUI()
        {
            bool validationStatus = true;
            errValidator.Clear();

            DateTime dob, doj;
            string dateFormat = "dd-MM-yyyy";
            CultureInfo provider = CultureInfo.InvariantCulture;

            if (lblActionMode.Text == "add")
            {
                //if (txtReimbursementAmount.Text.ToString().Trim() == "")
                //{
                //    validationStatus = false;
                //    errValidator.SetError(this.txtReimbursementAmount, txtReimbursementAmount.Tag?.ToString() ?? "Reimbursment Amount should not be Blank.");
                //}
                //if (Convert.ToDecimal(txtReimbursementAmount.Text.ToString()) <= 0)
                //{
                //    validationStatus = false;
                //    errValidator.SetError(this.txtReimbursementAmount, txtReimbursementAmount.Tag?.ToString() ?? "Reimbursment Amount should not be Zero.");
                //}

                //DateTime reimbTRDate = DateTime.ParseExact(dtReimbRequestDate.Text, dateFormat, provider);
                //if (reimbTRDate > DateTime.Today)
                //{
                //    errValidator.SetError(dtReimbRequestDate, "Transaction Date cannot be future date");
                //    validationStatus = false;
                //}

                //if (Convert.ToDecimal(txtCurrentBalance.Text.ToString()) < 0)
                //{
                //    validationStatus = false;
                //    errValidator.SetError(this.txtCurrentBalance, txtCurrentBalance.Tag?.ToString() ?? "Outstanding Balance should not be less than Zero.");
                //}
                //if (Convert.ToDecimal(txtOutstandingQuantity.Text.ToString()) < Convert.ToDecimal(txtTotalQuantity.Text.ToString()))
                //{
                //    validationStatus = false;
                //    errValidator.SetError(this.txtOutstandingQuantity, txtOutstandingQuantity.Tag?.ToString() ?? "Total Outstanding Quantity should not be less than Total Quantity.");
                //}
            }
            return validationStatus;
        }

        public void clearControls()
        {
            txtRequestCode.Text = "";
            txtRequestDate.Text = "";
            txtReimbStatus.Text = "";
            txtRequestDescription.Text = "";
            txtComments.Text = "";
            txtEmpCode.Text = "";
            txtEmpName.Text = "";
            txtDesignation.Text = "";
            txtDepartment.Text = "";
            txtRequestDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            dtIssueDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
            dtReturnDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
            txtComments.Text = "";
            lblAssetRequestID.Text = "";
            lblClientID.Text = "";
            lblEmpID.Text = "";
            lblPersonalInfoID.Text = "";
            picEmpPhoto.Image  = null;

            cmbAssetCondition.DataSource = null;
            LoadAssetConditionValues();
            dtgAssetRequestedList.DataSource = null;
            dtgAssetRequestedList.DataSource = objAssetRegisterInfo.getAssetInformationOfAnAssetRequest(0);
            formatTransactionGrid();

            lblEmpID.Text = "";
            lblPersonalInfoID.Text = "";
        }

        public void enableControls()
        {
            btnReimbursmentSearch.Enabled = true;
            dtIssueDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
            dtReturnDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
            dtIssueDate.Enabled = true;
            txtComments.Enabled = true;
            btnSaveDetails.Enabled = true;
        }

        public void disableControls()
        {
            txtRequestCode.Enabled = false;
            txtRequestDate.Enabled = false;
            txtReimbStatus.Enabled = false;
            txtRequestDescription.Enabled = false;
            txtEmpCode.Enabled = false;
            txtEmpName.Enabled = false;
            txtDesignation.Enabled = false;
            txtDepartment.Enabled = false;
            txtRequestDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            dtIssueDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
            dtReturnDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
            dtIssueDate.Enabled = false;
            txtRequestDate.Enabled = false;
            txtComments.Enabled = false;
            picEmpPhoto.Image = null;
            btnReimbursmentSearch.Enabled = false;
            btnSaveDetails.Enabled = false;
            cmbAssetCondition.Enabled = false;
            //txtLeaveApprovalDate.Enabled = false;
            //dtgBulkLeaveApproval.Enabled = false;
        }

        public void onGenerateButtonClick()
        {
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = dtgAssetRequestedList != null &&
                                      dtgAssetRequestedList.Rows.Count > 0;
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
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onSaveButtonClick()
        {
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onCancelButtonClick()
        {
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = true;
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

        private void LoadAssetConditionValues()
        {
            cmbAssetCondition.Items.Clear();
            cmbAssetCondition.Items.Add("");
            cmbAssetCondition.Items.Add("New");
            cmbAssetCondition.Items.Add("Excellent");
            cmbAssetCondition.Items.Add("Good");
            cmbAssetCondition.Items.Add("Fair");
            cmbAssetCondition.Items.Add("Poor");
            cmbAssetCondition.Items.Add("Damaged");
            cmbAssetCondition.Items.Add("Under Repair");
            cmbAssetCondition.Items.Add("Refurbished");
            cmbAssetCondition.SelectedIndex = 1;
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

        }

        private void dtgBulkLeaveApproval_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
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

        private void cmbAttendanceMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LoadAttendanceInfo();
            //attendanceGridControl1.SetCellValue(1, 15, "P"); // row 1, column 3 → "P"
        }

        private void attendanceGridControl1_DoubleClick(object sender, EventArgs e)
        {
            MessageBox.Show("Double Clicked on the grid", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void frmEmpReimbursmentPayout_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                objDashboard.lblDashboardTitle.Text = "Dashboard";
                objDashboard.sptrDashboardContainer.Visible = true;
                objDashboard.grpDashboardDateRange.Visible = true;
                objDashboard.refreshDashboardCharts();
                this.Close();
            }
        }

        private void frmEmpReimbursmentPayout_Activated(object sender, EventArgs e)
        {

        }

        private void dtgConsolidatedAttendanceReport_Paint(object sender, PaintEventArgs e)
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

        private void dtgImportDataSourceList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chkSelectOrUnselect_Click(object sender, EventArgs e)
        {

        }

        private void btnEmpSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnReimbursmentSearch_Click(object sender, EventArgs e)
        {
            if (lblActionMode.Text == "add")
            {
                frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listAssetRequestPendingList", Convert.ToInt32(objTempClientFinYearInfo.ClientID.ToString()));
                frmEmployeeList.ShowDialog(this);
            }
            else if (lblActionMode.Text == "modify")
            {
                frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listReimbRequestPaymentPendingList", Convert.ToInt32(objTempClientFinYearInfo.ClientID.ToString()));
                frmEmployeeList.ShowDialog(this);
            }
        }

        public void SelectedEmployeeID(string SearchOptionSelectedForm, int selectedEmployeeID)
        {
            if (SearchOptionSelectedForm == "listAssetRequestPendingList")
            {
                List<ApprovedAssetInformationRequestList> objApprovedAssetInformationRequestList = objAssetRegisterInfo.getApprovedAssetInformationRequestList(Convert.ToInt32(objTempClientFinYearInfo.ClientID.ToString()));

                lblAssetRequestID.Text = objApprovedAssetInformationRequestList[0].AssetRequestID.ToString();
                txtRequestCode.Text = objApprovedAssetInformationRequestList[0].AssetRequestCode.ToString();
                txtRequestDate.Text = objApprovedAssetInformationRequestList[0].AssetRequestDate.ToString();
                txtReimbStatus.Text = objApprovedAssetInformationRequestList[0].RequestStatus.ToString();
                txtRequestDescription.Text = objApprovedAssetInformationRequestList[0].AssetRequestComments.ToString();

                lblEmpID.Text = objApprovedAssetInformationRequestList[0].EmpID.ToString();
                lblPersonalInfoID.Text = objApprovedAssetInformationRequestList[0].PersonalInfoID.ToString();
                txtEmpCode.Text = objApprovedAssetInformationRequestList[0].EmpCode.ToString();
                txtEmpName.Text = objApprovedAssetInformationRequestList[0].EmpName.ToString();
                txtDesignation.Text = objApprovedAssetInformationRequestList[0].DesignationTitle.ToString();
                txtDepartment.Text = objApprovedAssetInformationRequestList[0].DepartmentTitle.ToString();
                picEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(lblEmpID.Text)).EmpPhoto);
                
                dtgAssetRequestedList.DataSource = null;
                dtgAssetRequestedList.DataSource = objAssetRegisterInfo.getAssetInformationOfAnAssetRequest(Convert.ToInt32(lblAssetRequestID.Text));
                formatTransactionGrid();
            }
        }

        private void formatTransactionGrid()
        {
            dtgAssetRequestedList.Columns["EmpAssetAllocationID"].Visible = false;
            dtgAssetRequestedList.Columns["AssetCatMasID"].Visible = false;
            dtgAssetRequestedList.Columns["AssetCategoryName"].HeaderText = "Category";
            dtgAssetRequestedList.Columns["AssetCategoryName"].Width = 150;
            dtgAssetRequestedList.Columns["AssetCategoryName"].ReadOnly = true;
            dtgAssetRequestedList.Columns["AssetID"].Visible = false;
            dtgAssetRequestedList.Columns["AssetCode"].HeaderText = "Asset Code";
            dtgAssetRequestedList.Columns["AssetCode"].Width = 150;
            dtgAssetRequestedList.Columns["AssetCode"].ReadOnly = true;
            dtgAssetRequestedList.Columns["AssetName"].HeaderText = "Asset Name";
            dtgAssetRequestedList.Columns["AssetName"].Width = 200;
            dtgAssetRequestedList.Columns["AssetName"].ReadOnly = true;
            dtgAssetRequestedList.Columns["AssetMasMoreDetID"].Visible = false;
            dtgAssetRequestedList.Columns["SerialNumber"].HeaderText = "Serial Number";
            dtgAssetRequestedList.Columns["SerialNumber"].Width = 150;
            dtgAssetRequestedList.Columns["SerialNumber"].ReadOnly = true;
            dtgAssetRequestedList.Columns["ModelNumber"].HeaderText = "Model Number";
            dtgAssetRequestedList.Columns["ModelNumber"].Width = 150;
            dtgAssetRequestedList.Columns["ModelNumber"].ReadOnly = true;
            dtgAssetRequestedList.Columns["AssetTag"].HeaderText = "Asset Tag";
            dtgAssetRequestedList.Columns["AssetTag"].Width = 150;
            dtgAssetRequestedList.Columns["AssetTag"].ReadOnly = true;
            dtgAssetRequestedList.Columns["AssetRequestID"].Visible = false;
            dtgAssetRequestedList.Columns["AssetRegID"].Visible = false;
            dtgAssetRequestedList.Columns["Comments"].HeaderText = "Request Description";
            dtgAssetRequestedList.Columns["Comments"].Width = 350;
            dtgAssetRequestedList.Columns["Comments"].ReadOnly = true;
            dtgAssetRequestedList.Columns["OBalance"].HeaderText = "Available Balance";
            dtgAssetRequestedList.Columns["OBalance"].Width = 150;
            dtgAssetRequestedList.Columns["OBalance"].ReadOnly = true;
            dtgAssetRequestedList.Columns["OBalance"].DefaultCellStyle.Format = "c2";
            dtgAssetRequestedList.Columns["OBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dtgAssetRequestedList.Columns["DrBalance"].HeaderText = "Requested Quantity";
            dtgAssetRequestedList.Columns["DrBalance"].Width = 150;
            dtgAssetRequestedList.Columns["DrBalance"].ReadOnly = true;
            dtgAssetRequestedList.Columns["DrBalance"].DefaultCellStyle.Format = "c2";
            dtgAssetRequestedList.Columns["DrBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dtgAssetRequestedList.Columns["CBalance"].HeaderText = "Outstanding Balance";
            dtgAssetRequestedList.Columns["CBalance"].Width = 150;
            dtgAssetRequestedList.Columns["CBalance"].ReadOnly = true;
            dtgAssetRequestedList.Columns["CBalance"].DefaultCellStyle.Format = "c2";
            dtgAssetRequestedList.Columns["CBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dtgAssetRequestedList.Columns["IssueDate"].HeaderText = "Issue Date";
            dtgAssetRequestedList.Columns["IssueDate"].Width = 150;
            dtgAssetRequestedList.Columns["IssueDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgAssetRequestedList.Columns["IssueCondition"].HeaderText = "Issue Condition";
            dtgAssetRequestedList.Columns["IssueCondition"].Width = 150;
            dtgAssetRequestedList.Columns["IssueRemarks"].HeaderText = "Remarks";
            dtgAssetRequestedList.Columns["IssueRemarks"].Width = 300;
            dtgAssetRequestedList.Columns["TRType"].Visible = false;
        }

        private void txtCurrentBalance_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtgReimbursmentPaymentHistory_Paint(object sender, PaintEventArgs e)
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

        private void txtReimbursementAmount_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
