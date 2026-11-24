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
        [GridColumn(Visible = false, Width = 200, Alignment = "left")]
        public int EmpID { get; set; }

        [GridColumn(Visible = false, Width = 200, Alignment = "left")]
        public string EmpCode { get; set; }

        [GridColumn(Visible = false, Width = 200, Alignment = "left")]
        public string EmpName { get; set; }

        [GridColumn(Visible = false, Width = 200, Alignment = "left")]
        public int EmpDocumentID { get; set; }

        [GridColumn(Visible = false, Width = 200, Alignment = "left")]
        public int DocID { get; set; }

        [GridColumn(Visible = true, Width = 200, Alignment = "left")]
        public string DocCode { get; set; }

        [GridColumn(Visible = true, Width = 200, Alignment = "left")]
        public string DocName { get; set; }

        [GridColumn(Visible = true, Width = 200, Alignment = "left")]
        public string DocType { get; set; }

        [GridColumn(Visible = true, Width = 200, Alignment = "left")]
        public DateTime DocUploadDate { get; set; }

        [GridColumn(Visible = false, Width = 200, Alignment = "left")]
        public string DocPath { get; set; }

        [GridColumn(Visible = true, Width = 200, Alignment = "left")]
        public bool DocUploadStatus { get; set; }
    }
}
