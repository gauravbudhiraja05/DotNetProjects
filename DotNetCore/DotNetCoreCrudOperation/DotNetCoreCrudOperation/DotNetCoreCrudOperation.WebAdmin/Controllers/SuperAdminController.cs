using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using PickfordsIntranet.Core.DomainServices;
using PickfordsIntranet.Core.DomainServices.FrontEnd;
using PickfordsIntranet.Core.Helper;
using PickfordsIntranet.Core.ThirdPartyServices;
using PickfordsIntranet.ViewModels.EndUser;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.SuperAdmin;
using PickfordsIntranet.WebAdmin.Filters;
using PickfordsIntranet.WebAdmin.Utility;

namespace PickfordsIntranet.WebAdmin.Controllers
{
    /// <summary>
    /// SuperAdmin Controller for administration and departmental management
    /// </summary>
    [Authorize(Roles = "SA")]
    public class SuperAdminController : Controller
    {
        #region Private Member

        /// <summary>
        /// INewsService data member
        /// </summary>
        private IFronEndService _frontEndService;



        /// <summary>
        /// IAuthService data member
        /// </summary>
        private IAdminService _adminService;

        /// <summary>
        /// IAdminService data member
        /// </summary>
        private IAuthService _authService;


        /// <summary>
        /// Ilogger Data Member
        /// </summary>
        private ILogger<SuperAdminController> _logger;

        /// <summary>
        /// IPathProvider data member
        /// </summary>
        private IPathProvider _pathProvider;

        private IViewParser _viewParser;
        private IConfigurationRoot _config;
        private Utility.SmtpMessage _smtpMessage;
        #endregion

        #region Constructor

        /// <summary>
        /// SuperAdminController constructor
        /// </summary>
        /// <param name="adminService">admin service object</param>
        /// <param name="logger">logger object</param>
        /// <param name="viewParser">View Parser object</param>
        public SuperAdminController(IAdminService adminService, IAuthService authService, IConfigurationRoot config, ILogger<SuperAdminController> logger, IViewParser viewParser
            , Utility.SmtpMessage smtpMessage
            , IPathProvider pathProvider , IFronEndService frontEndService)
        {
            _config = config;
            _adminService = adminService;
            _authService = authService;
            _logger = logger;
            _viewParser = viewParser;
            _smtpMessage = smtpMessage;
            _pathProvider = pathProvider;
            _frontEndService = frontEndService; 
        }

        #endregion

        #region Admin Users

        /// <summary>
        /// Get Admin Users grid view 
        /// </summary>
        /// <returns>Admin Users grid view</returns>
        [AutoPopulateLoggingDetails]
        public IActionResult AdminUsers(UserActionLoggingDetails loggingDetails)
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult AdminUserList(int roleType)
        {
            var result = _adminService.GetAllAdminUsers();

            // Remove Duplicate records

            // Remove the comma from first index
            result.ToList().ForEach(r => {
                r.RoleType = r.RoleType.Substring(2);
            });

            return PartialView("_AdminUserList", result);
        }

        /// <summary>
        /// Get Add Admin users view 
        /// </summary>
        /// <returns>Add Admin user view</returns>
        public IActionResult AddAdminUser()
        {
            try
            {
                return View(_adminService.GetAdminUserDataToCreateAdminUser());
            }

            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }

        }

        /// <summary>
        /// Post Add admin user to save it
        /// </summary>
        /// <param name="user">AddAdminUserVM data structure model</param>
        /// <returns>Success or Failure</returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult AddAdminUser([Bind] AdminUserVM user, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = null;

            try
            {
                if (ModelState.IsValid)
                {
                    user.CreatedBy = Convert.ToInt32(loggingDetails.UserId);
                    user.UserPwd = RandomPassword.GeneratePassword(8);
                    result = _adminService.SaveAdminUser(user);
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

        public IActionResult EmailTemplate()
        {
            return View("EmailTemplateAccessCredential");
        }


        /// <summary>
        /// Post Add admin user to save and send mail to user
        /// </summary>
        /// <param name="user">AddAdminUserVM data structure model</param>
        /// <returns>Success or Failure</returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult AddAdminUserWithSendEmail([Bind] AdminUserVM user, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = null;

            try
            {
                if (ModelState.IsValid)
                {
                    user.CreatedBy = Convert.ToInt32(loggingDetails.UserId);
                    user.UserPwd = RandomPassword.GeneratePassword(8);
                    result = _adminService.SaveAdminUser(user);

                    // Check Admin user is saved or not and then send mail to user
                    if (result.IsSuccess == true)
                    {
                        //string messageBody = _config["EmailTemplateAccessCredential"];
                        string messageBody = _authService.GetEmailTemplateHtmlStringByName("EmailTemplateAccessCredential");
                        messageBody = messageBody.Replace("ReplaceFullName", user.FirstName);
                        messageBody = messageBody.Replace("ReplaceUrl", "http://" + this.Request.Host.Value + "/Account/Login");
                        messageBody = messageBody.Replace("ReplaceUserName", user.EmailAddress);
                        messageBody = messageBody.Replace("ReplacePassword", user.UserPwd);
                        messageBody = messageBody.Replace("<div>", "<div style" + "=" + "font-family:" + "Arial" + ";" + ">");


                        _smtpMessage.Subject = "Your login account for the Pickfords Intranet Administration System";

                        _smtpMessage.BodyContent = messageBody;
                        _smtpMessage.ToAddress = user.EmailAddress;
                        bool isSent = _smtpMessage.Send();
                        result.IsSuccess = isSent;


                    }
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
        /// Edit Admin User Record
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>View of Edit Admin User</returns>
        public IActionResult EditAdminUser(int id)
        {
            try
            {
                AdminUserVM user = _adminService.GetAdminUserById(id);
                return View(user);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("Error");
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
        public IActionResult EditAdminuser(AdminUserVM user, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = null;

            try
            {

                if (ModelState.IsValid)
                {
                    user.ModifiedBy = Convert.ToInt32(loggingDetails.UserId);
                    result = _adminService.UpdateAdminUser(user);
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

        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult EditAdminUserWithSendEmail(AdminUserVM user, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = null;
            bool isMailSent = false;

            try
            {

                if (ModelState.IsValid)
                {
                    user.ModifiedBy = Convert.ToInt32(loggingDetails.UserId);
                    result = _adminService.UpdateAdminUser(user);


                    // Check Admin user is saved or not and then send mail to user
                    if (result.IsSuccess == true && user.IsActive == true)
                    {
                        // reset password
                        string newPassword = RandomPassword.GeneratePassword(8);
                        var changePwdResult = _adminService.ChangePasswordForLoginUser(newPassword, user.Id);

                        string messageBody = _authService.GetEmailTemplateHtmlStringByName("EmailTemplateAccessCredential");
                        messageBody = messageBody.Replace("ReplaceFullName", user.FirstName);
                        messageBody = messageBody.Replace("ReplaceUrl", "http://" + this.Request.Host.Value + "/Account/Login");
                        messageBody = messageBody.Replace("ReplaceUserName", user.EmailAddress);
                        messageBody = messageBody.Replace("ReplacePassword", newPassword);
                        messageBody = messageBody.Replace("<div>", "<div style" + "=" + "font-family:" + "Arial" + ";" + ">");


                        _smtpMessage.Subject = "Your login account for the Pickfords Intranet Administration System";

                        _smtpMessage.BodyContent = messageBody;
                        _smtpMessage.ToAddress = user.EmailAddress;
                        isMailSent = _smtpMessage.Send();
                        result.IsSuccess = isMailSent;

                    }
                }

                if (isMailSent)
                {
                    result.Message = "The login credentials have been sent to this user.";
                }

                else
                {
                    result.Message = "The admin user has been updated successfully.";
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
        /// Delete Admin Users 
        /// </summary>
        /// <param name="allUserIds">List of Admin User IDs</param>
        /// <returns>Success/Failuare as Baseresult</returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult DeleteAdminUsers(DeleteItemVM targetIds, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = new BaseResult();
            targetIds.DeletedBy = Convert.ToInt32(loggingDetails.UserId);

            try
            {
                result = _adminService.DeleteAdmunUsersByIds(targetIds);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }

            return Ok(result);
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
            bool isExist = _adminService.IsEmailExist(emailAddress, id);
            if (isExist)
                return Json(data: "This email address is already associated with an existing Intranet user.");

            return Json(data: true);
        }

        #endregion

        #region Featured Message

        /// <summary>
        /// Get Featured Message grid view
        /// </summary>
        /// <returns>Message list view</returns>
        public IActionResult FeaturedMessage()
        {
            IEnumerable<FeaturedMessageGridItemVM> result = _adminService.GetAllFeaturedMessages();
            return View(result);
        }



        /// <summary>
        /// Get Add Message view
        /// </summary>
        /// <returns>Add Message view</returns>
        [HttpGet]
        [AutoPopulateLoggingDetails]
        public IActionResult AddFeaturedMessage(UserActionLoggingDetails loggingDetails)
        {
            FeaturedMessageVM obj = new FeaturedMessageVM();
            obj.CreationDateString = DateTime.Now.ToString("MM/dd/yyyy");
            obj.AuthorName = loggingDetails.FullName;
            return View(obj);
        }

        /// <summary>
        /// Add Featured Message
        /// </summary>
        /// <returns>BaseResult (it has been saved or not)</returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult AddFeaturedMessage([Bind] FeaturedMessageVM message, UserActionLoggingDetails loggingDetails)

        {

            BaseResult result = new BaseResult();
            result.IsSuccess = true;
            result.Message = "Test";

            try
            {
                if (ModelState.IsValid)
                {
                    if (message.MessageImage != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            message.MessageImage.CopyTo(memoryStream);
                            string imageName = Guid.NewGuid() + Path.GetFileName(message.MessageImage.FileName).Trim().Replace(" ", String.Empty);
                            string imagePath = _pathProvider.MapPath("") + @"\Uploads\Admin\FeaturedMessage\" + imageName;
                            System.IO.File.WriteAllBytes(imagePath, memoryStream.ToArray());
                            message.ImageName = imageName;
                        }
                    }
                    message.CreatedBy = Convert.ToInt32(loggingDetails.UserId);
                    result = _adminService.SaveFeaturedMessage(message);
                }
            }

            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Edit fetured message view
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>View of Edit Admin User</returns>
        public IActionResult EditFeaturedMessage(int id)
        {
            try
            {
                FeaturedMessageVM feturedMessage = _adminService.GetFeaturedMessageById(id);

                return View(feturedMessage);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("Error");
            }

        }

        /// <summary>
        /// Update featured message
        /// </summary>
        /// <param name="feturedMessage">Featured messager that will be updated</param>
        /// <param name="loggingDetails">logged-in User Info</param>
        /// <returns></returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult EditFeaturedMessage(FeaturedMessageVM feturedMessage, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = null;
            ModelState.Remove("MessageImage");
            try
            {
                if (ModelState.IsValid)
                {
                    if (feturedMessage.MessageImage != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            feturedMessage.MessageImage.CopyTo(memoryStream);
                            string imageName = Guid.NewGuid() + Path.GetFileName(feturedMessage.MessageImage.FileName).Trim().Replace(" ", String.Empty);
                            string imagePath = _pathProvider.MapPath("") + @"\Uploads\Admin\FeaturedMessage\" + imageName;
                            System.IO.File.WriteAllBytes(imagePath, memoryStream.ToArray());
                            feturedMessage.ImageName = imageName;
                        }
                    }

                    feturedMessage.ModifiedBy = Convert.ToInt32(loggingDetails.UserId);
                    result = _adminService.UpdateFeaturedMessage(feturedMessage);
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
        /// Delete featured Messages
        /// </summary>
        /// <param name="allUserIds">List of Fatured Message IDs</param>
        /// <returns>Success/Failuare as Baseresult</returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult DeleteFeaturedMessage(DeleteItemVM targetIds, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = new BaseResult();
            targetIds.DeletedBy = Convert.ToInt32(loggingDetails.UserId);

            try
            {
                result = _adminService.DeleteFeaturedMessageByIds(targetIds);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }

            return Ok(result);
        }


        #endregion

        #region Our Values
        /// <summary>
        /// Get OurValues view in edit mode
        /// </summary>
        /// <returns>OurValues view in edit mode</returns>
        public IActionResult OurValues()
        {
            // Get our Values information from data source
            OurValuesVM ourValues = new OurValuesVM();
            ourValues = _adminService.GetOurValues();
            return View(ourValues);
        }

        /// <summary>
        /// Update the OurValues information in system
        /// </summary>
        /// <param name="ourValues">OurValues data structure object</param>
        /// <param name="loggingDetails">logged-In user details</param>
        /// <returns>BaseResult</returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult UpdateOurValues(OurValuesVM ourValues, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = new BaseResult();
            try
            {
                if (ModelState.IsValid)
                {
                    // To Save Background Image
                    if (ourValues.ValueBackgrountImageData != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            ourValues.ValueBackgrountImageData.CopyTo(memoryStream);
                            ourValues.ValueBackgroundImage = Guid.NewGuid() + Path.GetFileName(ourValues.ValueBackgrountImageData.FileName).Trim().Replace(" ", String.Empty);
                            string backGroundImagePath = _pathProvider.MapPath("") + @"\Uploads\Admin\OurValues\" + ourValues.ValueBackgroundImage;
                            System.IO.File.WriteAllBytes(backGroundImagePath, memoryStream.ToArray());

                        }
                    }

                    // To Save Communication Icon
                    if (ourValues.CommunicationIconData != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            ourValues.CommunicationIconData.CopyTo(memoryStream);
                            ourValues.CommunicationIcon = Guid.NewGuid() + Path.GetFileName(ourValues.CommunicationIconData.FileName).Trim().Replace(" ", String.Empty);
                            string communicationIconpath = _pathProvider.MapPath("") + @"\Uploads\Admin\OurValues\" + ourValues.CommunicationIcon;
                            System.IO.File.WriteAllBytes(communicationIconpath, memoryStream.ToArray());

                        }
                    }

                    // To Save Communication Image
                    if (ourValues.CommunicationImageData != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            ourValues.CommunicationImageData.CopyTo(memoryStream);
                            ourValues.CommunicationImage = Guid.NewGuid() + Path.GetFileName(ourValues.CommunicationImageData.FileName).Trim().Replace(" ", String.Empty);
                            string communicationImagePath = _pathProvider.MapPath("") + @"\Uploads\Admin\OurValues\" + ourValues.CommunicationImage;
                            System.IO.File.WriteAllBytes(communicationImagePath, memoryStream.ToArray());

                        }
                    }

                    // To Save Dedication Icon
                    if (ourValues.DedicationIconData != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            ourValues.DedicationIconData.CopyTo(memoryStream);
                            ourValues.DedicationIcon = Guid.NewGuid() + Path.GetFileName(ourValues.DedicationIconData.FileName).Trim().Replace(" ", String.Empty);
                            string dedicationIconPath = _pathProvider.MapPath("") + @"\Uploads\Admin\OurValues\" + ourValues.DedicationIcon;
                            System.IO.File.WriteAllBytes(dedicationIconPath, memoryStream.ToArray());

                        }
                    }

                    // To Save Dedication Image
                    if (ourValues.DedicationImageData != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            ourValues.DedicationImageData.CopyTo(memoryStream);
                            ourValues.DedicationImage = Guid.NewGuid() + Path.GetFileName(ourValues.DedicationImageData.FileName).Trim().Replace(" ", String.Empty);
                            string dedicationImagePath = _pathProvider.MapPath("") + @"\Uploads\Admin\OurValues\" + ourValues.DedicationImage;
                            System.IO.File.WriteAllBytes(dedicationImagePath, memoryStream.ToArray());

                        }
                    }

                    // To Save care Icon
                    if (ourValues.CareIconData != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            ourValues.CareIconData.CopyTo(memoryStream);
                            ourValues.CareIcon = Guid.NewGuid() + Path.GetFileName(ourValues.CareIconData.FileName).Trim().Replace(" ", String.Empty);
                            string careIconPath = _pathProvider.MapPath("") + @"\Uploads\Admin\OurValues\" + ourValues.CareIcon;
                            System.IO.File.WriteAllBytes(careIconPath, memoryStream.ToArray());

                        }
                    }

                    // To Save Care Image
                    if (ourValues.CareImageData != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            ourValues.CareImageData.CopyTo(memoryStream);
                            ourValues.CareImage = Guid.NewGuid() + Path.GetFileName(ourValues.CareImageData.FileName).Trim().Replace(" ", String.Empty);
                            string careImagePath = _pathProvider.MapPath("") + @"\Uploads\Admin\OurValues\" + ourValues.CareImage;
                            System.IO.File.WriteAllBytes(careImagePath, memoryStream.ToArray());

                        }
                    }

                    // To Save Excellent Icon
                    if (ourValues.ExcellentIconData != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            ourValues.ExcellentIconData.CopyTo(memoryStream);
                            ourValues.ExcellentIcon = Guid.NewGuid() + Path.GetFileName(ourValues.ExcellentIconData.FileName).Trim().Replace(" ", String.Empty);
                            string excellentIconPath = _pathProvider.MapPath("") + @"\Uploads\Admin\OurValues\" + ourValues.ExcellentIcon;
                            System.IO.File.WriteAllBytes(excellentIconPath, memoryStream.ToArray());

                        }
                    }

                    // To Save Excellent Image
                    if (ourValues.ExcellentImageData != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            ourValues.ExcellentImageData.CopyTo(memoryStream);
                            ourValues.ExcellentImage = Guid.NewGuid() + Path.GetFileName(ourValues.ExcellentImageData.FileName).Trim().Replace(" ", String.Empty);
                            string excellentImagePath = _pathProvider.MapPath("") + @"\Uploads\Admin\OurValues\" + ourValues.ExcellentImage;
                            System.IO.File.WriteAllBytes(excellentImagePath, memoryStream.ToArray());

                        }
                    }


                    ourValues.ModifiedBy = Convert.ToInt32(loggingDetails.UserId);
                    result = _adminService.UpdateOurValues(ourValues);
                }
            }
            catch (Exception ex)
            {

                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;

            }

            return Ok(result);

        }

        #endregion

        #region Month Stars

        /// <summary>
        /// Get Stars in grid view
        /// </summary>
        /// <returns>Stars list grid view</returns>
        public IActionResult MonthStars()
        {
            IEnumerable<MonthStarGridItemVM> result = _adminService.GetAllmonthStars();
            return View(result);
        }

        /// <summary>
        /// Get Add Stars view
        /// </summary>
        /// <returns>Add stars view</returns>
        public IActionResult AddMonthStars()
        {

            return View();
        }


        /// <summary>
        /// To Check the New Month already exist or not
        /// </summary>
        /// <param name="NewMonthName">NewMonthName</param>
        /// <param name="id">Id</param>
        /// <returns>Json data :true or string message</returns>
        [AllowAnonymous]
        public IActionResult ValidatNewMonthName(string NewMonthName, int id)
        {
            bool isExist = _adminService.ValidateNewMonthName(NewMonthName, id);
            if (isExist)
                return Json(data: "This month already exists, please enter another one.");

            return Json(data: true);
        }

        /// <summary>
        /// To Check the Month Year already exist or not
        /// </summary>
        /// <param name="MonthYear">MonthYear</param>
        /// <param name="id">Id</param>
        /// <returns>Json data :true or string message</returns>
        [AllowAnonymous]
        public IActionResult ValidateMonthYear(string MonthYear, int id)
        {
            bool isExist = _adminService.ValidateMonthYear(MonthYear, id);
            if (isExist)
                return Json(data: "This month or date already exists, please enter another.");

            return Json(data: true);
        }

        /// <summary>
        /// Add Month Stars information
        /// </summary>
        /// <param name="monthStars">MonthStarsVM object</param>
        /// <param name="loggingDetails"></param>
        /// <returns>BaseResult</returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult AddMonthStars(MonthStarVM monthStars, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = new BaseResult();
            try
            {
                monthStars.Star1 = SetProperValueForStar(monthStars.Star1);
                monthStars.Star1.CreatedBy = Convert.ToInt32(loggingDetails.UserId);

                monthStars.Star2 = SetProperValueForStar(monthStars.Star2);
                monthStars.Star2.CreatedBy = Convert.ToInt32(loggingDetails.UserId);

                monthStars.Star3 = SetProperValueForStar(monthStars.Star3);
                monthStars.Star3.CreatedBy = Convert.ToInt32(loggingDetails.UserId);

                monthStars.Star4 = SetProperValueForStar(monthStars.Star4);
                monthStars.Star4.CreatedBy = Convert.ToInt32(loggingDetails.UserId);

                monthStars.Star5 = SetProperValueForStar(monthStars.Star5);
                monthStars.Star5.CreatedBy = Convert.ToInt32(loggingDetails.UserId);

                monthStars.Star6 = SetProperValueForStar(monthStars.Star6);
                monthStars.Star6.CreatedBy = Convert.ToInt32(loggingDetails.UserId);

                monthStars.Star7 = SetProperValueForStar(monthStars.Star7);
                monthStars.Star7.CreatedBy = Convert.ToInt32(loggingDetails.UserId);

                monthStars.Star8 = SetProperValueForStar(monthStars.Star8);
                monthStars.Star8.CreatedBy = Convert.ToInt32(loggingDetails.UserId);

                monthStars.Star9 = SetProperValueForStar(monthStars.Star9);
                monthStars.Star9.CreatedBy = Convert.ToInt32(loggingDetails.UserId);

                monthStars.Star10 = SetProperValueForStar(monthStars.Star10);
                monthStars.Star10.CreatedBy = Convert.ToInt32(loggingDetails.UserId);

                monthStars.Star11 = SetProperValueForStar(monthStars.Star11);
                monthStars.Star11.CreatedBy = Convert.ToInt32(loggingDetails.UserId);

                monthStars.Star12 = SetProperValueForStar(monthStars.Star12);
                monthStars.Star12.CreatedBy = Convert.ToInt32(loggingDetails.UserId);

                monthStars.CreatedBy = Convert.ToInt32(loggingDetails.UserId);
                result = _adminService.AddMonthStars(monthStars);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }
            return Ok();
        }

        [NonAction]
        private MonthStarDetails SetProperValueForStar(MonthStarDetails stardetail)
        {

            try
            {
                if (stardetail.StarPhotoData != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        stardetail.StarPhotoData.CopyTo(memoryStream);
                        stardetail.StarPhoto = Guid.NewGuid() + Path.GetFileName(stardetail.StarPhotoData.FileName).Trim().Replace(" ", String.Empty);
                        string starPhotoPath = _pathProvider.MapPath("") + @"\Uploads\Admin\Stars\" + stardetail.StarPhoto;
                        System.IO.File.WriteAllBytes(starPhotoPath, memoryStream.ToArray());

                    }
                }
                else if (!string.IsNullOrEmpty(stardetail.FrontEndUserImageForStar))
                {

                    var sourcePath = _pathProvider.MapPath("") + @"\Uploads\Images\FrontEndUser\" + stardetail.FrontEndUserImageForStar;
                    var destinationPath = _pathProvider.MapPath("") + @"\Uploads\Admin\Stars\" + stardetail.FrontEndUserImageForStar;

                    if (!System.IO.File.Exists(destinationPath))
                    {
                        System.IO.File.Copy(sourcePath, destinationPath);
                    }


                    stardetail.StarPhoto = stardetail.FrontEndUserImageForStar;


                }
                

                // If StarValueType does not select then set null
                if (stardetail.StarValueType.Contains("select"))
                {
                    stardetail.StarValueType = null;
                }

                return stardetail;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Get Edit View for a month stars entity
        /// </summary>
        /// <param name="id">MonthStars Id</param>
        /// <returns>View to edit the month stars</returns>
        public IActionResult EditMonthStars(int id)
        {
            try
            {
                MonthStarVM monthStar = _adminService.GetMonthStarsById(id);

                return View(monthStar);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("Error");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="monthStar"></param>
        /// <param name="loggingDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult EditMonthStars(MonthStarVM monthStars, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = null;
            int loggedInUserId = Convert.ToInt32(loggingDetails.UserId);

            try
            {
                if (ModelState.IsValid)
                {
                    monthStars.ModifiedBy = loggedInUserId;
                    monthStars.Star1 = SetProperValueForStar(monthStars.Star1);
                    monthStars.Star1.CreatedBy = loggedInUserId;

                    monthStars.Star2 = SetProperValueForStar(monthStars.Star2);
                    monthStars.Star2.CreatedBy = loggedInUserId;

                    monthStars.Star3 = SetProperValueForStar(monthStars.Star3);
                    monthStars.Star3.CreatedBy = loggedInUserId;

                    monthStars.Star4 = SetProperValueForStar(monthStars.Star4);
                    monthStars.Star4.CreatedBy = loggedInUserId;

                    monthStars.Star5 = SetProperValueForStar(monthStars.Star5);
                    monthStars.Star5.CreatedBy = loggedInUserId;

                    monthStars.Star6 = SetProperValueForStar(monthStars.Star6);
                    monthStars.Star6.CreatedBy = loggedInUserId;

                    monthStars.Star7 = SetProperValueForStar(monthStars.Star7);
                    monthStars.Star7.CreatedBy = loggedInUserId;

                    monthStars.Star8 = SetProperValueForStar(monthStars.Star8);
                    monthStars.Star8.CreatedBy = loggedInUserId;

                    monthStars.Star9 = SetProperValueForStar(monthStars.Star9);
                    monthStars.Star9.CreatedBy = loggedInUserId;

                    monthStars.Star10 = SetProperValueForStar(monthStars.Star10);
                    monthStars.Star10.CreatedBy = loggedInUserId;

                    monthStars.Star11 = SetProperValueForStar(monthStars.Star11);
                    monthStars.Star11.CreatedBy = loggedInUserId;

                    monthStars.Star12 = SetProperValueForStar(monthStars.Star12);
                    monthStars.Star12.CreatedBy = loggedInUserId;

                    monthStars.ModifiedBy = loggedInUserId;
                    result = _adminService.UpdateMonthStars(monthStars);
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
        /// Delete Month Stars
        /// </summary>
        /// <param name="allUserIds">List of Month Stars IDs</param>
        /// <returns>Success/Failuare as Baseresult</returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult DeleteMonthStars(DeleteItemVM targetIds, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = new BaseResult();
            targetIds.DeletedBy = Convert.ToInt32(loggingDetails.UserId);

            try
            {
                result = _adminService.DeleteMonthStarsByIds(targetIds);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }

            return Ok(result);
        }

        [AllowAnonymous]
        public JsonResult GetStarsImageList(string personName, string JobTitle)
        {
            List<FrontEndUser> user = new List<FrontEndUser>();



            var loggedUserId = 0; //nvert.ToInt32(TempData["FrontEndUserId"]);
            user = _frontEndService.GetPersonsList(personName, JobTitle, loggedUserId);



            return Json(user);
        }

        #endregion

        #region Gazetteers

        /// <summary>
        /// Get Gazetteers list grid view
        /// </summary>
        /// <returns>Gazetteers list grid view</returns>
        public IActionResult Gazetteers()
        {
            IEnumerable<GazetteersGridItemVM> gazetteersList = _adminService.GetAllGazetteers();
            return View(gazetteersList);
        }

        [AutoPopulateLoggingDetails]
        public IActionResult AddGazetteers(UserActionLoggingDetails loggingDetails)
        {
            GazetteersVM gazetteers = new GazetteersVM();
            gazetteers.AuthorName = loggingDetails.FullName;
            gazetteers.CreationDateToDisplay = DateTime.Now.Month + "/" + DateTime.Now.Day + "/" + DateTime.Now.Year;
            return View(gazetteers);
        }

        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult DeleteGazetteers(DeleteItemVM targetIds, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = new BaseResult();
            targetIds.DeletedBy = Convert.ToInt32(loggingDetails.UserId);

            try
            {
                result = _adminService.DeleteGazetteersByIds(targetIds);
            }
            catch (Exception ex)
            {

                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult AddGazetteers(GazetteersVM gazetteers, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = new BaseResult();
            try
            {
                var newFileName = "";
                if (gazetteers.FileNameData != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        gazetteers.FileNameData.CopyTo(memoryStream);
                        gazetteers.FileName = Guid.NewGuid() + Path.GetFileName(gazetteers.FileNameData.FileName).Trim().Replace(" ", String.Empty);
                        string fileNamePath = _pathProvider.MapPath("") + @"\Uploads\Admin\Gazetteers\" + gazetteers.FileName;
                        System.IO.File.WriteAllBytes(fileNamePath, memoryStream.ToArray());
                        newFileName = gazetteers.FileName;
                        //if(Path.GetExtension(fileNamePath).ToLower()==".xls")
                        //{
                        //    newFileName =  ConvertToXlsx(fileNamePath);
                        //}
                    }
                }
                gazetteers.AuthorName = loggingDetails.FullName;
                gazetteers.GazetteersData = ImportGazetteersData(newFileName);

                if (!string.IsNullOrEmpty(gazetteers.GazetteersData.ExcelErrorMessage))
                {
                    result.IsSuccess = false;
                    result.Message = gazetteers.GazetteersData.ExcelErrorMessage;
                }
                //else if (!string.IsNullOrEmpty(gazetteers.GazetteersData.TerritoryErrorMessage))
                //{
                //    result.IsSuccess = false;
                //    result.Message = gazetteers.GazetteersData.TerritoryErrorMessage;
                //}
                //else if (!string.IsNullOrEmpty(gazetteers.GazetteersData.SalesCentresErrorMessage))
                //{
                //    result.IsSuccess = false;
                //    result.Message = gazetteers.GazetteersData.SalesCentresErrorMessage;
                //}
                //else if (!string.IsNullOrEmpty(gazetteers.GazetteersData.OpsLocationErrorMessage))
                //{
                //    result.IsSuccess = false;
                //    result.Message = gazetteers.GazetteersData.OpsLocationErrorMessage;
                //}
                else
                {
                    //gazetteers.GazetteersData = ImportGazetteersData(newFileName);
                    gazetteers.CreatedBy = Convert.ToInt32(loggingDetails.UserId);

                    result = _adminService.AddGazetteers(gazetteers);
                }


                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = ex.Message;
                return Ok(result);
            }


        }

        [NonAction]
        public string ConvertToXlsx(String filepath)
        {
            //var file = Directory.GetFiles(filepath);
            try
            {
                var newFilePath = filepath + "x";
                //var app = new Microsoft.Office.Interop.Excel.Application();
                //var wb = app.Workbooks.Open(filepath.ToString());
                //app.DisplayAlerts = false;
                //wb.SaveAs(Filename: filepath + "x", FileFormat: Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook);
                //wb.Close();
                //app.Quit();
                return newFilePath;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Extract Excel data into Gazetteers Data
        /// </summary>
        /// <param name="fileName">Excel File Name</param>
        /// <returns>Gazetteers Data</returns>
        [NonAction]
        public GazetteersData ImportGazetteersData(string fileName)
        {
            try
            {
                string rootFolder = _pathProvider.MapPath("") + @"\Uploads\Admin\Gazetteers\";

                FileInfo file = new FileInfo(Path.Combine(rootFolder, fileName));

                using (ExcelPackage package = new ExcelPackage(file))
                {
                    List<GazetteersPickfordsMoveCentreTerritory> listTerritetory = new List<GazetteersPickfordsMoveCentreTerritory>();
                    List<GazetteersSalesCentres> listSalesCentres = new List<GazetteersSalesCentres>();
                    List<GazetteersOPSLocations> listOpsLocation = new List<GazetteersOPSLocations>();
                    GazetteersData gazetteersData = new GazetteersData();

                    // Count=5 => To Check the xlsm file and Count=4 => To Check sample xslx file format
                    if (package.Workbook.Worksheets.Count == 4 || package.Workbook.Worksheets.Count == 5)
                    {
                        if (package.Workbook.Worksheets[2].Name == "Pickfords MOVE CENTRE TERRITORY")
                        {
                            // Extract Sheet-1 Data [GazetteersPickfordsMoveCentreTerritory] from Excel
                            ExcelWorksheet workSheetTerritory = package.Workbook.Worksheets[2];
                            //if (workSheetTerritory.Cells.Count() == 6)
                            //{
                            int territoryTotalRows = workSheetTerritory.Dimension.Rows;
                            if (territoryTotalRows > 0)
                            {
                                if (Convert.ToString(workSheetTerritory.Cells[1, 1].Value) == "POST DISTRICT" &&
                                    Convert.ToString(workSheetTerritory.Cells[1, 2].Value) == "SALES CENTRE" &&
                                    Convert.ToString(workSheetTerritory.Cells[1, 3].Value) == "MOVE CENTRE" &&
                                    Convert.ToString(workSheetTerritory.Cells[1, 4].Value) == "CODE" &&
                                    Convert.ToString(workSheetTerritory.Cells[1, 5].Value) == "WH" &&
                                    Convert.ToString(workSheetTerritory.Cells[1, 6].Value) == ""
                                   )
                                {
                                    for (int i = 2; i <= territoryTotalRows; i++)
                                    {
                                        listTerritetory.Add(new GazetteersPickfordsMoveCentreTerritory
                                        {
                                            PostDistrict = Convert.ToString(workSheetTerritory.Cells[i, 1].Value),
                                            SalesCentre = Convert.ToString(workSheetTerritory.Cells[i, 2].Value),
                                            MoveCentre = Convert.ToString(workSheetTerritory.Cells[i, 3].Value),
                                            Code = Convert.ToString(workSheetTerritory.Cells[i, 4].Value)
                                        });
                                    }
                                }
                                else
                                {
                                    //gazetteersData.TerritoryErrorMessage = "Invalid Columns Sequence in 'Pickfords MOVE CENTRE TERRITORY' excel worksheet";
                                    gazetteersData.ExcelErrorMessage = "The spreadsheet you have uploaded is in the wrong format. Please  upload the correct Gazateer format. ";
                                }
                            }
                            else
                            {
                                //gazetteersData.TerritoryErrorMessage = "'Pickfords MOVE CENTRE TERRITORY' excel worksheet is empty";
                                gazetteersData.ExcelErrorMessage = "The spreadsheet you have uploaded is in the wrong format. Please  upload the correct Gazateer format. ";
                            }
                            //}
                        }
                        else
                        {
                            //gazetteersData.TerritoryErrorMessage = "Invalid filename at index 2 in excel worksheet";
                            gazetteersData.ExcelErrorMessage = "The spreadsheet you have uploaded is in the wrong format. Please  upload the correct Gazateer format. ";
                        }

                        if (package.Workbook.Worksheets[3].Name == "Sales Centres")
                        {
                            // Extract Sheet-2 Data [GazetteersSalesCentres] from Excel
                            ExcelWorksheet workSheetSalesCentres = package.Workbook.Worksheets[3];
                            //if (workSheetSalesCentres.Cells.Count() == 6)
                            //{
                            int salesCentreTotalRows = workSheetSalesCentres.Dimension.Rows;
                            if (salesCentreTotalRows > 0)
                            {
                                if (Convert.ToString(workSheetSalesCentres.Cells[1, 1].Value) == "BRANCH" &&
                                   Convert.ToString(workSheetSalesCentres.Cells[1, 2].Value) == "Customer Number" &&
                                   Convert.ToString(workSheetSalesCentres.Cells[1, 3].Value) == "GROUP SALES MANAGER" &&
                                   Convert.ToString(workSheetSalesCentres.Cells[1, 4].Value) == "CUSTOMER SERVICE MANAGER" &&
                                   Convert.ToString(workSheetSalesCentres.Cells[1, 5].Value) == "AREA MANAGER" &&
                                   Convert.ToString(workSheetSalesCentres.Cells[1, 6].Value) == "OPS DEPT"
                                  )
                                {
                                    for (int i = 2; i <= salesCentreTotalRows; i++)
                                    {
                                        listSalesCentres.Add(new GazetteersSalesCentres
                                        {
                                            Branch = Convert.ToString(workSheetSalesCentres.Cells[i, 1].Value),
                                            CustomerNumber = Convert.ToString(workSheetSalesCentres.Cells[i, 2].Value),
                                            GroupSalesManager = Convert.ToString(workSheetSalesCentres.Cells[i, 3].Value),
                                            CustomerServiceManager = Convert.ToString(workSheetSalesCentres.Cells[i, 4].Value),
                                            AreaManager = Convert.ToString(workSheetSalesCentres.Cells[i, 5].Value),
                                            OpsDept = Convert.ToString(workSheetSalesCentres.Cells[i, 6].Value)
                                        });
                                    }
                                }
                                else
                                {
                                    //gazetteersData.SalesCentresErrorMessage = "Invalid Columns Sequence in 'Sales Centres' excel worksheet";
                                    gazetteersData.ExcelErrorMessage = "The spreadsheet you have uploaded is in the wrong format. Please  upload the correct Gazateer format. ";
                                    return gazetteersData;
                                }
                            }
                            else
                            {
                                //gazetteersData.SalesCentresErrorMessage = "'Sales Centres' excel worksheet is empty";
                                gazetteersData.ExcelErrorMessage = "The spreadsheet you have uploaded is in the wrong format. Please  upload the correct Gazateer format. ";
                                return gazetteersData;
                            }
                            //}
                        }
                        else
                        {
                            //gazetteersData.SalesCentresErrorMessage = "Invalid filename at index 3 in excel worksheet";
                            gazetteersData.ExcelErrorMessage = "The spreadsheet you have uploaded is in the wrong format. Please  upload the correct Gazateer format. ";
                            return gazetteersData;
                        }

                        if (package.Workbook.Worksheets[4].Name == "OPS Locations")
                        {
                            // Extract Sheet-3 Data [GazetteersOPSLocations] from Excel
                            ExcelWorksheet workSheetOpsLocation = package.Workbook.Worksheets[4];
                            //if (workSheetOpsLocation.Cells.Count() == 19)
                            //{
                            int totalRows = workSheetOpsLocation.Dimension.Rows;
                            if (totalRows > 0)
                            {
                                if (Convert.ToString(workSheetOpsLocation.Cells[1, 1].Value) == "LOCATION" &&
                                      Convert.ToString(workSheetOpsLocation.Cells[1, 2].Value) == "AREA & ADDRESS" &&
                                      Convert.ToString(workSheetOpsLocation.Cells[1, 3].Value) == "AREA & ADDRESS" &&
                                      Convert.ToString(workSheetOpsLocation.Cells[1, 4].Value) == "" &&
                                      Convert.ToString(workSheetOpsLocation.Cells[1, 5].Value) == "" &&
                                      Convert.ToString(workSheetOpsLocation.Cells[1, 6].Value) == "" &&
                                      Convert.ToString(workSheetOpsLocation.Cells[1, 7].Value) == "" &&
                                      Convert.ToString(workSheetOpsLocation.Cells[1, 8].Value) == "" &&
                                      Convert.ToString(workSheetOpsLocation.Cells[1, 9].Value) == "RQM" &&
                                      Convert.ToString(workSheetOpsLocation.Cells[1, 10].Value) == "Ops Contact" &&
                                      Convert.ToString(workSheetOpsLocation.Cells[1, 11].Value) == "OPS DEPT" &&
                                      Convert.ToString(workSheetOpsLocation.Cells[1, 12].Value) == "Area Manager" &&
                                      Convert.ToString(workSheetOpsLocation.Cells[1, 13].Value) == "CSM" //&&
                                      //Convert.ToString(workSheetOpsLocation.Cells[1, 14].Value) == "MC" &&
                                      //Convert.ToString(workSheetOpsLocation.Cells[1, 15].Value) == "MC" &&
                                      //Convert.ToString(workSheetOpsLocation.Cells[1, 16].Value) == "MC Email 1" &&
                                      //Convert.ToString(workSheetOpsLocation.Cells[1, 17].Value) == "MC Email 2" &&
                                      //Convert.ToString(workSheetOpsLocation.Cells[1, 18].Value) == "MC Email 3" &&
                                      //Convert.ToString(workSheetOpsLocation.Cells[1, 19].Value) == "STORE"
                                     )
                                {
                                    for (int i = 2; i <= totalRows; i++)
                                    {
                                        GazetteersOPSLocations location = new GazetteersOPSLocations();
                                        location.Location = Convert.ToString(workSheetOpsLocation.Cells[i, 1].Value);
                                        location.AreaAndAddress1 = Convert.ToString(workSheetOpsLocation.Cells[i, 2].Value);
                                        location.AreaAndAddress2 = Convert.ToString(workSheetOpsLocation.Cells[i, 3].Value);
                                        location.AreaAndAddress3 = Convert.ToString(workSheetOpsLocation.Cells[i, 4].Value);
                                        location.AreaAndAddress4 = Convert.ToString(workSheetOpsLocation.Cells[i, 5].Value);
                                        location.AreaAndAddress5 = Convert.ToString(workSheetOpsLocation.Cells[i, 6].Value);
                                        location.AreaAndAddress6 = Convert.ToString(workSheetOpsLocation.Cells[i, 7].Value);
                                        location.AreaAndAddress7 = Convert.ToString(workSheetOpsLocation.Cells[i, 8].Value);
                                        location.RQM = Convert.ToString(workSheetOpsLocation.Cells[i, 9].Value);
                                        location.OpsContact = Convert.ToString(workSheetOpsLocation.Cells[i, 10].Value);
                                        location.OpsDept = Convert.ToString(workSheetOpsLocation.Cells[i, 11].Value);
                                        location.AreaManager = Convert.ToString(workSheetOpsLocation.Cells[i, 12].Value);
                                        location.CSM = Convert.ToString(workSheetOpsLocation.Cells[i, 1].Value);
                                        listOpsLocation.Add(location);
                                    }
                                }
                                else
                                {
                                    //gazetteersData.OpsLocationErrorMessage = "Invalid Columns Sequence in 'OPS Locations' excel worksheet";
                                    gazetteersData.ExcelErrorMessage = "The spreadsheet you have uploaded is in the wrong format. Please  upload the correct Gazateer format. ";
                                    return gazetteersData;
                                }
                            }
                            else
                            {
                                //gazetteersData.OpsLocationErrorMessage = "'OPS Locations' excel worksheet is empty";
                                gazetteersData.ExcelErrorMessage = "The spreadsheet you have uploaded is in the wrong format. Please  upload the correct Gazateer format. ";
                                return gazetteersData;
                            }
                            //}
                        }
                        else
                        {
                            //gazetteersData.OpsLocationErrorMessage = "Invalid filename at index 4 in excel worksheet";
                            gazetteersData.ExcelErrorMessage = "The spreadsheet you have uploaded is in the wrong format. Please  upload the correct Gazateer format. ";
                            return gazetteersData;
                        }

                        gazetteersData.TerritetoryList = listTerritetory;
                        gazetteersData.SalesCentresList = listSalesCentres;
                        gazetteersData.OperationLocationsList = listOpsLocation;
                        return gazetteersData;
                    }
                    else
                    {
                        gazetteersData.ExcelErrorMessage = "The spreadsheet you have uploaded is in the wrong format. Please  upload the correct Gazateer format. ";
                        return gazetteersData;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }


}

