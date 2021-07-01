using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PickfordsIntranet.Core.DomainServices;
using PickfordsIntranet.Core.DomainServices.FrontEnd;
using PickfordsIntranet.ViewModels.FrontEnd;
using System.Web;
using PickfordsIntranet.ViewModels;
using System.Diagnostics.Tracing;
using PickfordsIntranet.ViewModels.EndUser;
using PickfordsIntranet.ViewModels.Global;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.DirectoryServices.AccountManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using PickfordsIntranet.WebAdmin.Filters;
using PickfordsIntranet.WebAdmin.Utility;
using PickfordsIntranet.ViewModels.Auth;

namespace PickfordsIntranet.WebAdmin.Controllers.FrontEnd
{
    //[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)] 
    public class FrontEndHomeController : Controller
    {
        private Utility.SmtpMessage _smtpMessage;
        private IAuthService _authService;

        /// <summary>
        /// INewsService data member
        /// </summary>
        private IFronEndService _frontEndService;

        /// <summary>
        /// IPathProvider data member
        /// </summary>
        private IPathProvider _pathProvider;

        /// <summary>
        /// Ilogger Data Member
        /// </summary>
        private ILogger<FrontEndHomeController> _logger;

        private IConfigurationRoot _config;


        /// <summary>
        /// FrontEndHomeController constructor
        /// </summary>
        /// <param name="newsService"></param>
        public FrontEndHomeController(IAuthService authService, Utility.SmtpMessage smtpMessage,IConfigurationRoot config, IFronEndService frontEndService, IPathProvider pathProvider, ILogger<FrontEndHomeController> logger)
        {
            _config = config;
            _pathProvider = pathProvider;
            _frontEndService = frontEndService;
            _logger = logger;
            _authService = authService;
            _smtpMessage = smtpMessage;
        }


        public async Task<ActionResult> Login()
        {
            if (Request.Cookies.Any(c => c.Key == ".AspNetCore.AdminCookies"))
            {

                string userId = HttpContext.User.Claims?.FirstOrDefault(c => c.Type == "UserID")?.Value ?? null;
                string email = HttpContext.User.Claims?.FirstOrDefault(c => c.Type == "Email")?.Value ?? null;
                string fullName = HttpContext.User.Claims?.FirstOrDefault(c => c.Type == "FullName")?.Value ?? null;
                //string role= HttpContext.User.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value ?? null;
                string[] roles = User.Claims?.Where(c => c.Type == ClaimTypes.Role).Select(r => r.Value).ToArray();
                string allRoles = string.Join("+", roles);
                string tempUserCookies = userId + "||" + email + "||" + fullName + "||" + allRoles;

                await HttpContext.SignOutAsync();
                Response.Cookies.Append("tempUserCookies", tempUserCookies);
            }

            TempData["FrontEndUserId"] = null;
            var result = _frontEndService.GetDepartmentListForLogin();

            var CurrentImplementedUserName = User.Identity.Name;
            var WindowUserName = System.Environment.UserName;

            _logger.LogInformation("CurrentImplementedUserName: {0} and WindowUserName: {1}", CurrentImplementedUserName, WindowUserName);

            return View(result);
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
                    var result = _frontEndService.FrontEndUserAuthenticate(user);
                    if (result.IsSuccess == 1)
                    {
                        TempData["FrontEndUserId"] = result.Id;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["UserLoginFailed"] = "Sorry, we can't find a login account with those details. Please enter your user name (which is your email address) and check your password.";
                        return View();
                    }
                }
                else
                    return View();
            }

            catch (Exception ex)
            {
                _logger.LogError((int)System.Diagnostics.Tracing.EventLevel.Error, ex, ex.Message);
                return View("/Error");
            }
        }

        [HttpPost]
        public JsonResult LoginEndUser(string windowsUserId)
        {
            try
            {
                AuthUserVM user = new AuthUserVM();
                user.WindowsUserId = windowsUserId.Trim();
                user.Password = null;

                if (!(string.IsNullOrEmpty(windowsUserId)))
                {

                    var result = _frontEndService.FrontEndUserAuthenticate(user);
                    if (result.IsSuccess == 1)
                    {
                        TempData["FrontEndUserId"] = result.Id;
                        return Json("success");
                    }
                    else
                    {
                        // Get the user info from active directory
                        var adUserInfo = new Dictionary<string, string>();

                        //using (var pc = new PrincipalContext(ContextType.Domain, _config["ADServer"]))
                        //{
                        //    var activeDirectoryuser = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, _config["ADServer"]+"\\" + user.WindowsUserId);
                        //    adUserInfo.Add("TelephoneNo", activeDirectoryuser.VoiceTelephoneNumber);
                        //    adUserInfo.Add("EmployeeId", activeDirectoryuser.EmployeeId);
                        //}

                        // Get the employee Id using windows user id from EndUsersADRecoreds
                        ADUserInfo userADInfo = _frontEndService.GetEmployeeADRecordsByWindowsUserId(user.WindowsUserId);
                        if (user.WindowsUserId != null)
                        {
                            adUserInfo.Add("EmployeeId", userADInfo.EmployeeId);
                        }
                        return Json(adUserInfo);
                    }
                }



                else
                    return Json("emailEmptyOrNull");
            }

            catch (Exception ex)
            {
                _logger.LogError((int)System.Diagnostics.Tracing.EventLevel.Error, ex, ex.Message);
                // return View("/Error");
                return Json("errorPage");
            }
        }

        [Route("/index")]
        [HttpGet]
        public ActionResult Index()
        {

            //string userId=  context.HttpContext.User.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? null;
            if (TempData["FrontEndUserId"] != null)
            {
                TempData.Keep("FrontEndUserId");
                FrontEndUser user = _frontEndService.GetFrontEndUserById(Convert.ToInt32(TempData["FrontEndUserId"]));
                FrontEndVM frontEndVM = _frontEndService.GetFrontEndVMDetails(0, string.Empty);
                frontEndVM.FrontEndUserDetails = user;
                return View(frontEndVM);
            }
            else
            {
                return RedirectToAction("Login");
                // return View("Login");
            }
        }

        [HttpPost]
        public JsonResult GetLocationDetails(string Code)
        {
            GazetersDetail details = _frontEndService.GetSelectedPostalCodeDetail(Code.TrimEnd());
            return Json(new { details = details });
        }

        [HttpPost]
        public ActionResult SaveUserProfile(IFormFile file, string id, string FirstName, string SurName, string JobTitle, string Location, string MyDepartmentName, string EmailAddress, string TelephoneNumber, string Mobile, string Photo, string MyDepartmentId, string WindowsUserId)
        {
            BaseResult result = new BaseResult();


            bool isExist = _frontEndService.IsEmailExist(EmailAddress, id);

            FrontEndUser user = new FrontEndUser();

            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    Byte[] arr = memoryStream.ToArray();
                    string imageName = Guid.NewGuid() + Path.GetFileName(file.FileName).Trim().Replace(" ", String.Empty).Replace("(", String.Empty).Replace(")", String.Empty);
                    string imagePath = _pathProvider.MapPath("") + @"\Uploads\Images\FrontEndUser\" + imageName;
                    System.IO.File.WriteAllBytes(imagePath, arr);
                    user.Photo = imageName;
                }
            }
            else
            {
                user.Photo = Photo;
            }
            user.Id = Convert.ToInt32(id);
            user.UploadImage = file;
            if (!string.IsNullOrEmpty(FirstName)) user.FirstName = FirstName.Trim();
            if (!string.IsNullOrEmpty(SurName)) user.SurName = SurName.Trim();
            if (!string.IsNullOrEmpty(JobTitle)) user.JobTitle = JobTitle.Trim();
            if (!string.IsNullOrEmpty(Location)) user.Location = Location.Trim();
            if (!string.IsNullOrEmpty(MyDepartmentName)) user.MyDepartmentName = MyDepartmentName.Trim();
            if (!string.IsNullOrEmpty(MyDepartmentId)) user.MyDepartmentId = MyDepartmentId.Trim();
            if (!string.IsNullOrEmpty(TelephoneNumber)) user.TelephoneNumber = TelephoneNumber.Trim();
            if (!string.IsNullOrEmpty(Mobile)) user.Mobile = Mobile.Trim();
            user.Photo = user.Photo;

            user.EmailAddress = EmailAddress;
            user.WindowsUserId = WindowsUserId;

            if (!isExist)
            {
                result = _frontEndService.UpdateEndUserProfile(user);
                return Json(new { user = user, result = result });
            }

            else
            {
                result = new BaseResult() { IsSuccess = false, Message = "Email address already exist." };
                return Json(new { user = user, result = result, isEmailValid = false });
            }


        }


        [HttpPost]
        public ActionResult CreateEnUserProfile(IFormFile file, string id, string FirstName, string SurName, string JobTitle, string Location, string MyDepartmentName, string TelephoneNumber, string Mobile, string Photo, string MyDepartmentId, string EmailAddress, string WindowsUserId, string EmployeeId)
        {
            FrontEndUser user = new FrontEndUser();

            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    Byte[] arr = memoryStream.ToArray();
                    string imageName = Guid.NewGuid() + Path.GetFileName(file.FileName).Trim().Replace(" ", String.Empty).Replace("(", String.Empty).Replace(")", String.Empty);
                    string imagePath = _pathProvider.MapPath("") + @"\Uploads\Images\FrontEndUser\" + imageName;
                    System.IO.File.WriteAllBytes(imagePath, arr);
                    user.Photo = imageName;
                }
            }
            else
            {
                user.Photo = Photo;
            }
            user.Id = Convert.ToInt32(id);
            user.UploadImage = file;
            if (!string.IsNullOrEmpty(FirstName)) user.FirstName = FirstName.Trim();
            if (!string.IsNullOrEmpty(SurName)) user.SurName = SurName.Trim();
            if (!string.IsNullOrEmpty(JobTitle)) user.JobTitle = JobTitle.Trim();
            if (!string.IsNullOrEmpty(Location)) user.Location = Location.Trim();
            if (!string.IsNullOrEmpty(MyDepartmentName)) user.MyDepartmentName = MyDepartmentName.Trim();
            if (!string.IsNullOrEmpty(MyDepartmentId)) user.MyDepartmentId = MyDepartmentId.Trim();
            if (!string.IsNullOrEmpty(TelephoneNumber)) user.TelephoneNumber = TelephoneNumber.Trim();
            if (!string.IsNullOrEmpty(Mobile)) user.Mobile = Mobile.Trim();
            if (!string.IsNullOrEmpty(EmailAddress)) user.EmailAddress = EmailAddress.Trim();
            if (!string.IsNullOrEmpty(WindowsUserId)) user.WindowsUserId = WindowsUserId.Trim();
            if (!string.IsNullOrEmpty(EmployeeId)) user.EmployeeId = EmployeeId.Trim();
            user.Photo = user.Photo;


            if (string.IsNullOrEmpty(EmailAddress))
            {

                TempData["FrontEndUserId"] = 0;
                return Json(new { isSuccess = false, isEmailValid = false });
            }
            else
            {
                bool isEmailAlreadyExist = _frontEndService.IsEmailExist(EmailAddress, id);
                if (isEmailAlreadyExist == true)
                {
                    return Json(new { isSuccess = false, isEmailValid = false });
                }
                else
                {
                    var endUserId = _frontEndService.CreateEndUserProfile(user);
                    TempData["FrontEndUserId"] = endUserId;
                    return Json(new { isSuccess = true, isEmailValid = true });
                }
            }

        }

        [Route("/intranet/news")]
        public ActionResult IntranetNews()
        {
            if (TempData["FrontEndUserId"] != null)
            {
                TempData.Keep("FrontEndUserId");
                FrontEndUser user = _frontEndService.GetFrontEndUserById(Convert.ToInt32(TempData["FrontEndUserId"]));
                FrontEndVM frontEndVM = _frontEndService.GetAllDepartmentsFroNews();

                frontEndVM.FrontEndUserDetails = user;
                return View(frontEndVM);
                //return View(_frontEndService.GetFrontEndVMDetails(0, string.Empty));
            }
            else
            {
                return View("Login");
            }
        }

        [HttpGet]
        public JsonResult GetNewsDetailsByDepartmentWise(int id)
        {
            var result = _frontEndService.GetNewsOnDepartmentId(id);
            return Json(new { result = result });

        }


        [HttpGet]
        public ActionResult GetVacancyDetailsByDepartmentWise(int id)
        {
            var result = _frontEndService.GetVacancyOnDepartmentId(id);
            return PartialView("_IntranetVacancyDetail", result);
        }

        [Route("/intranet/our_values")]
        public ActionResult IntranetOurValues()
        {
            if (TempData["FrontEndUserId"] != null)
            {
                TempData.Keep("FrontEndUserId");
                FrontEndUser user = _frontEndService.GetFrontEndUserById(Convert.ToInt32(TempData["FrontEndUserId"]));
                FrontEndVM frontEndVM = _frontEndService.GetFrontEndVMDetails(0, string.Empty);
                frontEndVM.FrontEndUserDetails = user;
                return View(frontEndVM);
                //return View(_frontEndService.GetFrontEndVMDetails(0, string.Empty));
            }
            else
            {
                return View("Login");
            }
            //return View(_frontEndService.GetFrontEndVMDetails(0, string.Empty));
        }

        //[Route("Movies/Calendar")]
        [Route("/intranet/department/{id}/{title}")]
        public ActionResult IntranetDepartment(int id, string title)
        {
            if (TempData["FrontEndUserId"] != null)
            {
                TempData.Keep("FrontEndUserId");
                FrontEndUser user = _frontEndService.GetFrontEndUserById(Convert.ToInt32(TempData["FrontEndUserId"]));
                FrontEndVM frontEndVM = _frontEndService.GetFrontEndVMDetails(id, "NewsByDepartmentWise");
                frontEndVM.SelectedDepartmentValues = _frontEndService.GetDepartmentValuesById(id);
                frontEndVM.FeaturedDocumentList = _frontEndService.GetFeaturedDocumentList(id);
                frontEndVM.FrontEndUserDetails = user;
                return View(frontEndVM);
                //return View(_frontEndService.GetFrontEndVMDetails(0, string.Empty));
            }
            else
            {
                return View("Login");
            }
            //return View(_frontEndService.GetFrontEndVMDetails(0, string.Empty));
        }

        public JsonResult SaveMydepartment(int deptId, int userId)
        {
            BaseResult result = _frontEndService.SaveMyDepartment(deptId, userId);
            return Json(new { result = result });
        }

        public JsonResult AddMyFavouriteDocument(int docId, int userId)
        {
            BaseResult result = _frontEndService.AddMyFavouriteDocument(docId, userId);
            return Json(new { result = result });
        }

        public JsonResult GetFavDocumentList(int userId)
        {
            List<DocumentGridFrontEnd> Docs = new List<DocumentGridFrontEnd>();

            Docs = _frontEndService.GetFavouriteDocument(userId);

            return Json(Docs);
        }

        public JsonResult RemoveFromFavouriteList(int docId, int UserId)
        {

            var result = _frontEndService.RemoveFromFavouriteList(docId, UserId);
            return Json(result);
        }

        [Route("/intranet/vacancies")]
        public ActionResult IntranetVacancies()
        {
            if (TempData["FrontEndUserId"] != null)
            {
                TempData.Keep("FrontEndUserId");
                FrontEndUser user = _frontEndService.GetFrontEndUserById(Convert.ToInt32(TempData["FrontEndUserId"]));
                FrontEndVM frontEndVM = _frontEndService.GetAllDepartmentsFroVacancies();

                frontEndVM.FrontEndUserDetails = user;
                return View(frontEndVM);

                //FrontEndUser user = _frontEndService.GetFrontEndUserById(Convert.ToInt32(TempData["FrontEndUserId"]));
                //FrontEndVM frontEndVM = _frontEndService.GetFrontEndVMDetails(0, string.Empty);
                //frontEndVM.FrontEndUserDetails = user;
                //return View(frontEndVM);
                //return View(_frontEndService.GetFrontEndVMDetails(0, string.Empty));
            }
            else
            {
                return View("Login");
            }
            //return View(_frontEndService.GetFrontEndVMDetails(0, string.Empty));
        }

        [Route("/intranet/vacancies/{id}/{title}")]
        public ActionResult IntranetVacanciesDetail(int id, string title)
        {
            if (TempData["FrontEndUserId"] != null)
            {
                TempData.Keep("FrontEndUserId");
                FrontEndUser user = _frontEndService.GetFrontEndUserById(Convert.ToInt32(TempData["FrontEndUserId"]));
                FrontEndVM frontEndVM = _frontEndService.GetFrontEndVMDetails(id, "VacancyId");
                frontEndVM.FrontEndUserDetails = user;
                return View(frontEndVM);
                //return View(_frontEndService.GetFrontEndVMDetails(0, string.Empty));
            }
            else
            {
                return View("Login");
            }
            //return View(_frontEndService.GetFrontEndVMDetails(id, "VacancyId"));
        }

        [Route("/intranet/news/{id}/{title}")]
        public ActionResult IntranetNewsDetail(int id)
        {

            if (TempData["FrontEndUserId"] != null)
            {
                TempData.Keep("FrontEndUserId");
                FrontEndUser user = _frontEndService.GetFrontEndUserById(Convert.ToInt32(TempData["FrontEndUserId"]));
                FrontEndVM frontEndVM = _frontEndService.GetFrontEndVMDetails(id, "NewsId");
                frontEndVM.FrontEndUserDetails = user;
                return View(frontEndVM);
                //return View(_frontEndService.GetFrontEndVMDetails(0, string.Empty));
            }
            else
            {
                return View("Login");
            }
            //return View(_frontEndService.GetFrontEndVMDetails(id, "NewsId"));
        }

        public ActionResult GetIntranetDepartmentDetailsByDepartmentWise(int id)
        {
            if (TempData["FrontEndUserId"] != null)
            {
                TempData.Keep("FrontEndUserId");
                int LoggedInUserId = Convert.ToInt32(TempData["FrontEndUserId"]);

                //FrontEndUser user = _frontEndService.GetFrontEndUserById(Convert.ToInt32(TempData["FrontEndUserId"]));
                return PartialView("_IntranetDepartmentDetail", _frontEndService.GetFrontEndVMDetails(id, "DepartmentId~" + LoggedInUserId));
            }
            else
            {
                return View("Login");
            }
        }
        
        public async Task<IActionResult> GetDocumentToDownload(string documentName)
        {
            if (documentName == null)
                return RedirectToAction("Index", "FrontEndHome");

            string filePath = _pathProvider.MapPath("") + @"\Uploads\Documents\" + documentName;

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filePath), Path.GetFileName(filePath));
        }

        [NonAction]
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        [NonAction]
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
        public ActionResult GetIntranetNewsDetailByDepartmentWise(int id)
        {
            if (TempData["FrontEndUserId"] != null)
            {
                TempData.Keep("FrontEndUserId");
                int LoggedInUserId = Convert.ToInt32(TempData["FrontEndUserId"]);
                var result = _frontEndService.GetFrontEndVMDetails(id, "NewsByDepartmentWise");
                //FrontEndUser user = _frontEndService.GetFrontEndUserById(Convert.ToInt32(TempData["FrontEndUserId"]));
                return PartialView("_IntranetNewsByDepartment", result);
            }
            else
            {
                return View("Login");
            }
        }


        #region Documents

        public JsonResult GetDocumentList(String searchKeyword)
        {
            FrontEndVM obj = new FrontEndVM();
            obj.SearchDocumentList = _frontEndService.GetAllDocuments(searchKeyword);

            return Json(obj.SearchDocumentList);
        }

        public JsonResult GetDocumentsTitle()
        {
            List<DocumentGridFrontEnd> obj = new List<DocumentGridFrontEnd>();
            obj = _frontEndService.GetDocumentsTitle();

            return Json(obj);


        }

        #endregion

        #region Faqs

        public JsonResult GetFaqsList(string searchKeyword)
        {
            SearchFAQsList obj = new SearchFAQsList();
            obj = _frontEndService.GetAllSearchFaqs(searchKeyword);

            return Json(obj);
        }

        public JsonResult GetFaqsQuestions()
        {
            List<FAQGridVM> obj = new List<FAQGridVM>();
            obj = _frontEndService.GetFaqsQuestions();

            return Json(obj);
        }


        #endregion

        #region search By Person

        public JsonResult GetPersonsList(string personName, string JobTitle)
        {
            List<FrontEndUser> user = new List<FrontEndUser>();

            if (TempData["FrontEndUserId"] != null)
            {
                TempData.Keep("FrontEndUserId");
                var loggedUserId = Convert.ToInt32(TempData["FrontEndUserId"]);
                user = _frontEndService.GetPersonsList(personName, JobTitle, loggedUserId);
            }



            return Json(user);
        }

        #endregion

        #region Get mail content
        /// <summary>
        /// Get mail content view of reward recipeint using Id
        /// </summary>
        /// <param name="Id">Mail Generated Id</param>
        /// <returns></returns>
        [Route("/intranet/recipientaward/{id}")]
        [AllowAnonymous]
        public IActionResult GetRewardRecipientMailContentToViewInBrowser(string id)
        {
            try
            {
                string rewardRecipientmailContent = _frontEndService.GetRewardRecipientMailContentToViewInBrowser(id: new Guid(id));
                MailContentVM mailContent = new MailContentVM();
                mailContent.MailContentString = rewardRecipientmailContent;
                return View(mailContent);
            }
            catch (Exception ex)
            {
                _logger.LogError((int)ViewModels.Global.EventLevel.Error, ex, ex.Message);
                return View("Error");
            }

        }
   #endregion

        #region Manage Leave
        [Route("/intranet/manageleave")]        
        public ActionResult IntranetManageLeave(UserActionLoggingDetails loggingDetails)
        {
            if (TempData["FrontEndUserId"] != null)
            {
                TempData.Keep("FrontEndUserId");
                FrontEndUser user = _frontEndService.GetFrontEndUserById(Convert.ToInt32(TempData["FrontEndUserId"]));
                FrontEndVM frontEndVM = _frontEndService.GetAllDepartmentsFroNews();
                frontEndVM.FrontEndUserDetails = user;
                frontEndVM.leaveCount = _frontEndService.GetHolidayEntitlement(Convert.ToInt32(TempData["FrontEndUserId"]));
                return View(frontEndVM);

                //return View(_frontEndService.GetFrontEndVMDetails(0, string.Empty));
            }
            else
            {
                return View("Login");
            }
           
        }
    
        [Route("/intranet/whathappensnext")]
        public ActionResult Whathappensnext(UserActionLoggingDetails loggingDetails)
        {
            if (TempData["FrontEndUserId"] != null)
            {
                TempData.Keep("FrontEndUserId");
                FrontEndUser user = _frontEndService.GetFrontEndUserById(Convert.ToInt32(TempData["FrontEndUserId"]));
                FrontEndVM frontEndVM = _frontEndService.GetAllDepartmentsFroNews();
                frontEndVM.FrontEndUserDetails = user;
                return View(frontEndVM);

                //return View(_frontEndService.GetFrontEndVMDetails(0, string.Empty));
            }
            else
            {
                return View("Login");
            }

        }

        #endregion
        #region Leave Request
        [Route("/intranet/leaverequest")]
        public ActionResult IntranetLeaveRequest(int id)
        {
            if (TempData["FrontEndUserId"] != null)
            {
                TempData.Keep("FrontEndUserId");
                FrontEndUser user = _frontEndService.GetFrontEndUserById(Convert.ToInt32(TempData["FrontEndUserId"]));
                FrontEndVM frontEndVM = _frontEndService.GetAllDepartmentsFroNews();
                frontEndVM.FrontEndUserDetails = user;
                frontEndVM.LeaveTypes = _frontEndService.GetAllLeavetypes();
                frontEndVM.LeaveReq = _frontEndService.GetALeaveDetailByID(id);
                frontEndVM.BankHolidays = _frontEndService.GetAllBankHolidays();
                
                // frontEndVM.BookedLeaves = _frontEndService.GetAllBookedLeaves(user.EmployeeId,frontEndVM.LeaveReq.StartDate, frontEndVM.LeaveReq.EndDate);
                return View(frontEndVM);
                //return View(_frontEndService.GetFrontEndVMDetails(0, string.Empty));
            }
            else
            {
                return View("Login");
            }

        }
       
        public JsonResult GetAllBookedLeaves(string startdate, string enddate)
        {
            List<LeaveRequest> leave = new List<LeaveRequest>();
            if (TempData["FrontEndUserId"] != null)
            {
                TempData.Keep("FrontEndUserId");
                DateTime sdatevalue = (Convert.ToDateTime(startdate.ToString()));
                DateTime edatevalue = (Convert.ToDateTime(enddate.ToString()));
               //string sDate = datevalue.ToString("yyyy-MM-dd");
               //string eDate = edatevalue.ToString("yyyy-MM-dd");
                FrontEndUser user = _frontEndService.GetFrontEndUserById(Convert.ToInt32(TempData["FrontEndUserId"]));
                leave = _frontEndService.GetAllBookedLeaves(user.EmployeeId, sdatevalue, edatevalue);
            }

            return Json(leave);
        }
        public JsonResult CountBankHolidays(string startdate, string enddate,string starttime,string endtime)
        {
            decimal count = 0; ;

            DateTime sdatevalue = (Convert.ToDateTime(startdate.ToString()));
            DateTime edatevalue = (Convert.ToDateTime(enddate.ToString()));

            count = _frontEndService.CountBankHolidays(sdatevalue, edatevalue, starttime, endtime);

            return Json(count);
        }

        #endregion
        /// <summary>
        /// To Check leave balance exist or not
        /// </summary>
        [AllowAnonymous]
        public IActionResult ValidateLeaveBalanceExistOrNot(decimal Quantity)
        {
            BaseResult result = new BaseResult();
            bool retvalue = false;
            if (TempData["FrontEndUserId"] != null)
            {
                TempData.Keep("FrontEndUserId");
                FrontEndUser user = _frontEndService.GetFrontEndUserById(Convert.ToInt32(TempData["FrontEndUserId"]));
                int userid = Convert.ToInt32(user.Id);
                bool isExist = _frontEndService.IsLeaveBalanceExist(Quantity, userid);
                if (isExist)
                    retvalue = true;
            }
            return Json(data: retvalue);
        }
        [AllowAnonymous]
        public IActionResult CheckBankHolidayByDate(string date)
        {
            BaseResult result = new BaseResult();
            bool retvalue = false;
            if (TempData["FrontEndUserId"] != null)
            {
                TempData.Keep("FrontEndUserId");
                DateTime datevalue = (Convert.ToDateTime(date.ToString()));
                bool isExist = _frontEndService.IsExistBankHolidayByDate(datevalue);
                if (isExist)
                    retvalue = true;
            }
            return Json(data: retvalue);
        }
        [NonAction]
        private void SendMail(string subject,string messageBody,string toemail)
        {
            // Step 4: Send mail to requested user
            _smtpMessage.Subject = subject;
            _smtpMessage.BodyContent = messageBody;
            _smtpMessage.ToAddress = toemail;
            bool isSent = _smtpMessage.SendAsync();
            //result.IsSuccess = isSent;
        }

        /// <summary>
        /// Post Add Leave Req to save it
        /// </summary>
        /// <param name="leavereq">AddLeaveRequest data structure model</param>
        /// <returns>Success or Failure</returns>
        [HttpPost]
       // [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult AddLeaveRequest([Bind] LeaveRequest leavereq, UserActionLoggingDetails loggingDetails, string SelectedLeaveTypeId,decimal quantity,int leaveid,string filename,string newenddate)
        {

            BaseResult result = new BaseResult();

            try
            {
                var newFileName = "";

                if (leavereq.FileNameData != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        leavereq.FileNameData.CopyTo(memoryStream);
                        leavereq.Filename = Guid.NewGuid() + Path.GetFileName(leavereq.FileNameData.FileName).Trim().Replace(" ", String.Empty);
                        string fileNamePath = _pathProvider.MapPath("") + @"\Uploads\Admin\Leave\" + leavereq.Filename;
                        System.IO.File.WriteAllBytes(fileNamePath, memoryStream.ToArray());
                        newFileName = leavereq.Filename;
                    }
                }
                else
                {
                    if (leaveid > 0)
                    {
                        newFileName = filename;
                    }
                }
               
                if (ModelState.IsValid)
                {
                    String sDate = leavereq.LeaveStartDate;
                    String eDate = newenddate;
                    String rbackDate = leavereq.ReturnBackDate;
                    DateTime datevalue = Convert.ToDateTime(sDate.ToString());
                    DateTime edatevalue = Convert.ToDateTime(eDate.ToString());
                    DateTime rbackdatevalue = Convert.ToDateTime(rbackDate.ToString());

                    //DateTime yesterday = edatevalue.AddDays(-1);
                    DateTime todateforemail = edatevalue;
                    sDate = datevalue.ToString("yyyy-MM-dd");// mm + "/" + dd + "/" + yy;
                    eDate = edatevalue.ToString("yyyy-MM-dd");//emm + "/" + edd + "/" + eyy;
                    rbackDate = rbackdatevalue.ToString("yyyy-MM-dd");
                    string etime = "";

                    if (leavereq.EndTime.ToLower() == "morning")
                    {
                        etime = "AFTERNOON";
                    }
                    if (leavereq.EndTime.ToLower() == "afternoon")
                        etime = "MORNING";

                   

                    TempData.Keep("FrontEndUserId");
                    FrontEndUser user = _frontEndService.GetFrontEndUserById(Convert.ToInt32(TempData["FrontEndUserId"]));
                    //leavereq.EndUserId = Convert.ToInt32(loggingDetails.UserId);
                    leavereq.EndUserId = Convert.ToInt32(user.Id);
                    leavereq.Filename= newFileName;
                    leavereq.SelectedLeaveTypeId = Convert.ToInt32(SelectedLeaveTypeId);
                    leavereq.LeaveStartDate = sDate;
                    leavereq.LeaveEndDate = eDate;
                    leavereq.ReturnBackDate = rbackDate;
                    leavereq.Quantity = quantity;
                    leavereq.Department = user.MyDepartmentName;
                    leavereq.LeaveId = leaveid;
                    result = _frontEndService.SaveLeaveRequest(leavereq);
                    if(result.IsSuccess && leaveid == 0)
                    {
                        string messageBody = _authService.GetEmailTemplateHtmlStringByName("EmailTemplateNewLeaveRequest");

                        messageBody = messageBody.Replace("ReplaceFirstName",user.FirstName);
                        messageBody = messageBody.Replace("ReplaceNoofDays", quantity.ToString());
                        messageBody = messageBody.Replace("ReplaceStartDate", datevalue.ToString("dd-MM-yyyy"));
                        messageBody = messageBody.Replace("ReplaceEndDate", todateforemail.ToString("dd-MM-yyyy"));
                        messageBody = messageBody.Replace("ReplaceStartTime",leavereq.StartTime);
                        messageBody = messageBody.Replace("ReplaceEndTime", etime);

                        messageBody = messageBody.Replace("ReplacePageURL", "http://" + this.Request.Host.Value + "/intranet/manageleave");
                        messageBody = messageBody.Replace("<div>", "<div style" + "=" + "font-family:" + "Arial" + ";" + ">");
                        string subject = "Thank you for requesting your leave";
                        //string toemail = user.EmailAddress; //"biswajit.mishra@idslogic.com";
                        string toemail = "mclancy@idslogic.co.uk";
                        SendMail(subject, messageBody, toemail);

                        //send mail to Line Manager
                        string messageBodyLM = _authService.GetEmailTemplateHtmlStringByName("EmailTemplateNewLeaveRequestLM");

                        messageBodyLM = messageBodyLM.Replace("ReplaceFirstName", user.FirstName);
                        messageBodyLM = messageBodyLM.Replace("ReplaceNoofDays", quantity.ToString());
                        messageBodyLM = messageBodyLM.Replace("ReplaceStartDate", datevalue.ToString("dd-MM-yyyy"));
                        messageBodyLM = messageBodyLM.Replace("ReplaceEndDate", todateforemail.ToString("dd-MM-yyyy"));
                        messageBodyLM = messageBodyLM.Replace("ReplaceStartTime", leavereq.StartTime);
                        messageBodyLM = messageBodyLM.Replace("ReplaceEndTime", etime);
                        messageBodyLM = messageBodyLM.Replace("ReplaceSurName", user.SurName);
                        messageBodyLM = messageBodyLM.Replace("ReplaceLMFirstName", user.LineManagerFirstName);
                        messageBodyLM = messageBodyLM.Replace("<div>", "<div style" + "=" + "font-family:" + "Arial" + ";" + ">");
                        subject = user.FirstName + " " + user.SurName + " has requested leave";
                        toemail = "mclancy@idslogic.co.uk";
                       // toemail = user.LineManagerEmail;
                        SendMail(subject, messageBodyLM, toemail);

                    }
                    if (result.IsSuccess && leaveid > 0)
                    {
                        string messageBody = _authService.GetEmailTemplateHtmlStringByName("EmailTemplateUpdateLeaveRequest");

                        messageBody = messageBody.Replace("ReplaceFirstName", user.FirstName);
                        messageBody = messageBody.Replace("ReplaceNoofDays", quantity.ToString());
                        messageBody = messageBody.Replace("ReplaceStartDate", datevalue.ToString("dd-MM-yyyy"));
                        messageBody = messageBody.Replace("ReplaceEndDate", todateforemail.ToString("dd-MM-yyyy"));
                        messageBody = messageBody.Replace("ReplaceStartTime", leavereq.StartTime);
                        messageBody = messageBody.Replace("ReplaceEndTime", etime);

                        messageBody = messageBody.Replace("ReplacePageURL", "http://" + this.Request.Host.Value + "/intranet/manageleave");
                        messageBody = messageBody.Replace("<div>", "<div style" + "=" + "font-family:" + "Arial" + ";" + ">");
                        string subject = "Thank you for submitting changes to your existing leave arrangements";
                        //string toemail = user.EmailAddress;// "biswajit.mishra@idslogic.com";
                        string toemail = "mclancy@idslogic.co.uk";
                        SendMail(subject, messageBody, toemail);

                        //send mail to Line Manager
                        string messageBodyLM = _authService.GetEmailTemplateHtmlStringByName("EmailTemplateUpdateLeaveRequestLM");

                        messageBodyLM = messageBodyLM.Replace("ReplaceFirstName", user.FirstName);
                        messageBodyLM = messageBodyLM.Replace("ReplaceNoofDays", quantity.ToString());
                        messageBodyLM = messageBodyLM.Replace("ReplaceStartDate", datevalue.ToString("dd-MM-yyyy"));
                        messageBodyLM = messageBodyLM.Replace("ReplaceEndDate", todateforemail.ToString("dd-MM-yyyy"));
                        messageBodyLM = messageBodyLM.Replace("ReplaceStartTime", leavereq.StartTime);
                        messageBodyLM = messageBodyLM.Replace("ReplaceEndTime", etime);
                        
                        messageBodyLM = messageBodyLM.Replace("ReplaceSurName", user.SurName);
                        messageBodyLM = messageBodyLM.Replace("ReplaceLMFirstName", user.LineManagerFirstName);
                        messageBodyLM = messageBodyLM.Replace("<div>", "<div style" + "=" + "font-family:" + "Arial" + ";" + ">");
                        subject = user.FirstName + " " + user.SurName + " has requested a change to one of their existing (pending) leave requests";
                        //toemail = "biswajit.mishra@idslogic.com";
                        toemail = "mclancy@idslogic.co.uk";
                        //toemail = user.LineManagerEmail;
                        SendMail(subject, messageBodyLM, toemail);
                    }
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
       
        public IActionResult GetPendingLeaveRequests()
        {
            List<LeaveManagement> pendingLeaveRequests = new List<LeaveManagement>();
            if (TempData["FrontEndUserId"] != null)
            {
                TempData.Keep("FrontEndUserId");
                pendingLeaveRequests = _frontEndService.GetPendingLeaveRequests(Convert.ToInt32(TempData["FrontEndUserId"]));                
            }
            return PartialView("_PendingLeaveRequest", pendingLeaveRequests);
        }

        public IActionResult GetApprovedLeaveRequests()
        {
            List<LeaveManagement> approvedLeaveRequest = new List<LeaveManagement>();
            if (TempData["FrontEndUserId"] != null)
            {
                TempData.Keep("FrontEndUserId");
                approvedLeaveRequest = _frontEndService.GetApprovedLeaveRequests(Convert.ToInt32(TempData["FrontEndUserId"]));
            }               
            return PartialView("_ApprovedLeaveRequest", approvedLeaveRequest);

        }


        [Route("/intranet/cancel-confirmation")]
        [AutoPopulateLoggingDetails]
        [AllowAnonymous]
        public IActionResult CancelePendingLeaveRequest(int LeaveId, UserActionLoggingDetails loggingDetails)
        {
            BaseResult result = new BaseResult();
            try
            {
                if (TempData["FrontEndUserId"] != null)
                {
                    TempData.Keep("FrontEndUserId");
                    FrontEndUser user = _frontEndService.GetFrontEndUserById(Convert.ToInt32(TempData["FrontEndUserId"]));
                    FrontEndVM frontEndVM = _frontEndService.GetAllDepartmentsFroNews();
                    frontEndVM.LeaveReq = _frontEndService.GetALeaveDetailByID(LeaveId);
                    result = _frontEndService.CancelePendingLeaveRequest(LeaveId);
                    if (result.IsSuccess)
                    {
                       
                        string messageBody = _authService.GetEmailTemplateHtmlStringByName("EmailTemplateCancelLeaveRequest");

                        messageBody = messageBody.Replace("ReplaceFirstName", user.FirstName);
                        messageBody = messageBody.Replace("ReplaceNoofDays", frontEndVM.LeaveReq.Quantity.ToString());
                        messageBody = messageBody.Replace("ReplaceStartDate", frontEndVM.LeaveReq.StartDate.ToString("dd-MM-yyyy"));
                        messageBody = messageBody.Replace("ReplaceEndDate", frontEndVM.LeaveReq.ToDate.ToString("dd-MM-yyyy"));
                        messageBody = messageBody.Replace("ReplaceStartTime", frontEndVM.LeaveReq.StartTime);
                        messageBody = messageBody.Replace("ReplaceEndTime", frontEndVM.LeaveReq.ToTime);

                        messageBody = messageBody.Replace("ReplacePageURL", "http://" + this.Request.Host.Value + "/intranet/manageleave");
                        messageBody = messageBody.Replace("<div>", "<div style" + "=" + "font-family:" + "Arial" + ";" + ">");

                        string subject = "Your Leave Request has been cancelled";
                        //string toemail = user.EmailAddress; //"biswajit.mishra@idslogic.com";
                        string toemail = "mclancy@idslogic.co.uk";
                        SendMail(subject, messageBody, toemail);
                    }
                   
                 
                    frontEndVM.FrontEndUserDetails = user;
                    return View(frontEndVM);
                }
                else
                {
                    return View("Login");
                }
            }
            catch (Exception ex)
            {

                _logger.LogError((int)System.Diagnostics.Tracing.EventLevel.Error, ex, ex.Message);
                result = new BaseResult();
                result.Message = StaticResource.InternalServerMessage;
                return Ok(result);
            }
        }

    }
}