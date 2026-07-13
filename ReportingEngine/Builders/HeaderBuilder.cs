using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using MigraDoc.DocumentObjectModel.Tables;
using ModelStaffSync;
using ReportingEngine.Core;
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

            row.Height = Unit.FromCentimeter(3.5);

            row.VerticalAlignment = VerticalAlignment.Top;

            //BuildLogo(row.Cells[0]);
            AddCompanyLogo(row.Cells[0], company);

            BuildCompany(row.Cells[1], company);
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

        private void AddCompanyLogo(Cell cell, CompanyInfo company)
        {
            cell.VerticalAlignment = VerticalAlignment.Center;

            Paragraph p = cell.AddParagraph();

            p.Format.Alignment = ParagraphAlignment.Center;

            p.Format.SpaceBefore = Unit.FromPoint(0);

            if (string.IsNullOrWhiteSpace(company.LogoPath) ||
                !System.IO.File.Exists(company.LogoPath))
            {
                BuildLogo(cell);
                return;
            }

            Image image = p.AddImage(company.LogoPath);

            image.LockAspectRatio = true;

            //image.Width = Unit.FromCentimeter(2.6);
            image.Width = Unit.FromCentimeter(company.LogoWidth);

            image.RelativeHorizontal = RelativeHorizontal.Margin;

            image.RelativeVertical = RelativeVertical.Line;
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
    }
}
