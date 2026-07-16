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
    public class clsEmpActiveInactiveStatusInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();


        public List<EmpActiveInactiveStatusInfoModel> getEmpActiveInactiveStatusInfo(int txtEmpID, string txtEmpActiveInactiveStatusComments)
        {
            List<EmpActiveInactiveStatusInfoModel> objEmpActiveInactiveStatusInfoList = new List<EmpActiveInactiveStatusInfoModel>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " + 
                                        " EmpActiveInactiveStatusInfo.EmpActiveInactiveStatusID, " + 
                                        " EmpActiveInactiveStatusInfo.PersonalInfoID, " +
                                        " EmpActiveInactiveStatusInfo.ActiveInactiveStatus, " +
                                        " EmpActiveInactiveStatusInfo.ActiveInactiveStatusDate, " + 
                                        " EmpActiveInactiveStatusInfo.Comments " +
                                    " FROM " + 
                                        " ( " + 
                                            " EmpMas INNER JOIN PersonalInfoMas ON EmpMas.EmpID = PersonalInfoMas.EmpID " + 
                                        " ) " + 
                                        " INNER JOIN EmpActiveInactiveStatusInfo ON PersonalInfoMas.PersonalInfoID = EmpActiveInactiveStatusInfo.PersonalInfoID " + 
                                    " WHERE " + 
                                        " ( " + 
                                            " ((EmpMas.EmpID) = " + txtEmpID + " ) " + 
                                        " )";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objEmpActiveInactiveStatusInfoList = JsonConvert.DeserializeObject<List<EmpActiveInactiveStatusInfoModel>>(DataTableToJSon);
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

            return objEmpActiveInactiveStatusInfoList;
        }

        public int InsertEmpActiveInactiveStatus(int txtEmpPersonalID, bool ActiveInactiveStatus, DateTime txtActiveInactiveDate, string txtComments)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("EmpActiveInactiveStatusInfo", "EmpActiveInactiveStatusID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpActiveInactiveStatusInfo (EmpActiveInactiveStatusID, PersonalInfoID, ActiveInactiveStatus, ActiveInactiveStatusDate, Comments) VALUES " +
                 "(" + maxRowCount.Data + "," + txtEmpPersonalID + ", " + ActiveInactiveStatus + ", '" + txtActiveInactiveDate.Date.ToString("dd-MMM-yyyy hh:mm:ss tt") + "','" + txtComments + "')";

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
    }
}
