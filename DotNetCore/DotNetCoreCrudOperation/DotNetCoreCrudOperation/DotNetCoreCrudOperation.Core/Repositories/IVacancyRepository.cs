using System.Collections.Generic;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.News;
using PickfordsIntranet.ViewModels.SuperAdmin;
using PickfordsIntranet.ViewModels.Vacancy;

namespace PickfordsIntranet.Core.Repositories
{
    /// <summary>
    /// IAdminRepository describes the implementation required for only SuperAdmin data/object access
    /// </summary>
    public interface IVacancyRepository
    {
        /// <summary>
        /// Get all Departments from target data source
        /// </summary>
        /// <returns>List of VacancyGridItemVM Entity</returns>
        VacancyListGridItemVM GetAllDepartments(string query);

        /// <summary>
        /// Get all Vacancy Department Wise from target data source
        /// </summary>
        /// <returns>List of VacancyGridItemVM Entity</returns>
        VacancyListGridItemVM GetVacancyByDepartmentWise(string query, object param);

        /// <summary>
        /// Get GetUserDepartmentDetail from target data source
        /// </summary>
        /// <returns>List of VacancyListGridItemVM Entity</returns>
        VacancyListGridItemVM GetUserDepartmentDetailbyUserId(string query, object param);

        /// <summary>
        /// Get Pre-Requisites data to create vacancy
        /// </summary>
        /// <returns>VacancyVM Model</returns>
        VacancyVM GetPreRequisitesDataToCreateVacancy(string query, object param);

        /// <summary>
        /// Save Vacancy
        /// </summary>
        /// <param name="user">VacancyVM data structure</param>
        /// <returns>true/false</returns>
        BaseResult SaveVacancy(VacancyVM vacancy);

        /// <summary>
        /// Update Vacancy
        /// </summary>
        /// <param name="user">VacancyVM data structure</param>
        /// <returns>true/false</returns>
        BaseResult UpdateVacancy(VacancyVM vacancy);

        /// <summary>
        ///  Get vacancy by id
        /// </summary>
        /// <param name="query">Stored procedure name that return Vacancy by Id</param>
        /// <param name="param">Id</param>
        /// <returns>News Info as NewsVM</returns>
        VacancyVM GetVacancyById(string query, object param);

        /// <summary>
        /// Delete news by Ids
        /// </summary>
        /// <param name="query">Stored procedure name</param>
        /// <param name="deleteItems">all news ids </param>
        /// <returns>BaseResult</returns>
        BaseResult DeleteVacancyByIds(string query, DeleteItemVM deleteItems);

        List<ImageNamesVM> DeleteImagesForEachVacancyDeleted(string query, DeleteItemVM deleteItems);


    }
}
