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
    public partial class frmDesignationList : Form
    {
        clsDesignation clsDesignation = new clsDesignation();

        frmDesignationMaster frmDesignationMas = null;
        public frmDesignationList()
        {
            InitializeComponent();
        }

        public frmDesignationList(frmDesignationMaster frmDesigMastr)
        {
            InitializeComponent();
            this.frmDesignationMas = frmDesigMastr;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDesignationList_Load(object sender, EventArgs e)
        {
            dtgDesignationList.DataSource = clsDesignation.GetDesignationList();
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
                    dtgDesignationList.DataSource = clsDesignation.GetDesignationList();
                }
                else
                {
                    dtgDesignationList.DataSource = clsDesignation.GetDesignationList(txtSearch.Text.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgDepartmentList_DoubleClick(object sender, EventArgs e)
        {
            DesignationModel objDesignationModel = new DesignationModel();
            objDesignationModel.DesignationID = Convert.ToInt16(dtgDesignationList.SelectedRows[0].Cells["DesignationID"].Value.ToString());
            objDesignationModel.DesignationCode = dtgDesignationList.SelectedRows[0].Cells["DesignationCode"].Value.ToString();
            objDesignationModel.DesignationTitle = dtgDesignationList.SelectedRows[0].Cells["DesignationTitle"].Value.ToString();
            objDesignationModel.DesignationInitial = dtgDesignationList.SelectedRows[0].Cells["DesignationInitial"].Value.ToString();
            objDesignationModel.IsActive = Convert.ToBoolean(dtgDesignationList.SelectedRows[0].Cells["IsActive"].Value);

            if (this.frmDesignationMas.lblActionMode.Text == "remove")
                this.frmDesignationMas.lblActionMode.Text = "delete";

            this.frmDesignationMas.displaySelectedValuesOnUI(objDesignationModel);
            this.Close();
        }

        private void frmDesignationList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DesignationModel objDesignationModel = new DesignationModel();
                objDesignationModel.DesignationID = 0;
                objDesignationModel.DesignationCode = "";
                objDesignationModel.DesignationTitle = "";
                objDesignationModel.DesignationInitial = "";
                objDesignationModel.IsActive = false;
                this.frmDesignationMas.displaySelectedValuesOnUI(objDesignationModel);
                this.Close();
            }
        }
    }
}
