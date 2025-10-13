using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class RolesAndResponsibilitiesInfo
    {
        public int RoleID { get; set; }
        public string RoleTitle { get; set; }
        public string RoleDescription { get; set; }
        public bool Access { get; set; }
        public int? UserID { get; set; }
        //public bool IsActive { get; set; }
        //public bool IsDeleted { get; set; }
    }
}
