using ChangeCalculator.ViewModels.Calculator;

namespace ChangeCalculator.Core.Repositories
{
    public interface IUSDRepository
    {
        Calculator GetUSDDenominations(Calculator calculator);
    }
}
