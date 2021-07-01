using DoseBookAdmin.ViewModels.DoseData;
using DoseBookAdmin.ViewModels.Global;

namespace DoseBookAdmin.Core.Repositories
{
    public interface IDoseDataRepository
    {
        DoseMetaTypeListGridItemVM GetAllDoseMetaTypes(string query);

        DoseMetaTypeListGridItemVM GetDoseDataByDoseMetaTypeWise(string query, object param);
        
        DoseData GetDoseDataToCreateDoseData(string query);

        BaseResult SaveDoseData(DoseData doseData);

        DoseData GetDoseDataById(string query, object param);

        BaseResult UpdateDoseData(DoseData doseData);

        BaseResult DeleteDoseDataByIds(string query, DeleteItemVM targetIds);
    }
}
