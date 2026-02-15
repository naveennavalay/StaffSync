using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class ClientBranchInfo
    {
        public int ClientBranchID { get; set; }
        public int ClientID { get; set; }

        [DisplayName("Branch Code")]
        public string ClientBranchCode { get; set; }

        [DisplayName("Branch Name")]
        public string ClientBranchName { get; set; }

        [DisplayName("Address1")]
        public string ClientBranchAddress1 { get; set; }

        [DisplayName("Address2")]
        public string ClientBranchAddress2 { get; set; }

        [DisplayName("Area")]
        public string ClientBranchArea { get; set; }

        [DisplayName("City")]
        public string ClientBranchCity { get; set; }

        [DisplayName("State")]
        public string ClientBranchState { get; set; }

        [DisplayName("PIN")]
        public string ClientBranchPIN { get; set; }

        [DisplayName("Country")]
        public string ClientBranchCountry { get; set; }

        [DisplayName("Phone")]
        public string ClientBranchPhone { get; set; }

        [DisplayName("Mail ID")]
        public string ClientBranchMailID { get; set; }

        [DisplayName("Contact Person")]
        public string ClientBranchContactPerson { get; set; }

        [DisplayName("Contact Number")]
        public string ClientBranchContactNumber { get; set; }

        [DisplayName("Contact Mail ID")]
        public string ClientBranchContactMail { get; set; }

        [DisplayName("WebSite")]
        public string ClientBranchWebSite { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
