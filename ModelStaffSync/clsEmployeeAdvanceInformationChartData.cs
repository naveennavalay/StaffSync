using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class EmployeeAdvanceInformationChartData
    {
        public int? EmpID { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public int? AdvanceTypeID { get; set; }
        public string AdvanceTypeTitle { get; set; }
        public int? EmpAdvanceRequestID { get; set; }
        public string EmpAdvReqCode { get; set; }
        public decimal? AdvanceAmount { get; set; }
        public int? AdvanceYear { get; set; }
        public int? AdvanceMonth { get; set; }
        public string AdvanceMonthName { get; set; }
        public int? AdvanceDay { get; set; }
        public DateTime? AdvanceDate { get; set; }
        public decimal? RequestAmount { get; set; }
        public decimal? IssuedAmount { get; set; }
        public decimal? OutstandingAmount { get; set; }
        public decimal? RecoveredAmount { get; set; }
    }
}
