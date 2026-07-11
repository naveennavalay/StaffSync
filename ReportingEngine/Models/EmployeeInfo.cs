using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Models
{
    public class EmployeeInfo
    {
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Status { get; set; }
        public DateTime EmployeeDOB { get; set; }
        public DateTime EmployeeDOJ { get; set; }
        public decimal EmployeeAnnualCTC { get; set; }
    }
}
