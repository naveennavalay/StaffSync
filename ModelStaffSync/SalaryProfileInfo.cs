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
        [GridColumn(Visible = true, Width = 200)]
        public string HeaderTitle { get; set; }

        [DisplayName("Type")]
        [GridColumn(Visible = true, Width = 200)]
        public string HeaderType { get; set; }

        [DisplayName("Calc. Formula")]
        [GridColumn(Visible = false, Width = 200)]
        public string CalcFormula { get; set; }

        [DisplayName("Calc. Formula1")]
        [GridColumn(Visible = false, Width = 200, Alignment = "right" )]
        public string CalcFormula1 { get; set; }

        [GridColumn(Visible = false, Width = 200)]
        public bool IsFixed { get; set; }

        [DisplayName("Actual Amount")]
        [GridColumn(Visible = false, Width = 200, Alignment = "right")] 
        public decimal ActualAmount { get; set; }

        [DisplayName("Allowance Amount")]
        [GridColumn(Visible = true, Width = 200, Alignment = "right")] 
        public decimal AllowanceAmount { get; set; }

        [DisplayName("Deduction Amount")]
        [GridColumn(Visible = true, Width = 200, Alignment = "right")] 
        public decimal DeductionAmount { get; set; }

        [DisplayName("Reimbursment Amount")]
        [GridColumn(Visible = true, Width = 200, Alignment = "right")] 
        public decimal ReimbursmentAmount { get; set; }

        [GridColumn(Visible = false, Width = 200)] 
        public decimal SalProAmount { get; set; }

        [GridColumn(Visible = false, Width = 200)] 
        public int OrderID { get; set; }
    }
}
