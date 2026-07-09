using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Helpers
{
    /// <summary>
    /// Provides common file related utility methods used by the Reporting Engine.
    /// </summary>
    public static class FileHelper
    {
        #region Folder Methods

        /// <summary>
        /// Creates a folder if it does not exist.
        /// </summary>
        public static void CreateFolder(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath))
                return;

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
        }

        /// <summary>
        /// Checks whether a folder exists.
        /// </summary>
        public static bool FolderExists(string folderPath)
        {
            return Directory.Exists(folderPath);
        }

        #endregion

        #region File Methods

        /// <summary>
        /// Checks whether a file exists.
        /// </summary>
        public static bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }

        /// <summary>
        /// Deletes a file.
        /// </summary>
        public static bool DeleteFile(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                    File.Delete(fileName);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Copies a file.
        /// </summary>
        public static void CopyFile(string sourceFile, string destinationFile, bool overwrite = true)
        {
            File.Copy(sourceFile, destinationFile, overwrite);
        }

        /// <summary>
        /// Moves a file.
        /// </summary>
        public static void MoveFile(string sourceFile, string destinationFile)
        {
            File.Move(sourceFile, destinationFile);
        }

        #endregion

        #region File Name

        /// <summary>
        /// Returns a unique file name.
        /// </summary>
        public static string GetUniqueFileName(string folderPath, string prefix, string extension)
        {
            CreateFolder(folderPath);

            string fileName =
                $"{prefix}_{DateTime.Now:yyyyMMdd_HHmmss}.{extension.Trim('.')}";

            return Path.Combine(folderPath, fileName);
        }

        /// <summary>
        /// Returns report file name.
        /// </summary>
        public static string GetReportFileName(string reportName)
        {
            return $"{reportName}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
        }

        #endregion

        #region Path

        /// <summary>
        /// Combines path.
        /// </summary>
        public static string Combine(params string[] paths)
        {
            return Path.Combine(paths);
        }

        /// <summary>
        /// Gets file extension.
        /// </summary>
        public static string GetExtension(string fileName)
        {
            return Path.GetExtension(fileName);
        }

        /// <summary>
        /// Gets file name.
        /// </summary>
        public static string GetFileName(string fileName)
        {
            return Path.GetFileName(fileName);
        }

        /// <summary>
        /// Gets file name without extension.
        /// </summary>
        public static string GetFileNameWithoutExtension(string fileName)
        {
            return Path.GetFileNameWithoutExtension(fileName);
        }

        #endregion

        #region PDF

        /// <summary>
        /// Opens generated PDF.
        /// </summary>
        public static void OpenFile(string fileName)
        {
            if (!File.Exists(fileName))
                return;

            Process.Start(new ProcessStartInfo
            {
                FileName = fileName,
                UseShellExecute = true
            });
        }

        #endregion

        #region Temp

        /// <summary>
        /// Returns Temp Folder.
        /// </summary>
        public static string GetTempFolder()
        {
            return Path.GetTempPath();
        }

        /// <summary>
        /// Clears temp PDF files.
        /// </summary>
        public static void ClearTempPDF()
        {
            string folder = GetTempFolder();

            string[] files = Directory.GetFiles(folder, "*.pdf");

            foreach (string file in files)
            {
                try
                {
                    File.Delete(file);
                }
                catch
                {
                }
            }
        }

        #endregion
    }
}
