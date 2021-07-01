using DoseBookAdmin.Common.Utility;
using DoseBookAdmin.Entity.Advice;
using DoseBookAdmin.Entity.Global;
using DoseBookAdmin.WebAdmin.Common.Repository;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.Advice.Service
{
    public class AdviceService : IAdviceService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// AdviceService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public AdviceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public AdviceEntityList GetAllAdvices()
        {
            return _unitOfWork.AdviceRepo.GetAdviceListByDoctorWise("usp_GetAdviceListByDoctorId", new { In_doctorId = 0, IN_type = DataType.Master });
        }

        public AdviceEntityList GetAdviceListByDoctorWise(int doctorId)
        {
            return _unitOfWork.AdviceRepo.GetAdviceListByDoctorWise("usp_GetAdviceListByDoctorId", new { In_doctorId = doctorId, IN_type = DataType.Doctor });
        }

        public string GetSearchedDoctorProblemTagsList()
        {
            return _unitOfWork.AdviceRepo.GetSearchedDoctorProblemTagsList("usp_GetAdviceSearchedDoctorProblemTagsList");
        }

        public List<string> GetSearchedAdviceList(string prefix)
        {
            return _unitOfWork.AdviceRepo.GetSearchedAdviceList("usp_GetSearchedAdviceList", new { IN_prefix = prefix });
        }

        public bool IsAdviceExist(string description, int doctorId, int id)
        {
            return _unitOfWork.AdviceRepo.IsAdviceExist("usp_IsAdviceExist", new { IN_description = description, In_doctorId = doctorId, In_Id = id });
        }

        public BaseResultEntity SaveDoctorAdviceList(AdviceEntityList adviceEntityList)
        {
            BaseResultEntity baseResultEntity = new BaseResultEntity();
            adviceEntityList.ForEach(adviceEntity =>
            {
                baseResultEntity = _unitOfWork.AdviceRepo.SaveAdvice("usp_SaveAdvice", new { IN_Id = adviceEntity.Id, IN_description = adviceEntity.Description, IN_doctorId = adviceEntity.DoctorId, IN_problemTags = adviceEntity.ProblemTags, IN_type = DataType.Doctor });
            });

            return baseResultEntity;
        }

        public BaseResultEntity SaveMasterAdviceList(AdviceEntityList adviceEntityList)
        {
            BaseResultEntity baseResultEntity = new BaseResultEntity();
            adviceEntityList.ForEach(adviceEntity =>
            {
                baseResultEntity = _unitOfWork.AdviceRepo.SaveAdvice("usp_SaveAdvice", new { IN_Id = adviceEntity.Id, IN_description = adviceEntity.Description, IN_doctorId = adviceEntity.DoctorId, IN_problemTags = adviceEntity.ProblemTags, IN_type = DataType.Master });
            });

            return baseResultEntity;
        }

        public BaseResultEntity UpdateDoctorAdvice(AdviceEntity adviceEntity)
        {
            return _unitOfWork.AdviceRepo.UpdateAdvice("usp_UpdateAdvice", new { IN_Id = adviceEntity.Id, IN_description = adviceEntity.Description, IN_doctorId = adviceEntity.DoctorId, IN_problemTags = adviceEntity.ProblemTags, IN_type = DataType.Doctor });
        }

        public BaseResultEntity UpdateMasterAdvice(AdviceEntity adviceEntity)
        {
            return _unitOfWork.AdviceRepo.UpdateAdvice("usp_UpdateAdvice", new { IN_Id = adviceEntity.Id, IN_description = adviceEntity.Description, IN_doctorId = adviceEntity.DoctorId, IN_problemTags = adviceEntity.ProblemTags, IN_type = DataType.Master });
        }

        public BaseResultEntity DeleteMasterAdviceByIds(DeleteItemEntity deleteItemEntity)
        {
            BaseResultEntity baseResultEntity = new BaseResultEntity();
            deleteItemEntity.ItemIds.ForEach(itemId =>
            {
                baseResultEntity = _unitOfWork.AdviceRepo.DeleteAdviceById("usp_DeleteAdviceById", new { In_Id = itemId, IN_Type = DataType.Master });
            });

            return baseResultEntity;
        }

        public BaseResultEntity DeleteDoctorAdviceByIds(DeleteItemEntity deleteItemEntity)
        {
            BaseResultEntity baseResultEntity = new BaseResultEntity();
            deleteItemEntity.ItemIds.ForEach(itemId =>
            {
                baseResultEntity = _unitOfWork.AdviceRepo.DeleteAdviceById("usp_DeleteAdviceById", new { In_Id = itemId, IN_Type = DataType.Doctor });
            });

            return baseResultEntity;
        }
    }
}
