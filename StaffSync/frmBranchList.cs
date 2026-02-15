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
    public partial class frmBranchList : Form
    {
        //DALStaffSync.clsLastCompanyMas objLastCompany = new DALStaffSync.clsLastCompanyMas();
        //DALStaffSync.clsClientInfo objClientInfo = new DALStaffSync.clsClientInfo();
        DALStaffSync.clsClientBranchInfo objClientBranchInfo = new DALStaffSync.clsClientBranchInfo();
        frmBranchInfo frmBranchInfo = null;
        int txtClientID = 0;

        public frmBranchList()
        {
            InitializeComponent();
        }

        public frmBranchList(frmBranchInfo frmBrancInfo, int txtClientID)
        {
            InitializeComponent();
            this.frmBranchInfo = frmBrancInfo;
            this.txtClientID = txtClientID;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmBranchList_Load(object sender, EventArgs e)
        {
            dtgCompanyList.DataSource = objClientBranchInfo.getAllCompanyList(txtClientID);
            dtgCompanyList.Columns["ClientBranchID"].Visible = false;
            dtgCompanyList.Columns["ClientID"].Visible = false;
            dtgCompanyList.Columns["ClientBranchCode"].Width = 100;
            dtgCompanyList.Columns["ClientBranchName"].Width = 250;
            dtgCompanyList.Columns["ClientBranchAddress1"].Width = 200;
            dtgCompanyList.Columns["ClientBranchAddress2"].Width = 200;
            dtgCompanyList.Columns["ClientBranchArea"].Width = 100;
            dtgCompanyList.Columns["ClientBranchState"].Width = 100;
            dtgCompanyList.Columns["ClientBranchPIN"].Width = 100;
            dtgCompanyList.Columns["ClientBranchCountry"].Width = 150;
            dtgCompanyList.Columns["ClientBranchContactNumber"].Width = 150;
            dtgCompanyList.Columns["ClientBranchPhone"].Width = 150;
            dtgCompanyList.Columns["ClientBranchContactMail"].Width = 200;
            dtgCompanyList.Columns["ClientBranchWebSite"].Width = 200;
            dtgCompanyList.Columns["ClientBranchContactPerson"].Visible = false;
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
                txtClientID = 1;
                if (string.IsNullOrEmpty(txtSearch.Text))
                {
                    dtgCompanyList.DataSource = objClientBranchInfo.getAllCompanyList(txtClientID);
                }
                else
                {
                    dtgCompanyList.DataSource = objClientBranchInfo.getAllCompanyList(txtClientID, txtSearch.Text.Trim());
                }

                dtgCompanyList.Columns["ClientBranchID"].Visible = false;
                dtgCompanyList.Columns["ClientID"].Visible = false;
                dtgCompanyList.Columns["ClientBranchCode"].Width = 100;
                dtgCompanyList.Columns["ClientBranchName"].Width = 250;
                dtgCompanyList.Columns["ClientBranchAddress1"].Width = 200;
                dtgCompanyList.Columns["ClientBranchAddress2"].Width = 200;
                dtgCompanyList.Columns["ClientBranchArea"].Width = 100;
                dtgCompanyList.Columns["ClientBranchState"].Width = 100;
                dtgCompanyList.Columns["ClientBranchPIN"].Width = 100;
                dtgCompanyList.Columns["ClientBranchCountry"].Width = 150;
                dtgCompanyList.Columns["ClientBranchContactNumber"].Width = 150;
                dtgCompanyList.Columns["ClientBranchPhone"].Width = 150;
                dtgCompanyList.Columns["ClientBranchContactMail"].Width = 200;
                dtgCompanyList.Columns["ClientBranchWebSite"].Width = 200;
                dtgCompanyList.Columns["ClientBranchContactPerson"].Visible = false;
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

        private void frmBranchList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ClientBranchInfo objClientBranchInfo = new ClientBranchInfo();
                objClientBranchInfo.ClientBranchID = 0;
                objClientBranchInfo.ClientID = 0;
                objClientBranchInfo.ClientBranchCode = "";
                objClientBranchInfo.ClientBranchName = "";
                objClientBranchInfo.ClientBranchAddress1 = "";
                objClientBranchInfo.ClientBranchAddress2 = "";
                objClientBranchInfo.ClientBranchArea = "";
                objClientBranchInfo.ClientBranchCity = "";
                objClientBranchInfo.ClientBranchState = "";
                objClientBranchInfo.ClientBranchPIN = "";
                objClientBranchInfo.ClientBranchCountry = "";
                objClientBranchInfo.ClientBranchPhone = "";
                objClientBranchInfo.ClientBranchContactPerson = "";
                objClientBranchInfo.ClientBranchContactMail = "";
                objClientBranchInfo.ClientBranchWebSite = "";
                objClientBranchInfo.IsActive = false;
                this.frmBranchInfo.displaySelectedValuesOnUI(objClientBranchInfo);
                this.Close();
            }
        }

        private void dtgCompanyList_DoubleClick(object sender, EventArgs e)
        {
            ClientBranchInfo objClientBranchInfo = new ClientBranchInfo();
            objClientBranchInfo.ClientBranchID = Convert.ToInt16(dtgCompanyList.SelectedRows[0].Cells["ClientBranchID"].Value.ToString());
            objClientBranchInfo.ClientID = Convert.ToInt16(dtgCompanyList.SelectedRows[0].Cells["ClientID"].Value.ToString());
            objClientBranchInfo.ClientBranchCode = dtgCompanyList.SelectedRows[0].Cells["ClientBranchCode"].Value.ToString();
            objClientBranchInfo.ClientBranchName = dtgCompanyList.SelectedRows[0].Cells["ClientBranchName"].Value.ToString();
            objClientBranchInfo.ClientBranchAddress1 = dtgCompanyList.SelectedRows[0].Cells["ClientBranchAddress1"].Value.ToString();
            objClientBranchInfo.ClientBranchAddress2 = dtgCompanyList.SelectedRows[0].Cells["ClientBranchAddress2"].Value.ToString();
            objClientBranchInfo.ClientBranchArea = dtgCompanyList.SelectedRows[0].Cells["ClientBranchArea"].Value.ToString();
            objClientBranchInfo.ClientBranchCity = dtgCompanyList.SelectedRows[0].Cells["ClientBranchCity"].Value.ToString();
            objClientBranchInfo.ClientBranchState = dtgCompanyList.SelectedRows[0].Cells["ClientBranchState"].Value.ToString();
            objClientBranchInfo.ClientBranchPIN = dtgCompanyList.SelectedRows[0].Cells["ClientBranchPIN"].Value.ToString();
            objClientBranchInfo.ClientBranchCountry = dtgCompanyList.SelectedRows[0].Cells["ClientBranchCountry"].Value.ToString();
            objClientBranchInfo.ClientBranchContactPerson = dtgCompanyList.SelectedRows[0].Cells["ClientBranchContactPerson"].Value.ToString();
            objClientBranchInfo.ClientBranchPhone = dtgCompanyList.SelectedRows[0].Cells["ClientBranchPhone"].Value.ToString();
            objClientBranchInfo.ClientBranchContactMail = dtgCompanyList.SelectedRows[0].Cells["ClientBranchMailID"].Value.ToString();
            objClientBranchInfo.ClientBranchWebSite = dtgCompanyList.SelectedRows[0].Cells["ClientBranchWebSite"].Value.ToString();
            objClientBranchInfo.IsActive = Convert.ToBoolean(dtgCompanyList.SelectedRows[0].Cells["IsActive"].Value.ToString());

            if (this.frmBranchInfo.lblActionMode.Text == "remove")
                this.frmBranchInfo.lblActionMode.Text = "delete";

            this.frmBranchInfo.displaySelectedValuesOnUI(objClientBranchInfo);
            this.Close();
        }
    }
}
