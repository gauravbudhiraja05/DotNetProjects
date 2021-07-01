using DoseBookAdmin.Dto.Global;
using DoseBookAdmin.Dto.PrescriptionMeta;
using DoseBookAdmin.Entity.Global;
using DoseBookAdmin.Entity.PrescriptionMeta;
using DoseBookAdmin.WebAdmin.Common.Mapping;
using DoseBookAdmin.WebAdmin.PrescriptionMeta.Mapping;
using DoseBookAdmin.WebAdmin.PrescriptionMeta.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoseBookAdmin.WebAdmin.PrescriptionMeta.Af
{
    public class PrescriptionMetaAf : IPrescriptionMetaAf
    {
        /// <summary>
        /// Private IPrescriptionMetaService Meta Member
        /// </summary>
        private IPrescriptionMetaService _prescriptionMetaService;

        /// <summary>
        /// Private PrescriptionMetaMapping Meta Member
        /// </summary>
        private PrescriptionMetaMapping _prescriptionMetaMapping;

        /// <summary>
        /// Private BaseResultMapping Meta Member
        /// </summary>
        private BaseResultMapping _baseResultMapping;

        /// <summary>
        /// Private DeleteItemMapping Data Member
        /// </summary>
        private DeleteItemMapping _deleteItemMapping;

        public PrescriptionMetaAf(IPrescriptionMetaService prescriptionMetaService)
        {
            _prescriptionMetaService = prescriptionMetaService;
            _prescriptionMetaMapping = new PrescriptionMetaMapping();
            _baseResultMapping = new BaseResultMapping();
            _deleteItemMapping = new DeleteItemMapping();
        }

        public PrescriptionMetaTypeDtoList GetAllPrescriptionMetaTypes()
        {
            try
            {
                PrescriptionMetaTypeEntityList prescriptionMetaTypeEntityList = _prescriptionMetaService.GetAllPrescriptionMetaTypes();
                PrescriptionMetaTypeDtoList prescriptionMetaTypeDtoList = _prescriptionMetaMapping.TypeEntityList2TypeDtoList(prescriptionMetaTypeEntityList);
                return prescriptionMetaTypeDtoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PrescriptionMetaDataListGridItemDto GetPrescriptionMetaDataByPrescriptionMetaTypeWise(string prescriptionMetaType)
        {
            try
            {
                PrescriptionMetaDataListGridItemDto prescriptionMetaDataListGridItemDto = new PrescriptionMetaDataListGridItemDto();

                if (prescriptionMetaType == "All" || prescriptionMetaType == "Select PrescriptionType")
                    prescriptionMetaType = "";

                PrescriptionMetaDataEntityList prescriptionMetaDataEntityList = _prescriptionMetaService.GetPrescriptionMetaDataByPrescriptionMetaTypeWise(prescriptionMetaType);
                prescriptionMetaDataListGridItemDto.PrescriptionMetaDataDtoList = _prescriptionMetaMapping.DataEntityList2DataDtoList(prescriptionMetaDataEntityList);
                PrescriptionMetaTypeDtoList prescriptionMetaTypeDtoList = GetAllPrescriptionMetaTypes();

                if (prescriptionMetaType == null || prescriptionMetaType == "" || prescriptionMetaType == "Select PrescriptionType")
                    prescriptionMetaType = "All";

                if (prescriptionMetaTypeDtoList.Count > 0 && !string.IsNullOrEmpty(prescriptionMetaType))
                {
                    PrescriptionMetaTypeDto selectedPrescriptionMetaTypeDto = prescriptionMetaTypeDtoList.Where(prescriptionMetaTypeDto => prescriptionMetaTypeDto.Type == prescriptionMetaType.Trim()).FirstOrDefault();
                    prescriptionMetaDataListGridItemDto.PrescriptionMetaTypeDto = selectedPrescriptionMetaTypeDto;
                }

                return prescriptionMetaDataListGridItemDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto SavePrescriptionMetaDataList(List<PrescriptionMetaDataDto> prescriptionMetaDataDtoList)
        {
            try
            {
                PrescriptionMetaDataEntityList prescriptionMetaDataEntityList = _prescriptionMetaMapping.ListOfDataDto2DataEntityList(prescriptionMetaDataDtoList);
                BaseResultEntity baseResultEntity = _prescriptionMetaService.SavePrescriptionMetaDataList(prescriptionMetaDataEntityList);
                BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
                return baseResultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto UpdatePrescriptionMetaData(PrescriptionMetaDataDto prescriptionMetaDataDto)
        {
            try
            {
                PrescriptionMetaDataEntity prescriptionMetaDataEntity = _prescriptionMetaMapping.DataDto2DataEntity(prescriptionMetaDataDto);
                BaseResultEntity baseResultEntity = _prescriptionMetaService.UpdatePrescriptionMetaData(prescriptionMetaDataEntity);
                BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
                return baseResultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto DeletePrescriptionMetaDataByIds(DeleteItemDto deleteItemDto)
        {
            try
            {
                DeleteItemEntity deleteItemEntity = _deleteItemMapping.Dto2Entity(deleteItemDto);
                BaseResultEntity baseResultEntity = _prescriptionMetaService.DeletePrescriptionMetaDataByIds(deleteItemEntity);
                BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
                return baseResultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetSearchedTypeList(string prefix)
        {
            try
            {
                return _prescriptionMetaService.GetSearchedTypeList(prefix);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsTypeExist(string type, int id)
        {
            try
            {
                return _prescriptionMetaService.IsTypeExist(type, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto SavePrescriptionMetaTypeList(List<PrescriptionMetaTypeDto> prescriptionMetaTypeDtoList)
        {
            try
            {
                PrescriptionMetaTypeEntityList prescriptionMetaTypeEntityList = _prescriptionMetaMapping.ListOfTypeDto2TypeEntityList(prescriptionMetaTypeDtoList);
                BaseResultEntity baseResultEntity = _prescriptionMetaService.SavePrescriptionMetaTypeList(prescriptionMetaTypeEntityList);
                BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
                return baseResultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto UpdatePrescriptionMetaType(PrescriptionMetaTypeDto prescriptionMetaTypeDto)
        {
            try
            {
                PrescriptionMetaTypeEntity prescriptionMetaTypeEntity = _prescriptionMetaMapping.TypeDto2TypeEntity(prescriptionMetaTypeDto);
                BaseResultEntity baseResultEntity = _prescriptionMetaService.UpdatePrescriptionMetaType(prescriptionMetaTypeEntity);
                BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
                return baseResultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto DeletePrescriptionMetaTypeByIds(DeleteItemDto deleteItemDto)
        {
            try
            {
                DeleteItemEntity deleteItemEntity = _deleteItemMapping.Dto2Entity(deleteItemDto);
                BaseResultEntity baseResultEntity = _prescriptionMetaService.DeletePrescriptionMetaDataByIds(deleteItemEntity);
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
