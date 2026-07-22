using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using ModelStaffSync;
using ReportingEngine.Builders;
using ReportingEngine.Core;
using ReportingEngine.Helpers;
using ReportingEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
namespace ReportingEngine.Reports
{
    public class ReportDesigner
    {
        private DocumentContext _context;

        public ReportDesigner()
        {
        }

        public void Generate(DocumentContext context, string outputFile)
        {
            _context = context;

            Document document = CreateDocument();

            BuildDocument(document);

            Export(document, outputFile);
        }

        private Document CreateDocument()
        {
            Document document = new Document();

            document.Info.Title = _context.ReportInfo.ReportTitle;

            document.Info.Author = "StaffSync Reporting Engine";

            document.Info.Subject = _context.ReportInfo.ReportTitle;

            return document;
        }

        private void BuildDocument(Document document)
        {
            Section section = document.AddSection();

            PageLayoutManager layout = new PageLayoutManager();

            //layout.ApplyLayout(section, _context.Columns);

            //section.PageSetup.PageFormat = PageFormat.A4;

            section.PageSetup.PageWidth = Unit.FromCentimeter(_context.Settings.PageWidth);
            section.PageSetup.PageHeight = Unit.FromCentimeter(_context.Settings.PageHeight);

            section.PageSetup.TopMargin = Unit.FromCentimeter(1);
            section.PageSetup.BottomMargin = Unit.FromCentimeter(1);
            section.PageSetup.LeftMargin = Unit.FromCentimeter(1);
            section.PageSetup.RightMargin = Unit.FromCentimeter(1);

            BuildHeader(section);

            new WatermarkBuilder(_context).Build(section);

            Table titleTable = section.AddTable();

            titleTable.Borders.Visible = false;

            titleTable.AddColumn(Unit.FromCentimeter(15.0));
            titleTable.AddColumn(Unit.FromCentimeter(4.0));

            Row titleRow = titleTable.AddRow();

            Paragraph title = titleRow.Cells[0].AddParagraph();

            title.Format.Alignment = ParagraphAlignment.Center;
            title.Format.Font.Size = 18;
            title.Format.Font.Bold = true;
            title.Format.SpaceBefore = Unit.FromPoint(5);
            title.Format.SpaceAfter = Unit.FromPoint(5);

            title.AddText(_context.ReportInfo.ReportTitle.ToUpper());

            Paragraph reportDate = titleRow.Cells[1].AddParagraph();

            reportDate.Format.Alignment = ParagraphAlignment.Right;
            reportDate.Format.Font.Size = 9;
            reportDate.Format.Font.Bold = true;
            reportDate.Format.SpaceBefore = Unit.FromPoint(10);

            reportDate.AddText("Date : " + _context.ReportInfo.GeneratedOn.ToString("dd-MMM-yyyy"));

            //DynamicTableBuilder builder = new DynamicTableBuilder();

            //builder.Build(section, _context.Columns, _context.Data);

            if (string.IsNullOrWhiteSpace(_context.GroupByProperty))
            {
                DynamicTableBuilder builder = new DynamicTableBuilder();

                builder.Build(
                    section,
                    _context.Columns,
                    _context.Data);
            }
            else
            {
                GroupedTableBuilder builder = new GroupedTableBuilder(_context);

                builder.Build(section);
            }

            new SummaryBuilder(_context).Build(section);

            BuildFooter(section);
        }

        private void BuildFooter(Section section)
        {
            if (!_context.DisplayOptions.ShowFooter)
                return;

            FooterBuilder footerBuilder = new FooterBuilder();

            footerBuilder.Build(section, _context.ReportInfo, _context.DisplayOptions);
        }

        private void BuildHeader(Section section)
        {
            if (!_context.DisplayOptions.ShowHeader)
                return;

            HeaderBuilder headerBuilder = new HeaderBuilder();

            headerBuilder.Build(
                section,
                _context.CompanyInfo,
                _context.ReportInfo);
        }

        private void Export(Document document, string outputFile)
        {
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);

            renderer.Document = document;

            renderer.RenderDocument();

            renderer.PdfDocument.Save(outputFile);
        }

        private CompanyInfo CreateCompanyInfo()
        {
            return new CompanyInfo()
            {
                CompanyName = "StaffSync Technologies Pvt. Ltd.",
                ProductName = "Employee Management System",

                AddressLine1 = "Baner Road",
                AddressLine2 = "Baner",

                City = "Pune",
                State = "Maharashtra",
                Country = "India",
                PinCode = "411045",

                Phone = "+91 20 12345678",
                Mobile = "+91 9876543210",

                Email = "support@staffsync.com",

                Website = "www.staffsync.com",

                GSTNumber = "27ABCDE1234F1Z5",
                CINNumber = "U12345PN2026PTC123456",

                LogoPath = ""
            };
        }

        private ReportInfo CreateReportInfo()
        {
            return new ReportInfo()
            {
                ReportTitle = "Employee Master Report",

                GeneratedBy = Environment.UserName,

                GeneratedOn = DateTime.Now,

                Version = "1.0.0",

                FinancialYear = "2026-2027",

                //PageNumber = 1
            };
        }
    }
}

