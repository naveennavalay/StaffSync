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

                    "AND EmpDailyAttendanceInfo.AttDate >= #" + dtFrom.ToString("dd-MMM-yyyy") + "# " +

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

        public AttendanceCalendarChartResponse getAttendanceCalendarChartData(int txtClientID, DateTime dtFrom, DateTime dtTo)
        {
            AttendanceCalendarChartResponse response = new AttendanceCalendarChartResponse();

            try
            {
                conn = dbStaffSync.openDBConnection();

                // ============================================================
                // LEVEL 1
                // MONTH-WISE
                // ============================================================

                string strMonthQuery = "SELECT " + 
                                                "Q.AttendanceMonth, " +
                                                "Sum(IIf(Q.StatusType = 'Present', 1, 0)) AS TotalPresent, " +
                                                "Sum(IIf(Q.StatusType = 'Leave', 1, 0)) AS TotalLeave, " +
                                                "Sum(IIf(Q.StatusType = 'Loss Of Pay', 1, 0)) AS TotalLOP " +
                                            "FROM " +
                                                " ( " + 
                                                    "SELECT DISTINCT " + 
                                                        "Format(EmpDailyAttendanceInfo.AttDate,'mmm-yyyy') AS AttendanceMonth, " + 
                                                        "EmpDailyAttendanceInfo.EmpID, " + 
                                                        "IIf(EmpDailyAttendanceInfo.AttStatus = 'Present', 'Present', " + 
                                                            "IIf(EmpDailyAttendanceInfo.AttStatus = 'Leave : Full Day' OR EmpDailyAttendanceInfo.AttStatus = 'Leave : First Half' OR EmpDailyAttendanceInfo.AttStatus = 'Leave : Second Half', 'Leave', " + 
                                                                "IIf(EmpDailyAttendanceInfo.AttStatus = 'Loss Of Pay' " + 
                                                                    "OR EmpDailyAttendanceInfo.AttStatus = 'Loss Of Pay : First Half' " + 
                                                                    "OR EmpDailyAttendanceInfo.AttStatus = 'Loss Of Pay : Second Half', " + 
                                                                    "'Loss Of Pay', " + 
                                                                    "'Other' " + 
                                                                ") " + 
                                                            ") " + 
                                                        ") AS StatusType " + 
                                                    "FROM " + 
                                                        "(" + 
                                                            "ClientMas " + 
                                                            "INNER JOIN EmpMas " + 
                                                                "ON ClientMas.ClientID = EmpMas.ClientID " + 
                                                        ") " + 
                                                        "INNER JOIN EmpDailyAttendanceInfo ON EmpMas.EmpID = EmpDailyAttendanceInfo.EmpID " + 
                                                    "WHERE " + 
                                                        "ClientMas.ClientID = " + txtClientID +
                                                        "AND EmpMas.IsActive = True " + 
                                                        "AND EmpMas.IsDeleted = False " + 
                                                        "AND EmpDailyAttendanceInfo.AttDate >= #" + dtFrom.ToString("dd-MMM-yyyy") + "# " + 
                                                        "AND EmpDailyAttendanceInfo.AttDate < #" + dtTo.ToString("dd-MMM-yyyy") + "# " + 
                                                ") AS Q " + 
                                            " WHERE " + 
                                                " Q.StatusType <> 'Other' " + 
                                            " GROUP BY " + 
                                                " Q.AttendanceMonth " + 
                                            " ORDER BY " + 
                                                "Q.AttendanceMonth;";

                OleDbCommand cmdMonth = conn.CreateCommand();

                cmdMonth.CommandType = CommandType.Text;
                cmdMonth.CommandText = strMonthQuery;

                OleDbDataAdapter daMonth =
                    new OleDbDataAdapter(cmdMonth);

                DataTable dtMonth = new DataTable();

                daMonth.Fill(dtMonth);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dtMonth);
                response.MonthData = JsonConvert.DeserializeObject<List<AttendanceCalendarMonthData>>(DataTableToJSon);

                //foreach (DataRow row in dtMonth.Rows)
                //{
                //    AttendanceCalendarChartData obj =
                //        new AttendanceCalendarChartData();

                //    obj.AttendanceMonth =
                //        Convert.ToString(row["AttendanceMonth"]);

                //    obj.TotalPresent =
                //        Convert.ToDouble(row["TotalPresent"]);

                //    obj.TotalLeave =
                //        Convert.ToDouble(row["TotalLeave"]);

                //    obj.TotalLOP =
                //        Convert.ToDouble(row["TotalLOP"]);

                //    CalculateAttendancePercentage(obj);

                //    response.MonthData.Add(obj);
                //}

                // ============================================================
                // LEVEL 2
                // DATE-WISE
                // ============================================================

                string strDateQuery ="SELECT " + 
                                            " Q.AttendanceDate, " +
                                            " Sum(IIf(Q.StatusType = 'Present', 1, 0)) AS TotalPresent, " +
                                            " Sum(IIf(Q.StatusType = 'Leave', 1, 0)) AS TotalLeave, " +
                                            " Sum(IIf(Q.StatusType = 'Loss Of Pay', 1, 0)) AS TotalLOP " +
                                        " FROM " +
                                            " (" +
                                                " SELECT DISTINCT " +
                                                    " EmpDailyAttendanceInfo.AttDate AS AttendanceDate, " +
                                                    " EmpDailyAttendanceInfo.EmpID, " +
                                                    " IIf(" +
                                                        " EmpDailyAttendanceInfo.AttStatus = 'Present', " +
                                                        " 'Present', " +
                                                        " IIf(" + 
                                                            " EmpDailyAttendanceInfo.AttStatus = 'Leave : Full Day' " + 
                                                            " OR EmpDailyAttendanceInfo.AttStatus = 'Leave : First Half' " + 
                                                            " OR EmpDailyAttendanceInfo.AttStatus = 'Leave : Second Half', " + 
                                                            " 'Leave', " + 
                                                            " IIf(" + 
                                                                " EmpDailyAttendanceInfo.AttStatus = 'Loss Of Pay' " + 
                                                                " OR EmpDailyAttendanceInfo.AttStatus = 'Loss Of Pay : First Half' " + 
                                                                " OR EmpDailyAttendanceInfo.AttStatus = 'Loss Of Pay : Second Half', " + 
                                                                " 'Loss Of Pay', " + 
                                                                " 'Other' " + 
                                                            ") " + 
                                                        ") " + 
                                                    ") AS StatusType " +
                                                " FROM " +
                                                    " (" +
                                                        " ClientMas INNER JOIN EmpMas ON ClientMas.ClientID = EmpMas.ClientID " +
                                                    " ) " +
                                                    " INNER JOIN EmpDailyAttendanceInfo ON EmpMas.EmpID = EmpDailyAttendanceInfo.EmpID " +
                                                " WHERE " +
                                                        "ClientMas.ClientID = " + txtClientID +
                                                        "AND EmpMas.IsActive = True " +
                                                        "AND EmpMas.IsDeleted = False " +
                                                        "AND EmpDailyAttendanceInfo.AttDate >= #" + dtFrom.ToString("dd-MMM-yyyy") + "# " +
                                                        "AND EmpDailyAttendanceInfo.AttDate < #" + dtTo.ToString("dd-MMM-yyyy") + "# " +
                                            " ) AS Q " +
                                        " WHERE " +
                                            " Q.StatusType <> 'Other' " +
                                        " GROUP BY " +
                                            " Q.AttendanceDate " +
                                        " ORDER BY " +
                                            " Q.AttendanceDate;";

                OleDbCommand cmdDate = conn.CreateCommand();

                cmdDate.CommandType = CommandType.Text;
                cmdDate.CommandText = strDateQuery;

                OleDbDataAdapter daDate = new OleDbDataAdapter(cmdDate);

                DataTable dtDate = new DataTable();
                daDate.Fill(dtDate);

                DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dtDate);
                response.DateData = JsonConvert.DeserializeObject<List<AttendanceCalendarDateData>>(DataTableToJSon);

                //foreach (DataRow row in dtDate.Rows)
                //{
                //    AttendanceCalendarChartData obj = new AttendanceCalendarChartData();

                //    DateTime attendanceDate = Convert.ToDateTime(row["AttendanceDate"]);

                //    //obj.AttendanceDate = attendanceDate.ToString("yyyy-MM-dd");

                //    obj.TotalPresent = Convert.ToDouble(row["TotalPresent"]);

                //    obj.TotalLeave = Convert.ToDouble(row["TotalLeave"]);

                //    obj.TotalLOP = Convert.ToDouble(row["TotalLOP"]);

                //    CalculateAttendancePercentage(obj);

                //    response.DateData.Add(obj);
                //}


                // ============================================================
                // LEVEL 3
                // DATE + DEPARTMENT
                // ============================================================

                string strDepartmentQuery =
                    "SELECT " + 
                            " Q.AttendanceDate, " + 
                            " Q.Department, " + 
                            " Sum(IIf(Q.StatusType = 'Present', 1, 0)) AS TotalPresent, " + 
                            " Sum(IIf(Q.StatusType = 'Leave', 1, 0)) AS TotalLeave, " + 
                            " Sum(IIf(Q.StatusType = 'Loss Of Pay', 1, 0)) AS TotalLOP " + 
                        " FROM " + 
                            "(" + 
                                "SELECT DISTINCT " + 
                                    " EmpDailyAttendanceInfo.AttDate AS AttendanceDate, " + 
                                    " DepMas.DepartmentTitle AS Department, " + 
                                    " EmpDailyAttendanceInfo.EmpID, " + 
                                    " IIf(EmpDailyAttendanceInfo.AttStatus = 'Present', 'Present', " + 
                                        " IIf(" + 
                                            " EmpDailyAttendanceInfo.AttStatus = 'Leave : Full Day' " + 
                                            " OR EmpDailyAttendanceInfo.AttStatus = 'Leave : First Half' " + 
                                            " OR EmpDailyAttendanceInfo.AttStatus = 'Leave : Second Half', " + 
                                            " 'Leave', " + 
                                            " IIf(" + 
                                                " EmpDailyAttendanceInfo.AttStatus = 'Loss Of Pay' " + 
                                                " OR EmpDailyAttendanceInfo.AttStatus = 'Loss Of Pay : First Half' " + 
                                                " OR EmpDailyAttendanceInfo.AttStatus = 'Loss Of Pay : Second Half', " + 
                                                " 'Loss Of Pay', " + 
                                                "'Other' " + 
                                            " ) " +
                                        " ) " +
                                    " ) AS StatusType " +
                                " FROM " +
                                    " (" + 
                                      " (" + 
                                            "ClientMas INNER JOIN EmpMas ON ClientMas.ClientID = EmpMas.ClientID " + 
                                        ") " + 
                                        " INNER JOIN DepMas ON EmpMas.DepartmentID = DepMas.DepartmentID " + 
                                    ") " + 
                                    " INNER JOIN EmpDailyAttendanceInfo ON EmpMas.EmpID = EmpDailyAttendanceInfo.EmpID " + 
                                " WHERE " +
                                        "ClientMas.ClientID = " + txtClientID +
                                        "AND EmpMas.IsActive = True " +
                                        "AND EmpMas.IsDeleted = False " +
                                        "AND EmpDailyAttendanceInfo.AttDate >= #" + dtFrom.ToString("dd-MMM-yyyy") + "# " +
                                        "AND EmpDailyAttendanceInfo.AttDate < #" + dtTo.ToString("dd-MMM-yyyy") + "# " + ") AS Q " + 
                        " WHERE " + 
                            " Q.StatusType <> 'Other' " + 
                        " GROUP BY " + 
                            " Q.AttendanceDate, " + 
                            " Q.Department " + 
                        " ORDER BY " + 
                            " Q.AttendanceDate, " + 
                            " Q.Department;";

                OleDbCommand cmdDepartment = conn.CreateCommand();

                cmdDepartment.CommandType = CommandType.Text;
                cmdDepartment.CommandText = strDepartmentQuery;

                OleDbDataAdapter daDepartment = new OleDbDataAdapter(cmdDepartment);

                DataTable dtDepartment = new DataTable();

                daDepartment.Fill(dtDepartment);

                DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dtDepartment);
                response.DepartmentData = JsonConvert.DeserializeObject<List<AttendanceCalendarDepartmentData>>(DataTableToJSon);

                //foreach (DataRow row in dtDepartment.Rows)
                //{
                //    AttendanceCalendarChartData obj =
                //        new AttendanceCalendarChartData();

                //    DateTime attendanceDate =
                //        Convert.ToDateTime(row["AttendanceDate"]);

                //    obj.AttendanceDate =
                //        attendanceDate.ToString("yyyy-MM-dd");

                //    obj.Department =
                //        Convert.ToString(row["Department"]);

                //    obj.TotalPresent =
                //        Convert.ToDouble(row["TotalPresent"]);

                //    obj.TotalLeave =
                //        Convert.ToDouble(row["TotalLeave"]);

                //    obj.TotalLOP =
                //        Convert.ToDouble(row["TotalLOP"]);

                //    CalculateAttendancePercentage(obj);

                //    response.DepartmentData.Add(obj);
                //}
            }
            catch (Exception ex)
            {
                // Log if required
                // MessageBox.Show(ex.Message);
            }
            finally
            {
                dbStaffSync.closeDBConnection();
            }

            return response;
        }

        private void CalculateAttendancePercentage(AttendanceCalendarChartData obj)
        {
            double total = obj.TotalPresent + obj.TotalLeave + obj.TotalLOP;

            if (total <= 0)
            {
                obj.PresentPercentage = 0;
                obj.LeavePercentage = 0;
                obj.LOPPercentage = 0;
                return;
            }

            obj.PresentPercentage = Math.Round((obj.TotalPresent / total) * 100, 2);

            obj.LeavePercentage = Math.Round((obj.TotalLeave / total) * 100, 2);

            obj.LOPPercentage = Math.Round((obj.TotalLOP / total) * 100, 2);
        }

        public List<LeaveInfoDepartmentInfoChartData> GetLeaveUtilisationDepartmentInfo(int txtClientID, DateTime dtTillDate)
        {
            List<LeaveInfoDepartmentInfoChartData> objLeaveInfoDepartmentInfoChartData = new List<LeaveInfoDepartmentInfoChartData>();

            DataTable dt = new DataTable();

            OleDbConnection conn = null;

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM qryDepWiseDBChart WHERE ClientID = " + txtClientID + " AND EffectiveDate <= #" + dtTillDate.ToString("yyyy-MM-dd") + "#;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objLeaveInfoDepartmentInfoChartData = JsonConvert.DeserializeObject<List<LeaveInfoDepartmentInfoChartData>>(DataTableToJSon);
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

            return objLeaveInfoDepartmentInfoChartData;
        }

        public List<LeaveInfoEmpWiseChartData> GetEmpWiseLeaveUtilisationInfo(int txtClientID, int txtDepartmentID, DateTime dtTillDate)
        {
            List<LeaveInfoEmpWiseChartData> objLeaveInfoEmpWiseChartData = new List<LeaveInfoEmpWiseChartData>();

            DataTable dt = new DataTable();

            OleDbConnection conn = null;

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT TOP 3 * FROM qryEmpWiseDBChart WHERE ClientID = " + txtClientID + " AND DepartmentID = " + txtDepartmentID + " AND EffectiveDate <= #" + dtTillDate.ToString("yyyy-MM-dd") + "# ORDER BY TotalLeaveBalance DESC;";
                strQuery = "SELECT TOP 3 " + 
                                    " EmpID, EmpName, DepartmentID, TotalLeaveAllotted, TotalLeaveAvailed, TotalLeaveBalance, 'Highest Balance' AS BalanceCategory " +
                                " FROM " +
                                    " qryEmpWiseDBChart " +
                                " WHERE " +
                                    " ClientID = " + txtClientID +
                                    " AND DepartmentID = " + txtDepartmentID +
                                    " AND EffectiveDate <= #" + dtTillDate.ToString("yyyy-MM-dd") + "# " +
                                " ORDER BY " +
                                    " TotalLeaveBalance DESC, EmpID Asc " +
                                " UNION ALL " +
                                " SELECT TOP 3 " +
                                    " EmpID, EmpName, DepartmentID, TotalLeaveAllotted, TotalLeaveAvailed, TotalLeaveBalance, 'Lowest Balance' AS BalanceCategory " +
                                " FROM " +
                                    " qryEmpWiseDBChart " +
                                " WHERE " +
                                    " ClientID = " + txtClientID +
                                    " AND DepartmentID = " + txtDepartmentID +
                                    " AND EffectiveDate <= #" + dtTillDate.ToString("yyyy-MM-dd") + "# " +
                                    " AND TotalLeaveBalance > 0 " +
                                " ORDER BY " +
                                    " TotalLeaveBalance ASC, EmpID Asc " +
                                " UNION ALL " +
                                " SELECT TOP 3 " +
                                    " EmpID, EmpName, DepartmentID, TotalLeaveAllotted, TotalLeaveAvailed, TotalLeaveBalance, 'Zero Balance' AS BalanceCategory " +
                                " FROM " +
                                    " qryEmpWiseDBChart " +
                                " WHERE " +
                                    " ClientID = " + txtClientID +
                                    " AND DepartmentID = " + txtDepartmentID +
                                    " AND EffectiveDate <= #" + dtTillDate.ToString("yyyy-MM-dd") + "# " +
                                    " AND TotalLeaveBalance = 0 " +
                                " ORDER BY " +
                                    " EmpID ASC;";
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objLeaveInfoEmpWiseChartData = JsonConvert.DeserializeObject<List<LeaveInfoEmpWiseChartData>>(DataTableToJSon);
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

            return objLeaveInfoEmpWiseChartData;
        }
    }
}
