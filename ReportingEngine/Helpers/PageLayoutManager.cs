using MigraDoc.DocumentObjectModel;
using ReportingEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Helpers
{
    /// <summary>
    /// Calculates page layout and automatically
    /// switches Portrait/Landscape and scales columns.
    /// </summary>
    public class PageLayoutManager
    {
        public void ApplyLayout(
            Section section,
            IList<ReportColumn> columns)
        {
            if (section == null)
                return;

            if (columns == null || columns.Count == 0)
                return;

            //-------------------------------------------------------
            // Total Requested Width
            //-------------------------------------------------------

            double totalWidth =
                columns
                .Where(c => c.Visible)
                .Sum(c => c.Width);

            //-------------------------------------------------------
            // Portrait Printable Width
            //-------------------------------------------------------

            //double portraitWidth = 18.0;
            double printableWidth = section.PageSetup.PageWidth.Centimeter - section.PageSetup.LeftMargin.Centimeter - section.PageSetup.RightMargin.Centimeter;

            //-------------------------------------------------------
            // Landscape Printable Width
            //-------------------------------------------------------

            double landscapeWidth = 26.0;

            //-------------------------------------------------------
            // Portrait
            //-------------------------------------------------------

            if (totalWidth <= printableWidth)
            {
                section.PageSetup.Orientation =
                    Orientation.Portrait;

                return;
            }

            //-------------------------------------------------------
            // Landscape
            //-------------------------------------------------------

            if (totalWidth <= landscapeWidth)
            {
                section.PageSetup.Orientation =
                    Orientation.Landscape;

                return;
            }

            //-------------------------------------------------------
            // Landscape + Scale
            //-------------------------------------------------------

            section.PageSetup.Orientation =
                Orientation.Landscape;

            double factor =
                landscapeWidth / totalWidth;

            foreach (ReportColumn column in columns)
            {
                if (!column.Visible)
                    continue;

                column.Width =
                    column.Width * factor;
            }
        }
    }
}
