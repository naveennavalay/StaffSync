using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace StaffSync
{
    public class myDBClass
    {
        string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StaffsyncDB.accdb";
        public OleDbConnection conn = null;

        public OleDbConnection openDBConnection()
        {
            conn = new OleDbConnection(connString);
            if(conn.State == System.Data.ConnectionState.Closed)
                conn.Open();
            
            return conn;
        }

        public OleDbConnection closeDBConnection()
        {
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();

            return conn;
        }
    }
}
