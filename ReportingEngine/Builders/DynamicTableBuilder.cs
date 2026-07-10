using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using ReportingEngine.Enum;
using ReportingEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Builders
{
    /// <summary>
    /// Builds a dynamic table using ReportColumn metadata.
    /// Works with any object collection.
    /// </summary>
    public class DynamicTableBuilder
    {
        public void Build(Section section, IList<ReportColumn> columns, IEnumerable<object> data)
        {
            if (section == null)
                throw new ArgumentNullException(nameof(section));

            if (columns == null)
                throw new ArgumentNullException(nameof(columns));

            Table table = section.AddTable();

            table.Style = "Table";

            table.Borders.Width = 0.5;

            table.Rows.LeftIndent = 0;

            //---------------------------------------------------
            // Visible Columns
            //---------------------------------------------------

            List<ReportColumn> visibleColumns =
                columns
                .Where(c => c.Visible)
                .ToList();

            //---------------------------------------------------
            // Columns
            //---------------------------------------------------

            foreach (ReportColumn column in visibleColumns)
            {
                Column pdfColumn =
                    table.AddColumn(Unit.FromCentimeter(column.Width));

                pdfColumn.Format.Alignment =
                    ConvertAlignment(column.Alignment);
            }

            //---------------------------------------------------
            // Header
            //---------------------------------------------------

            Row header = table.AddRow();

            header.HeadingFormat = true;

            header.Shading.Color = Color.Parse("#0B1F8A");

            header.Format.Font.Bold = true;

            header.Format.Font.Color = Colors.White;

            header.VerticalAlignment = VerticalAlignment.Center;

            for (int i = 0; i < visibleColumns.Count; i++)
            {
                Paragraph p = header.Cells[i].AddParagraph();

                p.Format.Alignment =
                    ConvertAlignment(visibleColumns[i].Alignment);

                p.AddText(visibleColumns[i].Header);
            }

            //---------------------------------------------------
            // Data
            //---------------------------------------------------

            bool alternate = false;

            if (data != null)
            {
                foreach (object item in data)
                {
                    Row row = table.AddRow();

                    row.VerticalAlignment = VerticalAlignment.Center;

                    if (alternate)
                        row.Shading.Color = Color.Parse("#F7F9FC");

                    alternate = !alternate;

                    for (int i = 0; i < visibleColumns.Count; i++)
                    {
                        ReportColumn column = visibleColumns[i];

                        object value =
                            GetPropertyValue(item, column.FieldName);

                        Paragraph p =
                            row.Cells[i].AddParagraph();

                        p.Format.Alignment =
                            ConvertAlignment(column.Alignment);

                        if (value != null)
                            p.AddText(value.ToString());
                    }
                }
            }

            section.Add(table);
        }

        //-------------------------------------------------------
        // Reflection
        //-------------------------------------------------------

        private object GetPropertyValue(object instance, string propertyName)
        {
            if (instance == null)
                return null;

            PropertyInfo property =
                instance
                .GetType()
                .GetProperty(propertyName);

            if (property == null)
                return null;

            return property.GetValue(instance);
        }

        //-------------------------------------------------------
        // Alignment
        //-------------------------------------------------------

        private ParagraphAlignment ConvertAlignment(ReportAlignment alignment)
        {
            switch (alignment)
            {
                case ReportAlignment.Center:
                    return ParagraphAlignment.Center;

                case ReportAlignment.Right:
                    return ParagraphAlignment.Right;

                default:
                    return ParagraphAlignment.Left;
            }
        }
    }
}
