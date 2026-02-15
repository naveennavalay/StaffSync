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
    public class clsProvidentFundCalculation
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public ProvidentFund CalculatePF(int PFMasID)
        {
            ProvidentFund tmpProvidentFund = new ProvidentFund();
            List<ProvidentFund> objProvidentFund = new List<ProvidentFund>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " + 
                                        " ProvFundDetails.PFDetID, " + 
                                        " ProvFundDetails.EmpPFPercentageOrAmount, " + 
                                        " ProvFundDetails.EmpPFPercentage, " + 
                                        " ProvFundDetails.EmpPFAmount, " + 
                                        " ProvFundDetails.EmprPFPercentageOrAmount, " + 
                                        " ProvFundDetails.EmprPFPercentage, " + 
                                        " ProvFundDetails.EmprPFAmount, " + 
                                        " ProvFundDetails.EmprPSPercentageOrAmount, " + 
                                        " ProvFundDetails.EmprPSPercentage, " + 
                                        " ProvFundDetails.EmprPSAmount, " + 
                                        " ProvFundDetails.EffectiveDate, " + 
                                        " ProvFundDetails.OrderID " + 
                                    " FROM " + 
                                        " ProvFundMas INNER JOIN ProvFundDetails ON ProvFundMas.PFMasID = ProvFundDetails.PFMasID " + 
                                    " WHERE " +
                                        " ( " + 
                                            " ((ProvFundMas.IsActive) = True) " +
                                            " AND ((ProvFundMas.IsDeleted) = False) " + 
                                            " AND ((ProvFundMas.DedID) = " + PFMasID + ") " + 
                                        " ) " +
                                    " ORDER BY " +
                                        " ProvFundDetails.PFDetID DESC;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objProvidentFund = JsonConvert.DeserializeObject<List<ProvidentFund>>(DataTableToJSon);
                if (objProvidentFund.Count > 0)
                {
                    strQuery = "";
                    cmd = conn.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = strQuery;
                    cmd.ExecuteNonQuery();

                    da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);

                    DataTableToJSon = "";
                    DataTableToJSon = JsonConvert.SerializeObject(dt);
                    objProvidentFund = JsonConvert.DeserializeObject<List<ProvidentFund>>(DataTableToJSon);
                    //if (objProvidentFund.Count > 0)
                    //{
                    //    foreach (ProvidentFund PT in objProvidentFund)
                    //    {
                    //        if (grossIncome >= PT.GrossFrom && grossIncome <= PT.GrossTo)
                    //        {
                    //            tmpProvidentFund = PT;
                    //            break;
                    //        }
                    //    }

                    //}
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

            return tmpProvidentFund;
        }
    }
}
