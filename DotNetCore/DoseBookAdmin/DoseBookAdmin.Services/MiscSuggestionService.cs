using DoseBookAdmin.Core.DomainServices;
using DoseBookAdmin.Core.Repositories;
using DoseBookAdmin.ViewModels.ClassificationData;
using DoseBookAdmin.ViewModels.Doctors;
using DoseBookAdmin.ViewModels.Global;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoseBookAdmin.Services
{
    public class MiscSuggestionService : IMiscSuggestionService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// MedicineDoseService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public MiscSuggestionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public MiscSuggestionListGridItemVM GetAllDoctors()
        {
            MiscSuggestionListGridItemVM miscSuggestionListGridItemVM = new MiscSuggestionListGridItemVM();
            miscSuggestionListGridItemVM.AllDoctors = _unitOfWork.DoctorRepo.GetAllDoctors("usp_GetAllDoctors").ToList();
            return miscSuggestionListGridItemVM;
        }

        public MiscSuggestionListGridItemVM GetMiscSuggestionByDoctorWise(int doctorId)
        {
            try
            {
                var result = _unitOfWork.MiscSuggestionRepo.GetMiscSuggestionByDoctorWise("usp_GetMiscSuggestionByDoctorId", new { doctorId = Convert.ToInt32(doctorId) });
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

        public MiscSuggestion GetMiscSuggestionDataToCreateMiscSuggestion()
        {
            return _unitOfWork.MiscSuggestionRepo.GetMiscSuggestionDataToCreateMiscSuggestion("usp_GetMiscSuggestionDataToCreateMiscSuggestion");
        }

        public BaseResult DeleteMiscSuggestionByIds(DeleteItemVM targetIds)
        {
            return _unitOfWork.MiscSuggestionRepo.DeleteMiscSuggestionByIds("usp_DeleteMiscSuggestionByIds", targetIds);
        }

        public MiscSuggestion GetMiscSuggestionById(int testId)
        {
            return _unitOfWork.MiscSuggestionRepo.GetMiscSuggestionById("usp_GetMiscSuggestionById", new { TestId = testId });
        }

        public BaseResult SaveMiscSuggestion(MiscSuggestion miscSuggestion)
        {
            return _unitOfWork.MiscSuggestionRepo.SaveMiscSuggestion("usp_SaveMiscSuggestion", new { miscSuggestion.DoctorId, miscSuggestion.TestName, miscSuggestion.Description });
        }
        public BaseResult UpdateMiscSuggestion(MiscSuggestion miscSuggestion)
        {
            return _unitOfWork.MiscSuggestionRepo.UpdateMiscSuggestion("usp_UpdateMiscSuggestion", new { miscSuggestion.DoctorId, miscSuggestion.TestId, miscSuggestion.TestName, miscSuggestion.Description });
        }
    }
}
