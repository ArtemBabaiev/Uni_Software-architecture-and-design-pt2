using Server.UoW;
using Server.UoW.Interfaces;
using System.Data.SqlClient;

namespace Server.ObjectManagers
{
    internal static class UowManager
    {
        public static IUnitOfWork GetUnitOfWork(SqlConnection connection)
        {
            return new UnitOfWork(connection);
        }
    }
}
