using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
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

    // Design-time enums
    public enum DayNameFormat { None, Short, Full }
    //public enum LegendPosition { None, Bottom, Right }
    public enum DateHeaderAlignment { Left, Center, Right }
    public enum GridLinesMode { None, Horizontal, Vertical, Both }
    public enum CellTextAlignment { Left, Center, Right }

    /// <summary>
    /// Attendance grid UserControl with:
    /// - Public API: AddColumns, AddDayColumns, AddRow
    /// - Dropdowns hidden until double-click (ComboBox DisplayStyle = Nothing)
    /// - Edit only on double-click
    /// - Immediate change event via CurrentCellDirtyStateChanged + CellValueChanged
    /// - Tracks changed cells; GetChangedCells / ClearChanges
    /// - Disables future dates based on DisplayMonth/DisplayYear and AllowFutureDates
    /// - WeeklyOffs highlighting/optional read-only
    /// - Day header formatting via DayNameFormat and DateHeaderAlignment
    /// - Header styling & grid lines customization
    /// </summary>
    public partial class AttendanceGridControl : UserControl
    {
        private DataGridView dgv;

        private readonly List<ChangedCell> _changed = new List<ChangedCell>();
        private string _oldValue = null;

        // ===============================
        // DESIGN/INIT-TIME PROPERTIES
        // ===============================

        [Category("Date"), Description("Month number (1-12) that the grid represents.")]
        public int DisplayMonth { get; set; } = DateTime.Today.Month;

        [Category("Date"), Description("Year that the grid represents.")]
        public int DisplayYear { get; set; } = DateTime.Today.Year;

        [Category("Behavior"), Description("If false, cells for future dates are read-only and greyed.")]
        public bool AllowFutureDates { get; set; } = false;

        [Category("Behavior"), Description("Number of fixed non-day columns at the start (e.g., SlNo, EmpCode, EmpName). Used to skip validations/formatting for day columns.")]
        public int FixedColumnCount { get; set; } = 3;

        [Category("Behavior"), Description("Optional name of the column to use as row key. If empty or not found, the first column is used.")]
        public string KeyColumnName { get; set; } = string.Empty;

        [Category("Behavior"), Description("Allowed options in day ComboBox cells. If AddDayColumns options arg is null, this list is used.")]
        public string[] OptionsList { get; set; } = new[] { "P", "L", "A" };

        [Category("Behavior"), Description("Days of week to treat as weekly off (highlighted).")]
        public DayOfWeek[] WeeklyOffs { get; set; } = Array.Empty<DayOfWeek>();

        [Category("Behavior"), Description("If true, weekly off cells are read-only as well as highlighted.")]
        public bool WeeklyOffsAreReadOnly { get; set; } = false;

        [Category("Appearance"), Description("How day headers show day names.")]
        public DayNameFormat DayNameFormat { get; set; } = DayNameFormat.None;

        [Category("Appearance"), Description("Align day header text.")]
        public DateHeaderAlignment DateHeaderAlignment { get; set; } = DateHeaderAlignment.Center;

        //[Category("Appearance"), Description("Legend position (not rendered yet).")]
        //public LegendPosition LegendPosition { get; set; } = LegendPosition.None;

        [Category("Appearance"), Description("Header background color.")]
        public Color HeaderBackColor { get; set; } = Color.Gainsboro;

        [Category("Appearance"), Description("Header text color.")]
        public Color HeaderForeColor { get; set; } = Color.Black;

        [Category("Appearance"), Description("Grid lines mode.")]
        public GridLinesMode GridLines { get; set; } = GridLinesMode.Both;

        private CellTextAlignment _cellTextAlignment = CellTextAlignment.Center;

        [Category("Appearance"), Description("Color for weekly off cells.")]
        public Color WeeklyOffBackColor { get; set; } = Color.AliceBlue;

        [Category("Appearance"), Description("Color for future cells when AllowFutureDates = false.")]
        public Color FutureBackColor { get; set; } = Color.LightGray;

        [Category("Appearance"), Description("Text color for disabled/future cells.")]
        public Color DisabledForeColor { get; set; } = Color.DarkGray;

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

            // Header style
            dgv.EnableHeadersVisualStyles = false;
            ApplyHeaderStyle();
            ApplyGridLines();

            // Event wiring
            dgv.CellFormatting += Dgv_CellFormatting;                  // gray out future cells & highlight weekly offs
            dgv.CellDoubleClick += Dgv_CellDoubleClick;                // start editing only on double-click
            dgv.CellBeginEdit += Dgv_CellBeginEdit;                    // capture old value + block future/weekly off
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
        /// Adds day columns 1..daysInMonth as ComboBox columns with the given options (default from OptionsList).
        /// Dropdown arrow is hidden until the cell enters edit mode (double-click).
        /// Header text respects DayNameFormat and DateHeaderAlignment.
        /// Column Name is always "Day#" so internal logic can resolve the date even if header text changes.
        /// </summary>
        public void AddDayColumns(int daysInMonth, IEnumerable<string> options = null)
        {
            if (daysInMonth < 1 || daysInMonth > 31) throw new ArgumentOutOfRangeException(nameof(daysInMonth));
            var opts = (options?.ToArray() ?? OptionsList?.ToArray() ?? new[] { "P", "L", "A" });

            for (int day = 1; day <= daysInMonth; day++)
            {
                string header = BuildDayHeaderText(day);
                var col = new DataGridViewComboBoxColumn
                {
                    HeaderText = header,
                    Name = $"Day{day}",
                    DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing, // hide dropdown until edit
                    FlatStyle = FlatStyle.Flat
                };
                col.Items.AddRange(opts);
                dgv.Columns.Add(col);
            }

            // apply header alignment after adding
            switch (DateHeaderAlignment)
            {
                case DateHeaderAlignment.Left:
                    dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft; break;
                case DateHeaderAlignment.Center:
                    dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; break;
                case DateHeaderAlignment.Right:
                    dgv.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight; break;
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

        /// <summary>
        /// Helper to update header style at runtime if you tweak properties.
        /// </summary>
        public void ApplyHeaderStyle()
        {
            if (dgv == null) return;
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColor;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = HeaderForeColor;
        }

        /// <summary>
        /// Helper to apply grid line mode & color.
        /// </summary>
        public void ApplyGridLines()
        {
            if (dgv == null) return;
            dgv.GridColor = Color.Silver;
            switch (GridLines)
            {
                case GridLinesMode.None:
                    dgv.CellBorderStyle = DataGridViewCellBorderStyle.None; break;
                case GridLinesMode.Horizontal:
                    dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; break;
                case GridLinesMode.Vertical:
                    dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical; break;
                case GridLinesMode.Both:
                default:
                    dgv.CellBorderStyle = DataGridViewCellBorderStyle.Single; break;
            }
        }

        [Category("Appearance")]
        public CellTextAlignment CellTextAlignment
        {
            get => _cellTextAlignment;
            set
            {
                _cellTextAlignment = value;
                ApplyCellTextAlignment();
            }
        }

        private void ApplyCellTextAlignment()
        {
            DataGridViewContentAlignment align = DataGridViewContentAlignment.MiddleCenter;
            switch (_cellTextAlignment)
            {
                case CellTextAlignment.Left:
                    align = DataGridViewContentAlignment.MiddleLeft;
                    break;
                case CellTextAlignment.Center:
                    align = DataGridViewContentAlignment.MiddleCenter;
                    break;
                case CellTextAlignment.Right:
                    align = DataGridViewContentAlignment.MiddleRight;
                    break;
            }

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                col.DefaultCellStyle.Alignment = align;
            }
        }

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

        private static bool TryGetDayFromColumn(DataGridViewColumn col, out int day)
        {
            day = -1;
            if (col == null || string.IsNullOrEmpty(col.Name)) return false;
            if (col.Name.StartsWith("Day", StringComparison.OrdinalIgnoreCase))
            {
                var num = col.Name.Substring(3);
                return int.TryParse(num, out day);
            }
            return false;
        }

        private DateTime MakeCellDate(int day)
        {
            int maxDay = DateTime.DaysInMonth(DisplayYear, DisplayMonth);
            int safeDay = Math.Max(1, Math.Min(day, maxDay));
            return new DateTime(DisplayYear, DisplayMonth, safeDay);
        }

        private string BuildDayHeaderText(int day)
        {
            var date = MakeCellDate(day);
            string dayHeader;
            switch (DayNameFormat)
            {
                case DayNameFormat.Short:
                    dayHeader = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(date.DayOfWeek)}-{day}";
                    break;
                case DayNameFormat.Full:
                    dayHeader = $"{CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(date.DayOfWeek)}-{day}";
                    break;
                default:
                    dayHeader = day.ToString();
                    break;
            }
            return dayHeader;
        }

        private bool IsWeeklyOff(DateTime date)
        {
            if (WeeklyOffs == null || WeeklyOffs.Length == 0) return false;
            return WeeklyOffs.Contains(date.DayOfWeek);
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

            // Block future dates / weekly offs for day columns only
            if (e.ColumnIndex >= FixedColumnCount && TryGetDayFromColumn(col, out int day))
            {
                var today = DateTime.Today;
                DateTime cellDate = MakeCellDate(day);

                if (!AllowFutureDates && cellDate > today)
                {
                    e.Cancel = true; // prevent editing future dates
                    return;
                }

                if (WeeklyOffsAreReadOnly && IsWeeklyOff(cellDate))
                {
                    e.Cancel = true; // prevent editing weekly offs if configured
                    return;
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
            if (!TryGetDayFromColumn(col, out int day)) return;

            DateTime cellDate = MakeCellDate(day);
            var today = DateTime.Today;

            // Weekly off styling
            if (IsWeeklyOff(cellDate))
            {
                e.CellStyle.BackColor = WeeklyOffBackColor;
            }

            // Future date styling & disabling tint
            if (!AllowFutureDates && cellDate > today)
            {
                e.CellStyle.BackColor = FutureBackColor;
                e.CellStyle.ForeColor = DisabledForeColor;
            }
        }

        public void SetColumnAlignment(string columnName, CellTextAlignment alignment)
        {
            if (dgv.Columns.Contains(columnName))
            {
                DataGridViewContentAlignment cellAlign = DataGridViewContentAlignment.MiddleCenter;
                DataGridViewContentAlignment headerAlign = DataGridViewContentAlignment.MiddleCenter;

                switch (alignment)
                {
                    case CellTextAlignment.Left:
                        cellAlign = DataGridViewContentAlignment.TopLeft;
                        headerAlign = DataGridViewContentAlignment.MiddleLeft;
                        break;
                    case CellTextAlignment.Center:
                        cellAlign = DataGridViewContentAlignment.MiddleCenter;
                        headerAlign = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case CellTextAlignment.Right:
                        cellAlign = DataGridViewContentAlignment.MiddleRight;
                        headerAlign = DataGridViewContentAlignment.MiddleRight;
                        break;
                }

                // Apply to entire column
                var col = dgv.Columns[columnName];
                col.DefaultCellStyle.Alignment = cellAlign;
                col.HeaderCell.Style.Alignment = headerAlign;

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.Cells[columnName] != null)
                    {
                        row.Cells[columnName].Style.Alignment = cellAlign;
                    }
                }

                //dgv.Refresh();
            }
        }

        /// <summary>
        /// Try to resolve a column using: exact HeaderText match, exact Name match,
        /// or DayN style match if the identifier is numeric (e.g. "1" -> Day1).
        /// </summary>
        private DataGridViewColumn FindColumnByIdentifier(string columnIdentifier)
        {
            if (string.IsNullOrEmpty(columnIdentifier) || dgv == null) return null;

            // 1) direct header text match
            var col = dgv.Columns.Cast<DataGridViewColumn>()
                         .FirstOrDefault(c => string.Equals(c.HeaderText, columnIdentifier, StringComparison.Ordinal));
            if (col != null) return col;

            // 2) direct internal name match
            col = dgv.Columns.Cast<DataGridViewColumn>()
                     .FirstOrDefault(c => string.Equals(c.Name, columnIdentifier, StringComparison.Ordinal));
            if (col != null) return col;

            // 3) if numeric, try Day# name or header as number
            if (int.TryParse(columnIdentifier, out int day))
            {
                col = dgv.Columns.Cast<DataGridViewColumn>()
                         .FirstOrDefault(c =>
                            string.Equals(c.Name, $"Day{day}", StringComparison.OrdinalIgnoreCase)
                            || string.Equals(c.HeaderText, day.ToString(), StringComparison.Ordinal));
                if (col != null) return col;
            }

            // not found
            return null;
        }

        /// <summary>
        /// Makes a specific column editable or non-editable.
        /// Works with HeaderText, internal Name, or Day number (e.g. "1" or "Day1").
        /// </summary>
        public void SetColumnEditable(string columnIdentifier, bool editable)
        {
            var col = FindColumnByIdentifier(columnIdentifier);
            if (col == null) return;

            col.ReadOnly = !editable;

            // Apply to existing cells too
            foreach (DataGridViewRow row in dgv.Rows)
            {
                var cell = row.Cells[col.Index];
                if (cell != null)
                {
                    cell.ReadOnly = !editable;
                }
            }

            dgv.Refresh();
        }

        /// <summary>
        /// Overload by numeric index (0-based).
        /// </summary>
        public void SetColumnEditable(int columnIndex, bool editable)
        {
            if (dgv == null) return;
            if (columnIndex < 0 || columnIndex >= dgv.Columns.Count) return;

            var col = dgv.Columns[columnIndex];
            col.ReadOnly = !editable;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                var cell = row.Cells[columnIndex];
                if (cell != null)
                {
                    cell.ReadOnly = !editable;
                }
            }

            dgv.Refresh();
        }
    }
}
