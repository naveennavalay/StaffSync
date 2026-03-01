using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;

namespace dbStaffSync
{
    public class clsDashboardWidgetData
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public DataTable GetDepartmentExposure(int txtClientID, int txtFinYearID)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM qryDepartmentExposureBase WHERE ClientID = " + txtClientID + " AND FinYearID " + txtFinYearID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
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

            return dt;
        }

        public DataTable GetAdvanceSummary(int txtClientID, int txtFinYearID)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " +
                                        " qryAdvanceApprovedRecoverBalanceStatement.TotalAdvances, " +
                                        " qryAdvanceApprovedRecoverBalanceStatement.TotalSanctioned, " +
                                        " qryAdvanceApprovedRecoverBalanceStatement.TotalRecovered, " +
                                        " qryAdvanceApprovedRecoverBalanceStatement.TotalOutstanding " +
                                    " FROM " +
                                        " qryAdvanceApprovedRecoverBalanceStatement " +
                                    " WHERE " +
                                        " qryAdvanceApprovedRecoverBalanceStatement.[ClientID] = " + txtClientID +
                                        " AND qryAdvanceApprovedRecoverBalanceStatement.[FinYearID] = " + txtFinYearID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
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

            return dt;
        }

        public List<AdvanceRiskBaseInfo> GetAdvanceRiskBaseInfo(int txtClientID, int txtFinYearID)
        {
            List<AdvanceRiskBaseInfo> objAdvanceRiskBaseInfoList = new List<AdvanceRiskBaseInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " + 
                                        " qryAdvanceRiskBase.EmpID, " + 
                                        " qryAdvanceRiskBase.EmpCode, " + 
                                        " qryAdvanceRiskBase.EmpName, " + 
                                        " qryAdvanceRiskBase.DesignationTitle, " + 
                                        " qryAdvanceRiskBase.DepartmentTitle, " + 
                                        " qryAdvanceRiskBase.AdvanceTypeID, " + 
                                        " qryAdvanceRiskBase.AdvanceTypeTitle, " + 
                                        " qryAdvanceRiskBase.EmpAdvanceRequestID, " + 
                                        " qryAdvanceRiskBase.EmpAdvReqCode, " + 
                                        " qryAdvanceRiskBase.AdvanceAmount, " + 
                                        " qryAdvanceRiskBase.AdvanceStartDate, " + 
                                        " qryAdvanceRiskBase.AdvanceEndDate, " + 
                                        " qryAdvanceRiskBase.RemainingBalance, " + 
                                        " qryAdvanceRiskBase.TotalRecovered, " + 
                                        " qryAdvanceRiskBase.AdvanceAgeDays, " + 
                                        " qryAdvanceRiskBase.ClientID, " + 
                                        " qryAdvanceRiskBase.FinYearID " + 
                                    " FROM " + 
                                        " qryAdvanceRiskBase " + 
                                    " WHERE " + 
                                         " qryAdvanceRiskBase.ClientID = " + txtClientID + " AND qryAdvanceRiskBase.FinYearID = " + txtFinYearID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAdvanceRiskBaseInfoList = JsonConvert.DeserializeObject<List<AdvanceRiskBaseInfo>>(DataTableToJSon);
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

            return objAdvanceRiskBaseInfoList;
        }

        public DataTable GetAgingDistributionInfo(int txtClientID, int txtFinYearID)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " + 
                                        " qryAgingDistribution.AgingBucket, " + 
                                        " qryAgingDistribution.AdvanceCount " + 
                                  " FROM " + 
                                        " qryAgingDistribution " + 
                                  " WHERE " + 
                                        " qryAgingDistribution.[clientid] = " + txtClientID + " AND qryAgingDistribution.[FinYearID] = " + txtFinYearID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
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

            return dt;
        }

        public int GetActiveEmployeesCount(int txtClientID, int txtFinYearID)
        {
            int intActiveEmployeesCount = 0;

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();
                DataTable dt = new DataTable();

                string strQuery = "SELECT " +
                                        " Count(EmpMas.EmpID) AS PresentEmployeesCount " +
                                    " FROM " +
                                        " FinYearMas " +
                                        " INNER JOIN ( " +
                                            " ClientMas " +
                                            " INNER JOIN EmpMas ON ClientMas.ClientID = EmpMas.ClientID " +
                                        " ) ON FinYearMas.FinYearID = ClientMas.FinYearID " +
                                    " WHERE " +
                                        " (" +
                                            " ((EmpMas.IsActive) = True) " +
                                            " AND ((EmpMas.IsDeleted) = False) " +
                                            " AND ((ClientMas.ClientID) = " + txtClientID + ") " +
                                            " AND ((FinYearMas.FinYearID) = " + txtFinYearID + ") " +
                                        " );";

                //" EmpSalMas.EmpSalDate"  = #" + txtMasterDataDate.ToString("dd-MMM-yyyy") + "# " + 

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                object result = cmd.ExecuteScalar();
                int a = result == null ? 0 : Convert.ToInt32(result);

                if (a > 0)
                    intActiveEmployeesCount = a;
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

            return intActiveEmployeesCount;
        }

        public int GetTotalEmployeesPresent(int txtClientID, int txtFinYearID)
        {
            int intTotalEmployeesPresentCount = 0;

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();
                DataTable dt = new DataTable();

                string strQuery = "SELECT " + 
                                            " COUNT(EmpMas.EmpID) AS PresentEmployeesCount " + 
                                        " FROM " + 
                                            " ( " + 
                                                " FinYearMas " + 
                                                " INNER JOIN ( " + 
                                                    " ClientMas " + 
                                                     " INNER JOIN EmpMas ON ClientMas.ClientID = EmpMas.ClientID " + 
                                                " ) ON FinYearMas.FinYearID = ClientMas.FinYearID " + 
                                            " ) " + 
                                            " INNER JOIN EmpDailyAttendanceInfo ON EmpMas.EmpID = EmpDailyAttendanceInfo.EmpID " + 
                                        " WHERE " + 
                                            " EmpDailyAttendanceInfo.AttStatus = 'Present'" +
                                            " AND EmpDailyAttendanceInfo.AttDate = Date() " +
                                            " AND EmpMas.IsActive = True " + 
                                            " AND EmpMas.IsDeleted = False " + 
                                        " AND ClientMas.ClientID = " + txtClientID +
                                        " AND FinYearMas.FinYearID = " + txtFinYearID;
                //" EmpSalMas.EmpSalDate"  = #" + txtMasterDataDate.ToString("dd-MMM-yyyy") + "# " + 

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                object result = cmd.ExecuteScalar();
                int a = result == null ? 0 : Convert.ToInt32(result);

                if (a > 0)
                    intTotalEmployeesPresentCount = a;
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

            return intTotalEmployeesPresentCount;
        }

        public int GetTotalEmployeesOnLeave(int txtClientID, int txtFinYearID)
        {
            int intTotalEmployeesPresentCount = 0;

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();
                DataTable dt = new DataTable();

                string strQuery = "SELECT " +
                                        " Count(EmpMas.EmpID) AS TotalPendingApprovalRequest " +
                                    " FROM " +
                                        " FinYearMas " +
                                        " INNER JOIN ( " +
                                            " ( " +
                                                " ClientMas " +
                                                " INNER JOIN EmpMas ON ClientMas.ClientID = EmpMas.ClientID " +
                                            " ) " +
                                            " INNER JOIN EmpLeaveTransMas ON EmpMas.EmpID = EmpLeaveTransMas.EmpID " +
                                        " ) ON FinYearMas.FinYearID = ClientMas.FinYearID " +
                                    " WHERE " +
                                        " ( " +
                                            " ( " +
                                                " (EmpLeaveTransMas.LeaveApprovalComments) = 'Approved : Approved'" +
                                            " ) " +
                                            " AND ( " +
                                                " (EmpLeaveTransMas.LeaveRejectionComments) = 'Not Rejected'" +
                                            " ) " +
                                            " AND ( " +
                                                " (EmpLeaveTransMas.ActualLeaveDateFrom) = Date() " +
                                            " ) " +
                                            " AND ((EmpMas.IsActive) = True) " +
                                            " AND ((EmpMas.IsDeleted) = False) " +
                                            " AND ((ClientMas.ClientID) = " + txtClientID + ") " +
                                            " AND ((FinYearMas.FinYearID) = " + txtFinYearID + "));";

                //" EmpSalMas.EmpSalDate"  = #" + txtMasterDataDate.ToString("dd-MMM-yyyy") + "# " + 

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                object result = cmd.ExecuteScalar();
                int a = result == null ? 0 : Convert.ToInt32(result);

                if (a > 0)
                    intTotalEmployeesPresentCount = a;
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

            return intTotalEmployeesPresentCount;
        }

        public int GetCountOfAllEmployeesBirthdayCountToday(int txtClientID, int txtFinYearID)
        {
            int intTotalEmployeesPresentCount = 0;

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();
                DataTable dt = new DataTable();

                string strQuery = "SELECT " +
                                            " Count(EmpMas.EmpID) AS WorkAnniversaryTodayCount " +
                                        " FROM " +
                                            " FinYearMas " +
                                            " INNER JOIN ( " +
                                                " ( " +
                                                    " DesigMas " +
                                                    " INNER JOIN ( " +
                                                        " DepMas " +
                                                        " INNER JOIN ( " +
                                                            " ClientMas " +
                                                            " INNER JOIN EmpMas ON ClientMas.ClientID = EmpMas.ClientID " +
                                                        " ) ON DepMas.DepartmentID = EmpMas.DepartmentID " +
                                                    " ) ON DesigMas.DesignationID = EmpMas.EmpDesignationID " +
                                                " ) " +
                                                " INNER JOIN PersonalInfoMas ON EmpMas.EmpID = PersonalInfoMas.EmpID " +
                                            " ) ON FinYearMas.FinYearID = ClientMas.FinYearID " +
                                        " WHERE " +
                                            " ClientMas.ClientID = " + txtClientID +
                                            " AND FinYearMas.FinYearID = " + txtFinYearID +
                                            " AND EmpMas.IsActive = True " +
                                            " AND EmpMas.IsDeleted = False " +
                                            " AND Month(PersonalInfoMas.DOJ) = Month(Date()) " +
                                            " AND Day(PersonalInfoMas.DOJ) = Day(Date());";

                //" EmpSalMas.EmpSalDate"  = #" + txtMasterDataDate.ToString("dd-MMM-yyyy") + "# " + 

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                object result = cmd.ExecuteScalar();
                int a = result == null ? 0 : Convert.ToInt32(result);

                if (a > 0)
                    intTotalEmployeesPresentCount = a;
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

            return intTotalEmployeesPresentCount;
        }

        public int GetCountOfAllEmployeesWorkAnniversaryCountToday(int txtClientID, int txtFinYearID)
        {
            int intTotalEmployeesPresentCount = 0;

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();
                DataTable dt = new DataTable();

                string strQuery = "SELECT " +
                                        " Count(EmpMas.EmpID) AS BirthdayTodayCount " +
                                    " FROM " +
                                        " FinYearMas " +
                                        " INNER JOIN ( " +
                                            " ( " +
                                                " DesigMas " +
                                                " INNER JOIN ( " +
                                                    " DepMas " +
                                                    " INNER JOIN ( " +
                                                        " ClientMas " +
                                                        " INNER JOIN EmpMas ON ClientMas.ClientID = EmpMas.ClientID " +
                                                    " ) ON DepMas.DepartmentID = EmpMas.DepartmentID " +
                                                " ) ON DesigMas.DesignationID = EmpMas.EmpDesignationID " +
                                            " ) " +
                                            " INNER JOIN PersonalInfoMas ON EmpMas.EmpID = PersonalInfoMas.EmpID " +
                                        " ) ON FinYearMas.FinYearID = ClientMas.FinYearID " +
                                    " WHERE " +
                                        " ClientMas.ClientID = " + txtClientID +
                                        " AND FinYearMas.FinYearID = " + txtFinYearID +
                                        " AND EmpMas.IsActive = True " +
                                        " AND EmpMas.IsDeleted = False " +
                                        " AND Month(PersonalInfoMas.DOB) = Month(Date()) " +
                                        " AND Day(PersonalInfoMas.DOB) = Day(Date())";

                //" EmpSalMas.EmpSalDate"  = #" + txtMasterDataDate.ToString("dd-MMM-yyyy") + "# " + 

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                object result = cmd.ExecuteScalar();
                int a = result == null ? 0 : Convert.ToInt32(result);

                if (a > 0)
                    intTotalEmployeesPresentCount = a;
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

            return intTotalEmployeesPresentCount;
        }

        public int GetPendingLeaveApprovalCount(int txtClientID, int txtFinYearID)
        {
            int intTotalPendingLeavesToApproval = 0;

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();
                DataTable dt = new DataTable();

                string strQuery = "SELECT " +
                                         " COUNT(EmpMas.EmpID) AS TotalPendingApprovalRequest, " +
                                        " ClientMas.ClientID, " +
                                        " FinYearMas.FinYearID " +
                                    " FROM " +
                                        " FinYearMas " +
                                        " INNER JOIN ( " +
                                            " ( " +
                                                " ClientMas " +
                                                " INNER JOIN EmpMas ON ClientMas.ClientID = EmpMas.ClientID " +
                                            " ) " +
                                            " INNER JOIN EmpLeaveTransMas ON EmpMas.EmpID = EmpLeaveTransMas.EmpID " +
                                        " ) ON FinYearMas.FinYearID = ClientMas.FinYearID " +
                                    " WHERE " +
                                        " EmpLeaveTransMas.LeaveApprovalComments = 'Not yet Approved'" +
                                        " AND EmpLeaveTransMas.LeaveRejectionComments = 'Not yet Rejected'" +
                                        " AND EmpLeaveTransMas.ActualLeaveDateFrom = Date() " +
                                        " AND EmpMas.IsActive = True " +
                                        " AND EmpMas.IsDeleted = False " +
                                    " GROUP BY " +
                                        " ClientMas.ClientID, " +
                                        " FinYearMas.FinYearID;";

                //" EmpSalMas.EmpSalDate"  = #" + txtMasterDataDate.ToString("dd-MMM-yyyy") + "# " + 

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                object result = cmd.ExecuteScalar();
                int a = result == null ? 0 : Convert.ToInt32(result);

                if (a > 0)
                    intTotalPendingLeavesToApproval = a;
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

            return intTotalPendingLeavesToApproval;
        }

        public int GetEmployeesWeeklyOffCount(int txtClientID, int txtFinYearID)
        {
            int intTotalPendingLeavesToApproval = 0;

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();
                DataTable dt = new DataTable();

                int todayIndex = ((int)DateTime.Today.DayOfWeek + 6) % 7 + 1;

                string strQuery = "SELECT " + 
                                        " Count(EmpMas.EmpID) AS WeeklyOffTodayCount " + 
                                    " FROM " +
                                        " FinYearMas " +
                                        " INNER JOIN ( " +
                                            " ( " +
                                                " WklyOffProfileInfo " + 
                                                " INNER JOIN ( " + 
                                                    " ( " + 
                                                        " ClientMas " + 
                                                        " INNER JOIN EmpMas ON ClientMas.ClientID = EmpMas.ClientID " + 
                                                    " ) " + 
                                                    " INNER JOIN EmpWeeklyOff ON EmpMas.EmpID = EmpWeeklyOff.EmpID " + 
                                                " ) ON WklyOffProfileInfo.WklyOffMasID = EmpWeeklyOff.WklyOffMasID " + 
                                            " ) " + 
                                            " INNER JOIN WklyOffProfileDetails ON WklyOffProfileInfo.WklyOffMasID = WklyOffProfileDetails.WklyOffMasID " + 
                                        " ) ON FinYearMas.FinYearID = ClientMas.FinYearID " + 
                                    " WHERE " + 
                                        " ( " + 
                                            " ( " + 
                                                " (WklyOffProfileDetails.WklyOffDay) = Weekday (Date(), 2) " + 
                                            " ) " + 
                                            " AND ((ClientMas.ClientID) = " + txtClientID + ") " + 
                                            " AND ((FinYearMas.FinYearID) = " + txtFinYearID + ") " + 
                                            " AND ((EmpMas.IsActive) = True) " + 
                                            " AND ((EmpMas.IsDeleted) = False) " + 
                                        ");";

                //" EmpSalMas.EmpSalDate"  = #" + txtMasterDataDate.ToString("dd-MMM-yyyy") + "# " + 

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                object result = cmd.ExecuteScalar();
                int a = result == null ? 0 : Convert.ToInt32(result);

                if (a > 0)
                    intTotalPendingLeavesToApproval = a;
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

            return intTotalPendingLeavesToApproval;
        }

        public DataTable GetUpcomingHolidays(int txtClientID, int txtFinYearID)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " +
                                        " TOP 3 PubHolType.PubHolTypeTitle, " +
                                        " PubHolidayDetails.PubHolidayTitle, " +
                                        " PubHolidayDetails.PubHolDate, " +
                                        " DateDiff(\"d\", Date(), PubHolidayDetails.PubHolDate) AS DaysRemaining " +
                                    " FROM " +
                                        " ( " +
                                            " FinYearMas " +
                                            " INNER JOIN (" +
                                                " ClientMas " +
                                                " INNER JOIN PublicHolidayMas ON ClientMas.ClientID = PublicHolidayMas.ClientID " +
                                            " ) ON FinYearMas.FinYearID = PublicHolidayMas.FinYearID " +
                                        " ) " +
                                        " INNER JOIN ( " +
                                            " PubHolType " +
                                            " INNER JOIN PubHolidayDetails ON PubHolType.PubHolTypeID = PubHolidayDetails.PubHolTypeID " +
                                        " ) ON PublicHolidayMas.PubHolMasID = PubHolidayDetails.PubHolMasID " +
                                    " WHERE " +
                                        " ( " +
                                            " ((PubHolidayDetails.PubHolDate) >= Date()) " +
                                            " AND((ClientMas.ClientID) = " + txtClientID + ") " +
                                            " AND((FinYearMas.FinYearID) = " + txtFinYearID + ") " +
                                        " ) " +
                                    " ORDER BY " +
                                        " PubHolidayDetails.PubHolDate;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
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

            return dt;
        }

        public DataTable GetAllEmployeesPresentList(int txtClientID, int txtFinYearID)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " + 
                                        " EmpMas.EmpID, " +
                                        " EmpMas.EmpCode, " +
                                        " EmpMas.EmpName, " +
                                        " DesigMas.DesignationTitle, " +
                                        " DepMas.DepartmentTitle, " +
                                        " PersonalInfoMas.ContactNumber1, " +
                                        " PersonalInfoMas.ContactNumber2, " +
                                        " EmpDailyAttendanceInfo.AttDate, " +
                                        " EmpDailyAttendanceInfo.AttStatus, " +
                                        " ClientMas.ClientID, " +
                                        " FinYearMas.FinYearID " +
                                    " FROM " +
                                        " ( " +
                                            " ( " +
                                                " FinYearMas " +
                                                " INNER JOIN ( " +
                                                    " DesigMas " +
                                                    " INNER JOIN ( " +
                                                        " DepMas " +
                                                        " INNER JOIN ( " +
                                                            " ClientMas " +
                                                            " INNER JOIN EmpMas ON ClientMas.ClientID = EmpMas.ClientID " +
                                                        " ) ON DepMas.DepartmentID = EmpMas.DepartmentID " +
                                                    " ) ON DesigMas.DesignationID = EmpMas.EmpDesignationID " +
                                                " ) ON FinYearMas.FinYearID = ClientMas.FinYearID " +
                                            " ) " +
                                            " INNER JOIN PersonalInfoMas ON EmpMas.EmpID = PersonalInfoMas.EmpID " +
                                        " ) " +
                                        " INNER JOIN EmpDailyAttendanceInfo ON EmpMas.EmpID = EmpDailyAttendanceInfo.EmpID " +
                                    " WHERE " +
                                        " ( " +
                                            " ((EmpDailyAttendanceInfo.AttDate) = Date()) " +
                                            " AND ((EmpDailyAttendanceInfo.AttStatus) = 'Present') " +
                                            " AND ((EmpMas.IsActive) = True) " +
                                            " AND ((EmpMas.IsDeleted) = False) " +
                                            " AND ((ClientMas.ClientID) = " + txtClientID + ") " +
                                            " AND ((FinYearMas.FinYearID) = " + txtFinYearID + ") " +
                                        " ) " +
                                    " ORDER BY " +
                                        " EmpMas.EmpID;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
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

            return dt;
        }

        public DataTable GetAllEmployeesWeeklyOffList(int txtClientID, int txtFinYearID)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                int todayIndex = ((int)DateTime.Today.DayOfWeek + 6) % 7 + 1;

                string strQuery = "SELECT " + 
                                        " EmpMas.EmpID, " + 
                                        " EmpMas.EmpCode, " + 
                                        " EmpMas.EmpName, " + 
                                        " DesigMas.DesignationTitle, " + 
                                        " DepMas.DepartmentTitle, " + 
                                        " PersonalInfoMas.ContactNumber1, " + 
                                        " PersonalInfoMas.ContactNumber2, " + 
                                        " WklyOffProfileInfo.WklyOffCode, " + 
                                        " WklyOffProfileInfo.WklyOffTitle, " + 
                                        " WklyOffProfileDetails.WklyOffDay, " + 
                                        " Choose(" + 
                                            "WklyOffProfileDetails.WklyOffDay, " + 
                                            "\"Monday\", " + 
                                            "\"Tuesday\", " + 
                                            "\"Wednesday\", " + 
                                            "\"Thursday\", " + 
                                            "\"Friday\", " + 
                                            "\"Saturday\", " + 
                                            "\"Sunday\" " +
                                         ") AS DayName, " + 
                                         " ClientMas.ClientID, " +
                                        " FinYearMas.FinYearID " +
                                    " FROM " +
                                        " ( " +
                                            " ( " +
                                                " WklyOffProfileInfo  " +
                                                " INNER JOIN (" +
                                                    " FinYearMas " +
                                                    " INNER JOIN ( " +
                                                        " ( " +
                                                            " DesigMas " +
                                                            " INNER JOIN (" +
                                                                " DepMas " +
                                                                " INNER JOIN (" +
                                                                    " ClientMas " +
                                                                    " INNER JOIN EmpMas ON ClientMas.ClientID = EmpMas.ClientID " +
                                                                " ) ON DepMas.DepartmentID = EmpMas.DepartmentID " +
                                                            " ) ON DesigMas.DesignationID = EmpMas.EmpDesignationID " +
                                                        " ) " +
                                                        " INNER JOIN EmpWeeklyOff ON EmpMas.EmpID = EmpWeeklyOff.EmpID " +
                                                    " ) ON FinYearMas.FinYearID = ClientMas.FinYearID " +
                                                " ) ON WklyOffProfileInfo.WklyOffMasID = EmpWeeklyOff.WklyOffMasID " +
                                            " ) " +
                                            " INNER JOIN WklyOffProfileDetails ON WklyOffProfileInfo.WklyOffMasID = WklyOffProfileDetails.WklyOffMasID " +
                                        " ) " +
                                        " INNER JOIN PersonalInfoMas ON EmpMas.EmpID = PersonalInfoMas.EmpID " +
                                    " WHERE " +
                                        " ( " +
                                            " ( " +
                                                " (WklyOffProfileDetails.WklyOffDay) = Weekday (Date(), 2) " +
                                            " ) " +
                                            " AND ((ClientMas.ClientID) = " + txtClientID + ") " +
                                            " AND ((FinYearMas.FinYearID) = " + txtFinYearID + ") " +
                                            " AND ((EmpMas.IsActive) = True) " +
                                            " AND ((EmpMas.IsDeleted) = False) " +
                                        " );";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
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

            return dt;
        }

        public DataTable GetTodaysEmployeesBirthdayList(int txtClientID, int txtFinYearID)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                int todayIndex = ((int)DateTime.Today.DayOfWeek + 6) % 7 + 1;

                string strQuery = "SELECT " + 
                                        "EmpMas.EmpID, " + 
                                        "EmpMas.EmpCode, " + 
                                        "EmpMas.EmpName, " + 
                                        "DesigMas.DesignationTitle, " + 
                                        "DepMas.DepartmentTitle, " +
                                        "PersonalInfoMas.ContactNumber1, " + 
                                        "PersonalInfoMas.ContactNumber2, " + 
                                        "PersonalInfoMas.DOB, " + 
                                        "ClientMas.ClientID, " + 
                                        "FinYearMas.FinYearID " + 
                                    " FROM " + 
                                        " FinYearMas " +
                                        " INNER JOIN ( " +
                                            " ( " +
                                                " DesigMas " +
                                                " INNER JOIN ( " +
                                                    " DepMas " +
                                                    " INNER JOIN ( " +
                                                        " ClientMas " +
                                                        " INNER JOIN EmpMas ON ClientMas.ClientID = EmpMas.ClientID " +
                                                    " ) ON DepMas.DepartmentID = EmpMas.DepartmentID " +
                                                " ) ON DesigMas.DesignationID = EmpMas.EmpDesignationID " +
                                            " ) " +
                                            " INNER JOIN PersonalInfoMas ON EmpMas.EmpID = PersonalInfoMas.EmpID " +
                                        " ) ON FinYearMas.FinYearID = ClientMas.FinYearID " +
                                    " WHERE " +
                                        " ( " +
                                            " ((ClientMas.ClientID) = " + txtClientID + ") " +
                                            " AND ((FinYearMas.FinYearID) = " + txtFinYearID + ") " +
                                            " AND ((EmpMas.IsActive) = True) " +
                                            " AND ((EmpMas.IsDeleted) = False) " +
                                            " AND ((Month([PersonalInfoMas].[DOB])) = Month(Date())) " +
                                            " AND ((Day([PersonalInfoMas].[DOB])) = Day(Date())) " +
                                        ");";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
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

            return dt;
        }

        public DataTable GetTodaysEmployeesWorkAnniversaryList(int txtClientID, int txtFinYearID)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                int todayIndex = ((int)DateTime.Today.DayOfWeek + 6) % 7 + 1;

                string strQuery = "SELECT " +
                                        "EmpMas.EmpID, " +
                                        "EmpMas.EmpCode, " +
                                        "EmpMas.EmpName, " +
                                        "DesigMas.DesignationTitle, " +
                                        "DepMas.DepartmentTitle, " +
                                        "PersonalInfoMas.ContactNumber1, " +
                                        "PersonalInfoMas.ContactNumber2, " +
                                        "PersonalInfoMas.DOJ, " +
                                        "ClientMas.ClientID, " +
                                        "FinYearMas.FinYearID " +
                                    " FROM " +
                                        " FinYearMas " +
                                        " INNER JOIN ( " +
                                            " ( " +
                                                " DesigMas " +
                                                " INNER JOIN ( " +
                                                    " DepMas " +
                                                    " INNER JOIN ( " +
                                                        " ClientMas " +
                                                        " INNER JOIN EmpMas ON ClientMas.ClientID = EmpMas.ClientID " +
                                                    " ) ON DepMas.DepartmentID = EmpMas.DepartmentID " +
                                                " ) ON DesigMas.DesignationID = EmpMas.EmpDesignationID " +
                                            " ) " +
                                            " INNER JOIN PersonalInfoMas ON EmpMas.EmpID = PersonalInfoMas.EmpID " +
                                        " ) ON FinYearMas.FinYearID = ClientMas.FinYearID " +
                                    " WHERE " +
                                        " ( " +
                                            " ((ClientMas.ClientID) = " + txtClientID + ") " +
                                            " AND ((FinYearMas.FinYearID) = " + txtFinYearID + ") " +
                                            " AND ((EmpMas.IsActive) = True) " +
                                            " AND ((EmpMas.IsDeleted) = False) " +
                                            " AND ((Month([PersonalInfoMas].[DOJ])) = Month(Date())) " +
                                            " AND ((Day([PersonalInfoMas].[DOJ])) = Day(Date())) " +
                                        ");";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
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

            return dt;
        }
    }
}
