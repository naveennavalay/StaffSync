using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class ReimbursementModel
    {
        public int ReimbID { get; set; }

        [DisplayName("Reimbursement Code")]
        public string ReimbCode { get; set; }

        [DisplayName("Reimbursement Title")]
        public string ReimbTitle { get; set; }

        [DisplayName("Reimbursement Description")]
        public string ReimbDescription { get; set; }
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
