using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Dapper;
using PickfordsIntranet.Core.Repositories;
using PickfordsIntranet.ViewModels.Auth;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.News;
using PickfordsIntranet.ViewModels.SuperAdmin;

namespace PickfordsIntranet.Repo
{
    /// <summary>
    /// AdminRepository implements the IAdminRepository and Extends the generic behabiour of User Repository
    /// </summary>
    public class NewsRepository : Repository, INewsRepository
    {
        /// <summary>
        /// AdminRepository Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        public NewsRepository(IDbConnection connection) : base(connection)
        {

        }


        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection as IDbConnection; }
        }

        public NewsListGridItemVM GetAllDepartments(string query)
        {
            try
            {
                NewsListGridItemVM rs = new NewsListGridItemVM();
                rs.AllDepartments = Connection.Query<DepartmentVM>(query, commandType: CommandType.StoredProcedure).ToList<DepartmentVM>();

                //if (rs.AllDepartments.Count > 0)
                //{
                //    DepartmentVM dept = new DepartmentVM();
                //    dept.DepartmentId = 0;
                //    dept.DepartmentName = "All";
                //    rs.AllDepartments.Insert(0, dept);
                //}
                return rs;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public NewsListGridItemVM GetUserDepartmentDetailbyUserId(string query, object param)
        {
            try
            {
                NewsListGridItemVM rs = new NewsListGridItemVM();
                rs.SelectedDepartmentDetails = Connection.Query<DepartmentVM>(query, param, null, false, null, CommandType.StoredProcedure).FirstOrDefault();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public NewsListGridItemVM GetNewsByDepartmentWise(string query, object param)
        {
            try
            {
                NewsListGridItemVM rs = new NewsListGridItemVM();
                rs.AllNewsListGridItems = Connection.Query<NewsListGridItems>(query, param, null, false, null, CommandType.StoredProcedure).ToList<NewsListGridItems>();
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NewsVM GetPreRequisitesDataToCreateNews(string query, object param)
        {
            try
            {
                NewsVM news = new NewsVM();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    news.AllDepartments = multi.Read<DepartmentVM>().AsList();
                    news.AuthorName = multi.Read<string>().SingleOrDefault();
                }

                String sDate = DateTime.Now.ToString();
                DateTime datevalue = (Convert.ToDateTime(sDate.ToString()));

                String dy = datevalue.Day.ToString();
                String mn = datevalue.Month.ToString();
                String yy = datevalue.Year.ToString();

                news.CreationDate = DateTime.Now.ToString("MM/dd/yyyy");
                //news.PublishDateDisplay = mn + "/" + dy + "/" + yy;
                //news.CreationDate = DateTime.Now.ToString("MM/dd/yyyy");
                //news.PublishDateDisplay= DateTime.Now.ToString("MM/dd/yyyy");
                return news;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult SaveNews(NewsVM news)
        {
            try
            {
                var result = this.ExecuteQuery<BaseResult>("usp_AddNews", SqlCommandType.StoredProcedure,
                    new
                    {
                        Title = news.Title,
                        TeaserText = news.TeaserText,
                        Content1 = news.Content1,
                        Content2 = news.Content2,
                        DepartmentId = news.DepartmentId,
                        IsFeatureOnHomePage = news.IsFeatureOnHomePage,
                        ThumbnailImage = news.ThumbnailImage,
                        MainImage = news.MainImage,
                        AdditionalImage1 = news.AdditionalImage1,
                        AdditionalImage2 = news.AdditionalImage2,
                        PublishDate = news.PublishDateDisplay,
                        AuthorName = news.AuthorName,
                        IsActive = news.IsActive,
                        CreatedBy = news.CreatedBy
                    }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult UpdateNews(NewsVM news)
        {
            try
            {
                var result = this.ExecuteQuery<BaseResult>("usp_UpdateNews", SqlCommandType.StoredProcedure,
                    new
                    {
                        Id = news.Id,
                        Title = news.Title,
                        TeaserText = news.TeaserText,
                        Content1 = news.Content1,
                        Content2 = news.Content2,
                        DepartmentId = news.DepartmentId,
                        IsFeatureOnHomePage = news.IsFeatureOnHomePage,
                        ThumbnailImage = news.ThumbnailImage,
                        MainImage = news.MainImage,
                        AdditionalImage1 = news.AdditionalImage1,
                        AdditionalImage2 = news.AdditionalImage2,
                        PublishDate = news.PublishDateDisplay,
                        AuthorName = news.AuthorName,
                        IsActive = news.IsActive,
                        CreatedBy = news.CreatedBy,
                        ModifiedBy = news.ModifiedBy,
                    }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public NewsVM GetNewsById(string query, object param)
        {
            try
            {
                NewsVM news = new NewsVM();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    news = multi.Read<NewsVM>().SingleOrDefault();
                    news.AllDepartments = multi.Read<DepartmentVM>().AsList();
                }

                return news;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult DeleteNewsByIds(string query, DeleteItemVM deleteItems)
        {
            try
            {
                var IdsWithDelimitedPipeline = string.Join('|', deleteItems.ItemIds);
                var result = Connection.Query<BaseResult>(query, new { NewsIds = IdsWithDelimitedPipeline, DeletedBy = deleteItems.DeletedBy }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ImageNamesVM> GetImagesNameForEachNewsdeleted(string query, DeleteItemVM deleteItems)
        {
            try
            {
                var IdsWithDelimitedPipeline = string.Join('|', deleteItems.ItemIds);
                List<ImageNamesVM> images = new List<ImageNamesVM>();
                images = Connection.Query<ImageNamesVM>(query, new { NewsIds = IdsWithDelimitedPipeline }, null, false, null, CommandType.StoredProcedure).ToList<ImageNamesVM>();
                return images;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
