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
    public class clsAdvanceTypeConfigInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public AdvanceTypeConfigModel GetAdvanceTypeConfigByID(int txtAdvanceTypeID)
        {
            List<AdvanceTypeConfigModel> objAdvanceTypeConfigList = new List<AdvanceTypeConfigModel>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " +
                                        " AdvanceTypeConfigID, AdvanceTypeID, AutoDeductFromSalary, BasedOnNetOrGross, MaxPerOfNetOrGross, MaxPercentage, MaxFixed, IncludeInSalary, RecoveryRequired, AutoRecoveryFromNextSalary, InterestRequired, ApprovalRequired, AllowPause, WaiverAllowed, MaxTenure, OrderID " + 
                                  " FROM AdvanceConfigInfo " + 
                                  " WHERE " + 
                                        " AdvanceTypeID = " + txtAdvanceTypeID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAdvanceTypeConfigList = JsonConvert.DeserializeObject<List<AdvanceTypeConfigModel>>(DataTableToJSon);
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
            
            return objAdvanceTypeConfigList[0];
        }

        public int InsertAdvanceTypeConfig(int txtAdvanceTypeID, bool AutoDeductFromSalary, string txtBasedOnNetOrGross, string txtMaxPerOfNetOrGross, decimal txtMaxPercentage, decimal txtMaxFixed, bool IncludeInSalary, bool RecoveryRequired, bool AutoRecoveryFromNextSalary, bool InterestRequired, bool ApprovalRequired, bool AllowPause, bool WaiverAllowed, decimal MaxTenure)
        {
            int affectedRows = 0;
            try
            {

                Response<int> maxRowCount = objGenFunc.getMaxRowCount("AdvanceConfigInfo", "AdvanceTypeConfigID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO AdvanceConfigInfo (AdvanceTypeConfigID, AdvanceTypeID, AutoDeductFromSalary, BasedOnNetOrGross, MaxPerOfNetOrGross, MaxPercentage, MaxFixed, IncludeInSalary, RecoveryRequired, AutoRecoveryFromNextSalary, InterestRequired, ApprovalRequired, AllowPause, WaiverAllowed, MaxTenure, OrderID) VALUES " +
                 "(" + maxRowCount.Data + "," + txtAdvanceTypeID + "," + AutoDeductFromSalary + ",'" + txtBasedOnNetOrGross + "','" + txtMaxPerOfNetOrGross + "'," + txtMaxPercentage + "," + txtMaxFixed + "," + IncludeInSalary + "," + RecoveryRequired + "," + AutoRecoveryFromNextSalary + "," + InterestRequired + "," + ApprovalRequired + "," + AllowPause + "," + WaiverAllowed + "," + MaxTenure + "," + maxRowCount.Data +  ")";

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

        public int UpdateAdvanceTypeConfig(int AdvanceTypeConfigID, int txtAdvanceTypeID, bool AutoDeductFromSalary, string txtBasedOnNetOrGross, string txtMaxPerOfNetOrGross, decimal txtMaxPercentage, decimal txtMaxFixed, bool IncludeInSalary, bool RecoveryRequired, bool AutoRecoveryFromNextSalary, bool InterestRequired, bool ApprovalRequired, bool AllowPause, bool WaiverAllowed, decimal MaxTenure)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE AdvanceConfigInfo SET " +
                 " AutoDeductFromSalary = " + AutoDeductFromSalary + "," + 
                 " BasedOnNetOrGross = '" + txtBasedOnNetOrGross.ToString() + "'," +
                 " MaxPerOfNetOrGross = '" + txtMaxPerOfNetOrGross + "'," +
                 " MaxPercentage = " + txtMaxPercentage + "," +
                 " MaxFixed = " + txtMaxFixed + "," +
                 " IncludeInSalary = " + IncludeInSalary + "," +
                 " RecoveryRequired = " + RecoveryRequired + "," +
                 " AutoRecoveryFromNextSalary = " + AutoRecoveryFromNextSalary + "," +
                 " InterestRequired = " + InterestRequired + "," +
                 " ApprovalRequired = " + ApprovalRequired + "," +
                 " AllowPause = " + AllowPause + "," +
                 " WaiverAllowed = " + WaiverAllowed + "," +
                 " MaxTenure = " + MaxTenure +
                 " WHERE AdvanceTypeConfigID = " + AdvanceTypeConfigID.ToString().Trim();

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
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

        public int DeleteAdvanceTypeConfig(int txtAdvanceTypeConfigID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE * FROM AdvanceConfigInfo WHERE AdvanceTypeConfigID = " + txtAdvanceTypeConfigID.ToString().Trim();

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
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
