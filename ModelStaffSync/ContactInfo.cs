using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class ContactInfo
    {
        public int ContactID { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonAddressInfo { get; set; }
        public int RelationShipID { get; set; }
        public int SexID { get; set; }
    }
}
