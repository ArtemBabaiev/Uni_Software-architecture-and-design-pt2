using Server.Models;
using Server.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Repositories
{
    internal class AuthorRepository : GenericRepository<Author>, IAuthorReposiitory
    {
        public AuthorRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction, string tableName) : base(sqlConnection, dbTransaction, tableName)
        {
        }
    }
}
