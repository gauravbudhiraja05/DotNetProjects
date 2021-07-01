using DoseBookAdmin.Dto.User;
using DoseBookAdmin.Entity.User;
using DoseBookAdmin.WebAdmin.User.Mapping;
using DoseBookAdmin.WebAdmin.User.Service;
using System;

namespace DoseBookAdmin.WebAdmin.User.Af
{
    public class UserAf : IUserAf
    {
        /// <summary>
        /// Private IUserService Data Member
        /// </summary>
        private IUserService _userService;

        /// <summary>
        /// Private UserMapping Data Member
        /// </summary>
        private UserMapping _userMapping;

        /// <summary>
        /// UserAf Constructor
        /// </summary>
        /// <param name="userService">IUserService</param>
        public UserAf(IUserService userService)
        {
            _userService = userService;
            _userMapping = new UserMapping();
        }

        public LoggedInUserDto UserAuthenticate(AuthUserDto authUserDto)
        {
            try
            {
                AuthUserEntity authUserEntity = _userMapping.AutoDto2AuthEntity(authUserDto);
                LoggedInUserEntity loggedInUserEntity = _userService.UserAuthenticate(authUserEntity);
                LoggedInUserDto loggedInUserDto = _userMapping.LoggedInUserEntity2LoggedInUserDto(loggedInUserEntity);
                return loggedInUserDto;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
