using HiveReport.Entity.Common;
using HiveReport.Entity.User;
using System;
using System.Collections.Generic;

namespace HiveReport.WebAdmin.User.Repository
{
    public interface IUserDao
    {
        /// <summary>
        /// IsEmailExists will check the user detail from registeration table
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>base result object</returns>
        BaseResultEntity IsEmailExists(string email);

        /// <summary>
        /// AddRegisterationDetail will insert the user detail into registeration table
        /// </summary>
        /// <param name="registeredUserEntity">registeredUserEntity</param>
        /// <returns>base result object</returns>
        BaseResultEntity AddRegisterationDetail(RegisteredUserEntity registeredUserEntity);
       
        /// <summary>
        /// AddProductDemoDetail will insert the user detail into productdemo table
        /// </summary>
        /// <param name="registeredUserEntity">registeredUserEntity</param>
        /// <returns>base result object</returns>
        BaseResultEntity AddProductDemoDetail(RegisteredUserEntity registeredUserEntity);

        /// <summary>
        /// AddDemoUserDuration will insert the demo user duration into userduration table
        /// </summary>
        /// <param name="emailAddress">emailAddress</param>
        /// <returns>base result object</returns>
        BaseResultEntity AddDemoUserDuration(string emailAddress, string userId);

        /// <summary>
        /// AddPasswordHistory will insert the user password into PasswordHistory table
        /// </summary>
        /// <param name="emailAddress">emailAddress</param>
        /// <param name="password">password</param>
        /// <returns>base result object</returns>
        BaseResultEntity AddPasswordHistory(string emailAddress, string password, string userid);

        /// <summary>
        /// GetRightsOnProductCodeBasis will get the user rights on product basis from ProductMaster table
        /// </summary>
        /// <param name="productCode">productCode</param>
        /// <returns>string object</returns>
        string GetRightsOnProductCodeBasis(RegisteredUserEntity registeredUserEntity);

        /// <summary>
        /// AddParentMenuRights will add the parent menu rights into  ProductMaster table
        /// </summary>
        /// <param name="rightsList">rightsList</param>
        /// <returns>base result object</returns>
        BaseResultEntity AddParentMenuRights(List<string> rightsList, string emailAddress, string datababase);

        /// <summary>
        /// GetLoggedInUserDetails will verify the user details from Registeration table
        /// </summary>
        /// <param name="rightsList">rightsList</param>
        /// <returns>base result object</returns>
        LoggedInUserEntity GetLoggedInUserDetails(AuthUserEntity authUserEntity);

        /// <summary>
        /// CheckMasterAdmin will check the user is master admin from masteradmin table
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>int object</returns>
        int CheckMasterAdmin(string userId);

        /// <summary>
        /// GetDemoUserEndDate will return the demo user end date from ProductDemo table
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>DateTime object</returns>
        DateTime GetDemoUserEndDate(string userId);

        /// <summary>
        /// CheckUserRegisteration will check the user registeration status from registeration table
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>int object</returns>
        int CheckUserRegisteration(string userID, string password);
        
        /// <summary>
        /// GetProductDemoDetail will get the product details from product demo table
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns>Registered User object</returns>
        RegisteredUserEntity GetProductDemoDetail(string userId);

        /// <summary>
        /// GetSearchedResult will get the user detail from registeration table
        /// </summary>
        /// <param name="dropdownValue">dropdownValue</param>
        /// <param name="txtValue">txtValue</param>
        /// <returns>Logged In User object</returns>
        LoggedInUserEntity GetSearchedResult(string dropdownValue, string txtValue, string userid);

    }
}
