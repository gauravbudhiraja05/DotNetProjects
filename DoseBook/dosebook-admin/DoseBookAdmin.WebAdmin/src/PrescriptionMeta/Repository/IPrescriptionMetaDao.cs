using DoseBookAdmin.Entity.Global;
using DoseBookAdmin.Entity.PrescriptionMeta;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.PrescriptionMeta.Repository
{
    public interface IPrescriptionMetaDao
    {
        PrescriptionMetaTypeEntityList GetAllPrescriptionMetaTypes(string query);

        PrescriptionMetaDataEntityList GetPrescriptionMetaDataByPrescriptionMetaTypeWise(string query, object param);

        BaseResultEntity SavePrescriptionMetaData(string query, object param);

        BaseResultEntity UpdatePrescriptionMetaData(string query, object param);

        BaseResultEntity DeletePrescriptionMetaDataById(string query, object param);

        List<string> GetSearchedTypeList(string query, object param);

        bool IsTypeExist(string query, object param);

        BaseResultEntity SavePrescriptionMetaType(string query, object param);

        BaseResultEntity UpdatePrescriptionMetaType(string query, object param);

        BaseResultEntity DeletePrescriptionMetaTypeById(string query, object param);
    }
}
