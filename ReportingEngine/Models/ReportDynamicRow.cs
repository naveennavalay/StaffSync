using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Models
{
    /// <summary>
    /// Represents a row in a dynamic report table.
    /// </summary>
    public class ReportDynamicRow
    {
        /// <summary>
        /// Cell values.
        /// Number of values should match the number of columns.
        /// </summary>
        public List<object> Cells { get; set; }

        /// <summary>
        /// Determines whether the row is visible.
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// Row height (0 = Auto).
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// Background color for the row.
        /// Leave Empty to use table settings.
        /// </summary>
        public Color BackColor { get; set; }

        /// <summary>
        /// Foreground (text) color.
        /// </summary>
        public Color ForeColor { get; set; }

        /// <summary>
        /// Makes the entire row bold.
        /// </summary>
        public bool Bold { get; set; }

        /// <summary>
        /// Horizontal alignment for the entire row.
        /// Individual column alignment will override this.
        /// </summary>
        public ParagraphAlignment Alignment { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ReportDynamicRow()
        {
            Cells = new List<object>();

            Visible = true;

            Height = 0;

            BackColor = Colors.Transparent;
            ForeColor = Colors.Black;

            Bold = false;

            Alignment = ParagraphAlignment.Left;
        }

        /// <summary>
        /// Adds a cell value.
        /// </summary>
        public void Add(object value)
        {
            Cells.Add(value);
        }
    }
}
