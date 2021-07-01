using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PickfordsIntranet.ViewModels.Documents
{
    public class DocumentAttachement
    {
        public string FileName { get; set; }

        public string FileType { get; set; }

        public byte[] FileContent { get; set; }

        public int Id { get; set; }

        [Required(ErrorMessage = "Please browse an image.")]
        public IFormFile MessageImage { get; set; }

        public string ImageName { get; set; }

        [Required(ErrorMessage = "Please enter content.")]
        public string Content { get; set; }

        public bool Live { get; set; }

        [Required(ErrorMessage = "Please enter author name.")]
        public string AuthorName { get; set; }

        [Required(ErrorMessage = "Please enter date.")]
        public DateTime CreationDate { get; set; }

        public string CreationDateToDisplay { get; set; }

        public int CreatedBy { get; set; }

        public int ModifiedBy { get; set; }
    }
}
