using ChangeCalculator.ViewModels.Calculator;

namespace ChangeCalculator.Core.Repositories
{
    public interface IGBPRepository
    {
        Calculator GetGBPDenominations(Calculator calculator);
    }
}
