using System;
using PickfordsIntranet.ViewModels;
using PickfordsIntranet.ViewModels.Auth;
using PickfordsIntranet.ViewModels.Global;

namespace PickfordsIntranet.Core.DomainServices
{
    /// <summary>
    /// IAuthService describes the implementation required for User authentication, authorization and security
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// It's authenticated that user credentials valid or not
        /// </summary>
        /// <param name="user">AuthUserVM</param>
        /// <returns>boolean</returns>
        LoggedInUser UserAuthenticate(AuthUserVM user);

        /// <summary>
        /// Get RoleName by User User Name
        /// </summary>
        /// <param name="userName:emailId"></param>
        /// <returns>string</returns>
        string RoleByUserName(string userName);

        /// <summary>
        /// Change the User's Password using Old Password Authentication
        /// </summary>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        bool ChangePassword(ChangePasswordVM userPwd);

        /// <summary>
        /// Check email is exist or not for password reset
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        FieldCheckVM IsEmailExistForPasswordReset(string email);

        /// <summary>
        /// Generate token for reset password
        /// </summary>
        /// <param name="email">Email Address</param>
        /// <returns>Token</returns>
        ResetPasswordForEmailTemplateVM GenerateTokenForResetPassword(string email);

        /// <summary>
        /// Token is valid or not for password reset
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns>true/false</returns>
        bool IsTokenValidForPasswordReset(Guid token);

        /// <summary>
        /// Reset passord using token
        /// </summary>
        /// <param name="resetPassword">ResetPasswordVM data structure </param>
        /// <returns>BaseResult</returns>
        BaseResult ResetPassword(ResetPasswordVM resetPassword);
        string GetEmailTemplateHtmlStringByName(string v);
    }
}
