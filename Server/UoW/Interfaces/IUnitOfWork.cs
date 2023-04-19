using Server.Repositories.Interfaces;

namespace Server.UoW.Interfaces
{
    internal interface IUnitOfWork : IDisposable
    {
        IAuthorReposiitory AuthorRepository { get; }

        void Commit();
        void Dispose();
    }
}
