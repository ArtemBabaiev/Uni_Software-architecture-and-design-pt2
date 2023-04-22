using Server.Models;
using Server.Repositories.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Server.Repositories
{
    internal class PublisherRepository : GenericRepository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction, "Publishers")
        {
        }

    }
}
