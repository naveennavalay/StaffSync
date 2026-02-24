using Common;
using DocumentFormat.OpenXml.Office2016.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Humanizer;
using Microsoft.Office.Core;
using ModelStaffSync;
using Quartz.Impl.AdoJobStore.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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
    public partial class frmPayrollMaster : Form
    {
        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        DALStaffSync.clsEmployeeMaster objEmployeeMaster = new DALStaffSync.clsEmployeeMaster();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        DALStaffSync.clsAuditLog objAuditLog = new DALStaffSync.clsAuditLog();
        clsImpageOperation objImpageOperation = new clsImpageOperation();
        DALStaffSync.clsEmployeePersonalIDInfo objEmployeePersonalIDInfo = new DALStaffSync.clsEmployeePersonalIDInfo();
        DALStaffSync.clsEmployeePersonalInfo objEmployeePersonalInfo = new DALStaffSync.clsEmployeePersonalInfo();
        DALStaffSync.clsPhotoMas objPhotoMas = new DALStaffSync.clsPhotoMas();
        DALStaffSync.clsSalaryProfile objSalaryProfile = new DALStaffSync.clsSalaryProfile();
        DALStaffSync.clsAdvanceTypeMas objAdvanceTypesModel = new DALStaffSync.clsAdvanceTypeMas();
        DALStaffSync.clsAdvanceTypeConfigInfo objAdvanceTypeConfigInfo = new DALStaffSync.clsAdvanceTypeConfigInfo();
        DALStaffSync.clsAdvanceTransaction objAdvanceTransaction = new DALStaffSync.clsAdvanceTransaction();
        DALStaffSync.clsAllowenceInfo objAllowenceInfo = new DALStaffSync.clsAllowenceInfo();
        DALStaffSync.clsDeductionsInfo objDeductionsInfo = new DALStaffSync.clsDeductionsInfo();
        DALStaffSync.clsReimbursement objReimbursement = new DALStaffSync.clsReimbursement();
        DALStaffSync.clsEmpPayroll objEmployeePayroll = new DALStaffSync.clsEmpPayroll();
        DALStaffSync.clsClientStatutory objClientStatutory = new DALStaffSync.clsClientStatutory();
        DALStaffSync.clsAttendanceMas objAttendanceMas = new DALStaffSync.clsAttendanceMas();
        DALStaffSync.clsProvidentFundCalculation objProvidentFundCalculation = new DALStaffSync.clsProvidentFundCalculation();
        DALStaffSync.clsProfessionalTaxCalculation objProfessionalTaxCalculation = new DALStaffSync.clsProfessionalTaxCalculation();
        frmDashboard objDashboard = (frmDashboard) System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();
        frmEmpAdvanceRepayment frmEmpAdvanceRepayment = null;

        public frmPayrollMaster()
        {
            InitializeComponent();
        }

        public frmPayrollMaster(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmPayrollMaster(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
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

        private bool ValidateValuesOnUI()
        {
            bool validationStatus = true;
            errValidator.Clear();

            // Helper for date validation
            DateTime dos;
            string dateFormat = "dd-MM-yyyy";
            CultureInfo provider = CultureInfo.InvariantCulture;
            if (lblReportingManagerID.Text.Trim() == "")
            {
                errValidator.SetError(txtRepEmpCode, "Please select the employee");
                txtRepEmpCode.Focus();
                validationStatus = false;
            }
            else
            {
                errValidator.SetError(txtRepEmpCode, "");
            }

            if (cmbSalaryMonth.Text.Trim() == "")
            {
                errValidator.SetError(cmbSalaryMonth, "Please select the salary month");
                cmbSalaryMonth.Focus();
                validationStatus = false;
            }
            else
            {
                errValidator.SetError(cmbSalaryMonth, "");
            }

            // Date of Birth
            if (string.IsNullOrEmpty(txtSalaryDate.Text))
            {
                validationStatus = false;
                txtSalaryDate.Focus();
                errValidator.SetError(this.txtSalaryDate, txtSalaryDate.Tag?.ToString() ?? "Date of Salary is required.");
            }
            else if (!DateTime.TryParseExact(txtSalaryDate.Text, dateFormat, provider, DateTimeStyles.None, out dos))
            {
                validationStatus = false;
                txtSalaryDate.Focus();
                errValidator.SetError(this.txtSalaryDate, "Invalid Date of Salary format (dd-MM-yyyy).");
            }
            else if (dos > DateTime.Now.Date)
            {
                validationStatus = false;
                txtSalaryDate.Focus();
                errValidator.SetError(this.txtSalaryDate, "Date of Salary cannot be in the future.");
            }
            else
            {
                errValidator.SetError(this.txtSalaryDate, "");
            }

            if (txtTotalWorkingDays.Text.Trim() == "" || Convert.ToDecimal(txtTotalWorkingDays.Text.Trim()) == 0)
            {
                errValidator.SetError(txtTotalWorkingDays, "Please enter the total working days");
                txtTotalWorkingDays.Focus();
                validationStatus = false;
            }
            else
            {
                errValidator.SetError(txtTotalWorkingDays, "");
            }

            if (lblActionMode.Text == "add" && objEmployeePayroll.IsSalaryAlreadyProcessed(Convert.ToInt32(lblReportingManagerID.Text.Trim()), Convert.ToDateTime(txtSalaryDate.Text), cmbSalaryMonth.Text.Trim()))
            {
                txtSalaryDate.Focus();
                errValidator.SetError(cmbSalaryMonth, "The salary has been processed already.");
                validationStatus = false;
            }

            if (tabAdvanceHeaders.Enabled == true)
            {
                foreach (DataGridViewRow dc in dtgAdvanceDetails.Rows)
                {
                    //AdvanceTypesModel tmpAdvanceTypesModel = objAdvanceTypesModel.GetAdvanceTypeConfigByID(txtAdvanceTypeID);

                    int AdvanceRequestID = Convert.ToInt32(dc.Cells["EmpAdvanceRequestID"].Value);
                    if (Convert.ToBoolean(dc.Cells["Select"].Value) == true)
                    {
                        var cell = dc.Cells["RePaymentBalance"];
                        if (Convert.ToDecimal(dc.Cells["CBalance"].Value) - Convert.ToDecimal(dc.Cells["RePaymentBalance"].Value) < 0)
                        {
                            tabControl1.SelectedIndex = 1;
                            dtgAdvanceDetails.ClearSelection();
                            dc.Selected = true;
                            dtgAdvanceDetails.CurrentCell = cell;
                            dtgAdvanceDetails.FirstDisplayedScrollingRowIndex = dc.Index;
                            dtgAdvanceDetails.BeginEdit(true);

                            MessageBox.Show("Re-pay Advance Amount cannot be greater than Closing Balance.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //errValidator.SetError(txtAdvanceTRAmount, "Re-pay Advance Amount cannot be greater than Closing Balance.");
                            validationStatus = false;
                        }
                        else if (Convert.ToDecimal(dc.Cells["CBalance"].Value) - Convert.ToDecimal(dc.Cells["RePaymentBalance"].Value) == 0)
                        {
                            //if (MessageBox.Show("The " + txtAdvanceType.Text + " " + Convert.ToDecimal(txtAdvanceAmount.Text.ToString()).ToString("#,##0.00") + " will get completely recovered.\nPlease make a note and notify the Employee.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                            //{

                            //}
                        }
                        else if (Convert.ToDecimal(dc.Cells["RePaymentBalance"].Value) < 0)
                        {
                            tabControl1.SelectedIndex = 1;
                            dtgAdvanceDetails.ClearSelection();
                            dc.Selected = true;
                            dtgAdvanceDetails.CurrentCell = cell;
                            dtgAdvanceDetails.FirstDisplayedScrollingRowIndex = dc.Index;
                            dtgAdvanceDetails.BeginEdit(true);

                            MessageBox.Show("Re-pay Advance Amount cannot be negative.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //errValidator.SetError(txtAdvanceTRAmount, "Re-pay Advance Amount cannot be negative.");
                            validationStatus = false;
                        }
                        else if (Convert.ToDecimal(dc.Cells["RePaymentBalance"].Value) > Convert.ToDecimal(dc.Cells["AdvanceInstallment"].Value))
                        {
                            tabControl1.SelectedIndex = 1;
                            dtgAdvanceDetails.ClearSelection();
                            dc.Selected = true;
                            dtgAdvanceDetails.CurrentCell = cell;
                            dtgAdvanceDetails.FirstDisplayedScrollingRowIndex = dc.Index;
                            dtgAdvanceDetails.BeginEdit(true);

                            MessageBox.Show("Re-pay Advance Amount cannot be greater than Installment Amount.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //errValidator.SetError(txtAdvanceTRAmount, "Re-pay Advance Amount cannot be greater than Installment Amount.");
                            validationStatus = false;
                        }
                        else if (Convert.ToDecimal(dc.Cells["RePaymentBalance"].Value) > Convert.ToDecimal(dc.Cells["CBalance"].Value))
                        {
                            tabControl1.SelectedIndex = 1;
                            dtgAdvanceDetails.ClearSelection();
                            dc.Selected = true;
                            dtgAdvanceDetails.CurrentCell = cell;
                            dtgAdvanceDetails.FirstDisplayedScrollingRowIndex = dc.Index;
                            dtgAdvanceDetails.BeginEdit(true);

                            MessageBox.Show("Re-pay Advance Amount cannot be greater than Closing Balance.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                            //errValidator.SetError(txtAdvanceTRAmount, "Re-pay Advance Amount cannot be greater than Closing Balance.");
                            validationStatus = false;
                        }
                        else if (Convert.ToDecimal(dc.Cells["RePaymentBalance"].Value) == 0)
                        {
                            tabControl1.SelectedIndex = 1;
                            dtgAdvanceDetails.ClearSelection();
                            dc.Selected = true;
                            dtgAdvanceDetails.CurrentCell = cell;
                            dtgAdvanceDetails.FirstDisplayedScrollingRowIndex = dc.Index;
                            dtgAdvanceDetails.BeginEdit(true);

                            MessageBox.Show("Re-pay Advance Amount cannot be zero.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //errValidator.SetError(txtAdvanceTRAmount, "Re-pay Advance Amount cannot be zero.");
                            validationStatus = false;
                        }
                        else
                        {
                            //errValidator.SetError(txtAdvanceTRAmount, "");
                        }
                    }
                }
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

            if (lblActionMode.Text == "add")
            {
                empSalaryID = objEmployeePayroll.InsertEmployeeSalaryMasterInfo(Convert.ToInt16(lblReportingManagerID.Text.Trim()), Convert.ToDateTime(txtSalaryDate.Text), cmbSalaryMonth.Text, Convert.ToDecimal(txtTotalWorkingDays.Text).RoundUp(), Convert.ToDecimal(txtTotalWorkedDays.Text).RoundUp(), Convert.ToDecimal(txtLeaveDays.Text).RoundUp(), Convert.ToDecimal(txtUnpaidLeaves.Text).RoundUp(), Convert.ToDecimal(txtTotalPayableDays.Text), Convert.ToDecimal(txtAallowences.Text).RoundUp(), Convert.ToDecimal(txtDeductions.Text).RoundUp(), Convert.ToDecimal(txtReimbursement.Text).RoundUp(), Convert.ToDecimal(txtNetPayable.Text).RoundUp(), false);
                foreach (DataGridViewRow dc in dtgSalaryDetails.Rows)
                {
                    int EmpSalDetID = objEmployeePayroll.InsertEmployeeSalaryDetailsInfo(Convert.ToInt16(empSalaryID), Convert.ToInt16(dc.Cells["SalProDetID"].Value.ToString()), Convert.ToInt16(dc.Cells["HeaderID"].Value.ToString()), dc.Cells["HeaderTitle"].Value.ToString(), dc.Cells["HeaderType"].Value.ToString(), dc.Cells["CalcFormula"].Value.ToString(), Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()).RoundUp(), Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString()).RoundUp(), Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString()).RoundUp(), iRowCounter);
                    iRowCounter = iRowCounter + 1;
                }

                if (tabAdvanceHeaders.Enabled == true)
                {
                    foreach (DataGridViewRow dc in dtgAdvanceDetails.Rows)
                    {
                        //AdvanceTypesModel tmpAdvanceTypesModel = objAdvanceTypesModel.GetAdvanceTypeConfigByID(txtAdvanceTypeID);

                        int AdvanceRequestID = Convert.ToInt32(dc.Cells["EmpAdvanceRequestID"].Value);
                        if(Convert.ToBoolean(dc.Cells["Select"].Value) == true)
                        {
                            int newTransactionID = objAdvanceTransaction.InsertAdvanceTransaction(dc.Cells["EmpAdvReqCode"].Value.ToString(), Convert.ToInt32(AdvanceRequestID), Convert.ToDateTime(DateTime.Today.Date), Convert.ToDecimal(dc.Cells["CBalance"].Value.ToString()).RoundUp(), 0, Convert.ToDecimal(dc.Cells["RePaymentBalance"].Value.ToString()).RoundUp(), Convert.ToDecimal(dc.Cells["CBalance"].Value.ToString()).RoundUp() - Convert.ToDecimal(dc.Cells["RePaymentBalance"].Value.ToString()).RoundUp(), "Dr", "Via Deduction in Salary", empSalaryID);
                            objAuditLog.InsertAuditLog(Convert.ToInt32(lblReportingManagerID.Text.ToString()), Convert.ToInt32(empSalaryID), "To " + dc.Cells["AdvanceTypeTitle"].Value.ToString() + " Repayment via Salary Deduction", ModelStaffSync.CurrentUser.EmpName, "SalaryToAdvanceRepayment");
                            if (Convert.ToDecimal(dc.Cells["CBalance"].Value.ToString()) - Convert.ToDecimal(dc.Cells["RePaymentBalance"].Value.ToString()) == 0)
                            {
                                objAdvanceTransaction.CloseEmployeeSpecificAdvanceRequest(Convert.ToInt32(AdvanceRequestID.ToString()));
                                objAuditLog.InsertAuditLog(Convert.ToInt32(lblReportingManagerID.Text.ToString()), Convert.ToInt32(empSalaryID), "\"" + dc.Cells["AdvanceTypeTitle"].Value.ToString() + "\" completely recovered from Employee : \"" + txtRepEmpName.Text + " [ EmpCode : " + txtRepEmpCode.Text.ToString() + " ] \" via Salary Deduction" , ModelStaffSync.CurrentUser.EmpName, "SalaryToAdvanceRepayment");
                            }
                        }
                    }
                }

                if (empSalaryID > 0)
                    MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (lblActionMode.Text == "modify")
            {
                empSalaryID = objEmployeePayroll.UpdateEmployeeSalaryMasterInfo(Convert.ToInt16(lblSelectedMonthSalaryID.Text.Trim()), Convert.ToInt16(lblReportingManagerID.Text.Trim()), Convert.ToDateTime(txtSalaryDate.Text), cmbSalaryMonth.Text, Convert.ToDecimal(txtTotalWorkingDays.Text), Convert.ToDecimal(txtTotalWorkedDays.Text), Convert.ToDecimal(txtLeaveDays.Text), Convert.ToDecimal(txtUnpaidLeaves.Text), Convert.ToDecimal(txtTotalPayableDays.Text), Convert.ToDecimal(txtAallowences.Text).RoundUp(), Convert.ToDecimal(txtDeductions.Text).RoundUp(), Convert.ToDecimal(txtReimbursement.Text).RoundUp(), Convert.ToDecimal(txtNetPayable.Text).RoundUp(), false);
                foreach (DataGridViewRow dc in dtgSalaryDetails.Rows)
                {
                    int EmpSalDetID = objEmployeePayroll.UpdateEmployeeSalaryDetailsInfo(Convert.ToInt16(dc.Cells["EmpSalDetID"].Value.ToString()), Convert.ToInt16(lblSelectedMonthSalaryID.Text.ToString()), Convert.ToInt16(dc.Cells["SalProDetID"].Value.ToString()), Convert.ToInt16(dc.Cells["HeaderID"].Value.ToString()), dc.Cells["HeaderTitle"].Value.ToString(), dc.Cells["HeaderType"].Value.ToString(), dc.Cells["CalcFormula"].Value.ToString(), Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()).RoundUp(), Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString()).RoundUp(), Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString()).RoundUp(), Convert.ToInt16(dc.Cells["OrderID"].Value.ToString()));
                    iRowCounter = iRowCounter + 1;
                }
                if (empSalaryID > 0)
                    MessageBox.Show("Details updated successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

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
            lblStateID.Text = "";
            lblSexID.Text = "";
            txtAallowences.Text = "0.00";
            txtDeductions.Text = "0.00";
            txtReimbursement.Text = "0.00";
            picRepEmpPhoto.Image = null;
            lblSelectedMonthSalaryID.Text = "";
            lblBasicSalary.Text = "0";
            lblBasicSalaryPerDay.Text = "0";
            lblBasicSalaryPerHour.Text = "0";

            txtTotalWorkingDays.Text = "0";
            txtTotalWorkedDays.Text = "0";
            txtLeaveDays.Text = "0";
            txtUnpaidLeaves.Text = "0";
            txtTotalPayableDays.Text = "0";
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
            dtgSalaryDetails.Columns["HeaderTitle"].Width = 250;
            dtgSalaryDetails.Columns["HeaderTitle"].ReadOnly = true;
            dtgSalaryDetails.Columns["HeaderType"].ReadOnly = true;
            dtgSalaryDetails.Columns["HeaderType"].Width = 125;
            dtgSalaryDetails.Columns["CalcFormula"].Visible = false;
            dtgSalaryDetails.Columns["IsFixed"].Visible = false;
            dtgSalaryDetails.Columns["ActualAmount"].Width = 135;
            dtgSalaryDetails.Columns["ActualAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgSalaryDetails.Columns["ActualAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryDetails.Columns["AllowanceAmount"].Width = 135;
            dtgSalaryDetails.Columns["AllowanceAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgSalaryDetails.Columns["AllowanceAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryDetails.Columns["DeductionAmount"].Width = 135;
            dtgSalaryDetails.Columns["ReimbursmentAmount"].Width = 135;
            dtgSalaryDetails.Columns["DeductionAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Deductions
            dtgSalaryDetails.Columns["DeductionAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Reimbursments
            dtgSalaryDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryDetails.Columns["SalProAmount"].Visible = false;
            dtgSalaryDetails.Columns["SalProAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Deductions
            dtgSalaryDetails.Columns["SalProAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryDetails.Columns["OrderID"].Visible = false;
            dtgSalaryDetails.Enabled = false;
            btnViewCalender.Visible = false;
            tabAdvanceHeaders.Enabled = false;
            tabControl1.SelectedIndex = 0;
            dtgAdvanceDetails.DataSource = null;
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

            List<string> last6Months = new List<string>();
            DateTime currentMonth = DateTime.Now;

            //for (int i = 6; i >= 0; i--)
            //{
            //    DateTime month = currentMonth.AddMonths(-i);
            //    cmbSalaryMonth.Items.Add(month.ToString("MMM - yyyy"));
            //}

            currentMonth = DateTime.Parse("01-01-" + DateTime.Now.Year.ToString());
            for (int i = 0; i < 12; i++)
            {
                DateTime month = currentMonth.AddMonths(i);
                cmbSalaryMonth.Items.Add(month.ToString("MMM - yyyy"));
            }
            cmbSalaryMonth.SelectedIndex = DateTime.Now.Month-1;

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

        private void frmPayrollMaster_Load(object sender, EventArgs e)
        {
            clearControls();
            disableControls();
            onCancelButtonClick();
        }

        private void btnReportingManagerSearch_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            if (lblActionMode.Text == "add")
            {
                frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listPayrollUsersList");
                frmEmployeeList.ShowDialog(this);
            }
            else if (lblActionMode.Text == "modify")
            {
                frmEmployeeList frmEmployeeList = new frmEmployeeList(this, "listEmployeesPayslip");
                frmEmployeeList.ShowDialog(this);
            }
        }

        public void SelectedEmployeeID(string SearchOptionSelectedForm, int selectedEmployeeID, int selectedMonthSalaryID)
        {
            tabControl1.SelectedIndex = 0;
            if (SearchOptionSelectedForm == "listPayrollUsersList")
            {
                lblReportingManagerID.Text = selectedEmployeeID.ToString();
                ReportingManagerInfo objReportingManagerInfo = objEmployeeMaster.GetReportingManagerInfo(Convert.ToInt16(selectedEmployeeID.ToString()));
                txtRepEmpCode.Text = objReportingManagerInfo.EmpCode;
                txtRepEmpName.Text = objReportingManagerInfo.EmpName;
                txtRepEmpDesig.Text = objReportingManagerInfo.DesignationTitle;
                txtRepEmpDepartment.Text = objReportingManagerInfo.DepartmentTitle;
                lblStateID.Text = objReportingManagerInfo.StateID.ToString();
                lblSexID.Text = objReportingManagerInfo.SexID.ToString();
                picRepEmpPhoto.Image = objImpageOperation.BytesToImage(objPhotoMas.getEmployeePhoto(Convert.ToInt16(lblReportingManagerID.Text.ToString())).EmpPhoto);

                int SalaryProfileID = 0;
                
                //SalaryProfileID = objSalaryProfile.getEmployeeSpecificSalaryProfile(Convert.ToInt16(lblReportingManagerID.Text.ToString())).SalProfileID;
                SalaryProfileID = objSalaryProfile.getEmployeeSalaryConfigStructure(Convert.ToInt16(lblReportingManagerID.Text.ToString()));

                dtgSalaryDetails.Enabled = true;
                //dtgSalaryDetails.DataSource = objSalaryProfile.GetEmployeeSpecificSalaryProfileInfo(Convert.ToInt16(lblReportingManagerID.Text));
                dtgSalaryDetails.DataSource = null;
                dtgSalaryDetails.Rows.Clear();
                dtgSalaryDetails.Refresh();
                dtgSalaryDetails.DataSource = objSalaryProfile.getEmployeeSpecificSalaryConfigStructure(Convert.ToInt16(lblReportingManagerID.Text.ToString()), Convert.ToInt16(SalaryProfileID));
                if (SalaryProfileID == 0)
                    dtgSalaryDetails.DataSource = objSalaryProfile.GetDefaultSalaryProfileInfo(SalaryProfileID);

                dtgSalaryDetails.Columns["EmpSalDetID"].Visible = false;
                dtgSalaryDetails.Columns["SalProDetID"].Visible = false;
                dtgSalaryDetails.Columns["SalProfileID"].Visible = false;
                dtgSalaryDetails.Columns["HeaderID"].Visible = false;
                dtgSalaryDetails.Columns["HeaderTitle"].Width = 250;
                dtgSalaryDetails.Columns["HeaderTitle"].ReadOnly = true;
                dtgSalaryDetails.Columns["HeaderType"].ReadOnly = true;
                dtgSalaryDetails.Columns["HeaderType"].Width = 125;
                dtgSalaryDetails.Columns["CalcFormula"].Visible = false;
                dtgSalaryDetails.Columns["CalcFormula"].ReadOnly = true;
                dtgSalaryDetails.Columns["IsFixed"].Visible = false;
                dtgSalaryDetails.Columns["ActualAmount"].ReadOnly = true; 
                dtgSalaryDetails.Columns["ActualAmount"].Width = 135;
                dtgSalaryDetails.Columns["ActualAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
                dtgSalaryDetails.Columns["ActualAmount"].DefaultCellStyle.Format = "c2";
                dtgSalaryDetails.Columns["AllowanceAmount"].Width = 135;
                dtgSalaryDetails.Columns["AllowanceAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
                dtgSalaryDetails.Columns["AllowanceAmount"].DefaultCellStyle.Format = "c2";
                dtgSalaryDetails.Columns["DeductionAmount"].Width = 135;
                dtgSalaryDetails.Columns["ReimbursmentAmount"].Width = 135;
                dtgSalaryDetails.Columns["DeductionAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Deductions
                dtgSalaryDetails.Columns["DeductionAmount"].DefaultCellStyle.Format = "c2";
                dtgSalaryDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Reimbursments
                dtgSalaryDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Format = "c2";
                dtgSalaryDetails.Columns["SalProAmount"].Visible = false;
                dtgSalaryDetails.Columns["SalProAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Deductions
                dtgSalaryDetails.Columns["SalProAmount"].DefaultCellStyle.Format = "c2";
                dtgSalaryDetails.Columns["OrderID"].Visible = false;

                List<EmployeeSpecificAdvanceInformation> objAdvanceOutstandingList = objAdvanceTransaction.EmployeeSpecificAdvanceInformation(Convert.ToInt32(lblReportingManagerID.Text.ToString()));

                if (objAdvanceOutstandingList.Count > 0)
                {
                    tabAdvanceHeaders.Enabled = true;

                    dtgAdvanceDetails.DataSource = objAdvanceOutstandingList;
                    dtgAdvanceDetails.Columns["Select"].Visible = true;
                    dtgAdvanceDetails.Columns["Select"].ReadOnly = false;

                    dtgAdvanceDetails.Columns["EmpID"].Visible = false;
                    dtgAdvanceDetails.Columns["EmpID"].ReadOnly = true;
                    dtgAdvanceDetails.Columns["EmpID"].Width = 100;

                    dtgAdvanceDetails.Columns["EmpCode"].Visible = false;
                    dtgAdvanceDetails.Columns["EmpCode"].ReadOnly = true;
                    dtgAdvanceDetails.Columns["EmpCode"].Width = 100;

                    dtgAdvanceDetails.Columns["EmpName"].Visible = false;
                    dtgAdvanceDetails.Columns["EmpName"].ReadOnly = true;
                    dtgAdvanceDetails.Columns["EmpName"].Width = 175;

                    dtgAdvanceDetails.Columns["DesignationTitle"].Visible = false;
                    dtgAdvanceDetails.Columns["DesignationTitle"].ReadOnly = true;
                    dtgAdvanceDetails.Columns["DesignationTitle"].Width = 175;

                    dtgAdvanceDetails.Columns["DepartmentTitle"].Visible = false;
                    dtgAdvanceDetails.Columns["DepartmentTitle"].ReadOnly = true;
                    dtgAdvanceDetails.Columns["DepartmentTitle"].Width = 175;

                    dtgAdvanceDetails.Columns["PersonalInfoID"].Visible = false;
                    dtgAdvanceDetails.Columns["PersonalInfoID"].ReadOnly = true;
                    dtgAdvanceDetails.Columns["PersonalInfoID"].Width = 100;

                    dtgAdvanceDetails.Columns["ContactNumber2"].Visible = false;
                    dtgAdvanceDetails.Columns["ContactNumber2"].ReadOnly = true;
                    dtgAdvanceDetails.Columns["ContactNumber2"].Width = 200;

                    dtgAdvanceDetails.Columns["EmpAdvanceRequestID"].Visible = false;
                    dtgAdvanceDetails.Columns["EmpAdvanceRequestID"].ReadOnly = true;
                    dtgAdvanceDetails.Columns["EmpAdvanceRequestID"].Width = 175;

                    dtgAdvanceDetails.Columns["EmpAdvReqCode"].Visible = true;
                    dtgAdvanceDetails.Columns["EmpAdvReqCode"].ReadOnly = true;
                    dtgAdvanceDetails.Columns["EmpAdvReqCode"].Width = 175;

                    dtgAdvanceDetails.Columns["AdvanceTypeID"].Visible = false;
                    dtgAdvanceDetails.Columns["AdvanceTypeID"].ReadOnly = true;
                    dtgAdvanceDetails.Columns["AdvanceTypeID"].Width = 175;

                    dtgAdvanceDetails.Columns["AdvanceTypeTitle"].Visible = true;
                    dtgAdvanceDetails.Columns["AdvanceTypeTitle"].ReadOnly = true;
                    dtgAdvanceDetails.Columns["AdvanceTypeTitle"].Width = 175;

                    dtgAdvanceDetails.Columns["AdvanceAmount"].Visible = true;
                    dtgAdvanceDetails.Columns["AdvanceAmount"].ReadOnly = true;
                    dtgAdvanceDetails.Columns["AdvanceAmount"].Width = 125;
                    dtgAdvanceDetails.Columns["AdvanceAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
                    dtgAdvanceDetails.Columns["AdvanceAmount"].DefaultCellStyle.Format = "c2";

                    dtgAdvanceDetails.Columns["AdvanceInstallment"].Visible = true;
                    dtgAdvanceDetails.Columns["AdvanceInstallment"].ReadOnly = true;
                    dtgAdvanceDetails.Columns["AdvanceInstallment"].Width = 125;
                    dtgAdvanceDetails.Columns["AdvanceInstallment"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
                    dtgAdvanceDetails.Columns["AdvanceInstallment"].DefaultCellStyle.Format = "c2";

                    dtgAdvanceDetails.Columns["AdvanceStartDate"].Visible = true;
                    dtgAdvanceDetails.Columns["AdvanceStartDate"].ReadOnly = true;
                    dtgAdvanceDetails.Columns["AdvanceStartDate"].Width = 125;
                    dtgAdvanceDetails.Columns["AdvanceStartDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";

                    dtgAdvanceDetails.Columns["AdvanceEndDate"].Visible = true;
                    dtgAdvanceDetails.Columns["AdvanceEndDate"].ReadOnly = true;
                    dtgAdvanceDetails.Columns["AdvanceEndDate"].Width = 125;
                    dtgAdvanceDetails.Columns["AdvanceEndDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";

                    dtgAdvanceDetails.Columns["AdvanceRequestStatus"].Visible = true;
                    dtgAdvanceDetails.Columns["AdvanceRequestStatus"].ReadOnly = true;
                    dtgAdvanceDetails.Columns["AdvanceRequestStatus"].Width = 150;

                    dtgAdvanceDetails.Columns["LastRepayDate"].Visible = true;
                    dtgAdvanceDetails.Columns["LastRepayDate"].ReadOnly = true;
                    dtgAdvanceDetails.Columns["LastRepayDate"].Width = 125;
                    dtgAdvanceDetails.Columns["LastRepayDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";

                    dtgAdvanceDetails.Columns["CBalance"].Visible = true;
                    dtgAdvanceDetails.Columns["CBalance"].ReadOnly = true;
                    dtgAdvanceDetails.Columns["CBalance"].Width = 125;
                    dtgAdvanceDetails.Columns["CBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
                    dtgAdvanceDetails.Columns["CBalance"].DefaultCellStyle.Format = "c2";

                    dtgAdvanceDetails.Columns["RePaymentBalance"].Visible = true;
                    dtgAdvanceDetails.Columns["RePaymentBalance"].ReadOnly = false;
                    dtgAdvanceDetails.Columns["RePaymentBalance"].Width = 125;
                    dtgAdvanceDetails.Columns["RePaymentBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
                    dtgAdvanceDetails.Columns["RePaymentBalance"].DefaultCellStyle.Format = "c2";

                    dtgAdvanceDetails.Columns["EmpAdvanceRecoveryID"].Visible = false;
                    dtgAdvanceDetails.Columns["EmpAdvanceRecoveryID"].ReadOnly = true;
                    dtgAdvanceDetails.Columns["EmpAdvanceRecoveryID"].Width = 175;

                    dtgAdvanceDetails.Columns["AdvanceRequestStatus"].Visible = false;
                    dtgAdvanceDetails.Columns["AdvanceRequestStatus"].ReadOnly = true;
                    dtgAdvanceDetails.Columns["AdvanceRequestStatus"].Width = 175;

                    foreach (DataGridViewRow row in dtgAdvanceDetails.Rows)
                    {
                        AdvanceTypeConfigModel objAdvanceTypeConfigModel = objAdvanceTypeConfigInfo.GetAdvanceTypeConfigByID(Convert.ToInt32(row.Cells["AdvanceTypeID"].Value.ToString()));
                        if (objAdvanceTypeConfigModel.RecoveryRequired)
                        {
                            if (objAdvanceTypeConfigModel.AutoDeductFromSalary)
                            {
                                if (objAdvanceTypeConfigModel.IncludeInSalary)
                                {
                                    if (objAdvanceTypeConfigModel.AllowPause || objAdvanceTypeConfigModel.WaiverAllowed)
                                    {
                                        row.Cells["Select"].Value = true;
                                    }
                                    else
                                    {
                                        row.Cells["Select"].Value = true;
                                    }
                                    row.Cells["Select"].ReadOnly = false;
                                    row.Cells["RePaymentBalance"].ReadOnly = false;
                                }
                                else
                                {
                                    row.Cells["Select"].Value = false;
                                    row.Cells["RePaymentBalance"].ReadOnly = false;
                                }
                            }
                            else if (objAdvanceTypeConfigModel.AutoDeductFromSalary == false)
                            {
                                if (objAdvanceTypeConfigModel.IncludeInSalary)
                                {
                                    if (objAdvanceTypeConfigModel.AllowPause || objAdvanceTypeConfigModel.WaiverAllowed)
                                    {
                                        row.Cells["Select"].Value = true;
                                    }
                                    else
                                    {
                                        row.Cells["Select"].Value = true;
                                    }
                                    row.Cells["Select"].ReadOnly = false;
                                    row.Cells["RePaymentBalance"].ReadOnly = false;
                                }
                                else
                                {
                                    row.Cells["Select"].Value = false;
                                    row.Cells["Select"].ReadOnly = true;
                                    row.Cells["RePaymentBalance"].ReadOnly = true;
                                }
                            }
                        }
                        else
                        {
                            row.Cells["Select"].Value = false;
                            row.Cells["RePaymentBalance"].ReadOnly = true;
                        }
                    }
                }
                else
                {
                    tabAdvanceHeaders.Enabled = false;
                }

                decimal totalAallowences = 0;
                decimal totalDeductions = 0;
                decimal totalReimbursement = 0;

                foreach (DataGridViewRow dc in dtgSalaryDetails.Rows)
                {
                    totalAallowences = totalAallowences + Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString());
                    totalDeductions = totalDeductions + Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString());
                    totalReimbursement = totalReimbursement + Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString());
                    dc.Cells["EmpSalDetID"].ReadOnly = true;
                    dc.Cells["SalProDetID"].ReadOnly = true;
                    dc.Cells["SalProfileID"].ReadOnly = true;
                    dc.Cells["HeaderID"].ReadOnly = true;
                    dc.Cells["HeaderTitle"].ReadOnly = true;
                    dc.Cells["HeaderType"].ReadOnly = true;
                    if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "allowences")
                    {
                        dc.Cells["AllowanceAmount"].ReadOnly = false;
                        dc.Cells["DeductionAmount"].ReadOnly = true;
                        dc.Cells["ReimbursmentAmount"].ReadOnly = true;
                    }
                    else if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "deductions")
                    {
                        dc.Cells["AllowanceAmount"].ReadOnly = true;
                        dc.Cells["DeductionAmount"].ReadOnly = false;
                        dc.Cells["ReimbursmentAmount"].ReadOnly = true;
                    }
                    else if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "reimbursement")
                    {
                        dc.Cells["AllowanceAmount"].ReadOnly = true;
                        dc.Cells["DeductionAmount"].ReadOnly = true;
                        dc.Cells["ReimbursmentAmount"].ReadOnly = false;
                    }
                }
                txtAallowences.Text = Convert.ToDecimal(totalAallowences.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
                txtDeductions.Text = Convert.ToDecimal(totalDeductions.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
                txtReimbursement.Text = Convert.ToDecimal(totalReimbursement.ToString()).ToString("0.00", CultureInfo.InvariantCulture);

                txtNetPayable.Text = Convert.ToDecimal((totalAallowences + totalReimbursement) - totalDeductions).ToString();
                txtNetPayable.Text = Convert.ToDecimal(txtNetPayable.Text.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
                btnViewCalender.Visible = true;

                DateTime parsedDate = DateTime.ParseExact(cmbSalaryMonth.SelectedItem.ToString(), "MMM - yyyy", CultureInfo.InvariantCulture);
                int daysInMonth = DateTime.DaysInMonth(parsedDate.Year, parsedDate.Month);
                txtTotalWorkingDays.Text = daysInMonth.ToString();

                getMonthlyWorkingDays(Convert.ToInt16(lblReportingManagerID.Text.ToString()), parsedDate, parsedDate.AddDays(daysInMonth).Date);

                decimal basicSalary = dtgSalaryDetails.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == "Basic Salary").Select(r => Convert.ToDecimal(r.Cells["AllowanceAmount"].Value ?? 0)).FirstOrDefault();
                lblBasicSalary.Text = basicSalary.ToString("#,#0.00");

                dtgSalaryDetails.ClearSelection();
                dtgSalaryDetails.Rows[1].Selected = true;
                var cell = dtgSalaryDetails.Rows[1].Cells["AllowanceAmount"];
                dtgSalaryDetails.CurrentCell = cell;
                dtgSalaryDetails.FirstDisplayedScrollingRowIndex = dtgSalaryDetails.Rows[1].Index;
                dtgSalaryDetails.BeginEdit(true);

                dtgSalaryDetails.ClearSelection();
                dtgSalaryDetails.Rows[0].Selected = true;
                cell = dtgSalaryDetails.Rows[0].Cells["AllowanceAmount"];
                dtgSalaryDetails.CurrentCell = cell;
                dtgSalaryDetails.FirstDisplayedScrollingRowIndex = dtgSalaryDetails.Rows[0].Index;
                dtgSalaryDetails.BeginEdit(true);
            }
            else if (SearchOptionSelectedForm == "listEmployeesPayslip")
            {
                lblReportingManagerID.Text = selectedEmployeeID.ToString();
                ReportingManagerInfo objReportingManagerInfo = objEmployeeMaster.GetReportingManagerInfo(Convert.ToInt16(selectedEmployeeID.ToString()));
                txtRepEmpCode.Text = objReportingManagerInfo.EmpCode;
                txtRepEmpName.Text = objReportingManagerInfo.EmpName;
                txtRepEmpDesig.Text = objReportingManagerInfo.DesignationTitle;
                txtRepEmpDepartment.Text = objReportingManagerInfo.DepartmentTitle;
                lblStateID.Text = objReportingManagerInfo.StateID.ToString();
                lblSexID.Text = objReportingManagerInfo.SexID.ToString();
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
                    txtNetPayable.Text = Convert.ToDecimal(txtNetPayable.Text.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
                }

                dtgSalaryDetails.DataSource = objEmployeePayroll.getSelectedSpecificMonthSalaryDetails(Convert.ToInt16(selectedEmployeeID.ToString()), selectedMonthSalaryID);

                dtgSalaryDetails.Enabled = true;
                dtgSalaryDetails.Columns["EmpSalDetID"].Visible = false;
                dtgSalaryDetails.Columns["EmpSalDetID"].ReadOnly = true;
                dtgSalaryDetails.Columns["SalProDetID"].Visible = false;
                dtgSalaryDetails.Columns["SalProDetID"].ReadOnly = true;
                //dtgSalaryDetails.Columns["EmpSalID"].Visible = false;
                //dtgSalaryDetails.Columns["EmpSalID"].ReadOnly = true;
                dtgSalaryDetails.Columns["HeaderID"].Visible = false;
                dtgSalaryDetails.Columns["HeaderID"].ReadOnly = true;
                dtgSalaryDetails.Columns["HeaderTitle"].Width = 250;
                dtgSalaryDetails.Columns["HeaderTitle"].ReadOnly = true;
                dtgSalaryDetails.Columns["HeaderType"].ReadOnly = true;
                dtgSalaryDetails.Columns["HeaderType"].Width = 125;
                dtgSalaryDetails.Columns["CalcFormula"].Visible = false;
                dtgSalaryDetails.Columns["ActualAmount"].ReadOnly = true;
                dtgSalaryDetails.Columns["ActualAmount"].Width = 135;
                dtgSalaryDetails.Columns["ActualAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
                dtgSalaryDetails.Columns["ActualAmount"].DefaultCellStyle.Format = "c2";
                dtgSalaryDetails.Columns["AllowanceAmount"].Width = 135;
                dtgSalaryDetails.Columns["AllowanceAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
                dtgSalaryDetails.Columns["AllowanceAmount"].DefaultCellStyle.Format = "c2";
                dtgSalaryDetails.Columns["DeductionAmount"].Width = 135;
                dtgSalaryDetails.Columns["ReimbursmentAmount"].Width = 135;
                dtgSalaryDetails.Columns["DeductionAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Deductions
                dtgSalaryDetails.Columns["DeductionAmount"].DefaultCellStyle.Format = "c2";
                dtgSalaryDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Reimbursments
                dtgSalaryDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Format = "c2";
                //dtgSalaryDetails.Columns["SalProAmount"].Visible = true;;
                //dtgSalaryDetails.Columns["SalProAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Deductions
                //dtgSalaryDetails.Columns["SalProAmount"].DefaultCellStyle.Format = "c2";
                dtgSalaryDetails.Columns["OrderID"].Visible = false;

                decimal totalAallowences = 0;
                decimal totalDeductions = 0;
                decimal totalReimbursement = 0;

                foreach (DataGridViewRow dc in dtgSalaryDetails.Rows)
                {
                    totalAallowences = totalAallowences + Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString());
                    totalDeductions = totalDeductions + Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString());
                    totalReimbursement = totalReimbursement + Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString());
                    if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "allowences")
                    {
                        dc.Cells["AllowanceAmount"].ReadOnly = false;
                        dc.Cells["DeductionAmount"].ReadOnly = true;
                        dc.Cells["ReimbursmentAmount"].ReadOnly = true;
                    }
                    else if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "deductions")
                    {
                        dc.Cells["AllowanceAmount"].ReadOnly = true;
                        dc.Cells["DeductionAmount"].ReadOnly = false;
                        dc.Cells["ReimbursmentAmount"].ReadOnly = true;
                    }
                    else if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "reimbursement")
                    {
                        dc.Cells["AllowanceAmount"].ReadOnly = true;
                        dc.Cells["DeductionAmount"].ReadOnly = true;
                        dc.Cells["ReimbursmentAmount"].ReadOnly = false;
                    }
                }

                txtAallowences.Text = Convert.ToDecimal(totalAallowences.ToString()).RoundUp().ToString("0.00", CultureInfo.InvariantCulture);
                txtDeductions.Text = Convert.ToDecimal(totalDeductions.ToString()).RoundUp().ToString("0.00", CultureInfo.InvariantCulture);
                txtReimbursement.Text = Convert.ToDecimal(totalReimbursement.ToString()).RoundUp().ToString("0.00", CultureInfo.InvariantCulture);

                txtNetPayable.Text = Convert.ToDecimal((totalAallowences + totalReimbursement) - totalDeductions).RoundUp().ToString();
                txtNetPayable.Text = Convert.ToDecimal(txtNetPayable.Text.ToString()).RoundUp().ToString("0.00", CultureInfo.InvariantCulture);
                btnViewCalender.Visible = true;
            }
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
            if (dtgSalaryDetails.CurrentRow.Cells["HeaderType"].Value.ToString().ToLower() == "allowences")
            {
                dtgSalaryDetails.CurrentRow.Cells["AllowanceAmount"].ReadOnly = false;
                dtgSalaryDetails.CurrentRow.Cells["DeductionAmount"].ReadOnly = true;
                dtgSalaryDetails.CurrentRow.Cells["ReimbursmentAmount"].ReadOnly = true;
            }
            else if (dtgSalaryDetails.CurrentRow.Cells["HeaderType"].Value.ToString().ToLower() == "deductions")
            {
                dtgSalaryDetails.CurrentRow.Cells["AllowanceAmount"].ReadOnly = true;
                dtgSalaryDetails.CurrentRow.Cells["DeductionAmount"].ReadOnly = false;
                dtgSalaryDetails.CurrentRow.Cells["ReimbursmentAmount"].ReadOnly = true;
            }
            else if (dtgSalaryDetails.CurrentRow.Cells["HeaderType"].Value.ToString().ToLower() == "reimbursement")
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

            txtAallowences.Text = "0.00";
            txtDeductions.Text = "0.00";
            txtReimbursement.Text = "0.00";
            txtNetPayable.Text = "0.00";

            if (chkAutoCalculate.Checked == true)
            {

            }

            foreach (DataGridViewRow dc in dtgSalaryDetails.Rows)
            {
                totalAallowences = totalAallowences + Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString());
                totalDeductions = totalDeductions + Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString());
                totalReimbursement = totalReimbursement + Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString());
            }

            dtgSalaryDetails.Columns["AllowanceAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgSalaryDetails.Columns["AllowanceAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryDetails.Columns["DeductionAmount"].Width = 135;
            dtgSalaryDetails.Columns["ReimbursmentAmount"].Width = 135;
            dtgSalaryDetails.Columns["DeductionAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Deductions
            dtgSalaryDetails.Columns["DeductionAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Reimbursments
            dtgSalaryDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Format = "c2";

            txtAallowences.Text = Convert.ToDecimal(totalAallowences.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtDeductions.Text = Convert.ToDecimal(totalDeductions.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtReimbursement.Text = Convert.ToDecimal(totalReimbursement.ToString()).ToString("0.00", CultureInfo.InvariantCulture);

            totalAallowences = 0;
            totalDeductions = 0;
            totalReimbursement = 0;

            decimal basicSalary = dtgSalaryDetails.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == "Basic Salary").Select(r => Convert.ToDecimal(r.Cells["ActualAmount"].Value ?? 0)).FirstOrDefault();
            if(lblActionMode.Text == "modify")
                basicSalary = dtgSalaryDetails.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == "Basic Salary").Select(r => Convert.ToDecimal(r.Cells["AllowanceAmount"].Value ?? 0)).FirstOrDefault();

            lblBasicSalary.Text = basicSalary.RoundUp().ToString("#,#0.00");
            lblBasicSalaryPerDay.Text = Convert.ToDecimal(basicSalary / Convert.ToDecimal(txtTotalWorkingDays.Text.ToString())).RoundUp().ToString("#,#0.00");
            lblBasicSalaryPerHour.Text = Convert.ToDecimal(Convert.ToDecimal(lblBasicSalaryPerDay.Text.ToString()) / Convert.ToDecimal("8.0")).RoundUp().ToString("#,#0.00");

            foreach (DataGridViewRow dc in dtgSalaryDetails.Rows)
            {
                totalAallowences = totalAallowences + Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString());
                totalDeductions = totalDeductions + Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString());
                totalReimbursement = totalReimbursement + Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString());
                if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "allowences")
                {
                    dc.Cells["AllowanceAmount"].ReadOnly = false;
                    dc.Cells["DeductionAmount"].ReadOnly = true;
                    dc.Cells["ReimbursmentAmount"].ReadOnly = true;

                    AllowenceModel tmpAllowenceInfo = objAllowenceInfo.getSelectedAllowenceInfo(Convert.ToInt32(dc.Cells["HeaderID"].Value.ToString()));

                    string formula = dc.Cells["CalcFormula"].Value.ToString();
                    string headerName = formula == "N/A" ? dc.Cells["HeaderTitle"].Value.ToString() : formula.Substring(0, formula.IndexOf("*")).Trim();// formula.Substring(0, formula.IndexOf("*")).Trim();
                    string amount = dtgSalaryDetails.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == headerName).Select(r => Convert.ToDecimal(r.Cells["ActualAmount"].Value ?? 0)).DefaultIfEmpty(-1).First().ToString();
                    if (dc.Cells["HeaderTitle"].Value.ToString() == headerName)
                    {
                        amount = dtgSalaryDetails.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == headerName).Select(r => Convert.ToDecimal(r.Cells["ActualAmount"].Value ?? 0)).FirstOrDefault().ToString() ?? "0";
                        if (tmpAllowenceInfo.ProrataBasis)
                            amount = dtgSalaryDetails.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == headerName).Select(r => Convert.ToDecimal(r.Cells["AllowanceAmount"].Value ?? 0)).FirstOrDefault().ToString() ?? "0";
                    }
                    else
                    {
                        if (tmpAllowenceInfo.ProrataBasis)
                            amount = dtgSalaryDetails.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == headerName).Select(r => Convert.ToDecimal(r.Cells["AllowanceAmount"].Value ?? 0)).FirstOrDefault().ToString() ?? "0";
                        else
                            amount = dtgSalaryDetails.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == headerName).Select(r => Convert.ToDecimal(r.Cells["ActualAmount"].Value ?? 0)).FirstOrDefault().ToString() ?? "0";
                    }
                    if (dc.Cells["CalcFormula"].Value.ToString() != "N/A" )
                    {
                        dc.Cells["HeaderTitle"].ToolTipText = dc.Cells["CalcFormula"].Value.ToString();

                        //string formula = dc.Cells["CalcFormula"].Value.ToString();
                        //headerName = formula.Substring(0, formula.IndexOf("*")).Trim();
                        ////string amount = ""; // dtgSalaryDetails.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == headerName).Select(r => Convert.ToDecimal(r.Cells["AllowanceAmount"].Value ?? 0)).FirstOrDefault().ToString() ?? "-1";

                        lblBasicSalary.Text = Convert.ToDecimal(amount.ToString()).ToString("#,#0.00");
                        if(Convert.ToDecimal(amount) >= 0)
                          formula = formula.Replace(headerName, amount.ToString());
                        else if (Convert.ToDecimal(amount) < 0)
                            formula = formula.Replace(headerName, lblBasicSalaryPerHour.Text.ToString());

                        if (tmpAllowenceInfo.ProrataBasis)
                        {
                            formula = formula.Replace("TotalPayableDays", txtTotalPayableDays.Text.Trim());
                            formula = formula.Replace("TotalWorkingDays", txtTotalWorkingDays.Text.Trim());

                            if (lblActionMode.Text == "add" && formula.IndexOf("NumOfHours") > 0)
                            {
                                formula = formula.Replace("OverTime Allowance1", lblBasicSalaryPerHour.Text.Trim());
                                formula = formula.Replace("NumOfHours", dc.Cells["AllowanceAmount"].Value.ToString());
                            }
                            else if (lblActionMode.Text == "modify" && formula.IndexOf("NumOfHours") > 0)
                            {
                                //formula = formula.Replace("NumOfHours", dc.Cells["AllowanceAmount"].Value.ToString());
                                if (Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()).RoundUp() > 0 && Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()).RoundUp() < 50)
                                    formula = formula.Replace("NumOfHours", dc.Cells["AllowanceAmount"].Value.ToString());
                                else
                                    formula = Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()).RoundUp().ToString(); // (Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()) / Convert.ToDecimal(lblBasicSalaryPerHour.Text.ToString())).RoundUp().ToString();
                            }
                        }
                        else
                        {
                            formula = formula.Replace("TotalPayableDays", txtTotalWorkingDays.Text.Trim());
                            formula = formula.Replace("TotalWorkingDays", txtTotalWorkingDays.Text.Trim());

                            if (lblActionMode.Text == "add" && formula.IndexOf("NumOfHours") > 0)
                            {
                                formula = formula.Replace("OverTime Allowance1", lblBasicSalaryPerHour.Text.Trim());
                                formula = formula.Replace("NumOfHours", dc.Cells["AllowanceAmount"].Value.ToString());
                            }
                            else if (lblActionMode.Text == "modify" && formula.IndexOf("NumOfHours") > 0)
                            {
                                //formula = formula.Replace("NumOfHours", dc.Cells["AllowanceAmount"].Value.ToString());
                                if (Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()).RoundUp() > 0 && Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()).RoundUp() < 50)
                                    formula = formula.Replace("NumOfHours", dc.Cells["AllowanceAmount"].Value.ToString());
                                else
                                    formula = Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()).RoundUp().ToString(); // (Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()) / Convert.ToDecimal(lblBasicSalaryPerHour.Text.ToString())).RoundUp().ToString();
                            }
                        }

                        DataTable dt1 = new DataTable();
                        if (formula != "")
                        {
                            if (dc.Cells["HeaderTitle"].Value.ToString() == headerName)
                            {
                                dc.Cells["AllowanceAmount"].Value = Convert.ToDecimal(dt1.Compute(formula, "")).RoundUp();
                                lblBasicSalary.Text = Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()).RoundUp().ToString();
                            }
                            else
                                dc.Cells["AllowanceAmount"].Value = Convert.ToDecimal(dt1.Compute(formula, "")).RoundUp();
                        }
                    }
                    else if (dc.Cells["CalcFormula"].Value.ToString() == "N/A")
                    {
                        if(tmpAllowenceInfo.IsFixed)
                        {
                            if (tmpAllowenceInfo.ProrataBasis)
                            {
                                dc.Cells["AllowanceAmount"].Value = Convert.ToDecimal(tmpAllowenceInfo.MaxCap).RoundUp();
                            }
                            else
                            {

                            }
                        }
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
                                        DeductionModel tmpDeductionInfo = objDeductionsInfo.getSelectedDeductionInfo(Convert.ToInt32(dc.Cells["HeaderID"].Value.ToString()));
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

                                        basicSalary = dtgSalaryDetails.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == "Basic Salary").Select(r => Convert.ToDecimal(r.Cells["ActualAmount"].Value ?? 0)).FirstOrDefault();

                                        if(tmpDeductionInfo.ProrataBasis)
                                            basicSalary = dtgSalaryDetails.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == "Basic Salary").Select(r => Convert.ToDecimal(r.Cells["AllowanceAmount"].Value ?? 0)).FirstOrDefault();

                                        lblBasicSalary.Text = basicSalary.ToString("#,#0.00");
                                        
                                        decimal total = dtgSalaryDetails.Rows.Cast<DataGridViewRow>().Where(r => headers.Contains(r.Cells["HeaderTitle"].Value?.ToString())).Sum(r => Convert.ToDecimal(r.Cells["AllowanceAmount"].Value ?? 0));

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

            if(tabAdvanceHeaders.Enabled == true)
            {
                foreach (DataGridViewRow dc in dtgAdvanceDetails.Rows)
                {
                    if (Convert.ToBoolean(dc.Cells["Select"].Value.ToString()) == true)
                    {
                        totalDeductions = totalDeductions + Convert.ToDecimal(dc.Cells["RePaymentBalance"].Value.ToString());
                    }
                }
            }

            foreach (DataGridViewRow dc in dtgSalaryDetails.Rows)
            {
                totalAallowences = totalAallowences + Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString());
                totalDeductions = totalDeductions + Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString());
                totalReimbursement = totalReimbursement + Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString());
            }

            dtgSalaryDetails.Columns["AllowanceAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgSalaryDetails.Columns["AllowanceAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryDetails.Columns["DeductionAmount"].Width = 135;
            dtgSalaryDetails.Columns["ReimbursmentAmount"].Width = 135;
            dtgSalaryDetails.Columns["DeductionAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Deductions
            dtgSalaryDetails.Columns["DeductionAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Reimbursments
            dtgSalaryDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Format = "c2";

            txtAallowences.Text = Convert.ToDecimal(totalAallowences.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtDeductions.Text = Convert.ToDecimal(totalDeductions.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtReimbursement.Text = Convert.ToDecimal(totalReimbursement.ToString()).ToString("0.00", CultureInfo.InvariantCulture);

            txtNetPayable.Text = Convert.ToDecimal((totalAallowences + totalReimbursement) - totalDeductions).ToString();
            txtNetPayable.Text = Convert.ToDecimal(txtNetPayable.Text.ToString()).ToString("0.00", CultureInfo.InvariantCulture);

            basicSalary = dtgSalaryDetails.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == "Basic Salary").Select(r => Convert.ToDecimal(r.Cells["AllowanceAmount"].Value ?? 0)).FirstOrDefault();            
            lblBasicSalary.Text = basicSalary.RoundUp().ToString("#,#0.00");
            lblBasicSalaryPerDay.Text = Convert.ToDecimal(basicSalary / Convert.ToDecimal(txtTotalWorkingDays.Text.ToString())).RoundUp().ToString("#,#0.00");
            lblBasicSalaryPerHour.Text = Convert.ToDecimal(Convert.ToDecimal(lblBasicSalaryPerDay.Text.ToString()) / Convert.ToDecimal("8.0")).RoundUp().ToString("#,#0.00");
        }

        private void cmbSalaryMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime parsedDate = DateTime.ParseExact(cmbSalaryMonth.SelectedItem.ToString(), "MMM - yyyy", CultureInfo.InvariantCulture);

            int daysInMonth = DateTime.DaysInMonth(parsedDate.Year, parsedDate.Month);

            txtTotalWorkingDays.Text = daysInMonth.ToString();

            if (lblReportingManagerID.Text != "")
            {
                getMonthlyWorkingDays(Convert.ToInt16(lblReportingManagerID.Text.ToString()), parsedDate, parsedDate.AddDays(daysInMonth).Date);
                SelectedEmployeeID("listPayrollUsersList", Convert.ToInt16(lblReportingManagerID.Text.ToString()), Convert.ToInt32(cmbSalaryMonth.SelectedIndex + 1));
            }
        }

        private void getMonthlyWorkingDays(int txtEmpID, DateTime dtSelectedDateFrom, DateTime dtSelectedDateTo)
        {
            //txtTotalWorkedDays.Text = objAttendanceMas.getTotalPresentDays(txtEmpID, dtSelectedDateFrom, dtSelectedDateTo).TotalPresent.ToString();// objEmployeeTotalWorkingInfo.TotalPresent.ToString();
            //txtLeaveDays.Text = objAttendanceMas.getTotalPaidLeave(txtEmpID, dtSelectedDateFrom, dtSelectedDateTo).TotalPaidLeave.ToString();// objEmployeeTotalWorkingInfo.TotalPresent.ToString();
            //txtUnpaidLeaves.Text = objAttendanceMas.getTotalLossOfPayDays(txtEmpID, dtSelectedDateFrom, dtSelectedDateTo).TotalLossOfPay.ToString();// objEmployeeTotalWorkingInfo.TotalPresent.ToString();
            //txtTotalPayableDays.Text = (Convert.ToDecimal(txtTotalWorkedDays.Text.ToString()) + Convert.ToDecimal(txtLeaveDays.Text.ToString()) - Convert.ToDecimal(txtUnpaidLeaves.Text.ToString())).ToString(); 

            EmployeeTotalWorkingInfo objEmployeeTotalWorkingInfo = objAttendanceMas.GetEmployeeMonthlyWorkingDays(Convert.ToInt16(lblReportingManagerID.Text.ToString()), Convert.ToDateTime(dtSelectedDateFrom), Convert.ToDateTime(dtSelectedDateTo));
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
        }


        private void btnViewCalender_Click(object sender, EventArgs e)
        {
            //frmAttendanceMater frmAttendanceMater = new frmAttendanceMater("listAttendanceMasterList", Convert.ToInt16(lblReportingManagerID.Text));
            //frmAttendanceMater frmAttendanceMater = new frmAttendanceMater();
            //frmAttendanceMater.ShowDialog(this);
            frmIndEmpAttendanceCalender frmIndEmpAttendanceCalender = new frmIndEmpAttendanceCalender(objTempCurrentlyLoggedInUserInfo, objTempClientFinYearInfo, Convert.ToInt16(lblReportingManagerID.Text.ToString()), Convert.ToDateTime("01-" + cmbSalaryMonth.Text.Substring(0, 3) + "-" + cmbSalaryMonth.Text.Substring(cmbSalaryMonth.Text.IndexOf("-") + 2)));
            frmIndEmpAttendanceCalender.ShowDialog();
        }

        private void frmPayrollMaster_KeyDown(object sender, KeyEventArgs e)
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

        private void dtgAdvanceDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            decimal totalAallowences = 0;
            decimal totalDeductions = 0;
            decimal totalReimbursement = 0;

            txtAallowences.Text = "0.00";
            txtDeductions.Text = "0.00";
            txtReimbursement.Text = "0.00";
            txtNetPayable.Text = "0.00";

            if (chkAutoCalculate.Checked == true)
            {

            }

            decimal basicSalary = dtgSalaryDetails.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == "Basic Salary").Select(r => Convert.ToDecimal(r.Cells["ActualAmount"].Value ?? 0)).FirstOrDefault();
            if (lblActionMode.Text == "modify")
                basicSalary = dtgSalaryDetails.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == "Basic Salary").Select(r => Convert.ToDecimal(r.Cells["AllowanceAmount"].Value ?? 0)).FirstOrDefault();

            lblBasicSalary.Text = basicSalary.RoundUp().ToString("#,#0.00");
            lblBasicSalaryPerDay.Text = Convert.ToDecimal(basicSalary / Convert.ToDecimal(txtTotalWorkingDays.Text.ToString())).RoundUp().ToString("#,#0.00");
            lblBasicSalaryPerHour.Text = Convert.ToDecimal(Convert.ToDecimal(lblBasicSalaryPerDay.Text.ToString()) / Convert.ToDecimal("8.0")).RoundUp().ToString("#,#0.00");

            foreach (DataGridViewRow dc in dtgSalaryDetails.Rows)
            {
                totalAallowences = totalAallowences + Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString());
                totalDeductions = totalDeductions + Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString());
                totalReimbursement = totalReimbursement + Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString());
                if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "allowences")
                {
                    dc.Cells["AllowanceAmount"].ReadOnly = false;
                    dc.Cells["DeductionAmount"].ReadOnly = true;
                    dc.Cells["ReimbursmentAmount"].ReadOnly = true;
                    if (dc.Cells["CalcFormula"].Value.ToString() != "N/A")
                    {
                        dc.Cells["HeaderTitle"].ToolTipText = dc.Cells["CalcFormula"].Value.ToString();

                        string formula = dc.Cells["CalcFormula"].Value.ToString();
                        string headerName = formula.Substring(0, formula.IndexOf("*")).Trim();
                        string amount = dtgSalaryDetails.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == headerName).Select(r => Convert.ToDecimal(r.Cells["AllowanceAmount"].Value ?? 0)).FirstOrDefault().ToString() ?? "-1";

                        amount = dtgSalaryDetails.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == headerName).Select(r => Convert.ToDecimal(r.Cells["AllowanceAmount"].Value ?? 0)).DefaultIfEmpty(-1).First().ToString();
                        if (dc.Cells["HeaderTitle"].Value.ToString() == headerName)
                            amount = dtgSalaryDetails.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == headerName).Select(r => Convert.ToDecimal(r.Cells["ActualAmount"].Value ?? 0)).FirstOrDefault().ToString() ?? "0";

                        lblBasicSalary.Text = Convert.ToDecimal(amount.ToString()).ToString("#,#0.00");
                        if (Convert.ToDecimal(amount) >= 0)
                            formula = formula.Replace(headerName, amount.ToString());
                        else if (Convert.ToDecimal(amount) < 0)
                            formula = formula.Replace(headerName, lblBasicSalaryPerHour.Text.ToString());

                        formula = formula.Replace("TotalPayableDays", txtTotalPayableDays.Text.Trim());
                        formula = formula.Replace("TotalWorkingDays", txtTotalWorkingDays.Text.Trim());

                        if (lblActionMode.Text == "add" && formula.IndexOf("NumOfHours") > 0)
                        {
                            formula = formula.Replace("OverTime Allowance1", lblBasicSalaryPerHour.Text.Trim());
                            formula = formula.Replace("NumOfHours", dc.Cells["AllowanceAmount"].Value.ToString());
                        }
                        else if (lblActionMode.Text == "modify" && formula.IndexOf("NumOfHours") > 0)
                        {
                            //formula = formula.Replace("NumOfHours", dc.Cells["AllowanceAmount"].Value.ToString());
                            if (Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()).RoundUp() > 0 && Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()).RoundUp() < 50)
                                formula = formula.Replace("NumOfHours", dc.Cells["AllowanceAmount"].Value.ToString());
                            else
                                formula = Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()).RoundUp().ToString(); // (Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()) / Convert.ToDecimal(lblBasicSalaryPerHour.Text.ToString())).RoundUp().ToString();
                        }

                        DataTable dt1 = new DataTable();
                        if (formula != "")
                        {
                            if (dc.Cells["HeaderTitle"].Value.ToString() == headerName)
                            {
                                dc.Cells["AllowanceAmount"].Value = Convert.ToDecimal(dt1.Compute(formula, ""));
                                lblBasicSalary.Text = Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()).RoundUp().ToString("#,#0.00");
                            }
                            else
                                dc.Cells["AllowanceAmount"].Value = Convert.ToDecimal(dt1.Compute(formula, "")).RoundUp();
                        }
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
                                        string[] headers = null;
                                        if (Convert.ToDecimal(lblBasicSalary.Text.ToString()) > Convert.ToDecimal(objDeductionsInfo.getDeductionMaxCap(Convert.ToInt32(dc.Cells["HeaderID"].Value.ToString()))))
                                        {
                                            headers = new[] { "Basic Salary", "Dearness Allowance" };
                                        }
                                        else if (Convert.ToDecimal(lblBasicSalary.Text.ToString()) <= Convert.ToDecimal(objDeductionsInfo.getDeductionMaxCap(Convert.ToInt32(dc.Cells["HeaderID"].Value.ToString()))))
                                        {
                                            headers = new[] { "Basic Salary" };
                                        }

                                        basicSalary = dtgSalaryDetails.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == "Basic Salary").Select(r => Convert.ToDecimal(r.Cells["AllowanceAmount"].Value ?? 0)).FirstOrDefault();
                                        lblBasicSalary.Text = basicSalary.ToString("#,#0.00");
                                        decimal total = dtgSalaryDetails.Rows.Cast<DataGridViewRow>().Where(r => headers.Contains(r.Cells["HeaderTitle"].Value?.ToString())).Sum(r => Convert.ToDecimal(r.Cells["AllowanceAmount"].Value ?? 0));

                                        if (Convert.ToDecimal(lblBasicSalary.Text.ToString()) > Convert.ToDecimal(objDeductionsInfo.getDeductionMaxCap(Convert.ToInt32(dc.Cells["HeaderID"].Value.ToString()))))
                                        {
                                            total = Convert.ToDecimal(objDeductionsInfo.getDeductionMaxCap(Convert.ToInt32(dc.Cells["HeaderID"].Value.ToString())));
                                        }

                                        ProvidentFund objProvidentFund = objClientStatutory.GetCompanyProvidentFundSettings(Convert.ToInt16(objTempClientFinYearInfo.ClientID));
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
                                        string[] headers = null;
                                        if (Convert.ToDecimal(lblBasicSalary.Text.ToString()) > Convert.ToDecimal(objDeductionsInfo.getDeductionMaxCap(Convert.ToInt32(dc.Cells["HeaderID"].Value.ToString()))))
                                        {
                                            headers = new[] { "Basic Salary", "Dearness Allowance" };
                                        }
                                        else if (Convert.ToDecimal(lblBasicSalary.Text.ToString()) <= Convert.ToDecimal(objDeductionsInfo.getDeductionMaxCap(Convert.ToInt32(dc.Cells["HeaderID"].Value.ToString()))))
                                        {
                                            headers = new[] { "Basic Salary" };
                                        }

                                        basicSalary = dtgSalaryDetails.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == "Basic Salary").Select(r => Convert.ToDecimal(r.Cells["AllowanceAmount"].Value ?? 0)).FirstOrDefault();
                                        lblBasicSalary.Text = basicSalary.ToString("#,#0.00");
                                        decimal total = dtgSalaryDetails.Rows.Cast<DataGridViewRow>().Where(r => headers.Contains(r.Cells["HeaderTitle"].Value?.ToString())).Sum(r => Convert.ToDecimal(r.Cells["AllowanceAmount"].Value ?? 0));

                                        if (Convert.ToDecimal(lblBasicSalary.Text.ToString()) > Convert.ToDecimal(objDeductionsInfo.getDeductionMaxCap(Convert.ToInt32(dc.Cells["HeaderID"].Value.ToString()))))
                                        {
                                            total = Convert.ToDecimal(objDeductionsInfo.getDeductionMaxCap(Convert.ToInt32(dc.Cells["HeaderID"].Value.ToString())));
                                        }

                                        ProvidentFund objProvidentFund = objClientStatutory.GetCompanyProvidentFundSettings(Convert.ToInt16(objTempClientFinYearInfo.ClientID));
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

            if (tabAdvanceHeaders.Enabled == true)
            {
                foreach (DataGridViewRow dc in dtgAdvanceDetails.Rows)
                {
                    if (Convert.ToBoolean(dc.Cells["Select"].Value.ToString()) == true)
                    {
                        totalDeductions = totalDeductions + Convert.ToDecimal(dc.Cells["RePaymentBalance"].Value.ToString());
                    }
                }
            }

            foreach (DataGridViewRow dc in dtgSalaryDetails.Rows)
            {
                totalAallowences = totalAallowences + Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString());
                totalDeductions = totalDeductions + Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString());
                totalReimbursement = totalReimbursement + Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString());
            }

            dtgSalaryDetails.Columns["AllowanceAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgSalaryDetails.Columns["AllowanceAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryDetails.Columns["DeductionAmount"].Width = 135;
            dtgSalaryDetails.Columns["ReimbursmentAmount"].Width = 135;
            dtgSalaryDetails.Columns["DeductionAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Deductions
            dtgSalaryDetails.Columns["DeductionAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Reimbursments
            dtgSalaryDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Format = "c2";

            txtAallowences.Text = Convert.ToDecimal(totalAallowences.ToString()).RoundUp().ToString("0.00", CultureInfo.InvariantCulture);
            txtDeductions.Text = Convert.ToDecimal(totalDeductions.ToString()).RoundUp().ToString("0.00", CultureInfo.InvariantCulture);
            txtReimbursement.Text = Convert.ToDecimal(totalReimbursement.ToString()).RoundUp().ToString("0.00", CultureInfo.InvariantCulture);

            txtNetPayable.Text = Convert.ToDecimal((totalAallowences + totalReimbursement) - totalDeductions).RoundUp().ToString();
            txtNetPayable.Text = Convert.ToDecimal(txtNetPayable.Text.ToString()).RoundUp().ToString("0.00", CultureInfo.InvariantCulture);

            basicSalary = dtgSalaryDetails.Rows.Cast<DataGridViewRow>().Where(r => r.Cells["HeaderTitle"].Value?.ToString() == "Basic Salary").Select(r => Convert.ToDecimal(r.Cells["AllowanceAmount"].Value ?? 0)).FirstOrDefault();
            lblBasicSalary.Text = basicSalary.RoundUp().ToString("#,#0.00");
            lblBasicSalaryPerDay.Text = Convert.ToDecimal(basicSalary / Convert.ToDecimal(txtTotalWorkingDays.Text.ToString())).RoundUp().ToString("#,#0.00");
            lblBasicSalaryPerHour.Text = Convert.ToDecimal(Convert.ToDecimal(lblBasicSalaryPerDay.Text.ToString()) / Convert.ToDecimal("8.0")).RoundUp().ToString("#,#0.00");
        }

        private void dtgAdvanceDetails_DoubleClick(object sender, EventArgs e)
        {
            if (lblReportingManagerID.Text.Trim() == "")
                return;

            frmAdvanceConfigInfoReadOnly frmViewAdvanceConfigInfoReadOnly = new frmAdvanceConfigInfoReadOnly(Convert.ToInt32(dtgAdvanceDetails.SelectedRows[0].Cells["AdvanceTypeID"].Value.ToString()));
            frmViewAdvanceConfigInfoReadOnly.ShowDialog(this);
        }

        private void frmPayrollMaster_Activated(object sender, EventArgs e)
        {
            dtgSalaryDetails.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
            dtgAdvanceDetails.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
        }
    }
}
