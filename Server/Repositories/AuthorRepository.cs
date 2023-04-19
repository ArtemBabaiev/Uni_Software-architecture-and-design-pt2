using Server.Models;
using Server.Repositories.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Server.Repositories
{
    internal class AuthorRepository : GenericRepository<Author>, IAuthorReposiitory
    {
        public AuthorRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction, "Authors")
        {
        }
    }
}
