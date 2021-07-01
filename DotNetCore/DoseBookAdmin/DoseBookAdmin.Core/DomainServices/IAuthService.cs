using DoseBookAdmin.ViewModels;
using DoseBookAdmin.ViewModels.Global;

namespace DoseBookAdmin.Core.DomainServices
{
    public interface IAuthService
    {
        /// <summary>
        /// It's authenticated that user credentials valid or not
        /// </summary>
        /// <param name="user">AuthUserVM</param>
        /// <returns>boolean</returns>
        LoggedInUser UserAuthenticate(AuthUserVM user);
    }
}
