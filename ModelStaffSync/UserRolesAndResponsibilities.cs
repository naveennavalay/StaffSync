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

    public class UserRolesAndResponsibilitiesInfo
    {
        public int UserID { get; set; }
        public int EmpID { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string EmpUserName { get; set; }
        public string EmpPassword { get; set; }
        public bool IsLocked { get; set; }
        public int RoleID { get; set; }
        public string RoleTitle { get; set; }
        public string RoleDescription { get; set; }
        public bool RoleIsActive { get; set; }
        public bool RoleIsDeleted { get; set; }
        public int RolesOrderiD { get; set; }
        public int ModuleID { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleTitle { get; set; }
        public string ModuleInitials { get; set; }
        public bool ModuleIsActive { get; set; }
        public bool ModuleIsDeleted { get; set; }
        public int ModuleOrderID { get; set; }
        public int RoleDefID { get; set; }
        public bool CanAdd { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanView { get; set; }
        public bool CanExport { get; set; }
        public bool CanApprove { get; set; }
        public bool CanReject { get; set; }
        public bool CanModifySettings { get; set; }
        public string Notes { get; set; }
    }


}
