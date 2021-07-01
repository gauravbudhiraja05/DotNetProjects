using ChangeCalculator.ViewModels.Calculator;

namespace ChangeCalculator.Core.DomainServices
{
    public interface IEURService
    {
        /// <summary>
        /// It returns the USDDenominations 
        /// </summary>
        /// <param name="calculator">Calculator</param>
        /// <returns>Calculator</returns>
        Calculator GetEURDenominations(Calculator calculator);
    }
}
