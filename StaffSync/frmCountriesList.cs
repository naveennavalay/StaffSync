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
    public partial class frmCountriesList : Form
    {
        DALStaffSync.clsCountries objCountries = new DALStaffSync.clsCountries();

        frmCountryMaster frmCountryMas = null;
        public frmCountriesList()
        {
            InitializeComponent();
        }

        public frmCountriesList(frmCountryMaster frmContMastr)
        {
            InitializeComponent();
            this.frmCountryMas = frmContMastr;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCountriesList_Load(object sender, EventArgs e)
        {
            dtgDepartmentList.DataSource = objCountries.GetCountryList();
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
                    dtgDepartmentList.DataSource = objCountries.GetCountryList();
                }
                else
                {
                    dtgDepartmentList.DataSource = objCountries.GetCountryList(txtSearch.Text.ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgDepartmentList_DoubleClick(object sender, EventArgs e)
        {
            CountriesModel objCountryModel = new CountriesModel();
            objCountryModel.CountryID = Convert.ToInt16(dtgDepartmentList.SelectedRows[0].Cells["CountryID"].Value.ToString());
            objCountryModel.CountryCode = dtgDepartmentList.SelectedRows[0].Cells["CountryCode"].Value.ToString();
            objCountryModel.CountryTitle = dtgDepartmentList.SelectedRows[0].Cells["CountryTitle"].Value.ToString();
            objCountryModel.CountryInitial = dtgDepartmentList.SelectedRows[0].Cells["CountryInitial"].Value.ToString();
            objCountryModel.IsActive = Convert.ToBoolean(dtgDepartmentList.SelectedRows[0].Cells["IsActive"].Value);

            if (this.frmCountryMas.lblActionMode.Text == "remove")
                this.frmCountryMas.lblActionMode.Text = "delete";

            this.frmCountryMas.displaySelectedValuesOnUI(objCountryModel);
            this.Close();
        }

        private void frmCountriesList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                CountriesModel objCountryModel = new CountriesModel();
                objCountryModel.CountryID = 0;
                objCountryModel.CountryCode = "";
                objCountryModel.CountryTitle = "";
                objCountryModel.CountryInitial = "";
                objCountryModel.IsActive = false;
                this.frmCountryMas.displaySelectedValuesOnUI(objCountryModel);
                this.Close();
            }
        }
    }
}
