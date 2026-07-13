using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using ModelStaffSync;
using ReportingEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Builders
{
    public class SummaryBuilder
    {
        private readonly DocumentContext _context;

        public SummaryBuilder(DocumentContext context)
        {
            _context = context;
        }

        public void Build(Section section)
        {
            if (_context.Summary == null)
                return;

            if (_context.Summary.Count == 0)
                return;

            section.AddParagraph();

            Paragraph heading = section.AddParagraph();

            heading.AddText("SUMMARY");

            heading.Format.Font.Bold = true;

            heading.Format.Font.Size = 12;

            heading.Format.SpaceAfter = Unit.FromPoint(5);

            Table table = section.AddTable();

            table.Borders.Width = 0.25;

            table.AddColumn(Unit.FromCentimeter(8));

            table.AddColumn(Unit.FromCentimeter(4));

            foreach (ReportSummary item in _context.Summary)
            {
                Row row = table.AddRow();

                row.Cells[0].AddParagraph(item.Caption);

                Paragraph value =
                    row.Cells[1].AddParagraph(item.Value);

                value.Format.Alignment =
                    ParagraphAlignment.Right;

                if (item.Bold)
                {
                    row.Cells[0].Format.Font.Bold = true;
                    row.Cells[1].Format.Font.Bold = true;
                }
            }
        }
    }
}
