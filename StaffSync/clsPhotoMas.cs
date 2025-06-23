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
    public class clsPhotoMas
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsStates objState = new clsStates();
        clsCountries objCountry = new clsCountries();

        public clsPhotoMas() { 

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
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return rowCount;
        }

        public PhotoInfo getEmployeePhoto(int txtEmpID)
        {
            PhotoInfo photoInfo = new PhotoInfo();

            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT * FROM Photos WHERE EmpID = " + txtEmpID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                List<PhotoInfo> objPhotoInfo = JsonConvert.DeserializeObject<List<PhotoInfo>>(DataTableToJSon);
                if (objPhotoInfo.Count > 0)
                {
                    photoInfo.PhotoID = objPhotoInfo[0].PhotoID;
                    photoInfo.EmpID = objPhotoInfo[0].EmpID;
                    photoInfo.EmpPhoto = objPhotoInfo[0].EmpPhoto;
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

            return photoInfo;
        }

        public int InsertPhotoInfo(int txtEmpID, byte[] txtPhoto)
        {
            int affectedRows = 0;
            try
            {
                int maxRowCount = getMaxRowCount("Photos", "PhotoID");

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO Photos (PhotoID, EmpID, EmpPhoto) VALUES " +
                 "(" + maxRowCount + "," + txtEmpID + ",'" + txtPhoto + "')";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = maxRowCount;


                cmd = new OleDbCommand("UPDATE Photos SET EmpPhoto=@Image WHERE EmpID = " + txtEmpID + "", conn);

                // Add the image as a parameter.
                OleDbParameter param = new OleDbParameter();
                param.OleDbType = OleDbType.Binary;
                param.ParameterName = "Image";
                param.Value = txtPhoto;
                cmd.Parameters.Add(param);
                cmd.ExecuteScalar();

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

        public int UpdatePhotoInfo(int txtEmpID, byte[] txtPhoto)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE Photos SET EmpPhoto = '" + txtPhoto + "'" +
                " WHERE EmpID = " + txtEmpID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtEmpID;

                cmd = new OleDbCommand("UPDATE Photos SET EmpPhoto=@Image WHERE EmpID = " + txtEmpID + "", conn);

                // Add the image as a parameter.
                OleDbParameter param = new OleDbParameter();
                param.OleDbType = OleDbType.Binary;
                param.ParameterName = "Image";
                param.Value = txtPhoto;
                cmd.Parameters.Add(param);
                cmd.ExecuteScalar();
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

    public class PhotoInfo
    {
        public int PhotoID { get; set; }
        public int EmpID { get; set; }
        public byte[] EmpPhoto { get; set; }

    }
}
