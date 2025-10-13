using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class FinYearMas
    {
        public int FinYearID { get; set; }
        public string FinYearFromTo { get; set; }
        public int FinYearFrom { get; set; }
        public int FinYearTo { get; set; }
        public bool IsDefault { get; set; }
        public int OrderID { get; set; }
    }
}
