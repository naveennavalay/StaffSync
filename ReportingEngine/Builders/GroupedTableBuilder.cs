using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using ReportingEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ReportingEngine.Builders;

namespace ReportingEngine.Builders
{
    public class GroupedTableBuilder
    {
        private readonly DocumentContext _context;

        public GroupedTableBuilder(DocumentContext context)
        {
            _context = context;
        }

        public void Build(Section section)
        {
            //------------------------------------------------------
            // Get Property
            //------------------------------------------------------

            PropertyInfo property =
                _context.Data.First()
                             .GetType()
                             .GetProperty(_context.GroupByProperty);

            //------------------------------------------------------
            // Group Data
            //------------------------------------------------------

            var groups =
                _context.Data
                        .GroupBy(x => property.GetValue(x))
                        .OrderBy(x => x.Key);

            //------------------------------------------------------
            // Process each group
            //------------------------------------------------------

            foreach (var group in groups)
            {
                DrawGroupHeader(section, group);

                new DynamicTableBuilder()
                    .Build(section,
                           _context.Columns,
                           group.Cast<object>().ToList());

                DrawGroupSummary(section, group);
            }

            DrawGrandSummary(section);
        }

        private void DrawGroupHeader(Section section, IGrouping<object, object> group)
        {
            //----------------------------------------------------
            // Space before every group
            //----------------------------------------------------

            section.AddParagraph();

            //----------------------------------------------------
            // Header Table
            //----------------------------------------------------

            Table table = section.AddTable();

            table.Rows.LeftIndent = 0;
            table.Borders.Width = 0;

            table.AddColumn(Unit.FromCentimeter(18));
            table.AddColumn(Unit.FromCentimeter(6));

            Row row = table.AddRow();

            row.Height = Unit.FromCentimeter(0.7);

            row.Shading.Color = Color.Parse("#0B1F8A");

            row.VerticalAlignment = VerticalAlignment.Center;

            //----------------------------------------------------
            // Left Cell
            //----------------------------------------------------

            Paragraph pLeft = row.Cells[0].AddParagraph();

            pLeft.Format.Font.Bold = true;
            pLeft.Format.Font.Size = 10;
            pLeft.Format.Font.Color = Colors.White;

            pLeft.AddText($"   {_context.GroupByProperty.Replace("Title", "")} : {group.Key}");
            //pLeft.Format.LeftIndent = Unit.FromPoint(5);

            //----------------------------------------------------
            // Right Cell
            //----------------------------------------------------

            Paragraph pRight = row.Cells[1].AddParagraph();

            pRight.Format.Alignment = ParagraphAlignment.Right;
            pRight.Format.Font.Bold = true;
            pRight.Format.Font.Size = 10;
            pRight.Format.Font.Color = Colors.White;

            pRight.AddText($"Total Employees : {group.Count():00}");

            //----------------------------------------------------
            // Space after
            //----------------------------------------------------

            Paragraph separator = section.AddParagraph();

            separator.Format.SpaceAfter = Unit.FromPoint(3);

            separator.Format.Borders.Bottom.Width = 0.3;

            separator.Format.Borders.Bottom.Color = Colors.Gray;

            section.AddParagraph();
        }

        private void DrawGroupSummary(Section section, IGrouping<object, object> group)
        {
            // TODO
        }

        private void DrawGrandSummary(Section section)
        {
            // TODO
        }
    }
}
