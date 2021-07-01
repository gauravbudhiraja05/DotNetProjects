using DoseBookAdmin.Dto.Global;
using DoseBookAdmin.Dto.MedicineDose;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.MedicineDose.Af
{
    public interface IMedicineDoseAf
    {
        /// <summary>
        /// GetAllMedicineDoses will return MedicineDose List
        /// </summary>
        /// <returns></returns>
        MedicineDoseDtoList GetAllMedicineDoses();

        /// <summary>
        /// GetMedicineDoseByDoctorWise will return MedicineDose By Doctor Wise
        /// </summary>
        /// <returns></returns>
        MedicineDoseListGridItemDto GetMedicineDoseListByDoctorWise(int id);

        ///<summary>
        /// Get MedicineDose Information
        /// </summary>
        /// <param name="doctor">MedicineDose data structure</param>
        /// <returns> </returns>
        MedicineDoseSimulationDto GetMedicineDoseDataToCreateMasterMedicineDose();

        /// <summary>
        /// GetSearchedMedicineDoseList will return the list of medicine doses.
        /// </summary>
        /// <returns></returns>
        List<string> GetSearchedMedicineDoseList(string prefix);

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
        MedicineDoseDto GetMedicineDetailByMedicineName(string medicineName, string type);

        ///<summary>
        /// Save MasterMedicineDose will save Master MedicineDose Object
        /// </summary>
        /// <param name="doctor">MedicineDose data structure</param>
        /// <returns> </returns>
        BaseResultDto SaveMasterMedicineDose(MedicineDoseDto masterMedicineDose);

        /// <summary>
        /// Get Master MedicineDose by Id
        /// </summary>
        /// <param name="id">medicine Id</param>
        /// <returns>Info of Medicine using MedicineDoseVM</returns>
        MedicineDoseSimulationDto GetMasterMedicineDoseById(int medicineId);

        /// <summary>
        /// Update Master MedicineDose
        /// </summary>
        /// <param name="medicineDose">MedicineDoseVM data structure</param>
        /// <returns>true/false</returns>
        BaseResultDto UpdateMasterMedicineDose(MedicineDoseDto masterMedicineDose);

        /// <summary>
        /// Delete MasterMedicineDose by ids
        /// </summary>
        /// <param name="targetIds">list of Master Advice Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResultDto DeleteMasterMedicineDoseByIds(DeleteItemDto deleteItemDto);

        // <summary>
        /// GetSearchedAdviceList will return the string of ProblemTags.
        /// </summary>
        /// <returns></returns>
        string GetSearchedDoctorProblemTagsList();

        ///<summary>
        /// Get MedicineDose Information
        /// </summary>
        /// <returns> </returns>
        MedicineDoseSimulationDto GetMedicineDoseDataToCreateDoctorMedicineDose();

        /// <summary>
        /// SaveDoctorMedicineDose will save Doctor MedicineDose Object
        /// </summary>
        /// <param name="doctorMedicineDose">MedicineDoseDto</param>
        /// <returns></returns>
        BaseResultDto SaveDoctorMedicineDose(MedicineDoseDto doctorMedicineDose);

        /// <summary>
        /// Get Doctor MedicineDose by Id
        /// </summary>
        /// <param name="id">medicine Id</param>
        /// <returns>Info of Medicine using MedicineDoseVM</returns>
        MedicineDoseSimulationDto GetDoctorMedicineDoseById(int medicineId);

        /// <summary>
        /// Update Doctor MedicineDose
        /// </summary>
        /// <param name="medicineDose">MedicineDoseVM data structure</param>
        /// <returns>true/false</returns>
        BaseResultDto UpdateDoctorMedicineDose(MedicineDoseDto doctorMedicineDose);

        /// <summary>
        /// Delete DoctorAdvice by ids
        /// </summary>
        /// <param name="deleteItemDto">list of Doctor Advice Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResultDto DeleteDoctorMedicineDoseByIds(DeleteItemDto deleteItemDto);
    }
}
