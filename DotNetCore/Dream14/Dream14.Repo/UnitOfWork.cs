using Dream14.Core.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Dream14.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;

        public IRepository Repo { get; private set; }
        public IAdminRepository AdminRepo { get; private set; }

        public UnitOfWork(IConfigurationRoot config)
        {
            string connString = config["ConnectionStrings:DBConnectionString"];
            _connection = new SqlConnection(connString);
            Repo = new Repository(_connection);
            AdminRepo = new AdminRepository(_connection);
        }

        public void BeginTransaction()
        {
            try
            {
                _transaction = _connection.BeginTransaction();
            }
            catch (Exception)
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
            catch (Exception)
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
            //_transaction.Dispose();
            //_connection.Dispose();
        }
    }
}
