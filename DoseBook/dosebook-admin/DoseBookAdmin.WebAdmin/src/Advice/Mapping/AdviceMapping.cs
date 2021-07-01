using DoseBookAdmin.Dto.Advice;
using DoseBookAdmin.Entity.Advice;
using DoseBookAdmin.WebAdmin.Doctor.Mapping;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.Advice.Mapping
{
    public class AdviceMapping
    {
        private DoctorMapping _doctorMapping;

        public AdviceMapping()
        {
            _doctorMapping = new DoctorMapping();
        }

        public AdviceEntityList ListOfEntity2EntityList(List<AdviceEntity> adviceEntityList)
        {
            AdviceEntityList AdviceEntityList = new AdviceEntityList();

            adviceEntityList.ForEach(adviceEntity =>
            {
                AdviceEntityList.Add(new AdviceEntity()
                {
                    Id = adviceEntity.Id,
                    DoctorId = adviceEntity.DoctorId,
                    DoctorName = adviceEntity.DoctorName,
                    Description = adviceEntity.Description,
                    ProblemTags = adviceEntity.ProblemTags,
                });
            });

            return AdviceEntityList;
        }

        public AdviceEntityList ListofDto2EntityList(List<AdviceDto> adviceDtoList)
        {
            AdviceEntityList AdviceEntityList = new AdviceEntityList();

            adviceDtoList.ForEach(adviceDto =>
            {
                AdviceEntity adviceEntity = Dto2Entity(adviceDto);
                AdviceEntityList.Add(adviceEntity);
            });

            return AdviceEntityList;
        }



        public AdviceDtoList EntityList2DtoList(AdviceEntityList adviceEntityList)
        {
            AdviceDtoList adviceDtoList = new AdviceDtoList();

            adviceEntityList.ForEach(adviceEntity =>
            {
                adviceDtoList.Add(Entity2Dto(adviceEntity));
            });

            return adviceDtoList;

        }

        public AdviceDto Entity2Dto(AdviceEntity adviceEntity)
        {
            AdviceDto adviceDto = new AdviceDto()
            {
                Id = adviceEntity.Id,
                Description = adviceEntity.Description,
                DoctorId = adviceEntity.DoctorId,
                DoctorName = adviceEntity.DoctorName,
                ProblemTags = adviceEntity.ProblemTags,
            };

            return adviceDto;
        }

        public AdviceEntity Dto2Entity(AdviceDto adviceDto)
        {
            AdviceEntity adviceEntity = new AdviceEntity()
            {
                Id = adviceDto.Id,
                Description = adviceDto.Description,
                DoctorId = adviceDto.DoctorId,
                DoctorName = adviceDto.DoctorName,
                ProblemTags = adviceDto.ProblemTags,
            };

            return adviceEntity;
        }
    }
}
