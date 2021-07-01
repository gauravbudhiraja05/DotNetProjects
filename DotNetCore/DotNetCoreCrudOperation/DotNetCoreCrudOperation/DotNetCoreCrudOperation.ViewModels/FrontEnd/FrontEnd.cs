using PickfordsIntranet.ViewModels.Auth;
using PickfordsIntranet.ViewModels.SuperAdmin;
using PickfordsIntranet.ViewModels.Documents;
using PickfordsIntranet.ViewModels.Global;
using System;
using System.Collections.Generic;
using System.Text;
using PickfordsIntranet.ViewModels.EndUser;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PickfordsIntranet.ViewModels.FrontEnd
{
    public class FrontEndVM
    {
        public List<PostalCode> PostalCodesList { get; set; }
        public List<News> NewsList { get; set; }
        public News NewsDetail { get; set; }
        public List<Vacancy> VacancyList { get; set; }
        public Vacancy VacancyDetail { get; set; }
        public OurValues OurValuesDetail { get; set; }
        public FeatureMessages FeatureMessagesDetail { get; set; }
        public List<DocumentGridFrontEnd> SearchDocumentList { get; set; }
        public List<DepartmentVM> DepartmentsList { get; set; }
        public DepartmentVM SelectedDepartmentValues { get; set; }
        public List<DocumentGridFrontEnd> FeaturedDocumentList { get; set; }
        public int MideValue { get; set; }
        public List<MonthStarDetails> MonthStarList { get; set; }
        public DocumentGridItemVM DocumentsByDepartment { get; set; }
        public FrontEndUser FrontEndUserDetails { get; set; }
        public List<FavouriteDocument> FavouriteDocumentsList { get; set; }
        public List<EndUserDepartment> EndUserDepartmentList { get; set; }
        public List<VacancyDepartment> VacancyDepartmentsList { get; set; }
        public List<News> FrontEndNewsList { get; set; }
        public List<NewsDepartment> NewsDepartmentsList { get; set; }
        public List<News> NewsListByDepartmentWise { get; set; }
        public LeaveManagement LeaveMgmt { get; set; }
        public LeaveRequest LeaveReq { get; set; }
        public List<LeaveRequestType> LeaveTypes { get; set; }
        public List<LeaveRequest> BookedLeaves { get; set; }
        public List<LeaveRequestType> BankHolidays { get; set; }
        public LeaveCount leaveCount { get; set; }
    }

    public class NewsDepartment
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string TelephoneNumber { get; set; }
        public string HeaderImage { get; set; }
    }

    public class VacancyDepartment
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string TelephoneNumber { get; set; }
        public string HeaderImage { get; set; }
    }

    public class EndUserDepartment
    {
            public int DepartmentId { get; set; }
            public string DepartmentName { get; set; }
            public string TelephoneNumber { get; set; }
            public string HeaderImage { get; set; }
    }

    public class PostalCode
    {
        public string Code { get; set; }
    }

    public class OurValues
    {
        public int Id { get; set; }
        public string ValueTitle { get; set; }
        public string ValueBackgroundImage { get; set; }
        public string ValueTopLeftText { get; set; }
        public string ValueTopRightText { get; set; }
        public string CommunicationTitle { get; set; }
        public string CommunicationIcon { get; set; }
        public string CommunicationImage { get; set; }
        public string CommunicationContent { get; set; }
        public string DedicationTitle { get; set; }
        public string DedicationIcon { get; set; }
        public string DedicationImage { get; set; }
        public string DedicationContent { get; set; }
        public string CareTitle { get; set; }
        public string CareIcon { get; set; }
        public string CareImage { get; set; }
        public string CareContent { get; set; }
        public string ExcellentTitle { get; set; }
        public string ExcellentIcon { get; set; }
        public string ExcellentImage { get; set; }
        public string ExcellentContent { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public byte[] Timestamp { get; set; }

    }

    public class News
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TeaserText { get; set; }
        public string Content1 { get; set; }
        public string Content2 { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public bool IsFeatureOnHomePage { get; set; }
        public string ThumbnailImage { get; set; }
        public string ThumbnailImagePath { get; set; }
        public string MainImage { get; set; }
        public string MainImagePath { get; set; }
        public string AdditionalImage1 { get; set; }
        public string AdditionalImage1Path { get; set; }
        public string AdditionalImage2 { get; set; }
        public string AdditionalImage2Path { get; set; }
        public DateTime PublishDate { get; set; }
        public string PublishDateDisplay { get; set; }
        public string CreationDate { get; set; }
        public string AuthorName { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int IsActive { get; set; }
    }

    public class GazetersDetail
    {
        public int Id { get; set; }
        public int? GazetteersId { get; set; }
        public string PostalCode { get; set; }
        public string SalesCentre { get; set; }
        public string CustomerNumber { get; set; }
        public string GroupSalesManager { get; set; }
        public string CustomerServiceManager { get; set; }
        public string AreaManager { get; set; }
        public string OperationsLocation { get; set; }
        public string BookingDeptCode { get; set; }
        public string ResourceQualityManager { get; set; }
        public string OperationalContact { get; set; }
    }

    public class Vacancy
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TeaserText { get; set; }
        public string Content1 { get; set; }
        public string Content2 { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        //public bool IsFeatureOnHomePage { get; set; }
        public string ThumbnailImage { get; set; }
        public string ThumbnailImagePath { get; set; }
        public string MainImage { get; set; }
        public string MainImagePath { get; set; }
        public string AdditionalImage1 { get; set; }
        public string AdditionalImage1Path { get; set; }
        public string AdditionalImage2 { get; set; }
        public string AdditionalImage2Path { get; set; }
        public DateTime PublishDate { get; set; }
        public string PublishDateDisplay { get; set; }
        public string CreationDate { get; set; }
        public string AuthorName { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public int IsActive { get; set; }
    }

    public class FeatureMessages
    {
        public int Id { get; set; }
        public string MessageCode { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public bool? IsLive { get; set; }
        public string AuthorName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public byte[] Timestamp { get; set; }
    }

    public class FavouriteDocument
    {
        public int Id { get; set; }
        public int EndUserId { get; set; }
        public int DocumentId { get; set; }
        public bool IsFavourite { get; set; }
    }

    public class LeaveManagement
    {
        public int LeaveID { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string LeaveType { get; set; }
        public string StartTime { get; set; }
        public string StartDayName { get; set; }
        public string StartDayOfMonth { get; set; }
        public string StartMonth { get; set; }
        public string EndTime { get; set; }
        public string EndDayName { get; set; }
        public string EndDayOfMonth { get; set; }
        public string EndMonth { get; set; }
        public decimal LeaveDuration { get; set; }
    }
    public class LeaveRequest
    {
        public int Id { get; set; }
        public int LeaveId { get; set; }
        public int EndUserId { get; set; }
        public string Filename { get; set; }
        public IFormFile FileNameData { get; set; }
        public bool IsFavourite { get; set; }
        public List<DocumentId> ListOfDocumentId { get; set; }
        public List<DocumentDetail> AttachDocuments { get; set; }
        public string CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

        [Required(ErrorMessage = "Please enter the date of your first day of leave")]
        public string LeaveStartDate { get; set; }
        public DateTime StartDate { get; set; }
 
      
        public string LeaveEndDate { get; set; }
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Please enter the date that you return back to work on")]
        public string ReturnBackDate { get; set; }

        [Required(ErrorMessage = "Please select If you are leaving in the morning or afternoon")]
        public string StartTime { get; set; }
        [Required(ErrorMessage = "Please select If you are returning in the morning or afternoon")]
        public string EndTime { get; set; }
        public int fk_LeaveTypeId { get; set; }
        public int SelectedLeaveTypeId { get; set; }
        public string EmployeeName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string EmployeeNo { get; set; }
        public string Department { get; set; }
        public string ParentCode { get; set; }
        public string CancelRequested { get; set; }
        public decimal Quantity { get; set; }
        public string LineManagerName { get; set; }

        public DateTime ToDate { get; set; }
        public string ToTime { get; set; }
        public int TotalNoofBankHolidays { get; set; }
    }
    public class DocumentId
    {
        public int IntegerDocumentId { get; set; }
    }

    public class LeaveRequestType
    {
        public string LeaveType { get; set; }
        public int LeaveTypeId { get; set; }
        public string IsComment { get; set; }
        public int LeaveTypeOrder { get; set; }
        public string BankHolidays { get; set; }
    }
    public class LeaveCount
    {
        public decimal HolidayEntitlementCount { get; set; }
        public decimal ApprovedCount { get; set; }
    }
}
