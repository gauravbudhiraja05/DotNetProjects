using HiveReport.Dto.User;
using HiveReport.Entity.Common;
using HiveReport.Entity.User;
using System.Collections.Generic;

namespace HiveReport.WebAdmin.Account.Service
{
    public interface IAccountService
    {
        List<string> GetDesignationList();

        Dictionary<int, string> GetDepartmentList(string savedBy);

        Dictionary<int, string> GetClientList(int departmentId);

        Dictionary<int, string> GetLOBList(int departmentId, int clientId);

        bool CheckAvailableEmployeeId(int employeeId);

        bool CheckAvailableUserId(string emailAddress);

        bool AddNewDesignation(string designation);

        int CheckMasterAdmin(string userid);

        BaseResultEntity AddRegisterationDetail(RegisteredUserEntity registeredUserEntity);
        
        void AddAccountUTypeDetails(string userType, string userId);
        
        void AddWarsCountLogin(string userId);

        void AddBuddyDetails(RegisteredUserEntity registeredUser);

        void AddUserDuration(string userId, string userid);
        
        void AddPasswordHistory(string emailAddress, string password, string userid);
        
        RegisteredUserEntity GetProductDemoDetail(string userid);

        BaseResultEntity AddProductDemoDetail(RegisteredUserEntity registeredUserEntity);
        
        string GetRightsOnProductCodeBasis(RegisteredUserEntity registeredUserEntity);
        
        BaseResultEntity AddParentMenuRights(List<string> rightsList, string emailAddress, string database);
        
        LoggedInUserEntity GetSearchedResult(string dropdownValue, string txtValue, string userid);
    }
}
