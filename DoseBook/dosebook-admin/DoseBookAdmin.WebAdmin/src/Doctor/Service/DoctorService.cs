using DoseBookAdmin.Entity.Doctor;
using DoseBookAdmin.Entity.Global;
using DoseBookAdmin.WebAdmin.Common.Repository;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.Doctor.Service
{
    public class DoctorService : IDoctorService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public DoctorEntityList GetAllDoctors()
        {
            return _unitOfWork.DoctorRepo.GetAllDoctors("usp_GetAllDoctors");
        }

        public List<string> GetSearchedDoctorNameList(string prefix)
        {
            return _unitOfWork.DoctorRepo.GetSearchedDoctorNameList("usp_GetSearchedDoctorNameList", new { IN_prefix = prefix });
        }

        public bool IsDoctorNameExist(string doctorName, int id)
        {
            return _unitOfWork.DoctorRepo.IsDoctorNameExist("usp_IsDoctorNameExist", new { IN_doctorName = doctorName, In_Id = id });
        }

        public BaseResultEntity SaveDoctorList(DoctorEntityList doctorEntityList)
        {
            BaseResultEntity baseResultEntity = new BaseResultEntity();
            doctorEntityList.ForEach(doctorEntity =>
            {
                baseResultEntity = _unitOfWork.DoctorRepo.SaveDoctor("usp_SaveDoctor", new { IN_DoctorId = doctorEntity.DoctorId, IN_doctorName = doctorEntity.DoctorName, IN_doctorEmail = doctorEntity.DoctorEmail, IN_telephoneNumber = doctorEntity.TelephoneNumber });
            });

            return baseResultEntity;
        }

        public BaseResultEntity UpdateDoctor(DoctorEntity doctorEntity)
        {
            return _unitOfWork.DoctorRepo.UpdateDoctor("usp_UpdateDoctor", new { IN_DoctorId = doctorEntity.DoctorId, IN_doctorName = doctorEntity.DoctorName, IN_doctorEmail = doctorEntity.DoctorEmail, IN_telephoneNumber = doctorEntity.TelephoneNumber });
        }

        public BaseResultEntity DeleteDoctorByIds(DeleteItemEntity deleteItemEntity)
        {
            BaseResultEntity baseResultEntity = new BaseResultEntity();
            deleteItemEntity.ItemIds.ForEach(itemId =>
            {
                baseResultEntity = _unitOfWork.DoctorRepo.DeleteDoctorById("usp_DeleteDoctorById", new { In_Id = itemId });
            });

            return baseResultEntity;
        }
    }
}
