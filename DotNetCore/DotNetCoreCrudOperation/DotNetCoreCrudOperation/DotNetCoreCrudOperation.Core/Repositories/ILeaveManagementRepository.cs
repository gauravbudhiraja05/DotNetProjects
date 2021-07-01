using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.LeaveRequest;
using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.Core.Repositories
{
   public interface ILeaveManagementRepository
    {
        LeaveRequest GetAllLeaveRequests(string query, object param);
        LeaveCancelledByLM DeleteLeaveByIds(string query, DeleteItemVM deleteItems);
        List<LeaveStatusList> GetAllLeaveStatus(string query);
        LeaveRequestLM GetPreRequisitesDataToAddLeaves(string query, object param);
        EmployeeDetails GetEmployeeDetailsById(string query, string userId);
        BaseResult SaveLeavesByLM(LeaveRequestLM leave);
        decimal CountBankHolidays(string query, object param);
        bool IsExistBankHolidayByDate(string query, object param);
    }
}
