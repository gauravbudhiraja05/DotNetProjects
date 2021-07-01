using DoseBookAdmin.Common.Utility;
using DoseBookAdmin.Dto.Global;
using DoseBookAdmin.Dto.Test;
using DoseBookAdmin.WebAdmin.Doctor.Af;
using DoseBookAdmin.WebAdmin.Test.Af;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using EventLevel = DoseBookAdmin.Dto.Global.EventLevel;

namespace DoseBookAdmin.WebAdmin.Controllers
{
    public class TestController : Controller
    {
        /// <summary>
        /// Private ITestAf Data Member
        /// </summary>
        private ITestAf _testAf;

        /// <summary>
        /// Private IDoctorAf Data Member
        /// </summary>
        private IDoctorAf _doctorAf;

        /// <summary>
        /// Private ILogger Data Member
        /// </summary>
        private ILogger<TestController> _logger;

        public TestController(ITestAf testAf, IDoctorAf doctorAf, ILogger<TestController> logger)
        {
            _testAf = testAf;
            _doctorAf = doctorAf;
            _logger = logger;
        }

        public IActionResult MasterTest()
        {
            return View(_testAf.GetAllTests());
        }

        public IActionResult DoctorTest(int doctorId)
        {
            TempData["DoctorId"] = doctorId;
            return View(_doctorAf.GetAllDoctors());
        }

        public ActionResult GetTestListByDoctorWise(int id)
        {
            int doctorId = Convert.ToInt32(TempData["DoctorId"]);
            if (doctorId != 0 && id == 0)
                id = doctorId;

            TestListGridItemDto testListGridItemVM = _testAf.GetTestListByDoctorWise(id);
            return PartialView("_DoctorTestList", testListGridItemVM);
        }

        
        public JsonResult DoctorProblemTags()
        {
            string adviceDescriptionList = _testAf.GetSearchedDoctorProblemTagsList();
            return Json(adviceDescriptionList);
        }

        
        public JsonResult TestAutoComplete(string prefix)
        {
            List<string> adviceDescriptionList = _testAf.GetSearchedTestList(prefix);
            return Json(adviceDescriptionList);
        }

        [AllowAnonymous]
        public JsonResult ValidateTestExistOrNot(string test, string doctorId, int id)
        {
            if (doctorId == "undefined") doctorId = "0";

            bool isExist = _testAf.IsTestExist(test, Convert.ToInt32(doctorId), Convert.ToInt32(id));
            if (isExist)
                return Json(data: "The test already exists, please enter an alternative.");

            return Json(data: true);
        }

        [HttpPost]
        public JsonResult AddDoctorTestList(List<TestDto> doctorTestList)
        {
            BaseResultDto result;
            try
            {
                result = _testAf.SaveDoctorTestList(doctorTestList);
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
        public JsonResult AddMasterTestList(List<TestDto> masterTestList)
        {
            BaseResultDto result;
            try
            {
                result = _testAf.SaveMasterTestList(masterTestList);
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
        
        public JsonResult UpdateDoctorTest(TestDto doctorTest)
        {
            BaseResultDto result;
            try
            {
                result = _testAf.UpdateDoctorTest(doctorTest);
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
        
        public JsonResult UpdateMasterTest(TestDto masterTest)
        {
            BaseResultDto result;
            try
            {
                result = _testAf.UpdateMasterTest(masterTest);
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
        public IActionResult DeleteMasterTest(DeleteItemDto deleteItemDto)
        {
            BaseResultDto result = new BaseResultDto();
            try
            {
                result = _testAf.DeleteMasterTestByIds(deleteItemDto);
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
        public IActionResult DeleteDoctorTest(DeleteItemDto targetIds)
        {
            BaseResultDto result = new BaseResultDto();
            try
            {
                result = _testAf.DeleteDoctorTestByIds(targetIds);
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