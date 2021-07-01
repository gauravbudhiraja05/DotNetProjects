using DoseBookAdmin.Dto.Doctor;
using DoseBookAdmin.Dto.Global;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.Doctor.Af
{
    public interface IDoctorAf
    {
        /// <summary>
        /// GetAllDoctors will return all Doctors
        /// </summary>
        /// <returns></returns>
        DoctorDtoList GetAllDoctors();

        /// <summary>
        /// GetSearchedDoctorNameList will return all Doctors
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
        BaseResultDto SaveDoctorList(List<DoctorDto> doctorList);

        /// <summary>
        /// UpdateDoctor will Update Doctor Object
        /// </summary>
        /// <returns></returns>
        BaseResultDto UpdateDoctor(DoctorDto doctor);

        /// <summary>
        /// Delete Doctor by ids
        /// </summary>
        /// <param name="deleteItemDto">list of Doctor Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResultDto DeleteDoctorByIds(DeleteItemDto deleteItemDto);
    }
}
