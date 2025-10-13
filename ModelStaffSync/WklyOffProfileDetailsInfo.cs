using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class WklyOffProfileDetailsInfo
    {
        [DisplayName("Select")]
        public bool Select { get; set; }

        [DisplayName("WeeklyOffDetailsID")]
        public int WklyOffDetID { get; set; }

        [DisplayName("WeeklyOffMasterID")]
        public int WklyOffMasID { get; set; }

        [DisplayName("WeeklyOffDayID")]
        public int WklyOffDay { get; set; }

        [DisplayName("WeeklyOffDayName")]
        public int WklyOffDayName { get; set; }

        [DisplayName("WeeklyOffDayOrderID")]
        public int WklyOffOrderID { get; set; }
    }
}
