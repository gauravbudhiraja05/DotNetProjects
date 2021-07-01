using HiveReport.WebAdmin.Utility;
using HiveReport.Dto.User;
using HiveReport.WebAdmin.User.Af;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using HiveReport.WebAdmin.Common.Af;
using HiveReport.Dto.Common;
using HiveReport.WebAdmin.Account.Af;

namespace HiveReport.WebAdmin.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// Private IUserAf Data Member
        /// </summary>
        private readonly IUserAf _userAf;

        /// <summary>
        /// Private IAccountAf Data Member
        /// </summary>
        private readonly IAccountAf _accountAf;

        /// <summary>
        /// Private IUserAf Data Member
        /// </summary>
        private readonly ISharedLayoutAf _sharedLayoutAf;

        /// <summary>
        /// Private ILogger Data Member
        /// </summary>
        private readonly ILogger<AccountController> _logger;

        public AccountController(IUserAf userAf, ILogger<AccountController> logger, ISharedLayoutAf sharedLayoutAf, IAccountAf accountAf)
        {
            _userAf = userAf;
            _logger = logger;
            _sharedLayoutAf = sharedLayoutAf;
            _accountAf = accountAf;
        }

        public ActionResult Index(int id)
        {
            ViewBag.SharedLayoutDto = GetLayout(id);
            return View();
        }

        public ActionResult Registration(int id)
        {
            ViewBag.sharedLayoutDto = GetLayout(id);
            return View();
        }

        [HttpPost]
        public JsonResult Registration(RegisteredUserDto registeredUser)
        {
            var claims = (HttpContext.User.Identity as ClaimsIdentity).Claims;
            string userType = claims.Where(x => x.Type == "typeofuser").FirstOrDefault().Value;
            string userid = claims.Where(x => x.Type == "userid").FirstOrDefault().Value;
            return Json(_accountAf.AddUserDetails(registeredUser, userType, userid));
        }

        [HttpPost]
        public JsonResult Login(AuthUserDto authUserDto)
        {
            try
            {
                LoggedInUserDto loggedInUserDto = _userAf.UserAuthenticate(authUserDto);

                if (loggedInUserDto != null && loggedInUserDto.IsSuccess == true)
                {
                    string userType;
                    if (loggedInUserDto.UserType == 1)
                        userType = "User";
                    else if (loggedInUserDto.UserType == 3)
                        userType = "Super Admin";
                    else
                        userType = "Admin";


                    var claims = new List<Claim>
                        {
                            new Claim("typeofuser", userType),
                            //new Claim("CreatedBy", loggedInUserDto.CreatedBy),
                            new Claim("userid", loggedInUserDto.UserID),
                            new Claim("userid1" ,loggedInUserDto.UserID),
                            new Claim("username" , loggedInUserDto.UserName),
                            new Claim("logintime" , DateTime.Now.ToString()),
                            new Claim("usertype" , loggedInUserDto.UserType.ToString()),
                            new Claim("deptid" , loggedInUserDto.DeptID.ToString()),
                            new Claim("clientid" , loggedInUserDto.ClientID.ToString()),
                            new Claim("lobid" , loggedInUserDto.LOBID.ToString()),
                            new Claim("useradmincheck", loggedInUserDto.UserAdminCheck.ToString()),
                            //new Claim("prefix", loggedInUserDto.Prefix),
                            new Claim("Password", loggedInUserDto.Password),
                        };


                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "reportuserlogin");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    HttpContext.SignInAsync("ReportUserCookies", principal, new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.UtcNow.AddHours(12),
                        IsPersistent = false,
                        AllowRefresh = false
                    });
                }
                else
                {
                    loggedInUserDto.IsSuccess = false;
                }

                return Json(loggedInUserDto);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return Json(null);
            }
        }

        [HttpGet]
        public JsonResult CheckAvailableEmployeeId(int employeeId)
        {
            return Json(_accountAf.CheckAvailableEmployeeId(employeeId));
        }

        [HttpGet]
        public JsonResult CheckAvailableUserId(string emailAddress)
        {
            return Json(_accountAf.CheckAvailableUserId(emailAddress));
        }

        [HttpGet]
        public JsonResult GetDesignationList()
        {
            try
            {
                return Json(_accountAf.GetDesignationList());
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return Json(null);
            }
        }

        public JsonResult GetDepartmentList()
        {
            try
            {
                var claims = (HttpContext.User.Identity as ClaimsIdentity).Claims;
                string userName = claims.Where(x => x.Type == "username").FirstOrDefault().Value;
                return Json(_accountAf.GetDepartmentList(userName));
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return Json(null);
            }
        }

        public JsonResult GetClientList(int departmentId)
        {
            try
            {
                return Json(_accountAf.GetClientList(departmentId));
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return Json(null);
            }
        }

        public JsonResult GetLOBList(int departmentId, int clientId)
        {
            try
            {
                return Json(_accountAf.GetLOBList(departmentId, clientId));
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return Json(null);
            }
        }

        public ActionResult SearchUser(int id)
        {
            ViewBag.sharedLayoutDto = GetLayout(id);
            return View();
        }

        public JsonResult GetSearchedResult(string dropdownValue, string txtValue)
        {
            try
            {
                var claims = (HttpContext.User.Identity as ClaimsIdentity).Claims;
                string userid = claims.Where(x => x.Type == "userid").FirstOrDefault().Value;
                return Json(_accountAf.GetSearchedResult(dropdownValue, txtValue, userid));
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return Json(null);
            }
        }

        #region Private Methods

        private SharedLayoutDto GetLayout(int id)
        {
            var claims = (HttpContext.User.Identity as ClaimsIdentity).Claims;
            SharedLayoutSearchCriteria sharedLayoutSearchCriteria = new SharedLayoutSearchCriteria
            {
                IsDashboardMenu = false,
                UserId = claims.Where(x => x.Type == "userid").FirstOrDefault().Value,
                UserName = claims.Where(x => x.Type == "username").FirstOrDefault().Value,
                UserAdminCheck = claims.Where(x => x.Type == "useradmincheck").FirstOrDefault().Value,
                UserType = claims.Where(x => x.Type == "typeofuser").FirstOrDefault().Value,
                RequestValue = id.ToString(),
            };

            return _sharedLayoutAf.GetSharedLayoutDetail(sharedLayoutSearchCriteria);
        }

        #endregion
    }
}
