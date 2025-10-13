using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class EmpTypeInfo
    {
        public int EmpTypeInfoID { get; set; }
        public int EmpID { get; set; }
        public int EmpTypeMasID { get; set; }
        public DateTime EffectiveDate { get; set; }

    }
}
