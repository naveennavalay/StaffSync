using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class UserRoles
    {
        public int UserRoleID { get; set; }
        public int RoleID { get; set; }
        public int? UserID { get; set; }
    }
}
