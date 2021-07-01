using DoseBookAdmin.Core.DomainServices;
using DoseBookAdmin.ViewModels.ClassificationData;
using DoseBookAdmin.ViewModels.Global;
using DoseBookAdmin.WebAdmin.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DoseBookAdmin.WebAdmin.Controllers
{
    public class MiscSuggestionController : Controller
    {
        
        /// <summary>
        /// IMiscSuggestionService data member
        /// </summary>
        private IMiscSuggestionService _miscSuggestionService;

        /// <summary>
        /// Ilogger Data Member
        /// </summary>
        private ILogger<MiscSuggestionController> _logger;

        /// <summary>
        /// MedicineController Constructor
        /// </summary>
        /// <param name="miscSuggestionService">IMedicineService object reference</param>
        public MiscSuggestionController(IMiscSuggestionService miscSuggestionService, ILogger<MiscSuggestionController> logger)
        {
            _miscSuggestionService = miscSuggestionService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_miscSuggestionService.GetAllDoctors());
        }

        public ActionResult GetMiscSuggestionByDoctorWise(int id)
        {
            MiscSuggestionListGridItemVM miscSuggestionListGridItemVM = _miscSuggestionService.GetMiscSuggestionByDoctorWise(id);
            return PartialView("_MiscSuggestionList", miscSuggestionListGridItemVM);
        }

        /// <summary>
        /// Get Add Medicine view 
        /// </summary>
        /// <returns>Add Medicine view</returns>
        public IActionResult AddMiscSuggestion(int DoctorId)
        {
            try
            {
                ViewBag.DoctorId = DoctorId;
                MiscSuggestion miscSuggestion = _miscSuggestionService.GetMiscSuggestionDataToCreateMiscSuggestion();
                miscSuggestion.DoctorId = DoctorId;
                return View(miscSuggestion);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }
        }

        /// <summary>
        /// Post Add MiscSuggestion to save it
        /// </summary>
        /// <param name="miscSuggestion">AddDoctorVM data structure model</param>
        /// <returns>Success or Failure</returns>
        [HttpPost]
        public IActionResult AddMiscSuggestion([Bind] MiscSuggestion miscSuggestion)
        {
            BaseResult result = null;
            try
            {
                if (ModelState.IsValid)
                {
                    result = _miscSuggestionService.SaveMiscSuggestion(miscSuggestion);
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
        public IActionResult EditMiscSuggestion(int TestId)
        {
            try
            {
                MiscSuggestion miscSuggestion = _miscSuggestionService.GetMiscSuggestionById(TestId);
                return View(miscSuggestion);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }
        }

        /// <summary>
        /// Update MiscSuggestion
        /// </summary>
        /// <param name="miscSuggestion">Doctor that will be updated</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditMiscSuggestion(MiscSuggestion miscSuggestion)
        {
            BaseResult result = null;

            try
            {
                if (ModelState.IsValid)
                {
                    //miscSuggestion.ModifiedBy = _loggedInUserID;
                    result = _miscSuggestionService.UpdateMiscSuggestion(miscSuggestion);
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
        /// Delete MiscSuggestion 
        /// </summary>
        /// <param name="targetIds">List of End User IDs</param>
        /// <returns>Success/Failuare as Baseresult</returns>
        [HttpPost]
        public IActionResult DeleteMiscSuggestion(DeleteItemVM targetIds)
        {
            BaseResult result = new BaseResult();
            try
            {
                result = _miscSuggestionService.DeleteMiscSuggestionByIds(targetIds);
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
