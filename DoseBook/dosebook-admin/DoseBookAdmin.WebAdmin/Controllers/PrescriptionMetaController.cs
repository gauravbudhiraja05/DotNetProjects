using DoseBookAdmin.Common.Utility;
using DoseBookAdmin.Dto.Global;
using DoseBookAdmin.Dto.PrescriptionMeta;
using DoseBookAdmin.WebAdmin.PrescriptionMeta.Af;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using EventLevel = DoseBookAdmin.Dto.Global.EventLevel;

namespace DoseBookAdmin.WebAdmin.Controllers
{
    public class PrescriptionMetaController : Controller
    {
        /// <summary>
        /// Private IPrescriptionMetaAf Meta Member
        /// </summary>
        private IPrescriptionMetaAf _prescriptionMetaAf;

        /// <summary>
        /// Private ILogger Meta Member
        /// </summary>
        private ILogger<PrescriptionMetaController> _logger;

        public PrescriptionMetaController(IPrescriptionMetaAf prescriptionMetaAf, ILogger<PrescriptionMetaController> logger)
        {
            _prescriptionMetaAf = prescriptionMetaAf;
            _logger = logger;
        }

        public IActionResult Index(string prescriptionMetaDataType)
        {
            TempData["PrescriptionMetaDataType"] = prescriptionMetaDataType;
            return View(_prescriptionMetaAf.GetAllPrescriptionMetaTypes());
        }
        
        public ActionResult GetPrescriptionMetaDataByPrescriptionMetaTypeWise(string prescriptionMetaDataType)
        {
            string prescriptionType = Convert.ToString(TempData["PrescriptionMetaDataType"]);
            if (prescriptionMetaDataType.Trim() == "Select PrescriptionType" && !string.IsNullOrEmpty(prescriptionType))
                prescriptionMetaDataType = prescriptionType;

            PrescriptionMetaDataListGridItemDto prescriptionMetaDataListGridItemDto = _prescriptionMetaAf.GetPrescriptionMetaDataByPrescriptionMetaTypeWise(prescriptionMetaDataType.Trim());
            return PartialView("_PrescriptionMetaDataList", prescriptionMetaDataListGridItemDto);
        }

        [HttpPost]
        
        public JsonResult AddPrescriptionMetaDataList(List<PrescriptionMetaDataDto> prescriptionMetaDataList)
        {
            BaseResultDto result = _prescriptionMetaAf.SavePrescriptionMetaDataList(prescriptionMetaDataList);
            return Json(result);
        }

        [HttpPost]
        public JsonResult UpdatePrescriptionMetaData(PrescriptionMetaDataDto prescriptionMetaData)
        {
            BaseResultDto result;
            try
            {
                result = _prescriptionMetaAf.UpdatePrescriptionMetaData(prescriptionMetaData);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResultDto();
                result.Message = StaticResource.InternalServerMessage;
            }

            return Json(result);
        }

        public IActionResult DeletePrescriptionMetaDataByIds(DeleteItemDto deleteItemDto)
        {
            BaseResultDto result;
            try
            {
                result = _prescriptionMetaAf.DeletePrescriptionMetaDataByIds(deleteItemDto);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResultDto();
                result.Message = StaticResource.InternalServerMessage;
            }

            return Ok(result);
        }

        public ActionResult PrescriptionMetaType()
        {
            return View(_prescriptionMetaAf.GetAllPrescriptionMetaTypes());
        }

        
        public JsonResult TypeAutoComplete(string prefix)
        {
            List<string> prescriptionMetaTypeList = _prescriptionMetaAf.GetSearchedTypeList(prefix);
            return Json(prescriptionMetaTypeList);
        }

        
        public JsonResult ValidateTypeExistOrNot(string type, int id)
        {
            bool isExist = _prescriptionMetaAf.IsTypeExist(type, id);
            if (isExist)
                return Json(data: "The prescription type already exists, please enter an alternative.");

            return Json(data: true);
        }

        [HttpPost]
        
        public JsonResult AddPrescriptionMetaTypeList(List<PrescriptionMetaTypeDto> prescriptionMetaTypeList)
        {
            BaseResultDto result;
            try
            {
                result = _prescriptionMetaAf.SavePrescriptionMetaTypeList(prescriptionMetaTypeList);
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
        
        public JsonResult UpdatePrescriptionMetaType(PrescriptionMetaTypeDto prescriptionMetaType)
        {
            BaseResultDto result;
            try
            {
                result = _prescriptionMetaAf.UpdatePrescriptionMetaType(prescriptionMetaType);
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

        
        public ActionResult DeletePrescriptionMetaType(DeleteItemDto targetIds)
        {
            BaseResultDto result;
            try
            {
                result = _prescriptionMetaAf.DeletePrescriptionMetaTypeByIds(targetIds);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResultDto();
                result.Message = StaticResource.InternalServerMessage;
            }

            return Ok(result);
        }

    }
}