//using C1.Framework;
using ModelStaffSync;
using Newtonsoft.Json;
//using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
using System.Data;
//using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace dbStaffSync
{
    public class clsWeeklyOffInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public clsWeeklyOffInfo() { 

        }

        public List<WklyOffProfileMasInfo> getWklyOffProfileMasInfoList(string txtWklyOffTitle)
        {
            DataTable dt = new DataTable();
            List<WklyOffProfileMasInfo> objWklyOffProfileMasInfoList = new List<WklyOffProfileMasInfo>();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "";
                if(string.IsNullOrEmpty(txtWklyOffTitle))
                    strQuery = "SELECT " + 
                                        "WklyOffMasID, WklyOffCode, WklyOffTitle, WklyOffEffectiveDate " +
                                  " FROM " + 
                                        " WklyOffProfileInfo " + 
                                  " WHERE " + 
                                        "(((WklyOffProfileInfo.IsActive) = True) AND ((WklyOffProfileInfo.IsDelete) = False));";
                else
                    strQuery ="SELECT " + 
                                        "WklyOffMasID, WklyOffCode, WklyOffTitle, WklyOffEffectiveDate " +
                                  " FROM " + 
                                        " WklyOffProfileInfo " + 
                                  " WHERE " + 
                                        "WklyOffTitle LIKE '" + txtWklyOffTitle + "%' AND IsActive = True AND IsDelete = False;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objWklyOffProfileMasInfoList = JsonConvert.DeserializeObject<List<WklyOffProfileMasInfo>>(DataTableToJSon);

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

            return objWklyOffProfileMasInfoList;
        }

        public int InsertWeeklyOffInfo(string txtWklyOffCode, string txtWklyOffTitle, DateTime txtWklyOffEffectiveDate, bool IsActive, bool IsDelete)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("WklyOffProfileInfo", "WklyOffMasID");
                
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO WklyOffProfileInfo (WklyOffMasID, WklyOffCode, WklyOffTitle, WklyOffEffectiveDate, IsActive, IsDelete) VALUES " +
                 "(" + maxRowCount.Data + ",'" + "WOF-" + (maxRowCount.Data).ToString().PadLeft(4, '0').Trim() + "','" + txtWklyOffTitle + "','" + txtWklyOffEffectiveDate.ToString("dd-MMM-yyyy") + "'," + IsActive + "," + IsDelete + ")";

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

        public int UpdateWeeklyOffInfo(int txtWeeklyOffMasID, string txtWklyOffCode, string txtWklyOffTitle, DateTime txtWklyOffEffectiveDate, bool IsActive, bool IsDelete)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE WklyOffProfileInfo SET " + 
                                " WklyOffCode = '" + txtWklyOffCode + "', WklyOffTitle = '" +  txtWklyOffTitle + "', WklyOffEffectiveDate = '" + txtWklyOffEffectiveDate.ToString("dd-MMM-yyyy") + "', IsActive = " + IsActive + ", IsDelete = " + IsDelete + 
                                " WHERE " + 
                                " WklyOffMasID = " + txtWeeklyOffMasID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = affectedRows;
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

        public int DeleteWeeklyOffInfo(int txtWeeklyOffMasID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                //LeaveDuration = 0,
                string strQuery = "DELETE FROM WklyOffProfileInfo WHERE WklyOffMasID = " + txtWeeklyOffMasID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = affectedRows;
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

        public List<WklyOffProfileDetailsInfo> getWeeklyOffDetailsInfo(int txtWklyOffMasID)
        {
            DataTable dt = new DataTable();
            List<WklyOffProfileDetailsInfo> objWklyOffProfileDetailsInfoList = new List<WklyOffProfileDetailsInfo>();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " + 
                                    " WklyOffDetID, WklyOffDay, WklyOffOrderID " + 
                                  " FROM " + 
                                    " WklyOffProfileDetails " + 
                                  " WHERE " + 
                                    " (((WklyOffMasID) = " + txtWklyOffMasID + ")) " + 
                                  " ORDER BY " + 
                                    " WklyOffDetID, WklyOffOrderID ASC";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objWklyOffProfileDetailsInfoList = JsonConvert.DeserializeObject<List<WklyOffProfileDetailsInfo>>(DataTableToJSon);
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

            return objWklyOffProfileDetailsInfoList;
        }

        public List<EmployeeWklyOffInfo> getEmployeeSpecificWeeklyOffMasterInfo(int txtEmpID)
        {
            DataTable dt = new DataTable();
            List<EmployeeWklyOffInfo> objEmployeeWklyOffInfoList = new List<EmployeeWklyOffInfo>();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " + 
                                        " WeeklyOffID, WklyOffMasID, EffectDateFrom " + 
                                        " FROM EmpWeeklyOff " +
                                  " WHERE " +
                                        " EmpID = " + txtEmpID + 
                                        " ORDER BY WeeklyOffID, EffectDateFrom ASC";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmployeeWklyOffInfoList = JsonConvert.DeserializeObject<List<EmployeeWklyOffInfo>>(DataTableToJSon);
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

            return objEmployeeWklyOffInfoList;
        }

        public int InsertEmployeeSpecificWeeklyInfo(int txtEmpID, int txtWklyOffMasID, DateTime txtEffectDateFrom)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount =  objGenFunc.getMaxRowCount("EmpWeeklyOff", "WeeklyOffID");
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();
                string strQuery = "INSERT INTO EmpWeeklyOff (WeeklyOffID, EmpID, WklyOffMasID, EffectDateFrom) VALUES " +
                 "(" + maxRowCount.Data + "," + txtEmpID + "," + txtWklyOffMasID + ",'" + txtEffectDateFrom.ToString("dd-MMM-yyyy") + "')";
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

        public int UpdateEmployeeSpecificWeeklyInfo(int txtWeeklyOffID, int txtEmpID, int txtWklyOffMasID, DateTime txtEffectDateFrom)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();
                string strQuery = "UPDATE EmpWeeklyOff SET " +
                    " EmpID = " + txtEmpID + ", WklyOffMasID = " + txtWklyOffMasID + ", EffectDateFrom = '" + txtEffectDateFrom.ToString("dd-MMM-yyyy") + "' WHERE WeeklyOffID = " + txtWeeklyOffID;
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtWeeklyOffID;
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

        public int DeleteEmployeeSpecificWeeklyInfo(int txtWeeklyOffID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();
                string strQuery = "DELETE FROM EmpWeeklyOff WHERE WeeklyOffID = " + txtWeeklyOffID;
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = affectedRows;
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

        public int InsertWeeklyOffDetailInfo(int txtWklyOffMasID, int txtWklyOffDay, int txtWklyOffOrderID)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("WklyOffProfileDetails", "WklyOffDetID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO WklyOffProfileDetails (WklyOffDetID, WklyOffMasID, WklyOffDay, WklyOffOrderID) VALUES " +
                 "(" + maxRowCount.Data + "," + txtWklyOffMasID + "," + txtWklyOffDay + "," + maxRowCount + ")";

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

        public int UpdateWeeklyOffDetailInfo(int txtWklyOffDetID, int txtWklyOffMasID, int txtWklyOffDay, int txtWklyOffOrderID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE WklyOffProfileDetails SET " +
                    "WklyOffMasID = " + txtWklyOffMasID + ", WklyOffDay = " + txtWklyOffDay + ", WklyOffOrderID = " + txtWklyOffOrderID + " WHERE WklyOffDetID = " + txtWklyOffDetID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = affectedRows;
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

        public int DeleteWeeklyOffDetailInfo(int txtWklyOffDetID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE FROM WklyOffProfileDetails WHERE WklyOffDetID = " + txtWklyOffDetID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = affectedRows;
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
