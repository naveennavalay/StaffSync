using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public partial class frmPayrollDeductionsList : Form
    {
        DALStaffSync.clsDeductionsInfo objDeductionsInfo = new DALStaffSync.clsDeductionsInfo();
        frmPayrollDeductions frmPayrollDeduction = null;

        public frmPayrollDeductionsList()
        {
            InitializeComponent();
        }

        public frmPayrollDeductionsList(frmPayrollDeductions frmDedMastr)
        {
            InitializeComponent();
            this.frmPayrollDeduction = frmDedMastr;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPayrollDeductionsList_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dsDepartmentList.qryDepartmentList' table. You can move, or remove it, as needed.
            //this.qryDepartmentListTableAdapter.Fill(this.dsDepartmentList.qryDepartmentList);
            dtgDeductionsList.DataSource = objDeductionsInfo.GetDeductionList();
            dtgDeductionsList.Columns["DedID"].Visible = false;
            dtgDeductionsList.Columns["DedCode"].Width = 150;
            dtgDeductionsList.Columns["DedTitle"].Width = 250;
            dtgDeductionsList.Columns["DedDescription"].Width = 350;
            dtgDeductionsList.Columns["IsActive"].Visible = false;
            dtgDeductionsList.Columns["IsDeleted"].Visible = false;
            dtgDeductionsList.Columns["OrderID"].Visible = false;
            dtgDeductionsList.Columns["CalcFormula"].Visible = false;
            dtgDeductionsList.Columns["MaxCap"].Visible = false;
            dtgDeductionsList.Columns["IsFixed"].Visible = false;
            dtgDeductionsList.Columns["VisibleInPayslip"].Visible = false;
            dtgDeductionsList.Columns["ProrataBasis"].Visible = false;
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    dtgDeductionsList.DataSource = objDeductionsInfo.GetDeductionList();
                }
                else
                {
                    dtgDeductionsList.DataSource = objDeductionsInfo.GetDeductionList(txtSearch.Text.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgDepartmentList_DoubleClick(object sender, EventArgs e)
        {
            DeductionModel objDeductionModel = new DeductionModel();
            objDeductionModel.DedID = Convert.ToInt16(dtgDeductionsList.SelectedRows[0].Cells["DedID"].Value.ToString());
            objDeductionModel.DedCode = dtgDeductionsList.SelectedRows[0].Cells["DedCode"].Value.ToString();
            objDeductionModel.DedTitle = dtgDeductionsList.SelectedRows[0].Cells["DedTitle"].Value.ToString();
            objDeductionModel.DedDescription = dtgDeductionsList.SelectedRows[0].Cells["DedDescription"].Value.ToString();
            objDeductionModel.IsFixed = Convert.ToBoolean(dtgDeductionsList.SelectedRows[0].Cells["IsFixed"].Value);
            objDeductionModel.IsActive = Convert.ToBoolean(dtgDeductionsList.SelectedRows[0].Cells["IsActive"].Value);
            objDeductionModel.MaxCap = Convert.ToDecimal(dtgDeductionsList.SelectedRows[0].Cells["MaxCap"].Value);
            objDeductionModel.VisibleInPayslip = Convert.ToBoolean(dtgDeductionsList.SelectedRows[0].Cells["VisibleInPayslip"].Value);
            objDeductionModel.ProrataBasis = Convert.ToBoolean(dtgDeductionsList.SelectedRows[0].Cells["ProrataBasis"].Value);

            if (this.frmPayrollDeduction.lblActionMode.Text == "remove")
                this.frmPayrollDeduction.lblActionMode.Text = "delete";

            this.frmPayrollDeduction.displaySelectedValuesOnUI(objDeductionModel);
            this.Close();
        }

        private void frmPayrollDeductionsList_Activated(object sender, EventArgs e)
        {
            //dtgDeductionsList.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
        }
    }
}
