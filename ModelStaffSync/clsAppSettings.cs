using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class AppSettings
    {
        public int AppSettingID { get; set; }
        public string AppSettingCode { get; set; }
        public string AppSettingTitle { get; set; }
        public string AppSettingValue { get; set; }
    }
}
