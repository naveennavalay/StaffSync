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

                        // Convert value according to column format
                        string text = FormatValue(value, column);

                        Paragraph p = row.Cells[i].AddParagraph(text);

                        ApplyAlignment(p, column);

                        ApplyFont(p, column);
                    }
                }
            }
            //section.Add(table);
        }

        private void ApplyAlignment(Paragraph p, ReportColumn column)
        {
            p.Format.Alignment = ConvertAlignment(column.Alignment);
        }

        private void ApplyFont(Paragraph p, ReportColumn column)
        {
            if (column.Bold)
                p.Format.Font.Bold = true;

            if (column.FontSize > 0)
                p.Format.Font.Size = column.FontSize;
        }

        private string FormatValue(object value, ReportColumn column)
        {
            if (value == null)
                return "";

            if (string.IsNullOrWhiteSpace(column.Format))
                return value.ToString();

            switch (column.Format)
            {
                case "Date":
                    if (value is DateTime dt)
                        return dt.ToString("dd-MMM-yyyy");
                    break;

                case "Currency":
                    if (decimal.TryParse(value.ToString(), out decimal amount))
                        return amount.ToString("#,##0.00");
                    break;

                case "Integer":
                    if (int.TryParse(value.ToString(), out int i))
                        return i.ToString("N0");
                    break;

                case "Decimal":
                    if (decimal.TryParse(value.ToString(), out decimal d))
                        return d.ToString("#,##0.00");
                    break;
            }

            return value.ToString();
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
