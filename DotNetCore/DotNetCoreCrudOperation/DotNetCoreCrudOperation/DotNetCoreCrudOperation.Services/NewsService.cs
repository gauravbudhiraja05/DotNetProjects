using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PickfordsIntranet.Core.DomainServices;
using PickfordsIntranet.Core.Repositories;
using PickfordsIntranet.ViewModels.Auth;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.News;
using PickfordsIntranet.ViewModels.SuperAdmin;

namespace PickfordsIntranet.Services
{
    /// <summary>
    /// AdminService implemented IAdminService
    /// </summary>
    public class NewsService : INewsService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        private IPathProvider _pathProvider;

        /// <summary>
        /// NewsService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public NewsService(IUnitOfWork unitOfWork, IPathProvider pathProvider)
        {
            try
            {
                _unitOfWork = unitOfWork;
                _pathProvider = pathProvider;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        public NewsListGridItemVM GetAllDepartments()
        {
            try
            {
                //var result = _unitOfWork.Repo.ExecuteQuery<NewsListGridItemVM>("usp_GetAllNewsList", SqlCommandType.StoredProcedure);
                var result = _unitOfWork.NewsRepo.GetAllDepartments("usp_GetAllDepartments");
                //if (result.AllDepartments.Count > 0)
                //{
                //    DepartmentVM dept1 = new DepartmentVM();
                //    dept1.DepartmentId = 0;
                //    dept1.DepartmentName = "All";
                //    result.AllDepartments.Insert(0, dept1);
                //    result.UserType = "SA";
                //}
                result.UserType = "SA";
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NewsListGridItemVM GetUserDepartmentDetailbyUserId(int userId)
        {
            try
            {
                var result = _unitOfWork.NewsRepo.GetUserDepartmentDetailbyUserId("usp_GetUserDepartmentDetailbyUserId", new { userId = userId });
                result.UserType = "DA";
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NewsListGridItemVM GetNewsByDepartmentWise(int departmentId)
        {
            try
            {
                var result = _unitOfWork.NewsRepo.GetNewsByDepartmentWise("usp_GetNewsByDepartmentId", new { deptid = Convert.ToInt32(departmentId) });
                DepartmentVM seledtedDepartmentDetail = new DepartmentVM();
                seledtedDepartmentDetail.DepartmentId = departmentId;
                var result1 = _unitOfWork.NewsRepo.GetAllDepartments("usp_GetAllDepartments");
                if (result1.AllDepartments.Count > 0)
                {
                    DepartmentVM dept1 = new DepartmentVM();
                    dept1.DepartmentId = 0;
                    dept1.DepartmentName = "All";
                    result1.AllDepartments.Insert(0, dept1);

                    seledtedDepartmentDetail.DepartmentName = result1.AllDepartments.Where(dept => dept.DepartmentId == departmentId).FirstOrDefault().DepartmentName;
                    result.SelectedDepartmentDetails = seledtedDepartmentDetail;
                }
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NewsVM GetNewsDataToCreateNews(UserActionLoggingDetails userActionLoggingDetails)
        {
            try
            {
                NewsVM newsModal = _unitOfWork.NewsRepo.GetPreRequisitesDataToCreateNews("usp_GetPreRequisitesDataToCreateNews", new { UserId = Convert.ToInt32(userActionLoggingDetails.UserId) });
                /*  newsModal.PublishDateDisplay = DateTime.Now.ToString("MM.dd.yyyy")*/
                ;
                return newsModal;
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
                var result = _unitOfWork.NewsRepo.SaveNews(news);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public NewsVM GetNewsById(int id)
        {
            try
            {
                NewsVM news = _unitOfWork.NewsRepo.GetNewsById("usp_GetNewsById", new { Id = id });
                return news;
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
                var result = _unitOfWork.NewsRepo.UpdateNews(news);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult DeleteNewsByIds(DeleteItemVM targetIds)
        {
            try
            {
                DeleteImagesForEachNewsDeleted(targetIds);
                BaseResult result = _unitOfWork.NewsRepo.DeleteNewsByIds("usp_DeleteNewsByIds", targetIds);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeleteImagesForEachNewsDeleted(DeleteItemVM targetIds)
        {
            try
            {
                var result = _unitOfWork.NewsRepo.GetImagesNameForEachNewsdeleted("usp_GetImageNames", targetIds);

                foreach (var item in result)
                {
                    var thumbNailImage = _pathProvider.MapPath("") + @"\Uploads\Images\News\" + item.Thumbnailimage;
                    var mainImage = _pathProvider.MapPath("") + @"\Uploads\Images\News\" + item.MainImage;
                    var additionalImage1 = _pathProvider.MapPath("") + @"\Uploads\Images\News\" + item.AdditionalImage1;
                    var additionalImage2 = _pathProvider.MapPath("") + @"\Uploads\Images\News\" + item.AdditionalImage2;

                    if (File.Exists(thumbNailImage))
                    {
                        File.Delete(thumbNailImage);
                    }

                    if (File.Exists(mainImage))
                    {
                        File.Delete(mainImage);
                    }

                    if (File.Exists(additionalImage1))
                    {
                        File.Delete(additionalImage1);
                    }

                    if (File.Exists(additionalImage2))
                    {
                        File.Delete(additionalImage2);
                    }

                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
