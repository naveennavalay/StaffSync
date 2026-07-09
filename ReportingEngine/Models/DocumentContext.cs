using ReportingEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Models
{
    /// <summary>
    /// Contains all information required to generate a report.
    /// This class is shared across all report components.
    /// </summary>
    public class DocumentContext
    {
        #region General

        public ReportSettings Settings { get; set; }

        #endregion

        #region Company

        public CompanyInfo Company { get; set; }

        #endregion

        #region Report

        public ReportInfo Report { get; set; }

        #endregion

        #region Data

        /// <summary>
        /// Main data source.
        /// Can contain Employee List,
        /// Attendance List,
        /// Salary List,
        /// Leave List etc.
        /// </summary>
        public object DataSource { get; set; }

        #endregion

        #region Filters

        public List<ReportFilter> Filters { get; set; }

        #endregion

        #region Summary

        public List<ReportSummary> Summary { get; set; }

        #endregion

        #region Parameters

        public List<ReportParameter> Parameters { get; set; }

        #endregion

        public DocumentContext()
        {
            Company = new CompanyInfo();

            Report = new ReportInfo();

            Filters = new List<ReportFilter>();

            Summary = new List<ReportSummary>();

            Parameters = new List<ReportParameter>();
        }
    }
}
