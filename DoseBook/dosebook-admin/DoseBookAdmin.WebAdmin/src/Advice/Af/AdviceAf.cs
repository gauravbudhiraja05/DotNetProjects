using DoseBookAdmin.Dto.Advice;
using DoseBookAdmin.Dto.Doctor;
using DoseBookAdmin.Dto.Global;
using DoseBookAdmin.Entity.Advice;
using DoseBookAdmin.Entity.Global;
using DoseBookAdmin.WebAdmin.Advice.Mapping;
using DoseBookAdmin.WebAdmin.Advice.Service;
using DoseBookAdmin.WebAdmin.Common.Mapping;
using DoseBookAdmin.WebAdmin.Doctor.Af;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoseBookAdmin.WebAdmin.Advice.Af
{
    public class AdviceAf : IAdviceAf
    {
        /// <summary>
        /// Private IAdviceService Data Member
        /// </summary>
        private IAdviceService _adviceService;

        /// <summary>
        /// Private IDoctorService Data Member
        /// </summary>
        private IDoctorAf _doctorAf;

        /// <summary>
        /// Private AdviceMapping Data Member
        /// </summary>
        private AdviceMapping _adviceMapping;

        /// <summary>
        /// Private BaseResultMapping Data Member
        /// </summary>
        private BaseResultMapping _baseResultMapping;

        /// <summary>
        /// Private BaseResultMapping Data Member
        /// </summary>
        private DeleteItemMapping _deleteItemMapping;

        /// <summary>
        /// AdviceAf Constructor
        /// </summary>
        /// <param name="adviceService">IAdviceService</param>
        public AdviceAf(IAdviceService adviceService, IDoctorAf doctorAf)
        {
            _adviceService = adviceService;
            _doctorAf = doctorAf;
            _adviceMapping = new AdviceMapping();
            _baseResultMapping = new BaseResultMapping();
            _deleteItemMapping = new DeleteItemMapping();
        }

        public AdviceDtoList GetAllAdvices()
        {
            try
            {
                AdviceEntityList adviceEntityList = _adviceService.GetAllAdvices();
                AdviceDtoList adviceDtoList = _adviceMapping.EntityList2DtoList(adviceEntityList);
                return adviceDtoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AdviceListGridItemDto GetAdviceListByDoctorWise(int doctorId)
        {
            try
            {
                AdviceEntityList adviceEntityList = _adviceService.GetAdviceListByDoctorWise(doctorId);
                AdviceListGridItemDto adviceListGridItemDto = new AdviceListGridItemDto();
                adviceListGridItemDto.AdviceDtoList = _adviceMapping.EntityList2DtoList(adviceEntityList);
                DoctorDtoList doctorDtoList = _doctorAf.GetAllDoctors();
                if (doctorDtoList.Count > 0 && doctorId > 0)
                {
                    DoctorDto selectedDoctorDto = doctorDtoList.Where(doctorDto => doctorDto.DoctorId == doctorId).FirstOrDefault();
                    adviceListGridItemDto.DoctorDto = selectedDoctorDto;
                }
                return adviceListGridItemDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string GetSearchedDoctorProblemTagsList()
        {
            try
            {
                return _adviceService.GetSearchedDoctorProblemTagsList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetSearchedAdviceList(string prefix)
        {
            try
            {
                return _adviceService.GetSearchedAdviceList(prefix);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsAdviceExist(string description, int doctorId, int id)
        {
            try
            {
                return _adviceService.IsAdviceExist(description, doctorId, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto SaveDoctorAdviceList(List<AdviceDto> adviceDtoList)
        {
            try
            {
                AdviceEntityList adviceEntityList = _adviceMapping.ListofDto2EntityList(adviceDtoList);
                BaseResultEntity baseResultEntity = _adviceService.SaveDoctorAdviceList(adviceEntityList);
                BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
                return baseResultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto SaveMasterAdviceList(List<AdviceDto> adviceDtoList)
        {
            try
            {
                AdviceEntityList adviceEntityList = _adviceMapping.ListofDto2EntityList(adviceDtoList);
                BaseResultEntity baseResultEntity = _adviceService.SaveMasterAdviceList(adviceEntityList);
                BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
                return baseResultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto UpdateDoctorAdvice(AdviceDto doctorAdvice)
        {
            try
            {
                AdviceEntity adviceEntity = _adviceMapping.Dto2Entity(doctorAdvice);
                BaseResultEntity baseResultEntity = _adviceService.UpdateDoctorAdvice(adviceEntity);
                BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
                return baseResultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto UpdateMasterAdvice(AdviceDto masterAdvice)
        {
            try
            {
                AdviceEntity adviceEntity = _adviceMapping.Dto2Entity(masterAdvice);
                BaseResultEntity baseResultEntity = _adviceService.UpdateMasterAdvice(adviceEntity);
                BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
                return baseResultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto DeleteDoctorAdviceByIds(DeleteItemDto deleteItemDto)
        {
            try
            {
                DeleteItemEntity deleteItemEntity = _deleteItemMapping.Dto2Entity(deleteItemDto);
                BaseResultEntity baseResultEntity = _adviceService.DeleteDoctorAdviceByIds(deleteItemEntity);
                BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
                return baseResultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto DeleteMasterAdviceByIds(DeleteItemDto deleteItemDto)
        {
            try
            {
                DeleteItemEntity deleteItemEntity = _deleteItemMapping.Dto2Entity(deleteItemDto);
                BaseResultEntity baseResultEntity = _adviceService.DeleteMasterAdviceByIds(deleteItemEntity);
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
