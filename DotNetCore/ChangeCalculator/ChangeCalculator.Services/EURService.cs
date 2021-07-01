using ChangeCalculator.Core.DomainServices;
using ChangeCalculator.Core.Repositories;
using ChangeCalculator.ViewModels.Calculator;
using System;

namespace ChangeCalculator.Services
{
    public class EURService : IEURService
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
        public EURService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Calculator GetEURDenominations(Calculator calculator)
        {
            try
            {
                return _unitOfWork.EURRepo.GetEURDenominations(calculator);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
