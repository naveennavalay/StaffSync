using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Styles
{
    public static class ReportStyles
    {
        public static void ApplyTitle(Paragraph p)
        {
            p.Format.Font.Name = Fonts.Family;
            p.Format.Font.Size = Fonts.TitleSize;
            p.Format.Font.Bold = true;
            p.Format.Font.Color = ColorPalette.Title;
            p.Format.Alignment = ParagraphAlignment.Center;
            p.Format.SpaceAfter = Layout.HeaderSpacing;
        }

        public static void ApplySubTitle(Paragraph p)
        {
            p.Format.Font.Name = Fonts.Family;
            p.Format.Font.Size = Fonts.SubTitleSize;
            p.Format.Alignment = ParagraphAlignment.Center;
        }

        public static void ApplyBody(Paragraph p)
        {
            p.Format.Font.Name = Fonts.Family;
            p.Format.Font.Size = Fonts.BodySize;
        }

        public static void ApplyFooter(Paragraph p)
        {
            p.Format.Font.Name = Fonts.Family;
            p.Format.Font.Size = Fonts.FooterSize;
            p.Format.Alignment = ParagraphAlignment.Center;
            p.Format.Font.Color = ColorPalette.Footer;
        }

        public static void ApplyHeaderCell(Cell cell)
        {
            cell.Format.Font.Name = Fonts.Family;
            cell.Format.Font.Size = Fonts.HeaderSize;
            cell.Format.Font.Bold = true;
            cell.Shading.Color = ColorPalette.HeaderBackground;
            cell.Borders.Color = ColorPalette.Border;
            cell.Borders.Width = 0.5;
        }

        public static void ApplyBodyCell(Cell cell, bool alternate = false)
        {
            cell.Format.Font.Name = Fonts.Family;
            cell.Format.Font.Size = Fonts.BodySize;
            cell.Borders.Color = ColorPalette.Border;
            cell.Borders.Width = 0.25;
            if (alternate)
                cell.Shading.Color = ColorPalette.AlternateRow;
        }

        public static void ApplySummaryCell(Cell cell)
        {
            cell.Format.Font.Name = Fonts.Family;
            cell.Format.Font.Size = Fonts.SummarySize;
            cell.Format.Font.Bold = true;
            cell.Shading.Color = ColorPalette.SummaryBackground;
            cell.Borders.Color = ColorPalette.Border;
        }
    }
}
