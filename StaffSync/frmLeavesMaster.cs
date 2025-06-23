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

namespace StaffSync
{
    public partial class frmLeavesMaster : Form
    {
        clsEmployeeMaster objEmployeeMaster = new clsEmployeeMaster();
        clsDepartment objDepartment = new clsDepartment();
        clsDesignation objDesignation = new clsDesignation();
        clsLeaveTypeMas objLeaveTypeInfo = new clsLeaveTypeMas();
        clsLeaveTRList objLeaveTRList = new clsLeaveTRList();
        clsImpageOperation objImpageOperation = new clsImpageOperation();
        clsDownload objDownload = new clsDownload();
        clsPhotoMas objPhotoMas = new clsPhotoMas();

        public frmLeavesMaster()
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
                if (lblCancelStatus.Text == "")
                {
                    if (cmbDuration.SelectedIndex == 0)
                    {
                        DateTime LeaveDate = Convert.ToDateTime(txtLeaveDateFrom.Text);
                        for (int iLeaveCounter = 1; iLeaveCounter <= Convert.ToInt16(txtActualLeaveDays.Text); iLeaveCounter++)
                        {
                            employeeLeaveTRID = objLeaveTRList.InsertLeaveTransaction(Convert.ToInt16(lblEmpID.Text.ToString()), cmbLeaveType.SelectedIndex + 1, DateTime.Now, txtLeaveNote.Text.Trim(), Convert.ToDateTime(LeaveDate.ToString("dd-MM-yyyy")), Convert.ToDateTime(LeaveDate.ToString("dd-MM-yyyy")), Convert.ToDecimal(1), DateTime.Now, "Not yet Approved", DateTime.Now, "Not yet Rejected", Convert.ToInt16(lblEmpID.Text.ToString()));
                            LeaveDate = Convert.ToDateTime(txtLeaveDateFrom.Text).AddDays(iLeaveCounter);
                        }
                    }
                    else
                    {
                        employeeLeaveTRID = objLeaveTRList.InsertLeaveTransaction(Convert.ToInt16(lblEmpID.Text.ToString()), cmbLeaveType.SelectedIndex + 1, DateTime.Now, txtLeaveNote.Text.Trim(), Convert.ToDateTime(txtLeaveDateFrom.Text), Convert.ToDateTime(txtLeaveDateTo.Text), Convert.ToDecimal(txtActualLeaveDays.Text), DateTime.Now, "Not yet Approved", DateTime.Now, "Not yet Rejected", Convert.ToInt16(lblEmpID.Text.ToString()));
                    }
                }
                else
                {
                    employeeLeaveTRID = objLeaveTRList.CancelLeaveTransaction(Convert.ToInt16(lblCancelStatus.Text.ToString()), Convert.ToInt16(lblEmpID.Text.ToString()), "");
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
            else if (lblActionMode.Text == "modify")
            {
                int employeeLeaveTRID = 0;

                if(lblCancelStatus.Text == "")
                {
                    if (cmbDuration.SelectedIndex == 0)
                    {
                        DateTime LeaveDate = Convert.ToDateTime(txtLeaveDateFrom.Text);
                        for (int iLeaveCounter = 1; iLeaveCounter <= Convert.ToInt16(txtActualLeaveDays.Text); iLeaveCounter++)
                        {
                            employeeLeaveTRID = objLeaveTRList.InsertLeaveTransaction(Convert.ToInt16(lblEmpID.Text.ToString()), cmbLeaveType.SelectedIndex + 1, DateTime.Now, txtLeaveNote.Text.Trim(), Convert.ToDateTime(LeaveDate.ToString("dd-MM-yyyy")), Convert.ToDateTime(LeaveDate.ToString("dd-MM-yyyy")), Convert.ToDecimal(1), DateTime.Now, "Not yet Approved", DateTime.Now, "Not yet Rejected", Convert.ToInt16(lblEmpID.Text.ToString()));
                            LeaveDate = Convert.ToDateTime(txtLeaveDateFrom.Text).AddDays(iLeaveCounter);
                        }
                    }
                    else
                    {
                        employeeLeaveTRID = objLeaveTRList.InsertLeaveTransaction(Convert.ToInt16(lblEmpID.Text.ToString()), cmbLeaveType.SelectedIndex + 1, DateTime.Now, txtLeaveNote.Text.Trim(), Convert.ToDateTime(txtLeaveDateFrom.Text), Convert.ToDateTime(txtLeaveDateTo.Text), Convert.ToDecimal(txtActualLeaveDays.Text), DateTime.Now, "Not yet Approved", DateTime.Now, "Not yet Rejected", Convert.ToInt16(lblEmpID.Text.ToString()));
                    }
                }
                else
                {
                    employeeLeaveTRID = objLeaveTRList.CancelLeaveTransaction(Convert.ToInt16(lblCancelStatus.Text.ToString()), Convert.ToInt16(lblEmpID.Text.ToString()), "");
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
            txtEmpCode.Text = "";
            txtEmployeeName.Text = "";
            picEmpPhoto.Image = null;

            lblCancelStatus.Text = "";

            txtAvailableLeave.Text = "";
            txtLeaveDateFrom.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtLeaveDateTo.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtActualLeaveDays.Text = "";
            txtBalanceLeave.Text = "";
            txtLeaveNote.Text = "";

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
            cmbLeaveType.Enabled = true;
            cmbDuration.Enabled = true;
            txtLeaveDateFrom.Enabled = true;
            txtLeaveDateTo.Enabled = true;
            txtLeaveNote.Enabled = true;
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
            txtLeaveNote.Enabled = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listEmployeeLeaveList");
            frmEmployeeList.ShowDialog();
        }

        public void SelectedEmployeeID(string SearchOptionSelectedForm, int selectedEmployeeID)
        {
            if (SearchOptionSelectedForm == "listEmployeeLeaveList")
            {
                lblEmpID.Text = selectedEmployeeID.ToString();
                EmployeeInfo objSelectedEmployeeInfo = objEmployeeMaster.GetSelectedEmployeeInfo(Convert.ToInt16(lblEmpID.Text));
                txtEmpCode.Text = objSelectedEmployeeInfo.EmpCode;
                txtEmployeeName.Text = objSelectedEmployeeInfo.EmpName;
                cmbDesignation.SelectedIndex = objSelectedEmployeeInfo.EmpDesignationID - 1;
                cmbDepartment.SelectedIndex = objSelectedEmployeeInfo.DepartmentID - 1;
                picEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(lblEmpID.Text)).EmpPhoto);

                txtAvailableLeave.Text = objLeaveTRList.getBalanceLeave(Convert.ToInt16(lblEmpID.Text)).ToString();

                LeaveCalculation();

                RefreshLeavesHistoryList();
            }
        }

        private void frmLeavesMaster_Load(object sender, EventArgs e)
        {
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
            txtLeaveDateTo.Text = txtLeaveDateFrom.Text.ToString();
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
                    indEmployeeLeaveTRList.LeaveStatus = strLeaveStatus.ToString()
                });

                lstLeaveTRList.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem1 });
            }
            txtAvailableLeave.Text = objLeaveTRList.getBalanceLeave(Convert.ToInt16(lblEmpID.Text)).ToString();
        }

        private void lstLeaveTRList_MouseUp(object sender, MouseEventArgs e)
        {
            if (lblActionMode.Text == "add")
            {
                if (lstLeaveTRList.SelectedItems[0].SubItems[4].Text.ToString() != "0" && lstLeaveTRList.SelectedItems[0].SubItems[6].Text.ToString() == "Pending") //"LeaveDuration"                    
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        tlbCancelLeave.Visible = true;
                    }
                }
                else
                    tlbCancelLeave.Visible = false;
            }
        }

        private void cmLeaveCancel_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            lblCancelStatus.Text = lstLeaveTRList.SelectedItems[0].SubItems[0].Text.ToString();
        }
    }
}
