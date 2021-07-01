using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using PickfordsIntranet.Core.DomainServices;
using PickfordsIntranet.Core.DomainServices.FrontEnd;
using PickfordsIntranet.Core.Helper;
using PickfordsIntranet.ViewModels.EndUser;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.Rewards;
using PickfordsIntranet.WebAdmin.Filters;
using PickfordsIntranet.WebAdmin.Utility;

namespace PickfordsIntranet.WebAdmin.Controllers
{
    [Authorize(Roles = "SA,LM")]
    public class RewardController : Controller
    {

        #region Private Member

        /// <summary>
        /// INewsService data member
        /// </summary>
        private IFronEndService _frontEndService;



        /// <summary>
        /// IAdminService data member
        /// </summary>
        private IAdminService _adminService;

        private IRewardService _rewardService;

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
        /// RewardController constructor
        /// </summary>
        /// <param name="adminService">admin service object</param>
        /// <param name="logger">logger object</param>
        /// <param name="viewParser">View Parser object</param>
        public RewardController(IAdminService adminService, IAuthService authService, IConfigurationRoot config, ILogger<SuperAdminController> logger, IViewParser viewParser
            , Utility.SmtpMessage smtpMessage
            , IPathProvider pathProvider, IFronEndService frontEndService, IRewardService rewardService)
        {
            _config = config;
            _adminService = adminService;
            _authService = authService;
            _logger = logger;
            _viewParser = viewParser;
            _smtpMessage = smtpMessage;
            _pathProvider = pathProvider;
            _frontEndService = frontEndService;
            _rewardService = rewardService;
        }

        #endregion


        #region Rewards

        [AutoPopulateLoggingDetails]
        [Route("Reward/index")]
        public IActionResult Rewards(UserActionLoggingDetails loggingDetails)
        {
            List<RewardGridItemVM> model = new List<RewardGridItemVM>();
            var userId = 0;
            if (loggingDetails.RoleName != "SA")
            {

                userId = Convert.ToInt32(loggingDetails.UserId);
            }
            model = _rewardService.GetAllRewards(userId);

            return View(model);
        }

        [AutoPopulateLoggingDetails]
        public IActionResult Add(UserActionLoggingDetails loggingDetails)
        {
            try
            {
                var result = _rewardService.GetPreRequisitesDataToAddReward(loggedInUserId: Convert.ToInt32(loggingDetails.UserId));
               
                return View(result);
            }

            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("Error");
            }
        }

        [AutoPopulateLoggingDetails]
        public IActionResult Edit(int id, UserActionLoggingDetails loggingDetails)
        {
            try
            {
                RewardVM reward = _rewardService.GetRewardById(id, loggedInUserId: Convert.ToInt32(loggingDetails.UserId));
                
                return View(reward);
            }

            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("Error");
            }
        }

        [AutoPopulateLoggingDetails]
        public IActionResult Details(int id, UserActionLoggingDetails loggingDetails)
        {
            try
            {
                RewardVM reward = _rewardService.GetRewardToDisplayById(id, loggedInUserId: Convert.ToInt32(loggingDetails.UserId));
                return View(reward);
            }

            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("Error");
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
        public IActionResult UpsertReward([Bind] RewardVM reward, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = null;

            try
            {
                if (ModelState.IsValid)
                {
                    // get the reward sent or not flag IsSend from DB
                    reward.IsSend = _rewardService.IsRewardSend(reward.Id);
                    reward.CreatedBy = Convert.ToInt32(loggingDetails.UserId);
                    result = _rewardService.SaveReward(reward);
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
        public IActionResult UpsertRewardAndsendMail([Bind] RewardVM reward, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = new BaseResult();

            try
            {
                if (ModelState.IsValid)
                {
                    // All parameters that is required to send mail
                    RewardSendMailVM objRewardToSendMail = new RewardSendMailVM();

                    objRewardToSendMail.LineManagerEmailAddress = loggingDetails.Username;
                    objRewardToSendMail.ReplaceLineManager = loggingDetails.FullName;
                    objRewardToSendMail.ReplaceRecipientFirstName = reward.RecipientName.Trim().Split(" ")[0];
                    objRewardToSendMail.ReplaceRecipientFullName = reward.RecipientName;
                    objRewardToSendMail.ReplaceValue = reward.ValueName;
                    objRewardToSendMail.ReplaceDateOfAward = DateTime.Now.ToShortDateString();
                    objRewardToSendMail.ReplaceTestimonial = reward.Testimonial;
                    objRewardToSendMail.ReplaceThankYouMessage = reward.ThankYouMsg;
                    objRewardToSendMail.ReplaceHRManager = "HR Team";
                    objRewardToSendMail.ReplaceUserPicUrl = "http://" + this.Request.Host.Value + reward.RecipientImage;
                    objRewardToSendMail.ReplacePickfordsLogoUrl = "http://" + this.Request.Host.Value + "/images/pickfords_logo_w.png";
                    objRewardToSendMail.ReplaceValueIconUrl = "http://" + this.Request.Host.Value + _config["FileRequestPath"] + "/Uploads/Admin/OurValues/" + _adminService.GetValueIconByValueName(reward.ValueName);
                    objRewardToSendMail.ReplaceRewardAmount = "£" + Convert.ToString(reward.RewardAmount);

                    AwardTypeInfo awardTypeInfo = _adminService.GetAwardById(reward.AwardId);
                    objRewardToSendMail.ReplaceAwardType = awardTypeInfo.AwardName;

                    if (!string.IsNullOrEmpty(awardTypeInfo.AwardTextWording))
                    {
                        objRewardToSendMail.ReplaceRewardRecipientWordingForAward = awardTypeInfo.AwardTextWording.Replace("Amount", Convert.ToString(reward.RewardAmount));
                    }
                    
                    objRewardToSendMail.ReplaceAwardIconUrl = "http://" + this.Request.Host.Value + _config["FileRequestPath"] + "/Uploads/AwardIcons/" + awardTypeInfo.AwardIcon;
                    objRewardToSendMail.HREmailAddress = _config["HREmailAddress"];

                    RewardRecipientInfoVM recipientInfo = _adminService.GetRewardRecipientInfoById(Convert.ToInt32(reward.RecipientId));
                    objRewardToSendMail.ReplaceAwardRecipientEmail = recipientInfo.EmailAddress;
                    objRewardToSendMail.RecipientEmailAddress = recipientInfo.EmailAddress;
                    objRewardToSendMail.ReplaceAwardRecipientTelephoneNumber = recipientInfo.TelephoneNumber;
                    objRewardToSendMail.ReplaceEmployeeRefNumber = recipientInfo.EmployeeNumber;

                    // Send mail to Line Manager OR Super User
                    bool isMailSentToLM = false;
                    if (awardTypeInfo.AwardName.Equals(_config["ServerEnd:SuperAdmin:LineManagerCanTextRecogniseOnly"]))
                    {
                        isMailSentToLM = SendMailToLineManagerWithoutAward(objRewardToSendMail);
                    }

                    else
                    {
                        isMailSentToLM = SendMailToLineManager(objRewardToSendMail);
                    }

                    // Send mail to HR
                    bool isMailSentToHR = false;
                    if (!awardTypeInfo.AwardName.Equals(_config["ServerEnd:SuperAdmin:LineManagerCanTextRecogniseOnly"]))
                    {
                        isMailSentToHR = SendMailToHR(objRewardToSendMail);
                    }

                    else
                    {
                        // Simply set the isMailSentToHR flag to true
                        isMailSentToHR = true;
                    }

                    // Send mail to Reward Recipient
                    bool isMailSentToRecipient = false;
                    
                    if (awardTypeInfo.AwardName.Equals(_config["ServerEnd:SuperAdmin:LineManagerCanTextRecogniseOnly"]))
                    {
                        isMailSentToRecipient= SendMailToRewardRecipientWithoutAward(objRewardToSendMail);
                    }

                    else
                    {
                        isMailSentToRecipient = SendMailToRewardRecipient(objRewardToSendMail);
                    }

                    if (isMailSentToLM && isMailSentToHR && isMailSentToRecipient)
                    {
                        reward.IsSend = true;
                        reward.CreatedBy = Convert.ToInt32(loggingDetails.UserId);
                        result = _rewardService.SaveReward(reward);
                    }

                    else
                    {
                        result.IsSuccess = false;
                        result.Message = StaticResource.SmtpServerErrorMessage;
                    }


                }


                return Ok(result);
            }

            catch (SmtpException ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.SmtpServerErrorMessage;
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

        [NonAction]
        private bool SendMailToLineManager(RewardSendMailVM objRewardToSendMail)
        {
            try
            {
                string messageBody = _authService.GetEmailTemplateHtmlStringByName("EmailTemplateLineManagerAwardInfo");
                //messageBody = messageBody.Replace("<div>", "<div style" + "=" + "font-family:" + "Arial" + ";" + ">");
                messageBody = messageBody.Replace("ReplaceLineManager", objRewardToSendMail.ReplaceLineManager);
                messageBody = messageBody.Replace("ReplaceRecipientFirstName", objRewardToSendMail.ReplaceRecipientFirstName);
                messageBody = messageBody.Replace("ReplaceRecipientFullName", objRewardToSendMail.ReplaceRecipientFullName);
                messageBody = messageBody.Replace("ReplaceValue", objRewardToSendMail.ReplaceValue);
                messageBody = messageBody.Replace("ReplaceDateOfAward", objRewardToSendMail.ReplaceDateOfAward);
                messageBody = messageBody.Replace("ReplaceTestimonial", objRewardToSendMail.ReplaceTestimonial);
                messageBody = messageBody.Replace("ReplaceThankYouMessage", objRewardToSendMail.ReplaceThankYouMessage);
                messageBody = messageBody.Replace("ReplaceAwardRecipientFullName", objRewardToSendMail.ReplaceRecipientFullName);
                messageBody = messageBody.Replace("ReplaceAwardType", objRewardToSendMail.ReplaceAwardType);
                messageBody = messageBody.Replace("ReplaceAwardType", objRewardToSendMail.ReplaceAwardType);
                messageBody = messageBody.Replace("ReplaceRewardAmount", objRewardToSendMail.ReplaceRewardAmount);

                _smtpMessage.Subject = string.Format("Your reward to {0} has been sent!", objRewardToSendMail.ReplaceRecipientFullName);
                _smtpMessage.BodyContent = messageBody;
                _smtpMessage.ToAddress = objRewardToSendMail.LineManagerEmailAddress;
                return _smtpMessage.Send();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [NonAction]
        private bool SendMailToLineManagerWithoutAward(RewardSendMailVM objRewardToSendMail)
        {
            try
            {
                string messageBody = _authService.GetEmailTemplateHtmlStringByName("EmailTemplateLineManagerAwardInfoWithoutAward");
                //messageBody = messageBody.Replace("<div>", "<div style" + "=" + "font-family:" + "Arial" + ";" + ">");
                messageBody = messageBody.Replace("ReplaceLineManager", objRewardToSendMail.ReplaceLineManager);
                messageBody = messageBody.Replace("ReplaceRecipientFirstName", objRewardToSendMail.ReplaceRecipientFirstName);
                messageBody = messageBody.Replace("ReplaceRecipientFullName", objRewardToSendMail.ReplaceRecipientFullName);
                messageBody = messageBody.Replace("ReplaceValue", objRewardToSendMail.ReplaceValue);
                messageBody = messageBody.Replace("ReplaceDateOfAward", objRewardToSendMail.ReplaceDateOfAward);
                messageBody = messageBody.Replace("ReplaceTestimonial", objRewardToSendMail.ReplaceTestimonial);
                messageBody = messageBody.Replace("ReplaceThankYouMessage", objRewardToSendMail.ReplaceThankYouMessage);
                messageBody = messageBody.Replace("ReplaceAwardRecipientFullName", objRewardToSendMail.ReplaceRecipientFullName);
                messageBody = messageBody.Replace("ReplaceAwardType", objRewardToSendMail.ReplaceAwardType);

                _smtpMessage.Subject = string.Format("Your reward to {0} has been sent!", objRewardToSendMail.ReplaceRecipientFullName);
                _smtpMessage.BodyContent = messageBody;
                _smtpMessage.ToAddress = objRewardToSendMail.LineManagerEmailAddress;
                return _smtpMessage.Send();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [NonAction]
        private bool SendMailToHR(RewardSendMailVM objRewardToSendMail)
        {
            try
            {
                string messageBody = _authService.GetEmailTemplateHtmlStringByName("EmailTemplateHRAwardInfo");
                //messageBody = messageBody.Replace("<div>", "<div style" + "=" + "font-family:" + "Arial" + ";" + ">");
                messageBody = messageBody.Replace("ReplaceHRManager", objRewardToSendMail.ReplaceHRManager);
                messageBody = messageBody.Replace("ReplaceRecipientFirstName", objRewardToSendMail.ReplaceRecipientFirstName);
                messageBody = messageBody.Replace("ReplaceRecipientFullName", objRewardToSendMail.ReplaceRecipientFullName);
                messageBody = messageBody.Replace("ReplaceValue", objRewardToSendMail.ReplaceValue);
                messageBody = messageBody.Replace("ReplaceDateOfAward", objRewardToSendMail.ReplaceDateOfAward);
                messageBody = messageBody.Replace("ReplaceTestimonial", objRewardToSendMail.ReplaceTestimonial);
                messageBody = messageBody.Replace("ReplaceThankYouMessage", objRewardToSendMail.ReplaceThankYouMessage);

                messageBody = messageBody.Replace("ReplaceAwardRecipientEmail", objRewardToSendMail.ReplaceAwardRecipientEmail);
                messageBody = messageBody.Replace("ReplaceAwardRecipientTelephoneNumber", objRewardToSendMail.ReplaceAwardRecipientTelephoneNumber);
                messageBody = messageBody.Replace("ReplaceAwardType", objRewardToSendMail.ReplaceAwardType);
                messageBody = messageBody.Replace("ReplaceRewardAmount", objRewardToSendMail.ReplaceRewardAmount);
                messageBody = messageBody.Replace("ReplaceLineManager", objRewardToSendMail.ReplaceLineManager);
                messageBody = messageBody.Replace("ReplaceEmployeeRefNumber", objRewardToSendMail.ReplaceEmployeeRefNumber);

                _smtpMessage.Subject = string.Format("A new reward for {0} has been submitted ", objRewardToSendMail.ReplaceRecipientFullName);
                _smtpMessage.BodyContent = messageBody;
                _smtpMessage.ToAddress = objRewardToSendMail.HREmailAddress;
                return _smtpMessage.Send();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [NonAction]
        private bool SendMailToRewardRecipient(RewardSendMailVM objRewardToSendMail)
        {
            try
            {
                string messageBody = _authService.GetEmailTemplateHtmlStringByName("EmailTemplateAwardRecipient");
                messageBody = messageBody.Replace("ReplacePickfordsLogoUrl", objRewardToSendMail.ReplacePickfordsLogoUrl);
                messageBody = messageBody.Replace("ReplaceOurValue", objRewardToSendMail.ReplaceValue);
                messageBody = messageBody.Replace("ReplaceRecipientFirstName", objRewardToSendMail.ReplaceRecipientFirstName);
                messageBody = messageBody.Replace("ReplaceLineManager", objRewardToSendMail.ReplaceLineManager);

                messageBody = messageBody.Replace("ReplaceUserPicUrl", objRewardToSendMail.ReplaceUserPicUrl);
                messageBody = messageBody.Replace("ReplaceValueIconUrl", objRewardToSendMail.ReplaceValueIconUrl);
                messageBody = messageBody.Replace("ReplaceThankYouMessage", objRewardToSendMail.ReplaceThankYouMessage);
                messageBody = messageBody.Replace("ReplaceAwardIconUrl", objRewardToSendMail.ReplaceAwardIconUrl);                
                messageBody = messageBody.Replace("ReplaceAwardType", objRewardToSendMail.ReplaceRewardRecipientWordingForAward);
                messageBody = messageBody.Replace("ReplaceFrontEndUrl", "http://" + this.Request.Host.Value);

                // Create mailId for this mail and also to generate url for mail
                Guid generatedMailId = Guid.NewGuid();
                messageBody = messageBody.Replace("ReplaceViewInBrowserUrl", "http://" + this.Request.Host.Value + "/intranet/recipientaward/" + Convert.ToString(generatedMailId));

                // Keep mail html string database to view in browser
                
                string mailRecipientHtml = messageBody.Replace("ReplaceBrowserLinkStyle", "display:none;");
                // uncomment for testing the styles
                //string mailRecipientHtml = messageBody.Replace("ReplaceBrowserLinkStyle", "font-family:Arial;background:gray;");

                // insert this mail to GeneratedMail table with GUID and Replace ReplaceViewInBrowserUrl for it in mail content
                var mailGeneratedResult =_adminService.InsertAwardRecipientMailContent(Id: generatedMailId, mailContent: mailRecipientHtml);

                // Styles for Recipient mail that are going to send in mail-box.
                messageBody = messageBody.Replace("ReplaceBrowserLinkStyle", "font-family:Arial;background:gray;");

               


                _smtpMessage.Subject = string.Format("You're a star!");
                _smtpMessage.BodyContent = messageBody;
                _smtpMessage.ToAddress = objRewardToSendMail.RecipientEmailAddress;

                _logger.LogInformation("****************************************");
                _logger.LogInformation(messageBody);
                _logger.LogInformation("****************************************");
                return _smtpMessage.Send();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [NonAction]
        private bool SendMailToRewardRecipientWithoutAward(RewardSendMailVM objRewardToSendMail)
        {
            try
            {

                string messageBody = _authService.GetEmailTemplateHtmlStringByName("EmailTemplateAwardRecipientWithoutAward");
                messageBody = messageBody.Replace("ReplacePickfordsLogoUrl", objRewardToSendMail.ReplacePickfordsLogoUrl);
                messageBody = messageBody.Replace("ReplaceOurValue", objRewardToSendMail.ReplaceValue);
                messageBody = messageBody.Replace("ReplaceRecipientFirstName", objRewardToSendMail.ReplaceRecipientFirstName);
                messageBody = messageBody.Replace("ReplaceLineManager", objRewardToSendMail.ReplaceLineManager);

                messageBody = messageBody.Replace("ReplaceUserPicUrl", objRewardToSendMail.ReplaceUserPicUrl);
                messageBody = messageBody.Replace("ReplaceValueIconUrl", objRewardToSendMail.ReplaceValueIconUrl);
                messageBody = messageBody.Replace("ReplaceThankYouMessage", objRewardToSendMail.ReplaceThankYouMessage);
                messageBody = messageBody.Replace("ReplaceAwardIconUrl", objRewardToSendMail.ReplaceAwardIconUrl);
                messageBody = messageBody.Replace("ReplaceAwardType", objRewardToSendMail.ReplaceAwardType);
                messageBody = messageBody.Replace("ReplaceFrontEndUrl", "http://" + this.Request.Host.Value);

                // Create mailId for this mail and also to generate url for mail
                Guid generatedMailId = Guid.NewGuid();
                messageBody = messageBody.Replace("ReplaceViewInBrowserUrl", "http://" + this.Request.Host.Value + "/intranet/recipientaward/" + Convert.ToString(generatedMailId));

                // Keep mail html string database to view in browser

                string mailRecipientHtml = messageBody.Replace("ReplaceBrowserLinkStyle", "display:none;");
                // uncomment for testing the styles
                //string mailRecipientHtml = messageBody.Replace("ReplaceBrowserLinkStyle", "font-family:Arial;background:gray;");

                // insert this mail to GeneratedMail table with GUID and Replace ReplaceViewInBrowserUrl for it in mail content
                var mailGeneratedResult = _adminService.InsertAwardRecipientMailContent(Id: generatedMailId, mailContent: mailRecipientHtml);

                // Styles for Recipient mail that are going to send in mail-box.
                messageBody = messageBody.Replace("ReplaceBrowserLinkStyle", "font-family:Arial;background:gray;");




                _smtpMessage.Subject = string.Format("You're a star!");
                _smtpMessage.BodyContent = messageBody;
                _smtpMessage.ToAddress = objRewardToSendMail.RecipientEmailAddress;

                _logger.LogInformation("****************************************");
                _logger.LogInformation(messageBody);
                _logger.LogInformation("****************************************");
                return _smtpMessage.Send();
            }
            catch 
            {
                return false;
                //throw ex;
            }
        }

        [AllowAnonymous]
        public JsonResult GetUserListByName(string userName)
        {
            BaseResult result = null;
            try
            {
                List<FrontEndUser> user = new List<FrontEndUser>();
                var loggedUserId = 0; //Convert.ToInt32(TempData["FrontEndUserId"]);
                user = _frontEndService.GetPersonsList(userName, null, loggedUserId);
                return Json(user);

            }
            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
                return Json(result);
            }

        }

        /// <summary>
        /// Delete Rewards
        /// </summary>
        /// <param name="allUserIds">List of Rewards IDs</param>
        /// <returns>Success/Failuare as Baseresult</returns>
        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult DeleteRewards(DeleteItemVM targetIds, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = new BaseResult();
            targetIds.DeletedBy = Convert.ToInt32(loggingDetails.UserId);

            try
            {
                result = _rewardService.DeleteRewardsByIds(targetIds);
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

        //Download Excel report for worklogs
        [HttpGet]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public FileContentResult ExportToExcelRewards(UserActionLoggingDetails loggingDetails)
        {
            var userId = 0;
            if (loggingDetails.RoleName != "SA")
            {

                userId = Convert.ToInt32(loggingDetails.UserId);
            }
            var result = _rewardService.GetRewardsForExcel(userId);

           
            // Add Currency sign in amount
            result.ForEach(r => {

                r.Amount = "£" + r.Amount;
            });


            string[] columns = { "Name", "EmployeeNumber", "Department", "Award", "Amount", "Value", "Date", "Manager" };

            byte[] filecontent = ExcelExportHelper.ExportExcelFirst(result, "Values /rewards ", false, columns);
            return File(filecontent, ExcelExportHelper.ExcelContentType, "RewardsDetail.xlsx");
        }


        #endregion

        #region
        //[AutoPopulateLoggingDetails]
        //[Route("LeaveManagement/Index")]
        //public IActionResult GetAllLeaveRequests(string status, UserActionLoggingDetails loggingDetails)
        //{
        //    LeaveRequest res = new LeaveRequest();
        //    if (loggingDetails.RoleName == "LM" || loggingDetails.RoleName == "SA")
        //    {
        //         res = _rewardService.GetAllLeaveRequests(loggingDetails.Username);
               
        //    }
        //    return View(res);
        //}
        #endregion

    }

    public class ExcelExportHelper
    {
        public static string ExcelContentType
        {
            get
            { return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; }
        }

        // call rest of the methods present here
        public static byte[] ExportExcelFirst<T>(List<T> data, string Heading = "", bool showSlno = false, params string[] ColumnsToTake)
        {
            return ExportExcel(ListToDataTable<T>(data), Heading, showSlno, ColumnsToTake);
        }


        //create datatable by list
        public static DataTable ListToDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dataTable = new DataTable();

            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dataTable.Columns.Add(property.Name, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
            }

            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }

                dataTable.Rows.Add(values);
            }
            return dataTable;
        }


        //convert datat into bytes
        public static byte[] ExportExcel(DataTable dataTable, string heading = "", bool showSrNo = false, params string[] columnsToTake)
        {

            byte[] result = null;
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets.Add(String.Format("{0} Data", heading));
                int startRowFrom = String.IsNullOrEmpty(heading) ? 1 : 3;

                if (showSrNo)
                {
                    DataColumn dataColumn = dataTable.Columns.Add("#", typeof(int));
                    dataColumn.SetOrdinal(0);
                    int index = 1;
                    foreach (DataRow item in dataTable.Rows)
                    {
                        item[0] = index;
                        index++;
                    }
                }
               

                // add the content into the Excel file  
                workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);

                // autofit width of cells with small content  
                int columnIndex = 1;
                foreach (DataColumn column in dataTable.Columns)
                {
                    ExcelRange columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex, workSheet.Dimension.End.Row, columnIndex];
                    int maxLength = columnCells.Max(cell => cell.Value.ToString().Count());
                    if (maxLength < 150)
                    {
                        workSheet.Column(columnIndex).AutoFit();
                    }


                    columnIndex++;
                }

                // format header - bold, yellow on black  
                using (ExcelRange r = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
                {
                    r.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    r.Style.Font.Bold = true;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#1fb5ad"));
                }

                // format cells - add borders  
                using (ExcelRange r = workSheet.Cells[startRowFrom + 1, 1, startRowFrom + dataTable.Rows.Count, dataTable.Columns.Count])
                {
                    r.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    r.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                    r.Style.Border.Top.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Bottom.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Left.Color.SetColor(System.Drawing.Color.Black);
                    r.Style.Border.Right.Color.SetColor(System.Drawing.Color.Black);
                }

                // removed ignored columns  
                for (int i = dataTable.Columns.Count - 1; i >= 0; i--)
                {
                    if (i == 0 && showSrNo)
                    {
                        continue;
                    }
                    if (!columnsToTake.Contains(dataTable.Columns[i].ColumnName))
                    {
                        workSheet.DeleteColumn(i + 1);
                    }
                }

                if (!String.IsNullOrEmpty(heading))
                {
                    workSheet.Cells["A1"].Value = heading;
                    workSheet.Cells["A1"].Style.Font.Size = 20;

                    workSheet.InsertColumn(1, 1);
                    workSheet.InsertRow(1, 1);
                    workSheet.Column(1).Width = 5;
                }

                result = package.GetAsByteArray();
            }

            return result;
        }
    }
}