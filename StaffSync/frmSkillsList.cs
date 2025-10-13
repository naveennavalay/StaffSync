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
    public partial class frmSkillsList : Form
    {
        DALStaffSync.clsSkillsMas clsSkills = new DALStaffSync.clsSkillsMas();
        frmSkillsMaster frmSkillsMas = null;

        public frmSkillsList()
        {
            InitializeComponent();
        }

        public frmSkillsList(frmSkillsMaster frmSkillsMastr)
        {
            InitializeComponent();
            this.frmSkillsMas = frmSkillsMastr;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSkillsList_Load(object sender, EventArgs e)
        {
            dtgStateList.DataSource = clsSkills.GetSkillList();
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
                    dtgStateList.DataSource = clsSkills.GetSkillList();
                }
                else
                {
                    dtgStateList.DataSource = clsSkills.GetSkillList(txtSearch.Text.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgDepartmentList_DoubleClick(object sender, EventArgs e)
        {
            SkillModel objSkillsModel = new SkillModel();
            objSkillsModel.SkillID = Convert.ToInt16(dtgStateList.SelectedRows[0].Cells["SkillID"].Value.ToString());
            objSkillsModel.SkillCode = dtgStateList.SelectedRows[0].Cells["SkillCode"].Value.ToString();
            objSkillsModel.SkillTitle = dtgStateList.SelectedRows[0].Cells["SkillTitle"].Value.ToString();
            objSkillsModel.SkillInitial = dtgStateList.SelectedRows[0].Cells["SkillInitial"].Value.ToString();
            objSkillsModel.IsActive = Convert.ToBoolean(dtgStateList.SelectedRows[0].Cells["IsActive"].Value);

            if (this.frmSkillsMas.lblActionMode.Text == "remove")
                this.frmSkillsMas.lblActionMode.Text = "delete";

            this.frmSkillsMas.displaySelectedValuesOnUI(objSkillsModel);
            this.Close();
        }

        private void frmSkillsList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                SkillModel objSkillsModel = new SkillModel();
                objSkillsModel.SkillID = 0;
                objSkillsModel.SkillCode = "";
                objSkillsModel.SkillTitle = "";
                objSkillsModel.SkillInitial = "";
                objSkillsModel.IsActive = false;
                this.frmSkillsMas.displaySelectedValuesOnUI(objSkillsModel);
                this.Close();
            }
        }

        private void btnCloseMe_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
