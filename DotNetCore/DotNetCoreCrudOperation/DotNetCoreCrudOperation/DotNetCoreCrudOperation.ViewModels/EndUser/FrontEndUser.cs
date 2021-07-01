using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PickfordsIntranet.ViewModels.EndUser
{
    [Serializable]
    public class FrontEndUser
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter firstname")]
        [MaxLength(40, ErrorMessage = "The first name should not be greater than 40 characters.")]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "Please enter surname")]
        [MaxLength(40, ErrorMessage = "The sur name should not be greater than 40 characters.")]
        public string SurName { get; set; }

        [MaxLength(200, ErrorMessage = "The job title should not be greater than 200 characters.")]
        //[Required(ErrorMessage = "Please enter job title.")]
        public string JobTitle { get; set; }

        //[Required(ErrorMessage = "Please enter job location")]
        [MaxLength(200, ErrorMessage = "The location should not be greater than 200 characters.")]
        public string Location { get; set; }

        //[Required(ErrorMessage = "Please enter department")]
        //[MaxLength(200, ErrorMessage = "The department should not be greater than 200 characters.")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Please enter email address")]
        [EmailAddress(ErrorMessage ="Please enter valid email address")]
        public string EmailAddress { get; set; }

        public string WindowsUserId { get; set; }

        public string EmployeeId { get; set; }

        public string LineManagerName { get; set; }

        public string LineManagerFirstName { get; set; }
        public string LineManagerEmail { get; set; }

        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter telephone number")]
        public string TelephoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter mobile number")]
        public string Mobile { get; set; }

        public string Photo { get; set; }

        [Required(ErrorMessage = "Please browse an image")]
        public IFormFile UploadImage { get; set; }

        //[Required(ErrorMessage = "Please enter my department")]
        public string MyDepartmentName { get; set; }

        public string MyDepartmentId { get; set; }

        public string OperationStatus { get; set; }

        public string OperationMessage { get; set; }

        public Int32 IsSuccess { get; set; }
    }
}
