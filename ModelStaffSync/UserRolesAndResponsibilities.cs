using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class UserRolesAndResponsibilities
    {
        public int EmpID { get; set; }
        public int ModuleID { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleTitle { get; set; }
        public int RoleID { get; set; }
        public string RoleTitle { get; set; }
        public string RoleDescription { get; set; }
        public int RoleDefID { get; set; }
        public bool CanAdd { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanSave { get; set; }
        public bool CanRemove { get; set; }
        public bool CanView { get; set; }
        public bool CanExport { get; set; }
    }
}
