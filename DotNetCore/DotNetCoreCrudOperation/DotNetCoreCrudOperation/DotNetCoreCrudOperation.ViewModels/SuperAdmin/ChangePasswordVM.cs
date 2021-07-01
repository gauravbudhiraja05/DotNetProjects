using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PickfordsIntranet.ViewModels.SuperAdmin
{
    public class ChangePasswordVM
    {
        [Required(ErrorMessage = "Please enter the old password.")]
        [StringLength(40, ErrorMessage = "The old password must be between 8 and 40 characters.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Remote(action: "ValidateOldPassword", controller: "Home")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Please enter the new password.")]
        [StringLength(40, ErrorMessage = "The new password must be between 8 and 40 characters.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please confirm the new password.")]
        [StringLength(40, ErrorMessage = "The new password must be between 8 and 40 characters.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirm password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
