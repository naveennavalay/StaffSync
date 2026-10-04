using ModelStaffSync;
using Newtonsoft.Json;
using ReportingEngine.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace dbStaffSync
{
    public class clsImportDataInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public List<ImportDataInfoModel> getImportInfoData()
        {
            List<ImportDataInfoModel> objImportDataInfoModelList = new List<ImportDataInfoModel>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "";
                strQuery = @"SELECT
                                    ImprtDataInfo.ImpDataInfoID,
                                    ImprtDataInfo.ImpDataInfoCode,
                                    ImprtDataInfo.ImpDataInfoTitle,
                                    ImprtDataInfo.ImpDataInfoDescription,
                                    ImprtDataInfo.ImpDataInfoTemplateName,
                                    ImprtDataInfo.IsActive,
                                    ImprtDataInfo.IsDeleted,
                                    ImprtDataInfo.OrderID
                                FROM
                                    ImprtDataInfo
                                WHERE
                                    (
                                        ((ImprtDataInfo.IsActive) = True)
                                        AND ((ImprtDataInfo.IsDeleted) = False)
                                    )
                                ORDER BY
                                    ImprtDataInfo.ImpDataInfoID,
                                    ImprtDataInfo.OrderID;";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objImportDataInfoModelList = JsonConvert.DeserializeObject<List<ImportDataInfoModel>>(DataTableToJSon);
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

            return objImportDataInfoModelList;
        }
    }
}
