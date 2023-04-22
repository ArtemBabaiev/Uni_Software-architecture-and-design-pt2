using Server.Repositories;
using Server.Repositories.Interfaces;
using Server.UoW.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace Server.UoW
{
    internal class UnitOfWork : IUnitOfWork, IDisposable
    {
        public IAuthorReposiitory AuthorRepository { get; }
        public IBookRepository BookRepository { get; }
        public IExemplarRepository ExemplarRepository { get; }
        public IGenreRepository GenreRepository { get; }
        public IPublisherRepository PublisherRepository { get; }

        protected readonly IDbTransaction _dbTransaction;

        public UnitOfWork(SqlConnection sqlConnection)
        {
            sqlConnection.Open();
            _dbTransaction = sqlConnection.BeginTransaction();
            AuthorRepository = new AuthorRepository(sqlConnection, _dbTransaction);
            BookRepository = new BookRepository(sqlConnection, _dbTransaction);
            ExemplarRepository = new ExemplarRepository(sqlConnection, _dbTransaction);
            GenreRepository = new GenreRepository(sqlConnection, _dbTransaction);
            PublisherRepository = new PublisherRepository(sqlConnection, _dbTransaction);
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
