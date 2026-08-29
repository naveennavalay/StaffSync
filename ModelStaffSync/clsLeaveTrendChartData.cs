using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class LeaveTrendChartData
    {
        public string MonthName { get; set; }

        public int Applied { get; set; }

        public int Approved { get; set; }

        public int Rejected { get; set; }

        public int Cancelled { get; set; }
    }
}
