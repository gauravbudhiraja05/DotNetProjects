using DoseBookAdmin.ViewModels.Document;
using DoseBookAdmin.ViewModels.Global;

namespace DoseBookAdmin.Core.DomainServices
{
    public interface IDocumentService
    {
        /// <summary>
        /// GetAllDoctors will return all Doctors
        /// </summary>
        /// <returns></returns>
        DocumentListGridItemVM GetAllDoctors();

        /// <summary>
        /// GetDocumentByDoctorWise will return Document By Doctor Wise
        /// </summary>
        /// <returns></returns>
        DocumentListGridItemVM GetDocumentByDoctorWise(int doctorId);

        ///<summary>
        /// Get Document Information
        /// </summary>
        /// <returns> </returns>
        Document GetDocumentDataToCreateDocument();

        ///<summary>
        /// Save Document Information
        /// </summary>
        /// <param name="doctor">Document data structure</param>
        /// <returns> </returns>
        BaseResult SaveDocument(Document doctor);

        /// <summary>
        /// Update Doctor
        /// </summary>
        /// <param name="Document">DocumentVM data structure</param>
        /// <returns>true/false</returns>
        BaseResult UpdateDocument(Document Document);

        /// <summary>
        /// Delete doctors by ids
        /// </summary>
        /// <param name="targetIds">list of Document Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResult DeleteDocumentByIds(DeleteItemVM targetIds);

        /// <summary>
        /// Get doctor by doctor Id
        /// </summary>
        /// <param name="id">Doctor Id</param>
        /// <returns>Info of Document using DocumentVM</returns>
        Document GetDocumentById(int id);
    }
}
