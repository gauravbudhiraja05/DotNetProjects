using Dream14.Core.DomainServices;
using Dream14.Core.Repositories;
using Dream14.ViewModels.Global;
using Dream14.ViewModels.SuperAdmin;
using System;
using System.Collections.Generic;

namespace Dream14.Services
{
    public class AdminService : IAdminService
    {

        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// AdminService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Admin Users

        public IEnumerable<AdminUser> GetAllAdminUsers()
        {
            return _unitOfWork.AdminRepo.GetAllAdminUsers("usp_GetAllAdminUsers");
        }

        public BaseResult CheckEmailExist(string emailAddress)
        {
            return _unitOfWork.AdminRepo.CheckEmailExist("usp_CheckEmailExist", new { emailAddress });
        }

        public BaseResult SaveAdminUser(AdminUser adminUser)
        {
            try
            {
                return _unitOfWork.AdminRepo.SaveAdminUser("usp_AddAdminUser", new
                {
                    adminUser.FullName,
                    adminUser.EmailAddress,
                    adminUser.Password,
                    adminUser.RoleName,
                    adminUser.IsActive,
                    adminUser.CreatedBy,
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AdminUser GetAdminUserById(int Id)
        {
            try
            {
                return _unitOfWork.AdminRepo.GetAdminUserById("usp_GetAdminUserById", new { id = Id });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public BaseResult UpdateAdminUser(AdminUser adminUser)
        {
            try
            {
                return _unitOfWork.AdminRepo.UpdateAdminUser("usp_UpdateAdminUser", new
                {
                    adminUser.Id,
                    adminUser.FullName,
                    adminUser.EmailAddress,
                    adminUser.Password,
                    adminUser.ModifiedBy,
                });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public BaseResult DeleteAdminUsersByIds(DeleteItemVM targetIds)
        {
            try
            {
                var adminIds = string.Join('|', targetIds.ItemIds);
                return _unitOfWork.AdminRepo.DeleteAdminUsersByIds("usp_DeleteAdminByIds", new
                {
                    adminIds,
                    targetIds.DeletedBy,
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
