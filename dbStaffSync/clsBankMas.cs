using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace dbStaffSync
{
    public class clsBankMas
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public clsBankMas() { 

        }

        public EmployeeBankInfo GetEmployeeSpecificBankInfo(int txtEmpID)
        {
            EmployeeBankInfo empBankDetailsInfo = new EmployeeBankInfo();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM EmpBankInfo WHERE EmpID = " + txtEmpID ;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                List<EmployeeBankInfo> objEmpBankDetailsInfoList = JsonConvert.DeserializeObject<List<EmployeeBankInfo>>(DataTableToJSon);
                if (objEmpBankDetailsInfoList.Count > 0)
                {
                    empBankDetailsInfo.EmpBankID = objEmpBankDetailsInfoList[0].EmpBankID;
                    empBankDetailsInfo.EmpID = objEmpBankDetailsInfoList[0].EmpID;
                    empBankDetailsInfo.EmpACNumber = objEmpBankDetailsInfoList[0].EmpACNumber;
                    empBankDetailsInfo.BankID = objEmpBankDetailsInfoList[0].BankID;
                    empBankDetailsInfo.IsDefault = objEmpBankDetailsInfoList[0].IsDefault;
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

            return empBankDetailsInfo;
        }

        public List<BankDetailsInfo> GetFullBanksList()
        {
            List<BankDetailsInfo> bankDetailsInfoList = new List<BankDetailsInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT * FROM BankMasInfo WHERE IsActive = true AND IsDeleted = false";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                bankDetailsInfoList = JsonConvert.DeserializeObject<List<BankDetailsInfo>>(DataTableToJSon);
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

            return bankDetailsInfoList;
        }


        public int InsertEmployeeBankReference(int txtEmpID, string txtEmpACNumber, int txtBankID, bool IsDefault)
        {
            int affectedRows = 0;

            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("EmpBankInfo", "EmpBankID");

                conn = dbStaffSync.openDBConnection();

                string strQuery = "INSERT INTO EmpBankInfo (EmpBankID, EmpID, EmpACNumber, BankID, IsDefault) VALUES " +
                "(" + maxRowCount.Data + "," + txtEmpID + ",'" + txtEmpACNumber + "'," + txtBankID + "," + IsDefault + ")";

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

        public int UpdateEmployeeBankReference(int txtEmpBankID, int txtEmpID, string txtEmpACNumber, int txtBankID, bool IsDefault)
        {
            int affectedRows = 0;

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "UPDATE EmpBankInfo SET EmpACNumber = '" + txtEmpACNumber + "', BankID = " + txtBankID + ", IsDefault = " + IsDefault +
                " WHERE EmpID = " + txtEmpID;


                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtEmpID;
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
