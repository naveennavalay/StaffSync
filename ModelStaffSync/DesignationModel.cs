using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class DesignationModel
    {
        public int DesignationID { get; set; }
        public string DesignationCode { get; set; }

        public string DesignationTitle { get; set; }
        public string DesignationInitial { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
