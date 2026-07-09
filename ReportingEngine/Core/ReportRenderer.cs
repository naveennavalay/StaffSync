using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Core
{
    public class ReportRenderer
    {
        public void Render(Document document, string outputFile)
        {
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true);

            renderer.Document = document;

            renderer.RenderDocument();

            renderer.PdfDocument.Save(outputFile);
        }
    }
}
