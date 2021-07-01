using PickfordsIntranet.ViewModels;
using PickfordsIntranet.ViewModels.Auth;
using PickfordsIntranet.ViewModels.Documents;
using PickfordsIntranet.ViewModels.EndUser;
using PickfordsIntranet.ViewModels.FrontEnd;
using PickfordsIntranet.ViewModels.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.Core.Repositories.FrontEnd
{
    public interface IFrontEndRepository
    {

        FrontEndVM GetFrontEndVMDetails(int id, string type);

        //FrontEndVM GetAllLocationList(string query);

        FrontEndUser FrontEndUserAuthenticate(AuthUserVM user);

        GazetersDetail GetSelectedPostalCodeDetail(string query, object param);

        BaseResult UpdateEndUserProfile(FrontEndUser user);

        FrontEndUser GetFrontEndUserById(string query, object param);

        List<PostalCode> GetAllLocationList(string query);
        //FrontEndVM GetAllNews_LocationList(string query);

        //FrontEndVM GetNewsById_LocationList(string query, object param);

        //FrontEndVM GetAllVacancies_LocationList(string query);

        //FrontEndVM GetVacancyById_LocationList(string query, object param);

        //FrontEndVM GetOurValues_LocationList(string query);

        //FrontEndVM GetFrontEndViewHomePageDetails(string query);

        List<DocumentGridFrontEnd> GetAllDocuments(string query, object param);

        List<DocumentGridFrontEnd> GetSearchFAQsAttachedDocument(string query, object param);

        List<FAQGridVM> GetSearchFAQs(string query, object param);

        DocumentGridItemVM GetDocumentListByDepartmentId(string query, object param);

        List<News> GetAllNewsForDocuments(string query, object param);

        List<DepartmentVM> GetAllDepartMentsForMenu(string query);

        BaseResult SaveMyDepartment(int deptId, int userId);

        BaseResult AddMyFavouriteDocument(int docId, int userId);

        List<DocumentGridFrontEnd> GetFavouriteDocument(string query, object param);

        bool RemoveFromFavouriteList(string query, object param);

        List<FrontEndUser> GetPersonsList(string query, object param);

        FrontEndVM GetNewsListBydepartmentId(int departmentId);

        FrontEndVM GetVacancyListBydepartmentId(int departmentId);

        FrontEndVM GetAllDepartmentForNews();

        FrontEndVM GetAllDepartmentsForVacancies();

        List<FAQGridVM> GetFaqsQuestions(string query);

        List<DocumentGridFrontEnd> GetAllDocumentsTitle(string query);

        List<DocumentGridFrontEnd> GetFeaturedDocumentList(string query, object param);

        DepartmentVM GetDepartmentValuesById(string query, object param);

        Registration GetDepartmentListForLogin();

        int CreateEndUserProfile_New(FrontEndUser user);

        BaseResult SaveLeaveRequest(LeaveRequest leavevm);
        List<LeaveRequestType> GetAllLeavetypes(string query);
         List<LeaveRequestType> GetAllBankHolidays(string query);
       LeaveRequest GetALeaveDetailByID(string query, object param);
        List<LeaveRequest> GetAllBookedLeaves(string query, object param);
       decimal CountBankHolidays(string query, object param);

        List<LeaveManagement> GetPendingLeaveRequests(string query, object param);

        List<LeaveManagement> GetApprovedLeaveRequests(string query, object param);

        BaseResult CancelePendingLeaveRequest(string query, int leaveId);
        LeaveCount GetHolidayEntitlement(string query, int userid);

        bool IsLeaveBalanceExist(string query, object param);
        bool IsExistBankHolidayByDate(string query, object param);
    }
}
