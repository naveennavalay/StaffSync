using MigraDoc.DocumentObjectModel;
using ReportingEngine.Styles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Core
{
    public class ReportDocumentBuilder
    {
        public ReportSettings Settings { get; private set; }
        public ReportContext Context { get; private set; }
        public Document Document { get; private set; }
        public Section Section { get; private set; }
        public void Initialize(ReportSettings settings, ReportContext context)
        {
            Settings = settings; Context = context;
            Document = new Document();
            ReportStyleManager.ApplyDocumentStyle(Document);
            Document.DefaultPageSetup.PageFormat = Settings.PageFormat;
            Document.DefaultPageSetup.Orientation = Settings.Orientation;
            Document.DefaultPageSetup.TopMargin = Settings.TopMargin;
            Document.DefaultPageSetup.BottomMargin = Settings.BottomMargin;
            Document.DefaultPageSetup.LeftMargin = Settings.LeftMargin;
            Document.DefaultPageSetup.RightMargin = Settings.RightMargin;
            Section = Document.AddSection();
            Document.Info.Title = Context.ReportTitle;
            Document.Info.Subject = Context.ReportSubTitle;
            Document.Info.Author = Context.GeneratedBy;
            Document.Info.Keywords = "StaffSync Reporting Engine";
        }
    }
}
