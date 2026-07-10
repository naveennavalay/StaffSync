using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using ReportingEngine.Core;
using ReportingEngine.Models;
using ReportingEngine.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Builders
{
    public class FooterBuilder
    {
        public void Build(
            Section section,
            ReportInfo report,
            ReportDisplayOptions display)
        {
            if (!display.ShowFooter)
                return;

            HeaderFooter footer = section.Footers.Primary;

            Table table = footer.AddTable();

            table.Borders.Visible = false;

            table.AddColumn(Unit.FromCentimeter(11.5));

            table.AddColumn(Unit.FromCentimeter(8));

            //----------------------------------------------------
            // Top Border
            //----------------------------------------------------

            Row border = table.AddRow();

            border.Borders.Top.Width = 0.75;

            border.Borders.Top.Color = Colors.Gray;

            border.Cells[0].MergeRight = 1;

            //----------------------------------------------------
            // Footer Row
            //----------------------------------------------------

            Row row = table.AddRow();

            row.VerticalAlignment = VerticalAlignment.Center;

            //
            // Left Side
            //
            Paragraph left = row.Cells[0].AddParagraph();

            left.Format.Font.Size = 8;

            left.Format.Alignment = ParagraphAlignment.Left;

            left.AddFormattedText("StaffSync Payroll Management System", TextFormat.Bold);

            //
            // Right Side
            //
            Paragraph right = row.Cells[1].AddParagraph();

            right.Format.Alignment = ParagraphAlignment.Right;

            right.Format.Font.Size = 8;

            bool first = true;

            if (display.ShowGeneratedDate)
            {
                right.AddFormattedText("Generated : ", TextFormat.Bold);
                right.AddText(report.GeneratedOn.ToString("dd-MMM-yyyy hh:mm tt"));

                first = false;
            }

            if (display.ShowPageNumbers)
            {
                if (!first)
                    right.AddText("      ");

                right.AddFormattedText("Page : ", TextFormat.Bold);

                right.AddPageField();

                right.AddText(" of ");

                right.AddNumPagesField();
            }
        }
    }
}
