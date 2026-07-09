using MigraDoc.DocumentObjectModel;
using ReportingEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Base
{
    public abstract class ReportBase
    {
        protected readonly ReportContext Context;
        protected readonly ReportSettings Settings;
        protected readonly Document Document;

        protected ReportBase(ReportContext context, ReportSettings settings)
        {
            Context = context;
            Settings = settings;
            Document = new Document();
        }

        public Document Build()
        {
            OnBuild();
            return Document;
        }

        protected abstract void OnBuild();
    }
}
