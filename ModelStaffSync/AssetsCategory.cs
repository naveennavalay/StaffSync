using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class AssetsCategory
    {
        public int AssetCatMasID { get; set; }
        public string AssetCode { get; set; }
        public string AssetName { get; set; }
        public string AssetDescription { get; set; }
        public string AssetNote { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int ClientID { get; set; }
    }
}
