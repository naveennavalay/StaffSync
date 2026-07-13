using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Core
{
    public class DocumentContext
    {
        public CompanyInfo CompanyInfo { get; set; }

        public ReportInfo ReportInfo { get; set; }

        public List<ReportColumn> Columns { get; set; }

        public IEnumerable<object> Data { get; set; }

        public ReportSettings Settings { get; set; }

        public IList<ReportSummary> Summary { get; set; }

        public ReportDisplayOptions DisplayOptions { get; set; }
    }
}
