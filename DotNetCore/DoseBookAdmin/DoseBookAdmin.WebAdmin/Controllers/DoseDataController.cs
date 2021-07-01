using DoseBookAdmin.Core.DomainServices;
using DoseBookAdmin.ViewModels.DoseData;
using DoseBookAdmin.ViewModels.Global;
using DoseBookAdmin.WebAdmin.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DoseBookAdmin.WebAdmin.Controllers
{
    public class DoseDataController : Controller
    {
        /// <summary>
        /// IDoseDataService data member
        /// </summary>
        private IDoseDataService _doseDataService;

        /// <summary>
        /// Ilogger Data Member
        /// </summary>
        private ILogger<DoseDataController> _logger;


        /// <summary>
        /// DoctorController constructor
        /// </summary>
        /// <param name="doseDataService"></param>
        public DoseDataController(IDoseDataService doseDataService, ILogger<DoseDataController> logger)
        {
            _doseDataService = doseDataService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_doseDataService.GetAllDoseMetaTypes());
        }

        public ActionResult GetDoseDataByDoseMetaTypeWise(int id)
        {
            DoseMetaTypeListGridItemVM doseDataListGridItemVM = _doseDataService.GetDoseDataByDoseMetaTypeWise(id);
            return PartialView("_DoseDataList", doseDataListGridItemVM);
        }

        /// <summary>
        /// Get Add Dose Data view action
        /// </summary>
        /// <returns>Add Dose Data view</returns>
        public IActionResult Add(int DoseMetaTypeId)
        {
            try
            {
                ViewBag.DoseMetaTypeId = DoseMetaTypeId;
                return View(_doseDataService.GetDoseDataToCreateDoseData());
            }

            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }
        }

        /// <summary>
        /// Post Add DoseData to save it
        /// </summary>
        /// <param name="user">AddDoseDataVM data structure model</param>
        /// <returns>Success or Failure</returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Add([Bind] DoseData doseData)
        {
            BaseResult result = null;

            try
            {
                if (ModelState.IsValid)
                {
                    doseData.IsActive = true;
                    result = _doseDataService.SaveDoseData(doseData);
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
        public IActionResult Edit(int DoseDataId, int SelectedDoseMetaTypeId)
        {
            try
            {
                DoseData doseData = _doseDataService.GetDoseDataById(DoseDataId);
                ViewBag.SelectedDoseMetaTypeId = SelectedDoseMetaTypeId;
                return View(doseData);
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
        [AllowAnonymous]
        public IActionResult Edit(DoseData doseData)
        {
            BaseResult result = null;
            try
            {
                if (ModelState.IsValid)
                {
                    result = _doseDataService.UpdateDoseData(doseData);
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

        public IActionResult DeleteDoseDataByIds(DeleteItemVM targetIds)
        {
            BaseResult result = new BaseResult();
            try
            {
                result = _doseDataService.DeleteDoseDataByIds(targetIds);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
            }

            return Ok(result);
        }


    }
}