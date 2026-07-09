using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Base
{
    /// <summary>
    /// Base class for all report headers.
    /// </summary>
    public abstract class ReportHeaderBase
    {
        protected ReportingEngine.Core.ReportContext Context { get; }
        protected ReportingEngine.Core.ReportContext Settings { get; }

        protected ReportHeaderBase(
            ReportingEngine.Core.ReportContext context,
            ReportingEngine.Core.ReportContext settings)
        {
            Context = context;
            Settings = settings;
        }

        /// <summary>
        /// Implement header rendering.
        /// </summary>
        public abstract void Build(Section section);

        protected void AddTitle(Section section, string title)
        {
            var p = section.AddParagraph(title);
            p.Format.Font.Bold = true;
            p.Format.Font.Size = 18;
            p.Format.SpaceAfter = Unit.FromCentimeter(0.30);
        }

        protected void AddSubTitle(Section section, string text)
        {
            var p = section.AddParagraph(text);
            p.Format.Font.Size = 10;
            p.Format.SpaceAfter = Unit.FromCentimeter(0.20);
        }
    }
}
