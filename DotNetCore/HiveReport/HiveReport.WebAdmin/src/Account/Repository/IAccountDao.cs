using HiveReport.Entity.User;
using System.Collections.Generic;

namespace HiveReport.WebAdmin.Account.Repository
{
    public interface IAccountDao
    {
        Dictionary<int, string> GetDesignationList();

        Dictionary<int, string> GetDepartmentList(string savedBy);

        Dictionary<int, string> GetClientList(int departmentId);

        Dictionary<int, string> GetLOBList(int departmentId, int clientId);

        bool CheckAvailableEmployeeId(int employeeId);

        bool CheckAvailableUserId(string emailAddress);

        bool AddNewDesignation(string designation);

        void AddAccountUTypeDetails(string userType, string userId);
        
        void AddWarsCountLogin(string userId);

        void AddBuddyDetails(RegisteredUserEntity registeredUser);
    }
}
