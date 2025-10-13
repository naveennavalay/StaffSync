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
    public class clsEmploymentTypeInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset = null;
        clsGenFunc objGenFunc = new clsGenFunc();

        public clsEmploymentTypeInfo()
        {

        }

        public List<EmploymentTypeInfo> GetEmploymentTypeList()
        {
            List<EmploymentTypeInfo> objEmploymentTypeInfoList = new List<EmploymentTypeInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

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
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }

            return objEmploymentTypeInfoList;
        }

        public EmpTypeInfo getEmployeeSpecificEmploymentTypeInfo(int txtEmpID)
        {
            List<EmpTypeInfo> objEmploymentTypeInfoList = new List<EmpTypeInfo>();
            EmpTypeInfo objEmploymentTypeInfo = new EmpTypeInfo();

            try
            {
                conn = dbStaffSync.openDBConnection();
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
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }

            return objEmploymentTypeInfo;
        }

        public int InsertEmploymentTypeInfo(int txtEmpID, int txtEmpTypeMasID, DateTime txtEffectiveDate)
        {
            int affectedRows = 0;
            try
            {

                Response<int> maxRowCount = objGenFunc.getMaxRowCount("EmpTypeInfo", "EmpTypeInfoID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpTypeInfo (EmpTypeInfoID, EmpID, EmpTypeMasID, EffectiveDate) VALUES " +
                 "(" + maxRowCount.Data + ", " + txtEmpID + ", " + txtEmpTypeMasID + ", #" + txtEffectiveDate.ToString("dd-MMM-yyyy") + "#)";

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

        public int UpdateEmploymentTypeInfo(int txtEmpTypeInfoID, int txtEmpID, int txtEmpTypeMasID, DateTime txtEffectiveDate)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
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
                //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }

            return affectedRows;
        }

        public int DeleteEmployeeShiftInfo(int txtEmpTypeInfoID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE * FROM EmpTypeInfo WHERE EmpTypeInfoID = " + txtEmpTypeInfoID;

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
