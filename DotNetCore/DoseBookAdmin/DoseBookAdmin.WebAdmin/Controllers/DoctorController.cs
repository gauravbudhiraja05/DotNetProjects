using DoseBookAdmin.Core.DomainServices;
using DoseBookAdmin.ViewModels.Doctors;
using DoseBookAdmin.ViewModels.Global;
using DoseBookAdmin.WebAdmin.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace DoseBookAdmin.WebAdmin.Controllers
{
    public class DoctorController : Controller
    {
        /// <summary>
        /// IDoctorService data member
        /// </summary>
        private IDoctorService _doctorService;

        /// <summary>
        /// Ilogger Data Member
        /// </summary>
        private ILogger<DoctorController> _logger;

        /// <summary>
        /// DoctorController constructor
        /// </summary>
        /// <param name="enduserService"></param>
        public DoctorController(IDoctorService doctorService, ILogger<DoctorController> logger)
        {
            _doctorService = doctorService;
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View(_doctorService.GetAllDoctors());
        }

        /// <summary>
        /// Get Add Doctor view 
        /// </summary>
        /// <returns>Add Admin user view</returns>
        public IActionResult AddDoctor()
        {
            try
            {
                Doctor doctor = new Doctor();
                return View(doctor);
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
        /// <param name="doctor">AddDoctorVM data structure model</param>
        /// <returns>Success or Failure</returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult AddDoctor([Bind] Doctor doctor)
        {
            BaseResult result = null;
            try
            {
                if (ModelState.IsValid)
                {
                    result = _doctorService.SaveDoctor(doctor);
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
        /// Edit Doctor Record
        /// </summary>
        /// <param name="id">Doctor Id</param>
        /// <returns>View of Edit Doctor</returns>
        public IActionResult EditDoctor(int id)
        {
            try
            {
                Doctor doctor = _doctorService.GetDoctorById(id);
                return View(doctor);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }
        }

        /// <summary>
        /// Update Doctor
        /// </summary>
        /// <param name="doctor">Doctor that will be updated</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult EditDoctor(Doctor doctor)
        {
            BaseResult result = null;

            try
            {
                if (ModelState.IsValid)
                {
                    result = _doctorService.UpdateDoctor(doctor);
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
        /// Delete Doctor 
        /// </summary>
        /// <param name="targetIds">List of Doctor IDs</param>
        /// <returns>Success/Failuare as Baseresult</returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult DeleteDoctor(DeleteItemVM targetIds)
        {
            BaseResult result = new BaseResult();
            
            try
            {
                result = _doctorService.DeleteDoctorsByIds(targetIds);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)System.Diagnostics.Tracing.EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }

            return Ok(result);
        }
    }
}