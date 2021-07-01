using HiveReport.Dto.User;

namespace HiveReport.WebAdmin.Dashboard.Af
{
    public interface IDashboardAf
    {
        /// <summary>
        /// GetDashboardUserDetails will details for the users is valid or not
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="userType">userType</param>
        /// <returns>UserDetail object</returns>
        UserDetailDto GetDashboardUserDetails(string userId, string userType);
    }
}
