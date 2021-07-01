using DoseBookAdmin.Core.DomainServices;
using DoseBookAdmin.Core.Repositories;
using DoseBookAdmin.ViewModels.DoseData;
using DoseBookAdmin.ViewModels.Global;
using System;
using System.Linq;

namespace DoseBookAdmin.Services
{
    public class DoseDataService : IDoseDataService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// DoctorService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public DoseDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public DoseMetaTypeListGridItemVM GetAllDoseMetaTypes()
        {
            try
            {
                var result = _unitOfWork.DoseDataRepo.GetAllDoseMetaTypes("usp_GetAllDoseMetaTypes");
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DoseMetaTypeListGridItemVM GetDoseDataByDoseMetaTypeWise(int doseMetaTypeId)
        {
            try
            {
                var result = _unitOfWork.DoseDataRepo.GetDoseDataByDoseMetaTypeWise("usp_GetDoseDataByDoseMetaTypeId", new { doseMetaTypeId = Convert.ToInt32(doseMetaTypeId) });
                DoseMetaType selectedDoseMetaTypeDetail = new DoseMetaType();
                selectedDoseMetaTypeDetail.Id = doseMetaTypeId;
                var result1 = _unitOfWork.DoseDataRepo.GetAllDoseMetaTypes("usp_GetAllDoseMetaTypes");
                if (result1.AllDoseMetaTypes.Count > 0)
                {
                    selectedDoseMetaTypeDetail.Type = result1.AllDoseMetaTypes.Where(doseMetaType => doseMetaType.Id == doseMetaTypeId).FirstOrDefault().Type;
                    result.SelectedDoseMetaType = selectedDoseMetaTypeDetail;
                }

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DoseData GetDoseDataToCreateDoseData()
        {
            try
            {
                DoseData doseData = _unitOfWork.DoseDataRepo.GetDoseDataToCreateDoseData("usp_GetPreRequisitesDoseDataToCreateDoseData");

                return doseData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult SaveDoseData(DoseData doseData)
        {
            try
            {
                var result = _unitOfWork.DoseDataRepo.SaveDoseData(doseData);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DoseData GetDoseDataById(int id)
        {
            try
            {
                DoseData doseData = _unitOfWork.DoseDataRepo.GetDoseDataById("usp_GetDoseDataById", new { Id = id });
                return doseData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult UpdateDoseData(DoseData doseData)
        {
            try
            {
                var result = _unitOfWork.DoseDataRepo.UpdateDoseData(doseData);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult DeleteDoseDataByIds(DeleteItemVM targetIds)
        {
            try
            {
                BaseResult result = _unitOfWork.DoseDataRepo.DeleteDoseDataByIds("usp_DeleteDoseDataByIds", targetIds);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
