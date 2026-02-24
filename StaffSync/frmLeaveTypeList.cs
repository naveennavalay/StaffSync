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
    public partial class frmLeaveTypeList : Form
    {
        //clsRelationship clsRelationship = new clsRelationship();
        DALStaffSync.clsLeaveTypeMas clsLeaveTypeMas = new DALStaffSync.clsLeaveTypeMas();
        frmLeaveTypeMaster frmLeaveTypeMaster = null;
        frmEmpLeaveEntitlement frmEmpLeaveEntitlement = null;

        public frmLeaveTypeList()
        {
            InitializeComponent();
        }

        public frmLeaveTypeList(frmEmpLeaveEntitlement frmEmpLeaveEntitlemnt, string SearchOptionClickedFor)
        {
            InitializeComponent();
            this.frmEmpLeaveEntitlement = frmEmpLeaveEntitlemnt;
            lblSearchOptionClickedFor.Text = SearchOptionClickedFor;
        }

        public frmLeaveTypeList(frmLeaveTypeMaster frmLeaveTypeMastr, string SearchOptionClickedFor)
        {
            InitializeComponent();
            this.frmLeaveTypeMaster = frmLeaveTypeMastr;
            lblSearchOptionClickedFor.Text = SearchOptionClickedFor;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLeaveTypeList_Load(object sender, EventArgs e)
        {
            dtgLeaveTypeList.DataSource = null;
            dtgLeaveTypeList.Columns.Clear();
            dtgLeaveTypeList.Refresh();
            dtgLeaveTypeList.DataSource = clsLeaveTypeMas.GetLeaveTypeList();
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
                    dtgLeaveTypeList.DataSource = null;
                    dtgLeaveTypeList.Columns.Clear();
                    dtgLeaveTypeList.Refresh();
                    dtgLeaveTypeList.DataSource = clsLeaveTypeMas.GetLeaveTypeList();
                    FormatTable();
                }
                else
                {
                    dtgLeaveTypeList.DataSource = null;
                    dtgLeaveTypeList.Columns.Clear();
                    dtgLeaveTypeList.Refresh();
                    dtgLeaveTypeList.DataSource = clsLeaveTypeMas.GetLeaveTypeList(txtSearch.Text.ToString().Trim());
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
            dtgLeaveTypeList.Columns["LeaveTypeID"].Visible = false;
            dtgLeaveTypeList.Columns["LeaveCode"].HeaderText = "Leave Code";
            dtgLeaveTypeList.Columns["LeaveCode"].Width = 100;
            dtgLeaveTypeList.Columns["LeaveTypeTitle"].HeaderText = "Leave Type Title";
            dtgLeaveTypeList.Columns["LeaveTypeTitle"].Width = 250;
            dtgLeaveTypeList.Columns["IsPaid"].HeaderText = "Is Paid Leave?";
            dtgLeaveTypeList.Columns["IsActive"].HeaderText = "Is Active?";
            dtgLeaveTypeList.Columns["IsPaid"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgLeaveTypeList.Columns["IsActive"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dtgLeaveTypeList.Columns["IsDelete"].Visible = false;
            dtgLeaveTypeList.Columns["OrderID"].Visible = false;
        }

        private void dtgDepartmentList_DoubleClick(object sender, EventArgs e)
        {
            LeaveTypeInfoModel objLeaveTypeInfoModel = new LeaveTypeInfoModel();
            objLeaveTypeInfoModel.LeaveTypeID = Convert.ToInt16(dtgLeaveTypeList.SelectedRows[0].Cells["LeaveTypeID"].Value.ToString());
            objLeaveTypeInfoModel.LeaveCode = dtgLeaveTypeList.SelectedRows[0].Cells["LeaveCode"].Value.ToString();
            objLeaveTypeInfoModel.LeaveTypeTitle = dtgLeaveTypeList.SelectedRows[0].Cells["LeaveTypeTitle"].Value.ToString();
            objLeaveTypeInfoModel.IsPaid = Convert.ToBoolean(dtgLeaveTypeList.SelectedRows[0].Cells["IsPaid"].Value);
            objLeaveTypeInfoModel.IsActive = Convert.ToBoolean(dtgLeaveTypeList.SelectedRows[0].Cells["IsActive"].Value);

            if (lblSearchOptionClickedFor.Text.Trim() == "listEmpLeaveEntitlements")
            {
                if (this.frmEmpLeaveEntitlement.lblActionMode.Text == "remove")
                    this.frmEmpLeaveEntitlement.lblActionMode.Text = "delete";

                this.frmEmpLeaveEntitlement.displaySelectedValuesOnUI(objLeaveTypeInfoModel);

            }
            else if (lblSearchOptionClickedFor.Text.Trim() == "listLeaveTypeMaster")
            {
                if (this.frmLeaveTypeMaster.lblActionMode.Text == "remove")
                    this.frmLeaveTypeMaster.lblActionMode.Text = "delete";

                this.frmLeaveTypeMaster.displaySelectedValuesOnUI(objLeaveTypeInfoModel);
            }

            this.Close();
        }

        private void frmLeaveTypeList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                LeaveTypeInfoModel objLeaveTypeInfoModel = new LeaveTypeInfoModel();
                objLeaveTypeInfoModel.LeaveTypeID = 0;
                objLeaveTypeInfoModel.LeaveCode = "";
                objLeaveTypeInfoModel.LeaveTypeTitle = "";
                objLeaveTypeInfoModel.IsPaid = false;
                objLeaveTypeInfoModel.IsActive = false;

                if (lblSearchOptionClickedFor.Text.Trim() == "listEmpLeaveEntitlements")
                {
                    //this.frmEmpLeaveEntitlement.displaySelectedValuesOnUI(objLeaveTypeInfoModel);
                }
                else if (lblSearchOptionClickedFor.Text.Trim() == "listLeaveTypeMaster")
                {
                    this.frmLeaveTypeMaster.displaySelectedValuesOnUI(objLeaveTypeInfoModel);
                }
                
                this.Close();
            }
        }

        private void btnCloseMe_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLeaveTypeList_Activated(object sender, EventArgs e)
        {
            //dtgLeaveTypeList.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
        }
    }
}
