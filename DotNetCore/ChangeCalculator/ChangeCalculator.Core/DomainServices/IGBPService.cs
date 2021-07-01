using ChangeCalculator.ViewModels.Calculator;

namespace ChangeCalculator.Core.DomainServices
{
    public interface IGBPService
    {
        /// <summary>
        /// It returns the GBPDenominations 
        /// </summary>
        /// <param name="calculator">Calculator</param>
        /// <returns>Calculator</returns>
        Calculator GetGBPDenominations(Calculator calculator);
    }
}
