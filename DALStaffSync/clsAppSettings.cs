using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DALStaffSync
{
    public class clsAppSettings
    {
        dbStaffSync.clsAppSettings objAppSettings = new dbStaffSync.clsAppSettings();

        public clsAppSettings() { 

        }

        public AppSettings GetSpecificAppSettingsInfo(int txtAppSettingID)
        {
            return objAppSettings.GetSpecificAppSettingsInfo(txtAppSettingID);
        }

        public AppSettings GetSpecificAppSettingsInfo(string txtAppSettingsTitle)
        {
            return objAppSettings.GetSpecificAppSettingsInfo(txtAppSettingsTitle);
        }

        public List<AppSettings> GetAppSettingsList()
        {
            return objAppSettings.GetAppSettingsList();
        }
    }
}
