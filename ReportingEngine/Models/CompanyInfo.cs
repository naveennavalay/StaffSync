using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Models
{
    /// <summary>
    /// Represents company information displayed in reports.
    /// </summary>
    public class CompanyInfo
    {
        public string CompanyName { get; set; }

        public string ProductName { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string PinCode { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public string GSTNumber { get; set; }

        public string CINNumber { get; set; }

        public string LogoPath { get; set; }
        public double LogoWidth { get; set; } = 2.2;

        public double LogoHeight { get; set; } = 2.2;

        public CompanyInfo()
        {
            CompanyName = string.Empty;
            ProductName = string.Empty;
            AddressLine1 = string.Empty;
            AddressLine2 = string.Empty;
            City = string.Empty;
            State = string.Empty;
            Country = string.Empty;
            PinCode = string.Empty;
            Phone = string.Empty;
            Mobile = string.Empty;
            Email = string.Empty;
            Website = string.Empty;
            GSTNumber = string.Empty;
            CINNumber = string.Empty;
            LogoPath = string.Empty;
        }
    }
}
