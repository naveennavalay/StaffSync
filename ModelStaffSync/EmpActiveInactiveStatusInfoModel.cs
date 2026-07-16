using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class EmpActiveInactiveStatusInfoModel
    {
        public int EmpActiveInactiveStatusID { get; set; }

        public int PersonalInfoID { get; set; }

        [DisplayName("Date")]
        public DateTime ActiveInactiveStatusDate { get; set; }

        [DisplayName("Status")]
        public bool ActiveInactiveStatus { get; set; }

        [DisplayName("Comments")]
        public string Comments { get; set; }
    }
}
