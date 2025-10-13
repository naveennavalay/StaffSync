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
    public partial class frmDepartmentList : Form
    {
        DALStaffSync.clsDepartment objDepartment = new DALStaffSync.clsDepartment();
        frmDepartmentMaster frmDepMaster = null;

        public frmDepartmentList()
        {
            InitializeComponent();
        }

        public frmDepartmentList(frmDepartmentMaster frmDepMastr)
        {
            InitializeComponent();
            this.frmDepMaster = frmDepMastr;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDepartmentList_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dsDepartmentList.qryDepartmentList' table. You can move, or remove it, as needed.
            this.qryDepartmentListTableAdapter.Fill(this.dsDepartmentList.qryDepartmentList);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    dtgDepartmentList.DataSource = objDepartment.GetDepartmentList();
                }
                else
                {
                    dtgDepartmentList.DataSource = objDepartment.GetDepartmentList(txtSearch.Text.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgDepartmentList_DoubleClick(object sender, EventArgs e)
        {
            DepartmentModel objDepartmentModel = new DepartmentModel();
            objDepartmentModel.DepartmentID = Convert.ToInt16(dtgDepartmentList.SelectedRows[0].Cells["DepartmentID"].Value.ToString());
            objDepartmentModel.DepCode = dtgDepartmentList.SelectedRows[0].Cells["DepCode"].Value.ToString();
            objDepartmentModel.DepartmentTitle = dtgDepartmentList.SelectedRows[0].Cells["DepartmentTitle"].Value.ToString();
            objDepartmentModel.DepartmentInitial = dtgDepartmentList.SelectedRows[0].Cells["DepartmentInitial"].Value.ToString();
            objDepartmentModel.IsActive = Convert.ToBoolean(dtgDepartmentList.SelectedRows[0].Cells["IsActive"].Value);

            if (this.frmDepMaster.lblActionMode.Text == "remove")
                this.frmDepMaster.lblActionMode.Text = "delete";

            this.frmDepMaster.displaySelectedValuesOnUI(objDepartmentModel);
            this.Close();
        }
    }
}
