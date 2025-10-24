using Microsoft.Office.Interop.Excel;
using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static C1.Util.Win.Win32;

namespace StaffSync
{
    public partial class frmLeavesApproval : Form
    {
        DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster();
        DALStaffSync.clsDepartment objDepartment = new DALStaffSync.clsDepartment();
        DALStaffSync.clsDesignation objDesignation = new DALStaffSync.clsDesignation();
        DALStaffSync.clsLeaveTypeMas objLeaveTypeInfo = new DALStaffSync.clsLeaveTypeMas();
        DALStaffSync.clsLeaveTRList objLeaveTRList = new DALStaffSync.clsLeaveTRList();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        clsImpageOperation objImpageOperation = new clsImpageOperation();
        //Download objDownload = new Download();
        DALStaffSync.clsPhotoMas objPhotoMas = new DALStaffSync.clsPhotoMas();
        DALStaffSync.clsAttendanceMas objAttendanceInfo = new DALStaffSync.clsAttendanceMas();
        frmDashboard objDashboard = (frmDashboard) System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();

        public frmLeavesApproval()
        {
            InitializeComponent();
        }

        public frmLeavesApproval(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
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

            cmbLeaveType.DataSource = objLeaveTypeInfo.GetLeaveTypeList();
            cmbLeaveType.DisplayMember = "LeaveTypeTitle";
            cmbLeaveType.ValueMember = "LeaveTypeID";
            cmbLeaveType.SelectedIndex = 0;

            LeaveDurationList();
        }

        private void LeaveDurationList()
        {
            cmbDuration.Items.Clear();
            cmbDuration.Items.Add("Full Day");
            cmbDuration.Items.Add("First Half");
            cmbDuration.Items.Add("Second Half");
            cmbDuration.SelectedIndex = 0;
        }

        private void btnGenerateDetails_Click(object sender, EventArgs e)
        {
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

            cmbDepartment.DataSource = objDepartment.GetDepartmentList();
            cmbDepartment.DisplayMember = "DepartmentTitle";
            cmbDepartment.ValueMember = "DepartmentID";
            cmbDepartment.SelectedIndex = 0;

            cmbDesignation.DataSource = objDesignation.GetDesignationList();
            cmbDesignation.DisplayMember = "DesignationTitle";
            cmbDesignation.ValueMember = "DesignationID";
            cmbDesignation.SelectedIndex = 0;

            cmbLeaveType.DataSource = objLeaveTypeInfo.GetLeaveTypeList();
            cmbLeaveType.DisplayMember = "LeaveTypeTitle";
            cmbLeaveType.ValueMember = "LeaveTypeID";
            cmbLeaveType.SelectedIndex = 0;

            LeaveDurationList();
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
            string strAttendanceStatus = cmbDuration.SelectedItem.ToString() == "Full Day" ? "Leave" : cmbDuration.SelectedItem.ToString() + " Day Leave";
            strAttendanceStatus = "Present";
            if (lblActionMode.Text == "add")
            {
                int employeeLeaveTRID = 0;
                if (Convert.ToDecimal(txtActualLeaveDays.Text) > 0)
                {
                    for (int iLeaveCounter = 1; iLeaveCounter <= 1; iLeaveCounter++)
                    {
                        employeeLeaveTRID = objLeaveTRList.ApproveLeave(Convert.ToInt16(lblLeaveTRID.Text.ToString()), Convert.ToInt16(lblEmpID.Text.ToString()), txtApprovalNote.Text, clsCurrentUser.UserID);
                        if (lstLeaveTRList.SelectedItems[0].SubItems[8].Text.ToString().ToLower() == "")
                        {
                            objLeaveTRList.UpdateEmployeeLeaveBalance(Convert.ToInt16(lblLeaveMasID.Text.ToString()), Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToDecimal(txtAvailableLeave.Text), (Convert.ToDecimal(txtBalanceLeave.Text)), DateTime.Now);
                            objAttendanceInfo.InsertDailyAttendance(Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToDateTime(txtLeaveDateFrom.Text.ToString()), "Leave : " + lstLeaveTRList.SelectedItems[0].SubItems[6].Text.ToString(), Convert.ToInt16(lblLeaveTRID.Text.ToString()));
                        }
                        else if (lstLeaveTRList.SelectedItems[0].SubItems[8].Text.ToString().ToLower() == "cancelled")
                        {
                            objLeaveTRList.UpdateEmployeeLeaveBalance(Convert.ToInt16(lblLeaveMasID.Text.ToString()), Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToDecimal(txtAvailableLeave.Text), (Convert.ToDecimal(txtBalanceLeave.Text) + Convert.ToDecimal(txtActualLeaveDays.Text)), DateTime.Now);
                            objAttendanceInfo.InsertDailyAttendance(Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToDateTime(txtLeaveDateFrom.Text.ToString()), "Present", Convert.ToInt16(lblLeaveTRID.Text.ToString()));
                        }
                    }
                }
                else if (Convert.ToDecimal(txtActualLeaveDays.Text) < 0)
                {
                    employeeLeaveTRID = objLeaveTRList.ApproveLeaveCancellation(Convert.ToInt16(lblLeaveTRID.Text.ToString()), Convert.ToInt16(lblEmpID.Text.ToString()), txtApprovalNote.Text, clsCurrentUser.UserID);
                    if (lstLeaveTRList.SelectedItems[0].SubItems[8].Text.ToString().ToLower() == "")
                    {
                        objLeaveTRList.UpdateSpecificLeaveTypeBalance(Convert.ToInt16(lblLeaveMasID.Text.ToString()), Convert.ToInt16(cmbLeaveType.SelectedIndex + 1), (Convert.ToDecimal(lblSpecificLeaveBalance.Text.ToString()) - Convert.ToDecimal(txtActualLeaveDays.Text.ToString())));
                        objAttendanceInfo.InsertDailyAttendance(Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToDateTime(txtLeaveDateFrom.Text.ToString()), lstLeaveTRList.SelectedItems[0].SubItems[5].Text.ToString(), Convert.ToInt16(lblLeaveTRID.Text.ToString()));
                    }
                    else if (lstLeaveTRList.SelectedItems[0].SubItems[8].Text.ToString().ToLower() == "cancelled")
                    {
                        objLeaveTRList.UpdateSpecificLeaveTypeBalance(Convert.ToInt16(lblLeaveMasID.Text.ToString()), Convert.ToInt16(cmbLeaveType.SelectedIndex + 1), (Convert.ToDecimal(lblSpecificLeaveBalance.Text.ToString()) - Convert.ToDecimal(txtActualLeaveDays.Text.ToString())));
                        objAttendanceInfo.InsertDailyAttendance(Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToDateTime(txtLeaveDateFrom.Text.ToString()), lstLeaveTRList.SelectedItems[0].SubItems[5].Text.ToString(), Convert.ToInt16(lblLeaveTRID.Text.ToString()));
                    }
                }
                if (employeeLeaveTRID > 0)
                {
                    //objLeaveTRList.UpdateEmployeeLeaveBalance(Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToDecimal(txtAvailableLeave.Text), Convert.ToDecimal(txtBalanceLeave.Text));
                    RefreshLeavesHistoryList();
                    MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Details not inserted successfully.\nPlease verify once again.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            //else if (lblActionMode.Text == "modify")
            //{
            //    int employeeLeaveTRID = 0;
            //    if (cmbDuration.SelectedIndex == 0)
            //    {
            //        DateTime LeaveDate = Convert.ToDateTime(txtLeaveDateFrom.Text);
            //        for (int iLeaveCounter = 1; iLeaveCounter <= Convert.ToInt16(txtActualLeaveDays.Text); iLeaveCounter++)
            //        {
            //            employeeLeaveTRID = objLeaveTRList.InsertLeaveTransaction(Convert.ToInt16(lblEmpID.Text.ToString()), cmbLeaveType.SelectedIndex + 1, DateTime.Now, txtApprovalNote.Text.Trim(), Convert.ToDateTime(LeaveDate.ToString("dd-MM-yyyy")), Convert.ToDateTime(LeaveDate.ToString("dd-MM-yyyy")), Convert.ToDecimal(1), DateTime.Now, "Not yet Approved", DateTime.Now, "Not yet Rejected", Convert.ToInt16(lblEmpID.Text.ToString()));
            //            LeaveDate = Convert.ToDateTime(txtLeaveDateFrom.Text).AddDays(iLeaveCounter);
            //        }
            //    }
            //    else
            //    {
            //        employeeLeaveTRID = objLeaveTRList.InsertLeaveTransaction(Convert.ToInt16(lblEmpID.Text.ToString()), cmbLeaveType.SelectedIndex + 1, DateTime.Now, txtApprovalNote.Text.Trim(), Convert.ToDateTime(txtLeaveDateFrom.Text), Convert.ToDateTime(txtLeaveDateTo.Text), Convert.ToDecimal(txtActualLeaveDays.Text), DateTime.Now, "Not yet Approved", DateTime.Now, "Not yet Rejected", Convert.ToInt16(lblEmpID.Text.ToString()));
            //    }
            //    if (employeeLeaveTRID > 0)
            //    {
            //        //objLeaveTRList.UpdateEmployeeLeaveBalance(Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToDecimal(txtAvailableLeave.Text), Convert.ToDecimal(txtBalanceLeave.Text));
            //        RefreshLeavesHistoryList();
            //        MessageBox.Show("Details updated successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    else
            //    {
            //        MessageBox.Show("Details not inserted successfully.\nPlease verify once again.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //}
            
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
            lblLeaveTRID.Text = "";
            lblSpecificLeaveBalance.Text = "";
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
            lblLeaveTRID.Text = "";
            lblSpecificLeaveBalance.Text = "";
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
            lblLeaveTRID.Text = "";
            lblSpecificLeaveBalance.Text = "";
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
            lblLeaveTRID.Text = "";
            lblSpecificLeaveBalance.Text = "";
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
            lblLeaveTRID.Text = "";
            lblSpecificLeaveBalance.Text = "";
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
            txtEmpCode.Text = "";
            txtEmployeeName.Text = "";
            picEmpPhoto.Image = null;

            lblLeaveTRID.Text = "";
            lblSpecificLeaveBalance.Text = "";
            lblLeaveMasID.Text = "";

            txtAvailableLeave.Text = "";
            txtLeaveDateFrom.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtLeaveDateTo.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtActualLeaveDays.Text = "";
            txtBalanceLeave.Text = "";

            cmbDesignation.DataSource = null;
            cmbDepartment.DataSource = null;
            cmbLeaveType.DataSource = null;
            cmbDuration.DataSource = null;

            lstLeaveTRList.Items.Clear();
        }

        public void enableControls()
        {
            txtEmpCode.Enabled = false;
            txtEmployeeName.Enabled = false;
            cmbDesignation.Enabled = false;
            cmbDepartment.Enabled = false;
            txtAvailableLeave.Enabled = false;
            txtActualLeaveDays.Enabled = false;
            txtBalanceLeave.Enabled = false;
            cmbLeaveType.Enabled = false;
            cmbDuration.Enabled = false;
            txtLeaveDateFrom.Enabled = false;
            txtLeaveDateTo.Enabled = false;
            txtActualLeaveDays.Enabled = false;
        }

        public void disableControls()
        {
            txtEmpCode.Enabled = false;
            txtEmployeeName.Enabled = false;
            cmbDesignation.Enabled = false;
            cmbDepartment.Enabled = false;
            txtAvailableLeave.Enabled = false;
            txtActualLeaveDays.Enabled = false;
            txtBalanceLeave.Enabled = false;
            cmbLeaveType.Enabled = false;
            cmbDuration.Enabled = false;
            txtLeaveDateFrom.Enabled = false;
            txtLeaveDateTo.Enabled = false;
            txtActualLeaveDays.Enabled = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listEmployeeLeaveApprovalRequestList");
            frmEmployeeList.ShowDialog();
        }

        public void SelectedEmployeeID(string SearchOptionSelectedForm, int selectedEmployeeID, int selectedLeaveID)
        {
            if (SearchOptionSelectedForm == "listEmployeeLeaveApprovalRequestList")
            {
                lblEmpID.Text = selectedEmployeeID.ToString();
                EmployeeInfo objSelectedEmployeeInfo = objEmployeeMaster.GetSelectedEmployeeInfo(Convert.ToInt16(lblEmpID.Text));
                txtEmpCode.Text = objSelectedEmployeeInfo.EmpCode;
                txtEmployeeName.Text = objSelectedEmployeeInfo.EmpName;
                cmbDesignation.SelectedIndex = objSelectedEmployeeInfo.EmpDesignationID - 1;
                cmbDepartment.SelectedIndex = objSelectedEmployeeInfo.DepartmentID - 1;
                picEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(lblEmpID.Text)).EmpPhoto);

                lblLeaveMasID.Text = objLeaveTRList.getMaxLeaveMasID(Convert.ToInt16(lblEmpID.Text)).ToString();

                txtAvailableLeave.Text = objLeaveTRList.getBalanceLeave(Convert.ToInt16(lblEmpID.Text)).ToString();
                List<EmployeeSpecificLeaveInfo> objEmployeeSpecificLeaveInfo = objLeaveTRList.getSpecificEmployeeSpecificLeaveInfo(selectedLeaveID);
                if (objEmployeeSpecificLeaveInfo.Count > 0)
                {
                    lblLeaveTRID.Text = selectedLeaveID.ToString();
                    cmbLeaveType.SelectedIndex = objEmployeeSpecificLeaveInfo[0].LeaveTypeID - 1;
                    cmbDuration.SelectedIndex = objEmployeeSpecificLeaveInfo[0].LeaveDuration == 1 ? 0 : 1;
                    txtLeaveDateFrom.Text = objEmployeeSpecificLeaveInfo[0].ActualLeaveDateFrom.ToString("dd-MM-yyyy");
                    txtLeaveDateTo.Text = objEmployeeSpecificLeaveInfo[0].ActualLeaveDateTo.ToString("dd-MM-yyyy");
                    txtActualLeaveDays.Text = objEmployeeSpecificLeaveInfo[0].LeaveDuration.ToString();

                    if (objEmployeeSpecificLeaveInfo[0].LeaveComments.ToString() == "Rejecting the Leave Request")
                    {
                        txtActualLeaveDays.Text = (Convert.ToDecimal(objEmployeeSpecificLeaveInfo[0].LeaveDuration.ToString()) * -1).ToString();
                        txtBalanceLeave.Text = (Convert.ToDecimal(txtAvailableLeave.Text.ToString())).ToString();
                    }
                    else
                    {
                        txtBalanceLeave.Text = (Convert.ToDecimal(txtAvailableLeave.Text.ToString()) - Convert.ToDecimal(txtActualLeaveDays.Text.ToString())).ToString();
                    }
                }

                //LeaveCalculation();

                RefreshLeavesHistoryList();

                int iRowCounter = 0;
                if (objEmployeeSpecificLeaveInfo.Count > 0)
                {
                    foreach (ListViewItem indRow in lstLeaveTRList.Items)
                    {
                        if (indRow.SubItems[7].Text.ToString().ToLower() == "cancelled")
                        {
                            indRow.BackColor = Color.LightGray;
                        }
                        if (indRow.ToString().Contains(lblLeaveTRID.Text))
                        {
                            lstLeaveTRList.Items[iRowCounter].Selected = true;
                            break;
                        }
                        iRowCounter = iRowCounter + 1;
                    }
                }
            }
        }

        private void frmLeavesApproval_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'staffsyncDBDTSet.qryRoleProfile' table. You can move, or remove it, as needed.
            //this.qryRoleProfileTableAdapter.Fill(this.staffsyncDBDTSet.qryRoleProfile);
            lblActionMode.Text = "";
            onCancelButtonClick();
            disableControls();
            clearControls();
            errValidator.Clear();
        }

        private void lstLeaveTRList_DoubleClick(object sender, EventArgs e)
        {
            //MessageBox.Show(lstLeaveTRList.SelectedItems[0].SubItems[0].Text.ToString());
            //DateTime dtLeaveAppliedDate = Convert.ToDateTime(lstLeaveTRList.SelectedItems[0].SubItems[2].Text.ToString());
            //MessageBox.Show(dtLeaveAppliedDate.ToString("dd-MM-yyyy"));
        }

        private void txtLeaveDateTo_TextChanged(object sender, EventArgs e)
        {
            LeaveCalculation();
        }

        private void txtLeaveDateFrom_TextChanged(object sender, EventArgs e)
        {
            LeaveCalculation();
        }

        private void cmbDuration_SelectedIndexChanged(object sender, EventArgs e)
        {
            LeaveCalculation();
        }

        private void LeaveCalculation()
        {

            if (lblEmpID.Text.Trim() == "")
                return;

            //txtLeaveDateFrom.Text = txtLeaveDateFrom.Text.ToString().Trim() == "-  -" ? DateTime.Now.ToString("dd-MM-yyyy") : DateTime.Now.ToString("dd-MM-yyyy");
            //txtLeaveDateTo.Text = txtLeaveDateTo.Text.ToString().Trim() == "-  -" ? DateTime.Now.ToString("dd-MM-yyyy") : txtLeaveDateTo.Text.ToString().Trim();
            DateTime dtFrom = IsDateTime(txtLeaveDateFrom.Text.ToString()) == true ? Convert.ToDateTime(txtLeaveDateFrom.Text.ToString()) : DateTime.Now;
            DateTime dtTo = IsDateTime(txtLeaveDateTo.Text.ToString()) == true ? Convert.ToDateTime(txtLeaveDateTo.Text.ToString()) : DateTime.Now;
            if (txtAvailableLeave.Text.ToString().Trim() != "" &&  IsDateTime(txtLeaveDateFrom.Text.ToString()) && IsDateTime(txtLeaveDateTo.Text.ToString()))
            {
                if (cmbDuration.SelectedIndex == 0)
                {
                    txtActualLeaveDays.Text = ((dtTo - dtFrom).TotalDays + 1).ToString();
                    txtBalanceLeave.Text = (Convert.ToDecimal(txtAvailableLeave.Text.ToString()) - Convert.ToDecimal((dtTo - dtFrom).TotalDays + 1)).ToString();
                }
                else
                {
                    txtActualLeaveDays.Text = (((dtTo - dtFrom).TotalDays + 1) / 2).ToString();
                    txtBalanceLeave.Text = (Convert.ToDecimal(txtAvailableLeave.Text.ToString()) - Convert.ToDecimal((dtTo - dtFrom).TotalDays + 1) / 2).ToString();
                }
            }
        }

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

            lstLeaveTRList.Items.Clear();
            List<EmployeeLeaveTRList> objEmployeeLeaveTRList = objLeaveTRList.getEmployeeLeaveTRList(Convert.ToInt16(lblEmpID.Text));
            foreach (EmployeeLeaveTRList indEmployeeLeaveTRList in objEmployeeLeaveTRList)
            {
                string strLeaveStatus = "";
                if (indEmployeeLeaveTRList.LeaveApprovalComments == "Not yet Approved" || indEmployeeLeaveTRList.LeaveApprovalComments == "Not yet Rejected")
                {
                    strLeaveStatus = "Pending";
                }
                else if (indEmployeeLeaveTRList.LeaveApprovalComments.StartsWith("Approved"))
                {
                    strLeaveStatus = "Approved";
                    if (indEmployeeLeaveTRList.Canceled == true)
                        strLeaveStatus = indEmployeeLeaveTRList.LeaveApprovalComments;
                    else
                        strLeaveStatus = indEmployeeLeaveTRList.LeaveApprovalComments;
                }
                else if (indEmployeeLeaveTRList.LeaveRejectionComments.StartsWith("Rejected"))
                {
                    strLeaveStatus = "Rejected";
                    if (indEmployeeLeaveTRList.Canceled == true)
                        strLeaveStatus = indEmployeeLeaveTRList.LeaveRejectionComments;
                    else
                        strLeaveStatus = indEmployeeLeaveTRList.LeaveRejectionComments;
                }

                System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
                    indEmployeeLeaveTRList.LeaveTRID.ToString(),
                    indEmployeeLeaveTRList.LeaveTypeTitle != null ? indEmployeeLeaveTRList.LeaveTypeTitle.ToString() : "",
                    indEmployeeLeaveTRList.ActualLeaveDateFrom != null ? Convert.ToDateTime(indEmployeeLeaveTRList.ActualLeaveDateFrom.ToString()).ToString("dd-MMM-yyyy") : "",
                    indEmployeeLeaveTRList.ActualLeaveDateTo != null ? Convert.ToDateTime(indEmployeeLeaveTRList.ActualLeaveDateTo.ToString()).ToString("dd-MMM-yyyy") : "",
                    indEmployeeLeaveTRList.LeaveDuration != null ? indEmployeeLeaveTRList.LeaveDuration.ToString() : "0.00",
                    indEmployeeLeaveTRList.LeaveComments.ToString(),
                    indEmployeeLeaveTRList.LeaveMode = indEmployeeLeaveTRList.LeaveMode.ToString(),
                    indEmployeeLeaveTRList.LeaveStatus = strLeaveStatus.ToString(),
                    Convert.ToBoolean(indEmployeeLeaveTRList.Canceled) == true ? "Cancelled" : "",
                    Convert.ToBoolean(indEmployeeLeaveTRList.Canceled) == true ? indEmployeeLeaveTRList.CanceledDate.ToString() : ""
                });

                lstLeaveTRList.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem1 });
            }
            txtAvailableLeave.Text = objLeaveTRList.getBalanceLeave(Convert.ToInt16(lblEmpID.Text)).ToString();
        }

        private void cmbLeaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lblEmpID.Text.Trim() == "")
                return;

            lblSpecificLeaveBalance.Text = objLeaveTRList.getSpecificLeaveTypeBalance(Convert.ToInt16(lblLeaveMasID.Text), Convert.ToInt16(cmbLeaveType.SelectedIndex + 1)).ToString();
        }

        private void picViewLeaves_Click(object sender, EventArgs e)
        {
            if (lblEmpID.Text.Trim() == "")
                return;

            frmViewLeavesOutstanding frmViewLeavesOutstanding = new frmViewLeavesOutstanding(Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToInt16(lblLeaveMasID.Text.ToString()));
            frmViewLeavesOutstanding.ShowDialog(this);
        }

        private void frmLeavesApproval_KeyDown(object sender, KeyEventArgs e)
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

        private void btnRemoveDetails_Click_1(object sender, EventArgs e)
        {

        }
    }
}
