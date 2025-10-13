using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class EmploymentTypeInfo
    {
        public int EmpTypeMasID { get; set; }
        public string EmpTypeCode { get; set; }
        public string EmpTypeTitle { get; set; }
        public string EmpTypeInitial { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
