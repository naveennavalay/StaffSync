using dbStaffSync;
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
    public class clsShiftMas
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();


        public List<ShiftInfo> GetShiftList()
        {
            List<ShiftInfo> objShiftInfoList = new List<ShiftInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM ShiftMas WHERE IsDelete = false ORDER By ShiftID Asc";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objShiftInfoList = JsonConvert.DeserializeObject<List<ShiftInfo>>(DataTableToJSon);
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

            return objShiftInfoList;
        }

        public EmpShiftInfo getEmployeeSpecificShiftInfo(int txtEmpID)
        {
            List<EmpShiftInfo> objEmpShiftInfo = new List<EmpShiftInfo>();
            EmpShiftInfo objSpecificShiftInfo = new EmpShiftInfo();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();
                DataTable dt = new DataTable();

                string strQuery = "SELECT " +
                                  " EmpShiftInfoID, " +
                                  " EmpID, " +
                                  " ShiftID, " +
                                  " EffectiveDate " +
                                " FROM " +
                                    " EmpShiftInfo " +
                                " WHERE " +
                                    " EmpID = " + txtEmpID + " AND EmpShiftInfoID = (SELECT Max(EmpShiftInfoID) AS MaxEmpShiftInfoID " +
                                            " FROM EmpShiftInfo " + 
                                            " WHERE " + 
                                            " EmpID = " + txtEmpID +
                                    " )";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmpShiftInfo = JsonConvert.DeserializeObject<List<EmpShiftInfo>>(DataTableToJSon);
                if (objEmpShiftInfo.Count > 0)
                {
                    objSpecificShiftInfo = objEmpShiftInfo[0];
                }
                else
                {
                    objSpecificShiftInfo.EffectiveDate = DateTime.Now;
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

            return objSpecificShiftInfo;
        }

        public int GetShiftTitleByTitle(string ShiftTitle)
        {
            int selectedShiftID = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT ShiftID FROM ShiftMas WHERE ShiftTitle = '" + ShiftTitle + "'";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;

                cmd.Parameters.AddWithValue("@ShiftTitle", ShiftTitle.Trim());
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    selectedShiftID = Convert.ToInt32(result);
                }
                else
                {
                    selectedShiftID = 0;
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

            return selectedShiftID;
        }

        public int InsertShiftMasInfo(string txtShiftCode, string txtShiftTitle, string txtShiftInitial, DateTime ShiftStart, DateTime ShiftEnd, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            try
            {

                Response<int> maxRowCount = objGenFunc.getMaxRowCount("SkillsMas", "SkillID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO ShiftMas (ShiftID, ShiftCode, ShiftTitle, ShiftInitital, ShiftStart, ShiftEnd, IsActive, IsDelete) VALUES " +
                 "(" + maxRowCount.Data + ",'" + "STT-" + (maxRowCount.Data).ToString().PadLeft(4, '0').Trim() + "','" + txtShiftTitle.Trim() + "','" + txtShiftInitial.Trim() + "', #" + ShiftStart.ToString("hh:mm:ss tt") + "#, #" + ShiftEnd.ToString("hh:mm:ss tt") + "#, " + IsActive + "," + IsDeleted + ")";

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

        public int UpdateShiftMas(int ShiftID, string txtShiftCode, string txtShiftTitle, string txtShiftInitial, DateTime ShiftStart, DateTime ShiftEnd, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE ShiftMas SET " +
                "ShiftCode = '" + txtShiftCode.Trim() + "', ShiftTitle = '" + txtShiftTitle.Trim() + "', ShiftInitial = '" + txtShiftInitial.Trim() + "', ShiftStart = #" + ShiftStart.ToString("hh:mm:ss tt") + "#, ShiftEnd = #" + ShiftEnd.ToString("hh:mm:ss tt") + "#, IsActive = " + IsActive + ", IsDeleted = " + IsDeleted +
                " WHERE ShiftID = " + ShiftID.ToString().Trim();

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
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

        public int DeleteShiftMasInfo(int ShiftID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE * FROM ShiftMas WHERE ShiftID = " + ShiftID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
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


        public int InsertEmployeeShiftInfo(int txtEmpID, int txtShiftID, DateTime txtEffectiveDate)
        {
            int affectedRows = 0;
            try
            {

                Response<int> maxRowCount = objGenFunc.getMaxRowCount("EmpShiftInfo", "EmpShiftInfoID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpShiftInfo (EmpShiftInfoID, EmpID, ShiftID, EffectiveDate) VALUES " +
                 "(" + maxRowCount.Data  + ", " + txtEmpID + ", " + txtShiftID + ", #" + txtEffectiveDate.ToString("dd-MMM-yyyy") + "#)";

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

        public int UpdateEmployeeShiftInfo(int txtEmpShiftInfoID, int txtEmpID, int txtShiftID, DateTime txtEffectiveDate)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpShiftInfo SET " +
                " ShiftID = " + txtShiftID + ", EffectiveDate = #" + txtEffectiveDate.ToString("dd-MMM-yyyy") + "# " +
                " WHERE EmpShiftInfoID = (SELECT Max(EmpShiftInfoID) AS MaxEmpShiftInfoID FROM EmpShiftInfo WHERE EmpID = " + txtEmpID + ")";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
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

        public int DeleteEmployeeShiftInfo(int txtEmpShiftInfoID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE * FROM EmpShiftInfo WHERE EmpShiftInfoID = " + txtEmpShiftInfoID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
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
