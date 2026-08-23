using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class LeaveInfoEmpSpecificDetailsChartData
    {
        public int EmpID { get; set; }

        public string EmpCode { get; set; }

        public string EmpName { get; set; }

        public string DesignationTitle { get; set; }

        public string DepartmentTitle { get; set; }

        public int LeaveTypeID { get; set; }

        public string LeaveTypeTitle { get; set; }

        public double BalanceLeaves { get; set; }
    }
}
