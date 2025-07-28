//using C1.Framework;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.OleDb;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public class clsWeeklyOffInfo
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        DataSet dtDataset = null;

        public clsWeeklyOffInfo() { 

        }

        public int getMaxRowCount(string tableName, string ColumnName)
        {
            int rowCount = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT Max(" + ColumnName.ToString().Trim() + ") FROM " + tableName;

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

        public DataTable getWklyOffProfileMasInfoList(string txtWklyOffTitle)
        {
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "";
                if(string.IsNullOrEmpty(txtWklyOffTitle))
                    strQuery = "SELECT " + 
                                        "WklyOffMasID, WklyOffCode, WklyOffTitle, WklyOffEffectiveDate " +
                                  " FROM " + 
                                        " WklyOffProfileInfo " + 
                                  " WHERE " + 
                                        "(((WklyOffProfileInfo.IsActive) = True) AND ((WklyOffProfileInfo.IsDelete) = False));";
                else
                    strQuery ="SELECT " + 
                                        "WklyOffMasID, WklyOffCode, WklyOffTitle, WklyOffEffectiveDate " +
                                  " FROM " + 
                                        " WklyOffProfileInfo " + 
                                  " WHERE " + 
                                        "WklyOffTitle LIKE '" + txtWklyOffTitle + "%' AND IsActive = True AND IsDelete = False;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

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

            return dt;
        }

        public int InsertWeeklyOffInfo(string txtWklyOffCode, string txtWklyOffTitle, DateTime txtWklyOffEffectiveDate, bool IsActive, bool IsDelete)
        {
            int affectedRows = 0;
            try
            {
                int maxRowCount = getMaxRowCount("WklyOffProfileInfo", "WklyOffMasID");
                
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO WklyOffProfileInfo (WklyOffMasID, WklyOffCode, WklyOffTitle, WklyOffEffectiveDate, IsActive, IsDelete) VALUES " +
                 "(" + maxRowCount + ",'" + "WOF-" + (maxRowCount).ToString().PadLeft(4, '0').Trim() + "','" + txtWklyOffTitle + "','" + txtWklyOffEffectiveDate.ToString("dd-MMM-yyyy") + "'," + IsActive + "," + IsDelete + ")";

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

        public int UpdateWeeklyOffInfo(int txtWeeklyOffMasID, string txtWklyOffCode, string txtWklyOffTitle, DateTime txtWklyOffEffectiveDate, bool IsActive, bool IsDelete)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE WklyOffProfileInfo SET " + 
                                " WklyOffCode = '" + txtWklyOffCode + "', WklyOffTitle = '" +  txtWklyOffTitle + "', WklyOffEffectiveDate = '" + txtWklyOffEffectiveDate.ToString("dd-MMM-yyyy") + "', IsActive = " + IsActive + ", IsDelete = " + IsDelete + 
                                " WHERE " + 
                                " WklyOffMasID = " + txtWeeklyOffMasID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = affectedRows;
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

        public int DeleteWeeklyOffInfo(int txtWeeklyOffMasID)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                //LeaveDuration = 0,
                string strQuery = "DELETE FROM WklyOffProfileInfo WHERE WklyOffMasID = " + txtWeeklyOffMasID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = affectedRows;
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

        public List<WklyOffProfileDetailsInfo> getWeeklyOffDetailsInfo(int txtWklyOffMasID)
        {
            DataTable dt = new DataTable();
            List<WklyOffProfileDetailsInfo> objWklyOffProfileDetailsInfoList = new List<WklyOffProfileDetailsInfo>();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT " + 
                                    " WklyOffDetID, WklyOffDay, WklyOffOrderID " + 
                                  " FROM " + 
                                    " WklyOffProfileDetails " + 
                                  " WHERE " + 
                                    " (((WklyOffMasID) = " + txtWklyOffMasID + ")) " + 
                                  " ORDER BY " + 
                                    " WklyOffDetID, WklyOffOrderID ASC";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objWklyOffProfileDetailsInfoList = JsonConvert.DeserializeObject<List<WklyOffProfileDetailsInfo>>(DataTableToJSon);
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

            return objWklyOffProfileDetailsInfoList;
        }

        public int InsertWeeklyOffDetailInfo(int txtWklyOffMasID, int txtWklyOffDay, int txtWklyOffOrderID)
        {
            int affectedRows = 0;
            try
            {
                int maxRowCount = getMaxRowCount("WklyOffProfileDetails", "WklyOffDetID");

                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO WklyOffProfileDetails (WklyOffDetID, WklyOffMasID, WklyOffDay, WklyOffOrderID) VALUES " +
                 "(" + maxRowCount + "," + txtWklyOffMasID + "," + txtWklyOffDay + "," + maxRowCount + ")";

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

        public int UpdateWeeklyOffDetailInfo(int txtWklyOffDetID, int txtWklyOffMasID, int txtWklyOffDay, int txtWklyOffOrderID)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE WklyOffProfileDetails SET " +
                    "WklyOffMasID = " + txtWklyOffMasID + ", WklyOffDay = " + txtWklyOffDay + ", WklyOffOrderID = " + txtWklyOffOrderID + " WHERE WklyOffDetID = " + txtWklyOffDetID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = affectedRows;
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

        public int DeleteWeeklyOffDetailInfo(int txtWklyOffDetID)
        {
            int affectedRows = 0;
            try
            {
                conn = objDBClass.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE FROM WklyOffProfileDetails WHERE WklyOffDetID = " + txtWklyOffDetID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = affectedRows;
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

    public class WklyOffProfileMasInfo
    {
        public int WklyOffMasID { get; set; }

        [DisplayName("Weekly Off Code")]
        public string WklyOffCode { get; set; }

        [DisplayName("Weekly Off Title")] 
        public string WklyOffTitle { get; set; }

        [DisplayName("Effective From")]
        public DateTime WklyOffEffectiveDate { get; set; }

        public bool IsActive { get; set; }

        public bool IsDelete { get; set; }        
    }

    public class WklyOffProfileDetailsInfo
    {
        [DisplayName("Select")]
        public bool Select { get; set; }
        
        [DisplayName("WeeklyOffDetailsID")]
        public int WklyOffDetID { get; set; }

        [DisplayName("WeeklyOffMasterID")] 
        public int WklyOffMasID { get; set; }

        [DisplayName("WeeklyOffDayID")] 
        public int WklyOffDay { get; set; }

        [DisplayName("WeeklyOffDayName")]
        public int WklyOffDayName { get; set; }

        [DisplayName("WeeklyOffDayOrderID")]
        public int WklyOffOrderID { get; set; }
    }
}
