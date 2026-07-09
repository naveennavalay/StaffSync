using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Core
{
    /// <summary>
    /// Holds runtime information required while generating a report.
    /// This class contains report-specific information and is passed
    /// throughout the Reporting Engine.
    /// </summary>
    public class ReportContext
    {
        #region Company Information

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }

        public string CompanyPhone { get; set; }

        public string CompanyEmail { get; set; }

        public string CompanyWebsite { get; set; }

        public string CompanyLogoPath { get; set; }

        #endregion

        #region Report Information

        public string ReportTitle { get; set; }

        public string ReportSubTitle { get; set; }

        public string ModuleName { get; set; }

        public string ReportDescription { get; set; }

        #endregion

        #region User Information

        public string GeneratedBy { get; set; }

        public DateTime GeneratedOn { get; set; }

        public string UserName { get; set; }

        public string MachineName { get; set; }

        #endregion

        #region Date Information

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string FinancialYear { get; set; }

        #endregion

        #region Filter Information

        public Dictionary<string, string> Filters { get; set; }

        #endregion

        #region Report Statistics

        public int TotalRecords { get; set; }

        public int PageCount { get; set; }

        public string ReportVersion { get; set; }

        #endregion

        #region Custom Parameters

        public Dictionary<string, object> Parameters { get; set; }

        #endregion

        #region Constructor

        public ReportContext()
        {
            Filters = new Dictionary<string, string>();

            Parameters = new Dictionary<string, object>();

            GeneratedOn = DateTime.Now;

            MachineName = Environment.MachineName;

            UserName = Environment.UserName;

            ReportVersion = "1.0";
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Add or Update Filter.
        /// </summary>
        public void AddFilter(string key, string value)
        {
            if (Filters.ContainsKey(key))
                Filters[key] = value;
            else
                Filters.Add(key, value);
        }

        /// <summary>
        /// Add or Update Custom Parameter.
        /// </summary>
        public void AddParameter(string key, object value)
        {
            if (Parameters.ContainsKey(key))
                Parameters[key] = value;
            else
                Parameters.Add(key, value);
        }

        /// <summary>
        /// Returns a filter value.
        /// </summary>
        public string GetFilter(string key)
        {
            if (Filters.ContainsKey(key))
                return Filters[key];

            return string.Empty;
        }

        /// <summary>
        /// Returns a custom parameter.
        /// </summary>
        public object GetParameter(string key)
        {
            if (Parameters.ContainsKey(key))
                return Parameters[key];

            return null;
        }

        #endregion
    }
}
