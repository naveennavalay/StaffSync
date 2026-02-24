using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Common
{
    public static class DBCommon
    {
        public static int ExecuteScalarInt(OleDbCommand cmd)
        {
            object result = cmd.ExecuteScalar();
            return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
        }
    }
}
