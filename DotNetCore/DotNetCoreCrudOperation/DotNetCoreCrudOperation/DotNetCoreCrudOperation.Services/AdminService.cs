using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using PickfordsIntranet.Core.DomainServices;
using PickfordsIntranet.Core.Repositories;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.Rewards;
using PickfordsIntranet.ViewModels.SuperAdmin;

namespace PickfordsIntranet.Services
{
    /// <summary>
    /// AdminService implemented IAdminService
    /// </summary>
    public class AdminService : IAdminService
    {
        /// <summary>
        /// Private IUnitOfWork Data Member
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// IConfigurationRoot Data Member
        /// </summary>
        private IConfigurationRoot _config;

        /// <summary>
        /// AdminService Constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        public AdminService(IUnitOfWork unitOfWork, IConfigurationRoot config)
        {
            try
            {
                _unitOfWork = unitOfWork;
                _config = config;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckValidOldPassword(string password, Int32 userId)
        {
            try
            {
                bool result = _unitOfWork.AdminRepo.CheckValidOldPassword("usp_CheckValidOldPassword", new { password = password, userId=userId });
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult ChangePasswordForLoginUser(string newPassword, int userId)
        {
            try
            {
                BaseResult result = _unitOfWork.AdminRepo.ChangePasswordForLoginUser("usp_ChangePasswordForLoginUser", new { newPassword = newPassword, userId = userId });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Admin Users
        public BaseResult DeleteAdmunUsersByIds(DeleteItemVM targetIds)
        {
            try
            {
                BaseResult result = _unitOfWork.AdminRepo.DeleteAdminUserByIds("usp_DeleteAdminUsersByUserIds", targetIds);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public AdminUserVM GetAdminUserById(int id)
        {
            try
            {

                var adminUser = _unitOfWork.AdminRepo.GetAdminUserById("usp_GetAdminUserById", new { UserId = id });

                adminUser.AllDepartments.Insert(0, new ViewModels.Auth.DepartmentVM() { DepartmentId = 0, DepartmentName = "All" });

                // Select default user status
                adminUser.AllStatus = new List<StatusVM>()
                {
                    new StatusVM(){ StatusId=1, StatusName="Live"},
                    new StatusVM(){ StatusId=0, StatusName="Suspended"}
                };
                adminUser.StatusId = adminUser.IsActive == true ? 1 : 0;

                // select LineManagerCan Option
                if(string.IsNullOrEmpty(adminUser.LineManagerCanSelectOption))
                {
                    adminUser.LineManagerCanSelectOption = "RecogniseOnly";
                }
                adminUser.LineManagerCanOptionList = new List<LineManagerCanVM>();
                adminUser.LineManagerCanOptionList.Add(new LineManagerCanVM() { LineManagerCanText = _config["ServerEnd:SuperAdmin:LineManagerCanTextRecogniseOnly"], LineManagerCanValue = _config["ServerEnd:SuperAdmin:LineManagerCanValueRecogniseOnly"] });
                adminUser.LineManagerCanOptionList.Add(new LineManagerCanVM() { LineManagerCanText = _config["ServerEnd:SuperAdmin:LineManagerCanTextRecogniseAndReward"], LineManagerCanValue = _config["ServerEnd:SuperAdmin:LineManagerCanValueRecogniseAndReward"] });


                return adminUser;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public AdminUserVM GetAdminUserDataToCreateAdminUser()
        {
            try
            {
                AdminUserVM adminUserModel = _unitOfWork.AdminRepo.GetPreRequisitesDataToCreateAdminUser("usp_GetPreRequisitesDataToCreateAdminUser");

                adminUserModel.AllDepartments.Insert(0, new ViewModels.Auth.DepartmentVM() { DepartmentId=0,DepartmentName="Please Select"});
                // Select default role type
                adminUserModel.RoleId = adminUserModel.AllRoles.SingleOrDefault(r => r.RoleName.Equals("Departmental Administrator")).RoleId;

                // Select default user status
                adminUserModel.AllStatus = new List<StatusVM>()
                {
                    new StatusVM(){ StatusId=1, StatusName="Live"},
                    new StatusVM(){ StatusId=0, StatusName="Suspended"}
                };
                adminUserModel.StatusId = 1;

                // Select Line Manager Can default option Recognise only
                adminUserModel.LineManagerCanOptionList = new List<LineManagerCanVM>();
                adminUserModel.LineManagerCanOptionList.Add(new LineManagerCanVM() { LineManagerCanText = _config["ServerEnd:SuperAdmin:LineManagerCanTextRecogniseOnly"], LineManagerCanValue = _config["ServerEnd:SuperAdmin:LineManagerCanValueRecogniseOnly"] });
                adminUserModel.LineManagerCanOptionList.Add(new LineManagerCanVM() { LineManagerCanText = _config["ServerEnd:SuperAdmin:LineManagerCanTextRecogniseAndReward"], LineManagerCanValue = _config["ServerEnd:SuperAdmin:LineManagerCanValueRecogniseAndReward"] });
                adminUserModel.LineManagerCanSelectOption = _config["ServerEnd:SuperAdmin:LineManagerCanValueRecogniseOnly"];

                return adminUserModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<AdminUserGridItemVM> GetAllAdminUsers()
        {
            try
            {
                var result = _unitOfWork.AdminRepo.GetAllAdminUsers("usp_GetAllAdminUsers");
                //var result= _unitOfWork.Repo.ExecuteQuery<AdminUserGridItemVM>("usp_GetAllAdminUsers", SqlCommandType.StoredProcedure);
                
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }



        public bool IsEmailExist(string emailAddress, string userId)
        {
            try
            {
                userId = userId == "undefined" ? "0" : userId;
                var result = _unitOfWork.AdminRepo.IsEmailExist(emailAddress, Convert.ToInt32(userId));
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult SaveAdminUser(AdminUserVM user)
        {
            try
            {

                // Create Random Password 
                //user.UserPwd = this.GeneratePassword(8);

                // Set IsSuperuser Flag
                // user.IsSuperUser = user.RoleId == 1 ? true : false;
                user.IsSuperUser = user.AllSelectedRoleTypes.Split("|").Contains("1") ? true : false;

                // Set IsDepartmentalUser Flag
                user.IsDepartmentalUser = user.AllSelectedRoleTypes.Split("|").Contains("2") ? true : false;

                // Set IsDepartmentalUser Flag
                user.IsLineManagerUser = user.AllSelectedRoleTypes.Split("|").Contains("3") ? true : false;

                // Set IsActive 
                user.IsActive = user.StatusId == 1 ? true : false;

                // Set LineManager Can Option properly
                if(!user.IsLineManagerUser)
                {
                    user.LineManagerCanSelectOption = string.Empty;
                }

                return _unitOfWork.AdminRepo.SaveUser(user);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public BaseResult UpdateAdminUser(AdminUserVM user)
        {
            try
            {
                // user.IsSuperUser = user.RoleId == 1 ? true : false;
                user.IsSuperUser = user.AllSelectedRoleTypes.Split("|").Contains("1") ? true : false;

                // Set IsDepartmentalUser Flag
                user.IsDepartmentalUser = user.AllSelectedRoleTypes.Split("|").Contains("2") ? true : false;

                // Set IsDepartmentalUser Flag
                user.IsLineManagerUser = user.AllSelectedRoleTypes.Split("|").Contains("3") ? true : false;

                // Set IsActive 
                user.IsActive = user.StatusId == 1 ? true : false;

                // Set LineManager Can Option properly
                if (!user.IsLineManagerUser)
                {
                    user.LineManagerCanSelectOption = string.Empty;
                }

                return _unitOfWork.AdminRepo.UpdateUser(user);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string GeneratePassword(int passwordLength)
        {
            string _allowedChars = "0123456789abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ";
            Random randNum = new Random();
            char[] chars = new char[passwordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
        public string GetPasswordByUserName(string emailAddress)
        {
            try
            {
                return _unitOfWork.AdminRepo.GetPasswordByUserName(emailAddress);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region Featured Messages
        public IEnumerable<FeaturedMessageGridItemVM> GetAllFeaturedMessages()
        {
            try
            {
                return _unitOfWork.AdminRepo.GetAllFeaturedMessages("usp_GetAllFeaturedMessages");

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult SaveFeaturedMessage(FeaturedMessageVM message)
        {
            try
            {
                return _unitOfWork.AdminRepo.SaveFeaturedMessage("usp_AddFeaturedMessage", message);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public FeaturedMessageVM GetFeaturedMessageById(int id)
        {
            try
            {

                return _unitOfWork.AdminRepo.GetFeaturedMessageById("usp_GetFeaturedMessageById", id);

            }


            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult UpdateFeaturedMessage(FeaturedMessageVM feturedMessage)
        {
            try
            {
                return _unitOfWork.AdminRepo.UpdateFeaturedMessage(feturedMessage);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public BaseResult DeleteFeaturedMessageByIds(DeleteItemVM targetIds)
        {
            try
            {
                BaseResult result = _unitOfWork.AdminRepo.DeleteFeaturedMessageByIds("usp_DeleteFeaturedMessageByIds", targetIds);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        #endregion

        #region Our Values
        public OurValuesVM GetOurValues()
        {
            try
            {
                return _unitOfWork.AdminRepo.GetOurValues("usp_GetOurValues");

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public BaseResult UpdateOurValues(OurValuesVM ourValues)
        {
            try
            {
                return _unitOfWork.AdminRepo.UpdateOurValues(ourValues);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region MonthStar
        public BaseResult AddMonthStars(MonthStarVM monthStars)
        {
            try
            {
                return _unitOfWork.AdminRepo.AddMonthStars(monthStars);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ValidateNewMonthName(string NewMonthName, int id)
        {
            try
            { 
                return _unitOfWork.AdminRepo.ValidateNewMonthName("usp_ValidateNewMonthName", new { NewMonthName = NewMonthName, id = id });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidateMonthYear(string MonthYear, int id)
        {
            try
            {
                string[] monthYear = MonthYear.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string month = monthYear[0];
                string year = monthYear[1];
                return _unitOfWork.AdminRepo.ValidateMonthYear("usp_ValidateMonthYear", new { month = month, year = year, id = id });
            }


            catch (Exception ex)
            {

                throw ex;
            }
        }

        public MonthStarVM GetMonthStarsById(int id)
        {
            try
            {

                return _unitOfWork.AdminRepo.GetMonthStarsById("usp_GetMonthStarsById", id);

            }


            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult DeleteMonthStarsByIds(DeleteItemVM targetIds)
        {

            try
            {
                BaseResult result = _unitOfWork.AdminRepo.DeleteMonthStarsByIds("usp_DeleteMonthStarsByIds", targetIds);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<MonthStarGridItemVM> GetAllmonthStars()
        {
            try
            {
                return _unitOfWork.AdminRepo.GetAllMonthStars("usp_GetAllmonthStars");

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<GazetteersGridItemVM> GetAllGazetteers()
        {
            try
            {
                return _unitOfWork.AdminRepo.GetAllGazetteers("usp_GetAllGazetteers");

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }





        public BaseResult AddGazetteers(GazetteersVM gazetteers)
        {
            try
            {

                return _unitOfWork.AdminRepo.AddGazetteers(gazetteers);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult UpdateMonthStars(MonthStarVM monthStars)
        {
            try
            {
                return _unitOfWork.AdminRepo.UpdateMonthStars(monthStars);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public BaseResult DeleteGazetteersByIds(DeleteItemVM targetIds)
        {
            try
            {
                BaseResult result = _unitOfWork.AdminRepo.DeleteMonthStarsByIds("usp_DeleteGazetteersByIds", targetIds);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string GetValueIconByValueName(string valueName)
        {
            try
            {
                string result = _unitOfWork.Repo.ExecuteQuery<string>("usp_GetValueIconByValueName", SqlCommandType.StoredProcedure, new { ValueName = valueName }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public AwardTypeInfo GetAwardById(int awardId)
        {
            try
            {
                AwardTypeInfo result = _unitOfWork.Repo.ExecuteQuery<AwardTypeInfo>("usp_GetAwardById", SqlCommandType.StoredProcedure, new { AwardId = awardId }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public RewardRecipientInfoVM GetRewardRecipientInfoById(int recipientId)
        {
            try
            {
                RewardRecipientInfoVM result = _unitOfWork.Repo.ExecuteQuery<RewardRecipientInfoVM>("usp_GetAwardRecipientInfoById", SqlCommandType.StoredProcedure, new { RecipientId = recipientId }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult InsertAwardRecipientMailContent(Guid Id, string mailContent)
        {
            try
            {
                return _unitOfWork.Repo.ExecuteQuery<BaseResult>("usp_InsertAwardRecipientMailContent", SqlCommandType.StoredProcedure, new { Id = Id, MailHtmlContent = mailContent }).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        #endregion
    }
}
