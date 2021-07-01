using DoseBookAdmin.Dto.PrescriptionMeta;
using DoseBookAdmin.Entity.PrescriptionMeta;
using System;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.PrescriptionMeta.Mapping
{
    public class PrescriptionMetaMapping
    {

        #region  PrescriptionMetaType

        public PrescriptionMetaTypeEntityList ListOfTypeEntity2TypeEntityList(List<PrescriptionMetaTypeEntity> prescriptionMetaTypeEntityList)
        {
            PrescriptionMetaTypeEntityList PrescriptionMetaTypeEntityList = new PrescriptionMetaTypeEntityList();

            prescriptionMetaTypeEntityList.ForEach(prescriptionMetaTypeEntity =>
            {
                PrescriptionMetaTypeEntityList.Add(new PrescriptionMetaTypeEntity()
                {
                    Id = prescriptionMetaTypeEntity.Id,
                    Type = prescriptionMetaTypeEntity.Type,
                });
            });

            return PrescriptionMetaTypeEntityList;
        }

        public PrescriptionMetaTypeEntityList ListOfTypeDto2TypeEntityList(List<PrescriptionMetaTypeDto> prescriptionMetaTypeDtoList)
        {
            PrescriptionMetaTypeEntityList PrescriptionMetaTypeEntityList = new PrescriptionMetaTypeEntityList();

            prescriptionMetaTypeDtoList.ForEach(prescriptionMetaTypeDto =>
            {
                PrescriptionMetaTypeEntity prescriptionMetaTypeEntity = TypeDto2TypeEntity(prescriptionMetaTypeDto);

                PrescriptionMetaTypeEntityList.Add(prescriptionMetaTypeEntity);
            });

            return PrescriptionMetaTypeEntityList;
        }

        public PrescriptionMetaTypeDtoList TypeEntityList2TypeDtoList(PrescriptionMetaTypeEntityList prescriptionMetaTypeEntityList)
        {
            PrescriptionMetaTypeDtoList prescriptionMetaTypeDtoList = new PrescriptionMetaTypeDtoList();
            prescriptionMetaTypeEntityList.ForEach(prescriptionMetaTypeEntity =>
            {
                prescriptionMetaTypeDtoList.Add(TypeEntity2TypeDto(prescriptionMetaTypeEntity));
            });

            return prescriptionMetaTypeDtoList;

        }

        public PrescriptionMetaTypeDto TypeEntity2TypeDto(PrescriptionMetaTypeEntity prescriptionMetaTypeEntity)
        {
            PrescriptionMetaTypeDto prescriptionMetaTypeDto = new PrescriptionMetaTypeDto()
            {
                Id = prescriptionMetaTypeEntity.Id,
                Type = prescriptionMetaTypeEntity.Type,
            };

            return prescriptionMetaTypeDto;
        }

        public PrescriptionMetaTypeEntity TypeDto2TypeEntity(PrescriptionMetaTypeDto prescriptionMetaTypeDto)
        {
            PrescriptionMetaTypeEntity prescriptionMetaTypeEntity = new PrescriptionMetaTypeEntity()
            {
                Id = prescriptionMetaTypeDto.Id,
                Type = prescriptionMetaTypeDto.Type,
            };

            return prescriptionMetaTypeEntity;
        }

        #endregion

        #region PrescriptionMetaData

        public PrescriptionMetaDataEntityList ListOfDataEntity2DataEntityList(List<PrescriptionMetaDataEntity> prescriptionMetaDataEntityList)
        {
            PrescriptionMetaDataEntityList PrescriptionMetaDataEntityList = new PrescriptionMetaDataEntityList();

            prescriptionMetaDataEntityList.ForEach(prescriptionMetaDataEntity =>
            {
                PrescriptionMetaDataEntityList.Add(new PrescriptionMetaDataEntity()
                {
                    Id = prescriptionMetaDataEntity.Id,
                    Type = prescriptionMetaDataEntity.Type,
                    Value = prescriptionMetaDataEntity.Value,
                    DisplayOrderNumber = prescriptionMetaDataEntity.DisplayOrderNumber,

                });
            });

            return PrescriptionMetaDataEntityList;
        }

        public PrescriptionMetaDataEntityList ListOfDataDto2DataEntityList(List<PrescriptionMetaDataDto> prescriptionMetaDataDtoList)
        {
            PrescriptionMetaDataEntityList PrescriptionMetaDataEntityList = new PrescriptionMetaDataEntityList();

            prescriptionMetaDataDtoList.ForEach(prescriptionMetaDataDto =>
            {
                PrescriptionMetaDataEntity prescriptionMetaDataEntity = DataDto2DataEntity(prescriptionMetaDataDto);

                PrescriptionMetaDataEntityList.Add(prescriptionMetaDataEntity);
            });

            return PrescriptionMetaDataEntityList;
        }



        public PrescriptionMetaDataDtoList DataEntityList2DataDtoList(PrescriptionMetaDataEntityList prescriptionMetaDataEntityList)
        {
            PrescriptionMetaDataDtoList prescriptionMetaDataDtoList = new PrescriptionMetaDataDtoList();
            prescriptionMetaDataEntityList.ForEach(prescriptionMetaDataEntity =>
            {
                prescriptionMetaDataDtoList.Add(DataEntity2DataDto(prescriptionMetaDataEntity));
            });

            return prescriptionMetaDataDtoList;

        }

        public PrescriptionMetaDataDto DataEntity2DataDto(PrescriptionMetaDataEntity prescriptionMetaDataEntity)
        {
            PrescriptionMetaDataDto prescriptionMetaDataDto = new PrescriptionMetaDataDto()
            {
                Id = prescriptionMetaDataEntity.Id,
                Type = prescriptionMetaDataEntity.Type,
                Value = prescriptionMetaDataEntity.Value,
                DisplayOrderNumber = prescriptionMetaDataEntity.DisplayOrderNumber,
            };

            return prescriptionMetaDataDto;
        }

        public PrescriptionMetaDataEntity DataDto2DataEntity(PrescriptionMetaDataDto prescriptionMetaDataDto)
        {
            PrescriptionMetaDataEntity prescriptionMetaDataEntity = new PrescriptionMetaDataEntity()
            {
                Id = prescriptionMetaDataDto.Id,
                Type = prescriptionMetaDataDto.Type,
                Value = prescriptionMetaDataDto.Value,
                DisplayOrderNumber = prescriptionMetaDataDto.DisplayOrderNumber,
            };

            return prescriptionMetaDataEntity;
        }

        #endregion
    }
}
