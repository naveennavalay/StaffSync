using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace StaffSync.Controls
{
    // Represents a single changed cell entry
    public class ChangedCell
    {
        public int RowIndex { get; set; }
        public string RowKey { get; set; } // value from key column (or first column)
        public int ColumnIndex { get; set; }
        public string ColumnName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }

    // Custom EventArgs for instant notifications
    public class CellValueChangedEventArgs : EventArgs
    {
        public ChangedCell Change { get; }
        public CellValueChangedEventArgs(ChangedCell change) => Change = change;
    }

    /// <summary>
    /// Attendance grid UserControl with:
    /// - Public API: AddColumns, AddDayColumns, AddRow
    /// - Dropdowns hidden until double-click (ComboBox DisplayStyle = Nothing)
    /// - Edit only on double-click
    /// - Immediate change event via CurrentCellDirtyStateChanged + CellValueChanged
    /// - Tracks changed cells; GetChangedCells to retrieve
    /// - Disables future dates based on DisplayMonth/DisplayYear
    /// </summary>
    public partial class AttendanceGridControl : UserControl
    {
        private DataGridView dgv;

        private readonly List<ChangedCell> _changed = new List<ChangedCell>();
        private string _oldValue = null;

        [Category("Custom Properties"), Description("Month number (1-12) that the grid represents.")]
        public int DisplayMonth { get; set; } = DateTime.Today.Month;

        [Category("Custom Properties"), Description("Year that the grid represents.")]
        public int DisplayYear { get; set; } = DateTime.Today.Year;

        [Category("Custom Properties"), Description("Number of fixed non-day columns at the start (e.g., SlNo, EmpCode, EmpName). Used to skip validations/formatting for day columns.")]
        public int FixedColumnCount { get; set; } = 3;

        [Category("Custom Properties"), Description("Optional name of the column to use as row key. If empty or not found, the first column is used.")]
        public string KeyColumnName { get; set; } = string.Empty;

        // Fired whenever a user changes a cell value (committed immediately for ComboBox cells)
        public event EventHandler<CellValueChangedEventArgs> CellValueChangedCustom;

        public AttendanceGridControl()
        {
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells,
                SelectionMode = DataGridViewSelectionMode.CellSelect,
                EditMode = DataGridViewEditMode.EditProgrammatically // only edit when we say so
            };

            // Event wiring
            dgv.CellFormatting += Dgv_CellFormatting;                  // gray out future cells
            dgv.CellDoubleClick += Dgv_CellDoubleClick;                // start editing only on double-click
            dgv.CellBeginEdit += Dgv_CellBeginEdit;                    // capture old value + block future dates
            dgv.CellValueChanged += Dgv_CellValueChanged;              // record & raise custom event
            dgv.CurrentCellDirtyStateChanged += Dgv_CurrentCellDirtyStateChanged; // commit ComboBox immediately

            Controls.Add(dgv);
        }

        // ===============================
        // PUBLIC API
        // ===============================

        /// <summary>
        /// Adds the fixed (non-day) columns to the grid. This method does NOT add day columns.
        /// Use AddDayColumns(...) after this.
        /// </summary>
        public void AddColumns(params string[] columnHeaders)
        {
            dgv.Columns.Clear();
            if (columnHeaders == null || columnHeaders.Length == 0) return;

            foreach (var header in columnHeaders)
            {
                var safeName = MakeSafeName(header);
                dgv.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = safeName,
                    HeaderText = header,
                    ReadOnly = false
                });
            }
        }

        /// <summary>
        /// Adds day columns 1..daysInMonth as ComboBox columns with the given options (default P/L/A).
        /// Dropdown arrow is hidden until the cell enters edit mode (double-click).
        /// </summary>
        public void AddDayColumns(int daysInMonth, IEnumerable<string> options = null)
        {
            if (daysInMonth < 1 || daysInMonth > 31) throw new ArgumentOutOfRangeException(nameof(daysInMonth));
            var opts = (options?.ToArray() ?? new[] { "P", "L", "A" });

            for (int day = 1; day <= daysInMonth; day++)
            {
                var col = new DataGridViewComboBoxColumn
                {
                    HeaderText = day.ToString(),
                    Name = $"Day{day}",
                    DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing, // hide dropdown until edit
                    FlatStyle = FlatStyle.Flat
                };
                col.Items.AddRange(opts);
                dgv.Columns.Add(col);
            }
        }

        /// <summary>
        /// Adds a row. Pass values in the order of the columns you created (fixed columns first, then days).
        /// If fewer values are provided than columns, remaining cells stay empty.
        /// </summary>
        public void AddRow(params string[] values)
        {
            dgv.Rows.Add(values);
        }

        /// <summary>
        /// Returns a copy of the list of changed cells since the last clear. Use ClearChanges() to reset.
        /// </summary>
        public List<ChangedCell> GetChangedCells() => new List<ChangedCell>(_changed);

        /// <summary>
        /// Clears the internal list of tracked changes.
        /// </summary>
        public void ClearChanges() => _changed.Clear();

        // ===============================
        // INTERNAL HELPERS
        // ===============================

        private static string MakeSafeName(string header)
        {
            if (string.IsNullOrWhiteSpace(header)) return "Column" + Guid.NewGuid().ToString("N");
            // Remove spaces and non-alphanumerics for a safe internal name
            var chars = header.Where(char.IsLetterOrDigit).ToArray();
            var name = new string(chars);
            if (string.IsNullOrEmpty(name)) name = "Col" + Guid.NewGuid().ToString("N");
            return name;
        }

        private string GetRowKey(DataGridViewRow row)
        {
            if (!string.IsNullOrWhiteSpace(KeyColumnName) && dgv.Columns.Contains(KeyColumnName))
                return row.Cells[KeyColumnName]?.Value?.ToString();

            // Fallback to first column value
            return row.Cells.Count > 0 ? row.Cells[0]?.Value?.ToString() : null;
        }

        private static bool TryGetDayFromHeader(DataGridViewColumn col, out int day)
        {
            return int.TryParse(col?.HeaderText, out day);
        }

        private DateTime MakeCellDate(int day)
        {
            // Guard: if day is out of range for the configured month, clamp
            int maxDay = DateTime.DaysInMonth(DisplayYear, DisplayMonth);
            int safeDay = Math.Max(1, Math.Min(day, maxDay));
            return new DateTime(DisplayYear, DisplayMonth, safeDay);
        }

        // ===============================
        // EVENT HANDLERS
        // ===============================

        private void Dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // Begin edit explicitly on double click
            dgv.BeginEdit(true);
        }

        private void Dgv_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            // For ComboBox cells, commit as soon as selection changes so CellValueChanged fires immediately
            if (dgv.IsCurrentCellDirty)
            {
                dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void Dgv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var col = dgv.Columns[e.ColumnIndex];
            var cell = dgv[e.ColumnIndex, e.RowIndex];
            _oldValue = cell?.Value?.ToString();

            // Block future dates for day columns only
            if (e.ColumnIndex >= FixedColumnCount && TryGetDayFromHeader(col, out int day))
            {
                var today = DateTime.Today;
                DateTime cellDate = MakeCellDate(day);
                if (cellDate > today)
                {
                    e.Cancel = true; // prevent editing future dates
                }
            }
        }

        private void Dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var col = dgv.Columns[e.ColumnIndex];
            var cell = dgv[e.ColumnIndex, e.RowIndex];

            string newValue = cell?.Value?.ToString();
            if (_oldValue == newValue) return; // no change

            var row = dgv.Rows[e.RowIndex];
            var change = new ChangedCell
            {
                RowIndex = e.RowIndex,
                RowKey = GetRowKey(row),
                ColumnIndex = e.ColumnIndex,
                ColumnName = col.Name,
                OldValue = _oldValue,
                NewValue = newValue
            };

            _changed.Add(change);
            CellValueChangedCustom?.Invoke(this, new CellValueChangedEventArgs(change));
        }

        private void Dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (e.ColumnIndex < FixedColumnCount) return; // skip fixed columns

            var col = dgv.Columns[e.ColumnIndex];
            if (!TryGetDayFromHeader(col, out int day)) return;

            var today = DateTime.Today;
            DateTime cellDate = MakeCellDate(day);
            if (cellDate > today)
            {
                e.CellStyle.BackColor = Color.LightGray;
                e.CellStyle.ForeColor = Color.DarkGray;
            }
        }
    }
}
