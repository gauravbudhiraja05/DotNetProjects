using DoseBookAdmin.Dto.Doctor;
using DoseBookAdmin.Dto.Global;
using DoseBookAdmin.Entity.Doctor;
using DoseBookAdmin.Entity.Global;
using DoseBookAdmin.WebAdmin.Common.Mapping;
using DoseBookAdmin.WebAdmin.Doctor.Mapping;
using DoseBookAdmin.WebAdmin.Doctor.Service;
using System;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.Doctor.Af
{
    public class DoctorAf : IDoctorAf
    {
        /// <summary>
        /// Private IAdviceService Data Member
        /// </summary>
        private IDoctorService _doctorService;

        /// <summary>
        /// Private AdviceMapping Data Member
        /// </summary>
        private DoctorMapping _doctorMapping;


        /// <summary>
        /// Private BaseResultMapping Data Member
        /// </summary>
        private BaseResultMapping _baseResultMapping;

        /// <summary>
        /// Private BaseResultMapping Data Member
        /// </summary>
        private DeleteItemMapping _deleteItemMapping;

        /// <summary>
        /// DoctorAf Constructor
        /// </summary>
        /// <param name="doctorService">IDoctorService</param>
        public DoctorAf(IDoctorService doctorService)
        {
            _doctorService = doctorService;
            _doctorMapping = new DoctorMapping();
            _baseResultMapping = new BaseResultMapping();
            _deleteItemMapping = new DeleteItemMapping();
        }

        public DoctorDtoList GetAllDoctors()
        {
            try
            {
                DoctorEntityList doctorEntityList = _doctorService.GetAllDoctors();
                DoctorDtoList doctorDtoList = _doctorMapping.EntityList2DtoList(doctorEntityList);
                return doctorDtoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetSearchedDoctorNameList(string prefix)
        {
            try
            {
                return _doctorService.GetSearchedDoctorNameList(prefix);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsDoctorNameExist(string doctorName, int id)
        {
            try
            {
                return _doctorService.IsDoctorNameExist(doctorName, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto SaveDoctorList(List<DoctorDto> doctorDtoList)
        {
            try
            {
                DoctorEntityList doctorEntityList = _doctorMapping.ListofDto2EntityList(doctorDtoList);
                BaseResultEntity baseResultEntity = _doctorService.SaveDoctorList(doctorEntityList);
                BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
                return baseResultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto UpdateDoctor(DoctorDto doctor)
        {
            try
            {
                DoctorEntity doctorEntity = _doctorMapping.Dto2Entity(doctor);
                BaseResultEntity baseResultEntity = _doctorService.UpdateDoctor(doctorEntity);
                BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
                return baseResultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto DeleteDoctorByIds(DeleteItemDto deleteItemDto)
        {
            try
            {
                DeleteItemEntity deleteItemEntity = _deleteItemMapping.Dto2Entity(deleteItemDto);
                BaseResultEntity baseResultEntity = _doctorService.DeleteDoctorByIds(deleteItemEntity);
                BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
                return baseResultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
