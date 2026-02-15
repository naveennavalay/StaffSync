using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class CompanyBranchLogoInfo
    {
        public int ClientBranchID { get; set; }
        public byte[] ClientBranchLogo { get; set; }
    }
}
