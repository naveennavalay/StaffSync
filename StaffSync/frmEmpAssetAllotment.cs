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

        // Issue Date cell calendar popup state. The existing data-bound grid is retained.
        private DateTimePicker issueDatePicker;
        private ToolStripDropDown issueDatePickerPopup;
        private ToolStripControlHost issueDatePickerHost;
        private DataGridViewCell activeIssueDateCell;
        private bool issueDatePickerEventsConfigured;

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
            InitializeIssueDatePicker();
            clearControls();
        }

        public frmEmpAssetAllotment(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            InitializeIssueDatePicker();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objActiveClientInfo = objClientInfo.getClientInfoByEmpID(objTempCurrentlyLoggedInUserInfo.EmpID);
            clearControls();
        }

        public frmEmpAssetAllotment(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            InitializeIssueDatePicker();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
            objActiveClientInfo = objClientInfo.getClientInfoByEmpID(objTempCurrentlyLoggedInUserInfo.EmpID);
            clearControls();
        }

        public frmEmpAssetAllotment(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo, DateTime dtFrom, DateTime dtTo)
        {
            InitializeComponent();
            InitializeIssueDatePicker();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
            objActiveClientInfo = objClientInfo.getClientInfoByEmpID(objTempCurrentlyLoggedInUserInfo.EmpID);
            clearControls();
        }

        public frmEmpAssetAllotment(int txtEmployeeID, int txtLeaveMasID)
        {
            InitializeComponent();
            InitializeIssueDatePicker();
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

            txtComments.Text = "To Issue: \n" + txtRequestDescription.Text;

            btnSaveDetails.Enabled = true;
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

            if (ValidateValuesOnUI())
            {
                int assetIssuedInfoID = 0;
                foreach (DataGridViewRow dc in dtgAssetRequestedList.Rows)
                {
                    int AssetID = Convert.ToInt32(dc.Cells["EmpAssetAllocationID"].Value);
                    assetIssuedInfoID = objAssetRegisterInfo.UpdatgeEmployeeAssetAllocationInfo(Convert.ToInt32(dc.Cells["EmpAssetAllocationID"].Value), Convert.ToInt32(dc.Cells["AssetID"].Value), Convert.ToDateTime(dc.Cells["IssueDate"].Value), dc.Cells["IssueCondition"].Value.ToString(), objTempCurrentlyLoggedInUserInfo.EmpID, dc.Cells["IssueRemarks"].Value.ToString(), cmbAssetCondition.SelectedIndex + 1, Convert.ToInt32(dc.Cells["AssetRequestID"].Value));
                }
                MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                disableControls();
                clearControls();
                errValidator.Clear();
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
                errValidator.Clear();

                // 1. Validate that an asset request has been selected.
                if (string.IsNullOrWhiteSpace(lblAssetRequestID.Text))
                {
                    validationStatus = false;
                    errValidator.SetError(this.btnReimbursmentSearch, txtRequestCode.Tag?.ToString() ?? "Please select an Asset Request before continuing.");
                }

                // 2. Validate IssueDate, IssueCondition and IssueRemarks
                //    for every row in the asset request grid.
                foreach (DataGridViewRow row in dtgAssetRequestedList.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    // Validate IssueDate.
                    object issueDateValue = row.Cells["IssueDate"].Value;

                    if (issueDateValue == null || issueDateValue == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(issueDateValue)))
                    {
                        validationStatus = false;
                        errValidator.SetError(this.dtgAssetRequestedList, dtgAssetRequestedList.Tag?.ToString() ?? $"Issue Date is required for asset row {row.Index + 1}.");
                        dtgAssetRequestedList.CurrentCell = row.Cells["IssueDate"];
                        return false;
                    }

                    DateTime issueDate;

                    if (!DateTime.TryParse(Convert.ToString(issueDateValue), CultureInfo.CurrentCulture, DateTimeStyles.None, out issueDate))
                    {
                        validationStatus = false;
                        errValidator.SetError(this.dtgAssetRequestedList, dtgAssetRequestedList.Tag?.ToString() ?? $"Please enter a valid Issue Date for asset row {row.Index + 1}.");
                        dtgAssetRequestedList.CurrentCell = row.Cells["IssueDate"];
                        return false;
                    }

                    if (issueDate.Date > DateTime.Today)
                    {
                        validationStatus = false;
                        errValidator.SetError(this.dtgAssetRequestedList, dtgAssetRequestedList.Tag?.ToString() ?? $"Issue Date cannot be a future date. Please correct asset row {row.Index + 1}.");
                        dtgAssetRequestedList.CurrentCell = row.Cells["IssueDate"];
                        return false;
                    }

                    // 3. Default blank IssueCondition to N/A.
                    object issueConditionValue = row.Cells["IssueCondition"].Value;

                    if (issueConditionValue == null || issueConditionValue == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(issueConditionValue)))
                    {
                        row.Cells["IssueCondition"].Value = "N/A";
                    }

                    // 4. Default blank IssueRemarks to N/A.
                    object issueRemarksValue = row.Cells["IssueRemarks"].Value;

                    if (issueRemarksValue == null || issueRemarksValue == DBNull.Value || string.IsNullOrWhiteSpace(Convert.ToString(issueRemarksValue)))
                    {
                        row.Cells["IssueRemarks"].Value = "N/A";
                    }
                }
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
            txtComments.Text = "";
            lblAssetRequestID.Text = "";
            lblClientID.Text = "";
            lblEmpID.Text = "";
            lblPersonalInfoID.Text = "";
            picEmpPhoto.Image = null;

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
            cmbAssetCondition.Enabled = true;
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
            cmbAssetCondition.DataSource = objAssetRegisterInfo.GetCurrentAssetStatus();
            cmbAssetCondition.DisplayMember = "CurrentAssetStatusName";
            cmbAssetCondition.ValueMember = "CurrentAssetStatusID";
            cmbAssetCondition.SelectedIndex = 8;
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

                txtComments.Text = "To Issue: \n" + txtRequestDescription.Text;
                dtgAssetRequestedList.DataSource = null;
                dtgAssetRequestedList.DataSource = objAssetRegisterInfo.getAssetInformationOfAnAssetRequest(Convert.ToInt32(lblAssetRequestID.Text));
                formatTransactionGrid();
            }
        }

        /// <summary>
        /// Sets up a DateTimePicker popup for the data-bound IssueDate column.
        /// A calendar button is painted inside each Issue Date cell, so no custom
        /// DataGridView column class or change to the existing DataSource is required.
        /// </summary>
        private void InitializeIssueDatePicker()
        {
            if (issueDatePickerEventsConfigured)
                return;

            issueDatePicker = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd-MMM-yyyy",
                Width = 150,
                Value = DateTime.Today
            };

            issueDatePickerHost = new ToolStripControlHost(issueDatePicker)
            {
                Padding = Padding.Empty,
                Margin = Padding.Empty,
                AutoSize = false,
                Size = issueDatePicker.Size
            };

            issueDatePickerPopup = new ToolStripDropDown
            {
                Padding = Padding.Empty
            };
            issueDatePickerPopup.Items.Add(issueDatePickerHost);

            issueDatePicker.CloseUp += IssueDatePicker_CloseUp;
            dtgAssetRequestedList.CellPainting += dtgAssetRequestedList_IssueDateCellPainting;
            dtgAssetRequestedList.CellClick += dtgAssetRequestedList_IssueDateCellClick;
            issueDatePickerEventsConfigured = true;
        }

        private void dtgAssetRequestedList_IssueDateCellPainting(
            object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 ||
                dtgAssetRequestedList.Columns[e.ColumnIndex].Name != "IssueDate")
                return;

            e.Paint(e.CellBounds, DataGridViewPaintParts.All);

            int buttonWidth = Math.Min(24, e.CellBounds.Width);
            System.Drawing.Rectangle buttonBounds = new System.Drawing.Rectangle(
                e.CellBounds.Right - buttonWidth - 1,
                e.CellBounds.Top + 2,
                Math.Max(1, buttonWidth - 3),
                Math.Max(1, e.CellBounds.Height - 4));

            ButtonRenderer.DrawButton(e.Graphics, buttonBounds,
                System.Windows.Forms.VisualStyles.PushButtonState.Normal);

            // Draw a simple calendar glyph on the button.
            int glyphX = buttonBounds.Left + (buttonBounds.Width - 12) / 2;
            int glyphY = buttonBounds.Top + (buttonBounds.Height - 12) / 2;
            using (Pen pen = new Pen(SystemColors.ControlText, 1.2f))
            {
                e.Graphics.DrawRectangle(pen, glyphX + 1, glyphY + 2, 10, 9);
                e.Graphics.DrawLine(pen, glyphX + 1, glyphY + 5, glyphX + 11, glyphY + 5);
                e.Graphics.DrawLine(pen, glyphX + 4, glyphY, glyphX + 4, glyphY + 4);
                e.Graphics.DrawLine(pen, glyphX + 8, glyphY, glyphX + 8, glyphY + 4);
            }

            e.Handled = true;
        }

        private void dtgAssetRequestedList_IssueDateCellClick(
            object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 ||
                dtgAssetRequestedList.Columns[e.ColumnIndex].Name != "IssueDate")
                return;

            System.Drawing.Rectangle cellBounds = dtgAssetRequestedList.GetCellDisplayRectangle(
                e.ColumnIndex, e.RowIndex, true);
            Point mousePoint = dtgAssetRequestedList.PointToClient(Control.MousePosition);
            int buttonWidth = Math.Min(24, cellBounds.Width);
            System.Drawing.Rectangle buttonBounds = new System.Drawing.Rectangle(
                cellBounds.Right - buttonWidth - 1,
                cellBounds.Top + 2,
                Math.Max(1, buttonWidth - 3),
                Math.Max(1, cellBounds.Height - 4));

            // Open the picker only when the calendar button portion of the cell is clicked.
            if (!buttonBounds.Contains(mousePoint))
                return;

            activeIssueDateCell = dtgAssetRequestedList.Rows[e.RowIndex].Cells[e.ColumnIndex];
            object currentValue = activeIssueDateCell.Value;
            DateTime initialDate = DateTime.Today;

            if (currentValue != null && currentValue != DBNull.Value &&
                DateTime.TryParse(Convert.ToString(currentValue), out DateTime parsedDate))
            {
                // Some Access records contain DateTime.MinValue (01-Jan-0001).
                // DateTimePicker cannot display dates before its MinDate (1753 by default).
                // Treat uninitialized/invalid dates as today, while preserving valid dates.
                if (parsedDate.Date >= issueDatePicker.MinDate.Date &&
                    parsedDate.Date <= issueDatePicker.MaxDate.Date)
                {
                    initialDate = parsedDate.Date;
                }
            }

            // Final safety check before assigning the DateTimePicker.Value.
            if (initialDate < issueDatePicker.MinDate)
                initialDate = DateTime.Today;
            if (initialDate > issueDatePicker.MaxDate)
                initialDate = issueDatePicker.MaxDate.Date;

            issueDatePicker.Value = initialDate;
            issueDatePickerHost.Size = issueDatePicker.Size;

            Point popupLocation = dtgAssetRequestedList.PointToScreen(
                new Point(cellBounds.Left, cellBounds.Bottom));
            issueDatePickerPopup.Show(popupLocation);
        }

        private void IssueDatePicker_CloseUp(object sender, EventArgs e)
        {
            if (activeIssueDateCell != null && !activeIssueDateCell.ReadOnly)
            {
                activeIssueDateCell.Value = issueDatePicker.Value.Date;
                dtgAssetRequestedList.CommitEdit(DataGridViewDataErrorContexts.Commit);
                dtgAssetRequestedList.EndEdit();
                dtgAssetRequestedList.InvalidateCell(activeIssueDateCell);
            }
            else if (activeIssueDateCell != null)
            {
                // The grid column is read-only to prevent accidental text edits; the
                // calendar button is still allowed to update the bound cell value.
                activeIssueDateCell.Value = issueDatePicker.Value.Date;
                dtgAssetRequestedList.CommitEdit(DataGridViewDataErrorContexts.Commit);
                dtgAssetRequestedList.InvalidateCell(activeIssueDateCell);
            }

            activeIssueDateCell = null;
            issueDatePickerPopup.Close();
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
            dtgAssetRequestedList.Columns["OBalance"].Visible = false;
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
            dtgAssetRequestedList.Columns["CBalance"].Visible = false; 
            dtgAssetRequestedList.Columns["CBalance"].HeaderText = "Outstanding Balance";
            dtgAssetRequestedList.Columns["CBalance"].Width = 150;
            dtgAssetRequestedList.Columns["CBalance"].ReadOnly = true;
            dtgAssetRequestedList.Columns["CBalance"].DefaultCellStyle.Format = "c2";
            dtgAssetRequestedList.Columns["CBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dtgAssetRequestedList.Columns["IssueDate"].HeaderText = "Issue Date";
            dtgAssetRequestedList.Columns["IssueDate"].Width = 170;
            dtgAssetRequestedList.Columns["IssueDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgAssetRequestedList.Columns["IssueDate"].ReadOnly = true;

            // New/empty issue dates default to today's date; existing saved dates are preserved.
            foreach (DataGridViewRow issueDateRow in dtgAssetRequestedList.Rows)
            {
                if (issueDateRow.IsNewRow)
                    continue;

                object issueDateValue = issueDateRow.Cells["IssueDate"].Value;
                if (issueDateValue == null || issueDateValue == DBNull.Value ||
                    string.IsNullOrWhiteSpace(Convert.ToString(issueDateValue)))
                {
                    issueDateRow.Cells["IssueDate"].Value = DateTime.Today;
                }
            }
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
