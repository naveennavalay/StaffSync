//using C1.Framework;
using ModelStaffSync;
using Newtonsoft.Json;
//using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
//using static C1.Util.Win.Win32;

namespace dbStaffSync
{
    public class clsSalaryProfile
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset = null;
        clsGenFunc objGenFunc = new clsGenFunc();

        public SpecificEmployeeSalaryProfileInfo getEmployeeSpecificSalaryProfile(int txtEmpID)
        {
            SpecificEmployeeSalaryProfileInfo specificEmployeeSalaryProfileInfo = new SpecificEmployeeSalaryProfileInfo();
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();
                DataTable dt = new DataTable();

                string strQuery = "SELECT " +
                                    "EmpSalProfileInfo.EmpSalProID, " +
                                    "EmpSalProfileInfo.SalProfileID " +
                                "FROM " +
                                    "EmpSalProfileInfo " +
                                "WHERE " +
                                    "(" +
                                        "((EmpSalProfileInfo.EmpID) = " + txtEmpID + ") " +
                                        "AND((EmpSalProfileInfo.IsActive) = True) " +
                                        "AND((EmpSalProfileInfo.IsDeleted) = False) " +
                                        "AND((EmpSalProfileInfo.IsDefault) = True) " +
                                    ")";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                List<SpecificEmployeeSalaryProfileInfo> objEmployeeSalaryProfileInfo = JsonConvert.DeserializeObject<List<SpecificEmployeeSalaryProfileInfo>>(DataTableToJSon);
                if (objEmployeeSalaryProfileInfo.Count > 0)
                {
                    specificEmployeeSalaryProfileInfo.EmpID = objEmployeeSalaryProfileInfo[0].EmpID;
                    specificEmployeeSalaryProfileInfo.EmpSalProfileID = objEmployeeSalaryProfileInfo[0].EmpSalProfileID;
                    specificEmployeeSalaryProfileInfo.SalProfileID = objEmployeeSalaryProfileInfo[0].SalProfileID;
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

            return specificEmployeeSalaryProfileInfo;
        }

        public List<SalaryProfileTitleList> GetSalProfileTitleList()
        {
            DataTable dt = new DataTable();
            List<SalaryProfileTitleList> objSalaryProfileInfoList = new List<SalaryProfileTitleList>();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT SalProfileID, SalProfileCode, SalProfileTitle, SalProfileDescription, IsActive, IsDeleted, OrderID, IsAutomaticCalculation FROM SalProfileMas WHERE IsActive = true AND IsDeleted = false ORDER BY OrderID Asc";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objSalaryProfileInfoList = JsonConvert.DeserializeObject<List<SalaryProfileTitleList>>(DataTableToJSon);

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

            return objSalaryProfileInfoList;
        }

        public List<SalaryProfileTitleList> GetSalProfileTitleList(string filterText)
        {
            DataTable dt = new DataTable();
            List<SalaryProfileTitleList> objSalaryProfileInfoList = new List<SalaryProfileTitleList>();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT SalProfileID, SalProfileCode, SalProfileTitle, SalProfileDescription, IsActive, IsDeleted, OrderID, IsAutomaticCalculation FROM SalProfileMas WHERE IsActive = true AND IsDeleted = false AND SalProfileTitle LIKE '" + filterText + "%' ORDER BY OrderID Asc";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objSalaryProfileInfoList = JsonConvert.DeserializeObject<List<SalaryProfileTitleList>>(DataTableToJSon);

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

            return objSalaryProfileInfoList;
        }

        public List<SalaryProfileInfo> GetDefaultSalaryProfileInfo(int SalaryProfileID)
        {

            List<SalaryProfileInfo> objSalaryProfileInfo = new List<SalaryProfileInfo>();
            List<SalaryProfileInfo> objReturnSalaryProfileInfoList = new List<SalaryProfileInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " +
                                    "SalProfileDetails.SalProDetID, " +
                                    "SalProfileDetails.SalProfileID, " +
                                    "AllowanceHeaderMas.AllID AS HeaderID, " +
                                    "AllowanceHeaderMas.AllTitle AS HeaderTitle, " +
                                    "AllowanceHeaderMas.CalcFormula, " +
                                    "AllowanceHeaderMas.IsFixed, " +
                                    "SalProfileDetails.SalProAmount " +
                                "FROM " +
                                    "SalProfileDetails " +
                                    "INNER JOIN AllowanceHeaderMas ON SalProfileDetails.AllID = AllowanceHeaderMas.AllID " +
                                "WHERE " +
                                    "(" +
                                        "((SalProfileDetails.SalProfileID) = " + SalaryProfileID + ") " +
                                        "AND ((AllowanceHeaderMas.IsActive) = True) " +
                                        "AND ((AllowanceHeaderMas.IsDeleted) = False) " +
                                    ") " +
                                "ORDER BY " +
                                    "AllowanceHeaderMas.OrderID";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objSalaryProfileInfo = JsonConvert.DeserializeObject<List<SalaryProfileInfo>>(DataTableToJSon);

                if (objSalaryProfileInfo.Count == 0)
                {
                    strQuery = "SELECT " +
                                "0 AS SalProDetID," +
                                "0 AS SalProfileID," +
                                "AllowanceHeaderMas.AllID AS HeaderID," +
                                "AllowanceHeaderMas.AllTitle AS HeaderTitle," +
                                "AllowanceHeaderMas.CalcFormula," +
                                "AllowanceHeaderMas.IsFixed," +
                                "0 AS SalProAmount " +
                            "FROM " +
                                "AllowanceHeaderMas " +
                            " WHERE " +
                                "(" +
                                    " ((AllowanceHeaderMas.IsActive) = True) " +
                                    " AND ((AllowanceHeaderMas.IsDeleted) = False) " +
                                ") " +
                            "ORDER BY " +
                                "OrderID";
                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strQuery;
                    cmd.ExecuteNonQuery();

                    da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);

                    DataTableToJSon = "";
                    DataTableToJSon = JsonConvert.SerializeObject(dt);
                    objSalaryProfileInfo = JsonConvert.DeserializeObject<List<SalaryProfileInfo>>(DataTableToJSon);
                }
                foreach (SalaryProfileInfo indSalaryProfileInfo in objSalaryProfileInfo)
                {
                    objReturnSalaryProfileInfoList.Add(new SalaryProfileInfo
                    {
                        EmpSalDetID = 0,
                        SalProDetID = indSalaryProfileInfo.SalProDetID,
                        SalProfileID = indSalaryProfileInfo.SalProfileID,
                        HeaderID = indSalaryProfileInfo.HeaderID,
                        HeaderTitle = indSalaryProfileInfo.HeaderTitle,
                        HeaderType = "Allowences",
                        CalcFormula = indSalaryProfileInfo.CalcFormula,
                        IsFixed = indSalaryProfileInfo.IsFixed,
                        AllowanceAmount = indSalaryProfileInfo.SalProAmount,
                        DeductionAmount = 0,
                        ReimbursmentAmount = 0
                    });
                }

                strQuery = "SELECT " + 
                                "SalProfileDetails.SalProDetID, " +
                                "SalProfileDetails.SalProfileID, " +
                                "DeductionHeaderMas.DedID AS HeaderID, " +
                                "DeductionHeaderMas.DedTitle AS HeaderTitle, " +
                                "DeductionHeaderMas.CalcFormula, " +
                                "DeductionHeaderMas.IsFixed, " +
                                "SalProfileDetails.SalProAmount " +
                            "FROM " +
                                "SalProfileDetails " +
                                "INNER JOIN DeductionHeaderMas ON SalProfileDetails.DedID = DeductionHeaderMas.DedID " +
                            "WHERE " +
                                "(" +
                                    "((SalProfileDetails.SalProfileID) = " + SalaryProfileID + ") " +
                                    "AND ((DeductionHeaderMas.IsActive) = True) " +
                                    "AND ((DeductionHeaderMas.IsDeleted) = False) " +
                                ") " +
                            "ORDER BY " +
                                "DeductionHeaderMas.OrderID";

                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                dt = new DataTable();
                da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objSalaryProfileInfo = new List<SalaryProfileInfo>();
                objSalaryProfileInfo = JsonConvert.DeserializeObject<List<SalaryProfileInfo>>(DataTableToJSon);

                if (objSalaryProfileInfo.Count == 0)
                {
                    strQuery = "SELECT " +
                                    "0 AS SalProDetID, " +
                                    "0 AS SalProfileID, " +
                                    "DeductionHeaderMas.DedID AS HeaderID, " +
                                    "DeductionHeaderMas.DedTitle AS HeaderTitle, " +
                                    "DeductionHeaderMas.CalcFormula, " +
                                    "DeductionHeaderMas.IsFixed, " +
                                    "0 AS SalProAmount " +
                                "FROM " +
                                    "DeductionHeaderMas " +
                                "WHERE " +
                                    "( " +
                                        "((DeductionHeaderMas.IsActive) = True)  " +
                                        "AND ((DeductionHeaderMas.IsDeleted) = False)  " +
                                    ") " +
                                "ORDER BY " +
                                    "DeductionHeaderMas.OrderID;";

                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strQuery;
                    cmd.ExecuteNonQuery();

                    dt = new DataTable();
                    da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);

                    DataTableToJSon = "";
                    DataTableToJSon = JsonConvert.SerializeObject(dt);
                    objSalaryProfileInfo = new List<SalaryProfileInfo>();
                    objSalaryProfileInfo = JsonConvert.DeserializeObject<List<SalaryProfileInfo>>(DataTableToJSon);
                }

                foreach (SalaryProfileInfo indSalaryProfileInfo in objSalaryProfileInfo)
                {
                    objReturnSalaryProfileInfoList.Add(new SalaryProfileInfo
                    {
                        EmpSalDetID = 0,
                        SalProDetID = indSalaryProfileInfo.SalProDetID,
                        SalProfileID = indSalaryProfileInfo.SalProfileID,
                        HeaderID = indSalaryProfileInfo.HeaderID,
                        HeaderTitle = indSalaryProfileInfo.HeaderTitle,
                        HeaderType = "Deductions",
                        CalcFormula = indSalaryProfileInfo.CalcFormula,
                        IsFixed = indSalaryProfileInfo.IsFixed,
                        AllowanceAmount = 0,
                        DeductionAmount = indSalaryProfileInfo.SalProAmount,
                        ReimbursmentAmount = 0
                    });
                }

                strQuery = "SELECT " + 
                            "SalProfileDetails.SalProDetID, " + 
                            "SalProfileDetails.SalProfileID, " +
                            "ReimbursementHeaderMas.ReimbID AS HeaderID, " +
                            "ReimbursementHeaderMas.ReimbTitle AS HeaderTitle, " +
                            "ReimbursementHeaderMas.CalcFormula, " +
                            "ReimbursementHeaderMas.IsFixed, " +
                            "SalProfileDetails.SalProAmount " +
                        "FROM " +
                            "SalProfileDetails " +
                            "INNER JOIN ReimbursementHeaderMas ON SalProfileDetails.ReimbID = ReimbursementHeaderMas.ReimbID " +
                        "WHERE " +
                            "(" +
                                "((SalProfileDetails.SalProfileID) = " + SalaryProfileID + ") " +
                                "AND ((ReimbursementHeaderMas.IsActive) = True) " +
                                "AND ((ReimbursementHeaderMas.IsDeleted) = False) " +
                            ") " +
                        "ORDER BY " +
                            "ReimbursementHeaderMas.OrderID; ";

                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                dt = new DataTable();
                da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objSalaryProfileInfo = new List<SalaryProfileInfo>();
                objSalaryProfileInfo = JsonConvert.DeserializeObject<List<SalaryProfileInfo>>(DataTableToJSon);

                if (objSalaryProfileInfo.Count == 0)
                {
                    strQuery = "SELECT " +
                                    "0 AS SalProDetID, " +
                                    "0 AS SalProfileID, " +
                                    "ReimbursementHeaderMas.ReimbID AS HeaderID, " +
                                    "ReimbursementHeaderMas.ReimbTitle AS HeaderTitle, " +
                                    "ReimbursementHeaderMas.CalcFormula, " +
                                    "ReimbursementHeaderMas.IsFixed, " +
                                    "0 AS SalProAmount " +
                                "FROM " +
                                    "ReimbursementHeaderMas " +
                                "WHERE " +
                                    "( " +
                                        "((ReimbursementHeaderMas.IsActive) = True) " +
                                        "AND ((ReimbursementHeaderMas.IsDeleted) = False) " +
                                    ") " +
                                "ORDER BY " +
                                    "ReimbursementHeaderMas.OrderID;";

                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strQuery;
                    cmd.ExecuteNonQuery();

                    dt = new DataTable();
                    da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);

                    DataTableToJSon = "";
                    DataTableToJSon = JsonConvert.SerializeObject(dt);
                    objSalaryProfileInfo = new List<SalaryProfileInfo>();
                    objSalaryProfileInfo = JsonConvert.DeserializeObject<List<SalaryProfileInfo>>(DataTableToJSon);
                }
                foreach (SalaryProfileInfo indSalaryProfileInfo in objSalaryProfileInfo)
                {
                    objReturnSalaryProfileInfoList.Add(new SalaryProfileInfo
                    {
                        EmpSalDetID = 0,
                        SalProDetID = indSalaryProfileInfo.SalProDetID,
                        SalProfileID = indSalaryProfileInfo.SalProfileID,
                        HeaderID = indSalaryProfileInfo.HeaderID,
                        HeaderTitle = indSalaryProfileInfo.HeaderTitle,
                        CalcFormula = indSalaryProfileInfo.CalcFormula,
                        IsFixed = indSalaryProfileInfo.IsFixed,
                        HeaderType = "Reimbursement",
                        AllowanceAmount = 0,
                        DeductionAmount = 0,
                        ReimbursmentAmount = indSalaryProfileInfo.SalProAmount
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
            return objReturnSalaryProfileInfoList;
        }

        public List<SalaryProfileInfo> GetEmployeeSpecificSalaryProfileInfo(int txtEmpID)
        {

            List<SalaryProfileInfo> objSalaryProfileInfo = new List<SalaryProfileInfo>();
            List<SalaryProfileInfo> objReturnSalaryProfileInfoList = new List<SalaryProfileInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " + 
                        "EmpSalDetails.EmpSalDetID, " + 
                        "EmpSalDetails.SalProDetID, " +
                        "EmpSalDetails.EmpSalID as SalProfileID, " +
                        "EmpSalDetails.SalHeaderID as HeaderID, " +
                        "EmpSalDetails.SalHeaderTitle as HeaderTitle, " + 
                        "EmpSalDetails.SalHeaderType as HeaderType, " + 
                        "EmpSalDetails.AllowanceAmount, " + 
                        "EmpSalDetails.DeductionAmount, " + 
                        "EmpSalDetails.ReimbursmentAmount, " + 
                        "EmpSalDetails.OrderID " + 
                    "FROM " + 
                        "EmpSalMas " + 
                        "INNER JOIN EmpSalDetails ON EmpSalMas.EmpSalID = EmpSalDetails.EmpSalID " + 
                    "WHERE " + 
                        "(" + 
                            "(" + 
                                "(EmpSalMas.[EmpSalID]) = (" + 
                                    "SELECT " + 
                                        "MAX(EmpSalID) " + 
                                    "FROM " + 
                                        "EmpSalMas " + 
                                    "WHERE " + 
                                        "EmpID = " + txtEmpID + 
                                        " AND [EmpSalMas].[EmpSalMonthYear] = 'Jan - 1900' " + 
                                ") " + 
                            ") " + 
                        ") " + 
                    "ORDER BY " + 
                        "EmpSalDetails.EmpSalDetID, " + 
                        "EmpSalDetails.OrderID;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objSalaryProfileInfo = JsonConvert.DeserializeObject<List<SalaryProfileInfo>>(DataTableToJSon);
                foreach (SalaryProfileInfo indSalaryProfileInfo in objSalaryProfileInfo)
                {
                    objReturnSalaryProfileInfoList.Add(new SalaryProfileInfo
                    {
                        EmpSalDetID = indSalaryProfileInfo.EmpSalDetID,
                        SalProDetID = indSalaryProfileInfo.SalProDetID,
                        SalProfileID = indSalaryProfileInfo.SalProfileID,
                        HeaderID = indSalaryProfileInfo.HeaderID,
                        HeaderTitle = indSalaryProfileInfo.HeaderTitle,
                        HeaderType = indSalaryProfileInfo.HeaderType,
                        AllowanceAmount = indSalaryProfileInfo.AllowanceAmount,
                        DeductionAmount = indSalaryProfileInfo.DeductionAmount,
                        ReimbursmentAmount = indSalaryProfileInfo.ReimbursmentAmount,
                        OrderID = indSalaryProfileInfo.OrderID
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
            return objReturnSalaryProfileInfoList;
        }

        public int InsertSalaryProfileInfo(string txtSalProfileCode, string txtSalProfileTitle, string txtSalProfileDescription, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("SalProfileMas", "SalProfileID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO SalProfileMas (SalProfileID, SalProfileCode, SalProfileTitle, SalProfileDescription, IsActive, IsDeleted, IsDefault, OrderID) VALUES " +
                 "(" + maxRowCount.Data + ",'" + "SPF-" + (maxRowCount.Data).ToString().PadLeft(4, '0').Trim() + "','" + txtSalProfileTitle + "','" + txtSalProfileDescription + "'," + IsActive + "," + IsDeleted + ", false, " + maxRowCount + ")";

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

        public int InsertSalaryProfileDetailInfo(int txtSalProfileID, int txtAllID, int txtDedID, int txtReimbID, decimal txtSalProAmount)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("SalProfileDetails", "SalProDetID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO SalProfileDetails (SalProDetID, SalProfileID, AllID, DedID, ReimbID, SalProAmount) VALUES " +
                 "(" + maxRowCount.Data + "," + txtSalProfileID + "," + txtAllID + "," + txtDedID + "," + txtReimbID + "," + txtSalProAmount + ")";

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

        public int UpdateSalaryProfileInfo(int txtSalaryProfileID, string txtSalaryProfileCode, string txtSalaryProfileTitle, string txtSalaryProfileDescription, bool IsActive, bool IsDeleted)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE SalProfileMas SET SalProfileCode = '" + txtSalaryProfileCode + "', SalProfileTitle = '" + txtSalaryProfileTitle + "', SalProfileDescription = '" + txtSalaryProfileDescription + "', IsActive = " + IsActive +
                 " WHERE SalProfileID = " + txtSalaryProfileID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtSalaryProfileID;
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

        public int UpdateSalaryProfileInfoAutomaticCalculationStatus(int txtSalaryProfileID, bool IsAutomaticCalculationRequired)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE SalProfileMas SET IsAutomaticCalculation = " + IsAutomaticCalculationRequired + 
                 " WHERE SalProfileID = " + txtSalaryProfileID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtSalaryProfileID;
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

        public int UpdateSalaryProfileDetailInfo(int SalProDetID, int txtSalProfileID, int txtAllID, int txtDedID, int txtReimbID, decimal txtSalProAmount)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE SalProfileDetails SET SalProfileID = " + txtSalProfileID + ", AllID = " + txtAllID + ", DedID = " + txtDedID + ", ReimbID = " + txtReimbID + ", SalProAmount = " + txtSalProAmount +
                 " WHERE SalProDetID = " + SalProDetID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = SalProDetID;
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

        public int DeleteSalaryProfileInfo(int txtSalaryProfileID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE * FROM SalProfileMas WHERE SalProfileID = " + txtSalaryProfileID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtSalaryProfileID;
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
