using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.ViewModels.SuperAdmin
{
    public class GazetteersGridItemVM
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public DateTime CreationDateToDisplay { get; set; }
        public string AuthorName { get; set; }
        public string UploadedDateToDisplay { get; set; }
    }
}
