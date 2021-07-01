using DoseBookAdmin.Core.DomainServices;
using DoseBookAdmin.ViewModels.ClassificationData;
using DoseBookAdmin.ViewModels.Global;
using DoseBookAdmin.WebAdmin.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace DoseBookAdmin.WebAdmin.Controllers
{
    public class ClassificationResultController : Controller
    {
        /// <summary>
        /// IClassificationResultService data member
        /// </summary>
        private IClassificationResultService _classificationResultService;


        /// <summary>
        /// Ilogger Data Member
        /// </summary>
        private ILogger<ClassificationResultController> _logger;

        /// <summary>
        /// DoctorController constructor
        /// </summary>
        /// <param name="classificationResultService"></param>
        public ClassificationResultController(IClassificationResultService classificationResultService, ILogger<ClassificationResultController> logger)
        {
            _classificationResultService = classificationResultService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_classificationResultService.GetAllDoctors());
        }

        public ActionResult GetClassificationResultByDoctorWise(int id)
        {
            ClassificationResultListGridItemVM doseDataListGridItemVM = _classificationResultService.GetClassificationResultByDoctorWise(id);
            return PartialView("_ClassificationResultList", doseDataListGridItemVM);
        }

        /// <summary>
        /// Get Add Medicine view 
        /// </summary>
        /// <returns>Add Medicine view</returns>
        public IActionResult AddClassificationResult(int DoctorId)
        {
            try
            {
                ViewBag.DoctorId = DoctorId;
                ClassificationResult medicineDose = _classificationResultService.GetClassificationResultDataToCreateClassificationResult();
                medicineDose.DoctorId = DoctorId;
                return View(medicineDose);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }
        }

        public ActionResult GetClassificationResultData(int doctorId, string classificationTypeName)
        {
            if (classificationTypeName == "TEST")
            {
                List<DiagnosticTest> AllDiagnosticTests = _classificationResultService.GetDiagnosticTestByDoctorId(doctorId).AllDiagnosticTests;
                return Json(AllDiagnosticTests);
            }
            else if (classificationTypeName == "MEDICINE")
            {
                List<MedicineDose> AllMedicineDoses = _classificationResultService.GetMedicineDoseByDoctorId(doctorId).AllMedicineDoses;
                return Json(AllMedicineDoses);
            }
            else if (classificationTypeName == "MISC")
            {
                List<MiscSuggestion> AllMiscSuggestions = _classificationResultService.GetMiscSuggestionByDoctorId(doctorId).AllMiscSuggestions;
                return Json(AllMiscSuggestions);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Post Add doctor to save it
        /// </summary>
        /// <param name="classificationResult">ClassificationResult data structure model</param>
        /// <returns>Success or Failure</returns>
        [HttpPost]
        public IActionResult AddClassificationResult([Bind] ClassificationResult classificationResult)
        {
            BaseResult result = null;
            try
            {
                if (ModelState.IsValid)
                {
                    if (classificationResult.ClassificationTypeId == 1)
                    {
                        classificationResult.ClassificationTypeName = "TEST";
                        classificationResult.ClassificationName = "Diagnostic Test";
                    }
                    else if (classificationResult.ClassificationTypeId == 2)
                    {
                        classificationResult.ClassificationTypeName = "MEDICINE";
                        classificationResult.ClassificationName = "Medicine Dose";
                    }
                    else if (classificationResult.ClassificationTypeId == 3)
                    {
                        classificationResult.ClassificationTypeName = "MISC";
                        classificationResult.ClassificationName = "Misc Suggesstion";
                    }



                    result = _classificationResultService.SaveClassificationResult(classificationResult);
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
        /// Edit Classification Result Record
        /// </summary>
        /// <param name="id">Classification ResultId Id</param>
        /// <returns>View of Edit Classification Result</returns>
        public IActionResult EditClassificationResult(int ClassificationResultId, int SelectedDoctorId)
        {
            try
            {
                ClassificationResult classificationResult = _classificationResultService.GetClassificationResultById(ClassificationResultId);
                ViewBag.DoctorId = classificationResult.DoctorId;
                return View(classificationResult);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }
        }

        /// <summary>
        /// Update ClassificationResult
        /// </summary>
        /// <param name="classificationResult">ClassificationResult that will be updated</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditClassificationResult(ClassificationResult classificationResult)
        {
            BaseResult result = null;

            try
            {
                if (ModelState.IsValid)
                {
                    result = _classificationResultService.UpdateClassificationResult(classificationResult);
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
        /// Delete ClassificationResult 
        /// </summary>
        /// <param name="targetIds">List of Classification Result IDs</param>
        /// <returns>Success/Failuare as Baseresult</returns>
        [HttpPost]
        public IActionResult DeleteClassificationResult(DeleteItemVM targetIds)
        {
            BaseResult result = new BaseResult();
            try
            {
                result = _classificationResultService.DeleteClassificationResultByIds(targetIds);
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
