using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class SpecificEmployeeSalaryProfileInfo
    {
        public int EmpID { get; set; }
        public int EmpSalProfileID { get; set; }
        public int SalProfileID { get; set; }
    }

    public class SpecificEmployeeSalaryInfo
    {
        public int EmpSalID { get; set; }
        public int EmpID { get; set; }
        public DateTime EmpSalDate { get; set; }
        public decimal TotalAllowance { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal TotalReimbursement { get; set; }
        public decimal NetPayable { get; set; }
        public int OrderID { get; set; }
    }
}
