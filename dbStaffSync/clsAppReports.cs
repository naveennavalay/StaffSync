using ModelStaffSync;
using Newtonsoft.Json;
using ReportingEngine.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace dbStaffSync
{
    public class clsAppReports
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public List<AppReports> GetReportsList(string ReportType)
        {
            List<AppReports> objAppReportsList = new List<AppReports>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "";
                strQuery = "SELECT " +
                                " AppReports.ReportsID, " +
                                " AppReports.ReportsCode, " +
                                " AppReports.ReportsName, " +
                                " AppReports.ReportsDescription, " +
                                " AppReports.IsActive, " +
                                " AppReports.IsDeleted, " +
                                " AppReports.ReportType, " +
                                " AppReports.ClientID, " +
                                " AppReports.OrderID " +
                            " FROM " +
                                " AppReports " +
                            " WHERE " +
                                    " AppReports.IsActive = True AND AppReports.IsDeleted = False";

                if(ReportType != "")
                    strQuery = strQuery + " AND AppReports.ReportType = '" + ReportType + "'";

                strQuery  = strQuery  + " ORDER BY " +
                    " AppReports.ReportsID, AppReports.OrderID";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAppReportsList = JsonConvert.DeserializeObject<List<AppReports>>(DataTableToJSon);
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

            return objAppReportsList;
        }
    }
}
