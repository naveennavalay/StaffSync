using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Excel;
using ModelStaffSync;
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
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StaffSync
{
    public partial class frmLeaveStatements : Form
    {
        DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster();
        DALStaffSync.clsDepartment objDepartment = new DALStaffSync.clsDepartment();
        DALStaffSync.clsDesignation objDesignation = new DALStaffSync.clsDesignation();
        DALStaffSync.clsLeaveTypeMas objLeaveTypeInfo = new DALStaffSync.clsLeaveTypeMas();
        DALStaffSync.clsLeaveTRList objLeaveTRList = new DALStaffSync.clsLeaveTRList();
        clsImpageOperation objImpageOperation = new clsImpageOperation();
        //Download objDownload = new Download();
        DALStaffSync.clsPhotoMas objPhotoMas = new DALStaffSync.clsPhotoMas();
        frmDashboard objDashboard = (frmDashboard) System.Windows.Forms.Application.OpenForms["frmDashboard"];

        public frmLeaveStatements()
        {
            InitializeComponent();
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
            RefreshDataOnGrid(-1);
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

            cmbLeaveType.DataSource = objLeaveTypeInfo.GetLeaveTypeList();
            cmbLeaveType.DisplayMember = "LeaveTypeTitle";
            cmbLeaveType.ValueMember = "LeaveTypeID";
            cmbLeaveType.SelectedIndex = 0;

            LeaveStatusList();
        }

        private void btnRemoveDetails_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

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

            txtFromDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");

            cmbDesignation.DataSource = null;
            cmbDepartment.DataSource = null;
            cmbLeaveType.DataSource = null;
        }

        public void enableControls()
        {
            txtEmpCode.Enabled = false;
            txtEmployeeName.Enabled = false;
            cmbDesignation.Enabled = false;
            cmbDepartment.Enabled = false;
            txtFromDate.Enabled = true;
            txtToDate.Enabled = true;
            txtFromDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            cmbDesignation.Enabled = false;
            cmbLeaveType.Enabled = true;
            cmbLeaveStatus.Enabled = true;
        }

        public void disableControls()
        {
            txtEmpCode.Enabled = false;
            txtEmployeeName.Enabled = false;
            cmbDesignation.Enabled = false;
            cmbDepartment.Enabled = false;
            txtFromDate.Enabled = false;
            txtToDate.Enabled = false;
            cmbLeaveType.Enabled = false;
            cmbLeaveStatus.Enabled = false;
        }

        private void LeaveStatusList()
        {
            cmbLeaveStatus.Items.Clear();
            cmbLeaveStatus.Items.Add("All");
            cmbLeaveStatus.Items.Add("Approved");
            cmbLeaveStatus.Items.Add("Rejected");
            cmbLeaveStatus.Items.Add("Pending");
            cmbLeaveStatus.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listLeaveStatements");
            frmEmployeeList.ShowDialog();
        }

        public void SelectedEmployeeID(string SearchOptionSelectedForm, int selectedEmployeeID)
        {
            if (SearchOptionSelectedForm == "listLeaveStatements")
            {
                lblEmpID.Text = selectedEmployeeID.ToString();
                EmployeeInfo objSelectedEmployeeInfo = objEmployeeMaster.GetSelectedEmployeeInfo(Convert.ToInt16(lblEmpID.Text));
                txtEmpCode.Text = objSelectedEmployeeInfo.EmpCode;
                txtEmployeeName.Text = objSelectedEmployeeInfo.EmpName;
                cmbDesignation.SelectedIndex = objSelectedEmployeeInfo.EmpDesignationID - 1;
                cmbDepartment.SelectedIndex = objSelectedEmployeeInfo.DepartmentID - 1;
                picEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(lblEmpID.Text)).EmpPhoto);

                RefreshDataOnGrid(Convert.ToInt16(lblEmpID.Text));

                //RefreshLeavesHistoryList();
            }
        }

        private void frmLeaveStatements_Load(object sender, EventArgs e)
        {
            RefreshDataOnGrid(-1);

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

            //dtgLeaveStatement.DataSource = null;
            //if (lblActionMode.Text == "add")
            //    dtgLeaveStatement.DataSource = objEmpLeaveEntitlementInfo.getDefaultLeaveEntitilementList();
            //else if (lblActionMode.Text == "modify")
            //    dtgLeaveStatement.DataSource = objEmpLeaveEntitlementInfo.getEmployeeLeaveEntitilementList(Convert.ToInt16(lblEmpID.Text), Convert.ToInt16(lblLeaveMasID.Text));

            //dtgLeaveStatement.Columns["LeaveEntmtID"].Visible = false;
            //dtgLeaveStatement.Columns["EmpID"].Visible = false;
            //dtgLeaveStatement.Columns["EmpID"].ReadOnly = true;
            //dtgLeaveStatement.Columns["LeaveMasID"].Visible = false;

            //List<EmployeeLeaveTRList> objEmployeeLeaveTRList = objLeaveTRList.getEmployeeLeaveTRList(Convert.ToInt16(lblEmpID.Text));
            //foreach (EmployeeLeaveTRList indEmployeeLeaveTRList in objEmployeeLeaveTRList)
            //{
            //    string strLeaveStatus = "";
            //    if (indEmployeeLeaveTRList.LeaveApprovalComments == "Not yet Approved" || indEmployeeLeaveTRList.LeaveApprovalComments == "Not yet Rejected")
            //    {
            //        strLeaveStatus = "Pending";
            //    }
            //    else if (indEmployeeLeaveTRList.LeaveApprovalComments.StartsWith("Approved"))
            //    {
            //        strLeaveStatus = "Approved";
            //        if (indEmployeeLeaveTRList.Canceled == true)
            //            strLeaveStatus = indEmployeeLeaveTRList.LeaveApprovalComments;
            //        else
            //            strLeaveStatus = indEmployeeLeaveTRList.LeaveApprovalComments;
            //    }
            //    else if (indEmployeeLeaveTRList.LeaveRejectionComments.StartsWith("Rejected"))
            //    {
            //        strLeaveStatus = "Rejected";
            //        if (indEmployeeLeaveTRList.Canceled == true)
            //            strLeaveStatus = indEmployeeLeaveTRList.LeaveRejectionComments;
            //        else
            //            strLeaveStatus = indEmployeeLeaveTRList.LeaveRejectionComments;
            //    }

            //    System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            //        indEmployeeLeaveTRList.LeaveTRID.ToString(),
            //        indEmployeeLeaveTRList.LeaveTypeTitle != null ? indEmployeeLeaveTRList.LeaveTypeTitle.ToString() : "",
            //        indEmployeeLeaveTRList.ActualLeaveDateFrom != null ? Convert.ToDateTime(indEmployeeLeaveTRList.ActualLeaveDateFrom.ToString()).ToString("dd-MMM-yyyy") : "",
            //        indEmployeeLeaveTRList.ActualLeaveDateTo != null ? Convert.ToDateTime(indEmployeeLeaveTRList.ActualLeaveDateTo.ToString()).ToString("dd-MMM-yyyy") : "",
            //        indEmployeeLeaveTRList.LeaveDuration != null ? indEmployeeLeaveTRList.LeaveDuration.ToString() : "0.00",
            //        indEmployeeLeaveTRList.LeaveComments.ToString(),
            //        indEmployeeLeaveTRList.LeaveStatus = strLeaveStatus.ToString()
            //    });

            //    lstLeaveTRList.Items.AddRange(new System.Windows.Forms.ListViewItem[] { listViewItem1 });
            //}
        }

        private void lstLeaveTRList_MouseUp(object sender, MouseEventArgs e)
        {
            //if (lblActionMode.Text == "add")
            //{
            //    if (lstLeaveTRList.SelectedItems[0].SubItems[4].Text.ToString() != "0" && lstLeaveTRList.SelectedItems[0].SubItems[6].Text.ToString() == "Pending") //"LeaveDuration"                    
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
            //lblCancelStatus.Text = lstLeaveTRList.SelectedItems[0].SubItems[0].Text.ToString();
        }

        private void picDownloadLeaveTRList_Click(object sender, EventArgs e)
        {
            var employeeInfo = new Dictionary<string, string>
            {
                { "Employee Code", CurrentUser.EmpCode },
                { "Employee Name", CurrentUser.EmpName },
                { "Department", CurrentUser.DepartmentTitle },
                { "Designation", CurrentUser.DesignationTitle },
                { "Date Of Joining", CurrentUser.DOJ.ToString("dd-MMM-yyyy") }
            };

            // Create a 4-column table (Title: Value | Title: Value)
            PdfPTable empInfoTable = new PdfPTable(4);
            empInfoTable.WidthPercentage = 100;
            empInfoTable.SpacingAfter = 10f;
            empInfoTable.SetWidths(new float[] { 1.5f, 2.5f, 1.5f, 2.5f });

            iTextSharp.text.Font labelFont = FontFactory.GetFont(FontFactory.TIMES, 10);
            iTextSharp.text.Font valueFont = FontFactory.GetFont(FontFactory.TIMES, 10);

            // Convert dictionary to pairs and add cells
            var keys = new List<string>(employeeInfo.Keys);
            for (int i = 0; i < keys.Count;)
            {
                // First pair
                empInfoTable.AddCell(new Phrase((keys[i]), labelFont));
                empInfoTable.AddCell(new Phrase(employeeInfo[keys[i]], valueFont));
                i++;

                // Second pair (if exists)
                if (i < keys.Count)
                {
                    empInfoTable.AddCell(new Phrase(keys[i], labelFont));
                    empInfoTable.AddCell(new Phrase(employeeInfo[keys[i]], valueFont));
                    i++;
                }
                else
                {
                    // Add two empty cells if odd number of items
                    empInfoTable.AddCell(new Phrase(""));
                    empInfoTable.AddCell(new Phrase(""));
                }
            }

            var leaveTable = new TableData
            {
                Title = "Leave Statements",
                Columns = new List<string>
                {
                    "Leave Type", "Leave From", "Leave To", "Leave Duration", "Comments", "Leave Status"
                }
            };

            //var columnIndexMap = new Dictionary<string, int>();
            //foreach (ColumnHeader header in lstLeaveTRList.Columns)
            //{
            //    if (leaveTable.Columns.Contains(header.Text))
            //    {
            //        columnIndexMap[header.Text] = header.Index;
            //    }
            //}

            //for (int i = 0; i < lstLeaveTRList.Items.Count; i++)
            //{
            //    var row = new Dictionary<string, object>();

            //    foreach (var column in leaveTable.Columns)
            //    {
            //        if (columnIndexMap.TryGetValue(column, out int index))
            //        {
            //            string value = lstLeaveTRList.Items[i].SubItems.Count > index ? lstLeaveTRList.Items[i].SubItems[index].Text : "";
            //            row[column] = value;
            //        }
            //        else
            //        {
            //            row[column] = "";
            //        }
            //    }
            //    leaveTable.Rows.Add(row);
            //}

            string filePath = AppVariables.TempFolderPath + @"\Employee Leave Summary.pdf";
            var generator = new PDFTableGen(filePath, "Employee Leave Summary");
            generator.SetCompanyInfo(
                companyName: AppVariables.CompanyName,
                address: AppVariables.CompanyAddress,
                phone: AppVariables.CompanyPhone,
                email: AppVariables.CompanyEmail,
                logoPath: AppVariables.CompanyCode + ".jpg"
            ); 
            generator.SetTopInfo(employeeInfo);
            generator.CreatePdf(new List<TableData> { leaveTable });

            Download.DownloadPDF(filePath);
        }

        private void cmbLeaveStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lblEmpID.Text.Trim() == "")
                return;

            RefreshDataOnGrid(Convert.ToInt16(lblEmpID.Text));
        }

        private void RefreshDataOnGrid(int EmpID)
        {
            dtgLeaveStatement.DataSource = null;
            dtgLeaveStatement.DataSource = objLeaveTRList.getAllEmployeesLeaveStatement(EmpID);
            dtgLeaveStatement.Columns["LeaveTRID"].Visible = false;
            dtgLeaveStatement.Columns["EmpID"].Visible = false;
            dtgLeaveStatement.Columns["EmpCode"].Visible = false;
            dtgLeaveStatement.Columns["EmpCode"].Width = 100;
            dtgLeaveStatement.Columns["EmpName"].Visible = false;
            dtgLeaveStatement.Columns["EmpName"].Width = 250;
            dtgLeaveStatement.Columns["DesignationTitle"].Visible = false;
            dtgLeaveStatement.Columns["DesignationTitle"].Width = 200;
            dtgLeaveStatement.Columns["DepartmentTitle"].Visible = false;
            dtgLeaveStatement.Columns["DepartmentTitle"].Width = 200;
            dtgLeaveStatement.Columns["AttDate"].Width = 100;
            dtgLeaveStatement.Columns["AttDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgLeaveStatement.Columns["AttStatus"].Width = 150;
            dtgLeaveStatement.Columns["LeaveTypeID"].Visible = false;
            dtgLeaveStatement.Columns["LeaveTypeID"].Width = 150;
            dtgLeaveStatement.Columns["LeaveTypeTitle"].Width = 150;
            dtgLeaveStatement.Columns["ActualLeaveDateFrom"].Width = 100;
            dtgLeaveStatement.Columns["ActualLeaveDateFrom"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgLeaveStatement.Columns["ActualLeaveDateTo"].Width = 100;
            dtgLeaveStatement.Columns["ActualLeaveDateTo"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgLeaveStatement.Columns["LeaveDuration"].Width = 100;
            dtgLeaveStatement.Columns["LeaveDuration"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgLeaveStatement.Columns["LeaveDuration"].DefaultCellStyle.Format = "c2";
            dtgLeaveStatement.Columns["LeaveComments"].Width = 250;
            dtgLeaveStatement.Columns["LeaveApprovalComments"].Width = 250;
            dtgLeaveStatement.Columns["LeaveRejectionComments"].Width = 250;
            dtgLeaveStatement.Columns["OrderID"].Visible = false; 
            dtgLeaveStatement.Columns["OrderID"].Width = 150;
            dtgLeaveStatement.Columns["LeaveApprovedDate"].Visible = false;
            dtgLeaveStatement.Columns["LeaveAppliedDate"].Visible = false;
            dtgLeaveStatement.Columns["LeaveRejectedDate"].Visible = false;
            dtgLeaveStatement.Columns["LeaveStatus"].Visible = false; 
            dtgLeaveStatement.Columns["ApprovedOrRejectedByEmpID"].Visible = false;

            foreach (DataGridViewRow dc in dtgLeaveStatement.Rows)
            {
                if (dc.Cells["LeaveStatus"].Value.ToString().ToLower() == "cancelled")
                {
                    dc.DefaultCellStyle.BackColor = Color.LightGray;
                }
            }
        }

        private void frmLeaveStatements_KeyDown(object sender, KeyEventArgs e)
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
