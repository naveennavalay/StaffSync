using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class OutstandingLeaveStatement
    {
        public bool Select { get; set; }
        public int EmpID { get; set; }

        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }

        [DisplayName("Employee Name")]
        public string EmpName { get; set; }

        [DisplayName("Designation")]
        public string DesignationTitle { get; set; }

        [DisplayName("Department")]
        public string DepartmentTitle { get; set; }

        [DisplayName("Total Leaves")]
        public float TotalLeaves { get; set; }

        [DisplayName("Balance Leaves")]
        public float BalanceLeaves { get; set; }

        [DisplayName("Utilised Leaves")]
        public float UtilisedLeaves { get; set; }
    }
}
