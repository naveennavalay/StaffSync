using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace dbStaffSync
{
    public class clsAppMailTemplate
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public List<AppMailTemplateModel> GetAppMailTemplateList()
        {
            List<AppMailTemplateModel> objAppMailTemplateList = new List<AppMailTemplateModel>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " +
                                        " AppMailTemplate.AppMailTempID, " +
                                        " AppMailTemplate.AppMailTempName, " +
                                        " AppMailTemplate.AppMailTempSubject, " +
                                        " AppMailTemplate.AppMailTempBodyHTML, " +
                                        " AppMailTemplate.IsActive, " +
                                        " AppMailTemplate.IsDeleted " +
                                    " FROM " +
                                        " AppMailTemplate " +
                                    " WHERE " +
                                        " ( " +
                                            " ((AppMailTemplate.IsActive) = True) " +
                                            " AND ((AppMailTemplate.IsDeleted) = False) " +
                                        " ) " +
                                    " ORDER BY " +
                                        " AppMailTemplate.AppMailTempID;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAppMailTemplateList = JsonConvert.DeserializeObject<List<AppMailTemplateModel>>(DataTableToJSon);
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

            return objAppMailTemplateList;
        }

        public AppMailTemplateModel GetSpecificAppMailTemplateByID(int appMailTempID)
        {
            List<AppMailTemplateModel> objAppMailTemplateList = new List<AppMailTemplateModel>();
            AppMailTemplateModel objSelectedAppMailTemplate = new AppMailTemplateModel();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " + 
                                         " AppMailTemplate.AppMailTempID, " +
                                        " AppMailTemplate.AppMailTempName, " +
                                        " AppMailTemplate.AppMailTempSubject, " +
                                        " AppMailTemplate.AppMailTempBodyHTML, " +
                                        " AppMailTemplate.IsActive, " +
                                        " AppMailTemplate.IsDeleted " +
                                    " FROM " +
                                        " AppMailTemplate " +
                                    " WHERE " +
                                        " ( " +
                                            " ((AppMailTemplate.AppMailTempID) = " + appMailTempID + ") " +
                                            " AND ((AppMailTemplate.IsActive) = True) " +
                                            " AND ((AppMailTemplate.IsDeleted) = False) " +
                                        " ) " +
                                    " ORDER BY " +
                                        " AppMailTemplate.AppMailTempID;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objSelectedAppMailTemplate = JsonConvert.DeserializeObject<AppMailTemplateModel>(DataTableToJSon);
                if (objAppMailTemplateList.Count > 0)
                {
                    objSelectedAppMailTemplate = objAppMailTemplateList[0];
                }
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

            return objSelectedAppMailTemplate;
        }


        public AppMailTemplateModel GetSpecificAppMailTemplateByName(string appMailTempName)
        {
            List<AppMailTemplateModel> objAppMailTemplateList = new List<AppMailTemplateModel>();
            AppMailTemplateModel objSelectedAppMailTemplate = new AppMailTemplateModel();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " +
                                         " AppMailTemplate.AppMailTempID, " +
                                        " AppMailTemplate.AppMailTempName, " +
                                        " AppMailTemplate.AppMailTempSubject, " +
                                        " AppMailTemplate.AppMailTempBodyHTML, " +
                                        " AppMailTemplate.IsActive, " +
                                        " AppMailTemplate.IsDeleted " +
                                    " FROM " +
                                        " AppMailTemplate " +
                                    " WHERE " +
                                        " ( " +
                                            " ((AppMailTemplate.AppMailTempName) = '" + appMailTempName + "') " + 
                                            " AND ((AppMailTemplate.IsActive) = True) " +
                                            " AND ((AppMailTemplate.IsDeleted) = False) " +
                                        " ) " +
                                    " ORDER BY " +
                                        " AppMailTemplate.AppMailTempID;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAppMailTemplateList = JsonConvert.DeserializeObject<List<AppMailTemplateModel>>(DataTableToJSon);
                if (objAppMailTemplateList.Count > 0)
                {
                    objSelectedAppMailTemplate = objAppMailTemplateList[0];
                }
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

            return objSelectedAppMailTemplate;
        }
    }
}
