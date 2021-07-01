using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PickfordsIntranet.ViewModels.Departments
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Please enter the department name.")]
        [MaxLength(80, ErrorMessage = "More than 80 character is not allowed")]
        [Remote(action: "ValidateDepartmentExistOrNot", controller: "Department", AdditionalFields = "DepartmentId")]
        public string DepartmentName { get; set; }

        public bool IsActive { get; set; }

        //[Required(ErrorMessage = "Please Enter Creation Name")]
        public string CreationDate { get; set; }

        public string CreationDateDisplay { get; set; }

        [Required(ErrorMessage = "Please enter the telephone number.")]
        [MaxLength(20, ErrorMessage = "More than 20 character is not allowed")]
        public string TelephoneNumber { get; set; }

        //[Required(ErrorMessage = "Please upload a Header Image.")]
        public IFormFile HeaderImage { get; set; }

        public byte[] HeaderImageArray { get; set; }

        public string ImageName { get; set; }

        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter the department head.")]
        public string DepartmentHead { get; set; }
    }
}
