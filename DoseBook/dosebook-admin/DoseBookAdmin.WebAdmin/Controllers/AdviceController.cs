using DoseBookAdmin.Common.Utility;
using DoseBookAdmin.Dto.Advice;
using DoseBookAdmin.Dto.Global;
using DoseBookAdmin.WebAdmin.Advice.Af;
using DoseBookAdmin.WebAdmin.Doctor.Af;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using EventLevel = DoseBookAdmin.Dto.Global.EventLevel;

namespace DoseBookAdmin.WebAdmin.Controllers
{
    public class AdviceController : Controller
    {
        /// <summary>
        /// Private IAdviceAf Data Member
        /// </summary>
        private IAdviceAf _adviceAf;

        /// <summary>
        /// Private IDoctorAf Data Member
        /// </summary>
        private IDoctorAf _doctorAf;

        /// <summary>
        /// Private ILogger Data Member
        /// </summary>
        private ILogger<AdviceController> _logger;

        public AdviceController(IAdviceAf adviceAf, IDoctorAf doctorAf, ILogger<AdviceController> logger)
        {
            _adviceAf = adviceAf;
            _doctorAf = doctorAf;
            _logger = logger;
        }

        public IActionResult MasterAdvice()
        {
            return View(_adviceAf.GetAllAdvices());
        }

        public IActionResult DoctorAdvice(int doctorId)
        {
            TempData["DoctorId"] = doctorId;
            return View(_doctorAf.GetAllDoctors());
        }

        public ActionResult GetAdviceListByDoctorWise(int id)
        {
            int doctorId = Convert.ToInt32(TempData["DoctorId"]);
            if (doctorId != 0 && id == 0)
                id = doctorId;

            AdviceListGridItemDto adviceListGridItemVM = _adviceAf.GetAdviceListByDoctorWise(id);
            return PartialView("_DoctorAdviceList", adviceListGridItemVM);
        }

        
        public JsonResult DoctorProblemTags()
        {
            string adviceDescriptionList = _adviceAf.GetSearchedDoctorProblemTagsList();
            return Json(adviceDescriptionList);
        }

        
        public JsonResult AdviceAutoComplete(string prefix)
        {
            List<string> adviceDescriptionList = _adviceAf.GetSearchedAdviceList(prefix);
            return Json(adviceDescriptionList);
        }

        
        public JsonResult ValidateAdviceExistOrNot(string description, string doctorId, int id)
        {
            if (doctorId == "undefined") doctorId = "0";

            bool isExist = _adviceAf.IsAdviceExist(description, Convert.ToInt32(doctorId), id);
            if (isExist)
                return Json(data: "The advice already exists, please enter an alternative.");

            return Json(data: true);
        }

        [HttpPost]
        
        public JsonResult AddDoctorAdviceList(List<AdviceDto> doctorAdviceList)
        {
            BaseResultDto result;
            try
            {
                result = _adviceAf.SaveDoctorAdviceList(doctorAdviceList);
                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResultDto();
                result.Message = StaticResource.InternalServerMessage;
                return Json(result);
            }
        }

        [HttpPost]
        
        public JsonResult AddMasterAdviceList(List<AdviceDto> masterAdviceList)
        {
            BaseResultDto result;
            try
            {
                result = _adviceAf.SaveMasterAdviceList(masterAdviceList);
                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResultDto();
                result.Message = StaticResource.InternalServerMessage;
                return Json(result);
            }
        }

        [HttpPost]
        
        public JsonResult UpdateDoctorAdvice(AdviceDto doctorAdvice)
        {
            BaseResultDto result;
            try
            {
                result = _adviceAf.UpdateDoctorAdvice(doctorAdvice);
                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResultDto();
                result.Message = StaticResource.InternalServerMessage;
                return Json(result);
            }
        }

        [HttpPost]
        
        public JsonResult UpdateMasterAdvice(AdviceDto masterAdvice)
        {
            BaseResultDto result;
            try
            {
                result = _adviceAf.UpdateMasterAdvice(masterAdvice);
                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResultDto();
                result.Message = StaticResource.InternalServerMessage;
                return Json(result);
            }
        }

        [HttpPost]
        public IActionResult DeleteMasterAdvice(DeleteItemDto deleteItemDto)
        {
            BaseResultDto result;
            try
            {
                result = _adviceAf.DeleteMasterAdviceByIds(deleteItemDto);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResultDto();
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult DeleteDoctorAdvice(DeleteItemDto deleteItemDto)
        {
            BaseResultDto result;
            try
            {
                result = _adviceAf.DeleteDoctorAdviceByIds(deleteItemDto);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResultDto();
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }

            return Ok(result);
        }
    }
}
