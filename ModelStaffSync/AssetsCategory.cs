using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.ComponentModel;

namespace ModelStaffSync
{
    public class AssetsCategory
    {
        public int AssetCatMasID { get; set; }

        [DisplayName("Category Code")]
        public string AssetCode { get; set; }
        
        [DisplayName("Category Name")] 
        public string AssetName { get; set; }
        
        [DisplayName("Category Description")] 
        public string AssetDescription { get; set; }
        public string AssetNote { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int ClientID { get; set; }
    }
}
