using HiveReport.Dto.Common;

namespace HiveReport.WebAdmin.Common.Af
{
    public interface ISharedLayoutAf
    {
        public SharedLayoutDto GetSharedLayoutDetail(SharedLayoutSearchCriteria sharedLayoutSearchCriteria);
    }
}
