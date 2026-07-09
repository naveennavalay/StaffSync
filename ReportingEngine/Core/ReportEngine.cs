using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportingEngine.Core;

namespace ReportingEngine.Core
{
    public class ReportEngine
    {
        public ReportSettings Settings { get; private set; }
        public ReportContext Context { get; private set; }
        public ReportDocumentBuilder Builder { get; private set; }
        public void Initialize(ReportSettings settings, ReportContext context)
        {
            Settings = settings; Context = context;
            Builder = new ReportDocumentBuilder();
            Builder.Initialize(settings, context);
        }
        public void Generate() { }
    }
}
