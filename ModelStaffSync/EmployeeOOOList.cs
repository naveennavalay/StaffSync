using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.ComponentModel;

namespace ModelStaffSync
{
    public class EmployeeOOOList
    {
        public int EmpID { get; set; }

        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }

        [DisplayName("Employee Name")]
        public string EmpName { get; set; }

        [DisplayName("Designation")]
        public string DesignationTitle { get; set; }

        [DisplayName("Department")] 
        public string DepartmentTitle { get; set; }

        [DisplayName("Leave Type")]
        public string LeaveTypeTitle { get; set; }

        [DisplayName("Leave From")]
        public DateTime ActualLeaveDateFrom { get; set; }

        [DisplayName("Leave To")] 
        public DateTime ActualLeaveDateTo { get; set; }

        [DisplayName("Duration")] 
        public decimal LeaveDuration { get; set; }

        [DisplayName("Mode")] 
        public string LeaveMode { get; set; }

        [DisplayName("OrderID")]
        public int OrderID { get; set; }
    }
}
