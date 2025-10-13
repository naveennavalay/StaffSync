using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class EmployeeSalaryProfileInfo
    {
        public int SalProDetID { get; set; }
        public int SalProfileID { get; set; }
        public int HeaderID { get; set; }

        [DisplayName("Salary Header")]
        public string HeaderTitle { get; set; }

        [DisplayName("Type")]
        public string SalHeaderType { get; set; }
    }
}
