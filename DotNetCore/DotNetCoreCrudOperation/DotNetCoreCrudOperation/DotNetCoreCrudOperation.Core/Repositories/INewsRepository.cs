using System.Collections.Generic;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.News;
using PickfordsIntranet.ViewModels.SuperAdmin;

namespace PickfordsIntranet.Core.Repositories
{
    /// <summary>
    /// IAdminRepository describes the implementation required for only SuperAdmin data/object access
    /// </summary>
    public interface INewsRepository
    {
        /// <summary>
        /// Get all Departments from target data source
        /// </summary>
        /// <returns>List of NewsGridItemVM Entity</returns>
        NewsListGridItemVM GetAllDepartments(string query);

        /// <summary>
        /// Get GetUserDepartmentDetail from target data source
        /// </summary>
        /// <returns>List of NewsListGridItemVM Entity</returns>
        NewsListGridItemVM GetUserDepartmentDetailbyUserId(string query, object param);

        /// <summary>
        /// Get all News Department Wise from target data source
        /// </summary>
        /// <returns>List of NewsGridItemVM Entity</returns>
        NewsListGridItemVM GetNewsByDepartmentWise(string query, object param);

        /// <summary>
        /// Get Pre-Requisites data to create news
        /// </summary>
        /// <returns>NewsVM Model</returns>
        NewsVM GetPreRequisitesDataToCreateNews(string query, object param);

        /// <summary>
        /// Save News
        /// </summary>
        /// <param name="user">NewsVM data structure</param>
        /// <returns>true/false</returns>
        BaseResult SaveNews(NewsVM news);

        /// <summary>
        /// Update news
        /// </summary>
        /// <param name="user">NewsVM data structure</param>
        /// <returns>true/false</returns>
        BaseResult UpdateNews(NewsVM news);

        /// <summary>
        ///  Get admin user by user id
        /// </summary>
        /// <param name="query">Stored procedure name that return News by Id</param>
        /// <param name="param">Id</param>
        /// <returns>News Info as NewsVM</returns>
        NewsVM GetNewsById(string query, object param);

        /// <summary>
        /// Delete news by Ids
        /// </summary>
        /// <param name="query">Stored procedure name</param>
        /// <param name="deleteItems">all news ids </param>
        /// <returns>BaseResult</returns>
        BaseResult DeleteNewsByIds(string query, DeleteItemVM deleteItems);

        List<ImageNamesVM> GetImagesNameForEachNewsdeleted(string query, DeleteItemVM deleteItems);


    }
}
