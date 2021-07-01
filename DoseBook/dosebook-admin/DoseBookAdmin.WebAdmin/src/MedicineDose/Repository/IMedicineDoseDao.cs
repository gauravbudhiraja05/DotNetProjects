using DoseBookAdmin.Entity.Global;
using DoseBookAdmin.Entity.MedicineDose;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.MedicineDose.Repository
{
    public interface IMedicineDoseDao
    {
        MedicineDoseEntityList GetMedicineDoseByDoctorWise(string query, object param);

        MedicineDoseSimulationEntity GetMedicineDoseDataToCreateMedicineDose(string query);

        List<string> GetSearchedMedicineDoseList(string query, object param);

        bool IsMedicineExist(string query, object param);

        MedicineDoseEntity GetMedicineDetailByMedicineName(string query, object param);

        BaseResultEntity SaveMedicineDose(string query, object param);

        MedicineDoseSimulationEntity GetMedicineDoseById(string query, object param);

        BaseResultEntity UpdateMedicineDose(string query, object param);

        BaseResultEntity DeleteMedicineDoseById(string query, object param);

        string GetSearchedDoctorProblemTagsList(string query);
    }
}
