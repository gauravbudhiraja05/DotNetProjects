using DoseBookAdmin.ViewModels.User;
using System.Collections.Generic;

namespace DoseBookAdmin.Core.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers(string query);
    }
}
