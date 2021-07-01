using HiveReport.Entity.Common;
using HiveReport.Entity.User;

namespace HiveReport.WebAdmin.User.Service
{
    public interface IUserService
    {
        
        /// <summary>
        /// IsEmailExists will check Email Exists or not
        /// </summary>
        /// <param name="emailAddress">emailAddress</param>
        /// <returns>base result object</returns>
        BaseResultEntity IsEmailExists(string emailAddress);

        /// <summary>
        /// AddUserInformation will add the user information
        /// </summary>
        /// <param name="registeredUser">registeredUser</param>
        /// <returns>Registerted User object</returns>
        BaseResultEntity AddUserInformation(RegisteredUserEntity registeredUser);

        /// <summary>
        /// UserAuthenticate will authenticate the user is valid or not
        /// </summary>
        /// <param name="authUserEntity">AuthUser object</param>
        /// <returns>LoggedInUser object</returns>
        LoggedInUserEntity UserAuthenticate(AuthUserEntity authUserEntity);

    }
}
