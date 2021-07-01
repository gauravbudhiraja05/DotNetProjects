using DoseBookAdmin.Entity.Global;
using DoseBookAdmin.Entity.Test;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.Test.Repository
{
    public interface ITestDao
    {
        TestEntityList GetTestListByDoctorWise(string query, object param);

        string GetSearchedDoctorProblemTagsList(string query);

        List<string> GetSearchedTestList(string query, object param);

        bool IsTestExist(string query, object param);

        BaseResultEntity SaveTest(string query, object param);

        BaseResultEntity UpdateTest(string query, object param);

        BaseResultEntity DeleteTestById(string query, object param);
    }
}
