using PickfordsIntranet.ViewModels.EndUser;
using PickfordsIntranet.ViewModels.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.Core.DomainServices
{
    /// <summary>
    /// The IEndUser Inteface describes the implementation required for End User 
    /// </summary>
    public interface IEndUserService
    {
        /// <summary>
        /// GetAllEndUsers will return all end Users
        /// </summary>
        /// <returns></returns>
        IEnumerable<EndUserGridItemVM> GetAllEndUsers();

        /// <summary>
        /// Check Email Address already exist or not
        /// </summary>
        /// <param name="emailAddress">Email Address</param>
        /// <param name="id">User Id</param>
        /// <returns>true/false</returns>
        bool IsEmailExist(string emailAddress, string id);

        /// <summary>
        /// Save End User
        /// </summary>
        /// <param name="user">EndUserVM data structure</param>
        /// <returns>true/false</returns>
        BaseResult SaveEndUser(EndUserVM user);

        /// <summary>
        /// Get admin user by user Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>Info of End User using EndUserVM</returns>
        EndUserVM GetEndUserById(int id);

        /// <summary>
        /// Update End User
        /// </summary>
        /// <param name="user">EndUserVM data structure</param>
        /// <returns>true/false</returns>
        BaseResult UpdateEndUser(EndUserVM user);

        /// <summary>
        /// Delete end users by ids
        /// </summary>
        /// <param name="allUserIds">list of user Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResult DeleteEndUsersByIds(DeleteItemVM targetIds);
    }
}
