using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PickfordsIntranet.ViewModels.EndUser
{
    public class EndUserVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter first name")]
        [MaxLength(80, ErrorMessage = "More than 80 character is not allowed")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Surname")]
        [MaxLength(80, ErrorMessage = "More than 80 character is not allowed")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Please enter email address")]
        [MaxLength(80, ErrorMessage = "More than 80 character is not allowed")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Remote(action: "ValidateEMailExistOrNot", controller: "EndUser", AdditionalFields = "Id")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Please enter Password")]
        [MaxLength(80, ErrorMessage = "More than 80 character is not allowed")]
        public string Password { get; set; }

    }
}
