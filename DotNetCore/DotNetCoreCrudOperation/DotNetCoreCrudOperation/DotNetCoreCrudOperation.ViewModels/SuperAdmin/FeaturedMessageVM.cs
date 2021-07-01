using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PickfordsIntranet.ViewModels.SuperAdmin
{
    /// <summary>
    /// Featured Message data structure to create it.
    /// </summary>
    public class FeaturedMessageVM
    {
        public int Id { get; set; }
        public string MessageCode { get; set; }
        [Required(ErrorMessage = "Please upload an image.")]
        public IFormFile MessageImage { get; set; }
        public string ImageName { get; set; }
        [Required(ErrorMessage = "Please enter the content.")]
        public string Content { get; set; }
        public bool Live { get; set; }
        [MaxLength(40, ErrorMessage = "The author name should not be greater than 40 characters.")]
        [Required(ErrorMessage = "Please enter the author name.")]
        public string AuthorName { get; set; }
        [Required(ErrorMessage = "Please enter date")]
        public DateTime CreationDate { get; set; }
        public string CreationDateToDisplay { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public string CreationDateString { get; set; }
    }
}
