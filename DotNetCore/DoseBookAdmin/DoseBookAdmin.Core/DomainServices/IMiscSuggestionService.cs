using DoseBookAdmin.ViewModels.ClassificationData;
using DoseBookAdmin.ViewModels.Global;
using System.Collections.Generic;

namespace DoseBookAdmin.Core.DomainServices
{
    public interface IMiscSuggestionService
    {

        /// <summary>
        /// GetAllDoctors will return all Doctors
        /// </summary>
        /// <returns></returns>
        MiscSuggestionListGridItemVM GetAllDoctors();

        /// <summary>
        /// GetMedicineDoseByDoctorWise will return MedicineDose By Doctor Wise
        /// </summary>
        /// <returns></returns>
        MiscSuggestionListGridItemVM GetMiscSuggestionByDoctorWise(int doctorId);

        ///<summary>
        /// Get MiscSuggestion Information
        /// </summary>
        /// <param name="doctor">MedicineDose data structure</param>
        /// <returns> </returns>
        MiscSuggestion GetMiscSuggestionDataToCreateMiscSuggestion();

        ///<summary>
        /// Save MiscSuggestion Information
        /// </summary>
        /// <param name="doctor">MiscSuggestion data structure</param>
        /// <returns> </returns>
        BaseResult SaveMiscSuggestion(MiscSuggestion doctor);

        /// <summary>
        /// Update MiscSuggestion
        /// </summary>
        /// <param name="medicineDose">MiscSuggestionVM data structure</param>
        /// <returns>true/false</returns>
        BaseResult UpdateMiscSuggestion(MiscSuggestion medicineDose);

        /// <summary>
        /// Delete MiscSuggestion by ids
        /// </summary>
        /// <param name="targetIds">list of MiscSuggestion Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResult DeleteMiscSuggestionByIds(DeleteItemVM targetIds);

        /// <summary>
        /// Get Misc Suggestion by Misc Suggestion Id
        /// </summary>
        /// <param name="id">MiscSuggestion Id</param>
        /// <returns>Info of MiscSuggestion using DoctorVM</returns>
        MiscSuggestion GetMiscSuggestionById(int id);
    }
}
