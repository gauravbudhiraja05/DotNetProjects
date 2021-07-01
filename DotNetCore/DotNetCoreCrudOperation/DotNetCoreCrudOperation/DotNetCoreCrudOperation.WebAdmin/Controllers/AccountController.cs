using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PickfordsIntranet.Core.DomainServices;
using PickfordsIntranet.Core.Helper;
using PickfordsIntranet.ViewModels;
using PickfordsIntranet.ViewModels.Auth;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.WebAdmin.Utility;

namespace PickfordsIntranet.WebAdmin.Controllers
{

    /// <summary>
    /// Account Controller for Authentication
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// IAuthService data member
        /// </summary>
        private IAuthService _authService;
        private ILogger<AccountController> _logger;
        private IViewParser _viewParser;
        private Utility.SmtpMessage _smtpMessage;
        private IConfigurationRoot _config;


        /// <summary>
        /// AccountController Constructor
        /// </summary>
        /// <param name="authService">IAuthService object reference</param>
        public AccountController(IAuthService authService, IConfigurationRoot config, IViewParser viewParser, Utility.SmtpMessage smtpMessage, ILogger<AccountController> logger)
        {
            _config = config;
            _authService = authService;
            _smtpMessage = smtpMessage;
            _viewParser = viewParser;
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
                        
                        HttpContext.SignInAsync("AdminCookies", principal, new AuthenticationProperties { ExpiresUtc = DateTime.UtcNow.AddHours(12) });

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


        public IActionResult AccessDenied(string returnUrl)
        {
            // string tempUserCookies = userId + "||" + email + "||" + fullName + "||" + role ;
            ViewBag.ReturnUrl = returnUrl;
            if (Request.Cookies.Any(c => c.Key == "tempUserCookies"))
            {
                var tempUserCookiesArray = Request.Cookies["tempUserCookies"].Split("||");
                string roles = tempUserCookiesArray[3];
                var claims = new List<Claim>
                    {
                        new Claim("UserID", Convert.ToString(tempUserCookiesArray[0])),
                        new Claim("Email", tempUserCookiesArray[1]),
                        new Claim("FullName", tempUserCookiesArray[2])
                    };

                // Add Roles in Claim after split it with + character
                roles.Split("+").ToList().ForEach(r => {
                    claims.Add(new Claim(ClaimTypes.Role, r));
                });

                ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "adminlogin");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                HttpContext.SignInAsync("AdminCookies", principal, new AuthenticationProperties { ExpiresUtc = DateTime.UtcNow.AddHours(12) });
                Response.Cookies.Delete("tempUserCookies");
                return Redirect(returnUrl);
            }

            else
            {
                return View();
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

        /// <summary>
        /// To Check the Email Address exist or not for password reset
        /// </summary>
        /// <param name="user">AuthUserVM data structure</param>
        /// <returns>Email is exist or not</returns>
        [HttpPost]
        public IActionResult ValidateEMailExistOrNot(AuthUserVM user)
        {
            FieldCheckVM result = _authService.IsEmailExistForPasswordReset(user.Email);

            return Json(result);
        }

        /// <summary>
        /// Send mail to requested user for password reset
        /// </summary>
        /// <param name="user">AuthUserVM data structure</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult MailSendLinkToResetPassword(AuthUserVM user)
        {
            BaseResult result = new BaseResult();
            FieldCheckVM emailValid = _authService.IsEmailExistForPasswordReset(user.Email);
            try
            {
                if (emailValid.IsValid == false)
                {
                    result.IsSuccess = false;
                    result.Message = emailValid.Message;
                }

                else
                {
                    // Stpe 1: Generate Token
                    ResetPasswordForEmailTemplateVM token = _authService.GenerateTokenForResetPassword(user.Email);

                    // Step 2: Prepare Reset Password Link
                    token.ResetPasswordLink = "http://" + this.Request.Host.Value + "/Account/ResetPassword/" + Convert.ToString(token.Token);

                    // Step 3: Prepare Template
                    // string messageBody = _viewParser.RenderToStringAsync("../SuperAdmin/EmailTemplateResetPassword", token);
                    //string messageBody = _config["EmailTemplateResetPassword"];
                    string messageBody = _authService.GetEmailTemplateHtmlStringByName("EmailTemplateResetPassword");

                    messageBody = messageBody.Replace("ReplaceFullName", token.FullName);
                    messageBody = messageBody.Replace("ReplaceUrl", token.ResetPasswordLink);
                    messageBody = messageBody.Replace("<div>", "<div style" + "=" + "font-family:" + "Arial" + ";" + ">");

                    // Step 4: Send mail to requested user
                    _smtpMessage.Subject = "Reset your password for the Pickfords Intranet Administration System";
                    _smtpMessage.BodyContent = messageBody;
                    _smtpMessage.ToAddress = user.Email;
                    bool isSent = _smtpMessage.Send();
                    result.IsSuccess = isSent;

                    // Step 5 return the appropriate result
                    return Json(result);
                }
            }

            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result.IsSuccess = false;
                result.Message = ex.Message;
                return Json(result);
            }

            return Json(result);
        }

        /// <summary>
        /// Reset password view
        /// </summary>
        /// <param name="id">Token</param>
        /// <returns>View of reset password</returns>
        public IActionResult ResetPassword(string id)
        {
            try
            {
                // Check Token is valid or not
                bool isValid = _authService.IsTokenValidForPasswordReset(token: new Guid(id));
                ViewBag.IsTokenValid = isValid ? true : false;

                return View(new ResetPasswordVM() { Token = new Guid(id) });
            }
            catch (Exception ex)
            {

                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }
        }

        [HttpPost]
        
        public IActionResult ResetYourPassord(ResetPasswordVM resetPassword)
        {
            BaseResult result = new BaseResult();

            try
            {
                if (ModelState.IsValid)
                {
                    result = _authService.ResetPassword(resetPassword);

                }

                return Ok(result);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }

        }
    }
}