using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public static class CurrentUser
    {
        public static int EmpID { get; set; }
        public static string EmpCode { get; set; }
        public static string EmpName { get; set; }
        public static string DesignationTitle { get; set; }
        public static string DepartmentTitle { get; set; }
        public static string BloodGroupTitle { get; set; }
        public static DateTime DOB { get; set; }
        public static DateTime DOJ { get; set; }
        public static string Address1 { get; set; }
        public static string Address2 { get; set; }
        public static string Area { get; set; }
        public static string City { get; set; }
        public static string StateTitle { get; set; }
        public static string PIN { get; set; }
        public static string CountryTitle { get; set; }
        public static string SexTitle { get; set; }
        public static string NomineePerson { get; set; }
        public static string RelationShipTitle { get; set; }
        public static string ContactNumber1 { get; set; }
        public static string ContactNumber2 { get; set; }
        public static string EmpACNumber { get; set; }
        public static string BankName { get; set; }
        public static string BankAddress { get; set; }
        public static string IFSCCode { get; set; }
        public static decimal BalanceLeaves { get; set; }
        public static int ClientID { get; set; } = 1;

    }
}
