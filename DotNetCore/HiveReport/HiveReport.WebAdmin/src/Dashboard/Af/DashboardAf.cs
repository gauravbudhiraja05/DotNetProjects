using HiveReport.Dto.User;
using HiveReport.Entity.User;
using HiveReport.WebAdmin.Common.Service;
using HiveReport.WebAdmin.Dashboard.Mapping;
using HiveReport.WebAdmin.Dashboard.Service;

namespace HiveReport.WebAdmin.Dashboard.Af
{
    public class DashboardAf : IDashboardAf
    {
        /// <summary>
        /// Private IDashboardService Data Member
        /// </summary>
        private readonly IDashboardService _dashboardService;

        /// <summary>
        /// Private ISharedLayoutService Data Member
        /// </summary>
        private readonly ISharedLayoutService _sharedLayoutService;

        /// <summary>
        /// Private DashboardMapping Data Member
        /// </summary>
        private readonly DashboardMapping _dashboardMapping;

        public DashboardAf(IDashboardService dashboardService, ISharedLayoutService sharedLayoutService)
        {
            _dashboardService = dashboardService;
            _sharedLayoutService = sharedLayoutService;
            _dashboardMapping = new DashboardMapping();
        }

        public UserDetailDto GetDashboardUserDetails(string userId, string userType)
        {
            UserDetailEntity userDetailEntity = _dashboardService.GetDashboardUserDetails(userId);
            UserDetailDto userDetailDto = _dashboardMapping.Entity2Dto(userDetailEntity);
            return userDetailDto;
        }
    }
}
