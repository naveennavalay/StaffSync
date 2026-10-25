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
    public partial class frmEmpReimbursmentPayments : Form
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

        public frmEmpReimbursmentPayments()
        {
            InitializeComponent();
            clearControls();
        }

        public frmEmpReimbursmentPayments(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objActiveClientInfo = objClientInfo.getClientInfoByEmpID(objTempCurrentlyLoggedInUserInfo.EmpID);
            clearControls();
        }

        public frmEmpReimbursmentPayments(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
            objActiveClientInfo = objClientInfo.getClientInfoByEmpID(objTempCurrentlyLoggedInUserInfo.EmpID);
            clearControls();
        }

        public frmEmpReimbursmentPayments(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo, DateTime dtFrom, DateTime dtTo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
            objActiveClientInfo = objClientInfo.getClientInfoByEmpID(objTempCurrentlyLoggedInUserInfo.EmpID);
            clearControls();
        }

        public frmEmpReimbursmentPayments(int txtEmployeeID, int txtLeaveMasID)
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
            dtReimbRequestDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
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
                int transactionID = objEmpReimbursRequestInfo.InsertEmployeeReimbursmentRequestDetailedInfo(Convert.ToInt32(lblReimbRequestID.Text), Convert.ToDateTime(dtReimbRequestDate.Text.ToString()),  Convert.ToDecimal(txtOBalance.Text), Convert.ToDecimal(0), Convert.ToDecimal(txtReimbursementAmount.Text), Convert.ToDecimal(txtCurrentBalance.Text), "Dr", txtComments.Text);
                if (transactionID > 0)
                {
                    if(Convert.ToDecimal(txtCurrentBalance.Text) == 0)
                    {
                        objEmpReimbursRequestInfo.UpdateEmployeeReimbursmentRequestStatus(Convert.ToInt32(lblReimbRequestID.Text), "Completed");
                    }
                    else if (Convert.ToDecimal(txtCurrentBalance.Text) > 0)
                    {
                        objEmpReimbursRequestInfo.UpdateEmployeeReimbursmentRequestStatus(Convert.ToInt32(lblReimbRequestID.Text), "In Progress");
                    }

                    dtgReimbursmentPaymentHistory.DataSource = null;
                    dtgReimbursmentPaymentHistory.DataSource = objEmpReimbursRequestInfo.getReimbursmentTransactionList(Convert.ToInt32(lblReimbRequestID.Text.ToString()));
                    formatTransactionGrid();

                    MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    disableControls();
                    clearControls();
                    errValidator.Clear();
                }
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
                if (txtReimbursementAmount.Text.ToString().Trim() == "")
                {
                    validationStatus = false;
                    errValidator.SetError(this.txtReimbursementAmount, txtReimbursementAmount.Tag?.ToString() ?? "Reimbursment Amount should not be Blank.");
                }
                if (Convert.ToDecimal(txtReimbursementAmount.Text.ToString()) <= 0)
                {
                    validationStatus = false;
                    errValidator.SetError(this.txtReimbursementAmount, txtReimbursementAmount.Tag?.ToString() ?? "Reimbursment Amount should not be Zero.");
                }

                DateTime reimbTRDate = DateTime.ParseExact(dtReimbRequestDate.Text, dateFormat, provider);
                if (reimbTRDate > DateTime.Today)
                {
                    errValidator.SetError(dtReimbRequestDate, "Transaction Date cannot be future date");
                    validationStatus = false;
                }

                if (Convert.ToDecimal(txtCurrentBalance.Text.ToString()) < 0)
                {
                    validationStatus = false;
                    errValidator.SetError(this.txtCurrentBalance, txtCurrentBalance.Tag?.ToString() ?? "Outstanding Balance should not be less than Zero.");
                }
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
            txtOBalance.Text = "0.00";
            txtTotalCredit.Text = "0.00";
            txtTotalDebit.Text = "0.00";
            txtCurrentBalance.Text = "0.00";
            txtRequestCode.Text = "";
            txtReimbType.Text = "";
            txtRequestDate.Text = "";
            txtReimbStatus.Text = "";
            txtRequestDescription.Text = "";
            txtComments.Text = "";
            txtEmpCode.Text = "";
            txtEmpName.Text = "";
            txtDesignation.Text = "";
            txtDepartment.Text = "";
            txtReimbursementAmount.Text = "0.00";
            txtRequestDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            txtComments.Text = "";
            picEmpPhoto.Image  = null;
            dtgReimbursmentPaymentHistory.DataSource = null;
            dtgReimbursmentPaymentHistory.DataSource = objEmpReimbursRequestInfo.getReimbursmentTransactionList(0);
            formatTransactionGrid();

            lblEmpID.Text = "";
            lblReimbRequestID.Text = "";
            lblReimbTypeID.Text = "";
            lblPersonalInfoID.Text = "";
        }

        public void enableControls()
        {
            btnReimbursmentSearch.Enabled = true;
            txtReimbursementAmount.Text = "0.00";
            txtReimbursementAmount.Enabled = true;
            dtReimbRequestDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
            dtReimbRequestDate.Enabled = true;
            txtComments.Enabled = true;
            btnSaveDetails.Enabled = false;
        }

        public void disableControls()
        {
            txtRequestCode.Enabled = false;
            txtReimbType.Enabled = false;
            txtRequestDate.Enabled = false;
            txtReimbStatus.Enabled = false;
            txtRequestDescription.Enabled = false;
            txtComments.Enabled = false;
            txtEmpCode.Enabled = false;
            txtEmpName.Enabled = false;
            txtDesignation.Enabled = false;
            txtDepartment.Enabled = false;
            txtReimbursementAmount.Text = "0.00";
            txtReimbursementAmount.Enabled = false;
            txtRequestDate.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            dtReimbRequestDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
            dtReimbRequestDate.Enabled = false;
            txtRequestDate.Enabled = false;
            txtComments.Enabled = false;
            picEmpPhoto.Image = null;
            btnReimbursmentSearch.Enabled = false;
            btnSaveDetails.Enabled = false;
            //txtLeaveApprovalDate.Enabled = false;
            //dtgBulkLeaveApproval.Enabled = false;
        }

        public void onGenerateButtonClick()
        {
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = dtgReimbursmentPaymentHistory != null &&
                                      dtgReimbursmentPaymentHistory.Rows.Count > 0;
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
            else if (e.Control && e.KeyCode == Keys.N)
            {
                e.SuppressKeyPress = true;
                btnGenerateDetails_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                e.SuppressKeyPress = true;
                btnModifyDetails_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                e.SuppressKeyPress = true;
                btnRemoveDetails_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                e.SuppressKeyPress = true;
                btnSaveDetails_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.X)
            {
                e.SuppressKeyPress = true;
                btnCancel_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.Z)
            {
                e.SuppressKeyPress = true;
                btnCloseMe_Click(sender, e);
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
                frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listReimbRequestPaymentPendingList", Convert.ToInt32(objTempClientFinYearInfo.ClientID.ToString()));
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
            if (SearchOptionSelectedForm == "listReimbRequestPaymentPendingList")
            {
                List<ReimbursementPaymentsPendingList> objReimbursementPaymentsPendingList = objEmpReimbursRequestInfo.getSelectedReimbursmentPaymentPendingList(selectedEmployeeID);

                lblReimbTypeID.Text = objReimbursementPaymentsPendingList[0].ReimbID.ToString();
                lblReimbRequestID.Text = objReimbursementPaymentsPendingList[0].ReimbRequestID.ToString();
                txtRequestCode.Text = objReimbursementPaymentsPendingList[0].ReimbRequestCode.ToString();
                txtReimbType.Text = objReimbursementPaymentsPendingList[0].ReimbTitle.ToString();
                txtRequestDescription.Text = objReimbursementPaymentsPendingList[0].ReimbRequestDescription.ToString();
                txtReimbStatus.Text = objReimbursementPaymentsPendingList[0].ReimbRequestStatus.ToString();
                txtRequestDate.Text = Convert.ToDateTime(objReimbursementPaymentsPendingList[0].ReimbRequestDate.ToString()).ToString("dd-MMM-yyyy");
                dtReimbRequestDate.Text = DateTime.Today.ToString("dd-MM-yyyy");
                lblEmpID.Text = objReimbursementPaymentsPendingList[0].EmpID.ToString();
                txtEmpCode.Text = objReimbursementPaymentsPendingList[0].EmpCode.ToString();
                txtEmpName.Text = objReimbursementPaymentsPendingList[0].EmpName.ToString();
                txtDesignation.Text = objReimbursementPaymentsPendingList[0].DesignationTitle.ToString();
                txtDepartment.Text = objReimbursementPaymentsPendingList[0].DepartmentTitle.ToString();
                txtOBalance.Text = objReimbursementPaymentsPendingList[0].CBalance.ToString();
                txtCurrentBalance.Text = objReimbursementPaymentsPendingList[0].CBalance.ToString();

                picEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(lblEmpID.Text)).EmpPhoto);
                
                dtgReimbursmentPaymentHistory.DataSource = null;
                dtgReimbursmentPaymentHistory.DataSource = objEmpReimbursRequestInfo.getReimbursmentTransactionList(Convert.ToInt32(lblReimbRequestID.Text));
                formatTransactionGrid();

                CalculateTransactionSummary();
                txtCurrentBalance.Text = objReimbursementPaymentsPendingList[0].CBalance.ToString();
            }
        }

        private void formatTransactionGrid()
        {
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestDetailID"].Visible = false;
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestID"].Visible = false;
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestTRDate"].HeaderText = "Date";
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestTRDate"].Width = 100;
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestTRDate"].ReadOnly = true;
            dtgReimbursmentPaymentHistory.Columns["ReimbRequestTRDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgReimbursmentPaymentHistory.Columns["OBalance"].HeaderText = "O. Balance";
            dtgReimbursmentPaymentHistory.Columns["OBalance"].Width = 100;
            dtgReimbursmentPaymentHistory.Columns["OBalance"].ReadOnly = true;
            dtgReimbursmentPaymentHistory.Columns["OBalance"].DefaultCellStyle.Format = "c2";
            dtgReimbursmentPaymentHistory.Columns["OBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dtgReimbursmentPaymentHistory.Columns["CrAmount"].HeaderText = "Cr. Amount";
            dtgReimbursmentPaymentHistory.Columns["CrAmount"].Width = 100;
            dtgReimbursmentPaymentHistory.Columns["CrAmount"].ReadOnly = true;
            dtgReimbursmentPaymentHistory.Columns["CrAmount"].DefaultCellStyle.Format = "c2";
            dtgReimbursmentPaymentHistory.Columns["CrAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dtgReimbursmentPaymentHistory.Columns["DrAmount"].HeaderText = "Dr. Amount";
            dtgReimbursmentPaymentHistory.Columns["DrAmount"].Width = 100;
            dtgReimbursmentPaymentHistory.Columns["DrAmount"].ReadOnly = true;
            dtgReimbursmentPaymentHistory.Columns["DrAmount"].DefaultCellStyle.Format = "c2";
            dtgReimbursmentPaymentHistory.Columns["DrAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dtgReimbursmentPaymentHistory.Columns["CBalance"].HeaderText = "C. Balance";
            dtgReimbursmentPaymentHistory.Columns["CBalance"].Width = 100;
            dtgReimbursmentPaymentHistory.Columns["CBalance"].ReadOnly = true;
            dtgReimbursmentPaymentHistory.Columns["CBalance"].DefaultCellStyle.Format = "c2";
            dtgReimbursmentPaymentHistory.Columns["CBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dtgReimbursmentPaymentHistory.Columns["TRType"].HeaderText = "Trans. Type";
            dtgReimbursmentPaymentHistory.Columns["TRType"].Width = 100;
            dtgReimbursmentPaymentHistory.Columns["TRType"].ReadOnly = true;
            dtgReimbursmentPaymentHistory.Columns["Comments"].HeaderText = "Comments";
            dtgReimbursmentPaymentHistory.Columns["Comments"].Width = 350;
            dtgReimbursmentPaymentHistory.Columns["Comments"].ReadOnly = true;
            dtgReimbursmentPaymentHistory.Columns["OrderID"].Visible = false;
        }

        private void CalculateTransactionSummary()
        {
            decimal totalCredit = 0m;
            decimal totalDebit = 0m;

            foreach (DataGridViewRow row in dtgReimbursmentPaymentHistory.Rows)
            {
                if (row.IsNewRow)
                    continue;

                decimal.TryParse(Convert.ToString(row.Cells["CrAmount"].Value), out decimal credit);

                decimal.TryParse(Convert.ToString(row.Cells["DrAmount"].Value), out decimal debit);

                totalCredit += credit;
                totalDebit += debit;
            }

            decimal openingBalance = 0m;

            decimal.TryParse(txtOBalance.Text, out openingBalance);

            decimal currentBalance = openingBalance - totalDebit;

            txtTotalCredit.Text = totalCredit.ToString("N2");
            txtTotalDebit.Text = totalDebit.ToString("N2");
            txtCurrentBalance.Text = currentBalance.ToString("N2");
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
            if (!string.IsNullOrEmpty(txtReimbursementAmount.Text))
            {
                if (Convert.ToDecimal(txtReimbursementAmount.Text) >= 0)
                {
                    decimal currentBalance = Convert.ToDecimal(txtOBalance.Text) - Convert.ToDecimal(txtReimbursementAmount.Text);
                    txtCurrentBalance.Text = currentBalance.ToString("N2");
                }
            }
            else
                txtReimbursementAmount.Text = "0.00";
        }
    }
}
