using DoseBookAdmin.ViewModels.Doctors;
using DoseBookAdmin.ViewModels.Global;
using System.Collections.Generic;

namespace DoseBookAdmin.Core.DomainServices
{
    public interface IDoctorService
    {
        /// <summary>
        /// GetAllDoctors will return all Doctors
        /// </summary>
        /// <returns></returns>
        IEnumerable<Doctor> GetAllDoctors();

        ///<summary>
        /// Save Doctor Information
        /// </summary>
        /// <param name="doctor">Doctor data structure</param>
        /// <returns> </returns>
        BaseResult SaveDoctor(Doctor doctor);

        /// <summary>
        /// Update Doctor
        /// </summary>
        /// <param name="user">DoctorVM data structure</param>
        /// <returns>true/false</returns>
        BaseResult UpdateDoctor(Doctor doctor);

        /// <summary>
        /// Delete doctors by ids
        /// </summary>
        /// <param name="targetIds">list of doctor Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResult DeleteDoctorsByIds(DeleteItemVM targetIds);

        /// <summary>
        /// Get doctor by doctor Id
        /// </summary>
        /// <param name="id">Doctor Id</param>
        /// <returns>Info of Doctor using DoctorVM</returns>
        Doctor GetDoctorById(int id);
    }
}
