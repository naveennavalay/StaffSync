using DocumentFormat.OpenXml.Wordprocessing;
using ModelStaffSync;
using Quartz.Impl.AdoJobStore.Common;
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
using static Quartz.Logging.OperationName;
//using static C1.Util.Win.Win32;

namespace StaffSync
{
    public partial class frmPayrollBatchProcess : Form
    {
        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        clsImpageOperation objImpageOperation = new clsImpageOperation();
        DALStaffSync.clsPhotoMas objPhotoMas = new DALStaffSync.clsPhotoMas();
        DALStaffSync.clsSalaryProfile objSalaryProfile = new DALStaffSync.clsSalaryProfile();
        DALStaffSync.clsEmpPayroll objEmployeePayroll = new DALStaffSync.clsEmpPayroll();
        DALStaffSync.clsAllowenceInfo objAllowenceInfo = new DALStaffSync.clsAllowenceInfo();
        DALStaffSync.clsDeductionsInfo objDeductionsInfo = new DALStaffSync.clsDeductionsInfo();
        DALStaffSync.clsReimbursement objReimbursement = new DALStaffSync.clsReimbursement();

        frmDashboard objDashboard = (frmDashboard) System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();

        public frmPayrollBatchProcess()
        {
            InitializeComponent();
        }

        public frmPayrollBatchProcess(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmPayrollBatchProcess(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            onCancelButtonClick();
            clearControls();
            cmbSalaryMonth.Enabled = true;
            dtSalaryDate.Enabled = true;
            dtgSalaryDetails.Enabled = true;
            errValidator.Clear();
        }

        private void btnCloseMe_Click_1(object sender, EventArgs e)
        {
            //if (lblActionMode.Text != "")
            //{
            //    if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //    {
            //        return;
            //    }
            //}
            objDashboard.lblDashboardTitle.Text = "Dashboard";
            this.Close();
        }

        private bool ValidateValuesOnUI()
        {
            bool validationStatus = true;
            errValidator.Clear();

            // Helper for date validation
            DateTime dos;
            string dateFormat = "dd-MM-yyyy";
            CultureInfo provider = CultureInfo.InvariantCulture;

            if (string.IsNullOrEmpty(dtSalaryDate.Text.Trim()))
            {
                errValidator.SetError(dtSalaryDate, "Salary Date is required");
                MessageBox.Show("Salary Date is required", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                validationStatus = false;
                return validationStatus;
            }
            else if (Convert.ToDateTime(dtSalaryDate.Value.ToString("dd-MM-yyyy")).Date < Convert.ToDateTime("01-" + cmbSalaryMonth.SelectedItem.ToString().Replace(" - ", "-")).Date)
            {
                errValidator.SetError(dtSalaryDate, "Salary Date cannot be less than the selected month");
                MessageBox.Show("Salary Date cannot be less than the selected month", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                validationStatus = false;
                return validationStatus;
            }
            else if (Convert.ToDateTime(dtSalaryDate.Value.ToString("dd-MM-yyyy")) > DateTime.Now)
            {
                errValidator.SetError(dtSalaryDate, "Salary Date cannot be in the future");
                MessageBox.Show("Salary Date cannot be in the future", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                validationStatus = false;
                return validationStatus;
            }

            int intSalaryProcessedID = objSalaryProfile.IsSalaryAlreadyProcessed(cmbSalaryMonth.SelectedItem.ToString());
            if (intSalaryProcessedID > 0)
            {
                errValidator.SetError(cmbSalaryMonth, "Salary for the selected month is already processed");
                MessageBox.Show("Salary for the selected month is already processed", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                validationStatus = false;
                return validationStatus;
            }

            if (MessageBox.Show("You are about to continue to process the Salary. \nPlease confirm once again to proceed.?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                validationStatus = false;
                return validationStatus;
            }

            return validationStatus;
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

            if (!ValidateValuesOnUI())
            {
                this.Cursor = Cursors.Default;
                return;
            }

            int empSalaryID = 0;
            decimal AllowanceAmount = 0;
            decimal DeductionAmount = 0;
            decimal ReimbursmentAmount = 0;
            int iRowCounter = 1;

            objTempCurrentlyLoggedInUserInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objTempCurrentlyLoggedInUserInfo.EmpID.ToString()));

            onSaveButtonClick();
            disableControls();
            clearControls();
            errValidator.Clear();
            this.Cursor = Cursors.Default;
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

            if (!ValidateValuesOnUI())
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            long EmpSalMasID = 0;
            long EmpSalDetID = 0;
            long SalProDetID = 0;
            int iRowCounter = 1;

            for (int i = 0; i <= dtgSalaryDetails.Rows.Count - 2; i++)
            {
                iRowCounter = 1;
                for (int j = 0; j < dtgSalaryDetails.Columns.Count; j++)
                {
                    if(j == 0)
                        EmpSalMasID = objEmployeePayroll.InsertEmployeeSalaryMasterInfo(Convert.ToInt16(dtgSalaryDetails.Rows[i].Cells[j].Value.ToString().Trim()), Convert.ToDateTime(dtSalaryDate.Text), cmbSalaryMonth.Text, 0, 0, 0, 0, 1);

                    if(j > 5)
                    {
                        if (dtgSalaryDetails.Columns[j].Visible == true)
                        {
                            int empSalProfileID = objSalaryProfile.getEmployeeSpecificSalaryProfile(Convert.ToInt16(dtgSalaryDetails.Rows[i].Cells[0].Value.ToString().Trim())).SalProfileID;
                            int AllowenceHeaderID = objAllowenceInfo.GetAllowenceTitleByTitle(dtgSalaryDetails.Columns[j].HeaderCell.Value.ToString());
                            int DeductionHeaderID = objDeductionsInfo.GetDeductionTitleByTitle(dtgSalaryDetails.Columns[j].HeaderCell.Value.ToString());
                            int ReimbursementHeaderID = objReimbursement.GetReimbursementTitleByTitle(dtgSalaryDetails.Columns[j].HeaderCell.Value.ToString());

                            if (AllowenceHeaderID > 0)
                            {
                                SalProDetID = objSalaryProfile.GetAllowenceProfileDetailID(empSalProfileID, AllowenceHeaderID);
                                EmpSalDetID = objEmployeePayroll.InsertEmployeeSalaryDetailsInfo(Convert.ToInt16(EmpSalMasID), Convert.ToInt16(SalProDetID), Convert.ToInt16(AllowenceHeaderID), dtgSalaryDetails.Columns[j].HeaderCell.Value.ToString(), "Allowences", Convert.ToDecimal(dtgSalaryDetails.Rows[i].Cells[j].Value.ToString()), 0, 0, iRowCounter);
                            }
                            else if (DeductionHeaderID > 0)
                            {
                                SalProDetID = objSalaryProfile.GetDeductionProfileDetailID(empSalProfileID, DeductionHeaderID);
                                EmpSalDetID = objEmployeePayroll.InsertEmployeeSalaryDetailsInfo(Convert.ToInt16(EmpSalMasID), Convert.ToInt16(SalProDetID), Convert.ToInt16(DeductionHeaderID), dtgSalaryDetails.Columns[j].HeaderCell.Value.ToString(), "Deductions", 0, Convert.ToDecimal(dtgSalaryDetails.Rows[i].Cells[j].Value.ToString()), 0, iRowCounter);
                            }
                            else if (ReimbursementHeaderID > 0)
                            {
                                SalProDetID = objSalaryProfile.GetReimbursementProfileDetailID(empSalProfileID, ReimbursementHeaderID);
                                EmpSalDetID = objEmployeePayroll.InsertEmployeeSalaryDetailsInfo(Convert.ToInt16(EmpSalMasID), Convert.ToInt16(SalProDetID), Convert.ToInt16(ReimbursementHeaderID), dtgSalaryDetails.Columns[j].HeaderCell.Value.ToString(), "Reimbursement", 0, 0, dtgSalaryDetails.Rows[i].Cells[j].Value == "{}" ? Convert.ToDecimal(dtgSalaryDetails.Rows[i].Cells[j].Value.ToString()) : 0, iRowCounter);
                            }
                            iRowCounter = iRowCounter + 1;
                        }
                    }
                }
            }

            this.Cursor = Cursors.Default;

            MessageBox.Show("Salary processed for the for the month : " + cmbSalaryMonth.SelectedItem.ToString() + " successfully.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);

            onGenerateButtonClick();
            clearControls();
            enableControls();
            errValidator.Clear();
        }

        public void onGenerateButtonClick()
        {
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = false;
            btnCancel.Enabled = true;
            LoadSalaryMonthList();
        }

        public void onModifyButtonClick()
        {
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = false;
            btnCancel.Enabled = true;
            LoadSalaryMonthList();
        }

        public void onRemoveButtonClick()
        {
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
            LoadSalaryMonthList();
        }

        public void onSaveButtonClick()
        {
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
            LoadSalaryMonthList();
        }

        public void onCancelButtonClick()
        {
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
            LoadSalaryMonthList();
        }

        public void clearControls()
        {
            dtgSalaryDetails.Text = DateTime.Now.ToString("dd-MM-yyyy");
            cmbSalaryMonth.DataSource = null;
            LoadSalaryMonthList();
            //dtgSalaryDetails.Enabled = false;
        }

        public void enableControls()
        {
            cmbSalaryMonth.Enabled = true;
            dtSalaryDate.Enabled = true;
            dtgSalaryDetails.Enabled = true;
        }

        public void disableControls()
        {
            cmbSalaryMonth.Enabled = false;
            dtSalaryDate.Enabled = false;
            dtgSalaryDetails.Enabled = false;
        }

        public void LoadSalaryMonthList()
        {
            cmbSalaryMonth.Items.Clear();

            List<string> last6Months = new List<string>();
            DateTime currentMonth = DateTime.Now;

            for (int i = 6; i >= 0; i--)
            {
                DateTime month = currentMonth.AddMonths(-i);
                cmbSalaryMonth.Items.Add(month.ToString("MMM - yyyy"));
            }
            cmbSalaryMonth.SelectedIndex = cmbSalaryMonth.Items.Count - 1;
        }

        private void frmPayrollBatchProcess_Load(object sender, EventArgs e)
        {
            clearControls();
            onCancelButtonClick();
            cmbSalaryMonth.Enabled = true;
            dtSalaryDate.Enabled = true;
            dtgSalaryDetails.Enabled = true;
        }

        private void btnReportingManagerSearch_Click(object sender, EventArgs e)
        {

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
            
            onModifyButtonClick();
            enableControls();
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
        }

        private void dtgSalaryDetails_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dtgSalaryDetails.Rows.Count > 0)
            {
                decimal sum = 0;
                for (int j = 0; j < dtgSalaryDetails.Columns.Count; j++)
                {
                    if (j > 5)
                    {
                        for (int i = 0; i <= dtgSalaryDetails.Rows.Count - 2; i++)
                        {
                            if (!string.IsNullOrEmpty(dtgSalaryDetails.Rows[i].Cells[j].Value.ToString()))
                                sum = sum + Convert.ToDecimal(dtgSalaryDetails.Rows[i].Cells[j].Value.ToString());    //dtgSalaryDetails.AsEnumerable().Where(r => !r.IsNull(i)).Sum(r => Convert.ToDouble(r[i]));
                            else
                                sum = sum + 0;
                        }
                        dtgSalaryDetails.Rows[dtgSalaryDetails.Rows.Count - 1].Cells[j].Value = sum;
                        sum = 0;
                    }
                }
                FormatDataGridView();
            }
        }

        private void dtgSalaryDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void cmbSalaryMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtgSalaryDetails.DataSource = null;
            dtgSalaryDetails.DataSource = objSalaryProfile.GetSalaryInfoForBatchProcess(Convert.ToDateTime(dtSalaryDate.Value));
            if (dtgSalaryDetails.Columns.Count > 0)
            {
                dtgSalaryDetails.Enabled = true;
                FormatDataGridView();
            }
        }

        private void btnViewCalender_Click(object sender, EventArgs e)
        {
            //frmAttendanceMater frmAttendanceMater = new frmAttendanceMater("listAttendanceMasterList", Convert.ToInt16(lblReportingManagerID.Text));
            frmAttendanceMater frmAttendanceMater = new frmAttendanceMater();
            frmAttendanceMater.ShowDialog(this);
        }

        private void frmPayrollBatchProcess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                objDashboard.lblDashboardTitle.Text = "Dashboard";
                this.Close();
            }
        }

        private void dtSalaryDate_ValueChanged(object sender, EventArgs e)
        {
            dtgSalaryDetails.DataSource = null;
            dtgSalaryDetails.DataSource = objSalaryProfile.GetSalaryInfoForBatchProcess(Convert.ToDateTime(dtSalaryDate.Value));
            if (dtgSalaryDetails.Columns.Count > 0)
            {
                dtgSalaryDetails.Enabled = true;
                FormatDataGridView();
            }
        }

        private void FormatDataGridView()
        {
            dtgSalaryDetails.Columns["EmpID"].Visible = false;
            dtgSalaryDetails.Columns["EmpCode"].HeaderText = "Employee Code";
            dtgSalaryDetails.Columns["EmpCode"].ReadOnly = true;
            dtgSalaryDetails.Columns["EmpCode"].Width = 125;
            dtgSalaryDetails.Columns["EmpName"].HeaderText = "Employee Name";
            dtgSalaryDetails.Columns["EmpName"].ReadOnly = true;
            dtgSalaryDetails.Columns["EmpName"].Width = 225;
            dtgSalaryDetails.Columns["DesignationTitle"].HeaderText = "Designation";
            dtgSalaryDetails.Columns["DesignationTitle"].ReadOnly = true;
            dtgSalaryDetails.Columns["DesignationTitle"].Width = 225;
            dtgSalaryDetails.Columns["DepartmentTitle"].HeaderText = "Department";
            dtgSalaryDetails.Columns["DepartmentTitle"].ReadOnly = true;
            dtgSalaryDetails.Columns["DepartmentTitle"].Width = 225;
            dtgSalaryDetails.Columns["EmpSalDate"].Visible = false;

            foreach (DataGridViewColumn indCol in dtgSalaryDetails.Columns)
            {
                if (indCol.Index > 5)
                {
                    indCol.Width = 165;
                    indCol.ReadOnly = false;
                    indCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
                    indCol.DefaultCellStyle.Format = "c2";
                }
            }

            if (dtgSalaryDetails.Rows.Count > 0)
            {
                dtgSalaryDetails.Rows[dtgSalaryDetails.Rows.Count - 1].ReadOnly = true;
                dtgSalaryDetails.Rows[dtgSalaryDetails.Rows.Count - 1].Cells[1].Value = "Total Sum";
                dtgSalaryDetails.Rows[dtgSalaryDetails.Rows.Count - 1].Cells[0].Style.BackColor = System.Drawing.Color.Blue;
                dtgSalaryDetails.Rows[dtgSalaryDetails.Rows.Count - 1].Cells[1].Style.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void picDownloadDataAsCSV_Click(object sender, EventArgs e)
        {
            string filePath = AppVariables.TempFolderPath + @"\Consolidated Salary Summary.csv";
            bool ReportGenerated = Download.DownloadExcel(filePath, dtgSalaryDetails);
            if(ReportGenerated)
                Download.OpenCSV(filePath);
        }
    }
}
