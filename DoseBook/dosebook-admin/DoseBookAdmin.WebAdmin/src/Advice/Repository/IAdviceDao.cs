using DoseBookAdmin.Entity.Global;
using DoseBookAdmin.Entity.Advice;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.Advice.Repository
{
    public interface IAdviceDao
    {
        AdviceEntityList GetAdviceListByDoctorWise(string query, object param);

        string GetSearchedDoctorProblemTagsList(string query);

        List<string> GetSearchedAdviceList(string query, object param);

        bool IsAdviceExist(string query, object param);

        BaseResultEntity SaveAdvice(string query, object param);

        BaseResultEntity UpdateAdvice(string query, object param);

        BaseResultEntity DeleteAdviceById(string query, object param);
    }
}
