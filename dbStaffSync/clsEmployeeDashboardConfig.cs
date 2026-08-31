using ModelStaffSync;
using Newtonsoft.Json;
using ReportingEngine.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace dbStaffSync
{
    public class clsEmployeeDashboardConfig
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public List<EmployeeDashboardConfigModel> getEmployeeDashboardConfigInfoList(int ClientID, int EmpID)
        {
            List<EmployeeDashboardConfigModel> objEmployeeDashboardConfigModelList = new List<EmployeeDashboardConfigModel>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = @"SELECT
                                        DBChartInfo.DBChartID,
                                        DBChartInfo.DBChartTitle,
                                        DBChartInfo.DBChartShortTitle,
                                        EmpDBChartInfo.EmpDBChartID,
                                        EmpDBChartInfo.PersonalInfoID,
                                        DBChartInfo.UIChartID,
                                        EmpDBChartInfo.DBChartEnabled,
                                        EmpDBChartInfo.OrderID
                                    FROM
                                        (
                                            (
                                                ClientMas
                                                INNER JOIN EmpMas ON ClientMas.ClientID = EmpMas.ClientID
                                            )
                                            INNER JOIN PersonalInfoMas ON EmpMas.EmpID = PersonalInfoMas.EmpID
                                        )
                                        INNER JOIN (
                                            DBChartInfo
                                            INNER JOIN EmpDBChartInfo ON DBChartInfo.DBChartID = EmpDBChartInfo.DBChartID
                                        ) ON PersonalInfoMas.PersonalInfoID = EmpDBChartInfo.PersonalInfoID
                                    WHERE
                                        (
                                            ((EmpMas.EmpID) = " + EmpID + ") " + @"
                                            AND ((EmpMas.IsActive) = True)
                                            AND ((EmpMas.IsDeleted) = False)
                                            AND ((ClientMas.ClientID) = " + ClientID + ") " + @"
                                        )
                                    ORDER BY
                                        DBChartInfo.DBChartID,
                                        EmpDBChartInfo.EmpDBChartID,
                                        EmpDBChartInfo.OrderID;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmployeeDashboardConfigModelList = JsonConvert.DeserializeObject<List<EmployeeDashboardConfigModel>>(DataTableToJSon);
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

            return objEmployeeDashboardConfigModelList;
        }

        public int InsertEmployeeDashboardConfigInfo(int txtPersonalInfoID, int txtDBChartID, bool boolDBChartEnabled, int txtOrderID)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("EmpDBChartInfo", "EmpDBChartID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpDBChartInfo (EmpDBChartID, PersonalInfoID, DBChartID, DBChartEnabled, OrderID) " +
                                  "VALUES (" + maxRowCount.Data + ", " + txtPersonalInfoID + ", " + txtDBChartID + ", " + boolDBChartEnabled + ", " + txtOrderID + ")";

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

        public int UpdateEmployeeDashboardConfigInfo(int txtEmpDBChartID, int txtPersonalInfoID, int txtDBChartID, bool boolDBChartEnabled, int txtOrderID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpDBChartInfo SET " +
                                        " PersonalInfoID = " + txtPersonalInfoID + ", " +
                                        " DBChartID = " + txtDBChartID + ", " + 
                                        " DBChartEnabled = " + boolDBChartEnabled + ", " + 
                                        " OrderID = " + txtOrderID +
                                  " WHERE " + 
                                        " EmpDBChartID = " + txtEmpDBChartID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtEmpDBChartID;
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
