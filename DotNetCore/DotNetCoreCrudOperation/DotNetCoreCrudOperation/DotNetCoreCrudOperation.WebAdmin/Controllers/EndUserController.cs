using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PickfordsIntranet.Core.DomainServices;
using PickfordsIntranet.ViewModels.EndUser;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.WebAdmin.Filters;
using PickfordsIntranet.WebAdmin.Utility;

namespace PickfordsIntranet.WebAdmin.Controllers
{
    /// <summary>
    /// SuperAdmin Controller for administration and departmental management
    /// </summary>
    [Authorize(Roles = "SA")]
    public class EndUserController : Controller
    {
        /// <summary>
        /// IAdminService data member
        /// </summary>
        private IEndUserService _endUserService;

        /// <summary>
        /// IPathProvider data member
        /// </summary>
        private IPathProvider _pathProvider;

        /// <summary>
        /// Ilogger Data Member
        /// </summary>
        private ILogger<EndUserController> _logger;

        /// <summary>
        /// EndUserController constructor
        /// </summary>
        /// <param name="enduserService"></param>
        public EndUserController(IEndUserService enduserService, IPathProvider pathProvider, ILogger<EndUserController> logger)
        {
            _pathProvider = pathProvider;
            _endUserService = enduserService;
            _logger = logger;
        }

        /// <summary>
        /// Get End Users grid view 
        /// </summary>
        /// <returns>End Users grid view</returns>
        [AutoPopulateLoggingDetails]
        public IActionResult Index()
        {
            return View(_endUserService.GetAllEndUsers());
        }

        /// <summary>
        /// Get Add Admin users view 
        /// </summary>
        /// <returns>Add Admin user view</returns>
        public IActionResult AddEndUser()
        {
            try
            {
                return View();
            }

            catch (Exception ex)
            {
                _logger.LogError((int)System.Diagnostics.Tracing.EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }

        }

        /// <summary>
        /// To Check the Email Address already exist or not
        /// </summary>
        /// <param name="emailAddress">Email Address</param>
        /// <param name="id">User Id</param>
        /// <returns>Json data :true or string message</returns>
        [AllowAnonymous]
        public IActionResult ValidateEMailExistOrNot(string emailAddress, string id)
        {
            bool isExist = _endUserService.IsEmailExist(emailAddress, id);
            if (isExist)
                return Json(data: "Email address already exist.");

            return Json(data: true);
        }

        /// <summary>
        /// Post Add admin user to save it
        /// </summary>
        /// <param name="user">AddAdminUserVM data structure model</param>
        /// <returns>Success or Failure</returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult AddEndUser([Bind] EndUserVM user, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = null;

            try
            {
                if (ModelState.IsValid)
                {
                    result = _endUserService.SaveEndUser(user);
                }


                return Ok(result);
            }

            catch (Exception ex)
            {
                _logger.LogError((int)System.Diagnostics.Tracing.EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }
        }

        /// <summary>
        /// Edit End User Record
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>View of Edit Admin User</returns>
        public IActionResult EditEndUser(int id)
        {
            try
            {
                EndUserVM user = _endUserService.GetEndUserById(id);
                return View(user);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)System.Diagnostics.Tracing.EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }

        }

        /// <summary>
        /// Update Admin user
        /// </summary>
        /// <param name="user">Admin user that will be updated</param>
        /// <param name="loggingDetails">logged-in User Info</param>
        /// <returns></returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult EditEndUser(EndUserVM user, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = null;

            try
            {

                if (ModelState.IsValid)
                {
                    result = _endUserService.UpdateEndUser(user);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)System.Diagnostics.Tracing.EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }
        }
        /// <summary>
        /// Delete Admin Users 
        /// </summary>
        /// <param name="allUserIds">List of End User IDs</param>
        /// <returns>Success/Failuare as Baseresult</returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult DeleteEndUsers(DeleteItemVM targetIds, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = new BaseResult();
            targetIds.DeletedBy = Convert.ToInt32(loggingDetails.UserId);

            try
            {
                result = _endUserService.DeleteEndUsersByIds(targetIds);
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