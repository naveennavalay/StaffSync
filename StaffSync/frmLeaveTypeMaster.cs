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

namespace StaffSync
{
    public partial class frmLeaveTypeMaster : Form
    {
        //clsCountries objCountries = new clsCountries();
        //clsDesignation objDesignation = new clsDesignation();
        //clsStates objState = new clsStates();
        //clsRelationship objRelationship = new clsRelationship();
        clsLeaveTypeMas objLeaveTypeMas = new clsLeaveTypeMas();

        public frmLeaveTypeMaster()
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
            this.Close();
        }

        private void frmLeaveTypeMaster_Load(object sender, EventArgs e)
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
            frmLeaveTypeList frmLeaveTypeList = new frmLeaveTypeList(this, "listLeaveTypeMaster");
            frmLeaveTypeList.ShowDialog();
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
            lblLeaveTypeID.Text = objLeaveTypeMas.getMaxRowCount("LeaveTypeMas", "LeaveTypeID").ToString();
            txtLeaveCode.Text = "LEV-" + (lblLeaveTypeID.Text.Trim()).ToString().PadLeft(4, '0');
            errValidator.Clear();
        }

        private void btnSaveDetails_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLeaveTitle.Text))
            {
                txtLeaveTitle.Focus();
                errValidator.SetError(this.txtLeaveTitle, "Please enter Leave Title");
            }
            else if (string.IsNullOrEmpty(txtLeaveTitle.Text))
            {
                txtLeaveTitle.Focus();
                errValidator.SetError(this.txtLeaveTitle, "Please enter Leave Initial");
            }
            else if (string.IsNullOrEmpty(cmbLeavePaid.Text))
            {
                cmbLeavePaid.Focus();
                cmbLeavePaid.SelectedIndex = 1;
                errValidator.SetError(this.cmbLeavePaid, "Please select Leave Paid Status");
            }
            else if (string.IsNullOrEmpty(cmbIsActive.Text))
            {
                cmbIsActive.Focus();
                cmbIsActive.SelectedIndex = 1;
                errValidator.SetError(this.cmbIsActive, "Please select Leave Status");
            }
            else
            {
                if (lblActionMode.Text == "add")
                {
                    int newID = objLeaveTypeMas.InsertLeaveTypeInfo(txtLeaveCode.Text.Trim(), txtLeaveTitle.Text.Trim(), cmbLeavePaid.Text.Trim() == "Yes" ? true : false, cmbIsActive.Text.Trim() == "Yes" ? true : false, false);
                    if (newID > 0)
                        MessageBox.Show("Details inserted successfully", "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (lblActionMode.Text == "modify")
                {
                    int affectedRows = objLeaveTypeMas.UpadateLeaveTypeInfo(Convert.ToInt16(lblLeaveTypeID.Text.Trim()), txtLeaveCode.Text.Trim(), txtLeaveTitle.Text.Trim(), cmbLeavePaid.Text.Trim() == "Yes" ? true : false, cmbIsActive.Text.Trim() == "Yes" ? true : false);
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
            txtLeaveCode.Text = "";
            txtLeaveCode.ReadOnly = true;
            txtLeaveTitle.Text = "";
            cmbLeavePaid.Items.Clear();
            cmbLeavePaid.Items.Add("");
            cmbLeavePaid.Items.Add("Yes");
            cmbLeavePaid.Items.Add("No");
            cmbLeavePaid.SelectedIndex = 1;

            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.SelectedIndex = 1;
        }

        public void enableControls()
        {
            txtLeaveCode.Enabled = true;
            txtLeaveCode.ReadOnly = true;
            txtLeaveTitle.Enabled = true;
            cmbLeavePaid.Enabled = true;
            cmbLeavePaid.Items.Clear();
            cmbLeavePaid.Items.Add("");
            cmbLeavePaid.Items.Add("Yes");
            cmbLeavePaid.Items.Add("No");
            cmbLeavePaid.SelectedIndex = 1;
            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.SelectedIndex = 1; 
            cmbIsActive.Enabled = true;
        }

        public void disableControls()
        {
            txtLeaveCode.Enabled = false;
            txtLeaveCode.ReadOnly = true;
            txtLeaveTitle.Enabled = false;
            cmbLeavePaid.Enabled = false;
            cmbLeavePaid.Items.Clear();
            cmbLeavePaid.Items.Add("");
            cmbLeavePaid.Items.Add("Yes");
            cmbLeavePaid.Items.Add("No");
            cmbLeavePaid.SelectedIndex = 1;
            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.SelectedIndex = 1;
            cmbIsActive.Enabled = false;
        }

        public void onGenerateButtonClick()
        {
            lblActionMode.Text = "add";
            lblLeaveTypeID.Text = "";
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
            lblLeaveTypeID.Text = "";
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
            lblLeaveTypeID.Text = "";
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
            lblLeaveTypeID.Text = "";
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
            lblLeaveTypeID.Text = "";
            btnSearch.Enabled = false;
            btnGenerateDetails.Enabled = true;
            btnModifyDetails.Enabled = true;
            btnSaveDetails.Enabled = false;
            btnRemoveDetails.Enabled = true;
            btnCancel.Enabled = true;
        }

        public void displaySelectedValuesOnUI(LeaveTypeInfoModel LeaveTypeInfoModel)
        {
            lblLeaveTypeID.Text = LeaveTypeInfoModel.LeaveTypeID.ToString();
            txtLeaveCode.Text = LeaveTypeInfoModel.LeaveCode;
            txtLeaveTitle.Text = LeaveTypeInfoModel.LeaveTypeTitle;
            cmbLeavePaid.Text = LeaveTypeInfoModel.IsPaid == true ? "Yes" : "No";
            cmbIsActive.Text = LeaveTypeInfoModel.IsActive == true ? "Yes" : "No";
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
            }
            else if (lblActionMode.Text == "delete")
            {
                if (MessageBox.Show("The selected record will be deleted. \nAre you sure to continue", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int affectedRows = objLeaveTypeMas.DeleteListTypeInfo(Convert.ToInt16(lblLeaveTypeID.Text.Trim()));
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
    }
}
