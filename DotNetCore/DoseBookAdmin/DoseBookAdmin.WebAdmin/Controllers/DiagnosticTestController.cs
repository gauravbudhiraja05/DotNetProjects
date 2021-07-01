using DoseBookAdmin.Core.DomainServices;
using DoseBookAdmin.ViewModels.ClassificationData;
using DoseBookAdmin.ViewModels.Global;
using DoseBookAdmin.WebAdmin.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DoseBookAdmin.WebAdmin.Controllers
{
    public class DiagnosticTestController : Controller
    {
        /// <summary>
        /// IDiagnosticTestService data member
        /// </summary>
        private IDiagnosticTestService _diagnosticTestService;

        /// <summary>
        /// Ilogger Data Member
        /// </summary>
        private ILogger<DiagnosticTestController> _logger;

        /// <summary>
        /// MedicineController Constructor
        /// </summary>
        /// <param name="diagnosticTestService">IMedicineService object reference</param>
        public DiagnosticTestController(IDiagnosticTestService diagnosticTestService, ILogger<DiagnosticTestController> logger)
        {
            _diagnosticTestService = diagnosticTestService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_diagnosticTestService.GetAllDoctors());
        }

        public ActionResult GetDiagnosticTestByDoctorWise(int id)
        {
            DiagnosticTestListGridItemVM diagnosticTestListGridItemVM = _diagnosticTestService.GetDiagnosticTestByDoctorWise(id);
            return PartialView("_DiagnosticTestList", diagnosticTestListGridItemVM);
        }

        /// <summary>
        /// Get Add Medicine view 
        /// </summary>
        /// <returns>Add Medicine view</returns>
        public IActionResult AddDiagnosticTest(int DoctorId)
        {
            try
            {
                ViewBag.DoctorId = DoctorId;
                DiagnosticTest diagnosticTest = _diagnosticTestService.GetDiagnosticTestDataToCreateDiagnosticTest(); ;
                diagnosticTest.DoctorId = DoctorId;
                return View(diagnosticTest);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }
        }

        /// <summary>
        /// Post Add DiagnosticTest to save it
        /// </summary>
        /// <param name="diagnosticTest">AddDoctorVM data structure model</param>
        /// <returns>Success or Failure</returns>
        [HttpPost]
        public IActionResult AddDiagnosticTest([Bind] DiagnosticTest diagnosticTest)
        {
            BaseResult result = null;
            try
            {
                if (ModelState.IsValid)
                {
                    result = _diagnosticTestService.SaveDiagnosticTest(diagnosticTest);
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
        /// Edit Medicine Dose Record
        /// </summary>
        /// <param name="id">Medicine Dose Id</param>
        /// <returns>View of Edit Medicine Dose</returns>
        public IActionResult EditDiagnosticTest(int TestId)
        {
            try
            {
                DiagnosticTest diagnosticTest = _diagnosticTestService.GetDiagnosticTestById(TestId);
                return View(diagnosticTest);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }
        }

        /// <summary>
        /// Update DiagnosticTest
        /// </summary>
        /// <param name="diagnosticTest">Doctor that will be updated</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditDiagnosticTest(DiagnosticTest diagnosticTest)
        {
            BaseResult result = null;

            try
            {
                if (ModelState.IsValid)
                {
                    //diagnosticTest.ModifiedBy = _loggedInUserID;
                    result = _diagnosticTestService.UpdateDiagnosticTest(diagnosticTest);
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
        /// Delete DiagnosticTest 
        /// </summary>
        /// <param name="targetIds">List of End User IDs</param>
        /// <returns>Success/Failuare as Baseresult</returns>
        [HttpPost]
        public IActionResult DeleteDiagnosticTest(DeleteItemVM targetIds)
        {
            BaseResult result = new BaseResult();
            try
            {
                result = _diagnosticTestService.DeleteDiagnosticTestByIds(targetIds);
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
