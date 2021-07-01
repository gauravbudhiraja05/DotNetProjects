using PickfordsIntranet.Core.DomainServices;
using PickfordsIntranet.Core.Repositories;
using PickfordsIntranet.ViewModels.EndUser;
using PickfordsIntranet.ViewModels.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.Services
{
    public class EndUserService : IEndUserService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// AdminService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public EndUserService(IUnitOfWork unitOfWork)
        {
            try
            {
                _unitOfWork = unitOfWork;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<EndUserGridItemVM> GetAllEndUsers()
        {
            try
            {
                return _unitOfWork.EndUserRepo.GetAllEndUsers("usp_GetAllEndUsers");
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsEmailExist(string emailAddress, string id)
        {
            try
            {
                var result = _unitOfWork.EndUserRepo.IsEmailExist(emailAddress, Convert.ToInt32(id));
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult SaveEndUser(EndUserVM user)
        {
            try
            {

                return _unitOfWork.EndUserRepo.SaveUser(user);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public BaseResult UpdateEndUser(EndUserVM user)
        {
            try
            {
                return _unitOfWork.EndUserRepo.UpdateEndUser(user);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public EndUserVM GetEndUserById(int id)
        {
            try
            {

                return _unitOfWork.EndUserRepo.GetEndUserById("usp_GetEndUserById", new { UserId = id });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult DeleteEndUsersByIds(DeleteItemVM targetIds)
        {
            try
            {
                return _unitOfWork.EndUserRepo.DeleteEndUserByIds("usp_DeleteEndUsersByUserIds", targetIds);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
