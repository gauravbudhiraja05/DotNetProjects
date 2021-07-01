using DoseBookAdmin.Dto.MedicineDose;
using DoseBookAdmin.Entity.MedicineDose;
using DoseBookAdmin.WebAdmin.Doctor.Mapping;
using System;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.MedicineDose.Mapping
{
    public class MedicineDoseMapping
    {
        private DoctorMapping _doctorMapping;

        public MedicineDoseMapping()
        {
            _doctorMapping = new DoctorMapping();
        }

        public MedicineDoseDto Entity2Dto(MedicineDoseEntity medicineDoseEntity)
        {
            MedicineDoseDto medicineDoseDto = new MedicineDoseDto()
            {
                MedicineId = medicineDoseEntity.MedicineId,
                DoctorId = medicineDoseEntity.DoctorId,
                DoctorName = medicineDoseEntity.DoctorName,
                MedicineName = medicineDoseEntity.MedicineName,
                Frequency = medicineDoseEntity.Frequency,
                Directions = medicineDoseEntity.Directions,
                Composition = medicineDoseEntity.Composition,
                Duration = medicineDoseEntity.Duration,
                DoseUnit = medicineDoseEntity.DoseUnit,
                Dose = medicineDoseEntity.Dose,
                ProblemTags = medicineDoseEntity.ProblemTags,
                //DoctorMedicineDoseType = medicineDoseEntity.DoctorMedicineDoseType,
                //MasterMedicineDoseMedicineName = medicineDoseEntity.MasterMedicineDoseMedicineName,
            };

            return medicineDoseDto;
        }

        public MedicineDoseEntity Dto2Entity(MedicineDoseDto medicineDoseDto)
        {
            MedicineDoseEntity medicineDoseEntity = new MedicineDoseEntity()
            {
                MedicineId = medicineDoseDto.MedicineId,
                DoctorId = medicineDoseDto.DoctorId,
                DoctorName = medicineDoseDto.DoctorName,
                MedicineName = medicineDoseDto.MedicineName,
                Frequency = medicineDoseDto.Frequency,
                Directions = medicineDoseDto.Directions,
                Composition = medicineDoseDto.Composition,
                Duration = medicineDoseDto.Duration,
                DoseUnit = medicineDoseDto.DoseUnit,
                Dose = medicineDoseDto.Dose,
                ProblemTags = medicineDoseDto.ProblemTags,
                //DoctorMedicineDoseType = medicineDoseDto.DoctorMedicineDoseType,
                //MasterMedicineDoseMedicineName = medicineDoseDto.MasterMedicineDoseMedicineName,
            };

            return medicineDoseEntity;
        }

        public MedicineDoseEntityList ListOfEntity2EntityList(List<MedicineDoseEntity> medicineDoseEntityList)
        {
            MedicineDoseEntityList MedicineDoseEntityList = new MedicineDoseEntityList();

            medicineDoseEntityList.ForEach(medicineDoseEntity =>
            {
                MedicineDoseEntityList.Add(new MedicineDoseEntity()
                {
                    MedicineId = medicineDoseEntity.MedicineId,
                    DoctorId = medicineDoseEntity.DoctorId,
                    DoctorName = medicineDoseEntity.DoctorName,
                    MedicineName = medicineDoseEntity.MedicineName,
                    Frequency = medicineDoseEntity.Frequency,
                    Directions = medicineDoseEntity.Directions,
                    Composition = medicineDoseEntity.Composition,
                    Duration = medicineDoseEntity.Duration,
                    DoseUnit = medicineDoseEntity.DoseUnit,
                    Dose = medicineDoseEntity.Dose,
                    ProblemTags = medicineDoseEntity.ProblemTags,
                    //DoctorMedicineDoseType = medicineDoseEntity.DoctorMedicineDoseType,
                    //MasterMedicineDoseMedicineName = medicineDoseEntity.MasterMedicineDoseMedicineName,
                });
            });

            return MedicineDoseEntityList;
        }

        public MedicineDoseDtoList EntityList2DtoList(MedicineDoseEntityList medicineDoseEntityList)
        {
            MedicineDoseDtoList medicineDoseDtoList = new MedicineDoseDtoList();

            medicineDoseEntityList.ForEach(medicineDoseEntity =>
            {
                medicineDoseDtoList.Add(Entity2Dto(medicineDoseEntity));
            });

            return medicineDoseDtoList;
        }

        public MedicineDoseSimulationDto SimulationEntity2SimulationDto(MedicineDoseSimulationEntity medicineDoseSimulationEntity)
        {
            MedicineDoseSimulationDto medicineDoseSimulationDto = new MedicineDoseSimulationDto()
            {
                DirectionList = medicineDoseSimulationEntity.DirectionList,
                DoseList = medicineDoseSimulationEntity.DoseList,
                DoseUnitList = medicineDoseSimulationEntity.DoseUnitList,
                DurationList = medicineDoseSimulationEntity.DurationList,
                FrequencyList = medicineDoseSimulationEntity.FrequencyList,
                DoctorDtoList = _doctorMapping.EntityList2DtoList(medicineDoseSimulationEntity.DoctorEntityList),
                MedicineDoseDto = Entity2Dto(medicineDoseSimulationEntity.MedicineDoseEntity),
            };

            return medicineDoseSimulationDto;
        }

        
    }
}
