using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class EmployeePayslipDetails
    {
        public int EmpSalDetID { get; set; }
        public int SalProDetID { get; set; }
        public int EmpSalID { get; set; }
        public int HeaderID { get; set; }

        [DisplayName("Salary Header")]
        public string HeaderTitle { get; set; }

        [DisplayName("Type")]
        public string HeaderType { get; set; }

        public string CalcFormula { get; set; }

        [DisplayName("Actual Amount")]
        public decimal ActualAmount { get; set; }

        [DisplayName("Allowance Amount")]
        public decimal AllowanceAmount { get; set; }

        [DisplayName("Deduction Amount")]
        public decimal DeductionAmount { get; set; }

        [DisplayName("Reimbursment Amount")]
        public decimal ReimbursmentAmount { get; set; }
        public int OrderID { get; set; }
    }
}
