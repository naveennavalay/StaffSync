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
    public partial class frmAssetInfoList : Form
    {
        //DALStaffSync.clsAssetsCategory objAssetCategory = new DALStaffSync.clsAssetsCategory();
        DALStaffSync.clsAssetsInfo objAssetsInfo = new DALStaffSync.clsAssetsInfo();

        frmAssetsInfo frmAssetsInfo = null;
        public frmAssetInfoList()
        {
            InitializeComponent();
        }

        public frmAssetInfoList(frmAssetsInfo frmAssetInfo, int ClientID)
        {
            InitializeComponent();
            this.frmAssetsInfo = frmAssetInfo;
            lblClientID.Text = ClientID.ToString();
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAssetInfoList_Load(object sender, EventArgs e)
        {
            dtgAssetInfoList.DataSource = objAssetsInfo.getAssetsInfoList(Convert.ToInt32(lblClientID.Text.ToString()));
            FormatGrid();
        }

        private void FormatGrid()
        {
            dtgAssetInfoList.Columns["AssetID"].ReadOnly = true;
            dtgAssetInfoList.Columns["AssetID"].Width = 50;
            dtgAssetInfoList.Columns["AssetID"].Visible = false;

            dtgAssetInfoList.Columns["AssetCode"].ReadOnly = true;
            dtgAssetInfoList.Columns["AssetCode"].Width = 100;

            dtgAssetInfoList.Columns["AssetName"].ReadOnly = true;
            dtgAssetInfoList.Columns["AssetName"].Width = 200;

            dtgAssetInfoList.Columns["AssetDescription"].ReadOnly = true;
            dtgAssetInfoList.Columns["AssetDescription"].Width = 200;

            dtgAssetInfoList.Columns["AssetCategoryName"].ReadOnly = true;
            dtgAssetInfoList.Columns["AssetCategoryName"].Width = 200;

            dtgAssetInfoList.Columns["CurrentAssetStatusName"].ReadOnly = true;
            dtgAssetInfoList.Columns["CurrentAssetStatusName"].Width = 200;

            dtgAssetInfoList.Columns["CurrentAssetDescription"].ReadOnly = true;
            dtgAssetInfoList.Columns["CurrentAssetDescription"].Visible = false;

            dtgAssetInfoList.Columns["IsActive"].Visible = false;
            dtgAssetInfoList.Columns["IsDeleted"].Visible = false;            
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
                    dtgAssetInfoList.DataSource = objAssetsInfo.getAssetsInfoList(Convert.ToInt32(lblClientID.Text.ToString()));
                }
                else
                {
                    dtgAssetInfoList.DataSource = objAssetsInfo.getAssetsInfoFilter(txtSearch.Text.ToString().Trim(), Convert.ToInt32(lblClientID.Text.ToString()));
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
            AssetInfoListing objAssetsInfoModel = new AssetInfoListing();
            objAssetsInfoModel.AssetID = Convert.ToInt16(dtgAssetInfoList.SelectedRows[0].Cells["AssetID"].Value.ToString());
            objAssetsInfoModel.AssetCode = dtgAssetInfoList.SelectedRows[0].Cells["AssetCode"].Value.ToString();
            objAssetsInfoModel.AssetName = dtgAssetInfoList.SelectedRows[0].Cells["AssetName"].Value.ToString();
            objAssetsInfoModel.AssetDescription = dtgAssetInfoList.SelectedRows[0].Cells["AssetDescription"].Value.ToString();
            objAssetsInfoModel.AssetCategoryName = dtgAssetInfoList.SelectedRows[0].Cells["AssetCategoryName"].Value.ToString();
            objAssetsInfoModel.CurrentAssetStatusName = dtgAssetInfoList.SelectedRows[0].Cells["CurrentAssetStatusName"].Value.ToString();

            if (this.frmAssetsInfo.lblActionMode.Text == "remove")
                this.frmAssetsInfo.lblActionMode.Text = "delete";

            this.frmAssetsInfo.displaySelectedValuesOnUI(objAssetsInfoModel);
            this.Close();
        }

        private void frmAssetInfoList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                AssetInfoListing objAssetsInfoModel = new AssetInfoListing();
                objAssetsInfoModel.AssetID = 0;

                this.frmAssetsInfo.displaySelectedValuesOnUI(objAssetsInfoModel);
                this.Close();
            }
        }
    }
}
