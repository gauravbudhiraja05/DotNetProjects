using DoseBookAdmin.Dto.Advice;
using DoseBookAdmin.Dto.Global;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.Advice.Af
{
    public interface IAdviceAf
    {
        /// <summary>
        /// GetAllAdvices will return Advice List
        /// </summary>
        /// <returns></returns>
        AdviceDtoList GetAllAdvices();

        /// <summary>
        /// GetAdviceListByDoctorWise will return Advice List By Doctor Wise
        /// </summary>
        /// <returns></returns>
        AdviceListGridItemDto GetAdviceListByDoctorWise(int doctorId);

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
        BaseResultDto SaveDoctorAdviceList(List<AdviceDto> doctorAdviceList);

        /// <summary>
        /// SaveMasterAdviceList will save List of Master Advice Object
        /// </summary>
        /// <returns></returns>
        BaseResultDto SaveMasterAdviceList(List<AdviceDto> masterAdviceList);

        /// <summary>
        /// UpdateDoctorAdvice will Update Doctor Advice Object
        /// </summary>
        /// <returns></returns>
        BaseResultDto UpdateDoctorAdvice(AdviceDto doctorAdvice);

        /// <summary>
        /// UpdateMasterAdvice will Update Master Advice Object
        /// </summary>
        /// <returns></returns>
        BaseResultDto UpdateMasterAdvice(AdviceDto masterAdvice);

        /// <summary>
        /// Delete MasterAdvice by ids
        /// </summary>
        /// <param name="targetIds">list of Master Advice Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResultDto DeleteMasterAdviceByIds(DeleteItemDto deleteItemDto);

        /// <summary>
        /// Delete DoctorAdvice by ids
        /// </summary>
        /// <param name="targetIds">list of Doctor Advice Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResultDto DeleteDoctorAdviceByIds(DeleteItemDto targetIds);
    }
}
