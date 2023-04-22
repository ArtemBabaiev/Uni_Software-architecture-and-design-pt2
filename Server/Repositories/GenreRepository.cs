using Server.Models;
using Server.Repositories.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Server.Repositories
{
    internal class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction, "Genres")
        {
        }
    }
}
