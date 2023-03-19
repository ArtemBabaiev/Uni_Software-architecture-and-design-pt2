using Server.Repositories;
using Server.Repositories.Interfaces;
using Server.UoW.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.UoW
{
    internal class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IAuthorReposiitory AuthorRepository { get; }
        protected readonly IDbTransaction _dbTransaction;

        public UnitOfWork(SqlConnection sqlConnection)
        {
            sqlConnection.Open();
            _dbTransaction = sqlConnection.BeginTransaction();
            AuthorRepository = new AuthorRepository(sqlConnection, _dbTransaction);
        }

        public void Commit()
        {
            try
            {
                _dbTransaction.Commit();
            }
            catch (Exception)
            {
                _dbTransaction.Rollback();
            }
        }

        public void Dispose()
        {
            _dbTransaction.Connection?.Close();
            _dbTransaction.Connection?.Dispose();
            _dbTransaction.Dispose();
        }
    }
}
