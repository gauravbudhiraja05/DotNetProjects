using DoseBookAdmin.Entity.User;
using DoseBookAdmin.WebAdmin.Common.Repository;

namespace DoseBookAdmin.WebAdmin.User.Service
{
    public class UserService : IUserService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// AuthService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork object reference</param>
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public LoggedInUserEntity UserAuthenticate(AuthUserEntity authUserEntity)
        {
            return _unitOfWork.UserRepo.UserAuthenticate("usp_AuthenticateUser", new { IN_Email = authUserEntity.Email, IN_Password = authUserEntity.Password });
        }
    }
}
