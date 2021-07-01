using System;
using System.Collections.Generic;
using System.Text;

namespace PickfordsIntranet.ViewModels.Global
{
   public class DocumentDetail
    {
        public int DocumentId { get; set; }

        public int SubFolderId { get; set; }

        public int ParentFolderId { get; set; }

        public int DepartMentId { get; set; }

        public int FileTypeId { get; set; }

        public string DocumentCode { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Filesize { get; set; }

        public string DocumentName { get; set; }

        public string SubFolderName { get; set; }

        public string ParentFolderName { get; set; }

        public string FileTypeName { get; set; }

        public string FileTypeExtension { get; set; }

        public string IconImageName { get; set; }

        public int CreatedBy { get; set; }

        public string FirstName { get; set; }

        public string SurName { get; set; }

        public string DocPublishDate { get; set; }

        public string DocCreationDate { get; set; }

        public string ListAuthorName { get; set; }

        public string DepartmentName { get; set; }

        public string TelephoneNumber { get; set; }

        public bool IsFavouriteDocumentForUser { get; set; }

        public string Type { get; set; }

        public string LinkDestination { get; set; }

    }
}
