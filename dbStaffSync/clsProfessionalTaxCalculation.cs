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
    }
}
