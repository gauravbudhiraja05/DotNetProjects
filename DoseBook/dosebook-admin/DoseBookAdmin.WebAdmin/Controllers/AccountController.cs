using DoseBookAdmin.Dto.User;
using DoseBookAdmin.WebAdmin.User.Af;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DoseBookAdmin.WebAdmin.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// Private IUserAf Data Member
        /// </summary>
        private IUserAf _userAf;

        /// <summary>
        /// Private ILogger Data Member
        /// </summary>
        private ILogger<AccountController> _logger;

        public AccountController(IUserAf userAf, ILogger<AccountController> logger)
        {
            _userAf = userAf;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([Bind] AuthUserDto authUserDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LoggedInUserDto loggedInUserDto = _userAf.UserAuthenticate(authUserDto);

                    if (loggedInUserDto != null && loggedInUserDto.IsSuccess == true)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim("UserID", Convert.ToString(loggedInUserDto.UserId)),
                            new Claim("Email", loggedInUserDto.EmailId),
                            new Claim("FullName", loggedInUserDto.FullName)
                        };

                        claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                        HttpContext.Session.SetString("Role", "Admin");


                        ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "adminlogin");
                        ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                        HttpContext.SignInAsync("AdminCookies", principal, new AuthenticationProperties { ExpiresUtc = DateTime.UtcNow.AddSeconds(1) });

                        return RedirectToAction("Dashboard", "Home");
                    }
                    else
                    {
                        TempData["UserLoginFailed"] = loggedInUserDto.Message;
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

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}