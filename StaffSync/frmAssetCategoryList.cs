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
    public partial class frmAssetCategoryList : Form
    {
        DALStaffSync.clsAssetsCategory objAssetCategory = new DALStaffSync.clsAssetsCategory();
        frmAssetCategory frmAssetCategory = null;
        public frmAssetCategoryList()
        {
            InitializeComponent();
        }

        public frmAssetCategoryList(frmAssetCategory frmAssetCategry, int ClientID)
        {
            InitializeComponent();
            this.frmAssetCategory = frmAssetCategry;
            lblClientID.Text = ClientID.ToString();
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAssetCategoryList_Load(object sender, EventArgs e)
        {
            dtgCategoryList.DataSource = objAssetCategory.getAssetsCategoryList(Convert.ToInt32(lblClientID.Text.ToString()));
            FormatGrid();
        }

        private void FormatGrid()
        {
            dtgCategoryList.Columns["AssetCatMasID"].ReadOnly = true;
            dtgCategoryList.Columns["AssetCatMasID"].Width = 50;
            dtgCategoryList.Columns["AssetCatMasID"].Visible = false;
            dtgCategoryList.Columns["AssetCode"].ReadOnly = true;
            dtgCategoryList.Columns["AssetCode"].Width = 100;
            dtgCategoryList.Columns["AssetName"].ReadOnly = true;
            dtgCategoryList.Columns["AssetName"].Width = 250;
            dtgCategoryList.Columns["AssetDescription"].ReadOnly = true;
            dtgCategoryList.Columns["AssetDescription"].Width = 300;
            dtgCategoryList.Columns["IsActive"].ReadOnly = true;
            dtgCategoryList.Columns["IsActive"].Width = 50;
            dtgCategoryList.Columns["IsActive"].Visible = false;
            dtgCategoryList.Columns["AssetNote"].Visible = false;
            dtgCategoryList.Columns["ClientID"].Visible = false;
            dtgCategoryList.Columns["IsDeleted"].Visible =false;            
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
                    dtgCategoryList.DataSource = objAssetCategory.getAssetsCategoryList(Convert.ToInt32(lblClientID.Text.ToString()));
                }
                else
                {
                    dtgCategoryList.DataSource = objAssetCategory.getAssetsCategoryInfoFilter(txtSearch.Text.ToString().Trim(), Convert.ToInt32(lblClientID.Text.ToString()));
                }
                FormatGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgDepartmentList_DoubleClick(object sender, EventArgs e)
        {
            AssetsCategory objAssetCategoryModel = new AssetsCategory();
            objAssetCategoryModel.AssetCatMasID = Convert.ToInt16(dtgCategoryList.SelectedRows[0].Cells["AssetCatMasID"].Value.ToString());
            objAssetCategoryModel.AssetCode = dtgCategoryList.SelectedRows[0].Cells["AssetCode"].Value.ToString();
            objAssetCategoryModel.AssetName = dtgCategoryList.SelectedRows[0].Cells["AssetName"].Value.ToString();
            objAssetCategoryModel.AssetDescription = dtgCategoryList.SelectedRows[0].Cells["AssetDescription"].Value.ToString();
            objAssetCategoryModel.IsActive = Convert.ToBoolean(dtgCategoryList.SelectedRows[0].Cells["IsActive"].Value.ToString());
            objAssetCategoryModel.IsDeleted = Convert.ToBoolean(dtgCategoryList.SelectedRows[0].Cells["IsDeleted"].Value.ToString());


            if (this.frmAssetCategory.lblActionMode.Text == "remove")
                this.frmAssetCategory.lblActionMode.Text = "delete";

            this.frmAssetCategory.displaySelectedValuesOnUI(objAssetCategoryModel);
            this.Close();
        }

        private void frmAssetCategoryList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                AssetsCategory objAssetCategoryModel = new AssetsCategory();
                objAssetCategoryModel.AssetCatMasID = 0;
                objAssetCategoryModel.AssetCode = "";
                objAssetCategoryModel.AssetName = "";
                objAssetCategoryModel.AssetDescription = "";
                objAssetCategoryModel.IsActive = false;
                this.frmAssetCategory.displaySelectedValuesOnUI(objAssetCategoryModel);
                this.Close();
            }
        }
    }
}
