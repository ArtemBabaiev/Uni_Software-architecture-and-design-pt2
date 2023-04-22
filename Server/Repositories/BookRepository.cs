using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Models;
using Server.Repositories.Interfaces;

namespace Server.Repositories
{
    internal class BookRepository: GenericRepository<Book>, IBookRepository
    {
        public BookRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction) : base(sqlConnection, dbTransaction, "Books")
        {
        }
    }
}
