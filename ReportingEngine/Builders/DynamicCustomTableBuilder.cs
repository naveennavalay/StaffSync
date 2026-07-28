using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using ReportingEngine.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Builders
{
    /// <summary>
    /// Builds a completely dynamic table.
    /// </summary>
    public class DynamicCustomTableBuilder
    {
        public void Build(
            Section section,
            ReportDynamicTable model)
        {
            if (model == null)
                return;

            //----------------------------------------------------------
            // Space Before
            //----------------------------------------------------------

            if (model.SpaceBefore > 0)
            {
                Paragraph space = section.AddParagraph();
                space.Format.SpaceBefore = Unit.FromCentimeter(model.SpaceBefore);
            }

            //----------------------------------------------------------
            // Title
            //----------------------------------------------------------

            if (model.ShowTitle && !string.IsNullOrWhiteSpace(model.Title))
            {
                Paragraph title = section.AddParagraph();

                title.Format.Font.Size = 11;
                title.Format.Font.Bold = true;
                title.Format.SpaceAfter = Unit.FromPoint(5);

                title.AddText(model.Title);
            }

            //----------------------------------------------------------
            // Subtitle
            //----------------------------------------------------------

            if (model.ShowSubTitle &&
                !string.IsNullOrWhiteSpace(model.SubTitle))
            {
                Paragraph subtitle = section.AddParagraph();

                subtitle.Format.Font.Size = 9;
                subtitle.Format.Font.Italic = true;
                subtitle.Format.SpaceAfter = Unit.FromPoint(5);

                subtitle.AddText(model.SubTitle);
            }

            //----------------------------------------------------------
            // Table
            //----------------------------------------------------------

            Table table = section.AddTable();

            table.Rows.LeftIndent = 0;

            if (model.ShowBorders)
            {
                table.Borders.Width = model.BorderWidth;
                table.Borders.Color = model.BorderColor;
            }

            //----------------------------------------------------------
            // Visible Columns
            //----------------------------------------------------------

            var visibleColumns =
                model.Columns
                     .Where(x => x.Visible)
                     .OrderBy(x => x.DisplayOrder)
                     .ToList();

            foreach (ReportDynamicColumn column in visibleColumns)
            {
                Column pdfColumn =
                    table.AddColumn(Unit.FromCentimeter(column.Width));

                pdfColumn.Format.Alignment =
                    column.Alignment;
            }

            //----------------------------------------------------------
            // Header
            //----------------------------------------------------------

            if (model.ShowHeader)
            {
                Row header = table.AddRow();

                header.HeadingFormat = model.RepeatHeader;

                header.Shading.Color = model.HeaderBackColor;

                header.Format.Font.Color = model.HeaderForeColor;

                header.Format.Font.Bold = true;

                header.Format.Font.Size = 9;

                for (int i = 0; i < visibleColumns.Count; i++)
                {
                    Cell cell = header.Cells[i];

                    cell.AddParagraph(visibleColumns[i].Header);

                    cell.Format.Alignment =
                        visibleColumns[i].Alignment;

                    cell.VerticalAlignment =
                        VerticalAlignment.Center;
                }
            }

            //----------------------------------------------------------
            // Data Rows
            //----------------------------------------------------------

            bool alternate = false;

            foreach (ReportDynamicRow dynamicRow in model.Rows)
            {
                if (!dynamicRow.Visible)
                    continue;

                Row row = table.AddRow();

                if (dynamicRow.Height > 0)
                    row.Height =
                        Unit.FromCentimeter(dynamicRow.Height);

                if (dynamicRow.BackColor != null)
                {
                    row.Shading.Color =
                        dynamicRow.BackColor;
                }
                else if (alternate &&
                         model.AlternateRows)
                {
                    row.Shading.Color =
                        model.AlternateRowColor;
                }

                alternate = !alternate;

                row.Format.Font.Bold =
                    dynamicRow.Bold;

                row.Format.Font.Color =
                    dynamicRow.ForeColor;

                int pdfColumnIndex = 0;

                for (int sourceIndex = 0;
                     sourceIndex < model.Columns.Count;
                     sourceIndex++)
                {
                    ReportDynamicColumn column =
                        model.Columns[sourceIndex];

                    if (!column.Visible)
                        continue;

                    object value = null;

                    if (sourceIndex < dynamicRow.Cells.Count)
                        value = dynamicRow.Cells[sourceIndex];

                    Cell cell = row.Cells[pdfColumnIndex];

                    Paragraph p = cell.AddParagraph();

                    p.Format.Alignment = column.Alignment;

                    p.AddText(
                        FormatValue(
                            value,
                            column.Format));

                    pdfColumnIndex++;
                }
            }

            //----------------------------------------------------------
            // Space After
            //----------------------------------------------------------

            if (model.SpaceAfter > 0)
            {
                Paragraph space = section.AddParagraph();
                space.Format.SpaceAfter =
                    Unit.FromCentimeter(model.SpaceAfter);
            }
        }

        private string FormatValue(
            object value,
            string format)
        {
            if (value == null ||
                value == DBNull.Value)
                return "";

            if (string.IsNullOrWhiteSpace(format))
                return value.ToString();

            try
            {
                IFormattable formattable =
                    value as IFormattable;

                if (formattable != null)
                {
                    return formattable.ToString(
                        format,
                        CultureInfo.InvariantCulture);
                }
            }
            catch
            {
            }

            return value.ToString();
        }
    }
}
