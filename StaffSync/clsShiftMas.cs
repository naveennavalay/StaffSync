using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public class clsShiftMas
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        DataSet dtDataset;

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

        public List<ShiftInfo> GetShiftList()
        {
            List<ShiftInfo> objShiftInfoList = new List<ShiftInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return objShiftInfoList;
        }

        public EmpShiftInfo getEmployeeSpecificShiftInfo(int txtEmpID)
        {
            List<EmpShiftInfo> objEmpShiftInfo = new List<EmpShiftInfo>();
            EmpShiftInfo objSpecificShiftInfo = new EmpShiftInfo();

            try
            {
                conn = objDBClass.openDBConnection();
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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return objSpecificShiftInfo;
        }

        public int InsertEmployeeShiftInfo(int txtEmpID, int txtShiftID, DateTime txtEffectiveDate)
        {
            int affectedRows = 0;
            try
            {

                int maxRowCount = getMaxRowCount("EmpShiftInfo", "EmpShiftInfoID");

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpShiftInfo (EmpShiftInfoID, EmpID, ShiftID, EffectiveDate) VALUES " +
                 "(" + maxRowCount  + ", " + txtEmpID + ", " + txtShiftID + ", #" + txtEffectiveDate.ToString("dd-MMM-yyyy") + "#)";

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

        public int UpdateEmployeeShiftInfo(int txtEmpShiftInfoID, int txtEmpID, int txtShiftID, DateTime txtEffectiveDate)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return affectedRows;
        }

        public int DeleteEmployeeShiftInfo(int txtEmpShiftInfoID)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE * FROM EmpShiftInfo WHERE EmpShiftInfoID = " + txtEmpShiftInfoID;

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

    public class EmpShiftInfo
    {
        public int EmpShiftInfoID { get; set; }
        public int EmpID { get; set; }
        public int ShiftID { get; set; }
        public DateTime EffectiveDate { get; set; }

    }

    public class ShiftInfo
    {
        public int ShiftID { get; set; }
        public string ShiftCode { get; set; }
        public string ShiftTitle { get; set; }
        public string ShiftInitital { get; set; }
        public DateTime? ShiftStart { get; set; } = DateTime.Now;
        public DateTime? ShiftEnd { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
