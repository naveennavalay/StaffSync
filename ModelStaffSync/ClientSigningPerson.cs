using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class ClientSigningPerson
    {
        public int ClientSigningPersonID { get; set; }

        public int ClientID { get; set; }

        [DisplayName("Contact Person")]
        public string ClientSigningPersonName { get; set; }

        [DisplayName("Designation")]
        public string ClientSigningPersonDesignation { get; set; }

        [DisplayName("Father Name")]
        public string ClientSigningPersonFatherName { get; set; }

        [DisplayName("PAN Number")]
        public string ClientSigningPersonPANNumber { get; set; }

        [DisplayName("Gender")]
        public string ClientSigningPersonSex { get; set; }

        [DisplayName("Date Of Birth")]
        public DateTime ClientSigningPersonDOB { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public int OrderID { get; set; }
    }
}
