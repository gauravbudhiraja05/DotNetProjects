using DoseBookAdmin.Dto.User;
using DoseBookAdmin.Entity.User;

namespace DoseBookAdmin.WebAdmin.User.Mapping
{
    public class UserMapping
    {
        public AuthUserEntity AutoDto2AuthEntity(AuthUserDto authUserDto)
        {
            AuthUserEntity authUserEntity = new AuthUserEntity()
            {
                Email = authUserDto.Email,
                Password = authUserDto.Password,
            };

            return authUserEntity;
        }

        public LoggedInUserDto LoggedInUserEntity2LoggedInUserDto(LoggedInUserEntity loggedInUserEntity)
        {
            LoggedInUserDto loggedInUserDto = new LoggedInUserDto()
            {
                EmailId = loggedInUserEntity.EmailId,
                FullName = loggedInUserEntity.FullName,
                UserId = loggedInUserEntity.UserId,
                IsSuccess = loggedInUserEntity.IsSuccess,
                Message = loggedInUserEntity.Message,
            };

            return loggedInUserDto;
        }
    }
}
