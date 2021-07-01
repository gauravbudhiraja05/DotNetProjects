using ChangeCalculator.Core.Repositories;
using System;

namespace ChangeCalculator.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository Repo { get; private set; }
        public IGBPRepository GBPRepo { get; private set; }
        public IUSDRepository USDRepo { get; private set; }
        public IEURRepository EURRepo { get; private set; }

        public UnitOfWork()
        {
            Repo = new Repository();
            GBPRepo = new GBPRepository();
            USDRepo = new USDRepository();
            EURRepo = new EURRepository();
        }

        public void BeginTransaction()
        {
           
        }

        public void CommitTransaction()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}
