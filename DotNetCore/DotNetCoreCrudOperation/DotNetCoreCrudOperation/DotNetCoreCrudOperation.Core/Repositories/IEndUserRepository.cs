using PickfordsIntranet.ViewModels.EndUser;
using PickfordsIntranet.ViewModels.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.Core.Repositories
{
    public interface IEndUserRepository
    {
        /// <summary>
        /// Get all admin users from target data source
        /// </summary>
        /// <returns>List of AdminUserGridItemVM Entity</returns>
        IEnumerable<EndUserGridItemVM> GetAllEndUsers(string query);

        /// <summary>
        /// Check EmailId already exist or not
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="userId"></param>
        /// <returns>true/false</returns>
        bool IsEmailExist(string emailAddress, int userId);

        /// <summary>
        /// Save user
        /// </summary>
        /// <param name="user">End data structure</param>
        /// <returns>true/false</returns>
        BaseResult SaveUser(EndUserVM user);

        /// <summary>
        ///  Get admin user by user id
        /// </summary>
        /// <param name="query">Stored procedure name that return End User by User Id</param>
        /// <param name="param">UserId</param>
        /// <returns>End User Info as AdminUserVM</returns>
        EndUserVM GetEndUserById(string query, object param);

        /// <summary>
        /// Update End user
        /// </summary>
        /// <param name="user">End User as EndUserVM data structure</param>
        /// <returns>result as Baseresult</returns>
        BaseResult UpdateEndUser(EndUserVM user);

        /// <summary>
        /// Delete End users by Ids
        /// </summary>
        /// <param name="query">Stored procedure name</param>
        /// <param name="deleteItems">all user ids </param>
        /// <returns>BaseResult</returns>
        BaseResult DeleteEndUserByIds(string query, DeleteItemVM deleteItems);
    }
}
