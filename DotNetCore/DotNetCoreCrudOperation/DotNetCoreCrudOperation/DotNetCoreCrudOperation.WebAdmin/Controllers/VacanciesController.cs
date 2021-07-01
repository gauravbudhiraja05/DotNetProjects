using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PickfordsIntranet.Core.DomainServices;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.Vacancy;
using PickfordsIntranet.WebAdmin.Filters;
using PickfordsIntranet.WebAdmin.Utility;
using System;
using System.IO;

namespace PickfordsIntranet.WebAdmin.Controllers
{
    /// <summary>
    /// Vacancies Controller for Vacancies management 
    /// </summary>
    /// 
    [Authorize(Roles = "SA")]
    public class VacanciesController : Controller
    {/// <summary>
     /// INewsService data member
     /// </summary>
        private IVacancyService _vacancyService;

        /// <summary>
        /// IPathProvider data member
        /// </summary>
        private IPathProvider _pathProvider;

        /// <summary>
        /// Ilogger Data Member
        /// </summary>
        private ILogger<SuperAdminController> _logger;


        /// <summary>
        /// SuperAdminController constructor
        /// </summary>
        /// <param name="newsService"></param>
        public VacanciesController(IVacancyService vacancyService, IPathProvider pathProvider, ILogger<SuperAdminController> logger)
        {
            _pathProvider = pathProvider;
            _vacancyService = vacancyService;
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
                return View(_vacancyService.GetAllDepartments());
            }
            else
            {
                return View(_vacancyService.GetUserDepartmentDetailbyUserId(Convert.ToInt32(loggingDetails.UserId)));
            }
        }

        [AllowAnonymous]
        public ActionResult GetVacancyByDepartmentWise(int id)
        {
            VacancyListGridItemVM vacancyListGridItemVM = _vacancyService.GetVacancyByDepartmentWise(id);
            return PartialView("_VacancyList", vacancyListGridItemVM);
        }

        /// <summary>
        /// Get Add news view action
        /// </summary>
        /// <returns>Add Bews view</returns>
        [AutoPopulateLoggingDetails]
        public IActionResult Add(int DepartmentId, UserActionLoggingDetails loggingDetails)
        {
            try
            {
                ViewBag.DepartmentId = DepartmentId;
                return View(_vacancyService.GetVacancyDataToCreateVacancy(loggingDetails));
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
        public IActionResult Add([Bind] VacancyVM vacancy, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = null;

            try
            {
                if (ModelState.IsValid)
                {
                    if (vacancy.ThumbnailImg != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            vacancy.ThumbnailImg.CopyTo(memoryStream);
                            vacancy.ThumbnailImageArray = memoryStream.ToArray();
                            //string imageName = Guid.NewGuid() + news.ThumbnailImg.FileName.Trim().Replace(" ", String.Empty);
                            string imageName = Guid.NewGuid() + Path.GetFileName(vacancy.ThumbnailImg.FileName).Trim().Replace(" ", String.Empty);
                            string imagePath = _pathProvider.MapPath("") + @"\Uploads\Images\Vacancy\" + imageName;
                            System.IO.File.WriteAllBytes(imagePath, vacancy.ThumbnailImageArray);
                            vacancy.ThumbnailImage = imageName;
                        }
                    }

                    if (vacancy.MainImg != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            vacancy.MainImg.CopyTo(memoryStream);
                            vacancy.MainImageArray = memoryStream.ToArray();
                            //string imageName = Guid.NewGuid() + news.MainImg.FileName.Trim().Replace(" ", String.Empty);
                            string imageName = Guid.NewGuid() + Path.GetFileName(vacancy.MainImg.FileName).Trim().Replace(" ", String.Empty);
                            string imagePath = _pathProvider.MapPath("") + @"\Uploads\Images\Vacancy\" + imageName;
                            System.IO.File.WriteAllBytes(imagePath, vacancy.MainImageArray);
                            vacancy.MainImage = imageName;
                        }
                    }
                    if (vacancy.AdditionalImg1 != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            vacancy.AdditionalImg1.CopyTo(memoryStream);
                            vacancy.AdditionalImage1Array = memoryStream.ToArray();
                            //string imageName = Guid.NewGuid() + news.AdditionalImg1.FileName.Trim().Replace(" ", String.Empty);
                            string imageName = Guid.NewGuid() + Path.GetFileName(vacancy.AdditionalImg1.FileName).Trim().Replace(" ", String.Empty);
                            string imagePath = _pathProvider.MapPath("") + @"\Uploads\Images\Vacancy\" + imageName;
                            System.IO.File.WriteAllBytes(imagePath, vacancy.AdditionalImage1Array);
                            vacancy.AdditionalImage1 = imageName;
                        }
                    }

                    if (vacancy.AdditionalImg2 != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            vacancy.AdditionalImg2.CopyTo(memoryStream);
                            vacancy.AdditionalImage2Array = memoryStream.ToArray();
                            //string imageName = Guid.NewGuid() + news.AdditionalImg2.FileName.Trim().Replace(" ", String.Empty);
                            string imageName = Guid.NewGuid() + Path.GetFileName(vacancy.AdditionalImg2.FileName).Trim().Replace(" ", String.Empty);
                            string imagePath = _pathProvider.MapPath("") + @"\Uploads\Images\Vacancy\" + imageName;
                            System.IO.File.WriteAllBytes(imagePath, vacancy.AdditionalImage2Array);
                            vacancy.AdditionalImage2 = imageName;
                        }

                    }
                    vacancy.IsActive = 1;
                    vacancy.CreatedBy = Convert.ToInt32(loggingDetails.UserId);
                    result = _vacancyService.SaveVacancy(vacancy);
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
        /// Edit Vacancy Record
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>View of Edit News</returns>
        public IActionResult Edit(int VacancyId, int SelectedDepartmentId)
        {
            try
            {
                VacancyVM vacancy = _vacancyService.GetVacancyById(VacancyId);
                ViewBag.SelectedDepartmentId = SelectedDepartmentId;
                return View(vacancy);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("Error");
            }

        }

        /// <summary>
        /// Post Update vacancy to update existing vacancy
        /// </summary>
        /// <param name="user">Edit VacancyVM data structure model</param>
        /// <returns>Success or Failure</returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult Update(VacancyVM vacancy, UserActionLoggingDetails loggingDetails)
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
                    if (vacancy.ThumbnailImg != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            vacancy.ThumbnailImg.CopyTo(memoryStream);
                            vacancy.ThumbnailImageArray = memoryStream.ToArray();
                            //string imageName = Guid.NewGuid() + news.ThumbnailImg.FileName.Trim().Replace(" ", String.Empty);
                            string imageName = Guid.NewGuid() + Path.GetFileName(vacancy.ThumbnailImg.FileName).Trim().Replace(" ", String.Empty);
                            string imagePath = _pathProvider.MapPath("") + @"\Uploads\Images\Vacancy\" + imageName;
                            System.IO.File.WriteAllBytes(imagePath, vacancy.ThumbnailImageArray);
                            vacancy.ThumbnailImage = imageName;
                        }
                    }

                    if (vacancy.MainImg != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            vacancy.MainImg.CopyTo(memoryStream);
                            vacancy.MainImageArray = memoryStream.ToArray();
                            //string imageName = Guid.NewGuid() + news.MainImg.FileName.Trim().Replace(" ", String.Empty);
                            string imageName = Guid.NewGuid() + Path.GetFileName(vacancy.MainImg.FileName).Trim().Replace(" ", String.Empty);
                            string imagePath = _pathProvider.MapPath("") + @"\Uploads\Images\Vacancy\" + imageName;
                            System.IO.File.WriteAllBytes(imagePath, vacancy.MainImageArray);
                            vacancy.MainImage = imageName;
                        }
                    }
                    if (vacancy.AdditionalImg1 != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            vacancy.AdditionalImg1.CopyTo(memoryStream);
                            vacancy.AdditionalImage1Array = memoryStream.ToArray();
                            //string imageName = Guid.NewGuid() + news.AdditionalImg1.FileName.Trim().Replace(" ", String.Empty);
                            string imageName = Guid.NewGuid() + Path.GetFileName(vacancy.AdditionalImg1.FileName).Trim().Replace(" ", String.Empty);
                            string imagePath = _pathProvider.MapPath("") + @"\Uploads\Images\Vacancy\" + imageName;
                            System.IO.File.WriteAllBytes(imagePath, vacancy.AdditionalImage1Array);
                            vacancy.AdditionalImage1 = imageName;
                        }
                    }

                    if (vacancy.AdditionalImg2 != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            vacancy.AdditionalImg2.CopyTo(memoryStream);
                            vacancy.AdditionalImage2Array = memoryStream.ToArray();
                            //string imageName = Guid.NewGuid() + news.AdditionalImg2.FileName.Trim().Replace(" ", String.Empty);
                            string imageName = Guid.NewGuid() + Path.GetFileName(vacancy.AdditionalImg2.FileName).Trim().Replace(" ", String.Empty);
                            string imagePath = _pathProvider.MapPath("") + @"\Uploads\Images\Vacancy\" + imageName;
                            System.IO.File.WriteAllBytes(imagePath, vacancy.AdditionalImage2Array);
                            vacancy.AdditionalImage2 = imageName;
                        }

                    }
                    vacancy.IsActive = 1;
                    vacancy.ModifiedBy = Convert.ToInt32(loggingDetails.UserId);
                    result = _vacancyService.UpdateVacancy(vacancy);
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
        /// Delete Vacancy  
        /// </summary>
        /// <param name="allUserIds">List of Vacancies IDs</param>
        /// <returns>Success/Failuare as Baseresult</returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult DeleteVacancy(DeleteItemVM targetIds, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = new BaseResult();
            targetIds.DeletedBy = Convert.ToInt32(loggingDetails.UserId);

            try
            {
                result = _vacancyService.DeleteVacancyByIds(targetIds);
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