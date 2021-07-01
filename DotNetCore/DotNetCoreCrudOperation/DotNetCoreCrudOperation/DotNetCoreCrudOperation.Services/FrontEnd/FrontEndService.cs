using PickfordsIntranet.Core.DomainServices.FrontEnd;
using PickfordsIntranet.Core.Repositories;
using PickfordsIntranet.ViewModels;
using PickfordsIntranet.ViewModels.Auth;
using PickfordsIntranet.ViewModels.EndUser;
using PickfordsIntranet.ViewModels.FrontEnd;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.SuperAdmin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PickfordsIntranet.Services.FrontEnd
{
    /// <summary>
    /// FrontEndService implemented IFronEndService
    /// </summary>
    public class FrontEndService : IFronEndService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// FrontEndService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public FrontEndService(IUnitOfWork unitOfWork)
        {
            try
            {
                _unitOfWork = unitOfWork;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FrontEndUser FrontEndUserAuthenticate(AuthUserVM user)
        {
            try
            {
                var result = _unitOfWork.FrontEndRepo.FrontEndUserAuthenticate(user);
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FrontEndVM GetFrontEndVMDetails(int id, string type)
        {
            try
            {
                var result = _unitOfWork.FrontEndRepo.GetFrontEndVMDetails(id, type);

                // Put the Star Pics placeholder if Star count is less than 12
                if (result.MonthStarList.Count < 12)
                {
                    // Make MonthStarList List object if null
                    if (result.MonthStarList.Count == 0)
                    {
                        result.MonthStarList = new List<MonthStarDetails>();
                    }

                    int starPlaceholderCount = 12 - result.MonthStarList.Count;

                    for (int i = 1; i <= starPlaceholderCount; i++)
                    {
                        MonthStarDetails objStar = new MonthStarDetails() { StarName = String.Empty, StarPhoto = "starplaceholder.jpg" };
                        result.MonthStarList.Add(objStar);
                    }
                }

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Registration GetDepartmentListForLogin()
        {
            try
            {
                var result = _unitOfWork.FrontEndRepo.GetDepartmentListForLogin();
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentGridFrontEnd> GetFeaturedDocumentList(int departmentId)
        {
            try
            {
                var result = _unitOfWork.FrontEndRepo.GetFeaturedDocumentList("usp_GetFeaturedDocumentList", new { departmentId = departmentId });
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FrontEndVM GetAllDepartmentsFroNews()
        {
            try
            {
                var result = _unitOfWork.FrontEndRepo.GetAllDepartmentForNews();
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FrontEndVM GetAllDepartmentsFroVacancies()
        {
            try
            {
                var result = _unitOfWork.FrontEndRepo.GetAllDepartmentsForVacancies();
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FrontEndVM GetNewsOnDepartmentId(int departmentId)
        {
            try
            {
                var result = _unitOfWork.FrontEndRepo.GetNewsListBydepartmentId(departmentId);
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DepartmentVM GetDepartmentValuesById(int departmentId)
        {
            try
            {
                var result = _unitOfWork.FrontEndRepo.GetDepartmentValuesById("usp_GetDepartmentValuesById", new { departmentId = departmentId });
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FrontEndVM GetVacancyOnDepartmentId(int departmentId)
        {
            try
            {
                var result = _unitOfWork.FrontEndRepo.GetVacancyListBydepartmentId(departmentId);
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult UpdateEndUserProfile(FrontEndUser frontEndUser)
        {
            try
            {
                var result = _unitOfWork.FrontEndRepo.UpdateEndUserProfile(frontEndUser);
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CreateEndUserProfile(FrontEndUser frontEndUser)
        {
            try
            {
                var result = _unitOfWork.FrontEndRepo.CreateEndUserProfile_New(frontEndUser);
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult SaveMyDepartment(int deptId, int userId)
        {
            try
            {
                var result = _unitOfWork.FrontEndRepo.SaveMyDepartment(deptId, userId);
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public BaseResult AddMyFavouriteDocument(int docId, int userId)
        {
            try
            {
                var result = _unitOfWork.FrontEndRepo.AddMyFavouriteDocument(docId, userId);
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentGridFrontEnd> GetFavouriteDocument(int UserId)
        {
            try
            {
                List<DocumentGridFrontEnd> obj = new List<DocumentGridFrontEnd>();

                obj = _unitOfWork.FrontEndRepo.GetFavouriteDocument("usp_GetFavouriteDocument", new { UserId = UserId });

                return obj;
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool RemoveFromFavouriteList(int DocId, int UserId)
        {
            try
            {


                var result = _unitOfWork.FrontEndRepo.RemoveFromFavouriteList("usp_RemoveFromFavouriteList", new { DocId = DocId, UserId = UserId });

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }


        public FrontEndUser GetFrontEndUserById(Int32 Id)
        {
            try
            {
                var result = _unitOfWork.FrontEndRepo.GetFrontEndUserById("usp_GetFrontEndUserById", new { Id = Id });
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public FrontEndVM GetAllLocationList()

        //public FrontEndVM GetAllLocationList(int id)
        //{
        //    try
        //    {
        //        //var result = _unitOfWork.FrontEndRepo.GetFrontEndVMDetails(id, type);
        //        //var result = _unitOfWork.FrontEndRepo.GetAllLocationList("usp_GetAllLocations");

        //        //var result = _unitOfWork.FrontEndRepo.GetAllLocationList("usp_GetAllLocations");

        //        result.DocumentsByDepartment = _unitOfWork.FrontEndRepo.GetDocumentListByDepartmentId("usp_GetAllDocumentAndFolder", new { DepartmentId = id });

        //        return result;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public FrontEndVM GetAllLocationList()
        //{
        //    try
        //    {
        //        var result = _unitOfWork.FrontEndRepo.GetAllLocationList("usp_GetAllLocations");
        //        return result;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public GazetersDetail GetSelectedPostalCodeDetail(string PostalCode)
        {
            try
            {
                var result = _unitOfWork.FrontEndRepo.GetSelectedPostalCodeDetail("usp_GetSelectedPostalCodeDetail", new { PostalCode = PostalCode });
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public FrontEndVM GetAllNews_LocationList()
        //{
        //    try
        //    {
        //        var result = _unitOfWork.FrontEndRepo.GetAllNews_LocationList("usp_GetAllNews_LocationList");
        //        return result;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public FrontEndVM GetNewsById_LocationList(int newsId)
        //{
        //    try
        //    {
        //        var result = _unitOfWork.FrontEndRepo.GetNewsById_LocationList("usp_GetNewsById_LocationList", new { newsId = newsId });
        //        return result;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public FrontEndVM GetAllVacancies_LocationList()
        //{
        //    try
        //    {
        //        var result = _unitOfWork.FrontEndRepo.GetAllVacancies_LocationList("usp_GetAllVacancies_LocationList");
        //        return result;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public FrontEndVM GetVacancyById_LocationList(int vacancyId)
        //{
        //    try
        //    {
        //        var result = _unitOfWork.FrontEndRepo.GetVacancyById_LocationList("usp_GetVacancyById_LocationList", new { vacancyId = vacancyId });
        //        return result;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public FrontEndVM GetOurValues_LocationList()
        //{
        //    try
        //    {
        //        var result = _unitOfWork.FrontEndRepo.GetOurValues_LocationList("usp_GetOurValues_LocationList");
        //        return result;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public FrontEndVM GetFrontEndViewHomePageDetails()
        //{
        //    try
        //    {
        //        var result = _unitOfWork.FrontEndRepo.GetFrontEndViewHomePageDetails("usp_GetFrontEndViewHomePageDetails");
        //        return result;
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #region Documents

        public List<DocumentGridFrontEnd> GetAllDocuments(string searchKeyword)
        {
            try
            {
                var result = _unitOfWork.FrontEndRepo.GetAllDocuments("usp_GetDocuments", new { SearchKeyword = searchKeyword });
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DocumentGridFrontEnd> GetDocumentsTitle()
        {

            try
            {
                var result = _unitOfWork.FrontEndRepo.GetAllDocumentsTitle("usp_GetDocumentsTitle");
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region FAQS

        public SearchFAQsList GetAllSearchFaqs(string Keyword)
        {
            try
            {
                SearchFAQsList obj = new SearchFAQsList();

                obj.FAQsList = _unitOfWork.FrontEndRepo.GetSearchFAQs("usp_GetSearchFAQs", new { SearchKeyword = Keyword });

                foreach (var faq in obj.FAQsList)
                {
                    obj.FAQsAttachedDocumentList = _unitOfWork.FrontEndRepo.GetSearchFAQsAttachedDocument("usp_GetSearchFAQAttachedDocument", new { FAQsId = faq.FAQsId });
                }



                return obj;
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<FAQGridVM> GetFaqsQuestions()
        {
            try
            {
                List<FAQGridVM> obj = new List<FAQGridVM>();

                obj = _unitOfWork.FrontEndRepo.GetFaqsQuestions("usp_GetFaqsQuestions");

                return obj;
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }



        #endregion

        #region departments

        public FrontEndVM GetIntranetDepartmentList(int id)
        {
            try
            {

                //var result = _unitOfWork.FrontEndRepo.GetAllLocationList("usp_GetAllLocations");
                var result = new FrontEndVM();
                result.PostalCodesList = _unitOfWork.FrontEndRepo.GetAllLocationList("usp_GetAllLocations");
                //result.DocumentsByDepartment = _unitOfWork.FrontEndRepo.GetDocumentListByDepartmentId("usp_GetAllDocumentAndFolder", new { DepartmentId = id });
                result.NewsList = _unitOfWork.FrontEndRepo.GetAllNewsForDocuments("usp_GetFrontendNewsByDepartMentId", new { DepartmentId = id });
                result.DepartmentsList = _unitOfWork.FrontEndRepo.GetAllDepartMentsForMenu("usp_GetAllDepartments");

                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Search By Person
        public List<FrontEndUser> GetPersonsList(string personName, string JobTitle , int loggedUserId)
        {
            try
            {
                if (string.IsNullOrEmpty(personName)) personName = null;
                if (string.IsNullOrEmpty(JobTitle)) JobTitle = null;

                List<FrontEndUser> userList = new List<FrontEndUser>();

                userList = _unitOfWork.FrontEndRepo.GetPersonsList("usp_GetPersonsListByNameAndJobTitle", new { personName = personName, JobTitle = JobTitle, loggedUserId = loggedUserId });

                return userList;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ADUserInfo GetEmployeeADRecordsByWindowsUserId(string windowsUserId)
        {
            try
            {
                var result = _unitOfWork.Repo.ExecuteQuery<ADUserInfo>("usp_GetEmployeeADRecordsByWindowsUserId", SqlCommandType.StoredProcedure, new { WindowsUserId = windowsUserId }).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetRewardRecipientMailContentToViewInBrowser(Guid id)
        {
            try
            {
                var result = _unitOfWork.Repo.ExecuteQuery<string>("usp_GetRewardRecipientMailContentToViewInBrowser", SqlCommandType.StoredProcedure, new { GeneratedMailId = id }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool IsEmailExist(string emailAddress, string userId)
        {
            try
            {
                userId = userId == "undefined" ? "0" : userId;
                var result = _unitOfWork.AdminRepo.IsEmailExistInEndUser(emailAddress, Convert.ToInt32(userId));
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        public BaseResult SaveLeaveRequest(LeaveRequest leavevm)
        {
            try
            {

                return _unitOfWork.FrontEndRepo.SaveLeaveRequest(leavevm);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Get All Leave types from unitOfWork context
        /// </summary>
        /// <returns>List of LeaveRequest</returns>
        public List<LeaveRequestType>  GetAllLeavetypes()
        {
            try
            {
                List<LeaveRequestType> obj = new List<LeaveRequestType>();
                obj = _unitOfWork.FrontEndRepo.GetAllLeavetypes("usp_GetAllLeaveTypes");
                return obj;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<LeaveRequestType> GetAllBankHolidays()
        {
            try
            {
                List<LeaveRequestType> obj = new List<LeaveRequestType>();
                obj = _unitOfWork.FrontEndRepo.GetAllBankHolidays("usp_GetAllBankHolidays");
                return obj;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public LeaveRequest GetALeaveDetailByID(int leaveid)
        {
            try
            {
                var result = _unitOfWork.FrontEndRepo.GetALeaveDetailByID("usp_GetABookedLeaveByID", new { LeaveId = leaveid });
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        /// <summary>
        /// Get All Booked Leaves from unitOfWork context
        /// </summary>
        /// <returns>List of LeaveRequest</returns>
        public List<LeaveRequest> GetAllBookedLeaves(string employeecode, DateTime startdate, DateTime enddate)
        {
            try
            {
                List<LeaveRequest> obj = new List<LeaveRequest>();
                obj = _unitOfWork.FrontEndRepo.GetAllBookedLeaves("usp_GetAllBookedLeaves", new { EmployeeNo = employeecode, StartDate = startdate, EndDate = enddate });
                return obj;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        public decimal CountBankHolidays(DateTime startdate, DateTime enddate,string starttime,string endtime)
        {
            try
            {
                decimal obj = 0;
                obj = _unitOfWork.FrontEndRepo.CountBankHolidays("usp_CountBankHolidays", new {StartDate = startdate, EndDate = enddate, StartTime= starttime,EndTime=endtime });
                return obj;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        public bool IsLeaveBalanceExist(decimal quantity, int userid)
        {
            return _unitOfWork.FrontEndRepo.IsLeaveBalanceExist("usp_IsBalanceLeaveExists", new { Quantity = quantity, UserID = userid });
        }
        public bool IsExistBankHolidayByDate(DateTime date)
        {
            return _unitOfWork.FrontEndRepo.IsExistBankHolidayByDate("usp_CheckBankHolidayByDate", new { Date = date });
        }
        public List<LeaveManagement> GetPendingLeaveRequests(int userId)
        {
            try
            {
                var result = _unitOfWork.FrontEndRepo.GetPendingLeaveRequests("usp_GetPendingLeaveRequests", new { UserID = userId });
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                foreach (LeaveManagement item in result)
                {
                    if (item.LeaveType != null && !string.IsNullOrEmpty(item.LeaveType))
                    {
                        item.LeaveType = textInfo.ToTitleCase(item.LeaveType);
                    }
                }
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LeaveManagement> GetApprovedLeaveRequests(int userId)
        {
            try
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                var result = _unitOfWork.FrontEndRepo.GetPendingLeaveRequests("usp_GetApprovedLeaveRequests", new { UserID = userId });                
                foreach (LeaveManagement item in result)
                {
                    if (item.LeaveType != null && !string.IsNullOrEmpty(item.LeaveType))
                    {
                        item.LeaveType = textInfo.ToTitleCase(item.LeaveType);
                    } 
                }
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult CancelePendingLeaveRequest(int leaveId)
        {
            try
            {
                var result = _unitOfWork.FrontEndRepo.CancelePendingLeaveRequest("usp_CancelePendingLeaveRequest", leaveId);
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LeaveCount GetHolidayEntitlement(int userId)
        {
            try
            {
                LeaveCount result = _unitOfWork.FrontEndRepo.GetHolidayEntitlement("usp_GetHolidayEntitlement", userId);
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
