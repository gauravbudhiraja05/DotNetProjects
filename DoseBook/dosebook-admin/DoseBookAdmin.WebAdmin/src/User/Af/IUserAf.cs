using DoseBookAdmin.Dto.User;

namespace DoseBookAdmin.WebAdmin.User.Af
{
    public interface IUserAf
    {
        LoggedInUserDto UserAuthenticate(AuthUserDto authUserDto);
    }
}
