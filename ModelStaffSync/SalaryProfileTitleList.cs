using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class SalaryProfileTitleList
    {
        public int SalProfileID { get; set; }

        [DisplayName("Salary Profile Code")]
        public string SalProfileCode { get; set; }

        [DisplayName("Salary Profile Title")]
        public string SalProfileTitle { get; set; }

        [DisplayName("Salary Profile Description")]
        public string SalProfileDescription { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDefault { get; set; }
        public int OrderID { get; set; }

        public bool IsAutomaticCalculation { get; set; }
    }
}
