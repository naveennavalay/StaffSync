using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class EmployeeBirthdayInfo
    {
        [DisplayName("Employee ID")] 
        public int EmpID { get; set; }

        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }

        [DisplayName("Employee Name")]
        public string EmpName { get; set; }

        [DisplayName("Designation")]
        public string DesignationTitle { get; set; }

        [DisplayName("Department")]
        public string DepartmentTitle { get; set; }

        [DisplayName("Date Of Birth")]
        public DateTime DOB { get; set; }

        [DisplayName("Mail ID")]
        public string ContactNumber2 { get; set; }
        public int ClientID { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class EmployeeWorkAnniversaryInfo
    {
        [DisplayName("Employee ID")]
        public int EmpID { get; set; }

        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }

        [DisplayName("Employee Name")]
        public string EmpName { get; set; }

        [DisplayName("Designation")]
        public string DesignationTitle { get; set; }

        [DisplayName("Department")]
        public string DepartmentTitle { get; set; }

        [DisplayName("Date Of Joining")]
        public DateTime DOJ { get; set; }

        [DisplayName("Mail ID")]
        public string ContactNumber2 { get; set; }
        public int ClientID { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }

    public class EmployeeWorkProbationCompletionInfo
    {
        [DisplayName("Employee ID")]
        public int EmpID { get; set; }

        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }

        [DisplayName("Employee Name")]
        public string EmpName { get; set; }

        [DisplayName("Designation")]
        public string DesignationTitle { get; set; }

        [DisplayName("Department")]
        public string DepartmentTitle { get; set; }

        [DisplayName("Date Of Joining")]
        public DateTime DOJ { get; set; }

        [DisplayName("Date Of Confirmation")]
        public DateTime DOC { get; set; }

        [DisplayName("Mail ID")]
        public string ContactNumber2 { get; set; }
        public int ClientID { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class ConfirmationOfEmploymentInfo
    {
        [DisplayName("Employee ID")]
        public int EmpID { get; set; }

        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }

        [DisplayName("Employee Name")]
        public string EmpName { get; set; }

        [DisplayName("Designation")]
        public string DesignationTitle { get; set; }

        [DisplayName("Department")]
        public string DepartmentTitle { get; set; }

        [DisplayName("Date Of Joining")]
        public DateTime DOJ { get; set; }

        [DisplayName("Date Of Confirmation")]
        public DateTime DOC { get; set; }

        [DisplayName("Mail ID")]
        public string ContactNumber2 { get; set; }
        public int ClientID { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
