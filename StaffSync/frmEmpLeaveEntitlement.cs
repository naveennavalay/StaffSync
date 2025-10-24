using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Excel;
using ModelStaffSync;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static C1.Util.Win.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StaffSync
{
    public partial class frmEmpLeaveEntitlement : Form
    {
        DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster();
        DALStaffSync.clsDepartment objDepartment = new DALStaffSync.clsDepartment();
        DALStaffSync.clsDesignation objDesignation = new DALStaffSync.clsDesignation();
        DALStaffSync.clsLeaveTypeMas objLeaveTypeInfo = new DALStaffSync.clsLeaveTypeMas();
        DALStaffSync.clsLeaveTRList objLeaveTRList = new DALStaffSync.clsLeaveTRList();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        clsImpageOperation objImpageOperation = new clsImpageOperation();
        DALStaffSync.clsEmpLeaveEntitlementInfo objEmpLeaveEntitlementInfo = new DALStaffSync.clsEmpLeaveEntitlementInfo();
        //Download objDownload = new Download();
        DALStaffSync.clsPhotoMas objPhotoMas = new DALStaffSync.clsPhotoMas();
        frmDashboard objDashboard = (frmDashboard) System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();

        public frmEmpLeaveEntitlement()
        {
            InitializeComponent();
        }

        public frmEmpLeaveEntitlement(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
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

        private void picRefreshLeaveTRList_Click(object sender, EventArgs e)
        {
            RefreshLeavesHistoryList();
        }

        private void btnModifyDetails_Click(object sender, EventArgs e)
        {
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
            lblEmpID.Text = "";
            txtEmpCode.Text = "";
            errValidator.Clear();

            cmbDepartment.DataSource = objDepartment.GetDepartmentList();
            cmbDepartment.DisplayMember = "DepartmentTitle";
            cmbDepartment.ValueMember = "DepartmentID";
            cmbDepartment.SelectedIndex = 0;

            cmbDesignation.DataSource = objDesignation.GetDesignationList();
            cmbDesignation.DisplayMember = "DesignationTitle";
            cmbDesignation.ValueMember = "DesignationID";
            cmbDesignation.SelectedIndex = 0;

            //LeaveDurationList();
        }

        //private void LeaveDurationList()
        //{
        //    cmbDuration.Items.Clear();
        //    cmbDuration.Items.Add("Full Day");
        //    cmbDuration.Items.Add("First Half");
        //    cmbDuration.Items.Add("Second Half");
        //    cmbDuration.SelectedIndex = 0;
        //}

        private void btnGenerateDetails_Click(object sender, EventArgs e)
        {
            lblActionMode.Text = "add";
            onGenerateButtonClick();
            clearControls();
            enableControls();
            errValidator.Clear();

            cmbDepartment.DataSource = objDepartment.GetDepartmentList();
            cmbDepartment.DisplayMember = "DepartmentTitle";
            cmbDepartment.ValueMember = "DepartmentID";
            cmbDepartment.SelectedIndex = 0;

            cmbDesignation.DataSource = objDesignation.GetDesignationList();
            cmbDesignation.DisplayMember = "DesignationTitle";
            cmbDesignation.ValueMember = "DesignationID";
            cmbDesignation.SelectedIndex = 0;

            //LeaveDurationList();
        }

        private void btnRemoveDetails_Click(object sender, EventArgs e)
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
            
            this.Cursor = Cursors.WaitCursor;

            if (lblActionMode.Text == "add" || lblActionMode.Text == "modify")
            {
                int employeeLeaveTRID = 0;
                int leaveMasID = 0;

                if (chkNewAllotment.Checked == false)
                {
                    foreach (DataGridViewRow dc in dtgLeaveEntitlement.Rows)
                    {
                        if(Convert.ToInt16(dc.Cells["LeaveEntmtID"].Value.ToString()) == 0)
                        {
                            employeeLeaveTRID = objEmpLeaveEntitlementInfo.InsertLeaveEntitlementInfo(Convert.ToInt16(lblEmpID.Text.Trim()), Convert.ToInt16(lblLeaveMasID.Text.ToString()), Convert.ToInt16(dc.Cells["LeaveTypeID"].Value.ToString()), Convert.ToDecimal(dc.Cells["TotalLeaves"].Value.ToString()), Convert.ToDecimal(dc.Cells["BalanceLeaves"].Value.ToString()), Convert.ToInt16(dc.Cells["OrderID"].Value.ToString()));
                            dc.Cells["LeaveEntmtID"].Value = employeeLeaveTRID;
                        }
                        else if (Convert.ToInt16(dc.Cells["LeaveEntmtID"].Value.ToString()) > 0)
                        {
                            employeeLeaveTRID = objEmpLeaveEntitlementInfo.UpadateLeaveEntitlementInfo(Convert.ToInt16(dc.Cells["LeaveEntmtID"].Value.ToString()), Convert.ToInt16(lblEmpID.Text.Trim()), Convert.ToInt16(lblLeaveMasID.Text.ToString()), Convert.ToInt16(dc.Cells["LeaveTypeID"].Value.ToString()), Convert.ToDecimal(dc.Cells["TotalLeaves"].Value.ToString()), Convert.ToDecimal(dc.Cells["BalanceLeaves"].Value.ToString()), Convert.ToInt16(dc.Cells["OrderID"].Value.ToString()));
                        }
                        employeeLeaveTRID = objEmpLeaveEntitlementInfo.UpadateLeaveEntitlementInfo(Convert.ToInt16(dc.Cells["LeaveEntmtID"].Value.ToString()), Convert.ToInt16(lblEmpID.Text.Trim()), Convert.ToInt16(lblLeaveMasID.Text.ToString()), Convert.ToInt16(dc.Cells["LeaveTypeID"].Value.ToString()), Convert.ToDecimal(dc.Cells["TotalLeaves"].Value.ToString()), Convert.ToDecimal(dc.Cells["BalanceLeaves"].Value.ToString()), Convert.ToInt16(dc.Cells["OrderID"].Value.ToString()));
                    }
                    leaveMasID = objLeaveTRList.UpdateEmployeeLeaveBalance(Convert.ToInt16(lblLeaveMasID.Text.ToString()), Convert.ToInt16(lblEmpID.Text.Trim()), Convert.ToDecimal(txtTotalLeavesAlloted.Text), Convert.ToDecimal(txtTotalBalanceLeaves.Text), DateTime.Now);
                }
                else if (chkNewAllotment.Checked == true)
                {
                    leaveMasID = objLeaveTRList.InsertDefaultLeaveAllotment(Convert.ToInt16(lblEmpID.Text.Trim()), 0, 0, Convert.ToDateTime(txtEffectiveFrom.Text));
                    lblLeaveMasID.Text = leaveMasID.ToString();
                    foreach (DataGridViewRow dc in dtgLeaveEntitlement.Rows)
                    {
                        employeeLeaveTRID = objEmpLeaveEntitlementInfo.InsertLeaveEntitlementInfo(Convert.ToInt16(lblEmpID.Text.Trim()), Convert.ToInt16(lblLeaveMasID.Text.ToString()), Convert.ToInt16(dc.Cells["LeaveTypeID"].Value.ToString()), Convert.ToDecimal(dc.Cells["TotalLeaves"].Value.ToString()), Convert.ToDecimal(dc.Cells["BalanceLeaves"].Value.ToString()), Convert.ToInt16(dc.Cells["OrderID"].Value.ToString()));
                    }
                    leaveMasID = objLeaveTRList.UpdateEmployeeLeaveBalance(Convert.ToInt16(lblLeaveMasID.Text.ToString()), Convert.ToInt16(lblEmpID.Text.Trim()), Convert.ToDecimal(txtTotalLeavesAlloted.Text), Convert.ToDecimal(txtTotalBalanceLeaves.Text), DateTime.Now);
                }
                if (employeeLeaveTRID > 0)
                {
                    //objLeaveTRList.UpdateEmployeeLeaveBalance(Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToDecimal(txtAvailableLeave.Text), Convert.ToDecimal(txtBalanceLeave.Text));
                    RefreshLeavesHistoryList();
                    MessageBox.Show("Details updated successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Details not inserted successfully.\nPlease verify once again.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            objTempCurrentlyLoggedInUserInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objTempCurrentlyLoggedInUserInfo.EmpID.ToString()));

            onSaveButtonClick();
            disableControls();
            clearControls();
            errValidator.Clear();
            this.Cursor = Cursors.Default;
        }


        public void onGenerateButtonClick()
        {
            lblActionMode.Text = "add";
            lblEmpID.Text = "";
            lblCancelStatus.Text = "";
            btnSearch.Enabled = true;
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
            lblCancelStatus.Text = "";
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
            lblEmpID.Text = "";
            lblCancelStatus.Text = "";
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
            lblEmpID.Text = "";
            lblCancelStatus.Text = "";
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
            lblEmpID.Text = "";
            lblCancelStatus.Text = "";
            btnSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void clearControls()
        {
            lblEmpID.Text = "";
            lblLeaveTRID.Text = "";
            lblLeaveMasID.Text = "";
            txtEmpCode.Text = "";
            txtEmployeeName.Text = "";
            picEmpPhoto.Image = null;
            chkNewAllotment.Checked = false;

            lblCancelStatus.Text = "";

            txtEffectiveFrom.Text = DateTime.Now.ToString("dd-MM-yyyy");

            cmbDesignation.DataSource = null;
            cmbDepartment.DataSource = null;

            dtgLeaveEntitlement.DataSource = null;
            dtgLeaveEntitlement.DataSource = objEmpLeaveEntitlementInfo.getDefaultLeaveEntitilementList();
            LeaveEntitlementTableFormat();

            txtTotalLeavesAlloted.Text = "0.00";
            txtTotalBalanceLeaves.Text = "0.00";
            txtTotalUtilised.Text = "0.00";

            decimal totalLeavesAllotted = 0;
            decimal totalBalanceLeaves = 0;
            foreach (DataGridViewRow dc in dtgLeaveEntitlement.Rows)
            {
                totalLeavesAllotted = totalLeavesAllotted + Convert.ToDecimal(dc.Cells["TotalLeaves"].Value.ToString());
                totalBalanceLeaves = totalBalanceLeaves + Convert.ToDecimal(dc.Cells["BalanceLeaves"].Value.ToString());
            }

            dtgLeaveEntitlement.Columns["TotalLeaves"].ReadOnly = true;
            txtTotalLeavesAlloted.Text = Convert.ToDecimal(totalLeavesAllotted.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtTotalBalanceLeaves.Text = Convert.ToDecimal(totalBalanceLeaves.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtTotalUtilised.Text = Convert.ToDecimal(totalLeavesAllotted - totalBalanceLeaves).ToString("0.00", CultureInfo.InvariantCulture);
        }

        public void enableControls()
        {
            txtEmpCode.Enabled = false;
            txtEmployeeName.Enabled = false;
            cmbDesignation.Enabled = false;
            cmbDepartment.Enabled = false;
            txtEffectiveFrom.Enabled = true;
        }

        public void disableControls()
        {
            txtEmpCode.Enabled = false;
            txtEmployeeName.Enabled = false;
            cmbDesignation.Enabled = false;
            cmbDepartment.Enabled = false;
            txtEffectiveFrom.Enabled = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listEmpLeaveEntitlements");
            frmEmployeeList.ShowDialog();
        }

        public void SelectedEmployeeID(string SearchOptionSelectedForm, int selectedEmployeeID)
        {
            if (SearchOptionSelectedForm == "listEmpLeaveEntitlements")
            {
                lblEmpID.Text = selectedEmployeeID.ToString();
                EmployeeInfo objSelectedEmployeeInfo = objEmployeeMaster.GetSelectedEmployeeInfo(Convert.ToInt16(lblEmpID.Text));
                txtEmpCode.Text = objSelectedEmployeeInfo.EmpCode;
                txtEmployeeName.Text = objSelectedEmployeeInfo.EmpName;
                cmbDesignation.SelectedIndex = objSelectedEmployeeInfo.EmpDesignationID - 1;
                cmbDepartment.SelectedIndex = objSelectedEmployeeInfo.DepartmentID - 1;
                picEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(lblEmpID.Text)).EmpPhoto);

                RefreshLeavesHistoryList();
            }
        }

        private void frmEmpLeaveEntitlement_Load(object sender, EventArgs e)
        {
            lblActionMode.Text = "";
            onCancelButtonClick();
            disableControls();
            clearControls();
            errValidator.Clear();
        }

        private void lstLeaveTRList_DoubleClick(object sender, EventArgs e)
        {
            //MessageBox.Show(lstLeaveType.SelectedItems[0].SubItems[0].Text.ToString());

            //DateTime dtLeaveAppliedDate = Convert.ToDateTime(lstLeaveType.SelectedItems[0].SubItems[2].Text.ToString());
            //MessageBox.Show(dtLeaveAppliedDate.ToString("dd-MM-yyyy"));
        }

        private void txtLeaveDateTo_TextChanged(object sender, EventArgs e)
        {
            //LeaveCalculation();
        }

        private void txtLeaveDateFrom_TextChanged(object sender, EventArgs e)
        {
            //LeaveCalculation();
        }

        private void cmbDuration_SelectedIndexChanged(object sender, EventArgs e)
        {
            //LeaveCalculation();
        }

        //private void LeaveCalculation()
        //{

        //    if (lblEmpID.Text.Trim() == "")
        //        return;

        //    //txtLeaveDateFrom.Text = txtLeaveDateFrom.Text.ToString().Trim() == "-  -" ? DateTime.Now.ToString("dd-MM-yyyy") : DateTime.Now.ToString("dd-MM-yyyy");
        //    //txtLeaveDateTo.Text = txtLeaveDateTo.Text.ToString().Trim() == "-  -" ? DateTime.Now.ToString("dd-MM-yyyy") : txtLeaveDateTo.Text.ToString().Trim();
        //    DateTime dtFrom = IsDateTime(txtDateAsOn.Text.ToString()) == true ? Convert.ToDateTime(txtDateAsOn.Text.ToString()) : DateTime.Now;
        //    DateTime dtTo = IsDateTime(txtLeaveDateTo.Text.ToString()) == true ? Convert.ToDateTime(txtLeaveDateTo.Text.ToString()) : DateTime.Now;
        //    if (txtAvailableLeave.Text.ToString().Trim() != "" &&  IsDateTime(txtDateAsOn.Text.ToString()) && IsDateTime(txtLeaveDateTo.Text.ToString()))
        //    {
        //        if (cmbDuration.SelectedIndex == 0)
        //        {
        //            txtActualLeaveDays.Text = ((dtTo - dtFrom).TotalDays + 1).ToString();
        //            txtBalanceLeave.Text = (Convert.ToDecimal(txtAvailableLeave.Text.ToString()) - Convert.ToDecimal((dtTo - dtFrom).TotalDays + 1)).ToString();
        //        }
        //        else
        //        {
        //            txtActualLeaveDays.Text = (((dtTo - dtFrom).TotalDays + 1) / 2).ToString();
        //            txtBalanceLeave.Text = (Convert.ToDecimal(txtAvailableLeave.Text.ToString()) - Convert.ToDecimal((dtTo - dtFrom).TotalDays + 1) / 2).ToString();
        //        }
        //    }
        //}

        public bool IsDateTime(string dtValue)
        {
            Regex regex = new Regex(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$");

            //Verify whether date entered in dd/MM/yyyy format.
            bool isValid = regex.IsMatch(dtValue.Trim());

            bool validStatus = true;
            if (string.IsNullOrEmpty(dtValue))
                validStatus = false;

            DateTime dt;
            isValid = DateTime.TryParseExact(dtValue, "dd-MM-yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out dt);
            if (!isValid)
            {
                validStatus = false;
            }
            return validStatus;
        }

        private void RefreshLeavesHistoryList()
        {
            if (lblEmpID.Text.Trim() == "")
                return;

            lblLeaveMasID.Text = objLeaveTRList.getMaxLeaveMasID(Convert.ToInt16(lblEmpID.Text)).ToString();
            //txtTotalLeaveAllotment.Text = objLeaveTRList.getBalanceLeave(Convert.ToInt16(lblEmpID.Text)).ToString();
            //txtBalanceLeaveAllotment.Text = txtTotalLeaveAllotment.Text;

            dtgLeaveEntitlement.DataSource = null;
            dtgLeaveEntitlement.DataSource = objEmpLeaveEntitlementInfo.getEmployeeLeaveEntitilementList(Convert.ToInt16(lblEmpID.Text), Convert.ToInt16(lblLeaveMasID.Text.ToString()));
            LeaveEntitlementTableFormat();
        }

        private void LeaveEntitlementTableFormat()
        {
            dtgLeaveEntitlement.Columns["LeaveEntmtID"].Visible = false;
            dtgLeaveEntitlement.Columns["EmpID"].Visible = false;
            dtgLeaveEntitlement.Columns["EmpID"].ReadOnly = true;
            dtgLeaveEntitlement.Columns["LeaveMasID"].Visible = false;
            dtgLeaveEntitlement.Columns["LeaveMasID"].ReadOnly = true;
            dtgLeaveEntitlement.Columns["LeaveTypeID"].Visible = false;
            dtgLeaveEntitlement.Columns["LeaveTypeID"].ReadOnly = true;
            dtgLeaveEntitlement.Columns["LeaveTypeTitle"].ReadOnly = true;
            dtgLeaveEntitlement.Columns["LeaveTypeTitle"].Width = 350;

            dtgLeaveEntitlement.Columns["TotalLeaves"].Width = 135;
            dtgLeaveEntitlement.Columns["TotalLeaves"].ReadOnly = false;
            dtgLeaveEntitlement.Columns["TotalLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgLeaveEntitlement.Columns["TotalLeaves"].DefaultCellStyle.Format = "0.00";

            dtgLeaveEntitlement.Columns["BalanceLeaves"].Width = 135;
            dtgLeaveEntitlement.Columns["BalanceLeaves"].ReadOnly = true;
            dtgLeaveEntitlement.Columns["BalanceLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgLeaveEntitlement.Columns["BalanceLeaves"].DefaultCellStyle.Format = "0.00";

            dtgLeaveEntitlement.Columns["UsedLeaves"].Width = 135;
            dtgLeaveEntitlement.Columns["UsedLeaves"].ReadOnly = true;
            dtgLeaveEntitlement.Columns["UsedLeaves"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgLeaveEntitlement.Columns["UsedLeaves"].DefaultCellStyle.Format = "0.00";
            dtgLeaveEntitlement.Columns["OrderID"].Visible = false;

            decimal totalLeavesAllotted = 0;
            decimal totalBalanceLeaves = 0;
            txtTotalLeavesAlloted.Text = "0.00";
            txtTotalBalanceLeaves.Text = "0.00";
            txtTotalUtilised.Text = "0.00";
            foreach (DataGridViewRow dc in dtgLeaveEntitlement.Rows)
            {
                totalLeavesAllotted = totalLeavesAllotted + Convert.ToDecimal(dc.Cells["TotalLeaves"].Value.ToString());
                totalBalanceLeaves = totalBalanceLeaves + Convert.ToDecimal(dc.Cells["BalanceLeaves"].Value.ToString());
                if (Convert.ToDecimal(dc.Cells["BalanceLeaves"].Value.ToString()) < Convert.ToDecimal(dc.Cells["TotalLeaves"].Value.ToString()))
                {
                    dc.Cells["BalanceLeaves"].Style.BackColor = Color.LightPink;
                }
            }

            txtTotalLeavesAlloted.Text = Convert.ToDecimal(totalLeavesAllotted.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtTotalBalanceLeaves.Text = Convert.ToDecimal(totalBalanceLeaves.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtTotalUtilised.Text = Convert.ToDecimal(totalLeavesAllotted - totalBalanceLeaves).ToString("0.00", CultureInfo.InvariantCulture);
        }

        private void lstLeaveTRList_MouseUp(object sender, MouseEventArgs e)
        {
            //if (lblActionMode.Text == "add")
            //{
            //    if (lstLeaveType.SelectedItems[0].SubItems[4].Text.ToString() != "0" && lstLeaveType.SelectedItems[0].SubItems[6].Text.ToString() == "Pending") //"LeaveDuration"                    
            //    {
            //        if (e.Button == MouseButtons.Right)
            //        {
            //            tlbCancelLeave.Visible = true;
            //        }
            //    }
            //    else
            //        tlbCancelLeave.Visible = false;
            //}
        }

        private void cmLeaveCancel_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //lblCancelStatus.Text = lstLeaveType.SelectedItems[0].SubItems[0].Text.ToString();
        }

        private void picDownloadLeaveTRList_Click(object sender, EventArgs e)
        {
           
        }

        private void dtgLeaveEntitlement_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            decimal totalLeavesAllotted = 0;
            decimal totalBalanceLeaves = 0;
            txtTotalLeavesAlloted.Text = "0.00";
            txtTotalBalanceLeaves.Text = "0.00";
            txtTotalUtilised.Text = "0.00";
            if (dtgLeaveEntitlement.CurrentRow.Cells["BalanceLeaves"].Style.BackColor != Color.LightPink)
            {
                dtgLeaveEntitlement.CurrentRow.Cells["BalanceLeaves"].Value = Convert.ToDecimal(dtgLeaveEntitlement.CurrentRow.Cells["TotalLeaves"].Value.ToString());
            }
            foreach (DataGridViewRow dc in dtgLeaveEntitlement.Rows)
            {
                totalLeavesAllotted = totalLeavesAllotted + Convert.ToDecimal(dc.Cells["TotalLeaves"].Value.ToString());
                totalBalanceLeaves = totalBalanceLeaves + Convert.ToDecimal(dc.Cells["BalanceLeaves"].Value.ToString());
                //dc.Cells["BalanceLeaves"].Value = Convert.ToDecimal(dc.Cells["TotalLeaves"].Value.ToString());
            }

            txtTotalLeavesAlloted.Text = Convert.ToDecimal(totalLeavesAllotted.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtTotalBalanceLeaves.Text = Convert.ToDecimal(totalBalanceLeaves.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtTotalUtilised.Text = Convert.ToDecimal(totalLeavesAllotted - totalBalanceLeaves).ToString("0.00", CultureInfo.InvariantCulture);
        }

        private void picAddLeave_Click(object sender, EventArgs e)
        {
            frmLeaveTypeList frmLeaveTypeList = new frmLeaveTypeList(this, "listEmpLeaveEntitlements");
            frmLeaveTypeList.ShowDialog(this);
        }

        public void displaySelectedValuesOnUI(LeaveTypeInfoModel LeaveTypeInfoModel)
        {
            List<int> LeaveTypeIDs = new List<int>();
            if (dtgLeaveEntitlement.Rows.Count > 0)
            {
                foreach (DataGridViewRow dc in dtgLeaveEntitlement.Rows)
                {
                    LeaveTypeIDs.Add(Convert.ToInt16(dc.Cells["LeaveTypeID"].Value.ToString()));
                }
            }

            int[] arrLeaveTypeIDs = LeaveTypeIDs.ToArray();

            dtgLeaveEntitlement.DataSource = null;
            dtgLeaveEntitlement.DataSource = objEmpLeaveEntitlementInfo.AddNewEntryOnGridEmployeeLeaveEntitilementList(Convert.ToInt16(lblEmpID.Text), arrLeaveTypeIDs, LeaveTypeInfoModel.LeaveTypeID);
            LeaveEntitlementTableFormat();
        }

        private void frmEmpLeaveEntitlement_KeyDown(object sender, KeyEventArgs e)
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
                this.Close();
            }
        }
    }
}
