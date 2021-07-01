using Microsoft.AspNetCore.Http;
using PickfordsIntranet.ViewModels.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PickfordsIntranet.ViewModels.Documents
{
    public class DocumentGridItemVM
    {
        public List<DocumentDetail> Documents { get; set; }

        public List<FolderDetails> Folders { get; set; }

        public int DepartmentId { get; set; }

        public int DocumentId { get; set; }

        public int FileTypeId { get; set; }

        public int FolderId { get; set; }

        public string FileTypeExtension { get; set; }
       
        public IFormFile UploadFile { get; set; }

        public int CreatedBy { get; set; }

        public string FileName { get; set; }

        public string FileSize { get; set; }

        [Required(ErrorMessage = "Please enter the document title.")]
        public string DocumentTitle { get; set; }

       
        public string DocumentDescription { get; set; }

        public bool IsFeatureDocument { get; set; }

        public string CreationDate { get; set; }

        public string DocumentCode { get; set; }

        public string DefaultDocumentAddAuthorName { get; set; }

        public string PublishDateDisplay { get; set; }

        public string DepartmentName { get; set; }

        public bool IsPublishDocument { get; set; }

        public string Type { get; set; }

        public string LinkDestination { get; set; }
    }

    
}
