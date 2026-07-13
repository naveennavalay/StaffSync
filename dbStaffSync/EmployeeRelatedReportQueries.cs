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
    public class EmployeeRelatedReportQueries
    {

        dbStaffSync dbStaffSync = new dbStaffSync();
        OleDbConnection conn = null;
        DataSet dtDataset;


        public List<ActiveEmployeeListReport> getActiveEmployeeListReport(int ClientID)
        {
            List<ActiveEmployeeListReport> objActiveEmployeeListReportList = new List<ActiveEmployeeListReport>();

            DataTable dt = new DataTable();

            try
            {
                conn = dbStaffSync.openDBConnection();
                dtDataset = new DataSet();

                string strQuery = "SELECT " + 
                                        " FinYearMas.FinYearFromTo, " + 
                                        " EmpMas.EmpID, " + 
                                        " EmpMas.EmpCode, " + 
                                        " EmpMas.EmpName, " + 
                                        " DesigMas.DesignationTitle, " + 
                                        " DepMas.DepartmentTitle, " + 
                                        " PersonalInfoMas.ContactNumber1, " + 
                                        " PersonalInfoMas.ContactNumber2, " + 
                                        " PersonalInfoMas.DOJ, " + 
                                        " PersonalInfoMas.LastDateOfProbation, " + 
                                        " PersonalInfoMas.DateOfConfirmation, " + 
                                        " SexMas.SexTitle, " + 
                                        " BloodGroupMas.BloodGroupTitle, " + 
                                        " NomineeMas.NomineePerson, " + 
                                        " NomineeMas.ContactNumber, " + 
                                        " RelationShipMas.RelationShipTitle, " + 
                                        " ClientBranchMas.ClientBranchCode, " + 
                                        " ClientBranchMas.ClientBranchName " + 
                                    " FROM " +
                                        " StateMas " +
                                        " INNER JOIN ( " +
                                            " SexMas " +
                                            " INNER JOIN ( " +
                                                " RelationShipMas " +
                                                " INNER JOIN ( " +
                                                    " FinYearMas " +
                                                    " INNER JOIN ( " +
                                                        " ( " +
                                                            " ( " +
                                                                " DesigMas " +
                                                                " INNER JOIN ( " +
                                                                    " DepMas " +
                                                                    " INNER JOIN ( " +
                                                                        " ClientMas " +
                                                                        " INNER JOIN ( " +
                                                                            " BloodGroupMas " +
                                                                            " INNER JOIN EmpMas ON BloodGroupMas.BloodGroupID = EmpMas.BloodGroupID " +
                                                                        " ) ON ClientMas.ClientID = EmpMas.ClientID " +
                                                                    " ) ON DepMas.DepartmentID = EmpMas.DepartmentID " +
                                                                " ) ON DesigMas.DesignationID = EmpMas.EmpDesignationID " +
                                                            " ) " +
                                                            " INNER JOIN NomineeMas ON EmpMas.EmpID = NomineeMas.EmpID " +
                                                        " ) " +
                                                        " INNER JOIN ( " +
                                                            " ClientBranchMas " +
                                                            " INNER JOIN ( " +
                                                                " AddressMas " +
                                                                " INNER JOIN PersonalInfoMas ON AddressMas.AddressID = PersonalInfoMas.PerAddressID " +
                                                            " ) ON ClientBranchMas.ClientBranchID = PersonalInfoMas.ClientBranchID " +
                                                        " ) ON EmpMas.EmpID = PersonalInfoMas.EmpID " +
                                                    " ) ON FinYearMas.FinYearID = ClientMas.FinYearID " +
                                                " ) ON RelationShipMas.RelationShipID = NomineeMas.RelationShipID " +
                                            " ) ON SexMas.SexID = PersonalInfoMas.SexID " +
                                        " ) ON StateMas.StateID = AddressMas.StateID " +
                                    " WHERE " +
                                        " ( " +
                                            " ((ClientMas.ClientID) = " + ClientID + ") " +
                                            " AND ((EmpMas.IsActive) = True) " +
                                            " AND ((EmpMas.IsDeleted) = False) " +
                                            " AND ((FinYearMas.FinYearID) = 1) " +
                                        " ) " +
                                    " ORDER BY " +
                                        " EmpMas.EmpID, " +
                                        " ClientMas.ClientID;";
                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                objActiveEmployeeListReportList = JsonConvert.DeserializeObject<List<ActiveEmployeeListReport>>(DataTableToJSon);
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

            return objActiveEmployeeListReportList;
        }
    }
}