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
    public partial class frmPayrollReimbursementList : Form
    {
        clsDepartment objDepartment = new clsDepartment();
        //clsDeductionsInfo objDeductionsInfo = new clsDeductionsInfo();
        clsReimbursement objReimbursement = new clsReimbursement();
        frmReimbursement frmReimbursement = null;

        public frmPayrollReimbursementList()
        {
            InitializeComponent();
        }

        public frmPayrollReimbursementList(frmReimbursement frmReimbMastr)
        {
            InitializeComponent();
            this.frmReimbursement = frmReimbMastr;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPayrollReimbursementList_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dsDepartmentList.qryDepartmentList' table. You can move, or remove it, as needed.
            //this.qryDepartmentListTableAdapter.Fill(this.dsDepartmentList.qryDepartmentList);
            dtgReimbursementList.DataSource = objReimbursement.GetReimbursementList();
            dtgReimbursementList.Columns["ReimbID"].Visible = false;
            dtgReimbursementList.Columns["ReimbCode"].Width = 150;
            dtgReimbursementList.Columns["ReimbTitle"].Width = 250;
            dtgReimbursementList.Columns["ReimbDescription"].Width = 350;
            dtgReimbursementList.Columns["IsActive"].Visible = false;
            dtgReimbursementList.Columns["IsDeleted"].Visible = false;
            dtgReimbursementList.Columns["OrderID"].Visible = false;
            dtgReimbursementList.Columns["CalcFormula"].Visible = false;
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    dtgReimbursementList.DataSource = objReimbursement.GetReimbursementList();
                }
                else
                {
                    dtgReimbursementList.DataSource = objReimbursement.GetReimbursementList(txtSearch.Text.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgDepartmentList_DoubleClick(object sender, EventArgs e)
        {
            ReimbursementModel objReimbursementModel = new ReimbursementModel();
            objReimbursementModel.ReimbID = Convert.ToInt16(dtgReimbursementList.SelectedRows[0].Cells["ReimbID"].Value.ToString());
            objReimbursementModel.ReimbCode = dtgReimbursementList.SelectedRows[0].Cells["ReimbCode"].Value.ToString();
            objReimbursementModel.ReimbTitle = dtgReimbursementList.SelectedRows[0].Cells["ReimbTitle"].Value.ToString();
            objReimbursementModel.ReimbDescription= dtgReimbursementList.SelectedRows[0].Cells["ReimbDescription"].Value.ToString();
            objReimbursementModel.IsFixed = Convert.ToBoolean(dtgReimbursementList.SelectedRows[0].Cells["IsFixed"].Value);
            objReimbursementModel.IsActive = Convert.ToBoolean(dtgReimbursementList.SelectedRows[0].Cells["IsActive"].Value);

            if (this.frmReimbursement.lblActionMode.Text == "remove")
                this.frmReimbursement.lblActionMode.Text = "delete";

            this.frmReimbursement.displaySelectedValuesOnUI(objReimbursementModel);
            this.Close();
        }
    }
}
