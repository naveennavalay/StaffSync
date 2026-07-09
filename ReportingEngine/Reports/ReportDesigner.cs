using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using ReportingEngine.Builders;
using ReportingEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ReportingEngine.Reports
{
    public class ReportDesigner
    {
        private readonly CompanyInfo _company;
        private readonly ReportInfo _report;

        public ReportDesigner()
        {
            _company = CreateCompanyInfo();
            _report = CreateReportInfo();
        }

        public void Generate(string outputFile)
        {
            Document document = CreateDocument();

            BuildDocument(document);

            Export(document, outputFile);
        }

        private Document CreateDocument()
        {
            Document document = new Document();

            document.Info.Title = _report.ReportTitle;
            document.Info.Author = "StaffSync Reporting Engine";
            document.Info.Subject = _report.ReportTitle;

            return document;
        }

        private void BuildDocument(Document document)
        {
            Section section = document.AddSection();

            section.PageSetup.PageFormat = PageFormat.A4;
            section.PageSetup.TopMargin = Unit.FromCentimeter(1);
            section.PageSetup.BottomMargin = Unit.FromCentimeter(1);
            section.PageSetup.LeftMargin = Unit.FromCentimeter(1);
            section.PageSetup.RightMargin = Unit.FromCentimeter(1);

            //-------------------------------------------------------
            // Header
            //-------------------------------------------------------

            HeaderBuilder headerBuilder = new HeaderBuilder();

            headerBuilder.Build(section, _company, _report);

            //-------------------------------------------------------
            // Report Title
            //-------------------------------------------------------

            //Paragraph title = section.AddParagraph();

            //title.Format.Alignment = ParagraphAlignment.Center;
            //title.Format.Font.Bold = true;
            //title.Format.Font.Size = 18;
            //title.Format.SpaceBefore = Unit.FromCentimeter(0.4);
            //title.Format.SpaceAfter = Unit.FromCentimeter(0.4);

            //title.AddText(_report.ReportTitle.ToUpper());

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

            title.AddText(_report.ReportTitle.ToUpper());

            Paragraph reportDate = titleRow.Cells[1].AddParagraph();

            reportDate.Format.Alignment = ParagraphAlignment.Right;
            reportDate.Format.Font.Size = 9;
            reportDate.Format.Font.Bold = true;
            reportDate.Format.SpaceBefore = Unit.FromPoint(10);

            reportDate.AddText("Date : " + _report.GeneratedOn.ToString("dd-MMM-yyyy"));

            //-------------------------------------------------------
            // Professional Divider
            //-------------------------------------------------------

            //Paragraph divider = section.AddParagraph();

            //divider.Format.Borders.Bottom.Width = 1.2;
            //divider.Format.SpaceAfter = Unit.FromCentimeter(0.4);

            ////Paragraph line = section.AddParagraph();

            ////line.Format.Borders.Bottom.Width = 1;

            ////line.Format.SpaceAfter = Unit.FromPoint(8);

            //-------------------------------------------------------
            // Employee Table
            //-------------------------------------------------------

            BuildEmployeeTable(section);
        }

        private void BuildEmployeeTable(Section section)
        {
            Table table = section.AddTable();

            table.Style = "Table";
            table.Borders.Width = 0.5;
            table.Rows.LeftIndent = 0;

            table.AddColumn(Unit.FromCentimeter(2.5));
            table.AddColumn(Unit.FromCentimeter(5.5));
            table.AddColumn(Unit.FromCentimeter(4.0));
            table.AddColumn(Unit.FromCentimeter(4.5));
            table.AddColumn(Unit.FromCentimeter(2.5));

            //----------------------------------------------------
            // Header
            //----------------------------------------------------

            Row header = table.AddRow();

            header.Shading.Color = Colors.DarkBlue;
            header.Format.Font.Color = Colors.White;
            header.Format.Font.Bold = true;
            header.Format.Alignment = ParagraphAlignment.Center;
            header.VerticalAlignment = VerticalAlignment.Center;

            header.Cells[0].AddParagraph("Code");
            header.Cells[1].AddParagraph("Employee Name");
            header.Cells[2].AddParagraph("Department");
            header.Cells[3].AddParagraph("Designation");
            header.Cells[4].AddParagraph("Status");

            //----------------------------------------------------
            // Data
            //----------------------------------------------------

            List<EmployeeInfo> employees = EmployeeSampleData.Get();

            bool alternate = false;

            foreach (EmployeeInfo employee in employees)
            {
                Row row = table.AddRow();

                row.VerticalAlignment = VerticalAlignment.Center;

                if (alternate)
                    row.Shading.Color = Color.Parse("#F7F9FC");

                alternate = !alternate;

                row.Cells[0].AddParagraph(employee.EmployeeCode);
                row.Cells[1].AddParagraph(employee.EmployeeName);
                row.Cells[2].AddParagraph(employee.Department);
                row.Cells[3].AddParagraph(employee.Designation);
                row.Cells[4].AddParagraph(employee.Status);
            }

            //section.Add(table);
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

