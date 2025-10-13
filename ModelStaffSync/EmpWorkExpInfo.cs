using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class EmpWorkExpInfo
    {
        public int? LastCompID { get; set; }
        public int? EmpID { get; set; }
        public int? LastCompanyInfoID { get; set; }

        [DisplayName("Company Name")]
        public string LastCompanyTitle { get; set; }

        [DisplayName("Company Address")]
        public string Address { get; set; }

        [DisplayName("Start Date")]
        public DateTime? StartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime? EndDate { get; set; }

        [DisplayName("More Details")]
        public string Comments { get; set; }
    }
}
