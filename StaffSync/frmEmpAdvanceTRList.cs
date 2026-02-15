using Krypton.Toolkit;
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
    public partial class frmEmpAdvanceTRList : Form
    {
        //DALStaffSync.clsCountries objCountries = new DALStaffSync.clsCountries();
        DALStaffSync.clsAdvanceTransaction objAdvanceTransaction = new DALStaffSync.clsAdvanceTransaction();

        //frmCountryMaster frmCountryMas = null;
        frmEmpAdvanceRepayment frmEmpAdvanceRepayment = null;
        frmEmpAdvanceRequest frmEmpAdvanceRequest = null;
        public frmEmpAdvanceTRList()
        {
            InitializeComponent();
        }

        public frmEmpAdvanceTRList(frmEmpAdvanceRepayment frmEmpAdvanceRepymnt, string strQueryFor, int filterID)
        {
            InitializeComponent();
            this.frmEmpAdvanceRepayment = frmEmpAdvanceRepymnt;
            lblSearchOptionClickedFor.Text = strQueryFor;
            lblFilterID.Text = filterID.ToString();

            if(lblSearchOptionClickedFor.Text == "empadvancerepayment" || lblSearchOptionClickedFor.Text == "empadvanceoutstanding")
            {
                this.Text = "Employee Advance Repayment List";
                lblSearchCaption.Text = "Search by Employee Name :";
            }
            else if (lblSearchOptionClickedFor.Text == "empadvancestatement")
            {
                this.Text = "Employee Advance Statement List";
                lblSearchCaption.Text = "Search by Comments :";
            }
        }

        public frmEmpAdvanceTRList(frmEmpAdvanceRequest frmEmpAdvanceRequst, string strQueryFor, int filterID)
        {
            InitializeComponent();
            this.frmEmpAdvanceRequest = frmEmpAdvanceRequst;
            lblSearchOptionClickedFor.Text = strQueryFor;
            lblFilterID.Text = filterID.ToString();

            if (lblSearchOptionClickedFor.Text == "empadvancerepayment" || lblSearchOptionClickedFor.Text == "empadvanceoutstanding")
            {
                this.Text = "Employee Advance Repayment List";
                lblSearchCaption.Text = "Search by Employee Name :";
            }
            else if (lblSearchOptionClickedFor.Text == "empadvancestatement")
            {
                this.Text = "Employee Advance Statement List";
                lblSearchCaption.Text = "Search by Comments :";
            }
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEmpAdvanceTRList_Load(object sender, EventArgs e)
        {
            LoadTheData();
        }

        private void btnCloseMe_Click_1(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (lblSearchOptionClickedFor.Text == "empadvancerepayment")
                {
                    foreach (DataGridViewRow row in dtgAdvanceList.Rows)
                    {
                        row.Visible = row.Cells["EmpName"].Value.ToString().ToLower().Contains(txtSearch.Text.ToLower());
                    }
                }
                else if (lblSearchOptionClickedFor.Text == "empadvancestatement")
                {
                    foreach (DataGridViewRow row in dtgAdvanceList.Rows)
                    {
                        row.Visible = row.Cells["Comments"].Value.ToString().ToLower().Contains(txtSearch.Text.ToLower());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtgEmployeeList_DoubleClick(object sender, EventArgs e)
        {
            if (lblSearchOptionClickedFor.Text == "empadvancerepayment")
            {
                this.frmEmpAdvanceRepayment.displaySelectedValuesOnUI(Convert.ToInt16(dtgAdvanceList.SelectedRows[0].Cells["EmpID"].Value.ToString()));
                this.Close();
            }
        }

        private void frmEmpAdvanceTRList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void LoadTheData()
        {
            try
            {
                if (lblSearchOptionClickedFor.Text == "empadvancerepayment" || lblSearchOptionClickedFor.Text == "empadvanceoutstanding")
                {
                    dtgAdvanceList.DataSource = objAdvanceTransaction.EmployeeSpecificAdvanceInformation(Convert.ToInt32(lblFilterID.Text.ToString()));
                    dtgAdvanceList.Columns["Select"].Visible = false;

                    dtgAdvanceList.Columns["EmpID"].Visible = false;
                    dtgAdvanceList.Columns["EmpID"].ReadOnly = true;
                    dtgAdvanceList.Columns["EmpID"].Width = 100;

                    dtgAdvanceList.Columns["EmpCode"].Visible = true;
                    dtgAdvanceList.Columns["EmpCode"].ReadOnly = true;
                    dtgAdvanceList.Columns["EmpCode"].Width = 100;

                    dtgAdvanceList.Columns["EmpName"].Visible = true;
                    dtgAdvanceList.Columns["EmpName"].ReadOnly = true;
                    dtgAdvanceList.Columns["EmpName"].Width = 175;

                    dtgAdvanceList.Columns["DesignationTitle"].Visible = true;
                    dtgAdvanceList.Columns["DesignationTitle"].ReadOnly = true;
                    dtgAdvanceList.Columns["DesignationTitle"].Width = 175;

                    dtgAdvanceList.Columns["DepartmentTitle"].Visible = true;
                    dtgAdvanceList.Columns["DepartmentTitle"].ReadOnly = true;
                    dtgAdvanceList.Columns["DepartmentTitle"].Width = 175;

                    dtgAdvanceList.Columns["PersonalInfoID"].Visible = false;
                    dtgAdvanceList.Columns["PersonalInfoID"].ReadOnly = true;
                    dtgAdvanceList.Columns["PersonalInfoID"].Width = 100;

                    dtgAdvanceList.Columns["ContactNumber2"].Visible = true;
                    dtgAdvanceList.Columns["ContactNumber2"].ReadOnly = true;
                    dtgAdvanceList.Columns["ContactNumber2"].Width = 200;

                    dtgAdvanceList.Columns["EmpAdvanceRequestID"].Visible = false;
                    dtgAdvanceList.Columns["EmpAdvanceRequestID"].ReadOnly = true;
                    dtgAdvanceList.Columns["EmpAdvanceRequestID"].Width = 175;

                    dtgAdvanceList.Columns["EmpAdvReqCode"].Visible = true;
                    dtgAdvanceList.Columns["EmpAdvReqCode"].ReadOnly = true;
                    dtgAdvanceList.Columns["EmpAdvReqCode"].Width = 175;

                    dtgAdvanceList.Columns["AdvanceTypeID"].Visible = false;
                    dtgAdvanceList.Columns["AdvanceTypeID"].ReadOnly = true;
                    dtgAdvanceList.Columns["AdvanceTypeID"].Width = 175;

                    dtgAdvanceList.Columns["AdvanceTypeTitle"].Visible = true;
                    dtgAdvanceList.Columns["AdvanceTypeTitle"].ReadOnly = true;
                    dtgAdvanceList.Columns["AdvanceTypeTitle"].Width = 175;

                    dtgAdvanceList.Columns["AdvanceAmount"].Visible = true;
                    dtgAdvanceList.Columns["AdvanceAmount"].ReadOnly = true;
                    dtgAdvanceList.Columns["AdvanceAmount"].Width = 125;
                    dtgAdvanceList.Columns["AdvanceAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
                    dtgAdvanceList.Columns["AdvanceAmount"].DefaultCellStyle.Format = "c2";

                    dtgAdvanceList.Columns["AdvanceInstallment"].Visible = true;
                    dtgAdvanceList.Columns["AdvanceInstallment"].ReadOnly = true;
                    dtgAdvanceList.Columns["AdvanceInstallment"].Width = 125;
                    dtgAdvanceList.Columns["AdvanceInstallment"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
                    dtgAdvanceList.Columns["AdvanceInstallment"].DefaultCellStyle.Format = "c2";


                    dtgAdvanceList.Columns["AdvanceStartDate"].Visible = true;
                    dtgAdvanceList.Columns["AdvanceStartDate"].ReadOnly = true;
                    dtgAdvanceList.Columns["AdvanceStartDate"].Width = 125;
                    dtgAdvanceList.Columns["AdvanceStartDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";

                    dtgAdvanceList.Columns["AdvanceEndDate"].Visible = true;
                    dtgAdvanceList.Columns["AdvanceEndDate"].ReadOnly = true;
                    dtgAdvanceList.Columns["AdvanceEndDate"].Width = 125;
                    dtgAdvanceList.Columns["AdvanceEndDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";

                    dtgAdvanceList.Columns["AdvanceRequestStatus"].Visible = true;
                    dtgAdvanceList.Columns["AdvanceRequestStatus"].ReadOnly = true;
                    dtgAdvanceList.Columns["AdvanceRequestStatus"].Width = 150;

                    dtgAdvanceList.Columns["LastRepayDate"].Visible = true;
                    dtgAdvanceList.Columns["LastRepayDate"].ReadOnly = true;
                    dtgAdvanceList.Columns["LastRepayDate"].Width = 125;
                    dtgAdvanceList.Columns["LastRepayDate"].DefaultCellStyle.Format = "dd-MMM-yyyy";

                    dtgAdvanceList.Columns["CBalance"].Visible = true;
                    dtgAdvanceList.Columns["CBalance"].ReadOnly = true;
                    dtgAdvanceList.Columns["CBalance"].Width = 125;
                    dtgAdvanceList.Columns["CBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
                    dtgAdvanceList.Columns["CBalance"].DefaultCellStyle.Format = "c2";

                    dtgAdvanceList.Columns["RePaymentBalance"].Visible = false;
                    dtgAdvanceList.Columns["RePaymentBalance"].ReadOnly = true;
                    dtgAdvanceList.Columns["RePaymentBalance"].Width = 125;
                    dtgAdvanceList.Columns["RePaymentBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
                    dtgAdvanceList.Columns["RePaymentBalance"].DefaultCellStyle.Format = "c2";

                    dtgAdvanceList.Columns["EmpAdvanceRecoveryID"].Visible = false;
                    dtgAdvanceList.Columns["EmpAdvanceRecoveryID"].ReadOnly = true;
                    dtgAdvanceList.Columns["EmpAdvanceRecoveryID"].Width = 175;

                    dtgAdvanceList.Columns["AdvanceRequestStatus"].Visible = false;
                    dtgAdvanceList.Columns["AdvanceRequestStatus"].ReadOnly = true;
                    dtgAdvanceList.Columns["AdvanceRequestStatus"].Width = 175;
                }
                else if (lblSearchOptionClickedFor.Text == "empadvancestatement")
                {
                    dtgAdvanceList.DataSource = objAdvanceTransaction.EmployeeSpecificAdvanceStatemetns(Convert.ToInt32(lblFilterID.Text.ToString()));
                    dtgAdvanceList.Columns["EmpAdvanceRecoveryID"].Visible = false;
                    dtgAdvanceList.Columns["EmpAdvanceRecoveryID"].ReadOnly = true;
                    dtgAdvanceList.Columns["EmpAdvanceRecoveryID"].Width = 100;
                    dtgAdvanceList.Columns["AdvanceDate"].Visible = true;
                    dtgAdvanceList.Columns["AdvanceDate"].ReadOnly = true;
                    dtgAdvanceList.Columns["AdvanceDate"].Width = 100;
                    dtgAdvanceList.Columns["OBalance"].Visible = true;
                    dtgAdvanceList.Columns["OBalance"].ReadOnly = true;
                    dtgAdvanceList.Columns["OBalance"].Width = 125;
                    dtgAdvanceList.Columns["OBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
                    dtgAdvanceList.Columns["OBalance"].DefaultCellStyle.Format = "c2";
                    dtgAdvanceList.Columns["CrBalance"].Visible = true;
                    dtgAdvanceList.Columns["CrBalance"].ReadOnly = true;
                    dtgAdvanceList.Columns["CrBalance"].Width = 125;
                    dtgAdvanceList.Columns["CrBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
                    dtgAdvanceList.Columns["CrBalance"].DefaultCellStyle.Format = "c2";
                    dtgAdvanceList.Columns["DrBalance"].Visible = true;
                    dtgAdvanceList.Columns["DrBalance"].ReadOnly = true;
                    dtgAdvanceList.Columns["DrBalance"].Width = 125;
                    dtgAdvanceList.Columns["DrBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
                    dtgAdvanceList.Columns["DrBalance"].DefaultCellStyle.Format = "c2";
                    dtgAdvanceList.Columns["CBalance"].Visible = true;
                    dtgAdvanceList.Columns["CBalance"].ReadOnly = true;
                    dtgAdvanceList.Columns["CBalance"].Width = 125;
                    dtgAdvanceList.Columns["CBalance"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; //Allowences
                    dtgAdvanceList.Columns["CBalance"].DefaultCellStyle.Format = "c2";
                    dtgAdvanceList.Columns["TRType"].Visible = true;
                    dtgAdvanceList.Columns["TRType"].ReadOnly = true;
                    dtgAdvanceList.Columns["TRType"].Width = 100;
                    dtgAdvanceList.Columns["Comments"].Visible = true;
                    dtgAdvanceList.Columns["Comments"].ReadOnly = true;
                    dtgAdvanceList.Columns["Comments"].Width = 300;
                    dtgAdvanceList.Columns["OrderID"].Visible = false;
                    dtgAdvanceList.Columns["OrderID"].ReadOnly = true;
                    dtgAdvanceList.Columns["OrderID"].Width = 175;
                    dtgAdvanceList.Columns["EmpAdvanceRequestID"].Visible = false;
                    dtgAdvanceList.Columns["EmpAdvanceRequestID"].ReadOnly = true;
                    dtgAdvanceList.Columns["EmpAdvanceRequestID"].Width = 175;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
