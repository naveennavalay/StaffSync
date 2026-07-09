using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using ReportingEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Builders
{
    /// <summary>
    /// Central document builder for all reports.
    /// </summary>
    public class ReportingDocumentBuilder
    {
        public Document Document { get; }
        public Section Section { get; }

        public ReportContext Context { get; }
        public ReportSettings Settings { get; }

        public ReportingDocumentBuilder(
            ReportContext context,
            ReportSettings settings)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Settings = settings ?? throw new ArgumentNullException(nameof(settings));

            Document = new Document();
            Section = Document.AddSection();

            ApplyPageSettings();
        }

        private void ApplyPageSettings()
        {
            PageBuilder builder = new PageBuilder();
            builder.Apply(Section, Settings);
        }

        public Paragraph AddHeading(string text)
        {
            Paragraph p = Section.AddParagraph(text);
            p.Format.Font.Bold = true;
            p.Format.Font.Size = 16;
            p.Format.SpaceAfter = Unit.FromCentimeter(0.30);
            return p;
        }

        public Paragraph AddText(string text)
        {
            Paragraph p = Section.AddParagraph(text);
            p.Format.Font.Size = 10;
            return p;
        }

        public Table CreateTable(int columns, double columnWidthCm = 3.0)
        {
            Table table = Section.AddTable();
            table.Borders.Width = 0.5;

            for (int i = 0; i < columns; i++)
                table.AddColumn(Unit.FromCentimeter(columnWidthCm));

            return table;
        }

        public Row AddHeaderRow(Table table, params string[] headers)
        {
            Row row = table.AddRow();
            row.HeadingFormat = true;
            row.Format.Font.Bold = true;

            for (int i = 0; i < headers.Length && i < row.Cells.Count; i++)
                row.Cells[i].AddParagraph(headers[i]);

            return row;
        }

        public Row AddRow(Table table, params string[] values)
        {
            Row row = table.AddRow();

            for (int i = 0; i < values.Length && i < row.Cells.Count; i++)
                row.Cells[i].AddParagraph(values[i]);

            return row;
        }

        public Document Build()
        {
            return Document;
        }
    }
}
