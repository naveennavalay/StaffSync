using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class SalaryProfileInfo
    {
        public int EmpSalDetID { get; set; }
        public int SalProDetID { get; set; }
        public int SalProfileID { get; set; }
        public int HeaderID { get; set; }

        [DisplayName("Salary Header")]
        public string HeaderTitle { get; set; }

        [DisplayName("Type")]
        public string HeaderType { get; set; }

        [DisplayName("Calc. Formula")]
        public string CalcFormula { get; set; }
        public bool IsFixed { get; set; }

        [DisplayName("Allowance Amount")]
        public decimal AllowanceAmount { get; set; }

        [DisplayName("Deduction Amount")]
        public decimal DeductionAmount { get; set; }

        [DisplayName("Reimbursment Amount")]
        public decimal ReimbursmentAmount { get; set; }

        public decimal SalProAmount { get; set; }
        public int OrderID { get; set; }
    }
}
