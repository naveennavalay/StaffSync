using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class ResignationInfo
    {
        public int ResigMasID { get; set; }

        [DisplayName("Resignation Code")]
        public string ResigMasCode { get; set; }

        [DisplayName("Resignation Reason")]
        public string ResigMasTitle { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class IsResignationAppliedInfo
    {
        public int EmpResignID { get; set; }
        public int EmpResignStatus { get; set; }
    }

    public class EmployeeSpecificResignationInfo
    {
        public int EmpID { get; set; }
        public int PersonalInfoID { get; set; }
        public int ResigMasID { get; set; }
        public string ResigMasCode { get; set; }
        public string ResigMasTitle { get; set; }
        public int EmpResignID { get; set; }
        public string EmpResignCode { get; set; }
        public DateTime EmpResignDate { get; set; }
        public string EmpResignMoreInfo { get; set; }
        public int EmpResignApprovedBy1 { get; set; }
        public bool EmpResignApprovalStatus1 { get; set; }
        public DateTime EmpResignApprovalDate1 { get; set; }
        public int EmpResignApprovedBy2 { get; set; }
        public bool EmpResignApprovalStatus2 { get; set; }
        public DateTime EmpResignApprovalDate2 { get; set; }
        public int EmpResignStatus { get; set; }
        public bool ResignStatus { get; set; }
    }

    public class ResignedEmployeesList
    {
        public int EmpID { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string DesignationTitle { get; set; }
        public string DepartmentTitle { get; set; }
        public string ContactNumber1 { get; set; }
        public string ContactNumber2 { get; set; }
        public int PersonalInfoID { get; set; }
        public int ResigMasID { get; set; }
        public string ResigMasCode { get; set; }
        public string ResigMasTitle { get; set; }
        public int EmpResignID { get; set; }
        public string EmpResignCode { get; set; }
        public DateTime EmpResignDate { get; set; }
        public string EmpResignMoreInfo { get; set; }
    }
}
