using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class AppReports
    {
        public int ReportsID { get; set; }

        public string ReportsCode { get; set; }

        public string ReportsName { get; set; }

        public string ReportsDescription { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted {  get; set; }

        public int ClientID { get; set; }

        public int OrderID { get; set; }
    }
}
