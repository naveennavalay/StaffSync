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
    public class clsEmpResignGratuityInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();


        //public EmpResignResignGratuityInfo getEmpResignResignGratuityInfotatements(int EmpResignID)
        //{
        //    List<EmpResignResignGratuityInfo> objEmpResignResignGratuityInfo = new List<EmpResignResignGratuityInfo>();
        //    DataTable dt = new DataTable();

        //    try
        //    {
        //        conn = dbStaffSync.openDBConnection();

        //        string strQuery = @"SELECT
        //                                EmpResignResignGratuityInfo.LeaveEncashmentID,
        //                                EmpResignResignGratuityInfo.EmpResignID,
        //                                EmpResignResignGratuityInfo.EncashmentDate,
        //                                EmpResignResignGratuityInfo.TotalLeaveAllotted,
        //                                EmpResignResignGratuityInfo.TotalLeaveUtilised,
        //                                EmpResignResignGratuityInfo.TotalLeaveBalance,
        //                                EmpResignResignGratuityInfo.TotalEncashableLeave,
        //                                EmpResignResignGratuityInfo.DaysToEncash,
        //                                EmpResignResignGratuityInfo.EligibleSalary,
        //                                EmpResignResignGratuityInfo.DailySalaryRate,
        //                                EmpResignResignGratuityInfo.SalaryCalculationBasis,
        //                                EmpResignResignGratuityInfo.EncashmentAmount,
        //                                EmpResignResignGratuityInfo.TaxDeduction,
        //                                EmpResignResignGratuityInfo.NetEncashmentAmount,
        //                                EmpResignResignGratuityInfo.MoreDetails,
        //                                EmpResignResignGratuityInfo.EncashmentProcessStatus
        //                            FROM
        //                                EmpResignResignGratuityInfo
        //                            WHERE
        //                                (((EmpResignResignGratuityInfo.EmpResignID) = " + EmpResignID + ")); ";

        //        OleDbCommand cmd = conn.CreateCommand();
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = strQuery;
        //        cmd.ExecuteNonQuery();

        //        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        //        da.Fill(dt);

        //        string DataTableToJSon = "";
        //        DataTableToJSon = JsonConvert.SerializeObject(dt);
        //        objEmpResignResignGratuityInfo = JsonConvert.DeserializeObject<List<EmpResignResignGratuityInfo>>(DataTableToJSon);
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        conn = dbStaffSync.closeDBConnection();
        //    }
        //    finally
        //    {
        //        conn = dbStaffSync.closeDBConnection();
        //    }

        //    if (objEmpResignResignGratuityInfo.Count > 0)
        //        return objEmpResignResignGratuityInfo[0];
        //    else
        //        return new EmpResignResignGratuityInfo();
        //}

        public int InsertEmpResignResignGratuityInfo(int EmpResignID, DateTime DateOfJoining, DateTime ResignationDate, DateTime LastWorkingDate, int ActualServiceYears, int ActualServiceMonths, int ActualServiceDays,
            decimal GratuityServiceYears, bool IsGratuityEligible, string EligibilityStatus, string GratuityFormulaTitle, 
            decimal LastDrawnBasicSalary, decimal EligibleAllowances, decimal WageAdjustmentAmount, decimal GratuitySalaryBasis, int GratuityDays, int WageDivisor, 
            decimal GrossGratuityAmount, decimal StatutoryGratuityLimit, decimal GratuityCeilingAdjustment, decimal TaxOrDeductionAmount, decimal NetGratuityAmount, 
            int HRVerifiedBy, DateTime HRVerificationDate, int HRVerificationStatus, string HRRemarks, 
            int FinanceVerifiedBy, DateTime FinanceVerificationDate, int FinanceVerificationStatus, string FinanceRemarks, 
            int PaymentStatus, DateTime PaymentDate, string PaymentReferenceNo, string PaymentRemarks)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("EmpResignGratuityInfo", "EmpResignGratuityID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpResignGratuityInfo (EmpResignGratuityID, EmpResignID, DateOfJoining, ResignationDate, LastWorkingDate, " +
                    "ActualServiceYears, ActualServiceMonths, ActualServiceDays, GratuityServiceYears, IsGratuityEligible, EligibilityStatus, GratuityFormulaTitle, " +
                    "LastDrawnBasicSalary, EligibleAllowances, WageAdjustmentAmount, GratuitySalaryBasis, GratuityDays, WageDivisor, GrossGratuityAmount, StatutoryGratuityLimit, GratuityCeilingAdjustment, TaxOrDeductionAmount, NetGratuityAmount," +
                    "HRVerifiedBy, HRVerificationDate, HRVerificationStatus, HRRemarks, " +
                    "FinanceVerifiedBy, FinanceVerificationDate, FinanceVerificationStatus, FinanceRemarks, " +
                    "PaymentStatus, PaymentDate, PaymentReferenceNo, PaymentRemarks) VALUES " +
                 "(" + maxRowCount.Data + "," + EmpResignID + ", #" + DateOfJoining.Date.ToString("dd-MMM-yyyy") + "#, #" + ResignationDate.Date.ToString() + "#, #" + LastWorkingDate.Date.ToString("dd-MMM-yyyy") + "#, " +
                 ActualServiceYears + ", " + ActualServiceMonths + ", " + ActualServiceDays + ", " + GratuityServiceYears + ", " + IsGratuityEligible + ", " + EligibilityStatus + ", '" + GratuityFormulaTitle + "', " +
                 LastDrawnBasicSalary + ", " + EligibleAllowances + ", " + WageAdjustmentAmount + ", " + GratuitySalaryBasis + ", " + GratuityDays + ", " + WageDivisor + ", " + GrossGratuityAmount + ", " + StatutoryGratuityLimit + ", " + GratuityCeilingAdjustment + ", " + TaxOrDeductionAmount + ", " + NetGratuityAmount +
                 HRVerifiedBy + ", " + HRVerificationDate + ", " + HRVerificationStatus + ", '" + HRRemarks + "', " +
                 FinanceVerifiedBy + ", " + FinanceVerificationDate + ", " + FinanceVerificationStatus + ", " + FinanceRemarks + ", " +
                 PaymentStatus + ", " + PaymentDate + ", " + PaymentReferenceNo + ", '" + PaymentRemarks + "')";

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

        public int UpdateEmpResignResignGratuityInfo(int EmpResignGratuityID, int EmpResignID, DateTime DateOfJoining, DateTime ResignationDate, DateTime LastWorkingDate, int ActualServiceYears, int ActualServiceMonths, int ActualServiceDays, 
            decimal GratuityServiceYears, bool IsGratuityEligible, string EligibilityStatus, string GratuityFormulaTitle,
            decimal LastDrawnBasicSalary, decimal EligibleAllowances, decimal WageAdjustmentAmount, decimal GratuitySalaryBasis, int GratuityDays, int WageDivisor,
            decimal GrossGratuityAmount, decimal StatutoryGratuityLimit, decimal GratuityCeilingAdjustment, decimal TaxOrDeductionAmount, decimal NetGratuityAmount,
            int HRVerifiedBy, DateTime HRVerificationDate, int HRVerificationStatus, string HRRemarks,
            int FinanceVerifiedBy, DateTime FinanceVerificationDate, int FinanceVerificationStatus, string FinanceRemarks,
            int PaymentStatus, DateTime PaymentDate, string PaymentReferenceNo, string PaymentRemarks)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpResignGratuityInfo SET " +
                    " EmpResignID = " + EmpResignID + 
                    " DateOfJoining = #" + DateOfJoining.Date.ToString("dd-MMM-yyyy") + "#, " +
                    " ResignationDate = #" + ResignationDate.Date.ToString("dd-MMM-yyyy") + "#, " +
                    " LastWorkingDate = #" + LastWorkingDate.Date.ToString("dd-MMM-yyyy") + "#, " +
                    " ActualServiceYears = " + ActualServiceYears + ", " +
                    " ActualServiceMonths = " + ActualServiceMonths + ", " +
                    " ActualServiceDays = " + ActualServiceDays + ", " +
                    " GratuityServiceYears = " + ActualServiceDays + ", " +
                    " IsGratuityEligible = " + IsGratuityEligible + ", " +
                    " EligibilityStatus = " + EligibilityStatus + ", " +
                    " GratuityFormulaTitle = '" + EligibilityStatus + "', " +
                    " LastDrawnBasicSalary = " + LastDrawnBasicSalary + ", " +
                    " EligibleAllowances = " + EligibleAllowances + ", " +
                    " WageAdjustmentAmount = " + WageAdjustmentAmount + ", " +
                    " GratuitySalaryBasis = " + GratuitySalaryBasis + ", " +
                    " GratuityDays = " + GratuityDays + ", " +
                    " WageDivisor = " + WageDivisor + ", " +
                    " GrossGratuityAmount = " + GrossGratuityAmount + ", " +
                    " StatutoryGratuityLimit = " + StatutoryGratuityLimit + ", " +
                    " GratuityCeilingAdjustment = " + GratuityCeilingAdjustment + ", " +
                    " TaxOrDeductionAmount = " + TaxOrDeductionAmount + ", " +
                    " NetGratuityAmount = " + NetGratuityAmount + ", " +
                    " HRVerifiedBy = " + HRVerifiedBy + ", " +
                    " HRVerificationDate = #" + HRVerificationDate.Date.ToString("dd-MMM-yyyy") + "#, " +
                    " HRVerificationStatus = " + HRVerificationStatus + ", " +
                    " HRRemarks = '" + HRRemarks + "', " +
                    " FinanceVerifiedBy = " + FinanceVerifiedBy + ", " +
                    " FinanceVerificationDate = #" + FinanceVerificationDate.Date.ToString("dd-MMM-yyyy") + "#, " +
                    " FinanceVerificationStatus = " + FinanceVerificationStatus + ", " +
                    " FinanceRemarks = '" + FinanceRemarks + "', " +
                    " PaymentStatus = " + PaymentStatus + ", " +
                    " PaymentDate = #" + PaymentDate.Date.ToString("dd-MMM-yyyy") + "#, " +
                    " PaymentReferenceNo = '" + PaymentReferenceNo + "', " +
                    " PaymentRemarks = '" + PaymentRemarks + "'" +
                                    " WHERE " +
                                            " EmpResignGratuityID = " + EmpResignGratuityID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = EmpResignGratuityID;
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
