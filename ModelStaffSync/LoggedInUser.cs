using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class LoggedInUser
    {
        public int EmpID { get; set; }
        public int ClientID { get; set; }

        [DisplayName("Employee Code")]
        public string EmpCode { get; set; }

        [DisplayName("Employee Name")]
        public string EmpName { get; set; }

        [DisplayName("Designation")]
        public string DesignationTitle { get; set; }

        [DisplayName("Department")]
        public string DepartmentTitle { get; set; }

        [DisplayName("Blood Group")]
        public string BloodGroupTitle { get; set; }

        [DisplayName("Date Of Birth")]
        public DateTime DOB { get; set; }

        [DisplayName("Date Of Joining")]
        public DateTime DOJ { get; set; }


        [DisplayName("Address")]
        public string Address1 { get; set; }

        [DisplayName("Address")]
        public string Address2 { get; set; }

        [DisplayName("Area")]
        public string Area { get; set; }

        [DisplayName("City")]
        public string City { get; set; }

        [DisplayName("State")]
        public string StateTitle { get; set; }

        [DisplayName("PIN")]
        public string PIN { get; set; }

        [DisplayName("Country")]
        public string CountryTitle { get; set; }

        [DisplayName("Gender")]
        public string SexTitle { get; set; }

        [DisplayName("Nominee Name")]
        public string NomineePerson { get; set; }

        [DisplayName("Relationship")]
        public string RelationShipTitle { get; set; }

        [DisplayName("Contact Number")]
        public string ContactNumber1 { get; set; }

        [DisplayName("Mail ID")]
        public string ContactNumber2 { get; set; }

        [DisplayName("Bank Account Number")]
        public string EmpACNumber { get; set; }

        [DisplayName("Bank Name")]
        public string BankName { get; set; }

        [DisplayName("Bank Address")]
        public string BankAddress { get; set; }

        [DisplayName("Bank IFSC Code")]
        public string IFSCCode { get; set; }

        [DisplayName("Outstanding Leave Balance")]
        public decimal BalanceLeaves { get; set; }
    }
}
