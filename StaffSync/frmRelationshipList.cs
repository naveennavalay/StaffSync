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
    public partial class frmRelationshipList : Form
    {
        DALStaffSync.clsRelationship clsRelationship = new DALStaffSync.clsRelationship();
        frmRelationshipMaster frmRelationshipMas = null;

        public frmRelationshipList()
        {
            InitializeComponent();
        }

        public frmRelationshipList(frmRelationshipMaster frmRelationshipMastr)
        {
            InitializeComponent();
            this.frmRelationshipMas = frmRelationshipMastr;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmRelationshipList_Load(object sender, EventArgs e)
        {
            dtgStateList.DataSource = clsRelationship.GetRelationshipList();
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
                    dtgStateList.DataSource = clsRelationship.GetRelationshipList();
                }
                else
                {
                    dtgStateList.DataSource = clsRelationship.GetRelationshipList(txtSearch.Text.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgDepartmentList_DoubleClick(object sender, EventArgs e)
        {
            RelationshipModel objRelationshipModel = new RelationshipModel();
            objRelationshipModel.RelationshipID = Convert.ToInt16(dtgStateList.SelectedRows[0].Cells["RelationshipID"].Value.ToString());
            objRelationshipModel.RelationshipCode= dtgStateList.SelectedRows[0].Cells["RelationshipCode"].Value.ToString();
            objRelationshipModel.RelationshipTitle = dtgStateList.SelectedRows[0].Cells["RelationshipTitle"].Value.ToString();
            objRelationshipModel.RelationshipInitial = dtgStateList.SelectedRows[0].Cells["RelationshipInitial"].Value.ToString();
            objRelationshipModel.IsActive = Convert.ToBoolean(dtgStateList.SelectedRows[0].Cells["IsActive"].Value);

            if (this.frmRelationshipMas.lblActionMode.Text == "remove")
                this.frmRelationshipMas.lblActionMode.Text = "delete";

            this.frmRelationshipMas.displaySelectedValuesOnUI(objRelationshipModel);
            this.Close();
        }

        private void frmRelationshipList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                RelationshipModel objRelationshipModel = new RelationshipModel();
                objRelationshipModel.RelationshipID = 0;
                objRelationshipModel.RelationshipCode = "";
                objRelationshipModel.RelationshipTitle = "";
                objRelationshipModel.RelationshipInitial = "";
                objRelationshipModel.IsActive = false;
                this.frmRelationshipMas.displaySelectedValuesOnUI(objRelationshipModel);
                this.Close();
            }
        }

        private void btnCloseMe_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
