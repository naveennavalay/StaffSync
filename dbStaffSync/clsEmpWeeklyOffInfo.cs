using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace dbStaffSync
{
    public class clsEmpWeeklyOffInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();


        public List<EmployeeWeeklyOffInfo> getEmployeeSpecificWeeklyOffInfo(int intEmpID)
        {
            List<EmployeeWeeklyOffInfo> objEmployeeSpecificWeeklyOffInfoList = new List<EmployeeWeeklyOffInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = @"SELECT
                                        WklyOffProfileDetails.WklyOffDay,
                                        Switch (
                                            WklyOffProfileDetails.WklyOffDay = 1,
                                            ""Monday"",
                                            WklyOffProfileDetails.WklyOffDay = 2,
                                            ""Tuesday"",
                                            WklyOffProfileDetails.WklyOffDay = 3,
                                            ""Wednesday"",
                                            WklyOffProfileDetails.WklyOffDay = 4,
                                            ""Thursday"",
                                            WklyOffProfileDetails.WklyOffDay = 5,
                                            ""Friday"",
                                            WklyOffProfileDetails.WklyOffDay = 6,
                                            ""Saturday"",
                                            WklyOffProfileDetails.WklyOffDay = 7,
                                            ""Sunday""
                                        ) AS WeeklyOffDayName,
                                        EmpMas.EmpID
                                    FROM
                                        (
                                            WklyOffProfileInfo
                                            INNER JOIN (
                                                EmpMas
                                                INNER JOIN EmpWeeklyOff ON EmpMas.EmpID = EmpWeeklyOff.EmpID
                                            ) ON WklyOffProfileInfo.WklyOffMasID = EmpWeeklyOff.WklyOffMasID
                                        )
                                        INNER JOIN WklyOffProfileDetails ON WklyOffProfileInfo.WklyOffMasID = WklyOffProfileDetails.WklyOffMasID
                                    WHERE
                                        (((EmpMas.EmpID) = " + intEmpID + "));";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmployeeSpecificWeeklyOffInfoList = JsonConvert.DeserializeObject<List<EmployeeWeeklyOffInfo>>(DataTableToJSon);
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

            return objEmployeeSpecificWeeklyOffInfoList;
        }
    }
}
