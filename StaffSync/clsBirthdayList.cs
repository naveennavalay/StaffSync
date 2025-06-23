using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StaffSync
{
    public class clsBirthdayList
    {
        myDBClass objDBClass = new myDBClass();
        OleDbConnection conn = null;
        clsStates objState = new clsStates();
        clsCountries objCountry = new clsCountries();

        public clsBirthdayList() { 

        }

        public List<BirthdayList> GetEmployeesBirthdayList()
        {
            List<BirthdayList> birthdayList = new List<BirthdayList>();
            DataTable dt = new DataTable();

            try
            {
                conn = objDBClass.openDBConnection();

                string strQuery = "SELECT * FROM qryEmpBirthdayList WHERE DAY(DOB) = " + DateTime.Now.Day + " AND Month(DOB) = " + DateTime.Now.Month;

                OleDbCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.ExecuteNonQuery();

                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                da.Fill(dt);

                string DataTableToJSon = "";
                DataTableToJSon = JsonConvert.SerializeObject(dt);
                birthdayList = JsonConvert.DeserializeObject<List<BirthdayList>>(DataTableToJSon);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Staffsync", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn = objDBClass.closeDBConnection();
            }
            finally
            {
                conn = objDBClass.closeDBConnection();
            }

            return birthdayList;
        }
    }

    public class BirthdayList
    {
        public int EmpID { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string DepartmentTitle { get; set; }
        public string DesignationTitle { get; set; }
        public DateTime DOB { get; set; }
        public byte[] EmpPhoto { get; set; }
    }
}
