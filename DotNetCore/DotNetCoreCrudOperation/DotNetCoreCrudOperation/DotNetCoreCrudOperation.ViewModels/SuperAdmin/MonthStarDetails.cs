using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace PickfordsIntranet.ViewModels.SuperAdmin
{
    public class MonthStarDetails
    {
        public int Id { get; set; }

        public int MonthStarId { get; set; }

        [MaxLength(100, ErrorMessage = "Star name should not be greater than 100 characters")]
        public string StarName { get; set; }

        [MaxLength(200, ErrorMessage = "Role should not be greater than 200 characters")]
        public string StarRole { get; set; }

        [MaxLength(200, ErrorMessage = "Location should not be greater than 200 characters")]
        public string StarLocation { get; set; }

        public string StarPhoto { get; set; }

        public IFormFile StarPhotoData { get; set; }

        [MaxLength(500, ErrorMessage = "Testimonial should not be greater than 500 characters")]
        public string StarTestimonial { get; set; }

        public string StarValueType { get; set; }

        public bool? IsActive { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }

        public int CreatedBy { get; set; }

        public int? ModifiedBy { get; set; }

        public byte[] Timestamp { get; set; }

        public string FrontEndUserImageForStar { get; set; }

    }
}
