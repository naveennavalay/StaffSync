using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MigraDoc.DocumentObjectModel;

namespace ReportingEngine.Base
{
    public abstract class ReportFooterBase
    {
        public abstract void Build(Section section);
    }
}
