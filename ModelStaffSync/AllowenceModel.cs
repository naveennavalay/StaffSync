using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class AllowenceModel
    {
        public int AllID { get; set; }

        [DisplayName("Allowance Code")]
        public string AllCode { get; set; }

        [DisplayName("Allowance Title")]
        public string AllTitle { get; set; }

        [DisplayName("Allowance Description")]
        public string AllDescription { get; set; }

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
