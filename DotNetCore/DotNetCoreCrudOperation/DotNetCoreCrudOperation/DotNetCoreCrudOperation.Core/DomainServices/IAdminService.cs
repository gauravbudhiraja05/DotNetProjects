using System;
using System.Collections.Generic;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.Rewards;
using PickfordsIntranet.ViewModels.SuperAdmin;

namespace PickfordsIntranet.Core.DomainServices
{
    /// <summary>
    /// The IAdminService Inteface describes the implementation required for Super Admin and Departmental Admin. 
    /// </summary>
    public interface IAdminService
    {
        /// <summary>
        /// GetAllAdminUsers will return all users except Super Admin
        /// </summary>
        /// <returns></returns>
         IEnumerable<AdminUserGridItemVM> GetAllAdminUsers();

        /// <summary>
        /// Get AdminUser view model object with prerequisites populated data to create admin user
        /// </summary>
        /// <returns>AdminUserVM view model</returns>
        AdminUserVM GetAdminUserDataToCreateAdminUser();


        bool CheckValidOldPassword(string password, int userId);


        BaseResult ChangePasswordForLoginUser(string newPassword, int userId);

        /// <summary>
        /// ValidatNewMonthName validate month name for MonthStar
        /// </summary>
        /// <returns>bool</returns>
        bool ValidateNewMonthName(string NewMonthName, int id);

        /// <summary>
        /// ValidateMonthYear validate month&year for MonthStar
        /// </summary>
        /// <returns>bool</returns>
        bool ValidateMonthYear(string MonthYear, int id);

        /// <summary>
        /// Save Admin User
        /// </summary>
        /// <param name="user">AdminUserVM data structure</param>
        /// <returns>true/false</returns>
        BaseResult SaveAdminUser(AdminUserVM user);

        /// <summary>
        /// Check Email Address already exist or not
        /// </summary>
        /// <param name="emailAddress">Email Address</param>
        /// <param name="id">User Id</param>
        /// <returns>true/false</returns>
        bool IsEmailExist(string emailAddress, string id);

        /// <summary>
        /// Get admin user by user Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>Info of Admin User using AdminuserVM</returns>
        AdminUserVM GetAdminUserById(int id);

        /// <summary>
        /// Update Admin User
        /// </summary>
        /// <param name="user">AdminUserVM data structure</param>
        /// <returns>true/false</returns>
        BaseResult UpdateAdminUser(AdminUserVM user);

        /// <summary>
        /// Delete  admin users by ids
        /// </summary>
        /// <param name="allUserIds">list of user Ids</param>
        /// <returns>Result as Baseresult</returns>
        BaseResult DeleteAdmunUsersByIds(DeleteItemVM targetIds);

        /// <summary>
        /// Get All Featured Messages
        /// </summary>
        /// <returns>List of Featured Messages</returns>
        IEnumerable<FeaturedMessageGridItemVM> GetAllFeaturedMessages();

        /// <summary>
        /// Save Featured message
        /// </summary>
        /// <param name="message">Feature message data structure as FeaturedMesageVM</param>
        /// <returns>Result as BaseResult</returns>
        BaseResult SaveFeaturedMessage(FeaturedMessageVM message);

        /// <summary>
        /// Get featured message  by id
        /// </summary>
        /// <param name="id">featured message Id</param>
        FeaturedMessageVM GetFeaturedMessageById(int id);

        /// <summary>
        /// Update Featured  Message
        /// </summary>
        /// <param name="user">FeaturedMEssageVM data structure</param>
        /// <returns>true/false</returns>
        BaseResult UpdateFeaturedMessage(FeaturedMessageVM feturedMessage);

        /// <summary>
        ///  Delete featured messages by ids
        /// </summary>
        /// <param name="targetIds"></param>
        /// <returns>Result as Baseresult</returns>
        BaseResult DeleteFeaturedMessageByIds(DeleteItemVM targetIds);

        /// <summary>
        /// Get our values information
        /// </summary>
        /// <returns>OurValues info as OurValuesVM</returns>
        OurValuesVM GetOurValues();

        /// <summary>
        /// Update OurValues information
        /// </summary>
        /// <param name="ourValues"></param>
        /// <returns>BaseResult object</returns>
        BaseResult UpdateOurValues(OurValuesVM ourValues);

        /// <summary>
        /// Add Month Stars information
        /// </summary>
        /// <param name="monthStars"></param>
        /// <returns></returns>
        BaseResult AddMonthStars(MonthStarVM monthStars);

        /// <summary>
        /// Get MonthStar details to edit the record
        /// </summary>
        /// <param name="id">MonthStarsId</param>
        /// <returns>Month Stars details as MonthStarVM</returns>
        MonthStarVM GetMonthStarsById(int id);

        /// <summary>
        /// Delete Month Stars by passed paramete targetIds
        /// </summary>
        /// <param name="targetIds">Month Star Ids </param>
        /// <returns>BaseResult</returns>
        BaseResult DeleteMonthStarsByIds(DeleteItemVM targetIds);

        /// <summary>
        /// Get All Month Stars
        /// </summary>
        /// <returns>List of month star</returns>
        IEnumerable<MonthStarGridItemVM> GetAllmonthStars();

        /// <summary>
        /// Get All Gazetteers
        /// </summary>
        /// <returns>List of Gazetteers</returns>
        IEnumerable<GazetteersGridItemVM> GetAllGazetteers();

        /// <summary>
        /// Add Gazetteers
        /// </summary>
        /// <param name="gazetteers">Gazetters entity and data</param>
        /// <returns>BaseResult</returns>
        BaseResult AddGazetteers(GazetteersVM gazetteers);

        /// <summary>
        /// Update Month Stars
        /// </summary>
        /// <param name="monthStars"></param>
        /// <returns></returns>
        BaseResult UpdateMonthStars(MonthStarVM monthStars);
        /// <summary>
        /// Get Password of User by UserName
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        string GetPasswordByUserName(string emailAddress);
        /// <summary>
        /// Delete Gazatiers using Ids
        /// </summary>
        /// <param name="targetIds"></param>
        /// <returns></returns>
        BaseResult DeleteGazetteersByIds(DeleteItemVM targetIds);

        /// <summary>
        /// Get Value Icon by Value name: like Communication
        /// </summary>
        /// <param name="valueName"></param>
        /// <returns></returns>
        string GetValueIconByValueName(string valueName);

        /// <summary>
        /// Get Award By Id
        /// </summary>
        /// <param name="awardId"></param>
        /// <returns></returns>
        AwardTypeInfo GetAwardById(int awardId);

        /// <summary>
        /// get Reward Recipient details by recipient id
        /// </summary>
        /// <param name="recipientId"></param>
        /// <returns></returns>
        RewardRecipientInfoVM GetRewardRecipientInfoById(int recipientId);

        /// <summary>
        /// Insert Award Recipient mail html to view the Recipientawrd mail using browser
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="mailContent"></param>
        BaseResult InsertAwardRecipientMailContent(Guid Id, string mailContent);
    }
}
