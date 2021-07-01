using System;

namespace DoseBookAdmin.Core.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository Repo { get; }
        IDoctorRepository DoctorRepo { get; }
        IUserRepository UserRepo { get; }
        IMedicineDoseRepository MedicineDoseRepo { get; }
        IDiagnosticTestRepository DiagnosticTestRepo { get; }
        IMiscSuggestionRepository MiscSuggestionRepo { get; }
        IDoseDataRepository DoseDataRepo { get; }
        IClassificationResultRepository ClassificationResultRepo { get; }
        IClassificationDataRepository ClassificationDataRepo { get; }
        IDocumentRepository DocumentRepo { get; }
        void BeginTransaction();
        void CommitTransaction();        
    }
}
