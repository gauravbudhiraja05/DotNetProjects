using DoseBookAdmin.Core.DomainServices;
using DoseBookAdmin.Core.Repositories;
using DoseBookAdmin.ViewModels.Doctors;
using DoseBookAdmin.ViewModels.Document;
using DoseBookAdmin.ViewModels.Global;
using System;
using System.Linq;

namespace DoseBookAdmin.Services
{
    public class DocumentService : IDocumentService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// DocumentService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public DocumentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public DocumentListGridItemVM GetAllDoctors()
        {
            DocumentListGridItemVM documentListGridItemVM = new DocumentListGridItemVM();
            documentListGridItemVM.AllDoctors = _unitOfWork.DoctorRepo.GetAllDoctors("usp_GetAllDoctors").ToList();
            return documentListGridItemVM;
        }

        public DocumentListGridItemVM GetDocumentByDoctorWise(int doctorId)
        {
            try
            {
                var result = _unitOfWork.DocumentRepo.GetDocumentByDoctorWise("usp_GetDocumentByDoctorId", new { doctorId = Convert.ToInt32(doctorId) });
                Doctor selectedDoctorDetail = new Doctor();
                selectedDoctorDetail.DoctorId = doctorId;
                var doctorList = _unitOfWork.DoctorRepo.GetAllDoctors("usp_GetAllDoctors").ToList();
                if (doctorList.Count > 0 && doctorId > 0)
                {
                    selectedDoctorDetail.DoctorName = doctorList.Where(doctor => doctor.DoctorId == doctorId).FirstOrDefault().DoctorName;
                    result.SelectedDoctor = selectedDoctorDetail;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Document GetDocumentDataToCreateDocument()
        {
            return _unitOfWork.DocumentRepo.GetDocumentDataToCreateDocument("usp_GetDocumentDataToCreateDocument");
        }

        public BaseResult DeleteDocumentByIds(DeleteItemVM targetIds)
        {
            return _unitOfWork.DocumentRepo.DeleteDocumentByIds("usp_DeleteDocumentByIds", targetIds);
        }

        public Document GetDocumentById(int medicineId)
        {
            return _unitOfWork.DocumentRepo.GetDocumentById("usp_GetDocumentById", new { MedicineId = medicineId });
        }

        public BaseResult SaveDocument(Document document)
        {
            return _unitOfWork.DocumentRepo.SaveDocument("usp_SaveDocument", new { document.Label, document.DoctorId, document.ClassificationResultId, document.Description });
        }

        public BaseResult UpdateDocument(Document document)
        {
            return _unitOfWork.DocumentRepo.UpdateDocument("usp_UpdateDocument", new { document.DocumentId, document.Label, document.DoctorId, document.ClassificationResultId, document.Description });
        }
    }
}
