using Dream14.ViewModels;
using Dream14.ViewModels.Global;

namespace Dream14.Core.DomainServices
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
