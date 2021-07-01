using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PickfordsIntranet.Core.DomainServices;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.News;
using PickfordsIntranet.ViewModels.SuperAdmin;
using PickfordsIntranet.WebAdmin.Filters;
using PickfordsIntranet.WebAdmin.Utility;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PickfordsIntranet.WebAdmin.Controllers
{
    /// <summary>
    /// News Controller for News Management
    /// </summary>
    /// 
    [Authorize(Roles = "SA")]
    public class NewsController : Controller
    {

        /// <summary>
        /// INewsService data member
        /// </summary>
        private INewsService _newsService;

        /// <summary>
        /// IPathProvider data member
        /// </summary>
        private IPathProvider _pathProvider;

        /// <summary>
        /// Ilogger Data Member
        /// </summary>
        private ILogger<NewsController> _logger;


        /// <summary>
        /// SuperAdminController constructor
        /// </summary>
        /// <param name="newsService"></param>
        public NewsController(INewsService newsService, IPathProvider pathProvider, ILogger<NewsController> logger)
        {
            _pathProvider = pathProvider;
            _newsService = newsService;
            _logger = logger;
        }

        /// <summary>
        /// Get News Grid view action
        /// </summary>
        /// <returns>News Grid view</returns>
        [AutoPopulateLoggingDetails]
        public IActionResult Index(int id, UserActionLoggingDetails loggingDetails)
        {
            if (loggingDetails.RoleName == "SA")
            {
                return View(_newsService.GetAllDepartments());
            }
            else
            {
                return View(_newsService.GetUserDepartmentDetailbyUserId(Convert.ToInt32(loggingDetails.UserId)));
            }
        }

        public ActionResult GetNewsByDepartmentWise(int id)
        {
            NewsListGridItemVM newsListGridItemVM = _newsService.GetNewsByDepartmentWise(id);
            return PartialView("_NewsList", newsListGridItemVM);
        }

        /// <summary>
        /// Get Add news view action
        /// </summary>
        /// <returns>Add Bews view</returns>
        [AutoPopulateLoggingDetails]
        public IActionResult Add(int DepartmentId,  UserActionLoggingDetails loggingDetails)
        {
            try
            {
                ViewBag.DepartmentId = DepartmentId;
                return View(_newsService.GetNewsDataToCreateNews(loggingDetails));
            }

            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }
        }


        /// <summary>
        /// Post Add news to save it
        /// </summary>
        /// <param name="user">AddNewsVM data structure model</param>
        /// <returns>Success or Failure</returns>
        [HttpPost]
        [AllowAnonymous]
        [AutoPopulateLoggingDetails]
        public IActionResult Add([Bind] NewsVM news, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = null;

            try
            {
                if (ModelState.IsValid)
                {
                    if (news.ThumbnailImg != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            news.ThumbnailImg.CopyTo(memoryStream);
                            news.ThumbnailImageArray = memoryStream.ToArray();
                            //string imageName = Guid.NewGuid() + news.ThumbnailImg.FileName.Trim().Replace(" ", String.Empty);
                            string imageName = Guid.NewGuid() + Path.GetFileName(news.ThumbnailImg.FileName).Trim().Replace(" ", String.Empty);
                            string imagePath = _pathProvider.MapPath("") + @"\Uploads\Images\News\" + imageName;
                            System.IO.File.WriteAllBytes(imagePath, news.ThumbnailImageArray);
                            news.ThumbnailImage = imageName;
                        }
                    }

                    if (news.MainImg != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            news.MainImg.CopyTo(memoryStream);
                            news.MainImageArray = memoryStream.ToArray();
                            //string imageName = Guid.NewGuid() + news.MainImg.FileName.Trim().Replace(" ", String.Empty);
                            string imageName = Guid.NewGuid() + Path.GetFileName(news.MainImg.FileName).Trim().Replace(" ", String.Empty);
                            string imagePath = _pathProvider.MapPath("") + @"\Uploads\Images\News\" + imageName;
                            System.IO.File.WriteAllBytes(imagePath, news.MainImageArray);
                            news.MainImage = imageName;
                        }
                    }
                    if (news.AdditionalImg1 != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            news.AdditionalImg1.CopyTo(memoryStream);
                            news.AdditionalImage1Array = memoryStream.ToArray();
                            //string imageName = Guid.NewGuid() + news.AdditionalImg1.FileName.Trim().Replace(" ", String.Empty);
                            string imageName = Guid.NewGuid() + Path.GetFileName(news.AdditionalImg1.FileName).Trim().Replace(" ", String.Empty);
                            string imagePath = _pathProvider.MapPath("") + @"\Uploads\Images\News\" + imageName;
                            System.IO.File.WriteAllBytes(imagePath, news.AdditionalImage1Array);
                            news.AdditionalImage1 = imageName;
                        }
                    }

                    if (news.AdditionalImg2 != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            news.AdditionalImg2.CopyTo(memoryStream);
                            news.AdditionalImage2Array = memoryStream.ToArray();
                            //string imageName = Guid.NewGuid() + news.AdditionalImg2.FileName.Trim().Replace(" ", String.Empty);
                            string imageName = Guid.NewGuid() + Path.GetFileName(news.AdditionalImg2.FileName).Trim().Replace(" ", String.Empty);
                            string imagePath = _pathProvider.MapPath("") + @"\Uploads\Images\News\" + imageName;
                            System.IO.File.WriteAllBytes(imagePath, news.AdditionalImage2Array);
                            news.AdditionalImage2 = imageName;
                        }

                    }
                    news.IsActive = 1;
                    news.CreatedBy = Convert.ToInt32(loggingDetails.UserId);
                    result = _newsService.SaveNews(news);   
                }

                return Ok(result);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }            
        }


        /// <summary>
        /// Edit News Record
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>View of Edit News</returns>
        public IActionResult Edit(int NewsId, int SelectedDepartmentId)
        {
            try
            {
                NewsVM news = _newsService.GetNewsById(NewsId);
                ViewBag.SelectedDepartmentId = SelectedDepartmentId;
                return View(news);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("Error");
            }

        }

        /// <summary>
        /// Post Update news to update existing news
        /// </summary>
        /// <param name="user">Edit NewsVM data structure model</param>
        /// <returns>Success or Failure</returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult Update(NewsVM news, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = null;
            ModelState.Remove("ThumbnailImg");
            ModelState.Remove("MainImg");
            ModelState.Remove("AdditionalImg1");
            ModelState.Remove("AdditionalImg2");
            try
            {
                if (ModelState.IsValid)
                {
                    if (news.ThumbnailImg != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            news.ThumbnailImg.CopyTo(memoryStream);
                            news.ThumbnailImageArray = memoryStream.ToArray();
                            //string imageName = Guid.NewGuid() + news.ThumbnailImg.FileName.Trim().Replace(" ", String.Empty);
                            string imageName = Guid.NewGuid() + Path.GetFileName(news.ThumbnailImg.FileName).Trim().Replace(" ", String.Empty);
                            string imagePath = _pathProvider.MapPath("") + @"\Uploads\Images\News\" + imageName;
                            System.IO.File.WriteAllBytes(imagePath, news.ThumbnailImageArray);
                            news.ThumbnailImage = imageName;
                        }
                    }

                    if (news.MainImg != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            news.MainImg.CopyTo(memoryStream);
                            news.MainImageArray = memoryStream.ToArray();
                            //string imageName = Guid.NewGuid() + news.MainImg.FileName.Trim().Replace(" ", String.Empty);
                            string imageName = Guid.NewGuid() + Path.GetFileName(news.MainImg.FileName).Trim().Replace(" ", String.Empty);
                            string imagePath = _pathProvider.MapPath("") + @"\Uploads\Images\News\" + imageName;
                            System.IO.File.WriteAllBytes(imagePath, news.MainImageArray);
                            news.MainImage = imageName;
                        }
                    }
                    if (news.AdditionalImg1 != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            news.AdditionalImg1.CopyTo(memoryStream);
                            news.AdditionalImage1Array = memoryStream.ToArray();
                            //string imageName = Guid.NewGuid() + news.AdditionalImg1.FileName.Trim().Replace(" ", String.Empty);
                            string imageName = Guid.NewGuid() + Path.GetFileName(news.AdditionalImg1.FileName).Trim().Replace(" ", String.Empty);
                            string imagePath = _pathProvider.MapPath("") + @"\Uploads\Images\News\" + imageName;
                            System.IO.File.WriteAllBytes(imagePath, news.AdditionalImage1Array);
                            news.AdditionalImage1 = imageName;
                        }
                    }

                    if (news.AdditionalImg2 != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            news.AdditionalImg2.CopyTo(memoryStream);
                            news.AdditionalImage2Array = memoryStream.ToArray();
                            //string imageName = Guid.NewGuid() + news.AdditionalImg2.FileName.Trim().Replace(" ", String.Empty);
                            string imageName = Guid.NewGuid() + Path.GetFileName(news.AdditionalImg2.FileName).Trim().Replace(" ", String.Empty);
                            string imagePath = _pathProvider.MapPath("") + @"\Uploads\Images\News\" + imageName;
                            System.IO.File.WriteAllBytes(imagePath, news.AdditionalImage2Array);
                            news.AdditionalImage2 = imageName;
                        }

                    }
                    news.IsActive = 1;
                    news.ModifiedBy = Convert.ToInt32(loggingDetails.UserId);
                    result = _newsService.UpdateNews(news);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }
        }

        //DeleteNews
        /// <summary>
        /// Delete News  
        /// </summary>
        /// <param name="allUserIds">List of Admin User IDs</param>
        /// <returns>Success/Failuare as Baseresult</returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult DeleteNews(DeleteItemVM targetIds, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = new BaseResult();
            targetIds.DeletedBy = Convert.ToInt32(loggingDetails.UserId);

            try
            {
                result = _newsService.DeleteNewsByIds(targetIds);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }

            return Ok(result);
        }
    }
}