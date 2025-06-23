using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public partial class frmPayrollMaster : Form
    {
        clsEmployeeMaster objEmployeeMaster = new clsEmployeeMaster();
        clsImpageOperation objImpageOperation = new clsImpageOperation();
        clsPhotoMas objPhotoMas = new clsPhotoMas();
        clsSalaryProfile objSalaryProfile = new clsSalaryProfile();
        clsEmpPayroll objEmployeePayroll = new clsEmpPayroll();

        public frmPayrollMaster()
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

        private void btnCloseMe_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            int empSalaryID = 0;
            decimal AllowanceAmount = 0;
            decimal DeductionAmount = 0;
            decimal ReimbursmentAmount = 0;
            int iRowCounter = 1;

            if (lblActionMode.Text == "add")
            {
                empSalaryID = objEmployeePayroll.InsertEmployeeSalaryMasterInfo(Convert.ToInt16(lblReportingManagerID.Text.Trim()), Convert.ToDateTime(txtSalaryDate.Text), cmbSalaryMonth.Text, Convert.ToDecimal(txtTotalWorkingDays.Text), Convert.ToDecimal(txtTotalWorkedDays.Text), Convert.ToDecimal(txtLeaveDays.Text), Convert.ToDecimal(txtNetPayable.Text), 0);
                foreach (DataGridViewRow dc in dtgSalaryDetails.Rows)
                {
                    int EmpSalDetID = objEmployeePayroll.InsertEmployeeSalaryDetailsInfo(Convert.ToInt16(empSalaryID), Convert.ToInt16(dc.Cells["SalProDetID"].Value.ToString()), dc.Cells["HeaderTitle"].Value.ToString(), dc.Cells["SalHeaderType"].Value.ToString(), Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()), Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString()), Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString()), iRowCounter);
                    iRowCounter = iRowCounter + 1;
                }
                if (empSalaryID > 0)
                    MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (lblActionMode.Text == "modify")
            {
                empSalaryID = objEmployeePayroll.UpdateEmployeeSalaryMasterInfo(Convert.ToInt16(lblSelectedMonthSalaryID.Text.Trim()), Convert.ToInt16(lblReportingManagerID.Text.Trim()), Convert.ToDateTime(txtSalaryDate.Text), cmbSalaryMonth.Text, Convert.ToDouble(txtTotalWorkingDays.Text), Convert.ToDouble(txtTotalWorkedDays.Text), Convert.ToDouble(txtLeaveDays.Text), Convert.ToDouble(txtNetPayable.Text));
                foreach (DataGridViewRow dc in dtgSalaryDetails.Rows)
                {
                    int EmpSalDetID = objEmployeePayroll.UpdateEmployeeSalaryDetailsInfo(Convert.ToInt16(dc.Cells["EmpSalDetID"].Value.ToString()), Convert.ToInt16(dc.Cells["EmpSalID"].Value.ToString()), Convert.ToInt16(dc.Cells["SalProDetID"].Value.ToString()), dc.Cells["SalHeaderTitle"].Value.ToString(), dc.Cells["SalHeaderType"].Value.ToString(), Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()), Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString()), Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString()), Convert.ToInt16(dc.Cells["OrderID"].Value.ToString()));
                    iRowCounter = iRowCounter + 1;
                }
                if (empSalaryID > 0)
                    MessageBox.Show("Details updated successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            onSaveButtonClick();
            disableControls();
            clearControls();
            errValidator.Clear();
            this.Cursor = Cursors.Default;
        }

        private void btnGenerateDetails_Click(object sender, EventArgs e)
        {
            lblActionMode.Text = "add";
            onGenerateButtonClick();
            clearControls();
            enableControls();
            errValidator.Clear();
        }



        public void onGenerateButtonClick()
        {
            lblActionMode.Text = "add";
            lblReportingManagerID.Text = "";
            lblSelectedMonthSalaryID.Text = "";
            btnReportingManagerSearch.Enabled = true;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = false;
            btnCancel.Enabled = true;
            LoadSalaryMonthList();
        }

        public void onModifyButtonClick()
        {
            lblActionMode.Text = "modify";
            lblReportingManagerID.Text = "";
            lblSelectedMonthSalaryID.Text = "";
            btnReportingManagerSearch.Enabled = true;
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = false;
            btnCancel.Enabled = true;
            LoadSalaryMonthList();
        }

        public void onRemoveButtonClick()
        {
            lblActionMode.Text = "remove";
            lblReportingManagerID.Text = "";
            lblSelectedMonthSalaryID.Text = "";
            btnReportingManagerSearch.Enabled = true;
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
            LoadSalaryMonthList();
        }

        public void onSaveButtonClick()
        {
            lblActionMode.Text = "";
            lblReportingManagerID.Text = "";
            lblSelectedMonthSalaryID.Text = "";
            btnReportingManagerSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
            LoadSalaryMonthList();
        }

        public void onCancelButtonClick()
        {
            lblActionMode.Text = "";
            lblReportingManagerID.Text = "";
            lblSelectedMonthSalaryID.Text = "";
            btnReportingManagerSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
            LoadSalaryMonthList();
        }

        public void clearControls()
        {
            lblReportingManagerID.Text = "";
            txtRepEmpCode.Text = "";
            txtRepEmpName.Text = "";
            txtRepEmpDesig.Text = "";
            txtRepEmpDepartment.Text = "";
            txtAallowences.Text = "0.00";
            txtDeductions.Text = "0.00";
            txtReimbursement.Text = "0.00";
            picRepEmpPhoto.Image = null;
            lblSelectedMonthSalaryID.Text = "";

            txtTotalWorkingDays.Text = "0";
            txtTotalWorkedDays.Text = "0";
            txtLeaveDays.Text = "0";
            txtSalaryDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            txtNetPayable.Text = "0.00";
            cmbSalaryMonth.DataSource = null;

            dtgSalaryDetails.DataSource = null;
            LoadSalaryMonthList();

            dtgSalaryDetails.DataSource = objSalaryProfile.GetDefaultSalaryProfileInfo(1);

            dtgSalaryDetails.Columns["EmpSalDetID"].Visible = false;
            dtgSalaryDetails.Columns["EmpSalDetID"].ReadOnly = true;
            dtgSalaryDetails.Columns["SalProDetID"].ReadOnly = true;
            dtgSalaryDetails.Columns["SalProDetID"].Visible = false;
            dtgSalaryDetails.Columns["SalProDetID"].ReadOnly = true;
            dtgSalaryDetails.Columns["SalProfileID"].Visible = false;
            dtgSalaryDetails.Columns["SalProfileID"].ReadOnly = true;
            dtgSalaryDetails.Columns["HeaderID"].Visible = false;
            dtgSalaryDetails.Columns["HeaderID"].ReadOnly = true;
            dtgSalaryDetails.Columns["HeaderTitle"].Width = 300;
            dtgSalaryDetails.Columns["HeaderTitle"].ReadOnly = true;
            dtgSalaryDetails.Columns["SalHeaderType"].Visible = false;
            dtgSalaryDetails.Columns["AllowanceAmount"].Width = 150;
            dtgSalaryDetails.Columns["AllowanceAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgSalaryDetails.Columns["AllowanceAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryDetails.Columns["DeductionAmount"].Width = 150;
            dtgSalaryDetails.Columns["ReimbursmentAmount"].Width = 150;
            dtgSalaryDetails.Columns["DeductionAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Deductions
            dtgSalaryDetails.Columns["DeductionAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Reimbursments
            dtgSalaryDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryDetails.Columns["OrderID"].Visible = false;
            dtgSalaryDetails.Enabled = false;
        }

        public void enableControls()
        {
            txtRepEmpCode.Enabled = false;
            txtRepEmpName.Enabled = false;
            txtRepEmpDesig.Enabled = false;
            txtRepEmpDepartment.Enabled = false;
            cmbSalaryMonth.Enabled = true;
            txtSalaryDate.Enabled = true;
            txtTotalWorkingDays.Enabled = true;
            txtTotalWorkedDays.Enabled = true;
            txtLeaveDays.Enabled = true;
            txtAallowences.Enabled = false;
            txtDeductions.Enabled = false;
            txtReimbursement.Enabled = false;
            txtNetPayable.Enabled = false;
        }

        public void disableControls()
        {
            txtRepEmpCode.Enabled = false;
            txtRepEmpName.Enabled = false;
            txtRepEmpDesig.Enabled = false;
            txtRepEmpDepartment.Enabled = false;
            cmbSalaryMonth.Enabled = false;
            txtSalaryDate.Enabled = false;
            txtTotalWorkingDays.Enabled = false;
            txtTotalWorkedDays.Enabled = false;
            txtLeaveDays.Enabled = false;
            txtAallowences.Enabled = false;
            txtDeductions.Enabled = false;
            txtReimbursement.Enabled = false; txtNetPayable.Enabled = false;
        }

        public void LoadSalaryMonthList()
        {
            cmbSalaryMonth.Items.Clear();
            cmbSalaryMonth.Items.Add("Jan - " + DateTime.Now.Year.ToString());
            cmbSalaryMonth.Items.Add("Feb - " + DateTime.Now.Year.ToString());
            cmbSalaryMonth.Items.Add("Mar - " + DateTime.Now.Year.ToString());
            cmbSalaryMonth.Items.Add("Apr - " + DateTime.Now.Year.ToString());
            cmbSalaryMonth.Items.Add("May - " + DateTime.Now.Year.ToString());
            cmbSalaryMonth.Items.Add("Jun - " + DateTime.Now.Year.ToString());
            cmbSalaryMonth.Items.Add("Jul - " + DateTime.Now.Year.ToString());
            cmbSalaryMonth.Items.Add("Aug - " + DateTime.Now.Year.ToString());
            cmbSalaryMonth.Items.Add("Sep - " + DateTime.Now.Year.ToString());
            cmbSalaryMonth.Items.Add("Oct - " + DateTime.Now.Year.ToString());
            cmbSalaryMonth.Items.Add("Nov - " + DateTime.Now.Year.ToString());
            cmbSalaryMonth.Items.Add("Dec - " + DateTime.Now.Year.ToString());
            cmbSalaryMonth.SelectedIndex = 0;
        }

        private void frmPayrollMaster_Load(object sender, EventArgs e)
        {
            clearControls();
            disableControls();
            onCancelButtonClick();
        }

        private void btnReportingManagerSearch_Click(object sender, EventArgs e)
        {
            if (lblActionMode.Text == "add")
            {
                frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listPayrollUsersList");
                frmEmployeeList.ShowDialog();
            }
            else if (lblActionMode.Text == "modify")
            {
                frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listEmployeesPayslip");
                frmEmployeeList.ShowDialog();
            }
        }

        public void SelectedEmployeeID(string SearchOptionSelectedForm, int selectedEmployeeID, int selectedMonthSalaryID)
        {
            if (SearchOptionSelectedForm == "listPayrollUsersList")
            {
                lblReportingManagerID.Text = selectedEmployeeID.ToString();
                ReportingManagerInfo objReportingManagerInfo = objEmployeeMaster.GetReportingManagerInfo(Convert.ToInt16(selectedEmployeeID.ToString()));
                txtRepEmpCode.Text = objReportingManagerInfo.EmpCode;
                txtRepEmpName.Text = objReportingManagerInfo.EmpName;
                txtRepEmpDesig.Text = objReportingManagerInfo.DesignationTitle;
                txtRepEmpDepartment.Text = objReportingManagerInfo.DepartmentTitle;
                picRepEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(lblReportingManagerID.Text.ToString())).EmpPhoto);

                int SalaryProfileID = 0;
                
                SalaryProfileID = objSalaryProfile.getEmployeeSpecificSalaryProfile(Convert.ToInt16(lblReportingManagerID.Text.ToString())).SalProfileID;

                dtgSalaryDetails.Enabled = true;
                dtgSalaryDetails.DataSource = objSalaryProfile.GetDefaultSalaryProfileInfo(SalaryProfileID);
                dtgSalaryDetails.Columns["SalProDetID"].Visible = false;
                dtgSalaryDetails.Columns["SalProDetID"].ReadOnly = true;
                dtgSalaryDetails.Columns["SalProfileID"].Visible = false;
                dtgSalaryDetails.Columns["SalProfileID"].ReadOnly = true;
                dtgSalaryDetails.Columns["HeaderID"].Visible = false;
                dtgSalaryDetails.Columns["HeaderID"].ReadOnly = true;
                dtgSalaryDetails.Columns["HeaderTitle"].Width = 300;
                dtgSalaryDetails.Columns["HeaderTitle"].ReadOnly = true;
                dtgSalaryDetails.Columns["SalHeaderType"].Visible = false;
                dtgSalaryDetails.Columns["AllowanceAmount"].Width = 150;
                dtgSalaryDetails.Columns["AllowanceAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
                dtgSalaryDetails.Columns["AllowanceAmount"].DefaultCellStyle.Format = "c2";
                dtgSalaryDetails.Columns["DeductionAmount"].Width = 150;
                dtgSalaryDetails.Columns["ReimbursmentAmount"].Width = 150;
                dtgSalaryDetails.Columns["DeductionAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Deductions
                dtgSalaryDetails.Columns["DeductionAmount"].DefaultCellStyle.Format = "c2";
                dtgSalaryDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Reimbursments
                dtgSalaryDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Format = "c2";
                dtgSalaryDetails.Columns["OrderID"].Visible = false;

                foreach (DataGridViewRow dc in dtgSalaryDetails.Rows)
                {
                    if (dc.Cells["SalHeaderType"].Value.ToString().ToLower() == "allowences")
                    {
                        dc.Cells["AllowanceAmount"].ReadOnly = false;
                        dc.Cells["DeductionAmount"].ReadOnly = true;
                        dc.Cells["ReimbursmentAmount"].ReadOnly = true;
                    }
                    else if (dc.Cells["SalHeaderType"].Value.ToString().ToLower() == "deductions")
                    {
                        dc.Cells["AllowanceAmount"].ReadOnly = true;
                        dc.Cells["DeductionAmount"].ReadOnly = false;
                        dc.Cells["ReimbursmentAmount"].ReadOnly = true;
                    }
                    else if (dc.Cells["SalHeaderType"].Value.ToString().ToLower() == "reimbursement")
                    {
                        dc.Cells["AllowanceAmount"].ReadOnly = true;
                        dc.Cells["DeductionAmount"].ReadOnly = true;
                        dc.Cells["ReimbursmentAmount"].ReadOnly = false;
                    }
                }
            }
            else if (SearchOptionSelectedForm == "listEmployeesPayslip")
            {
                lblReportingManagerID.Text = selectedEmployeeID.ToString();
                ReportingManagerInfo objReportingManagerInfo = objEmployeeMaster.GetReportingManagerInfo(Convert.ToInt16(selectedEmployeeID.ToString()));
                txtRepEmpCode.Text = objReportingManagerInfo.EmpCode;
                txtRepEmpName.Text = objReportingManagerInfo.EmpName;
                txtRepEmpDesig.Text = objReportingManagerInfo.DesignationTitle;
                txtRepEmpDepartment.Text = objReportingManagerInfo.DepartmentTitle;
                picRepEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(lblReportingManagerID.Text.ToString())).EmpPhoto);

                lblSelectedMonthSalaryID.Text = selectedMonthSalaryID.ToString();


                List<EmployeePayslipMasterDetails> objSelectedEmployeeSalaryMasterDetails = objEmployeePayroll.getSelectedSpecificMonthSalaryMasterDetails(Convert.ToInt16(selectedEmployeeID.ToString()), selectedMonthSalaryID);
                if(objSelectedEmployeeSalaryMasterDetails != null)
                {
                    cmbSalaryMonth.Text = objSelectedEmployeeSalaryMasterDetails[0].EmpSalMonthYear.ToString();
                    txtSalaryDate.Text = objSelectedEmployeeSalaryMasterDetails[0].EmpSalDate.ToString("dd-MM-yyyy");
                    txtTotalWorkingDays.Text = objSelectedEmployeeSalaryMasterDetails[0].TotalDaysInMonth.ToString();
                    txtTotalWorkedDays.Text = objSelectedEmployeeSalaryMasterDetails[0].TotalDaysWorked.ToString();
                    txtLeaveDays.Text = objSelectedEmployeeSalaryMasterDetails[0].TotalDaysOnLeave.ToString();
                    txtNetPayable.Text = objSelectedEmployeeSalaryMasterDetails[0].NetPayable.ToString();
                    txtNetPayable.Text = Convert.ToDecimal(txtNetPayable.Text.ToString()).ToString("00.00", CultureInfo.InvariantCulture);
                }

                dtgSalaryDetails.DataSource = objEmployeePayroll.getSelectedSpecificMonthSalaryDetails(selectedMonthSalaryID);

                dtgSalaryDetails.Enabled = true;
                dtgSalaryDetails.Columns["EmpSalDetID"].Visible = false;
                dtgSalaryDetails.Columns["EmpSalDetID"].ReadOnly = true;
                dtgSalaryDetails.Columns["SalProDetID"].Visible = false;
                dtgSalaryDetails.Columns["SalProDetID"].ReadOnly = true;
                dtgSalaryDetails.Columns["EmpSalID"].Visible = false;
                dtgSalaryDetails.Columns["EmpSalID"].ReadOnly = true;
                dtgSalaryDetails.Columns["SalHeaderTitle"].ReadOnly = true;
                dtgSalaryDetails.Columns["SalHeaderTitle"].Width = 300;
                dtgSalaryDetails.Columns["SalHeaderType"].Visible = false;
                dtgSalaryDetails.Columns["AllowanceAmount"].Width = 150;
                dtgSalaryDetails.Columns["AllowanceAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
                dtgSalaryDetails.Columns["AllowanceAmount"].DefaultCellStyle.Format = "c2";
                dtgSalaryDetails.Columns["DeductionAmount"].Width = 150;
                dtgSalaryDetails.Columns["DeductionAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Deductions
                dtgSalaryDetails.Columns["DeductionAmount"].DefaultCellStyle.Format = "c2";
                dtgSalaryDetails.Columns["ReimbursmentAmount"].Width = 150;
                dtgSalaryDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Reimbursments
                dtgSalaryDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Format = "c2";
                dtgSalaryDetails.Columns["OrderID"].Visible = false;

                decimal totalAallowences = 0;
                decimal totalDeductions = 0;
                decimal totalReimbursement = 0;

                foreach (DataGridViewRow dc in dtgSalaryDetails.Rows)
                {
                    totalAallowences = totalAallowences + Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString());
                    totalDeductions = totalDeductions + Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString());
                    totalReimbursement = totalReimbursement + Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString());
                    if (dc.Cells["SalHeaderType"].Value.ToString().ToLower() == "allowences")
                    {
                        dc.Cells["AllowanceAmount"].ReadOnly = false;
                        dc.Cells["DeductionAmount"].ReadOnly = true;
                        dc.Cells["ReimbursmentAmount"].ReadOnly = true;
                    }
                    else if (dc.Cells["SalHeaderType"].Value.ToString().ToLower() == "deductions")
                    {
                        dc.Cells["AllowanceAmount"].ReadOnly = true;
                        dc.Cells["DeductionAmount"].ReadOnly = false;
                        dc.Cells["ReimbursmentAmount"].ReadOnly = true;
                    }
                    else if (dc.Cells["SalHeaderType"].Value.ToString().ToLower() == "reimbursement")
                    {
                        dc.Cells["AllowanceAmount"].ReadOnly = true;
                        dc.Cells["DeductionAmount"].ReadOnly = true;
                        dc.Cells["ReimbursmentAmount"].ReadOnly = false;
                    }
                }

                txtAallowences.Text = Convert.ToDecimal(totalAallowences.ToString()).ToString("00.00", CultureInfo.InvariantCulture);
                txtDeductions.Text = Convert.ToDecimal(totalDeductions.ToString()).ToString("00.00", CultureInfo.InvariantCulture);
                txtReimbursement.Text = Convert.ToDecimal(totalReimbursement.ToString()).ToString("00.00", CultureInfo.InvariantCulture);

                txtNetPayable.Text = Convert.ToDecimal((totalAallowences + totalReimbursement) - totalDeductions).ToString();
                txtNetPayable.Text = Convert.ToDecimal(txtNetPayable.Text.ToString()).ToString("00.00", CultureInfo.InvariantCulture);
            }
        }

        private void btnModifyDetails_Click(object sender, EventArgs e)
        {
            onModifyButtonClick();
            enableControls();
        }

        private void btnRemoveDetails_Click(object sender, EventArgs e)
        {
            if (lblActionMode.Text == "" || lblActionMode.Text == "remove")
            {
                lblActionMode.Text = "remove";
                onRemoveButtonClick();
                clearControls();
                //enableControls();
                errValidator.Clear();
            }
            else if (lblActionMode.Text == "delete")
            {
                if (MessageBox.Show("The selected record will be deleted. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int affectedRows = 1; // objEmployeeMaster.DeleteEmployeeMaster(Convert.ToInt16(lblReportingManagerID.Text.Trim()));
                    if (affectedRows > 0)
                        MessageBox.Show("Details deleted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                onCancelButtonClick();
                disableControls();
                clearControls();
                lblActionMode.Text = "";
                errValidator.Clear();
            }
        }

        private void dtgSalaryDetails_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dtgSalaryDetails.CurrentRow.Cells["SalHeaderType"].Value.ToString().ToLower() == "allowences")
            {
                dtgSalaryDetails.CurrentRow.Cells["AllowanceAmount"].ReadOnly = false;
                dtgSalaryDetails.CurrentRow.Cells["DeductionAmount"].ReadOnly = true;
                dtgSalaryDetails.CurrentRow.Cells["ReimbursmentAmount"].ReadOnly = true;
            }
            else if (dtgSalaryDetails.CurrentRow.Cells["SalHeaderType"].Value.ToString().ToLower() == "deductions")
            {
                dtgSalaryDetails.CurrentRow.Cells["AllowanceAmount"].ReadOnly = true;
                dtgSalaryDetails.CurrentRow.Cells["DeductionAmount"].ReadOnly = false;
                dtgSalaryDetails.CurrentRow.Cells["ReimbursmentAmount"].ReadOnly = true;
            }
            else if (dtgSalaryDetails.CurrentRow.Cells["SalHeaderType"].Value.ToString().ToLower() == "reimbursement")
            {
                dtgSalaryDetails.CurrentRow.Cells["AllowanceAmount"].ReadOnly = true;
                dtgSalaryDetails.CurrentRow.Cells["DeductionAmount"].ReadOnly = true;
                dtgSalaryDetails.CurrentRow.Cells["ReimbursmentAmount"].ReadOnly = false;
            }
        }

        private void dtgSalaryDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            decimal totalAallowences = 0;
            decimal totalDeductions = 0;
            decimal totalReimbursement = 0;

            foreach (DataGridViewRow dc in dtgSalaryDetails.Rows)
            {
                totalAallowences = totalAallowences + Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString());
                totalDeductions = totalDeductions + Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString());
                totalReimbursement = totalReimbursement + Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString());
            }

            txtAallowences.Text = Convert.ToDecimal(totalAallowences.ToString()).ToString("00.00", CultureInfo.InvariantCulture);
            txtDeductions.Text = Convert.ToDecimal(totalDeductions.ToString()).ToString("00.00", CultureInfo.InvariantCulture);
            txtReimbursement.Text = Convert.ToDecimal(totalReimbursement.ToString()).ToString("00.00", CultureInfo.InvariantCulture);

            txtNetPayable.Text = Convert.ToDecimal((totalAallowences + totalReimbursement) - totalDeductions).ToString();
            txtNetPayable.Text = Convert.ToDecimal(txtNetPayable.Text.ToString()).ToString("00.00", CultureInfo.InvariantCulture);
        }

        private void cmbSalaryMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTotalWorkingDays.Text = DateTime.DaysInMonth(DateTime.Now.Year, cmbSalaryMonth.SelectedIndex + 1).ToString();
        }
    }
}
