using ModelStaffSync;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace DALStaffSync
{
    public class clsAssetRegister
    {
        dbStaffSync.clsAssetRegister objAssetsRegisterInfo = new dbStaffSync.clsAssetRegister();


        public List<CurrentAssetStatusModel> GetCurrentAssetStatus()
        {
            return objAssetsRegisterInfo.GetCurrentAssetStatus();
        }

        public AssetRegister getSpecificAssetRegisterInfo(int AssetID, DateTime AssetTRDate, int EmpAssetRequestID)
        {
            return objAssetsRegisterInfo.getSpecificAssetRegisterInfo(AssetID, AssetTRDate, EmpAssetRequestID);
        }

        public SpecificAssetRegisterInfo getSpecificAssetRegisterInfo(int AssetID, int EmpAssetRequestID)
        {
            return objAssetsRegisterInfo.getSpecificAssetRegisterInfo(AssetID, EmpAssetRequestID);
        }

        public int InsertAssetRegisterInfo(int txtAssetID, DateTime dtAssetTRDate, decimal txtOBalance, decimal txtCrBalance, decimal txtDrBalance, decimal txtCBalance, string txtTRType, string txtComments, int txtEmpAdvanceRequestID)
        {
            return objAssetsRegisterInfo.InsertAssetRegisterInfo(txtAssetID, dtAssetTRDate, txtOBalance, txtCrBalance, txtDrBalance, txtCBalance, txtTRType, txtComments, txtEmpAdvanceRequestID);
        }

        public int UpdateAssetRegisterInfo(int txtAssetRegID, int txtAssetID, DateTime dtAssetTRDate, decimal txtOBalance, decimal txtCrBalance, decimal txtDrBalance, decimal txtCBalance, string txtTRType, string txtComments, int txtEmpAdvanceRequestID)
        {
            return objAssetsRegisterInfo.UpdateAssetRegisterInfo(txtAssetRegID, txtAssetID, dtAssetTRDate, txtOBalance,txtCrBalance, txtDrBalance,txtCBalance, txtTRType,txtComments, txtEmpAdvanceRequestID);
        }

        public int DeleteAssetRegisterInfo(int txtAssetRegID)
        {
            return objAssetsRegisterInfo.DeleteAssetRegisterInfo(txtAssetRegID);
        }

        public int UpdatgeEmployeeAssetAllocationInfo(int EmpAssetAllocationID, int AssetID, DateTime IssueDate, string IssueCondition, int IssuedBy, string IssueRemarks, int CurrentAssetStatusID, int AssetRequestID)
        {
            return objAssetsRegisterInfo.UpdatgeEmployeeAssetAllocationInfo(EmpAssetAllocationID, AssetID, IssueDate, IssueCondition, IssuedBy, IssueRemarks, CurrentAssetStatusID, AssetRequestID);
        }

        public List<AssetApproverPendingList> ApprovalPendingAssetApprovalList(int txtClientID)
        {
            return objAssetsRegisterInfo.ApprovalPendingAssetApprovalList(txtClientID);
        }

        public List<AssetApproverPendingList> FirstApprovalPendingAssetApprovalList(int txtClientID, int FirstApproverID)
        {
            return objAssetsRegisterInfo.FirstApprovalPendingAssetApprovalList(txtClientID, FirstApproverID);
        }

        public List<AssetApproverPendingList> SecondApprovalPendingAssetApprovalList(int txtClientID, int SecondApproverID)
        {
            return objAssetsRegisterInfo.SecondApprovalPendingAssetApprovalList(txtClientID, SecondApproverID);
        }

        public List<ApprovedAssetInformationRequestList> getApprovedAssetInformationRequestList(int ClientID)
        {
            return objAssetsRegisterInfo.getApprovedAssetInformationRequestList(ClientID);
        }

        public List<AssetInformationOfAnAssetRequest> getAssetInformationOfAnAssetRequest(int AssetRequestID) 
        {
            return objAssetsRegisterInfo.getAssetInformationOfAnAssetRequest(AssetRequestID);
        }

        public List<AssetAnalyticsInformation> getAssetAnalyticsInformation(int ClientID)
        {
            return objAssetsRegisterInfo.getAssetAnalyticsInformation(ClientID);
        }
    }
}
