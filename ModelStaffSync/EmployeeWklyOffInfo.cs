using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class EmployeeWklyOffInfo
    {
        public int WeeklyOffID { get; set; }
        public int EmpID { get; set; }
        public int WklyOffMasID { get; set; }
        public DateTime EffectDateFrom { get; set; }

    }
}
