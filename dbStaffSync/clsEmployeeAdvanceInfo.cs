using ModelStaffSync;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace dbStaffSync
{
    public class clsEmployeeAdvanceInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public clsEmployeeAdvanceInfo()
        {

        }

        public List<EmpAdvanceInfoModel> getEmployeeAdvanceInformationList(int empID)
        {
            List<EmpAdvanceInfoModel> objEmpAdvanceInfoList = new List<EmpAdvanceInfoModel>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = @"SELECT
                                        qryempoutstandingadvanceinfo.EmpID,
                                        qryempoutstandingadvanceinfo.AdvanceName,
                                        qryempoutstandingadvanceinfo.IssueDate,
                                        qryempoutstandingadvanceinfo.IssueAmount,
                                        qryempoutstandingadvanceinfo.LastPaymentDate,
                                        qryempoutstandingadvanceinfo.CBalance,
                                        *
                                    FROM
                                        qryempoutstandingadvanceinfo
                                    WHERE
                                        (((qryempoutstandingadvanceinfo.EmpID) = " + empID + "));";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmpAdvanceInfoList = JsonConvert.DeserializeObject<List<EmpAdvanceInfoModel>>(DataTableToJSon);
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

            return objEmpAdvanceInfoList;
        }
    }
}
