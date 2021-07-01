using DoseBookAdmin.Dto.Test;
using DoseBookAdmin.Entity.Test;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.Test.Mapping
{
    public class TestMapping
    {
        public TestEntityList ListOfEntity2EntityList(List<TestEntity> testEntityList)
        {
            TestEntityList TestEntityList = new TestEntityList();

            testEntityList.ForEach(testEntity =>
            {
                TestEntityList.Add(new TestEntity()
                {
                    Id = testEntity.Id,
                    DoctorId = testEntity.DoctorId,
                    DoctorName = testEntity.DoctorName,
                    TestName = testEntity.TestName,
                    ProblemTags = testEntity.ProblemTags,
                });

            });

            return TestEntityList;
        }

        public TestDtoList EntityList2DtoList(TestEntityList testEntityList)
        {
            TestDtoList testDtoList = new TestDtoList();

            testEntityList.ForEach(testEntity =>
            {
                testDtoList.Add(Entity2Dto(testEntity));
            });

            return testDtoList;
        }

        public TestEntityList ListofDto2EntityList(List<TestDto> testDtoList)
        {
            TestEntityList testEntityList = new TestEntityList();

            testDtoList.ForEach(testDto =>
            {
                TestEntity testEntity = Dto2Entity(testDto);
                testEntityList.Add(testEntity);
            });

            return testEntityList;
        }

        public TestEntity Dto2Entity(TestDto testDto)
        {
            TestEntity testEntity = new TestEntity()
            {
                Id = testDto.Id,
                DoctorId = testDto.DoctorId,
                DoctorName = testDto.DoctorName,
                TestName = testDto.TestName,
                ProblemTags = testDto.ProblemTags,
            };

            return testEntity;
        }

        public TestDto Entity2Dto(TestEntity testEntity)
        {
            TestDto testDto = new TestDto()
            {
                Id = testEntity.Id,
                DoctorId = testEntity.DoctorId,
                DoctorName = testEntity.DoctorName,
                TestName = testEntity.TestName,
                ProblemTags = testEntity.ProblemTags,
            };

            return testDto;
        }
    }
}
