using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Styles
{
    /// <summary>
    /// Central place for all report styling.
    /// Similar to a CSS file for HTML.
    /// Every report will use these styles.
    /// </summary>
    public static class ReportStyleManager
    {
        #region Document

        public static void ApplyDocumentStyle(Document document)
        {
            document.DefaultPageSetup.PageFormat = PageFormat.A4;
            document.DefaultPageSetup.Orientation = Orientation.Portrait;

            document.DefaultPageSetup.TopMargin = Unit.FromCentimeter(1.5);
            document.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(1.5);
            document.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(1.5);
            document.DefaultPageSetup.RightMargin = Unit.FromCentimeter(1.5);
        }

        #endregion

        #region Heading 1

        public static void ApplyHeading1(Paragraph paragraph)
        {
            paragraph.Format.Font.Name = "Calibri";
            paragraph.Format.Font.Size = 18;
            paragraph.Format.Font.Bold = true;

            paragraph.Format.SpaceBefore = 0;
            paragraph.Format.SpaceAfter = Unit.FromPoint(10);

            paragraph.Format.Alignment = ParagraphAlignment.Center;
        }

        #endregion

        #region Heading 2

        public static void ApplyHeading2(Paragraph paragraph)
        {
            paragraph.Format.Font.Name = "Calibri";
            paragraph.Format.Font.Size = 14;
            paragraph.Format.Font.Bold = true;

            paragraph.Format.SpaceBefore = Unit.FromPoint(5);
            paragraph.Format.SpaceAfter = Unit.FromPoint(5);

            paragraph.Format.Alignment = ParagraphAlignment.Left;
        }

        #endregion

        #region Normal Text

        public static void ApplyNormalText(Paragraph paragraph)
        {
            paragraph.Format.Font.Name = "Calibri";
            paragraph.Format.Font.Size = 10;

            paragraph.Format.SpaceAfter = Unit.FromPoint(3);

            paragraph.Format.Alignment = ParagraphAlignment.Left;
        }

        #endregion

        #region Small Text

        public static void ApplySmallText(Paragraph paragraph)
        {
            paragraph.Format.Font.Name = "Calibri";
            paragraph.Format.Font.Size = 8;

            paragraph.Format.Alignment = ParagraphAlignment.Left;
        }

        #endregion

        #region Table Header

        public static void ApplyTableHeader(Cell cell)
        {
            cell.Format.Font.Name = "Calibri";
            cell.Format.Font.Size = 10;
            cell.Format.Font.Bold = true;

            cell.Format.Alignment = ParagraphAlignment.Center;

            cell.VerticalAlignment = VerticalAlignment.Center;

            cell.Shading.Color = Colors.LightSteelBlue;

            cell.Borders.Width = 0.5;
        }

        #endregion

        #region Table Cell

        public static void ApplyTableCell(Cell cell)
        {
            cell.Format.Font.Name = "Calibri";
            cell.Format.Font.Size = 9;

            cell.VerticalAlignment = VerticalAlignment.Center;

            cell.Borders.Width = 0.25;
        }

        #endregion

        #region Alternate Row

        public static void ApplyAlternateRow(Cell cell)
        {
            cell.Shading.Color = Colors.AliceBlue;
        }

        #endregion

        #region Footer

        public static void ApplyFooter(Paragraph paragraph)
        {
            paragraph.Format.Font.Name = "Calibri";
            paragraph.Format.Font.Size = 8;

            paragraph.Format.Alignment = ParagraphAlignment.Center;
        }

        #endregion

        #region Report Title

        public static void ApplyReportTitle(Paragraph paragraph)
        {
            paragraph.Format.Font.Name = "Calibri";
            paragraph.Format.Font.Size = 20;
            paragraph.Format.Font.Bold = true;

            paragraph.Format.Alignment = ParagraphAlignment.Center;

            paragraph.Format.SpaceAfter = Unit.FromPoint(15);
        }

        #endregion

        #region Company Name

        public static void ApplyCompanyName(Paragraph paragraph)
        {
            paragraph.Format.Font.Name = "Calibri";
            paragraph.Format.Font.Size = 16;
            paragraph.Format.Font.Bold = true;

            paragraph.Format.Alignment = ParagraphAlignment.Center;
        }

        #endregion

        #region Summary

        public static void ApplySummary(Paragraph paragraph)
        {
            paragraph.Format.Font.Name = "Calibri";
            paragraph.Format.Font.Size = 10;
            paragraph.Format.Font.Bold = true;

            paragraph.Format.SpaceBefore = Unit.FromPoint(10);

            paragraph.Format.SpaceAfter = Unit.FromPoint(10);
        }

        #endregion

        #region Watermark

        public static void ApplyWatermark(Paragraph paragraph)
        {
            paragraph.Format.Font.Name = "Calibri";
            paragraph.Format.Font.Size = 60;
            paragraph.Format.Font.Bold = true;

            paragraph.Format.Alignment = ParagraphAlignment.Center;
        }

        #endregion
    }
}
