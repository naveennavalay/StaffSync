using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace dbStaffSync
{
    public class clsAttendanceMas
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public clsAttendanceMas() { 

        }

        public DateTime getLastAttendanceDate()
        {
            DateTime dtLastAttendanceDate = new DateTime();
            try
            {
                DataTable dt = new DataTable();
                conn = dbStaffSync.openDBConnection();
                string strQuery = "SELECT MAX(AttDate) AS LastAttendanceDate FROM EmpDailyAttendanceInfo WHERE AttStatus = 'Present'";
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0 && dt.Rows[0]["LastAttendanceDate"] != DBNull.Value)
                {
                    dtLastAttendanceDate = Convert.ToDateTime(dt.Rows[0]["LastAttendanceDate"]);
                    dtLastAttendanceDate = dtLastAttendanceDate.AddDays(1);
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
            return dtLastAttendanceDate;
        }

        public List<EmployeeAttendanceInfo> GetDefaultEmployeeAttendanceInfo(int txtEmpID, DateTime dtSelectedMonth)
        {
            List<EmployeeAttendanceInfo> objEmployeeAttendanceInfo = new List<EmployeeAttendanceInfo>();
            List<EmployeeAttendanceInfo> objReturnEmployeeAttendanceInfoList = new List<EmployeeAttendanceInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                //DateTime.Now.Month
                //string strQuery = "SELECT * FROM qryEmpAttendanceList WHERE EmpID = " + txtEmpID + " AND AttDate = #" + dtSelectedMonth.Date.ToString("dd-MMM-yyyy") + "# ORDER BY AttDate, OrderID Asc";
                string strQuery = "SELECT * FROM qryEmpAttendanceList WHERE EmpID = " + txtEmpID + " AND Month(AttDate) = " + dtSelectedMonth.Date.Month + " AND Year(AttDate) = " + dtSelectedMonth.Date.Year + " ORDER BY AttDate, OrderID Asc";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmployeeAttendanceInfo = JsonConvert.DeserializeObject<List<EmployeeAttendanceInfo>>(DataTableToJSon);
                foreach (EmployeeAttendanceInfo indEmployeeAttendanceInfo in objEmployeeAttendanceInfo)
                {
                    objReturnEmployeeAttendanceInfoList.Add(new EmployeeAttendanceInfo
                    {
                        AttID = indEmployeeAttendanceInfo.AttID,
                        AttDate = indEmployeeAttendanceInfo.AttDate,
                        AttStatus = indEmployeeAttendanceInfo.AttStatus,
                        EmpID = indEmployeeAttendanceInfo.EmpID,
                        LeaveTRID = indEmployeeAttendanceInfo.LeaveTRID == null ? 0 : indEmployeeAttendanceInfo.LeaveTRID,
                        LeaveComments = indEmployeeAttendanceInfo.LeaveComments == null ? "" : indEmployeeAttendanceInfo.LeaveComments
                    });
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
            return objReturnEmployeeAttendanceInfoList;
        }

        public List<EmployeeAttendanceInfo> GetDefaultEmployeeAttendanceInfoForJobs(int txtEmpID, DateTime dtSelectedMonth)
        {
            List<EmployeeAttendanceInfo> objEmployeeAttendanceInfo = new List<EmployeeAttendanceInfo>();
            List<EmployeeAttendanceInfo> objReturnEmployeeAttendanceInfoList = new List<EmployeeAttendanceInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                //DateTime.Now.Month
                string strQuery = "SELECT * FROM qryEmpAttendanceList WHERE EmpID = " + txtEmpID + " AND AttDate = #" + dtSelectedMonth.Date.ToString("dd-MMM-yyyy") + "# ORDER BY AttDate, OrderID Asc";
                //string strQuery = "SELECT * FROM qryEmpAttendanceList WHERE EmpID = " + txtEmpID + " AND Month(AttDate) = " + dtSelectedMonth.Date.Month + " AND Year(AttDate) = " + dtSelectedMonth.Date.Year + " ORDER BY AttDate, OrderID Asc";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmployeeAttendanceInfo = JsonConvert.DeserializeObject<List<EmployeeAttendanceInfo>>(DataTableToJSon);
                foreach (EmployeeAttendanceInfo indEmployeeAttendanceInfo in objEmployeeAttendanceInfo)
                {
                    objReturnEmployeeAttendanceInfoList.Add(new EmployeeAttendanceInfo
                    {
                        AttID = indEmployeeAttendanceInfo.AttID,
                        AttDate = indEmployeeAttendanceInfo.AttDate,
                        AttStatus = indEmployeeAttendanceInfo.AttStatus,
                        EmpID = indEmployeeAttendanceInfo.EmpID,
                        LeaveTRID = indEmployeeAttendanceInfo.LeaveTRID == null ? 0 : indEmployeeAttendanceInfo.LeaveTRID,
                        LeaveComments = indEmployeeAttendanceInfo.LeaveComments == null ? "" : indEmployeeAttendanceInfo.LeaveComments
                    });
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
            return objReturnEmployeeAttendanceInfoList;
        }


        private EmployeeTotalWorkingInfo GetEmployeeMonthlyAttendanceTotals(
            int employeeId, DateTime dateFrom, DateTime dateToExclusive)
        {
            var result = new EmployeeTotalWorkingInfo
            {
                EmpID = employeeId,
                AttDate = dateFrom.Date,
                TotalDaysInMonth = Math.Max(0, (dateToExclusive.Date - dateFrom.Date).Days)
            };

            DataTable attendance = new DataTable();
            conn = dbStaffSync.openDBConnection();

            try
            {
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        "SELECT AttDate, AttStatus " +
                        "FROM EmpDailyAttendanceInfo " +
                        "WHERE EmpID = ? AND AttDate >= ? AND AttDate < ? " +
                        "ORDER BY AttDate";

                    cmd.Parameters.Add("?", OleDbType.Integer).Value = employeeId;
                    cmd.Parameters.Add("?", OleDbType.Date).Value = dateFrom.Date;
                    cmd.Parameters.Add("?", OleDbType.Date).Value = dateToExclusive.Date;

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                        adapter.Fill(attendance);
                }

                foreach (DataRow row in attendance.Rows)
                {
                    string status = row["AttStatus"] == DBNull.Value
                        ? string.Empty
                        : Convert.ToString(row["AttStatus"]).Trim();

                    if (status.Equals("Present", StringComparison.OrdinalIgnoreCase))
                    {
                        result.TotalPresent += 1m;
                        continue;
                    }

                    bool isLop = status.IndexOf("Loss Of Pay", StringComparison.OrdinalIgnoreCase) >= 0;
                    bool fullDay = status.IndexOf("Full Day", StringComparison.OrdinalIgnoreCase) >= 0;
                    bool firstHalf = status.IndexOf("First Half", StringComparison.OrdinalIgnoreCase) >= 0;
                    bool secondHalf = status.IndexOf("Second Half", StringComparison.OrdinalIgnoreCase) >= 0;

                    decimal duration = fullDay || (firstHalf && secondHalf)
                        ? 1m
                        : (firstHalf || secondHalf ? 0.5m : 0m);

                    if (isLop)
                        result.TotalLossOfPay += duration;
                    else
                        result.TotalPaidLeave += duration;
                }

                result.TotalPayableDays = Math.Max(0m,
                    result.TotalDaysInMonth - result.TotalLossOfPay);
                return result;
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }
        }

        public EmployeeTotalWorkingInfo GetEmployeeMonthlyWorkingDays(
            int txtEmpID, DateTime dtSelectedDateFrom, DateTime dtSelectedDateTo)
        {
            return GetEmployeeMonthlyAttendanceTotals(
                txtEmpID, dtSelectedDateFrom, dtSelectedDateTo);
        }

        public EmployeeTotalWorkingInfo getTotalPresentDays(
            int txtEmpID, DateTime dtSelectedDateFrom, DateTime dtSelectedDateTo)
        {
            return GetEmployeeMonthlyAttendanceTotals(
                txtEmpID, dtSelectedDateFrom, dtSelectedDateTo);
        }

        public EmployeeTotalWorkingInfo getTotalLossOfPayDays(
            int txtEmpID, DateTime dtSelectedDateFrom, DateTime dtSelectedDateTo)
        {
            return GetEmployeeMonthlyAttendanceTotals(
                txtEmpID, dtSelectedDateFrom, dtSelectedDateTo);
        }

        public EmployeeTotalWorkingInfo getTotalPaidLeave(
            int txtEmpID, DateTime dtSelectedDateFrom, DateTime dtSelectedDateTo)
        {
            return GetEmployeeMonthlyAttendanceTotals(
                txtEmpID, dtSelectedDateFrom, dtSelectedDateTo);
        }

        public EmployeeAttendanceInfo GetEmployeeSpecificDailyAttendanceInfo(int txtEmpID, DateTime dtSelectedDate)
        {
            List<EmployeeAttendanceInfo> objEmployeeSpecificDailyAttendanceInfo = new List<EmployeeAttendanceInfo>();

            try
            {
                DataTable dt = new DataTable();

                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " +
                                        " AttID, " +
                                        " AttDate, " +
                                        " AttStatus, " +
                                        " LeaveTRID, " +
                                        " LeaveComments, " +
                                        " EmpMas.EmpID " +
                                  " FROM " +
                                        " EmpMas INNER JOIN EmpDailyAttendanceInfo ON EmpMas.EmpID = EmpDailyAttendanceInfo.EmpID " +
                                  " WHERE " +
                                        " (((EmpDailyAttendanceInfo.AttDate) = #" + dtSelectedDate.ToString("dd-MMM-yyyy") + "#) " +
                                        " AND ((EmpMas.EmpID) = " + txtEmpID + ")) ORDER BY EmpDailyAttendanceInfo.AttID;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmployeeSpecificDailyAttendanceInfo = JsonConvert.DeserializeObject<List<EmployeeAttendanceInfo>>(DataTableToJSon);
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

            if (objEmployeeSpecificDailyAttendanceInfo.Count > 0)
                return objEmployeeSpecificDailyAttendanceInfo[0];
            else
                return new EmployeeAttendanceInfo();
        }


        public MonthlyAttendanceInfo GetEmployeeAttendanceHeaderInfo(int employeeId)
        {
            DataTable dt = new DataTable();
            try
            {
                conn = dbStaffSync.openDBConnection();
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT EmpID, EmpCode, EmpName FROM EmpMas WHERE EmpID = ?";
                    cmd.Parameters.Add("?", OleDbType.Integer).Value = employeeId;
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                        adapter.Fill(dt);
                }

                if (dt.Rows.Count == 0) return new MonthlyAttendanceInfo();
                string json = JsonConvert.SerializeObject(dt);
                var rows = JsonConvert.DeserializeObject<List<MonthlyAttendanceInfo>>(json);
                return rows == null ? new MonthlyAttendanceInfo() : rows.FirstOrDefault();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }
        }

        public List<MonthlyAttendanceInfo> EmployeeSpecificMonthlyAttendanceInfo(int EmpID, DateTime ReportForTheMonth)
        {
            List<MonthlyAttendanceInfo> objMonthlyAttendanceReport = new List<MonthlyAttendanceInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                //DateTime.Now.Month
                string strQuery = "SELECT * FROM qryMnthlyAttdInfo WHERE EmpID = " + EmpID + " AND AttdMonth = #" + ReportForTheMonth.Date.ToString("dd-MMM-yyyy") + "#";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objMonthlyAttendanceReport = JsonConvert.DeserializeObject<List<MonthlyAttendanceInfo>>(DataTableToJSon);
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

        public List<MonthlyAttendanceInfo> MonthlyAttendanceReport(DateTime ReportForTheMonth)
        {
            List<MonthlyAttendanceInfo> objMonthlyAttendanceReport = new List<MonthlyAttendanceInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                //DateTime.Now.Month
                string strQuery = "SELECT * FROM qryMnthlyAttdInfo WHERE AttdMonth = #" + ReportForTheMonth.Date.ToString("dd-MMM-yyyy") + "#";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objMonthlyAttendanceReport = JsonConvert.DeserializeObject<List<MonthlyAttendanceInfo>>(DataTableToJSon);

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

        public List<MonthlyAttendanceInfo> DailyBatchAttendance(int txtCompanyID, DateTime ReportForTheMonth, string DayNumber)
        {
            List<MonthlyAttendanceInfo> objMonthlyAttendanceReport = new List<MonthlyAttendanceInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                //DateTime.Now.Month
                //string strQuery = "SELECT ClientID, EmpID, EmpCode, EmpName, DesignationTitle, DepartmentTitle, qryMnthlyAttdInfo.SlNo, 'Present' AS " + DayNumber + " FROM qryMnthlyAttdInfo WHERE ClientID = " + txtCompanyID + " AND AttdMonth = #" + ReportForTheMonth.Date.ToString("dd-MMM-yyyy") + "#";

                string strQuery =
                    "SELECT " +
                    "    ClientMas.ClientID, " +
                    "    EmpMas.EmpID, " +
                    "    EmpMas.EmpCode, " +
                    "    EmpMas.EmpName, " +
                    "    DesigMas.DesignationTitle, " +
                    "    DepMas.DepartmentTitle, " +

                    "    CLng(" +
                    "        IIf(" +
                    "            IsNull(" +
                    "                DMax(" +
                    "                    'SlNo', " +
                    "                    'qryMnthlyAttdInfo', " +
                    "                    'EmpID=' & EmpMas.EmpID & " +
                    "                    ' AND ClientID=" + txtCompanyID + "'" +
                    "                    & ' AND AttdMonth=#" +
                                         ReportForTheMonth.Date.ToString("dd-MMM-yyyy") +
                                         "#'" +
                    "                )" +
                    "            ), " +
                    "            0, " +
                    "            DMax(" +
                    "                'SlNo', " +
                    "                'qryMnthlyAttdInfo', " +
                    "                'EmpID=' & EmpMas.EmpID & " +
                    "                ' AND ClientID=" + txtCompanyID + "'" +
                    "                & ' AND AttdMonth=#" +
                                     ReportForTheMonth.Date.ToString("dd-MMM-yyyy") +
                                     "#'" +
                    "            )" +
                    "        )" +
                    "    ) AS SlNo, " +

                    "    'Present' AS " + DayNumber + " " +

                    "FROM " +
                    "    ( " +
                    "        ( " +
                    "            ClientMas " +
                    "            INNER JOIN EmpMas " +
                    "                ON ClientMas.ClientID = EmpMas.ClientID " +
                    "        ) " +
                    "        INNER JOIN DepMas " +
                    "            ON EmpMas.DepartmentID = DepMas.DepartmentID " +
                    "    ) " +
                    "    INNER JOIN DesigMas " +
                    "        ON EmpMas.EmpDesignationID = DesigMas.DesignationID " +

                    "WHERE " +
                    "    ClientMas.ClientID = " + txtCompanyID + " " +
                    "    AND EmpMas.EmpID > 1 " +
                    "    AND EmpMas.IsActive = True " +
                    "    AND EmpMas.IsDeleted = False " +
                    "ORDER BY " +
                    "    EmpMas.EmpID, " +
                    "    EmpMas.EmpName;";

                strQuery = @"SELECT
                                ClientMas.ClientID,
                                EmpMas.EmpID,
                                EmpMas.EmpCode,
                                EmpMas.EmpName,
                                DesigMas.DesignationTitle,
                                DepMas.DepartmentTitle,
                                CLng (
                                    IIf(
                                        IsNull(
                                            DMax (
                                                'SlNo',
                                                'qryMnthlyAttdInfo',
                                                'EmpID=' & EmpMas.EmpID & ' AND ClientID=' & " + txtCompanyID + @" & ' AND AttdMonth=#" + ReportForTheMonth.Date.ToString("dd-MMM-yyyy") + @"#'
                                            )
                                        ),
                                        0,
                                        DMax (
                                            'SlNo',
                                            'qryMnthlyAttdInfo',
                                            'EmpID=' & EmpMas.EmpID & ' AND ClientID=' & " + txtCompanyID + @" & ' AND AttdMonth=#" + ReportForTheMonth.Date.ToString("dd-MMM-yyyy") + @"#'
                                        )
                                    )
                                ) AS SlNo,
                                'Present' AS " + DayNumber + @",
                                ShiftMas.ShiftStart,
                                ShiftMas.ShiftEnd
                            FROM
                                ShiftMas
                                INNER JOIN (
                                    (
                                        DesigMas
                                        INNER JOIN (
                                            DepMas
                                            INNER JOIN (
                                                ClientMas
                                                INNER JOIN EmpMas ON ClientMas.ClientID = EmpMas.ClientID
                                            ) ON DepMas.DepartmentID = EmpMas.DepartmentID
                                        ) ON DesigMas.DesignationID = EmpMas.EmpDesignationID
                                    )
                                    INNER JOIN EmpShiftInfo ON EmpMas.EmpID = EmpShiftInfo.EmpID
                                ) ON ShiftMas.ShiftID = EmpShiftInfo.ShiftID
                            WHERE
                                (
                                    ((ClientMas.ClientID) = " + txtCompanyID + @")
                                    AND ((EmpMas.EmpID) > 1)
                                    AND ((EmpMas.IsActive) = True)
                                    AND ((EmpMas.IsDeleted) = False)
                                )
                            ORDER BY
                                EmpMas.EmpID,
                                EmpMas.EmpName;";

                strQuery = @"SELECT
                                    ClientMas.ClientID,
                                    EmpMas.EmpID,
                                    EmpMas.EmpCode,
                                    EmpMas.EmpName,
                                    DesigMas.DesignationTitle,
                                    DepMas.DepartmentTitle,
                                    CLng (
                                        IIf(
                                            IsNull(
                                                DMax (
                                                    'SlNo',
                                                    'qryMnthlyAttdInfo',
                                                    'EmpID=' & EmpMas.EmpID & ' AND ClientID=' & 1 & ' AND AttdMonth=#01-Nov-2026#'
                                                )
                                            ),
                                            0,
                                            DMax (
                                                'SlNo',
                                                'qryMnthlyAttdInfo',
                                                'EmpID=' & EmpMas.EmpID & ' AND ClientID=' & 1 & ' AND AttdMonth=#01-Nov-2026#'
                                            )
                                        )
                                    ) AS SlNo,
                                    'Present' AS Day16,
                                    ShiftMas.ShiftStart,
                                    ShiftMas.ShiftEnd
                                FROM
                                    (
                                        ShiftMas
                                        INNER JOIN (
                                            (
                                                DesigMas
                                                INNER JOIN (
                                                    DepMas
                                                    INNER JOIN (
                                                        ClientMas
                                                        INNER JOIN EmpMas ON ClientMas.ClientID = EmpMas.ClientID
                                                    ) ON DepMas.DepartmentID = EmpMas.DepartmentID
                                                ) ON DesigMas.DesignationID = EmpMas.EmpDesignationID
                                            )
                                            INNER JOIN EmpShiftInfo ON EmpMas.EmpID = EmpShiftInfo.EmpID
                                        ) ON ShiftMas.ShiftID = EmpShiftInfo.ShiftID
                                    )
                                    INNER JOIN PersonalInfoMas ON EmpMas.EmpID = PersonalInfoMas.EmpID
                                WHERE
                                    (
                                        ((ClientMas.ClientID) = " + txtCompanyID + @")
                                        AND ((EmpMas.EmpID) > 1)
                                        AND ((EmpMas.IsActive) = True)
                                        AND ((EmpMas.IsDeleted) = False)
                                        AND ((EmpMas.ResignedStatus) = False)
                                        AND ((PersonalInfoMas.GoldProfile) = False)
                                    )
                                ORDER BY
                                    EmpMas.EmpID,
                                    EmpMas.EmpName;";
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objMonthlyAttendanceReport = JsonConvert.DeserializeObject<List<MonthlyAttendanceInfo>>(DataTableToJSon);
                if(objMonthlyAttendanceReport.Count == 0)
                {
                    strQuery = "SELECT ClientID, EmpID, EmpCode, EmpName, DesignationTitle, DepartmentTitle, qryMnthlyAttdInfo.SlNo, 'Present' AS " + DayNumber + " FROM qryMnthlyAttdInfo WHERE ClientID = " + txtCompanyID;

                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strQuery;
                    cmd.ExecuteNonQuery();

                    da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);

                    DataTableToJSon = "";
                    DataTableToJSon = JsonConvert.SerializeObject(dt);
                    objMonthlyAttendanceReport = JsonConvert.DeserializeObject<List<MonthlyAttendanceInfo>>(DataTableToJSon);
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

            return objMonthlyAttendanceReport;
        }

        public List<MonthlyAttendanceSummaryInfo> getMonthlyAttendanceSummaryInfo(int ClientID, DateTime txtDTFrom, DateTime txtDTTo)
        {
            List<MonthlyAttendanceSummaryInfo> objMonthlyAttendanceSummaryInfo = new List<MonthlyAttendanceSummaryInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                //DateTime.Now.Month
                string strQuery = "SELECT " + 
                                        " Sum(IIf(StatusType = 'Present', 1, 0)) AS PresentEmployees, " +
                                        " Sum(IIf(StatusType = 'Leave', 1, 0)) AS LeaveEmployees, " +
                                        " Sum(IIf(StatusType = 'Half Leave', 1, 0)) AS HalfLeaveEmployees " +
                                    " FROM " +
                                        " ( " +
                                            " SELECT " +
                                                " EmpMas.EmpID, Max(EmpDailyAttendanceInfo.AttStatus) AS StatusType " +
                                            " FROM " +
                                                " ( " +
                                                    " ClientMas INNER JOIN EmpMas ON ClientMas.ClientID = EmpMas.ClientID " +
                                                " ) " +
                                                " INNER JOIN EmpDailyAttendanceInfo ON EmpMas.EmpID = EmpDailyAttendanceInfo.EmpID " +
                                            " WHERE " +
                                                " EmpDailyAttendanceInfo.AttDate BETWEEN #" + Convert.ToDateTime(txtDTFrom).ToString("dd-MMM-yyyy") + "# AND #" + Convert.ToDateTime(txtDTTo).ToString("dd-MMM-yyyy") + "# " +
                                                " AND EmpMas.EmpID > 1 " + 
                                                " AND EmpMas.IsActive = True " +
                                                " AND EmpMas.IsDeleted = False " +
                                                " AND ClientMas.ClientID = " + ClientID + 
                                            " GROUP BY " +
                                                " EmpMas.EmpID " + 
                                        " )";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objMonthlyAttendanceSummaryInfo = JsonConvert.DeserializeObject<List<MonthlyAttendanceSummaryInfo>>(DataTableToJSon);
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

            return objMonthlyAttendanceSummaryInfo;
        }

        public List<EmployeeAttendanceClockInfo> getDailyLoginLogoutDefaultInformation(int EmpID)
        {
            List<EmployeeAttendanceClockInfo> objEmployeeAttendanceClockInfo = new List<EmployeeAttendanceClockInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                //DateTime.Now.Month
                string strQuery = @"SELECT
                                        ShiftMas.ShiftID,
                                        ShiftMas.ShiftTitle,
                                        Format(ShiftMas.ShiftStart, ""hh:nn:ss AM/PM"") AS ShiftLoginTime,
                                        Format(ShiftMas.ShiftEnd, ""hh:nn:ss AM/PM"") AS ShiftLogoutTime,
                                        Format(ShiftMas.GracePeriod, ""hh:nn:ss AM/PM"") AS GracePeriod
                                    FROM
                                        ShiftMas
                                        INNER JOIN (
                                            EmpMas
                                            INNER JOIN EmpShiftInfo ON EmpMas.EmpID = EmpShiftInfo.EmpID
                                        ) ON ShiftMas.ShiftID = EmpShiftInfo.ShiftID
                                    WHERE
                                        (((EmpMas.EmpID) = " + EmpID + "));";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmployeeAttendanceClockInfo = JsonConvert.DeserializeObject<List<EmployeeAttendanceClockInfo>>(DataTableToJSon);
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

            return objEmployeeAttendanceClockInfo;
        }


        /// <summary>
        /// Retrieves the attendance row for one exact employee/date.
        /// This query intentionally starts from EmpDailyAttendanceInfo so a missing
        /// or changed shift assignment cannot hide an attendance record.
        /// Read-only; no attendance values are inserted or updated.
        /// </summary>
        public EmployeeAttendanceClockInfo GetEmployeeDailyAttendanceClockInfo(int EmpID, DateTime dtDate)
        {
            EmployeeAttendanceClockInfo result = null;
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT TOP 1
                                            A.AttID,
                                            A.AttDate,
                                            A.AttStatus,
                                            A.LeaveTRID,
                                            A.LoginTime,
                                            A.LateLoginTime,
                                            A.LogoutTime,
                                            A.LateLogoutTime,
                                            A.TotalTime
                                        FROM EmpDailyAttendanceInfo AS A
                                        WHERE A.EmpID = ?
                                          AND A.AttDate >= ?
                                          AND A.AttDate < ?
                                        ORDER BY A.AttID DESC";
                    // OleDb parameters are positional; keep this order aligned with the SQL.
                    cmd.Parameters.Add("?", OleDbType.Integer).Value = EmpID;
                    cmd.Parameters.Add("?", OleDbType.Date).Value = dtDate.Date;
                    cmd.Parameters.Add("?", OleDbType.Date).Value = dtDate.Date.AddDays(1);

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                        adapter.Fill(dt);
                }

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    result = new EmployeeAttendanceClockInfo
                    {
                        AttID = row["AttID"] == DBNull.Value ? 0 : Convert.ToInt32(row["AttID"]),
                        AttDate = row["AttDate"] == DBNull.Value ? dtDate.Date : Convert.ToDateTime(row["AttDate"]),
                        AttStatus = row["AttStatus"] == DBNull.Value ? null : Convert.ToString(row["AttStatus"]),
                        LoginTime = row["LoginTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["LoginTime"]),
                        LateLoginTime = row["LateLoginTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["LateLoginTime"]),
                        LogoutTime = row["LogoutTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["LogoutTime"]),
                        LateLogoutTime = row["LateLogoutTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["LateLogoutTime"]),
                        TotalTime = row["TotalTime"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(row["TotalTime"])
                    };

                    if (row.Table.Columns.Contains("LeaveTRID") && row["LeaveTRID"] != DBNull.Value)
                        result.Message = Convert.ToString(row["LeaveTRID"]);
                }

                // Release the attendance query connection before the separate shift lookup.
                conn = dbStaffSync.closeDBConnection();

                // Shift information is supplementary. It must not determine whether
                // the underlying daily attendance record is returned.
                List<EmployeeAttendanceClockInfo> shiftRows = getDailyLoginLogoutDefaultInformation(EmpID);
                EmployeeAttendanceClockInfo shift = shiftRows == null ? null : shiftRows.FirstOrDefault();
                if (shift != null)
                {
                    if (result == null) result = new EmployeeAttendanceClockInfo();
                    result.ShiftID = shift.ShiftID;
                    result.ShiftTitle = shift.ShiftTitle;
                    result.ShiftLoginTime = shift.ShiftLoginTime;
                    result.ShiftLogoutTime = shift.ShiftLogoutTime;
                    result.GracePeriod = shift.GracePeriod;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(
                    "GetEmployeeDailyAttendanceClockInfo failed for EmpID {0}, Date {1}: {2}",
                    EmpID, dtDate, ex);
                result = null;
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }

            return result;
        }

        public List<EmployeeAttendanceClockInfo> getDailyLoginLogoutInformation(int EmpID, DateTime dtDate)
        {
            List<EmployeeAttendanceClockInfo> objEmployeeAttendanceClockInfo = new List<EmployeeAttendanceClockInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"SELECT
                                            ShiftMas.ShiftID,
                                            ShiftMas.ShiftTitle,
                                            ShiftMas.ShiftStart AS ShiftLoginTime,
                                            ShiftMas.ShiftEnd AS ShiftLogoutTime,
                                            ShiftMas.GracePeriod,
                                            A.AttID,
                                            A.AttDate,
                                            A.AttStatus,
                                            A.LeaveTRID,
                                            A.LoginTime,
                                            A.LateLoginTime,
                                            A.LogoutTime,
                                            A.LateLogoutTime,
                                            A.TotalTime
                                        FROM
                                            (ShiftMas
                                            INNER JOIN (EmpMas
                                            INNER JOIN EmpShiftInfo ON EmpMas.EmpID = EmpShiftInfo.EmpID)
                                            ON ShiftMas.ShiftID = EmpShiftInfo.ShiftID)
                                            LEFT JOIN EmpDailyAttendanceInfo AS A
                                            ON EmpMas.EmpID = A.EmpID AND A.AttDate >= ? AND A.AttDate < ?
                                        WHERE
                                            EmpMas.EmpID = ?
                                        ORDER BY A.AttID DESC";

                    cmd.Parameters.Add("?", OleDbType.Date).Value = dtDate.Date;
                    cmd.Parameters.Add("?", OleDbType.Date).Value = dtDate.Date.AddDays(1);
                    cmd.Parameters.Add("?", OleDbType.Integer).Value = EmpID;

                    using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                        da.Fill(dt);
                }

                string json = JsonConvert.SerializeObject(dt);
                objEmployeeAttendanceClockInfo =
                    JsonConvert.DeserializeObject<List<EmployeeAttendanceClockInfo>>(json)
                    ?? new List<EmployeeAttendanceClockInfo>();
            }
            catch (Exception ex)
            {
                // Keep the DAL's existing return contract; caller receives an empty list on failure.
                conn = dbStaffSync.closeDBConnection();
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }

            return objEmployeeAttendanceClockInfo;
        }

        public List<IsEmployeeOnLeave> IsEmployeeOnLeave(int EmpID, DateTime dtDate)
        {
            List<IsEmployeeOnLeave> objIsEmployeeOnLeave = new List<IsEmployeeOnLeave>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                //DateTime.Now.Month
                string strQuery = @"SELECT
                                        EmpMas.EmpID,
                                        #" + dtDate.ToString("dd-MMM-yyyy") + @"# AS CheckDate,
                                        EmpDailyAttendanceInfo.AttID,
                                        EmpDailyAttendanceInfo.AttStatus,
                                        EmpLeaveTransMas.LeaveTRID,
                                        EmpLeaveTransMas.LeaveTypeID,
                                        EmpLeaveTransMas.LeaveMode,
                                        EmpLeaveTransMas.LeaveDuration,
                                        EmpLeaveTransMas.ActualLeaveDateFrom,
                                        EmpLeaveTransMas.ActualLeaveDateTo,
                                        IIf(
                                            EmpLeaveTransMas.LeaveTRID IS NOT NULL,
                                            IIf(
                                                EmpLeaveTransMas.LeaveMode = ""Full Day"",
                                                ""On Leave - Full Day"",
                                                EmpLeaveTransMas.LeaveMode & "" Leave; Attendance: "" & Nz (EmpDailyAttendanceInfo.AttStatus, ""Not Marked"")
                                            ),
                                            Nz (EmpDailyAttendanceInfo.AttStatus, ""Not Marked"")
                                        ) AS DayStatus
                                    FROM
                                        (
                                            (
                                                EmpMas
                                                LEFT JOIN EmpDailyAttendanceInfo ON (
                                                    EmpMas.EmpID = EmpDailyAttendanceInfo.EmpID
                                                    AND EmpDailyAttendanceInfo.AttDate = #" + dtDate.ToString("dd-MMM-yyyy") +  @"#
                                                )
                                            )
                                            LEFT JOIN EmpLeaveTransMas ON (
                                                EmpMas.EmpID = EmpLeaveTransMas.EmpID
                                                AND #" + dtDate.ToString("dd-MMM-yyyy") +  @"# BETWEEN EmpLeaveTransMas.ActualLeaveDateFrom AND EmpLeaveTransMas.ActualLeaveDateTo
                                                AND EmpLeaveTransMas.LeaveApprovalComments = ""Approved : Approved""
                                                AND EmpLeaveTransMas.LeaveRejectionComments = ""Not Rejected""
                                                AND EmpLeaveTransMas.Canceled = False
                                            )
                                        )
                                    WHERE
                                        EmpMas.EmpID = " + EmpID;

                strQuery = @"SELECT
                                EmpMas.EmpID,
                                #" + dtDate.ToString("dd-MMM-yyyy") + @"# AS CheckDate,
                                EmpDailyAttendanceInfo.AttID,
                                EmpDailyAttendanceInfo.AttStatus,
                                EmpLeaveTransMas.LeaveTRID,
                                EmpLeaveTransMas.LeaveTypeID,
                                EmpLeaveTransMas.LeaveMode,
                                EmpLeaveTransMas.LeaveDuration,
                                EmpLeaveTransMas.ActualLeaveDateFrom,
                                EmpLeaveTransMas.ActualLeaveDateTo,
                                IIf(
                                    EmpLeaveTransMas.LeaveTRID Is Not Null,
                                    IIf(
                                        EmpLeaveTransMas.LeaveMode = ""Full Day"",
                                        ""On Leave - Full Day"",

                                        EmpLeaveTransMas.LeaveMode &
                                        "" Leave; Attendance: "" &
                                        IIf(
                                            IsNull(EmpDailyAttendanceInfo.AttStatus),
                                            ""Not Marked"",
                                            EmpDailyAttendanceInfo.AttStatus
                                        )
                                    ),
                                    IIf(
                                        IsNull(EmpDailyAttendanceInfo.AttStatus),
                                        ""Not Marked"",
                                        EmpDailyAttendanceInfo.AttStatus
                                    )
                                ) AS DayStatus
                            FROM
                                (
                                    (
                                        EmpMas
                                        LEFT JOIN EmpDailyAttendanceInfo
                                        ON
                                            (
                                                EmpMas.EmpID = EmpDailyAttendanceInfo.EmpID
                                                AND EmpDailyAttendanceInfo.AttDate = #" + dtDate.ToString("dd-MMM-yyyy") + @"#
                                            )
                                    )
                                    LEFT JOIN EmpLeaveTransMas
                                    ON
                                        (
                                            EmpMas.EmpID = EmpLeaveTransMas.EmpID

                                            AND #" + dtDate.ToString("dd-MMM-yyyy") + @"# BETWEEN
                                                EmpLeaveTransMas.ActualLeaveDateFrom
                                                AND EmpLeaveTransMas.ActualLeaveDateTo

                                            AND EmpLeaveTransMas.LeaveApprovalComments =
                                                ""Approved : Approved""

                                            AND EmpLeaveTransMas.LeaveRejectionComments =
                                                ""Not Rejected""

                                            AND EmpLeaveTransMas.Canceled = False
                                        )
                                )
                            WHERE
                                EmpMas.EmpID = " + EmpID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objIsEmployeeOnLeave = JsonConvert.DeserializeObject<List<IsEmployeeOnLeave>>(DataTableToJSon);
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

            return objIsEmployeeOnLeave;
        }

        public int InsertDailyAttendance(int txtEmpID, DateTime AttendanceDate, string AttendanceStatus, int LeaveTRID)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("EmpDailyAttendanceInfo", "AttID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpDailyAttendanceInfo (AttID, EmpID, AttDate, AttStatus, LeaveTRID) VALUES " +
                 "(" + maxRowCount.Data + "," + txtEmpID + ",'" + AttendanceDate.ToString("dd-MMM-yyyy") + "','" + AttendanceStatus + "'," + LeaveTRID + ")";

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

        public int UpdateDailyAttendance(int txtEmpID, DateTime AttendanceDate, string AttendanceStatus, int LeaveTRID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpDailyAttendanceInfo SET AttStatus = '" + AttendanceStatus + "', LeaveTRID = " + LeaveTRID + 
                 " WHERE EmpID = " + txtEmpID + " AND AttDate = #" + AttendanceDate.ToString("dd-MMM-yyyy") + "#";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = affectedRows;
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

        public int UpdateDailyLogInTime(int txtEmpID, DateTime AttendanceDate, DateTime LoginTime, DateTime LateLoginTime)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpDailyAttendanceInfo SET LoginTime = #" + LoginTime.ToString("hh:mm:ss tt") + "#, LateLoginTime = #" + LateLoginTime.ToString("hh:mm:ss tt") + "#" +
                 " WHERE EmpID = " + txtEmpID + " AND AttDate = #" + AttendanceDate.ToString("dd-MMM-yyyy") + "#";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = affectedRows;
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

        public int UpdateDailyLogOutTime(int txtEmpID, DateTime AttendanceDate, DateTime LogoutTime,DateTime TotalTime, DateTime LateLogoutTime)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpDailyAttendanceInfo SET LogoutTime = #" + LogoutTime.ToString("hh:mm:ss tt") + "#, TotalTime = #" + TotalTime.ToString("hh:mm:ss tt") + "#, LateLogoutTime = #" + LateLogoutTime.ToString("hh:mm:ss tt") + "#" +
                 " WHERE EmpID = " + txtEmpID + " AND AttDate = #" + AttendanceDate.ToString("dd-MMM-yyyy") + "#";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = affectedRows;
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

        /// <summary>
        /// Returns the employee's configured weekly-off day numbers from EmpnWeeklyOff.
        /// Stored numbering is Monday=1 ... Saturday=6, Sunday=7.
        /// </summary>
        public List<int> GetEmployeeWeeklyOffDays(int employeeId)
        {
            var result = new List<int>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                using (OleDbCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    //cmd.CommandText =
                    //    "SELECT WklyOffDay " +
                    //    "FROM EmpnWeeklyOff " +
                    //    "WHERE EmpID = ? " +
                    //    "ORDER BY WklyOffDay";

                    cmd.CommandText = @"SELECT
                                            WklyOffProfileDetails.WklyOffDay
                                        FROM
                                            (
                                                WklyOffProfileInfo
                                                INNER JOIN EmpWeeklyOff ON WklyOffProfileInfo.WklyOffMasID = EmpWeeklyOff.WklyOffMasID
                                            )
                                            INNER JOIN WklyOffProfileDetails ON WklyOffProfileInfo.WklyOffMasID = WklyOffProfileDetails.WklyOffMasID
                                        WHERE
                                            (((EmpWeeklyOff.[EmpID]) = ?))
                                        ORDER BY
                                            WklyOffProfileDetails.WklyOffDay;";

                    cmd.Parameters.Add("?", OleDbType.Integer).Value = employeeId;

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                        adapter.Fill(dt);
                }

                foreach (DataRow row in dt.Rows)
                {
                    if (row["WklyOffDay"] == DBNull.Value)
                        continue;

                    int dayNumber = Convert.ToInt32(row["WklyOffDay"]);
                    if (dayNumber >= 1 && dayNumber <= 7 && !result.Contains(dayNumber))
                        result.Add(dayNumber);
                }

                return result;
            }
            finally
            {
                conn = dbStaffSync.closeDBConnection();
            }
        }

    }
}
