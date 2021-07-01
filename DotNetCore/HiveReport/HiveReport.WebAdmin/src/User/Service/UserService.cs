using HiveReport.Entity.Common;
using HiveReport.Entity.User;
using HiveReport.WebAdmin.User.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HiveReport.WebAdmin.User.Service
{
    public class UserService : IUserService
    {
        /// <summary>
        /// Private IUserDao Data Member
        /// </summary>
        private readonly IUserDao _userDao;

        /// <summary>
        /// UserService Constructor
        /// </summary>
        /// <param name="userDao">IUserDao object reference</param>
        public UserService(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public BaseResultEntity IsEmailExists(string email)
        {
            return _userDao.IsEmailExists(email);
        }

        public BaseResultEntity AddUserInformation(RegisteredUserEntity registeredUserEntity)
        {
            BaseResultEntity baseResultEntity;

            try
            {
                string password = registeredUserEntity.EmailAddress.Substring(0, 3) + "@1234";
                //password = Crypto.Encrypt(password);

                DateTime dateTime = DateTime.Now;

                baseResultEntity = _userDao.AddRegisterationDetail(registeredUserEntity);

                baseResultEntity = _userDao.AddProductDemoDetail(registeredUserEntity);

                baseResultEntity = _userDao.AddDemoUserDuration(registeredUserEntity.EmailAddress, string.Empty);

                baseResultEntity = _userDao.AddPasswordHistory(registeredUserEntity.EmailAddress, password, string.Empty);

                string rights = _userDao.GetRightsOnProductCodeBasis(registeredUserEntity);

                List<string> rightsList = rights.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                baseResultEntity = _userDao.AddParentMenuRights(rightsList, registeredUserEntity.EmailAddress, registeredUserEntity.Database);

            }
            catch (Exception ex)
            {
                baseResultEntity = new BaseResultEntity
                {
                    IsSuccess = false,
                    Message = ex.Message
                };

            }
            return baseResultEntity;
        }

        public LoggedInUserEntity UserAuthenticate(AuthUserEntity authUserEntity)
        {
            LoggedInUserEntity loggedInUserEntity = _userDao.GetLoggedInUserDetails(authUserEntity);
            
            int count = _userDao.CheckMasterAdmin(loggedInUserEntity.UserID);

            if (count > 0)
                loggedInUserEntity.UserAdminCheck = "Yes";
            else
                loggedInUserEntity.UserAdminCheck = "No";

            DateTime endDate = _userDao.GetDemoUserEndDate(loggedInUserEntity.UserID);

            int days = Convert.ToInt32((endDate - DateTime.Now).TotalDays);

            if (days < 0)
            {
                loggedInUserEntity.Message = "Your Password has been Expired";
                loggedInUserEntity.IsSuccess = false;
            }
            else
            {
                int records = _userDao.CheckUserRegisteration(loggedInUserEntity.UserID, loggedInUserEntity.Password);
                if (records > 0)
                {
                    loggedInUserEntity.RegisterationStatus = "Success";
                }
            }

            return loggedInUserEntity;
        }

    }
}
