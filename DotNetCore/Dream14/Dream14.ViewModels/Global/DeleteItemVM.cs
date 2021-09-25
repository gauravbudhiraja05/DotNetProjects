using System.Collections.Generic;

namespace Dream14.ViewModels.Global
{

    /// <summary>
    /// DeleteItemVM represents the targeted Item Id list that will be deleted
    /// </summary>
    public class DeleteItemVM
    {
        public List<int> ItemIds { get; set; }
        public int DeletedBy { get; set; }
    }
}
