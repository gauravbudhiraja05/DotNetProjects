using DoseBookAdmin.ViewModels.User;
using System.Collections.Generic;

namespace DoseBookAdmin.Core.DomainServices
{
    public interface IUserService
    {
        /// <summary>
        /// GetAllUsers will return all Users
        /// </summary>
        /// <returns></returns>
        IEnumerable<User> GetAllUsers();
    }
}
