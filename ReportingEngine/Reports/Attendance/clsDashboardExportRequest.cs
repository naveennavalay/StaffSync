using System;
using System.Collections.Generic;

namespace StaffSync.ReportingEngine.Reports.Attendance
{
    /// <summary>
    /// Common request model received from the StaffSync dashboard
    /// when the user selects an export format for a dashboard card.
    ///
    /// Supported formats:
    /// PDF, DOCX, XLSX, CSV, XML and JSON.
    /// </summary>
    public sealed class clsDashboardExportRequest
    {
        /// <summary>
        /// Action requested by the dashboard.
        /// Expected value: "Export".
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// HTML ID of the dashboard card being exported.
        /// Example:
        /// attendanceSummaryCard
        /// monthlyAttendanceRegisterCard
        /// attendanceCalendarCard
        /// </summary>
        public string CardId { get; set; }

        /// <summary>
        /// Display title of the dashboard card.
        /// Example:
        /// Attendance Summary
        /// Monthly Attendance Register
        /// </summary>
        public string CardTitle { get; set; }

        /// <summary>
        /// Requested export format.
        ///
        /// Expected values:
        /// pdf
        /// docx
        /// xlsx
        /// csv
        /// xml
        /// json
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Dashboard data rows.
        ///
        /// Dictionary is intentionally used here instead of a fixed
        /// report-specific model because different dashboard cards
        /// have different data structures.
        /// </summary>
        public List<Dictionary<string, object>> Rows { get; set; }

        /// <summary>
        /// Optional chart image supplied by the dashboard.
        ///
        /// This can contain a Base64/data URL representation of the
        /// chart when a chart image needs to be included in PDF/DOCX.
        /// </summary>
        public string ChartImage { get; set; }

        /// <summary>
        /// Date/time at which the dashboard generated the export request.
        /// </summary>
        public DateTime? GeneratedAt { get; set; }

        /// <summary>
        /// Initializes a new dashboard export request.
        /// </summary>
        public clsDashboardExportRequest()
        {
            Rows = new List<Dictionary<string, object>>();
            GeneratedAt = DateTime.Now;
        }
    }
}