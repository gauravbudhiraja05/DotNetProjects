using DoseBookAdmin.ViewModels.ClassificationData;

namespace DoseBookAdmin.Core.Repositories
{
    public interface IClassificationDataRepository
    {
        ClassificationTypeListGridItemVM GetAllClassificationTypes(string query);
        ClassificationTypeListGridItemVM GetClassificationDataByClassificationTypeWise(string query, object param, int classificationTypeId);
    }
}
