using DoseBookAdmin.Entity.Advice;
using DoseBookAdmin.Entity.Global;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.Advice.Service
{
    public interface IAdviceService
    {
        /// <summary>
        /// GetAllAdvices will return Advice List
        /// </summary>
        /// <returns></returns>
        AdviceEntityList GetAllAdvices();

        /// <summary>
        /// GetAdviceListByDoctorWise will return Advice List By Doctor Wise
        /// </summary>
        /// <returns></returns>
        AdviceEntityList GetAdviceListByDoctorWise(int doctorId);

        /// <summary>
        /// GetSearchedAdviceList will return the string of ProblemTags.
        /// </summary>
        /// <returns></returns>
        string GetSearchedDoctorProblemTagsList();

        /// <summary>
        /// GetSearchedAdviceList will return the list of advices.
        /// </summary>
        /// <returns></returns>
        List<string> GetSearchedAdviceList(string prefix);

        /// <summary>
        /// Check Advice already exist or not
        /// </summary>
        /// <returns>true/false</returns>
        bool IsAdviceExist(string description, int doctorId, int id);

        /// <summary>
        /// SaveDoctorAdviceList will save List of Doctor Advice Object
        /// </summary>
        /// <returns></returns>
        BaseResultEntity SaveDoctorAdviceList(AdviceEntityList adviceEntityList);

        /// <summary>
        /// SaveMasterAdviceList will save List of Master Advice Object
        /// </summary>
        /// <returns></returns>
        BaseResultEntity SaveMasterAdviceList(AdviceEntityList adviceEntityList);

        /// <summary>
        /// UpdateDoctorAdvice will Update Doctor Advice Object
        /// </summary>
        /// <returns></returns>
        BaseResultEntity UpdateDoctorAdvice(AdviceEntity adviceEntity);

        /// <summary>
        /// UpdateMasterAdvice will Update Master Advice Object
        /// </summary>
        /// <returns></returns>
        BaseResultEntity UpdateMasterAdvice(AdviceEntity adviceEntity);

        /// <summary>
        /// Delete DoctorAdvice by ids
        /// </summary>
        /// <param name="targetIds">list of Doctor Advice Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResultEntity DeleteDoctorAdviceByIds(DeleteItemEntity deleteItemEntity);

        /// <summary>
        /// Delete MasterAdvice by ids
        /// </summary>
        /// <param name="targetIds">list of Master Advice Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResultEntity DeleteMasterAdviceByIds(DeleteItemEntity deleteItemEntity);
    }
}
