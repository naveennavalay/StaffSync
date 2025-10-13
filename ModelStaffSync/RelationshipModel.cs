using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class RelationshipModel
    {
        public int RelationshipID { get; set; }
        public string RelationshipCode { get; set; }

        public string RelationshipTitle { get; set; }
        public string RelationshipInitial { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
