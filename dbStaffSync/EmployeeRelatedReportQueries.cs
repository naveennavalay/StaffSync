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
    public class EmployeeRelatedReportQueries
    {

        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;

        public List<ActiveEmployeeListReport> getActiveEmployeeListReport(int ClientID, string Filter)
        {
            List<ActiveEmployeeListReport> objActiveEmployeeListReportList = new List<ActiveEmployeeListReport>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " +
                                        " FinYearMas.FinYearFromTo, " +
                                        " EmpMas.EmpID, " +
                                        " EmpMas.EmpCode, " +
                                        " EmpMas.EmpName, " +
                                        " DesigMas.DesignationTitle, " +
                                        " DepMas.DepartmentTitle, " +
                                        " PersonalInfoMas.ContactNumber1, " +
                                        " PersonalInfoMas.ContactNumber2, " +
                                        " PersonalInfoMas.DOJ, " +
                                        " PersonalInfoMas.LastDateOfProbation, " +
                                        " PersonalInfoMas.DateOfConfirmation, " +
                                        " SexMas.SexTitle, " +
                                        " BloodGroupMas.BloodGroupTitle, " +
                                        " NomineeMas.NomineePerson, " +
                                        " NomineeMas.ContactNumber, " +
                                        " RelationShipMas.RelationShipTitle, " +
                                        " ClientBranchMas.ClientBranchCode, " +
                                        " ClientBranchMas.ClientBranchName " +
                                    " FROM " +
                                        " StateMas " +
                                        " INNER JOIN ( " +
                                            " SexMas " +
                                            " INNER JOIN ( " +
                                                " RelationShipMas " +
                                                " INNER JOIN ( " +
                                                    " FinYearMas " +
                                                    " INNER JOIN ( " +
                                                        " ( " +
                                                            " ( " +
                                                                " DesigMas " +
                                                                " INNER JOIN ( " +
                                                                    " DepMas " +
                                                                    " INNER JOIN ( " +
                                                                        " ClientMas " +
                                                                        " INNER JOIN ( " +
                                                                            " BloodGroupMas " +
                                                                            " INNER JOIN EmpMas ON BloodGroupMas.BloodGroupID = EmpMas.BloodGroupID " +
                                                                        " ) ON ClientMas.ClientID = EmpMas.ClientID " +
                                                                    " ) ON DepMas.DepartmentID = EmpMas.DepartmentID " +
                                                                " ) ON DesigMas.DesignationID = EmpMas.EmpDesignationID " +
                                                            " ) " +
                                                            " INNER JOIN NomineeMas ON EmpMas.EmpID = NomineeMas.EmpID " +
                                                        " ) " +
                                                        " INNER JOIN ( " +
                                                            " ClientBranchMas " +
                                                            " INNER JOIN ( " +
                                                                " AddressMas " +
                                                                " INNER JOIN PersonalInfoMas ON AddressMas.AddressID = PersonalInfoMas.PerAddressID " +
                                                            " ) ON ClientBranchMas.ClientBranchID = PersonalInfoMas.ClientBranchID " +
                                                        " ) ON EmpMas.EmpID = PersonalInfoMas.EmpID " +
                                                    " ) ON FinYearMas.FinYearID = ClientMas.FinYearID " +
                                                " ) ON RelationShipMas.RelationShipID = NomineeMas.RelationShipID " +
                                            " ) ON SexMas.SexID = PersonalInfoMas.SexID " +
                                        " ) ON StateMas.StateID = AddressMas.StateID " +
                                    " WHERE " +
                                        " ( " +
                                            " ((ClientMas.ClientID) = " + ClientID + ") " +
                                            " AND ((EmpMas.IsActive) = True) " +
                                            " AND ((EmpMas.IsDeleted) = False) " +
                                            " AND ((FinYearMas.FinYearID) = 1) " +
                                            Filter +
                                        " ) " +
                                    " ORDER BY " +
                                        " EmpMas.EmpID, " +
                                        " ClientMas.ClientID;";
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objActiveEmployeeListReportList = JsonConvert.DeserializeObject<List<ActiveEmployeeListReport>>(DataTableToJSon);
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

            return objActiveEmployeeListReportList;
        }

        public List<MonthlyAttendanceReport> getMonthlyAttendanceRegister(int ClientID, DateTime dtFrom, DateTime dtTo)
        {
            List<MonthlyAttendanceReport> objMonthlyAttendanceReport = new List<MonthlyAttendanceReport>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "TRANSFORM First ( " +
                                        " Switch ( " +
                                            " EmpDailyAttendanceInfo.AttStatus = 'Present', 'P', " +
                                            " IsNull(EmpDailyAttendanceInfo.AttStatus) OR EmpDailyAttendanceInfo.AttStatus = '', 'WE', " +
                                            " EmpDailyAttendanceInfo.AttStatus = 'Leave : Full Day', 'L', " +
                                            " EmpDailyAttendanceInfo.AttStatus = 'Leave : First Half', 'L/P', " +
                                            " EmpDailyAttendanceInfo.AttStatus = 'Leave : Second Half', 'P/L', " +
                                            " True, " +
                                            " EmpDailyAttendanceInfo.AttStatus " +
                                        " ) " +
                                    " ) AS AttendanceStatus " +
                                    " SELECT " +
                                        " EmpMas.EmpID, " +
                                        " EmpMas.EmpCode, " +
                                        " EmpMas.EmpName, " +
                                        " DesigMas.DesignationTitle, " +
                                        " DepMas.DepartmentTitle, " +
                                        " PersonalInfoMas.DOJ " +
                                    " FROM " +
                                        " (" +
                                            " (" +
                                                " DesigMas " +
                                                " INNER JOIN ( " +
                                                    " DepMas " +
                                                    " INNER JOIN ( " +
                                                        " ClientMas " +
                                                        " INNER JOIN EmpMas ON ClientMas.ClientID = EmpMas.ClientID " +
                                                    " ) ON DepMas.DepartmentID = EmpMas.DepartmentID " +
                                                " ) ON DesigMas.DesignationID = EmpMas.EmpDesignationID " +
                                            " ) " +
                                            " INNER JOIN EmpDailyAttendanceInfo ON EmpMas.EmpID = EmpDailyAttendanceInfo.EmpID " +
                                        " ) " +
                                        " INNER JOIN PersonalInfoMas ON EmpMas.EmpID = PersonalInfoMas.EmpID " +
                                    " WHERE " +
                                        " ( " +
                                            " ( " +
                                                " (ClientMas.ClientID) = " + ClientID +
                                            " ) " +
                                            " AND ( " +
                                                " (EmpDailyAttendanceInfo.AttDate) >= #" + dtFrom.ToString("dd-MMM-yyyy") + "# " +
                                                " AND (EmpDailyAttendanceInfo.AttDate) <= #" + dtTo.ToString("dd-MMM-yyyy") + "# " +
                                            " ) " +
                                            " AND ((EmpMas.IsActive) = True) " +
                                            " AND ((EmpMas.IsDeleted) = False) " +
                                        " ) " +
                                    " GROUP BY " +
                                        " EmpMas.EmpID, " +
                                        " EmpMas.EmpCode, " +
                                        " EmpMas.EmpName, " +
                                        " DesigMas.DesignationTitle, " +
                                        " DepMas.DepartmentTitle, " +
                                        " PersonalInfoMas.DOJ, " +
                                        " ClientMas.ClientID, " +
                                        " EmpMas.IsActive, " +
                                        " EmpMas.IsDeleted " +
                                    " ORDER BY " +
                                        " EmpMas.EmpName PIVOT Day([EmpDailyAttendanceInfo].[AttDate]) IN ( " +
                                            " 1, " +
                                            " 2, " +
                                            " 3, " +
                                            " 4, " +
                                            " 5, " +
                                            " 6, " +
                                            " 7, " +
                                            " 8, " +
                                            " 9, " +
                                            " 10, " +
                                            " 11, " +
                                            " 12, " +
                                            " 13, " +
                                            " 14, " +
                                            " 15, " +
                                            " 16, " +
                                            " 17, " +
                                            " 18, " +
                                            " 19, " +
                                            " 20, " +
                                            " 21, " +
                                            " 22, " +
                                            " 23, " +
                                            " 24, " +
                                            " 25, " +
                                            " 26, " +
                                            " 27, " +
                                            " 28, " +
                                            " 29, " +
                                            " 30, " +
                                            " 31 " +
                                        " );";
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objMonthlyAttendanceReport = JsonConvert.DeserializeObject<List<MonthlyAttendanceReport>>(DataTableToJSon);
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

            return objMonthlyAttendanceReport;
        }
    }
}