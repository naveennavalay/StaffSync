using ReportingEngine.Core;
using ReportingEngine.Models;
using ReportingEngine.Reports;
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
            GenerateEmployeeMasterReport();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmDashboard()); 
            
            Application.Run(new frmLogin());
        }

        private static void GenerateEmployeeMasterReport()
        {
            //-------------------------------------------------------
            // Company Information
            //-------------------------------------------------------

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

            //-------------------------------------------------------
            // Report Display Options
            //-------------------------------------------------------

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

                WatermarkAngle = 45,

                WatermarkOpacity = 0.15,

                WatermarkColorHex = "#FF0000"
            };

            var columns = new List<ReportColumn>()
            {
                new ReportColumn("Code","EmployeeCode"){ Width=2 },

                new ReportColumn("Employee Name","EmployeeName"){ Width=5 },

                new ReportColumn("Department","Department"){ Width=4 },

                new ReportColumn("Designation","Designation"){ Width=4 },

                new ReportColumn("Status","Status"){ Width=2 },
                new ReportColumn("Date Of Birth","EmployeeDOB"){ Width=2 },
            };

            //-------------------------------------------------------
            // Sample Employee Data
            //-------------------------------------------------------

            List<EmployeeInfo> employees = new List<EmployeeInfo>()
            {
                new EmployeeInfo()
                {
                    EmployeeCode = "EMP001",
                    EmployeeName = "Naveen Navale",
                    Department = "Development",
                    Designation = "Technical Specialist",
                    Status = "Active",
                    EmployeeDOB = new DateTime(1990, 5, 15),
                },

                new EmployeeInfo()
                {
                    EmployeeCode = "EMP002",
                    EmployeeName = "John Smith",
                    Department = "Testing",
                    Designation = "QA Engineer",
                    Status = "Active",
                    EmployeeDOB = new DateTime(1990, 5, 15)
                },

                new EmployeeInfo()
                {
                    EmployeeCode = "EMP003",
                    EmployeeName = "David Wilson",
                    Department = "HR",
                    Designation = "HR Executive",
                    Status = "Active",
                    EmployeeDOB = new DateTime(1990, 5, 15)
                }
            };

            //-------------------------------------------------------
            // Generate Report
            //-------------------------------------------------------

            new ReportBuilder()
                .Company(company)
                .Title(report)
                .Columns(columns)
                .Data(employees)
                .Display(displayOptions)
                .Generate(@"C:\Development\StaffSync\StaffSync\bin\Debug\ReportDesigner.pdf");
        }
    }
}
