using Microsoft.Office.Interop.Excel;
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
using static C1.Util.Win.Win32;

namespace StaffSync
{
    public partial class frmLeavesReject : Form
    {
        clsEmployeeMaster objEmployeeMaster = new clsEmployeeMaster();
        clsDepartment objDepartment = new clsDepartment();
        clsDesignation objDesignation = new clsDesignation();
        clsLeaveTypeMas objLeaveTypeInfo = new clsLeaveTypeMas();
        clsLeaveTRList objLeaveTRList = new clsLeaveTRList();
        clsImpageOperation objImpageOperation = new clsImpageOperation();
        //Download objDownload = new Download();
        clsPhotoMas objPhotoMas = new clsPhotoMas();
        clsAttendanceMas objAttendanceInfo = new clsAttendanceMas();

        public frmLeavesReject()
        {
            InitializeComponent();
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
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
            this.Cursor = Cursors.WaitCursor;

            if (lblActionMode.Text == "add")
            {
                int employeeLeaveTRID = 0;
                if (Convert.ToDecimal(txtActualLeaveDays.Text) > 0)
                {
                    for (int iLeaveCounter = 1; iLeaveCounter <= 1; iLeaveCounter++)
                    {
                        employeeLeaveTRID = objLeaveTRList.RejectLeave(Convert.ToInt16(lblLeaveTRID.Text.ToString()), Convert.ToInt16(lblEmpID.Text.ToString()), txtApprovalNote.Text, CurrentLoggedInUserInfo.UserID);
                        objAttendanceInfo.InsertDailyAttendance(Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToDateTime(txtLeaveDateFrom.Text.ToString()), "Present", Convert.ToInt16(lblLeaveTRID.Text.ToString()));
                    }
                }
                else if (Convert.ToDecimal(txtActualLeaveDays.Text) < 0)
                {
                    txtBalanceLeave.Text = (Convert.ToDecimal(txtBalanceLeave.Text.ToString()) + Convert.ToDecimal(txtActualLeaveDays.Text.ToString())).ToString();
                    employeeLeaveTRID = objLeaveTRList.ApproveLeave(Convert.ToInt16(lblLeaveTRID.Text.ToString()), Convert.ToInt16(lblEmpID.Text.ToString()), txtApprovalNote.Text, CurrentLoggedInUserInfo.UserID);
                    objLeaveTRList.UpdateEmployeeLeaveBalance(Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToDecimal(txtAvailableLeave.Text), Convert.ToDecimal(txtBalanceLeave.Text));
                    objAttendanceInfo.InsertDailyAttendance(Convert.ToInt16(lblEmpID.Text.ToString()), Convert.ToDateTime(txtLeaveDateFrom.Text.ToString()), "Leave", Convert.ToInt16(lblLeaveTRID.Text.ToString()));
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
            //            employeeLeaveTRID = objLeaveTRList.RejectLeave(Convert.ToInt16(lblLeaveTRID.Text.ToString()), Convert.ToInt16(lblEmpID.Text.ToString()), txtApprovalNote.Text, CurrentLoggedInUserInfo.UserID);
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
            frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listEmployeeLeaveRejectRequestList");
            frmEmployeeList.ShowDialog();
        }

        public void SelectedEmployeeID(string SearchOptionSelectedForm, int selectedEmployeeID, int selectedLeaveID)
        {
            if (SearchOptionSelectedForm == "listEmployeeLeaveRejectRequestList")
            {
                lblEmpID.Text = selectedEmployeeID.ToString();
                EmployeeInfo objSelectedEmployeeInfo = objEmployeeMaster.GetSelectedEmployeeInfo(Convert.ToInt16(lblEmpID.Text));
                txtEmpCode.Text = objSelectedEmployeeInfo.EmpCode;
                txtEmployeeName.Text = objSelectedEmployeeInfo.EmpName;
                cmbDesignation.SelectedIndex = objSelectedEmployeeInfo.EmpDesignationID - 1;
                cmbDepartment.SelectedIndex = objSelectedEmployeeInfo.DepartmentID - 1;
                picEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(lblEmpID.Text)).EmpPhoto);

                txtAvailableLeave.Text = objLeaveTRList.getBalanceLeave(Convert.ToInt16(lblEmpID.Text)).ToString();
                List<EmployeeSpecificLeaveInfo> objEmployeeSpecificLeaveInfo = objLeaveTRList.getSpecificEmployeeSpecificLeaveInfo(selectedLeaveID);
                if (objEmployeeSpecificLeaveInfo.Count > 0)
                {
                    lblLeaveTRID.Text = selectedLeaveID.ToString();
                    cmbLeaveType.SelectedIndex = objEmployeeSpecificLeaveInfo[0].LeaveTypeID;
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
                    foreach (var indRow in lstLeaveTRList.Items)
                    {
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

        private void frmLeavesReject_Load(object sender, EventArgs e)
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
            MessageBox.Show(lstLeaveTRList.SelectedItems[0].SubItems[0].Text.ToString());

            DateTime dtLeaveAppliedDate = Convert.ToDateTime(lstLeaveTRList.SelectedItems[0].SubItems[2].Text.ToString());
            MessageBox.Show(dtLeaveAppliedDate.ToString("dd-MM-yyyy"));
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
                System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
                    indEmployeeLeaveTRList.LeaveTRID.ToString(),
                    indEmployeeLeaveTRList.LeaveTypeTitle != null ? indEmployeeLeaveTRList.LeaveTypeTitle.ToString() : "",
                    indEmployeeLeaveTRList.ActualLeaveDateFrom != null ? Convert.ToDateTime(indEmployeeLeaveTRList.ActualLeaveDateFrom.ToString()).ToString("dd-MMM-yyyy") : "",
                    indEmployeeLeaveTRList.ActualLeaveDateTo != null ? Convert.ToDateTime(indEmployeeLeaveTRList.ActualLeaveDateTo.ToString()).ToString("dd-MMM-yyyy") : "",
                    indEmployeeLeaveTRList.LeaveDuration != null ? indEmployeeLeaveTRList.LeaveDuration.ToString() : "0.00",
                    indEmployeeLeaveTRList.LeaveComments.ToString()
                });
                lstLeaveTRList.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem1 });
            }
        }

    }
}
