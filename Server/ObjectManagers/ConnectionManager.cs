using System.Data.SqlClient;

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
