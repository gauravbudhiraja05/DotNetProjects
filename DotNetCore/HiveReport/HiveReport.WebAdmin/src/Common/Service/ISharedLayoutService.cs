using HiveReport.Dto.Common;
using HiveReport.Entity.Common;

namespace HiveReport.WebAdmin.Common.Service
{
    public interface ISharedLayoutService
    {
        /// <summary>
        /// GetSharedLayoutDetail will get the details for the shared layout details
        /// </summary>
        /// <param name="sharedLayoutSearchCriteria">sharedLayoutSearchCriteria</param>
        /// <returns>SharedLayout object</returns>
        SharedLayoutEntity GetSharedLayoutDetail(SharedLayoutSearchCriteria sharedLayoutSearchCriteria);
    }
}
