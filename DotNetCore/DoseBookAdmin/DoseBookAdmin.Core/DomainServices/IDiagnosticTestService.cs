using DoseBookAdmin.ViewModels.ClassificationData;
using DoseBookAdmin.ViewModels.Global;
using System.Collections.Generic;

namespace DoseBookAdmin.Core.DomainServices
{
    public interface IDiagnosticTestService
    {
        /// <summary>
        /// GetAllDoctors will return all Doctors
        /// </summary>
        /// <returns></returns>
        DiagnosticTestListGridItemVM GetAllDoctors();

        /// <summary>
        /// GetDiagnosticTestByDoctorWise will return all DiagnosticTest by Doctor Wise
        /// </summary>
        /// <returns></returns>
        DiagnosticTestListGridItemVM GetDiagnosticTestByDoctorWise(int id);

        /// <summary>
        /// GetDiagnosticTestDataToCreateDiagnosticTest will return DiagnosticTest to create Diagnostic Test
        /// </summary>
        /// <returns></returns>
        DiagnosticTest GetDiagnosticTestDataToCreateDiagnosticTest();

        ///<summary>
        /// Save DiagnosticTest Information
        /// </summary>
        /// <param name="doctor">DiagnosticTest data structure</param>
        /// <returns> </returns>
        BaseResult SaveDiagnosticTest(DiagnosticTest doctor);

        /// <summary>
        /// Update Doctor
        /// </summary>
        /// <param name="medicineDose">DiagnosticTestVM data structure</param>
        /// <returns>true/false</returns>
        BaseResult UpdateDiagnosticTest(DiagnosticTest medicineDose);

        /// <summary>
        /// Delete doctors by ids
        /// </summary>
        /// <param name="targetIds">list of DiagnosticTest Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResult DeleteDiagnosticTestByIds(DeleteItemVM targetIds);
        
        /// <summary>
        /// Get doctor by doctor Id
        /// </summary>
        /// <param name="id">Doctor Id</param>
        /// <returns>Info of DiagnosticTest using DoctorVM</returns>
        DiagnosticTest GetDiagnosticTestById(int id);
    }
}
