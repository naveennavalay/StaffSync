using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using ModelStaffSync;
using Quartz.Impl.AdoJobStore.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Quartz.Logging.OperationName;

namespace StaffSync
{
    public partial class frmActiveCompanyInfo : Form
    {
        DALStaffSync.clsFinYearMas objFinYearMas = new DALStaffSync.clsFinYearMas();
        DALStaffSync.clsCurrentUserInfo objCurrentUserInfo = new DALStaffSync.clsCurrentUserInfo();
        DALStaffSync.clsClientInfo objClientInfo = new DALStaffSync.clsClientInfo();
        frmDashboard objDashboard = (frmDashboard)System.Windows.Forms.Application.OpenForms["frmDashboard"];
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();
        DateTime dob, doj;
        string dateFormat = "dd-MM-yyyy";
        CultureInfo provider = CultureInfo.InvariantCulture;
        ClientInfo selectedCompany = new ClientInfo();
        List<FinYearMas> selectedFinYearMas = new List<FinYearMas>();
        ClientFinYearInfo objClientFinYearInfo = new ClientFinYearInfo();
        ClientFinYearInfo objTempClientFinYearInfo = new ClientFinYearInfo();

        public frmActiveCompanyInfo()
        {
            InitializeComponent();
        }

        public frmActiveCompanyInfo(ClientFinYearInfo objClientFinYearInfo)
        {
            InitializeComponent();

            cmbFinYear.DataSource = objFinYearMas.GetCompleteFinYearList();
            cmbFinYear.DisplayMember = "FinYearFromTo";
            cmbFinYear.ValueMember = "FinYearID";

            if (objClientFinYearInfo.FinYearID == 0)
                cmbFinYear.SelectedIndex = 0;
            else
                cmbFinYear.SelectedIndex = objClientFinYearInfo.FinYearID - 1;

            cmbCompanyList.DataSource = objClientInfo.getAllCompanyList();
            cmbCompanyList.DisplayMember = "ClientName";
            cmbCompanyList.ValueMember = "ClientID";
            if(objClientFinYearInfo.ClientID == 0)
                cmbCompanyList.SelectedIndex = 0;
            else
                cmbCompanyList.SelectedIndex = objClientFinYearInfo.ClientID - 1;
        }

        public frmActiveCompanyInfo(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;

            cmbFinYear.DataSource = objFinYearMas.GetCompleteFinYearList();
            cmbFinYear.DisplayMember = "FinYearFromTo";
            cmbFinYear.ValueMember = "FinYearID";

            cmbCompanyList.DataSource = objClientInfo.getAllCompanyList();
            cmbCompanyList.DisplayMember = "ClientName";
            cmbCompanyList.ValueMember = "ClientID";
        }

        public frmActiveCompanyInfo(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo, ClientFinYearInfo objSelectedClientFinYearInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
            objTempClientFinYearInfo = objSelectedClientFinYearInfo;
            ModelStaffSync.CurrentUser.ClientID = objTempClientFinYearInfo.ClientID;

            cmbFinYear.DataSource = objFinYearMas.GetCompleteFinYearList();
            cmbFinYear.DisplayMember = "FinYearFromTo";
            cmbFinYear.ValueMember = "FinYearID";

            cmbCompanyList.DataSource = objClientInfo.getAllCompanyList();
            cmbCompanyList.DisplayMember = "ClientName";
            cmbCompanyList.ValueMember = "ClientID";
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmActiveCompanyInfo_Load(object sender, EventArgs e)
        {

        }

        private void btnCloseMe_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmActiveCompanyInfo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            selectedCompany = objClientInfo.getClientInfo(Convert.ToInt32(cmbCompanyList.SelectedIndex + 1)).FirstOrDefault();
            selectedFinYearMas = objFinYearMas.GetSpecificFinYearInfo(cmbFinYear.SelectedIndex + 1);

            objClientFinYearInfo.ClientID = selectedCompany.ClientID;
            objClientFinYearInfo.FinYearID = selectedFinYearMas.FirstOrDefault().FinYearID;
            this.Close();
        }

        public ClientFinYearInfo GetSelectedClientAndFinYearDetails()
        {
            return objClientFinYearInfo;
        }
    }
}
