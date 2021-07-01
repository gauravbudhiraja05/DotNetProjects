using DoseBookAdmin.ViewModels.ClassificationData;
using DoseBookAdmin.ViewModels.Global;

namespace DoseBookAdmin.Core.Repositories
{
    public interface IMedicineDoseRepository
    {
        MedicineDoseListGridItemVM GetMedicineDoseByDoctorWise(string query, object param);

        MedicineDose GetMedicineDoseDataToCreateMedicineDose(string query);

        BaseResult DeleteMedicineDoseByIds(string query, DeleteItemVM targetIds);

        MedicineDose GetMedicineDoseById(string query, object param);

        BaseResult SaveMedicineDose(string query, object param);

        BaseResult UpdateMedicineDose(string query, object param);
    }
}
