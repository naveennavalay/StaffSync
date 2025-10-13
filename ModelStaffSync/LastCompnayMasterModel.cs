using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class LastCompnayMasterModel
    {
        public int LastCompanyInfoID { get; set; }
        public string LastCompanyCode { get; set; }

        public string LastCompanyTitle { get; set; }
        public string LastCompanyAddress { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
