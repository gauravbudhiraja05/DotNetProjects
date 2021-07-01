using HiveReport.Dto.Common;
using HiveReport.Dto.User;
using HiveReport.Entity.Common;
using HiveReport.Entity.User;
using HiveReport.WebAdmin.Common.Mapping;
using HiveReport.WebAdmin.User.Mapping;
using HiveReport.WebAdmin.User.Service;

namespace HiveReport.WebAdmin.User.Af
{
    public class UserAf : IUserAf
    {
        /// <summary>
        /// Private IUserService Data Member
        /// </summary>
        private readonly IUserService _userService;

        /// <summary>
        /// Private UserMapping Data Member
        /// </summary>
        private readonly UserMapping _userMapping;

        /// <summary>
        /// Private BaseResultMapping Data Member
        /// </summary>
        private readonly BaseResultMapping _baseResultMapping;

        public UserAf(IUserService userService)
        {
            _userService = userService;
            _userMapping = new UserMapping();
            _baseResultMapping = new BaseResultMapping();
        }

        public BaseResultDto IsEmailExists(string emailAddress)
        {
            BaseResultEntity baseResultEntity = _userService.IsEmailExists(emailAddress);
            BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
            return baseResultDto;
        }

        public BaseResultDto AddUserInformation(RegisteredUserDto registeredUserDto)
        {
            RegisteredUserEntity registeredUserEntity = _userMapping.RegisteredUserDto2RegisteredUserEntity(registeredUserDto);
            BaseResultEntity baseResultEntity = _userService.AddUserInformation(registeredUserEntity);
            BaseResultDto baseResultDto = _baseResultMapping.Entity2Dto(baseResultEntity);
            return baseResultDto;
        }

        public LoggedInUserDto UserAuthenticate(AuthUserDto authUserDto)
        {
            AuthUserEntity authUserEntity = _userMapping.AuthUserDto2AuthUserEntity(authUserDto);
            LoggedInUserEntity loggedInUserEntity = _userService.UserAuthenticate(authUserEntity);
            LoggedInUserDto loggedInUserDto = _userMapping.LoggedInUserEntity2LoggedInUserDto(loggedInUserEntity);
            return loggedInUserDto;
        }
    }
}
