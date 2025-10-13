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
    public partial class frmStateList : Form
    {
        DALStaffSync.clsStates clsStates = new DALStaffSync.clsStates();
        frmStateMaster frmStateMas = null;

        public frmStateList()
        {
            InitializeComponent();
        }

        public frmStateList(frmStateMaster frmStateMastr)
        {
            InitializeComponent();
            this.frmStateMas = frmStateMastr;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmStateList_Load(object sender, EventArgs e)
        {
            dtgStateList.DataSource = clsStates.GetStateList();
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
                    dtgStateList.DataSource = clsStates.GetStateList();
                }
                else
                {
                    dtgStateList.DataSource = clsStates.GetStateList(txtSearch.Text.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgDepartmentList_DoubleClick(object sender, EventArgs e)
        {
            StateModel objStateModel = new StateModel();
            objStateModel.StateID = Convert.ToInt16(dtgStateList.SelectedRows[0].Cells["StateID"].Value.ToString());
            objStateModel.StateCode= dtgStateList.SelectedRows[0].Cells["StateCode"].Value.ToString();
            objStateModel.StateTitle = dtgStateList.SelectedRows[0].Cells["StateTitle"].Value.ToString();
            objStateModel.StateInitial = dtgStateList.SelectedRows[0].Cells["StateInitial"].Value.ToString();
            objStateModel.IsActive = Convert.ToBoolean(dtgStateList.SelectedRows[0].Cells["IsActive"].Value);

            if (this.frmStateMas.lblActionMode.Text == "remove")
                this.frmStateMas.lblActionMode.Text = "delete";

            this.frmStateMas.displaySelectedValuesOnUI(objStateModel);
            this.Close();
        }

        private void frmStateList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                StateModel objStateModel = new StateModel();
                objStateModel.StateID = 0;
                objStateModel.StateCode = "";
                objStateModel.StateTitle = "";
                objStateModel.StateInitial = "";
                objStateModel.IsActive = false;
                this.frmStateMas.displaySelectedValuesOnUI(objStateModel);
                this.Close();
            }
        }

        private void btnCloseMe_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
