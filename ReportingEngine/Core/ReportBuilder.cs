using ModelStaffSync;
using ReportingEngine.Core;
using ReportingEngine.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine
{
    public class ReportBuilder
    {
        private readonly DocumentContext _context;

        public ReportBuilder()
        {
            _context = new DocumentContext();

            _context.CompanyInfo = new CompanyInfo();
            _context.ReportInfo = new ReportInfo();

            _context.Columns = new List<ReportColumn>();
            _context.Data = new List<object>();

            _context.Settings = new ReportSettings();
            _context.DisplayOptions = new ReportDisplayOptions();
        }

        public ReportBuilder Summary(IList<ReportSummary> summary)
        {
            _context.Summary = summary;
            return this;
        }

        public ReportBuilder Company(CompanyInfo company)
        {
            _context.CompanyInfo = company;
            return this;
        }

        public ReportBuilder Title(ReportInfo report)
        {
            _context.ReportInfo = report;
            return this;
        }

        public ReportBuilder Data<T>(IEnumerable<T> data)
        {
            _context.Data = data.Cast<object>().ToList();

            _context.Columns = Helpers.ReportMetadataReader.Create<T>();

            foreach (ReportColumn column in _context.Columns)
            {
                if (_context.ColumnVisibility.ContainsKey(column.PropertyName))
                {
                    column.Visible = _context.ColumnVisibility[column.PropertyName];
                }
            }

            return this;
        }

        public ReportBuilder SetColumnVisibility(string propertyName, bool visible)
        {
            _context.ColumnVisibility[propertyName] = visible;
            return this;
        }

        public ReportBuilder Layout(ReportSettings settings)
        {
            _context.Settings = settings;
            return this;
        }

        public ReportBuilder Display(ReportDisplayOptions options)
        {
            _context.DisplayOptions = options;
            return this;
        }

        public ReportBuilder Settings(ReportSettings settings)
        {
            _context.Settings = settings;
            return this;
        }

        public void Generate(string outputFile)
        {
            ReportDesigner designer = new ReportDesigner();

            designer.Generate(_context, outputFile);
        }

        public ReportBuilder GroupBy(string propertyName, string caption = null)
        {
            _context.GroupByProperty = propertyName;

            _context.GroupCaption = caption ?? propertyName;

            return this;
        }
    }
}
