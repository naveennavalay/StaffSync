using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Helpers
{
    public static class ImageHelper
    {
        public static void AddImage(Section section, string path, double widthCm)
        {
            var image = section.AddImage(path);

            image.Width = Unit.FromCentimeter(widthCm);
        }
    }
}
