using DoseBookAdmin.Dto.Doctor;
using DoseBookAdmin.Entity.Doctor;
using System;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.Doctor.Mapping
{
    public class DoctorMapping
    {
        public DoctorDtoList EntityList2DtoList(DoctorEntityList doctorEntityList)
        {
            DoctorDtoList doctorDtoList = new DoctorDtoList();

            doctorEntityList.ForEach(doctorEntity =>
            {
                doctorDtoList.Add(Entity2Dto(doctorEntity));
            });

            return doctorDtoList;
        }

        public DoctorEntityList ListOfEntity2EntityList(List<DoctorEntity> doctorEntityList)
        {
            DoctorEntityList DoctorEntityList = new DoctorEntityList();

            doctorEntityList.ForEach(doctorEntity =>
            {
                DoctorEntityList.Add(new DoctorEntity()
                {
                    DoctorId = doctorEntity.DoctorId,
                    DoctorEmail = doctorEntity.DoctorEmail,
                    DoctorName = doctorEntity.DoctorName,
                    TelephoneNumber = doctorEntity.TelephoneNumber
                });
            });

            return DoctorEntityList;
        }

        public DoctorDto Entity2Dto(DoctorEntity doctorEntity)
        {
            DoctorDto doctorDto = new DoctorDto()
            {
                DoctorId = doctorEntity.DoctorId,
                DoctorName = doctorEntity.DoctorName,
                DoctorEmail = doctorEntity.DoctorEmail,
                TelephoneNumber = doctorEntity.TelephoneNumber,
            };

            return doctorDto;
        }

        public DoctorEntityList ListofDto2EntityList(List<DoctorDto> doctorDtoList)
        {
            DoctorEntityList doctorEntityList = new DoctorEntityList();

            doctorDtoList.ForEach(doctorDto =>
            {
                DoctorEntity doctorEntity = Dto2Entity(doctorDto);
                doctorEntityList.Add(doctorEntity);
            });

            return doctorEntityList;
        }

        public DoctorEntity Dto2Entity(DoctorDto doctorDto)
        {
            DoctorEntity doctorEntity = new DoctorEntity()
            {
                DoctorId = doctorDto.DoctorId,
                DoctorName = doctorDto.DoctorName,
                DoctorEmail = doctorDto.DoctorEmail,
                TelephoneNumber = doctorDto.TelephoneNumber,
            };

            return doctorEntity;
        }
    }
}
