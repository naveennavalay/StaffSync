using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class CurrentAssetStatusModel
    {
        public int CurrentAssetStatusID { get; set; }
        public string CurrentAssetStatusCode { get; set; }

        public string CurrentAssetStatusName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string CurrentAssetDescription { get; set; }
    }
}
