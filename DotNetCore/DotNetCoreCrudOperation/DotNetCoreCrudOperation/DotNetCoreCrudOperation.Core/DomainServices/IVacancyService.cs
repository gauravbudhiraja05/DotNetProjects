using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.SuperAdmin;
using PickfordsIntranet.ViewModels.Vacancy;
using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.Core.DomainServices
{
    public interface IVacancyService
    {
        /// <summary>
        /// GetVacancyByDepartmentWise will return all vacancylist department wise
        /// </summary>
        /// <returns></returns>
        VacancyListGridItemVM GetVacancyByDepartmentWise(int departmentId);

        /// <summary>
        /// GetAllDepartments will return all departmentslist
        /// </summary>
        /// <returns></returns>
        VacancyListGridItemVM GetAllDepartments();

        /// <summary>
        /// GetUserDepartmentDetail will return department of admin
        /// </summary>
        /// <returns></returns>
        VacancyListGridItemVM GetUserDepartmentDetailbyUserId(Int32 userId);

        /// <summary>
        /// GetPrerequisticeVacancyDataToCreateVacancy will return prerequisites populated data to create Vacancy
        /// </summary>
        /// <returns></returns>
        VacancyVM GetVacancyDataToCreateVacancy(UserActionLoggingDetails userActionLoggingDetails);

        /// <summary>
        /// Save vacancy
        /// </summary>
        /// <param name="user">NewsVM data structure</param>
        /// <returns>true/false</returns>
        BaseResult SaveVacancy(VacancyVM news);

        /// <summary>
        /// Update vacancy
        /// </summary>
        /// <param name="user">VacancyVM data structure</param>
        /// <returns>true/false</returns>
        BaseResult UpdateVacancy(VacancyVM news);

        /// <summary>
        /// Get news by Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Info of news using NwsVM</returns>
        VacancyVM GetVacancyById(int id);

        /// <summary>
        /// Delete vacancy by ids
        /// </summary>
        /// <param name="allvacancyIds">list of user Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResult DeleteVacancyByIds(DeleteItemVM targetIds);
    }
}
