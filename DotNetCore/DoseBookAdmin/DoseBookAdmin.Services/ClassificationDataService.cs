using DoseBookAdmin.Core.DomainServices;
using DoseBookAdmin.Core.Repositories;
using DoseBookAdmin.ViewModels.ClassificationData;
using System;
using System.Linq;

namespace DoseBookAdmin.Services
{
    public class ClassificationDataService : IClassificationDataService
    {
        /// <summary>
        /// IUnitOfWork Data Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// ClassificationDataService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public ClassificationDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ClassificationTypeListGridItemVM GetAllClassificationTypes()
        {
            try
            {
                var result = _unitOfWork.ClassificationDataRepo.GetAllClassificationTypes("usp_GetAllClassificationTypes");
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ClassificationTypeListGridItemVM GetClassificationDataByClassificationTypeWise(int classificationTypeId)
        {
            try
            {
                var result = _unitOfWork.ClassificationDataRepo.GetClassificationDataByClassificationTypeWise("usp_GetClassificationDataByClassificationTypeId", new { ClassificationTypeId = Convert.ToInt32(classificationTypeId) }, classificationTypeId);
                ClassificationType selectedClassificationTypeDetail = new ClassificationType();
                selectedClassificationTypeDetail.Id = classificationTypeId;
                var result1 = _unitOfWork.DoseDataRepo.GetAllDoseMetaTypes("usp_GetAllClassificationTypes");
                if (result1.AllDoseMetaTypes.Count > 0)
                {
                    selectedClassificationTypeDetail.Type = result1.AllDoseMetaTypes.Where(classificationType => classificationType.Id == classificationTypeId).FirstOrDefault().Type;
                    result.SelectedClassificationType = selectedClassificationTypeDetail;
                }
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
