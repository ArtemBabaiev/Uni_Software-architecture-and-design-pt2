using Server.Repositories.Interfaces;

namespace Server.UoW.Interfaces
{
    internal interface IUnitOfWork : IDisposable
    {
        IAuthorReposiitory AuthorRepository { get; }
        IBookRepository BookRepository { get; }
        IExemplarRepository ExemplarRepository { get; }
        IGenreRepository GenreRepository { get; }
        IPublisherRepository PublisherRepository { get; }

        void Commit();
        void Dispose();
    }
}
