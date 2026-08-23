using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class LeaveInfoDepartmentInfoChartData
    {
        public int DepartmentID { get; set; }

        public string DepartmentTitle { get; set; }

        public double TotalLeaveAllotted { get; set; }

        public double TotalLeaveAvailed { get; set; }

        public double TotalLeaveBalance { get; set; }
    }
}
