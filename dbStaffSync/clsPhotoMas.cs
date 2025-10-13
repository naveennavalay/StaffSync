﻿using ModelStaffSync;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace dbStaffSync
{
    public class clsPhotoMas
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset = null;
        clsEncryptDecrypt objEncryptDecrypt = new clsEncryptDecrypt();
        clsGenFunc objGenFunc = new clsGenFunc();

        public clsPhotoMas() { 

        }

        public PhotoInfo getEmployeePhoto(int txtEmpID)
        {
            PhotoInfo photoInfo = new PhotoInfo();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT * FROM Photos WHERE EmpID = " + txtEmpID + "";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                List<PhotoInfo> objPhotoInfo = JsonConvert.DeserializeObject<List<PhotoInfo>>(DataTableToJSon);
                if (objPhotoInfo.Count > 0)
                {
                    photoInfo.PhotoID = objPhotoInfo[0].PhotoID;
                    photoInfo.EmpID = objPhotoInfo[0].EmpID;
                    photoInfo.EmpPhoto = objPhotoInfo[0].EmpPhoto;
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

            return photoInfo;
        }

        public int InsertPhotoInfo(int txtEmpID, byte[] txtPhoto)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("Photos", "PhotoID");

                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "INSERT INTO Photos (PhotoID, EmpID, EmpPhoto) VALUES " +
                 "(" + maxRowCount.Data + "," + txtEmpID + ",'" + txtPhoto + "')";

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = maxRowCount.Data;


                cmd = new OleDbCommand("UPDATE Photos SET EmpPhoto=@Image WHERE EmpID = " + txtEmpID + "", conn);

                // Add the image as a parameter.
                OleDbParameter param = new OleDbParameter();
                param.OleDbType = OleDbType.Binary;
                param.ParameterName = "Image";
                param.Value = txtPhoto;
                cmd.Parameters.Add(param);
                cmd.ExecuteScalar();

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

        public int UpdatePhotoInfo(int txtEmpID, byte[] txtPhoto)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "UPDATE Photos SET EmpPhoto = '" + txtPhoto + "'" +
                " WHERE EmpID = " + txtEmpID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtEmpID;

                cmd = new OleDbCommand("UPDATE Photos SET EmpPhoto=@Image WHERE EmpID = " + txtEmpID + "", conn);

                // Add the image as a parameter.
                OleDbParameter param = new OleDbParameter();
                param.OleDbType = OleDbType.Binary;
                param.ParameterName = "Image";
                param.Value = txtPhoto;
                cmd.Parameters.Add(param);
                cmd.ExecuteScalar();
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
