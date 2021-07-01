using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PickfordsIntranet.Core.DomainServices;
using PickfordsIntranet.Core.Helper;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.WebAdmin.Filters;
using PickfordsIntranet.WebAdmin.Models;

namespace PickfordsIntranet.WebAdmin.Controllers
{
    /// <summary>
    /// Home Controller for dashboard and common view
    /// </summary>
    [Authorize(Roles = "SA,DA,LM")]
    public class HomeController : Controller
    {
        /// <summary>
        /// IAdminService data member
        /// </summary>
        private IAdminService _adminService;

        /// <summary>
        /// Ilogger Data Member
        /// </summary>
        private ILogger<HomeController> _logger;

        /// <summary>
        /// IPathProvider data member
        /// </summary>
        private IPathProvider _pathProvider;

        private IViewParser _viewParser;

        //private IViewParser _viewParser;

        //private Utility.SmtpMessage _smtpMessage;

        public HomeController(IAdminService adminService, IViewParser viewParser, ILogger<HomeController> logger)
        {
            _adminService = adminService;
            _viewParser = viewParser;
            _logger = logger;
        }


        /// <summary>
        /// Get Dashboard view action
        /// </summary>
        /// <returns>Dashboard View</returns>
        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            return PartialView("_ChangePassword");
        }

        /// <summary>
        /// Get Error view action
        /// </summary>
        /// <returns>Error View</returns>
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public JsonResult ValidateOldPassword(string oldPassword, UserActionLoggingDetails loggingDetails)
        {
            bool result = _adminService.CheckValidOldPassword(oldPassword, Convert.ToInt32(loggingDetails.UserId));
            if (!result)
                return Json(data: "The old password does not match our records.");

            return Json(data: true);

            //return Json(result);
            //GazetersDetail details = _frontEndService.GetSelectedPostalCodeDetail(Code.TrimEnd());
            //return Json(new { details = details });
        }


        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public JsonResult ChangePasswordForLoginUser(string NewPassword,string ConfirmPassword, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = _adminService.ChangePasswordForLoginUser(NewPassword, Convert.ToInt32(loggingDetails.UserId));
            return Json(result);
        }
        
    }
}
