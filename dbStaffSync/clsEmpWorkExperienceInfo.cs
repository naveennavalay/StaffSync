//using C1.Framework;
using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace dbStaffSync
{
    public class clsEmpWorkExperienceInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset = null;
        clsGenFunc objGenFunc = new clsGenFunc();

        public clsEmpWorkExperienceInfo()
        {

        }

        public List<EmpWorkExpInfo> GetWorkExpDefaultList()
        {
            List<EmpWorkExpInfo> objEmpWorkExpInfo = new List<EmpWorkExpInfo>();
            List<EmpWorkExpInfo> objEmpWorkExpInfoList = new List<EmpWorkExpInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " + 
                                    "LastCompDetMas.LastCompanyInfoID, " + 
                                    "LastCompDetMas.LastCompanyCode, " + 
                                    "LastCompDetMas.LastCompanyTitle, " + 
                                    "LastCompDetMas.Industry, " + 
                                    "LastCompDetMas.Address " + 
                                "FROM " + 
                                    "LastCompDetMas " + 
                                "WHERE " + 
                                    "(" + 
                                        "((LastCompDetMas.IsActive) = True) " + 
                                        "AND ((LastCompDetMas.IsDeleted) = False) " + 
                                    "); ";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmpWorkExpInfo = JsonConvert.DeserializeObject<List<EmpWorkExpInfo>>(DataTableToJSon);
                foreach (EmpWorkExpInfo indEmpWorkExpInfo in objEmpWorkExpInfo)
                {
                    objEmpWorkExpInfoList.Add(new EmpWorkExpInfo
                    {
                        LastCompID = 0,
                        EmpID = indEmpWorkExpInfo.EmpID,
                        LastCompanyInfoID = indEmpWorkExpInfo.LastCompanyInfoID,
                        LastCompanyTitle = indEmpWorkExpInfo.LastCompanyTitle,
                        Address = indEmpWorkExpInfo.Address,
                        StartDate = indEmpWorkExpInfo.StartDate,
                        EndDate = indEmpWorkExpInfo.EndDate,
                        Comments = indEmpWorkExpInfo.Comments
                    });
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

            return objEmpWorkExpInfoList;
        }

        public List<EmpWorkExpInfo> GetWorkExpListInfo(int txtEmployeeID)
        {
            List<EmpWorkExpInfo> objEmpWorkExpInfo = new List<EmpWorkExpInfo>();
            List<EmpWorkExpInfo> objEmpWorkExpInfoList = new List<EmpWorkExpInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " + 
                        "EmpWorkExp.LastCompID, " +
                        "LastCompDetMas.LastCompanyInfoID, " +
                        "LastCompDetMas.LastCompanyCode, " +
                        "LastCompDetMas.LastCompanyTitle, " +
                        "LastCompDetMas.Industry, " +
                        "LastCompDetMas.Address, " +
                        "EmpWorkExp.StartDate, " +
                        "EmpWorkExp.EndDate, " +
                        "EmpWorkExp.Comments, " +
                        "EmpWorkExp.EmpID " +
                    "FROM " +
                        "LastCompDetMas " +
                        "INNER JOIN EmpWorkExp ON LastCompDetMas.LastCompanyInfoID = EmpWorkExp.LastCompanyInfoID " +
                    "WHERE " +
                        "(" +
                            "((LastCompDetMas.IsActive) = True) " +
                            "AND ((LastCompDetMas.IsDeleted) = False) " +
                            "AND ((EmpWorkExp.EmpID) = " + txtEmployeeID + ")" +
                        ") ORDER BY EmpWorkExp.LastCompID;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmpWorkExpInfo = JsonConvert.DeserializeObject<List<EmpWorkExpInfo>>(DataTableToJSon);
                foreach (EmpWorkExpInfo indEmpWorkExpInfo in objEmpWorkExpInfo)
                {
                    objEmpWorkExpInfoList.Add(new EmpWorkExpInfo
                    {
                        LastCompID = indEmpWorkExpInfo.LastCompID,
                        EmpID = indEmpWorkExpInfo.EmpID,
                        LastCompanyInfoID = indEmpWorkExpInfo.LastCompanyInfoID,
                        LastCompanyTitle = indEmpWorkExpInfo.LastCompanyTitle,
                        Address = indEmpWorkExpInfo.Address,
                        StartDate = indEmpWorkExpInfo.StartDate,
                        EndDate = indEmpWorkExpInfo.EndDate,
                        Comments = indEmpWorkExpInfo.Comments
                    });
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

            return objEmpWorkExpInfoList;
        }

        public List<EmpWorkExpInfo> UpdateWorkExpListInfo(int txtEmployeeID, EmpWorkExpInfo objNewRow)
        {
            List<EmpWorkExpInfo> objEmpWorkExpInfo = new List<EmpWorkExpInfo>();
            List<EmpWorkExpInfo> objEmpWorkExpInfoList = new List<EmpWorkExpInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " +
                        "EmpWorkExp.LastCompID, " +
                        "LastCompDetMas.LastCompanyInfoID, " +
                        "LastCompDetMas.LastCompanyCode, " +
                        "LastCompDetMas.LastCompanyTitle, " +
                        "LastCompDetMas.Industry, " +
                        "LastCompDetMas.Address, " +
                        "EmpWorkExp.StartDate, " +
                        "EmpWorkExp.EndDate, " +
                        "EmpWorkExp.Comments, " +
                        "EmpWorkExp.EmpID " +
                    "FROM " +
                        "LastCompDetMas " +
                        "INNER JOIN EmpWorkExp ON LastCompDetMas.LastCompanyInfoID = EmpWorkExp.LastCompanyInfoID " +
                    "WHERE " +
                        "(" +
                            "((LastCompDetMas.IsActive) = True) " +
                            "AND ((LastCompDetMas.IsDeleted) = False) " +
                            "AND ((EmpWorkExp.EmpID) = " + txtEmployeeID + ") " +
                        ")";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmpWorkExpInfo = JsonConvert.DeserializeObject<List<EmpWorkExpInfo>>(DataTableToJSon);
                foreach (EmpWorkExpInfo indEmpWorkExpInfo in objEmpWorkExpInfo)
                {
                    objEmpWorkExpInfoList.Add(new EmpWorkExpInfo
                    {
                        LastCompID = indEmpWorkExpInfo.LastCompID,
                        EmpID = indEmpWorkExpInfo.EmpID,
                        LastCompanyInfoID = indEmpWorkExpInfo.LastCompanyInfoID,
                        LastCompanyTitle = indEmpWorkExpInfo.LastCompanyTitle,
                        Address = indEmpWorkExpInfo.Address,
                        StartDate = indEmpWorkExpInfo.StartDate,
                        EndDate = indEmpWorkExpInfo.EndDate,
                        Comments = indEmpWorkExpInfo.Comments
                    });
                }

                objEmpWorkExpInfoList.Add(new EmpWorkExpInfo
                {
                    LastCompID = objNewRow.LastCompID,
                    EmpID = objNewRow.EmpID,
                    LastCompanyInfoID = objNewRow.LastCompanyInfoID,
                    LastCompanyTitle = objNewRow.LastCompanyTitle,
                    Address = objNewRow.Address,
                    StartDate = objNewRow.StartDate,
                    EndDate = objNewRow.EndDate,
                    Comments = objNewRow.Comments
                });
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

            return objEmpWorkExpInfoList;
        }

        public int InsertEmpWorkExpInfo(int txtEmpID, int txtLastCompanyInfoID, DateTime txtStartDate, DateTime txtEndDate, string txtComments)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("EmpWorkExp", "LastCompID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpWorkExp (LastCompID, EmpID, LastCompanyInfoID, StartDate, EndDate, Comments) VALUES " +
                 "(" + maxRowCount.Data + "," + txtEmpID + "," + txtLastCompanyInfoID + ",'" + txtStartDate + "','" + txtEndDate + "','" + txtComments + "')";

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

        public int UpdatetEmpWorkExpInfo(int txtLastCompanyID, int txtEmpID, int txtLastCompanyInfoID, DateTime txtStartDate, DateTime txtEndDate, string txtComments)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpWorkExp SET " +
                "EmpID = " + txtEmpID + ", LastCompanyInfoID = " + txtLastCompanyInfoID + ", StartDate = '" + txtStartDate + "', EndDate = '" + txtEndDate + "', Comments = '" + txtComments + "'" +
                " WHERE LastCompID = " + txtLastCompanyID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtLastCompanyID;
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

        public int DeleteEmpWorkExpInfo(int txtLastCompanyID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE FROM EmpWorkExp WHERE LastCompID = " + txtLastCompanyID;

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
