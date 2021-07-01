using PickfordsIntranet.ViewModels;
using PickfordsIntranet.ViewModels.Auth;
using PickfordsIntranet.ViewModels.EndUser;
using PickfordsIntranet.ViewModels.FrontEnd;
using PickfordsIntranet.ViewModels.Global;
using System;
using System.Collections.Generic;

namespace PickfordsIntranet.Core.DomainServices.FrontEnd
{
    public interface IFronEndService
    {
        /// <summary>
        /// Get all front end data to display
        /// </summary>
        /// <param name="id">Id can be NewsId or vacancyId or DepartmentId</param>
        /// <param name="type">type of id that will pass at runtime like Newsid, DepartmentId and VacancyId</param>
        /// <returns>FrontEndVM: Model/Data Structure  that represents the all data on Front-End Landing</returns>
        FrontEndVM GetFrontEndVMDetails(int id, string type);

        /// <summary>
        /// Authenticate Front-End User : User's window id is available in database or not
        /// </summary>
        /// <param name="user">contain the windows user id</param>
        /// <returns>frontEndUser data structue object</returns>
        FrontEndUser FrontEndUserAuthenticate(AuthUserVM user);

        /// <summary>
        /// Update End User profile
        /// </summary>
        /// <param name="frontEndUser">FrontEnd user details/info</param>
        /// <returns>End user profile is updated or not as BaseResult</returns>
        BaseResult UpdateEndUserProfile(FrontEndUser frontEndUser);

        /// <summary>
        /// get FrontEnd User by Id
        /// </summary>
        /// <param name="Id">user id</param>
        /// <returns>FrontEnd user details as FrontEndUser</returns>
        FrontEndUser GetFrontEndUserById(Int32 Id);
        
        /// <summary>
        /// GetSelectedPostalCodeDetail will selected postal ocde details
        /// </summary>
        /// <returns></returns>
        GazetersDetail GetSelectedPostalCodeDetail(string PostalCode);
        
        /// <summary>
        /// Get All document list using search keyword
        /// </summary>
        /// <param name="searchKeyword">search keyword as string</param>
        /// <returns>List of documents</returns>
        List<DocumentGridFrontEnd> GetAllDocuments(string searchKeyword);

        /// <summary>
        /// Get FAQs search list using search keyword parameter
        /// </summary>
        /// <param name="Keyword">keyword as search sring</param>
        /// <returns>SearchFAQsList</returns>
        SearchFAQsList GetAllSearchFaqs(string Keyword);

        /// <summary>
        /// Get Department List
        /// </summary>
        /// <param name="id"></param>
        /// <returns>FrontEndVM</returns>
        FrontEndVM GetIntranetDepartmentList(int id);

        /// <summary>
        /// save Mydepartment for a particular Front End User
        /// </summary>
        /// <param name="deptId">Department Id</param>
        /// <param name="userId">Front End user id</param>
        /// <returns>mydepartment is saved or not as BaseResult</returns>
        BaseResult SaveMyDepartment(int deptId, int userId);

        /// <summary>
        /// Add my Favourite document
        /// </summary>
        /// <param name="docId">document Id</param>
        /// <param name="userId">front End user Id</param>
        /// <returns>Document is saved or not as BaseResult object</returns>
        BaseResult AddMyFavouriteDocument(int docId, int userId);

        /// <summary>
        /// Get Favourite documents list by Front end user id
        /// </summary>
        /// <param name="UserId">front End user Id</param>
        /// <returns>List of Favourite documents list</returns>
        List<DocumentGridFrontEnd> GetFavouriteDocument(int UserId);

        /// <summary>
        /// Remove favourite document by document id and user id
        /// </summary>
        /// <param name="DocId">document Id</param>
        /// <param name="UserId">user Id</param>
        /// <returns>removed or not</returns>
        bool RemoveFromFavouriteList(int DocId, int UserId);

        /// <summary>
        /// Get Person List using job title or person name
        /// </summary>
        /// <param name="personName">person name</param>
        /// <param name="JobTitle">job title</param>
        /// <param name="loggedUserId">logged user id</param>
        /// <returns></returns>
        List<FrontEndUser> GetPersonsList(string personName, string JobTitle, int loggedUserId);

        /// <summary>
        /// Ge News by Department Id
        /// </summary>
        /// <param name="departmentId">department id</param>
        /// <returns>FrontEndVM</returns>
        FrontEndVM GetNewsOnDepartmentId(int departmentId);

        /// <summary>
        /// Get Vacancies by Department Id
        /// </summary>
        /// <param name="depoartmentId">department Id</param>
        /// <returns>FrontEndVM</returns>
        FrontEndVM GetVacancyOnDepartmentId(int depoartmentId);

        /// <summary>
        /// Get all department for news
        /// </summary>
        /// <returns>FrontEndVM</returns>
        FrontEndVM GetAllDepartmentsFroNews();

        /// <summary>
        /// Get all department for News
        /// </summary>
        /// <returns></returns>
        FrontEndVM GetAllDepartmentsFroVacancies();

        List<FAQGridVM> GetFaqsQuestions();

        List<DocumentGridFrontEnd> GetDocumentsTitle();

        List<DocumentGridFrontEnd> GetFeaturedDocumentList(int departmentId);

        DepartmentVM GetDepartmentValuesById(int departmentId);

        Registration GetDepartmentListForLogin();

        int CreateEndUserProfile(FrontEndUser frontEndUser);
        ADUserInfo GetEmployeeADRecordsByWindowsUserId(string windowsUserId);
        string GetRewardRecipientMailContentToViewInBrowser(Guid id);
        bool IsEmailExist(string emailAddress, string id);

        BaseResult SaveLeaveRequest(LeaveRequest leavevm);
        List<LeaveRequestType> GetAllLeavetypes();
        List<LeaveRequestType> GetAllBankHolidays();
        LeaveRequest GetALeaveDetailByID(int leaveid);
        List<LeaveRequest> GetAllBookedLeaves(string employeecode,DateTime startdate,DateTime enddate);
        decimal CountBankHolidays(DateTime startdate, DateTime enddate,string starttime,string endtime);

        List<LeaveManagement> GetPendingLeaveRequests(int userId);

        List<LeaveManagement> GetApprovedLeaveRequests(int userId);
        BaseResult CancelePendingLeaveRequest(int leaveId);

        LeaveCount GetHolidayEntitlement(int userId);

        bool IsLeaveBalanceExist(decimal quantity, int userid);
        bool IsExistBankHolidayByDate(DateTime date);
    }
}
