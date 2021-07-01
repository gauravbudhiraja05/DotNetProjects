using DoseBookAdmin.Common.Utility;
using DoseBookAdmin.Entity.Global;
using DoseBookAdmin.Entity.Test;
using DoseBookAdmin.WebAdmin.Common.Repository;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.Test.Service
{
    public class TestService : ITestService
    {

        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// TestService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public TestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public TestEntityList GetAllTests()
        {
            return _unitOfWork.TestRepo.GetTestListByDoctorWise("usp_GetTestListByDoctorId", new { In_doctorId = 0, IN_type = DataType.Master });
        }

        public TestEntityList GetTestListByDoctorWise(int doctorId)
        {
            return _unitOfWork.TestRepo.GetTestListByDoctorWise("usp_GetTestListByDoctorId", new { In_doctorId = doctorId, IN_type = DataType.Doctor });
        }

        public string GetSearchedDoctorProblemTagsList()
        {
            return _unitOfWork.TestRepo.GetSearchedDoctorProblemTagsList("usp_GetTestSearchedDoctorProblemTagsList");
        }

        public List<string> GetSearchedTestList(string prefix)
        {
            return _unitOfWork.TestRepo.GetSearchedTestList("usp_GetSearchedTestList", new { IN_prefix = prefix });
        }

        public bool IsTestExist(string testName, int doctorId, int id)
        {
            return _unitOfWork.TestRepo.IsTestExist("usp_IsTestExist", new { IN_testName = testName, In_doctorId = doctorId, IN_id = id });
        }

        public BaseResultEntity SaveDoctorTestList(TestEntityList testEntityList)
        {
            BaseResultEntity baseResultEntity = new BaseResultEntity();
            testEntityList.ForEach(testEntity =>
            {
                baseResultEntity = _unitOfWork.TestRepo.SaveTest("usp_SaveTest", new { IN_Id = testEntity.Id, IN_testName = testEntity.TestName, IN_doctorId = testEntity.DoctorId, IN_problemTags = testEntity.ProblemTags, IN_type = DataType.Doctor, });
            });

            return baseResultEntity;
        }

        public BaseResultEntity SaveMasterTestList(TestEntityList testEntityList)
        {
            BaseResultEntity baseResultEntity = new BaseResultEntity();
            testEntityList.ForEach(testEntity =>
            {
                baseResultEntity = _unitOfWork.TestRepo.SaveTest("usp_SaveTest", new { IN_Id = testEntity.Id, IN_testName = testEntity.TestName, IN_doctorId = testEntity.DoctorId, IN_problemTags = testEntity.ProblemTags, IN_type = DataType.Master, });
            });

            return baseResultEntity;
        }

        public BaseResultEntity UpdateDoctorTest(TestEntity testEntity)
        {
            return _unitOfWork.TestRepo.UpdateTest("usp_UpdateTest", new { IN_Id = testEntity.Id, IN_testName = testEntity.TestName, IN_doctorId = testEntity.DoctorId, IN_problemTags = testEntity.ProblemTags, IN_type = DataType.Doctor });
        }

        public BaseResultEntity UpdateMasterTest(TestEntity testEntity)
        {
            return _unitOfWork.TestRepo.UpdateTest("usp_UpdateTest", new { IN_Id = testEntity.Id, IN_testName = testEntity.TestName, IN_doctorId = testEntity.DoctorId, IN_problemTags = testEntity.ProblemTags, IN_type = DataType.Master });
        }

        public BaseResultEntity DeleteMasterTestByIds(DeleteItemEntity deleteItemEntity)
        {
            BaseResultEntity baseResultEntity = new BaseResultEntity();
            deleteItemEntity.ItemIds.ForEach(itemId =>
            {
                baseResultEntity = _unitOfWork.TestRepo.DeleteTestById("usp_DeleteAdviceById", new { In_Id = itemId, IN_Type = DataType.Master });
            });

            return baseResultEntity;
        }

        public BaseResultEntity DeleteDoctorTestByIds(DeleteItemEntity deleteItemEntity)
        {
            BaseResultEntity baseResultEntity = new BaseResultEntity();
            deleteItemEntity.ItemIds.ForEach(itemId =>
            {
                baseResultEntity = _unitOfWork.TestRepo.DeleteTestById("usp_DeleteAdviceById", new { In_Id = itemId, IN_Type = DataType.Doctor });
            });

            return baseResultEntity;
        }
    }
}
