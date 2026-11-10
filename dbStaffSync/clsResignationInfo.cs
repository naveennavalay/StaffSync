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
    public class clsResignationInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public IsResignationAppliedInfo getIsResignationAppliedInfotatements(int txtEmpID)
        {
            List<IsResignationAppliedInfo> objIsResignationAppliedInfoList = new List<IsResignationAppliedInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = @"SELECT
                                            EmpResignationInfo.EmpResignID,
                                            EmpResignationInfo.EmpResignStatus
                                        FROM
                                            (
                                                EmpMas
                                                INNER JOIN PersonalInfoMas ON EmpMas.EmpID = PersonalInfoMas.EmpID
                                            )
                                            INNER JOIN EmpResignationInfo ON PersonalInfoMas.PersonalInfoID = EmpResignationInfo.PersonalInfoID
                                        WHERE
                                            (
                                                ((EmpResignationInfo.EmpResignStatus) > 0)
                                                AND ((EmpMas.EmpID) = " + txtEmpID + @")
                                            );";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objIsResignationAppliedInfoList = JsonConvert.DeserializeObject<List<IsResignationAppliedInfo>>(DataTableToJSon);
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

            if(objIsResignationAppliedInfoList.Count > 0)
                return objIsResignationAppliedInfoList[0];
            else
                return new IsResignationAppliedInfo();
        }

        public List<ResignedEmployeesList> getResignedEmployeeList(int ClientID)
        {
            List<ResignedEmployeesList> objResignedEmployeesList = new List<ResignedEmployeesList>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = @"SELECT
                                        EmpMas.EmpID,
                                        EmpMas.EmpCode,
                                        EmpMas.EmpName,
                                        DesigMas.DesignationTitle,
                                        DepMas.DepartmentTitle,
                                        PersonalInfoMas.ContactNumber1,
                                        PersonalInfoMas.ContactNumber2,
                                        PersonalInfoMas.PersonalInfoID,
                                        ResignationMasInfo.ResigMasID,
                                        ResignationMasInfo.ResigMasCode,
                                        ResignationMasInfo.ResigMasTitle,
                                        EmpResignationInfo.EmpResignID,
                                        EmpResignationInfo.EmpResignCode,
                                        EmpResignationInfo.EmpResignDate,
                                        EmpResignationInfo.EmpResignMoreInfo,
                                        EmpResignationInfo.EmpResignStatus,
                                        Switch (
                                            EmpResignationInfo.EmpResignStatus = 1,
                                            'In Progress',
                                            EmpResignationInfo.EmpResignStatus = 2,
                                            'Approved',
                                            EmpResignationInfo.EmpResignStatus = 3,
                                            'With Finance (Reimbursements)',
                                            EmpResignationInfo.EmpResignStatus = 4,
                                            'With Finance (Advances)',
                                            EmpResignationInfo.EmpResignStatus = 5,
                                            'With IT (Assets)',
                                            EmpResignationInfo.EmpResignStatus = 6,
                                            'Completed',
                                            EmpResignationInfo.EmpResignStatus = 7,
                                            'Withdrawn/Cancelled',
                                            True,
                                            'Unknown'
                                        ) AS EmpResignStatusTitle,
                                        EmpMas.IsActive,
                                        EmpMas.IsDeleted,
                                        EmpMas.ResignedStatus,
                                        ClientMas.ClientID
                                    FROM
                                        ResignationMasInfo
                                        INNER JOIN (
                                            (
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
                                                INNER JOIN PersonalInfoMas ON EmpMas.EmpID = PersonalInfoMas.EmpID
                                            )
                                            INNER JOIN EmpResignationInfo ON PersonalInfoMas.PersonalInfoID = EmpResignationInfo.PersonalInfoID
                                        ) ON ResignationMasInfo.ResigMasID = EmpResignationInfo.ResigMasID
                                    WHERE
                                        EmpMas.EmpID > 1
                                        AND EmpMas.IsActive = True
                                        AND EmpMas.IsDeleted = False
                                        AND EmpMas.ResignedStatus = False
                                        AND ClientMas.ClientID = " + ClientID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objResignedEmployeesList = JsonConvert.DeserializeObject<List<ResignedEmployeesList>>(DataTableToJSon);
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

            return objResignedEmployeesList;
        }

        public List<ResignationInfo> getResignationInfotatements()
        {
            List<ResignationInfo> objResignationReasonList = new List<ResignationInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = @"SELECT
                                        ResignationMasInfo.ResigMasID,
                                        ResignationMasInfo.ResigMasCode,
                                        ResignationMasInfo.ResigMasTitle,
                                        ResignationMasInfo.IsActive,
                                        ResignationMasInfo.IsDeleted
                                    FROM
                                        ResignationMasInfo
                                    WHERE
                                        (
                                            ((ResignationMasInfo.IsActive) = True)
                                            AND ((ResignationMasInfo.IsDeleted) = False)
                                        )
                                    ORDER BY
                                        ResignationMasInfo.ResigMasID;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objResignationReasonList = JsonConvert.DeserializeObject<List<ResignationInfo>>(DataTableToJSon);
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

            return objResignationReasonList;
        }

        public EmployeeSpecificResignationInfo getEmployeeSpecificEmployeeSpecificResignationInfo(int ResignationID)
        {
            List<EmployeeSpecificResignationInfo> objEmployeeSpecificResignationInfoList = new List<EmployeeSpecificResignationInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = @"SELECT
                                        EmpMas.EmpID,
                                        PersonalInfoMas.PersonalInfoID,
                                        ResignationMasInfo.ResigMasID,
                                        ResignationMasInfo.ResigMasCode,
                                        ResignationMasInfo.ResigMasTitle,
                                        EmpResignationInfo.EmpResignID,
                                        EmpResignationInfo.EmpResignCode,
                                        EmpResignationInfo.EmpResignDate,
                                        EmpResignationInfo.EmpResignMoreInfo,
                                        EmpResignationInfo.EmpResignApprovedBy1,
                                        EmpResignationInfo.EmpResignApprovalStatus1,
                                        EmpResignationInfo.EmpResignApprovalDate1,
                                        EmpResignationInfo.EmpResignApprovedBy2,
                                        EmpResignationInfo.EmpResignApprovalStatus2,
                                        EmpResignationInfo.EmpResignApprovalDate2,
                                        EmpResignationInfo.EmpResignStatus,
                                        EmpMas.IsActive AS ResignStatus
                                    FROM
                                        ResignationMasInfo
                                        INNER JOIN (
                                            (
                                                (
                                                    ClientMas
                                                    INNER JOIN EmpMas ON ClientMas.ClientID = EmpMas.ClientID
                                                )
                                                INNER JOIN PersonalInfoMas ON EmpMas.EmpID = PersonalInfoMas.EmpID
                                            )
                                            INNER JOIN EmpResignationInfo ON PersonalInfoMas.PersonalInfoID = EmpResignationInfo.PersonalInfoID
                                        ) ON ResignationMasInfo.ResigMasID = EmpResignationInfo.ResigMasID
                                    WHERE
                                        (
                                            ((EmpResignationInfo.EmpResignID) = " + ResignationID + @")
                                            AND ((EmpMas.IsActive) = True)
                                            AND ((EmpMas.IsDeleted) = False)
                                            AND ((EmpMas.ResignedStatus) = False)
                                        );";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmployeeSpecificResignationInfoList = JsonConvert.DeserializeObject<List<EmployeeSpecificResignationInfo>>(DataTableToJSon);
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

            if(objEmployeeSpecificResignationInfoList.Count > 0)
                return objEmployeeSpecificResignationInfoList[0];
            else
                return new EmployeeSpecificResignationInfo();
        }

        public int InsertEmployeeResignationInfo(int PersonalInfoID, int ResigMasID, string EmpResignMoreInfo, DateTime EmpResignDate, 
            DateTime EmpResignApprovalDate1, int EmpResignApprovedBy1, bool EmpResignApprovalStatus1, 
            DateTime EmpResignApprovalDate2, int EmpResignApprovedBy2, bool EmpResignApprovalStatus2, 
            int EmpResignFinalStatus, DateTime EmpFinalResignApprovalDate, 
            int EmpNoticePeriod, DateTime EmpLastWorkingDay, string EmpNoticePeriodServeStatus)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("EmpResignationInfo", "EmpResignID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpResignationInfo (EmpResignID, EmpResignCode, PersonalInfoID, ResigMasID, EmpResignMoreInfo, EmpResignDate, " + 
                    " EmpResignApprovalDate1, EmpResignApprovedBy1, EmpResignApprovalStatus1, " + 
                    " EmpResignApprovalDate2, EmpResignApprovedBy2, EmpResignApprovalStatus2, " + 
                    " EmpResignStatus, EmpFinalResignApprovalDate, EmpNoticePeriod, EmpLastWorkingDay, EmpNoticePeriodServeStatus) VALUES " +
                    "(" + maxRowCount.Data + ", '" + "RSGN-" + (maxRowCount.Data).ToString().PadLeft(4, '0').Trim() + "', " + PersonalInfoID + ", " + ResigMasID + ", '" + EmpResignMoreInfo + "', #" + EmpResignDate.ToString("dd-MMM-yyyy") + "#, " + 
                    "#" + EmpResignApprovalDate1.ToString("dd-MMM-yyyy") + "#, " + EmpResignApprovedBy1 + ", " + EmpResignApprovalStatus1 + ", " + 
                    "#" + EmpResignApprovalDate2.ToString("dd-MMM-yyyy") + "#, " + EmpResignApprovedBy2 + ", " + EmpResignApprovalStatus2 + ", " + 
                    EmpResignFinalStatus + ", #" + EmpFinalResignApprovalDate.ToString("dd-MMM-yyyy") + "#, " + EmpNoticePeriod + ", #" + EmpLastWorkingDay.ToString("dd-MMM-yyyy") + "#, '" + EmpNoticePeriodServeStatus + "')";
                    
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

        public int ApprovalResignationByReportingManager(int EmpResignID, DateTime EmpResignApprovalDate1, int EmpResignApprovedBy1, bool EmpResignApprovalStatus1, int EmpResignFinalStatus)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpResignationInfo SET " +
                    " EmpResignApprovalDate1 = #" + EmpResignApprovalDate1.ToString("dd-MMM-yyyy") + "#, EmpResignApprovedBy1 = " + EmpResignApprovedBy1 + ", EmpResignApprovalStatus1 = " + EmpResignApprovalStatus1 + ", EmpResignStatus = " +
                    EmpResignFinalStatus + " WHERE EmpResignID = " + EmpResignID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = EmpResignID;
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

        public int ApprovalResignationByHRManager(int EmpResignID, DateTime EmpResignApprovalDate2, int EmpResignApprovedBy2, bool EmpResignApprovalStatus2, int EmpResignFinalStatus)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpResignationInfo SET " +
                    " EmpResignApprovalDate1 = #" + EmpResignApprovalDate2.ToString("dd-MMM-yyyy") + "#, EmpResignApprovedBy2 = " + EmpResignApprovedBy2 + ", EmpResignApprovalStatus2 = " + EmpResignApprovalStatus2 + ", EmpResignStatus = " +
                    EmpResignFinalStatus + " WHERE EmpResignID = " + EmpResignID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = EmpResignID;
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

        public int UpdateApprovers(int EmpResignID, int EmpResignApprovedBy1, int EmpResignApprovedBy2)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = @"UPDATE EmpResignationInfo SET " +
                    " EmpResignApprovedBy1 = " + EmpResignApprovedBy1 + ", " +
                    " EmpResignApprovedBy2 = " + EmpResignApprovedBy2 +
                    " WHERE EmpResignID = " + EmpResignID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = EmpResignID;
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
