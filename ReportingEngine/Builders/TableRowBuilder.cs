using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using ReportingEngine.Layout;
using ReportingEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Builders
{
    internal class TableRowBuilder
    {
        private readonly DynamicCustomTableBuilder _tableBuilder;

        public TableRowBuilder()
        {
            _tableBuilder = new DynamicCustomTableBuilder();
        }

        public void Build(Section section, ReportTableRow reportRow)
        {
            if (section == null)
                throw new ArgumentNullException(nameof(section));

            if (reportRow == null || reportRow.Tables == null || reportRow.Tables.Count == 0)
                return;

            if (reportRow.SpaceBefore > 0)
            {
                Paragraph p = section.AddParagraph();
                p.Format.SpaceBefore = Unit.FromCentimeter(reportRow.SpaceBefore);
            }

            int maxTablesPerRow = reportRow.MaxTablesPerRow <= 0 ? 3 : reportRow.MaxTablesPerRow;

            List<ReportDynamicTable> currentRow = new List<ReportDynamicTable>();

            foreach (ReportDynamicTable table in reportRow.Tables)
            {
                currentRow.Add(table);

                if (currentRow.Count == maxTablesPerRow)
                {
                    RenderLayoutRow(section, currentRow, reportRow.TableSpacing);

                    currentRow.Clear();

                    section.AddParagraph();
                }
            }

            if (currentRow.Count > 0)
            {
                RenderLayoutRow(
                    section,
                    currentRow,
                    reportRow.TableSpacing);
            }

            if (reportRow.SpaceAfter > 0)
            {
                Paragraph p = section.AddParagraph();
                p.Format.SpaceAfter = Unit.FromCentimeter(reportRow.SpaceAfter);
            }
        }

        private void RenderLayoutRow(Section section, List<ReportDynamicTable> tables, double spacing)
        {
            Table layoutTable = section.AddTable();

            layoutTable.Borders.Visible = false;
            layoutTable.Rows.LeftIndent = 0;
            layoutTable.LeftPadding = 0;
            layoutTable.RightPadding = 0;

            for (int i = 0; i < tables.Count; i++)
            {
                double width = tables[i].GetTableWidth();

                if (i < tables.Count - 1)
                    width += spacing;

                layoutTable.AddColumn(Unit.FromCentimeter(width));
            }

            Row layoutRow = layoutTable.AddRow();

            for (int i = 0; i < tables.Count; i++)
            {
                Cell cell = layoutRow.Cells[i];

                //cell.LeftPadding = 0;
                //cell.RightPadding = 0;

                _tableBuilder.Build(cell, tables[i]);
            }
        }
    }
}
