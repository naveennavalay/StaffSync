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
    public class clsUploadDocuments
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        DataSet dtDataset;

        public clsUploadDocuments() { 

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
                int maxRow = (int) cmd.ExecuteScalar();
                if (maxRow == 0)
                    rowCount = 1;
                else if (maxRow > 0)
                    rowCount = maxRow + 1;

            }
            catch (Exception ex)
            {
                if (ex.Message.ToLower() == "Specified cast is not valid.".ToLower())
                {
                    rowCount = 1;
                }
                else
                {
                    MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    conn = objDBClass.closeDBConnection();
                }
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return rowCount;
        }

        public int InsertDocumentUpload(string txtDocName, string txtDocType, DateTime txtDocUploadDate, string txtDocumentPath, bool DocUploadStatus)
        {
            int affectedRows = 0;
            try
            {
                int maxRowCount = getMaxRowCount("DocUploads", "DocID");

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO DocUploads (DocID, DocCode, DocName, DocType, DocUploadDate, DocPath, DocUploadStatus) VALUES " +
                 "(" + maxRowCount + ",'" + "DOC-" + (maxRowCount).ToString().PadLeft(4, '0').Trim() + "','" + txtDocName +
                 "','" + txtDocType + "','" + DateTime.Now.ToString() + "','" + txtDocumentPath + "'," + DocUploadStatus + ")";

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

        public int InsertLinkUpdatedDocuments(int txtEmpID, int txtDocID)
        {
            int affectedRows = 0;
            try
            {
                int maxRowCount = getMaxRowCount("EmpDocMas", "EmpDocumentID");

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpDocMas (EmpDocumentID, EmpID, DocID) VALUES " +
                 "(" + maxRowCount + "," + txtEmpID + "," + txtDocID + ")";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = maxRowCount;

                strQuery = "UPDATE DocUploads SET DocUploadStatus = true WHERE DocID = " + txtDocID;

                cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

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

        public int UpdateLinkUpdatedDocuments(int txtEmpDocumentID, int txtEmpID, int txtDocID)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE EmpDocMas SET EmpID = " + txtEmpID + ", DocID = " + txtDocID +
                 " WHERE EmpDocumentID = " + txtEmpDocumentID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtEmpDocumentID;

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

        public List<EmployeeDocumentInfo> getEmployeeSpecificDocumentsList(int txtEmpID)
        {
            List<EmployeeDocumentInfo> employeeDocumentInfo = new List<EmployeeDocumentInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT * FROM qryDocumentsList WHERE EmpID = " + txtEmpID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                employeeDocumentInfo = JsonConvert.DeserializeObject<List<EmployeeDocumentInfo>>(DataTableToJSon);
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

            return employeeDocumentInfo;
        }

        public EmployeeDocumentInfo getSpecificDocumentInfo(int txtDocumentID)
        {
            EmployeeDocumentInfo employeeDocumentInfo = new EmployeeDocumentInfo();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT * FROM DocUploads WHERE DocID = " + txtDocumentID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                List<EmployeeDocumentInfo> objEmployeeDocumentInfo = JsonConvert.DeserializeObject<List<EmployeeDocumentInfo>>(DataTableToJSon);
                if (objEmployeeDocumentInfo.Count > 0)
                {
                    employeeDocumentInfo.DocID = objEmployeeDocumentInfo[0].DocID;
                    employeeDocumentInfo.DocCode = objEmployeeDocumentInfo[0].DocCode;
                    employeeDocumentInfo.DocName = objEmployeeDocumentInfo[0].DocName;
                    employeeDocumentInfo.DocType = objEmployeeDocumentInfo[0].DocType;
                    employeeDocumentInfo.DocUploadDate = objEmployeeDocumentInfo[0].DocUploadDate;
                    employeeDocumentInfo.DocPath = objEmployeeDocumentInfo[0].DocPath;
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

            return employeeDocumentInfo;
        }


        public EmployeeDocumentInfo isDocumentReferenced(int txtEmpID, int txtDocumentID)
        {
            EmployeeDocumentInfo employeeDocumentInfo = new EmployeeDocumentInfo();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT * FROM EmpDocMas WHERE EmpID = " + txtEmpID + " AND DocID = " + txtDocumentID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                List<EmployeeDocumentInfo> objEmployeeDocumentInfo = JsonConvert.DeserializeObject<List<EmployeeDocumentInfo>>(DataTableToJSon);
                if (objEmployeeDocumentInfo.Count > 0)
                {
                    employeeDocumentInfo.EmpDocumentID = objEmployeeDocumentInfo[0].EmpDocumentID;
                    employeeDocumentInfo.DocID = objEmployeeDocumentInfo[0].DocID;
                    employeeDocumentInfo.DocCode = objEmployeeDocumentInfo[0].DocCode;
                    employeeDocumentInfo.DocName = objEmployeeDocumentInfo[0].DocName;
                    employeeDocumentInfo.DocType = objEmployeeDocumentInfo[0].DocType;
                    employeeDocumentInfo.DocUploadDate = objEmployeeDocumentInfo[0].DocUploadDate;
                    employeeDocumentInfo.DocPath = objEmployeeDocumentInfo[0].DocPath;
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

            return employeeDocumentInfo;
        }
    }

    public class EmployeeDocumentInfo
    {
        public int EmpID { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public int EmpDocumentID { get; set; }
        public int DocID { get; set; }
        public string DocCode { get; set; }
        public string DocName { get; set; }
        public string DocType { get; set; }
        public DateTime DocUploadDate { get; set; }
        public string DocPath { get; set; }
        public bool DocUploadStatus { get; set; }
    }
}
