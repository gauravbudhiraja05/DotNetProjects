using HiveReport.Dto.User;
using HiveReport.Entity.Common;
using HiveReport.Entity.User;
using HiveReport.WebAdmin.Account.Mapping;
using HiveReport.WebAdmin.Account.Service;
using HiveReport.WebAdmin.User.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HiveReport.WebAdmin.Account.Af
{
    public class AccountAf : IAccountAf
    {
        // <summary>
        /// Private IAccountService Data Member
        /// </summary>
        private readonly IAccountService _accountService;

        // <summary>
        /// Private AccountMapping Data Member
        /// </summary>
        private readonly AccountMapping _accountMapping;

        // <summary>
        /// Private UserMapping Data Member
        /// </summary>
        private readonly UserMapping _userMapping;


        public AccountAf(IAccountService accountService)
        {
            _accountService = accountService;
            _accountMapping = new AccountMapping();
            _userMapping = new UserMapping();
        }

        public List<string> GetDesignationList()
        {
            return _accountService.GetDesignationList();
        }

        public Dictionary<int, string> GetDepartmentList(string savedBy)
        {
            return _accountService.GetDepartmentList(savedBy);
        }

        public Dictionary<int, string> GetClientList(int departmentId)
        {
            return _accountService.GetClientList(departmentId);
        }

        public Dictionary<int, string> GetLOBList(int departmentId, int clientId)
        {
            return _accountService.GetLOBList(departmentId, clientId);
        }

        public bool CheckAvailableEmployeeId(int employeeId)
        {
            return _accountService.CheckAvailableEmployeeId(employeeId);
        }
        public bool CheckAvailableUserId(string emailAddress)
        {
            return _accountService.CheckAvailableUserId(emailAddress);
        }

        public string AddUserDetails(RegisteredUserDto registeredUser, string userType, string userid)
        {
            string message = string.Empty;

            if (registeredUser.IsNewDesignation)
            {
                _accountService.AddNewDesignation(registeredUser.Designation);
            }

            if (userType == "Admin")
            {
                int count = _accountService.CheckMasterAdmin(userid);
                if (count > 0)
                {
                    registeredUser.UserType = 1;
                    if (userid == "idmsadmin")
                        registeredUser.CreatorId = 3;
                    else
                        registeredUser.CreatorId = 2;

                    registeredUser.CreatedBy = userid;
                    RegisteredUserEntity registeredUserEntity = _userMapping.RegisteredUserDto2RegisteredUserEntity(registeredUser);
                    BaseResultEntity baseResultEntity = _accountService.AddRegisterationDetail(registeredUserEntity);
                    if (baseResultEntity.IsSuccess)
                    {
                        _accountService.AddAccountUTypeDetails(userType, registeredUser.UserId);
                        _accountService.AddWarsCountLogin(registeredUser.UserId);
                        _accountService.AddBuddyDetails(registeredUserEntity);
                        _accountService.AddUserDuration(registeredUser.UserId, userid);
                        _accountService.AddPasswordHistory(registeredUser.EmailAddress, registeredUser.Password, userid);
                        message = "Registration has been completed successfully!!!";
                    }
                    else
                        message = baseResultEntity.Message;
                }
                else
                {
                    message = "You are not admin of this span!!!";
                }
            }
            else if (userType == "Super Admin")
            {
                registeredUser.UserType = 1;
                registeredUser.BU = "BFI";
                if (userid == "idmsadmin")
                    registeredUser.CreatorId = 3;
                else
                    registeredUser.CreatorId = 2;

                registeredUser.CreatedBy = userid;
                RegisteredUserEntity registeredUserEntity = _userMapping.RegisteredUserDto2RegisteredUserEntity(registeredUser);
                BaseResultEntity baseResultEntity = _accountService.AddRegisterationDetail(registeredUserEntity);
                if (baseResultEntity.IsSuccess)
                {
                    _accountService.AddAccountUTypeDetails(userType, registeredUser.UserId);
                    _accountService.AddWarsCountLogin(registeredUser.UserId);
                    _accountService.AddBuddyDetails(registeredUserEntity);
                    _accountService.AddUserDuration(registeredUser.UserId, userid);
                    _accountService.AddPasswordHistory(registeredUser.EmailAddress, registeredUser.Password, userid);

                    registeredUserEntity = _accountService.GetProductDemoDetail(userid);
                    registeredUserEntity.EmailAddress = registeredUser.EmailAddress;
                    _accountService.AddProductDemoDetail(registeredUserEntity);

                    string rights = _accountService.GetRightsOnProductCodeBasis(registeredUserEntity);
                    List<string> rightsList = rights.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    _accountService.AddParentMenuRights(rightsList, registeredUserEntity.EmailAddress, registeredUserEntity.Database);

                    message = "Registration has been completed successfully!!!";
                }
                else
                    message = baseResultEntity.Message;
            }
            else if (userType == "User")
            {
                message = "User is not allowed to register new employee!!!";
            }

            return message;
        }

        public LoggedInUserDto GetSearchedResult(string dropdownValue, string txtValue, string userid)
        {
            LoggedInUserEntity loggedInUserEntity = _accountService.GetSearchedResult(dropdownValue, txtValue, userid);
            LoggedInUserDto loggedInUserDto = _userMapping.LoggedInUserEntity2LoggedInUserDto(loggedInUserEntity);
            return loggedInUserDto;
        }
    }
}
