using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class UserInfo
    {
        public int UserID { get; set; }
        public int EmpID { get; set; }
        public string EmpUserName { get; set; }
        public string EmpPassword { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsLocked { get; set; }
    }
}
