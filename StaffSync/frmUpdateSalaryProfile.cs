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

        clsAllowenceInfo objAllowenceInfo = new clsAllowenceInfo();
        clsDeductionsInfo objDeductionsInfo = new clsDeductionsInfo();
        clsReimbursement objReimbursement = new clsReimbursement();
        clsSalaryProfile objSalaryProfile = new clsSalaryProfile();
        clsEmployeeSalaryProfileInfo objEmployeeSalaryProfileInfo = new clsEmployeeSalaryProfileInfo();
        clsEmpPayroll objEmployeePayroll = new clsEmpPayroll();

        public frmUpdateSalaryProfile()
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
            this.Close();
        }

        private void frmUpdateSalaryProfile_Load(object sender, EventArgs e)
        {
            //// TODO: This line of code loads data into the 'staffsyncDBDTSet.EmpMasInfo' table. You can move, or remove it, as needed.
            //this.empMasInfoTableAdapter.Fill(this.staffsyncDBDTSet.EmpMasInfo);
            dtgSalaryProfileDetails.DataSource = objSalaryProfile.GetDefaultSalaryProfileInfo(1);
            FormatTheGrid();
            onCancelButtonClick();
            disableControls();
            clearControls();
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
            lblSalaryProfileID.Text = objSalaryProfile.getMaxRowCount("SalProfileMas", "SalProfileID").ToString();
            txtSalProfCode.Text = "ALL-" + (lblSalaryProfileID.Text.Trim()).ToString().PadLeft(4, '0');
            errValidator.Clear();
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
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
                foreach (DataGridViewRow dc in dtgSalaryProfileDetails.Rows)
                {
                    if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "allowences")
                    {
                        EmpSalDetID = objSalaryProfile.UpdateSalaryProfileDetailInfo(Convert.ToInt16(dc.Cells["SalProDetID"].Value.ToString()), Convert.ToInt16(dc.Cells["SalProfileID"].Value.ToString()), Convert.ToInt16(dc.Cells["HeaderID"].Value.ToString()), 0, 0, Convert.ToDecimal(dc.Cells["AllowanceAmount"].Value.ToString()));
                    }
                    else if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "deductions")
                    {
                        EmpSalDetID = objSalaryProfile.UpdateSalaryProfileDetailInfo(Convert.ToInt16(dc.Cells["SalProDetID"].Value.ToString()), Convert.ToInt16(dc.Cells["SalProfileID"].Value.ToString()), 0, Convert.ToInt16(dc.Cells["HeaderID"].Value.ToString()), 0, Convert.ToDecimal(dc.Cells["DeductionAmount"].Value.ToString()));
                    }
                    else if (dc.Cells["HeaderType"].Value.ToString().ToLower() == "reimbursement")
                    {
                        EmpSalDetID = objSalaryProfile.UpdateSalaryProfileDetailInfo(Convert.ToInt16(dc.Cells["SalProDetID"].Value.ToString()), Convert.ToInt16(dc.Cells["SalProfileID"].Value.ToString()), 0, 0, Convert.ToInt16(dc.Cells["HeaderID"].Value.ToString()), Convert.ToDecimal(dc.Cells["ReimbursmentAmount"].Value.ToString()));
                    }
                }
                MessageBox.Show("Details updated successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
        }

        public void disableControls()
        {
            txtSalProfCode.Enabled = false;
            txtSalProfTitle.Enabled = false;
            txtAallowences.Enabled = false;
            txtDeductions.Enabled = false;
            txtReimbursement.Enabled = false;
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
            lblActionMode.Text = "modify"; 
            onModifyButtonClick();
            clearControls();
            enableControls();
            errValidator.Clear();
        }

        private void btnRemoveDetails_Click(object sender, EventArgs e)
        {
            if(lblActionMode.Text == "" || lblActionMode.Text == "remove")
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
            dtgSalaryProfileDetails.Columns["AllowanceAmount"].Visible = true;
            dtgSalaryProfileDetails.Columns["AllowanceAmount"].Width = 150;
            dtgSalaryProfileDetails.Columns["AllowanceAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgSalaryProfileDetails.Columns["AllowanceAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryProfileDetails.Columns["DeductionAmount"].Visible = true;
            dtgSalaryProfileDetails.Columns["DeductionAmount"].Width = 150;
            dtgSalaryProfileDetails.Columns["DeductionAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgSalaryProfileDetails.Columns["DeductionAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryProfileDetails.Columns["ReimbursmentAmount"].Visible = true;
            dtgSalaryProfileDetails.Columns["ReimbursmentAmount"].Width = 150;
            dtgSalaryProfileDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
            dtgSalaryProfileDetails.Columns["ReimbursmentAmount"].DefaultCellStyle.Format = "c2";
            dtgSalaryProfileDetails.Columns["SalProAmount"].Visible = false;
            dtgSalaryProfileDetails.Columns["OrderID"].Visible = false;

            decimal totalAallowences = 0;
            decimal totalDeductions = 0;
            decimal totalReimbursement = 0;

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
            txtAallowences.Text = Convert.ToDecimal(totalAallowences.ToString()).ToString("00.00", CultureInfo.InvariantCulture);
            txtDeductions.Text = Convert.ToDecimal(totalDeductions.ToString()).ToString("00.00", CultureInfo.InvariantCulture);
            txtReimbursement.Text = Convert.ToDecimal(totalReimbursement.ToString()).ToString("00.00", CultureInfo.InvariantCulture);

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

            txtAallowences.Text = Convert.ToDecimal(totalAallowences.ToString()).ToString("00.00", CultureInfo.InvariantCulture);
            txtDeductions.Text = Convert.ToDecimal(totalDeductions.ToString()).ToString("00.00", CultureInfo.InvariantCulture);
            txtReimbursement.Text = Convert.ToDecimal(totalReimbursement.ToString()).ToString("00.00", CultureInfo.InvariantCulture);
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
        }
    }
}
