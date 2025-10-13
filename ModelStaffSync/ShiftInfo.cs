using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class ShiftInfo
    {
        public int ShiftID { get; set; }
        public string ShiftCode { get; set; }
        public string ShiftTitle { get; set; }
        public string ShiftInitital { get; set; }
        public DateTime? ShiftStart { get; set; } = DateTime.Now;
        public DateTime? ShiftEnd { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
