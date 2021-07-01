using Microsoft.Extensions.Configuration;
using PickfordsIntranet.Core.DomainServices;
using PickfordsIntranet.Core.Repositories;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.Rewards;
using PickfordsIntranet.ViewModels.SuperAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PickfordsIntranet.Services
{
    public class RewardService : IRewardService
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
        public RewardService(IUnitOfWork unitOfWork, IConfigurationRoot config)
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


        public List<RewardGridItemVM> GetAllRewards(int id)
        {
            try
            {
                var result = _unitOfWork.RewRepo.GetAllRewards("usp_GetAllRewards" ,new { UserId = id });
                //var result= _unitOfWork.Repo.ExecuteQuery<AdminUserGridItemVM>("usp_GetAllAdminUsers", SqlCommandType.StoredProcedure);
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RewardVM GetPreRequisitesDataToAddReward(int loggedInUserId)
        {
            try
            {
                RewardVM reward = _unitOfWork.RewRepo.GetPreRequisitesDataToAddReward("usp_GetPreRequisitesDataToCreateReward");

                reward = AwardTypeDropdownBindingWithUserPrivilege(loggedInUserId, reward);

                List<RewardValue> rewardVal = new List<RewardValue>();
                rewardVal.Add(new RewardValue() { Key = 0, Value = "Select Values" });
                rewardVal.Add(new RewardValue() { Key = 1, Value = "Dedication" });
                rewardVal.Add(new RewardValue() { Key = 2, Value = "Care" });
                rewardVal.Add(new RewardValue() { Key = 3, Value = "Excellence" });
                rewardVal.Add(new RewardValue() { Key = 4, Value = "Communication" });

                reward.Values = rewardVal;

                 reward.RewardAmountList = new List<RewardAmountVM>()
                                            {
                                                new RewardAmountVM(){ RewardAmountText="£ 25",RewardAmountValue="25" },
                                                new RewardAmountVM(){ RewardAmountText="£ 50",RewardAmountValue="50" },
                                                new RewardAmountVM(){ RewardAmountText="£ 75",RewardAmountValue="75" },
                                                new RewardAmountVM(){ RewardAmountText="£ 100",RewardAmountValue="100" }
                                            };
                       


                return reward;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private RewardVM AwardTypeDropdownBindingWithUserPrivilege(int loggedInUserId, RewardVM reward)
        {
            try
            {
                // Check Line Manager Privilege for Reward And Recogition
                var userRewardAndRecognitionPrivilege = _unitOfWork.Repo.ExecuteQuery<AdminUserVM>("usp_CheckLineManagerPrivilegeForRewardAndRecognitionByUserId", SqlCommandType.StoredProcedure, new { UserId = loggedInUserId }).FirstOrDefault();

                if (userRewardAndRecognitionPrivilege.IsSuperUser)
                {
                    reward.Award.Insert(0, new RewardType() { Key = 0, Value = "Select Reward type" });

                    // put the "Recognise Only" at index 1
                    var recogniseOnly = reward.Award.FirstOrDefault(a => a.Value == _config["ServerEnd:SuperAdmin:LineManagerCanTextRecogniseOnly"]);
                    reward.Award.Remove(recogniseOnly);
                    reward.Award.Insert(1, recogniseOnly);
                }

                if (userRewardAndRecognitionPrivilege.IsLineManagerUser)
                {
                    string lineManagerCan = userRewardAndRecognitionPrivilege.LineManagerCanSelectOption;
                    if (lineManagerCan.Equals(_config["ServerEnd:SuperAdmin:LineManagerCanValueRecogniseOnly"]))
                    {
                        // Remove the all Award Types except Recognise Only 
                        reward.Award.RemoveAll(a => a.Value != _config["ServerEnd:SuperAdmin:LineManagerCanTextRecogniseOnly"]);
                        reward.Award.Insert(0, new RewardType() { Key = 0, Value = "Select Reward type" });
                    }

                    else
                    {
                        reward.Award.Insert(0, new RewardType() { Key = 0, Value = "Select Reward type" });

                        // put the "Recognise Only" at index 1
                        var recogniseOnly = reward.Award.FirstOrDefault(a => a.Value == _config["ServerEnd:SuperAdmin:LineManagerCanTextRecogniseOnly"]);
                        reward.Award.Remove(recogniseOnly);
                        reward.Award.Insert(1, recogniseOnly);
                    }
                }

                return reward;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public BaseResult SaveReward(RewardVM reward)
        {
            try
            {

                return _unitOfWork.RewRepo.SaveReward(reward);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public RewardVM GetRewardById(int id, int loggedInUserId)
        {
            try
            {

                var reward = _unitOfWork.RewRepo.GetRewardById("usp_GetRewardById", new { RewardId = id });

                reward = AwardTypeDropdownBindingWithUserPrivilege(loggedInUserId, reward);

                List<RewardValue> rewardVal = new List<RewardValue>();
                rewardVal.Add(new RewardValue() { Key = 0, Value = "Select Values" });
                rewardVal.Add(new RewardValue() { Key = 1, Value = "Dedication" });
                rewardVal.Add(new RewardValue() { Key = 2, Value = "Care" });
                rewardVal.Add(new RewardValue() { Key = 3, Value = "Excellence" });
                rewardVal.Add(new RewardValue() { Key = 4, Value = "Communication" });

                reward.Values = rewardVal;

                foreach (var item in reward.Values)
                {
                    if (item.Value == reward.ValueName)
                    {
                        reward.ValueId = item.Key;
                    }
                }

                reward.RewardAmountList = new List<RewardAmountVM>()
                        {
                            new RewardAmountVM(){ RewardAmountText="£ 25",RewardAmountValue="25" },
                            new RewardAmountVM(){ RewardAmountText="£ 50",RewardAmountValue="50" },
                            new RewardAmountVM(){ RewardAmountText="£ 75",RewardAmountValue="75" },
                            new RewardAmountVM(){ RewardAmountText="£ 100",RewardAmountValue="100" }
                        };

                // Set RewardAmountType
                switch (reward.AwardName)
                {
                    case "Love 2 Shop Gift Voucher":
                    case "Amazon Gift Voucher":
                    case "iTunes Voucher":
                    case "Experience of your choice":
                        reward.RewardAmountType = RewardAmountTypeEnum.Dropdown;
                        reward.SelectedRewardAmount = string.Format("{0:G29}", reward.RewardAmount);
                        break;
                    case "Team Meal":
                    case "Commission Amount":
                        reward.RewardAmountType = RewardAmountTypeEnum.Textbox;
                        reward.RewardAmountVariable = reward.RewardAmount;
                        break;
                    case "Recognise Only":
                    case "An extra day\'s holiday":
                        reward.RewardAmountType = RewardAmountTypeEnum.None;
                        break;
                    default:
                        reward.RewardAmountType = RewardAmountTypeEnum.None;
                        break;
                }

                return reward;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public RewardVM GetRewardToDisplayById(int id, int loggedInUserId)
        {
            try
            {

                var reward = _unitOfWork.RewRepo.GetRewardById("usp_GetRewardById", new { RewardId = id });

                reward.Award.Insert(0, new RewardType() { Key = 0, Value = "Select Reward type" });

                List<RewardValue> rewardVal = new List<RewardValue>();
                rewardVal.Add(new RewardValue() { Key = 0, Value = "Select Values" });
                rewardVal.Add(new RewardValue() { Key = 1, Value = "Dedication" });
                rewardVal.Add(new RewardValue() { Key = 2, Value = "Care" });
                rewardVal.Add(new RewardValue() { Key = 3, Value = "Excellence" });
                rewardVal.Add(new RewardValue() { Key = 4, Value = "Communication" });

                reward.Values = rewardVal;

                foreach (var item in reward.Values)
                {
                    if (item.Value == reward.ValueName)
                    {
                        reward.ValueId = item.Key;
                    }
                }

                reward.RewardAmountList = new List<RewardAmountVM>()
                        {
                            new RewardAmountVM(){ RewardAmountText="£ 25",RewardAmountValue="25" },
                            new RewardAmountVM(){ RewardAmountText="£ 50",RewardAmountValue="50" },
                            new RewardAmountVM(){ RewardAmountText="£ 75",RewardAmountValue="75" },
                            new RewardAmountVM(){ RewardAmountText="£ 100",RewardAmountValue="100" }
                        };

                // Set RewardAmountType
                switch (reward.AwardName)
                {
                    case "Love 2 Shop Gift Voucher":
                    case "Amazon Gift Voucher":
                    case "iTunes Voucher":
                    case "Experience of your choice":
                        reward.RewardAmountType = RewardAmountTypeEnum.Dropdown;
                        reward.SelectedRewardAmount = string.Format("{0:G29}", reward.RewardAmount);
                        break;
                    case "Team Meal":
                    case "Commission Amount":
                        reward.RewardAmountType = RewardAmountTypeEnum.Textbox;
                        reward.RewardAmountVariable = reward.RewardAmount;
                        break;
                    case "Recognise Only":
                    case "An extra day\'s holiday":
                        reward.RewardAmountType = RewardAmountTypeEnum.None;
                        break;
                    default:
                        reward.RewardAmountType = RewardAmountTypeEnum.None;
                        break;
                }
                return reward;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public BaseResult DeleteRewardsByIds(DeleteItemVM targetIds)
        {

            try
            {
                BaseResult result = _unitOfWork.RewRepo.DeleteRewardsByIds("usp_DeleteRewardsByIds", targetIds);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ExcelRewardFields> GetRewardsForExcel(int id)
        {
            try
            {
                var result = _unitOfWork.RewRepo.GetAllRewardsExcel("usp_GetAllRewardsForExcel", new { UserId = id });
                //var result= _unitOfWork.Repo.ExecuteQuery<AdminUserGridItemVM>("usp_GetAllAdminUsers", SqlCommandType.StoredProcedure);
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsRewardSend(int rewardId)
        {
            bool isSend = false;
            try
            {
                var result = _unitOfWork.Repo.ExecuteQuery<RewardVM>("usp_IsRewardSend",SqlCommandType.StoredProcedure, new { RewardId = rewardId }).FirstOrDefault();
                if (result != null)
                    isSend = result.IsSend;
                
                return isSend;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
