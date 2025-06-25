using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace StaffSync
{
    public static class AppVariables
    {
        
        public static string AppVersion = System.Configuration.ConfigurationSettings.AppSettings["AppVersion"].ToString(); // 🆕 Application version
        public static string AppName = System.Configuration.ConfigurationSettings.AppSettings["AppName"].ToString(); // 🆕 Application name
        public static int CompanyID = 1;
        public static string CompanyCode = ""; // 🆕 Default company name
        public static string CompanyName = ""; // 🆕 Default company name
        public static string CompanyAddress = ""; // 🆕 Default company address
        public static string CompanyPhone = ""; // 🆕 Default company phone number
        public static string CompanyEmail = "";
        public static string CompanyContactPerson = "";
        public static string ClientContactNumber = "";
        public static string ClientContactMail = "";
        public static string ClientWebSite = "";
        public static bool IsActive = true;
        public static bool IsDeleted = false;

        public static string TempFolderPath = System.IO.Path.Combine(@System.IO.Path.GetTempPath(), @System.Configuration.ConfigurationSettings.AppSettings["tempfolderpath"].ToString());
    }
}
