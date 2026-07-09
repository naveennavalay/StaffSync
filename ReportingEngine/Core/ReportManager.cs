using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Core
{
    public class ReportManager
    {
        private readonly ReportValidator _validator = new ReportValidator();
        private readonly ReportRenderer _renderer = new ReportRenderer();

        public void Generate(Document document, string outputFile)
        {
            _validator.Validate(document);
            _renderer.Render(document, outputFile);
        }
    }
}
