using DoseBookAdmin.Dto.Global;
using DoseBookAdmin.Dto.Test;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.Test.Af
{
    public interface ITestAf
    {
        /// <summary>
        /// GetAllTests will return Test List
        /// </summary>
        /// <returns></returns>
        TestDtoList GetAllTests();

        /// <summary>
        /// GetTestListByDoctorWise will return Advice List By Doctor Wise
        /// </summary>
        /// <returns></returns>
        TestListGridItemDto GetTestListByDoctorWise(int id);

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
        bool IsTestExist(string test, int doctorId, int id);

        /// <summary>
        /// SaveDoctorTestList will save Doctor Test Object
        /// </summary>
        /// <returns></returns>
        BaseResultDto SaveDoctorTestList(List<TestDto> doctorTestList);

        /// <summary>
        /// SaveMasterTestList will save Master Test Object
        /// </summary>
        /// <returns></returns>
        BaseResultDto SaveMasterTestList(List<TestDto> masterTestList);

        /// <summary>
        /// UpdateDoctorTest will Update Doctor Test Object
        /// </summary>
        /// <returns></returns>
        BaseResultDto UpdateDoctorTest(TestDto doctorTest);

        /// <summary>
        /// UpdateMasterTest will Update Master Test Object
        /// </summary>
        /// <returns></returns>
        BaseResultDto UpdateMasterTest(TestDto masterTest);

        // <summary>
        /// Delete MasterTest by ids
        /// </summary>
        /// <param name="targetIds">list of Master Test Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResultDto DeleteMasterTestByIds(DeleteItemDto deleteItemDto);

        /// <summary>
        /// Delete DoctorTest by ids
        /// </summary>
        /// <param name="targetIds">list of Doctor Test Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResultDto DeleteDoctorTestByIds(DeleteItemDto deleteItemDto);
    }
}
