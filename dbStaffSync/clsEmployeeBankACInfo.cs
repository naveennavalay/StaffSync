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
    public class clsEmployeeBankACInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public clsEmployeeBankACInfo()
        {

        }

        public List<EmployeeSpecificBankInfo> getEmployeeSpecificBankAccountInfo(int empID)
        {
            List<EmployeeSpecificBankInfo> objEmployeeSpecificBankInfo = new List<EmployeeSpecificBankInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = @"SELECT
                                        BankMasInfo.BankID,
                                        BankMasInfo.BankName,
                                        EmpBankInfo.EmpACNumber,
                                        BankMasInfo.BankAddress,
                                        BankMasInfo.IFSCCode,
                                        EmpMas.EmpID
                                    FROM
                                        EmpMas
                                        INNER JOIN (
                                            BankMasInfo
                                            INNER JOIN EmpBankInfo ON BankMasInfo.BankID = EmpBankInfo.BankID
                                        ) ON EmpMas.EmpID = EmpBankInfo.EmpID
                                    WHERE
                                        (((EmpMas.EmpID) = " + empID + "));";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmployeeSpecificBankInfo = JsonConvert.DeserializeObject<List<EmployeeSpecificBankInfo>>(DataTableToJSon);
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

            return objEmployeeSpecificBankInfo;
        }
    }
}
