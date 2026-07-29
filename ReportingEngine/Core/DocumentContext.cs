using ModelStaffSync;
using ReportingEngine.Layout;
using ReportingEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Core
{
    public class DocumentContext
    {
        public List<ReportSort> SortColumns { get; } = new List<ReportSort>();

        public CompanyInfo CompanyInfo { get; set; }

        public ReportInfo ReportInfo { get; set; }

        public List<ReportColumn> Columns { get; set; }

        public IEnumerable<object> Data { get; set; }

        public ReportSettings Settings { get; set; }

        public IList<ReportSummary> Summary { get; set; }

        public ReportDisplayOptions DisplayOptions { get; set; }

        public Dictionary<string, bool> ColumnVisibility { get; set; } = new Dictionary<string, bool>();

        public string GroupByProperty { get; set; }

        public string GroupCaption { get; set; }

        public List<ReportDynamicTable> AdditionalTables
        {
            get;
            set;
        } = new List<ReportDynamicTable>();

        public List<ReportTableRow> AdditionalTableRows
        {
            get;
            set;
        } = new List<ReportTableRow>();
    }

    public class ReportSort
    {
        public string PropertyName { get; set; }

        public bool Descending { get; set; }
    }
}
