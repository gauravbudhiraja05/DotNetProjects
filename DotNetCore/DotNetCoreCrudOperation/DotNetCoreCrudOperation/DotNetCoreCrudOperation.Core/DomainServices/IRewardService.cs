using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.Rewards;
using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.Core.DomainServices
{
    public interface IRewardService
    {
        List<RewardGridItemVM> GetAllRewards(int id);
        RewardVM GetPreRequisitesDataToAddReward(int loggedInUserId);
        BaseResult SaveReward(RewardVM reward); 
        RewardVM GetRewardById(int id,int loggedInUserId);
        RewardVM GetRewardToDisplayById(int id, int loggedInUserId);
        BaseResult DeleteRewardsByIds(DeleteItemVM targetIds);
        List<ExcelRewardFields> GetRewardsForExcel(int id);
        bool IsRewardSend(int rewardId);        
    }
}
