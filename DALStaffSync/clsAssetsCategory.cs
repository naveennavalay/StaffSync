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
    public class clsAssetsCategory
    {
        dbStaffSync.clsAssetsCategory objAssetsCategory = new dbStaffSync.clsAssetsCategory();


        public DataTable getAssetsCategoryNamesList(int txtClientID)
        {
            return objAssetsCategory.getAssetsCategoryNamesList(txtClientID);
        }

        public DataTable getRecoveryTypeNamesList()
        {
            return objAssetsCategory.getRecoveryTypeNamesList();
        }

        public DataTable getCurrentStatusNamesList()
        {
            return objAssetsCategory.getCurrentStatusNamesList();
        }

        public List<AssetsCategory> getAssetsCategoryList(int txtClientID)
        {
            return objAssetsCategory.getAssetsCategoryList(txtClientID);
        }

        public AssetsCategory getAssetsCategoryInfo(int txtAssetCatMasID, int txtClientID)
        {
            return objAssetsCategory.getAssetsCategoryInfo(txtAssetCatMasID, txtClientID);
        }

        public AssetsCategory getAssetsCategoryInfo(string txtAssetName, int txtClientID)
        {
            return objAssetsCategory.getAssetsCategoryInfo(txtAssetName, txtClientID);
        }

        public List<AssetsCategory> getAssetsCategoryInfoFilter(string txtAssetName, int txtClientID)
        {
            return objAssetsCategory.getAssetsCategoryInfoFilter(txtAssetName, txtClientID);
        }

        public int InsertAssetCategoryInfo(string txtAssetCode, string txtAssetName, string txtAssetDescription, string txtAssetNote, bool IsActive, bool IsDeleted, int txtClientID)
        {
            int affectedRows = 0;

            affectedRows = objAssetsCategory.InsertAssetCategoryInfo(txtAssetCode, txtAssetName, txtAssetDescription, txtAssetNote, IsActive, IsDeleted, txtClientID);

            return affectedRows;
        }

        public int UpdateAssetCategoryInfo(int intAssetCatMasID, string txtAssetCode, string txtAssetName, string txtAssetDescription, string txtAssetNote, bool IsActive, bool IsDeleted, int txtClientID)
        {
            int affectedRows = 0;

            affectedRows = objAssetsCategory.UpdateAssetCategoryInfo(intAssetCatMasID, txtAssetCode, txtAssetName,txtAssetDescription, txtAssetNote,IsActive, IsDeleted, txtClientID);

            return affectedRows;
        }

        public int DeleteAssetCategoryInfo(int intAssetCatMasID)
        {
            int affectedRows = 0;

            affectedRows = objAssetsCategory.DeleteAssetCategoryInfo(intAssetCatMasID);

            return affectedRows;
        }
    }
}
