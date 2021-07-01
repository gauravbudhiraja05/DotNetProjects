using DoseBookAdmin.Core.DomainServices;
using DoseBookAdmin.Core.Helper;
using DoseBookAdmin.Core.Repositories;
using DoseBookAdmin.ViewModels;
using DoseBookAdmin.ViewModels.Global;
using System;
using System.Linq;

namespace DoseBookAdmin.Services
{
    public class AuthService : IAuthService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// AuthService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork object reference</param>
        public AuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public LoggedInUser UserAuthenticate(AuthUserVM user)
        {
            try
            {
                var result = _unitOfWork.Repo.ExecuteQuery<LoggedInUser>("usp_AuthenticateUser", SqlCommandType.StoredProcedure, new { user.Email, user.Password }).SingleOrDefault();
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
