using HiveReport.Entity.User;
using HiveReport.WebAdmin.Dashboard.Repository;
using HiveReport.WebAdmin.User.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HiveReport.WebAdmin.Dashboard.Service
{
    public class DashboardService : IDashboardService
    {
        /// <summary>
        /// Private IDashboardDao Data Member
        /// </summary>
        private readonly IDashboardDao _dashboardDao;

        /// <summary>
        /// Private IUserDao Data Member
        /// </summary>
        private readonly IUserDao _userDao;

        /// <summary>
        /// UserService Constructor
        /// </summary>
        /// <param name="dashboardDao">IDashboardDao object reference</param>
        /// <param name="userDao">IUserDao object reference</param>
        public DashboardService(IDashboardDao dashboardDao, IUserDao userDao)
        {
            _dashboardDao = dashboardDao;
            _userDao = userDao;
        }

        public UserDetailEntity GetDashboardUserDetails(string userId)
        {
            UserDetailEntity userDetailEntity = new UserDetailEntity();
            string productCode = _dashboardDao.GetDemoUserProductCode(userId);
            string rights = _dashboardDao.GetDemoUserRights(productCode);
            List<string> rightsList = rights.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            string code = "103";

            if (rightsList.Contains(code))
            {
                userDetailEntity.HasQuery = true;
                userDetailEntity.HasReport = false;
            }
            else
            {
                userDetailEntity.HasQuery = false;
                userDetailEntity.HasReport = true;
            }

            userDetailEntity.RightsName = rights;

            userDetailEntity.ReportMasterEntityList = _dashboardDao.GetReportMasterList(userId);
            userDetailEntity.TableMasterEntityList = _dashboardDao.GetTableMasterList(userId);
            userDetailEntity.ViewMasterEntityList = _dashboardDao.GetViewMasterList(userId);
            userDetailEntity.HTMLFileEntityList = _dashboardDao.GetHtmlFileMasterList(userId);
            userDetailEntity.GraphMasterEntityList = _dashboardDao.GetGraphMasterList(userId);
            userDetailEntity.QueryMasterEntityList = _dashboardDao.GetQueryMasterList(userId);

            DateTime endDate = _userDao.GetDemoUserEndDate(userId);

            int days = Convert.ToInt32((endDate - DateTime.Now).TotalDays);

            userDetailEntity.PasswordExpirationMessage = days <= 10 && days > 0
                ? days != 1 ? "Your Password Will Expire in " + days + " days" : "Your Password Will Expire in " + days + " day"
                : string.Empty;

            return userDetailEntity;
        }
    }
}
