using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.News;
using PickfordsIntranet.ViewModels.SuperAdmin;
using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.Core.DomainServices
{
    /// <summary>
    /// The INewsService Inteface describes the implementation required for News Section. 
    /// </summary>
    public interface INewsService
    {
        /// <summary>
        /// GetAllNewsList will return all newslist
        /// </summary>
        /// <returns></returns>
       NewsListGridItemVM GetNewsByDepartmentWise(int departmentId);

        /// <summary>
        /// GetAllDepartments will return all newslist
        /// </summary>
        /// <returns></returns>
        NewsListGridItemVM GetAllDepartments();

        /// <summary>
        /// GetUserDepartmentDetail will return department of admin
        /// </summary>
        /// <returns></returns>
        NewsListGridItemVM GetUserDepartmentDetailbyUserId(Int32 userId);

        /// <summary>
        /// GetNewsDataToCreateNews will return prerequisites populated data to create News
        /// </summary>
        /// <returns></returns>
        NewsVM GetNewsDataToCreateNews(UserActionLoggingDetails userActionLoggingDetails);

        /// <summary>
        /// Save news
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
        /// Get news by Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Info of news using NwsVM</returns>
        NewsVM GetNewsById(int id);

        /// <summary>
        /// Delete  news by ids
        /// </summary>
        /// <param name="allnewsIds">list of user Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResult DeleteNewsByIds(DeleteItemVM targetIds);
        
    }
}
