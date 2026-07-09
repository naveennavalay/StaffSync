using ReportingEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Templates
{
    public class PayslipTemplate
    {
        public string Title => "Employee Payslip";

        public void Configure(ReportContext context)
        {
            context.ReportTitle = Title;
        }
    }
}
