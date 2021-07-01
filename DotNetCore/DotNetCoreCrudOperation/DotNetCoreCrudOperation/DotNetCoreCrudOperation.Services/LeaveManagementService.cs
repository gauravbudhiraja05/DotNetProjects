using Microsoft.Extensions.Configuration;
using PickfordsIntranet.Core.DomainServices;
using PickfordsIntranet.Core.Repositories;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.LeaveRequest;
using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.Services
{
    public class LeaveManagementService : ILeaveManagementService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// IConfigurationRoot Data Member
        /// </summary>
        private IConfigurationRoot _config;

        /// <summary>
        /// AdminService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public LeaveManagementService(IUnitOfWork unitOfWork, IConfigurationRoot config)
        {
            try
            {
                _unitOfWork = unitOfWork;
                _config = config;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LeaveRequest GetAllLeaveRequests(string userEmail,string leaveStatus)
        {
            try
            {
                LeaveRequest leaveRequestobj = _unitOfWork.LevRepo.GetAllLeaveRequests("usp_GetAllLeaveRequests", new { UserEmail = userEmail, LeaveStatus= leaveStatus });
                return leaveRequestobj;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LeaveCancelledByLM DeleteLeaveByIds(DeleteItemVM targetIds)
        {
            try
            {
                LeaveCancelledByLM result = _unitOfWork.LevRepo.DeleteLeaveByIds("usp_DeleteLeavesByIds", targetIds);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<LeaveStatusList> GetAllLeaveStatus()
        {
            try
            {
                List<LeaveStatusList> leaveRequestobj = _unitOfWork.LevRepo.GetAllLeaveStatus("usp_GetAllLeaveStatus");
                return leaveRequestobj;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public LeaveRequestLM GetPreRequisitesDataToAddLeaves(string emailAddress, int leaveId)
        {
            try
            {
                LeaveRequestLM leave = _unitOfWork.LevRepo.GetPreRequisitesDataToAddLeaves("usp_GetPreRequisitesDataToCreateLeaves", new { emailAddress = emailAddress, leaveId = leaveId });                
                return leave;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EmployeeDetails GetEmployeeDetailsById(string userId)
        {
            try
            {
                EmployeeDetails employeeDetails = _unitOfWork.LevRepo.GetEmployeeDetailsById("usp_GetUserDetailsById", userId);
                return employeeDetails;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult SaveLeavesByLM(LeaveRequestLM leavevm)
        {
            try
            {

                return _unitOfWork.LevRepo.SaveLeavesByLM(leavevm);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public decimal CountBankHolidays(DateTime startdate, DateTime enddate, string starttime, string endtime)
        {
            try
            {
                decimal obj = 0;
                obj = _unitOfWork.LevRepo.CountBankHolidays("usp_CountBankHolidays", new { StartDate = startdate, EndDate = enddate, StartTime = starttime, EndTime = endtime });
                return obj;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsExistBankHolidayByDate(DateTime date)
        {
            return _unitOfWork.FrontEndRepo.IsExistBankHolidayByDate("usp_CheckBankHolidayByDate", new { Date = date });
        }
    }
}
