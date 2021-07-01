using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.ViewModels.FAQs
{
    public class DeleteFaqVM
    {
        public List<int> ItemIds { get; set; }
        public int DeletedBy { get; set; }
    }
}
