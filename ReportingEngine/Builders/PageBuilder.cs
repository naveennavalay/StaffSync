using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Fields;
using ReportingEngine.Core;
using ReportingEngine.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Builders
{
    public class PageBuilder
    {
        public void Apply(Section section, ReportSettings settings)
        {
            section.PageSetup.PageFormat = settings.PageFormat;
            section.PageSetup.Orientation = settings.Orientation;
            section.PageSetup.TopMargin = settings.TopMargin;
            section.PageSetup.BottomMargin = settings.BottomMargin;
            section.PageSetup.LeftMargin = settings.LeftMargin;
            section.PageSetup.RightMargin = settings.RightMargin;
        }
    }
}
