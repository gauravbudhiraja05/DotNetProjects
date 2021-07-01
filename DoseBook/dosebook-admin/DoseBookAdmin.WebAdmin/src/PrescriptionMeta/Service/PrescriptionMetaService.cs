using DoseBookAdmin.Entity.Global;
using DoseBookAdmin.Entity.PrescriptionMeta;
using DoseBookAdmin.WebAdmin.Common.Repository;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.PrescriptionMeta.Service
{
    public class PrescriptionMetaService : IPrescriptionMetaService
    {
        /// <summary>
        /// Private IUnitOfWork Meta Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// DoctorService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public PrescriptionMetaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PrescriptionMetaTypeEntityList GetAllPrescriptionMetaTypes()
        {
            return _unitOfWork.PrescriptionMetaRepo.GetAllPrescriptionMetaTypes("usp_GetAllPrescriptionMetaTypes");
        }

        public PrescriptionMetaDataEntityList GetPrescriptionMetaDataByPrescriptionMetaTypeWise(string prescriptionMetaType)
        {
            return _unitOfWork.PrescriptionMetaRepo.GetPrescriptionMetaDataByPrescriptionMetaTypeWise("usp_GetPrescriptionMetaDataByPrescriptionMetaTypeId", new { IN_prescriptionMetaType = prescriptionMetaType.Trim() });
        }

        public BaseResultEntity SavePrescriptionMetaDataList(PrescriptionMetaDataEntityList prescriptionMetaDataEntityList)
        {
            BaseResultEntity baseResultEntity = new BaseResultEntity();
            prescriptionMetaDataEntityList.ForEach(prescriptionMetaDataEntity =>
            {
                baseResultEntity = _unitOfWork.PrescriptionMetaRepo.SavePrescriptionMetaData("usp_SavePrescriptionMetaData", new { IN_type = prescriptionMetaDataEntity.Type, IN_value = prescriptionMetaDataEntity.Value, IN_displayOrder = prescriptionMetaDataEntity.DisplayOrderNumber });
            });

            return baseResultEntity;
        }

        public BaseResultEntity UpdatePrescriptionMetaData(PrescriptionMetaDataEntity prescriptionMetaDataEntity)
        {
            return _unitOfWork.PrescriptionMetaRepo.UpdatePrescriptionMetaData("usp_UpdatePrescriptionMetaData", new { IN_id = prescriptionMetaDataEntity.Id, IN_prescriptionMetaType = prescriptionMetaDataEntity.Type, IN_name = prescriptionMetaDataEntity.Value, IN_displayOrder = prescriptionMetaDataEntity.DisplayOrderNumber });
        }

        public BaseResultEntity DeletePrescriptionMetaDataByIds(DeleteItemEntity deleteItemEntity)
        {
            BaseResultEntity baseResultEntity = new BaseResultEntity();
            deleteItemEntity.ItemIds.ForEach(itemId =>
            {
                baseResultEntity = _unitOfWork.PrescriptionMetaRepo.DeletePrescriptionMetaDataById("DeletePrescriptionMetaDataById", new { IN_id = itemId });
            });

            return baseResultEntity;
        }

        public List<string> GetSearchedTypeList(string prefix)
        {
            return _unitOfWork.PrescriptionMetaRepo.GetSearchedTypeList("usp_GetSearchedTypeList", new
            {
                IN_prefix = prefix
            });
        }

        public bool IsTypeExist(string type, int id)
        {
            return _unitOfWork.PrescriptionMetaRepo.IsTypeExist("usp_IsTypeExist", new { IN_type = type, In_Id = id });
        }

        public BaseResultEntity SavePrescriptionMetaTypeList(PrescriptionMetaTypeEntityList prescriptionMetaTypeEntityList)
        {
            BaseResultEntity baseResultEntity = new BaseResultEntity();

            prescriptionMetaTypeEntityList.ForEach(prescriptionMetaTypeEntity =>
            {
                baseResultEntity = _unitOfWork.PrescriptionMetaRepo.SavePrescriptionMetaType("usp_SavePrescriptionMetaType", new
                {
                    IN_Id = prescriptionMetaTypeEntity.Id,
                    IN_type = prescriptionMetaTypeEntity.Type
                });
            });

            return baseResultEntity;

        }

        public BaseResultEntity UpdatePrescriptionMetaType(PrescriptionMetaTypeEntity prescriptionMetaTypeEntity)
        {
            return _unitOfWork.PrescriptionMetaRepo.UpdatePrescriptionMetaType("usp_UpdatePrescriptionMetaType", new { IN_Id = prescriptionMetaTypeEntity.Id, IN_type = prescriptionMetaTypeEntity.Type });
        }

        public BaseResultEntity DeletePrescriptionMetaTypeByIds(DeleteItemEntity deleteItemEntity)
        {
            BaseResultEntity baseResultEntity = new BaseResultEntity();
            deleteItemEntity.ItemIds.ForEach(itemId =>
            {
                baseResultEntity = _unitOfWork.PrescriptionMetaRepo.DeletePrescriptionMetaTypeById("usp_DeletePrescriptionMetaTypeById", new { In_Id = itemId });
            });

            return baseResultEntity;
        }

    }
}
