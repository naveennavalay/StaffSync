using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class ClientInfo
    {
        public int ClientID { get; set; }

        [DisplayName("Client Code")]
        public string ClientCode { get; set; }

        [DisplayName("Client Name")]
        public string ClientName { get; set; }

        [DisplayName("Address1")]
        public string ClientAddress1 { get; set; }

        [DisplayName("Address2")]
        public string ClientAddress2 { get; set; }

        [DisplayName("Area")]
        public string ClientArea { get; set; }

        [DisplayName("City")]
        public string ClientCity { get; set; }

        [DisplayName("State")]
        public string ClientState { get; set; }

        [DisplayName("PIN")]
        public string ClientPIN { get; set; }

        [DisplayName("Country")]
        public string ClientCountry { get; set; }

        [DisplayName("Phone")]
        public string ClientPhone { get; set; }

        [DisplayName("Mail ID")]
        public string ClientMailID { get; set; }

        [DisplayName("Contact Person")]
        public string ClientContactPerson { get; set; }

        [DisplayName("Contact Number")]
        public string ClientContactNumber { get; set; }

        [DisplayName("Contact Mail ID")]
        public string ClientContactMail { get; set; }

        [DisplayName("WebSite")]
        public string ClientWebSite { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class ClientFinYearInfo
    {
        public int ClientID { get; set; }
        public int FinYearID { get; set; }
    }
}
