using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class AppModuleInfo
    {
        public int ModuleID { get; set; }
        //public string ModuleCode { get; set; }
        public string ModuleTitle { get; set; }
        //public string ModuleInitial { get; set; }
        //public bool IsActive { get; set; }
        //public bool IsDeleted { get; set; }
        public bool Access { get; set; }
        public int? UserID { get; set; }
    }
}
