using Krypton.Toolkit;
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
        DALStaffSync.clsProfessionalTaxCalculation objProfessionalTaxSlab = new DALStaffSync.clsProfessionalTaxCalculation();
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

        public frmStateList(frmStateMaster frmStateMastr, int txtClientID, int txtStateID)
        {
            InitializeComponent();
            this.frmStateMas = frmStateMastr;
            this.lblClientID.Text = txtClientID.ToString();
            this.lblStateID.Text = txtStateID.ToString();
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmStateList_Load(object sender, EventArgs e)
        {
            dtgStateList.DataSource = clsStates.GetStateList();
            DataGridViewImageColumn colStatus = new DataGridViewImageColumn();
            colStatus.Name = "ConfigStatus";
            colStatus.ReadOnly = true;
            colStatus.HeaderText = "Prof. Tax Slab Configured.?";
            colStatus.Width = 125;
            colStatus.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dtgStateList.Columns.Add(colStatus);

            dtgStateList.Columns["StateID"].Visible = false;
            dtgStateList.Columns["StateCode"].ReadOnly = true;
            dtgStateList.Columns["StateCode"].Width = 125;
            dtgStateList.Columns["StateTitle"].ReadOnly = true;
            dtgStateList.Columns["StateTitle"].Width = 300;
            dtgStateList.Columns["StateInitial"].ReadOnly = true;
            dtgStateList.Columns["StateInitial"].Width = 125;
            dtgStateList.Columns["IsConfigured"].Visible = false;
            dtgStateList.Columns["IsActive"].Visible = false;
            dtgStateList.Columns["IsDeleted"].Visible = false;
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

            DataTable tmpStateSpecificProfessionalTaxSlabConfigStatus = objProfessionalTaxSlab.getStateSpecificProfessionalTaxSlabList(Convert.ToInt32(lblClientID.Text.ToString()), Convert.ToInt32(objStateModel.StateID.ToString()));
            if (tmpStateSpecificProfessionalTaxSlabConfigStatus.Rows.Count > 0)
                objStateModel.IsConfigured = true;

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

        private void dtgStateList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dtgStateList.Columns[e.ColumnIndex].Name == "ConfigStatus")
            {
                var row = dtgStateList.Rows[e.RowIndex];

                bool isConfigured = false;

                if (row.Cells["IsConfigured"].Value != DBNull.Value)
                    isConfigured = Convert.ToBoolean(row.Cells["IsConfigured"].Value);

                if (isConfigured)
                {
                    e.Value = ResizeImage(SystemIcons.Shield.ToBitmap(), 16, 16);   // 🛡
                    row.Cells["ConfigStatus"].ToolTipText = "Professional Tax is configured for this state.";
                }
                else
                {
                    e.Value = ResizeImage(SystemIcons.Warning.ToBitmap(), 16, 16);  // ⚠
                    row.Cells["ConfigStatus"].ToolTipText = "Professional Tax slab is not configured for this state.";
                }
            }
        }

        public Image ResizeImage(Image img, int width, int height)
        {
            return new Bitmap(img, new Size(width, height));
        }
    }
}
