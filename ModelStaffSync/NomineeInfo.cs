using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class NomineeInfo
    {
        public int NomineeID { get; set; }
        public string NomineePerson { get; set; }
        public int EmpID { get; set; }
        public int RelationshipID { get; set; }
        public string ContactNumber { get; set; }
    }
}
