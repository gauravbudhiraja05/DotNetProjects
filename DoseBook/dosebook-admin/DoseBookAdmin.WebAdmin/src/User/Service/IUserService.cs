using DoseBookAdmin.Entity.User;

namespace DoseBookAdmin.WebAdmin.User.Service
{
    public interface IUserService
    {
        /// <summary>
        /// It's authenticated that user credentials valid or not
        /// </summary>
        /// <param name="user">AuthUserVM</param>
        /// <returns>boolean</returns>
        LoggedInUserEntity UserAuthenticate(AuthUserEntity authUserEntity);
    }
}
