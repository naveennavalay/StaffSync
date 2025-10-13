using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class EmpShiftInfo
    {
        public int EmpShiftInfoID { get; set; }
        public int EmpID { get; set; }
        public int ShiftID { get; set; }
        public DateTime EffectiveDate { get; set; }

    }
}
