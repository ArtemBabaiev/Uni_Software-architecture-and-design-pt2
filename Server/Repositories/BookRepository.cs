using Server.Models;
using Server.Repositories.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Server.Repositories
{
    internal class BookRepository : GenericRepository<Book>, IBookRepository
    {
        public BookRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction, "Books")
        {
        }
    }
}
