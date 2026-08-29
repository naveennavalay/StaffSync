using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class LeaveInfoEmpWiseChartData
    {
        public int EmpID { get; set; }

        public string EmpName { get; set; }

        public int DepartmentID { get; set; }

        public double TotalLeaveAllotted { get; set; }

        public double TotalLeaveAvailed { get; set; }

        public double TotalLeaveBalance { get; set; }

        public string BalanceCategory { get; set; }
    }
}
