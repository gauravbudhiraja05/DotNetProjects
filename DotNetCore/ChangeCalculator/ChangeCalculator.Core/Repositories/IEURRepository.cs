using ChangeCalculator.ViewModels.Calculator;

namespace ChangeCalculator.Core.Repositories
{
    public interface IEURRepository
    {
        Calculator GetEURDenominations(Calculator calculator);
    }
}
