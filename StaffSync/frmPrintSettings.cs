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
    public partial class frmPrintSettings : Form
    {
        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        //DALStaffSync.clsLeaveTRList objLeaveTRList = new DALStaffSync.clsLeaveTRList();
        //DALStaffSync.clsWeeklyOffInfo objWeeklyOffInfo = new DALStaffSync.clsWeeklyOffInfo();
        //DALStaffSync.clsAttendanceMas objAttendanceInfo = new DALStaffSync.clsAttendanceMas();
        //DALStaffSync.clsEmpLeaveEntitlementInfo objEmpLeaveEntitlementInfo = new DALStaffSync.clsEmpLeaveEntitlementInfo();
        //List<WklyOffProfileDetailsInfo> lstWeeklyOffDetailsInfo = new List<WklyOffProfileDetailsInfo>();
        DALStaffSync.clsPrintSettings objPrintSettings = new DALStaffSync.clsPrintSettings();
        DALStaffSync.clsAuditLog objAuditLog = new DALStaffSync.clsAuditLog();
        DALStaffSync.clsAssetRegister objAssetRegister = new DALStaffSync.clsAssetRegister();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        frmDashboard objDashboard = (frmDashboard)System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();
        ClientStatutory tmpClientStatutory = new ClientStatutory();

        public frmPrintSettings()
        {
            InitializeComponent();
        }

        public frmPrintSettings(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmPrintSettings(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;

            disableControls();
            clearControls();

            PrintSettings objSelectedPrintSettings = objPrintSettings.GetPrintSettings(objTempClientFinYearInfo.ClientID);
            txtClientCode.Text = objSelectedPrintSettings.ClientCode.ToString();
            txtClientName.Text = objSelectedPrintSettings.ClientName.ToString();
            lblClientID.Text = objTempClientFinYearInfo.ClientID.ToString();
            lblPrintSettingID.Text = objSelectedPrintSettings.PRNTSettingID.ToString();
            chkPrintReportGeneratedBy.Checked = objSelectedPrintSettings.PrntReportGeneratedBy;
            chkPrintReportGeneratedOn.Checked = objSelectedPrintSettings.PrntReportGeneratedOn;
            chkPrintLogo.Checked = objSelectedPrintSettings.PrntLogoInReport;
            chkPrintReportHeader.Checked = objSelectedPrintSettings.PrntHeaderInReport;
            chkPrintReportFooter.Checked = objSelectedPrintSettings.PrntFooterInReport;
            chkPrintWatermark.Checked = objSelectedPrintSettings.PnrtShowWatermark;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPrintSettings_Load(object sender, EventArgs e)
        {
            FocusManager.EnableHighlighting = false;
            FocusManager.ShowNavigationError = true;
            FocusManager.Register(this);
            //FocusManager.SetFocus(btnSaveDetails);

            // TODO: This line of code loads data into the 'staffsyncDBDataSet1.qryAllEmpLeavePendingStatement' table. You can move, or remove it, as needed.
            //this.qryAllEmpLeavePendingStatementTableAdapter.Fill(this.staffsyncDBDataSet1.qryAllEmpLeavePendingStatement);
            //// TODO: This line of code loads data into the 'staffsyncDBDTSet.EmpMasInfo' table. You can move, or remove it, as needed.
            //this.empMasInfoTableAdapter.Fill(this.staffsyncDBDTSet.EmpMasInfo);
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

        public void clearControls()
        {

            lblClientID.Text = objTempClientFinYearInfo.ClientID.ToString();
            lblPrintSettingID.Text = "0";
        }

        public void enableControls()
        {
            //dtgAdvanceRequestersList.Enabled = true;
        }

        public void disableControls()
        {
            txtClientCode.Enabled = false;
            txtClientName.Enabled = false;
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

        private void frmPrintSettings_KeyDown(object sender, KeyEventArgs e)
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

        }

        private void picRefresh_Click(object sender, EventArgs e)
        {

        }

        private void frmPrintSettings_Activated(object sender, EventArgs e)
        {

        }

        private void dtgAdvanceRequestersList_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtgAssetsRequestersList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chkEnabled_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            string strValidationMessage = objLogin.ValidateUserRolesAndResponsibilitiesInfo(objTempCurrentlyLoggedInUserInfo.EmpID, "");
            if (strValidationMessage != "Success")
            {
                MessageBox.Show(strValidationMessage, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Convert.ToInt16(lblPrintSettingID.Text.ToString()) == 0)
            {
                objPrintSettings.InsertPrintSettings(Convert.ToInt16(lblClientID.Text.ToString()), chkPrintReportGeneratedBy.Checked, chkPrintReportGeneratedOn.Checked, chkPrintLogo.Checked, chkPrintReportHeader.Checked, chkPrintReportFooter.Checked, chkPrintWatermark.Checked);
                MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                objPrintSettings.UpdatePrintSettings(Convert.ToInt16(lblPrintSettingID.Text.ToString()), Convert.ToInt16(lblClientID.Text.ToString()), chkPrintReportGeneratedBy.Checked, chkPrintReportGeneratedOn.Checked, chkPrintLogo.Checked, chkPrintReportHeader.Checked, chkPrintReportFooter.Checked, chkPrintWatermark.Checked);
                MessageBox.Show("Details updated successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //clearControls();
            //disableControls();
        }
    }
}
