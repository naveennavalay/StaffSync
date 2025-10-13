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
    public partial class frmWeeklyProfileMasterList : Form
    {
        //clsRelationship clsRelationship = new clsRelationship();
        //clsLeaveTypeMas clsLeaveTypeMas = new clsLeaveTypeMas();
        //frmLeaveTypeMaster frmLeaveTypeMaster = null;
        //frmEmpLeaveEntitlement frmEmpLeaveEntitlement = null;

        DALStaffSync.clsWeeklyOffInfo objWeeklyOffInfo = new DALStaffSync.clsWeeklyOffInfo();
        frmWeeklyProfileMas frmWeeklyProfileMas1 = new frmWeeklyProfileMas();
        frmWeeklyProfileDetailsInfo frmWeeklyProfileDetailsInfo1 = new frmWeeklyProfileDetailsInfo();
        string ShowListFor = "";


        public frmWeeklyProfileMasterList()
        {
            InitializeComponent();
        }

        public frmWeeklyProfileMasterList(frmWeeklyProfileMas frmWeeklyProfileMaster, string strShowListFor)
        {
            InitializeComponent();
            this.frmWeeklyProfileMas1 = frmWeeklyProfileMaster;
            ShowListFor = strShowListFor;
        }

        public frmWeeklyProfileMasterList(frmWeeklyProfileDetailsInfo frmWeeklyProfileDetailsInfo, string strShowListFor)
        {
            InitializeComponent();
            this.frmWeeklyProfileDetailsInfo1 = frmWeeklyProfileDetailsInfo;
            ShowListFor = strShowListFor;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmWeeklyProfileMasterList_Load(object sender, EventArgs e)
        {
            dtgWeeklyOffList.DataSource = null;
            dtgWeeklyOffList.Columns.Clear();
            dtgWeeklyOffList.Refresh();
            dtgWeeklyOffList.DataSource = objWeeklyOffInfo.getWklyOffProfileMasInfoList("");
            FormatTable();
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
                    dtgWeeklyOffList.DataSource = null;
                    dtgWeeklyOffList.Columns.Clear();
                    dtgWeeklyOffList.Refresh();
                    dtgWeeklyOffList.DataSource = objWeeklyOffInfo.getWklyOffProfileMasInfoList("");
                    FormatTable();
                }
                else
                {
                    dtgWeeklyOffList.DataSource = null;
                    dtgWeeklyOffList.Columns.Clear();
                    dtgWeeklyOffList.Refresh();
                    dtgWeeklyOffList.DataSource = objWeeklyOffInfo.getWklyOffProfileMasInfoList(txtSearch.Text.ToString().Trim());
                    FormatTable();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormatTable()
        {
            if (dtgWeeklyOffList.Columns.Count > 0)
            {
                dtgWeeklyOffList.Columns["WklyOffMasID"].Visible = false;
                dtgWeeklyOffList.Columns["WklyOffCode"].HeaderText = "Weekly Off Code";
                dtgWeeklyOffList.Columns["WklyOffTitle"].HeaderText = "Weekly Off Title";
                //dtgWeeklyOffList.Columns["WklyOffEffectiveDate"].HeaderText = "Effective Date";
                //dtgWeeklyOffList.Columns["WklyOffEffectiveDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
                dtgWeeklyOffList.Columns["WklyOffCode"].Width = 50;
                dtgWeeklyOffList.Columns["WklyOffTitle"].Width = 300;
                dtgWeeklyOffList.Columns["EffectDateFrom"].Width = 150;
                dtgWeeklyOffList.Columns["WklyOffMasID"].ReadOnly = true;
                dtgWeeklyOffList.Columns["WklyOffCode"].ReadOnly = true;
                dtgWeeklyOffList.Columns["WklyOffTitle"].ReadOnly = true;
                dtgWeeklyOffList.Columns["EffectDateFrom"].ReadOnly = true;
                dtgWeeklyOffList.Columns["WklyOffCode"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dtgWeeklyOffList.Columns["WklyOffTitle"].SortMode = DataGridViewColumnSortMode.NotSortable;
                //dtgWeeklyOffList.Columns["WklyOffEffectiveDate"].SortMode = DataGridViewColumnSortMode.NotSortable;
                dtgWeeklyOffList.Columns["IsActive"].Visible = false;
                dtgWeeklyOffList.Columns["IsDelete"].Visible = false;
                dtgWeeklyOffList.AllowUserToAddRows = false;
                dtgWeeklyOffList.AllowUserToDeleteRows = false;
                dtgWeeklyOffList.AllowUserToResizeRows = false;
                dtgWeeklyOffList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dtgWeeklyOffList.MultiSelect = false;
                dtgWeeklyOffList.ReadOnly = true;
                dtgWeeklyOffList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dtgWeeklyOffList.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dtgWeeklyOffList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
                dtgWeeklyOffList.RowHeadersVisible = false;
                dtgWeeklyOffList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                dtgWeeklyOffList.RowHeadersWidth = 20;
            }
        }

        private void dtgDepartmentList_DoubleClick(object sender, EventArgs e)
        {

        }

        private void frmWeeklyProfileMasterList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                WklyOffProfileMasInfo objWklyOffProfileMasInfoModel = new WklyOffProfileMasInfo();
                objWklyOffProfileMasInfoModel.WklyOffMasID = 0;
                objWklyOffProfileMasInfoModel.WklyOffCode = "";
                objWklyOffProfileMasInfoModel.WklyOffTitle = "";
                objWklyOffProfileMasInfoModel.EffectDateFrom = Convert.ToDateTime(DateTime.Today.Date.ToString("dd-MM-yyyy"));
                objWklyOffProfileMasInfoModel.IsActive = false;

                this.frmWeeklyProfileMas1.displaySelectedValuesOnUI(objWklyOffProfileMasInfoModel);

                this.Close();
            }
        }

        private void btnCloseMe_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtgWeeklyOffList_DoubleClick(object sender, EventArgs e)
        {
            WklyOffProfileMasInfo objWklyOffProfileMasInfoModel = new WklyOffProfileMasInfo();
            objWklyOffProfileMasInfoModel.WklyOffMasID = Convert.ToInt16(dtgWeeklyOffList.SelectedRows[0].Cells["WklyOffMasID"].Value.ToString());
            objWklyOffProfileMasInfoModel.WklyOffCode = dtgWeeklyOffList.SelectedRows[0].Cells["WklyOffCode"].Value.ToString();
            objWklyOffProfileMasInfoModel.WklyOffTitle = dtgWeeklyOffList.SelectedRows[0].Cells["WklyOffTitle"].Value.ToString();
            objWklyOffProfileMasInfoModel.EffectDateFrom = Convert.ToDateTime(dtgWeeklyOffList.SelectedRows[0].Cells["EffectDateFrom"].Value);
            objWklyOffProfileMasInfoModel.IsActive = true;

            if(ShowListFor == "weeklyOffProfileMaster")
            {
                if (frmWeeklyProfileMas1.lblActionMode.Text == "remove")
                    frmWeeklyProfileMas1.lblActionMode.Text = "delete";

                frmWeeklyProfileMas1.displaySelectedValuesOnUI(objWklyOffProfileMasInfoModel);
            }
            else if (ShowListFor == "weeklyOffProfileDetails")
            {
                if (frmWeeklyProfileDetailsInfo1.lblActionMode.Text == "remove")
                    frmWeeklyProfileDetailsInfo1.lblActionMode.Text = "delete";

                frmWeeklyProfileDetailsInfo1.displaySelectedValuesOnUI(objWklyOffProfileMasInfoModel);
            }

            this.Close();
        }
    }
}
