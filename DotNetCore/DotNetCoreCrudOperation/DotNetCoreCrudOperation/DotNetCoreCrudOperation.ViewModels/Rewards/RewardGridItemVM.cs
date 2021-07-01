using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.ViewModels.Rewards
{
    public class RewardGridItemVM
    {
        public int Id { get; set; }
        public int RewardRecipientId { get; set; }
        public string RewardRecipientName { get; set; }
        public string Value { get; set; }
        public int AwardTypeId { get; set; }
        public string AwardTypeName { get; set; }
        public string Testimonial { get; set; }
        public string ThankYouMessage { get; set; }
        public int RewardGivenBy { get; set; }
        public string RewardGivenByName { get; set; }
        public bool  IsSend { get; set; }
        public string CreationDate { get; set; }
        public string ModificationDate { get; set; }
        public string Roletype { get; set; }
    }
}
