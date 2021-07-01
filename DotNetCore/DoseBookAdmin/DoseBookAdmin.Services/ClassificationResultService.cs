using DoseBookAdmin.Core.DomainServices;
using DoseBookAdmin.Core.Repositories;
using DoseBookAdmin.ViewModels.ClassificationData;
using DoseBookAdmin.ViewModels.Doctors;
using DoseBookAdmin.ViewModels.Global;
using System;
using System.Linq;

namespace DoseBookAdmin.Services
{
    public class ClassificationResultService : IClassificationResultService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// MedicineDoseService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public ClassificationResultService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ClassificationResultListGridItemVM GetAllDoctors()
        {
            ClassificationResultListGridItemVM classificationResultListGridItemVM = new ClassificationResultListGridItemVM();
            classificationResultListGridItemVM.AllDoctors = _unitOfWork.DoctorRepo.GetAllDoctors("usp_GetAllDoctors").ToList();
            return classificationResultListGridItemVM;
        }

        public ClassificationResultListGridItemVM GetClassificationResultByDoctorWise(int doctorId)
        {
            try
            {
                var result = _unitOfWork.ClassificationResultRepo.GetClassificationResultByDoctorWise("usp_GetClassificationResultByDoctorId", new { doctorId = Convert.ToInt32(doctorId) });
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

        public ClassificationResult GetClassificationResultDataToCreateClassificationResult()
        {
            return _unitOfWork.ClassificationResultRepo.GetClassificationResultDataToCreateClassificationResult("usp_GetClassificationResultDataToCreateClassificationResult");
        }

        public DiagnosticTestListGridItemVM GetDiagnosticTestByDoctorId(int doctorId)
        {
            return _unitOfWork.DiagnosticTestRepo.GetDiagnosticTestByDoctorWise("usp_GetDiagnosticTestByDoctorId", new { doctorId = Convert.ToInt32(doctorId) });
        }

        public MiscSuggestionListGridItemVM GetMiscSuggestionByDoctorId(int doctorId)
        {
            return _unitOfWork.MiscSuggestionRepo.GetMiscSuggestionByDoctorWise("usp_GetMiscSuggestionByDoctorId", new { doctorId = Convert.ToInt32(doctorId) });
        }

        public MedicineDoseListGridItemVM GetMedicineDoseByDoctorId(int doctorId)
        {
            return _unitOfWork.MedicineDoseRepo.GetMedicineDoseByDoctorWise("usp_GetMedicineDoseByDoctorId", new { doctorId = Convert.ToInt32(doctorId) });
        }

        public BaseResult SaveClassificationResult(ClassificationResult classificationResult)
        {
            return _unitOfWork.ClassificationResultRepo.SaveClassificationResult("usp_SaveClassificationResult", new { classificationResult.ClassificationResultName, classificationResult.ClassificationTypeId, classificationResult.ClassificationId, classificationResult.DoctorId });
        }

        public ClassificationResult GetClassificationResultById(int classificationResultId)
        {
            return _unitOfWork.ClassificationResultRepo.GetClassificationResultById("usp_GetClassificationResultById", new { ClassificationResultId = classificationResultId });
        }

        public BaseResult UpdateClassificationResult(ClassificationResult classificationResult)
        {
            return _unitOfWork.ClassificationResultRepo.UpdateClassificationResult("usp_UpdateClassificationResult", new { classificationResult.ClassificationResultId, classificationResult.ClassificationResultName, classificationResult.ClassificationTypeId, classificationResult.ClassificationId, classificationResult.DoctorId });
        }

        public BaseResult DeleteClassificationResultByIds(DeleteItemVM targetIds)
        {
            return _unitOfWork.ClassificationResultRepo.DeleteClassificationResultByIds("usp_DeleteClassificationResultByIds", targetIds);
        }
    }
}
