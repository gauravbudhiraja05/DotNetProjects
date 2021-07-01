using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.Rewards;
using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.Core.Repositories
{
    public interface IRewardRepository
    {
        List<RewardGridItemVM> GetAllRewards(string query, object param);
        RewardVM GetPreRequisitesDataToAddReward(string query);
        BaseResult SaveReward(RewardVM reward);
        RewardVM GetRewardById(string query, object param);
        BaseResult DeleteRewardsByIds(string query, DeleteItemVM targetIds);
        List<ExcelRewardFields> GetAllRewardsExcel(string query, object param);        
    }
}
