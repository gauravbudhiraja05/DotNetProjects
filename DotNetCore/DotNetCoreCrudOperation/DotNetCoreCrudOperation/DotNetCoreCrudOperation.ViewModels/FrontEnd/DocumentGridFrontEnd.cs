using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.ViewModels.FrontEnd
{
    public class DocumentGridFrontEnd
    {
        public int DocumentId { get; set; }

        public int DepartmentId { get; set; }

        public int DocumentFolderId { get; set; }

        public int FileTypeId { get; set; }

        public int CreatedBy { get; set; }

        public string DocumentCode { get; set; }

        public string DocumentTitle { get; set; }

        public string DocumentDescription { get; set; }

        public string FileSize { get; set; }

        public string DocumentName { get; set; }

        public string FileTypeExtension { get; set; }

        public string FileTypeIcon { get; set; }

        public string FileTypeName {get;set;}

        public string Type { get; set; }

        public string LinkDestination { get; set; }
    }
}
