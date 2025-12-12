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
    public class clsPublicHolidayInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public clsPublicHolidayInfo() { 

        }

        public List<PublicHolidayInfo> GetHolidayDetailsInfo(int txtYearID)
        {
            List<PublicHolidayInfo> lstPublicHolidayInfo = new List<PublicHolidayInfo>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " + 
                                        "PubHolidayDetails.PubHolDetID, " + 
                                        "PubHolidayDetails.PubHolMasID, " + 
                                        "PubHolidayDetails.PubHolidayTitle, " + 
                                        "PubHolidayDetails.PubHolDate, " + 
                                        "PubHolidayDetails.OrderID, " + 
                                        "WeekdayName (Weekday ([PubHolDate], 0)) AS DayName " + 
                                    "FROM " + 
                                        "PubHolidayDetails " + 
                                    "WHERE " + 
                                        "(((PubHolidayDetails.PubHolMasID) = " + txtYearID + "));";

                strQuery = "SELECT " +
                                "PubHolidayDetails.PubHolDetID, " +
                                "PubHolidayDetails.PubHolMasID, " +
                                "PubHolidayDetails.PubHolidayTitle, " +
                                "PubHolidayDetails.PubHolDate, " +
                                "PubHolidayDetails.OrderID, " +
                                "WeekdayName (Weekday ([PubHolDate], 0)) AS DayName, " +
                                "ClientMas.ClientID " +
                            "FROM " +
                                "( " +
                                    "ClientMas " +
                                    "INNER JOIN PublicHolidayMas ON ClientMas.ClientID = PublicHolidayMas.ClientID " +
                                ") " +
                                "INNER JOIN PubHolidayDetails ON PublicHolidayMas.PubHolMasID = PubHolidayDetails.PubHolMasID " +
                            "WHERE " +
                                "(" +
                                    "((PubHolidayDetails.PubHolMasID) = " + txtYearID + ") " +
                                    "AND ((ClientMas.ClientID) = " + +CurrentUser.ClientID + ") " +
                                ");";


                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                List<PublicHolidayInfo> objPublicHolidayInfo = JsonConvert.DeserializeObject<List<PublicHolidayInfo>>(DataTableToJSon);
                if (objPublicHolidayInfo.Count > 0)
                {
                    lstPublicHolidayInfo = objPublicHolidayInfo;
                    int minRecords = Math.Abs(lstPublicHolidayInfo.Count - 26);
                    for (int iDefaultRecord = 0; iDefaultRecord < minRecords; iDefaultRecord++)
                    {
                        lstPublicHolidayInfo.Add(new PublicHolidayInfo
                        {
                            PubHolDetID = 0,
                            PubHolMasID = txtYearID,
                            PubHolidayTitle = "",
                            PubHolDate = null,
                            OrderID = 0
                        });
                    }
                }
                else
                {
                    for (int iDefaultRecord = 0; iDefaultRecord < 26; iDefaultRecord++)
                    {
                        lstPublicHolidayInfo.Add(new PublicHolidayInfo
                        {
                            PubHolDetID = 0,
                            PubHolMasID = txtYearID,
                            PubHolidayTitle = "",
                            PubHolDate = null,
                            OrderID = 0
                        });
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

            return lstPublicHolidayInfo;
        }

        public int InsertPublicHolidayMasterInfo(string txtPublicHolidayYear, bool IsDefault, int txtOrderID, int txtFinYearID)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("PublicHolidayMas", "PubHolMasID");

                Response<int> OrderID = objGenFunc.getMaxRowCount("PublicHolidayMas", "OrderID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE PublicHolidayMas SET IsDefault = false";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = maxRowCount.Data;


                strQuery = "INSERT INTO PublicHolidayMas (PubHolMasID, PubHolYear, IsDefault, OrderID, FinYearID, ClientID) VALUES " +
                 "(" + maxRowCount.Data + ",'" + txtPublicHolidayYear + "'," + IsDefault + "," + OrderID.Data + "," + txtFinYearID + "," + CurrentUser.ClientID + ")";

                cmd = conn.CreateCommand();
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

        public int InsertPublicHolidayDetailInfo(int txtPubHolMasID, string txtPublicHolidayTitle, DateTime txtPublicHolidayDate, int txtOrderID)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("PubHolidayDetails", "PubHolDetID");

                Response<int> OrderID = objGenFunc.getMaxRowCount("PubHolidayDetails", "OrderID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO PubHolidayDetails (PubHolDetID, PubHolMasID, PubHolidayTitle, PubHolDate, OrderID) VALUES " +
                 "(" + maxRowCount.Data + "," + txtPubHolMasID + ",'" + txtPublicHolidayTitle + "','" + txtPublicHolidayDate.ToString("dd-MM-yyyy") + "'," + OrderID.Data + ")";

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

        public int UpdatePublicHolidayDetailInfo(int txtPubHolDetID, int txtPubHolMasID, string txtPublicHolidayTitle, DateTime txtPublicHolidayDate, int txtOrderID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE PubHolidayDetails SET PubHolidayTitle = '" + txtPublicHolidayTitle + "', PubHolDate = '" + txtPublicHolidayDate.ToString("dd-MM-yyyy") + "'" +
                 "WHERE PubHolDetID = " + txtPubHolDetID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtPubHolDetID;
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

        public int DeletePublicHolidayDetailInfo(int txtPubHolDetID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE * FROM PubHolidayDetails " +
                 "WHERE PubHolDetID = " + txtPubHolDetID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtPubHolDetID;
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
