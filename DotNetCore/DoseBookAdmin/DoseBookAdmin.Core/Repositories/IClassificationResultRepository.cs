using DoseBookAdmin.ViewModels.ClassificationData;
using DoseBookAdmin.ViewModels.Global;

namespace DoseBookAdmin.Core.Repositories
{
    public interface IClassificationResultRepository
    {
        ClassificationResultListGridItemVM GetClassificationResultByDoctorWise(string query, object param);
        
        ClassificationResult GetClassificationResultDataToCreateClassificationResult(string query);
       
        BaseResult SaveClassificationResult(string query, object param);

        ClassificationResult GetClassificationResultById(string query, object param);

        BaseResult UpdateClassificationResult(string query, object param);

        BaseResult DeleteClassificationResultByIds(string query, DeleteItemVM targetIds);
    }
}
