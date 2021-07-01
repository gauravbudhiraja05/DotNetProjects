using DoseBookAdmin.Dto.Doctor;
using DoseBookAdmin.Dto.Global;
using DoseBookAdmin.Dto.MedicineDose;
using DoseBookAdmin.Entity.Global;
using DoseBookAdmin.Entity.MedicineDose;
using DoseBookAdmin.WebAdmin.Common.Mapping;
using DoseBookAdmin.WebAdmin.Doctor.Af;
using DoseBookAdmin.WebAdmin.MedicineDose.Mapping;
using DoseBookAdmin.WebAdmin.MedicineDose.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoseBookAdmin.WebAdmin.MedicineDose.Af
{
    public class MedicineDoseAf : IMedicineDoseAf
    {
        /// <summary>
        /// Private IMedicineDoseService Data Member
        /// </summary>
        private IMedicineDoseService _medicineDoseService;

        /// <summary>
        /// Private IDoctorAf Data Member
        /// </summary>
        private IDoctorAf _doctorAf;

        /// <summary>
        /// Private MedicineDoseMapping Data Member
        /// </summary>
        private MedicineDoseMapping _medicineDoseMapping;

        /// <summary>
        /// Private DoctorMapping Data Member
        /// </summary>
        private BaseResultMapping _baseResultMapping;

        /// <summary>
        /// Private BaseResultMapping Data Member
        /// </summary>
        private DeleteItemMapping _deleteItemMapping;

        /// <summary>
        /// MedicineDoseService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public MedicineDoseAf(IMedicineDoseService medicineDoseService, IDoctorAf doctorAf)
        {
            _medicineDoseService = medicineDoseService;
            _doctorAf = doctorAf;
            _medicineDoseMapping = new MedicineDoseMapping();
            _baseResultMapping = new BaseResultMapping();
            _deleteItemMapping = new DeleteItemMapping();
        }

        public MedicineDoseDtoList GetAllMedicineDoses()
        {
            try
            {
                MedicineDoseEntityList medicineDoseEntityList = _medicineDoseService.GetAllMedicineDoses();
                MedicineDoseDtoList medicineDoseDtoList = _medicineDoseMapping.EntityList2DtoList(medicineDoseEntityList);
                return medicineDoseDtoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MedicineDoseListGridItemDto GetMedicineDoseListByDoctorWise(int doctorId)
        {
            try
            {
                MedicineDoseListGridItemDto medicineDoseListGridItemDto = new MedicineDoseListGridItemDto();
                MedicineDoseEntityList medicineDoseEntityList = _medicineDoseService.GetMedicineDoseListByDoctorWise(doctorId);
                medicineDoseListGridItemDto.MedicineDoseDtoList = _medicineDoseMapping.EntityList2DtoList(medicineDoseEntityList);
                DoctorDtoList doctorDtoList = _doctorAf.GetAllDoctors();
                if (doctorDtoList.Count > 0 && doctorId > 0)
                {
                    DoctorDto selectedDoctorDto = doctorDtoList.Where(doctorDto => doctorDto.DoctorId == doctorId).FirstOrDefault();
                    medicineDoseListGridItemDto.DoctorDto = selectedDoctorDto;
                }
                return medicineDoseListGridItemDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MedicineDoseSimulationDto GetMedicineDoseDataToCreateMasterMedicineDose()
        {
            try
            {
                MedicineDoseSimulationEntity medicineDoseSimulationEntity = _medicineDoseService.GetMedicineDoseDataToCreateDoctorMedicineDose();
                MedicineDoseSimulationDto medicineDoseSimulationDto = _medicineDoseMapping.SimulationEntity2SimulationDto(medicineDoseSimulationEntity);
                return medicineDoseSimulationDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetSearchedMedicineDoseList(string prefix)
        {
            try
            {
                List<string> medicineDoseList = _medicineDoseService.GetSearchedMedicineDoseList(prefix);
                return medicineDoseList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsMedicineExist(string medicineName, int doctorId, int medicineId)
        {
            try
            {
                bool exist = _medicineDoseService.IsMedicineExist(medicineName, doctorId, medicineId);
                return exist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MedicineDoseDto GetMedicineDetailByMedicineName(string medicineName, string type)
        {
            try
            {
                MedicineDoseEntity medicineDoseEntity = _medicineDoseService.GetMedicineDetailByMedicineName(medicineName, type);
                MedicineDoseDto medicineDoseDto = _medicineDoseMapping.Entity2Dto(medicineDoseEntity);
                return medicineDoseDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto SaveMasterMedicineDose(MedicineDoseDto medicineDoseDto)
        {
            try
            {
                MedicineDoseEntity medicineDoseEntity = _medicineDoseMapping.Dto2Entity(medicineDoseDto);
                BaseResultEntity baseResultEntity = _medicineDoseService.SaveMasterMedicineDose(medicineDoseEntity);
                BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
                return baseResultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MedicineDoseSimulationDto GetMasterMedicineDoseById(int medicineId)
        {
            try
            {
                MedicineDoseSimulationEntity medicineDoseSimulationEntity = _medicineDoseService.GetMasterMedicineDoseById(medicineId);
                MedicineDoseSimulationDto medicineDoseSimulationDto = _medicineDoseMapping.SimulationEntity2SimulationDto(medicineDoseSimulationEntity);
                return medicineDoseSimulationDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto UpdateMasterMedicineDose(MedicineDoseDto medicineDoseDto)
        {
            try
            {
                MedicineDoseEntity medicineDoseEntity = _medicineDoseMapping.Dto2Entity(medicineDoseDto);
                BaseResultEntity baseResultEntity = _medicineDoseService.UpdateMasterMedicineDose(medicineDoseEntity);
                BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
                return baseResultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto DeleteMasterMedicineDoseByIds(DeleteItemDto deleteItemDto)
        {
            try
            {
                DeleteItemEntity deleteItemEntity = _deleteItemMapping.Dto2Entity(deleteItemDto);
                BaseResultEntity baseResultEntity = _medicineDoseService.DeleteMasterMedicineDoseByIds(deleteItemEntity);
                BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
                return baseResultDto;
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
                return _medicineDoseService.GetSearchedDoctorProblemTagsList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MedicineDoseSimulationDto GetMedicineDoseDataToCreateDoctorMedicineDose()
        {
            try
            {
                MedicineDoseSimulationEntity medicineDoseSimulationEntity = _medicineDoseService.GetMedicineDoseDataToCreateMasterMedicineDose();
                MedicineDoseSimulationDto medicineDoseSimulationDto = _medicineDoseMapping.SimulationEntity2SimulationDto(medicineDoseSimulationEntity);
                return medicineDoseSimulationDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto SaveDoctorMedicineDose(MedicineDoseDto medicineDoseDto)
        {
            try
            {
                MedicineDoseEntity medicineDoseEntity = _medicineDoseMapping.Dto2Entity(medicineDoseDto);
                BaseResultEntity baseResultEntity = _medicineDoseService.SaveDoctorMedicineDose(medicineDoseEntity);
                BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
                return baseResultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MedicineDoseSimulationDto GetDoctorMedicineDoseById(int medicineId)
        {
            try
            {
                MedicineDoseSimulationEntity medicineDoseSimulationEntity = _medicineDoseService.GetDoctorMedicineDoseById(medicineId);
                MedicineDoseSimulationDto medicineDoseSimulationDto = _medicineDoseMapping.SimulationEntity2SimulationDto(medicineDoseSimulationEntity);
                return medicineDoseSimulationDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto UpdateDoctorMedicineDose(MedicineDoseDto medicineDoseDto)
        {
            try
            {
                MedicineDoseEntity medicineDoseEntity = _medicineDoseMapping.Dto2Entity(medicineDoseDto);
                BaseResultEntity baseResultEntity = _medicineDoseService.UpdateDoctorMedicineDose(medicineDoseEntity);
                BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
                return baseResultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto DeleteDoctorMedicineDoseByIds(DeleteItemDto deleteItemDto)
        {
            try
            {
                DeleteItemEntity deleteItemEntity = _deleteItemMapping.Dto2Entity(deleteItemDto);
                BaseResultEntity baseResultEntity = _medicineDoseService.DeleteDoctorMedicineDoseByIds(deleteItemEntity);
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
