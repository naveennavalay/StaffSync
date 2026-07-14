using ModelStaffSync;
using ReportingEngine;
using ReportingEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //GenerateEmployeeMasterReport();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmDashboard()); 
            
            Application.Run(new frmLogin());
        }

        private static void GenerateEmployeeMasterReport()
        {
            DALStaffSync.EmployeeRelatedReportQueries objEmployeeRelatedReportQueries = new DALStaffSync.EmployeeRelatedReportQueries();

            //List <ActiveEmployeeListReport> objActiveEmployeeListReport = objEmployeeRelatedReportQueries.getActiveEmployeeListReport(1);

            List<MonthlyAttendanceReport> objActiveEmployeeListReport = objEmployeeRelatedReportQueries.getMonthlyAttendanceRegister(1, Convert.ToDateTime("01-06-2026"), Convert.ToDateTime("30-06-2026"));

            CompanyInfo company = new CompanyInfo()
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

                LogoPath = @"C:\Development\StaffSync\StaffSync\logo.png",
                LogoHeight = 3.5,
                LogoWidth = 3.5
            };

            //-------------------------------------------------------
            // Report Information
            //-------------------------------------------------------

            ReportInfo report = new ReportInfo()
            {
                ReportTitle = "Employee Master Report",
                GeneratedBy = Environment.UserName,
                GeneratedOn = DateTime.Now,
                Version = "1.0",
                FinancialYear = "2026-2027"
            };

            ReportDisplayOptions displayOptions = new ReportDisplayOptions()
            {
                ShowCompanyLogo = true,
                ShowHeader = true,
                ShowFooter = true,
                ShowGeneratedDate = true,
                ShowPageNumbers = true,
                ShowSummary = false,
                ShowWatermark = true,
                WatermarkText = "TRIAL VERSION",
                WatermarkFontSize = 48,
                WatermarkColorHex = "#D0D0D0",
                WatermarkAngle = 45,
                WatermarkOpacity = 0.15
            };

            ReportSettings settings = new ReportSettings
            {
                PageWidth = 60,
                PageHeight = 29.7,
                LeftMargin = 1,
                RightMargin = 1,
                TopMargin = 1,
                BottomMargin = 1
            };

            new ReportBuilder()
            .Company(company)
            .Title(report)
            .Data(objActiveEmployeeListReport)
            .Settings(settings)
            .Generate(@"C:\Development\StaffSync\StaffSync\bin\Debug\ReportDesigner.pdf");

            //new ReportBuilder()
            //    .Company(company)
            //    .Title(report)
            //    //.Columns(columns)
            //    .Data(objActiveEmployeeListReport)
            //    //.Summary(new List<ReportSummary>()
            //    //{
            //    //    new ReportSummary("Total Employees", employees.Count.ToString()),
            //    //    new ReportSummary("Active Employees", employees.Count(e => e.Status == "Active").ToString()),
            //    //    new ReportSummary("Inactive Employees", employees.Count(e => e.Status != "Active").ToString()),
            //    //})
            //    .Display(displayOptions)
            //    .Generate(@"C:\Development\StaffSync\StaffSync\bin\Debug\ReportDesigner.pdf");
        }
    }
}
