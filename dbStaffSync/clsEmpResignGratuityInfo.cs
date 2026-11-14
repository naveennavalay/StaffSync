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


        public EmpResignGratuityInfo getEmpResignResignGratuityInfotatements(int EmpResignID)
        {
            List<EmpResignGratuityInfo> objEmpResignGratuityInfo = new List<EmpResignGratuityInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = @"SELECT
                                        EmpResignGratuityInfo.EmpResignGratuityID,
                                        EmpResignGratuityInfo.DateOfJoining,
                                        EmpResignGratuityInfo.ResignationDate,
                                        EmpResignGratuityInfo.LastWorkingDate,
                                        EmpResignGratuityInfo.ActualServiceYears,
                                        EmpResignGratuityInfo.ActualServiceMonths,
                                        EmpResignGratuityInfo.ActualServiceDays,
                                        EmpResignGratuityInfo.GratuityServiceYears,
                                        EmpResignGratuityInfo.IsGratuityEligible,
                                        EmpResignGratuityInfo.EligibilityStatus,
                                        EmpResignGratuityInfo.GratuityFormulaTitle,
                                        EmpResignGratuityInfo.LastDrawnBasicSalary,
                                        EmpResignGratuityInfo.EligibleAllowances,
                                        EmpResignGratuityInfo.WageAdjustmentAmount,
                                        EmpResignGratuityInfo.GratuitySalaryBasis,
                                        EmpResignGratuityInfo.GratuityDays,
                                        EmpResignGratuityInfo.WageDivisor,
                                        EmpResignGratuityInfo.GrossGratuityAmount,
                                        EmpResignGratuityInfo.StatutoryGratuityLimit,
                                        EmpResignGratuityInfo.GratuityCeilingAdjustment,
                                        EmpResignGratuityInfo.TaxOrDeductionAmount,
                                        EmpResignGratuityInfo.NetGratuityAmount,
                                        EmpResignGratuityInfo.HRVerifiedBy,
                                        EmpResignGratuityInfo.HRVerificationDate,
                                        EmpResignGratuityInfo.HRVerificationStatus,
                                        EmpResignGratuityInfo.HRRemarks,
                                        EmpResignGratuityInfo.FinanceVerifiedBy,
                                        EmpResignGratuityInfo.FinanceVerificationDate,
                                        EmpResignGratuityInfo.FinanceVerificationStatus,
                                        EmpResignGratuityInfo.FinanceRemarks,
                                        EmpResignGratuityInfo.PaymentStatus,
                                        EmpResignGratuityInfo.PaymentDate,
                                        EmpResignGratuityInfo.PaymentReferenceNo,
                                        EmpResignGratuityInfo.PaymentRemarks,
                                        EmpResignationInfo.EmpResignID
                                    FROM
                                        EmpResignationInfo
                                        INNER JOIN EmpResignGratuityInfo ON EmpResignationInfo.EmpResignID = EmpResignGratuityInfo.EmpResignID
                                    WHERE
                                        (((EmpResignationInfo.EmpResignID) = " + EmpResignID + "));";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmpResignGratuityInfo = JsonConvert.DeserializeObject<List<EmpResignGratuityInfo>>(DataTableToJSon);
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

            if (objEmpResignGratuityInfo.Count > 0)
                return objEmpResignGratuityInfo[0];
            else
                return new EmpResignGratuityInfo();
        }

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
                    "LastDrawnBasicSalary, EligibleAllowances, WageAdjustmentAmount, GratuitySalaryBasis, GratuityDays, WageDivisor, GrossGratuityAmount, StatutoryGratuityLimit, " +
                    "GratuityCeilingAdjustment, TaxOrDeductionAmount, NetGratuityAmount," +
                    "HRVerifiedBy, HRVerificationDate, HRVerificationStatus, HRRemarks, " +
                    "FinanceVerifiedBy, FinanceVerificationDate, FinanceVerificationStatus, FinanceRemarks, " +
                    "PaymentStatus, PaymentDate, PaymentReferenceNo, PaymentRemarks) VALUES " +
                 "(" + maxRowCount.Data + "," + EmpResignID + ", #" + DateOfJoining.Date.ToString("dd-MMM-yyyy") + "#, #" + ResignationDate.Date.ToString() + "#, #" + LastWorkingDate.Date.ToString("dd-MMM-yyyy") + "#, " +
                 ActualServiceYears + ", " + ActualServiceMonths + ", " + ActualServiceDays + ", " + GratuityServiceYears + ", " + IsGratuityEligible + ", '" + EligibilityStatus + "', '" + GratuityFormulaTitle + "', " +
                 LastDrawnBasicSalary + ", " + EligibleAllowances + ", " + WageAdjustmentAmount + ", " + GratuitySalaryBasis + ", " + GratuityDays + ", " + WageDivisor + ", " + GrossGratuityAmount + ", " + StatutoryGratuityLimit + ", " + 
                 GratuityCeilingAdjustment + ", " + TaxOrDeductionAmount + ", " + NetGratuityAmount + ", " +
                 HRVerifiedBy + ", #" + HRVerificationDate.Date.ToString("dd-MMM-yyyy") + "#, " + HRVerificationStatus + ", '" + HRRemarks + "', " +
                 FinanceVerifiedBy + ", #" + FinanceVerificationDate.Date.ToString("dd-MMM-yyyy") + "#, " + FinanceVerificationStatus + ", '" + FinanceRemarks + "', " +
                 PaymentStatus + ", #" + PaymentDate.Date.ToString("dd-MMM-yyyy") + "#, '" + PaymentReferenceNo + "', '" + PaymentRemarks + "')";

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
                    " EmpResignID = " + EmpResignID + ", " +
                    " DateOfJoining = #" + DateOfJoining.Date.ToString("dd-MMM-yyyy") + "#, " +
                    " ResignationDate = #" + ResignationDate.Date.ToString("dd-MMM-yyyy") + "#, " +
                    " LastWorkingDate = #" + LastWorkingDate.Date.ToString("dd-MMM-yyyy") + "#, " +
                    " ActualServiceYears = " + ActualServiceYears + ", " +
                    " ActualServiceMonths = " + ActualServiceMonths + ", " +
                    " ActualServiceDays = " + ActualServiceDays + ", " +
                    " GratuityServiceYears = " + ActualServiceDays + ", " +
                    " IsGratuityEligible = " + IsGratuityEligible + ", " +
                    " EligibilityStatus = '" + EligibilityStatus + "', " +
                    " GratuityFormulaTitle = '" + GratuityFormulaTitle + "', " +
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
