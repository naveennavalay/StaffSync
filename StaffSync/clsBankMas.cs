using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public class clsBankMas
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        DataSet dtDataset = null;

        public clsBankMas() { 

        }

        public int getMaxRowCount(string tableName, string ColumnName)
        {
            int rowCount = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT MAX(" + ColumnName.ToString().Trim() + ") FROM " + tableName;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                int maxRow = (Int32)cmd.ExecuteScalar();
                if (maxRow == 0)
                    rowCount = 1;
                else if (maxRow > 0)
                    rowCount = maxRow + 1;

            }
            catch (Exception ex)
            {
                if (ex.Message.ToString().ToLower() == "Specified cast is not valid.".ToLower())
                {
                    rowCount = 1;
                }
                else
                {
                    MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return rowCount;
        }

        public EmployeeBankInfo GetEmployeeSpecificBankInfo(int txtEmpID)
        {
            EmployeeBankInfo empBankDetailsInfo = new EmployeeBankInfo();
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return empBankDetailsInfo;
        }

        public List<BankDetailsInfo> GetFullBanksList()
        {
            List<BankDetailsInfo> bankDetailsInfoList = new List<BankDetailsInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return bankDetailsInfoList;
        }


        public int InsertEmployeeBankReference(int txtEmpID, string txtEmpACNumber, int txtBankID, bool IsDefault)
        {
            int affectedRows = 0;

            try
            {
                int maxRowCount = getMaxRowCount("EmpBankInfo", "EmpBankID");

                conn = objDBClass.openDBConnection();

                string strQuery = "INSERT INTO EmpBankInfo (EmpBankID, EmpID, EmpACNumber, BankID, IsDefault) VALUES " +
                "(" + maxRowCount + "," + txtEmpID + ",'" + txtEmpACNumber + "'," + txtBankID + "," + IsDefault + ")";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = maxRowCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }
            return affectedRows;
        }

        public int UpdateEmployeeBankReference(int txtEmpBankID, int txtEmpID, string txtEmpACNumber, int txtBankID, bool IsDefault)
        {
            int affectedRows = 0;

            try
            {
                conn = objDBClass.openDBConnection();

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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }
            return affectedRows;
        }
    }

    public class BankDetailsInfo
    {
        public int BankID { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public string IFSCCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class EmployeeBankInfo
    {
        public int EmpBankID { get; set; }
        public int EmpID { get; set; }
        public int BankID { get; set; }
        public string EmpACNumber { get; set; }
        public bool IsDefault { get; set; }
    }
}
