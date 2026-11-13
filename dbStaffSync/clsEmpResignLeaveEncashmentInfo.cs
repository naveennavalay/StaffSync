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
    public class clsEmpResignLeaveEncashmentInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();


        public EmpResignLeaveEncashmentInfo getEmpResignLeaveEncashmentInfotatements(int EmpResignID)
        {
            List<EmpResignLeaveEncashmentInfo> objEmpResignLeaveEncashmentInfo = new List<EmpResignLeaveEncashmentInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = @"SELECT
                                        EmpResignLeaveEncashmentInfo.LeaveEncashmentID,
                                        EmpResignLeaveEncashmentInfo.EmpResignID,
                                        EmpResignLeaveEncashmentInfo.EncashmentDate,
                                        EmpResignLeaveEncashmentInfo.TotalLeaveAllotted,
                                        EmpResignLeaveEncashmentInfo.TotalLeaveUtilised,
                                        EmpResignLeaveEncashmentInfo.TotalLeaveBalance,
                                        EmpResignLeaveEncashmentInfo.TotalEncashableLeave,
                                        EmpResignLeaveEncashmentInfo.DaysToEncash,
                                        EmpResignLeaveEncashmentInfo.EligibleSalary,
                                        EmpResignLeaveEncashmentInfo.DailySalaryRate,
                                        EmpResignLeaveEncashmentInfo.SalaryCalculationBasis,
                                        EmpResignLeaveEncashmentInfo.EncashmentAmount,
                                        EmpResignLeaveEncashmentInfo.TaxDeduction,
                                        EmpResignLeaveEncashmentInfo.NetEncashmentAmount,
                                        EmpResignLeaveEncashmentInfo.MoreDetails,
                                        EmpResignLeaveEncashmentInfo.EncashmentProcessStatus
                                    FROM
                                        EmpResignLeaveEncashmentInfo
                                    WHERE
                                        (((EmpResignLeaveEncashmentInfo.EmpResignID) = " + EmpResignID + ")); ";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmpResignLeaveEncashmentInfo = JsonConvert.DeserializeObject<List<EmpResignLeaveEncashmentInfo>>(DataTableToJSon);
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

            if (objEmpResignLeaveEncashmentInfo.Count > 0)
                return objEmpResignLeaveEncashmentInfo[0];
            else
                return new EmpResignLeaveEncashmentInfo();
        }

        public int InsertEmpResignLeaveEncashmentInfo(int EmpResignID, DateTime EncashmentDate, decimal TotalLeaveAllotted, decimal TotalLeaveUtilised, decimal TotalLeaveBalance, decimal TotalEncashableLeave, 
            decimal DaysToEncash, decimal EligibleSalary, decimal DailySalaryRate, string SalaryCalculationBasis, decimal EncashmentAmount, decimal TaxDeduction, decimal NetEncashmentAmount, string MoreDetails, bool EncashmentProcessStatus)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("EmpResignLeaveEncashmentInfo", "LeaveEncashmentID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpResignLeaveEncashmentInfo (LeaveEncashmentID, EmpResignID, EncashmentDate, TotalLeaveAllotted, TotalLeaveUtilised, TotalLeaveBalance, TotalEncashableLeave, DaysToEncash, EligibleSalary, DailySalaryRate, SalaryCalculationBasis, EncashmentAmount, TaxDeduction, NetEncashmentAmount, MoreDetails, EncashmentProcessStatus) VALUES " +
                 "(" + maxRowCount.Data + "," + EmpResignID + ", #" + EncashmentDate.Date.ToString("dd-MMM-yyyy") + "#, " + TotalLeaveAllotted + ", " + TotalLeaveUtilised + ", " + TotalLeaveBalance + ", " + TotalEncashableLeave + ", " +
                 DaysToEncash + ", " + EligibleSalary + ", " + DailySalaryRate + ", '" + SalaryCalculationBasis + "', " + EncashmentAmount + ", " + TaxDeduction + ", " + NetEncashmentAmount + ", '" + MoreDetails + "', " + EncashmentProcessStatus + ")";

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

        public int UpdateEmpResignLeaveEncashmentInfo(int LeaveEncashmentID, int EmpResignID, DateTime EncashmentDate, decimal TotalLeaveAllotted, decimal TotalLeaveUtilised, decimal TotalLeaveBalance, decimal TotalEncashableLeave,
            decimal DaysToEncash, decimal EligibleSalary, decimal DailySalaryRate, string SalaryCalculationBasis, decimal EncashmentAmount, decimal TaxDeduction, decimal NetEncashmentAmount, string MoreDetails, bool EncashmentProcessStatus)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpResignLeaveEncashmentInfo SET " +
                                            " EmpResignID = " + EmpResignID + ", " +
                                            " EncashmentDate = #" + EncashmentDate.Date.ToString("dd-MMM-yyyy") + "#, " +
                                            " TotalLeaveAllotted = " + TotalLeaveAllotted + ", " +
                                            " TotalLeaveUtilised = " + TotalLeaveUtilised + ", " +
                                            " TotalLeaveBalance = " + TotalLeaveBalance + ", " +
                                            " TotalEncashableLeave = " + TotalEncashableLeave + ", " +
                                            " DaysToEncash = " + DaysToEncash + ", " +
                                            " EligibleSalary = " + EligibleSalary + ", " +
                                            " DailySalaryRate = " + DailySalaryRate + ", " +
                                            " SalaryCalculationBasis = '" + SalaryCalculationBasis + "', " +
                                            " EncashmentAmount = " + EncashmentAmount + ", " +
                                            " TaxDeduction = " + TaxDeduction + ", " +
                                            " NetEncashmentAmount = " + NetEncashmentAmount + ", " +
                                            " MoreDetails = '" + MoreDetails + "', " +
                                            " EncashmentProcessStatus = " + EncashmentProcessStatus +
                                    " WHERE " +
                                            " LeaveEncashmentID = " + LeaveEncashmentID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = LeaveEncashmentID;
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
