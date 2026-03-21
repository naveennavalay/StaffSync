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
    }
}
