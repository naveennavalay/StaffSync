using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Models
{
    /// <summary>
    /// Represents a dynamic table column.
    /// </summary>
    public class ReportDynamicColumn
    {
        /// <summary>
        /// Column header text displayed in PDF.
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Property name (used when binding from List<T>)
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Column width in centimeters.
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// Display order.
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// Determines whether the column is visible.
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// Horizontal alignment.
        /// </summary>
        public ParagraphAlignment Alignment { get; set; }

        /// <summary>
        /// Text format.
        /// Example:
        /// dd-MMM-yyyy
        /// N2
        /// C2
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Header background color.
        /// </summary>
        public Color HeaderBackColor { get; set; }

        /// <summary>
        /// Header font color.
        /// </summary>
        public Color HeaderForeColor { get; set; }

        /// <summary>
        /// Cell background color.
        /// </summary>
        public Color CellBackColor { get; set; }

        /// <summary>
        /// Cell font color.
        /// </summary>
        public Color CellForeColor { get; set; }

        /// <summary>
        /// Header font size.
        /// </summary>
        public double HeaderFontSize { get; set; }

        /// <summary>
        /// Cell font size.
        /// </summary>
        public double CellFontSize { get; set; }

        /// <summary>
        /// Header font bold.
        /// </summary>
        public bool HeaderBold { get; set; }

        /// <summary>
        /// Cell font bold.
        /// </summary>
        public bool CellBold { get; set; }

        /// <summary>
        /// Enable text wrapping.
        /// </summary>
        public bool WordWrap { get; set; }

        /// <summary>
        /// Allows sorting.
        /// </summary>
        public bool AllowSort { get; set; }

        /// <summary>
        /// Allows grouping.
        /// </summary>
        public bool AllowGroup { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ReportDynamicColumn()
        {
            Header = string.Empty;
            PropertyName = string.Empty;

            Width = 3.0;

            DisplayOrder = 0;

            Visible = true;

            Alignment = ParagraphAlignment.Left;

            Format = string.Empty;

            HeaderBackColor = Color.Parse("#1F3A93");
            HeaderForeColor = Colors.White;

            CellBackColor = Colors.White;
            CellForeColor = Colors.Black;

            HeaderFontSize = 9;
            CellFontSize = 8;

            HeaderBold = true;
            CellBold = false;

            WordWrap = true;

            AllowSort = true;
            AllowGroup = true;
        }
    }
}
