using DoseBookAdmin.Core.DomainServices;
using DoseBookAdmin.Core.Repositories;
using DoseBookAdmin.ViewModels.Doctors;
using DoseBookAdmin.ViewModels.Global;
using System.Collections.Generic;

namespace DoseBookAdmin.Services
{
    public class DoctorService : IDoctorService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// DoctorService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _unitOfWork.DoctorRepo.GetAllDoctors("usp_GetAllDoctors");
        }

        public BaseResult SaveDoctor(Doctor doctor)
        {
            return _unitOfWork.DoctorRepo.SaveDoctor("usp_SaveDoctor", new { doctorname = doctor.DoctorName, telephoneNumber = doctor.TelephoneNumber });
        }

        public BaseResult UpdateDoctor(Doctor doctor)
        {
            return _unitOfWork.DoctorRepo.UpdateDoctor("usp_UpdateDoctor", new { doctorName = doctor.DoctorName, doctorId = doctor.DoctorId, telephoneNumber = doctor.TelephoneNumber });
        }

        public BaseResult DeleteDoctorsByIds(DeleteItemVM targetIds)
        {
            return _unitOfWork.DoctorRepo.DeleteDoctorsByIds("usp_DeleteDoctorsByIds", targetIds);
        }

        public Doctor GetDoctorById(int id)
        {
            return _unitOfWork.DoctorRepo.GetDoctorById("usp_GetDoctorById", new { Id = id });
        }
    }
}
