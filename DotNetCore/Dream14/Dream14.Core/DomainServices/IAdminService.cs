using Dream14.ViewModels.Global;
using Dream14.ViewModels.SuperAdmin;
using System.Collections.Generic;

namespace Dream14.Core.DomainServices
{
    public interface IAdminService
    {
        /// <summary>
        /// GetAllAdminUsers will return all end Users
        /// </summary>
        /// <returns>Admin User List</returns>
        IEnumerable<AdminUser> GetAllAdminUsers();

        /// <summary>
        /// Check Email Exists
        /// </summary>
        /// <param name="emailAddress">emailAddress</param>
        /// <returns>Base result</returns>
        BaseResult CheckEmailExist(string emailAddress);

        /// <summary>
        /// Save Admin User
        /// </summary>
        /// <param name="user">AdminUser data structure</param>
        /// <returns>Base result</returns>
        BaseResult SaveAdminUser(AdminUser user);

        /// <summary>
        /// Get Admin User Detail
        /// </summary>
        /// <param name="id">Admin User Ide</param>
        /// <returns>Admin User Object</returns>
        AdminUser GetAdminUserById(int id);

        /// <summary>
        /// Update Admin User
        /// </summary>
        /// <param name="user">AdminUser data structure</param>
        /// <returns>Base result</returns>
        BaseResult UpdateAdminUser(AdminUser user);

        /// <summary>
        /// Delete Admin Users By Ids
        /// </summary>
        /// <param name="targetIds">DeleteItem data structure</param>
        /// <returns>Base result</returns>
        BaseResult DeleteAdminUsersByIds(DeleteItemVM targetIds);

    }
}
