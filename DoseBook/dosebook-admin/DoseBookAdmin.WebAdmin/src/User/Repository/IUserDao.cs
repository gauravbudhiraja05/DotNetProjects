using DoseBookAdmin.Entity.User;

namespace DoseBookAdmin.WebAdmin.User.Repository
{
    public interface IUserDao
    {
        LoggedInUserEntity UserAuthenticate(string query, object param);
    }
}
