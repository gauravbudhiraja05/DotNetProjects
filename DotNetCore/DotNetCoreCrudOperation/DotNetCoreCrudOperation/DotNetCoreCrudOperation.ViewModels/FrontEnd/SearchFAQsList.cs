using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.ViewModels.FrontEnd
{
    public class SearchFAQsList
    {
        public List<DocumentGridFrontEnd> FAQsAttachedDocumentList { get; set; }

        public List<FAQGridVM> FAQsList { get; set; }
    }
}
