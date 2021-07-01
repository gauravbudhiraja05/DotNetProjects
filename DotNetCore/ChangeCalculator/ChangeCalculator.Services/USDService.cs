using ChangeCalculator.Core.DomainServices;
using ChangeCalculator.Core.Repositories;
using ChangeCalculator.ViewModels.Calculator;
using System;

namespace ChangeCalculator.Services
{
    public class USDService : IUSDService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        //private IUnitOfWork _unitOfWork;
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// AuthService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork object reference</param>
        public USDService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Calculator GetUSDDenominations(Calculator calculator)
        {
            try
            {
                return _unitOfWork.USDRepo.GetUSDDenominations(calculator);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
