using DoseBookAdmin.ViewModels.ClassificationData;
using DoseBookAdmin.ViewModels.Global;

namespace DoseBookAdmin.Core.DomainServices
{
    public interface IClassificationResultService
    {
        /// <summary>
        /// GetAllDoctors will return all Doctors
        /// </summary>
        /// <returns></returns>
        ClassificationResultListGridItemVM GetAllDoctors();

        /// <summary>
        /// GetClassificationResultByDoctorWise will return MedicineDose By Doctor Wise
        /// </summary>
        /// <returns></returns>
        ClassificationResultListGridItemVM GetClassificationResultByDoctorWise(int doctorId);

        /// <summary>
        /// GetClassificationResultDataToCreateClassificationResult will return ClassificationResult By Doctor Wise
        /// </summary>
        /// <returns></returns>
        ClassificationResult GetClassificationResultDataToCreateClassificationResult();

        /// <summary>
        /// GetDiagnosticTestByDoctorId will return all Diagnostic Test By Doctor Wise
        /// </summary>
        /// <returns></returns>
        DiagnosticTestListGridItemVM GetDiagnosticTestByDoctorId(int doctorId);

        /// <summary>
        /// GetMiscSuggestionByDoctorId will return all Misc Suggestion By Doctor Wise
        /// </summary>
        /// <returns></returns>
        MiscSuggestionListGridItemVM GetMiscSuggestionByDoctorId(int doctorId);

        /// <summary>
        /// GetMedicineDoseByDoctorId will return all Medicine Dose By Doctor Wise
        /// </summary>
        /// <returns></returns>
        MedicineDoseListGridItemVM GetMedicineDoseByDoctorId(int doctorId);

        ///<summary>
        /// Save ClassificationResult Information
        /// </summary>
        /// <param name="classificationResult">ClassificationResult data structure</param>
        /// <returns> </returns>
        BaseResult SaveClassificationResult(ClassificationResult classificationResult);

        /// <summary>
        /// Get Classification Result By Id
        /// </summary>
        /// <param name="classificationResultId">ClassificationResult Id</param>
        /// <returns>Info of Classification Result using ClassificationResult</returns>
        ClassificationResult GetClassificationResultById(int classificationResultId);

        /// <summary>
        /// Update ClassificationResult
        /// </summary>
        /// <param name="classificationResult">ClassificationResultVM data structure</param>
        /// <returns>true/false</returns>
        BaseResult UpdateClassificationResult(ClassificationResult classificationResult);

        /// <summary>
        /// Delete ClassificationResult by ids
        /// </summary>
        /// <param name="targetIds">list of ClassificationResult Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResult DeleteClassificationResultByIds(DeleteItemVM targetIds);
    }
}
