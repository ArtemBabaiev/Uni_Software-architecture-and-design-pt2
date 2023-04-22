using Server.Models;
using Server.Repositories.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Server.Repositories
{
    internal class ExemplarRepository : GenericRepository<Exemplar>, IExemplarRepository
    {
        public ExemplarRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction, "Exemplars")
        {
        }
    }
}
