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
    public partial class frmAdvanceTypeMasList : Form
    {
        //DALStaffSync.clsCountries objCountries = new DALStaffSync.clsCountries();
        DALStaffSync.clsAdvanceTypeMas objAdvanceTypeMas = new DALStaffSync.clsAdvanceTypeMas();

        //frmCountryMaster frmCountryMas = null;
        frmAdvanceTypeMas frmAdvanceTypeMas = null;
        public frmAdvanceTypeMasList()
        {
            InitializeComponent();
        }

        public frmAdvanceTypeMasList(frmAdvanceTypeMas frmAdvanceTypeMaster)
        {
            InitializeComponent();
            this.frmAdvanceTypeMas = frmAdvanceTypeMaster;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAdvanceTypeMasList_Load(object sender, EventArgs e)
        {
            dtgEmployeeList.DataSource = objAdvanceTypeMas.GetAdvanceTypeList(1);
            FormatTheGrid();
        }

        private void btnCloseMe_Click_1(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    dtgEmployeeList.DataSource = objAdvanceTypeMas.GetAdvanceTypeList(1);
                }
                else
                {
                    dtgEmployeeList.DataSource = objAdvanceTypeMas.GetAdvanceTypeList(1, txtSearch.Text.ToString().Trim());
                }
                FormatTheGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgEmployeeList_DoubleClick(object sender, EventArgs e)
        {
            AdvanceTypesModel objAdvanceTypesModel = new AdvanceTypesModel();
            objAdvanceTypesModel.AdvanceTypeID = Convert.ToInt16(dtgEmployeeList.SelectedRows[0].Cells["AdvanceTypeID"].Value.ToString());
            objAdvanceTypesModel.AdvanceTypeCode = dtgEmployeeList.SelectedRows[0].Cells["AdvanceTypeCode"].Value.ToString();
            objAdvanceTypesModel.AdvanceTypeTitle = dtgEmployeeList.SelectedRows[0].Cells["AdvanceTypeTitle"].Value.ToString();
            objAdvanceTypesModel.IsActive = Convert.ToBoolean(dtgEmployeeList.SelectedRows[0].Cells["IsActive"].Value);
            objAdvanceTypesModel.IsDeleted = Convert.ToBoolean(dtgEmployeeList.SelectedRows[0].Cells["IsDeleted"].Value);

            if (this.frmAdvanceTypeMas.lblActionMode.Text == "remove")
                this.frmAdvanceTypeMas.lblActionMode.Text = "delete";

            this.frmAdvanceTypeMas.displaySelectedValuesOnUI(objAdvanceTypesModel);
            this.Close();
        }

        private void frmAdvanceTypeMasList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                AdvanceTypesModel objAdvanceTypesModel = new AdvanceTypesModel();
                objAdvanceTypesModel.AdvanceTypeID = 0;
                objAdvanceTypesModel.AdvanceTypeCode = string.Empty;
                objAdvanceTypesModel.AdvanceTypeTitle = string.Empty;
                objAdvanceTypesModel.IsActive = false;

                this.frmAdvanceTypeMas.displaySelectedValuesOnUI(objAdvanceTypesModel);
                this.Close();
            }
        }

        private void FormatTheGrid()
        {
            try
            {
                dtgEmployeeList.Columns["AdvanceTypeID"].Visible = false;
                dtgEmployeeList.Columns["AdvanceTypeCode"].HeaderText = "Advance Code";
                dtgEmployeeList.Columns["AdvanceTypeCode"].Width = 100;
                dtgEmployeeList.Columns["AdvanceTypeTitle"].HeaderText = "Advance Title";
                dtgEmployeeList.Columns["AdvanceTypeTitle"].Width = 350;
                dtgEmployeeList.Columns["IsActive"].Visible = false;
                dtgEmployeeList.Columns["IsDeleted"].Visible = false;
                dtgEmployeeList.Columns["OrderID"].Visible = false;
                dtgEmployeeList.Columns["ClientID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
