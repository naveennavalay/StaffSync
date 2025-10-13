using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class StateModel
    {
        public int StateID { get; set; }
        public string StateCode { get; set; }

        public string StateTitle { get; set; }
        public string StateInitial { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
