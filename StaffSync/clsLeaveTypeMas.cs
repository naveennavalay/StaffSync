using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public class clsLeaveTypeMas
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        DataSet dtDataset;

        public clsLeaveTypeMas() { 

        }

        public int getMaxRowCount(string tableName, string ColumnName)
        {
            int rowCount = 0;
            try
            {
                conn = objDBClass.openDBConnection();
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
                    MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return rowCount;
        }

        public DataTable GetLeaveTypeList()
        {
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return dt;
        }

        public DataTable GetLeaveTypeList(string FilterText)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT * FROM LeaveTypeMas WHERE IsActive = true and IsDelete = false and LeaveTypeTitle like '" + FilterText + "%";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return dt;
        }

        public List<LeaveTypeInfoModel> GetLeaveTypeInfo(int LeaveTypeID)
        {
            List<LeaveTypeInfoModel> LeaveTypeInfoList = new List<LeaveTypeInfoModel>(); 
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return LeaveTypeInfoList;
        }

        public int InsertLeaveTypeInfo(string txtLeaveCode, string txtLeaveTypeTitle, bool IsPaid, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            try
            {
                int maxRowCount = getMaxRowCount("LeaveTypeMas", "LeaveTypeID");

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO LeaveTypeMas (LeaveTypeID, LeaveCode, LeaveTypeTitle, IsPaid, IsActive, IsDelete, OrderID) VALUES " +
                 "(" + maxRowCount + ",'" + "LEV-" + (maxRowCount).ToString().PadLeft(4, '0').Trim() + "', '" + txtLeaveTypeTitle + "', " + IsPaid + ", " + IsActive + ", false," + maxRowCount + ")";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = maxRowCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return affectedRows;
        }

        public int UpadateLeaveTypeInfo(int txtLeaveTypeID, string txtLeaveCode, string txtLeaveTypeTitle, bool IsPaid, bool IsActive)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return affectedRows;
        }

        public int DeleteListTypeInfo(int txtLeaveTypeID)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE FROM LeaveTypeMas WHERE LeaveTypeID = " + txtLeaveTypeID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return affectedRows;
        }
    }

    //public class LeaveTypeInfoModel
    //{
    //    public int LeaveTypeID { get; set; }

    //    [DisplayName("Leave Type Code")] 
    //    public string LeaveCode { get; set; }

    //    [DisplayName("Leave Type Title")]
    //    public string LeaveTypeTitle { get; set; }

    //    [DisplayName("Is Paid")]
    //    public bool IsPaid { get; set; }
    //    public bool IsActive { get; set; }
    //    public bool IsDelete { get; set; }
    //    public int OrderID { get; set; }
    //}
}
