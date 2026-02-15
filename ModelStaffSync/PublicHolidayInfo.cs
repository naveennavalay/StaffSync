using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.ComponentModel;

namespace ModelStaffSync
{
    public class PublicHolidayInfo
    {
        public int PubHolDetID { get; set; }
        public int PubHolMasID { get; set; }

        [DisplayName("Holiday Title")]
        public string PubHolidayTitle { get; set; }
        public DateTime? PubHolDate { get; set; }
        public int? OrderID { get; set; }
        public int PubHolTypeID { get; set; }
        
        [DisplayName("Holiday Type")] 
        public string PubHolTypeTitle { get; set; }
        public string DayName { get; set; }
    }

    public class PublicHolidayType
    {
        public int PubHolTypeID { get; set; }
        public string PubHolTypeCode { get; set; }

        [DisplayName("Holiday Type")] 
        public string PubHolTypeTitle { get; set; }
    }
}
