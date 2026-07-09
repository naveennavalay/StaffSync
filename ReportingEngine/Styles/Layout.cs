using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Styles
{
    public static class Layout
    {
        public static readonly Unit TopMargin = Unit.FromCentimeter(1.5);
        public static readonly Unit BottomMargin = Unit.FromCentimeter(1.5);
        public static readonly Unit LeftMargin = Unit.FromCentimeter(1.5);
        public static readonly Unit RightMargin = Unit.FromCentimeter(1.5);

        public static readonly Unit HeaderSpacing = Unit.FromCentimeter(0.30);
        public static readonly Unit FooterSpacing = Unit.FromCentimeter(0.20);
        public static readonly Unit CellPadding = Unit.FromPoint(3);
        public static readonly Unit DefaultColumnWidth = Unit.FromCentimeter(3);
    }
}
