using DoseBookAdmin.ViewModels.ClassificationData;
using DoseBookAdmin.ViewModels.Global;

namespace DoseBookAdmin.Core.Repositories
{
    public interface IDiagnosticTestRepository
    {
        DiagnosticTestListGridItemVM GetDiagnosticTestByDoctorWise(string query, object param);

        DiagnosticTest GetDiagnosticTestDataToCreateDiagnosticTest(string query);

        BaseResult DeleteDiagnosticTestByIds(string query, DeleteItemVM targetIds);

        DiagnosticTest GetDiagnosticTestById(string query, object param);

        BaseResult SaveDiagnosticTest(string query, object param);

        BaseResult UpdateDiagnosticTest(string query, object param);

    }
}
