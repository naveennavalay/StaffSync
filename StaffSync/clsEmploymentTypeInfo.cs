using ModelStaffSync;
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
    public class clsEmploymentTypeInfo
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

        public List<EmploymentTypeInfo> GetEmploymentTypeList()
        {
            List<EmploymentTypeInfo> objEmploymentTypeInfoList = new List<EmploymentTypeInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT * FROM EmploymentTypeMas WHERE IsDelete = false ORDER By EmpTypeMasID Asc";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmploymentTypeInfoList = JsonConvert.DeserializeObject<List<EmploymentTypeInfo>>(DataTableToJSon);
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

            return objEmploymentTypeInfoList;
        }

        public EmpTypeInfo getEmployeeSpecificEmploymentTypeInfo(int txtEmpID)
        {
            List<EmpTypeInfo> objEmploymentTypeInfoList = new List<EmpTypeInfo>();
            EmpTypeInfo objEmploymentTypeInfo = new EmpTypeInfo();

            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();
                DataTable dt = new DataTable();

                string strQuery = "SELECT " +
                                  " EmpTypeInfoID, " +
                                  " EmpID, " +
                                  " EmpTypeMasID, " +
                                  " EffectiveDate " +
                                " FROM " +
                                    " EmpTypeInfo " +
                                " WHERE " +
                                    " EmpID = " + txtEmpID + " AND EmpTypeInfoID = (SELECT Max(EmpTypeInfoID) AS MaxEmpTypeInfoID " +
                                            " FROM EmpTypeInfo " + 
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
                objEmploymentTypeInfoList = JsonConvert.DeserializeObject<List<EmpTypeInfo>>(DataTableToJSon);
                if (objEmploymentTypeInfoList.Count > 0)
                {
                    objEmploymentTypeInfo = objEmploymentTypeInfoList[0];
                }
                else
                {
                    objEmploymentTypeInfo.EffectiveDate = DateTime.Now;
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

            return objEmploymentTypeInfo;
        }

        public int InsertEmploymentTypeInfo(int txtEmpID, int txtEmpTypeMasID, DateTime txtEffectiveDate)
        {
            int affectedRows = 0;
            try
            {

                int maxRowCount = getMaxRowCount("EmpTypeInfo", "EmpTypeInfoID");

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpTypeInfo (EmpTypeInfoID, EmpID, EmpTypeMasID, EffectiveDate) VALUES " +
                 "(" + maxRowCount  + ", " + txtEmpID + ", " + txtEmpTypeMasID + ", #" + txtEffectiveDate.ToString("dd-MMM-yyyy") + "#)";

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

        public int UpdateEmploymentTypeInfo(int txtEmpTypeInfoID, int txtEmpID, int txtEmpTypeMasID, DateTime txtEffectiveDate)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpTypeInfo SET " +
                " EmpTypeMasID = " + txtEmpTypeMasID + ", EffectiveDate = #" + txtEffectiveDate.ToString("dd-MMM-yyyy") + "# " +
                " WHERE EmpTypeInfoID = (SELECT Max(EmpTypeInfoID) AS MaxEmpTypeInfoID FROM EmpTypeInfo WHERE EmpID = " + txtEmpID + ")";

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

        public int DeleteEmployeeShiftInfo(int txtEmpTypeInfoID)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE * FROM EmpTypeInfo WHERE EmpTypeInfoID = " + txtEmpTypeInfoID;

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

    //public class EmpTypeInfo
    //{
    //    public int EmpTypeInfoID { get; set; }
    //    public int EmpID { get; set; }
    //    public int EmpTypeMasID { get; set; }
    //    public DateTime EffectiveDate { get; set; }

    //}

    //public class EmploymentTypeInfo
    //{
    //    public int EmpTypeMasID { get; set; }
    //    public string EmpTypeCode { get; set; }
    //    public string EmpTypeTitle { get; set; }
    //    public string EmpTypeInitial { get; set; }
    //    public bool IsActive { get; set; }
    //    public bool IsDeleted { get; set; }
    //}
}
