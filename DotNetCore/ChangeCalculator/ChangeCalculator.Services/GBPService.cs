using ChangeCalculator.Core.DomainServices;
using ChangeCalculator.Core.Repositories;
using ChangeCalculator.ViewModels.Calculator;
using System;

namespace ChangeCalculator.Services
{
    public class GBPService : IGBPService
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
        public GBPService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Calculator GetGBPDenominations(Calculator calculator)
        {
            try
            {
                return _unitOfWork.GBPRepo.GetGBPDenominations(calculator);
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
