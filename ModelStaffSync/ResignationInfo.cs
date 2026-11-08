using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class ResignationInfo
    {
        public int ResigMasID { get; set; }

        [DisplayName("Resignation Code")]
        public string ResigMasCode { get; set; }

        [DisplayName("Resignation Reason")]
        public string ResigMasTitle { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class IsResignationAppliedInfo
    {
        public int EmpResignID { get; set; }
        public int EmpResignStatus { get; set; }
    }
}
