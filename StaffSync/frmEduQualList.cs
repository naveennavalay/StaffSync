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
    public partial class frmEduQualList : Form
    {
        DALStaffSync.clsEduQalification objEduQalification = new DALStaffSync.clsEduQalification();

        frmEduQualMaster frmEduQualMas = null;
        public frmEduQualList()
        {
            InitializeComponent();
        }

        public frmEduQualList(frmEduQualMaster frmEduQualMastr)
        {
            InitializeComponent();
            this.frmEduQualMas = frmEduQualMastr;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEduQualList_Load(object sender, EventArgs e)
        {
            dtgDepartmentList.DataSource = objEduQalification.GetEduQualMasList();
            FormatGrid();
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
                    dtgDepartmentList.DataSource = objEduQalification.GetEduQualMasList();
                }
                else
                {
                    dtgDepartmentList.DataSource = objEduQalification.GetEduQualMasList(txtSearch.Text.ToString().Trim());
                }
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatGrid()
        {
            dtgDepartmentList.Columns["EduQualID"].Visible = false;
            dtgDepartmentList.Columns["EduQualCode"].ReadOnly = true;
            dtgDepartmentList.Columns["EduQualCode"].Width = 100;
            dtgDepartmentList.Columns["EduQualTitle"].ReadOnly = true;
            dtgDepartmentList.Columns["EduQualTitle"].Width = 350;
            dtgDepartmentList.Columns["EduQualInitial"].ReadOnly = true;
            dtgDepartmentList.Columns["EduQualInitial"].Width = 100;
            dtgDepartmentList.Columns["IsActive"].Visible = false;
            dtgDepartmentList.Columns["IsDeleted"].Visible = false;
        }

        private void dtgDepartmentList_DoubleClick(object sender, EventArgs e)
        {
            CountriesModel objCountryModel = new CountriesModel();
            objCountryModel.CountryID = Convert.ToInt16(dtgDepartmentList.SelectedRows[0].Cells["EduQualID"].Value.ToString());
            objCountryModel.CountryCode = dtgDepartmentList.SelectedRows[0].Cells["EduQualCode"].Value.ToString();
            objCountryModel.CountryTitle = dtgDepartmentList.SelectedRows[0].Cells["EduQualTitle"].Value.ToString();
            objCountryModel.CountryInitial = dtgDepartmentList.SelectedRows[0].Cells["EduQualInitial"].Value.ToString();
            objCountryModel.IsActive = Convert.ToBoolean(dtgDepartmentList.SelectedRows[0].Cells["IsActive"].Value);

            if (this.frmEduQualMas.lblActionMode.Text == "remove")
                this.frmEduQualMas.lblActionMode.Text = "delete";

            this.frmEduQualMas.displaySelectedValuesOnUI(objCountryModel);
            this.Close();
        }

        private void frmEduQualList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                CountriesModel objCountryModel = new CountriesModel();
                objCountryModel.CountryID = 0;
                objCountryModel.CountryCode = "";
                objCountryModel.CountryTitle = "";
                objCountryModel.CountryInitial = "";
                objCountryModel.IsActive = false;
                this.frmEduQualMas.displaySelectedValuesOnUI(objCountryModel);
                this.Close();
            }
        }
    }
}
