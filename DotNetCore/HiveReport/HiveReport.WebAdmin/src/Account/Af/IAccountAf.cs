using HiveReport.Dto.User;
using System.Collections.Generic;

namespace HiveReport.WebAdmin.Account.Af
{
    public interface IAccountAf
    {
        List<string> GetDesignationList();

        Dictionary<int, string> GetDepartmentList(string savedBy);

        Dictionary<int, string> GetClientList(int departmentId);

        Dictionary<int, string> GetLOBList(int departmentId, int clientId);
        
        bool CheckAvailableEmployeeId(int employeeId);

        bool CheckAvailableUserId(string emailAddress);
        
        string AddUserDetails(RegisteredUserDto registeredUser, string userType, string userid);
        
        LoggedInUserDto GetSearchedResult(string dropdownValue, string txtValue, string userid);
    }
}
