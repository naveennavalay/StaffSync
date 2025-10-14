using StaffSync.StaffsyncDBDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using StaffSync.StaffsyncDBDTSetTableAdapters;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ModelStaffSync;

namespace StaffSync
{
    public partial class frmCompanyInfo : Form
    {
        DALStaffSync.clsGenFunc objGenFunc = new DALStaffSync.clsGenFunc();
        DALStaffSync.clsCountries objCountries = new DALStaffSync.clsCountries();
        DALStaffSync.clsClientInfo objClientInfo = new DALStaffSync.clsClientInfo();
        frmDashboard objDashboard = (frmDashboard) System.Windows.Forms.Application.OpenForms["frmDashboard"];

        public frmCompanyInfo()
        {
            InitializeComponent();
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
            this.Close();
        }

        private void frmCompanyInfo_Load(object sender, EventArgs e)
        {
            //// TODO: This line of code loads data into the 'staffsyncDBDTSet.EmpMasInfo' table. You can move, or remove it, as needed.
            //this.empMasInfoTableAdapter.Fill(this.staffsyncDBDTSet.EmpMasInfo);
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
            frmCompanyList frmCompanyList = new frmCompanyList(this);
            frmCompanyList.ShowDialog(this);
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
            cmbIsActive.SelectedIndex = 1;
            lblCompID.Text = objGenFunc.getMaxRowCount("ClientMas", "ClientID").Data.ToString();
            txtCompCode.Text = "CNT-" + (lblCompID.Text.Trim()).ToString().PadLeft(4, '0');
            errValidator.Clear();

            cmbCountry.DataSource = objCountries.GetCountryList();
            cmbCountry.DisplayMember = "CountryTitle";
            cmbCountry.ValueMember = "CountryID";
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCompanyName.Text))
            {
                txtCompanyName.Focus();
                errValidator.SetError(this.txtCompanyName, "Please enter Company Name");
            }
            else
            {
                if (lblActionMode.Text == "add")
                {
                    int newID = objClientInfo.InsertClientInfo(txtCompCode.Text, txtCompanyName.Text, txtAddress01.Text, txtAddress02.Text, txtArea.Text, txtCity.Text, txtState.Text, txtPIN.Text, cmbCountry.Text, txtContactNumber.Text, txtMailID.Text, "Contact Person", "Contact Person Number", "Contact Person MailID", txtWebsite.Text, cmbIsActive.Text.Trim() == "Yes" ? true : false, false);
                    if (newID > 0)
                        MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (lblActionMode.Text == "modify")
                {
                    int affectedRows = objClientInfo.UpdateClientInfo(Convert.ToInt16(lblCompID.Text), txtCompCode.Text, txtCompanyName.Text, txtAddress01.Text, txtAddress02.Text, txtArea.Text, txtCity.Text, txtState.Text, txtPIN.Text, cmbCountry.Text, txtContactNumber.Text, txtMailID.Text, "Contact Person", "Contact Person Number", "Contact Person MailID", txtWebsite.Text, cmbIsActive.Text.Trim() == "Yes" ? true : false, false);
                    if (affectedRows > 0)
                        MessageBox.Show("Details updated successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                onSaveButtonClick();
                disableControls();
                clearControls();
                errValidator.Clear();
            }
        }

        public void clearControls()
        {
            txtCompCode.Text = "";
            txtCompCode.ReadOnly = true;
            txtCompanyName.Text = "";
            txtAddress01.Text = "";
            txtAddress02.Text = "";
            txtArea.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtPIN.Text = "";
            cmbCountry.Text = "";
            txtContactNumber.Text = "";
            txtMailID.Text = "";
            txtWebsite.Text = "";

            picCompLogo.Image = null;

            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.SelectedIndex = 0;
        }

        public void enableControls()
        {
            txtCompCode.Enabled = true;
            txtCompCode.ReadOnly = true;
            txtCompanyName.Enabled = true;
            txtAddress01.Enabled = true;
            txtAddress02.Enabled = true;
            txtArea.Enabled = true;
            txtCity.Enabled = true;
            txtState.Enabled = true;
            txtPIN.Enabled = true;
            cmbCountry.Enabled = true;
            txtContactNumber.Enabled = true;
            txtMailID.Enabled = true;
            txtWebsite.Enabled = true;

            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.Enabled = true;
        }

        public void disableControls()
        {
            txtCompCode.Enabled = false;
            txtCompCode.ReadOnly = true;
            txtCompanyName.Enabled = false;
            txtAddress01.Enabled = false;
            txtAddress02.Enabled = false;
            txtArea.Enabled = false;  
            txtCity.Enabled = false;  
            txtState.Enabled = false;
            txtPIN.Enabled = false;
            cmbCountry.Enabled = false;
            txtContactNumber.Enabled = false;
            txtMailID.Enabled = false;
            txtWebsite.Enabled = false;
            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.Enabled = false;
        }

        public void onGenerateButtonClick()
        {
            lblActionMode.Text = "add";
            lblCompID.Text = "";
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
            lblCompID.Text = "";
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
            lblCompID.Text = "";
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
            lblCompID.Text = "";
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
            lblCompID.Text = "";
            btnSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void displaySelectedValuesOnUI(ClientInfo ClientInfoModel)
        {
            lblCompID.Text = ClientInfoModel.ClientID.ToString();
            txtCompCode.Text = ClientInfoModel.ClientCode.ToString();
            txtCompanyName.Text = ClientInfoModel.ClientName.ToString();
            txtAddress01.Text = ClientInfoModel.ClientAddress1.ToString();
            txtAddress02.Text = ClientInfoModel.ClientAddress2.ToString();
            txtArea.Text = ClientInfoModel.ClientArea.ToString();
            txtCity.Text = ClientInfoModel.ClientCity.ToString();
            txtState.Text = ClientInfoModel.ClientState.ToString();
            txtPIN.Text = ClientInfoModel.ClientPIN.ToString();
            cmbCountry.Text = ClientInfoModel.ClientCountry.ToString();
            txtContactNumber.Text = ClientInfoModel.ClientPhone.ToString();
            txtMailID.Text = ClientInfoModel.ClientContactMail.ToString(); ;
            txtWebsite.Text = ClientInfoModel.ClientWebSite.ToString();

            cmbIsActive.Text = ClientInfoModel.IsActive == true ? "Yes" : "No";
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
            lblActionMode.Text = "modify"; 
            onModifyButtonClick();
            clearControls();
            enableControls();
            cmbIsActive.SelectedIndex = 1;
            errValidator.Clear();

            cmbCountry.DataSource = objCountries.GetCountryList();
            cmbCountry.DisplayMember = "CountryTitle";
            cmbCountry.ValueMember = "CountryID";
        }

        private void btnRemoveDetails_Click(object sender, EventArgs e)
        {
            if(lblActionMode.Text == "" || lblActionMode.Text == "remove")
            {
                lblActionMode.Text = "remove";
                onRemoveButtonClick();
                clearControls();
                enableControls();
                cmbIsActive.SelectedIndex = 1;

                cmbCountry.DataSource = objCountries.GetCountryList();
                cmbCountry.DisplayMember = "CountryTitle";
                cmbCountry.ValueMember = "CountryID";
            }
            else if (lblActionMode.Text == "delete")
            {
                if (MessageBox.Show("The selected record will be deleted. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int affectedRows = objClientInfo.DeleteClientInfo(Convert.ToInt16(lblCompID.Text.Trim()));
                    if (affectedRows > 0)
                        MessageBox.Show("Details deleted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                onCancelButtonClick();
                disableControls();
                clearControls();
                cmbIsActive.SelectedIndex = 0;
                lblActionMode.Text = "";
                errValidator.Clear();
            }
        }

        private void frmCompanyInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (lblActionMode.Text != "")
            {
                if (MessageBox.Show("Changes will be discarded. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }
            objDashboard.lblDashboardTitle.Text = "Dashboard";
            this.Close();
        }
    }
}
