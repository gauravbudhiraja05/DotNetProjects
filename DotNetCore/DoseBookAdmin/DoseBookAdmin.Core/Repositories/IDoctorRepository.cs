using DoseBookAdmin.ViewModels.Doctors;
using DoseBookAdmin.ViewModels.Global;
using System.Collections.Generic;

namespace DoseBookAdmin.Core.Repositories
{
    public interface IDoctorRepository
    {
        IEnumerable<Doctor> GetAllDoctors(string query);

        BaseResult SaveDoctor(string query, object param);

        BaseResult DeleteDoctorsByIds(string query, DeleteItemVM targetIds);

        Doctor GetDoctorById(string query, object param);

        BaseResult UpdateDoctor(string query, object param);
    }
}
