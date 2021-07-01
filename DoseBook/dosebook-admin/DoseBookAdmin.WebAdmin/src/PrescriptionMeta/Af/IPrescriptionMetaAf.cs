using DoseBookAdmin.Dto.Global;
using DoseBookAdmin.Dto.PrescriptionMeta;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.PrescriptionMeta.Af
{
    public interface IPrescriptionMetaAf
    {
        /// <summary>
        /// GetAllPrescriptionMetaTypes will return all PrescriptionMetaTypes
        /// </summary>
        /// <returns></returns>
        PrescriptionMetaTypeDtoList GetAllPrescriptionMetaTypes();

        /// <summary>
        /// GetPrescriptionMetaDataByPrescriptionMetaTypeWise will return all AllPrescriptionMetaDatas
        /// </summary>
        /// <returns></returns>
        PrescriptionMetaDataListGridItemDto GetPrescriptionMetaDataByPrescriptionMetaTypeWise(string prescriptionMetaType);

        /// <summary>
        /// Save PrescriptionMetaList
        /// </summary>
        /// <param name="prescriptionMetaDataList">PrescriptionMetaData List data structure</param>
        /// <returns>true/false</returns>
        BaseResultDto SavePrescriptionMetaDataList(List<PrescriptionMetaDataDto> prescriptionMetaDataList);

        /// <summary>
        /// Update PrescriptionMetaData
        /// </summary>
        /// <param name="prescriptionMeta">PrescriptionMetaDataVM data structure</param>
        /// <returns>true/false</returns>
        BaseResultDto UpdatePrescriptionMetaData(PrescriptionMetaDataDto prescriptionMetaData);

        /// <summary>
        /// Delete PrescriptionMetaData by ids
        /// </summary>
        /// <param name="targetIds">list of PrescriptionMetaData Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResultDto DeletePrescriptionMetaDataByIds(DeleteItemDto deleteItemDto);

        /// <summary>
        /// GetSearchedTypeList
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns>Result as string list object</returns>
        List<string> GetSearchedTypeList(string prefix);

        /// <summary>
        /// Check Prescription Type already exist or not
        /// </summary>
        /// <returns>true/false</returns>
        bool IsTypeExist(string type, int id);

        /// <summary>
        /// SavePrescriptionMetaTypeList will save List of Prescription Meta Type Object
        /// </summary>
        /// <returns></returns>
        BaseResultDto SavePrescriptionMetaTypeList(List<PrescriptionMetaTypeDto> prescriptionMetaTypeList);

        /// <summary>
        /// UpdatePrescriptionMetaType will update Prescription Meta Type Object
        /// </summary>
        /// <returns></returns>
        BaseResultDto UpdatePrescriptionMetaType(PrescriptionMetaTypeDto prescriptionMetaType);

        /// <summary>
        /// Delete PrescriptionMetaData by ids
        /// </summary>
        /// <param name="targetIds">list of PrescriptionMetaData Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResultDto DeletePrescriptionMetaTypeByIds(DeleteItemDto deleteItemDto);
    }
}
