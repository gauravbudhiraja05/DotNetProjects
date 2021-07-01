using HiveReport.Dto.Common;
using HiveReport.Dto.User;

namespace HiveReport.WebAdmin.User.Af
{
    public interface IUserAf
    {
        /// <summary>
        /// IsEmailExists will check Email Exists or not
        /// </summary>
        /// <param name="emailAddress">emailAddress</param>
        /// <returns>base result object</returns>
        BaseResultDto IsEmailExists(string emailAddress);

        /// <summary>
        /// AddUserInformation will add the user information
        /// </summary>
        /// <param name="registeredUser">registeredUser</param>
        /// <returns>Registerted User object</returns>
        BaseResultDto AddUserInformation(RegisteredUserDto registeredUser);

        /// <summary>
        /// UserAuthenticate will authenticate the user is valid or not
        /// </summary>
        /// <param name="authUserDto">AuthUser object</param>
        /// <returns>LoggedInUser object</returns>
        LoggedInUserDto UserAuthenticate(AuthUserDto authUserDto);
    }
}
