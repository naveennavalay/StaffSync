using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class PrintSettings
    {
        public int PRNTSettingID { get; set; }
        public int ClientID { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public bool PrntReportGeneratedBy { get; set; }
        public bool PrntReportGeneratedOn { get; set; }
        public bool PrntLogoInReport { get; set; }
        public bool PrntHeaderInReport { get; set; }
        public bool PrntFooterInReport { get; set; }
        public bool PnrtShowWatermark { get; set; }
    }
}
