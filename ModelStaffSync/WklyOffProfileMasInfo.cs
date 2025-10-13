using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace ModelStaffSync
{
    public class WklyOffProfileMasInfo
    {
        public int WklyOffMasID { get; set; }

        [DisplayName("Weekly Off Code")]
        public string WklyOffCode { get; set; }

        [DisplayName("Weekly Off Title")]
        public string WklyOffTitle { get; set; }

        [DisplayName("Effective From")]
        public DateTime EffectDateFrom { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }
    }
}
