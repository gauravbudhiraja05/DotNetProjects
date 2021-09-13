using HiveReport.Dto.Common;
using HiveReport.Dto.User;
using HiveReport.WebAdmin.Common.Af;
using HiveReport.WebAdmin.Dashboard.Af;
using HiveReport.WebAdmin.Models;
using HiveReport.WebAdmin.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;

namespace HiveReport.WebAdmin.Controllers
{

    public class HomeController : Controller
    {
        // <summary>
        /// Private IDashboardAf Data Member
        /// </summary>
        private readonly IDashboardAf _dashboardAf;

        // <summary>
        /// Private ISharedLayoutAf Data Member
        /// </summary>
        private readonly ISharedLayoutAf _sharedLayoutAf;

        // <summary>
        /// Private ILogger Data Member
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        public HomeController(IDashboardAf dashboardAf, ISharedLayoutAf sharedLayoutAf, ILogger<HomeController> logger)
        {
            _dashboardAf = dashboardAf;
            _sharedLayoutAf = sharedLayoutAf;
            _logger = logger;
        }

        public IActionResult Index() => View();
        public IActionResult About() => View();
        public IActionResult Product() => View();
        public IActionResult Support() => View();
        public IActionResult Licence() => View();
        public IActionResult Contact() => View();

        [HttpGet]
        public IActionResult Dashboard()
        {
            UserDetailDto userDetailDto;
            try
            {
                var claims = (HttpContext.User.Identity as ClaimsIdentity).Claims;
                string userId = claims.Where(x => x.Type == "userid").FirstOrDefault().Value;
                string userType = claims.Where(x => x.Type == "typeofuser").FirstOrDefault().Value;
                string userName = claims.Where(x => x.Type == "username").FirstOrDefault().Value;
                userDetailDto = _dashboardAf.GetDashboardUserDetails(userId, userType);
                userDetailDto.UserName = userName;

                SharedLayoutSearchCriteria criteria = new SharedLayoutSearchCriteria
                {
                    IsDashboardMenu = true,
                    RequestValue = string.Empty,
                    UserId = userId,
                    UserName = userName,
                    UserAdminCheck = claims.Where(x => x.Type == "useradmincheck").FirstOrDefault().Value,
                    UserType = userType,
                };

                ViewBag.sharedLayoutDto = _sharedLayoutAf.GetSharedLayoutDetail(criteria);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                throw;
            }
            return View(userDetailDto);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
