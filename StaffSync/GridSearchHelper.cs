using Krypton.Toolkit;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public class GridSearchHelper
    {
        private KryptonDataGridView _grid;
        private TextBox _searchBox;
        private BindingSource _bindingSource;

        public static void Attach(KryptonDataGridView grid, TextBox searchBox)
        {
            GridSearchHelper helper = new GridSearchHelper();
            helper.Initialize(grid, searchBox);
        }

        private void Initialize(KryptonDataGridView grid, TextBox searchBox)
        {
            _grid = grid;
            _searchBox = searchBox;

            SetupBindingSource();

            _searchBox.TextChanged += SearchBox_TextChanged;
            _grid.CellPainting += Grid_CellPainting;
        }

        private void SetupBindingSource()
        {
            if (_grid.DataSource is BindingSource bs)
            {
                _bindingSource = bs;
            }
            else
            {
                _bindingSource = new BindingSource();
                _bindingSource.DataSource = _grid.DataSource;
                _grid.DataSource = _bindingSource;
            }
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            string search = _searchBox.Text.ToLower();

            if (string.IsNullOrWhiteSpace(search))
            {
                _bindingSource.DataSource = _bindingSource.List;
                _grid.Refresh();
                return;
            }

            var list = _bindingSource.List.Cast<object>().ToList();

            var filtered = list.Where(item =>
            {
                var props = item.GetType().GetProperties();

                return props.Any(p =>
                {
                    var value = p.GetValue(item);
                    return value != null &&
                           value.ToString().ToLower().Contains(search);
                });

            }).ToList();

            _bindingSource.DataSource = filtered;

            _grid.Refresh();
        }

        private void Grid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            string search = _searchBox.Text;

            if (string.IsNullOrWhiteSpace(search))
                return;

            var value = e.FormattedValue?.ToString();

            if (string.IsNullOrEmpty(value))
                return;

            int index = value.IndexOf(search, StringComparison.OrdinalIgnoreCase);

            if (index < 0)
                return;

            e.Handled = true;

            e.PaintBackground(e.CellBounds, true);

            Font font = e.CellStyle.Font;
            Brush textBrush = new SolidBrush(e.CellStyle.ForeColor);

            string before = value.Substring(0, index);
            string match = value.Substring(index, search.Length);
            string after = value.Substring(index + search.Length);

            SizeF beforeSize = e.Graphics.MeasureString(before, font);

            float x = e.CellBounds.Left + 2;
            float y = e.CellBounds.Top + 4;

            e.Graphics.DrawString(before, font, textBrush, x, y);

            x += beforeSize.Width;

            SizeF matchSize = e.Graphics.MeasureString(match, font);

            Rectangle highlightRect = new Rectangle(
                (int)x,
                (int)y,
                (int)matchSize.Width,
                (int)matchSize.Height
            );

            e.Graphics.FillRectangle(Brushes.Yellow, highlightRect);
            e.Graphics.DrawString(match, font, Brushes.Black, x, y);

            x += matchSize.Width;

            e.Graphics.DrawString(after, font, textBrush, x, y);
        }
    }
}
