using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class CompanyLogoInfo
    {
        public int CompanyID { get; set; }
        public byte[] ClientLogo { get; set; }
    }
}
