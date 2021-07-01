using DoseBookAdmin.ViewModels.ClassificationData;
using DoseBookAdmin.ViewModels.Global;

namespace DoseBookAdmin.Core.DomainServices
{
    public interface IMedicineDoseService
    {
        /// <summary>
        /// GetAllDoctors will return all Doctors
        /// </summary>
        /// <returns></returns>
        MedicineDoseListGridItemVM GetAllDoctors();

        /// <summary>
        /// GetMedicineDoseByDoctorWise will return MedicineDose By Doctor Wise
        /// </summary>
        /// <returns></returns>
        MedicineDoseListGridItemVM GetMedicineDoseByDoctorWise(int doctorId);

        ///<summary>
        /// Get MedicineDose Information
        /// </summary>
        /// <param name="doctor">MedicineDose data structure</param>
        /// <returns> </returns>
        MedicineDose GetMedicineDoseDataToCreateMedicineDose();

        ///<summary>
        /// Save MedicineDose Information
        /// </summary>
        /// <param name="doctor">MedicineDose data structure</param>
        /// <returns> </returns>
        BaseResult SaveMedicineDose(MedicineDose doctor);

        /// <summary>
        /// Update Doctor
        /// </summary>
        /// <param name="medicineDose">MedicineDoseVM data structure</param>
        /// <returns>true/false</returns>
        BaseResult UpdateMedicineDose(MedicineDose medicineDose);

        /// <summary>
        /// Delete doctors by ids
        /// </summary>
        /// <param name="targetIds">list of MedicineDose Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResult DeleteMedicineDoseByIds(DeleteItemVM targetIds);
        
        /// <summary>
        /// Get doctor by doctor Id
        /// </summary>
        /// <param name="id">Doctor Id</param>
        /// <returns>Info of Medicine using DoctorVM</returns>
        MedicineDose GetMedicineDoseById(int id);
        
    }
}
