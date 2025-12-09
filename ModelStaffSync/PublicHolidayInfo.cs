using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class PublicHolidayInfo
    {
        public int PubHolDetID { get; set; }
        public int PubHolMasID { get; set; }
        public string PubHolidayTitle { get; set; }
        public DateTime? PubHolDate { get; set; }
        public int? OrderID { get; set; }
        public string DayName { get; set; }
    }
}
