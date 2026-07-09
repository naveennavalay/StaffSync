using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Core
{
    public class ReportDocument
    {
        public Document Document { get; } = new Document();
        public Section CreatePage()
        {
            return Document.AddSection();
        }
    }
}
