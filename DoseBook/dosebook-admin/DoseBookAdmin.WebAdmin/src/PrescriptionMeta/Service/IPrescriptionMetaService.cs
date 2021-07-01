using DoseBookAdmin.Dto.Global;
using DoseBookAdmin.Dto.PrescriptionMeta;
using DoseBookAdmin.Entity.Global;
using DoseBookAdmin.Entity.PrescriptionMeta;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.PrescriptionMeta.Service
{
    public interface IPrescriptionMetaService
    {
        /// <summary>
        /// GetAllPrescriptionMetaTypes will return all PrescriptionMetaTypes
        /// </summary>
        /// <returns></returns>
        PrescriptionMetaTypeEntityList GetAllPrescriptionMetaTypes();

        /// <summary>
        /// GetPrescriptionMetaDataByPrescriptionMetaTypeWise will return all AllPrescriptionMetaDatas
        /// </summary>
        /// <returns></returns>
        PrescriptionMetaDataEntityList GetPrescriptionMetaDataByPrescriptionMetaTypeWise(string prescriptionMetaType);

        /// <summary>
        /// Save PrescriptionMetaList
        /// </summary>
        /// <param name="prescriptionMetaDataList">PrescriptionMetaData List data structure</param>
        /// <returns>true/false</returns>
        BaseResultEntity SavePrescriptionMetaDataList(PrescriptionMetaDataEntityList prescriptionMetaDataEntityList);

        /// <summary>
        /// Update PrescriptionMetaData
        /// </summary>
        /// <param name="prescriptionMeta">PrescriptionMetaDataVM data structure</param>
        /// <returns>true/false</returns>
        BaseResultEntity UpdatePrescriptionMetaData(PrescriptionMetaDataEntity prescriptionMetaDataEntity);

        /// <summary>
        /// Delete PrescriptionMetaData by ids
        /// </summary>
        /// <param name="targetIds">list of PrescriptionMetaData Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResultEntity DeletePrescriptionMetaDataByIds(DeleteItemEntity deleteItemEntity);

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
        BaseResultEntity SavePrescriptionMetaTypeList(PrescriptionMetaTypeEntityList prescriptionMetaTypeEntityList);

        /// <summary>
        /// UpdatePrescriptionMetaType will update Prescription Meta Type Object
        /// </summary>
        /// <returns></returns>
        BaseResultEntity UpdatePrescriptionMetaType(PrescriptionMetaTypeEntity prescriptionMetaTypeEntity);

        /// <summary>
        /// Delete PrescriptionMetaType by ids
        /// </summary>
        /// <param name="deleteItemEntity">list of Prescription Meta Type Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResultEntity DeletePrescriptionMetaTypeByIds(DeleteItemEntity deleteItemEntity);
    }
}
