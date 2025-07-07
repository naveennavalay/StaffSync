using Microsoft.Office.Interop.Excel;
using Org.BouncyCastle.Ocsp;
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
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StaffSync
{
    public partial class frmLastCompanySelection : Form
    {
        clsEmpWorkExperienceInfo objEmpWorkExperienceInfo = new clsEmpWorkExperienceInfo();
        public EmpWorkExpInfo selectedPublicWorkExpInfo = new EmpWorkExpInfo();

        public frmLastCompanySelection()
        {
            InitializeComponent();
        }

        public frmLastCompanySelection(int EmpID)
        {
            InitializeComponent();
            lblEmpID.Text = EmpID.ToString();
        }

        public frmLastCompanySelection(EmpWorkExpInfo objSelectedWorkExpInfo)
        {
            InitializeComponent();
            selectedPublicWorkExpInfo = objSelectedWorkExpInfo;

            dtgPreviousWorkExp.DataSource = objEmpWorkExperienceInfo.GetWorkExpDefaultList();
            dtgPreviousWorkExp.Columns["LastCompID"].Visible = true;
            dtgPreviousWorkExp.Columns["LastCompanyInfoID"].Visible = true;
            dtgPreviousWorkExp.Columns["EmpID"].Visible = true;
            dtgPreviousWorkExp.Columns["LastCompanyTitle"].Width = 200;
            dtgPreviousWorkExp.Columns["Address"].Width = 350;
            dtgPreviousWorkExp.Columns["StartDate"].Width = 100;
            dtgPreviousWorkExp.Columns["StartDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgPreviousWorkExp.Columns["EndDate"].Width = 100;
            dtgPreviousWorkExp.Columns["EndDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgPreviousWorkExp.Columns["Comments"].Width = 350;

            lblEmpID.Text = selectedPublicWorkExpInfo.EmpID.ToString();
            txtStartDate.Text = Convert.ToDateTime(objSelectedWorkExpInfo.StartDate.ToString()).ToString("dd-MM-yyyy");
            txtEndDate.Text = Convert.ToDateTime(objSelectedWorkExpInfo.EndDate.ToString()).ToString("dd-MM-yyyy");
            txtMoreDetails.Text = objSelectedWorkExpInfo.Comments;
            dtgPreviousWorkExp.Rows[Convert.ToInt16(objSelectedWorkExpInfo.LastCompanyInfoID)-1].Selected = true;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLastCompanySelection_Load(object sender, EventArgs e)
        {
            //// TODO: This line of code loads data into the 'staffsyncDBDTSet.EmpMasInfo' table. You can move, or remove it, as needed.
            //this.empMasInfoTableAdapter.Fill(this.staffsyncDBDTSet.EmpMasInfo);
            onCancelButtonClick();
            disableControls();
            clearControls();

            dtgPreviousWorkExp.DataSource = objEmpWorkExperienceInfo.GetWorkExpDefaultList();
            dtgPreviousWorkExp.Columns["LastCompID"].Visible = false;
            dtgPreviousWorkExp.Columns["LastCompanyInfoID"].Visible = false;
            dtgPreviousWorkExp.Columns["EmpID"].Visible = false;
            dtgPreviousWorkExp.Columns["LastCompanyTitle"].Width = 200;
            dtgPreviousWorkExp.Columns["Address"].Width = 350;
            dtgPreviousWorkExp.Columns["StartDate"].Width = 100;
            dtgPreviousWorkExp.Columns["StartDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgPreviousWorkExp.Columns["EndDate"].Width = 100;
            dtgPreviousWorkExp.Columns["EndDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";
            dtgPreviousWorkExp.Columns["Comments"].Width = 350;
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
        }

        private void btnCloseMe_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        public void clearControls()
        {

        }

        public void enableControls()
        {

        }

        public void disableControls()
        {

        }

        public void onGenerateButtonClick()
        {

        }

        public void onModifyButtonClick()
        {

        }

        public void onRemoveButtonClick()
        {

        }

        public void onSaveButtonClick()
        {

        }

        public void onCancelButtonClick()
        {

        }

        public void displaySelectedValuesOnUI(CountriesModel countryModel)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnModifyDetails_Click(object sender, EventArgs e)
        {

        }

        private void btnRemoveDetails_Click(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(!ValidateData())
            {
                return;
            }

            if (selectedPublicWorkExpInfo.LastCompID != 0)
            {
                selectedPublicWorkExpInfo.LastCompID = selectedPublicWorkExpInfo.LastCompID == null ? 0 : selectedPublicWorkExpInfo.LastCompID;
                selectedPublicWorkExpInfo.EmpID = Convert.ToInt16(lblEmpID.Text.ToString());
                selectedPublicWorkExpInfo.LastCompanyInfoID = Convert.ToInt16(dtgPreviousWorkExp.SelectedRows[0].Cells["LastCompanyInfoID"].Value.ToString());
                selectedPublicWorkExpInfo.LastCompanyTitle = dtgPreviousWorkExp.SelectedRows[0].Cells["LastCompanyTitle"].Value.ToString();
                selectedPublicWorkExpInfo.Address = dtgPreviousWorkExp.SelectedRows[0].Cells["Address"].Value.ToString();
                selectedPublicWorkExpInfo.StartDate = Convert.ToDateTime(txtStartDate.Text.ToString());
                selectedPublicWorkExpInfo.EndDate = Convert.ToDateTime(txtEndDate.Text.ToString());
                selectedPublicWorkExpInfo.Comments = txtMoreDetails.Text;
            }
            else
            {
                selectedPublicWorkExpInfo.LastCompID = 0;
                selectedPublicWorkExpInfo.EmpID = Convert.ToInt16(lblEmpID.Text.ToString());
                selectedPublicWorkExpInfo.LastCompanyInfoID = Convert.ToInt16(dtgPreviousWorkExp.SelectedRows[0].Cells["LastCompanyInfoID"].Value.ToString());
                selectedPublicWorkExpInfo.LastCompanyTitle = dtgPreviousWorkExp.SelectedRows[0].Cells["LastCompanyTitle"].Value.ToString();
                selectedPublicWorkExpInfo.Address = dtgPreviousWorkExp.SelectedRows[0].Cells["Address"].Value.ToString();
                selectedPublicWorkExpInfo.StartDate = Convert.ToDateTime(txtStartDate.Text.ToString());
                selectedPublicWorkExpInfo.EndDate = Convert.ToDateTime(txtEndDate.Text.ToString());
                selectedPublicWorkExpInfo.Comments = txtMoreDetails.Text;
            }
            this.Close();
        }

        private bool ValidateData()
        {
            DateTime startDate, endDate;
            string dateFormat = "dd-MM-yyyy";
            var provider = System.Globalization.CultureInfo.InvariantCulture;

            // Check blank dates
            if (string.IsNullOrWhiteSpace(txtStartDate.Text))
            {
                MessageBox.Show("Start Date cannot be blank.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStartDate.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEndDate.Text))
            {
                MessageBox.Show("End Date cannot be blank.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEndDate.Focus();
                return false;
            }
            // Parse dates
            if (!DateTime.TryParseExact(txtStartDate.Text, dateFormat, provider, System.Globalization.DateTimeStyles.None, out startDate))
            {
                MessageBox.Show("Invalid Start Date format. Use dd-MM-yyyy.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStartDate.Focus();
                return false;
            }
            if (!DateTime.TryParseExact(txtEndDate.Text, dateFormat, provider, System.Globalization.DateTimeStyles.None, out endDate))
            {
                MessageBox.Show("Invalid End Date format. Use dd-MM-yyyy.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEndDate.Focus();
                return false;
            }
            // Dates should not be in the future
            if (startDate > DateTime.Now.Date)
            {
                MessageBox.Show("Start Date cannot be greater than today.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStartDate.Focus();
                return false;
            }
            if (endDate > DateTime.Now.Date)
            {
                MessageBox.Show("End Date cannot be greater than today.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEndDate.Focus();
                return false;
            }
            // End Date should not be less than Start Date
            if (endDate < startDate)
            {
                MessageBox.Show("End Date cannot be less than Start Date.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEndDate.Focus();
                return false;
            }
            // More Details should not be blank
            if (string.IsNullOrWhiteSpace(txtMoreDetails.Text))
            {
                MessageBox.Show("More Details cannot be blank.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMoreDetails.Focus();
                return false;
            }
            return true;
        }

        private void txtStartDate_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtStartDate.Text.Trim()))
            {
                txtEndDate.Text = string.Empty;
                return;
            }
            txtEndDate.Text = txtStartDate.Text;
        }
    }
}
