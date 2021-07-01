using DoseBookAdmin.Core.DomainServices;
using DoseBookAdmin.Core.Repositories;
using DoseBookAdmin.ViewModels.ClassificationData;
using DoseBookAdmin.ViewModels.Doctors;
using DoseBookAdmin.ViewModels.Global;
using System;
using System.Linq;

namespace DoseBookAdmin.Services
{
    public class MedicineDoseService : IMedicineDoseService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// MedicineDoseService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public MedicineDoseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public MedicineDoseListGridItemVM GetAllDoctors()
        {
            MedicineDoseListGridItemVM medicineDoseListGridItemVM = new MedicineDoseListGridItemVM();
            medicineDoseListGridItemVM.AllDoctors = _unitOfWork.DoctorRepo.GetAllDoctors("usp_GetAllDoctors").ToList();
            return medicineDoseListGridItemVM;
        }

        public MedicineDoseListGridItemVM GetMedicineDoseByDoctorWise(int doctorId)
        {
            try
            {
                var result = _unitOfWork.MedicineDoseRepo.GetMedicineDoseByDoctorWise("usp_GetMedicineDoseByDoctorId", new { doctorId = Convert.ToInt32(doctorId) });
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

        public MedicineDose GetMedicineDoseDataToCreateMedicineDose()
        {
            return _unitOfWork.MedicineDoseRepo.GetMedicineDoseDataToCreateMedicineDose("usp_GetMedicineDoseDataToCreateMedicineDose");
        }

        public BaseResult DeleteMedicineDoseByIds(DeleteItemVM targetIds)
        {
            return _unitOfWork.MedicineDoseRepo.DeleteMedicineDoseByIds("usp_DeleteMedicineDoseByIds", targetIds);
        }

        public MedicineDose GetMedicineDoseById(int medicineId)
        {
            return _unitOfWork.MedicineDoseRepo.GetMedicineDoseById("usp_GetMedicineDoseById", new { MedicineId = medicineId });
        }

        public BaseResult SaveMedicineDose(MedicineDose medicineDose)
        {
            return _unitOfWork.MedicineDoseRepo.SaveMedicineDose("usp_SaveMedicineDose", new { medicineDose.MedicineName, medicineDose.DoctorId, medicineDose.Frequency, medicineDose.Directions, medicineDose.Label, medicineDose.Duration, medicineDose.Dose, medicineDose.DoseUnit, medicineDose.IsActive });
        }

        public BaseResult UpdateMedicineDose(MedicineDose medicineDose)
        {
            return _unitOfWork.MedicineDoseRepo.UpdateMedicineDose("usp_UpdateMedicineDose", new { medicineDose.MedicineId, medicineDose.MedicineName, medicineDose.DoctorId, medicineDose.Frequency, medicineDose.Directions, medicineDose.Label, medicineDose.Duration, medicineDose.Dose, medicineDose.DoseUnit });
        }
    }
}
