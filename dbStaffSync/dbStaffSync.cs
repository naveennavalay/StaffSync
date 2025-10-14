using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace dbStaffSync
{
    public class dbStaffSync
    {
        //string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\StaffsyncDB.accdb";
        string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Development\\StaffSync\\StaffSync\\bin\\Debug\\StaffsyncDB.accdb";
        public OleDbConnection conn = null;

        public OleDbConnection openDBConnection()
        {
            conn = new OleDbConnection(connString);
            if (conn.State == System.Data.ConnectionState.Closed)
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
