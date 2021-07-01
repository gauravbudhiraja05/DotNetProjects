using DoseBookAdmin.Common.Utility;
using DoseBookAdmin.Entity.Global;
using DoseBookAdmin.Entity.MedicineDose;
using DoseBookAdmin.WebAdmin.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoseBookAdmin.WebAdmin.MedicineDose.Service
{
    public class MedicineDoseService : IMedicineDoseService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// MedicineDoseService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public MedicineDoseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public MedicineDoseEntityList GetAllMedicineDoses()
        {
            return _unitOfWork.MedicineDoseRepo.GetMedicineDoseByDoctorWise("usp_GetMedicineDoseListByDoctorId", new { In_doctorId = 0, IN_type = DataType.Master });
        }

        public MedicineDoseEntityList GetMedicineDoseListByDoctorWise(int doctorId)
        {
            return _unitOfWork.MedicineDoseRepo.GetMedicineDoseByDoctorWise("usp_GetMedicineDoseListByDoctorId", new { In_doctorId = Convert.ToInt32(doctorId), IN_type = DataType.Doctor });
        }

        public MedicineDoseSimulationEntity GetMedicineDoseDataToCreateMasterMedicineDose()
        {
            return _unitOfWork.MedicineDoseRepo.GetMedicineDoseDataToCreateMedicineDose("usp_GetMedicineDoseDataToCreateMedicineDose");
        }

        public List<string> GetSearchedMedicineDoseList(string prefix)
        {
            return _unitOfWork.MedicineDoseRepo.GetSearchedMedicineDoseList("usp_GetSearchedMedicineDoseList", new { IN_prefix = prefix });
        }

        public bool IsMedicineExist(string medicineName, int doctorId, int medicineId)
        {
            return _unitOfWork.MedicineDoseRepo.IsMedicineExist("usp_IsMedicineExist", new { IN_medicineName = medicineName, In_doctorId = doctorId, IN_id = medicineId });
        }

        public MedicineDoseEntity GetMedicineDetailByMedicineName(string medicineName, string type)
        {
            return _unitOfWork.MedicineDoseRepo.GetMedicineDetailByMedicineName("usp_GetMedicineDetailByMedicineName", new { IN_medicineName = medicineName, IN_type = type });
        }

        public BaseResultEntity SaveMasterMedicineDose(MedicineDoseEntity medicineDoseEntity)
        {
            return _unitOfWork.MedicineDoseRepo.SaveMedicineDose("usp_SaveMedicineDose", new { IN_medicineName = medicineDoseEntity.MedicineName, IN_doctorId = medicineDoseEntity.DoctorId, IN_frequency = medicineDoseEntity.Frequency, IN_directions = medicineDoseEntity.Directions, IN_composition = medicineDoseEntity.Composition, IN_duration = medicineDoseEntity.Duration, IN_dose = medicineDoseEntity.Dose, IN_problemTags = medicineDoseEntity.ProblemTags, IN_doseUnit = medicineDoseEntity.DoseUnit, IN_type = DataType.Master });
        }

        public MedicineDoseSimulationEntity GetMasterMedicineDoseById(int medicineId)
        {
            return _unitOfWork.MedicineDoseRepo.GetMedicineDoseById("usp_GetMedicineDoseById", new { IN_medicineid = medicineId, IN_type = DataType.Master });
        }

        public BaseResultEntity UpdateMasterMedicineDose(MedicineDoseEntity medicineDoseEntity)
        {
            return _unitOfWork.MedicineDoseRepo.UpdateMedicineDose("usp_UpdateMedicineDose", new { IN_medicineId = medicineDoseEntity.MedicineId, IN_medicineName = medicineDoseEntity.MedicineName, IN_doctorId = medicineDoseEntity.DoctorId, IN_frequency = medicineDoseEntity.Frequency, IN_directions = medicineDoseEntity.Directions, IN_composition = medicineDoseEntity.Composition, IN_duration = medicineDoseEntity.Duration, IN_dose = medicineDoseEntity.Dose, IN_dose_unit = medicineDoseEntity.DoseUnit, IN_problemTags = medicineDoseEntity.ProblemTags, IN_type = DataType.Master });
        }

        public BaseResultEntity DeleteMasterMedicineDoseByIds(DeleteItemEntity deleteItemEntity)
        {
            BaseResultEntity baseResultEntity = new BaseResultEntity();
            deleteItemEntity.ItemIds.ForEach(itemId =>
            {
                baseResultEntity = _unitOfWork.MedicineDoseRepo.DeleteMedicineDoseById("usp_DeleteMedicineDoseById", new { In_medicineId = itemId, In_type = DataType.Master });
            });

            return baseResultEntity;
        }

        public string GetSearchedDoctorProblemTagsList()
        {
            return _unitOfWork.MedicineDoseRepo.GetSearchedDoctorProblemTagsList("usp_GetMedicineDoseSearchedDoctorProblemTagsList");
        }

        public MedicineDoseSimulationEntity GetMedicineDoseDataToCreateDoctorMedicineDose()
        {
            return _unitOfWork.MedicineDoseRepo.GetMedicineDoseDataToCreateMedicineDose("usp_GetMedicineDoseDataToCreateMedicineDose");
        }

        public BaseResultEntity SaveDoctorMedicineDose(MedicineDoseEntity medicineDoseEntity)
        {
            return _unitOfWork.MedicineDoseRepo.SaveMedicineDose("usp_SaveMedicineDose", new { IN_medicineName = medicineDoseEntity.MedicineName, IN_doctorId = medicineDoseEntity.DoctorId, IN_frequency = medicineDoseEntity.Frequency, IN_directions = medicineDoseEntity.Directions, IN_composition = medicineDoseEntity.Composition, IN_duration = medicineDoseEntity.Duration, IN_dose = medicineDoseEntity.Dose, IN_problemTags = medicineDoseEntity.ProblemTags, IN_doseUnit = medicineDoseEntity.DoseUnit, IN_type = DataType.Doctor });
        }

        public MedicineDoseSimulationEntity GetDoctorMedicineDoseById(int medicineId)
        {
            return _unitOfWork.MedicineDoseRepo.GetMedicineDoseById("usp_GetMedicineDoseById", new { IN_medicineid = medicineId, IN_type = DataType.Doctor });
        }

        public BaseResultEntity UpdateDoctorMedicineDose(MedicineDoseEntity medicineDoseEntity)
        {
            return _unitOfWork.MedicineDoseRepo.UpdateMedicineDose("usp_UpdateMedicineDose", new { IN_medicineId = medicineDoseEntity.MedicineId, IN_medicineName = medicineDoseEntity.MedicineName, IN_doctorId = medicineDoseEntity.DoctorId, IN_frequency = medicineDoseEntity.Frequency, IN_directions = medicineDoseEntity.Directions, IN_composition = medicineDoseEntity.Composition, IN_duration = medicineDoseEntity.Duration, IN_dose = medicineDoseEntity.Dose, IN_dose_unit = medicineDoseEntity.DoseUnit, IN_problemTags = medicineDoseEntity.ProblemTags, IN_type = DataType.Doctor });
        }

        public BaseResultEntity DeleteDoctorMedicineDoseByIds(DeleteItemEntity deleteItemEntity)
        {
            BaseResultEntity baseResultEntity = new BaseResultEntity();
            deleteItemEntity.ItemIds.ForEach(itemId =>
            {
                baseResultEntity = _unitOfWork.MedicineDoseRepo.DeleteMedicineDoseById("usp_DeleteMedicineDoseById", new { In_medicineId = itemId, In_type = DataType.Doctor });
            });

            return baseResultEntity;
        }
    }
}
