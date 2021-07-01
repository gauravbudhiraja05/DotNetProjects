using DoseBookAdmin.Entity.Doctor;
using DoseBookAdmin.Entity.Global;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.Doctor.Service
{
    public interface IDoctorService
    {
        /// <summary>
        /// GetAllDoctors will return Advice List
        /// </summary>
        /// <returns></returns>
        DoctorEntityList GetAllDoctors();

        /// <summary>
        /// GetSearchedDoctorNameList will return the list of doctor.
        /// </summary>
        /// <returns></returns>
        List<string> GetSearchedDoctorNameList(string prefix);

        /// <summary>
        /// Check DoctorName already exist or not
        /// </summary>
        /// <returns>true/false</returns>
        bool IsDoctorNameExist(string doctorName, int id);

        /// <summary>
        /// SaveDoctorList will save List of Doctor Object
        /// </summary>
        /// <returns></returns>
        BaseResultEntity SaveDoctorList(DoctorEntityList doctorEntityList);

        /// <summary>
        /// UpdateDoctor will Update Doctor Object
        /// </summary>
        /// <returns></returns>
        BaseResultEntity UpdateDoctor(DoctorEntity doctorEntity);

        /// <summary>
        /// Delete Doctor by ids
        /// </summary>
        /// <param name="deleteItemEntity">list of Doctor Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResultEntity DeleteDoctorByIds(DeleteItemEntity deleteItemEntity);
    }
}
