using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class DeductionModel
    {
        public int DedID { get; set; }

        [DisplayName("Deduction Code")]
        public string DedCode { get; set; }

        [DisplayName("Deduction Title")]
        public string DedTitle { get; set; }

        [DisplayName("Deduction Description")]
        public string DedDescription { get; set; }
        public bool IsFixed { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool VisibleInPayslip { get; set; }
        public bool ProrataBasis { get; set; }
        public int OrderID { get; set; }

        [DisplayName("Calculation Formula")]
        public string CalcFormula { get; set; }

        public decimal MaxCap { get; set; }
    }
}
