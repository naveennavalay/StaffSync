using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public partial class BadgeControl : UserControl
    {
        private int _value = 0;
        private readonly ToolTip _toolTip = new ToolTip();
        private bool _isHovered = false;

        #region Public Properties

        [Browsable(true)]
        [Category("KPI")]
        public int Value
        {
            get => _value;
            set
            {
                _value = value;
                _toolTip.SetToolTip(this, _value.ToString("N0"));
                Invalidate();
            }
        }

        [Browsable(true)]
        [Category("KPI")]
        public Color CircleBackColor { get; set; } = Color.FromArgb(31, 78, 121);

        [Browsable(true)]
        [Category("KPI")]
        public Color HoverBackColor { get; set; } = Color.FromArgb(41, 98, 150);

        [Browsable(true)]
        [Category("KPI")]
        public Color BorderColor { get; set; } = Color.Transparent;

        [Browsable(true)]
        [Category("KPI")]
        public int BorderThickness { get; set; } = 0;

        [Browsable(true)]
        [Category("KPI")]
        public Color TextColor { get; set; } = Color.White;

        #endregion

        public BadgeControl()
        {
            DoubleBuffered = true;
            Size = new Size(140, 140);
            Font = new Font("Segoe UI", 18, FontStyle.Bold);
            Cursor = Cursors.Hand;
            _toolTip.SetToolTip(this, "0");
        }

        #region Mouse Events

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovered = false;
            Invalidate();
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            // Tooltip already set in Value property
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            // You can handle externally from Form
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            // You can handle externally from Form
        }

        #endregion

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (Width != Height)
                Width = Height;

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int diameter = Math.Min(Width, Height) - BorderThickness * 2;

            Rectangle rect = new Rectangle(
                BorderThickness,
                BorderThickness,
                diameter,
                diameter);

            // Choose hover color if hovered
            Color backColorToUse = _isHovered ? HoverBackColor : CircleBackColor;

            using (SolidBrush brush = new SolidBrush(backColorToUse))
            {
                e.Graphics.FillEllipse(brush, rect);
            }

            if (BorderThickness > 0)
            {
                using (Pen pen = new Pen(BorderColor, BorderThickness))
                {
                    e.Graphics.DrawEllipse(pen, rect);
                }
            }

            string text = FormatValue(Value);

            float fontSize = diameter / 3f;
            Font dynamicFont;

            while (true)
            {
                dynamicFont = new Font(Font.FontFamily, fontSize, FontStyle.Bold);
                SizeF textSize = e.Graphics.MeasureString(text, dynamicFont);

                if (textSize.Width <= diameter * 0.8f &&
                    textSize.Height <= diameter * 0.8f)
                    break;

                fontSize -= 1;

                if (fontSize <= 6)
                    break;
            }

            SizeF finalSize = e.Graphics.MeasureString(text, dynamicFont);

            float x = rect.X + (rect.Width - finalSize.Width) / 2;
            float y = rect.Y + (rect.Height - finalSize.Height) / 2;

            using (SolidBrush textBrush = new SolidBrush(TextColor))
            {
                e.Graphics.DrawString(text, dynamicFont, textBrush, x, y);
            }

            dynamicFont.Dispose();
        }

        #region Helper

        private string FormatValue(int number)
        {
            if (number >= 1000000)
                return (number / 1000000d).ToString("0.#") + "M";

            if (number >= 1000)
                return (number / 1000d).ToString("0.#") + "K";

            return number.ToString();
        }

        #endregion
    }
}
