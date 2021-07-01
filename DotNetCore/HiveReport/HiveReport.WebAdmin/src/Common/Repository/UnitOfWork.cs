using HiveReport.WebAdmin.Dashboard.Repository;
using HiveReport.WebAdmin.User.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HiveReport.WebAdmin.Global.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;

        private IDbTransaction _transaction;
        public IDao Dao { get; private set; }
        public IUserDao UserDao { get; private set; }
        public IDashboardDao DashboardDao { get; private set; }

        public UnitOfWork(IConfiguration config)
        {
            try
            {
                string connString = config["ConnectionStrings:DBConnectionString"];
                _connection = new SqlConnection(connString);

                Dao = new Dao(_connection);
                UserDao = new UserDao();
                DashboardDao = new DashboardDao(_connection);
            }
            catch
            {

            }
        }

        public void BeginTransaction()
        {
            try
            {
                _transaction = _connection.BeginTransaction();
            }
            catch
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }
                throw;
            }
        }

        public void CommitTransaction()
        {
            try
            {
                if (_transaction != null)
                {
                    _transaction.Commit();

                }
            }
            catch
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            #pragma warning disable CA1816 // Dispose methods should call SuppressFinalize
            GC.SuppressFinalize(true);  // Violates rule   
            #pragma warning restore CA1816 // Dispose methods should call SuppressFinalize
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_connection != null)
                {
                    _connection.Dispose();
                }
            }
        }
    }
}
