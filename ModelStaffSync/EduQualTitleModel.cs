using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class EduQualTitleModel
    {
        public int EduQualID { get; set; }
        public string EduQualCode { get; set; }

        public string EduQualTitle { get; set; }
        public string EduQualInitial { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
