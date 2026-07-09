using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using ReportingEngine.Core;
using ReportingEngine.Models;
using ReportingEngine.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Builders
{
    public class HeaderBuilder
    {
        public void Build(Section section, CompanyInfo company, ReportInfo report)
        {
            Table table = section.AddTable();

            table.Borders.Width = 0.5;
            table.Borders.Color = Colors.LightGray;
            table.Rows.LeftIndent = 0;

            // Logo
            table.AddColumn(Unit.FromCentimeter(3.0));

            // Company Information
            table.AddColumn(Unit.FromCentimeter(15.5));

            Row row = table.AddRow();

            row.Height = Unit.FromCentimeter(3.8);

            row.VerticalAlignment = VerticalAlignment.Top;

            BuildLogo(row.Cells[0]);

            BuildCompany(row.Cells[1], company);

            ////-------------------------------------------------------
            //// Divider
            ////-------------------------------------------------------

            //Row divider = table.AddRow();

            //divider.Height = Unit.FromPoint(2);

            //divider.Borders.Top.Width = 1.2;

            //divider.Borders.Top.Color = Color.Parse("#1F4E79");

            //divider.Cells[0].MergeRight = 1;

            //section.AddParagraph().Format.SpaceAfter = Unit.FromPoint(3);
        }

        private void BuildLogo(Cell cell)
        {
            cell.VerticalAlignment = VerticalAlignment.Center;

            cell.Format.Alignment = ParagraphAlignment.Center;

            Paragraph p = cell.AddParagraph();

            p.Format.SpaceBefore = Unit.FromCentimeter(1.0);

            p.Format.Alignment = ParagraphAlignment.Center;

            p.Format.Font.Bold = true;

            p.Format.Font.Size = 18;

            p.AddText("LOGO");

            Paragraph p2 = cell.AddParagraph();

            p2.Format.Alignment = ParagraphAlignment.Center;

            p2.Format.Font.Size = 8;

            p2.Format.Font.Color = Colors.Gray;

            p2.AddText("120 x 120");

            // Future Enhancement
            //
            // if (!string.IsNullOrWhiteSpace(company.LogoPath))
            // {
            //      Image image = cell.AddImage(company.LogoPath);
            //      image.Width = Unit.FromCentimeter(2.2);
            // }
        }

        private void BuildCompany(Cell cell, CompanyInfo company)
        {
            cell.VerticalAlignment = VerticalAlignment.Top;

            //----------------------------------------------------------
            // Company Name
            //----------------------------------------------------------

            Paragraph p = cell.AddParagraph();

            p.Format.Font.Bold = true;
            p.Format.Font.Size = 16;
            p.Format.Font.Color = Color.Parse("#1F4E79");
            p.Format.SpaceAfter = Unit.FromPoint(2);

            p.AddText(company.CompanyName);

            //----------------------------------------------------------
            // Product Name
            //----------------------------------------------------------

            p = cell.AddParagraph();

            p.Format.Font.Bold = true;
            p.Format.Font.Size = 10;
            p.Format.SpaceAfter = Unit.FromPoint(6);

            p.AddText(company.ProductName);

            //----------------------------------------------------------
            // Tab Stop
            //----------------------------------------------------------

            //TabStop tab = new TabStop();

            //tab.Position = Unit.FromCentimeter(8.5);

            //p.Format.TabStops.Add(tab);

            //----------------------------------------------------------
            // Address + Contact
            //----------------------------------------------------------

            AddCompanyAddressContact(cell, company.AddressLine1, "Phone", company.Phone);

            AddCompanyAddressContact(cell, company.AddressLine2, "Mobile", company.Mobile);

            AddCompanyAddressContact(cell, $"{company.City}, {company.State}", "Email", company.Email);

            AddCompanyAddressContact(cell, $"{company.Country} - {company.PinCode}", "Website", company.Website);

            AddCompanyAddressContact(cell, "", "GST", company.GSTNumber);

            AddCompanyAddressContact(cell, "", "CIN", company.CINNumber);
        }

        private void AddCompanyAddressContact(Cell cell, string address, string caption, string value)
        {
            Paragraph p = cell.AddParagraph();

            p.Format.Font.Size = 8.5;

            p.Format.SpaceAfter = Unit.FromPoint(2);

            p.Format.TabStops.AddTabStop(Unit.FromCentimeter(8.5));

            if (!string.IsNullOrWhiteSpace(address))
                p.AddText(address);

            p.AddTab();

            p.AddFormattedText(caption + " : ", TextFormat.Bold);

            if (!string.IsNullOrWhiteSpace(value))
                p.AddText(value);
        }




        //public class HeaderBuilder
        //{
        //    public void Build(Section section, CompanyInfo company, ReportInfo report)
        //    {
        //        Table table = section.AddTable();

        //        table.Borders.Visible = false;

        //        table.AddColumn(Unit.FromCentimeter(3.0));
        //        table.AddColumn(Unit.FromCentimeter(16.0));
        //        //table.AddColumn(Unit.FromCentimeter(5.0));

        //        Row row = table.AddRow();

        //        BuildLogo(row.Cells[0]);
        //        BuildCompany(row.Cells[1], company);
        //        //BuildReportInfo(row.Cells[2], report);

        //        section.AddParagraph();
        //    }

        //    private void BuildLogo(Cell cell)
        //    {
        //        cell.VerticalAlignment = VerticalAlignment.Center;

        //        cell.Format.Alignment = ParagraphAlignment.Center;

        //        cell.Borders.Width = 0.75;

        //        cell.Borders.Color = Colors.DarkGray;

        //        cell.Shading.Color = Color.Parse("#F8F8F8");

        //        Paragraph p = cell.AddParagraph();

        //        p.Format.Alignment = ParagraphAlignment.Center;

        //        p.Format.SpaceBefore = Unit.FromCentimeter(0.60);

        //        p.Format.Font.Bold = true;

        //        p.Format.Font.Size = 15;

        //        //-------------------------------------------------------
        //        // Future Enhancement
        //        //-------------------------------------------------------
        //        //
        //        // if(File.Exists(company.LogoPath))
        //        // {
        //        //      cell.AddImage(company.LogoPath);
        //        // }
        //        //
        //        //-------------------------------------------------------

        //        p.AddText("LOGO");

        //        Paragraph p2 = cell.AddParagraph();

        //        p2.Format.Alignment = ParagraphAlignment.Center;

        //        p2.Format.Font.Size = 8;

        //        p2.Format.Font.Color = Colors.Gray;

        //        p2.AddText("120 x 120");
        //    }

        //    private void BuildCompany(Cell cell, CompanyInfo company)
        //    {
        //        Paragraph p;

        //        p = cell.AddParagraph(company.CompanyName);
        //        p.Format.Font.Size = 16;
        //        p.Format.Font.Bold = true;
        //        p.Format.SpaceAfter = 2;

        //        p = cell.AddParagraph(company.ProductName);
        //        p.Format.Font.Size = 10;

        //        if (!string.IsNullOrWhiteSpace(company.AddressLine1))
        //            cell.AddParagraph(company.AddressLine1);

        //        if (!string.IsNullOrWhiteSpace(company.AddressLine2))
        //            cell.AddParagraph(company.AddressLine2);

        //        cell.AddParagraph($"{company.City}, {company.State}");

        //        cell.AddParagraph($"{company.Country} - {company.PinCode}");

        //        //cell.AddParagraph($"Phone : {company.Phone}");

        //        //cell.AddParagraph($"Email : {company.Email}");

        //        //cell.AddParagraph(company.Website);
        //    }

        //    private void BuildReportInfo(Cell cell, ReportInfo report)
        //    {
        //        cell.Format.Alignment = ParagraphAlignment.Left;

        //        AddRow(cell, "Generated By", report.GeneratedBy);

        //        AddRow(cell, "Generated On", report.GeneratedOn.ToString("dd-MMM-yyyy hh:mm tt"));

        //        AddRow(cell, "Version", report.Version);

        //        AddRow(cell, "Financial Year", report.FinancialYear);
        //    }

        //    private void AddRow(Cell cell, string caption, string value)
        //    {
        //        Paragraph p = cell.AddParagraph();

        //        p.AddFormattedText(caption + " : ", TextFormat.Bold);

        //        p.AddText(value);
        //    }
        //}
    }
}
