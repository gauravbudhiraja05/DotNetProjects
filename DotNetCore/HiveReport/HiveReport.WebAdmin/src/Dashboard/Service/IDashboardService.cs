using HiveReport.Entity.User;

namespace HiveReport.WebAdmin.Dashboard.Service
{
    public interface IDashboardService
    {
        /// <summary>
        /// GetDashboardUserDetails will details for the users is valid or not
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>UserDetailEntity object</returns>
        UserDetailEntity GetDashboardUserDetails(string userId);
        
    }
}
