using DoseBookAdmin.WebAdmin.Advice.Repository;
using DoseBookAdmin.WebAdmin.Doctor.Repository;
using DoseBookAdmin.WebAdmin.MedicineDose.Repository;
using DoseBookAdmin.WebAdmin.PrescriptionMeta.Repository;
using DoseBookAdmin.WebAdmin.Test.Repository;
using DoseBookAdmin.WebAdmin.User.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace DoseBookAdmin.WebAdmin.Common.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;
        public IDao Repo { get; private set; }
        public IUserDao UserRepo { get; private set; }
        public IDoctorDao DoctorRepo { get; private set; }
        public IAdviceDao AdviceRepo { get; private set; }
        public ITestDao TestRepo { get; set; }
        public IPrescriptionMetaDao PrescriptionMetaRepo { get; private set; }
        public IMedicineDoseDao MedicineDoseRepo { get; private set; }

        public UnitOfWork()
        {
            //IConfigurationRoot config, ILogger<UnitOfWork> logger
            try
            {
                //string connString = config["ConnectionStrings:DBConnectionString"];
                //string connString = "Persist Security Info=False;server=103.21.58.240;user=veterzkp_dbuser;password=Dosebook_produser;database=veterzkp_dosebook;";
                string connString = "server=localhost;user=root;password=1234;database=dosebook_admin;";
                _connection = new MySqlConnection(connString);

                Repo = new Dao(_connection);
                UserRepo = new UserDao(_connection);
                DoctorRepo = new DoctorDao(_connection);
                AdviceRepo = new AdviceDao(_connection);
                TestRepo = new TestDao(_connection);
                PrescriptionMetaRepo = new PrescriptionMetaDao(_connection);
                MedicineDoseRepo = new MedicineDoseDao(_connection);
            }
            catch (Exception ex)
            {
                //logger.LogError("UnitOfWork error :" + ex + "  $$$$$");
            }
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
