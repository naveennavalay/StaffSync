using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class RolesProfile
    {
        public int RoleID { get; set; }
        public string RoleTitle { get; set; }
        public int RoleDefID { get; set; }
        public bool CanAdd { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public bool CanView { get; set; }
        public bool CanExport { get; set; }
    }
}
