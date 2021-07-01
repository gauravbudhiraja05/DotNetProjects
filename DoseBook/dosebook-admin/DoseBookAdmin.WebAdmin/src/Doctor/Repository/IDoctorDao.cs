using DoseBookAdmin.Entity.Doctor;
using DoseBookAdmin.Entity.Global;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.Doctor.Repository
{
    public interface IDoctorDao
    {
        DoctorEntityList GetAllDoctors(string query);

        List<string> GetSearchedDoctorNameList(string query, object param);

        bool IsDoctorNameExist(string query, object param);

        BaseResultEntity SaveDoctor(string query, object param);

        BaseResultEntity UpdateDoctor(string query, object param);

        BaseResultEntity DeleteDoctorById(string query, object param);
    }
}
