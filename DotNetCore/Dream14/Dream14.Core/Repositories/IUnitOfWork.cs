using System;

namespace Dream14.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository Repo { get; }
        IAdminRepository AdminRepo { get; }

        void BeginTransaction();
        void CommitTransaction();
    }
}
