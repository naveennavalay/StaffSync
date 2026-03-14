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
    public class clsEmployeeStateInsurance
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public ESIModel GetEmployeeStateInsuranceMasterInfo(int txtESIMasID)
        {
            ESIModel tmpESIModel = new ESIModel();
            List<ESIModel> objESIModel = new List<ESIModel>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " +
                                " ESIMas.ESIMasID, " + 
                                " ESIMas.ESIMasTitle, " +
                                " ESIMas.MaxESIAmount, " +
                                " ESIMas.DedID, " + 
                                " ESIMas.ClientID, " + 
                                " ESIMas.IsActive, " + 
                                " ESIMas.IsDeleted, " +
                                " ESIDetails.ESIDetID, " + 
                                " ESIDetails.EmpESIPercentageOrAmount, " + 
                                " ESIDetails.EmpESIPercentage, " + 
                                " ESIDetails.EmpESIAmount, " + 
                                " ESIDetails.EmprESIPercentageOrAmount, " + 
                                " ESIDetails.EmprPercentage, " + 
                                " ESIDetails.EmprESIAmount, " + 
                                " ESIDetails.EffectiveDate, " + 
                                " ESIDetails.OrderID, " + 
                                " ESIMas.IsActive, " + 
                                " ESIMas.IsDeleted, " + 
                                " ESIMas.ESIMasID " + 
                            " FROM " + 
                                " ESIMas INNER JOIN ESIDetails ON ESIMas.ESIMasID = ESIDetails.ESIMasID " + 
                            " WHERE " + 
                                " ( " +
                                    " ((ESIMas.IsActive) = True) " + 
                                    " AND ((ESIMas.IsDeleted) = False) " +
                                    " AND ((ESIMas.DedID) = " + txtESIMasID + ") " + 
                                ") " + 
                            " ORDER BY " + 
                                " ESIDetails.ESIDetID DESC;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objESIModel = JsonConvert.DeserializeObject<List<ESIModel>>(DataTableToJSon);
                //if (objESIModel.Count > 0)
                //{
                //    strQuery = "";
                //    cmd = conn.CreateCommand();
                //    cmd.CommandType = CommandType.Text;
                //    cmd.CommandText = strQuery;
                //    cmd.ExecuteNonQuery();

                //    da = new OleDbDataAdapter(cmd);
                //    da.Fill(dt);

                //    DataTableToJSon = "";
                //    DataTableToJSon = JsonConvert.SerializeObject(dt);
                //    objESIModel = JsonConvert.DeserializeObject<List<ESIModel>>(DataTableToJSon);
                //    //if (objESIModel.Count > 0)
                //    //{
                //    //    foreach (ESIModel PT in objESIModel)
                //    //    {
                //    //        if (grossIncome >= PT.GrossFrom && grossIncome <= PT.GrossTo)
                //    //        {
                //    //            tmpESIModel = PT;
                //    //            break;
                //    //        }
                //    //    }

                //    //}
                //}
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

            if(objESIModel.Count > 0)
                tmpESIModel = objESIModel[0];

            return tmpESIModel;
        }
    }
}
