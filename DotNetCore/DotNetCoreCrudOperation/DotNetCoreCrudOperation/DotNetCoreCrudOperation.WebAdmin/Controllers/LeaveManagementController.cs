using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PickfordsIntranet.Core.DomainServices;
using PickfordsIntranet.Core.DomainServices.FrontEnd;
using PickfordsIntranet.Core.Helper;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.LeaveRequest;
using PickfordsIntranet.WebAdmin.Filters;
using PickfordsIntranet.WebAdmin.Utility;

namespace PickfordsIntranet.WebAdmin.Controllers
{
    [Authorize(Roles = "SA,LM")]
    public class LeaveManagementController : Controller
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

        private IAuthService _authService;

        private ILeaveManagementService _levMgService;
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
        public LeaveManagementController(IAdminService adminService, IAuthService authService, IConfigurationRoot config, ILogger<SuperAdminController> logger, IViewParser viewParser
            , Utility.SmtpMessage smtpMessage
            , IPathProvider pathProvider, IFronEndService frontEndService, ILeaveManagementService levMgService)
        {
            _config = config;
            _adminService = adminService;
            _authService = authService;
            _logger = logger;
            _viewParser = viewParser;
            _smtpMessage = smtpMessage;
            _pathProvider = pathProvider;
            _frontEndService = frontEndService;
            _levMgService = levMgService;
        }

        #endregion

        [AutoPopulateLoggingDetails]
        public IActionResult Index(string status, UserActionLoggingDetails loggingDetails)
        {
            List<LeaveStatusList> res = new List<LeaveStatusList>();
            if (loggingDetails.RoleName == "LM" || loggingDetails.RoleName == "SA")
            {
                if (status == null)
                {
                    status = string.Empty;
                }
                res = _levMgService.GetAllLeaveStatus();
            }
            return View(res);
        }

        [AutoPopulateLoggingDetails]
        public IActionResult GetAllLeaves(string status, UserActionLoggingDetails loggingDetails)
        {
            LeaveRequest res = new LeaveRequest();
            if (loggingDetails.RoleName == "LM" || loggingDetails.RoleName == "SA")
            {
                if (status==null)
                {
                    status = string.Empty;
                }
                res = _levMgService.GetAllLeaveRequests(loggingDetails.Username,status);                
            }
            return PartialView("_GetLeaveRequests", res);
        }

        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult DeleteLeaves(DeleteItemVM leaveIds, UserActionLoggingDetails loggingDetails)
        {
            LeaveCancelledByLM result = new LeaveCancelledByLM();
            leaveIds.DeletedBy = Convert.ToInt32(loggingDetails.UserId);
            try
            {
                result = _levMgService.DeleteLeaveByIds(leaveIds);
                if (result.CancelledLeaveDetails.Count > 0)
                {
                    foreach (var item in result.CancelledLeaveDetails)
                    {
                        string messageBody = _authService.GetEmailTemplateHtmlStringByName("ApprovedLeaveCancelledByLineManager");

                        messageBody = messageBody.Replace("{FirstName}", item.FirstName);
                        messageBody = messageBody.Replace("{RequestedNumberOfDays}", item.LeaveDuration);
                        messageBody = messageBody.Replace("{LeaveStartDate}", Convert.ToDateTime(item.StartDate).ToString("dd-MM-yyyy"));
                        messageBody = messageBody.Replace("{LeaveStartTime}", item.StartTime);
                        messageBody = messageBody.Replace("{LeaveEndDate}", Convert.ToDateTime(item.EndDate).ToString("dd-MM-yyyy"));
                        messageBody = messageBody.Replace("{LeaveEndTime}", item.EndTime);
                        messageBody = messageBody.Replace("ReplacePageURL", "http://" + this.Request.Host.Value + "/intranet/manageleave");
                        messageBody = messageBody.Replace("<div>", "<div style" + "=" + "font-family:" + "Arial" + ";" + ">");

                        // Step 4: Send mail to requested user
                        _smtpMessage.Subject = "Your Leave Request has been cancelled";
                        _smtpMessage.BodyContent = messageBody;
                        // _smtpMessage.ToAddress =  leavereq.EmailAddress;
                        _smtpMessage.ToAddress = "mclancy@idslogic.co.uk";                       
                        bool isSent = _smtpMessage.SendAsync();                        
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                result = new LeaveCancelledByLM();
                result.CancelledLeaveStatus.Message = StaticResource.InternalServerMessage;
                return Ok(result.CancelledLeaveStatus);
            }
            return Ok(result.CancelledLeaveStatus);
        }

        [AutoPopulateLoggingDetails]
        public IActionResult Add(UserActionLoggingDetails loggingDetails,int leaveId)
        {
            try
            {
                LeaveRequestLM objleave = new LeaveRequestLM();
                objleave = _levMgService.GetPreRequisitesDataToAddLeaves(Convert.ToString(loggingDetails.Username),leaveId);
                //objleave.FilePath= "~/fileserver/Uploads/Admin/Leave/" + objleave.FileName;
                return View(objleave);
            }

            catch (Exception ex)
            {
                _logger.LogError((int)EventLevel.Error, ex, ex.Message);
                return View("Error");
            }
        }

        public IActionResult GetEmployeeDetailsById(string empId)
        {
            EmployeeDetails employeeDetails = _levMgService.GetEmployeeDetailsById(empId);
            return PartialView("_EmployeeDetails", employeeDetails);

        }

        [HttpPost]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult AddLMLeaveRequest([Bind] LeaveRequestLM leavereq, string leaveId,UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = new BaseResult();
            try
            {
                if (ModelState.IsValid)
                {
                LeaveRequestLM objleave = new LeaveRequestLM();
                objleave.UserId = leavereq.UserId;
                objleave.LeaveID = leaveId == "" ? 0 : Convert.ToInt32(leaveId);
                objleave.LeaveDuration = leavereq.LeaveDuration;
                objleave.StartDate = Convert.ToDateTime(leavereq.StartDate).ToString("yyyy-MM-dd"); ;
                objleave.StartTime = leavereq.StartTime;
                string replaceEndDate = "";
                string replaceEndTime = "";
                if (leavereq.EndTime == "MORNING")
                {
                    replaceEndDate = Convert.ToDateTime(leavereq.NewEndDate).ToString("dd-MM-yyyy");
                    replaceEndTime = "AFTERNOON";
                }
                else if (leavereq.EndTime == "AFTERNOON")
                {
                    replaceEndDate = Convert.ToDateTime(leavereq.NewEndDate).ToString("dd-MM-yyyy");
                    replaceEndTime = "MORNING";
                }
                objleave.EndDate = Convert.ToDateTime(leavereq.NewEndDate).ToString("yyyy-MM-dd");
                objleave.EndTime = leavereq.EndTime;
                objleave.Status = leavereq.Status;
                objleave.Description = leavereq.Description;
                objleave.LeaveTypeId = leavereq.LeaveTypeId;
                objleave.Comment = leavereq.Comment;
                objleave.ReturnBackDate = Convert.ToDateTime(leavereq.EndDate).ToString("yyyy-MM-dd");
                var newFileName = "";

                if (leavereq.FileNameData != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        leavereq.FileNameData.CopyTo(memoryStream);
                        objleave.FileName = Guid.NewGuid() + Path.GetFileName(leavereq.FileNameData.FileName).Trim().Replace(" ", String.Empty);
                        string fileNamePath = _pathProvider.MapPath("") + @"\Uploads\Admin\Leave\" + objleave.FileName;
                        System.IO.File.WriteAllBytes(fileNamePath, memoryStream.ToArray());
                        newFileName = objleave.FileName;
                    }
                }
                else
                {
                    if (Convert.ToInt32(leaveId) > 0)
                    {
                        newFileName = leavereq.FileName;
                    }
                }
                objleave.FileName = newFileName;
                result = _levMgService.SaveLeavesByLM(objleave);

                if (result.IsSuccess && Convert.ToInt32(leaveId) > 0 && leavereq.Status == "Approved")
                {
                    string messageBody = _authService.GetEmailTemplateHtmlStringByName("EmailTemplateLeaveApprovedByLineManager");

                    messageBody = messageBody.Replace("{FirstName}", leavereq.FirstName);
                    messageBody = messageBody.Replace("{RequestedNumberOfDays}", objleave.LeaveDuration);
                    messageBody = messageBody.Replace("{LeaveStartDate}", Convert.ToDateTime(leavereq.StartDate).ToString("dd-MM-yyyy"));
                    messageBody = messageBody.Replace("{LeaveStartTime}", leavereq.StartTime);
                    messageBody = messageBody.Replace("{LeaveEndDate}", replaceEndDate);
                    messageBody = messageBody.Replace("{LeaveEndTime}", replaceEndTime);
                    messageBody = messageBody.Replace("ReplacePageURL", "http://" + this.Request.Host.Value + "/intranet/manageleave");
                    messageBody = messageBody.Replace("<div>", "<div style" + "=" + "font-family:" + "Arial" + ";" + ">");

                    // Step 4: Send mail to requested user
                    _smtpMessage.Subject = "Your Leave Request has been approved";
                    _smtpMessage.BodyContent = messageBody;
                        // _smtpMessage.ToAddress =  leavereq.EmailAddress;
                        _smtpMessage.ToAddress = "mclancy@idslogic.co.uk";
                        bool isSent = _smtpMessage.SendAsync();
                    result.IsSuccess = isSent;
                }
                else if (result.IsSuccess && Convert.ToInt32(leaveId) > 0 && leavereq.Status == "Declined")
                {
                    string messageBody = _authService.GetEmailTemplateHtmlStringByName("EmailTemplateLeaveDeclinedByLineManager");

                    messageBody = messageBody.Replace("{FirstName}", leavereq.FirstName);
                    messageBody = messageBody.Replace("{RequestedNumberOfDays}", objleave.LeaveDuration);
                    messageBody = messageBody.Replace("{LeaveStartDate}", Convert.ToDateTime(leavereq.StartDate).ToString("dd-MM-yyyy"));
                    messageBody = messageBody.Replace("{LeaveStartTime}", leavereq.StartTime);
                    messageBody = messageBody.Replace("{LeaveEndDate}", replaceEndDate);
                    messageBody = messageBody.Replace("{LeaveEndTime}", replaceEndTime);
                    messageBody = messageBody.Replace("ReplacePageURL", "http://" + this.Request.Host.Value + "/intranet/manageleave");
                    messageBody = messageBody.Replace("<div>", "<div style" + "=" + "font-family:" + "Arial" + ";" + ">");

                    // Step 4: Send mail to requested user
                    _smtpMessage.Subject = "Your Leave Request has been declined";
                    _smtpMessage.BodyContent = messageBody;
                        //_smtpMessage.ToAddress = leavereq.EmailAddress;
                        _smtpMessage.ToAddress = "mclancy@idslogic.co.uk";                        
                        bool isSent = _smtpMessage.SendAsync();
                    result.IsSuccess = isSent;
                }
                else if (result.IsSuccess && Convert.ToInt32(leaveId) > 0 && leavereq.PreviousLeaveStatus == "Approved"&& leavereq.Status == "Cancelled")
                    {
                        string messageBody = _authService.GetEmailTemplateHtmlStringByName("ApprovedLeaveCancelledByLineManager");

                        messageBody = messageBody.Replace("{FirstName}", leavereq.FirstName);
                        messageBody = messageBody.Replace("{RequestedNumberOfDays}", objleave.LeaveDuration);
                        messageBody = messageBody.Replace("{LeaveStartDate}", Convert.ToDateTime(leavereq.StartDate).ToString("dd-MM-yyyy"));
                        messageBody = messageBody.Replace("{LeaveStartTime}", leavereq.StartTime);
                        messageBody = messageBody.Replace("{LeaveEndDate}", replaceEndDate);
                        messageBody = messageBody.Replace("{LeaveEndTime}", replaceEndTime);
                        messageBody = messageBody.Replace("ReplacePageURL", "http://" + this.Request.Host.Value + "/intranet/manageleave");
                        messageBody = messageBody.Replace("<div>", "<div style" + "=" + "font-family:" + "Arial" + ";" + ">");

                        // Step 4: Send mail to requested user
                        _smtpMessage.Subject = "Your Leave Request has been cancelled";
                        _smtpMessage.BodyContent = messageBody;
                        // _smtpMessage.ToAddress =  leavereq.EmailAddress;
                        _smtpMessage.ToAddress = "mclancy@idslogic.co.uk";
                        bool isSent = _smtpMessage.SendAsync();
                        result.IsSuccess = isSent;
                    }
                    else if (result.IsSuccess && leavereq.Status == "Approved")
                    {
                        string messageBody = _authService.GetEmailTemplateHtmlStringByName("EmailTemplateLeaveApprovedByLineManager");

                        messageBody = messageBody.Replace("{FirstName}", leavereq.FirstName);
                        messageBody = messageBody.Replace("{RequestedNumberOfDays}", objleave.LeaveDuration);
                        messageBody = messageBody.Replace("{LeaveStartDate}", Convert.ToDateTime(leavereq.StartDate).ToString("dd-MM-yyyy"));
                        messageBody = messageBody.Replace("{LeaveStartTime}", leavereq.StartTime);
                        messageBody = messageBody.Replace("{LeaveEndDate}", replaceEndDate);
                        messageBody = messageBody.Replace("{LeaveEndTime}", replaceEndTime);
                        messageBody = messageBody.Replace("ReplacePageURL", "http://" + this.Request.Host.Value + "/intranet/manageleave");
                        messageBody = messageBody.Replace("<div>", "<div style" + "=" + "font-family:" + "Arial" + ";" + ">");

                        // Step 4: Send mail to requested user
                        _smtpMessage.Subject = "Your Leave Request has been approved";
                        _smtpMessage.BodyContent = messageBody;
                        // _smtpMessage.ToAddress =  leavereq.EmailAddress;
                        _smtpMessage.ToAddress = "mclancy@idslogic.co.uk";                        
                        bool isSent = _smtpMessage.SendAsync();
                        result.IsSuccess = isSent;
                    }
                    return Ok(result);
            }
                else
                {

                    result.Message = ModelState.Select(x => x.Value.Errors)
                            .Where(y => y.Count > 0)
                            .FirstOrDefault()[0].ErrorMessage;
                }


                return Ok(result);
            }

            catch (Exception ex)
            {
                _logger.LogError((int)System.Diagnostics.Tracing.EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = ex.Message;
                return Ok(result);
            }
        }

        public JsonResult CountBankHolidays(string startdate, string enddate, string starttime, string endtime)
        {
            decimal count = 0; ;

            DateTime sdatevalue = (Convert.ToDateTime(startdate.ToString()));
            DateTime edatevalue = (Convert.ToDateTime(enddate.ToString()));
            count = _levMgService.CountBankHolidays(sdatevalue, edatevalue, starttime, endtime);
            return Json(count);
        }
       
        public JsonResult CheckBankHolidayByDate(string date)
        {
            BaseResult result = new BaseResult();
            bool retvalue = false; 
            DateTime datevalue = (Convert.ToDateTime(date.ToString()));
            bool isExist = _levMgService.IsExistBankHolidayByDate(datevalue);
            if (isExist)
                retvalue = true;          
            return Json(data: retvalue);
        }
    }
}