using HiveReport.Entity.Common;
using System.Collections.Generic;

namespace HiveReport.WebAdmin.Common.Repository
{
    public interface ISharedLayoutDao
    {

        /// <summary>
        /// GetDepartmentList will get the user departments from Department table
        /// </summary>
        /// <param name="userId">userId</param>
        /// <Department List object</returns>
        DepartmentEntityList GetDepartmentList(string userId);

        /// <summary>
        /// GetClientList will get the user clients on departments basis from Client table
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="departmentIdList">departmentIdList</param>
        /// <Department List object</returns>
        ClientEntityList GetClientList(string userId, List<int> departmentIdList);

        /// <summary>
        /// GetLobList will get the user lob on departments & client basis from Lob table
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="departmentIdList">departmentIdList</param>
        /// <Department List object</returns>
        LOBEntityList GetLobList(string userId, List<int> departmentIdList, List<int> clientIdList);

        /// <summary>
        /// GetLeftMenuList will get the user lob on departments & client basis from Lob table
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="requestVal">requestVal</param>
        /// <param name="userType">userType</param>
        /// <returns>LeftMenuEntity List object</returns>
        LeftMenuEntityList GetLeftMenuList(string userId, string requestVal, string userType);

        /// <summary>
        /// GetTopMenuList will get the top menu from MenuRights, UserMenuRights table
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>TopMenuEntity List object</returns>
        TopMenuEntityList GetTopMenuList(string userId);
    }
}
