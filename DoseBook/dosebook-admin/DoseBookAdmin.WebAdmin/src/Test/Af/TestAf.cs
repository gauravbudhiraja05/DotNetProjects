using DoseBookAdmin.Dto.Doctor;
using DoseBookAdmin.Dto.Global;
using DoseBookAdmin.Dto.Test;
using DoseBookAdmin.Entity.Global;
using DoseBookAdmin.Entity.Test;
using DoseBookAdmin.WebAdmin.Common.Mapping;
using DoseBookAdmin.WebAdmin.Doctor.Af;
using DoseBookAdmin.WebAdmin.Test.Mapping;
using DoseBookAdmin.WebAdmin.Test.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoseBookAdmin.WebAdmin.Test.Af
{
    public class TestAf : ITestAf
    {
        /// <summary>
        /// Private ITestService Data Member
        /// </summary>
        private ITestService _testService;

        /// <summary>
        /// Private IDoctorAf Data Member
        /// </summary>
        private IDoctorAf _doctorAf;

        /// <summary>
        /// Private TestMapping Data Member
        /// </summary>
        private TestMapping _testMapping;

        /// <summary>
        /// Private BaseResultMapping Data Member
        /// </summary>
        private BaseResultMapping _baseResultMapping;

        /// <summary>
        /// Private DeleteItemMapping Data Member
        /// </summary>
        private DeleteItemMapping _deleteItemMapping;

        /// <summary>
        /// TestAf Constructor
        /// </summary>
        /// <param name="testService">ITestService</param>
        public TestAf(ITestService testService, IDoctorAf doctorAf)
        {
            _testService = testService;
            _doctorAf = doctorAf;
            _testMapping = new TestMapping();
            _baseResultMapping = new BaseResultMapping();
            _deleteItemMapping = new DeleteItemMapping();
        }

        public TestDtoList GetAllTests()
        {
            try
            {
                TestEntityList testEntityList = _testService.GetAllTests();
                TestDtoList testDtoList = _testMapping.EntityList2DtoList(testEntityList);
                return testDtoList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TestListGridItemDto GetTestListByDoctorWise(int doctorId)
        {
            try
            {
                TestListGridItemDto testListGridItemDto = new TestListGridItemDto();
                TestEntityList testEntityList = _testService.GetTestListByDoctorWise(doctorId);
                testListGridItemDto.TestDtoList = _testMapping.EntityList2DtoList(testEntityList);
                DoctorDtoList doctorDtoList = _doctorAf.GetAllDoctors();
                if (doctorDtoList.Count > 0 && doctorId > 0)
                {
                    DoctorDto selectedDoctorDto = doctorDtoList.Where(doctorDto => doctorDto.DoctorId == doctorId).FirstOrDefault();
                    testListGridItemDto.DoctorDto = selectedDoctorDto;
                }
                return testListGridItemDto;
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
                return _testService.GetSearchedDoctorProblemTagsList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetSearchedTestList(string prefix)
        {
            try
            {
                return _testService.GetSearchedTestList(prefix);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsTestExist(string test, int doctorId, int id)
        {
            try
            {
                return _testService.IsTestExist(test, doctorId, id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BaseResultDto SaveDoctorTestList(List<TestDto> doctorTestList)
        {
            try
            {
                TestEntityList testEntityList = _testMapping.ListofDto2EntityList(doctorTestList);
                BaseResultEntity baseResultEntity = _testService.SaveDoctorTestList(testEntityList);
                BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
                return baseResultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto SaveMasterTestList(List<TestDto> masterTestList)
        {
            try
            {
                TestEntityList testEntityList = _testMapping.ListofDto2EntityList(masterTestList);
                BaseResultEntity baseResultEntity = _testService.SaveDoctorTestList(testEntityList);
                BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
                return baseResultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto UpdateDoctorTest(TestDto doctorTest)
        {
            try
            {
                TestEntity testEntity = _testMapping.Dto2Entity(doctorTest);
                BaseResultEntity baseResultEntity = _testService.UpdateDoctorTest(testEntity);
                BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
                return baseResultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto UpdateMasterTest(TestDto masterTest)
        {
            try
            {
                TestEntity testEntity = _testMapping.Dto2Entity(masterTest);
                BaseResultEntity baseResultEntity = _testService.UpdateMasterTest(testEntity);
                BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
                return baseResultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto DeleteMasterTestByIds(DeleteItemDto deleteItemDto)
        {
            try
            {
                DeleteItemEntity deleteItemEntity = _deleteItemMapping.Dto2Entity(deleteItemDto);
                BaseResultEntity baseResultEntity = _testService.DeleteMasterTestByIds(deleteItemEntity);
                BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
                return baseResultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResultDto DeleteDoctorTestByIds(DeleteItemDto deleteItemDto)
        {
            try
            {
                DeleteItemEntity deleteItemEntity = _deleteItemMapping.Dto2Entity(deleteItemDto);
                BaseResultEntity baseResultEntity = _testService.DeleteDoctorTestByIds(deleteItemEntity);
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
