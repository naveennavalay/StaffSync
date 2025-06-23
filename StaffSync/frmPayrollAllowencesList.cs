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
    public partial class frmPayrollAllowencesList : Form
    {
        clsDepartment objDepartment = new clsDepartment();
        clsAllowenceInfo objAllowenceInfo = new clsAllowenceInfo();
        frmPayrollAllowences frmPayrollAllowence = null;

        public frmPayrollAllowencesList()
        {
            InitializeComponent();
        }

        public frmPayrollAllowencesList(frmPayrollAllowences frmAllMastr)
        {
            InitializeComponent();
            this.frmPayrollAllowence = frmAllMastr;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPayrollAllowencesList_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dsDepartmentList.qryDepartmentList' table. You can move, or remove it, as needed.
            //this.qryDepartmentListTableAdapter.Fill(this.dsDepartmentList.qryDepartmentList);
            dtgAllowancesList.DataSource = objAllowenceInfo.GetAllowenceList();
            dtgAllowancesList.Columns["AllID"].Visible = false;
            dtgAllowancesList.Columns["AllCode"].Width = 150;
            dtgAllowancesList.Columns["AllTitle"].Width = 250;
            dtgAllowancesList.Columns["AllDescription"].Width = 350;
            dtgAllowancesList.Columns["IsActive"].Visible = false;
            dtgAllowancesList.Columns["IsDeleted"].Visible = false;
            dtgAllowancesList.Columns["OrderID"].Visible = false;
            dtgAllowancesList.Columns["CalcFormula"].Visible = false;
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    dtgAllowancesList.DataSource = objAllowenceInfo.GetAllowenceList();
                }
                else
                {
                    dtgAllowancesList.DataSource = objAllowenceInfo.GetAllowenceList(txtSearch.Text.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgDepartmentList_DoubleClick(object sender, EventArgs e)
        {
            AllowenceModel objAllowenceModel = new AllowenceModel();
            objAllowenceModel.AllID = Convert.ToInt16(dtgAllowancesList.SelectedRows[0].Cells["AllID"].Value.ToString());
            objAllowenceModel.AllCode = dtgAllowancesList.SelectedRows[0].Cells["AllCode"].Value.ToString();
            objAllowenceModel.AllTitle = dtgAllowancesList.SelectedRows[0].Cells["AllTitle"].Value.ToString();
            objAllowenceModel.AllDescription = dtgAllowancesList.SelectedRows[0].Cells["AllDescription"].Value.ToString();
            objAllowenceModel.IsActive = Convert.ToBoolean(dtgAllowancesList.SelectedRows[0].Cells["IsActive"].Value);

            if (this.frmPayrollAllowence.lblActionMode.Text == "remove")
                this.frmPayrollAllowence.lblActionMode.Text = "delete";

            this.frmPayrollAllowence.displaySelectedValuesOnUI(objAllowenceModel);
            this.Close();
        }
    }
}
