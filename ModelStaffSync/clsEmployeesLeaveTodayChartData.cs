using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class EmployeesLeaveTodayChartData
    {
        public int EmpID { get; set; }        
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string DesignationTitle { get; set; }
        public string DepartmentTitle { get; set; }
        public string LeaveTypeTitle { get; set; }
        public DateTime LeaveDate { get; set; }
        public string LeaveApprovalComments { get; set; }
        public int ClientID { get; set; }
        public int LeaveTRID { get; set; }
    }
}
