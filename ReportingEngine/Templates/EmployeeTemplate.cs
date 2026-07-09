using ReportingEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Templates
{
    public class EmployeeTemplate
    {
        public string Title => "Employee Master Report";

        public void Configure(ReportContext context)
        {
            context.ReportTitle = Title;
        }
    }
}
