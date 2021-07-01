using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.LeaveRequest;
using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.Core.DomainServices
{
   public interface ILeaveManagementService
    {
        LeaveRequest GetAllLeaveRequests(string userEmail, string leaveStatus);
        LeaveCancelledByLM DeleteLeaveByIds(DeleteItemVM deleteItems);
        List<LeaveStatusList> GetAllLeaveStatus();
        LeaveRequestLM GetPreRequisitesDataToAddLeaves(string emailAddress,int leaveId);
        EmployeeDetails GetEmployeeDetailsById(string userId);
        BaseResult SaveLeavesByLM(LeaveRequestLM leave);
        decimal CountBankHolidays(DateTime startdate, DateTime enddate, string starttime, string endtime);
        bool IsExistBankHolidayByDate(DateTime date);
    }
}
