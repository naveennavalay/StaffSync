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
    public class clsLeaveTypeMas
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public clsLeaveTypeMas() { 

        }

        public DataTable GetLeaveTypeList()
        {
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM LeaveTypeMas WHERE IsActive = true and IsDelete = false Order By OrderID";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

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

            return dt;
        }

        public DataTable GetLeaveTypeList(string FilterText)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM LeaveTypeMas WHERE IsActive = true and IsDelete = false and LeaveTypeTitle like '" + FilterText + "%'";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

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

            return dt;
        }

        public List<LeaveTypeInfoModel> GetLeaveTypeInfo(int LeaveTypeID)
        {
            List<LeaveTypeInfoModel> LeaveTypeInfoList = new List<LeaveTypeInfoModel>(); 
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM LeaveTypeMas WHERE LeaveTypeID = " + LeaveTypeID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                LeaveTypeInfoList = JsonConvert.DeserializeObject<List<LeaveTypeInfoModel>>(DataTableToJSon);
                //foreach (LeaveEntitlementInfo indLeaveTypeInfo in LeaveTypeInfoList)
                //{
                //    indLeaveTypeInfo.LeaveEntmtID = indLeaveTypeInfo.LeaveEntmtID;
                //    indLeaveTypeInfo.EmpID = indLeaveTypeInfo.EmpID;
                //    indLeaveTypeInfo.LeaveMasID = indLeaveTypeInfo.LeaveMasID;
                //    indLeaveTypeInfo.LeaveTypeID = indLeaveTypeInfo.LeaveTypeID;
                //    indLeaveTypeInfo.TotalLeaves = indLeaveTypeInfo.TotalLeaves;
                //    indLeaveTypeInfo.BalanceLeaves = indLeaveTypeInfo.BalanceLeaves;
                //    indLeaveTypeInfo.OrderID = indLeaveTypeInfo.OrderID;
                //}
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

            return LeaveTypeInfoList;
        }

        public int InsertLeaveTypeInfo(string txtLeaveCode, string txtLeaveTypeTitle, bool IsPaid, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("LeaveTypeMas", "LeaveTypeID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO LeaveTypeMas (LeaveTypeID, LeaveCode, LeaveTypeTitle, IsPaid, IsActive, IsDelete, OrderID) VALUES " +
                 "(" + maxRowCount.Data + ",'" + "LEV-" + (maxRowCount.Data).ToString().PadLeft(4, '0').Trim() + "', '" + txtLeaveTypeTitle + "', " + IsPaid + ", " + IsActive + ", false," + maxRowCount + ")";

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

        public int UpadateLeaveTypeInfo(int txtLeaveTypeID, string txtLeaveCode, string txtLeaveTypeTitle, bool IsPaid, bool IsActive)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE LeaveTypeMas SET " +
                "LeaveCode = '" + txtLeaveCode + "', LeaveTypeTitle = '" + txtLeaveTypeTitle + "', IsPaid = " + IsPaid + ", IsActive = " + IsActive +
                " WHERE LeaveTypeID = " + txtLeaveTypeID;

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

        public int DeleteListTypeInfo(int txtLeaveTypeID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE FROM LeaveTypeMas WHERE LeaveTypeID = " + txtLeaveTypeID;

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
