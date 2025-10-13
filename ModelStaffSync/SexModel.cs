using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class SexModel
    {
        public int SexID { get; set; }
        public string SexCode { get; set; }

        public string SexTitle { get; set; }
        public string SexInitial { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
