using Server.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.UoW.Interfaces
{
    internal interface IUnitOfWork: IDisposable
    {
        IAuthorReposiitory AuthorRepository { get; }

        void Commit();
        void Dispose();
    }
}
