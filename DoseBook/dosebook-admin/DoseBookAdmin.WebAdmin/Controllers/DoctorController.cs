using DoseBookAdmin.Common.Utility;
using DoseBookAdmin.Dto.Doctor;
using DoseBookAdmin.Dto.Global;
using DoseBookAdmin.WebAdmin.Doctor.Af;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using EventLevel = DoseBookAdmin.Dto.Global.EventLevel;

namespace DoseBookAdmin.WebAdmin.Controllers
{
    public class DoctorController : Controller
    {
        /// <summary>
        /// Private IDoctorAf Data Member
        /// </summary>
        private IDoctorAf _doctorAf;

        /// <summary>
        /// Private ILogger Data Member
        /// </summary>
        private ILogger<DoctorController> _logger;

        public DoctorController(IDoctorAf doctorAf, ILogger<DoctorController> logger)
        {
            _doctorAf = doctorAf;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_doctorAf.GetAllDoctors());
        }

        
        public JsonResult DoctorNameAutoComplete(string prefix)
        {
            List<string> adviceDescriptionList = _doctorAf.GetSearchedDoctorNameList(prefix);
            return Json(adviceDescriptionList);
        }

        
        public JsonResult ValidateDoctorNameExistOrNot(string doctorName, int id)
        {
            bool isExist = _doctorAf.IsDoctorNameExist(doctorName, id);
            if (isExist)
                return Json(data: "The doctor name exists, please enter an alternative.");

            return Json(data: true);
        }

        [HttpPost]
        
        public JsonResult AddDoctorList(List<DoctorDto> doctorList)
        {
            BaseResultDto result;
            try
            {
                result = _doctorAf.SaveDoctorList(doctorList);
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
        
        public JsonResult UpdateDoctor(DoctorDto doctor)
        {
            BaseResultDto result;
            try
            {
                result = _doctorAf.UpdateDoctor(doctor);
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
        public IActionResult DeleteDoctor(DeleteItemDto targetIds)
        {
            BaseResultDto result;
            try
            {
                result = _doctorAf.DeleteDoctorByIds(targetIds);
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
