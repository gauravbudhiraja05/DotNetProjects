using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PickfordsIntranet.Core.Repositories;
using PickfordsIntranet.Core.Repositories.FrontEnd;
using PickfordsIntranet.Repo.FrontEnd;

namespace PickfordsIntranet.Repo
{
    /// <summary>
    /// UnitOfWork implemented IUnitOfWork to maintain the consistency in data source
    /// </summary>

    /// <summary>
    /// UnitOfWork implemented IUnitOfWork to maintain the consistency in data source
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;
        public IRepository Repo { get; private set; }

        public IAdminRepository AdminRepo { get; private set; }
        public INewsRepository NewsRepo { get; private set; }
        public IFaqRepository FaqRepo { get; private set; }
        public IVacancyRepository VacancyRepo { get; private set; }
        public IDocumentRepository DocuRepo { get; private set; }
        public IFrontEndRepository FrontEndRepo { get; private set; }
        public IEndUserRepository EndUserRepo { get; private set; }
        public IDepartmentRepository DeptRepo { get; private set; }
        public IRewardRepository RewRepo { get; private set; }
        public ILeaveManagementRepository LevRepo { get; private set; }

        public UnitOfWork(IConfigurationRoot config)
        {
            string connString = config["ConnectionStrings:DBConnectionString"];
            _connection = new SqlConnection(connString);
            Repo = new Repository(_connection);
            AdminRepo = new AdminRepository(_connection);
            NewsRepo = new NewsRepository(_connection);
            FaqRepo = new FaqRepository(_connection);
            VacancyRepo = new VacancyRepository(_connection);
            DocuRepo = new DocumentRepository(_connection);
            FrontEndRepo = new FrontEndRepository(_connection);
            EndUserRepo = new EndUserRepository(_connection);
            DeptRepo = new DepartmentRepository(_connection);
            RewRepo = new RewardRepository(_connection);
            LevRepo = new LeaveManagementRepository(_connection);
        }

        public void BeginTransaction()
        {
            try
            {
               _transaction= _connection.BeginTransaction();
            }
            catch (Exception)
            {
                if(_transaction!=null)
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
                if(_transaction!=null)
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

