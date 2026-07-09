using ReportingEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Templates
{
    public class LetterTemplate
    {
        public string Title => "Official Letter";

        public void Configure(ReportContext context)
        {
            context.ReportTitle = Title;
        }
    }
}
