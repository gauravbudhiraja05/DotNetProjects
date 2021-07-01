using System;

namespace ChangeCalculator.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository Repo { get; }
        IGBPRepository GBPRepo { get; }
        IUSDRepository USDRepo { get; }
        IEURRepository EURRepo { get; }
        void BeginTransaction();
        void CommitTransaction();
    }
}
