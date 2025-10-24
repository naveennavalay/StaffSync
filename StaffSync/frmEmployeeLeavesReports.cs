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
    public partial class frmEmployeeLeavesReports : Form
    {
        UserRolesAndResponsibilitiesInfo objTempCurrentlyLoggedInUserInfo = new UserRolesAndResponsibilitiesInfo();

        public frmEmployeeLeavesReports()
        {
            InitializeComponent();
        }

        public frmEmployeeLeavesReports(UserRolesAndResponsibilitiesInfo objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo)
        {
            InitializeComponent();
            objTempCurrentlyLoggedInUserInfo = objCurrentlyLoggedInUserRolesAndResponsibilitiesInfo;
        }

        private void btnCloseMe_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
