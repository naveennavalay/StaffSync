using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace StaffSync
{
    public class PDFTableGen
    {
        private string _filePath;
        private string _title;
        private Document _document;
        private Font _headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
        private Font _cellFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

        private string _companyName;
        private string _companyAddress;
        private string _phone;
        private string _email;
        private string _logoPath;

        public PDFTableGen(string filePath, string title = "Report")
        {
            _filePath = filePath;
            _title = title;
        }

        // 🆕 This method stores info table
        private Dictionary<string, string> _topInfoData = null;

        public void SetCompanyInfo(string companyName, string address, string phone, string email, string logoPath = null)
        {
            _companyName = companyName;
            _companyAddress = address;
            _phone = phone;
            _email = email;
            _logoPath = logoPath;
        }

        public void SetTopInfo(Dictionary<string, string> infoData)
        {
            _topInfoData = infoData;
        }

        public void CreatePdf(List<TableData> tables)
        {
            _document = new Document(PageSize.A4, 25, 25, 30, 30);

            PdfWriter writer = PdfWriter.GetInstance(_document, new FileStream(_filePath, FileMode.Create)); // ✅ declared here

            // Footer event
            string genTime = DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");
            var footerEvent = new PdfFooter { GeneratedText = genTime };
            writer.PageEvent = footerEvent;

            _document.Open();

            if (!string.IsNullOrEmpty(_companyName))
            {
                AddCompanyHeader(); // company + logo
                _document.Add(new Paragraph("\n"));
            }

            if (!string.IsNullOrEmpty(_title))
            {
                Paragraph titlePara = new Paragraph(_title, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16));
                titlePara.Alignment = Element.ALIGN_CENTER;
                _document.Add(titlePara);
                _document.Add(new Paragraph("\n"));
            }

            if (_topInfoData != null)
            {
                AddTopInfoTable(_topInfoData);
                _document.Add(new Paragraph("\n"));
            }

            foreach (var tableData in tables)
            {
                if (!string.IsNullOrWhiteSpace(tableData.Title))
                {
                    _document.Add(new Paragraph(tableData.Title, _headerFont));
                    _document.Add(new Paragraph("\n"));
                }

                PdfPTable table = new PdfPTable(tableData.Columns.Count);
                table.WidthPercentage = 100;

                foreach (var col in tableData.Columns)
                {
                    var headerCell = new PdfPCell(new Phrase(col, _headerFont))
                    {
                        BackgroundColor = BaseColor.LIGHT_GRAY
                    };
                    table.AddCell(headerCell);
                }

                foreach (var row in tableData.Rows)
                {
                    foreach (var col in tableData.Columns)
                    {
                        table.AddCell(new Phrase(row.ContainsKey(col) ? row[col]?.ToString() : "", _cellFont));
                    }
                }

                _document.Add(table);
                _document.Add(new Paragraph("\n"));
            }

            AddBottomNotice(writer); // ✅ pass writer here

            _document.Close();
        }


        private void AddCompanyHeader()
        {
            PdfPTable headerTable = new PdfPTable(2);
            headerTable.WidthPercentage = 100;
            headerTable.SetWidths(new float[] { 1f, 4f });

            // === Logo ===
            PdfPCell logoCell;
            if (!string.IsNullOrEmpty(_logoPath) && File.Exists(_logoPath))
            {
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(_logoPath);
                logo.ScaleToFit(130f, 130f);
                logoCell = new PdfPCell(logo);
            }
            else
            {
                logoCell = new PdfPCell(new Phrase(""));
            }

            logoCell.Border = Rectangle.NO_BORDER;
            logoCell.Rowspan = 4;
            logoCell.VerticalAlignment = Element.ALIGN_TOP;
            headerTable.AddCell(logoCell);

            // === Fonts ===
            Font nameFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
            Font infoFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

            // === Right-aligned company info ===
            PdfPCell nameCell = new PdfPCell(new Phrase(_companyName, nameFont));
            nameCell.Border = Rectangle.NO_BORDER;
            nameCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            headerTable.AddCell(nameCell);

            PdfPCell addrCell = new PdfPCell(new Phrase(_companyAddress, infoFont));
            addrCell.Border = Rectangle.NO_BORDER;
            addrCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            headerTable.AddCell(addrCell);

            string contactLine = $"Phone: {_phone}   |   Mail: {_email}";
            PdfPCell contactCell = new PdfPCell(new Phrase(contactLine, infoFont));
            contactCell.Border = Rectangle.NO_BORDER;
            contactCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            headerTable.AddCell(contactCell);

            PdfPCell empty = new PdfPCell(new Phrase(""));
            empty.Border = Rectangle.NO_BORDER;
            headerTable.AddCell(empty);

            _document.Add(headerTable);

            // === Horizontal line ===
            LineSeparator line = new LineSeparator(1f, 100f, BaseColor.GRAY, Element.ALIGN_CENTER, -2);
            _document.Add(new Chunk(line));
        }


        private void AddTopInfoTable(Dictionary<string, string> infoData)
        {
            PdfPTable infoTable = new PdfPTable(4);
            infoTable.WidthPercentage = 100;
            infoTable.SetWidths(new float[] { 1.5f, 2.5f, 1.5f, 2.5f });

            Font labelFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
            Font valueFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);

            var keys = new List<string>(infoData.Keys);
            for (int i = 0; i < keys.Count;)
            {
                // Left pair
                infoTable.AddCell(new Phrase(keys[i] + ":", labelFont));
                infoTable.AddCell(new Phrase(infoData[keys[i]], valueFont));
                i++;

                // Right pair
                if (i < keys.Count)
                {
                    infoTable.AddCell(new Phrase(keys[i] + ":", labelFont));
                    infoTable.AddCell(new Phrase(infoData[keys[i]], valueFont));
                    i++;
                }
                else
                {
                    // fill empty cells
                    infoTable.AddCell("");
                    infoTable.AddCell("");
                }
            }

            _document.Add(infoTable);
        }

        private void AddBottomNotice(PdfWriter writer)
        {
            PdfPTable box = new PdfPTable(1);
            box.TotalWidth = _document.PageSize.Width - _document.LeftMargin - _document.RightMargin;

            var noticeFont = FontFactory.GetFont(FontFactory.HELVETICA, 9, BaseColor.BLACK);
            var cell = new PdfPCell(new Phrase("System generated statement. Signature not needed.", noticeFont))
            {
                BackgroundColor = new BaseColor(230, 230, 230), // light gray
                Padding = 6f,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = Rectangle.NO_BORDER
            };

            box.AddCell(cell);

            // ✅ Use writer.DirectContent instead of document.DirectContent
            box.WriteSelectedRows(0, -1, _document.LeftMargin, _document.BottomMargin + 30, writer.DirectContent);
        }
    }

    public class TableData
    {
        public string Title { get; set; } = "";
        public List<string> Columns { get; set; } = new List<string>();
        public List<Dictionary<string, object>> Rows { get; set; } = new List<Dictionary<string, object>>();
    }

    public class PdfFooter : PdfPageEventHelper
    {
        public string GeneratedText { get; set; }

        private Font footerFont = FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 9, BaseColor.GRAY);

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            PdfPTable footer = new PdfPTable(1);
            footer.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;

            PdfPCell cell = new PdfPCell(new Phrase($"Generated on: {GeneratedText}", footerFont));
            cell.Border = Rectangle.NO_BORDER;
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.PaddingTop = 4;
            footer.AddCell(cell);

            footer.WriteSelectedRows(0, -1, document.LeftMargin, document.BottomMargin - 5, writer.DirectContent);
        }
    }
}
