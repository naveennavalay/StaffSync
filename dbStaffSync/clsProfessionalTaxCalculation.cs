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
using System.Text.RegularExpressions;

namespace dbStaffSync
{
    public class clsProfessionalTaxCalculation
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public decimal? CalculateProfessionalTax(int PTMasID, int StateID, decimal grossIncome, int monthNo, int SexID)
        {
            ProfessionalTax tmpProfessionalTax = new ProfessionalTax();
            List<ProfessionalTax> objProfessionalTax = new List<ProfessionalTax>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " + 
                                        " Max(ProfTaxDetailedSlab.EffectiveDate) AS EffectiveDate " + 
                                    " FROM ProfTaxMas INNER JOIN ProfTaxDetailedSlab ON ProfTaxMas.PTMasID = ProfTaxDetailedSlab.PTMasID " + 
                                    " GROUP BY " + 
                                        " ProfTaxMas.DedID, ProfTaxMas.IsActive, ProfTaxMas.IsDeleted " + 
                                    " HAVING " + 
                                        " ( " +
                                            " ((ProfTaxMas.DedID) = " + PTMasID + " ) " + 
                                            " AND ((ProfTaxMas.IsActive) = True) " + 
                                            " AND ((ProfTaxMas.IsDeleted) = False) " +
                                        ");";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objProfessionalTax = JsonConvert.DeserializeObject<List<ProfessionalTax>>(DataTableToJSon);
                if (objProfessionalTax.Count > 0)
                {
                    strQuery = "SELECT " + 
                                    " ProfTaxDetailedSlab.PTDSlabID, " +
                                    " ProfTaxDetailedSlab.GrossFrom, " +
                                    " ProfTaxDetailedSlab.GrossTo, " +
                                    " ProfTaxDetailedSlab.PTAmount, " +
                                    " ProfTaxDetailedSlab.MonthNumber, " +
                                    " ProfTaxDetailedSlab.EffectiveDate " +
                                " FROM " +
                                    " StateMas " +
                                    " INNER JOIN ( " +
                                        " SexMas " +
                                        " INNER JOIN ( " +
                                            " ProfTaxMas " +
                                            " INNER JOIN ProfTaxDetailedSlab ON ProfTaxMas.PTMasID = ProfTaxDetailedSlab.PTMasID " +
                                        " ) ON SexMas.SexID = ProfTaxDetailedSlab.SexID " +
                                    " ) ON StateMas.StateID = ProfTaxMas.StateID " +
                                " WHERE " +
                                    " ( " +
                                        " ((ProfTaxDetailedSlab.PTDSlabID) IS NOT NULL) " +
                                        " AND ((ProfTaxDetailedSlab.MonthNumber) = " + monthNo + ") " +
                                        " AND ((ProfTaxDetailedSlab.EffectiveDate) = #" + objProfessionalTax[0].EffectiveDate.ToString("dd-MMM-yyyy") + "#) " +
                                        " AND ((StateMas.StateID) = " + StateID + ") " +
                                        " AND ((ProfTaxMas.DedID) = " + PTMasID + ") " +
                                        " AND ((ProfTaxDetailedSlab.SexID) = " + SexID + ") " +
                                    " ) " +
                                " ORDER BY " +
                                    " ProfTaxDetailedSlab.PTDSlabID Desc;";
                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strQuery;
                    cmd.ExecuteNonQuery();

                    da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);

                    DataTableToJSon = "";
                    DataTableToJSon = JsonConvert.SerializeObject(dt);
                    objProfessionalTax = JsonConvert.DeserializeObject<List<ProfessionalTax>>(DataTableToJSon);
                    if (objProfessionalTax.Count > 0)
                    {
                        foreach (ProfessionalTax PT in objProfessionalTax)
                        {
                            if (grossIncome >= PT.GrossFrom && grossIncome <= PT.GrossTo)
                            {
                                tmpProfessionalTax = PT;
                                break;
                            }
                        }
                    }
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

            return tmpProfessionalTax.PTAmount;
        }

        public int getProfessionalTaxSlab(int txtClientID, int txtBranchID, int txtStateID)
        {
            int intBranchWiseEmployeeCount = 0;
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT DISTINCT " + 
                                            " Count(ProfTaxDetailedSlab.GrossFrom) AS CountOfSlabs " +
                                        " FROM " +
                                            " ( " +
                                                " (  " +
                                                    " ( " +
                                                        " DeductionHeaderMas " +
                                                        " INNER JOIN ( " +
                                                            " ClientMas " +
                                                            " INNER JOIN ProfTaxMas ON ClientMas.ClientID = ProfTaxMas.ClientID " +
                                                        " ) ON DeductionHeaderMas.DedID = ProfTaxMas.DedID " +
                                                    " ) " +
                                                    " INNER JOIN ProfTaxDetailedSlab ON ProfTaxMas.PTMasID = ProfTaxDetailedSlab.PTMasID " +
                                                " ) " +
                                                " INNER JOIN ClientBranchMas ON ClientMas.ClientID = ClientBranchMas.ClientID " +
                                            " ) " +
                                            " INNER JOIN StateMas ON ClientBranchMas.ClientBranchState = StateMas.StateTitle " +
                                        " GROUP BY " +
                                            " DeductionHeaderMas.DedID, " +
                                            " ClientMas.ClientID, " +
                                            " ClientBranchMas.ClientBranchID, " +
                                            " StateMas.StateID, " +
                                            " ProfTaxDetailedSlab.MonthNumber " +
                                        " HAVING " +
                                            " ( " +
                                                " ((DeductionHeaderMas.DedID) = 2) " +
                                                " AND ((ClientMas.ClientID) = " + txtClientID + ") " +
                                                " AND ((ClientBranchMas.ClientBranchID) = " + txtBranchID + ") " +
                                                " AND ((StateMas.StateID) = " + txtStateID + ") " +
                                            " ); ";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    intBranchWiseEmployeeCount = Convert.ToInt32(dt.Rows[0]["CountOfSlabs"]);
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

            return intBranchWiseEmployeeCount;
        }

        public DataTable getProfessionalTaxSlabList(int txtClientID, int txtBranchID, int txtStateID)
        {
            int intBranchWiseEmployeeCount = 0;
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " + 
                                        " ProfTaxDetailedSlab.GrossFrom, " +
                                        " ProfTaxDetailedSlab.GrossTo, " +
                                        " ProfTaxDetailedSlab.PTAmount " +
                                    " FROM " +
                                        " StateMas " +
                                        " INNER JOIN ( " +
                                            " ( " +
                                                " ( " +
                                                    " ClientMas " +
                                                    " INNER JOIN ClientBranchMas ON ClientMas.ClientID = ClientBranchMas.ClientID " +
                                                " ) " +
                                                " INNER JOIN ProfTaxMas ON ClientMas.ClientID = ProfTaxMas.ClientID " +
                                            " ) " +
                                            " INNER JOIN ProfTaxDetailedSlab ON ProfTaxMas.PTMasID = ProfTaxDetailedSlab.PTMasID " +
                                        " ) ON StateMas.StateID = ProfTaxMas.StateID " +
                                    " GROUP BY " +
                                        " ProfTaxDetailedSlab.GrossFrom, " +
                                        " ProfTaxDetailedSlab.GrossTo, " +
                                        " ProfTaxDetailedSlab.PTAmount, " +
                                        " ClientBranchMas.ClientBranchID, " +
                                        " ClientMas.ClientID, " +
                                        " StateMas.StateID " +
                                    " HAVING " +
                                        " (" +
                                            " ((ClientBranchMas.ClientBranchID) = " + txtBranchID + ") " +
                                            " AND ((ClientMas.ClientID) = " + txtClientID + ") " +
                                            " AND ((StateMas.StateID) = " + txtStateID + ") " +
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

        public DataTable getStateSpecificProfessionalTaxSlabList(int txtClientID, int txtStateID)
        {
            int intBranchWiseEmployeeCount = 0;
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " + 
                                        " ProfTaxDetailedSlab.GrossFrom, " +
                                        " ProfTaxDetailedSlab.GrossTo, " +
                                        " ProfTaxDetailedSlab.PTAmount " +
                                    " FROM " + 
                                        " StateMas INNER JOIN ((ClientMas INNER JOIN ProfTaxMas ON ClientMas.ClientID = ProfTaxMas.ClientID) INNER JOIN ProfTaxDetailedSlab ON ProfTaxMas.PTMasID = ProfTaxDetailedSlab.PTMasID) " + 
                                        " ON StateMas.StateID = ProfTaxMas.StateID " + 
                                    " GROUP BY " + 
                                        " ProfTaxDetailedSlab.GrossFrom, " + 
                                        " ProfTaxDetailedSlab.GrossTo, " + 
                                        " ProfTaxDetailedSlab.PTAmount, " + 
                                        " ClientMas.ClientID, " +
                                        " StateMas.StateID " +
                                    " HAVING " +
                                        " ( " +
                                            " ((ClientMas.ClientID) = " + txtClientID + ") " +
                                            " AND ((StateMas.StateID) = " + txtStateID + ") " +
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
