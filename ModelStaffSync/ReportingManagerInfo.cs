using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class ReportingManagerInfo
    {
        public int EmpID { get; set; }

        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }

        [DisplayName("Employee Name")]
        public string EmpName { get; set; }

        [DisplayName("Date of Birth")] 
        public DateTime DOB { get; set; }

        [DisplayName("Designation")]
        public string DesignationTitle { get; set; }

        [DisplayName("Department")]
        public string DepartmentTitle { get; set; }

        [DisplayName("Date of Joining")]
        public DateTime DOJ { get; set; }

        [DisplayName("Contact Number")]
        public string ContactNumber1 { get; set; }

        [DisplayName("Mail ID")]
        public string ContactNumber2 { get; set; }

        public int StateID { get; set; }

        public int SexID { get; set; }
    }
}
