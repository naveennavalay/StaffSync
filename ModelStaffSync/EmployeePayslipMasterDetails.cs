using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class EmployeePayslipMasterDetails
    {
        public int EmpSalID { get; set; }
        public DateTime EmpSalDate { get; set; }
        public string EmpSalMonthYear { get; set; }
        public double TotalDaysInMonth { get; set; }
        public double TotalDaysWorked { get; set; }
        public double TotalDaysOnLeave { get; set; }
        public double NetPayable { get; set; }
        public int EmpID { get; set; }
    }
}
