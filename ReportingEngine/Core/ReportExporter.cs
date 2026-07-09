using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MigraDoc.Rendering;

namespace ReportingEngine.Core
{
    public class ReportExporter
    {
        public void Export(ReportDocumentBuilder builder, string file, bool openAfterExport = true)
        {
            PdfDocumentRenderer r = new PdfDocumentRenderer(true);
            r.Document = builder.Document;
            r.RenderDocument();
            r.PdfDocument.Save(file);
            if (openAfterExport) Process.Start(file);
        }
    }
}
