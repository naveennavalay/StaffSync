using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;

namespace ModelStaffSync
{
    public class EmployeeDocumentInfo
    {
        public int EmpID { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public int EmpDocumentID { get; set; }
        public int DocID { get; set; }
        public string DocCode { get; set; }
        public string DocName { get; set; }
        public string DocType { get; set; }
        public DateTime DocUploadDate { get; set; }
        public string DocPath { get; set; }
        public bool DocUploadStatus { get; set; }
    }
}
