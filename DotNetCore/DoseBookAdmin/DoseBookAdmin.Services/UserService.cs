using DoseBookAdmin.Core.DomainServices;
using DoseBookAdmin.Core.Repositories;
using DoseBookAdmin.ViewModels.User;
using System.Collections.Generic;

namespace DoseBookAdmin.Services
{
    public class UserService : IUserService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// UserService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _unitOfWork.UserRepo.GetAllUsers("usp_GetAllUsers");
        }
    }
}
