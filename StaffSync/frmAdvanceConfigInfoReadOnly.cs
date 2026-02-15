using Krypton.Toolkit;
using ModelStaffSync;
using Org.BouncyCastle.Asn1.Cmp;
using Org.BouncyCastle.Asn1.Ocsp;
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
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace StaffSync
{
    public partial class frmAdvanceConfigInfoReadOnly : Form
    {
        DALStaffSync.clsAdvanceTypeMas objAdvanceTypesModel = new DALStaffSync.clsAdvanceTypeMas();
        DALStaffSync.clsAdvanceTypeConfigInfo objAdvanceTypeConfigInfo = new DALStaffSync.clsAdvanceTypeConfigInfo();

        public frmAdvanceConfigInfoReadOnly()
        {
            InitializeComponent();
        }

        public frmAdvanceConfigInfoReadOnly(int txtAdvanceTypeID)
        {
            InitializeComponent();

            txtMaxTenure.Text = "0";
            cmbIsActive.Items.Clear();
            cmbIsActive.Items.Add("");
            cmbIsActive.Items.Add("Yes");
            cmbIsActive.Items.Add("No");
            cmbIsActive.SelectedIndex = 0;

            cmbAdvanceBasedOn.Items.Clear();
            cmbAdvanceBasedOn.Items.Add("");
            cmbAdvanceBasedOn.Items.Add("Net Salary");
            cmbAdvanceBasedOn.Items.Add("Gross Salary");
            cmbAdvanceBasedOn.SelectedIndex = 0;

            cmbAdvanceAmountBased.Items.Clear();
            cmbAdvanceAmountBased.Items.Add("");
            cmbAdvanceAmountBased.Items.Add("Percentage");
            cmbAdvanceAmountBased.Items.Add("Fixed");
            cmbAdvanceAmountBased.SelectedIndex = 0;

            AdvanceTypesModel tmpAdvanceTypesModel = objAdvanceTypesModel.GetAdvanceTypeConfigByID(txtAdvanceTypeID);

            lblAdvanceTypeID.Text = tmpAdvanceTypesModel.AdvanceTypeID.ToString();
            txtAdvanceCode.Text = tmpAdvanceTypesModel.AdvanceTypeCode.ToString();
            txtAdvanceTitle.Text = tmpAdvanceTypesModel.AdvanceTypeTitle.ToString();
            cmbIsActive.Text = tmpAdvanceTypesModel.IsActive ? "Yes" : "No";

            AdvanceTypeConfigModel objAdvanceTypeConfigModel = objAdvanceTypeConfigInfo.GetAdvanceTypeConfigByID(tmpAdvanceTypesModel.AdvanceTypeID);
            lblAdvanceTypeConfigID.Text = objAdvanceTypeConfigModel.AdvanceTypeConfigID.ToString();
            cmbAdvanceBasedOn.Text = objAdvanceTypeConfigModel.BasedOnNetOrGross.ToString();
            cmbAdvanceAmountBased.Text = objAdvanceTypeConfigModel.MaxPerOfNetOrGross.ToString();
            txtAdvancePercentage.Text = objAdvanceTypeConfigModel.MaxPercentage.ToString();
            txtAdvanceAmountFixed.Text = objAdvanceTypeConfigModel.MaxFixed.ToString();
            chkRecoveryRequired.Checked = objAdvanceTypeConfigModel.RecoveryRequired;
            chkAutoDeductFromSalary.Checked = objAdvanceTypeConfigModel.AutoDeductFromSalary;
            chkAutoDeductFromSalary.Enabled = chkRecoveryRequired.Checked == true || chkAutoDeductFromSalary.Checked == true ? true : false;
            chkIncludeAsDeductionInSalary.Checked = objAdvanceTypeConfigModel.IncludeInSalary;
            chkIncludeAsDeductionInSalary.Enabled = chkRecoveryRequired.Checked == true || chkIncludeAsDeductionInSalary.Checked == true ? true : false;
            chkAutoDeductFromNextSaslary.Checked = objAdvanceTypeConfigModel.AutoRecoveryFromNextSalary;
            chkAutoDeductFromNextSaslary.Enabled = chkRecoveryRequired.Checked == true || chkAutoDeductFromNextSaslary.Checked == true ? true : false;
            chkInterestRequired.Checked = objAdvanceTypeConfigModel.InterestRequired;
            chkApprovalNeeded.Checked = objAdvanceTypeConfigModel.ApprovalRequired;
            chkAllowPause.Checked = objAdvanceTypeConfigModel.AllowPause;
            chkWaiverAllowed.Checked = objAdvanceTypeConfigModel.WaiverAllowed;
            txtMaxTenure.Text = objAdvanceTypeConfigModel.MaxTenure.ToString();
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAdvanceConfigInfoReadOnly_Load(object sender, EventArgs e)
        {

        }

        private void btnCloseMe_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAdvanceConfigInfoReadOnly_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }
    }
}
