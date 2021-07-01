using System.Collections.Generic;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.SuperAdmin;

namespace PickfordsIntranet.Core.Repositories
{
    /// <summary>
    /// IAdminRepository describes the implementation required for only SuperAdmin data/object access
    /// </summary>
    public interface IAdminRepository
    {
        /// <summary>
        /// Get all admin users from target data source
        /// </summary>
        /// <returns>List of AdminUserGridItemVM Entity</returns>
        IEnumerable<AdminUserGridItemVM> GetAllAdminUsers(string query);

        /// <summary>
        /// Get Pre-Requisites data to create Admin user
        /// </summary>
        /// <returns>AdminUserVM Model</returns>
        AdminUserVM GetPreRequisitesDataToCreateAdminUser(string query);


        bool CheckValidOldPassword(string query, object param);

        BaseResult ChangePasswordForLoginUser(string query, object param);

        /// <summary>
        /// Save user
        /// </summary>
        /// <param name="user">AdminUserVM data structure</param>
        /// <returns>true/false</returns>
        BaseResult SaveUser(AdminUserVM user);

        /// <summary>
        /// Delete Admin users by Ids
        /// </summary>
        /// <param name="query">Stored procedure name</param>
        /// <param name="deleteItems">all user ids </param>
        /// <returns>BaseResult</returns>
        BaseResult DeleteAdminUserByIds(string query, DeleteItemVM deleteItems);

        /// <summary>
        ///  Get admin user by user id
        /// </summary>
        /// <param name="query">Stored procedure name that return Admin User by uUser Id</param>
        /// <param name="param">UserId</param>
        /// <returns>Admin User Info as AdminUserVM</returns>
        AdminUserVM GetAdminUserById(string query, object param);

        /// <summary>
        /// Check EmailId already exist or not
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="userId"></param>
        /// <returns>true/false</returns>
        bool IsEmailExist(string emailAddress, int userId);

        /// <summary>
        /// Update Admin user
        /// </summary>
        /// <param name="user">Admin User as AdminUserVM data structure</param>
        /// <returns>result as Baseresult</returns>
        BaseResult UpdateUser(AdminUserVM user);

        /// <summary>
        /// Get all Featured messages list
        /// </summary>
        /// <param name="query">Stored Procedure name</param>
        /// <returns></returns>
        IEnumerable<FeaturedMessageGridItemVM> GetAllFeaturedMessages(string query);

        /// <summary>
        /// Save Featured Message
        /// </summary>
        /// <param name="spName">Stored Procedure name</param>
        /// <param name="message">Featured Message data as FeaturedMessageVM</param>
        /// <returns></returns>
        BaseResult SaveFeaturedMessage(string spName, FeaturedMessageVM message);

        /// <summary>
        ///  Get featured message by id
        /// </summary>
        /// <param name="query">Stored procedure name that return fetured message by  Id</param>
        /// <param name="param">FeaturedMessageId</param>
        /// <returns>Featured Message Info as FeaturedMessageVM</returns>
        FeaturedMessageVM GetFeaturedMessageById(string query, int param);

        /// <summary>
        /// Update Featured message
        /// </summary>
        /// <param name="feturedMessage">FeaturedMessageVM data structure</param>
        /// <returns>true/false as BaseResult</returns>
        BaseResult UpdateFeaturedMessage(FeaturedMessageVM feturedMessage);

        /// <summary>
        /// Delete featured messages by ids
        /// </summary>
        /// <param name="query">Stored procedure name</param>
        /// <param name="deleteItems">all featured message ids </param>
        /// <returns>BaseResult</returns>
        BaseResult DeleteFeaturedMessageByIds(string v, DeleteItemVM targetIds);

        /// <summary>
        /// Get OurValues 
        /// </summary>
        /// <param name="v"></param>
        /// <returns>OurValues as OurValuesVM</returns>
        OurValuesVM GetOurValues(string query);

        /// <summary>
        /// Update Ourvalues information
        /// </summary>
        /// <param name="ourValues"></param>
        /// <returns></returns>
        BaseResult UpdateOurValues(OurValuesVM ourValues);

        bool ValidateNewMonthName(string query, object param);

        bool ValidateMonthYear(string query, object param);

        /// <summary>
        /// Add month Stars Information
        /// </summary>
        /// <param name="monthStars">MonthStarsVM object</param>
        /// <returns>BaseResult</returns>
        BaseResult AddMonthStars(MonthStarVM monthStars);

        /// <summary>
        /// Get Month Stars detail by Id
        /// </summary>
        /// <param name="query">Stored procedure name</param>
        /// <param name="id">MonthStarId</param>
        /// <returns></returns>
        MonthStarVM GetMonthStarsById(string query, int id);

        /// <summary>
        /// Delete MonthStars by Ids
        /// </summary>
        /// <param name="query"></param>
        /// <param name="targetIds"></param>
        /// <returns>BaseResult</returns>
        BaseResult DeleteMonthStarsByIds(string query, DeleteItemVM targetIds);

        /// <summary>
        /// Get All Month Stars
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<MonthStarGridItemVM> GetAllMonthStars(string query);
        /// <summary>
        /// Get all Gazetteers
        /// </summary>
        /// <param name="query">SP Name</param>
        /// <returns>List of Gazetteers</returns>
        IEnumerable<GazetteersGridItemVM> GetAllGazetteers(string query);

        /// <summary>
        /// Get User Password by User Name
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        string GetPasswordByUserName(string emailAddress);

        /// <summary>
        /// Add Gazetteers
        /// </summary>
        /// <param name="gazetteers">Gazetteers data </param>
        /// <returns>BaseResult</returns>
        BaseResult AddGazetteers(GazetteersVM gazetteers);

        /// <summary>
        /// Update month stars
        /// </summary>
        /// <param name="monthStars"></param>
        /// <returns></returns>
        BaseResult UpdateMonthStars(MonthStarVM monthStars);
        BaseResult DeleteGazetteersByIds(string v, DeleteItemVM targetIds);
        bool IsEmailExistInEndUser(string emailAddress, int v);
    }
}
