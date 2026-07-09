using MigraDoc.DocumentObjectModel;
using ReportingEngine.Base;
using ReportingEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Reports.Employee
{
    public class EmployeeProfileReport : ReportBase
    {
        public EmployeeProfileReport(ReportContext context, ReportSettings settings)
            : base(context, settings)
        {
        }

        protected override void OnBuild()
        {
            Section section = Document.AddSection();
            var p = section.AddParagraph(Context.ReportTitle ?? "Employee Profile");
            p.Format.Font.Size = 18;
            p.Format.Font.Bold = true;
            section.AddParagraph("Employee profile report framework.");
        }
    }
}
