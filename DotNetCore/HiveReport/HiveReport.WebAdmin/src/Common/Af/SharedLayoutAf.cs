using HiveReport.Dto.Common;
using HiveReport.Entity.Common;
using HiveReport.WebAdmin.Common.Mapping;
using HiveReport.WebAdmin.Common.Service;

namespace HiveReport.WebAdmin.Common.Af
{
    public class SharedLayoutAf : ISharedLayoutAf
    {
        /// <summary>
        /// Private ISharedLayoutService Data Member
        /// </summary>
        private readonly ISharedLayoutService _sharedLayoutService;

        /// <summary>
        /// Private SharedLayoutMapping Data Member
        /// </summary>
        private readonly SharedLayoutMapping _sharedLayoutMapping;

        public SharedLayoutAf(ISharedLayoutService sharedLayoutService)
        {
            _sharedLayoutService = sharedLayoutService;
            _sharedLayoutMapping = new SharedLayoutMapping();
        }

        public SharedLayoutDto GetSharedLayoutDetail(SharedLayoutSearchCriteria sharedLayoutSearchCriteria)
        {
            SharedLayoutEntity sharedLayoutEntity = _sharedLayoutService.GetSharedLayoutDetail(sharedLayoutSearchCriteria);
            SharedLayoutDto sharedLayoutDto = _sharedLayoutMapping.Entity2Dto(sharedLayoutEntity);
            return sharedLayoutDto;
        }
    }
}
