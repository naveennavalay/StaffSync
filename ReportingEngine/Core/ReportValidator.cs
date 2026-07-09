using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Core
{
    public class ReportValidator
    {
        public void Validate(Document document)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            if (document.Sections.Count == 0)
                throw new InvalidOperationException("Document contains no sections.");
        }
    }
}
