using PickfordsIntranet.Core.Repositories.FrontEnd;
using System;

namespace PickfordsIntranet.Core.Repositories
{
    /// <summary>
    /// Its decribes the unit of work that will help to mantain consitency.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IRepository Repo { get; }
        IAdminRepository AdminRepo { get; }
        INewsRepository NewsRepo { get; }
        IFaqRepository FaqRepo { get; }
        IVacancyRepository VacancyRepo { get; }
        IDocumentRepository DocuRepo { get; }
        IFrontEndRepository FrontEndRepo { get; }
        IEndUserRepository EndUserRepo { get; }
        IDepartmentRepository DeptRepo { get; }
        IRewardRepository RewRepo { get; }
        ILeaveManagementRepository LevRepo { get; }
        void BeginTransaction();
        void CommitTransaction();
    }
}
