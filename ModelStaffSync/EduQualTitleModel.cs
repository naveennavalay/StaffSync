using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.ComponentModel;

namespace ModelStaffSync
{
    public class EduQualTitleModel
    {
        public int EduQualID { get; set; }

        [DisplayName("Qualification Code")]
        public string EduQualCode { get; set; }

        [DisplayName("Qualification Title")]
        public string EduQualTitle { get; set; }

        [DisplayName("Qualification Initials")] 
        public string EduQualInitial { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
