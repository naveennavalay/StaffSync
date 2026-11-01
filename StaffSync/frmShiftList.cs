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
    public partial class frmShiftList : Form
    {
        DALStaffSync.clsShiftMas clsShift = new DALStaffSync.clsShiftMas();
        frmShiftMaster frmShiftMas = null;

        public frmShiftList()
        {
            InitializeComponent();
        }

        public frmShiftList(frmShiftMaster frmShiftMastr)
        {
            InitializeComponent();
            this.frmShiftMas = frmShiftMastr;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShiftList_Load(object sender, EventArgs e)
        {
            FocusManager.EnableHighlighting = false;
            FocusManager.ShowNavigationError = true;
            FocusManager.Register(this);
            FocusManager.SetFocus(btnCloseMe);

            dtgStateList.DataSource = null;
            dtgStateList.DataSource = clsShift.GetShiftList();
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
                    dtgStateList.DataSource = clsShift.GetShiftList();
                }
                else
                {
                    dtgStateList.DataSource = clsShift.GetShiftList();
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
            dtgStateList.Columns["ShiftID"].Visible = false;
            dtgStateList.Columns["ShiftCode"].HeaderText = "Shift Code"; 
            dtgStateList.Columns["ShiftCode"].ReadOnly = true;
            dtgStateList.Columns["ShiftCode"].Width = 100;
            dtgStateList.Columns["ShiftTitle"].HeaderText = "Shift Title";
            dtgStateList.Columns["ShiftTitle"].ReadOnly = true;
            dtgStateList.Columns["ShiftTitle"].Width = 250;
            dtgStateList.Columns["ShiftInitital"].HeaderText = "Shift Initial";
            dtgStateList.Columns["ShiftInitital"].ReadOnly = true;
            dtgStateList.Columns["ShiftInitital"].Width = 100;
            dtgStateList.Columns["ShiftStart"].HeaderText = "Shift Start";
            dtgStateList.Columns["ShiftStart"].ReadOnly = true;
            dtgStateList.Columns["ShiftStart"].Width = 100;
            dtgStateList.Columns["ShiftStart"].DefaultCellStyle.Format = "hh:mm:ss";
            dtgStateList.Columns["ShiftEnd"].HeaderText = "Shift End";
            dtgStateList.Columns["ShiftEnd"].ReadOnly = true;
            dtgStateList.Columns["ShiftEnd"].Width = 100;
            dtgStateList.Columns["ShiftEnd"].DefaultCellStyle.Format = "hh:mm:ss";
            dtgStateList.Columns["GracePeriod"].HeaderText = "Grace Period";
            dtgStateList.Columns["GracePeriod"].ReadOnly = true;
            dtgStateList.Columns["GracePeriod"].Width = 100;
            dtgStateList.Columns["GracePeriod"].DefaultCellStyle.Format = "mm:ss";
            dtgStateList.Columns["IsActive"].Visible = false;
            dtgStateList.Columns["IsDeleted"].Visible = false;
        }

        private void dtgDepartmentList_DoubleClick(object sender, EventArgs e)
        {
            ShiftInfo objShiftInfo = new ShiftInfo();
            objShiftInfo.ShiftID = Convert.ToInt16(dtgStateList.SelectedRows[0].Cells["ShiftID"].Value.ToString());
            objShiftInfo.ShiftCode = dtgStateList.SelectedRows[0].Cells["ShiftCode"].Value.ToString();
            objShiftInfo.ShiftTitle = dtgStateList.SelectedRows[0].Cells["ShiftTitle"].Value.ToString();
            objShiftInfo.ShiftInitital = dtgStateList.SelectedRows[0].Cells["ShiftInitital"].Value.ToString();
            objShiftInfo.ShiftStart = Convert.ToDateTime(dtgStateList.SelectedRows[0].Cells["ShiftStart"].Value.ToString());
            objShiftInfo.ShiftEnd = Convert.ToDateTime(dtgStateList.SelectedRows[0].Cells["ShiftEnd"].Value.ToString());
            objShiftInfo.GracePeriod = Convert.ToDateTime(dtgStateList.SelectedRows[0].Cells["GracePeriod"].Value.ToString());

            objShiftInfo.IsActive = Convert.ToBoolean(dtgStateList.SelectedRows[0].Cells["IsActive"].Value);

            if (this.frmShiftMas.lblActionMode.Text == "remove")
                this.frmShiftMas.lblActionMode.Text = "delete";

            this.frmShiftMas.displaySelectedValuesOnUI(objShiftInfo);
            this.Close();
        }

        private void frmShiftList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ShiftInfo objShiftInfo = new ShiftInfo();
                objShiftInfo.ShiftID = 0;
                objShiftInfo.ShiftCode = "";
                objShiftInfo.ShiftTitle = "";
                objShiftInfo.ShiftInitital = "";
                objShiftInfo.ShiftStart = DateTime.Now;
                objShiftInfo.ShiftEnd = DateTime.Now;
                objShiftInfo.GracePeriod = DateTime.Now;
                objShiftInfo.IsActive = false;
                this.frmShiftMas.displaySelectedValuesOnUI(objShiftInfo);
                this.Close();
            }
        }

        private void btnCloseMe_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
