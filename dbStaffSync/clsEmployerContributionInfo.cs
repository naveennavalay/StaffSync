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
    public class clsEmployerContributionInfo
    {
        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;
        clsGenFunc objGenFunc = new clsGenFunc();

        public int InsertEmployerContribution(int txtEmpSalDetID, decimal txtEmprPFAmount, decimal txtEmprPFAdminCharges, decimal txtEmprNPSAmount, decimal txtEmprESIAmount, decimal txtEmprGratuityAmount, decimal txtEmprBonusAmount)
        {
            int affectedRows = 0;
            try
            {
                Response<int> maxRowCount = objGenFunc.getMaxRowCount("EmprContribution", "EmprContributionID");

                conn = dbStaffSync.openDBConnection();
                string strQuery = "INSERT INTO EmprContribution (EmprContributionID, EmpSalDetID, EmprPFAmount, EmprPFAdminCharges, EmprNPSAmount, EmprESIAmount, EmprGratuityAmount, EmprBonusAmount, OrderID) VALUES (" +
                                   maxRowCount.Data + "," + txtEmpSalDetID + "," + txtEmprPFAmount + "," + txtEmprPFAdminCharges + "," + txtEmprNPSAmount + "," + txtEmprESIAmount + "," + txtEmprGratuityAmount + "," + txtEmprBonusAmount + "," + maxRowCount.Data + ")";
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

        public int UpdateEmployerContribution(int txtEmpSalDetID, decimal txtEmprPFAmount, decimal txtEmprPFAdminCharges, decimal txtEmprNPSAmount, decimal txtEmprESIAmount, decimal txtEmprGratuityAmount, decimal txtEmprBonusAmount, int txtOrderID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                string strQuery = "UPDATE EmprContribution " +
                            " SET " +
                                " EmprContribution.EmprPFAmount = " + txtEmprPFAmount + ", " +
                                " EmprContribution.EmprPFAdminCharges = " + txtEmprPFAdminCharges + ", " +
                                " EmprContribution.EmprNPSAmount = " + txtEmprNPSAmount + ", " +
                                " EmprContribution.EmprESIAmount = " + txtEmprESIAmount + ", " +
                                " EmprContribution.EmprGratuityAmount = " + txtEmprGratuityAmount + ", " +
                                " EmprContribution.EmprBonusAmount = " + txtEmprBonusAmount +
                            " WHERE " +
                                " EmprContribution.EmpSalDetID = " + txtEmpSalDetID + " AND EmprContribution.OrderID = " + txtOrderID;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                    affectedRows = txtEmpSalDetID;
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

        public int DeleteEmployerContribution(int txtEmpSalDetID)
        {
            int affectedRows = 0;
            try
            {
                conn = dbStaffSync.openDBConnection();
                string strQuery = "DELETE FROM EmprContribution WHERE EmpSalDetID = " + txtEmpSalDetID;
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
