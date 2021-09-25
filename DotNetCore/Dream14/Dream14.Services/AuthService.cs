using Dream14.Core.DomainServices;
using Dream14.Core.Helper;
using Dream14.Core.Repositories;
using Dream14.ViewModels;
using Dream14.ViewModels.Global;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Linq;

namespace Dream14.Services
{
    public class AuthService : IAuthService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        //private IUnitOfWork _unitOfWork;
        private readonly IUnitOfWork _unitOfWork;

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
                var result = _unitOfWork.Repo.ExecuteQuery<LoggedInUser>("usp_AuthenticateAdminUser", SqlCommandType.StoredProcedure, new { user.Email, user.Password }).SingleOrDefault();

                return result;
            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}
