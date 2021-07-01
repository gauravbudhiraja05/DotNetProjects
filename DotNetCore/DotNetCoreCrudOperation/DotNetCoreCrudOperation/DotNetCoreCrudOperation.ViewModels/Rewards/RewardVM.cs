using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PickfordsIntranet.ViewModels.Rewards
{
    public class RewardVM
    {
        public int Id { get; set; }

        public string RecipientName { get; set; }
        public string RecipientImage { get; set; }
        public string RecipientPost { get; set; }
        public string RecipientPlace { get; set; }
        public string RecipientEmail { get; set; }

        public string RecipientId { get; set; }

        public int ValueId { get; set; }
        public string ValueName { get; set; }
        public List<RewardValue> Values { get; set; }

        public List<RewardType> Award { get; set; }
        public int AwardId { get; set; }
        public string AwardName { get; set; }

        public string Testimonial { get; set; }
        public string ThankYouMsg { get; set; }

        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        public bool IsSend { get; set; }

        public decimal RewardAmount { get; set; }
        public string SelectedRewardAmount { get; set; }
        public List<RewardAmountVM> RewardAmountList { get; set; }
        public decimal RewardAmountVariable { get; set; }
        public RewardAmountTypeEnum RewardAmountType { get; set; }

    }

    
}
