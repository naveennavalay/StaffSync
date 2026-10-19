using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class EmpAdvanceInfoModel
    {
        public int EmpID { get; set; }
        public string AdvanceName { get; set; }
        public DateTime IssueDate { get; set; }
        public decimal IssueAmount { get; set; }
        public DateTime LastPaymentDate { get; set; }
        public decimal CBalance { get; set; }

    }
}
