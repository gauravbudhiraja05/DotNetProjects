using Microsoft.AspNetCore.Http;
using PickfordsIntranet.ViewModels.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PickfordsIntranet.ViewModels.LeaveRequest
{
    public class LeaveRequest
    {
        public List<LeaveDetails> LeaveDetailList { get; set; }
        public List<LeaveStatusList> StatusList { get; set; }
    }

    public class LeaveDetails
    {
        public int LeaveID { get; set; }
        public string EmployeeNo { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }     
        public string Status { get; set; }
        public string CreatedDate { get; set; }
    }

    public class LeaveStatusList
    {
        public int Id { get; set; }
        public string LeaveStatus { get; set; }
    }

    public class LeaveRequestLM
    {   
        public List<Employee> employees { get; set; }
        public List<ApprovedLeaveMember> ApprovedLeaveMembers { get; set; }        
        public List<LeaveStatusList> StatusList { get; set; }
        public List<LeaveType> LeaveTypeList { get; set; }
        public List<BankHoliday> BankHolidays { get; set; }

        [Required(ErrorMessage = "Please select employee")]
        public int UserId { get; set; }  
        
        public int LeaveID { get; set; }
        public string DateLeaveRequest { get; set; }

        [Required(ErrorMessage = "Please enter the date of your first day of leave")]
        public string StartDate { get; set; }

        [Required(ErrorMessage = "Please select If you are leaving in the morning or afternoon")]
        public string StartTime { get; set; }

        [Required(ErrorMessage = "Please select If you are leaving in the morning or afternoon")]
        public string EndDate { get; set; }

        [Required(ErrorMessage = "Please select If you are returning in the morning or afternoon")]
        public string EndTime { get; set; }

        public string CreatedDate { get; set; }
        public string Status { get; set; }
        public string UpdatedDate { get; set; }
        public string Description { get; set; }
        public string LeaveDuration { get; set; }
        public string ReturnBackDate { get; set; }
        public string ReturnBackTime { get; set; }
        public string FirstName { get; set; }
        public string EmailAddress { get; set; }
        public int LeaveTypeId { get; set; }        
        public string Comment { get; set; }
        public string FileName { get; set; }
        public string ApprovedDate { get; set; }
        public string BankHolidayCount { get; set; }
        public string NewEndDate { get; set; }
        public IFormFile FileNameData { get; set; }
        public string PreviousLeaveStatus { get; set; }
    }
    public class EmployeeDetails
    {
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Department { get; set; }
        public string TelephoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string AnnualLeaveEntitlement { get; set; }
        public string RemainingAnnualLeaveEntitlement { get; set; }
    }

    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
    }

    public class ApprovedLeaveMember
    {
        public string Name { get; set; }
        public string StartDayName { get; set; }
        public string StartDayOfMonth { get; set; }
        public string StartMonth { get; set; }
        public string StartTime { get; set; }
        public string EndDayName { get; set; }
        public string EndDayOfMonth { get; set; }
        public string EndMonth { get; set; }
        public string EndTime { get; set; }
    }

    public class LeaveType
    {
        public int LeaveTypeID { get; set; }
        public string Type { get; set; }
    }

    public class BankHoliday
    {
        public string BankHolidays { get; set; }
    }

    public class LeaveCancelledByLM
    {
        public BaseResult CancelledLeaveStatus { get; set; }
        public List<LeaveRequestLM> CancelledLeaveDetails { get; set; }
    }
}
