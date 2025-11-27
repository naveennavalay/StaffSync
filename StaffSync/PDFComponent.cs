using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Drawing;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace StaffSync
{
    public class PDFComponent
    {
        public enum PdfPageSize
        {
            A4,
            Letter,
            Custom
        }

        public enum PageOrientation
        {
            Portrait,
            Landscape
        }

        public enum HorizontalAlign
        {
            Left = Element.ALIGN_LEFT,
            Center = Element.ALIGN_CENTER,
            Right = Element.ALIGN_RIGHT
        }

        public class PdfTextBlock
        {
            public string Text { get; set; }
            public float FontSize { get; set; } = 12;
            public bool Bold { get; set; } = false;
            public HorizontalAlign Alignment { get; set; } = HorizontalAlign.Left;
            public float SpacingBefore { get; set; } = 8;
            public float SpacingAfter { get; set; } = 6;
        }

        public class PdfMargins
        {
            public float Left { get; set; }
            public float Right { get; set; }
            public float Top { get; set; }
            public float Bottom { get; set; }

            public PdfMargins() { }

            public PdfMargins(float all)
            {
                Left = Right = Top = Bottom = all;
            }

            public PdfMargins(float left, float right, float top, float bottom)
            {
                Left = left;
                Right = right;
                Top = top;
                Bottom = bottom;
            }

            public static PdfMargins Default() => new PdfMargins(36, 36, 54, 54);
        }

        public class PdfHeaderFooter
        {
            public HorizontalAlign Alignment { get; set; } = HorizontalAlign.Left;
            public Dictionary<string, string> Items { get; } = new Dictionary<string, string>();
            public string Separator { get; set; } = " | ";

            public void AddItem(string key, string value)
            {
                if (!string.IsNullOrWhiteSpace(key))
                    Items[key] = value ?? "";
            }
        }

        public class PdfColumnDefinition
        {
            public string ColumnName { get; set; }
            public string HeaderText { get; set; }
            public float? Width { get; set; }

            public bool HeaderBold { get; set; } = true;
            public bool DataBold { get; set; } = false;

            public BaseColor HeaderBackColor { get; set; } = BaseColor.LIGHT_GRAY;
            public BaseColor HeaderTextColor { get; set; } = BaseColor.BLACK;
            public BaseColor DataTextColor { get; set; } = BaseColor.BLACK;

            public string FormatString { get; set; }
            public HorizontalAlign Alignment { get; set; } = HorizontalAlign.Left;
        }

        public class PdfTableDefinition
        {
            public bool Border { get; set; } = true;
            public float BorderWidth { get; set; } = 0.5f;
            public bool AutoWidth { get; set; } = true;
            public bool RepeatHeaderOnEachPage { get; set; } = true;

            public bool UseEqualColumnWidth { get; set; } = false;

            public bool UseAlternateRowBackground { get; set; } = false;
            public BaseColor AlternateRowColor { get; set; } = new BaseColor(240, 240, 240);

            public List<PdfColumnDefinition> Columns { get; } = new List<PdfColumnDefinition>();
            public List<IList<object>> Rows { get; } = new List<IList<object>>();

            // Custom table (like header with image)
            public PdfPTable CustomTable { get; set; } = null;

            public void AddColumn(
                string name,
                string header,
                float? width = null,
                bool headerBold = true,
                bool dataBold = false,
                BaseColor headerBack = null,
                BaseColor headerText = null,
                BaseColor dataText = null,
                string format = null,
                HorizontalAlign align = HorizontalAlign.Left)
            {
                Columns.Add(new PdfColumnDefinition
                {
                    ColumnName = name,
                    HeaderText = header,
                    Width = width,
                    HeaderBold = headerBold,
                    DataBold = dataBold,
                    HeaderBackColor = headerBack ?? BaseColor.LIGHT_GRAY,
                    HeaderTextColor = headerText ?? BaseColor.BLACK,
                    DataTextColor = dataText ?? BaseColor.BLACK,
                    FormatString = format,
                    Alignment = align
                });
            }

            public void AddRow(params object[] values)
            {
                Rows.Add(values);
            }
        }

        public class PdfContentBuilder
        {
            internal List<PdfTextBlock> TextBlocks { get; } = new List<PdfTextBlock>();
            internal PdfHeaderFooter Header { get; private set; }
            internal PdfHeaderFooter Footer { get; private set; }
            internal List<PdfFlowElement> Flow { get; } = new List<PdfFlowElement>();

            internal string FontName { get; }
            internal float FontSize { get; }
            internal class PdfFlowElement
            {
                public PdfTextBlock Text { get; set; }
                public PdfTableDefinition Table { get; set; }
            }

            public PdfContentBuilder(string fontName, float fontSize)
            {
                FontName = fontName;
                FontSize = fontSize;
            }

            public void PageHeader(Action<PdfHeaderFooter> config)
            {
                Header = new PdfHeaderFooter();
                config?.Invoke(Header);
            }

            public void PageFooter(Action<PdfHeaderFooter> config)
            {
                Footer = new PdfHeaderFooter();
                config?.Invoke(Footer);
            }

            public void TableInfo(Action<PdfTableDefinition> config)
            {
                var tbl = new PdfTableDefinition();
                config(tbl);
                Flow.Add(new PdfFlowElement { Table = tbl });
            }

            public void AddText(string text, float size = 12, bool bold = false, HorizontalAlign align = HorizontalAlign.Left)
            {
                Flow.Add(new PdfFlowElement
                {
                    Text = new PdfTextBlock
                    {
                        Text = text,
                        FontSize = size,
                        Bold = bold,
                        Alignment = align
                    }
                });
            }
        }

        public static class SimplePdfGenerator
        {
            #region PUBLIC API

            public static void CreatePdf(
                string filePath,
                PdfPageSize pageSize,
                PageOrientation orientation,
                string fontName,
                float fontSize,
                PdfMargins margins,
                float? customW,
                float? customH,
                Action<PdfContentBuilder> content)
            {
                var builder = new PdfContentBuilder(fontName, fontSize);
                content(builder);

                Rectangle pageRect = GetPage(pageSize, orientation, customW, customH);

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                using (Document doc = new Document(pageRect, margins.Left, margins.Right, margins.Top, margins.Bottom))
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, fs);
                    doc.Open();

                    Font baseFont = FontFactory.GetFont(fontName, fontSize, BaseColor.BLACK);

                    // HEADER
                    if (builder.Header != null)
                    {
                        doc.Add(BuildHeaderFooter(builder.Header, baseFont));
                        doc.Add(Chunk.NEWLINE);
                    }

                    // TABLES
                    foreach (var element in builder.Flow)
                    {
                        // TEXT BLOCK
                        if (element.Text != null)
                        {
                            Font f = FontFactory.GetFont(builder.FontName, element.Text.FontSize, element.Text.Bold ? Font.BOLD : Font.NORMAL, BaseColor.BLACK);

                            Paragraph p = new Paragraph(element.Text.Text, f)
                            {
                                Alignment = (int)element.Text.Alignment,
                                SpacingBefore = element.Text.SpacingBefore,
                                SpacingAfter = element.Text.SpacingAfter
                            };

                            doc.Add(p);
                            continue;
                        }

                        // TABLE BLOCK
                        if (element.Table != null)
                        {
                            PdfPTable tbl = BuildTable(element.Table, baseFont);
                            doc.Add(tbl);
                            doc.Add(Chunk.NEWLINE);
                        }
                    }

                    // FOOTER
                    if (builder.Footer != null)
                    {
                        doc.Add(Chunk.NEWLINE);
                        doc.Add(BuildHeaderFooter(builder.Footer, baseFont));
                    }

                    doc.Close();
                }
            }

            public static void CreatePdf(
                string filePath,
                PdfPageSize pageSize,
                PageOrientation orientation,
                string fontName,
                float fontSize,
                PdfMargins margins,
                Action<PdfContentBuilder> content)
            {
                CreatePdf(filePath, pageSize, orientation, fontName, fontSize, margins, null, null, content);
            }

            #endregion


            #region HELPERS

            private static Rectangle GetPage(PdfPageSize size, PageOrientation orient, float? w, float? h)
            {
                Rectangle rec;

                switch (size)
                {
                    case PdfPageSize.A4:
                        rec = PageSize.A4;
                        break;

                    case PdfPageSize.Letter:
                        rec = PageSize.LETTER;
                        break;

                    case PdfPageSize.Custom:
                        rec = new Rectangle(w ?? 595, h ?? 842);
                        break;

                    default:
                        rec = PageSize.A4;
                        break;
                }

                if (orient == PageOrientation.Landscape)
                    rec = rec.Rotate();

                return rec;
            }

            private static Paragraph BuildHeaderFooter(PdfHeaderFooter hf, Font font)
            {
                Paragraph p = new Paragraph
                {
                    Alignment = (int)hf.Alignment,
                    SpacingBefore = 5,
                    SpacingAfter = 5
                };

                bool first = true;
                foreach (var kv in hf.Items)
                {
                    if (!first)
                        p.Add(new Chunk(hf.Separator, font));

                    p.Add(new Chunk($"{kv.Key}: {kv.Value}", font));
                    first = false;
                }

                return p;
            }

            private static PdfPTable BuildTable(PdfTableDefinition def, Font baseFont)
            {
                // If a custom table is provided → return it directly
                if (def.CustomTable != null)
                    return def.CustomTable;

                // Normal PDF table
                PdfPTable table = new PdfPTable(def.Columns.Count);
                table.WidthPercentage = 100;

                if (def.UseEqualColumnWidth)
                {
                    float[] widths = new float[def.Columns.Count];
                    for (int i = 0; i < def.Columns.Count; i++)
                        widths[i] = 1f;   // equal width weight

                    table.SetWidths(widths);
                }

                // Widths
                if (!def.AutoWidth)
                {
                    float[] widths = new float[def.Columns.Count];
                    for (int i = 0; i < def.Columns.Count; i++)
                        widths[i] = def.Columns[i].Width ?? 1f;

                    table.SetTotalWidth(widths);
                    table.LockedWidth = true;
                }

                // HEADER ROW
                foreach (var col in def.Columns)
                {
                    Font hf = new Font(baseFont);
                    if (col.HeaderBold) hf.SetStyle(Font.BOLD);
                    hf.Color = col.HeaderTextColor;

                    PdfPCell cell = new PdfPCell(new Phrase(col.HeaderText, hf))
                    {
                        BackgroundColor = col.HeaderBackColor,
                        HorizontalAlignment = (int)col.Alignment,
                        BorderWidth = def.Border ? def.BorderWidth : 0
                    };

                    table.AddCell(cell);
                }
                table.HeaderRows = 1;

                // DATA ROWS
                bool alt = false;
                foreach (var row in def.Rows)
                {
                    for (int i = 0; i < def.Columns.Count; i++)
                    {
                        var col = def.Columns[i];
                        object value = (i < row.Count) ? row[i] : null;

                        string text;
                        if (value == null) text = "";
                        else if (!string.IsNullOrWhiteSpace(col.FormatString) && value is IFormattable f)
                            text = f.ToString(col.FormatString, null);
                        else text = value.ToString();

                        Font df = new Font(baseFont);
                        if (col.DataBold) df.SetStyle(Font.BOLD);
                        df.Color = col.DataTextColor;

                        PdfPCell dcell = new PdfPCell(new Phrase(text, df))
                        {
                            HorizontalAlignment = (int)col.Alignment,
                            BorderWidth = def.Border ? def.BorderWidth : 0
                        };

                        if (def.UseAlternateRowBackground && alt)
                            dcell.BackgroundColor = def.AlternateRowColor;

                        table.AddCell(dcell);
                    }

                    alt = !alt;
                }

                return table;
            }

            #endregion


            #region CUSTOM: IMAGE + KEY VALUE INFO TABLE

            public static PdfPTable BuildInfoWithImage(
                string imagePath,
                float imageSize,
                Dictionary<string, string> items,
                Font font)
            {
                // 2 columns: Left = Details, Right = Photo
                PdfPTable main = new PdfPTable(2);
                main.WidthPercentage = 100;
                main.SetWidths(new float[] { 3f, 1.2f });  // Text wider than image

                // -----------------------------
                // LEFT SIDE — TEXT ROWS
                // -----------------------------
                PdfPTable textTable = new PdfPTable(1);
                textTable.WidthPercentage = 100;

                foreach (var kv in items)
                {
                    PdfPCell c = new PdfPCell(new Phrase($"{kv.Key}: {kv.Value}", font))
                    {
                        Border = Rectangle.NO_BORDER,
                        PaddingBottom = 4
                    };
                    textTable.AddCell(c);
                }

                PdfPCell textCell = new PdfPCell(textTable)
                {
                    Border = Rectangle.NO_BORDER
                };
                main.AddCell(textCell);

                // -----------------------------
                // RIGHT SIDE — PHOTO
                // -----------------------------
                PdfPCell imgCell;

                if (!string.IsNullOrWhiteSpace(imagePath) && File.Exists(imagePath))
                {
                    Image img = Image.GetInstance(imagePath);
                    img.ScaleToFit(imageSize, imageSize);
                    img.Alignment = Element.ALIGN_MIDDLE;

                    imgCell = new PdfPCell(img);
                }
                else
                {
                    imgCell = new PdfPCell(new Phrase("PHOTO", font))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE
                    };
                }

                imgCell.Border = Rectangle.NO_BORDER;
                imgCell.Rowspan = items.Count;     // Full height of text rows
                imgCell.PaddingLeft = 10;

                main.AddCell(imgCell);

                return main;
            }

            #endregion
        }
    }
}
