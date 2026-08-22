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
    public class clsDashboardChartWidgets
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public List<LeaveStatusSummary> getLeaveStatusSummaryChartData(int txtClientID, DateTime dtFrom, DateTime dtTo)
        {
            List<LeaveStatusSummary> objLeaveStatusSummaryList = new List<LeaveStatusSummary>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                //" EmpLeaveTransMas.LeaveApprovalComments AS LeaveApprovalComments, " + 
                //" EmpLeaveTransMas.Canceled AS Canceled," +
                string strQuery = "SELECT " +
                                        " Format(EmpLeaveTransMas.ActualLeaveDateFrom, 'mmm-yyyy') AS MonthName, " + 
                                        " Year(EmpLeaveTransMas.ActualLeaveDateFrom) AS LeaveYear, " + 
                                        " Month(EmpLeaveTransMas.ActualLeaveDateFrom) AS LeaveMonth, " + 
                                        " Count(EmpLeaveTransMas.OrderID) AS TotalApplication, " + 
                                        " Sum(IIf(EmpLeaveTransMas.LeaveApprovalComments LIKE 'Approved :%', 1, 0)) AS TotalApproved, " + 
                                        " Sum(IIf(EmpLeaveTransMas.LeaveApprovalComments LIKE 'Rejected :%' AND EmpLeaveTransMas.LeaveRejectionComments LIKE 'Rejected :%' AND EmpLeaveTransMas.Canceled = False, 1, 0)) AS TotalRejected, " + 
                                        " Sum(IIf(EmpLeaveTransMas.LeaveApprovalComments = 'Not yet Approved' AND EmpLeaveTransMas.LeaveRejectionComments = 'Not yet Rejected' AND EmpLeaveTransMas.Canceled = False, 1, 0)) AS TotalPending, " +  
                                        " Sum(IIf(EmpLeaveTransMas.Canceled = True, 1, 0)) AS TotalCancelled, " + 
                                        " Sum(IIf(EmpLeaveTransMas.LeaveApprovalComments LIKE 'Approved :%' AND EmpLeaveTransMas.Canceled = False, IIf(IsNull(EmpLeaveTransMas.LeaveDuration), 0, EmpLeaveTransMas.LeaveDuration), 0)) AS TotalLeaveDays " + 
                                    " FROM " + 
                                        " ClientMas INNER JOIN (EmpMas INNER JOIN EmpLeaveTransMas ON EmpMas.EmpID = EmpLeaveTransMas.EmpID) ON ClientMas.ClientID = EmpMas.ClientID " + 
                                    " WHERE " + 
                                        " ClientMas.ClientID = " + txtClientID + 
                                        " AND EmpMas.IsActive = True " + 
                                        " AND EmpMas.IsDeleted = False " +
                                        " AND ((EmpLeaveTransMas.ActualLeaveDateFrom) >= #" + dtFrom.ToString("dd-MMM-yyyy") + "# AND (EmpLeaveTransMas.ActualLeaveDateFrom) < #" + dtTo.ToString("dd-MMM-yyyy") + "#) " +
                                    " GROUP BY " + 
                                        " Format(EmpLeaveTransMas.ActualLeaveDateFrom, 'mmm-yyyy'), " + 
                                        " Year(EmpLeaveTransMas.ActualLeaveDateFrom), " + 
                                        " Month(EmpLeaveTransMas.ActualLeaveDateFrom) " + 
                                    " ORDER BY " + 
                                        " Year(EmpLeaveTransMas.ActualLeaveDateFrom), " + 
                                        " Month(EmpLeaveTransMas.ActualLeaveDateFrom);";
                //" EmpLeaveTransMas.LeaveApprovalComments, " +
                //" EmpLeaveTransMas.Canceled, " +
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                //cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                //foreach (DataRow row in dt.Rows)
                //{
                //    LeaveStatusSummary obj = new LeaveStatusSummary();

                //    obj.MonthName = Convert.ToString(row["MonthName"]);
                //    //obj.LeaveApprovalComments = Convert.ToString(row["LeaveApprovalComments"]);
                //    //obj.Canceled = Convert.ToBoolean(row["Canceled"]);
                //    obj.LeaveYear = Convert.ToInt32(row["LeaveYear"]);
                //    obj.LeaveMonth = Convert.ToInt32(row["LeaveMonth"]);
                //    obj.TotalApplication = Convert.ToDouble(row["TotalApplication"]);
                //    obj.TotalApproved = Convert.ToDouble(row["TotalApproved"]);
                //    obj.TotalRejected = Convert.ToDouble(row["TotalRejected"]);
                //    obj.TotalPending = Convert.ToDouble(row["TotalPending"]);
                //    obj.TotalCancelled = Convert.ToDouble(row["TotalCancelled"]);
                //    obj.TotalLeaveDays = Convert.ToDouble(row["TotalLeaveDays"]);

                //    objLeaveStatusSummaryList.Add(obj);
                //}

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objLeaveStatusSummaryList = JsonConvert.DeserializeObject<List<LeaveStatusSummary>>(DataTableToJSon);
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

            return objLeaveStatusSummaryList;
        }

        public List<LeaveMatrixChartData> GetLeaveMatrixChartData(int txtClientID, DateTime dtFrom, DateTime dtTo)
        {
            List<LeaveMatrixChartData> objLeaveMatrixList = new List<LeaveMatrixChartData>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " + 
                                        " DepMas.DepartmentTitle AS Department, " + 
                                        " Sum(IIf(EmpLeaveTransMas.LeaveApprovalComments LIKE 'Approved :%' AND EmpLeaveTransMas.Canceled = False, 1, 0)) AS TotalApproved, " + 
                                        " Sum(IIf(EmpLeaveTransMas.LeaveApprovalComments = 'Not yet Approved' AND EmpLeaveTransMas.LeaveRejectionComments = 'Not yet Rejected' AND EmpLeaveTransMas.Canceled = False, 1, 0)) AS TotalPending, " + 
                                        " Sum(IIf(EmpLeaveTransMas.LeaveApprovalComments LIKE 'Rejected :%' AND EmpLeaveTransMas.LeaveRejectionComments LIKE 'Rejected :%' AND EmpLeaveTransMas.Canceled = False, 1, 0)) AS TotalRejected, " + 
                                        " Sum(IIf(EmpLeaveTransMas.Canceled = True, 1, 0)) AS TotalCancelled " + 
                                    " FROM " + 
                                        " ClientMas " + 
                                        " INNER JOIN ( " + 
                                            " DepMas " + 
                                            " INNER JOIN ( " + 
                                                " EmpMas " + 
                                                " INNER JOIN EmpLeaveTransMas ON EmpMas.EmpID = EmpLeaveTransMas.EmpID " + 
                                            " ) ON DepMas.DepartmentID = EmpMas.DepartmentID " + 
                                        " ) ON ClientMas.ClientID = EmpMas.ClientID " + 
                                    " WHERE " + 
                                        " ClientMas.ClientID = " + txtClientID +
                                        " AND EmpMas.IsActive = True  " +
                                        " AND EmpMas.IsDeleted = False " + 
                                        " AND EmpLeaveTransMas.ActualLeaveDateFrom >= #" + dtFrom.ToString("dd-MMM-yyyy") + "# " +
                                        " AND EmpLeaveTransMas.ActualLeaveDateFrom< #" + dtTo.ToString("dd-MMM-yyyy") + "# " +
                                    " GROUP BY " + 
                                        " DepMas.DepartmentTitle " + 
                                    " ORDER BY " + 
                                        " DepMas.DepartmentTitle";
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objLeaveMatrixList = JsonConvert.DeserializeObject<List<LeaveMatrixChartData>>(DataTableToJSon);
            }
            catch (Exception ex)
            {
                // Log if required
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }

            return objLeaveMatrixList;
        }

        public List<UpcomingHolidayChartData> GetUpcomingHolidayChartData(int txtClientID, int txtFinYearID)
        {
            List<UpcomingHolidayChartData> objUpcomingHolidayList = new List<UpcomingHolidayChartData>();

            DataTable dt = new DataTable();

            OleDbConnection conn = null;

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT TOP 3 " + 
                    "PubHolidayDetails.PubHolidayTitle AS HolidayName, " + 
                    "PubHolidayDetails.PubHolDate AS HolidayDate, " +
                    "DateDiff(" + "\"d\", " + "Date(), " + "PubHolidayDetails.PubHolDate" + ") AS DaysRemaining " +
                    "FROM " +
                    "PublicHolidayMas " +
                    "INNER JOIN PubHolidayDetails ON PublicHolidayMas.PubHolMasID = " +
                    "PubHolidayDetails.PubHolMasID " +
                    "WHERE " +
                    "PublicHolidayMas.ClientID = " + txtClientID +
                    " AND PublicHolidayMas.FinYearID = " + txtFinYearID +
                    " AND PubHolidayDetails.PubHolDate > Date() " +
                    "ORDER BY " +
                    "PubHolidayDetails.PubHolDate ASC";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objUpcomingHolidayList = JsonConvert.DeserializeObject<List<UpcomingHolidayChartData>>(DataTableToJSon);
            }
            catch (Exception ex)
            {
                // Log if required
            }
            finally
            {
                if (conn != null)
                {
                    dbStaffSync.closeDBConnection();
                }
            }


            return objUpcomingHolidayList;
        }

        public List<AttendanceSummaryChartData> getAttendanceDepartmentChartData(int txtClientID, DateTime dtFrom, DateTime dtTo)
        {
            List<AttendanceSummaryChartData> objAttendanceSummaryChartData = new List<AttendanceSummaryChartData>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();


                string strQuery =
                    "SELECT " +

                    "DepMas.DepartmentTitle AS Department, " +

                    "Sum(" +
                        "IIf(" +
                            "EmpDailyAttendanceInfo.AttStatus = 'Present', " +
                            "1, " +
                            "0" +
                        ")" +
                    ") AS TotalPresent, " +

                    "Sum(" +
                        "IIf(" +
                            "EmpDailyAttendanceInfo.AttStatus = 'Leave : Full Day', " +
                            "1, " +
                            "IIf(" +
                                "EmpDailyAttendanceInfo.AttStatus = 'Leave : First Half' " +
                                "OR " +
                                "EmpDailyAttendanceInfo.AttStatus = 'Leave : Second Half', " +
                                "0.5, " +
                                "0" +
                            ")" +
                        ")" +
                    ") AS TotalLeave, " +

                    "Sum(" +
                        "IIf(" +
                            "EmpDailyAttendanceInfo.AttStatus = 'Loss Of Pay', " +
                            "1, " +
                            "IIf(" +
                                "EmpDailyAttendanceInfo.AttStatus = 'Loss Of Pay : First Half' " +
                                "OR " +
                                "EmpDailyAttendanceInfo.AttStatus = 'Loss Of Pay : Second Half', " +
                                "0.5, " +
                                "0" +
                            ")" +
                        ")" +
                    ") AS TotalLOP " +

                    "FROM " +

                    "ClientMas " +

                    "INNER JOIN " +
                    "( " +

                        "DepMas " +

                        "INNER JOIN " +
                        "( " +

                            "EmpMas " +

                            "INNER JOIN EmpDailyAttendanceInfo " +
                            "ON EmpMas.EmpID = " +
                            "EmpDailyAttendanceInfo.EmpID " +

                        ") " +

                        "ON DepMas.DepartmentID = " +
                        "EmpMas.DepartmentID " +

                    ") " +

                    "ON ClientMas.ClientID = " +
                    "EmpMas.ClientID " +

                    "WHERE " +

                    "ClientMas.ClientID = " +
                    txtClientID + " " +

                    "AND EmpMas.IsActive = True " +

                    "AND EmpMas.IsDeleted = False " +

                    "AND EmpDailyAttendanceInfo.AttDate >= #" +
                    dtFrom.ToString("dd-MMM-yyyy") +
                    "# " +

                    "AND EmpDailyAttendanceInfo.AttDate < #" +
                    dtTo.ToString("dd-MMM-yyyy") +
                    "# " +

                    "GROUP BY " +
                    "DepMas.DepartmentTitle " +

                    "ORDER BY " +
                    "DepMas.DepartmentTitle;";


                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAttendanceSummaryChartData = JsonConvert.DeserializeObject<List<AttendanceSummaryChartData>>(DataTableToJSon);
            }
            catch (Exception ex)
            {
                // Log if required
            }
            finally
            {
                conn =
                    dbStaffSync.closeDBConnection();
            }


            return objAttendanceSummaryChartData;
        }
    }
}
