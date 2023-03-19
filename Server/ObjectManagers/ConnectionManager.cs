using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.ObjectManagers
{
    internal static class ConnectionManager
    {
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(SingletonPool.ConnectionString);
        }
    }
}
