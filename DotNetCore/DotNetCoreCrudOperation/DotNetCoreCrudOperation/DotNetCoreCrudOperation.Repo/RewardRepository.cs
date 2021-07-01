using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Dapper;
using PickfordsIntranet.Core.Repositories;
using PickfordsIntranet.ViewModels.Auth;
using PickfordsIntranet.ViewModels.Global;
using PickfordsIntranet.ViewModels.Rewards;
using PickfordsIntranet.ViewModels.SuperAdmin;
namespace PickfordsIntranet.Repo
{
    public class RewardRepository : Repository, IRewardRepository
    {
        /// <summary>
        /// RewardRepository Constructor
        /// </summary>
        /// <param name="context">Database Context</param>
        public RewardRepository(IDbConnection connection) : base(connection)
        {

        }


        /// <summary>
        /// Database Connection
        /// </summary>
        public IDbConnection DatabaseConnection
        {
            get { return Connection as IDbConnection; }
        }

        public List<RewardGridItemVM> GetAllRewards(string query, object param)
        {
            try
            {
                List<RewardGridItemVM> result = new List<RewardGridItemVM>();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {

                    result = multi.Read<RewardGridItemVM>().AsList();
                }
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public RewardVM GetPreRequisitesDataToAddReward(string query)
        {
            try
            {
                RewardVM reward = new RewardVM();
                using (var multi = Connection.QueryMultiple(query))
                {
                   // reward.Values = multi.Read<RewardValue>().AsList();
                    reward.Award = multi.Read<RewardType>().AsList();
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
                var result = this.ExecuteQuery<BaseResult>("usp_AddUpdateReward", SqlCommandType.StoredProcedure,
                    new
                    {
                        RewardId = reward.Id,
                        RecipientName= reward.RecipientName,
                        RecipientId = reward.RecipientId,
                        ValueId = reward.ValueId,
                        ValueName = reward.ValueName,
                        AwardId =reward.AwardId,
                        AwardName =reward.AwardName,
                        Testimonial =reward.Testimonial,
                        ThankYouMessage =reward.ThankYouMsg,
                        RewardGivenBy = reward.CreatedBy,
                        IsSend = reward.IsSend,
                        RewardAmount = reward.RewardAmount
                    }).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public RewardVM GetRewardById(string query, object param)
        {
            try
            {
                RewardVM reward = new RewardVM();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {
                    reward = multi.Read<RewardVM>().SingleOrDefault();
                    reward.Award = multi.Read<RewardType>().AsList();
                }

                return reward;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BaseResult DeleteRewardsByIds(string query, DeleteItemVM targetIds)
        {
            try
            {
                var IdsWithDelimitedPipeline = string.Join('|', targetIds.ItemIds);
                var result = Connection.Query<BaseResult>(query, new { TargetIds = IdsWithDelimitedPipeline, DeletedBy = targetIds.DeletedBy }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<ExcelRewardFields> GetAllRewardsExcel(string query, object param)
        {
            try
            {
                List<ExcelRewardFields> result = new List<ExcelRewardFields>();
                using (var multi = Connection.QueryMultiple(query, param, null, null, CommandType.StoredProcedure))
                {

                    result = multi.Read<ExcelRewardFields>().AsList();
                }
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }       
    }
}
