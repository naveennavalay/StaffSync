using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public partial class frmEmployeeResignation : Form
    {
        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        frmDashboard objDashboard = (frmDashboard)System.Windows.Forms.Application.OpenForms["frmDashboard"];
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        DALStaffSync.clsResignationInfo objResignationInfo = new DALStaffSync.clsResignationInfo();
        DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster();
        DALStaffSync.clsEmployeePersonalInfo objEmployeePersonalInfo = new DALStaffSync.clsEmployeePersonalInfo();

        clsImpageOperation objImpageOperation = new clsImpageOperation();
        DALStaffSync.clsPhotoMas objPhotoMas = new DALStaffSync.clsPhotoMas();

        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();
        SpecificEmployeeSalaryInfo objSpecificEmployeeSalaryInfo = new SpecificEmployeeSalaryInfo();
        AdvanceTypeConfigModel objAdvanceTypeConfigModel = new AdvanceTypeConfigModel();

        string strActionStatement = "";
        private Dictionary<string, object> _originalValues;

        DateTime dos;
        string dateFormat = "dd-MM-yyyy";
        CultureInfo provider = CultureInfo.InvariantCulture;

        public frmEmployeeResignation()
        {
            InitializeComponent();
        }

        public frmEmployeeResignation(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmEmployeeResignation(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
            lblClientID.Text = objTempClientFinYearInfo.ClientID.ToString();
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
            objDashboard.sptrDashboardContainer.Visible = true;
            objDashboard.grpDashboardDateRange.Visible = true;
            objDashboard.refreshDashboardCharts();
            this.Close();
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
            errValidator.Clear();
        }

        public void clearControls()
        {
            lblEmpID.Text = "";
            txtEmpCode.Text = "";
            txtEmpName.Text = "";
            txtDesignation.Text = "";
            txtDepartment.Text = "";
            picEmpPhoto.Image = null;
            lblPersonalInfoID.Text = "";

            lblReportingManagerID.Text = "";
            txtRepCode.Text = "";
            txtRepName.Text = "";
            txtRepDesignation.Text = "";
            txtRepDepartment.Text = "";
            picRepEmpPhoto.Image = null;

            lblEmpResignID.Text = "";
            dtResignationDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            cmbResignationReason.DataSource = null;
            lblEmpResignID.Text = "";
            lblTaskID.Text = "";

            txtResignationMoreDetails.Text = "";

            lblRequestFromMailID.Text = "";
            lblRequestToMailID.Text = "";
            lblResignationReasonID.Text = "";

            lnkViewAuditLog.Visible = false;
        }

        public void enableControls()
        {
            btnEmpSearch.Enabled = true;
            txtResignationCode.Enabled = false;
            txtEmpCode.Enabled = false;
            txtEmpName.Enabled = false;
            txtDesignation.Enabled = false;
            txtDepartment.Enabled = false;

            txtRepCode.Enabled = false;
            txtRepName.Enabled = false;
            txtRepDesignation.Enabled = false;
            txtRepDepartment.Enabled = false;
            btnRepSearch.Enabled = true;

            cmbResignationReason.Enabled = true;
            dtResignationDate.Enabled = true;
            txtResignationMoreDetails.Enabled = true;
        }

        public void disableControls()
        {
            btnEmpSearch.Enabled = false;
            txtResignationCode.Enabled = false;
            txtEmpCode.Enabled = false;
            txtEmpName.Enabled = false;
            txtDesignation.Enabled = false;
            txtDepartment.Enabled = false;

            txtRepCode.Enabled = false;
            txtRepName.Enabled = false;
            txtRepDesignation.Enabled = false;
            txtRepDepartment.Enabled = false;
            btnRepSearch.Enabled = false;

            cmbResignationReason.Enabled = false;
            dtResignationDate.Enabled = false;
        }

        public void onGenerateButtonClick()
        {
            lblActionMode.Text = "add";
            lblEmpID.Text = "";
            lblReportingManagerID.Text = "";
            lblPersonalInfoID.Text = "";
            btnEmpSearch.Enabled = true;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = false;
            btnCancel.Enabled = true;
        }

        public void onModifyButtonClick()
        {
            lblActionMode.Text = "modify";
            lblEmpID.Text = "";
            lblReportingManagerID.Text = "";
            lblPersonalInfoID.Text = "";
            btnEmpSearch.Enabled = true;
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = false;
            btnCancel.Enabled = true;
        }

        public void onRemoveButtonClick()
        {
            lblActionMode.Text = "remove";
            lblEmpID.Text = "";
            lblReportingManagerID.Text = "";
            lblPersonalInfoID.Text = "";
            btnEmpSearch.Enabled = true;
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onSaveButtonClick()
        {
            lblActionMode.Text = "";
            lblReportingManagerID.Text = "";
            lblPersonalInfoID.Text = "";
            btnEmpSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onCancelButtonClick()
        {
            lblActionMode.Text = "";
            lblEmpID.Text = "";
            lblReportingManagerID.Text = "";
            lblPersonalInfoID.Text = "";
            btnEmpSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void frmEmployeeResignation_Load(object sender, EventArgs e)
        {
            FocusManager.EnableHighlighting = false;
            FocusManager.ShowNavigationError = true;
            FocusManager.Register(this);
            FocusManager.SetFocus(btnGenerateDetails);

            clearControls();
            disableControls();
            onCancelButtonClick();
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

            lblEmpResignID.Text = objGenFunc.getMaxRowCount("EmpResignationInfo", "EmpResignID").Data.ToString();
            txtResignationCode.Text = "RSGN-" + (lblEmpResignID.Text.Trim()).ToString().PadLeft(4, '0');

            dtResignationDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            cmbResignationReason.DataSource = objResignationInfo.getResignationInfotatements();
            cmbResignationReason.DisplayMember = "ResigMasTitle";
            cmbResignationReason.ValueMember = "ResigMasID";
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            if(objResignationInfo.getIsResignationAppliedInfotatements(Convert.ToInt32(lblEmpID.Text)).EmpResignStatus > 0)
            {
                MessageBox.Show("Resignation has already been applied.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string strValidationMessage = objLogin.ValidateUserRolesAndResponsibilitiesInfo(objTempCurrentlyLoggedInUserInfo.EmpID, "");
            if (strValidationMessage != "Success")
            {
                MessageBox.Show(strValidationMessage, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            if (!ValidateValuesOnUI())
            {
                this.Cursor = Cursors.Default;
                return;
            }

            int iTaskID = 0;

            var updatedValues = AuditLogger.getOriginalValues(this);
            var onlyChangedValues = (dynamic)null;

            strActionStatement = "";

            lblResignationReasonID.Text = (cmbResignationReason.SelectedIndex + 1).ToString();

            if (lblActionMode.Text == "add")
            {
                objResignationInfo.InsertEmployeeResignationInfo(Convert.ToInt32(lblPersonalInfoID.Text.ToString()), Convert.ToInt32(lblResignationReasonID.Text), txtResignationMoreDetails.Text.Trim(), Convert.ToDateTime(dtResignationDate.Text.ToString()),
                Convert.ToDateTime(dtResignationDate.Text.ToString()), Convert.ToInt32(lblReportingManagerID.Text.ToString()), false,
                Convert.ToDateTime(dtResignationDate.Text.ToString()), Convert.ToInt32(lblReportingManagerID.Text.ToString()), false,
                1, Convert.ToDateTime(dtResignationDate.Text.ToString()), 0, Convert.ToDateTime(dtResignationDate.Text.ToString()), "Need to serve");

                SendResignationSubmissionEmail();
                SendResignationAcknowledgementEmail();
            }

            objTempCurrentlyLoggedInUserInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objTempCurrentlyLoggedInUserInfo.EmpID.ToString()));

            onSaveButtonClick();
            disableControls();
            clearControls();
            errValidator.Clear();
            this.Cursor = Cursors.Default;
        }

        private string BuildResignationSubmissionEmailTemplate()
        {
            StringBuilder html = new StringBuilder();

            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine("<meta charset=\"utf-8\" />");
            html.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />");
            html.AppendLine("<title>Resignation Submission</title>");
            html.AppendLine("<style>");
            html.AppendLine("body{font-family:Segoe UI,Arial,sans-serif;background:#edf1f7;margin:0;padding:24px;color:#202124;}");
            html.AppendLine(".mail-container{max-width:700px;margin:0 auto;background:#ffffff;border:1px solid #d9dee7;border-radius:10px;overflow:hidden;}");
            html.AppendLine(".mail-header{background:#245b85;color:#ffffff;padding:20px 26px;font-size:21px;font-weight:600;}");
            html.AppendLine(".mail-body{padding:26px;line-height:1.6;font-size:14px;}");
            html.AppendLine(".details{width:100%;border-collapse:collapse;margin:20px 0;font-size:13px;}");
            html.AppendLine(".details td{border:1px solid #e1e5eb;padding:10px 12px;vertical-align:top;}");
            html.AppendLine(".details td:first-child{width:34%;background:#f6f8fb;font-weight:600;color:#344054;}");
            html.AppendLine(".employee-name{font-weight:600;color:#245b85;white-space:nowrap;}");
            html.AppendLine(".mail-footer{background:#f8fafc;border-top:1px solid #e3e7ed;padding:14px 26px;color:#667085;font-size:11px;text-align:center;}");
            html.AppendLine("</style>");
            html.AppendLine("</head>");
            html.AppendLine("<body>");
            html.AppendLine("<div class=\"mail-container\">");
            html.AppendLine("<div class=\"mail-header\">Resignation Submission</div>");
            html.AppendLine("<div class=\"mail-body\">");
            html.AppendLine("<p>Dear {{ManagerFirstName}},</p>");
            html.AppendLine("<p>I hope you are doing well.</p>");
            html.AppendLine("<p>I would like to formally submit my resignation from my position as {{Designation}} in the {{Department}} Department, with the details provided below.</p>");
            html.AppendLine("<table class=\"details\">");
            html.AppendLine("<tr><td>Employee Name</td><td class=\"employee-name\">{{EmployeeName}} [{{EmployeeCode}}]</td></tr>");
            html.AppendLine("<tr><td>Designation / Department</td><td>{{Designation}} / {{Department}}</td></tr>");
            html.AppendLine("<tr><td>Resignation Date</td><td>{{ResignationDate}}</td></tr>");
            html.AppendLine("<tr><td>Reason for Resignation</td><td>{{ResignationReason}}</td></tr>");
            html.AppendLine("<tr><td>More Details</td><td>{{MoreDetails}}</td></tr>");
            html.AppendLine("</table>");
            html.AppendLine("<p>This decision has been made after careful consideration. I am grateful for the guidance and opportunities provided during my time with the organisation.</p>");
            html.AppendLine("<p>I will extend my full cooperation to complete the necessary handover activities and ensure a smooth transition. I would appreciate your acknowledgement of this submission and guidance on the next steps and applicable formalities.</p>");
            html.AppendLine("<p>Thank you for your understanding and support.</p>");
            html.AppendLine("<p>Warm regards,<br />{{EmployeeName}}<br />{{Designation}}<br />{{Department}} Department</p>");
            html.AppendLine("</div>");
            html.AppendLine("<div class=\"mail-footer\">&copy; {{Year}} StaffSync | Payroll Management</div>");
            html.AppendLine("</div>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            return html.ToString();
        }

        private void SendResignationSubmissionEmail()
        {
            string recipientEmail = lblRequestToMailID.Text.Trim();

            if (string.IsNullOrWhiteSpace(recipientEmail))
            {
                MessageBox.Show(
                    "Resignation details were saved, but the reporting manager's email address is not available. The notification was not sent.",
                    "StaffSync - Email Notification",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string managerName = txtRepName.Text.Trim();
                string managerFirstName = managerName;

                if (!string.IsNullOrWhiteSpace(managerName))
                {
                    string[] nameParts = managerName.Split(
                        new[] { ' ' },
                        StringSplitOptions.RemoveEmptyEntries);

                    if (nameParts.Length > 0)
                        managerFirstName = nameParts[0];
                }

                // Encode dynamic values because they are inserted into an HTML template.
                Dictionary<string, string> templateData = new Dictionary<string, string>
                {
                    { "ManagerFirstName", System.Net.WebUtility.HtmlEncode(managerFirstName) },
                    { "EmployeeName", System.Net.WebUtility.HtmlEncode(txtEmpName.Text.Trim()) },
                    { "EmployeeCode", System.Net.WebUtility.HtmlEncode(txtEmpCode.Text.Trim()) },
                    { "Designation", System.Net.WebUtility.HtmlEncode(txtDesignation.Text.Trim()) },
                    { "Department", System.Net.WebUtility.HtmlEncode(txtDepartment.Text.Trim()) },
                    { "ResignationDate", System.Net.WebUtility.HtmlEncode(dos.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture)) },
                    { "ResignationReason", System.Net.WebUtility.HtmlEncode(cmbResignationReason.Text.Trim()) },
                    { "MoreDetails", System.Net.WebUtility.HtmlEncode(txtResignationMoreDetails.Text.Trim()).Replace("\r\n", "<br />").Replace("\n", "<br />") },
                    { "Year", DateTime.Now.Year.ToString(CultureInfo.InvariantCulture) }
                };

                string mailSubject = "Resignation Submission - " + txtEmpName.Text.Trim();
                string mailTemplate = BuildResignationSubmissionEmailTemplate();

                myMailService objMailService = new myMailService();
                objMailService.SendMail(
                    recipientEmail,
                    mailSubject,
                    mailTemplate,
                    templateData);

            }
            catch (Exception ex)
            {
                // Email failure must not undo or interrupt the resignation record save.
                MessageBox.Show(
                    "Resignation details were saved successfully, but the email could not be sent."
                    + Environment.NewLine + Environment.NewLine
                    + ex.Message,
                    "StaffSync - Email Notification",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private string BuildResignationAcknowledgementEmailTemplate()
        {
            StringBuilder html = new StringBuilder();

            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html>");
            html.AppendLine("<head>");
            html.AppendLine("<meta charset=\"utf-8\" />");
            html.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />");
            html.AppendLine("<title>Resignation Acknowledgement</title>");
            html.AppendLine("<style>");
            html.AppendLine("body{font-family:Segoe UI,Arial,sans-serif;background:#edf1f7;margin:0;padding:24px;color:#202124;}");
            html.AppendLine(".mail-container{max-width:700px;margin:0 auto;background:#ffffff;border:1px solid #d9dee7;border-radius:10px;overflow:hidden;}");
            html.AppendLine(".mail-header{background:#245b85;color:#ffffff;padding:20px 26px;font-size:21px;font-weight:600;}");
            html.AppendLine(".mail-body{padding:26px;line-height:1.6;font-size:14px;}");
            html.AppendLine(".details{width:100%;border-collapse:collapse;margin:20px 0;font-size:13px;}");
            html.AppendLine(".details td{border:1px solid #e1e5eb;padding:10px 12px;vertical-align:top;}");
            html.AppendLine(".details td:first-child{width:34%;background:#f6f8fb;font-weight:600;color:#344054;}");
            html.AppendLine(".employee-name{font-weight:600;color:#245b85;white-space:nowrap;}");
            html.AppendLine(".status{display:inline-block;background:#eaf4ff;color:#245b85;border:1px solid #c9def2;border-radius:4px;padding:5px 10px;font-weight:600;}");
            html.AppendLine(".mail-footer{background:#f8fafc;border-top:1px solid #e3e7ed;padding:14px 26px;color:#667085;font-size:11px;text-align:center;}");
            html.AppendLine("</style>");
            html.AppendLine("</head>");
            html.AppendLine("<body>");
            html.AppendLine("<div class=\"mail-container\">");
            html.AppendLine("<div class=\"mail-header\">Resignation Acknowledgement</div>");
            html.AppendLine("<div class=\"mail-body\">");
            html.AppendLine("<p>Dear {{EmployeeFirstName}},</p>");
            html.AppendLine("<p>Thank you for your email and for formally communicating your decision to resign from your position as {{Designation}} in the {{Department}} Department.</p>");
            html.AppendLine("<p>This is to acknowledge that we have received your resignation dated <strong>{{ResignationDate}}</strong>, citing <em>{{ResignationReason}}</em> as the reason.</p>");
            html.AppendLine("<table class=\"details\">");
            html.AppendLine("<tr><td>Employee Name</td><td class=\"employee-name\">{{EmployeeName}} [{{EmployeeCode}}]</td></tr>");
            html.AppendLine("<tr><td>Designation / Department</td><td>{{Designation}} / {{Department}}</td></tr>");
            html.AppendLine("<tr><td>Resignation Date</td><td>{{ResignationDate}}</td></tr>");
            html.AppendLine("<tr><td>Reason for Resignation</td><td>{{ResignationReason}}</td></tr>");
            html.AppendLine("<tr><td>Acknowledgement Status</td><td><span class=\"status\">Received</span></td></tr>");
            html.AppendLine("</table>");
            html.AppendLine("<p>We appreciate your contributions, commitment and efforts during your association with the organisation. We respect your decision and understand that this is an important step in your professional journey.</p>");
            html.AppendLine("<p>Your resignation request has been received and will be taken forward through the organisation's resignation management process. The concerned teams will review the necessary formalities, including the applicable notice period, handover arrangements and clearance requirements.</p>");
            html.AppendLine("<p>We will keep you informed about the progress and any further steps required from your side. In the meantime, please continue with your regular responsibilities and coordinate with your reporting team to ensure a smooth transition.</p>");
            html.AppendLine("<p>Thank you once again for your contributions. We wish you continued success and all the very best in your future endeavours.</p>");
            html.AppendLine("<p>Warm regards,<br />{{ManagerName}}<br />Reporting Manager</p>");
            html.AppendLine("</div>");
            html.AppendLine("<div class=\"mail-footer\">&copy; {{Year}} StaffSync | Payroll Management</div>");
            html.AppendLine("</div>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            return html.ToString();
        }

        private void SendResignationAcknowledgementEmail()
        {
            string recipientEmail = lblRequestFromMailID.Text.Trim();

            if (string.IsNullOrWhiteSpace(recipientEmail))
            {
                MessageBox.Show(
                    "Resignation details were saved, but the employee's email address is not available. The acknowledgement was not sent.",
                    "StaffSync - Email Notification",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string employeeName = txtEmpName.Text.Trim();
                string employeeFirstName = employeeName;
                if (!string.IsNullOrWhiteSpace(employeeName))
                {
                    string[] nameParts = employeeName.Split(
                        new[] { ' ' },
                        StringSplitOptions.RemoveEmptyEntries);

                    if (nameParts.Length > 0)
                        employeeFirstName = nameParts[0];
                }

                Dictionary<string, string> templateData = new Dictionary<string, string>
                {
                    { "EmployeeFirstName", System.Net.WebUtility.HtmlEncode(employeeFirstName) },
                    { "EmployeeName", System.Net.WebUtility.HtmlEncode(employeeName) },
                    { "EmployeeCode", System.Net.WebUtility.HtmlEncode(txtEmpCode.Text.Trim()) },
                    { "Designation", System.Net.WebUtility.HtmlEncode(txtDesignation.Text.Trim()) },
                    { "Department", System.Net.WebUtility.HtmlEncode(txtDepartment.Text.Trim()) },
                    { "ResignationDate", System.Net.WebUtility.HtmlEncode(dos.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture)) },
                    { "ResignationReason", System.Net.WebUtility.HtmlEncode(cmbResignationReason.Text.Trim()) },
                    { "ManagerName", System.Net.WebUtility.HtmlEncode(txtRepName.Text.Trim()) },
                    { "Year", DateTime.Now.Year.ToString(CultureInfo.InvariantCulture) }
                };

                // Prefixing the original subject with Re: makes this a reply-style
                // notification. The current shared SendMail method does not expose
                // the original message's Message-ID/In-Reply-To headers.
                string mailSubject = "Re: Resignation Submission - " + employeeName;
                string mailTemplate = BuildResignationAcknowledgementEmailTemplate();

                myMailService objMailService = new myMailService();
                objMailService.SendMail(
                    recipientEmail,
                    mailSubject,
                    mailTemplate,
                    templateData);
            }
            catch (Exception ex)
            {
                // Email failure must not undo or interrupt the resignation record save.
                MessageBox.Show(
                    "Resignation details were saved successfully, but the acknowledgement email could not be sent."
                    + Environment.NewLine + Environment.NewLine
                    + ex.Message,
                    "StaffSync - Email Notification",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private bool ValidateValuesOnUI()
        {
            bool validationStatus = true;
            errValidator.Clear();

            // Helper for date validation
            if (lblEmpID.Text.Trim() == "")
            {
                errValidator.SetError(txtEmpCode, "Please select the employee");
                validationStatus = false;
            }
            else
            {
                errValidator.SetError(txtEmpCode, "");
            }

            if (cmbResignationReason.Text.Trim() == "")
            {
                errValidator.SetError(cmbResignationReason, "Please select the Resignation Reason");
                validationStatus = false;
            }
            else
            {
                errValidator.SetError(cmbResignationReason, "");
            }

            // Date of Birth
            if (string.IsNullOrEmpty(dtResignationDate.Text))
            {
                validationStatus = false;
                errValidator.SetError(this.dtResignationDate, dtResignationDate.Tag?.ToString() ?? "Date of Resignation is required.");
            }
            else if (!DateTime.TryParseExact(dtResignationDate.Text, dateFormat, provider, DateTimeStyles.None, out dos))
            {
                validationStatus = false;
                errValidator.SetError(this.dtResignationDate, "Invalid Date of Resignation format (dd-MM-yyyy).");
            }
            else if (dos < DateTime.Now.Date)
            {
                validationStatus = false;
                errValidator.SetError(this.dtResignationDate, "Date of Resignation cannot be in the past.");
            }
            else if (dos > DateTime.Now.Date)
            {
                validationStatus = false;
                errValidator.SetError(this.dtResignationDate, "Date of Resignation cannot be in the future.");
            }
            else
            {
                errValidator.SetError(this.dtResignationDate, "");
            }

            if (txtResignationMoreDetails.Text.Trim() == "")
            {
                txtResignationMoreDetails.Text = "N/A";
            }

            return validationStatus;
        }

        private void frmEmployeeResignation_KeyDown(object sender, KeyEventArgs e)
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
                //btnModifyDetails_Click(sender, e);
            }
            else if (e.Control && e.KeyCode == Keys.D)
            {
                e.SuppressKeyPress = true;
                //btnRemoveDetails_Click(sender, e);
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

        private void btnEmpSearch_Click(object sender, EventArgs e)
        {
            frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "EmployeeListToResign");
            frmEmployeeList.ShowDialog();
        }

        public void SelectedEmployeeID(string SearchOptionSelectedForm, int selectedEmployeeID)
        {
            if (SearchOptionSelectedForm == "EmployeeListToResign")
            {
                if (objResignationInfo.getIsResignationAppliedInfotatements(Convert.ToInt32(selectedEmployeeID)).EmpResignStatus > 0)
                {
                    MessageBox.Show("Resignation has already been applied.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                lblEmpID.Text = selectedEmployeeID.ToString();
                EmpPersonalPersonalInfo objSelectedPersonalInfo = objEmployeePersonalInfo.GetEmpPersonalPersonalInfo(Convert.ToInt16(lblEmpID.Text));
                lblPersonalInfoID.Text = objSelectedPersonalInfo.PersonalInfoID.ToString();
                lblRequestFromMailID.Text = objSelectedPersonalInfo.ContactNumber2.ToString();

                ReportingManagerInfo objEmpMaster = objEmployeeMaster.GetReportingManagerInfo(Convert.ToInt16(selectedEmployeeID.ToString()));
                txtEmpCode.Text = objEmpMaster.EmpCode;
                txtEmpName.Text = objEmpMaster.EmpName;
                txtDesignation.Text = objEmpMaster.DesignationTitle;
                txtDepartment.Text = objEmpMaster.DepartmentTitle;
                picEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(selectedEmployeeID.ToString())).EmpPhoto);

                EmployeeInfo objSelectedEmployeeInfo = objEmployeeMaster.GetSelectedEmployeeInfo(Convert.ToInt16(selectedEmployeeID.ToString()));
                lblReportingManagerID.Text = objSelectedEmployeeInfo.EmpRepManID.ToString();
                ReportingManagerInfo objReportingManagerInfo = objEmployeeMaster.GetReportingManagerInfo(Convert.ToInt16(lblReportingManagerID.Text));
                txtRepCode.Text = objReportingManagerInfo.EmpCode;
                txtRepName.Text = objReportingManagerInfo.EmpName;
                txtRepDesignation.Text = objReportingManagerInfo.DesignationTitle;
                txtRepDepartment.Text = objReportingManagerInfo.DepartmentTitle;
                picRepEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(lblReportingManagerID.Text)).EmpPhoto);
                EmpPersonalPersonalInfo objSelectedReportingManagerPersonalInfo = objEmployeePersonalInfo.GetEmpPersonalPersonalInfo(Convert.ToInt16(lblReportingManagerID.Text));
                lblRequestToMailID.Text = objSelectedReportingManagerPersonalInfo.ContactNumber2.ToString();
            }
        }

        public void UpdateUIWithSelectedEmployeeDetails(int EmployeeID)
        {
            this.Cursor = Cursors.WaitCursor;

            lblEmpID.Text = EmployeeID.ToString();
            EmployeeInfo objSelectedEmployeeInfo = objEmployeeMaster.GetSelectedEmployeeInfo(EmployeeID);
            txtEmpCode.Text = objSelectedEmployeeInfo.EmpCode;
            txtEmpName.Text = objSelectedEmployeeInfo.EmpName;
            txtDepartment.Text = objSelectedEmployeeInfo.DepartmentTitle;
            txtDesignation.Text = objSelectedEmployeeInfo.DesignationTitle;

            EmpPersonalPersonalInfo objSelectedPersonalInfo = objEmployeePersonalInfo.GetEmpPersonalPersonalInfo(Convert.ToInt16(lblEmpID.Text));
            lblPersonalInfoID.Text = objSelectedPersonalInfo.PersonalInfoID.ToString();
            picEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(EmployeeID).EmpPhoto);

            lblReportingManagerID.Text = objSelectedEmployeeInfo.EmpRepManID.ToString();
            ReportingManagerInfo objReportingManagerInfo = objEmployeeMaster.GetReportingManagerInfo(Convert.ToInt16(lblReportingManagerID.Text));
            txtRepCode.Text = objReportingManagerInfo.EmpCode;
            txtRepName.Text = objReportingManagerInfo.EmpName;
            txtRepDesignation.Text = objReportingManagerInfo.DesignationTitle;
            txtRepDepartment.Text = objReportingManagerInfo.DepartmentTitle;
            picRepEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(lblReportingManagerID.Text)).EmpPhoto);

            this.Cursor = Cursors.Default;
        }
    }
}
