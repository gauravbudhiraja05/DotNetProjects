using Dream14.Core.DomainServices;
using Dream14.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dream14.WebAdmin.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// IAuthService data member
        /// </summary>
        private readonly IAuthService _authService;

        /// <summary>
        /// ILogger data member
        /// </summary>
        private readonly ILogger<AccountController> _logger;

        /// <summary>
        /// AccountController Constructor
        /// </summary>
        /// <param name="authService">IAuthService object reference</param>
        public AccountController(IAuthService authService, ILogger<AccountController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Get login action that returns login view
        /// </summary>
        /// <returns>login view</returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Post login action that authenticate users
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Redirect to dashboard</returns>
        [HttpPost]
        public IActionResult Login([Bind] AuthUserVM user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _authService.UserAuthenticate(user);

                    if (result.IsSuccess == true)
                    {
                        // Remove commma from Roles
                        //result.RoleName = result.RoleName.Substring(1);
                        result.RoleName = result.RoleName;

                        string role = result.RoleName.Trim();
                        var claims = new List<Claim>
                        {
                            new Claim("UserID", Convert.ToString(result.UserId)),
                            new Claim("Email", user.Email),
                            new Claim("FullName", result.FullName)
                        };

                        // Add Roles
                        role.Split(",").ToList().ForEach(r =>
                        {
                            claims.Add(new Claim(ClaimTypes.Role, r));
                        });
                        ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "adminlogin");
                        ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                        HttpContext.SignInAsync("AdminCookies", principal, new AuthenticationProperties
                        {
                            ExpiresUtc = DateTime.UtcNow.AddHours(12),
                            IsPersistent = false,
                            AllowRefresh = false
                        });

                        return RedirectToAction("Dashboard", "Home");
                    }
                    else
                    {
                        TempData["UserLoginFailed"] = result.Message;
                        return View();
                    }
                }
                else
                    return View();
            }

            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }
        }

        /// <summary>
        /// Logout from system
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
