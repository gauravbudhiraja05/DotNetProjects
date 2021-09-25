using Dream14.Core.DomainServices;
using Dream14.Core.Helper;
using Dream14.ViewModels.Global;
using Dream14.ViewModels.SuperAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.Claims;

namespace Dream14.WebAdmin.Controllers
{
    public class SuperAdminController : Controller
    {

        #region Private Member

        /// <summary>
        /// IAuthService data member
        /// </summary>
        private readonly IAdminService _adminService;


        /// <summary>
        /// ILogger data member
        /// </summary>
        private readonly ILogger<SuperAdminController> _logger;

        #endregion

        #region Constructor

        /// <summary>
        /// SuperAdminController constructor
        /// </summary>
        /// <param name="adminService">admin service object</param>
        /// <param name="logger">loggwr object</param>
        public SuperAdminController(IAdminService adminService, ILogger<SuperAdminController> logger)
        {
            _adminService = adminService;
            _logger = logger;
            //var claims = (HttpContext.User.Identity as ClaimsIdentity).Claims;
            //string userId = claims.Where(x => x.Type == "UserID").FirstOrDefault().Value;
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }


        #region Admin Users


        /// <summary>
        /// Get Admin Users grid view 
        /// </summary>
        /// <returns>Admin Users grid view</returns>
        public IActionResult AdminUsers()
        {
            return View(_adminService.GetAllAdminUsers());
        }


        public IActionResult AddAdminUser()
        {
            AdminUser adminUser = new AdminUser();
            return View(adminUser);
        }


        public JsonResult CheckEmailExists(string emailAddress)
        {
            BaseResult result = null;

            try
            {
                result = _adminService.CheckEmailExist(emailAddress);
                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResult
                {
                    Message = StaticResource.InternalServerMessage
                };
                return Json(result);
            }
        }

        [HttpPost]
        public JsonResult AddAdminUser(string FullName, string EmailAddress, string Password)
        {
            BaseResult result = null;

            try
            {
                var claims = (HttpContext.User.Identity as ClaimsIdentity).Claims;
                AdminUser adminUser = new AdminUser
                {
                    FullName = FullName,
                    EmailAddress = EmailAddress,
                    Password = Password,
                    CreatedBy = Convert.ToInt32(claims.Where(x => x.Type == "UserID").FirstOrDefault().Value),
                    IsActive = true,
                    RoleName = "Admin"
                };
                result = _adminService.SaveAdminUser(adminUser);

                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResult
                {
                    Message = StaticResource.InternalServerMessage
                };
                return Json(result);
            }

        }

        public IActionResult EditAdminUser(int id)
        {
            try
            {
                AdminUser adminUser = _adminService.GetAdminUserById(id);
                return View(adminUser);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult EditAdminUser(string FullName, string EmailAddress, string Password, int userId)
        {
            BaseResult result = null;

            try
            {
                var claims = (HttpContext.User.Identity as ClaimsIdentity).Claims;
                AdminUser adminUser = new AdminUser
                {
                    FullName = FullName,
                    EmailAddress = EmailAddress,
                    Password = Password,
                    Id = userId,
                    ModifiedBy = Convert.ToInt32(claims.Where(x => x.Type == "UserID").FirstOrDefault().Value)
                };
                result = _adminService.UpdateAdminUser(adminUser);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResult
                {
                    Message = StaticResource.InternalServerMessage
                };
                return Ok(result);
            }
        }


        /// <summary>
        /// Delete Admin Users 
        /// </summary>
        /// <param name="targetIds">List of Admin User IDs</param>
        /// <returns>Success/Failuare as Baseresult</returns>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult DeleteAdminUsers(DeleteItemVM targetIds)
        {
            BaseResult result = new BaseResult();

            try
            {
                var claims = (HttpContext.User.Identity as ClaimsIdentity).Claims;
                targetIds.DeletedBy = Convert.ToInt32(claims.Where(x => x.Type == "UserID").FirstOrDefault().Value);
                result = _adminService.DeleteAdminUsersByIds(targetIds);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResult
                {
                    Message = StaticResource.InternalServerMessage
                };
                return Ok(result);
            }
        }

        #endregion
    }
}
