using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes;
using ReportingEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Builders
{
    public class WatermarkBuilder
    {
        private readonly DocumentContext _context;

        public WatermarkBuilder(DocumentContext context)
        {
            _context = context;
        }

        public void Build(Section section)
        {
            if (!_context.DisplayOptions.ShowWatermark)
                return;

            TextFrame frame = section.AddTextFrame();

            frame.RelativeHorizontal = RelativeHorizontal.Page;
            frame.RelativeVertical = RelativeVertical.Page;

            frame.Left = ShapePosition.Center;
            frame.Top = ShapePosition.Center;

            frame.Width = Unit.FromCentimeter(20);
            frame.Height = Unit.FromCentimeter(5);

            Paragraph p = frame.AddParagraph();

            p.Format.Alignment = ParagraphAlignment.Center;

            p.Format.Font.Size = _context.DisplayOptions.WatermarkFontSize;
            p.Format.Font.Bold = true;
            p.Format.Font.Italic = true;

            p.Format.Font.Color =
                Color.Parse(_context.DisplayOptions.WatermarkColorHex);

            p.AddText(_context.DisplayOptions.WatermarkText);
        }
    }
}
