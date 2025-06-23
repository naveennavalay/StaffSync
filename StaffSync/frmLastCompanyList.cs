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
    public partial class frmLastCompanyList : Form
    {
        clsLastCompanyMas objLastCompany = new clsLastCompanyMas();

        frmLastCompanyMaster frmLastCompanyMas = null;
        public frmLastCompanyList()
        {
            InitializeComponent();
        }

        public frmLastCompanyList(frmLastCompanyMaster frmLastCompanyMastr)
        {
            InitializeComponent();
            this.frmLastCompanyMas = frmLastCompanyMastr;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLastCompanyList_Load(object sender, EventArgs e)
        {
            dtgDepartmentList.DataSource = objLastCompany.GetLastCompDetMasList();
        }

        private void btnCloseMe_Click_1(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtSearch.Text))
                {
                    dtgDepartmentList.DataSource = objLastCompany.GetLastCompDetMasList();
                }
                else
                {
                    dtgDepartmentList.DataSource = objLastCompany.GetLastCompDetMasList(txtSearch.Text.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgDepartmentList_DoubleClick(object sender, EventArgs e)
        {
            LastCompnayMasterModel objLastCompnayModel = new LastCompnayMasterModel();
            objLastCompnayModel.LastCompanyInfoID = Convert.ToInt16(dtgDepartmentList.SelectedRows[0].Cells["LastCompanyInfoID"].Value.ToString());
            objLastCompnayModel.LastCompanyCode = dtgDepartmentList.SelectedRows[0].Cells["LastCompanyCode"].Value.ToString();
            objLastCompnayModel.LastCompanyTitle = dtgDepartmentList.SelectedRows[0].Cells["LastCompanyTitle"].Value.ToString();
            objLastCompnayModel.LastCompanyAddress = dtgDepartmentList.SelectedRows[0].Cells["Address"].Value.ToString();
            objLastCompnayModel.IsActive = Convert.ToBoolean(dtgDepartmentList.SelectedRows[0].Cells["IsActive"].Value);

            if (this.frmLastCompanyMas.lblActionMode.Text == "remove")
                this.frmLastCompanyMas.lblActionMode.Text = "delete";

            this.frmLastCompanyMas.displaySelectedValuesOnUI(objLastCompnayModel);
            this.Close();
        }

        private void frmLastCompanyList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                LastCompnayMasterModel objLastCompnayModel = new LastCompnayMasterModel();
                objLastCompnayModel.LastCompanyInfoID = 0;
                objLastCompnayModel.LastCompanyCode = "";
                objLastCompnayModel.LastCompanyTitle = "";
                objLastCompnayModel.LastCompanyAddress = "";
                objLastCompnayModel.IsActive = false;
                this.frmLastCompanyMas.displaySelectedValuesOnUI(objLastCompnayModel);
                this.Close();
            }
        }
    }
}
