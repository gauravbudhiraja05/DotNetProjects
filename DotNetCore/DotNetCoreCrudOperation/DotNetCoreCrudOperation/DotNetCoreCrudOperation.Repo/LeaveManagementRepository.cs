using PickfordsIntranet.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using PickfordsIntranet.ViewModels.LeaveRequest;
using PickfordsIntranet.ViewModels.Global;
using System.Linq;

namespace PickfordsIntranet.Repo
{
 public class LeaveManagementRepository:  Repository, ILeaveManagementRepository
    {

        /// <summary>
        /// RewardRepository Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        public LeaveManagementRepository(IDbConnection connection) : base(connection)
        {

        }
        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection as IDbConnection; }
        }

        public LeaveRequest GetAllLeaveRequests(string query, object param)
        {
            try
            {
                LeaveRequest result = new LeaveRequest();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    result.LeaveDetailList = multi.Read<LeaveDetails>().AsList();                    
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        public LeaveCancelledByLM DeleteLeaveByIds(string query, DeleteItemVM deleteItems)
        {
            try
            {
                LeaveCancelledByLM result = new LeaveCancelledByLM();
                var IdsWithDelimitedPipeline = string.Join('|', deleteItems.ItemIds);               
                using (var multi = Connection.QueryMultiple(query, new { LeavesIds = IdsWithDelimitedPipeline, DeletedBy = deleteItems.DeletedBy }, null, null, CommandType.StoredProcedure))
                {
                    result.CancelledLeaveDetails = multi.Read<LeaveRequestLM>().ToList();
                    result.CancelledLeaveStatus = multi.Read<BaseResult>().FirstOrDefault();                    
                }
              return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<LeaveStatusList> GetAllLeaveStatus(string query)
        {
            try
            {
                var result = Connection.Query<LeaveStatusList>(query, commandType: CommandType.StoredProcedure).ToList();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public LeaveRequestLM GetPreRequisitesDataToAddLeaves(string query, object param)
        {   
            try
            {
                LeaveRequestLM result = new LeaveRequestLM();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    List<Employee> objempList = new List<Employee>();
                    objempList = multi.Read<Employee>().AsList();
                    Employee emp = new Employee();
                    emp.Id = 0;
                    emp.EmployeeName = "Please Select";
                    objempList.Insert(0, emp);
                    result.employees = objempList;

                    result.ApprovedLeaveMembers = multi.Read<ApprovedLeaveMember>().AsList();

                    List<LeaveStatusList> objStatusList = new List<LeaveStatusList>();                    
                    objStatusList = multi.Read<LeaveStatusList>().AsList();                   
                    LeaveStatusList objleve = new LeaveStatusList();
                    objleve.Id = 0;
                    objleve.LeaveStatus = "Please Select";
                    objStatusList.Insert(0, objleve);
                    result.StatusList = objStatusList;

                    result.LeaveTypeList = multi.Read<LeaveType>().ToList();
                    result.BankHolidays = multi.Read<BankHoliday>().ToList();

                    result.LeaveID = 0;
                    var lM = multi.Read<LeaveRequestLM>().FirstOrDefault();
                    if (lM != null)
                    {                        
                        result.LeaveID = lM.LeaveID;
                        result.UserId = lM.UserId;
                        if (!String.IsNullOrEmpty(lM.CreatedDate))
                        {
                            result.DateLeaveRequest = Convert.ToDateTime(lM.CreatedDate).ToString("dd/MM/yyyy");
                        }
                        if (!String.IsNullOrEmpty(lM.StartDate))
                        {
                            result.StartDate = Convert.ToDateTime(lM.StartDate).ToString("dd/MM/yyyy").Replace("-", "/"); 
                        }                        
                        result.StartTime = lM.StartTime;

                        if (!String.IsNullOrEmpty(lM.ReturnBackDate))
                        {
                            result.EndDate = Convert.ToDateTime(lM.ReturnBackDate).ToString("dd/MM/yyyy").Replace("-", "/"); ;
                        }
                        result.EndTime = lM.ReturnBackTime;   
                        
                        result.Status = lM.Status;
                        if (lM.Status=="Approved")
                        {
                            result.ApprovedDate = Convert.ToDateTime(lM.UpdatedDate).ToString("dd/MM/yyyy").Replace("-", "/"); ;
                        }
                        else
                        {
                            result.ApprovedDate = "";
                        }
                        result.PreviousLeaveStatus= lM.Status;
                        result.Description = lM.Description;
                        result.LeaveDuration = lM.LeaveDuration;
                        result.FirstName = lM.FirstName;
                        result.EmailAddress = lM.EmailAddress;
                        result.LeaveTypeId = lM.LeaveTypeId;
                        result.Comment = lM.Comment;
                        result.FileName = lM.FileName;
                    } 
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public EmployeeDetails GetEmployeeDetailsById(string query, string userId)
        {
            try
            {
                int iuserId = Convert.ToInt32(userId);
                var result = Connection.Query<EmployeeDetails>(query, new { userId = iuserId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return result;
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
                var result = this.ExecuteQuery<BaseResult>("usp_AddEditLMLeaveRequest", SqlCommandType.StoredProcedure,
                    new
                    {
                        UserID = leavevm.UserId,
                        LeaveID = leavevm.LeaveID,
                        StartDate = leavevm.StartDate,
                        EndDate = leavevm.EndDate,
                        StartTime = leavevm.StartTime,
                        EndTime = leavevm.EndTime,
                        Status = leavevm.Status,
                        Description = leavevm.Description,
                        LeaveDuration = Convert.ToDecimal(leavevm.LeaveDuration),
                        LeaveTypeId = leavevm.LeaveTypeId,
                        Comment = leavevm.Comment,
                        FileName = leavevm.FileName,
                        ReturnBackDate = leavevm.ReturnBackDate
                    }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public decimal CountBankHolidays(string query, object param)
        {
            decimal obj = 0;
            try
            {
                obj = Connection.Query<decimal>(query, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return obj;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool IsExistBankHolidayByDate(string query, object param)
        {
            try
            {
                bool isExist = Connection.Query<bool>(query, param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                return isExist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
