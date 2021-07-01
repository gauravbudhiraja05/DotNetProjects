using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PickfordsIntranet.ViewModels.Auth;
using PickfordsIntranet.ViewModels.Global;

namespace PickfordsIntranet.ViewModels.Vacancy
{
    /// <summary>
    /// NewsVM data structure to create news
    /// </summary>
    public class VacancyVM
    {
        public int Id { get; set; }

        public string VacancyCode { get; set; }

        [MaxLength(80, ErrorMessage = "The vacancy title should not be greater than 80 characters.")]
        [Required(ErrorMessage = "Please enter the vacancy title.")]
        public string Title { get; set; }

        [MaxLength(300, ErrorMessage = "The vacancy teaser text should not be greater than 300 characters.")]
        [Required(ErrorMessage = "Please enter the vacancy teaser text.")]
        public string TeaserText { get; set; }

        //[MaxLength(2500, ErrorMessage = "Vacancy content should not be greater than 2500 characters.")]
        [Required(ErrorMessage = "Please enter the vacancy content.")]
        public string Content1 { get; set; }

        //[MaxLength(2500, ErrorMessage = "Vacancy content should not be greater than 2500 characters.")]
        //[Required(ErrorMessage = "Please enter news content 2")]
        public string Content2 { get; set; }

        public int DepartmentId { get; set; }
        public List<DepartmentVM> AllDepartments { get; set; }

        //public bool IsFeatureOnHomePage { get; set; }

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

        //[Required(ErrorMessage = "Please Enter Publish Date")]
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
