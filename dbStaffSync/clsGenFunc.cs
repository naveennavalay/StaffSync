using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace dbStaffSync
{
    public class clsGenFunc
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset = null;

        public Response<int> getMaxRowCount(string tableName, string ColumnName)
        {
            int rowCount = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT MAX(" + ColumnName.ToString().Trim() + ") FROM " + tableName;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                int maxRow = (Int32)cmd.ExecuteScalar();
                if (maxRow == 0)
                    rowCount = 1;
                else if (maxRow > 0)
                    rowCount = maxRow + 1;
            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().ToLower() == "Specified cast is not valid.".ToLower())
                {
                    rowCount = 1;
                }
                else
                {
                    return Response<int>.Fail(ex.Message);
                    //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }

            return Response<int>.Success(rowCount);
        }

        public Response<int> getLeaveMasMaxRowCount(int txtLeaveMasID)
        {
            int rowCount = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " +
                                        "MAX(OrderID) AS OrderID " +
                                    "FROM " +
                                        "EmpLeaveEntitlement " +
                                    "WHERE " +
                                        "(((EmpLeaveEntitlement.LeaveMasID) = " + txtLeaveMasID + "));";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                int maxRow = (Int32)cmd.ExecuteScalar();
                if (maxRow == 0)
                    rowCount = 1;
                else if (maxRow > 0)
                    rowCount = maxRow + 1;

            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().ToLower() == "Specified cast is not valid.".ToLower())
                {
                    rowCount = 1;
                }
                else
                {
                    return Response<int>.Fail(ex.Message);
                    //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }

            return Response<int>.Success(rowCount);
        }

        public Response<int> getEmployeeSpecificOrderID(string tableName, string ColumnName, int EmpID)
        {
            int rowCount = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT MAX(OrderID) FROM " + tableName + " WHERE EmpID = " + EmpID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                int maxRow = (int)cmd.ExecuteScalar();
                if (maxRow == 0)
                    rowCount = 1;
                else if (maxRow > 0)
                    rowCount = maxRow + 1;

            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().ToLower() == "Specified cast is not valid.".ToLower())
                {
                    rowCount = 1;
                }
                else
                {
                    //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }

            return Response<int>.Success(rowCount);
        }

        public Response<int> getMaxRowCount(string tableName, string ColumnName, int CurrentCompanyID)
        {
            int rowCount = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                //string strQuery = "SELECT MAX(" + ColumnName.ToString().Trim() + ") FROM " + tableName;
                string strQuery = "SELECT MAX(EmpCode) FROM EMPMAS WHERE ClientID = " + CurrentCompanyID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                int maxRow = (Int32)cmd.ExecuteScalar();
                if (maxRow == 0)
                    rowCount = 1;
                else if (maxRow > 0)
                    rowCount = maxRow + 1;

            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().ToLower() == "Specified cast is not valid.".ToLower())
                {
                    rowCount = 1;
                }
                else
                {
                    //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }

            return Response<int>.Success(rowCount);
        }

    }
}
