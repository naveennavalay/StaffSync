using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Builders
{
    public class WatermarkBuilder
    {
        public void Build(Section section, string text)
        {
            var p = section.Headers.Primary.AddParagraph();
            p.AddText(text);
            p.Format.Font.Size = 40;
            p.Format.Font.Color = Colors.LightGray;
            p.Format.Alignment = ParagraphAlignment.Center;
        }
    }
}
