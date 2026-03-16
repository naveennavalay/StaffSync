using Common;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Wordprocessing;
using Humanizer;
using Krypton.Ribbon;
using Krypton.Toolkit;
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
using System.Threading;
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
        DALStaffSync.clsEmployeePersonalIDInfo objEmployeePersonalIDInfo = new DALStaffSync.clsEmployeePersonalIDInfo();
        DALStaffSync.clsAllowenceInfo objAllowenceInfo = new DALStaffSync.clsAllowenceInfo();
        DALStaffSync.clsDeductionsInfo objDeductionsInfo = new DALStaffSync.clsDeductionsInfo();
        DALStaffSync.clsReimbursement objReimbursement = new DALStaffSync.clsReimbursement();
        DALStaffSync.clsAttendanceMas objAttendanceMas = new DALStaffSync.clsAttendanceMas();
        DALStaffSync.clsAppSettings objAppSettings = new DALStaffSync.clsAppSettings();
        DALStaffSync.clsClientStatutory objClientStatutory = new DALStaffSync.clsClientStatutory();
        DALStaffSync.clsEmployeePersonalInfo objEmployeePersonalInfo = new DALStaffSync.clsEmployeePersonalInfo();
        DALStaffSync.clsAdvanceTransaction objAdvanceTransaction = new DALStaffSync.clsAdvanceTransaction();
        DALStaffSync.clsProfessionalTaxCalculation objProfessionalTaxCalculation = new DALStaffSync.clsProfessionalTaxCalculation();
        DALStaffSync.clsProvidentFundCalculation objProvidentFundCalculation = new DALStaffSync.clsProvidentFundCalculation();
        DALStaffSync.clsEmployeeStateInsurance objEmployeeStateInsurance = new DALStaffSync.clsEmployeeStateInsurance();
        DALStaffSync.clsEmployerContributionInfo objEmployerContributionInfo = new DALStaffSync.clsEmployerContributionInfo();

        frmDashboard objDashboard = (frmDashboard) System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();
        Dictionary<string, string> formulaMap = new Dictionary<string, string>();

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


            this.Cursor = Cursors.WaitCursor;

            chkExcludeEmpWithPendingAdvances.Checked = false;
            //chkExcludeEmpWithPendingAdvances.Values.Image = Properties.Resources.uncheck;
            chkExcludeEmpWithPendingAdvances.Text = "☐ Show Salary Summary excluding employees who have Pending Advances";

            ProcessSalaryBatch();

            //dtgSalaryDetails.Columns["SelectRow"].Frozen = true;
            //dtgSalaryDetails.Columns["EmpCode"].Frozen = true;
            //dtgSalaryDetails.Columns["EmpName"].Frozen = true;
            //dtgSalaryDetails.Columns["DesignationTitle"].Frozen = true;
            //dtgSalaryDetails.Columns["DepartmentTitle"].Frozen = true;

            this.Cursor = Cursors.Default;

            btnGenerateDetails.Enabled = true;
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
            objDashboard.sptrDashboardContainer.Visible = true;
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
            int OrderID = 1;

            decimal allowancesTotal = 0;
            decimal deductionsTotal = 0;
            decimal reimbursementsTotal = 0;

            DataGridView gridLeft = dtgSalaryDetails;     // first grid
            DataGridView gridRight = dtgSalaryDetails1;    // second grid

            int totalRowIndex = gridLeft.Rows.Count - 1;
            int rowCounter = 1;
            lblPFCalcAmount.Text = "0.00";

            foreach (DataGridViewRow row in dtgSalaryDetails.Rows)
            {
                if (rowCounter == dtgSalaryDetails.Rows.Count)
                    break;

                rowCounter = rowCounter + 1;
                lblReportingManagerID.Text = "";
                lblBasicSalary.Text = "0.00";
                lblBasicSalaryPerDay.Text = "0.00";
                lblBasicSalaryPerHour.Text = "0.00";
                txtTotalWorkedDays.Text = "0";
                txtLeaveDays.Text = "0";
                txtUnpaidLeaves.Text = "0";
                txtTotalPayableDays.Text = "0";
                txtAallowences.Text = "0.00";
                txtDeductions.Text = "0.00";
                txtReimbursement.Text = "0.00";
                txtNetPayable.Text = "0.00";

                decimal basicSalary = Convert.ToDecimal(row.Cells["Basic Salary"].Value);

                lblBasicSalary.Text = basicSalary.RoundUp().ToString("#,#0.00");
                lblBasicSalaryPerDay.Text = Convert.ToDecimal(basicSalary / Convert.ToDecimal(txtTotalWorkingDays.Text.ToString())).RoundUp().ToString("#,#0.00");
                lblBasicSalaryPerHour.Text = Convert.ToDecimal(Convert.ToDecimal(lblBasicSalaryPerDay.Text.ToString()) / Convert.ToDecimal("8.0")).RoundUp().ToString("#,#0.00");

                lblPFCalcAmount.Text = "0";
                lblPFCalcAmount.Text = Convert.ToDecimal(row.Cells["PFCalculatedAmount"].Value).RoundUp().ToString("#,#0.00");

                int empID = Convert.ToInt16(row.Cells["EmpID"].Value);
                lblReportingManagerID.Text = empID.ToString();

                //List<EmpStateAndGenderInfo> objEmpStateAndGenderInfo = objEmployeeMaster.getEmpStateAndGenderInfo(empID);
                //lblStateID.Text = objEmpStateAndGenderInfo[0].StateID.ToString();
                //lblSexID.Text = objEmpStateAndGenderInfo[0].SexID.ToString();

                int SalaryProfileID = 0;

                DateTime parsedDate = DateTime.ParseExact(cmbSalaryMonth.SelectedItem.ToString(), "MMM - yyyy", CultureInfo.InvariantCulture);
                int daysInMonth = DateTime.DaysInMonth(parsedDate.Year, parsedDate.Month);
                txtTotalWorkingDays.Text = daysInMonth.ToString();

                EmployeeTotalWorkingInfo objEmployeeTotalWorkingInfo = objAttendanceMas.GetEmployeeMonthlyWorkingDays(Convert.ToInt16(lblReportingManagerID.Text.ToString()), Convert.ToDateTime(parsedDate), parsedDate.AddDays(daysInMonth).Date);
                if (objEmployeeTotalWorkingInfo != null)
                {
                    txtTotalWorkedDays.Text = objEmployeeTotalWorkingInfo.TotalPresent.ToString();
                    txtLeaveDays.Text = objEmployeeTotalWorkingInfo.TotalPaidLeave.ToString();
                    txtUnpaidLeaves.Text = objEmployeeTotalWorkingInfo.TotalLossOfPay.ToString();
                    txtTotalPayableDays.Text = objEmployeeTotalWorkingInfo.TotalPayableDays.ToString();
                }
                else
                {
                    txtTotalWorkedDays.Text = "0";
                    txtLeaveDays.Text = "0";
                }

                foreach (DataGridViewRow headerRow in gridRight.Rows)
                {
                    if (headerRow.IsNewRow) continue;

                    int headerID = Convert.ToInt32(headerRow.Cells["HeaderID"].Value);
                    string headerName = Convert.ToString(headerRow.Cells["HeaderTitle"].Value);
                    string headerType = Convert.ToString(headerRow.Cells["HeaderType"].Value);

                    if (!gridLeft.Columns.Contains(headerName))
                        continue;

                    decimal value = 0;

                    var cellValue = gridLeft.Rows[totalRowIndex].Cells[headerName].Value;

                    if (chkExcludeEmpWithPendingAdvances.Checked && gridLeft.Rows[totalRowIndex].Cells["SelectRow"].ReadOnly == true)
                        continue;

                    if (cellValue != null)
                        decimal.TryParse(cellValue.ToString(), out value);

                    if (headerType == "Allowences" || headerType == "Allowances")
                        allowancesTotal += value;

                    else if (headerType == "Deductions")
                        deductionsTotal += value;

                    else if (headerType == "Reimbursement")
                        reimbursementsTotal += value;
                }

                txtAallowences.Text = allowancesTotal.ToString("N2");
                txtDeductions.Text = deductionsTotal.ToString("N2");
                txtReimbursement.Text = reimbursementsTotal.ToString("N2");

                txtNetPayable.Text = (allowancesTotal - deductionsTotal + reimbursementsTotal).ToString("N2");

                OrderID = 1;

                EmpSalMasID = objEmployeePayroll.InsertEmployeeSalaryMasterInfo(Convert.ToInt16(lblReportingManagerID.Text.Trim()), Convert.ToDateTime(dtSalaryDate.Text), cmbSalaryMonth.Text, Convert.ToDecimal(txtTotalWorkingDays.Text).RoundUp(), Convert.ToDecimal(txtTotalWorkedDays.Text), Convert.ToDecimal(txtLeaveDays.Text), Convert.ToDecimal(txtUnpaidLeaves.Text), Convert.ToDecimal(txtTotalPayableDays.Text), Convert.ToDecimal(lblBasicSalary.Text).RoundUp(), Convert.ToDecimal(lblBasicSalaryPerDay.Text).RoundUp(), Convert.ToDecimal(lblBasicSalaryPerHour.Text).RoundUp(), Convert.ToDecimal(txtAallowences.Text).RoundUp(), Convert.ToDecimal(txtDeductions.Text).RoundUp(), Convert.ToDecimal(txtReimbursement.Text).RoundUp(), Convert.ToDecimal(lblPFCalcAmount.Text).RoundUp(), Convert.ToDecimal(txtNetPayable.Text).RoundUp(), false);
                foreach (DataGridViewRow headerRow in gridRight.Rows)
                {
                    if (headerRow.IsNewRow) continue;

                    int headerID = Convert.ToInt32(headerRow.Cells["HeaderID"].Value);
                    string headerName = Convert.ToString(headerRow.Cells["HeaderTitle"].Value);
                    string headerType = Convert.ToString(headerRow.Cells["HeaderType"].Value);
                    string headerCalcFormula = Convert.ToString(headerRow.Cells["CalcFormula"].Value);

                    if (!gridLeft.Columns.Contains(headerName))
                        continue;

                    decimal value = 0;

                    var cellValue = gridLeft.Rows[totalRowIndex].Cells[headerName].Value;

                    if (chkExcludeEmpWithPendingAdvances.Checked && gridLeft.Rows[totalRowIndex].Cells["SelectRow"].ReadOnly == true)
                        continue;

                    if (cellValue != null)
                        decimal.TryParse(cellValue.ToString(), out value);

                    if (headerType == "Allowences")
                        EmpSalDetID = objEmployeePayroll.InsertEmployeeSalaryDetailsInfo(Convert.ToInt16(EmpSalMasID), Convert.ToInt16(SalProDetID), Convert.ToInt16(headerID), headerName.ToString(), headerType.ToString(), headerCalcFormula.ToString(), Convert.ToDecimal(cellValue.ToString()), 0, 0, OrderID);
                    else if (headerType == "Deductions")
                    {
                        EmpSalDetID = objEmployeePayroll.InsertEmployeeSalaryDetailsInfo(Convert.ToInt16(EmpSalMasID), Convert.ToInt16(SalProDetID), Convert.ToInt16(headerID), headerName.ToString(), headerType.ToString(), headerCalcFormula.ToString(), 0, Convert.ToDecimal(cellValue.ToString()), 0, OrderID);

                        DeductionModel tmpObj = objDeductionsInfo.getSelectedDeductionInfo(Convert.ToInt32(headerID));

                        if (tmpObj.CalcFormula.ToString().ToLower() == "computepf1")
                        {
                            ProvidentFund objProvidentFundResult = objProvidentFundCalculation.GetProvidentFundMasterInfo(Convert.ToInt32(headerID));
                            if (objProvidentFundResult != null)
                            {
                                if (objProvidentFundResult.EmprPFPercentageOrAmount.ToString().ToUpper() == "P")
                                    objEmployerContributionInfo.InsertEmployerContribution(Convert.ToInt32(EmpSalDetID), ((Convert.ToDecimal(lblPFCalcAmount.Text.ToString()).RoundUp() * Convert.ToDecimal(objProvidentFundResult.EmprPFPercentage)) / 100).RoundUp(), 0, 0, 0, 0, 0);
                                else
                                    objEmployerContributionInfo.InsertEmployerContribution(Convert.ToInt32(EmpSalDetID), Convert.ToDecimal(objProvidentFundResult.EmprPFAmount).RoundUp(), 0, 0, 0, 0, 0);

                                if (objProvidentFundResult.EmprPSPercentageOrAmount.ToString().ToUpper() == "P")
                                    objEmployerContributionInfo.InsertEmployerContribution(Convert.ToInt32(EmpSalDetID), 0, 0, ((Convert.ToDecimal(lblPFCalcAmount.Text.ToString()).RoundUp() * Convert.ToDecimal(objProvidentFundResult.EmprPSPercentage)) / 100).RoundUp(), 0, 0, 0);
                                else
                                    objEmployerContributionInfo.InsertEmployerContribution(Convert.ToInt32(EmpSalDetID), 0, 0, Convert.ToDecimal(objProvidentFundResult.EmprPSAmount).RoundUp(), 0, 0, 0);
                            }
                        }
                        else if (tmpObj.CalcFormula.ToString().ToLower() == "computepf1")
                        {

                        }
                        else if (tmpObj.CalcFormula.ToString().ToLower() == "computeesi1")
                        {
                            ESIModel objEmployeeStateInsuranceResult = objEmployeeStateInsurance.GetEmployeeStateInsuranceMasterInfo(Convert.ToInt32(headerID));
                            if (objEmployeeStateInsuranceResult != null)
                            {
                                if (Convert.ToDecimal(txtAallowences.Text.ToString()) <= Convert.ToDecimal(objEmployeeStateInsuranceResult.MaxESIAmount))
                                {
                                    if (objEmployeeStateInsuranceResult.EmpESIPercentageOrAmount.ToString().ToUpper() == "P")
                                        objEmployerContributionInfo.InsertEmployerContribution(Convert.ToInt32(EmpSalDetID), 0, 0, 0, ((Convert.ToDecimal(txtAallowences.Text.ToString()).RoundUp() * Convert.ToDecimal(objEmployeeStateInsuranceResult.EmprPercentage)) / 100).RoundUp(), 0, 0);
                                    else
                                        objEmployerContributionInfo.InsertEmployerContribution(Convert.ToInt32(EmpSalDetID), 0, 0, Convert.ToDecimal(objEmployeeStateInsuranceResult.EmprESIAmount).RoundUp(), 0, 0, 0);
                                }
                            }
                        }
                    }
                    else if (headerType == "Reimbursement")
                        EmpSalDetID = objEmployeePayroll.InsertEmployeeSalaryDetailsInfo(Convert.ToInt16(EmpSalMasID), Convert.ToInt16(SalProDetID), Convert.ToInt16(headerID), headerName.ToString(), headerType.ToString(), headerCalcFormula.ToString(), 0, 0, Convert.ToDecimal(cellValue.ToString()), OrderID);

                    OrderID = OrderID + 1;
                }
            }

            this.Cursor = Cursors.Default;

            MessageBox.Show("Salary processed for the for the month : " + cmbSalaryMonth.SelectedItem.ToString() + " successfully.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);

            lblPFCalcAmount.Text = "0.00";
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
            btnGenerateDetails.Enabled = false; 
            dtgSalaryDetails.Text = DateTime.Now.ToString("dd-MM-yyyy");
            cmbSalaryMonth.DataSource = null;
            LoadSalaryMonthList();
            lblReportingManagerID.Text = "";
            lblBasicSalary.Text = "0.00";
            lblBasicSalaryPerDay.Text = "0.00";
            lblBasicSalaryPerHour.Text = "0.00";
            lblPFCalcAmount.Text = "0.00";
            txtTotalWorkingDays.Text = "0";
            txtTotalWorkedDays.Text = "0";
            txtLeaveDays.Text = "0";
            txtUnpaidLeaves.Text = "0";
            lblActionMode.Text = "add"; //Don't change the value of this label as it is being used to identify the mode of operation "add" by default when the form is loaded for the first time.
            //dtgSalaryDetails.Enabled = false;
            dtgSalaryDetails1.DataSource = null;
            dtgSalaryDetails1.Rows.Clear();
            dtgSalaryDetails1.Refresh();
            lblReportingManagerID.Text = "";
            lblBasicSalary.Text = "0.00";
            lblBasicSalaryPerDay.Text = "0.00";
            lblBasicSalaryPerHour.Text = "0.00";
            txtTotalWorkedDays.Text = "0";
            txtLeaveDays.Text = "0";
            txtUnpaidLeaves.Text = "0";
            txtTotalPayableDays.Text = "0";
            txtAallowences.Text = "0.00";
            txtDeductions.Text = "0.00";
            txtReimbursement.Text = "0.00";
            txtNetPayable.Text = "0.00";
            lblTotalEarnings.Text = "0.00";
            lblTotalDeductions.Text = "0.00";
            lblTotalReimbursement.Text = "0.00";
            lblNetPayable.Text = "0.00";
            chkExcludeEmpWithPendingAdvances.Checked = false;
            //chkExcludeEmpWithPendingAdvances.Values.Image = Properties.Resources.uncheck;
            chkExcludeEmpWithPendingAdvances.Text = "☐ Show Salary Summary excluding employees who have Pending Advances";
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
            //cmbSalaryMonth.Items.Clear();

            //List<string> last6Months = new List<string>();
            //DateTime currentMonth = DateTime.Now;

            //for (int i = 6; i >= 0; i--)
            //{
            //    DateTime month = currentMonth.AddMonths(-i);
            //    cmbSalaryMonth.Items.Add(month.ToString("MMM - yyyy"));
            //}
            //cmbSalaryMonth.SelectedIndex = cmbSalaryMonth.Items.Count - 1;

            cmbSalaryMonth.Items.Clear();

            List<string> last6Months = new List<string>();
            DateTime currentMonth = DateTime.Now;

            //for (int i = 6; i >= 0; i--)
            //{
            //    DateTime month = currentMonth.AddMonths(-i);
            //    cmbSalaryMonth.Items.Add(month.ToString("MMM - yyyy"));
            //}

            currentMonth = DateTime.Parse("01-01-" + DateTime.Now.Year.ToString());
            for (int i = 0; i < DateTime.Now.Month - 1; i++)
            {
                DateTime month = currentMonth.AddMonths(i);
                cmbSalaryMonth.Items.Add(month.ToString("MMM - yyyy"));
            }
            //cmbSalaryMonth.SelectedIndex = DateTime.Now.Month-1;
            cmbSalaryMonth.SelectedIndex = cmbSalaryMonth.Items.Count - 1;

            //cmbSalaryMonth.Items.Add("Jan - " + DateTime.Now.Year.ToString());
            //cmbSalaryMonth.Items.Add("Feb - " + DateTime.Now.Year.ToString());
            //cmbSalaryMonth.Items.Add("Mar - " + DateTime.Now.Year.ToString());
            //cmbSalaryMonth.Items.Add("Apr - " + DateTime.Now.Year.ToString());
            //cmbSalaryMonth.Items.Add("May - " + DateTime.Now.Year.ToString());
            //cmbSalaryMonth.Items.Add("Jun - " + DateTime.Now.Year.ToString());
            //cmbSalaryMonth.Items.Add("Jul - " + DateTime.Now.Year.ToString());
            //cmbSalaryMonth.Items.Add("Aug - " + DateTime.Now.Year.ToString());
            //cmbSalaryMonth.Items.Add("Sep - " + DateTime.Now.Year.ToString());
            //cmbSalaryMonth.Items.Add("Oct - " + DateTime.Now.Year.ToString());
            //cmbSalaryMonth.Items.Add("Nov - " + DateTime.Now.Year.ToString());
            //cmbSalaryMonth.Items.Add("Dec - " + DateTime.Now.Year.ToString());
            //cmbSalaryMonth.SelectedIndex = cmbSalaryMonth.Items.Count - 1;
        }

        private void frmPayrollBatchProcess_Load(object sender, EventArgs e)
        {
            clearControls();
            onCancelButtonClick();
            cmbSalaryMonth.Enabled = true;
            dtSalaryDate.Enabled = true;
            dtgSalaryDetails.Enabled = true;
            btnGenerateDetails.Enabled = false;

            //ProcessSalaryBatch();
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
            if (e.RowIndex == dtgSalaryDetails.Rows.Count - 1) // last row
            {
                e.Cancel = true;
            }
        }

        private void dtgSalaryDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void cmbSalaryMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private decimal EvaluateFormula(string formula, DataGridViewRow row)
        {
            formula = formula.Replace("Basic Salary", row.Cells["Basic Salary"].Value.ToString());
            formula = formula.Replace("TotalPayableDays", txtTotalPayableDays.Text.ToString()); //txtPayableDays.Text
            formula = formula.Replace("TotalWorkingDays", txtTotalWorkingDays.Text.ToString()); //txtWorkingDays.Text

            DataTable dt = new DataTable();
            return Convert.ToDecimal(dt.Compute(formula, ""));
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
                objDashboard.sptrDashboardContainer.Visible = true;
                this.Close();
            }
        }

        private void dtSalaryDate_ValueChanged(object sender, EventArgs e)
        {
            //dtgSalaryDetails.DataSource = null;
            //dtgSalaryDetails.DataSource = objSalaryProfile.GetSalaryInfoForBatchProcess(Convert.ToInt32(objTempClientFinYearInfo.ClientID), Convert.ToDateTime(dtSalaryDate.Value));
            //if (dtgSalaryDetails.Columns.Count > 0)
            //{
            //    dtgSalaryDetails.Enabled = true;
            //    FormatDataGridView();
            //}
        }

        private void FormatDataGridView()
        {
            int intEmployeesExcluded = 0;

            dtgSalaryDetails.Columns["ClientID"].Visible = false;
            dtgSalaryDetails.Columns["EmpID"].Visible = false;
            dtgSalaryDetails.Columns["EmpCode"].HeaderText = "Employee Code";
            //dtgSalaryDetails.Columns["EmpCode"].ReadOnly = false;
            dtgSalaryDetails.Columns["EmpCode"].Width = 125;
            //dtgSalaryDetails.Columns["EmpCode"].Frozen = true;
            dtgSalaryDetails.Columns["EmpName"].HeaderText = "Employee Name";
            //dtgSalaryDetails.Columns["EmpName"].ReadOnly = false;
            dtgSalaryDetails.Columns["EmpName"].Width = 225;
            //dtgSalaryDetails.Columns["EmpName"].Frozen = true;
            dtgSalaryDetails.Columns["DesignationTitle"].HeaderText = "Designation";
            //dtgSalaryDetails.Columns["DesignationTitle"].ReadOnly = false;
            dtgSalaryDetails.Columns["DesignationTitle"].Width = 225;
            //dtgSalaryDetails.Columns["DesignationTitle"].Frozen = true;
            dtgSalaryDetails.Columns["DepartmentTitle"].HeaderText = "Department";
            //dtgSalaryDetails.Columns["DepartmentTitle"].ReadOnly = false;
            dtgSalaryDetails.Columns["DepartmentTitle"].Width = 225;
            //dtgSalaryDetails.Columns["DepartmentTitle"].Frozen = true;
            //dtgSalaryDetails.Columns["EmpSalDate"].Visible = false;

            foreach (DataGridViewColumn col in dtgSalaryDetails.Columns)
            {
                if (col.Name != "SelectRow")
                    col.ReadOnly = true;
                //else
                //    col.ReadOnly = false;

                col.Width = 165;
                if (col.Index > 5)
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
                    col.DefaultCellStyle.Format = "c2";
                }
            }
            dtgSalaryDetails.Columns["SelectRow"].Width = 75;
            //dtgSalaryDetails.Columns["SelectRow"].ReadOnly = false;

            //if (dtgSalaryDetails.Rows.Count > 0)
            //{
            //    dtgSalaryDetails.Rows[dtgSalaryDetails.Rows.Count - 1].ReadOnly = true;
            //    dtgSalaryDetails.Rows[dtgSalaryDetails.Rows.Count - 1].Cells[1].Value = "Total Sum";
            //    dtgSalaryDetails.Rows[dtgSalaryDetails.Rows.Count - 1].Cells[0].Style.BackColor = System.Drawing.Color.Blue;
            //    dtgSalaryDetails.Rows[dtgSalaryDetails.Rows.Count - 1].Cells[1].Style.ForeColor = System.Drawing.Color.Black;
            //}

            int rowCounter = 1;
            foreach (DataGridViewRow row in dtgSalaryDetails.Rows)
            {
                if (rowCounter == dtgSalaryDetails.Rows.Count)
                    break;

                rowCounter = rowCounter + 1;
                if (objAdvanceTransaction.getEmployeeAdvancePendingCount(Convert.ToInt16(row.Cells["EmpID"].Value)) > 0)
                {
                    intEmployeesExcluded = intEmployeesExcluded + 1;
                    row.Cells["SelectRow"].ReadOnly = true;
                    row.Cells["SelectRow"].Value = Convert.ToBoolean(false);
                    row.Cells["SelectRow"].ToolTipText = "Advance pending. Please process salary individually.";
                }
                else
                    row.Cells["SelectRow"].ReadOnly = false;
            }

            lblSalarySummary.Text = "Employees loaded : " + Convert.ToInt32(dtgSalaryDetails.Rows.Count - 1) + "\nExcluded due to advances : " + Convert.ToInt32(intEmployeesExcluded)  + "\nEligible for batch processing : " + (Convert.ToInt32(dtgSalaryDetails.Rows.Count - 1) - Convert.ToInt32(intEmployeesExcluded));
        }

        private void picDownloadDataAsCSV_Click(object sender, EventArgs e)
        {
            string filePath = AppVariables.TempFolderPath + @"\Consolidated Salary Summary.csv";
            bool ReportGenerated = Download.DownloadExcel(filePath, dtgSalaryDetails);
            if(ReportGenerated)
                Download.OpenCSV(filePath);
        }

        private void frmPayrollBatchProcess_Activated(object sender, EventArgs e)
        {
            dtgSalaryDetails.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
        }

        private void btnBatchProcess_Click(object sender, EventArgs e)
        {
            
            this.Cursor = Cursors.WaitCursor;

            btnGenerateDetails.Enabled = false;

            chkExcludeEmpWithPendingAdvances.Checked = false;
            //chkExcludeEmpWithPendingAdvances.Values.Image = Properties.Resources.uncheck;
            chkExcludeEmpWithPendingAdvances.Text = "☐ Show Salary Summary excluding employees who have Pending Advances";

            ProcessSalaryBatch();

            foreach (DataGridViewColumn col in dtgSalaryDetails.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            this.Cursor = Cursors.Default;
                        
            btnGenerateDetails.Enabled = true;

            MessageBox.Show("Salary information processed and initiated.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ProcessSalaryBatch()
        {
            int rowCounter1 = 1;

            lblSalarySummary.Text = "Employees loaded : 0 \nExcluded due to advances : 0 \nEligible for batch processing : 0";

            dtgSalaryDetails.ReadOnly = false;
            chkSelectUnSelect.Checked = false;
            //chkExcludeEmpWithPendingAdvances.Checked = false;
            dtgSalaryDetails.DataSource = null;
            dtgSalaryDetails1.DataSource = null;
            dtgSalaryDetails1.Rows.Clear();
            dtgSalaryDetails1.Refresh();
            dtgSalaryDetails.DataSource = objSalaryProfile.GetSalaryInfoForBatchProcess(Convert.ToInt32(objTempClientFinYearInfo.ClientID), Convert.ToDateTime(dtSalaryDate.Value));
            if (dtgSalaryDetails.Columns.Count > 0)
            {
                dtgSalaryDetails.Columns["ClientID"].Visible = false;
                dtgSalaryDetails.Columns["EmpID"].Visible = false;

                // Remove existing checkbox column if already added
                if (!dtgSalaryDetails.Columns.Contains("SelectRow"))
                {
                    DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                    chk.Name = "SelectRow";
                    chk.HeaderText = "Select";
                    chk.Width = 20;
                    chk.FalseValue = false;
                    chk.ReadOnly = false;
                    chk.TrueValue = true;

                    dtgSalaryDetails.Columns.Insert(0, chk);
                }
                if (!dtgSalaryDetails.Columns.Contains("PFCalculatedAmount"))
                {
                    DataGridViewTextBoxColumn colPFCalculatedAmount = new DataGridViewTextBoxColumn();
                    colPFCalculatedAmount.Name = "PFCalculatedAmount";
                    colPFCalculatedAmount.HeaderText = "PFCalculatedAmount";
                    colPFCalculatedAmount.Width = 20;
                    colPFCalculatedAmount.ReadOnly = false;
                    colPFCalculatedAmount.ValueType = typeof(decimal);

                    dtgSalaryDetails.Columns.Insert(0, colPFCalculatedAmount);
                }

                int lastRow = dtgSalaryDetails.Rows.Count - 1;

                dtgSalaryDetails.Rows[lastRow].Cells["SelectRow"].ReadOnly = true;
                dtgSalaryDetails.Rows[lastRow].Cells["SelectRow"].Value = false;
                dtgSalaryDetails.Rows[lastRow].Cells["EmpName"].ReadOnly = false;
                dtgSalaryDetails.Rows[lastRow].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                dtgSalaryDetails.Rows[lastRow].ReadOnly = true;

                //dtgSalaryDetails.Columns["SelectRow"].ReadOnly = false;
                foreach (DataGridViewColumn col in dtgSalaryDetails.Columns)
                {
                    if (col.Name != "SelectRow")
                        col.ReadOnly = true;
                    //else
                    //    col.ReadOnly = false;

                    col.Width = 150;
                }

                dtgSalaryDetails.Columns["SelectRow"].Width = 75;

                int rowCounter = 1;
                formulaMap = objSalaryProfile.getSalaryHeadersList();
                foreach (DataGridViewRow row in dtgSalaryDetails.Rows)
                {
                    if (rowCounter == dtgSalaryDetails.Rows.Count)
                        break;

                    dtgSalaryDetails1.DataSource = null;
                    dtgSalaryDetails1.Rows.Clear();
                    dtgSalaryDetails1.Refresh();
                    lblReportingManagerID.Text = "";
                    lblBasicSalary.Text = "0.00";
                    lblBasicSalaryPerDay.Text = "0.00";
                    lblBasicSalaryPerHour.Text = "0.00";
                    txtTotalWorkedDays.Text = "0";
                    txtLeaveDays.Text = "0";
                    txtUnpaidLeaves.Text = "0";
                    txtTotalPayableDays.Text = "0";
                    txtAallowences.Text = "0.00";
                    txtDeductions.Text = "0.00";
                    txtReimbursement.Text = "0.00";
                    txtNetPayable.Text = "0.00";
                    lblPFCalcAmount.Text = "0";

                    rowCounter = rowCounter + 1;
                    decimal basicSalary = Convert.ToDecimal(row.Cells["Basic Salary"].Value);

                    int empID = Convert.ToInt16(row.Cells["EmpID"].Value);
                    lblReportingManagerID.Text = empID.ToString();

                    List<EmpStateAndGenderInfo> objEmpStateAndGenderInfo = objEmployeeMaster.getEmpStateAndGenderInfo(empID);
                    lblStateID.Text = objEmpStateAndGenderInfo[0].StateID.ToString();
                    lblSexID.Text = objEmpStateAndGenderInfo[0].SexID.ToString();

                    int SalaryProfileID = 0;

                    DateTime parsedDate = DateTime.ParseExact(cmbSalaryMonth.SelectedItem.ToString(), "MMM - yyyy", CultureInfo.InvariantCulture);
                    int daysInMonth = DateTime.DaysInMonth(parsedDate.Year, parsedDate.Month);
                    txtTotalWorkingDays.Text = daysInMonth.ToString();

                    EmployeeTotalWorkingInfo objEmployeeTotalWorkingInfo = objAttendanceMas.GetEmployeeMonthlyWorkingDays(Convert.ToInt16(lblReportingManagerID.Text.ToString()), Convert.ToDateTime(parsedDate), Convert.ToDateTime(parsedDate));
                    if (objEmployeeTotalWorkingInfo != null)
                    {
                        txtTotalWorkedDays.Text = objEmployeeTotalWorkingInfo.TotalPresent.ToString();
                        txtLeaveDays.Text = objEmployeeTotalWorkingInfo.TotalPaidLeave.ToString();
                        txtUnpaidLeaves.Text = objEmployeeTotalWorkingInfo.TotalLossOfPay.ToString();
                        txtTotalPayableDays.Text = objEmployeeTotalWorkingInfo.TotalPayableDays.ToString();
                    }
                    else
                    {
                        txtTotalWorkedDays.Text = "0";
                        txtLeaveDays.Text = "0";
                    }

                    //SalaryProfileID = objSalaryProfile.getEmployeeSpecificSalaryProfile(Convert.ToInt16(lblReportingManagerID.Text.ToString())).SalProfileID;
                    SalaryProfileID = objSalaryProfile.getEmployeeSalaryConfigStructure(Convert.ToInt16(empID.ToString()));

                    dtgSalaryDetails1.Enabled = true;
                    //dtgSalaryDetails.DataSource = objSalaryProfile.GetEmployeeSpecificSalaryProfileInfo(Convert.ToInt16(lblReportingManagerID.Text));
                    dtgSalaryDetails1.DataSource = null;
                    dtgSalaryDetails1.Rows.Clear();
                    dtgSalaryDetails1.Refresh();
                    dtgSalaryDetails1.DataSource = objSalaryProfile.getEmployeeSpecificSalaryConfigStructure(Convert.ToInt16(empID.ToString()), Convert.ToInt16(SalaryProfileID));
                    if (SalaryProfileID == 0)
                        dtgSalaryDetails1.DataSource = objSalaryProfile.GetDefaultSalaryProfileInfo(SalaryProfileID);

                    dtgSalaryDetails1.FirstDisplayedScrollingRowIndex = dtgSalaryDetails.Rows[1].Index;
                    dtgSalaryDetails1.BeginEdit(true);

                    rowCounter1 = 1;
                    foreach (DataGridViewRow rowTop in dtgSalaryDetails.Rows)
                    {
                        if (rowTop.IsNewRow) continue;

                        if (rowCounter1 == dtgSalaryDetails.Rows.Count)
                            break;

                        rowCounter1 = rowCounter1 + 1;

                        // Start after non-salary columns
                        for (int colIndex = 6; colIndex < dtgSalaryDetails.Columns.Count; colIndex++)
                        {
                            string salaryHeader = dtgSalaryDetails.Columns[colIndex].HeaderText;

                            foreach (DataGridViewRow rowBottom in dtgSalaryDetails1.Rows)
                            {
                                if (rowBottom.IsNewRow) continue;

                                string headerBottom = Convert.ToString(rowBottom.Cells["HeaderTitle"].Value);

                                if (headerBottom == salaryHeader)
                                {
                                    string type = Convert.ToString(rowBottom.Cells["HeaderType"].Value);

                                    decimal value = 0;

                                    if (type == "Allowences")
                                    {
                                        value = Convert.ToDecimal(rowBottom.Cells["AllowanceAmount"].Value ?? 0);
                                    }
                                    else if (type == "Deductions")
                                    {
                                        value = Convert.ToDecimal(rowBottom.Cells["DeductionAmount"].Value ?? 0);
                                    }
                                    else if (type == "Reimbursement")
                                    {
                                        value = Convert.ToDecimal(rowBottom.Cells["ReimbursmentAmount"].Value ?? 0);
                                    }

                                    rowTop.Cells[colIndex].Value = value;

                                    break; // stop searching once matched
                                }
                            }
                        }
                    }

                    int selectedRowIndex = row.Index;
                    int SelectedEmpID = Convert.ToInt32(dtgSalaryDetails.Rows[row.Index].Cells["EmpID"].Value);

                    frmIndPayrollMaster frmIndPayrollMaster = new frmIndPayrollMaster("listPayrollUsersList", objTempCurrentlyLoggedInUserInfo, objTempClientFinYearInfo, this, Convert.ToInt32(SelectedEmpID), cmbSalaryMonth.Text.ToString(), Convert.ToDateTime(dtSalaryDate.Value.ToString()), true);
                    frmIndPayrollMaster.Visible = false;
                    frmIndPayrollMaster.StartPosition = FormStartPosition.Manual;
                    frmIndPayrollMaster.Location = new System.Drawing.Point(-5000, -5000);
                    if (frmIndPayrollMaster.ShowDialog() == DialogResult.OK)
                    {
                        UpdateSalaryRow(selectedRowIndex, frmIndPayrollMaster.UpdatedSalaryValues);
                    }
                }

                DataGridViewRow totalRow = new DataGridViewRow();
                totalRow = dtgSalaryDetails.Rows[dtgSalaryDetails.Rows.Count - 1];

                totalRow.Cells[7].Value = "Total";
                for (int col = 7; col < dtgSalaryDetails.Columns.Count; col++) // skip employee info columns
                {
                    decimal sum = 0;

                    foreach (DataGridViewRow row in dtgSalaryDetails.Rows)
                    {
                        if (row.Index == totalRow.Index)
                            continue;

                        if (row.IsNewRow) continue;

                        decimal value = 0;
                        decimal.TryParse(Convert.ToString(row.Cells[col].Value), out value);

                        sum += value;
                    }

                    totalRow.Cells[col].Value = sum;
                }

                totalRow.DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                totalRow.DefaultCellStyle.Font = new System.Drawing.Font(dtgSalaryDetails.Font, FontStyle.Bold);

                chkSelectUnSelect.Checked = true;

                //chkExcludeEmpWithPendingAdvances.Checked = false;
                FormatDataGridView();
                CalculateSalarySummary();
            }
        }

        private void CalculateSalarySummary()
        {
            decimal allowancesTotal = 0;
            decimal deductionsTotal = 0;
            decimal reimbursementsTotal = 0;

            DataGridView gridLeft = dtgSalaryDetails;     // first grid
            DataGridView gridRight = dtgSalaryDetails1;    // second grid

            int totalRowIndex = gridLeft.Rows.Count - 1;

            foreach (DataGridViewRow headerRow in gridRight.Rows)
            {
                if (headerRow.IsNewRow) continue;

                string headerName = Convert.ToString(headerRow.Cells["HeaderTitle"].Value);
                string headerType = Convert.ToString(headerRow.Cells["HeaderType"].Value);

                if (!gridLeft.Columns.Contains(headerName))
                    continue;

                decimal value = 0;

                var cellValue = gridLeft.Rows[totalRowIndex].Cells[headerName].Value;

                if (chkExcludeEmpWithPendingAdvances.Checked && gridLeft.Rows[totalRowIndex].Cells["SelectRow"].ReadOnly == true)
                    continue;

                if (cellValue != null)
                    decimal.TryParse(cellValue.ToString(), out value);

                if (headerType == "Allowences" || headerType == "Allowances")
                    allowancesTotal += value;

                else if (headerType == "Deductions")
                    deductionsTotal += value;

                else if (headerType == "Reimbursement")
                    reimbursementsTotal += value;
            }

            lblTotalEarnings.Text = allowancesTotal.ToString("N2");
            lblTotalDeductions.Text = deductionsTotal.ToString("N2");
            lblTotalReimbursement.Text = reimbursementsTotal.ToString("N2");

            lblNetPayable.Text = (allowancesTotal - deductionsTotal + reimbursementsTotal).ToString("N2");
        }

        private void SelectAllRows()
        {
            for (int i = 0; i < dtgSalaryDetails.Rows.Count - 1; i++) // skip total row
            {
                if (dtgSalaryDetails.Rows[i].Cells["SelectRow"].ReadOnly == true)
                {
                    dtgSalaryDetails.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                    continue;
                }
                dtgSalaryDetails.Rows[i].Cells["SelectRow"].Value = true;
            }
        }

        private void UnselectAllRows()
        {
            for (int i = 0; i < dtgSalaryDetails.Rows.Count - 1; i++)
            {
                if (dtgSalaryDetails.Rows[i].Cells["SelectRow"].ReadOnly == true)
                {
                    dtgSalaryDetails.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                    continue;
                }

                dtgSalaryDetails.Rows[i].Cells["SelectRow"].Value = false;
            }
        }

        private void dtgSalaryDetails1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            ////////////////
            decimal totalAallowences = 0;
            decimal totalDeductions = 0;
            decimal totalReimbursement = 0;

            txtAallowences.Text = "0.00";
            txtDeductions.Text = "0.00";
            txtReimbursement.Text = "0.00";
            txtNetPayable.Text = "0.00";

            if (lblReportingManagerID.Text.ToString().Trim() == "")
                return;

            if (chkAutoCalculate.Checked == true)
            {

            }

            foreach (DataGridViewRow dc in dtgSalaryDetails1.Rows)
            {
                totalAallowences = totalAallowences + Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString());
                totalDeductions = totalDeductions + Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString());
                totalReimbursement = totalReimbursement + Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString());
            }

            dtgSalaryDetails1.Columns["AllowanceAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgSalaryDetails1.Columns["AllowanceAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryDetails1.Columns["DeductionAmount"].Width = 135;
            dtgSalaryDetails1.Columns["ReimbursmentAmount"].Width = 135;
            dtgSalaryDetails1.Columns["DeductionAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Deductions
            dtgSalaryDetails1.Columns["DeductionAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryDetails1.Columns["ReimbursmentAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Reimbursments
            dtgSalaryDetails1.Columns["ReimbursmentAmount"].DefaultCellStyle.Format = "c2";

            txtAallowences.Text = Convert.ToDecimal(totalAallowences.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtDeductions.Text = Convert.ToDecimal(totalDeductions.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtReimbursement.Text = Convert.ToDecimal(totalReimbursement.ToString()).ToString("0.00", CultureInfo.InvariantCulture);

            totalAallowences = 0;
            totalDeductions = 0;
            totalReimbursement = 0;

            decimal indCalculatedAmount = 0;
            decimal basicSalary = dtgSalaryDetails1.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == "Basic Salary").Select(r => Convert.ToDecimal(r.Cells["ActualAmount"].Value ?? 0)).FirstOrDefault();

            lblBasicSalary.Text = basicSalary.RoundUp().ToString("#,#0.00");
            lblBasicSalaryPerDay.Text = Convert.ToDecimal(basicSalary / Convert.ToDecimal(txtTotalWorkingDays.Text.ToString())).RoundUp().ToString("#,#0.00");
            lblBasicSalaryPerHour.Text = Convert.ToDecimal(Convert.ToDecimal(lblBasicSalaryPerDay.Text.ToString()) / Convert.ToDecimal("8.0")).RoundUp().ToString("#,#0.00");

            foreach (DataGridViewRow dc in dtgSalaryDetails1.Rows)
            {
                totalAallowences = totalAallowences + Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString());
                totalDeductions = totalDeductions + Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString());
                totalReimbursement = totalReimbursement + Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString());
                if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "allowences")
                {
                    AllowenceModel tmpObj = objAllowenceInfo.getSelectedAllowenceInfo(Convert.ToInt32(dc.Cells["HeaderID"].Value.ToString()));

                    dc.Cells["AllowanceAmount"].ReadOnly = false;
                    dc.Cells["DeductionAmount"].ReadOnly = true;
                    dc.Cells["ReimbursmentAmount"].ReadOnly = true;
                    if (dc.Cells["CalcFormula"].Value.ToString() != "N/A")
                    {
                        dc.Cells["HeaderTitle"].ToolTipText = dc.Cells["CalcFormula"].Value.ToString();

                        string formula = dc.Cells["CalcFormula"].Value.ToString();
                        string headerName = formula.Substring(0, formula.IndexOf("*")).Trim();
                        string amount = "0"; // dtgSalaryDetails1.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == headerName).Select(r => Convert.ToDecimal(r.Cells["ActualAmount"].Value ?? 0)).FirstOrDefault().ToString() ?? "-1";

                        DateTime parsedDate = DateTime.ParseExact(cmbSalaryMonth.SelectedItem.ToString(), "MMM - yyyy", CultureInfo.InvariantCulture);
                        int daysInMonth = DateTime.DaysInMonth(parsedDate.Year, parsedDate.Month);
                        txtTotalWorkingDays.Text = daysInMonth.ToString();

                        EmployeeTotalWorkingInfo objEmployeeTotalWorkingInfo = objAttendanceMas.GetEmployeeMonthlyWorkingDays(Convert.ToInt16(lblReportingManagerID.Text.ToString()), Convert.ToDateTime(parsedDate), parsedDate.AddDays(daysInMonth).Date);
                        if (objEmployeeTotalWorkingInfo != null)
                        {
                            txtTotalWorkedDays.Text = objEmployeeTotalWorkingInfo.TotalPresent.ToString();
                            txtLeaveDays.Text = objEmployeeTotalWorkingInfo.TotalPaidLeave.ToString();
                            txtUnpaidLeaves.Text = objEmployeeTotalWorkingInfo.TotalLossOfPay.ToString();
                            txtTotalPayableDays.Text = objEmployeeTotalWorkingInfo.TotalPayableDays.ToString();
                        }
                        else
                        {
                            txtTotalWorkedDays.Text = "0";
                            txtLeaveDays.Text = "0";
                        }
                        //amount = basicSalary.ToString();

                        if (dc.Cells["HeaderTitle"].Value.ToString() == "Basic Salary")
                        {
                            if (tmpObj.ProrataBasis == true)
                                indCalculatedAmount = dtgSalaryDetails1.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == "Basic Salary").Select(r => Convert.ToDecimal(r.Cells["ActualAmount"].Value ?? 0)).FirstOrDefault();
                            else if (tmpObj.ProrataBasis == false)
                                indCalculatedAmount = dtgSalaryDetails1.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == "Basic Salary").Select(r => Convert.ToDecimal(r.Cells["ActualAmount"].Value ?? 0)).FirstOrDefault();

                            lblBasicSalary.Text = indCalculatedAmount.RoundUp().ToString("#,#0.00");
                            lblBasicSalaryPerDay.Text = Convert.ToDecimal(basicSalary / Convert.ToDecimal(txtTotalWorkingDays.Text.ToString())).RoundUp().ToString("#,#0.00");
                            lblBasicSalaryPerHour.Text = Convert.ToDecimal(Convert.ToDecimal(lblBasicSalaryPerDay.Text.ToString()) / Convert.ToDecimal("8.0")).RoundUp().ToString("#,#0.00");
                        }
                        else
                        {
                            if (tmpObj.IsFixed)
                            {
                                if (tmpObj.ProrataBasis)
                                    indCalculatedAmount = Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()).RoundUp() * Convert.ToDecimal(txtTotalPayableDays.Text.ToString()) / Convert.ToDecimal(txtTotalWorkingDays.Text.ToString());
                                else
                                    indCalculatedAmount = Convert.ToDecimal(dc.Cells["ActualAmount"].Value.ToString()).RoundUp();// * Convert.ToDecimal(txtTotalWorkingDays.Text.ToString()) / Convert.ToDecimal(txtTotalWorkingDays.Text.ToString());
                            }
                            else
                            {
                                if (Convert.ToDecimal(txtTotalWorkingDays.Text.ToString()) > 0)
                                {
                                    if (tmpObj.ProrataBasis)
                                        indCalculatedAmount = Convert.ToDecimal(Convert.ToDecimal(dc.Cells["ActualAmount"].Value.ToString()).RoundUp() * Convert.ToDecimal(txtTotalPayableDays.Text.ToString()) / Convert.ToDecimal(txtTotalWorkingDays.Text.ToString())).RoundUp();
                                    else
                                        indCalculatedAmount = Convert.ToDecimal(dc.Cells["ActualAmount"].Value.ToString()).RoundUp();// * Convert.ToDecimal(txtTotalWorkingDays.Text.ToString()) / Convert.ToDecimal(txtTotalWorkingDays.Text.ToString());
                                }
                                else
                                    indCalculatedAmount = 0;
                            }
                            if (headerName.ToString() == "Basic Salary")
                            {
                                if (tmpObj.ProrataBasis)
                                    indCalculatedAmount = Convert.ToDecimal(Convert.ToDecimal(lblBasicSalary.Text.ToString()).RoundUp() * Convert.ToDecimal(txtTotalPayableDays.Text.ToString()) / Convert.ToDecimal(txtTotalWorkingDays.Text.ToString())).RoundUp();
                                else
                                    indCalculatedAmount = Convert.ToDecimal(lblBasicSalary.Text.ToString()).RoundUp();// * Convert.ToDecimal(txtTotalWorkingDays.Text.ToString()) / Convert.ToDecimal(txtTotalWorkingDays.Text.ToString());
                            }
                        }

                        if (headerName.ToString() == "Basic Salary")
                        {
                            amount = indCalculatedAmount.ToString();
                        }
                        else
                            amount = indCalculatedAmount.ToString();

                        if (lblActionMode.Text == "add" && formula.IndexOf("NumOfHours") > 0)
                        {
                            formula = formula.Replace("OverTime Allowance1", Convert.ToString(Convert.ToDecimal(lblBasicSalaryPerHour.Text.Trim()) * Convert.ToDecimal(objAppSettings.GetSpecificAppSettingsInfo("Overtime Pay Rate").AppSettingValue.ToString())));
                            if (Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()).RoundUp() > 0 && Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()).RoundUp() < 50)
                                formula = formula.Replace("NumOfHours", Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()).ToString());
                            else
                                formula = Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()).RoundUp().ToString();
                        }
                        else if (lblActionMode.Text == "modify" && formula.IndexOf("NumOfHours") > 0)
                        {
                            formula = formula.Replace("OverTime Allowance1", lblBasicSalaryPerHour.Text.Trim());
                            if (Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()).RoundUp() > 0 && Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()).RoundUp() < 50)
                                formula = formula.Replace("NumOfHours", dc.Cells["AllowanceAmount"].Value.ToString());
                            else
                                formula = Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()).RoundUp().ToString(); // (Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()) / Convert.ToDecimal(lblBasicSalaryPerHour.Text.ToString())).RoundUp().ToString();
                        }
                        else
                        {
                            if (Convert.ToDecimal(amount) >= 0)
                                formula = formula.Replace(headerName, amount.ToString());
                            else if (Convert.ToDecimal(amount) < 0)
                                formula = formula.Replace(headerName, lblBasicSalaryPerHour.Text.ToString());

                            if (tmpObj.ProrataBasis == true)
                                formula = formula.Replace("TotalPayableDays", txtTotalPayableDays.Text.Trim());
                            else if (tmpObj.ProrataBasis == false)
                                formula = formula.Replace("TotalPayableDays", txtTotalWorkingDays.Text.Trim());
                        }
                        formula = formula.Replace("TotalWorkingDays", txtTotalWorkingDays.Text.Trim());

                        DataTable dt1 = new DataTable();
                        if (formula != "")
                        {
                            if (dc.Cells["HeaderTitle"].Value.ToString() == headerName)
                                dc.Cells["AllowanceAmount"].Value = Convert.ToDecimal(dt1.Compute(formula, "")).RoundUp();
                            else
                                dc.Cells["AllowanceAmount"].Value = Convert.ToDecimal(dt1.Compute(formula, "")).RoundUp();
                        }
                    }
                    else
                    {
                        if (tmpObj.IsFixed)
                        {
                            if (tmpObj.ProrataBasis)
                                indCalculatedAmount = Convert.ToDecimal(Convert.ToDecimal(dc.Cells["ActualAmount"].Value.ToString()).RoundUp() * Convert.ToDecimal(txtTotalPayableDays.Text.ToString()).RoundUp() / Convert.ToDecimal(txtTotalWorkingDays.Text.ToString()).RoundUp()).RoundUp();
                            else
                                indCalculatedAmount = Convert.ToDecimal(dc.Cells["ActualAmount"].Value.ToString()).RoundUp();
                        }
                        else
                        {
                            if (tmpObj.ProrataBasis)
                                indCalculatedAmount = Convert.ToDecimal(dc.Cells["ActualAmount"].Value.ToString()).RoundUp() * Convert.ToDecimal(txtTotalPayableDays.Text.ToString()) / Convert.ToDecimal(txtTotalWorkingDays.Text.ToString()).RoundUp();
                            else
                                indCalculatedAmount = Convert.ToDecimal(dc.Cells["ActualAmount"].Value.ToString()).RoundUp();
                        }
                        dc.Cells["AllowanceAmount"].Value = indCalculatedAmount.RoundUp();
                    }
                }
                else if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "deductions")
                {
                    dc.Cells["AllowanceAmount"].ReadOnly = true;
                    dc.Cells["DeductionAmount"].ReadOnly = false;
                    dc.Cells["ReimbursmentAmount"].ReadOnly = true;

                    if (dc.Cells["CalcFormula"].Value.ToString().ToLower() == "computepf1")
                    {
                        if (chkAutoCalculate.Checked == true)
                        {
                            ClientStatutory selectedClientStatutory = objClientStatutory.getClientStatutory(Convert.ToInt16(objTempClientFinYearInfo.ClientID));
                            EmpPersonalPersonalInfo objSelectedPersonalInfo = objEmployeePersonalInfo.GetEmpPersonalPersonalInfo(Convert.ToInt16(lblReportingManagerID.Text));
                            EmpPersonalIDInfo objEmpPersonalIDInfo = objEmployeePersonalIDInfo.GetEmpPersonalIDInfo(Convert.ToInt16(objSelectedPersonalInfo.PersonalInfoID.ToString()));
                            if (objEmpPersonalIDInfo.PFApplicable == false)
                            {
                                dc.Cells["DeductionAmount"].Value = "0.00";
                            }
                            else
                            {
                                if (selectedClientStatutory.EnableClientStatutory)
                                {
                                    if (selectedClientStatutory.EnablePF == true)
                                    {
                                        ProvidentFund objProvidentFund = objClientStatutory.GetCompanyProvidentFundSettings(Convert.ToInt16(objTempClientFinYearInfo.ClientID));

                                        string[] headers = null;
                                        if (Convert.ToDecimal(lblBasicSalary.Text.ToString()) > Convert.ToDecimal(objProvidentFund.MaxPFAmount))
                                        {
                                            headers = new[] { "Basic Salary", "Dearness Allowance" };
                                        }
                                        else if (Convert.ToDecimal(lblBasicSalary.Text.ToString()) <= Convert.ToDecimal(objProvidentFund.MaxPFAmount))
                                        {
                                            headers = new[] { "Basic Salary" };
                                        }

                                        basicSalary = dtgSalaryDetails1.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == "Basic Salary").Select(r => Convert.ToDecimal(r.Cells["AllowanceAmount"].Value ?? 0)).FirstOrDefault();
                                        //lblBasicSalary.Text = basicSalary.ToString("#,#0.00");
                                        decimal total = dtgSalaryDetails1.Rows.Cast<DataGridViewRow>().Where(r => headers.Contains(r.Cells["HeaderTitle"].Value?.ToString())).Sum(r => Convert.ToDecimal(r.Cells["AllowanceAmount"].Value ?? 0));

                                        if (Convert.ToDecimal(lblBasicSalary.Text.ToString()) > Convert.ToDecimal(objProvidentFund.MaxPFAmount))
                                        {
                                            total = Convert.ToDecimal(objProvidentFund.MaxPFAmount);
                                        }

                                        if (objProvidentFund.EmpPFPercentageOrAmount.ToString().ToUpper() == "P")
                                        {
                                            dc.Cells["DeductionAmount"].Value = Math.Round(total * Convert.ToDecimal(objProvidentFund.EmpPFPercentage.ToString()) / 100, 2); //total
                                            string strPFTooltip = "";
                                            if (objProvidentFund.EmprPFPercentageOrAmount.ToString().ToUpper() == "P")
                                            {
                                                strPFTooltip = "Employer Contribution\nTo EPF Account : [@" + Convert.ToDecimal(objProvidentFund.EmprPFPercentage.ToString()) + "% ] " + (Math.Round(total * Convert.ToDecimal(objProvidentFund.EmprPFPercentage.ToString()) / 100, 2)).ToString() + " + " + Convert.ToDecimal(dc.Cells["DeductionAmount"].Value).ToString() + " = " + Math.Round(Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString()) + Convert.ToDecimal((total * Convert.ToDecimal(objProvidentFund.EmprPFPercentage.ToString()) / 100).ToString()), 2);
                                                dc.Cells["HeaderTitle"].ToolTipText = strPFTooltip;
                                            }
                                            else
                                            {
                                                dc.Cells["HeaderTitle"].ToolTipText = objProvidentFund.EmprPFAmount.ToString();
                                            }
                                            if (objProvidentFund.EmprPSPercentageOrAmount.ToString().ToUpper() == "P")
                                            {
                                                if (strPFTooltip != "")
                                                    strPFTooltip = "Employer Contribution\nTo EPF Account : [@" + Convert.ToDecimal(objProvidentFund.EmprPFPercentage.ToString()) + "% ] " + (Math.Round(total * Convert.ToDecimal(objProvidentFund.EmprPFPercentage.ToString()) / 100, 2)).ToString() + " + " + Convert.ToDecimal(dc.Cells["DeductionAmount"].Value).ToString() + " = " + Math.Round(Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString()) + Convert.ToDecimal((total * Convert.ToDecimal(objProvidentFund.EmprPFPercentage.ToString()) / 100).ToString()), 2) + "\nTo NPS Account : [@" + Convert.ToDecimal(objProvidentFund.EmprPSPercentage.ToString()) + " % ] " + Math.Round(total * Convert.ToDecimal(objProvidentFund.EmprPSPercentage.ToString()) / 100, 2).ToString();
                                                else
                                                    strPFTooltip = "To NPS Account : " + (Math.Round(total * Convert.ToDecimal(objProvidentFund.EmprPSPercentage.ToString()) / 100, 2)).ToString();
                                                dc.Cells["HeaderTitle"].ToolTipText = strPFTooltip;
                                            }
                                            else
                                            {
                                                dc.Cells["HeaderTitle"].ToolTipText = objProvidentFund.EmprPFAmount.ToString();
                                            }
                                        }
                                        else if (objProvidentFund.EmpPFPercentageOrAmount.ToString().ToUpper() == "A")
                                        {
                                            dc.Cells["DeductionAmount"].Value = Convert.ToDecimal(objProvidentFund.EmpPFAmount.ToString()).RoundUp();
                                        }
                                    }
                                    else
                                    {
                                        dc.Cells["DeductionAmount"].Value = "0.00";
                                    }
                                }
                                else
                                {
                                    dc.Cells["DeductionAmount"].Value = "0.00";
                                }
                            }
                        }
                    }
                    else if (dc.Cells["CalcFormula"].Value.ToString().ToLower() == "computetax1")
                    {
                        if (chkAutoCalculate.Checked == true)
                        {
                            ClientStatutory selectedClientStatutory = objClientStatutory.getClientStatutory(Convert.ToInt16(objTempClientFinYearInfo.ClientID));
                            if (selectedClientStatutory.EnableClientStatutory)
                            {
                                if (selectedClientStatutory.EnablePT == true)
                                {
                                    string[] months = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
                                    int MonthNumber = months.ToList().FindIndex(s => s == cmbSalaryMonth.Text.ToString().Substring(0, 3).ToUpper()) + 1;
                                    dc.Cells["DeductionAmount"].Value = objProfessionalTaxCalculation.CalculateProfessionalTax(2, Convert.ToInt32(lblStateID.Text), totalAallowences, MonthNumber, Convert.ToInt32(lblSexID.Text));
                                    dc.Cells["DeductionAmount"].Value = Convert.ToDecimal(dc.Cells["DeductionAmount"].Value).RoundUp();
                                }
                                else
                                    dc.Cells["DeductionAmount"].Value = "0.00";
                            }
                            else
                            {
                                dc.Cells["DeductionAmount"].Value = "0.00";
                            }
                        }
                    }
                    else if (dc.Cells["CalcFormula"].Value.ToString().ToLower() == "computeesi1")
                    {
                        if (chkAutoCalculate.Checked == true)
                        {
                            ClientStatutory selectedClientStatutory = objClientStatutory.getClientStatutory(Convert.ToInt16(objTempClientFinYearInfo.ClientID));
                            EmpPersonalPersonalInfo objSelectedPersonalInfo = objEmployeePersonalInfo.GetEmpPersonalPersonalInfo(Convert.ToInt16(lblReportingManagerID.Text));
                            EmpPersonalIDInfo objEmpPersonalIDInfo = objEmployeePersonalIDInfo.GetEmpPersonalIDInfo(Convert.ToInt16(objSelectedPersonalInfo.PersonalInfoID.ToString()));
                            if (objEmpPersonalIDInfo.ESIApplicable == false)
                            {
                                dc.Cells["DeductionAmount"].Value = "0.00";
                            }
                            else
                            {
                                if (selectedClientStatutory.EnableClientStatutory)
                                {
                                    if (selectedClientStatutory.EnableESI == true)
                                    {
                                        ESIModel objESIModel = objClientStatutory.GetCompanyESISettings(Convert.ToInt16(objTempClientFinYearInfo.ClientID));

                                        decimal total = Convert.ToDecimal(txtAallowences.Text.ToString());

                                        if (Convert.ToDecimal(total) <= Convert.ToDecimal(objESIModel.MaxESIAmount))
                                        {
                                            total = Convert.ToDecimal(objESIModel.MaxESIAmount);
                                            if (objESIModel.EmpESIPercentageOrAmount.ToString().ToUpper() == "P")
                                            {
                                                dc.Cells["DeductionAmount"].Value = Math.Round(total * Convert.ToDecimal(objESIModel.EmpESIPercentage.ToString()) / 100, 2); //total
                                            }
                                            else if (objESIModel.EmpESIPercentageOrAmount.ToString().ToUpper() == "A")
                                            {
                                                dc.Cells["DeductionAmount"].Value = Convert.ToDecimal(objESIModel.EmprESIAmount.ToString()).RoundUp();
                                            }
                                        }
                                        else
                                        {
                                            dc.Cells["DeductionAmount"].Value = "0.00";
                                        }
                                    }
                                    else
                                    {
                                        dc.Cells["DeductionAmount"].Value = "0.00";
                                    }
                                }
                                else
                                {
                                    dc.Cells["DeductionAmount"].Value = "0.00";
                                }
                            }
                        }
                    }
                }
                else if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "reimbursement")
                {
                    dc.Cells["AllowanceAmount"].ReadOnly = true;
                    dc.Cells["DeductionAmount"].ReadOnly = true;
                    dc.Cells["ReimbursmentAmount"].ReadOnly = false;
                }
            }

            totalAallowences = 0;
            totalDeductions = 0;
            totalReimbursement = 0;

            //if (tabAdvanceHeaders.Enabled == true)
            //{
            //    foreach (DataGridViewRow dc in dtgAdvanceDetails.Rows)
            //    {
            //        if (Convert.ToBoolean(dc.Cells["Select"].Value.ToString()) == true)
            //        {
            //            totalDeductions = totalDeductions + Convert.ToDecimal(dc.Cells["RePaymentBalance"].Value.ToString());
            //        }
            //    }
            //}

            foreach (DataGridViewRow dc in dtgSalaryDetails1.Rows)
            {
                totalAallowences = totalAallowences + Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString());
                totalDeductions = totalDeductions + Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString());
                totalReimbursement = totalReimbursement + Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString());
            }

            dtgSalaryDetails1.Columns["AllowanceAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgSalaryDetails1.Columns["AllowanceAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryDetails1.Columns["DeductionAmount"].Width = 135;
            dtgSalaryDetails1.Columns["ReimbursmentAmount"].Width = 135;
            dtgSalaryDetails1.Columns["DeductionAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Deductions
            dtgSalaryDetails1.Columns["DeductionAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryDetails1.Columns["ReimbursmentAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Reimbursments
            dtgSalaryDetails1.Columns["ReimbursmentAmount"].DefaultCellStyle.Format = "c2";

            txtAallowences.Text = Convert.ToDecimal(totalAallowences.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtDeductions.Text = Convert.ToDecimal(totalDeductions.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtReimbursement.Text = Convert.ToDecimal(totalReimbursement.ToString()).ToString("0.00", CultureInfo.InvariantCulture);

            txtNetPayable.Text = Convert.ToDecimal((totalAallowences + totalReimbursement) - totalDeductions).ToString();
            txtNetPayable.Text = Convert.ToDecimal(txtNetPayable.Text.ToString()).ToString("0.00", CultureInfo.InvariantCulture);

            basicSalary = dtgSalaryDetails1.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == "Basic Salary").Select(r => Convert.ToDecimal(r.Cells["AllowanceAmount"].Value ?? 0)).FirstOrDefault();
            lblBasicSalary.Text = basicSalary.RoundUp().ToString("#,#0.00");
            lblBasicSalaryPerDay.Text = Convert.ToDecimal(basicSalary / Convert.ToDecimal(txtTotalWorkingDays.Text.ToString())).RoundUp().ToString("#,#0.00");
            lblBasicSalaryPerHour.Text = Convert.ToDecimal(Convert.ToDecimal(lblBasicSalaryPerDay.Text.ToString()) / Convert.ToDecimal("8.0")).RoundUp().ToString("#,#0.00");
            ////////////////
        }

        private void chkSelectUnSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectUnSelect.Checked == true)
            {
                SelectAllRows();
            }
            else if (chkSelectUnSelect.Checked == false)
            {
                UnselectAllRows();
            }
        }

        private void dtgSalaryDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dtgSalaryDetails.Columns[e.ColumnIndex].Name == "SelectRow")
            {
                bool selected = Convert.ToBoolean(dtgSalaryDetails.Rows[e.RowIndex].Cells["SelectRow"].Value);

                dtgSalaryDetails.Rows[e.RowIndex].DefaultCellStyle.BackColor = selected ? System.Drawing.Color.LightGreen : System.Drawing.Color.Transparent;
            }
        }

        private void dtgSalaryDetails_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dtgSalaryDetails.IsCurrentCellDirty)
            {
                dtgSalaryDetails.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void chkFlipOnOff_Click(object sender, EventArgs e)
        {
            if (chkFlipOnOff.Checked == true)
            {
                chkFlipOnOff.BackgroundImage = Properties.Resources.flip1;
            }
            else
            {
                chkFlipOnOff.BackgroundImage = Properties.Resources.flip;
            }

            chkFlipOnOff.Refresh();

            foreach (DataGridViewRow row in dtgSalaryDetails.Rows)
            {
                // Skip new row and Total row
                if (row.IsNewRow || row.Index == dtgSalaryDetails.Rows.Count - 1)
                    continue;

                if (row.Cells["SelectRow"].ReadOnly == true)
                {
                    dtgSalaryDetails.Rows[row.Index].DefaultCellStyle.BackColor = System.Drawing.Color.Red;
                    continue;
                }

                bool currentValue = false;

                if (row.Cells["SelectRow"].Value != null)
                    currentValue = Convert.ToBoolean(row.Cells["SelectRow"].Value);

                row.Cells["SelectRow"].Value = !currentValue;
            }
        }

        private void chkEmpWithPendingAdvances_CheckedChanged(object sender, EventArgs e)
        {
            CalculateSalarySummary();
        }

        private void chkExcludeEmpWithPendingAdvances_Click(object sender, EventArgs e)
        {
            if(chkExcludeEmpWithPendingAdvances.Checked == true)
            {
                //chkExcludeEmpWithPendingAdvances.Values.Image = Properties.Resources.check_16x16;
                chkExcludeEmpWithPendingAdvances.Text = "☑ Show Salary Summary including employees who have Pending Advances";
            }
            else
            {
                //chkExcludeEmpWithPendingAdvances.Values.Image = Properties.Resources.uncheck;
                chkExcludeEmpWithPendingAdvances.Text = "☐ Show Salary Summary excluding employees who have Pending Advances";
            }
            CalculateSalarySummary();
        }

        private void dtgSalaryDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            //if (dtgSalaryDetails.Columns[e.ColumnIndex].Name == "EmpCode")
            {
                int selectedRowIndex = e.RowIndex;
                int SelectedEmpID = Convert.ToInt32(dtgSalaryDetails.Rows[e.RowIndex].Cells["EmpID"].Value);                

                frmIndPayrollMaster frmIndPayrollMaster = new frmIndPayrollMaster("listPayrollUsersList", objTempCurrentlyLoggedInUserInfo, objTempClientFinYearInfo, this, Convert.ToInt32(SelectedEmpID), cmbSalaryMonth.Text.ToString(), Convert.ToDateTime(dtSalaryDate.Value.ToString()), false);
                if (frmIndPayrollMaster.ShowDialog() == DialogResult.OK)
                {
                    UpdateSalaryRow(selectedRowIndex, frmIndPayrollMaster.UpdatedSalaryValues);
                }

                DataGridViewRow totalRow = new DataGridViewRow();
                totalRow = dtgSalaryDetails.Rows[dtgSalaryDetails.Rows.Count - 1];

                totalRow.Cells[7].Value = "Total";
                for (int col = 8; col < dtgSalaryDetails.Columns.Count; col++) // skip employee info columns
                {
                    decimal sum = 0;

                    foreach (DataGridViewRow row in dtgSalaryDetails.Rows)
                    {
                        if (row.Index == totalRow.Index)
                            continue;

                        if (row.IsNewRow) continue;

                        decimal value = 0;
                        decimal.TryParse(Convert.ToString(row.Cells[col].Value), out value);

                        sum += value;
                    }

                    totalRow.Cells[col].Value = sum;
                }
                FormatDataGridView();
                CalculateSalarySummary();
            }
        }

        private void UpdateSalaryRow(int rowIndex, Dictionary<string, decimal> salaryValues)
        {
            DataGridViewRow row = dtgSalaryDetails.Rows[rowIndex];

            foreach (var item in salaryValues)
            {
                string header = item.Key;
                decimal value = item.Value;

                if (dtgSalaryDetails.Columns.Contains(header))
                {
                    row.Cells[header].Value = value;
                }
            }
        }

        private void dtgSalaryDetails_Paint(object sender, PaintEventArgs e)
        {
            KryptonDataGridView dgv = sender as KryptonDataGridView;

            if (dgv.Rows.Count == 0)
            {
                string message = "No Data Available";

                using (System.Drawing.Font font = new System.Drawing.Font("Segoe UI", 12, FontStyle.Bold))
                {
                    SizeF size = e.Graphics.MeasureString(message, font);

                    e.Graphics.DrawString(message, font, System.Drawing.Brushes.Gray, (dgv.Width - size.Width) / 2, (dgv.Height - size.Height) / 2);
                }
            }
        }
    }
}
