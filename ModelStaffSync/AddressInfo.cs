using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class AddressInfo
    {
        public int AddressID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string PIN { get; set; }
        public int StateID { get; set; }
        public string State { get; set; }
        public int CountryID { get; set; }
        public string Country { get; set; }
    }
}
