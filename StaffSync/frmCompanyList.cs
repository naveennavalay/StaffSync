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
    public partial class frmCompanyList : Form
    {
        DALStaffSync.clsLastCompanyMas objLastCompany = new DALStaffSync.clsLastCompanyMas();
        DALStaffSync.clsClientInfo objClientInfo = new DALStaffSync.clsClientInfo();

        frmCompanyInfo frmCompanyInfo = null;
        frmOrgMasterInfo frmOrgMasterInfo = null;
        public frmCompanyList()
        {
            InitializeComponent();
        }

        public frmCompanyList(frmCompanyInfo frmCompInfo)
        {
            InitializeComponent();
            this.frmCompanyInfo = frmCompInfo;
        }

        public frmCompanyList(frmOrgMasterInfo frmOrgMastrInfo)
        {
            InitializeComponent();
            this.frmOrgMasterInfo = frmOrgMastrInfo;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCompanyList_Load(object sender, EventArgs e)
        {
            dtgCompanyList.DataSource = objClientInfo.getAllCompanyList();
            dtgCompanyList.Columns["ClientID"].Visible = false;
            dtgCompanyList.Columns["ClientCode"].Width = 100;
            dtgCompanyList.Columns["ClientName"].Width = 250;
            dtgCompanyList.Columns["ClientAddress1"].Width = 200;
            dtgCompanyList.Columns["ClientAddress2"].Width = 200;
            dtgCompanyList.Columns["ClientArea"].Width = 100;
            dtgCompanyList.Columns["ClientState"].Width = 100;
            dtgCompanyList.Columns["ClientPIN"].Width = 100;
            dtgCompanyList.Columns["ClientCountry"].Width = 150;
            dtgCompanyList.Columns["ClientPhone"].Width = 150;
            dtgCompanyList.Columns["ClientContactMail"].Width = 200;
            dtgCompanyList.Columns["ClientWebSite"].Width = 200;
            dtgCompanyList.Columns["ClientContactPerson"].Visible = false;
            dtgCompanyList.Columns["IsActive"].Visible = false;
            dtgCompanyList.Columns["IsDeleted"].Visible = false;
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
                    dtgCompanyList.DataSource = objClientInfo.getAllCompanyList();
                }
                else
                {
                    dtgCompanyList.DataSource = objClientInfo.getAllCompanyList(txtSearch.Text.Trim());
                }

                dtgCompanyList.Columns["ClientID"].Visible = false;
                dtgCompanyList.Columns["ClientCode"].Width = 100;
                dtgCompanyList.Columns["ClientName"].Width = 250;
                dtgCompanyList.Columns["ClientAddress1"].Width = 200;
                dtgCompanyList.Columns["ClientAddress2"].Width = 200;
                dtgCompanyList.Columns["ClientArea"].Width = 100;
                dtgCompanyList.Columns["ClientState"].Width = 100;
                dtgCompanyList.Columns["ClientPIN"].Width = 100;
                dtgCompanyList.Columns["ClientCountry"].Width = 150;
                dtgCompanyList.Columns["ClientPhone"].Width = 150;
                dtgCompanyList.Columns["ClientContactMail"].Width = 200;
                dtgCompanyList.Columns["ClientWebSite"].Width = 200;
                dtgCompanyList.Columns["ClientContactPerson"].Visible = false;
                dtgCompanyList.Columns["IsActive"].Visible = false;
                dtgCompanyList.Columns["IsDeleted"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgDepartmentList_DoubleClick(object sender, EventArgs e)
        {

        }

        private void frmCompanyList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ClientInfo objClientInfo = new ClientInfo();
                objClientInfo.ClientID = 0;
                objClientInfo.ClientCode = "";
                objClientInfo.ClientName = "";
                objClientInfo.ClientAddress1 = "";
                objClientInfo.ClientAddress2 = "";
                objClientInfo.ClientArea = "";
                objClientInfo.ClientCity = "";
                objClientInfo.ClientState = "";
                objClientInfo.ClientPIN = "";
                objClientInfo.ClientCountry = "";
                objClientInfo.ClientPhone = "";
                objClientInfo.ClientContactMail = "";
                objClientInfo.ClientWebSite = "";

                objClientInfo.IsActive = false;
                this.frmCompanyInfo.displaySelectedValuesOnUI(objClientInfo);
                this.Close();
            }
        }

        private void dtgCompanyList_DoubleClick(object sender, EventArgs e)
        {
            ClientInfo objClientInfo = new ClientInfo();
            objClientInfo.ClientID = Convert.ToInt16(dtgCompanyList.SelectedRows[0].Cells["ClientID"].Value.ToString()); ;
            objClientInfo.ClientCode = dtgCompanyList.SelectedRows[0].Cells["ClientCode"].Value.ToString();
            objClientInfo.ClientName = dtgCompanyList.SelectedRows[0].Cells["ClientName"].Value.ToString();
            objClientInfo.ClientAddress1 = dtgCompanyList.SelectedRows[0].Cells["ClientAddress1"].Value.ToString();
            objClientInfo.ClientAddress2 = dtgCompanyList.SelectedRows[0].Cells["ClientAddress2"].Value.ToString();
            objClientInfo.ClientArea = dtgCompanyList.SelectedRows[0].Cells["ClientArea"].Value.ToString();
            objClientInfo.ClientCity = dtgCompanyList.SelectedRows[0].Cells["ClientCity"].Value.ToString();
            objClientInfo.ClientState = dtgCompanyList.SelectedRows[0].Cells["ClientState"].Value.ToString();
            objClientInfo.ClientPIN = dtgCompanyList.SelectedRows[0].Cells["ClientPIN"].Value.ToString();
            objClientInfo.ClientCountry = dtgCompanyList.SelectedRows[0].Cells["ClientCountry"].Value.ToString();
            objClientInfo.ClientPhone = dtgCompanyList.SelectedRows[0].Cells["ClientPhone"].Value.ToString();
            objClientInfo.ClientContactMail = dtgCompanyList.SelectedRows[0].Cells["ClientMailID"].Value.ToString();
            objClientInfo.ClientWebSite = dtgCompanyList.SelectedRows[0].Cells["ClientWebSite"].Value.ToString();
            objClientInfo.IsActive = Convert.ToBoolean(dtgCompanyList.SelectedRows[0].Cells["IsActive"].Value.ToString());

            if (this.frmOrgMasterInfo.lblActionMode.Text == "remove")
                this.frmOrgMasterInfo.lblActionMode.Text = "delete";

            this.frmOrgMasterInfo.displaySelectedValuesOnUI(objClientInfo);
            this.Close();
        }

        private void frmCompanyList_Activated(object sender, EventArgs e)
        {
            dtgCompanyList.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Segoe UI", 8F, FontStyle.Bold);
        }
    }
}
