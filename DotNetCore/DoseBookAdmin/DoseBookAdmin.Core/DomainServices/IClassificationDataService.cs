using DoseBookAdmin.ViewModels.ClassificationData;

namespace DoseBookAdmin.Core.DomainServices
{
    public interface IClassificationDataService
    {
        /// <summary>
        /// GetAllClassificationTypes will return all ClassificationTypes
        /// </summary>
        /// <returns></returns>
        ClassificationTypeListGridItemVM GetAllClassificationTypes();

        /// <summary>
        /// GetClassificationDataByClassificationTypeWise will return all AllClassificationDatas
        /// </summary>
        /// <returns></returns>
        ClassificationTypeListGridItemVM GetClassificationDataByClassificationTypeWise(int id);
    }
}
