using HiveReport.Entity.Common;
using HiveReport.Entity.User;
using HiveReport.WebAdmin.Account.Repository;
using HiveReport.WebAdmin.User.Repository;
using System.Collections.Generic;

namespace HiveReport.WebAdmin.Account.Service
{
    public class AccountService : IAccountService
    {
        /// <summary>
        /// Private IAccountDao Data Member
        /// </summary>
        private readonly IAccountDao _accountDao;

        /// <summary>
        /// Private IUserDao Data Member
        /// </summary>
        private readonly IUserDao _userDao;

        /// <summary>
        /// AccountService Constructor
        /// </summary>
        /// <param name="accountDao">IAccountDao object reference</param>
        /// <param name="userDao">IUserDao object reference</param>
        public AccountService(IAccountDao accountDao, IUserDao userDao)
        {
            _accountDao = accountDao;
            _userDao = userDao;
        }

        public Dictionary<int, string> GetDesignationList()
        {
            return _accountDao.GetDesignationList();
        }
        public Dictionary<int, string> GetDepartmentList(string savedBy)
        {
            return _accountDao.GetDepartmentList(savedBy);
        }
        public Dictionary<int, string> GetClientList(int departmentId)
        {
            return _accountDao.GetClientList(departmentId);
        }
        public Dictionary<int, string> GetLOBList(int departmentId, int clientId)
        {
            return _accountDao.GetLOBList(departmentId, clientId);
        }

        public bool CheckAvailableEmployeeId(int employeeId)
        {
            return _accountDao.CheckAvailableEmployeeId(employeeId);
        }

        public bool CheckAvailableUserId(string emailAddress)
        {
            return _accountDao.CheckAvailableUserId(emailAddress);
        }

        public bool AddNewDesignation(string designation)
        {
            return _accountDao.AddNewDesignation(designation);
        }

        public int CheckMasterAdmin(string designation)
        {
            return _userDao.CheckMasterAdmin(designation);
        }

        public BaseResultEntity AddRegisterationDetail(RegisteredUserEntity registeredUserEntity)
        {
            return _userDao.AddRegisterationDetail(registeredUserEntity);
        }

        public void AddAccountUTypeDetails(string userType, string userId)
        {
            _accountDao.AddAccountUTypeDetails(userType, userId);
        }

        public void AddWarsCountLogin(string userId)
        {
            _accountDao.AddWarsCountLogin(userId);
        }

        public void AddBuddyDetails(RegisteredUserEntity registeredUser)
        {
            _accountDao.AddBuddyDetails(registeredUser);
        }

        public void AddUserDuration(string userId, string userid)
        {
            _userDao.AddDemoUserDuration(userId, userid);
        }

        public void AddPasswordHistory(string emailAddress, string password, string userid)
        {
            _userDao.AddPasswordHistory(emailAddress, password, userid);
        }

        public RegisteredUserEntity GetProductDemoDetail(string userid)
        {
            return _userDao.GetProductDemoDetail(userid);
        }

        public BaseResultEntity AddProductDemoDetail(RegisteredUserEntity registeredUserEntity)
        {
            return _userDao.AddProductDemoDetail(registeredUserEntity);
        }

        public string GetRightsOnProductCodeBasis(RegisteredUserEntity registeredUserEntity)
        {
            return _userDao.GetRightsOnProductCodeBasis(registeredUserEntity);
        }

        public BaseResultEntity AddParentMenuRights(List<string> rightsList, string emailAddress, string database)
        {
            return _userDao.AddParentMenuRights(rightsList, emailAddress, database);
        }

        public LoggedInUserEntity GetSearchedResult(string dropdownValue, string txtValue, string userid)
        {
            return _userDao.GetSearchedResult(dropdownValue, txtValue, userid);
        }
    }
}
