using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class BloodGroupsModel
    {
        public int BloodGroupID { get; set; }
        public string BloodGroupCode { get; set; }

        public string BloodGroupTitle { get; set; }
        public string BloodGroupInitial { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
