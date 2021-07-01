using HiveReport.Dto.Common;
using HiveReport.Entity.Common;
using HiveReport.WebAdmin.Common.Repository;
using System.Collections.Generic;
using System.Linq;

namespace HiveReport.WebAdmin.Common.Service
{
    public class SharedLayoutService : ISharedLayoutService
    {
        /// <summary>
        /// Private IUserDao Data Member
        /// </summary>
        private readonly ISharedLayoutDao _sharedLayoutDao;

        /// <summary>
        /// SharedLayoutService Constructor
        /// </summary>
        /// <param name="sharedLayoutDao">ISharedLayoutDao object reference</param>
        public SharedLayoutService(ISharedLayoutDao sharedLayoutDao)
        {
            _sharedLayoutDao = sharedLayoutDao;
        }

        public SharedLayoutEntity GetSharedLayoutDetail(SharedLayoutSearchCriteria sharedLayoutSearchCriteria)
        {
            SharedLayoutEntity sharedLayoutEntity = null;

            string userId = sharedLayoutSearchCriteria.UserId;
            string userType = sharedLayoutSearchCriteria.UserType;
            string requestValue = sharedLayoutSearchCriteria.RequestValue;

            TopMenuEntityList topMenuEntityList = _sharedLayoutDao.GetTopMenuList(userId);

            if (sharedLayoutSearchCriteria.IsDashboardMenu)
            {
                DepartmentEntityList departmentEntityList = _sharedLayoutDao.GetDepartmentList(userId);

                List<int> departmentIdList = departmentEntityList.Select(x => x.DepartmentId).ToList();

                ClientEntityList clientEntityList = _sharedLayoutDao.GetClientList(userId, departmentIdList);

                List<int> clientIdList = clientEntityList.Select(x => x.ClientId).ToList();

                LOBEntityList lOBEntityList = _sharedLayoutDao.GetLobList(userId, departmentIdList, clientIdList);

                sharedLayoutEntity = new SharedLayoutEntity
                {
                    UserId = sharedLayoutSearchCriteria.UserId,
                    UserName = sharedLayoutSearchCriteria.UserName,
                    UserType = sharedLayoutSearchCriteria.UserType,
                    UserAdminCheck = sharedLayoutSearchCriteria.UserAdminCheck,
                    ParentNodeName = "Dashboard",
                    IsDashboard = sharedLayoutSearchCriteria.IsDashboardMenu,
                    DepartmentEntityList = departmentEntityList,
                    ClientEntityList = clientEntityList,
                    LOBEntityList = lOBEntityList,
                    TopMenuEntityList = topMenuEntityList,
                    LeftMenuEntityList = new LeftMenuEntityList(),
                };
            }
            else
            {
                sharedLayoutEntity = new SharedLayoutEntity
                {
                    UserId = sharedLayoutSearchCriteria.UserId,
                    UserName = sharedLayoutSearchCriteria.UserName,
                    UserType = sharedLayoutSearchCriteria.UserType,
                    UserAdminCheck = sharedLayoutSearchCriteria.UserAdminCheck,
                    ParentNodeName = string.Empty,
                    IsDashboard = sharedLayoutSearchCriteria.IsDashboardMenu,
                    DepartmentEntityList = new DepartmentEntityList(),
                    ClientEntityList = new ClientEntityList(),
                    LOBEntityList = new LOBEntityList(),
                    TopMenuEntityList = topMenuEntityList,
                    LeftMenuEntityList = _sharedLayoutDao.GetLeftMenuList(userId, requestValue, userType)
                };
            }

            return sharedLayoutEntity;
        }
    }
}
