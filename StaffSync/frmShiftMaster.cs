using ModelStaffSync;
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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StaffSync
{
    public partial class frmShiftMaster : Form
    {
        //clsCountries objCountries = new clsCountries();
        //clsDesignation objDesignation = new clsDesignation();
        //clsStates objState = new clsStates();
        //clsRelationship objRelationship = new clsRelationship();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        //DALStaffSync.clsSkillsMas objSkills = new DALStaffSync.clsSkillsMas();
        DALStaffSync.clsShiftMas objShiftMas = new DALStaffSync.clsShiftMas();
        frmDashboard objDashboard = (frmDashboard) System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();
        DALStaffSync.clsAuditLog objAuditLog = new DALStaffSync.clsAuditLog();

        string strActionStatement = "";
        private Dictionary<string, object> _originalValues;

        public frmShiftMaster()
        {
            InitializeComponent();
        }

        public frmShiftMaster(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmShiftMaster(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
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
            objDashboard.refreshDashboardCharts();
            this.Close();
        }

        private void frmShiftMaster_Load(object sender, EventArgs e)
        {
            FocusManager.EnableHighlighting = false;
            FocusManager.ShowNavigationError = true;
            FocusManager.Register(this);
            FocusManager.SetFocus(btnGenerateDetails);

            //// TODO: This line of code loads data into the 'staffsyncDBDTSet.EmpMasInfo' table. You can move, or remove it, as needed.
            //this.empMasInfoTableAdapter.Fill(this.staffsyncDBDTSet.EmpMasInfo);
            onCancelButtonClick();
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
            frmShiftList frmShiftsList = new frmShiftList(this);
            frmShiftsList.ShowDialog(this);
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
            cmbIsActive.SelectedIndex = 1;
            lblShiftID.Text = objGenFunc.getMaxRowCount("ShiftMas", "ShiftID").Data.ToString();
            txtShiftCode.Text = "SHT-" + (lblShiftID.Text.Trim()).ToString().PadLeft(4, '0');
            errValidator.Clear();
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            string strValidationMessage = objLogin.ValidateUserRolesAndResponsibilitiesInfo(objTempCurrentlyLoggedInUserInfo.EmpID, "");
            if (strValidationMessage != "Success")
            {
                MessageBox.Show(strValidationMessage, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtShiftTitle.Text))
            {
                txtShiftTitle.Focus();
                errValidator.SetError(this.txtShiftTitle, "Please enter Shift Title");
            }
            else if (string.IsNullOrEmpty(txtShiftInitial.Text))
            {
                txtShiftInitial.Focus();
                errValidator.SetError(this.txtShiftInitial, "Please enter Shift Initial");
            }
            else if (string.IsNullOrEmpty(txtFromTime.Text))
            {
                txtFromTime.Focus();
                errValidator.SetError(this.txtFromTime, "Please enter Shift Start Time");
            }
            else if (string.IsNullOrEmpty(txtTimeTo.Text))
            {
                txtTimeTo.Focus();
                errValidator.SetError(this.txtTimeTo, "Please enter Shift End Time");
            }

            else if (string.IsNullOrEmpty(txtGraceTime.Text))
            {
                txtGraceTime.Focus();
                errValidator.SetError(this.txtGraceTime, "Please enter Grace Period");
            }
            else if (!DateTime.TryParseExact(txtFromTime.Text.Trim(), "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime shiftStartDateTime))
            {
                txtFromTime.Focus();
                errValidator.SetError(this.txtFromTime, "Enter a valid Shift Start Time in 24-hour HH:mm format (00:00 to 23:59).");
            }
            else if (!DateTime.TryParseExact(txtTimeTo.Text.Trim(), "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime shiftEndDateTime))
            {
                txtTimeTo.Focus();
                errValidator.SetError(this.txtTimeTo, "Enter a valid Shift End Time in 24-hour HH:mm format (00:00 to 23:59).");
            }
            //else if (shiftStartDateTime.TimeOfDay >= shiftEndDateTime.TimeOfDay)
            //{
            //    txtTimeTo.Focus();
            //    errValidator.SetError(this.txtTimeTo, "Shift End Time must be later than Shift Start Time.");
            //}
            //else if ((shiftEndDateTime.TimeOfDay - shiftStartDateTime.TimeOfDay) < TimeSpan.FromHours(8))
            //{
            //    txtTimeTo.Focus();
            //    errValidator.SetError(this.txtTimeTo, "The difference between Shift Start Time and Shift End Time must be at least 8 hours.");
            //}
            else if (!IsValidGracePeriod(txtGraceTime.Text.Trim()))
            {
                txtGraceTime.Focus();
                errValidator.SetError(this.txtGraceTime, "Grace Period must be between 00:00 and 60:00 minutes (mm:ss).");
            }
            else if (string.IsNullOrEmpty(cmbIsActive.Text))
            {
                cmbIsActive.Focus();
                cmbIsActive.SelectedIndex = 1;
                errValidator.SetError(this.cmbIsActive, "Please select Shift Status");
            }
            else
            {
                var updatedValues = AuditLogger.getOriginalValues(this);
                var onlyChangedValues = (dynamic)null;

                strActionStatement = "";

                if (lblActionMode.Text == "add")
                {
                    strActionStatement = "ShiftsMasterInfoNewUpdates";
                    onlyChangedValues = AuditLogger.getUpdatedValues(_originalValues, updatedValues, true);

                    int newID = objShiftMas.InsertShiftMasInfo(txtShiftCode.Text.Trim(), txtShiftTitle.Text.Trim(), txtShiftInitial.Text.Trim(), Convert.ToDateTime(txtFromTime.Text), Convert.ToDateTime(txtTimeTo.Text), Convert.ToDateTime(txtGraceTime.Text), cmbIsActive.Text.Trim() == "Yes" ? true : false, false);
                    if (newID > 0)
                        MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (lblActionMode.Text == "modify")
                {
                    strActionStatement = "ShiftsMasterInfoExistingUpdates";
                    onlyChangedValues = AuditLogger.getUpdatedValues(_originalValues, updatedValues, false);

                    int affectedRows = objShiftMas.UpdateShiftMasInfo(Convert.ToInt16(lblShiftID.Text.Trim()), txtShiftCode.Text.Trim(), txtShiftTitle.Text.Trim(), txtShiftInitial.Text.Trim(), Convert.ToDateTime(txtFromTime.Text), Convert.ToDateTime(txtTimeTo.Text), Convert.ToDateTime("00:" + txtGraceTime.Text), cmbIsActive.Text.Trim() == "Yes" ? true : false, false);
                    if (affectedRows > 0)
                        MessageBox.Show("Details updated successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                foreach (var changedValues in onlyChangedValues)
                {
                    if (lblActionMode.Text == "add")
                        objAuditLog.InsertAuditLog(Convert.ToInt32(CurrentUser.EmpID.ToString()), Convert.ToInt32(lblShiftID.Text.ToString()), changedValues.ToString().Trim(), "Insert", ModelStaffSync.CurrentUser.EmpName, strActionStatement, Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                    else if (lblActionMode.Text == "modify")
                        objAuditLog.InsertAuditLog(Convert.ToInt32(CurrentUser.EmpID.ToString()), Convert.ToInt32(lblShiftID.Text.ToString()), changedValues.ToString().Trim(), "Update", ModelStaffSync.CurrentUser.EmpName, strActionStatement, Convert.ToInt32(objTempClientFinYearInfo.ClientID));
                }

                objTempCurrentlyLoggedInUserInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objTempCurrentlyLoggedInUserInfo.EmpID.ToString()));

                onSaveButtonClick();
                disableControls();
                clearControls();
                errValidator.Clear();
            }
        }

        /// <summary>
        /// Validates Grace Period entered in the existing mm:ss format.
        /// Accepts values from 00:00 through 60:00 (inclusive).
        /// </summary>
        private bool IsValidGracePeriod(string gracePeriodText)
        {
            if (string.IsNullOrWhiteSpace(gracePeriodText))
                return false;

            string[] parts = gracePeriodText.Split(':');
            int minutes;
            int seconds;

            if (parts.Length != 2 ||
                !int.TryParse(parts[0], NumberStyles.None, CultureInfo.InvariantCulture, out minutes) ||
                !int.TryParse(parts[1], NumberStyles.None, CultureInfo.InvariantCulture, out seconds))
            {
                return false;
            }

            if (minutes < 0 || seconds < 0 || seconds > 59)
                return false;

            int totalSeconds = (minutes * 60) + seconds;
            return totalSeconds <= 60 * 60;
        }

        public void clearControls()
        {
            lnkViewAuditLog.Visible = false;
            txtShiftCode.Text = "";
            txtShiftCode.ReadOnly = true;
            txtShiftTitle.Text = "";
            txtShiftInitial.Text = "";
            txtFromTime.Text = "";
            txtTimeTo.Text = "";
            txtGraceTime.Text = "";
            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.SelectedIndex = 0;
        }

        public void enableControls()
        {
            txtShiftCode.Enabled = true;
            txtShiftCode.ReadOnly = true;
            txtShiftTitle.Enabled = true;
            txtShiftInitial.Enabled = true;
            txtFromTime.Enabled = true;
            txtTimeTo.Enabled = true;
            txtGraceTime.Enabled = true;
            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.Enabled = true;
        }

        public void disableControls()
        {
            txtShiftCode.Enabled = false;
            txtShiftCode.ReadOnly = true;
            txtShiftTitle.Enabled = false;
            txtShiftInitial.Enabled = false;
            txtFromTime.Enabled = false;
            txtTimeTo.Enabled = false;
            txtGraceTime.Enabled = false;
            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.Enabled = false;
        }

        public void onGenerateButtonClick()
        {
            lblActionMode.Text = "add";
            lblShiftID.Text = "";
            btnSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = false;
            btnCancel.Enabled = true;
        }

        public void onModifyButtonClick()
        {
            lblActionMode.Text = "modify";
            lblShiftID.Text = "";
            btnSearch.Enabled = true;
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = false;
            btnCancel.Enabled = true;
        }

        public void onRemoveButtonClick()
        {
            lblActionMode.Text = "remove";
            lblShiftID.Text = "";
            btnSearch.Enabled = true;
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onSaveButtonClick()
        {
            lblActionMode.Text = "";
            lblShiftID.Text = "";
            btnSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onCancelButtonClick()
        {
            lblActionMode.Text = "";
            lblShiftID.Text = "";
            btnSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void displaySelectedValuesOnUI(ShiftInfo ShiftsModel)
        {
            lnkViewAuditLog.Visible = true;
            lblShiftID.Text = ShiftsModel.ShiftID.ToString();
            txtShiftCode.Text = ShiftsModel.ShiftCode;
            txtShiftTitle.Text = ShiftsModel.ShiftTitle;
            txtShiftInitial.Text = ShiftsModel.ShiftInitital;
            txtFromTime.Text = ShiftsModel.ShiftStart.HasValue ? ShiftsModel.ShiftStart.Value.ToString("hh:mm:ss") : "";
            txtTimeTo.Text = ShiftsModel.ShiftEnd.HasValue ? ShiftsModel.ShiftEnd.Value.ToString("hh:mm:ss") : "";
            txtGraceTime.Text = ShiftsModel.GracePeriod.HasValue ? ShiftsModel.GracePeriod.Value.ToString("mm:ss") : "";
            cmbIsActive.Text = ShiftsModel.IsActive == true ? "Yes" : "No";
            _originalValues = AuditLogger.getOriginalValues(this);
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

        private void btnModifyDetails_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            string strValidationMessage = objLogin.ValidateUserRolesAndResponsibilitiesInfo(objTempCurrentlyLoggedInUserInfo.EmpID, "update");
            if (strValidationMessage != "Success")
            {
                MessageBox.Show(strValidationMessage, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            lblActionMode.Text = "modify"; 
            onModifyButtonClick();
            clearControls();
            enableControls();
            cmbIsActive.SelectedIndex = 1;
            errValidator.Clear();
        }

        private void btnRemoveDetails_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            string strValidationMessage = objLogin.ValidateUserRolesAndResponsibilitiesInfo(objTempCurrentlyLoggedInUserInfo.EmpID, "delete");
            if (strValidationMessage != "Success")
            {
                MessageBox.Show(strValidationMessage, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (lblActionMode.Text == "" || lblActionMode.Text == "remove")
            {
                lblActionMode.Text = "remove";
                onRemoveButtonClick();
                clearControls();
                enableControls();
                cmbIsActive.SelectedIndex = 1;
            }
            else if (lblActionMode.Text == "delete")
            {
                if (MessageBox.Show("The selected record will be deleted. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int affectedRows = objShiftMas.DeleteShiftMasInfo(Convert.ToInt16(lblShiftID.Text.Trim()));
                    if (affectedRows > 0)
                        MessageBox.Show("Details deleted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                onCancelButtonClick();
                disableControls();
                clearControls();
                cmbIsActive.SelectedIndex = 0;
                lblActionMode.Text = "";
                errValidator.Clear();
            }
        }

        private void frmShiftMaster_KeyDown(object sender, KeyEventArgs e)
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

        private void lnkViewAuditLog_LinkClicked(object sender, EventArgs e)
        {
            frmAuditLogStatements objAuditLogStatements = new frmAuditLogStatements(Convert.ToInt32(lblShiftID.Text.ToString()), "ShiftsMasterInfo", "Shifts Master Information", Convert.ToInt32(objTempClientFinYearInfo.ClientID));
            objAuditLogStatements.ShowDialog(this);
        }
    }
}
