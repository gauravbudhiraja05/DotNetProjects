using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.ViewModels.Documents
{
    /// <summary>
    /// DocumentDeleteItemVM represents the targeted Item Id list that will be deleted for documents or folders
    /// </summary>
    public class DocumentDeleteItemVM
    {
        public List<int> ItemIds { get; set; }
        public List<String> ItemTypes { get; set; }
        public int DeletedBy { get; set; }

    }
}
