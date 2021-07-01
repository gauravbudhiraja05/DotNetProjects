using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PickfordsIntranet.ViewModels.SuperAdmin
{
    public class OurValuesVM
    {
        
      
        public int Id { get; set; }
        [MaxLength(200)]
        [Required(ErrorMessage = "Value title is required")]
        public string ValueTitle { get; set; }
        public IFormFile ValueBackgrountImageData { get; set; }
        public string ValueBackgroundImage { get; set; }
        [Required(ErrorMessage = "Please enter the top left content.")]
        public string ValueTopLeftText { get; set; }
        [Required(ErrorMessage = "Please enter the top right content.")]
        public string ValueTopRightText { get; set; }
        [MaxLength(200)]
        [Required(ErrorMessage = "Communication title is required")]
        public string CommunicationTitle { get; set; }
        
        public IFormFile CommunicationIconData { get; set; }
        public string CommunicationIcon { get; set; }
        public IFormFile CommunicationImageData { get; set; }
        public string CommunicationImage { get; set; }
        [Required(ErrorMessage = "Please enter the Communication content.")]
        public string CommunicationContent { get; set; }

        public string DedicationTitle { get; set; }

        public IFormFile DedicationIconData { get; set; }
        public string DedicationIcon { get; set; }

        public IFormFile DedicationImageData { get; set; }
        public string DedicationImage { get; set; }
        [Required(ErrorMessage = "Please enter the Dedication content.")]
        public string DedicationContent { get; set; }
        [MaxLength(200)]
        [Required(ErrorMessage = "Care title is required")]
        public string CareTitle { get; set; }
        public IFormFile CareIconData { get; set; }
        public string CareIcon { get; set; }
        public IFormFile CareImageData { get; set; }
        public string CareImage { get; set; }
        [Required(ErrorMessage = "Please enter the Care content.")]
        public string CareContent { get; set; }
        [MaxLength(200)]
        [Required(ErrorMessage = "Excellent title is required")]
        public string ExcellentTitle { get; set; }
        public IFormFile ExcellentIconData { get; set; }
        public string ExcellentIcon { get; set; }
        public IFormFile ExcellentImageData { get; set; }
        public string ExcellentImage { get; set; }
        [Required(ErrorMessage = "Please enter the Excellence content.")]
        public string ExcellentContent { get; set; }
       
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
     
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }

}
