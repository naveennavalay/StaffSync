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
    public partial class frmSalaryProfileList : Form
    {
        DALStaffSync.clsSalaryProfile objSalaryProfile = new DALStaffSync.clsSalaryProfile();
        frmSalaryProfile frmSalaryProfile = null;
        frmUpdateSalaryProfile frmUpdateSalaryProfile = null;
        string strFormName = string.Empty;

        public frmSalaryProfileList()
        {
            InitializeComponent();
        }

        public frmSalaryProfileList(frmSalaryProfile frmSalaryProfile)
        {
            InitializeComponent();
            this.frmSalaryProfile = frmSalaryProfile;
            strFormName = "SalaryProfile";
        }

        public frmSalaryProfileList(frmUpdateSalaryProfile frmUpdateSalaryProfle)
        {
            InitializeComponent();
            this.frmUpdateSalaryProfile = frmUpdateSalaryProfle;
            strFormName = "UpdateSalaryProfile";
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSalaryProfileList_Load(object sender, EventArgs e)
        {
            dtgSalaryProfileList.DataSource = objSalaryProfile.GetSalProfileTitleList();
            FormatTheGrid();
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
                    dtgSalaryProfileList.DataSource = objSalaryProfile.GetSalProfileTitleList();
                }
                else
                {
                    dtgSalaryProfileList.DataSource = objSalaryProfile.GetSalProfileTitleList(txtSearch.Text.ToString().Trim());
                }
                FormatTheGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgDepartmentList_DoubleClick(object sender, EventArgs e)
        {
            SalaryProfileTitleList objSalaryProfileTitleList = new SalaryProfileTitleList();
            objSalaryProfileTitleList.SalProfileID = Convert.ToInt16(dtgSalaryProfileList.SelectedRows[0].Cells["SalProfileID"].Value.ToString());
            objSalaryProfileTitleList.SalProfileCode = dtgSalaryProfileList.SelectedRows[0].Cells["SalProfileCode"].Value.ToString();
            objSalaryProfileTitleList.SalProfileTitle = dtgSalaryProfileList.SelectedRows[0].Cells["SalProfileTitle"].Value.ToString();
            objSalaryProfileTitleList.SalProfileDescription = dtgSalaryProfileList.SelectedRows[0].Cells["SalProfileDescription"].Value.ToString();
            objSalaryProfileTitleList.IsActive = Convert.ToBoolean(dtgSalaryProfileList.SelectedRows[0].Cells["IsActive"].Value.ToString());
            objSalaryProfileTitleList.IsDeleted = Convert.ToBoolean(dtgSalaryProfileList.SelectedRows[0].Cells["IsDeleted"].Value.ToString());
            objSalaryProfileTitleList.OrderID = Convert.ToInt16(dtgSalaryProfileList.SelectedRows[0].Cells["OrderID"].Value.ToString());
            objSalaryProfileTitleList.IsAutomaticCalculation = Convert.ToBoolean(dtgSalaryProfileList.SelectedRows[0].Cells["IsAutomaticCalculation"].Value.ToString());
            

            if (strFormName == "UpdateSalaryProfile")
            {
                this.frmUpdateSalaryProfile.displaySelectedValuesOnUI(objSalaryProfileTitleList);
            }
            else if (strFormName == "SalaryProfile")
            {
                if (this.frmSalaryProfile.lblActionMode.Text == "remove")
                    this.frmSalaryProfile.lblActionMode.Text = "delete";

                this.frmSalaryProfile.displaySelectedValuesOnUI(objSalaryProfileTitleList);
            }
            this.Close();
        }

        private void frmSalaryProfileList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                SalaryProfileTitleList objSalaryProfileInfoList = new SalaryProfileTitleList();
                this.frmSalaryProfile.displaySelectedValuesOnUI(objSalaryProfileInfoList);
                this.Close();
            }
        }

        private void btnCloseMe_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormatTheGrid()
        {
            dtgSalaryProfileList.Columns["SalProfileID"].Visible = false;
            dtgSalaryProfileList.Columns["SalProfileCode"].HeaderText = "Salary Profile Code";
            dtgSalaryProfileList.Columns["SalProfileCode"].Width = 150;
            dtgSalaryProfileList.Columns["SalProfileTitle"].HeaderText = "Salary Profile Title";
            dtgSalaryProfileList.Columns["SalProfileTitle"].Width = 300;
            dtgSalaryProfileList.Columns["SalProfileDescription"].HeaderText = "Salary Profile Description";
            dtgSalaryProfileList.Columns["SalProfileDescription"].Width = 500;
            dtgSalaryProfileList.Columns["IsActive"].Visible = false;
            dtgSalaryProfileList.Columns["IsDeleted"].Visible = false;
            dtgSalaryProfileList.Columns["IsDefault"].Visible = false;
            dtgSalaryProfileList.Columns["OrderID"].Visible = false;
            dtgSalaryProfileList.Columns["IsAutomaticCalculation"].Visible = false;
        }
    }
}
