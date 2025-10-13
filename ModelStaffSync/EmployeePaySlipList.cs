using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class EmployeePaySlipList
    {
        [DisplayName("EmpID")]
        public int EmpID { get; set; }

        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }

        [DisplayName("Employee Name")]
        public string EmpName { get; set; }

        [DisplayName("Designation")]
        public string DesignationTitle { get; set; }

        [DisplayName("Department")]
        public string DepartmentTitle { get; set; }

        [DisplayName("EmpSalID")]
        public int EmpSalID { get; set; }

        [DisplayName("Salary Date")]
        public DateTime EmpSalDate { get; set; }

        [DisplayName("Salary Month")]
        public string EmpSalMonthYear { get; set; }

        [DisplayName("SalaryOrderID")]
        public int OrderID { get; set; }
    }
}
