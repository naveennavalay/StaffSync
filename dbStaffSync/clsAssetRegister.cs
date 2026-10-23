using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace dbStaffSync
{
    public class clsAssetRegister
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        //public AssetInfo getAssetRegisterInfo(int txtAssetID)
        //{
        //    List<AssetInfo> objAssetInfoList = new List<AssetInfo>();
        //    DataTable dt = new DataTable();

        //    try
        //    {
        //        conn = dbStaffSync.openDBConnection();

        //        string strQuery = "SELECT " + 
        //                                " AssetMas.AssetID, " + 
        //                                " AssetMas.AssetCode, " + 
        //                                " AssetMas.AssetName, " + 
        //                                " AssetMas.AssetDescription, " + 
        //                                " AssetMas.IsActive, " + 
        //                                " AssetMas.IsDeleted, " + 
        //                                " AssetMas.AssetCatMasID, " + 
        //                                " AssetMas.IsRecoverable, " + 
        //                                " AssetMas.IsRequireReturn, " + 
        //                                " AssetMas.IsCriticalAsset, " + 
        //                                " AssetMas.RecoveryTypeID, " + 
        //                                " AssetMas.AffectsPayroll, " + 
        //                                " AssetMas.PayrollImpact, " + 
        //                                " AssetMas.PayrollHeaderID, " + 
        //                                " AssetMas.CurrentAssetStatusID " + 
        //                            " FROM " + 
        //                                " AssetMas " + 
        //                            " WHERE " + 
        //                                " AssetMas.AssetID = " + txtAssetInfoID;

        //        OleDbCommand cmd = conn.CreateCommand();
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = strQuery;
        //        cmd.ExecuteNonQuery();

        //        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        //        da.Fill(dt);

        //        string DataTableToJSon = "";
        //        DataTableToJSon = JsonConvert.SerializeObject(dt);
        //        objAssetInfoList = JsonConvert.DeserializeObject<List<AssetInfo>>(DataTableToJSon);
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        conn = dbStaffSync.closeDBConnection();
        //    }
        //    finally
        //    {
        //        conn = dbStaffSync.closeDBConnection();
        //    }

        //    if (objAssetInfoList.Count > 0)
        //        return objAssetInfoList[0];
        //    else
        //        return new AssetInfo();
        //}

        public SpecificAssetRegisterInfo getSpecificAssetRegisterInfo(int AssetID, int EmpAssetRequestID)
        {
            List<SpecificAssetRegisterInfo> objSpecificAssetRegisterInfoList = new List<SpecificAssetRegisterInfo>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = @"SELECT
                                        AssetRegister.AssetRegID,
                                        AssetRegister.AssetID,
                                        AssetRegister.CBalance,
                                        AssetRegister.EmpAdvanceRequestID
                                    FROM
                                        AssetRegister
                                    WHERE
                                        (
                                            ((AssetRegister.AssetID) = " + AssetID + @")
                                            AND ((AssetRegister.EmpAdvanceRequestID) = " + EmpAssetRequestID + @")
                                        );";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objSpecificAssetRegisterInfoList = JsonConvert.DeserializeObject<List<SpecificAssetRegisterInfo>>(DataTableToJSon);
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

            if (objSpecificAssetRegisterInfoList.Count > 0)
                return objSpecificAssetRegisterInfoList[0];
            else
                return new SpecificAssetRegisterInfo();
        }

        public AssetRegister getSpecificAssetRegisterInfo(int AssetID, DateTime AssetTRDate, int EmpAssetRequestID)
        {
            List<AssetRegister> objAssetRegisterInfo = new List<AssetRegister>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " + 
                                        " AssetRegister.AssetRegID, " + 
                                        " AssetRegister.AssetID, " + 
                                        " AssetRegister.AssetTRDate, " + 
                                        " AssetRegister.OBalance, " + 
                                        " AssetRegister.CrBalance, " + 
                                        " AssetRegister.DrBalance, " + 
                                        " AssetRegister.CBalance, " + 
                                        " AssetRegister.TRType, " + 
                                        " AssetRegister.Comments, " + 
                                        " AssetRegister.OrderID, " + 
                                        " AssetRegister.EmpAdvanceRequestID " + 
                                    " FROM " + 
                                        " AssetRegister " + 
                                    " WHERE " + 
                                            " AssetRegister.AssetID = " + AssetID + 
                                            " AND AssetRegister.AssetTRDate = #" + AssetTRDate.ToString("dd-MMM-yyyy") + "# " + 
                                            " AND AssetRegister.TRType = 'Rq' " + 
                                            " AND AssetRegister.EmpAdvanceRequestID = " + EmpAssetRequestID +
                                        ";";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAssetRegisterInfo = JsonConvert.DeserializeObject<List<AssetRegister>>(DataTableToJSon);
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

            if (objAssetRegisterInfo.Count > 0)
                return objAssetRegisterInfo[0];
            else
                return new AssetRegister();
        }

        public int InsertAssetRegisterInfo(int txtAssetID, DateTime dtAssetTRDate, decimal txtOBalance, decimal txtCrBalance, decimal txtDrBalance, decimal txtCBalance, string txtTRType, string txtComments, int txtEmpAdvanceRequestID)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("AssetRegister", "AssetRegID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO AssetRegister (AssetRegID, AssetID, AssetTRDate, OBalance, CrBalance, DrBalance, CBalance, TRType, Comments, OrderID, EmpAdvanceRequestID) VALUES " +
                 "(" + maxRowCount.Data + "," + txtAssetID + ", '" + dtAssetTRDate.ToString("dd-MMM-yyyy") + "', " + txtOBalance + "," + txtCrBalance + ", " + txtDrBalance + ", " + txtCBalance + ", '" + txtTRType + "', '" + txtComments + "', " + maxRowCount.Data  + ", " + txtEmpAdvanceRequestID + ")";

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

        public int UpdateAssetRegisterInfo(int txtAssetRegID, int txtAssetID, DateTime dtAssetTRDate, decimal txtOBalance, decimal txtCrBalance, decimal txtDrBalance, decimal txtCBalance, string txtTRType, string txtComments, int txtEmpAdvanceRequestID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE AssetRegister SET " +
                                        " AssetID = " + txtAssetID + ", AssetTRDate = '" + dtAssetTRDate.ToString("dd-MMM-yyyy") + "', OBalance = " + txtOBalance + ", CrBalance = " + txtCrBalance + ", DrBalance = " + txtDrBalance + ", CBalance = " + txtCBalance + ", TRType = '" + txtTRType + "', Comments = '" + txtComments + "'" +
                                  " WHERE " +
                                        " AssetRegID = " + txtAssetRegID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtAssetRegID;
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

        public int DeleteAssetRegisterInfo(int txtAssetRegID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "DELETE * FROM AssetRegister " +
                                  " WHERE " +
                                        " AssetID = " + txtAssetRegID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtAssetRegID;
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

        public List<AssetApproverPendingList> FirstApprovalPendingAssetApprovalList(int txtClientID, int FirstApproverID)
        {
            List<AssetApproverPendingList> objAssetApproverPendingListInfo = new List<AssetApproverPendingList>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "SELECT " + 
                                        " EmpMas.EmpID, " + 
                                        " EmpMas.EmpCode, " + 
                                        " EmpMas.EmpName, " + 
                                        " DesigMas.DesignationTitle, " + 
                                        " DepMas.DepartmentTitle, " + 
                                        " AssetMas.AssetID, " + 
                                        " AssetMas.AssetCode, " + 
                                        " AssetMas.AssetName, " + 
                                        " EmpAssetRequest.AssetRequestID, " + 
                                        " EmpAssetRequest.AssetRequestCode, " + 
                                        " EmpAssetRequest.AssetRequestDate, " + 
                                        " EmpAssetRequest.AssetRequestComments, " + 
                                        " EmpAssetRequest.AssetRequestByStatus, " + 
                                        " EmpAssetRequest.RequestedTo, " + 
                                        " EmpMas_1.EmpID AS ApproverEmpID, " + 
                                        " EmpMas_1.EmpCode AS ApproverEmpCode, " + 
                                        " DesigMas_1.DesignationTitle AS ApproverEmpDesignationTitle, " + 
                                        " DepMas_1.DepartmentTitle AS ApproverEmpDepartmentTitle, " + 
                                        " EmpAssetRequest.RequestedDate, " + 
                                        " EmpAssetRequest.RequestedToComments, " + 
                                        " EmpAssetRequest.AssetRequestStatus, " + 
                                        " ClientMas.ClientID " + 
                                    " FROM " +
                                        " ( " +
                                            " ClientMas " +
                                            " INNER JOIN ( " +
                                                " ( " +
                                                    " AssetCategoryMas " +
                                                    " INNER JOIN AssetMas ON AssetCategoryMas.AssetCatMasID = AssetMas.AssetCatMasID " +
                                                " ) " +
                                                " INNER JOIN ( " +
                                                    " DesigMas AS DesigMas_1 " +
                                                    " INNER JOIN ( " +
                                                        " DepMas AS DepMas_1 " +
                                                        " INNER JOIN ( " +
                                                            " EmpMas AS EmpMas_1 " +
                                                            " INNER JOIN EmpAssetRequest ON EmpMas_1.EmpID = EmpAssetRequest.RequestedTo " +
                                                        " ) ON DepMas_1.DepartmentID = EmpMas_1.DepartmentID " +
                                                    " ) ON DesigMas_1.DesignationID = EmpMas_1.EmpDesignationID " +
                                                " ) ON AssetMas.AssetID = EmpAssetRequest.AssetID " +
                                            " ) ON ClientMas.ClientID = AssetCategoryMas.ClientID " +
                                        " ) " +
                                        " INNER JOIN ( " +
                                            " DepMas " +
                                            " INNER JOIN ( " +
                                                " DesigMas " +
                                                " INNER JOIN EmpMas ON DesigMas.DesignationID = EmpMas.EmpDesignationID " +
                                            " ) ON DepMas.DepartmentID = EmpMas.DepartmentID " +
                                        " ) ON ClientMas.ClientID = EmpMas.ClientID " +
                                    " WHERE " +
                                        " ( " +
                                            " ((EmpAssetRequest.AssetRequestByStatus) = False) " +
                                            " AND ((ClientMas.ClientID) = " + txtClientID + ") " +
                                        " ) " +
                                    " ORDER BY " +
                                        " EmpAssetRequest.AssetRequestID;";
                strQuery = @"SELECT
                                E.EmpID,
                                E.EmpCode,
                                E.EmpName,
                                D.DesignationTitle,
                                P.DepartmentTitle,
                                C.AssetCatMasID,
                                C.AssetName AS AssetCategoryName,
                                A.AssetID,
                                A.AssetCode,
                                A.AssetName,
                                R.AssetRequestID,
                                R.AssetRequestCode,
                                R.AssetRequestDate,
                                R.AssetRequestComments,
                                R.AssetRequestByStatus,
                                IIf(R.AssetRequestByStatus = False, ""Pending"", """") AS RequestStatus,
                                R.RequestedTo,
                                R.RequestedDate,
                                R.RequestedToStatus,
                                R.RequestedToComments,
                                R.AssetRequestStatus,
                                CL.ClientID
                            FROM
                                DesigMas AS D
                                INNER JOIN (
                                    DepMas AS P
                                    INNER JOIN (
                                        ClientMas AS CL
                                        INNER JOIN (
                                            (
                                                AssetCategoryMas AS C
                                                INNER JOIN AssetMas AS A ON C.AssetCatMasID = A.AssetCatMasID
                                            )
                                            INNER JOIN (
                                                EmpAssetRequest AS R
                                                INNER JOIN EmpMas AS E ON R.AssetRequestByID = E.EmpID
                                            ) ON A.AssetID = R.AssetID
                                        ) ON CL.ClientID = E.ClientID
                                    ) ON P.DepartmentID = E.DepartmentID
                                ) ON D.DesignationID = E.EmpDesignationID
                            WHERE
                                (
                                    ((E.EmpID) > 1)
                                    AND ((R.AssetRequestByID) = " + FirstApproverID + @")
                                    AND ((R.AssetRequestByStatus) = False)
                                    AND ((R.RequestedToStatus) = False)
                                    AND ((R.AssetRequestStatus) = False)
                                    AND ((R.IsActive) = True)
                                    AND ((R.IsDeleted) = False)
                                    AND ((CL.ClientID) = " + txtClientID + @")
                                    AND ((C.ClientID) = [CL].[ClientID])
                                )
                            ORDER BY
                                R.AssetRequestID,
                                R.AssetRequestDate;";
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAssetApproverPendingListInfo = JsonConvert.DeserializeObject<List<AssetApproverPendingList>>(DataTableToJSon);
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

            return objAssetApproverPendingListInfo;
        }

        public List<AssetApproverPendingList> SecondApprovalPendingAssetApprovalList(int txtClientID, int SecondApproverID)
        {
            List<AssetApproverPendingList> objAssetApproverPendingListInfo = new List<AssetApproverPendingList>();
            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();

                string strQuery = "";
                strQuery = @"SELECT
                                E.EmpID,
                                E.EmpCode,
                                E.EmpName,
                                D.DesignationTitle,
                                P.DepartmentTitle,
                                C.AssetCatMasID,
                                C.AssetName AS AssetCategoryName,
                                A.AssetID,
                                A.AssetCode,
                                A.AssetName,
                                R.AssetRequestID,
                                R.AssetRequestCode,
                                R.AssetRequestDate,
                                R.AssetRequestComments,
                                R.AssetRequestByStatus,
                                IIf(R.AssetRequestByStatus = False, ""Pending"", """") AS RequestStatus,
                                R.RequestedTo,
                                R.RequestedDate,
                                R.RequestedToStatus,
                                R.RequestedToComments,
                                R.AssetRequestStatus,
                                CL.ClientID
                            FROM
                                DesigMas AS D
                                INNER JOIN (
                                    DepMas AS P
                                    INNER JOIN (
                                        ClientMas AS CL
                                        INNER JOIN (
                                            (
                                                AssetCategoryMas AS C
                                                INNER JOIN AssetMas AS A ON C.AssetCatMasID = A.AssetCatMasID
                                            )
                                            INNER JOIN (
                                                EmpAssetRequest AS R
                                                INNER JOIN EmpMas AS E ON R.AssetRequestByID = E.EmpID
                                            ) ON A.AssetID = R.AssetID
                                        ) ON CL.ClientID = E.ClientID
                                    ) ON P.DepartmentID = E.DepartmentID
                                ) ON D.DesignationID = E.EmpDesignationID
                            WHERE
                                (
                                    ((E.EmpID) > 1)
                                    AND ((R.RequestedTo) = " + SecondApproverID + @")
                                    AND ((R.AssetRequestByStatus) = True)
                                    AND ((R.RequestedToStatus) = True)
                                    AND ((R.AssetRequestStatus) = False)
                                    AND ((R.IsActive) = True)
                                    AND ((R.IsDeleted) = False)
                                    AND ((CL.ClientID) = " + txtClientID + @")
                                    AND ((C.ClientID) = [CL].[ClientID])
                                )
                            ORDER BY
                                R.AssetRequestID,
                                R.AssetRequestDate;";
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objAssetApproverPendingListInfo = JsonConvert.DeserializeObject<List<AssetApproverPendingList>>(DataTableToJSon);
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

            return objAssetApproverPendingListInfo;
        }
    }
}
