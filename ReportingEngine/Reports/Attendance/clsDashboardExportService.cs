using ClosedXML.Excel;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using Word = DocumentFormat.OpenXml.Wordprocessing;

namespace StaffSync.ReportingEngine.Reports.Attendance
{
    /// <summary>
    /// Common dashboard export service.
    ///
    /// Supported formats:
    /// PDF
    /// DOCX
    /// XLSX
    /// CSV
    /// XML
    /// JSON
    ///
    /// PDF is generated directly without PDFsharp/MigraDoc.
    /// This avoids any dependency on the PDF rendering engine
    /// for dashboard exports.
    /// </summary>
    public sealed class clsDashboardExportService
    {
        #region Public Entry Point

        public string Export(
            clsDashboardExportRequest request,
            string outputFilePath)
        {
            if (request == null)
            {
                throw new ArgumentNullException(
                    nameof(request));
            }

            if (string.IsNullOrWhiteSpace(
                    outputFilePath))
            {
                throw new ArgumentException(
                    "Output file path is required.",
                    nameof(outputFilePath));
            }

            string format =
                NormalizeFormat(
                    request.Format);

            string directory =
                Path.GetDirectoryName(
                    outputFilePath);

            if (!string.IsNullOrWhiteSpace(directory) &&
                !Directory.Exists(directory))
            {
                Directory.CreateDirectory(
                    directory);
            }

            switch (format)
            {
                case "PDF":
                    ExportPdf(
                        request,
                        outputFilePath);
                    break;

                case "DOCX":
                    ExportDocx(
                        request,
                        outputFilePath);
                    break;

                case "XLSX":
                    ExportXlsx(
                        request,
                        outputFilePath);
                    break;

                case "CSV":
                    ExportCsv(
                        request,
                        outputFilePath);
                    break;

                case "XML":
                    ExportXml(
                        request,
                        outputFilePath);
                    break;

                case "JSON":
                    ExportJson(
                        request,
                        outputFilePath);
                    break;

                default:
                    throw new NotSupportedException(
                        "Unsupported export format: " +
                        request.Format);
            }

            return outputFilePath;
        }

        #endregion


        #region Format Normalization

        private string NormalizeFormat(
            string format)
        {
            if (string.IsNullOrWhiteSpace(
                    format))
            {
                throw new ArgumentException(
                    "Export format is required.");
            }

            return format
                .Trim()
                .TrimStart('.')
                .ToUpperInvariant();
        }

        #endregion


        #region PDF

        /// <summary>
        /// Creates a basic valid PDF directly.
        ///
        /// No PDFsharp.
        /// No MigraDoc.
        /// No chart/image rendering.
        ///
        /// The PDF contains:
        /// - Report title
        /// - Generated date
        /// - Column headers
        /// - Dashboard rows
        /// </summary>
        private void ExportPdf(
            clsDashboardExportRequest request,
            string outputFilePath)
        {
            List<string> columns =
                GetColumns(
                    request.Rows);

            List<string> lines =
                new List<string>();


            /*
             * --------------------------------------------------------
             * Title
             * --------------------------------------------------------
             */

            string title =
                string.IsNullOrWhiteSpace(
                    request.CardTitle)
                    ? "StaffSync Dashboard Export"
                    : request.CardTitle;

            lines.Add(
                title);


            /*
             * --------------------------------------------------------
             * Generated date
             * --------------------------------------------------------
             */

            DateTime generatedAt =
                request.GeneratedAt ??
                DateTime.Now;

            lines.Add(
                "Generated: " +
                generatedAt.ToString(
                    "dd-MMM-yyyy HH:mm:ss",
                    CultureInfo.InvariantCulture));


            /*
             * --------------------------------------------------------
             * Separator
             * --------------------------------------------------------
             */

            lines.Add(
                "------------------------------------------------------------");


            /*
             * --------------------------------------------------------
             * Data
             * --------------------------------------------------------
             */

            if (columns.Count == 0)
            {
                lines.Add(
                    "No data available.");
            }
            else
            {
                /*
                 * Header
                 */

                lines.Add(
                    string.Join(
                        " | ",
                        columns.Select(
                            FormatColumnName)));


                lines.Add(
                    "------------------------------------------------------------");


                /*
                 * Rows
                 */

                foreach (
                    Dictionary<string, object> row
                    in request.Rows ??
                    new List<Dictionary<string, object>>())
                {
                    List<string> values =
                        new List<string>();

                    foreach (
                        string column
                        in columns)
                    {
                        object value =
                            null;

                        if (row != null)
                        {
                            row.TryGetValue(
                                column,
                                out value);
                        }

                        values.Add(
                            FormatPdfText(
                                FormatValue(
                                    value)));
                    }

                    lines.Add(
                        string.Join(
                            " | ",
                            values));
                }
            }


            /*
             * --------------------------------------------------------
             * Create PDF.
             * --------------------------------------------------------
             */

            byte[] pdfBytes =
                BuildSimplePdf(
                    lines);

            File.WriteAllBytes(
                outputFilePath,
                pdfBytes);
        }


        /// <summary>
        /// Builds a minimal PDF using standard PDF syntax.
        /// </summary>
        private byte[] BuildSimplePdf(
            List<string> lines)
        {
            if (lines == null)
            {
                lines =
                    new List<string>();
            }


            /*
             * ------------------------------------------------------------
             * PDF page settings
             * ------------------------------------------------------------
             */

            const double pageWidth = 595.0;
            const double pageHeight = 842.0;

            const double leftMargin = 40.0;
            const double topPosition = 800.0;

            const double lineHeight = 14.0;

            const int fontSize = 8;


            /*
             * ------------------------------------------------------------
             * Split content into pages
             * ------------------------------------------------------------
             */

            List<List<string>> pages =
                new List<List<string>>();

            List<string> currentPage =
                new List<string>();

            double currentY =
                topPosition;


            foreach (
                string originalLine
                in lines)
            {
                List<string> wrappedLines =
                    WrapPdfText(
                        originalLine,
                        110);

                foreach (
                    string line
                    in wrappedLines)
                {
                    if (currentY < 50)
                    {
                        pages.Add(
                            currentPage);

                        currentPage =
                            new List<string>();

                        currentY =
                            topPosition;
                    }

                    currentPage.Add(
                        line);

                    currentY -=
                        lineHeight;
                }
            }


            if (currentPage.Count > 0)
            {
                pages.Add(
                    currentPage);
            }


            if (pages.Count == 0)
            {
                pages.Add(
                    new List<string>
                    {
                "No data available."
                    });
            }


            /*
             * ------------------------------------------------------------
             * PDF objects
             *
             * IMPORTANT:
             *
             * Index 0 is deliberately kept empty because PDF object
             * numbering starts from 1.
             *
             * objects[1] = Catalog
             * objects[2] = Pages
             * objects[3+] = Page / Content / Font objects
             * ------------------------------------------------------------
             */

            List<string> objects =
                new List<string>();


            /*
             * Object 0
             *
             * Required free object.
             */

            objects.Add(
                "");


            /*
             * Object 1
             *
             * Catalog.
             */

            objects.Add(
                "<< /Type /Catalog " +
                "/Pages 2 0 R >>");


            /*
             * Object 2 will be populated after page
             * object numbers are known.
             */

            objects.Add(
                "");


            int pagesObjectNumber =
                2;

            int nextObjectNumber =
                3;


            List<int> pageObjectNumbers =
                new List<int>();

            List<int> contentObjectNumbers =
                new List<int>();


            /*
             * ------------------------------------------------------------
             * Reserve page/content object numbers.
             * ------------------------------------------------------------
             */

            for (
                int pageIndex = 0;
                pageIndex < pages.Count;
                pageIndex++)
            {
                pageObjectNumbers.Add(
                    nextObjectNumber++);

                contentObjectNumbers.Add(
                    nextObjectNumber++);
            }


            /*
             * Font object.
             */

            int fontObjectNumber =
                nextObjectNumber++;


            /*
             * ------------------------------------------------------------
             * Object 2 - Pages
             * ------------------------------------------------------------
             */

            string kids =
                string.Join(
                    " ",
                    pageObjectNumbers.Select(
                        number =>
                            number +
                            " 0 R"));


            objects[pagesObjectNumber] =
                "<< /Type /Pages " +
                "/Kids [" +
                kids +
                "] " +
                "/Count " +
                pages.Count +
                " >>";


            /*
             * ------------------------------------------------------------
             * Page and Content objects
             * ------------------------------------------------------------
             */

            for (
                int pageIndex = 0;
                pageIndex < pages.Count;
                pageIndex++)
            {
                int pageObjectNumber =
                    pageObjectNumbers[
                        pageIndex];

                int contentObjectNumber =
                    contentObjectNumbers[
                        pageIndex];


                /*
                 * --------------------------------------------------------
                 * Page object
                 * --------------------------------------------------------
                 */

                string pageObject =
                    "<< /Type /Page " +
                    "/Parent " +
                    pagesObjectNumber +
                    " 0 R " +
                    "/MediaBox [0 0 " +
                    pageWidth.ToString(
                        CultureInfo.InvariantCulture) +
                    " " +
                    pageHeight.ToString(
                        CultureInfo.InvariantCulture) +
                    "] " +
                    "/Resources << " +
                    "/Font << /F1 " +
                    fontObjectNumber +
                    " 0 R >> " +
                    ">> " +
                    "/Contents " +
                    contentObjectNumber +
                    " 0 R >>";


                AddObjectAtNumber(
                    objects,
                    pageObjectNumber,
                    pageObject);


                /*
                 * --------------------------------------------------------
                 * Content stream
                 * --------------------------------------------------------
                 */

                StringBuilder content =
                    new StringBuilder();


                content.AppendLine(
                    "BT");


                content.AppendLine(
                    "/F1 " +
                    fontSize +
                    " Tf");


                content.AppendLine(
                    "1 0 0 1 " +
                    leftMargin.ToString(
                        CultureInfo.InvariantCulture) +
                    " " +
                    topPosition.ToString(
                        CultureInfo.InvariantCulture) +
                    " Tm");


                foreach (
                    string line
                    in pages[pageIndex])
                {
                    string escaped =
                        EscapePdfText(
                            line);


                    content.Append(
                        "(");


                    content.Append(
                        escaped);


                    content.AppendLine(
                        ") Tj");


                    content.AppendLine(
                        "0 -" +
                        lineHeight.ToString(
                            CultureInfo.InvariantCulture) +
                        " Td");
                }


                content.AppendLine(
                    "ET");


                string contentText =
                    content.ToString();


                int contentLength =
                    Encoding.ASCII.GetByteCount(
                        contentText);


                string contentObject =
                    "<< /Length " +
                    contentLength +
                    " >>\n" +
                    "stream\n" +
                    contentText +
                    "endstream";


                AddObjectAtNumber(
                    objects,
                    contentObjectNumber,
                    contentObject);
            }


            /*
             * ------------------------------------------------------------
             * Font object
             * ------------------------------------------------------------
             */

            AddObjectAtNumber(
                objects,
                fontObjectNumber,
                "<< /Type /Font " +
                "/Subtype /Type1 " +
                "/BaseFont /Helvetica >>");


            /*
             * ------------------------------------------------------------
             * Build final PDF
             * ------------------------------------------------------------
             */

            using (
                MemoryStream stream =
                new MemoryStream())
            {
                /*
                 * PDF header
                 */

                byte[] header =
                    Encoding.ASCII.GetBytes(
                        "%PDF-1.4\n");


                stream.Write(
                    header,
                    0,
                    header.Length);


                /*
                 * --------------------------------------------------------
                 * Object offsets
                 *
                 * offsets[0] corresponds to object 0.
                 * --------------------------------------------------------
                 */

                List<long> offsets =
                    new List<long>();


                for (
                    int i = 0;
                    i < objects.Count;
                    i++)
                {
                    if (i == 0)
                    {
                        /*
                         * Object 0 is the special free object.
                         */

                        offsets.Add(
                            0);

                        continue;
                    }


                    offsets.Add(
                        stream.Position);


                    string objectText =
                        i +
                        " 0 obj\n" +
                        objects[i] +
                        "\nendobj\n";


                    byte[] objectBytes =
                        Encoding.ASCII.GetBytes(
                            objectText);


                    stream.Write(
                        objectBytes,
                        0,
                        objectBytes.Length);
                }


                /*
                 * --------------------------------------------------------
                 * Cross-reference table
                 * --------------------------------------------------------
                 */

                long xrefPosition =
                    stream.Position;


                StringBuilder xref =
                    new StringBuilder();


                xref.AppendLine(
                    "xref");


                xref.AppendLine(
                    "0 " +
                    objects.Count);


                /*
                 * Object 0.
                 */

                xref.AppendLine(
                    "0000000000 65535 f ");


                /*
                 * Objects 1 onwards.
                 */

                for (
                    int i = 1;
                    i < objects.Count;
                    i++)
                {
                    xref.AppendLine(
                        offsets[i].ToString(
                            "D10",
                            CultureInfo.InvariantCulture) +
                        " 00000 n ");
                }


                byte[] xrefBytes =
                    Encoding.ASCII.GetBytes(
                        xref.ToString());


                stream.Write(
                    xrefBytes,
                    0,
                    xrefBytes.Length);


                /*
                 * --------------------------------------------------------
                 * Trailer
                 * --------------------------------------------------------
                 */

                string trailer =
                    "trailer\n" +
                    "<< /Size " +
                    objects.Count +
                    " /Root 1 0 R >>\n" +
                    "startxref\n" +
                    xrefPosition.ToString(
                        CultureInfo.InvariantCulture) +
                    "\n" +
                    "%%EOF\n";


                byte[] trailerBytes =
                    Encoding.ASCII.GetBytes(
                        trailer);


                stream.Write(
                    trailerBytes,
                    0,
                    trailerBytes.Length);


                return stream.ToArray();
            }
        }

        private void AddObjectAtNumber(
            List<string> objects,
            int objectNumber,
            string value)
        {
            while (
                objects.Count <=
                objectNumber)
            {
                objects.Add(
                    "");
            }

            objects[
                objectNumber] =
                value;
        }


        private List<string> WrapPdfText(
            string text,
            int maximumCharacters)
        {
            List<string> result =
                new List<string>();

            if (text == null)
            {
                result.Add(
                    "");

                return result;
            }

            string cleanText =
                text.Replace(
                    "\r",
                    " ")
                .Replace(
                    "\n",
                    " ")
                .Trim();

            if (cleanText.Length == 0)
            {
                result.Add(
                    "");

                return result;
            }


            while (
                cleanText.Length >
                maximumCharacters)
            {
                int breakPosition =
                    cleanText.LastIndexOf(
                        ' ',
                        maximumCharacters);

                if (breakPosition <= 0)
                {
                    breakPosition =
                        maximumCharacters;
                }

                result.Add(
                    cleanText.Substring(
                        0,
                        breakPosition));

                cleanText =
                    cleanText.Substring(
                        breakPosition)
                    .TrimStart();
            }


            result.Add(
                cleanText);

            return result;
        }


        private string EscapePdfText(
            string text)
        {
            if (string.IsNullOrEmpty(
                    text))
            {
                return "";
            }

            return text
                .Replace(
                    "\\",
                    "\\\\")
                .Replace(
                    "(",
                    "\\(")
                .Replace(
                    ")",
                    "\\)");
        }


        private string FormatPdfText(
            string value)
        {
            if (string.IsNullOrEmpty(
                    value))
            {
                return "";
            }

            return value
                .Replace(
                    "\t",
                    " ")
                .Replace(
                    "\r",
                    " ")
                .Replace(
                    "\n",
                    " ");
        }

        #endregion


        #region DOCX

        private void ExportDocx(
            clsDashboardExportRequest request,
            string outputFilePath)
        {
            using (
                WordprocessingDocument document =
                WordprocessingDocument.Create(
                    outputFilePath,
                    WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart =
                    document.AddMainDocumentPart();

                mainPart.Document =
                    new Word.Document();

                Word.Body body =
                    new Word.Body();

                mainPart.Document.AppendChild(
                    body);


                /*
                 * Title
                 */

                Word.Paragraph titleParagraph =
                    new Word.Paragraph();

                Word.Run titleRun =
                    new Word.Run();

                Word.RunProperties titleProperties =
                    new Word.RunProperties();

                titleProperties.Bold =
                    new Word.Bold();

                titleProperties.FontSize =
                    new Word.FontSize
                    {
                        Val = "32"
                    };

                titleRun.Append(
                    titleProperties);

                titleRun.Append(
                    new Word.Text(
                        string.IsNullOrWhiteSpace(
                            request.CardTitle)
                            ? "StaffSync Dashboard Export"
                            : request.CardTitle));

                titleParagraph.Append(
                    titleRun);

                body.Append(
                    titleParagraph);


                /*
                 * Generated date
                 */

                DateTime generatedAt =
                    request.GeneratedAt ??
                    DateTime.Now;

                Word.Paragraph generatedParagraph =
                    new Word.Paragraph();

                Word.Run generatedRun =
                    new Word.Run();

                generatedRun.Append(
                    new Word.Text(
                        "Generated: " +
                        generatedAt.ToString(
                            "dd-MMM-yyyy HH:mm:ss",
                            CultureInfo.InvariantCulture)));

                generatedParagraph.Append(
                    generatedRun);

                body.Append(
                    generatedParagraph);


                /*
                 * Data table
                 */

                AddDocxDataTable(
                    body,
                    request.Rows);

                mainPart.Document.Save();
            }
        }


        private void AddDocxDataTable(
            Word.Body body,
            List<Dictionary<string, object>> rows)
        {
            List<string> columns =
                GetColumns(
                    rows);


            if (columns.Count == 0)
            {
                Word.Paragraph paragraph =
                    new Word.Paragraph();

                paragraph.Append(
                    new Word.Run(
                        new Word.Text(
                            "No data available.")));

                body.Append(
                    paragraph);

                return;
            }


            Word.Table table =
                new Word.Table();


            /*
             * Table borders
             */

            Word.TableProperties tableProperties =
                new Word.TableProperties();

            Word.TableBorders tableBorders =
                new Word.TableBorders();

            Word.TopBorder topBorder =
                new Word.TopBorder
                {
                    Val =
                        Word.BorderValues.Single,
                    Size = 4
                };

            Word.BottomBorder bottomBorder =
                new Word.BottomBorder
                {
                    Val =
                        Word.BorderValues.Single,
                    Size = 4
                };

            Word.LeftBorder leftBorder =
                new Word.LeftBorder
                {
                    Val =
                        Word.BorderValues.Single,
                    Size = 4
                };

            Word.RightBorder rightBorder =
                new Word.RightBorder
                {
                    Val =
                        Word.BorderValues.Single,
                    Size = 4
                };

            Word.InsideHorizontalBorder
                insideHorizontalBorder =
                new Word.InsideHorizontalBorder
                {
                    Val =
                        Word.BorderValues.Single,
                    Size = 4
                };

            Word.InsideVerticalBorder
                insideVerticalBorder =
                new Word.InsideVerticalBorder
                {
                    Val =
                        Word.BorderValues.Single,
                    Size = 4
                };

            tableBorders.Append(
                topBorder,
                bottomBorder,
                leftBorder,
                rightBorder,
                insideHorizontalBorder,
                insideVerticalBorder);

            tableProperties.Append(
                tableBorders);

            table.AppendChild(
                tableProperties);


            /*
             * Header
             */

            Word.TableRow headerRow =
                new Word.TableRow();

            foreach (
                string column
                in columns)
            {
                Word.TableCell cell =
                    new Word.TableCell();

                Word.Paragraph paragraph =
                    new Word.Paragraph();

                Word.Run run =
                    new Word.Run();

                Word.RunProperties properties =
                    new Word.RunProperties();

                properties.Bold =
                    new Word.Bold();

                run.Append(
                    properties);

                run.Append(
                    new Word.Text(
                        FormatColumnName(
                            column)));

                paragraph.Append(
                    run);

                cell.Append(
                    paragraph);

                headerRow.Append(
                    cell);
            }

            table.Append(
                headerRow);


            /*
             * Data rows
             */

            foreach (
                Dictionary<string, object> row
                in rows ??
                new List<Dictionary<string, object>>())
            {
                Word.TableRow dataRow =
                    new Word.TableRow();

                foreach (
                    string column
                    in columns)
                {
                    object value =
                        null;

                    if (row != null)
                    {
                        row.TryGetValue(
                            column,
                            out value);
                    }

                    Word.TableCell cell =
                        new Word.TableCell();

                    Word.Paragraph paragraph =
                        new Word.Paragraph();

                    Word.Run run =
                        new Word.Run();

                    run.Append(
                        new Word.Text(
                            FormatValue(
                                value)));

                    paragraph.Append(
                        run);

                    cell.Append(
                        paragraph);

                    dataRow.Append(
                        cell);
                }

                table.Append(
                    dataRow);
            }

            body.Append(
                table);
        }

        #endregion


        #region XLSX

        private void ExportXlsx(
            clsDashboardExportRequest request,
            string outputFilePath)
        {
            using (
                XLWorkbook workbook =
                new XLWorkbook())
            {
                string worksheetName =
                    SanitizeWorksheetName(
                        string.IsNullOrWhiteSpace(
                            request.CardTitle)
                            ? "Dashboard Export"
                            : request.CardTitle);

                var worksheet =
                    workbook.Worksheets.Add(
                        worksheetName);

                List<string> columns =
                    GetColumns(
                        request.Rows);


                /*
                 * Header
                 */

                for (
                    int columnIndex = 0;
                    columnIndex < columns.Count;
                    columnIndex++)
                {
                    worksheet.Cell(
                        1,
                        columnIndex + 1)
                        .Value =
                        FormatColumnName(
                            columns[
                                columnIndex]);
                }


                /*
                 * Data
                 */

                int rowIndex = 2;

                foreach (
                    Dictionary<string, object> row
                    in request.Rows ??
                    new List<Dictionary<string, object>>())
                {
                    for (
                        int columnIndex = 0;
                        columnIndex < columns.Count;
                        columnIndex++)
                    {
                        object value =
                            null;

                        if (row != null)
                        {
                            row.TryGetValue(
                                columns[
                                    columnIndex],
                                out value);
                        }

                        worksheet.Cell(
                            rowIndex,
                            columnIndex + 1)
                            .Value =
                            FormatValue(
                                value);
                    }

                    rowIndex++;
                }


                /*
                 * Formatting
                 */

                if (columns.Count > 0)
                {
                    var headerRange =
                        worksheet.Range(
                            1,
                            1,
                            1,
                            columns.Count);

                    headerRange.Style.Font.Bold =
                        true;

                    headerRange.Style.Alignment
                        .WrapText =
                        true;
                }

                worksheet.Columns()
                    .AdjustToContents();

                workbook.SaveAs(
                    outputFilePath);
            }
        }

        #endregion


        #region CSV

        private void ExportCsv(
            clsDashboardExportRequest request,
            string outputFilePath)
        {
            List<string> columns =
                GetColumns(
                    request.Rows);

            StringBuilder csv =
                new StringBuilder();


            /*
             * Header
             */

            csv.AppendLine(
                string.Join(
                    ",",
                    columns.Select(
                        EscapeCsv)));


            /*
             * Data
             */

            foreach (
                Dictionary<string, object> row
                in request.Rows ??
                new List<Dictionary<string, object>>())
            {
                List<string> values =
                    new List<string>();

                foreach (
                    string column
                    in columns)
                {
                    object value =
                        null;

                    if (row != null)
                    {
                        row.TryGetValue(
                            column,
                            out value);
                    }

                    values.Add(
                        EscapeCsv(
                            FormatValue(
                                value)));
                }

                csv.AppendLine(
                    string.Join(
                        ",",
                        values));
            }

            File.WriteAllText(
                outputFilePath,
                csv.ToString(),
                new UTF8Encoding(true));
        }


        private string EscapeCsv(
            string value)
        {
            if (value == null)
                return "";

            bool mustQuote =
                value.Contains(",") ||
                value.Contains("\"") ||
                value.Contains("\r") ||
                value.Contains("\n");

            string escaped =
                value.Replace(
                    "\"",
                    "\"\"");

            return mustQuote
                ? "\"" +
                  escaped +
                  "\""
                : escaped;
        }

        #endregion


        #region XML

        private void ExportXml(
            clsDashboardExportRequest request,
            string outputFilePath)
        {
            List<string> columns =
                GetColumns(
                    request.Rows);

            XElement root =
                new XElement(
                    "DashboardExport");


            root.Add(
                new XElement(
                    "CardTitle",
                    request.CardTitle ?? ""));


            root.Add(
                new XElement(
                    "GeneratedAt",
                    (request.GeneratedAt ??
                     DateTime.Now)
                    .ToString(
                        "o",
                        CultureInfo.InvariantCulture)));


            XElement data =
                new XElement(
                    "Data");


            foreach (
                Dictionary<string, object> row
                in request.Rows ??
                new List<Dictionary<string, object>>())
            {
                XElement rowElement =
                    new XElement(
                        "Row");

                foreach (
                    string column
                    in columns)
                {
                    object value =
                        null;

                    if (row != null)
                    {
                        row.TryGetValue(
                            column,
                            out value);
                    }

                    string elementName =
                        SanitizeXmlElementName(
                            column);

                    rowElement.Add(
                        new XElement(
                            elementName,
                            FormatValue(
                                value)));
                }

                data.Add(
                    rowElement);
            }

            root.Add(
                data);


            XDocument document =
                new XDocument(
                    new XDeclaration(
                        "1.0",
                        "utf-8",
                        "yes"),
                    root);

            document.Save(
                outputFilePath);
        }

        #endregion


        #region JSON

        private void ExportJson(
            clsDashboardExportRequest request,
            string outputFilePath)
        {
            string json =
                JsonConvert.SerializeObject(
                    request.Rows ??
                    new List<Dictionary<string, object>>(),
                    Formatting.Indented);

            File.WriteAllText(
                outputFilePath,
                json,
                new UTF8Encoding(false));
        }

        #endregion


        #region Common Helpers

        private List<string> GetColumns(
            List<Dictionary<string, object>> rows)
        {
            List<string> columns =
                new List<string>();

            if (rows == null)
                return columns;

            foreach (
                Dictionary<string, object> row
                in rows)
            {
                if (row == null)
                    continue;

                foreach (
                    string key
                    in row.Keys)
                {
                    if (!columns.Contains(
                            key))
                    {
                        columns.Add(
                            key);
                    }
                }
            }

            return columns;
        }


        private string FormatValue(
            object value)
        {
            if (value == null)
                return "";

            if (value == DBNull.Value)
                return "";

            if (value is DateTime)
            {
                return ((DateTime)value)
                    .ToString(
                        "dd-MMM-yyyy HH:mm:ss",
                        CultureInfo.InvariantCulture);
            }

            if (value is bool)
            {
                return ((bool)value)
                    ? "Yes"
                    : "No";
            }

            if (value is decimal)
            {
                return ((decimal)value)
                    .ToString(
                        CultureInfo.InvariantCulture);
            }

            if (value is double)
            {
                return ((double)value)
                    .ToString(
                        CultureInfo.InvariantCulture);
            }

            if (value is float)
            {
                return ((float)value)
                    .ToString(
                        CultureInfo.InvariantCulture);
            }

            return Convert.ToString(
                value,
                CultureInfo.InvariantCulture)
                ?? "";
        }


        private string FormatColumnName(
            string columnName)
        {
            if (string.IsNullOrWhiteSpace(
                    columnName))
            {
                return "";
            }

            StringBuilder result =
                new StringBuilder();

            for (
                int i = 0;
                i < columnName.Length;
                i++)
            {
                char current =
                    columnName[i];

                if (
                    i > 0 &&
                    char.IsUpper(current) &&
                    !char.IsWhiteSpace(
                        columnName[i - 1]))
                {
                    result.Append(
                        ' ');
                }

                result.Append(
                    current);
            }

            return result.ToString();
        }


        private string SanitizeWorksheetName(
            string name)
        {
            if (string.IsNullOrWhiteSpace(
                    name))
            {
                return "Dashboard Export";
            }

            char[] invalidCharacters =
            {
                ':',
                '\\',
                '/',
                '?',
                '*',
                '[',
                ']'
            };

            string result =
                name;

            foreach (
                char invalid
                in invalidCharacters)
            {
                result =
                    result.Replace(
                        invalid.ToString(),
                        "");
            }

            result =
                result.Trim();

            if (result.Length == 0)
            {
                result =
                    "Dashboard Export";
            }

            if (result.Length > 31)
            {
                result =
                    result.Substring(
                        0,
                        31);
            }

            return result;
        }


        private string SanitizeXmlElementName(
            string name)
        {
            if (string.IsNullOrWhiteSpace(
                    name))
            {
                return "Column";
            }

            StringBuilder result =
                new StringBuilder();

            for (
                int i = 0;
                i < name.Length;
                i++)
            {
                char c =
                    name[i];

                if (
                    char.IsLetterOrDigit(c) ||
                    c == '_' ||
                    c == '-')
                {
                    result.Append(
                        c);
                }
                else
                {
                    result.Append(
                        '_');
                }
            }

            if (result.Length == 0)
            {
                result.Append(
                    "Column");
            }

            if (
                !char.IsLetter(result[0]) &&
                result[0] != '_')
            {
                result.Insert(
                    0,
                    "Column_");
            }

            return result.ToString();
        }

        #endregion
    }
}