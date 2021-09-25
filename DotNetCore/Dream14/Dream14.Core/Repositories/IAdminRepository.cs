using Dream14.ViewModels.Global;
using Dream14.ViewModels.SuperAdmin;
using System.Collections.Generic;

namespace Dream14.Core.Repositories
{
    public interface IAdminRepository
    {
        /// <summary>
        /// Get All AdminUsers
        /// </summary>
        /// <param name="query">procedure name</param>
        /// <returns>Admin User List</returns>
        IEnumerable<AdminUser> GetAllAdminUsers(string query);

        /// <summary>
        /// Check Email Exist
        /// </summary>
        /// <param name="query">procedure name</param>
        /// <param name="param">paramters object</param>
        /// <returns>true/false</returns>
        BaseResult CheckEmailExist(string query, object param);

        /// <summary>
        /// Save Admin user
        /// </summary>
        /// <param name="query">procedure name</param>
        /// <param name="param">paramters object</param>
        /// <returns>true/false</returns>
        BaseResult SaveAdminUser(string query, object param);

        /// <summary>
        /// Save user
        /// </summary>
        /// <param name="query">procedure name</param>
        /// <param name="param">paramters object</param>
        /// <returns>true/false</returns>
        AdminUser GetAdminUserById(string query, object param);

        /// <summary>
        /// Update Admin user
        /// </summary>
        /// <param name="query">procedure name</param>
        /// <param name="param">paramters object</param>
        /// <returns>true/false</returns>
        BaseResult UpdateAdminUser(string query, object param);

        /// <summary>
        /// Delete Admin Users By Ids
        /// </summary>
        /// <param name="query">procedure name</param>
        /// <param name="param">paramters object</param>
        /// <returns>true/false</returns>
        BaseResult DeleteAdminUsersByIds(string query, object param);
    }
}
