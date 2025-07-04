using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using static C1.Util.Win.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace StaffSync
{
    public static class clsCurrentUser
    {
        public static int EmpID { get; set; }
        public static int UserID { get; set; }
        public static string UserName { get; set; }
        public static string UserMailID { get; set; }
        public static string UserPassword { get; set; }
        public static bool IsActive { get; set; }
        public static bool IsDeleted { get; set; }
        public static bool IsLocked { get; set; }
        public static int RoleID { get; set; }
        public static string RoleTItle { get; set; }
        public static int ModuleID { get; set; }
        public static string ModuleTitle { get; set; }
        public static string DesignationTitle { get; set; }
        public static string DepartmentTitle { get; set; }

        public static DateTime LoginDateTime { get; set; }
        public static DateTime LogoutDateTime { get; set; }

        public static bool CanAdd { get; set; }
        public static bool CanUpdate { get; set; }
        public static bool CanDelete { get; set; }
        public static bool CanView { get; set; }
        public static bool CanExport { get; set; }

        public static clsAuditLog AppAuditLog = new clsAuditLog();
    }
}
