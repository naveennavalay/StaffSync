using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class PhotoInfo
    {
        public int PhotoID { get; set; }
        public int EmpID { get; set; }
        public byte[] EmpPhoto { get; set; }

    }
}
