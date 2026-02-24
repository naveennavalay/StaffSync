using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;

namespace dbStaffSync
{
    public class clsAppSettings
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public clsAppSettings() { 

        }

        public AppSettings GetSpecificAppSettingsInfo(int txtAppSettingID)
        {
            List<AppSettings> objAppSettings = new List<AppSettings>();
            try
            {
                conn = dbStaffSync.openDBConnection();
                DataTable dt = new DataTable();

                string strQuery = "SELECT AppSettingID, AppSettingCode, AppSettingTitle, AppSettingValue FROM AppSettings WHERE AppSettingID = " + txtAppSettingID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAppSettings = JsonConvert.DeserializeObject<List<AppSettings>>(DataTableToJSon);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }

            if (objAppSettings != null && objAppSettings.Count > 0)
                return objAppSettings[0];
            else
                return new AppSettings();

        }

        public AppSettings GetSpecificAppSettingsInfo(string txtAppSettingsTitle)
        {
            List<AppSettings> objAppSettings = new List<AppSettings>();
            try
            {
                conn = dbStaffSync.openDBConnection();
                DataTable dt = new DataTable();

                string strQuery = "SELECT AppSettingID, AppSettingCode, AppSettingTitle, AppSettingValue FROM AppSettings WHERE AppSettingTitle = '" + txtAppSettingsTitle + "'";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAppSettings = JsonConvert.DeserializeObject<List<AppSettings>>(DataTableToJSon);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }

            if (objAppSettings != null && objAppSettings.Count > 0)
                return objAppSettings[0];
            else
                return new AppSettings();

        }

        public List<AppSettings> GetAppSettingsList()
        {
            List<AppSettings> objAppSettings = new List<AppSettings>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT AppSettingID, AppSettingCode, AppSettingTitle, AppSettingValue FROM AppSettings ORDER BY AppSettingID";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAppSettings = JsonConvert.DeserializeObject<List<AppSettings>>(DataTableToJSon);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }
            return objAppSettings;
        }
    }
}
