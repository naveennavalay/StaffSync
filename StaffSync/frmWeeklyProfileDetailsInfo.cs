using ModelStaffSync;
using StaffSync.StaffsyncDBDataSetTableAdapters;
using StaffSync.StaffsyncDBDTSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StaffSync
{
    public partial class frmWeeklyProfileDetailsInfo : Form
    {
        //DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        DALStaffSync.clsLogin objLogin = new DALStaffSync.clsLogin();
        DALStaffSync.clsWeeklyOffInfo objWeeklyOffInfo = new DALStaffSync.clsWeeklyOffInfo();
        List<WklyOffProfileDetailsInfo> lstWeeklyOffDetailsInfo = new List<WklyOffProfileDetailsInfo>();
        frmDashboard objDashboard = (frmDashboard) System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();

        public frmWeeklyProfileDetailsInfo()
        {
            InitializeComponent();
        }

        public frmWeeklyProfileDetailsInfo(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        public frmWeeklyProfileDetailsInfo(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            if (lblActionMode.Text != "")
            {
                if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            objDashboard.lblDashboardTitle.Text = "Dashboard";
            objDashboard.sptrDashboardContainer.Visible = true;
            this.Close();
        }

        private void frmWeeklyProfileDetailsInfo_Load(object sender, EventArgs e)
        {
            //// TODO: This line of code loads data into the 'staffsyncDBDTSet.EmpMasInfo' table. You can move, or remove it, as needed.
            //this.empMasInfoTableAdapter.Fill(this.staffsyncDBDTSet.EmpMasInfo);
            lstWeeklyOffDetailsInfo = objWeeklyOffInfo.getWeeklyOffDetailsInfo(1);
            FormatTheGrid();
            onCancelButtonClick();
            disableControls();
            clearControls();
        }

        private void btnCloseMe_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbRelationship_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmWeeklyProfileMasterList frmWeeklyProfileMasList = new frmWeeklyProfileMasterList(this, "weeklyOffProfileDetails");
            frmWeeklyProfileMasList.ShowDialog(this);
        }

        private void btnCloseMe_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGenerateDetails_Click(object sender, EventArgs e)
        {
            lblActionMode.Text = "add";
            onGenerateButtonClick();
            clearControls();
            enableControls();
            errValidator.Clear();
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            string strValidationMessage = objLogin.ValidateUserRolesAndResponsibilitiesInfo(objTempCurrentlyLoggedInUserInfo.EmpID, "");
            if (strValidationMessage != "Success")
            {
                MessageBox.Show(strValidationMessage, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            int WeeklyOffDayID = 0;
            if (lblActionMode.Text == "modify")
            {
                foreach (DataGridViewRow dc in dtgWeeklyOffDetails.Rows)
                {
                    if(Convert.ToBoolean(dc.Cells["Select"].Value.ToString()) == true && Convert.ToInt16(dc.Cells["WklyOffDetID"].Value.ToString()) == 0)
                        WeeklyOffDayID = objWeeklyOffInfo.InsertWeeklyOffDetailInfo(Convert.ToInt16(lblWeeklyOffID.Text.ToString()), Convert.ToInt16(dc.Cells["WklyOffDay"].Value.ToString()), 0);
                    else if (Convert.ToBoolean(dc.Cells["Select"].Value.ToString()) == true && Convert.ToInt16(dc.Cells["WklyOffDetID"].Value.ToString()) != 0)
                        WeeklyOffDayID = objWeeklyOffInfo.UpdateWeeklyOffDetailInfo(Convert.ToInt16(dc.Cells["WklyOffDetID"].Value.ToString()), Convert.ToInt16(lblWeeklyOffID.Text.ToString()), Convert.ToInt16(dc.Cells["WklyOffDay"].Value.ToString()), Convert.ToInt16(dc.Cells["OrderID"].Value.ToString()));
                    else if (Convert.ToBoolean(dc.Cells["Select"].Value.ToString()) == false && Convert.ToInt16(dc.Cells["WklyOffDetID"].Value.ToString()) != 0)
                        WeeklyOffDayID = objWeeklyOffInfo.DeleteWeeklyOffDetailInfo(Convert.ToInt16(dc.Cells["WklyOffDetID"].Value.ToString()));
                }
                MessageBox.Show("Details updated successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
            objTempCurrentlyLoggedInUserInfo = objLogin.GetUserRolesAndResponsibilitiesInfo(Convert.ToInt16(objTempCurrentlyLoggedInUserInfo.EmpID.ToString()));

            onSaveButtonClick();
            disableControls();
            clearControls();
            errValidator.Clear();          
        }

        public void clearControls()
        {
            txtWeeklyCode.Text = "";
            txtWeeklyCode.ReadOnly = true;
            txtWeeklyOffTitle.Text = "";
            FormatTheGrid();
        }

        public void enableControls()
        {
            txtWeeklyCode.Enabled = false;
            txtWeeklyOffTitle.Enabled = false;
            txtWeeklyCode.ReadOnly = true;
            dtgWeeklyOffDetails.Enabled = true;
        }

        public void disableControls()
        {
            txtWeeklyCode.Enabled = false;
            txtWeeklyOffTitle.Enabled = false;
            dtgWeeklyOffDetails.Enabled = false;
        }

        public void onGenerateButtonClick()
        {
            lblActionMode.Text = "add";
            lblWeeklyOffID.Text = "";
            btnSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = false;
            btnCancel.Enabled = true;

        }

        public void onModifyButtonClick()
        {
            lblActionMode.Text = "modify";
            lblWeeklyOffID.Text = "";
            btnSearch.Enabled = true;
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = true;
            btnRemoveDetails.Enabled = false;
            btnCancel.Enabled = true;
        }

        public void onRemoveButtonClick()
        {
            lblActionMode.Text = "remove";
            lblWeeklyOffID.Text = "";
            btnSearch.Enabled = true;
            btnGenerateDetails.Enabled = false;
            btnModifyDetails.Enabled = false;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onSaveButtonClick()
        {
            lblActionMode.Text = "";
            lblWeeklyOffID.Text = "";
            btnSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void onCancelButtonClick()
        {
            lblActionMode.Text = "";
            lblWeeklyOffID.Text = "";
            btnSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void displaySelectedValuesOnUI(WklyOffProfileMasInfo WklyOffProfileMasInfoModel)
        {
            lblWeeklyOffID.Text = WklyOffProfileMasInfoModel.WklyOffMasID.ToString();
            txtWeeklyCode.Text = WklyOffProfileMasInfoModel.WklyOffCode;
            txtWeeklyOffTitle.Text = WklyOffProfileMasInfoModel.WklyOffTitle;
            FormatTheGrid();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (lblActionMode.Text == "add")
            {
                if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            else if (lblActionMode.Text == "modify")
            {
                if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            else if (lblActionMode.Text == "delete")
            {
                if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            lblActionMode.Text = "";
            onCancelButtonClick();
            disableControls();
            clearControls();
            errValidator.Clear();

        }

        private void btnModifyDetails_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            string strValidationMessage = objLogin.ValidateUserRolesAndResponsibilitiesInfo(objTempCurrentlyLoggedInUserInfo.EmpID, "update");
            if (strValidationMessage != "Success")
            {
                MessageBox.Show(strValidationMessage, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            lblActionMode.Text = "modify"; 
            onModifyButtonClick();
            clearControls();
            enableControls();
            errValidator.Clear();
        }

        private void btnRemoveDetails_Click(object sender, EventArgs e)
        {
            if (CurrentUser.ClientID == 0)
            {
                MessageBox.Show("Please select client and financial year from dashboard.", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            string strValidationMessage = objLogin.ValidateUserRolesAndResponsibilitiesInfo(objTempCurrentlyLoggedInUserInfo.EmpID, "delete");
            if (strValidationMessage != "Success")
            {
                MessageBox.Show(strValidationMessage, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (lblActionMode.Text == "" || lblActionMode.Text == "remove")
            {
                lblActionMode.Text = "remove";
                onRemoveButtonClick();
                clearControls();
                enableControls();
            }
            else if (lblActionMode.Text == "delete")
            {
                if (MessageBox.Show("The selected record will be deleted. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MessageBox.Show("Details deleted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                onCancelButtonClick();
                disableControls();
                clearControls();
                lblActionMode.Text = "";
                errValidator.Clear();
            }
        }

        private void FormatTheGrid()
        {
            //dtgWeeklyOffDetails.Columns.Add("WklyOffDetID", "WklyOffDetID");
            //dtgWeeklyOffDetails.Columns.Add("WklyOffDayID", "DayID");
            //dtgWeeklyOffDetails.Columns.Add("WklyOffDayName", "Day Name");
            //dtgWeeklyOffDetails.Columns.Add("OrderID", "Order ID");

            if (lblWeeklyOffID.Text.ToString().Trim() == "")
                lblWeeklyOffID.Text = "0";

            dtgWeeklyOffDetails.Rows.Clear();
            string[] weekDays = new CultureInfo("en-us").DateTimeFormat.DayNames;

            lstWeeklyOffDetailsInfo = objWeeklyOffInfo.getWeeklyOffDetailsInfo(Convert.ToInt16(lblWeeklyOffID.Text));
            for (int i = 1; i <= 7; i++)
            {
                List<WklyOffProfileDetailsInfo> objWklyOffProfileDetailsInfoList = lstWeeklyOffDetailsInfo.Where(x => x.WklyOffDay == i).ToList();
                if(objWklyOffProfileDetailsInfoList.Count > 0)
                    dtgWeeklyOffDetails.Rows.Add(new object[] { true, objWklyOffProfileDetailsInfoList[0].WklyOffDetID, objWklyOffProfileDetailsInfoList[0].WklyOffDay, weekDays[i % 7], objWklyOffProfileDetailsInfoList[0].WklyOffOrderID });
                else
                    dtgWeeklyOffDetails.Rows.Add(new object[] { false, 0, i, weekDays[i % 7], 0 });
            }

            dtgWeeklyOffDetails.Columns["Select"].Visible = true;
            dtgWeeklyOffDetails.Columns["WklyOffDetID"].Visible = false;
            dtgWeeklyOffDetails.Columns["WklyOffDay"].Visible = false;
            dtgWeeklyOffDetails.Columns["WklyOffDayName"].Visible = true;
            dtgWeeklyOffDetails.Columns["OrderID"].Visible = false;
            dtgWeeklyOffDetails.Columns["Select"].Width = 125;
            dtgWeeklyOffDetails.Columns["WklyOffDetID"].Width = 150;
            dtgWeeklyOffDetails.Columns["WklyOffDay"].Width = 150;
            dtgWeeklyOffDetails.Columns["WklyOffDayName"].Width = 250;
            dtgWeeklyOffDetails.Columns["OrderID"].Width = 150;
        }

        private void dtgSalaryProfileDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dtgSalaryProfileDetails_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
        }

        private void frmWeeklyProfileDetailsInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (lblActionMode.Text != "")
                {
                    if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }
                objDashboard.lblDashboardTitle.Text = "Dashboard";
                objDashboard.sptrDashboardContainer.Visible = true;
                this.Close();
            }
        }

        private void frmWeeklyProfileDetailsInfo_Activated(object sender, EventArgs e)
        {
            dtgWeeklyOffDetails.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
        }
    }
}
