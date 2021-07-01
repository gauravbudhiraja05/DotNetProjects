using DoseBookAdmin.Common.Utility;
using DoseBookAdmin.Dto.Global;
using DoseBookAdmin.Dto.MedicineDose;
using DoseBookAdmin.WebAdmin.Doctor.Af;
using DoseBookAdmin.WebAdmin.MedicineDose.Af;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using EventLevel = DoseBookAdmin.Dto.Global.EventLevel;

namespace DoseBookAdmin.WebAdmin.Controllers
{
    public class MedicineDoseController : Controller
    {
        /// <summary>
        /// Private IMedicineDoseAf Data Member
        /// </summary>
        private IMedicineDoseAf _medicineDoseAf;

        /// <summary>
        /// Private IDoctorAf Data Member
        /// </summary>
        private IDoctorAf _doctorAf;

        /// <summary>
        /// Private ILogger Data Member
        /// </summary>
        private ILogger<MedicineDoseController> _logger;

        public MedicineDoseController(IMedicineDoseAf medicineDoseAf, IDoctorAf doctorAf, ILogger<MedicineDoseController> logger)
        {
            _medicineDoseAf = medicineDoseAf;
            _doctorAf = doctorAf;
            _logger = logger;
        }

        public IActionResult MasterMedicineDose()
        {
            return View(_medicineDoseAf.GetAllMedicineDoses());
        }

        public IActionResult DoctorMedicineDose(int doctorId)
        {
            TempData["DoctorId"] = doctorId;
            return View(_doctorAf.GetAllDoctors());
        }

        public ActionResult GetMedicineDoseListByDoctorWise(int id)
        {
            int doctorId = Convert.ToInt32(TempData["DoctorId"]);
            if (doctorId != 0 && id == 0)
                id = doctorId;

            MedicineDoseListGridItemDto medicineDoseListGridItemDto = _medicineDoseAf.GetMedicineDoseListByDoctorWise(id);
            return PartialView("_DoctorMedicineDoseList", medicineDoseListGridItemDto);
        }

        public JsonResult AddMasterMedicineDose()
        {
            MedicineDoseSimulationDto medicineDoseSimulationDto = _medicineDoseAf.GetMedicineDoseDataToCreateMasterMedicineDose();
            return Json(medicineDoseSimulationDto);
        }

        
        public JsonResult MedicineDoseAutoComplete(string prefix)
        {
            List<string> medicineDoseMedicineNameList = _medicineDoseAf.GetSearchedMedicineDoseList(prefix);
            return Json(medicineDoseMedicineNameList);
        }

        
        public JsonResult ValidateMedicineExistOrNot(string medicineName, string doctorId, int medicineId)
        {
            if (doctorId == "undefined") doctorId = "0";

            bool isExist = _medicineDoseAf.IsMedicineExist(medicineName, Convert.ToInt32(doctorId), Convert.ToInt32(medicineId));
            if (isExist)
                return Json(data: "The medicine name already exists, please enter an alternative.");

            return Json(data: true);
        }

        public JsonResult GetMedicineDetailByMedicineName(string medicineName, string type)
        {
            MedicineDoseDto medicineDoseDto = _medicineDoseAf.GetMedicineDetailByMedicineName(medicineName, type);
            return Json(medicineDoseDto);
        }

        [HttpPost]
        public JsonResult AddMasterMedicineDose(MedicineDoseDto masterMedicineDose)
        {
            BaseResultDto result;
            try
            {
                result = _medicineDoseAf.SaveMasterMedicineDose(masterMedicineDose);
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

        public JsonResult EditMasterMedicineDose(int medicineId)
        {
            MedicineDoseSimulationDto medicineDoseSimulationDto = _medicineDoseAf.GetMasterMedicineDoseById(medicineId);
            return Json(medicineDoseSimulationDto);
        }

        [HttpPost]
        public JsonResult EditMasterMedicineDose(MedicineDoseDto masterMedicineDose)
        {
            BaseResultDto result;
            try
            {
                result = _medicineDoseAf.UpdateMasterMedicineDose(masterMedicineDose);
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
        public IActionResult DeleteMasterMedicineDose(DeleteItemDto deleteItemDto)
        {
            BaseResultDto result;
            try
            {
                result = _medicineDoseAf.DeleteMasterMedicineDoseByIds(deleteItemDto);
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

        
        public JsonResult DoctorProblemTags()
        {
            string medicineDoseMedicineNameList = _medicineDoseAf.GetSearchedDoctorProblemTagsList();
            return Json(medicineDoseMedicineNameList);
        }

        public JsonResult AddDoctorMedicineDose(int doctorId)
        {
            ViewBag.DoctorId = doctorId;
            MedicineDoseSimulationDto medicineDoseSimulationDto = _medicineDoseAf.GetMedicineDoseDataToCreateDoctorMedicineDose();
            medicineDoseSimulationDto.MedicineDoseDto.DoctorId = doctorId;
            return Json(medicineDoseSimulationDto);
        }

        [HttpPost]
        public JsonResult AddDoctorMedicineDose(MedicineDoseDto doctorMedicineDose)
        {
            BaseResultDto result;
            try
            {
                result = _medicineDoseAf.SaveDoctorMedicineDose(doctorMedicineDose);
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

        public JsonResult EditDoctorMedicineDose(int medicineId)
        {
            MedicineDoseSimulationDto medicineDoseSimulationDto = _medicineDoseAf.GetDoctorMedicineDoseById(medicineId);
            return Json(medicineDoseSimulationDto);
        }

        [HttpPost]
        public JsonResult EditDoctorMedicineDose(MedicineDoseDto doctorMedicineDose)
        {
            BaseResultDto result;
            try
            {
                result = _medicineDoseAf.UpdateDoctorMedicineDose(doctorMedicineDose);
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
        public IActionResult DeleteDoctorMedicineDose(DeleteItemDto deleteItemDto)
        {
            BaseResultDto result;
            try
            {
                result = _medicineDoseAf.DeleteDoctorMedicineDoseByIds(deleteItemDto);
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