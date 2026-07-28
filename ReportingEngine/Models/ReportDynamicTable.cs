using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Models
{
    /// <summary>
    /// Represents a completely dynamic table that can be added
    /// anywhere in the report.
    /// </summary>
    public class ReportDynamicTable
    {
        #region General

        /// <summary>
        /// Table title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Table subtitle.
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// Show title.
        /// </summary>
        public bool ShowTitle { get; set; }

        /// <summary>
        /// Show subtitle.
        /// </summary>
        public bool ShowSubTitle { get; set; }

        #endregion

        #region Layout

        /// <summary>
        /// Space before table.
        /// </summary>
        public double SpaceBefore { get; set; }

        /// <summary>
        /// Space after table.
        /// </summary>
        public double SpaceAfter { get; set; }

        /// <summary>
        /// Show table border.
        /// </summary>
        public bool ShowBorders { get; set; }

        /// <summary>
        /// Border width.
        /// </summary>
        public double BorderWidth { get; set; }

        /// <summary>
        /// Border color.
        /// </summary>
        public Color BorderColor { get; set; }

        #endregion

        #region Header

        /// <summary>
        /// Show header row.
        /// </summary>
        public bool ShowHeader { get; set; }

        /// <summary>
        /// Repeat header on every page.
        /// </summary>
        public bool RepeatHeader { get; set; }

        /// <summary>
        /// Header background color.
        /// </summary>
        public Color HeaderBackColor { get; set; }

        /// <summary>
        /// Header foreground color.
        /// </summary>
        public Color HeaderForeColor { get; set; }

        #endregion

        #region Rows

        /// <summary>
        /// Alternate row colors.
        /// </summary>
        public bool AlternateRows { get; set; }

        /// <summary>
        /// Alternate row color.
        /// </summary>
        public Color AlternateRowColor { get; set; }

        #endregion

        #region Table

        /// <summary>
        /// Left / Center / Right
        /// </summary>
        public RowAlignment Alignment { get; set; }

        /// <summary>
        /// Columns.
        /// </summary>
        public List<ReportDynamicColumn> Columns { get; set; }

        /// <summary>
        /// Rows.
        /// </summary>
        public List<ReportDynamicRow> Rows { get; set; }

        #endregion

        #region Constructor

        public ReportDynamicTable()
        {
            Title = string.Empty;

            SubTitle = string.Empty;

            ShowTitle = true;

            ShowSubTitle = false;

            SpaceBefore = 0.30;

            SpaceAfter = 0.30;

            ShowBorders = true;

            BorderWidth = 0.50;

            BorderColor = Colors.Gray;

            ShowHeader = true;

            RepeatHeader = true;

            HeaderBackColor = Color.Parse("#1F3A93");

            HeaderForeColor = Colors.White;

            AlternateRows = true;

            AlternateRowColor = Color.Parse("#F5F7FA");

            Alignment = RowAlignment.Left;

            Columns = new List<ReportDynamicColumn>();

            Rows = new List<ReportDynamicRow>();
        }

        #endregion

        #region Helper Methods

        public void AddColumn(ReportDynamicColumn column)
        {
            Columns.Add(column);
        }

        public void AddRow(ReportDynamicRow row)
        {
            Rows.Add(row);
        }

        #endregion
    }
}
