using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Core
{
    /// <summary>
    /// Global settings used by the Reporting Engine.
    /// Every report will use these settings unless overridden.
    /// </summary>
    public class ReportSettings
    {
        #region Document Settings

        /// <summary>
        /// Page Size (A4, Letter, Legal etc.)
        /// </summary>
        public PageFormat PageFormat { get; set; }

        /// <summary>
        /// Portrait / Landscape
        /// </summary>
        public Orientation Orientation { get; set; }

        /// <summary>
        /// Top Margin
        /// </summary>
        public Unit TopMargin { get; set; }

        /// <summary>
        /// Bottom Margin
        /// </summary>
        public Unit BottomMargin { get; set; }

        /// <summary>
        /// Left Margin
        /// </summary>
        public Unit LeftMargin { get; set; }

        /// <summary>
        /// Right Margin
        /// </summary>
        public Unit RightMargin { get; set; }

        #endregion

        #region Font Settings

        public string DefaultFontName { get; set; }

        public double DefaultFontSize { get; set; }

        public string HeaderFontName { get; set; }

        public double HeaderFontSize { get; set; }

        public string FooterFontName { get; set; }

        public double FooterFontSize { get; set; }

        public string TitleFontName { get; set; }

        public double TitleFontSize { get; set; }

        public bool EmbedFonts { get; set; }

        #endregion

        #region Company Information

        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }

        public string CompanyPhone { get; set; }

        public string CompanyEmail { get; set; }

        public string CompanyWebsite { get; set; }

        public string CompanyGSTNumber { get; set; }

        public string CompanyPANNumber { get; set; }

        public string CompanyLogoPath { get; set; }

        #endregion

        #region Report Information

        public string ApplicationName { get; set; }

        public string ApplicationVersion { get; set; }

        public string ReportTitle { get; set; }

        public string ReportSubTitle { get; set; }

        #endregion

        #region Header Settings

        public bool ShowCompanyLogo { get; set; }

        public bool ShowCompanyName { get; set; }

        public bool ShowCompanyAddress { get; set; }

        public bool ShowReportTitle { get; set; }

        public bool ShowReportSubTitle { get; set; }

        public bool ShowHorizontalLine { get; set; }

        #endregion

        #region Footer Settings

        public bool ShowPageNumber { get; set; }

        public bool ShowGeneratedBy { get; set; }

        public bool ShowGeneratedDate { get; set; }

        public bool ShowApplicationName { get; set; }

        public bool ShowApplicationVersion { get; set; }

        public bool ShowCopyright { get; set; }

        public string CopyrightText { get; set; }

        #endregion

        #region Watermark

        public bool EnableWatermark { get; set; }

        public string WatermarkText { get; set; }

        public int WatermarkOpacity { get; set; }

        #endregion

        #region Table Settings

        public bool AlternateRowColor { get; set; }

        public bool RepeatHeaderOnEveryPage { get; set; }

        public bool ShowGridLines { get; set; }

        public bool AutoFitColumns { get; set; }

        public bool WrapCellText { get; set; }

        #endregion

        #region Output Settings

        public bool AutoOpenPDF { get; set; }

        public bool OverwriteExistingFile { get; set; }

        public bool CreateFolderIfNotExists { get; set; }

        public bool ShowSaveDialog { get; set; }

        public string OutputFolder { get; set; }

        public string DefaultFileName { get; set; }

        #endregion

        #region Security

        public bool AllowPrinting { get; set; }

        public bool AllowCopyContent { get; set; }

        public bool AllowDocumentModification { get; set; }

        public string OwnerPassword { get; set; }

        public string UserPassword { get; set; }

        #endregion

        #region Constructor

        public ReportSettings()
        {
            LoadDefaults();
        }

        #endregion

        #region Default Values

        private void LoadDefaults()
        {
            //-----------------------------
            // Document
            //-----------------------------

            PageFormat = PageFormat.A4;
            Orientation = Orientation.Portrait;

            TopMargin = Unit.FromCentimeter(1.5);
            BottomMargin = Unit.FromCentimeter(1.5);
            LeftMargin = Unit.FromCentimeter(1.5);
            RightMargin = Unit.FromCentimeter(1.5);

            //-----------------------------
            // Fonts
            //-----------------------------

            DefaultFontName = "Calibri";
            DefaultFontSize = 10;

            HeaderFontName = "Calibri";
            HeaderFontSize = 11;

            FooterFontName = "Calibri";
            FooterFontSize = 8;

            TitleFontName = "Calibri";
            TitleFontSize = 18;

            EmbedFonts = false;

            //-----------------------------
            // Company
            //-----------------------------

            CompanyName = "";
            CompanyAddress = "";
            CompanyPhone = "";
            CompanyEmail = "";
            CompanyWebsite = "";
            CompanyGSTNumber = "";
            CompanyPANNumber = "";
            CompanyLogoPath = "";

            //-----------------------------
            // Report
            //-----------------------------

            ApplicationName = "StaffSync";

            ApplicationVersion = "1.0.0";

            ReportTitle = "";

            ReportSubTitle = "";

            //-----------------------------
            // Header
            //-----------------------------

            ShowCompanyLogo = true;

            ShowCompanyName = true;

            ShowCompanyAddress = true;

            ShowReportTitle = true;

            ShowReportSubTitle = true;

            ShowHorizontalLine = true;

            //-----------------------------
            // Footer
            //-----------------------------

            ShowPageNumber = true;

            ShowGeneratedBy = true;

            ShowGeneratedDate = true;

            ShowApplicationName = true;

            ShowApplicationVersion = true;

            ShowCopyright = true;

            CopyrightText = "© StaffSync";

            //-----------------------------
            // Watermark
            //-----------------------------

            EnableWatermark = false;

            WatermarkText = "";

            WatermarkOpacity = 20;

            //-----------------------------
            // Table
            //-----------------------------

            AlternateRowColor = true;

            RepeatHeaderOnEveryPage = true;

            ShowGridLines = true;

            AutoFitColumns = true;

            WrapCellText = true;

            //-----------------------------
            // Output
            //-----------------------------

            AutoOpenPDF = true;

            OverwriteExistingFile = true;

            CreateFolderIfNotExists = true;

            ShowSaveDialog = false;

            OutputFolder = "";

            DefaultFileName = "";

            //-----------------------------
            // Security
            //-----------------------------

            AllowPrinting = true;

            AllowCopyContent = true;

            AllowDocumentModification = false;

            OwnerPassword = "";

            UserPassword = "";
        }

        #endregion
    }
}
