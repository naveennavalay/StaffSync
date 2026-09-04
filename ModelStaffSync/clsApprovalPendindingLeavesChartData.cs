using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class ApprovalPendindingLeavesChartData
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string DesignationTitle { get; set; }
        public string DepartmentTitle { get; set; }
        public DateTime LeaveDate { get; set; }
        public string LeaveType { get; set; }
        public decimal DaysToGo { get; set; }
    }
}
