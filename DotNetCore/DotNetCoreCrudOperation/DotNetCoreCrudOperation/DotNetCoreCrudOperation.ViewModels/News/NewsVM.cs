using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickfordsIntranet.ViewModels.Auth;
using PickfordsIntranet.ViewModels.Global;

namespace PickfordsIntranet.ViewModels.News
{
    /// <summary>
    /// NewsVM data structure to create news
    /// </summary>
    public class NewsVM
    {
        public int Id { get; set; }

        public string NewsCode { get; set; }

        [MaxLength(80, ErrorMessage = "The news title should not be greater than 80 characters.")]
        [Required(ErrorMessage = "Please enter the news title.")]
        public string Title { get; set; }

        [MaxLength(300, ErrorMessage = "The teaser text should not be greater than 300 characters.")]
        [Required(ErrorMessage = "Please enter the news teaser text.")]
        public string TeaserText { get; set; }

        //[MaxLength(2500, ErrorMessage = "News content should not be greater than 2500 characters.")]
        [Required(ErrorMessage = "Please enter the news content.")]
        public string Content1 { get; set; }

        //[MaxLength(2500, ErrorMessage = "News content should not be greater than 2500 characters.")]
        public string Content2 { get; set; }

        public int DepartmentId { get; set; }
        public List<DepartmentVM> AllDepartments { get; set; }

        public bool IsFeatureOnHomePage { get; set; }

        public string ThumbnailImage { get; set; }
        public byte[] ThumbnailImageArray { get; set; }

        //[Required(ErrorMessage = "Please browse an image")]
        public IFormFile ThumbnailImg { get; set; }

        public string MainImage { get; set; }
        public byte[] MainImageArray { get; set; }

        //[Required(ErrorMessage = "Please browse an image")]
        public IFormFile MainImg { get; set; }

        public string AdditionalImage1 { get; set; }
        public byte[] AdditionalImage1Array { get; set; }

        //[Required(ErrorMessage = "Please browse an image")]
        public IFormFile AdditionalImg1 { get; set; }

        public string AdditionalImage2 { get; set; }
        public byte[] AdditionalImage2Array { get; set; }

        //[Required(ErrorMessage = "Please browse an image")]
        public IFormFile AdditionalImg2 { get; set; }

        public DateTime PublishDate { get; set; }

        [Required(ErrorMessage = "Please enter the publish date.")]
        public string PublishDateDisplay { get; set; }

        public string CreationDate { get; set; }

        [MaxLength(40, ErrorMessage = "The author name not must be greater than 40 characters.")]
        [Required(ErrorMessage = "Please enter the author name.")]
        public string AuthorName { get; set; }

        public int CreatedBy { get; set; }

        public int ModifiedBy { get; set; }

        public int IsActive { get; set; }
    }
}
