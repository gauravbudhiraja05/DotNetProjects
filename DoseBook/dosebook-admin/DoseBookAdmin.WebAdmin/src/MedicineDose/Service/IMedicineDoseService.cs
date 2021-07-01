using DoseBookAdmin.Dto.Doctor;
using DoseBookAdmin.Dto.Global;
using DoseBookAdmin.Dto.MedicineDose;
using DoseBookAdmin.Entity.Global;
using DoseBookAdmin.Entity.MedicineDose;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.MedicineDose.Service
{
    public interface IMedicineDoseService
    {

        /// <summary>
        /// GetAllMedicineDoses will return MedicineDose List
        /// </summary>
        /// <returns></returns>
        MedicineDoseEntityList GetAllMedicineDoses();

        /// <summary>
        /// GetMedicineDoseByDoctorWise will return MedicineDose By Doctor Wise
        /// </summary>
        /// <returns></returns>
        MedicineDoseEntityList GetMedicineDoseListByDoctorWise(int doctorId);

        ///<summary>
        /// Get MedicineDose Information
        /// </summary>
        /// <returns> </returns>
        MedicineDoseSimulationEntity GetMedicineDoseDataToCreateDoctorMedicineDose();

        /// <summary>
        /// GetSearchedMedicineDoseList will return the list of medicine doses.
        /// </summary>
        /// <returns></returns>
        List<string> GetSearchedMedicineDoseList(string prefix);

        ///<summary>
        /// Get MedicineDose Information
        /// </summary>
        /// <param name="doctor">MedicineDose data structure</param>
        /// <returns> </returns>
        MedicineDoseSimulationEntity GetMedicineDoseDataToCreateMasterMedicineDose();

        /// <summary>
        /// Check Medicine already exist or not
        /// </summary>
        /// <param name="medicineName">MedicineName</param>
        /// <param name="medicineId">Medicine Id</param>
        /// <param name="doctorId">DoctorId Id</param>
        /// <returns>true/false</returns>
        bool IsMedicineExist(string medicineName, int doctorId, int medicineId);

        /// <summary>
        /// GetMedicineDetailByMedicineName will return MedicineDose Object
        /// </summary>
        /// <returns></returns>
        MedicineDoseEntity GetMedicineDetailByMedicineName(string medicineName, string type);

        ///<summary>
        /// Save MasterMedicineDose will save Master MedicineDose Object
        /// </summary>
        /// <param name="doctor">MedicineDose data structure</param>
        /// <returns> </returns>
        BaseResultEntity SaveMasterMedicineDose(MedicineDoseEntity medicineDose);

        /// <summary>
        /// Get Master MedicineDose by Id
        /// </summary>
        /// <param name="id">medicine Id</param>
        /// <returns>Info of Medicine using MedicineDoseVM</returns>
        MedicineDoseSimulationEntity GetMasterMedicineDoseById(int id);

        /// <summary>
        /// Update Master MedicineDose
        /// </summary>
        /// <param name="medicineDose">MedicineDoseVM data structure</param>
        /// <returns>true/false</returns>
        BaseResultEntity UpdateMasterMedicineDose(MedicineDoseEntity medicineDoseEntity);

        /// <summary>
        /// Delete MasterMedicineDose by ids
        /// </summary>
        /// <param name="targetIds">list of Master Advice Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResultEntity DeleteMasterMedicineDoseByIds(DeleteItemEntity deleteItemEntity);

        // <summary>
        /// GetSearchedAdviceList will return the string of ProblemTags.
        /// </summary>
        /// <returns></returns>
        string GetSearchedDoctorProblemTagsList();

        /// <summary>
        /// SaveDoctorMedicineDose will save Doctor MedicineDose Object
        /// </summary>
        /// <returns></returns>
        BaseResultEntity SaveDoctorMedicineDose(MedicineDoseEntity medicineDoseEntity);

        /// <summary>
        /// Get Doctor MedicineDose by Id
        /// </summary>
        /// <param name="id">medicine Id</param>
        /// <returns>Info of Medicine using MedicineDoseVM</returns>
        MedicineDoseSimulationEntity GetDoctorMedicineDoseById(int id);

        /// <summary>
        /// Update Doctor MedicineDose
        /// </summary>
        /// <param name="medicineDose">MedicineDoseVM data structure</param>
        /// <returns>true/false</returns>
        BaseResultEntity UpdateDoctorMedicineDose(MedicineDoseEntity medicineDoseEntity);

        /// <summary>
        /// Delete DoctorAdvice by ids
        /// </summary>
        /// <param name="deleteItemEntity">list of Medicine Dose Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResultEntity DeleteDoctorMedicineDoseByIds(DeleteItemEntity deleteItemEntity);
    }
}
