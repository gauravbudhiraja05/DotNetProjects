using DoseBookAdmin.Core.DomainServices;
using DoseBookAdmin.ViewModels.ClassificationData;
using DoseBookAdmin.ViewModels.Global;
using DoseBookAdmin.WebAdmin.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DoseBookAdmin.WebAdmin.Controllers
{
    public class MedicineDoseController : Controller
    {
        /// <summary>
        /// IMedicineDoseService data member
        /// </summary>
        private IMedicineDoseService _medicineDoseService;

        /// <summary>
        /// Ilogger Data Member
        /// </summary>
        private ILogger<MedicineDoseController> _logger;

        /// <summary>
        /// MedicineController Constructor
        /// </summary>
        /// <param name="medicineDoseService">IMedicineService object reference</param>
        public MedicineDoseController(IMedicineDoseService medicineDoseService, ILogger<MedicineDoseController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _medicineDoseService = medicineDoseService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(_medicineDoseService.GetAllDoctors());
        }

        public ActionResult GetMedicineDoseByDoctorWise(int id)
        {
            MedicineDoseListGridItemVM medicineDoseListGridItemVM = _medicineDoseService.GetMedicineDoseByDoctorWise(id);
            return PartialView("_MedicineDoseList", medicineDoseListGridItemVM);
        }

        /// <summary>
        /// Get Add Medicine view 
        /// </summary>
        /// <returns>Add Medicine view</returns>
        public IActionResult AddMedicineDose(int DoctorId)
        {
            try
            {
                ViewBag.DoctorId = DoctorId;
                MedicineDose medicineDose = _medicineDoseService.GetMedicineDoseDataToCreateMedicineDose();
                medicineDose.DoctorId = DoctorId;
                return View(medicineDose);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }
        }

        /// <summary>
        /// Post Add doctor to save it
        /// </summary>
        /// <param name="medicineDose">AddDoctorVM data structure model</param>
        /// <returns>Success or Failure</returns>
        [HttpPost]
        public IActionResult AddMedicineDose([Bind] MedicineDose medicineDose)
        {
            BaseResult result = null;
            try
            {
                if (ModelState.IsValid)
                {
                    result = _medicineDoseService.SaveMedicineDose(medicineDose);
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
        public IActionResult EditMedicineDose(int MedicineId, int SelectedDoctorId)
        {
            try
            {               
                MedicineDose medicineDose = _medicineDoseService.GetMedicineDoseById(MedicineId);
                ViewBag.DoctorId = medicineDose.DoctorId;
                return View(medicineDose);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }
        }

        /// <summary>
        /// Update MedicineDose
        /// </summary>
        /// <param name="medicineDose">Doctor that will be updated</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult EditMedicineDose(MedicineDose medicineDose)
        {
            BaseResult result = null;

            try
            {
                if (ModelState.IsValid)
                {
                    //medicineDose.ModifiedBy = _loggedInUserID;
                    result = _medicineDoseService.UpdateMedicineDose(medicineDose);
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
        /// Delete MedicineDose 
        /// </summary>
        /// <param name="targetIds">List of End User IDs</param>
        /// <returns>Success/Failuare as Baseresult</returns>
        [HttpPost]
        public IActionResult DeleteMedicineDose(DeleteItemVM targetIds)
        {
            BaseResult result = new BaseResult();
            try
            {
                result = _medicineDoseService.DeleteMedicineDoseByIds(targetIds);
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