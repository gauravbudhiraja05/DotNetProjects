using DoseBookAdmin.Dto.Doctor;
using DoseBookAdmin.Dto.Global;
using DoseBookAdmin.Dto.Test;
using DoseBookAdmin.Entity.Global;
using DoseBookAdmin.Entity.Test;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.Test.Service
{
    public interface ITestService
    {
        /// <summary>
        /// GetAllDoctors will return all Doctors
        /// </summary>
        /// <returns></returns>
        //DoctorDtoList GetAllDoctors();

        /// <summary>
        /// GetAllTests will return Test List
        /// </summary>
        /// <returns></returns>
        TestEntityList GetAllTests();

        /// <summary>
        /// GetTestListByDoctorWise will return Test List By Doctor Wise
        /// </summary>
        /// <returns></returns>
        TestEntityList GetTestListByDoctorWise(int doctorId);

        /// <summary>
        /// GetSearchedAdviceList will return the string of ProblemTags.
        /// </summary>
        /// <returns></returns>
        string GetSearchedDoctorProblemTagsList();

        /// <summary>
        /// GetSearchedTestList will return the list of tests.
        /// </summary>
        /// <returns></returns>
        List<string> GetSearchedTestList(string prefix);

        /// <summary>
        /// Check Test already exist or not
        /// </summary>
        /// <param name="testName">Description</param>
        /// <param name="id">Advice Id</param>
        /// <param name="doctorId">DoctorId Id</param>
        /// <returns>true/false</returns>
        bool IsTestExist(string testName, int doctorId, int id);

        /// <summary>
        /// SaveDoctorTestList will save Doctor Test Object
        /// </summary>
        /// <returns></returns>
        BaseResultEntity SaveDoctorTestList(TestEntityList testEntityList);

        /// <summary>
        /// SaveMasterTestList will save Master Test Object
        /// </summary>
        /// <returns></returns>
        BaseResultEntity SaveMasterTestList(TestEntityList testEntityList);

        /// <summary>
        /// UpdateDoctorTest will Update Doctor Test Object
        /// </summary>
        /// <returns></returns>
        BaseResultEntity UpdateDoctorTest(TestEntity testEntity);

        /// <summary>
        /// UpdateMasterTest will Update Master Test Object
        /// </summary>
        /// <returns></returns>
        BaseResultEntity UpdateMasterTest(TestEntity testEntity);

        // <summary>
        /// Delete MasterTest by ids
        /// </summary>
        /// <param name="targetIds">list of Master Advice Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResultEntity DeleteMasterTestByIds(DeleteItemEntity deleteItemEntity);

        /// <summary>
        /// Delete DoctorTest by ids
        /// </summary>
        /// <param name="targetIds">list of Doctor Advice Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResultEntity DeleteDoctorTestByIds(DeleteItemEntity deleteItemEntity);
    }
}
