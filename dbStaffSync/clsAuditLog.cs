using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace dbStaffSync
{
    public class clsAuditLog
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();


        public List<AuditLogs> getAuditLogStatements(int txtSourceID, string txtAuditLogStatementFilter, string txtEventGroup, int txtClientiD)
        {
            List<AuditLogs> objAuditStatementsList = new List<AuditLogs>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " +
                                        " UserAuditLog.UserAuditLogID, " +
                                        " UserAuditLog.SourceID, " +
                                        " UserAuditLog.UserName, " +
                                        " UserAuditLog.ActionType, " +
                                        " UserAuditLog.EventDateTime, " +
                                        " UserAuditLog.AuditLogStatement, " +
                                        " UserAuditLog.EventGroup, " +
                                        " UserAuditLog.ClientID " +
                                    " FROM " + 
                                        " UserAuditLog " + 
                                    " WHERE " + 
                                        " ( " + 
                                            " ((UserAuditLog.SourceID) = " + txtSourceID + ") " + 
                                            " AND ((UserAuditLog.AuditLogStatement) LIKE '%" + txtAuditLogStatementFilter + "%') " + 
                                            " AND ((UserAuditLog.EventGroup) LIKE '%" + txtEventGroup  + "%') " + 
                                            " AND ((UserAuditLog.ClientID) = " + txtClientiD + ") " + 
                                        " ) " + 
                                    " ORDER BY " + 
                                        " UserAuditLog.UserAuditLogID Desc;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAuditStatementsList = JsonConvert.DeserializeObject<List<AuditLogs>>(DataTableToJSon);
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

            return objAuditStatementsList;
        }

        public List<AuditLogs> getAuditLogStatements(int txtSourceID, string txtEventGroup, int txtClientiD)
        {
            List<AuditLogs> objAuditStatementsList = new List<AuditLogs>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " + 
                                        " UserAuditLog.UserAuditLogID, " + 
                                        " UserAuditLog.SourceID, " +
                                        " UserAuditLog.UserName, " +
                                        " UserAuditLog.ActionType, " +
                                        " UserAuditLog.EventDateTime, " + 
                                        " UserAuditLog.AuditLogStatement, " +
                                        " UserAuditLog.EventGroup, " + 
                                        " UserAuditLog.ClientID " + 
                                    " FROM " + 
                                        " UserAuditLog " + 
                                    " WHERE " + 
                                        " ( " +
                                           " ((UserAuditLog.SourceID) = " + txtSourceID + ") " + 
                                           " AND ((UserAuditLog.EventGroup) LIKE '%" + txtEventGroup + "%') " + 
                                            " AND ((UserAuditLog.ClientID) = " + txtClientiD + ") " + 
                                        " ) " + 
                                    " ORDER BY " +
                                        " UserAuditLog.UserAuditLogID Desc;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAuditStatementsList = JsonConvert.DeserializeObject<List<AuditLogs>>(DataTableToJSon);
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

            return objAuditStatementsList;
        }

        public int InsertAuditLog(int txtEmpID, int txtSourceID, string txtAuditLogStatement, string txtActionType, string txtUserName, string txtEventGroup, int txtClientID)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("UserAuditLog", "UserAuditLogID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO UserAuditLog (UserAuditLogID, EmpID, SourceID, EventDateTime, AuditLogStatement, ActionType, UserName, EventGroup, ClientID) VALUES " +
                 "(" + maxRowCount.Data + "," + txtEmpID + ", " + txtSourceID + ", '" + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") + "','" + txtAuditLogStatement + "','" + txtActionType + "', '" + txtUserName + "', '" + txtEventGroup + "', " + txtClientID + ")";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = maxRowCount.Data;
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
            return affectedRows;
        }
    }
}
