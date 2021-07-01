using HiveReport.Dto.Common;
using HiveReport.Dto.User;
using HiveReport.WebAdmin.Dashboard.Af;
using HiveReport.WebAdmin.User.Af;
using HiveReport.WebAdmin.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace HiveReport.WebAdmin.Controllers
{
    public class UserController : Controller
    {
        // <summary>
        /// Private IUserAf Data Member
        /// </summary>
        private readonly IUserAf _userAf;

        // <summary>
        /// Private ILogger Data Member
        /// </summary>
        private readonly ILogger<UserController> _logger;

        public UserController(IUserAf userAf, ILogger<UserController> logger)
        {
            _userAf = userAf;
            _logger = logger;
        }

        public IActionResult Registration(string code, string productType, string databaseType, string database)
        {
            TempData["ProductCode"] = code;
            TempData["ProductType"] = productType;
            TempData["DatabaseType"] = databaseType;
            TempData["Database"] = database;
            return View();
        }

        [HttpPost]
        public JsonResult IsEmailExists(string emailAddress)
        {
            BaseResultDto baseResultDto;
            try
            {
                baseResultDto = _userAf.IsEmailExists(emailAddress);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                baseResultDto = new BaseResultDto
                {
                    Message = StaticResource.InternalServerMessage
                };
            }
            return Json(baseResultDto);
        }

        [HttpPost]
        public JsonResult Registration(RegisteredUserDto registeredUser)
        {
            BaseResultDto baseResultDto;
            try
            {
                baseResultDto = _userAf.AddUserInformation(registeredUser);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                baseResultDto = new BaseResultDto
                {
                    Message = StaticResource.InternalServerMessage
                };
            }
            return Json(baseResultDto);
        }
    }
}
