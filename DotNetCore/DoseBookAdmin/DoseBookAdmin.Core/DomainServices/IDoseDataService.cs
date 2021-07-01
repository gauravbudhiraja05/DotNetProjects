using DoseBookAdmin.ViewModels.DoseData;
using DoseBookAdmin.ViewModels.Global;

namespace DoseBookAdmin.Core.DomainServices
{
    public interface IDoseDataService
    {
        /// <summary>
        /// GetAllDoseMetaTypes will return all DoseMetaTypes
        /// </summary>
        /// <returns></returns>
        DoseMetaTypeListGridItemVM GetAllDoseMetaTypes();

        /// <summary>
        /// GetDoseDataByDoseMetaTypeWise will return all AllDoseDatas
        /// </summary>
        /// <returns></returns>
        DoseMetaTypeListGridItemVM GetDoseDataByDoseMetaTypeWise(int id);

        /// <summary>
        /// GetDoseDataToCreateDoseData will return prerequisites populated data to create DoseData
        /// </summary>
        /// <returns></returns>
        DoseData GetDoseDataToCreateDoseData();

        /// <summary>
        /// Save DoseData
        /// </summary>
        /// <param name="doseData">DoseData data structure</param>
        /// <returns>true/false</returns>
        BaseResult SaveDoseData(DoseData doseData);

        /// <summary>
        /// Get DoseData by Id
        /// </summary>
        /// <param name="doseMetaTypeId">DoseMetaTypeId</param>
        /// <returns>Info of DoseData using DoseDataVM</returns>
        DoseData GetDoseDataById(int doseMetaTypeId);

        /// <summary>
        /// Update DoseData
        /// </summary>
        /// <param name="doseData">DoseDataVM data structure</param>
        /// <returns>true/false</returns>
        BaseResult UpdateDoseData(DoseData doseData);

        /// <summary>
        /// Delete DoseData by ids
        /// </summary>
        /// <param name="targetIds">list of DoseData Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResult DeleteDoseDataByIds(DeleteItemVM targetIds);
    }
}
