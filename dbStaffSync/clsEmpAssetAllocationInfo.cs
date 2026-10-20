using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace dbStaffSync
{
    public class clsEmpAssetAllocationInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset = null;
        clsGenFunc objGenFunc = new clsGenFunc();

        public clsEmpAssetAllocationInfo() { 

        }

        public void getEmployeeAssetRequestInfoList(int EmployeeID)
        {

        }

        public int InsertEmployeeAssetAllocationShortInformation(int EmpAssetAllocationID, int PersonalInfoID, int AssetID, DateTime TransDate, string Description, int AssetRequestID)
        {
            int affectedRows = 0;
            try
            {

                Response<int> maxRowCount = objGenFunc.getMaxRowCount("EmpAssetAllocation", "EmpAssetAllocationID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpAssetAllocation (EmpAssetAllocationID, PersonalInfoID, AssetID, TransDate, CurrentAssetStatusID, Description, ParentEmpAssetAllocationID, AssetRequestID) VALUES " +
                 "(" + maxRowCount.Data + "," + PersonalInfoID + "," + AssetID + ",#" + TransDate.ToString("dd-MMM-yyyy") + "#, 2, '" + Description + "', 0," + AssetRequestID + ")";

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


        public int InsertEmployeeAssetAllocationInformation(int EmpAssetAllocationID, int PersonalInfoID, int AssetID, DateTime TransDate, DateTime IssueDate, string IssueCondition, int IssuedBy, string IssueRemarks, DateTime ReturnDate, string ReturnCondition, int ReturnedTo, string ReturnRemarks, int CurrentAssetStatusID, string Description, int ParentEmpAssetAllocationID, int AssetRequestID)
        {
            int affectedRows = 0;
            try
            {

                Response<int> maxRowCount = objGenFunc.getMaxRowCount("EmpAssetAllocation", "EmpAssetAllocationID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO EmpAssetAllocation (EmpAssetAllocationID, PersonalInfoID, AssetID, TransDate, IssueDate, IssueCondition, IssuedBy, IssueRemarks, ReturnDate, ReturnCondition, ReturnedTo, ReturnRemarks, CurrentAssetStatusID, Description, ParentEmpAssetAllocationID, AssetRequestID) VALUES " +
                 "(" + maxRowCount.Data + "," + PersonalInfoID + "," + AssetID + ",'" + TransDate + "','" + IssueDate + "','" + IssueCondition + "'," + IssuedBy + ",'" + IssueRemarks + "','" + ReturnDate + "','" + ReturnCondition + "'," + ReturnedTo + ",'" + ReturnRemarks + "'," + CurrentAssetStatusID + ",'" + Description + "'," + ParentEmpAssetAllocationID + "," + AssetRequestID + ")";

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

        public int UdpateNomineeInfo(int txtNomineeID, string txtNomineePerson, int EmpID, int txtNomineeRelationship, string txtContactNumber)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE NomineeMas SET " +
                "NomineePerson = '" + txtNomineePerson + "', RelationShipID = " + txtNomineeRelationship + ", ContactNumber = '" + txtContactNumber + "'" +
                " WHERE NomineeID = " + txtNomineeID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtNomineeID;
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
