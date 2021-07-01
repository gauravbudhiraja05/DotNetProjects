using DoseBookAdmin.Core.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DoseBookAdmin.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;
        public IRepository Repo { get; private set; }

        public IDoctorRepository DoctorRepo { get; private set; }
        public IUserRepository UserRepo { get; private set; }
        public IMedicineDoseRepository MedicineDoseRepo { get; private set; }
        public IDiagnosticTestRepository DiagnosticTestRepo { get; private set; }
        public IMiscSuggestionRepository MiscSuggestionRepo { get; private set; }
        public IDoseDataRepository DoseDataRepo { get; private set; }
        public IClassificationResultRepository ClassificationResultRepo { get; private set; }
        public IClassificationDataRepository ClassificationDataRepo { get; private set; }
        public IDocumentRepository DocumentRepo { get; private set; }

        public UnitOfWork(IConfigurationRoot config)
        {
            string connString = config["ConnectionStrings:DBConnectionString"];
            _connection = new SqlConnection(connString);
            Repo = new Repository(_connection);
            DoctorRepo = new DoctorRepository(_connection);
            UserRepo = new UserRepository(_connection);
            MedicineDoseRepo = new MedicineDoseRepository(_connection);
            DiagnosticTestRepo = new DiagnosticTestRepository(_connection);
            MiscSuggestionRepo = new MiscSuggestionRepository(_connection);
            DoseDataRepo = new DoseDataRepository(_connection);
            ClassificationDataRepo = new ClassificationDataRepository(_connection);
            ClassificationResultRepo = new ClassificationResultRepository(_connection);
            DocumentRepo = new DocumentRepository(_connection);
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
        }
    }
}
