using ChangeCalculator.ViewModels.Calculator;

namespace ChangeCalculator.Core.DomainServices
{
    public interface IUSDService
    {
        /// <summary>
        /// It returns the USDDenominations 
        /// </summary>
        /// <param name="calculator">Calculator</param>
        /// <returns>Calculator</returns>
        Calculator GetUSDDenominations(Calculator calculator);
    }
}
