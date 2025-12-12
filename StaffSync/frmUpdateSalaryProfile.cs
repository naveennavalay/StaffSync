using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Office.Y2022.FeaturePropertyBag;
using DocumentFormat.OpenXml.Wordprocessing;
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
    public partial class frmUpdateSalaryProfile : Form
    {
        //myDBClass objDBClass = new myDBClass();
        //OleDbConnection conn = null;
        //DataSet dtDataset;

        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        DALStaffSync.clsAllowenceInfo objAllowenceInfo = new DALStaffSync.clsAllowenceInfo();
        DALStaffSync.clsDeductionsInfo objDeductionsInfo = new DALStaffSync.clsDeductionsInfo();
        DALStaffSync.clsReimbursement objReimbursement = new DALStaffSync.clsReimbursement();
        DALStaffSync.clsSalaryProfile objSalaryProfile = new DALStaffSync.clsSalaryProfile();
        DALStaffSync.clsEmployeeSalaryProfileInfo objEmployeeSalaryProfileInfo = new DALStaffSync.clsEmployeeSalaryProfileInfo();
        DALStaffSync.clsEmpPayroll objEmployeePayroll = new DALStaffSync.clsEmpPayroll();
        frmDashboard objDashboard = (frmDashboard)System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();

        public frmUpdateSalaryProfile()
        {
            InitializeComponent();
        }

        public frmUpdateSalaryProfile(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmUpdateSalaryProfile(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
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
            this.Close();
        }

        private void frmUpdateSalaryProfile_Load(object sender, EventArgs e)
        {
            //// TODO: This line of code loads data into the 'staffsyncDBDTSet.EmpMasInfo' table. You can move, or remove it, as needed.
            //this.empMasInfoTableAdapter.Fill(this.staffsyncDBDTSet.EmpMasInfo);
            dtgSalaryProfileDetails.DataSource = objSalaryProfile.GetDefaultSalaryProfileInfo(1);
            onCancelButtonClick();
            disableControls();
            clearControls();
            chkAutomaticCalculate.Checked = true;
            FormatTheGrid();
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
            frmSalaryProfileList frmSalaryProfileList = new frmSalaryProfileList(this);
            frmSalaryProfileList.ShowDialog(this);
        }

        private void btnCloseMe_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenerateDetails_Click(object sender, EventArgs e)
        {
            lblActionMode.Text = "add";
            onGenerateButtonClick();
            clearControls();
            enableControls();
            lblSalaryProfileID.Text = objGenFunc.getMaxRowCount("SalProfileMas", "SalProfileID").Data.ToString();
            txtSalProfCode.Text = "SPF-" + (lblSalaryProfileID.Text.Trim()).ToString().PadLeft(4, '0');
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
            
            int EmpSalDetID = 0;
            //if (lblActionMode.Text == "add")
            //{
            //    foreach (DataGridViewRow dc in dtgSalaryProfileDetails.Rows)
            //    {
            //        if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "allowences")
            //        {
            //            EmpSalDetID = objSalaryProfile.UpdateSalaryProfileDetailInfo(Convert.ToInt16(dc.Cells["SalProDetID"].Value.ToString()), Convert.ToInt16(dc.Cells["SalProfileID"].Value.ToString()), Convert.ToInt16(dc.Cells["HeaderID"].Value.ToString()), 0, 0, Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()));
            //        }
            //        else if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "deductions")
            //        {
            //            EmpSalDetID = objSalaryProfile.UpdateSalaryProfileDetailInfo(Convert.ToInt16(dc.Cells["SalProDetID"].Value.ToString()), Convert.ToInt16(dc.Cells["SalProfileID"].Value.ToString()), 0, Convert.ToInt16(dc.Cells["HeaderID"].Value.ToString()), 0, Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString()));
            //        }
            //        else if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "reimbursement")
            //        {
            //            EmpSalDetID = objSalaryProfile.UpdateSalaryProfileDetailInfo(Convert.ToInt16(dc.Cells["SalProDetID"].Value.ToString()), Convert.ToInt16(dc.Cells["SalProfileID"].Value.ToString()), 0, 0, Convert.ToInt16(dc.Cells["HeaderID"].Value.ToString()), Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString()));
            //        }

            //    }
            //    MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //} else 

            if (lblActionMode.Text == "modify")
            {
                objSalaryProfile.UpdateSalaryProfileInfoAutomaticCalculationStatus(Convert.ToInt16(lblSalaryProfileID.Text.ToString()), chkAutomaticCalculate.Checked);
                foreach (DataGridViewRow dc in dtgSalaryProfileDetails.Rows)
                {
                    if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "allowences")
                    {
                        if(chkAutomaticCalculate.Checked == true)
                        {
                            EmpSalDetID = objSalaryProfile.InsertSalaryProfileDetailInfo(Convert.ToInt16(lblSalaryProfileID.Text.ToString()), Convert.ToInt16(dc.Cells["HeaderID"].Value.ToString()), 0, 0, Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()));
                        }
                        else
                        {
                            if (Convert.ToInt16(dc.Cells["SalProDetID"].Value.ToString()) == 0)
                                EmpSalDetID = objSalaryProfile.InsertSalaryProfileDetailInfo(Convert.ToInt16(lblSalaryProfileID.Text.ToString()), Convert.ToInt16(dc.Cells["HeaderID"].Value.ToString()), 0, 0, Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()));
                            else
                                EmpSalDetID = objSalaryProfile.UpdateSalaryProfileDetailInfo(Convert.ToInt16(dc.Cells["SalProDetID"].Value.ToString()), Convert.ToInt16(dc.Cells["SalProfileID"].Value.ToString()), Convert.ToInt16(dc.Cells["HeaderID"].Value.ToString()), 0, 0, Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()));
                        }
                    }
                    else if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "deductions")
                    {
                        if (chkAutomaticCalculate.Checked == true)
                        {
                            EmpSalDetID = objSalaryProfile.InsertSalaryProfileDetailInfo(Convert.ToInt16(lblSalaryProfileID.Text.ToString()), 0, Convert.ToInt16(dc.Cells["HeaderID"].Value.ToString()), 0, Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString()));
                        }
                        else
                        {
                            if (Convert.ToInt16(dc.Cells["SalProDetID"].Value.ToString()) == 0)
                                EmpSalDetID = objSalaryProfile.InsertSalaryProfileDetailInfo(Convert.ToInt16(lblSalaryProfileID.Text.ToString()), 0, Convert.ToInt16(dc.Cells["HeaderID"].Value.ToString()), 0, Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString()));
                            else
                                EmpSalDetID = objSalaryProfile.UpdateSalaryProfileDetailInfo(Convert.ToInt16(dc.Cells["SalProDetID"].Value.ToString()), Convert.ToInt16(dc.Cells["SalProfileID"].Value.ToString()), 0, Convert.ToInt16(dc.Cells["HeaderID"].Value.ToString()), 0, Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString()));
                        }
                    }
                    else if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "reimbursement")
                    {
                        if (chkAutomaticCalculate.Checked == true)
                        {
                            EmpSalDetID = objSalaryProfile.InsertSalaryProfileDetailInfo(Convert.ToInt16(lblSalaryProfileID.Text.ToString()), 0, 0, Convert.ToInt16(dc.Cells["HeaderID"].Value.ToString()), Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString()));
                        }
                        else
                        {
                            if (Convert.ToInt16(dc.Cells["SalProDetID"].Value.ToString()) == 0)
                                EmpSalDetID = objSalaryProfile.InsertSalaryProfileDetailInfo(Convert.ToInt16(lblSalaryProfileID.Text.ToString()), 0, 0, Convert.ToInt16(dc.Cells["HeaderID"].Value.ToString()), Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString()));
                            else
                                EmpSalDetID = objSalaryProfile.UpdateSalaryProfileDetailInfo(Convert.ToInt16(dc.Cells["SalProDetID"].Value.ToString()), Convert.ToInt16(dc.Cells["SalProfileID"].Value.ToString()), 0, 0, Convert.ToInt16(dc.Cells["HeaderID"].Value.ToString()), Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString()));
                        }
                    }
                }

                MessageBox.Show("Details updated successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            objTempCurrentlyLoggedInUserInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objTempCurrentlyLoggedInUserInfo.EmpID.ToString()));

            onSaveButtonClick();
            disableControls();
            clearControls();
            errValidator.Clear();          
        }

        public void clearControls()
        {
            txtSalProfCode.Text = "";
            txtSalProfCode.ReadOnly = true;
            txtSalProfTitle.Text = "";
            txtAallowences.Text = "0.00";
            txtDeductions.Text = "0.00";
            txtReimbursement.Text = "0.00";
            txtNetSalary.Text = "0.00";
            dtgSalaryProfileDetails.DataSource = objSalaryProfile.GetDefaultSalaryProfileInfo(1);
            FormatTheGrid();
            foreach (DataGridViewRow dc in dtgSalaryProfileDetails.Rows)
            {
                dc.Cells["AllowanceAmount"].ReadOnly = true;
                dc.Cells["DeductionAmount"].ReadOnly = true;
                dc.Cells["ReimbursmentAmount"].ReadOnly = true;
            }
        }

        public void enableControls()
        {
            txtSalProfCode.Enabled = false;
            txtSalProfTitle.Enabled = false;
            txtAallowences.Enabled = false;
            txtDeductions.Enabled = false;
            txtReimbursement.Enabled = false;
            txtSalProfCode.ReadOnly = true;
            txtAallowences.ReadOnly = true;
            txtDeductions.ReadOnly = true;
            txtReimbursement.ReadOnly = true;
            txtNetSalary.ReadOnly = true;
            chkAutomaticCalculate.Enabled = true;
        }

        public void disableControls()
        {
            txtSalProfCode.Enabled = false;
            txtSalProfTitle.Enabled = false;
            txtAallowences.Enabled = false;
            txtDeductions.Enabled = false;
            txtReimbursement.Enabled = false;
            txtNetSalary.Enabled = false;
            chkAutomaticCalculate.Enabled = false;
        }

        public void onGenerateButtonClick()
        {
            lblActionMode.Text = "add";
            lblSalaryProfileID.Text = "";
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
            lblSalaryProfileID.Text = "";
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
            lblSalaryProfileID.Text = "";
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
            lblSalaryProfileID.Text = "";
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
            lblSalaryProfileID.Text = "";
            btnSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void displaySelectedValuesOnUI(SalaryProfileTitleList SalaryProfileInfoModel)
        {
            lblSalaryProfileID.Text = SalaryProfileInfoModel.SalProfileID.ToString();
            txtSalProfCode.Text = SalaryProfileInfoModel.SalProfileCode;
            txtSalProfTitle.Text = SalaryProfileInfoModel.SalProfileTitle;
            chkAutomaticCalculate.Checked = SalaryProfileInfoModel.IsAutomaticCalculation;
            dtgSalaryProfileDetails.DataSource = objSalaryProfile.GetDefaultSalaryProfileInfo(Convert.ToInt16(lblSalaryProfileID.Text.ToString()));
            FormatTheGrid();
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
            chkAutomaticCalculate.Checked = true;
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
            }
            else if (lblActionMode.Text == "delete")
            {
                if (MessageBox.Show("The selected record will be deleted. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MessageBox.Show("Details deleted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                onCancelButtonClick();
                disableControls();
                clearControls();
                lblActionMode.Text = "";
                errValidator.Clear();
            }
        }

        private void FormatTheGrid()
        {
            dtgSalaryProfileDetails.Columns["EmpSalDetID"].Visible = false;
            dtgSalaryProfileDetails.Columns["SalProDetID"].Visible = false;
            dtgSalaryProfileDetails.Columns["SalProfileID"].Visible = false;
            dtgSalaryProfileDetails.Columns["HeaderID"].Visible = false;
            dtgSalaryProfileDetails.Columns["HeaderTitle"].Visible = true;
            dtgSalaryProfileDetails.Columns["HeaderTitle"].Width = 250;
            dtgSalaryProfileDetails.Columns["HeaderType"].Visible = true;
            dtgSalaryProfileDetails.Columns["HeaderType"].Width = 100;
            dtgSalaryProfileDetails.Columns["CalcFormula"].Visible = false;
            dtgSalaryProfileDetails.Columns["CalcFormula"].Width = 150;
            dtgSalaryProfileDetails.Columns["CalcFormula"].ReadOnly = true;
            dtgSalaryProfileDetails.Columns["IsFixed"].Visible = false;
            dtgSalaryProfileDetails.Columns["IsFixed"].Width = 150;
            dtgSalaryProfileDetails.Columns["IsFixed"].ReadOnly = true;
            dtgSalaryProfileDetails.Columns["AllowanceAmount"].Visible = true;
            dtgSalaryProfileDetails.Columns["AllowanceAmount"].Width = 150;
            dtgSalaryProfileDetails.Columns["AllowanceAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgSalaryProfileDetails.Columns["AllowanceAmount"].DefaultCellStyle.Format = "0.00";
            dtgSalaryProfileDetails.Columns["DeductionAmount"].Visible = true;
            dtgSalaryProfileDetails.Columns["DeductionAmount"].Width = 150;
            dtgSalaryProfileDetails.Columns["DeductionAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgSalaryProfileDetails.Columns["DeductionAmount"].DefaultCellStyle.Format = "0.00";
            dtgSalaryProfileDetails.Columns["ReimbursmentAmount"].Visible = true;
            dtgSalaryProfileDetails.Columns["ReimbursmentAmount"].Width = 150;
            dtgSalaryProfileDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgSalaryProfileDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Format = "0.00";
            dtgSalaryProfileDetails.Columns["SalProAmount"].Visible = false;
            dtgSalaryProfileDetails.Columns["OrderID"].Visible = false;

            decimal totalAallowences = 0;
            decimal totalDeductions = 0;
            decimal totalReimbursement = 0;
            decimal NetSalary = 0;

            foreach (DataGridViewRow dc in dtgSalaryProfileDetails.Rows)
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
                if(dc.Cells["CalcFormula"].Value != null )
                    dc.Cells["HeaderTitle"].ToolTipText = dc.Cells["CalcFormula"].Value.ToString();
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
            txtNetSalary.Text = (totalAallowences + totalReimbursement - totalDeductions).ToString("0.00", CultureInfo.InvariantCulture);

            //if(lblActionMode.Text == "")
            //{
            //    foreach (DataGridViewRow dc in dtgSalaryProfileDetails.Rows)
            //    {
            //        dc.Cells["AllowanceAmount"].ReadOnly = true;
            //        dc.Cells["DeductionAmount"].ReadOnly = true;
            //        dc.Cells["ReimbursmentAmount"].ReadOnly = true; 
            //    }
            //}
        }

        private void CalculateRow(int rowIndex)
        {
            var dgv = dtgSalaryProfileDetails;
            if (rowIndex < 0) return;

            var formula = dgv.Rows[rowIndex].Cells["CalcFormula"].Value?.ToString();
            var headerType = dgv.Rows[rowIndex].Cells["HeaderType"].Value?.ToString().ToLower();
            if (string.IsNullOrWhiteSpace(formula)) return;

            // Example: "Basic Salary * 40 / 100"
            string[] parts = formula.Split('*');

            if (formula.Contains("*"))
                parts = formula.Split('*');
            else if (formula.Contains("<"))
                parts = formula.Split('<');

            if (parts.Length < 2) return;

            string headerName = parts[0]; // e.g. "Basic Salary"
            headerName = headerName.ToString().Substring(headerName.IndexOf("(") + 1).ToString().Trim();

            decimal baseValue = 0;
            string expr = "";

            // Find the dependent row
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells["HeaderTitle"].Value?.ToString() == headerName.ToString().Trim())
                {
                    if (row.Cells["HeaderType"].Value.ToString().ToLower() == "allowences")
                    {
                        baseValue = Convert.ToDecimal(row.Cells["AllowanceAmount"].Value ?? 0);
                    }
                    else if (row.Cells["HeaderType"].Value.ToString().ToLower() == "deductions")
                    {
                        baseValue = Convert.ToDecimal(row.Cells["DeductionAmount"].Value ?? 0);
                    }
                    else if (row.Cells["HeaderType"].Value.ToString().ToLower() == "reimbursement")
                    {
                        baseValue = Convert.ToDecimal(row.Cells["ReimbursmentAmount"].Value ?? 0);
                    }
                }

                if (row.Cells["HeaderType"].Value.ToString().ToLower() == headerType && headerType == "allowences")
                {
                    expr = formula.Replace(headerName, baseValue.ToString());

                    var result = new System.Data.DataTable().Compute(expr, null);
                    decimal finalValue = Convert.ToDecimal(result);
                    finalValue = Math.Ceiling((decimal)finalValue);

                    dgv.Rows[rowIndex].Cells["AllowanceAmount"].Value = finalValue;
                    dgv.Rows[rowIndex].Cells["HeaderTitle"].ToolTipText = formula;
                    break;
                }
                else if (row.Cells["HeaderType"].Value.ToString().ToLower() == headerType && headerType == "deductions")
                {
                    expr = formula.Replace(headerName, baseValue.ToString());

                    var result = new System.Data.DataTable().Compute(expr, null);
                    decimal finalValue = Convert.ToDecimal(result);
                    finalValue = Math.Ceiling((decimal)finalValue);

                    dgv.Rows[rowIndex].Cells["DeductionAmount"].Value = finalValue;
                    dgv.Rows[rowIndex].Cells["HeaderTitle"].ToolTipText = formula;
                    break;
                }
                else if (row.Cells["HeaderType"].Value.ToString().ToLower() == headerType && headerType == "reimbursement")
                {
                    expr = formula.Replace(headerName, baseValue.ToString());

                    var result = new System.Data.DataTable().Compute(expr, null);
                    decimal finalValue = Convert.ToDecimal(result);
                    finalValue = Math.Ceiling((decimal)finalValue);

                    dgv.Rows[rowIndex].Cells["ReimbursmentAmount"].Value = finalValue;
                    dgv.Rows[rowIndex].Cells["HeaderTitle"].ToolTipText = formula;
                    break;
                }
            }
        }

        private void AutomaticCalculate()
        {
            foreach (DataGridViewRow dc in dtgSalaryProfileDetails.Rows)
            {
                CalculateRow(dc.Index);
            }
            FormatTheGrid();
        }

        private void dtgSalaryProfileDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            decimal totalAallowences = 0;
            decimal totalDeductions = 0;
            decimal totalReimbursement = 0;

            foreach (DataGridViewRow dc in dtgSalaryProfileDetails.Rows)
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

            txtAallowences.Text = Convert.ToDecimal(totalAallowences.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtDeductions.Text = Convert.ToDecimal(totalDeductions.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
            txtReimbursement.Text = Convert.ToDecimal(totalReimbursement.ToString()).ToString("0.00", CultureInfo.InvariantCulture);
        }

        private void dtgSalaryProfileDetails_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dtgSalaryProfileDetails.CurrentRow.Cells["HeaderType"].Value.ToString().ToLower() == "allowences")
            {
                dtgSalaryProfileDetails.CurrentRow.Cells["AllowanceAmount"].ReadOnly = false;
                dtgSalaryProfileDetails.CurrentRow.Cells["DeductionAmount"].ReadOnly = true;
                dtgSalaryProfileDetails.CurrentRow.Cells["ReimbursmentAmount"].ReadOnly = true;
            }
            else if (dtgSalaryProfileDetails.CurrentRow.Cells["HeaderType"].Value.ToString().ToLower() == "deductions")
            {
                dtgSalaryProfileDetails.CurrentRow.Cells["AllowanceAmount"].ReadOnly = true;
                dtgSalaryProfileDetails.CurrentRow.Cells["DeductionAmount"].ReadOnly = false;
                dtgSalaryProfileDetails.CurrentRow.Cells["ReimbursmentAmount"].ReadOnly = true;
            }
            else if (dtgSalaryProfileDetails.CurrentRow.Cells["HeaderType"].Value.ToString().ToLower() == "reimbursement")
            {
                dtgSalaryProfileDetails.CurrentRow.Cells["AllowanceAmount"].ReadOnly = true;
                dtgSalaryProfileDetails.CurrentRow.Cells["DeductionAmount"].ReadOnly = true;
                dtgSalaryProfileDetails.CurrentRow.Cells["ReimbursmentAmount"].ReadOnly = false;
            }
            if (chkAutomaticCalculate.Checked == true)
            {
                AutomaticCalculate();
            }
            else
            {
                if(lblActionMode.Text == "add")
                {
                    CalculateRow(e.RowIndex);
                }
            }
            FormatTheGrid();
        }

        private void chkAutomaticCalculate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutomaticCalculate.Checked == true)
            {
                AutomaticCalculate();
            }
        }

        private void frmUpdateSalaryProfile_KeyDown(object sender, KeyEventArgs e)
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
