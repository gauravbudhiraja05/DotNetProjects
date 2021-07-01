using DoseBookAdmin.WebAdmin.Advice.Repository;
using DoseBookAdmin.WebAdmin.Doctor.Repository;
using DoseBookAdmin.WebAdmin.MedicineDose.Repository;
using DoseBookAdmin.WebAdmin.PrescriptionMeta.Repository;
using DoseBookAdmin.WebAdmin.Test.Repository;
using DoseBookAdmin.WebAdmin.User.Repository;
using System;

namespace DoseBookAdmin.WebAdmin.Common.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IDao Repo { get; }
        IUserDao UserRepo { get; }
        IDoctorDao DoctorRepo { get; }
        IAdviceDao AdviceRepo { get; }
        ITestDao TestRepo { get; }
        IPrescriptionMetaDao PrescriptionMetaRepo { get; }
        IMedicineDoseDao MedicineDoseRepo { get; }
        
        void BeginTransaction();
        void CommitTransaction();
    }
}
