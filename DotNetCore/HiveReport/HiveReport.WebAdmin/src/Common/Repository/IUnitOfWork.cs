using HiveReport.WebAdmin.Dashboard.Repository;
using HiveReport.WebAdmin.Global.Repository;
using HiveReport.WebAdmin.User.Repository;
using System;

namespace HiveReport.WebAdmin.Global.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IDao Dao { get; }
        IUserDao UserDao { get; }
        IDashboardDao DashboardDao { get; }
        void BeginTransaction();
        void CommitTransaction();
    }
}
