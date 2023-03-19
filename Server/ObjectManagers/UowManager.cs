using Server.UoW;
using Server.UoW.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
