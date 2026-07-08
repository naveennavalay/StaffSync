using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace ModelStaffSync
{
    public class AppMailTemplateModel
    {
        public int AppMailTempID { get; set; }
        public string AppMailTempName { get; set; }
        public string AppMailTempSubject { get; set; }
        public string AppMailTempBodyHTML { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
